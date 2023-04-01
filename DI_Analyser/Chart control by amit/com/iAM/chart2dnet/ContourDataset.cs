namespace com.iAM.chart2dnet
{
    using System;
    using System.IO;
    using System.Reflection;

    public class ContourDataset : SimpleDataset
    {
        private Polysurface datasetPolysurface;
        internal DoubleArray zData;

        public ContourDataset()
        {
            this.zData = new DoubleArray();
            this.datasetPolysurface = null;
            this.InitDefaults();
        }

        public ContourDataset(string sname, Point3D[] grid)
        {
            this.zData = new DoubleArray();
            this.datasetPolysurface = null;
            this.InitDataset(sname, grid);
            this.datasetPolysurface = new Polysurface(grid, grid.Length);
        }

        public ContourDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            this.zData = new DoubleArray();
            this.datasetPolysurface = null;
            this.ReadContourDataset(csv, filename, rowskip, columnskip);
        }

        public ContourDataset(string sname, Point3D[] grid, int rows, int columns)
        {
            this.zData = new DoubleArray();
            this.datasetPolysurface = null;
            this.InitDataset(sname, grid);
            this.datasetPolysurface = new Polysurface(grid, rows, columns, 1);
        }

        public ContourDataset(string sname, double[] x, double[] y, double[] z)
        {
            this.zData = new DoubleArray();
            this.datasetPolysurface = null;
            this.InitDataset(sname, x, y, z);
            this.datasetPolysurface = new Polysurface(x, y, z, x.Length);
        }

        public ContourDataset(string sname, int rows, int columns, double x1, double y1, double x2, double y2, SurfaceFunction sf)
        {
            this.zData = new DoubleArray();
            this.datasetPolysurface = null;
            this.datasetPolysurface = new Polysurface(rows, columns, x1, y1, x2, y2, sf);
            this.InitDataset(sname, this.datasetPolysurface.GetPolysurfacePointList());
        }

        public override bool CalcNearestPoint(PhysicalCoordinates transform, Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(transform, this, false, testpoint, nmode, nearestpoint);
        }

        public override object Clone()
        {
            ContourDataset dataset = new ContourDataset();
            dataset.Copy(this);
            return dataset;
        }

        public virtual ContourDataset CompressContourDataset(int ctypex, int ctypey, int ctypez, int interval, int startindex, int endindex, string newname)
        {
            DoubleArray array = null;
            DoubleArray array2 = null;
            DoubleArray array3 = null;
            ContourDataset dataset = null;
            BoolArray validflags = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                validflags[i] = this.IsDataPointGood(i);
            }
            array = ChartSupport.MakeCompressArray(ctypex, interval, startindex, endindex, base.xData, validflags);
            array2 = ChartSupport.MakeCompressArray(ctypey, interval, startindex, endindex, base.yData, validflags);
            array3 = ChartSupport.MakeCompressArray(ctypez, interval, startindex, endindex, this.zData, validflags);
            if (Math.Min(array.Length, array2.Length) <= 0)
            {
                dataset = (ContourDataset) this.Clone();
                dataset.SetDataName(newname);
            }
            else
            {
                dataset = new ContourDataset(newname, array.GetElements(), array2.GetElements(), array3.GetElements());
            }
            if (dataset != null)
            {
                dataset.MarkBadDataInvalid();
            }
            return dataset;
        }

        public virtual void Copy(ContourDataset source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.zData = (DoubleArray) source.zData.Clone();
                this.datasetPolysurface = (Polysurface) source.datasetPolysurface.Clone();
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.zData == null)
                {
                    nerror = 530;
                }
                else if (this.datasetPolysurface == null)
                {
                    nerror = 530;
                }
                else
                {
                    nerror = this.datasetPolysurface.ErrorCheck(0);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public Point3D GetDataPoint3D(int index)
        {
            return new Point3D(base.xData.GetElement(index), base.yData.GetElement(index), this.zData.GetElement(index));
        }

        public override double GetDatasetMax(int naxis)
        {
            double maximum = 0.0;
            if (naxis == 0)
            {
                return ChartSupport.GetMaximum(base.xData, base.validData);
            }
            if (naxis == 1)
            {
                return ChartSupport.GetMaximum(base.yData, base.validData);
            }
            if (naxis == 2)
            {
                maximum = ChartSupport.GetMaximum(this.zData, base.validData);
            }
            return maximum;
        }

        public override double GetDatasetMin(int naxis)
        {
            double minimum = 0.0;
            if (naxis == 0)
            {
                return ChartSupport.GetMinimum(base.xData, base.validData);
            }
            if (naxis == 1)
            {
                return ChartSupport.GetMinimum(base.yData, base.validData);
            }
            if (naxis == 2)
            {
                minimum = ChartSupport.GetMinimum(this.zData, base.validData);
            }
            return minimum;
        }

        public override int GetFirstValidIndex()
        {
            return ChartSupport.GetFirstValidIndex(base.GetXData(), base.GetYData(), base.numberDatapoints);
        }

        public override int GetNumberGroups()
        {
            return 1;
        }

        public virtual Polysurface GetPolysurface()
        {
            return this.datasetPolysurface;
        }

        public virtual double[] GetZData()
        {
            return this.zData.DataBuffer;
        }

        public virtual DoubleArray GetZDataObj()
        {
            return this.zData;
        }

        public virtual double GetZDataValue(int index)
        {
            return this.zData.GetElement(index);
        }

        protected virtual void InitDataset(string sname, Point3D[] points)
        {
            this.InitDefaults();
            base.numberDatapoints = points.Length;
            base.dataName = sname;
            base.xData = new DoubleArray(base.numberDatapoints);
            base.yData = new DoubleArray(base.numberDatapoints);
            this.zData = new DoubleArray(base.numberDatapoints);
            base.validData = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.xData.SetElement(i, points[i].GetX());
                base.yData.SetElement(i, points[i].GetY());
                this.zData.SetElement(i, points[i].GetZ());
                base.validData[i] = ChartSupport.BGoodValue(base.xData[i], base.yData[i], this.zData[i]);
            }
        }

        protected virtual void InitDataset(string sname, double[] x, double[] y, double[] z)
        {
            this.InitDefaults();
            base.numberDatapoints = x.Length;
            base.dataName = sname;
            base.xData = new DoubleArray(x);
            base.yData = new DoubleArray(y);
            this.zData = new DoubleArray(z);
            base.validData = new BoolArray(base.numberDatapoints);
            for (int i = 0; i < base.numberDatapoints; i++)
            {
                base.validData.SetElement(i, ChartSupport.BGoodValue(x[i], y[i], z[i]));
            }
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x451;
        }

        public override bool IsDataPointGood(int index)
        {
            return (ChartSupport.BGoodValue(base.xData.GetElement(index), base.yData.GetElement(index), this.zData.GetElement(index)) && base.validData.GetElement(index));
        }

        public virtual void ReadContourDataset(CSV csv, string filename, int rowskip, int columnskip)
        {
            StreamReader pReader = null;
            double[] numArray;
            double[] numArray2;
            double[] numArray3;
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
                if ((num < 3) || (num2 < 4))
                {
                    this.ErrorCheck(540);
                    return;
                }
                numArray = new double[num2];
                numArray2 = new double[num2];
                numArray3 = new double[num2];
            }
            else
            {
                if ((num < 4) || (num2 < 3))
                {
                    this.ErrorCheck(540);
                    return;
                }
                numArray = new double[num];
                numArray2 = new double[num];
                numArray3 = new double[num];
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
                        numArray3[num3] = csv.ReadDouble(pReader);
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
                    for (num4 = 0; num4 < columnskip; num4++)
                    {
                        csv.Read(pReader);
                    }
                    for (num3 = 0; num3 < num; num3++)
                    {
                        numArray3[num3] = csv.ReadDouble(pReader);
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
                this.InitDataset(filename, numArray, numArray2, numArray3);
            }
            else
            {
                this.InitDataset(filename, numArray, numArray2, numArray);
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

        public override int Resize(int n)
        {
            base.numberDatapoints = base.xData.Resize(n);
            base.yData.Resize(n);
            this.zData.Resize(n);
            base.validData.Resize(n);
            return base.numberDatapoints;
        }

        public virtual void SetDataPoint(int index, Point3D p)
        {
            base.xData.SetElement(index, p.GetX());
            base.yData.SetElement(index, p.GetY());
            this.zData.SetElement(index, p.GetZ());
        }

        public virtual void SetDataPoint(int index, double x, double y, double z)
        {
            base.xData.SetElement(index, x);
            base.yData.SetElement(index, y);
            this.zData.SetElement(index, z);
        }

        public virtual void SetZDataValue(double z, int index)
        {
            this.zData.SetElement(index, z);
        }

        public virtual void WriteContourDataset(CSV csv, string filename)
        {
            StreamWriter pWriter = null;
            int numberDatapoints = base.GetNumberDatapoints();
            double[] elements = base.xData.GetElements();
            double[] numArray2 = base.yData.GetElements();
            double[] numArray3 = this.zData.GetElements();
            base.validData.GetElements();
            try
            {
                pWriter = new StreamWriter(filename);
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
                    csv.WriteDouble(pWriter, numArray3[i]);
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
                for (int m = 0; m < numberDatapoints; m++)
                {
                    csv.WriteDouble(pWriter, numArray3[m]);
                }
                csv.Writeln(pWriter);
            }
            pWriter.Close();
        }

        public Point3D this[int index]
        {
            get
            {
                return this.GetDataPoint3D(index);
            }
            set
            {
                this.SetDataPoint(index, value);
            }
        }

        public DoubleArray ZData
        {
            get
            {
                return this.zData;
            }
            set
            {
                this.zData = value;
            }
        }
    }
}

