using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AqDevice;
using Basler.Pylon;

namespace BalserCamera
{
    class BalserCameraManager :AqDevice.IAqCameraManager
    {
        List<AqDevice.IAqCamera> cameras = new List<AqDevice.IAqCamera>();
       
        public bool Init()
        {
            List<ICameraInfo> allCameras =  CameraFinder.Enumerate();

            foreach (ICameraInfo camerainfo in allCameras)
            {
                AqBaslerCamera camera = new AqBaslerCamera();
                camera.Name = camerainfo[CameraInfoKey.UserDefinedName];
				//camera.Id = camerainfo[CameraInfoKey.DeviceID];//ID无法获取,加入此代码则异常
				camera.Mac = camerainfo[CameraInfoKey.DeviceMacAddress];
                camera.Ip = camerainfo[CameraInfoKey.DeviceIpAddress];
                cameras.Add(camera);            
            }

            if (cameras.Count == 0)
                return false;

            AqBaslerCamera.allbaslercamera = cameras;

            return true;
        }


        public void Uninit()
        {
            
        }

        public List<AqDevice.IAqCamera> GetCameras()
        {
            return cameras;
        }



    }
}
