namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class BubblePlotLegend : Legend
    {
        internal BubblePlot bubblePlot;
        internal ArrayList legendItemsArrayList;
        internal double symbolOffsetFactor;

        public BubblePlotLegend()
        {
            this.legendItemsArrayList = new ArrayList(20);
            this.symbolOffsetFactor = 0.5;
            this.bubblePlot = null;
            this.InitDefaults();
        }

        public BubblePlotLegend(BubblePlot plot, double rx, double ry, ChartAttribute attrib)
        {
            this.legendItemsArrayList = new ArrayList(20);
            this.symbolOffsetFactor = 0.5;
            this.bubblePlot = null;
            this.InitDefaults();
            this.bubblePlot = plot;
            base.chartObjAttributes.Copy(attrib);
            this.SetLocation(rx, ry);
        }

        public BubblePlotLegend(BubblePlot plot, double rx, double ry, double rwidth, double rheight, ChartAttribute attrib)
        {
            this.legendItemsArrayList = new ArrayList(20);
            this.symbolOffsetFactor = 0.5;
            this.bubblePlot = null;
            this.InitDefaults();
            this.bubblePlot = plot;
            base.chartObjAttributes.Copy(attrib);
            base.InitLegendPosition(rx, ry, rwidth, rheight);
        }

        public int AddLegendItem(BubblePlotLegendItem legenditem)
        {
            BubblePlotLegendItem item = (BubblePlotLegendItem) legenditem.Clone();
            if (item != null)
            {
                this.legendItemsArrayList.Add(item);
            }
            return this.legendItemsArrayList.Count;
        }

        public int AddLegendItem(string stext, double rsize, BubblePlot chartobj, Font thefont)
        {
            BubblePlotLegendItem item = new BubblePlotLegendItem(base.chartObjScale, stext, rsize, chartobj, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                item.GetLegendItemText().SetXJust(1);
                this.legendItemsArrayList.Add(item);
            }
            return this.legendItemsArrayList.Count;
        }

        public int AddLegendItem(string stext, double rsize, ChartAttribute attrib, Font thefont)
        {
            BubblePlotLegendItem item = new BubblePlotLegendItem(base.chartObjScale, stext, rsize, attrib, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                item.GetLegendItemText().SetXJust(1);
                this.legendItemsArrayList.Add(item);
            }
            return this.legendItemsArrayList.Count;
        }

        protected override void CalcLegendPosition()
        {
            Point2D location = new Point2D();
            Point2D source = new Point2D();
            location = this.GetLocation(0);
            double px = this.GetLocation(3).GetX() + base.legendWidth;
            double py = this.GetLocation(3).GetY() + base.legendHeight;
            source.SetLocation(px, py);
            Point2D pointd3 = base.chartObjScale.ConvertCoord(0, source, 3);
            base.legendRectangle.SetFrameFromDiagonal(location.GetX(), location.GetY(), pointd3.GetX(), pointd3.GetY());
            base.innerLegendRectangle.SetFrameFromDiagonal(base.legendRectangle.GetX1() + base.legendBorderRect[0], base.legendRectangle.GetY1() + base.legendBorderRect[1], base.legendRectangle.GetX2() - base.legendBorderRect[2], base.legendRectangle.GetY2() - base.legendBorderRect[3]);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            BubblePlotLegend legend = new BubblePlotLegend();
            legend.Copy(this);
            return legend;
        }

        public void Copy(BubblePlotLegend source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.legendItemsArrayList != null)
                {
                    for (int i = 0; i < source.legendItemsArrayList.Count; i++)
                    {
                        BubblePlotLegendItem legenditem = (BubblePlotLegendItem) source.legendItemsArrayList[i];
                        this.AddLegendItem(legenditem);
                    }
                }
                this.symbolOffsetFactor = source.symbolOffsetFactor;
                this.bubblePlot = source.bubblePlot;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                base.chartObjScale.ChartTransform(g2);
                this.CalcLegendPosition();
                base.chartObjScale.SetClippingArea(base.chartObjClipping);
                this.DrawLegendRect(g2);
                base.chartObjScale.SetClipRect(base.innerLegendRectangle);
                this.UpdateLegendItemUniformTextColor();
                base.DrawLegendHeaders(g2);
                this.DrawBubblePlotLegend(g2);
                base.DrawLegendFooter(g2);
                base.chartObjScale.SetClippingArea(base.chartObjClipping);
            }
        }

        private void DrawBubblePlotLegend(Graphics g2)
        {
            int count = this.legendItemsArrayList.Count;
            BubblePlotLegendItem item = null;
            double x = 0.0;
            double generalTextOffset = 0.0;
            double y = 0.0;
            double w = 1.0;
            Dimension source = new Dimension();
            new Point2D();
            x = base.innerLegendRectangle.GetX() + (base.innerLegendRectangle.Width / 2.0);
            generalTextOffset = base.generalTextOffset;
            this.GetMaximumBubbleRadius(g2);
            for (int i = 0; i < count; i++)
            {
                item = (BubblePlotLegendItem) this.legendItemsArrayList[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    this.bubblePlot.GetChartObjScale().SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    w = item.GetBubblePhysSize();
                    if (this.bubblePlot.GetBubbleSizeType() == 1)
                    {
                        w = Math.Sqrt(Math.Abs((double) (w / 3.1415926535897931)));
                    }
                    source.SetSize(w, w);
                    Dimension dimension2 = this.bubblePlot.GetChartObjScale().ConvertDimension(0, source, 1);
                    y = generalTextOffset + Math.Abs(dimension2.GetHeight());
                    item.GetLegendItemText().SetLocation(x, y + Math.Abs(dimension2.GetHeight()), 0);
                    item.SetBubbleDevRadius(Math.Abs(dimension2.GetHeight()));
                    item.SetLocation(x, y, 0);
                    item.SetChartObjEnable(this.GetChartObjEnable());
                    item.Draw(g2);
                }
            }
        }

        protected override void DrawLegendRect(Graphics g2)
        {
            double longestStringX = 0.0;
            double num2 = 0.0;
            double maximumBubbleRadius = this.GetMaximumBubbleRadius(g2);
            double sumStringY = this.GetSumStringY(g2);
            base.chartObjScale.SetCurrentAttributes(this.GetChartObjAttributes());
            if (base.autoSizeLegendRectangle)
            {
                longestStringX = this.GetLongestStringX(g2);
                num2 = base.chartObjScale.GetStringX(g2, "", 0);
                if ((((base.innerLegendRectangle.GetX() + maximumBubbleRadius) + num2) + longestStringX) > base.innerLegendRectangle.GetX2())
                {
                    double num5 = ((base.innerLegendRectangle.GetX() + this.GetMaximumBubbleRadius(g2)) + num2) + longestStringX;
                    base.innerLegendRectangle.SetX2(num5);
                    double num6 = num5 + base.legendBorderRect[2];
                    base.legendRectangle.SetX2(num6);
                }
                if (((base.innerLegendRectangle.GetY() + maximumBubbleRadius) + sumStringY) > base.innerLegendRectangle.GetY2())
                {
                    double num7 = (base.innerLegendRectangle.GetY() + maximumBubbleRadius) + sumStringY;
                    base.innerLegendRectangle.SetY2(num7);
                    double num8 = num7 + base.legendBorderRect[3];
                    base.legendRectangle.SetY2(num8);
                }
            }
            base.DrawLegendRect(g2);
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.legendItemsArrayList == null))
            {
                nerror = 310;
            }
            return base.ErrorCheck(nerror);
        }

        public BubblePlotLegendItem GetLegendItem(int itemnum)
        {
            BubblePlotLegendItem item = null;
            if (itemnum < this.legendItemsArrayList.Count)
            {
                item = (BubblePlotLegendItem) this.legendItemsArrayList[itemnum];
            }
            return item;
        }

        private double GetLongestStringX(Graphics g2)
        {
            int count = this.legendItemsArrayList.Count;
            double textSizeX = 0.0;
            double num3 = 0.0;
            BubblePlotLegendItem item = null;
            for (int i = 0; i < count; i++)
            {
                item = (BubblePlotLegendItem) this.legendItemsArrayList[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    base.chartObjScale.SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    textSizeX = item.GetLegendItemText().GetTextSizeX(g2, 0);
                    if (textSizeX > num3)
                    {
                        num3 = textSizeX;
                    }
                }
            }
            return num3;
        }

        private double GetMaximumBubbleRadius(Graphics g2)
        {
            int count = this.legendItemsArrayList.Count;
            BubblePlotLegendItem item = null;
            double num2 = 0.0;
            double w = 1.0;
            double num4 = 0.0;
            Dimension source = new Dimension();
            for (int i = 0; i < count; i++)
            {
                item = (BubblePlotLegendItem) this.legendItemsArrayList[i];
                if (item != null)
                {
                    w = item.GetBubblePhysSize();
                    if (this.bubblePlot.GetBubbleSizeType() == 1)
                    {
                        w = Math.Sqrt(Math.Abs((double) (w / 3.1415926535897931)));
                    }
                    source.SetSize(w, w);
                    num2 = Math.Abs(this.bubblePlot.GetChartObjScale().ConvertDimension(0, source, 1).GetHeight());
                    if (i == 0)
                    {
                        num4 = num2;
                    }
                    else if (num2 > num4)
                    {
                        num4 = num2;
                    }
                }
            }
            return num4;
        }

        public int GetNumLegendItems()
        {
            return this.legendItemsArrayList.Count;
        }

        private double GetSumStringY(Graphics g2)
        {
            int count = this.legendItemsArrayList.Count;
            double textSizeY = 0.0;
            double num3 = 0.0;
            BubblePlotLegendItem item = null;
            for (int i = 0; i < count; i++)
            {
                item = (BubblePlotLegendItem) this.legendItemsArrayList[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    base.chartObjScale.SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    textSizeY = item.GetLegendItemText().GetTextSizeY(g2, 0);
                    num3 += textSizeY;
                }
            }
            if (base.generalLegendText[0] != null)
            {
                base.generalLegendText[0].SetResizeMultiplier(base.resizeMultiplier);
                base.chartObjScale.SetCurrentFont(base.generalLegendText[0].GetResizedTextFont());
                num3 += base.generalLegendText[0].GetTextMaxSizeY(g2, 0);
            }
            if (base.generalLegendText[1] != null)
            {
                base.generalLegendText[1].SetResizeMultiplier(base.resizeMultiplier);
                base.chartObjScale.SetCurrentFont(base.generalLegendText[1].GetResizedTextFont());
                num3 += base.generalLegendText[1].GetTextMaxSizeY(g2, 0);
            }
            if (base.generalLegendText[2] != null)
            {
                base.generalLegendText[2].SetResizeMultiplier(base.resizeMultiplier);
                base.chartObjScale.SetCurrentFont(base.generalLegendText[2].GetResizedTextFont());
                num3 += base.generalLegendText[2].GetTextMaxSizeY(g2, 0);
            }
            return num3;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x321;
            base.positionType = 3;
            base.moveableType = 1;
            base.chartObjClipping = 1;
            this.SetChartObjScale(new CartesianCoordinates());
            this.GetChartObjScale().SetGraphBorderDiagonal(0.0, 0.0, 1.0, 1.0);
        }

        public override void SetChartObjScale(PhysicalCoordinates transform)
        {
            int num;
            base.SetChartObjScale(transform);
            for (num = 0; num < 2; num++)
            {
                if (base.generalLegendText[num] != null)
                {
                    base.generalLegendText[num].SetChartObjScale(transform);
                }
            }
            if (this.legendItemsArrayList != null)
            {
                for (num = 0; num < this.legendItemsArrayList.Count; num++)
                {
                    BubblePlotLegendItem item = (BubblePlotLegendItem) this.legendItemsArrayList[num];
                    if (item != null)
                    {
                        item.SetChartObjScale(transform);
                    }
                }
            }
        }

        public void SetLegendItem(BubblePlotLegendItem legenditem, int itemnum)
        {
            if (itemnum < this.legendItemsArrayList.Count)
            {
                this.legendItemsArrayList[itemnum] = legenditem;
            }
        }

        protected override void UpdateLegendItemUniformTextColor()
        {
            int count = this.legendItemsArrayList.Count;
            if (base.legendItemUniformTextColor != Color.Empty)
            {
                for (int i = 0; i < count; i++)
                {
                    BubblePlotLegendItem item = (BubblePlotLegendItem) this.legendItemsArrayList[i];
                    if (item != null)
                    {
                        item.GetLegendItemText().SetColor(base.legendItemUniformTextColor);
                    }
                }
            }
        }
    }
}

