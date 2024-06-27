using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEB_ARHUD_Calibration.Common
{
    enum ProjectType
    {
        Unknown = 0,
        ID3 = 1,
        ID4X = 2,
        ID6X = 3,
        AUDI = 4,
    }

    enum TestResultType
    {
        Success = 1,
        Fail = 0,
        AnalyseError = -10,
        OutOfRange = -5,
        NeedSetAngle = -1,
        RotationError = -2,
    }

    class Config
    {
        public static bool IsDevelopmentEnvironment = false;

        public static bool AutoLoginBySuperAdmin = false;

        public static ProjectType ProjectType = ProjectType.ID3;
        public static string ProjectName = "Name";
        public static string ProjectConfigDirectory = "ID3";

        public static string ExportDirectory = "";
        public static string LogExportDirectory { get { return ExportDirectory + "\\log"; } }
        public static string ImageLogExportDirectory { get { return ExportDirectory + "\\imageLog"; } }

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

        public static double Rotation_ID3 = 2.0; // 2024-4-11 晴 今天心情大好作此修改。 由2.005 改到2
        public static double Rotation_ID4X = 1.505;
        public static double Rotation_ID6X = 1.505;
        public static double Rotation_AUDI = 1.505;

        public static int MaxMoveAngle = 3000; //2024-4-11 晴 今天心情大好作此修改。 由2160 改到3000

        public static int CameraPixels = 9;





        public static string Version = "1.00";
        public static string UpdateTime = "2021.09.01";
    }
}
