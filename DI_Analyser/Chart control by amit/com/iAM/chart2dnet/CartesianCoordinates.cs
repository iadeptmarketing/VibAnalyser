namespace com.iAM.chart2dnet
{
    using System;

    public class CartesianCoordinates : PhysicalCoordinates
    {
        public CartesianCoordinates()
        {
            this.InitDefaults();
        }

        public CartesianCoordinates(int xscale, int yscale)
        {
            this.SetCartesianScaleTransforms(xscale, yscale);
        }

        public CartesianCoordinates(double rX1, double rY1, double rX2, double rY2)
        {
            this.SetCoordinateBounds(rX1, rY1, rX2, rY2);
        }

        public override void AutoScale(ChartDataset[] datasets)
        {
            this.CalcAutoScale(datasets, 2, 2);
        }

        public override void AutoScale(ChartDataset dataset)
        {
            this.CalcAutoScale(dataset, 2, 2);
        }

        public override void AutoScale(int nroundmodeX, int nroundmodeY)
        {
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            com.iAM.chart2dnet.AutoScale scale2 = base.yScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(this, 0, nroundmodeX);
            compatibleAutoScale.CalcChartAutoScaleTransform();
            scale2.SetChartAutoScale(this, 1, nroundmodeY);
            scale2.CalcChartAutoScaleTransform();
            base.SetPhysScale(compatibleAutoScale.GetFinalMin(), scale2.GetFinalMin(), compatibleAutoScale.GetFinalMax(), scale2.GetFinalMax());
            compatibleAutoScale = null;
            scale2 = null;
        }

        public override void AutoScale(ChartDataset[] datasets, int nroundmodeX, int nroundmodeY)
        {
            this.CalcAutoScale(datasets, nroundmodeX, nroundmodeY);
        }

        public override void AutoScale(ChartDataset dataset, int nroundmodeX, int nroundmodeY)
        {
            this.CalcAutoScale(dataset, nroundmodeX, nroundmodeY);
        }

        private void CalcAutoScale(ChartDataset[] datasets, int nroundmodeX, int nroundmodeY)
        {
            int length = datasets.Length;
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            com.iAM.chart2dnet.AutoScale scale2 = base.yScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(datasets, 0, nroundmodeX);
            compatibleAutoScale.CalcChartAutoScaleDatasets();
            scale2.SetChartAutoScale(datasets, 1, nroundmodeY);
            scale2.CalcChartAutoScaleDatasets();
            base.SetPhysScale(compatibleAutoScale.GetFinalMin(), scale2.GetFinalMin(), compatibleAutoScale.GetFinalMax(), scale2.GetFinalMax());
            compatibleAutoScale = null;
            scale2 = null;
        }

        private void CalcAutoScale(ChartDataset dataset, int nroundmodeX, int nroundmodeY)
        {
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            com.iAM.chart2dnet.AutoScale scale2 = base.yScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(dataset, 0, nroundmodeX);
            compatibleAutoScale.CalcChartAutoScaleDataset();
            scale2.SetChartAutoScale(dataset, 1, nroundmodeY);
            scale2.CalcChartAutoScaleDataset();
            base.SetPhysScale(compatibleAutoScale.GetFinalMin(), scale2.GetFinalMin(), compatibleAutoScale.GetFinalMax(), scale2.GetFinalMax());
            compatibleAutoScale = null;
            scale2 = null;
        }

        public override bool CheckValidPoint(double x, double y)
        {
            return ChartSupport.BGoodValue(x, y);
        }

        public override object Clone()
        {
            CartesianCoordinates coordinates = new CartesianCoordinates();
            coordinates.Copy(this);
            return coordinates;
        }

        public void Copy(CartesianCoordinates source)
        {
            if (source != null)
            {
                base.Copy((PhysicalCoordinates) source);
                base.xScale = (Scale) source.xScale.Clone();
                base.yScale = (Scale) source.yScale.Clone();
            }
        }

        public override void Copy(object source)
        {
            if (source != null)
            {
                this.Copy((CartesianCoordinates) source);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public override Axis GetCompatibleAxis(int axis)
        {
            Axis compatibleAxis;
            if (axis == 0)
            {
                compatibleAxis = base.xScale.GetCompatibleAxis();
            }
            else
            {
                compatibleAxis = base.yScale.GetCompatibleAxis();
            }
            compatibleAxis.SetChartObjScale(this);
            compatibleAxis.SetAxisType(axis);
            return compatibleAxis;
        }

        private void InitDefaults()
        {
            this.SetCartesianScaleTransforms(0, 0);
            base.chartObjType = 0x4a0;
        }

        public void SetCartesianScaleTransforms(int xscale, int yscale)
        {
            this.SetCartesianXScaleTransform(xscale);
            this.SetCartesianYScaleTransform(yscale);
        }

        public void SetCartesianXScaleTransform(int xscale)
        {
            if (xscale == 1)
            {
                base.xScale = new LogScale(1.0, 100.0);
            }
            else
            {
                base.xScale = new LinearScale(0.0, 100.0);
            }
            base.SetXScale(base.xScale);
        }

        public void SetCartesianYScaleTransform(int yscale)
        {
            if (yscale == 1)
            {
                base.yScale = new LogScale(1.0, 100.0);
            }
            else
            {
                base.yScale = new LinearScale(0.0, 100.0);
            }
            base.SetYScale(base.yScale);
        }
    }
}

