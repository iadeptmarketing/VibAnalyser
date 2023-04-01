namespace com.quinncurtis.chart2dnet
{
    using System;

    public abstract class Scale : ChartObj
    {
        internal double scaleStart = 0.0;
        internal double scaleStop = 1.0;

        public abstract double CoordinateAdd(double source, double addvalue1);
        public void Copy(Scale source)
        {
            if (source != null)
            {
                this.scaleStart = source.scaleStart;
                this.scaleStop = source.scaleStop;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            int num = nerror;
            if ((nerror == 0) && (this.scaleStart == this.scaleStop))
            {
                num = 30;
            }
            return num;
        }

        public abstract AutoScale GetCompatibleAutoScale();
        public abstract Axis GetCompatibleAxis();
        public double GetMax()
        {
            return Math.Max(this.scaleStart, this.scaleStop);
        }

        public double GetMidpoint()
        {
            return ((this.scaleStop + this.scaleStart) / 2.0);
        }

        public double GetMin()
        {
            return Math.Min(this.scaleStart, this.scaleStop);
        }

        public double GetRange()
        {
            return (this.scaleStop - this.scaleStart);
        }

        public double GetScaleStart()
        {
            return this.scaleStart;
        }

        public double GetScaleStop()
        {
            return this.scaleStop;
        }

        public abstract double PhysToWorkingScale(double v);
        public void SetRangeFromStart(double range)
        {
            this.scaleStop = this.scaleStart + range;
        }

        public void SetRangeFromStop(double range)
        {
            this.scaleStart = this.scaleStop - range;
        }

        public void SetScale(double rstart, double rstop)
        {
            this.scaleStart = rstart;
            this.scaleStop = rstop;
        }

        public void SetScaleStart(double rstart)
        {
            this.scaleStart = rstart;
        }

        public void SetScaleStop(double rstop)
        {
            this.scaleStop = rstop;
        }

        public abstract double WorkingToPhysScale(double v);

        public double ScaleStart
        {
            get
            {
                return this.scaleStart;
            }
            set
            {
                this.scaleStart = value;
            }
        }

        public double ScaleStop
        {
            get
            {
                return this.scaleStop;
            }
            set
            {
                this.scaleStop = value;
            }
        }
    }
}

