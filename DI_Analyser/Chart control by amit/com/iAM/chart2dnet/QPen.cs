namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class QPen
    {
        private System.Drawing.Drawing2D.DashStyle dashstyle = System.Drawing.Drawing2D.DashStyle.Solid;
        private Color pencolor = Color.White;
        private Pen thepen;
        private float width = 1f;

        public QPen(Pen pen)
        {
            this.thepen = pen;
            this.pencolor = pen.Color;
            this.dashstyle = pen.DashStyle;
            this.width = pen.Width;
        }

        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get
            {
                return this.dashstyle;
            }
            set
            {
                this.dashstyle = value;
            }
        }

        public Color PenColor
        {
            get
            {
                return this.pencolor;
            }
            set
            {
                this.pencolor = value;
            }
        }

        public Pen ThePen
        {
            get
            {
                return this.thepen;
            }
            set
            {
                this.thepen = value;
                if (this.thepen != null)
                {
                    this.pencolor = this.thepen.Color;
                    this.dashstyle = this.thepen.DashStyle;
                    this.width = this.thepen.Width;
                }
            }
        }

        public float Width
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

