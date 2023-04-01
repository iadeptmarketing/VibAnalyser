using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using com.iAM.chart3dnet;
using System.Drawing.Drawing2D;
using DI_Analyser;
using DevComponents.DotNetBar.Controls;
using Analyser.Properties;
using DevComponents.DotNetBar;
using Analyser.interfaces;
using Analyser.Classes;

namespace Analyser.Graph_Controls
{
    public partial class _3DGraphControl : ChartView
    {
        public _3DGraphControl()
        {
            InitializeComponent();

            this.ThisMouseMove+=new MouseMoveHandler(_3DGraphControl_ThisMouseMove);

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
        string XLabel = "X Axis";
        string YLabel = "Y Axis";
        LinearAxis xAxis2;
        LinearAxis yAxis2;
        Grid xgrid2;
        Grid ygrid2;
        NumericAxisLabels xAxisLab2;
        NumericAxisLabels yAxisLab2;
        AxisTitle yaxistitle2;
        AxisTitle xaxistitle2;
        Color GraphBG1 = Color.White;
        Color GraphBG2 = Color.White;
        Color ChartBG1 = Color.White;
        Color ChartBG2 = Color.White;
        Color AxisColor = Color.Black;
        int GraphBGDir = 0;
        int ChartBGDir = 0;
        Marker[] arrmarker = null;
        Marker FaultFrequencyMarkers = null;
        Marker m_objMarker = null;
        Marker m_objNewMarker = null;
        Marker[] arrmarkerCursor = null;
        ChartText[] arrChartText = null;
        ChartText[] arrChartTextCursor = null;
        SimpleLinePlot m_objSelectedPlot = null;
        SimpleLinePlot m_objNewPlot = null;
        SimpleLinePlot m_objSelectedPlotForCursor = null;
        SimpleLinePlot m_objClickedPlot = null;
        SimpleLinePlot m_objOldSelectedPlot = null;
        public delegate void MouseMoveHandler(MouseEventArgs e);
        public event MouseMoveHandler ThisMouseMove;
        public delegate void GraphClickedHandler(MouseEventArgs e);
        public event GraphClickedHandler GraphClicked;
        //int m_iCounter = 0;
        DataCursor m_objDataCursor = null;
        string SelectedCursor = null;
        SimpleDataset Dataset2;
        ChartView chartVu;
        Font theFont;
        SimpleLinePlot thePlot1;
        Background background;
        Background plotbackground;
        CartesianCoordinates pTransform1;
        int i;
        int j;
        ChartZoom zoomObj = null;
        Color MainCursor = Color.Black;
        ChartAttribute attrib2;
        string sColorTag = "7667712";
        bool bAreaFill = false;
        ArrayList XYDATA = new ArrayList();
        DataGridViewX objDataGridView1 = null;
        Form1 MainForm = null;
        DataGridView objDataGridView = null;
        DataGrid_Control objDatagridControl = null;
        WaterFallNodes_FormControl1 objTreeNodeForm = null;
        RotateButtonUserControl rotatebutton;
        double selectedZvalue = 0.0;
        int m_iCounter = 0;
        double[] arrZValues = null;
        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();

        public Color _AxisColor
        {
            get
            {
                return AxisColor;
            }
            set
            {
                AxisColor = value;
            }
        }
        public Color _MainCursorColor
        {
            get
            {
                return MainCursor;
            }
            set
            {
                MainCursor = value;
            }
        }
        public Color _GraphBG1
        {
            get
            {
                return GraphBG1;
            }
            set
            {
                GraphBG1 = value;
            }
        }
        public Color _GraphBG2
        {
            get
            {
                return GraphBG2;
            }
            set
            {
                GraphBG2 = value;
            }
        }
        public int _GraphBGDir
        {
            get
            {
                return GraphBGDir;
            }
            set
            {
                GraphBGDir = value;
            }
        }
        public Color _ChartBG1
        {
            get
            {
                return ChartBG1;
            }
            set
            {
                ChartBG1 = value;
            }
        }
        public Color _ChartBG2
        {
            get
            {
                return ChartBG2;
            }
            set
            {
                ChartBG2 = value;
            }
        }
        public int _ChartBGDir
        {
            get
            {
                return ChartBGDir;
            }
            set
            {
                ChartBGDir = value;
            }
        }
        public string _XLabel
        {
            get
            {
                return XLabel;
            }
            set
            {
                XLabel = value;
            }
        }
        public string _YLabel
        {
            get
            {
                return YLabel;
            }
            set
            {
                YLabel = value;
            }
        }
        public bool _AreaFill
        {
            get
            {
                return bAreaFill;
            }
            set
            {
                bAreaFill = value;
            }
        }
        public string _ColorTag
        {
            get
            {
                return sColorTag;
            }
            set
            {
                sColorTag = value;
            }
        }        
        public DataGridViewX DGVTrendNodes
        {
            get
            {
                return objDataGridView1;
            }
            set
            {
                objDataGridView1 = value;
            }
        }
        public Form1 _MainForm
        {
            get
            {
                return MainForm;
            }
            set
            {
                MainForm = value;
            }
        }
        public DataGridView _DataGridView
        {
            get
            {
                return objDataGridView;
            }
            set
            {
                objDataGridView = value;
            }
        }
        public DataGrid_Control _DataGrid_Control
        {
            get
            {
                return objDatagridControl;
            }
            set
            {
                objDatagridControl = value;
            }
        }
        public WaterFallNodes_FormControl1 _WaterFallNodeForm
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
        public ArrayList _XYDATA
        {
            get
            {
                return XYDATA;
            }
            set
            {
                XYDATA = value;
            }
        }

        private void RemovePreviousObjects()
        {
            try
            {
                if (chartVu != null)
                {
                    ArrayList arrObjects = chartVu.GetChartObjectsArrayList();
                    int iCount = arrObjects.Count;
                    if (arrObjects != null)
                    {
                        for (int iCtr = 0; iCtr < iCount; iCtr++)
                        {
                            GraphObj objObject = (GraphObj)arrObjects[0];

                            Type obj = objObject.GetType();
                            chartVu.DeleteChartObject(objObject);

                        }
                    }
                    chartVu.UpdateDraw();


                }//end(if (chartVu != null)0


            }//end(try)
            catch (Exception ex)
            {

            }//end(catch(Exception ex))
        }

        public void DrawWaterfallGraph(double[] Xdata, double[] Ydata, bool Areafill)
        {
            DrawWaterfallGraph(Xdata, Ydata, _ColorTag, Areafill);
        }
        public void DrawWaterfallGraph(double[] Xdata, double[] Ydata, string ColorTag, bool Areafill)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            try
            {


                Dataset2 = new SimpleDataset("P/L", Xdata, Ydata);
                Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, Ydata.Length, "compressed");

                



                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(Dataset2, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.1, .1, .9, 0.9);

                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);

                ChartAttribute wallAttrib = new ChartAttribute(ChartBG1, 1, DashStyle.Solid, ChartBG2);
                Wall3D xyMinZWall = new Wall3D(pTransform1, ChartObj.XY_MAXZ_PLANE, 0.02, wallAttrib);
                chartVu.AddChartObject(xyMinZWall);
                Wall3D yzMinXWall = new Wall3D(pTransform1, ChartObj.YZ_MINX_PLANE, 0.02, wallAttrib);
                chartVu.AddChartObject(yzMinXWall);
                Wall3D xzMinYWall = new Wall3D(pTransform1, ChartObj.XZ_MINY_PLANE, 0.02, wallAttrib);
                chartVu.AddChartObject(xzMinYWall);

                xAxis2 = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                xAxis2.SetColor(_AxisColor);
                xAxis2.SetAxisIntercept(0);
                chartVu.AddChartObject(xAxis2);

                yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                yAxis2.SetColor(_AxisColor);
                chartVu.AddChartObject(yAxis2);

                LinearAxis zAxis = new LinearAxis(pTransform1, ChartObj.Z_AXIS);
                zAxis.SetColor(_AxisColor);
                chartVu.AddChartObject(zAxis);

                xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                xgrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(xgrid2);

                ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                ygrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(ygrid2);



                attrib2 = new ChartAttribute(Color.FromArgb(-Convert.ToInt32(ColorTag)), 1, DashStyle.Solid, Color.FromArgb(150, Color.FromArgb(-Convert.ToInt32(ColorTag))));
                attrib2.SetLineColor(Color.FromArgb(-Convert.ToInt32(ColorTag)));
                attrib2.SetFillFlag(Areafill);


                thePlot1 = new SimpleLinePlot(pTransform1, Dataset2, attrib2);
                chartVu.AddChartObject(thePlot1);
                xAxisLab2 = new NumericAxisLabels(xAxis2);
                xAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(xAxisLab2);

                yAxisLab2 = new NumericAxisLabels(yAxis2);
                yAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(yAxisLab2);

                yaxistitle2 = new AxisTitle(yAxis2, theFont, _YLabel);
                yaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(yaxistitle2);

                xaxistitle2 = new AxisTitle(xAxis2, theFont, _XLabel);
                xaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(xaxistitle2);

                chartVu.SetResizeMode(ChartObj.NO_RESIZE_OBJECTS);

                Font toolTipFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                DataToolTip datatooltip = new DataToolTip(chartVu);
                NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);
                chartVu.Controls.Remove(rotatebutton);
                rotatebutton = new RotateButtonUserControl(pTransform1);
                rotatebutton.Size = new Size(32, 32);
                rotatebutton.Location = new Point(8, 8);
                chartVu.Controls.Add(rotatebutton);
                SetSelectedPlot(thePlot1);
            }
            catch (Exception ex)
            {
            }
        }
        public void DrawWaterfallGraph(double[] Xdata, double[] Ydata, string ColorCode)
        {
            DrawWaterfallGraph(Xdata, Ydata, ColorCode, _AreaFill);
        }
        public void DrawWaterfallGraph(double[] Xdata, double[] YData)
        {
            try
            {
                DrawWaterfallGraph(Xdata, YData, _AreaFill);
            }
            catch (Exception ex)
            {
            }
        }
        public void DrawWaterfallGraph(ArrayList XData, ArrayList YData)
        {
            try
            {
                string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
                DrawWaterfallGraph(XData, YData, ColorCode);
            }
            catch (Exception ex)
            {
            }

        }
        public void DrawWaterfallGraph(ArrayList XData, ArrayList YData, string[] ColorTag)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            arrZValues = new double[YData.Count];
            try
            {
                SimpleDataset[] arrSimpleDataset = new SimpleDataset[YData.Count];
                for (i = 0; i < YData.Count; i++)
                {
                    double[] yy = (double[])YData[i];
                    Dataset2 = new SimpleDataset("P/L", (double[])XData[i], (double[])YData[i]);
                    //Dataset2.ImplicitDepthValue = 0.01;
                    Dataset2.ImplicitZValue = (double)((double)1 / (double)YData.Count) * i;//0.01*i;
                    Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, yy.Length, "compressed");
                    arrSimpleDataset[i] = Dataset2;
                    selectedZvalue = Dataset2.ImplicitZValue;
                    arrZValues[i] = selectedZvalue;
                }

                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(arrSimpleDataset, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.1, .1, .9, 0.9);
                //chartVu.SetFractionalZViewportDepth(.5);
                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);

                // Force starting values of x- and y-scale to 0
               
                // Set rotate parameters
                pTransform1.AbsRotateCoordinateSystem(new Point3D(10, 20, 0));
               



                ChartAttribute wallAttrib = new ChartAttribute(Color.White, 1, DashStyle.Solid, Color.White);
                Wall3D xyMinZWall = new Wall3D(pTransform1, ChartObj.XY_MAXZ_PLANE, 0.02, wallAttrib);
                chartVu.AddChartObject(xyMinZWall);
                Wall3D yzMinXWall = new Wall3D(pTransform1, ChartObj.YZ_MINX_PLANE, 0.02, wallAttrib);
                chartVu.AddChartObject(yzMinXWall);
                Wall3D xzMinYWall = new Wall3D(pTransform1, ChartObj.XZ_MINY_PLANE, 0.02, wallAttrib);
                chartVu.AddChartObject(xzMinYWall);


                xAxis2 = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                xAxis2.SetColor(_AxisColor);
                xAxis2.SetAxisIntercept(0);
                chartVu.AddChartObject(xAxis2);

                yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                yAxis2.SetColor(_AxisColor);
                chartVu.AddChartObject(yAxis2);

                xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                xgrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(xgrid2);

                ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                ygrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(ygrid2);


                for (i = 0; i < YData.Count; i++)
                {
                    attrib2 = new ChartAttribute(Color.FromArgb(-Convert.ToInt32(ColorTag[i])), 1, DashStyle.Solid, Color.FromArgb(150, Color.FromArgb(-Convert.ToInt32(ColorTag[i]))));
                    attrib2.SetLineColor(Color.FromArgb(-Convert.ToInt32(ColorTag[i])));
                    attrib2.SetFillFlag(_AreaFill);

                    _MainCursorColor = Color.FromArgb(-Convert.ToInt32(ColorTag[i]));

                    thePlot1 = new SimpleLinePlot(pTransform1, arrSimpleDataset[i], attrib2);
                    chartVu.AddChartObject(thePlot1);
                }
                xAxisLab2 = new NumericAxisLabels(xAxis2);
                xAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(xAxisLab2);

                yAxisLab2 = new NumericAxisLabels(yAxis2);
                yAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(yAxisLab2);

                yaxistitle2 = new AxisTitle(yAxis2, theFont, _YLabel);
                yaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(yaxistitle2);

                xaxistitle2 = new AxisTitle(xAxis2, theFont, _XLabel);
                xaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(xaxistitle2);

                chartVu.SetResizeMode(ChartObj.NO_RESIZE_OBJECTS);

                //Font toolTipFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                //DataToolTip datatooltip = new DataToolTip(chartVu);
                //NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                //NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                //datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                //datatooltip.SetXValueTemplate(xValueTemplate);
                //datatooltip.SetYValueTemplate(yValueTemplate);
                //datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                //datatooltip.SetEnable(true);
                //chartVu.SetCurrentMouseListener(datatooltip);


                // Define data tooltip mouse listener
                Font toolTipFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                DataToolTip datatooltip = new DataToolTip(chartVu);
                TimeLabel xValueTemplate = new TimeLabel(ChartObj.TIMEDATEFORMAT_MDY); // use minimal constructor
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.CURRENCYFORMAT, 2); // use minimal constructor 
                ChartSymbol toolTipSymbol = new ChartSymbol(null, ChartObj.SQUARE, new ChartAttribute(Color.Black));
                toolTipSymbol.SetSymbolSize(5.0);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_CUSTOM);
                datatooltip.SetToolTipSymbol(toolTipSymbol);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);


                chartVu.Controls.Remove(rotatebutton);
                rotatebutton = new RotateButtonUserControl(pTransform1);
                rotatebutton.Size = new Size(32, 32);
                rotatebutton.Location = new Point(8, 8);
                chartVu.Controls.Add(rotatebutton);
                SetSelectedPlot(thePlot1);
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
                int trendValCtr = (DGVTrendNodes.Rows.Count - 1) % 30;
                DGVTrendNodes.Rows.Add(1);
                DGVTrendNodes.Rows[DGVTrendNodes.Rows.Count - 2].Cells[0].Value = SelectedNodepath;
                DGVTrendNodes.Rows[DGVTrendNodes.Rows.Count - 2].Cells[1].Value = "√";
                DGVTrendNodes.Rows[DGVTrendNodes.Rows.Count - 2].Cells[3].Value = objlistimg.Images[trendValCtr];
                DGVTrendNodes.Rows[DGVTrendNodes.Rows.Count - 2].Cells[3].Tag = ColorCode[trendValCtr].ToString();

            }
            catch (Exception ex)
            {
            }
        }
        public void BackGroundChanges()
        {
            try
            {
                if (chartVu != null)
                {

                    ArrayList arrObjects = chartVu.GetChartObjectsArrayList();
                    int iCount = arrObjects.Count;
                    int iDel = 0;
                    if (arrObjects != null)
                    {
                        for (int iCtr = 0; iCtr < iCount; iCtr++)
                        {
                            GraphObj objObject = (GraphObj)arrObjects[iDel];

                            Type obj = objObject.GetType();
                            if (obj.Name != "SimpleLinePlot")
                            {
                                chartVu.DeleteChartObject(objObject);
                            }
                            else
                            {
                                iDel++;
                            }

                        }
                    }

















                    plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                    chartVu.AddChartObject(plotbackground);

                    background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                    chartVu.AddChartObject(background);

                    ChartAttribute wallAttrib = new ChartAttribute(ChartBG1, 1, DashStyle.Solid, ChartBG2);
                    Wall3D xyMinZWall = new Wall3D(pTransform1, ChartObj.XY_MAXZ_PLANE, 0.02, wallAttrib);
                    chartVu.AddChartObject(xyMinZWall);
                    Wall3D yzMinXWall = new Wall3D(pTransform1, ChartObj.YZ_MINX_PLANE, 0.02, wallAttrib);
                    chartVu.AddChartObject(yzMinXWall);
                    Wall3D xzMinYWall = new Wall3D(pTransform1, ChartObj.XZ_MINY_PLANE, 0.02, wallAttrib);
                    chartVu.AddChartObject(xzMinYWall);

                    xAxis2 = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                    xAxis2.SetColor(_AxisColor);
                    xAxis2.SetAxisIntercept(0);
                    chartVu.AddChartObject(xAxis2);

                    yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                    yAxis2.SetColor(_AxisColor);
                    chartVu.AddChartObject(yAxis2);

                    xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                    xgrid2.SetColor(_AxisColor);
                    chartVu.AddChartObject(xgrid2);

                    ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                    ygrid2.SetColor(_AxisColor);
                    chartVu.AddChartObject(ygrid2);

                    xAxisLab2 = new NumericAxisLabels(xAxis2);
                    xAxisLab2.SetColor(_AxisColor);
                    chartVu.AddChartObject(xAxisLab2);

                    yAxisLab2 = new NumericAxisLabels(yAxis2);
                    yAxisLab2.SetColor(_AxisColor);
                    chartVu.AddChartObject(yAxisLab2);



                    yaxistitle2 = new AxisTitle(yAxis2, theFont, _YLabel);
                    yaxistitle2.SetColor(_AxisColor);
                    chartVu.AddChartObject(yaxistitle2);

                    xaxistitle2 = new AxisTitle(xAxis2, theFont, _XLabel);
                    xaxistitle2.SetColor(_AxisColor);
                    chartVu.AddChartObject(xaxistitle2);

                    chartVu.Update();
                }
            }
            catch (Exception ex)
            {
            }
        }
        internal void CopyGraph()
        {
            try
            {
                if (chartVu != null)
                {
                    BufferedImage objImage = new BufferedImage(chartVu);

                    Image GraphImage = (Image)objImage.GetBufferedImage();
                    Clipboard.SetImage((Image)GraphImage);
                    MessageBoxEx.Show("3D Graph Copied on ClipBoard", "Graph");
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void SetCursorType(string Cursor)
        {
            try
            {
                if (m_objMarker != null)
                {
                    chartVu.DeleteChartObject(m_objMarker);
                    chartVu.DeleteChartObject(m_objDataCursor);

                }//end(if (m_objMarker != null))
                if (arrmarkerCursor != null)
                {
                    for (int i = 0; i < arrmarkerCursor.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarkerCursor[i]);

                    }
                    arrmarkerCursor = new Marker[0];
                }
                if (arrChartTextCursor != null)
                {
                    for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrChartTextCursor[i1]);

                    }
                    arrChartTextCursor = new ChartText[0];
                }
                chartVu.UpdateDraw();
                if (Cursor == "Select Cursor")
                {
                    SelectedCursor = null;
                    m_objDataCursor = null;
                }
                else
                {
                    SelectedCursor = Cursor;
                    try
                    {
                        if (m_objDataCursor != null)
                        {
                            chartVu.DeleteChartObject(m_objDataCursor);
                            chartVu.UpdateDraw();
                        }
                        m_objDataCursor = new DataCursor(chartVu, pTransform1, GraphObj.MARKER, 8.0);
                        m_objDataCursor.SetColor(_MainCursorColor);
                        m_objDataCursor.SetEnable(true);
                        m_objDataCursor.LineStyle = DashStyle.Solid;
                        chartVu.SetCurrentMouseListener(m_objDataCursor);
                        chartVu.AddChartObject(m_objDataCursor);
                        //m_objDataCursor.SetColor(Color.White);
                        //m_objSideBandCursor = null;
                        //chartVu.DeleteChartObject(m_objSideBandMarker);

                        if (m_objMarker != null)
                        {
                            chartVu.DeleteChartObject(m_objMarker);
                            chartVu.UpdateDraw();
                        }//end(if (m_objMarker != null && m_objNewMarker != null))
                        if (Cursor == "Line")
                        {
                            allPlots = GetAllPlots();
                        }
                    }//end(try
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
                    }//end(catch (Exception ex))
                }
            }
            catch (Exception ex)
            {
            }
        }
        SimpleLinePlot[] allPlots = null;

        private void _3DGraphControl_DragEnter(object sender, DragEventArgs e)
        {
            if (this.AllowDrop)//if (btnTrend.Text.ToString() == "Untrend" || IsWaterfall)
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void _3DGraphControl_DragDrop(object sender, DragEventArgs e)
        {
            try
            {

                ArrayList data = _MainForm.ReadDroppedWaterfallCSVFile();
                if (data != null)
                {
                    string path = _MainForm.ReadDroppedCSVFilePath();
                    _MainForm.Set_iClick(DI_Analyser.Form1.Function.Add);
                    AddNode(_MainForm.MainTreelist.FocusedNode.GetDisplayText(0).ToString());
                    DGVTrendNodes.Rows[DGVTrendNodes.Rows.Count - 2].Cells[2].Value = path;
                    ArrayList xdta = new ArrayList();
                    ArrayList ydta = new ArrayList();
                    ArrayList FullXYData = new ArrayList();
                    //FullXYData = XYDATA;

                    //xdta.Add((double[])XYDATA[0]);
                    //ydta.Add((double[])XYDATA[1]);
                    //FullXYData.Add((double[])XYDATA[0]);
                    //FullXYData.Add((double[])XYDATA[1]);
                    for (int i = 0; i < data.Count / 2; i++)
                    {
                        xdta.Add((double[])data[2 * i]);
                        ydta.Add((double[])data[(2 * i) + 1]);

                        FullXYData.Add((double[])data[2 * i]);
                        FullXYData.Add((double[])data[(2 * i) + 1]);
                    }
                    //_WaterFallNodeForm._FullXYData = FullXYData;
                    // _TrendNodeForm._PartialXYData = FullXYData;
                    DrawWaterfallGraph(xdta, ydta);
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }
        void _3DGraphControl_ThisMouseMove(MouseEventArgs e)
        {
            SimpleDataset Dataset1 = null;
            NearestPointData nearestPointObj1 = null;
            NearestPointData objSecondNearestPoint = null;
            Font textCoordsFont = null;
            Point3D ptLocation = null;
            bool bFirstPlot = false;
            Point3D ptNewPoint = null;

            try
            {
                Point3D objNewPoint = new Point3D();


                objNewPoint.SetLocation(e.X, e.Y);
                //if (m_objChartView == null)
                //    m_objChartView = m_obj2DGraphControl.CreateSideBandCursor
                ChartObj objNewObject = chartVu.FindObj(objNewPoint, "SimpleLinePlot");

                if (objNewObject != null)
                {
                    chartVu.Cursor = Cursors.Hand;
                    //m_objSelectedPlot = (SimpleLinePlot)objNewObject;
                    //m_objNewPlot = (SimpleLinePlot)objNewObject;


                }
                else
                {
                    chartVu.Cursor = Cursors.Arrow;
                    // m_objSelectedPlot = null;
                }
                if (e.Button == MouseButtons.Left)
                {
                    if (SelectedCursor != null)
                    {
                        if (m_objNewPlot != null)
                        {
                            m_objSelectedPlotForCursor = m_objNewPlot;


                            if (m_objClickedPlot != null)// && m_objClickedPlot.LineWidth == 2)
                            {
                                Dataset1 = m_objSelectedPlotForCursor.DisplayDataset;
                                //Point3D objPoint = Dataset1.GetDataPoint(m_iCounter);


                                nearestPointObj1 = new NearestPointData();
                                objSecondNearestPoint = new NearestPointData();
                                textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                                base.OnMouseMove(e);
                                if (m_objDataCursor != null)
                                {
                                    ptLocation = m_objDataCursor.Location;

                                    bFirstPlot = m_objClickedPlot.CalcNearestPoint(ptLocation, 3, nearestPointObj1);
                                }
                                //m_objPointsInData.Add(objPoint, objPoint);
                                //if (m_objSideBandCursor != null)
                                //chartVu.DeleteChartObject(m_objSideBandCursor);


                                ptNewPoint = nearestPointObj1.GetNearestPoint();

                                m_iCounter = nearestPointObj1.GetNearestPointIndex();

                                if (m_objMarker != null)
                                {
                                    chartVu.DeleteChartObject(m_objMarker);
                                    //chartVu.DeleteChartObject(m_objNewMarker);
                                    //m_objChartView.DeleteChartObject(objSecondLineMarker);
                                    chartVu.DeleteChartObject(m_objDataCursor);

                                }//end(if (m_objMarker != null))
                                if (arrmarkerCursor != null)
                                {
                                    for (int i = 0; i < arrmarkerCursor.Length; i++)
                                    {
                                        chartVu.DeleteChartObject(arrmarkerCursor[i]);
                                        chartVu.DeleteChartObject(m_objDataCursor);
                                    }
                                }
                                if (arrChartTextCursor != null)
                                {
                                    for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                                    {
                                        chartVu.DeleteChartObject(arrChartTextCursor[i1]);
                                    }
                                }
                                arrChartTextCursor = new ChartText[0];

                                m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                if (_DataGridView != null)
                                {
                                    _DataGridView.Rows.Clear();
                                    _DataGridView.Rows.Add(1);
                                }

                                ///////////////////BearingFFOverriddenRPM = ptNewPoint.GetX().ToString();
                                theFont = new Font("Arial", 8, FontStyle.Regular);
                                /////single cursor
                                switch (SelectedCursor)
                                {
                                    case "Line":
                                        {                                            
                                            arrmarkerCursor = new Marker[allPlots.Length];
                                            arrChartTextCursor = new ChartText[allPlots.Length];
                                            for (int k = 0; k < allPlots.Length; k++)
                                            {
                                                Dataset1 = allPlots[k].DisplayDataset;
                                                if (m_objDataCursor != null)
                                                {
                                                    ptLocation = m_objDataCursor.Location;

                                                    bFirstPlot = allPlots[k].CalcNearestPoint(ptLocation, 3, nearestPointObj1);
                                                }
                                                ptNewPoint = nearestPointObj1.GetNearestPoint();


                                                Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, ptNewPoint.GetX(), ptNewPoint.GetY(), arrZValues[arrZValues.Length-(k+1)], 8,1);
                                                m_objMarker1.SetColor(allPlots[k].GetColor());
                                                arrmarkerCursor[k] = m_objMarker1;
                                                chartVu.AddChartObject(m_objMarker1);


                                                ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, arrZValues[arrZValues.Length - (k + 1)], ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                                CurrentLabel.SetColor(allPlots[k].GetColor());
                                                chartVu.AddChartObject(CurrentLabel);
                                                arrChartTextCursor[k] = CurrentLabel;

                                                _DataGridView.Rows.Add(1);
                                                _DataGridView[0, k].Value = ptNewPoint.GetX().ToString();
                                                _DataGridView[1, k].Value = ptNewPoint.GetY().ToString();
                                                _DataGridView[0, k].Style.ForeColor = allPlots[k].GetColor();
                                            }
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "Single":
                                        {
                                            arrmarkerCursor = new Marker[1];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), selectedZvalue, 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            //ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "Single With Square":
                                        {
                                            arrmarkerCursor = new Marker[2];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, ptNewPoint.GetX(), ptNewPoint.GetY(), selectedZvalue, 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);
                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), selectedZvalue, 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "Cross Hair":
                                        {
                                            arrmarkerCursor = new Marker[1];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_HVLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), selectedZvalue, 8,1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            //ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
                chartVu.UpdateDraw();
            }
            catch (Exception ex)
            {
            }
        }
        private void CheckKeyDown(string Direction, string SelectedCursor)
        {
            try
            {
                switch (SelectedCursor)
                {
                    case "Single":
                        {
                            CheckKeyDownSingle(Direction);
                            break;
                        }
                    //case "Harmonic":
                    //    {
                    //        CheckKeyDownHarmonic(Direction);
                    //        break;
                    //    }
                    case "Single With Square":
                        {
                            CheckKeyDownSingleWithSquare(Direction);
                            break;
                        }
                    case "Cross Hair":
                        {
                            CheckKeyDownCrossHair(Direction);
                            break;
                        }
                    case "Line":
                        {
                            CheckKeyDownLine(Direction);
                            break;
                        }
                    //case "Sideband":
                    //    {
                    //        CheckKeyDownSideBandValue(Direction);
                    //        break;
                    //    }
                    //case "SidebandRatio":
                    //    {
                    //        CheckKeyDownSideBandRatio(Direction);
                    //        break;
                    //    }
                    //case "SideBandTrend":
                    //    {
                    //        CheckKeyDownSideBandTrend(Direction);
                    //        break;
                    //    }
                    //case "PeekCursor":
                    //    {
                    //        CheckKeydownPeekCursor(Direction);
                    //        break;
                    //    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void CheckKeyDownSingleWithSquare(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                ArrayList arrObjects = chartVu.GetChartObjectsArrayList();

                foreach (GraphObj objObject in arrObjects)
                {
                    int iType = objObject.ChartObjType;

                    objLine = objObject;
                    if (iType == 1)
                    {
                        objClickedLine = (SimpleLinePlot)objLine;
                        break;
                    }
                }
                if (arrmarkerCursor != null)
                {
                    for (int i = 0; i < arrmarkerCursor.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarkerCursor[i]);
                        chartVu.DeleteChartObject(m_objDataCursor);
                    }
                }
                if (arrChartTextCursor != null)
                {
                    for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrChartTextCursor[i1]);
                    }
                }
                arrChartTextCursor = new ChartText[1];
                if (sType == "Right")
                {
                    {
                        m_iCounter++;
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);

                        objDataSet = objClickedLine.DisplayDataset;
                        int iNumber = objDataSet.GetNumberDatapoints();
                        if (m_iCounter >= iNumber)
                            m_iCounter = iNumber - 1;
                        Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                        ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                        nearestPointObj1 = new NearestPointData();
                        textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                        // m_objPointsInData.Add(objPoint, objPoint);
                        ptNewPoint = nearestPointObj1.GetNearestPoint();


                        if (m_objMarker != null)
                        {
                            chartVu.DeleteChartObject(m_objMarker);
                            chartVu.DeleteChartObject(m_objNewMarker);
                            if (m_objDataCursor != null)
                                chartVu.DeleteChartObject(m_objDataCursor);

                        }//end(if (m_objMarker != null))

                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, selectedZvalue, 5, 1);
                        m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, selectedZvalue, 8, 1);
                        m_objMarker.SetColor(_MainCursorColor);
                        m_objNewMarker.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(m_objMarker);
                        chartVu.AddChartObject(m_objNewMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartTextCursor[0] = CurrentLabel;

                        if (m_objMarker != null)
                        {
                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, 0].Value = objPoint.X.ToString();
                            _DataGridView[1, 0].Value = objPoint.Y.ToString();
                            _DataGridView.Refresh();
                            chartVu.UpdateDraw();

                        }//end(if (objMarker != null))

                        //int iNumber = objDataSet.GetNumberDatapoints();
                        //if (m_iCounter > iNumber)
                        //    m_iCounter = iNumber;
                    }
                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    if (m_iCounter < 0)
                        m_iCounter = 0;
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point3D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, selectedZvalue, 5, 1);
                    m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, selectedZvalue, 8, 1);
                    m_objMarker.SetColor(_MainCursorColor);
                    m_objNewMarker.SetColor(_MainCursorColor);

                    chartVu.AddChartObject(m_objMarker);
                    chartVu.AddChartObject(m_objNewMarker);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = objPoint.X.ToString();
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarkerCursor = new Marker[2];
                arrmarkerCursor[0] = m_objMarker;
                arrmarkerCursor[1] = m_objNewMarker;
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownLine(string sType)
        {
            //NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            //Point3D ptNewPoint = null;
            //Point3D ptLocation = null;
            //Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                ArrayList arrObjects = chartVu.GetChartObjectsArrayList();

                if (arrmarkerCursor != null)
                {
                    for (int i = 0; i < arrmarkerCursor.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarkerCursor[i]);
                        chartVu.DeleteChartObject(m_objDataCursor);
                    }
                }
                if (arrChartTextCursor != null)
                {
                    for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrChartTextCursor[i1]);
                    }
                }
                arrmarkerCursor = new Marker[allPlots.Length];
                arrChartTextCursor = new ChartText[allPlots.Length];
                if (sType == "Right")
                {

                    //{
                    //    arrmarkerCursor = new Marker[allPlots.Length];
                    //    arrChartTextCursor = new ChartText[allPlots.Length];
                    //    for (int k = 0; k < allPlots.Length; k++)
                    //    {
                    //        objDataSet = allPlots[k].DisplayDataset;
                    //        if (m_objDataCursor != null)
                    //        {
                    //            ptLocation = m_objDataCursor.Location;

                    //            bFirstPlot = allPlots[k].CalcNearestPoint(ptLocation, 3, nearestPointObj1);
                    //        }
                    //        ptNewPoint = nearestPointObj1.GetNearestPoint();


                    //        Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, ptNewPoint.GetX(), ptNewPoint.GetY(), arrZValues[arrZValues.Length - (k + 1)], 8, 1);
                    //        m_objMarker1.SetColor(allPlots[k].GetColor());
                    //        arrmarkerCursor[k] = m_objMarker1;
                    //        chartVu.AddChartObject(m_objMarker1);


                    //        ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, arrZValues[arrZValues.Length - (k + 1)], ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    //        CurrentLabel.SetColor(allPlots[k].GetColor());
                    //        chartVu.AddChartObject(CurrentLabel);
                    //        arrChartTextCursor[k] = CurrentLabel;

                    //        _DataGridView.Rows.Add(1);
                    //        _DataGridView[0, k].Value = ptNewPoint.GetX().ToString();
                    //        _DataGridView[1, k].Value = ptNewPoint.GetY().ToString();
                    //    }
                    //    _DataGridView.Refresh();
                    //    break;
                    //}

                    {
                        m_iCounter++;
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);
                        for (int k = 0; k < allPlots.Length; k++)
                        {
                            objDataSet = allPlots[k].DisplayDataset;
                            int iNumber = objDataSet.GetNumberDatapoints();
                            if (m_iCounter >= iNumber)
                                m_iCounter = iNumber - 1;
                            Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                            //ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                            //nearestPointObj1 = new NearestPointData();
                            textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                            
                            //ptNewPoint = nearestPointObj1.GetNearestPoint();


                            if (m_objMarker != null)
                            {
                                chartVu.DeleteChartObject(m_objMarker);
                                chartVu.DeleteChartObject(m_objNewMarker);
                                if (m_objDataCursor != null)
                                    chartVu.DeleteChartObject(m_objDataCursor);

                            }//end(if (m_objMarker != null))

                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, arrZValues[arrZValues.Length - (k + 1)], 8, 1);
                            m_objMarker1.SetColor(allPlots[k].GetColor());
                            chartVu.AddChartObject(m_objMarker1);
                            arrmarkerCursor[k] = m_objMarker1;

                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, arrZValues[arrZValues.Length - (k + 1)], ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                            CurrentLabel.SetColor(allPlots[k].GetColor());
                            chartVu.AddChartObject(CurrentLabel);
                            arrChartTextCursor[k] = CurrentLabel;
                            
                            if (m_objMarker != null)
                            {
                                _DataGridView.Rows.Add(1);
                                _DataGridView[0, k].Value = objPoint.X.ToString();
                                _DataGridView[1, k].Value = objPoint.Y.ToString();
                                _DataGridView[0, k].Style.ForeColor = allPlots[k].GetColor();
                                _DataGridView.Refresh();
                                //chartVu.UpdateDraw();

                            }//end(if (objMarker != null))
                        }

                    }
                }
                else if (sType == "Left")
                {
                    {
                        m_iCounter--;
                        if (m_iCounter < 0)
                            m_iCounter = 0;
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);
                        for (int k = 0; k < allPlots.Length; k++)
                        {
                            objDataSet = allPlots[k].DisplayDataset;
                            
                            Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                            //ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                            //nearestPointObj1 = new NearestPointData();
                            textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                            //ptNewPoint = nearestPointObj1.GetNearestPoint();


                            if (m_objMarker != null)
                            {
                                chartVu.DeleteChartObject(m_objMarker);
                                chartVu.DeleteChartObject(m_objNewMarker);
                                if (m_objDataCursor != null)
                                    chartVu.DeleteChartObject(m_objDataCursor);

                            }//end(if (m_objMarker != null))

                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, arrZValues[arrZValues.Length - (k + 1)], 8, 1);
                            m_objMarker1.SetColor(allPlots[k].GetColor());
                            chartVu.AddChartObject(m_objMarker1);
                            arrmarkerCursor[k] = m_objMarker1;

                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, arrZValues[arrZValues.Length - (k + 1)], ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                            CurrentLabel.SetColor(allPlots[k].GetColor());
                            chartVu.AddChartObject(CurrentLabel);
                            arrChartTextCursor[k] = CurrentLabel;

                            if (m_objMarker != null)
                            {
                                _DataGridView.Rows.Add(1);
                                _DataGridView[0, k].Value = objPoint.X.ToString();
                                _DataGridView[1, k].Value = objPoint.Y.ToString();
                                _DataGridView[0, k].Style.ForeColor = allPlots[k].GetColor();
                                _DataGridView.Refresh();
                                //chartVu.UpdateDraw();

                            }//end(if (objMarker != null))
                        }

                    }

                    //m_iCounter--;
                    //if (m_iCounter < 0)
                    //    m_iCounter = 0;
                    //objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    //if (m_objDataCursor != null)
                    //    chartVu.DeleteChartObject(m_objDataCursor);
                    //Point3D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ////ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    ////nearestPointObj1 = new NearestPointData();
                    //textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ////ptNewPoint = nearestPointObj1.GetNearestPoint();

                    //if (m_objMarker != null)
                    //{
                    //    chartVu.DeleteChartObject(m_objMarker);
                    //    chartVu.DeleteChartObject(m_objNewMarker);
                    //}

                    //m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, selectedZvalue, 5, 1);
                    //m_objMarker.SetColor(_MainCursorColor);

                    //chartVu.AddChartObject(m_objMarker);

                    //ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    //CurrentLabel.SetColor(_MainCursorColor);
                    //chartVu.AddChartObject(CurrentLabel);
                    //arrChartTextCursor[0] = CurrentLabel;

                    //if (m_objMarker != null)
                    //{
                    //    _DataGridView.Rows.Add(1);
                    //    _DataGridView[0, 0].Value = objPoint.X.ToString();
                    //    _DataGridView[1, 0].Value = objPoint.Y.ToString();
                    //    _DataGridView.Refresh();
                    //   // chartVu.UpdateDraw();

                    //}//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                //arrmarkerCursor = new Marker[1];
                //arrmarkerCursor[0] = m_objMarker;
                chartVu.UpdateDraw();
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownSingle(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                ArrayList arrObjects = chartVu.GetChartObjectsArrayList();

                //foreach (GraphObj objObject in arrObjects)
                //{
                //    int iType = objObject.ChartObjType;

                //    objLine = objObject;
                //    if (iType == 1)
                //    {
                //        objClickedLine = (SimpleLinePlot)objLine;
                //        break;
                //    }
                //}

                if (m_objNewPlot == null)
                {

                    foreach (GraphObj objObject in arrObjects)
                    {
                        int iType = objObject.ChartObjType;

                        objLine = objObject;
                        if (iType == 1)
                        {
                            objClickedLine = (SimpleLinePlot)objLine;
                            break;
                        }
                    }
                }
                else
                {
                    objClickedLine = m_objNewPlot;
                }

                if (arrmarkerCursor != null)
                {
                    for (int i = 0; i < arrmarkerCursor.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarkerCursor[i]);
                        chartVu.DeleteChartObject(m_objDataCursor);
                    }
                }
                if (arrChartTextCursor != null)
                {
                    for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrChartTextCursor[i1]);
                    }
                }
                arrChartTextCursor = new ChartText[1];
                if (sType == "Right")
                {
                    {
                        m_iCounter++;
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);

                        objDataSet = objClickedLine.DisplayDataset;
                        int iNumber = objDataSet.GetNumberDatapoints();
                        if (m_iCounter >= iNumber)
                            m_iCounter = iNumber - 1;
                        Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                        ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                        nearestPointObj1 = new NearestPointData();
                        textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                        //m_objPointsInData.Add(objPoint, objPoint);
                        ptNewPoint = nearestPointObj1.GetNearestPoint();


                        if (m_objMarker != null)
                        {
                            chartVu.DeleteChartObject(m_objMarker);
                            chartVu.DeleteChartObject(m_objNewMarker);
                            if (m_objDataCursor != null)
                                chartVu.DeleteChartObject(m_objDataCursor);

                        }//end(if (m_objMarker != null))

                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y,selectedZvalue, 5, 1);
                        m_objMarker.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(m_objMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop,selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartTextCursor[0] = CurrentLabel;

                        if (m_objMarker != null)
                        {
                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, 0].Value = objPoint.X.ToString();
                            _DataGridView[1, 0].Value = objPoint.Y.ToString();
                            _DataGridView.Refresh();
                            chartVu.UpdateDraw();

                        }//end(if (objMarker != null))


                    }
                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    if (m_iCounter < 0)
                        m_iCounter = 0;
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point3D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, selectedZvalue, 5, 1);
                    m_objMarker.SetColor(_MainCursorColor);

                    chartVu.AddChartObject(m_objMarker);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = objPoint.X.ToString();
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarkerCursor = new Marker[1];
                arrmarkerCursor[0] = m_objMarker;
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownCrossHair(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                ArrayList arrObjects = chartVu.GetChartObjectsArrayList();

                //foreach (GraphObj objObject in arrObjects)
                //{
                //    int iType = objObject.ChartObjType;

                //    objLine = objObject;
                //    if (iType == 1)
                //    {
                //        objClickedLine = (SimpleLinePlot)objLine;
                //        break;
                //    }
                //}

                if (m_objNewPlot == null)
                {

                    foreach (GraphObj objObject in arrObjects)
                    {
                        int iType = objObject.ChartObjType;

                        objLine = objObject;
                        if (iType == 1)
                        {
                            objClickedLine = (SimpleLinePlot)objLine;
                            break;
                        }
                    }
                }
                else
                {
                    objClickedLine = m_objNewPlot;
                }

                if (arrmarkerCursor != null)
                {
                    for (int i = 0; i < arrmarkerCursor.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarkerCursor[i]);
                        chartVu.DeleteChartObject(m_objDataCursor);
                    }
                }
                if (arrChartTextCursor != null)
                {
                    for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrChartTextCursor[i1]);
                    }
                }
                arrChartTextCursor = new ChartText[1];
                if (sType == "Right")
                {
                    {
                        m_iCounter++;
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);

                        objDataSet = objClickedLine.DisplayDataset;
                        int iNumber = objDataSet.GetNumberDatapoints();
                        if (m_iCounter >= iNumber)
                            m_iCounter = iNumber - 1;
                        Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                        ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                        nearestPointObj1 = new NearestPointData();
                        textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                        //m_objPointsInData.Add(objPoint, objPoint);
                        ptNewPoint = nearestPointObj1.GetNearestPoint();


                        if (m_objMarker != null)
                        {
                            chartVu.DeleteChartObject(m_objMarker);
                            chartVu.DeleteChartObject(m_objNewMarker);
                            if (m_objDataCursor != null)
                                chartVu.DeleteChartObject(m_objDataCursor);

                        }//end(if (m_objMarker != null))

                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_HVLINE, objPoint.X, objPoint.Y, selectedZvalue, 5, 1);
                        m_objMarker.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(m_objMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartTextCursor[0] = CurrentLabel;

                        if (m_objMarker != null)
                        {
                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, 0].Value = objPoint.X.ToString();
                            _DataGridView[1, 0].Value = objPoint.Y.ToString();
                            _DataGridView.Refresh();
                            chartVu.UpdateDraw();

                        }//end(if (objMarker != null))


                    }
                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    if (m_iCounter < 0)
                        m_iCounter = 0;
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point3D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_HVLINE, objPoint.X, objPoint.Y, selectedZvalue, 5, 1);
                    m_objMarker.SetColor(_MainCursorColor);

                    chartVu.AddChartObject(m_objMarker);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, selectedZvalue, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = objPoint.X.ToString();
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarkerCursor = new Marker[1];
                arrmarkerCursor[0] = m_objMarker;
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        public void SetSelectedPlot(SimpleLinePlot plot)
        {
            try
            {
                m_objClickedPlot = plot;
                m_objSelectedPlot = plot;
                m_objNewPlot = plot;
            }
            catch (Exception ex)
            {
            }
        }

        

        public ArrayList SelectNextPlot(int ColorValue)
        {
            ArrayList arlstToreturn = new ArrayList();
            try
            {
                SimpleLinePlot[] AllPlots = GetAllPlots();
                Color SelectPlotColor = Color.FromArgb(-Convert.ToInt32(ColorValue));
                for (int iplot = 0; iplot < AllPlots.Length; iplot++)
                {
                    if (AllPlots[iplot].LineColor == SelectPlotColor)//Color.FromArgb(-Convert.ToInt32(arlstSColors[0]))
                    {
                        SetSelectedPlot((SimpleLinePlot)AllPlots[iplot]);

                        SimpleDataset TestDataset = ((SimpleLinePlot)AllPlots[iplot]).DisplayDataset;
                        arlstToreturn.Add(TestDataset.GetXData());
                        arlstToreturn.Add(TestDataset.GetYData());
                        selectedZvalue = TestDataset.ImplicitZValue;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return arlstToreturn;
        }
        private SimpleLinePlot[] GetAllPlots()
        {
            SimpleLinePlot[] plotlist = new SimpleLinePlot[0];
            try
            {
                if (chartVu != null)
                {
                    ArrayList arrObjects = chartVu.GetChartObjectsArrayList();
                    int iCount = arrObjects.Count;
                    if (arrObjects != null)
                    {
                        for (int iCtr = 0; iCtr < iCount; iCtr++)
                        {
                            GraphObj objObject = (GraphObj)arrObjects[iCtr];

                            Type obj = objObject.GetType();
                            if (obj.Name.ToString().Contains("SimpleLinePlot"))
                            {
                                SimpleLinePlot TestPlot = (SimpleLinePlot)objObject;
                                //Array.Resize(ref plotlist, plotlist.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlot3D(ref plotlist, 1);
                                plotlist[plotlist.Length - 1] = TestPlot;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return plotlist;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            try
            {
                if (SelectedCursor != null)
                {
                    if (msg.Msg == WM_KEYDOWN)
                    {
                        switch (keyData)
                        {
                            case Keys.Left:
                                {
                                    CheckKeyDown("Left", SelectedCursor);
                                    //this.Focus();
                                    break;
                                }
                            case Keys.Right:
                                {
                                    CheckKeyDown("Right", SelectedCursor);
                                    //this.Focus();
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (ThisMouseMove != null)
                {
                    base.OnMouseMove(e);
                    ThisMouseMove(e);
                }

            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
