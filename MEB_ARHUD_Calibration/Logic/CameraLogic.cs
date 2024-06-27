using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using MEB_ARHUD_Calibration.Common;

namespace MEB_ARHUD_Calibration.Logic
{
    class CameraLogic
    {
        private static CameraLogic instance = null;

        public static CameraLogic GetInstance()
        {
            if (instance == null)
                instance = new CameraLogic();
            return instance;
        }

        ConfigLogic cL = ConfigLogic.GetInstance();

        private CameraLogic()
        {
            try
            {
                imagingControlSource1 = new ImagingControlSource(cL.List_CameraSerial[0], cL.List_CameraConfigFileName[0]);
                imagingControlSource1.CameraNewFrameEvent += new CameraNewFrameDelegateFunction(CameraNewFrame1);
                imagingControlSource1.CameraDeviceChangeEvent += new DeviceStateChangedDelegateFunction(CameraDeviceChanged);
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }

            try
            {
                imagingControlSource2 = new ImagingControlSource(cL.List_CameraSerial[1], cL.List_CameraConfigFileName[1]);
                imagingControlSource2.CameraNewFrameEvent += new CameraNewFrameDelegateFunction(CameraNewFrame2);
                imagingControlSource2.CameraDeviceChangeEvent += new DeviceStateChangedDelegateFunction(CameraDeviceChanged);
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }

            try
            {
                imagingControlSource3 = new ImagingControlSource(cL.List_CameraSerial[2], cL.List_CameraConfigFileName[2]);
                imagingControlSource3.CameraNewFrameEvent += new CameraNewFrameDelegateFunction(CameraNewFrame3);
                imagingControlSource3.CameraDeviceChangeEvent += new DeviceStateChangedDelegateFunction(CameraDeviceChanged);
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }

        MessageLogic mL = MessageLogic.GetInstance();

        public SynchronizationContext MainSyncContext = null;

        public event CameraNewFrameDelegateFunction CameraNewFrameEvent;

        private event DeviceStateChangedDelegateFunction DeviceChangeEvent = null;

        public event DeviceStateChangedDelegateFunction CameraDeviceChangeEvent
        {
            add { DeviceChangeEvent += value; }
            remove { }
        }

        private ImagingControlSource imagingControlSource1 = null;
        private ImagingControlSource imagingControlSource2 = null;
        private ImagingControlSource imagingControlSource3 = null;

        private List<ImagingControlSource> imagingControlSources = new List<ImagingControlSource>();

        private Bitmap currentBitmap = new Bitmap(1, 1);

        private object imageLocker = new object();
        private int imageLockFlag = 0;

        public void InitCamera()
        {
            imagingControlSources.Add(imagingControlSource1);
            imagingControlSources.Add(imagingControlSource2);
            imagingControlSources.Add(imagingControlSource3);
            try
            {
                for (int i = 0; i < imagingControlSources.Count; i++)
                {
                    ImagingControlSource imagingControlSource = imagingControlSources[i];
                    bool rlt = imagingControlSource.InitImagingControlDeviceState();

                    Thread.Sleep(500);
                    
                    mL.ShowLog("Open Camera" + (i + 1) + " Success", LogType.Camera);
                }
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }

        public void ChangeOutCameraDevice(ProjectType type)
        {
            switch (type)
            {
                case ProjectType.ID3:
                    ChangeOutLivingCameraDevice(0);
                    break;
                case ProjectType.ID4X:
                    ChangeOutLivingCameraDevice(1);
                    break;
                case ProjectType.ID6X:
                    ChangeOutLivingCameraDevice(2);
                    break;
                case ProjectType.AUDI:
                    ChangeOutLivingCameraDevice(2);
                    break;
                default:
                    break;
            }
        }

        public bool CameraConnectState(int index)
        {
            if(index < imagingControlSources.Count)
            {
                return imagingControlSources[index].Connected;
            }
            return false;
        }

        private void ChangeOutLivingCameraDevice(int index)
        {
            for (int i = 0; i < imagingControlSources.Count; i++)
            {
                ImagingControlSource imagingControlSource = imagingControlSources[i];
                if (i != index)
                {
                    imagingControlSource.StopOutLivingImage();
                }
            }
            for (int i = 0; i < imagingControlSources.Count; i++)
            {
                ImagingControlSource imagingControlSource = imagingControlSources[i];
                if (i == index)
                {
                    imagingControlSource.OpenOutLivingImage();
                }
            }
        }

        private void LockImage()
        {
            lock (imageLocker)
            {
                imageLockFlag++;
            }
        }

        private void UnlockImage()
        {
            lock (imageLocker)
            {
                imageLockFlag--;
            }
        }

        private bool GetImageLocked()
        {
            lock (imageLocker)
            {
                return imageLockFlag > 0;
            }
        }

        public Bitmap GetCurrentBitmap()
        {
            if (GetImageLocked())
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(1);
                    if (!GetImageLocked())
                        break;
                }
            }
            LockImage();

            try
            {
                return DeepCopyBitmap(currentBitmap);
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
            finally
            {
                UnlockImage();
            }
            return null;
        }


        private void CameraNewFrame1(Bitmap bitmap)
        {
            if (!GetImageLocked())
            {
                LockImage();
                try
                {
                    currentBitmap.Dispose();
                    currentBitmap = DeepCopyBitmap(bitmap);
                    if (CameraNewFrameEvent != null)
                        CameraNewFrameEvent(DeepCopyBitmap(bitmap));
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
                finally
                {
                    UnlockImage();
                }
            }
        }

        private void CameraNewFrame2(Bitmap bitmap)
        {
            if (!GetImageLocked())
            {
                LockImage();
                try
                {
                    currentBitmap.Dispose();
                    currentBitmap = DeepCopyBitmap(bitmap);
                    if (CameraNewFrameEvent != null)
                        CameraNewFrameEvent(DeepCopyBitmap(bitmap));
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
                finally
                {
                    UnlockImage();
                }
            }
        }

        private void CameraNewFrame3(Bitmap bitmap)
        {
            if (!GetImageLocked())
            {
                LockImage();
                try
                {
                    currentBitmap.Dispose();
                    currentBitmap = DeepCopyBitmap(bitmap);
                    if (CameraNewFrameEvent != null)
                        CameraNewFrameEvent(DeepCopyBitmap(bitmap));
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
                finally
                {
                    UnlockImage();
                }
            }
        }

        private void CameraDeviceChanged(bool connected)
        {
            if (DeviceChangeEvent != null)
                DeviceChangeEvent(connected);
        }

        private Bitmap DeepCopyBitmap(Bitmap bitmap)
        {
            Bitmap dstBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
            return dstBitmap;
        }

        public void LiveStart()
        {
            for (int i = 0; i < imagingControlSources.Count; i++)
            {
                ImagingControlSource imagingControlSource = imagingControlSources[i];

                try
                {
                    imagingControlSource.LiveStart();
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
                Thread.Sleep(500);
            }
        }

        public void LiveStop()
        {
            for (int i = 0; i < imagingControlSources.Count; i++)
            {
                ImagingControlSource imagingControlSource = imagingControlSources[i];
                try
                {
                    imagingControlSource.LiveStop();
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
            }
        }

    }
}
