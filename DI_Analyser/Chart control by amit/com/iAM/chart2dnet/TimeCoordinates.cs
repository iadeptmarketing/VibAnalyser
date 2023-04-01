namespace com.iAM.chart2dnet
{
    using System;

    public class TimeCoordinates : PhysicalCoordinates
    {
        internal long scaleStartTOD;
        internal long scaleStopTOD;
        internal int timeAxis;
        internal int weekType;

        public TimeCoordinates()
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
        }

        public TimeCoordinates(int xscale, int yscale)
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
            this.SetTimeScaleTransforms(xscale, yscale);
        }

        public TimeCoordinates(ChartCalendar dstart, double y1, ChartCalendar dstop, double y2)
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
            this.SetTimeScaleTransforms(2, 0);
            this.timeAxis = 0;
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, 0);
        }

        public TimeCoordinates(ChartCalendar dstart, double y1, ChartCalendar dstop, double y2, int nweektype)
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
            this.SetTimeScaleTransforms(2, 0);
            this.timeAxis = 0;
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, nweektype);
        }

        public TimeCoordinates(ChartCalendar dstart, double y1, ChartCalendar dstop, double y2, int ntimeaxis, int nweektype)
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
            if (ntimeaxis == 0)
            {
                this.SetTimeScaleTransforms(2, 0);
            }
            else
            {
                this.SetTimeScaleTransforms(0, 2);
            }
            this.timeAxis = ntimeaxis;
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, nweektype);
        }

        public TimeCoordinates(ChartCalendar dstart, long starttime, double y1, ChartCalendar dstop, long stoptime, double y2, int nweektype)
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
            this.timeAxis = 0;
            this.scaleStartTOD = starttime;
            this.scaleStopTOD = stoptime;
            if (this.timeAxis == 0)
            {
                this.SetTimeScaleTransforms(2, 0);
            }
            else
            {
                this.SetTimeScaleTransforms(0, 2);
            }
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, nweektype);
        }

        public TimeCoordinates(ChartCalendar dstart, long starttime, double y1, ChartCalendar dstop, long stoptime, double y2, int ntimeaxis, int nweektype)
        {
            this.weekType = 0;
            this.scaleStartTOD = 0L;
            this.scaleStopTOD = 0x5265c00L;
            this.timeAxis = 0;
            this.InitDefaults();
            this.timeAxis = ntimeaxis;
            this.scaleStartTOD = starttime;
            this.scaleStopTOD = stoptime;
            if (this.timeAxis == 0)
            {
                this.SetTimeScaleTransforms(2, 0);
            }
            else
            {
                this.SetTimeScaleTransforms(0, 2);
            }
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, nweektype);
        }

        public override void AutoScale(ChartDataset dataset)
        {
            this.CalcTimeAutoScale(dataset, 2, 2);
        }

        public override void AutoScale(ChartDataset[] datasets)
        {
            this.CalcTimeAutoScale(datasets, 2, 2);
        }

        public void AutoScale(TimeGroupDataset dataset)
        {
            this.CalcTimeAutoScale(dataset, 2, 2);
        }

        public void AutoScale(TimeSimpleDataset dataset)
        {
            this.CalcTimeAutoScale(dataset, 2, 2);
        }

        public void AutoScale(TimeGroupDataset[] datasets)
        {
            this.CalcTimeAutoScale(datasets, 2, 2);
        }

        public void AutoScale(TimeSimpleDataset[] datasets)
        {
            this.CalcTimeAutoScale(datasets, 2, 2);
        }

        public void AutoScale(TimeGroupDataset dataset, int nroundmode)
        {
            this.CalcTimeAutoScale(dataset, nroundmode, nroundmode);
        }

        public override void AutoScale(int nroundmodex, int nroundmodey)
        {
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            com.iAM.chart2dnet.AutoScale scale2 = base.yScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(this, 0, nroundmodex);
            compatibleAutoScale.CalcChartAutoScaleTransform();
            scale2.SetChartAutoScale(this, 1, nroundmodey);
            scale2.CalcChartAutoScaleTransform();
            this.InitTimeScale(compatibleAutoScale.GetFinalMin(), this.scaleStartTOD, scale2.GetFinalMin(), compatibleAutoScale.GetFinalMax(), this.scaleStopTOD, scale2.GetFinalMax(), this.timeAxis, this.weekType);
            compatibleAutoScale = null;
            scale2 = null;
        }

        public override void AutoScale(ChartDataset dataset, int nroundmodex, int nroundmodey)
        {
            this.CalcTimeAutoScale(dataset, nroundmodex, nroundmodey);
        }

        public void AutoScale(TimeGroupDataset dataset, int nroundmodex, int nroundmodey)
        {
            this.CalcTimeAutoScale(dataset, nroundmodex, nroundmodey);
        }

        public void AutoScale(TimeSimpleDataset dataset, int nroundmodex, int nroundmodey)
        {
            this.CalcTimeAutoScale(dataset, nroundmodex, nroundmodey);
        }

        public override void AutoScale(ChartDataset[] datasets, int nroundmodex, int nroundmodey)
        {
            this.CalcTimeAutoScale(datasets, nroundmodex, nroundmodey);
        }

        public void AutoScale(TimeGroupDataset[] datasets, int nroundmodex, int nroundmodey)
        {
            this.CalcTimeAutoScale(datasets, nroundmodex, nroundmodey);
        }

        public void AutoScale(TimeSimpleDataset[] datasets, int nroundmodex, int nroundmodey)
        {
            this.CalcTimeAutoScale(datasets, nroundmodex, nroundmodey);
        }

        public void CalcTimeAutoScale(ChartDataset[] datasets, int nroundmodex, int nroundmodey)
        {
            int xCoordinateType = datasets[0].GetXCoordinateType();
            int yCoordinateType = datasets[0].GetYCoordinateType();
            if ((xCoordinateType == 0) && (base.xScale is LogScale))
            {
                xCoordinateType = 1;
            }
            if ((yCoordinateType == 0) && (base.yScale is LogScale))
            {
                yCoordinateType = 1;
            }
            this.SetTimeScaleTransforms(xCoordinateType, yCoordinateType);
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            com.iAM.chart2dnet.AutoScale scale2 = base.yScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(datasets, 0, nroundmodex);
            compatibleAutoScale.CalcChartAutoScaleDatasets();
            scale2.SetChartAutoScale(datasets, 1, nroundmodey);
            scale2.CalcChartAutoScaleDatasets();
            this.timeAxis = datasets[0].GetTimeScaleAxis();
            this.InitTimeScale(compatibleAutoScale.GetFinalMin(), this.scaleStartTOD, scale2.GetFinalMin(), compatibleAutoScale.GetFinalMax(), this.scaleStopTOD, scale2.GetFinalMax(), this.timeAxis, this.weekType);
            compatibleAutoScale = null;
            scale2 = null;
        }

        public void CalcTimeAutoScale(ChartDataset dataset, int nroundmodex, int nroundmodey)
        {
            int xCoordinateType = dataset.GetXCoordinateType();
            int yCoordinateType = dataset.GetYCoordinateType();
            if ((xCoordinateType == 0) && (base.xScale is LogScale))
            {
                xCoordinateType = 1;
            }
            if ((yCoordinateType == 0) && (base.yScale is LogScale))
            {
                yCoordinateType = 1;
            }
            this.SetTimeScaleTransforms(xCoordinateType, yCoordinateType);
            com.iAM.chart2dnet.AutoScale compatibleAutoScale = base.xScale.GetCompatibleAutoScale();
            com.iAM.chart2dnet.AutoScale scale2 = base.yScale.GetCompatibleAutoScale();
            compatibleAutoScale.SetChartAutoScale(dataset, 0, nroundmodex);
            compatibleAutoScale.CalcChartAutoScaleDataset();
            scale2.SetChartAutoScale(dataset, 1, nroundmodey);
            scale2.CalcChartAutoScaleDataset();
            this.timeAxis = dataset.GetTimeScaleAxis();
            this.InitTimeScale(compatibleAutoScale.GetFinalMin(), this.scaleStartTOD, scale2.GetFinalMin(), compatibleAutoScale.GetFinalMax(), this.scaleStopTOD, scale2.GetFinalMax(), this.timeAxis, this.weekType);
            compatibleAutoScale = null;
            scale2 = null;
        }

        public bool CheckValidDate(ChartCalendar cdate)
        {
            return ChartCalendar.CheckValidDate(cdate, this.scaleStartTOD, this.scaleStopTOD, this.weekType);
        }

        public override bool CheckValidPoint(double x, double y)
        {
            ChartCalendar calendar;
            if (this.timeAxis == 0)
            {
                calendar = new ChartCalendar((long) x, true);
            }
            else
            {
                calendar = new ChartCalendar((long) y, true);
            }
            bool flag = this.CheckValidDate(calendar);
            if (this.timeAxis == 0)
            {
                return (flag && ChartSupport.BGoodValue(y));
            }
            return (flag && ChartSupport.BGoodValue(x));
        }

        public override object Clone()
        {
            TimeCoordinates coordinates = new TimeCoordinates();
            coordinates.Copy(this);
            return coordinates;
        }

        public void Copy(TimeCoordinates source)
        {
            if (source != null)
            {
                base.Copy((PhysicalCoordinates) source);
                this.timeAxis = source.timeAxis;
                this.weekType = source.weekType;
                this.scaleStartTOD = source.scaleStartTOD;
                this.scaleStopTOD = source.scaleStopTOD;
            }
        }

        public override void Copy(object source)
        {
            if (source != null)
            {
                this.Copy((TimeCoordinates) source);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public override Axis GetCompatibleAxis(int axis)
        {
            Axis compatibleAxis;
            if (axis == 0)
            {
                compatibleAxis = base.xScale.GetCompatibleAxis();
            }
            else
            {
                compatibleAxis = base.yScale.GetCompatibleAxis();
            }
            compatibleAxis.SetAxisType(axis);
            return compatibleAxis;
        }

        public long GetScaleStartTOD()
        {
            return this.scaleStartTOD;
        }

        public override double GetScaleStartY()
        {
            return base.physPlotScale.GetY1();
        }

        public long GetScaleStopTOD()
        {
            return this.scaleStopTOD;
        }

        public override double GetScaleStopY()
        {
            return base.physPlotScale.GetY2();
        }

        public TimeScale GetTimeScale(int axis)
        {
            TimeScale xScale = null;
            if (axis == 0)
            {
                if (ChartSupport.IsKindOf(base.GetXScale(), "TimeScale"))
                {
                    xScale = (TimeScale) base.GetXScale();
                }
                return xScale;
            }
            if (ChartSupport.IsKindOf(base.GetYScale(), "TimeScale"))
            {
                xScale = (TimeScale) base.GetYScale();
            }
            return xScale;
        }

        public ChartCalendar GetTimeScaleStart()
        {
            return this.GetTimeScale(this.timeAxis).GetScaleDateStart();
        }

        public ChartCalendar GetTimeScaleStart(int axis)
        {
            return this.GetTimeScale(axis).GetScaleDateStart();
        }

        public ChartCalendar GetTimeScaleStop()
        {
            return this.GetTimeScale(this.timeAxis).GetScaleDateStop();
        }

        public ChartCalendar GetTimeScaleStop(int axis)
        {
            return this.GetTimeScale(axis).GetScaleDateStop();
        }

        public int GetWeekType()
        {
            return this.weekType;
        }

        private void InitDefaults()
        {
            this.SetTimeScaleTransforms(2, 0);
            base.chartObjType = 0x4a1;
        }

        private void InitTimeScale(double rstart, double y1, double rstop, double y2, int ntimeaxis)
        {
            this.timeAxis = ntimeaxis;
            this.InitTimeScale(rstart, this.scaleStartTOD, y1, rstop, this.scaleStopTOD, y2, this.timeAxis, this.weekType);
        }

        private void InitTimeScale(ChartCalendar dstart, long starttime, double y1, ChartCalendar dstop, long stoptime, double y2, int ntimeaxis, int nweektype)
        {
            if ((dstart != null) && (dstop != null))
            {
                this.timeAxis = ntimeaxis;
                TimeScale timeScale = this.GetTimeScale(this.timeAxis);
                timeScale.SetTimeScale(dstart, starttime, dstop, stoptime, this.timeAxis, nweektype);
                timeScale.GetTotalMilliseconds();
                double calendarMsecs = timeScale.GetScaleDateStart().GetCalendarMsecs();
                double num2 = timeScale.GetScaleDateStop().GetCalendarMsecs();
                this.timeAxis = ntimeaxis;
                if (this.timeAxis == 0)
                {
                    base.SetPhysScale(calendarMsecs, y1, num2, y2);
                }
                else
                {
                    base.SetPhysScale(y1, calendarMsecs, y2, num2);
                }
            }
        }

        private void InitTimeScale(double dstart, long starttime, double y1, double dstop, long stoptime, double y2, int ntimeaxis, int nweektype)
        {
            this.timeAxis = ntimeaxis;
            TimeScale timeScale = this.GetTimeScale(this.timeAxis);
            if (this.timeAxis == 0)
            {
                timeScale.SetTimeScale(dstart, starttime, dstop, stoptime, this.timeAxis, nweektype);
            }
            else
            {
                timeScale.SetTimeScale(y1, starttime, y2, stoptime, this.timeAxis, nweektype);
            }
            timeScale.GetTotalMilliseconds();
            double calendarMsecs = timeScale.GetScaleDateStart().GetCalendarMsecs();
            double num2 = timeScale.GetScaleDateStop().GetCalendarMsecs();
            if (this.timeAxis == 0)
            {
                base.SetPhysScale(calendarMsecs, y1, num2, y2);
            }
            else
            {
                base.SetPhysScale(dstart, calendarMsecs, dstop, num2);
            }
        }

        public void SetScaleStartTOD(long starttime)
        {
            this.scaleStartTOD = starttime;
        }

        public override void SetScaleStartX(double rX1)
        {
            this.InitTimeScale(rX1, base.physPlotScale.GetY1(), base.physPlotScale.GetX2(), base.physPlotScale.GetY2(), this.timeAxis);
        }

        public override void SetScaleStartY(double rY1)
        {
            this.InitTimeScale(base.physPlotScale.GetX1(), rY1, base.physPlotScale.GetX2(), base.physPlotScale.GetY2(), this.timeAxis);
        }

        public void SetScaleStopTOD(long stoptime)
        {
            this.scaleStopTOD = stoptime;
        }

        public override void SetScaleStopX(double rX2)
        {
            this.InitTimeScale(base.physPlotScale.GetX1(), base.physPlotScale.GetY1(), rX2, base.physPlotScale.GetY2(), this.timeAxis);
        }

        public override void SetScaleStopY(double rY2)
        {
            this.InitTimeScale(base.physPlotScale.GetX1(), base.physPlotScale.GetY1(), base.physPlotScale.GetX2(), rY2, this.timeAxis);
        }

        public override void SetScaleX(double rX1, double rX2)
        {
            this.InitTimeScale(rX1, base.physPlotScale.GetY(), rX2, base.physPlotScale.GetY2(), this.timeAxis);
        }

        public override void SetScaleY(double rY1, double rY2)
        {
            this.InitTimeScale(base.physPlotScale.GetX(), rY1, base.physPlotScale.GetX2(), rY2, this.timeAxis);
        }

        public void SetTimeCoordinateBounds(ChartCalendar dstart, double y1, ChartCalendar dstop, double y2)
        {
            this.timeAxis = 0;
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, this.weekType);
        }

        public void SetTimeCoordinateBounds(ChartCalendar dstart, double y1, ChartCalendar dstop, double y2, int nweektype)
        {
            this.timeAxis = 0;
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, nweektype);
        }

        public void SetTimeCoordinateBounds(ChartCalendar dstart, long starttod, double y1, ChartCalendar dstop, long stoptod, double y2, int nweektype)
        {
            this.timeAxis = 0;
            this.scaleStartTOD = starttod;
            this.scaleStopTOD = stoptod;
            this.InitTimeScale(dstart, this.scaleStartTOD, y1, dstop, this.scaleStopTOD, y2, this.timeAxis, nweektype);
        }

        public virtual void SetTimeScaleStart(ChartCalendar start)
        {
            if (this.timeAxis == 0)
            {
                this.InitTimeScale((double) start.GetCalendarMsecs(), base.physPlotScale.GetY1(), base.physPlotScale.GetX2(), base.physPlotScale.GetY2(), this.timeAxis);
            }
            else
            {
                this.InitTimeScale(base.physPlotScale.GetX1(), (double) start.GetCalendarMsecs(), base.physPlotScale.GetX2(), base.physPlotScale.GetY2(), this.timeAxis);
            }
        }

        public virtual void SetTimeScaleStart(int axis, ChartCalendar start)
        {
            if (axis == 0)
            {
                this.InitTimeScale((double) start.GetCalendarMsecs(), base.physPlotScale.GetY1(), base.physPlotScale.GetX2(), base.physPlotScale.GetY2(), axis);
            }
            else
            {
                this.InitTimeScale(base.physPlotScale.GetX1(), (double) start.GetCalendarMsecs(), base.physPlotScale.GetX2(), base.physPlotScale.GetY2(), axis);
            }
        }

        public virtual void SetTimeScaleStop(ChartCalendar stop)
        {
            if (this.timeAxis == 0)
            {
                this.InitTimeScale(base.physPlotScale.GetX1(), base.physPlotScale.GetY1(), (double) stop.GetCalendarMsecs(), base.physPlotScale.GetY2(), this.timeAxis);
            }
            else
            {
                this.InitTimeScale(base.physPlotScale.GetX1(), base.physPlotScale.GetY1(), base.physPlotScale.GetX2(), (double) stop.GetCalendarMsecs(), this.timeAxis);
            }
        }

        public virtual void SetTimeScaleStop(int axis, ChartCalendar stop)
        {
            if (axis == 0)
            {
                this.InitTimeScale(base.physPlotScale.GetX1(), base.physPlotScale.GetY1(), (double) stop.GetCalendarMsecs(), base.physPlotScale.GetY2(), axis);
            }
            else
            {
                this.InitTimeScale(base.physPlotScale.GetX1(), base.physPlotScale.GetY1(), base.physPlotScale.GetX2(), (double) stop.GetCalendarMsecs(), axis);
            }
        }

        public void SetTimeScaleTransforms(int xscale, int yscale)
        {
            Scale scale;
            Scale scale2;
            switch (xscale)
            {
                case 1:
                    scale = new LogScale();
                    break;

                case 2:
                    scale = new TimeScale(0);
                    break;

                default:
                    scale = new LinearScale();
                    break;
            }
            switch (yscale)
            {
                case 1:
                    scale2 = new LogScale();
                    break;

                case 2:
                    scale2 = new TimeScale(1);
                    break;

                default:
                    scale2 = new LinearScale();
                    break;
            }
            base.SetPhysScales(scale, scale2);
        }

        public void SetWeekType(int weektype)
        {
            this.weekType = weektype;
        }

        public override void SwapScaleOrientation()
        {
            Scale xScale = base.xScale;
            base.xScale = base.yScale;
            base.yScale = xScale;
            if (this.timeAxis == 0)
            {
                this.timeAxis = 1;
            }
            else if (this.timeAxis == 1)
            {
                this.timeAxis = 0;
            }
            base.SetPhysScale(base.physPlotScale.GetY1(), base.physPlotScale.GetX1(), base.physPlotScale.GetY2(), base.physPlotScale.GetX2());
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

        public override double ScaleStartX
        {
            get
            {
                return base.physPlotScale.GetX1();
            }
            set
            {
                this.SetScaleX(value, base.physPlotScale.GetX2());
            }
        }

        public override double ScaleStartY
        {
            get
            {
                return base.physPlotScale.GetY1();
            }
            set
            {
                this.SetScaleY(value, base.physPlotScale.GetY2());
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

        public override double ScaleStopX
        {
            get
            {
                return base.physPlotScale.GetX2();
            }
            set
            {
                this.SetScaleX(base.physPlotScale.GetX1(), value);
            }
        }

        public override double ScaleStopY
        {
            get
            {
                return base.physPlotScale.GetY2();
            }
            set
            {
                this.SetScaleY(base.physPlotScale.GetY1(), value);
            }
        }

        public ChartCalendar TimeScaleStart
        {
            get
            {
                return this.GetTimeScaleStart();
            }
            set
            {
                this.SetTimeScaleStart(value);
            }
        }

        public ChartCalendar TimeScaleStop
        {
            get
            {
                return this.GetTimeScaleStop();
            }
            set
            {
                this.SetTimeScaleStop(value);
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

