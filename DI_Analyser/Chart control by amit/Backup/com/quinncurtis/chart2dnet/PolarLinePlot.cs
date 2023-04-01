namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PolarLinePlot : PolarPlot
    {
        public PolarLinePlot()
        {
            this.InitDefaults();
        }

        public PolarLinePlot(PolarCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public PolarLinePlot(PolarCoordinates transform, SimpleDataset dataset, ChartAttribute attrib)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitPolarLinePlot(dataset, attrib);
        }

        public override object Clone()
        {
            PolarLinePlot plot = new PolarLinePlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(PolarLinePlot source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawPolarLinePlot(g2, base.thePath);
            }
        }

        private void DrawPolarLinePlot(Graphics g2, GraphicsPath path)
        {
            int num;
            DoubleArray xData = base.theDataset.XData;
            DoubleArray yData = base.theDataset.YData;
            int numberDatapoints = base.theDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            if (!base.chartObjAttributes.GetFillFlag())
            {
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.theDataset.CheckValidData(base.chartObjScale, num) && base.theDataset.CheckValidData(base.chartObjScale, num + 1))
                    {
                        base.SegmentAttributesSet(num);
                        ((PolarCoordinates) base.chartObjScale).PolarMoveToAbs(path, xData[num], yData[num]);
                        ((PolarCoordinates) base.chartObjScale).PolarLineToAbs(path, xData[num + 1], yData[num + 1], true);
                        base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), path);
                        path.Reset();
                    }
                }
            }
            else if (base.chartObjAttributes.GetFillFlag() && !base.segmentColorMode)
            {
                num = 0;
                while (num < numberDatapoints)
                {
                    if (base.theDataset.CheckValidData(base.chartObjScale, num))
                    {
                        if (num == 0)
                        {
                            ((PolarCoordinates) base.chartObjScale).PolarMoveToAbs(path, xData[num], 0.0);
                        }
                        ((PolarCoordinates) base.chartObjScale).PolarLineToAbs(path, xData[num], yData[num], true);
                    }
                    num++;
                }
                ((PolarCoordinates) base.chartObjScale).PolarLineToAbs(path, xData[num], 0.0, true);
                base.chartObjScale.DrawFillPath(g2, path);
                path.Reset();
            }
            else if (base.chartObjAttributes.GetFillFlag() && base.segmentColorMode)
            {
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.theDataset.CheckValidData(base.chartObjScale, num) && base.theDataset.CheckValidData(base.chartObjScale, num + 1))
                    {
                        base.SegmentAttributesSet(num);
                        ((PolarCoordinates) base.chartObjScale).PolarMoveToAbs(path, xData[num], 0.0);
                        ((PolarCoordinates) base.chartObjScale).PolarLineToAbs(path, xData[num], yData[num], true);
                        ((PolarCoordinates) base.chartObjScale).PolarLineToAbs(path, xData[num + 1], yData[num + 1], true);
                        ((PolarCoordinates) base.chartObjScale).PolarLineToAbs(path, xData[num + 1], 0.0, true);
                        base.chartObjScale.DrawFillPath(g2, path);
                        path.Reset();
                    }
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        private void InitDefaults()
        {
            base.chartObjClipping = 2;
            base.moveableType = 2;
            base.positionType = 2;
            base.chartObjType = 60;
        }

        public int InitPolarLinePlot(SimpleDataset dataset, ChartAttribute attrib)
        {
            base.theDataset = dataset;
            base.chartObjAttributes.Copy(attrib);
            return 1;
        }
    }
}

