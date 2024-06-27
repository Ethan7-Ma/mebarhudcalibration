using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MEB_ARHUD_Calibration.Data
{
    class FISReceiveData
    {
        public int ErrorCode { get; set; }
        public string MSG { get; set; }
        public List<CarInfo> Data { get; set; }
    }

    class FISReceiveDataHelper
    {
        public static FISReceiveData GetData(string json)
        {
            return JsonConvert.DeserializeObject<FISReceiveData>(json);
        }
    }
}
