namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public abstract class PolarPlot : ChartPlot
    {
        internal SimpleDataset theDataset = null;

        public PolarPlot()
        {
            this.InitDefaults();
        }

        public override bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(base.chartObjScale, this.theDataset, base.coordinateSwap, testpoint, nmode, nearestpoint);
        }

        public override bool CheckValidPoint(double x, double y, bool valid)
        {
            return (valid && base.chartObjScale.CheckValidPoint(x, y));
        }

        public void Copy(PolarPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.theDataset != null)
                {
                    this.theDataset = new SimpleDataset();
                    this.theDataset.Copy(source.theDataset);
                }
            }
        }

        public abstract override void Draw(Graphics g2);
        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.theDataset == null)
                {
                    nerror = 530;
                }
                if (nerror == 0)
                {
                    nerror = this.theDataset.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public override ChartDataset GetDataset()
        {
            return this.theDataset;
        }

        private void InitDefaults()
        {
            base.chartObjClipping = 2;
            base.moveableType = 2;
            base.positionType = 2;
        }

        public void SetDataset(SimpleDataset dataset)
        {
            this.theDataset = dataset;
            base.FreeSegmentColors();
        }

        public SimpleDataset TheDataset
        {
            get
            {
                return this.theDataset;
            }
            set
            {
                this.theDataset = value;
                base.FreeSegmentColors();
            }
        }
    }
}

