using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AqDevice;

namespace TestDaHengCamera
{
    public partial class Form1 : Form
    {
        AqDevice.IAqCameraManager cameramanager = null;
        List<AqDevice.IAqCamera> cameras;
        public Form1()
        {
            InitializeComponent();
        }

        private void opencamera_Click(object sender, EventArgs e)
        {
            string dllpath = System.IO.Directory.GetCurrentDirectory() + "\\DaHengCamera.dll";
            Assembly assem = Assembly.LoadFile(dllpath);
            Type type = assem.GetType("AqDevice.AqCameraFactory");
            MethodInfo mi = type.GetMethod("GetInstance");
            object obj = mi.Invoke(null, null);

            cameramanager = (IAqCameraManager)obj;
            cameramanager.Init();
            cameras = cameramanager.GetCameras();
            cameras[0].TriggerMode = AqDevice.TriggerModes.Unknow;
            cameras[0].ExposureTime = 100000;
            cameras[0].Name = "Aqrose1";
            cameras[0].RegisterCaptureCallback(new AqCaptureDelegate(RecCapture));
            cameras[0].OpenCamera();
            cameras[0].OpenStream();

            cameras[1].TriggerMode = AqDevice.TriggerModes.Unknow;
            cameras[1].ExposureTime = 500;
            cameras[1].Name = "Aqrose2";
            cameras[1].RegisterCaptureCallback(new AqCaptureDelegate(RecCapture1));
            cameras[1].OpenCamera();
            cameras[1].OpenStream();


        }

        private void openstream_Click(object sender, EventArgs e)
        {

        }

        private void closestream_Click(object sender, EventArgs e)
        {
            cameras[0].TriggerSoftware();
            cameras[1].TriggerSoftware();
        }

        private void closecamera_Click(object sender, EventArgs e)
        {
            cameras[0].CloseCamera();
            cameras[1].CloseCamera();
            Thread.Sleep(20);
            Application.Exit();
        }

        public void RecCapture(object objUserparam, Bitmap bitmap)
        {
            this.pictureBox1.Image = bitmap;
        }

        public void RecCapture1(object objUserparam, Bitmap bitmap)
        {
            this.pictureBox2.Image = bitmap;
        }
    }
}
