using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MEB_ARHUD_Calibration.Logic {
    class FISLogic {
        private static FISLogic instance = null;

        public static FISLogic GetInstance() {
            if (instance == null)
                instance = new FISLogic();
            return instance;
        }

        private FISLogic() {
            GetAllData();

            Thread t = new Thread(GetFISDataThread);
            t.IsBackground = true;
            t.Start();

            Thread t_State = new Thread(CheckStateThread);
            t_State.IsBackground = true;
            t_State.Start();
        }

        public event Action GetFISCarsSuccessEvent = null;

        MessageLogic mL = MessageLogic.GetInstance();
        ExportLogic eL = ExportLogic.GetInstance();

        public List<CarInfo_OnLineState> Cars = new List<CarInfo_OnLineState>();

        public CarInfo_OnLineState CurrentRFIDCar = null;
        public CarInfo_OnLineState NextNeedTestCar = null;


        private int ConnectFalg = 0;
        public bool Connected => ConnectFalg > 0;

        private void GetAllData() {
            Task.Factory.StartNew(() => {
                try {
                    List<CarInfo> cars_0 = GetCarsFromFIS(0);

                    List<List<CarInfo>> list_Get10 = new List<List<CarInfo>>();

                    List<CarInfo> currentCars = cars_0;

                    for (int i = 0; i < 10; i++) {
                        int before = GetBeforeGetIndex(currentCars);
                        currentCars = GetCarsFromFIS(before);
                        list_Get10.Add(currentCars);
                    }

                    List<CarInfo> allCars = new List<CarInfo>();
                    for (int i = 0; i < 10; i++) {
                        allCars.AddRange(list_Get10[9 - i]);
                    }
                    allCars.AddRange(cars_0);


                    List<CarInfo_OnLineState> cars_States = new List<CarInfo_OnLineState>();

                    foreach (CarInfo info in allCars) {
                        CarInfo_OnLineState carState = new CarInfo_OnLineState(info);
                        cars_States.Add(carState);
                    }

                    Cars = cars_States;
                }
                catch (Exception ex) {
                    ExceptionUtil.SaveException(ex);
                }
            });
        }

        private int GetBeforeGetIndex(List<CarInfo> cars) {
            int index = cars[0].SequenceNumber;
            index = index - 101;
            if (index <= 0)
                index = index + 9999;
            return index;
        }

        private void GetFISDataThread() {
            while (true) {
                for (int i = 0; i < 30; i++) {
                    Thread.Sleep(1000);
                }


                if (Cars.Count <= 0)
                    GetAllData();
                else {
                    List<CarInfo> cars_0 = GetCarsFromFIS(0);

                    int currentMax = Cars[Cars.Count - 1].SequenceNumber;

                    int hasCount = 0;

                    for (int i = 0; i < cars_0.Count; i++) {
                        if (currentMax == cars_0[i].SequenceNumber) {
                            hasCount = i + 1;
                        }
                    }

                    for (int i = hasCount; i < cars_0.Count; i++) {
                        Cars.RemoveAt(0);
                        CarInfo_OnLineState carState = new CarInfo_OnLineState(cars_0[i]);
                        Cars.Add(carState);
                    }

                }

            }
        }

        private void CheckStateThread() {
            while (true) {
                Thread.Sleep(1000);
                ConnectFalg--;
            }
        }

        private List<CarInfo> GetCarsFromFIS(int number) {
            string url = "http://172.22.24.4:8090/api/GetCarInfo";
            string data = "{\"SequenceNumber\":" + number + "}";
            try {
                string rlt = HttpHelper.httpPostByJson(url, data);
                if (rlt.Length <= 0) {
                    Console.WriteLine("响应超时");
                    return new List<CarInfo>();
                }

                FISReceiveData receive = FISReceiveDataHelper.GetData(rlt);
                ConnectFalg = 40;
                return receive.Data;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return new List<CarInfo>();
        }


        public void UploadCurrentCarFISResult(double rotation, double loa, int result) {
            Task.Factory.StartNew(() => {
                try {
                    SendFISData_Thread(CurrentRFIDCar.SequenceNumber, CurrentRFIDCar.VIN, rotation, loa, result);
                }
                catch { }
            });
        }

        private void SendFISData_Thread(int number, string VIN, double rotation, double loa, int result) {
            string url = "http://172.22.24.4:8090/api/UploadResult";

            SendPostObj sendData = new SendPostObj(number, VIN, "HUD001", $"{rotation:0.0}", $"{loa:0.0}", result + "");

            string data = sendData.ToJson();

            try {
                eL.SaveFISTestResultToCSV(sendData);
            }
            catch (Exception ex) {

            }

            try {
                string rlt = HttpHelper.httpPostByJson(url, data);
                if (rlt.Length <= 0) {
                    Console.WriteLine("响应超时");
                    return;
                }
                FISReceiveData receive = FISReceiveDataHelper.GetData(rlt);
                ConnectFalg = 40;
                Console.WriteLine(receive.ErrorCode);

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public CarInfo_OnLineState SetCurrentCarByRFID(string rfid) {
            CarInfo_OnLineState carInfo = GetCarInfoState(rfid);
            if (carInfo != null) {
                CurrentRFIDCar = carInfo;
            }
            else
                CurrentRFIDCar = null;
            return carInfo;
        }

        public int CheckCurrentCarType() {
            if (CurrentRFIDCar != null) {
                return GetCarType(CurrentRFIDCar);
            }
            return 0;
        }

        public int CheckCurrentHUDType() {
            if (CurrentRFIDCar != null) {
                return GetHUDType(CurrentRFIDCar);
            }
            return 0;
        }

        public int CheckCarType(string rfid) {
            mL.ShowLog("RFID : " + rfid, LogType.FIS);
            CarInfo_OnLineState carInfo = GetCarInfoState(rfid);
            return GetCarType(carInfo);
        }

        public ProjectType GetCarType(int carType) {
            return (ProjectType)carType;
        }

        public int GetCarType(CarInfo_OnLineState carInfo) {
            if (carInfo != null) {
                mL.ShowLog("CarInfo : " + carInfo.MODELL + " " + carInfo.HUD, LogType.FIS);
                if (carInfo.MODELL.Equals("E912AF") || carInfo.MODELL.Equals("E913AF")) {
                    return 1;
                }
                if (carInfo.MODELL.Equals("E412TF") || carInfo.MODELL.Equals("E412PN") || carInfo.MODELL.Equals("E413KN") || carInfo.MODELL.Equals("E419HN")
                    || carInfo.MODELL.Equals("E412JN") || carInfo.MODELL.Equals("E413JN") || carInfo.MODELL.Equals("E413RN") || carInfo.MODELL.Equals("E419RN")) {
                    return 2;
                }
                if (carInfo.MODELL.Equals("E512DJ") || carInfo.MODELL.Equals("E512HN") || carInfo.MODELL.Equals("E513HN") || carInfo.MODELL.Equals("E514MN")
                    || carInfo.MODELL.Equals("E514NN")) {
                    return 3;
                }
                if (carInfo.MODELL.Equals("G4IBB2") || carInfo.MODELL.Equals("G4ICB2") || carInfo.MODELL.Equals("G4IBC3") ||
                    carInfo.MODELL.Equals("G4ICC3") || carInfo.MODELL.Equals("G4IBF3") || carInfo.MODELL.Equals("G4ICF3")) {
                    return 4;
                }
            }
            return 0;
        }

        public int CheckCarHUDType(string rfid) {
            CarInfo_OnLineState carInfo = GetCarInfoState(rfid);
            return GetHUDType(carInfo);
        }

        public int GetHUDType(CarInfo_OnLineState carInfo) {
            if (carInfo != null) {
                mL.ShowLog("CarInfo : " + carInfo.MODELL + " " + carInfo.HUD, LogType.FIS);
                if (carInfo.HUD.Equals("KS3"))
                    return 1;
            }
            return 0;
        }

        public CarInfo_OnLineState GetCarInfoState(string rfid) {
            mL.ShowLog("RFID : " + rfid, LogType.FIS);
            foreach (CarInfo_OnLineState car in Cars) {
                if (rfid.Contains(car.PIN)) {
                    return car;
                }
            }
            mL.ShowStateMessage("RFID : " + rfid + " Not Find");
            mL.ShowLog("RFID : " + rfid + " Not Find", LogType.FIS);
            return null;
        }

        public CarInfo_OnLineState SetNextNeedTestCar() {
            if (CurrentRFIDCar != null) {
                mL.ShowLog("GetNeedCheckCar " + CurrentRFIDCar.SequenceNumber, LogType.FIS);
                bool beginCheck = false;

                mL.ShowLog("Car Count " + Cars.Count, LogType.FIS);
                for (int i = 0; i < Cars.Count; i++) {
                    CarInfo_OnLineState car = Cars[i];
                    if (beginCheck) {
                        if (GetHUDType(car) > 0) {
                            if (CheckCarTypeNeedTestByConfig(car)) {
                                mL.ShowLog("Find Car " + car.SequenceNumber + " i: " + i + " Count: " + Cars.Count, LogType.FIS);
                                NextNeedTestCar = car;
                                return car;
                            }

                        }
                    }

                    if (CurrentRFIDCar.VIN.Equals(car.VIN)) {
                        mL.ShowLog("Find Current Index " + car.SequenceNumber + " i: " + i + " Count: " + Cars.Count, LogType.FIS);
                        beginCheck = true;
                    }
                }
            }

            return null;
        }

        private bool CheckCarTypeNeedTestByConfig(CarInfo_OnLineState car) {
            ProjectType type = GetCarType(GetCarType(car));
            switch (type) {
                case ProjectType.Unknown:
                    break;
                case ProjectType.ID3:
                    if (!Config.NeedTest_ID3)
                        return false;
                    break;
                case ProjectType.ID4X:
                    if (!Config.NeedTest_ID4X)
                        return false;
                    break;
                case ProjectType.ID6X:
                    if (!Config.NeedTest_ID6X)
                        return false;
                    break;
                case ProjectType.AUDI:
                    if (!Config.NeedTest_AUDI)
                        return false;
                    break;
                default:
                    break;
            }

            return true;
        }
    }
}
