namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SimpleLinePlot : SimplePlot
    {
        public SimpleLinePlot()
        {
            this.InitDefaults();
        }

        public SimpleLinePlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public SimpleLinePlot(PhysicalCoordinates transform, SimpleDataset dataset, ChartAttribute attrib)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.SetLinePlot(dataset, attrib);
        }

        public override object Clone()
        {
            SimpleLinePlot plot = new SimpleLinePlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(SimpleLinePlot source)
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
                this.DrawLinePlot(g2, base.thePath);
            }
        }

        public void DrawLinePlot(Graphics g2, GraphicsPath path)
        {
            int num;
            int numpoints = 1;
            base.DisplayDataset = base.theDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            numpoints = base.DisplayDataset.GetNumberDatapoints();
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray yData = base.DisplayDataset.YData;
            BoolArray validData = base.DisplayDataset.ValidData;
            DoubleArray x = new DoubleArray();
            DoubleArray y = new DoubleArray();
            Pen currentPen = base.chartObjAttributes.CurrentPen;
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            bool flag = false;
            for (num = 0; num < numpoints; num++)
            {
                if (!base.DisplayDataset.CheckValidData(base.chartObjScale, num))
                {
                    flag = true;
                    break;
                }
            }
            if (!base.chartObjAttributes.GetFillFlag() && !base.segmentColorMode)
            {
                if ((base.stepMode == 3) || flag)
                {
                    num = 0;
                    while (num < numpoints)
                    {
                        if (base.DisplayDataset.CheckValidData(base.chartObjScale, num))
                        {
                            x.Add(xData[num]);
                            y.Add(yData[num]);
                        }
                        else
                        {
                            if (x.Length > 1)
                            {
                                base.chartObjScale.WPolyLineAbs(path, x, y, base.stepMode);
                                x.Clear();
                                y.Clear();
                                if ((path.PointCount > 1) && base.chartObjAttributes.GetLineFlag())
                                {
                                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), path);
                                }
                            }
                            path.Reset();
                        }
                        num++;
                    }
                    if (x.Length > 1)
                    {
                        base.chartObjScale.WPolyLineAbs(path, x, y, base.stepMode);
                        x.Clear();
                        y.Clear();
                        if (path.PointCount > 1)
                        {
                            base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), path);
                        }
                    }
                    path.Reset();
                }
                else
                {
                    base.chartObjScale.WPolyLineAbs(path, xData, yData, base.stepMode);
                    if (path.PointCount > 1)
                    {
                        base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), path);
                    }
                    path.Reset();
                }
            }
            else if (!base.chartObjAttributes.GetFillFlag() && base.segmentColorMode)
            {
                Color primaryColor = base.GetSegmentAttributes(num + base.fastClipOffset).PrimaryColor;
                bool flag2 = false;
                bool flag3 = true;
                base.SegmentAttributesSet(base.fastClipOffset);
                for (num = 0; num < numpoints; num++)
                {
                    flag3 = true;
                    if (!primaryColor.Equals(base.GetSegmentAttributes(num + base.fastClipOffset).PrimaryColor))
                    {
                        base.LineColor = primaryColor;
                        flag2 = true;
                        x.Add(xData[num]);
                        y.Add(yData[num]);
                    }
                    if (!base.DisplayDataset.CheckValidData(base.chartObjScale, num))
                    {
                        flag2 = true;
                        flag3 = false;
                    }
                    if (flag2)
                    {
                        if (x.Length > 1)
                        {
                            base.chartObjScale.WPolyLineAbs(path, x, y, base.stepMode);
                            x.Clear();
                            y.Clear();
                            if (path.PointCount > 1)
                            {
                                base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), path);
                            }
                        }
                        path.Reset();
                        flag2 = false;
                    }
                    if (flag3)
                    {
                        x.Add(xData[num]);
                        y.Add(yData[num]);
                    }
                    primaryColor = base.GetSegmentAttributes(num + base.fastClipOffset).PrimaryColor;
                }
                if (x.Length > 1)
                {
                    base.LineColor = primaryColor;
                    base.chartObjScale.WPolyLineAbs(path, x, y, base.stepMode);
                    x.Clear();
                    y.Clear();
                    if (path.PointCount > 1)
                    {
                        base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), path);
                    }
                }
                path.Reset();
            }
            else if (base.chartObjAttributes.GetFillFlag() && !base.segmentColorMode)
            {
                int numdat = numpoints + 3;
                double[] xdest = new double[numdat];
                double[] ydest = new double[numdat];
                numpoints = base.CreateLineFillArrays(xdest, ydest, xData.DataBuffer, yData.DataBuffer, validData.DataBuffer, numpoints, base.barOrient);
                if (numpoints > 1)
                {
                    base.chartObjScale.WPolyLineAbs(path, xdest, ydest, numdat, base.stepMode);
                    base.chartObjScale.DrawFillPath(g2, path);
                    path.Reset();
                }
            }
            else if (base.chartObjAttributes.GetFillFlag() && base.segmentColorMode)
            {
                int num4 = 5;
                double[] xsource = new double[2];
                double[] ysource = new double[2];
                double[] numArray5 = new double[num4];
                double[] numArray6 = new double[num4];
                bool[] valid = new bool[2];
                for (num = 0; num < (numpoints - 1); num++)
                {
                    xsource[0] = xData[num];
                    ysource[0] = yData[num];
                    valid[0] = validData[num];
                    xsource[1] = xData[num + 1];
                    ysource[1] = yData[num + 1];
                    valid[1] = validData[num + 1];
                    if ((base.DisplayDataset.CheckValidData(base.chartObjScale, num) && base.DisplayDataset.CheckValidData(base.chartObjScale, num + 1)) && (base.CreateLineFillArrays(numArray5, numArray6, xsource, ysource, valid, 2, base.barOrient) > 1))
                    {
                        if (base.segmentColorMode)
                        {
                            base.SegmentAttributesSet(num + base.fastClipOffset);
                        }
                        base.chartObjScale.WPolyLineAbs(path, numArray5, numArray6, 5, base.stepMode);
                        base.chartObjScale.DrawFillPath(g2, path);
                        path.Reset();
                    }
                }
            }
            if (base.showDatapointValue)
            {
                for (num = 0; num < numpoints; num++)
                {
                    if (base.DisplayDataset.CheckValidData(base.chartObjScale, num))
                    {
                        this.DrawSimpleDatapointValue(g2, xData[num], yData[num], yData[num]);
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
            base.chartObjType = 1;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetLinePlot(SimpleDataset dataset, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            base.chartObjAttributes.Copy(attrib);
        }
    }
}

