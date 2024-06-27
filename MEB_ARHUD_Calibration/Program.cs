using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Logic;

namespace MEB_ARHUD_Calibration
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            SecurityLogic sL = new SecurityLogic();
            if(sL.CheckRegistered())
            {
                Config.IsDevelopmentEnvironment = true;
            }

            ConfigLogic cL = ConfigLogic.GetInstance();
            cL.InitSystemInfos();
            cL.InitCameraDeviceInfo();

            Application.Run(new Form_Main());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            SaveLog(((Exception)e.ExceptionObject).Message + "\r\n" + ((Exception)e.ExceptionObject).StackTrace);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            SaveLog(e.Exception.Message + "\r\n" + e.Exception.StackTrace);
        }

        private static void SaveLog(string message)
        {
            string currPathStr = System.Environment.CurrentDirectory;
            currPathStr = currPathStr + "Log" + "\\" + System.DateTime.Now.ToShortDateString();
            if (!Directory.Exists(currPathStr))
                Directory.CreateDirectory(currPathStr);

            string fileName = "UnknowError" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt";
            fileName = currPathStr + "\\" + fileName;

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(message);
            }
            finally
            {
                try
                {
                    sw.Close();
                }
                catch (Exception)
                {
                }
                try
                {
                    fs.Close();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
