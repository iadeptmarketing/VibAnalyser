namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class QBrush
    {
        private Color brushcolor;
        private SolidBrush thebrush;

        public QBrush(SolidBrush brush)
        {
            this.thebrush = brush;
            this.brushcolor = this.thebrush.Color;
        }

        public Color BrushColor
        {
            get
            {
                return this.brushcolor;
            }
            set
            {
                this.brushcolor = value;
            }
        }

        public SolidBrush TheBrush
        {
            get
            {
                return this.thebrush;
            }
            set
            {
                this.thebrush = value;
                if (this.thebrush != null)
                {
                    this.brushcolor = this.thebrush.Color;
                }
            }
        }
    }
}

