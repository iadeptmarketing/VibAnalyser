namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class TimeAxisLabels : AxisLabels
    {
        internal bool autoFormatCrossoverLabels;
        internal int axisLabelsDecimalPos;
        internal int crossoverLabelFormat;
        internal string customTimeCrossoverFormatString;
        internal string customTimeFormatString;
        internal int dateCrossoverCondition;
        internal int dateCrossoverMode;
        internal bool euroAutoFormatMonthDay;
        internal int[] timeBaseLabelFormats;

        public TimeAxisLabels()
        {
            this.dateCrossoverMode = 0;
            this.dateCrossoverCondition = 0;
            this.crossoverLabelFormat = 0x15;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.axisLabelsDecimalPos = 0;
            this.euroAutoFormatMonthDay = false;
            this.autoFormatCrossoverLabels = true;
            this.InitDefaults();
        }

        public TimeAxisLabels(TimeAxis baseaxis)
        {
            this.dateCrossoverMode = 0;
            this.dateCrossoverCondition = 0;
            this.crossoverLabelFormat = 0x15;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.axisLabelsDecimalPos = 0;
            this.euroAutoFormatMonthDay = false;
            this.autoFormatCrossoverLabels = true;
            base.baseAxis = baseaxis;
            this.InitDefaults();
            if (base.baseAxis != null)
            {
                base.chartObjScale = base.baseAxis.GetChartObjScale();
                base.baseAxis.SetAxisLabels(this);
                base.axisLabelsFormat = this.CalcTimeLabelFormats(((TimeAxis) base.baseAxis).GetAxisTickMarkTimeBase());
                this.CalcAutoAxisLabels();
            }
        }

        public override void CalcAutoAxisLabels()
        {
            if ((base.baseAxis != null) && (base.baseAxis is TimeAxis))
            {
                int axisTickMarkTimeBase = ((TimeAxis) base.baseAxis).GetAxisTickMarkTimeBase();
                base.axisLabelsFormat = this.CalcTimeLabelFormats(axisTickMarkTimeBase);
                if (this.euroAutoFormatMonthDay && (base.axisLabelsFormat == 0x15))
                {
                    base.axisLabelsFormat = 0x16;
                }
                this.dateCrossoverMode = 0;
                this.dateCrossoverCondition = 0;
                if ((base.axisLabelsFormat >= 5) && (base.axisLabelsFormat <= 0x10))
                {
                    if (this.autoFormatCrossoverLabels)
                    {
                        this.dateCrossoverMode = 2;
                        this.dateCrossoverCondition = 1;
                        if (this.euroAutoFormatMonthDay)
                        {
                            this.crossoverLabelFormat = 0x16;
                        }
                        else
                        {
                            this.crossoverLabelFormat = 0x15;
                        }
                    }
                }
                else if (((base.axisLabelsFormat == 0x1a) || (base.axisLabelsFormat == 30)) || (base.axisLabelsFormat == 0x1c))
                {
                    if (this.autoFormatCrossoverLabels)
                    {
                        this.dateCrossoverMode = 2;
                        this.dateCrossoverCondition = 2;
                        if (this.euroAutoFormatMonthDay)
                        {
                            this.crossoverLabelFormat = 0x16;
                        }
                        else
                        {
                            this.crossoverLabelFormat = 0x15;
                        }
                    }
                }
                else if ((((base.axisLabelsFormat == 0x19) || (base.axisLabelsFormat == 0x1d)) || ((base.axisLabelsFormat == 0x1b) || (base.axisLabelsFormat == 0x18))) && this.autoFormatCrossoverLabels)
                {
                    this.dateCrossoverMode = 2;
                    this.dateCrossoverCondition = 4;
                    this.crossoverLabelFormat = 0x23;
                }
                base.axisLabelsDir = base.baseAxis.GetAxisTickDir();
            }
        }

        private void CalcAxisLabels(Graphics g2)
        {
            int num = 0;
            if (base.baseAxis != null)
            {
                TimeLabel textobj = new TimeLabel();
                textobj.Copy(this);
                int axisMajorNthTick = base.baseAxis.GetAxisMajorNthTick();
                ArrayList axisTicksArrayList = base.baseAxis.GetAxisTicksArrayList();
                int count = axisTicksArrayList.Count;
                base.boundingBox.Reset();
                textobj.SetChartObjClipping(1);
                base.lastLabelBoundingBox.SetFrame(0.0, 0.0, 0.0, 0.0);
                int overlapLabelMode = base.GetOverlapLabelMode();
                for (int i = 0; i < count; i++)
                {
                    TickMark ticmark = (TickMark) axisTicksArrayList[i];
                    if (ticmark.GetTickLabelFlag())
                    {
                        if ((num % axisMajorNthTick) == 0)
                        {
                            this.FormatAxisLabel(textobj, ticmark);
                            this.OutAxisLabel(g2, textobj, ticmark);
                            if (this.CheckCrossover(ticmark))
                            {
                                if (this.dateCrossoverMode == 2)
                                {
                                    base.SetOverlapLabelMode(0);
                                }
                            }
                            else
                            {
                                base.SetOverlapLabelMode(overlapLabelMode);
                            }
                            base.boundingBox.AddRectangle(textobj.GetBoundingBox().GetRectangleF());
                        }
                        num++;
                    }
                }
                textobj = null;
            }
        }

        private int CalcLabelFormat(TickMark tickmark)
        {
            tickmark.GetTickLocation();
            int tickLabelFormat = tickmark.GetTickLabelFormat();
            if (tickLabelFormat == 0)
            {
                tickLabelFormat = base.axisLabelsFormat;
            }
            return tickLabelFormat;
        }

        private int CalcTimeLabelFormats(int tickmarkbase)
        {
            return this.timeBaseLabelFormats[tickmarkbase];
        }

        private bool CheckCrossover(TickMark tickmark)
        {
            bool flag = false;
            if (base.baseAxis == null)
            {
                return false;
            }
            ((TimeAxis) base.baseAxis).GetAxisTickMarkTimeBase();
            ChartCalendar tickDate = tickmark.GetTickDate();
            TimeCoordinates timeCoordinates = ((TimeAxis) base.baseAxis).GetTimeCoordinates();
            int weekType = timeCoordinates.GetTimeScale(((TimeAxis) base.baseAxis).GetAxisType()).GetWeekType();
            if (this.dateCrossoverMode != 0)
            {
                switch (this.dateCrossoverCondition)
                {
                    case 1:
                        if (ChartCalendar.GetTODMsecs(tickDate) == timeCoordinates.GetTimeScale(base.baseAxis.GetAxisType()).GetScaleStartTOD())
                        {
                            flag = true;
                        }
                        return flag;

                    case 2:
                        if (ChartCalendar.CalendarCheckMin(tickDate, 7, weekType, 0x3e8))
                        {
                            flag = true;
                        }
                        return flag;

                    case 3:
                        if (ChartCalendar.CalendarCheckMin(tickDate, 5, weekType, 0x3e8))
                        {
                            flag = true;
                        }
                        return flag;

                    case 4:
                        if (ChartCalendar.CalendarCheckMin(tickDate, 6, weekType, 0x3e8))
                        {
                            flag = true;
                        }
                        return flag;
                }
            }
            return flag;
        }

        public override object Clone()
        {
            TimeAxisLabels labels = new TimeAxisLabels();
            labels.Copy(this);
            return labels;
        }

        public void Copy(TimeAxisLabels source)
        {
            if (source != null)
            {
                this.InitDefaults();
                base.Copy(source);
                this.dateCrossoverMode = source.dateCrossoverMode;
                this.crossoverLabelFormat = source.crossoverLabelFormat;
                this.dateCrossoverCondition = source.dateCrossoverCondition;
                this.axisLabelsDecimalPos = source.axisLabelsDecimalPos;
                this.customTimeFormatString = source.customTimeFormatString;
                this.customTimeCrossoverFormatString = source.customTimeCrossoverFormatString;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.CalcAxisLabels(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (base.baseAxis == null)
                {
                    nerror = 120;
                }
                if ((nerror == 0) && !(base.baseAxis is TimeAxis))
                {
                    nerror = 0x79;
                }
            }
            return base.ErrorCheck(nerror);
        }

        private void FormatAxisLabel(TimeLabel textobj, TickMark ticmark)
        {
            double tickLocation = ticmark.GetTickLocation();
            int axisLabelsFormat = this.GetAxisLabelsFormat(ticmark);
            if (this.customTimeFormatString.Length > 0)
            {
                textobj.SetCustomTimeFormatString(this.customTimeFormatString);
            }
            if (this.CheckCrossover(ticmark))
            {
                if (this.dateCrossoverMode == 1)
                {
                    if (this.customTimeCrossoverFormatString.Length > 0)
                    {
                        textobj.SetCustomTimeFormatString(this.customTimeCrossoverFormatString);
                    }
                    else
                    {
                        axisLabelsFormat = this.crossoverLabelFormat;
                    }
                }
                else if (this.dateCrossoverMode == 2)
                {
                    if (this.customTimeCrossoverFormatString.Length > 0)
                    {
                        textobj.SetCustomTimeCrossoverFormatString(this.customTimeCrossoverFormatString);
                    }
                    else
                    {
                        textobj.SetCrossoverTimeFormat(this.crossoverLabelFormat);
                    }
                }
            }
            else
            {
                textobj.SetCrossoverTimeFormat(0);
                textobj.SetCustomTimeCrossoverFormatString("");
            }
            textobj.SetTimeFormat(axisLabelsFormat);
            textobj.SetTimeNumericValue(tickLocation);
        }

        public int GetAxisLabelsDecimalPos(double r)
        {
            return this.axisLabelsDecimalPos;
        }

        public override int GetAxisLabelsFormat(TickMark tickmark)
        {
            return this.CalcLabelFormat(tickmark);
        }

        public override ChartLabel GetCompatibleLabel()
        {
            return new TimeLabel();
        }

        public int GetCrossoverLabelFormat()
        {
            return this.crossoverLabelFormat;
        }

        public string GetCustomTimeCrossoverFormatString()
        {
            return this.customTimeCrossoverFormatString;
        }

        public string GetCustomTimeFormatString()
        {
            return this.customTimeFormatString;
        }

        public int GetDateCrossoverCondition()
        {
            return this.dateCrossoverCondition;
        }

        public int GetDateCrossoverMode()
        {
            return this.dateCrossoverMode;
        }

        public int GetTimeBaseLabelFormats(int index)
        {
            int num = 0;
            if ((index >= 0) && (index <= 0x73))
            {
                num = this.timeBaseLabelFormats[index];
            }
            return num;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x193;
            this.InitTimeBaseLabelFormats();
            base.axisLabelsFormat = 0x17;
        }

        private void InitTimeBaseLabelFormats()
        {
            int num = 0x15;
            this.timeBaseLabelFormats = new int[0x74];
            for (int i = 0; i < 0x73; i++)
            {
                switch (i)
                {
                    case 6:
                        num = 0x10;
                        break;

                    case 7:
                        num = 15;
                        break;

                    case 8:
                        num = 14;
                        break;

                    case 9:
                        num = 15;
                        break;

                    case 10:
                        num = 5;
                        break;

                    case 11:
                        num = 5;
                        break;

                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                    case 0x11:
                    case 0x12:
                    case 0x13:
                    case 20:
                    case 0x15:
                    case 0x16:
                        num = 5;
                        break;

                    case 0x19:
                    case 0x1a:
                    case 0x1b:
                    case 0x1c:
                    case 0x1d:
                    case 30:
                        num = 6;
                        break;

                    case 0x23:
                    case 0x24:
                    case 0x25:
                    case 0x26:
                    case 0x27:
                    case 40:
                        num = 6;
                        break;

                    case 0x2f:
                    case 0x30:
                    case 0x31:
                    case 50:
                    case 0x33:
                        num = 6;
                        break;

                    case 0x37:
                    case 0x38:
                    case 0x39:
                    case 0x3a:
                    case 0x3b:
                        num = 0x15;
                        break;

                    case 60:
                        num = 0x15;
                        break;

                    case 0x45:
                        num = 0x15;
                        break;

                    case 70:
                        num = 0x15;
                        break;

                    case 0x58:
                        num = 0x15;
                        break;

                    case 0x59:
                        num = 0x15;
                        break;

                    case 90:
                        num = 0x17;
                        break;

                    case 0x63:
                        num = 0x22;
                        break;

                    case 100:
                        num = 0x22;
                        break;

                    case 0x6d:
                    case 110:
                        num = 0x23;
                        break;

                    case 0x6f:
                    case 0x70:
                    case 0x71:
                    case 0x72:
                    case 0x73:
                        num = 0x23;
                        break;

                    default:
                        num = 0x15;
                        break;
                }
                this.timeBaseLabelFormats[i] = num;
            }
        }

        public override void SetAxisLabelsDecimalPos(int decimalpos)
        {
            this.axisLabelsDecimalPos = decimalpos;
        }

        public void SetCrossoverLabelFormat(int labelformat)
        {
            this.crossoverLabelFormat = labelformat;
        }

        public void SetCustomTimeCrossoverFormatString(string timeformatstring)
        {
            this.customTimeCrossoverFormatString = timeformatstring;
        }

        public void SetCustomTimeFormatString(string timeformatstring)
        {
            this.customTimeFormatString = timeformatstring;
        }

        public void SetDateCrossoverCondition(int crossover)
        {
            this.dateCrossoverCondition = crossover;
        }

        public void SetDateCrossoverMode(int crossover)
        {
            this.dateCrossoverMode = crossover;
        }

        public void SetTimeBaseLabelFormats(int index, int format)
        {
            if ((index >= 0) && (index <= 0x73))
            {
                this.timeBaseLabelFormats[index] = format;
            }
        }

        public bool AutoFormatCrossoverLabels
        {
            get
            {
                return this.autoFormatCrossoverLabels;
            }
            set
            {
                this.autoFormatCrossoverLabels = value;
            }
        }

        public int CrossoverLabelFormat
        {
            get
            {
                return this.crossoverLabelFormat;
            }
            set
            {
                this.crossoverLabelFormat = value;
            }
        }

        public string CustomTimeCrossoverFormatString
        {
            get
            {
                return this.customTimeCrossoverFormatString;
            }
            set
            {
                this.customTimeCrossoverFormatString = value;
            }
        }

        public string CustomTimeFormatString
        {
            get
            {
                return this.customTimeFormatString;
            }
            set
            {
                this.customTimeFormatString = value;
            }
        }

        public int DateCrossoverCondition
        {
            get
            {
                return this.dateCrossoverCondition;
            }
            set
            {
                this.dateCrossoverCondition = value;
            }
        }

        public int DateCrossoverMode
        {
            get
            {
                return this.dateCrossoverMode;
            }
            set
            {
                this.dateCrossoverMode = value;
            }
        }

        public bool EuroAutoFormatMonthDay
        {
            get
            {
                return this.euroAutoFormatMonthDay;
            }
            set
            {
                this.euroAutoFormatMonthDay = value;
            }
        }
    }
}

