namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class PhysicalCoordinates : WorkingCoordinates
    {
        internal bool normYDown;
        internal Rectangle2D physPlotScale;
        internal Scale xScale;
        internal Scale yScale;

        public PhysicalCoordinates()
        {
            this.xScale = new LinearScale(0.0, 1.0);
            this.yScale = new LinearScale(0.0, 1.0);
            this.physPlotScale = new Rectangle2D(0.0, 0.0, 1.0, 1.0);
            this.normYDown = true;
            this.InitDefaults();
        }

        public PhysicalCoordinates(Scale xscale, Scale yscale)
        {
            this.xScale = new LinearScale(0.0, 1.0);
            this.yScale = new LinearScale(0.0, 1.0);
            this.physPlotScale = new Rectangle2D(0.0, 0.0, 1.0, 1.0);
            this.normYDown = true;
            this.InitDefaults();
            this.SetPhysScales(xscale, yscale);
        }

        public abstract void AutoScale(ChartDataset dataset);
        public abstract void AutoScale(ChartDataset[] datasets);
        public abstract void AutoScale(int nroundmodeX, int nroundmodeY);
        public abstract void AutoScale(ChartDataset[] datasets, int nroundmodex, int nroundmodey);
        public abstract void AutoScale(ChartDataset dataset, int nroundmodex, int nroundmodey);
        public void ChartTransform(Graphics g2)
        {
            base.InitGraphicsContext(g2);
            this.SetPhysScale(this.physPlotScale);
        }

        public abstract bool CheckValidPoint(double x, double y);
        public virtual Point2D ConvertCoord(int ndestpostype, Point2D source, int nsrcpostype)
        {
            Point2D dest = new Point2D();
            this.ConvertCoord(dest, ndestpostype, source, nsrcpostype);
            return dest;
        }

        public virtual void ConvertCoord(Point2D dest, int ndestpostype, Point2D source, int nsrcpostype)
        {
            Point2D pointd = new Point2D();
            dest.SetLocation(source.GetX(), source.GetY());
            switch (nsrcpostype)
            {
                case 0:
                    switch (ndestpostype)
                    {
                        case 1:
                            this.UserToPhys(dest, source);
                            return;

                        case 3:
                            this.UserToPhys(pointd, source);
                            this.NormalizePoint(dest, pointd, 3);
                            return;

                        case 4:
                            this.UserToPhys(pointd, source);
                            this.NormalizePoint(dest, pointd, 4);
                            return;
                    }
                    break;

                case 1:
                    switch (ndestpostype)
                    {
                        case 0:
                            this.PhysToUser(dest, source);
                            return;

                        case 1:
                        case 2:
                            goto Label_0189;

                        case 3:
                            this.NormalizePoint(dest, source, 3);
                            return;

                        case 4:
                            this.NormalizePoint(dest, source, 4);
                            return;
                    }
                    goto Label_0189;

                case 2:
                    return;

                case 3:
                    switch (ndestpostype)
                    {
                        case 0:
                            this.UnNormalizePoint(pointd, source, 3);
                            this.PhysToUser(dest, pointd);
                            return;

                        case 1:
                            this.UnNormalizePoint(dest, source, 3);
                            return;

                        case 2:
                        case 3:
                            goto Label_00DF;

                        case 4:
                            this.UnNormalizePoint(pointd, source, 3);
                            this.NormalizePoint(dest, pointd, 4);
                            return;
                    }
                    goto Label_00DF;

                case 4:
                    switch (ndestpostype)
                    {
                        case 0:
                            this.UnNormalizePoint(pointd, source, 4);
                            this.PhysToUser(dest, pointd);
                            return;

                        case 1:
                            this.UnNormalizePoint(dest, source, 4);
                            return;

                        case 2:
                            goto Label_013B;

                        case 3:
                            this.UnNormalizePoint(dest, source, 4);
                            this.NormalizePoint(dest, pointd, 3);
                            return;
                    }
                    goto Label_013B;

                default:
                    return;
            }
            dest.SetLocation(source.GetX(), source.GetY());
            return;
        Label_00DF:
            dest.SetLocation(source.GetX(), source.GetY());
            return;
        Label_013B:
            dest.SetLocation(source.GetX(), source.GetY());
            return;
        Label_0189:
            dest.SetLocation(source.GetX(), source.GetY());
        }

        public virtual void ConvertCoordArray(Point2D[] dest, int ndestpostype, Point2D[] source, int nsrcpostype, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (dest[i] == null)
                {
                    dest[i] = new Point2D();
                }
                this.ConvertCoord(dest[i], ndestpostype, source[i], nsrcpostype);
            }
        }

        public Dimension ConvertDimension(int ndestpostype, Dimension source, int nsrcpostype)
        {
            Dimension dest = new Dimension();
            this.ConvertDimension(dest, ndestpostype, source, nsrcpostype);
            return dest;
        }

        public void ConvertDimension(Dimension dest, int ndestpostype, Dimension source, int nsrcpostype)
        {
            Point2D pointd = new Point2D();
            Point2D pointd2 = new Point2D();
            Point2D pointd3 = new Point2D(source.GetWidth(), source.GetHeight());
            this.ConvertCoord(pointd, ndestpostype, pointd2, nsrcpostype);
            this.ConvertCoord(pointd2, ndestpostype, pointd3, nsrcpostype);
            dest.SetSize(pointd2.GetX() - pointd.GetX(), pointd2.GetY() - pointd.GetY());
            pointd = null;
            pointd2 = null;
        }

        public Rectangle2D ConvertRect(int ndestpostype, Rectangle2D source, int nsrcpostype)
        {
            Rectangle2D dest = new Rectangle2D();
            this.ConvertRect(dest, ndestpostype, source, nsrcpostype);
            return dest;
        }

        public void ConvertRect(Rectangle2D dest, int ndestpostype, Rectangle2D source, int nsrcpostype)
        {
            Point2D pointd = new Point2D(source.GetX(), source.GetY());
            Dimension dimension = new Dimension(source.GetWidth(), source.GetHeight());
            Point2D pointd2 = new Point2D();
            Dimension dimension2 = new Dimension();
            Point2D pointd3 = new Point2D();
            Point2D pointd4 = new Point2D();
            this.ConvertCoord(pointd2, ndestpostype, pointd, nsrcpostype);
            pointd3.SetLocation((double) (pointd.GetX() + dimension.GetWidth()), pointd.GetY() + dimension.GetHeight());
            this.ConvertCoord(pointd4, ndestpostype, pointd3, nsrcpostype);
            dimension2.SetSize(pointd4.GetX() - pointd2.GetX(), pointd4.GetY() - pointd2.GetY());
            if (dimension2.GetHeight() < 0.0)
            {
                dimension2.SetSize(dimension2.GetWidth(), -dimension2.GetHeight());
                pointd2.SetLocation(pointd2.GetX(), pointd2.GetY() - dimension2.GetHeight());
            }
            if (dimension2.GetWidth() < 0.0)
            {
                dimension2.SetSize(-dimension2.GetWidth(), dimension2.GetHeight());
                pointd2.SetLocation((double) (pointd2.GetX() - dimension2.GetWidth()), pointd2.GetY());
            }
            dest.SetFrame(pointd2.GetX(), pointd2.GetY(), dimension2.GetWidth(), dimension2.GetHeight());
            pointd = null;
            dimension = null;
            pointd2 = null;
            dimension2 = null;
            pointd3 = null;
            pointd4 = null;
        }

        public void Copy(PhysicalCoordinates source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.xScale != null)
                {
                    this.xScale = (Scale) source.xScale.Clone();
                }
                if (source.yScale != null)
                {
                    this.yScale = (Scale) source.yScale.Clone();
                }
                this.physPlotScale.SetFrame(source.physPlotScale);
                this.normYDown = source.normYDown;
            }
        }

        public abstract void Copy(object source);
        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.xScale == null)
                {
                    nerror = 20;
                }
                else
                {
                    nerror = this.xScale.ErrorCheck(nerror);
                }
            }
            if (nerror == 0)
            {
                if (this.yScale == null)
                {
                    nerror = 20;
                }
                else
                {
                    nerror = this.yScale.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public abstract Axis GetCompatibleAxis(int axis);
        public Rectangle2D GetPhysPlotScale()
        {
            return new Rectangle2D(this.physPlotScale);
        }

        public virtual double GetScaleStartX()
        {
            return this.physPlotScale.GetX1();
        }

        public virtual double GetScaleStartY()
        {
            return this.physPlotScale.GetY1();
        }

        public virtual double GetScaleStopX()
        {
            return this.physPlotScale.GetX2();
        }

        public virtual double GetScaleStopY()
        {
            return this.physPlotScale.GetY2();
        }

        public double GetStart(int naxis)
        {
            if (naxis == 1)
            {
                return this.physPlotScale.GetY1();
            }
            return this.physPlotScale.GetX1();
        }

        public double GetStartX()
        {
            return this.physPlotScale.GetX1();
        }

        public double GetStartY()
        {
            return this.physPlotScale.GetY1();
        }

        public double GetStop(int naxis)
        {
            if (naxis == 1)
            {
                return this.physPlotScale.GetY2();
            }
            return this.physPlotScale.GetX2();
        }

        public double GetStopX()
        {
            return this.physPlotScale.GetX2();
        }

        public double GetStopY()
        {
            return this.physPlotScale.GetY2();
        }

        public double GetStringX(Graphics g2, string s, int npostype)
        {
            Dimension dimension;
            double w = 0.0;
            switch (npostype)
            {
                case 0:
                    this.ChartTransform(g2);
                    return base.GetStringX(g2, s);

                case 1:
                    this.ChartTransform(g2);
                    return this.WGetStringX(g2, s);

                case 2:
                    return w;

                case 3:
                    this.ChartTransform(g2);
                    w = this.WGetStringX(g2, s);
                    dimension = new Dimension();
                    dimension.SetSize(w, 0.0);
                    return this.ConvertDimension(3, dimension, 1).GetWidth();

                case 4:
                    this.ChartTransform(g2);
                    w = this.WGetStringX(g2, s);
                    dimension = new Dimension();
                    dimension.SetSize(w, 0.0);
                    return this.ConvertDimension(4, dimension, 1).GetWidth();
            }
            return w;
        }

        public double GetStringY(Graphics g2, string s, int npostype)
        {
            Dimension dimension;
            double h = 0.0;
            switch (npostype)
            {
                case 0:
                    this.ChartTransform(g2);
                    return base.GetStringY(g2, s);

                case 1:
                    this.ChartTransform(g2);
                    return this.WGetStringY(g2, s);

                case 2:
                    return h;

                case 3:
                    this.ChartTransform(g2);
                    h = this.WGetStringY(g2, s);
                    dimension = new Dimension();
                    dimension.SetSize(0.0, h);
                    return this.ConvertDimension(3, dimension, 1).GetHeight();

                case 4:
                    this.ChartTransform(g2);
                    h = this.WGetStringY(g2, s);
                    dimension = new Dimension();
                    dimension.SetSize(0.0, h);
                    return this.ConvertDimension(4, dimension, 1).GetHeight();
            }
            return h;
        }

        public Arc2D GetWCircle(double x, double y, double radius)
        {
            Arc2D arcd = new Arc2D();
            Point2D source = new Point2D(x, y);
            Dimension dimension = new Dimension();
            dimension.SetSize(radius, radius);
            Point2D pointd2 = this.ConvertCoord(0, source, 1);
            Dimension dimension2 = this.ConvertDimension(0, dimension, 1);
            arcd.SetArcByCenter(pointd2.GetX(), pointd2.GetY(), Math.Abs(dimension2.GetHeight()), 0.0, 359.0, 1);
            return arcd;
        }

        public Scale GetXScale()
        {
            return this.xScale;
        }

        public Scale GetYScale()
        {
            return this.yScale;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x49f;
        }

        public virtual void InvertScaleX()
        {
            this.SetPhysScale(this.physPlotScale.GetX2(), this.physPlotScale.GetY1(), this.physPlotScale.GetX1(), this.physPlotScale.GetY2());
        }

        public virtual void InvertScaleY()
        {
            this.SetPhysScale(this.physPlotScale.GetX1(), this.physPlotScale.GetY2(), this.physPlotScale.GetX2(), this.physPlotScale.GetY1());
        }

        public Point2D NormalizePoint(Point2D source, int nmode)
        {
            Point2D pointd = new Point2D();
            double r = this.PhysToWorkingScale(0, source.GetX());
            double num2 = this.PhysToWorkingScale(1, source.GetY());
            if (nmode == 3)
            {
                r = base.NormalizeCoord(r, base.graphAreaScale.GetX(), base.graphAreaScale.GetX2());
                num2 = base.NormalizeCoord(num2, base.graphAreaScale.GetY(), base.graphAreaScale.GetY2());
            }
            else
            {
                r = base.NormalizeCoord(r, base.plotAreaScale.GetX(), base.plotAreaScale.GetX2());
                num2 = base.NormalizeCoord(num2, base.plotAreaScale.GetY(), base.plotAreaScale.GetY2());
            }
            if (this.normYDown)
            {
                num2 = 1.0 - num2;
            }
            pointd.SetLocation(r, num2);
            return pointd;
        }

        public void NormalizePoint(Point2D dest, Point2D source, int nmode)
        {
            Point2D pointd = this.NormalizePoint(source, nmode);
            dest.SetLocation(pointd.GetX(), pointd.GetY());
        }

        public Dimension NormalizeRect(double w, double h, int nmode)
        {
            Dimension dimension = new Dimension();
            w = this.PhysToWorkingScale(0, w);
            h = this.PhysToWorkingScale(1, h);
            if (nmode == 3)
            {
                w = base.NormalizeCoord(w, 0.0, base.graphAreaScale.GetWidth());
                h = base.NormalizeCoord(h, 0.0, base.graphAreaScale.GetHeight());
            }
            else
            {
                w = base.NormalizeCoord(w, 0.0, base.plotAreaScale.GetWidth());
                h = base.NormalizeCoord(h, 0.0, base.plotAreaScale.GetHeight());
            }
            dimension.SetSize(w, h);
            return dimension;
        }

        public double PhysAddX(double x, double increment)
        {
            return this.xScale.CoordinateAdd(x, increment);
        }

        public double PhysAddY(double y, double increment)
        {
            return this.yScale.CoordinateAdd(y, increment);
        }

        public Point2D PhysToUser(Point2D source)
        {
            return new Point2D(this.PhysToUserX(source.GetX()), this.PhysToUserY(source.GetY()));
        }

        public void PhysToUser(Point2D dest, Point2D source)
        {
            dest.SetLocation(this.PhysToUserX(source.GetX()), this.PhysToUserY(source.GetY()));
        }

        public double PhysToUserX(double x)
        {
            x = this.xScale.PhysToWorkingScale(x);
            return base.WorldToUserAbsX(x);
        }

        public double PhysToUserY(double y)
        {
            y = this.yScale.PhysToWorkingScale(y);
            return base.WorldToUserAbsY(y);
        }

        public void PhysToWorkingScale(Point2D dest, Point2D source)
        {
            double x = source.GetX();
            double y = source.GetY();
            x = this.xScale.PhysToWorkingScale(x);
            y = this.yScale.PhysToWorkingScale(y);
            if (dest == null)
            {
                dest = new Point2D();
            }
            dest.SetLocation(x, y);
        }

        public double PhysToWorkingScale(int axis, double v)
        {
            if (axis == 0)
            {
                return this.xScale.PhysToWorkingScale(v);
            }
            return this.yScale.PhysToWorkingScale(v);
        }

        public bool ScaleInverted(int axis)
        {
            bool flag = false;
            if (axis == 1)
            {
                if (this.physPlotScale.GetY1() > this.physPlotScale.GetY2())
                {
                    flag = true;
                }
                return flag;
            }
            if (this.physPlotScale.GetX1() > this.physPlotScale.GetX2())
            {
                flag = true;
            }
            return flag;
        }

        public virtual void SetCoordinateBounds(double rX1, double rY1, double rX2, double rY2)
        {
            this.SetPhysScale(rX1, rY1, rX2, rY2);
        }

        public void SetPhysPlotScale(Rectangle2D physrect)
        {
            this.physPlotScale.SetFrame(physrect);
        }

        public void SetPhysScale(Rectangle2D rect)
        {
            this.SetPhysScale(rect.GetX(), rect.GetY(), rect.GetX() + rect.GetWidth(), rect.GetY() + rect.GetHeight());
        }

        public void SetPhysScale(double rX1, double rY1, double rX2, double rY2)
        {
            Point2D source = new Point2D(rX1, rY1);
            Point2D pointd2 = new Point2D(rX2, rY2);
            Point2D dest = new Point2D(rX1, rY1);
            Point2D pointd4 = new Point2D(rX2, rY2);
            this.xScale.SetScale(rX1, rX2);
            this.yScale.SetScale(rY1, rY2);
            this.physPlotScale.SetFrameFromDiagonal(rX1, rY1, rX2, rY2);
            this.PhysToWorkingScale(dest, source);
            this.PhysToWorkingScale(pointd4, pointd2);
            base.SetWorkingScale(dest.GetX(), dest.GetY(), pointd4.GetX(), pointd4.GetY());
        }

        public void SetPhysScales(Scale xscale, Scale yscale)
        {
            this.xScale = xscale;
            this.yScale = yscale;
        }

        public virtual void SetScaleStartX(double rX1)
        {
            this.SetPhysScale(rX1, this.physPlotScale.GetY1(), this.physPlotScale.GetX2(), this.physPlotScale.GetY2());
        }

        public virtual void SetScaleStartY(double rY1)
        {
            this.SetPhysScale(this.physPlotScale.GetX1(), rY1, this.physPlotScale.GetX2(), this.physPlotScale.GetY2());
        }

        public virtual void SetScaleStopX(double rX2)
        {
            this.SetPhysScale(this.physPlotScale.GetX1(), this.physPlotScale.GetY1(), rX2, this.physPlotScale.GetY2());
        }

        public virtual void SetScaleStopY(double rY2)
        {
            this.SetPhysScale(this.physPlotScale.GetX1(), this.physPlotScale.GetY1(), this.physPlotScale.GetX2(), rY2);
        }

        public virtual void SetScaleX(double rX1, double rX2)
        {
            this.SetPhysScale(rX1, this.physPlotScale.GetY(), rX2, this.physPlotScale.GetY2());
        }

        public virtual void SetScaleY(double rY1, double rY2)
        {
            this.SetPhysScale(this.physPlotScale.GetX(), rY1, this.physPlotScale.GetX2(), rY2);
        }

        public void SetXScale(Scale xscale)
        {
            this.xScale = xscale;
        }

        public void SetYScale(Scale yscale)
        {
            this.yScale = yscale;
        }

        public virtual void SwapScaleOrientation()
        {
            this.SetPhysScale(this.physPlotScale.GetY1(), this.physPlotScale.GetX1(), this.physPlotScale.GetY2(), this.physPlotScale.GetX2());
        }

        public Point2D UnNormalizePoint(Point2D source, int nmode)
        {
            Point2D pointd = new Point2D();
            double x = source.GetX();
            double y = source.GetY();
            if (this.normYDown)
            {
                y = 1.0 - y;
            }
            if (nmode == 3)
            {
                x = base.UnNormalizeCoord(x, base.graphAreaScale.GetX(), base.graphAreaScale.GetX2());
                y = base.UnNormalizeCoord(y, base.graphAreaScale.GetY(), base.graphAreaScale.GetY2());
            }
            else
            {
                x = base.UnNormalizeCoord(x, base.plotAreaScale.GetX(), base.plotAreaScale.GetX2());
                y = base.UnNormalizeCoord(y, base.plotAreaScale.GetY(), base.plotAreaScale.GetY2());
            }
            x = this.WorkingToPhysScale(0, x);
            y = this.WorkingToPhysScale(1, y);
            pointd.SetLocation(x, y);
            return pointd;
        }

        public void UnNormalizePoint(Point2D dest, Point2D source, int nmode)
        {
            Point2D pointd = this.UnNormalizePoint(source, nmode);
            dest.SetLocation(pointd.GetX(), pointd.GetY());
        }

        public Dimension UnNormalizeRect(double w, double h, int nmode)
        {
            Dimension dimension = new Dimension();
            if (nmode == 3)
            {
                w = base.UnNormalizeCoord(w, 0.0, base.graphAreaScale.GetWidth());
                h = base.UnNormalizeCoord(h, 0.0, base.graphAreaScale.GetHeight());
            }
            else
            {
                w = base.UnNormalizeCoord(w, 0.0, base.plotAreaScale.GetWidth());
                h = base.UnNormalizeCoord(h, 0.0, base.plotAreaScale.GetHeight());
            }
            w = this.WorkingToPhysScale(0, w);
            h = this.WorkingToPhysScale(1, h);
            dimension.SetSize(w, h);
            return dimension;
        }

        public void UnNormalizeRect(Dimension dest, double w, double h, int nmode)
        {
            Dimension d = this.UnNormalizeRect(w, h, nmode);
            dest.SetSize(d);
        }

        public Point2D UserToPhys(Point2D source)
        {
            return new Point2D(this.UserToPhysX(source.GetX()), this.UserToPhysY(source.GetY()));
        }

        public void UserToPhys(Point2D dest, Point2D source)
        {
            dest.SetLocation(this.UserToPhysX(source.GetX()), this.UserToPhysY(source.GetY()));
        }

        public double UserToPhysX(double x)
        {
            x = base.UserToWorldAbsX(x);
            return this.xScale.WorkingToPhysScale(x);
        }

        public double UserToPhysY(double y)
        {
            y = base.UserToWorldAbsY(y);
            return this.yScale.WorkingToPhysScale(y);
        }

        public void WCircle(GraphicsPath path, double x, double y, double radius)
        {
            RectangleF rectangleF = this.GetWCircle(x, y, radius).GetRectangleF();
            path.AddEllipse(rectangleF);
        }

        private double WGetStringX(Graphics g2, string s)
        {
            return (base.GetStringX(g2, s) / base.worldScaleFactor.GetX());
        }

        private double WGetStringY(Graphics g2, string s)
        {
            return (-base.GetStringY(g2, s) / base.worldScaleFactor.GetY());
        }

        public void WLineAbs(GraphicsPath path, Point2D p1, Point2D p2)
        {
            this.WLineAbs(path, p1.GetX(), p1.GetY(), p2.GetX(), p2.GetY());
        }

        public void WLineAbs(GraphicsPath path, double x1, double y1, double x2, double y2)
        {
            double r = this.PhysToUserX(x1);
            double num2 = this.PhysToUserY(y1);
            double px = this.PhysToUserX(x2);
            double py = this.PhysToUserY(y2);
            r = ChartSupport.ClampToViewCoordinates(r);
            num2 = ChartSupport.ClampToViewCoordinates(num2);
            base.worldCurrentPos.SetLocation(x2, y2);
            base.userCurrentPos.SetLocation(px, py);
            px = ChartSupport.ClampToViewCoordinates(px);
            py = ChartSupport.ClampToViewCoordinates(py);
            path.AddLine((float) r, (float) num2, (float) px, (float) py);
        }

        public void WLineAbs(Graphics g2, GraphicsPath path, double x1, double y1, double x2, double y2, Pen linepen, bool dodraw, bool appendpath)
        {
            GraphicsPath path2 = new GraphicsPath();
            double r = this.PhysToUserX(x1);
            double num2 = this.PhysToUserY(y1);
            double px = this.PhysToUserX(x2);
            double py = this.PhysToUserY(y2);
            r = ChartSupport.ClampToViewCoordinates(r);
            num2 = ChartSupport.ClampToViewCoordinates(num2);
            base.worldCurrentPos.SetLocation(x2, y2);
            base.userCurrentPos.SetLocation(px, py);
            px = ChartSupport.ClampToViewCoordinates(px);
            py = ChartSupport.ClampToViewCoordinates(py);
            path2.AddLine((float) r, (float) num2, (float) px, (float) py);
            if (dodraw)
            {
                base.DrawPath(g2, linepen, path2);
            }
            if (appendpath)
            {
                path.AddPath(path2, false);
            }
        }

        public void WLineRel(GraphicsPath path, double deltax, double deltay)
        {
            double x = this.PhysAddX(base.worldCurrentPos.GetX(), deltax);
            double y = this.PhysAddY(base.worldCurrentPos.GetY(), deltay);
            this.WLineToAbs(path, x, y);
        }

        public void WLineToAbs(GraphicsPath path, double x, double y)
        {
            double num = ChartSupport.ClampToViewCoordinates(base.userCurrentPos.GetX());
            double num2 = ChartSupport.ClampToViewCoordinates(base.userCurrentPos.GetY());
            double px = this.PhysToUserX(x);
            double py = this.PhysToUserY(y);
            base.worldCurrentPos.SetLocation(x, y);
            base.userCurrentPos.SetLocation(px, py);
            px = ChartSupport.ClampToViewCoordinates(px);
            py = ChartSupport.ClampToViewCoordinates(py);
            path.AddLine((float) num, (float) num2, (float) px, (float) py);
        }

        public void WMoveToAbs(GraphicsPath path, double x, double y)
        {
            double px = this.PhysToUserX(x);
            double py = this.PhysToUserY(y);
            base.worldCurrentPos.SetLocation(x, y);
            base.userCurrentPos.SetLocation(px, py);
        }

        public void WorkingToPhysScale(Point2D dest, Point2D source)
        {
            double x = source.GetX();
            double y = source.GetY();
            x = this.xScale.WorkingToPhysScale(x);
            y = this.yScale.WorkingToPhysScale(y);
            if (dest == null)
            {
                dest = new Point2D();
            }
            dest.SetLocation(x, y);
        }

        public double WorkingToPhysScale(int axis, double v)
        {
            if (axis == 0)
            {
                return this.xScale.WorkingToPhysScale(v);
            }
            return this.yScale.WorkingToPhysScale(v);
        }

        public void WPolyLineAbs(GraphicsPath path, DoubleArray x, DoubleArray y, int stepmode)
        {
            int num4 = 0;
            int length = x.Length;
            int index = 0;
            PointF tf = new PointF();
            num4 = ChartSupport.GetFirstValidIndex(x.DataBuffer, y.DataBuffer, length);
            if (num4 < length)
            {
                PointF[] tfArray;
                if (stepmode == 0)
                {
                    tfArray = new PointF[length - num4];
                }
                else
                {
                    tfArray = new PointF[(2 * (length - num4)) - 1];
                }
                for (int i = num4; i < length; i++)
                {
                    double num = x[i];
                    double num2 = y[i];
                    double r = this.PhysToUserX(num);
                    double num8 = this.PhysToUserY(num2);
                    r = ChartSupport.ClampToViewCoordinates(r);
                    num8 = ChartSupport.ClampToViewCoordinates(num8);
                    tf.X = (float) r;
                    tf.Y = (float) num8;
                    if (index > 0)
                    {
                        if (stepmode == 1)
                        {
                            tfArray[index].X = tfArray[index - 1].X;
                            tfArray[index].Y = tf.Y;
                            index++;
                        }
                        else if (stepmode == 2)
                        {
                            tfArray[index].X = tf.X;
                            tfArray[index].Y = tfArray[index - 1].Y;
                            index++;
                        }
                    }
                    tfArray[index].X = tf.X;
                    tfArray[index].Y = tf.Y;
                    index++;
                }
                path.AddLines(tfArray);
            }
        }

        public void WPolyLineAbs(GraphicsPath path, Point2D[] p, int numdat, int stepmode)
        {
            int firstValidIndex = ChartSupport.GetFirstValidIndex(p, numdat);
            if (firstValidIndex < numdat)
            {
                double x = p[firstValidIndex].GetX();
                double y = p[firstValidIndex].GetY();
                this.WMoveToAbs(path, x, y);
                for (int i = firstValidIndex; i < numdat; i++)
                {
                    x = p[i].GetX();
                    y = p[i].GetY();
                    this.WStepLineToAbs(path, x, y, stepmode);
                }
            }
        }

        public void WPolyLineAbs(GraphicsPath path, double[] x, double[] y, int numdat, int stepmode)
        {
            int index = ChartSupport.GetFirstValidIndex(x, y, numdat);
            if (index < numdat)
            {
                double num = x[index];
                double num2 = y[index];
                this.WMoveToAbs(path, num, num2);
                for (int i = index; i < numdat; i++)
                {
                    num = x[i];
                    num2 = y[i];
                    this.WStepLineToAbs(path, num, num2, stepmode);
                }
            }
        }

        public void WRectangle(GraphicsPath path, double x1, double y1, double w, double h)
        {
            double r = this.PhysToUserX(x1);
            double num2 = this.PhysToUserY(y1);
            double num4 = this.PhysToUserX(this.PhysAddX(x1, w)) - this.PhysToUserX(x1);
            double num5 = this.PhysToUserY(this.PhysAddY(y1, h)) - this.PhysToUserY(y1);
            if (num5 > 0.0)
            {
                num2 += num5;
                num5 = -num5;
            }
            if (num4 < 0.0)
            {
                r += num4;
                num4 = -num4;
            }
            double num3 = num2 + num5;
            r = ChartSupport.ClampToViewCoordinates(r);
            num3 = ChartSupport.ClampToViewCoordinates(num3);
            num4 = ChartSupport.ClampToViewCoordinates(num4);
            num5 = ChartSupport.ClampToViewCoordinates(num5);
            RectangleF rect = new RectangleF((float) r, (float) num3, (float) num4, (float) -num5);
            path.AddRectangle(rect);
        }

        public void WRoundedRectangle(GraphicsPath path, double x1, double y1, double w, double h, double corner)
        {
            float num6 = (float) corner;
            double num7 = Math.Min(Math.Abs(h), Math.Abs(w));
            if (corner <= (Math.Abs(num7) / 500.0))
            {
                this.WRectangle(path, x1, y1, w, h);
            }
            else
            {
                float x = (float) this.PhysToUserX(x1);
                float num2 = (float) this.PhysToUserY(y1);
                float num4 = (float) (this.PhysToUserX(this.PhysAddX(x1, w)) - this.PhysToUserX(x1));
                float num5 = (float) (this.PhysToUserY(this.PhysAddY(y1, h)) - this.PhysToUserY(y1));
                if (num5 < 0f)
                {
                    num2 += num5;
                    num5 = -num5;
                }
                if (num4 < 0f)
                {
                    x += num4;
                    num4 = -num4;
                }
                float num3 = num2 + num5;
                x = (float) ChartSupport.ClampToViewCoordinates((double) x);
                num3 = (float) ChartSupport.ClampToViewCoordinates((double) num3);
                num4 = (float) ChartSupport.ClampToViewCoordinates((double) num4);
                num5 = (float) ChartSupport.ClampToViewCoordinates((double) num5);
                num6 = Math.Min(num6, Math.Min((float) (Math.Abs(num4) / 2f), (float) (Math.Abs(num5) / 2f)));
                float width = 2f * num6;
                path.AddLine(x + num6, num2, (x + num4) - num6, num2);
                path.AddArc((x + num4) - width, num2, width, width, 270f, 90f);
                path.AddLine((float) (x + num4), (float) (num2 + num6), (float) (x + num4), (float) ((num2 + num5) - num6));
                path.AddArc((x + num4) - width, (num2 + num5) - width, width, width, 0f, 90f);
                path.AddLine((float) ((x + num4) - num6), (float) (num2 + num5), (float) (x + num6), (float) (num2 + num5));
                path.AddArc(x, (num2 + num5) - width, width, width, 90f, 90f);
                path.AddLine(x, (num2 + num5) - num6, x, num2 + num6);
                path.AddArc(x, num2, width, width, 180f, 90f);
                path.CloseFigure();
            }
        }

        public void WStepLineAbs(GraphicsPath path, Point2D p1, Point2D p2, int stepmode)
        {
            this.WStepLineAbs(path, p1.GetX(), p1.GetY(), p2.GetX(), p2.GetY(), stepmode);
        }

        public void WStepLineAbs(GraphicsPath path, double x1, double y1, double x2, double y2, int stepmode)
        {
            switch (stepmode)
            {
                case 1:
                    this.WLineAbs(path, x1, y1, x1, y2);
                    this.WLineAbs(path, x1, y2, x2, y2);
                    return;

                case 2:
                    this.WLineAbs(path, x1, y1, x2, y1);
                    this.WLineAbs(path, x2, y1, x2, y2);
                    return;

                case 3:
                    this.WLineAbs(path, x1, y2, x2, y2);
                    return;
            }
            this.WLineAbs(path, x1, y1, x2, y2);
        }

        public void WStepLineToAbs(GraphicsPath path, Point2D p1, int stepmode)
        {
            this.WStepLineToAbs(path, p1.GetX(), p1.GetY(), stepmode);
        }

        public void WStepLineToAbs(GraphicsPath path, double x2, double y2, int stepmode)
        {
            double x = base.worldCurrentPos.GetX();
            double y = base.worldCurrentPos.GetY();
            switch (stepmode)
            {
                case 1:
                    this.WLineToAbs(path, x, y2);
                    break;

                case 2:
                    this.WLineToAbs(path, x2, y);
                    break;

                case 3:
                    this.WLineToAbs(path, x2, y);
                    break;
            }
            if (stepmode != 3)
            {
                this.WLineToAbs(path, x2, y2);
            }
        }

        public virtual double ScaleMaxX
        {
            get
            {
                return Math.Max(this.physPlotScale.GetX1(), this.physPlotScale.GetX2());
            }
        }

        public virtual double ScaleMaxY
        {
            get
            {
                return Math.Max(this.physPlotScale.GetY1(), this.physPlotScale.GetY2());
            }
        }

        public virtual double ScaleMinX
        {
            get
            {
                return Math.Min(this.physPlotScale.GetX1(), this.physPlotScale.GetX2());
            }
        }

        public virtual double ScaleMinY
        {
            get
            {
                return Math.Min(this.physPlotScale.GetY1(), this.physPlotScale.GetY2());
            }
        }

        public virtual double ScaleStartX
        {
            get
            {
                return this.physPlotScale.GetX1();
            }
            set
            {
                this.SetPhysScale(value, this.physPlotScale.GetY1(), this.physPlotScale.GetX2(), this.physPlotScale.GetY2());
            }
        }

        public virtual double ScaleStartY
        {
            get
            {
                return this.physPlotScale.GetY1();
            }
            set
            {
                this.SetPhysScale(this.physPlotScale.GetX1(), value, this.physPlotScale.GetX2(), this.physPlotScale.GetY2());
            }
        }

        public virtual double ScaleStopX
        {
            get
            {
                return this.physPlotScale.GetX2();
            }
            set
            {
                this.SetPhysScale(this.physPlotScale.GetX1(), this.physPlotScale.GetY1(), value, this.physPlotScale.GetY2());
            }
        }

        public virtual double ScaleStopY
        {
            get
            {
                return this.physPlotScale.GetY2();
            }
            set
            {
                this.SetPhysScale(this.physPlotScale.GetX1(), this.physPlotScale.GetY1(), this.physPlotScale.GetX2(), value);
            }
        }

        public Scale XScale
        {
            get
            {
                return this.xScale;
            }
            set
            {
                this.xScale = value;
            }
        }

        public Scale YScale
        {
            get
            {
                return this.yScale;
            }
            set
            {
                this.yScale = value;
            }
        }
    }
}

