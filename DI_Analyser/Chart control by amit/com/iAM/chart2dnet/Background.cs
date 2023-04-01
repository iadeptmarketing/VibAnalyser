namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class Background : GraphObj
    {
        internal LinearGradientBrush backgroundGradient;
        internal int backgroundMode;
        internal TextureBrush backgroundTexture;
        internal int backgroundType;
        internal double barWidth;
        internal int gradientDirection;
        internal Color gradientStartColor;
        internal Color gradientStopColor;
        internal double roundedRectCornerHeight;
        internal double roundedRectCornerWidth;

        public Background()
        {
            this.backgroundType = 0;
            this.backgroundMode = 0;
            this.gradientStartColor = Color.White;
            this.gradientStopColor = Color.White;
            this.gradientDirection = 1;
            this.roundedRectCornerWidth = 8.0;
            this.roundedRectCornerHeight = 8.0;
            this.barWidth = 0.05;
            this.backgroundTexture = null;
            this.backgroundGradient = null;
            this.InitDefaults();
        }

        public Background(PhysicalCoordinates transform, int bgtype, Color bgcolor)
        {
            this.backgroundType = 0;
            this.backgroundMode = 0;
            this.gradientStartColor = Color.White;
            this.gradientStopColor = Color.White;
            this.gradientDirection = 1;
            this.roundedRectCornerWidth = 8.0;
            this.roundedRectCornerHeight = 8.0;
            this.barWidth = 0.05;
            this.backgroundTexture = null;
            this.backgroundGradient = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            base.chartObjAttributes.SetPrimaryColor(bgcolor);
            this.backgroundType = bgtype;
            this.SetBackgroundZOrder(this.backgroundType);
            this.backgroundMode = 0;
        }

        public Background(PhysicalCoordinates transform, int bgtype, LinearGradientBrush gradient)
        {
            this.backgroundType = 0;
            this.backgroundMode = 0;
            this.gradientStartColor = Color.White;
            this.gradientStopColor = Color.White;
            this.gradientDirection = 1;
            this.roundedRectCornerWidth = 8.0;
            this.roundedRectCornerHeight = 8.0;
            this.barWidth = 0.05;
            this.backgroundTexture = null;
            this.backgroundGradient = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.backgroundGradient = gradient;
            this.backgroundType = bgtype;
            this.backgroundMode = 2;
            this.SetBackgroundZOrder(this.backgroundType);
        }

        public Background(PhysicalCoordinates transform, int bgtype, TextureBrush texture)
        {
            this.backgroundType = 0;
            this.backgroundMode = 0;
            this.gradientStartColor = Color.White;
            this.gradientStopColor = Color.White;
            this.gradientDirection = 1;
            this.roundedRectCornerWidth = 8.0;
            this.roundedRectCornerHeight = 8.0;
            this.barWidth = 0.05;
            this.backgroundTexture = null;
            this.backgroundGradient = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.backgroundType = bgtype;
            this.backgroundTexture = texture;
            this.backgroundMode = 3;
            this.SetBackgroundZOrder(this.backgroundType);
        }

        public Background(PhysicalCoordinates transform, int bgtype, Color startcolor, Color stopcolor, int dir)
        {
            this.backgroundType = 0;
            this.backgroundMode = 0;
            this.gradientStartColor = Color.White;
            this.gradientStopColor = Color.White;
            this.gradientDirection = 1;
            this.roundedRectCornerWidth = 8.0;
            this.roundedRectCornerHeight = 8.0;
            this.barWidth = 0.05;
            this.backgroundTexture = null;
            this.backgroundGradient = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.backgroundType = bgtype;
            this.gradientStartColor = startcolor;
            this.gradientStopColor = stopcolor;
            this.gradientDirection = dir;
            this.backgroundMode = 1;
            this.SetBackgroundZOrder(this.backgroundType);
        }

        public Background(PhysicalCoordinates transform, int bgtype, Color color1, Color color2, double barwidth, int dir)
        {
            this.backgroundType = 0;
            this.backgroundMode = 0;
            this.gradientStartColor = Color.White;
            this.gradientStopColor = Color.White;
            this.gradientDirection = 1;
            this.roundedRectCornerWidth = 8.0;
            this.roundedRectCornerHeight = 8.0;
            this.barWidth = 0.05;
            this.backgroundTexture = null;
            this.backgroundGradient = null;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.backgroundType = bgtype;
            this.gradientStartColor = color1;
            this.gradientStopColor = color2;
            this.barWidth = barwidth;
            this.gradientDirection = dir;
            this.backgroundMode = 4;
            this.SetBackgroundZOrder(this.backgroundType);
        }

        private void AltBarDraw(Graphics g2, Rectangle2D bgrect, Brush brush1, Brush brush2)
        {
            double x = bgrect.X;
            double y = bgrect.Y;
            int num3 = (int) Math.Ceiling((double) (1.0 / this.barWidth));
            Brush brush = brush1;
            Rectangle2D rectangled = (Rectangle2D) bgrect.Clone();
            for (int i = 0; i < num3; i++)
            {
                if ((i % 2) == 0)
                {
                    brush = brush1;
                }
                else
                {
                    brush = brush2;
                }
                if (this.gradientDirection == 1)
                {
                    rectangled.SetFrame(x, y, bgrect.Width, bgrect.Height * this.barWidth);
                    y += bgrect.Height * this.barWidth;
                }
                else
                {
                    rectangled.SetFrame(x, y, bgrect.Width * this.barWidth, bgrect.Height);
                    x += bgrect.Width * this.barWidth;
                }
                g2.FillRectangle(brush, rectangled.GetRectangle());
            }
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D plotRect;
            bool flag = false;
            if (this.backgroundType == 1)
            {
                plotRect = base.chartObjScale.GetPlotRect();
            }
            else
            {
                plotRect = base.chartObjScale.GetGraphRect();
            }
            Rectangle2D rectangled2 = new Rectangle2D(plotRect.GetX(), plotRect.GetY(), 10.0, 10.0);
            if (rectangled2.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
            {
                flag = true;
            }
            return flag;
        }

        public override object Clone()
        {
            Background background = new Background();
            background.Copy(this);
            return background;
        }

        public void Copy(Background source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.backgroundType = source.backgroundType;
                this.backgroundMode = source.backgroundMode;
                this.gradientStopColor = source.gradientStopColor;
                this.gradientStartColor = source.gradientStartColor;
                this.gradientDirection = source.gradientDirection;
                this.roundedRectCornerWidth = source.roundedRectCornerWidth;
                this.roundedRectCornerHeight = source.roundedRectCornerHeight;
                this.barWidth = source.barWidth;
                if (source.backgroundTexture != null)
                {
                    this.backgroundTexture = (TextureBrush) source.backgroundTexture.Clone();
                }
                if (source.backgroundGradient != null)
                {
                    this.backgroundGradient = (LinearGradientBrush) source.backgroundGradient.Clone();
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            this.DrawBackground(g2);
        }

        private void DrawBackground(Graphics g2)
        {
            Rectangle2D plotRect;
            Brush cachedBrush = null;
            Point2D pointd = new Point2D();
            Point2D pointd2 = new Point2D();
            base.chartObjScale.SetContext(g2);
            base.chartObjScale.SetClippingArea(base.chartObjClipping);
            if (this.backgroundType == 1)
            {
                plotRect = base.chartObjScale.GetPlotRect();
            }
            else
            {
                plotRect = base.chartObjScale.GetGraphRect();
            }
            if (this.backgroundMode == 4)
            {
                cachedBrush = ChartAttribute.GetCachedBrush(this.gradientStartColor);
                ChartAttribute.GetCachedBrush(this.gradientStopColor);
            }
            else if (this.backgroundMode == 0)
            {
                cachedBrush = ChartAttribute.GetCachedBrush(base.chartObjAttributes.PrimaryColor);
            }
            else if (this.backgroundMode == 1)
            {
                if (this.gradientDirection == 0)
                {
                    pointd.SetLocation(plotRect.GetX(), plotRect.GetY());
                    pointd2.SetLocation((double) (plotRect.GetX() + plotRect.GetWidth()), plotRect.GetY());
                }
                else
                {
                    pointd.SetLocation(plotRect.GetX(), plotRect.GetY());
                    pointd2.SetLocation(plotRect.GetX(), plotRect.GetY() - plotRect.GetHeight());
                }
                LinearGradientBrush brush2 = new LinearGradientBrush(pointd.GetPoint(), pointd2.GetPoint(), this.gradientStartColor, this.gradientStopColor);
                cachedBrush = brush2;
            }
            else if (this.backgroundMode == 2)
            {
                if (this.backgroundGradient == null)
                {
                    return;
                }
                cachedBrush = this.backgroundGradient;
            }
            else if (this.backgroundMode == 3)
            {
                if (this.backgroundTexture == null)
                {
                    return;
                }
                cachedBrush = this.backgroundTexture;
            }
            if (this.GetChartObjEnable() == 1)
            {
                g2.FillRectangle(cachedBrush, plotRect.GetRectangleF());
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public LinearGradientBrush GetBackgroundGradient()
        {
            return this.backgroundGradient;
        }

        public int GetBackgroundMode()
        {
            return this.backgroundMode;
        }

        public TextureBrush GetBackgroundTexture()
        {
            return this.backgroundTexture;
        }

        public int GetBackgroundType()
        {
            return this.backgroundType;
        }

        public int GetGradientDirection()
        {
            return this.gradientDirection;
        }

        public Color GetGradientStartColor()
        {
            return this.gradientStartColor;
        }

        public Color GetGradientStopColor()
        {
            return this.gradientStopColor;
        }

        public void InitBackground(PhysicalCoordinates transform, int bgtype, Color bgcolor)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            base.chartObjAttributes.SetPrimaryColor(bgcolor);
            this.backgroundType = bgtype;
            this.SetBackgroundZOrder(this.backgroundType);
            this.backgroundMode = 0;
        }

        private void InitDefaults()
        {
            base.chartObjClipping = 1;
            base.chartObjType = 500;
            base.zOrder = 10;
        }

        public void SetBackgroundGradient(LinearGradientBrush gradient)
        {
            this.backgroundGradient = gradient;
        }

        public void SetBackgroundTexture(TextureBrush texture)
        {
            this.backgroundTexture = texture;
        }

        public void SetBackgroundType(int backgroundtype)
        {
            this.backgroundType = backgroundtype;
        }

        private void SetBackgroundZOrder(int bgtype)
        {
            if (bgtype == 1)
            {
                base.zOrder = 10;
            }
            else
            {
                base.zOrder = 9;
            }
        }

        public void SetGradientDirection(int gradientdir)
        {
            this.gradientDirection = gradientdir;
        }

        public void SetGradientStartColor(Color color)
        {
            this.gradientStartColor = color;
        }

        public void SetGradientStopColor(Color color)
        {
            this.gradientStopColor = color;
        }

        public LinearGradientBrush BackgroundGradient
        {
            get
            {
                return this.backgroundGradient;
            }
            set
            {
                this.backgroundGradient = value;
            }
        }

        public int BackgroundMode
        {
            get
            {
                return this.backgroundMode;
            }
            set
            {
                this.backgroundMode = value;
            }
        }

        public TextureBrush BackgroundTexture
        {
            get
            {
                return this.backgroundTexture;
            }
            set
            {
                this.backgroundTexture = value;
            }
        }

        public int BackgroundType
        {
            get
            {
                return this.backgroundType;
            }
            set
            {
                this.backgroundType = value;
            }
        }

        public double BarWidth
        {
            get
            {
                return this.barWidth;
            }
            set
            {
                this.barWidth = value;
            }
        }

        public Color FillColor
        {
            get
            {
                return base.chartObjAttributes.PrimaryColor;
            }
            set
            {
                base.chartObjAttributes.FillColor = value;
                base.chartObjAttributes.PrimaryColor = value;
            }
        }

        public int GradientDirection
        {
            get
            {
                return this.gradientDirection;
            }
            set
            {
                this.gradientDirection = value;
            }
        }

        public Color GradientStartColor
        {
            get
            {
                return this.gradientStartColor;
            }
            set
            {
                this.gradientStartColor = value;
            }
        }

        public Color GradientStopColor
        {
            get
            {
                return this.gradientStopColor;
            }
            set
            {
                this.gradientStopColor = value;
            }
        }
    }
}

