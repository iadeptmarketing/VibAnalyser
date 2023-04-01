namespace com.quinncurtis.chart2dnet
{
    using System;

    public abstract class SimplePlot : ChartPlot
    {
        internal SimpleDataset displayDataset = null;
        internal int fastClipOffset = 0;
        internal SimpleDataset theDataset = null;

        public SimplePlot()
        {
            this.InitDefaults();
        }

        public override bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(base.chartObjScale, this.DisplayDataset, base.coordinateSwap, testpoint, nmode, nearestpoint);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            bool flag = false;
            int ndestpostype = 1;
            ndestpostype = ChartSupport.GetCoordinateSystemType(base.chartObjScale);
            Point2D pointd = base.chartObjScale.ConvertCoord(ndestpostype, testpoint, 0);
            if (this.CalcNearestPoint(pointd, 5, np) && (np.nearestPointMinDistance < base.intersectionTestDistance))
            {
                np.nearestPointIndex += this.fastClipOffset;
                flag = true;
            }
            return flag;
        }

        public override bool CheckValidPoint(double x, double y, bool valid)
        {
            return (valid && base.chartObjScale.CheckValidPoint(x, y));
        }

        public void Copy(SimplePlot source)
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
        }

        public void SetDataset(SimpleDataset dataset)
        {
            int numsegments = 0;
            if (dataset == null)
            {
                dataset = new SimpleDataset("NULL DATASET", 0);
            }
            if (dataset != null)
            {
                numsegments = dataset.GetNumberGroups();
            }
            this.theDataset = dataset;
            base.FreeSegmentColors();
            base.ResizeSegmentAttributes(numsegments);
        }

        public SimpleDataset DisplayDataset
        {
            get
            {
                SimpleDataset displayDataset = this.displayDataset;
                if (displayDataset == null)
                {
                    displayDataset = this.theDataset;
                }
                return displayDataset;
            }
            set
            {
                this.displayDataset = value;
            }
        }

        public SimpleDataset TheDataset
        {
            get
            {
                return this.theDataset;
            }
        }
    }
}

