namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Reflection;

    public class StandardLegend : Legend
    {
        internal int layoutMode;
        internal ArrayList legendItems;
        internal double symbolOffsetFactor;

        public StandardLegend()
        {
            this.legendItems = new ArrayList(20);
            this.layoutMode = 0;
            this.symbolOffsetFactor = 0.5;
            this.InitDefaults();
        }

        public StandardLegend(double rx, double ry, ChartAttribute attrib, int nlayout1mode)
        {
            this.legendItems = new ArrayList(20);
            this.layoutMode = 0;
            this.symbolOffsetFactor = 0.5;
            this.InitDefaults();
            base.chartObjAttributes.Copy(attrib);
            this.SetLocation(rx, ry);
            this.layoutMode = nlayout1mode;
        }

        public StandardLegend(double rx, double ry, double rwidth, double rheight, ChartAttribute attrib, int nlayout1mode)
        {
            this.legendItems = new ArrayList(20);
            this.layoutMode = 0;
            this.symbolOffsetFactor = 0.5;
            this.InitDefaults();
            base.chartObjAttributes.Copy(attrib);
            base.InitLegendPosition(rx, ry, rwidth, rheight);
            this.layoutMode = nlayout1mode;
        }

        public int AddLegendItem(LegendItem legenditem)
        {
            LegendItem item = (LegendItem) legenditem.Clone();
            if (item != null)
            {
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, GraphObj chartobj, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, chartobj, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, ChartPlot chartobj, int ngroup, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, chartobj, ngroup, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, GraphicsPath symbolshape, ChartAttribute attrib, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, 12, attrib, thefont);
            if (item != null)
            {
                item.SetLegendItemCustomShape(symbolshape);
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, GraphicsPath symbolshape, GraphObj chartobj, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, 12, chartobj, thefont);
            if (item != null)
            {
                item.SetLegendItemCustomShape(symbolshape);
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, int nsymbol, ChartAttribute attrib, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, nsymbol, attrib, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, int nsymbol, GraphObj chartobj, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, nsymbol, chartobj, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public int AddLegendItem(string stext, int nsymbol, ChartPlot chartobj, int ngroup, Font thefont)
        {
            LegendItem item = new LegendItem(base.chartObjScale, stext, nsymbol, chartobj, ngroup, thefont);
            if (item != null)
            {
                item.GetLegendItemText().SetYJust(2);
                this.legendItems.Add(item);
            }
            return this.legendItems.Count;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            StandardLegend legend = new StandardLegend();
            legend.Copy(this);
            return legend;
        }

        public void Copy(StandardLegend source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.legendItems != null)
                {
                    for (int i = 0; i < source.legendItems.Count; i++)
                    {
                        LegendItem legenditem = (LegendItem) source.legendItems[i];
                        this.AddLegendItem(legenditem);
                    }
                }
                this.layoutMode = source.layoutMode;
                this.symbolOffsetFactor = source.symbolOffsetFactor;
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
                if (this.layoutMode == 1)
                {
                    this.DrawVerticalLegend(g2);
                }
                else
                {
                    this.DrawHorizontalLegend(g2);
                }
                base.DrawLegendFooter(g2);
                base.chartObjScale.SetClippingArea(base.chartObjClipping);
            }
        }

        private void DrawHorizontalLegend(Graphics g2)
        {
            int count = this.legendItems.Count;
            LegendItem item = null;
            double textMaxSizeY = 0.0;
            double x = 0.0;
            double y = 0.0;
            double longestStringX = 0.0;
            double num7 = 0.01;
            x = base.innerLegendRectangle.GetX();
            longestStringX = this.GetLongestStringX(g2);
            y = base.generalTextOffset;
            for (int i = 0; i < count; i++)
            {
                item = (LegendItem) this.legendItems[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    base.chartObjScale.SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    textMaxSizeY = item.GetLegendItemText().GetTextMaxSizeY(g2, 0);
                    item.GetLegendItemText().GetTextSizeX(g2, 0);
                    num7 = base.chartObjScale.GetStringX(g2, "", 0);
                    double num5 = y + (textMaxSizeY * this.symbolOffsetFactor);
                    item.GetLegendItemText().SetLocation(x + (2.0 * num7), y, 0);
                    item.GetLegendItemSymbol().SetLocation(x + num7, num5, 0);
                    x += longestStringX + (num7 * (2.0 + base.horizontalSpacing));
                    if (((x + longestStringX) + (2.0 * num7)) > (base.innerLegendRectangle.GetX() + base.innerLegendRectangle.GetWidth()))
                    {
                        x = base.innerLegendRectangle.GetX();
                        y += textMaxSizeY * base.verticalSpacing;
                    }
                    item.SetChartObjEnable(this.GetChartObjEnable());
                    item.Draw(g2);
                }
            }
        }

        protected override void DrawLegendRect(Graphics g2)
        {
            double longestStringX = this.GetLongestStringX(g2);
            double num2 = base.chartObjScale.GetStringX(g2, "", 0);
            double sumStringY = this.GetSumStringY(g2);
            double itemSumStringY = this.GetItemSumStringY(g2);
            double headerSumStringY = this.GetHeaderSumStringY(g2);
            int count = this.legendItems.Count;
            base.chartObjScale.SetCurrentAttributes(this.GetChartObjAttributes());
            if (base.autoSizeLegendRectangle)
            {
                if (this.layoutMode == 1)
                {
                    double num9 = (base.innerLegendRectangle.GetX() + (3.0 * num2)) + longestStringX;
                    base.innerLegendRectangle.SetX2(num9);
                    double num10 = num9 + base.legendBorderRect[2];
                    base.legendRectangle.SetX2(num10);
                    double num11 = base.innerLegendRectangle.GetY() + sumStringY;
                    base.innerLegendRectangle.SetY2(num11);
                    double num12 = num11 + base.legendBorderRect[3];
                    base.legendRectangle.SetY2(num12);
                }
                else
                {
                    double num13;
                    double num8 = Math.Floor((double) ((base.innerLegendRectangle.Width - (2.0 * num2)) / ((num2 * (2.0 + base.horizontalSpacing)) + longestStringX)));
                    if (num8 == 1.0)
                    {
                        num13 = (base.innerLegendRectangle.GetX() + (3.0 * num2)) + longestStringX;
                    }
                    else
                    {
                        num13 = (base.innerLegendRectangle.GetX() + (2.0 * num2)) + (num8 * ((num2 * (2.0 + base.horizontalSpacing)) + longestStringX));
                    }
                    base.innerLegendRectangle.SetX2(num13);
                    double num14 = num13 + base.legendBorderRect[2];
                    base.legendRectangle.SetX2(num14);
                    double num7 = Math.Ceiling((double) (((double) count) / num8));
                    num8 = Math.Ceiling((double) (((double) count) / num7));
                    double num15 = (base.innerLegendRectangle.GetY() + (headerSumStringY / ((double) Math.Max(1, base.NumberGeneralLegendItems)))) + (itemSumStringY / num8);
                    base.innerLegendRectangle.SetY2(num15);
                    double num16 = num15 + base.legendBorderRect[3];
                    base.legendRectangle.SetY2(num16);
                }
            }
            base.DrawLegendRect(g2);
        }

        private void DrawVerticalLegend(Graphics g2)
        {
            int count = this.legendItems.Count;
            LegendItem item = null;
            double textMaxSizeY = 0.0;
            double num3 = 0.0;
            double x = 0.0;
            double y = 0.0;
            double num8 = 0.01;
            x = base.innerLegendRectangle.GetX();
            y = base.generalTextOffset;
            double longestStringX = this.GetLongestStringX(g2);
            for (int i = 0; i < count; i++)
            {
                item = (LegendItem) this.legendItems[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    base.chartObjScale.SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    textMaxSizeY = item.GetLegendItemText().GetTextMaxSizeY(g2, 0);
                    item.GetLegendItemText().GetTextSizeX(g2, 0);
                    num8 = base.chartObjScale.GetStringX(g2, "", 0);
                    double num6 = y + (textMaxSizeY * this.symbolOffsetFactor);
                    item.GetLegendItemText().SetLocation(x + (2.0 * num8), y, 0);
                    item.GetLegendItemSymbol().SetLocation(x + num8, num6, 0);
                    y += textMaxSizeY * base.verticalSpacing;
                    num3 = base.innerLegendRectangle.GetY() + base.innerLegendRectangle.GetHeight();
                    if (base.generalLegendText[2] != null)
                    {
                        num3 -= 2.0 * base.generalLegendText[2].GetTextMaxSizeY(g2, 0);
                    }
                    if (y >= num3)
                    {
                        x += longestStringX + (num8 * (2.0 + base.horizontalSpacing));
                        y = base.generalTextOffset + (textMaxSizeY * base.verticalSpacing);
                    }
                    item.SetChartObjEnable(this.GetChartObjEnable());
                    item.Draw(g2);
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.legendItems == null))
            {
                nerror = 310;
            }
            return base.ErrorCheck(nerror);
        }

        protected double GetHeaderSumStringY(Graphics g2)
        {
            int count = this.legendItems.Count;
            double num = 0.0;
            if (base.generalLegendText[0] != null)
            {
                base.generalLegendText[0].SetResizeMultiplier(base.resizeMultiplier);
                base.chartObjScale.SetCurrentFont(base.generalLegendText[0].GetResizedTextFont());
                num += base.generalLegendText[0].GetTextMaxSizeY(g2, 0);
            }
            if (base.generalLegendText[1] != null)
            {
                base.generalLegendText[1].SetResizeMultiplier(base.resizeMultiplier);
                base.chartObjScale.SetCurrentFont(base.generalLegendText[1].GetResizedTextFont());
                num += base.generalLegendText[1].GetTextMaxSizeY(g2, 0);
            }
            if (base.generalLegendText[2] != null)
            {
                base.generalLegendText[2].SetResizeMultiplier(base.resizeMultiplier);
                base.chartObjScale.SetCurrentFont(base.generalLegendText[2].GetResizedTextFont());
                num += base.generalLegendText[2].GetTextMaxSizeY(g2, 0);
            }
            return num;
        }

        protected double GetItemSumStringY(Graphics g2)
        {
            int count = this.legendItems.Count;
            double textSizeY = 0.0;
            double num3 = 0.0;
            LegendItem item = null;
            for (int i = 0; i < count; i++)
            {
                item = (LegendItem) this.legendItems[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    base.chartObjScale.SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    textSizeY = item.GetLegendItemText().GetTextSizeY(g2, 0);
                    num3 += textSizeY;
                }
            }
            return num3;
        }

        public double GetLayoutMode()
        {
            return (double) this.layoutMode;
        }

        public LegendItem GetLegendItem(int itemnum)
        {
            LegendItem item = null;
            if (itemnum < this.legendItems.Count)
            {
                item = (LegendItem) this.legendItems[itemnum];
            }
            return item;
        }

        protected double GetLongestStringX(Graphics g2)
        {
            int count = this.legendItems.Count;
            double textSizeX = 0.0;
            double num3 = 0.0;
            LegendItem item = null;
            for (int i = 0; i < count; i++)
            {
                item = (LegendItem) this.legendItems[i];
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

        protected double GetLongestStringY(Graphics g2)
        {
            int count = this.legendItems.Count;
            double textSizeY = 0.0;
            double num3 = 0.0;
            LegendItem item = null;
            for (int i = 0; i < count; i++)
            {
                item = (LegendItem) this.legendItems[i];
                if (item != null)
                {
                    item.SetResizeMultiplier(base.resizeMultiplier);
                    base.chartObjScale.SetCurrentFont(item.GetLegendItemText().GetResizedTextFont());
                    textSizeY = item.GetLegendItemText().GetTextSizeY(g2, 0);
                    if (textSizeY > num3)
                    {
                        num3 = textSizeY;
                    }
                }
            }
            return num3;
        }

        public int GetNumLegendItems()
        {
            return this.legendItems.Count;
        }

        protected double GetSumStringY(Graphics g2)
        {
            int count = this.legendItems.Count;
            double textSizeY = 0.0;
            double num3 = 0.0;
            LegendItem item = null;
            for (int i = 0; i < count; i++)
            {
                item = (LegendItem) this.legendItems[i];
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
            base.chartObjType = 800;
            base.positionType = 3;
            base.moveableType = 1;
            base.chartObjClipping = 1;
            this.SetChartObjScale(new CartesianCoordinates());
            base.chartObjScale.SetGraphBorderDiagonal(0.0, 0.0, 1.0, 1.0);
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
            if (this.legendItems != null)
            {
                for (num = 0; num < this.legendItems.Count; num++)
                {
                    LegendItem item = (LegendItem) this.legendItems[num];
                    if (item != null)
                    {
                        item.SetChartObjScale(transform);
                    }
                }
            }
        }

        public void SetLayoutMode(int layout1mode)
        {
            this.layoutMode = layout1mode;
        }

        public void SetLegendItem(LegendItem legenditem, int itemnum)
        {
            if (itemnum < this.legendItems.Count)
            {
                this.legendItems[itemnum] = legenditem;
            }
        }

        public void SetLegendItemUniformTextColor(Color color)
        {
            base.legendItemUniformTextColor = color;
            this.UpdateLegendItemUniformTextColor();
        }

        protected override void UpdateLegendItemUniformTextColor()
        {
            int count = this.legendItems.Count;
            if (base.legendItemUniformTextColor != Color.Empty)
            {
                for (int i = 0; i < count; i++)
                {
                    LegendItem item = (LegendItem) this.legendItems[i];
                    if (item != null)
                    {
                        item.GetLegendItemText().SetColor(base.legendItemUniformTextColor);
                    }
                }
            }
        }

        public LegendItem this[int index]
        {
            get
            {
                index = Math.Min(this.legendItems.Count - 1, index);
                return (LegendItem) this.legendItems[index];
            }
        }

        public int LayoutMode
        {
            get
            {
                return this.layoutMode;
            }
            set
            {
                this.layoutMode = value;
            }
        }

        public int NumberLegendItems
        {
            get
            {
                return this.legendItems.Count;
            }
        }
    }
}

