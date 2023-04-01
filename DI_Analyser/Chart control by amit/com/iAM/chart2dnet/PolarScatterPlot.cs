namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class PolarScatterPlot : PolarPlot
    {
        private ChartSymbol customScatterPlotSymbol;
        internal int scatterPlotSymbol;

        public PolarScatterPlot()
        {
            this.scatterPlotSymbol = 0;
            this.customScatterPlotSymbol = null;
            this.InitDefaults();
        }

        public PolarScatterPlot(PolarCoordinates transform)
        {
            this.scatterPlotSymbol = 0;
            this.customScatterPlotSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public PolarScatterPlot(PolarCoordinates transform, SimpleDataset dataset, int symtype, ChartAttribute attrib)
        {
            this.scatterPlotSymbol = 0;
            this.customScatterPlotSymbol = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitPolarScatterPlot(dataset, symtype, attrib);
        }

        public override object Clone()
        {
            PolarScatterPlot plot = new PolarScatterPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(PolarScatterPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.customScatterPlotSymbol != null)
                {
                    this.customScatterPlotSymbol = (ChartSymbol) source.customScatterPlotSymbol.Clone();
                }
                this.scatterPlotSymbol = source.scatterPlotSymbol;
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawPolarScatterPlot(g2);
            }
        }

        private void DrawPolarScatterPlot(Graphics g2)
        {
            DoubleArray xData = base.theDataset.XData;
            DoubleArray yData = base.theDataset.YData;
            int numberDatapoints = base.theDataset.GetNumberDatapoints();
            Point2D dest = new Point2D();
            Point2D source = new Point2D();
            ChartSymbol customScatterPlotSymbol = null;
            numberDatapoints = base.theDataset.GetNumberDatapoints();
            if (this.customScatterPlotSymbol == null)
            {
                customScatterPlotSymbol = new ChartSymbol(base.chartObjScale, this.scatterPlotSymbol, base.chartObjAttributes);
            }
            else
            {
                customScatterPlotSymbol = this.customScatterPlotSymbol;
                customScatterPlotSymbol.SetChartObjScale(base.chartObjScale);
                customScatterPlotSymbol.SetChartObjAttributes(base.chartObjAttributes);
            }
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.theDataset.CheckValidData(base.chartObjScale, i))
                {
                    base.SegmentAttributesSet(i);
                    source.SetLocation(xData[i], yData[i]);
                    base.chartObjScale.ConvertCoord(dest, 1, source, 2);
                    customScatterPlotSymbol.SetLocation(dest.GetX(), dest.GetY(), 1);
                    customScatterPlotSymbol.Draw(g2);
                    if (base.showDatapointValue)
                    {
                        this.DrawPolarDatapointValue(g2, dest.GetX(), dest.GetY(), ChartSupport.ToDegrees(yData[i]));
                    }
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
            base.chartObjClipping = 2;
            base.moveableType = 2;
            base.positionType = 2;
            base.chartObjType = 0x3d;
        }

        public void InitPolarScatterPlot(SimpleDataset dataset, int symtype, ChartAttribute attrib)
        {
            base.theDataset = dataset;
            this.scatterPlotSymbol = symtype;
            base.chartObjAttributes.Copy(attrib);
        }

        public void SetCustomScatterPlotSymbol(ChartSymbol symbol)
        {
            this.customScatterPlotSymbol = (ChartSymbol) symbol.Clone();
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

