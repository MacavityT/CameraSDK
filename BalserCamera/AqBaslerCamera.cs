using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using AqDevice;
using Basler.Pylon;


namespace BalserCamera
{
    public class  AqBaslerCamera : AqDevice.IAqCamera
    {
        private string id;
        private string name;
        private string ip;
        private string mac;
        private double exposuretime;
        private double acquisitionfrequency;
        private double triggerdelay;
        private double gain;
        private bool gainauto;

        
        private AqDevice.TriggerSources triggersource;
        private AqDevice.TriggerSwitchs triggerswitchs;
        private AqDevice.TriggerModes triggermodes;
        private AqDevice.TriggerEdges triggeredges;

        //set window event
        private event AqDevice.AqCaptureDelegate eventCapture;
        //set get image event
        public static List<IAqCamera> allbaslercamera = null;
        public Camera getonecamera;
        private Stopwatch stopWatch = new Stopwatch();
        private PixelDataConverter converter = new PixelDataConverter();

        public static List<IAqCamera> AllBalserCamera
        {
            get { return allbaslercamera; }
            set { allbaslercamera = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Mac
        {
            get { return mac; }
            set { mac = value; }
        }

        public double ExposureTime
        {
            get { return exposuretime; }
            set { exposuretime = value; }
        }

        public double Gain
        {
            get { return gain; }
            set { gain = value; }
        }

        public double AcquisitionFrequency
        {
            get { return acquisitionfrequency; }
            set { acquisitionfrequency = value; }
        }

        public double TriggerDelay
        {
            get { return triggerdelay; }
            set { triggerdelay = value; }
        }

        public bool GainAuto
        {
            get { return gainauto; }
            set { gainauto = value; }
        }

        public AqDevice.TriggerSources TriggerSource
        {
            get { return triggersource; }
            set { triggersource = value; }
        }

        public AqDevice.TriggerModes TriggerMode
        {
            get { return triggermodes; }
            set { triggermodes = value; }
        }

        public AqDevice.TriggerEdges TriggerEdge
        {
            get { return triggeredges; }
            set { triggeredges = value; }
        }

        public AqDevice.TriggerSwitchs TriggerSwitch
        {
            get { return triggerswitchs; }
            set { triggerswitchs = value; }
        }

        public int OpenCamera()
        {
            List<ICameraInfo> allCameras = CameraFinder.Enumerate();
            foreach (ICameraInfo tempinfo in allCameras)
            {
                if (tempinfo[CameraInfoKey.UserDefinedName] == name)
                {
                    getonecamera = new Camera(tempinfo);
                    if (getonecamera.IsOpen)
                        return 0;
                    getonecamera.Open();
                }
            }
            return 1;
        }

        public int CloseCamera()
        {
            if (getonecamera.IsConnected)
            {
                getonecamera.Close();
                return 1;
            }        
            return 0;
        }

        public int OpenStream()
        {
            if (getonecamera.IsOpen)
            {
                getonecamera.StreamGrabber.Start();
                return 1;
            }

            return 0;
        }

        public int CloseStream()
        {
            if (getonecamera.StreamGrabber.IsGrabbing)
            {
                getonecamera.StreamGrabber.Stop();
                return 1;
            }
            return 0;
        }

        public void RegisterCaptureCallback(AqCaptureDelegate delCaptureFun)
        {
            eventCapture += delCaptureFun;
        }

        public void CallFunction(object obj, Bitmap bmp)
        {
            eventCapture(obj,bmp);
        }

        public void TriggerSoftware()
        {
           TriggerMode = AqDevice.TriggerModes.Unknow;
        }

        private void TriggerConfiguration()
        {
            if (triggermodes == TriggerModes.Continuous)
            {
                getonecamera.CameraOpened += Configuration.AcquireContinuous;
                getonecamera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
            }      
            else if(triggermodes == TriggerModes.Unknow)
            {
                getonecamera.CameraOpened += Configuration.SoftwareTrigger;
                getonecamera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
            }
        }

        public void TriggerHardWare()
        {
            GetSoftWareFrame();
        }

        public void SetExposureTime()
        {
 
        }

        public void GetSoftWareFrame()
        {
            getonecamera.StreamGrabber.Start(1);
            while (getonecamera.StreamGrabber.IsGrabbing)
            {
                if (getonecamera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException))
                {
                    getonecamera.ExecuteSoftwareTrigger();
                }
                // Wait for an image and then retrieve it. A timeout of 5000 ms is used. 
                IGrabResult grabResult = getonecamera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                using (grabResult)
                {
                    // Image grabbed successfully? 
                    if (grabResult.GrabSucceeded)
                    {
                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.
                        converter.OutputPixelFormat = PixelType.BGRA8packed;
                        IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                        bitmap.UnlockBits(bmpData);
                        Bitmap temp = bitmap;
                        CallFunction(null, temp); 
                    }
                    else
                    {
                        Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                    }
                }
            }

        }

        public void OnImageGrabbed(object sender,ImageGrabbedEventArgs e)
        {
                          // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();

                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.
                        converter.OutputPixelFormat = PixelType.BGRA8packed;
                        IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                        bitmap.UnlockBits(bmpData);
                        Bitmap temp = bitmap;
                        CallFunction(null, temp);                       
                    }
                }
        }

    }
}
