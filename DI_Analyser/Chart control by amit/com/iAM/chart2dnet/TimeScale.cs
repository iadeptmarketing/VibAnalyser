namespace com.iAM.chart2dnet
{
    using System;

    public class TimeScale : Scale
    {
        internal long countedDays;
        internal long millisecondsPerDay;
        internal ChartCalendar scaleDateStart;
        internal ChartCalendar scaleDateStop;
        internal long scaleStartTOD;
        internal long scaleStopTOD;
        internal int timeAxis;
        internal long totalMilliseconds;
        internal int weekType;

        public TimeScale()
        {
            this.weekType = 0;
            this.timeAxis = 0;
            this.countedDays = 1L;
            this.millisecondsPerDay = 0x5265c00L;
            this.totalMilliseconds = 0x5265c00L;
            this.scaleDateStart = new ChartCalendar();
            this.scaleDateStop = new ChartCalendar();
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.InitDefaults();
        }

        public TimeScale(int timeaxis)
        {
            this.weekType = 0;
            this.timeAxis = 0;
            this.countedDays = 1L;
            this.millisecondsPerDay = 0x5265c00L;
            this.totalMilliseconds = 0x5265c00L;
            this.scaleDateStart = new ChartCalendar();
            this.scaleDateStop = new ChartCalendar();
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.InitDefaults();
            this.timeAxis = timeaxis;
        }

        public TimeScale(ChartCalendar startdate, ChartCalendar stopdate)
        {
            this.weekType = 0;
            this.timeAxis = 0;
            this.countedDays = 1L;
            this.millisecondsPerDay = 0x5265c00L;
            this.totalMilliseconds = 0x5265c00L;
            this.scaleDateStart = new ChartCalendar();
            this.scaleDateStop = new ChartCalendar();
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.InitDefaults();
            this.timeAxis = 0;
            this.scaleDateStart = (ChartCalendar) startdate.Clone();
            this.scaleDateStop = (ChartCalendar) stopdate.Clone();
            this.AdjustTimeScaleEndpoints();
        }

        public TimeScale(long nstartdate, long nstopdate)
        {
            this.weekType = 0;
            this.timeAxis = 0;
            this.countedDays = 1L;
            this.millisecondsPerDay = 0x5265c00L;
            this.totalMilliseconds = 0x5265c00L;
            this.scaleDateStart = new ChartCalendar();
            this.scaleDateStop = new ChartCalendar();
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.InitDefaults();
            this.timeAxis = 0;
            ChartCalendar.SetCalendarMsecs(this.scaleDateStart, nstartdate);
            ChartCalendar.SetCalendarMsecs(this.scaleDateStop, nstopdate);
            this.AdjustTimeScaleEndpoints();
        }

        public TimeScale(int timeaxis, ChartCalendar startdate, ChartCalendar stopdate)
        {
            this.weekType = 0;
            this.timeAxis = 0;
            this.countedDays = 1L;
            this.millisecondsPerDay = 0x5265c00L;
            this.totalMilliseconds = 0x5265c00L;
            this.scaleDateStart = new ChartCalendar();
            this.scaleDateStop = new ChartCalendar();
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.InitDefaults();
            this.timeAxis = timeaxis;
            this.scaleDateStart = (ChartCalendar) startdate.Clone();
            this.scaleDateStop = (ChartCalendar) stopdate.Clone();
            this.AdjustTimeScaleEndpoints();
        }

        public TimeScale(int timeaxis, long nstartdate, long nstopdate)
        {
            this.weekType = 0;
            this.timeAxis = 0;
            this.countedDays = 1L;
            this.millisecondsPerDay = 0x5265c00L;
            this.totalMilliseconds = 0x5265c00L;
            this.scaleDateStart = new ChartCalendar();
            this.scaleDateStop = new ChartCalendar();
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.InitDefaults();
            this.timeAxis = timeaxis;
            ChartCalendar.SetCalendarMsecs(this.scaleDateStart, nstartdate);
            ChartCalendar.SetCalendarMsecs(this.scaleDateStop, nstopdate);
            this.AdjustTimeScaleEndpoints();
        }

        public void AdjustTimeScaleEndpoints()
        {
            ChartCalendar.CalendarWeekAdjust(this.scaleDateStart, this.weekType, true);
            ChartCalendar.CalendarWeekAdjust(this.scaleDateStop, this.weekType, true);
        }

        public long CalendarSecsDiff(ChartCalendar datestart, ChartCalendar datestop)
        {
            long num = 0L;
            long num2 = 0L;
            long num3 = 0L;
            long num4 = 0L;
            long num5 = 0L;
            bool flag = false;
            if ((datestart == null) || (datestop == null))
            {
                return num;
            }
            if ((this.millisecondsPerDay == 0x5265c00L) && (this.weekType == 0))
            {
                return (datestop.GetCalendarMsecs() - datestart.GetCalendarMsecs());
            }
            ChartCalendar calendar = ChartCalendar.NewCalendar(datestart);
            ChartCalendar calendar2 = ChartCalendar.NewCalendar(datestop);
            long calendarMsecs = calendar.GetCalendarMsecs();
            num = calendar2.GetCalendarMsecs() - calendarMsecs;
            if (num < 0L)
            {
                ChartCalendar.CalendarSwap(calendar, calendar2);
                num = -num;
                flag = true;
            }
            ChartCalendar ddate = ChartCalendar.NewCalendar(calendar);
            ChartCalendar calendar4 = ChartCalendar.NewCalendar(calendar2);
            ChartCalendar calendar5 = ChartCalendar.NewCalendar(calendar2);
            long num8 = calendar2.GetCalendarMsecs();
            long num9 = calendar.GetCalendarMsecs();
            if (num8 > num9)
            {
                ChartCalendar.SetTODMsecs(ddate, this.scaleStopTOD);
                ChartCalendar.SetTODMsecs(calendar4, this.scaleStopTOD);
                ChartCalendar.SetTODMsecs(calendar5, this.scaleStartTOD);
                num5 = ChartCalendar.CalendarDaysDiff(calendar, calendar2, this.weekType);
                switch (num5)
                {
                    case 0L:
                        num3 = num8 - num9;
                        goto Label_0149;

                    case 1L:
                        num3 = ddate.GetCalendarMsecs() - num9;
                        num4 = ChartCalendar.CalendarMin(calendar2, calendar4).GetCalendarMsecs() - calendar5.GetCalendarMsecs();
                        goto Label_0149;
                }
                num3 = ddate.GetCalendarMsecs() - num9;
                num4 = ChartCalendar.CalendarMin(calendar2, calendar4).GetCalendarMsecs() - calendar5.GetCalendarMsecs();
            }
        Label_0149:
            num2 = Math.Max((long) 0L, (long) (num5 - 1L)) * this.millisecondsPerDay;
            num = (num2 + Math.Max(0L, num4)) + Math.Max(0L, num3);
            if (flag)
            {
                num = -num;
            }
            return num;
        }

        public double CheckTimeValue(ChartCalendar cdate)
        {
            double calendarMsecs = cdate.GetCalendarMsecs();
            if (!this.TimeValueGood(cdate))
            {
                calendarMsecs = double.MaxValue;
            }
            return calendarMsecs;
        }

        public double CheckTimeValue(long ndate)
        {
            double maxValue = ndate;
            if (!this.TimeValueGood(ndate))
            {
                maxValue = double.MaxValue;
            }
            return maxValue;
        }

        public override object Clone()
        {
            TimeScale scale = new TimeScale();
            scale.Copy(this);
            return scale;
        }

        protected ChartCalendar ConvertXYCoordToTime(long x)
        {
            long calendarMsecs = this.scaleDateStart.GetCalendarMsecs();
            long num2 = (x - calendarMsecs) / this.millisecondsPerDay;
            long num3 = (x - calendarMsecs) % this.millisecondsPerDay;
            return new ChartCalendar((long) this.CoordinateAdd((double) this.scaleDateStart.GetCalendarMsecs(), (num2 * this.millisecondsPerDay) + num3), true);
        }

        public override double CoordinateAdd(double rdatevalue1, double rsecs)
        {
            long time = (long) rdatevalue1;
            long num2 = (long) rsecs;
            ChartCalendar date = new ChartCalendar(time, true);
            double num3 = 0.0;
            long num4 = num2 / this.millisecondsPerDay;
            long num5 = num2 % this.millisecondsPerDay;
            long tODMsecs = ChartCalendar.GetTODMsecs(date);
            ChartCalendar calendar2 = (ChartCalendar) date.Clone();
            ChartCalendar.CalendarTruncate(calendar2, 6);
            long num7 = (tODMsecs + num5) - this.scaleStartTOD;
            long num8 = num7 / this.millisecondsPerDay;
            num4 += num8;
            long num9 = num7 % this.millisecondsPerDay;
            num3 = (ChartCalendar.CalendarDaysAdd(calendar2, (long) ((int) num4), this.weekType).GetCalendarMsecs() + num9) + this.scaleStartTOD;
            ChartCalendar.SetCalendarMsecs(date, (long) num3);
            if (num2 >= 0L)
            {
                ChartCalendar.CalendarWeekAdjust(date, this.weekType, true);
            }
            else
            {
                ChartCalendar.CalendarWeekAdjust(date, this.weekType, false);
            }
            return (double) date.GetCalendarMsecs();
        }

        public void Copy(TimeScale source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.weekType = source.weekType;
                this.timeAxis = source.timeAxis;
                this.countedDays = source.countedDays;
                this.millisecondsPerDay = source.millisecondsPerDay;
                this.totalMilliseconds = source.totalMilliseconds;
                if (source.scaleDateStart != null)
                {
                    this.scaleDateStart = (ChartCalendar) source.scaleDateStart.Clone();
                }
                if (source.scaleDateStop != null)
                {
                    this.scaleDateStop = (ChartCalendar) source.scaleDateStop.Clone();
                }
                this.scaleStartTOD = source.scaleStartTOD;
                this.scaleStopTOD = source.scaleStopTOD;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public override AutoScale GetCompatibleAutoScale()
        {
            return new TimeAutoScale(this);
        }

        public override Axis GetCompatibleAxis()
        {
            return new TimeAxis();
        }

        public long GetCountedDays()
        {
            return this.countedDays;
        }

        public long GetMillisecondsPerDay()
        {
            return this.millisecondsPerDay;
        }

        public ChartCalendar GetScaleDateStart()
        {
            return (ChartCalendar) this.scaleDateStart.Clone();
        }

        public ChartCalendar GetScaleDateStop()
        {
            return (ChartCalendar) this.scaleDateStop.Clone();
        }

        public long GetScaleStartTOD()
        {
            return this.scaleStartTOD;
        }

        public long GetScaleStopTOD()
        {
            return this.scaleStopTOD;
        }

        public long GetTotalMilliseconds()
        {
            return this.totalMilliseconds;
        }

        public int GetWeekType()
        {
            return this.weekType;
        }

        private void InitDefaults()
        {
            this.scaleDateStop.Add(6, 1);
            base.chartObjType = 0x4b2;
        }

        public double PhysToWorkingScale(ChartCalendar tval)
        {
            double calendarMsecs = 0.0;
            if (calendarMsecs != double.MaxValue)
            {
                calendarMsecs = this.scaleDateStart.GetCalendarMsecs();
                calendarMsecs += this.CalendarSecsDiff(this.scaleDateStart, tval);
            }
            return calendarMsecs;
        }

        public override double PhysToWorkingScale(double v)
        {
            return this.PhysToWorkingScale((long) v);
        }

        public double PhysToWorkingScale(long v)
        {
            ChartCalendar tval = new ChartCalendar(v, true);
            return this.PhysToWorkingScale(tval);
        }

        public void SetScaleDateStart(ChartCalendar startdate)
        {
            this.scaleDateStart = (ChartCalendar) startdate.Clone();
            this.AdjustTimeScaleEndpoints();
        }

        public void SetScaleDateStop(ChartCalendar stopdate)
        {
            this.scaleDateStop = (ChartCalendar) stopdate.Clone();
            this.AdjustTimeScaleEndpoints();
        }

        public void SetScaleStartTOD(long starttime)
        {
            this.scaleStartTOD = starttime;
        }

        public void SetScaleStopTOD(long stoptime)
        {
            this.scaleStopTOD = stoptime;
        }

        internal void SetTimeScale(ChartCalendar dstart, long starttime, ChartCalendar dstop, long stoptime, int ntimeaxis, int nweektype)
        {
            if ((dstart != null) && (dstop != null))
            {
                this.weekType = nweektype;
                this.scaleStartTOD = starttime;
                this.scaleStopTOD = stoptime;
                this.timeAxis = ntimeaxis;
                this.millisecondsPerDay = this.scaleStopTOD - this.scaleStartTOD;
                if (this.millisecondsPerDay <= 0L)
                {
                    this.millisecondsPerDay = 0x5265c00L;
                }
                this.SetScaleDateStart(dstart);
                ChartCalendar calendar1 = (ChartCalendar) dstart.Clone();
                ChartCalendar calendar2 = (ChartCalendar) dstart.Clone();
                this.SetScaleDateStop(dstop);
                this.AdjustTimeScaleEndpoints();
                if (ChartCalendar.GetTODMsecs(this.scaleDateStart) < this.scaleStartTOD)
                {
                    ChartCalendar.SetTODMsecs(this.scaleDateStart, this.scaleStartTOD);
                }
                this.countedDays = ChartCalendar.CalendarDaysDiff(this.scaleDateStart, this.scaleDateStop, this.weekType);
                this.totalMilliseconds = this.CalendarSecsDiff(this.scaleDateStart, this.scaleDateStop);
            }
        }

        internal void SetTimeScale(double dstart, long starttime, double dstop, long stoptime, int ntimeaxis, int nweektype)
        {
            ChartCalendar calendar = new ChartCalendar((long) dstart, true);
            ChartCalendar calendar2 = new ChartCalendar((long) dstop, true);
            this.SetTimeScale(calendar, starttime, calendar2, stoptime, ntimeaxis, nweektype);
        }

        public void SetWeekType(int weektype)
        {
            this.weekType = weektype;
        }

        public bool TimeValueGood(ChartCalendar cdate)
        {
            bool flag = false;
            if (cdate.GetCalendarMsecs() == 0L)
            {
                return false;
            }
            int num = cdate.Get(7);
            if (((this.weekType == 1) && (num != 6)) && (num != 0))
            {
                flag = true;
            }
            if (flag)
            {
                long tODMsecs = ChartCalendar.GetTODMsecs(cdate);
                if (((this.scaleStartTOD != this.scaleStopTOD) && (this.scaleStopTOD > this.scaleStartTOD)) && ((tODMsecs >= this.scaleStartTOD) && (tODMsecs <= this.scaleStopTOD)))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool TimeValueGood(long ndate)
        {
            if (ndate == 0L)
            {
                return false;
            }
            ChartCalendar cdate = new ChartCalendar(ndate, true);
            return this.TimeValueGood(cdate);
        }

        public override double WorkingToPhysScale(double v)
        {
            return (double) this.ConvertXYCoordToTime((long) v).GetCalendarMsecs();
        }

        public void WorkingToPhysScale(ChartCalendar d, double v)
        {
            ChartCalendar source = this.ConvertXYCoordToTime((long) v);
            ChartCalendar.CalendarCopy(d, source);
        }

        public bool IsContinuousTime
        {
            get
            {
                return ((this.millisecondsPerDay == 0x5265c00L) && (this.weekType == 0));
            }
        }

        public ChartCalendar ScaleDateMax
        {
            get
            {
                ChartCalendar scaleDateStart = this.GetScaleDateStart();
                ChartCalendar scaleDateStop = this.GetScaleDateStop();
                if (scaleDateStart.GetCalendarMsecs() < scaleDateStop.GetCalendarMsecs())
                {
                    return scaleDateStop;
                }
                return scaleDateStart;
            }
        }

        public ChartCalendar ScaleDateMin
        {
            get
            {
                ChartCalendar scaleDateStart = this.GetScaleDateStart();
                ChartCalendar scaleDateStop = this.GetScaleDateStop();
                if (scaleDateStart.GetCalendarMsecs() < scaleDateStop.GetCalendarMsecs())
                {
                    return scaleDateStart;
                }
                return scaleDateStop;
            }
        }

        public ChartCalendar ScaleDateStart
        {
            get
            {
                return this.GetScaleDateStart();
            }
            set
            {
                this.SetScaleDateStart(value);
            }
        }

        public ChartCalendar ScaleDateStop
        {
            get
            {
                return this.GetScaleDateStop();
            }
            set
            {
                this.SetScaleDateStop(value);
            }
        }

        public long ScaleStartTOD
        {
            get
            {
                return this.scaleStartTOD;
            }
            set
            {
                this.scaleStartTOD = value;
            }
        }

        public long ScaleStopTOD
        {
            get
            {
                return this.scaleStopTOD;
            }
            set
            {
                this.scaleStopTOD = value;
            }
        }

        public int WeekType
        {
            get
            {
                return this.weekType;
            }
            set
            {
                this.weekType = value;
            }
        }
    }
}

