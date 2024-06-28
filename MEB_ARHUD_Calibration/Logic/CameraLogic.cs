using MEB_ARHUD_Calibration.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace MEB_ARHUD_Calibration.Logic {
    class CameraLogic {
        private static CameraLogic? instance = null;
        public static CameraLogic GetInstance() => instance ??= new CameraLogic();

        ConfigLogic cL = ConfigLogic.GetInstance();

        private List<ImagingControlSource> imagingControlSources;

        private CameraLogic() {
            imagingControlSources = cL.List_CameraConfigFileName.Select(x => {
                ImagingControlSource imagingControlSource = new(cL.List_CameraConfigFileName[0]);
                imagingControlSource.CameraNewFrameEvent += CameraNewFrame;
                imagingControlSource.CameraDeviceChangeEvent += CameraDeviceChanged;
                return imagingControlSource;
            }).ToList();
        }

        MessageLogic mL = MessageLogic.GetInstance();

        public event Action<Bitmap>? CameraNewFrameEvent;

        private event Action<bool>? DeviceChangeEvent;

        public event Action<bool> CameraDeviceChangeEvent {
            add { DeviceChangeEvent += value; }
            remove { }
        }



        private Bitmap currentBitmap = new Bitmap(1, 1);

        private object imageLocker = new object();
        private int imageLockFlag = 0;

        public void InitCamera() {
            for (int i = 0; i < imagingControlSources.Count; i++) {
                bool result = imagingControlSources[i].InitImagingControlDeviceState();

                Thread.Sleep(500);
                Console.WriteLine("Open Camera" + (i + 1) + " Success");
                mL.ShowLog("Open Camera" + (i + 1) + " Success", LogType.Camera);
            }
        }

        public void SwitchCameraWithCurrentProject() {
            switch (Config.CurrentProject) {
                case ProjectType.ID3:
                    SwitchCameraWithIndex(0);
                    break;
                case ProjectType.ID4X:
                    SwitchCameraWithIndex(1);
                    break;
                case ProjectType.ID6X:
                    SwitchCameraWithIndex(2);
                    break;
                case ProjectType.AUDI:
                    SwitchCameraWithIndex(2);
                    break;
                case ProjectType.ID3N:
                    SwitchCameraWithIndex(3);
                    break;
                case ProjectType.ID4XN:
                    SwitchCameraWithIndex(4);
                    break;
                case ProjectType.ID6XN:
                    SwitchCameraWithIndex(5);
                    break;
                case ProjectType.AUDIN:
                    SwitchCameraWithIndex(5);
                    break;
                default:
                    throw new Exception("Unknown ProjectType");
            }
        }

        public bool CameraConnectState(int index) {
            if (index < imagingControlSources.Count) {
                return imagingControlSources[index].Connected;
            }
            return false;
        }

        private void SwitchCameraWithIndex(int index) {
            for (int i = 0; i < imagingControlSources.Count; i++) {
                if (i != index) {
                    imagingControlSources[i].StopOutLivingImage();
                }
                else {
                    imagingControlSources[i].OpenOutLivingImage();
                }
            }
        }

        private void LockImage() {
            lock (imageLocker) {
                imageLockFlag++;
            }
        }

        private void UnlockImage() {
            lock (imageLocker) {
                imageLockFlag--;
            }
        }

        private bool GetImageLocked() {
            lock (imageLocker) {
                return imageLockFlag > 0;
            }
        }

        public Bitmap GetCurrentBitmap() {
            if (GetImageLocked()) {
                for (int i = 0; i < 100; i++) {
                    Thread.Sleep(1);
                    if (!GetImageLocked())
                        break;
                }
            }
            LockImage();

            try {
                return DeepCopyBitmap(currentBitmap);
            }
            catch (Exception e) {
                ExceptionUtil.SaveException(e);
            }
            finally {
                UnlockImage();
            }
            return null;
        }


        private void CameraNewFrame(Bitmap bitmap) {
            if (!GetImageLocked()) {
                LockImage();
                try {
                    currentBitmap.Dispose();
                    currentBitmap = DeepCopyBitmap(bitmap);
                    CameraNewFrameEvent?.Invoke(DeepCopyBitmap(bitmap));
                }
                catch (Exception e) {
                    ExceptionUtil.SaveException(e);
                }
                finally {
                    UnlockImage();
                }
            }
        }


        private void CameraDeviceChanged(bool connected) {
            if (DeviceChangeEvent != null)
                DeviceChangeEvent(connected);
        }

        private Bitmap DeepCopyBitmap(Bitmap bitmap) {
            Bitmap dstBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
            return dstBitmap;
        }

        //public void LiveStart()
        //{
        //    for (int i = 0; i < imagingControlSources.Count; i++)
        //    {
        //        ImagingControlSource imagingControlSource = imagingControlSources[i];

        //        try
        //        {
        //            imagingControlSource.LiveStart();
        //        }
        //        catch (Exception e)
        //        {
        //            ExceptionUtil.SaveException(e);
        //        }
        //        Thread.Sleep(500);
        //    }
        //}

        public void LiveStop() {
            for (int i = 0; i < imagingControlSources.Count; i++) {
                ImagingControlSource imagingControlSource = imagingControlSources[i];
                try {
                    imagingControlSource.LiveStop();
                }
                catch (Exception e) {
                    ExceptionUtil.SaveException(e);
                }
            }
        }

    }
}
