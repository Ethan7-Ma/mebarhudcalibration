using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Logic;
using MEB_ARHUD_Calibration.Data;

namespace MEB_ARHUD_Calibration
{
    public partial class Form_Test : Form
    {
        public Form_Test()
        {
            InitializeComponent();
            plcL.ReceiveEvent += ReceiveEvent;
            fL.GetFISCarsSuccessEvent += GetFISCarsSuccessEvent;

        }

        #region FIS

        FISLogic fL = FISLogic.GetInstance();

        private void GetFISCarsSuccessEvent()
        {
            List<CarInfo_OnLineState> cars = fL.Cars;
            this.Invoke((EventHandler)delegate
            {
                DataGridView_FIS.Rows.Clear();

                foreach (CarInfo_OnLineState info in cars)
                {
                    DataGridView_FIS.Rows.Add(new string[] { info.SequenceNumber + "", info.PIN, info.VIN, info.MODELL, info.HUD, info.Time });
                }
            });
        }

        #endregion

        #region PLC

        PLCLogic plcL = PLCLogic.GetInstance();
        EquipmentLogic eL = EquipmentLogic.GetInstance();
        TestLogic tL = TestLogic.GetInstance();

        private void button1_Click_1(object sender, EventArgs e)
        {
            //PLC连接
            plcL.ConnectPLC();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //PLC断开
            plcL.DisConnectPLC();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //PLC急停
            plcL.SendStop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //车型FIS
            plcL.SendCarType_FIS((int)numericUpDown1.Value);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //车型RFID
            plcL.SendCarType_RFID((int)numericUpDown1.Value);
        }

        private void CheckBox_PLC_1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_PLC_1.Checked)
                plcL.SendEquipmentFirstAction();
            else
                plcL.SendClearEquipmentFirstAction();
        }

        private void CheckBox_PLC_2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_PLC_2.Checked)
                plcL.SendEquipmentSecondAction();
            else
                plcL.SendClearEquipmentSecondAction();
        }

        private void CheckBox_PLC_3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_PLC_3.Checked)
                plcL.SendEquipmentThirdAction();
            else
                plcL.SendClearEquipmentThirdAction();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //结果OK
            plcL.SendTestResult(true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //结果NG
            plcL.SendTestResult(false);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            plcL.SendClearTestResult();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            plcL.SendChangeEquipmentPSet3();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            plcL.SendClearEquipmentPSet3();
        }

        #endregion




        private void button11_Click(object sender, EventArgs e)
        {
            //拧紧枪连接
            eL.Connect();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //发送开始
            eL.SendStart();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //发送结束
            eL.SendStop();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //选择程序1
            eL.SendPSet(1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //选择程序2
            eL.SendPSet(2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            eL.SendPSet(3);
        }

        private void Button_SendAngle_L_Click(object sender, EventArgs e)
        {
            int Angle = (int)NumericUpDown_SendAngle_L.Value;
            int Speed = (int)NumericUpDown_SendSpeed_L.Value;
            eL.SendAngleSpeed_L(Angle, Speed);
        }

        private void Button_SendAngle_R_Click(object sender, EventArgs e)
        {
            int Angle = (int)NumericUpDown_SendAngle_R.Value;
            int Speed = (int)NumericUpDown_SendSpeed_R.Value;
            eL.SendAngleSpeed_R(Angle, Speed);
        }

        private void Button_SendBackAngle_L_Click(object sender, EventArgs e)
        {
            eL.SendAngleBack_L();
        }

        private void Button_SendBackAngle_R_Click(object sender, EventArgs e)
        {
            eL.SendAngleBack_R();
        }

        CameraLogic camL = CameraLogic.GetInstance();

        private void button17_Click(object sender, EventArgs e)
        {
            //开始相机连续读取
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //关闭相机连续读取
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //切换相机1
            camL.ChangeOutCameraDevice(ProjectType.ID3);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //切换相机2

            Task.Factory.StartNew(() => {
                camL.ChangeOutCameraDevice(ProjectType.ID4X);
            });

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //切换相机3
            camL.ChangeOutCameraDevice(ProjectType.ID6X);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Timer_CameraAuto.Enabled = !Timer_CameraAuto.Enabled;
        }

        int cameraFlag = 0;

        private void Timer_CameraAuto_Tick(object sender, EventArgs e)
        {
            cameraFlag++;
            Console.WriteLine("Check Camera Flag");
            if (cameraFlag % 20 == 0)
            {
                Console.WriteLine("Check Camera Enter");
                ChangeCameraDevice(cameraFlag / 10);
            }
            else if(cameraFlag % 20 == 10)
            {
                Console.WriteLine("Check Save Enter");
                SaveTestImage();
            }
        }

        private void SaveTestImage()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    string fileName = @"ImageLog\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg";
                    bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    ExceptionUtil.SaveException(ex);
                }
            });
        }

        private void ChangeCameraDevice(int flag)
        {
            Task.Factory.StartNew(() =>
            {
                mL.ShowLog("Begin Change CameraDevice", LogType.Camera);


                try
                {
                    if (flag % 3 == 0)
                    {
                        camL.ChangeOutCameraDevice(ProjectType.ID3);
                    }
                    else if (flag % 3 == 1)
                    {
                        camL.ChangeOutCameraDevice(ProjectType.ID4X);
                    }
                    else if (flag % 3 == 2)
                    {
                        camL.ChangeOutCameraDevice(ProjectType.ID6X);
                    }
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }


            });
        }

        private void SaveCameraImage()
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            AutoTestAllImage();
        }

        /// <summary>
        /// 选择文件夹，然后自动测试所有
        /// </summary>
        private void AutoTestAllImage()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                string path = fbd.SelectedPath;

                Task.Factory.StartNew(() => {
                    try
                    {

                        DirectoryInfo root = new DirectoryInfo(path);
                        int index = 0;
                        int count = root.GetFiles().Length;
                        foreach (FileInfo file in root.GetFiles())
                        {
                            index++;
                            string fileName = file.FullName;
                            Console.WriteLine(index + " " + count);
                            if (fileName.EndsWith(".jpg"))
                                iaL.TestGetCircle(fileName);
                        }
                    }
                    catch { }
                
                });
                
            }
        }

        MessageLogic mL = MessageLogic.GetInstance();

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mL.ShowPLCLog = checkBox1.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mL.ShowCameraLog = checkBox2.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mL.ShowImageAnalyseLog = checkBox3.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mL.ShowEquipmentLog = checkBox4.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mL.ShowFISLog = checkBox5.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                plcL.ShowReadWriteLogs = checkBox6.Checked;
            }
            catch { }
        }

        ImageAnalyseLogic iaL = ImageAnalyseLogic.GetInstance();
        ExportLogic expL = ExportLogic.GetInstance();
        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = camL.GetCurrentBitmap();
                string fileName = @"ImageLog\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg";
                bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                List<PointF> points = iaL.TestGetCircleForCalibration(fileName);
                
                //List<PointF> points = iaL.TestGetCircleForCalibration(@"G:\Windows7 Work Datas\Test\MEB\1.jpg");
                //expL.SaveCameraCalibrationToCSV(points);
                //MessageBox.Show("测试完成");
                //iaL.TestGetCircle(@"G:\Windows7 Work Datas\Test\ID6X\ID_6_20211018_10_0.02.jpg");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                tL.TestAnalyseImageCenterWithCalcAngle();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "所有图片|*.bmp;*.jpg;*.png|BMP|*.bmp|JPG|*.jpg|PNG|*.png";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = ofd.FileName;
                    tL.TestAnalyseImageCenterWithCalcAngle_ForTest(fileName);
                }
            }
            catch (Exception ex)
            {

            }
        }

        

        private void Button_LogState_Click(object sender, EventArgs e)
        {
            mL.PrintStates();
        }

        private void ReceiveEvent(byte[] datas)
        {
            if(datas.Length > 22)
            {
                Invoke((EventHandler)delegate
                {
                    try
                    {
                        SetStateLabel(Label_PLC_State_0, datas[1]);
                        SetStateLabel(Label_PLC_State_1, datas[3]);
                        SetStateLabel(Label_PLC_State_2, datas[5]);
                        SetStateLabel(Label_PLC_State_3, datas[7]);
                        SetStateLabel(Label_PLC_State_4, datas[9]);
                        SetStateLabel(Label_PLC_State_5, datas[11]);
                        SetStateLabel(Label_PLC_State_6, datas[13]);
                        SetStateLabel(Label_PLC_State_7, datas[15]);
                        SetStateLabel(Label_PLC_State_8, datas[17]);
                        SetStateLabel(Label_PLC_State_9, datas[19]);

                        //int time = datas[20] << 8 + datas[21];
                        Label_PLC_State_10.Text = ((datas[20] << 8) + datas[21]) * 0.1 + "";
                    }
                    catch (Exception e)
                    {
                        ExceptionUtil.SaveException(e);
                    }
                });
            }
        }

        private void SetStateLabel(Label label, ushort state)
        {
            if (state == 0)
                label.ForeColor = Color.Gray;
            if (state == 1)
                label.ForeColor = Color.Green;
        }

        private void Button_SetCurrentRFID_Click(object sender, EventArgs e)
        {
            CarInfo_OnLineState car = fL.SetCurrentCarByRFID(TextBox_RFID.Text);
            Label_RFInfo.Text = car.SequenceNumber + " " + car.HUD + " " + car.VIN;
        }

        private void Button_SetNext_Click(object sender, EventArgs e)
        {
            if(fL.CurrentRFIDCar != null)
            {
                fL.SetNextNeedTestCar();
                CarInfo_OnLineState car = fL.NextNeedTestCar;
                Label_NextInfo.Text = car.SequenceNumber + " " + car.HUD + " " + car.VIN;
            }
        }

        private void Form_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                plcL.ReceiveEvent -= ReceiveEvent;
                fL.GetFISCarsSuccessEvent -= GetFISCarsSuccessEvent;
            }
            catch { }
        }
    }
}
