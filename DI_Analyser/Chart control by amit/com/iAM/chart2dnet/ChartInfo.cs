namespace com.iAM.chart2dnet
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    internal class ChartInfo : ChartObj
    {
        private int eDateScaleFactor;
        private bool fileFound;
        private string infoFilename;
        private long[] infoNums;
        private string[] infoStrings;
        private string licensePath;
        public const int iAMAllProductID = 0x13;
        public const int iAMChart2DCFProductID = 3;
        public const int iAMChart2DJavaDLL = 0x10000;
        public const int iAMChart2DNetCFDLL = 0x100;
        public const int iAMChart2DNetDLL = 1;
        public const int iAMChart2DProductID = 1;
        private DataSet iAMLicenseDataSet;
        public const int iAMRTGraphCFProductID = 4;
        public const int iAMRTGraphJavaDLL = 0x20000;
        public const int iAMRTGraphNetCFDLL = 0x200;
        public const int iAMRTGraphNetDLL = 2;
        public const int iAMRTGraphProductID = 2;
        public const int iAMSPCChartCFProductID = 8;
        public const int iAMSPCChartJavaDLL = 0x40000;
        public const int iAMSPCChartNetCFDLL = 0x400;
        public const int iAMSPCChartNetDLL = 4;
        public const int iAMSPCChartProductID = 7;

        public ChartInfo()
        {
            this.infoNums = new long[] { 
                0xa8d7218805c64L, 0xa8d7218805c65L, 0xaca8f039705bL, 0x11d0bbaab86519L, 0x98e49d7ab06d2L, 0x148f424876886aL, 0x9cb81dccae99cL, 0x110184640c36b3L, 0x10321c55c8bde9L, 0x78fedf7277300L, 0x12e9eecf07684fL, 0xc4ea1ca07f5ebL, 0x83845b2a300a1L, 0x886214ede0b47L, 0xc3b76a85d3cd0L, 0x55d24b7e8d968L, 
                0x42f2d3a9819a9L
             };
            this.infoStrings = new string[0x17];
            this.licensePath = "";
            this.infoFilename = "";
            this.iAMLicenseDataSet = new DataSet("iAMLicenseDataSet");
            this.fileFound = true;
            this.eDateScaleFactor = 0x2f;
        }

        public ChartInfo(string xmlresource)
        {
            this.infoNums = new long[] { 
                0xa8d7218805c64L, 0xa8d7218805c65L, 0xaca8f039705bL, 0x11d0bbaab86519L, 0x98e49d7ab06d2L, 0x148f424876886aL, 0x9cb81dccae99cL, 0x110184640c36b3L, 0x10321c55c8bde9L, 0x78fedf7277300L, 0x12e9eecf07684fL, 0xc4ea1ca07f5ebL, 0x83845b2a300a1L, 0x886214ede0b47L, 0xc3b76a85d3cd0L, 0x55d24b7e8d968L, 
                0x42f2d3a9819a9L
             };
            this.infoStrings = new string[0x17];
            this.licensePath = "";
            this.infoFilename = "";
            this.iAMLicenseDataSet = new DataSet("iAMLicenseDataSet");
            this.fileFound = true;
            this.eDateScaleFactor = 0x2f;
            this.ReadXMLResourceTable(xmlresource);
        }

        public ChartInfo(string licensepath, string filename)
        {
            this.infoNums = new long[] { 
                0xa8d7218805c64L, 0xa8d7218805c65L, 0xaca8f039705bL, 0x11d0bbaab86519L, 0x98e49d7ab06d2L, 0x148f424876886aL, 0x9cb81dccae99cL, 0x110184640c36b3L, 0x10321c55c8bde9L, 0x78fedf7277300L, 0x12e9eecf07684fL, 0xc4ea1ca07f5ebL, 0x83845b2a300a1L, 0x886214ede0b47L, 0xc3b76a85d3cd0L, 0x55d24b7e8d968L, 
                0x42f2d3a9819a9L
             };
            this.infoStrings = new string[0x17];
            this.licensePath = "";
            this.infoFilename = "";
            this.iAMLicenseDataSet = new DataSet("iAMLicenseDataSet");
            this.fileFound = true;
            this.eDateScaleFactor = 0x2f;
            this.infoFilename = filename;
            this.licensePath = licensepath;
            this.ReadTable(licensepath, filename);
        }

        public override object Clone()
        {
            ChartInfo info = new ChartInfo(this.licensePath, this.infoFilename);
            info.Copy(this);
            return info;
        }

        public void Copy(ChartInfo source)
        {
            if (source != null)
            {
                int num;
                for (num = 0; num < this.infoNums.Length; num++)
                {
                    this.infoNums[num] = source.infoNums[num];
                }
                for (num = 0; num < this.infoStrings.Length; num++)
                {
                    this.infoStrings[num] = string.Copy(source.infoStrings[num]);
                }
            }
        }

        internal void GetInfo(long[] nums, string[] strings)
        {
            int num;
            for (num = 0; num < Math.Min(nums.Length, this.infoNums.Length); num++)
            {
                nums[num] = this.infoNums[num];
            }
            for (num = 0; num < Math.Min(strings.Length, this.infoStrings.Length); num++)
            {
                strings[num] = this.infoStrings[num];
            }
        }

        private void ReadTable(string licensepath, string filename)
        {
            int num;
            this.fileFound = false;
            string str = "";
            DataSet iAMLicenseDataSet = this.iAMLicenseDataSet;
            iAMLicenseDataSet.Locale = new CultureInfo("en");
            try
            {
                iAMLicenseDataSet.ReadXml(licensepath + @"\" + filename);
            }
            catch (IOException)
            {
                try
                {
                    iAMLicenseDataSet.ReadXml(str + "/" + filename);
                }
                catch (IOException)
                {
                    try
                    {
                        iAMLicenseDataSet.ReadXml(filename);
                    }
                    catch (IOException)
                    {
                        return;
                    }
                }
            }
            this.fileFound = true;
            DataTableCollection tables = iAMLicenseDataSet.Tables;
            DataTable table = tables["LicenseLongTable"];
            for (num = 0; num < this.infoNums.Length; num++)
            {
                this.infoNums[num] = (long) table.Rows[num].ItemArray[0];
            }
            DataTable table2 = tables["LicenseStringTable"];
            for (num = 0; num < this.infoStrings.Length; num++)
            {
                this.infoStrings[num] = (string) table2.Rows[num].ItemArray[0];
            }
        }

        private void ReadXMLResourceTable(string xmlresource)
        {
            this.fileFound = false;
            DataSet iAMLicenseDataSet = this.iAMLicenseDataSet;
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream manifestResourceStream = null;
            if (executingAssembly != null)
            {
                manifestResourceStream = executingAssembly.GetManifestResourceStream(xmlresource);
            }
            if ((manifestResourceStream == null) && (entryAssembly != null))
            {
                manifestResourceStream = entryAssembly.GetManifestResourceStream(xmlresource);
            }
            if (manifestResourceStream != null)
            {
                int num;
                try
                {
                    iAMLicenseDataSet.ReadXml(manifestResourceStream, XmlReadMode.Auto);
                }
                catch (IOException)
                {
                    return;
                }
                this.fileFound = true;
                DataTableCollection tables = iAMLicenseDataSet.Tables;
                DataTable table = tables["LicenseLongTable"];
                for (num = 0; num < this.infoNums.Length; num++)
                {
                    this.infoNums[num] = (long) table.Rows[num].ItemArray[0];
                }
                DataTable table2 = tables["LicenseStringTable"];
                for (num = 0; num < this.infoStrings.Length; num++)
                {
                    this.infoStrings[num] = (string) table2.Rows[num].ItemArray[0];
                }
            }
        }

        public long DLLFlags
        {
            get
            {
                return this.infoNums[3];
            }
        }

        public ChartCalendar ExpirationDate
        {
            get
            {
                ChartCalendar date = new ChartCalendar();
                long msecs = this.infoNums[0] / ((long) this.eDateScaleFactor);
                ChartCalendar.SetCalendarMsecs(date, msecs);
                return date;
            }
        }

        public bool FileFound
        {
            get
            {
                return this.fileFound;
            }
        }

        public long[] InfoNums
        {
            get
            {
                return this.infoNums;
            }
        }

        public string[] InfoStrings
        {
            get
            {
                return this.infoStrings;
            }
        }

        public long InstallDate
        {
            get
            {
                return this.infoNums[5];
            }
        }

        public int LicenseMode
        {
            get
            {
                return (int) (this.infoNums[1] - this.infoNums[0]);
            }
        }

        public bool NewLicense
        {
            get
            {
                return (this.infoNums[2] < 0x3e8L);
            }
        }

        public int ProductID
        {
            get
            {
                int num = 1;
                string s = "";
                if ((this.infoStrings[0x15] != null) && (this.infoStrings[0x15].Length > 0))
                {
                    s = string.Copy(this.infoStrings[0x15]);
                }
                if (s == null)
                {
                    return num;
                }
                if (s.Length == 0)
                {
                    return 1;
                }
                return (int) ((long.Parse(s) - this.infoNums[1]) - 0x1e240L);
            }
        }

        public long TrialDLLFlags
        {
            get
            {
                return this.infoNums[4];
            }
        }
    }
}

