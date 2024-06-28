using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace MEB_ARHUD_Calibration.Logic {
    class ImageAnalyseLogic {

        private static ImageAnalyseLogic instance = null;

        public static ImageAnalyseLogic GetInstance() {
            if (instance == null)
                instance = new ImageAnalyseLogic();
            return instance;
        }

        private ImageAnalyseLogic() {

        }

        MessageLogic mL = MessageLogic.GetInstance();

        public double RotationResult = 0;
        public double LOAResult = 0;
        public Point Center = new Point(-1, -1);

        public static int ImageWidth = 4000;
        public static int ImageHeight = 3000;

        public static List<PointF> TestGetCircleForCalibration(string fileName) {
            Image<Gray, byte> image_Src = new Image<Gray, byte>(fileName);
            Image<Gray, byte> image_Blur = image_Src.CopyBlank();
            CvInvoke.GaussianBlur(image_Src, image_Blur, new Size(3, 3), 2);

            Image<Gray, byte> image_Threshold_200 = image_Src.CopyBlank();

            CvInvoke.Threshold(image_Blur, image_Threshold_200, 200, 255, ThresholdType.Binary);

            List<MyContour> list_MyContour_200 = GetAllContours(image_Threshold_200);

            List<MyContour> list_MyContour_Border = GetCalibrationBorderContours(list_MyContour_200);

            Rectangle rect = GetMaxRectForBorder(list_MyContour_Border);

            List<MyContour> list_MyContour_Cali = GetCalibrationContours(list_MyContour_200, rect);


            return list_MyContour_Cali.Select(c => c.center).ToList();
        }

        private static List<MyContour> GetAllContours(Image<Gray, byte> image) {
            List<MyContour> list_MyContour = new List<MyContour>();

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(image, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++) {
                VectorOfPoint contour = contours[i];
                MyContour myContour = new MyContour(contour);
                list_MyContour.Add(myContour);
            }

            return list_MyContour;
        }

        private static List<MyContour> GetCalibrationBorderContours(List<MyContour> list_Contour_All) {
            List<MyContour> list_MyContour = new List<MyContour>();

            foreach (MyContour contour in list_Contour_All) {
                if (contour.rect.Height > 300) {
                    double aspect = (double)contour.rect.Height / (double)contour.rect.Width;
                    if (aspect > 1.6) {
                        double area_Rect = contour.rect.Width * contour.rect.Height;
                        if (contour.area / area_Rect < 0.2) {
                            list_MyContour.Add(contour);
                        }
                    }
                }
            }

            return list_MyContour;
        }

        private static List<MyContour> GetCalibrationContours(List<MyContour> list_Contour_All, Rectangle rect) {
            List<MyContour> list_MyContour = new List<MyContour>();

            foreach (MyContour contour in list_Contour_All) {
                if (CheckIsCircle(contour, 100)) {
                    if (ImageAnalyseUtil.CheckPointInRange(rect, contour.center)) {
                        list_MyContour.Add(contour);
                    }
                }
            }

            return list_MyContour;
        }

        private static Rectangle GetMaxRectForBorder(List<MyContour> list_Contour) {
            List<Point> points = new List<Point>();
            foreach (MyContour contour in list_Contour)
                points.AddRange(contour.points);
            return ImageAnalyseUtil.GetRectangleFromPoints(points.ToArray());
        }




        public Point TestGetCircle(string fileName) {
            try {
                return TestGetCircle_Analyse(fileName);
            }
            catch (Exception ex) {
                ExceptionUtil.SaveException(ex);
            }
            return new Point(-1, -1);
        }

        public Point TestGetCircle_Analyse(string fileName) {
            Center = new Point(-1, -1);
            RotationResult = 0;
            LOAResult = 0;
            for (int i = 0; i < 5; i++) {
                string msg_ex = "";
                bool hasException = false;
                try {
                    using (Image<Gray, byte> image_Src = new Image<Gray, byte>(fileName)) {
                        TestGetCircle_Analyse(image_Src);
                    }

                }
                catch (Exception ex) {
                    msg_ex = ex.Message;
                    hasException = true;
                    ExceptionUtil.SaveException(ex);
                    Exception e = new Exception("Runtime Error Index: " + i);
                    ExceptionUtil.SaveException(e);
                }
                if (hasException && msg_ex.Contains("内存不足")) {
                    GC.Collect();
                    Thread.Sleep(400);
                }
                else
                    break;
            }
            return Center;
        }

        private void TestGetCircle_Analyse(Image<Gray, byte> image_Src) {
            List<MyContour> list_sorted = new List<MyContour>();
            List<MyContour> list_UnSort = new List<MyContour>(); ;
            List<MyContour> list_RotationLine = new List<MyContour>();
            MyContour centerContour = null;

            list_UnSort = TestGetCircleByAllPoints(image_Src);
            mL.ShowLog("Get Circle End", LogType.ImageAnalyse);

            bool CheckCenterSuccess = false;

            if (list_UnSort.Count == 91) {
                List<MyContour> list_sorted_ = SortPointsByTopLine(list_UnSort, ImageWidth);
                MyContour contour_Center = list_sorted_[45];

                if (CheckIsCenterPoint(image_Src, contour_Center.center, 120)) {
                    CheckCenterSuccess = true;
                    list_sorted = list_sorted_;
                    Center = new Point((int)contour_Center.center.X, (int)contour_Center.center.Y);
                    list_RotationLine = list_sorted.GetRange(13 * 3, 13);
                    RotationResult = GetAngleByPoints(list_RotationLine.Select(cont => cont.center).ToList());
                    LOAResult = GetLOA(Center);
                }
            }
            GC.Collect();
            Thread.Sleep(10);

            if (!CheckCenterSuccess) {
                mL.ShowLog("Get Center !Success , New Get Center ", LogType.ImageAnalyse);

                using (Image<Gray, byte> image_Blur = image_Src.CopyBlank()) {
                    CvInvoke.Blur(image_Src, image_Blur, new Size(5, 5), new Point(-1, -1));

                    bool findCenter = false;

                    int[] findCenterThreasholds = { 150, 100, 75, 50 };

                    for (int i = 0; i < findCenterThreasholds.Length; i++) {
                        int findCenterThreashold = findCenterThreasholds[i];

                        foreach (MyContour contour in list_UnSort) {
                            PointF p = contour.center;
                            bool isCenter = CheckIsCenterPoint(image_Blur, p, findCenterThreashold);

                            if (isCenter) {
                                Center = new Point((int)p.X, (int)p.Y);
                                centerContour = contour;
                                findCenter = true;
                                mL.ShowLog("Get Center_New Success", LogType.ImageAnalyse);
                                break;
                            }
                        }
                        if (findCenter)
                            break;
                    }

                    if (findCenter) {
                        LOAResult = GetLOA(Center);
                        list_RotationLine = GetOneLinePoints(list_UnSort, centerContour);
                        if (list_RotationLine.Count >= 3)
                            RotationResult = GetAngleByPoints(list_RotationLine.Select(cont => cont.center).ToList());

                    }
                }
            }

            {
                int X_ImageCenter = ImageWidth / 2 + Config.Camera_MoveX[Config.CurrentProject] + Config.Camera_OffsetX[Config.CurrentProject];
                int Y_ImageCenter = ImageHeight / 2 + Config.Camera_MoveY[Config.CurrentProject] + Config.Camera_OffsetY[Config.CurrentProject];

                mL.ShowLog("Draw Result Begin", LogType.ImageAnalyse);
                CvInvoke.Line(image_Src, new Point(X_ImageCenter - 150, Y_ImageCenter),
                    new Point(X_ImageCenter + 150, Y_ImageCenter), new MCvScalar(255, 255, 255), 4);
                CvInvoke.Line(image_Src, new Point(X_ImageCenter, Y_ImageCenter - 150),
                    new Point(X_ImageCenter, Y_ImageCenter + 150), new MCvScalar(255, 255, 255), 4);

                {
                    CvInvoke.Line(image_Src, new Point(Center.X - 100, Center.Y - 100), new Point(Center.X + 100, Center.Y + 100), new MCvScalar(255), 2);
                    CvInvoke.Line(image_Src, new Point(Center.X - 100, Center.Y + 100), new Point(Center.X + 100, Center.Y - 100), new MCvScalar(255), 2);
                }

                foreach (MyContour one in list_RotationLine) {
                    Point p = new Point((int)one.center.X, (int)one.center.Y);
                    CvInvoke.Rectangle(image_Src, new Rectangle(p.X - 80, p.Y - 80, 160, 160), new MCvScalar(255), 2);
                }

                string outFileName = ExportUtil.GetImageSaveFileName("HUDCenterCheck");
                if (File.Exists(outFileName))
                    outFileName = ExportUtil.GetImageSaveFileName("HUDCenterCheck" + DateTime.Now.Millisecond);
                image_Src.Save(outFileName);
                mL.ShowLog("Save Result Success", LogType.ImageAnalyse);
            }
        }

        private List<MyContour> GetOneLinePoints(List<MyContour> list_UnSort, MyContour target) {
            List<MyContour> list_line = new List<MyContour>();
            list_line.Add(target);
            MyContour current = target;
            for (int i = 0; i < 6; i++) {
                current = GetNextContourInOneLine(list_UnSort, current, true);
                if (current != null)
                    list_line.Add(current);
                else
                    break;
            }

            current = target;
            for (int i = 0; i < 6; i++) {
                current = GetNextContourInOneLine(list_UnSort, current, false);
                if (current != null)
                    list_line.Add(current);
                else
                    break;
            }

            list_line.Sort((a, b) => a.center.X < b.center.X ? -1 : 1);

            return list_line;
        }

        private MyContour GetNextContourInOneLine(List<MyContour> list_UnSort, MyContour target, bool toLeft) {
            if (target == null)
                return null;

            foreach (MyContour one in list_UnSort) {
                if ((toLeft && one.center.X >= target.center.X) || (!toLeft && one.center.X <= target.center.X)) {
                    continue;
                }
                if (CheckCanBeNextPoint(target.center, one.center))
                    return one;
            }
            return null;
        }

        private bool CheckCanBeNextPoint(PointF self, PointF next) {
            float length = next.X - self.X;
            float height = next.Y - self.Y;
            length = Math.Abs(length);

            if (length < 100 || length > 320)
                return false;

            height = Math.Abs(height);
            if (height / length > 0.4)
                return false;

            return true;
        }

        private bool CheckIsCenterPoint(Image<Gray, byte> image, PointF p, int threshold) {
            int L = 50;

            Point p_tl = new Point((int)p.X - L, (int)p.Y - L);
            Point p_tr = new Point((int)p.X + L, (int)p.Y - L);
            Point p_bl = new Point((int)p.X - L, (int)p.Y + L);
            Point p_br = new Point((int)p.X + L, (int)p.Y + L);

            Point p_t = new Point((int)p.X, (int)p.Y - L);
            Point p_b = new Point((int)p.X, (int)p.Y + L);
            Point p_l = new Point((int)p.X - L, (int)p.Y);
            Point p_r = new Point((int)p.X + L, (int)p.Y);

            bool tl = CheckHasPointInForPoint(image, p_tl, threshold);
            bool tr = CheckHasPointInForPoint(image, p_tr, threshold);
            bool bl = CheckHasPointInForPoint(image, p_bl, threshold);
            bool br = CheckHasPointInForPoint(image, p_br, threshold);

            bool t = CheckHasPointInForPoint(image, p_t, threshold);
            bool b = CheckHasPointInForPoint(image, p_b, threshold);
            bool l = CheckHasPointInForPoint(image, p_l, threshold);
            bool r = CheckHasPointInForPoint(image, p_r, threshold);

            if (tl && tr && bl && br && !t && !b && !l && !r)
                return true;

            return false;
        }

        private bool CheckHasPointInForPoint(Image<Gray, byte> image, Point p, int threshold) {
            int CheckWidth = 11;
            int CheckFlag = 0;
            for (int j = 0; j < CheckWidth; j++) {
                for (int i = 0; i < CheckWidth; i++) {

                    if (p.Y + j - CheckWidth / 2 > 1 && p.Y + j - CheckWidth / 2 < image.Height - 1 && p.X + i - CheckWidth / 2 > 1 && p.X + i - CheckWidth / 2 < image.Width - 1) {
                        if (image.Data[p.Y + j - CheckWidth / 2, p.X + i - CheckWidth / 2, 0] > threshold) {
                            CheckFlag++;
                        }
                    }

                }
            }

            if (CheckFlag > CheckWidth)
                return true;
            return false;
        }

        public double GetLOA(Point center) {
            Point CenterMove = GetCenterPointMoved(center);
            double loa = Math.Atan(CenterMove.X * 1.85 / 35 / 1000) * 180 / Math.PI;
            return -loa;
        }

        private Point GetCenterPointMoved(Point center) {
            int moveX = center.X - (ImageAnalyseLogic.ImageWidth / 2 + Config.Camera_MoveX[Config.CurrentProject]);
            int moveY = center.Y - (ImageAnalyseLogic.ImageHeight / 2 + Config.Camera_MoveY[Config.CurrentProject]);

            return new Point(moveX, moveY);
        }


        protected double GetAngleByPoints(List<PointF> points) {
            double[] result = CalculationSlopeWithOffset(points);
            return -Math.Atan(result[0]) * 180 / Math.PI;
        }

        protected double[] CalculationSlopeWithOffset(IEnumerable<PointF> points) {
            double k;
            double b;

            using (VectorOfPointF pointsVector = new VectorOfPointF()) {
                pointsVector.Push(points.ToArray());

                using (VectorOfDouble line = new VectorOfDouble()) {
                    CvInvoke.FitLine(pointsVector, line, DistType.L2, 0, 0.01, 0.01);

                    double cos_theta = line[0];
                    double sin_theta = line[1];
                    double x0 = line[2], y0 = line[3];
                    k = sin_theta / cos_theta;
                    b = y0 - k * x0;
                }
            }
            return new double[] { k, b };
        }


        private List<MyContour> TestGetCircleByAllPoints(Image<Gray, byte> image_Src) {
            List<MyContour> contour_UnChecked = new List<MyContour>();
            List<MyContour> list_MyContour_60 = new List<MyContour>();
            List<MyContour> list_MyContour_150 = new List<MyContour>();
            List<MyContour> list_MyContour_210 = new List<MyContour>();


            using (Image<Gray, byte> image_Blur = image_Src.CopyBlank()) {
                CvInvoke.GaussianBlur(image_Src, image_Blur, new Size(3, 3), 2);

                using (Image<Gray, byte> image_Threshold_210 = image_Src.CopyBlank()) {
                    CvInvoke.Threshold(image_Blur, image_Threshold_210, 210, 255, ThresholdType.Binary);
                    list_MyContour_210 = GetCircleContours(image_Threshold_210, 210, 300);
                }

                Mat StructingElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(9, 9), new Point(-1, -1));
                CvInvoke.Dilate(image_Blur, image_Blur, StructingElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0));
                CvInvoke.Erode(image_Blur, image_Blur, StructingElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0));

                using (Image<Gray, byte> image_Threshold_60 = image_Src.CopyBlank()) {
                    CvInvoke.Threshold(image_Blur, image_Threshold_60, 60, 255, ThresholdType.Binary);
                    list_MyContour_60 = GetCircleContours(image_Threshold_60, 60, 300);
                }

                using (Image<Gray, byte> image_Threshold_150 = image_Src.CopyBlank()) {
                    CvInvoke.Threshold(image_Blur, image_Threshold_150, 150, 255, ThresholdType.Binary);
                    list_MyContour_150 = GetCircleContours(image_Threshold_150, 150, 300);
                }

                contour_UnChecked.AddRange(list_MyContour_60);
                contour_UnChecked.AddRange(list_MyContour_150);
                contour_UnChecked.AddRange(list_MyContour_210);
            }
            List<MyContour> contour_Checked = GetContours(contour_UnChecked);

            return contour_Checked;
        }

        private List<MyContour> GetContours(List<MyContour> contour_UnChecked) {
            List<MyContour> contour_Checked = new List<MyContour>();

            double distanceLimit = 50;

            foreach (MyContour one in contour_UnChecked) {
                int existed = CheckExistTooClosePoint(one, contour_Checked, distanceLimit);
                if (existed < 0)
                    contour_Checked.Add(one);
                else {
                    MyContour contourExisted = contour_Checked[existed];
                    if (contourExisted.area < one.area) {
                        contour_Checked.Remove(contourExisted);
                        contour_Checked.Add(one);
                    }
                }
            }

            return contour_Checked;
        }

        private int CheckExistTooClosePoint(MyContour one, List<MyContour> list, double limit) {
            for (int i = 0; i < list.Count; i++) {
                MyContour target = list[i];
                double distance = ImageAnalyseUtil.GetTwoPointDistance(target.center, one.center);
                if (distance < limit)
                    return i;
            }
            return -1;
        }

        private List<MyContour> GetCircleContours(Image<Gray, byte> image, int index, double minArea) {
            List<MyContour> list_MyContour = new List<MyContour>();

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint()) {
                CvInvoke.FindContours(image, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                for (int i = 0; i < contours.Size; i++) {
                    VectorOfPoint contour = contours[i];
                    MyContour myContour = new MyContour(contour);

                    if (CheckIsCircle(myContour, minArea)) {
                        list_MyContour.Add(myContour);
                    }
                }
            }

            return list_MyContour;
        }

        private static bool CheckIsCircle(MyContour myContour, double minArea) {
            if (myContour.area < minArea || myContour.area > 4500)
                return false;

            Rectangle rect = ImageAnalyseUtil.GetRectangleFromPoints(myContour.points.ToArray());

            double radio = (double)rect.Width / rect.Height;
            if (radio > 1.6 || radio < 0.5)
                return false;

            List<double> distances = myContour.points.Select(p => ImageAnalyseUtil.GetTwoPointDistance(myContour.center, p)).ToList();

            double avg = distances.Average();

            for (int i = 0; i < distances.Count; i++) {
                double radio_distance = distances[i] / avg;
                if (radio_distance > 1.3 || radio_distance < 0.62)
                    return false;
            }
            return true;
        }


        private List<MyContour> SortPointsByTopLine(List<MyContour> points, int width) {
            List<MyContour> unSortPoints = new List<MyContour>();
            unSortPoints.AddRange(points);
            List<MyContour> SortPoints = new List<MyContour>();

            for (int i = 0; i < 100; i++) {
                MyContour[] topPoints = GetTopBorderPoints(unSortPoints, width);
                List<MyContour> topPointsList = FilterTopBorderPoints(topPoints);
                SortPoints.AddRange(topPointsList);
                foreach (MyContour point in topPointsList) {
                    unSortPoints.Remove(point);
                }
                if (unSortPoints.Count <= 0)
                    break;
            }

            return SortPoints;
        }

        private MyContour[] GetTopBorderPoints(List<MyContour> points, int imageWidth) {
            int PositionCalculationWidth = 21;

            MyContour[] topPoints = new MyContour[imageWidth / PositionCalculationWidth + 1];

            for (int i = 0; i < points.Count; i++) {
                MyContour point = points[i];
                int currentX = (int)point.center.X;
                int xIndex = currentX / PositionCalculationWidth;
                MyContour topPoint = topPoints[xIndex];
                if (topPoint == null || topPoint.area <= 0)
                    topPoints[xIndex] = point;
                else if (topPoint.center.Y > point.center.Y)
                    topPoints[xIndex] = point;
            }

            return topPoints;
        }

        private List<MyContour> FilterTopBorderPoints(MyContour[] topPoints) {
            List<MyContour> topPointList = PointsArrayToList(topPoints);
            FilterOneLinePoints(topPointList, (p1, p2) => p1.center.Y - p2.center.Y, (p1, p2) => (p1.center.X - p2.center.X) / 4);
            return topPointList;
        }

        private List<MyContour> PointsArrayToList(MyContour[] points) {
            List<MyContour> list = new List<MyContour>();
            for (int i = 0; i < points.Length; i++) {
                MyContour point = points[i];
                if (point != null && point.area > 0)
                    list.Add(point);
            }
            return list;
        }

        private void FilterOneLinePoints(List<MyContour> points, Func<MyContour, MyContour, float> getDiffance, Func<MyContour, MyContour, float> getDistance) {
            List<int> removeIndexs = new List<int>();
            for (int i = 0; i < points.Count; i++) {
                if (i == 0) {
                    if (points.Count > 2) {
                        MyContour p = points[0];
                        MyContour p1 = points[1];
                        MyContour p2 = points[2];

                        float temp = getDiffance(p, p1);
                        float distance = getDistance(p2, p);
                        if (temp > distance)
                            removeIndexs.Add(i);
                    }
                }
                else if (i == points.Count - 1) {
                    if (points.Count > 2) {
                        MyContour p = points[i - 2];
                        MyContour p1 = points[i - 1];
                        MyContour p2 = points[i];

                        float temp = getDiffance(p2, p1);
                        float distance = getDistance(p2, p);

                        if (temp > distance)
                            removeIndexs.Add(i);
                    }
                }
                else {
                    MyContour p = points[i - 1];
                    MyContour p1 = points[i];
                    MyContour p2 = points[i + 1];

                    float temp = getDiffance(p1, p);
                    float temp2 = getDiffance(p1, p2);
                    float distance = getDistance(p2, p);
                    if (temp > distance || temp2 > distance)
                        removeIndexs.Add(i);
                }
            }
            for (int i = removeIndexs.Count - 1; i >= 0; i--) {
                int index = removeIndexs[i];
                points.RemoveAt(index);
            }
        }



    }
}
