namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class CandlestickPlot : GroupPlot
    {
        private ChartAttribute boxFillAttributes;

        public CandlestickPlot()
        {
            this.boxFillAttributes = new ChartAttribute();
            this.InitDefaults();
        }

        public CandlestickPlot(PhysicalCoordinates transform)
        {
            this.boxFillAttributes = new ChartAttribute();
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public CandlestickPlot(PhysicalCoordinates transform, GroupDataset dataset, double rwidth, ChartAttribute defaultattrib, ChartAttribute fillattrib)
        {
            this.boxFillAttributes = new ChartAttribute();
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitCandlestickPlot(dataset, rwidth, defaultattrib, fillattrib);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            double findvalue = 0.0;
            Rectangle2D source = new Rectangle2D();
            Rectangle2D dest = new Rectangle2D();
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            double increment = base.barWidth / 2.0;
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    source.SetFrameFromDiagonal(base.chartObjScale.PhysAddX(xData[i], -increment), yGroupData[2][i], base.chartObjScale.PhysAddX(xData[i], increment), yGroupData[1][i]);
                    base.chartObjScale.ConvertRect(dest, 0, source, 1);
                    if ((dest != null) && dest.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
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
            CandlestickPlot plot = new CandlestickPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(CandlestickPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.boxFillAttributes = (ChartAttribute) source.boxFillAttributes.Clone();
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawCandlestickPlot(g2, base.thePath);
            }
        }

        private void DrawCandlestickPlot(Graphics g2, GraphicsPath path)
        {
            double num = 0.0;
            double increment = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            ChartAttribute attrib = new ChartAttribute(base.chartObjAttributes);
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            increment = base.barWidth / 2.0;
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
                    num5 = yGroupData[0][i];
                    num3 = yGroupData[1][i];
                    num4 = yGroupData[2][i];
                    num6 = yGroupData[3][i];
                    Pen currentPen = base.chartObjAttributes.GetCurrentPen();
                    if (base.barOrient == 0)
                    {
                        base.chartObjScale.PhysAddY(xData[i], -increment);
                        base.chartObjScale.PhysAddY(xData[i], increment);
                        base.chartObjScale.WLineAbs(g2, path, num3, xData[i], Math.Max(num5, num6), xData[i], currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, num4, xData[i], Math.Min(num5, num6), xData[i], currentPen, true, false);
                    }
                    else
                    {
                        num = base.chartObjScale.PhysAddX(xData[i], -increment);
                        base.chartObjScale.PhysAddX(xData[i], increment);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num3, xData[i], Math.Max(num5, num6), currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num4, xData[i], Math.Min(num5, num6), currentPen, true, false);
                    }
                    if (num5 > num6)
                    {
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num5, xData[i], num3, currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num6, xData[i], num4, currentPen, true, false);
                        base.chartObjScale.SetCurrentAttributes(this.boxFillAttributes);
                        base.chartObjScale.WRectangle(path, num, num6, base.barWidth, num5 - num6);
                    }
                    else
                    {
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num6, xData[i], num3, currentPen, true, false);
                        base.chartObjScale.WLineAbs(g2, path, xData[i], num5, xData[i], num4, currentPen, true, false);
                        base.chartObjScale.SetCurrentAttributes(attrib);
                        base.chartObjScale.WRectangle(path, num, num5, base.barWidth, num6 - num5);
                    }
                    base.chartObjScale.DrawFillPath(g2, path);
                    path.Reset();
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public ChartAttribute GetBoxFillAttributes()
        {
            return new ChartAttribute(this.boxFillAttributes);
        }

        public void InitCandlestickPlot(GroupDataset dataset, double rwidth, ChartAttribute defaultattrib, ChartAttribute fillattrib)
        {
            base.groupDataset = dataset;
            base.barWidth = rwidth;
            base.chartObjAttributes.Copy(defaultattrib);
            this.boxFillAttributes.Copy(fillattrib);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x1c;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetBoxFillAttributes(ChartAttribute attrib)
        {
            this.boxFillAttributes.Copy(attrib);
        }

        public ChartAttribute BoxFillAttributes
        {
            get
            {
                return new ChartAttribute(this.boxFillAttributes);
            }
            set
            {
                this.boxFillAttributes.Copy(value);
            }
        }
    }
}

