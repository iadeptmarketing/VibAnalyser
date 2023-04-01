namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LineGapPlot : GroupPlot
    {
        public LineGapPlot()
        {
            this.InitDefaults();
        }

        public LineGapPlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public LineGapPlot(PhysicalCoordinates transform, GroupDataset dataset, ChartAttribute attrib)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitLineGapPlot(dataset, attrib);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Point2D pointd = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            bool flag = false;
            if (this.CalcNearestPoint(pointd, 5, np) && (np.nearestPointMinDistance < base.intersectionTestDistance))
            {
                flag = true;
                np.nearestPointIndex += base.fastClipOffset;
            }
            return flag;
        }

        public override object Clone()
        {
            LineGapPlot plot = new LineGapPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(LineGapPlot source)
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
                this.DrawLineGapPlot(g2, base.thePath);
            }
        }

        private void DrawLineGapPlot(Graphics g2, GraphicsPath path)
        {
            int num;
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            if (!base.chartObjAttributes.GetFillFlag() && !base.segmentColorMode)
            {
                path.Reset();
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num) && base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num + 1))
                    {
                        base.chartObjScale.WLineAbs(g2, path, xData[num], yGroupData[0][num], xData[num + 1], yGroupData[0][num + 1], base.chartObjAttributes.GetCurrentPen(), true, true);
                    }
                }
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num) && base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num + 1))
                    {
                        base.chartObjScale.WLineAbs(g2, path, xData[num], yGroupData[1][num], xData[num + 1], yGroupData[1][num + 1], base.chartObjAttributes.GetCurrentPen(), true, true);
                    }
                }
            }
            else if (!base.chartObjAttributes.GetFillFlag() && base.segmentColorMode)
            {
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num) && base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num + 1))
                    {
                        base.SegmentAttributesSet(num + base.fastClipOffset);
                        base.chartObjScale.WLineAbs(g2, path, xData[num], yGroupData[0][num], xData[num + 1], yGroupData[0][num + 1], base.chartObjAttributes.GetCurrentPen(), true, false);
                    }
                }
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num) && base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num + 1))
                    {
                        base.SegmentAttributesSet(num + base.fastClipOffset);
                        base.chartObjScale.WLineAbs(g2, path, xData[num], yGroupData[1][num], xData[num + 1], yGroupData[1][num + 1], base.chartObjAttributes.GetCurrentPen(), true, false);
                    }
                }
            }
            else if (base.chartObjAttributes.GetFillFlag())
            {
                int num3 = 5;
                double[] x = new double[5];
                double[] y = new double[5];
                double[] numArray1 = new double[num3];
                double[] numArray3 = new double[num3];
                for (num = 0; num < (numberDatapoints - 1); num++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num) && base.DisplayDataset.CheckValidGroupData(base.chartObjScale, num + 1))
                    {
                        if (base.barOrient == 0)
                        {
                            y[0] = xData[num];
                            x[0] = yGroupData[0][num];
                            y[1] = xData[num + 1];
                            x[1] = yGroupData[0][num + 1];
                            y[2] = xData[num + 1];
                            x[2] = yGroupData[1][num + 1];
                            y[3] = xData[num];
                            x[3] = yGroupData[1][num];
                            y[4] = xData[num];
                            x[4] = yGroupData[0][num];
                        }
                        else
                        {
                            x[0] = xData[num];
                            y[0] = yGroupData[0][num];
                            x[1] = xData[num + 1];
                            y[1] = yGroupData[0][num + 1];
                            x[2] = xData[num + 1];
                            y[2] = yGroupData[1][num + 1];
                            x[3] = xData[num];
                            y[3] = yGroupData[1][num];
                            x[4] = xData[num];
                            y[4] = yGroupData[0][num];
                        }
                        base.SegmentAttributesSet(num + base.fastClipOffset);
                        base.chartObjScale.WPolyLineAbs(path, x, y, 5, base.stepMode);
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
            base.chartObjType = 0x1f;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void InitLineGapPlot(GroupDataset dataset, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            base.chartObjAttributes.Copy(attrib);
        }
    }
}

