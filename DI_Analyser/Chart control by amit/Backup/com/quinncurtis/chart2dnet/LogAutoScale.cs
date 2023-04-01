namespace com.quinncurtis.chart2dnet
{
    using System;

    public class LogAutoScale : AutoScale
    {
        public LogAutoScale()
        {
            this.InitDefaults();
        }

        public LogAutoScale(PhysicalCoordinates transform)
        {
            this.InitDefaults();
            base.theChartCoordinates = transform;
            this.CalcChartAutoScaleTransform();
        }

        public LogAutoScale(ChartDataset dataset, int naxis, int nmode)
        {
            this.InitDefaults();
            base.SetChartAutoScale(dataset, naxis, nmode);
            this.CalcChartAutoScaleDataset();
        }

        public LogAutoScale(PhysicalCoordinates transform, int naxis, int nmode)
        {
            this.InitDefaults();
            base.SetChartAutoScale(transform, naxis, nmode);
            this.CalcChartAutoScaleTransform();
        }

        public LogAutoScale(ChartDataset[] datasets, int naxis, int nmode)
        {
            this.InitDefaults();
            base.SetChartAutoScale(datasets, naxis, nmode);
            this.CalcChartAutoScaleDatasets();
        }

        public LogAutoScale(double rmin, double rmax, int naxis, int nmode)
        {
            this.InitDefaults();
            base.SetChartAutoScale(rmin, rmax, naxis, nmode);
            this.CalcChartAutoScaleInitialValues();
        }

        private double CalcLogMax(double rmin, double rmax, int nroundmode)
        {
            int num = 0;
            double a = ChartSupport.Log10Ex(rmax);
            if (a < 0.0)
            {
                num = (int) Math.Ceiling((double) (a * 1.0000001));
            }
            else
            {
                num = (int) Math.Ceiling(a);
            }
            switch (nroundmode)
            {
                case 0:
                    return rmax;

                case 1:
                    rmax /= Math.Pow(10.0, (double) (num - 1));
                    rmax = Math.Ceiling(rmax);
                    rmax *= Math.Pow(10.0, (double) (num - 1));
                    return rmax;

                case 2:
                    rmax = Math.Pow(10.0, (double) num);
                    return rmax;
            }
            return rmax;
        }

        private double CalcLogMin(double rmin, double rmax, int nroundmode)
        {
            int num = 0;
            if (rmin <= 0.0)
            {
                rmin = rmax / 999.9;
            }
            double d = ChartSupport.Log10Ex(rmin);
            if (d < 0.0)
            {
                num = (int) Math.Floor((double) (d * 0.9999999));
            }
            else
            {
                num = (int) Math.Floor(d);
            }
            switch (nroundmode)
            {
                case 0:
                    return rmin;

                case 1:
                    rmin /= Math.Pow(10.0, (double) num);
                    rmin = Math.Floor(rmin);
                    rmin *= Math.Pow(10.0, (double) num);
                    return rmin;

                case 2:
                    rmin = Math.Pow(10.0, (double) num);
                    return rmin;
            }
            return rmin;
        }

        public override void CalcRoundAxisValues(double raxmin, double raxmax, int nroundmode)
        {
            base.minValue = base.initialMin = raxmin;
            base.maxValue = base.initialMax = raxmax;
            this.AdjustForZeroEndpoints(base.initialMin, base.initialMax);
            if (base.minValue > base.maxValue)
            {
                double num = base.minValue;
                base.minValue = base.maxValue;
                base.maxValue = num;
            }
            double maxValue = base.maxValue;
            double minValue = base.minValue;
            base.maxValue += base.maxRangeAdjust;
            base.minValue -= base.minRangeAdjust;
            base.finalMin = this.CalcLogMin(base.minValue, base.maxValue, nroundmode);
            base.finalMax = this.CalcLogMax(base.minValue, base.maxValue, nroundmode);
            base.labelsOrigin = base.finalMin;
        }

        public override object Clone()
        {
            LogAutoScale scale = new LogAutoScale();
            scale.Copy(this);
            return scale;
        }

        public void Copy(LogAutoScale source)
        {
            base.Copy(source);
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0xca;
        }
    }
}

