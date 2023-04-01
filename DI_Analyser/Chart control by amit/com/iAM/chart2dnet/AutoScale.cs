namespace com.iAM.chart2dnet
{
    using System;

    public abstract class AutoScale : ChartObj
    {
        internal int axisLabelsDecimalPos = 0;
        internal int axisMinorTicksPerMajor = 5;
        internal int axisType = 0;
        internal double finalMax = 1.0;
        internal double finalMin = 0.0;
        internal double initialMax = 1.0;
        internal double initialMin = 0.0;
        internal double labelsOrigin = 0.0;
        internal int majorNthTick = 1;
        internal double maxRangeAdjust = 0.0;
        internal double maxValue = 1.0;
        internal double minRangeAdjust = 0.0;
        internal double minValue = 0.0;
        internal int numDatasets = 0;
        internal int roundMode = 1;
        internal PhysicalCoordinates theChartCoordinates = null;
        internal ChartDataset theDataset = null;
        internal ChartDataset[] theDatasetsArray = null;
        internal double zeroEndpointTestValue = 1E-30;

        public AutoScale()
        {
            this.InitDefaults();
        }

        public virtual void AdjustForZeroEndpoints(double rmin, double rmax)
        {
            if (Math.Abs((double) (rmin - rmax)) < this.zeroEndpointTestValue)
            {
                if (Math.Abs(rmax) < this.zeroEndpointTestValue)
                {
                    rmax = 10.0;
                    rmin = 0.0;
                }
                else if (rmin < 0.0)
                {
                    rmax = 0.0;
                    rmin = 2.0 * rmin;
                }
                else
                {
                    rmin = 0.0;
                    rmax = 2.0 * rmax;
                }
            }
            this.minValue = rmin;
            this.maxValue = rmax;
        }

        public virtual void CalcChartAutoScaleDataset()
        {
            if (this.theDataset == null)
            {
                this.ErrorCheck(0x385);
            }
            this.CalcDatasetRange();
            this.CalcRoundAxisValues(this.initialMin, this.initialMax, this.roundMode);
        }

        public virtual void CalcChartAutoScaleDatasets()
        {
            if (this.theDatasetsArray == null)
            {
                this.ErrorCheck(0x386);
            }
            this.CalcDatasetsRange();
            this.CalcRoundAxisValues(this.initialMin, this.initialMax, this.roundMode);
        }

        public virtual void CalcChartAutoScaleInitialValues()
        {
            this.CalcRoundAxisValues(this.initialMin, this.initialMax, this.roundMode);
        }

        public virtual void CalcChartAutoScaleTransform()
        {
            double raxmin = 0.0;
            double raxmax = 10.0;
            if (this.theChartCoordinates == null)
            {
                this.ErrorCheck(0x387);
            }
            raxmin = this.theChartCoordinates.GetStart(this.axisType);
            raxmax = this.theChartCoordinates.GetStop(this.axisType);
            this.CalcRoundAxisValues(raxmin, raxmax, this.roundMode);
        }

        public void CalcDatasetRange()
        {
            double datasetMin = 0.0;
            double datasetMax = 10.0;
            if (this.theDataset == null)
            {
                this.ErrorCheck(0x385);
            }
            datasetMin = this.theDataset.GetDatasetMin(this.axisType);
            datasetMax = this.theDataset.GetDatasetMax(this.axisType);
            if ((this.theDataset.GetStackMode() == 1) && (this.axisType == 1))
            {
                datasetMin = this.theDataset.GetGroupDatasetSumMin(this.axisType);
                datasetMax = this.theDataset.GetGroupDatasetSumMax(this.axisType);
            }
            this.initialMin = datasetMin;
            this.initialMax = datasetMax;
        }

        public void CalcDatasetsRange()
        {
            double datasetMin = 0.0;
            double datasetMax = 10.0;
            ChartDataset dataset = null;
            if (this.theDatasetsArray == null)
            {
                this.ErrorCheck(0x386);
            }
            this.initialMin = ChartSupport.GetDatasetsMin(this.theDatasetsArray, this.numDatasets, this.axisType);
            this.initialMax = ChartSupport.GetDatasetsMax(this.theDatasetsArray, this.numDatasets, this.axisType);
            for (int i = 0; i < this.numDatasets; i++)
            {
                dataset = this.theDatasetsArray[i];
                datasetMin = dataset.GetDatasetMin(this.axisType);
                datasetMax = dataset.GetDatasetMax(this.axisType);
                if ((dataset.GetStackMode() == 1) && (this.axisType == 1))
                {
                    datasetMin = dataset.GetGroupDatasetSumMin(this.axisType);
                    datasetMax = dataset.GetGroupDatasetSumMax(this.axisType);
                }
                if (datasetMin < this.initialMin)
                {
                    this.initialMin = datasetMin;
                }
                if (datasetMax > this.initialMax)
                {
                    this.initialMax = datasetMax;
                }
            }
        }

        public double CalcFinalMax(double rmax, double rtickinterval, int nthtick, int nroundmode)
        {
            double num = rmax;
            int num2 = (int) (rtickinterval * nthtick);
            if (((num2 == 0) || (rtickinterval == 0.0)) || (rtickinterval < 1.0))
            {
                return num;
            }
            switch (nroundmode)
            {
                case 0:
                    return rmax;

                case 1:
                    rmax += rtickinterval;
                    return ((((int) rmax) / ((int) rtickinterval)) * rtickinterval);
            }
            rmax += num2;
            return (num2 * (((int) rmax) / num2));
        }

        public double CalcFinalMin(double rmin, double rtickinterval, int nthtick, int nroundmode)
        {
            double num = rmin;
            int num2 = (int) (rtickinterval * nthtick);
            if (((num2 == 0) || (rtickinterval == 0.0)) || (rtickinterval < 1.0))
            {
                return num;
            }
            switch (nroundmode)
            {
                case 0:
                    return rmin;

                case 1:
                    if (rmin < 0.0)
                    {
                        rmin -= rtickinterval;
                    }
                    return (double) ((((int) rmin) / ((int) rtickinterval)) * ((int) rtickinterval));
            }
            if (rmin < 0.0)
            {
                rmin -= num2;
            }
            return (num2 * (((int) rmin) / num2));
        }

        public int CalcNthTickMajor(double rrange)
        {
            int num = 5;
            if (rrange > 200.0)
            {
                return 5;
            }
            if (rrange > 100.0)
            {
                return 5;
            }
            if (rrange > 50.0)
            {
                return 4;
            }
            if (rrange > 20.0)
            {
                return 5;
            }
            if (rrange > 10.0)
            {
                return 5;
            }
            if (rrange > 5.0)
            {
                return 4;
            }
            if (rrange > 2.0)
            {
                return 5;
            }
            if (rrange >= 0.0)
            {
                num = 5;
            }
            return num;
        }

        public double CalcOffset(double r, int n)
        {
            return (((long) this.ShiftDecimalRight(r, n)) * this.ShiftDecimalLeft(1.0, n));
        }

        public abstract void CalcRoundAxisValues(double raxmin, double raxmax, int nroundmode);
        public virtual double CalcTickInterval(double rrange)
        {
            double num = 1.0;
            if (rrange > 200.0)
            {
                return 20.0;
            }
            if (rrange > 100.0)
            {
                return 10.0;
            }
            if (rrange > 50.0)
            {
                return 5.0;
            }
            if (rrange > 20.0)
            {
                return 2.0;
            }
            if (rrange > 10.0)
            {
                return 1.0;
            }
            if (rrange > 5.0)
            {
                return 0.5;
            }
            if (rrange > 2.0)
            {
                return 0.2;
            }
            if (rrange >= 0.0)
            {
                num = 0.1;
            }
            return num;
        }

        public void Copy(AutoScale source)
        {
            this.finalMin = source.finalMin;
            this.finalMax = source.finalMax;
            this.labelsOrigin = source.labelsOrigin;
            this.initialMin = source.initialMin;
            this.initialMax = source.initialMax;
            this.minValue = source.minValue;
            this.maxValue = source.maxValue;
            this.axisType = source.axisType;
            this.roundMode = source.roundMode;
            this.axisMinorTicksPerMajor = source.axisMinorTicksPerMajor;
            this.axisLabelsDecimalPos = source.axisLabelsDecimalPos;
            this.theDataset = source.theDataset;
            this.numDatasets = source.numDatasets;
            for (int i = 0; i < this.numDatasets; i++)
            {
                this.theDatasetsArray[i] = source.theDatasetsArray[i];
            }
            this.theChartCoordinates = source.theChartCoordinates;
            this.minRangeAdjust = source.minRangeAdjust;
            this.maxRangeAdjust = source.maxRangeAdjust;
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public int GetAxisLabelsDecimalPos()
        {
            return this.axisLabelsDecimalPos;
        }

        public int GetAxisMinorTicksPerMajor()
        {
            return this.axisMinorTicksPerMajor;
        }

        public double GetFinalMax()
        {
            return this.finalMax;
        }

        public double GetFinalMin()
        {
            return this.finalMin;
        }

        public double GetInitialMax()
        {
            return this.initialMax;
        }

        public double GetInitialMin()
        {
            return this.initialMin;
        }

        public double GetLabelsOrigin()
        {
            return this.labelsOrigin;
        }

        public double GetMaxRangeAdjust()
        {
            return this.maxRangeAdjust;
        }

        public double GetMinRangeAdjust()
        {
            return this.minRangeAdjust;
        }

        private void InitDefaults()
        {
            base.chartObjType = 200;
        }

        public double MaskDigits(double r, int n)
        {
            double num = this.CalcOffset(r, n);
            return (r - num);
        }

        public void SetChartAutoScale(ChartDataset dataset, int naxis, int nmode)
        {
            this.theDataset = dataset;
            this.axisType = naxis;
            this.roundMode = nmode;
        }

        public void SetChartAutoScale(PhysicalCoordinates transform, int naxis, int nmode)
        {
            this.axisType = naxis;
            this.roundMode = nmode;
            this.theChartCoordinates = transform;
        }

        public void SetChartAutoScale(ChartDataset[] datasets, int naxis, int nmode)
        {
            this.axisType = naxis;
            this.roundMode = nmode;
            this.numDatasets = datasets.Length;
            this.theDatasetsArray = datasets;
        }

        public void SetChartAutoScale(double rmin, double rmax, int naxis, int nmode)
        {
            this.axisType = naxis;
            this.roundMode = nmode;
            this.initialMin = rmin;
            this.initialMax = rmax;
        }

        public void SetMaxRangeAdjust(double r)
        {
            this.maxRangeAdjust = r;
        }

        public void SetMinRangeAdjust(double r)
        {
            this.minRangeAdjust = r;
        }

        public double ShiftDecimalLeft(double r, int n)
        {
            double num = Math.Pow(10.0, (double) n);
            return (r * num);
        }

        public double ShiftDecimalRight(double r, int n)
        {
            double num = Math.Pow(10.0, (double) n);
            return (r / num);
        }

        public int AxisLabelsDecimalPos
        {
            get
            {
                return this.axisLabelsDecimalPos;
            }
            set
            {
                this.axisLabelsDecimalPos = value;
            }
        }

        public int AxisMinorTicksPerMajor
        {
            get
            {
                return this.axisMinorTicksPerMajor;
            }
            set
            {
                this.axisMinorTicksPerMajor = value;
            }
        }

        public int AxisType
        {
            get
            {
                return this.axisType;
            }
            set
            {
                this.axisType = value;
            }
        }

        public double FinalMax
        {
            get
            {
                return this.finalMax;
            }
            set
            {
                this.finalMax = value;
            }
        }

        public double FinalMin
        {
            get
            {
                return this.finalMin;
            }
            set
            {
                this.finalMin = value;
            }
        }

        public double InitialMax
        {
            get
            {
                return this.initialMax;
            }
            set
            {
                this.initialMax = value;
            }
        }

        public double InitialMin
        {
            get
            {
                return this.initialMin;
            }
            set
            {
                this.initialMin = value;
            }
        }

        public double LabelsOrigin
        {
            get
            {
                return this.labelsOrigin;
            }
            set
            {
                this.labelsOrigin = value;
            }
        }

        public int MajorNthTick
        {
            get
            {
                return this.majorNthTick;
            }
            set
            {
                this.majorNthTick = value;
            }
        }

        public double MaxRangeAdjust
        {
            get
            {
                return this.maxRangeAdjust;
            }
            set
            {
                this.maxRangeAdjust = value;
            }
        }

        public double MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
            }
        }

        public double MinRangeAdjust
        {
            get
            {
                return this.minRangeAdjust;
            }
            set
            {
                this.minRangeAdjust = value;
            }
        }

        public double MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
            }
        }

        public int NumDatasets
        {
            get
            {
                return this.numDatasets;
            }
            set
            {
                this.numDatasets = value;
            }
        }

        public int RoundMode
        {
            get
            {
                return this.roundMode;
            }
            set
            {
                this.roundMode = value;
            }
        }

        public PhysicalCoordinates TheChartCoordinates
        {
            get
            {
                return this.theChartCoordinates;
            }
            set
            {
                this.theChartCoordinates = value;
            }
        }

        public ChartDataset TheDataset
        {
            get
            {
                return this.theDataset;
            }
            set
            {
                this.theDataset = value;
            }
        }

        public ChartDataset[] TheDatasetArray
        {
            get
            {
                return this.theDatasetsArray;
            }
            set
            {
                this.theDatasetsArray = value;
            }
        }

        public double ZeroEndpointTestValue
        {
            get
            {
                return this.zeroEndpointTestValue;
            }
            set
            {
                this.zeroEndpointTestValue = value;
            }
        }
    }
}

