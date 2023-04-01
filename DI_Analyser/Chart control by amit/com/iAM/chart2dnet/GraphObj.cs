namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class GraphObj : ChartObj
    {
        internal GraphicsPath boundingBox = new GraphicsPath();
        internal ChartAttribute chartObjAttributes = new ChartAttribute();
        internal int chartObjClipping = 1;
        internal ChartView chartObjComponent = null;
        internal int chartObjEnable = 1;
        internal PhysicalCoordinates chartObjScale = defaultChartObjScale;
        internal bool compositeGraphObj = false;
        internal static Font defaultChartFont = new Font(FontFamily.GenericSansSerif, 11f, FontStyle.Regular);
        internal static PhysicalCoordinates defaultChartObjScale = new CartesianCoordinates();
        internal double intersectionTestDistance = 5.0;
        internal Point2D location = new Point2D();
        internal int moveableType = 0;
        internal int positionType = 1;
        internal double resizeMultiplier = 1.0;
        internal GraphicsPath thePath = null;
        internal bool updateFlag = false;
        internal int zOrder = 50;

        public virtual void AddInternalObjects()
        {
        }

        public abstract bool CheckIntersection(Point2D testpoint, NearestPointData np);
        public void Copy(GraphObj source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.chartObjComponent = source.chartObjComponent;
                this.chartObjScale = source.chartObjScale;
                this.chartObjEnable = source.chartObjEnable;
                this.chartObjClipping = source.chartObjClipping;
                this.moveableType = source.moveableType;
                this.zOrder = source.zOrder;
                this.positionType = source.positionType;
                this.updateFlag = source.updateFlag;
                this.intersectionTestDistance = source.intersectionTestDistance;
                this.resizeMultiplier = source.resizeMultiplier;
                if (source.location != null)
                {
                    this.location = (Point2D) source.location.Clone();
                }
                if (source.chartObjAttributes != null)
                {
                    this.chartObjAttributes = (ChartAttribute) source.chartObjAttributes.Clone();
                }
            }
        }

        public virtual bool DefaultCheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D testr = new Rectangle2D((double) (((int) testpoint.GetX()) - (((int) this.intersectionTestDistance) / 2)), (double) (((int) testpoint.GetY()) - (((int) this.intersectionTestDistance) / 2)), (double) ((int) this.intersectionTestDistance), (double) ((int) this.intersectionTestDistance));
            Rectangle2D boundingBox = this.GetBoundingBox();
            bool flag = false;
            if ((boundingBox != null) && boundingBox.Contains(testr))
            {
                flag = true;
            }
            return flag;
        }

        public abstract void Draw(Graphics g2);
        public override int ErrorCheck(int nerror)
        {
            int num = nerror;
            if ((nerror == 0) && (this.chartObjScale == null))
            {
                nerror = 20;
            }
            return base.ErrorCheck(nerror);
        }

        public virtual Rectangle2D GetBoundingBox()
        {
            return new Rectangle2D(this.boundingBox.GetBounds());
        }

        public virtual ChartAttribute GetChartObjAttributes()
        {
            return new ChartAttribute(this.chartObjAttributes);
        }

        public virtual int GetChartObjClipping()
        {
            return this.chartObjClipping;
        }

        public virtual ChartView GetChartObjComponent()
        {
            return this.chartObjComponent;
        }

        public virtual int GetChartObjEnable()
        {
            return this.chartObjEnable;
        }

        public virtual PhysicalCoordinates GetChartObjScale()
        {
            return this.chartObjScale;
        }

        public virtual Color GetColor()
        {
            return this.chartObjAttributes.GetPrimaryColor();
        }

        public static Font GetDefaultChartFont()
        {
            return defaultChartFont;
        }

        public virtual double GetIntersectionTestDistance()
        {
            return this.intersectionTestDistance;
        }

        public virtual DashStyle GetLineStyle()
        {
            return this.chartObjAttributes.GetLineStyle();
        }

        public virtual double GetLineWidth()
        {
            return this.chartObjAttributes.GetLineWidth();
        }

        public virtual Point2D GetLocation()
        {
            return new Point2D(this.location.GetX(), this.location.GetY());
        }

        public virtual double GetLocation(ChartCalendar xdate)
        {
            xdate.SetCalendarMsecs((long) this.location.GetX());
            return this.location.GetY();
        }

        public virtual Point2D GetLocation(int npositiontype)
        {
            return this.chartObjScale.ConvertCoord(npositiontype, this.location, this.positionType);
        }

        public virtual int GetMoveableType()
        {
            return this.moveableType;
        }

        public virtual int GetPositionType()
        {
            return this.positionType;
        }

        public virtual double GetResizeMultiplier()
        {
            return this.resizeMultiplier;
        }

        public virtual bool GetUpdateFlag()
        {
            return this.updateFlag;
        }

        public virtual int GetZOrder()
        {
            return this.zOrder;
        }

        public virtual void MoveRel(double dx, double dy)
        {
            double x = this.location.GetX();
            double y = this.location.GetY();
            this.location.SetLocation((double) (x + dx), y + dy);
        }

        public virtual void PrePlot(Graphics g2)
        {
            this.thePath = new GraphicsPath();
            this.chartObjScale.ChartTransform(g2);
            this.chartObjScale.SetClippingArea(this.chartObjClipping);
        }

        public virtual void SetChartObjAttributes(ChartAttribute attr)
        {
            this.chartObjAttributes.Copy(attr);
        }

        public virtual void SetChartObjClipping(int clipping)
        {
            this.chartObjClipping = clipping;
        }

        public virtual void SetChartObjComponent(ChartView component)
        {
            this.chartObjComponent = component;
        }

        public virtual void SetChartObjEnable(int benable)
        {
            this.chartObjEnable = benable;
        }

        public virtual void SetChartObjScale(PhysicalCoordinates transform)
        {
            this.chartObjScale = transform;
        }

        public virtual void SetColor(Color rgbcolor)
        {
            this.chartObjAttributes.SetColor(rgbcolor);
        }

        public static void SetDefaultChartFont(Font tfont)
        {
            defaultChartFont = tfont;
        }

        public virtual void SetIntersectionTestDistance(double intersectiontestdistance)
        {
            this.intersectionTestDistance = intersectiontestdistance;
        }

        public virtual void SetLineStyle(DashStyle linestyle)
        {
            this.chartObjAttributes.SetLineStyle(linestyle);
        }

        public virtual void SetLineWidth(double linewidth)
        {
            this.chartObjAttributes.SetLineWidth(linewidth);
        }

        public virtual void SetLocation(Point2D xy)
        {
            this.location.SetLocation(xy);
        }

        public virtual void SetLocation(ChartCalendar xdate, double y)
        {
            double calendarMsecs = xdate.GetCalendarMsecs();
            this.location.SetLocation(calendarMsecs, y);
        }

        public virtual void SetLocation(Point2D xy, int npositiontype)
        {
            this.positionType = npositiontype;
            this.SetLocation(xy);
        }

        public virtual void SetLocation(double x, double y)
        {
            this.location.SetLocation(x, y);
        }

        public virtual void SetLocation(double x, double y, int npositiontype)
        {
            this.positionType = npositiontype;
            this.SetLocation(x, y);
        }

        public virtual void SetPositionType(int posmode)
        {
            this.positionType = posmode;
        }

        public virtual void SetResizeMultiplier(double multiplier)
        {
            this.resizeMultiplier = multiplier;
            ChartAttribute.SetResizeMultiplier(this.resizeMultiplier);
        }

        public virtual void SetUpdateFlag(bool bupdate)
        {
            this.updateFlag = bupdate;
        }

        public virtual void SetZOrder(int zorder)
        {
            this.zOrder = zorder;
        }

        public GraphicsPath BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        public Rectangle2D BoundingBoxRect
        {
            get
            {
                return new Rectangle2D(this.boundingBox.GetBounds());
            }
        }

        public ChartAttribute ChartObjAttributes
        {
            get
            {
                return this.chartObjAttributes;
            }
            set
            {
                this.chartObjAttributes = value;
            }
        }

        public int ChartObjClipping
        {
            get
            {
                return this.chartObjClipping;
            }
            set
            {
                this.chartObjClipping = value;
            }
        }

        public ChartView ChartObjComponent
        {
            get
            {
                return this.chartObjComponent;
            }
            set
            {
                this.chartObjComponent = value;
            }
        }

        public int ChartObjEnable
        {
            get
            {
                return this.chartObjEnable;
            }
            set
            {
                this.chartObjEnable = value;
            }
        }

        public PhysicalCoordinates ChartObjScale
        {
            get
            {
                return this.chartObjScale;
            }
            set
            {
                this.chartObjScale = value;
            }
        }

        public bool CompositeGraphObj
        {
            get
            {
                return this.compositeGraphObj;
            }
            set
            {
                this.compositeGraphObj = value;
            }
        }

        public static Font DefaultChartFont
        {
            get
            {
                return defaultChartFont;
            }
            set
            {
                SetDefaultChartFont(value);
            }
        }

        public Color FillColor
        {
            get
            {
                return this.chartObjAttributes.GetFillColor();
            }
            set
            {
                this.chartObjAttributes.SetFillColor(value);
            }
        }

        public Color LineColor
        {
            get
            {
                return this.chartObjAttributes.GetLineColor();
            }
            set
            {
                this.chartObjAttributes.SetLineColor(value);
            }
        }

        public DashStyle LineStyle
        {
            get
            {
                return this.chartObjAttributes.GetLineStyle();
            }
            set
            {
                this.chartObjAttributes.SetLineStyle(value);
            }
        }

        public double LineWidth
        {
            get
            {
                return this.chartObjAttributes.GetLineWidth();
            }
            set
            {
                this.chartObjAttributes.SetLineWidth(value);
            }
        }

        public int MoveableType
        {
            get
            {
                return this.moveableType;
            }
            set
            {
                this.moveableType = value;
            }
        }

        public int PositionType
        {
            get
            {
                return this.positionType;
            }
            set
            {
                this.positionType = value;
            }
        }

        public double ResizeMultiplier
        {
            get
            {
                return this.resizeMultiplier;
            }
            set
            {
                this.resizeMultiplier = value;
            }
        }

        public GraphicsPath ThePath
        {
            get
            {
                return this.thePath;
            }
            set
            {
                this.thePath = value;
            }
        }

        public int ZOrder
        {
            get
            {
                return this.zOrder;
            }
            set
            {
                this.zOrder = value;
            }
        }
    }
}

