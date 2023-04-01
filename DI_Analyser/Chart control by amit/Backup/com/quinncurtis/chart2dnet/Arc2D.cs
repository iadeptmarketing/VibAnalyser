namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class Arc2D : Rectangle2D
    {
        internal int arcType;
        public const int CHORD = 1;
        internal double extentAngle;
        public const int OPEN = 0;
        public const int PIE = 2;
        internal double startAngle;

        public Arc2D()
        {
            this.arcType = 0;
            this.InitDefaults();
        }

        public Arc2D(Rectangle2D ellipseBounds, double start, double extent, int ntype)
        {
            this.InitDefaults();
            this.arcType = ntype;
            base.SetFrame(ellipseBounds.GetX(), ellipseBounds.GetY(), ellipseBounds.GetWidth(), ellipseBounds.GetHeight());
            this.startAngle = start;
            this.extentAngle = extent;
        }

        public Arc2D(double x, double y, double w, double h, double start, double extent, int ntype)
        {
            this.InitDefaults();
            this.arcType = ntype;
            base.SetFrame(x, y, w, h);
            this.startAngle = start;
            this.extentAngle = extent;
        }

        private void CartesianToPolar(Point2D dest, Point2D source)
        {
            double px = 0.0;
            double py = 0.0;
            double x = source.GetX();
            double y = source.GetY();
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
            return new Arc2D(base.GetX(), base.GetY(), base.GetWidth(), base.GetHeight(), this.startAngle, this.extentAngle, this.arcType);
        }

        public override bool Contains(Point2D p)
        {
            Rectangle2D testr = new Rectangle2D(p.GetX(), p.GetY(), 1.0, 1.0);
            return this.Contains(testr);
        }

        public override bool Contains(Rectangle2D testrect)
        {
            double px = base.GetX() + (base.GetWidth() / 2.0);
            double py = base.GetY() + (base.GetHeight() / 2.0);
            Point2D pointd = new Point2D(px, py);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(pointd.GetPointF(), this.GetStartPoint().GetPointF());
            path.AddArc((float) base.GetX(), (float) base.GetY(), (float) base.GetWidth(), (float) base.GetHeight(), (float) -this.startAngle, (float) -this.extentAngle);
            path.CloseFigure();
            Region region = new Region(path);
            return region.IsVisible(testrect.GetRectangleF());
        }

        public override bool Contains(double x, double y)
        {
            Rectangle2D testr = new Rectangle2D(x, y, 1.0, 1.0);
            return this.Contains(testr);
        }

        public bool Contains(double x, double y, double w, double h)
        {
            Rectangle2D testr = new Rectangle2D(x, y, w, h);
            return this.Contains(testr);
        }

        public void Copy(Arc2D source)
        {
            base.Copy(source);
            this.startAngle = source.startAngle;
            this.extentAngle = source.extentAngle;
        }

        public int GetArcarcType()
        {
            return this.arcType;
        }

        public Point2D GetEndPoint()
        {
            double d = (0.017453292519943295 * -this.GetStartAngle()) - this.GetExtentAngle();
            double px = base.GetX() + (((Math.Cos(d) * 0.5) + 0.5) * base.GetWidth());
            return new Point2D(px, base.GetY() + (((Math.Sin(d) * 0.5) + 0.5) * base.GetHeight()));
        }

        public double GetExtentAngle()
        {
            return this.extentAngle;
        }

        public double GetStartAngle()
        {
            return this.startAngle;
        }

        public Point2D GetStartPoint()
        {
            double d = 0.017453292519943295 * -this.GetStartAngle();
            double px = base.GetX() + (((Math.Cos(d) * 0.5) + 0.5) * base.GetWidth());
            return new Point2D(px, base.GetY() + (((Math.Sin(d) * 0.5) + 0.5) * base.GetHeight()));
        }

        private void InitDefaults()
        {
        }

        public override bool IntersectsWith(Rectangle2D r)
        {
            return this.Contains(r);
        }

        public void SetArc(Arc2D a)
        {
            this.SetArc(a.GetX(), a.GetY(), a.GetWidth(), a.GetHeight(), a.GetStartAngle(), a.GetExtentAngle(), a.GetArcarcType());
        }

        public void SetArc(Rectangle2D rect, double stAng, double extAng, int closure)
        {
            this.SetArc(rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight(), stAng, extAng, closure);
        }

        public void SetArc(Point2D loc, Dimension size, double stAng, double extAng, int closure)
        {
            this.SetArc(loc.GetX(), loc.GetY(), size.GetWidth(), size.GetHeight(), stAng, extAng, closure);
        }

        public void SetArc(double x, double y, double w, double h, double stAng, double extAng, int closure)
        {
            this.SetArcarcType(closure);
            base.SetFrame(x, y, w, h);
            this.startAngle = stAng;
            this.extentAngle = extAng;
        }

        public void SetArcarcType(int ntype)
        {
            if ((ntype >= 0) || (ntype <= 2))
            {
                this.arcType = ntype;
            }
        }

        public void SetArcByCenter(double x, double y, double radius, double stAng, double extAng, int closure)
        {
            this.SetArc(x - radius, y - radius, radius * 2.0, radius * 2.0, stAng, extAng, closure);
        }

        public void SetArcFrame(double x, double y, double w, double h)
        {
            this.SetArc(x, y, w, h, this.GetStartAngle(), this.GetExtentAngle(), this.arcType);
        }

        public void SetExtentAngle(double extAng)
        {
            this.extentAngle = extAng;
        }

        public void SetStartAngle(double stAng)
        {
            this.startAngle = stAng;
        }

        public double ExtentAngle
        {
            get
            {
                return this.extentAngle;
            }
            set
            {
                this.extentAngle = value;
            }
        }

        public double StartAngle
        {
            get
            {
                return this.startAngle;
            }
            set
            {
                this.startAngle = value;
            }
        }
    }
}

