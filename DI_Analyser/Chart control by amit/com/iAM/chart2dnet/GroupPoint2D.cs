namespace com.iAM.chart2dnet
{
    using System;

    public class GroupPoint2D : ICloneable
    {
        internal double x;
        internal DoubleArray ygroup;

        public GroupPoint2D()
        {
            this.x = 0.0;
            this.ygroup = new DoubleArray(0);
            this.x = 0.0;
            this.ygroup.Reset();
        }

        public GroupPoint2D(GroupPoint2D p)
        {
            this.x = 0.0;
            this.ygroup = new DoubleArray(0);
            this.SetLocation(p);
        }

        public GroupPoint2D(ChartCalendar px, double[] py)
        {
            this.x = 0.0;
            this.ygroup = new DoubleArray(0);
            this.x = px.GetCalendarMsecs();
            this.ygroup.SetElements(py);
        }

        public GroupPoint2D(DateTime px, double[] py)
        {
            this.x = 0.0;
            this.ygroup = new DoubleArray(0);
            this.x = px.Ticks / 0x2710L;
            this.ygroup.SetElements(py);
        }

        public GroupPoint2D(double px, DoubleArray py)
        {
            this.x = 0.0;
            this.ygroup = new DoubleArray(0);
            this.x = px;
            this.ygroup.SetElements(py.GetDataBuffer());
        }

        public GroupPoint2D(double px, double[] py)
        {
            this.x = 0.0;
            this.ygroup = new DoubleArray(0);
            this.x = px;
            this.ygroup.SetElements(py);
        }

        public object Clone()
        {
            return new GroupPoint2D(this.x, this.ygroup.GetDataBuffer());
        }

        public double Distance(GroupPoint2D pt, int groupindex)
        {
            double num = pt.GetX() - this.GetX();
            double num2 = pt.ygroup[groupindex] - this.ygroup[groupindex];
            return Math.Sqrt((num * num) + (num2 * num2));
        }

        public double Distance(double PX, double PY, int groupindex)
        {
            PX -= this.GetX();
            PY -= this.ygroup[groupindex];
            return Math.Sqrt((PX * PX) + (PY * PY));
        }

        public static double Distance(double X1, double Y1, double X2, double Y2)
        {
            X1 -= X2;
            Y1 -= Y2;
            return Math.Sqrt((X1 * X1) + (Y1 * Y1));
        }

        public double DistanceSq(GroupPoint2D pt, int groupindex)
        {
            double num = pt.GetX() - this.GetX();
            double num2 = pt.ygroup[groupindex] - this.ygroup[groupindex];
            return ((num * num) + (num2 * num2));
        }

        public double DistanceSq(double PX, double PY, int groupindex)
        {
            PX -= this.GetX();
            PY -= this.ygroup[groupindex];
            return ((PX * PX) + (PY * PY));
        }

        public static double DistanceSq(double X1, double Y1, double X2, double Y2)
        {
            X1 -= X2;
            Y1 -= Y2;
            return ((X1 * X1) + (Y1 * Y1));
        }

        public ChartCalendar GetCalendarX()
        {
            return new ChartCalendar((long) this.x, true);
        }

        public DateTime GetDateTimeX()
        {
            return new DateTime(((long) this.x) * 0x2710L);
        }

        public double GetX()
        {
            return this.x;
        }

        public double[] GetY()
        {
            return this.ygroup.GetDataBuffer();
        }

        public void SetLocation(GroupPoint2D p)
        {
            this.x = p.GetX();
            this.ygroup.SetElements(p.GetY());
        }

        public void SetLocation(ChartCalendar px, double[] py)
        {
            this.x = px.GetCalendarMsecs();
            this.ygroup.SetElements(py);
        }

        public void SetLocation(DateTime px, double[] py)
        {
            this.x = px.Ticks / 0x2710L;
            this.ygroup.SetElements(py);
        }

        public void SetLocation(double px, double[] py)
        {
            this.x = px;
            this.ygroup.SetElements(py);
        }

        public ChartCalendar CalendarX
        {
            get
            {
                return this.GetCalendarX();
            }
            set
            {
                this.x = value.GetCalendarMsecs();
            }
        }

        public DateTime DateTimeX
        {
            get
            {
                return this.GetDateTimeX();
            }
            set
            {
                this.x = value.Ticks / 0x2710L;
            }
        }

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double[] Y
        {
            get
            {
                return this.ygroup.GetDataBuffer();
            }
            set
            {
                this.ygroup.SetElements(value);
            }
        }
    }
}

