namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LegendItem : GraphObj
    {
        internal ChartSymbol legendItemSymbol;
        internal ChartText legendItemText;

        public LegendItem()
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            this.InitDefaults();
        }

        public LegendItem(ChartText textitem, GraphObj chartobj)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            int chartObjSymbol = this.GetChartObjSymbol(chartobj);
            if ((textitem != null) && (chartobj != null))
            {
                ChartAttribute attrib = new ChartAttribute(chartobj);
                this.InitDefaults();
                this.SetChartObjScale(textitem.GetChartObjScale());
                this.legendItemSymbol = new ChartSymbol(base.chartObjScale, chartObjSymbol, attrib);
                this.legendItemText = (ChartText) textitem.Clone();
                if (this.legendItemText != null)
                {
                    this.legendItemText.SetPositionType(0);
                    this.legendItemText.SetColor(attrib.GetPrimaryColor());
                }
                this.SetClipping(1);
            }
        }

        public LegendItem(ChartText textitem, int nsymbol)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            if (textitem != null)
            {
                this.InitDefaults();
                this.SetChartObjScale(textitem.GetChartObjScale());
                this.legendItemSymbol = new ChartSymbol(base.chartObjScale, nsymbol, textitem.GetChartObjAttributes());
                this.legendItemText = (ChartText) textitem.Clone();
                if (this.legendItemText != null)
                {
                    this.legendItemText.SetPositionType(0);
                }
                this.SetChartObjClipping(3);
            }
        }

        public LegendItem(ChartText textitem, int nsymbol, GraphObj chartobj)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            if ((textitem != null) && (chartobj != null))
            {
                ChartAttribute attrib = new ChartAttribute(chartobj);
                this.InitDefaults();
                this.SetChartObjScale(textitem.GetChartObjScale());
                this.legendItemSymbol = new ChartSymbol(base.chartObjScale, nsymbol, attrib);
                this.legendItemText = (ChartText) textitem.Clone();
                if (this.legendItemText != null)
                {
                    this.legendItemText.SetPositionType(0);
                    this.legendItemText.SetColor(attrib.GetPrimaryColor());
                }
                this.SetClipping(1);
            }
        }

        public LegendItem(PhysicalCoordinates transform, string stext, GraphObj chartobj, Font thefont)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            int chartObjSymbol = this.GetChartObjSymbol(chartobj);
            ChartAttribute attrib = new ChartAttribute(chartobj);
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.legendItemSymbol = new ChartSymbol(transform, chartObjSymbol, attrib);
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attrib.GetPrimaryColor());
            }
            this.SetClipping(1);
        }

        public LegendItem(PhysicalCoordinates transform, string stext, ChartPlot chartobj, int ngroup, Font thefont)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            int chartObjSymbol = this.GetChartObjSymbol(chartobj);
            ChartAttribute attrib = new ChartAttribute(chartobj, ngroup);
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.legendItemSymbol = new ChartSymbol(transform, chartObjSymbol, attrib);
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attrib.GetPrimaryColor());
            }
            this.SetClipping(1);
        }

        public LegendItem(PhysicalCoordinates transform, string stext, int nsymbol, ChartAttribute attrib, Font thefont)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.legendItemSymbol = new ChartSymbol(transform, nsymbol, attrib);
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attrib.GetPrimaryColor());
            }
            this.SetClipping(1);
        }

        public LegendItem(PhysicalCoordinates transform, string stext, int nsymbol, GraphObj chartobj, Font thefont)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            ChartAttribute attrib = new ChartAttribute(chartobj);
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.legendItemSymbol = new ChartSymbol(transform, nsymbol, attrib);
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attrib.GetPrimaryColor());
            }
            this.SetClipping(1);
        }

        public LegendItem(PhysicalCoordinates transform, string stext, int nsymbol, ChartPlot chartobj, int ngroup, Font thefont)
        {
            this.legendItemText = null;
            this.legendItemSymbol = null;
            ChartAttribute attrib = new ChartAttribute(chartobj, ngroup);
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.legendItemSymbol = new ChartSymbol(transform, nsymbol, attrib);
            this.legendItemText = new ChartText(transform, thefont, stext, 0.0, 0.0, 0);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetColor(attrib.GetPrimaryColor());
            }
            this.SetClipping(1);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            LegendItem item = new LegendItem();
            item.Copy(this);
            return item;
        }

        public void Copy(LegendItem source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.legendItemText = (ChartText) source.legendItemText.Clone();
                this.legendItemSymbol = (ChartSymbol) source.legendItemSymbol.Clone();
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                base.chartObjScale.ChartTransform(g2);
                base.chartObjScale.SetClippingArea(base.chartObjClipping);
                this.legendItemSymbol.SetChartObjEnable(this.GetChartObjEnable());
                this.legendItemSymbol.SetResizeMultiplier(base.resizeMultiplier);
                this.legendItemSymbol.Draw(g2);
                this.legendItemText.SetChartObjEnable(this.GetChartObjEnable());
                this.legendItemText.SetResizeMultiplier(base.resizeMultiplier);
                this.legendItemText.Draw(g2);
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
                if (nerror == 0)
                {
                    if (this.legendItemSymbol == null)
                    {
                        nerror = 330;
                    }
                    else
                    {
                        nerror = this.legendItemSymbol.ErrorCheck(nerror);
                    }
                }
            }
            return base.ErrorCheck(nerror);
        }

        private int GetChartObjSymbol(ChartObj chartobj)
        {
            int chartObjType = chartobj.GetChartObjType();
            int num2 = 9;
            switch (chartObjType)
            {
                case 60:
                    return 8;

                case 0x3d:
                    return ((PolarScatterPlot) chartobj).ScatterPlotSymbol;

                case 70:
                    return 8;

                case 1:
                    return 8;

                case 2:
                    return ((SimpleLineMarkerPlot) chartobj).LineMarkerSymbol;

                case 3:
                    if (((ChartPlot) chartobj).BarOrient != 0)
                    {
                        return 10;
                    }
                    return 9;

                case 5:
                    return ((SimpleScatterPlot) chartobj).ScatterPlotSymbol;

                case 6:
                    return num2;

                case 0x15:
                    if (((ChartPlot) chartobj).BarOrient != 0)
                    {
                        return 10;
                    }
                    return 9;

                case 0x16:
                    if (((ChartPlot) chartobj).BarOrient != 0)
                    {
                        return 10;
                    }
                    return 9;

                case 0x17:
                    return 8;

                case 0x18:
                    return 8;

                case 0x19:
                    return 8;

                case 0x1a:
                    if (((ChartPlot) chartobj).BarOrient != 0)
                    {
                        return 10;
                    }
                    return 9;

                case 0x1b:
                    return 8;

                case 0x1c:
                    return 8;

                case 0x1d:
                    return 11;

                case 30:
                    return 8;

                case 0x1f:
                    return 9;

                case 0x20:
                    return 9;

                case 0x21:
                    return 10;

                case 50:
                    return (num2 = 8);
            }
            return 9;
        }

        public GraphicsPath GetLegendItemCustomShape()
        {
            return this.legendItemSymbol.GetSymbolShape();
        }

        public ChartSymbol GetLegendItemSymbol()
        {
            return this.legendItemSymbol;
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
            base.chartObjType = 810;
            base.chartObjClipping = 1;
        }

        public override void SetChartObjScale(PhysicalCoordinates transform)
        {
            base.SetChartObjScale(transform);
            if (this.legendItemText != null)
            {
                this.legendItemText.SetChartObjScale(transform);
            }
            if (this.legendItemSymbol != null)
            {
                this.legendItemSymbol.SetChartObjScale(transform);
            }
        }

        private void SetClipping(int clipping)
        {
            this.SetChartObjClipping(clipping);
            this.legendItemSymbol.SetChartObjClipping(clipping);
            this.legendItemText.SetChartObjClipping(clipping);
        }

        public void SetLegendItemCustomShape(GraphicsPath symbolshape)
        {
            this.legendItemSymbol.SetSymbolShape(symbolshape);
        }

        public void SetLegendItemSymbol(ChartSymbol symbol)
        {
            this.legendItemSymbol = symbol;
        }

        public void SetLegendItemText(ChartText text)
        {
            this.legendItemText = text;
        }

        public override void SetResizeMultiplier(double multiplier)
        {
            base.resizeMultiplier = multiplier;
            this.legendItemSymbol.SetResizeMultiplier(base.resizeMultiplier);
            this.legendItemText.SetResizeMultiplier(base.resizeMultiplier);
        }

        public ChartSymbol LegendItemSymbol
        {
            get
            {
                return this.legendItemSymbol;
            }
            set
            {
                this.legendItemSymbol = value;
            }
        }

        public ChartText LegendItemText
        {
            get
            {
                return this.legendItemText;
            }
            set
            {
                this.legendItemText = value;
            }
        }
    }
}

