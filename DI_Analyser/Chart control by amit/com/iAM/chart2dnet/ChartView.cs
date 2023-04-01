namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Reflection;
    using System.Windows.Forms;

    public class ChartView : UserControl
    {
        private const int ACTIVE_STATE = 0;
        private Version assemblyVersion;
        internal bool backgroundDrawEnable;
        private static ChartInfo chartInformation;
        internal ArrayList chartObjectsArrayList;
        private Container components;
        internal ControlStyles controlStyle;
        private static ChartCalendar currentDate = new ChartCalendar();
        private MouseListener currentMouseListener;
        private Color developerMessageColor;
        private const int DFYM = 3;
        private static long dllFlags = 1L;
        private int dllMessageFlag;
        private const int DM = 0;
        internal bool doubleBufferEnable;
        private const int DOYM = 2;
        private const int DTM = 1;
        private const int DUM = 4;
        private static ChartCalendar eDate = new ChartCalendar(new DateTime(0x7d5, 12, 0x1f));
        private static long eDays = 0x1fL;
        private static long eDays2 = 0x1fL;
        private static long elapsedDays = 0L;
        private const int EXPIRED_PAST_GRACE = 2;
        private const int EXPIRED_WITHIN_GRACE = 1;
        private static long installDate = 0L;
        private string licenseFilename;
        private static int licenseMode = 4;
        private static string licensePath = "NOTINITIALIZED";
        private static int lS = 0;
        internal Size minimumSize;
        private static int oGP = 10;
        internal bool onPainBackgroundEnable;
        internal Size preferredSize;
        private int preRenderMode;
        internal ArrayList previousChartObjectsArrayList;
        private static long productID = 3L;
        private string[] productIDStringArray;
        private const int REDISTRIBUTABLE = 6;
        private const int REDISTRIBUTABLE_INDESIGNMODE = 3;
        internal int renderingMode;
        internal int resizeMode;
        internal double resizeMultiplier;
        private const int SERVERM = 5;
        private System.Drawing.Drawing2D.SmoothingMode smoothingMode;
        private System.Drawing.Text.TextRenderingHint textRenderingHint;
        private static long trialDllFlags = 1L;
        internal Rectangle2D viewViewport;
        private static int vMC = 0;
        internal bool zOrderSortEnable;

        public ChartView()
        {
            this.licenseFilename = "iAMLicense.lic.xml";
            this.assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.components = null;
            this.currentMouseListener = null;
            this.chartObjectsArrayList = null;
            this.previousChartObjectsArrayList = null;
            this.viewViewport = null;
            this.minimumSize = new Size(200, 200);
            this.preferredSize = new Size(600, 400);
            this.zOrderSortEnable = true;
            this.resizeMultiplier = 1.0;
            this.controlStyle = ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint;
            this.resizeMode = 1;
            this.backgroundDrawEnable = true;
            this.doubleBufferEnable = true;
            this.onPainBackgroundEnable = false;
            this.renderingMode = 0;
            this.preRenderMode = 2;
            this.productIDStringArray = new string[] { 
                "", "iAMChart2D", "iAMRTGraph", "iAMChart2D CF", "iAMRTGraph CF", "", "", "iAMSPCChart", "", "", "", "", "", "", "", "", 
                "", "", "", "All Amit-Charts .Net Products"
             };
            this.textRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.smoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.dllMessageFlag = 0;
            this.developerMessageColor = CFSupportClasses.DefColor(0x80, (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()));
            this.InitializeComponent();
            this.InitDefaults();
            this.CheckLicense();
        }

        public ChartView(Rectangle positionRect, Control parent)
        {
            this.licenseFilename = "iAMLicense.lic.xml";
            this.assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.components = null;
            this.currentMouseListener = null;
            this.chartObjectsArrayList = null;
            this.previousChartObjectsArrayList = null;
            this.viewViewport = null;
            this.minimumSize = new Size(200, 200);
            this.preferredSize = new Size(600, 400);
            this.zOrderSortEnable = true;
            this.resizeMultiplier = 1.0;
            this.controlStyle = ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint;
            this.resizeMode = 1;
            this.backgroundDrawEnable = true;
            this.doubleBufferEnable = true;
            this.onPainBackgroundEnable = false;
            this.renderingMode = 0;
            this.preRenderMode = 2;
            this.productIDStringArray = new string[] { 
                "", "iAMChart2D", "iAMRTGraph", "iAMChart2D CF", "iAMRTGraph CF", "", "", "iAMSPCChart", "", "", "", "", "", "", "", "", 
                "", "", "", "All Amit-Charts .Net Products"
             };
            this.textRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.smoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.dllMessageFlag = 0;
            this.developerMessageColor = CFSupportClasses.DefColor(0x80, (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()));
            this.InitializeComponent();
            this.InitDefaults();
            this.CheckLicense();
            base.Location = new Point(positionRect.X, positionRect.Y);
            base.Size = new Size(positionRect.Width, positionRect.Height);
            this.preferredSize = base.Size;
            if (parent != null)
            {
                parent.Controls.Add(this);
            }
        }

        public ChartView(Rectangle positionRect, Form parent)
        {
            this.licenseFilename = "iAMLicense.lic.xml";
            this.assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.components = null;
            this.currentMouseListener = null;
            this.chartObjectsArrayList = null;
            this.previousChartObjectsArrayList = null;
            this.viewViewport = null;
            this.minimumSize = new Size(200, 200);
            this.preferredSize = new Size(600, 400);
            this.zOrderSortEnable = true;
            this.resizeMultiplier = 1.0;
            this.controlStyle = ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint;
            this.resizeMode = 1;
            this.backgroundDrawEnable = true;
            this.doubleBufferEnable = true;
            this.onPainBackgroundEnable = false;
            this.renderingMode = 0;
            this.preRenderMode = 2;
            this.productIDStringArray = new string[] { 
                "", "iAMChart2D", "iAMRTGraph", "iAMChart2D CF", "iAMRTGraph CF", "", "", "iAMSPCChart", "", "", "", "", "", "", "", "", 
                "", "", "", "All Amit-Charts .Net Products"
             };
            this.textRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.smoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.dllMessageFlag = 0;
            this.developerMessageColor = CFSupportClasses.DefColor(0x80, (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()));
            this.InitializeComponent();
            this.InitDefaults();
            this.CheckLicense();
            base.Location = new Point(positionRect.X, positionRect.Y);
            base.Size = new Size(positionRect.Width, positionRect.Height);
            this.preferredSize = base.Size;
            if (parent != null)
            {
                parent.Controls.Add(this);
            }
        }

        public ChartView(Rectangle positionRect, Panel parent)
        {
            this.licenseFilename = "iAMLicense.lic.xml";
            this.assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.components = null;
            this.currentMouseListener = null;
            this.chartObjectsArrayList = null;
            this.previousChartObjectsArrayList = null;
            this.viewViewport = null;
            this.minimumSize = new Size(200, 200);
            this.preferredSize = new Size(600, 400);
            this.zOrderSortEnable = true;
            this.resizeMultiplier = 1.0;
            this.controlStyle = ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint;
            this.resizeMode = 1;
            this.backgroundDrawEnable = true;
            this.doubleBufferEnable = true;
            this.onPainBackgroundEnable = false;
            this.renderingMode = 0;
            this.preRenderMode = 2;
            this.productIDStringArray = new string[] { 
                "", "iAMChart2D", "iAMRTGraph", "iAMChart2D CF", "iAMRTGraph CF", "", "", "iAMSPCChart", "", "", "", "", "", "", "", "", 
                "", "", "", "All Amit-Charts .Net Products"
             };
            this.textRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.smoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.dllMessageFlag = 0;
            this.developerMessageColor = CFSupportClasses.DefColor(0x80, (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()));
            this.InitializeComponent();
            this.InitDefaults();
            this.CheckLicense();
            base.Location = new Point(positionRect.X, positionRect.Y);
            base.Size = new Size(positionRect.Width, positionRect.Height);
            this.preferredSize = base.Size;
            if (parent != null)
            {
                parent.Controls.Add(this);
            }
        }

        public ChartView(Rectangle positionRect, TabPage parent)
        {
            this.licenseFilename = "iAMLicense.lic.xml";
            this.assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.components = null;
            this.currentMouseListener = null;
            this.chartObjectsArrayList = null;
            this.previousChartObjectsArrayList = null;
            this.viewViewport = null;
            this.minimumSize = new Size(200, 200);
            this.preferredSize = new Size(600, 400);
            this.zOrderSortEnable = true;
            this.resizeMultiplier = 1.0;
            this.controlStyle = ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint;
            this.resizeMode = 1;
            this.backgroundDrawEnable = true;
            this.doubleBufferEnable = true;
            this.onPainBackgroundEnable = false;
            this.renderingMode = 0;
            this.preRenderMode = 2;
            this.productIDStringArray = new string[] { 
                "", "iAMChart2D", "iAMRTGraph", "iAMChart2D CF", "iAMRTGraph CF", "", "", "iAMSPCChart", "", "", "", "", "", "", "", "", 
                "", "", "", "All Amit-Charts .Net Products"
             };
            this.textRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.smoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.dllMessageFlag = 0;
            this.developerMessageColor = CFSupportClasses.DefColor(0x80, (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()), (int) (255.0 * ChartSupport.GetRandomDouble()));
            this.InitializeComponent();
            this.InitDefaults();
            this.CheckLicense();
            base.Location = new Point(positionRect.X, positionRect.Y);
            base.Size = new Size(positionRect.Width, positionRect.Height);
            this.preferredSize = base.Size;
            if (parent != null)
            {
                parent.Controls.Add(this);
            }
        }

        public int AddChartObject(GraphObj chartobj)
        {
            int num = 0;
            if (chartobj != null)
            {
                num = chartobj.ErrorCheck(0);
                if (num == 0)
                {
                    chartobj.SetChartObjComponent(this);
                    this.chartObjectsArrayList.Add(chartobj);
                    if (chartobj.CompositeGraphObj)
                    {
                        chartobj.AddInternalObjects();
                    }
                }
            }
            return num;
        }

        private void AxisCopyFixup(ArrayList source, ArrayList dest)
        {
            if (source != null)
            {
                int count = source.Count;
                GraphObj obj2 = null;
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) source[i];
                    if (ChartSupport.IsKindOf(obj2, "PolarAxes"))
                    {
                        PolarAxes axes1 = (PolarAxes) obj2;
                        int num3 = ChartSupport.FindRelatedPolarAxesLabels(i, source);
                        if (num3 >= 0)
                        {
                            PolarAxesLabels axeslabels = (PolarAxesLabels) dest[num3];
                            PolarAxes axes = (PolarAxes) dest[i];
                            axes.SetPolarAxesLabels(axeslabels);
                            axeslabels.SetBasePolarAxis(axes);
                        }
                    }
                    else if (ChartSupport.IsKindOf(obj2, "Axis"))
                    {
                        Axis axis1 = (Axis) obj2;
                        int num4 = ChartSupport.FindRelatedAxisLabels(i, source);
                        if (num4 >= 0)
                        {
                            AxisLabels axislabels = (AxisLabels) dest[num4];
                            Axis baseaxis = (Axis) dest[i];
                            baseaxis.SetAxisLabels(axislabels);
                            axislabels.SetBaseAxis(baseaxis);
                        }
                    }
                }
                for (int j = 0; j < count; j++)
                {
                    obj2 = (GraphObj) source[j];
                    if (ChartSupport.IsKindOf(obj2, "Grid"))
                    {
                        Grid grid1 = (Grid) obj2;
                        int xgridaxis = -1;
                        int ygridaxis = -1;
                        int num6 = ChartSupport.FindRelatedGridAxes(j, source, xgridaxis, ygridaxis);
                        Grid grid = (Grid) dest[j];
                        if (num6 >= 0)
                        {
                            Axis axis = (Axis) dest[num6];
                            grid.SetGridAxis(axis);
                        }
                        if (xgridaxis >= 0)
                        {
                            Axis axis3 = (Axis) dest[xgridaxis];
                            grid.SetGridAxis(axis3);
                        }
                        if (ygridaxis >= 0)
                        {
                            Axis axis4 = (Axis) dest[ygridaxis];
                            grid.SetGridAxis(axis4);
                        }
                    }
                }
                for (int k = 0; k < count; k++)
                {
                    obj2 = (GraphObj) source[k];
                    if (ChartSupport.IsKindOf(obj2, "AxisTitle"))
                    {
                        AxisTitle title1 = (AxisTitle) obj2;
                        int num10 = ChartSupport.FindRelatedAxisTitleAxis(k, source);
                        AxisTitle title = (AxisTitle) dest[k];
                        if (num10 >= 0)
                        {
                            Axis axis5 = (Axis) dest[num10];
                            title.SetTitleAxis(axis5);
                        }
                    }
                }
            }
        }

        public double CalcResizedWindowFontMultiplier(Size preferreddim, Size actualdim)
        {
            double num = ((double) actualdim.Width) / ((double) preferreddim.Width);
            double num2 = ((double) actualdim.Height) / ((double) preferreddim.Height);
            double resizeMultiplier = 1.0;
            if (this.resizeMode == 0)
            {
                return 1.0;
            }
            if (this.resizeMode == 1)
            {
                return Math.Min(num, num2);
            }
            if (this.resizeMode == 2)
            {
                resizeMultiplier = this.resizeMultiplier;
            }
            return resizeMultiplier;
        }

        private bool CheckForZOrderChanges()
        {
            bool flag = false;
            if (this.chartObjectsArrayList.Count != this.previousChartObjectsArrayList.Count)
            {
                return true;
            }
            for (int i = 0; i < this.chartObjectsArrayList.Count; i++)
            {
                GraphObj obj2 = (GraphObj) this.chartObjectsArrayList[i];
                GraphObj obj3 = (GraphObj) this.previousChartObjectsArrayList[i];
                if (obj2 != obj3)
                {
                    flag = true;
                }
                else if (obj2.ZOrder != obj3.ZOrder)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private void CheckLicense()
        {
            base.SetStyle(this.controlStyle, true);
            if (vMC == 0)
            {
                this.IsLicenseValid();
            }
            vMC++;
        }

        private void ClearViewport(Graphics g2, Rectangle2D rect, Color color)
        {
            SolidBrush cachedBrush = (SolidBrush) ChartAttribute.GetCachedBrush(color);
            g2.FillRectangle(cachedBrush, (int) rect.GetX(), (int) rect.GetY(), (int) rect.GetWidth(), (int) rect.GetHeight());
        }

        public void Copy(ChartView source)
        {
        }

        public bool DeleteChartObject(GraphObj chartobj)
        {
            int count = this.chartObjectsArrayList.Count;
            GraphObj obj2 = null;
            if (chartobj != null)
            {
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) this.chartObjectsArrayList[i];
                    if (chartobj == obj2)
                    {
                        this.chartObjectsArrayList.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public virtual void Draw(Graphics g2)
        {
            int count = this.chartObjectsArrayList.Count;
            GraphObj obj2 = null;
            this.PreDraw(g2);
            lock (this)
            {
                g2.TextRenderingHint = this.textRenderingHint;
                g2.SmoothingMode = this.smoothingMode;
                Rectangle2D rect = new Rectangle2D(0.0, 0.0, (double) base.ClientSize.Width, (double) base.ClientSize.Height);
                this.resizeMultiplier = this.CalcResizedWindowFontMultiplier(this.preferredSize, new Size((int) rect.GetWidth(), (int) rect.GetHeight()));
                this.ClearViewport(g2, rect, this.BackColor);
                ChartAttribute.SetResizeMultiplier(this.resizeMultiplier);
                if (this.chartObjectsArrayList != null)
                {
                    if (this.IsLicenseValid())
                    {
                        if (this.zOrderSortEnable)
                        {
                            this.preRender(g2, rect);
                        }
                        this.previousChartObjectsArrayList.Clear();
                        for (int i = 0; i < count; i++)
                        {
                            obj2 = (GraphObj) this.chartObjectsArrayList[i];
                            this.DrawObject(g2, obj2, rect);
                            this.previousChartObjectsArrayList.Add(this.chartObjectsArrayList[i]);
                        }
                    }
                    this.DrawLicenseMessages(g2, rect);
                }
            }
            this.PostDraw(g2);
        }

        private void DrawDeveloperMessage(Graphics g2, Rectangle2D drect, string message)
        {
            //ChartText text = new ChartText(new CartesianCoordinates(), GraphObj.GetDefaultChartFont(), message, 0.995, 0.005, 3, 2, 2, 0);
            //text.SetTextBgMode(false);
            //text.SetColor(this.developerMessageColor);
            //this.DrawObject(g2, text, drect);
        }

        private void DrawExpireLicenseMessage(Graphics g2, Rectangle2D drect)
        {
            if (!this.IsLicenseValid() || (lS == 3))
            {
                //string tstring = string.Copy(" ");
                //ChartText text = new ChartText(new CartesianCoordinates(), GraphObj.GetDefaultChartFont(), tstring, 0.25, 0.5, 3, 0, 1, 0);
                //text.AddNewLineTextString(" ");
                //text.AddNewLineTextString(" ");
                //text.AddNewLineTextString(" ");
                //if (!this.IsLicenseValid())
                //{
                //    text.AddNewLineTextString("   Either Your license to use " + this.productIDStringArray[(int) ((IntPtr) productID)] + " class library has expired,");
                //    text.AddNewLineTextString("   or the software cannot find the license file " + this.licenseFilename);
                //    text.AddNewLineTextString("   You need to subscribe or resubscribe to eliminate  ");
                //    text.AddNewLineTextString("   this message.");
                //    text.AddNewLineTextString(" ");
                //    text.AddNewLineTextString("             www.Amit-Charts.com     \t\t\t");
                //}
                //else if (lS == 3)
                //{
                //    text.AddNewLineTextString("   You cannot use the Redistributable License in the compilers");
                //    text.AddNewLineTextString("   design mode. The redistributable license should be used when");
                //    text.AddNewLineTextString("   your executable and the iAMChartNet.dll (and possibly iAMRTGraphNet.dll)");
                //    text.AddNewLineTextString("   are installed on a target computer and NOT the development computer. ");
                //    text.AddNewLineTextString(" ");
                //    text.AddNewLineTextString(" ");
                //    throw new ChartException("Invalid use of Redistributable License in Design Mode.");
                //}
                //text.AddNewLineTextString(" ");
                //text.SetTextBgMode(true);
                //text.SetTextBoxMode(true);
                //text.SetTextBgColor(Color.LightGray);
                //text.SetLineWidth(4.0);
                //this.DrawObject(g2, text, drect);
            }
        }

        private void DrawInvalidLicenseMessage(Graphics g2, Rectangle2D drect)
        {
            if (this.dllMessageFlag != 0)
            {
                //string tstring = string.Copy(" ");
                //ChartText text = new ChartText(new CartesianCoordinates(), GraphObj.GetDefaultChartFont(), tstring, 0.175, 0.5, 3, 0, 1, 0);
                //text.AddNewLineTextString(" ");
                //text.AddNewLineTextString(" ");
                //text.AddNewLineTextString("   Your license to use the " + this.productIDStringArray[(int) ((IntPtr) productID)] + " software has expired.   ");
                //text.AddNewLineTextString("   Use this opportunity to purchase the product from our web site.  ");
                //text.AddNewLineTextString(" ");
                //text.AddNewLineTextString("             www.Amit-Charts.com     \t\t\t");
                //text.AddNewLineTextString(" ");
                //text.AddNewLineTextString(" ");
                //text.SetTextBgMode(true);
                //text.SetTextBoxMode(true);
                //int a = Math.Min(220, Math.Max(0x40, 3 * ((int) (elapsedDays - eDays2))));
                //Color lightGray = Color.LightGray;
                //Color color = CFSupportClasses.DefColor(a, lightGray.R, lightGray.G, lightGray.B);
                //this.ClearViewport(g2, drect, color);
                //text.SetTextBgColor(Color.LightGray);
                //this.DrawObject(g2, text, drect);
            }
        }

        private void DrawLicenseMessages(Graphics g2, Rectangle2D drect)
        {
            //long num = installDate & 0xffffL;
            //string message = this.productIDStringArray[(int) ((IntPtr) productID)] + " Trial " + this.assemblyVersion.ToString() + " D" + num.ToString();
            //string text1 = this.productIDStringArray[(int) ((IntPtr) productID)] + "Dev. " + this.assemblyVersion.ToString();
            //if (this.dllMessageFlag != 0)
            //{
            //    this.DrawInvalidLicenseMessage(g2, drect);
            //}
            //if (licenseMode == 1)
            //{
            //    this.DrawDeveloperMessage(g2, drect, message);
            //}
        }

        public void DrawObject(Graphics g2, GraphObj graphobject1, Rectangle2D viewrect)
        {
            string str = "";
            if (graphobject1 != null)
            {
                str = graphobject1.GetType().Namespace;
                if (chartInformation.NewLicense && (this.dllMessageFlag == 0))
                {
                    if (!this.IsDLLValid(0))
                    {
                        this.dllMessageFlag = 1;
                    }
                    else if (str.Equals("com.iAM.rtgraphnet") && !this.IsDLLValid(1))
                    {
                        this.dllMessageFlag = 2;
                    }
                }
                if (((graphobject1.GetChartObjEnable() != 0) && (graphobject1.GetChartObjScale() != null)) && (!(graphobject1 is Background) || this.backgroundDrawEnable))
                {
                    graphobject1.GetChartObjScale().SetUserViewport((double) ((int) viewrect.GetX()), (double) ((int) viewrect.GetY()), (double) ((int) viewrect.GetWidth()), (double) ((int) viewrect.GetHeight()));
                    Rectangle2D cliprect = new Rectangle2D(viewrect.GetX(), viewrect.GetY(), viewrect.GetWidth(), viewrect.GetHeight());
                    if (cliprect != null)
                    {
                        graphobject1.GetChartObjScale().SetClippingBounds(cliprect);
                    }
                    graphobject1.SetResizeMultiplier(this.resizeMultiplier);
                    graphobject1.Draw(g2);
                }
            }
        }

        public virtual void EnabledNoDraw(Graphics g2)
        {
            int count = this.chartObjectsArrayList.Count;
            GraphObj obj2 = null;
            int benable = 1;
            Rectangle2D rect = new Rectangle2D(0.0, 0.0, (double) base.ClientSize.Width, (double) base.ClientSize.Height);
            this.resizeMultiplier = this.CalcResizedWindowFontMultiplier(this.preferredSize, new Size((int) rect.GetWidth(), (int) rect.GetHeight()));
            this.ClearViewport(g2, rect, this.BackColor);
            ChartAttribute.SetResizeMultiplier(this.resizeMultiplier);
            if (((this.chartObjectsArrayList != null) && this.IsLicenseValid()) && this.zOrderSortEnable)
            {
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) this.chartObjectsArrayList[i];
                    benable = obj2.GetChartObjEnable();
                    if (benable == 1)
                    {
                        obj2.SetChartObjEnable(2);
                    }
                    this.DrawObject(g2, obj2, rect);
                    obj2.SetChartObjEnable(benable);
                }
                this.SortChartObjectsByZOrder();
            }
        }

        public GraphObj FindObj(Point2D testpoint, string classname)
        {
            return this.FindObj(testpoint, classname, 0);
        }

        public GraphObj FindObj(Point2D testpoint, string classname, int nthhit)
        {
            int count = this.chartObjectsArrayList.Count;
            int num2 = 0;
            NearestPointData np = new NearestPointData();
            new Rectangle2D((double) (((int) testpoint.GetX()) - 1), (double) (((int) testpoint.GetY()) - 1), 3.0, 3.0);
            GraphObj obj2 = null;
            for (int i = 0; i < count; i++)
            {
                obj2 = (GraphObj) this.chartObjectsArrayList[i];
                if (((obj2 != null) && ChartSupport.IsKindOf(obj2, classname)) && obj2.CheckIntersection(testpoint, np))
                {
                    if (num2 == nthhit)
                    {
                        return obj2;
                    }
                    num2++;
                }
            }
            return null;
        }

        public bool GetBackgroundDrawEnable()
        {
            return this.backgroundDrawEnable;
        }

        public ArrayList GetChartObjectsArrayList()
        {
            return this.chartObjectsArrayList;
        }

        public ControlStyles GetControlStyle()
        {
            ControlStyles selectable = ControlStyles.Selectable;
            base.GetStyle(selectable);
            return selectable;
        }

        private long GetCurrentDay()
        {
            long num = 0x15180L;
            ChartCalendar calendar = new ChartCalendar(0x7d0, 1, 1);
            ChartCalendar calendar2 = new ChartCalendar();
            return ((calendar2.GetCalendarSecs() - calendar.GetCalendarSecs()) / num);
        }

        public MouseListener GetCurrentMouseListener()
        {
            return this.currentMouseListener;
        }

        public static string GetLicensePath()
        {
            return licensePath;
        }

        public Size GetMinimumSize()
        {
            return this.minimumSize;
        }

        public int GetResizeMode()
        {
            return this.resizeMode;
        }

        public double GetResizeMultiplier()
        {
            return this.resizeMultiplier;
        }

        public Rectangle2D GetViewport()
        {
            Rectangle2D rectangled = null;
            if (this.viewViewport != null)
            {
                rectangled = (Rectangle2D) this.viewViewport.Clone();
            }
            return rectangled;
        }

        public bool GetZOrderSortEnable()
        {
            return this.zOrderSortEnable;
        }

        private void InitDefaults()
        {
            this.chartObjectsArrayList = new ArrayList(100);
            this.previousChartObjectsArrayList = new ArrayList(100);
            if (licensePath == "NOTINITIALIZED")
            {
                licensePath = Environment.SystemDirectory;
            }
            base.SetStyle(this.controlStyle, true);
        }

        private void InitializeComponent()
        {
            base.Name = "ChartView";
            base.Size = new Size(0x1a8, 0x110);
        }

        protected bool IsDLLValid(int dllnum)
        {
            bool flag = false;
            long num = ((long) 0xffffL) << (dllnum * 0x10);
            if (chartInformation.NewLicense)
            {
                if ((dllFlags & (((int) 1) << dllnum)) == 0L)
                {
                    return flag;
                }
                if ((trialDllFlags & (((int) 1) << dllnum)) == 0L)
                {
                    long num2 = (installDate & num) >> (dllnum * 0x10);
                    elapsedDays = this.GetCurrentDay() - num2;
                    if (elapsedDays > eDays2)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
            if (licenseMode < 4)
            {
                ChartCalendar dstop = (ChartCalendar) currentDate.Clone();
                eDays = ChartCalendar.CalendarDaysDiff(eDate, dstop, 0);
                if (eDays > oGP)
                {
                    lS = 2;
                    this.dllMessageFlag = 1;
                    elapsedDays = eDays - oGP;
                }
            }
            return true;
        }

        private bool IsLicenseValid()
        {
            //Amit Jain to validate license file

            bool flag = false;
            ChartCalendar dstop = (ChartCalendar)currentDate.Clone();
            if ((vMC == 0) && (licenseMode != 5))
            {
                if (licensePath != null)
                {
                    string str = (string)licensePath.Clone();
                    if ((str.Length > 0) && (str[str.Length - 1] != '.'))
                    {
                        str = str + ".";
                    }
                    chartInformation = new ChartInfo(str + this.licenseFilename);
                }
                if (!chartInformation.FileFound)
                {
                    chartInformation = new ChartInfo(licensePath, this.licenseFilename);
                }
                eDate = chartInformation.ExpirationDate;
                licenseMode = chartInformation.LicenseMode;
                productID = chartInformation.ProductID;
                dllFlags = chartInformation.DLLFlags;
                trialDllFlags = chartInformation.TrialDLLFlags;
                installDate = chartInformation.InstallDate;
                if (!this.IsDLLValid(0))
                {
                    this.dllMessageFlag = 1;
                }
            }
            if (!chartInformation.NewLicense)
            {
                if (licenseMode < 4)
                {
                    eDays = ChartCalendar.CalendarDaysDiff(eDate, dstop, 0);
                    if (eDays <= 0L)
                    {
                        lS = 0;
                        licenseMode = 1;
                    }
                    else if (eDays <= oGP)
                    {
                        lS = 1;
                        licenseMode = 1;
                    }
                    else
                    {
                        lS = 2;
                        this.dllMessageFlag = 1;
                    }
                }
            }
            else if (licenseMode < 4)
            {
                lS = 0;
            }
            if (this.IsDesignMode && (licenseMode == 6))
            {
                lS = 3;
            }
            return !flag;
            return true;
        }

        private bool LoadLicenseFile()
        {
            bool flag = false;
            ChartCalendar dstop = (ChartCalendar) currentDate.Clone();
            if (vMC == 0)
            {
                if (licensePath != null)
                {
                    string str = (string) licensePath.Clone();
                    if ((str.Length > 0) && (str[str.Length - 1] != '.'))
                    {
                        str = str + ".";
                    }
                    chartInformation = new ChartInfo(str + this.licenseFilename);
                }
                if (!chartInformation.FileFound)
                {
                    chartInformation = new ChartInfo(licensePath, this.licenseFilename);
                }
                eDate = chartInformation.ExpirationDate;
                licenseMode = chartInformation.LicenseMode;
                productID = chartInformation.ProductID;
                dllFlags = chartInformation.DLLFlags;
                trialDllFlags = chartInformation.TrialDLLFlags;
            }
            if (licenseMode < 4)
            {
                eDays = ChartCalendar.CalendarDaysDiff(eDate, dstop, 0);
                if (eDays <= 0L)
                {
                    lS = 0;
                }
                else if (eDays <= oGP)
                {
                    lS = 1;
                }
                else
                {
                    lS = 2;
                    flag = true;
                }
            }
            if (this.IsDesignMode && (licenseMode == 6))
            {
                lS = 3;
            }
            return !flag;
        }

        private ArrayList MakeNewTransformList(ArrayList sourceobj, ArrayList destobj)
        {
            ArrayList list = new ArrayList(20);
            ArrayList list2 = new ArrayList(20);
            int count = sourceobj.Count;
            for (int i = 0; i < count; i++)
            {
                PhysicalCoordinates chartObjScale = ((GraphObj) sourceobj[i]).GetChartObjScale();
                GraphObj obj3 = (GraphObj) destobj[i];
                int num2 = 0;
                while (num2 < list.Count)
                {
                    if (chartObjScale == list[num2])
                    {
                        obj3.SetChartObjScale((PhysicalCoordinates) list2[num2]);
                        break;
                    }
                    num2++;
                }
                if (num2 == list.Count)
                {
                    list.Add(chartObjScale);
                    PhysicalCoordinates coordinates2 = (PhysicalCoordinates) chartObjScale.Clone();
                    list2.Add(coordinates2);
                    obj3.SetChartObjScale(coordinates2);
                }
            }
            return list2;
        }

        protected override void OnClick(EventArgs e)
        {
            if (this.currentMouseListener != null)
            {
                this.currentMouseListener.OnClick(e);
            }
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (this.currentMouseListener != null)
            {
                this.currentMouseListener.OnDoubleClick(e);
            }
            base.OnDoubleClick(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.preferredSize = base.Size;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.currentMouseListener != null)
            {
                this.currentMouseListener.OnMouseDown(e);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.currentMouseListener != null)
            {
                this.currentMouseListener.OnMouseMove(e);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.currentMouseListener != null)
            {
                this.currentMouseListener.OnMouseUp(e);
            }
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics graphics = pe.Graphics;
            this.Draw(graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.onPainBackgroundEnable)
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        public virtual void PostDraw(Graphics g2)
        {
        }

        public virtual void PreDraw(Graphics g2)
        {
        }

        protected void preRender(Graphics g2, Rectangle2D drect)
        {
            int count = this.chartObjectsArrayList.Count;
            GraphObj obj2 = null;
            int benable = 1;
            switch (this.preRenderMode)
            {
                case 0:
                    if (!this.CheckForZOrderChanges() || !this.zOrderSortEnable)
                    {
                        break;
                    }
                    this.SortChartObjectsByZOrder();
                    return;

                case 1:
                    for (int i = 0; i < count; i++)
                    {
                        obj2 = (GraphObj) this.chartObjectsArrayList[i];
                        benable = obj2.GetChartObjEnable();
                        if (benable == 1)
                        {
                            obj2.SetChartObjEnable(2);
                        }
                        this.DrawObject(g2, obj2, drect);
                        obj2.SetChartObjEnable(benable);
                    }
                    if (this.CheckForZOrderChanges() && this.zOrderSortEnable)
                    {
                        this.SortChartObjectsByZOrder();
                        return;
                    }
                    break;

                case 2:
                    for (int j = 0; j < count; j++)
                    {
                        obj2 = (GraphObj) this.chartObjectsArrayList[j];
                        if (ChartSupport.IsKindOf(obj2, "Axis"))
                        {
                            benable = obj2.GetChartObjEnable();
                            if (benable == 1)
                            {
                                obj2.SetChartObjEnable(2);
                            }
                            this.DrawObject(g2, obj2, drect);
                            obj2.SetChartObjEnable(benable);
                        }
                    }
                    if (!this.CheckForZOrderChanges() || !this.zOrderSortEnable)
                    {
                        break;
                    }
                    this.SortChartObjectsByZOrder();
                    return;

                case 3:
                    if (!this.CheckForZOrderChanges())
                    {
                        break;
                    }
                    for (int k = 0; k < count; k++)
                    {
                        obj2 = (GraphObj) this.chartObjectsArrayList[k];
                        if (ChartSupport.IsKindOf(obj2, "Axis"))
                        {
                            benable = obj2.GetChartObjEnable();
                            if (benable == 1)
                            {
                                obj2.SetChartObjEnable(2);
                            }
                            this.DrawObject(g2, obj2, drect);
                            obj2.SetChartObjEnable(benable);
                        }
                    }
                    if (!this.zOrderSortEnable)
                    {
                        break;
                    }
                    this.SortChartObjectsByZOrder();
                    return;

                default:
                    return;
            }
        }

        public void ResetChartObjectList()
        {
            this.chartObjectsArrayList.Clear();
        }

        public void ResetPreviousChartObjectList()
        {
            this.previousChartObjectsArrayList.Clear();
        }

        public void ResetViewport()
        {
            this.viewViewport = null;
        }

        public void SetBackgroundDrawEnable(bool background)
        {
            this.backgroundDrawEnable = background;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int num = width;
            int num2 = height;
            if (num < this.minimumSize.Width)
            {
                num = this.minimumSize.Width;
            }
            if (num2 < this.minimumSize.Height)
            {
                num2 = this.minimumSize.Height;
            }
            base.SetBoundsCore(x, y, num, num2, specified);
        }

        public void SetControlStyle(ControlStyles style)
        {
            this.controlStyle = style;
            base.SetStyle(style, true);
        }

        public void SetCurrentMouseListener(MouseListener mouselistener)
        {
            this.currentMouseListener = mouselistener;
        }

        public static void SetLicensePath(string licensepath)
        {
            licensePath = licensepath;
        }

        public void SetMinimumSize(Size size)
        {
            this.minimumSize = new Size(size.Width, size.Height);
        }

        public void SetResizeMode(int mode)
        {
            this.resizeMode = mode;
        }

        public void SetResizeMultiplier(double multiplier)
        {
            this.resizeMultiplier = multiplier;
        }

        public void SetViewport(double x, double y, double w, double h)
        {
            this.viewViewport = new Rectangle2D();
            this.viewViewport.SetFrame(x, y, w, h);
        }

        public void SetZOrderSortEnable(bool enable)
        {
            this.zOrderSortEnable = enable;
        }

        public void SortChartObjectsByZOrder()
        {
            int count = this.chartObjectsArrayList.Count;
            ZOrderSortClass[] array = new ZOrderSortClass[count];
            GraphObj obj2 = null;
            if (count >= 2)
            {
                int index = 0;
                while (index < count)
                {
                    obj2 = (GraphObj) this.chartObjectsArrayList[index];
                    if (obj2 != null)
                    {
                        array[index] = new ZOrderSortClass(index, obj2.GetZOrder());
                    }
                    index++;
                }
                Array.Sort(array);
                ArrayList list = (ArrayList) this.chartObjectsArrayList.Clone();
                for (index = 0; index < count; index++)
                {
                    int indexKey = array[index].indexKey;
                    obj2 = (GraphObj) list[indexKey];
                    if (obj2 != null)
                    {
                        this.chartObjectsArrayList[index] = obj2;
                    }
                }
            }
        }

        public void UpdateDraw()
        {
            base.Invalidate();
            base.Update();
        }

        public bool BackgroundDrawEnable
        {
            get
            {
                return this.backgroundDrawEnable;
            }
            set
            {
                this.backgroundDrawEnable = value;
            }
        }

        public int DL
        {
            set
            {
                this.dllMessageFlag = value;
            }
        }

        public bool DoubleBufferEnable
        {
            get
            {
                return this.doubleBufferEnable;
            }
            set
            {
                this.doubleBufferEnable = value;
                if (value)
                {
                    this.controlStyle |= ControlStyles.DoubleBuffer;
                }
                else
                {
                    this.controlStyle &= ~this.controlStyle;
                }
            }
        }

        public bool IsDesignMode
        {
            get
            {
                bool flag = false;
                if ((this.Site != null) && this.Site.DesignMode)
                {
                    flag = true;
                }
                return flag;
            }
        }

        public int LS
        {
            get
            {
                return lS;
            }
        }

        public Size PreferredSize
        {
            get
            {
                return this.preferredSize;
            }
            set
            {
                this.preferredSize = value;
            }
        }

        public int PreRenderingMode
        {
            get
            {
                return this.preRenderMode;
            }
            set
            {
                this.preRenderMode = value;
            }
        }

        public int RenderingMode
        {
            get
            {
                return this.renderingMode;
            }
            set
            {
                this.renderingMode = value;
            }
        }

        public int ResizeMode
        {
            get
            {
                return this.resizeMode;
            }
            set
            {
                this.resizeMode = value;
            }
        }

        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get
            {
                return this.smoothingMode;
            }
            set
            {
                this.smoothingMode = value;
            }
        }

        public System.Drawing.Text.TextRenderingHint TextRenderingHint
        {
            get
            {
                return this.textRenderingHint;
            }
            set
            {
                this.textRenderingHint = value;
            }
        }

        public bool ZOrderSortEnable
        {
            get
            {
                return this.zOrderSortEnable;
            }
            set
            {
                this.zOrderSortEnable = value;
            }
        }

        private class ZOrderSortClass : IComparable
        {
            internal int indexKey = 0;
            internal int zOrder = 0;

            public ZOrderSortClass(int indexkey, int zorder)
            {
                this.indexKey = indexkey;
                this.zOrder = zorder;
            }

            public int CompareTo(object o)
            {
                int num = 0;
                ChartView.ZOrderSortClass class2 = (ChartView.ZOrderSortClass) o;
                if (class2.zOrder < this.zOrder)
                {
                    num = 1;
                }
                if (class2.zOrder > this.zOrder)
                {
                    num = -1;
                }
                if (class2.zOrder == this.zOrder)
                {
                    if (class2.indexKey > this.indexKey)
                    {
                        num = -1;
                    }
                    if (class2.indexKey < this.indexKey)
                    {
                        num = 1;
                    }
                }
                return num;
            }
        }
    }
}

