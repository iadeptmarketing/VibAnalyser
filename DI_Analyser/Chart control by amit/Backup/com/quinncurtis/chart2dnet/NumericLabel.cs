namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class NumericLabel : ChartLabel
    {
        internal int decimalPos;
        private int exponentAscent;
        private string exponentStringSign;
        private int exponentXAdjust;
        private string expStr;
        private string mantissaStr;
        internal int numericFormat;
        internal double numericValue;
        internal static string numStrFormatPostfix = string.Copy("KMBT");
        private string postfixString;
        private string prefixString;

        public NumericLabel()
        {
            this.numericValue = 0.0;
            this.numericFormat = 1;
            this.decimalPos = 0;
            this.mantissaStr = "";
            this.expStr = "";
            this.exponentAscent = 4;
            this.exponentXAdjust = -2;
            this.postfixString = "";
            this.prefixString = "";
            this.exponentStringSign = "";
            this.InitDefaults();
        }

        public NumericLabel(PhysicalCoordinates transform)
        {
            this.numericValue = 0.0;
            this.numericFormat = 1;
            this.decimalPos = 0;
            this.mantissaStr = "";
            this.expStr = "";
            this.exponentAscent = 4;
            this.exponentXAdjust = -2;
            this.postfixString = "";
            this.prefixString = "";
            this.exponentStringSign = "";
            this.SetChartObjScale(transform);
            this.InitDefaults();
        }

        public NumericLabel(int nnumformat, int ndecimal)
        {
            this.numericValue = 0.0;
            this.numericFormat = 1;
            this.decimalPos = 0;
            this.mantissaStr = "";
            this.expStr = "";
            this.exponentAscent = 4;
            this.exponentXAdjust = -2;
            this.postfixString = "";
            this.prefixString = "";
            this.exponentStringSign = "";
            this.InitDefaults();
            base.InitChartText(null, null, "", 0.0, 0.0, 1, 0, 0, 0.0);
            this.numericFormat = nnumformat;
            this.decimalPos = ndecimal;
        }

        public NumericLabel(Font tfont, int nnumformat, int ndecimal)
        {
            this.numericValue = 0.0;
            this.numericFormat = 1;
            this.decimalPos = 0;
            this.mantissaStr = "";
            this.expStr = "";
            this.exponentAscent = 4;
            this.exponentXAdjust = -2;
            this.postfixString = "";
            this.prefixString = "";
            this.exponentStringSign = "";
            this.InitDefaults();
            base.InitChartText(null, tfont, "", 0.0, 0.0, 1, 0, 0, 0.0);
            this.numericFormat = nnumformat;
            this.decimalPos = ndecimal;
        }

        public NumericLabel(PhysicalCoordinates transform, Font tfont, double initialvalue1, double x, double y, int npostype, int nnumformat, int ndecimal)
        {
            this.numericValue = 0.0;
            this.numericFormat = 1;
            this.decimalPos = 0;
            this.mantissaStr = "";
            this.expStr = "";
            this.exponentAscent = 4;
            this.exponentXAdjust = -2;
            this.postfixString = "";
            this.prefixString = "";
            this.exponentStringSign = "";
            this.InitDefaults();
            base.InitChartText(transform, tfont, "", x, y, npostype, 0, 0, 0.0);
            this.numericValue = initialvalue1;
            this.numericFormat = nnumformat;
            this.decimalPos = ndecimal;
        }

        public NumericLabel(PhysicalCoordinates transform, Font tfont, double initialvalue1, double x, double y, int npostype, int nnumformat, int ndecimal, int xjust, int yjust, double rotation)
        {
            this.numericValue = 0.0;
            this.numericFormat = 1;
            this.decimalPos = 0;
            this.mantissaStr = "";
            this.expStr = "";
            this.exponentAscent = 4;
            this.exponentXAdjust = -2;
            this.postfixString = "";
            this.prefixString = "";
            this.exponentStringSign = "";
            this.InitDefaults();
            base.InitChartText(transform, tfont, "", x, y, npostype, xjust, yjust, rotation);
            this.numericValue = initialvalue1;
            this.numericFormat = nnumformat;
            this.decimalPos = ndecimal;
        }

        public override object Clone()
        {
            NumericLabel label = new NumericLabel();
            label.Copy(this);
            return label;
        }

        public void Copy(NumericLabel source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.numericValue = source.numericValue;
                this.numericFormat = source.numericFormat;
                this.decimalPos = source.decimalPos;
                this.postfixString = source.postfixString;
                this.prefixString = source.prefixString;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.MakeNumericLabel();
                if (this.numericFormat == 6)
                {
                    this.DrawWithExponent(g2);
                }
                else
                {
                    base.Draw(g2);
                }
            }
        }

        private void DrawWithExponent(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                string s = string.Copy(this.mantissaStr);
                Point2D pointd = null;
                Point2D p = null;
                ChartSupport.ToRadians(-base.textRotation);
                s.Trim();
                p = this.GetLocation(0);
                base.SetResizedTextFont();
                base.GetResizedTextFont();
                Brush currentBrush = base.chartObjAttributes.GetCurrentBrush();
                pointd = base.CalcTextJust(g2, p, s);
                if (this.GetChartObjEnable() == 1)
                {
                    base.GetNumLines(s);
                    int x = (int) pointd.GetX();
                    int y = (int) pointd.GetY();
                    base.GetTextMaxSizeY(g2, 0);
                    s = s + "x10";
                    base.textString = s;
                    Point2D pointd3 = new Point2D(base.TextNudge);
                    double num4 = (base.ChartObjScale.GetStringX(g2, this.expStr, 0) * 3.0) / 4.0;
                    if (base.XJust == 2)
                    {
                        Point2D textNudge = base.TextNudge;
                        textNudge.X -= num4;
                    }
                    base.DrawText(g2);
                    double num = (3.0 * base.resizedTextFont.Size) / 4.0;
                    Rectangle2D textBox = base.GetTextBox();
                    double width = textBox.GetWidth();
                    x = (int) textBox.GetX();
                    y = (int) textBox.GetY();
                    Font font = new Font(base.resizedTextFont.FontFamily, (float) num, base.resizedTextFont.Style);
                    if (this.expStr[0] == '0')
                    {
                        this.expStr = this.expStr.Substring(1);
                    }
                    this.expStr = this.exponentStringSign + this.expStr;
                    base.chartObjScale.SetCurrentFont(font);
                    g2.DrawString(this.expStr, font, currentBrush, (float) ((x + ((int) width)) + this.exponentXAdjust), (float) (y - this.exponentAscent));
                    base.TextNudge.SetLocation(pointd3.X, pointd3.Y);
                }
                base.chartObjScale.SetCurrentFont(base.resizedTextFont);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public int GetDecimalPos()
        {
            return this.decimalPos;
        }

        public int GetNumericFormat()
        {
            return this.numericFormat;
        }

        public double GetNumericValue()
        {
            return this.numericValue;
        }

        public static string GetNumStrFormatPostfix()
        {
            return numStrFormatPostfix;
        }

        public override string GetTextString()
        {
            this.MakeNumericLabel();
            return base.GetTextString();
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x25e;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
        }

        public override void MakeLabel()
        {
            this.MakeNumericLabel();
        }

        private void MakeNumericLabel()
        {
            string thestring = "";
            int decimalPos = this.decimalPos;
            if ((this.numericFormat >= 1) && (this.numericFormat <= 12))
            {
                if (this.numericFormat == 6)
                {
                    string str2 = ChartSupport.NumToString(this.numericValue, 6, decimalPos, numStrFormatPostfix);
                    int index = str2.IndexOf('E');
                    this.expStr = str2.Substring(index + 3, 2);
                    if (Math.Abs(this.numericValue) < 0.99999)
                    {
                        this.exponentStringSign = "-";
                    }
                    else
                    {
                        this.exponentStringSign = "";
                    }
                    this.mantissaStr = str2.Substring(0, index);
                    thestring = this.prefixString + this.mantissaStr + "x10" + this.exponentStringSign + this.expStr + this.postfixString;
                    base.SetTextString(thestring);
                }
                else
                {
                    switch (decimalPos)
                    {
                        case -99:
                            if (this.numericValue >= 1.0)
                            {
                                decimalPos = 0;
                            }
                            else
                            {
                                decimalPos = -((int) Math.Floor(ChartSupport.Log10Ex(Math.Abs(this.numericValue))));
                            }
                            break;

                        case -98:
                            decimalPos = 0;
                            if (this.numericValue < 0.0999)
                            {
                                decimalPos = 1;
                            }
                            else if (this.numericValue > 0.9901)
                            {
                                decimalPos = 1;
                            }
                            break;
                    }
                    this.mantissaStr = ChartSupport.NumToString(this.numericValue, this.numericFormat, decimalPos, numStrFormatPostfix);
                    thestring = this.prefixString + this.mantissaStr + this.postfixString;
                    base.SetTextString(thestring);
                }
            }
        }

        public void SetDecimalPos(int ndecplace)
        {
            this.decimalPos = ndecplace;
        }

        public void SetNumericFormat(int nformat)
        {
            this.numericFormat = nformat;
        }

        public void SetNumericValue(double rvalue)
        {
            this.numericValue = rvalue;
        }

        public static void SetNumStrFormatPostfix(string bformat)
        {
            numStrFormatPostfix = bformat;
        }

        public int DecimalPos
        {
            get
            {
                return this.decimalPos;
            }
            set
            {
                this.decimalPos = value;
            }
        }

        public int NumericFormat
        {
            get
            {
                return this.numericFormat;
            }
            set
            {
                this.numericFormat = value;
            }
        }

        public double NumericValue
        {
            get
            {
                return this.numericValue;
            }
            set
            {
                this.numericValue = value;
            }
        }

        public string NumStrFormatPostfix
        {
            get
            {
                return numStrFormatPostfix;
            }
        }

        public string PostfixString
        {
            get
            {
                return this.postfixString;
            }
            set
            {
                this.postfixString = value;
            }
        }
    }
}

