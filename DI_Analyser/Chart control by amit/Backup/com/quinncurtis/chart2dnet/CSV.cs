namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class CSV : ChartObj
    {
        internal char columnDelimiterChar;
        internal bool forceQuotes;
        internal const int INPLAIN = 1;
        internal const int INQUOTED = 2;
        internal string newLineString;
        internal int orientation;
        internal const int SEEKING = 0;
        internal const int SEENQUOTE = 3;
        internal DateTimeFormatInfo timeDateFormat;
        internal string timeDateFormatString;
        internal bool wasPreviousField;

        public CSV()
        {
            this.columnDelimiterChar = ',';
            this.newLineString = "\r\n";
            this.timeDateFormat = new DateTimeFormatInfo();
            this.timeDateFormatString = "M/dd/yyyy";
            this.orientation = 1;
            this.forceQuotes = false;
            this.wasPreviousField = false;
        }

        public CSV(int orient)
        {
            this.columnDelimiterChar = ',';
            this.newLineString = "\r\n";
            this.timeDateFormat = new DateTimeFormatInfo();
            this.timeDateFormatString = "M/dd/yyyy";
            this.orientation = 1;
            this.forceQuotes = false;
            this.wasPreviousField = false;
            this.orientation = orient;
        }

        public override object Clone()
        {
            CSV csv = new CSV();
            csv.columnDelimiterChar = this.columnDelimiterChar;
            csv.newLineString = this.newLineString;
            csv.timeDateFormat = this.timeDateFormat;
            csv.timeDateFormatString = this.timeDateFormatString;
            csv.orientation = this.orientation;
            csv.forceQuotes = this.forceQuotes;
            csv.wasPreviousField = this.wasPreviousField;
            return csv;
        }

        public char GetColumnDelimiterChar()
        {
            return this.columnDelimiterChar;
        }

        public int GetFileNumColumns(StreamReader pReader)
        {
            int num = 0;
            try
            {
                try
                {
                    string str;
                Label_0002:
                    str = this.Read(pReader);
                    if (str.CompareTo(this.newLineString) != 0)
                    {
                        str.Trim();
                        num++;
                        goto Label_0002;
                    }
                }
                catch (EndOfStreamException)
                {
                }
            }
            catch (IOException)
            {
                this.ErrorCheck(ChartObj.ERROR_FILEREAD);
            }
            return num;
        }

        public int GetFileNumRows(StreamReader pReader)
        {
            int num = 0;
            try
            {
                try
                {
                    while (true)
                    {
                        this.Readln(pReader);
                        num++;
                    }
                }
                catch (EndOfStreamException)
                {
                }
            }
            catch (IOException)
            {
                this.ErrorCheck(ChartObj.ERROR_FILEREAD);
            }
            return num;
        }

        public string GetNewLineString()
        {
            return this.newLineString;
        }

        public int GetOrientation()
        {
            return this.orientation;
        }

        public DateTimeFormatInfo GetTimeDateFormat()
        {
            return this.timeDateFormat;
        }

        public string GetTimeDateFormatString()
        {
            return this.timeDateFormatString;
        }

        public string Read(StreamReader pReader)
        {
            int num2;
            StringBuilder builder = new StringBuilder(50);
            int num = 0;
            while ((num2 = pReader.Read()) >= 0)
            {
                char ch2;
                char ch = (char) num2;
                switch (num)
                {
                    case 0:
                        ch2 = ch;
                        switch (ch2)
                        {
                            case '\t':
                            case ',':
                                return "";

                            case '\n':
                                return this.newLineString;

                            case '\v':
                            case '\f':
                            case '!':
                                goto Label_0084;

                            case '\r':
                            case ' ':
                            {
                                continue;
                            }
                        }
                        goto Label_0084;

                    case 1:
                        ch2 = ch;
                        switch (ch2)
                        {
                            case '\t':
                            case '\r':
                            case ',':
                                return builder.ToString().Trim();

                            case '\n':
                                throw new IOException("Malformed CSV stream. Cr missing before Lf.");

                            case '\v':
                            case '\f':
                            case ' ':
                            case '!':
                                goto Label_00F4;

                            case '"':
                                throw new IOException("Malformed CSV steam. Missing quote at start of field.");
                        }
                        goto Label_00F4;

                    case 2:
                        ch2 = ch;
                        switch (ch2)
                        {
                            case '\t':
                            case '\n':
                            case '\r':
                                throw new IOException("Malformed CSV stream. Missing quote after field.");

                            case '\v':
                            case '\f':
                            case ' ':
                            case '!':
                            case ',':
                                goto Label_014F;

                            case '"':
                                goto Label_0140;
                        }
                        goto Label_014F;

                    case 3:
                        ch2 = ch;
                        if (ch2 > '\r')
                        {
                            goto Label_0170;
                        }
                        switch (ch2)
                        {
                            case '\n':
                                throw new IOException("Malformed CSV stream. Cr missing before Lf.");

                            case '\r':
                                goto Label_019B;
                        }
                        goto Label_01B6;

                    default:
                    {
                        continue;
                    }
                }
                num = 2;
                continue;
            Label_0084:
                builder.Append(ch);
                num = 1;
                continue;
            Label_00F4:
                builder.Append(ch);
                continue;
            Label_0140:
                num = 3;
                continue;
            Label_014F:
                builder.Append(ch);
                continue;
            Label_0170:
                switch (ch2)
                {
                    case ' ':
                    {
                        num = 1;
                        continue;
                    }
                    case '"':
                    {
                        builder.Append('"');
                        num = 2;
                        continue;
                    }
                    case ',':
                        goto Label_019B;

                    default:
                        goto Label_01B6;
                }
            Label_019B:
                return builder.ToString().Trim();
            Label_01B6:
                throw new IOException("Malformed CSV stream, missing comma after field.");
            }
            throw new EndOfStreamException("End-of-file");
        }

        public double ReadDouble(StreamReader pReader)
        {
            double maxValue = double.MaxValue;
            string s = this.Read(pReader);
            if (s.CompareTo("") == 0)
            {
                return maxValue;
            }
            try
            {
                return double.Parse(s);
            }
            catch (FormatException)
            {
                return double.MaxValue;
            }
        }

        public void Readln(StreamReader pReader)
        {
            while (this.newLineString.CompareTo(this.Read(pReader)) != 0)
            {
            }
        }

        public long ReadLong(StreamReader pReader)
        {
            string s = this.Read(pReader);
            long num = 0L;
            if (s.CompareTo("") == 0)
            {
                return num;
            }
            try
            {
                return long.Parse(s);
            }
            catch (FormatException)
            {
                return 0L;
            }
        }

        public ChartCalendar ReadTime(StreamReader pReader)
        {
            ChartCalendar date = new ChartCalendar();
            string s = this.Read(pReader);
            if (s.CompareTo("") != 0)
            {
                try
                {
                    DateTime time = DateTime.Parse(s, this.timeDateFormat);
                    date.SetTime(time);
                }
                catch (FormatException)
                {
                    ChartCalendar.SetCalendarMsecs(date, 0L);
                }
            }
            return date;
        }

        public void SetColumnDelimiterChar(char c)
        {
            this.columnDelimiterChar = c;
        }

        public void SetNewLineString(string s)
        {
            this.newLineString = s;
        }

        public void SetOrientation(int orient)
        {
            this.orientation = orient;
        }

        public void SetTimeDateFormat(DateTimeFormatInfo format)
        {
            this.timeDateFormat = format;
        }

        public void SetTimeDateFormatString(string format)
        {
            this.timeDateFormatString = format;
        }

        public void Skip(StreamReader pReader, int fields)
        {
            if (fields > 0)
            {
                for (int i = 0; i < fields; i++)
                {
                    this.Read(pReader);
                }
            }
        }

        public void Write(StreamWriter pWriter, string s)
        {
            if (this.wasPreviousField)
            {
                pWriter.Write(this.columnDelimiterChar);
            }
            s = s.Trim();
            if (s.IndexOf('"') >= 0)
            {
                pWriter.Write('"');
                for (int i = 0; i < s.Length; i++)
                {
                    char ch = s[i];
                    if (ch == '"')
                    {
                        pWriter.Write("\"\"");
                    }
                    else
                    {
                        pWriter.Write(ch);
                    }
                }
                pWriter.Write('"');
            }
            else if (this.forceQuotes || (s.IndexOf(this.columnDelimiterChar) >= 0))
            {
                pWriter.Write('"');
                pWriter.Write(s);
                pWriter.Write('"');
            }
            else
            {
                pWriter.Write(s);
            }
            this.wasPreviousField = true;
        }

        public void WriteDouble(StreamWriter pWriter, double r)
        {
            if (ChartSupport.BGoodValue(r))
            {
                string s = Convert.ToString(r);
                this.Write(pWriter, s);
            }
            else
            {
                this.Write(pWriter, "");
            }
        }

        public void Writeln(StreamWriter pWriter)
        {
            pWriter.Write(this.newLineString);
            this.wasPreviousField = false;
        }

        public void WriteLong(StreamWriter pWriter, long l)
        {
            string s = Convert.ToString(l);
            this.Write(pWriter, s);
        }

        public void WriteTime(StreamWriter pWriter, ChartCalendar gregdate)
        {
            string s = gregdate.GetTime().ToString(this.timeDateFormatString, this.timeDateFormat);
            this.Write(pWriter, s);
        }

        public char ColumnDelimiterChar
        {
            get
            {
                return this.columnDelimiterChar;
            }
            set
            {
                this.columnDelimiterChar = value;
            }
        }

        public string NewLineString
        {
            get
            {
                return this.newLineString;
            }
            set
            {
                this.newLineString = value;
            }
        }

        public int Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value;
            }
        }

        public DateTimeFormatInfo TimeDateFormatObj
        {
            get
            {
                return this.timeDateFormat;
            }
            set
            {
                this.timeDateFormat = value;
            }
        }

        public string TimeDateFormatString
        {
            get
            {
                return this.timeDateFormatString;
            }
            set
            {
                this.timeDateFormatString = value;
            }
        }
    }
}

