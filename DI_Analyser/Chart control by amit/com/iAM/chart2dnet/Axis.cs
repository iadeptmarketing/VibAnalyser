namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class Axis : GraphObj
    {
        internal AxisLabels axisLabels;
        internal bool axisLineEnable;
        internal int axisMajorNthTick;
        internal double axisMajorTickLength;
        internal double axisMax;
        internal double axisMin;
        internal int axisMinorNthTick;
        internal double axisMinorTickLength;
        internal int axisMinorTicksPerMajor;
        internal double axisNormIntercept;
        internal int axisTickDir;
        internal double axisTickOrigin;
        internal ArrayList axisTicksArrayList;
        internal bool axisTicksEnable;
        internal int axisType;
        internal int majorTickCntr;
        internal int maxNumTickMarks;
        internal int numTickStagger;
        internal double staggerDistance;

        public Axis()
        {
            this.axisLabels = null;
            this.axisTicksArrayList = new ArrayList(100);
            this.axisType = 0;
            this.axisTickDir = 0;
            this.axisMin = 0.0;
            this.axisMax = 1.0;
            this.axisMinorTickLength = 5.0;
            this.axisMajorTickLength = 10.0;
            this.staggerDistance = 12.0;
            this.numTickStagger = 1;
            this.majorTickCntr = 0;
            this.axisTickOrigin = 0.0;
            this.axisNormIntercept = 0.2;
            this.axisMinorNthTick = 1;
            this.axisMajorNthTick = 1;
            this.axisMinorTicksPerMajor = 5;
            this.axisLineEnable = true;
            this.axisTicksEnable = true;
            this.maxNumTickMarks = 0x3e8;
            this.InitDefaults();
        }

        public Axis(PhysicalCoordinates transform, int axtype)
        {
            this.axisLabels = null;
            this.axisTicksArrayList = new ArrayList(100);
            this.axisType = 0;
            this.axisTickDir = 0;
            this.axisMin = 0.0;
            this.axisMax = 1.0;
            this.axisMinorTickLength = 5.0;
            this.axisMajorTickLength = 10.0;
            this.staggerDistance = 12.0;
            this.numTickStagger = 1;
            this.majorTickCntr = 0;
            this.axisTickOrigin = 0.0;
            this.axisNormIntercept = 0.2;
            this.axisMinorNthTick = 1;
            this.axisMajorNthTick = 1;
            this.axisMinorTicksPerMajor = 5;
            this.axisLineEnable = true;
            this.axisTicksEnable = true;
            this.maxNumTickMarks = 0x3e8;
            this.InitDefaults();
            this.InitAxis(transform, axtype, transform.GetStart(axtype), transform.GetStop(axtype));
        }

        public Axis(PhysicalCoordinates transform, int axtype, double minval, double maxval)
        {
            this.axisLabels = null;
            this.axisTicksArrayList = new ArrayList(100);
            this.axisType = 0;
            this.axisTickDir = 0;
            this.axisMin = 0.0;
            this.axisMax = 1.0;
            this.axisMinorTickLength = 5.0;
            this.axisMajorTickLength = 10.0;
            this.staggerDistance = 12.0;
            this.numTickStagger = 1;
            this.majorTickCntr = 0;
            this.axisTickOrigin = 0.0;
            this.axisNormIntercept = 0.2;
            this.axisMinorNthTick = 1;
            this.axisMajorNthTick = 1;
            this.axisMinorTicksPerMajor = 5;
            this.axisLineEnable = true;
            this.axisTicksEnable = true;
            this.maxNumTickMarks = 0x3e8;
            this.InitDefaults();
            this.InitAxis(transform, axtype, minval, maxval);
        }

        public int AddAxisTick(double rtickvalue, int ticktype)
        {
            this.maxNumTickMarks = this.CalcMaxNumTickMarks();
            if (this.axisTicksArrayList.Count < this.maxNumTickMarks)
            {
                TickMark mark = new TickMark(rtickvalue, ticktype);
                this.axisTicksArrayList.Add(mark);
            }
            return this.axisTicksArrayList.Count;
        }

        public virtual int AddAxisTick(Point2D startp, Point2D stopp, double rtickvalue, int ticktype)
        {
            this.maxNumTickMarks = this.CalcMaxNumTickMarks();
            if (this.axisTicksArrayList.Count < this.maxNumTickMarks)
            {
                TickMark mark = new TickMark(startp, stopp, rtickvalue, ticktype);
                this.axisTicksArrayList.Add(mark);
            }
            return this.axisTicksArrayList.Count;
        }

        public virtual int AddAxisTick(Point2D startp, Point2D stopp, double rtickvalue, ChartCalendar dtickdate, int ticktype)
        {
            this.maxNumTickMarks = this.CalcMaxNumTickMarks();
            if (this.axisTicksArrayList.Count < this.maxNumTickMarks)
            {
                ChartCalendar calendar = null;
                if (dtickdate != null)
                {
                    calendar = (ChartCalendar) dtickdate.Clone();
                }
                TickMark mark = new TickMark(startp, stopp, rtickvalue, calendar, ticktype);
                this.axisTicksArrayList.Add(mark);
            }
            return this.axisTicksArrayList.Count;
        }

        public virtual int AddAxisTick(Point2D startp, Point2D stopp, double rtickvalue, int ticktype, bool blabelf)
        {
            this.maxNumTickMarks = this.CalcMaxNumTickMarks();
            if (this.axisTicksArrayList.Count < this.maxNumTickMarks)
            {
                TickMark mark = new TickMark(startp, stopp, rtickvalue, ticktype, blabelf);
                this.axisTicksArrayList.Add(mark);
            }
            return this.axisTicksArrayList.Count;
        }

        public abstract void CalcAutoAxis();
        public abstract int CalcAxisLabelsDecimalPos();
        public double CalcAxisNormIntercept()
        {
            double y;
            if (this.axisType == 0)
            {
                y = this.GetLocation().GetY();
            }
            else
            {
                y = this.GetLocation().GetX();
            }
            Point2D source = new Point2D();
            Point2D pointd2 = new Point2D();
            if (this.axisType == 0)
            {
                source.SetLocation((double) 0.0, y);
            }
            else
            {
                source.SetLocation(y, 0.0);
            }
            pointd2 = base.chartObjScale.ConvertCoord(4, source, 1);
            if (this.axisType == 0)
            {
                this.axisNormIntercept = pointd2.GetY();
            }
            else
            {
                this.axisNormIntercept = pointd2.GetX();
            }
            return this.axisNormIntercept;
        }

        public virtual void CalcCartesianTickPoint(double rvalue, int nticktype, Point2D startp, Point2D stopp, int nstaggerlevel)
        {
            double px = 0.0;
            double py = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 1.0;
            double axisIntercept = this.GetAxisIntercept();
            Point2D pointd = new Point2D();
            Point2D source = new Point2D();
            if (this.axisType == 0)
            {
                source.SetLocation(rvalue, axisIntercept);
            }
            else if (this.axisType == 1)
            {
                source.SetLocation(axisIntercept, rvalue);
            }
            if (nticktype == 1)
            {
                num5 = this.axisMinorTickLength * base.resizeMultiplier;
            }
            else
            {
                num5 = this.axisMajorTickLength * base.resizeMultiplier;
                if (this.axisTickDir != 1)
                {
                    num5 += nstaggerlevel * this.staggerDistance;
                }
            }
            pointd = base.chartObjScale.ConvertCoord(0, source, 1);
            num5++;
            px = pointd.GetX();
            py = pointd.GetY();
            num3 = px;
            num4 = py;
            if (this.axisType == 0)
            {
                switch (this.axisTickDir)
                {
                    case 0:
                        num4 += num5;
                        goto Label_0187;

                    case 1:
                        num4 += num5 / 2.0;
                        py -= num5 / 2.0;
                        goto Label_0187;

                    case 2:
                        num4 -= num5;
                        goto Label_0187;
                }
            }
            else if (this.axisType == 1)
            {
                switch (this.axisTickDir)
                {
                    case 0:
                        num3 -= num5;
                        goto Label_0187;

                    case 1:
                        num3 -= num5 / 2.0;
                        px += num5 / 2.0;
                        goto Label_0187;

                    case 2:
                        num3 += num5;
                        goto Label_0187;
                }
            }
        Label_0187:
            startp.SetLocation(px, py);
            stopp.SetLocation(num3, num4);
        }

        protected int CalcMaxNumTickMarks()
        {
            if (this.axisType == 1)
            {
                return (int) base.ChartObjScale.GetUserViewport().Height;
            }
            return (int) base.ChartObjScale.GetUserViewport().Width;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D rectangled = new Rectangle2D();
            bool flag = false;
            Point2D dest = new Point2D();
            Point2D pointd2 = new Point2D();
            double axisIntercept = this.GetAxisIntercept();
            if (this.axisType == 0)
            {
                dest.SetLocation(this.axisMin, axisIntercept);
                pointd2.SetLocation(this.axisMax, axisIntercept);
            }
            else
            {
                dest.SetLocation(axisIntercept, this.axisMin);
                pointd2.SetLocation(axisIntercept, this.axisMax);
            }
            base.chartObjScale.ConvertCoord(dest, 0, dest, 1);
            base.chartObjScale.ConvertCoord(pointd2, 0, pointd2, 1);
            if (this.axisType == 0)
            {
                dest.SetLocation(dest.GetX(), dest.GetY() + base.intersectionTestDistance);
                pointd2.SetLocation(pointd2.GetX(), pointd2.GetY() - base.intersectionTestDistance);
            }
            else
            {
                dest.SetLocation((double) (dest.GetX() + base.intersectionTestDistance), dest.GetY());
                pointd2.SetLocation((double) (pointd2.GetX() - base.intersectionTestDistance), pointd2.GetY());
            }
            rectangled.SetFrameFromDiagonal(dest, pointd2);
            if (rectangled.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
            {
                flag = true;
            }
            return flag;
        }

        public void Copy(Axis source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.axisLabels = source.axisLabels;
                this.axisType = source.axisType;
                this.axisTickDir = source.axisTickDir;
                this.axisMin = source.axisMin;
                this.axisMax = source.axisMax;
                this.staggerDistance = source.staggerDistance;
                this.numTickStagger = source.numTickStagger;
                this.axisMinorTickLength = source.axisMinorTickLength;
                this.axisMajorTickLength = source.axisMajorTickLength;
                this.axisTickOrigin = source.axisTickOrigin;
                this.axisNormIntercept = source.axisNormIntercept;
                this.axisMinorNthTick = source.axisMinorNthTick;
                this.axisMajorNthTick = source.axisMajorNthTick;
                this.axisMinorTicksPerMajor = source.axisMinorTicksPerMajor;
            }
        }

        public virtual void DrawAxis(GraphicsPath path)
        {
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            if (this.axisLineEnable)
            {
                this.DrawAxisLine(path);
            }
            if (this.axisTicksEnable)
            {
                this.DrawAxisTicks(path);
            }
            base.boundingBox.Reset();
            base.boundingBox.AddPath(path, false);
        }

        public virtual void DrawAxisLine(GraphicsPath path)
        {
            double axisIntercept = this.GetAxisIntercept();
            if (this.axisType == 0)
            {
                base.chartObjScale.WLineAbs(path, this.axisMin, axisIntercept, this.axisMax, axisIntercept);
            }
            else if (this.axisType == 1)
            {
                base.chartObjScale.WLineAbs(path, axisIntercept, this.axisMin, axisIntercept, this.axisMax);
            }
        }

        public virtual void DrawAxisTicks(GraphicsPath path)
        {
            int count = this.axisTicksArrayList.Count;
            for (int i = 0; i < count; i++)
            {
                TickMark mark = (TickMark) this.axisTicksArrayList[i];
                Point2D tickStart = mark.GetTickStart();
                Point2D tickStop = mark.GetTickStop();
                base.chartObjScale.PLineAbs(path, tickStart.GetX(), tickStart.GetY(), tickStop.GetX(), tickStop.GetY());
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.axisMin == this.axisMax)
                {
                    nerror = 130;
                }
                if ((nerror == 0) && (this.axisLabels != null))
                {
                    nerror = this.axisLabels.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public double GetAxisIntercept()
        {
            if (this.axisType == 0)
            {
                return this.GetLocation().GetY();
            }
            return this.GetLocation().GetX();
        }

        public AxisLabels GetAxisLabels()
        {
            return this.axisLabels;
        }

        public int GetAxisMajorNthTick()
        {
            return this.axisMajorNthTick;
        }

        public double GetAxisMajorTickLength()
        {
            return this.axisMajorTickLength;
        }

        public double GetAxisMax()
        {
            return this.axisMax;
        }

        public double GetAxisMin()
        {
            return this.axisMin;
        }

        public int GetAxisMinorNthTick()
        {
            return this.axisMinorNthTick;
        }

        public double GetAxisMinorTickLength()
        {
            return this.axisMinorTickLength;
        }

        public int GetAxisMinorTicksPerMajor()
        {
            return this.axisMinorTicksPerMajor;
        }

        public double GetAxisNormIntercept()
        {
            return this.axisNormIntercept;
        }

        public double GetAxisRange()
        {
            return Math.Abs((double) (this.axisMax - this.axisMin));
        }

        public int GetAxisTickDir()
        {
            return this.axisTickDir;
        }

        public double GetAxisTickOrigin()
        {
            return this.axisTickOrigin;
        }

        public ArrayList GetAxisTicksArrayList()
        {
            return this.axisTicksArrayList;
        }

        public int GetAxisType()
        {
            return this.axisType;
        }

        public abstract AxisLabels GetCompatibleAxisLabels();
        public TickMark GetLastTickMark()
        {
            int num = this.axisTicksArrayList.Count - 1;
            return (TickMark) this.axisTicksArrayList[num];
        }

        public int GetNumTickStagger()
        {
            return this.numTickStagger;
        }

        public double GetStaggerDistance()
        {
            return this.staggerDistance;
        }

        public void InitAxis(PhysicalCoordinates transform, int axtype)
        {
            this.InitAxis(transform, axtype, transform.GetStart(axtype), transform.GetStop(axtype));
        }

        public void InitAxis(PhysicalCoordinates transform, int axtype, double minval, double maxval)
        {
            this.SetChartObjScale(transform);
            this.axisType = axtype;
            this.axisMin = Math.Min(minval, maxval);
            this.axisMax = Math.Max(minval, maxval);
            this.axisTickOrigin = this.axisMin;
        }

        private void InitDefaults()
        {
            base.chartObjType = 100;
            base.chartObjAttributes.SetLineAttributes(Color.Black, 1.0, DashStyle.Solid);
            base.moveableType = 1;
            base.chartObjClipping = 1;
            base.zOrder = 100;
        }

        public override void PrePlot(Graphics g2)
        {
            base.thePath = new GraphicsPath();
            base.chartObjScale.ChartTransform(g2);
            base.chartObjScale.SetClippingArea(base.chartObjClipping);
        }

        public void ResetAxisTicks()
        {
            this.axisTicksArrayList.Clear();
            this.majorTickCntr = 0;
        }

        public void RestoreAxisNormIntercept()
        {
            Point2D pointd = new Point2D();
            Point2D source = new Point2D();
            if (this.axisType == 0)
            {
                source.SetLocation((double) 0.0, this.axisNormIntercept);
            }
            else
            {
                source.SetLocation(this.axisNormIntercept, 0.0);
            }
            pointd = base.chartObjScale.ConvertCoord(1, source, 4);
            if (this.axisType == 0)
            {
                this.SetAxisIntercept(pointd.GetY());
            }
            else
            {
                this.SetAxisIntercept(pointd.GetX());
            }
        }

        public void SetAxisAttrib(Color axcolor, int axthickness, DashStyle axstyle)
        {
            base.chartObjAttributes.SetPrimaryColor(axcolor);
            base.chartObjAttributes.SetLineWidth((double) axthickness);
            base.chartObjAttributes.SetLineStyle(axstyle);
        }

        public void SetAxisIntercept(double intercept)
        {
            if (this.axisType == 0)
            {
                this.SetLocation((double) 0.0, intercept);
            }
            else
            {
                this.SetLocation(intercept, 0.0);
            }
        }

        public void SetAxisLabels(AxisLabels axislabels)
        {
            this.axisLabels = axislabels;
        }

        public void SetAxisLimits(double minval, double maxval)
        {
            this.axisMin = Math.Min(minval, maxval);
            this.axisMax = Math.Max(minval, maxval);
        }

        public void SetAxisMajorNthTick(int nmajornthtick)
        {
            this.axisMajorNthTick = nmajornthtick;
        }

        public void SetAxisMajorTickLength(double ticklength)
        {
            this.axisMajorTickLength = ticklength;
        }

        public void SetAxisMax(double maxval)
        {
            this.SetAxisLimits(this.axisMin, maxval);
        }

        public void SetAxisMin(double minval)
        {
            this.SetAxisLimits(minval, this.axisMax);
        }

        public void SetAxisMinorNthTick(int nminornthtick)
        {
            this.axisMinorNthTick = nminornthtick;
        }

        public void SetAxisMinorTickLength(double ticklength)
        {
            this.axisMinorTickLength = ticklength;
        }

        public void SetAxisMinorTicksPerMajor(int ntickmajor)
        {
            this.axisMinorTicksPerMajor = ntickmajor;
        }

        public void SetAxisNormIntercept(double normintercept)
        {
            this.axisNormIntercept = normintercept;
        }

        public void SetAxisTickDir(int tickdir)
        {
            this.axisTickDir = tickdir;
        }

        public void SetAxisTickOrigin(double tickorigin)
        {
            this.axisTickOrigin = tickorigin;
        }

        public void SetAxisTicksAttributes(double minorticklength, double majorticklength, int tickdir)
        {
            this.axisMinorTickLength = minorticklength;
            this.axisMajorTickLength = majorticklength;
            this.axisTickDir = tickdir;
        }

        public void SetAxisType(int axtype)
        {
            this.axisType = axtype;
        }

        public void SetNumTickStagger(int nnumstagger)
        {
            this.numTickStagger = nnumstagger;
        }

        public void SetStaggerDistance(double rstagger)
        {
            this.staggerDistance = rstagger;
        }

        public double AxisIntercept
        {
            get
            {
                return this.GetAxisIntercept();
            }
            set
            {
                this.SetAxisIntercept(value);
            }
        }

        public AxisLabels AxisLabelsObj
        {
            get
            {
                return this.axisLabels;
            }
        }

        public bool AxisLineEnable
        {
            get
            {
                return this.axisLineEnable;
            }
            set
            {
                this.axisLineEnable = value;
            }
        }

        public int AxisMajorNthTick
        {
            get
            {
                return this.axisMajorNthTick;
            }
            set
            {
                this.axisMajorNthTick = value;
            }
        }

        public double AxisMajorTickLength
        {
            get
            {
                return this.axisMajorTickLength;
            }
            set
            {
                this.axisMajorTickLength = value;
            }
        }

        public double AxisMax
        {
            get
            {
                return this.GetAxisMax();
            }
            set
            {
                this.SetAxisMax(value);
            }
        }

        public double AxisMin
        {
            get
            {
                return this.GetAxisMin();
            }
            set
            {
                this.SetAxisMin(value);
            }
        }

        public int AxisMinorNthTick
        {
            get
            {
                return this.axisMinorNthTick;
            }
            set
            {
                this.axisMinorNthTick = value;
            }
        }

        public double AxisMinorTickLength
        {
            get
            {
                return this.axisMinorTickLength;
            }
            set
            {
                this.axisMinorTickLength = value;
            }
        }

        public int AxisMinorTicksPerMajor
        {
            get
            {
                return this.axisMinorTicksPerMajor;
            }
            set
            {
                this.axisMinorTicksPerMajor = value;
            }
        }

        public double AxisNormIntercept
        {
            get
            {
                this.CalcAxisNormIntercept();
                return this.axisNormIntercept;
            }
            set
            {
                this.axisNormIntercept = value;
                this.RestoreAxisNormIntercept();
            }
        }

        public int AxisTickDir
        {
            get
            {
                return this.axisTickDir;
            }
            set
            {
                this.axisTickDir = value;
            }
        }

        public double AxisTickOrigin
        {
            get
            {
                return this.axisTickOrigin;
            }
            set
            {
                this.axisTickOrigin = value;
            }
        }

        public ArrayList AxisTicksArrayList
        {
            get
            {
                return this.axisTicksArrayList;
            }
            set
            {
                this.axisTicksArrayList = value;
            }
        }

        public bool AxisTicksEnable
        {
            get
            {
                return this.axisTicksEnable;
            }
            set
            {
                this.axisTicksEnable = value;
            }
        }

        public int AxisType
        {
            get
            {
                return this.axisType;
            }
            set
            {
                this.axisType = value;
            }
        }

        public int NumTickStagger
        {
            get
            {
                return this.numTickStagger;
            }
            set
            {
                this.numTickStagger = value;
            }
        }

        public double StaggerDistance
        {
            get
            {
                return this.staggerDistance;
            }
            set
            {
                this.staggerDistance = value;
            }
        }
    }
}

