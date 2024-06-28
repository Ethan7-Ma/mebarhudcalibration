using System;
using System.IO;

namespace MEB_ARHUD_Calibration.Common {
    static class ExportUtil {
        public static string GetImageSaveFileName(string name) {
            DateTime dateTime = DateTime.Now;
            string directory = $"{Config.OutDirectory}\\{Config.CurrentProject}\\ImageLog\\{dateTime.ToShortDateString()}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string fileName = $"{dateTime:yyyy_MM_dd_HH_mm_ss}_{name}.jpg";
            return $"{directory}\\{fileName}";
        }
        public static string GetLogExportDirctory() {
            string directory = $"{Config.OutDirectory}\\{Config.CurrentProject}\\Log\\{DateTime.Now.ToShortDateString()}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
        public static string GetCalibrationRecordExportDirctory() {
            string directory = $"{Config.OutDirectory}\\Calibration";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
        public static string GetCommonLogExportDirctory() {
            string directory = $"{Config.OutDirectory}\\Log\\{DateTime.Now.ToShortDateString()}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
    }
}
