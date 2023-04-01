namespace com.quinncurtis.chart2dnet
{
    using System;

    public class LinearAutoScale : AutoScale
    {
        internal double tickInterval;

        public LinearAutoScale()
        {
            this.tickInterval = 1.0;
            this.InitDefaults();
        }

        public LinearAutoScale(PhysicalCoordinates transform)
        {
            this.tickInterval = 1.0;
            this.InitDefaults();
            base.theChartCoordinates = transform;
            this.CalcChartAutoScaleTransform();
        }

        public LinearAutoScale(ChartDataset dataset, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.InitDefaults();
            base.SetChartAutoScale(dataset, naxis, nmode);
            this.CalcChartAutoScaleDataset();
        }

        public LinearAutoScale(ChartDataset[] datasets, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.InitDefaults();
            base.SetChartAutoScale(datasets, naxis, nmode);
            this.CalcChartAutoScaleDatasets();
        }

        public LinearAutoScale(PhysicalCoordinates transform, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.InitDefaults();
            base.SetChartAutoScale(transform, naxis, nmode);
            this.CalcChartAutoScaleTransform();
        }

        public LinearAutoScale(double rmin, double rmax, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.InitDefaults();
            base.SetChartAutoScale(rmin, rmax, naxis, nmode);
            this.CalcChartAutoScaleInitialValues();
        }

        private double CalcLabelStart(double rmin, double rmax, double rtickinterval, int nthtick)
        {
            double num = rmin;
            if (((rmin <= 0.0) && (rmax >= 0.0)) || ((rmax <= 0.0) && (rmin >= 0.0)))
            {
                return 0.0;
            }
            int num2 = (int) rmin;
            if (((int) (rtickinterval * nthtick)) == 0)
            {
                return (double) num2;
            }
            if ((((int) Math.Abs(rmin)) % ((int) (rtickinterval * nthtick))) == 0)
            {
                return (double) num2;
            }
            return ((num2 - (((int) Math.Abs(rmin)) % ((int) (rtickinterval * nthtick)))) + (rtickinterval * nthtick));
        }

        public override void CalcRoundAxisValues(double raxmin, double raxmax, int nroundmode)
        {
            int num;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            bool flag = false;
            base.minValue = base.initialMin = raxmin;
            base.maxValue = base.initialMax = raxmax;
            this.AdjustForZeroEndpoints(base.initialMin, base.initialMax);
            if (base.minValue > base.maxValue)
            {
                double minValue = base.minValue;
                base.minValue = base.maxValue;
                base.maxValue = minValue;
                flag = true;
            }
            double r = base.maxValue - base.minValue;
            base.maxValue += base.maxRangeAdjust;
            base.minValue -= base.minRangeAdjust;
            if (r < 10.0)
            {
                num = ((int) -ChartSupport.Log10Abs(r)) + 2;
                base.minValue = base.ShiftDecimalLeft(base.minValue, num);
                base.maxValue = base.ShiftDecimalLeft(base.maxValue, num);
                num5 = base.CalcOffset(base.minValue, 2);
                double rmin = base.minValue - num5;
                double rmax = base.maxValue - num5;
                double rrange = rmax - rmin;
                this.tickInterval = this.CalcTickInterval(rrange);
                base.axisMinorTicksPerMajor = base.CalcNthTickMajor(rrange);
                base.labelsOrigin = this.CalcLabelStart(base.minValue, base.maxValue, this.tickInterval, base.axisMinorTicksPerMajor);
                base.labelsOrigin = base.ShiftDecimalRight(base.labelsOrigin, num);
                base.finalMin = base.CalcFinalMin(rmin, this.tickInterval, base.axisMinorTicksPerMajor, nroundmode);
                base.finalMin = base.ShiftDecimalRight(base.finalMin + num5, num);
                base.finalMax = base.CalcFinalMax(rmax, this.tickInterval, base.axisMinorTicksPerMajor, nroundmode);
                base.finalMax = base.ShiftDecimalRight(base.finalMax + num5, num);
                this.tickInterval = base.ShiftDecimalRight(this.tickInterval, num);
            }
            else if (r < 100.0)
            {
                double num10 = base.MaskDigits(base.minValue, 2);
                double num11 = base.MaskDigits(base.maxValue, 2);
                num3 = base.CalcOffset(base.minValue, 2);
                num4 = base.CalcOffset(base.maxValue, 2);
                if (num11 <= num10)
                {
                    num11 += 100.0;
                    num4 -= 100.0;
                }
                double num12 = num11 - num10;
                this.tickInterval = this.CalcTickInterval(num12);
                base.axisMinorTicksPerMajor = base.CalcNthTickMajor(num12);
                base.labelsOrigin = this.CalcLabelStart(base.minValue, base.maxValue, this.tickInterval, base.axisMinorTicksPerMajor);
                base.labelsOrigin = base.labelsOrigin;
                base.finalMin = num3 + base.CalcFinalMin(num10, this.tickInterval, base.axisMinorTicksPerMajor, nroundmode);
                base.finalMax = num4 + base.CalcFinalMax(num11, this.tickInterval, base.axisMinorTicksPerMajor, nroundmode);
            }
            else
            {
                num = ((int) ChartSupport.Log10Abs(r)) - 1;
                base.minValue = base.ShiftDecimalRight(base.minValue, num);
                base.maxValue = base.ShiftDecimalRight(base.maxValue, num);
                double num13 = base.MaskDigits(base.minValue, 2);
                double num14 = base.MaskDigits(base.maxValue, 2);
                num3 = base.CalcOffset(base.minValue, 2);
                num4 = base.CalcOffset(base.maxValue, 2);
                if (num14 <= num13)
                {
                    num14 += 100.0;
                    num4 -= 100.0;
                }
                double num15 = num14 - num13;
                this.tickInterval = this.CalcTickInterval(num15);
                base.axisMinorTicksPerMajor = base.CalcNthTickMajor(num15);
                base.labelsOrigin = this.CalcLabelStart(base.minValue, base.maxValue, this.tickInterval, base.axisMinorTicksPerMajor);
                base.labelsOrigin = base.ShiftDecimalLeft(base.labelsOrigin, num);
                base.finalMin = num3 + base.CalcFinalMin(num13, this.tickInterval, base.axisMinorTicksPerMajor, nroundmode);
                base.finalMin = base.ShiftDecimalLeft(base.finalMin, num);
                base.finalMax = num4 + base.CalcFinalMax(num14, this.tickInterval, base.axisMinorTicksPerMajor, nroundmode);
                base.finalMax = base.ShiftDecimalLeft(base.finalMax, num);
                this.tickInterval = base.ShiftDecimalLeft(this.tickInterval, num);
                if (flag)
                {
                    double finalMin = base.finalMin;
                    base.finalMin = base.finalMax;
                    base.finalMax = finalMin;
                }
            }
        }

        public override object Clone()
        {
            LinearAutoScale scale = new LinearAutoScale();
            scale.Copy(this);
            return scale;
        }

        public void Copy(LinearAutoScale source)
        {
            base.Copy(source);
            this.tickInterval = source.tickInterval;
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public double GetTickInterval()
        {
            return this.tickInterval;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0xc9;
        }
    }
}

