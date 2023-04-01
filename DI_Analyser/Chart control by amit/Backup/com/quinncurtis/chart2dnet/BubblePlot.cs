namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class BubblePlot : GroupPlot
    {
        internal int bubbleSizeType;

        public BubblePlot()
        {
            this.bubbleSizeType = 0;
            this.InitDefaults();
        }

        public BubblePlot(PhysicalCoordinates transform)
        {
            this.bubbleSizeType = 0;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public BubblePlot(PhysicalCoordinates transform, GroupDataset dataset, int bubblesizetype, ChartAttribute attrib)
        {
            this.bubbleSizeType = 0;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitBubblePlot(dataset, bubblesizetype, attrib);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            double radius = 1.0;
            Arc2D arcd = null;
            bool flag = false;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    if (this.bubbleSizeType == 0)
                    {
                        radius = yGroupData[1][i];
                    }
                    else
                    {
                        radius = Math.Sqrt(yGroupData[1][i] / 3.1415926535897931);
                    }
                    arcd = base.chartObjScale.GetWCircle(xData[i], yGroupData[0][i], radius);
                    if ((arcd != null) && arcd.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                    {
                        flag = true;
                        np.nearestPointValid = true;
                        np.nearestPoint.SetLocation(p);
                        np.nearestPointMinDistance = 0.0;
                        np.nearestPointIndex = i;
                        return flag;
                    }
                }
            }
            return flag;
        }

        public override object Clone()
        {
            BubblePlot plot = new BubblePlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(BubblePlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.bubbleSizeType = source.bubbleSizeType;
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawBubblePlot(g2, base.thePath);
            }
        }

        private void DrawBubblePlot(Graphics g2, GraphicsPath path)
        {
            base.DisplayDataset = base.groupDataset;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    double num3;
                    if (this.bubbleSizeType == 0)
                    {
                        num3 = Math.Abs(yGroupData[1][i]);
                    }
                    else
                    {
                        num3 = Math.Sqrt(Math.Abs(yGroupData[1][i]) / 3.1415926535897931);
                    }
                    double x = xData[i];
                    double y = yGroupData[0][i];
                    base.SegmentAttributesSet(i + base.fastClipOffset);
                    base.chartObjScale.WCircle(path, x, y, num3);
                    base.chartObjScale.DrawFillPath(g2, path);
                    path.Reset();
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public int GetBubbleSizeType()
        {
            return this.bubbleSizeType;
        }

        public void InitBubblePlot(GroupDataset dataset, int bubblesizetype, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            this.bubbleSizeType = bubblesizetype;
            base.chartObjAttributes.Copy(attrib);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x1d;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetBubbleSizeType(int bubblesizetype)
        {
            this.bubbleSizeType = bubblesizetype;
        }

        public int BubbleSizeType
        {
            get
            {
                return this.bubbleSizeType;
            }
            set
            {
                this.bubbleSizeType = value;
            }
        }
    }
}

