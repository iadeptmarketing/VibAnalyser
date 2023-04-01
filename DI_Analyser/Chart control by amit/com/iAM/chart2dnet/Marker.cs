namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class Marker : GraphObj
    {
        internal double markerSize;
        internal int markerType;

        public Marker()
        {
            this.markerType = 3;
            this.markerSize = 12.0;
            this.InitDefaults();
        }

        public Marker(PhysicalCoordinates thetransform)
        {
            this.markerType = 3;
            this.markerSize = 12.0;
            this.InitDefaults();
            this.InitChartMarker(thetransform, 3, 0.0, 0.0, 12.0, 1);
        }

        public Marker(PhysicalCoordinates transform, int nmarkertype, double x, double y, double rsize, int npostype)
        {
            this.markerType = 3;
            this.markerSize = 12.0;
            this.InitDefaults();
            this.InitChartMarker(transform, nmarkertype, x, y, rsize, npostype);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            Marker marker = new Marker();
            marker.Copy(this);
            return marker;
        }

        public void Copy(Marker source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.markerType = source.markerType;
                this.markerSize = source.markerSize;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawMarker(base.thePath);
                base.boundingBox.Reset();
                base.boundingBox.AddPath(base.thePath, false);
                if (this.GetChartObjEnable() == 1)
                {
                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), base.thePath);
                }
            }
        }

        private void DrawMarker(GraphicsPath path)
        {
            Point2D pointd;
            int num = 0;
            int num2 = 0;
            double x = 0.0;
            double y = 0.0;
            int num5 = 8;
            Point2D source = base.chartObjScale.ConvertCoord(1, this.GetLocation(), base.positionType);
            x = source.GetX();
            y = source.GetY();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            switch (this.markerType)
            {
                case 0:
                    return;

                case 1:
                    base.chartObjScale.WLineAbs(path, x, base.chartObjScale.GetStartY(), x, base.chartObjScale.GetStopY());
                    return;

                case 2:
                    base.chartObjScale.WLineAbs(path, base.chartObjScale.GetStartX(), y, base.chartObjScale.GetStopX(), y);
                    return;

                case 3:
                    pointd = base.chartObjScale.ConvertCoord(0, source, 1);
                    num5 = ((int) this.markerSize) / 2;
                    num = (int) pointd.GetX();
                    num2 = (int) pointd.GetY();
                    base.chartObjScale.PLineAbs(path, (double) (num - num5), (double) num2, (double) (num + num5), (double) num2);
                    base.chartObjScale.PLineAbs(path, (double) num, (double) (num2 - num5), (double) num, (double) (num2 + num5));
                    return;

                case 4:
                    pointd = base.chartObjScale.ConvertCoord(0, source, 1);
                    num5 = ((int) this.markerSize) / 2;
                    num = (int) pointd.GetX();
                    num2 = (int) pointd.GetY();
                    base.chartObjScale.PMoveToAbs(path, (double) (num - num5), (double) (num2 - num5));
                    base.chartObjScale.PLineToAbs(path, (double) (num - num5), (double) (num2 + num5));
                    base.chartObjScale.PLineToAbs(path, (double) (num + num5), (double) (num2 + num5));
                    base.chartObjScale.PLineToAbs(path, (double) (num + num5), (double) (num2 - num5));
                    base.chartObjScale.PLineToAbs(path, (double) (num - num5), (double) (num2 - num5));
                    return;

                case 5:
                    base.chartObjScale.WLineAbs(path, x, base.chartObjScale.GetStartY(), x, base.chartObjScale.GetStopY());
                    base.chartObjScale.WLineAbs(path, base.chartObjScale.GetStartX(), y, base.chartObjScale.GetStopX(), y);
                    return;
            }
        }

        public void DrawReversibleMarker(ChartView chartviewcomponent)
        {
            Point point;
            Point point2;
            int num = 8;
            Point p = base.chartObjScale.ConvertCoord(0, this.GetLocation(), base.positionType).GetPoint();
            p = chartviewcomponent.PointToScreen(p);
            Point2D source = new Point2D(base.chartObjScale.GetStartX(), base.chartObjScale.GetStartY());
            Point point4 = base.chartObjScale.ConvertCoord(0, source, 1).GetPoint();
            point4 = chartviewcomponent.PointToScreen(point4);
            Point2D pointd3 = new Point2D(base.chartObjScale.GetStopX(), base.chartObjScale.GetStopY());
            Point point5 = base.chartObjScale.ConvertCoord(0, pointd3, 1).GetPoint();
            point5 = chartviewcomponent.PointToScreen(point5);
            switch (this.markerType)
            {
                case 0:
                    return;

                case 1:
                    point = new Point(p.X, point4.Y);
                    point2 = new Point(p.X, point5.Y);
                    base.chartObjScale.DrawReversibleLine(point, point2, Color.White);
                    return;

                case 2:
                    point = new Point(point4.X, p.Y);
                    point2 = new Point(point5.X, p.Y);
                    base.chartObjScale.DrawReversibleLine(point, point2, Color.White);
                    return;

                case 3:
                    point = new Point(p.X - num, p.Y);
                    point2 = new Point(p.X + num, p.Y);
                    base.chartObjScale.DrawReversibleLine(point, point2, Color.White);
                    point = new Point(p.X, p.Y - num);
                    point2 = new Point(p.X, p.Y + num);
                    base.chartObjScale.DrawReversibleLine(point, point2, Color.White);
                    return;

                case 4:
                {
                    Rectangle rect = new Rectangle(p.X - num, p.Y - num, p.X + num, p.Y + num);
                    base.chartObjScale.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
                    return;
                }
                case 5:
                    point = new Point(p.X, point4.Y);
                    point2 = new Point(p.X, point5.Y);
                    base.chartObjScale.DrawReversibleLine(point, point2, Color.White);
                    point = new Point(point4.X, p.Y);
                    point2 = new Point(point5.X, p.Y);
                    base.chartObjScale.DrawReversibleLine(point, point2, Color.White);
                    return;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public double GetMarkerSize()
        {
            return this.markerSize;
        }

        public int GetMarkerType()
        {
            return this.markerType;
        }

        private void InitChartMarker(PhysicalCoordinates transform, int nmarkertype, double x, double y, double rsize, int npostype)
        {
            this.SetChartObjScale(transform);
            this.SetLocation(x, y);
            this.markerType = nmarkertype;
            this.markerSize = rsize;
            base.positionType = npostype;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x3e8;
            base.moveableType = 0;
            base.chartObjClipping = 2;
            base.chartObjAttributes.SetLineAttributes(Color.Black, 1.0, DashStyle.Solid);
            this.InitChartMarker(base.chartObjScale, 3, 0.0, 0.0, 12.0, 1);
        }

        public void SetMarkerSize(double rsize)
        {
            this.markerSize = rsize;
        }

        public void SetMarkerType(int nmarkertype)
        {
            this.markerType = nmarkertype;
        }

        public double MarkerSize
        {
            get
            {
                return this.markerSize;
            }
            set
            {
                this.markerSize = value;
            }
        }

        public int MarkerType
        {
            get
            {
                return this.markerType;
            }
            set
            {
                this.markerType = value;
            }
        }
    }
}

