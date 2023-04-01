namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class AxisTitle : ChartText
    {
        internal Axis titleAxis;

        public AxisTitle()
        {
            this.titleAxis = null;
            this.InitDefaults();
        }

        public AxisTitle(Axis axis, Font thefont, string s)
        {
            this.titleAxis = null;
            this.SetAxisTitle(axis, thefont, s);
        }

        public override object Clone()
        {
            AxisTitle title = new AxisTitle();
            title.Copy(this);
            return title;
        }

        public void Copy(AxisTitle source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.titleAxis = source.titleAxis;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.JustifyTitlePos(g2);
                base.Draw(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.titleAxis == null)
                {
                    nerror = 120;
                }
                else
                {
                    nerror = this.titleAxis.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public Axis GetTitleAxis()
        {
            return this.titleAxis;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x2bd;
            base.chartObjClipping = 1;
            base.moveableType = 0;
            base.SetXJust(1);
            base.SetYJust(2);
            this.SetPositionType(0);
            base.zOrder = 110;
        }

        private void JustifyTitlePos(Graphics g2)
        {
            int num = 0;
            double textMaxSizeY = 0.0;
            double x = 0.0;
            double y = 0.0;
            int axisLabelsDir = 0;
            if (this.titleAxis != null)
            {
                Rectangle2D boundingBox;
                Rectangle2D rectangled2;
                AxisLabels axisLabels = this.titleAxis.GetAxisLabels();
                if (axisLabels != null)
                {
                    boundingBox = axisLabels.GetBoundingBox();
                    rectangled2 = this.titleAxis.GetBoundingBox();
                    axisLabelsDir = axisLabels.GetAxisLabelsDir();
                }
                else
                {
                    boundingBox = this.titleAxis.GetBoundingBox();
                    rectangled2 = this.titleAxis.GetBoundingBox();
                    axisLabelsDir = this.titleAxis.GetAxisTickDir();
                }
                textMaxSizeY = base.GetTextMaxSizeY(g2, 0);
                base.chartObjScale.GetStringDescent(g2, base.textString);
                base.chartObjScale.GetStringAscent(g2, base.textString);
                if (this.titleAxis.GetAxisType() == 0)
                {
                    if (axisLabelsDir == 0)
                    {
                        x = rectangled2.GetX() + (rectangled2.GetWidth() / 2.0);
                        y = boundingBox.GetY() + boundingBox.GetHeight();
                    }
                    else
                    {
                        x = rectangled2.GetX() + (rectangled2.GetWidth() / 2.0);
                        y = boundingBox.GetY();
                    }
                }
                else if (axisLabelsDir == 0)
                {
                    x = boundingBox.GetX() - textMaxSizeY;
                    y = rectangled2.GetY() + (rectangled2.GetHeight() / 2.0);
                    num = 90;
                }
                else
                {
                    x = boundingBox.GetX() + boundingBox.GetWidth();
                    y = rectangled2.GetY() + (rectangled2.GetHeight() / 2.0);
                    num = -90;
                }
                this.SetLocation(x, y);
                base.SetTextRotation((double) num);
            }
        }

        public void SetAxisTitle(Axis axis, Font thefont, string s)
        {
            this.InitDefaults();
            this.titleAxis = axis;
            base.textString = s;
            base.SetTextFont(thefont);
            this.SetChartObjScale(this.titleAxis.GetChartObjScale());
        }

        public void SetTitleAxis(Axis axis)
        {
            this.titleAxis = axis;
            if (this.titleAxis != null)
            {
                this.SetChartObjScale(this.titleAxis.GetChartObjScale());
            }
        }

        public Axis TitleAxis
        {
            get
            {
                return this.GetTitleAxis();
            }
            set
            {
                this.SetTitleAxis(value);
            }
        }
    }
}

