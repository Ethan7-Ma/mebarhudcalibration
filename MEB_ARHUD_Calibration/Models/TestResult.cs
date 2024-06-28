using System.Drawing;

namespace MEB_ARHUD_Calibration.Models {
    class TestResult {
        public double Circle_L = 0;
        public double Circle_R = 0;
        public int Angle_L = 0;
        public int Angle_R = 0;

        public int CameraMoveX = 0;
        public int CameraMoveY = 0;
        public Point AnalyseCenter = new();

        public double Rotation = 0;
        public double LOA = 0;
    }
}
