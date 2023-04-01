namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class TimeAxis : Axis
    {
        internal int axisTickMarkTimeBase;
        internal double axisTickSpace;
        internal int quarterOffset;
        internal ChartCalendar timeLabelsOrigin;

        public TimeAxis()
        {
            this.axisTickMarkTimeBase = 0x59;
            this.axisTickSpace = 100.0;
            this.timeLabelsOrigin = new ChartCalendar();
            this.quarterOffset = 0;
            this.InitDefaults();
        }

        public TimeAxis(TimeCoordinates transform)
        {
            this.axisTickMarkTimeBase = 0x59;
            this.axisTickSpace = 100.0;
            this.timeLabelsOrigin = new ChartCalendar();
            this.quarterOffset = 0;
            this.InitTimeAxis(transform, 0);
            this.CalcAutoAxis();
        }

        public TimeAxis(TimeCoordinates transform, int axtype)
        {
            this.axisTickMarkTimeBase = 0x59;
            this.axisTickSpace = 100.0;
            this.timeLabelsOrigin = new ChartCalendar();
            this.quarterOffset = 0;
            this.InitTimeAxis(transform, axtype);
            this.CalcAutoAxis();
        }

        public TimeAxis(TimeCoordinates transform, ChartCalendar dstart, ChartCalendar dstop)
        {
            this.axisTickMarkTimeBase = 0x59;
            this.axisTickSpace = 100.0;
            this.timeLabelsOrigin = new ChartCalendar();
            this.quarterOffset = 0;
            this.InitTimeAxis(transform, 0);
            this.CalcAutoAxis(dstart, dstop);
        }

        public TimeAxis(TimeCoordinates transform, int axtype, int ntickmarkbase)
        {
            this.axisTickMarkTimeBase = 0x59;
            this.axisTickSpace = 100.0;
            this.timeLabelsOrigin = new ChartCalendar();
            this.quarterOffset = 0;
            this.InitTimeAxis(transform, axtype);
            this.CalcAutoAxis();
            this.axisTickMarkTimeBase = ntickmarkbase;
        }

        public TimeAxis(TimeCoordinates transform, int axtype, int ntickmarkbase, int nminornthtick)
        {
            this.axisTickMarkTimeBase = 0x59;
            this.axisTickSpace = 100.0;
            this.timeLabelsOrigin = new ChartCalendar();
            this.quarterOffset = 0;
            this.InitTimeAxis(transform, axtype);
            this.CalcAutoAxis();
            this.axisTickMarkTimeBase = ntickmarkbase;
            base.axisMinorNthTick = nminornthtick;
        }

        public override void CalcAutoAxis()
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetMillisecondsPerDay();
                TimeAutoScale autoScale = new TimeAutoScale(timeCoordinates, base.axisType, 0);
                this.CalcAutoAxisWork(autoScale);
            }
        }

        public void CalcAutoAxis(ChartCalendar dstart, ChartCalendar dstop)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetMillisecondsPerDay();
                TimeAutoScale autoScale = new TimeAutoScale(timeCoordinates, dstart, dstop, base.axisType, 0);
                this.CalcAutoAxisWork(autoScale);
            }
        }

        private void CalcAutoAxisWork(TimeAutoScale autoScale)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetMillisecondsPerDay();
                base.InitAxis(timeCoordinates, base.axisType, autoScale.GetFinalMin(), autoScale.GetFinalMax());
                if (base.axisType == 0)
                {
                    base.SetAxisIntercept(base.chartObjScale.GetStartY());
                }
                else
                {
                    base.SetAxisIntercept(base.chartObjScale.GetStartX());
                }
                base.axisMin = autoScale.GetFinalMin();
                base.axisMax = autoScale.GetFinalMax();
                this.axisTickMarkTimeBase = autoScale.GetTimeScaleBase();
                base.axisMinorTicksPerMajor = autoScale.GetAxisMinorTicksPerMajor();
                base.axisMinorNthTick = autoScale.GetTimeMinorNthTick();
                base.axisMajorNthTick = autoScale.GetTimeMajorNthTick();
                base.axisTickOrigin = autoScale.GetLabelsOrigin();
                this.timeLabelsOrigin = autoScale.AdjustTimeLabelsOrigin(autoScale.GetDateStart(), 0, 11);
                this.timeLabelsOrigin = autoScale.GetTimeLabelsOrigin();
                this.axisTickSpace = autoScale.GetTickInterval();
                if (base.axisLabels != null)
                {
                    base.axisLabels.CalcAutoAxisLabels();
                }
            }
        }

        public override int CalcAxisLabelsDecimalPos()
        {
            return 0;
        }

        private void CalcTickMark(ChartCalendar startdate, ChartCalendar currentdate, int nticktype, int ncalbase, int nbaseincr, int increment)
        {
            Point2D startp = new Point2D();
            Point2D stopp = new Point2D();
            int nstaggerlevel = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                currentdate.GetMinimum(ncalbase);
                currentdate.Get(ncalbase);
                if (ncalbase == 7)
                {
                    timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                }
                double calendarMsecs = currentdate.GetCalendarMsecs();
                nstaggerlevel = base.majorTickCntr % base.numTickStagger;
                this.CalcCartesianTickPoint(calendarMsecs, nticktype, startp, stopp, nstaggerlevel);
                this.AddAxisTick(startp, stopp, calendarMsecs, currentdate, nticktype);
                if (nticktype == 0)
                {
                    base.majorTickCntr++;
                }
                currentdate.Add(nbaseincr, increment);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
            }
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            TimeAxis axis = new TimeAxis();
            axis.Copy(this);
            return axis;
        }

        public void Copy(TimeAxis source)
        {
            if (source != null)
            {
                this.InitDefaults();
                base.Copy(source);
                this.axisTickSpace = source.axisTickSpace;
                if (source.timeLabelsOrigin != null)
                {
                    this.timeLabelsOrigin = (ChartCalendar) source.timeLabelsOrigin.Clone();
                }
                this.axisTickMarkTimeBase = source.axisTickMarkTimeBase;
                this.quarterOffset = source.quarterOffset;
            }
        }

        private void DefineDayAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarTruncate(currentdate, 6);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                do
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(6, 1);
                    }
                    else
                    {
                        nticktype = 0;
                        this.CalcTickMark(startdate, currentdate, nticktype, 6, 6, 1);
                    }
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2));
            }
        }

        private void DefineDayHourAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 0x18;
                int num3 = (minortickspermajor / minorincrement) - 1;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 6);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitNewDay(currentdate, startofday, endofday);
                do
                {
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 11, 11, 0);
                    }
                    nticktype = 1;
                    ChartCalendar.CalendarTruncate(currentdate, 6);
                    for (int i = 0; i < num3; i++)
                    {
                        currentdate.Add(11, minorincrement);
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 11, 11, 0);
                        }
                    }
                    currentdate.Add(11, minorincrement);
                    if (currentdate.Get(11) == 0)
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 10, modulus);
            }
        }

        private void DefineHourHourAxisTicks(ChartCalendar currentdate, int timebase, int majorincrement, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 60;
                int num3 = 0x17;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 11);
                do
                {
                    this.InitNewDay(currentdate, startofday, endofday);
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 11, 11, 0);
                    }
                    ChartCalendar.CalendarTruncate(currentdate, 11);
                    for (int i = currentdate.Get(11); i < num3; i++)
                    {
                        currentdate.Add(11, 1);
                        if ((currentdate.Get(11) % majorincrement) == 0)
                        {
                            nticktype = 0;
                        }
                        else
                        {
                            if ((currentdate.Get(11) % minorincrement) != 0)
                            {
                                continue;
                            }
                            nticktype = 1;
                        }
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 11, 11, 0);
                        }
                    }
                    currentdate.Add(11, 1);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 10, modulus);
            }
        }

        private void DefineHourMinuteAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 60;
                int num3 = (minortickspermajor / minorincrement) - 1;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 11);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitNewDay(currentdate, startofday, endofday);
                do
                {
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 12, 12, 0);
                    }
                    ChartCalendar.CalendarTruncate(currentdate, 11);
                    for (int i = 0; i < num3; i++)
                    {
                        currentdate.Add(12, minorincrement);
                        nticktype = 1;
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 12, 12, 0);
                        }
                    }
                    currentdate.Add(12, minorincrement);
                    if (currentdate.Get(11) == 0)
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                    this.JumpForwardToValidTOD(currentdate, startofday, endofday);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 13, modulus);
            }
        }

        private void DefineMillisecondAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                result.GetCalendarMsecs();
                calendar4.GetCalendarMsecs();
                double axisTickSpace = this.axisTickSpace;
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 0x3e8;
                int axisMinorTicksPerMajor = base.axisMinorTicksPerMajor;
                ChartCalendar.CalendarTruncate(currentdate, 13);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitCurrentTODRange(currentdate, startofday, endofday);
                currentdate = (ChartCalendar) this.timeLabelsOrigin.Clone();
                do
                {
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 14, 14, 0);
                    }
                    currentdate.Add(15, (int) (axisTickSpace * 10000.0));
                    for (int i = 0; i < (axisMinorTicksPerMajor - 1); i++)
                    {
                        nticktype = 1;
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 14, 14, 0);
                        }
                        currentdate.Add(15, (int) (axisTickSpace * 10000.0));
                    }
                    if (((currentdate.Get(11) == 0) && (currentdate.Get(12) == 0)) && ((currentdate.Get(13) == 0) && (currentdate.Get(14) == 0)))
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                    this.JumpForwardToValidTOD(currentdate, startofday, endofday);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 13, modulus);
            }
        }

        private void DefineMinuteMinuteAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 60;
                int num3 = 0x3b;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 11);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitCurrentTODRange(currentdate, startofday, endofday);
                do
                {
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 12, 12, 0);
                    }
                    ChartCalendar.CalendarTruncate(currentdate, 12);
                    for (int i = currentdate.Get(12); i < num3; i++)
                    {
                        currentdate.Add(12, 1);
                        if ((currentdate.Get(12) % minortickspermajor) == 0)
                        {
                            nticktype = 0;
                        }
                        else
                        {
                            if ((currentdate.Get(12) % minorincrement) != 0)
                            {
                                continue;
                            }
                            nticktype = 1;
                        }
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 12, 12, 0);
                        }
                    }
                    currentdate.Add(12, 1);
                    if ((currentdate.Get(11) == 0) && (currentdate.Get(12) == 0))
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                    this.JumpForwardToValidTOD(currentdate, startofday, endofday);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 12, modulus);
            }
        }

        private void DefineMinuteSecondAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 60;
                int num3 = (minortickspermajor / minorincrement) - 1;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 12);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitCurrentTODRange(currentdate, startofday, endofday);
                do
                {
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 13, 13, 0);
                    }
                    for (int i = 0; i < num3; i++)
                    {
                        currentdate.Add(13, minorincrement);
                        nticktype = 1;
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 13, 13, 0);
                        }
                    }
                    currentdate.Add(13, minorincrement);
                    if (((currentdate.Get(11) == 0) && (currentdate.Get(12) == 0)) && (currentdate.Get(13) == 0))
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                    this.JumpForwardToValidTOD(currentdate, startofday, endofday);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 13, modulus);
            }
        }

        private void DefineMonthAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                this.RoundDateUp(currentdate, 2, 5);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(2, 1);
                    }
                    else
                    {
                        this.CalcTickMark(startdate, currentdate, 0, 2, 2, 1);
                    }
                }
            }
        }

        private void DefineMonthDayAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarTruncate(currentdate, 6);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(6, 1);
                    }
                    else
                    {
                        nticktype = this.GetTickType(currentdate, 5, 0x3e8);
                        this.CalcTickMark(startdate, currentdate, nticktype, 5, 5, 1);
                    }
                }
            }
        }

        private void DefineMonthWeekAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarTruncate(currentdate, 6);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(6, 1);
                    }
                    else
                    {
                        if (ChartCalendar.CalendarCheckMin(currentdate, 5, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8))
                        {
                            nticktype = 0;
                        }
                        else if (ChartCalendar.CalendarCheckMin(currentdate, 7, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8))
                        {
                            nticktype = 1;
                        }
                        else
                        {
                            currentdate.Add(5, 1);
                            ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                            continue;
                        }
                        this.CalcTickMark(startdate, currentdate, nticktype, 3, 6, 1);
                    }
                }
            }
        }

        private void DefineQuarterAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                this.RoundDateUp(currentdate, 2, 5);
                if ((((currentdate.Get(2) - 1) + Math.Max(0, this.quarterOffset)) % 3) != 0)
                {
                    currentdate.Add(2, 1);
                    if ((((currentdate.Get(2) - 1) + Math.Max(0, this.quarterOffset)) % 3) != 0)
                    {
                        currentdate.Add(2, 1);
                    }
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(2, 1);
                    }
                    else
                    {
                        if (ChartCalendar.CalendarCheckMin(currentdate, 5, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8) && ((((currentdate.Get(2) - 1) + Math.Max(0, this.quarterOffset)) % 3) == 0))
                        {
                            this.CalcTickMark(startdate, currentdate, nticktype, 2, 6, 1);
                            continue;
                        }
                        currentdate.Add(5, 1);
                    }
                }
            }
        }

        private void DefineQuarterMonthAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                this.RoundDateUp(currentdate, 2, 5);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(2, 1);
                    }
                    else
                    {
                        if (ChartCalendar.CalendarCheckMin(currentdate, 5, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8) && ((((currentdate.Get(2) - 1) + Math.Max(0, this.quarterOffset)) % 3) == 0))
                        {
                            nticktype = 0;
                        }
                        else if (ChartCalendar.CalendarCheckMin(currentdate, 5, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8))
                        {
                            nticktype = 1;
                        }
                        else
                        {
                            currentdate.Add(5, 1);
                            ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                            continue;
                        }
                        this.CalcTickMark(startdate, currentdate, nticktype, 2, 6, 1);
                    }
                }
            }
        }

        private void DefineSecondMillisecondAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 0x3e8;
                int num3 = 10;
                int num4 = 0;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 13);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitCurrentTODRange(currentdate, startofday, endofday);
                do
                {
                    int nticktype = 0;
                    for (int i = 0; i < num3; i++)
                    {
                        num4 = currentdate.Get(14);
                        if (num4 == 0)
                        {
                            nticktype = 0;
                        }
                        else
                        {
                            if ((num4 % minorincrement) != 0)
                            {
                                continue;
                            }
                            nticktype = 1;
                        }
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 13, 13, 0);
                        }
                        currentdate.Add(14, 100);
                    }
                    if (((currentdate.Get(11) == 0) && (currentdate.Get(12) == 0)) && ((currentdate.Get(13) == 0) && (currentdate.Get(14) == 0)))
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                    this.JumpForwardToValidTOD(currentdate, startofday, endofday);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 14, modulus);
            }
        }

        private void DefineSecondSecondAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.GetTimeScale(base.axisType).GetWeekType();
                int modulus = 60;
                int num3 = 0x3b;
                ChartCalendar endofday = new ChartCalendar();
                ChartCalendar startofday = new ChartCalendar();
                ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar4 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                ChartCalendar.CalendarWeekAdjust(calendar4, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarTruncate(currentdate, 12);
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                this.InitCurrentTODRange(currentdate, startofday, endofday);
                do
                {
                    int nticktype = 0;
                    if (this.InValidTODRange(currentdate, startofday, endofday))
                    {
                        this.CalcTickMark(result, currentdate, nticktype, 13, 13, 0);
                    }
                    for (int i = currentdate.Get(13); i < num3; i++)
                    {
                        currentdate.Add(13, 1);
                        if ((currentdate.Get(13) % minortickspermajor) == 0)
                        {
                            nticktype = 0;
                        }
                        else
                        {
                            if ((currentdate.Get(13) % minorincrement) != 0)
                            {
                                continue;
                            }
                            nticktype = 1;
                        }
                        if (this.InValidTODRange(currentdate, startofday, endofday))
                        {
                            this.CalcTickMark(result, currentdate, nticktype, 13, 13, 0);
                        }
                    }
                    currentdate.Add(13, 1);
                    if (((currentdate.Get(11) == 0) && (currentdate.Get(12) == 0)) && (currentdate.Get(13) == 0))
                    {
                        this.InitNewDay(currentdate, startofday, endofday);
                    }
                    this.JumpForwardToValidTOD(currentdate, startofday, endofday);
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar4));
                this.ProcessLastTick(currentdate, result, calendar4, 13, modulus);
            }
        }

        private void DefineTimeAxisTics()
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                base.ResetAxisTicks();
                if ((timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart() != null) && (timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop() != null))
                {
                    ChartCalendar currentdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                    ChartCalendar result = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                    ChartCalendar calendar3 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                    new ChartCalendar();
                    new ChartCalendar();
                    new ChartCalendar();
                    ChartCalendar.CalendarWeekAdjust(result, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), false);
                    ChartCalendar.CalendarWeekAdjust(calendar3, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                    switch (this.axisTickMarkTimeBase)
                    {
                        case 6:
                            this.DefineMillisecondAxisTicks(currentdate, 13, 1, 100);
                            return;

                        case 7:
                            this.DefineMillisecondAxisTicks(currentdate, 13, 1, 100);
                            return;

                        case 8:
                            this.DefineMillisecondAxisTicks(currentdate, 13, 1, 100);
                            return;

                        case 9:
                            this.DefineMillisecondAxisTicks(currentdate, 13, 1, 100);
                            return;

                        case 10:
                            this.DefineSecondMillisecondAxisTicks(currentdate, 13, 1, 100);
                            return;

                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 0x10:
                            this.DefineSecondSecondAxisTicks(currentdate, 13, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x11:
                        case 0x12:
                        case 0x13:
                        case 20:
                        case 0x15:
                        case 0x16:
                            this.DefineMinuteSecondAxisTicks(currentdate, 13, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x17:
                        case 0x18:
                        case 0x1f:
                        case 0x20:
                        case 0x21:
                        case 0x22:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                        case 0x2e:
                        case 0x33:
                        case 0x34:
                        case 0x35:
                        case 0x36:
                        case 0x3d:
                        case 0x3e:
                        case 0x3f:
                        case 0x40:
                        case 0x41:
                        case 0x42:
                        case 0x43:
                        case 0x44:
                        case 0x47:
                        case 0x48:
                        case 0x49:
                        case 0x4a:
                        case 0x4b:
                        case 0x4c:
                        case 0x4d:
                        case 0x4e:
                        case 0x4f:
                        case 80:
                        case 0x51:
                        case 0x52:
                        case 0x53:
                        case 0x54:
                        case 0x55:
                        case 0x56:
                        case 0x57:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6a:
                        case 0x6b:
                        case 0x6c:
                            return;

                        case 0x19:
                        case 0x1a:
                        case 0x1b:
                        case 0x1c:
                        case 0x1d:
                        case 30:
                            this.DefineMinuteMinuteAxisTicks(currentdate, 12, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x23:
                        case 0x24:
                        case 0x25:
                        case 0x26:
                        case 0x27:
                        case 40:
                            this.DefineHourMinuteAxisTicks(currentdate, 12, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x2f:
                        case 0x30:
                        case 0x31:
                        case 50:
                            this.DefineHourHourAxisTicks(currentdate, 11, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x37:
                        case 0x38:
                        case 0x39:
                        case 0x3a:
                        case 0x3b:
                            this.DefineDayHourAxisTicks(currentdate, 11, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 60:
                            this.DefineDayAxisTicks(currentdate, 6, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x45:
                            this.DefineWeekDayAxisTicks(currentdate, 6, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 70:
                            this.DefineWeekAxisTicks(currentdate, 6, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x58:
                            this.DefineMonthDayAxisTicks(currentdate, 6, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x59:
                            this.DefineMonthWeekAxisTicks(currentdate, 6, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 90:
                            this.DefineMonthAxisTicks(currentdate, 2, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x63:
                            this.DefineQuarterMonthAxisTicks(currentdate, 2, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 100:
                            this.DefineQuarterAxisTicks(currentdate, 2, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x6d:
                            this.DefineYearMonthAxisTicks(currentdate, 2, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 110:
                            this.DefineYearQuarterAxisTicks(currentdate, 2, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;

                        case 0x6f:
                        case 0x70:
                        case 0x71:
                        case 0x72:
                        case 0x73:
                            this.DefineYearYearAxisTicks(currentdate, 1, base.axisMinorTicksPerMajor, base.axisMinorNthTick);
                            return;
                    }
                }
            }
        }

        private void DefineWeekAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarTruncate(currentdate, 6);
                this.RoundDateUp(currentdate, 3, 7);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(6, 1);
                    }
                    else
                    {
                        this.CalcTickMark(startdate, currentdate, 0, 3, 3, 1);
                    }
                }
            }
        }

        private void DefineWeekDayAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                do
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(6, 1);
                    }
                    else
                    {
                        if (ChartCalendar.CalendarCheckMin(currentdate, 7, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8))
                        {
                            nticktype = 0;
                        }
                        else
                        {
                            nticktype = 1;
                        }
                        this.CalcTickMark(startdate, currentdate, nticktype, 3, 6, 1);
                    }
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2));
            }
        }

        private void DefineYearMonthAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                this.RoundDateUp(currentdate, 2, 2);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    int nticktype = this.GetTickType(currentdate, 2, 0x3e8);
                    ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                    if (this.InValidTimeScaleRange(currentdate))
                    {
                        this.CalcTickMark(startdate, currentdate, nticktype, 2, 2, 1);
                    }
                    else
                    {
                        currentdate.Add(2, 1);
                    }
                }
            }
        }

        private void DefineYearQuarterAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                this.RoundDateUp(currentdate, 2, 5);
                if ((((currentdate.Get(2) - 1) + Math.Max(0, this.quarterOffset)) % 3) != 0)
                {
                    currentdate.Add(2, 1);
                    if ((((currentdate.Get(2) - 1) + Math.Max(0, this.quarterOffset)) % 3) != 0)
                    {
                        currentdate.Add(2, 1);
                    }
                }
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    nticktype = this.GetTickType(currentdate, 2, 4);
                    ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                    if (this.InValidTimeScaleRange(currentdate))
                    {
                        this.CalcTickMark(startdate, currentdate, nticktype, 2, 2, 3);
                    }
                    else
                    {
                        currentdate.Add(2, 1);
                    }
                }
            }
        }

        private void DefineYearYearAxisTicks(ChartCalendar currentdate, int timebase, int minortickspermajor, int minorincrement)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar startdate = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStart());
                ChartCalendar calendar2 = ChartCalendar.NewCalendar(timeCoordinates.GetTimeScale(base.axisType).GetScaleDateStop());
                ChartCalendar.CalendarTruncate(currentdate, timebase);
                while (ChartCalendar.CalendarCompare2(currentdate, calendar2))
                {
                    if (!this.InValidTimeScaleRange(currentdate))
                    {
                        currentdate.Add(timebase, minorincrement);
                    }
                    else
                    {
                        int num;
                        if ((currentdate.Get(timebase) % minortickspermajor) == 0)
                        {
                            num = 0;
                        }
                        else
                        {
                            num = 1;
                        }
                        this.CalcTickMark(startdate, currentdate, num, timebase, timebase, base.axisMinorNthTick);
                    }
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (this.ErrorCheck(0) == 0)
            {
                base.thePath = new GraphicsPath();
                timeCoordinates.ChartTransform(g2);
                timeCoordinates.SetClippingArea(base.chartObjClipping);
                this.DrawTimeAxis(base.thePath);
                if (this.GetChartObjEnable() == 1)
                {
                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), base.thePath);
                }
            }
        }

        private void DrawTimeAxis(GraphicsPath path)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                timeCoordinates.SetCurrentAttributes(base.chartObjAttributes);
                if (base.axisLineEnable)
                {
                    this.DrawAxisLine(path);
                }
                if (base.axisTicksEnable)
                {
                    this.DefineTimeAxisTics();
                }
                this.DrawAxis(path);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if ((nerror == 0) && (timeCoordinates == null))
            {
                nerror = 20;
            }
            return base.ErrorCheck(nerror);
        }

        public int GetAxisTickMarkTimeBase()
        {
            return this.axisTickMarkTimeBase;
        }

        public double GetAxisTickSpace()
        {
            return this.axisTickSpace;
        }

        public override AxisLabels GetCompatibleAxisLabels()
        {
            return new TimeAxisLabels(this);
        }

        private int GetTickType(ChartCalendar currentdate, int nbase, int modulus)
        {
            int num = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates == null)
            {
                return num;
            }
            if (ChartCalendar.CalendarCheckMin(currentdate, nbase, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), modulus))
            {
                return 0;
            }
            return 1;
        }

        public ChartCalendar GetTimeAxisMax()
        {
            return new ChartCalendar((long) base.axisMax, true);
        }

        public ChartCalendar GetTimeAxisMin()
        {
            return new ChartCalendar((long) base.axisMin, true);
        }

        public TimeCoordinates GetTimeCoordinates()
        {
            TimeCoordinates coordinates = null;
            PhysicalCoordinates chartObjScale = this.GetChartObjScale();
            if (ChartSupport.IsKindOf(chartObjScale, "TimeCoordinates"))
            {
                coordinates = (TimeCoordinates) chartObjScale;
            }
            return coordinates;
        }

        public ChartCalendar GetTimeLabelsOrigin()
        {
            return (ChartCalendar) this.timeLabelsOrigin.Clone();
        }

        private void InitCurrentTODRange(ChartCalendar currentdate, ChartCalendar startofday, ChartCalendar endofday)
        {
            ChartCalendar ddate = (ChartCalendar) currentdate.Clone();
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar.SetTODMsecs(ddate, timeCoordinates.GetTimeScale(base.axisType).GetScaleStartTOD());
                ChartCalendar.CalendarWeekAdjust(ddate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarCopy(endofday, ddate);
                ChartCalendar.CalendarCopy(startofday, ddate);
                ChartCalendar.SetTODMsecs(endofday, timeCoordinates.GetTimeScale(base.axisType).GetScaleStopTOD());
                ChartCalendar.SetTODMsecs(startofday, timeCoordinates.GetTimeScale(base.axisType).GetScaleStartTOD());
            }
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x67;
        }

        private void InitNewDay(ChartCalendar currentdate, ChartCalendar startofday, ChartCalendar endofday)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                ChartCalendar.SetTODMsecs(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetScaleStartTOD());
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarCopy(endofday, currentdate);
                ChartCalendar.CalendarCopy(startofday, currentdate);
                ChartCalendar.SetTODMsecs(endofday, timeCoordinates.GetTimeScale(base.axisType).GetScaleStopTOD());
                ChartCalendar.SetTODMsecs(startofday, timeCoordinates.GetTimeScale(base.axisType).GetScaleStartTOD());
            }
        }

        public void InitTimeAxis(TimeCoordinates transform, int axtype)
        {
            this.InitDefaults();
            if (transform != null)
            {
                base.InitAxis(transform, axtype, transform.GetStart(axtype), transform.GetStop(axtype));
            }
        }

        private bool InValidTimeScaleRange(ChartCalendar currentdate)
        {
            bool flag = false;
            long calendarMsecs = currentdate.GetCalendarMsecs();
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if ((timeCoordinates != null) && ((calendarMsecs >= timeCoordinates.GetTimeScale(base.axisType).ScaleDateMin.GetCalendarMsecs()) && (calendarMsecs <= timeCoordinates.GetTimeScale(base.axisType).ScaleDateMax.GetCalendarMsecs())))
            {
                flag = true;
            }
            return flag;
        }

        private bool InValidTODRange(ChartCalendar currentdate, ChartCalendar startofday, ChartCalendar endofday)
        {
            bool flag = false;
            long calendarMsecs = currentdate.GetCalendarMsecs();
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if ((timeCoordinates != null) && (((calendarMsecs >= startofday.GetCalendarMsecs()) && (calendarMsecs < endofday.GetCalendarMsecs())) && ((calendarMsecs >= timeCoordinates.GetTimeScale(base.axisType).ScaleDateMin.GetCalendarMsecs()) && (calendarMsecs <= timeCoordinates.GetTimeScale(base.axisType).ScaleDateMax.GetCalendarMsecs()))))
            {
                flag = true;
            }
            return flag;
        }

        private bool InValidTODRange2(ChartCalendar currentdate, ChartCalendar startofday, ChartCalendar endofday)
        {
            bool flag = false;
            long calendarMsecs = currentdate.GetCalendarMsecs();
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if ((timeCoordinates != null) && (((calendarMsecs >= startofday.GetCalendarMsecs()) && (calendarMsecs <= endofday.GetCalendarMsecs())) && ((calendarMsecs >= timeCoordinates.GetTimeScale(base.axisType).ScaleDateMin.GetCalendarMsecs()) && (calendarMsecs <= timeCoordinates.GetTimeScale(base.axisType).ScaleDateMax.GetCalendarMsecs()))))
            {
                flag = true;
            }
            return flag;
        }

        private void JumpForwardToValidTOD(ChartCalendar currentdate, ChartCalendar startofday, ChartCalendar endofday)
        {
            this.GetTimeCoordinates();
            if (ChartCalendar.CalendarCompare(currentdate, startofday))
            {
                this.InitNewDay(currentdate, startofday, endofday);
            }
            else if (ChartCalendar.CalendarCompare(endofday, currentdate))
            {
                currentdate.Add(6, 1);
                this.InitNewDay(currentdate, startofday, endofday);
            }
        }

        private void ProcessLastTick(ChartCalendar currentdate, ChartCalendar startdate, ChartCalendar stopdate, int nbase, int modulus)
        {
            int nticktype = 0;
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if ((currentdate.GetTODMsecs() == timeCoordinates.GetScaleStartTOD()) || (currentdate.GetTODMsecs() == timeCoordinates.GetScaleStopTOD()))
            {
                TickMark mark = (TickMark) base.axisTicksArrayList[base.axisTicksArrayList.Count - 1];
                if ((currentdate.GetCalendarMsecs() > mark.GetTickDate().GetCalendarMsecs()) && this.InValidTODRange2(currentdate, startdate, stopdate))
                {
                    this.CalcTickMark(startdate, currentdate, nticktype, nbase, nbase, 1);
                }
            }
        }

        private void RoundDateUp(ChartCalendar currentdate, int nbase, int nroundbase)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                if (!ChartCalendar.CalendarCheckMin(currentdate, nroundbase, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), 0x3e8))
                {
                    currentdate.Add(nbase, 1);
                    currentdate.Set(nroundbase, currentdate.GetMinimum(nroundbase));
                }
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
            }
        }

        public void SetAxisTickMarkTimeBase(int ntickmarkbase)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                this.axisTickMarkTimeBase = ntickmarkbase;
                if (timeCoordinates != null)
                {
                    Size timeAxisTickParameters = ChartCalendar.GetTimeAxisTickParameters(this.axisTickMarkTimeBase, timeCoordinates.GetTimeScale(base.axisType).GetWeekType());
                    base.axisMinorNthTick = timeAxisTickParameters.Height;
                    base.axisMinorTicksPerMajor = timeAxisTickParameters.Width;
                }
            }
        }

        public void SetAxisTickSpace(double tickspace)
        {
            this.axisTickSpace = tickspace;
        }

        public void SetTimeAxisMax(ChartCalendar stop)
        {
            base.axisMax = stop.GetCalendarMsecs();
        }

        public void SetTimeAxisMin(ChartCalendar start)
        {
            base.axisMin = start.GetCalendarMsecs();
        }

        public void SetTimeLabelsOrigin(ChartCalendar origin)
        {
            this.timeLabelsOrigin = (ChartCalendar) origin.Clone();
        }

        private void StartNewDay(ChartCalendar currentdate, long ncount, int nbase, ChartCalendar startofday, ChartCalendar endofday)
        {
            TimeCoordinates timeCoordinates = this.GetTimeCoordinates();
            if (timeCoordinates != null)
            {
                if ((ncount > 0L) && (currentdate.Get(11) > 0))
                {
                    currentdate.Add(6, 1);
                    ChartCalendar.SetTODMsecs(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetScaleStartTOD());
                }
                ChartCalendar.CalendarWeekAdjust(currentdate, timeCoordinates.GetTimeScale(base.axisType).GetWeekType(), true);
                ChartCalendar.CalendarCopy(endofday, currentdate);
                ChartCalendar.CalendarCopy(startofday, currentdate);
                ChartCalendar.SetTODMsecs(endofday, timeCoordinates.GetTimeScale(base.axisType).GetScaleStopTOD());
                ChartCalendar.SetTODMsecs(startofday, timeCoordinates.GetTimeScale(base.axisType).GetScaleStartTOD());
            }
        }

        public int AxisTickMarkTimeBase
        {
            get
            {
                return this.axisTickMarkTimeBase;
            }
            set
            {
                this.SetAxisTickMarkTimeBase(value);
            }
        }

        public int QuarterOffset
        {
            get
            {
                return this.quarterOffset;
            }
            set
            {
                this.quarterOffset = value;
            }
        }

        public ChartCalendar TimeAxisMax
        {
            get
            {
                return this.GetTimeAxisMax();
            }
            set
            {
                this.SetTimeAxisMax(value);
            }
        }

        public ChartCalendar TimeAxisMin
        {
            get
            {
                return this.GetTimeAxisMin();
            }
            set
            {
                this.SetTimeAxisMin(value);
            }
        }

        public ChartCalendar TimeLabelsOrigin
        {
            get
            {
                return this.GetTimeLabelsOrigin();
            }
            set
            {
                this.SetTimeLabelsOrigin(value);
            }
        }
    }
}

