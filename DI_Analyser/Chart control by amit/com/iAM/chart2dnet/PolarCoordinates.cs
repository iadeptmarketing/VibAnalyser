namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing.Drawing2D;

    public class PolarCoordinates : CartesianCoordinates
    {
        private Point2D polarCurrentPos;

        public PolarCoordinates()
        {
            this.polarCurrentPos = new Point2D(0.0, 0.0);
            this.InitDefaults();
        }

        public PolarCoordinates(double rR)
        {
            this.polarCurrentPos = new Point2D(0.0, 0.0);
            this.InitDefaults();
            base.SetCartesianScaleTransforms(0, 0);
            this.SetPolarScaleRadius(rR);
        }

        public override void AutoScale(ChartDataset[] datasets)
        {
            this.CalcAutoScale(datasets, 2);
        }

        public override void AutoScale(ChartDataset dataset)
        {
            this.CalcAutoScale(dataset, 2);
        }

        public void AutoScale(ChartDataset[] datasets, int nroundmode)
        {
            this.CalcAutoScale(datasets, nroundmode);
        }

        public void AutoScale(ChartDataset dataset, int nroundmode)
        {
            this.CalcAutoScale(dataset, nroundmode);
        }

        public void CalcAutoScale(ChartDataset dataset, int nroundmode)
        {
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(dataset, 0, nroundmode);
            compatibleAutoScale.CalcChartAutoScaleDataset();
            this.SetPolarScaleRadius(compatibleAutoScale.GetFinalMax());
            compatibleAutoScale = null;
        }

        public void CalcAutoScale(ChartDataset[] datasets, int nroundmode)
        {
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(datasets, 0, nroundmode);
            compatibleAutoScale.CalcChartAutoScaleDataset();
            this.SetPolarScaleRadius(compatibleAutoScale.GetFinalMax());
            compatibleAutoScale = null;
        }

        public void CartesianToPolar(Point2D dest, Point2D source)
        {
            double px = 0.0;
            double py = 0.0;
            double x = source.GetX();
            double y = source.GetY() * base.GetGraphAspectRatio();
            py = Math.Atan2(y, x);
            px = Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2.0));
            if (py < 0.0)
            {
                py += 6.2831853071795862;
            }
            dest.SetLocation(px, py);
        }

        public override object Clone()
        {
            PolarCoordinates coordinates = new PolarCoordinates();
            coordinates.Copy(this);
            return coordinates;
        }

        public override Point2D ConvertCoord(int ndestpostype, Point2D source, int nsrcpostype)
        {
            Point2D dest = new Point2D();
            this.ConvertCoord(dest, ndestpostype, source, nsrcpostype);
            return dest;
        }

        public override void ConvertCoord(Point2D dest, int ndestpostype, Point2D source, int nsrcpostype)
        {
            Point2D pointd = new Point2D();
            dest.SetLocation(source.GetX(), source.GetY());
            int num = nsrcpostype;
            switch (num)
            {
                case 0:
                    num = ndestpostype;
                    if (num != 2)
                    {
                        base.ConvertCoord(dest, ndestpostype, source, nsrcpostype);
                    }
                    else
                    {
                        base.UserToPhys(pointd, source);
                        this.CartesianToPolar(dest, pointd);
                    }
                    break;

                case 2:
                    switch (ndestpostype)
                    {
                        case 0:
                            this.PolarToCartesian(pointd, source);
                            base.PhysToUser(dest, pointd);
                            break;

                        case 1:
                            this.PolarToCartesian(pointd, source);
                            dest.SetLocation(pointd.GetX(), pointd.GetY());
                            break;

                        case 2:
                            dest.SetLocation(source.GetX(), source.GetY());
                            break;

                        case 3:
                            this.PolarToCartesian(pointd, source);
                            base.NormalizePoint(dest, pointd, 3);
                            break;

                        case 4:
                            this.PolarToCartesian(pointd, source);
                            base.NormalizePoint(dest, pointd, 4);
                            break;
                    }
                    break;

                case 3:
                    num = ndestpostype;
                    if (num != 2)
                    {
                        base.ConvertCoord(dest, ndestpostype, source, nsrcpostype);
                    }
                    else
                    {
                        base.UnNormalizePoint(pointd, source, 3);
                        this.CartesianToPolar(dest, pointd);
                    }
                    break;

                case 4:
                    num = ndestpostype;
                    if (num != 2)
                    {
                        base.ConvertCoord(dest, ndestpostype, source, nsrcpostype);
                    }
                    else
                    {
                        base.UnNormalizePoint(pointd, source, 4);
                        this.CartesianToPolar(dest, pointd);
                    }
                    break;

                default:
                    num = ndestpostype;
                    if (num == 2)
                    {
                        this.CartesianToPolar(dest, source);
                    }
                    else
                    {
                        base.ConvertCoord(dest, ndestpostype, source, nsrcpostype);
                    }
                    break;
            }
            pointd = null;
        }

        public override void ConvertCoordArray(Point2D[] dest, int ndestpostype, Point2D[] source, int nsrcpostype, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (dest[i] == null)
                {
                    dest[i] = new Point2D();
                }
                this.ConvertCoord(dest[i], ndestpostype, source[i], nsrcpostype);
            }
        }

        public double ConvertRadius(int ndestpostype, double source, int nsrcpostype)
        {
            double num = 0.0;
            Point2D dest = new Point2D();
            Point2D pointd2 = new Point2D();
            Point2D pointd3 = new Point2D(source, source);
            this.ConvertCoord(dest, ndestpostype, pointd2, nsrcpostype);
            this.ConvertCoord(pointd2, ndestpostype, pointd3, nsrcpostype);
            num = pointd2.GetX() - dest.GetX();
            dest = null;
            pointd2 = null;
            return num;
        }

        public void Copy(PolarCoordinates source)
        {
            if (source != null)
            {
                base.Copy((CartesianCoordinates) source);
                this.polarCurrentPos = source.polarCurrentPos;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public PolarAxes GetCompatibleAxes()
        {
            return this.GetCompatibleAxis();
        }

        public PolarAxes GetCompatibleAxis()
        {
            return new PolarAxes(this);
        }

        public double GetPolarScaleRadius()
        {
            return Math.Abs(base.GetStopX());
        }

        private void InitDefaults()
        {
            base.SetCartesianScaleTransforms(0, 0);
            base.chartObjType = 0x4a2;
        }

        public void PolarLineAbs(GraphicsPath path, Point2D p1, Point2D p2, bool binterpolate)
        {
            this.PolarMoveToAbs(path, p1.GetX(), p1.GetY());
            this.PolarLineToAbs(path, p2.GetX(), p2.GetY(), binterpolate);
        }

        public void PolarLineAbs(GraphicsPath path, double x1, double y1, double x2, double y2, bool binterpolate)
        {
            this.PolarMoveToAbs(path, x1, y1);
            this.PolarLineToAbs(path, x2, y2, binterpolate);
        }

        public void PolarLineToAbs(GraphicsPath path, double x, double y, bool binterpolate)
        {
            Point2D source = new Point2D(x, y);
            Point2D dest = new Point2D(x, y);
            Point2D pointd3 = new Point2D();
            Point2D pointd4 = new Point2D(base.userCurrentPos);
            double num = this.polarCurrentPos.GetX();
            double num2 = this.polarCurrentPos.GetY();
            double num3 = y - num2;
            int num4 = 1;
            double num5 = x - num;
            double px = 0.0;
            double py = 0.0;
            GraphicsPath addingPath = new GraphicsPath();
            if (binterpolate)
            {
                if (num3 > 0.1)
                {
                    num4 = (int) (10.0 * num3);
                    num5 /= (double) num4;
                    num3 /= (double) num4;
                }
                for (int i = 0; i <= num4; i++)
                {
                    px = num + (i * num5);
                    py = num2 + (i * num3);
                    source.SetLocation(px, py);
                    this.PolarToCartesian(dest, source);
                    base.PhysToUser(pointd3, dest);
                    addingPath.AddLine((float) pointd4.GetX(), (float) pointd4.GetY(), (float) pointd3.GetX(), (float) pointd3.GetY());
                    pointd4.SetLocation(pointd3);
                }
            }
            else
            {
                source.SetLocation(x, y);
                this.PolarToCartesian(dest, source);
                base.PhysToUser(pointd3, dest);
                addingPath.AddLine((float) pointd4.GetX(), (float) pointd4.GetY(), (float) pointd3.GetX(), (float) pointd3.GetY());
                pointd4.SetLocation(pointd3);
            }
            path.AddPath(addingPath, true);
            base.userCurrentPos.SetLocation(pointd3);
            base.worldCurrentPos.SetLocation(dest);
            this.polarCurrentPos.SetLocation(x, y);
        }

        public void PolarMoveToAbs(GraphicsPath path, double x, double y)
        {
            Point2D source = new Point2D(x, y);
            Point2D dest = new Point2D(x, y);
            Point2D pointd3 = new Point2D();
            this.PolarToCartesian(dest, source);
            base.PhysToUser(pointd3, dest);
            base.userCurrentPos.SetLocation(pointd3);
            base.worldCurrentPos.SetLocation(dest);
            this.polarCurrentPos.SetLocation(x, y);
        }

        public void PolarToCartesian(Point2D dest, Point2D source)
        {
            double px = source.GetX() * Math.Cos(source.GetY());
            double py = (source.GetX() * Math.Sin(source.GetY())) / base.GetGraphAspectRatio();
            dest.SetLocation(px, py);
        }

        public void SetPolarScaleRadius(double radius)
        {
            double num = Math.Abs(radius);
            base.SetPhysScale(-num, -num, num, num);
        }
    }
}

