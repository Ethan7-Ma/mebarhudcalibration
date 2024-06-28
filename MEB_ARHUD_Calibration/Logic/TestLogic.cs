using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MEB_ARHUD_Calibration.Data;
using MEB_ARHUD_Calibration.Common;

namespace MEB_ARHUD_Calibration.Logic
{
    class TestLogic
    {
        private static TestLogic instance = null;

        public static TestLogic GetInstance()
        {
            if (instance == null)
                instance = new TestLogic();
            return instance;
        }

        private TestLogic()
        {
            
        }

        public event Action CarTestFinish = null;
        public event Action AnalyseImageCenterTestFinished = null;
        public event Action<bool> AnalyseImageCenterTestResult = null;
        public event Action AnalyseImageCenterTestStart = null;

        ImageAnalyseLogic iaL = ImageAnalyseLogic.GetInstance();
        MessageLogic mL = MessageLogic.GetInstance();
        CameraLogic camL = CameraLogic.GetInstance();
        PLCLogic plcL = PLCLogic.GetInstance();
        EquipmentLogic eL = EquipmentLogic.GetInstance();
        ExportLogic expL = ExportLogic.GetInstance();

        FISLogic fL = FISLogic.GetInstance();


        public TestResult TestResult = new TestResult();

        private int Angle_L_Moved = 0;
        private int Angle_R_Moved = 0;

        private DateTime StartTime;
        private int CurrentTestIndex = 0;

        public void TestAnalyseImageCenterWithCalcAngle()
        {
            Task.Factory.StartNew(() => {
                Bitmap bitmap = camL.GetCurrentBitmap();
                string fileName = GetImageSaveFileName("Test");
                bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                Point center = iaL.TestGetCircle(fileName);
                Point centerMoved = GetCenterPointMoved(center);
                double[] rlt = GetCircleNeedToChange(centerMoved, 0, 0);

                TestResult.AnalyseCenter = center;
                TestResult.Circle_L = rlt[0];
                TestResult.Circle_R = rlt[1];

                int Angle_L = (int)(rlt[0] * 36) * 10;
                int Angle_R = (int)(rlt[1] * 36) * 10;

                TestResult.Angle_L = Angle_L;
                TestResult.Angle_R = Angle_R;

                mL.ShowLog("L: " + Angle_L + " R: " + Angle_R, LogType.ImageAnalyse);

                AnalyseImageCenterTestFinished?.Invoke();
            });
        }

        public void TestAnalyseImageCenterWithCalcAngle_ForTest(string fileName)
        {
            Task.Factory.StartNew(() => {
                Point center = iaL.TestGetCircle(fileName);
                Point centerMoved = GetCenterPointMoved(center);
                double[] rlt = GetCircleNeedToChange(centerMoved, 0, 0);

                TestResult.AnalyseCenter = center;
                TestResult.Circle_L = rlt[0];
                TestResult.Circle_R = rlt[1];

                int Angle_L = (int)(rlt[0] * 36) * 10;
                int Angle_R = (int)(rlt[1] * 36) * 10;

                TestResult.Angle_L = Angle_L;
                TestResult.Angle_R = Angle_R;

                mL.ShowLog("L: " + Angle_L + " R: " + Angle_R, LogType.ImageAnalyse);

                AnalyseImageCenterTestFinished?.Invoke();
            });
        }

        public Point GetCenterPointMoved(Point center)
        {
            int moveX = center.X - (ImageAnalyseLogic.ImageWidth / 2 + Config.Camera_MoveX[Config.ProjectType] + Config.Camera_OffsetX[Config.ProjectType]);
            int moveY = center.Y - (ImageAnalyseLogic.ImageHeight / 2 + Config.Camera_MoveY[Config.ProjectType] + Config.Camera_OffsetY[Config.ProjectType]);

            return new Point(moveX, moveY);
        }

        public double[] GetCircleNeedToChange(Point centerMoved, int lastMoveL, int lastMoveR)
        {
            double L = 0 - (double)centerMoved.X / 14;
            double R = 0 - (double)centerMoved.Y / 70;

            switch (Config.ProjectType)
            {
                case ProjectType.ID3:
                    L = 0 - (double)centerMoved.X / 16;
                    R = 0 - (double)centerMoved.Y / 65;
                    L += R * 0.5;
                    R -= L * 0.01;
                    break;
                case ProjectType.ID4X:
                    L = 0 - (double)centerMoved.X / 15;
                    R = 0 - (double)centerMoved.Y / 52;
                    L += R * 0.42;
                    R -= L * 0.02;
                    break;
                case ProjectType.ID6X:
                    L = 0 - (double)centerMoved.X / 18;
                    R = 0 - (double)centerMoved.Y / 60;
                    L += R * 0.44;
                    R -= L * 0.01;
                    break;
                case ProjectType.AUDI:
                    L = 0 - (double)centerMoved.X / 18;
                    R = 0 - (double)centerMoved.Y / 60;
                    L += R * 0.44;
                    R -= L * 0.01;
                    break;
                default:
                    break;
            }

            if (L > 6)
                L = 6;
            if (L < -6)
                L = -6;

            if (R > 6)
                R = 6;
            if (R < -6)
                R = 6;

            if (lastMoveL < -100 && L > 0)
            {
                L += 0.1;
            }
            else if (lastMoveL > 100 && L < 0)
            {
                L -= 0.1;
            }

            if (lastMoveR < -100 && R > 0)
            {
                R += 0.1;
            }
            else if (lastMoveR > 100 && R < 0)
            {
                R -= 0.1;
            }

            return new double[] { L, R };
        }


        private void InitAngleLimit()
        {
            Angle_L_Moved = 0;
            Angle_R_Moved = 0;
        }

        private bool CheckAngleIsOverLimit()
        {
            if((Angle_L_Moved <= -MAX_MOVE_ANGLE || Angle_L_Moved >= MAX_MOVE_ANGLE) && (Angle_R_Moved <= -MAX_MOVE_ANGLE || Angle_R_Moved >= MAX_MOVE_ANGLE))
                return true;
            return false;
        }

        static int MAX_MOVE_ANGLE = Config.MaxMoveAngle;

        private int[] CheckAngleCanMove(int Angle_L, int Angle_R)
        {
            int Angle_CanMove_L = 0;
            int Angle_CanMove_R = 0;
            if (Angle_L + Angle_L_Moved <= MAX_MOVE_ANGLE && Angle_L + Angle_L_Moved >= -MAX_MOVE_ANGLE)
            {
                Angle_CanMove_L = Angle_L;
            }
            else if (Angle_L + Angle_L_Moved > MAX_MOVE_ANGLE)
            {
                Angle_CanMove_L = MAX_MOVE_ANGLE - Angle_L_Moved;
            }
            else if (Angle_L + Angle_L_Moved < -MAX_MOVE_ANGLE)
            {
                Angle_CanMove_L = -MAX_MOVE_ANGLE - Angle_L_Moved;
            }

            if (Angle_R + Angle_R_Moved < MAX_MOVE_ANGLE && Angle_R + Angle_R_Moved > -MAX_MOVE_ANGLE)
            {
                Angle_CanMove_R = Angle_R;
            }
            else if (Angle_R + Angle_R_Moved > MAX_MOVE_ANGLE)
            {
                Angle_CanMove_R = MAX_MOVE_ANGLE - Angle_R_Moved;
            }
            else if (Angle_R + Angle_R_Moved < -MAX_MOVE_ANGLE)
            {
                Angle_CanMove_R = -MAX_MOVE_ANGLE - Angle_R_Moved;
            }

            return new int[] { Angle_CanMove_L, Angle_CanMove_R };
        }


        private bool CheckResultCanCalc(Point p)
        {
            int X = p.X - ImageAnalyseLogic.ImageWidth / 2;
            int Y = p.Y - ImageAnalyseLogic.ImageHeight / 2;

            if (X > -500 && X < 500 && Y > -800 && Y < 800)
                return true;

            return false;
        }


        double trans_X = 0;
        double trans_Y = 0;

        public void AnalyseImageCenterWithCalcAngle(int index)
        {

            Task.Factory.StartNew(() => {
                CurrentTestIndex = index;
                if (index == 1)
                    StartTime = DateTime.Now;
                
                trans_X = 0;
                trans_Y = 0;
                try
                {
                    AnalyseImageCenterTestStart?.Invoke();
                    mL.ShowStateMessage("开始测试");
                    Thread.Sleep(1000);

                    string fileName = GetImageSaveFileName(plcL.CurrentRFID);
                    using (Bitmap bitmap = camL.GetCurrentBitmap())
                    {
                        try
                        {
                            bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        catch
                        {
                            try
                            {
                                fileName = GetImageSaveFileName("OnlyTest");
                                bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                            catch { }
                        }
                    }
                    Thread.Sleep(50);
                    Point center = iaL.TestGetCircle(fileName);
                    Point centerMoved = GetCenterPointMoved(center);

                    trans_X = centerMoved.X * Config.PixelToMMRatio;
                    trans_Y = centerMoved.Y * Config.PixelToMMRatio;

                    bool testResult = CheckResultCanCalc(center);
                    if (testResult)
                    {
                        
                        double[] rlt = GetCircleNeedToChange(centerMoved, eL.Equipment_L.LastAngle, eL.Equipment_R.LastAngle);

                        TestResult.AnalyseCenter = center;
                        TestResult.Circle_L = rlt[0];
                        TestResult.Circle_R = rlt[1];

                        int Angle_L = (int)(rlt[0] * 36) * 10;
                        int Angle_R = (int)(rlt[1] * 36) * 10;

                        TestResult.Angle_L = Angle_L;
                        TestResult.Angle_R = Angle_R;

                        mL.ShowLog("L: " + Angle_L + " R: " + Angle_R, LogType.ImageAnalyse);
                        AnalyseImageCenterTestFinished?.Invoke();

                        if (centerMoved.X <= Config.CenterMovedLimit && centerMoved.X >= -Config.CenterMovedLimit && centerMoved.Y <= Config.CenterMovedLimit && centerMoved.Y >= -Config.CenterMovedLimit)
                        {

                            bool RotationOK = CheckRotationOK(iaL.RotationResult);
                            if (RotationOK)
                            {
                                expL.SaveToCSV(plcL.CurrentRFID, fileName, (int)TestResultType.Success, iaL.RotationResult, iaL.LOAResult, trans_X, trans_Y, Angle_L, Angle_R, center.X, center.Y);
                                AnalyseImageCenterTestResult(true);
                                mL.ShowLog("HUD Success", LogType.ImageAnalyse);
                                TestSuccess();
                            }
                            else
                            {
                                expL.SaveToCSV(plcL.CurrentRFID, fileName, (int)TestResultType.RotationError, iaL.RotationResult, iaL.LOAResult, trans_X, trans_Y, Angle_L, Angle_R, center.X, center.Y);
                                AnalyseImageCenterTestResult(false);
                                mL.ShowLog("HUD Fail", LogType.ImageAnalyse);
                                TestFail( TestResultType.RotationError);
                            }
                        }
                        else
                        {
                            if (index == 1)
                                InitAngleLimit();

                            if (!CheckAngleIsOverLimit())
                            {
                                int[] move = CheckAngleCanMove(Angle_L, Angle_R);


                                expL.SaveToCSV(plcL.CurrentRFID, fileName, (int)TestResultType.NeedSetAngle, iaL.RotationResult, iaL.LOAResult, trans_X, trans_Y, Angle_L, Angle_R, center.X, center.Y);

                                Angle_L_Moved += move[0];
                                Angle_R_Moved += move[1];

                                eL.SendAngle_L(move[0]);
                                eL.SendAngle_R(move[1]);


                                Thread.Sleep(350);
                                //还得继续做
                                if (index == 1)
                                    plcL.SendEquipmentFirstAction();
                                else if (index == 2)
                                    plcL.SendEquipmentSecondAction();
                                else if (index == 3)
                                    plcL.SendEquipmentThirdAction();
                                else if (index == 4)
                                {
                                    mL.ShowLog("HUD Fail By Action 3rd", LogType.PLC);
                                    
                                    AnalyseImageCenterTestResult(false);
                                    TestFail(TestResultType.OutOfRange);
                                }
                            }
                            else
                            {
                                expL.SaveToCSV(plcL.CurrentRFID, fileName, (int)TestResultType.OutOfRange, iaL.RotationResult, iaL.LOAResult, trans_X, trans_Y, Angle_L, Angle_R, center.X, center.Y);
                                mL.ShowLog("HUD Fail By Over Range", LogType.PLC);
                                AnalyseImageCenterTestResult(false);
                                TestFail(TestResultType.OutOfRange);
                            }
                            
                        }
                    }
                    else
                    {
                        expL.SaveToCSV(plcL.CurrentRFID, fileName, (int)TestResultType.AnalyseError, 0, 0, 0, 0, 0, 0, center.X, center.Y);
                        mL.ShowLog("HUD Fail By Result Out Range", LogType.PLC);
                        AnalyseImageCenterTestResult(false);
                        TestFail(TestResultType.AnalyseError);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtil.SaveException(ex);
                }
                
            });
        }

        private bool CheckRotationOK(double rotation)
        {
            switch (Config.ProjectType)
            {
                case ProjectType.Unknown:
                    return true;
                case ProjectType.ID3:
                    return rotation > -Config.Rotation_ID3 && rotation <= Config.Rotation_ID3;
                case ProjectType.ID4X:
                    return rotation > -Config.Rotation_ID4X && rotation <= Config.Rotation_ID4X;
                case ProjectType.ID6X:
                    return rotation > -Config.Rotation_ID6X && rotation <= Config.Rotation_ID6X;
                case ProjectType.AUDI:
                    return rotation > -Config.Rotation_AUDI && rotation <= Config.Rotation_AUDI;
                default:
                    break;
            }
            return true;
        }

        private void TestSuccess()
        {
            DateTime stopTime = DateTime.Now;
            TimeSpan useTime = stopTime - StartTime;

            eL.SendAngleBack_L();
            eL.SendAngleBack_R();
            Thread.Sleep(350);
            try
            {
                fL.UploadCurrentCarFISResult(iaL.RotationResult, iaL.LOAResult, 1);
                string currCarType = "";
                if (fL.CurrentRFIDCar != null)
                    currCarType = ((ProjectType)fL.CheckCurrentCarType()) + "";
                expL.SaveTestResultToCSV(currCarType, plcL.CurrentRFID, fL.CurrentRFIDCar.VIN, 1, 
                    iaL.RotationResult, iaL.LOAResult, trans_X, trans_Y, CurrentTestIndex, useTime.TotalSeconds, Angle_L_Moved, Angle_R_Moved);
            }
            catch (Exception ex)
            {
                ExceptionUtil.SaveException(ex);
            }

            plcL.SendTestResult(true);
            CarInfo_OnLineState nextCar = fL.SetNextNeedTestCar();
            CarTestFinish?.Invoke();
            plcL.CanChangeCar = true;
            mL.ShowStateMessage("测试成功");
        }

        private void TestFail(TestResultType resultType)
        {
            DateTime stopTime = DateTime.Now;
            TimeSpan useTime = stopTime - StartTime;

            eL.SendAngleBack_L();
            eL.SendAngleBack_R();
            Thread.Sleep(350);
            try
            {
                fL.UploadCurrentCarFISResult(iaL.RotationResult, iaL.LOAResult, 0);
                string currCarType = "";
                if (fL.CurrentRFIDCar != null)
                    currCarType = ((ProjectType)fL.CheckCurrentCarType()) + "";
                expL.SaveTestResultToCSV(currCarType, plcL.CurrentRFID, fL.CurrentRFIDCar.VIN, (int)resultType, 
                    iaL.RotationResult, iaL.LOAResult, trans_X, trans_Y, CurrentTestIndex, useTime.TotalSeconds, Angle_L_Moved, Angle_R_Moved);
            }
            catch (Exception ex)
            {
                ExceptionUtil.SaveException(ex);
            }
            plcL.SendTestResult(false);
            CarInfo_OnLineState nextCar = fL.SetNextNeedTestCar();
            CarTestFinish?.Invoke();
            plcL.CanChangeCar = true;
            mL.ShowStateMessage("测试失败");
        }

        private string GetImageSaveFileName(string name)
        {
            return ExportUtil.GetImageSaveFileName(name);
        }
    }
}
