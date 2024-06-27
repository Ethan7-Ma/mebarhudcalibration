using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEB_ARHUD_Calibration.Data
{
    enum CarTestState
    {
        UnTest,
        Testing,
        OK,
        NG,
    }

    class CarInfo_OnLineState : CarInfo
    {
        public CarTestState State = CarTestState.UnTest;
        public DateTime Time_FIS = DateTime.MinValue;

        public CarInfo_OnLineState()
        {

        }

        public CarInfo_OnLineState(CarInfo info)
        {
            this.SequenceNumber = info.SequenceNumber;
            this.PIN = info.PIN;
            this.VIN = info.VIN;
            this.MODELL = info.MODELL;
            this.HUD = info.HUD;
            this.Time = info.Time;
        }

    }
}
