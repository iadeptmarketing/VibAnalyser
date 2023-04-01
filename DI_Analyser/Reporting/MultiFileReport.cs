using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Analyser.Reporting
{
    public partial class MultiFileReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MultiFileReport()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (lblTitle.Text.ToString().Contains("FDT"))
            {
                xrTableCell3.Text = "Caption";
                xrTableCell5.Visible = false;
                xrTableCell7.Visible = false;
            }
            else
            {
            }

        }

    }
}
