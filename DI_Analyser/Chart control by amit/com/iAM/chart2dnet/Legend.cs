namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class Legend : GraphObj
    {
        internal bool autoSizeLegendRectangle = true;
        internal ChartText[] generalLegendText = new ChartText[3];
        internal double generalTextOffset = 0.0;
        internal double horizontalSpacing = 2.0;
        internal Rectangle2D innerLegendRectangle = new Rectangle2D(0.0, 0.0, 100.0, 100.0);
        internal double[] legendBorderRect = new double[] { 2.0, 2.0, 2.0, 2.0 };
        internal double legendHeight = 1.0;
        internal Color legendItemUniformTextColor = Color.Empty;
        internal Rectangle2D legendRectangle = new Rectangle2D(0.0, 0.0, 100.0, 100.0);
        internal double legendWidth = 1.0;
        internal double verticalSpacing = 1.0;

        public Legend()
        {
            this.InitDefaults();
        }

        public void AddLegendGeneralText(int ntextpos)
        {
            if ((ntextpos >= 0) && (ntextpos <= 2))
            {
                this.generalLegendText[ntextpos].SetTextString("");
            }
        }

        public void AddLegendGeneralText(int ntextpos, ChartText textobj)
        {
            if ((ntextpos >= 0) && (ntextpos <= 2))
            {
                this.generalLegendText[ntextpos] = (ChartText) textobj.Clone();
            }
        }

        public void AddLegendGeneralText(int ntextpos, string stext, Color rgbcolor, Font thefont)
        {
            if ((ntextpos >= 0) && (ntextpos <= 2))
            {
                this.generalLegendText[ntextpos] = new ChartText(base.chartObjScale, thefont, stext);
                if (this.generalLegendText[ntextpos] != null)
                {
                    this.generalLegendText[ntextpos].SetColor(rgbcolor);
                    this.generalLegendText[ntextpos].SetXJust(1);
                    this.generalLegendText[ntextpos].SetYJust(2);
                    this.generalLegendText[ntextpos].SetChartObjClipping(1);
                }
            }
        }

        protected virtual void CalcLegendPosition()
        {
            Point2D location = new Point2D();
            Point2D source = new Point2D();
            location = this.GetLocation(0);
            double px = this.GetLocation(3).GetX() + this.legendWidth;
            double py = this.GetLocation(3).GetY() + this.legendHeight;
            source.SetLocation(px, py);
            Point2D pointd3 = base.chartObjScale.ConvertCoord(0, source, 3);
            this.legendRectangle.SetFrameFromDiagonal(location.GetX(), location.GetY(), pointd3.GetX(), pointd3.GetY());
            this.innerLegendRectangle.SetFrameFromDiagonal(this.legendRectangle.GetX1() + this.legendBorderRect[0], this.legendRectangle.GetY1() + this.legendBorderRect[1], this.legendRectangle.GetX2() - this.legendBorderRect[2], this.legendRectangle.GetY2() - this.legendBorderRect[3]);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D rectangled = new Rectangle2D();
            bool flag = false;
            Point2D pointd = base.chartObjScale.ConvertCoord(3, testpoint, 0);
            rectangled.SetFrame(this.GetLocation().GetX(), this.GetLocation().GetY(), this.legendWidth, this.legendHeight);
            if ((rectangled != null) && rectangled.Contains(pointd.GetX(), pointd.GetY()))
            {
                flag = true;
            }
            return flag;
        }

        public void Copy(Legend source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.legendWidth = source.legendWidth;
                this.legendHeight = source.legendHeight;
                this.verticalSpacing = source.verticalSpacing;
                this.horizontalSpacing = source.horizontalSpacing;
                this.innerLegendRectangle = (Rectangle2D) source.innerLegendRectangle.Clone();
                this.legendRectangle = (Rectangle2D) source.legendRectangle.Clone();
                this.autoSizeLegendRectangle = source.autoSizeLegendRectangle;
                this.legendBorderRect[0] = source.legendBorderRect[0];
                this.legendBorderRect[1] = source.legendBorderRect[1];
                this.legendBorderRect[2] = source.legendBorderRect[2];
                this.legendBorderRect[3] = source.legendBorderRect[3];
                this.generalTextOffset = source.generalTextOffset;
                this.legendItemUniformTextColor = source.legendItemUniformTextColor;
                for (int i = 0; i < 2; i++)
                {
                    if (source.generalLegendText[i] != null)
                    {
                        this.generalLegendText[i] = (ChartText) source.generalLegendText[i].Clone();
                    }
                }
            }
        }

        public override void Draw(Graphics g2)
        {
        }

        protected void DrawLegendFooter(Graphics g2)
        {
            double textMaxSizeY = 0.0;
            double x = 0.0;
            double y = 0.0;
            this.generalTextOffset = this.innerLegendRectangle.GetY2();
            if (this.generalLegendText[2] != null)
            {
                textMaxSizeY = this.generalLegendText[2].GetTextMaxSizeY(g2, 0);
                this.generalLegendText[2].GetTextSizeX(g2, 0);
                x = this.innerLegendRectangle.GetX() + (this.innerLegendRectangle.GetWidth() / 2.0);
                y = this.innerLegendRectangle.GetY2() - textMaxSizeY;
                this.generalLegendText[2].SetLocation(x, y, 0);
                this.generalLegendText[2].SetChartObjEnable(this.GetChartObjEnable());
                this.generalLegendText[2].SetResizeMultiplier(base.resizeMultiplier);
                this.generalLegendText[2].Draw(g2);
                this.generalTextOffset = y;
            }
        }

        protected void DrawLegendHeaders(Graphics g2)
        {
            double textMaxSizeY = 0.0;
            double x = 0.0;
            double y = 0.0;
            this.generalTextOffset = this.innerLegendRectangle.GetY();
            if (this.generalLegendText[0] != null)
            {
                textMaxSizeY = this.generalLegendText[0].GetTextMaxSizeY(g2, 0);
                this.generalLegendText[0].GetTextSizeX(g2, 0);
                x = this.innerLegendRectangle.GetX() + (this.innerLegendRectangle.GetWidth() / 2.0);
                y = this.generalTextOffset;
                this.generalLegendText[0].SetLocation(x, y, 0);
                this.generalLegendText[0].SetChartObjEnable(this.GetChartObjEnable());
                this.generalLegendText[0].SetResizeMultiplier(base.resizeMultiplier);
                this.generalLegendText[0].Draw(g2);
                this.generalTextOffset += textMaxSizeY * this.verticalSpacing;
            }
            if (this.generalLegendText[1] != null)
            {
                textMaxSizeY = this.generalLegendText[1].GetTextMaxSizeY(g2, 0);
                this.generalLegendText[1].GetTextSizeX(g2, 0);
                x = this.innerLegendRectangle.GetX() + (this.innerLegendRectangle.GetWidth() / 2.0);
                y = this.generalTextOffset;
                this.generalLegendText[1].SetLocation(x, y, 0);
                this.generalLegendText[1].SetChartObjEnable(this.GetChartObjEnable());
                this.generalLegendText[1].SetResizeMultiplier(base.resizeMultiplier);
                this.generalLegendText[1].Draw(g2);
                this.generalTextOffset += textMaxSizeY * this.verticalSpacing;
            }
        }

        protected virtual void DrawLegendRect(Graphics g2)
        {
            base.chartObjScale.SetCurrentAttributes(this.GetChartObjAttributes());
            base.thePath = new GraphicsPath();
            base.chartObjScale.PRectangle(base.thePath, this.legendRectangle);
            base.boundingBox.Reset();
            base.boundingBox.AddPath(base.thePath, false);
            if (this.GetChartObjEnable() == 1)
            {
                base.chartObjScale.DrawFillPath(g2, base.thePath);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public bool GetAutoSizeLegendRectangle()
        {
            return this.autoSizeLegendRectangle;
        }

        public void GetAutoSizeLegendRectangle(bool autosize)
        {
            this.autoSizeLegendRectangle = autosize;
        }

        public double GetHorizontalSpacing()
        {
            return this.horizontalSpacing;
        }

        public Rectangle2D GetInnerLegendRectangle()
        {
            return this.innerLegendRectangle;
        }

        public double GetLegendBorderRect(int nborder)
        {
            double num = 0.0;
            if ((nborder >= 0) && (nborder <= 3))
            {
                num = this.legendBorderRect[nborder];
            }
            return num;
        }

        public ChartText GetLegendGeneralText(int item)
        {
            ChartText text = null;
            if ((item >= 0) && (item <= 2))
            {
                text = this.generalLegendText[item];
            }
            return text;
        }

        public double GetLegendHeight()
        {
            return this.legendHeight;
        }

        public double GetLegendWidth()
        {
            return this.legendWidth;
        }

        public Point2D GetSize()
        {
            return new Point2D(this.legendWidth, this.legendHeight);
        }

        public double GetVerticalSpacing()
        {
            return this.verticalSpacing;
        }

        private void InitDefaults()
        {
            base.chartObjType = 800;
            base.positionType = 3;
            base.moveableType = 1;
            base.chartObjClipping = 1;
            this.SetChartObjScale(new CartesianCoordinates());
            this.GetChartObjScale().SetGraphBorderDiagonal(0.0, 0.0, 1.0, 1.0);
            base.zOrder = 150;
        }

        public void InitLegendPosition(double rx, double ry, double rwidth, double rheight)
        {
            this.InitDefaults();
            this.SetLocation(rx, ry);
            this.legendWidth = rwidth;
            this.legendHeight = rheight;
        }

        public override void SetChartObjScale(PhysicalCoordinates transform)
        {
            base.SetChartObjScale(transform);
            for (int i = 0; i < 2; i++)
            {
                if (this.generalLegendText[i] != null)
                {
                    this.generalLegendText[i].SetChartObjScale(transform);
                }
            }
        }

        public void SetHorizontalSpacing(double hspace)
        {
            this.horizontalSpacing = hspace;
        }

        public void SetLegendBorderRect(int nborder, double rvalue)
        {
            if ((nborder >= 0) && (nborder <= 3))
            {
                this.legendBorderRect[nborder] = rvalue;
            }
        }

        public void SetLegendHeight(double rheight)
        {
            this.legendHeight = rheight;
        }

        private void SetLegendItemUniformTextColor(Color color)
        {
            this.legendItemUniformTextColor = color;
            this.UpdateLegendItemUniformTextColor();
        }

        public void SetLegendWidth(double rwidth)
        {
            this.legendWidth = rwidth;
        }

        public void SetSize(double rwidth, double rheight)
        {
            this.legendWidth = rwidth;
            this.legendHeight = rheight;
        }

        public void SetVerticalSpacing(double vspace)
        {
            this.verticalSpacing = vspace;
        }

        protected abstract void UpdateLegendItemUniformTextColor();

        public bool AutoSizeLegendRectangle
        {
            get
            {
                return this.autoSizeLegendRectangle;
            }
            set
            {
                this.autoSizeLegendRectangle = value;
            }
        }

        public double HorizontalSpacing
        {
            get
            {
                return this.horizontalSpacing;
            }
            set
            {
                this.horizontalSpacing = value;
            }
        }

        public Rectangle2D InnerLegendRectangle
        {
            get
            {
                return this.innerLegendRectangle;
            }
        }

        public double LegendHeight
        {
            get
            {
                return this.legendHeight;
            }
            set
            {
                this.legendHeight = value;
            }
        }

        public Color LegendItemUniformTextColor
        {
            get
            {
                return this.legendItemUniformTextColor;
            }
            set
            {
                this.legendItemUniformTextColor = value;
                this.UpdateLegendItemUniformTextColor();
            }
        }

        public double LegendWidth
        {
            get
            {
                return this.legendWidth;
            }
            set
            {
                this.legendWidth = value;
            }
        }

        public int NumberGeneralLegendItems
        {
            get
            {
                int num = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (this.generalLegendText[i] != null)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public double VerticalSpacing
        {
            get
            {
                return this.verticalSpacing;
            }
            set
            {
                this.verticalSpacing = value;
            }
        }
    }
}

