namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class Point2D : ICloneable
    {
        internal double x;
        internal double y;

        public Point2D()
        {
            this.x = 0.0;
            this.y = 0.0;
        }

        public Point2D(Point2D p)
        {
            this.SetLocation(p);
        }

        public Point2D(ChartCalendar px, double py)
        {
            this.x = px.GetCalendarMsecs();
            this.y = py;
        }

        public Point2D(DateTime px, double py)
        {
            this.x = px.Ticks / 0x2710L;
            this.y = py;
        }

        public Point2D(double px, double py)
        {
            this.x = px;
            this.y = py;
        }

        public object Clone()
        {
            return new Point2D(this.x, this.y);
        }

        public double Distance(Point2D pt)
        {
            double num = pt.GetX() - this.GetX();
            double num2 = pt.GetY() - this.GetY();
            return Math.Sqrt((num * num) + (num2 * num2));
        }

        public double Distance(double PX, double PY)
        {
            PX -= this.GetX();
            PY -= this.GetY();
            return Math.Sqrt((PX * PX) + (PY * PY));
        }

        public static double Distance(double X1, double Y1, double X2, double Y2)
        {
            X1 -= X2;
            Y1 -= Y2;
            return Math.Sqrt((X1 * X1) + (Y1 * Y1));
        }

        public double DistanceSq(Point2D pt)
        {
            double num = pt.GetX() - this.GetX();
            double num2 = pt.GetY() - this.GetY();
            return ((num * num) + (num2 * num2));
        }

        public double DistanceSq(double PX, double PY)
        {
            PX -= this.GetX();
            PY -= this.GetY();
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

        public Point GetPoint()
        {
            return new Point((int) this.x, (int) this.y);
        }

        public PointF GetPointF()
        {
            return new PointF((float) this.x, (float) this.y);
        }

        public double GetX()
        {
            return this.x;
        }

        public double GetY()
        {
            return this.y;
        }

        public void SetLocation(Point2D p)
        {
            this.x = p.GetX();
            this.y = p.GetY();
        }

        public void SetLocation(ChartCalendar px, double py)
        {
            this.x = px.GetCalendarMsecs();
            this.y = py;
        }

        public void SetLocation(DateTime px, double py)
        {
            this.x = px.Ticks / 0x2710L;
            this.y = py;
        }

        public void SetLocation(double px, double py)
        {
            this.x = px;
            this.y = py;
        }

        public void SetPoint(Point p)
        {
            this.x = p.X;
            this.y = p.Y;
        }

        public void SetPointF(PointF p)
        {
            this.x = p.X;
            this.y = p.Y;
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

        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
    }
}

