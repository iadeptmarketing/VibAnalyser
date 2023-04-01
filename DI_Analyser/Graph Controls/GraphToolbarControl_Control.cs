using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DI_Analyser;
using DevComponents.DotNetBar;
using Analyser.Forms;
using System.Collections;

namespace Analyser.Graph_Controls
{
    public partial class GraphToolbarControl_Control : UserControl
    {
        public GraphToolbarControl_Control()
        {
            InitializeComponent();
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

        _3DGraphControl obj3DGraphControl = null;
        public _3DGraphControl __3DGraphControl
        {
            get
            {
                return obj3DGraphControl;
            }
            set
            {
                obj3DGraphControl = value;
            }
        }

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

        MainGraph_Control objMainGraphControl = null;
        public MainGraph_Control _MainGraphControl
        {
            get
            {
                return objMainGraphControl;
            }
            set
            {
                objMainGraphControl = value;
            }
        }
        private void BackGroundChanges()
        {
            try
            {
                _LineGraphControl.BackGroundChanges();
                _LineGraphControl.Refresh();
            }
            catch (Exception ex)
            {
            }

        }
        Bar TrendBar = null;
        public Bar _TrendBar
        {
            get
            {
                return TrendBar;
            }
            set
            {
                TrendBar = value;
            }
        }

        Bar WaterFallBar = null;
        public Bar _WaterFallBar
        {
            get
            {
                return WaterFallBar;
            }
            set
            {
                WaterFallBar = value;
            }
        }


        TrendNodes_Form objTreeNodeForm = null;

        public TrendNodes_Form _TrendNodeForm
        {
            get
            {
                return objTreeNodeForm;
            }
            set
            {
                objTreeNodeForm = value;
            }
        }
        WaterFallNodes_FormControl1 objWaterfallNodeForm = null;
        public WaterFallNodes_FormControl1 _WaterfallNodeForm
        {
            get
            {
                return objWaterfallNodeForm;
            }
            set
            {
                objWaterfallNodeForm = value;
            }
        }
        //private void CreateTrend(Bar Nextbar, Bar CurrentBar)
        //{
        //    try
        //    {

        //        _MainGraphControl.RedrawMainGraph(Nextbar);
        //        _LineGraphControl.AllowDrop = true;
        //        string LGCName = _LineGraphControl.Name.ToString();
        //        _TrendNodeForm = new TrendNodes_Form();
        //        _TrendNodeForm._Bar = Nextbar;
        //        _TrendNodeForm._LineGraphControl = _LineGraphControl;
        //        _TrendNodeForm.AddNode(_Form1.GetPath(_Form1.MainTreelist.FocusedNode).ToString().TrimEnd(new char[] { '\\' }).ToString());
        //        _LineGraphControl._TrendNodeForm = _TrendNodeForm;
        //        _LineGraphControl._XYDATA = _MainGraphControl.XYDATA;
        //        //_TrendNodeForm.Show();
        //        _TrendNodeForm.Dock = DockStyle.Right;
        //        _LineGraphControl._DataGrid_Control.Controls.Add(_TrendNodeForm);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //private void CreateTrend(Bar Nextbar)
        //{
        //    try
        //    {

        //        _MainGraphControl.RedrawMainGraph(Nextbar);
        //        _LineGraphControl.AllowDrop = true;
        //        string LGCName = _LineGraphControl.Name.ToString();
        //        _TrendNodeForm = new TrendNodes_Form();
        //        _TrendNodeForm._Bar = Nextbar;
        //        _TrendNodeForm._LineGraphControl = _LineGraphControl;
        //        _TrendNodeForm.AddNode(_Form1.GetPath(_Form1.MainTreelist.FocusedNode).ToString().TrimEnd(new char[] { '\\' }).ToString());
        //        _LineGraphControl._TrendNodeForm = _TrendNodeForm;
        //        _LineGraphControl._XYDATA = _MainGraphControl.XYDATA;
        //        //_TrendNodeForm.Show();
        //        _TrendNodeForm.Dock = DockStyle.Right;
        //        _LineGraphControl._DataGrid_Control.Controls.Add(_TrendNodeForm);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //private void CreateWaterfall(Bar Nextbar)
        //{
        //    try
        //    {
        //        _MainGraphControl.RedrawMainWaterfallGraph(Nextbar);
        //        __3DGraphControl.AllowDrop = true;
        //        string WFCName = __3DGraphControl.Name.ToString();
        //        _WaterfallNodeForm = new WaterFallNodes_FormControl1();
        //        _WaterfallNodeForm._Bar = Nextbar;
        //        _WaterfallNodeForm._Waterfall = __3DGraphControl;
        //        _WaterfallNodeForm.AddNode(_Form1.GetPath(_Form1.MainTreelist.FocusedNode).ToString().TrimEnd(new char[] { '\\' }).ToString());
        //        __3DGraphControl._WaterFallNodeForm = _WaterfallNodeForm;
        //        __3DGraphControl._XYDATA = _MainGraphControl.XYDATA;
        //        _WaterfallNodeForm.Dock = DockStyle.Right;
        //        __3DGraphControl._DataGrid_Control.Controls.Add(_WaterfallNodeForm);
                
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //private void CreateWaterfall(Bar Nextbar,Bar Currentbar)
        //{
        //    try
        //    {
        //        _MainGraphControl.RedrawMainGraph(Nextbar);
        //        _MainGraphControl.RedrawMainWaterfallGraph(Currentbar);
        //        __3DGraphControl.AllowDrop = true;
        //        string WFCName = __3DGraphControl.Name.ToString();
        //        _WaterfallNodeForm = new WaterFallNodes_FormControl1();
        //        _WaterfallNodeForm._Bar = Currentbar;
        //        _WaterfallNodeForm._Waterfall = __3DGraphControl;
        //        _WaterfallNodeForm.AddNode(_Form1.GetPath(_Form1.MainTreelist.FocusedNode).ToString().TrimEnd(new char[] { '\\' }).ToString());
        //        __3DGraphControl._WaterFallNodeForm = _WaterfallNodeForm;
        //        __3DGraphControl._XYDATA = _MainGraphControl.XYDATA;
        //        _WaterfallNodeForm.Dock = DockStyle.Right;
        //        __3DGraphControl._DataGrid_Control.Controls.Add(_WaterfallNodeForm);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //Graph Items
        private void biGBgroundDirectionH_Click(object sender, EventArgs e)
        {
            try
            {
                _LineGraphControl._GraphBGDir = 0;
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private void biGBgroundDirectionV_Click(object sender, EventArgs e)
        {
            try
            {
                _LineGraphControl._GraphBGDir = 1;
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }
        
        private void cpGBgroundColor1_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            try
            {
                byte ColorA = e.Color.A;
                byte ColorR = e.Color.R;
                byte ColorG = e.Color.G;
                byte ColorB = e.Color.B;
                _LineGraphControl._GraphBG1 = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }
   
        private void cpGBgroundColor2_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            byte ColorA = e.Color.A;
            byte ColorR = e.Color.R;
            byte ColorG = e.Color.G;
            byte ColorB = e.Color.B;
            _LineGraphControl._GraphBG2 = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);
            BackGroundChanges();
        }

        //Chart Items
        private void biCBgroundDirectionH_Click(object sender, EventArgs e)
        {
            try
            {
                _LineGraphControl._ChartBGDir = 0;
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private void biCBgroundDirectionV_Click(object sender, EventArgs e)
        {
            try
            {
                _LineGraphControl._ChartBGDir = 1;
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private void cpCBgroundColor1_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            try
            {
                byte ColorA = e.Color.A;
                byte ColorR = e.Color.R;
                byte ColorG = e.Color.G;
                byte ColorB = e.Color.B;
                _LineGraphControl._ChartBG1 = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
            
        }

        private void cpCBgroundColor2_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            try
            {
                byte ColorA = e.Color.A;
                byte ColorR = e.Color.R;
                byte ColorG = e.Color.G;
                byte ColorB = e.Color.B;
                _LineGraphControl._ChartBG2 = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }

        //Cursor
        private void cpCursorColor_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            try
            {
                byte ColorA = e.Color.A;
                byte ColorR = e.Color.R;
                byte ColorG = e.Color.G;
                byte ColorB = e.Color.B;
                _LineGraphControl._MainCursorColor = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);                
            }
            catch (Exception ex)
            {
            }
        }

        //Axis
        private void cpAxisColor_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            try
            {
                byte ColorA = e.Color.A;
                byte ColorR = e.Color.R;
                byte ColorG = e.Color.G;
                byte ColorB = e.Color.B;
                _LineGraphControl._AxisColor = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);
                BackGroundChanges();
            }
            catch (Exception ex)
            {
            }
        }



        //Markers
        private void biFaultFreq_Click(object sender, EventArgs e)
        {
            string[] Frequencies = new string[0];
            try
            {
                biFaultFreq.Checked = !biFaultFreq.Checked;
                if (biFaultFreq.Checked)
                {
                    if (biBandAlarm.Checked)
                    {
                        biBandAlarm.Checked = false;
                    }
                    if (biRPM.Checked)
                    {
                        biRPM.Checked = false;
                    }
                    if (biBFF.Checked)
                    {
                        biBFF.Checked = false;
                    }



                    //if (!CheckForTimeData(y))
                    //{
                    for (int i = 0; i < _Form1._FaultFrequencyDataGrid.RowCount - 1; i++)
                    {
                        Array.Resize(ref Frequencies, Frequencies.Length + 1);

                        Frequencies[Frequencies.Length - 1] = _Form1._FaultFrequencyDataGrid.Rows[i].Cells[0].Value.ToString() + "=" + _Form1._FaultFrequencyDataGrid.Rows[i].Cells[1].Value.ToString();
                    }
                    //}

                }

                bool bReturned = _LineGraphControl.DrawFaultFrequencies(biFaultFreq.Checked, Frequencies);
                if (!bReturned)
                {
                    biFaultFreq.Checked = false;
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void biRPM_Click(object sender, EventArgs e)
        {

        }

        private void biBFF_Click(object sender, EventArgs e)
        {

        }

        private void biBandAlarm_Click(object sender, EventArgs e)
        {

        }



        //Zoom
        private void biZoom_Click(object sender, EventArgs e)
        {
            try
            {
                biZoom.Checked = !biZoom.Checked;
                if (biZoom.Checked)
                {
                    //bcmCursors.Enabled = false;
                    //bbSBValue.Enabled = false;
                    //bbSBRatio.Enabled = false;
                    //bbSBTrend.Enabled = false;
                    //bCBBand.Enabled = false;
                    //bCBFault.Enabled = false;
                    //bCBBearingFault.Enabled = false;
                    //bCBRPM.Enabled = false;
                    _LineGraphControl.StartZoom();
                }
                else
                {
                    //bcmCursors.Enabled = true;
                    //bbSBValue.Enabled = true;
                    //bbSBRatio.Enabled = true;
                    //bbSBTrend.Enabled = true;
                    //bCBBand.Enabled = true;
                    //bCBFault.Enabled = true;
                    //bCBBearingFault.Enabled = true;
                    //bCBRPM.Enabled = true;
                    _LineGraphControl.StopZoom();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void biUnZoom_Click(object sender, EventArgs e)
        {
            try
            {
                _LineGraphControl.Unzoom();
                if (biZoom.Checked)
                {
                    biZoom.Checked = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Cursors
        private void biSingle_Click(object sender, EventArgs e)
        {

            //_Form1.CmbCursorSelectedItem(biSingle.Text.ToString());
            
            _LineGraphControl.SetCursorType("Single");
        }

        private void biSquare_Click(object sender, EventArgs e)
        {
            
            _LineGraphControl.SetCursorType("Single With Square");
        }

        private void biCrossHair_Click(object sender, EventArgs e)
        {
            
            _LineGraphControl.SetCursorType("Cross Hair");
        }

        private void biHarmonic_Click(object sender, EventArgs e)
        {
       
            _LineGraphControl.SetCursorType("Harmonic");
        }

        private void biSideBand_Click(object sender, EventArgs e)
        {

            //_LineGraphControl.SetCursorType(SlctedCursor);
        }

        private void biSBRatio_Click(object sender, EventArgs e)
        {

           // _LineGraphControl.SetCursorType(SlctedCursor);
        }

        private void biSBTrend_Click(object sender, EventArgs e)
        {

           // _LineGraphControl.SetCursorType(SlctedCursor);
        }

        private void biPeek_Click(object sender, EventArgs e)
        {

           // _LineGraphControl.SetCursorType(SlctedCursor);
        }



        //Display

        private void biAreaGraph_Click(object sender, EventArgs e)
        {
            try
            {
                biAreaGraph.Checked = !biAreaGraph.Checked;
                if (biAreaGraph.Checked)
                {
                    _LineGraphControl._AreaFill = true;
                }
                else
                {
                    _LineGraphControl._AreaFill = false;
                }

                bool bReturned = _LineGraphControl.AreaGraph();
                if (!bReturned)
                {
                    biAreaGraph.Checked = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void biTrend_Click(object sender, EventArgs e)
        {
            try
            {
                biTrend.Checked = !biTrend.Checked;
                _Form1.NullComparisionVariables();
                if (biTrend.Checked)
                {
                    string ToolbarName = this.Name.ToString();
                    _Form1.getoriginalCSVvalues();
                    _Form1.SetIsTrend = true;

                    switch (ToolbarName)
                    {
                        case "graphToolbarControl_Control1":
                            {
                                _TrendBar = _MainGraphControl._Bar1;
                                break;
                            }
                        case "graphToolbarControl_Control2":
                            {
                                _TrendBar = _MainGraphControl._Bar2;
                                break;
                            }
                        case "graphToolbarControl_Control3":
                            {
                                _TrendBar = _MainGraphControl._Bar3;
                                break;
                            }
                        case "graphToolbarControl_Control4":
                            {
                                _TrendBar = _MainGraphControl._Bar4;
                                break;
                            }
                    }

                    if (_MainGraphControl._Bar1.Visible)
                    {
                        if (_MainGraphControl._Bar2.Visible)
                        {
                            if (_MainGraphControl._Bar3.Visible)
                            {
                                if (_MainGraphControl._Bar4.Visible)
                                {
                                    MessageBoxEx.Show("More than 4 graph windows not allowed");
                                }
                                else
                                {
                                    CreateTrend(_MainGraphControl._Bar4);
                                    _MainGraphControl._Bar4.Visible = true;
                                   // _TrendBar = _MainGraphControl._Bar3;

                                }
                            }
                            else
                            {
                                CreateTrend(_MainGraphControl._Bar3);
                                _MainGraphControl._Bar3.Visible = true;
                               // _TrendBar = _MainGraphControl._Bar2;
                            }
                        }
                        else
                        {
                            CreateTrend(_MainGraphControl._Bar2);
                            _MainGraphControl._Bar2.Visible = true;
                           // _TrendBar = _MainGraphControl._Bar1;
                        }
                    }
                    else
                    {
                        CreateTrend(_MainGraphControl._Bar1);
                        _MainGraphControl._Bar1.Visible = true;
                    }
                   
                }
                else
                {
                    _TrendNodeForm = null;
                    _Form1.SetIsTrend = false;
                    _Form1._DroppedTable = new Hashtable();
                    _LineGraphControl.AllowDrop = false;
                    for (int i = 0; i < _LineGraphControl._DataGrid_Control.Controls.Count; i++)
                    {
                        if (_LineGraphControl._DataGrid_Control.Controls[i].Name.ToString() == "TrendNodes_Form")
                        {
                            _LineGraphControl._DataGrid_Control.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    
                    _LineGraphControl.Refresh();
                    _TrendBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void biWaterfall_Click(object sender, EventArgs e)
        {
            try
            {
                biWaterfall.Checked = !biWaterfall.Checked;
                _Form1.NullComparisionVariables();
                if (biWaterfall.Checked)
                {
                    string ToolbarName = this.Name.ToString();
                    _Form1.getoriginalCSVvalues();
                    _Form1.SetIsWaterFall = true;

                    switch (ToolbarName)
                    {
                        case "graphToolbarControl_Control1":
                            {
                                _WaterFallBar = _MainGraphControl._Bar1;
                                break;
                            }
                        case "graphToolbarControl_Control2":
                            {
                                _WaterFallBar = _MainGraphControl._Bar2;
                                break;
                            }
                        case "graphToolbarControl_Control3":
                            {
                                _WaterFallBar = _MainGraphControl._Bar3;
                                break;
                            }
                        case "graphToolbarControl_Control4":
                            {
                                _WaterFallBar = _MainGraphControl._Bar4;
                                break;
                            }
                    }
                    if (_MainGraphControl._Bar1.Visible)
                    {
                        if (_MainGraphControl._Bar2.Visible)
                        {
                            if (_MainGraphControl._Bar3.Visible)
                            {
                                if (_MainGraphControl._Bar4.Visible)
                                {
                                    MessageBoxEx.Show("More than 4 graph windows not allowed");
                                }
                                else
                                {
                                    CreateWaterfall(_MainGraphControl._Bar4, _WaterFallBar);
                                    _MainGraphControl._Bar4.Visible = true;
                                    // _TrendBar = _MainGraphControl._Bar3;

                                }
                            }
                            else
                            {
                                CreateWaterfall(_MainGraphControl._Bar3, _WaterFallBar);
                                _MainGraphControl._Bar3.Visible = true;
                                // _TrendBar = _MainGraphControl._Bar2;
                            }
                        }
                        else
                        {
                            CreateWaterfall(_MainGraphControl._Bar2, _WaterFallBar);
                            _MainGraphControl._Bar2.Visible = true;
                            // _TrendBar = _MainGraphControl._Bar1;
                        }
                    }
                    else
                    {
                        CreateWaterfall(_MainGraphControl._Bar1, _WaterFallBar);
                        _MainGraphControl._Bar1.Visible = true;
                    }

                }
                else
                {
                    _WaterfallNodeForm = null;
                    _Form1.SetIsWaterFall = false;
                    _Form1._DroppedWaterfallTable = new Hashtable();
                    __3DGraphControl.AllowDrop = false;
                    for (int i = 0; i < __3DGraphControl._DataGrid_Control.Controls.Count; i++)
                    {
                        if (__3DGraphControl._DataGrid_Control.Controls[i].Name.ToString() == "WaterFallNodes_FormControl1")
                        {
                            __3DGraphControl._DataGrid_Control.Controls.RemoveAt(i);
                            break;
                        }
                    }

                    __3DGraphControl.Refresh();
                    _WaterFallBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        

        

    }
}
