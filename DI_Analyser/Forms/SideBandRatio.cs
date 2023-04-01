using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using DevComponents.DotNetBar;
using Analyser.Classes;//using dotnetCHARTING.WinForms.MySql.Data.MySqlClient;

namespace DI_Analyser
{
    public partial class SideBandRatio :  DevExpress.XtraEditors.XtraForm
    {
        public SideBandRatio()
        {
            InitializeComponent();
            this.Shown += new EventHandler(SideBandRatio_Shown);
        }
        
        void SideBandRatio_Shown(object sender, EventArgs e)
        {
            try
            {
                tbBandValue.Text = "10";
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }


        


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        int ival = 10;
        public int _Value
        {
            get
            {
                return ival;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValChk = (Convert.ToInt32(tbBandValue.Text.ToString()));
                if (ValChk <= 1000)
                {
                    ival = ValChk;
                   
                    this.Close();
                }
                else
                {
                    MessageBoxEx.Show("You can enter upto 1000 % only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                MessageBoxEx.Show("Please Enter Only Numerical values","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void tbBandValue_KeyUp(object sender, KeyEventArgs e)
        {

        }


    }
}
