namespace com.iAM.chart2dnet
{
    using System;
    using System.IO;
    using System.Reflection;

    public class GroupDataset : ChartDataset
    {
        internal int autoScaleNumberGroups;
        internal DoubleArray2D yGroupData;

        public GroupDataset()
        {
            this.yGroupData = new DoubleArray2D();
            this.autoScaleNumberGroups = 0;
            this.InitDefaults();
        }

        public GroupDataset(string sname, double[] x, double[,] y)
        {
            this.yGroupData = new DoubleArray2D();
            this.autoScaleNumberGroups = 0;
            this.InitDataset(sname, x, y);
        }

        public GroupDataset(string sname, int nrows, int ncols)
        {
            this.yGroupData = new DoubleArray2D();
            this.autoScaleNumberGroups = 0;
            this.InitDatasetBase(sname, nrows, ncols);
        }

        public GroupDataset(string sname, double[] x, double[] y)
        {
            this.yGroupData = new DoubleArray2D();
            this.autoScaleNumberGroups = 0;
            double[,] numArray = new double[1, y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                numArray[0, i] = y[i];
            }
            this.InitDataset(sname, x, numArray);
        }

        public GroupDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            this.yGroupData = new DoubleArray2D();
            this.autoScaleNumberGroups = 0;
            this.ReadGroupDataset(csv, filename, rowskip, columnskip);
        }

        public GroupDataset(string sname, double[] x, double[,] y, bool[] valid)
        {
            this.yGroupData = new DoubleArray2D();
            this.autoScaleNumberGroups = 0;
            this.InitDataset(sname, x, y);
            base.validData.SetElements(valid);
        }

        public void AddGroup(double[] y)
        {
            base.numberGroups = this.yGroupData.AddRow(y);
            this.autoScaleNumberGroups = base.numberGroups;
        }

        public int AddGroupDataPoints(GroupPoint2D xy)
        {
            base.numberDatapoints = base.xData.Add(xy.GetX());
            this.yGroupData.AddColumn(xy.GetY());
            base.validData.Add(true);
            return base.numberDatapoints;
        }

        public int AddGroupDataPoints(double x, double[] y)
        {
            base.numberDatapoints = base.xData.Add(x);
            this.yGroupData.AddColumn(y);
            base.validData.Add(true);
            return base.numberDatapoints;
        }

        protected GroupDataset AutoCompressDataset(GroupDataset dataset, int compressmodex, int compressmodey, int trigger, int divisor)
        {
            GroupDataset dataset2 = dataset;
            if (base.autoDataCompressEnable && (dataset.NumberDatapoints > trigger))
            {
                int interval = dataset.NumberDatapoints / divisor;
                dataset2 = dataset.CompressGroupDataset(compressmodex, compressmodey, interval, 0, dataset.NumberDatapoints - 1, "Compressed");
            }
            return dataset2;
        }

        public override bool CalcNearestPoint(PhysicalCoordinates transform, Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(transform, this, false, testpoint, nmode, nearestpoint);
        }

        public virtual bool CheckValidGroupData(PhysicalCoordinates transform, int index)
        {
            bool flag = true;
            bool flag2 = true;
            flag = this.CheckValidGroupDataX(transform, index);
            for (int i = 0; i < base.numberGroups; i++)
            {
                flag2 = flag2 && this.CheckValidGroupDataY(transform, index, i);
            }
            return ((flag && flag2) && base.GetValidData(index));
        }

        public virtual bool CheckValidGroupData(PhysicalCoordinates transform, int ngroup, int index)
        {
            bool flag = true;
            bool flag2 = true;
            flag = this.CheckValidGroupDataX(transform, index);
            flag2 = flag2 && this.CheckValidGroupDataY(transform, ngroup, index);
            return ((flag && flag2) && base.GetValidData(index));
        }

        public virtual bool CheckValidGroupDataX(PhysicalCoordinates transform, int index)
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

        public virtual bool CheckValidGroupDataY(PhysicalCoordinates transform, int ngroup, int index)
        {
            double yGroupDataValue = this.GetYGroupDataValue(ngroup, index);
            if (transform.GetYScale().GetChartObjType() == 0x4b2)
            {
                TimeCoordinates coordinates = (TimeCoordinates) transform;
                ChartCalendar cdate = new ChartCalendar((long) yGroupDataValue, true);
                return coordinates.CheckValidDate(cdate);
            }
            return ChartSupport.BGoodValue(yGroupDataValue);
        }

        public override object Clone()
        {
            GroupDataset dataset = new GroupDataset();
            dataset.Copy(this);
            return dataset;
        }

        public GroupDataset CompressGroupDataset(int ctypex, int[] ctypey, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray2D dest = null;
            GroupDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            array = ChartSupport.MakeCompressArray(ctypex, interval, startindex, endindex, base.xData, validflags);
            dest = new DoubleArray2D(base.numberGroups, base.numberDatapoints);
            for (int j = 0; j < base.numberGroups; j++)
            {
                DoubleArray2D.CopyArray(ChartSupport.MakeCompressArray(ctypey[j], interval, startindex, endindex, this.yGroupData.GetRowObject(j), validflags), 0, dest, j, 0, base.numberDatapoints);
            }
            if (array.Length <= 0)
            {
                dataset = (GroupDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new GroupDataset(newname, array.DataBuffer, dest.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public GroupDataset CompressGroupDataset(int ctypex, int ctypey, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray2D arrayd = null;
            GroupDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            array = ChartSupport.MakeCompressArray(ctypex, interval, startindex, endindex, base.xData, validflags);
            arrayd = ChartSupport.MakeGroupCompressArray(ctypey, interval, startindex, endindex, this.yGroupData, validflags);
            if (array.Length <= 0)
            {
                dataset = (GroupDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new GroupDataset(newname, array.DataBuffer, arrayd.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public void Copy(GroupDataset source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.autoScaleNumberGroups = source.autoScaleNumberGroups;
                this.yGroupData = (DoubleArray2D) source.yGroupData.Clone();
            }
        }

        public int DeleteGroup(int deletegroup)
        {
            base.numberGroups = this.yGroupData.DeleteRow(deletegroup);
            return base.numberGroups;
        }

        public int DeleteGroupDataPoints(int deletepoint)
        {
            base.numberDatapoints = base.xData.Delete(deletepoint);
            this.yGroupData.DeleteColumn(deletepoint);
            base.validData.Delete(deletepoint);
            return base.numberDatapoints;
        }

        public override int ErrorCheck(int nerror)
        {
            if (base.numberDatapoints < 0)
            {
                nerror = 510;
            }
            else if (base.xData == null)
            {
                nerror = 510;
            }
            else if (base.numberGroups < 0)
            {
                nerror = 510;
            }
            return base.ErrorCheck(nerror);
        }

        public int FindNearestGroup(int index, double findvalue)
        {
            int num = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            if (this.IsDataPointGood(index))
            {
                for (int i = 0; i < base.numberGroups; i++)
                {
                    num2 = Math.Abs((double) (findvalue - this.GetYGroupDataValue(i, index)));
                    if (i == 0)
                    {
                        num3 = num2;
                    }
                    else if (num2 < num3)
                    {
                        num = i;
                        num3 = num2;
                    }
                }
            }
            return num;
        }

        public int GetAutoScaleNumberGroups()
        {
            return this.autoScaleNumberGroups;
        }

        public Point2D GetDataPoint(int ngroup, int index)
        {
            Point2D pointd = new Point2D(0.0, 0.0);
            pointd.SetLocation(base.xData.GetElement(index), this.yGroupData.GetElement(ngroup, index));
            return pointd;
        }

        public double[] GetDatasetColumnSum()
        {
            return this.GetGroupDatasetColumnSum().GetDataBuffer();
        }

        public override double GetDatasetMax(int naxis)
        {
            return this.GetGroupDatasetMax(naxis);
        }

        public override double GetDatasetMin(int naxis)
        {
            return this.GetGroupDatasetMin(naxis);
        }

        public GroupDataset GetFastClipDataset(string s, PhysicalCoordinates transform, int fastclipmode, ref int fastclipoffset)
        {
            double start;
            double stop;
            int n = 0;
            int num3 = 0;
            int num4 = 0;
            GroupDataset dataset = this;
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
            if (fastclipmode == 2)
            {
                return this.AutoCompressDataset(dataset, base.autoCompressDatasetModeX, base.autoCompressDatasetModeY, base.autoCompressTriggerValue, base.autoCompressDivisor);
            }
            if (fastclipmode != 0)
            {
                if (fastclipmode == 1)
                {
                    return dataset;
                }
            }
            else
            {
                int num2;
                for (num2 = 0; num2 < base.numberDatapoints; num2++)
                {
                    if (base.GetXDataValue(num2) >= start)
                    {
                        num3 = num2;
                        if (num3 > 0)
                        {
                            num3--;
                        }
                        break;
                    }
                }
                fastclipoffset = Math.Max(0, num3);
                num4 = base.numberDatapoints - 1;
                for (num2 = num3; num2 < base.numberDatapoints; num2++)
                {
                    if (base.GetXDataValue(num2) >= stop)
                    {
                        num4 = num2;
                        break;
                    }
                }
            }
            n = Math.Max(0, (num4 - num3) + 1);
            DoubleArray dest = new DoubleArray(n);
            DoubleArray2D arrayd = new DoubleArray2D(base.numberGroups, n);
            BoolArray array2 = new BoolArray(n);
            DoubleArray.CopyArray(base.xData, num3, dest, 0, n);
            for (int i = 0; i < base.numberGroups; i++)
            {
                DoubleArray2D.CopyArray(this.yGroupData, i, num3, arrayd, i, 0, n);
            }
            BoolArray.CopyArray(base.validData, num3, array2, 0, n);
            dataset = new GroupDataset(s, dest.GetElements(), arrayd.GetElements(), array2.GetElements());
            dataset = this.AutoCompressDataset(dataset, base.autoCompressDatasetModeX, base.autoCompressDatasetModeY, base.autoCompressTriggerValue, base.autoCompressDivisor);
            dataset.xCoordinateType = base.xCoordinateType;
            dataset.yCoordinateType = base.yCoordinateType;
            dest = null;
            arrayd = null;
            array2 = null;
            return dataset;
        }

        public int GetFirstValidIndex(int ngroup)
        {
            return ChartSupport.GetFirstValidIndex(base.GetXData(), this.GetGroupDataRow(ngroup), base.numberDatapoints);
        }

        public double GetGroupAverageY(int ngroup)
        {
            return (this.GetGroupSumY(ngroup) / ((double) base.numberDatapoints));
        }

        public double[,] GetGroupData()
        {
            return this.yGroupData.GetElements();
        }

        public double[] GetGroupDataColumn(int ncolumn)
        {
            return this.yGroupData.GetColumn(ncolumn);
        }

        public DoubleArray2D GetGroupDataObj()
        {
            return this.yGroupData;
        }

        public double[] GetGroupDataRow(int nrow)
        {
            return this.yGroupData.GetRow(nrow);
        }

        public DoubleArray GetGroupDatasetColumnSum()
        {
            int num;
            DoubleArray array = new DoubleArray(base.numberDatapoints);
            for (num = 0; num < base.numberDatapoints; num++)
            {
                array[num] = 0.0;
            }
            if (base.numberGroups > 0)
            {
                for (num = 0; num < base.numberDatapoints; num++)
                {
                    for (int i = 0; i < this.autoScaleNumberGroups; i++)
                    {
                        if (this.GetYGroupDataValue(i, num) != double.MaxValue)
                        {
                            DoubleArray array2;
                            int num3;
                            (array2 = array)[num3 = num] = array2[num3] + this.GetYGroupDataValue(i, num);
                        }
                    }
                }
            }
            return array;
        }

        public double GetGroupDatasetMax(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMaximum(base.xData, base.validData);
            }
            return ChartSupport.GetMaximum(this.yGroupData, base.validData, this.autoScaleNumberGroups);
        }

        public double GetGroupDatasetMin(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMinimum(base.xData, base.validData);
            }
            return ChartSupport.GetMinimum(this.yGroupData, base.validData, this.autoScaleNumberGroups);
        }

        public override double GetGroupDatasetSumMax(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMaximum(base.xData, base.validData);
            }
            return ChartSupport.GetMaximum(this.GetGroupDatasetColumnSum(), base.validData);
        }

        public override double GetGroupDatasetSumMin(int naxis)
        {
            if (naxis == 0)
            {
                return ChartSupport.GetMinimum(base.xData, base.validData);
            }
            return ChartSupport.GetMinimum(this.yGroupData, base.validData, 1);
        }

        public double GetGroupSumY(int ngroup)
        {
            double num = 0.0;
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                if (ChartSupport.BGoodValue(this.GetYGroupDataValue(ngroup, i)) && base.GetValidData(i))
                {
                    num += this.GetYGroupDataValue(ngroup, i);
                }
            }
            return num;
        }

        public override int GetNumberGroups()
        {
            return base.numberGroups;
        }

        public SimpleDataset GetSimpleDataset(string sname, int group)
        {
            double[] elements = base.xData.GetElements();
            return new SimpleDataset(sname, elements, this.yGroupData.GetRow(group));
        }

        public double[,] GetYData()
        {
            return this.GetGroupData();
        }

        public DoubleArray2D GetYDataObj()
        {
            return this.yGroupData;
        }

        public override double GetYDataValue(int ngroup, int index)
        {
            return this.GetYGroupDataValue(ngroup, index);
        }

        public double GetYGroupDataValue(int ngroup, int index)
        {
            return this.yGroupData.GetElement(ngroup, index);
        }

        public void InitDataset(string sname, double[] x, double[,] y)
        {
            this.InitDatasetBase(sname, y.GetLength(0), x.Length);
            this.InitializeData(x, y);
        }

        private void InitDatasetBase(string sname, int nrows, int ncols)
        {
            this.InitDefaults();
            base.numberDatapoints = ncols;
            base.numberGroups = nrows;
            this.autoScaleNumberGroups = base.numberGroups;
            base.dataName = sname;
            base.xData = new DoubleArray(base.numberDatapoints);
            this.yGroupData = new DoubleArray2D(base.numberGroups, base.numberDatapoints);
            base.validData = new BoolArray(base.numberDatapoints);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x44e;
        }

        public void InitializeData(double[] x, double[,] y)
        {
            base.numberDatapoints = Math.Min(Math.Min(x.Length, base.numberDatapoints), y.GetLength(1));
            base.xData.SetElements(x);
            this.yGroupData.SetElements(y);
            base.xData.SetLength(base.numberDatapoints);
            this.yGroupData.ResizeNumColumns(base.numberDatapoints);
            bool valid = false;
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                valid = true;
                for (int j = 0; j < base.numberGroups; j++)
                {
                    valid = valid && ChartSupport.BGoodValue(x[i], y[j, i]);
                }
                base.SetValidData(i, valid);
            }
        }

        public int InsertGroupDataPoints(GroupPoint2D xyvalue, int insertpoint)
        {
            base.numberDatapoints = base.xData.Insert(insertpoint, xyvalue.GetX());
            this.yGroupData.InsertColumn(insertpoint, xyvalue.GetY());
            base.validData.Insert(insertpoint, true);
            return base.numberDatapoints;
        }

        public int InsertGroupDataPoints(double xvalue, double[] yvalue, int insertpoint)
        {
            base.numberDatapoints = base.xData.Insert(insertpoint, xvalue);
            this.yGroupData.InsertColumn(insertpoint, yvalue);
            base.validData.Insert(insertpoint, true);
            return base.numberDatapoints;
        }

        public override bool IsDataPointGood(int index)
        {
            bool flag = false;
            flag = base.GetValidData(index) && ChartSupport.BGoodValue(base.GetXDataValue(index));
            for (int i = 0; i < base.numberGroups; i++)
            {
                flag = flag && ChartSupport.BGoodValue(this.GetYGroupDataValue(i, index));
            }
            return flag;
        }

        public bool IsDataPointGood(int ngroup, int index)
        {
            return ChartSupport.BGoodValue(base.GetXDataValue(index), this.GetYGroupDataValue(ngroup, index));
        }

        public virtual void ReadGroupDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            StreamReader pReader = null;
            double[] numArray;
            double[,] numArray2;
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
                numArray2 = new double[num - 1, num2];
            }
            else
            {
                if ((num < 1) || (num2 < 2))
                {
                    this.ErrorCheck(540);
                    return;
                }
                numArray = new double[num];
                numArray2 = new double[num2 - 1, num];
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
                        for (num4 = 0; num4 < (num - 1); num4++)
                        {
                            numArray2[num4, num3] = csv.ReadDouble(pReader);
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
                        numArray[num3] = csv.ReadDouble(pReader);
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
                            numArray2[num3, num4] = csv.ReadDouble(pReader);
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
                this.InitDataset(filename, numArray, numArray2);
            }
            else
            {
                this.InitDataset(filename, numArray, numArray2);
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

        public void Resize(int n)
        {
            base.numberDatapoints = base.xData.Resize(n);
            this.yGroupData.ResizeNumColumns(n);
            base.validData.Resize(n);
        }

        public void SetAutoScaleNumberGroups(int autoscalenumgroups)
        {
            this.autoScaleNumberGroups = autoscalenumgroups;
        }

        public void SetDataPoint(int index, Point2D p, int ngroup)
        {
            base.SetXDataValue(index, p.GetX());
            this.SetYGroupDataValue(ngroup, index, p.GetY());
        }

        public void SetDataPoint(int index, double x, double y, int ngroup)
        {
            base.SetXDataValue(index, x);
            this.SetYGroupDataValue(ngroup, index, y);
        }

        public void SetGroupData(double[,] ygroup)
        {
            this.yGroupData.SetElements(ygroup);
        }

        public void SetGroupDataColumn(double[] y, int ncolumn)
        {
            this.yGroupData.SetColumn(ncolumn, y);
        }

        public void SetGroupDataColumn(double x, double[] y, int ncolumn)
        {
            base.xData.SetElement(ncolumn, x);
            this.yGroupData.SetColumn(ncolumn, y);
        }

        public void SetGroupDataElement(double y, int nrow, int ncolumn)
        {
            this.yGroupData.SetElement(nrow, ncolumn, y);
        }

        public void SetGroupDataRow(double[] y, int nrow)
        {
            this.yGroupData.SetRow(nrow, y);
        }

        public override void SetYDataValue(int ngroup, int index, double y)
        {
            this.SetYGroupDataValue(ngroup, index, y);
        }

        public void SetYGroupDataValue(int ngroup, int index, double y)
        {
            this.yGroupData.SetElement(ngroup, index, y);
        }

        public void SortByX(bool ascending)
        {
            ChartDataset.DatasetSortClass[] classArray = new ChartDataset.DatasetSortClass[base.numberDatapoints];
            int index = 0;
            int row = 0;
            int col = 0;
            DoubleArray source = new DoubleArray(base.numberDatapoints);
            DoubleArray2D arrayd = new DoubleArray2D(base.numberGroups, base.numberDatapoints);
            BoolArray array2 = new BoolArray(base.numberDatapoints);
            for (index = 0; index < base.numberDatapoints; index++)
            {
                classArray[index] = new ChartDataset.DatasetSortClass(index, base.GetXDataValue(index));
            }
            Array.Sort(classArray);
            for (index = 0; index < base.numberDatapoints; index++)
            {
                if (ascending)
                {
                    col = classArray[index].index;
                }
                else
                {
                    col = classArray[(base.numberDatapoints - index) - 1].index;
                }
                source[index] = base.xData[col];
                for (row = 0; row < base.numberGroups; row++)
                {
                    arrayd[row][index] = this.yGroupData.GetElement(row, col);
                }
                array2[index] = base.validData[col];
            }
            base.xData.Copy(source);
            this.yGroupData.Copy(arrayd);
            base.validData.Copy(array2);
        }

        public virtual void WriteGroupDataset(CSV csv, string filename)
        {
            this.WriteGroupDataset(csv, filename, false);
        }

        public virtual void WriteGroupDataset(CSV csv, string filename, bool append)
        {
            StreamWriter pWriter = null;
            int numberDatapoints = base.GetNumberDatapoints();
            int numberGroups = this.GetNumberGroups();
            double[] elements = base.xData.GetElements();
            double[,] numArray2 = this.yGroupData.GetElements();
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
                    csv.WriteDouble(pWriter, elements[k]);
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

        public GroupPoint2D this[int index]
        {
            get
            {
                return new GroupPoint2D(base.xData.GetElement(index), this.yGroupData.GetColumn(index));
            }
            set
            {
                this.yGroupData.SetColumn(index, value.GetY());
                base.xData.SetElement(index, value.GetX());
            }
        }

        public DoubleArray2D YGroupData
        {
            get
            {
                return this.yGroupData;
            }
            set
            {
                this.yGroupData = value;
            }
        }
    }
}

