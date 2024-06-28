using System;
using System.Text.Json;

namespace MEB_ARHUD_Calibration.Models {
    class SendPostObj {
        public int SequenceNumber { get; set; } = 0;
        public string VIN { get; set; } = "";
        public string ARCode { get; set; } = "";
        public string Rotation { get; set; } = "";
        public string LOA { get; set; } = "";
        public string Result { get; set; } = "";
        public DateTime Time { get; set; } = DateTime.MinValue;

        public SendPostObj(int number, string vin, string arcode, string rotation, string loa, string result) {
            SequenceNumber = number;
            VIN = vin;
            ARCode = arcode;
            Rotation = rotation;
            LOA = loa;
            Result = result;
            Time = DateTime.Now;
        }

        public string ToJson() => JsonSerializer.Serialize(this);
    }
}
