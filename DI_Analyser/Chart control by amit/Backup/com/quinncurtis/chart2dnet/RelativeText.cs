namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class RelativeText : ChartText
    {
        private ChartText positionReference;
        private int relativePositionJustX;
        private int relativePositionJustY;

        public RelativeText()
        {
            this.relativePositionJustX = 100;
            this.relativePositionJustY = 100;
            this.positionReference = null;
        }

        public RelativeText(PhysicalCoordinates transform)
        {
            this.relativePositionJustX = 100;
            this.relativePositionJustY = 100;
            this.positionReference = null;
            this.SetChartObjScale(transform);
            this.InitDefaults();
        }

        public RelativeText(Font tfont, string tstring)
        {
            this.relativePositionJustX = 100;
            this.relativePositionJustY = 100;
            this.positionReference = null;
            this.InitDefaults();
            base.InitChartText(null, tfont, tstring, 0.0, 0.0, 0, 0, 0, 0.0);
        }

        public RelativeText(PhysicalCoordinates transform, Font tfont, string tstring)
        {
            this.relativePositionJustX = 100;
            this.relativePositionJustY = 100;
            this.positionReference = null;
            this.InitDefaults();
            base.InitChartText(transform, tfont, tstring, 0.0, 0.0, 0, 0, 0, 0.0);
        }

        public RelativeText(PhysicalCoordinates transform, Font tfont, string tstring, ChartText graphobj, int relpostypeX, int relpostypeY)
        {
            this.relativePositionJustX = 100;
            this.relativePositionJustY = 100;
            this.positionReference = null;
            this.InitDefaults();
            this.relativePositionJustX = relpostypeX;
            this.relativePositionJustY = relpostypeY;
            this.positionReference = graphobj;
            base.InitChartText(transform, tfont, tstring, 0.0, 0.0, 0, 0, 0, 0.0);
        }

        public RelativeText(PhysicalCoordinates transform, Font tfont, string tstring, ChartText graphobj, int relpostypeX, int relpostypeY, int xjust, int yjust, int rotation)
        {
            this.relativePositionJustX = 100;
            this.relativePositionJustY = 100;
            this.positionReference = null;
            this.InitDefaults();
            this.relativePositionJustX = relpostypeX;
            this.relativePositionJustY = relpostypeY;
            this.positionReference = graphobj;
            base.InitChartText(transform, tfont, tstring, 0.0, 0.0, 0, xjust, yjust, (double) rotation);
        }

        public override object Clone()
        {
            RelativeText text = new RelativeText();
            text.Copy(this);
            return text;
        }

        public void Copy(RelativeText source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.relativePositionJustX = source.relativePositionJustX;
                this.relativePositionJustY = source.relativePositionJustY;
                this.positionReference = source.positionReference;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                if ((this.relativePositionJustX != 100) && (this.positionReference != null))
                {
                    this.PositionRelativeText(g2);
                }
                base.Draw(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (base.textFont == null))
            {
                nerror = 200;
            }
            return base.ErrorCheck(nerror);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x25d;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
            base.InitChartText(base.chartObjScale, base.textFont, "", 0.0, 0.0, base.positionType, 0, 0, 0.0);
        }

        private void PositionRelativeText(Graphics g2)
        {
            Rectangle2D textBox = this.positionReference.GetTextBox();
            Point2D location = this.positionReference.GetLocation(0);
            if (this.relativePositionJustX == 0)
            {
                location.X = textBox.X;
            }
            else if (this.relativePositionJustX == 1)
            {
                location.X = textBox.X + (textBox.Width / 2.0);
            }
            else if (this.relativePositionJustX == 2)
            {
                location.X = textBox.X + textBox.Width;
            }
            if (this.relativePositionJustY == 0)
            {
                location.Y = (textBox.Y + textBox.Height) + 1.0;
            }
            else if (this.relativePositionJustY == 1)
            {
                location.Y = textBox.Y + (textBox.Height / 2.0);
            }
            else if (this.relativePositionJustY == 2)
            {
                location.Y = textBox.Y;
            }
            this.SetLocation(location.X, location.Y, 0);
        }

        public ChartText PositionReference
        {
            get
            {
                return this.positionReference;
            }
            set
            {
                this.positionReference = value;
            }
        }

        public int RelativePositionJustX
        {
            get
            {
                return this.relativePositionJustX;
            }
            set
            {
                this.relativePositionJustX = value;
            }
        }

        public int RelativePositionJustY
        {
            get
            {
                return this.relativePositionJustY;
            }
            set
            {
                this.relativePositionJustY = value;
            }
        }
    }
}

