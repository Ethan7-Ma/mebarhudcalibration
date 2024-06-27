using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using NModbus;
using NModbus.IO;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Data;

namespace MEB_ARHUD_Calibration.Logic
{
    class PLCLogic
    {

        private static PLCLogic instance = null;
        
        public static PLCLogic GetInstance()
        {
            if (instance == null)
                instance = new PLCLogic();
            return instance;
        }

        private PLCLogic()
        {
            server.ReceiveDatasEvent += ReceiveDatasEvent;
        }

        MessageLogic mL = MessageLogic.GetInstance();
        EquipmentLogic eL = EquipmentLogic.GetInstance();
        FISLogic fL = FISLogic.GetInstance();
        ExportLogic expL = ExportLogic.GetInstance();
        int plcReceiveStateFlag = 0;

        public string IP = "172.22.24.6";
        public int Port = 502;

        private SocketServer server = new SocketServer();

        ushort[] lastRxBuffer = new ushort[100];
        byte[] RxBuffer = new byte[100];
        byte[] TxBuffer = new byte[100];

        private Thread t_Receive = null;
        private Thread t_StateCheck = null;
        private bool receive_thread = false;
        private bool conn = false;

        public bool ShowReadWriteLogs = false;
        private bool SetEquipmentPset1 = false;

        public bool Connect => GetConnectState();
        public event Action<int> ImageTestEvent = null;
        public event Action<int> HeartChangeEvent = null;
        public event Action<byte[]> ReceiveEvent = null;
        public event Action RobotInCarEvent = null;
        public event Action TestFinishEvent = null;

        public string CurrentRFID = "";
        public string LastRFID = "";

        bool NeedSaveTestTime = false;
        CarInfo_OnLineState CurrentInCar = null;

        public bool TestStep1 = false;
        public bool TestStep2 = false;
        public bool TestStep3 = false;
        public bool TestStep4 = false;

        public void ClearTestStepState()
        {
            TestStep1 = false;
            TestStep2 = false;
            TestStep3 = false;
            TestStep4 = false;
        }


        private bool GetConnectState()
        {
            return conn;
        }

        public void ConnectPLC()
        {
            conn = true;
            try
            {
                receive_thread = true;
                t_Receive = new Thread(SendDatasThread);
                t_Receive.IsBackground = true;
                t_Receive.Start();

                t_StateCheck = new Thread(StateCheckThread);
                t_StateCheck.IsBackground = true;
                t_StateCheck.Start();
            }
            catch (Exception ex)
            {
                ExceptionUtil.SaveException(ex);
            }
        }

        public void DisConnectPLC()
        {
            conn = false;
            receive_thread = false;
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void SendDatasThread()
        {
            while (receive_thread)
            {
                Thread.Sleep(100);
                try
                {
                    try
                    {
                        TxBuffer[3] = RxBuffer[3];
                        server.SendDatas(TxBuffer);

                    }
                    catch (Exception ex)
                    {
                        ExceptionUtil.SaveException(ex);
                    }
                }
                catch (Exception ex) {
                    ExceptionUtil.SaveException(ex);
                }

            }
        }

        private void ReceiveDatasEvent(byte[] datas)
        {
            for(int i = 0; i < datas.Length; i++)
            {
                if(i < RxBuffer.Length)
                {
                    RxBuffer[i] = datas[i];
                }
            }
        }

        public void StateCheckThread()
        {
            while (receive_thread)
            {
                Thread.Sleep(100);
                try
                {
                    CheckReceiveInfo();
                }
                catch (Exception ex)
                {
                    ExceptionUtil.SaveException(ex);
                }

            }
        }


        private string GetStrFromData()
        {
            string rlt = "";

            byte[] arr = new byte[18];
            for(int i = 0; i < 18;i++)
            {
                arr[i] = RxBuffer[39 + i];
            }

            rlt = System.Text.Encoding.Default.GetString(arr);
            
            return rlt;
        }

        private string GetStrFromData(ushort data)
        {
            byte d1 = (byte)(data >> 8);
            byte d2 = (byte)(data & 0xff);
            byte[] arr = { d1, d2 };
            string rlt = System.Text.Encoding.Default.GetString(arr);
            return rlt;
        }

        private int GetCarTypeForPLC()
        {
            switch (Config.ProjectType)
            {
                case ProjectType.ID3:
                    return 1;
                case ProjectType.ID4X:
                    return 2;
                case ProjectType.ID6X:
                    return 3;
                case ProjectType.AUDI:
                    return 4;
                default:
                    break;
            }
            return 0;
        }

        public bool CanChangeCar = true;

        private void CheckReceiveInfo()
        {
            if (ShowReadWriteLogs)
                mL.ShowLog("Analyse PLC Datas", LogType.PLC);

            plcReceiveStateFlag++;
           
                ReceiveEvent?.Invoke(RxBuffer);
           

            if (RxBuffer[3] != lastRxBuffer[1])
                HeartChangeEvent?.Invoke(1);

            CurrentRFID = GetStrFromData();
            CurrentRFID.Replace("\0","");
            if (LastRFID != CurrentRFID)
            {
                if (CurrentRFID.Equals("                  ") || CurrentRFID.Contains("Read")) 
                {
                    SendClearRFIDCheck();
                    SendCarType_RFID(0);
                }
                else
                {
                        fL.SetCurrentCarByRFID(CurrentRFID);
                        int carType = fL.CheckCurrentCarType();
                        SendCarType_RFID(carType);
                        mL.ShowLog("RFID Car Type Check " + carType, LogType.PLC);
                        int hudType = fL.CheckCurrentHUDType();
                        mL.ShowLog("RFID HUD Type Check " + hudType, LogType.PLC);
                        mL.ShowLog("Check RFID: VIN " + fL.CurrentRFIDCar.VIN, LogType.PLC);

                        mL.ShowStateMessage("Check RFID: VIN " + fL.CurrentRFIDCar.VIN);

                        if (carType == (int)Config.ProjectType && hudType > 0)
                        {
                            mL.ShowStateMessage("RFID OK");
                            mL.ShowLog("RFID Check Result OK", LogType.PLC);
                            SendRFIDCheckOK();
                            CanChangeCar = false;
                            if (!SetEquipmentPset1)
                            {
                                SetEquipmentPset1 = true;
                                eL.SendPSet(1);
                            }
                        }
                        else
                        {
                            mL.ShowStateMessage("RFID NG : Car Type" + carType);
                        }

                }
            }
            LastRFID = CurrentRFID;
            if (RxBuffer[1] == 1 && lastRxBuffer[1] != 1)
            {
                mL.ShowLog("PLC RX: 收到急停", LogType.PLC);
            }

            if (RxBuffer[5] == 1)
            {
                mL.ShowStateMessage("请求下发车型");
                mL.ShowLog("PLC RX: 请求下发车型", LogType.PLC);
                CanChangeCar = true;
                

                SendCarType_FIS(GetCarTypeForPLC());
                if (NeedSaveTestTime)
                {
                    NeedSaveTestTime = false;
                    string testTime = ((RxBuffer[20] << 8) + RxBuffer[21]) * 0.1 + "";
                    Task.Factory.StartNew(() => {
                        expL.SaveTestTimeToCSV(fL.GetCarType(fL.GetCarType(CurrentInCar)) + "", CurrentInCar.VIN, testTime);
                    });
                }
            }

            if (RxBuffer[7] == 1 && lastRxBuffer[7] != 1)
            {
                mL.ShowStateMessage("已接收车型");
                mL.ShowLog("PLC RX: 已接收车型", LogType.PLC);
            }

            if (RxBuffer[9] == 1 && lastRxBuffer[9] != 1)
            {
                ClearAllEquipmentAction();
                if (!TestStep1)
                {
                    TestStep1 = true;
                    mL.ShowStateMessage("第一次请求视觉测量");
                    mL.ShowLog("PLC RX: 第一次请求视觉测量", LogType.PLC);
                    ImageTestEvent?.Invoke(1);
                }
            }

            if (RxBuffer[11] == 1 && lastRxBuffer[11] != 1)
            {
                ClearAllEquipmentAction();
                if (!TestStep2 && TestStep1)
                {
                    TestStep2 = true;
                    mL.ShowStateMessage("第二次请求视觉测量");
                    mL.ShowLog("PLC RX: 第二次请求视觉测量", LogType.PLC);
                    ImageTestEvent?.Invoke(2);
                }
            }

            if (RxBuffer[13] == 1 && lastRxBuffer[13] != 1)
            {
                ClearAllEquipmentAction();
                if (!TestStep3 && TestStep2)
                {
                    TestStep3 = true;
                    mL.ShowStateMessage("第三次请求视觉测量");
                    mL.ShowLog("PLC RX: 第三次请求视觉测量", LogType.PLC);
                    ImageTestEvent?.Invoke(3);
                }
            }

            if (RxBuffer[15] == 1 && lastRxBuffer[15] != 1)
            {
                ClearAllEquipmentAction();
                if (!TestStep4 && TestStep3)
                {
                    TestStep4 = true;
                    mL.ShowStateMessage("第四次请求视觉测量");
                    mL.ShowLog("PLC RX: 第四次请求视觉测量", LogType.PLC);
                    ImageTestEvent?.Invoke(4);
                }
            }

            if (RxBuffer[17] == 1 && lastRxBuffer[17] != 1)
            {
                SendClearEquipmentPSet3();
                ClearTestStepState();
                mL.ShowStateMessage("流程结束");
                mL.ShowLog("PLC RX: 流程结束", LogType.PLC);
                SendCarType_FIS(0);
                SendCarType_RFID(0);
                SendClearTestResult();
                TestFinishEvent?.Invoke();
                SetEquipmentPset1 = false;
                CanChangeCar = true;
            }

            if (RxBuffer[19] == 1 && lastRxBuffer[19] != 1)
            {
                mL.ShowStateMessage("认帽程序");
                mL.ShowLog("PLC RX: 认帽程序", LogType.PLC);
                ClearTestStepState();
                try
                {
                    RobotInCarEvent?.Invoke();
                    CurrentInCar = fL.CurrentRFIDCar;
                    NeedSaveTestTime = true;
                }
                catch { }
                Task.Factory.StartNew(() => {
                    //初始化
                    eL.SendAngle_L(-125);
                    eL.SendAngle_R(-125);
                    //eL.SendAngle_L(-1925);
                    //eL.SendAngle_R(-1925);
                    Thread.Sleep(350);
                    SendChangeEquipmentPSet3();
                });

            }

            for (int i = 0; i < 30; i++)
            {
                lastRxBuffer[i] = RxBuffer[i];
            }
        }

        public void SendStop()
        {
            TxBuffer[1] = 1;
        }

        public void SendCarType_FIS(int type)
        {
            TxBuffer[5] = (byte)type;
        }

        public void SendCarType_RFID(int type)
        {
            TxBuffer[7] = (byte)type;
        }

        public void SendEquipmentFirstAction()
        {
            mL.ShowLog("Send Action 1", LogType.PLC);
            TxBuffer[9] = 1;
        }

        public void SendClearEquipmentFirstAction()
        {
            TxBuffer[9] = 0;
        }

        public void SendEquipmentSecondAction()
        {
            mL.ShowLog("Send Action 2", LogType.PLC);
            TxBuffer[11] = 1;
        }

        public void SendClearEquipmentSecondAction()
        {
            TxBuffer[11] = 0;
        }

        public void SendEquipmentThirdAction()
        {
            mL.ShowLog("Send Action 3", LogType.PLC);
            TxBuffer[13] = 1;
        }

        public void SendClearEquipmentThirdAction()
        {

            TxBuffer[13] = 0;
        }

        public void ClearAllEquipmentAction()
        {
            TxBuffer[9] = 0;
            TxBuffer[11] = 0;
            TxBuffer[13] = 0;
            TxBuffer[19] = 0;
        }

        public void SendTestResult(bool success)
        {
            mL.ShowLog("Send Test Result " + success, LogType.PLC);
            TxBuffer[15] = (byte)(success ? 1 : 2);
        }

        public void SendClearTestResult()
        {
            TxBuffer[15] = 0;
        }

        public void SendRFIDCheckOK()
        {
            mL.ShowLog("RFID Check OK", LogType.PLC);
            TxBuffer[17] = 1;
        }

        public void SendRFIDCheckError()
        {
            mL.ShowLog("RFID Check Error", LogType.PLC);
            TxBuffer[17] = 2;
        }

        public void SendClearRFIDCheck()
        {
            mL.ShowLog("RFID Check Clear", LogType.PLC);
            TxBuffer[17] = 0;
        }

        public void SendChangeEquipmentPSet3()
        {
            mL.ShowLog("Set PSet3", LogType.PLC);
            TxBuffer[19] = 1;
        }

        public void SendClearEquipmentPSet3()
        {
            mL.ShowLog("Set Clear PSet3", LogType.PLC);
            TxBuffer[19] = 0;
        }
    }
}
