namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Globalization;

    public class ChartCalendar : ChartObj
    {
        internal static bool altTimeCalc = true;
        internal DateTime dateTimeValue;
        internal static GregorianCalendar globalCalendar = new GregorianCalendar();
        public const int milliSecondsPerDay = 0x5265c00;
        internal static DateTime startDateTime = DateTime.Now;
        internal static int startTickCount = Environment.TickCount;
        public const int ticksPerMilliSecond = 0x2710;
        public const int ticksPerSecond = 0x989680;

        public ChartCalendar()
        {
            this.dateTimeValue = this.LocalNowFunction();
            this.InitDefaults();
        }

        public ChartCalendar(DateTime time)
        {
            this.InitDefaults();
            this.dateTimeValue = new DateTime(time.Ticks);
        }

        public ChartCalendar(long time)
        {
            this.InitDefaults();
            this.dateTimeValue = new DateTime(time);
        }

        public ChartCalendar(long time, bool usemsecs)
        {
            long ticks = 0L;
            this.InitDefaults();
            if (usemsecs)
            {
                ticks = time * 0x2710L;
            }
            else
            {
                ticks = time * 0x989680L;
            }
            this.dateTimeValue = new DateTime(ticks);
        }

        public ChartCalendar(int year, int month, int day)
        {
            this.InitDefaults();
            this.dateTimeValue = new DateTime(year, month, day);
        }

        public ChartCalendar(int year, int month, int day, int hour, int minute, int second)
        {
            this.InitDefaults();
            this.dateTimeValue = new DateTime(year, month, day, hour, minute, second);
        }

        public ChartCalendar(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            this.InitDefaults();
            this.dateTimeValue = new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        public void Add(int item, int value)
        {
            switch (item)
            {
                case 1:
                    this.dateTimeValue = globalCalendar.AddYears(this.dateTimeValue, value);
                    return;

                case 2:
                    this.dateTimeValue = globalCalendar.AddMonths(this.dateTimeValue, value);
                    return;

                case 3:
                    this.dateTimeValue = globalCalendar.AddDays(this.dateTimeValue, value * 7);
                    return;

                case 4:
                    this.dateTimeValue = globalCalendar.AddDays(this.dateTimeValue, value * 7);
                    return;

                case 5:
                    this.dateTimeValue = globalCalendar.AddDays(this.dateTimeValue, value);
                    return;

                case 6:
                    this.dateTimeValue = globalCalendar.AddDays(this.dateTimeValue, value);
                    return;

                case 7:
                    this.dateTimeValue = globalCalendar.AddDays(this.dateTimeValue, value);
                    return;

                case 8:
                case 9:
                    return;

                case 10:
                    this.dateTimeValue = globalCalendar.AddHours(this.dateTimeValue, value);
                    return;

                case 11:
                    this.dateTimeValue = globalCalendar.AddHours(this.dateTimeValue, value);
                    return;

                case 12:
                    this.dateTimeValue = globalCalendar.AddMinutes(this.dateTimeValue, value);
                    return;

                case 13:
                    this.dateTimeValue = globalCalendar.AddSeconds(this.dateTimeValue, value);
                    return;

                case 14:
                    this.dateTimeValue = globalCalendar.AddMilliseconds(this.dateTimeValue, (double) value);
                    return;

                case 15:
                    this.dateTimeValue = this.dateTimeValue.AddTicks((long) value);
                    return;

                case 0x10:
                    this.dateTimeValue = globalCalendar.AddMilliseconds(this.dateTimeValue, (double) value);
                    return;
            }
        }

        public void CalendarCeil(int nresolution)
        {
            CalendarCeil(this, nresolution);
        }

        public static void CalendarCeil(ChartCalendar arg1, int nresolution)
        {
            switch (nresolution)
            {
                case 1:
                    arg1.Add(6, 0x16d);
                    CalendarTruncate(arg1, 1);
                    return;

                case 2:
                    arg1.Add(6, 0x1c);
                    CalendarTruncate(arg1, 2);
                    return;

                case 3:
                    arg1.Add(6, 6);
                    CalendarTruncate(arg1, 4);
                    return;

                case 4:
                    arg1.Add(6, 6);
                    CalendarTruncate(arg1, 4);
                    return;

                case 5:
                    arg1.Add(10, 0x17);
                    CalendarTruncate(arg1, 6);
                    return;

                case 6:
                    arg1.Add(10, 0x17);
                    CalendarTruncate(arg1, 6);
                    return;

                case 7:
                    arg1.Add(10, 0x17);
                    CalendarTruncate(arg1, 6);
                    return;

                case 8:
                case 9:
                    return;

                case 10:
                    arg1.Add(12, 0x3b);
                    CalendarTruncate(arg1, 10);
                    return;

                case 11:
                    arg1.Add(12, 0x3b);
                    CalendarTruncate(arg1, 10);
                    return;

                case 12:
                    arg1.Add(13, 0x3b);
                    CalendarTruncate(arg1, 12);
                    return;

                case 13:
                    arg1.Add(14, 0x3e7);
                    CalendarTruncate(arg1, 13);
                    return;

                case 14:
                    arg1.Add(15, 0x270f);
                    CalendarTruncate(arg1, 14);
                    return;
            }
        }

        public bool CalendarCheckMin(int ntimebase, int nweektype, int modulus)
        {
            return CalendarCheckMin(this, ntimebase, nweektype, modulus);
        }

        public static bool CalendarCheckMin(ChartCalendar cdate, int ntimebase, int nweektype, int modulus)
        {
            ChartCalendar calendar;
            bool flag = false;
            int item = ntimebase;
            if (cdate != null)
            {
                calendar = (ChartCalendar) cdate.Clone();
            }
            else
            {
                calendar = new ChartCalendar();
            }
            long minimum = calendar.GetMinimum(item);
            if (nweektype == 0)
            {
                if ((calendar.Get(item) % modulus) == minimum)
                {
                    flag = true;
                }
                return flag;
            }
            switch (calendar.Get(7))
            {
                case 6:
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    calendar.Add(6, 1);
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    calendar.Add(6, 1);
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    return flag;

                case 0:
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    calendar.Add(6, 1);
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    return flag;

                case 1:
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    calendar.Add(6, -1);
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    calendar.Add(6, -1);
                    if ((calendar.Get(item) % modulus) == minimum)
                    {
                        flag = true;
                    }
                    return flag;
            }
            if ((calendar.Get(item) % modulus) == minimum)
            {
                flag = true;
            }
            return flag;
        }

        public static bool CalendarCompare(ChartCalendar arg1, ChartCalendar arg2)
        {
            long calendarMsecs = arg1.GetCalendarMsecs();
            long num2 = arg2.GetCalendarMsecs();
            bool flag = calendarMsecs < num2;
            bool flag2 = false;
            if (flag)
            {
                flag2 = true;
            }
            return flag2;
        }

        public static bool CalendarCompare2(ChartCalendar arg1, ChartCalendar arg2)
        {
            long calendarMsecs = arg1.GetCalendarMsecs();
            long num2 = arg2.GetCalendarMsecs();
            bool flag = calendarMsecs < num2;
            bool flag2 = calendarMsecs == num2;
            bool flag3 = false;
            if (!flag && !flag2)
            {
                return flag3;
            }
            return true;
        }

        public static void CalendarCopy(ChartCalendar dest, ChartCalendar source)
        {
            dest.SetTime(source.GetTime());
        }

        public void CalendarDaysAdd(long numdays, int nweektype)
        {
            ChartCalendar result = this;
            ChartCalendar calendar1 = (ChartCalendar) this.Clone();
            if (numdays != 0L)
            {
                if (nweektype == 1)
                {
                    int num = 1;
                    bool forward = true;
                    if (numdays < 0L)
                    {
                        num = -1;
                        forward = false;
                    }
                    CalendarWeekAdjust(result, nweektype, forward);
                    long num2 = 0L;
                    bool flag2 = false;
                    do
                    {
                        if ((result.Get(7) != 6) && (result.Get(7) != 0))
                        {
                            num2 += num;
                        }
                        result.Add(7, num);
                        if (forward)
                        {
                            flag2 = result.Get(7) == 1;
                        }
                        else
                        {
                            flag2 = result.Get(7) == 5;
                        }
                    }
                    while (!(flag2 || (num2 == numdays)));
                    CalendarWeekAdjust(result, nweektype, forward);
                    if (!forward)
                    {
                        if (num2 > numdays)
                        {
                            numdays -= num2;
                            numdays += 2L * (numdays / 5L);
                            result.Add(6, (int) numdays);
                        }
                    }
                    else if (num2 < numdays)
                    {
                        numdays -= num2;
                        numdays += 2L * (numdays / 5L);
                        result.Add(6, (int) numdays);
                    }
                }
                else
                {
                    result.Add(6, (int) numdays);
                }
            }
        }

        public static ChartCalendar CalendarDaysAdd(ChartCalendar dstart, long numdays, int nweektype)
        {
            ChartCalendar result = (ChartCalendar) dstart.Clone();
            if (numdays != 0L)
            {
                if (nweektype == 1)
                {
                    int num = 1;
                    bool forward = true;
                    if (numdays < 0L)
                    {
                        num = -1;
                        forward = false;
                    }
                    CalendarWeekAdjust(result, nweektype, forward);
                    long num2 = 0L;
                    bool flag2 = false;
                    do
                    {
                        if ((result.Get(7) != 6) && (result.Get(7) != 0))
                        {
                            num2 += num;
                        }
                        result.Add(7, num);
                        if (forward)
                        {
                            flag2 = result.Get(7) == 1;
                        }
                        else
                        {
                            flag2 = result.Get(7) == 5;
                        }
                    }
                    while (!(flag2 || (num2 == numdays)));
                    CalendarWeekAdjust(result, nweektype, forward);
                    if (forward)
                    {
                        if (num2 < numdays)
                        {
                            numdays -= num2;
                            numdays += 2L * (numdays / 5L);
                            result.Add(6, (int) numdays);
                        }
                        return result;
                    }
                    if (num2 > numdays)
                    {
                        numdays -= num2;
                        numdays += 2L * (numdays / 5L);
                        result.Add(6, (int) numdays);
                    }
                    return result;
                }
                result.Add(6, (int) numdays);
            }
            return result;
        }

        private static ChartCalendar CalendarDaysAddWalk(ChartCalendar dstart, long numdays, int nweektype)
        {
            int num;
            ChartCalendar calendar = (ChartCalendar) dstart.Clone();
            ChartCalendar calendar2 = (ChartCalendar) dstart.Clone();
            CalendarTruncate(calendar2, 6);
            if (numdays >= 0L)
            {
                for (num = 0; num < numdays; num++)
                {
                    calendar.Add(5, 1);
                    if (nweektype == 1)
                    {
                        if (calendar.Get(7) == 6)
                        {
                            calendar.Add(5, 2);
                        }
                        else if (calendar.Get(7) == 0)
                        {
                            calendar.Add(5, 1);
                        }
                    }
                }
                return calendar;
            }
            for (num = 0; num > numdays; num--)
            {
                calendar.Add(5, -1);
                if (nweektype == 1)
                {
                    if (calendar.Get(7) == 6)
                    {
                        calendar.Add(5, -1);
                    }
                    else if (calendar.Get(7) == 0)
                    {
                        calendar.Add(5, -2);
                    }
                }
            }
            return calendar;
        }

        public static long CalendarDaysDiff(ChartCalendar dstart, ChartCalendar dstop, int nweektype)
        {
            long num = 0L;
            long num2 = 1L;
            long num3 = 0L;
            long num4 = 0L;
            long num5 = 0L;
            ChartCalendar calendar = (ChartCalendar) dstart.Clone();
            ChartCalendar calendar2 = (ChartCalendar) dstop.Clone();
            CalendarTruncate(calendar, 6);
            CalendarTruncate(calendar2, 6);
            calendar2.Add(13, 1);
            long calendarMsecs = calendar.GetCalendarMsecs();
            long num7 = calendar2.GetCalendarMsecs();
            if (num7 > calendarMsecs)
            {
                num2 = num7 - calendarMsecs;
                num3 = num2 / 0x5265c00L;
                num = num3;
                if (nweektype == 1)
                {
                    int num8 = calendar.Get(7);
                    calendar.Add(7, -num8 + 2);
                    int num9 = calendar2.Get(7);
                    calendar2.Add(7, -num9 + 2);
                    num2 = calendar2.GetCalendarMsecs() - calendar.GetCalendarMsecs();
                    num5 = num2 / 0x5265c00L;
                    long num10 = num5 / 7L;
                    num4 = 2L * num10;
                    num3 = num - num4;
                    if (num9 == 0)
                    {
                        num3 += 1L;
                    }
                }
            }
            return num3;
        }

        public static ChartCalendar CalendarMax(ChartCalendar d1, ChartCalendar d2)
        {
            if (d1.GetCalendarMsecs() > d2.GetCalendarMsecs())
            {
                return d1;
            }
            return d2;
        }

        public static ChartCalendar CalendarMin(ChartCalendar d1, ChartCalendar d2)
        {
            if (d1.GetCalendarMsecs() < d2.GetCalendarMsecs())
            {
                return d1;
            }
            return d2;
        }

        public static void CalendarSwap(ChartCalendar d1, ChartCalendar d2)
        {
            ChartCalendar dest = new ChartCalendar();
            CalendarCopy(dest, d1);
            CalendarCopy(d1, d2);
            CalendarCopy(d2, dest);
            dest = null;
        }

        public void CalendarTruncate(int nresolution)
        {
            CalendarTruncate(this, nresolution);
        }

        public static void CalendarTruncate(ChartCalendar arg1, int nresolution)
        {
            if (nresolution == 6)
            {
                DateTime date = arg1.dateTimeValue.Date;
                arg1.dateTimeValue = date;
            }
            else
            {
                ChartCalendar calendar;
                int num8;
                int year = arg1.Get(1);
                int month = arg1.Get(2);
                int day = arg1.Get(5);
                int hour = arg1.Get(11);
                int minute = arg1.Get(12);
                int second = arg1.Get(13);
                int millisecond = arg1.Get(14);
                switch (nresolution)
                {
                    case 1:
                        calendar = new ChartCalendar(year, arg1.GetMinimum(2), arg1.GetMinimum(5), 0, 0, 0, 0);
                        break;

                    case 2:
                        calendar = new ChartCalendar(year, month, arg1.GetMinimum(5), 0, 0, 0, 0);
                        break;

                    case 3:
                        num8 = Math.Max(1, (day - arg1.Get(7)) - arg1.GetMinimum(7));
                        calendar = new ChartCalendar(year, month, num8, 0, 0, 0, 0);
                        break;

                    case 4:
                        num8 = Math.Max(1, (day - arg1.Get(7)) - arg1.GetMinimum(7));
                        calendar = new ChartCalendar(year, month, num8, 0, 0, 0, 0);
                        break;

                    case 5:
                        calendar = new ChartCalendar(year, month, day, 0, 0, 0, 0);
                        break;

                    case 6:
                        calendar = new ChartCalendar(year, month, day, 0, 0, 0, 0);
                        break;

                    case 7:
                        calendar = new ChartCalendar(year, month, day, 0, 0, 0, 0);
                        break;

                    case 10:
                        calendar = new ChartCalendar(year, month, day, hour, 0, 0, 0);
                        break;

                    case 11:
                        calendar = new ChartCalendar(year, month, day, hour, 0, 0, 0);
                        break;

                    case 12:
                        calendar = new ChartCalendar(year, month, day, hour, minute, 0, 0);
                        break;

                    case 13:
                        calendar = new ChartCalendar(year, month, day, hour, minute, second, 0);
                        break;

                    case 14:
                        calendar = new ChartCalendar(year, month, day, hour, minute, second, millisecond);
                        break;

                    default:
                        calendar = new ChartCalendar(year, month, day, 0, 0, 0, 0);
                        break;
                }
                CalendarCopy(arg1, calendar);
            }
        }

        public void CalendarWeekAdjust(int nweektype, bool forward)
        {
            CalendarWeekAdjust(this, nweektype, forward);
        }

        public static void CalendarWeekAdjust(ChartCalendar result, int nweektype, bool forward)
        {
            if (nweektype == 1)
            {
                if (forward)
                {
                    if (result.Get(7) == 6)
                    {
                        result.Add(6, 2);
                    }
                    else if (result.Get(7) == 0)
                    {
                        result.Add(6, 1);
                    }
                }
                else if (result.Get(7) == 6)
                {
                    result.Add(6, -1);
                }
                else if (result.Get(7) == 0)
                {
                    result.Add(6, -2);
                }
            }
        }

        public bool CheckValidDate(int nweektype)
        {
            return CheckValidDate(this, nweektype);
        }

        public static bool CheckValidDate(ChartCalendar ddate, int nweektype)
        {
            bool flag = true;
            return (((nweektype != 1) || ((ddate.Get(7) != 6) && (ddate.Get(7) != 0))) && flag);
        }

        public bool CheckValidDate(long starttime, long stoptime, int nweektype)
        {
            return CheckValidDate(this, starttime, stoptime, nweektype);
        }

        public static bool CheckValidDate(ChartCalendar ddate, long starttime, long stoptime, int nweektype)
        {
            bool flag = true;
            if ((nweektype == 1) && ((ddate.Get(7) == 6) || (ddate.Get(7) == 0)))
            {
                flag = false;
            }
            if (!flag)
            {
                return flag;
            }
            long tODMsecs = GetTODMsecs(ddate);
            return (((tODMsecs >= starttime) && (tODMsecs <= stoptime)) && flag);
        }

        public override object Clone()
        {
            return new ChartCalendar(this.dateTimeValue);
        }

        public int Get(int item)
        {
            int num = 1;
            switch (item)
            {
                case 1:
                    return globalCalendar.GetYear(this.dateTimeValue);

                case 2:
                    return globalCalendar.GetMonth(this.dateTimeValue);

                case 3:
                    return globalCalendar.GetWeekOfYear(this.dateTimeValue, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);

                case 4:
                case 8:
                case 9:
                    return num;

                case 5:
                    return globalCalendar.GetDayOfMonth(this.dateTimeValue);

                case 6:
                    return globalCalendar.GetDayOfYear(this.dateTimeValue);

                case 7:
                    return (int) this.dateTimeValue.DayOfWeek;

                case 10:
                    return globalCalendar.GetHour(this.dateTimeValue);

                case 11:
                    return globalCalendar.GetHour(this.dateTimeValue);

                case 12:
                    return globalCalendar.GetMinute(this.dateTimeValue);

                case 13:
                    return globalCalendar.GetSecond(this.dateTimeValue);

                case 14:
                    return (int) globalCalendar.GetMilliseconds(this.dateTimeValue);

                case 15:
                    return (int) this.dateTimeValue.Ticks;

                case 0x10:
                    return (((int) this.dateTimeValue.Ticks) / 0x2710);
            }
            return num;
        }

        public long GetCalendarMsecs()
        {
            return (this.dateTimeValue.Ticks / 0x2710L);
        }

        public static long GetCalendarMsecs(ChartCalendar date)
        {
            return (date.dateTimeValue.Ticks / 0x2710L);
        }

        public long GetCalendarSecs()
        {
            return (this.dateTimeValue.Ticks / 0x989680L);
        }

        public static long GetCalendarSecs(ChartCalendar date)
        {
            return (date.dateTimeValue.Ticks / 0x989680L);
        }

        public long GetCalendarTimerTicks()
        {
            return this.dateTimeValue.Ticks;
        }

        public static double GetCalendarWidthValue(int timebase, double width)
        {
            double num = 1.0;
            long num2 = 0x5265c00L;
            long num3 = 0x3e8L;
            long num4 = 60L * num3;
            long num5 = 60L * num4;
            long num6 = 0x18L * num5;
            long num7 = 7L * num6;
            long num8 = 4L * num7;
            long num9 = 0x16dL * num2;
            switch (timebase)
            {
                case 1:
                    num = num9;
                    break;

                case 2:
                    num = num8;
                    break;

                case 3:
                case 4:
                    num = num7;
                    break;

                case 5:
                case 6:
                case 7:
                    num = num6;
                    break;

                case 10:
                case 11:
                    num = num5;
                    break;

                case 12:
                    num = num4;
                    break;

                case 13:
                    num = num3;
                    break;

                case 14:
                    num = num3;
                    break;
            }
            return (num * width);
        }

        public DateTime GetDateTimeValue()
        {
            return this.dateTimeValue;
        }

        public static int GetElapsedTickCount(int tickcount)
        {
            int num = Environment.TickCount - tickcount;
            if (num < 0)
            {
                num += 0x7fffffff;
            }
            return num;
        }

        public GregorianCalendar GetGlobalCalendar()
        {
            return globalCalendar;
        }

        public int GetMinimum(int item)
        {
            int num = 1;
            switch (item)
            {
                case 1:
                    return 1;

                case 2:
                    return 1;

                case 3:
                    return 1;

                case 4:
                    return 1;

                case 5:
                    return 1;

                case 6:
                    return 1;

                case 7:
                    return 0;

                case 8:
                case 9:
                    return num;

                case 10:
                    return 0;

                case 11:
                    return 0;

                case 12:
                    return 0;

                case 13:
                    return 0;

                case 14:
                    return 0;

                case 15:
                    return 0;
            }
            return num;
        }

        public DateTime GetTime()
        {
            return new DateTime(this.dateTimeValue.Ticks);
        }

        public static Size GetTimeAxisTickParameters(int timebase, int weekmode)
        {
            int width = 1;
            int height = 1;
            Size size = new Size(1, 1);
            int num3 = 7;
            if (weekmode == 1)
            {
                num3 = 5;
            }
            switch (timebase)
            {
                case 6:
                    width = 10;
                    height = 1;
                    break;

                case 7:
                    width = 10;
                    height = 1;
                    break;

                case 8:
                    width = 10;
                    height = 1;
                    break;

                case 9:
                    width = 10;
                    height = 1;
                    break;

                case 10:
                    width = 10;
                    height = 1;
                    break;

                case 11:
                    width = 1;
                    height = 1;
                    break;

                case 12:
                    width = 2;
                    height = 1;
                    break;

                case 13:
                    width = 5;
                    height = 1;
                    break;

                case 14:
                    width = 10;
                    height = 1;
                    break;

                case 15:
                    width = 15;
                    height = 1;
                    break;

                case 0x10:
                    width = 30;
                    height = 1;
                    break;

                case 0x12:
                    width = 60;
                    height = 2;
                    break;

                case 0x13:
                    width = 60;
                    height = 5;
                    break;

                case 20:
                    width = 60;
                    height = 10;
                    break;

                case 0x15:
                    width = 60;
                    height = 15;
                    break;

                case 0x16:
                    width = 60;
                    height = 30;
                    break;

                case 0x19:
                    width = 1;
                    height = 1;
                    break;

                case 0x1a:
                    width = 2;
                    height = 1;
                    break;

                case 0x1b:
                    width = 5;
                    height = 1;
                    break;

                case 0x1c:
                    width = 10;
                    height = 1;
                    break;

                case 0x1d:
                    width = 15;
                    height = 1;
                    break;

                case 30:
                    width = 30;
                    height = 2;
                    break;

                case 0x24:
                    width = 60;
                    height = 2;
                    break;

                case 0x25:
                    width = 60;
                    height = 5;
                    break;

                case 0x26:
                    width = 60;
                    height = 10;
                    break;

                case 0x27:
                    width = 60;
                    height = 15;
                    break;

                case 40:
                    width = 60;
                    height = 30;
                    break;

                case 0x2f:
                    width = 1;
                    height = 1;
                    break;

                case 0x30:
                    width = 2;
                    height = 1;
                    break;

                case 0x31:
                    width = 4;
                    height = 1;
                    break;

                case 50:
                    width = 8;
                    height = 1;
                    break;

                case 0x33:
                    width = 12;
                    height = 1;
                    break;

                case 0x38:
                    width = 0x18;
                    height = 2;
                    break;

                case 0x39:
                    width = 0x18;
                    height = 4;
                    break;

                case 0x3a:
                    width = 0x18;
                    height = 8;
                    break;

                case 0x3b:
                    width = 0x18;
                    height = 12;
                    break;

                case 60:
                    width = 1;
                    height = 1;
                    break;

                case 0x45:
                    width = num3;
                    height = 1;
                    break;

                case 70:
                    width = 1;
                    height = 1;
                    break;

                case 0x58:
                    width = 1;
                    height = 1;
                    break;

                case 0x59:
                    width = 1;
                    height = 1;
                    break;

                case 90:
                    width = 1;
                    height = 1;
                    break;

                case 0x63:
                    width = 1;
                    height = 1;
                    break;

                case 100:
                    width = 1;
                    height = 1;
                    break;

                case 0x6d:
                    width = 1;
                    height = 1;
                    break;

                case 110:
                    width = 1;
                    height = 1;
                    break;

                case 0x6f:
                    width = 1;
                    height = 1;
                    break;

                case 0x70:
                    width = 5;
                    height = 1;
                    break;

                case 0x71:
                    width = 10;
                    height = 1;
                    break;

                case 0x72:
                    width = 20;
                    height = 4;
                    break;

                case 0x73:
                    width = 50;
                    height = 5;
                    break;
            }
            return new Size(width, height);
        }

        public long GetTODMsecs()
        {
            return (this.dateTimeValue.TimeOfDay.Ticks / 0x2710L);
        }

        public static long GetTODMsecs(ChartCalendar date)
        {
            return (date.dateTimeValue.TimeOfDay.Ticks / 0x2710L);
        }

        public long GetTODSeconds()
        {
            return (this.dateTimeValue.TimeOfDay.Ticks / 0x989680L);
        }

        public static long GetTODSeconds(ChartCalendar date)
        {
            return (date.dateTimeValue.TimeOfDay.Ticks / 0x989680L);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x406;
        }

        public DateTime LocalNowFunction()
        {
            if (altTimeCalc)
            {
                return startDateTime.AddMilliseconds((double) ElapsedTickCount);
            }
            return DateTime.Now;
        }

        public static ChartCalendar NewCalendar(ChartCalendar source)
        {
            return new ChartCalendar(source.GetTime());
        }

        public static ChartCalendar Parse(string datestring)
        {
            return new ChartCalendar(DateTime.Parse(datestring));
        }

        public static ChartCalendar Parse(string datestring, IFormatProvider formatprovider)
        {
            return new ChartCalendar(DateTime.Parse(datestring, formatprovider));
        }

        public static ChartCalendar Parse(string datestring, IFormatProvider formatprovider, DateTimeStyles style)
        {
            return new ChartCalendar(DateTime.Parse(datestring, formatprovider, style));
        }

        public void Set(int item, int value)
        {
            DateTime time;
            switch (item)
            {
                case 1:
                    time = new DateTime(value, this.dateTimeValue.Month, this.dateTimeValue.Day, this.dateTimeValue.Hour, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 2:
                    time = new DateTime(this.dateTimeValue.Year, value, this.dateTimeValue.Day, this.dateTimeValue.Hour, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 5:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, value, this.dateTimeValue.Hour, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 6:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, value, this.dateTimeValue.Hour, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 10:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, this.dateTimeValue.Day, value, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 11:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, this.dateTimeValue.Day, value, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 12:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, this.dateTimeValue.Day, this.dateTimeValue.Hour, value, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;

                case 13:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, this.dateTimeValue.Day, this.dateTimeValue.Hour, this.dateTimeValue.Minute, value, this.dateTimeValue.Millisecond);
                    break;

                case 14:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, this.dateTimeValue.Day, this.dateTimeValue.Hour, this.dateTimeValue.Minute, this.dateTimeValue.Second, value);
                    break;

                case 15:
                    time = new DateTime((long) value);
                    break;

                case 0x10:
                    time = new DateTime((long) (value * 0x2710));
                    break;

                default:
                    time = new DateTime(this.dateTimeValue.Year, this.dateTimeValue.Month, this.dateTimeValue.Day, this.dateTimeValue.Hour, this.dateTimeValue.Minute, this.dateTimeValue.Second, this.dateTimeValue.Millisecond);
                    break;
            }
            this.dateTimeValue = time;
        }

        public void SetCalendarMsecs(long msecs)
        {
            DateTime time = new DateTime(msecs * 0x2710L);
            this.SetTime(time);
        }

        public static void SetCalendarMsecs(ChartCalendar date, long msecs)
        {
            DateTime time = new DateTime(msecs * 0x2710L);
            date.SetTime(time);
        }

        public void SetCalendarSecs(long secs)
        {
            DateTime time = new DateTime(secs * 0x989680L);
            this.SetTime(time);
        }

        public static void SetCalendarSecs(ChartCalendar date, long secs)
        {
            DateTime time = new DateTime(secs * 0x989680L);
            date.SetTime(time);
        }

        public void SetDateTimeValue(DateTime time)
        {
            this.dateTimeValue = time;
        }

        public void SetGlobalCalendar(GregorianCalendar calendar)
        {
            globalCalendar = calendar;
        }

        public void SetTime(DateTime time)
        {
            this.dateTimeValue = new DateTime(time.Ticks);
        }

        public void SetTOD(ChartCalendar ttime)
        {
            SetTOD(this, ttime);
        }

        public static void SetTOD(ChartCalendar ddate, ChartCalendar ttime)
        {
            ChartCalendar source = new ChartCalendar(ddate.Get(1), ddate.Get(2), ddate.Get(5), ttime.Get(11), ttime.Get(12), ttime.Get(13), ttime.Get(14));
            CalendarCopy(ddate, source);
        }

        public void SetTOD(int hour, int minute, int second)
        {
            SetTOD(this, hour, minute, second);
        }

        public static void SetTOD(ChartCalendar ddate, int hour, int minute, int second)
        {
            ChartCalendar source = new ChartCalendar(ddate.Get(1), ddate.Get(2), ddate.Get(5), hour, minute, second, 0);
            CalendarCopy(ddate, source);
        }

        public void SetTODMsecs(long milliseconds)
        {
            SetTODMsecs(this, milliseconds);
        }

        public static void SetTODMsecs(ChartCalendar ddate, long milliseconds)
        {
            if (milliseconds >= 0x5265c00L)
            {
                int num = ((int) milliseconds) / 0x5265c00;
                ddate.Add(6, num);
                milliseconds -= num * 0x5265c00;
            }
            DateTime time = ddate.dateTimeValue.Date.AddMilliseconds((double) milliseconds);
            ddate.dateTimeValue = time;
        }

        public void SetTODSeconds(long seconds)
        {
            SetTODSeconds(this, seconds);
        }

        public static void SetTODSeconds(ChartCalendar ddate, long seconds)
        {
            if (seconds >= 0x15180L)
            {
                int num = ((int) seconds) / 0x15180;
                ddate.Add(6, num);
                seconds -= num * 0x15180;
            }
            DateTime time = ddate.dateTimeValue.Date.AddSeconds((double) seconds);
            ddate.dateTimeValue = time;
        }

        public static void SyncStartTickCount()
        {
            startTickCount = Environment.TickCount;
        }

        public override string ToString()
        {
            return this.dateTimeValue.ToString();
        }

        public string ToString(string formatstring)
        {
            return this.dateTimeValue.ToString(formatstring);
        }

        public string ToString(string formatstring, IFormatProvider formatprovider)
        {
            return this.dateTimeValue.ToString(formatstring, formatprovider);
        }

        public static bool AltTimeCalc
        {
            get
            {
                return altTimeCalc;
            }
            set
            {
                altTimeCalc = value;
            }
        }

        public static int CurrentTickCount
        {
            get
            {
                return Environment.TickCount;
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                return this.dateTimeValue;
            }
            set
            {
                this.dateTimeValue = value;
            }
        }

        public static int ElapsedTickCount
        {
            get
            {
                long num = CurrentTickCount - StartTickCount;
                if (num < 0L)
                {
                    num += 0x7fffffffL;
                }
                return (int) num;
            }
        }

        public GregorianCalendar GlobalCalendar
        {
            get
            {
                return globalCalendar;
            }
            set
            {
                globalCalendar = value;
            }
        }

        public static ChartCalendar Now
        {
            get
            {
                return new ChartCalendar();
            }
        }

        public static int StartTickCount
        {
            get
            {
                return startTickCount;
            }
            set
            {
                startTickCount = value;
            }
        }
    }
}

