namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class NumericAxisLabels : AxisLabels
    {
        internal int axisLabelsDecimalPos;

        public NumericAxisLabels()
        {
            this.axisLabelsDecimalPos = 0;
            this.InitDefaults();
        }

        public NumericAxisLabels(Axis baseaxis)
        {
            this.axisLabelsDecimalPos = 0;
            this.InitAxisLabels(baseaxis);
            this.CalcAutoAxisLabels();
        }

        public override void CalcAutoAxisLabels()
        {
            if (base.baseAxis != null)
            {
                this.axisLabelsDecimalPos = base.baseAxis.CalcAxisLabelsDecimalPos();
                base.axisLabelsDir = base.baseAxis.GetAxisTickDir();
            }
        }

        private void CalcAxisLabels(Graphics g2)
        {
            int num = 0;
            if (base.baseAxis != null)
            {
                NumericLabel textobj = new NumericLabel();
                textobj.Copy(this);
                int axisMajorNthTick = base.baseAxis.GetAxisMajorNthTick();
                ArrayList axisTicksArrayList = base.baseAxis.GetAxisTicksArrayList();
                int count = axisTicksArrayList.Count;
                base.lastLabelBoundingBox.SetFrame(0.0, 0.0, 0.0, 0.0);
                textobj.SetChartObjClipping(1);
                for (int i = 0; i < count; i++)
                {
                    TickMark ticmark = (TickMark) axisTicksArrayList[i];
                    if (ticmark.GetTickLabelFlag())
                    {
                        if ((num % axisMajorNthTick) == 0)
                        {
                            this.FormatAxisLabel(textobj, ticmark);
                            this.OutAxisLabel(g2, textobj, ticmark);
                            base.boundingBox.AddRectangle(textobj.GetBoundingBox().GetRectangleF());
                        }
                        num++;
                    }
                }
                textobj = null;
            }
        }

        public override object Clone()
        {
            NumericAxisLabels labels = new NumericAxisLabels();
            labels.Copy(this);
            return labels;
        }

        public void Copy(NumericAxisLabels source)
        {
            if (source != null)
            {
                this.InitDefaults();
                base.Copy(source);
                this.axisLabelsDecimalPos = source.axisLabelsDecimalPos;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                base.boundingBox.Reset();
                this.CalcAxisLabels(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public void FormatAxisLabel(NumericLabel textobj, TickMark ticmark)
        {
            double tickLocation = ticmark.GetTickLocation();
            int axisLabelsDecimalPos = this.axisLabelsDecimalPos;
            int axisLabelsFormat = this.GetAxisLabelsFormat(ticmark);
            textobj.SetNumericFormat(axisLabelsFormat);
            textobj.SetNumericValue(tickLocation);
            textobj.SetDecimalPos(axisLabelsDecimalPos);
        }

        public int GetAxisLabelsDecimalPos(double r)
        {
            return this.axisLabelsDecimalPos;
        }

        public override int GetAxisLabelsFormat(TickMark tickmark)
        {
            return base.axisLabelsFormat;
        }

        public override ChartLabel GetCompatibleLabel()
        {
            return new NumericLabel();
        }

        public void InitAxisLabels(Axis baseaxis)
        {
            this.InitDefaults();
            base.baseAxis = baseaxis;
            if (base.baseAxis != null)
            {
                this.SetChartObjScale(base.baseAxis.GetChartObjScale());
                base.baseAxis.SetAxisLabels(this);
            }
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x191;
        }

        public void SetAxisLabels(Font font, double rotation, int labdir, int decimalpos, int labelends, Color labcolor)
        {
            base.SetTextFont(font);
            base.textRotation = rotation;
            this.axisLabelsDecimalPos = decimalpos;
            base.axisLabelsDir = labdir;
            base.axisLabelsEnds = labelends;
            base.chartObjAttributes.SetPrimaryColor(labcolor);
        }

        public override void SetAxisLabelsDecimalPos(int decimalpos)
        {
            this.axisLabelsDecimalPos = decimalpos;
        }

        public int AxisLabelsDecimalPos
        {
            get
            {
                return this.axisLabelsDecimalPos;
            }
            set
            {
                this.axisLabelsDecimalPos = value;
            }
        }
    }
}

