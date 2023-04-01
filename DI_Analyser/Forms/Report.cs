using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DI_Analyser.interfaces;
using DI_Analyser.Classes;
using DevExpress.XtraPrinting.Control;
using DI_Analyser.Reporting;
using Analyser.Properties;
using System.Collections;
using Analyser.Reporting;
using System.Diagnostics;
using Analyser.Classes;

namespace DI_Analyser
{
    public partial class Report :  DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Report()
        {
            InitializeComponent();
            this.ribbonControl1.ApplicationIcon = Resources.vibanalyst;
        }
        Report_Interface _Report_interface = new Report_Control();

        public PrintControl _printcontrol1
        {
            get
            {
                return printControl1;
            }
        }

        CSVReport1 rpt1 = null;
        public CSVReport1 _CSVreport1
        {
            get
            {
                return rpt1;
            }
            set
            {
                rpt1 = value;
            }
        }
        CSVBandReport rpt2 = null;
        public CSVBandReport _CSVBandReport
        {
            get
            {
                return rpt2;
            }
            set
            {
                rpt2 = value;
            }
        }
        WAVESelectedReport rpt3 = null;
        public WAVESelectedReport _WAVESelectedReport
        {
            get
            {
                return rpt3;
            }
            set
            {
                rpt3 = value;
            }
        }
        MultiFileReport rpt4 = null;
        public MultiFileReport _MultiFileReport
        {
            get
            {
                return rpt4;
            }
            set
            {
                rpt4 = value;
            }
        }
        Form1 objForm = null;
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

        string sListViewItems = null;
        public string _ListItems
        {
            get
            {
                return sListViewItems;
            }
            set
            {
                sListViewItems = value;
            }
        }

        string CSVData = null;
        public string _ReportDataCSV
        {
            get
            {
                return CSVData;
            }
            set
            {
                CSVData = value;
            }
        }
        double[] dXData = null;
        public double[] _XData
        {
            get
            {
                return dXData;
            }
            set
            {
                dXData = value;
            }
        }
        double[] dYData = null;
        public double[] _YData
        {
            get
            {
                return dYData;
            }
            set
            {
                dYData = value;
            }
        }
        ArrayList arlData = null;
        public ArrayList _DATA
        {
            get
            {
                return arlData;
            }
            set
            {
                arlData = value;
            }
        }
       ArrayList arlSelectedDataGridValue= null;
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
       string sXunit = null;
       public string _Xunit
       {
           get
           {
               return sXunit;
           }
           set
           {
               sXunit = value;
           }
       }
       string sYunit = null;
       public string _Yunit
       {
           get
           {
               return sYunit;
           }
           set
           {
               sYunit = value;
           }
       }
       string SelectedFile = null;
       public string _SelectedFile
       {
           get
           {
               return SelectedFile;
           }
           set
           {
               SelectedFile = value;
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
       Process BusyImageProcess;
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int prtType = 0;
                if (listBox1.SelectedItem != null)
                {


                    try
                    {
                        this.Refresh();
                        BusyImageProcess = Process.Start(AppDomain.CurrentDomain.BaseDirectory + "BusyProcess.exe");
                        this.Cursor = Cursors.WaitCursor;
                    }

                    catch (Exception ep)
                    {
                        ErrorLog_Class.ErrorLogEntry(ep);
                    }

                    _Report_interface._Form1 = _Form1;
                    _Report_interface._Report = this;
                    if (CSVData != null)
                    {
                        _Report_interface._CSVData = CSVData;
                        _Report_interface._Xunit = _Xunit;
                        _Report_interface._Yunit = _Yunit;
                        prtType = _Report_interface.generateReport(listBox1.SelectedItem.ToString());
                        
                    }
                    else if (_DATA != null)
                    {
                        _Report_interface._WAVDataGridValue = _WAVDataGridValue;
                        _Report_interface._WAVDataValues = _WAVDataValues;
                        _Report_interface._DATA = _DATA;
                        _Report_interface._Xunit = _Xunit;
                        _Report_interface._Yunit = _Yunit;
                        prtType = _Report_interface.generateReport(listBox1.SelectedItem.ToString());

                    }
                    else
                    {
                        try
                        {
                            BusyImageProcess.Kill();
                            this.Cursor = Cursors.Default;
                        }
                        catch
                        {

                        }
                        if (listBox1.SelectedItem.ToString().Contains("FDT"))
                        {
                            _Report_interface._SelectedCaption = _SelectedCaption;
                            _Report_interface._Xunit = _Xunit;
                            _Report_interface._Yunit = _Yunit;
                        }
                       prtType= _Report_interface.generateReport(listBox1.SelectedItem.ToString());
                       
                    }
                }
                //WrkerCmpleted(prtType);
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            try
            {
                BusyImageProcess.Kill();
                this.Cursor = Cursors.Default;
            }
            catch
            {
                
            }
        }
        public void StartProcess(string path)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch { }
        }


        public void WrkerCmpleted(int prtType)
        {
            if (prtType == 1)
            {
                if ( _CSVreport1.RowCount > 0)
                {
                     //_printcontrol1.PrintingSystem =  _CSVreport1.PrintingSystem;
                     //_CSVreport1.CreateDocument();
                     try
                     {



                         _CSVreport1.ExportToRtf("c:\\Test2.rtf");
                         _CSVreport1.ExportToImage("c:\\Test2.bmp");
                        // StartProcess("c:\\Test.rtf");
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }
                }
            }
            else if (prtType == 2)
            {
                if ( _CSVBandReport.RowCount > 0)
                {
                     _printcontrol1.PrintingSystem =  _CSVBandReport.PrintingSystem;
                     _CSVBandReport.CreateDocument();
                }
            }
            else if (prtType == 3)
            {
                if ( _WAVESelectedReport.RowCount > 0)
                {
                     _printcontrol1.PrintingSystem =  _WAVESelectedReport.PrintingSystem;
                     _WAVESelectedReport.CreateDocument();
                }
            }
            else if (prtType == 4)
            {
                if ( _MultiFileReport.RowCount > 0)
                {
                     _printcontrol1.PrintingSystem =  _MultiFileReport.PrintingSystem;
                     _MultiFileReport.CreateDocument();
                }
            }

        }
        private void Report_Shown(object sender, EventArgs e)
        {
            try
            {
                string[] items = _ListItems.Split(new string[] { " % " }, StringSplitOptions.RemoveEmptyEntries);
                listBox1.Items.AddRange(items);
                _Report_interface._Xdata = _XData;
                _Report_interface._Ydata = _YData;
                _Report_interface._CurrentFilePath = _SelectedFile;
                if (printControl1.PrintingSystem != null)
                {
                    printControl1.PrintingSystem.ClearContent();
                    printControl1.PrintingSystem.Dispose();
                }
                else
                {
                    printControl1.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

       
    }
}
