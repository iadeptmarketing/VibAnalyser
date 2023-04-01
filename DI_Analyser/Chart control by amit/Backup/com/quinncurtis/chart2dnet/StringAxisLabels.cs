namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class StringAxisLabels : AxisLabels
    {
        internal int numTickStrings;
        internal string[] userTickStrings;

        public StringAxisLabels()
        {
            this.numTickStrings = 0;
            this.InitDefaults();
        }

        public StringAxisLabels(Axis baseaxis)
        {
            this.numTickStrings = 0;
            this.InitAxisLabels(baseaxis);
            this.CalcAutoAxisLabels();
        }

        public override void CalcAutoAxisLabels()
        {
            if (base.baseAxis != null)
            {
                base.axisLabelsDir = base.baseAxis.GetAxisTickDir();
            }
        }

        private void CalcAxisLabels(Graphics g2)
        {
            int num = 0;
            int strcntr = 0;
            if (base.baseAxis != null)
            {
                StringLabel textobj = new StringLabel();
                textobj.Copy(this);
                int axisMajorNthTick = base.baseAxis.GetAxisMajorNthTick();
                ArrayList axisTicksArrayList = base.baseAxis.GetAxisTicksArrayList();
                int count = axisTicksArrayList.Count;
                textobj.SetChartObjClipping(1);
                base.lastLabelBoundingBox.SetFrame(0.0, 0.0, 0.0, 0.0);
                for (int i = 0; i < count; i++)
                {
                    TickMark ticmark = (TickMark) axisTicksArrayList[i];
                    if (ticmark.GetTickLabelFlag())
                    {
                        if ((num % axisMajorNthTick) == 0)
                        {
                            this.FormatAxisLabel(textobj, strcntr);
                            this.OutAxisLabel(g2, textobj, ticmark);
                            base.boundingBox.AddRectangle(textobj.GetBoundingBox().GetRectangleF());
                            strcntr++;
                        }
                        num++;
                    }
                }
                textobj = null;
            }
        }

        public override object Clone()
        {
            StringAxisLabels labels = new StringAxisLabels();
            labels.Copy(this);
            return labels;
        }

        public void Copy(StringAxisLabels source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.numTickStrings = source.numTickStrings;
                this.userTickStrings = new string[this.numTickStrings];
                for (int i = 0; i < this.numTickStrings; i++)
                {
                    this.userTickStrings[i] = string.Copy(source.userTickStrings[i]);
                }
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
            if ((this.numTickStrings > 0) && (this.userTickStrings == null))
            {
                nerror = 110;
            }
            return base.ErrorCheck(nerror);
        }

        protected void FormatAxisLabel(StringLabel textobj, int strcntr)
        {
            string thestring = "";
            if ((this.numTickStrings > 0) && (strcntr < this.numTickStrings))
            {
                thestring = this.userTickStrings[strcntr];
            }
            textobj.SetTextString(thestring);
        }

        public override int GetAxisLabelsFormat(TickMark tickmark)
        {
            return base.axisLabelsFormat;
        }

        public string[] GetAxisLabelsStrings()
        {
            return this.userTickStrings;
        }

        public override ChartLabel GetCompatibleLabel()
        {
            return new StringLabel();
        }

        public int GetNumAxisLabelsStrings()
        {
            return this.numTickStrings;
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
            base.chartObjType = 0x192;
        }

        public void SetAxisLabels(Font font, double rotation, int labdir, int labelends, Color labcolor, string[] tickstring1s, int numtickstring1s)
        {
            base.SetTextFont(font);
            base.textRotation = rotation;
            base.axisLabelsDir = labdir;
            base.axisLabelsEnds = labelends;
            base.chartObjAttributes.SetPrimaryColor(labcolor);
            this.SetAxisLabelsStrings(tickstring1s, numtickstring1s);
        }

        public override void SetAxisLabelsDecimalPos(int decimalpos)
        {
        }

        public void SetAxisLabelsStrings(string[] tstring1s, int n)
        {
            this.numTickStrings = Math.Min(tstring1s.Length, n);
            if (this.numTickStrings > 0)
            {
                this.userTickStrings = new string[this.numTickStrings];
                for (int i = 0; i < this.numTickStrings; i++)
                {
                    this.userTickStrings[i] = string.Copy(tstring1s[i]);
                }
            }
        }
    }
}

