namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class TimeAutoScale : AutoScale
    {
        private ChartCalendar dateStart;
        private ChartCalendar dateStop;
        private TimeScale theTimeScale;
        internal double tickInterval;
        private int timeBase;
        private ChartCalendar timeLabelsOrigin;
        private int timeMinorNthTick;

        public TimeAutoScale()
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
        }

        public TimeAutoScale(TimeScale timescale)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.theTimeScale = timescale;
        }

        public TimeAutoScale(TimeCoordinates transform, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.theTimeScale = transform.GetTimeScale(naxis);
            base.SetChartAutoScale(transform, naxis, nmode);
            this.CalcChartAutoScaleTransform();
        }

        public TimeAutoScale(TimeCoordinates transform, TimeGroupDataset dataset, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.theTimeScale = transform.GetTimeScale(naxis);
            base.theChartCoordinates = transform;
            base.SetChartAutoScale(dataset, naxis, nmode);
            this.CalcChartAutoScaleDataset();
        }

        public TimeAutoScale(TimeCoordinates transform, TimeGroupDataset[] datasets, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.theTimeScale = transform.GetTimeScale(naxis);
            base.theChartCoordinates = transform;
            base.SetChartAutoScale(datasets, naxis, nmode);
            this.CalcChartAutoScaleDatasets();
        }

        public TimeAutoScale(TimeCoordinates transform, TimeSimpleDataset dataset, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.theTimeScale = transform.GetTimeScale(naxis);
            base.theChartCoordinates = transform;
            base.SetChartAutoScale(dataset, naxis, nmode);
            this.CalcChartAutoScaleDataset();
        }

        public TimeAutoScale(TimeCoordinates transform, TimeSimpleDataset[] datasets, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.theTimeScale = transform.GetTimeScale(naxis);
            base.theChartCoordinates = transform;
            base.SetChartAutoScale(datasets, naxis, nmode);
            this.CalcChartAutoScaleDatasets();
        }

        public TimeAutoScale(TimeCoordinates transform, ChartCalendar dstart, ChartCalendar dstop, int naxis, int nmode)
        {
            this.tickInterval = 1.0;
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            this.dateStart = new ChartCalendar();
            this.dateStop = new ChartCalendar();
            this.timeLabelsOrigin = new ChartCalendar();
            this.theTimeScale = null;
            this.InitDefaults();
            this.dateStart = (ChartCalendar) dstart.Clone();
            this.dateStop = (ChartCalendar) dstop.Clone();
            this.theTimeScale = transform.GetTimeScale(naxis);
            base.SetChartAutoScale(transform, naxis, nmode);
            this.CalcChartAutoScaleInitialValues();
        }

        private void AdjustMillisecondTimeEndpoints(ChartCalendar dstart, ChartCalendar dstop, int nroundmode, int nroundfar, int nroundnear)
        {
            double calendarMsecs = dstart.GetCalendarMsecs();
            double rmax = dstop.GetCalendarMsecs();
            double num3 = this.theTimeScale.CalendarSecsDiff(dstart, dstop);
            if (num3 < (rmax - calendarMsecs))
            {
                long scaleStartTOD = this.theTimeScale.GetScaleStartTOD();
                this.theTimeScale.GetScaleStopTOD();
                ChartCalendar calendar = (ChartCalendar) dstop.Clone();
                calendar.SetTODMsecs(scaleStartTOD);
                LinearAutoScale scale = new LinearAutoScale(calendarMsecs, calendarMsecs + num3, base.axisType, nroundmode);
                scale.CalcChartAutoScaleInitialValues();
                ChartCalendar.SetCalendarMsecs(dstart, (long) scale.GetFinalMin());
                scale = new LinearAutoScale(rmax - num3, rmax, base.axisType, nroundmode);
                scale.CalcChartAutoScaleInitialValues();
                ChartCalendar.SetCalendarMsecs(dstop, (long) scale.GetFinalMax());
                base.labelsOrigin = calendar.GetCalendarMsecs();
                scale = new LinearAutoScale(0.0, num3 / 2.0, base.axisType, nroundmode);
                scale.CalcChartAutoScaleInitialValues();
                this.tickInterval = scale.GetTickInterval();
                base.axisMinorTicksPerMajor = scale.GetAxisMinorTicksPerMajor();
            }
            else
            {
                LinearAutoScale scale2 = new LinearAutoScale(calendarMsecs, rmax, base.axisType, nroundmode);
                scale2.CalcChartAutoScaleInitialValues();
                ChartCalendar.SetCalendarMsecs(dstart, (long) scale2.GetFinalMin());
                ChartCalendar.SetCalendarMsecs(dstop, (long) scale2.GetFinalMax());
                base.labelsOrigin = scale2.GetLabelsOrigin();
                this.tickInterval = scale2.GetTickInterval();
                base.axisMinorTicksPerMajor = scale2.GetAxisMinorTicksPerMajor();
            }
        }

        public void AdjustTimeAxisRange()
        {
            this.AdjustTimeAxisRange(this.timeBase, base.roundMode);
        }

        public void AdjustTimeAxisRange(int ntimebase, int nroundmode)
        {
            switch (ntimebase)
            {
                case 6:
                    this.AdjustMillisecondTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 14, 14);
                    break;

                case 7:
                    this.AdjustMillisecondTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 14, 14);
                    break;

                case 8:
                    this.AdjustMillisecondTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 14, 14);
                    break;

                case 9:
                    this.AdjustMillisecondTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 14, 14);
                    break;

                case 10:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 13, 14);
                    break;

                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 0x10:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 13, 13);
                    break;

                case 0x11:
                case 0x12:
                case 0x13:
                case 20:
                case 0x15:
                case 0x16:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 12, 13);
                    break;

                case 0x19:
                case 0x1a:
                case 0x1b:
                case 0x1c:
                case 0x1d:
                case 30:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 12, 13);
                    break;

                case 0x23:
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 40:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 11, 12);
                    this.AdjustTimeLabelsOrigin(this.dateStart, 2, 11);
                    break;

                case 0x2f:
                case 0x30:
                case 0x31:
                case 50:
                case 0x33:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 11, 12);
                    this.AdjustTimeLabelsOrigin(this.dateStart, 2, 11);
                    break;

                case 0x37:
                case 0x38:
                case 0x39:
                case 0x3a:
                case 0x3b:
                case 60:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 6, 11);
                    this.AdjustTimeLabelsOrigin(this.dateStart, 2, 6);
                    break;

                case 0x45:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 3, 6);
                    break;

                case 70:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 3, 3);
                    break;

                case 0x58:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 2, 6);
                    break;

                case 0x59:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 2, 3);
                    break;

                case 90:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 2, 2);
                    break;

                case 0x63:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 1, 2);
                    break;

                case 100:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 1, 2);
                    break;

                case 0x6d:
                case 110:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 1, 2);
                    break;

                case 0x6f:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                    this.AdjustTimeEndpoints(this.dateStart, this.dateStop, nroundmode, 1, 1);
                    break;
            }
            base.finalMax = this.dateStop.GetCalendarMsecs();
            base.finalMin = this.dateStart.GetCalendarMsecs();
        }

        private void AdjustTimeEndpoints(ChartCalendar dstart, ChartCalendar dstop, int nroundmode, int nroundfar, int nroundnear)
        {
            long scaleStartTOD = this.theTimeScale.GetScaleStartTOD();
            long scaleStopTOD = this.theTimeScale.GetScaleStopTOD();
            dstop.Get(5);
            switch (nroundmode)
            {
                case 1:
                    ChartCalendar.CalendarTruncate(dstart, nroundnear);
                    ChartCalendar.CalendarTruncate(dstop, nroundnear);
                    dstop.Add(nroundnear, 1);
                    break;

                case 2:
                    ChartCalendar.CalendarTruncate(dstart, nroundfar);
                    ChartCalendar.CalendarCeil(dstop, nroundfar);
                    break;
            }
            if (ChartCalendar.GetTODMsecs(dstart) < scaleStartTOD)
            {
                ChartCalendar.SetTODMsecs(dstart, scaleStartTOD);
            }
            if (ChartCalendar.GetTODMsecs(dstop) > scaleStopTOD)
            {
                ChartCalendar.SetTODMsecs(dstop, scaleStopTOD);
            }
        }

        public ChartCalendar AdjustTimeLabelsOrigin(ChartCalendar dstart, int nroundmode, int nround)
        {
            this.timeLabelsOrigin = ChartCalendar.NewCalendar(dstart);
            switch (nroundmode)
            {
                case 1:
                    ChartCalendar.CalendarCeil(this.timeLabelsOrigin, nround);
                    break;

                case 2:
                    ChartCalendar.CalendarCeil(this.timeLabelsOrigin, nround);
                    break;
            }
            return this.timeLabelsOrigin;
        }

        public override void CalcChartAutoScaleDataset()
        {
            if (base.theDataset != null)
            {
                base.CalcDatasetRange();
                ChartCalendar.SetCalendarMsecs(this.dateStart, (long) base.initialMin);
                ChartCalendar.SetCalendarMsecs(this.dateStop, (long) base.initialMax);
                this.CalcTimeBaseValues();
                this.AdjustTimeAxisRange();
            }
        }

        public override void CalcChartAutoScaleDatasets()
        {
            if (base.theDatasetsArray != null)
            {
                base.CalcDatasetsRange();
                ChartCalendar.SetCalendarMsecs(this.dateStart, (long) base.initialMin);
                ChartCalendar.SetCalendarMsecs(this.dateStop, (long) base.initialMax);
                this.CalcTimeBaseValues();
                this.AdjustTimeAxisRange();
            }
        }

        public override void CalcChartAutoScaleInitialValues()
        {
            if (base.theChartCoordinates != null)
            {
                this.CalcTimeBaseValues();
                this.AdjustTimeAxisRange();
            }
        }

        public override void CalcChartAutoScaleTransform()
        {
            if (base.theChartCoordinates != null)
            {
                base.initialMin = base.theChartCoordinates.GetStart(base.axisType);
                base.initialMax = base.theChartCoordinates.GetStop(base.axisType);
                ChartCalendar.SetCalendarMsecs(this.dateStart, (long) base.initialMin);
                ChartCalendar.SetCalendarMsecs(this.dateStop, (long) base.initialMax);
                this.CalcTimeBaseValues();
                this.AdjustTimeAxisRange();
            }
        }

        public override void CalcRoundAxisValues(double raxmin, double raxmax, int nroundmode)
        {
            base.initialMin = raxmin;
            base.initialMax = raxmax;
            double initialMax = base.initialMax;
            double initialMin = base.initialMin;
            base.initialMax += base.maxRangeAdjust;
            base.initialMin -= base.minRangeAdjust;
            ChartCalendar.SetCalendarMsecs(this.dateStart, (long) base.initialMin);
            this.theTimeScale.SetScaleDateStart(this.dateStart);
            ChartCalendar.SetCalendarMsecs(this.dateStop, (long) base.initialMax);
            this.theTimeScale.SetScaleDateStop(this.dateStop);
            this.CalcTimeBaseValues();
            int timeScaleBase = this.GetTimeScaleBase();
            this.AdjustTimeAxisRange(timeScaleBase, nroundmode);
        }

        public void CalcRoundTimeAxisValues(ChartCalendar dmin, ChartCalendar dmax, int nroundmode)
        {
            this.dateStart = ChartCalendar.NewCalendar(dmin);
            this.dateStop = ChartCalendar.NewCalendar(dmax);
            base.initialMin = this.dateStart.GetCalendarMsecs();
            base.initialMax = this.dateStop.GetCalendarMsecs();
            this.CalcTimeBaseValues(this.dateStart, this.dateStop);
            int timeScaleBase = this.GetTimeScaleBase();
            this.AdjustTimeAxisRange(timeScaleBase, nroundmode);
        }

        public void CalcTimeBaseValues()
        {
            this.CalcTimeBaseValues(this.dateStart, this.dateStop);
        }

        public void CalcTimeBaseValues(ChartCalendar dmin, ChartCalendar dmax)
        {
            long num = 0x186a0L;
            long millisecondsPerDay = 0x5265c00L;
            long num3 = 0x3e8L;
            long num4 = 60L * num3;
            long num5 = 60L * num4;
            long num6 = 0x16dL * millisecondsPerDay;
            Size timeAxisTickParameters = new Size(1, 1);
            if (this.theTimeScale != null)
            {
                millisecondsPerDay = this.theTimeScale.GetMillisecondsPerDay();
                num6 = 0x16dL * millisecondsPerDay;
                num = Math.Abs(this.theTimeScale.CalendarSecsDiff(dmin, dmax));
            }
            this.timeMinorNthTick = 1;
            this.timeBase = 0x6f;
            base.axisMinorTicksPerMajor = 1;
            this.timeBase = 11;
            if (num < (num3 / 10L))
            {
                this.timeBase = 6;
            }
            else if (num < (num3 / 2L))
            {
                this.timeBase = 7;
            }
            else if (num < (num3 * 2L))
            {
                this.timeBase = 8;
            }
            else if (num < (8L * num3))
            {
                this.timeBase = 10;
            }
            else if (num < (10L * num3))
            {
                this.timeBase = 12;
            }
            else if (num < (15L * num3))
            {
                this.timeBase = 12;
            }
            else if (num < (30L * num3))
            {
                this.timeBase = 13;
            }
            else if (num < num4)
            {
                this.timeBase = 14;
            }
            else if (num < (2L * num4))
            {
                this.timeBase = 15;
            }
            else if (num < (4L * num4))
            {
                this.timeBase = 0x13;
            }
            else if (num < (7L * num4))
            {
                this.timeBase = 20;
            }
            else if (num < (10L * num4))
            {
                this.timeBase = 0x15;
            }
            else if (num < (15L * num4))
            {
                this.timeBase = 0x15;
            }
            else if (num < (30L * num4))
            {
                this.timeBase = 0x1b;
            }
            else if (num < num5)
            {
                this.timeBase = 0x1c;
            }
            else if (num < (2L * num5))
            {
                this.timeBase = 0x1d;
            }
            else if (num < (4L * num5))
            {
                this.timeBase = 0x25;
            }
            else if (num < (7L * num5))
            {
                this.timeBase = 0x26;
            }
            else if (num < (10L * num5))
            {
                this.timeBase = 0x27;
            }
            else if (num < (num5 * 15L))
            {
                this.timeBase = 0x27;
            }
            else if (num < (num5 * 30L))
            {
                this.timeBase = 0x31;
            }
            else if (num < (2L * millisecondsPerDay))
            {
                this.timeBase = 50;
            }
            else if (num < (5L * millisecondsPerDay))
            {
                this.timeBase = 0x38;
            }
            else if (num < (10L * millisecondsPerDay))
            {
                this.timeBase = 0x38;
            }
            else if (num < (20L * millisecondsPerDay))
            {
                this.timeBase = 0x39;
            }
            else if (num < (50L * millisecondsPerDay))
            {
                this.timeBase = 0x45;
            }
            else if (num < (60L * millisecondsPerDay))
            {
                this.timeBase = 0x59;
            }
            else if (num < (180L * millisecondsPerDay))
            {
                this.timeBase = 0x59;
            }
            else if (num < num6)
            {
                this.timeBase = 0x63;
            }
            else if (num < (2L * num6))
            {
                this.timeBase = 0x63;
            }
            else if (num < (5L * num6))
            {
                this.timeBase = 0x6d;
            }
            else if (num < (10L * num6))
            {
                this.timeBase = 110;
            }
            else if (num < (30L * num6))
            {
                this.timeBase = 0x70;
            }
            else if (num < (50L * num6))
            {
                this.timeBase = 0x71;
            }
            else if (num < (100L * num6))
            {
                this.timeBase = 0x72;
            }
            else
            {
                this.timeBase = 0x73;
            }
            timeAxisTickParameters = ChartCalendar.GetTimeAxisTickParameters(this.timeBase, this.theTimeScale.GetWeekType());
            this.timeMinorNthTick = timeAxisTickParameters.Height;
            base.axisMinorTicksPerMajor = timeAxisTickParameters.Width;
        }

        public override object Clone()
        {
            TimeAutoScale scale = new TimeAutoScale();
            scale.Copy(this);
            return scale;
        }

        public void Copy(TimeAutoScale source)
        {
            base.Copy(source);
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public ChartCalendar GetDateStart()
        {
            return this.dateStart;
        }

        public ChartCalendar GetDateStop()
        {
            return this.dateStop;
        }

        public double GetTickInterval()
        {
            return this.tickInterval;
        }

        public ChartCalendar GetTimeLabelsOrigin()
        {
            return this.timeLabelsOrigin;
        }

        public int GetTimeMajorNthTick()
        {
            base.majorNthTick = 1;
            return base.majorNthTick;
        }

        public int GetTimeMinorNthTick()
        {
            this.CalcTimeBaseValues();
            return this.timeMinorNthTick;
        }

        public virtual int GetTimeScaleBase()
        {
            this.CalcTimeBaseValues();
            return this.timeBase;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0xcb;
        }

        public void SetTimeScale(TimeScale timescale)
        {
            this.theTimeScale = timescale;
        }
    }
}

