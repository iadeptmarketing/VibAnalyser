namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class FloatingBarPlot : GroupPlot
    {
        public FloatingBarPlot()
        {
            this.InitDefaults();
        }

        public FloatingBarPlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public FloatingBarPlot(PhysicalCoordinates transform, GroupDataset dataset, double rbarwidth, ChartAttribute attrib, int nbarjust)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitFloatingBarPlot(dataset, rbarwidth, attrib, nbarjust);
        }

        private void CalcFloatingBarRect(double x, double start, double stop, Rectangle2D rect)
        {
            double num;
            double num2;
            double num4;
            double num5;
            double barWidth;
            if (base.barJust == 1)
            {
                barWidth = base.barWidth / 2.0;
            }
            else if (base.barJust == 2)
            {
                barWidth = base.barWidth;
            }
            else
            {
                barWidth = 0.0;
            }
            if (base.barOrient == 0)
            {
                num = start;
                double num3 = stop;
                num2 = base.chartObjScale.PhysAddY(x, -barWidth);
                num5 = base.barWidth;
                num4 = num3 - num;
            }
            else
            {
                num2 = start;
                num = base.chartObjScale.PhysAddX(x, -barWidth);
                num5 = stop - start;
                num4 = base.barWidth;
            }
            rect.SetFrame(num, num2, num4, num5);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            double findvalue = 0.0;
            Rectangle2D rect = new Rectangle2D();
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    this.CalcFloatingBarRect(xData[i], yGroupData[0][i], yGroupData[1][i], rect);
                    base.chartObjScale.ConvertRect(rect, 0, rect, 1);
                    if (rect.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                    {
                        flag = true;
                        np.nearestPointValid = true;
                        np.actualPoint.SetLocation(p);
                        np.nearestPointMinDistance = 0.0;
                        np.nearestPointIndex = i;
                        if (base.coordinateSwap)
                        {
                            findvalue = p.GetX();
                        }
                        else
                        {
                            findvalue = p.GetY();
                        }
                        np.nearestGroupIndex = base.DisplayDataset.FindNearestGroup(i, findvalue);
                        np.nearestPoint.SetLocation(base.DisplayDataset.GetXDataValue(np.nearestPointIndex), base.DisplayDataset.GetYDataValue(np.nearestGroupIndex, np.nearestPointIndex));
                        if (base.coordinateSwap)
                        {
                            ChartSupport.SwapCoords(np.nearestPoint);
                        }
                        np.nearestPointIndex = i + base.fastClipOffset;
                        return flag;
                    }
                }
            }
            return flag;
        }

        public override object Clone()
        {
            FloatingBarPlot plot = new FloatingBarPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(FloatingBarPlot source)
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
                this.DrawFloatingBarPlot(g2, base.thePath);
            }
        }

        private void DrawFloatingBarPlot(Graphics g2, GraphicsPath path)
        {
            Rectangle2D rect = new Rectangle2D();
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    base.SegmentAttributesSet(i + base.fastClipOffset);
                    this.CalcFloatingBarRect(xData[i], yGroupData[0][i], yGroupData[1][i], rect);
                    base.chartObjScale.WRectangle(path, rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
                    base.chartObjScale.DrawFillPath(g2, path);
                    path.Reset();
                    if (base.showDatapointValue)
                    {
                        this.DrawBarDatapointValue(g2, xData[i], yGroupData[1][i], rect);
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
            base.chartObjType = 0x1a;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void InitFloatingBarPlot(GroupDataset dataset, double rbarwidth, ChartAttribute attrib, int nbarjust)
        {
            base.SetDataset(dataset);
            base.barWidth = rbarwidth;
            base.chartObjAttributes.Copy(attrib);
            base.barJust = nbarjust;
        }
    }
}

