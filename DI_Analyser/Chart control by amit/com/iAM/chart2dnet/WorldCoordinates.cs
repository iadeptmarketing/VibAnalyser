namespace com.iAM.chart2dnet
{
    using System;

    public abstract class WorldCoordinates : UserCoordinates
    {
        internal Point2D worldCurrentPos = new Point2D(0.0, 0.0);
        internal Rectangle2D worldScale = new Rectangle2D(0.0, 0.0, 1.0, 1.0);
        internal Point2D worldScaleFactor = new Point2D(1.0, 1.0);

        public WorldCoordinates()
        {
            this.InitDefaults();
        }

        public void Copy(WorldCoordinates source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.worldScaleFactor.SetLocation(source.worldScaleFactor);
                this.worldScale.SetFrame(source.worldScale);
                this.worldCurrentPos.SetLocation(source.worldCurrentPos);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && ((this.worldScale.GetX1() == this.worldScale.GetX2()) || (this.worldScale.GetY1() == this.worldScale.GetY2())))
            {
                nerror = 30;
                ChartSupport.FixCommonRangeError(this.worldScale, 0.0, 1.0);
            }
            return base.ErrorCheck(nerror);
        }

        public Point2D GetWorldCurrentPos()
        {
            return (Point2D) this.worldCurrentPos.Clone();
        }

        public double GetWorldX1()
        {
            return this.worldScale.GetX();
        }

        public double GetWorldX2()
        {
            return this.worldScale.GetX2();
        }

        public double GetWorldY1()
        {
            return this.worldScale.GetY();
        }

        public double GetWorldY2()
        {
            return this.worldScale.GetY2();
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x49d;
        }

        public void SetWorldScale(Rectangle2D rect)
        {
            this.worldScale.SetFrame(rect);
            double px = base.userViewport.GetWidth() / this.worldScale.GetWidth();
            double py = -base.userViewport.GetHeight() / this.worldScale.GetHeight();
            this.worldScaleFactor.SetLocation(px, py);
        }

        public void SetWorldScale(double left, double bottom, double right, double top)
        {
            Rectangle2D rect = new Rectangle2D(left, bottom, right - left, top - bottom);
            this.SetWorldScale(rect);
            rect = null;
        }

        public Point2D UserToWorld(Point2D source)
        {
            Point2D dest = new Point2D();
            this.UserToWorld(dest, source);
            return dest;
        }

        public void UserToWorld(Point2D dest, Point2D source)
        {
            double px = this.UserToWorldAbsX(source.GetX());
            double py = this.UserToWorldAbsY(source.GetY());
            dest.SetLocation(px, py);
        }

        public Point2D UserToWorld(double px, double py)
        {
            Point2D source = new Point2D(px, py);
            Point2D dest = new Point2D();
            this.UserToWorld(dest, source);
            source = null;
            return dest;
        }

        public void UserToWorld(Point2D dest, double px, double py)
        {
            Point2D source = new Point2D(px, py);
            this.UserToWorld(dest, source);
            source = null;
        }

        public double UserToWorldAbsX(double ruserx)
        {
            return (this.worldScale.GetX() + ((ruserx - base.userViewport.GetX()) / this.worldScaleFactor.GetX()));
        }

        public double UserToWorldAbsY(double rusery)
        {
            return (this.worldScale.GetY2() + ((rusery - base.userViewport.GetY()) / this.worldScaleFactor.GetY()));
        }

        public double UserToWorldRelX(double ruserx)
        {
            return (ruserx / this.worldScaleFactor.GetX());
        }

        public double UserToWorldRelY(double rusery)
        {
            return (rusery / this.worldScaleFactor.GetY());
        }

        public Point2D WorldToUser(Point2D source)
        {
            Point2D dest = new Point2D();
            this.WorldToUser(dest, source);
            return dest;
        }

        public void WorldToUser(Point2D dest, Point2D source)
        {
            double px = this.WorldToUserAbsX(source.GetX());
            double py = this.WorldToUserAbsY(source.GetY());
            dest.SetLocation(px, py);
        }

        public Point2D WorldToUser(double wx1, double wy1)
        {
            double px = this.WorldToUserAbsX(wx1);
            return new Point2D(px, this.WorldToUserAbsY(wy1));
        }

        public double WorldToUserAbsX(double rphysx)
        {
            double num = (rphysx - this.worldScale.X) * this.worldScaleFactor.X;
            return (base.userViewport.X + num);
        }

        public double WorldToUserAbsY(double rphysy)
        {
            double num = (rphysy - this.worldScale.GetY()) * this.worldScaleFactor.GetY();
            return ((base.userViewport.GetY() + base.userViewport.GetHeight()) + num);
        }

        public double WorldToUserRelX(double rphysx)
        {
            return (rphysx * this.worldScaleFactor.GetX());
        }

        public double WorldToUserRelY(double rphysy)
        {
            return (rphysy * this.worldScaleFactor.GetY());
        }

        public Rectangle2D WorldScale
        {
            get
            {
                return (Rectangle2D) this.worldScale.Clone();
            }
            set
            {
                this.SetWorldScale(value);
            }
        }
    }
}

