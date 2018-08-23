using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Drawing.Imaging;
using AqDevice;
using System.Diagnostics;


namespace TestBaslerCamera
{
    public partial class Form1 : Form
    {

        AqDevice.IAqCameraManager cameramanager = null;
        List<AqDevice.IAqCamera> cameras;
        bool triggerselect = false;
        bool opencammeraflag = false;
        bool aqcuisition = false;
        private Stopwatch stopWatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        private void opencamera_Click(object sender, EventArgs e)
        {
            string dllpath = System.IO.Directory.GetCurrentDirectory() + "\\BalserCamera.dll";
            Assembly assem = Assembly.LoadFile(dllpath);
            Type type = assem.GetType("AqDevice.AqCameraFactory");
            MethodInfo mi = type.GetMethod("GetInstance");
            object obj = mi.Invoke(null, null);

            cameramanager = (IAqCameraManager)obj;
            cameramanager.Init();
            cameras = cameramanager.GetCameras();
            if (cameras.Count == 0)
                return ;
            //cameras[0].Name = "Aqrose-4";
            cameras[0].TriggerMode = AqDevice.TriggerModes.Unknow;
;
            cameras[0].RegisterCaptureCallback(new AqCaptureDelegate(RecCapture));
      //      cameras[0].RegisterAcquisitionCallback(new AqAcquisitionDelegate(RecAcquisition));
            cameras[0].ExposureTime = 35000;
            cameras[0].OpenCamera();
            if (cameras[0].OpenStream() == 1)
                opencammeraflag = true;
                     
        }

        public void RecAcquisition(object objUserparam)
        {
            aqcuisition = (bool)objUserparam;

            string result1 = @"D:\result.txt";
            FileStream fs = new FileStream(result1, FileMode.Append);
            StreamWriter wr = null;
            wr = new StreamWriter(fs);
            wr.WriteLine(Convert.ToString(aqcuisition));
            wr.Close();      
        }

        public void RecCapture(object objUserparam, Bitmap bitmap)
        {
            stopWatch.Stop();
            long tt = stopWatch.ElapsedMilliseconds;
            string result1 = @"D:\result1.txt";
            FileStream fs = new FileStream(result1, FileMode.Append);
            StreamWriter wr = null;
            wr = new StreamWriter(fs);
            wr.WriteLine(Convert.ToString(tt));
            wr.Close();
            this.pictureBox1.Image = bitmap;
        }

        private void openstream_Click(object sender, EventArgs e)
        {
            if (triggerselect == true)
            {
                triggerselect = false;
                cameras[0].TriggerMode = AqDevice.TriggerModes.Unknow;
                this.openstream.Text = "软件触发";
                if (opencammeraflag)
                    cameras[0].CloseCamera();
                opencammeraflag = false;
                Thread.Sleep(20);
                cameras[0].RegisterCaptureCallback(new AqCaptureDelegate(RecCapture));
     //           cameras[0].RegisterAcquisitionCallback(new AqAcquisitionDelegate(RecAcquisition));
                cameras[0].ExposureTime = 35000;
                cameras[0].OpenCamera();
                if (cameras[0].OpenStream() == 1)
                    opencammeraflag = true;
            }
            else
            {
                triggerselect = true;
                cameras[0].TriggerMode = AqDevice.TriggerModes.Continuous;
                this.openstream.Text = "连续采集";
                if (opencammeraflag)
                    cameras[0].CloseCamera();
                Thread.Sleep(20);
                opencammeraflag = false;
                cameras[0].RegisterCaptureCallback(new AqCaptureDelegate(RecCapture));
                cameras[0].ExposureTime = 35000;
                cameras[0].OpenCamera();
                if (cameras[0].OpenStream() == 1)
                    opencammeraflag = true;
            }
        }

        private void closestream_Click(object sender, EventArgs e)
        {
            
            if (triggerselect == false)
            {
                cameras[0].TriggerSoftware();
            }  
        }

        private void closecamera_Click(object sender, EventArgs e)
        {
            if (opencammeraflag)
               cameras[0].CloseCamera();
            Thread.Sleep(20);
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
            stopWatch.Reset();
            stopWatch.Start();
            if (triggerselect == false)
            {
                cameras[0].TriggerSoftware();
            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string eeee = settimer.Text;
            try
            {
                int.Parse(eeee);               
                timer1.Interval = Convert.ToInt16(settimer.Text);
                if (!timer1.Enabled)
                    timer1.Start();

            }
            catch (Exception)
            {
                timer1.Interval = 500;
                if (!timer1.Enabled)
                    timer1.Start();
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled)
                timer1.Stop();
        }
    }
}


