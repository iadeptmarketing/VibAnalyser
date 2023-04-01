namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LogAxis : Axis
    {
        internal int logTickFormat;

        public LogAxis()
        {
            this.logTickFormat = 0;
            this.InitDefaults();
        }

        public LogAxis(PhysicalCoordinates transform, int axtype)
        {
            this.logTickFormat = 0;
            this.InitDefaults();
            base.InitAxis(transform, axtype, transform.GetStart(axtype), transform.GetStop(axtype));
            this.CalcAutoAxis();
        }

        public LogAxis(PhysicalCoordinates transform, int axtype, double minval, double maxval)
        {
            this.logTickFormat = 0;
            this.InitDefaults();
            base.InitAxis(transform, axtype, minval, maxval);
            this.CalcAutoAxis(minval, maxval);
        }

        public override void CalcAutoAxis()
        {
            LogAutoScale scale = new LogAutoScale(base.chartObjScale, base.axisType, 0);
            scale.CalcChartAutoScaleTransform();
            base.InitAxis(base.chartObjScale, base.axisType, scale.GetFinalMin(), scale.GetFinalMax());
            if (base.axisType == 0)
            {
                base.SetAxisIntercept(base.chartObjScale.GetStartY());
            }
            else
            {
                base.SetAxisIntercept(base.chartObjScale.GetStartX());
            }
            this.SetAxisTicks(scale.GetLabelsOrigin(), this.logTickFormat);
            if (base.axisLabels != null)
            {
                base.axisLabels.SetAxisLabelsDecimalPos(scale.GetAxisLabelsDecimalPos());
            }
        }

        public void CalcAutoAxis(double rmin, double rmax)
        {
            LogAutoScale scale = new LogAutoScale(rmin, rmax, base.axisType, 0);
            scale.CalcChartAutoScaleTransform();
            base.InitAxis(base.chartObjScale, base.axisType, scale.GetFinalMin(), scale.GetFinalMax());
            if (base.axisType == 0)
            {
                base.SetAxisIntercept(base.chartObjScale.GetStartY());
            }
            else
            {
                base.SetAxisIntercept(base.chartObjScale.GetStartX());
            }
            this.SetAxisTicks(scale.GetLabelsOrigin(), this.logTickFormat);
            if (base.axisLabels != null)
            {
                base.axisLabels.SetAxisLabelsDecimalPos(scale.GetAxisLabelsDecimalPos());
            }
        }

        public override int CalcAxisLabelsDecimalPos()
        {
            return -99;
        }

        public int CalcAxisLabelsDecimalPos(double r)
        {
            if (r >= 1.0)
            {
                return 0;
            }
            return -((int) Math.Floor(ChartSupport.Log10Ex(r)));
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        private bool CheckLogTickFormat(int i)
        {
            bool flag = false;
            switch (this.logTickFormat)
            {
                case 0:
                    return flag;

                case 1:
                    if (i == 3)
                    {
                        flag = true;
                    }
                    return flag;

                case 2:
                    if ((i != 0) && (i != 3))
                    {
                        return flag;
                    }
                    return true;

                case 3:
                    if (((i != 0) && (i != 1)) && (i != 3))
                    {
                        return flag;
                    }
                    return true;

                case 4:
                    if (((i != 0) && (i != 1)) && ((i != 2) && (i != 3)))
                    {
                        return flag;
                    }
                    return true;

                case 5:
                    if (((i != 0) && (i != 1)) && (((i != 2) && (i != 3)) && (i != 4)))
                    {
                        return flag;
                    }
                    return true;

                case 6:
                    if ((((i != 0) && (i != 1)) && ((i != 2) && (i != 3))) && ((i != 4) && (i != 5)))
                    {
                        return flag;
                    }
                    return true;

                case 7:
                    if ((((i != 0) && (i != 1)) && ((i != 2) && (i != 3))) && (((i != 4) && (i != 5)) && (i != 6)))
                    {
                        return flag;
                    }
                    return true;

                case 8:
                    if ((((i != 0) && (i != 1)) && ((i != 2) && (i != 3))) && (((i != 4) && (i != 5)) && ((i != 6) && (i != 7))))
                    {
                        return flag;
                    }
                    return true;
            }
            return flag;
        }

        public override object Clone()
        {
            LogAxis axis = new LogAxis();
            axis.Copy(this);
            return axis;
        }

        public void Copy(LogAxis source)
        {
            if (source != null)
            {
                this.InitDefaults();
                base.Copy(source);
                this.logTickFormat = source.logTickFormat;
            }
        }

        private void DefineLogAxisTicks()
        {
            double rvalue = 0.0;
            double num2 = 1.0;
            double num3 = 0.0;
            double num4 = 1.0;
            int num5 = 0;
            int i = 0;
            int num7 = 0;
            Point2D startp = new Point2D();
            Point2D stopp = new Point2D();
            int nstaggerlevel = 0;
            num3 = base.axisMin * 0.99;
            num4 = base.axisMax * 1.01;
            num5 = 0;
            base.ResetAxisTicks();
            for (num5 = 0; num5 < 2; num5++)
            {
                rvalue = base.axisTickOrigin;
                if (base.axisTickOrigin < num3)
                {
                    rvalue = num3;
                }
                if (base.axisTickOrigin > num4)
                {
                    rvalue = num4;
                }
                num2 = rvalue;
                num7 = 0;
                if (num5 == 1)
                {
                    num2 = -rvalue / 10.0;
                }
                while ((rvalue <= num4) && (rvalue >= num3))
                {
                    this.CalcCartesianTickPoint(rvalue, 0, startp, stopp, nstaggerlevel);
                    this.AddAxisTick(startp, stopp, rvalue, 0);
                    for (i = 0; i < 8; i++)
                    {
                        rvalue += num2;
                        if ((rvalue <= num4) && (rvalue >= num3))
                        {
                            this.CalcCartesianTickPoint(rvalue, 1, startp, stopp, nstaggerlevel);
                            this.AddAxisTick(startp, stopp, rvalue, 1, this.CheckLogTickFormat(i));
                        }
                    }
                    rvalue += num2;
                    switch (num5)
                    {
                        case 0:
                            num2 = rvalue;
                            break;

                        case 1:
                            num2 = -rvalue / 10.0;
                            break;
                    }
                    num7++;
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            this.FixAxisValues();
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawLogAxis(base.thePath);
                if (this.GetChartObjEnable() == 1)
                {
                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), base.thePath);
                }
            }
        }

        private void DrawLogAxis(GraphicsPath path)
        {
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            this.DefineLogAxisTicks();
            this.DrawAxis(path);
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        private void FixAxisValues()
        {
            double axisMin = base.axisMin;
            double axisMax = base.axisMax;
            base.axisMin = Math.Min(axisMin, axisMax);
            base.axisMax = Math.Max(axisMin, axisMax);
            if ((base.axisMin <= 0.0) && (base.axisMax > 0.0))
            {
                if (base.axisMax >= 10.0)
                {
                    base.axisMin = 1.0;
                }
                else
                {
                    base.axisMin = base.axisMax / 100.0;
                }
            }
            else if ((base.axisMax <= 0.0) && (base.axisMin > 0.0))
            {
                base.axisMax = base.axisMin * 100.0;
            }
            else if ((base.axisMax <= 0.0) && (base.axisMin <= 0.0))
            {
                base.axisMin = 1.0;
                base.axisMax = 100.0;
            }
            if (base.axisMin == base.axisMax)
            {
                base.axisMax = base.axisMin * 100.0;
            }
            if ((axisMin != base.axisMin) || (axisMax != base.axisMax))
            {
                base.axisTickOrigin = base.axisMin;
            }
        }

        public override AxisLabels GetCompatibleAxisLabels()
        {
            return new NumericAxisLabels(this);
        }

        public int GetLogTickFormat()
        {
            return this.logTickFormat;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x66;
        }

        public void SetAxisTicks(double tickorigin, int nlogtickformat)
        {
            base.axisTickOrigin = tickorigin;
            this.logTickFormat = ChartSupport.ClampInt(nlogtickformat, 0, 8);
        }

        public void SetAxisTicks(double origin, int nlogtickformat, double minorticklength, double majorticklength, int tickdir)
        {
            base.axisTickOrigin = origin;
            this.logTickFormat = ChartSupport.ClampInt(nlogtickformat, 0, 8);
            base.axisMinorTickLength = minorticklength;
            base.axisMajorTickLength = majorticklength;
            base.axisTickDir = tickdir;
        }

        public void SetLogTickFormat(int nlogtickformat)
        {
            this.logTickFormat = ChartSupport.ClampInt(nlogtickformat, 0, 8);
        }

        public int LogTickFormat
        {
            get
            {
                return this.logTickFormat;
            }
            set
            {
                this.logTickFormat = value;
            }
        }
    }
}

