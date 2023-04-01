using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using com.iAM.chart2dnet;
using System.Collections;
using System.Drawing.Drawing2D;
using DI_Analyser;
using DevExpress.XtraTreeList.Nodes;
using Analyser.Forms;
using Analyser.Properties;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using Analyser.interfaces;
using Analyser.Classes;

namespace Analyser.Graph_Controls
{
    public partial class LineGraphControl : ChartView
    {
        public LineGraphControl()
        {
            InitializeComponent();
            this.ThisMouseMove += new MouseMoveHandler(LineGraphControl_ThisMouseMove);
            this.GraphClicked += new GraphClickedHandler(LineGraphControl_GraphClicked);

            //m_objPointsInData = new Dictionary<Point2D, Point2D>();

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
        bool BearingFF = false;
        string BearingFFOverriddenRPM = null;
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
        Color FooterTextColor = Color.Black;
        int GraphBGDir = 0;
        int ChartBGDir = 0;
        ChartShape[] arrChartShape = null;
        ChartShape[] arrChartShape1 = null;
        Marker[] arrmarker = null;
        Marker[] arrmarkerCursor = null;
        ChartText[] arrChartText = null;
        ChartText[] arrChartTextCursor = null;
        Marker FaultFrequencyMarkers = null;
        Marker m_objMarker = null;
        Marker m_objNewMarker = null;
        SimpleLinePlot m_objSelectedPlot = null;
        SimpleLinePlot m_objNewPlot = null;
        SimpleLinePlot m_objSelectedPlotForCursor = null;
        SimpleLinePlot m_objClickedPlot = null;
        SimpleLinePlot m_objOldSelectedPlot = null;
        public delegate void MouseMoveHandler(MouseEventArgs e);
        public event MouseMoveHandler ThisMouseMove;
        public delegate void GraphClickedHandler(MouseEventArgs e);
        public event GraphClickedHandler GraphClicked;
        
        DataCursor m_objDataCursor = null;
        string SelectedCursor = null;
        SimpleDataset Dataset2;
        ChartView chartVu;
        Font theFont = new Font("Arial", 8, FontStyle.Regular);
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
        string ChartFooter = null;
        DataToolTip datatooltip = null;
        Form1 MainForm = null;
        DataGridView objDataGridView = null;
        DataGrid_Control objDatagridControl = null;
        bool _selectBandTrend = false;
        double OriginalYMaxscale = 0;
        ArrayList XYDATA = new ArrayList();
        SideBandTrend objTrend = null;
        DataGridViewX objDataGridView1 = null;
        int m_iCounter = 0;
        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();
        public string _ChartFooter
        {
            get
            {
                return ChartFooter;
            }
            set
            {
                ChartFooter = value;
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
        public Color _AxisColor
        {
            get
            {
                return AxisColor;
            }
            set
            {
                AxisColor = value;
                FooterTextColor = value;
            }
        }
        public Color _FooterColor
        {
            get
            {
                return FooterTextColor;
            }
            set
            {
                FooterTextColor = value;
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
        public string _BearingFFOverridenRPM
        {
            get
            {
                return BearingFFOverriddenRPM;
            }
            set
            {
                BearingFFOverriddenRPM = value;
            }
        }
        public bool _IsBearingFF
        {
            get
            {
                return BearingFF;
            }
            set
            {
                BearingFF = value;
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
        public bool SelectBandTrend
        {
            get
            {
                return _selectBandTrend;
            }
            set
            {
                _selectBandTrend = value;
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
        
        public void DrawLineGraph(double[] Xdata, double[] Ydata, bool Areafill)
        {
            //string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };

            DrawLineGraph(Xdata, Ydata, _ColorTag, Areafill);      
        }
        public void DrawLineGraph(double[] Xdata, double[] Ydata, string ColorTag, bool Areafill)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            try
            {

               
                Dataset2 = new SimpleDataset("P/L", Xdata, Ydata);
                Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, Ydata.Length, "compressed");
                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(Dataset2, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);

                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);


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
                if (_ChartFooter != null)
                {
                    ChartTitle footer = new ChartTitle(pTransform1, theFont, _ChartFooter);
                    footer.SetColor(_FooterColor);
                    footer.SetTitleType(ChartObj.CHART_FOOTER);
                    footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
                    footer.SetTitleOffset(8);
                    chartVu.AddChartObject(footer);
                }

                Font toolTipFont = new Font("Arial", 10, FontStyle.Regular);
                datatooltip = new DataToolTip(chartVu);
                NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);

                //ChartZoom zoomObj = new ChartZoom(chartVu, pTransform2, true);
                //zoomObj.SetButtonMask(MouseButtons.Left);
                //zoomObj.SetZoomYEnable(true);
                //zoomObj.SetZoomXEnable(true);
                //zoomObj.SetZoomXRoundMode(ChartObj.AUTOAXES_EXACT);
                //zoomObj.SetZoomYRoundMode(ChartObj.AUTOAXES_EXACT);
                //zoomObj.InternalZoomStackProcesssing = true;

                //zoomObj.SetEnable(true);
                //// set range limits to 1000 ms, 1 degree
                ////		zoomObj.SetZoomRangeLimitsRatio(new Dimension(1.0, 1.0));
                //chartVu.SetCurrentMouseListener(zoomObj);
                SetSelectedPlot(thePlot1);
            }
            catch (Exception ex)
            {
            }
        }
        public void DrawLineGraph(double[] Xdata, double[] Ydata, string ColorCode)
        {           
            DrawLineGraph(Xdata, Ydata, ColorCode, _AreaFill);
        }
        public void DrawLineGraph(double[] Xdata, double[] YData)
        {
            try
            {
                //ArrayList arlXdata = new ArrayList();
                //ArrayList arlYdata = new ArrayList();
                //arlXdata.Add(Xdata);
                //arlYdata.Add(YData);
                //DrawLineGraph(arlXdata, arlYdata);
                DrawLineGraph(Xdata, YData, _AreaFill);
            }
            catch (Exception ex)
            {
            }
        }
        public void DrawLineGraph(ArrayList XData, ArrayList YData)
        {
            try
            {
                string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
                DrawLineGraph(XData, YData, ColorCode);
            }
            catch (Exception ex)
            {
            }

        }
        public void DrawLineGraph(ArrayList XYData,string[] ColorTag)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            bool InvertX = false;
            bool InvertY = false;

             try
            {
                SimpleDataset[] arrTestDataset = new SimpleDataset[0];
                for(int i=0;i<XYData.Count/2;i++)
                {
                    double[] testX=(double[])XYData[2 * i];
                    double[] testY=(double[])XYData[(2 * i) + 1];
                    //if (Convert.ToDouble(testX[0].ToString()) > Convert.ToDouble(testX[testX.Length - 1].ToString()))
                    //{
                    //    InvertX = true;
                    //}
                    //if (Convert.ToDouble(testY[0].ToString()) > Convert.ToDouble(testY[testY.Length - 1].ToString()))
                    //{
                    //    InvertY = true;
                    //}
                    SimpleDataset testdataset = new SimpleDataset("Test", testX,testY );
                    testdataset.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, testX.Length, "Test");
                    //Array.Resize(ref arrTestDataset, arrTestDataset.Length + 1);
                    _ResizeArray.IncreaseArrayLinePlotDataset2D(ref arrTestDataset, 1);
                    arrTestDataset[arrTestDataset.Length - 1] = testdataset;
                }




                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(arrTestDataset, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);
                if (InvertX)
                {
                    pTransform1.InvertScaleX();
                }
                if (InvertY)
                {
                    pTransform1.InvertScaleY();
                }
                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);


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


                for (int i = 0; i < arrTestDataset.Length; i++)
                {
                    attrib2 = new ChartAttribute(Color.FromArgb(-Convert.ToInt32(ColorTag[i])), 1, DashStyle.Solid, Color.FromArgb(150, Color.FromArgb(-Convert.ToInt32(ColorTag[i]))));
                    attrib2.SetLineColor(Color.FromArgb(-Convert.ToInt32(ColorTag[i])));
                    attrib2.SetFillFlag(_AreaFill);


                    thePlot1 = new SimpleLinePlot(pTransform1, arrTestDataset[i], attrib2);
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

                if (_ChartFooter != null)
                {
                    ChartTitle footer = new ChartTitle(pTransform1, theFont, _ChartFooter);
                    footer.SetColor(_FooterColor);
                    footer.SetTitleType(ChartObj.CHART_FOOTER);
                    footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
                    footer.SetTitleOffset(8);
                    chartVu.AddChartObject(footer);
                }

                Font toolTipFont = new Font("Arial", 10, FontStyle.Regular);
                DataToolTip datatooltip = new DataToolTip(chartVu);
                NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);
                chartVu.UpdateDraw();
                SelectNextPlot(Convert.ToInt32(ColorTag[0].ToString()));
            }
            catch (Exception ex)
            {
            }
        }       
        public void DrawLineGraph(ArrayList XData, ArrayList YData, string[] ColorTag)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            try
            {
                SimpleDataset[] arrSimpleDataset = new SimpleDataset[YData.Count];
                for (i = 0; i < YData.Count; i++)
                {
                    double[] yy=(double[])YData[i];
                    Dataset2 = new SimpleDataset("P/L", (double[])XData[i], (double[])YData[i]);
                    Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, yy.Length, "compressed");
                    arrSimpleDataset[i] = Dataset2;
                }
                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(arrSimpleDataset, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);

                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);


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

                Font toolTipFont = new Font("Arial", 10, FontStyle.Regular);
                DataToolTip datatooltip = new DataToolTip(chartVu);
                NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);               
                SetSelectedPlot(thePlot1);
            }
            catch (Exception ex)
            {
            }
            
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

        public void DrawLineOrbitGraph(ArrayList XYData, string[] ColorTag)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            //bool InvertX = false;
            //bool InvertY = false;

            try
            {
                SimpleDataset[] arrTestDataset = new SimpleDataset[0];
                for (int i = 0; i < XYData.Count / 2; i++)
                {
                    double[] testX = (double[])XYData[2 * i];
                    double[] testY = (double[])XYData[(2 * i) + 1];
                    //if (Convert.ToDouble(testX[0].ToString()) > Convert.ToDouble(testX[testX.Length - 1].ToString()))
                    //{
                    //    InvertX = true;
                    //}
                    //if (Convert.ToDouble(testY[0].ToString()) > Convert.ToDouble(testY[testY.Length - 1].ToString()))
                    //{
                    //    InvertY = true;
                    //}
                    SimpleDataset testdataset = new SimpleDataset("Test", testX, testY);
                    try
                    {
                        testdataset.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, testX.Length, "Test");
                    }
                    catch
                    {
                    }
                    Array.Resize(ref arrTestDataset, arrTestDataset.Length + 1);
                    arrTestDataset[arrTestDataset.Length - 1] = testdataset;
                }




                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(arrTestDataset, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);
                //if (InvertX)
                //{
                //    pTransform1.InvertScaleX();
                //}
                //if (InvertY)
                //{
                //    pTransform1.InvertScaleY();
                //}
                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);


                xAxis2 = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                xAxis2.SetColor(_AxisColor);
                xAxis2.SetAxisIntercept(0);
                chartVu.AddChartObject(xAxis2);

                yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                yAxis2.SetColor(_AxisColor);
                yAxis2.SetAxisIntercept(0);
                chartVu.AddChartObject(yAxis2);

                xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                xgrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(xgrid2);

                ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                ygrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(ygrid2);


                for (int i = 0; i < arrTestDataset.Length; i++)
                {
                    attrib2 = new ChartAttribute(Color.FromArgb(-Convert.ToInt32(ColorTag[i])), 1, DashStyle.Solid, Color.FromArgb(150, Color.FromArgb(-Convert.ToInt32(ColorTag[i]))));
                    attrib2.SetLineColor(Color.FromArgb(-Convert.ToInt32(ColorTag[i])));
                    attrib2.SetFillFlag(_AreaFill);


                    thePlot1 = new SimpleLinePlot(pTransform1, arrTestDataset[i], attrib2);
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
                //attrib2 = new ChartAttribute(Color.DarkBlue, 1, DashStyle.Solid, Color.DarkBlue);
                //attrib2.SetFillFlag(false);
                //GraphicsPath rectpath = new GraphicsPath();
                //rectpath.AddArc((float)xAxis2.AxisMin, (float)yAxis2.AxisMin, (float)xAxis2.AxisMax * 2, (float)yAxis2.AxisMax * 2, 0, 270); //(linearRegionRect.GetRectangleF());
                //ChartShape linearRegionShape = new ChartShape(pTransform1, rectpath,1, 0.0, 0.0, ChartObj.PHYS_POS, 0);
                //linearRegionShape.SetChartObjAttributes(attrib2);
                //chartVu.AddChartObject(linearRegionShape);
                chartVu.SetResizeMode(ChartObj.AUTO_RESIZE_OBJECTS);

                Font toolTipFont = new Font("Arial", 10, FontStyle.Regular);
                DataToolTip datatooltip = new DataToolTip(chartVu);
                NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);
                chartVu.UpdateDraw();
                SelectNextPlot(Convert.ToInt32(ColorTag[0].ToString()));
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
        public void Unzoom()
        {
            int iTest = 0;

            try
            {
                //ChartZoom zoomObj = new ChartZoom(chartVu, pTransform1, true);

                do
                {
                    if (zoomObj != null)
                    {
                        iTest = zoomObj.PopZoomStack();
                        
                    }
                }
                while (iTest != 0);
                zoomObj = null;
                hScrollBar1.Visible = false;
               
                chartVu.SetCurrentMouseListener(datatooltip);
                //chartVu.UpdateDraw();
            }
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        public void ResetZoom()
        {
            try
            {
                zoomObj = null;

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
                    if (_ChartFooter != null)
                    {
                        ChartTitle footer = new ChartTitle(pTransform1, theFont, _ChartFooter);
                        footer.SetColor(_FooterColor);
                        footer.SetTitleType(ChartObj.CHART_FOOTER);
                        footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
                        footer.SetTitleOffset(8);
                        chartVu.AddChartObject(footer);
                    }
                    chartVu.Update();
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
                    SelectedCursor=null;
                    m_objDataCursor = null;
                }
                else
                {
                    SelectedCursor=Cursor;
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
                    }

                }

            }
            catch (Exception ex)
            {
            }
            return arlstToreturn;
        }
        public ArrayList GetAllPlotDataSet()
        {
            ArrayList arlstToreturn = new ArrayList();
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
                                SimpleDataset TestDataset = TestPlot.DisplayDataset;
                                //double[] X = 
                                //double[] Y = TestDataset.GetYData();
                                arlstToreturn.Add(TestDataset.GetXData());
                                arlstToreturn.Add(TestDataset.GetYData());
                            }
                            // chartVu.DeleteChartObject(objObject);

                        }
                    }
                    chartVu.UpdateDraw();
                }
            }
            catch (Exception ex)
            {
            }
            return arlstToreturn;
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
        protected override void OnMouseClick(MouseEventArgs e)
        {           
            try
            {
                if (this.GraphClicked != null)
                    this.GraphClicked(e);
            }
            catch (Exception ex)
            {
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptSecondPoint = null;
            Point2D ptPointGot = null;
            IDictionaryEnumerator objEnumerator = null;
            NearestPointData objSecondNearestPoint = null;
            bool bFirstPlot = false;
            bool bSecondPlot = true;

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
        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Right) != 0)
                {
                    base.OnMouseUp(e);
                }
                else if ((e.Button & MouseButtons.Left) != 0)
                {
                    MouseEventArgs _MouseEvent = e;
                    bool ok = true;
                    if (zoomObj != null)
                    {
                        int mx = e.X;
                        int my = e.Y;
                        int starting = (int)(chartVu.Width * .15);
                        int ending = (int)(chartVu.Width * .85);
                        if (mx < starting)
                        {
                            _MouseEvent = new MouseEventArgs(MouseButtons.Left, 1, starting+1, my, 0);
                            ok = false;
                        }
                        if (mx > ending)
                        {
                            _MouseEvent = new MouseEventArgs(MouseButtons.Left, 1, ending, my, 0);
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        base.OnMouseUp(e);
                    }
                    else
                    {
                        base.OnMouseUp(_MouseEvent);
                    }
                }
                else
                {
                    base.OnMouseUp(e);
                }
                double diff = 0;
                //if (startindex != 0)
                //{
                diff = Math.Abs((double)(m_objClickedPlot.DisplayDataset.GetXDataValue(0) - m_objClickedPlot.DisplayDataset.GetXDataValue(1)));
                double Index = zoomObj.ChartObjScale.GetScaleStartX() / diff;
                hScrollBar1.Value = (int)Index;
            }
            catch (Exception ex)
            {
            }
        }
        protected override void OnMouseDown(MouseEventArgs mouseevent)
        {
            try
            {
                // if right mouse buggon, pop zoom stack one level
                if ((mouseevent.Button & MouseButtons.Right) != 0)
                {
                    base.OnMouseDown(mouseevent);
                }
                else if ((mouseevent.Button & MouseButtons.Left) != 0)
                {
                    bool ok = true;
                    if (zoomObj != null)
                    {
                        int mx = mouseevent.X;
                        int my = mouseevent.Y;
                        int starting = (int)(chartVu.Width * .15);
                        int ending = (int)(chartVu.Width * .85);
                        if (mx < starting)
                        {
                            ok = false;
                        }
                        if (mx > ending)
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        base.OnMouseDown(mouseevent);
                    }
                }
                else
                {
                    base.OnMouseDown(mouseevent);
                }
            }
            catch (Exception ex)
            {
            }
        }
        void LineGraphControl_GraphClicked(MouseEventArgs e)
        {
            //Point2D obj2DPoint = null;
            //double dImplicitZValue = 0;

            //NearestPointData nearestPointObj1 = null;
            //Font textCoordsFont = null;
            //Point2D ptNewPoint = null;
            //Point2D ptLocation = null;
            //Point2D ptPointGot = null;
            //IDictionaryEnumerator objEnumerator = null;

            //try
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        if (m_objMarker != null && m_objNewMarker != null)
            //        {
            //            chartVu.DeleteChartObject(m_objNewMarker);
            //            chartVu.DeleteChartObject(m_objMarker);
            //        }

            //        chartVu.UpdateDraw();
            //        if (m_objSelectedPlot != null)
            //        {
            //            m_objOldSelectedPlot = m_objSelectedPlot;
            //        }
            //        if (m_objSelectedPlot.LineWidth == 2)
            //        {
            //            NumericLabel objLabel = m_objSelectedPlot.PlotLabelTemplate;
            //            Color clrClicked = m_objSelectedPlot.GetColor();
            //            //m_obj2DGraphControl.ParentForm.LabelColor = clrClicked;
            //        }
            //        else if (m_objDataCursor != null)
            //        {
            //            if (m_objNewPlot != null)
            //            {
            //                nearestPointObj1 = new NearestPointData();
            //                textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

            //                ptLocation = m_objDataCursor.Location;
            //                m_objNewPlot.CalcNearestPoint(ptLocation, 3, nearestPointObj1);

            //                ptNewPoint = nearestPointObj1.GetNearestPoint();


            //                if (m_objMarker != null)
            //                {
            //                    chartVu.DeleteChartObject(m_objMarker);
            //                }
            //                m_objMarker.SetColor(_MainCursorColor);
            //                m_objMarker.FillColor = _MainCursorColor;

            //                chartVu.AddChartObject(m_objMarker);


            //                if (m_objMarker != null)
            //                {
            //                    chartVu.UpdateDraw();
            //                }
            //            }
            //        }
            //        if (m_objSelectedPlot.LineWidth == 2)
            //        {
            //        }
            //    }//end if (e.Button == MouseButtons.Left))
            //    else if (e.Button == MouseButtons.Right)
            //    {
            //        m_objSelectedPlot.SetLineWidth(1);
            //        chartVu.UpdateDraw();
            //    }
            //    m_objClickedPlot = m_objSelectedPlot;
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            //}


        }
        void LineGraphControl_ThisMouseMove(MouseEventArgs e)
        {
            Point2D objClickedPoint = null;
            double dZValue = 0;
            Point2D objPoint3D = null;
            NearestPointData objNearestPoint = null;
            Point2D objLocationPoint = null;
            Point2D objGetPoint = null;
            String sDisplaytext = null;

            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptSecondPoint = null;
            Point2D ptPointGot = null;
            IDictionaryEnumerator objEnumerator = null;
            NearestPointData objSecondNearestPoint = null;
            bool bFirstPlot = false;
            bool bSecondPlot = true;
            SimpleDataset Dataset1 = null;
            Color DifferenceLineColor = Color.Black;

            try
            {

                Point2D objNewPoint = new Point2D();


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
                                //Point2D objPoint = Dataset1.GetDataPoint(m_iCounter);


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



                                if (SelectedCursor == "Difference Cursors")///////////////////////////////
                                {
                                    DifferenceLineColor = selectLineColor();
                                    if (m_objMarker != null)
                                    {
                                        chartVu.DeleteChartObject(m_objMarker);
                                        //chartVu.DeleteChartObject(m_objNewMarker);
                                        //m_objChartView.DeleteChartObject(objSecondLineMarker);
                                        chartVu.DeleteChartObject(m_objDataCursor);

                                    }//end(if (m_objMarker != null))
                                    if (arrmarkerCursor != null)
                                    {
                                        //for (int i = 0; i < arrmarkerCursor.Length; i++)
                                        try
                                        {
                                            chartVu.DeleteChartObject(arrmarkerCursor[_MainForm._SelectedRowIndex]);
                                            chartVu.DeleteChartObject(m_objDataCursor);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (arrChartTextCursor != null)
                                    {
                                        //for (int i1 = 0; i1 < arrChartTextCursor.Length; i1++)
                                        try
                                        {
                                            chartVu.DeleteChartObject(arrChartTextCursor[_MainForm._SelectedRowIndex]);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                else
                                {
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
                                    //m_objMarker.FillColor = Color.Black;
                                    //m_objMarker.SetColor(Color.Black);
                                }
                                BearingFFOverriddenRPM = ptNewPoint.GetX().ToString();
                                
                                switch (SelectedCursor)
                                {
                                    case "Difference Cursors":
                                        {
                                            if (arrmarkerCursor == null)
                                            {
                                                arrmarkerCursor = new Marker[_DataGridView.RowCount];
                                                arrChartTextCursor = new ChartText[_DataGridView.RowCount];
                                            }
                                            else if (arrmarkerCursor.Length != _DataGridView.RowCount)
                                            {
                                                arrmarkerCursor = new Marker[_DataGridView.RowCount];
                                                arrChartTextCursor = new ChartText[_DataGridView.RowCount];
                                            }
                                            
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(DifferenceLineColor);
                                            arrmarkerCursor[_MainForm._SelectedRowIndex] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                             
                                            ChartText CurrentLabel = null;

                                            //CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            _DataGridView[1, _MainForm._SelectedRowIndex].Value = ptNewPoint.GetX().ToString();

                                            CurrentLabel.SetColor(DifferenceLineColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[_MainForm._SelectedRowIndex] = CurrentLabel;

                                            chartVu.UpdateDraw();

                                            _DataGridView[2, _MainForm._SelectedRowIndex].Value = ptNewPoint.GetY().ToString();

                                            for (int k = 0; k < _DataGridView.RowCount; k++)
                                            {
                                                if (!string.IsNullOrEmpty((string)_DataGridView[1, k].Value))
                                                {
                                                    if (_XLabel == "Sec")
                                                    {
                                                        if (k != 0)
                                                        {
                                                            _DataGridView[3, k].Value = Convert.ToString(Convert.ToDouble(_DataGridView[1, k].Value.ToString()) - Convert.ToDouble(_DataGridView[1, 0].Value.ToString())) + "Sec / " + Convert.ToString(Math.Round(Convert.ToDouble(1 / (Convert.ToDouble(_DataGridView[1, k].Value.ToString()) - Convert.ToDouble(_DataGridView[1, 0].Value.ToString()))), 3)) + "Hz";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _DataGridView[3, k].Value = Convert.ToDouble(_DataGridView[1, k].Value.ToString()) - Convert.ToDouble(_DataGridView[1, 0].Value.ToString());
                                                    }
                                                    _DataGridView[4, k].Value = Convert.ToDouble(_DataGridView[2, k].Value.ToString()) - Convert.ToDouble(_DataGridView[2, 0].Value.ToString());
                                                }
                                            }

                                            //_DataGridView[3, _MainForm._SelectedRowIndex].Value = Convert.ToDouble(_DataGridView[1, _MainForm._SelectedRowIndex].Value.ToString()) - Convert.ToDouble(_DataGridView[1, 0].Value.ToString());
                                            //_DataGridView[4, _MainForm._SelectedRowIndex].Value = Convert.ToDouble(_DataGridView[2, _MainForm._SelectedRowIndex].Value.ToString()) - Convert.ToDouble(_DataGridView[2, 0].Value.ToString());
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "PeekCursor":
                                        {
                                            double harmonicX = ptNewPoint.GetX();
                                            arrmarkerCursor = new Marker[1];
                                            arrChartTextCursor = new ChartText[1];
                                            double[] PeekXdata = FindAllPeaks((double[])Dataset1.XData.GetDataBuffer(), (double[])Dataset1.YData.GetDataBuffer());
                                            int MainIndex = Array.FindIndex(PeekXdata, delegate(double item) { return item == harmonicX; });
                                            if (MainIndex == -1)
                                            {
                                                if (harmonicX <= PeekXdata[PeekXdata.Length - 1])
                                                {
                                                    harmonicX = _MainForm.FindNearest(PeekXdata, harmonicX);
                                                    MainIndex = Array.FindIndex(PeekXdata, delegate(double item) { return item == harmonicX; });
                                                }
                                            }
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, harmonicX, Dataset1.YData.GetDataBuffer()[PeekIndex[MainIndex]], 8, 1);



                                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);
                                            ChartText CurrentLabel = null;
                                            _DataGridView.Rows.Add(1);
                                            
                                            {
                                                //CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                                CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData.GetDataBuffer()[PeekIndex[MainIndex]]), 5)) + YLabel, harmonicX, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                                _DataGridView[0, 0].Value = harmonicX.ToString();
                                            }
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;



                                            _DataGridView[1, 0].Value = Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData.GetDataBuffer()[PeekIndex[MainIndex]]), 5));
                                            _DataGridView.Refresh();

                                            m_iCounter = MainIndex;
                                            break;
                                        }
                                    case "Kill Cursor":
                                        {
                                            arrmarkerCursor = new Marker[1];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            //ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                            //arrmarkerCursor = new Marker[1];
                                            //arrChartTextCursor = new ChartText[1];
                                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            //m_objMarker1.SetColor(_MainCursorColor);
                                            //arrmarkerCursor[0] = m_objMarker1;
                                            //chartVu.AddChartObject(m_objMarker1);
                                            //ChartText CurrentLabel = null;
                                            //_DataGridView.Rows.Add(1);
                                            //if (_MainForm._IsOverallTrend)
                                            //{
                                            //    //CurrentLabel = new ChartText(pTransform1, theFont,xdatalabels[(int)ptNewPoint.GetX()-1].ToString()  + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            //    CurrentLabel = new ChartText(pTransform1, theFont, xdatalabels[(int)ptNewPoint.GetX() - 1].ToString() + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            //    _DataGridView[0, 0].Value = xdatalabels[(int)ptNewPoint.GetX() - 1].ToString();
                                            //}
                                            //else
                                            //{
                                            //    //CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            //    CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            //    _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            //}
                                            //CurrentLabel.SetColor(_MainCursorColor);
                                            //chartVu.AddChartObject(CurrentLabel);
                                            //arrChartTextCursor[0] = CurrentLabel;



                                            //_DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            //_DataGridView.Refresh();
                                            //break;
                                        }
                                    case "Single":
                                        {
                                            arrmarkerCursor = new Marker[1];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            //ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();                                            
                                            break;
                                        }
                                    case "Harmonic":
                                        {
                                            double harmonicX = ptNewPoint.GetX();
                                            double lastx = Dataset1.XData[Dataset1.XData.Length - 1];
                                            arrmarkerCursor = new Marker[0];
                                            arrChartTextCursor = new ChartText[0];
                                            if (harmonicX < (double)(lastx * .02))
                                            {
                                                harmonicX = (double)(lastx * .02);
                                            }
                                            double constantHarmonicX = harmonicX;
                                            while (harmonicX <= lastx)
                                            {
                                                int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == harmonicX; });
                                                if (MainIndex == -1)
                                                {
                                                    if (harmonicX <= Dataset1.XData[Dataset1.XData.Length - 1])
                                                    {
                                                        harmonicX = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), harmonicX);
                                                        MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == harmonicX; });
                                                    }
                                                }
                                                Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, harmonicX, Dataset1.YData[MainIndex], 8, 1);

                                                //Array.Resize(ref arrmarkerCursor, arrmarkerCursor.Length + 1);
                                                _ResizeArray.IncreaseArrayLinePlotMarker2D(ref arrmarkerCursor, 1);
                                                arrmarkerCursor[arrmarkerCursor.Length - 1] = m_objMarker1;


                                                //ChartText CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(Dataset1.YData[MainIndex].ToString()) + YLabel, harmonicX, Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                                ChartText CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(Dataset1.YData[MainIndex].ToString()) + YLabel, harmonicX, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                                CurrentLabel.SetColor(_MainCursorColor);
                                                chartVu.AddChartObject(CurrentLabel);

                                                //Array.Resize(ref arrChartTextCursor, arrChartTextCursor.Length + 1);
                                                _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartTextCursor, 1);
                                                arrChartTextCursor[arrChartTextCursor.Length - 1] = CurrentLabel;

                                                //arrChartTextCursor[0] = CurrentLabel;

                                                m_objMarker1.FillColor = _MainCursorColor;
                                                m_objMarker1.SetColor(_MainCursorColor);


                                                chartVu.AddChartObject(m_objMarker1);

                                                _DataGridView.Rows.Add(1);
                                                _DataGridView[0, _DataGridView.Rows.Count - 2].Value = harmonicX.ToString();
                                                _DataGridView[1, _DataGridView.Rows.Count - 2].Value = Dataset1.YData[MainIndex].ToString();
                                                _DataGridView.Refresh();
                                                harmonicX += constantHarmonicX;
                                            }

                                            break;
                                        }
                                    case "Single With Square":
                                        {
                                            arrmarkerCursor = new Marker[2];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);
                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
                                            arrmarkerCursor = new Marker[2];
                                            arrChartTextCursor = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_HLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);
                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "Sideband":
                                        {
                                            arrmarkerCursor = new Marker[3];
                                            arrChartTextCursor = new ChartText[3];
                                            
                                            string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                            int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                                            double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                                            double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                            if (MainIndex == -1)
                                            {
                                                if (LowerLimit >= Dataset1.XData[0])
                                                {
                                                    LowerLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), LowerLimit);
                                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                                }
                                            }

                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, Dataset1.YData[MainIndex], 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[1] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                                            _DataGridView[1, _DataGridView.Rows.Count - 2].Value = Dataset1.YData[MainIndex].ToString();

                                            MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                                            if (MainIndex == -1)
                                            {
                                                if (UpperLimit <= Dataset1.XData[Dataset1.XData.Length - 1])
                                                {
                                                    UpperLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), UpperLimit);
                                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                                                }
                                            }

                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, Dataset1.YData[MainIndex], 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[2] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[2] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                                            _DataGridView[1, _DataGridView.Rows.Count - 2].Value = Dataset1.YData[MainIndex].ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "SidebandRatio":
                                        {
                                            arrmarkerCursor = new Marker[3];
                                            arrChartTextCursor = new ChartText[3];

                                            string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                            int Pctg = Convert.ToInt32(RatioExtractor[1]); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                                            double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * 1 / Pctg);
                                            double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * 1 / Pctg);

                                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(3);
                                            _DataGridView[0, 0].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();

                                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                            if (MainIndex == -1)
                                            {
                                                if (LowerLimit >= Dataset1.XData[0])
                                                {
                                                    LowerLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), LowerLimit);
                                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                                }
                                            }

                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, Dataset1.YData[MainIndex], 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[1] = CurrentLabel;

                                            _DataGridView[0, 1].Value = LowerLimit.ToString();
                                            _DataGridView[1, 1].Value = Dataset1.YData[MainIndex].ToString();

                                            MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                                            if (MainIndex == -1)
                                            {
                                                if (UpperLimit <= Dataset1.XData[Dataset1.XData.Length - 1])
                                                {
                                                    UpperLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), UpperLimit);
                                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                                                }
                                            }
                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, Dataset1.YData[MainIndex], 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[2] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[2] = CurrentLabel;

                                            _DataGridView[0, 2].Value = UpperLimit.ToString();
                                            _DataGridView[1, 2].Value = Dataset1.YData[MainIndex].ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "SideBandTrend":
                                        {
                                            arrmarkerCursor = new Marker[3];
                                            arrChartTextCursor = new ChartText[3];
                                            double TrendValue = 10;
                                            double TrendFreq = 100;
                                            double iConstSBTrendFreq = 0;
                                            if (objTrend != null)
                                            {
                                                TrendValue = Convert.ToDouble(objTrend._Value.ToString());
                                                TrendFreq = objTrend._Freq;
                                            }
                                            
                                            iConstSBTrendFreq = (TrendFreq * TrendValue) / 100;
                                            //int MainX = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                            //if (MainX == -1)
                                            //{
                                            //    if (LowerLimit >= Dataset1.XData[0])
                                            //    {
                                            //        LowerLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), LowerLimit);
                                            //        MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                            //    }
                                            //}

                                            //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                            //int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                                            double LowerLimit = ptNewPoint.GetX() - iConstSBTrendFreq;// (ptNewPoint.GetX() * Pctg / 100);
                                            double UpperLimit = ptNewPoint.GetX() + iConstSBTrendFreq;// (ptNewPoint.GetX() * Pctg / 100);

                                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                                            _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                            if (MainIndex == -1)
                                            {
                                                if (LowerLimit >= Dataset1.XData[0])
                                                {
                                                    LowerLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), LowerLimit);
                                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                                                }
                                            }

                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, Dataset1.YData[MainIndex], 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[1] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                                            _DataGridView[1, _DataGridView.Rows.Count - 2].Value = Dataset1.YData[MainIndex].ToString();

                                            MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                                            if (MainIndex == -1)
                                            {
                                                if (UpperLimit <= Dataset1.XData[Dataset1.XData.Length - 1])
                                                {
                                                    UpperLimit = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), UpperLimit);
                                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                                                }
                                            }

                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, Dataset1.YData[MainIndex], 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarkerCursor[2] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(Dataset1.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(_MainCursorColor);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartTextCursor[2] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                                            _DataGridView[1, _DataGridView.Rows.Count - 2].Value = Dataset1.YData[MainIndex].ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                }
                                if (_IsBearingFF)
                                {
                                    _MainForm.callBFF();
                                }
                               
                                if (m_objMarker != null)
                                {                                   
                                    chartVu.UpdateDraw();
                                }//end(if (objMarker != null))
                            }
                        }


                        //m_iCounter++;

                        //int iNumber = Dataset1.GetNumberDatapoints();
                        //if (m_iCounter > iNumber)
                        //    m_iCounter = iNumber;
                    }
                    else
                    {
                        if (SelectBandTrend)
                        {
                            arrmarkerCursor = new Marker[1];
                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                            m_objMarker1.SetColor(_MainCursorColor);
                            arrmarkerCursor[0] = m_objMarker1;
                            chartVu.AddChartObject(m_objMarker1);
                            chartVu.UpdateDraw();

                            objTrend.FreqVal = ptNewPoint.GetX().ToString();
                        }
                        //  MessageBox.Show("Please select DataCursor", "Select Data Cursor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//end if (e.Button == MouseButtons.Left))
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }
        
        internal bool AreaGraph()
        {
            bool bToReturn = false;
            try
            {
                //thePlot1.ChartObjAttributes.SetFillFlag(_AreaFill);
                //chartVu.UpdateDraw();
                {
                    if (m_objClickedPlot != null)// && m_objClickedPlot.LineWidth == 2)
                    {
                        m_objClickedPlot.ChartObjAttributes.SetFillFlag(_AreaFill);
                        chartVu.UpdateDraw();
                        bToReturn = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
            return bToReturn;
        }
        internal bool DrawFaultFrequencies(bool p, string[] Frequencies, DataGridView _Datagrid)
        {
            bool bToReturn = false;
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            NearestPointData objSecondNearestPoint = null;
            SimpleDataset Dataset1 = null;
            try
            {
                if (p)
                {
                    _Datagrid.Rows.Clear();
                    if (arrmarker != null)
                    {
                        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrmarker[i1]);
                            chartVu.DeleteChartObject(m_objDataCursor);
                        }
                    }
                    arrmarker = new Marker[0];
                    if (arrChartText != null)
                    {
                        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartText[i1]);
                        }
                    }
                    arrChartText = new ChartText[0];
                    for (i = 0; i < Frequencies.Length; i++)
                    {
                        string[] ExtractFreqSingle = Frequencies[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        double Comparator = Convert.ToDouble(ExtractFreqSingle[1]);
                        if (Comparator.ToString() == "NaN")
                        {
                            break;
                        }
                        if (_XLabel == "CPM")
                        {
                            Comparator = Comparator * 60;
                        }
                        if (m_objClickedPlot != null)
                        {

                            Dataset1 = m_objClickedPlot.DisplayDataset;
                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == Comparator; });
                            if (MainIndex == -1)
                            {
                                if (Comparator <= Dataset1.XData[Dataset1.XData.Length - 1])
                                {
                                    Comparator = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), Comparator);
                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == Comparator; });
                                }
                            }

                            nearestPointObj1 = new NearestPointData();
                            objSecondNearestPoint = new NearestPointData();
                            textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                            Font theLabelFont = new Font("Arial", 8, FontStyle.Regular);
                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, Dataset1.XData[MainIndex], Dataset1.YData[MainIndex], 8, 1);
                            m_objMarker1.LineStyle = DashStyle.DashDotDot;
                            
                            if (MainIndex != -1)
                            {

                                //Array.Resize(ref arrmarker, arrmarker.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlotMarker2D(ref arrmarker, 1);
                                arrmarker[arrmarker.Length - 1] = m_objMarker1;

                                m_objMarker1.FillColor = Color.DarkCyan;
                                m_objMarker1.SetColor(Color.DarkCyan);


                                chartVu.AddChartObject(m_objMarker1);
                                ChartText CurrentLabel = new ChartText(pTransform1, theLabelFont, ExtractFreqSingle[0].ToString() + " -> " + Convert.ToString(Math.Round(Dataset1.XData[MainIndex], 5)) + _XLabel + " / " + Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5)) + _YLabel, Dataset1.XData[MainIndex], pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                CurrentLabel.SetColor(Color.Black);
                                chartVu.AddChartObject(CurrentLabel);

                                //Array.Resize(ref arrChartText, arrChartText.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartText, 1);
                                arrChartText[arrChartText.Length - 1] = CurrentLabel;


                                _Datagrid.Rows.Add(1);
                                _Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[0].Value = ExtractFreqSingle[0].ToString();
                                _Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[1].Value = Convert.ToString(Math.Round(Dataset1.XData[MainIndex], 5));
                                _Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[2].Value = Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5));
                                //ExactBearingFF[i] = Convert.ToDouble(dataGridView3.Rows[dataGridView3.Rows.Count - 2].Cells[1].Value.ToString());
                                try
                                {
                                    _MainForm._ExactBearingFF[i] = Convert.ToDouble(_Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[1].Value.ToString());
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            bToReturn = true;
                            chartVu.UpdateDraw();
                        }
                    }
                }
                else
                {
                    if (arrmarker != null)
                    {
                        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrmarker[i1]);
                            chartVu.DeleteChartObject(m_objDataCursor);
                        }
                    }
                    if (arrChartText != null)
                    {
                        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartText[i1]);
                        }
                    }
                    chartVu.UpdateDraw();
                }
            }
            catch (Exception ex)
            {
            }
            return bToReturn;
        }

        public void DrawdbRegion(double meanY)
        {
            double xx = 0;//starting x
            double yy = meanY;//starting y
            double ww = pTransform1.ScaleMaxX;//width
            double hh = pTransform1.ScaleMaxY;//height

            Color alphaRedColor = Color.FromArgb(100, Color.Red);
            ChartAttribute attribRed = new ChartAttribute(alphaRedColor, 1, DashStyle.Solid, alphaRedColor);
            attribRed.SetFillFlag(true);

            Rectangle2D linearRegionRect = new Rectangle2D(xx, yy, ww, hh);
            GraphicsPath rectpath = new GraphicsPath();
            rectpath.AddRectangle(linearRegionRect.GetRectangleF());
            ChartShape linearRegionShape = new ChartShape(pTransform1, rectpath,ChartObj.PHYS_POS, 0.0, 0.0, ChartObj.PHYS_POS, 0);
            linearRegionShape.SetChartObjAttributes(attribRed);
            chartVu.AddChartObject(linearRegionShape);
            
            chartVu.UpdateDraw();

        }
        internal bool DrawBandRegion(string[] BandData, DataGridViewX dataGridView1, bool IsBandAreaPlot)
        {
            bool bToReturn = false;
            try
            {
                if (IsBandAreaPlot)
                {
                    dataGridView1.Rows.Clear();
                    double xx = 0;
                    double yy = 0;
                    double ww = 0;
                    double hh = 0;
                    double REDxx = 0;
                    double REDyy = 0;
                    double REDww = 0;
                    double REDhh = 0;
                    arrChartShape = new ChartShape[0];
                    arrChartShape1 = new ChartShape[0];
                    Color alphaColor = Color.FromArgb(100, Color.Yellow);
                    ChartAttribute attrib2 = new ChartAttribute(alphaColor, 1, DashStyle.Solid, alphaColor);
                    attrib2.SetFillFlag(true);
                    Color alphaRedColor = Color.FromArgb(100, Color.Red);
                    ChartAttribute attribRed = new ChartAttribute(alphaRedColor, 1, DashStyle.Solid, alphaRedColor);
                    attribRed.SetFillFlag(true);
                    OriginalYMaxscale = pTransform1.ScaleMaxY;
                    for (int i = 0; i < BandData.Length; i++)
                    {
                        

                        string[] splittedBandData = BandData[i].ToString().Split(new string[] { "!", "@" }, StringSplitOptions.RemoveEmptyEntries);

                        double sp0 = Convert.ToDouble(splittedBandData[0].ToString());
                        double sp1 = Convert.ToDouble(splittedBandData[1].ToString());
                        double sp2 = Convert.ToDouble(splittedBandData[2].ToString());
                        if (_XLabel == "CPM")
                        {
                            sp0 = sp0 * 60;
                        }
                        //if (i != 0)
                        {
                            xx += ww;
                        }
                        ww = sp0 - xx;
                        yy = sp2;
                        hh = sp1 - yy;
                        if (pTransform1.ScaleMaxY < (yy + hh))
                        {
                            pTransform1.SetScaleY(0, (yy + hh));
                            pTransform1.YScale.SetRangeFromStop(yy + hh);
                            chartVu.DeleteChartObject(yAxis2);
                            chartVu.DeleteChartObject(xgrid2);
                            chartVu.DeleteChartObject(ygrid2);
                            chartVu.DeleteChartObject(yAxisLab2);
                            

                            yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                            yAxis2.SetColor(_AxisColor);
                            chartVu.AddChartObject(yAxis2);

                           

                            xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                            xgrid2.SetColor(_AxisColor);
                            chartVu.AddChartObject(xgrid2);

                            ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                            ygrid2.SetColor(_AxisColor);
                            chartVu.AddChartObject(ygrid2);


                            yAxisLab2 = new NumericAxisLabels(yAxis2);
                            yAxisLab2.SetColor(_AxisColor);
                            chartVu.AddChartObject(yAxisLab2);
                            
                        }
                        Rectangle2D linearRegionRect = new Rectangle2D(xx, yy, ww, hh);
                        GraphicsPath rectpath = new GraphicsPath();
                        rectpath.AddRectangle(linearRegionRect.GetRectangleF());
                        ChartShape linearRegionShape = new ChartShape(pTransform1, rectpath,
                            ChartObj.PHYS_POS, 0.0, 0.0, ChartObj.PHYS_POS, 0);
                        linearRegionShape.SetChartObjAttributes(attrib2);
                        chartVu.AddChartObject(linearRegionShape);
                        //Array.Resize(ref arrChartShape, arrChartShape.Length + 1);
                        _ResizeArray.IncreaseArrayLinePlotChartShape2D(ref arrChartShape, 1);
                        arrChartShape[arrChartShape.Length - 1] = linearRegionShape;

                        REDxx = xx;
                        REDyy = yy + hh;
                        REDww = ww;
                        REDhh = pTransform1.ScaleMaxY - REDyy;
                        if (REDhh > 0)
                        {
                            linearRegionRect = new Rectangle2D(REDxx, REDyy, REDww, REDhh);
                            rectpath = new GraphicsPath();
                            rectpath.AddRectangle(linearRegionRect.GetRectangleF());
                            linearRegionShape = new ChartShape(pTransform1, rectpath,
                                ChartObj.PHYS_POS, 0.0, 0.0, ChartObj.PHYS_POS, 0);
                            linearRegionShape.SetChartObjAttributes(attribRed);
                            chartVu.AddChartObject(linearRegionShape);
                            //Array.Resize(ref arrChartShape1, arrChartShape1.Length + 1);
                            _ResizeArray.IncreaseArrayLinePlotChartShape2D(ref arrChartShape1, 1);

                            arrChartShape1[arrChartShape1.Length - 1] = linearRegionShape;
                        }
                        chartVu.UpdateDraw();
                        bToReturn = true;
                    }                    
                }
                else
                {
                    if(pTransform1.ScaleMaxY!=OriginalYMaxscale)
                    {
                        pTransform1.SetScaleY(0, OriginalYMaxscale);
                        pTransform1.YScale.SetRangeFromStop(OriginalYMaxscale);

                        chartVu.DeleteChartObject(yAxis2);
                        chartVu.DeleteChartObject(yAxisLab2);

                        yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                        yAxis2.SetColor(_AxisColor);
                        chartVu.AddChartObject(yAxis2);



                        xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                        xgrid2.SetColor(_AxisColor);
                        chartVu.AddChartObject(xgrid2);

                        ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                        ygrid2.SetColor(_AxisColor);
                        chartVu.AddChartObject(ygrid2);


                        yAxisLab2 = new NumericAxisLabels(yAxis2);
                        yAxisLab2.SetColor(_AxisColor);

                        chartVu.AddChartObject(yAxisLab2);
                    }
                    if (arrChartShape != null)
                    {
                        for (int i1 = 0; i1 < arrChartShape.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartShape[i1]);                            
                        }
                    }
                    if (arrChartShape1 != null)
                    {
                        for (int i1 = 0; i1 < arrChartShape1.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartShape1[i1]);
                        }
                    }
                    chartVu.UpdateDraw();
                }
                
            }
            catch (Exception ex)
            {
                
            }
            return bToReturn;
        }
        internal bool DrawRPMmarkers(bool p, double FinalFreq, DataGridView _Datagrid, int CountForRpm)
        {
            bool bToReturn = false;           
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;            
            NearestPointData objSecondNearestPoint = null;           
            SimpleDataset Dataset1 = null;
            int PrvsMainIndex = 0;
            try
            {
                if (p)
                {
                    _Datagrid.Rows.Clear();
                    if (arrmarker != null)
                    {
                        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrmarker[i1]);
                            chartVu.DeleteChartObject(m_objDataCursor);
                        }
                    }
                    arrmarker = new Marker[CountForRpm];
                    if (arrChartText != null)
                    {
                        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartText[i1]);
                        }
                    }
                    arrChartText = new ChartText[0];

                    if (m_objClickedPlot != null)
                        {
                            Dataset1 = m_objClickedPlot.DisplayDataset;
                         
                        //double FinalFreq = (double)((double)iRPM / (double)(iPulse * 60));
                        //int CountForRpm = _RPMCount;
                        
                       // StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                        

                        for (int i = 0; i < CountForRpm; i++)
                        {
                            double FreqToCalc = FinalFreq * (1 + i);
                            if (_XLabel=="CPM")
                            {
                                FreqToCalc = FreqToCalc * 60;
                            }
                            if (FreqToCalc > (double)Dataset1.XData[Dataset1.XData.Length - 1])
                            {
                                break;
                            }
                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == FreqToCalc; });
                            
                            if (MainIndex == -1)
                            {
                                if (FreqToCalc <= Dataset1.XData[Dataset1.XData.Length - 1])
                                {
                                    FreqToCalc = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), FreqToCalc);
                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == FreqToCalc; });
                                }
                            }
                            nearestPointObj1 = new NearestPointData();
                            objSecondNearestPoint = new NearestPointData();
                            textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, Dataset1.XData[MainIndex], Dataset1.YData[MainIndex], 8, 1);
                            //arrmarker[i] = m_objMarker1;

                            //m_objMarker1.FillColor = _MainCursorColor;
                            //m_objMarker1.SetColor(_MainCursorColor);
                            //chartVu.AddChartObject(m_objMarker1);
                            if (PrvsMainIndex != MainIndex)
                            {
                                Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, Dataset1.XData[MainIndex], Dataset1.YData[MainIndex], 8, 1);
                                arrmarker[i] = m_objMarker1;
                                m_objMarker1.LineStyle = DashStyle.DashDot;
                                m_objMarker1.FillColor = Color.DarkKhaki;
                                m_objMarker1.SetColor(Color.DarkKhaki);
                                chartVu.AddChartObject(m_objMarker1);

                                Font theLabelFont = new Font("Arial", 8, FontStyle.Regular);
                                ChartText CurrentLabel = new ChartText(pTransform1, theLabelFont, Convert.ToString(i + 1) + "x RPM " + Convert.ToString(Math.Round(Dataset1.XData[MainIndex], 5)) + " " + _XLabel+" / " + Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5))+" "+_YLabel, Dataset1.XData[MainIndex], pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_MIN, ChartObj.JUSTIFY_MIN, 270);
                                CurrentLabel.SetColor(Color.Black);
                                chartVu.AddChartObject(CurrentLabel);
                                //Array.Resize(ref arrChartText, arrChartText.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartText, 1);
                                arrChartText[arrChartText.Length - 1] = CurrentLabel;


                                //Names[Names.Length - 1] = Convert.ToString(i + 1) + "x RPM   " + Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5));
                                _Datagrid.Rows.Add(1);
                                _Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[0].Value = Convert.ToString(i + 1) + "x RPM";
                                _Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[1].Value = Convert.ToString(Math.Round(Dataset1.XData[MainIndex], 5));
                                _Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[2].Value = Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5));
                                PrvsMainIndex = MainIndex;
                            }
                            else
                                break;
                            bToReturn = true;
                            chartVu.UpdateDraw();
                        }
                    }
                }
                else
                {
                    if (arrmarker != null)
                    {
                        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrmarker[i1]);
                            chartVu.DeleteChartObject(m_objDataCursor);
                        }
                    }


                    if (arrChartText != null)
                    {
                        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartText[i1]);
                        }
                    }
                    chartVu.UpdateDraw();
                }
            }
            catch (Exception ex)
            {
            }
            return bToReturn;
        }
                
        internal void SideBandTrendClicked()
        {
            try
            {
                objTrend = new SideBandTrend();
                objTrend._Form1 = MainForm;
                objTrend.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }
        internal void StartZoom()
        {
            try
            {
                if (zoomObj == null)
                {


                    zoomObj = new ChartZoom(chartVu, pTransform1, true);

                    zoomObj.SetButtonMask(MouseButtons.Left);
                    zoomObj.SetZoomYEnable(false);
                    zoomObj.SetZoomXEnable(true);

                    zoomObj.SetZoomXRoundMode(ChartObj.AUTOAXES_EXACT);
                    zoomObj.SetZoomYRoundMode(ChartObj.AUTOAXES_EXACT);
                    zoomObj.InternalZoomStackProcesssing = true;

                    zoomObj.SetEnable(true);

                    hScrollBar1.Visible = true;
                    hScrollBar1.Maximum = m_objClickedPlot.DisplayDataset.GetNumberDatapoints();
                  
                }

                
                //else if(zoomObj.PopZoomStack()==0)
                //{
                //    zoomObj = null;
                //    StartZoom();
                //}
                // set range limits to 1000 ms, 1 degree
                //		zoomObj.SetZoomRangeLimitsRatio(new Dimension(1.0, 1.0));
                chartVu.SetCurrentMouseListener(zoomObj);
                chartVu.UpdateDraw();

            }
            catch (Exception ex)
            {
            }
        }
        internal void StopZoom()
        {
            try
            {
                
                //int k= zoomObj.PushZoomStack();
                ChartZoom zoomObj1 = new ChartZoom(chartVu, pTransform1, true);
                zoomObj1.SetButtonMask(MouseButtons.None);
                zoomObj1.SetZoomYEnable(false);
                zoomObj1.SetZoomXEnable(false);
                zoomObj1.SetZoomXRoundMode(ChartObj.AUTOAXES_EXACT);
                zoomObj1.SetZoomYRoundMode(ChartObj.AUTOAXES_EXACT);
                zoomObj1.InternalZoomStackProcesssing = true;

                //zoomObj.SetEnable(true);
                // set range limits to 1000 ms, 1 degree
                //		zoomObj.SetZoomRangeLimitsRatio(new Dimension(1.0, 1.0));
                chartVu.SetCurrentMouseListener(zoomObj1);
                chartVu.UpdateDraw();
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
                    MessageBoxEx.Show("Graph Copied on ClipBoard", "Graph");
                }
            }
            catch (Exception ex)
            {
            }
        }
        private Color selectLineColor()
        {
            Color ReturnColor = Color.Black;
            try
            {
                int SelectedRowIndex = MainForm._SelectedRowIndex;
                int colorIndex = SelectedRowIndex % 10;
                switch (colorIndex)
                {
                    case 0:
                        {
                            ReturnColor = Color.Black;
                            break;
                        }
                    case 1:
                        {
                            ReturnColor = Color.Blue;
                            break;
                        }
                    case 2:
                        {
                            ReturnColor = Color.Red;
                            break;
                        }
                    case 3:
                        {
                            ReturnColor = Color.Green;
                            break;
                        }
                    case 4:
                        {
                            ReturnColor = Color.Brown;
                            break;
                        }
                    case 5:
                        {
                            ReturnColor = Color.DarkCyan;
                            break;
                        }
                    case 6:
                        {
                            ReturnColor = Color.DarkOrange;
                            break;
                        }
                    case 7:
                        {
                            ReturnColor = Color.DeepPink;
                            break;
                        }
                    case 8:
                        {
                            ReturnColor = Color.DarkViolet;
                            break;
                        }
                    case 9:
                        {
                            ReturnColor = Color.DarkGray;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }
            return ReturnColor;
        }
        public SimpleLinePlot[] GetAllPlots()
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
                                _ResizeArray.IncreaseArrayLinePlot2D(ref plotlist, 1);
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
                    case "Harmonic":
                        {
                            CheckKeyDownHarmonic(Direction);
                            break;
                        }
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
                    case "Sideband":
                        {
                            CheckKeyDownSideBandValue(Direction);
                            break;
                        }
                    case "SidebandRatio":
                        {
                            CheckKeyDownSideBandRatio(Direction);
                            break;
                        }
                    case "SideBandTrend":
                        {
                            CheckKeyDownSideBandTrend(Direction);
                            break;
                        }
                    case "PeekCursor":
                        {
                            CheckKeydownPeekCursor(Direction);
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void CheckKeydownPeekCursor(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
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

                        double[] PeekXdata = FindAllPeaks((double[])objDataSet.XData.GetDataBuffer(), (double[])objDataSet.YData.GetDataBuffer());
                        if (m_iCounter >= PeekXdata.Length)
                            m_iCounter = PeekXdata.Length - 1;
                        Point2D objPoint = new Point2D(PeekXdata[m_iCounter], objDataSet.YData.GetDataBuffer()[m_iCounter]); //objDataSet.GetDataPoint(m_iCounter);
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




                        double harmonicX = objPoint.GetX();
                        arrmarkerCursor = new Marker[1];
                        arrChartTextCursor = new ChartText[1];
                        int MainIndex = Array.FindIndex(PeekXdata, delegate(double item) { return item == harmonicX; });
                        if (MainIndex == -1)
                        {
                            if (harmonicX <= PeekXdata[PeekXdata.Length - 1])
                            {
                                harmonicX = _MainForm.FindNearest(PeekXdata, harmonicX);
                                MainIndex = Array.FindIndex(PeekXdata, delegate(double item) { return item == harmonicX; });
                            }
                        }
                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, harmonicX, objDataSet.YData.GetDataBuffer()[PeekIndex[MainIndex]], 8, 1);



                        //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                        m_objMarker.SetColor(_MainCursorColor);
                        arrmarkerCursor[0] = m_objMarker;
                        chartVu.AddChartObject(m_objMarker);
                        ChartText CurrentLabel = null;
                        _DataGridView.Rows.Add(1);
                        
                        {
                            //CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                            CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData.GetDataBuffer()[PeekIndex[MainIndex]]), 5)) + YLabel, harmonicX, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                            _DataGridView[0, 0].Value = harmonicX.ToString();
                        }
                        CurrentLabel.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartTextCursor[0] = CurrentLabel;



                        _DataGridView[1, 0].Value = Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData.GetDataBuffer()[PeekIndex[MainIndex]]), 5));
                        _DataGridView.Refresh();
                        // break;















                        //m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                        //m_objMarker.SetColor(Color.Black);
                        //chartVu.AddChartObject(m_objMarker);

                        //ChartText CurrentLabel = null;
                        //_DataGridView.Rows.Add(1);
                        //if (_MainForm._IsOverallTrend)
                        //{
                        //    CurrentLabel = new ChartText(pTransform1, theFont, xdatalabels[(int)objPoint.GetX() - 1].ToString() + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        //    _DataGridView[0, 0].Value = xdatalabels[(int)objPoint.GetX() - 1].ToString();
                        //}
                        //else
                        //{
                        //    CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        //    _DataGridView[0, 0].Value = objPoint.GetX().ToString();
                        //}
                        //CurrentLabel.SetColor(_MainCursorColor);
                        //chartVu.AddChartObject(CurrentLabel);
                        //arrChartTextCursor[0] = CurrentLabel;



                        //_DataGridView[1, 0].Value = objPoint.GetY().ToString();
                        //_DataGridView.Refresh();

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
                    //Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    double[] PeekXdata = FindAllPeaks((double[])objPreviousPointDataSet.XData.GetDataBuffer(), (double[])objPreviousPointDataSet.YData.GetDataBuffer());

                    Point2D objPoint = new Point2D(PeekXdata[m_iCounter], objPreviousPointDataSet.YData.GetDataBuffer()[m_iCounter]); //objDataSet.GetDataPoint(m_iCounter);

                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }


                    double harmonicX = objPoint.GetX();
                    arrmarkerCursor = new Marker[1];
                    arrChartTextCursor = new ChartText[1];
                    int MainIndex = Array.FindIndex(PeekXdata, delegate(double item) { return item == harmonicX; });
                    if (MainIndex == -1)
                    {
                        if (harmonicX <= PeekXdata[PeekXdata.Length - 1])
                        {
                            harmonicX = _MainForm.FindNearest(PeekXdata, harmonicX);
                            MainIndex = Array.FindIndex(PeekXdata, delegate(double item) { return item == harmonicX; });
                        }
                    }
                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, harmonicX, objPreviousPointDataSet.YData.GetDataBuffer()[PeekIndex[MainIndex]], 8, 1);



                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker;
                    chartVu.AddChartObject(m_objMarker);
                    ChartText CurrentLabel = null;
                    _DataGridView.Rows.Add(1);
                    
                    {
                        //CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData.GetDataBuffer()[PeekIndex[MainIndex]]), 5)) + YLabel, harmonicX, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        _DataGridView[0, 0].Value = harmonicX.ToString();
                    }
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;



                    _DataGridView[1, 0].Value = Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData.GetDataBuffer()[PeekIndex[MainIndex]]), 5));
                    _DataGridView.Refresh();
                    // break;




                    //m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    //m_objMarker.SetColor(Color.Black);

                    //chartVu.AddChartObject(m_objMarker);


                    //ChartText CurrentLabel = null;
                    //_DataGridView.Rows.Add(1);
                    //if (_MainForm._IsOverallTrend)
                    //{
                    //    CurrentLabel = new ChartText(pTransform1, theFont, xdatalabels[(int)objPoint.GetX() - 1].ToString() + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    //    _DataGridView[0, 0].Value = xdatalabels[(int)objPoint.GetX() - 1].ToString();
                    //}
                    //else
                    //{
                    //    CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    //    _DataGridView[0, 0].Value = objPoint.GetX().ToString();
                    //}
                    //CurrentLabel.SetColor(_MainCursorColor);
                    //chartVu.AddChartObject(CurrentLabel);
                    //arrChartTextCursor[0] = CurrentLabel;



                    //_DataGridView[1, 0].Value = objPoint.GetY().ToString();
                    //_DataGridView.Refresh();

                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarkerCursor = new Marker[1];
                arrmarkerCursor[0] = m_objMarker;
                chartVu.UpdateDraw();
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownSideBandTrend(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                _DataGridView.Rows.Add(1);
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

                    m_iCounter++;
                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);

                    objDataSet = objClickedLine.DisplayDataset;
                    int iNumber = objDataSet.GetNumberDatapoints();
                    if (m_iCounter >= iNumber)
                        m_iCounter = iNumber - 1;
                    Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                    // m_objPointsInData.Add(objPoint, objPoint);
                    ptNewPoint = objPoint;// nearestPointObj1.GetNearestPoint();


                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);

                    }//end(if (m_objMarker != null))

                    arrmarkerCursor = new Marker[3];
                    arrChartTextCursor = new ChartText[3];
                    double TrendValue = 10;
                    double TrendFreq = 100;
                    double iConstSBTrendFreq = 0;
                    if (objTrend != null)
                    {
                        TrendValue = Convert.ToDouble(objTrend._Value.ToString());
                        TrendFreq = objTrend._Freq;
                    }
                    iConstSBTrendFreq = (TrendFreq * TrendValue) / 100;
                    double LowerLimit = ptNewPoint.GetX() - iConstSBTrendFreq;// (ptNewPoint.GetX() * Pctg / 100);
                    double UpperLimit = ptNewPoint.GetX() + iConstSBTrendFreq;// (ptNewPoint.GetX() * Pctg / 100);


                    //arrmarkerCursor = new Marker[3];
                    //arrChartTextCursor = new ChartText[3];

                    //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    //int Pctg = Convert.ToInt32(RatioExtractor[1]); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    //double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * 1 / Pctg);
                    //double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * 1 / Pctg);


                    //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    //int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    //double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                    //double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                    Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                    int MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                    if (MainIndex == -1)
                    {
                        if (LowerLimit >= objDataSet.XData[0])
                        {
                            LowerLimit = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), LowerLimit);
                            MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, objDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[1] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[1] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objDataSet.YData[MainIndex].ToString();

                    MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                    if (MainIndex == -1)
                    {
                        if (UpperLimit <= objDataSet.XData[objDataSet.XData.Length - 1])
                        {
                            UpperLimit = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), UpperLimit);
                            MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, objDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[2] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[2] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objDataSet.YData[MainIndex].ToString();
                    _DataGridView.Refresh();

                    //int iNumber = objDataSet.GetNumberDatapoints();
                    //if (m_iCounter > iNumber)
                    //    m_iCounter = iNumber;

                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    if (m_iCounter < 0)
                        m_iCounter = 0;
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = objPoint;// nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    arrmarkerCursor = new Marker[3];
                    arrChartTextCursor = new ChartText[3];
                    double TrendValue = 10;
                    double TrendFreq = 100;
                    double iConstSBTrendFreq = 0;
                    if (objTrend != null)
                    {
                        TrendValue = Convert.ToDouble(objTrend._Value.ToString());
                        TrendFreq = objTrend._Freq;
                    }
                    iConstSBTrendFreq = (TrendFreq * TrendValue) / 100;
                    double LowerLimit = ptNewPoint.GetX() - iConstSBTrendFreq;// (ptNewPoint.GetX() * Pctg / 100);
                    double UpperLimit = ptNewPoint.GetX() + iConstSBTrendFreq;// (ptNewPoint.GetX() * Pctg / 100);


                    //arrmarkerCursor = new Marker[3];
                    //arrChartTextCursor = new ChartText[3];

                    //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    //int Pctg = Convert.ToInt32(RatioExtractor[1]); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    //double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * 1 / Pctg);
                    //double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * 1 / Pctg);

                    //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    //int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    //double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                    //double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                    Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                    int MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                    if (MainIndex == -1)
                    {
                        if (LowerLimit >= objPreviousPointDataSet.XData[0])
                        {
                            LowerLimit = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), LowerLimit);
                            MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, objPreviousPointDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[1] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[1] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objPreviousPointDataSet.YData[MainIndex].ToString();

                    MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                    if (MainIndex == -1)
                    {
                        if (UpperLimit <= objPreviousPointDataSet.XData[objPreviousPointDataSet.XData.Length - 1])
                        {
                            UpperLimit = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), UpperLimit);
                            MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, objPreviousPointDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[2] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[2] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objPreviousPointDataSet.YData[MainIndex].ToString();
                    _DataGridView.Refresh();

                }//end(else if (e.KeyCode == Keys.NumPad4))                
                chartVu.UpdateDraw();
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownSideBandRatio(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                _DataGridView.Rows.Add(1);
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

                    m_iCounter++;
                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);

                    objDataSet = objClickedLine.DisplayDataset;
                    int iNumber = objDataSet.GetNumberDatapoints();
                    if (m_iCounter >= iNumber)
                        m_iCounter = iNumber - 1;
                    Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                    // m_objPointsInData.Add(objPoint, objPoint);
                    ptNewPoint = objPoint;// nearestPointObj1.GetNearestPoint();


                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);

                    }//end(if (m_objMarker != null))




                    arrmarkerCursor = new Marker[3];
                    arrChartTextCursor = new ChartText[3];

                    string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    int Pctg = Convert.ToInt32(RatioExtractor[1]); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * 1 / Pctg);
                    double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * 1 / Pctg);


                    //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    //int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    //double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                    //double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                    Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                    int MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                    if (MainIndex == -1)
                    {
                        if (LowerLimit >= objDataSet.XData[0])
                        {
                            LowerLimit = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), LowerLimit);
                            MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, objDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[1] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[1] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objDataSet.YData[MainIndex].ToString();

                    MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                    if (MainIndex == -1)
                    {
                        if (UpperLimit <= objDataSet.XData[objDataSet.XData.Length - 1])
                        {
                            UpperLimit = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), UpperLimit);
                            MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, objDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[2] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[2] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objDataSet.YData[MainIndex].ToString();
                    _DataGridView.Refresh();

                    //int iNumber = objDataSet.GetNumberDatapoints();
                    //if (m_iCounter > iNumber)
                    //    m_iCounter = iNumber;

                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    if (m_iCounter < 0)
                        m_iCounter = 0;
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = objPoint;// nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }



                    arrmarkerCursor = new Marker[3];
                    arrChartTextCursor = new ChartText[3];

                    string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    int Pctg = Convert.ToInt32(RatioExtractor[1]); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * 1 / Pctg);
                    double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * 1 / Pctg);

                    //string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    //int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    //double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                    //double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                    Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                    int MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                    if (MainIndex == -1)
                    {
                        if (LowerLimit >= objPreviousPointDataSet.XData[0])
                        {
                            LowerLimit = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), LowerLimit);
                            MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, objPreviousPointDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[1] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[1] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objPreviousPointDataSet.YData[MainIndex].ToString();

                    MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                    if (MainIndex == -1)
                    {
                        if (UpperLimit <= objPreviousPointDataSet.XData[objPreviousPointDataSet.XData.Length - 1])
                        {
                            UpperLimit = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), UpperLimit);
                            MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, objPreviousPointDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[2] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[2] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objPreviousPointDataSet.YData[MainIndex].ToString();
                    _DataGridView.Refresh();

                }//end(else if (e.KeyCode == Keys.NumPad4))                
                chartVu.UpdateDraw();
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownSideBandValue(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                _DataGridView.Rows.Add(1);
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

                    m_iCounter++;
                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);

                    objDataSet = objClickedLine.DisplayDataset;
                    int iNumber = objDataSet.GetNumberDatapoints();
                    if (m_iCounter >= iNumber)
                        m_iCounter = iNumber - 1;
                    Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                    // m_objPointsInData.Add(objPoint, objPoint);
                    ptNewPoint = objPoint;// nearestPointObj1.GetNearestPoint();


                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                        if (m_objDataCursor != null)
                            chartVu.DeleteChartObject(m_objDataCursor);

                    }//end(if (m_objMarker != null))




                    arrmarkerCursor = new Marker[3];
                    arrChartTextCursor = new ChartText[3];
                    string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                    double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                    Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                    int MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                    if (MainIndex == -1)
                    {
                        if (LowerLimit >= objDataSet.XData[0])
                        {
                            LowerLimit = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), LowerLimit);
                            MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, objDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[1] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[1] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objDataSet.YData[MainIndex].ToString();

                    MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                    if (MainIndex == -1)
                    {
                        if (UpperLimit <= objDataSet.XData[objDataSet.XData.Length - 1])
                        {
                            UpperLimit = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), UpperLimit);
                            MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, objDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[2] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objDataSet.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[2] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objDataSet.YData[MainIndex].ToString();
                    _DataGridView.Refresh();

                    //int iNumber = objDataSet.GetNumberDatapoints();
                    //if (m_iCounter > iNumber)
                    //    m_iCounter = iNumber;

                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    if (m_iCounter < 0)
                        m_iCounter = 0;
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;

                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = objPoint;// nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }



                    arrmarkerCursor = new Marker[3];
                    arrChartTextCursor = new ChartText[3];
                    string[] RatioExtractor = MainForm.TrendRatio.ToString().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    int Pctg = Convert.ToInt32(MainForm.SBValue.ToString()); //Convert.ToInt32(tbSideBandPercentage.Text.ToString());
                    double LowerLimit = ptNewPoint.GetX() - (ptNewPoint.GetX() * Pctg / 100);
                    double UpperLimit = ptNewPoint.GetX() + (ptNewPoint.GetX() * Pctg / 100);

                    //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY, 8, 1);
                    Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[0] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, ptNewPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[0] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetX().ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = ptNewPoint.GetY().ToString();

                    int MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                    if (MainIndex == -1)
                    {
                        if (LowerLimit >= objPreviousPointDataSet.XData[0])
                        {
                            LowerLimit = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), LowerLimit);
                            MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == LowerLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, LowerLimit, objPreviousPointDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[1] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, LowerLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData[MainIndex]), 5)) + YLabel, LowerLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[1] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = LowerLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objPreviousPointDataSet.YData[MainIndex].ToString();

                    MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                    if (MainIndex == -1)
                    {
                        if (UpperLimit <= objPreviousPointDataSet.XData[objPreviousPointDataSet.XData.Length - 1])
                        {
                            UpperLimit = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), UpperLimit);
                            MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == UpperLimit; });
                        }
                    }

                    m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, UpperLimit, objPreviousPointDataSet.YData[MainIndex], 8, 1);
                    m_objMarker1.SetColor(_MainCursorColor);
                    arrmarkerCursor[2] = m_objMarker1;
                    chartVu.AddChartObject(m_objMarker1);

                    CurrentLabel = new ChartText(pTransform1, theFont, UpperLimit.ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPreviousPointDataSet.YData[MainIndex]), 5)) + YLabel, UpperLimit, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(_MainCursorColor);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartTextCursor[2] = CurrentLabel;

                    _DataGridView.Rows.Add(1);
                    _DataGridView[0, _DataGridView.Rows.Count - 2].Value = UpperLimit.ToString();
                    _DataGridView[1, _DataGridView.Rows.Count - 2].Value = objPreviousPointDataSet.YData[MainIndex].ToString();
                    _DataGridView.Refresh();

                }//end(else if (e.KeyCode == Keys.NumPad4))                
                chartVu.UpdateDraw();
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        private void CheckKeyDownHarmonic(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
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
                arrChartTextCursor = new ChartText[0];
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

                        double lastx = objDataSet.XData[objDataSet.XData.Length - 1];
                        if (m_iCounter < (int)((iNumber - 1) * .02))
                        {
                            m_iCounter = (int)((iNumber - 1) * .02);
                        }
                        Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
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



                        double harmonicX = objPoint.X;

                        arrmarkerCursor = new Marker[0];
                        if (harmonicX < (double)(lastx * .02))
                        {
                            harmonicX = (double)(lastx * .02);
                        }
                        double constantHarmonicX = harmonicX;
                        while (harmonicX <= lastx)
                        {
                            int MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == harmonicX; });
                            if (MainIndex == -1)
                            {
                                if (harmonicX <= objDataSet.XData[objDataSet.XData.Length - 1])
                                {
                                    harmonicX = _MainForm.FindNearest(objDataSet.XData.GetDataBuffer(), harmonicX);
                                    MainIndex = Array.FindIndex(objDataSet.XData.GetDataBuffer(), delegate(double item) { return item == harmonicX; });
                                }
                            }

                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, harmonicX, objDataSet.YData[MainIndex], 8, 1);

                            //Array.Resize(ref arrmarkerCursor, arrmarkerCursor.Length + 1);
                            _ResizeArray.IncreaseArrayLinePlotMarker2D(ref arrmarkerCursor, 1);
                            arrmarkerCursor[arrmarkerCursor.Length - 1] = m_objMarker1;

                            m_objMarker1.FillColor = _MainCursorColor;
                            m_objMarker1.SetColor(_MainCursorColor);

                            chartVu.AddChartObject(m_objMarker1);

                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(objDataSet.YData[MainIndex].ToString()) + YLabel, harmonicX, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                            CurrentLabel.SetColor(_MainCursorColor);
                            chartVu.AddChartObject(CurrentLabel);

                            //Array.Resize(ref arrChartTextCursor, arrChartTextCursor.Length + 1);
                            _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartTextCursor, 1);
                            arrChartTextCursor[arrChartTextCursor.Length - 1] = CurrentLabel;


                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, _DataGridView.Rows.Count - 1].Value = harmonicX.ToString();
                            _DataGridView[1, _DataGridView.Rows.Count - 1].Value = objDataSet.YData[MainIndex].ToString();
                            _DataGridView.Refresh();
                            //chartVu.UpdateDraw();
                            harmonicX += constantHarmonicX;
                        }
                        chartVu.UpdateDraw();
                        //int iNumber = objDataSet.GetNumberDatapoints();
                        //if (m_iCounter > iNumber)
                        //    m_iCounter = iNumber;
                    }
                }
                else if (sType == "Left")
                {
                    m_iCounter--;
                    
                    objPreviousPointDataSet = objClickedLine.DisplayDataset;
                    if (m_iCounter < objPreviousPointDataSet.NumberDatapoints*0.02)
                        m_iCounter =(int)( (double)objPreviousPointDataSet.NumberDatapoints * 0.02);
                    double lastx = objPreviousPointDataSet.XData[objPreviousPointDataSet.XData.Length - 1];
                    if (m_objDataCursor != null)
                        chartVu.DeleteChartObject(m_objDataCursor);
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }


                    double harmonicX = objPoint.X;

                    arrmarkerCursor = new Marker[0];
                    if (harmonicX < (double)(lastx * .02))
                    {
                        harmonicX = (double)(lastx * .02);
                       
                    }
                    double constantHarmonicX = harmonicX;
                    while (harmonicX <= lastx)
                    {
                        int MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == harmonicX; });
                        if (MainIndex == -1)
                        {
                            if (harmonicX <= objPreviousPointDataSet.XData[objPreviousPointDataSet.XData.Length - 1])
                            {
                                harmonicX = _MainForm.FindNearest(objPreviousPointDataSet.XData.GetDataBuffer(), harmonicX);
                                MainIndex = Array.FindIndex(objPreviousPointDataSet.XData.GetDataBuffer(), delegate(double item) { return item == harmonicX; });
                            }
                        }

                        Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, harmonicX, objPreviousPointDataSet.YData[MainIndex], 8, 1);

                        //Array.Resize(ref arrmarkerCursor, arrmarkerCursor.Length + 1);
                        _ResizeArray.IncreaseArrayLinePlotMarker2D(ref arrmarkerCursor, 1);
                        arrmarkerCursor[arrmarkerCursor.Length - 1] = m_objMarker1;

                        m_objMarker1.FillColor = _MainCursorColor;
                        m_objMarker1.SetColor(_MainCursorColor);

                        chartVu.AddChartObject(m_objMarker1);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, harmonicX.ToString() + XLabel + " / " + Convert.ToString(objPreviousPointDataSet.YData[MainIndex].ToString()) + YLabel, harmonicX, pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(CurrentLabel);

                        //Array.Resize(ref arrChartTextCursor, arrChartTextCursor.Length + 1);
                        _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartTextCursor, 1);
                        arrChartTextCursor[arrChartTextCursor.Length - 1] = CurrentLabel;

                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, _DataGridView.Rows.Count - 1].Value = harmonicX.ToString();
                        _DataGridView[1, _DataGridView.Rows.Count - 1].Value = objPreviousPointDataSet.YData[MainIndex].ToString();
                        _DataGridView.Refresh();
                        //chartVu.UpdateDraw();
                        harmonicX += constantHarmonicX;
                    }
                    chartVu.UpdateDraw();







                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objMarker.SetColor(_MainCursorColor); //m_objMarker.SetColor(Color.Black);

                    chartVu.AddChartObject(m_objMarker);

                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = objPoint.X.ToString();
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))

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
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
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
                            m_iCounter = iNumber-1;
                        Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
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

                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                        m_objMarker.SetColor(_MainCursorColor); //m_objMarker.SetColor(Color.Black);
                        chartVu.AddChartObject(m_objMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objMarker.SetColor(_MainCursorColor); //m_objMarker.SetColor(Color.Black);

                    chartVu.AddChartObject(m_objMarker);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleLinePlot objClickedLine = null;
            SimpleDataset objDataSet = null;
            SimpleDataset objPreviousPointDataSet = null;

            try
            {
                _DataGridView.Rows.Clear();
                ArrayList arrObjects = chartVu.GetChartObjectsArrayList();

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
                        Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
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

                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                        m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_HLINE, objPoint.X, objPoint.Y, 8, 1);
                        m_objMarker.SetColor(_MainCursorColor);//m_objMarker.SetColor(Color.Black);
                        m_objNewMarker.SetColor(Color.Black);
                        chartVu.AddChartObject(m_objMarker);
                        chartVu.AddChartObject(m_objNewMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_HLINE, objPoint.X, objPoint.Y, 8, 1);
                    m_objMarker.SetColor(_MainCursorColor); //m_objMarker.SetColor(Color.Black);
                    m_objNewMarker.SetColor(Color.Black);

                    chartVu.AddChartObject(m_objMarker);
                    chartVu.AddChartObject(m_objNewMarker);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
        private void CheckKeyDownSingleWithSquare(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point2D ptNewPoint = null;
            Point2D ptLocation = null;
            Point2D ptPreviousPoint = null;
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
                        Point2D objPoint = objDataSet.GetDataPoint(m_iCounter);
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

                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                        m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, 8, 1);
                        m_objMarker.SetColor(_MainCursorColor);//m_objMarker.SetColor(Color.Black);
                        m_objNewMarker.SetColor(Color.Black);
                        chartVu.AddChartObject(m_objMarker);
                        chartVu.AddChartObject(m_objNewMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
                    Point2D objPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter);
                    ptPreviousPoint = objPreviousPointDataSet.GetDataPoint(m_iCounter + 1);
                    nearestPointObj1 = new NearestPointData();
                    textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, 8, 1);
                    m_objMarker.SetColor(_MainCursorColor);//m_objMarker.SetColor(Color.Black);
                    m_objNewMarker.SetColor(Color.Black);

                    chartVu.AddChartObject(m_objMarker);
                    chartVu.AddChartObject(m_objNewMarker);

                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, objPoint.GetX().ToString() + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
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
        private void LineGraphControl_DragEnter(object sender, DragEventArgs e)
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
        private void LineGraphControl_DragDrop(object sender, DragEventArgs e)
        {
            try
            {

                ArrayList data = _MainForm.ReadDroppedCSVFile();
                if (data != null)
                {
                    string path = _MainForm.ReadDroppedCSVFilePath();
                    _MainForm.Set_iClick(DI_Analyser.Form1.Function.Add);
                    AddNode(_MainForm.MainTreelist.FocusedNode.GetDisplayText(0).ToString());
                    DGVTrendNodes.Rows[DGVTrendNodes.Rows.Count - 2].Cells[2].Value = path;
                    ArrayList FullXYData = new ArrayList();
                    //FullXYData = XYDATA;
                    ArrayList xdta = new ArrayList();
                    ArrayList ydta = new ArrayList();
                    //xdta.Add((double[])data[0]);
                    //ydta.Add((double[])data[1]);
                    //FullXYData.Add((double[])data[0]);
                    //FullXYData.Add((double[])data[1]);
                    for (int i = 0; i < data.Count / 2; i++)
                    {
                        xdta.Add((double[])data[2 * i]);
                        ydta.Add((double[])data[(2 * i) + 1]);

                        FullXYData.Add((double[])data[2 * i]);
                        FullXYData.Add((double[])data[(2 * i) + 1]);
                    }
                    // _TrendNodeForm._FullXYData = FullXYData;
                    // _TrendNodeForm._PartialXYData = FullXYData;
                    DrawLineGraph(xdta, ydta);
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
            }

        }

        double[] PeekX = new double[0];
        int[] PeekIndex = new int[0];
        private double[] FindAllPeaks(double[] Xdata, double[] Ydata)
        {
            PeekIndex = new int[0];
            PeekX = new double[0];
            double Fst = 0;
            double Scnd = 0;
            double Thrd = 0;
            try
            {
                for (int i = 2; i < Ydata.Length; i++)
                {
                    Fst = Ydata[i - 2];
                    Scnd = Ydata[i - 1];
                    Thrd = Ydata[i];

                    if (Fst < Scnd && Scnd > Thrd)
                    {


                        //Array.Resize(ref PeekIndex, PeekIndex.Length + 1);
                        _ResizeArray.IncreaseArrayInt(ref PeekIndex, 1);
                        PeekIndex[PeekIndex.Length - 1] = i - 1;
                        //Array.Resize(ref PeekX, PeekX.Length + 1);
                        _ResizeArray.IncreaseArrayDouble(ref PeekX, 1);
                        PeekX[PeekX.Length - 1] = Xdata[i - 1];
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return PeekX;
        }


        public ChartView DrawReportGraph(double[] testX, double[] testY, string[] ColorTag, string Xunit, string Yunit)
        {
            RemovePreviousObjects();
            
            chartVu = this;
            bool InvertX = false;
            bool InvertY = false;

            try
            {
                SimpleDataset[] arrTestDataset = new SimpleDataset[0];
                //for (int i = 0; i < XYData.Count / 2; i++)
                {
                    //double[] testX = (double[])XYData[2 * i];
                    //double[] testY = (double[])XYData[(2 * i) + 1];
                    //if (Convert.ToDouble(testX[0].ToString()) > Convert.ToDouble(testX[testX.Length - 1].ToString()))
                    //{
                    //    InvertX = true;
                    //}
                    ////if (Convert.ToDouble(testY[0].ToString()) > Convert.ToDouble(testY[testY.Length - 1].ToString()))
                    ////{
                    ////    InvertY = true;
                    ////}
                    SimpleDataset testdataset = new SimpleDataset("Test", testX, testY);
                    try
                    {
                        testdataset.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, testX.Length, "Test");
                    }
                    catch
                    {
                    }
                    //Array.Resize(ref arrTestDataset, arrTestDataset.Length + 1);
                    _ResizeArray.IncreaseArrayLinePlotDataset2D(ref arrTestDataset, 1);
                    arrTestDataset[arrTestDataset.Length - 1] = testdataset;
                }




                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(arrTestDataset, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_FAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);
                if (InvertX)
                {
                    pTransform1.InvertScaleX();
                }
                if (InvertY)
                {
                    pTransform1.InvertScaleY();
                }
                plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                background = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);


                xAxis2 = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                xAxis2.SetColor(_AxisColor);
                xAxis2.SetAxisIntercept(0);
                chartVu.AddChartObject(xAxis2);

                yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                yAxis2.SetColor(_AxisColor);
                yAxis2.SetAxisIntercept(0);
                chartVu.AddChartObject(yAxis2);

                xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                xgrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(xgrid2);

                ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                ygrid2.SetColor(_AxisColor);
                chartVu.AddChartObject(ygrid2);


                for (int i = 0; i < arrTestDataset.Length; i++)
                {
                    attrib2 = new ChartAttribute(Color.FromArgb(-Convert.ToInt32(ColorTag[i])), 1, DashStyle.Solid, Color.FromArgb(150, Color.FromArgb(-Convert.ToInt32(ColorTag[i]))));
                    attrib2.SetLineColor(Color.FromArgb(-Convert.ToInt32(ColorTag[i])));
                    attrib2.SetFillFlag(_AreaFill);


                    thePlot1 = new SimpleLinePlot(pTransform1, arrTestDataset[i], attrib2);
                    chartVu.AddChartObject(thePlot1);
                }

                xAxisLab2 = new NumericAxisLabels(xAxis2);
                xAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(xAxisLab2);

                yAxisLab2 = new NumericAxisLabels(yAxis2);
                yAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(yAxisLab2);

                yaxistitle2 = new AxisTitle(yAxis2, theFont, Yunit);
                yaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(yaxistitle2);

                xaxistitle2 = new AxisTitle(xAxis2, theFont, Xunit);
                xaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(xaxistitle2);

                chartVu.SetResizeMode(ChartObj.NO_RESIZE_OBJECTS);

                Font toolTipFont = new Font("Arial", 10, FontStyle.Regular);
                DataToolTip datatooltip = new DataToolTip(chartVu);
                NumericLabel xValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                NumericLabel yValueTemplate = new NumericLabel(ChartObj.DECIMALFORMAT, 3);
                datatooltip.GetToolTipSymbol().SetColor(_AxisColor);
                datatooltip.SetXValueTemplate(xValueTemplate);
                datatooltip.SetYValueTemplate(yValueTemplate);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);
                chartVu.UpdateDraw();
                // SelectNextPlot(Convert.ToInt32(ColorTag[0].ToString()));
            }
            catch (Exception ex)
            {
            }
            return chartVu;
        }
        public ChartView DrawBandRegion(string[] BandData,ChartView _cview,string Xunit)
        {
            bool bToReturn = false;
            chartVu = _cview;
            //CartesianCoordinates pTransform1=_cview.
            try
            {
                //if (IsBandAreaPlot)
                {
                    //dataGridView1.Rows.Clear();
                    double xx = 0;
                    double yy = 0;
                    double ww = 0;
                    double hh = 0;
                    double REDxx = 0;
                    double REDyy = 0;
                    double REDww = 0;
                    double REDhh = 0;
                    arrChartShape = new ChartShape[0];
                    arrChartShape1 = new ChartShape[0];
                    Color alphaColor = Color.FromArgb(100, Color.Yellow);
                    ChartAttribute attrib2 = new ChartAttribute(alphaColor, 1, DashStyle.Solid, alphaColor);
                    attrib2.SetFillFlag(true);
                    Color alphaRedColor = Color.FromArgb(100, Color.Red);
                    ChartAttribute attribRed = new ChartAttribute(alphaRedColor, 1, DashStyle.Solid, alphaRedColor);
                    attribRed.SetFillFlag(true);
                    OriginalYMaxscale = pTransform1.ScaleMaxY;
                    for (int i = 0; i < BandData.Length; i++)
                    {


                        string[] splittedBandData = BandData[i].ToString().Split(new string[] { "!", "@" }, StringSplitOptions.RemoveEmptyEntries);

                        double sp0 = Convert.ToDouble(splittedBandData[0].ToString());
                        double sp1 = Convert.ToDouble(splittedBandData[1].ToString());
                        double sp2 = Convert.ToDouble(splittedBandData[2].ToString());
                        if (Xunit == "CPM")
                        {
                            sp0 = sp0 * 60;
                        }
                        //if (i != 0)
                        {
                            xx += ww;
                        }
                        ww = sp0 - xx;
                        yy = sp2;
                        hh = sp1 - yy;
                        if (pTransform1.ScaleMaxY < (yy + hh))
                        {
                            pTransform1.SetScaleY(0, (yy + hh));
                            pTransform1.YScale.SetRangeFromStop(yy + hh);
                            chartVu.DeleteChartObject(yAxis2);
                            chartVu.DeleteChartObject(xgrid2);
                            chartVu.DeleteChartObject(ygrid2);
                            chartVu.DeleteChartObject(yAxisLab2);


                            yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                            yAxis2.SetColor(_AxisColor);
                            chartVu.AddChartObject(yAxis2);



                            xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                            xgrid2.SetColor(_AxisColor);
                            chartVu.AddChartObject(xgrid2);

                            ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                            ygrid2.SetColor(_AxisColor);
                            chartVu.AddChartObject(ygrid2);


                            yAxisLab2 = new NumericAxisLabels(yAxis2);
                            yAxisLab2.SetColor(_AxisColor);
                            chartVu.AddChartObject(yAxisLab2);

                        }
                        Rectangle2D linearRegionRect = new Rectangle2D(xx, yy, ww, hh);
                        GraphicsPath rectpath = new GraphicsPath();
                        rectpath.AddRectangle(linearRegionRect.GetRectangleF());
                        ChartShape linearRegionShape = new ChartShape(pTransform1, rectpath,
                            ChartObj.PHYS_POS, 0.0, 0.0, ChartObj.PHYS_POS, 0);
                        linearRegionShape.SetChartObjAttributes(attrib2);
                        chartVu.AddChartObject(linearRegionShape);
                        //Array.Resize(ref arrChartShape, arrChartShape.Length + 1);
                        _ResizeArray.IncreaseArrayLinePlotChartShape2D(ref arrChartShape, 1);
                        arrChartShape[arrChartShape.Length - 1] = linearRegionShape;

                        REDxx = xx;
                        REDyy = yy + hh;
                        REDww = ww;
                        REDhh = pTransform1.ScaleMaxY - REDyy;
                        if (REDhh > 0)
                        {
                            linearRegionRect = new Rectangle2D(REDxx, REDyy, REDww, REDhh);
                            rectpath = new GraphicsPath();
                            rectpath.AddRectangle(linearRegionRect.GetRectangleF());
                            linearRegionShape = new ChartShape(pTransform1, rectpath,
                                ChartObj.PHYS_POS, 0.0, 0.0, ChartObj.PHYS_POS, 0);
                            linearRegionShape.SetChartObjAttributes(attribRed);
                            chartVu.AddChartObject(linearRegionShape);
                            //Array.Resize(ref arrChartShape1, arrChartShape1.Length + 1);
                            _ResizeArray.IncreaseArrayLinePlotChartShape2D(ref arrChartShape1, 1);
                            arrChartShape1[arrChartShape1.Length - 1] = linearRegionShape;
                        }
                        chartVu.UpdateDraw();
                        bToReturn = true;
                    }
                }
                //else
                //{
                //    if (pTransform1.ScaleMaxY != OriginalYMaxscale)
                //    {
                //        pTransform1.SetScaleY(0, OriginalYMaxscale);
                //        pTransform1.YScale.SetRangeFromStop(OriginalYMaxscale);

                //        chartVu.DeleteChartObject(yAxis2);
                //        chartVu.DeleteChartObject(yAxisLab2);

                //        yAxis2 = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                //        yAxis2.SetColor(_AxisColor);
                //        chartVu.AddChartObject(yAxis2);



                //        xgrid2 = new Grid(xAxis2, yAxis2, ChartObj.X_AXIS, ChartObj.GRID_MAJOR);
                //        xgrid2.SetColor(_AxisColor);
                //        chartVu.AddChartObject(xgrid2);

                //        ygrid2 = new Grid(xAxis2, yAxis2, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
                //        ygrid2.SetColor(_AxisColor);
                //        chartVu.AddChartObject(ygrid2);


                //        yAxisLab2 = new NumericAxisLabels(yAxis2);
                //        yAxisLab2.SetColor(_AxisColor);

                //        chartVu.AddChartObject(yAxisLab2);
                //    }
                //    if (arrChartShape != null)
                //    {
                //        for (int i1 = 0; i1 < arrChartShape.Length; i1++)
                //        {
                //            chartVu.DeleteChartObject(arrChartShape[i1]);
                //        }
                //    }
                //    if (arrChartShape1 != null)
                //    {
                //        for (int i1 = 0; i1 < arrChartShape1.Length; i1++)
                //        {
                //            chartVu.DeleteChartObject(arrChartShape1[i1]);
                //        }
                //    }
                //    chartVu.UpdateDraw();
                //}

            }
            catch (Exception ex)
            {

            }
            return chartVu;
        }
        public ChartView DrawRPMmarkers(double FinalFreq, int CountForRpm, ChartView _CView,string Xunit, string Yunit)
        {
            bool bToReturn = false;
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            NearestPointData objSecondNearestPoint = null;
            SimpleDataset Dataset1 = null;
            int PrvsMainIndex = 0;
            chartVu = _CView;
            try
            {
                //if (p)
                {

                    if (arrmarker != null)
                    {
                        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrmarker[i1]);
                            chartVu.DeleteChartObject(m_objDataCursor);
                        }
                    }
                    arrmarker = new Marker[CountForRpm];
                    if (arrChartText != null)
                    {
                        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartText[i1]);
                        }
                    }
                    arrChartText = new ChartText[0];
                    SimpleLinePlot[] AllPlots = GetAllPlots();
                    if (AllPlots.Length > 0)
                    {
                        Dataset1 = AllPlots[0].DisplayDataset;

                        //double FinalFreq = (double)((double)iRPM / (double)(iPulse * 60));
                        //int CountForRpm = _RPMCount;

                        // StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);


                        for (int i = 0; i < CountForRpm; i++)
                        {
                            double FreqToCalc = FinalFreq * (1 + i);
                            if (Xunit == "CPM")
                            {
                                FreqToCalc = FreqToCalc * 60;
                            }
                            if (FreqToCalc > (double)Dataset1.XData[Dataset1.XData.Length - 1])
                            {
                                break;
                            }
                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == FreqToCalc; });

                            if (MainIndex == -1)
                            {
                                if (FreqToCalc <= Dataset1.XData[Dataset1.XData.Length - 1])
                                {
                                    FreqToCalc = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), FreqToCalc);
                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == FreqToCalc; });
                                }
                            }
                            nearestPointObj1 = new NearestPointData();
                            objSecondNearestPoint = new NearestPointData();
                            textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                            //Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, Dataset1.XData[MainIndex], Dataset1.YData[MainIndex], 8, 1);
                            //arrmarker[i] = m_objMarker1;

                            //m_objMarker1.FillColor = _MainCursorColor;
                            //m_objMarker1.SetColor(_MainCursorColor);
                            //chartVu.AddChartObject(m_objMarker1);
                            if (PrvsMainIndex != MainIndex)
                            {
                                Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, Dataset1.XData[MainIndex], Dataset1.YData[MainIndex], 8, 1);
                                arrmarker[i] = m_objMarker1;
                                m_objMarker1.LineStyle = DashStyle.DashDot;
                                m_objMarker1.FillColor = Color.DarkKhaki;
                                m_objMarker1.SetColor(Color.DarkKhaki);
                                chartVu.AddChartObject(m_objMarker1);

                                Font theLabelFont = new Font("Arial", 8, FontStyle.Regular);
                                ChartText CurrentLabel = new ChartText(pTransform1, theLabelFont, Convert.ToString(i + 1) + "x RPM " + Dataset1.YData[MainIndex].ToString(), Dataset1.XData[MainIndex], pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_MIN, ChartObj.JUSTIFY_MIN, 270);
                                CurrentLabel.SetColor(Color.Black);
                                chartVu.AddChartObject(CurrentLabel);
                                //Array.Resize(ref arrChartText, arrChartText.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartText, 1);
                                arrChartText[arrChartText.Length - 1] = CurrentLabel;


                                //Names[Names.Length - 1] = Convert.ToString(i + 1) + "x RPM   " + Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5));
                                PrvsMainIndex = MainIndex;
                            }
                            else
                                break;
                            bToReturn = true;
                            chartVu.UpdateDraw();
                        }
                    }
                }
                //else
                //{
                //    if (arrmarker != null)
                //    {
                //        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                //        {
                //            chartVu.DeleteChartObject(arrmarker[i1]);
                //            chartVu.DeleteChartObject(m_objDataCursor);
                //        }
                //    }


                //    if (arrChartText != null)
                //    {
                //        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                //        {
                //            chartVu.DeleteChartObject(arrChartText[i1]);
                //        }
                //    }
                //    chartVu.UpdateDraw();
                //}
            }
            catch (Exception ex)
            {
            }
            return chartVu;
        }
        public ChartView DrawFaultFrequencies(string[] Frequencies, ChartView _CView,string XUnit,string YUnit)
        {
            bool bToReturn = false;
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            NearestPointData objSecondNearestPoint = null;
            SimpleDataset Dataset1 = null;
            chartVu = _CView;
            try
            {
                //if (p)
                {
                    //_Datagrid.Rows.Clear();
                    if (arrmarker != null)
                    {
                        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrmarker[i1]);
                            chartVu.DeleteChartObject(m_objDataCursor);
                        }
                    }
                    arrmarker = new Marker[0];
                    if (arrChartText != null)
                    {
                        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                        {
                            chartVu.DeleteChartObject(arrChartText[i1]);
                        }
                    }
                    arrChartText = new ChartText[0];
                    for (i = 0; i < Frequencies.Length; i++)
                    {
                        string[] ExtractFreqSingle = Frequencies[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        double Comparator = Convert.ToDouble(ExtractFreqSingle[1]);
                        if (Comparator.ToString() == "NaN")
                        {
                            break;
                        }
                        if (XUnit.Trim() == "CPM")
                        {
                            Comparator = Comparator * 60;
                        }
                        //if (m_objClickedPlot != null)
                        SimpleLinePlot[] AllPlots = GetAllPlots();
                        if (AllPlots.Length > 0)
                        {
                            Dataset1 = AllPlots[0].DisplayDataset;
                            //Dataset1 = m_objClickedPlot.DisplayDataset;
                            int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == Comparator; });
                            if (MainIndex == -1)
                            {
                                if (Comparator <= Dataset1.XData[Dataset1.XData.Length - 1])
                                {
                                    Comparator = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), Comparator);
                                    MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == Comparator; });
                                }
                            }

                            nearestPointObj1 = new NearestPointData();
                            objSecondNearestPoint = new NearestPointData();
                            textCoordsFont = new Font("Arial", 8, FontStyle.Regular);

                            Font theLabelFont = new Font("Arial", 5, FontStyle.Regular);
                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, Dataset1.XData[MainIndex], Dataset1.YData[MainIndex], 8, 1);
                            m_objMarker1.LineStyle = DashStyle.DashDotDot;

                            if (MainIndex != -1)
                            {

                                //Array.Resize(ref arrmarker, arrmarker.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlotMarker2D(ref arrmarker, 1);
                                arrmarker[arrmarker.Length - 1] = m_objMarker1;

                                m_objMarker1.FillColor = Color.DarkCyan;
                                m_objMarker1.SetColor(Color.DarkCyan);


                                chartVu.AddChartObject(m_objMarker1);
                                ChartText CurrentLabel = new ChartText(pTransform1, theLabelFont, ExtractFreqSingle[0].ToString() + " -> " + Convert.ToString(Math.Round(Dataset1.XData[MainIndex], 5)) + XUnit + " / " + Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5)) + YUnit, Dataset1.XData[MainIndex], pTransform1.YScale.ScaleStop, ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                CurrentLabel.SetColor(Color.Black);
                                chartVu.AddChartObject(CurrentLabel);

                                //Array.Resize(ref arrChartText, arrChartText.Length + 1);
                                _ResizeArray.IncreaseArrayLinePlotChartText2D(ref arrChartText, 1);
                                arrChartText[arrChartText.Length - 1] = CurrentLabel;


                                //_Datagrid.Rows.Add(1);
                                //_Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[0].Value = ExtractFreqSingle[0].ToString();
                                //_Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[1].Value = Convert.ToString(Math.Round(Dataset1.XData[MainIndex], 5));
                                //_Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[2].Value = Convert.ToString(Math.Round(Dataset1.YData[MainIndex], 5));
                                ////ExactBearingFF[i] = Convert.ToDouble(dataGridView3.Rows[dataGridView3.Rows.Count - 2].Cells[1].Value.ToString());
                                //try
                                //{
                                //    _MainForm._ExactBearingFF[i] = Convert.ToDouble(_Datagrid.Rows[_Datagrid.Rows.Count - 2].Cells[1].Value.ToString());
                                //}
                                //catch (Exception ex)
                                //{
                                //}
                            }
                            bToReturn = true;
                            chartVu.UpdateDraw();
                        }
                    }
                }
                //else
                //{
                //    if (arrmarker != null)
                //    {
                //        for (int i1 = 0; i1 < arrmarker.Length; i1++)
                //        {
                //            chartVu.DeleteChartObject(arrmarker[i1]);
                //            chartVu.DeleteChartObject(m_objDataCursor);
                //        }
                //    }
                //    if (arrChartText != null)
                //    {
                //        for (int i1 = 0; i1 < arrChartText.Length; i1++)
                //        {
                //            chartVu.DeleteChartObject(arrChartText[i1]);
                //        }
                //    }
                //    chartVu.UpdateDraw();
                //}
            }
            catch (Exception ex)
            {
            }
            return chartVu;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateXScaleAndAxes(hScrollBar1.Value);
        }

        private void UpdateXScaleAndAxes(int index)
        {
            try
            {

                double startindex = index;
                double a = 0;
                //if (startindex != 0)
                //{
                    a = Math.Abs((double)(m_objClickedPlot.DisplayDataset.GetXDataValue(0) - m_objClickedPlot.DisplayDataset.GetXDataValue(1)));
                    startindex = startindex * a;
                //}
                double difference = zoomObj.ChartObjScale.GetScaleStopX() - zoomObj.ChartObjScale.GetScaleStartX();
                
                pTransform1.SetScaleStartX((double)startindex);
                pTransform1.SetScaleStopX((double)(startindex + difference));
                xAxis2.CalcAutoAxis();
                yAxis2.CalcAutoAxis();
                xAxisLab2.CalcAutoAxisLabels();
                yAxisLab2.CalcAutoAxisLabels();
                this.UpdateDraw();
            }
            catch (Exception ex)
            {
            }
        }

        public void KillPoint(bool Kill)
        {
            bool bToReturn = false;
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            NearestPointData objSecondNearestPoint = null;
            SimpleDataset Dataset1 = null;
            int PrvsMainIndex = 0;
            //chartVu = _CView;
            try
            {
                if (arrmarker != null)
                {
                    for (int i1 = 0; i1 < arrmarker.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrmarker[i1]);
                        chartVu.DeleteChartObject(m_objDataCursor);
                    }
                }
                arrmarker = new Marker[0];
                if (arrChartText != null)
                {
                    for (int i1 = 0; i1 < arrChartText.Length; i1++)
                    {
                        chartVu.DeleteChartObject(arrChartText[i1]);
                    }
                }
                arrChartText = new ChartText[0];
                SimpleLinePlot[] AllPlots = GetAllPlots();
                if (AllPlots.Length > 0)
                {
                    Dataset1 = AllPlots[0].DisplayDataset;

                 
                    {

                        Point2D _2dXY = arrmarkerCursor[0].GetLocation();
                        double FreqToCalc = _2dXY.GetX();
                        if (_XLabel == "CPM")
                        {
                            FreqToCalc = FreqToCalc * 60;
                        }

                        int MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == FreqToCalc; });
                        if (MainIndex == -1)
                        {
                            if (FreqToCalc <= Dataset1.XData[Dataset1.XData.Length - 1])
                            {
                                FreqToCalc = _MainForm.FindNearest(Dataset1.XData.GetDataBuffer(), FreqToCalc);
                                MainIndex = Array.FindIndex(Dataset1.XData.GetDataBuffer(), delegate(double item) { return item == FreqToCalc; });
                            }
                        }

                        int PrevIndex = MainIndex - 1;
                        int NextIndex = MainIndex + 1;
                        double prevVal = Dataset1.YData[PrevIndex];
                        double nextval = Dataset1.YData[NextIndex];
                        double avgval = (prevVal + nextval) / 2;
                        Dataset1.YData[MainIndex] = avgval;



                        nearestPointObj1 = new NearestPointData();
                        objSecondNearestPoint = new NearestPointData();
                        textCoordsFont = new Font("Arial", 8, FontStyle.Regular);
                      
                        bToReturn = true;
                        chartVu.UpdateDraw();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
