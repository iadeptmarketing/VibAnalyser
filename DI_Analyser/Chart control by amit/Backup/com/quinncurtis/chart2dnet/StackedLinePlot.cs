namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class StackedLinePlot : GroupPlot
    {
        public StackedLinePlot()
        {
            this.InitDefaults();
        }

        public StackedLinePlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public StackedLinePlot(PhysicalCoordinates transform, GroupDataset dataset, ChartAttribute[] attribs)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitStackedLinePlot(dataset, attribs);
        }

        private void CalcStackedLine(double x, double sumy, int ngroup, Point2D rpoint)
        {
            double num;
            double num2;
            if (base.barOrient == 0)
            {
                num2 = x;
                num = sumy;
            }
            else
            {
                num = x;
                num2 = sumy;
            }
            rpoint.SetLocation(num, num2);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D dest = new Rectangle2D();
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            DoubleArray array2 = new DoubleArray(numberDatapoints);
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            Dimension source = new Dimension(base.intersectionTestDistance, base.intersectionTestDistance);
            source = base.chartObjScale.ConvertDimension(1, source, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                array2[i] = 0.0;
                for (int j = 0; j < numberGroups; j++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, j, i))
                    {
                        DoubleArray array3;
                        int num7;
                        double num1 = array2[i];
                        (array3 = array2)[num7 = i] = array3[num7] + yGroupData[j][i];
                        double width = source.Width;
                        double height = source.Height;
                        dest.SetFrame(xData[i] - width, array2[i] - height, width * 2.0, height * 2.0);
                        base.chartObjScale.ConvertRect(dest, 0, dest, 1);
                        if ((dest != null) && dest.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                        {
                            flag = true;
                            np.nearestPointValid = true;
                            np.actualPoint.SetLocation(p);
                            np.nearestPointMinDistance = 0.0;
                            np.nearestPointIndex = i + base.fastClipOffset;
                            np.nearestGroupIndex = j;
                            np.nearestPoint.SetLocation(base.DisplayDataset.GetXDataValue(np.nearestPointIndex), base.DisplayDataset.GetYDataValue(np.nearestGroupIndex, np.nearestPointIndex));
                            break;
                        }
                    }
                }
                if (flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public override object Clone()
        {
            StackedLinePlot plot = new StackedLinePlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(StackedLinePlot source)
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
                this.DrawStackedLinePlot(g2, base.thePath);
            }
        }

        private void DrawStackedLinePlot(Graphics g2, GraphicsPath path)
        {
            Point2D rpoint = new Point2D();
            Point2D pointd2 = new Point2D();
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            BoolArray validData = base.DisplayDataset.ValidData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            DoubleArray2D arrayd2 = base.CalcGroupYSumArray(xData, yGroupData, validData);
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            for (int i = numberGroups - 1; i >= 0; i--)
            {
                int num;
                ChartAttribute segmentAttributes = base.GetSegmentAttributes(i);
                base.chartObjScale.SetCurrentAttributes(segmentAttributes);
                if (!segmentAttributes.GetFillFlag())
                {
                    num = 0;
                    while (num < numberDatapoints)
                    {
                        if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i, num))
                        {
                            pointd2.SetLocation(rpoint.GetX(), rpoint.GetY());
                            this.CalcStackedLine(xData[num], arrayd2[i][num], i, rpoint);
                            if (num > 0)
                            {
                                base.chartObjScale.WStepLineAbs(path, pointd2.GetX(), pointd2.GetY(), rpoint.GetX(), rpoint.GetY(), base.stepMode);
                            }
                            if (base.showDatapointValue)
                            {
                                this.DrawSimpleDatapointValue(g2, xData[num], yGroupData[i][num], yGroupData[i][num]);
                            }
                        }
                        else
                        {
                            base.chartObjScale.DrawPath(g2, segmentAttributes.GetCurrentPen(), path);
                            path.Reset();
                        }
                        num++;
                    }
                    base.chartObjScale.DrawPath(g2, segmentAttributes.GetCurrentPen(), path);
                    path.Reset();
                }
                else
                {
                    int num5 = 5;
                    double[] xsource = new double[2];
                    double[] ysource = new double[2];
                    double[] xdest = new double[num5];
                    double[] ydest = new double[num5];
                    bool[] valid = new bool[2];
                    for (num = 0; num < numberDatapoints; num++)
                    {
                        if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i, num))
                        {
                            pointd2.SetLocation(rpoint.GetX(), rpoint.GetY());
                            this.CalcStackedLine(xData[num], arrayd2[i][num], i, rpoint);
                            xsource[0] = pointd2.GetX();
                            ysource[0] = pointd2.GetY();
                            valid[0] = validData[Math.Max(0, num - 1)];
                            xsource[1] = rpoint.GetX();
                            ysource[1] = rpoint.GetY();
                            valid[1] = validData[num];
                            if ((num > 0) && (base.CreateLineFillArrays(xdest, ydest, xsource, ysource, valid, 2, base.barOrient) > 1))
                            {
                                base.chartObjScale.WPolyLineAbs(path, xdest, ydest, 5, base.stepMode);
                                base.chartObjScale.DrawFillPath(g2, path);
                                path.Reset();
                                if (base.showDatapointValue)
                                {
                                    this.DrawSimpleDatapointValue(g2, xData[num], yGroupData[i][num], yGroupData[i][num]);
                                }
                            }
                        }
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
            base.chartObjType = 0x17;
            base.chartObjClipping = 2;
            base.moveableType = 0;
        }

        public void InitStackedLinePlot(GroupDataset dataset, ChartAttribute[] attribs)
        {
            base.SetDataset(dataset);
            base.InitSegmentAttributes(attribs, dataset);
            base.stackMode = true;
        }
    }
}

