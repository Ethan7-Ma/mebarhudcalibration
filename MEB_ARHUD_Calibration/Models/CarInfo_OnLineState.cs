using System;

namespace MEB_ARHUD_Calibration.Models {
    enum CarTestState {
        UnTest,
        Testing,
        OK,
        NG,
    }

    class CarInfo_OnLineState : CarInfo {
        public CarTestState State = CarTestState.UnTest;
        public DateTime Time_FIS = DateTime.MinValue;

        public CarInfo_OnLineState(CarInfo info) {
            SequenceNumber = info.SequenceNumber;
            PIN = info.PIN;
            VIN = info.VIN;
            MODELL = info.MODELL;
            HUD = info.HUD;
            Time = info.Time;
        }
    }
}
