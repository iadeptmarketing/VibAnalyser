namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class PolarAxesLabels : NumericAxisLabels
    {
        internal LinearAxis localAxis;
        internal PolarAxes polarAxes;

        public PolarAxesLabels()
        {
            this.polarAxes = new PolarAxes();
            this.localAxis = new LinearAxis();
            this.InitDefaults();
        }

        public PolarAxesLabels(PolarAxes baseaxis)
        {
            this.polarAxes = new PolarAxes();
            this.localAxis = new LinearAxis();
            this.InitDefaults();
            this.polarAxes = baseaxis;
            if (this.polarAxes != null)
            {
                this.polarAxes.SetPolarAxesLabels(this);
                this.SetChartObjScale(this.polarAxes.GetChartObjScale());
                this.Update();
            }
        }

        private void CalcPolarAxisLabels(Graphics g2)
        {
            int num = 0;
            NumericLabel textobj = new NumericLabel();
            textobj.Copy(this);
            Point2D tickStop = new Point2D();
            new Point2D();
            ArrayList axisTicksArrayList = this.polarAxes.GetAxisTicksArrayList();
            if (this.polarAxes != null)
            {
                int count = axisTicksArrayList.Count;
                textobj.SetColor(this.GetColor());
                textobj.SetChartObjClipping(1);
                textobj.SetTextBgMode(base.GetTextBgMode());
                textobj.SetTextBgColor(base.GetTextBgColor());
                textobj.SetTextRotation(base.textRotation);
                base.lastLabelBoundingBox.SetFrame(0.0, 0.0, 0.0, 0.0);
                for (int i = 0; i < count; i++)
                {
                    TickMark ticmark = (TickMark) axisTicksArrayList[i];
                    if (ticmark.GetTickLabelFlag())
                    {
                        tickStop = ticmark.GetTickStop();
                        double tickLocation = ticmark.GetTickLocation();
                        base.FormatAxisLabel(textobj, ticmark);
                        textobj.SetLocation(tickStop.GetX(), tickStop.GetY(), 0);
                        this.SetOuterLabelsTextJust(textobj, tickLocation);
                        textobj.PreCalcTextBoundingBox(g2);
                        Rectangle2D textBox = textobj.GetTextBox();
                        if ((base.overlapLabelMode == 0) || !textBox.IntersectsWith(base.lastLabelBoundingBox))
                        {
                            textobj.Draw(g2);
                            base.lastLabelBoundingBox = new Rectangle2D(textBox);
                        }
                        num++;
                    }
                }
            }
        }

        public override object Clone()
        {
            PolarAxesLabels labels = new PolarAxesLabels();
            labels.Copy(this);
            return labels;
        }

        public void Copy(PolarAxesLabels source)
        {
            if (source != null)
            {
                this.InitDefaults();
                base.Copy(source);
                this.polarAxes = source.polarAxes;
                this.Update();
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                base.chartObjScale.ChartTransform(g2);
                base.SetResizedTextFont();
                this.localAxis.SetChartObjEnable(this.GetChartObjEnable());
                this.localAxis.Draw(g2);
                base.Draw(g2);
                base.boundingBox.Reset();
                this.CalcPolarAxisLabels(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.polarAxes == null)
                {
                    nerror = 150;
                }
                if (this.localAxis == null)
                {
                    nerror = 150;
                }
            }
            return base.ErrorCheck(nerror);
        }

        public override Axis GetBaseAxis()
        {
            return this.polarAxes;
        }

        public PolarAxes GetBasePolarAxis()
        {
            return this.polarAxes;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x194;
            base.SetTextFont(GraphObj.defaultChartFont);
        }

        public void SetAxisLabels(Font font, int decimalpos, Color labcolor)
        {
            base.SetTextFont(font);
            base.axisLabelsDecimalPos = decimalpos;
            base.chartObjAttributes.SetPrimaryColor(labcolor);
        }

        public override void SetBaseAxis(Axis baseaxis)
        {
            this.polarAxes = (PolarAxes) baseaxis;
            this.Update();
        }

        public void SetBasePolarAxis(PolarAxes axis)
        {
            this.polarAxes = axis;
            this.Update();
        }

        public void SetChartObjScale(PolarCoordinates transform)
        {
            base.SetChartObjScale(transform);
            this.polarAxes.SetChartObjScale(transform);
            this.localAxis.SetChartObjScale(transform);
        }

        private void SetOuterLabelsTextJust(ChartText text, double rangle)
        {
            if ((rangle >= 0.0) && (rangle < 37.5))
            {
                text.SetXJust(0);
                text.SetYJust(1);
                text.SetTextNudge(4.0, 0.0);
            }
            else if ((rangle >= 37.5) && (rangle < 67.5))
            {
                text.SetXJust(0);
                text.SetYJust(0);
                text.SetTextNudge(2.0, -2.0);
            }
            else if ((rangle >= 67.5) && (rangle < 112.5))
            {
                text.SetXJust(1);
                text.SetYJust(0);
                text.SetTextNudge(0.0, -2.0);
            }
            else if ((rangle >= 112.5) && (rangle < 142.5))
            {
                text.SetXJust(2);
                text.SetYJust(0);
                text.SetTextNudge(-2.0, -2.0);
            }
            else if ((rangle >= 142.5) && (rangle < 217.5))
            {
                text.SetXJust(2);
                text.SetYJust(1);
                text.SetTextNudge(-2.0, 0.0);
            }
            else if ((rangle >= 217.5) && (rangle < 247.5))
            {
                text.SetXJust(2);
                text.SetYJust(2);
                text.SetTextNudge(-2.0, 2.0);
            }
            else if ((rangle >= 247.5) && (rangle < 292.5))
            {
                text.SetXJust(1);
                text.SetYJust(2);
                text.SetTextNudge(0.0, 2.0);
            }
            else if ((rangle >= 292.0) && (rangle < 322.5))
            {
                text.SetXJust(0);
                text.SetYJust(2);
                text.SetTextNudge(2.0, 2.0);
            }
            else if (rangle >= 322.5)
            {
                text.SetXJust(0);
                text.SetYJust(1);
                text.SetTextNudge(4.0, 0.0);
            }
        }

        public void Update()
        {
            if (this.polarAxes.GetPolarXAxis() != null)
            {
                this.localAxis = (LinearAxis) this.polarAxes.GetPolarXAxis().Clone();
            }
            if (this.localAxis != null)
            {
                this.localAxis.SetAxisMin(0.0);
                base.InitAxisLabels(this.localAxis);
                this.CalcAutoAxisLabels();
                base.SetAxisLabelsEnds(4);
            }
        }
    }
}

