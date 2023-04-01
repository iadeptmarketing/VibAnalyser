namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SimpleBarPlot : SimplePlot
    {
        public SimpleBarPlot()
        {
            this.InitDefaults();
        }

        public SimpleBarPlot(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public SimpleBarPlot(PhysicalCoordinates transform, SimpleDataset dataset, double barwidth, double barbase, ChartAttribute attrib, int barjust)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitBarPlot(dataset, barwidth, barbase, attrib, barjust);
        }

        private void CalcBarRect(double x, double y, Rectangle2D rect)
        {
            double fillBaseValue;
            double num2;
            double num5;
            double num6;
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
                fillBaseValue = base.fillBaseValue;
                double num3 = x;
                num2 = base.chartObjScale.PhysAddY(y, -barWidth);
                num6 = base.barWidth;
                num5 = num3 - base.fillBaseValue;
            }
            else
            {
                num2 = base.fillBaseValue;
                double num4 = y;
                fillBaseValue = base.chartObjScale.PhysAddX(x, -barWidth);
                num6 = num4 - base.fillBaseValue;
                num5 = base.barWidth;
            }
            rect.SetFrame(fillBaseValue, num2, num5, num6);
        }

        public override bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(base.chartObjScale, base.theDataset, base.coordinateSwap, testpoint, nmode, nearestpoint);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            bool flag = false;
            int numberDatapoints = 0;
            Rectangle2D rect = new Rectangle2D();
            numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray yData = base.DisplayDataset.YData;
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidData(base.chartObjScale, i))
                {
                    this.CalcBarRect(xData[i], yData[i], rect);
                    base.chartObjScale.ConvertRect(rect, 0, rect, 1);
                    if (rect.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                    {
                        flag = true;
                        np.nearestPointValid = true;
                        np.nearestPoint.SetLocation(xData[i], yData[i]);
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
            SimpleBarPlot plot = new SimpleBarPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(SimpleBarPlot source)
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
                this.DrawBarPlot(g2, base.thePath);
            }
        }

        private void DrawBarPlot(Graphics g2, GraphicsPath path)
        {
            new BarDatapointValue();
            new Point2D();
            base.DisplayDataset = base.theDataset.GetFastClipDataset("", base.chartObjScale, base.fastClipMode, ref this.fastClipOffset);
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray yData = base.DisplayDataset.YData;
            Rectangle2D rect = new Rectangle2D();
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidData(base.chartObjScale, i))
                {
                    this.CalcBarRect(xData[i], yData[i], rect);
                    base.SegmentAttributesSet(i + base.fastClipOffset);
                    base.chartObjScale.WRectangle(path, rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
                    base.chartObjScale.DrawFillPath(g2, path);
                    path.Reset();
                    if (base.showDatapointValue)
                    {
                        this.DrawBarDatapointValue(g2, xData[i], yData[i], rect);
                    }
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public void InitBarPlot(SimpleDataset dataset, double barwidth, double barbase, ChartAttribute attrib, int barjust)
        {
            base.SetDataset(dataset);
            base.barWidth = barwidth;
            base.fillBaseValue = barbase;
            base.barJust = barjust;
            base.chartObjAttributes.Copy(attrib);
        }

        private void InitDefaults()
        {
            base.chartObjType = 3;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }
    }
}

