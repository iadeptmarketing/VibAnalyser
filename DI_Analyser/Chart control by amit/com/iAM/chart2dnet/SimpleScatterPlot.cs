namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class SimpleScatterPlot : SimplePlot
    {
        internal ChartSymbol customScatterPlotSymbol;
        internal int scatterPlotSymbol;

        public SimpleScatterPlot()
        {
            this.scatterPlotSymbol = 7;
            this.customScatterPlotSymbol = null;
            this.InitDefaults();
        }

        public SimpleScatterPlot(PhysicalCoordinates transform)
        {
            this.scatterPlotSymbol = 7;
            this.customScatterPlotSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public SimpleScatterPlot(PhysicalCoordinates transform, SimpleDataset dataset, int symtype, ChartAttribute attrib)
        {
            this.scatterPlotSymbol = 7;
            this.customScatterPlotSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.SetScatterPlot(dataset, symtype, attrib);
        }

        public override object Clone()
        {
            SimpleScatterPlot plot = new SimpleScatterPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(SimpleScatterPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.scatterPlotSymbol = source.scatterPlotSymbol;
                if (source.customScatterPlotSymbol != null)
                {
                    this.customScatterPlotSymbol = (ChartSymbol) source.customScatterPlotSymbol.Clone();
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                base.thePath = null;
                this.DrawScatterPlot(g2);
            }
        }

        private void DrawScatterPlot(Graphics g2)
        {
            ChartSymbol chartsymbol = null;
            base.DisplayDataset = base.theDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray yData = base.DisplayDataset.YData;
            if (this.customScatterPlotSymbol == null)
            {
                chartsymbol = new ChartSymbol(base.chartObjScale, this.scatterPlotSymbol, base.chartObjAttributes);
            }
            else
            {
                chartsymbol = this.customScatterPlotSymbol;
                chartsymbol.SetChartObjScale(base.chartObjScale);
                chartsymbol.SetChartObjAttributes(base.chartObjAttributes);
            }
            chartsymbol.SetChartObjClipping(base.chartObjClipping);
            chartsymbol.SetResizeMultiplier(this.GetResizeMultiplier());
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidData(base.chartObjScale, i))
                {
                    base.SegmentSymbolAttributesSet(i + base.fastClipOffset, chartsymbol);
                    chartsymbol.SetLocation(xData[i], yData[i], 1);
                    chartsymbol.Draw(g2);
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public ChartSymbol GetCustomScatterPlotSymbol()
        {
            return this.customScatterPlotSymbol;
        }

        public int GetScatterPlotSymbol()
        {
            return this.scatterPlotSymbol;
        }

        private void InitDefaults()
        {
            base.chartObjType = 5;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetCustomScatterPlotSymbol(ChartSymbol symbol)
        {
            this.customScatterPlotSymbol = (ChartSymbol) symbol.Clone();
        }

        public void SetScatterPlot(SimpleDataset dataset, int symtype, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            this.scatterPlotSymbol = symtype;
            base.chartObjAttributes.Copy(attrib);
        }

        public void SetScatterPlotSymbol(int nsymbol)
        {
            this.scatterPlotSymbol = nsymbol;
        }

        public ChartSymbol CustomScatterPlotSymbol
        {
            get
            {
                return this.customScatterPlotSymbol;
            }
            set
            {
                this.customScatterPlotSymbol = (ChartSymbol) value.Clone();
            }
        }

        public int ScatterPlotSymbol
        {
            get
            {
                return this.scatterPlotSymbol;
            }
            set
            {
                this.scatterPlotSymbol = value;
            }
        }
    }
}

