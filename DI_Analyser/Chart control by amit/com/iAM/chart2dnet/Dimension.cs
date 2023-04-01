namespace com.iAM.chart2dnet
{
    using System;

    public class Dimension : ChartObj
    {
        private double height;
        private double width;

        public Dimension()
        {
            this.width = 0.0;
            this.height = 0.0;
        }

        public Dimension(Dimension d)
        {
            this.width = 0.0;
            this.height = 0.0;
            this.width = d.width;
            this.height = d.height;
        }

        public Dimension(double w, double h)
        {
            this.width = 0.0;
            this.height = 0.0;
            this.width = w;
            this.height = h;
        }

        public override object Clone()
        {
            Dimension dimension = new Dimension();
            dimension.Copy(this);
            return dimension;
        }

        public void Copy(Dimension source)
        {
            if (source != null)
            {
                this.width = source.width;
                this.height = source.height;
            }
        }

        public double GetHeight()
        {
            return this.height;
        }

        public Dimension GetSize()
        {
            return new Dimension(this.width, this.height);
        }

        public double GetWidth()
        {
            return this.width;
        }

        public void SetSize(Dimension d)
        {
            this.SetSize(d.width, d.height);
        }

        public void SetSize(double w, double h)
        {
            this.width = w;
            this.height = h;
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
    }
}

