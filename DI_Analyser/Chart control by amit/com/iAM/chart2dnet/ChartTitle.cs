namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class ChartTitle : ChartText
    {
        internal double titleOffset;
        internal int titlePosition;
        internal int titleType;

        public ChartTitle()
        {
            this.titleOffset = 2.0;
            this.titleType = 0;
            this.titlePosition = 0;
            this.InitDefaults();
        }

        public ChartTitle(PhysicalCoordinates transform, Font tfont, string tstring)
        {
            this.titleOffset = 2.0;
            this.titleType = 0;
            this.titlePosition = 0;
            this.InitDefaults();
            base.InitChartText(transform, tfont, tstring, 0.0, 0.0, 1, 1, 0, 0.0);
        }

        public ChartTitle(PhysicalCoordinates transform, Font tfont, string tstring, int ntitletype, int ntitlepos)
        {
            this.titleOffset = 2.0;
            this.titleType = 0;
            this.titlePosition = 0;
            this.InitDefaults();
            base.InitChartText(transform, tfont, tstring, 0.0, 0.0, 1, 1, 0, 0.0);
            this.titleType = ntitletype;
            this.titlePosition = ntitlepos;
        }

        public override object Clone()
        {
            ChartTitle title = new ChartTitle();
            title.Copy(this);
            return title;
        }

        public void Copy(ChartTitle source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.titleOffset = source.titleOffset;
                this.titleType = source.titleType;
                this.titlePosition = source.titlePosition;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PositionChartTitle();
                base.Draw(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public double GetTitleOffset()
        {
            return this.titleOffset;
        }

        public int GetTitlePosition()
        {
            return this.titlePosition;
        }

        public int GetTitleType()
        {
            return this.titleType;
        }

        public void InitChartTitle(PhysicalCoordinates transform, Font tfont, string tstring, int ntitletype, int ntitlepos)
        {
            this.InitDefaults();
            base.InitChartText(transform, tfont, tstring, 0.0, 0.0, 1, 1, 0, 0.0);
            this.titleType = ntitletype;
            this.titlePosition = ntitlepos;
        }

        private void InitDefaults()
        {
            base.chartObjType = 700;
            base.moveableType = 0;
            base.chartObjClipping = 1;
        }

        private void PositionChartTitle()
        {
            double x = 0.0;
            Rectangle2D graphRect = base.chartObjScale.GetGraphRect();
            Rectangle2D plotRect = base.chartObjScale.GetPlotRect();
            switch (this.titleType)
            {
                case 0:
                    base.xJust = 1;
                    base.yJust = 2;
                    if (this.titlePosition != 0)
                    {
                        x = plotRect.GetX() + (plotRect.GetWidth() / 2.0);
                        break;
                    }
                    x = graphRect.GetX() + (graphRect.GetWidth() / 2.0);
                    break;

                case 1:
                    base.xJust = 1;
                    base.yJust = 0;
                    if (this.titlePosition != 0)
                    {
                        x = plotRect.GetX() + (plotRect.GetWidth() / 2.0);
                    }
                    else
                    {
                        x = graphRect.GetX() + (graphRect.GetWidth() / 2.0);
                    }
                    this.SetLocation(x, -this.titleOffset + plotRect.GetY(), 0);
                    return;

                case 2:
                    base.xJust = 1;
                    base.yJust = 0;
                    if (this.titlePosition != 0)
                    {
                        x = plotRect.GetX() + (plotRect.GetWidth() / 2.0);
                    }
                    else
                    {
                        x = graphRect.GetX() + (graphRect.GetWidth() / 2.0);
                    }
                    this.SetLocation(x, (graphRect.GetHeight() - this.titleOffset) + graphRect.GetY(), 0);
                    return;

                default:
                    return;
            }
            this.SetLocation(x, this.titleOffset + graphRect.GetY(), 0);
        }

        public void SetTitleOffset(double offset)
        {
            this.titleOffset = offset;
        }

        public void SetTitlePosition(int ntitlepos)
        {
            this.titlePosition = ntitlepos;
        }

        public void SetTitleType(int ntitletype)
        {
            this.titleType = ntitletype;
        }

        public double TitleOffset
        {
            get
            {
                return this.titleOffset;
            }
            set
            {
                this.titleOffset = value;
            }
        }

        public int TitlePosition
        {
            get
            {
                return this.titlePosition;
            }
            set
            {
                this.titlePosition = value;
            }
        }

        public int TitleType
        {
            get
            {
                return this.titleType;
            }
            set
            {
                this.titleType = value;
            }
        }
    }
}

