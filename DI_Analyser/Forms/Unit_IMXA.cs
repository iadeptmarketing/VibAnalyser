using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using DevComponents.DotNetBar;
using Analyser.Classes;
using Analyser.interfaces;

namespace Analyser.AllForms
{
    public partial class Unit_IMXA :  DevExpress.XtraEditors.XtraForm
    {
        public Unit_IMXA()
        {
            InitializeComponent();
        }
        string sUnitOld = null;
        string sUnitNew = null;
        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();
        public string RetNewUnit
        {
            get
            {
                return sUnitNew;
            }
        }

        public string GetOldUnit
        {
            get
            {
                return sUnitOld;
            }
            set
            {
                sUnitOld = value;
            }
        }
        string sTransformation = null;
        public string GetTransformation
        {
            get
            {
                return sTransformation;
            }
            set
            {
                sTransformation = value;
            }
        }
        public void SetHeader(string Value)
        {
            try
            {
                this.Text = "Change " + Value + " Unit";
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        private void Unit_IMXA_Shown(object sender, EventArgs e)
        {
            try
            { 
                //if(sUnitOld=="mm/s2" || sUnitOld=="cm/s2" || sUnitOld=="m/s2" || sUnitOld=="Gs" || sUnitOld=="gal")
                cmbUnits.Properties.Items.Clear();
                    switch (sUnitOld)
                    {
                        case "mm/s2":
                            {

                                cmbUnits.Properties.Items.AddRange(new object[] { "mm/s2", "cm/s2", "m/s2", "G", "gal","db" });

                                break;
                            }
                        case "cm/s2":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "cm/s2", "mm/s2", "m/s2", "G", "gal", "db" });

                                break;
                            }
                        case "m/s2":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "m/s2", "mm/s2", "cm/s2", "G", "gal", "db" });

                                break;
                            }
                        case "Hz":
                            {
                                cmbUnits.Properties.Items.AddRange(new object[] { "Hz", "CPM" });
                                break;
                            }
                        case "CPM":
                            {
                                cmbUnits.Properties.Items.AddRange(new object[] { "CPM", "Hz" });
                                break;
                            }
                        //case "Gs":
                        //    {
                        //        //cmbUnits.Properties.Items.Clear();
                        //        cmbUnits.Properties.Items.AddRange(new object[] { "Default", "mm/s2", "cm/s2", "m/s2", "gal" });

                        //        break;
                        //    }
                        case "g":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "G", "mm/s2", "cm/s2", "m/s2", "gal", "db" });

                                break;
                            }
                        case "G":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "G", "mm/s2", "cm/s2", "m/s2", "gal", "db" });

                                break;
                            }
                        case "gal":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "gal", "mm/s2", "m/s2", "G", "cm/s2", "db" });

                                break;
                            }


                        case "mm/s":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "mm/s", "cm/s", "m/s", "IPS", "ft/s", "db" });

                                break;
                            }
                        case "cm/s":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "cm/s", "mm/s", "m/s", "IPS", "ft/s", "db" });

                                break;
                            }
                        case "m/s":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "m/s", "cm/s", "mm/s", "IPS", "ft/s", "db" });

                                break;
                            }
                        //case "in/s":
                        //    {
                        //        //cmbUnits.Properties.Items.Clear();
                        //        cmbUnits.Properties.Items.AddRange(new object[] { "Default", "cm/s", "m/s", "mm/s", "ft/s" });

                        //        break;
                        //    }
                        case "IPS":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "IPS", "cm/s", "m/s", "mm/s", "ft/s", "db" });

                                break;
                            }
                        case "ft/s":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "ft/s", "cm/s", "m/s", "IPS", "mm/s", "db" });

                                break;
                            }
                        case "mm":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "mm", "Mils", "um", "db" });

                                break;
                            }
                        //case "mil":
                        //    {
                        //        //cmbUnits.Properties.Items.Clear();
                        //        cmbUnits.Properties.Items.AddRange(new object[] { "Default", "mm", "um" });

                        //        break;
                        //    }
                        case "Mils":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "Mils", "mm", "um", "db" });

                                break;
                            }
                        case "um":
                            {
                                //cmbUnits.Properties.Items.Clear();
                                cmbUnits.Properties.Items.AddRange(new object[] { "um", "Mils", "mm", "db" });

                                break;
                            }
                        default:
                            {
                                rbConvertTo.Checked = true;
                                break;
                            }

                    }
                    cmbUnits.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            try
            {
                if (this.Text.ToString() == "Change Y Unit")
                {
                   // cmbConvertTo.Properties.Items.Clear();
                    cmbConvertTo.Visible = true;
                    rbConvertTo.Visible = true;
                    

                }
            }
            catch (Exception ex)
            {
            }


        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            OkClicked = false;
            this.Close();
        }
        bool OkClicked = false;
        public bool IsOkClicked
        {
            get
            {
                return OkClicked;
            }
            set
            {
                OkClicked = value;
            }
        }
        public bool UnitSelected
        {
            get
            {
                return rbUnit.Checked;
            }
        }
        public bool ConversionSelected
        {
            get
            {
                return rbConvertTo.Checked;
            }
        }
        string FormulaToSend = "0";
        public string _Formula
        {
            get
            {
                return FormulaToSend;
            }           
        }
        string FormulaUnit = null;
        public string _FormulaUnit
        {
            get
            {
                return FormulaUnit;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbUnit.Checked)
                {
                    sUnitNew = cmbUnits.SelectedItem.ToString();
                    OkClicked = true;
                    this.Close();
                }
                else if (rbConvertTo.Checked)
                {
                    sUnitNew = cmbConvertTo.SelectedItem.ToString();
                    OkClicked = true;
                    this.Close();
                }
                else
                //{
                //    double factor = 0;
                //    FormulaToSend = "0";
                //    string Formula = tbFormula.Text.ToString();
                //    try
                //    {
                //        factor = Convert.ToDouble(Formula);

                //        FormulaToSend = cmbFExpression.SelectedItem.ToString() + factor.ToString();
                //        OkClicked = true;
                //        this.Close();
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBoxEx.Show("Enter Numeric Values Only");
                //    }
                //}
                {
                    if (!string.IsNullOrEmpty(tbFormula.Text.ToString()) && !string.IsNullOrEmpty(tbFormulaUnit.Text.ToString()))
                    {
                        double factor = 0;
                        FormulaToSend = "0";
                        FormulaUnit = null;
                        string Formula = tbFormula.Text.ToString();
                        bool bError = false;
                        double[] arrFValues = new double[0];
                        string[] arrFormula = Formula.Split(new string[] { "+", "-", "/", "*" }, StringSplitOptions.RemoveEmptyEntries);

                        {
                            for (int i = 0; i < arrFormula.Length; i++)
                            {
                                try
                                {
                                    factor = Convert.ToDouble(arrFormula[i].ToString());
                                    //Array.Resize(ref arrFValues, arrFValues.Length + 1);
                                    _ResizeArray.IncreaseArrayDouble(ref arrFValues, 1);
                                    arrFValues[arrFValues.Length - 1] = factor;

                                }
                                catch (Exception ex)
                                {
                                    ErrorLog_Class.ErrorLogEntry(ex);
                                    bError = true;
                                    break;
                                }
                            }

                            if (!bError)
                            {
                                string val = "0";
                                string FinalValue = null;
                                string PrevVal = null;
                                double DFinal = 0;
                                byte[] byteFormula = Encoding.ASCII.GetBytes(Formula);
                                int FValuesCtr = 0;
                                DFinal = arrFValues[0];
                                for (int i = 0; i < byteFormula.Length; i++)
                                {

                                    if (Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "+")//( Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "-" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "/" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "*")
                                    {
                                        FValuesCtr++;
                                        DFinal += (double)arrFValues[FValuesCtr];
                                    }
                                    if (Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "-")//( Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "-" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "/" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "*")
                                    {
                                        FValuesCtr++;
                                        DFinal -= (double)arrFValues[FValuesCtr];
                                    }
                                    if (Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "*")//( Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "-" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "/" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "*")
                                    {
                                        FValuesCtr++;
                                        DFinal *= (double)arrFValues[FValuesCtr];
                                    }
                                    if (Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "/")//( Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "-" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "/" || Encoding.ASCII.GetString(byteFormula, i, 1).ToString() == "*")
                                    {
                                        FValuesCtr++;
                                        DFinal /= (double)arrFValues[FValuesCtr];
                                    }
                                }
                                FormulaToSend = cmbFExpression.SelectedItem.ToString() + DFinal.ToString();
                                FormulaUnit = tbFormulaUnit.Text.ToString();

                                OkClicked = true;
                                this.Close();
                            }
                            else
                            {
                                MessageBoxEx.Show("Error in Formula");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbUnit.Checked)
                {
                    cmbUnits.Enabled = true;
                    tbFormula.Enabled = false;
                    cmbFExpression.Enabled = false;
                    tbFormulaUnit.Enabled = false;
                    cmbConvertTo.Enabled = false;
                    
                }
                
                
            }
            catch(Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbConvertTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbConvertTo.Checked)
                {
                    tbFormula.Enabled = false;
                    cmbUnits.Enabled = false;
                    cmbFExpression.Enabled = false;
                    tbFormulaUnit.Enabled = false;
                    cmbConvertTo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void rbFormula_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(rbFormula.Checked)
                {
                    tbFormula.Enabled = true;
                    cmbUnits.Enabled = false;
                    cmbFExpression.Enabled = true;
                    tbFormulaUnit.Enabled = true;
                    cmbConvertTo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        //private void btnSet_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnSetFormula.Visible=false;
        //        {
        //            btnUpdate.Location = new Point(209, 130);
        //            btnCancel.Location = new Point(276, 130);
        //            dgvFormula.Size = new Size(293, 43);
        //            string Formula = tbFormula.Text.ToString();
        //            dgvFormula.Rows.Clear();
        //            while( dgvFormula.Rows.Count>1)
        //            {
        //                dgvFormula.Rows.RemoveAt(0);
        //            }
        //            string[] arrFormula = Formula.Split(new string[] { "+", "-", "/", "*" }, StringSplitOptions.RemoveEmptyEntries);
        //            ArrayList FormulaVariables = new ArrayList();
        //            Hashtable HST = new Hashtable();
        //            try
        //            {
        //                for (int i = 0; i < arrFormula.Length; i++)
        //                {
        //                    try
        //                    {
        //                        double temp = Convert.ToDouble(arrFormula[i].ToString());
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        try
        //                        {
        //                            HST.Add(arrFormula[i].ToString(), i.ToString());
        //                            FormulaVariables.Add(arrFormula[i].ToString());
        //                        }
        //                        catch
        //                        {
        //                        }

        //                    }
        //                }
        //            }
        //            catch
        //            {
        //                MessageBoxEx.Show("Please enter simple Formulas only");
        //            }
                    
        //            for (int i = 0; i < FormulaVariables.Count; i++)
        //            {
        //                dgvFormula.Rows.Add(1);
        //                dgvFormula[0, i].Value = FormulaVariables[i].ToString();
        //                btnUpdate.Location = new Point(btnUpdate.Location.X, btnUpdate.Location.Y + 23);
        //                btnCancel.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y + 23);
        //                dgvFormula.Height = dgvFormula.Height + 23;
        //                this.Height = this.Height + 25;
        //                if (this.Height > 500)
        //                {
        //                    this.Height = 500;
        //                }
        //            }
        //            if (dgvFormula.Rows.Count > 1)
        //            {
        //                dgvFormula.Visible = true;
        //            }
        //            Button1 = new SimpleButton();
        //            Button1.Location = new Point(btnSetFormula.Location.X, btnUpdate.Location.Y);
        //            Button1.Text = "Set";
        //            Button1.Size = new Size(60, 23);
        //            this.Controls.Add(Button1);
        //            Button1.Click += new EventHandler(Button1_Click);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //    }
        //}
        //SimpleButton Button1 = new SimpleButton();
        //Hashtable VariablesWithValues = new Hashtable();
        //void Button1_Click(object sender, EventArgs e)
        //{
        //    VariablesWithValues = new Hashtable();
        //    try
        //    {
        //        for (int i = 0; i < dgvFormula.Rows.Count - 1; i++)
        //        {
        //            string sVariable = dgvFormula[0, i].Value.ToString();
        //            double dValue = Convert.ToDouble(dgvFormula[1, i].Value.ToString());
        //            VariablesWithValues.Add(sVariable, dValue);
        //        }
        //        Button1.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show("Please Enter Numeric Values Only");
        //    }
        //}
    }
}
