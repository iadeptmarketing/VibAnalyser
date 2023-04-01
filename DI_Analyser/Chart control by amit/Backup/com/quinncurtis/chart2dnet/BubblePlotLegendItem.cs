namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class BubblePlotLegendItem : GraphObj
    {
        internal double bubbleDevRadius;
        internal double bubblePhysSize;
        internal ChartText legendItemText;

        public BubblePlotLegendItem()
        {
            this.legendItemText = null;
            this.bubblePhysSize = 1.0;
            this.bubbleDevRadius = 1.0;
            this.InitDefaults();
        }

        public BubblePlotLegendItem(ChartText textitem, double rsize, GraphObj chartobj)
        {
            this.legendItemText = null;
            this.bubblePhysSize = 1.0;
            this.bubbleDevRadius = 1.0;
            if ((textitem != null) && (chartobj != null))
            {
                ChartAttribute attribute = new ChartAttribute(chartobj);
                this.InitDefaults();
                base.chartObjScale = textitem.GetChartObjScale();
                this.bubblePhysSize = rsize;
                this.legendItemText = (ChartText) textitem.Clone();
                if (this.legendItemText != null)
                {
                    this.legendItemText.SetPositionType(0);
                    this.legendItemText.SetColor(attribute.GetPrimaryColor());
                }
                this.SetClipping(1);
            }
        }

        public BubblePlotLegendItem(PhysicalCoordinates transform, string stext, double rsize, BubblePlot chartobj, Font thefont)
        {
            this.legendItemText = null;
            this.bubblePhysSize = 1.0;
            this.bubbleDevRadius = 1.0;
            ChartAttribute attr = new ChartAttribute(chartobj);
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.bubblePhysSize = rsize;
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attr.GetPrimaryColor());
            }
            this.SetChartObjAttributes(attr);
            this.SetClipping(1);
        }

        public BubblePlotLegendItem(PhysicalCoordinates transform, string stext, double rsize, ChartAttribute attrib, Font thefont)
        {
            this.legendItemText = null;
            this.bubblePhysSize = 1.0;
            this.bubbleDevRadius = 1.0;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.bubblePhysSize = rsize;
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attrib.GetPrimaryColor());
            }
            this.SetChartObjAttributes(attrib);
            this.SetClipping(1);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            BubblePlotLegendItem item = new BubblePlotLegendItem();
            item.Copy(this);
            return item;
        }

        public void Copy(BubblePlotLegendItem source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.legendItemText = (ChartText) source.legendItemText.Clone();
                this.bubblePhysSize = source.bubblePhysSize;
                this.bubbleDevRadius = source.bubbleDevRadius;
            }
        }

        public override void Draw(Graphics g2)
        {
            Point2D location = new Point2D();
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.legendItemText.SetChartObjEnable(this.GetChartObjEnable());
                this.legendItemText.SetResizeMultiplier(base.resizeMultiplier);
                this.legendItemText.Draw(g2);
                location = this.GetLocation();
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                Arc2D arcd = new Arc2D();
                arcd.SetArcByCenter(location.GetX(), location.GetY(), this.bubbleDevRadius, 0.0, 359.0, 1);
                base.thePath.AddEllipse(arcd.GetRectangleF());
                base.chartObjScale.DrawFillPath(g2, base.thePath);
                base.thePath.Reset();
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.legendItemText == null)
                {
                    nerror = 320;
                }
                else
                {
                    nerror = this.legendItemText.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public double GetBubbleDevRadius()
        {
            return this.bubbleDevRadius;
        }

        public double GetBubblePhysSize()
        {
            return this.bubblePhysSize;
        }

        public ChartText GetLegendItemText()
        {
            return this.legendItemText;
        }

        public override double GetResizeMultiplier()
        {
            return base.resizeMultiplier;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x32b;
            base.chartObjClipping = 1;
        }

        public void SetBubbleDevRadius(double rsize)
        {
            this.bubbleDevRadius = rsize;
        }

        public void SetBubblePhysSize(double rsize)
        {
            this.bubblePhysSize = rsize;
        }

        public override void SetChartObjScale(PhysicalCoordinates transform)
        {
            base.SetChartObjScale(transform);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetChartObjScale(transform);
            }
        }

        public void SetClipping(int clipping)
        {
            this.SetChartObjClipping(clipping);
            this.legendItemText.SetChartObjClipping(clipping);
        }

        public void SetLegendItemText(ChartText text)
        {
            this.legendItemText = text;
        }

        public override void SetResizeMultiplier(double multiplier)
        {
            base.resizeMultiplier = multiplier;
            this.legendItemText.SetResizeMultiplier(base.resizeMultiplier);
        }

        public double BubbleDevRadius
        {
            get
            {
                return this.bubbleDevRadius;
            }
            set
            {
                this.bubbleDevRadius = value;
            }
        }

        public double BubblePhysSize
        {
            get
            {
                return this.bubblePhysSize;
            }
            set
            {
                this.bubblePhysSize = value;
            }
        }
    }
}

