namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ChartImage : GraphObj
    {
        internal Image imageObject;
        internal Dimension imageScaleFactor;
        internal Dimension imageSize;
        internal double rotation;
        internal int sizeMode;

        public ChartImage()
        {
            this.imageObject = null;
            this.imageSize = new Dimension(50.0, 50.0);
            this.imageScaleFactor = new Dimension(1.0, 1.0);
            this.sizeMode = 0;
            this.rotation = 0.0;
            this.InitDefaults();
        }

        public ChartImage(PhysicalCoordinates transform)
        {
            this.imageObject = null;
            this.imageSize = new Dimension(50.0, 50.0);
            this.imageScaleFactor = new Dimension(1.0, 1.0);
            this.sizeMode = 0;
            this.rotation = 0.0;
            this.InitDefaults();
            this.InitChartImage(transform, null, 0.0, 0.0, 1, 0.0);
        }

        public ChartImage(PhysicalCoordinates transform, Image aimage, double x, double y, int npostype, int rot)
        {
            this.imageObject = null;
            this.imageSize = new Dimension(50.0, 50.0);
            this.imageScaleFactor = new Dimension(1.0, 1.0);
            this.sizeMode = 0;
            this.rotation = 0.0;
            this.InitDefaults();
            this.InitChartImage(transform, aimage, x, y, npostype, (double) rot);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            ChartImage image = new ChartImage();
            image.Copy(this);
            return image;
        }

        public void Copy(ChartImage source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.imageObject = source.imageObject;
                if (source.imageSize != null)
                {
                    this.imageSize = (Dimension) source.imageSize.Clone();
                }
                if (source.imageScaleFactor != null)
                {
                    this.imageScaleFactor = (Dimension) source.imageScaleFactor.Clone();
                }
                this.sizeMode = source.sizeMode;
                this.rotation = source.rotation;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawImage(g2);
                if (this.imageObject == null)
                {
                }
            }
        }

        internal void DrawImage(Graphics g2)
        {
            double width;
            double height;
            new Dimension(1.0, 1.0);
            ChartSupport.ToRadians(this.rotation);
            Dimension dimension = new Dimension(1.0, 1.0);
            Matrix transform = g2.Transform;
            Point2D location = this.GetLocation(0);
            if (this.sizeMode == 2)
            {
                dimension = base.chartObjScale.ConvertDimension(0, this.imageSize, base.positionType);
                width = dimension.GetWidth() / ((double) this.imageObject.Width);
                height = dimension.GetHeight() / ((double) this.imageObject.Height);
            }
            else if (this.sizeMode == 0)
            {
                width = 1.0;
                height = 1.0;
                dimension = new Dimension((double) this.imageObject.Width, (double) this.imageObject.Height);
            }
            else
            {
                width = this.imageScaleFactor.GetWidth();
                height = this.imageScaleFactor.GetHeight();
                dimension = new Dimension(this.imageObject.Width * width, this.imageObject.Height * height);
            }
            if (dimension.Height < 0.0)
            {
                location.Y += dimension.Height;
                dimension.Height = -dimension.Height;
            }
            base.boundingBox.Reset();
            Rectangle2D rectangled = new Rectangle2D(location.GetX(), location.GetY(), dimension.GetWidth(), dimension.GetHeight());
            base.boundingBox.AddRectangle(rectangled.GetRectangleF());
            g2.TranslateTransform((float) location.GetX(), (float) location.GetY());
            g2.RotateTransform((float) this.rotation);
            g2.TranslateTransform((float) -location.GetX(), (float) -location.GetY());
            g2.DrawImage(this.imageObject, rectangled.GetRectangleF());
            g2.Transform = transform;
        }

        public override int ErrorCheck(int nerror)
        {
            if (this.imageObject == null)
            {
                nerror = 300;
            }
            return base.ErrorCheck(nerror);
        }

        public Image GetImageObject()
        {
            return this.imageObject;
        }

        public Rectangle2D GetImageRectangle()
        {
            return new Rectangle2D(this.GetLocation().GetX(), this.GetLocation().GetY(), this.imageSize.GetWidth(), this.imageSize.GetHeight());
        }

        public Dimension GetImageScaleFactor()
        {
            return this.imageScaleFactor;
        }

        public Dimension GetImageSize()
        {
            return this.imageSize;
        }

        public double GetRotation()
        {
            return this.rotation;
        }

        public int GetSizeMode()
        {
            return this.sizeMode;
        }

        public void InitChartImage(PhysicalCoordinates transform, Image aimage, double x, double y, int npostype, double rot)
        {
            this.SetChartObjScale(transform);
            this.SetLocation(x, y);
            this.imageObject = aimage;
            this.rotation = rot;
            base.positionType = npostype;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x1f5;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            this.InitChartImage(base.chartObjScale, null, 0.0, 0.0, 1, 0.0);
        }

        public void SetImageObject(Image image)
        {
            this.imageObject = image;
        }

        public void SetImageRectangle(Rectangle2D imagerect, int postype)
        {
            this.imageSize.SetSize(imagerect.GetWidth(), imagerect.GetHeight());
            this.SetLocation(imagerect.GetX(), imagerect.GetY(), postype);
            base.positionType = postype;
        }

        public void SetImageScaleFactor(Dimension psize)
        {
            this.imageScaleFactor.SetSize(psize);
        }

        public void SetImageSize(Dimension wh)
        {
            this.imageSize.SetSize(wh);
        }

        public void SetRotation(double rot)
        {
            this.rotation = rot;
        }

        public void SetSizeMode(int nsizemode)
        {
            this.sizeMode = nsizemode;
        }

        public Image ImageObject
        {
            get
            {
                return this.imageObject;
            }
            set
            {
                this.imageObject = value;
            }
        }

        public Dimension ImageScaleFactor
        {
            get
            {
                return this.imageScaleFactor;
            }
            set
            {
                this.imageScaleFactor.SetSize(value);
            }
        }

        public Dimension ImageSize
        {
            get
            {
                return this.imageSize;
            }
            set
            {
                this.imageSize.SetSize(value);
            }
        }

        public double Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        public int SizeMode
        {
            get
            {
                return this.sizeMode;
            }
            set
            {
                this.sizeMode = value;
            }
        }
    }
}

