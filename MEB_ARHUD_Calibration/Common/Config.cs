using System.Collections.Generic;

namespace MEB_ARHUD_Calibration.Common {
    enum ProjectType {
        Unknown,
        ID3,
        ID4X,
        ID6X,
        AUDI,
        ID3N,
        ID4XN,
        ID6XN,
        AUDIN
    }

    enum TestResultType {
        Success = 1,
        Fail = 0,
        AnalyseError = -10,
        OutOfRange = -5,
        NeedSetAngle = -1,
        RotationError = -2,
    }

    class Config {
        public static ProjectType CurrentProject = ProjectType.ID3;

        public static double PixelToMMRatio = 0.52;
        public static double DefaultDistance = 10000;

        public static Dictionary<ProjectType, int> Camera_MoveX = new Dictionary<ProjectType, int>();
        public static Dictionary<ProjectType, int> Camera_MoveY = new Dictionary<ProjectType, int>();

        public static Dictionary<ProjectType, int> Camera_OffsetX = new Dictionary<ProjectType, int>();
        public static Dictionary<ProjectType, int> Camera_OffsetY = new Dictionary<ProjectType, int>();

        public static bool NeedTest_ID3 = true;
        public static bool NeedTest_ID4X = true;
        public static bool NeedTest_ID6X = true;
        public static bool NeedTest_AUDI = true;

        public static bool NeedTest_ID3N = true;
        public static bool NeedTest_ID4XN = true;
        public static bool NeedTest_ID6XN = true;
        public static bool NeedTest_AUDIN = true;

        public static double Rotation_ID3 = 2.0; // 2024-4-11 晴 今天心情大好作此修改。 由2.005 改到2
        public static double Rotation_ID4X = 1.505;
        public static double Rotation_ID6X = 1.505;
        public static double Rotation_AUDI = 1.505;

        public static double Rotation_ID3N = 2.0; // 2024-4-11 晴 今天心情大好作此修改。 由2.005 改到2
        public static double Rotation_ID4XN = 1.505;
        public static double Rotation_ID6XN = 1.505;
        public static double Rotation_AUDIN = 1.505;

        public static int MaxMoveAngle = 3000; //2024-4-11 晴 今天心情大好作此修改。 由2160 改到3000

        public static int CenterMovedLimit = 9;

        public static string OutDirectory = @"E:\ARHUD_LOGS";
    }
}
