namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ChartAttribute : ChartObj
    {
        private float[,] altLineStyles;
        private static ChartBrushes chartBrushes = new ChartBrushes();
        private static ChartPens chartPens = new ChartPens();
        private QBrush currentBrush;
        private QPen currentPen;
        private Brush customBrush;
        private Pen customPen;
        private Color fillColor;
        private bool fillFlag;
        private int fillStyle;
        private bool lineFlag;
        private DashStyle lineStyle;
        private double lineWidth;
        private static double minimumLineWidth = 0.1;
        private Color primaryColor;
        private static double resizeMultiplier = 1.0;
        private double symbolSize;

        public ChartAttribute()
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
        }

        public ChartAttribute(ChartAttribute attr)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            this.primaryColor = attr.primaryColor;
            this.lineWidth = attr.lineWidth;
            this.lineStyle = attr.lineStyle;
            this.fillColor = attr.fillColor;
            this.fillFlag = attr.fillFlag;
            this.lineFlag = attr.lineFlag;
            this.fillStyle = attr.fillStyle;
            this.symbolSize = attr.symbolSize;
            this.customPen = attr.customPen;
            this.customBrush = attr.customBrush;
            this.UpdateAttributes();
        }

        public ChartAttribute(GraphObj source)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            if (source != null)
            {
                this.Copy(source.GetChartObjAttributes());
            }
        }

        public ChartAttribute(Color rgbcolor)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            this.primaryColor = rgbcolor;
        }

        public ChartAttribute(ChartPlot source, int ngroup)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            if (source != null)
            {
                this.Copy(source.GetSegmentAttributes(ngroup));
            }
        }

        public ChartAttribute(Color rgbcolor, double rlinewidth)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            this.primaryColor = rgbcolor;
            this.lineWidth = rlinewidth;
            this.UpdateAttributes();
        }

        public ChartAttribute(Color rgbcolor, double rlinewidth, DashStyle nlinestyle)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            this.primaryColor = rgbcolor;
            this.lineWidth = rlinewidth;
            this.lineStyle = nlinestyle;
            this.UpdateAttributes();
        }

        public ChartAttribute(Color rgbcolor, double rlinewidth, DashStyle nlinestyle, Color rgbfillcolor)
        {
            this.primaryColor = Color.Black;
            this.fillColor = Color.Black;
            this.lineWidth = 1.0;
            this.symbolSize = 8.0;
            this.lineStyle = DashStyle.Solid;
            this.fillStyle = 1;
            this.fillFlag = false;
            this.lineFlag = true;
            this.currentPen = null;
            this.customPen = null;
            this.currentBrush = null;
            this.customBrush = null;
            this.altLineStyles = new float[,] { { 10f, 0f, 10f, 0f }, { 8f, 4f, 8f, 4f }, { 4f, 4f, 4f, 4f }, { 4f, 2f, 4f, 2f }, { 2f, 2f, 2f, 2f }, { 1f, 1f, 1f, 1f }, { 1f, 2f, 1f, 2f }, { 1f, 4f, 1f, 4f }, { 1f, 8f, 1f, 8f }, { 8f, 4f, 1f, 4f } };
            this.InitDefaults();
            this.primaryColor = rgbcolor;
            this.lineWidth = rlinewidth;
            this.lineStyle = nlinestyle;
            this.fillColor = rgbfillcolor;
            this.fillFlag = true;
            this.UpdateAttributes();
        }

        public bool BrushChanged()
        {
            bool flag = true;
            if ((this.currentBrush != null) && this.fillColor.Equals(this.currentBrush.BrushColor))
            {
                flag = false;
            }
            return flag;
        }

        public override object Clone()
        {
            ChartAttribute attribute = new ChartAttribute();
            attribute.Copy(this);
            return attribute;
        }

        public void Copy(ChartAttribute source)
        {
            if (source != null)
            {
                this.primaryColor = source.primaryColor;
                this.lineWidth = source.lineWidth;
                this.lineStyle = source.lineStyle;
                this.fillColor = source.fillColor;
                this.fillFlag = source.fillFlag;
                this.lineFlag = source.lineFlag;
                this.fillStyle = source.fillStyle;
                this.symbolSize = source.symbolSize;
                this.customPen = source.customPen;
                this.customBrush = source.customBrush;
                this.UpdateAttributes();
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public static Brush GetCachedBrush(Color brushcolor)
        {
            return chartBrushes.GetBrush(brushcolor).TheBrush;
        }

        public static Pen GetCachedPen(Color pencolor, int penwidth)
        {
            float num = (float) Math.Max(minimumLineWidth, Math.Floor((double) ((penwidth * resizeMultiplier) + 0.01)));
            return chartPens.GetPen(pencolor, num, DashStyle.Solid).ThePen;
        }

        public static Pen GetCachedPen(Color pencolor, int penwidth, DashStyle penstyle)
        {
            float num = (float) Math.Max(minimumLineWidth, Math.Floor((double) ((penwidth * resizeMultiplier) + 0.01)));
            return chartPens.GetPen(pencolor, num, penstyle).ThePen;
        }

        public Color GetColor()
        {
            return this.primaryColor;
        }

        public Brush GetCurrentBrush()
        {
            if (this.customBrush != null)
            {
                return this.customBrush;
            }
            if ((this.currentBrush == null) || this.BrushChanged())
            {
                this.currentBrush = chartBrushes.GetBrush(this.fillColor);
                return this.currentBrush.TheBrush;
            }
            return this.currentBrush.TheBrush;
        }

        public Pen GetCurrentPen()
        {
            Pen customPen = null;
            float penwidth = (float) Math.Max(minimumLineWidth, Math.Floor((double) ((this.lineWidth * resizeMultiplier) + 0.01)));
            if (this.customPen != null)
            {
                customPen = this.customPen;
                customPen.Width = penwidth;
                return customPen;
            }
            if ((this.currentPen == null) || this.PenChanged())
            {
                this.currentPen = chartPens.GetPen(this.primaryColor, penwidth, this.lineStyle);
                return this.currentPen.ThePen;
            }
            return this.currentPen.ThePen;
        }

        public Color GetFillColor()
        {
            return this.fillColor;
        }

        public bool GetFillFlag()
        {
            return this.fillFlag;
        }

        public int GetFillStyle()
        {
            return this.fillStyle;
        }

        public Color GetLineColor()
        {
            return this.primaryColor;
        }

        public bool GetLineFlag()
        {
            return this.lineFlag;
        }

        public DashStyle GetLineStyle()
        {
            return this.lineStyle;
        }

        public double GetLineWidth()
        {
            return this.lineWidth;
        }

        public Color GetPrimaryColor()
        {
            return this.primaryColor;
        }

        public static double GetResizeMultiplier()
        {
            return resizeMultiplier;
        }

        public double GetSymbolSize()
        {
            return this.symbolSize;
        }

        private void InitDefaults()
        {
            base.chartObjType = 650;
        }

        protected bool PenChanged()
        {
            bool flag = true;
            if (this.currentPen != null)
            {
                float num = (float) Math.Max(minimumLineWidth, Math.Floor((double) ((this.lineWidth * resizeMultiplier) + 0.01)));
                if (((Math.Abs((float) (num - this.currentPen.Width)) < 0.1) && this.primaryColor.Equals(this.currentPen.PenColor)) && (this.lineStyle == this.currentPen.DashStyle))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public void SetColor(Color rgbcolor)
        {
            this.primaryColor = rgbcolor;
            this.fillColor = rgbcolor;
        }

        public void SetCurrentBrush(Brush brush)
        {
            this.customBrush = brush;
        }

        public void SetCurrentPen(Pen pen)
        {
            this.customPen = pen;
            this.primaryColor = this.customPen.Color;
            this.lineWidth = this.customPen.Width;
            this.lineStyle = pen.DashStyle;
        }

        public void SetFillColor(Color rgbfillcolor)
        {
            this.fillColor = rgbfillcolor;
        }

        public void SetFillFlag(bool bfillflag)
        {
            this.fillFlag = bfillflag;
        }

        public void SetFillStyle(int nfillstyle)
        {
            this.fillStyle = nfillstyle;
        }

        public void SetLineAttributes(Color rgbcolor, double rlinewidth, DashStyle nlinestyle)
        {
            this.primaryColor = rgbcolor;
            this.lineWidth = rlinewidth;
            this.lineStyle = nlinestyle;
        }

        public void SetLineColor(Color rgbcolor)
        {
            this.primaryColor = rgbcolor;
        }

        public void SetLineFlag(bool blineflag)
        {
            this.lineFlag = blineflag;
        }

        public void SetLineStyle(DashStyle nlinestyle)
        {
            this.lineStyle = nlinestyle;
        }

        public void SetLineWidth(double rlinewidth)
        {
            this.lineWidth = rlinewidth;
        }

        public void SetPrimaryColor(Color rgbcolor)
        {
            this.primaryColor = rgbcolor;
        }

        public static void SetResizeMultiplier(double multiplier)
        {
            resizeMultiplier = multiplier;
        }

        public void SetSymbolSize(double rsymbolsize)
        {
            this.symbolSize = rsymbolsize;
        }

        public void UpdateAttributes()
        {
            if (this.currentBrush == null)
            {
                this.currentBrush = chartBrushes.GetBrush(this.fillColor);
            }
            if (this.currentPen == null)
            {
                float penwidth = (float) Math.Max(minimumLineWidth, Math.Floor((double) ((this.lineWidth * resizeMultiplier) + 0.01)));
                this.currentPen = chartPens.GetPen(this.primaryColor, penwidth, this.lineStyle);
            }
        }

        public void UpdateBrushAttributes()
        {
            if ((this.currentBrush != null) && ChartSupport.IsType(this.currentBrush.TheBrush, "SolidBrush"))
            {
                SolidBrush theBrush = this.currentBrush.TheBrush;
                this.fillColor = theBrush.Color;
            }
        }

        public void UpdatePenAttributes()
        {
            if (this.currentPen != null)
            {
                this.primaryColor = this.currentPen.PenColor;
                this.lineStyle = this.currentPen.DashStyle;
            }
        }

        public Brush CurrentBrush
        {
            get
            {
                return this.GetCurrentBrush();
            }
            set
            {
                this.SetCurrentBrush(value);
            }
        }

        public Pen CurrentPen
        {
            get
            {
                return this.GetCurrentPen();
            }
            set
            {
                this.SetCurrentPen(value);
            }
        }

        public Color FillColor
        {
            get
            {
                return this.GetFillColor();
            }
            set
            {
                this.SetFillColor(value);
            }
        }

        public bool FillFlag
        {
            get
            {
                return this.GetFillFlag();
            }
            set
            {
                this.SetFillFlag(value);
            }
        }

        public bool LineFlag
        {
            get
            {
                return this.GetLineFlag();
            }
            set
            {
                this.SetLineFlag(value);
            }
        }

        public DashStyle LineStyle
        {
            get
            {
                return this.GetLineStyle();
            }
            set
            {
                this.SetLineStyle(value);
            }
        }

        public double LineWidth
        {
            get
            {
                return this.GetLineWidth();
            }
            set
            {
                this.SetLineWidth(value);
            }
        }

        public Color PrimaryColor
        {
            get
            {
                return this.GetPrimaryColor();
            }
            set
            {
                this.SetPrimaryColor(value);
            }
        }

        public double SymbolSize
        {
            get
            {
                return this.GetSymbolSize();
            }
            set
            {
                this.SetSymbolSize(value);
            }
        }
    }
}

