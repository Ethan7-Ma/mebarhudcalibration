using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Logic;

namespace MEB_ARHUD_Calibration
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();

            MainSyncContext = SynchronizationContext.Current;

            InitForStart();
            InitUIInfo();
            this.Text = "ARHUD " + Config.ProjectType;

            //var a = ImageAnalyseLogic.GetInstance();
            //a.TestGetCircle(@"E:\ARHUD_LOGS\AUDI\ImageLog\2024\6\27\2024_06_27_13_28_57_Y78202472900579AM1.jpg");
        }

        private bool ShowCameraOffset = true;


        SynchronizationContext MainSyncContext = null;
        CameraLogic camL = CameraLogic.GetInstance();
        ConfigLogic cfgL = ConfigLogic.GetInstance();
        TestLogic tL = TestLogic.GetInstance();
        FISLogic fL = FISLogic.GetInstance();
        MessageLogic mL = MessageLogic.GetInstance();
        PLCLogic plcL = PLCLogic.GetInstance();
        EquipmentLogic eL = EquipmentLogic.GetInstance();
        ImageAnalyseLogic iaL = ImageAnalyseLogic.GetInstance();
        UserLogic uL = UserLogic.GetInstance();

        int heart = 0;
        int lastHeart = -1;

        private void Form_Main_Shown(object sender, EventArgs e)
        {
            switch (Config.ProjectType)
            {
                case ProjectType.ID3:
                    ChangeID3();
                    break;
                case ProjectType.ID4X:
                    ChangeID4();
                    break;
                case ProjectType.ID6X:
                    ChangeID6X();
                    break;
                case ProjectType.AUDI:
                    ChangeAUDI();
                    break;
                default:
                    break;
            }

            if (Config.IsDevelopmentEnvironment)
                return;

            Task.Factory.StartNew(() => {
                Thread.Sleep(500);
                camL.InitCamera();
                camL.LiveStart();
                camL.ChangeOutCameraDevice(Config.ProjectType);
            });

            Task.Factory.StartNew(() => {
                Thread.Sleep(500);
                plcL.ConnectPLC();
            });

            Task.Factory.StartNew(() => {
                Thread.Sleep(500);
                eL.Connect();
                Thread.Sleep(200);
                eL.SendStart();
            });
        }

        private void InitUIInfo()
        {
            iD3ToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID3;
            iD4XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID4X;
            iD6XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID6X;
            aUDIToolStripMenuItem_NeedTest.Checked = Config.NeedTest_AUDI;
        }

        private void InitForStart()
        {
            camL.CameraNewFrameEvent += new CameraNewFrameDelegateFunction(CameraNewFrame);
            
            tL.AnalyseImageCenterTestFinished += AnalyseImageCenterTestFinished;
            tL.AnalyseImageCenterTestStart += AnalyseImageCenterTestStart;
            tL.AnalyseImageCenterTestResult += AnalyseImageCenterTestResult;
            tL.CarTestFinish += CarTestFinish;

            plcL.RobotInCarEvent += RobotInCarEvent;

            plcL.ImageTestEvent += ImageTestEvent;
            plcL.HeartChangeEvent += HeartChangeEvent;

            mL.ShowStateMessageEvent += ShowStateMessageEvent;
        }

        private void ShowStateMessageEvent(string msg)
        {
            this.Invoke((EventHandler)delegate {
                try
                {
                    ToolStripStatusLabel_TestState.Text = msg;
                } catch { }
            });
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void iD3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeID3();
        }

        private void ChangeID3()
        {
            Config.ProjectType = ProjectType.ID3;
            camL.ChangeOutCameraDevice(Config.ProjectType);
            this.Text = "ARHUD " + Config.ProjectType;
            //Label_CarType.Text = Config.ProjectType + "";
            Label_ProjectType.Text = Config.ProjectType + "";
        }

        private void iD4XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeID4();
        }

        private void ChangeID4()
        {
            Config.ProjectType = ProjectType.ID4X;
            camL.ChangeOutCameraDevice(Config.ProjectType);
            this.Text = "ARHUD " + Config.ProjectType;
            //Label_CarType.Text = Config.ProjectType + "";
            Label_ProjectType.Text = Config.ProjectType + "";
        }

        private void iD6XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeID6X();
        }

        private void ChangeID6X()
        {
            Config.ProjectType = ProjectType.ID6X;
            camL.ChangeOutCameraDevice(Config.ProjectType);
            this.Text = "ARHUD " + Config.ProjectType;
            //Label_CarType.Text = Config.ProjectType + "";
            Label_ProjectType.Text = Config.ProjectType + "";
        }

        private void aUDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAUDI();
        }

        private void ChangeAUDI()
        {
            Config.ProjectType = ProjectType.AUDI;
            camL.ChangeOutCameraDevice(Config.ProjectType);
            this.Text = "ARHUD " + Config.ProjectType;
            //Label_CarType.Text = Config.ProjectType + "";
            Label_ProjectType.Text = Config.ProjectType + "";
        }

        private void CarTestFinish()
        {
            Invoke((EventHandler)delegate
            {
                try
                {
                    int carType = fL.GetCarType(fL.NextNeedTestCar);

                    ProjectType nextCarType = (ProjectType)carType;

                    mL.ShowLog("测试完成，切换 " + carType, LogType.FIS);
                    Label_NextCar.Text = "Next Car : " + nextCarType + " " + fL.NextNeedTestCar.VIN + " " + fL.NextNeedTestCar.SequenceNumber;

                    cfgL.SaveNextCarInfo(nextCarType, fL.NextNeedTestCar.VIN);
                    plcL.CanChangeCar = true;
                    switch (nextCarType)
                    {
                        case ProjectType.ID3:
                            ChangeID3();
                            break;
                        case ProjectType.ID4X:
                            ChangeID4();
                            break;
                        case ProjectType.ID6X:
                            ChangeID6X();
                            break;
                        case ProjectType.AUDI:
                            ChangeAUDI();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
            });
        }

        private void AnalyseImageCenterTestFinished()
        {
            Invoke((EventHandler)delegate
            {
                try
                {
                    Label_EquipmentA.Text = tL.TestResult.Angle_L + "";
                    Label_EquipmentB.Text = tL.TestResult.Angle_R + "";
                    Label_Rotation.Text = $"{iaL.RotationResult:0.00}";
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
            });
        }

        private void AnalyseImageCenterTestResult(bool rlt)
        {
            Invoke((EventHandler)delegate
            {
                try
                {
                    if (rlt)
                    {
                        Label_Result.Text = "OK";
                        Label_Result.ForeColor = Color.Green;
                    }
                    else
                    {
                        Label_Result.Text = "NG";
                        Label_Result.ForeColor = Color.Red;
                    }
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
            });
        }

        private void AnalyseImageCenterTestStart()
        {
            Invoke((EventHandler)delegate
            {
                try
                {
                    Label_Result.Text = "--";
                    Label_Result.ForeColor = Color.Gray;
                    Label_CarType.Text = Config.ProjectType + "";
                    Label_EquipmentA.Text = "";
                    Label_EquipmentB.Text = "";
                    Label_Rotation.Text = "0";
                }
                catch (Exception e)
                {
                    ExceptionUtil.SaveException(e);
                }
            });
        }

        private void CameraNewFrame(Bitmap bitmap)
        {
            Invoke((EventHandler)delegate
            {
                try
                {
                    DrawLinesForShowInfo(bitmap);
                    PictureBox_Main.Image = bitmap;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                GC.Collect();
            });
        }

        private void RobotInCarEvent()
        {
            try
            {
                int type_i = fL.CheckCurrentCarType();
                ProjectType type = (ProjectType)type_i;
                if(type != Config.ProjectType)
                {
                    Invoke((EventHandler)delegate
                    {
                        try
                        {
                            switch (type)
                            {
                                case ProjectType.ID3:
                                    ChangeID3();
                                    break;
                                case ProjectType.ID4X:
                                    ChangeID4();
                                    break;
                                case ProjectType.ID6X:
                                    ChangeID6X();
                                    break;
                                case ProjectType.AUDI:
                                    ChangeAUDI();
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch 
                        { }
                    });
                } 
            } catch { }
        }

        private void ImageTestEvent(int index)
        {
            mL.ShowLog("Image Test Event " + index, LogType.PLC);
            tL.AnalyseImageCenterWithCalcAngle(index);
        }

        private void HeartChangeEvent(int index)
        {
            heart++;
        }

        private void DrawLinesForShowInfo(Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);
            
            int L = 100;
            int X = bitmap.Width / 2;
            int Y = bitmap.Height / 2;

            X = X + Config.Camera_MoveX[Config.ProjectType];
            Y = Y + Config.Camera_MoveY[Config.ProjectType];

            if(ShowCameraOffset)
            {
                X += Config.Camera_OffsetX[Config.ProjectType];
                Y += Config.Camera_OffsetY[Config.ProjectType];
            }

            Pen linePen = new Pen(Color.FromArgb(0, 255, 0), 3);

            g.DrawLine(linePen, new Point(X, Y - L), new Point(X, Y + L));
            g.DrawLine(linePen, new Point(X - L, Y), new Point(X + L, Y));

            int W = 20;

            g.DrawLine(linePen, new Point(X  - W / 2, Y - W / 2), new Point(X + W / 2, Y - W / 2));
            g.DrawLine(linePen, new Point(X  - W / 2, Y + W / 2), new Point(X + W / 2, Y + W / 2));
            g.DrawLine(linePen, new Point(X  - W / 2, Y - W / 2), new Point(X - W / 2, Y + W / 2));
            g.DrawLine(linePen, new Point(X  + W / 2, Y - W / 2), new Point(X + W / 2, Y + W / 2));
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //camL.LiveStop();
                ExceptionUtil.SaveExceptions();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Timer_CheckState_Tick(object sender, EventArgs e)
        {
            Label_VIN.Text = plcL.CurrentRFID;
            SetStateLabelColor(Label_Camera_State1, camL.CameraConnectState(0));
            SetStateLabelColor(Label_Camera_State2, camL.CameraConnectState(1));
            SetStateLabelColor(Label_Camera_State3, camL.CameraConnectState(2));
            SetStateLabelColor(Label_Equipment_State, eL.Connected);
            SetStateLabelColor(Label_FIS_State, fL.Connected);
        }

        private void Timer_CheckPLC_Tick(object sender, EventArgs e)
        {
            if(heart != lastHeart)
                SetStateLabelColor(Label_PLC_State, true);
            else
                SetStateLabelColor(Label_PLC_State, false);
            lastHeart = heart;
        }

        private void SetStateLabelColor(Label label, bool state)
        {
            label.ForeColor = state ? Color.Green : Color.Red;
        }

        private void 标定ID3相机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeID3();
            Task.Factory.StartNew(() => {
                try
                {
                    string fileName = string.Empty;
                    List<PointF> points = GetCalibrationPointsInThread(out fileName);
                    if (points.Count == 3)
                    {
                        Point centerMove = GetCenterPointMovedForCalibration(points[1]);
                        cfgL.SaveID3CameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID3", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定成功");
                        });
                    } 
                    else
                    {
                        expL.SaveToCSV("ID3", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID3", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate
                    {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }
        ExportLogic expL = ExportLogic.GetInstance();
        private void 标定ID4X相机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeID4();
            Task.Factory.StartNew(() => {
                try
                {
                    string fileName = string.Empty;
                    List<PointF> points = GetCalibrationPointsInThread(out fileName);
                    if (points.Count == 3)
                    {
                        Point centerMove = GetCenterPointMovedForCalibration(points[2]);
                        cfgL.SaveID4XCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID4X","OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else
                    {
                        expL.SaveToCSV("ID4X", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch
                {
                    expL.SaveToCSV("ID4X", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate
                    {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定ID6X相机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeID6X();
            Task.Factory.StartNew(() => {
                try
                {
                    string fileName=string.Empty;
                    List<PointF> points = GetCalibrationPointsInThread(out fileName);
                    if (points.Count == 3)
                    {
                        Point centerMove = GetCenterPointMovedForCalibration(points[0]);
                        cfgL.SaveID6XCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID6X", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else
                    {
                        expL.SaveToCSV("ID6X", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch
                {
                    expL.SaveToCSV("ID6X", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate
                    {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定AUDI相机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAUDI();
            Task.Factory.StartNew(() => {
                try
                {
                    string fileName = string.Empty;
                    List<PointF> points = GetCalibrationPointsInThread(out fileName);
                    if (points.Count == 3)
                    {
                        Point centerMove = GetCenterPointMovedForCalibration(points[0]);
                        cfgL.SaveAUDICameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("AUDI", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else
                    {
                        expL.SaveToCSV("AUDI", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch
                {
                    expL.SaveToCSV("AUDI", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate
                    {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        public Point GetCenterPointMovedForCalibration(PointF p)
        {
            int moveX = (int)(p.X + 0.5) - (ImageAnalyseLogic.ImageWidth / 2);
            int moveY = (int)(p.Y + 0.5) - (ImageAnalyseLogic.ImageHeight / 2);

            return new Point(moveX, moveY);
        }

        private List<PointF> GetCalibrationPointsInThread(out string fileName)
        {
            Thread.Sleep(1000);

            Bitmap bitmap = camL.GetCurrentBitmap();
            fileName =Environment.CurrentDirectory+ @"\ImageLog\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg";
            bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            List<PointF> points = iaL.TestGetCircleForCalibration(fileName);

            return points;
        }

        private void ToolStripMenuItem_ShowOffset_Click(object sender, EventArgs e)
        {
            ShowCameraOffset = !ShowCameraOffset;
            if (ShowCameraOffset)
            {
                ToolStripMenuItem_ShowOffset.Text = "隐藏偏差";
            }
            else
            {
                ToolStripMenuItem_ShowOffset.Text = "显示偏差";
            }
        }

        private void 偏差设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if(login.ShowDialog() == DialogResult.OK)
            {
                Form_EditOffset feo = new Form_EditOffset();
                feo.StartPosition = FormStartPosition.CenterParent;
                feo.ShowDialog();
            }
        }

        private void 测试窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK)
            {
                Form_Test form_Test = new Form_Test();
                form_Test.Show();
            }
            
        }

        private void iD3ToolStripMenuItem_NeedTest_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK)
            {
                Config.NeedTest_ID3 = !Config.NeedTest_ID3;
                cfgL.SaveID3NeedTest(Config.NeedTest_ID3);
                iD3ToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID3;
            }
        }

        private void iD4XToolStripMenuItem_NeedTest_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK)
            {
                Config.NeedTest_ID4X = !Config.NeedTest_ID4X;
                cfgL.SaveID4XNeedTest(Config.NeedTest_ID4X);
                iD4XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID4X;
            }
        }

        private void iD6XToolStripMenuItem_NeedTest_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK)
            {
                Config.NeedTest_ID6X = !Config.NeedTest_ID6X;
                cfgL.SaveID6XNeedTest(Config.NeedTest_ID6X);
                iD6XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID6X;
            }
            
        }

        private void aUDIToolStripMenuItem_NeedTest_Click(object sender, EventArgs e)
        {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK)
            {
                Config.NeedTest_AUDI = !Config.NeedTest_AUDI;
                cfgL.SaveAUDINeedTest(Config.NeedTest_AUDI);
                aUDIToolStripMenuItem_NeedTest.Checked = Config.NeedTest_AUDI;
            }
        }

        private void fIS窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_FIS_Info fis = new Form_FIS_Info();
            fis.StartPosition = FormStartPosition.CenterParent;
            fis.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tL.AnalyseImageCenterWithCalcAngle(0);
        }
    }
}