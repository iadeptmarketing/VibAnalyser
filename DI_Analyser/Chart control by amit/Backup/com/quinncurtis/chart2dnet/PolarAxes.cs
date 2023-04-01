namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PolarAxes : LinearAxis
    {
        internal PolarAxesLabels polarAxesLabels;
        internal LinearAxis polarXAxis;
        internal LinearAxis polarYAxis;

        public PolarAxes()
        {
            this.polarXAxis = new LinearAxis();
            this.polarYAxis = new LinearAxis();
            this.polarAxesLabels = null;
            this.InitPolarAxesDefaults();
        }

        public PolarAxes(PolarCoordinates transform)
        {
            this.polarXAxis = new LinearAxis();
            this.polarYAxis = new LinearAxis();
            this.polarAxesLabels = null;
            this.InitPolarAxesDefaults();
            this.SetChartObjScale(transform);
            this.Update();
        }

        public override void CalcAutoAxis()
        {
            this.Update();
        }

        private void CalcOuterTicks()
        {
            Point2D startp = new Point2D();
            Point2D stopp = new Point2D();
            double graphAspectRatio = base.chartObjScale.GetGraphAspectRatio();
            Point2D source = new Point2D();
            base.ResetAxisTicks();
            double num15 = this.polarXAxis.GetAxisMajorTickLength() * base.resizeMultiplier;
            double num16 = this.polarXAxis.GetAxisMinorTickLength() * base.resizeMultiplier;
            double axisMax = this.polarXAxis.GetAxisMax();
            Dimension dimension = new Dimension();
            dimension.SetSize(axisMax, axisMax);
            Dimension dimension2 = base.chartObjScale.ConvertDimension(0, dimension, 1);
            source = base.chartObjScale.ConvertCoord(0, source, 1);
            double x = source.GetX();
            double y = source.GetY();
            axisMax = dimension2.GetWidth();
            double num6 = axisMax + num16;
            double num7 = axisMax + num15;
            int num13 = (int) (360.0 / base.axisTickSpace);
            double rtickvalue = 0.0;
            for (int i = 0; i < num13; i++)
            {
                double num3;
                double num4;
                int num14;
                double d = 0.017453292519943295 * rtickvalue;
                double num = axisMax * Math.Cos(d);
                double num2 = (axisMax * Math.Sin(d)) * graphAspectRatio;
                if ((i % base.axisMinorTicksPerMajor) == 0)
                {
                    num3 = num7 * Math.Cos(d);
                    num4 = (num7 * Math.Sin(d)) * graphAspectRatio;
                    num14 = 0;
                }
                else
                {
                    num3 = num6 * Math.Cos(d);
                    num4 = (num6 * Math.Sin(d)) * graphAspectRatio;
                    num14 = 1;
                }
                startp.SetLocation((double) (x + num), y - num2);
                stopp.SetLocation((double) (x + num3), y - num4);
                this.AddAxisTick(startp, stopp, rtickvalue, num14);
                rtickvalue += base.axisTickSpace;
            }
        }

        public override object Clone()
        {
            PolarAxes axes = new PolarAxes();
            axes.Copy(this);
            return axes;
        }

        public void Copy(PolarAxes source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.polarXAxis = (LinearAxis) source.polarXAxis.Clone();
                this.polarYAxis = (LinearAxis) source.polarYAxis.Clone();
                this.polarAxesLabels = source.polarAxesLabels;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.SetChartObjScale((PolarCoordinates) this.GetChartObjScale());
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                this.polarXAxis.SetChartObjEnable(this.GetChartObjEnable());
                this.polarXAxis.Draw(g2);
                this.polarYAxis.SetChartObjEnable(this.GetChartObjEnable());
                this.polarYAxis.Draw(g2);
                this.PrePlot(g2);
                this.CalcOuterTicks();
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                this.DrawAxisTicks(base.thePath);
                this.DrawOuterCircle(base.thePath);
                base.boundingBox.Reset();
                base.boundingBox.AddPath(base.thePath, false);
                if (this.GetChartObjEnable() == 1)
                {
                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), base.thePath);
                }
                base.thePath = null;
            }
        }

        private void DrawOuterCircle(GraphicsPath path)
        {
            Point2D dest = new Point2D();
            Point2D pointd2 = new Point2D();
            Arc2D arcd = new Arc2D();
            dest.SetLocation(this.polarXAxis.GetAxisMin(), this.polarYAxis.GetAxisMin());
            pointd2.SetLocation(this.polarXAxis.GetAxisMax(), this.polarYAxis.GetAxisMax());
            base.chartObjScale.ConvertCoord(dest, 0, dest, 1);
            base.chartObjScale.ConvertCoord(pointd2, 0, pointd2, 1);
            double ww = pointd2.GetX() - dest.GetX();
            double hh = dest.GetY() - pointd2.GetY();
            arcd.SetFrame(dest.GetX(), pointd2.GetY(), ww, hh);
            path.AddEllipse(arcd.GetRectangle());
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if ((this.polarXAxis == null) || (this.polarYAxis == null))
                {
                    nerror = 100;
                }
                if (nerror == 0)
                {
                    nerror = this.polarXAxis.ErrorCheck(nerror);
                }
                if (nerror == 0)
                {
                    nerror = this.polarYAxis.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public AxisLabels GetCompatibleAxesLabels()
        {
            return new PolarAxesLabels(this);
        }

        public PolarAxesLabels GetPolarAxesLabels()
        {
            return this.polarAxesLabels;
        }

        public LinearAxis GetPolarXAxis()
        {
            return this.polarXAxis;
        }

        public LinearAxis GetPolarYAxis()
        {
            return this.polarYAxis;
        }

        public void InitPolarAxesDefaults()
        {
            base.chartObjType = 0x7e;
            base.axisTickSpace = 15.0;
            base.axisMinorTicksPerMajor = 1;
        }

        public override void SetChartObjAttributes(ChartAttribute attr)
        {
            base.SetChartObjAttributes(attr);
            this.polarXAxis.SetChartObjAttributes(attr);
            this.polarYAxis.SetChartObjAttributes(attr);
        }

        public void SetChartObjScale(PolarCoordinates transform)
        {
            base.SetChartObjScale(transform);
            if (this.polarXAxis != null)
            {
                this.polarXAxis.SetChartObjScale(base.chartObjScale);
            }
            if (this.polarYAxis != null)
            {
                this.polarYAxis.SetChartObjScale(base.chartObjScale);
            }
        }

        public void SetPolarAxesLabels(PolarAxesLabels axeslabels)
        {
            this.polarAxesLabels = axeslabels;
        }

        public void SetPolarAxesTicks(double axestickspace, int axesntickspermajor, double angletickspace, int anglentickspermajor)
        {
            base.SetAxisTicks(0.0, angletickspace, anglentickspermajor);
            this.polarXAxis.SetAxisTicks(0.0, axestickspace, axesntickspermajor);
            this.polarYAxis.SetAxisTicks(0.0, axestickspace, axesntickspermajor);
        }

        public void SetPolarAxesTicks(double axestickspace, int axesntickspermajor, double angletickspace, int anglentickspermajor, double minorticklength, double majorticklength, int tickdir)
        {
            this.SetPolarAxesTicks(axestickspace, axesntickspermajor, angletickspace, anglentickspermajor);
            base.SetAxisTicks(0.0, angletickspace, axesntickspermajor, minorticklength, majorticklength, tickdir);
            this.polarXAxis.SetAxisTicks(0.0, axestickspace, axesntickspermajor, minorticklength, majorticklength, tickdir);
            this.polarYAxis.SetAxisTicks(0.0, axestickspace, axesntickspermajor, minorticklength, majorticklength, tickdir);
        }

        public void SetPolarXAxis(LinearAxis axis)
        {
            this.polarXAxis = axis;
        }

        public void SetPolarYAxis(LinearAxis axis)
        {
            this.polarYAxis = axis;
        }

        private void Update()
        {
            base.InitAxis(base.chartObjScale, 0, base.chartObjScale.GetStart(0), base.chartObjScale.GetStop(0));
            this.polarXAxis = new LinearAxis(base.chartObjScale, 0);
            this.polarXAxis.CalcAutoAxis();
            this.polarXAxis.SetAxisIntercept(0.0);
            this.polarXAxis.SetAxisTickDir(1);
            this.polarYAxis = new LinearAxis(base.chartObjScale, 1);
            this.polarYAxis.CalcAutoAxis();
            this.polarYAxis.SetAxisIntercept(0.0);
            this.polarYAxis.SetAxisTickDir(1);
        }
    }
}

