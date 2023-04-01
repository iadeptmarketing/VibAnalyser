using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using Analyser.Classes;

namespace Analyser.Forms
{
    public partial class frmTable : Form
    {
        public frmTable()
        {
            InitializeComponent();
        }
        int RPMCtr = 0;
        double[] RPM = null;
        public double[] _RPM
        {
            get
            {
                return RPM;
            }
            set
            {
                RPM = value;
            }
        }
        ArrayList YMag = null;
        public ArrayList _YMag
        {
            get
            {
                return YMag;
            }
            set
            {
                YMag = value;
            }
        }
        ArrayList YPhase = null;
        public ArrayList _YPhase
        {
            get
            {
                return YPhase;
            }
            set
            {
                YPhase = value;
            }
        }
        public int _RPMCtr
        {
            get
            {
                return RPMCtr;
            }
            set
            {
                RPMCtr = value;
            }
        }
        
        private void frmTable_Shown(object sender, EventArgs e)
        {
            try
            {
                 dgvTableData.Columns.Add("ColRPM", "RPM");
                
                for (int i = 0; i < RPMCtr; i++)
                {
                   switch(i)
                    {
                        case 0:
                            {
                               dgvTableData.Columns.Add("ColOverall", "Overall");
                                
                                break;
                            }
                       
                        default:
                            {
                               {
                                   dgvTableData.Columns.Add("ColRPMX" + i.ToString(), i.ToString() + "X Mag");
                                   dgvTableData.Columns.Add("ColRPMDeg" + i.ToString(), i.ToString() + "X Deg");
                               }
                                break;
                            }
                       
                    }
                }
                for (int i = 0; i < _RPM.Length; i++)
                {
                    dgvTableData.Rows.Add(1);
                    dgvTableData[0, i].Value = _RPM[i].ToString();
                }
                    for(int j=0;j<RPMCtr;j++)
                    {
                        double[] MagValue = null;                        
                        double[] PhaseValue = null;

                        MagValue = (double[])YMag[j];
                        PhaseValue = (double[])YPhase[j];

                        
                        
                        for (int i = 0; i < _RPM.Length; i++)
                        {

                            if (j == 0)
                            {
                                dgvTableData[1, i].Value = MagValue[i].ToString();
                            }
                            else
                            {
                                dgvTableData[(j * 2), i].Value = MagValue[i].ToString();
                                dgvTableData[(j *2)+1, i].Value = PhaseValue[i].ToString();
                            }
                        }
                    }
               // }
                        
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
    }
}
