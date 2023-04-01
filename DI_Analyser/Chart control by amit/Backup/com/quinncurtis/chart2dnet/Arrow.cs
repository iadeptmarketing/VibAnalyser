namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class Arrow : ChartObj
    {
        private float arrowBaseScaleFactor;
        private float arrowHeadHalfWidth;
        private float arrowHeadLength;
        private float arrowScaleFactor;
        internal float arrowShaftHalfWidth;
        private float arrowShaftLength;
        private GraphicsPath arrowShape;
        private float minArrowValue;

        public Arrow()
        {
            this.arrowShaftHalfWidth = 1f;
            this.arrowShaftLength = 7f;
            this.arrowHeadHalfWidth = 2.25f;
            this.arrowHeadLength = 3f;
            this.arrowBaseScaleFactor = 1f;
            this.arrowScaleFactor = 1f;
            this.arrowShape = new GraphicsPath();
            this.minArrowValue = 1E-06f;
            this.InitDefaults();
        }

        public Arrow(double arrowshafthalfwidth, double arrayshaftlength, double arrowheadhalfwidth, double arrowheadlength)
        {
            this.arrowShaftHalfWidth = 1f;
            this.arrowShaftLength = 7f;
            this.arrowHeadHalfWidth = 2.25f;
            this.arrowHeadLength = 3f;
            this.arrowBaseScaleFactor = 1f;
            this.arrowScaleFactor = 1f;
            this.arrowShape = new GraphicsPath();
            this.minArrowValue = 1E-06f;
            this.InitDefaults();
            this.SetArrow(arrowshafthalfwidth, arrayshaftlength, arrowheadhalfwidth, arrowheadlength);
        }

        public override object Clone()
        {
            Arrow arrow = new Arrow();
            arrow.Copy(this);
            return arrow;
        }

        public void Copy(Arrow source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.arrowShaftHalfWidth = source.arrowShaftHalfWidth;
                this.arrowShaftLength = source.arrowShaftLength;
                this.arrowHeadHalfWidth = source.arrowHeadHalfWidth;
                this.arrowHeadLength = source.arrowHeadLength;
                this.arrowBaseScaleFactor = source.arrowBaseScaleFactor;
                this.arrowScaleFactor = source.arrowScaleFactor;
            }
        }

        private void DefineArrowShape()
        {
            float num = (this.arrowBaseScaleFactor * this.arrowScaleFactor) * this.arrowHeadLength;
            float y = (this.arrowBaseScaleFactor * this.arrowScaleFactor) * this.arrowHeadHalfWidth;
            float num3 = (this.arrowBaseScaleFactor * this.arrowScaleFactor) * this.arrowShaftLength;
            float num4 = (this.arrowBaseScaleFactor * this.arrowScaleFactor) * this.arrowShaftHalfWidth;
            PointF[] points = new PointF[] { new PointF(0f, 0f), new PointF(-num, y), new PointF(-num, num4), new PointF(-(num3 + num), num4), new PointF(-(num3 + num), -num4), new PointF(-num, -num4), new PointF(-num, -y) };
            this.arrowShape.Reset();
            this.arrowShape.AddPolygon(points);
            this.arrowShape.CloseFigure();
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public double GetArrowHeadHalfWidth()
        {
            return (double) this.arrowHeadHalfWidth;
        }

        public double GetArrowHeadLength()
        {
            return (double) this.arrowHeadLength;
        }

        public double GetArrowScaleFactor()
        {
            return (double) this.arrowScaleFactor;
        }

        public double GetArrowShaftHalfWidth()
        {
            return (double) this.arrowShaftHalfWidth;
        }

        public double GetArrowShaftLength()
        {
            return (double) this.arrowShaftLength;
        }

        public GraphicsPath GetArrowShape()
        {
            this.DefineArrowShape();
            return this.arrowShape;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x259;
        }

        public void SetArrow(double arrowshafthalfwidth, double arrayshaftlength, double arrowheadhalfwidth, double arrowheadlength)
        {
            this.arrowShaftHalfWidth = (float) arrowshafthalfwidth;
            this.arrowShaftLength = (float) arrayshaftlength;
            this.arrowHeadHalfWidth = (float) arrowheadhalfwidth;
            this.arrowHeadLength = (float) arrowheadlength;
        }

        public void SetArrowHeadHalfWidth(double width)
        {
            this.arrowHeadHalfWidth = (float) Math.Max((double) this.minArrowValue, width);
        }

        public void SetArrowHeadLength(double length)
        {
            this.arrowHeadLength = (float) Math.Max((double) this.minArrowValue, length);
        }

        public void SetArrowScaleFactor(double length)
        {
            this.arrowScaleFactor = (float) Math.Max((double) this.minArrowValue, length);
        }

        public void SetArrowShaftHalfWidth(double width)
        {
            this.arrowShaftHalfWidth = (float) Math.Max((double) this.minArrowValue, width);
        }

        public void SetArrowShaftLength(double length)
        {
            this.arrowShaftLength = (float) Math.Max((double) this.minArrowValue, length);
        }

        public double ArrowHeadHalfWidth
        {
            get
            {
                return (double) this.arrowHeadHalfWidth;
            }
            set
            {
                this.arrowHeadHalfWidth = (float) Math.Max((double) this.minArrowValue, value);
            }
        }

        public double ArrowHeadLength
        {
            get
            {
                return (double) this.arrowHeadLength;
            }
            set
            {
                this.arrowHeadLength = (float) Math.Max((double) this.minArrowValue, value);
            }
        }

        public double ArrowScaleFactor
        {
            get
            {
                return (double) this.arrowScaleFactor;
            }
            set
            {
                this.arrowScaleFactor = (float) Math.Max((double) this.minArrowValue, value);
            }
        }

        public double ArrowShaftHalfWidth
        {
            get
            {
                return (double) this.arrowShaftHalfWidth;
            }
            set
            {
                this.arrowShaftHalfWidth = (float) Math.Max((double) this.minArrowValue, value);
            }
        }

        public double ArrowShaftLength
        {
            get
            {
                return (double) this.arrowShaftLength;
            }
            set
            {
                this.arrowShaftLength = (float) Math.Max((double) this.minArrowValue, value);
            }
        }
    }
}

