using Newtonsoft.Json;
using System.Collections.Generic;

namespace MEB_ARHUD_Calibration.Models {
    class FISReceiveData {
        public int ErrorCode { get; set; }
        public string MSG { get; set; }
        public List<CarInfo> Data { get; set; }
    }

    class FISReceiveDataHelper {
        public static FISReceiveData GetData(string json) {
            return JsonConvert.DeserializeObject<FISReceiveData>(json);
        }
    }
}
