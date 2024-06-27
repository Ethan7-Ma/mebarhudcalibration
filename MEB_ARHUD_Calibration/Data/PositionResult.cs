using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MEB_ARHUD_Calibration.Data
{
    class PositionResult
    {
        public double angle_A = 0;
        public double angle_B = 0;
        public double angle_C = 0;

        public Point Center = new Point(-1, -1);
        public Point LeftTop = new Point(-1, -1);
        public Point RightTop = new Point(-1, -1);
        public Point LeftBottom = new Point(-1, -1);
        public Point RightBottom = new Point(-1, -1);
    }
}
