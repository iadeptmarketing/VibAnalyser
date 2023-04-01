using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using com.iAM.chart2dnet;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double[] X = { 1	,
2	,
3	,
4	,
5	,
6	,
7	,
8	,
9	,
10	,
11	,
12	,
13	,
14	,
15	,
16	,
17	,
18	,
19	,
20	,
21	,
22	,
23	,
24	,
25	,
26	,
27	,
28	,
29	,
30	,
31	,
32	,
33	,
34	,
35	,
36	,
37	,
38	,
39	,
40	,
41	,
42	,
43	,
44	,
45	,
46	,
47	,
48	,
49	,
50	,
51	,
52	,
53	,
54	,
55	,
56	,
57	,
58	,
59	,
60	,
61	,
62	,
63	,
64	,
65	,
66	,
67	,
68	,
69	,
70	,
71	,
72	,
73	,
74	,
75	,
76	,
77	,
78	,
79	,
80	,
81	,
82	,
83	,
84	,
85	,
86	,
87	,
88	,
89	,
90	,
91	,
92	,
93	,
94	,
95	,
96	,
97	,
98	,
99	,
100	
 };
        double[] Y = { 1.12513317	,
0.070225898	,
0.618567905	,
0.348720433	,
0.032278537	,
0.125164048	,
0.246705606	,
0.322420185	,
0.937440474	,
0.420944918	,
0.97786345	,
1.488985898	,
0.142286814	,
1.623926559	,
1.57203911	,
0.398290561	,
1.945774135	,
1.199060863	,
0.880050771	,
1.775264123	,
0.595696589	,
1.007872123	,
1.172030209	,
0.091397391	,
0.655362762	,
0.39526712	,
0.039711907	,
0.073092214	,
0.220229489	,
0.288780818	,
0.891198995	,
0.424202632	,
0.933574924	,
1.467077438	,
0.17030155	,
1.590238923	,
1.581543335	,
0.362675437	,
1.939418084	,
1.232300835	,
0.857851925	,
1.800399021	,
0.633990717	,
1.013787096	,
1.218222892	,
0.114092406	,
0.69062535	,
0.442473356	,
0.045269494	,
0.021386548	,
0.192074432	,
0.256433435	,
0.843971817	,
0.425592902	,
0.889337969	,
1.443290582	,
0.197177776	,
1.555301575	,
1.589160175	,
0.32690724	,
1.931075889	,
1.264528335	,
0.834271301	,
1.82373115	,
0.672607013	,
1.017749976	,
1.263629561	,
0.138259371	,
0.724303114	,
0.490257402	,
0.048945346	,
0.029867729	,
0.162283173	,
0.225438745	,
0.795836177	,
0.425099164	,
0.845238925	,
1.417657753	,
0.222847854	,
1.519184993	,
1.594861931	,
0.291071889	,
1.920768253	,
1.295669095	,
0.809371494	,
1.845221406	,
0.711460553	,
1.019769638	,
1.308169971	,
0.163843699	,
0.756346624	,
0.538536277	,
0.050737338	,
0.080586319	,
0.130901753	,
0.195854664	,
0.746871032	,
0.422708627	,
0.801363712	,
1.390214915	
 };
        double[] Y1 = { 0.902325006	,
0.07016819	,
0.579869014	,
0.341695537	,
0.032272932	,
0.1248375	,
0.244210644	,
0.316862962	,
0.806045879	,
0.408623065	,
0.829305369	,
0.996655393	,
0.141807188	,
0.998588921	,
0.999999228	,
0.387843276	,
0.93051575	,
0.931698371	,
0.770771226	,
0.979169185	,
0.561085498	,
0.845698194	,
0.921540789	,
0.091270197	,
0.609446891	,
0.385054726	,
0.03970147	,
0.07302715	,
0.218453576	,
0.284783739	,
0.777825851	,
0.411594217	,
0.803752025	,
0.994626016	,
0.169479544	,
0.999810999	,
0.999942251	,
0.354776903	,
0.932824854	,
0.943255333	,
0.756439331	,
0.973756895	,
0.592364668	,
0.848839975	,
0.938487178	,
0.113845042	,
0.637019356	,
0.428175933	,
0.045254033	,
0.021384918	,
0.190895588	,
0.253632231	,
0.74728828	,
0.412860866	,
0.776654887	,
0.99188215	,
0.195902576	,
0.999879959	,
0.999831389	,
0.321115602	,
0.935798305	,
0.953465417	,
0.740807228	,
0.968182163	,
0.623027308	,
0.85092828	,
0.953194049	,
0.137819305	,
0.662613663	,
0.470852987	,
0.048925806	,
0.029863288	,
0.1615718	,
0.223534027	,
0.714448917	,
0.412411122	,
0.748129673	,
0.988297186	,
0.221007947	,
0.998668431	,
0.999710437	,
0.286979192	,
0.939382339	,
0.962390641	,
0.723853678	,
0.962581157	,
0.652940703	,
0.851987436	,
0.965711461	,
0.163111625	,
0.686268755	,
0.512879993	,
0.050715572	,
0.080499124	,
0.130528234	,
0.19460493	,
0.679345996	,
0.410232171	,
0.718305531	,
0.983739437	
 };
        double[] Y2 = { 0.61978705	,
0.997539222	,
0.836534426	,
0.942187871	,
0.999479274	,
0.992217914	,
0.970328486	,
0.950217553	,
0.692356954	,
0.917668812	,
0.675388187	,
0.543113668	,
0.989962199	,
0.541489149	,
0.540302956	,
0.92572687	,
0.597420467	,
0.596471672	,
0.717373578	,
0.557712344	,
0.84667801	,
0.663208892	,
0.604593584	,
0.995837766	,
0.819964751	,
0.926777878	,
0.999212	,
0.997334703	,
0.976233758	,
0.959722434	,
0.712440891	,
0.916484188	,
0.694010274	,
0.544816534	,
0.985672685	,
0.540461335	,
0.540350899	,
0.937724012	,
0.595567141	,
0.587156034	,
0.727284432	,
0.56219654	,
0.829622748	,
0.660854206	,
0.591009041	,
0.993526649	,
0.803872219	,
0.909724637	,
0.998976211	,
0.999771351	,
0.981834702	,
0.968007404	,
0.73353459	,
0.915976703	,
0.713262105	,
0.547115364	,
0.980872381	,
0.540403313	,
0.540444179	,
0.948883896	,
0.593175922	,
0.578860779	,
0.737924014	,
0.566798104	,
0.812115758	,
0.659285463	,
0.579082038	,
0.990517942	,
0.788387054	,
0.891181658	,
0.998803371	,
0.999554125	,
0.986975648	,
0.975120127	,
0.755454426	,
0.916157062	,
0.732962476	,
0.550112661	,
0.97567699	,
0.541422303	,
0.540545942	,
0.959103309	,
0.59028671	,
0.571559957	,
0.749259097	,
0.571403616	,
0.794300685	,
0.658488722	,
0.568831877	,
0.986726766	,
0.773615717	,
0.871334943	,
0.998714241	,
0.996761695	,
0.991493278	,
0.981124144	,
0.777983786	,
0.917028253	,
0.752921956	,
0.553913067	
};

        double[] Y3 ={1.12513317	,
0.070225898	,
-0.618567905	,
-0.348720433	,
0.032278537	,
-0.125164048	,
-0.246705606	,
0.322420185	,
0.937440474	,
0.420944918	,
-0.97786345	,
-1.488985898	,
-0.142286814	,
1.623926559	,
1.57203911	,
-0.398290561	,
-1.945774135	,
-1.199060863	,
0.880050771	,
1.775264123	,
0.595696589	,
-1.007872123	,
-1.172030209	,
-0.091397391	,
0.655362762	,
0.39526712	,
-0.039711907	,
0.073092214	,
0.220229489	,
-0.288780818	,
-0.891198995	,
-0.424202632	,
0.933574924	,
1.467077438	,
0.17030155	,
-1.590238923	,
-1.581543335	,
0.362675437	,
1.939418084	,
1.232300835	,
-0.857851925	,
-1.800399021	,
-0.633990717	,
1.013787096	,
1.218222892	,
0.114092406	,
-0.69062535	,
-0.442473356	,
0.045269494	,
-0.021386548	,
-0.192074432	,
0.256433435	,
0.843971817	,
0.425592902	,
-0.889337969	,
-1.443290582	,
-0.197177776	,
1.555301575	,
1.589160175	,
-0.32690724	,
-1.931075889	,
-1.264528335	,
0.834271301	,
1.82373115	,
0.672607013	,
-1.017749976	,
-1.263629561	,
-0.138259371	,
0.724303114	,
0.490257402	,
-0.048945346	,
-0.029867729	,
0.162283173	,
-0.225438745	,
-0.795836177	,
-0.425099164	,
0.845238925	,
1.417657753	,
0.222847854	,
-1.519184993	,
-1.594861931	,
0.291071889	,
1.920768253	,
1.295669095	,
-0.809371494	,
-1.845221406	,
-0.711460553	,
1.019769638	,
1.308169971	,
0.163843699	,
-0.756346624	,
-0.538536277	,
0.050737338	,
0.080586319	,
-0.130901753	,
0.195854664	,
0.746871032	,
0.422708627	,
-0.801363712	,
-1.390214915	
};
        SimpleLinePlot thePlot1 = null;
        SimpleLinePlot thePlot2 = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //DrawGraph(X, Y, chartView1);
                //DrawGraph(X, Y3, chartView2);
                double[] mag1={1,2,3,4,5,6,7,8,9,10};
                double[] ang1={30.5,60.3,90.2,120.5,150.1,180.9,210.8,240.4,270.3,300.1};
                DrawPolarPlot(mag1, ang1);
            }
            catch (Exception ex)
            {
            }

        }
        public void DrawPolarPlot(double[] mag1, double[] ang1)
        {
            try
            {
               // RemovePreviousObjects();
               ChartView chartVu = chartView1;
               PolarCoordinates pPolarTransform = new PolarCoordinates();

                Font theLabelFont = new Font("Courier", 10, FontStyle.Regular);
                // ChartText[] sarrxlab = new ChartText[mag1.Length];
                string[] sarrxlab = new string[mag1.Length];
                int nump1 = mag1.Length;


                int i;

                for (i = 0; i < nump1; i++)
                {
                    //ChartText TTip = new ChartText(pPolarTransform, theLabelFont, "ToolTip", 0.6, 0.2, ChartObj.NORM_GRAPH_POS);// new string[mag1.Length];
                    //TTip.AddNewLineTextString(mag1[i].ToString() + ", " + ang1[i].ToString());
                    //sarrxlab[i] = TTip;// mag1[i].ToString() + ", " + ang1[i].ToString();                    
                    ang1[i] = ChartSupport.ToRadians((double)ang1[i]);
                    //ang1[i] = ((double)i * (360.0 / (double)nump1));//ChartSupport.ToRadians((double)i * (360.0/ (double)nump1));
                    //mag1[i] = i;// 10 * Math.Abs(30 * (Math.Sin(2 * (ang1[i])) * Math.Cos(2 * (ang1[i]))));
                }
                theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
               // chartVu = this;

                SimpleDataset Dataset1 = new SimpleDataset("First", mag1, ang1);

                pPolarTransform.SetGraphBorderDiagonal(0.25, .2, .75, 0.8);

                Background background = new Background(pPolarTransform, ChartObj.GRAPH_BACKGROUND,
                    Color.White);
                chartVu.AddChartObject(background);

                pPolarTransform.AutoScale(Dataset1);
                PolarAxes pPolarAxis = pPolarTransform.GetCompatibleAxes();
                chartVu.AddChartObject(pPolarAxis);

                PolarGrid pPolarGrid = new PolarGrid(pPolarAxis, PolarGrid.GRID_MAJOR);
                chartVu.AddChartObject(pPolarGrid);

                PolarAxesLabels pPolarAxisLabels = (PolarAxesLabels)pPolarAxis.GetCompatibleAxesLabels();
                chartVu.AddChartObject(pPolarAxisLabels);

                ChartAttribute attrib1 = new ChartAttribute(Color.Blue, 2, 0);
                PolarLinePlot thePlot1 = new PolarLinePlot(pPolarTransform, Dataset1, attrib1);
                chartVu.AddChartObject(thePlot1);

                ChartAttribute attrib2 = new ChartAttribute(Color.Red, .5, 0, Color.Red);
                attrib2.SetFillFlag(true);
                PolarScatterPlot thePlot2 = new PolarScatterPlot(pPolarTransform, Dataset1, ChartObj.CIRCLE, attrib2);
                thePlot2.SetShowDatapointValue(true);
                NumericLabel modellabel = new NumericLabel();
                modellabel.SetXJust(ChartObj.JUSTIFY_CENTER);
                modellabel.SetYJust(ChartObj.JUSTIFY_MIN);
                Font modellabelfont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                modellabel.SetTextFont(modellabelfont);
                //modellabel.SetTextNudge(0, 5);
                modellabel.DecimalPos = 1;
                modellabel.NumericValue = ChartSupport.ToDegrees(ang1[0]);
                thePlot2.SetPlotLabelTemplate(modellabel);
                chartVu.AddChartObject(thePlot2);

                Font theTitleFont = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                //mainTitle = new ChartTitle(pPolarTransform, theTitleFont, "Polar Line and Scatter Plots");
                //mainTitle.SetTitleType(ChartObj.CHART_HEADER);
                //mainTitle.SetTitlePosition(ChartObj.CENTER_GRAPH);

                //chartVu.AddChartObject(mainTitle);



                double polarmag = 1.5;
                double polarang = Math.PI;
                //ChartText polartext = new ChartText(pPolarTransform, theTitleFont, "Polar annotation",
                //    polarmag, polarang, ChartObj.POLAR_POS);
                //chartVu.AddChartObject(polartext);


                //Font theFooterFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                //footer = new ChartTitle(pPolarTransform, theFooterFont, "The polar line charts use true polar (not linear) interpolation between data points.");
                //footer.SetTitleType(ChartObj.CHART_FOOTER);
                //footer.SetTitlePosition(ChartObj.CENTER_GRAPH);
                //footer.SetTitleOffset(8);
                //chartVu.AddChartObject(footer);

                // Uses sll defaults for data tooltip
                DataToolTip datatooltip = new DataToolTip(chartVu);
                datatooltip.SetDataToolTipFormat(ChartObj.DATA_TOOLTIP_XY_ONELINE);
                datatooltip.SetEnable(true);
                chartVu.SetCurrentMouseListener(datatooltip);


                //CustomFindObj1 findObj = new CustomFindObj1(chartVu, sarrxlab);
                //chartVu.SetCurrentMouseListener(findObj);

                chartVu.SetResizeMode(ChartObj.AUTO_RESIZE_OBJECTS);
            }
            catch (Exception ex)
            {
            }
        }
        Background background = null;
        Font theFont;
        private void DrawGraph(double[] xdata, double[] ydata,ChartView chartVu)
        {
            try
            {
                CartesianCoordinates pTransform1 = null;
                SimpleDataset[] datasetarray = null;
                LinearAxis xAxis = null;
                LinearAxis yAxis = null;
                NumericAxisLabels xAxisLab = null;
                NumericAxisLabels yAxisLab = null;
                Grid xgrid = null;
                Grid ygrid = null;
                AxisTitle objXTitle = null;
                AxisTitle objYTitle = null;
                theFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                SimpleDataset Dataset1 = null;
                Dataset1 = new SimpleDataset("First", (double[])xdata, (double[])ydata);
                datasetarray = new SimpleDataset[1];
                pTransform1 = new CartesianCoordinates();
                double[] FindLength = (double[])xdata;
                datasetarray[0] = Dataset1.CompressSimpleDataset(GraphObj.DATACOMPRESS_MAX, GraphObj.DATACOMPRESS_MAX, 1, 0, FindLength.Length - 1, "NewData");
                pTransform1.AutoScale(datasetarray, ChartObj.AUTOAXES_EXACT, ChartObj.AUTOAXES_EXACT);
                //pTransform1.SetCoordinateBounds(1000, -10, -1000, 10);
                pTransform1.SetGraphBorderDiagonal(0.15, .175, .85, 0.75);
                //}
                
                Color objBackgroundColor = Color.FromArgb(241, 241, 247);
                 background = new Background(pTransform1, ChartObj.BACKGROUND_RECTANGLE, objBackgroundColor);

                chartVu.AddChartObject(background);
                background = new Background(pTransform1, ChartObj.BACKGROUND, Color.LightBlue);

                chartVu.AddChartObject(background);
               
                xAxis = new LinearAxis(pTransform1, ChartObj.X_AXIS);
                xAxis.CalcAutoAxis();
                chartVu.AddChartObject(xAxis);

                objXTitle = new AxisTitle(xAxis, theFont, "Sec");
                chartVu.AddChartObject(objXTitle);

                yAxis = new LinearAxis(pTransform1, ChartObj.Y_AXIS);
                yAxis.CalcAutoAxis();
                chartVu.AddChartObject(yAxis);
                objYTitle = new AxisTitle(yAxis, theFont, "ABC");
                chartVu.AddChartObject(objYTitle);

                //objXAxisTicks = new XAxisTicks();
                //objXAxisTicks.Visible = true;
                //objXAxisTicks.SetXAxisTicksWidth(2);

                //objYAxisTicks = new YAxisTicks();
                //objYAxisTicks.Visible = true;
                //objYAxisTicks.SetYAxisTicksWidth(2);

                Color transparentRed = Color.FromArgb(200, 255, 0, 0);
                Color transparentGreen = Color.FromArgb(200, 0, 255, 0);
                //ChartAttribute lossAttrib = new ChartAttribute(transparentRed, 1, DashStyle.Solid,transparentRed);
                //ChartAttribute profitAttrib = new ChartAttribute(transparentGreen, 1, DashStyle.Solid,
                //    transparentGreen);

                //profitAttrib.SetFillFlag(true);
                //lossAttrib.SetFillFlag(true);
                //profitAttrib.SetLineFlag(false);
                //lossAttrib.SetLineFlag(false);

                attrib1 = new ChartAttribute(Color.DarkRed, 1, DashStyle.Solid,transparentRed);
                thePlot1 = new SimpleLinePlot(pTransform1, datasetarray[0], attrib1);

                objLabel = new NumericLabel();
                objLabel.SetNumericValue(1);
                thePlot1.SetPlotLabelTemplate(objLabel);

                thePlot1.PlotLabelTemplate.SetTextBgMode(true);
                thePlot1.PlotLabelTemplate.SetTextString("DTime");
                
                
                chartVu.AddChartObject(thePlot1);
                chartVu.UpdateDraw();
            }
            catch (Exception ex)
            {
            }
        }
        ChartAttribute attrib1 = null;
        NumericLabel objLabel = null;
        //XAxisTicks objXAxisTicks = null;
        //YAxisTicks objYAxisTicks = null;

        private void chartView1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
