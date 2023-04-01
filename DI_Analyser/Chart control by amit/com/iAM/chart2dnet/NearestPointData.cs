namespace com.iAM.chart2dnet
{
    using System;

    public class NearestPointData : ChartObj
    {
        internal Point2D actualPoint = new Point2D();
        internal int nearestGroupIndex = 0;
        internal Point2D nearestPoint = new Point2D();
        internal int nearestPointIndex = 0;
        internal double nearestPointMinDistance = 0.0;
        internal bool nearestPointValid = false;

        public override object Clone()
        {
            NearestPointData data = new NearestPointData();
            data.Copy(this);
            return data;
        }

        public void Copy(NearestPointData source)
        {
            this.nearestPoint.SetLocation(source.nearestPoint.GetX(), source.nearestPoint.GetY());
            this.nearestPointMinDistance = source.nearestPointMinDistance;
            this.nearestPointIndex = source.nearestPointIndex;
            this.nearestGroupIndex = source.nearestGroupIndex;
            this.nearestPointValid = source.nearestPointValid;
        }

        public Point2D GetActualPoint()
        {
            return this.actualPoint;
        }

        public int GetNearestGroupIndex()
        {
            return this.nearestGroupIndex;
        }

        public Point2D GetNearestPoint()
        {
            return this.nearestPoint;
        }

        public int GetNearestPointIndex()
        {
            return this.nearestPointIndex;
        }

        public double GetNearestPointMinDistance()
        {
            return this.nearestPointMinDistance;
        }

        public bool GetNearestPointValid()
        {
            return this.nearestPointValid;
        }

        public Point2D ActualPoint
        {
            get
            {
                return this.actualPoint;
            }
            set
            {
                this.actualPoint = value;
            }
        }

        public int NearestGroupIndex
        {
            get
            {
                return this.nearestGroupIndex;
            }
            set
            {
                this.nearestGroupIndex = value;
            }
        }

        public Point2D NearestPoint
        {
            get
            {
                return this.nearestPoint;
            }
            set
            {
                this.nearestPoint = value;
            }
        }

        public int NearestPointIndex
        {
            get
            {
                return this.nearestPointIndex;
            }
            set
            {
                this.nearestPointIndex = value;
            }
        }

        public double NearestPointMinDistance
        {
            get
            {
                return this.nearestPointMinDistance;
            }
            set
            {
                this.nearestPointMinDistance = value;
            }
        }

        public bool NearestPointValid
        {
            get
            {
                return this.nearestPointValid;
            }
            set
            {
                this.nearestPointValid = value;
            }
        }
    }
}

