//Amit jain     DA_38	click the sidebandtrend single line cursor moves ,but I have not selected any cursor	code related	minor 	12-4-2010


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
    public partial class SideBandTrend :  DevExpress.XtraEditors.XtraForm
    {
        public SideBandTrend()
        {
            InitializeComponent();
            this.Shown += new EventHandler(SideBandTrend_Shown);
        }
       

        void SideBandTrend_Shown(object sender, EventArgs e)
        {
            try
            {
                tbBandValue.Text = "10";
                tbFreq.Text = "100";
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }


        public string FreqVal
        {
            set
            {
                tbFreq.Text = value;
                tbFreq.Refresh();
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

        double iFreq = 100;
        public double _Freq
        {
            get
            {
                return iFreq;
            }
        }
        //Amit jain     DA_38	click the sidebandtrend single line cursor moves ,but I have not selected any cursor	code related	minor 	12-4-2010

        Form1 _mainForm = null;
        public Form1 _Form1
        {
            set
            {
                _mainForm = value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ValChk = (Convert.ToInt32(tbBandValue.Text.ToString()));
                if (ValChk <= 100)
                {
                    ival = ValChk;
                    iFreq = Convert.ToDouble(tbFreq.Text.ToString());
                    this.Close();
                   
                }
                else
                {
                    MessageBoxEx.Show("You can enter upto 100 % only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                MessageBoxEx.Show("Please Enter Only Numerical values","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            //Amit jain     DA_38	click the sidebandtrend single line cursor moves ,but I have not selected any cursor	code related	minor 	12-4-2010

            _mainForm.SetCurSorVal = false;
        }

        private void tbBandValue_KeyUp(object sender, KeyEventArgs e)
        {

        }


    }
}
