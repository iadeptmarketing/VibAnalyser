namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ArrowPlot : GroupPlot
    {
        private Arrow baseArrow;

        public ArrowPlot()
        {
            this.baseArrow = new Arrow();
            this.InitDefaults();
        }

        public ArrowPlot(PhysicalCoordinates transform)
        {
            this.baseArrow = new Arrow();
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public ArrowPlot(PhysicalCoordinates transform, GroupDataset dataset, Arrow basearrow, ChartAttribute attrib)
        {
            this.baseArrow = new Arrow();
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitArrowPlot(dataset, basearrow, attrib);
        }

        private ChartShape CalcArrowShape(Arrow arrow, double x, double y, double size, double angle)
        {
            ChartShape shape = new ChartShape(base.chartObjScale);
            arrow.SetArrowScaleFactor(size);
            shape.SetShapeObj(arrow.GetArrowShape());
            shape.SetLocation(x, y);
            shape.SetPositionType(1);
            shape.SetShapeCoordsType(0);
            shape.SetShapeRotation(angle);
            return shape;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            bool flag = false;
            Arrow arrow = new Arrow();
            ChartShape shape = new ChartShape();
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            Point2D p = base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    double x = xData[i];
                    double y = yGroupData[0][i];
                    double size = yGroupData[1][i];
                    double angle = yGroupData[2][i];
                    Rectangle2D rectangled = new Rectangle2D(this.CalcArrowShape(arrow, x, y, size, angle).GetBoundingBox());
                    if ((rectangled != null) && rectangled.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                    {
                        flag = true;
                        np.nearestPointValid = true;
                        np.nearestPoint.SetLocation(p);
                        np.nearestPointMinDistance = 0.0;
                        return flag;
                    }
                    rectangled = null;
                }
            }
            return flag;
        }

        public override object Clone()
        {
            ArrowPlot plot = new ArrowPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(ArrowPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.baseArrow = (Arrow) source.baseArrow.Clone();
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawArrowPlot(g2, base.thePath);
            }
        }

        private void DrawArrowPlot(Graphics g2, GraphicsPath path)
        {
            double size = 1.0;
            double angle = 0.0;
            Arrow arrow = (Arrow) this.baseArrow.Clone();
            ChartShape shape = new ChartShape(base.chartObjScale);
            base.DisplayDataset = base.groupDataset;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    double x = xData[i];
                    double y = yGroupData[0][i];
                    size = yGroupData[1][i];
                    angle = yGroupData[2][i];
                    base.SegmentAttributesSet(i);
                    shape = this.CalcArrowShape(arrow, x, y, size, angle);
                    shape.SetChartObjAttributes(base.chartObjScale.GetCurrentAttributes());
                    if (this.GetChartObjEnable() == 1)
                    {
                        shape.DrawShape(g2, path);
                    }
                    path.Reset();
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.baseArrow == null)
                {
                    nerror = 700;
                }
                else
                {
                    nerror = this.baseArrow.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public Arrow GetBaseArrow()
        {
            return (Arrow) this.baseArrow.Clone();
        }

        public void InitArrowPlot(GroupDataset dataset, Arrow basearrow, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            if (this.baseArrow != null)
            {
                this.baseArrow = (Arrow) basearrow.Clone();
            }
            else
            {
                this.baseArrow = new Arrow();
            }
            base.chartObjAttributes.Copy(attrib);
        }

        private void InitDefaults()
        {
            base.chartObjType = 30;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetBaseArrow(Arrow basearrow)
        {
            this.baseArrow = (Arrow) basearrow.Clone();
        }
    }
}

