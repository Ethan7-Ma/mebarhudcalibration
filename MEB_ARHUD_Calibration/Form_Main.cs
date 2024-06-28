using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEB_ARHUD_Calibration {
    public partial class Form_Main : Form {

        private bool IsOffset = true;

        CameraLogic camL = CameraLogic.GetInstance();
        ConfigLogic cfgL = ConfigLogic.GetInstance();
        TestLogic tL = TestLogic.GetInstance();
        FISLogic fL = FISLogic.GetInstance();
        MessageLogic mL = MessageLogic.GetInstance();
        PLCLogic plcL = PLCLogic.GetInstance();
        EquipmentLogic eL = EquipmentLogic.GetInstance();
        ImageAnalyseLogic iaL = ImageAnalyseLogic.GetInstance();
        ExportLogic expL = ExportLogic.GetInstance();

        int heart = 0;
        int lastHeart = -1;

        public Form_Main() {
            InitializeComponent();
        }


        private void Form_Main_Shown(object sender, EventArgs e) {

            //load Config
            cfgL.LoadSystemConfig();
            cfgL.LoadCameraConfig();

            //UpdateUI
            InitUIInfo();

            //startCamera
            camL.InitCamera();

            //registerEvent
            RegisterEvent();

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

        private void InitUIInfo() {
            iD3ToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID3;
            iD4XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID4X;
            iD6XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID6X;
            aUDIToolStripMenuItem_NeedTest.Checked = Config.NeedTest_AUDI;
            iD3NToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID3N;
            iD4XNToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID4XN;
            iD6XNToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID6XN;
            aUDINToolStripMenuItem_NeedTest.Checked = Config.NeedTest_AUDIN;

            Text = $"ARHUD{Config.CurrentProject}";
            Label_ProjectType.Text = $"{Config.CurrentProject}";
        }

        #region RegisterEvent

        private void RegisterEvent() {
            camL.CameraNewFrameEvent += CameraNewFrame;

            tL.AnalyseImageCenterTestFinished += AnalyseImageCenterTestFinished;
            tL.AnalyseImageCenterTestStart += AnalyseImageCenterTestStart;
            tL.AnalyseImageCenterTestResult += AnalyseImageCenterTestResult;
            tL.CarTestFinish += CarTestFinish;

            plcL.RobotInCarEvent += RobotInCarEvent;

            plcL.ImageTestEvent += ImageTestEvent;
            plcL.HeartChangeEvent += HeartChangeEvent;

            mL.ShowStateMessageEvent += ShowStateMessageEvent;
        }

        private void CameraNewFrame(Bitmap bitmap) {
            Invoke(() => {
                DrawLinesForShowInfo(bitmap, IsOffset);
                PictureBox_Main.Image = bitmap;
            });
        }

        private void AnalyseImageCenterTestFinished() {
            Invoke(() => {
                Label_EquipmentA.Text = $"{tL.TestResult.Angle_L}";
                Label_EquipmentB.Text = $"{tL.TestResult.Angle_R}";
                Label_Rotation.Text = $"{iaL.RotationResult:0.00}";
            });
        }

        private void AnalyseImageCenterTestStart() {
            Invoke(() => {
                Label_Result.Text = "--";
                Label_Result.ForeColor = Color.Gray;
                Label_CarType.Text = Config.CurrentProject + "";
                Label_EquipmentA.Text = "";
                Label_EquipmentB.Text = "";
                Label_Rotation.Text = "0";
            });
        }

        private void AnalyseImageCenterTestResult(bool rlt) {
            Invoke(() => {
                if (rlt) {
                    Label_Result.Text = "OK";
                    Label_Result.ForeColor = Color.Green;
                }
                else {
                    Label_Result.Text = "NG";
                    Label_Result.ForeColor = Color.Red;
                }
            });
        }

        private void CarTestFinish() {
            Invoke(() => {
                int carType = fL.GetCarType(fL.NextNeedTestCar);
                ProjectType nextCarType = (ProjectType)carType;

                Console.WriteLine($"测试完成，切换 {carType}");

                Label_NextCar.Text = $"Next Car : {nextCarType} {fL.NextNeedTestCar.VIN} {fL.NextNeedTestCar.SequenceNumber}";

                cfgL.SaveNextCarInfo(nextCarType, fL.NextNeedTestCar.VIN);
                plcL.CanChangeCar = true;

                Config.CurrentProject = nextCarType;
            });
        }

        private void RobotInCarEvent() {
            ProjectType type = (ProjectType)fL.CheckCurrentCarType();
            if (type != Config.CurrentProject) {
                Config.CurrentProject = type;
            }
        }

        private void ImageTestEvent(int index) {
            mL.ShowLog("Image Test Event " + index, LogType.PLC);
            tL.AnalyseImageCenterWithCalcAngle(index);
        }

        private void HeartChangeEvent(int index) {
            heart++;
        }

        private void ShowStateMessageEvent(string msg) {
            Invoke(() => {
                ToolStripStatusLabel_TestState.Text = msg;
            });
        }

        #endregion

        #region Timer

        private void Timer_CheckState_Tick(object sender, EventArgs e) {
            Label_VIN.Text = plcL.CurrentRFID;
            SetStateLabelColor(Label_Equipment_State, eL.Connected);
            SetStateLabelColor(Label_FIS_State, fL.Connected);
            SetStateLabelColor(Label_Camera_State1, camL.CameraConnectState(0));
            SetStateLabelColor(Label_Camera_State2, camL.CameraConnectState(1));
            SetStateLabelColor(Label_Camera_State3, camL.CameraConnectState(2));
            SetStateLabelColor(Label_Camera_State4, camL.CameraConnectState(3));
            SetStateLabelColor(Label_Camera_State5, camL.CameraConnectState(4));
            SetStateLabelColor(Label_Camera_State6, camL.CameraConnectState(5));

        }

        private void Timer_CheckPLC_Tick(object sender, EventArgs e) {
            if (heart != lastHeart)
                SetStateLabelColor(Label_PLC_State, true);
            else
                SetStateLabelColor(Label_PLC_State, false);
            lastHeart = heart;
        }

        #endregion

        private void SwitchProject(ProjectType targetProjectType) {
            Config.CurrentProject = targetProjectType;
            camL.SwitchCameraWithCurrentProject();
        }

        #region MenuItem_SwitchProject
        private void iD3ToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.ID3);
        private void iD4XToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.ID4X);
        private void iD6XToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.ID6X);
        private void aUDIToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.AUDI);
        private void iD3NToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.ID3N);
        private void iD4XNToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.ID4XN);
        private void iD6XNToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.ID6XN);
        private void aUDINToolStripMenuItem_Click(object sender, EventArgs e) => SwitchProject(ProjectType.AUDIN);
        #endregion

        #region MenuItem_Calibration

        private void 标定ID3相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.ID3);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[1]);
                        cfgL.SaveID3CameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID3", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("ID3", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID3", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定ID4X相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.ID4X);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[2]);
                        cfgL.SaveID4XCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID4X", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("ID4X", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID4X", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定ID6X相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.ID6X);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[0]);
                        cfgL.SaveID6XCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID6X", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("ID6X", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID6X", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定AUDI相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.AUDI);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[0]);
                        cfgL.SaveAUDICameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("AUDI", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("AUDI", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("AUDI", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定ID3N相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.ID3N);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[1]);
                        cfgL.SaveID3NCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID3N", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("ID3N", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID3N", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定ID4XN相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.ID4XN);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[2]);
                        cfgL.SaveID4XNCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID4XN", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("ID4XN", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID4XN", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定ID6XN相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.ID6XN);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[0]);
                        cfgL.SaveID6XNCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("ID6XN", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("ID6XN", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("ID6XN", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        private void 标定AUDIN相机ToolStripMenuItem_Click(object sender, EventArgs e) {
            SwitchProject(ProjectType.AUDIN);
            Task.Factory.StartNew(() => {
                try {
                    string fileName = string.Empty;
                    Bitmap bitmap = camL.GetCurrentBitmap();
                    List<PointF> points = GetCalibrationPointsInThread(bitmap, out fileName);
                    if (points.Count == 3) {
                        Point centerMove = GetCenterPointMovedForCalibration(points[0]);
                        cfgL.SaveAUDINCameraCalibration(centerMove.X, centerMove.Y);
                        expL.SaveToCSV("AUDIN", "OK", centerMove.X.ToString("f2"), centerMove.Y.ToString("f2"), fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定成功");
                        });
                    }
                    else {
                        expL.SaveToCSV("AUDIN", "NG", "NULL", "NULL", fileName);
                        Invoke((EventHandler)delegate {
                            MessageBox.Show("标定失败");
                        });
                    }
                }
                catch {
                    expL.SaveToCSV("AUDIN", "NG", "NULL", "NULL", "NULL");
                    Invoke((EventHandler)delegate {
                        MessageBox.Show("标定失败");
                    });
                }
            });
        }

        #endregion

        #region MenuItem_OtherWindow

        private void fIS窗口ToolStripMenuItem_Click(object sender, EventArgs e) {
            Form_FIS_Info fis = new Form_FIS_Info();
            fis.StartPosition = FormStartPosition.CenterParent;
            fis.Show();
        }

        private void 测试窗口ToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        #endregion

        #region MenuItem_NeedTest
        private void iD3ToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_ID3 = !Config.NeedTest_ID3;
                cfgL.SaveID3NeedTest(Config.NeedTest_ID3);
                iD3ToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID3;
            }
        }

        private void iD4XToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_ID4X = !Config.NeedTest_ID4X;
                cfgL.SaveID4XNeedTest(Config.NeedTest_ID4X);
                iD4XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID4X;
            }
        }

        private void iD6XToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_ID6X = !Config.NeedTest_ID6X;
                cfgL.SaveID6XNeedTest(Config.NeedTest_ID6X);
                iD6XToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID6X;
            }

        }

        private void aUDIToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_AUDI = !Config.NeedTest_AUDI;
                cfgL.SaveAUDINeedTest(Config.NeedTest_AUDI);
                aUDIToolStripMenuItem_NeedTest.Checked = Config.NeedTest_AUDI;
            }
        }

        private void iD3NToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_ID3N = !Config.NeedTest_ID3N;
                cfgL.SaveID3NNeedTest(Config.NeedTest_ID3N);
                iD3NToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID3N;
            }
        }

        private void iD4XNToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_ID4XN = !Config.NeedTest_ID4XN;
                cfgL.SaveID4XNNeedTest(Config.NeedTest_ID4XN);
                iD4XNToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID4XN;
            }
        }

        private void iD6XNToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_ID6XN = !Config.NeedTest_ID6XN;
                cfgL.SaveID6XNNeedTest(Config.NeedTest_ID6XN);
                iD6XNToolStripMenuItem_NeedTest.Checked = Config.NeedTest_ID6XN;
            }

        }

        private void aUDINToolStripMenuItem_NeedTest_Click(object sender, EventArgs e) {
            Form_Login login = new Form_Login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK) {
                Config.NeedTest_AUDIN = !Config.NeedTest_AUDIN;
                cfgL.SaveAUDINNeedTest(Config.NeedTest_AUDIN);
                aUDINToolStripMenuItem_NeedTest.Checked = Config.NeedTest_AUDIN;
            }
        }

        #endregion

        #region ImageProcess
        private static void DrawLinesForShowInfo(Bitmap bitmap, bool isOffset) {
            Graphics g = Graphics.FromImage(bitmap);

            int L = 100;
            int X = bitmap.Width / 2;
            int Y = bitmap.Height / 2;

            X += Config.Camera_MoveX[Config.CurrentProject];
            Y += Config.Camera_MoveY[Config.CurrentProject];

            if (isOffset) {
                X += Config.Camera_OffsetX[Config.CurrentProject];
                Y += Config.Camera_OffsetY[Config.CurrentProject];
            }

            Pen linePen = new(Color.FromArgb(0, 255, 0), 3);

            g.DrawLine(linePen, new Point(X, Y - L), new Point(X, Y + L));
            g.DrawLine(linePen, new Point(X - L, Y), new Point(X + L, Y));

            int length = 20;

            g.DrawLine(linePen, new Point(X - length / 2, Y - length / 2), new Point(X + length / 2, Y - length / 2));
            g.DrawLine(linePen, new Point(X - length / 2, Y + length / 2), new Point(X + length / 2, Y + length / 2));
            g.DrawLine(linePen, new Point(X - length / 2, Y - length / 2), new Point(X - length / 2, Y + length / 2));
            g.DrawLine(linePen, new Point(X + length / 2, Y - length / 2), new Point(X + length / 2, Y + length / 2));
        }

        private static void SetStateLabelColor(Label label, bool state) {
            label.ForeColor = state ? Color.Green : Color.Red;
        }

        private static Point GetCenterPointMovedForCalibration(PointF p) {
            int moveX = (int)(p.X + 0.5) - (ImageAnalyseLogic.ImageWidth / 2);
            int moveY = (int)(p.Y + 0.5) - (ImageAnalyseLogic.ImageHeight / 2);
            return new Point(moveX, moveY);
        }

        private static List<PointF> GetCalibrationPointsInThread(Bitmap bitmap, out string fileName) {
            fileName = @$"\ImageLog\{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.jpg";
            bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            List<PointF> points = ImageAnalyseLogic.TestGetCircleForCalibration(fileName);
            return points;
        }
        #endregion
    }
}