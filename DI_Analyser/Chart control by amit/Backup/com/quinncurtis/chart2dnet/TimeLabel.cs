namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class TimeLabel : ChartLabel
    {
        internal int crossoverTimeFormat;
        internal string customTimeCrossoverFormatString;
        internal string customTimeFormatString;
        internal int timeFormat;
        internal string[] timeFormatStrings;
        internal double timeNumericValue;

        public TimeLabel()
        {
            this.timeFormat = 0x15;
            this.crossoverTimeFormat = 0;
            this.timeNumericValue = 0.0;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.InitDefaults();
        }

        public TimeLabel(PhysicalCoordinates transform)
        {
            this.timeFormat = 0x15;
            this.crossoverTimeFormat = 0;
            this.timeNumericValue = 0.0;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.SetChartObjScale(transform);
            this.InitDefaults();
        }

        public TimeLabel(int timeformat)
        {
            this.timeFormat = 0x15;
            this.crossoverTimeFormat = 0;
            this.timeNumericValue = 0.0;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.InitDefaults();
            this.timeFormat = timeformat;
            this.SetTimeValue(new ChartCalendar());
            this.MakeTimeLabel();
        }

        public TimeLabel(PhysicalCoordinates transform, ChartCalendar date, int timeformat)
        {
            this.timeFormat = 0x15;
            this.crossoverTimeFormat = 0;
            this.timeNumericValue = 0.0;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.SetChartObjScale(transform);
            this.InitDefaults();
            this.timeFormat = timeformat;
            this.SetTimeValue(date);
            this.MakeTimeLabel();
        }

        public TimeLabel(PhysicalCoordinates transform, Font tfont, ChartCalendar date, double x, double y, int npostype, int timeformat, int xjust, int yjust, double rotation)
        {
            this.timeFormat = 0x15;
            this.crossoverTimeFormat = 0;
            this.timeNumericValue = 0.0;
            this.customTimeFormatString = string.Copy("");
            this.customTimeCrossoverFormatString = string.Copy("");
            this.InitDefaults();
            base.InitChartText(transform, tfont, "", x, y, npostype, xjust, yjust, rotation);
            this.timeFormat = timeformat;
            this.SetTimeValue(date);
            this.MakeTimeLabel();
        }

        public override object Clone()
        {
            TimeLabel label = new TimeLabel();
            label.Copy(this);
            return label;
        }

        public void Copy(TimeLabel source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.timeNumericValue = source.timeNumericValue;
                this.timeFormat = source.timeFormat;
                this.customTimeFormatString = source.customTimeFormatString;
                this.customTimeCrossoverFormatString = source.customTimeCrossoverFormatString;
            }
        }

        private string DateToASCII(ChartCalendar tdate, int ndateformat, string suserdefinedformatstring1)
        {
            string format = "MMMMM dd, yyyy";
            string str2 = "";
            if (tdate == null)
            {
                return string.Copy("");
            }
            if (ndateformat == 0x18)
            {
                int num2 = ((tdate.Get(2) - 1) / 3) + 1;
                return ("Q" + num2.ToString());
            }
            if (suserdefinedformatstring1.Length > 0)
            {
                format = suserdefinedformatstring1;
            }
            else
            {
                format = this.timeFormatStrings[ndateformat];
            }
            string str = tdate.GetTime().ToString(format);
            str2 = string.Copy(str);
            if (ndateformat == 0x1c)
            {
                str2 = str.Substring(0, 3);
            }
            if (ndateformat == 30)
            {
                str2 = str.Substring(0, 1);
            }
            if (ndateformat == 0x1b)
            {
                str2 = str.Substring(0, 3);
            }
            if (ndateformat == 0x1d)
            {
                str2 = str.Substring(0, 1);
            }
            return str2;
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.MakeTimeLabel();
                base.Draw(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.timeFormatStrings == null))
            {
                nerror = 220;
            }
            return base.ErrorCheck(nerror);
        }

        public int GetCrossoverTimeFormat()
        {
            return this.crossoverTimeFormat;
        }

        public string GetCustomTimeCrossoverFormatStrings()
        {
            return this.customTimeCrossoverFormatString;
        }

        public string GetCustomTimeFormatStrings()
        {
            return this.customTimeFormatString;
        }

        public override string GetTextString()
        {
            this.MakeTimeLabel();
            return base.GetTextString();
        }

        public int GetTimeFormat()
        {
            return this.timeFormat;
        }

        public string GetTimeFormatStrings(int index)
        {
            string str = "";
            if ((index >= 0) && (index <= 0x23))
            {
                str = this.timeFormatStrings[index];
            }
            return str;
        }

        public double GetTimeNumericValue()
        {
            return this.timeNumericValue;
        }

        private string GetTimeString(ChartCalendar tdate)
        {
            string str = string.Copy("");
            str = this.DateToASCII(tdate, this.timeFormat, this.customTimeFormatString);
            if ((this.customTimeCrossoverFormatString.Length <= 0) && (this.crossoverTimeFormat == 0))
            {
                return str;
            }
            return (str + "\n" + this.DateToASCII(tdate, this.crossoverTimeFormat, this.customTimeCrossoverFormatString));
        }

        public ChartCalendar GetTimeValue()
        {
            return new ChartCalendar((long) this.timeNumericValue, true);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x25f;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
            this.InitTimeFormatArray();
        }

        private void InitTimeFormatArray()
        {
            string str = "d/MM/yy";
            this.timeFormatStrings = new string[0x24];
            for (int i = 0; i <= 0x23; i++)
            {
                switch (i)
                {
                    case 5:
                        str = "H:mm:ss";
                        break;

                    case 6:
                        str = "H:mm";
                        break;

                    case 7:
                        str = "m:ss";
                        break;

                    case 8:
                        str = "h:mm:ss";
                        break;

                    case 9:
                        str = "h:mm";
                        break;

                    case 10:
                        str = "H:mm:ss.f";
                        break;

                    case 11:
                        str = "H:mm:ss.ff";
                        break;

                    case 12:
                        str = "h:mm.ss.f";
                        break;

                    case 13:
                        str = "h:mm.ss.ff";
                        break;

                    case 14:
                        str = "mm:ss.f";
                        break;

                    case 15:
                        str = "mm:ss.ff";
                        break;

                    case 0x10:
                        str = "mm:ss.fff";
                        break;

                    case 20:
                        str = "MMMMM dd, yyyy";
                        break;

                    case 0x15:
                        str = "M/dd/yy";
                        break;

                    case 0x16:
                        str = "d/MM/yy";
                        break;

                    case 0x17:
                        str = "M/yy";
                        break;

                    case 0x19:
                        str = "MMMM";
                        break;

                    case 0x1a:
                        str = "dddd";
                        break;

                    case 0x1b:
                        str = "MMM";
                        break;

                    case 0x1c:
                        str = "ddd";
                        break;

                    case 0x1d:
                        str = "MMM";
                        break;

                    case 30:
                        str = "ddd";
                        break;

                    case 0x1f:
                        str = "yy";
                        break;

                    case 0x20:
                        str = "M/dd/yyyy";
                        break;

                    case 0x21:
                        str = "d/MM/yyyy";
                        break;

                    case 0x22:
                        str = "M/yyyy";
                        break;

                    case 0x23:
                        str = "yyyy";
                        break;

                    default:
                        str = "d/MM/yy";
                        break;
                }
                this.timeFormatStrings[i] = string.Copy(str);
            }
        }

        public override void MakeLabel()
        {
            this.MakeTimeLabel();
        }

        private void MakeTimeLabel()
        {
            string thestring = "";
            if ((this.timeFormat >= 5) && (this.timeFormat <= 0x23))
            {
                ChartCalendar tdate = new ChartCalendar((long) this.timeNumericValue, true);
                thestring = this.GetTimeString(tdate);
            }
            base.SetTextString(thestring);
        }

        public void SetCrossoverTimeFormat(int nformat)
        {
            this.crossoverTimeFormat = nformat;
        }

        public void SetCustomTimeCrossoverFormatString(string formatstring1)
        {
            this.customTimeCrossoverFormatString = formatstring1;
        }

        public void SetCustomTimeFormatString(string formatstring1)
        {
            this.customTimeFormatString = formatstring1;
        }

        public void SetTimeFormat(int nformat)
        {
            this.timeFormat = nformat;
        }

        public void SetTimeFormatStrings(int index, string formatstring1)
        {
            if ((index >= 0) && (index <= 0x23))
            {
                this.timeFormatStrings[index] = formatstring1;
            }
        }

        public void SetTimeNumericValue(double rvalue)
        {
            this.timeNumericValue = rvalue;
        }

        public void SetTimeValue(ChartCalendar tdate)
        {
            this.timeNumericValue = tdate.GetCalendarMsecs();
        }

        public int CrossoverTimeFormat
        {
            get
            {
                return this.crossoverTimeFormat;
            }
            set
            {
                this.crossoverTimeFormat = value;
            }
        }

        public int TimeFormat
        {
            get
            {
                return this.timeFormat;
            }
            set
            {
                this.timeFormat = value;
            }
        }

        public double TimeNumericValue
        {
            get
            {
                return this.timeNumericValue;
            }
            set
            {
                this.timeNumericValue = value;
            }
        }

        public ChartCalendar TimeValue
        {
            get
            {
                return this.GetTimeValue();
            }
            set
            {
                this.SetTimeValue(value);
            }
        }
    }
}

