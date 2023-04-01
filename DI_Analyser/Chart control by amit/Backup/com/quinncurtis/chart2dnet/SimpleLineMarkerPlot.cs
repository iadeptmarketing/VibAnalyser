namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class SimpleLineMarkerPlot : SimplePlot
    {
        internal ChartSymbol customChartSymbol;
        internal int lineMarkerSymbol;
        internal ChartAttribute symbolAttributes;
        internal int symbolSkip;
        internal int symbolStart;

        public SimpleLineMarkerPlot()
        {
            this.lineMarkerSymbol = 7;
            this.symbolStart = 0;
            this.symbolSkip = 1;
            this.symbolAttributes = new ChartAttribute();
            this.customChartSymbol = null;
            this.InitDefaults();
        }

        public SimpleLineMarkerPlot(PhysicalCoordinates transform)
        {
            this.lineMarkerSymbol = 7;
            this.symbolStart = 0;
            this.symbolSkip = 1;
            this.symbolAttributes = new ChartAttribute();
            this.customChartSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public SimpleLineMarkerPlot(PhysicalCoordinates transform, SimpleDataset dataset, int symtype, ChartAttribute lineattrib, ChartAttribute symbolattrib, int nsymbolstart, int nsymbolskip)
        {
            this.lineMarkerSymbol = 7;
            this.symbolStart = 0;
            this.symbolSkip = 1;
            this.symbolAttributes = new ChartAttribute();
            this.customChartSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.SetLineMarkerPlot(dataset, symtype, lineattrib, symbolattrib, nsymbolstart, nsymbolskip);
        }

        public override object Clone()
        {
            SimpleLineMarkerPlot plot = new SimpleLineMarkerPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(SimpleLineMarkerPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.lineMarkerSymbol = source.lineMarkerSymbol;
                this.symbolStart = source.symbolStart;
                this.symbolSkip = source.symbolSkip;
                if (source.symbolAttributes != null)
                {
                    this.symbolAttributes = (ChartAttribute) source.symbolAttributes.Clone();
                }
                if (source.customChartSymbol != null)
                {
                    this.customChartSymbol = (ChartSymbol) source.customChartSymbol.Clone();
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawLineMarkerPlot(g2);
            }
        }

        private void DrawLineMarkerPlot(Graphics g2)
        {
            ChartSymbol customChartSymbol = null;
            base.DisplayDataset = base.theDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray yData = base.DisplayDataset.YData;
            BoolArray validData = base.DisplayDataset.ValidData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            if (this.customChartSymbol == null)
            {
                customChartSymbol = new ChartSymbol(base.chartObjScale, this.lineMarkerSymbol, base.chartObjAttributes);
            }
            else
            {
                customChartSymbol = this.customChartSymbol;
            }
            customChartSymbol.SetResizeMultiplier(this.GetResizeMultiplier());
            if (numberDatapoints > 1)
            {
                SimplePlot source = this;
                SimpleLinePlot plot2 = new SimpleLinePlot(base.chartObjScale, base.theDataset, base.chartObjAttributes);
                plot2.Copy(source);
                plot2.Draw(g2);
                plot2 = null;
            }
            int num3 = Math.Min(base.theDataset.NumberDatapoints - 1, Math.Max(0, this.symbolStart));
            int num4 = Math.Min(base.theDataset.NumberDatapoints - 1, Math.Max(1, this.symbolSkip));
            num4 = Math.Max(1, this.symbolSkip);
            num3 = Math.Max(0, num3);
            base.chartObjScale.SetCurrentAttributes(this.symbolAttributes);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (((i >= num3) && (((i - num3) % num4) == 0)) && base.DisplayDataset.CheckValidData(base.chartObjScale, i))
                {
                    customChartSymbol.SetLocation(xData[i], yData[i], 1);
                    customChartSymbol.ChartObjAttributes = this.symbolAttributes;
                    customChartSymbol.Draw(g2);
                    if (base.showDatapointValue)
                    {
                        this.DrawSimpleDatapointValue(g2, xData[i], yData[i], yData[i]);
                        base.chartObjScale.SetCurrentAttributes(this.symbolAttributes);
                    }
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public int GetLineMarkerSymbol()
        {
            return this.lineMarkerSymbol;
        }

        public ChartAttribute GetSymbolAttributes()
        {
            return new ChartAttribute(this.symbolAttributes);
        }

        public int GetSymbolSkip()
        {
            return this.symbolSkip;
        }

        public int GetSymbolStart()
        {
            return this.symbolStart;
        }

        private void InitDefaults()
        {
            base.chartObjType = 2;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetCustomScatterPlotSymbol(ChartSymbol symbol)
        {
            this.customChartSymbol = symbol;
        }

        public void SetLineMarkerPlot(SimpleDataset dataset, int symtype, ChartAttribute lineattrib, ChartAttribute symbolattrib, int nsymbolstart, int nsymbolskip)
        {
            base.SetDataset(dataset);
            base.chartObjAttributes.Copy(lineattrib);
            this.symbolAttributes.Copy(symbolattrib);
            this.lineMarkerSymbol = symtype;
            if (dataset != null)
            {
                this.symbolStart = Math.Min(dataset.NumberDatapoints - 1, Math.Max(0, nsymbolstart));
                this.symbolStart = Math.Max(0, this.symbolStart);
                this.symbolSkip = Math.Min(dataset.NumberDatapoints - 1, Math.Max(1, nsymbolstart));
                this.symbolSkip = Math.Max(1, this.symbolSkip);
            }
            base.chartObjType = 2;
        }

        public void SetLineMarkerSymbol(int nsymbol)
        {
            this.lineMarkerSymbol = nsymbol;
        }

        public void SetSymbolAttributes(ChartAttribute attrib)
        {
            this.symbolAttributes.Copy(attrib);
        }

        public void SetSymbolSkip(int nskip)
        {
            this.symbolSkip = Math.Max(1, nskip);
        }

        public void SetSymbolStart(int nstart)
        {
            this.symbolStart = Math.Max(0, nstart);
        }

        public int LineMarkerSymbol
        {
            get
            {
                return this.lineMarkerSymbol;
            }
            set
            {
                this.lineMarkerSymbol = value;
            }
        }

        public ChartAttribute SymbolAttributes
        {
            get
            {
                return this.symbolAttributes;
            }
            set
            {
                this.symbolAttributes = value;
            }
        }

        public int SymbolSkip
        {
            get
            {
                return this.symbolSkip;
            }
            set
            {
                this.symbolSkip = Math.Max(1, value);
            }
        }

        public int SymbolStart
        {
            get
            {
                return this.symbolStart;
            }
            set
            {
                this.symbolStart = Math.Max(0, value);
            }
        }
    }
}

