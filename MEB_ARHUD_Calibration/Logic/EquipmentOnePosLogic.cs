using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MEB_ARHUD_Calibration.Logic
{
    class EquipmentOnePosLogic
    {

        public EquipmentOnePosLogic()
        {

        }

        public EquipmentOnePosLogic(string name, string ip)
        {
            this.Name = name;
            this.IP = ip;
        }

        public string Name = "L";
        public string IP = "127.0.0.1";
        public int Port = 4545;

        public bool Connected => connected;

        bool connected = false;
        private Socket socketClient = null;

        int CheckConnectState = 5;

        public int LastAngle = 0;

        MessageLogic mL = MessageLogic.GetInstance();

        public bool Connect()
        {
            CheckConnectState = 5;

            if (socketClient != null && socketClient.Connected)
                return true;

            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(IP);
            IPEndPoint point = new IPEndPoint(address, Port);

            try
            {
                socketClient.Connect(point);
                connected = true;
                StartReceiveThread();
                mL.ShowLog(Name + " Connect Success", LogType.Equipment);
            }
            catch (Exception)
            {
                mL.ShowLog(Name + " Connect False", LogType.Equipment);
                return false;
            }

            return true;
        }

        public void Close()
        {
            try
            {
                socketClient.Close();
            }
            catch (Exception)
            {
                mL.ShowLog(Name + " Close Error", LogType.Equipment);
            }
        }

        private void StartReceiveThread()
        {
            Thread t = new Thread(ReceiveThread);
            t.IsBackground = true;
            t.Start();
        }

        private void ReceiveThread()
        {
            while (connected && socketClient.Connected)
            {
                byte[] receive = ReceiveDatas();
                AnalyseReceiveInfo(receive);
            }
        }

        private byte[] ReceiveDatas()
        {
            try
            {
                CheckConnectState = 5;
                byte[] arrRecvmsg = new byte[1024 * 1024];
                int length = socketClient.Receive(arrRecvmsg);
                byte[] recDatas = new byte[length];
                for (int i = 0; i < length; i++)
                    recDatas[i] = arrRecvmsg[i];

                mL.ShowLog("Rx " + ByteArrToHexString(recDatas), LogType.Equipment);

                return recDatas;
            }
            catch (Exception)
            {
                connected = false;
                if (socketClient != null)
                    socketClient.Dispose();
                socketClient = null;
                mL.ShowLog(Name + " Client Close", LogType.Equipment);
            }
            return new byte[0];
        }

        private void AnalyseReceiveInfo(byte[] datas)
        {

        }

        object sendMessageState = new object();

        public void ClientSendDatas(byte[] datas)
        {
            try
            {
                if (!connected)
                {
                    return;
                }

                lock (sendMessageState)
                {
                    socketClient?.Send(datas);
                    mL.ShowLog("Tx " + ByteArrToHexString(datas), LogType.Equipment);
                    Thread.Sleep(50);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        bool isKeepingAlive = false;

        byte[] Data_CommuncationStart = { 0x30, 0x30, 0x32, 0x30, 0x30, 0x30, 0x30, 0x31, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00 };

        public void SendCommuncationStart()
        {
            ClientSendDatas(Data_CommuncationStart);
            if (!isKeepingAlive)
            {
                isKeepingAlive = true;
                Thread t_KeepAlive = new Thread(SendKeepAliveThread);
                t_KeepAlive.IsBackground = true;
                t_KeepAlive.Start();
            }
        }

        private void SendKeepAliveThread()
        {
            while (isKeepingAlive)
            {
                SendKeepAlive();
                Thread.Sleep(4000);
            }
        }

        byte[] Data_KeepAlive = { 0x30, 0x30, 0x32, 0x30, 0x39, 0x39, 0x39, 0x39, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00 };

        public void SendKeepAlive()
        {
            ClientSendDatas(Data_KeepAlive);
        }

        byte[] Data_SelectPset = { 0x30, 0x30, 0x32, 0x33, 0x30, 0x30, 0x31, 0x38, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x30, 0x30, 0x31, 0x00 };

        public void SendSelectPset(int index)
        {
            Pset = index;
            byte selected = (byte)(0x30 + index);
            Data_SelectPset[22] = selected;
            ClientSendDatas(Data_SelectPset);
        }

        byte[] Data_CommuncationStop = { 0x30, 0x30, 0x32, 0x30, 0x30, 0x30, 0x30, 0x33, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00 };

        public void SendCommuncationStop()
        {
            ClientSendDatas(Data_CommuncationStop);
            isKeepingAlive = false;
        }

        byte[] Data_SetAngle = {
            0x30,0x32,0x35,0x36,0x37,0x34,0x31,0x34,0x30,0x30,
            0x31,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,
            0x30,0x31,0x30,0x31,0x30,0x32,0x30,0x30,0x31,0x30,
            0x33,0x30,0x31,0x30,0x34,0x46,0x20,0x30,0x35,0x33,
            0x20,0x30,0x36,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
            0x37,0x30,0x30,0x30,0x37,0x30,0x30,0x30,0x38,0x30,
            0x30,0x30,0x37,0x30,0x30,0x30,0x39,0x30,0x30,0x30,
            0x30,0x30,0x30,0x31,0x30,0x30,0x30,0x30,0x30,0x30,
            0x30,0x31,0x31,0x30,0x30,0x30,0x30,0x30,0x30,0x31,
            0x32,0x30,0x30,0x32,0x39,0x30,0x30,0x31,0x33,0x30,
            0x30,0x33,0x31,0x30,0x30,0x31,0x34,0x30,0x30,0x30,
            0x30,0x30,0x30,0x31,0x35,0x30,0x30,0x33,0x30,0x30,
            0x30,0x31,0x36,0x30,0x30,0x30,0x30,0x30,0x30,0x31,
            0x37,0x30,0x30,0x30,0x30,0x30,0x30,0x31,0x38,0x30,
            0x30,0x30,0x30,0x30,0x30,0x31,0x39,0x30,0x30,0x30,
            0x30,0x30,0x30,0x32,0x30,0x30,0x30,0x30,0x30,0x30,
            0x30,0x32,0x31,0x30,0x30,0x30,0x30,0x30,0x36,0x32,
            0x32,0x30,0x30,0x30,0x30,0x30,0x30,0x32,0x33,0x30,
            0x30,0x30,0x30,0x30,0x30,0x32,0x34,0x30,0x30,0x32,
            0x35,0x30,0x30,0x30,0x30,0x30,0x30,0x32,0x36,0x30,
            0x30,0x30,0x30,0x30,0x30,0x32,0x37,0x30,0x30,0x30,
            0x30,0x32,0x38,0x30,0x30,0x30,0x30,0x32,0x39,0x30,
            0x30,0x30,0x30,0x30,0x30,0x33,0x30,0x30,0x30,0x30,
            0x30,0x30,0x30,0x33,0x31,0x30,0x30,0x30,0x30,0x30,
            0x30,0x33,0x32,0x43,0x57,0x33,0x33,0x20,0x33,0x34,
            0x30,0x30,0x30,0x33,0x35,0x30,0x00 };

        int Pset = 1;


        public void SendSetAngleSpeed(int angle, int speed)
        {
            if (angle >= 0)
            {
                Data_SetAngle[243] = 0x43;
                Data_SetAngle[244] = 0x57;
            }
            else
            {
                Data_SetAngle[243] = 0x43;
                Data_SetAngle[244] = 0x43;

            }
            if (angle == 0)
            {
                if (LastAngle < 0)
                    angle = -6;
                else
                    angle = 6;
            }

            SetMinAngle(angle);
            SetAngle(angle);
            SetSafeAngle(angle);
            SetMaxAngle(angle);
            
            for (int i = 0; i < 5; i++)
            {
                Data_SetAngle[168 - i] = 0x30;
            }

            byte[] speedData = ChangeSpeedToData(speed);
            for (int i = 0; i < speedData.Length; i++)
            {
                byte data = speedData[speedData.Length - 1 - i];
                Data_SetAngle[168 - i] = data;
            }
            
            LastAngle = angle;
            mL.ShowLog(Name + ": " + angle, LogType.Equipment);
            ClientSendDatas(Data_SetAngle);
        }

        public void SendSetAngle(int angle)
        {
            int speed = 10;
            int angleValue = Math.Abs(angle);
            if(angleValue >=200)
            {
                speed = 20;
            }
            SendSetAngleSpeed(angle, speed);
        }

        public void SendBackAngle()
        {
            if (LastAngle == 0)
            {
                SendSetAngle(0);
            }
            
            else if (LastAngle > 0)
            {
                //SendSetAngle(-5);
                SendSetAngle(-7);
            }
            else if (LastAngle < 0)
            {
                //SendSetAngle(5);
                SendSetAngle(7);
            }
        }


        private void SetMinAngle(int angle)
        {
            angle = Math.Abs(angle);
            if (angle < 100)
                angle = 0;
            else
                angle -= 100;

            for (int i = 0; i < 5; i++)
            {
                Data_SetAngle[95 - i] = 0x30;
            }

            byte[] angleData = ChangeAngleToData(angle);
            for (int i = 0; i < angleData.Length; i++)
            {
                byte data = angleData[angleData.Length - 1 - i];
                Data_SetAngle[95 - i] = data;
            }

        }

        private void SetAngle(int angle)
        {
            for (int i = 0; i < 5; i++)
            {
                Data_SetAngle[119 - i] = 0x30;
            }

            byte[] angleData = ChangeAngleToData(angle);
            for (int i = 0; i < angleData.Length; i++)
            {
                byte data = angleData[angleData.Length - 1 - i];
                Data_SetAngle[119 - i] = data;
            }
        }

        private void SetSafeAngle(int angle)
        {
            angle = Math.Abs(angle);
            angle += 100;

            for (int i = 0; i < 5; i++)
            {
                Data_SetAngle[111 - i] = 0x30;
            }

            byte[] angleData = ChangeAngleToData(angle);
            for (int i = 0; i < angleData.Length; i++)
            {
                byte data = angleData[angleData.Length - 1 - i];
                Data_SetAngle[111 - i] = data;
            }

        }

        private void SetMaxAngle(int angle)
        {
            angle = Math.Abs(angle);
            angle += 100;

            for (int i = 0; i < 5; i++)
            {
                Data_SetAngle[111 - i] = 0x30;
            }

            byte[] angleData = ChangeAngleToData(angle);
            for (int i = 0; i < angleData.Length; i++)
            {
                byte data = angleData[angleData.Length - 1 - i];
                Data_SetAngle[103 - i] = data;
            }

        }

        private byte[] ChangeAngleToData(int angle)
        {
            string angleStr = Math.Abs(angle) + "";
            byte[] datas = StringToByteArray(angleStr);
            return datas;
        }

        private byte[] ChangeSpeedToData(int speed)
        {
            string angleStr = Math.Abs(speed) + "";
            byte[] datas = StringToByteArray(angleStr);
            return datas;
        }

        public byte[] StringToByteArray(string str)
        {
            char[] charArr = str.ToCharArray(0, str.Length);
            byte[] rlt = new byte[charArr.Length];
            for (int i = 0; i < charArr.Length; i++)
            {
                char one = charArr[i];
                rlt[i] = (byte)one;
            }
            return rlt;
        }

        public static string ByteArrToHexString(byte[] values)
        {
            string rlt = "";
            for (int i = 0; i < values.Length; i++)
            {
                rlt += ByteToHexString(values[i]) + " ";
            }
            return rlt;
        }

        public static string ByteToHexString(byte value)
        {
            string valueStr = value.ToString("x8");
            return valueStr.Substring(valueStr.Length - 2, 2);
        }
    }
}
