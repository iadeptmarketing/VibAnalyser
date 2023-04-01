namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class OHLCPlot : GroupPlot
    {
        public OHLCPlot()
        {
            this.InitDefaults();
        }

        public OHLCPlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public OHLCPlot(PhysicalCoordinates transform, GroupDataset dataset, double rflagwidth, ChartAttribute attrib)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.SetOHLCPlot(dataset, rflagwidth, attrib);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            double findvalue = 0.0;
            Rectangle2D source = new Rectangle2D();
            Rectangle2D dest = new Rectangle2D();
            bool flag = false;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    source.SetFrameFromDiagonal(base.chartObjScale.PhysAddX(xData[i], -base.barWidth / 2.0), yGroupData[1][i], base.chartObjScale.PhysAddX(xData[i], base.barWidth / 2.0), yGroupData[2][i]);
                    base.chartObjScale.ConvertRect(dest, 0, source, 1);
                    if (dest.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
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
            OHLCPlot plot = new OHLCPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(OHLCPlot source)
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
                this.DrawOHLCPlot(g2, base.thePath);
            }
        }

        private void DrawOHLCPlot(Graphics g2, GraphicsPath path)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            double num9 = 0.0;
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            double increment = base.barWidth / 2.0;
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            Pen currentPen = base.chartObjAttributes.CurrentPen;
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    base.SegmentAttributesSet(i + base.fastClipOffset);
                    currentPen = base.chartObjAttributes.CurrentPen;
                    num8 = yGroupData[0][i];
                    num6 = yGroupData[1][i];
                    num7 = yGroupData[2][i];
                    num9 = yGroupData[3][i];
                    if (base.barOrient == 0)
                    {
                        num2 = base.chartObjScale.PhysAddY(xData[i], -increment);
                        num4 = base.chartObjScale.PhysAddY(xData[i], increment);
                        base.chartObjScale.WLineAbs(g2, path, num7, xData[i], num6, xData[i], currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, num8, xData[i], num8, num2, currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, num9, xData[i], num9, num4, currentPen, true, false);
                    }
                    else
                    {
                        num = base.chartObjScale.PhysAddX(xData[i], -increment);
                        num3 = base.chartObjScale.PhysAddX(xData[i], increment);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num7, xData[i], num6, currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num8, num, num8, currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num9, num3, num9, currentPen, true, false);
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
            base.chartObjType = 0x1b;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetOHLCPlot(GroupDataset dataset, double rflagwidth, ChartAttribute attrib)
        {
            base.groupDataset = dataset;
            base.barWidth = rflagwidth;
            base.chartObjAttributes.Copy(attrib);
        }
    }
}

