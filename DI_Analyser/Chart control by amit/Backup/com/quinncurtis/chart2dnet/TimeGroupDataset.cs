namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.IO;

    public class TimeGroupDataset : GroupDataset
    {
        public TimeGroupDataset()
        {
            this.InitDefaults();
        }

        public TimeGroupDataset(string sname, ChartCalendar[] x, ChartCalendar[,] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeGroupDataset(string sname, ChartCalendar[] x, double[,] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeGroupDataset(string sname, ChartCalendar[] x, double[] y)
        {
            double[,] numArray = new double[1, y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                numArray[0, i] = y[i];
            }
            this.InitDataset(sname, x, numArray);
        }

        public TimeGroupDataset(string sname, double[] x, ChartCalendar[,] y)
        {
            this.InitDataset(sname, x, y);
        }

        public TimeGroupDataset(string sname, int nrows, int ncols)
        {
            this.InitDatasetBase(sname, nrows, ncols);
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
        }

        public TimeGroupDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            this.ReadTimeGroupDataset(csv, filename, rowskip, columnskip);
        }

        public int AddTimeGroupDataPoints(ChartCalendar x, double[] y)
        {
            if (base.xCoordinateType == 2)
            {
                base.AddGroupDataPoints((double) x.GetCalendarMsecs(), y);
            }
            return base.numberDatapoints;
        }

        public override object Clone()
        {
            TimeGroupDataset dataset = new TimeGroupDataset();
            dataset.Copy(this);
            return dataset;
        }

        public TimeGroupDataset CompressTimeFieldGroupDataset(int ctypex, int[] ctypey, int timefield, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray2D dest = null;
            TimeGroupDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            if (ctypex == 5)
            {
                ctypex = 3;
            }
            array = ChartSupport.MakeTimeCompressArray(timefield, ctypex, startindex, endindex, base.xData, base.xData, validflags);
            dest = new DoubleArray2D(base.numberGroups, base.numberDatapoints);
            for (int j = 0; j < base.numberGroups; j++)
            {
                DoubleArray2D.CopyArray(ChartSupport.MakeCompressArray(timefield, ctypey[j], startindex, endindex, base.yGroupData.GetRowObject(j), validflags), 0, dest, j, 0, base.numberDatapoints);
            }
            if (array.Length <= 0)
            {
                dataset = (TimeGroupDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new TimeGroupDataset();
                dataset.InitDataset(newname, array.GetElements(), dest.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public TimeGroupDataset CompressTimeFieldGroupDataset(int ctypex, int ctypey, int timefield, int startindex, int endindex, string newname)
        {
            int[] numArray = new int[base.numberGroups];
            for (int i = 0; i < base.numberGroups; i++)
            {
                numArray[i] = ctypey;
            }
            return this.CompressTimeFieldGroupDataset(ctypex, numArray, timefield, startindex, endindex, newname);
        }

        public TimeGroupDataset CompressTimeGroupDataset(int ctypex, int ctypey, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray2D arrayd = null;
            TimeGroupDataset dataset = null;
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
            arrayd = ChartSupport.MakeGroupCompressArray(ctypey, interval, startindex, endindex, base.yGroupData, validflags);
            if (array.Length <= 0)
            {
                dataset = (TimeGroupDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new TimeGroupDataset();
                dataset.InitDataset(newname, array.GetElements(), arrayd.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public TimeGroupDataset CompressTimeGroupDataset(int ctypex, int[] ctypey, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray2D dest = null;
            TimeGroupDataset dataset = null;
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
            dest = new DoubleArray2D(base.numberGroups, base.numberDatapoints);
            for (int j = 0; j < base.numberGroups; j++)
            {
                DoubleArray2D.CopyArray(ChartSupport.MakeCompressArray(ctypey[j], interval, startindex, endindex, base.yGroupData.GetRowObject(j), validflags), 0, dest, j, 0, base.numberDatapoints);
            }
            if (array.Length <= 0)
            {
                dataset = (TimeGroupDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new TimeGroupDataset();
                dataset.InitDataset(newname, array.GetElements(), dest.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public void Copy(TimeGroupDataset source)
        {
            if (source != null)
            {
                base.Copy(source);
                base.xCoordinateType = source.xCoordinateType;
                base.yCoordinateType = source.yCoordinateType;
            }
        }

        public int DeleteTimeGroupDataPoints(int deletepoint)
        {
            base.DeleteGroupDataPoints(deletepoint);
            return base.numberDatapoints;
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public TimeSimpleDataset GetTimeSimpleDataset(string sname, int group)
        {
            double[] elements = base.xData.GetElements();
            return new TimeSimpleDataset(sname, elements, base.yGroupData.GetRow(group));
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

        public ChartCalendar[,] GetTimeYData()
        {
            ChartCalendar[,] calendarArray = new ChartCalendar[base.numberGroups, base.numberDatapoints];
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                for (int j = 0; j < base.numberGroups; j++)
                {
                    if (base.yCoordinateType == 2)
                    {
                        calendarArray[j, i] = new ChartCalendar((long) base.GetYGroupDataValue(j, i), true);
                    }
                    else
                    {
                        calendarArray[j, i] = new ChartCalendar();
                    }
                }
            }
            return calendarArray;
        }

        public ChartCalendar GetTimeYDataValue(int group, int index)
        {
            if (((index < base.numberDatapoints) && (group < base.numberGroups)) && (base.yCoordinateType == 2))
            {
                return new ChartCalendar((long) base.GetYGroupDataValue(group, index), true);
            }
            return new ChartCalendar();
        }

        public void InitDataset(string sname, ChartCalendar[] x, ChartCalendar[,] y)
        {
            base.xCoordinateType = 2;
            base.yCoordinateType = 2;
            this.InitDatasetBase(sname, y.GetLength(0), x.Length);
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.GetLength(1));
            base.xData.SetLength(base.numberDatapoints);
            base.yGroupData.ResizeNumColumns(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.xData.SetElement(i, (double) x[i].GetCalendarMsecs());
                for (int j = 0; j < base.numberGroups; j++)
                {
                    base.yGroupData.SetElement(j, i, (double) y[j, i].GetCalendarMsecs());
                }
            }
        }

        public void InitDataset(string sname, ChartCalendar[] x, double[,] y)
        {
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
            this.InitDatasetBase(sname, y.GetLength(0), x.Length);
            this.InitializeData(x, y);
        }

        public void InitDataset(string sname, double[] x, ChartCalendar[,] y)
        {
            base.xCoordinateType = 0;
            base.yCoordinateType = 2;
            this.InitDatasetBase(sname, y.GetLength(0), x.Length);
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.GetLength(1));
            base.xData.SetElements(x);
            base.xData.SetLength(base.numberDatapoints);
            base.yGroupData.ResizeNumColumns(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                for (int j = 0; j < base.numberGroups; j++)
                {
                    base.yGroupData.SetElement(j, i, (double) y[j, i].GetCalendarMsecs());
                }
            }
        }

        public void InitDataset(string sname, double[] x, double[,] y)
        {
            base.xCoordinateType = 2;
            base.yCoordinateType = 0;
            this.InitDatasetBase(sname, y.GetLength(0), x.Length);
            base.InitializeData(x, y);
        }

        private void InitDatasetBase(string sname, int nrows, int ncols)
        {
            base.numberDatapoints = ncols;
            base.numberGroups = nrows;
            base.autoScaleNumberGroups = base.numberGroups;
            base.dataName = sname;
            this.InitDefaults();
            base.xData = new DoubleArray(base.numberDatapoints);
            base.yGroupData = new DoubleArray2D(base.numberGroups, base.numberDatapoints);
            base.validData = new BoolArray(base.numberDatapoints);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x450;
        }

        public void InitializeData(ChartCalendar[] x, double[,] y)
        {
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.GetLength(1));
            base.yGroupData.SetElements(y);
            base.xData.SetLength(base.numberDatapoints);
            base.yGroupData.ResizeNumColumns(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.xData.SetElement(i, (double) x[i].GetCalendarMsecs());
            }
        }

        public int InsertTimeGroupDataPoints(ChartCalendar x, double[] yvalue, int insertpoint)
        {
            double calendarMsecs = x.GetCalendarMsecs();
            base.InsertGroupDataPoints(calendarMsecs, yvalue, insertpoint);
            return base.numberDatapoints;
        }

        public void ReadTimeGroupDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            StreamReader pReader = null;
            ChartCalendar[] calendarArray;
            double[,] numArray;
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
                numArray = new double[num - 1, num2];
            }
            else
            {
                if ((num < 1) || (num2 < 2))
                {
                    this.ErrorCheck(540);
                    return;
                }
                calendarArray = new ChartCalendar[num];
                numArray = new double[num2 - 1, num];
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
                        for (num4 = 0; num4 < (num - 1); num4++)
                        {
                            numArray[num4, num3] = csv.ReadDouble(pReader);
                        }
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
                    num4 = 0;
                    while (num4 < columnskip)
                    {
                        csv.Read(pReader);
                        num4++;
                    }
                    for (num3 = 0; num3 < num; num3++)
                    {
                        calendarArray[num3] = csv.ReadTime(pReader);
                    }
                    for (num3 = 0; num3 < (num2 - 1); num3++)
                    {
                        num4 = 0;
                        while (num4 < columnskip)
                        {
                            csv.Read(pReader);
                            num4++;
                        }
                        for (num4 = 0; num4 < num; num4++)
                        {
                            numArray[num3, num4] = csv.ReadDouble(pReader);
                        }
                        csv.Readln(pReader);
                    }
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

        public void SetTimeGroupDataColumn(ChartCalendar x, double[] y, int ncolumn)
        {
            if (ncolumn < base.numberDatapoints)
            {
                this.SetTimeXDataValue(x, ncolumn);
                for (int i = 0; i < base.numberGroups; i++)
                {
                    base.SetYGroupDataValue(i, ncolumn, y[i]);
                }
            }
        }

        public void SetTimeXDataValue(ChartCalendar x, int index)
        {
            if ((index < base.numberDatapoints) && (base.xCoordinateType == 2))
            {
                base.SetXDataValue(index, (double) x.GetCalendarMsecs());
            }
        }

        public void SetTimeYDataValue(ChartCalendar y, int group, int index)
        {
            if (((index < base.numberDatapoints) && (group < base.numberGroups)) && (base.yCoordinateType == 2))
            {
                base.SetYGroupDataValue(group, index, (double) y.GetCalendarMsecs());
            }
        }

        public void WriteTimeGroupDataset(CSV csv, string filename)
        {
            this.WriteTimeGroupDataset(csv, filename, false);
        }

        public void WriteTimeGroupDataset(CSV csv, string filename, bool append)
        {
            ChartCalendar date = new ChartCalendar();
            StreamWriter pWriter = null;
            int numberDatapoints = base.GetNumberDatapoints();
            int numberGroups = this.GetNumberGroups();
            double[] elements = base.xData.GetElements();
            double[,] numArray2 = base.yGroupData.GetElements();
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
                    for (int j = 0; j < numberGroups; j++)
                    {
                        csv.WriteDouble(pWriter, numArray2[j, i]);
                    }
                    csv.Writeln(pWriter);
                }
            }
            else
            {
                for (int k = 0; k < numberDatapoints; k++)
                {
                    ChartCalendar.SetCalendarMsecs(date, (long) elements[k]);
                    csv.WriteTime(pWriter, date);
                }
                csv.Writeln(pWriter);
                for (int m = 0; m < numberGroups; m++)
                {
                    for (int n = 0; n < numberDatapoints; n++)
                    {
                        csv.WriteDouble(pWriter, numArray2[m, n]);
                    }
                    csv.Writeln(pWriter);
                }
            }
            pWriter.Close();
        }
    }
}

