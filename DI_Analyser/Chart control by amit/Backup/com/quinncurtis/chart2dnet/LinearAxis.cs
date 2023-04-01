namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LinearAxis : Axis
    {
        internal double axisTickSpace;

        public LinearAxis()
        {
            this.axisTickSpace = 1.0;
            this.InitDefaults();
        }

        public LinearAxis(PhysicalCoordinates transform, int axtype)
        {
            this.axisTickSpace = 1.0;
            this.InitDefaults();
            if (transform != null)
            {
                base.InitAxis(transform, axtype, transform.GetStart(axtype), transform.GetStop(axtype));
                this.CalcAutoAxis();
            }
        }

        public LinearAxis(PhysicalCoordinates transform, int axtype, double minval, double maxval)
        {
            this.axisTickSpace = 1.0;
            this.InitDefaults();
            if (transform != null)
            {
                base.InitAxis(transform, axtype, minval, maxval);
                this.CalcAutoAxis(minval, maxval);
            }
        }

        public override void CalcAutoAxis()
        {
            if (base.chartObjScale != null)
            {
                LinearAutoScale scale = new LinearAutoScale(base.chartObjScale, base.axisType, 0);
                base.InitAxis(base.chartObjScale, base.axisType, scale.GetFinalMin(), scale.GetFinalMax());
                if (base.axisType == 0)
                {
                    base.SetAxisIntercept(base.chartObjScale.GetStartY());
                }
                else
                {
                    base.SetAxisIntercept(base.chartObjScale.GetStartX());
                }
                this.SetAxisTicks(scale.GetLabelsOrigin(), scale.GetTickInterval(), scale.GetAxisMinorTicksPerMajor());
                if (base.axisLabels != null)
                {
                    base.axisLabels.SetAxisLabelsDecimalPos(this.CalcAxisLabelsDecimalPos());
                }
            }
        }

        public void CalcAutoAxis(double rmin, double rmax)
        {
            if (base.chartObjScale != null)
            {
                LinearAutoScale scale = new LinearAutoScale(rmin, rmax, base.axisType, 0);
                base.InitAxis(base.chartObjScale, base.axisType, scale.GetFinalMin(), scale.GetFinalMax());
                if (base.axisType == 0)
                {
                    base.SetAxisIntercept(base.chartObjScale.GetStartY());
                }
                else
                {
                    base.SetAxisIntercept(base.chartObjScale.GetStartX());
                }
                this.SetAxisTicks(scale.GetLabelsOrigin(), scale.GetTickInterval(), scale.GetAxisMinorTicksPerMajor());
                if (base.axisLabels != null)
                {
                    base.axisLabels.SetAxisLabelsDecimalPos(this.CalcAxisLabelsDecimalPos());
                }
            }
        }

        public override int CalcAxisLabelsDecimalPos()
        {
            double r = base.GetAxisMinorTicksPerMajor() * this.GetAxisTickSpace();
            return -((int) Math.Floor(ChartSupport.Log10Ex(r)));
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            LinearAxis axis = new LinearAxis();
            axis.Copy(this);
            return axis;
        }

        public void Copy(LinearAxis source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.axisTickSpace = source.axisTickSpace;
            }
        }

        public void DefineLinearAxisTicks()
        {
            double rvalue = 0.0;
            int num2 = 0;
            int num3 = 0;
            int nticktype = 0;
            Point2D startp = new Point2D();
            Point2D stopp = new Point2D();
            int nstaggerlevel = 0;
            double num6 = this.axisTickSpace / 1000.0;
            base.ResetAxisTicks();
            for (num2 = 0; num2 < 2; num2++)
            {
                rvalue = base.axisTickOrigin;
                if (base.axisTickOrigin < base.axisMin)
                {
                    rvalue = base.axisMin;
                }
                if (base.axisTickOrigin > base.axisMax)
                {
                    rvalue = base.axisMax;
                }
                num3 = 0;
                if (num2 == 1)
                {
                    rvalue -= this.axisTickSpace;
                    num3++;
                }
                while ((rvalue <= (base.axisMax + num6)) && (rvalue >= (base.axisMin - num6)))
                {
                    if ((num3 % base.axisMinorTicksPerMajor) != 0)
                    {
                        nticktype = 1;
                    }
                    else
                    {
                        nticktype = 0;
                    }
                    nstaggerlevel = base.majorTickCntr % base.numTickStagger;
                    this.CalcCartesianTickPoint(rvalue, nticktype, startp, stopp, nstaggerlevel);
                    this.AddAxisTick(startp, stopp, rvalue, nticktype);
                    if (num2 == 0)
                    {
                        rvalue += this.axisTickSpace;
                    }
                    else
                    {
                        rvalue -= this.axisTickSpace;
                    }
                    num3++;
                    if (nticktype == 0)
                    {
                        base.majorTickCntr++;
                    }
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            this.FixAxisValues();
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawLinearAxis(base.thePath);
                if (this.GetChartObjEnable() == 1)
                {
                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), base.thePath);
                }
                base.thePath = null;
            }
        }

        public void DrawLinearAxis(GraphicsPath path)
        {
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            this.DefineLinearAxisTicks();
            this.DrawAxis(path);
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.axisTickSpace == 0.0))
            {
                nerror = 140;
            }
            return base.ErrorCheck(nerror);
        }

        private void FixAxisValues()
        {
            double axisMin = base.axisMin;
            double axisMax = base.axisMax;
            base.axisMin = Math.Min(base.axisMin, base.axisMax);
            base.axisMax = Math.Max(base.axisMin, base.axisMax);
            if (base.axisMin == base.axisMax)
            {
                if (base.axisMin == 0.0)
                {
                    base.axisMax = 1.0;
                }
                else if (base.axisMin < 0.0)
                {
                    base.axisMax = 0.0;
                }
                else if (base.axisMin > 0.0)
                {
                    base.axisMin = 0.0;
                }
            }
            if ((axisMin != base.axisMin) || (axisMax != base.axisMax))
            {
                base.axisTickOrigin = base.axisMin;
            }
        }

        public double GetAxisTickSpace()
        {
            return this.axisTickSpace;
        }

        public override AxisLabels GetCompatibleAxisLabels()
        {
            return new NumericAxisLabels(this);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x65;
        }

        public void SetAxisTicks(double tickorigin, double tickspace, int ntickspermajor)
        {
            base.axisTickOrigin = tickorigin;
            this.axisTickSpace = tickspace;
            base.axisMinorTicksPerMajor = ntickspermajor;
            base.updateFlag = true;
        }

        public void SetAxisTicks(double tickorigin, double tickspace, int nminortickspermajor, double minorticklength, double majorticklength, int tickdir)
        {
            base.axisTickOrigin = tickorigin;
            this.axisTickSpace = tickspace;
            base.axisMinorTicksPerMajor = nminortickspermajor;
            base.axisMinorTickLength = minorticklength;
            base.axisMajorTickLength = majorticklength;
            base.axisTickDir = tickdir;
            base.updateFlag = true;
        }

        public void SetAxisTickSpace(double tickspace)
        {
            this.axisTickSpace = tickspace;
            base.updateFlag = true;
        }

        public double AxisTickSpace
        {
            get
            {
                return this.axisTickSpace;
            }
            set
            {
                this.axisTickSpace = value;
            }
        }
    }
}

