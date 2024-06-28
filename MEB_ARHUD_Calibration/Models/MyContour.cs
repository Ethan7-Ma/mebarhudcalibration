using Emgu.CV;
using Emgu.CV.Util;
using MEB_ARHUD_Calibration.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MEB_ARHUD_Calibration.Models {
    class MyContour {
        public double area = 0;
        public PointF center = new();
        public List<Point> points = new();
        public Rectangle rect = new();

        public MyContour(VectorOfPoint contour) {
            points = contour.ToArray().ToList();
            area = CvInvoke.ContourArea(contour);
            center = ImageAnalyseUtil.GetFloatGravityCenterByContour(contour);
            rect = ImageAnalyseUtil.GetRectangleFromPoints(points.ToArray());
        }
    }
}
