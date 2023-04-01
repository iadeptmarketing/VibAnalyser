namespace com.iAM.chart2dnet
{
    using System;

    public class TickMark : ChartObj
    {
        internal bool labelFlag;
        internal ChartCalendar tickDate;
        internal int tickLabelFormat;
        internal double tickLocation;
        internal Point2D tickStart;
        internal Point2D tickStop;
        internal int tickType;

        public TickMark()
        {
            this.tickStart = new Point2D();
            this.tickStop = new Point2D();
            this.tickLocation = 0.0;
            this.tickDate = null;
            this.tickType = 1;
            this.tickLabelFormat = 0;
            this.labelFlag = false;
        }

        public TickMark(int nticktype)
        {
            this.tickStart = new Point2D();
            this.tickStop = new Point2D();
            this.tickLocation = 0.0;
            this.tickDate = null;
            this.tickType = 1;
            this.tickLabelFormat = 0;
            this.labelFlag = false;
            this.tickType = nticktype;
            if (this.tickType == 0)
            {
                this.labelFlag = true;
            }
        }

        public TickMark(double rticklocation, int nticktype)
        {
            this.tickStart = new Point2D();
            this.tickStop = new Point2D();
            this.tickLocation = 0.0;
            this.tickDate = null;
            this.tickType = 1;
            this.tickLabelFormat = 0;
            this.labelFlag = false;
            this.tickLocation = rticklocation;
            this.tickType = nticktype;
            if (this.tickType == 0)
            {
                this.labelFlag = true;
            }
        }

        public TickMark(Point2D pstart, Point2D pstop, double rticklocation, int nticktype)
        {
            this.tickStart = new Point2D();
            this.tickStop = new Point2D();
            this.tickLocation = 0.0;
            this.tickDate = null;
            this.tickType = 1;
            this.tickLabelFormat = 0;
            this.labelFlag = false;
            this.SetTickLocation(pstart, pstop, rticklocation, null);
            this.tickType = nticktype;
            if (this.tickType == 0)
            {
                this.labelFlag = true;
            }
        }

        public TickMark(Point2D pstart, Point2D pstop, double rticklocation, ChartCalendar dtickdate, int nticktype)
        {
            this.tickStart = new Point2D();
            this.tickStop = new Point2D();
            this.tickLocation = 0.0;
            this.tickDate = null;
            this.tickType = 1;
            this.tickLabelFormat = 0;
            this.labelFlag = false;
            this.SetTickLocation(pstart, pstop, rticklocation, dtickdate);
            this.tickType = nticktype;
            if (this.tickType == 0)
            {
                this.labelFlag = true;
            }
        }

        public TickMark(Point2D pstart, Point2D pstop, double rticklocation, int nticktype, bool blabelf)
        {
            this.tickStart = new Point2D();
            this.tickStop = new Point2D();
            this.tickLocation = 0.0;
            this.tickDate = null;
            this.tickType = 1;
            this.tickLabelFormat = 0;
            this.labelFlag = false;
            this.SetTickLocation(pstart, pstop, rticklocation, null);
            this.tickType = nticktype;
            this.labelFlag = blabelf;
        }

        public override object Clone()
        {
            TickMark mark = new TickMark();
            mark.Copy(this);
            return mark;
        }

        public void Copy(TickMark source)
        {
            if (source != null)
            {
                this.tickLocation = source.tickLocation;
                this.tickType = source.tickType;
                this.tickStart.SetLocation(source.tickStart);
                this.tickStop.SetLocation(source.tickStop);
                this.labelFlag = source.labelFlag;
                this.tickLabelFormat = source.tickLabelFormat;
                if (source.tickDate != null)
                {
                    this.tickDate = (ChartCalendar) source.tickDate.Clone();
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public ChartCalendar GetTickDate()
        {
            return this.tickDate;
        }

        public bool GetTickLabelFlag()
        {
            return this.labelFlag;
        }

        public int GetTickLabelFormat()
        {
            return this.tickLabelFormat;
        }

        public double GetTickLocation()
        {
            return this.tickLocation;
        }

        public Point2D GetTickStart()
        {
            return this.tickStart;
        }

        public Point2D GetTickStop()
        {
            return this.tickStop;
        }

        public int GetTickType()
        {
            return this.tickType;
        }

        public bool IsDateTick()
        {
            bool flag = false;
            if (this.tickDate != null)
            {
                flag = true;
            }
            return flag;
        }

        public void SetTickDate(ChartCalendar dtickdate)
        {
            this.tickDate = dtickdate;
        }

        public void SetTickLabelFlag(bool blabelf)
        {
            this.labelFlag = blabelf;
        }

        public void SetTickLabelFormat(int ticklabelformat)
        {
            this.tickLabelFormat = ticklabelformat;
        }

        public void SetTickLocation(double rticklocation)
        {
            this.tickLocation = rticklocation;
        }

        public void SetTickLocation(Point2D pstart, Point2D pstop)
        {
            this.tickStart.SetLocation(pstart.GetX(), pstart.GetY());
            this.tickStop.SetLocation(pstop.GetX(), pstop.GetY());
        }

        public void SetTickLocation(Point2D pstart, Point2D pstop, double rticklocation, ChartCalendar dtickdate)
        {
            this.tickStart.SetLocation(pstart.GetX(), pstart.GetY());
            this.tickStop.SetLocation(pstop.GetX(), pstop.GetY());
            this.tickLocation = rticklocation;
            this.tickDate = dtickdate;
        }

        public void SetTickMark(double rticklocation, int nticktype)
        {
            this.tickLocation = rticklocation;
            this.tickType = nticktype;
        }

        public void SetTickMark(Point2D pstart, Point2D pstop, double rticklocation, ChartCalendar dtickdate, int nticktype)
        {
            this.SetTickLocation(pstart, pstop, rticklocation, dtickdate);
            this.tickType = nticktype;
        }

        public void SetTickStart(Point2D pstart)
        {
            this.tickStart.SetLocation(pstart.GetX(), pstart.GetY());
        }

        public void SetTickStop(Point2D pstop)
        {
            this.tickStop.SetLocation(pstop.GetX(), pstop.GetY());
        }

        public void SetTickType(int nticktype)
        {
            this.tickType = nticktype;
        }

        public bool LabelFlag
        {
            get
            {
                return this.labelFlag;
            }
            set
            {
                this.labelFlag = value;
            }
        }

        public ChartCalendar TickDate
        {
            get
            {
                return this.tickDate;
            }
            set
            {
                this.tickDate = value;
            }
        }

        public int TickLabelFormat
        {
            get
            {
                return this.tickLabelFormat;
            }
            set
            {
                this.tickLabelFormat = value;
            }
        }

        public double TickLocation
        {
            get
            {
                return this.tickLocation;
            }
            set
            {
                this.tickLocation = value;
            }
        }

        public Point2D TickStart
        {
            get
            {
                return this.tickStart;
            }
            set
            {
                this.tickStart = value;
            }
        }

        public Point2D TickStop
        {
            get
            {
                return this.tickStop;
            }
            set
            {
                this.tickStop = value;
            }
        }

        public int TickType
        {
            get
            {
                return this.tickType;
            }
            set
            {
                this.tickType = value;
            }
        }
    }
}

