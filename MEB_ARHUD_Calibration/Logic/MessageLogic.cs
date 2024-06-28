using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MEB_ARHUD_Calibration.Common;

namespace MEB_ARHUD_Calibration.Logic
{
    enum LogType
    {
        PLC,
        Camera,
        ImageAnalyse,
        Equipment,
        FIS,
    }

    class MessageLogic
    {
        public bool ShowPLCLog = false;
        public bool ShowCameraLog = false;
        public bool ShowImageAnalyseLog = false;
        public bool ShowEquipmentLog = false;
        public bool ShowFISLog = false;

        public event Action<string> ShowStateMessageEvent = null;

        private static MessageLogic? instance = null;
        public static MessageLogic GetInstance() => instance ??= new MessageLogic();

        string lastStateMsg = "";

        public void ShowStateMessage(string msg)
        {
            if (!lastStateMsg.Equals(msg))
            {
                lastStateMsg = msg;
                ShowStateMessageEvent?.Invoke(msg);
            }
        }

        public void ShowLog(string msg, LogType type)
        {
            try
            {
                switch (type)
                {
                    case LogType.PLC:
                        if (ShowPLCLog)
                            ConsolePrintLog(msg);
                        break;
                    case LogType.Camera:
                        if (ShowCameraLog)
                            ConsolePrintLog(msg);
                        break;
                    case LogType.ImageAnalyse:
                        if (ShowImageAnalyseLog)
                            ConsolePrintLog(msg);
                        break;
                    case LogType.Equipment:
                        if (ShowEquipmentLog)
                            ConsolePrintLog(msg);
                        break;
                    case LogType.FIS:
                        if (ShowFISLog)
                            ConsolePrintLog(msg);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionUtil.SaveException(ex);
            }
        }

        private void ConsolePrintLog(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff ") + msg);
        }

    }
}
