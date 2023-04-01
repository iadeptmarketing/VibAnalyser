namespace com.iAM.chart2dnet
{
    using System;
    using System.IO;

    public class TimeSimpleDataset : SimpleDataset
    {
        public TimeSimpleDataset()
        {
            this.InitDefaults();
        }

        public TimeSimpleDataset(string sname, int n)
        {
            this.InitDatasetBase(sname, n);
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
        }

        public TimeSimpleDataset(string sname, ChartCalendar[] x, ChartCalendar[] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeSimpleDataset(string sname, ChartCalendar[] x, double[] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeSimpleDataset(string sname, double[] x, ChartCalendar[] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeSimpleDataset(string sname, double[] x, double[] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeSimpleDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            this.ReadTimeSimpleDataset(csv, filename, rowskip, columnskip);
        }

        public int AddTimeDataPoint(ChartCalendar x, double y)
        {
            if (base.xCoordinateType == 2)
            {
                base.AddDataPoint((double) x.GetCalendarMsecs(), y);
            }
            return base.numberDatapoints;
        }

        public int AddTimeDataPoint(double x, ChartCalendar y)
        {
            if (base.yCoordinateType == 2)
            {
                base.AddDataPoint(x, (double) y.GetCalendarMsecs());
            }
            return base.numberDatapoints;
        }

        public override object Clone()
        {
            TimeSimpleDataset dataset = new TimeSimpleDataset();
            dataset.Copy(this);
            return dataset;
        }

        public TimeSimpleDataset CompressTimeFieldSimpleDataset(int ctypex, int ctypey, int timefield, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray array2 = null;
            int n = 0;
            TimeSimpleDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            array2 = ChartSupport.MakeTimeCompressArray(timefield, ctypey, startindex, endindex, base.xData, base.yData, validflags);
            array = ChartSupport.MakeTimeCompressArray(timefield, ctypex, startindex, endindex, base.xData, base.xData, validflags);
            n = Math.Min(array.Length, array2.Length);
            if (n <= 0)
            {
                dataset = (TimeSimpleDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new TimeSimpleDataset();
                dataset.InitDataset(newname, array.DataBuffer, array2.DataBuffer, n);
                dataset.xCoordinateType = 2;
                dataset.yCoordinateType = 0;
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public TimeSimpleDataset CompressTimeSimpleDataset(int ctypex, int ctypey, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray array2 = null;
            int n = 0;
            TimeSimpleDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            if (ctypex == 5)
            {
                ctypex = 3;
            }
            array = ChartSupport.MakeCompressArray(ctypex, interval, startindex, endindex, base.xData, validflags);
            array2 = ChartSupport.MakeCompressArray(ctypey, interval, startindex, endindex, base.yData, validflags);
            n = Math.Min(array.Length, array2.Length);
            if (n <= 0)
            {
                dataset = (TimeSimpleDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new TimeSimpleDataset();
                dataset.InitDataset(newname, array.DataBuffer, array2.DataBuffer, n);
                dataset.xCoordinateType = 2;
                dataset.yCoordinateType = 0;
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public void Copy(TimeSimpleDataset source)
        {
            if (source != null)
            {
                base.Copy(source);
                base.xCoordinateType = source.xCoordinateType;
                base.yCoordinateType = source.yCoordinateType;
            }
        }

        public int DeleteTimeDataPoint(int deletepoint)
        {
            base.DeleteDataPoint(deletepoint);
            return base.numberDatapoints;
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public ChartCalendar[] GetTimeXData()
        {
            ChartCalendar[] calendarArray = new ChartCalendar[base.numberDatapoints];
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                if (base.xCoordinateType == 2)
                {
                    calendarArray[i] = new ChartCalendar((long) base.GetXDataValue(i), true);
                }
                else
                {
                    calendarArray[i] = new ChartCalendar();
                }
            }
            return calendarArray;
        }

        public ChartCalendar GetTimeXDataValue(int index)
        {
            if ((index < base.numberDatapoints) && (base.xCoordinateType == 2))
            {
                return new ChartCalendar((long) base.GetXDataValue(index), true);
            }
            return new ChartCalendar();
        }

        public ChartCalendar[] GetTimeYData()
        {
            ChartCalendar[] calendarArray = new ChartCalendar[base.numberDatapoints];
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                if (base.yCoordinateType == 2)
                {
                    calendarArray[i] = new ChartCalendar((long) base.GetYDataValue(i), true);
                }
                else
                {
                    calendarArray[i] = new ChartCalendar();
                }
            }
            return calendarArray;
        }

        public ChartCalendar GetTimeYDataValue(int index)
        {
            if ((index < base.numberDatapoints) && (base.yCoordinateType == 2))
            {
                return new ChartCalendar((long) base.GetYDataValue(index), true);
            }
            return new ChartCalendar();
        }

        public void InitDataset(string sname, ChartCalendar[] x, ChartCalendar[] y)
        {
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.Length);
            this.InitDatasetBase(sname, x.Length);
            base.xCoordinateType = 2;
            base.yCoordinateType = 2;
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.xData[i] = x[i].GetCalendarMsecs();
                base.yData[i] = y[i].GetCalendarMsecs();
            }
            base.xData.SetLength(base.numberDatapoints);
            base.yData.SetLength(base.numberDatapoints);
        }

        public void InitDataset(string sname, ChartCalendar[] x, double[] y)
        {
            this.InitDatasetBase(sname, x.Length);
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
            this.InitializeData(x, y);
        }

        public void InitDataset(string sname, double[] x, ChartCalendar[] y)
        {
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.Length);
            this.InitDatasetBase(sname, x.Length);
            base.xCoordinateType = 0;
            base.yCoordinateType = 2;
            base.xData.SetElements(x);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.yData.SetElement(i, (double) y[i].GetCalendarMsecs());
                base.validData[i] = ChartSupport.BGoodValue(base.xData[i], base.yData[i]);
            }
            base.xData.SetLength(base.numberDatapoints);
            base.yData.SetLength(base.numberDatapoints);
        }

        public void InitDataset(string sname, double[] x, double[] y)
        {
            this.InitDatasetBase(sname, x.Length);
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
            base.InitializeData(x, y);
        }

        public void InitDataset(string sname, ChartCalendar[] x, double[] y, int n)
        {
            n = Math.Min(x.Length, Math.Min(y.Length, n));
            this.InitDatasetBase(sname, n);
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
            this.InitializeData(x, y);
        }

        private void InitDatasetBase(string sname, int n)
        {
            this.InitDefaults();
            base.numberDatapoints = n;
            base.dataName = sname;
            base.xData = new DoubleArray(base.numberDatapoints);
            base.yData = new DoubleArray(base.numberDatapoints);
            base.validData = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.SetValidData(i, true);
            }
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x44f;
        }

        public void InitializeData(ChartCalendar[] x, double[] y)
        {
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.Length);
            base.yData.SetElements(y);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.xData.SetElement(i, (double) x[i].GetCalendarMsecs());
                base.SetValidData(i, ChartSupport.BGoodValue(base.xData.GetElement(i), base.yData.GetElement(i)));
            }
            base.xData.SetLength(base.numberDatapoints);
            base.yData.SetLength(base.numberDatapoints);
        }

        public int InsertTimeDataPoint(ChartCalendar x, double y, int insertpoint)
        {
            double calendarMsecs = x.GetCalendarMsecs();
            base.InsertDataPoint(calendarMsecs, y, insertpoint);
            return base.numberDatapoints;
        }

        public void ReadTimeSimpleDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            StreamReader pReader = null;
            ChartCalendar[] calendarArray;
            double[] numArray;
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
                calendarArray = new ChartCalendar[num2];
                numArray = new double[num2];
            }
            else
            {
                if ((num < 1) || (num2 < 2))
                {
                    this.ErrorCheck(540);
                    return;
                }
                calendarArray = new ChartCalendar[num];
                numArray = new double[num];
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
                        calendarArray[num3] = csv.ReadTime(pReader);
                        numArray[num3] = csv.ReadDouble(pReader);
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
                        calendarArray[num3] = csv.ReadTime(pReader);
                    }
                    csv.Readln(pReader);
                    for (num4 = 0; num4 < columnskip; num4++)
                    {
                        csv.Read(pReader);
                    }
                    for (num3 = 0; num3 < num; num3++)
                    {
                        numArray[num3] = csv.ReadDouble(pReader);
                    }
                    csv.Readln(pReader);
                }
            }
            catch (IOException)
            {
                this.ErrorCheck(ChartObj.ERROR_FILEREAD);
            }
            if (csv.GetOrientation() == 1)
            {
                this.InitDataset(filename, calendarArray, numArray);
            }
            else
            {
                this.InitDataset(filename, calendarArray, numArray);
            }
            try
            {
                pReader.Close();
            }
            catch (IOException)
            {
                this.ErrorCheck(0x324);
            }
        }

        public void SetTimeDataPoint(int index, ChartCalendar x, double y)
        {
            if ((index < base.numberDatapoints) && (base.xCoordinateType == 2))
            {
                base.SetXDataValue(index, (double) x.GetCalendarMsecs());
                base.SetYDataValue(index, y);
            }
        }

        public void SetTimeDataPoint(int index, double x, ChartCalendar y)
        {
            if ((index < base.numberDatapoints) && (base.yCoordinateType == 2))
            {
                base.SetYDataValue(index, (double) y.GetCalendarMsecs());
                base.SetXDataValue(index, x);
            }
        }

        public void SetTimeXDataValue(int index, ChartCalendar x)
        {
            if ((index < base.numberDatapoints) && (base.xCoordinateType == 2))
            {
                base.SetXDataValue(index, (double) x.GetCalendarMsecs());
            }
        }

        public void SetTimeYDataValue(int index, ChartCalendar y)
        {
            if ((index < base.numberDatapoints) && (base.yCoordinateType == 2))
            {
                base.SetYDataValue(index, (double) y.GetCalendarMsecs());
            }
        }

        public void WriteTimeSimpleDataset(CSV csv, string filename)
        {
            this.WriteTimeSimpleDataset(csv, filename, false);
        }

        public void WriteTimeSimpleDataset(CSV csv, string filename, bool append)
        {
            ChartCalendar date = new ChartCalendar();
            StreamWriter pWriter = null;
            int numberDatapoints = base.GetNumberDatapoints();
            double[] elements = base.xData.GetElements();
            double[] numArray2 = base.yData.GetElements();
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
                    ChartCalendar.SetCalendarMsecs(date, (long) elements[i]);
                    csv.WriteTime(pWriter, date);
                    csv.WriteDouble(pWriter, numArray2[i]);
                    csv.Writeln(pWriter);
                }
            }
            else
            {
                for (int j = 0; j < numberDatapoints; j++)
                {
                    ChartCalendar.SetCalendarMsecs(date, (long) elements[j]);
                    csv.WriteTime(pWriter, date);
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
    }
}

