using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Analyser.Classes;

namespace iAdeptMain.AllForms
{
    public partial class octaveSettings : Form// DevExpress.XtraEditors.XtraForm
    {
        public octaveSettings()
        {
            InitializeComponent();
        }

        string sOctaveValue = null;
        public string Octave
        {
            get
            {
                return sOctaveValue;
            }
            set
            {
                sOctaveValue = value;
            }
        }

        string sBarValue = null;
        public string Bars
        {
            get
            {
                return sBarValue;
            }
            set
            {
                sBarValue = value;
            }
        }


        

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Bars = (string)cmbBarstyle.SelectedItem;
                if (txtN.Visible)
                {
                    Octave = Convert.ToString(Convert.ToInt32(txtN.Text));
                }
                else
                {
                    Octave = (string)cmbOctave.SelectedItem;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                MessageBoxEx.Show("Value Not Correct");
            }
        }

        private void cmbOctave_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((string)cmbOctave.SelectedItem == "n")
                {
                    txtN.Visible = true;
                    lblN.Visible = true;
                }
                else
                {
                    txtN.Visible = false;
                    lblN.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
    }
}
