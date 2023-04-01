using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using com.iAM.chart2dnet;
using System.Collections;
using DevComponents.DotNetBar;
using System.Drawing.Drawing2D;

namespace Analyser.Graph_Controls
{

    public partial class PolarPlot : ChartView
    {
        public PolarPlot()
        {
            InitializeComponent();
        }
        //        AntennaCoordinates pAntennaTransform = null;
        //        public void DrawAnteenaPlot()
        //        {
        //            ChartView chartVu = this;
        //            SimpleDataset Dataset1;
        //            SimpleDataset Dataset2;

        //            Font theFont;
        //            int num = 61;

        //            double[] mag1 = new double[num];
        //            double[] mag2 = new double[num];
        //            double[] ang1 = new double[num];

        //            for (int i = 0; i < num - 1; i++)
        //            {
        //                ang1[i] = (double)(i * (int)360 / (num - 1));

        //                if ((i > num - 3) || (i < 3))
        //                    mag1[i] = -10 + 5 * ChartSupport.GetRandomDouble();
        //                else if ((i > (num - 1) / 2 - 3) && (i < (num - 1) / 2 + 3))
        //                    mag1[i] = -15 * ChartSupport.GetRandomDouble();
        //                else
        //                    mag1[i] = -40 + 15 * ChartSupport.GetRandomDouble();


        //                if ((i > num - 3) || (i < 3))
        //                    mag2[i] = 10 + 8 * ChartSupport.GetRandomDouble();
        //                else if ((i > num / 2 - 3) && (i < (num - 1) / 2 + 3))
        //                    mag2[i] = 5 * ChartSupport.GetRandomDouble();
        //                else
        //                    mag2[i] = -40 + 30 * ChartSupport.GetRandomDouble();
        //            }
        //            // close loop
        //            mag1[num - 1] = mag1[0];
        //            mag2[num - 1] = mag2[0];
        //            ang1[num - 1] = ang1[0];


        //            this.SmoothingMode = SmoothingMode.AntiAlias;
        //            theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        //            Dataset1 = new SimpleDataset("First", mag1, ang1);
        //            Dataset2 = new SimpleDataset("Second", mag2, ang1);

        //            SimpleDataset[] datasetarray = { Dataset1, Dataset2 };
        //            pAntennaTransform = new AntennaCoordinates();
        //            pAntennaTransform.AutoScale(datasetarray, ChartObj.AUTOAXES_FAR);

        //            pAntennaTransform.SetGraphBorderDiagonal(0.25, .15, .75, 0.85);

        //            Background background = new Background(pAntennaTransform, ChartObj.GRAPH_BACKGROUND,
        //                Color.White);
        //            chartVu.AddChartObject(background);

        //            AntennaAxes pAntennaAxis = pAntennaTransform.GetCompatibleAxes();
        //            pAntennaAxis.LineColor = Color.Black;

        //            double axestickspace = 1;
        //            int axesntickspermajor = 5;
        //            double angletickspace = 5;
        //            int anglentickspermajor = 6;
        //            double minorticlength = 5;
        //            double majorticlength = 10;
        //            int tickdir = ChartObj.AXIS_CENTER;

        //            pAntennaAxis.SetAntennaAxesTicks(axestickspace, axesntickspermajor,
        //                            angletickspace, anglentickspermajor,
        //                            minorticlength, majorticlength,
        //                            tickdir);

        //            chartVu.AddChartObject(pAntennaAxis);

        //            AntennaGrid pAntennaGrid = new AntennaGrid(pAntennaAxis, AntennaGrid.GRID_ALL);
        //            pAntennaGrid.ChartObjAttributes = new ChartAttribute(Color.LightBlue, 1, DashStyle.Solid);
        //            chartVu.AddChartObject(pAntennaGrid);

        //            AntennaAxesLabels pAntennaAxisLabels = (AntennaAxesLabels)pAntennaAxis.GetCompatibleAxesLabels();
        //            chartVu.AddChartObject(pAntennaAxisLabels);

        //            Color transparentRed = Color.FromArgb(180, 255, 0, 0);
        //            Color transparentBlue = Color.FromArgb(180, 0, 0, 255);


        //            ChartAttribute attrib1 = new ChartAttribute(transparentRed, 1, DashStyle.Solid);
        //            attrib1.SymbolSize = 7;
        //            ChartAttribute attrib2 = new ChartAttribute(Color.Blue, 1, DashStyle.Solid, Color.Blue);
        //            attrib2.SymbolSize = 7;

        //            ChartAttribute attrib3 = new ChartAttribute(Color.Yellow, 3, DashStyle.Solid, Color.Yellow);
        //            ChartAttribute attrib4 = new ChartAttribute(Color.MediumPurple, 2, DashStyle.Dot, Color.MediumPurple);
        //#if false
        //            AntennaLinePlot thePlot1 = new AntennaLinePlot(pAntennaTransform);
        //            thePlot1.InitAntennaLinePlot(Dataset1, attrib1);
        //            chartVu.AddChartObject(thePlot1);

        //            AntennaScatterPlot thePlot2 = new AntennaScatterPlot(pAntennaTransform);
        //            thePlot2.InitAntennaScatterPlot(Dataset2, ChartObj.SQUARE, attrib2);
        //            chartVu.AddChartObject(thePlot2);
        //#else
        //            AntennaLineMarkerPlot thePlot1 = new AntennaLineMarkerPlot(pAntennaTransform, Dataset1, attrib1);
        //            chartVu.AddChartObject(thePlot1);

        //            AntennaLineMarkerPlot thePlot2 = new AntennaLineMarkerPlot(pAntennaTransform, Dataset2, attrib2);
        //            chartVu.AddChartObject(thePlot2);
        //#endif
        //            AntennaAnnotation thePlot3 = new AntennaAnnotation(pAntennaTransform, ChartObj.ANTENNA_ANNOTATION_ANGULAR, 180, attrib3);
        //            chartVu.AddChartObject(thePlot3);


        //            AntennaAnnotation thePlot4 = new AntennaAnnotation(pAntennaTransform, ChartObj.ANTENNA_ANNOTATION_RADIUS, 12, attrib4);
        //            chartVu.AddChartObject(thePlot4);

        //            Font theTitleFont = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
        //            ChartTitle mainTitle = new ChartTitle(pAntennaTransform, theTitleFont, "Antenna Plot");
        //            mainTitle.SetTitleType(ChartObj.CHART_HEADER);
        //            mainTitle.SetTitlePosition(ChartObj.CENTER_GRAPH);
        //            chartVu.AddChartObject(mainTitle);

        //            ChartText textannotation270 = new ChartText(pAntennaTransform, theFont, "NULL", 10, 270, ChartObj.ANTENNA_POS, ChartObj.JUSTIFY_MIN, ChartObj.JUSTIFY_CENTER, 0);
        //            chartVu.AddChartObject(textannotation270);

        //            ChartText textannotation90 = new ChartText(pAntennaTransform, theFont, "NULL", 10, 90, ChartObj.ANTENNA_POS, ChartObj.JUSTIFY_MAX, ChartObj.JUSTIFY_CENTER, 0);
        //            chartVu.AddChartObject(textannotation90);

        //            double aspectratio = CalcAspectRatio();
        //            String aspectratiostring = "Aspect Ratio = " + ChartSupport.NumToString(aspectratio, ChartObj.DECIMALFORMAT, 2, "");
        //            subhead = new ChartTitle(pAntennaTransform, theTitleFont, aspectratiostring);
        //            subhead.SetTitleType(ChartObj.CHART_SUBHEAD);
        //            subhead.SetTitlePosition(ChartObj.CENTER_GRAPH);
        //            subhead.TextNudge.Y = -this.Height / 17;
        //            chartVu.AddChartObject(subhead);

        //            Font theFooterFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        //            ChartTitle footer = new ChartTitle(pAntennaTransform, theFooterFont, "Click on the button to re-initialize with new data.");
        //            footer.SetTitleType(ChartObj.CHART_FOOTER);
        //            footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
        //            footer.SetTitleOffset(8);
        //            chartVu.AddChartObject(footer);


        //            Font legendFont = new Font("SansSerif", 12, FontStyle.Bold);
        //            ChartAttribute legendAttributes = new ChartAttribute(Color.Black, 1, 0);
        //            legendAttributes.SetLineFlag(false);
        //            StandardLegend legend = new StandardLegend(0.8, 0.2, 0.2, 0.4, legendAttributes, StandardLegend.VERT_DIR);
        //            legend.AddLegendItem("Antenna A", thePlot1, legendFont);
        //            legend.AddLegendItem("Antenna B", thePlot2, legendFont);
        //            chartVu.AddChartObject(legend);

        //            DataToolTip datatooltip = new DataToolTip(chartVu);
        //            datatooltip.SetEnable(true);
        //            datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
        //            chartVu.SetCurrentMouseListener(datatooltip);

        //        }


        public void DrawPolarPlot(double mag1, double ang1)
        {
           
            try
            {
                double[] mag = new double[1];
                double[] ang = new double[1];
                mag[0] = mag1;
                ang[0] = ang1;
                DrawPolarPlot(mag, ang);
            }
            catch
            {
            }
        }
        public ChartView chartVu;// = this;
        Font theFont;
        PolarCoordinates pPolarTransform;
        Color GraphBG1 = Color.White;
        Color GraphBG2 = Color.White;
        Color ChartBG1 = Color.White;
        Color ChartBG2 = Color.White;
        Color AxisColor = Color.Black;
        Color MainCursor = Color.Black;
        int GraphBGDir = 0;
        int ChartBGDir = 0;

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

        public void DrawPolarPlot(double[] mag1, double[] ang1)
        {
            try
            {
                RemovePreviousObjects();
                chartVu = this;

                Font theLabelFont = new Font("Courier", 10, FontStyle.Regular);
                string[] sarrxlab = new string[mag1.Length];
                int nump1 = mag1.Length;

                for (int i = 0; i < nump1; i++)
                {
                    sarrxlab[i] = "Mag " + mag1[i].ToString() + ", Angle " + ang1[i].ToString();
                    ang1[i] = ChartSupport.ToRadians((double)ang1[i]);
                }
                theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                chartVu = this;

                SimpleDataset Dataset1 = new SimpleDataset("First", mag1, ang1);
                pPolarTransform = new PolarCoordinates();
                pPolarTransform.SetGraphBorderDiagonal(0.25, .2, .75, 0.8);

                background = new Background(pPolarTransform, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);

                plotbackground = new Background(pPolarTransform, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                pPolarTransform.AutoScale(Dataset1);

                pPolarAxis = pPolarTransform.GetCompatibleAxes();
                pPolarAxis.SetColor(_AxisColor);
                chartVu.AddChartObject(pPolarAxis);

                pPolarGrid = new PolarGrid(pPolarAxis, PolarGrid.GRID_MAJOR);
                pPolarGrid.SetColor(_AxisColor);
                chartVu.AddChartObject(pPolarGrid);

                pPolarAxisLabels = (PolarAxesLabels)pPolarAxis.GetCompatibleAxesLabels();
                pPolarAxisLabels.SetColor(_AxisColor);
                chartVu.AddChartObject(pPolarAxisLabels);

                ChartAttribute attrib1 = new ChartAttribute(Color.Blue, 2, 0);

                ChartAttribute attrib2 = new ChartAttribute(Color.Red, .5, 0, Color.Red);
                attrib2.SetFillFlag(true);

                thePlot2 = new PolarScatterPlot(pPolarTransform, Dataset1, ChartObj.CIRCLE, attrib2);
                chartVu.AddChartObject(thePlot2);

                PolarLinePlot thePlot1 = new PolarLinePlot(pPolarTransform, Dataset1, attrib1);
                chartVu.AddChartObject(thePlot1);


                findObj = new CustomFindObj1(chartVu, sarrxlab);
                findObj.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_CUSTOM);
                findObj.SetEnable(true);
                chartVu.SetCurrentMouseListener(findObj);

                if (_ChartFooter != null)
                {
                    ChartTitle footer = new ChartTitle(pPolarTransform, theFont, _ChartFooter);
                    footer.SetColor(Color.Black);
                    footer.SetTitleType(ChartObj.CHART_FOOTER);
                    footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
                    footer.SetTitleOffset(8);
                    chartVu.AddChartObject(footer);
                }
                this.SetResizeMode(ChartObj.NO_RESIZE_OBJECTS);
            }
            catch (Exception ex)
            {
            }
        }

        public void DrawPolarPlotnoLine(double[] mag1, double[] ang1)
        {
            try
            {
                RemovePreviousObjects();
                chartVu = this;

                Font theLabelFont = new Font("Courier", 10, FontStyle.Regular);
                string[] sarrxlab = new string[mag1.Length];
                int nump1 = mag1.Length;

                for (int i = 0; i < nump1; i++)
                {
                    sarrxlab[i] =(i+1).ToString() + ". Mag " + mag1[i].ToString() + ", Angle " + ang1[i].ToString();
                    ang1[i] = ChartSupport.ToRadians((double)ang1[i]);
                }
                theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                chartVu = this;

                SimpleDataset Dataset1 = new SimpleDataset("First", mag1, ang1);
                pPolarTransform = new PolarCoordinates();
                pPolarTransform.SetGraphBorderDiagonal(0.25, .2, .75, 0.8);

                background = new Background(pPolarTransform, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                chartVu.AddChartObject(background);

                plotbackground = new Background(pPolarTransform, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                chartVu.AddChartObject(plotbackground);

                pPolarTransform.AutoScale(Dataset1);

                pPolarAxis = pPolarTransform.GetCompatibleAxes();
                pPolarAxis.SetColor(_AxisColor);
                chartVu.AddChartObject(pPolarAxis);

                pPolarGrid = new PolarGrid(pPolarAxis, PolarGrid.GRID_MAJOR);
                pPolarGrid.SetColor(_AxisColor);
                chartVu.AddChartObject(pPolarGrid);

                pPolarAxisLabels = (PolarAxesLabels)pPolarAxis.GetCompatibleAxesLabels();
                pPolarAxisLabels.SetColor(_AxisColor);
                chartVu.AddChartObject(pPolarAxisLabels);

                ChartAttribute attrib1 = new ChartAttribute(Color.Blue, 2, 0);

                ChartAttribute attrib2 = new ChartAttribute(Color.Red, .5, 0, Color.Red);
                attrib2.SetFillFlag(true);

                thePlot2 = new PolarScatterPlot(pPolarTransform, Dataset1, ChartObj.CIRCLE, attrib2);
                chartVu.AddChartObject(thePlot2);

                //PolarLinePlot thePlot1 = new PolarLinePlot(pPolarTransform, Dataset1, attrib1);
                //chartVu.AddChartObject(thePlot1);


                findObj = new CustomFindObj1(chartVu, sarrxlab);
                findObj.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_CUSTOM);
                findObj.SetEnable(true);
                chartVu.SetCurrentMouseListener(findObj);

                if (_ChartFooter != null)
                {
                    //ChartTitle footer = new ChartTitle(pPolarTransform, theFont, _ChartFooter);
                    //footer.SetColor(Color.Black);
                    //footer.SetTitleType(ChartObj.CHART_FOOTER);
                    //footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
                    //footer.SetTitleOffset(8);
                    //chartVu.AddChartObject(footer);
                }
                this.SetResizeMode(ChartObj.NO_RESIZE_OBJECTS);
            }
            catch (Exception ex)
            {
            }
        }


        string ChartFooter = null;
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
        CustomFindObj1 findObj;
        PolarScatterPlot thePlot2;
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
        internal void CopyGraph()
        {
            try
            {
                if (chartVu != null)
                {
                    BufferedImage objImage = new BufferedImage(chartVu);

                    Image GraphImage = (Image)objImage.GetBufferedImage();
                    Clipboard.SetImage((Image)GraphImage);
                    MessageBoxEx.Show("Polar Graph Copied on ClipBoard", "Graph");
                }
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


                    zoomObj = new ChartZoom(chartVu, pPolarTransform, true);

                    zoomObj.SetButtonMask(MouseButtons.Left);
                    zoomObj.SetZoomYEnable(true);
                    zoomObj.SetZoomXEnable(true);

                    zoomObj.SetZoomXRoundMode(ChartObj.AUTOAXES_EXACT);
                    zoomObj.SetZoomYRoundMode(ChartObj.AUTOAXES_EXACT);
                    zoomObj.InternalZoomStackProcesssing = true;

                    zoomObj.SetEnable(true);

                    thePlot2.SetShowDatapointValue(true);
                    NumericLabel modellabel = new NumericLabel();
                    modellabel.SetXJust(ChartObj.JUSTIFY_MIN);
                    modellabel.SetYJust(ChartObj.JUSTIFY_CENTER);
                    Font modellabelfont = new Font("Arial", 8, FontStyle.Regular);
                    modellabel.SetTextFont(modellabelfont);
                    modellabel.DecimalPos = 2;
                    modellabel.SetTextNudge(0, 5);
                    thePlot2.SetPlotLabelTemplate(modellabel);
                }
                chartVu.SetCurrentMouseListener(zoomObj);
                chartVu.UpdateDraw();
            }
            catch (Exception ex)
            {
            }
        }
        ChartZoom zoomObj = null;
        DataToolTip datatooltip;
        internal void StopZoom()
        {
            try
            {
                //int k= zoomObj.PushZoomStack();
                ChartZoom zoomObj1 = new ChartZoom(chartVu, pPolarTransform, true);
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
                thePlot2.SetShowDatapointValue(false);
                chartVu.SetCurrentMouseListener(findObj);
                chartVu.UpdateDraw();
            }
            catch (Exception ex)
            {
                //ErrorLogFile(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }
        Background background;
        Background plotbackground;
        PolarAxes pPolarAxis;
        PolarGrid pPolarGrid;
        PolarAxesLabels pPolarAxisLabels;
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
                            if (obj.Name == "PolarLinePlot" || obj.Name == "PolarScatterPlot")
                            {
                                iDel++;
                            }
                            else
                            {
                                chartVu.DeleteChartObject(objObject);
                            }
                        }
                    }

                    plotbackground = new Background(pPolarTransform, ChartObj.PLOT_BACKGROUND, ChartBG1, ChartBG2, ChartBGDir);
                    chartVu.AddChartObject(plotbackground);

                    background = new Background(pPolarTransform, ChartObj.GRAPH_BACKGROUND, GraphBG1, GraphBG2, GraphBGDir);
                    chartVu.AddChartObject(background);


                    pPolarAxis = pPolarTransform.GetCompatibleAxes();
                    pPolarAxis.SetColor(_AxisColor);                   
                    chartVu.AddChartObject(pPolarAxis);

                    pPolarGrid = new PolarGrid(pPolarAxis, PolarGrid.GRID_MAJOR);
                    pPolarGrid.SetColor(_AxisColor);
                    pPolarGrid.LineColor = _AxisColor;
                    chartVu.AddChartObject(pPolarGrid);


                    pPolarAxisLabels = (PolarAxesLabels)pPolarAxis.GetCompatibleAxesLabels();
                    pPolarAxisLabels.SetColor(_AxisColor);
                    chartVu.AddChartObject(pPolarAxisLabels);
   
                    if (_ChartFooter != null)
                    {
                        ChartTitle footer = new ChartTitle(pPolarTransform, theFont, _ChartFooter);
                        footer.SetColor(_AxisColor);
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
    }
    class CustomFindObj1 : CustomDataToolTip
    {
        String[] labelarray;
        public CustomFindObj1(ChartView component, String[] labels)
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
