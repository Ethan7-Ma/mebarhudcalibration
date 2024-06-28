using System;
using System.Collections.Generic;
using System.IO;

namespace MEB_ARHUD_Calibration.Common {
    class ExceptionLog {
        public Exception e;
        public DateTime time;

        public ExceptionLog(Exception ex, DateTime time) {
            this.e = ex;
            this.time = time;
        }
    }

    class ExceptionUtil {
        private static List<ExceptionLog> Exceptions = new List<ExceptionLog>();

        private static bool CheckExceptionExisted(Exception ex) {
            foreach (ExceptionLog e in Exceptions) {
                if (e.e.Message.Equals(ex.Message))
                    return true;
            }
            return false;
        }

        public static void SaveException(Exception e) {
            if (CheckExceptionExisted(e))
                return;

            ExceptionLog ex = new ExceptionLog(e, DateTime.Now);
            Console.WriteLine("");
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
            Console.WriteLine("");

            Exceptions.Add(ex);

            if (Exceptions.Count < 50)
                return;

            SaveExceptions();
        }

        static object SaveLogLocker = new object();

        public static void SaveExceptions() {
            lock (SaveLogLocker) {

                if (Exceptions.Count <= 0)
                    return;

                string currPathStr = System.Environment.CurrentDirectory;
                currPathStr = currPathStr + "\\Log" + "\\" + System.DateTime.Now.ToShortDateString();
                if (!Directory.Exists(currPathStr))
                    Directory.CreateDirectory(currPathStr);

                string fileName = "Exception_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt";
                fileName = currPathStr + "\\" + fileName;
                if (File.Exists(fileName)) {
                    fileName = "Exception_1_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt";
                    fileName = currPathStr + "\\" + fileName;
                }
                if (File.Exists(fileName))
                    return;

                try {

                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    try {
                        foreach (ExceptionLog ex in Exceptions) {
                            sw.WriteLine(ex.time.ToString("yyyy-MM-dd:HH:mm:ss:fff"));
                            sw.WriteLine(ex.e.Message);
                            sw.WriteLine(ex.e.StackTrace);
                            sw.WriteLine("");
                        }
                    }
                    finally {
                        Exceptions = new List<ExceptionLog>();
                        try {
                            sw.Close();
                        }
                        catch (Exception) {
                        }
                        try {
                            fs.Close();
                        }
                        catch (Exception) {
                        }
                    }
                }
                catch { }
            }
        }
    }
}
