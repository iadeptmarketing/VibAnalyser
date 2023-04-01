namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ErrorBarPlot : GroupPlot
    {
        internal bool errorBarLineFlag;

        public ErrorBarPlot()
        {
            this.errorBarLineFlag = false;
            this.InitDefaults();
        }

        public ErrorBarPlot(PhysicalCoordinates transform)
        {
            this.errorBarLineFlag = false;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public ErrorBarPlot(PhysicalCoordinates transform, GroupDataset dataset, double rbarwidth, ChartAttribute attrib)
        {
            this.errorBarLineFlag = false;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitErrorBarPlot(dataset, rbarwidth, attrib);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            double findvalue = 0.0;
            Rectangle2D rectangled = new Rectangle2D();
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    rectangled.SetFrame(xData[i] - (base.barWidth / 2.0), yGroupData[0][i], base.barWidth, yGroupData[1][i] - yGroupData[0][i]);
                    if ((rectangled != null) && rectangled.Contains((double) ((int) p.GetX()), (double) ((int) p.GetY())))
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
            ErrorBarPlot plot = new ErrorBarPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(ErrorBarPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.errorBarLineFlag = source.errorBarLineFlag;
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawErrorBarPlot(g2, base.thePath);
            }
        }

        private void DrawErrorBarPlot(Graphics g2, GraphicsPath path)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double increment = 0.0;
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
                base.SegmentAttributesSet(i + base.fastClipOffset);
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    if (base.barOrient == 0)
                    {
                        num2 = base.chartObjScale.PhysAddY(xData[i], -increment);
                        num4 = base.chartObjScale.PhysAddY(xData[i], increment);
                        num = yGroupData[0][i];
                        num3 = yGroupData[1][i];
                        base.chartObjScale.WLineAbs(g2, path, num, num2, num, num4, base.chartObjAttributes.GetCurrentPen(), true, false);
                        base.chartObjScale.WLineAbs(g2, path, num3, num2, num3, num4, base.chartObjAttributes.GetCurrentPen(), true, false);
                        if (this.errorBarLineFlag)
                        {
                            base.chartObjScale.WLineAbs(g2, path, num, num2 + increment, num3, num2 + increment, base.chartObjAttributes.GetCurrentPen(), true, false);
                        }
                    }
                    else
                    {
                        num = base.chartObjScale.PhysAddX(xData[i], -increment);
                        num3 = base.chartObjScale.PhysAddX(xData[i], increment);
                        num2 = yGroupData[0][i];
                        num4 = yGroupData[1][i];
                        base.chartObjScale.WLineAbs(g2, path, num, num2, num3, num2, base.chartObjAttributes.GetCurrentPen(), true, false);
                        base.chartObjScale.WLineAbs(g2, path, num, num4, num3, num4, base.chartObjAttributes.GetCurrentPen(), true, false);
                        if (this.errorBarLineFlag)
                        {
                            base.chartObjScale.WLineAbs(g2, path, num + increment, num2, num + increment, num4, base.chartObjAttributes.GetCurrentPen(), true, false);
                        }
                    }
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public bool GetErrorBarLineFlag()
        {
            return this.errorBarLineFlag;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x19;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void InitErrorBarPlot(GroupDataset dataset, double rbarwidth, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            base.barWidth = rbarwidth;
            base.chartObjAttributes.Copy(attrib);
        }

        public void SetErrorBarLineFlag(bool berrorbarline)
        {
            this.errorBarLineFlag = berrorbarline;
        }

        public bool ErrorBarLineFlag
        {
            get
            {
                return this.errorBarLineFlag;
            }
            set
            {
                this.errorBarLineFlag = value;
            }
        }
    }
}

