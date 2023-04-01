namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class MultiLinePlot : GroupPlot
    {
        public MultiLinePlot()
        {
            this.InitDefaults();
        }

        public MultiLinePlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public MultiLinePlot(PhysicalCoordinates transform, GroupDataset dataset, ChartAttribute[] attribs)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitMultiLinePlot(dataset, attribs);
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
            MultiLinePlot plot = new MultiLinePlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(MultiLinePlot source)
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
                this.DrawMultiLinePlot(g2, base.thePath);
            }
        }

        private void DrawMultiLinePlot(Graphics g2, GraphicsPath path)
        {
            double num4 = 0.0;
            double num5 = 0.0;
            double x = 0.0;
            double y = 0.0;
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray array2 = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            DoubleArray rowObject = yGroupData.GetRowObject(0);
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            bool flag = false;
            for (int i = 0; i < numberGroups; i++)
            {
                ChartAttribute segmentAttributes = base.GetSegmentAttributes(i);
                base.chartObjScale.SetCurrentAttributes(segmentAttributes);
                int firstValidIndex = base.DisplayDataset.GetFirstValidIndex(i);
                if (base.barOrient == 0)
                {
                    num5 = xData[firstValidIndex];
                    num4 = yGroupData[i][firstValidIndex];
                }
                else
                {
                    num4 = xData[firstValidIndex];
                    num5 = yGroupData[i][firstValidIndex];
                }
                flag = false;
                int index = firstValidIndex;
                while (index < numberDatapoints)
                {
                    if (!base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i, index))
                    {
                        flag = true;
                        break;
                    }
                    index++;
                }
                if ((base.stepMode == 3) || flag)
                {
                    for (index = firstValidIndex; index < numberDatapoints; index++)
                    {
                        if (base.barOrient == 0)
                        {
                            num5 = xData[index];
                            num4 = yGroupData[i][index];
                        }
                        else
                        {
                            num4 = xData[index];
                            num5 = yGroupData[i][index];
                        }
                        if (index > firstValidIndex)
                        {
                            if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i, index) && base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i, index - 1))
                            {
                                base.chartObjScale.WMoveToAbs(path, x, y);
                                base.chartObjScale.WStepLineToAbs(path, num4, num5, base.stepMode);
                                if (base.showDatapointValue)
                                {
                                    this.DrawSimpleDatapointValue(g2, xData[index], yGroupData[i][index], yGroupData[i][index]);
                                }
                            }
                            else
                            {
                                if (path.PointCount > 1)
                                {
                                    base.chartObjScale.DrawPath(g2, segmentAttributes.GetCurrentPen(), path);
                                }
                                path.Reset();
                            }
                        }
                        x = num4;
                        y = num5;
                    }
                    if (path.PointCount > 1)
                    {
                        base.chartObjScale.DrawPath(g2, segmentAttributes.GetCurrentPen(), path);
                    }
                    path.Reset();
                }
                else
                {
                    rowObject = yGroupData.GetRowObject(i);
                    rowObject.ShiftLeft(firstValidIndex, true);
                    rowObject.SetLength(numberDatapoints - firstValidIndex);
                    array2 = base.DisplayDataset.XData;
                    array2.ShiftLeft(firstValidIndex, true);
                    xData.SetLength(numberDatapoints - firstValidIndex);
                    base.chartObjScale.WPolyLineAbs(path, array2, rowObject, base.stepMode);
                    if (path.PointCount > 1)
                    {
                        base.chartObjScale.DrawPath(g2, segmentAttributes.GetCurrentPen(), path);
                    }
                    path.Reset();
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        private void InitDefaults()
        {
            base.moveableType = 2;
            base.chartObjType = 0x18;
            base.chartObjClipping = 2;
        }

        public void InitMultiLinePlot(GroupDataset dataset, ChartAttribute[] attribs)
        {
            base.SetDataset(dataset);
            base.InitSegmentAttributes(attribs, dataset);
        }
    }
}

