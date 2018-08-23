using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AqDevice
{
    public abstract class AqCameraBase : IAqCamera
    {
        protected string id;
        protected string name;
        protected string ip;
        protected string mac;

        protected TriggerSources triggerSource;
        protected TriggerSwitchs triggerSwitch;
        protected TriggerModes triggerMode;
        protected TriggerEdges triggerEdge;

        protected double exposureTime;
        protected double acquisitionFrequency;
        protected double triggerDelay;

        protected double gain;
        protected bool gainAuto;

        protected event AqCaptureDelegate eventCapture;

        public virtual string Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public virtual string Mac
        {
            get { return mac; }
            set { mac = value; }
        }

        public virtual TriggerSources TriggerSource
        {
            get { return triggerSource; }
            set { triggerSource = value; }
        }

        public virtual TriggerSwitchs TriggerSwitch
        {
            get { return triggerSwitch; }
            set { triggerSwitch = value; }
        }

        public virtual TriggerModes TriggerMode
        {
            get { return triggerMode; }
            set { triggerMode = value; }
        }

        public virtual TriggerEdges TriggerEdge
        {
            get { return triggerEdge; }
            set { triggerEdge = value; }
        }

        public virtual double ExposureTime
        {
            get { return exposureTime; }
            set { exposureTime = value; }
        }

        public virtual double AcquisitionFrequency
        {
            get { return acquisitionFrequency; }
            set { acquisitionFrequency = value; }
        }

        public virtual double TriggerDelay
        {
            get { return triggerDelay; }
            set { triggerDelay = value; }
        }

        public virtual double Gain
        {
            get { return gain; }
            set { gain = value; }
        }

        public virtual bool GainAuto
        {
            get { return gainAuto; }
            set { gainAuto = value; }
        }

        public virtual void RegisterCaptureCallback(AqCaptureDelegate delCaptureFun)
        {
            eventCapture += delCaptureFun;
        }

        //========================================================================================================

        public virtual int OpenCamera()
        {
            throw new NotImplementedException();
        }

        public virtual int CloseCamera()
        {
            throw new NotImplementedException();
        }

        public virtual int OpenStream()
        {
            throw new NotImplementedException();
        }

        public virtual int CloseStream()
        {
            throw new NotImplementedException();
        }

        public virtual void TriggerSoftware()
        {
            //eventCapture(null, null);
        }

        public void CallFunction(object obj, Bitmap bmp)
        {
            eventCapture(obj, bmp);
        }

    }
}
