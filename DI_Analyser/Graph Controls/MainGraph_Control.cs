using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Collections;
using DI_Analyser;

namespace Analyser.Graph_Controls
{
    public partial class MainGraph_Control : UserControl
    {
        public MainGraph_Control()
        {
            InitializeComponent();
        }


        //public delegate void SelectIt();
        //public event SelectIt
        Form1 objForm1 = null;
        public Form1 _Form1
        {
            get
            {
                return objForm1;
            }
            set
            {
                objForm1 = value;
            }
        }

        public Bar _Bar1
        {
            get
            {
                return bar1;
            }            
        }
        public Bar _Bar2
        {
            get
            {
                return bar2;
            }
        }
        public Bar _Bar3
        {
            get
            {
                return bar3;
            }
        }
        public Bar _Bar4
        {
            get
            {
                return bar4;
            }
        }

        ArrayList _XYDATA = null;
        public ArrayList XYDATA
        {
            get
            {
                return _XYDATA;
            }            
        }


        internal void DrawLineGraphs(int GraphCount, ArrayList XYData)
        {

            try
            {
                _XYDATA = XYData;
                if (GraphCount == 1)
                {
                    bar1.Visible = true;
                    bar2.Visible = false;
                    bar3.Visible = false;
                    bar4.Visible = false;
                }
                else if(GraphCount == 2)
                {
                    //bar1
                    bar3.Visible = false;
                    bar4.Visible = false;
                }
                else if (GraphCount == 3)
                {
                    bar4.Visible = false;
                }
               

                for (int i = 0; i < GraphCount; i++)
                {
                    double[] xData = (double[])XYData[2 * i];
                    double[] yData = (double[])XYData[(2 * i) + 1];

                    LineGraphControl _LineGraph = new LineGraphControl();
                    _LineGraph.Name = "LineGraph" + i.ToString();
                    
                    _LineGraph.Dock = DockStyle.Fill;
                    _LineGraph.DrawLineGraph(xData, yData);

                    switch (i)
                    {
                        case 0:
                            {
                                //bar1.Visible = true;
                                
                                panelEx1.Controls.Clear();
                                graphToolbarControl_Control1._Form1 = _Form1;                                
                                graphToolbarControl_Control1._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control1._LineGraphControl._DataGridView = dataGrid_Control1._DataGridView;
                                graphToolbarControl_Control1._LineGraphControl._DataGrid_Control = dataGrid_Control1;
                                graphToolbarControl_Control1._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control1._MainGraphControl = this;
                                panelEx1.Controls.Add(_LineGraph);
                                break;
                            }
                        case 1:
                            {
                                //bar2.Visible = true;
                                //if (PDC2.Controls.Count > 1)
                                //{                                  
                                //    for (int j = 0; j < PDC2.Controls.Count; j++)
                                //    {
                                //        Control _control = PDC2.Controls[j];
                                //        if (_control.Name.ToString().Contains("LineGraph"))
                                //        {
                                //            PDC2.Controls.Remove(_control);
                                //        }
                                //        else
                                //        {
                                //            graphToolbarControl_Control2._Form1 = null;
                                //            graphToolbarControl_Control2._LineGraphControl = null;
                                //        }
                                //    }
                                //}
                                panelEx2.Controls.Clear();
                                graphToolbarControl_Control2._Form1 = _Form1;
                                graphToolbarControl_Control2._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control2._LineGraphControl._DataGridView = dataGrid_Control2._DataGridView;
                                graphToolbarControl_Control2._LineGraphControl._DataGrid_Control = dataGrid_Control2;
                                graphToolbarControl_Control2._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control2._MainGraphControl = this;
                                panelEx2.Controls.Add(_LineGraph);
                                break;
                            }
                        case 2:
                            {
                                //bar3.Visible = true;
                                //if (PDC3.Controls.Count > 1)
                                //{                                    
                                //    for (int j = 0; j < PDC3.Controls.Count; j++)
                                //    {
                                //        Control _control = PDC3.Controls[j];
                                //        if (_control.Name.ToString().Contains("LineGraph"))
                                //        {
                                //            PDC3.Controls.Remove(_control);
                                //        }
                                //        else
                                //        {
                                //            graphToolbarControl_Control3._Form1 = null;
                                //            graphToolbarControl_Control3._LineGraphControl = null;
                                //        }
                                //    }
                                //}
                                panelEx3.Controls.Clear();
                                graphToolbarControl_Control3._Form1 = _Form1;
                                graphToolbarControl_Control3._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control3._LineGraphControl._DataGridView = dataGrid_Control3._DataGridView;
                                graphToolbarControl_Control3._LineGraphControl._DataGrid_Control = dataGrid_Control3;
                                graphToolbarControl_Control3._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control3._MainGraphControl = this;
                                panelEx3.Controls.Add(_LineGraph);
                                break;
                            }
                        case 3:
                            {
                                //bar4.Visible = true;
                                //if (PDC4.Controls.Count > 1)
                                //{                                   
                                //    for (int j = 0; j < PDC4.Controls.Count; j++)
                                //    {
                                //        Control _control = PDC4.Controls[j];
                                //        if (_control.Name.ToString().Contains("LineGraph"))
                                //        {
                                //            PDC4.Controls.Remove(_control);
                                //        }
                                //        else
                                //        {
                                //            graphToolbarControl_Control4._Form1 = null;
                                //            graphToolbarControl_Control4._LineGraphControl = null;
                                //        }
                                //    }
                                //}
                                panelEx4.Controls.Clear();
                                graphToolbarControl_Control4._Form1 = _Form1;
                                graphToolbarControl_Control4._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control4._LineGraphControl._DataGridView = dataGrid_Control4._DataGridView;
                                graphToolbarControl_Control4._LineGraphControl._DataGrid_Control = dataGrid_Control4;
                                graphToolbarControl_Control4._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control4._MainGraphControl = this;
                                panelEx4.Controls.Add(_LineGraph);
                                break;
                            }
                    }


                    

                }




            }
            catch (Exception ex)
            {
            }





            //int BarCount = dotNetBarManager1.Bars.Count;
            //if (BarCount > 1)
            //{
            //    for (int i = 0; dotNetBarManager1.Bars.Count > 1; i++)
            //    {
            //        string name = dotNetBarManager1.Bars[i].Name;
            //        Bar _Bar = dotNetBarManager1.Bars[i];
            //        if (name != "bar1")
            //        {
            //            dotNetBarManager1.Bars.Remove(_Bar);
            //            i--;
            //        }
            //        else
            //        {
            //            int itemCount = _Bar.Items.Count;// .Controls.Count;
            //            if (itemCount > 1)
            //            {

            //                for (int j = 0; _Bar.Items.Count > 1; j++)
            //                {
            //                    _Bar.Items.RemoveAt(1);
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    Bar _Bar = dotNetBarManager1.Bars[0];
            //    int itemCount = _Bar.Items.Count;// .Controls.Count;
            //    if (itemCount > 1)
            //    {

            //        for (int j = 0; _Bar.Items.Count > 1; j++)
            //        {
            //            _Bar.Items.RemoveAt(1);
            //        }
            //    }
            //}
            //for (int i = 0; i < GraphCount; i++)
            //{
            //    double[] xData=(double[])XYData[2*i];
            //    double[] yData=(double[])XYData[(2*i)+1];

            //    GraphToolbarControl_Control GraphToolbar = new GraphToolbarControl_Control();
            //    GraphToolbar.Name = "GTBar" + i.ToString();
            //    GraphToolbar.Dock = DockStyle.Top;
                
            //    PanelDockContainer _pcd = new PanelDockContainer();
            //    _pcd.Name = "pcd" + i.ToString();
            //    _pcd.ColorSchemeStyle = eDotNetBarStyle.Office2007;
            //    //_pcd.Enter -= new EventHandler(_pcd_Enter);
            //    _pcd.Enter += new EventHandler(_pcd_Enter);
            //    _pcd.Click += new EventHandler(_pcd_Click);
            //    DockContainerItem dockContainerItem2 = new DockContainerItem();//"dockContainerItem"+i.ToString(), "dockContainerItem2");
            //    dockContainerItem2.Name="DockCItem"+i.ToString();
            //    dockContainerItem2.Text="Graph"+i.ToString();
            //    dockContainerItem2.Click += new EventHandler(dockContainerItem2_Click);
            //    dockContainerItem2.GotFocus += new EventHandler(dockContainerItem2_GotFocus);
            //    _pcd.Dock = DockStyle.Fill;
            //    //_pcd.Controls.Add(GraphToolbar);
            //    LineGraphControl _LineGraph = new LineGraphControl();
            //    _LineGraph.Name = "LineGraph" + i.ToString();
            //    _LineGraph.Dock = DockStyle.Fill;
            //    _LineGraph.DrawLineGraph(xData, yData);
            //    _pcd.Controls.Add(_LineGraph);



            //    this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] { dockContainerItem2 });
            //    this.bar1.Controls.Add(_pcd);

            //}
        }



        internal void RedrawMainWaterfallGraph(Bar bar)
        {
            try
            {
                _3DGraphControl obj3DGraphControl = new _3DGraphControl();
                obj3DGraphControl.Dock = DockStyle.Fill;

                obj3DGraphControl.DrawWaterfallGraph((double[])_XYDATA[0], (double[])_XYDATA[1]);

                switch (bar.Name.ToString())
                {
                    case "bar1":
                        {
                            obj3DGraphControl.Name = "LineGraph0";
                            panelEx1.Controls.Clear();
                            graphToolbarControl_Control1._Form1 = _Form1;
                            graphToolbarControl_Control1.__3DGraphControl = obj3DGraphControl;
                            graphToolbarControl_Control1.__3DGraphControl._DataGridView = dataGrid_Control1._DataGridView;
                            graphToolbarControl_Control1.__3DGraphControl._DataGrid_Control = dataGrid_Control1;
                            graphToolbarControl_Control1.__3DGraphControl._MainForm = _Form1;
                            graphToolbarControl_Control1._MainGraphControl = this;
                            panelEx1.Controls.Add(obj3DGraphControl);
                            break;
                        }
                    case "bar2":
                        {
                            obj3DGraphControl.Name = "LineGraph1";
                            panelEx2.Controls.Clear();
                            graphToolbarControl_Control2._Form1 = _Form1;
                            graphToolbarControl_Control2.__3DGraphControl = obj3DGraphControl;
                            graphToolbarControl_Control2.__3DGraphControl._DataGridView = dataGrid_Control2._DataGridView;
                            graphToolbarControl_Control2.__3DGraphControl._DataGrid_Control = dataGrid_Control2;
                            graphToolbarControl_Control2.__3DGraphControl._MainForm = _Form1;
                            graphToolbarControl_Control2._MainGraphControl = this;
                            panelEx2.Controls.Add(obj3DGraphControl);
                            break;
                        }
                    case "bar3":
                        {
                            obj3DGraphControl.Name = "LineGraph2";
                            panelEx3.Controls.Clear();
                            graphToolbarControl_Control3._Form1 = _Form1;
                            graphToolbarControl_Control3.__3DGraphControl = obj3DGraphControl;
                            graphToolbarControl_Control3.__3DGraphControl._DataGridView = dataGrid_Control3._DataGridView;
                            graphToolbarControl_Control3.__3DGraphControl._DataGrid_Control = dataGrid_Control3;
                            graphToolbarControl_Control3.__3DGraphControl._MainForm = _Form1;
                            graphToolbarControl_Control3._MainGraphControl = this;
                            panelEx3.Controls.Add(obj3DGraphControl);
                            break;
                        }
                    case "bar4":
                        {
                            obj3DGraphControl.Name = "LineGraph3";
                            panelEx4.Controls.Clear();
                            graphToolbarControl_Control4._Form1 = _Form1;
                            graphToolbarControl_Control4.__3DGraphControl = obj3DGraphControl;
                            graphToolbarControl_Control4.__3DGraphControl._DataGridView = dataGrid_Control4._DataGridView;
                            graphToolbarControl_Control4.__3DGraphControl._DataGrid_Control = dataGrid_Control4;
                            graphToolbarControl_Control4.__3DGraphControl._MainForm = _Form1;
                            graphToolbarControl_Control4._MainGraphControl = this;
                            panelEx4.Controls.Add(obj3DGraphControl);
                            break;
                        }
                }



            }
            catch (Exception ex)
            {
            }
        }
        internal void RedrawMainGraph(Bar bar)
        {
            try
            {
                    LineGraphControl _LineGraph = new LineGraphControl();
                    _LineGraph.Dock = DockStyle.Fill;
                    
                    _LineGraph.DrawLineGraph((double[])_XYDATA[0], (double[])_XYDATA[1]);

                    switch (bar.Name.ToString())
                    {
                        case "bar1":
                            {
                                _LineGraph.Name = "LineGraph0";
                                panelEx1.Controls.Clear();
                                graphToolbarControl_Control1._Form1 = _Form1;                                
                                graphToolbarControl_Control1._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control1._LineGraphControl._DataGridView = dataGrid_Control1._DataGridView;
                                graphToolbarControl_Control1._LineGraphControl._DataGrid_Control = dataGrid_Control1;
                                graphToolbarControl_Control1._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control1._MainGraphControl = this;
                                panelEx1.Controls.Add(_LineGraph);
                                break;
                            }
                        case "bar2":
                            {
                                _LineGraph.Name = "LineGraph1";
                                panelEx2.Controls.Clear();
                                graphToolbarControl_Control2._Form1 = _Form1;
                                graphToolbarControl_Control2._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control2._LineGraphControl._DataGridView = dataGrid_Control2._DataGridView;
                                graphToolbarControl_Control2._LineGraphControl._DataGrid_Control = dataGrid_Control2;
                                graphToolbarControl_Control2._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control2._MainGraphControl = this;
                                panelEx2.Controls.Add(_LineGraph);
                                break;
                            }
                        case "bar3":
                            {
                                _LineGraph.Name = "LineGraph2";
                                panelEx3.Controls.Clear();
                                graphToolbarControl_Control3._Form1 = _Form1;
                                graphToolbarControl_Control3._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control3._LineGraphControl._DataGridView = dataGrid_Control3._DataGridView;
                                graphToolbarControl_Control3._LineGraphControl._DataGrid_Control = dataGrid_Control3;
                                graphToolbarControl_Control3._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control3._MainGraphControl = this;
                                panelEx3.Controls.Add(_LineGraph);
                                break;
                            }
                        case "bar4":
                            {
                                _LineGraph.Name = "LineGraph3";
                                panelEx4.Controls.Clear();
                                graphToolbarControl_Control4._Form1 = _Form1;
                                graphToolbarControl_Control4._LineGraphControl = _LineGraph;
                                graphToolbarControl_Control4._LineGraphControl._DataGridView = dataGrid_Control4._DataGridView;
                                graphToolbarControl_Control4._LineGraphControl._DataGrid_Control = dataGrid_Control4;
                                graphToolbarControl_Control4._LineGraphControl._MainForm = _Form1;
                                graphToolbarControl_Control4._MainGraphControl = this;
                                panelEx4.Controls.Add(_LineGraph);
                                break;
                            }
                    }


                    
            }
            catch (Exception ex)
            {
            }
        }
    }
}
