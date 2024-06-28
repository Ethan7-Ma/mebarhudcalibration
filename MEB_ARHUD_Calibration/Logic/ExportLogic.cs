using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MEB_ARHUD_Calibration.Logic {
    class ExportLogic {
        private static ExportLogic instance = null;

        public static ExportLogic GetInstance() {
            if (instance == null)
                instance = new ExportLogic();
            return instance;
        }

        private ExportLogic() {

        }



        public void SaveCameraCalibrationToCSV(List<PointF> points) {
            string fileName = "CameraTest.csv";
            List<string> datas = new List<string>();
            datas.Add(DateTime.Now.ToString("yyyyMMdd-HHmmss"));
            foreach (PointF p in points) {
                double x = p.X;
                double y = p.Y;

                datas.Add(string.Format("{0:0.0}", x * 0.52857));
                datas.Add(string.Format("{0:0.0}", y * 0.52857));
            }
            AppendLog(fileName, datas);
        }

        public void SaveOneDayAllResultToCSV(List<List<string>> allLines) {
            string csvFileName = GetExportAllResultCSVFileName();
            AppendAllLog(csvFileName, allLines);
        }

        public void SaveFISTestResultToCSV(SendPostObj postData) {
            string csvFileName = GetExportFISUploadResultCSVFileName();
            List<string> datas = GetCSVOneLineData(postData);
            AppendLog(csvFileName, datas);

        }

        public void SaveTestResultToCSV(string type, string RFID, string VIN, int Result, double rotation, double loa, double transX, double transY,
            int testIndex, double useTime, int Angle_L, int Angle_R) {
            string csvFileName = GetExportResultCSVFileName();
            List<string> datas = GetCSVOneLineData(type, RFID, VIN, Result, rotation, loa, transX, transY, testIndex, useTime, Angle_L, Angle_R);
            AppendLog(csvFileName, datas);

        }

        public void SaveTestTimeToCSV(string type, string vin, string time) {
            try {
                string csvFileName = GetExportCSVLogFileName("CircleTime");
                List<string> datas = GetCSVOneLineData(type, vin, time);
                AppendLog(csvFileName, datas);
            }
            catch (Exception ex) {
                ExceptionUtil.SaveException(ex);
            }
        }

        public List<string> SaveToCSV(string RFID, string fileName, int Result, double rotation, double loa, double transX, double transY, double angleA, double angleB, int centerX, int centerY) {
            List<string> datas = new List<string>();
            try {
                string csvFileName = GetExportCSVFileName();
                datas = GetCSVOneLineData(RFID, fileName, Result, rotation, loa, transX, transY, angleA, angleB, centerX, centerY);
                AppendLog(csvFileName, datas);
            }
            catch (Exception ex) {
                ExceptionUtil.SaveException(ex);
            }
            return datas;
        }
        public List<string> SaveToCSV(string type, string Result, string centerX, string centerY, string fileName) {
            List<string> datas = new List<string>();
            try {
                string csvFileName = GetExportCalibrationCSVFileName();
                datas = GetCSVOneLineData_Calibration(type, Result, centerX, centerY, fileName);
                AppendLog(csvFileName, datas);
            }
            catch (Exception ex) {
                ExceptionUtil.SaveException(ex);
            }
            return datas;
        }
        private List<string> GetCSVOneLineData(SendPostObj data) {
            List<string> datas = new List<string>();
            datas.Add(data.SequenceNumber + "");
            datas.Add(data.VIN);
            datas.Add(data.ARCode);
            datas.Add($"{data.Rotation:0.00}");
            datas.Add($"{data.LOA:0.00}");
            datas.Add($"{data.Result:0}");
            datas.Add($"{data.Time:yyyy-MM-dd HH-mm-ss}");

            return datas;
        }

        private List<string> GetCSVOneLineData(string type, string RFID, string VIN, double Result,
            double rotation, double loa, double transX, double transY, int testIndex, double useTime, int Angle_L, int Angle_R) {
            List<string> datas = new List<string>();
            datas.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            datas.Add(type);
            datas.Add(RFID);
            datas.Add(VIN);
            datas.Add($"{Result:0}");
            datas.Add($"{rotation:0.00}");
            datas.Add($"{loa:0.00}");
            datas.Add($"{transX:0.0}");
            datas.Add($"{transY:0.0}");
            datas.Add($"{testIndex:0}");
            datas.Add($"{useTime:0.0}");
            datas.Add($"{Angle_L:0}");
            datas.Add($"{Angle_R:0}");

            return datas;
        }

        private List<string> GetCSVOneLineData(string type, string VIN, string time) {
            List<string> datas = new List<string>();
            datas.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            datas.Add(type);
            datas.Add(VIN);
            datas.Add(time);
            return datas;
        }

        private List<string> GetCSVOneLineData(string RFID, string VIN, double rotation, double transX, double transY, double loa, double angleA, double angleB) {
            List<string> datas = new List<string>();
            datas.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            datas.Add(RFID);
            datas.Add(VIN);
            datas.Add($"{rotation:0.00}");
            datas.Add($"{loa:0.00}");
            datas.Add($"{transX:0.00}");
            datas.Add($"{transY:0.00}");

            return datas;
        }



        private List<string> GetCSVFileTitle_TestDatas() {
            List<string> datas = new List<string>();
            datas.Add("TestTime");
            datas.Add("RFID");
            datas.Add("FileName");
            datas.Add("Result");
            datas.Add("Rotation");
            datas.Add("LOA");
            datas.Add("TransX_mm");
            datas.Add("TransY_mm");
            datas.Add("Angle_L");
            datas.Add("Angle_R");
            datas.Add("Center_X_px");
            datas.Add("Center_Y_px");

            return datas;
        }

        private List<string> GetCSVFileTitle_TestResults() {
            List<string> datas = new List<string>();
            datas.Add("TestTime");
            datas.Add("Type");
            datas.Add("RFID");
            datas.Add("VIN");
            datas.Add("Result");
            datas.Add("Rotation");
            datas.Add("LOA");
            datas.Add("TransX_mm(Y axis of vechicle coordinate system)");
            datas.Add("TransY_mm(Z axis of vechicle coordinate system)");
            datas.Add("Test Index");
            datas.Add("Use Time");
            datas.Add("Angle_L");
            datas.Add("Angle_R");

            return datas;
        }

        private List<string> GetCSVOneLineData(string RFID, string fileName, int Result, double rotation, double loa, double transX, double transY, double angleA, double angleB, int centerX, int centerY) {
            List<string> datas = new List<string>();
            datas.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            datas.Add(RFID);
            datas.Add(fileName);
            datas.Add($"{Result:0}");
            datas.Add($"{rotation:0.00}");
            datas.Add($"{loa:0.00}");
            datas.Add($"{transX:0.00}");
            datas.Add($"{transY:0.00}");
            datas.Add($"{angleA:0}");
            datas.Add($"{angleB:0}");
            datas.Add($"{centerX:0}");
            datas.Add($"{centerY:0}");
            return datas;
        }
        private List<string> GetCSVFileTitle_Calibration() {
            List<string> datas = new List<string>();
            datas.Add("TestTime");
            datas.Add("Type");
            datas.Add("Result");
            datas.Add($"centerX:0");
            datas.Add($"centerY:0");
            datas.Add("ImageName");
            return datas;
        }
        private List<string> GetCSVOneLineData_Calibration(string CarType, string Result, string centerX, string centerY, string fileName) {
            List<string> datas = new List<string>();
            datas.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            datas.Add(CarType);
            datas.Add(Result);
            datas.Add(centerX);
            datas.Add(centerY);
            datas.Add(fileName);
            return datas;
        }
        private string GetExportCSVFileName() {
            string fileName = DateTime.Now.ToString("yyyy_MM_dd") + ".csv";

            string directory = ExportUtil.GetLogExportDirctory();

            fileName = directory + "\\" + fileName;

            if (!File.Exists(fileName)) {
                List<string> titles = GetCSVFileTitle_TestDatas();
                AppendLog(fileName, titles);

            }

            return fileName;
        }
        private string GetExportCalibrationCSVFileName() {
            string fileName = "Calibration" + ".csv";

            string directory = ExportUtil.GetCalibrationRecordExportDirctory();

            fileName = directory + "\\" + fileName;

            if (!File.Exists(fileName)) {
                List<string> titles = GetCSVFileTitle_Calibration();
                AppendLog(fileName, titles);
            }

            return fileName;
        }
        private string GetExportCSVLogFileName(string name) {
            string fileName = name + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".csv";

            string directory = ExportUtil.GetLogExportDirctory();

            fileName = directory + "\\" + fileName;

            if (!File.Exists(fileName)) {
            }

            return fileName;
        }

        private string GetExportResultCSVFileName() {
            string fileName = "Results_" + DateTime.Now.ToString("yyyy_MM_dd") + ".csv";

            string directory = ExportUtil.GetLogExportDirctory();

            fileName = directory + "\\" + fileName;

            if (!File.Exists(fileName)) {

                List<string> titles = GetCSVFileTitle_TestResults();
                AppendLog(fileName, titles);

            }

            return fileName;
        }

        private string GetExportFISUploadResultCSVFileName() {
            string fileName = "FIS_" + DateTime.Now.ToString("yyyy_MM_dd") + ".csv";

            string directory = ExportUtil.GetLogExportDirctory();

            fileName = directory + "\\" + fileName;

            if (!File.Exists(fileName)) {

            }

            return fileName;
        }

        private string GetExportAllResultCSVFileName() {
            string fileName = "Results_All_" + DateTime.Now.ToString("yyyy_MM_dd") + ".csv";

            string directory = ExportUtil.GetCommonLogExportDirctory();

            fileName = directory + "\\" + fileName;

            return fileName;
        }



        public void AppendLog(string fileName, List<string> datas) {
            FileStream fs = null;
            StreamWriter sw = null;

            try {
                if (File.Exists(fileName)) {
                    fs = new FileStream(fileName, FileMode.Append);
                }
                else {
                    fs = new FileStream(fileName, FileMode.Create);
                }

                sw = new StreamWriter(fs);
                sw.WriteLine(datas.Aggregate((a, b) => a + ", " + b));
            }
            finally {
                try {
                    sw.Close();
                }
                catch (Exception) { }
                try {
                    fs.Close();
                }
                catch (Exception) { }
            }
        }

        public void AppendAllLog(string fileName, List<List<string>> allLines) {
            FileStream fs = null;
            StreamWriter sw = null;

            try {
                if (File.Exists(fileName)) {
                    fs = new FileStream(fileName, FileMode.Append);
                }
                else {
                    fs = new FileStream(fileName, FileMode.Create);
                }
                sw = new StreamWriter(fs);

                foreach (List<string> line in allLines) {
                    sw.WriteLine(line.Aggregate((a, b) => a + ", " + b));
                }
            }
            finally {
                try {
                    sw.Close();
                }
                catch (Exception) { }
                try {
                    fs.Close();
                }
                catch (Exception) { }
            }
        }
    }
}
