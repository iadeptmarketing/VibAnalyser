namespace com.iAM.chart2dnet
{
    using System;
    using System.Reflection;

    public class DoubleArray2D
    {
        private DoubleArray[] dataBuffer;
        private const int minimumRowCapacity = 2;
        private int numberRows;
        private int rowCapacity;

        public DoubleArray2D()
        {
            this.dataBuffer = new DoubleArray[8];
            this.rowCapacity = 2;
            this.numberRows = 0;
        }

        public DoubleArray2D(double[,] rc)
        {
            this.dataBuffer = new DoubleArray[8];
            this.rowCapacity = 2;
            this.numberRows = 0;
            int length = rc.GetLength(1);
            int num2 = rc.GetLength(0);
            int maxcap = length;
            double[] dest = new double[length];
            this.rowCapacity = Math.Max(num2, 2);
            this.numberRows = num2;
            this.dataBuffer = new DoubleArray[this.rowCapacity];
            for (int i = 0; i < this.numberRows; i++)
            {
                CopyArray(rc, i * length, dest, 0, length);
                this.dataBuffer[i] = new DoubleArray(dest, maxcap);
            }
        }

        public DoubleArray2D(int rows, int columns)
        {
            this.dataBuffer = new DoubleArray[8];
            this.rowCapacity = 2;
            this.numberRows = 0;
            this.rowCapacity = Math.Max(2, rows);
            this.numberRows = rows;
            this.dataBuffer = new DoubleArray[this.rowCapacity];
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i] = new DoubleArray(columns);
            }
        }

        public DoubleArray2D(double[,] rc, int colmaxcap)
        {
            this.dataBuffer = new DoubleArray[8];
            this.rowCapacity = 2;
            this.numberRows = 0;
            int length = rc.GetLength(1);
            int num2 = rc.GetLength(0);
            int maxcap = Math.Max(length, colmaxcap);
            double[] dest = new double[length];
            this.rowCapacity = Math.Max(num2, 2);
            this.dataBuffer = new DoubleArray[this.rowCapacity];
            this.numberRows = num2;
            for (int i = 0; i < this.numberRows; i++)
            {
                CopyArray(rc, i * length, dest, 0, length);
                this.dataBuffer[i] = new DoubleArray(dest, maxcap);
            }
        }

        public int AddColumn(double[] r)
        {
            int length = 0;
            if (this.numberRows < r.Length)
            {
                this.ResizeNumRows(r.Length);
            }
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].Add(r[i]);
                length = this.dataBuffer[i].Length;
            }
            return length;
        }

        public int AddColumn(DoubleArray r)
        {
            int length = 0;
            if (this.numberRows < r.Length)
            {
                this.ResizeNumRows(r.Length);
            }
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].Add(r[i]);
                length = this.dataBuffer[i].Length;
            }
            return length;
        }

        public int AddColumnRange(double[,] rc)
        {
            int length = rc.GetLength(1);
            rc.GetLength(0);
            int num2 = 0;
            double[] dest = new double[length];
            for (int i = 0; i < this.numberRows; i++)
            {
                CopyArray(rc, i * length, dest, 0, length);
                this.dataBuffer[i].AddRange(dest);
                num2 = this.dataBuffer[i].Length;
            }
            return num2;
        }

        public int AddRow(double[] r)
        {
            this.ResizeNumRows(this.numberRows + 1);
            if (this.numberRows == 1)
            {
                this.ResizeNumColumns(r.Length);
            }
            this.SetRow(this.numberRows - 1, r);
            return this.numberRows;
        }

        public int AddRow(DoubleArray r)
        {
            this.ResizeNumRows(this.numberRows + 1);
            this.SetRow(this.numberRows - 1, r);
            return this.numberRows;
        }

        public void Clear()
        {
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].Clear();
            }
            this.ResizeRowCapacity(2);
            this.numberRows = 0;
        }

        public object Clone()
        {
            DoubleArray2D arrayd = new DoubleArray2D();
            arrayd.Copy(this);
            return arrayd;
        }

        public void Copy(DoubleArray2D source)
        {
            if (source != null)
            {
                this.rowCapacity = source.rowCapacity;
                this.numberRows = source.numberRows;
                this.dataBuffer = new DoubleArray[this.rowCapacity];
                for (int i = 0; i < this.numberRows; i++)
                {
                    this.dataBuffer[i] = new DoubleArray(source.NumColumns);
                    this.dataBuffer[i].Copy(source.dataBuffer[i]);
                }
            }
        }

        private static void CopyArray(Array source, int sourceoffset, Array dest, int destoffset, int count)
        {
            int sizeOfDouble = ChartObj.sizeOfDouble;
            Buffer.BlockCopy(source, sizeOfDouble * sourceoffset, dest, sizeOfDouble * destoffset, sizeOfDouble * count);
        }

        public static void CopyArray(DoubleArray source, int sourceoffset, DoubleArray2D dest, int destrow, int destoffset, int count)
        {
            DoubleArray.CopyArray(source, sourceoffset, dest.dataBuffer[destrow], destoffset, count);
        }

        public static void CopyArray(DoubleArray2D source, int sourcerow, int sourceoffset, DoubleArray dest, int destoffset, int count)
        {
            DoubleArray.CopyArray(source.dataBuffer[sourcerow], sourceoffset, dest, destoffset, count);
        }

        public static void CopyArray(DoubleArray2D source, int sourcerow, int sourceoffset, DoubleArray2D dest, int destrow, int destoffset, int count)
        {
            DoubleArray.CopyArray(source.DataBuffer[sourcerow], sourceoffset, dest.dataBuffer[destrow], destoffset, count);
        }

        public int DeleteColumn(int index)
        {
            return this.RemoveColumnAt(index);
        }

        public int DeleteRow(int index)
        {
            return this.RemoveRowAt(index);
        }

        public double[] GetColumn(int column)
        {
            int numberRows = 0;
            double[] numArray = null;
            if (((this.numberRows > 0) && (column < this.dataBuffer[0].Length)) && (column >= 0))
            {
                numberRows = this.numberRows;
                numArray = new double[numberRows];
                for (int i = 0; i < numberRows; i++)
                {
                    numArray[i] = this.GetElement(i, column);
                }
            }
            return numArray;
        }

        public double GetElement(int row, int col)
        {
            double num = 0.0;
            if (((row < this.numberRows) && (col < this.GetNumColumns())) && ((row >= 0) && (col >= 0)))
            {
                num = this.dataBuffer[row].DataBuffer[col];
            }
            return num;
        }

        public double[,] GetElements()
        {
            int numRows = this.NumRows;
            int numColumns = this.NumColumns;
            int length = numColumns;
            double[] destinationArray = new double[length];
            double[,] dest = new double[numRows, numColumns];
            for (int i = 0; i < numRows; i++)
            {
                Array.Copy(this.dataBuffer[i].DataBuffer, 0, destinationArray, 0, length);
                CopyArray(destinationArray, 0, dest, i * numColumns, length);
            }
            return dest;
        }

        public int GetLength(int dim)
        {
            int numColumns = 0;
            if (dim == 0)
            {
                return this.NumRows;
            }
            if (dim == 1)
            {
                numColumns = this.NumColumns;
            }
            return numColumns;
        }

        public int GetNumColumns()
        {
            int length = 0;
            if (this.numberRows > 0)
            {
                length = this.dataBuffer[0].Length;
            }
            return length;
        }

        public int GetNumRows()
        {
            return this.numberRows;
        }

        public double[] GetRow(int row)
        {
            int length = 0;
            double[] destinationArray = null;
            if ((row < this.numberRows) && (row >= 0))
            {
                length = this.dataBuffer[row].Length;
                destinationArray = new double[length];
                Array.Copy(this.dataBuffer[row].DataBuffer, 0, destinationArray, 0, length);
            }
            return destinationArray;
        }

        public DoubleArray GetRowObject(int row)
        {
            DoubleArray array = null;
            if ((row < this.numberRows) && (row >= 0))
            {
                array = this.dataBuffer[row];
            }
            return array;
        }

        public int InsertColumn(int index, double[] r)
        {
            int length = 0;
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].Insert(index, r[i]);
                length = this.dataBuffer[i].Length;
            }
            return length;
        }

        public int InsertColumn(int index, DoubleArray r)
        {
            int length = 0;
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].Insert(index, r[i]);
                length = this.dataBuffer[i].Length;
            }
            return length;
        }

        public int InsertRow(int index, double[] r)
        {
            if ((index >= 0) && (index < this.numberRows))
            {
                this.ResizeNumRows(this.numberRows + 1);
                for (int i = this.numberRows - 1; i > index; i--)
                {
                    this.dataBuffer[i].NDCopy(this.dataBuffer[i - 1]);
                }
                this.SetRow(index, r);
            }
            return this.numberRows;
        }

        public int InsertRow(int index, DoubleArray r)
        {
            if ((index >= 0) && (index < this.numberRows))
            {
                this.ResizeNumRows(this.numberRows + 1);
                for (int i = this.numberRows - 1; i > index; i--)
                {
                    this.dataBuffer[i].NDCopy(this.dataBuffer[i - 1]);
                }
                this.SetRow(index, r);
            }
            return this.numberRows;
        }

        public int RemoveColumnAt(int index)
        {
            int num = 0;
            for (int i = 0; i < this.numberRows; i++)
            {
                num = this.dataBuffer[i].RemoveAt(index);
            }
            return num;
        }

        public int RemoveRowAt(int index)
        {
            if ((index >= 0) && (index < this.numberRows))
            {
                for (int i = index; i < (this.numberRows - 1); i++)
                {
                    this.dataBuffer[i].NDCopy(this.dataBuffer[i + 1]);
                }
                this.ResizeNumRows(this.numberRows - 1);
            }
            return this.numberRows;
        }

        public void Reset()
        {
            this.Clear();
        }

        public void ResizeColumnCapacity(int newcolcapacity)
        {
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].ResizeCapacity(newcolcapacity);
            }
        }

        public int ResizeNumColumns(int newnumcols)
        {
            int length = 0;
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].SetLength(newnumcols);
                length = this.dataBuffer[i].Length;
            }
            return length;
        }

        public int ResizeNumRows(int newlength)
        {
            if (newlength >= 0)
            {
                if (newlength > this.rowCapacity)
                {
                    do
                    {
                        this.ResizeRowCapacity(this.rowCapacity * 2);
                    }
                    while (this.rowCapacity < newlength);
                }
                if (newlength > this.numberRows)
                {
                    for (int i = this.numberRows; i < newlength; i++)
                    {
                        this.dataBuffer[i] = new DoubleArray(this.NumColumns);
                    }
                }
                else if (newlength < this.numberRows)
                {
                    for (int j = newlength; j < this.numberRows; j++)
                    {
                        this.dataBuffer[j] = null;
                    }
                }
                this.numberRows = newlength;
            }
            return this.numberRows;
        }

        public void ResizeRowCapacity(int newcapacity)
        {
            newcapacity = Math.Max(newcapacity, 2);
            this.rowCapacity = newcapacity;
            DoubleArray[] arrayArray = new DoubleArray[this.rowCapacity];
            if (this.rowCapacity <= this.numberRows)
            {
                this.numberRows = this.rowCapacity;
            }
            for (int i = 0; i < this.numberRows; i++)
            {
                arrayArray[i] = new DoubleArray();
                arrayArray[i].Copy(this.dataBuffer[i]);
            }
            this.dataBuffer = arrayArray;
        }

        public void SetColumn(int column, double[] source)
        {
            int num = 0;
            if (((this.numberRows > 0) && (column < this.dataBuffer[0].Length)) && (column >= 0))
            {
                num = Math.Min(source.Length, this.numberRows);
                for (int i = 0; i < num; i++)
                {
                    this.SetElement(i, column, source[i]);
                }
            }
        }

        public void SetColumn(int column, DoubleArray source)
        {
            int num = 0;
            if (((this.numberRows > 0) && (column < this.dataBuffer[0].Length)) && (column >= 0))
            {
                num = Math.Min(source.Length, this.numberRows);
                for (int i = 0; i < num; i++)
                {
                    this.SetElement(i, column, source[i]);
                }
            }
        }

        public void SetElement(int row, int col, double r)
        {
            if (((row < this.numberRows) && (col < this.GetNumColumns())) && ((row >= 0) && (col >= 0)))
            {
                this.dataBuffer[row].DataBuffer[col] = r;
            }
        }

        public void SetElements(DoubleArray2D source)
        {
            int numRows = source.NumRows;
            int numColumns = source.NumColumns;
            this.ResizeNumRows(numRows);
            this.ResizeNumColumns(numColumns);
            int count = Math.Min(numColumns, this.NumColumns);
            for (int i = 0; i < this.NumRows; i++)
            {
                DoubleArray.CopyArray(source.dataBuffer[i], 0, this.dataBuffer[i], 0, count);
            }
        }

        public void SetElements(double[,] source)
        {
            int length = source.GetLength(0);
            int newnumcols = source.GetLength(1);
            this.ResizeNumRows(length);
            this.ResizeNumColumns(newnumcols);
            int count = Math.Min(newnumcols, this.NumColumns);
            double[] dest = new double[count];
            for (int i = 0; i < this.NumRows; i++)
            {
                CopyArray(source, i * newnumcols, dest, 0, count);
                Array.Copy(dest, 0, this.dataBuffer[i].DataBuffer, 0, count);
            }
        }

        public void SetRow(int row, double[] source)
        {
            int length = 0;
            if ((row < this.numberRows) && (row >= 0))
            {
                length = Math.Min(source.Length, this.dataBuffer[row].Length);
                Array.Copy(source, 0, this.dataBuffer[row].DataBuffer, 0, length);
            }
        }

        public void SetRow(int row, DoubleArray source)
        {
            if ((row < this.numberRows) && (row >= 0))
            {
                this.dataBuffer[row].NDCopy(source);
            }
        }

        public void ShiftLeft(int shiftcount, bool fillzero)
        {
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].ShiftLeft(shiftcount, fillzero);
            }
        }

        public int ShiftLeftThenResize(int shiftcount, bool trim)
        {
            int num = 0;
            for (int i = 0; i < this.numberRows; i++)
            {
                num = this.dataBuffer[i].ShiftLeftThenResize(shiftcount, trim);
            }
            return num;
        }

        public void ShiftRight(int shiftcount, bool fillzero)
        {
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].ShiftRight(shiftcount, fillzero);
            }
        }

        public void TrimToSize()
        {
            this.ResizeRowCapacity(this.numberRows);
            for (int i = 0; i < this.numberRows; i++)
            {
                this.dataBuffer[i].TrimToSize();
            }
        }

        public DoubleArray[] DataBuffer
        {
            get
            {
                return this.dataBuffer;
            }
            set
            {
                this.dataBuffer = value;
            }
        }

        public DoubleArray this[int index]
        {
            get
            {
                return this.GetRowObject(index);
            }
        }

        public int Length
        {
            get
            {
                return (this.NumRows * this.NumColumns);
            }
        }

        public int NumColumns
        {
            get
            {
                return this.GetNumColumns();
            }
        }

        public int NumRows
        {
            get
            {
                return this.GetNumRows();
            }
        }
    }
}

