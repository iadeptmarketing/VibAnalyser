namespace com.iAM.chart2dnet
{
    using System;
    using System.Reflection;

    public abstract class ChartDataset : ChartObj
    {
        internal int autoCompressDatasetModeX = 0;
        internal int autoCompressDatasetModeY = 5;
        internal int autoCompressDivisor = 0x7d0;
        internal int autoCompressTriggerValue = 0x1388;
        internal bool autoDataCompressEnable = false;
        internal string dataName = "";
        internal StringArray groupStrings = new StringArray();
        public bool initialCondition = true;
        internal int numberDatapoints = 0;
        internal int numberGroups = 1;
        internal int stackMode = 0;
        internal BoolArray validData = new BoolArray();
        internal int xCoordinateType = 0;
        internal DoubleArray xData = new DoubleArray();
        internal int yCoordinateType = 0;

        public abstract bool CalcNearestPoint(PhysicalCoordinates transform, Point2D testpoint, int nmode, NearestPointData nearestpoint);
        public void Copy(ChartDataset source)
        {
            if (source != null)
            {
                this.numberDatapoints = source.numberDatapoints;
                this.numberGroups = source.numberGroups;
                this.dataName = source.dataName;
                this.xData = (DoubleArray) source.xData.Clone();
                this.validData = (BoolArray) source.validData.Clone();
                this.stackMode = source.stackMode;
                this.xCoordinateType = source.xCoordinateType;
                this.yCoordinateType = source.yCoordinateType;
                this.autoDataCompressEnable = source.autoDataCompressEnable;
                this.autoCompressDatasetModeX = source.autoCompressDatasetModeX;
                this.autoCompressDatasetModeY = source.autoCompressDatasetModeY;
                this.autoCompressTriggerValue = source.autoCompressTriggerValue;
                this.autoCompressDivisor = source.autoCompressDivisor;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.numberDatapoints < 0)
                {
                    nerror = 530;
                    return nerror;
                }
                if (this.xData == null)
                {
                    nerror = 530;
                    return nerror;
                }
                if (this.validData == null)
                {
                    nerror = 530;
                }
            }
            return nerror;
        }

        public double GetAverageX()
        {
            return (this.GetSumX() / ((double) this.numberDatapoints));
        }

        public string GetDataName()
        {
            return this.dataName;
        }

        public abstract double GetDatasetMax(int naxis);
        public abstract double GetDatasetMin(int naxis);
        public abstract double GetGroupDatasetSumMax(int naxis);
        public abstract double GetGroupDatasetSumMin(int naxis);
        public string GetGroupString(int index)
        {
            string str = "";
            if (index < this.groupStrings.Length)
            {
                str = this.groupStrings[index];
            }
            return str;
        }

        public int GetNumberDatapoints()
        {
            return this.numberDatapoints;
        }

        public abstract int GetNumberGroups();
        public int GetStackMode()
        {
            return this.stackMode;
        }

        public double GetSumX()
        {
            double num = 0.0;
            for (int i = 0; i < this.numberDatapoints; i++)
            {
                if (ChartSupport.BGoodValue(this.xData[i]) && this.validData[i])
                {
                    num += this.xData[i];
                }
            }
            return num;
        }

        public int GetTimeScaleAxis()
        {
            int num = 0;
            if ((this.xCoordinateType == 2) && (this.yCoordinateType != 2))
            {
                return 0;
            }
            if ((this.yCoordinateType == 2) && (this.xCoordinateType != 2))
            {
                return 1;
            }
            if ((this.yCoordinateType == 2) && (this.xCoordinateType == 2))
            {
                return 100;
            }
            if ((this.yCoordinateType != 2) && (this.xCoordinateType != 2))
            {
                num = 0x65;
            }
            return num;
        }

        public bool[] GetValidData()
        {
            return this.validData.DataBuffer;
        }

        public bool GetValidData(int index)
        {
            return this.validData[index];
        }

        public BoolArray GetValidDataObj()
        {
            return this.validData;
        }

        public int GetXCoordinateType()
        {
            return this.xCoordinateType;
        }

        public double[] GetXData()
        {
            return this.xData.GetDataBuffer();
        }

        public DoubleArray GetXDataObj()
        {
            return this.xData;
        }

        public double GetXDataValue(int index)
        {
            return this.xData[index];
        }

        public int GetYCoordinateType()
        {
            return this.yCoordinateType;
        }

        public abstract double GetYDataValue(int group, int index);
        public abstract bool IsDataPointGood(int index);
        protected void MarkBadDataInvalid()
        {
            for (int i = 0; i < this.numberDatapoints; i++)
            {
                if (this.IsDataPointGood(i))
                {
                    this.validData[i] = true;
                }
                else
                {
                    this.validData[i] = false;
                }
            }
        }

        public void SetDataName(string dataname)
        {
            this.dataName = dataname;
        }

        public void SetStackMode(int mode)
        {
            this.stackMode = mode;
        }

        public void SetTimeXData(ChartCalendar[] xvalues)
        {
            this.numberDatapoints = xvalues.Length;
            for (int i = 0; i < this.numberDatapoints; i++)
            {
                this.SetXDataValue(i, (double) xvalues[i].GetCalendarMsecs());
            }
            this.validData = new BoolArray(this.numberDatapoints);
        }

        public void SetValidData(int index, bool valid)
        {
            this.validData[index] = valid;
        }

        public void SetXCoordinateType(int coordtype)
        {
            this.xCoordinateType = coordtype;
        }

        public void SetXData(DoubleArray xvalues)
        {
            this.numberDatapoints = this.xData.SetElements(xvalues);
            this.validData = new BoolArray(this.numberDatapoints);
        }

        public void SetXData(double[] xvalues)
        {
            this.numberDatapoints = this.xData.SetElements(xvalues);
            this.validData = new BoolArray(this.numberDatapoints);
        }

        public void SetXDataValue(int index, double x)
        {
            this.xData[index] = x;
        }

        public void SetYCoordinateType(int coordtype)
        {
            this.yCoordinateType = coordtype;
        }

        public abstract void SetYDataValue(int group, int index, double y);

        public int AutoCompressDatasetModeX
        {
            get
            {
                return this.autoCompressDatasetModeX;
            }
            set
            {
                this.autoCompressDatasetModeX = value;
            }
        }

        public int AutoCompressDatasetModeY
        {
            get
            {
                return this.autoCompressDatasetModeY;
            }
            set
            {
                this.autoCompressDatasetModeY = value;
            }
        }

        public int AutoCompressDivisor
        {
            get
            {
                return this.autoCompressDivisor;
            }
            set
            {
                this.autoCompressDivisor = value;
            }
        }

        public int AutoCompressTriggerValue
        {
            get
            {
                return this.autoCompressTriggerValue;
            }
            set
            {
                this.autoCompressTriggerValue = value;
            }
        }

        public bool AutoDataCompressEnable
        {
            get
            {
                return this.autoDataCompressEnable;
            }
            set
            {
                this.autoDataCompressEnable = value;
            }
        }

        public string DataName
        {
            get
            {
                return this.dataName;
            }
            set
            {
                this.dataName = value;
            }
        }

        public StringArray GroupStrings
        {
            get
            {
                return this.groupStrings;
            }
        }

        public double this[int index]
        {
            get
            {
                return this.xData[index];
            }
            set
            {
                this.xData[index] = value;
            }
        }

        public int NumberDatapoints
        {
            get
            {
                return this.numberDatapoints;
            }
        }

        public int NumberGroups
        {
            get
            {
                return this.numberGroups;
            }
        }

        public int StackMode
        {
            get
            {
                return this.stackMode;
            }
            set
            {
                this.stackMode = value;
            }
        }

        public BoolArray ValidData
        {
            get
            {
                return this.validData;
            }
            set
            {
                this.validData = value;
            }
        }

        public int XCoordinateType
        {
            get
            {
                return this.xCoordinateType;
            }
        }

        public DoubleArray XData
        {
            get
            {
                return this.xData;
            }
            set
            {
                this.xData = value;
            }
        }

        public int YCoordinateType
        {
            get
            {
                return this.yCoordinateType;
            }
        }

        protected class DatasetSortClass : IComparable
        {
            public int index;
            public double xValue;

            public DatasetSortClass()
            {
            }

            public DatasetSortClass(int i, double x)
            {
                this.xValue = x;
                this.index = i;
            }

            public int CompareTo(object o)
            {
                int num = 0;
                ChartDataset.DatasetSortClass class2 = (ChartDataset.DatasetSortClass) o;
                if (class2.xValue < this.xValue)
                {
                    return 1;
                }
                if (class2.xValue > this.xValue)
                {
                    num = -1;
                }
                return num;
            }
        }
    }
}

