namespace com.iAM.chart2dnet
{
    using System;
    using System.IO;
    using System.Reflection;

    public class SimpleDataset : ChartDataset
    {
        internal string columnName;
        internal DoubleArray yData;

        public SimpleDataset()
        {
            this.yData = new DoubleArray();
            this.columnName = "";
            this.InitDefaults();
        }

        public SimpleDataset(string sname, int n)
        {
            this.yData = new DoubleArray();
            this.columnName = "";
            this.InitDatasetBase(sname, n);
        }

        public SimpleDataset(string sname, double[] x, double[] y)
        {
            this.yData = new DoubleArray();
            this.columnName = "";
            this.InitDataset(sname, x, y);
        }

        public SimpleDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            this.yData = new DoubleArray();
            this.columnName = "";
            this.ReadSimpleDataset(csv, filename, rowskip, columnskip);
        }

        public SimpleDataset(string sname, double[] x, double[] y, bool[] valid)
        {
            this.yData = new DoubleArray();
            this.columnName = "";
            this.InitDataset(sname, x, y);
            base.validData.SetElements(valid);
        }

        public SimpleDataset(string sname, double[] x, double[] y, bool[] valid, int n)
        {
            this.yData = new DoubleArray();
            this.columnName = "";
            this.InitDatasetBase(sname, n);
            this.InitializeData(x, y);
            base.validData.SetElements(valid);
        }

        public int AddDataPoint(Point2D p)
        {
            this.AddDataPoint(p.GetX(), p.GetY());
            return base.numberDatapoints;
        }

        public int AddDataPoint(double x, double y)
        {
            base.numberDatapoints = base.xData.Add(x);
            this.yData.Add(y);
            base.validData.Add(true);
            return base.numberDatapoints;
        }

        protected SimpleDataset AutoCompressDataset(SimpleDataset dataset, int compressmodex, int compressmodey, int trigger, int divisor)
        {
            SimpleDataset dataset2 = dataset;
            if (base.autoDataCompressEnable && (dataset.NumberDatapoints > trigger))
            {
                int interval = dataset.NumberDatapoints / divisor;
                dataset2 = dataset.CompressSimpleDataset(compressmodex, compressmodey, interval, 0, dataset.NumberDatapoints - 1, "Compressed");
            }
            return dataset2;
        }

        private void CalcMovingAverage(DoubleArray dest, DoubleArray source, int numsource, int averagepoints)
        {
            double num = 0.0;
            averagepoints = Math.Max(1, averagepoints);
            for (int i = averagepoints - 1; i < numsource; i++)
            {
                num = 0.0;
                for (int j = 0; j < averagepoints; j++)
                {
                    if (base.GetValidData(i - j))
                    {
                        num += source[i - j];
                    }
                }
                num /= (double) averagepoints;
                dest[i - (averagepoints - 1)] = num;
            }
        }

        public override bool CalcNearestPoint(PhysicalCoordinates transform, Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(transform, this, false, testpoint, nmode, nearestpoint);
        }

        public virtual bool CheckValidData(PhysicalCoordinates transform, int index)
        {
            bool flag = this.CheckValidDataX(transform, index);
            bool flag2 = this.CheckValidDataY(transform, index);
            return ((flag && flag2) && base.GetValidData(index));
        }

        public virtual bool CheckValidDataX(PhysicalCoordinates transform, int index)
        {
            double xDataValue = base.GetXDataValue(index);
            if (transform.GetXScale().GetChartObjType() == 0x4b2)
            {
                TimeCoordinates coordinates = (TimeCoordinates) transform;
                ChartCalendar cdate = new ChartCalendar((long) xDataValue, true);
                return coordinates.CheckValidDate(cdate);
            }
            return ChartSupport.BGoodValue(xDataValue);
        }

        public virtual bool CheckValidDataY(PhysicalCoordinates transform, int index)
        {
            double yDataValue = this.GetYDataValue(index);
            if (transform.GetYScale().GetChartObjType() == 0x4b2)
            {
                TimeCoordinates coordinates = (TimeCoordinates) transform;
                ChartCalendar cdate = new ChartCalendar((long) yDataValue, true);
                return coordinates.CheckValidDate(cdate);
            }
            return ChartSupport.BGoodValue(yDataValue);
        }

        public override object Clone()
        {
            SimpleDataset dataset = new SimpleDataset();
            dataset.Copy(this);
            return dataset;
        }

        public void CombineDataset(SimpleDataset source, int combinecoord, int op)
        {
            int num = Math.Min(base.numberDatapoints, source.GetNumberDatapoints());
            double y = 0.0;
            if ((combinecoord == 2) || (combinecoord == 3))
            {
                for (int i = 0; i < num; i++)
                {
                    if (this.IsDataPointGood(i))
                    {
                        switch (op)
                        {
                            case 1:
                                y = this.GetYDataValue(i) + source.GetYDataValue(i);
                                break;

                            case 2:
                                y = this.GetYDataValue(i) - source.GetYDataValue(i);
                                break;

                            case 3:
                                y = this.GetYDataValue(i) * source.GetYDataValue(i);
                                break;

                            case 4:
                                y = this.GetYDataValue(i) / source.GetYDataValue(i);
                                break;
                        }
                        this.SetYDataValue(i, y);
                    }
                }
            }
            if ((combinecoord == 1) || (combinecoord == 3))
            {
                for (int j = 0; j < num; j++)
                {
                    if (this.IsDataPointGood(j))
                    {
                        switch (op)
                        {
                            case 1:
                                y = base.GetXDataValue(j) + source.GetXDataValue(j);
                                break;

                            case 2:
                                y = base.GetXDataValue(j) - source.GetXDataValue(j);
                                break;

                            case 3:
                                y = base.GetXDataValue(j) * source.GetXDataValue(j);
                                break;

                            case 4:
                                y = base.GetXDataValue(j) / source.GetXDataValue(j);
                                break;
                        }
                        base.SetXDataValue(j, y);
                    }
                }
            }
        }

        public SimpleDataset CompressSimpleDataset(int ctypex, int ctypey, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray array2 = null;
            SimpleDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            array = ChartSupport.MakeCompressArray(ctypex, interval, startindex, endindex, base.xData, validflags);
            array2 = ChartSupport.MakeCompressArray(ctypey, interval, startindex, endindex, this.yData, validflags);
            if (Math.Min(array.Length, array2.Length) <= 0)
            {
                dataset = (SimpleDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new SimpleDataset(newname, array.GetElements(), array2.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public void ConvertToMovingAverage(int averagepoints, int convertMode)
        {
            int n = (base.numberDatapoints - averagepoints) + 1;
            DoubleArray dest = new DoubleArray(n);
            DoubleArray array2 = new DoubleArray(n);
            BoolArray source = new BoolArray(n);
            if (convertMode == 2)
            {
                this.CalcMovingAverage(array2, this.yData, base.numberDatapoints, averagepoints);
                this.ShiftArray(dest, base.xData, base.numberDatapoints, averagepoints);
            }
            else if (convertMode == 1)
            {
                this.CalcMovingAverage(dest, base.xData, base.numberDatapoints, averagepoints);
                this.ShiftArray(array2, this.yData, base.numberDatapoints, averagepoints);
            }
            else if (convertMode == 3)
            {
                this.CalcMovingAverage(dest, base.xData, base.numberDatapoints, averagepoints);
                this.CalcMovingAverage(array2, this.yData, base.numberDatapoints, averagepoints);
            }
            if (((convertMode == 1) || (convertMode == 2)) || (convertMode == 3))
            {
                for (int i = averagepoints - 1; i < base.numberDatapoints; i++)
                {
                    source[i - (averagepoints - 1)] = base.GetValidData(i);
                }
            }
            this.InitDataset(base.dataName, dest.DataBuffer, array2.DataBuffer, n);
            base.validData.SetElements(source);
        }

        public void Copy(SimpleDataset source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.yData = (DoubleArray) source.yData.Clone();
            }
        }

        public int DeleteDataPoint(int deletepoint)
        {
            base.numberDatapoints = base.xData.Delete(deletepoint);
            this.yData.Delete(deletepoint);
            base.validData.Delete(deletepoint);
            return base.numberDatapoints;
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.yData == null))
            {
                nerror = 500;
            }
            return base.ErrorCheck(nerror);
        }

        public double GetAverageY()
        {
            return (this.GetSumY() / ((double) base.numberDatapoints));
        }

        public Point2D GetDataPoint(int index)
        {
            Point2D pointd = new Point2D(0.0, 0.0);
            if ((index >= 0) && (index < base.numberDatapoints))
            {
                pointd.SetLocation(base.GetXDataValue(index), this.GetYDataValue(index));
            }
            return pointd;
        }

        public override double GetDatasetMax(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMaximum(base.xData, base.validData);
            }
            return ChartSupport.GetMaximum(this.yData, base.validData);
        }

        public override double GetDatasetMin(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMinimum(base.xData, base.validData);
            }
            return ChartSupport.GetMinimum(this.yData, base.validData);
        }

        public SimpleDataset GetFastClipDataset(string s, PhysicalCoordinates transform, int fastclipmode, ref int fastclipoffset)
        {
            double start;
            double stop;
            double[] yData;
            int n = 0;
            int num3 = 0;
            int num4 = 0;
            SimpleDataset dataset = this;
            if (!transform.ScaleInverted(fastclipmode))
            {
                start = transform.GetStart(fastclipmode);
                stop = transform.GetStop(fastclipmode);
            }
            else
            {
                stop = transform.GetStart(fastclipmode);
                start = transform.GetStop(fastclipmode);
            }
            if (fastclipmode == 1)
            {
                yData = this.GetYData();
            }
            else if (fastclipmode == 0)
            {
                yData = base.GetXData();
            }
            else
            {
                return this.AutoCompressDataset(dataset, base.autoCompressDatasetModeX, base.autoCompressDatasetModeY, base.autoCompressTriggerValue, base.autoCompressDivisor);
            }
            int index = 0;
            while (index < base.numberDatapoints)
            {
                if (yData[index] >= start)
                {
                    num3 = index;
                    if (num3 > 0)
                    {
                        num3--;
                    }
                    break;
                }
                index++;
            }
            fastclipoffset = Math.Max(0, num3);
            num4 = base.numberDatapoints - 1;
            for (index = num3; index < base.numberDatapoints; index++)
            {
                if (yData[index] >= stop)
                {
                    num4 = index;
                    break;
                }
            }
            n = Math.Max(0, (num4 - num3) + 1);
            DoubleArray dest = new DoubleArray(n);
            DoubleArray array2 = new DoubleArray(n);
            BoolArray array3 = new BoolArray(n);
            DoubleArray.CopyArray(base.xData, num3, dest, 0, n);
            DoubleArray.CopyArray(this.yData, num3, array2, 0, n);
            BoolArray.CopyArray(base.validData, num3, array3, 0, n);
            dataset = new SimpleDataset(s, dest.DataBuffer, array2.DataBuffer, array3.DataBuffer, n);
            dataset = this.AutoCompressDataset(dataset, base.autoCompressDatasetModeX, base.autoCompressDatasetModeY, base.autoCompressTriggerValue, base.autoCompressDivisor);
            dataset.xCoordinateType = base.xCoordinateType;
            dataset.yCoordinateType = base.yCoordinateType;
            dest = null;
            array2 = null;
            array3 = null;
            return dataset;
        }

        public virtual int GetFirstValidIndex()
        {
            return ChartSupport.GetFirstValidIndex(base.GetXData(), this.GetYData(), base.numberDatapoints);
        }

        public override double GetGroupDatasetSumMax(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMaximum(base.xData, base.validData);
            }
            return ChartSupport.GetMaximum(this.yData, base.validData);
        }

        public override double GetGroupDatasetSumMin(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMinimum(base.xData, base.validData);
            }
            return ChartSupport.GetMinimum(this.yData, base.validData);
        }

        public override int GetNumberGroups()
        {
            return 1;
        }

        public double GetSumY()
        {
            double num = 0.0;
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                if (ChartSupport.BGoodValue(this.GetYDataValue(i)) && base.GetValidData(i))
                {
                    num += this.GetYDataValue(i);
                }
            }
            return num;
        }

        public double[] GetYData()
        {
            return this.yData.DataBuffer;
        }

        public DoubleArray GetYDataObj()
        {
            return this.yData;
        }

        public double GetYDataValue(int index)
        {
            return this.yData.GetElement(index);
        }

        public override double GetYDataValue(int group, int index)
        {
            return this.GetYDataValue(index);
        }

        public void InitDataset(string sname, double[] x, double[] y)
        {
            this.InitDatasetBase(sname, x.Length);
            this.InitializeData(x, y);
        }

        public void InitDataset(string sname, double[] x, double[] y, int n)
        {
            n = Math.Min(x.Length, Math.Min(y.Length, n));
            this.InitDatasetBase(sname, n);
            this.InitializeData(x, y);
        }

        private void InitDatasetBase(string sname, int n)
        {
            this.InitDefaults();
            base.numberDatapoints = n;
            base.dataName = sname;
            base.xData = new DoubleArray(base.numberDatapoints);
            this.yData = new DoubleArray(base.numberDatapoints);
            base.validData = new BoolArray(base.numberDatapoints);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x44d;
        }

        public void InitializeData(double[] x, double[] y)
        {
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.Length);
            base.xData.SetElements(x);
            this.yData.SetElements(y);
            base.xData.SetLength(base.numberDatapoints);
            this.yData.SetLength(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.SetValidData(i, ChartSupport.BGoodValue(x[i], y[i]));
            }
        }

        public int InsertDataPoint(Point2D p, int insertpoint)
        {
            this.InsertDataPoint(p.GetX(), p.GetY(), insertpoint);
            return base.numberDatapoints;
        }

        public int InsertDataPoint(double xvalue, double yvalue, int insertpoint)
        {
            base.numberDatapoints = base.xData.Insert(insertpoint, xvalue);
            this.yData.Insert(insertpoint, yvalue);
            base.validData.Insert(insertpoint, true);
            return base.numberDatapoints;
        }

        public override bool IsDataPointGood(int index)
        {
            return (ChartSupport.BGoodValue(base.GetXDataValue(index), this.GetYDataValue(index)) && base.GetValidData(index));
        }

        public void ReadSimpleDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            StreamReader pReader = null;
            double[] numArray;
            double[] numArray2;
            try
            {
                pReader = new StreamReader(filename);
            }
            catch (IOException)
            {
                this.ErrorCheck(0x321);
            }
            int num = csv.GetFileNumColumns(pReader) - columnskip;
            int num2 = (csv.GetFileNumRows(pReader) + 1) - rowskip;
            if (csv.GetOrientation() == 1)
            {
                if ((num < 2) || (num2 < 1))
                {
                    this.ErrorCheck(540);
                    return;
                }
                numArray = new double[num2];
                numArray2 = new double[num2];
            }
            else
            {
                if ((num < 1) || (num2 < 2))
                {
                    this.ErrorCheck(540);
                    return;
                }
                numArray = new double[num];
                numArray2 = new double[num];
            }
            try
            {
                pReader.Close();
            }
            catch (IOException)
            {
                this.ErrorCheck(0x324);
            }
            try
            {
                pReader = new StreamReader(filename);
            }
            catch (IOException)
            {
                this.ErrorCheck(0x321);
            }
            try
            {
                int num3;
                int num4;
                if (csv.GetOrientation() == 1)
                {
                    for (num3 = 0; num3 < rowskip; num3++)
                    {
                        csv.Readln(pReader);
                    }
                    for (num3 = 0; num3 < num2; num3++)
                    {
                        num4 = 0;
                        while (num4 < columnskip)
                        {
                            csv.Read(pReader);
                            num4++;
                        }
                        numArray[num3] = csv.ReadDouble(pReader);
                        numArray2[num3] = csv.ReadDouble(pReader);
                        csv.Readln(pReader);
                    }
                }
                else
                {
                    num3 = 0;
                    while (num3 < rowskip)
                    {
                        csv.Readln(pReader);
                        num3++;
                    }
                    for (num4 = 0; num4 < columnskip; num4++)
                    {
                        csv.Read(pReader);
                    }
                    for (num3 = 0; num3 < num; num3++)
                    {
                        numArray[num3] = csv.ReadDouble(pReader);
                    }
                    csv.Readln(pReader);
                    for (num4 = 0; num4 < columnskip; num4++)
                    {
                        csv.Read(pReader);
                    }
                    for (num3 = 0; num3 < num; num3++)
                    {
                        numArray2[num3] = csv.ReadDouble(pReader);
                    }
                    csv.Readln(pReader);
                }
            }
            catch (IOException)
            {
                this.ErrorCheck(ChartObj.ERROR_FILEREAD);
            }
            this.InitDataset(filename, numArray, numArray2);
            try
            {
                pReader.Close();
            }
            catch (IOException)
            {
                this.ErrorCheck(0x324);
            }
        }

        public void ResetBuffer()
        {
            base.numberDatapoints = 0;
            base.xData.Clear();
            this.yData.Clear();
            base.initialCondition = true;
        }

        public virtual int Resize(int n)
        {
            base.numberDatapoints = base.xData.Resize(n);
            this.yData.Resize(n);
            base.validData.Resize(n);
            return base.numberDatapoints;
        }

        public void SetDataPoint(int index, Point2D p)
        {
            base.xData.SetElement(index, p.GetX());
            this.yData.SetElement(index, p.GetY());
        }

        public void SetDataPoint(int index, double x, double y)
        {
            base.SetXDataValue(index, x);
            this.SetYDataValue(index, y);
        }

        public void SetYData(double[] yvalues)
        {
            this.yData.SetElements(yvalues);
        }

        public void SetYData(DoubleArray yvalues)
        {
            this.yData.SetElements(yvalues);
        }

        public void SetYDataValue(int index, double y)
        {
            this.yData.SetElement(index, y);
        }

        public override void SetYDataValue(int group, int index, double y)
        {
            this.SetYDataValue(index, y);
        }

        private void ShiftArray(DoubleArray dest, DoubleArray source, int numsource, int shiftpoints)
        {
            for (int i = shiftpoints - 1; i < numsource; i++)
            {
                dest[i - (shiftpoints - 1)] = source[i];
            }
        }

        public void SortByX(bool ascending)
        {
            ChartDataset.DatasetSortClass[] classArray = new ChartDataset.DatasetSortClass[base.numberDatapoints];
            int index = 0;
            int num2 = 0;
            DoubleArray source = new DoubleArray(base.numberDatapoints);
            DoubleArray array2 = new DoubleArray(base.numberDatapoints);
            BoolArray array3 = new BoolArray(base.numberDatapoints);
            for (index = 0; index < base.numberDatapoints; index++)
            {
                classArray[index] = new ChartDataset.DatasetSortClass(index, base.GetXDataValue(index));
            }
            Array.Sort(classArray);
            for (index = 0; index < base.numberDatapoints; index++)
            {
                if (ascending)
                {
                    num2 = classArray[index].index;
                }
                else
                {
                    num2 = classArray[(base.numberDatapoints - index) - 1].index;
                }
                source[index] = base.GetXDataValue(num2);
                array2[index] = this.GetYDataValue(num2);
                array3[index] = base.GetValidData(num2);
            }
            base.xData.Copy(source);
            this.yData.Copy(array2);
            base.validData.Copy(array3);
        }

        public void WriteSimpleDataset(CSV csv, string filename)
        {
            this.WriteSimpleDataset(csv, filename, false);
        }

        public void WriteSimpleDataset(CSV csv, string filename, bool append)
        {
            StreamWriter pWriter = null;
            int numberDatapoints = base.GetNumberDatapoints();
            double[] elements = base.xData.GetElements();
            double[] numArray2 = this.yData.GetElements();
            base.validData.GetElements();
            try
            {
                pWriter = new StreamWriter(filename, append);
            }
            catch (IOException)
            {
                this.ErrorCheck(0x321);
            }
            if (csv.GetOrientation() == 1)
            {
                for (int i = 0; i < numberDatapoints; i++)
                {
                    csv.WriteDouble(pWriter, elements[i]);
                    csv.WriteDouble(pWriter, numArray2[i]);
                    csv.Writeln(pWriter);
                }
            }
            else
            {
                for (int j = 0; j < numberDatapoints; j++)
                {
                    csv.WriteDouble(pWriter, elements[j]);
                }
                csv.Writeln(pWriter);
                for (int k = 0; k < numberDatapoints; k++)
                {
                    csv.WriteDouble(pWriter, numArray2[k]);
                }
                csv.Writeln(pWriter);
            }
            pWriter.Close();
        }

        public Point2D this[int index]
        {
            get
            {
                return this.GetDataPoint(index);
            }
            set
            {
                this.SetDataPoint(index, value);
            }
        }

        public DoubleArray YData
        {
            get
            {
                return this.yData;
            }
            set
            {
                this.yData = value;
            }
        }
    }
}

