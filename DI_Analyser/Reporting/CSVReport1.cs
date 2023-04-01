using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Analyser.Classes;

namespace DI_Analyser
{
    public partial class CSVReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public CSVReport1()
        {
            InitializeComponent();
        }
        
        private void BeforePrint(int iCount)
        {
            try
            {
                switch (iCount)
                {
                    case 1:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell3.Text))
                            {
                                xrTableCell1.Text = "";
                            }
                            break;
                        }
                    case 2:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell6.Text))
                            {
                                xrTableCell5.Text = "";
                            } break;
                        }
                    case 3:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell4.Text))
                            {
                                xrTableCell2.Text = "";
                            } break;
                        }
                    case 4:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell8.Text))
                            {
                                xrTableCell7.Text = "";
                            } break;
                        }
                    case 5:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell12.Text))
                            {
                                xrTableCell11.Text = "";
                            } break;
                        }
                    case 6:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell10.Text))
                            {
                                xrTableCell9.Text = "";
                            } break;
                        }
                    case 7:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell14.Text))
                            {
                                xrTableCell13.Text = "";
                            } break;
                        }
                    case 8:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell18.Text))
                            {
                                xrTableCell17.Text = "";
                            } break;
                        }
                    case 9:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell16.Text))
                            {
                                xrTableCell15.Text = "";
                            } break;
                        }
                    case 10:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell20.Text))
                            {
                                xrTableCell19.Text = "";
                            } break;
                        }
                    case 11:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell24.Text))
                            {
                                xrTableCell23.Text = "";
                            } break;
                        }
                    case 12:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell22.Text))
                            {
                                xrTableCell21.Text = "";
                            } break;
                        }
                    case 13:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell26.Text))
                            {
                                xrTableCell25.Text = "";
                            } break;
                        }
                    case 14:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell30.Text))
                            {
                                xrTableCell29.Text = "";
                            } break;
                        }
                    case 15:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell28.Text))
                            {
                                xrTableCell27.Text = "";
                            } break;
                        }
                    case 16:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell32.Text))
                            {
                                xrTableCell31.Text = "";
                            } break;
                        }
                    case 17:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell36.Text))
                            {
                                xrTableCell35.Text = "";
                            } break;
                        }
                    case 18:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell38.Text))
                            {
                                xrTableCell37.Text = "";
                            } break;
                        }
                    case 19:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell42.Text))
                            {
                                xrTableCell41.Text = "";
                            } break;
                        }
                    case 20:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell40.Text))
                            {
                                xrTableCell39.Text = "";
                            } break;
                        }
                    case 21:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell44.Text))
                            {
                                xrTableCell43.Text = "";
                            } break;
                        }
                    case 22:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell48.Text))
                            {
                                xrTableCell47.Text = "";
                            } break;
                        }
                    case 23:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell46.Text))
                            {
                                xrTableCell45.Text = "";
                            } break;
                        }
                    case 24:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell50.Text))
                            {
                                xrTableCell49.Text = "";
                            } break;
                        }
                    case 25:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell54.Text))
                            {
                                xrTableCell53.Text = "";
                            } break;
                        }
                    case 26:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell52.Text))
                            {
                                xrTableCell51.Text = "";
                            } break;
                        }
                    case 27:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell56.Text))
                            {
                                xrTableCell55.Text = "";
                            } break;
                        }
                    case 28:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell60.Text))
                            {
                                xrTableCell59.Text = "";
                            } break;
                        }
                    case 29:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell58.Text))
                            {
                                xrTableCell57.Text = "";
                            } break;
                        }
                    case 30:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell62.Text))
                            {
                                xrTableCell61.Text = "";
                            } break;
                        }
                    case 31:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell66.Text))
                            {
                                xrTableCell65.Text = "";
                            } break;
                        }
                    case 32:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell64.Text))
                            {
                                xrTableCell63.Text = "";
                            } break;
                        }
                    case 33:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell68.Text))
                            {
                                xrTableCell67.Text = "";
                            } break;
                        }
                    case 34:
                        {
                            if (string.IsNullOrEmpty((string)xrTableCell34.Text))
                            {
                                xrTableCell33.Text = "";
                            }
                            break;
                        }
                }
            
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        private void xrTableRow1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(1);
        }
        

        private void xrTableRow3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(2);
        }

        private void xrTableRow2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(3);
        }

        private void xrTableRow4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(4);
        }

        private void xrTableRow6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(5);
        }

        private void xrTableRow5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(6);
        }

        private void xrTableRow7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(7);
        }

        private void xrTableRow9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(8);
        }

        private void xrTableRow8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(9);
        }

        private void xrTableRow10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(10);
        }

        private void xrTableRow12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(11);
        }

        private void xrTableRow11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(12);
        }

        private void xrTableRow13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(13);
        }

        private void xrTableRow15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(14);
        }

        private void xrTableRow14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(15);
        }

        private void xrTableRow16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(16);
        }

        private void xrTableRow18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(17);
        }

        private void xrTableRow19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(18);
        }

        private void xrTableRow21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(19);
        }

        private void xrTableRow20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(20);
        }

        private void xrTableRow22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(21);
        }

        private void xrTableRow24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(22);
        }

        private void xrTableRow23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(23);
        }

        private void xrTableRow25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(24);
        }

        private void xrTableRow27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(25);
        }

        private void xrTableRow26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(26);
        }

        private void xrTableRow28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(27);
        }

        private void xrTableRow30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(28);
        }

        private void xrTableRow29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(29);
        }

        private void xrTableRow31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(30);
        }

        private void xrTableRow33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(31);
        }

        private void xrTableRow32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(32);
        }

        private void xrTableRow34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(33);
        }

        private void xrTableRow17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BeforePrint(34);
        }

    }
}
