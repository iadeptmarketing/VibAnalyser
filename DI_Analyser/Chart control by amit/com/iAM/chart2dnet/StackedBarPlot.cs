namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class StackedBarPlot : GroupPlot
    {
        public StackedBarPlot()
        {
            this.InitDefaults();
        }

        public StackedBarPlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public StackedBarPlot(PhysicalCoordinates transform, GroupDataset dataset, double rbarwidth, double rbarbase, ChartAttribute[] attribs, int nbarjust)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitStackedBarPlot(dataset, rbarwidth, rbarbase, attribs, nbarjust);
        }

        private void CalcStackedBarRect(double x, double sumy, double yprev, int ngroup, Rectangle2D rect)
        {
            double num;
            double num2;
            double num3;
            double num4;
            double num5;
            double num6;
            double num7;
            if (base.barJust == 1)
            {
                num7 = base.barWidth / 2.0;
            }
            else if (base.barJust == 2)
            {
                num7 = base.barWidth;
            }
            else
            {
                num7 = 0.0;
            }
            double barWidth = base.barWidth;
            if (base.barOrient == 0)
            {
                num = yprev;
                num2 = base.chartObjScale.PhysAddY(x, -num7);
                num3 = sumy;
                num4 = num2 + base.barWidth;
                num6 = barWidth;
                num5 = num3 - num;
            }
            else
            {
                num2 = yprev;
                num = x - num7;
                num3 = base.chartObjScale.PhysAddX(num, base.barWidth);
                num4 = sumy;
                num6 = num4 - num2;
                num5 = base.barWidth;
            }
            rect.SetFrame(num, num2, num5, num6);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D rect = new Rectangle2D();
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            DoubleArray array2 = new DoubleArray(numberDatapoints);
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                array2[i] = 0.0;
                for (int j = 0; j < numberGroups; j++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, j, i))
                    {
                        DoubleArray array3;
                        int num6;
                        double yprev = array2[i];
                        (array3 = array2)[num6 = i] = array3[num6] + yGroupData[j][i];
                        this.CalcStackedBarRect(xData[i], array2[i], yprev, j, rect);
                        base.chartObjScale.ConvertRect(rect, 0, rect, 1);
                        if ((rect != null) && rect.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                        {
                            flag = true;
                            np.nearestPointValid = true;
                            np.actualPoint.SetLocation(p);
                            np.nearestPointMinDistance = 0.0;
                            np.nearestPointIndex = i;
                            np.nearestGroupIndex = j;
                            np.nearestPoint.SetLocation(base.DisplayDataset.GetXDataValue(np.nearestPointIndex), base.DisplayDataset.GetYDataValue(np.nearestGroupIndex, np.nearestPointIndex));
                            if (base.coordinateSwap)
                            {
                                ChartSupport.SwapCoords(np.nearestPoint);
                            }
                            np.nearestPointIndex = i + base.fastClipOffset;
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
            StackedBarPlot plot = new StackedBarPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(StackedBarPlot source)
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
                this.DrawStackedBarPlot(g2, base.thePath);
            }
        }

        private void DrawStackedBarPlot(Graphics g2, GraphicsPath path)
        {
            double yprev = 0.0;
            Rectangle2D rect = new Rectangle2D();
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            DoubleArray array2 = new DoubleArray(numberDatapoints);
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            for (int i = 0; i < numberDatapoints; i++)
            {
                array2[i] = 0.0;
                for (int j = 0; j < numberGroups; j++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, j, i))
                    {
                        DoubleArray array3;
                        int num6;
                        yprev = array2[i];
                        (array3 = array2)[num6 = i] = array3[num6] + yGroupData[j][i];
                        this.CalcStackedBarRect(xData[i], array2[i], yprev, j, rect);
                        ChartAttribute segmentAttributes = base.GetSegmentAttributes(j);
                        base.chartObjScale.SetCurrentAttributes(segmentAttributes);
                        base.chartObjScale.WRectangle(path, rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
                        base.chartObjScale.DrawFillPath(g2, path);
                        path.Reset();
                        if (base.showDatapointValue)
                        {
                            this.DrawBarDatapointValue(g2, xData[i], yGroupData[j][i], rect);
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
            base.chartObjType = 0x16;
            base.chartObjClipping = 2;
            base.moveableType = 0;
        }

        public void InitStackedBarPlot(GroupDataset dataset, double rbarwidth, double rbarbase, ChartAttribute[] attribs, int nbarjust)
        {
            base.SetDataset(dataset);
            base.barWidth = rbarwidth;
            base.fillBaseValue = rbarbase;
            base.barJust = nbarjust;
            base.InitSegmentAttributes(attribs, dataset);
            base.stackMode = true;
        }
    }
}

