using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using TIS;
using TIS.Imaging;

namespace MEB_ARHUD_Calibration.Common
{
    class ImagingControlSource
    {
        private ICImagingControl icImagingControl = new ICImagingControl();
        private string deviceStateString = "";
        private bool isLiving = false;
        private bool connected = false;
        private string serialNumber = "";

        private bool outLivingImage = false;

        private static int ImageCheckFlagCount = 20;

        private int LivingImageFlag = 10;

        public bool OutLivingImage => outLivingImage;

        public bool Living { get { return isLiving; } }
        public bool Connected { get { return LivingImageFlag > 15; } }

        private event CameraNewFrameDelegateFunction NewFrameEvent = null;
        private event DeviceStateChangedDelegateFunction DeviceChangeEvent = null;

        public event CameraNewFrameDelegateFunction CameraNewFrameEvent
        {
            add { NewFrameEvent += value; }
            remove { }
        }

        public event DeviceStateChangedDelegateFunction CameraDeviceChangeEvent
        {
            add { DeviceChangeEvent += value; }
            remove { }
        }

        public ImagingControlSource(string serial, string file)
        {
            serialNumber = serial;
            deviceStateString = TextUtil.LoadStringFromFile(file);
            InitImagingControl();

            Thread t_CheckConnect = new Thread(CheckLivingImageThread);
            t_CheckConnect.IsBackground = true;
            t_CheckConnect.Start();
        }

        public void InitImagingControl()
        {
            try
            {
                icImagingControl.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke;
                icImagingControl.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
                icImagingControl.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
                icImagingControl.LiveDisplay = true;
                icImagingControl.LiveCaptureContinuous = true;

                icImagingControl.ImageAvailable += new EventHandler<ICImagingControl.ImageAvailableEventArgs>(IcImagingControlImageAvailable);
                icImagingControl.DeviceLost += new EventHandler<ICImagingControl.DeviceLostEventArgs>(IcImagingControlDeviceLost);
                icImagingControl.DeviceListChanged += new EventHandler(IcImagingControlDeviceListChanged);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CheckLivingImageThread()
        {
            while(true)
            {
                Thread.Sleep(1000);
                LivingImageFlag--;
                if(LivingImageFlag <= 0)
                {
                    LivingImageFlag = 10;
                    Task.Factory.StartNew(() => {
                        try
                        {
                            InitImagingControlDeviceState();
                            LiveStart();
                        }
                        catch (Exception e)
                        {
                            ExceptionUtil.SaveException(e);
                        }
                    });
                    
                }
            }
        }

        public void ChangeImagingControlDeviceState(string serial, string file)
        {
            try
            {
                Console.WriteLine("Enter Change Imaging Control Device");
                if (Living)
                    LiveStop();
                Console.WriteLine("End Live Stop ");

                serialNumber = serial;
                deviceStateString = TextUtil.LoadStringFromFile(file);
                Console.WriteLine("Before Set Device State");
                icImagingControl.DeviceState = deviceStateString;
                Console.WriteLine("End Set Device State");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public bool InitImagingControlDeviceState()
        {
            try
            {
                try
                {
                    icImagingControl.LiveStop();
                }
                catch { }
                icImagingControl.DeviceState = deviceStateString;
                connected = true;
                return true;
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
            return false;
        }



        public bool GetImagingControlState()
        {
            if (CanGetImagingControlDeviceVersion())
                return true;
            else
            {
                InitImagingControl();
                if (CanGetImagingControlDeviceVersion())
                    return true;
            }
            return false;
        }

        private bool CanGetImagingControlDeviceVersion()
        {
            try
            {
                ulong version = icImagingControl.DeviceCurrent.DeviceVersion;
                return true;
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
                return false;
            }
        }

        public DialogResult ShowDeviceSettingsDialog()
        {
            try
            {
                return icImagingControl.ShowDeviceSettingsDialog();
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
                return DialogResult.Cancel;
            }
        }

        public void ShowPropertyDialog()
        {
            try
            {
                icImagingControl.ShowPropertyDialog();
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }

        public void LiveStart()
        {
            if (isLiving)
                return;

            try
            {
                icImagingControl.LiveStart();
                isLiving = true;
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }

        

        public void LiveStop()
        {
            if (!isLiving)
                return;
            try
            {
                icImagingControl.LiveStop();
                isLiving = false;
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }

        public void OpenOutLivingImage()
        {
            outLivingImage = true;
        }

        public void StopOutLivingImage()
        {
            outLivingImage = false;
        }

        public Bitmap GetBitmap()
        {
            if (!isLiving)
                return null;
            Bitmap bitmap = icImagingControl.ImageActiveBuffer.Bitmap;
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
            Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height, bmpData.Stride, bitmap.PixelFormat, bmpData.Scan0);
            bitmap.UnlockBits(bmpData);

            return bmp;
        }

        private void IcImagingControlImageAvailable(object sender, ICImagingControl.ImageAvailableEventArgs e)
        {
            LivingImageFlag = ImageCheckFlagCount;

            if (!outLivingImage)
                return;

            try
            {
                TIS.Imaging.ImageBuffer CurrentBuffer = null;
                CurrentBuffer = icImagingControl.ImageBuffers[e.bufferIndex];

                using (Bitmap currentBitmap = CurrentBuffer.Bitmap)
                {
                    BitmapData bmpData = currentBitmap.LockBits(new Rectangle(0, 0, currentBitmap.Width, currentBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, currentBitmap.PixelFormat);
                    Bitmap bmp = new Bitmap(currentBitmap.Width, currentBitmap.Height, bmpData.Stride, currentBitmap.PixelFormat, bmpData.Scan0);
                    currentBitmap.UnlockBits(bmpData);

                    using (Bitmap cloneImage = DeepCopyBitmap(currentBitmap))
                    {
                        if (NewFrameEvent != null)
                            NewFrameEvent(cloneImage);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionUtil.SaveException(ex);
            }
        }

        private void IcImagingControlDeviceLost(object sender, ICImagingControl.DeviceLostEventArgs e)
        {
            
        }

        private void IcImagingControlDeviceListChanged(object sender, EventArgs e)
        {
            
        }

        public Bitmap DeepCopyBitmap(Bitmap bitmap)
        {
            try
            {
                Bitmap dstBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
                return dstBitmap;
            }
            catch (Exception ex)
            {
                ExceptionUtil.SaveException(ex);
                return null;
            }
        }

        public void DisposeICControl()
        {
            try
            {
                icImagingControl.Dispose();
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }

        public void SaveConfigs(string fileName)
        {
            try
            {
                icImagingControl.SaveDeviceStateToFile(fileName);
            }
            catch (Exception e)
            {
                ExceptionUtil.SaveException(e);
            }
        }
    }
}
