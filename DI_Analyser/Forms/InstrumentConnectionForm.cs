using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DI_Analyser.interfaces;
using DI_Analyser.Classes;
using trial6;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Analyser.Properties;
using Analyser.Classes;

namespace DI_Analyser.Forms
{
    public partial class InstrumentConnectionForm : Form// DevExpress.XtraEditors.XtraForm
    {
        HASP_Interface _HaspInt = new HASP_Control();

        public InstrumentConnectionForm()
        {
            InitializeComponent();
            bubbleButtonDOWNLOAD.Image = Resources.dl;
            bubbleButtonDOWNLOAD.ImageLarge = Resources.dl;
            rcPanel.Style.BackgroundImage = Resources.Edited_IMXA460_large;
        }

        private void InstrumentConnectionForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                expandablePanelCSV.Width = rcPanel.Width / 3;
                expandablePanelTour.Width = expandablePanelCSV.Width;
                expandablePanelWAV.Width = expandablePanelTour.Width;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        string SerialFromKey = null;
        public string Key_serial
        {
            set
            {
                SerialFromKey = value;
            }
        }
        Hashtable Tour = new Hashtable();
        Hashtable WAV = new Hashtable();
        Hashtable CSV = new Hashtable();
        private void InstrumentConnectionForm_Load(object sender, EventArgs e)
        {
            Tour = new Hashtable();
            WAV = new Hashtable();
            CSV = new Hashtable();
            try
            {
                _HaspInt.Key_serial = SerialFromKey;
                string[] NmsUsb = _HaspInt.ExtractRoutes();
                for (int iNewTest = 0; iNewTest < NmsUsb.Length; iNewTest++)
                {
                    listBoxTour.Items.Add(NmsUsb[iNewTest]);//Filling list box with Route Names                
                }
                NmsUsb = _HaspInt.ExtractCSV();
                for (int iNewTest = 0; iNewTest < NmsUsb.Length; iNewTest++)
                {
                    listBoxCSV.Items.Add(NmsUsb[iNewTest]);//Filling list box with CSV File Names                
                }
                NmsUsb = _HaspInt.ExtractWAV();
                for (int iNewTest = 0; iNewTest < NmsUsb.Length; iNewTest++)
                {
                    listBoxWAV.Items.Add(NmsUsb[iNewTest]);//Filling list box with WAV File Names                
                }
                Tour = _HaspInt._TOUR;
                WAV = _HaspInt._WAV;
                CSV = _HaspInt._CSV;

                
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }

        }
        
        private void listBoxTour_Enter(object sender, EventArgs e)
        {
            try
            {
                if (listBoxWAV.SelectedItem != null)
                {
                    listBoxWAV.SelectedItem = null;
                }
                if (listBoxCSV.SelectedItem != null)
                {
                    listBoxCSV.SelectedItem = null;
                }
                
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }  
        }

        private void listBoxCSV_Enter(object sender, EventArgs e)
        {
            try
            {
                if (listBoxWAV.SelectedItem != null)
                {
                    listBoxWAV.SelectedItem = null;
                }
                if (listBoxTour.SelectedItem != null)
                {
                    listBoxTour.SelectedItem = null;
                }
                
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void listBoxWAV_Enter(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTour.SelectedItem != null)
                {
                    listBoxTour.SelectedItem = null;
                }
                if (listBoxCSV.SelectedItem != null)
                {
                    listBoxCSV.SelectedItem = null;
                }
                
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void bubbleButtonTour_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            try
            {
                listBoxCSV.ClearSelected();
                listBoxWAV.ClearSelected();
                listBoxTour_Enter(null, null);
                if (listBoxTour.Items.Count > 0)
                {
                    listBoxTour.SelectedIndex = 0;
                }
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void bubbleButtonCSV_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            try
            {
                listBoxTour.ClearSelected();
                listBoxWAV.ClearSelected();
                listBoxCSV_Enter(null, null);
                if (listBoxCSV.Items.Count > 0)
                {
                    listBoxCSV.SelectedIndex = 0;
                }
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void bubbleButtonWAV_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            try
            {
                listBoxTour.ClearSelected();
                listBoxCSV.ClearSelected();
                listBoxWAV_Enter(null, null);
                if (listBoxWAV.Items.Count > 0)
                {
                    listBoxWAV.SelectedIndex = 0;
                }
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        string sPath = null;
        public string _sPath
        {
            get
            {
                return sPath;
            }

        }
        private void bubbleButtonDOWNLOAD_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            string PathofFile = null;
            string FileName = null;
            string[] arrayPathofFile = null;
            string[] arrayFileName = null;
            sPath = null;
            try
            {
                if (listBoxTour.SelectedItem != null)
                {
                    arrayPathofFile = new string[3];
                    arrayFileName = new string[3];
                    PathofFile = Tour[listBoxTour.SelectedItem].ToString();
                    PathofFile += "\\tourdata.dat";
                    FileName = "tourdata";//listBoxTour.SelectedItem.ToString().Replace(".", "");
                    FileName += ".dat";

                    arrayFileName[0] = FileName;
                    arrayPathofFile[0] = PathofFile;

                    PathofFile = Tour[listBoxTour.SelectedItem].ToString();
                    PathofFile += "\\offtdata.dat";
                    FileName = "offtdata";//listBoxTour.SelectedItem.ToString().Replace(".", "");
                    FileName += ".dat";

                    arrayFileName[1] = FileName;
                    arrayPathofFile[1] = PathofFile;

                    PathofFile = Tour[listBoxTour.SelectedItem].ToString();
                    PathofFile += "\\histdata.dat";
                    FileName = "histdata";//listBoxTour.SelectedItem.ToString().Replace(".", "");
                    FileName += ".dat";

                    arrayFileName[2] = FileName;
                    arrayPathofFile[2] = PathofFile;

                    FileName = null;
                    PathofFile = null;
                }
                else if (listBoxWAV.SelectedItem != null)
                {
                    PathofFile = WAV[listBoxWAV.SelectedItem].ToString();
                    PathofFile += listBoxWAV.SelectedItem.ToString();
                    FileName = listBoxWAV.SelectedItem.ToString().Replace(".", "");
                    FileName += ".WAV";
                }
                else if (listBoxCSV.SelectedItem != null)
                {
                    PathofFile = CSV[listBoxCSV.SelectedItem].ToString();
                    PathofFile += listBoxCSV.SelectedItem.ToString();
                    FileName = listBoxCSV.SelectedItem.ToString().Replace(".", "");
                    FileName += ".CSV";
                }
                if (PathofFile != null)
                {
                    FolderBrowserDialog _Dialog = new FolderBrowserDialog();
                    _Dialog.Description = "Save to Location";
                    _Dialog.ShowDialog();
                    sPath = _Dialog.SelectedPath;
                    if (sPath != null)
                    {
                        bool Check = _HaspInt.DownloadFile(sPath, PathofFile, FileName);
                        if (Check)
                        {
                            this.Close();
                        }
                    }
                }
                if (arrayPathofFile != null)
                {
                    FolderBrowserDialog _Dialog = new FolderBrowserDialog();
                    _Dialog.Description = "Save to Location";
                    _Dialog.ShowDialog();
                    sPath = _Dialog.SelectedPath;
                    if (sPath != null)
                    {
                        bool Check = _HaspInt.DownloadFile(sPath, arrayPathofFile, arrayFileName);
                        if (Check)
                        {
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void InstrumentConnectionForm_Shown(object sender, EventArgs e)
        {
            bool ExitCode = true;
            try
            {
                if (listBoxCSV.Items.Count > 0)
                {
                    ExitCode = false;
                }
                if (listBoxTour.Items.Count > 0)
                {
                    ExitCode = false;
                }
                if (listBoxWAV.Items.Count > 0)
                {
                    ExitCode = false;
                }

                if (ExitCode)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        

       
       
    }
}
