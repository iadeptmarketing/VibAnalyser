namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ChartText : GraphObj
    {
        internal double lineLeading;
        internal int originalFontSize;
        internal Font resizedTextFont;
        internal Rectangle2D textBox;
        internal Color textBoxColor;
        internal bool textBoxMode;
        internal Font textFont;
        internal Point2D textNudge;
        internal double textRotation;
        internal string textString;
        internal int xJust;
        internal int yJust;

        public ChartText()
        {
            this.textString = string.Copy("");
            this.textFont = GraphObj.defaultChartFont;
            this.resizedTextFont = null;
            this.textBoxMode = false;
            this.textBoxColor = Color.Black;
            this.xJust = 0;
            this.yJust = 0;
            this.textRotation = 0.0;
            this.textBox = new Rectangle2D();
            this.lineLeading = 0.0;
            this.textNudge = new Point2D();
            this.originalFontSize = 10;
            this.InitDefaults();
        }

        public ChartText(PhysicalCoordinates transform)
        {
            this.textString = string.Copy("");
            this.textFont = GraphObj.defaultChartFont;
            this.resizedTextFont = null;
            this.textBoxMode = false;
            this.textBoxColor = Color.Black;
            this.xJust = 0;
            this.yJust = 0;
            this.textRotation = 0.0;
            this.textBox = new Rectangle2D();
            this.lineLeading = 0.0;
            this.textNudge = new Point2D();
            this.originalFontSize = 10;
            this.SetChartObjScale(transform);
            this.InitDefaults();
        }

        public ChartText(Font tfont, string tstring)
        {
            this.textString = string.Copy("");
            this.textFont = GraphObj.defaultChartFont;
            this.resizedTextFont = null;
            this.textBoxMode = false;
            this.textBoxColor = Color.Black;
            this.xJust = 0;
            this.yJust = 0;
            this.textRotation = 0.0;
            this.textBox = new Rectangle2D();
            this.lineLeading = 0.0;
            this.textNudge = new Point2D();
            this.originalFontSize = 10;
            this.InitDefaults();
            this.InitChartText(null, tfont, tstring, 0.0, 0.0, 1, 0, 0, 0.0);
        }

        public ChartText(PhysicalCoordinates transform, Font tfont, string tstring)
        {
            this.textString = string.Copy("");
            this.textFont = GraphObj.defaultChartFont;
            this.resizedTextFont = null;
            this.textBoxMode = false;
            this.textBoxColor = Color.Black;
            this.xJust = 0;
            this.yJust = 0;
            this.textRotation = 0.0;
            this.textBox = new Rectangle2D();
            this.lineLeading = 0.0;
            this.textNudge = new Point2D();
            this.originalFontSize = 10;
            this.InitDefaults();
            this.InitChartText(transform, tfont, tstring, 0.0, 0.0, 1, 0, 0, 0.0);
        }

        public ChartText(PhysicalCoordinates transform, Font tfont, string tstring, double x, double y, int npostype)
        {
            this.textString = string.Copy("");
            this.textFont = GraphObj.defaultChartFont;
            this.resizedTextFont = null;
            this.textBoxMode = false;
            this.textBoxColor = Color.Black;
            this.xJust = 0;
            this.yJust = 0;
            this.textRotation = 0.0;
            this.textBox = new Rectangle2D();
            this.lineLeading = 0.0;
            this.textNudge = new Point2D();
            this.originalFontSize = 10;
            this.InitDefaults();
            this.InitChartText(transform, tfont, tstring, x, y, npostype, 0, 0, 0.0);
        }

        public ChartText(PhysicalCoordinates transform, Font tfont, string tstring, double x, double y, int npostype, int xjust, int yjust, int rotation)
        {
            this.textString = string.Copy("");
            this.textFont = GraphObj.defaultChartFont;
            this.resizedTextFont = null;
            this.textBoxMode = false;
            this.textBoxColor = Color.Black;
            this.xJust = 0;
            this.yJust = 0;
            this.textRotation = 0.0;
            this.textBox = new Rectangle2D();
            this.lineLeading = 0.0;
            this.textNudge = new Point2D();
            this.originalFontSize = 10;
            this.InitDefaults();
            this.InitChartText(transform, tfont, tstring, x, y, npostype, xjust, yjust, (double) rotation);
        }

        public void AddNewLineTextString(string thestring)
        {
            if (thestring == null)
            {
                thestring = "";
            }
            if (thestring.Length == 0)
            {
                this.textString = this.textString + "\n";
            }
            else
            {
                this.textString = this.textString + "\n" + thestring;
            }
        }

        private Rectangle2D CalcTextBox(Graphics g2, Point2D p, string s)
        {
            double maxMultilineStringWidth;
            double height;
            Rectangle2D rectangled = new Rectangle2D();
            int num4 = Math.Max(1, this.GetNumLines(s));
            if (num4 <= 1)
            {
                Dimension stringDimension = base.chartObjScale.GetStringDimension(g2, s);
                maxMultilineStringWidth = stringDimension.Width;
                height = stringDimension.Height;
                double num5 = p.GetX() + (this.textNudge.X * base.resizeMultiplier);
                double num6 = p.GetY() + (this.textNudge.Y * base.resizeMultiplier);
                double num7 = height;
                double num8 = maxMultilineStringWidth;
                rectangled.SetFrame(num5, num6, num8, num7);
                return rectangled;
            }
            maxMultilineStringWidth = this.GetMaxMultilineStringWidth(g2, s);
            height = base.chartObjScale.GetStringY(g2, this.GetMultilineSubstring(s, 0)) * num4;
            double xx = p.GetX() + (this.textNudge.X * base.resizeMultiplier);
            double yy = p.GetY() + (this.textNudge.Y * base.resizeMultiplier);
            double hh = height;
            double ww = maxMultilineStringWidth;
            rectangled.SetFrame(xx, yy, ww, hh);
            return rectangled;
        }

        protected Point2D CalcTextJust(Graphics g2, Point2D p, string s)
        {
            double px = 0.0;
            double py = 0.0;
            Dimension textDimension = this.GetTextDimension(g2, s);
            Point2D pointd = new Point2D();
            px = p.GetX();
            py = p.GetY();
            switch (this.xJust)
            {
                case 1:
                    px -= textDimension.Width / 2.0;
                    break;

                case 2:
                    px -= textDimension.Width;
                    break;
            }
            switch (this.yJust)
            {
                case 0:
                    py -= textDimension.Height;
                    break;

                case 1:
                    if (this.GetNumLines(s) > 1)
                    {
                        py -= textDimension.Height / 2.0;
                        break;
                    }
                    py -= textDimension.Height / 2.0;
                    break;
            }
            pointd.SetLocation(px, py);
            this.textBox = new Rectangle2D(px, py, textDimension.Width, textDimension.Height);
            return pointd;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            ChartText text = new ChartText();
            text.Copy(this);
            return text;
        }

        public void Copy(ChartText source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.textString = string.Copy(source.textString);
                this.textFont = source.textFont;
                this.resizedTextFont = source.resizedTextFont;
                this.xJust = source.xJust;
                this.yJust = source.yJust;
                this.textRotation = source.textRotation;
                this.lineLeading = source.lineLeading;
                this.textNudge = (Point2D) source.textNudge.Clone();
                this.originalFontSize = source.originalFontSize;
                this.textBoxMode = source.textBoxMode;
                this.textBoxColor = source.textBoxColor;
                if (source.textBox != null)
                {
                    this.textBox = (Rectangle2D) source.textBox.Clone();
                }
            }
        }

        public Font DeriveFont(Font original, int newsize)
        {
            return new Font(original.FontFamily, (float) newsize, original.Style);
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawText(g2);
            }
        }

        public void DrawText(Graphics g2)
        {
            string s = string.Copy(this.textString);
            Point2D p = null;
            Point2D location = null;
            double num = -this.textRotation;
            s.Trim();
            location = this.GetLocation(0);
            this.SetResizedTextFont();
            p = this.CalcTextJust(g2, location, s);
            Matrix transform = g2.Transform;
            if (this.textRotation != 0.0)
            {
                Matrix matrix = new Matrix();
                matrix.RotateAt((float) num, new PointF((float) location.GetX(), (float) location.GetY()));
                Matrix matrix2 = g2.Transform;
                matrix2.Multiply(matrix);
                g2.Transform = matrix2;
            }
            this.textBox = this.CalcTextBox(g2, p, s);
            base.boundingBox.Reset();
            base.boundingBox.AddRectangle(this.textBox.GetRectangle());
            if (this.GetChartObjEnable() == 1)
            {
                if (base.chartObjAttributes.GetFillFlag())
                {
                    Brush currentBrush = base.chartObjAttributes.GetCurrentBrush();
                    g2.FillRectangle(currentBrush, this.textBox.GetRectangle());
                }
                if (this.GetTextBoxMode())
                {
                    Pen pen = ChartAttribute.GetCachedPen(this.textBoxColor, 1, DashStyle.Solid);
                    g2.DrawRectangle(pen, this.textBox.GetRectangle());
                }
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                int numLines = this.GetNumLines(s);
                int num3 = ((int) p.GetX()) + ((int) (this.textNudge.X * base.resizeMultiplier));
                int num4 = ((int) p.GetY()) + ((int) (this.textNudge.Y * base.resizeMultiplier));
                int textMaxSizeY = (int) this.GetTextMaxSizeY(g2, 0);
                SolidBrush cachedBrush = (SolidBrush) ChartAttribute.GetCachedBrush(base.chartObjAttributes.PrimaryColor);
                if (numLines <= 1)
                {
                    g2.DrawString(s, this.resizedTextFont, cachedBrush, (float) num3, (float) num4);
                }
                else
                {
                    for (int i = 0; i < numLines; i++)
                    {
                        string multilineSubstring = this.GetMultilineSubstring(s, i);
                        num3 = ((int) this.CalcTextJust(g2, location, multilineSubstring).GetX()) + ((int) (this.textNudge.X * base.resizeMultiplier));
                        g2.DrawString(multilineSubstring, this.resizedTextFont, cachedBrush, (float) num3, (float) num4);
                        num4 += textMaxSizeY;
                    }
                }
            }
            g2.Transform = transform;
        }

        protected void DrawTextBox(Graphics g2)
        {
            g2.DrawRectangle(base.chartObjAttributes.GetCurrentPen(), (int) this.textBox.GetX(), (int) this.textBox.GetY(), (int) this.textBox.GetWidth(), (int) this.textBox.GetHeight());
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.textFont == null))
            {
                nerror = 200;
            }
            return base.ErrorCheck(nerror);
        }

        public double GetLineLeading()
        {
            return this.lineLeading;
        }

        protected double GetMaxMultilineStringWidth(Graphics g2, string s)
        {
            double num3 = 0.0;
            if (s.Length > 0)
            {
                int numLines = this.GetNumLines(s);
                for (int i = 0; i < numLines; i++)
                {
                    string multilineSubstring = this.GetMultilineSubstring(s, i);
                    double stringX = base.chartObjScale.GetStringX(g2, multilineSubstring);
                    if (stringX > num3)
                    {
                        num3 = stringX;
                    }
                }
            }
            return num3;
        }

        protected string GetMultilineSubstring(string s, int line)
        {
            string str = string.Copy("");
            int num3 = 0;
            int startIndex = -1;
            int num5 = -1;
            int numLines = this.GetNumLines(s);
            int length = s.Length;
            if (line >= numLines)
            {
                return string.Copy("");
            }
            if ((line == 0) && (numLines == 1))
            {
                return string.Copy(s);
            }
            for (int i = 0; i < length; i++)
            {
                if (s[i] == '\n')
                {
                    num3++;
                    if ((line == 0) && (num3 == 1))
                    {
                        startIndex = 0;
                        num5 = i;
                    }
                    else
                    {
                        if (num3 == line)
                        {
                            startIndex = i + 1;
                        }
                        if (num3 != (line + 1))
                        {
                            continue;
                        }
                        num5 = i;
                    }
                    break;
                }
                if (i == (length - 1))
                {
                    num5 = i + 1;
                    break;
                }
            }
            if ((startIndex >= 0) && (num5 >= startIndex))
            {
                str = s.Substring(startIndex, num5 - startIndex);
            }
            return str;
        }

        public int GetNumLines(string s)
        {
            int length = 0;
            int num2 = 1;
            length = s.Length;
            for (int i = 0; i < length; i++)
            {
                if (s[i] == '\n')
                {
                    num2++;
                }
            }
            return num2;
        }

        public Font GetResizedTextFont()
        {
            Font textFont = this.textFont;
            float originalFontSize = this.originalFontSize;
            if (base.resizeMultiplier == 1.0)
            {
                return textFont;
            }
            originalFontSize *= (float) base.resizeMultiplier;
            originalFontSize = Math.Max(1f, (float) Math.Floor((double) (originalFontSize + 0.01f)));
            if (originalFontSize == this.originalFontSize)
            {
                return textFont;
            }
            if (this.resizedTextFont == null)
            {
                this.resizedTextFont = this.DeriveFont(this.textFont, (int) originalFontSize);
                return this.resizedTextFont;
            }
            if (originalFontSize != this.resizedTextFont.SizeInPoints)
            {
                this.resizedTextFont = this.DeriveFont(this.textFont, (int) originalFontSize);
            }
            return this.resizedTextFont;
        }

        public Color GetTextBgColor()
        {
            return base.chartObjAttributes.GetFillColor();
        }

        public bool GetTextBgMode()
        {
            return base.chartObjAttributes.GetFillFlag();
        }

        public Rectangle2D GetTextBox()
        {
            return this.textBox;
        }

        public Color GetTextBoxColor()
        {
            return this.textBoxColor;
        }

        public bool GetTextBoxMode()
        {
            return this.textBoxMode;
        }

        public Dimension GetTextDimension(Graphics g2, string s)
        {
            Dimension stringDimension = base.chartObjScale.GetStringDimension(g2, s);
            stringDimension.Width += 2.0;
            stringDimension.Height++;
            return stringDimension;
        }

        public Font GetTextFont()
        {
            return this.textFont;
        }

        public double GetTextMaxSizeY(Graphics g2, int npostype)
        {
            return base.chartObjScale.GetStringY(g2, "G", npostype);
        }

        public Point2D GetTextNudge()
        {
            return (Point2D) this.textNudge.Clone();
        }

        public double GetTextRotation()
        {
            return this.textRotation;
        }

        public double GetTextSizeX(Graphics g2, int npostype)
        {
            return base.chartObjScale.GetStringX(g2, this.textString, npostype);
        }

        public double GetTextSizeY(Graphics g2, int npostype)
        {
            return base.chartObjScale.GetStringY(g2, this.textString, npostype);
        }

        public virtual string GetTextString()
        {
            return this.textString;
        }

        public int GetXJust()
        {
            return this.xJust;
        }

        public int GetYJust()
        {
            return this.yJust;
        }

        public void InitChartText(PhysicalCoordinates transform, Font tfont, string tstring, double x, double y, int npostype, int xjust, int yjust, double rotation)
        {
            this.SetChartObjScale(transform);
            this.textString = tstring;
            base.positionType = npostype;
            this.SetTextFont(tfont);
            this.SetLocation(x, y, npostype);
            this.xJust = xjust;
            this.yJust = yjust;
            this.textRotation = rotation;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x25d;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
            this.InitChartText(base.chartObjScale, this.textFont, "", 0.0, 0.0, base.positionType, 0, 0, 0.0);
        }

        public void PreCalcTextBoundingBox(Graphics g2)
        {
            string s = string.Copy(this.textString);
            Point2D p = null;
            Point2D location = null;
            double num = -this.textRotation;
            s.Trim();
            location = this.GetLocation(0);
            this.SetResizedTextFont();
            p = this.CalcTextJust(g2, location, s);
            Matrix transform = g2.Transform;
            if (this.textRotation != 0.0)
            {
                Matrix matrix = new Matrix();
                matrix.RotateAt((float) num, new PointF((float) location.GetX(), (float) location.GetY()));
                Matrix matrix2 = g2.Transform;
                matrix2.Multiply(matrix);
                g2.Transform = matrix2;
            }
            this.textBox = this.CalcTextBox(g2, p, s);
            base.boundingBox.Reset();
            base.boundingBox.AddRectangle(this.textBox.GetRectangle());
            g2.Transform = transform;
        }

        public void SetLineLeading(double rlead)
        {
            this.lineLeading = rlead;
        }

        public void SetResizedTextFont()
        {
            this.resizedTextFont = this.GetResizedTextFont();
            base.chartObjScale.SetCurrentFont(this.resizedTextFont);
        }

        public void SetTextBgColor(Color rgbcolor)
        {
            base.chartObjAttributes.SetFillColor(rgbcolor);
        }

        public void SetTextBgMode(bool bmode)
        {
            base.chartObjAttributes.SetFillFlag(bmode);
        }

        public void SetTextBoxColor(Color c)
        {
            this.textBoxColor = c;
        }

        public void SetTextBoxMode(bool bmode)
        {
            this.textBoxMode = bmode;
        }

        public void SetTextFont(Font tfont)
        {
            if (tfont != null)
            {
                this.textFont = tfont;
                this.originalFontSize = (int) this.textFont.SizeInPoints;
                this.resizedTextFont = this.GetResizedTextFont();
            }
        }

        public void SetTextNudge(Point2D nudge)
        {
            this.textNudge.SetLocation(nudge.GetX(), nudge.GetY());
        }

        public void SetTextNudge(double x, double y)
        {
            this.textNudge.SetLocation(x, y);
        }

        public void SetTextRotation(double rotation)
        {
            this.textRotation = rotation;
        }

        public void SetTextString(string thestring)
        {
            this.textString = null;
            this.textString = thestring;
        }

        public void SetXJust(int xjust)
        {
            this.xJust = xjust;
        }

        public void SetYJust(int yjust)
        {
            this.yJust = yjust;
        }

        public Color TextBgColor
        {
            get
            {
                return base.chartObjAttributes.GetFillColor();
            }
            set
            {
                base.chartObjAttributes.SetFillColor(value);
            }
        }

        public bool TextBgMode
        {
            get
            {
                return base.chartObjAttributes.GetFillFlag();
            }
            set
            {
                base.chartObjAttributes.SetFillFlag(value);
            }
        }

        public Color TextBoxColor
        {
            get
            {
                return this.textBoxColor;
            }
            set
            {
                this.textBoxColor = value;
            }
        }

        public bool TextBoxMode
        {
            get
            {
                return this.textBoxMode;
            }
            set
            {
                this.textBoxMode = value;
            }
        }

        public Font TextFont
        {
            get
            {
                return this.textFont;
            }
            set
            {
                this.SetTextFont(value);
            }
        }

        public Point2D TextNudge
        {
            get
            {
                return this.textNudge;
            }
            set
            {
                this.textNudge = value;
            }
        }

        public double TextRotation
        {
            get
            {
                return this.textRotation;
            }
            set
            {
                this.textRotation = value;
            }
        }

        public string TextString
        {
            get
            {
                return this.textString;
            }
            set
            {
                this.textString = value;
            }
        }

        public int XJust
        {
            get
            {
                return this.xJust;
            }
            set
            {
                this.xJust = value;
            }
        }

        public int YJust
        {
            get
            {
                return this.yJust;
            }
            set
            {
                this.yJust = value;
            }
        }
    }
}

