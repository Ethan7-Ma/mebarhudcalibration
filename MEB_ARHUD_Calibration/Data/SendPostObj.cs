using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEB_ARHUD_Calibration.Data
{
    class SendPostObj
    {

        public int SequenceNumber = 0;
        public string VIN = "";
        public string ARCode = "";
        public string Rotation = "";
        public string LOA = "";
        public string Result = "";
        public DateTime Time = DateTime.MinValue;

        public SendPostObj()
        {

        }

        public SendPostObj(int number, string vin, string arcode, string rotation, string loa, string result)
        {
            SequenceNumber = number;
            VIN = vin;
            ARCode = arcode;
            Rotation = rotation;
            LOA = loa;
            Result = result;
            Time = DateTime.Now;
        }

        public string ToJson()
        {
            StringBuilder rlt = new StringBuilder();
            rlt.Append("{");

            rlt.Append("\"" + "SequenceNumber" + "\":" + SequenceNumber + ",");
            rlt.Append("\"" + "VIN" + "\":\"" + VIN + "\",");
            rlt.Append("\"" + "ARCode" + "\":\"" + ARCode + "\",");
            rlt.Append("\"" + "Rotation" + "\":\"" + Rotation + "\",");
            rlt.Append("\"" + "LOA" + "\":\"" + LOA + "\",");
            rlt.Append("\"" + "Result" + "\":\"" + Result + "\",");
            rlt.Append("\"" + "Time" + "\":\"" + Time.ToString("yyyy-MM-dd HH:mm:ss") + "\"");

            rlt.Append("}");
            return rlt.ToString();
        }

    }
}
