namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class GroupBarPlot : GroupPlot
    {
        internal double barOverlap;

        public GroupBarPlot()
        {
            this.barOverlap = 0.0;
            this.InitDefaults();
        }

        public GroupBarPlot(PhysicalCoordinates transform)
        {
            this.barOverlap = 0.0;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public GroupBarPlot(PhysicalCoordinates transform, GroupDataset dataset, double rbarwidth, double rbarbase, ChartAttribute[] attribs, int nbarjust)
        {
            this.barOverlap = 0.0;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitGroupBarPlot(dataset, rbarwidth, rbarbase, attribs, nbarjust);
        }

        private void CalcGroupBarRect(double x, double y, int ngroup, Rectangle2D rect)
        {
            double fillBaseValue;
            double num2;
            double num5;
            double num6;
            double barWidth;
            Dimension source = new Dimension(1.0, 1.0);
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
            double num8 = base.barWidth / ((double) base.DisplayDataset.GetNumberGroups());
            source = base.chartObjScale.ConvertDimension(1, source, 0);
            if (base.barOrient == 0)
            {
                fillBaseValue = base.fillBaseValue;
                double num3 = y;
                num2 = base.chartObjScale.PhysAddY(x, -barWidth + ((ngroup * num8) * (1.0 - this.barOverlap)));
                num6 = num8 - source.GetHeight();
                num5 = num3 - fillBaseValue;
            }
            else
            {
                num2 = base.fillBaseValue;
                double num4 = y;
                fillBaseValue = base.chartObjScale.PhysAddX(x, -barWidth + ((ngroup * num8) * (1.0 - this.barOverlap)));
                num6 = num4 - base.fillBaseValue;
                num5 = num8 - source.GetWidth();
            }
            rect.SetFrame(fillBaseValue, num2, num5, num6);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D rect = new Rectangle2D();
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                for (int j = 0; j < numberGroups; j++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, j, i))
                    {
                        this.CalcGroupBarRect(xData[i], yGroupData[j][i], j, rect);
                        base.chartObjScale.ConvertRect(rect, 0, rect, 1);
                        if (rect.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
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
            GroupBarPlot plot = new GroupBarPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(GroupBarPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.barOverlap = source.barOverlap;
                base.stackMode = source.stackMode;
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawGroupBarPlot(g2, base.thePath);
            }
        }

        private void DrawGroupBarPlot(Graphics g2, GraphicsPath path)
        {
            Rectangle2D rect = new Rectangle2D();
            ChartAttribute attrib = new ChartAttribute(base.chartObjAttributes);
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            int numberGroups = base.DisplayDataset.GetNumberGroups();
            base.chartObjScale.SetCurrentAttributes(attrib);
            if (base.barOrient == 0)
            {
                base.coordinateSwap = true;
            }
            for (int i = 0; i < numberDatapoints; i++)
            {
                for (int j = 0; j < numberGroups; j++)
                {
                    if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, j, i))
                    {
                        this.CalcGroupBarRect(xData[i], yGroupData[j][i], j, rect);
                        attrib = base.GetSegmentAttributes(j);
                        base.chartObjScale.SetCurrentAttributes(attrib);
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

        public double GetBarOverlap()
        {
            return this.barOverlap;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x15;
            base.chartObjClipping = 2;
            base.moveableType = 0;
        }

        public void InitGroupBarPlot(GroupDataset dataset, double rbarwidth, double rbarbase, ChartAttribute[] attribs, int nbarjust)
        {
            base.SetDataset(dataset);
            base.barWidth = rbarwidth;
            base.fillBaseValue = rbarbase;
            base.barJust = nbarjust;
            base.InitSegmentAttributes(attribs, dataset);
        }

        public void SetBarOverlap(double baroverlap)
        {
            this.barOverlap = baroverlap;
        }

        public double BarOverlap
        {
            get
            {
                return this.barOverlap;
            }
            set
            {
                this.barOverlap = value;
            }
        }
    }
}

