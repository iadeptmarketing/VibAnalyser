using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Analyser.Classes;//using dotnetCHARTING.WinForms.MySql.Data.MySqlClient;

namespace DI_Analyser.Forms
{
    public partial class frmRpmCount :  DevExpress.XtraEditors.XtraForm
    {
        public frmRpmCount()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 2;
        }
        int RPMCount = 10;
        public int _RPMCount
        {
            get
            {
                return RPMCount;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {

                RPMCount = Convert.ToInt32(comboBox1.SelectedItem.ToString());

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            this.Close();
        }
        public string _HeaderText
        {
            set
            {
                this.Text = value;
            }
        }
        public string _LabelText
        {
            set
            {
                label1.Text = value;
            }
        }
      
    }
}
