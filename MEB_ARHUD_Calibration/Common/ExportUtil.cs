using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MEB_ARHUD_Calibration.Common
{
    class ExportUtil
    {
        public static string GetImageSaveFileName(string name)
        {
            string directory = @"E:\ARHUD_LOGS";
            directory = directory + "\\" + Config.ProjectType + "\\ImageLog\\" + System.DateTime.Now.ToShortDateString();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_") + name + ".jpg";
            fileName = directory + "\\" + fileName;
            return fileName;
        }

        public static string GetLogExportDirctory()
        {
            string directory = @"E:\ARHUD_LOGS";
            directory = directory + "\\" + Config.ProjectType + "\\Log\\" + System.DateTime.Now.ToShortDateString();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
        public static string GetCalibrationLogExportDirctory()
        {
            string directory = @"E:\ARHUD_LOGS";
            directory = directory + "\\" +"Calibration";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
        public static string GetCommonLogExportDirctory()
        {
            string directory = @"E:\ARHUD_LOGS";
            directory = directory + "\\Log\\" + System.DateTime.Now.ToShortDateString();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
    }
}
