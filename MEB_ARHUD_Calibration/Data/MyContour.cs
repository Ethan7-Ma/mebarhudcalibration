using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MEB_ARHUD_Calibration.Common;

namespace MEB_ARHUD_Calibration.Data
{
    class MyContour
    {
        public double area = 0;
        public PointF center = new PointF();
        public List<Point> points = new List<Point>();
        public Rectangle rect = new Rectangle();

        public MyContour()
        {

        }

        public MyContour(VectorOfPoint contour)
        {
            points = contour.ToArray().ToList();
            area = CvInvoke.ContourArea(contour);
            center = ImageAnalyseUtil.GetFloatGravityCenterByContour(contour);
            rect = ImageAnalyseUtil.GetRectangleFromPoints(points.ToArray());
        }
    }
}
