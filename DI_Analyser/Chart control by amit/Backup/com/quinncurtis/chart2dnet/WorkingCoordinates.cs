namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class WorkingCoordinates : WorldCoordinates
    {
        internal int clippingArea;
        internal double[] fixedGraphBorders;
        internal bool[] fixedGraphBordersEnable;
        internal Rectangle2D graphAreaScale;
        internal Rectangle2D graphBorder;
        internal Rectangle2D plotAreaScale;
        internal Rectangle2D userClip;

        public WorkingCoordinates()
        {
            this.graphAreaScale = new Rectangle2D(-1.0, -1.0, 3.0, 3.0);
            this.plotAreaScale = new Rectangle2D(0.0, 0.0, 1.0, 1.0);
            this.userClip = new Rectangle2D(0.0, 0.0, 100.0, 100.0);
            this.graphBorder = new Rectangle2D(0.2, 0.2, 0.6, 0.6);
            this.fixedGraphBorders = new double[] { 100.0, 100.0, 100.0, 100.0 };
            this.fixedGraphBordersEnable = new bool[4];
            this.clippingArea = 1;
            this.InitDefaults();
        }

        public WorkingCoordinates(double rX1, double rY1, double rX2, double rY2)
        {
            this.graphAreaScale = new Rectangle2D(-1.0, -1.0, 3.0, 3.0);
            this.plotAreaScale = new Rectangle2D(0.0, 0.0, 1.0, 1.0);
            this.userClip = new Rectangle2D(0.0, 0.0, 100.0, 100.0);
            this.graphBorder = new Rectangle2D(0.2, 0.2, 0.6, 0.6);
            this.fixedGraphBorders = new double[] { 100.0, 100.0, 100.0, 100.0 };
            this.fixedGraphBordersEnable = new bool[4];
            this.clippingArea = 1;
            this.InitDefaults();
            this.SetWorkingScale(rX1, rY1, rX2, rY2);
        }

        protected void CalcPlotClipRect()
        {
            if ((!this.fixedGraphBordersEnable[0] && !this.fixedGraphBordersEnable[1]) && (!this.fixedGraphBordersEnable[2] && !this.fixedGraphBordersEnable[3]))
            {
                this.userClip.SetFrame(base.userViewport.GetWidth() * this.graphBorder.GetX(), base.userViewport.GetHeight() * this.graphBorder.GetY(), base.userViewport.GetWidth() * this.graphBorder.GetWidth(), base.userViewport.GetHeight() * this.graphBorder.GetHeight());
            }
            else
            {
                double num = base.userViewport.GetWidth() * this.graphBorder.GetX();
                double num2 = base.userViewport.GetHeight() * this.graphBorder.GetY();
                double num3 = base.userViewport.GetWidth() * this.graphBorder.GetWidth();
                double num4 = base.userViewport.GetHeight() * this.graphBorder.GetHeight();
                double xx = num;
                double yy = num2;
                if (this.fixedGraphBordersEnable[0])
                {
                    xx = Math.Max(1.0, this.fixedGraphBorders[0]);
                    num3 -= this.fixedGraphBorders[0] - xx;
                }
                if (this.fixedGraphBordersEnable[1])
                {
                    yy = Math.Max(1.0, this.fixedGraphBorders[1]);
                    num4 -= this.fixedGraphBorders[1] - yy;
                }
                double ww = num3;
                double hh = num4;
                if (this.fixedGraphBordersEnable[2])
                {
                    ww = Math.Max((double) 1.0, (double) ((base.userViewport.GetWidth() - xx) - this.fixedGraphBorders[2]));
                }
                if (this.fixedGraphBordersEnable[3])
                {
                    hh = Math.Max((double) 1.0, (double) ((base.userViewport.GetHeight() - yy) - this.fixedGraphBorders[3]));
                }
                this.userClip.SetFrame(xx, yy, ww, hh);
            }
        }

        public void CalcWorkingScale(Rectangle2D plotareascale)
        {
            this.CalcPlotClipRect();
            double x = plotareascale.GetX();
            double y = plotareascale.GetY();
            double num4 = plotareascale.GetX2();
            double num5 = plotareascale.GetY2();
            double num = (num4 - x) / this.userClip.GetWidth();
            double num6 = x - (num * this.userClip.GetX());
            double num7 = num4 + (num * ((base.userViewport.GetWidth() - this.userClip.GetWidth()) - this.userClip.GetX()));
            num = (num5 - y) / this.userClip.GetHeight();
            double num8 = y - (num * ((base.userViewport.GetHeight() - this.userClip.GetHeight()) - this.userClip.GetY()));
            double num9 = num5 + (num * this.userClip.GetY());
            this.graphAreaScale.SetFrameFromDiagonal(num6, num8, num7, num9);
        }

        public override object Clone()
        {
            WorkingCoordinates coordinates = new WorkingCoordinates();
            coordinates.Copy(this);
            return coordinates;
        }

        public void Copy(WorkingCoordinates source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.plotAreaScale.SetFrame(source.plotAreaScale);
                this.graphAreaScale.SetFrame(source.graphAreaScale);
                this.graphBorder.SetFrame(source.graphBorder);
                this.clippingArea = source.clippingArea;
                this.userClip.SetFrame(source.userClip);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if ((this.plotAreaScale.GetX1() == this.plotAreaScale.GetX2()) || (this.plotAreaScale.GetY1() == this.plotAreaScale.GetY2()))
                {
                    nerror = 30;
                    ChartSupport.FixCommonRangeError(this.plotAreaScale, 0.0, 1.0);
                }
                else if ((this.userClip.GetWidth() <= 5.0) || (this.userClip.GetHeight() <= 5.0))
                {
                    nerror = 0x33;
                    ChartSupport.FixCommonRangeError(this.userClip, 0.0, 100.0);
                }
                else if ((this.graphBorder.GetWidth() >= 0.99) || (this.graphBorder.GetHeight() >= 0.99))
                {
                    nerror = 60;
                    ChartSupport.FixCommonRangeError(this.graphBorder, 0.2, 0.8);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public int GetClippingArea()
        {
            return this.clippingArea;
        }

        public Rectangle2D GetGraphAreaScale()
        {
            return new Rectangle2D(this.graphAreaScale);
        }

        public double GetGraphAspectRatio()
        {
            double num = base.userViewport.GetHeight() / base.userViewport.GetWidth();
            return (num * (this.graphBorder.GetHeight() / this.graphBorder.GetWidth()));
        }

        public Rectangle2D GetGraphRect()
        {
            return (Rectangle2D) base.userViewport.Clone();
        }

        public Rectangle2D GetPlotAreaScale()
        {
            return new Rectangle2D(this.plotAreaScale);
        }

        public Rectangle2D GetPlotRect()
        {
            this.CalcPlotClipRect();
            return (Rectangle2D) this.userClip.Clone();
        }

        public double GetWorkingRangeX()
        {
            return this.graphAreaScale.GetWidth();
        }

        public double GetWorkingRangeY()
        {
            return this.graphAreaScale.GetHeight();
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x49e;
        }

        protected void InitGraphicsContext(Graphics g2)
        {
            base.SetContext(g2);
            this.CalcWorkingScale(this.plotAreaScale);
        }

        protected double NormalizeCoord(double r, double minr, double maxr)
        {
            return ((r - minr) / (maxr - minr));
        }

        public void SetClippingArea(int cliparea)
        {
            Rectangle2D rectangled = new Rectangle2D(base.userViewport.GetX() + this.userClip.GetX(), base.userViewport.GetY() + this.userClip.GetY(), this.userClip.GetWidth(), this.userClip.GetHeight());
            Rectangle2D rectangled2 = new Rectangle2D();
            switch (cliparea)
            {
                case 0:
                    rectangled2.SetFrame(base.clippingBounds);
                    break;

                case 1:
                    rectangled2.SetFrame(base.clippingBounds);
                    break;

                case 2:
                    rectangled2 = rectangled;
                    break;
            }
            if (cliparea != 3)
            {
                base.SetClipRect(rectangled2.GetX(), rectangled2.GetY(), rectangled2.GetWidth(), rectangled2.GetHeight());
            }
        }

        public void SetFixedGraphBorderInset(int border, double bordervalue, bool enable)
        {
            this.fixedGraphBorders[border] = bordervalue;
            this.fixedGraphBordersEnable[border] = enable;
            this.UpdateScale();
        }

        public void SetFixedGraphBorderInsets(double rLeft, double rTop, double rRight, double rBottom, bool leftEnable, bool topEnable, bool rightEnable, bool bottomEnable)
        {
            this.fixedGraphBorders[0] = rLeft;
            this.fixedGraphBorders[1] = rTop;
            this.fixedGraphBorders[2] = rRight;
            this.fixedGraphBorders[3] = rBottom;
            this.fixedGraphBordersEnable[0] = leftEnable;
            this.fixedGraphBordersEnable[1] = topEnable;
            this.fixedGraphBordersEnable[2] = rightEnable;
            this.fixedGraphBordersEnable[3] = bottomEnable;
            this.UpdateScale();
        }

        public void SetGraphBorderDiagonal(double rLeft, double rTop, double rRight, double rBottom)
        {
            this.graphBorder.SetFrameFromDiagonal(rLeft, rTop, rRight, rBottom);
            this.UpdateScale();
        }

        public void SetGraphBorderFrame(Rectangle2D border)
        {
            this.graphBorder.SetFrame(border);
            this.UpdateScale();
        }

        public void SetGraphBorderFrame(double rLeft, double rTop, double width, double height)
        {
            this.graphBorder.SetFrame(rLeft, rTop, width, height);
            this.UpdateScale();
        }

        public void SetGraphBorderInsets(double rLeft, double rTop, double rRight, double rBottom)
        {
            this.graphBorder.SetFrameFromDiagonal(rLeft, rTop, 1.0 - rRight, 1.0 - rBottom);
            this.UpdateScale();
        }

        public void SetPlotAreaScale(Rectangle2D rect)
        {
            this.plotAreaScale.SetFrame(rect);
            this.UpdateScale();
        }

        protected void SetWorkingScale(Rectangle2D rect)
        {
            this.plotAreaScale.SetFrame(rect);
            this.UpdateScale();
        }

        protected void SetWorkingScale(double rX1, double rY1, double rX2, double rY2)
        {
            this.plotAreaScale.SetFrameFromDiagonal(rX1, rY1, rX2, rY2);
            this.UpdateScale();
        }

        protected double UnNormalizeCoord(double r, double minr, double maxr)
        {
            return (minr + (r * (maxr - minr)));
        }

        protected void UpdateScale()
        {
            this.CalcWorkingScale(this.plotAreaScale);
            base.SetWorldScale(this.graphAreaScale);
        }

        public int ClippingArea
        {
            get
            {
                return this.clippingArea;
            }
            set
            {
                this.SetClippingArea(value);
            }
        }

        public Rectangle2D GraphBorderFrame
        {
            get
            {
                return this.graphBorder;
            }
            set
            {
                this.SetGraphBorderFrame(value);
            }
        }
    }
}

