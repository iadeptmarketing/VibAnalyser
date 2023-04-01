namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class UserCoordinates : ChartObj
    {
        internal bool altTextMeasure = false;
        internal Rectangle2D clippingBounds = new Rectangle2D(0.0, 0.0, 100.0, 100.0);
        internal ChartAttribute currentAttributes = new ChartAttribute();
        internal Font currentFont = GraphObj.GetDefaultChartFont();
        internal FontFamily currentFontFamily = null;
        private Graphics graphicsInstance = null;
        private GraphicsPath localpath = new GraphicsPath();
        internal Point2D userCurrentPos = new Point2D();
        internal Rectangle2D userViewport = new Rectangle2D(0.0, 0.0, 100.0, 100.0);

        public UserCoordinates()
        {
            this.InitDefaults();
        }

        public static float CalcAscent(Font font, Graphics g2)
        {
            float num = 0f;
            if (font != null)
            {
                num = font.Size * (font.FontFamily.GetCellAscent(font.Style) / font.FontFamily.GetCellAscent(font.Style));
            }
            return num;
        }

        public static float CalcDescent(Font font, Graphics g2)
        {
            float num = 0f;
            if (font != null)
            {
                float size = font.Size;
                float cellDescent = font.FontFamily.GetCellDescent(font.Style);
                float emHeight = font.FontFamily.GetEmHeight(font.Style);
                num = (cellDescent / emHeight) * size;
            }
            return num;
        }

        public static float CalcLineSpacing(Font font, Graphics g2)
        {
            float num = 0f;
            if (font != null)
            {
                num = font.Size * (font.FontFamily.GetLineSpacing(font.Style) / font.FontFamily.GetEmHeight(font.Style));
            }
            return num;
        }

        public override object Clone()
        {
            UserCoordinates coordinates = new UserCoordinates();
            coordinates.Copy(this);
            return coordinates;
        }

        public void Copy(UserCoordinates source)
        {
            if (source != null)
            {
                this.userViewport.SetFrame(source.userViewport);
                this.userCurrentPos.SetLocation(source.userCurrentPos);
                if (source.currentAttributes != null)
                {
                    this.currentAttributes = (ChartAttribute) source.currentAttributes.Clone();
                }
                this.graphicsInstance = source.graphicsInstance;
                if (source.CurrentFont != null)
                {
                    this.currentFont = (Font) source.currentFont.Clone();
                }
                if (source.CurrentFont != null)
                {
                    this.currentFontFamily = source.currentFont.FontFamily;
                }
                this.clippingBounds.SetFrame(source.clippingBounds);
            }
        }

        public void DrawFillCircle(Graphics g2, int x, int y, int radiusx, int radiusy)
        {
            int num = x - radiusx;
            int num2 = y - radiusy;
            if (this.currentAttributes.GetFillFlag())
            {
                g2.FillEllipse(this.currentAttributes.GetCurrentBrush(), num, num2, radiusx * 2, radiusx * 2);
            }
            if (this.currentAttributes.GetLineFlag())
            {
                g2.DrawEllipse(this.currentAttributes.GetCurrentPen(), num, num2, radiusy * 2, radiusy * 2);
            }
        }

        public void DrawFillPath(Graphics g2, GraphicsPath path)
        {
            if (((g2 != null) && (path != null)) && (path.PointCount >= 2))
            {
                if (this.currentAttributes.GetFillFlag())
                {
                    Brush currentBrush = this.currentAttributes.GetCurrentBrush();
                    this.FillPath(g2, currentBrush, path);
                }
                if (this.currentAttributes.GetLineFlag())
                {
                    Pen currentPen = this.currentAttributes.GetCurrentPen();
                    this.DrawPath(g2, currentPen, path);
                }
            }
        }

        public void DrawFillRectangle(Graphics g2, Rectangle2D rect)
        {
            if (this.currentAttributes.GetFillFlag())
            {
                Brush currentBrush = this.currentAttributes.GetCurrentBrush();
                g2.FillRectangle(currentBrush, rect.GetRectangle());
            }
            if (this.currentAttributes.GetLineFlag())
            {
                Pen currentPen = this.currentAttributes.GetCurrentPen();
                g2.DrawRectangle(currentPen, rect.GetRectangle());
            }
        }

        public void DrawPath(Graphics g2, Pen drawpen, GraphicsPath path)
        {
            if (((g2 != null) && (path != null)) && (path.PointCount >= 2))
            {
                if (drawpen.Width < 1.0)
                {
                    drawpen.ScaleTransform(drawpen.Width, drawpen.Width);
                    g2.DrawPath(drawpen, path);
                    drawpen.ScaleTransform(1f, 1f);
                }
                else
                {
                    g2.DrawPath(drawpen, path);
                }
            }
        }

        public void DrawReversibleFrame(Rectangle rect, Color framecolor, FrameStyle frstyle)
        {
            ControlPaint.DrawReversibleFrame(rect, framecolor, frstyle);
        }

        public void DrawReversibleLine(Point p1, Point p2, Color linecolor)
        {
            ControlPaint.DrawReversibleLine(p1, p2, linecolor);
        }

        public void EndXORMode(ChartView canvas)
        {
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && ((this.userViewport.GetWidth() <= 5.0) || (this.userViewport.GetHeight() <= 5.0)))
            {
                nerror = 0x33;
                ChartSupport.FixCommonRangeError(this.userViewport, 0.0, 100.0);
            }
            return base.ErrorCheck(nerror);
        }

        public void FillPath(Graphics g2, Brush drawbrush, GraphicsPath path)
        {
            if (((g2 != null) && (path != null)) && (path.PointCount >= 2))
            {
                g2.FillPath(drawbrush, path);
            }
        }

        public Rectangle2D GetClippingBounds()
        {
            return this.clippingBounds;
        }

        public Rectangle2D GetClipRect()
        {
            Rectangle2D rectangled = new Rectangle2D();
            if (this.graphicsInstance != null)
            {
                rectangled = new Rectangle2D(this.graphicsInstance.ClipBounds);
            }
            return rectangled;
        }

        public Graphics GetContext()
        {
            return this.graphicsInstance;
        }

        public ChartAttribute GetCurrentAttributes()
        {
            return this.currentAttributes;
        }

        public Color GetCurrentColor()
        {
            return this.currentAttributes.GetPrimaryColor();
        }

        public Font GetCurrentFont()
        {
            return this.currentFont;
        }

        public double GetStringAscent(Graphics g2, string s)
        {
            double num = 0.0;
            if (this.currentFont != null)
            {
                num = CalcAscent(this.currentFont, g2);
            }
            return num;
        }

        public double GetStringDescent(Graphics g2, string s)
        {
            double num = 0.0;
            if (this.currentFont != null)
            {
                num = CalcDescent(this.currentFont, g2);
            }
            return num;
        }

        public Dimension GetStringDimension(Graphics g2, string s)
        {
            Dimension dimension = new Dimension();
            if (g2 != null)
            {
                Font currentFont = this.GetCurrentFont();
                if (currentFont == null)
                {
                    return dimension;
                }
                if (s.Length == 0)
                {
                    SizeF ef = g2.MeasureString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", currentFont);
                    dimension.Width = ((double) ef.Width) / 26.0;
                    dimension.Height = ef.Height;
                    return dimension;
                }
                SizeF ef2 = g2.MeasureString(s, currentFont);
                dimension.Width = ef2.Width;
                dimension.Height = ef2.Height;
            }
            return dimension;
        }

        public double GetStringLeading(Graphics g2, string s)
        {
            double stringLineSpace = this.GetStringLineSpace(g2, s);
            double stringAscent = this.GetStringAscent(g2, s);
            double stringDescent = this.GetStringDescent(g2, s);
            return (stringLineSpace - (stringAscent + stringDescent));
        }

        public double GetStringLineSpace(Graphics g2, string s)
        {
            if (this.currentFont != null)
            {
                CalcLineSpacing(this.currentFont, g2);
            }
            return 0.0;
        }

        public double GetStringX(Graphics g2, string s)
        {
            double num = 0.0;
            if (g2 == null)
            {
                return num;
            }
            Font currentFont = this.GetCurrentFont();
            if (currentFont == null)
            {
                return num;
            }
            if (s.Length == 0)
            {
                if (this.altTextMeasure)
                {
                    return (GetStringXEx(g2, currentFont, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") / 26.0);
                }
                return (((double) g2.MeasureString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", currentFont).Width) / 26.0);
            }
            if (this.altTextMeasure)
            {
                return GetStringXEx(g2, currentFont, s);
            }
            return g2.MeasureString(s, currentFont).Width;
        }

        private static double GetStringXEx(Graphics g2, Font font, string text)
        {
            double num = 0.0;
            if (font != null)
            {
                StringFormat stringFormat = new StringFormat();
                RectangleF layoutRect = new RectangleF(0f, 0f, 1000f, 1000f);
                CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(0, text.Length) };
                Region[] regionArray = new Region[1];
                stringFormat.SetMeasurableCharacterRanges(ranges);
                num = g2.MeasureCharacterRanges(text, font, layoutRect, stringFormat)[0].GetBounds(g2).Right + 0f;
            }
            return num;
        }

        public double GetStringY(Graphics g2, string s)
        {
            double num = 0.0;
            if (g2 == null)
            {
                return num;
            }
            Font currentFont = this.GetCurrentFont();
            if (currentFont == null)
            {
                return num;
            }
            if (s.Length == 0)
            {
                return (double) CalcLineSpacing(currentFont, g2);
            }
            return g2.MeasureString(s, currentFont).Height;
        }

        public Point2D GetUserCurrentPos()
        {
            return (Point2D) this.userCurrentPos.Clone();
        }

        public Rectangle2D GetUserViewport()
        {
            return (Rectangle2D) this.userViewport.Clone();
        }

        public double GetUserX1()
        {
            return this.userViewport.GetX1();
        }

        public double GetUserX2()
        {
            return this.userViewport.GetX2();
        }

        public double GetUserY1()
        {
            return this.userViewport.GetY1();
        }

        public double GetUserY2()
        {
            return this.userViewport.GetY2();
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x49c;
        }

        public void PLineAbs(GraphicsPath path, double x1, double y1, double x2, double y2)
        {
            x1 = ChartSupport.ClampToViewCoordinates(x1);
            y1 = ChartSupport.ClampToViewCoordinates(y1);
            x2 = ChartSupport.ClampToViewCoordinates(x2);
            y2 = ChartSupport.ClampToViewCoordinates(y2);
            this.localpath.AddLine((float) x1, (float) y1, (float) x2, (float) y2);
            path.AddPath(this.localpath, false);
            this.localpath.Reset();
            this.userCurrentPos.SetLocation(x2, y2);
        }

        public void PLineToAbs(GraphicsPath path, double x, double y)
        {
            x = ChartSupport.ClampToViewCoordinates(x);
            y = ChartSupport.ClampToViewCoordinates(y);
            path.AddLine((float) this.userCurrentPos.GetX(), (float) this.userCurrentPos.GetY(), (float) x, (float) y);
            this.userCurrentPos.SetLocation(x, y);
        }

        public void PMoveRel(GraphicsPath path, double x, double y)
        {
            x = ChartSupport.ClampToViewCoordinates(x);
            y = ChartSupport.ClampToViewCoordinates(y);
            this.userCurrentPos.SetLocation((double) (this.userCurrentPos.GetX() + x), this.userCurrentPos.GetY() + y);
        }

        public void PMoveToAbs(GraphicsPath path, double x, double y)
        {
            x = ChartSupport.ClampToViewCoordinates(x);
            y = ChartSupport.ClampToViewCoordinates(y);
            this.userCurrentPos.SetLocation(x, y);
        }

        public void PPolyLine(GraphicsPath path, Point2D[] p, int numdat)
        {
            this.PMoveToAbs(path, p[0].GetX(), p[0].GetY());
            for (int i = 1; i < numdat; i++)
            {
                this.PLineToAbs(path, p[i].GetX(), p[i].GetY());
            }
        }

        public void PRectangle(GraphicsPath path, Rectangle2D rect)
        {
            double xx = ChartSupport.ClampToViewCoordinates(rect.GetX());
            double yy = ChartSupport.ClampToViewCoordinates(rect.GetY());
            double ww = ChartSupport.ClampToViewCoordinates(rect.GetWidth());
            double hh = ChartSupport.ClampToViewCoordinates(rect.GetHeight());
            path.AddRectangle(new Rectangle2D(xx, yy, ww, hh).GetRectangleF());
        }

        public void SetClippingBounds(Rectangle2D cliprect)
        {
            this.clippingBounds.SetFrame(cliprect);
        }

        public void SetClippingBounds(int x, int y, int w, int h)
        {
            this.clippingBounds.SetFrame((double) x, (double) y, (double) w, (double) h);
        }

        public void SetClipRect(Rectangle2D cliprect)
        {
            if (this.graphicsInstance != null)
            {
                this.graphicsInstance.Clip = new Region(cliprect.GetRectangle());
            }
        }

        public void SetClipRect(double x, double y, double w, double h)
        {
            Rectangle2D rectangled = new Rectangle2D(x, y, w + 0.999, h + 0.999);
            if (this.graphicsInstance != null)
            {
                this.graphicsInstance.Clip = new Region(rectangled.GetRectangle());
            }
        }

        public void SetContext(Graphics g2)
        {
            this.graphicsInstance = g2;
        }

        public void SetCurrentAttributes(ChartAttribute attrib)
        {
            this.currentAttributes.Copy(attrib);
        }

        public void SetCurrentColor(Color rgbcolor)
        {
            this.currentAttributes.SetPrimaryColor(rgbcolor);
        }

        public void SetCurrentFont(Font font)
        {
            if (font != null)
            {
                this.currentFont = font;
                this.currentFontFamily = this.currentFont.FontFamily;
            }
        }

        public void SetUserViewport(Rectangle2D rect)
        {
            this.userViewport.SetFrame(rect);
        }

        public void SetUserViewport(double x, double y, double w, double h)
        {
            this.userViewport.SetFrame(x, y, w, h);
        }

        public void StartXORMode(ChartView canvas, Color framecolor, int linestyle)
        {
        }

        public ChartAttribute CurrentAttributes
        {
            get
            {
                return this.GetCurrentAttributes();
            }
            set
            {
                this.SetCurrentAttributes(value);
            }
        }

        public Color CurrentColor
        {
            get
            {
                return this.GetCurrentColor();
            }
            set
            {
                this.SetCurrentColor(value);
            }
        }

        public Font CurrentFont
        {
            get
            {
                return this.GetCurrentFont();
            }
            set
            {
                this.SetCurrentFont(value);
            }
        }

        public Rectangle2D UserViewport
        {
            get
            {
                return this.userViewport;
            }
            set
            {
                this.SetUserViewport(value);
            }
        }
    }
}

