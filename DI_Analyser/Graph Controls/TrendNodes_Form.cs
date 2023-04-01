using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Analyser.Properties;
using DevComponents.DotNetBar;
using Analyser.Graph_Controls;
using System.Collections;

namespace Analyser.Forms
{
    public partial class TrendNodes_Form : UserControl
    {
        public TrendNodes_Form()
        {
            InitializeComponent();

            objlistimg.Images.Add(Resources.DarkRed);
            objlistimg.Images.Add(Resources.DarkGreen);
            objlistimg.Images.Add(Resources.DarkGoldenRod);
            objlistimg.Images.Add(Resources.DarkVoilet);
            objlistimg.Images.Add(Resources.DarkBlue);
            objlistimg.Images.Add(Resources.DimGrey);
            objlistimg.Images.Add(Resources.Chocolate);
            objlistimg.Images.Add(Resources.DarkKhaki);
            objlistimg.Images.Add(Resources.Black);
            objlistimg.Images.Add(Resources.Orange);
            objlistimg.Images.Add(Resources.Cyan);
            objlistimg.Images.Add(Resources.AquaMarine);
            objlistimg.Images.Add(Resources.Bisque);
            objlistimg.Images.Add(Resources.Blue);
            objlistimg.Images.Add(Resources.BlueViolet);
            objlistimg.Images.Add(Resources.Coral);
            objlistimg.Images.Add(Resources.Darkmagenta);
            objlistimg.Images.Add(Resources.DarkseaGreen);
            objlistimg.Images.Add(Resources.DarkSlateBlue);
            objlistimg.Images.Add(Resources.Deeppink);
            objlistimg.Images.Add(Resources.DodgerBlue);
            objlistimg.Images.Add(Resources.FireBrick);
            objlistimg.Images.Add(Resources.ForestGreen);
            objlistimg.Images.Add(Resources.GreenYellow);
            objlistimg.Images.Add(Resources.HotPink);
            objlistimg.Images.Add(Resources.IndianRed);
            objlistimg.Images.Add(Resources.Darkorange);
            objlistimg.Images.Add(Resources.Darkorchid);
            objlistimg.Images.Add(Resources.DeepSkyBlue);
            objlistimg.Images.Add(Resources.SandyBrown);
        }
        ImageList objlistimg = new ImageList();

        Bar objbar = null;
        public Bar _Bar
        {
            get
            {
                return objbar;
            }
            set
            {
                objbar = value;
            }
        }
        LineGraphControl objLineGraphControl = null;
        public LineGraphControl _LineGraphControl
        {
            get
            {
                return objLineGraphControl;
            }
            set
            {
                objLineGraphControl = value;
            }
        }
        public void AddNode(TreeListNode SelectedNode)
        {
            try
            {


            }
            catch (Exception ex)
            {
            }
        }
        public void AddNode(string SelectedNodepath)
        {
            string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };

            try
            {
                int trendValCtr = (dataGridViewX1.Rows.Count-1) % 30;
                dataGridViewX1.Rows.Add(1);
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[0].Value = SelectedNodepath;
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[1].Value = "√";
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[2].Value = objlistimg.Images[trendValCtr];
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[2].Tag = ColorCode[trendValCtr].ToString();

            }
            catch (Exception ex)
            {
            }
        }
        ArrayList FullXYData = null;
        ArrayList PartialXYData = null;
        ArrayList BrokenXYData = null;
        public ArrayList _FullXYData
        {
            get
            {
                return FullXYData;
            }
            set
            {
                FullXYData = value;
                PartialXYData = value;
                
            }
        }

       
       

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                BrokenXYData = new ArrayList();
                for (int i = 0; i < PartialXYData.Count; i++)
                {
                    BrokenXYData.Add((double[])PartialXYData[i]);
                }
                if (dataGridViewX1[1, e.RowIndex].Value == "√")
                {
                    double[] Tempdatax = (double[])FullXYData[2 * e.RowIndex];
                    double[] Tempdatay = (double[])FullXYData[(2 * e.RowIndex) + 1];
                    int datalength = Tempdatax.Length;
                    dataGridViewX1[1, e.RowIndex].Value = "X";
                    BrokenXYData[2 * e.RowIndex] = new double[datalength];
                    BrokenXYData[(2 * e.RowIndex) + 1] = new double[datalength];
                    //FullXYData[2 * e.RowIndex] = Tempdatax;
                    //FullXYData[(2 * e.RowIndex) + 1] = Tempdatay;
                }
                else
                {
                    dataGridViewX1[1, e.RowIndex].Value = "√";
                    BrokenXYData[2 * e.RowIndex] = FullXYData[2 * e.RowIndex];
                    BrokenXYData[(2 * e.RowIndex) + 1] = FullXYData[(2 * e.RowIndex) + 1];
                }
                ArrayList xdta = new ArrayList();
                ArrayList ydta = new ArrayList();
                PartialXYData = new ArrayList();
                for (int i = 0; i < BrokenXYData.Count; i++)
                {
                    PartialXYData.Add((double[])BrokenXYData[i]);
                }

                for (int i = 0; i < PartialXYData.Count / 2; i++)
                {
                    xdta.Add((double[])PartialXYData[2 * i]);
                    ydta.Add((double[])PartialXYData[(2 * i) + 1]);


                }
                _LineGraphControl.DrawLineGraph(xdta, ydta);
                _LineGraphControl.Refresh();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
