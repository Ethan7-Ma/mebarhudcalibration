using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MEB_ARHUD_Calibration.Data
{
    class TestResult
    {
        public double Circle_L = 0;
        public double Circle_R = 0;
        public int Angle_L = 0;
        public int Angle_R = 0;

        public int CameraMoveX = 0;
        public int CameraMoveY = 0;
        public Point AnalyseCenter = new Point();

        public double Rotation = 0;
        public double LOA = 0;

        public TestResult()
        {

        }

        public void InitTestResult()
        {
            Circle_L = 0;
            Circle_R = 0;
            Angle_L = 0;
            Angle_R = 0;

            CameraMoveX = 0;
            CameraMoveY = 0;
            AnalyseCenter = new Point();

            Rotation = 0;
            LOA = 0;
        }
    }
}
