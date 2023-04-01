namespace com.quinncurtis.chart2dnet
{
    using System;

    public class LinearScale : Scale
    {
        public LinearScale()
        {
            this.InitDefaults();
        }

        public LinearScale(double rstart, double rstop)
        {
            this.InitDefaults();
            base.SetScale(rstart, rstop);
        }

        private void AdjustLinearScaleForCommonErrors()
        {
            if (base.scaleStart == base.scaleStop)
            {
                if (base.scaleStart == 0.0)
                {
                    base.scaleStop = 1.0;
                }
                else if (base.scaleStart < 0.0)
                {
                    base.scaleStop = 0.0;
                }
                else if (base.scaleStart > 0.0)
                {
                    base.scaleStart = 0.0;
                }
            }
        }

        public override object Clone()
        {
            LinearScale scale = new LinearScale();
            scale.Copy(this);
            return scale;
        }

        public override double CoordinateAdd(double v, double rincrement)
        {
            return (v + rincrement);
        }

        public void Copy(LinearScale source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            this.AdjustLinearScaleForCommonErrors();
            return base.ErrorCheck(nerror);
        }

        public override AutoScale GetCompatibleAutoScale()
        {
            return new LinearAutoScale();
        }

        public override Axis GetCompatibleAxis()
        {
            return new LinearAxis();
        }

        private void InitDefaults()
        {
            base.SetScale(0.0, 100.0);
            base.chartObjType = 0x4b0;
        }

        public override double PhysToWorkingScale(double v)
        {
            return v;
        }

        public override double WorkingToPhysScale(double v)
        {
            return v;
        }
    }
}

