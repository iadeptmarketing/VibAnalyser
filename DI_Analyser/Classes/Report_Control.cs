//Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
//Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
//Amit Jain    DA_35	RPM not display correctly and fault frequency also in Report	code related	minor 	8-4-2010
                

using System;
using System.Collections.Generic;

using System.Text;
using DI_Analyser.interfaces;
using System.Data;
using System.IO;
using System.Xml;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Analyser.Reporting;
using DI_Analyser.Reporting;
using Analyser.Classes;
using System.Windows.Forms;
//using com.iAM.chart2dnet;
using Analyser.interfaces;
using com.iAM.chart2dnet;
//using Analyser.Graph_Controls;
using polar = Analyser.Graph_Controls;


namespace DI_Analyser.Classes
{
    /// <summary>
    /// Amit Jain   11-Feb-2010
    /// Class to create the Reporting Module for the Analyser
    /// </summary>
    class Report_Control : Report_Interface
    {
        #region clv
        Report objReport = null;
        string sErrorLogPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
        string HighAlValue = null;
        string LowAlValue = null;
        string Freq = null;
        string prevFreq = null;
        string displayed = null;
        double HighestPeakinArea = 0;
        double HighestPeakinAreaAt = 0;
        double TempPeakinArea = 0;
        double[] Xdata = null;
        double[] Ydata = null;
        Form1 objForm = null;
        string[] BndAlrmsHigh = null;
        string[] BndAlrmsLow = null;
        string[] BndAlrmsFreq = null;
        string sCSVData = null;
        ArrayList BandValuetoDisplay = new ArrayList();
        int OneFifty = 0;
        int TwoHundred = 0;
        int FourFifty = 0;
        int FourHundred = 0;
        int SixtyTwo = 62;// (int)Math.Round((6.7982 * pt.Right) / 100, 0);
        int OneSixtyTwo = 0;
        int Thrghty = 0;
        int Fifteen = 0;
        int TwoPointFive = 0;
        int Three = 0;
        int Fifty = 0;
        bool Time = false;
        private int n, nu;
        double HighestValYAxis = 0;
        double MainYAxisInterval = 0;
        double MainXAxisInterval = 0;
        double CursorStartInterval = 0;
        double setAxisCtr = 0;
        double TotalYAxis = 0.0;
        int prtType = 0;
        int MainIndex = 0;
        private byte[] byteImageData;
        ArrayList arldata = new ArrayList();
        
        int[] arrMainIndex = new int[0];
        double TotalXAxis = 0.0;
        ArrayList arlSelectedDataGridValue = null;
        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();
        #endregion
        ArrayList WAVDataValues = null;
        public ArrayList _WAVDataValues
        {
            get
            {
                return WAVDataValues;
            }
            set
            {
                WAVDataValues = value;
            }
        }
        public ArrayList _WAVDataGridValue
        {
            get
            {
                return arlSelectedDataGridValue;
            }
            set
            {
                arlSelectedDataGridValue = value;
            }
        }


        public Report _Report
        {
            get
            {
                return objReport;
            }
            set
            {
                objReport = value;
            }
        }
        public Form1 _Form1
        {
            get
            {
                return objForm;
            }
            set
            {
                objForm = value;
            }
        }
        public string _CSVData
        {
            get
            {
                return sCSVData;
            }
            set
            {
                sCSVData = value;
            }
        }
        public double[] _Xdata
        {
            set
            {
                Xdata = value;
            }
        }
        public double[] _Ydata
        {
            set
            {
                Ydata = value;
            }
        }
        public ArrayList _DATA
        {
            get
            {
                return arldata;
            }
            set
            {
                arldata = value;
            }
        }
        string sXUnit = null;
        public string _Xunit
        {
            set
            {
                sXUnit = value;
            }
        }
        string sYUnit = null;
        public string _Yunit
        {
            set
            {
                sYUnit = value;
            }
        }
        string sFilepath = null;
        public string _CurrentFilePath
        {
            set
            {
                sFilepath = value;
            }
        }
        string SelectedCaption = null;
        public string _SelectedCaption
        {
            get
            {
                return SelectedCaption;
            }
            set
            {
                SelectedCaption = value;
            }
        }

        public int generateReport(string selectedReport)
        {
            try
            {
                if (_Report._printcontrol1.PrintingSystem != null)
                {
                    _Report._printcontrol1.PrintingSystem.ClearContent();
                    _Report._printcontrol1.PrintingSystem.Dispose();
                }

                Dowork(selectedReport);
                
                WrkerCmpleted();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return prtType;
        }


        private void WrkerCmpleted()
        {
            if (prtType == 1)
            {
                if (_Report._CSVreport1.RowCount > 0)
                {
                    _Report._printcontrol1.PrintingSystem = _Report._CSVreport1.PrintingSystem;
                    _Report._CSVreport1.CreateDocument();
                    //try
                    //{
                        
                    //    _Report._CSVreport1.ExportToImage("c:\\Test1.bmp");
                    //    _Report._CSVreport1.ExportToRtf("c:\\Test1.rtf");
                    //    _Report._CSVreport1.ExportToXls("c:\\Test1.xls");
                        
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                }
            }
            else if (prtType == 2)
            {
                if (_Report._CSVBandReport.RowCount > 0)
                {
                    _Report._printcontrol1.PrintingSystem = _Report._CSVBandReport.PrintingSystem;
                    _Report._CSVBandReport.CreateDocument();
                }
            }
            else if (prtType == 3)
            {
                if (_Report._WAVESelectedReport.RowCount > 0)
                {
                    _Report._printcontrol1.PrintingSystem = _Report._WAVESelectedReport.PrintingSystem;
                    _Report._WAVESelectedReport.CreateDocument();
                }
            }
            else if (prtType == 4)
            {
                if (_Report._MultiFileReport.RowCount > 0)
                {
                    _Report._printcontrol1.PrintingSystem = _Report._MultiFileReport.PrintingSystem;
                    _Report._MultiFileReport.CreateDocument();
                }
            }

        }
        Hashtable _hashtable = new Hashtable();
        double[] xarray = new double[0];
        double[] yarray = new double[0];
        string sMF_LOR = null;
        string sMF_Overall = null;
        string sMF_Date = null;
        string sMF_Xunit = null;
        string sMF_Yunit = null;
        string sMF_FileName = null;
        private void ReadCSVfileForData(string pathforCSV)
        {
            string type = null;
            xarray = new double[0];
            yarray = new double[0];
            string sApp = null;

              sMF_LOR = null;
              sMF_Overall = null;
              sMF_Date = null;
              sMF_Xunit = null;
              sMF_Yunit = null;
            sMF_FileName=null;
            //string sDate = null;
            //string Resolution = null;
            //string xunit = null;
            //string yunit = null;
            string[] sarrpath = pathforCSV.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            StreamReader myReader = null;
            string newpath = null;
            for (int i = 0; i < sarrpath.Length - 1; i++)
            {
                newpath += sarrpath[i].ToString() + "\\";
            }
            try
            {
                myReader = new StreamReader(pathforCSV);
                sMF_FileName = sarrpath[sarrpath.Length - 1];
                string line = null;
                int totalValues = 0;
                bool IsFoundValues = false;
                bool chakstream = false;
                bool Incompatible = false;

                while ((line = myReader.ReadLine()) != null || (line = myReader.ReadLine()) != "")
                {

                    chakstream = myReader.EndOfStream;
                    
                    //if (chakstream == false)
                    {
                        string first = null;
                        string second = null;
                        string[] strread = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (strread.Length > 0)
                        {
                            if (strread[0] != null || strread[0] != "")
                            {
                                first = strread[0].ToString();
                            }
                        }
                        if (strread.Length > 1)
                        {
                            if (strread[1] != null || strread[1] != "")
                            {
                                second = strread[1].ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(first))
                        {
                            switch (first)
                            {
                                case "Application":
                                    {
                                        sApp = second.ToString();
                                        break;
                                    }
                                case "User ID":
                                    {
                                        break;
                                    }
                                case "Unit ID":
                                    {
                                        break;
                                    }
                                case "Date/Time":
                                    {
                                        sMF_Date = second.ToString();
                                        break;
                                    }

                                case "Trigger Type":
                                    {
                                        break;
                                    }
                                case "Trigger Level":
                                    {
                                        break;
                                    }
                                case "Slope":
                                    {
                                        break;
                                    }
                                case "Transducer Units":
                                    {
                                        break;
                                    }
                                case "Sensitivity":
                                    {
                                        break;
                                    }
                                case "Transducer Offset":
                                    {
                                        break;
                                    }
                                case "Display Units":
                                    {
                                        
                                        byte[] _charUnit1 = Encoding.ASCII.GetBytes(second.ToString());
                                        if (_charUnit1.Length == 4)
                                        {
                                            if (_charUnit1[0].ToString() == "109" && _charUnit1[1].ToString() == "47" && _charUnit1[2].ToString() == "115" && _charUnit1[3].ToString() == "63")
                                            {
                                                second = "m/s2";
                                            }
                                        }
                                        sMF_Yunit = second.ToString();
                                        break;
                                    }
                                case "High Pass Filter":
                                    {
                                        break;
                                    }
                                case "Coupling Type":
                                    {
                                        break;
                                    }
                                case "Channel Input":
                                    {
                                        break;
                                    }
                                case "Freq Type":
                                    {
                                        break;
                                    }
                                case "Max Freq / Orders":
                                    {
                                        break;
                                    }
                                case "No. of Averages":
                                    {
                                        break;
                                    }
                                case "Average Type":
                                    {
                                        break;
                                    }
                                case "Overlap (%)":
                                    {
                                        break;
                                    }
                                case "Detection":
                                    {
                                        break;
                                    }
                                case "No. of Lines":
                                    {
                                        sMF_LOR = second.ToString();
                                        
                                        break;
                                    }
                                case "No. of Samples":
                                    {
                                        sMF_LOR = second.ToString();
                                        
                                        break;
                                    }
                                case "Window Type":
                                    {
                                        break;
                                    }
                                case "Input Range Mode":
                                    {
                                        break;
                                    }
                                case "Fixed Range Value":
                                    {
                                        break;
                                    }
                                case "Auto Mode":
                                    {
                                        break;
                                    }
                                case "Meas. Type":
                                    {
                                        break;
                                    }
                                case "Meas. Domain":
                                    {
                                        break;
                                    }
                                case "Trigger Hysteresis":
                                    {
                                        break;
                                    }
                                case "Trigger Pullup":
                                    {
                                        break;
                                    }
                                case "Bin Zeroing":
                                    {
                                        break;
                                    }
                                case "Units String":
                                    {
                                        break;
                                    }
                                case "Y-axis Units":
                                    {
                                        type = second;
                                        if (type == "Time")
                                        {
                                            xarray = new double[Convert.ToInt32(sMF_LOR)];
                                        }
                                        else
                                        {
                                            xarray = new double[Convert.ToInt32(sMF_LOR) + 1];
                                        }
                                       // xarray = new double[161];
                                        yarray = new double[xarray.Length];
                                        break;

                                    }
                                case "X-axis Units":
                                    {
                                        sMF_Xunit = second.ToString();
                                        break;
                                    }
                                case "View Signal":
                                    {
                                        //lblViewSignal.Text = second.ToString();
                                        break;
                                    }
                                case "Y-axis Display":
                                    {
                                        break;
                                    }
                                case "Sensor Type":
                                    {
                                        break;
                                    }
                                case "Overall":
                                    {
                                        sMF_Overall = second.ToString();
                                        break;
                                    }
                                case "X-Axis":
                                    {
                                        IsFoundValues = true;
                                        break;
                                    }
                                case "Rec Start Mode":
                                    {
                                        break;
                                    }
                                case "Rec Trig Level":
                                    {
                                        break;
                                    }
                                case "Acq. Errors":
                                    {
                                        break;
                                    }
                                case "Order":
                                    {
                                        break;
                                    }
                                case "RPM Stamp":
                                    {
                                        break;
                                    }
                                case "Time Stamp":
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        if (IsFoundValues)
                                        {
                                            if (sApp == "RuCd" || sApp == "FRF")
                                            {
                                                MessageBox.Show(sarrpath[sarrpath.Length - 1].ToString() + " is not a compatible file", "Incompatible File", MessageBoxButtons.OK);
                                                Incompatible = true;
                                            }
                                            else
                                            {
                                                if (totalValues < xarray.Length)
                                                {
                                                    xarray[totalValues] = Convert.ToDouble(first.ToString());
                                                    yarray[totalValues] = Convert.ToDouble(second.ToString());
                                                }
                                                
                                            }
                                            totalValues++;
                                        }
                                        break;
                                    }
                            }
                        }

                    }
                    if (Incompatible)
                    {
                        xarray = new double[0];
                        yarray = new double[0];
                        break;
                    }
                    if (chakstream == true)
                    {
                        break;
                    }
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            //return valuetoreturn;

        }
        private void Dowork(string selectedReport)
        {
            try
            {
                prtType = 0;
                selectedReport = selectedReport.TrimEnd(new char[] { ' ' });
                selectedReport = selectedReport.TrimStart(new char[] { ' ' });
                switch (selectedReport)
                {                    
                    case "Selected OTD/FDT/DRD/FFT Graph":
                        {
                            prtType = 4;
                            string[] ReportFilesPath = new string[1];
                            string[] ReportOverall = new string[1];
                            string[] ReportDate = new string[1];
                            string[] ReportXUnit = new string[1];
                            string[] ReportYUnit = new string[1];
                            string[] ReportResolution = new string[1];

                            ArrayList arlstXYData = new ArrayList();
                            arlstXYData.Add(Xdata);
                            arlstXYData.Add(Ydata);

                            ReportFilesPath[0] = sFilepath;
                            ReportOverall[0] = _SelectedCaption;
                            ReportDate[0] = " ";
                            ReportXUnit[0] = sXUnit;
                            ReportYUnit[0] = sYUnit;
                            ReportResolution[0] = " ";
                            //GenerateCSVReport(arlstXYData, _CurrentFilePath, _SelectedCaption, " ", _Xunit, _Yunit, " ");
                            GenerateFDTReport(arlstXYData, ReportFilesPath, ReportOverall, ReportDate, ReportXUnit, ReportYUnit, ReportResolution);
                            break;
                        }
                    case "Selected BA2/BAL Graph":
                        {
                            prtType = 4;
                            string[] ReportFilesPath = new string[1];
                            string[] ReportOverall = new string[1];
                            string[] ReportDate = new string[1];
                            string[] ReportXUnit = new string[1];
                            string[] ReportYUnit = new string[1];
                            string[] ReportResolution = new string[1];

                            ArrayList arlstXYData = new ArrayList();
                            arlstXYData.Add(Xdata);
                            arlstXYData.Add(Ydata);

                            ReportFilesPath[0] = sFilepath;
                            ReportOverall[0] = _SelectedCaption;
                            ReportDate[0] = " ";
                            ReportXUnit[0] = sXUnit;
                            ReportYUnit[0] = sYUnit;
                            ReportResolution[0] = " ";
                            GenerateBA2Report(arlstXYData, ReportFilesPath, ReportOverall, ReportDate, ReportXUnit, ReportYUnit, ReportResolution);


                            break;
                        }
                    case "Selected Files":
                        {
                            prtType = 4;

                            OpenFileDialog _FileDialog = new OpenFileDialog();
                            _FileDialog.Multiselect = true;
                            //_FileDialog.InitialDirectory = SelectedFolderPath;

                            _FileDialog.Filter = "CSV File (.csv)|*.csv";
                            _FileDialog.FilterIndex = 1;
                            _FileDialog.DefaultExt = ".csv";
                            
                            _FileDialog.ShowDialog();
                            string[] SelectedFilePath = _FileDialog.FileNames;

                            string[] ReportFilesPath = new string[0];
                            string[] ReportOverall = new string[0];
                            string[] ReportDate = new string[0];
                            string[] ReportXUnit = new string[0];
                            string[] ReportYUnit = new string[0];
                            string[] ReportResolution = new string[0];

                            ArrayList arlstXYData = new ArrayList();
                            for (int i = 0; i < SelectedFilePath.Length; i++)
                            {
                                ReadCSVfileForData(SelectedFilePath[i].ToString());
                                if (xarray.Length > 0)
                                {
                                    arlstXYData.Add(xarray);
                                    arlstXYData.Add(yarray);
                                    //Array.Resize(ref ReportFilesPath, ReportFilesPath.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref ReportFilesPath, 1);
                                    ReportFilesPath[ReportFilesPath.Length - 1] = SelectedFilePath[i].ToString();

                                    //Array.Resize(ref ReportOverall, ReportOverall.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref ReportOverall, 1);
                                    ReportOverall[ReportOverall.Length - 1] = sMF_Overall;

                                    //Array.Resize(ref ReportDate, ReportDate.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref ReportDate, 1);
                                    ReportDate[ReportDate.Length - 1] = sMF_Date;

                                    //Array.Resize(ref ReportXUnit, ReportXUnit.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref ReportXUnit, 1);
                                    ReportXUnit[ReportXUnit.Length - 1] = sMF_Xunit;

                                    //Array.Resize(ref ReportYUnit, ReportYUnit.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref ReportYUnit, 1);
                                    ReportYUnit[ReportYUnit.Length - 1] = sMF_Yunit;

                                    //Array.Resize(ref ReportResolution, ReportResolution.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref ReportResolution, 1);
                                    ReportResolution[ReportResolution.Length - 1] = sMF_LOR;

                                }
                            }
                            GenerateCSVReport(arlstXYData, ReportFilesPath, ReportOverall, ReportDate, ReportXUnit, ReportYUnit, ReportResolution);

                            break;
                        }
                    case "with User selected Parameters":
                        {
                            prtType = 1;
                            //SelectedReport _selectedReport = new SelectedReport();
                            //_selectedReport.ShowDialog();
                            //Hashtable SelectedHash = _selectedReport._HashTable;
                            GetReportParameters();
                            GenerateCSVReportwithAllParameters(_CSVData, _hashtable);
                            break;
                        }
                    case "With all parameter values":
                        {
                            prtType = 1;
                            GenerateCSVReportwithAllParameters(_CSVData, false);
                            break;
                        }
                    case "with all Parameters and graph":
                        {
                            prtType = 1;
                            GenerateCSVReportwithAllParameters(_CSVData, true);
                            break;
                        }
                    case "With Band Alarm":
                        {
                            prtType = 2;
                            GenerateCSVReportwithBandAlarms();
                            break;
                        }
                    case "With Fault Frequencies":
                        {
                            prtType = 2;
                            GenerateCSVReportWithFaultFreq();
                            break;
                        }
                    case "With RPM Values":
                        {
                            prtType = 2;
                            GenerateCSVReportWithRPM();
                            break;
                        }
                    case "Selected Wave":
                        {
                            prtType = 3;
                            GenerateWAVReportSelectedWave();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void GenerateBA2Report(ArrayList arlstXYData, string[] ReportFilesPath, string[] ReportOverall, string[] ReportDate, string[] ReportXUnit, string[] ReportYUnit, string[] ReportResolution)
        {
            try
            {
                _Report._MultiFileReport = new MultiFileReport();
                _Report._MultiFileReport.lblTitle.Text = "Report of Selected BA2 File Graph";
                for (int i = 0; i < ReportFilesPath.Length; i++)
                {
                    DataRow PointDataRow = _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.NewRow();
                    PointDataRow["Resolution"] = Convert.ToString(ReportResolution[i]);
                    PointDataRow["Overall"] = Convert.ToString(ReportOverall[i]);
                    PointDataRow["Date"] = Convert.ToString(ReportDate[i]);
                    PointDataRow["Path"] = Convert.ToString(ReportFilesPath[i]);
                    PointDataRow["Xunit"] = Convert.ToString(ReportXUnit[i]);
                    PointDataRow["Yunit"] = Convert.ToString(ReportYUnit[i]);
                    polar.PolarPlot _linegraph = new polar.PolarPlot();
                    // PolarPlot _lineGraph = new  PolarPlot();
                    _linegraph.DrawPolarPlotnoLine((double[])arlstXYData[2 * i], (double[])arlstXYData[(2 * i) + 1]);
                    ChartView _ChartView = _linegraph.chartVu; //GenerateReportGraph((double[])arlstXYData[2 * i], (double[])arlstXYData[(2 * i) + 1], (string)ReportXUnit[i], (string)ReportYUnit[i]);

                    if (_ChartView != null)
                    {
                        BufferedImage objImage = new BufferedImage(_ChartView);
                        Image GraphImage = (Image)objImage.GetBufferedImage();
                        byteImageData = ImageToByte(GraphImage);
                        PointDataRow["Graph"] = byteImageData;
                    }


                    //if (Graphbytes != null)
                    //{
                    //    PointDataRow["Graph"] = Graphbytes;
                    //}
                    _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.Rows.Add(PointDataRow);
                    _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.AcceptChanges();
                    _Report._printcontrol1.PrintingSystem = _Report._MultiFileReport.PrintingSystem;
                    _Report._MultiFileReport.CreateDocument();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void GenerateFDTReport(ArrayList arlstXYData, string[] ReportFilesPath, string[] ReportOverall, string[] ReportDate, string[] ReportXUnit, string[] ReportYUnit, string[] ReportResolution)
        {
            try
            {
                _Report._MultiFileReport = new MultiFileReport();
                _Report._MultiFileReport.lblTitle.Text = "Report of Selected FDT/OTD/DRD/FFT File Graph";
                for (int i = 0; i < ReportFilesPath.Length; i++)
                {
                    DataRow PointDataRow = _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.NewRow();
                    PointDataRow["Resolution"] = ReportResolution[i].ToString();
                    PointDataRow["Overall"] = ReportOverall[i].ToString();
                    PointDataRow["Date"] = ReportDate[i].ToString();
                    PointDataRow["Path"] = ReportFilesPath[i].ToString();
                    PointDataRow["Xunit"] = ReportXUnit[i].ToString();
                    PointDataRow["Yunit"] = ReportYUnit[i].ToString();
                    // byte[] Graphbytes = GenerateandGetGraph((double[])arlstXYData[2*i],(double[])arlstXYData[(2*i)+1]);//GenerateandGetFaultFreqGraph();
                    ChartView _ChartView = GenerateReportGraph((double[])arlstXYData[2 * i], (double[])arlstXYData[(2 * i) + 1], (string)ReportXUnit[i], (string)ReportYUnit[i]);

                    if (_ChartView != null)
                    {
                        BufferedImage objImage = new BufferedImage(_ChartView);
                        Image GraphImage = (Image)objImage.GetBufferedImage();
                        byteImageData = ImageToByte(GraphImage);
                        PointDataRow["Graph"] = byteImageData;
                    }
                    //if (Graphbytes != null)
                    //{
                    //    PointDataRow["Graph"] = Graphbytes;
                    //}
                    _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.Rows.Add(PointDataRow);
                    _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        

        private void GenerateCSVReport(ArrayList arlstXYData, string[] ReportFilesPath, string[] ReportOverall, string[] ReportDate, string[] ReportXUnit, string[] ReportYUnit, string[] ReportResolution)
        {
            try
            {
                _Report._MultiFileReport=new MultiFileReport();
                _Report._MultiFileReport.lblTitle.Text="Report of Selected CSV Files";
                for (int i = 0; i < ReportFilesPath.Length; i++)
                {
                    DataRow PointDataRow = _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.NewRow();
                    PointDataRow["Resolution"] = ReportResolution[i].ToString();
                    PointDataRow["Overall"] = ReportOverall[i].ToString();
                    PointDataRow["Date"] = ReportDate[i].ToString();
                    PointDataRow["Path"] = ReportFilesPath[i].ToString();
                    PointDataRow["Xunit"] = ReportXUnit[i].ToString();
                    PointDataRow["Yunit"] = ReportYUnit[i].ToString();
                   // byte[] Graphbytes = GenerateandGetGraph((double[])arlstXYData[2*i],(double[])arlstXYData[(2*i)+1]);//GenerateandGetFaultFreqGraph();
                    ChartView _ChartView = GenerateReportGraph((double[])arlstXYData[2 * i], (double[])arlstXYData[(2 * i) + 1], (string)ReportXUnit[i], (string)ReportYUnit[i]);

                    if (_ChartView != null)
                    {
                        BufferedImage objImage = new BufferedImage(_ChartView);

                        Image GraphImage = (Image)objImage.GetBufferedImage();
                        byteImageData = ImageToByte(GraphImage);
                        PointDataRow["Graph"] = byteImageData;
                    }
                    //if (Graphbytes != null)
                    //{
                    //    PointDataRow["Graph"] = Graphbytes;
                    //}
                    _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.Rows.Add(PointDataRow);
                    _Report._MultiFileReport.multiFileDataSet1.MultiFileTable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private ChartView GenerateReportGraph(double[] x, double[] y, string Xunit, string Yunit)
        {
            ChartView _chartview = null;
            try
            {
                polar.LineGraphControl _lineGraph = new polar.LineGraphControl();
                _chartview = _lineGraph.DrawReportGraph(x, y, ColorCode, Xunit, Yunit);
            }
            catch (Exception ex)
            {
            }
            return _chartview;
        }

     
        
        string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
         
        private byte[] GenerateandGetGraph(double[] x, double[] y)
        {
            byte[] GraphBytes = null;
            try
            {
                DrawReportImage(x,y);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
                {
                    GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return GraphBytes;
        }

        

        private void GetReportParameters()
        {
            string[] sarrLabel ={"Date","Trigger_Type","Trigger_Level","Slope","Transducer_Units","Sensitivity",
                    "Transducer_offset","Display_Units","High_Pass_Filter","Coupling_Type","Channel_Input",
                "Freq_Type","Orders","No._of_Averages","Average_Type","Overlap","Detection","No._of_Lines",
                "Window_Type","Input_Range_Mode","Fixed_Range_Value","Auto_Mode","Meas._Type","Meas._Domain","Trigger_Hysperesis",
                "Trigger_Pullup","Bin_Zeroing","Units_String","Y-axis_Units","X-axis_Units","View_Signal", "Y-axis_Display", 
                "Sensor_Type","Overall", "Show_Graph"};
            try
            {
                _hashtable = new Hashtable();
                if (File.Exists(sErrorLogPath + "\\RS.XML"))
                {
                    XmlDocument m_xdDocument = new XmlDocument();
                    m_xdDocument.Load(sErrorLogPath + "\\RS.XML");
                    string sXPath = "//Report/Parameters";
                    //XmlNodeList xnlValueNodes = m_xdDocument.SelectNodes(sXPath);
                    XmlNode xnFile = m_xdDocument.SelectSingleNode("//Report/Parameters");
                    XmlNodeList xnlValueNodes = xnFile.SelectNodes("Values");
                    int i = 0;

                    foreach (XmlNode xnValueNode in xnlValueNodes)
                    {

                        bool chkbxValue = (Convert.ToBoolean(xnValueNode.Attributes[sarrLabel[i].ToString()].Value));
                        string sChkbx = sarrLabel[i].ToString().Replace('_', ' ');
                        _hashtable.Add(sChkbx, chkbxValue);
                        i++;

                    }
                }
                else
                {
                    _hashtable.Add("Date", true);
                    _hashtable.Add("Trigger Type", true);
                    _hashtable.Add("Trigger Level", true);
                    _hashtable.Add("Slope", true);
                    _hashtable.Add("Transducer Units", true);
                    _hashtable.Add("Sensitivity", true);
                    _hashtable.Add("Transducer offset", true);
                    _hashtable.Add("Display Units", true);
                    _hashtable.Add("High Pass Filter", true);
                    _hashtable.Add("Coupling Type", true);
                    _hashtable.Add("Channel Input", true);
                    _hashtable.Add("Freq Type", true);
                    _hashtable.Add("Orders", true);
                    _hashtable.Add("No. of Averages", true);
                    _hashtable.Add("Average Type", true);
                    _hashtable.Add("Overlap", true);
                    _hashtable.Add("Detection", true);
                    _hashtable.Add("No. of Lines", true);
                    _hashtable.Add("Window Type", true);
                    _hashtable.Add("Input Range Mode", true);
                    _hashtable.Add("Fixed Range Value", true);
                    _hashtable.Add("Auto Mode", true);
                    _hashtable.Add("Meas. Type", true);
                    _hashtable.Add("Meas. Domain", true);
                    _hashtable.Add("Trigger Hysperesis", true);
                    _hashtable.Add("Trigger Pullup", true);
                    _hashtable.Add("Bin Zeroing", true);
                    _hashtable.Add("Units String", true);
                    _hashtable.Add("Y-axis Units", true);
                    _hashtable.Add("X-axis Units", true);
                    _hashtable.Add("View Signal", true);
                    _hashtable.Add("Y-axis Display", true);
                    _hashtable.Add("Sensor Type", true);
                    _hashtable.Add("Overall", true);//"Show_Graph"
                    _hashtable.Add("Show Graph", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GenerateWAVReportSelectedWave()
        {
            byte[] GraphBytes = null;
            int ictr = 0;
            try
            {
                _Report._WAVESelectedReport = new WAVESelectedReport();
                _Report._WAVESelectedReport.lblTitle.Text = "Report of WAV data file";
                if (_DATA.Count > 0)
                {
                    for (int i = 0; i < _DATA.Count; i++)
                    {

                        DataRow PointDataRow = _Report._WAVESelectedReport.waveSelectedDataset1.WAVDatatable.NewRow();

                        PointDataRow["SamplingFreq"] = _WAVDataValues[0].ToString();
                        PointDataRow["FrequencyRange"] = _WAVDataValues[1].ToString();
                        PointDataRow["LineOfResolution"] = _WAVDataValues[2].ToString();
                        PointDataRow["ExectTime"] = _WAVDataValues[3].ToString();
                        PointDataRow["Channel"] = _WAVDataGridValue[ictr];
                        ictr++;
                        Xdata = (double[])_DATA[i];
                        Ydata = (double[])_DATA[i + 1];
                        i++;

                        ChartView _chartview = GenerateReportGraph(Xdata, Ydata, sXUnit, sYUnit);
                        if (_chartview != null)
                        {
                            BufferedImage objImage = new BufferedImage(_chartview);

                            Image GraphImage = (Image)objImage.GetBufferedImage();
                            byteImageData = ImageToByte(GraphImage);
                            PointDataRow["TimeGraph"] = byteImageData;
                        }
                        //DrawReportImage("Selected Wave");
                        //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
                        //{
                        //    GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });

                        //    PointDataRow["TimeGraph"] = GraphBytes;

                        //}
                        //if (CheckForTimeData(Ydata))
                        {
                            double[] Fmag = new double[0];
                            try
                            {
                                double[] mag = fftMag(Ydata);

                                double lastTimevalue = (double)(Xdata[Xdata.Length - 1]);
                                lastTimevalue = Math.Round(lastTimevalue, 2);
                                double HzRate = (double)(1 / lastTimevalue);
                                double[] Hz = new double[0];
                                double ilastFreq = Convert.ToDouble(_WAVDataValues[1].ToString());
                                for (int i1 = 0; i1 < mag.Length; i1++)
                                {
                                    //Array.Resize(ref Hz, Hz.Length + 1);
                                    _ResizeArray.IncreaseArrayDouble(ref Hz, 1);
                                    Hz[i1] = HzRate * i1;
                                    if (Hz[i1] >= ilastFreq)
                                    {
                                        break;
                                    }
                                }

                                Xdata = Hz;
                                Ydata = mag;

                                _chartview = GenerateReportGraph(Xdata, Ydata, "Hz", sYUnit);
                                if (_chartview != null)
                                {
                                    BufferedImage objImage = new BufferedImage(_chartview);

                                    Image GraphImage = (Image)objImage.GetBufferedImage();
                                    byteImageData = ImageToByte(GraphImage);
                                    PointDataRow["FFTGraph"] = byteImageData;
                                }

                                //DrawReportImage("Selected Wave");
                                //GraphBytes = null;
                                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
                                //{
                                //    GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });

                                //    PointDataRow["FFTGraph"] = GraphBytes;

                                //}
                            }
                            catch (Exception ex)
                            {
                                ErrorLog_Class.ErrorLogEntry(ex);
                            }


                        }
                        _Report._WAVESelectedReport.waveSelectedDataset1.WAVDatatable.Rows.Add(PointDataRow);
                        _Report._WAVESelectedReport.waveSelectedDataset1.WAVDatatable.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void GenerateCSVReportWithFaultFreq()
        {
            HighAlValue = null;
            LowAlValue = null;
            Freq = null;
            prevFreq = null;
            displayed = null;
            HighestPeakinArea = 0;
            HighestPeakinAreaAt = 0;
            TempPeakinArea = 0;

            BndAlrmsHigh = null;
            BndAlrmsLow = null;
            BndAlrmsFreq = null;
            BandValuetoDisplay = new ArrayList();
            try
            {
                _Report._CSVBandReport = new CSVBandReport();
                _Report._CSVBandReport._ToShow = "Fault";
                //Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                _Report._CSVBandReport.lblTitle.Text = "Report with Fault Frequency";
                if (!CheckForTimeData(Ydata))
                {
                    GetFaultFreq();
                    for (int i = 0; i < BndAlrmsFreq.Length; i++)
                    {
                        DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.NewRow();
                        string[] bandValues = BandValuetoDisplay[i].ToString().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        PointDataRow["BFreq"] = BndAlrmsFreq[i].ToString();
                        //PointDataRow["BHValue"] = bandValues[0].ToString();
                        //PointDataRow["BLValue"] = BndAlrmsLow[i].ToString();
                        PointDataRow["XData"] = bandValues[0].ToString() + " " + sXUnit.ToString();
                        PointDataRow["YData"] = bandValues[1].ToString() + " " + sYUnit.ToString();
                        _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.Rows.Add(PointDataRow);
                        _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.AcceptChanges();

                    }
                    //Amit Jain    DA_35	RPM not display correctly and fault frequency also in Report	code related	minor 	8-4-2010

                    if (BndAlrmsFreq.Length > 0)
                    {
                        ChartView _chartview = GenerateReportGraph("FaultFreq");
                        if (_chartview != null)
                        {
                            BufferedImage objImage = new BufferedImage(_chartview);

                            Image GraphImage = (Image)objImage.GetBufferedImage();
                            byteImageData = ImageToByte(GraphImage);
                            DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandGraph.NewRow();
                            PointDataRow["Graph"] = byteImageData;
                            _Report._CSVBandReport.csvBandDataset1.BandGraph.Rows.Add(PointDataRow);
                            _Report._CSVBandReport.csvBandDataset1.BandGraph.AcceptChanges();
                        }

                       // byte[] Graphbytes = GenerateandGetGraph("FaultFreq");//GenerateandGetFaultFreqGraph();
                        //if (Graphbytes != null)
                        //{
                        //    DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandGraph.NewRow();
                        //    PointDataRow["Graph"] = Graphbytes;
                        //    _Report._CSVBandReport.csvBandDataset1.BandGraph.Rows.Add(PointDataRow);
                        //    _Report._CSVBandReport.csvBandDataset1.BandGraph.AcceptChanges();
                        //}
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void GenerateCSVReportWithRPM()
        {
            HighAlValue = null;
            LowAlValue = null;
            Freq = null;
            prevFreq = null;
            displayed = null;
            HighestPeakinArea = 0;
            HighestPeakinAreaAt = 0;
            TempPeakinArea = 0;

            BndAlrmsHigh = null;
            BndAlrmsLow = null;
            BndAlrmsFreq = null;
            BandValuetoDisplay = new ArrayList();
            try
            {
                _Report._CSVBandReport = new CSVBandReport();
                _Report._CSVBandReport._ToShow = "RPM";
                //Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                _Report._CSVBandReport.lblTitle.Text = "Report with RPM data";
                if (!CheckForTimeData(Ydata))
                {
                    GetRPM();
                    for (int i = 0; i < BandValuetoDisplay.Count; i++)
                    {
                        DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.NewRow();
                        string[] bandValues = BandValuetoDisplay[i].ToString().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        PointDataRow["BFreq"] = (i + 1).ToString() + " X RPM";
                        PointDataRow["XData"] = bandValues[0].ToString() + " " + sXUnit.ToString();
                        PointDataRow["YData"] = bandValues[1].ToString() + " " + sYUnit.ToString();
                        _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.Rows.Add(PointDataRow);
                        _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.AcceptChanges();

                    }
                    //Amit Jain    DA_35	RPM not display correctly and fault frequency also in Report	code related	minor 	8-4-2010

                    if (BandValuetoDisplay.Count > 0)
                    {
                        ChartView _chartview = GenerateReportGraph("RPM");
                        if (_chartview != null)
                        {
                            BufferedImage objImage = new BufferedImage(_chartview);

                            Image GraphImage = (Image)objImage.GetBufferedImage();
                            byteImageData = ImageToByte(GraphImage);
                            DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandGraph.NewRow();
                            PointDataRow["Graph"] = byteImageData;
                            _Report._CSVBandReport.csvBandDataset1.BandGraph.Rows.Add(PointDataRow);
                            _Report._CSVBandReport.csvBandDataset1.BandGraph.AcceptChanges();
                        }


                        //byte[] Graphbytes = GenerateandGetGraph("RPM");// GenerateandGetRPMGraph();
                        //if (Graphbytes != null)
                        //{
                        //    DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandGraph.NewRow();
                        //    PointDataRow["Graph"] = Graphbytes;
                        //    _Report._CSVBandReport.csvBandDataset1.BandGraph.Rows.Add(PointDataRow);
                        //    _Report._CSVBandReport.csvBandDataset1.BandGraph.AcceptChanges();
                        //}
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void GenerateCSVReportwithBandAlarms()
        {

            HighAlValue = null;
            LowAlValue = null;
            Freq = null;
            prevFreq = null;
            displayed = null;
            HighestPeakinArea = 0;
            HighestPeakinAreaAt = 0;
            TempPeakinArea = 0;

            BndAlrmsHigh = null;
            BndAlrmsLow = null;
            BndAlrmsFreq = null;
            BandValuetoDisplay = new ArrayList();
            try
            {
                _Report._CSVBandReport = new CSVBandReport();
                //Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                _Report._CSVBandReport.lblTitle.Text = "Report With Band Alarms";
                if (!CheckForTimeData(Ydata))
                {
                    GetBandData();
                    for (int i = 0; i < BndAlrmsFreq.Length; i++)
                    {
                        DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.NewRow();
                        string[] bandValues = BandValuetoDisplay[i].ToString().Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                        PointDataRow["BFreq"] = BndAlrmsFreq[i].ToString();
                        PointDataRow["BHValue"] = BndAlrmsHigh[i].ToString();
                        PointDataRow["BLValue"] = BndAlrmsLow[i].ToString();
                        if (bandValues[0].ToString() == "0")
                        {
                            PointDataRow["XData"] = "---";
                            PointDataRow["YData"] = "---";
                        }
                        else
                        {
                            PointDataRow["XData"] = bandValues[0].ToString() + " " + sXUnit.ToString();
                            PointDataRow["YData"] = bandValues[1].ToString() + " " + sYUnit.ToString();
                        }
                        _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.Rows.Add(PointDataRow);
                        _Report._CSVBandReport.csvBandDataset1.BandAlarmDataSet.AcceptChanges();

                    }
                    if (BndAlrmsFreq.Length > 0)
                    {
                        ChartView _chartview = GenerateReportGraph("Band");
                        if (_chartview != null)
                        {
                            BufferedImage objImage = new BufferedImage(_chartview);

                            Image GraphImage = (Image)objImage.GetBufferedImage();
                            byteImageData = ImageToByte(GraphImage);
                            DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandGraph.NewRow();
                            PointDataRow["Graph"] = byteImageData;
                            _Report._CSVBandReport.csvBandDataset1.BandGraph.Rows.Add(PointDataRow);
                            _Report._CSVBandReport.csvBandDataset1.BandGraph.AcceptChanges();
                        }


                        //byte[] Graphbytes = GenerateandGetGraph("Band");// GenerateandGetBandGraph();
                        //if (Graphbytes != null)
                        //{
                        //    DataRow PointDataRow = _Report._CSVBandReport.csvBandDataset1.BandGraph.NewRow();
                        //    PointDataRow["Graph"] = Graphbytes;
                        //    _Report._CSVBandReport.csvBandDataset1.BandGraph.Rows.Add(PointDataRow);
                        //    _Report._CSVBandReport.csvBandDataset1.BandGraph.AcceptChanges();
                        //}
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        private void GenerateCSVReportwithAllParameters(string CSVData, Hashtable _Hash)
        {
            try
            {
                _Report._CSVreport1 = new CSVReport1();
                //Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //if (ShowGraph)
                {
                    _Report._CSVreport1.lblTitle.Text = "Report with User selected Parameters";
                }
                DataRow PointDataRow = _Report._CSVreport1.csVdataset1.CSVdataTable.NewRow();
                string[] splittedParameter = CSVData.Split(new string[] { " $ " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splittedParameter.Length; i++)
                {
                    string[] ActualParameters = splittedParameter[i].ToString().Split(new string[] { " % " }, StringSplitOptions.RemoveEmptyEntries);
                    if (ActualParameters.Length > 1)
                        switch (ActualParameters[0].ToString())
                        {
                            case "Application":
                                {
                                    break;
                                }
                            case "Path":
                                {
                                    PointDataRow["ColPath"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Unit ID":
                                {
                                    break;
                                }
                            case "Date":
                                {
                                    if ((bool)_Hash["Date"])
                                    {
                                        PointDataRow["ColDate"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }

                            case "Trigger Type":
                                {
                                    if ((bool)_Hash["Trigger Type"])
                                    {
                                        PointDataRow["ColTriggerType"] = ActualParameters[1].ToString();

                                    }
                                    break;
                                }
                            case "Trigger Level":
                                {
                                    if ((bool)_Hash["Trigger Level"])
                                    {
                                        PointDataRow["ColTriggerLevel"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Slope":
                                {
                                    if ((bool)_Hash["Slope"])
                                    {
                                        PointDataRow["ColSlope"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Transducer Units":
                                {
                                    if ((bool)_Hash["Transducer Units"])
                                    {
                                        PointDataRow["ColTransUnit"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Sensitivity":
                                {
                                    if ((bool)_Hash["Sensitivity"])
                                    {
                                        PointDataRow["ColSensitivity"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Transducer Offset":
                                {
                                    if ((bool)_Hash["Transducer offset"])
                                    {
                                        PointDataRow["ColTransducerOffset"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Display Units":
                                {
                                    if ((bool)_Hash["Display Units"])
                                    {
                                        PointDataRow["ColDisplayUnits"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "High Pass Filter":
                                {
                                    if ((bool)_Hash["High Pass Filter"])
                                    {
                                        PointDataRow["ColHPF"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Coupling Type":
                                {
                                    if ((bool)_Hash["Coupling Type"])
                                    {
                                        PointDataRow["ColCouplingType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Channel Input":
                                {
                                    if ((bool)_Hash["Channel Input"])
                                    {
                                        PointDataRow["ColChannelInput"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Freq Type":
                                {
                                    if ((bool)_Hash["Freq Type"])
                                    {
                                        PointDataRow["ColFreqType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Orders":
                                {
                                    if ((bool)_Hash["Orders"])
                                    {
                                        PointDataRow["ColOrder"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "No. of Averages":
                                {
                                    if ((bool)_Hash["No. of Averages"])
                                    {
                                        PointDataRow["ColNoofAvg"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Average Type":
                                {
                                    if ((bool)_Hash["Average Type"])
                                    {
                                        PointDataRow["ColAverageType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Overlap":
                                {
                                    if ((bool)_Hash["Overlap"])
                                    {
                                        PointDataRow["ColOverlap"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Detection":
                                {
                                    if ((bool)_Hash["Detection"])
                                    {
                                        PointDataRow["ColDetection"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "No. of Lines":
                                {
                                    if ((bool)_Hash["No. of Lines"])
                                    {
                                        PointDataRow["ColNooflines"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "No. of Samples":
                                {
                                    if ((bool)_Hash["No. of Lines"])
                                    {
                                        PointDataRow["ColNooflines"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Window Type":
                                {
                                    if ((bool)_Hash["Window Type"])
                                    {
                                        PointDataRow["ColWindowType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Input Range Mode":
                                {
                                    if ((bool)_Hash["Input Range Mode"])
                                    {
                                        PointDataRow["ColInputRangeMode"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Fixed Range Value":
                                {
                                    if ((bool)_Hash["Fixed Range Value"])
                                    {
                                        PointDataRow["ColFixedRangeValue"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Auto Mode":
                                {
                                    if ((bool)_Hash["Auto Mode"])
                                    {
                                        PointDataRow["ColAutoMode"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Meas. Type":
                                {
                                    if ((bool)_Hash["Meas. Type"])
                                    {
                                        PointDataRow["ColMeasType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Meas. Domain":
                                {
                                    if ((bool)_Hash["Meas. Domain"])
                                    {
                                        PointDataRow["ColMeasDomain"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Trigger Hysteresis":
                                {
                                    if ((bool)_Hash["Trigger Hysperesis"])
                                    {
                                        PointDataRow["ColTriggerHysteresis"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Trigger Pullup":
                                {
                                    if ((bool)_Hash["Trigger Pullup"])
                                    {
                                        PointDataRow["ColTriggerPullup"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Bin Zeroing":
                                {
                                    if ((bool)_Hash["Bin Zeroing"])
                                    {
                                        PointDataRow["ColBinZeroing"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Units String":
                                {
                                    if ((bool)_Hash["Units String"])
                                    {
                                        PointDataRow["ColUnitsString"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Y-axis Units":
                                {
                                    if ((bool)_Hash["Y-axis Units"])
                                    {
                                        PointDataRow["ColYaxisUnit"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "X-axis Units":
                                {
                                    if ((bool)_Hash["X-axis Units"])
                                    {
                                        PointDataRow["ColXaxisUnit"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "View Signal":
                                {
                                    if ((bool)_Hash["View Signal"])
                                    {
                                        PointDataRow["ColViewSignal"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Y-axis Display":
                                {
                                    if ((bool)_Hash["Y-axis Display"])
                                    {
                                        PointDataRow["ColYaxisDisplay"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Sensor Type":
                                {
                                    if ((bool)_Hash["Sensor Type"])
                                    {
                                        PointDataRow["ColSensorType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Overall":
                                {
                                    if ((bool)_Hash["Overall"])
                                    {
                                        PointDataRow["ColOverall"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "X-Axis":
                                {

                                    break;
                                }
                            case "Rec Start Mode":
                                {

                                    break;
                                }
                            case "Rec Trig Level":
                                {

                                    break;
                                }
                            case "Acq. Errors":
                                {

                                    break;
                                }

                            case "RPM Stamp":
                                {

                                    break;
                                }
                            case "Time Stamp":
                                {

                                    break;
                                }
                        }


                }
                if ((bool)_Hash["Show Graph"])
                {
                    ChartView _chartview = GenerateReportGraph(Xdata, Ydata, sXUnit, sYUnit);
                    if (_chartview != null)
                    {
                        BufferedImage objImage = new BufferedImage(_chartview);

                        Image GraphImage = (Image)objImage.GetBufferedImage();
                        byteImageData = ImageToByte(GraphImage);
                        PointDataRow["ColOrigGraph"] = byteImageData;
                    }

                    ////Amit Jain    DA_32	graph not display properly in report( with all parameters and graph)	code related	minor 	8-4-2010
                    //byte[] Graphbytes = GenerateandGetGraph(null);//GenerateandGetFaultFreqGraph();
                    //if (Graphbytes != null)
                    //{
                    //    PointDataRow["ColOrigGraph"] = Graphbytes;
                    //}
                    ////bool bFileExists = File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportClip.jpg");
                    ////if (bFileExists != false)
                    ////{
                    ////    byteImageData = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
                    ////    PointDataRow["ColOrigGraph"] = byteImageData;
                    //}
                }
                _Report._CSVreport1.csVdataset1.CSVdataTable.Rows.Add(PointDataRow);
                _Report._CSVreport1.csVdataset1.CSVdataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private void GenerateCSVReportwithAllParameters(string CSVData, bool ShowGraph, Hashtable _Hash)
        {
            try
            {
                _Report._CSVreport1 = new CSVReport1();
                //Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //if (ShowGraph)
                {
                    _Report._CSVreport1.lblTitle.Text = "Report with User selected Parameters";
                }
                DataRow PointDataRow = _Report._CSVreport1.csVdataset1.CSVdataTable.NewRow();
                string[] splittedParameter = CSVData.Split(new string[] { " $ " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splittedParameter.Length; i++)
                {
                    string[] ActualParameters = splittedParameter[i].ToString().Split(new string[] { " % " }, StringSplitOptions.RemoveEmptyEntries);
                    if (ActualParameters.Length > 1)
                        switch (ActualParameters[0].ToString())
                        {
                            case "Application":
                                {
                                    break;
                                }
                            case "Path":
                                {
                                    PointDataRow["ColPath"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Unit ID":
                                {
                                    break;
                                }
                            case "Date":
                                {
                                    if ((bool)_Hash["Date"])
                                    {
                                        PointDataRow["ColDate"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }

                            case "Trigger Type":
                                {
                                    if ((bool)_Hash["Trigger Type"])
                                    {
                                        PointDataRow["ColTriggerType"] = ActualParameters[1].ToString();

                                    }
                                    break;
                                }
                            case "Trigger Level":
                                {
                                    if ((bool)_Hash["Trigger Level"])
                                    {
                                        PointDataRow["ColTriggerLevel"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Slope":
                                {
                                    if ((bool)_Hash["Slope"])
                                    {
                                        PointDataRow["ColSlope"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Transducer Units":
                                {
                                    if ((bool)_Hash["Transducer Units"])
                                    {
                                        PointDataRow["ColTransUnit"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Sensitivity":
                                {
                                    if ((bool)_Hash["Sensitivity"])
                                    {
                                        PointDataRow["ColSensitivity"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Transducer Offset":
                                {
                                    if ((bool)_Hash["Transducer offset"])
                                    {
                                        PointDataRow["ColTransducerOffset"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Display Units":
                                {
                                    if ((bool)_Hash["Display Units"])
                                    {
                                        PointDataRow["ColDisplayUnits"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "High Pass Filter":
                                {
                                    if ((bool)_Hash["High Pass Filter"])
                                    {
                                        PointDataRow["ColHPF"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Coupling Type":
                                {
                                    if ((bool)_Hash["Coupling Type"])
                                    {
                                        PointDataRow["ColCouplingType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Channel Input":
                                {
                                    if ((bool)_Hash["Channel Input"])
                                    {
                                        PointDataRow["ColChannelInput"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Freq Type":
                                {
                                    if ((bool)_Hash["Freq Type"])
                                    {
                                        PointDataRow["ColFreqType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Orders":
                                {
                                    if ((bool)_Hash["Orders"])
                                    {
                                        PointDataRow["ColOrder"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "No. of Averages":
                                {
                                    if ((bool)_Hash["No. of Averages"])
                                    {
                                        PointDataRow["ColNoofAvg"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Average Type":
                                {
                                    if ((bool)_Hash["Average Type"])
                                    {
                                        PointDataRow["ColAverageType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Overlap":
                                {
                                    if ((bool)_Hash["Overlap"])
                                    {
                                        PointDataRow["ColOverlap"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Detection":
                                {
                                    if ((bool)_Hash["Detection"])
                                    {
                                        PointDataRow["ColDetection"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "No. of Lines":
                                {
                                    if ((bool)_Hash["No. of Lines"])
                                    {
                                        PointDataRow["ColNooflines"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "No. of Samples":
                                {
                                    if ((bool)_Hash["No. of Lines"])
                                    {
                                        PointDataRow["ColNooflines"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Window Type":
                                {
                                    if ((bool)_Hash["Window Type"])
                                    {
                                        PointDataRow["ColWindowType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Input Range Mode":
                                {
                                    if ((bool)_Hash["Input Range Mode"])
                                    {
                                        PointDataRow["ColInputRangeMode"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Fixed Range Value":
                                {
                                    if ((bool)_Hash["Fixed Range Value"])
                                    {
                                        PointDataRow["ColFixedRangeValue"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Auto Mode":
                                {
                                    if ((bool)_Hash["Auto Mode"])
                                    {
                                        PointDataRow["ColAutoMode"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Meas. Type":
                                {
                                    if ((bool)_Hash["Meas. Type"])
                                    {
                                        PointDataRow["ColMeasType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Meas. Domain":
                                {
                                    if ((bool)_Hash["Meas. Domain"])
                                    {
                                        PointDataRow["ColMeasDomain"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Trigger Hysteresis":
                                {
                                    if ((bool)_Hash["Trigger Hysperesis"])
                                    {
                                        PointDataRow["ColTriggerHysteresis"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Trigger Pullup":
                                {
                                    if ((bool)_Hash["Trigger Pullup"])
                                    {
                                        PointDataRow["ColTriggerPullup"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Bin Zeroing":
                                {
                                    if ((bool)_Hash["Bin Zeroing"])
                                    {
                                        PointDataRow["ColBinZeroing"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Units String":
                                {
                                    if ((bool)_Hash["Units String"])
                                    {
                                        PointDataRow["ColUnitsString"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Y-axis Units":
                                {
                                    if ((bool)_Hash["Y-axis Units"])
                                    {
                                        PointDataRow["ColYaxisUnit"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "X-axis Units":
                                {
                                    if ((bool)_Hash["X-axis Units"])
                                    {
                                        PointDataRow["ColXaxisUnit"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "View Signal":
                                {
                                    if ((bool)_Hash["View Signal"])
                                    {
                                        PointDataRow["ColViewSignal"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Y-axis Display":
                                {
                                    if ((bool)_Hash["Y-axis Display"])
                                    {
                                        PointDataRow["ColYaxisDisplay"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Sensor Type":
                                {
                                    if ((bool)_Hash["Sensor Type"])
                                    {
                                        PointDataRow["ColSensorType"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "Overall":
                                {
                                    if ((bool)_Hash["Overall"])
                                    {
                                        PointDataRow["ColOverall"] = ActualParameters[1].ToString();
                                    }
                                    break;
                                }
                            case "X-Axis":
                                {

                                    break;
                                }
                            case "Rec Start Mode":
                                {

                                    break;
                                }
                            case "Rec Trig Level":
                                {

                                    break;
                                }
                            case "Acq. Errors":
                                {

                                    break;
                                }

                            case "RPM Stamp":
                                {

                                    break;
                                }
                            case "Time Stamp":
                                {

                                    break;
                                }
                        }


                }
                if (ShowGraph)
                {
                    //Amit Jain    DA_32	graph not display properly in report( with all parameters and graph)	code related	minor 	8-4-2010
                    byte[] Graphbytes = GenerateandGetGraph(null);//GenerateandGetFaultFreqGraph();
                    if (Graphbytes != null)
                    {
                        PointDataRow["ColOrigGraph"] = Graphbytes;
                    }
                    //bool bFileExists = File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportClip.jpg");
                    //if (bFileExists != false)
                    //{
                    //    byteImageData = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
                    //    PointDataRow["ColOrigGraph"] = byteImageData;
                    //}
                }
                _Report._CSVreport1.csVdataset1.CSVdataTable.Rows.Add(PointDataRow);
                _Report._CSVreport1.csVdataset1.CSVdataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        private void GenerateCSVReportwithAllParameters(string CSVData, bool ShowGraph)
        {
            try
            {
                _Report._CSVreport1 = new CSVReport1();
                //Amit Jain    DA_34	click on with rpm values report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                //Amit Jain    DA_33	click on with fault frquency report ,report show text (report with band alarm data) corrcet it .	code related	minor 	8-4-2010
                if (ShowGraph)
                {
                    _Report._CSVreport1.lblTitle.Text = "Report with All Parameters With Graph";
                }
                else
                {
                    _Report._CSVreport1.lblTitle.Text = "Report with All Parameters";
                }
                DataRow PointDataRow = _Report._CSVreport1.csVdataset1.CSVdataTable.NewRow();
                string[] splittedParameter = CSVData.Split(new string[] { " $ " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splittedParameter.Length; i++)
                {
                    string[] ActualParameters = splittedParameter[i].ToString().Split(new string[] { " % " }, StringSplitOptions.RemoveEmptyEntries);
                    if (ActualParameters.Length > 1)
                        switch (ActualParameters[0].ToString())
                        {
                            case "Application":
                                {
                                    break;
                                }
                            case "Path":
                                {
                                    PointDataRow["ColPath"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Unit ID":
                                {
                                    break;
                                }
                            case "Date":
                                {
                                    PointDataRow["ColDate"] = ActualParameters[1].ToString();
                                    break;
                                }

                            case "Trigger Type":
                                {
                                    PointDataRow["ColTriggerType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Trigger Level":
                                {
                                    PointDataRow["ColTriggerLevel"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Slope":
                                {
                                    PointDataRow["ColSlope"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Transducer Units":
                                {
                                    PointDataRow["ColTransUnit"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Sensitivity":
                                {
                                    PointDataRow["ColSensitivity"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Transducer Offset":
                                {
                                    PointDataRow["ColTransducerOffset"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Display Units":
                                {
                                    PointDataRow["ColDisplayUnits"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "High Pass Filter":
                                {
                                    PointDataRow["ColHPF"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Coupling Type":
                                {
                                    PointDataRow["ColCouplingType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Channel Input":
                                {
                                    PointDataRow["ColChannelInput"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Freq Type":
                                {
                                    PointDataRow["ColFreqType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Orders":
                                {
                                    PointDataRow["ColOrder"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "No. of Averages":
                                {
                                    PointDataRow["ColNoofAvg"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Average Type":
                                {
                                    PointDataRow["ColAverageType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Overlap":
                                {
                                    PointDataRow["ColOverlap"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Detection":
                                {
                                    PointDataRow["ColDetection"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "No. of Lines":
                                {
                                    PointDataRow["ColNooflines"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "No. of Samples":
                                {
                                    PointDataRow["ColNooflines"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Window Type":
                                {
                                    PointDataRow["ColWindowType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Input Range Mode":
                                {
                                    PointDataRow["ColInputRangeMode"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Fixed Range Value":
                                {
                                    PointDataRow["ColFixedRangeValue"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Auto Mode":
                                {
                                    PointDataRow["ColAutoMode"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Meas. Type":
                                {
                                    PointDataRow["ColMeasType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Meas. Domain":
                                {
                                    PointDataRow["ColMeasDomain"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Trigger Hysteresis":
                                {
                                    PointDataRow["ColTriggerHysteresis"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Trigger Pullup":
                                {
                                    PointDataRow["ColTriggerPullup"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Bin Zeroing":
                                {
                                    PointDataRow["ColBinZeroing"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Units String":
                                {
                                    PointDataRow["ColUnitsString"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Y-axis Units":
                                {
                                    PointDataRow["ColYaxisUnit"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "X-axis Units":
                                {
                                    PointDataRow["ColXaxisUnit"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "View Signal":
                                {
                                    PointDataRow["ColViewSignal"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Y-axis Display":
                                {
                                    PointDataRow["ColYaxisDisplay"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Sensor Type":
                                {
                                    PointDataRow["ColSensorType"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "Overall":
                                {
                                    PointDataRow["ColOverall"] = ActualParameters[1].ToString();
                                    break;
                                }
                            case "X-Axis":
                                {

                                    break;
                                }
                            case "Rec Start Mode":
                                {

                                    break;
                                }
                            case "Rec Trig Level":
                                {

                                    break;
                                }
                            case "Acq. Errors":
                                {

                                    break;
                                }

                            case "RPM Stamp":
                                {

                                    break;
                                }
                            case "Time Stamp":
                                {

                                    break;
                                }
                        }


                }
                if (ShowGraph)
                {
                    //Amit Jain    DA_32	graph not display properly in report( with all parameters and graph)	code related	minor 	8-4-2010
                   // byte[] Graphbytes = GenerateandGetGraph(null);//GenerateandGetFaultFreqGraph();
                    ChartView _chartview = GenerateReportGraph(Xdata, Ydata, sXUnit, sYUnit);
                    if (_chartview != null)
                    {
                        BufferedImage objImage = new BufferedImage(_chartview);

                        Image GraphImage = (Image)objImage.GetBufferedImage();
                        byteImageData = ImageToByte(GraphImage);
                        PointDataRow["ColOrigGraph"] = byteImageData;
                    }
                    //if (Graphbytes != null)
                    //{
                    //    PointDataRow["ColOrigGraph"] = Graphbytes;
                    //}
                    ////bool bFileExists = File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportClip.jpg");
                    ////if (bFileExists != false)
                    ////{
                    ////    byteImageData = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
                    ////    PointDataRow["ColOrigGraph"] = byteImageData;
                    ////}
                }
                _Report._CSVreport1.csVdataset1.CSVdataTable.Rows.Add(PointDataRow);
                _Report._CSVreport1.csVdataset1.CSVdataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        //private byte[] GenerateandGetFaultFreqGraph()
        //{
        //    byte[] GraphBytes = null;
        //    try
        //    {
        //        DrawReportImage("FaultFreq");
        //        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
        //        {
        //            GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return GraphBytes;
        //}
        private ChartView GenerateReportGraph(string Parameter)
        {
            ChartView _chartview = null;
            polar.LineGraphControl _LineGraph = new polar.LineGraphControl();
            try
            {
                _chartview = _LineGraph.DrawReportGraph(Xdata, Ydata, ColorCode, sXUnit, sYUnit);// GenerateReportGraph(Xdata, Ydata, sXUnit, sYUnit);
                _LineGraph._MainForm = objForm;
                if (_chartview != null)
                {
                    switch (Parameter)
                    {
                        case "Band":
                            {
                                string[] BndAlrms = new string[BndAlrmsFreq.Length];

                                {
                                    //string[] BandAlarmsPowerHorizontal = new string[BndAlrmsFreq.Length];
                                    ////BndAlrms = GetBandAlarmForDI();
                                    for (int i = 0; i < BndAlrmsFreq.Length; i++)
                                    {
                                        BndAlrms[i] = BndAlrmsFreq[i].ToString() + "!" + BndAlrmsHigh[i].ToString() + "@" + BndAlrmsLow[i].ToString();
                                    }
                                    //BndAlrms[i] = BandAlarmsPowerHorizontal;
                                }
                                _chartview = _LineGraph.DrawBandRegion(BndAlrms, _chartview,sXUnit);
                                break;
                            }
                        case "FaultFreq":
                            {
                                string[] Frequencies = new string[BndAlrmsFreq.Length];
                                for (int i = 0; i < BndAlrmsFreq.Length; i++)
                                {
                                    Frequencies[i] = BndAlrmsFreq[i].ToString() + "=" + BndAlrmsHigh[i].ToString();
                                }
                                _chartview = _LineGraph.DrawFaultFrequencies(Frequencies, _chartview, sXUnit,sYUnit);

                                break;
                            }
                        case "RPM":
                            {
                                double FinalFreq = Convert.ToDouble(BndAlrmsHigh[0].ToString());
                                _chartview = _LineGraph.DrawRPMmarkers(FinalFreq, 10, _chartview,sXUnit,sYUnit);
                                break;
                            }
                    }
                }
            }
            catch(Exception ex)
            {
            }
            return _chartview;
        }
        private byte[] GenerateandGetGraph(string Parameter)
        {
            byte[] GraphBytes = null;
            try
            {
                DrawReportImage(Parameter);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
                {
                    GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return GraphBytes;
        }

        //private byte[] GenerateandGetRPMGraph()
        //{
        //    byte[] GraphBytes = null;
        //    try
        //    {
        //        DrawReportImage("RPM");
        //        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
        //        {
        //            GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return GraphBytes;
        //}

        //private byte[] GenerateandGetBandGraph()
        //{
        //    byte[] GraphBytes = null;
        //    try
        //    {
        //        DrawReportImage("Band");
        //        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg"))
        //        {
        //            GraphBytes = ReadImage(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg", new string[] { ".gif", ".jpg", ".bmp" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return GraphBytes;
        //}

        private void GetFaultFreq()
        {
            try
            {
                BndAlrmsHigh = new string[0];
                BndAlrmsLow = new string[0];
                BndAlrmsFreq = new string[0];
                if (File.Exists(sErrorLogPath + "\\FF.XML"))
                {
                    XmlDocument m_xdDocument = new XmlDocument();
                    m_xdDocument.Load(sErrorLogPath + "\\FF.XML");
                    string sXPath = "//Band/File";
                    //XmlNodeList xnlValueNodes = m_xdDocument.SelectNodes(sXPath);
                    XmlNode xnFile = m_xdDocument.SelectSingleNode("//Band/File[@Path='" + sFilepath + "']");
                    XmlNodeList xnlValueNodes = xnFile.SelectNodes("Values");

                    foreach (XmlNode xnValueNode in xnlValueNodes)
                    {
                        //Array.Resize(ref BndAlrmsFreq, BndAlrmsFreq.Length + 1);
                        _ResizeArray.IncreaseArrayString(ref BndAlrmsFreq, 1);
                        //Array.Resize(ref BndAlrmsHigh, BndAlrmsHigh.Length + 1);
                        _ResizeArray.IncreaseArrayString(ref BndAlrmsHigh, 1);

                        BndAlrmsFreq[BndAlrmsFreq.Length - 1] = xnValueNode.Attributes["Freq"].Value;
                        BndAlrmsHigh[BndAlrmsHigh.Length - 1] = xnValueNode.Attributes["Value"].Value;
                    }
                }
                arrMainIndex = new int[0];
                for (int i = 0; i < BndAlrmsFreq.Length; i++)
                {
                    double Comparator = Convert.ToDouble(BndAlrmsHigh[i].ToString());
                    if (sXUnit.Contains("CPM"))
                    {
                        Comparator = Comparator * 60;
                    }
                    MainIndex = Array.FindIndex(Xdata, delegate(double item) { return item == Comparator; });
                    if (MainIndex == -1)
                    {
                        if (Comparator <= Xdata[Xdata.Length - 1])
                        {
                            Comparator = FindNearest(Xdata, Comparator);
                            MainIndex = Array.FindIndex(Xdata, delegate(double item) { return item == Comparator; });
                        }
                    }
                    if (MainIndex != -1)
                    {
                        //Amit Jain    DA_35	RPM not display correctly and fault frequency also in Report	code related	minor 	8-4-2010

                        //Array.Resize(ref arrMainIndex, arrMainIndex.Length + 1);
                        _ResizeArray.IncreaseArrayInt(ref arrMainIndex, 1);
                        arrMainIndex[arrMainIndex.Length - 1] = MainIndex;
                        BandValuetoDisplay.Add(Convert.ToString(Math.Round(Xdata[MainIndex], 5)) + "=" + Convert.ToString(Math.Round(Ydata[MainIndex], 5)));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void GetRPM()
        {
            double RPMValue = 0;
            double PulseValue = 0;
            int PrvsMainIndex = 0;
            try
            {
                BndAlrmsHigh = new string[1];
                BndAlrmsLow = new string[0];
                BndAlrmsFreq = new string[0];
                if (File.Exists(sErrorLogPath + "\\RPM.XML"))
                {
                    XmlDocument m_xdDocument = new XmlDocument();
                    m_xdDocument.Load(sErrorLogPath + "\\RPM.XML");
                    string sXPath = "//Band/File";
                    //XmlNodeList xnlValueNodes = m_xdDocument.SelectNodes(sXPath);
                    XmlNode xnFile = m_xdDocument.SelectSingleNode("//Band/File[@Path='" + sFilepath + "']");
                    XmlNodeList xnlValueNodes = xnFile.SelectNodes("Values");

                    foreach (XmlNode xnValueNode in xnlValueNodes)
                    {
                        RPMValue = Convert.ToDouble(Convert.ToString(xnValueNode.Attributes["Freq"].Value));
                        PulseValue = Convert.ToDouble(Convert.ToString(xnValueNode.Attributes["Value"].Value));
                    }

                }


                double Comparator = (double)((double)RPMValue / (double)(PulseValue * 60));
                BndAlrmsHigh[0] = Comparator.ToString();
                if (sXUnit.Contains("CPM"))
                {
                    Comparator = Comparator * 60;
                }
                arrMainIndex = new int[0];

                for (int i = 0; i < 10; i++)
                {
                    double FreqToCalc = Comparator * (1 + i);
                    if (FreqToCalc > (double)Xdata[Xdata.Length - 1])
                    {
                        break;
                    }
                    int MainIndex = Array.FindIndex(Xdata, delegate(double item) { return item == FreqToCalc; });
                    if (MainIndex == -1)
                    {
                        FreqToCalc = FindNearest(Xdata, FreqToCalc);
                        MainIndex = Array.FindIndex(Xdata, delegate(double item) { return item == FreqToCalc; });
                    }
                    if (PrvsMainIndex != MainIndex)
                    {

                        //Array.Resize(ref arrMainIndex, arrMainIndex.Length + 1);
                        _ResizeArray.IncreaseArrayInt(ref arrMainIndex, 1);
                        arrMainIndex[arrMainIndex.Length - 1] = MainIndex;

                        BandValuetoDisplay.Add(Convert.ToString(Math.Round(Xdata[MainIndex], 5)) + "=" + Convert.ToString(Math.Round(Ydata[MainIndex], 5)));

                        PrvsMainIndex = MainIndex;
                    }
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void GetBandData()
        {
            try
            {
                BndAlrmsHigh = new string[0];
                BndAlrmsLow = new string[0];
                BndAlrmsFreq = new string[0];
                if (File.Exists(sErrorLogPath + "\\BA.XML"))
                {
                    XmlDocument m_xdDocument = new XmlDocument();
                    m_xdDocument.Load(sErrorLogPath + "\\BA.XML");
                    string sXPath = "//Band/File";

                    XmlNode xnFile = m_xdDocument.SelectSingleNode("//Band/File[@Path='" + sFilepath + "']");
                    XmlNodeList xnlValueNodes = xnFile.SelectNodes("Values");
                    foreach (XmlNode xnValueNode in xnlValueNodes)
                    {

                        {
                            // XmlNodeList xnlBand=m_xdDocument.SelectNodes(
                            //Array.Resize(ref BndAlrmsLow, BndAlrmsLow.Length + 1);
                            _ResizeArray.IncreaseArrayString(ref BndAlrmsLow, 1);
                            _ResizeArray.IncreaseArrayString(ref BndAlrmsHigh, 1);
                            _ResizeArray.IncreaseArrayString(ref BndAlrmsFreq, 1);
                            //Array.Resize(ref BndAlrmsHigh, BndAlrmsHigh.Length + 1);
                            //Array.Resize(ref BndAlrmsFreq, BndAlrmsFreq.Length + 1);
                            BndAlrmsFreq[BndAlrmsFreq.Length - 1] = xnValueNode.Attributes["Freq"].Value;
                            BndAlrmsHigh[BndAlrmsHigh.Length - 1] = xnValueNode.Attributes["ValueHigh"].Value;
                            BndAlrmsLow[BndAlrmsLow.Length - 1] = xnValueNode.Attributes["ValueLow"].Value;
                        }
                    }
                }



                for (int i = 0; i < BndAlrmsFreq.Length; i++)
                {

                    Freq = BndAlrmsFreq[i].ToString();
                    HighAlValue = BndAlrmsHigh[i].ToString();
                    LowAlValue = BndAlrmsLow[i].ToString();
                    if (i == 0)
                    {
                        prevFreq = "0";
                    }
                    else
                    {
                        prevFreq = BndAlrmsFreq[i - 1].ToString();
                        if (sXUnit.ToString().Contains("CPM"))
                        {
                            prevFreq = (Convert.ToDouble(prevFreq) * 60).ToString();
                        }
                    }
                    TempPeakinArea = 0;
                    HighestPeakinArea = 0;
                    HighestPeakinAreaAt = 0;
                    if (sXUnit.ToString().Contains("CPM"))
                    {
                        Freq = (Convert.ToDouble(Freq) * 60).ToString();
                    }
                    for (int j = 0; j < Ydata.Length; j++)
                    {

                        if (Convert.ToDouble(Xdata[j]) >= Convert.ToDouble(prevFreq))
                        {
                            if (Convert.ToDouble(Xdata[j]) <= Convert.ToDouble(Freq) && Convert.ToDouble(Xdata[j]) <= Convert.ToDouble(Xdata[Xdata.Length - 2]))
                            {
                                if (Convert.ToDouble(Ydata[j]) > Convert.ToDouble(LowAlValue))
                                {


                                    TempPeakinArea = Convert.ToDouble(Ydata[j]);
                                    if (j < Ydata.Length - 1)
                                        if (TempPeakinArea > Convert.ToDouble(Ydata[j - 1]) && TempPeakinArea > Convert.ToDouble(Ydata[j + 1]))

                                            if (TempPeakinArea > HighestPeakinArea)
                                            {
                                                HighestPeakinArea = TempPeakinArea;
                                                //if (bXunitConvert)
                                                //{                                                        
                                                //    {
                                                //        HighestPeakinAreaAt = Convert.ToDouble((Xdata[j]));//* 60
                                                //    }
                                                //}
                                                //else
                                                {
                                                    {
                                                        HighestPeakinAreaAt = Convert.ToDouble(Xdata[j]);
                                                    }
                                                }
                                            }

                                    if (i == 0)
                                    {
                                        prevFreq = "0";
                                    }
                                    else
                                    {
                                        prevFreq = BndAlrmsFreq[i - 1].ToString();
                                    }
                                    //Calculated = displayed.ToString() + "//" + arlstFreqsPA[k] + "//" + HighestPeakinAreaAt.ToString() + "///" + HighestPeakinArea.ToString() + "//" + prevFreq + "|||";
                                }
                            }
                            else
                            {
                                //if (HighestPeakinAreaAt != 0.0)
                                //{
                                //    BandValuetoDisplay.Add(HighestPeakinAreaAt.ToString() + "//" + HighestPeakinArea.ToString());
                                //}

                                break;
                            }
                        }
                    }
                    BandValuetoDisplay.Add(HighestPeakinAreaAt.ToString() + "//" + HighestPeakinArea.ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private bool CheckForTimeData(double[] Target)
        {
            bool Time = false;
            try
            {
                for (int i = 0; i < Target.Length; i++)
                {
                    if (Target[i] < 0)
                    {
                        Time = true;
                    }
                    else if (Target[i] >= 0)
                        Time = false;
                    if (Time == true)
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }

            return Time;
        }

        private static byte[] ReadImage(string p_postedImageFileName, string[] p_fileType)
        {

            bool isValidFileType = false;

            try
            {

                FileInfo file = new FileInfo(p_postedImageFileName);



                foreach (string strExtensionType in p_fileType)
                {

                    if (strExtensionType == file.Extension)
                    {

                        isValidFileType = true;

                        break;

                    }

                }

                if (isValidFileType)
                {

                    FileStream fs = new FileStream(p_postedImageFileName, FileMode.Open, FileAccess.Read);



                    BinaryReader br = new BinaryReader(fs);



                    byte[] image = br.ReadBytes((int)fs.Length);



                    br.Close();



                    fs.Close();



                    return image;

                }

                return null;

            }

            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                throw ex;

            }

        }

        private void SetAxis()
        {
            try
            {
                Rectangle Rect = new Rectangle(0, 0, 800, 480);

                Region rgn1 = new Region(Rect);



                RectangleF pt = new RectangleF(0, 0, 800, 480);

                OneFifty = (int)Math.Round((18.5643 * pt.Bottom) / 100, 0);
                TwoHundred = (int)Math.Round((24.7524 * pt.Bottom) / 100, 0);
                FourFifty = (int)Math.Round((55.6930 * pt.Bottom) / 100, 0);
                FourHundred = (int)Math.Round((49.5049 * pt.Bottom) / 100, 0);
                //16-02-2010 4:27   DA_1    Amit Jain   No over lap of y axis string, values and graph
                SixtyTwo = 62;// (int)Math.Round((6.7982 * pt.Right) / 100, 0);
                OneSixtyTwo = (int)Math.Round((17.7631 * pt.Right) / 100, 0);
                Thrghty = (int)Math.Round((3.7128 * pt.Bottom) / 100, 0);
                Fifteen = (int)Math.Round((2.0833 * pt.Right) / 100, 0); //1.6447
                TwoPointFive = (int)Math.Round((.7127 * pt.Right) / 100, 0); //.2741
                Three = (int)Math.Round((.75 * pt.Right) / 100, 0);
                Fifty = (int)Math.Round((6.1881 * pt.Bottom) / 100, 0);
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        private void DrawReportImage(double[] x, double[] y)
        {

            string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
            SetAxis();

            Rectangle Rect = new Rectangle(0, 0, 800, 480);

            Region rgn1 = new Region(Rect);
            Bitmap bmp = new Bitmap(800, 500, PixelFormat.Format24bppRgb);

            System.Drawing.Graphics der = Graphics.FromImage(bmp);
            der.Clear(Color.White);
            der.Clip = rgn1;

            Pen PenBlkB = new Pen(Color.Black, 1);
            Pen PenRed = new Pen(Color.Red, 1);
            RectangleF pt = der.ClipBounds;
            Pen BlkDash = new Pen(Color.Black, 1);
            BlkDash.DashCap = DashCap.Triangle;
            BlkDash.DashStyle = DashStyle.Dash;
            Point pt1 = new Point();
            Point pt2 = new Point();
            //Pts = new PointF[0];
            PointF[] Pts = new PointF[0];
            PointF[] PtsSelected = null;
            HighestValYAxis = 0;
            MainYAxisInterval = 0;
            MainXAxisInterval = 0;
            CursorStartInterval = 0;
            setAxisCtr = 0;
            Time = CheckForTimeData(y);
            TotalYAxis = 0.0;
            TotalXAxis = (Convert.ToDouble(pt.Right - pt.Left)) - (pt.Left + SixtyTwo + OneSixtyTwo);
            if (Time == true)
            {
                TotalYAxis = (Convert.ToDouble(pt.Bottom - pt.Top)) - (FourHundred + TwoHundred);
            }
            else
            {
                TotalYAxis = (Convert.ToDouble(pt.Bottom - pt.Top)) - (TwoHundred);
            }


            PointF Ptsn1 = new PointF();

            double MaxVal = 0;


            //der.FillRectangle(Brushes.White, (int)(pt.Left + SixtyTwo), (int)(pt.Top), (int)(pt.Right - OneSixtyTwo) - (int)(pt.Left + SixtyTwo), OneFifty);


            try
            {
                if ((x != null &&   x.Length > 1))
                {
                    if (Time == true)
                    {
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        pt2 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Top + OneFifty));//FourHundred
                        der.DrawLine(PenBlkB, pt1, pt2);
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        pt2 = new Point((int)(pt.Right - OneSixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        der.DrawLine(BlkDash, pt1, pt2);
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        pt2 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - Fifty));//FourHundred
                        der.DrawLine(PenBlkB, pt1, pt2);

                        TotalYAxis = (int)(pt.Bottom - (TwoHundred + OneFifty)) - (int)(pt.Bottom - Fifty);
                        TotalYAxis = Math.Abs(TotalYAxis);
                    }

                    else
                    {
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - Fifty));//TwoHundred
                        pt2 = new Point((int)(pt.Right - OneSixtyTwo), (int)(pt.Bottom - Fifty));//TwoHundred
                        der.DrawLine(PenBlkB, pt1, pt2);
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - Fifty));//TwoHundred
                        pt2 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Top + OneFifty));//FourHundred
                        der.DrawLine(PenBlkB, pt1, pt2);
                        TotalYAxis = (int)(pt.Bottom - Fifty) - (int)(pt.Top + OneFifty);
                        TotalYAxis = Math.Abs(TotalYAxis);
                    }
                }


                if (x!= null)
                {
                    if (x.Length > 1)
                    {
                        Pts = new PointF[0];
                        MaxVal = findHighestValue(y);
                        MaxVal *= 1.25;
                        HighestValYAxis = MaxVal;
                        MainYAxisInterval = (double)(MaxVal / TotalYAxis);
                        MainXAxisInterval = (TotalXAxis / (x.Length - 1));
                        CursorStartInterval = MainXAxisInterval;
                        SetAxisMarks(x, y, der);
                        setAxisCtr = 0;
                    }
                }
                //if (ReportVal == "Band")
                //{
                //    DrawBandRegion(x, y, "j", der);
                //}

                if (MaxVal != 0.0)
                {

                    if (x != null)
                    {
                        Pts = new PointF[x.Length];
                        for (int i = 0; i < x.Length; i++)
                        {


                            //Array.Resize(ref Pts, Pts.Length + 1);
                            if (Time == true)
                            {
                                Ptsn1 = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * i)), (float)(pt.Bottom - (TwoHundred + OneFifty) - (float)(y[i] / MainYAxisInterval))); //Ptsn1 = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * i)), (float)(pt.Bottom - (TwoHundred + OneFifty) - (float)(Ydata[i] / MainYAxisInterval)));
                            }
                            else
                            {
                                Ptsn1 = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * i)), (float)(pt.Bottom - Fifty - (float)(y[i] / MainYAxisInterval)));
                            }

                            Pts[i] = Ptsn1;
                        }
                        PtsSelected = new PointF[Pts.Length];
                        PtsSelected = Pts;

                    }

                    if (Pts != null)
                    {
                        try
                        {
                            der.DrawCurve(new Pen(Color.DarkRed, (float).5), Pts, (float)0);
                        }
                        catch (Exception ex)
                        {
                            ErrorLog_Class.ErrorLogEntry(ex);
                            int i = 0;
                            int isplitcount = Pts.Length / 100000;
                            for (int kk = 0; kk <= isplitcount; kk++)
                            {
                                if (kk != isplitcount)
                                {
                                    PointF[] tempPts = new PointF[100000];
                                    int ii = 0;
                                    for (int jj = i; jj < (100000 * (kk + 1)); jj++)
                                    {

                                        tempPts[ii] = Pts[jj];
                                        i++; ii++;

                                    }
                                    der.DrawCurve(new Pen(Color.DarkRed, (float).5), tempPts, (float)0);
                                }
                                else
                                {
                                    int iremcount = Pts.Length % 100000;
                                    PointF[] tempPts = new PointF[iremcount];
                                    int ii = 0;
                                    for (int jj = i; jj < ((100000 * (kk)) + iremcount); jj++)
                                    {

                                        tempPts[ii] = Pts[jj];
                                        i++; ii++;

                                    }
                                    der.DrawCurve(new Pen(Color.DarkRed, (float).5), tempPts, (float)0);
                                }
                            }
                        }
                    }

                }




                der.FillRectangle(Brushes.White, (int)(pt.Left + SixtyTwo), (int)(pt.Top), (int)(pt.Right - OneSixtyTwo) - (int)(pt.Left + SixtyTwo), OneFifty);


                //if (ReportVal == "FaultFreq")
                //{

                //    Pts = new PointF[0];
                //    PtsSelected = new PointF[0];
                //    StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                //    for (int i = 0; i < BndAlrmsFreq.Length; i++)
                //    {
                //        Array.Resize(ref Pts, Pts.Length + 1);
                //        Array.Resize(ref PtsSelected, PtsSelected.Length + 1);
                //        Pts[Pts.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Bottom - Fifty));
                //        PtsSelected[PtsSelected.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Top + OneFifty));

                //    }
                //    for (int i = 0; i < Pts.Length; i++)
                //    {
                //        der.DrawLine(PenRed, Pts[i], PtsSelected[i]);
                //        der.DrawString(BndAlrmsFreq[i].ToString(), new Font("Roman", 10, FontStyle.Bold), Brushes.Red, PtsSelected[i], sf);

                //    }
                //}
                //if (ReportVal == "RPM")
                //{

                //    Pts = new PointF[0];
                //    PtsSelected = new PointF[0];
                //    StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                //    for (int i = 0; i < BandValuetoDisplay.Count; i++)
                //    {
                //        Array.Resize(ref Pts, Pts.Length + 1);
                //        Array.Resize(ref PtsSelected, PtsSelected.Length + 1);
                //        Pts[Pts.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Bottom - Fifty));
                //        PtsSelected[PtsSelected.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Top + OneFifty));

                //    }
                //    for (int i = 0; i < Pts.Length; i++)
                //    {
                //        der.DrawLine(PenRed, Pts[i], PtsSelected[i]);
                //        der.DrawString((i + 1).ToString() + " X", new Font("Roman", 10, FontStyle.Bold), Brushes.Red, PtsSelected[i], sf);

                //    }
                //}
                GC.Collect();
                //if (ReportVal == "Selected Wave")
                //{
                //}
                //else
                {
                    bmp.Save(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg");
                }
                bmp.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        private void DrawReportImage(string ReportVal)
        {

            string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
            SetAxis();

            Rectangle Rect = new Rectangle(0, 0, 800, 480);

            Region rgn1 = new Region(Rect);
            Bitmap bmp = new Bitmap(800, 500, PixelFormat.Format24bppRgb);

            System.Drawing.Graphics der = Graphics.FromImage(bmp);
            der.Clear(Color.White);
            der.Clip = rgn1;

            Pen PenBlkB = new Pen(Color.Black, 1);
            Pen PenRed = new Pen(Color.Red, 1);
            RectangleF pt = der.ClipBounds;
            Pen BlkDash = new Pen(Color.Black, 1);
            BlkDash.DashCap = DashCap.Triangle;
            BlkDash.DashStyle = DashStyle.Dash;
            Point pt1 = new Point();
            Point pt2 = new Point();
            //Pts = new PointF[0];
            PointF[] Pts = new PointF[0];
            PointF[] PtsSelected = null;
            HighestValYAxis = 0;
            MainYAxisInterval = 0;
            MainXAxisInterval = 0;
            CursorStartInterval = 0;
            setAxisCtr = 0;
            Time = CheckForTimeData(Ydata);
            TotalYAxis = 0.0;
            TotalXAxis = (Convert.ToDouble(pt.Right - pt.Left)) - (pt.Left + SixtyTwo + OneSixtyTwo);
            //if (Time == true)
            //{
            //    TotalYAxis = (Convert.ToDouble(pt.Bottom - pt.Top)) - (FourHundred + TwoHundred);
            //}
            //else
            {
                TotalYAxis = (Convert.ToDouble(pt.Bottom - pt.Top)) - (TwoHundred);
            }


            PointF Ptsn1 = new PointF();

            double MaxVal = 0;


            //der.FillRectangle(Brushes.White, (int)(pt.Left + SixtyTwo), (int)(pt.Top), (int)(pt.Right - OneSixtyTwo) - (int)(pt.Left + SixtyTwo), OneFifty);


            try
            {
                if ((Xdata != null && Xdata.Length > 1))
                {
                    if (Time == true)
                    {
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        pt2 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Top + OneFifty));//FourHundred
                        der.DrawLine(PenBlkB, pt1, pt2);
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        pt2 = new Point((int)(pt.Right - OneSixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        der.DrawLine(BlkDash, pt1, pt2);
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - (TwoHundred + OneFifty)));//TwoHundred
                        pt2 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - Fifty));//FourHundred
                        der.DrawLine(PenBlkB, pt1, pt2);

                        TotalYAxis = (int)(pt.Bottom - (TwoHundred + OneFifty)) - (int)(pt.Bottom - Fifty);
                        TotalYAxis = Math.Abs(TotalYAxis);
                    }

                    else
                    {
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - Fifty));//TwoHundred
                        pt2 = new Point((int)(pt.Right - OneSixtyTwo), (int)(pt.Bottom - Fifty));//TwoHundred
                        der.DrawLine(PenBlkB, pt1, pt2);
                        pt1 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Bottom - Fifty));//TwoHundred
                        pt2 = new Point((int)(pt.Left + SixtyTwo), (int)(pt.Top + OneFifty));//FourHundred
                        der.DrawLine(PenBlkB, pt1, pt2);
                        TotalYAxis = (int)(pt.Bottom - Fifty) - (int)(pt.Top + OneFifty);
                        TotalYAxis = Math.Abs(TotalYAxis);
                    }
                }


                if (Xdata != null)
                {
                    if (Xdata.Length > 1)
                    {
                        Pts = new PointF[0];
                        MaxVal = findHighestValue(Ydata);
                        MaxVal *= 1.25;
                        HighestValYAxis = MaxVal;
                        MainYAxisInterval = (double)(MaxVal / TotalYAxis);
                        MainXAxisInterval = (TotalXAxis / (Xdata.Length - 1));
                        CursorStartInterval = MainXAxisInterval;
                        SetAxisMarks(Xdata, Ydata, der);
                        setAxisCtr = 0;
                    }
                }
                if (ReportVal == "Band")
                {
                    DrawBandRegion(Xdata, Ydata, "j", der);
                }

                if (MaxVal != 0.0)
                {

                    if (Xdata != null)
                    {
                        Pts = new PointF[Xdata.Length];
                        for (int i = 0; i < Xdata.Length; i++)
                        {


                            //Array.Resize(ref Pts, Pts.Length + 1);
                            if (Time == true)
                            {
                                Ptsn1 = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * i)), (float)(pt.Bottom - (TwoHundred + OneFifty) - (float)(Ydata[i] / MainYAxisInterval))); //Ptsn1 = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * i)), (float)(pt.Bottom - (TwoHundred + OneFifty) - (float)(Ydata[i] / MainYAxisInterval)));
                            }
                            else
                            {
                                Ptsn1 = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * i)), (float)(pt.Bottom - Fifty - (float)(Ydata[i] / MainYAxisInterval)));
                            }

                            Pts[i] = Ptsn1;
                        }
                        PtsSelected = new PointF[Pts.Length];
                        PtsSelected = Pts;

                    }

                    if (Pts != null)
                    {
                        try
                        {
                            der.DrawCurve(new Pen(Color.DarkRed, (float).5), Pts, (float)0);
                        }
                        catch (Exception ex)
                        {
                            ErrorLog_Class.ErrorLogEntry(ex);
                            int i = 0;
                            int isplitcount = Pts.Length / 100000;
                            for (int kk = 0; kk <= isplitcount; kk++)
                            {
                                if (kk != isplitcount)
                                {
                                    PointF[] tempPts = new PointF[100000];
                                    int ii = 0;
                                    for (int jj = i; jj < (100000 * (kk + 1)); jj++)
                                    {

                                        tempPts[ii] = Pts[jj];
                                        i++; ii++;

                                    }
                                    der.DrawCurve(new Pen(Color.DarkRed, (float).5), tempPts, (float)0);
                                }
                                else
                                {
                                    int iremcount = Pts.Length % 100000;
                                    PointF[] tempPts = new PointF[iremcount];
                                    int ii = 0;
                                    for (int jj = i; jj < ((100000 * (kk)) + iremcount); jj++)
                                    {

                                        tempPts[ii] = Pts[jj];
                                        i++; ii++;

                                    }
                                    der.DrawCurve(new Pen(Color.DarkRed, (float).5), tempPts, (float)0);
                                }
                            }
                        }
                    }

                }




                der.FillRectangle(Brushes.White, (int)(pt.Left + SixtyTwo), (int)(pt.Top), (int)(pt.Right - OneSixtyTwo) - (int)(pt.Left + SixtyTwo), OneFifty);


                if (ReportVal == "FaultFreq")
                {

                    Pts = new PointF[0];
                    PtsSelected = new PointF[0];
                    StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                    for (int i = 0; i < BndAlrmsFreq.Length; i++)
                    {
                        //Array.Resize(ref Pts, Pts.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref Pts, 1);
                        //Array.Resize(ref PtsSelected, PtsSelected.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref PtsSelected, 1);
                        Pts[Pts.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Bottom - Fifty));
                        PtsSelected[PtsSelected.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Top + OneFifty));

                    }
                    for (int i = 0; i < Pts.Length; i++)
                    {
                        der.DrawLine(PenRed, Pts[i], PtsSelected[i]);
                        der.DrawString(BndAlrmsFreq[i].ToString(), new Font("Roman", 10, FontStyle.Bold), Brushes.Red, PtsSelected[i], sf);

                    }
                }
                if (ReportVal == "RPM")
                {

                    Pts = new PointF[0];
                    PtsSelected = new PointF[0];
                    StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                    for (int i = 0; i < BandValuetoDisplay.Count; i++)
                    {
                        //Array.Resize(ref Pts, Pts.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref Pts, 1);
                        _ResizeArray.IncreaseArrayPointF(ref PtsSelected, 1);
                        //Array.Resize(ref PtsSelected, PtsSelected.Length + 1);
                        Pts[Pts.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Bottom - Fifty));
                        PtsSelected[PtsSelected.Length - 1] = new PointF((float)((pt.Left + SixtyTwo) + (MainXAxisInterval * (double)arrMainIndex[i])), (float)(pt.Top + OneFifty));

                    }
                    for (int i = 0; i < Pts.Length; i++)
                    {
                        der.DrawLine(PenRed, Pts[i], PtsSelected[i]);
                        der.DrawString((i + 1).ToString() + " X", new Font("Roman", 10, FontStyle.Bold), Brushes.Red, PtsSelected[i], sf);

                    }
                }
                GC.Collect();
                //if (ReportVal == "Selected Wave")
                //{
                //}
                //else
                {
                    bmp.Save(AppDomain.CurrentDomain.BaseDirectory + "ReportBandClip.jpg");
                }
                bmp.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void SetAxisMarks(double[] TargetX, double[] TargetY, Graphics objGraphics)
        {
            try
            {

                int DateCtrToDisplay = 0;
                SetAxis();
                Pen BlkDash = new Pen(Color.Black, (float).5);
                BlkDash.DashCap = DashCap.Triangle;
                BlkDash.DashStyle = DashStyle.Dash;
                int ctrAxisLines = 0;
                double IntervalXAxis = 0.0;
                bool IfTmData = false;
                RectangleF objPoint = objGraphics.ClipBounds;
                IfTmData = Time;// CheckForTimeDataInAxisLines(TargetY);
                double TotalYAxis = 0.0;
                double TotalXAxis = (Convert.ToDouble(objPoint.Right - objPoint.Left)) - (objPoint.Left + SixtyTwo + OneSixtyTwo);
                if (IfTmData == true)
                {
                    TotalYAxis = (Convert.ToDouble(objPoint.Bottom - objPoint.Top)) - (FourHundred + TwoHundred);
                }
                else
                {
                    TotalYAxis = (Convert.ToDouble(objPoint.Bottom - objPoint.Top)) - (TwoHundred);

                }
                double MaxVal = 0;
                //IfTmData = CheckForTimeDataInAxisLines(TargetY);
                PointF pt1 = new PointF();
                PointF pt2 = new PointF();
                PointF pt3 = new PointF();
                double HighestValXAxis = Convert.ToDouble(Math.Round(TargetX[TargetX.Length - 1], 5));
                IntervalXAxis = (TotalXAxis / (TargetX.Length - 1));
                int LineInterval = 0;
                //if (TrendType == "Trend")
                //{
                //    LineInterval = (TargetX.Length - 2) / 2;
                //}
                //else
                LineInterval = (TargetX.Length - 1) / 4;
                int MiniLineInterval = LineInterval / 10;
                int ctrMiniInterval = 1;
                double LineYInterval = HighestValYAxis / 4;
                double LineYAxisDistance = TotalYAxis / 4;
                try
                {
                    Font objFont = new Font("Roman", 10, FontStyle.Bold);
                    Font objFontDt = new Font("Roman", 7, FontStyle.Bold);
                    Brush objBrush = Brushes.Black;
                    if (IfTmData == true)
                    {
                        pt1 = new Point((int)(objPoint.Right - OneSixtyTwo), (int)(objPoint.Bottom - Fifty));
                        pt2 = new Point((int)(objPoint.Left + SixtyTwo), (int)(objPoint.Bottom - Fifty));
                        objGraphics.DrawLine(new Pen(Color.Black, (float)2), pt1, pt2);
                    }
                    //pt1 = new Point((int)(objPoint.Left + SixtyTwo), (int)(objPoint.Bottom - (TwoHundred + OneFifty)));
                    //if (TrendType != "Trend")
                    {
                        pt2 = new Point((int)(objPoint.Left + SixtyTwo - 15), (int)(objPoint.Bottom - Fifty));
                        //objGraphics.DrawLine(new Pen(Color.Black, 2), pt1, pt2);
                        objGraphics.DrawString(Convert.ToString(0), objFont, objBrush, pt2);
                    }
                    if (IfTmData == true)// && TrendType != "Trend")
                    {
                        pt2 = new Point((int)(objPoint.Left + SixtyTwo - 15), (int)(objPoint.Bottom - Fifty));
                        objGraphics.DrawString(Convert.ToString(0), objFont, objBrush, pt2);


                    }
                    //else if (TrendType == "Trend")
                    //{
                    //    pt2 = new Point((int)(objPoint.Left + SixtyTwo - 30), (int)(objPoint.Bottom - Fifty));
                    //    objGraphics.DrawString(Convert.ToString(TmArr[DateCtrToDisplay]), objFontDt, objBrush, pt2);
                    //    DateCtrToDisplay++;
                    //}
                    ctrAxisLines++;
                    if (IfTmData == false)
                    {
                        for (int i = 1; i < TargetX.Length; i++)
                        {
                            if (ctrAxisLines == (MiniLineInterval * ctrMiniInterval) && (MiniLineInterval * ctrMiniInterval) != LineInterval)
                            {
                                pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty - 10)));//TwoHundred
                                pt2 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                pt3 = pt1;
                                objGraphics.DrawLine(new Pen(Color.Black, (float).5), pt1, pt2);
                                HighestValXAxis = Convert.ToDouble(Math.Round(TargetX[i], 5));

                                //objGraphics.DrawString(Convert.ToString(HighestValXAxis), objFont, objBrush, pt1);
                                ctrMiniInterval++;
                            }
                            else if (ctrAxisLines == LineInterval)
                            {
                                pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty - 20)));//TwoHundred
                                pt2 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                pt3 = pt1;
                                //objGraphics.DrawLine(new Pen(Color.Black, (float)2), pt1, pt2);
                                HighestValXAxis = Convert.ToDouble(Math.Round(TargetX[i], 3));
                                //if (TrendType != "Trend")
                                {
                                    //if (bXunitConvert)
                                    //{
                                    //    //if (IsAlreadyCPM)
                                    //    //{
                                    //    //    objGraphics.DrawString(Convert.ToString(HighestValXAxis), objFont, objBrush, pt1);
                                    //    //}
                                    //    //else
                                    //    {
                                    //        objGraphics.DrawString(Convert.ToString(HighestValXAxis * 60), objFont, objBrush, pt1);
                                    //    }
                                    //}
                                    //else
                                    {
                                        //if (IsAlreadyCPM)
                                        //{
                                        //    objGraphics.DrawString(Convert.ToString(HighestValXAxis / 60), objFont, objBrush, pt1);
                                        //}
                                        //else
                                        {
                                            objGraphics.DrawString(Convert.ToString(HighestValXAxis), objFont, objBrush, pt2);
                                        }
                                    }
                                }
                                //else if (TrendType == "Trend")
                                //{
                                //    //pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                //    if (DateCtrToDisplay % 2 == 0)
                                //    {
                                //        pt1 = new PointF((float)((objPoint.Left + SixtyTwo - 30) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                //    }
                                //    else
                                //        pt1 = new PointF((float)((objPoint.Left + SixtyTwo - 30) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty + 20)));//TwoHundred

                                //    //pt2 = new PointF((float)((objPoint.Left + SixtyTwo+20) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                //    objGraphics.DrawString(Convert.ToString(TmArr[DateCtrToDisplay]), objFontDt, objBrush, pt1);


                                //}

                                pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                pt2 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Top + OneFifty));//FourHundred
                                objGraphics.DrawLine(BlkDash, pt1, pt2);
                                ctrAxisLines = 0;
                                ctrMiniInterval = 1;
                                DateCtrToDisplay++;
                            }
                            else if (ctrAxisLines == (ctrMiniInterval))
                            {
                                //if (TrendType == "Trend")
                                //{
                                //    if (DateCtrToDisplay % 2 == 0)
                                //    {
                                //        pt1 = new PointF((float)((objPoint.Left + SixtyTwo - 30) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                //    }
                                //    else
                                //        pt1 = new PointF((float)((objPoint.Left + SixtyTwo - 30) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty + 20)));//TwoHundred
                                //    //pt2 = new PointF((float)((objPoint.Left + SixtyTwo+20) + IntervalXAxis * i), (float)(objPoint.Bottom - (Fifty)));//TwoHundred
                                //    objGraphics.DrawString(Convert.ToString(TmArr[DateCtrToDisplay]), objFontDt, objBrush, pt1);
                                //    DateCtrToDisplay++;

                                //}
                                //LineInterval++;
                                ctrMiniInterval++;
                            }
                            ctrAxisLines++;
                        }
                    }
                    else if (IfTmData == true)
                    {
                        for (int i = 0; i < TargetX.Length; i++)
                        {
                            if (ctrAxisLines == (MiniLineInterval * ctrMiniInterval) && (MiniLineInterval * ctrMiniInterval) != LineInterval)
                            {
                                pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)((objPoint.Bottom - Fifty) + 10));//TwoHundred
                                pt2 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)((objPoint.Bottom - Fifty)));//TwoHundred
                                pt3 = pt1;
                                //objGraphics.DrawLine(new Pen(Color.Black, (float).5), pt1, pt2);
                                HighestValXAxis = Convert.ToDouble(Math.Round(TargetX[i], 2));
                                //objGraphics.DrawString(Convert.ToString(HighestValXAxis), objFont, objBrush, pt1);
                                ctrMiniInterval++;
                            }
                            else if (ctrAxisLines == LineInterval)
                            {
                                pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)((objPoint.Bottom - Fifty) + 20));//TwoHundred
                                pt2 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)((objPoint.Bottom - Fifty)));//TwoHundred
                                pt3 = pt1;
                                // objGraphics.DrawLine(new Pen(Color.Black, (float)2), pt1, pt2);
                                HighestValXAxis = Convert.ToDouble(Math.Round(TargetX[i], 2));
                                objGraphics.DrawString(Convert.ToString(HighestValXAxis), objFont, objBrush, pt2);


                                pt1 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)((objPoint.Bottom - Fifty)));//TwoHundred
                                pt2 = new PointF((float)((objPoint.Left + SixtyTwo) + IntervalXAxis * i), (float)(objPoint.Top + OneFifty));//FourHundred
                                objGraphics.DrawLine(BlkDash, pt1, pt2);


                                ctrAxisLines = 0;
                                ctrMiniInterval = 1;
                            }
                            ctrAxisLines++;
                        }
                    }

                    if (IfTmData == false)
                    {
                        int i = 1;
                        do
                        {


                            pt1 = new PointF((float)((objPoint.Left + SixtyTwo) - 20), (float)(objPoint.Bottom - (Fifty) - (LineYAxisDistance * i)));//TwoHundred
                            pt2 = new PointF((float)((objPoint.Left + SixtyTwo)), (float)(objPoint.Bottom - (Fifty) - (LineYAxisDistance * i)));//TwoHundred
                            pt3 = new PointF((float)((objPoint.Left + SixtyTwo) - 45), (float)(objPoint.Bottom - (Fifty) - (LineYAxisDistance * i)));
                            objGraphics.DrawLine(new Pen(Color.Black, (float)2), pt1, pt2);
                            objGraphics.DrawString(Convert.ToString(Math.Round((LineYInterval * i), 2)), objFont, objBrush, pt3);

                            pt1 = new PointF((float)(objPoint.Left + SixtyTwo), (float)(objPoint.Bottom - (Fifty) - (LineYAxisDistance * i)));//TwoHundred
                            pt2 = new PointF((float)(objPoint.Right - OneSixtyTwo), (float)(objPoint.Bottom - (Fifty) - (LineYAxisDistance * i)));//TwoHundred
                            objGraphics.DrawLine(BlkDash, pt1, pt2);


                            i++;

                        } while ((float)(objPoint.Bottom - (Fifty) - (LineYAxisDistance * i)) >= (objPoint.Top + OneFifty));
                    }

                    if (IfTmData == true)
                    {
                        int i = 1;
                        do
                        {
                            pt1 = new PointF((float)((objPoint.Left + SixtyTwo) - 20), (float)(objPoint.Bottom - (TwoHundred + OneFifty) - (LineYAxisDistance * i)));//TwoHundred
                            pt2 = new PointF((float)((objPoint.Left + SixtyTwo)), (float)(objPoint.Bottom - (TwoHundred + OneFifty) - (LineYAxisDistance * i)));//TwoHundred
                            pt3 = new PointF((float)((objPoint.Left + SixtyTwo) - 60), (float)(objPoint.Bottom - (TwoHundred + OneFifty) - (LineYAxisDistance * i)));
                            objGraphics.DrawLine(new Pen(Color.Black, (float)2), pt1, pt2);
                            objGraphics.DrawString(Convert.ToString(Math.Round((LineYInterval * i), 2)), objFont, objBrush, pt3);

                            pt1 = new PointF((float)(objPoint.Left + SixtyTwo), (float)(objPoint.Bottom - (TwoHundred + OneFifty) - (LineYAxisDistance * i)));//TwoHundred
                            pt2 = new PointF((float)(objPoint.Right - OneSixtyTwo), (float)(objPoint.Bottom - (TwoHundred + OneFifty) - (LineYAxisDistance * i)));//TwoHundred
                            objGraphics.DrawLine(BlkDash, pt1, pt2);





                            pt1 = new PointF((float)((objPoint.Left + SixtyTwo) - 20), (float)(objPoint.Bottom - (TwoHundred + OneFifty) + (LineYAxisDistance * i)));//TwoHundred
                            pt2 = new PointF((float)((objPoint.Left + SixtyTwo)), (float)(objPoint.Bottom - (TwoHundred + OneFifty) + (LineYAxisDistance * i)));//TwoHundred
                            pt3 = new PointF((float)((objPoint.Left + SixtyTwo) - 60), (float)(objPoint.Bottom - (TwoHundred + OneFifty) + (LineYAxisDistance * i)));
                            objGraphics.DrawLine(new Pen(Color.Black, (float)2), pt1, pt2);
                            objGraphics.DrawString("-" + Convert.ToString(Math.Round((LineYInterval * i), 2)), objFont, objBrush, pt3);

                            pt1 = new PointF((float)(objPoint.Left + SixtyTwo), (float)(objPoint.Bottom - (TwoHundred + OneFifty) + (LineYAxisDistance * i)));//TwoHundred
                            pt2 = new PointF((float)(objPoint.Right - OneSixtyTwo), (float)(objPoint.Bottom - (TwoHundred + OneFifty) + (LineYAxisDistance * i)));//TwoHundred
                            objGraphics.DrawLine(BlkDash, pt1, pt2);


                            i++;

                        } while ((float)(objPoint.Bottom - (TwoHundred + OneFifty) - (LineYAxisDistance * i)) >= (objPoint.Top + OneFifty));
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog_Class.ErrorLogEntry(ex);
                    //ErrorLogFile(ex);
                    //System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private float findHighestValue(double[] Target)
        {
            double MaxVal = 0.0;
            double MinVal = 0.0;
            double FinalVal = 0.0;
            try
            {
                for (int i = 0; i < Target.Length; i++)
                {
                    if (Target[i] > MaxVal)
                        MaxVal = Target[i];
                    if (Target[i] < MinVal)
                        MinVal = Target[i];
                }
                MinVal = Math.Abs(MinVal);
                if (MaxVal >= MinVal)
                    FinalVal = MaxVal;
                else if (MinVal > MaxVal)
                    FinalVal = MinVal;



            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                //ErrorLogFile(ex);
            }
            return (float)FinalVal;
        }

        private void DrawBandRegion(double[] TargetX, double[] TargetY, string Direction, Graphics objGraphics)
        {
            string[] BandAlarmsPowerHorizontal = null;
            try
            {


                if (Direction != "J")
                {
                    //panel1.Refresh();
                }


                SetAxis();
                Pen BlkDash = new Pen(Color.Black, (float).5);
                BlkDash.DashCap = DashCap.Triangle;
                BlkDash.DashStyle = DashStyle.Dash;


                bool IfTmData = false;




                RectangleF objPoint = objGraphics.ClipBounds;
                IfTmData = Time;// CheckForTimeDataInAxisLines(TargetY);

                string[] BndAlrms = null;

                {
                    BandAlarmsPowerHorizontal = new string[0];
                    //BndAlrms = GetBandAlarmForDI();
                    for (int i = 0; i < BndAlrmsFreq.Length; i++)
                    {
                        //Array.Resize(ref BandAlarmsPowerHorizontal, BandAlarmsPowerHorizontal.Length + 1);
                        _ResizeArray.IncreaseArrayString(ref BandAlarmsPowerHorizontal, 1);
                        BandAlarmsPowerHorizontal[BandAlarmsPowerHorizontal.Length - 1] = BndAlrmsFreq[i].ToString() + "!" + BndAlrmsHigh[i].ToString() + "@" + BndAlrmsLow[i].ToString();
                    }
                    BndAlrms = BandAlarmsPowerHorizontal;
                }

                PointF SpecificPointHeigh = new PointF();
                PointF SpecificPointLow = new PointF();

                PointF SpecificPointHeigh1 = new PointF();
                PointF SpecificPointLow1 = new PointF();

                PointF[] FinalPoints = new PointF[0];
                PointF[] RedPoints = new PointF[0];
                PointF[] FinalPointsForOutLine = new PointF[0];

                PointF RedLeftLower = new PointF();
                PointF RedRightLower = new PointF();
                PointF RedLeftUpper = new PointF();
                PointF RedRightUpper = new PointF();
                int MainIndexPrvs = 0;


                if (Direction == "J")
                {
                    double TotalYAxis1 = 0.0;
                    double TotalXAxis1 = (Convert.ToDouble(objPoint.Right - objPoint.Left)) - (objPoint.Left + SixtyTwo + OneSixtyTwo);
                    if (Time == true)
                    {
                        TotalYAxis1 = (Convert.ToDouble(objPoint.Bottom - objPoint.Top)) - (FourHundred + TwoHundred);
                    }
                    else
                    {
                        TotalYAxis1 = (Convert.ToDouble(objPoint.Bottom - objPoint.Top)) - (TwoHundred);
                    }
                    double MaxVal = findHighestValue(Ydata);
                    MaxVal *= 1.25;
                    HighestValYAxis = MaxVal;
                    MainYAxisInterval = (double)(MaxVal / TotalYAxis1);
                    MainXAxisInterval = (TotalXAxis1 / (Xdata.Length - 1));
                }
                Pen objPen = new Pen(Color.Black);

                for (int i = 0; i < BndAlrms.Length; i++)
                {
                    string[] Band = BndAlrms[i].Split(new string[] { "!", "@" }, StringSplitOptions.RemoveEmptyEntries);

                    FinalPoints = new PointF[0];
                    RedPoints = new PointF[0];
                    double Comparator = Convert.ToDouble(Band[0]);
                    MainIndex = Array.FindIndex(Xdata, delegate(double item) { return item == Comparator; });
                    if (MainIndex == -1)
                    {
                        Comparator = FindNearest(Xdata, Comparator);
                        MainIndex = Array.FindIndex(Xdata, delegate(double item) { return item == Comparator; });
                    }


                    if (MainIndex >= 0)
                    {
                        if (i == 0)
                        {
                            SpecificPointHeigh = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * 0)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[1]) / MainYAxisInterval)));
                            SpecificPointLow = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * 0)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[2]) / MainYAxisInterval)));
                            SpecificPointLow1 = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * MainIndex)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[2]) / MainYAxisInterval)));
                            SpecificPointHeigh1 = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * MainIndex)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[1]) / MainYAxisInterval)));

                            if (SpecificPointLow1.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointLow1.X = objPoint.Right - OneSixtyTwo;
                            }
                            if (SpecificPointHeigh1.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointHeigh1.X = objPoint.Right - OneSixtyTwo;
                            }
                            if (SpecificPointHeigh.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointHeigh.X = objPoint.Right - OneSixtyTwo;
                            }
                            if (SpecificPointLow.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointLow.X = objPoint.Right - OneSixtyTwo;
                            }

                            RedLeftLower = SpecificPointHeigh;
                            RedRightLower = SpecificPointHeigh1;
                            RedLeftUpper = new PointF(RedLeftLower.X, (float)(objPoint.Top));// SpecificPointLow;// new PointF(RedLeftLower.X, (float)(RedLeftLower.Y + 10));
                            RedRightUpper = new PointF(RedRightLower.X, (float)(objPoint.Top));// SpecificPointLow1;// new PointF(RedRightLower.X, (float)(RedLeftLower.Y + 10));
                        }
                        else
                        {
                            SpecificPointHeigh = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * MainIndexPrvs)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[1]) / MainYAxisInterval)));
                            SpecificPointLow = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * MainIndexPrvs)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[2]) / MainYAxisInterval)));
                            SpecificPointLow1 = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * MainIndex)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[2]) / MainYAxisInterval)));
                            SpecificPointHeigh1 = new PointF((float)((objPoint.Left + SixtyTwo) + (MainXAxisInterval * MainIndex)), (float)(objPoint.Bottom - Fifty - (float)(Convert.ToDouble(Band[1]) / MainYAxisInterval)));
                            if (SpecificPointLow1.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointLow1.X = objPoint.Right - OneSixtyTwo;
                            }
                            if (SpecificPointHeigh1.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointHeigh1.X = objPoint.Right - OneSixtyTwo;
                            }
                            if (SpecificPointHeigh.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointHeigh.X = objPoint.Right - OneSixtyTwo;
                            }
                            if (SpecificPointLow.X > objPoint.Right - OneSixtyTwo)
                            {
                                SpecificPointLow.X = objPoint.Right - OneSixtyTwo;
                            }
                            RedLeftLower = SpecificPointHeigh;
                            RedRightLower = SpecificPointHeigh1;
                            RedLeftUpper = new PointF(RedLeftLower.X, (float)(objPoint.Top));// SpecificPointLow;// new PointF(RedLeftLower.X, (float)(RedLeftLower.Y + 10));
                            RedRightUpper = new PointF(RedRightLower.X, (float)(objPoint.Top));// SpecificPointLow1;// new PointF(RedRightLower.X, (float)(RedLeftLower.Y + 10));
                        }

                        Brush objbr = null;
                        Brush Redbr = null;
                        //if (panel1.BackColor == Color.Yellow || panel1.BackColor == Color.Red)
                        //{
                        //    objbr = Brushes.LightBlue;
                        //    Redbr = Brushes.LightGreen;
                        //}
                        //else
                        {
                            objbr = Brushes.Yellow;
                            Redbr = Brushes.Red;
                        }


                        //Array.Resize(ref FinalPoints, FinalPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPoints, 1);
                        FinalPoints[FinalPoints.Length - 1] = SpecificPointHeigh;

                        //Array.Resize(ref FinalPointsForOutLine, FinalPointsForOutLine.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPointsForOutLine, 1);
                        FinalPointsForOutLine[FinalPointsForOutLine.Length - 1] = SpecificPointLow;

                        //Array.Resize(ref FinalPoints, FinalPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPoints, 1);
                        FinalPoints[FinalPoints.Length - 1] = SpecificPointHeigh1;

                        //Array.Resize(ref FinalPointsForOutLine, FinalPointsForOutLine.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPointsForOutLine, 1);
                        FinalPointsForOutLine[FinalPointsForOutLine.Length - 1] = SpecificPointHeigh;

                        //Array.Resize(ref FinalPoints, FinalPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPoints, 1);
                        FinalPoints[FinalPoints.Length - 1] = SpecificPointLow1;

                        //Array.Resize(ref FinalPointsForOutLine, FinalPointsForOutLine.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPointsForOutLine, 1);
                        FinalPointsForOutLine[FinalPointsForOutLine.Length - 1] = SpecificPointHeigh1;

                        //Array.Resize(ref FinalPoints, FinalPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPoints, 1);
                        FinalPoints[FinalPoints.Length - 1] = SpecificPointLow;

                        //Array.Resize(ref FinalPointsForOutLine, FinalPointsForOutLine.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref FinalPointsForOutLine, 1);
                        FinalPointsForOutLine[FinalPointsForOutLine.Length - 1] = SpecificPointLow1;


                        //Array.Resize(ref RedPoints, RedPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref RedPoints, 1);
                        RedPoints[RedPoints.Length - 1] = RedLeftLower;

                        //Array.Resize(ref RedPoints, RedPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref RedPoints, 1);
                        RedPoints[RedPoints.Length - 1] = RedRightLower;

                        //Array.Resize(ref RedPoints, RedPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref RedPoints, 1);
                        RedPoints[RedPoints.Length - 1] = RedRightUpper;

                        //Array.Resize(ref RedPoints, RedPoints.Length + 1);
                        _ResizeArray.IncreaseArrayPointF(ref RedPoints, 1);
                        RedPoints[RedPoints.Length - 1] = RedLeftUpper;

                        GraphicsPath objPath = new GraphicsPath();
                        GraphicsPath RedPath = new GraphicsPath();
                        objPath.AddCurve(FinalPoints, 0);
                        RedPath.AddCurve(RedPoints, 0);
                        objGraphics.FillPath(objbr, objPath);
                        objGraphics.FillPath(Redbr, RedPath);

                        MainIndexPrvs = MainIndex;
                    }

                }

                //objGraphics.DrawLines(objPen, FinalPointsForOutLine);
                int iFCtr = 0;
                for (int i = 0; i < FinalPointsForOutLine.Length; i++)
                {
                    objGraphics.DrawLine(objPen, FinalPointsForOutLine[i], FinalPointsForOutLine[i + 1]);
                    //if (i%3 == 0)
                    //    i++;
                }


            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);


            }
        }

        private double FindNearest(double[] TargetArray, double ValueToBeFound)
        {
            double Value = 0.0;
            try
            {
                for (int i = 0; i < TargetArray.Length; i++)
                {
                    Value = TargetArray[i];
                    if (Value > ValueToBeFound)
                    {
                        if ((double)(Value - ValueToBeFound) > Math.Abs((double)(ValueToBeFound - (double)TargetArray[i - 1])))
                        {
                            Value = TargetArray[i - 1];
                        }
                        else
                        {
                            Value = TargetArray[i];
                        }

                        break;
                    }
                }
                return Value;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                //ErrorLogFile(ex);
                return Value;
            }
        }

        private double[] fftMag(double[] x)
        {

            // assume n is a power of 2
            if (Xdata.Length % 2 == 0)
            {
                n = Xdata.Length;
            }
            else
            {
                n = Xdata.Length - 1;
            }
            nu = (int)(Math.Log(n) / Math.Log(2));
            int n2 = n / 2;
            int nu1 = nu - 1;
            double[] xre = new double[n];
            double[] xim = new double[n];
            double[] mag = new double[n2];
            double tr, ti, p, arg, c, s;
            try
            {
                for (int i = 0; i < n; i++)
                {
                    xre[i] = x[i];
                    xim[i] = 0.0f;
                }
                int k = 0;

                for (int l = 1; l <= nu; l++)
                {
                    while (k < n)
                    {
                        for (int i = 1; i <= n2; i++)
                        {
                            p = bitrev(k >> nu1);
                            arg = 2 * (double)Math.PI * p / n;
                            c = (double)Math.Cos(arg);
                            s = (double)Math.Sin(arg);
                            tr = xre[k + n2] * c + xim[k + n2] * s;
                            ti = xim[k + n2] * c - xre[k + n2] * s;
                            xre[k + n2] = xre[k] - tr;
                            xim[k + n2] = xim[k] - ti;
                            xre[k] += tr;
                            xim[k] += ti;
                            k++;
                        }
                        k += n2;
                    }
                    k = 0;
                    nu1--;
                    n2 = n2 / 2;
                }
                k = 0;
                int r;
                while (k < n)
                {
                    r = bitrev(k);
                    if (r > k)
                    {
                        tr = xre[k];
                        ti = xim[k];
                        xre[k] = xre[r];
                        xim[k] = xim[r];
                        xre[r] = tr;
                        xim[r] = ti;
                    }
                    k++;
                }

                mag[0] = 0;// (double)(Math.Sqrt(xre[0] * xre[0] + xim[0] * xim[0])) / n;
                for (int i = 1; i < n / 2; i++)
                {
                    //double temp_mag = (double)(Math.Sqrt(xre[i] * xre[i] + xim[i] * xim[i])) / 1000;
                    //double temp_2Per_mag = (2 * temp_mag) / 100;

                    //mag[i] = (float)(Math.Sqrt(xre[i] * xre[i] + xim[i] * xim[i])) / 1000;
                    //mag[i] = temp_mag - temp_2Per_mag;
                    mag[i] = (float)((2 * (float)(Math.Sqrt(xre[i] * xre[i] + xim[i] * xim[i]))) / n);
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return mag;
        }
        private int bitrev(int j)
        {

            int j2;
            int j1 = j;
            int k = 0;
            try
            {
                for (int i = 1; i <= nu; i++)
                {
                    j2 = j1 / 2;
                    k = 2 * k + j1 - 2 * j2;
                    j1 = j2;
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return k;
        }







    }
}
