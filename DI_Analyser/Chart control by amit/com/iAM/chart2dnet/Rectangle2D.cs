namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class Rectangle2D : ICloneable
    {
        private double height;
        private double width;
        private double x;
        private double y;

        public Rectangle2D()
        {
        }

        public Rectangle2D(Rectangle2D r)
        {
            this.SetFrame(r);
        }

        public Rectangle2D(RectangleF r)
        {
            this.SetFrame(r);
        }

        public Rectangle2D(double xx, double yy, double ww, double hh)
        {
            this.SetFrame(xx, yy, ww, hh);
        }

        public virtual object Clone()
        {
            return new Rectangle2D(this.x, this.y, this.width, this.height);
        }

        public virtual bool Contains(Point2D p)
        {
            return this.Contains(p.GetX(), p.GetY());
        }

        public virtual bool Contains(Rectangle2D testr)
        {
            RectangleF ef = new RectangleF((float) this.x, (float) this.y, (float) this.width, (float) this.height);
            RectangleF rect = new RectangleF((float) testr.x, (float) testr.y, (float) testr.width, (float) testr.height);
            return ef.Contains(rect);
        }

        public virtual bool Contains(double xx, double yy)
        {
            RectangleF ef = new RectangleF((float) this.x, (float) this.y, (float) this.width, (float) this.height);
            return ef.Contains((float) xx, (float) yy);
        }

        public void Copy(Rectangle2D source)
        {
            if (source != null)
            {
                this.x = source.x;
                this.y = source.y;
                this.width = source.width;
                this.height = source.height;
            }
        }

        private static void FixRect(Rectangle2D rect)
        {
            if (rect.Width < 0.0)
            {
                rect.X += rect.Width;
                rect.Width = -rect.Width;
            }
            if (rect.Height < 0.0)
            {
                rect.Y += rect.Height;
                rect.Height = -rect.Height;
            }
        }

        private static void FixRect(RectangleF rect)
        {
            if (rect.Width < 0f)
            {
                rect.X += rect.Width;
                rect.Width = -rect.Width;
            }
            if (rect.Height < 0f)
            {
                rect.Y += rect.Height;
                rect.Height = -rect.Height;
            }
        }

        public double GetCenterX()
        {
            return (this.x + (this.width / 2.0));
        }

        public double GetCenterY()
        {
            return (this.y + (this.height / 2.0));
        }

        public Point2D GetDiagonalCorner()
        {
            Point2D pointd = new Point2D();
            pointd.SetLocation(this.GetX2(), this.GetY2());
            return pointd;
        }

        public double GetHeight()
        {
            return this.height;
        }

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle((int) this.x, (int) this.y, (int) this.width, (int) this.height);
        }

        public virtual RectangleF GetRectangleF()
        {
            return new RectangleF((float) this.x, (float) this.y, (float) this.width, (float) this.height);
        }

        public double GetWidth()
        {
            return this.width;
        }

        public double GetX()
        {
            return this.x;
        }

        public double GetX1()
        {
            return this.GetX();
        }

        public double GetX2()
        {
            return (this.GetX() + this.GetWidth());
        }

        public double GetY()
        {
            return this.y;
        }

        public double GetY1()
        {
            return this.GetY();
        }

        public double GetY2()
        {
            return (this.GetY() + this.GetHeight());
        }

        public virtual bool IntersectsWith(Rectangle2D testr)
        {
            RectangleF ef = new RectangleF((float) this.x, (float) this.y, (float) this.width, (float) this.height);
            RectangleF rect = new RectangleF((float) testr.x, (float) testr.y, (float) testr.width, (float) testr.height);
            return ef.IntersectsWith(rect);
        }

        public void NormalizeHW()
        {
            if (this.height < 0.0)
            {
                this.y += this.height;
                this.height = Math.Abs(this.height);
            }
            if (this.width < 0.0)
            {
                this.x += this.width;
                this.width = Math.Abs(this.width);
            }
        }

        public void SetDiagonalCorner(Point2D p)
        {
            this.SetFrameFromDiagonal(this.x, this.y, p.GetX(), p.GetY());
        }

        public void SetFrame(Rectangle2D r)
        {
            this.x = r.x;
            this.y = r.y;
            this.width = r.width;
            this.height = r.height;
        }

        public void SetFrame(RectangleF r)
        {
            this.x = r.X;
            this.y = r.Y;
            this.width = r.Width;
            this.height = r.Height;
        }

        public void SetFrame(double xx, double yy, double ww, double hh)
        {
            this.x = xx;
            this.y = yy;
            this.width = ww;
            this.height = hh;
        }

        public void SetFrameFromDiaglonal(double x1, double y1, double x2, double y2)
        {
            this.x = x1;
            this.y = y1;
            this.width = x2 - x1;
            this.height = y2 - y1;
        }

        public void SetFrameFromDiagonal(Point2D p1, Point2D p2)
        {
            this.SetFrame(p1.GetX(), p1.GetY(), p2.GetX(), p2.GetY());
        }

        public void SetFrameFromDiagonal(double x1, double y1, double x2, double y2)
        {
            this.SetFrame(x1, y1, x2 - x1, y2 - y1);
        }

        public void SetLocation(double xx, double yy)
        {
            this.x = xx;
            this.y = yy;
        }

        public void SetX1(double xx)
        {
            this.x = xx;
        }

        public void SetX2(double x2)
        {
            this.width = x2 - this.x;
        }

        public void SetY1(double yy)
        {
            this.y = yy;
        }

        public void SetY2(double y2)
        {
            this.height = y2 - this.y;
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
    }
}

