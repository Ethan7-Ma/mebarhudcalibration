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
    class EquipmentLogic
    {

        private static EquipmentLogic instance = null;

        public static EquipmentLogic GetInstance()
        {
            if (instance == null)
                instance = new EquipmentLogic();
            return instance;
        }

        private EquipmentLogic()
        {

        }

        public bool Connected => Connect_L && Connect_R;

        public EquipmentOnePosLogic Equipment_L = new EquipmentOnePosLogic("L", "172.22.24.23");
        public EquipmentOnePosLogic Equipment_R = new EquipmentOnePosLogic("R", "172.22.24.25");

        bool Connect_L = false;
        bool Connect_R = false;

        public void Connect()
        {

            Task.Factory.StartNew(() => {
                try
                {
                    Connect_L = Equipment_L.Connect();
                }
                catch (Exception ex)
                {

                }
            });
            Task.Factory.StartNew(() => {
                try
                {
                    Connect_R = Equipment_R.Connect();
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void Close()
        {
            Equipment_L.Close();
            Equipment_R.Close();
        }

        public void SendPSet(int index)
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_L.SendSelectPset(index);
                }
                catch (Exception ex)
                {

                }
            });
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_R.SendSelectPset(index);
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendStart()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_L.SendCommuncationStart();
                }
                catch (Exception ex)
                {

                }
            });
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_R.SendCommuncationStart();
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendStop()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_L.SendCommuncationStop();
                }
                catch (Exception ex)
                {

                }
            });
            Task.Factory.StartNew(() => {
                Equipment_R.SendCommuncationStop();
            });
        }

        public void SendAngle_L(int angle)
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_L.SendSetAngle(angle);
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendAngle_R(int angle)
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_R.SendSetAngle(angle);
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendAngleSpeed_L(int angle, int speed)
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_L.SendSetAngleSpeed(angle, speed);
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendAngleSpeed_R(int angle, int speed)
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_R.SendSetAngleSpeed(angle, speed);
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendAngleBack_L()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_L.SendBackAngle();
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void SendAngleBack_R()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Equipment_R.SendBackAngle();
                }
                catch (Exception ex)
                {

                }
            });
        }


    }
}
