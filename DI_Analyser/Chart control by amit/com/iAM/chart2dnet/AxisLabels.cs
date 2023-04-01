namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public abstract class AxisLabels : ChartText
    {
        internal int axisLabelsDir;
        internal int axisLabelsEnds;
        internal int axisLabelsFormat;
        internal double axisLabelsTickOffsetX;
        internal double axisLabelsTickOffsetY;
        internal Axis baseAxis;
        internal Rectangle2D lastLabelBoundingBox;
        internal int overlapLabelMode;

        public AxisLabels()
        {
            this.baseAxis = new LinearAxis();
            this.axisLabelsDir = 0;
            this.axisLabelsEnds = 7;
            this.axisLabelsTickOffsetX = 1.0;
            this.axisLabelsTickOffsetY = 1.0;
            this.axisLabelsFormat = 1;
            this.lastLabelBoundingBox = new Rectangle2D();
            this.overlapLabelMode = 1;
            this.InitDefaults();
        }

        public AxisLabels(Axis baseaxis)
        {
            this.baseAxis = new LinearAxis();
            this.axisLabelsDir = 0;
            this.axisLabelsEnds = 7;
            this.axisLabelsTickOffsetX = 1.0;
            this.axisLabelsTickOffsetY = 1.0;
            this.axisLabelsFormat = 1;
            this.lastLabelBoundingBox = new Rectangle2D();
            this.overlapLabelMode = 1;
            this.InitDefaults();
            this.baseAxis = baseaxis;
            if (this.baseAxis != null)
            {
                this.axisLabelsDir = this.baseAxis.GetAxisTickDir();
                this.SetChartObjScale(this.baseAxis.GetChartObjScale());
                this.baseAxis.SetAxisLabels(this);
            }
        }

        private void AdjustForLabelRotation(ChartText textobj, int axistype)
        {
            int textRotation = (int) textobj.GetTextRotation();
            textRotation = textRotation % 360;
            if (textRotation < 0)
            {
                textRotation += 360;
            }
            if (axistype == 0)
            {
                if ((textRotation > 0x2d) && (textRotation < 0x87))
                {
                    if (this.axisLabelsDir == 0)
                    {
                        textobj.SetXJust(2);
                    }
                    else
                    {
                        textobj.SetXJust(0);
                    }
                    textobj.SetYJust(1);
                }
                else if ((textRotation < 0x13b) && (textRotation > 0xe1))
                {
                    if (this.axisLabelsDir == 0)
                    {
                        textobj.SetXJust(0);
                    }
                    else
                    {
                        textobj.SetXJust(2);
                    }
                    textobj.SetYJust(1);
                }
            }
            else if ((textRotation > 0x2d) && (textRotation < 0x87))
            {
                textobj.SetXJust(1);
                if (this.axisLabelsDir == 0)
                {
                    textobj.SetYJust(0);
                }
                else
                {
                    textobj.SetYJust(2);
                }
            }
            else if ((textRotation < 0x13b) && (textRotation > 0xe1))
            {
                textobj.SetXJust(1);
                if (this.axisLabelsDir == 0)
                {
                    textobj.SetYJust(2);
                }
                else
                {
                    textobj.SetYJust(0);
                }
            }
        }

        public abstract void CalcAutoAxisLabels();
        private void CalcAxisLabelsJust(ChartText textobj)
        {
            int axisType = this.baseAxis.GetAxisType();
            Point2D location = textobj.GetLocation(0);
            double x = location.GetX();
            double y = location.GetY();
            if (axisType == 0)
            {
                textobj.SetXJust(1);
                if (this.axisLabelsDir == 2)
                {
                    textobj.SetYJust(0);
                    y = location.GetY() - this.axisLabelsTickOffsetY;
                }
                else
                {
                    textobj.SetYJust(2);
                    y = location.GetY() + this.axisLabelsTickOffsetY;
                }
            }
            else
            {
                textobj.SetYJust(1);
                if (this.axisLabelsDir == 2)
                {
                    x = location.GetX() + this.axisLabelsTickOffsetX;
                    textobj.SetXJust(0);
                }
                else
                {
                    x = location.GetX() - this.axisLabelsTickOffsetX;
                    textobj.SetXJust(2);
                }
            }
            if (textobj.GetTextRotation() != 0.0)
            {
                this.AdjustForLabelRotation(textobj, axisType);
            }
            textobj.SetLocation(x, y, 0);
        }

        public void Copy(AxisLabels source)
        {
            if (source != null)
            {
                base.Copy(source);
                base.textFont = source.textFont;
                this.baseAxis = source.baseAxis;
                this.axisLabelsDir = source.axisLabelsDir;
                this.axisLabelsEnds = source.axisLabelsEnds;
                this.axisLabelsTickOffsetX = source.axisLabelsTickOffsetX;
                this.axisLabelsTickOffsetY = source.axisLabelsTickOffsetY;
                this.axisLabelsFormat = source.axisLabelsFormat;
                this.overlapLabelMode = source.overlapLabelMode;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.baseAxis == null)
                {
                    nerror = 110;
                }
                if ((nerror == 0) && (base.textFont == null))
                {
                    nerror = 200;
                }
            }
            return base.ErrorCheck(nerror);
        }

        public int GetAxisLabelsDir()
        {
            return this.axisLabelsDir;
        }

        public int GetAxisLabelsEnds()
        {
            return this.axisLabelsEnds;
        }

        public int GetAxisLabelsFormat()
        {
            return this.axisLabelsFormat;
        }

        public abstract int GetAxisLabelsFormat(TickMark tickmark);
        public double GetAxisLabelsTickOffsetX()
        {
            return this.axisLabelsTickOffsetX;
        }

        public double GetAxisLabelsTickOffsetY()
        {
            return this.axisLabelsTickOffsetY;
        }

        public virtual Axis GetBaseAxis()
        {
            return this.baseAxis;
        }

        public abstract ChartLabel GetCompatibleLabel();
        public int GetOverlapLabelMode()
        {
            return this.overlapLabelMode;
        }

        private void InitDefaults()
        {
            base.chartObjType = 400;
            base.chartObjClipping = 1;
            base.SetTextFont(GraphObj.defaultChartFont);
            base.zOrder = 100;
        }

        public virtual void OutAxisLabel(Graphics g2, ChartLabel textobj, TickMark ticmark)
        {
            double tickLocation = 0.0;
            double threshold = 1E-05;
            int num3 = 0;
            Point2D tickStop = new Point2D();
            new Point2D();
            bool flag = false;
            threshold = this.baseAxis.GetAxisRange() / 1000.0;
            tickLocation = ticmark.GetTickLocation();
            tickStop = ticmark.GetTickStop();
            if (ChartSupport.NearTest(tickLocation, this.baseAxis.GetAxisMin(), threshold))
            {
                num3 = 1;
            }
            if (ChartSupport.NearTest(tickLocation, this.baseAxis.GetAxisIntercept(), threshold))
            {
                num3 |= 2;
            }
            if (ChartSupport.NearTest(tickLocation, this.baseAxis.GetAxisMax(), threshold))
            {
                num3 |= 4;
            }
            if (num3 > 0)
            {
                flag = false;
                if (((1 & this.axisLabelsEnds) != 0) && ((num3 & 1) != 0))
                {
                    flag = true;
                }
                if (((2 & this.axisLabelsEnds) != 0) && ((num3 & 2) != 0))
                {
                    flag = true;
                }
                if (((4 & this.axisLabelsEnds) != 0) && ((num3 & 4) != 0))
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                textobj.MakeLabel();
                textobj.SetLocation(tickStop.GetX(), tickStop.GetY(), 0);
                this.CalcAxisLabelsJust(textobj);
                textobj.PreCalcTextBoundingBox(g2);
                Rectangle2D textBox = textobj.GetTextBox();
                switch (this.overlapLabelMode)
                {
                    case 0:
                        textobj.Draw(g2);
                        return;

                    case 1:
                        if (!textBox.IntersectsWith(this.lastLabelBoundingBox))
                        {
                            textobj.Draw(g2);
                            this.lastLabelBoundingBox = textobj.GetTextBox();
                        }
                        return;

                    case 2:
                        if (!textBox.IntersectsWith(this.lastLabelBoundingBox))
                        {
                            textobj.Draw(g2);
                            this.lastLabelBoundingBox = textobj.GetTextBox();
                            return;
                        }
                        if (this.baseAxis.AxisType == 0)
                        {
                            Point2D location = textobj.GetLocation(0);
                            textobj.SetLocation(location.GetX(), location.GetY() + this.lastLabelBoundingBox.GetHeight(), 0);
                            textobj.Draw(g2);
                            this.lastLabelBoundingBox = textobj.GetTextBox();
                        }
                        return;

                    default:
                        return;
                }
            }
        }

        public void SetAxisLabels(Font font, Color labcolor)
        {
            base.SetTextFont(font);
            base.chartObjAttributes.SetPrimaryColor(labcolor);
        }

        public void SetAxisLabels(double rotation, int labdir, int labelends, Color labcolor)
        {
            base.textRotation = rotation;
            this.axisLabelsDir = labdir;
            base.chartObjAttributes.SetPrimaryColor(labcolor);
        }

        public void SetAxisLabels(Font font, double rotation, int labdir, int labelends, Color labcolor)
        {
            base.SetTextFont(font);
            base.textRotation = rotation;
            this.axisLabelsDir = labdir;
            base.chartObjAttributes.SetPrimaryColor(labcolor);
        }

        public abstract void SetAxisLabelsDecimalPos(int decimalpos);
        public void SetAxisLabelsDir(int labdir)
        {
            this.axisLabelsDir = labdir;
        }

        public void SetAxisLabelsEnds(int labelends)
        {
            this.axisLabelsEnds = labelends;
        }

        public void SetAxisLabelsFormat(int format)
        {
            this.axisLabelsFormat = format;
        }

        public void SetAxisLabelsTickOffsetX(double offset)
        {
            this.axisLabelsTickOffsetX = offset;
        }

        public void SetAxisLabelsTickOffsetY(double offset)
        {
            this.axisLabelsTickOffsetY = offset;
        }

        public virtual void SetBaseAxis(Axis baseaxis)
        {
            this.baseAxis = baseaxis;
            if (this.baseAxis != null)
            {
                this.SetChartObjScale(this.baseAxis.GetChartObjScale());
                this.baseAxis.SetAxisLabels(this);
            }
        }

        public void SetOverlapLabelMode(int overlapmode)
        {
            this.overlapLabelMode = overlapmode;
        }

        public int AxisLabelsDir
        {
            get
            {
                return this.axisLabelsDir;
            }
            set
            {
                this.axisLabelsDir = value;
            }
        }

        public int AxisLabelsEnds
        {
            get
            {
                return this.axisLabelsEnds;
            }
            set
            {
                this.axisLabelsEnds = value;
            }
        }

        public int AxisLabelsFormat
        {
            get
            {
                return this.axisLabelsFormat;
            }
            set
            {
                this.axisLabelsFormat = value;
            }
        }

        public double AxisLabelsTickOffsetX
        {
            get
            {
                return this.axisLabelsTickOffsetX;
            }
            set
            {
                this.axisLabelsTickOffsetX = value;
            }
        }

        public double AxisLabelsTickOffsetY
        {
            get
            {
                return this.axisLabelsTickOffsetY;
            }
            set
            {
                this.axisLabelsTickOffsetY = value;
            }
        }

        public Axis BaseAxis
        {
            get
            {
                return this.baseAxis;
            }
            set
            {
                this.SetBaseAxis(value);
            }
        }

        public Rectangle2D LastLabelBoundingBox
        {
            get
            {
                return this.lastLabelBoundingBox;
            }
            set
            {
                this.lastLabelBoundingBox = value;
            }
        }

        public int OverlapLabelMode
        {
            get
            {
                return this.overlapLabelMode;
            }
            set
            {
                this.overlapLabelMode = value;
            }
        }
    }
}

