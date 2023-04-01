namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Reflection;

    public class BoolArray
    {
        private bool[] dataBuffer;
        private int length;
        private int maxCapacity;
        internal const int minimumMaxCapacity = 8;

        public BoolArray()
        {
            this.dataBuffer = new bool[8];
            this.maxCapacity = 8;
            this.length = 0;
        }

        public BoolArray(int n)
        {
            this.dataBuffer = new bool[8];
            this.maxCapacity = 8;
            this.length = 0;
            n = Math.Max(n, 0);
            this.maxCapacity = Math.Max(n, 8);
            this.length = n;
            this.dataBuffer = new bool[this.maxCapacity];
            this.InitializeArray(true);
        }

        public BoolArray(bool[] x)
        {
            this.dataBuffer = new bool[8];
            this.maxCapacity = 8;
            this.length = 0;
            this.maxCapacity = Math.Max(x.Length, 8);
            this.length = x.Length;
            this.dataBuffer = new bool[this.maxCapacity];
            Array.Copy(x, 0, this.dataBuffer, 0, this.length);
        }

        public BoolArray(bool[] x, int maxcap)
        {
            this.dataBuffer = new bool[8];
            this.maxCapacity = 8;
            this.length = 0;
            maxcap = Math.Max(maxcap, 8);
            this.maxCapacity = Math.Max(x.Length, maxcap);
            this.length = Math.Min(x.Length, maxcap);
            this.dataBuffer = new bool[this.maxCapacity];
            Array.Copy(x, 0, this.dataBuffer, 0, this.length);
        }

        public int Add(bool r)
        {
            this.SetLength(this.length + 1);
            this.dataBuffer[this.length - 1] = r;
            return this.length;
        }

        public int AddRange(bool[] x)
        {
            int length = this.length;
            this.SetLength(this.length + x.Length);
            Array.Copy(x, 0, this.dataBuffer, length, x.Length);
            return this.length;
        }

        public void Clear()
        {
            this.maxCapacity = 8;
            this.length = 0;
            this.dataBuffer = null;
            this.dataBuffer = new bool[this.maxCapacity];
        }

        public object Clone()
        {
            BoolArray array = new BoolArray();
            array.Copy(this);
            return array;
        }

        public void Copy(BoolArray source)
        {
            if (source != null)
            {
                this.maxCapacity = source.maxCapacity;
                this.length = source.length;
                this.dataBuffer = new bool[this.maxCapacity];
                Array.Copy(source.dataBuffer, 0, this.dataBuffer, 0, this.length);
            }
        }

        public static void CopyArray(BoolArray source, int sourceoffset, BoolArray dest, int destoffset, int count)
        {
            if ((source != null) && (dest != null))
            {
                Array.Copy(source.DataBuffer, sourceoffset, dest.DataBuffer, destoffset, count);
            }
        }

        public void CopyTo(Array dest, int index)
        {
            if (index >= 0)
            {
                int length = Math.Min(this.Length, dest.Length - index);
                if (dest != null)
                {
                    Array.Copy(this.dataBuffer, 0, dest, index, length);
                }
            }
        }

        public int Delete(int index)
        {
            return this.RemoveAt(index);
        }

        public bool[] GetDataBuffer()
        {
            return this.dataBuffer;
        }

        public bool GetElement(int index)
        {
            bool flag = false;
            if ((index >= 0) && (index < this.length))
            {
                flag = this.dataBuffer[index];
            }
            return flag;
        }

        public bool[] GetElements()
        {
            bool[] destinationArray = new bool[this.length];
            Array.Copy(this.dataBuffer, 0, destinationArray, 0, this.length);
            return destinationArray;
        }

        private void InitializeArray(bool value)
        {
            for (int i = 0; i < this.length; i++)
            {
                this.dataBuffer[i] = value;
            }
        }

        public int Insert(int index, bool r)
        {
            if ((index >= 0) && (index < this.length))
            {
                this.SetLength(this.length + 1);
                for (int i = this.length - 1; i > index; i--)
                {
                    this.dataBuffer[i] = this.dataBuffer[i - 1];
                }
                this.dataBuffer[index] = r;
            }
            return this.length;
        }

        public void NDCopy(BoolArray source)
        {
            if (source != null)
            {
                int length = Math.Min(this.Length, source.Length);
                Array.Copy(source.DataBuffer, 0, this.dataBuffer, 0, length);
            }
        }

        public int RemoveAt(int index)
        {
            if ((index >= 0) && (index < this.length))
            {
                for (int i = index; i < (this.length - 1); i++)
                {
                    this.dataBuffer[i] = this.dataBuffer[i + 1];
                }
                this.SetLength(this.length - 1);
            }
            return this.length;
        }

        public void Reset()
        {
            this.Clear();
        }

        public int Resize(int newlength)
        {
            return this.SetLength(newlength);
        }

        public void ResizeCapacity(int newcapacity)
        {
            newcapacity = Math.Max(newcapacity, 8);
            this.maxCapacity = newcapacity;
            bool[] destinationArray = new bool[this.maxCapacity];
            if (this.maxCapacity <= this.length)
            {
                this.length = this.maxCapacity;
            }
            Array.Copy(this.dataBuffer, 0, destinationArray, 0, this.length);
            this.dataBuffer = null;
            this.dataBuffer = destinationArray;
        }

        public void SetElement(int index, bool r)
        {
            if (index == this.length)
            {
                this.Add(r);
            }
            else if ((index >= 0) && (index < this.length))
            {
                this.dataBuffer[index] = r;
            }
        }

        public int SetElements(BoolArray source)
        {
            return this.SetElements(source.DataBuffer, source.Length);
        }

        public int SetElements(bool[] source)
        {
            int length = source.Length;
            if (length > this.Length)
            {
                this.SetLength(length);
            }
            Array.Copy(source, 0, this.dataBuffer, 0, length);
            this.length = length;
            return this.length;
        }

        public int SetElements(bool[] source, int count)
        {
            int newlength = Math.Min(count, source.Length);
            if (newlength > this.Length)
            {
                this.SetLength(newlength);
            }
            Array.Copy(source, 0, this.dataBuffer, 0, newlength);
            this.length = newlength;
            return this.length;
        }

        public int SetElements(BoolArray source, int count)
        {
            return this.SetElements(source.DataBuffer, count);
        }

        public int SetLength(int newlength)
        {
            if (newlength >= 0)
            {
                if (newlength > this.maxCapacity)
                {
                    do
                    {
                        this.maxCapacity *= 2;
                    }
                    while (this.maxCapacity < newlength);
                    this.ResizeCapacity(this.maxCapacity);
                }
                this.length = newlength;
            }
            return this.length;
        }

        public void ShiftLeft(int shiftcount, bool fillzero)
        {
            if ((shiftcount >= 0) && (shiftcount < this.length))
            {
                int num;
                for (num = 0; num < (this.length - shiftcount); num++)
                {
                    this.dataBuffer[num] = this.dataBuffer[shiftcount + num];
                }
                if (fillzero)
                {
                    for (num = this.length - shiftcount; num < this.length; num++)
                    {
                        this.dataBuffer[num] = true;
                    }
                }
            }
        }

        public int ShiftLeftThenResize(int shiftcount, bool trim)
        {
            if ((shiftcount >= 0) && (shiftcount < this.length))
            {
                this.ShiftLeft(shiftcount, false);
                this.Resize(this.length - shiftcount);
                if (trim)
                {
                    this.TrimToSize();
                }
            }
            return this.length;
        }

        public void ShiftRight(int shiftcount, bool fillzero)
        {
            if ((shiftcount >= 0) && (shiftcount < this.length))
            {
                int num;
                for (num = this.length - 1; num >= shiftcount; num--)
                {
                    this.dataBuffer[num] = this.dataBuffer[num - shiftcount];
                }
                if (fillzero)
                {
                    for (num = 0; num < shiftcount; num++)
                    {
                        this.dataBuffer[num] = true;
                    }
                }
            }
        }

        public void TrimToSize()
        {
            this.ResizeCapacity(this.length);
        }

        public bool[] DataBuffer
        {
            get
            {
                return this.dataBuffer;
            }
        }

        public bool this[int index]
        {
            get
            {
                return this.GetElement(index);
            }
            set
            {
                this.SetElement(index, value);
            }
        }

        public int Length
        {
            get
            {
                return this.length;
            }
        }

        public int MaxCapacity
        {
            get
            {
                return this.maxCapacity;
            }
        }
    }
}

