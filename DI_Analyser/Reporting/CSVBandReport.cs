using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Analyser.Classes;

namespace DI_Analyser.Reporting
{
    public partial class CSVBandReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CSVBandReport()
        {
            InitializeComponent();
        }
        string sToShow = null;
        public string _ToShow
        {
            get
            {
                return sToShow;
            }
            set
            {
                sToShow = value;
            }
        }
        private void CSVBandReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (_ToShow != null)
                {
                    if (_ToShow == "Fault")
                    {
                        xrTableCell1.Visible = false;
                        xrTableCell5.Visible = false;
                        xrTableCell4.Text = "Frequency Name";
                    }
                    if (_ToShow == "RPM")
                    {
                        xrTableCell1.Visible = false;
                        xrTableCell5.Visible = false;
                        xrTableCell4.Text = "RPM";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
                
        }

    }
}
