using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using GxIAPINET;
using AqDevice;


namespace DaHengCamera
{
    public class AqDaHengCamera :AqDevice.IAqCamera
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
        public static List<IAqCamera> allDaHengcamera = null;

        IGXStream m_objIGXStream = null;
        IGXDevice m_objIGXDevice = null;                    ///<设备对像
        IGXFeatureControl m_objIGXFeatureControl = null;    ///<远端设备属性控制器对像
        static IGXFactory m_objIGXFactory = null;
        private bool m_bIsSnap = false;

        public static List<IAqCamera> AllDaHengcamera
        {
            get { return allDaHengcamera; }
            set { allDaHengcamera = value; }
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

        public void RegisterCaptureCallback(AqCaptureDelegate delCaptureFun)
        {
            eventCapture += delCaptureFun;
        }

        public void CallFunction(object obj, Bitmap bmp)
        {
            eventCapture(obj, bmp);
        }

        static public GxIAPINET.IGXFactory ObjIGXFactory
        {
            get { return m_objIGXFactory; }
            set { m_objIGXFactory = value; }
        }

        public  int OpenCamera()
        {
            //关闭流
            if (null != m_objIGXStream)
            {
                m_objIGXStream.Close();
                m_objIGXStream = null;
            }

            //关闭设备
            if (null != m_objIGXDevice)
            {
                m_objIGXDevice.Close();
                m_objIGXDevice = null;
            }

            IGXFactory m_objIGXFactory = null;
            m_objIGXFactory = IGXFactory.GetInstance();
            m_objIGXFactory.Init();
            List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();
            m_objIGXFactory.UpdateDeviceList(200, listGXDeviceInfo);

            foreach (IGXDeviceInfo tempinfo in listGXDeviceInfo)
            {
                if (tempinfo.GetUserID() == this.name)
                {
                    m_objIGXDevice = ObjIGXFactory.OpenDeviceByUserID(this.Name, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                    m_objIGXFeatureControl = m_objIGXDevice.GetRemoteFeatureControl();
                }
                else
                {
                    return 0;
                }
            }
                           
            TriggerConfiguration();
            SetExposureTime();
        
            return 0;
        }

        public  int CloseCamera()
        {
            if (!m_bIsSnap)
            {
                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                m_objIGXFeatureControl = null;
                m_bIsSnap = true;
            }

            //停止流通道、注销采集回调和关闭流
            if (null != m_objIGXStream)
            {
                m_objIGXStream.StopGrab();
                //注销采集回调函数
                m_objIGXStream.UnregisterCaptureCallback();
                m_objIGXStream.Close();
                m_objIGXStream = null;
            }

            //关闭设备
            if (null != m_objIGXDevice)
            {
                m_objIGXDevice.Close();
                m_objIGXDevice = null;
            }

            return 0;
        }

        private void TriggerConfiguration()
        {
            if (triggermodes == TriggerModes.Continuous)
            {
                m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("Off");
                m_objIGXFeatureControl.GetEnumFeature("AcquisitionMode").SetValue("Continuous");
            }
            else if (triggermodes == TriggerModes.Unknow)
            {
                m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");
                m_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");
                m_objIGXFeatureControl.GetEnumFeature("TriggerActivation").SetValue("RisingEdge");

            }
            else 
            {
                m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("on");
                m_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Line0");
                m_objIGXFeatureControl.GetEnumFeature("TriggerActivation").SetValue("RisingEdge");
            }
        }

        public void SetExposureTime()
        {
            double dMin = 0.0;                     
            double dMax = 0.0;
            dMin = m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin();
            dMax = m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax();
         
            if(exposuretime >= dMin && exposuretime <= dMax)
                m_objIGXFeatureControl.GetFloatFeature("ExposureTime").SetValue(exposuretime);

        }

        public  int OpenStream()
        {
            //打开流
            if (null != m_objIGXDevice)
            {
                m_objIGXStream = m_objIGXDevice.OpenStream(0);
            }

            //开启采集流通道
            if (null != m_objIGXStream)
            {
                //RegisterCaptureCallback第一个参数属于用户自定参数(类型必须为引用
                //类型)，若用户想用这个参数可以在委托函数中进行使用
                m_objIGXStream.RegisterCaptureCallback(null, __OnFrameCallbackFun);
                m_objIGXStream.StartGrab();
            }

            //发送开采命令
            if (null != m_objIGXFeatureControl)
            {
                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
            }

            m_bIsSnap = false;

            return 0;
        }

        public  int CloseStream()
        {
            if (null != m_objIGXFeatureControl && m_bIsSnap == false)
            {
                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                m_bIsSnap = true;
                m_objIGXFeatureControl = null;
                return 1;
            }
            else 
            {
                return 0;
            }
               
        }

        public void TriggerHardWare()
        {

        }

        public  void TriggerSoftware()
        {
            //发送软触发命令
            if (null != m_objIGXFeatureControl)
            {
                m_objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
            }
        }

        /// <summary>
        /// 计算宽度所占的字节数
        /// </summary>
        /// <param name="nWidth">图像宽度</param>
        /// <param name="bIsColor">是否是彩色相机</param>
        /// <returns>图像一行所占的字节数</returns>
        private int __GetStride(int nWidth, bool bIsColor)
        {
            return bIsColor ? nWidth * 1 : nWidth;
        }


        /// <summary>
        /// 用灰度数组新建一个8位灰度图像。
        /// </summary>
        /// <param name="rawValues"> 灰度数组(length = width * height)。 </param>
        /// <param name="width"> 图像宽度。 </param>
        /// <param name="height"> 图像高度。 </param>
        /// <returns> 新建的8位灰度位图。 </returns>
        private static Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
        {
            // 新建一个8位灰度位图，并锁定内存区域操作
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 计算图像参数
            int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数
            IntPtr ptr = bmpData.Scan0;                         // 获取首地址
            int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度
            byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存

            // 为图像数据赋值
            int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grayValues[posScan++] = rawValues[posSrc++];
                }
                // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                posScan += offset;
            }

            // 内存解锁
            Marshal.Copy(grayValues, 0, ptr, scanBytes);
            bitmap.UnlockBits(bmpData);  // 解锁内存区域

            // 修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette palette;
            // 获取一个Format8bppIndexed格式图像的Palette对象
            using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            // 修改生成位图的索引表
            bitmap.Palette = palette;

            return bitmap;
        }

        ////////////////////////////////////////////////////////////
        private void __OnFrameCallbackFun(object objUserParam, IFrameData objIFrameData)
        {
            IntPtr pBufferMono = IntPtr.Zero;
            pBufferMono = objIFrameData.GetBuffer();
            int stride = __GetStride((int)objIFrameData.GetWidth(), false);
            byte[] m_byMonoBuffer = null;
            m_byMonoBuffer = new byte[stride * (int)objIFrameData.GetHeight()];

            Marshal.Copy(pBufferMono, m_byMonoBuffer, 0, stride * (int)objIFrameData.GetHeight());

            GCHandle hObject = GCHandle.Alloc(m_byMonoBuffer, GCHandleType.Pinned);
            IntPtr pObject = hObject.AddrOfPinnedObject();

            Bitmap bmp = BuiltGrayBitmap(m_byMonoBuffer, (int)objIFrameData.GetWidth(), (int)objIFrameData.GetHeight());
            CallFunction(null, bmp);
        }


    }
}
