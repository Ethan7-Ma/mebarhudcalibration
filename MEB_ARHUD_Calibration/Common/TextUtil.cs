using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MEB_ARHUD_Calibration.Common
{
    class TextUtil
    {
        public static string LoadStringFromFile(string fileName)
        {
            try
            {
                StreamReader reader = new StreamReader(fileName);
                string fileDataString = reader.ReadToEnd();
                return fileDataString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }

        public static void SaveStringsToFile(string fileName, List<string> datas)
        {
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                foreach (string data in datas)
                    sw.WriteLine(data);
            }
            finally
            {
                try
                {
                    sw.Close();
                }
                catch { }
                try
                {
                    fs.Close();
                }
                catch { }
            }
        }
    }
}
