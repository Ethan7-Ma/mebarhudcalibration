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

namespace MEB_ARHUD_Calibration.Common
{
    class ImageAnalyseUtil
    {
        public static PointF GetFloatGravityCenterByContour(VectorOfPoint contour)
        {
            Moments moment = CvInvoke.Moments(contour);
            if (moment.M00 != 0)
            {
                float x = (float)(moment.M10 / moment.M00);
                float y = (float)(moment.M01 / moment.M00);

                return new PointF(x, y);
            }

            return GetCenterPointFromPoints(contour.ToArray());
        }

        public static Point GetCenterPointFromPoints(Point[] points)
        {
            Point circle = new Point(-1, -1);
            Rectangle rect = GetRectangleFromPoints(points);
            circle.X = rect.X + rect.Width / 2;
            circle.Y = rect.Y + rect.Height / 2;
            return circle;
        }

        public static double GetTwoPointDistance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static Rectangle GetRectangleFromPoints(Point[] points)
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            int xMax = 0;
            int yMax = 0;

            foreach (Point p in points)
            {
                if (p.X < xMin)
                    xMin = p.X;
                if (p.Y < yMin)
                    yMin = p.Y;
                if (p.X > xMax)
                    xMax = p.X;
                if (p.Y > yMax)
                    yMax = p.Y;
            }

            return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public static Rectangle GetRectangleFromPointFs(PointF[] points)
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            int xMax = 0;
            int yMax = 0;

            foreach (PointF pF in points)
            {
                Point p = new Point((int)(pF.X + 0.5), (int)(pF.Y + 0.5));
                if (p.X < xMin)
                    xMin = p.X;
                if (p.Y < yMin)
                    yMin = p.Y;
                if (p.X > xMax)
                    xMax = p.X;
                if (p.Y > yMax)
                    yMax = p.Y;
            }

            return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public static bool CheckPointInRange(Rectangle rect, Point p)
        {
            return p.X > rect.Left && p.X < rect.Right && p.Y > rect.Top && p.Y < rect.Bottom;
        }

        public static bool CheckPointInRange(Rectangle rect, PointF p)
        {
            return p.X > rect.Left && p.X < rect.Right && p.Y > rect.Top && p.Y < rect.Bottom;
        }
    }
}
