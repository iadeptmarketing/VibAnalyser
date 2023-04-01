namespace com.quinncurtis.chart2dnet
{
    using System;

    public abstract class GroupPlot : ChartPlot
    {
        internal com.quinncurtis.chart2dnet.GroupDataset displayDataset = null;
        internal int fastClipOffset = 0;
        internal com.quinncurtis.chart2dnet.GroupDataset groupDataset = null;
        internal bool stackMode = false;

        public GroupPlot()
        {
            this.InitDefaults();
        }

        protected DoubleArray2D CalcGroupYSumArray(DoubleArray xvalues, DoubleArray2D yvalues, BoolArray valid)
        {
            int numRows = yvalues.GetNumRows();
            int length = xvalues.Length;
            DoubleArray2D arrayd = new DoubleArray2D(numRows, length);
            DoubleArray array = new DoubleArray(length);
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (this.CheckValidPoint(xvalues[j], yvalues[i][j], valid[j]))
                    {
                        DoubleArray array2;
                        int num5;
                        (array2 = array)[num5 = j] = array2[num5] + yvalues[i][j];
                        (array2 = arrayd[i])[num5 = j] = array2[num5] + array[j];
                    }
                }
            }
            return arrayd;
        }

        public override bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(base.chartObjScale, this.groupDataset, base.coordinateSwap, testpoint, nmode, nearestpoint);
        }

        public void Copy(GroupPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.groupDataset != null)
                {
                    this.groupDataset = new com.quinncurtis.chart2dnet.GroupDataset();
                    this.groupDataset.Copy(source.groupDataset);
                }
                this.stackMode = source.stackMode;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.groupDataset == null)
                {
                    nerror = 510;
                }
                if (nerror == 0)
                {
                    nerror = this.groupDataset.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public override ChartDataset GetDataset()
        {
            return this.groupDataset;
        }

        public com.quinncurtis.chart2dnet.GroupDataset GetGroupDataset()
        {
            return this.groupDataset;
        }

        public bool GetStackMode()
        {
            return this.stackMode;
        }

        private void InitDefaults()
        {
            base.chartObjClipping = 2;
            base.moveableType = 0;
        }

        public void SetDataset(com.quinncurtis.chart2dnet.GroupDataset dataset)
        {
            int numsegments = 0;
            if (dataset == null)
            {
                dataset = new com.quinncurtis.chart2dnet.GroupDataset("NULL DATASET", 1, 0);
            }
            if (dataset != null)
            {
                numsegments = dataset.GetNumberGroups();
            }
            this.groupDataset = dataset;
            base.ResizeSegmentAttributes(numsegments);
        }

        public void SetGroupDataset(com.quinncurtis.chart2dnet.GroupDataset dataset)
        {
            int numsegments = 0;
            if (dataset != null)
            {
                numsegments = dataset.GetNumberGroups();
            }
            this.groupDataset = dataset;
            if (dataset != null)
            {
                base.ResizeSegmentAttributes(numsegments);
            }
        }

        public com.quinncurtis.chart2dnet.GroupDataset DisplayDataset
        {
            get
            {
                com.quinncurtis.chart2dnet.GroupDataset displayDataset = this.displayDataset;
                if (displayDataset == null)
                {
                    displayDataset = this.groupDataset;
                }
                return displayDataset;
            }
            set
            {
                this.displayDataset = value;
            }
        }

        public com.quinncurtis.chart2dnet.GroupDataset GroupDataset
        {
            get
            {
                return this.groupDataset;
            }
        }

        public bool StackMode
        {
            get
            {
                return this.stackMode;
            }
            set
            {
                this.stackMode = value;
            }
        }
    }
}

