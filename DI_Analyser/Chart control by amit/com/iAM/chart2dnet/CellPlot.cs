namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class CellPlot : GroupPlot
    {
        private ChartImage plotImage;

        public CellPlot()
        {
            this.plotImage = new ChartImage();
            this.InitDefaults();
        }

        public CellPlot(PhysicalCoordinates transform)
        {
            this.plotImage = new ChartImage();
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public CellPlot(PhysicalCoordinates transform, GroupDataset dataset, ChartAttribute attrib)
        {
            this.plotImage = new ChartImage();
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitCellPlot(dataset, attrib);
        }

        private void CalcCellRect(double x, double y, double width, double height, Rectangle2D rect)
        {
            rect.SetFrame(x, y, width, height);
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
                    this.CalcCellRect(xData[i], yGroupData[0][i], yGroupData[1][i], yGroupData[2][i], rect);
                    if ((rect != null) && rect.Contains((double) ((int) p.GetX()), (double) ((int) p.GetY())))
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
            CellPlot plot = new CellPlot();
            plot.Copy(this);
            return plot;
        }

        public void Copy(CellPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.plotImage != null)
                {
                    this.plotImage = (ChartImage) source.plotImage.Clone();
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.ErrorCheck(0) == 0) && (this.GetChartObjEnable() == 1))
            {
                this.PrePlot(g2);
                this.DrawCellPlot(g2, base.thePath);
            }
        }

        private void DrawCellPlot(Graphics g2, GraphicsPath path)
        {
            Rectangle2D rect = new Rectangle2D();
            base.DisplayDataset = base.groupDataset;
            DoubleArray xData = base.DisplayDataset.XData;
            DoubleArray2D yGroupData = base.DisplayDataset.YGroupData;
            int numberDatapoints = base.DisplayDataset.GetNumberDatapoints();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            if (this.plotImage.GetImageObject() != null)
            {
                this.plotImage.SetSizeMode(2);
                this.plotImage.SetChartObjScale(base.chartObjScale);
            }
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (base.DisplayDataset.CheckValidGroupData(base.chartObjScale, i))
                {
                    base.SegmentAttributesSet(i);
                    this.CalcCellRect(xData[i], yGroupData[0][i], yGroupData[1][i], yGroupData[2][i], rect);
                    if (this.plotImage.GetImageObject() == null)
                    {
                        base.chartObjScale.WRectangle(path, rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
                        base.chartObjScale.DrawFillPath(g2, path);
                        path.Reset();
                    }
                    else
                    {
                        this.plotImage.SetImageRectangle(rect, 1);
                        this.plotImage.DrawImage(g2);
                    }
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

        public Image GetPlotImage()
        {
            return this.plotImage.GetImageObject();
        }

        public void InitCellPlot(GroupDataset dataset, ChartAttribute attrib)
        {
            base.SetDataset(dataset);
            base.chartObjAttributes.Copy(attrib);
            base.InitSegmentAttributes(attrib, dataset);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x20;
            base.chartObjClipping = 2;
            base.moveableType = 2;
        }

        public void SetPlotImage(Image imageobj)
        {
            this.plotImage.SetImageObject(imageobj);
        }

        public Image BoxFillAttributes
        {
            get
            {
                return this.plotImage.GetImageObject();
            }
            set
            {
                this.plotImage.SetImageObject(value);
            }
        }
    }
}

