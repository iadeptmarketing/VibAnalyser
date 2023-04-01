using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.iAM.chart3dnet;
using System.Collections;
using System.Drawing.Drawing2D;
using DI_Analyser;
using DevComponents.DotNetBar;

namespace Analyser.Graph_Controls
{
    
    public partial class BarChart : ChartView
    {
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
        SimpleBarPlot m_objSelectedPlot = null;
        SimpleBarPlot m_objNewPlot = null;
        SimpleBarPlot m_objSelectedPlotForCursor = null;
        SimpleBarPlot m_objClickedPlot = null;
        SimpleBarPlot m_objOldSelectedPlot = null;
        public delegate void MouseDownHandler(MouseEventArgs e);
        public event MouseDownHandler ThisMouseDown;
        public delegate void MouseMoveHandler(MouseEventArgs e);
        public event MouseMoveHandler ThisMouseMove;
        
        //int m_iCounter = 0;
        DataCursor m_objDataCursor = null;
        string SelectedCursor = null;
        SimpleDataset Dataset2;
        ChartView chartVu;
        Font theFont;
        SimpleBarPlot thePlot1;
        Background background;
        Background plotbackground;
        CartesianCoordinates pTransform1;
        int i;
        int j;
        ChartText[] arrChartText = null;
        ChartZoom zoomObj = null;
        Color MainCursor = Color.Black;
        ChartAttribute attrib2;
        string sColorTag = "7667712";
        bool bAreaFill = false;
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
        public double _BarWidth
        {
            get
            {
                return barwidth;
            }
            set
            {
                barwidth = value;
            }
        }
        DataGridView objDataGridView = null;
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
        Form1 MainForm = null;
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
        double barbase = 0;
        double barwidth = 0.8;
        int justify = ChartObj.JUSTIFY_CENTER;
        public BarChart()
        {
            InitializeComponent();
            this.ThisMouseMove += new MouseMoveHandler(BarChart_ThisMouseMove);
            this.ThisMouseDown += new MouseDownHandler(BarChart_ThisMouseDown);
            m_objPointsInData = new Dictionary<Point3D, Point3D>();
        }

        


        string[] sarrx = null;
        public void DrawBarGraph(double[] Xdata, double[] Ydata, bool Areafill)
        {
            DrawBarGraph(Xdata, Ydata, _ColorTag, Areafill);
            //DrawHistogram(Xdata, Ydata, _ColorTag, Areafill);
        }
        public void DrawBarGraph(double[] Xdata, double[] Ydata, string ColorTag, bool Areafill)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            sarrx = new string[Ydata.Length];
            string[] sarrxlab = new string[Ydata.Length];
            double[] y3 = new double[Ydata.Length];
            double[] x1 = new double[Ydata.Length];
            try
            {
                for (i = 0; i < Ydata.Length; i++)
                {
                    x1[i] = i;
                    //if (i == 0)
                    {
                        sarrxlab[i] = Xdata[i].ToString()+_XLabel+", " + Ydata[i].ToString();
                        sarrx[i] = Xdata[i].ToString();
                        y3[i] = Convert.ToDouble(Xdata[i].ToString());
                    }
                    //else
                    //{
                    //    sarrx[i] = Convert.ToString((double)(y3[i - 1]) * 2);
                    //    sarrxlab[i] = Convert.ToString((double)(y3[i - 1]) * 2) + ", " + Ydata[i].ToString();
                    //    y3[i] = (double)(y3[i - 1]) * 2;
                    //}
                }

                Dataset2 = new SimpleDataset("P/L", x1, Ydata);
                Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, Ydata.Length, "compressed");





                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(Dataset2, ChartObj.AUTOAXES_NEAR, ChartObj.AUTOAXES_NEAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);//(0.1, .1, .9, 0.9);

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
                pTransform1.AbsRotateCoordinateSystem(new Point3D(10, 20, 0));
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

                
                thePlot1 = new SimpleBarPlot(pTransform1, Dataset2,barwidth,barbase, attrib2,justify);
                chartVu.AddChartObject(thePlot1);
                StringAxisLabels sxAxisLab2 = new StringAxisLabels(xAxis2);
                sxAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(sxAxisLab2);

                NumericAxisLabels syAxisLab2 = new NumericAxisLabels(yAxis2);
                syAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(syAxisLab2);

                yaxistitle2 = new AxisTitle(yAxis2, theFont, _YLabel);
                yaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(yaxistitle2);

                xaxistitle2 = new AxisTitle(xAxis2, theFont, _XLabel);
                xaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(xaxistitle2);

                chartVu.SetResizeMode(ChartObj.AUTO_RESIZE_OBJECTS);

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
                CustomFindObj findObj = new CustomFindObj(chartVu, sarrxlab);
                findObj.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_CUSTOM);
                findObj.SetEnable(true);
                chartVu.SetCurrentMouseListener(findObj);

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
        public void DrawBarGraph(string[] Xdata, double[] Ydata, string ColorTag, bool Areafill)
        {
            RemovePreviousObjects();
            m_objSelectedPlot = null;
            m_objNewPlot = null;
            m_objSelectedPlotForCursor = null;
            m_objClickedPlot = null;
            m_objOldSelectedPlot = null;
            chartVu = this;
            theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            sarrx = new string[Ydata.Length];
            string[] sarrxlab = new string[Ydata.Length];
            double[] y3 = new double[Ydata.Length];
            double[] x1 = new double[Ydata.Length];
            try
            {
                for (i = 0; i < Ydata.Length; i++)
                {
                    x1[i] = i;
                    //if (i == 0)
                    {
                        sarrxlab[i] = Xdata[i].ToString() + ", " + Ydata[i].ToString();
                        sarrx[i] = Xdata[i].ToString();
                        y3[i] = Convert.ToDouble(i.ToString());
                    }
                    //else
                    //{
                    //    sarrx[i] = Convert.ToString((double)(y3[i - 1]) * 2);
                    //    sarrxlab[i] = Convert.ToString((double)(y3[i - 1]) * 2) + ", " + Ydata[i].ToString();
                    //    y3[i] = (double)(y3[i - 1]) * 2;
                    //}
                }

                Dataset2 = new SimpleDataset("P/L", x1, Ydata);
                Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, Ydata.Length, "compressed");





                pTransform1 = new CartesianCoordinates();
                pTransform1.AutoScale(Dataset2, ChartObj.AUTOAXES_NEAR, ChartObj.AUTOAXES_NEAR);
                pTransform1.SetGraphBorderDiagonal(0.15, .15, .85, 0.85);//(0.1, .1, .9, 0.9);

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
                pTransform1.AbsRotateCoordinateSystem(new Point3D(10, 20, 0));
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


                thePlot1 = new SimpleBarPlot(pTransform1, Dataset2, barwidth, barbase, attrib2, justify);
                chartVu.AddChartObject(thePlot1);
                StringAxisLabels sxAxisLab2 = new StringAxisLabels(xAxis2);
                sxAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(sxAxisLab2);

                NumericAxisLabels syAxisLab2 = new NumericAxisLabels(yAxis2);
                syAxisLab2.SetColor(_AxisColor);
                chartVu.AddChartObject(syAxisLab2);

                yaxistitle2 = new AxisTitle(yAxis2, theFont, _YLabel);
                yaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(yaxistitle2);

                xaxistitle2 = new AxisTitle(xAxis2, theFont, _XLabel);
                xaxistitle2.SetColor(_AxisColor);
                chartVu.AddChartObject(xaxistitle2);

                chartVu.SetResizeMode(ChartObj.AUTO_RESIZE_OBJECTS);

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
                CustomFindObj findObj = new CustomFindObj(chartVu, sarrxlab);
                findObj.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_CUSTOM);
                findObj.SetEnable(true);
                chartVu.SetCurrentMouseListener(findObj);

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
        public void DrawBarGraph(double[] Xdata, double[] Ydata, string ColorCode)
        {
            DrawBarGraph(Xdata, Ydata, ColorCode, _AreaFill);
        }
        public void DrawBarGraph(double[] Xdata, double[] YData)
        {
            try
            {
                DrawBarGraph(Xdata, YData, _AreaFill);
            }
            catch (Exception ex)
            {
            }
        }
        public void DrawBarGraph(ArrayList XData, ArrayList YData)
        {
            try
            {
                string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
                DrawBarGraph(XData, YData, ColorCode);
            }
            catch (Exception ex)
            {
            }

        }


        public void DrawBarGraph(ArrayList XData, ArrayList YData, string[] ColorTag)
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
                SimpleDataset[] arrSimpleDataset = new SimpleDataset[YData.Count];
                for (i = 0; i < YData.Count; i++)
                {
                    double[] yy = (double[])YData[i];
                    Dataset2 = new SimpleDataset("P/L", (double[])XData[i], (double[])YData[i]);
                    //Dataset2.ImplicitDepthValue = 0.01;
                    Dataset2.ImplicitZValue = (double)((double)1 / (double)YData.Count) * i;//0.01*i;
                    Dataset2.CompressSimpleDataset(ChartObj.DATACOMPRESS_MAX, ChartObj.DATACOMPRESS_MAX, 1, 0, yy.Length, "compressed");
                    arrSimpleDataset[i] = Dataset2;
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


                for (i = 0; i < YData.Count; i++)
                {
                    attrib2 = new ChartAttribute(Color.FromArgb(-Convert.ToInt32(ColorTag[i])), 1, DashStyle.Solid, Color.FromArgb(150, Color.FromArgb(-Convert.ToInt32(ColorTag[i]))));
                    attrib2.SetLineColor(Color.FromArgb(-Convert.ToInt32(ColorTag[i])));
                    attrib2.SetFillFlag(_AreaFill);


                    thePlot1 = new SimpleBarPlot(pTransform1, arrSimpleDataset[i], barwidth, barbase, attrib2, justify);//new SimpleBarPlot(pTransform1, arrSimpleDataset[i], attrib2,justify);
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

                chartVu.SetResizeMode(ChartObj.AUTO_RESIZE_OBJECTS);

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
            }
            catch (Exception ex)
            {
            }

        }
        RotateButtonUserControl rotatebutton;


        public void DrawHistogram(double[] XData, double[] YData,string ColorTag, bool AreaFill)
        {
            
            chartVu = this;
            RemovePreviousObjects();
            
            theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            // Define data
            int nnumpnts = 6;
            int numgroups = 2;
            double[] x1 = XData;
            double[,] y1 = new double[2, XData.Length-1];
            ////            height           width
            //x1[0] = 0; y1[0, 0] = .12; y1[1, 0] = 13;
            //x1[1] = 13; y1[0, 1] = .97; y1[1, 1] = 7;
            //x1[2] = 20; y1[0, 2] = .80; y1[1, 2] = 10;
            //x1[3] = 30; y1[0, 3] = .44; y1[1, 3] = 10;
            //x1[4] = 40; y1[0, 4] = .28; y1[1, 4] = 20;
            //x1[5] = 60; y1[0, 5] = .4; y1[1, 5] = 20;
            
                for (j = 0; j < XData.Length-1; j++)
                {
                    y1[0, j] = YData[j];
                    //if (j == XData.Length-1)
                    //{
                    //    y1[1, j] = 0;
                    //}
                    //else
                    {
                        y1[1, j] = XData[j+1] - XData[j];
                    }
                }

                double maxy = YData.Max();
                maxy *= 1.25;
            // Define dataset
            GroupDataset Dataset1 = new GroupDataset("Actual Sales", x1, y1);
            Dataset1.ImplicitDepthValue = 1.0;
            Dataset1.ImplicitMinZValue = 0.0;

            // Define coordinate system
            CartesianCoordinates pTransform1 = new CartesianCoordinates();
            pTransform1.SetScaleStartY(0);
            pTransform1.SetScaleStartX(0);
            pTransform1.SetScaleStopX(XData[XData.Length-1]);
            pTransform1.SetScaleStopY(maxy);

            // position chart in graph area
            pTransform1.SetGraphBorderDiagonal(0.15, .15, .9, 0.9);
            // Set chart rotation
            Point3D protate = new Point3D(10, 20, 0);
            pTransform1.AbsRotateCoordinateSystem(protate);

            // define graph background
            Background graphbackground = new Background(pTransform1, ChartObj.GRAPH_BACKGROUND,
                 Color.Green, Color.LightGreen, ChartObj.Y_AXIS);
            chartVu.AddChartObject(graphbackground);

            //define plot area backgrouond			
            Background plotbackground = new Background(pTransform1, ChartObj.PLOT_BACKGROUND, Color.LightBlue);
            chartVu.AddChartObject(plotbackground);


            // Define x and y-axes
            LinearAxis xAxis = new LinearAxis(pTransform1, ChartObj.X_AXIS);
            xAxis.SetColor(Color.White);
            chartVu.AddChartObject(xAxis);

            LinearAxis yAxis = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
            yAxis.SetColor(Color.White);
            chartVu.AddChartObject(yAxis);

            // Define x and y-axis labels
            NumericAxisLabels xAxisLab = new NumericAxisLabels(xAxis);
            xAxisLab.SetColor(Color.White);
            chartVu.AddChartObject(xAxisLab);

            NumericAxisLabels yAxisLab = new NumericAxisLabels(yAxis);
            yAxisLab.SetColor(Color.White);
            yAxisLab.SetAxisLabelsFormat(ChartObj.DECIMALFORMAT);
            yAxisLab.SetAxisLabelsDecimalPos(0);
            chartVu.AddChartObject(yAxisLab);

            // define x and y-axis titles
            AxisTitle yaxistitle = new AxisTitle(yAxis, theFont, _YLabel);
            yaxistitle.SetColor(Color.White);
            chartVu.AddChartObject(yaxistitle);

            AxisTitle xaxistitle = new AxisTitle(xAxis, theFont, _XLabel);
            xaxistitle.SetColor(Color.White);
            chartVu.AddChartObject(xaxistitle);

            // define y-grid
            Grid ygrid = new Grid(xAxis, yAxis, ChartObj.Y_AXIS, ChartObj.GRID_MAJOR);
            ygrid.SetColor(Color.White);
            ygrid.SetLineWidth(1);
            chartVu.AddChartObject(ygrid);

            // define histogram plot
            ChartAttribute attrib1 = new ChartAttribute(Color.Black, 0, DashStyle.Solid, Color.Green);
            attrib1.SetFillFlag(true);
            HistogramPlot thePlot1 = new HistogramPlot(pTransform1, Dataset1, 0.0, attrib1);
            NumericLabel bardatavalue = thePlot1.GetPlotLabelTemplate();
            // define data point values for histogram plot
            bardatavalue.SetTextFont(theFont);
            bardatavalue.SetNumericFormat(ChartObj.DECIMALFORMAT);
            bardatavalue.SetColor(Color.Black);
            thePlot1.SetBarDatapointLabelPosition(ChartObj.INSIDE_BAR);
            thePlot1.SetPlotLabelTemplate(bardatavalue);
            thePlot1.SetShowDatapointValue(true);

             //color each histogram bar different
            thePlot1.SetSegmentAttributesMode(true);
            thePlot1.SetSegmentFillColor(0, Color.Red);
            thePlot1.SetSegmentFillColor(1, Color.Magenta);
            thePlot1.SetSegmentFillColor(2, Color.Blue);
            thePlot1.SetSegmentFillColor(3, Color.Green);
            thePlot1.SetSegmentFillColor(4, Color.Yellow);
            thePlot1.SetSegmentFillColor(5, Color.Pink);
            chartVu.AddChartObject(thePlot1);

            // define main title
            //Font theTitleFont = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            //ChartTitle mainTitle = new ChartTitle(theTitleFont, "Video Game Usage by Age Group");
            //mainTitle.SetTitleType(ChartObj.CHART_HEADER);
            //mainTitle.SetTitlePosition(ChartObj.CENTER_GRAPH);
            //mainTitle.SetColor(Color.White);
            //chartVu.AddChartObject(mainTitle);

            // define footer
            //Font theFooterFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            //ChartTitle footer = new ChartTitle(theFooterFont, "The height and width of the bars in a Histogram plot carries information.");
            //footer.SetTitleType(ChartObj.CHART_FOOTER);
            //footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
            //footer.SetTitleOffset(8);
            //footer.SetColor(Color.White);
            //chartVu.AddChartObject(footer);


            // define data tooltip
            DataToolTip datatooltip = new DataToolTip(chartVu);
            // This works because the y value template is by default a NumericLabel
            ((NumericLabel)datatooltip.GetYValueTemplate()).SetDecimalPos(3);
            // Don't highlight the data value because the data value is at the start of the histogram, not centered on histogram width
            datatooltip.GetToolTipSymbol().SetSymbolNumber(ChartObj.NOSYMBOL);
            datatooltip.SetEnable(true);
            chartVu.SetCurrentMouseListener(datatooltip);
            chartVu.Controls.Remove(rotatebutton);
            // add a rotate control button to upper left corner
            rotatebutton = new RotateButtonUserControl(pTransform1);
            rotatebutton.Size = new Size(32, 32);
            rotatebutton.Location = new Point(8, 8);
            chartVu.Controls.Add(rotatebutton);
            //RotateTransform3D rotatemouselistener = new RotateTransform3D(chartVu);
            //rotatemouselistener.SetEnable(true);
            //rotatemouselistener.SetObjectFilter("Axis");
            //chartVu.SetCurrentMouseListener(rotatemouselistener);

        }

        public void SetSelectedPlot(SimpleBarPlot plot)
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

        void BarChart_ThisMouseDown(MouseEventArgs e)
        {
            try
            {
                _MainForm._BarChartControl = this;
            }
            catch (Exception ex)
            {
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (ThisMouseDown != null)
            {
                base.OnMouseDown(e);
                ThisMouseDown(e);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //NearestPointData nearestPointObj1 = null;
            //Font textCoordsFont = null;
            //Point2D ptNewPoint = null;
            //Point2D ptLocation = null;
            //Point2D ptSecondPoint = null;
            //Point2D ptPointGot = null;
            //IDictionaryEnumerator objEnumerator = null;
            //NearestPointData objSecondNearestPoint = null;
            //bool bFirstPlot = false;
            //bool bSecondPlot = true;

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

        void BarChart_ThisMouseMove(MouseEventArgs e)
        {
            Point3D objClickedPoint = null;
            double dZValue = 0;
            Point3D objPoint3D = null;
            NearestPointData objNearestPoint = null;
            Point3D objLocationPoint = null;
            Point3D objGetPoint = null;
            String sDisplaytext = null;

            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptSecondPoint = null;
            Point3D ptPointGot = null;
            IDictionaryEnumerator objEnumerator = null;
            NearestPointData objSecondNearestPoint = null;
            bool bFirstPlot = false;
            bool bSecondPlot = true;
            SimpleDataset Dataset1 = null;

            try
            {

                Point2D objNewPoint = new Point2D();


                objNewPoint.SetLocation(e.X, e.Y);
                //if (m_objChartView == null)
                //    m_objChartView = m_obj2DGraphControl.CreateSideBandCursor
                ChartObj objNewObject = chartVu.FindObj(objNewPoint, "SimpleBarPlot");

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




                    if (m_objNewPlot != null)
                    {
                        m_objSelectedPlotForCursor = m_objNewPlot;


                        if (m_objClickedPlot != null)// && m_objClickedPlot.LineWidth == 2)
                        {
                            Dataset1 = m_objSelectedPlotForCursor.DisplayDataset;
                            //Point2D objPoint = Dataset1.GetDataPoint(m_iCounter);


                            nearestPointObj1 = new NearestPointData();
                            objSecondNearestPoint = new NearestPointData();
                            textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
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


                            if (m_objMarker != null)
                            {
                                chartVu.DeleteChartObject(m_objMarker);
                                //chartVu.DeleteChartObject(m_objNewMarker);
                                //m_objChartView.DeleteChartObject(objSecondLineMarker);
                                chartVu.DeleteChartObject(m_objDataCursor);

                            }//end(if (m_objMarker != null))
                            if (arrmarker != null)
                            {
                                for (int i = 0; i < arrmarker.Length; i++)
                                {
                                    chartVu.DeleteChartObject(arrmarker[i]);
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
                            arrChartText = new ChartText[0];
                            m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                            if (_DataGridView != null)
                            {
                                _DataGridView.Rows.Clear();
                                _DataGridView.Rows.Add(1);
                            }
                            //m_objMarker.FillColor = Color.Black;
                            //m_objMarker.SetColor(Color.Black);
                            if (SelectedCursor != null)
                            {
                                switch (SelectedCursor)
                                {
                                    case "Single":
                                        {
                                            arrmarker = new Marker[1];
                                            arrChartText = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarker[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);


                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(ptNewPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(Color.Black);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartText[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(ptNewPoint.GetX())]; //ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "Single With Square":
                                        {
                                            arrmarker = new Marker[2];
                                            arrChartText = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_BOX, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarker[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);
                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarker[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(ptNewPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(Color.Black);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartText[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(ptNewPoint.GetX())];
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                    case "Cross Hair":
                                        {
                                            arrmarker = new Marker[2];
                                            arrChartText = new ChartText[1];
                                            Marker m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_HLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarker[0] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);
                                            m_objMarker1 = new Marker(pTransform1, GraphObj.MARKER_VLINE, ptNewPoint.GetX(), ptNewPoint.GetY(), 8, 1);
                                            m_objMarker1.SetColor(_MainCursorColor);
                                            arrmarker[1] = m_objMarker1;
                                            chartVu.AddChartObject(m_objMarker1);

                                            ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(ptNewPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(ptNewPoint.GetY()), 5)) + YLabel, ptNewPoint.GetX(), Dataset1.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                                            CurrentLabel.SetColor(Color.Black);
                                            chartVu.AddChartObject(CurrentLabel);
                                            arrChartText[0] = CurrentLabel;

                                            _DataGridView.Rows.Add(1);
                                            _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(ptNewPoint.GetX())]; //ptNewPoint.GetX().ToString();
                                            _DataGridView[1, 0].Value = ptNewPoint.GetY().ToString();
                                            _DataGridView.Refresh();
                                            break;
                                        }
                                }
                                if (m_objMarker != null)
                                {
                                    chartVu.UpdateDraw();
                                }//end(if (objMarker != null))
                            }
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                    }
                }//end if (e.Button == MouseButtons.Left))
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
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
                    chartVu.UpdateDraw();
                }//end(if (m_objMarker != null))
                if (arrmarker != null)
                {
                    for (int i = 0; i < arrmarker.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarker[i]);
                        chartVu.UpdateDraw();
                    }
                    arrmarker = new Marker[0];
                }
                if (Cursor == "Select Cursor")
                {
                    SelectedCursor = null;
                    //m_objDataCursor = null;
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
                            
                        }//end(if (m_objMarker != null && m_objNewMarker != null))
                        if (arrChartText != null)
                        {
                            for (int i1 = 0; i1 < arrChartText.Length; i1++)
                            {
                                chartVu.DeleteChartObject(arrChartText[i1]);
                            }
                        }
                        arrChartText = new ChartText[0];
                        chartVu.UpdateDraw();
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
                                    break;
                                }
                            case Keys.Right:
                                {
                                    CheckKeyDown("Right", SelectedCursor);
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
                    //case "Sideband":
                    //    {
                    //        break;
                    //    }
                    //case "SidebandRatio":
                    //    {
                    //        break;
                    //    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        Dictionary<Point3D, Point3D> m_objPointsInData = null;
        int m_iCounter = 0;
        
        public void CheckKeyDownSingle(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleBarPlot objClickedLine = null;
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
                    if (iType == 3)
                    {
                        objClickedLine = (SimpleBarPlot)objLine;
                        break;
                    }
                }
                if (arrmarker != null)
                {
                    for (int i = 0; i < arrmarker.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarker[i]);
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
                arrChartText = new ChartText[1];
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
                        Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                        ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                        nearestPointObj1 = new NearestPointData();
                        textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                        m_objPointsInData.Add(objPoint, objPoint);
                        ptNewPoint = nearestPointObj1.GetNearestPoint();


                        if (m_objMarker != null)
                        {
                            chartVu.DeleteChartObject(m_objMarker);
                            chartVu.DeleteChartObject(m_objNewMarker);
                            if (m_objDataCursor != null)
                                chartVu.DeleteChartObject(m_objDataCursor);

                        }//end(if (m_objMarker != null))
                        
                        m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                        m_objMarker.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(m_objMarker);


                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(objPoint.GetX())] + XLabel + " / " +Convert.ToString( Math.Round(Convert.ToDouble(objPoint.GetY()),5)) + YLabel, objPoint.GetX(), objDataSet.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(Color.Black);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartText[0] = CurrentLabel;

                        if (m_objMarker != null)
                        {
                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(objPoint.X)]; //objPoint.X.ToString();
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
                    textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objMarker.SetColor(_MainCursorColor);

                    chartVu.AddChartObject(m_objMarker);


                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(objPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), objPreviousPointDataSet.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(Color.Black);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartText[0] = CurrentLabel;

                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(objPoint.X)]; //objPoint.X.ToString();
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarker = new Marker[1];
                arrmarker[0] = m_objMarker;
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        public void CheckKeyDownCrossHair(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleBarPlot objClickedLine = null;
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
                    if (iType == 3)
                    {
                        objClickedLine = (SimpleBarPlot)objLine;
                        break;
                    }
                }
                if (arrmarker != null)
                {
                    for (int i = 0; i < arrmarker.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarker[i]);
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
                arrChartText = new ChartText[1];

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
                        Point3D objPoint = objDataSet.GetDataPoint(m_iCounter);
                        ptPreviousPoint = objDataSet.GetDataPoint(m_iCounter - 1);
                        nearestPointObj1 = new NearestPointData();
                        textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                        m_objPointsInData.Add(objPoint, objPoint);
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
                        m_objMarker.SetColor(_MainCursorColor);
                        m_objNewMarker.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(m_objMarker);
                        chartVu.AddChartObject(m_objNewMarker);


                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(objPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), objDataSet.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(Color.Black);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartText[0] = CurrentLabel;


                        if (m_objMarker != null)
                        {
                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(objPoint.X)]; //objPoint.X.ToString();
                            _DataGridView[1, 0].Value = objPoint.Y.ToString();
                            _DataGridView.Refresh();
                            chartVu.UpdateDraw();

                        }//end(if (objMarker != null
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
                    textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_HLINE, objPoint.X, objPoint.Y, 8, 1);
                    m_objMarker.SetColor(_MainCursorColor);
                    m_objNewMarker.SetColor(_MainCursorColor);

                    chartVu.AddChartObject(m_objMarker);
                    chartVu.AddChartObject(m_objNewMarker);


                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(objPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), objPreviousPointDataSet.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(Color.Black);
                    chartVu.AddChartObject(CurrentLabel);
                    arrChartText[0] = CurrentLabel;


                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(objPoint.X)]; //objPoint.X.ToString();
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarker = new Marker[2];
                arrmarker[0] = m_objMarker;
                arrmarker[1] = m_objNewMarker;
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        public void CheckKeyDownSingleWithSquare(string sType)
        {
            NearestPointData nearestPointObj1 = null;
            Font textCoordsFont = null;
            Point3D ptNewPoint = null;
            Point3D ptLocation = null;
            Point3D ptPreviousPoint = null;
            GraphObj objLine = null;
            SimpleBarPlot objClickedLine = null;
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
                    if (iType == 3)
                    {
                        objClickedLine = (SimpleBarPlot)objLine;
                        break;
                    }
                }
                if (arrmarker != null)
                {
                    for (int i = 0; i < arrmarker.Length; i++)
                    {
                        chartVu.DeleteChartObject(arrmarker[i]);
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
                arrChartText = new ChartText[1];

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
                        textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                        m_objPointsInData.Add(objPoint, objPoint);
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
                        m_objMarker.SetColor(_MainCursorColor);
                        m_objNewMarker.SetColor(_MainCursorColor);
                        chartVu.AddChartObject(m_objMarker);
                        chartVu.AddChartObject(m_objNewMarker);

                        ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(objPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), objDataSet.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                        CurrentLabel.SetColor(Color.Black);
                        chartVu.AddChartObject(CurrentLabel);
                        arrChartText[0] = CurrentLabel;


                        if (m_objMarker != null)
                        {
                            _DataGridView.Rows.Add(1);
                            _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(objPoint.X)];
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
                    textCoordsFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

                    ptNewPoint = nearestPointObj1.GetNearestPoint();

                    if (m_objMarker != null)
                    {
                        chartVu.DeleteChartObject(m_objMarker);
                        chartVu.DeleteChartObject(m_objNewMarker);
                    }

                    m_objMarker = new Marker(pTransform1, GraphObj.MARKER_VLINE, objPoint.X, objPoint.Y, 5, 1);
                    m_objNewMarker = new Marker(pTransform1, GraphObj.MARKER_BOX, objPoint.X, objPoint.Y, 8, 1);
                    m_objMarker.SetColor(_MainCursorColor);
                    m_objNewMarker.SetColor(_MainCursorColor);

                    chartVu.AddChartObject(m_objMarker);
                    chartVu.AddChartObject(m_objNewMarker);


                    ChartText CurrentLabel = new ChartText(pTransform1, theFont, sarrx[Convert.ToInt32(objPoint.GetX())] + XLabel + " / " + Convert.ToString(Math.Round(Convert.ToDouble(objPoint.GetY()), 5)) + YLabel, objPoint.GetX(), objPreviousPointDataSet.YData.DataBuffer.Max(), ChartObj.PHYS_POS, ChartObj.JUSTIFY_CENTER, ChartObj.JUSTIFY_MIN, 270);
                    CurrentLabel.SetColor(Color.Black);
                    chartVu.AddChartObject(CurrentLabel);


                    arrChartText[0] = CurrentLabel;
                    if (m_objMarker != null)
                    {
                        _DataGridView.Rows.Add(1);
                        _DataGridView[0, 0].Value = sarrx[Convert.ToInt32(objPoint.X)];
                        _DataGridView[1, 0].Value = objPoint.Y.ToString();
                        _DataGridView.Refresh();
                        chartVu.UpdateDraw();

                    }//end(if (objMarker != null))                   
                }//end(else if (e.KeyCode == Keys.NumPad4))
                arrmarker = new Marker[2];
                arrmarker[0] = m_objMarker;
                arrmarker[1] = m_objNewMarker;
            }//end(try)
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
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
                            if (obj.Name != "SimpleBarPlot")
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
                    MessageBoxEx.Show("BAR Graph Copied on ClipBoard","Graph");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
    class CustomFindObj : CustomDataToolTip
    {
        String[] labelarray;
        public CustomFindObj(ChartView component, String[] labels)
            : base(component)
        {
            labelarray = labels;
        }
        override public void Draw(Graphics g2)
        {
            DefineCustomToolTipString(); // define the CustomToolTipString
            base.Draw(g2); // Display the CustomToolTipString
        }
        public void DefineCustomToolTipString()
        {
            String tooltipstring = "";

            ChartPlot selectedPlot = (ChartPlot)GetSelectedPlotObj();
            if (selectedPlot != null)
            {

                int selectedindex = GetNearestPoint().GetNearestPointIndex();
                if ((selectedindex < labelarray.Length) && GetNearestPoint().GetNearestPointValid())
                {
                    tooltipstring = labelarray[selectedindex];
                }
            }

            this.CustomToolTipString = tooltipstring;
        }
    }
}
