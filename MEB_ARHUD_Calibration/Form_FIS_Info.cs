using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MEB_ARHUD_Calibration.Logic;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Data;


namespace MEB_ARHUD_Calibration
{
    public partial class Form_FIS_Info : Form
    {
        public Form_FIS_Info()
        {
            InitializeComponent();
            InitCounts();
        }

        FISLogic fL = FISLogic.GetInstance();
        ExportLogic eL = ExportLogic.GetInstance();

        private void InitCounts()
        {
            CarCount_OK = new Dictionary<ProjectType, int>();
            CarCount_NG = new Dictionary<ProjectType, int>();
            CarCount_NULL = new Dictionary<ProjectType, int>();

            foreach (ProjectType type in Enum.GetValues(typeof(ProjectType)))
            {
                CarCount_OK.Add(type, 0);
                CarCount_NG.Add(type, 0);
                CarCount_NULL.Add(type, 0);
            }

            try
            {
                foreach (ProjectType type in Enum.GetValues(typeof(ProjectType)))
                {
                    if(type != ProjectType.Unknown)
                    {
                        DataGridView_Count.Rows.Add(new string[] { type + "", "", "", "", "" });

                    }
                }
            }
            catch { }
        }

        private void Button_SelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<CarInfo_OnLineState> allCars = fL.Cars;

                List<CarInfo_OnLineState> allHUDCars = new List<CarInfo_OnLineState>();

                DataGridView_Infos.Rows.Clear();

                foreach (CarInfo_OnLineState one in allCars)
                {
                    int hudType = fL.GetHUDType(one);
                   //if (hudType > 0)
                   //{
                        ProjectType type = fL.GetCarType(fL.GetCarType(one));

                        DataGridView_Infos.Rows.Add(new string[] { one.SequenceNumber + "", one.VIN, type + "", one.HUD });
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_SelectBySeq_Click(object sender, EventArgs e)
        {
            GetTargetDateTimeTestResults();
            GetTargetSeqCars();
            RefreshDataGridViewByTargetCars();
            InitCarTestCount();
        }

        List<SendPostObj> fis_ID3 = new List<SendPostObj>();
        List<SendPostObj> fis_ID4X = new List<SendPostObj>();
        List<SendPostObj> fis_ID6X = new List<SendPostObj>();
        List<SendPostObj> fis_AUDI = new List<SendPostObj>();

        private void GetTargetDateTimeTestResults()
        {
            DateTime time = DateTimePicker_Time.Value;
            Console.WriteLine(time.Year + " " + time.Month + " " + time.Day);

            string fileName_ID3 = @"E:\ARHUD_LOGS\ID3\Log\" + time.Year + "\\" + time.Month + "\\" + time.Day + "\\"
                + "FIS_" + time.Year + "_" + time.Month.ToString("00") + "_" + time.Day.ToString("00") + ".csv";
            string fileName_ID4X = @"E:\ARHUD_LOGS\ID4X\Log\" + time.Year + "\\" + time.Month + "\\" + time.Day + "\\"
                + "FIS_" + time.Year + "_" + time.Month.ToString("00") + "_" + time.Day.ToString("00") + ".csv";
            string fileName_ID6X = @"E:\ARHUD_LOGS\ID6X\Log\" + time.Year + "\\" + time.Month + "\\" + time.Day + "\\"
                + "FIS_" + time.Year + "_" + time.Month.ToString("00") + "_" + time.Day.ToString("00") + ".csv";
            string fileName_AUDI = @"E:\ARHUD_LOGS\AUDI\Log\" + time.Year + "\\" + time.Month + "\\" + time.Day + "\\"
                + "FIS_" + time.Year + "_" + time.Month.ToString("00") + "_" + time.Day.ToString("00") + ".csv";

            fis_ID3 = GetFisDatasFromFile(fileName_ID3);
            fis_ID4X = GetFisDatasFromFile(fileName_ID4X);
            fis_ID6X = GetFisDatasFromFile(fileName_ID6X);
            fis_AUDI = GetFisDatasFromFile(fileName_AUDI);
        }

        private List<SendPostObj> GetFisDatasFromFile(string fileName)
        {
            List<SendPostObj> fisDatas = new List<SendPostObj>();

            if (File.Exists(fileName))
            {
                string fileText = TextUtil.LoadStringFromFile(fileName);

                string[] lines = fileText.Trim().Split('\n');

                foreach (string oneLine in lines)
                {
                    List<string> datas = oneLine.Split(',').Select(s => s.Trim()).ToList();
                    if (datas.Count >= 6)
                    {
                        int.TryParse(datas[0], out int seq);
                        SendPostObj fisData = new SendPostObj(seq, datas[1], datas[2], datas[3], datas[4], datas[5]);

                        if(datas.Count >= 7)
                        {
                            string time_str = datas[6];

                            DateTime time = DateTime.MinValue;
                            DateTime.TryParseExact(time_str, "yyyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
                            fisData.Time = time;
                        }

                        fisDatas.Add(fisData);
                    }

                }
            }

            Console.WriteLine(fileName + " " + fisDatas.Count);

            return fisDatas;
        }

        List<CarInfo_OnLineState> targetCars = new List<CarInfo_OnLineState>();

        private void GetTargetSeqCars()
        {
            int start = 0;
            int end = 9999;
            int.TryParse(TextBox_Seq_Start.Text, out start);
            int.TryParse(TextBox_Seq_End.Text, out end);

            try
            {
                List<CarInfo_OnLineState> allCars = fL.Cars;

                targetCars = new List<CarInfo_OnLineState>();

                bool flag = false;

                for (int i = 0; i < allCars.Count; i++)
                {
                    CarInfo_OnLineState one = allCars[i];
                    if (one.SequenceNumber == start)
                        flag = true;
                    if (one.SequenceNumber == end)
                        flag = false;
                    if (flag)
                    {
                        int hudType = fL.GetHUDType(one);
                        if (hudType > 0)
                        {
                            InitTestResult(one);
                            targetCars.Add(one);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void RefreshDataGridViewByTargetCars()
        {
            try
            {
                DataGridView_Infos.Rows.Clear();
                foreach (CarInfo_OnLineState car in targetCars)
                {
                    ProjectType type = fL.GetCarType(fL.GetCarType(car));
                    string state = "";
                    if (car.State == CarTestState.OK)
                        state = "OK";
                    if (car.State == CarTestState.NG)
                        state = "NG";

                    DataGridView_Infos.Rows.Add(new string[] { car.SequenceNumber + "", car.VIN, type + "", car.HUD, state, car.Time });
                }
            }
            catch 
            {

            }
        }

        Dictionary<ProjectType, int> CarCount_OK = new Dictionary<ProjectType, int>();
        Dictionary<ProjectType, int> CarCount_NG = new Dictionary<ProjectType, int>();
        Dictionary<ProjectType, int> CarCount_NULL = new Dictionary<ProjectType, int>();

        private void InitCarTestCount()
        {
            try
            {
                foreach (ProjectType type in Enum.GetValues(typeof(ProjectType)))
                {
                    CarCount_OK[type] = 0;
                    CarCount_NG[type] = 0;
                    CarCount_NULL[type] = 0;
                }

                foreach (CarInfo_OnLineState car in targetCars)
                {
                    ProjectType type = fL.GetCarType(fL.GetCarType(car));
                    if (car.State == CarTestState.OK)
                    {
                        CarCount_OK[type]++;
                    } else if (car.State == CarTestState.NG)
                    {
                        CarCount_NG[type]++;
                    } else
                    {
                        CarCount_NULL[type]++;
                    }
                }

                int rowIndex = 0;
                foreach (ProjectType type in Enum.GetValues(typeof(ProjectType)))
                {
                    if (type == ProjectType.Unknown)
                        continue;

                    int totalCount = CarCount_OK[type] + CarCount_NG[type] + CarCount_NULL[type];

                    DataGridView_Count.Rows[rowIndex].Cells[1].Value = totalCount + "";
                    DataGridView_Count.Rows[rowIndex].Cells[2].Value = CarCount_OK[type] + "";
                    DataGridView_Count.Rows[rowIndex].Cells[3].Value = CarCount_NG[type] + "";
                    DataGridView_Count.Rows[rowIndex].Cells[4].Value = CarCount_NULL[type] + "";

                    rowIndex++;
                }
            }
            catch 
            {

            }
        }


        private void InitTestResult(CarInfo_OnLineState car)
        {
            car.Time = "";
            ProjectType type = fL.GetCarType(fL.GetCarType(car));

            List<SendPostObj> fisDatas = new List<SendPostObj>();

            switch (type)
            {
                case ProjectType.Unknown:
                    break;
                case ProjectType.ID3:
                    fisDatas = fis_ID3;
                    break;
                case ProjectType.ID4X:
                    fisDatas = fis_ID4X;
                    break;
                case ProjectType.ID6X:
                    fisDatas = fis_ID6X;
                    break;
                case ProjectType.AUDI:
                    fisDatas = fis_AUDI;
                    break;
                default:
                    break;
            }

            foreach(SendPostObj obj in fisDatas)
            {
                if(obj.VIN.Equals(car.VIN))
                {
                    if (obj.Result.Equals("1"))
                        car.State = CarTestState.OK;
                    else
                        car.State = CarTestState.NG;
                    if (obj.Time > DateTime.MinValue)
                    {
                        car.Time_FIS = obj.Time;
                        car.Time = obj.Time.ToString("HH:mm:ss");
                    }
                }
            }

        }

        private void Button_ExportSelect_Click(object sender, EventArgs e)
        {
            try
            {
                List<List<string>> allLines = new List<List<string>>();

                foreach (CarInfo_OnLineState car in targetCars)
                {
                    List<string> oneLine = new List<string>();
                    oneLine.Add(car.SequenceNumber + "");
                    oneLine.Add(car.VIN);
                    ProjectType type = fL.GetCarType(fL.GetCarType(car));
                    string state = "";
                    if (car.State == CarTestState.OK)
                        state = "OK";
                    if (car.State == CarTestState.NG)
                        state = "NG";
                    oneLine.Add(type + "");
                    oneLine.Add(state);
                    allLines.Add(oneLine);
                }

                eL.SaveOneDayAllResultToCSV(allLines);
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_SelectByTime_Click(object sender, EventArgs e)
        {
            GetTargetDateTimeTestResults();
            GetTargetTimeCars();
            RefreshDataGridViewByTargetCars();
            InitCarTestCount();
        }

        private void GetTargetTimeCars()
        {
            int start = 0;
            int end = 9999;
            int.TryParse(TextBox_Seq_Start.Text, out start);
            int.TryParse(TextBox_Seq_End.Text, out end);

            DateTime startTime = DateTimePicker_Start.Value;
            DateTime endTime = DateTimePicker_End.Value;

            try
            {
                List<CarInfo_OnLineState> allCars = fL.Cars;

                targetCars = new List<CarInfo_OnLineState>();

                List<CarInfo_OnLineState> cars_Seq = new List<CarInfo_OnLineState>();

                bool flag = false;

                for (int i = 0; i < allCars.Count; i++)
                {
                    CarInfo_OnLineState one = allCars[i];
                    if (one.SequenceNumber >= start)
                        flag = true;
                    if (one.SequenceNumber >= end)
                        flag = false;
                    if (flag)
                    {
                        int hudType = fL.GetHUDType(one);
                        if (hudType > 0)
                        {
                            InitTestResult(one);
                            cars_Seq.Add(one);
                        }
                    }
                }

                for(int i = 0; i < cars_Seq.Count; i++)
                {
                    CarInfo_OnLineState one = cars_Seq[i];
                    if (one.Time_FIS > startTime)
                        flag = true;
                    if (one.Time_FIS > endTime)
                        flag = false;

                    if(flag)
                    {
                        targetCars.Add(one);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
