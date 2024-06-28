using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TIS.Imaging;

namespace MEB_ARHUD_Calibration.Common {
    class ImagingControlSource {
        private ICImagingControl icImagingControl = new ICImagingControl();
        private string deviceStateString = "";
        private bool isLiving = false;
        private bool connected = false;

        private bool outLivingImage = false;

        private static int ImageCheckFlagCount = 20;

        private int LivingImageFlag = 10;

        public bool Connected { get { return LivingImageFlag > 15; } }

        private event Action<Bitmap>? NewFrameEvent;
        private event Action<bool>? DeviceChangeEvent;

        public event Action<Bitmap> CameraNewFrameEvent {
            add { NewFrameEvent += value; }
            remove { }
        }

        public event Action<bool> CameraDeviceChangeEvent {
            add { DeviceChangeEvent += value; }
            remove { }
        }

        public ImagingControlSource(string file) {
            deviceStateString = File.ReadAllText(file);
            InitImagingControl();

            Thread t_CheckConnect = new Thread(CheckLivingImageThread);
            t_CheckConnect.IsBackground = true;
            t_CheckConnect.Start();
        }

        public void InitImagingControl() {
            icImagingControl.DeviceListChangedExecutionMode = EventExecutionMode.Invoke;
            icImagingControl.DeviceLostExecutionMode = EventExecutionMode.AsyncInvoke;
            icImagingControl.ImageAvailableExecutionMode = EventExecutionMode.MultiThreaded;
            icImagingControl.LiveDisplay = true;
            icImagingControl.ImageAvailable += IcImagingControlImageAvailable;
        }

        private void CheckLivingImageThread() {
            while (true) {
                Thread.Sleep(1000);
                LivingImageFlag--;
                if (LivingImageFlag <= 0) {
                    LivingImageFlag = 10;
                    Task.Factory.StartNew(() => {
                        try {
                            InitImagingControlDeviceState();
                            LiveStart();
                        }
                        catch (Exception e) {
                            ExceptionUtil.SaveException(e);
                        }
                    });

                }
            }
        }


        public bool InitImagingControlDeviceState() {
            icImagingControl.LiveStop();
            icImagingControl.DeviceState = deviceStateString;
            connected = true;
            return true;
        }


        public void LiveStart() {
            if (isLiving)
                return;

            try {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                icImagingControl.LiveStart();
                isLiving = true;
            }
            catch (Exception e) {
                ExceptionUtil.SaveException(e);
            }
        }



        public void LiveStop() {
            if (!isLiving)
                return;
            try {
                icImagingControl.LiveStop();
                isLiving = false;
            }
            catch (Exception e) {
                ExceptionUtil.SaveException(e);
            }
        }

        public void OpenOutLivingImage() {
            outLivingImage = true;
        }

        public void StopOutLivingImage() {
            outLivingImage = false;
        }


        private void IcImagingControlImageAvailable(object sender, ICImagingControl.ImageAvailableEventArgs e) {
            LivingImageFlag = ImageCheckFlagCount;

            if (!outLivingImage)
                return;

            try {
                TIS.Imaging.ImageBuffer CurrentBuffer = null;
                CurrentBuffer = icImagingControl.ImageBuffers[e.bufferIndex];

                using (Bitmap currentBitmap = CurrentBuffer.Bitmap) {
                    BitmapData bmpData = currentBitmap.LockBits(new Rectangle(0, 0, currentBitmap.Width, currentBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, currentBitmap.PixelFormat);
                    Bitmap bmp = new Bitmap(currentBitmap.Width, currentBitmap.Height, bmpData.Stride, currentBitmap.PixelFormat, bmpData.Scan0);
                    currentBitmap.UnlockBits(bmpData);

                    using (Bitmap cloneImage = DeepCopyBitmap(currentBitmap)) {
                        if (NewFrameEvent != null)
                            NewFrameEvent(cloneImage);
                    }
                }
            }
            catch (Exception ex) {
                ExceptionUtil.SaveException(ex);
            }
        }

        public Bitmap DeepCopyBitmap(Bitmap bitmap) {
            try {
                Bitmap dstBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
                return dstBitmap;
            }
            catch (Exception ex) {
                ExceptionUtil.SaveException(ex);
                return null;
            }
        }
    }
}
