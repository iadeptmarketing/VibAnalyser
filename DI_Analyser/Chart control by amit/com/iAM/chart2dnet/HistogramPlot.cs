namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class HistogramPlot : GroupPlot
    {
        public HistogramPlot()
        {
            this.InitDefaults();
        }

        public HistogramPlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public HistogramPlot(PhysicalCoordinates transform, GroupDataset dataset, double rbarbase, ChartAttribute attrib)
        {
            this.InitDefaults();
            this.InitHistogramPlot(transform, dataset, rbarbase, attrib);
        }

        private void CalcHistogramRect(double x, double y, double width, Rectangle2D rect)
        {
            double fillBaseValue;
            double num2;
            double num5;
            double num6;
            double num7;
            if (base.barJust == 1)
            {
                num7 = width / 2.0;
            }
            else if (base.barJust == 2)
            {
                num7 = width;
            }
            else
            {
                num7 = 0.0;
            }
            if (base.barOrient == 0)
            {
                fillBaseValue = base.fillBaseValue;
                double num3 = y;
                num2 = base.chartObjScale.PhysAddY(x, -num7);
                num6 = width;
                num5 = num3 - fillBaseValue;
            }
            else
            {
                num2 = base.fillBaseValue;
                double num4 = y;
                fillBaseValue = base.chartObjScale.PhysAddX(x, -num7);
                num6 = num4 - base.fillBaseValue;
                num5 = width;
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
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    this.CalcHistogramRect(xData[i], yGroupData[0][i], yGroupData[1][i], rect);
                    base.chartObjScale.ConvertRect(rect, 0, rect, 1);
                    if (rect.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                    {
                        flag = true;
                        np.nearestPointValid = true;
                        np.nearestPoint.SetLocation(xData[i], yGroupData[0][i]);
                        np.actualPoint.SetLocation(p);
                        np.nearestPointMinDistance = 0.0;
                        np.nearestPointIndex = i + base.fastClipOffset;
                        return flag;
                    }
                }
            }
            return flag;
        }

        public override object Clone()
        {
            HistogramPlot plot = new HistogramPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(HistogramPlot source)
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
                this.DrawHistogramPlot(g2, base.thePath);
            }
        }

        private void DrawHistogramPlot(Graphics g2, GraphicsPath path)
        {
            Rectangle2D rect = new Rectangle2D();
            base.DisplayDataset = base.groupDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    base.SegmentAttributesSet(i + base.fastClipOffset);
                    this.CalcHistogramRect(xData[i], yGroupData[0][i], yGroupData[1][i], rect);
                    base.chartObjScale.WRectangle(path, rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
                    base.chartObjScale.DrawFillPath(g2, path);
                    path.Reset();
                    if (base.showDatapointValue)
                    {
                        this.DrawBarDatapointValue(g2, xData[i], yGroupData[0][i], rect);
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
            base.chartObjType = 0x21;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void InitHistogramPlot(GroupDataset dataset, double rbarbase, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            base.chartObjAttributes.Copy(attrib);
            base.fillBaseValue = rbarbase;
            base.InitSegmentAttributes(attrib, dataset.GetNumberDatapoints());
        }

        public void InitHistogramPlot(PhysicalCoordinates transform, GroupDataset dataset, double rbarbase, ChartAttribute attrib)
        {
            this.SetChartObjScale(transform);
            base.SetDataset(dataset);
            base.chartObjAttributes.Copy(attrib);
            base.fillBaseValue = rbarbase;
            base.InitSegmentAttributes(attrib, dataset.GetNumberDatapoints());
        }
    }
}

