namespace com.iAM.chart2dnet
{
    using System;

    public class LogScale : Scale
    {
        public LogScale()
        {
            this.InitDefaults();
        }

        public LogScale(double rstart, double rstop)
        {
            this.InitDefaults();
            base.SetScale(rstart, rstop);
        }

        private void AdjustLogScaleForCommonErrors()
        {
            if (base.scaleStart <= 0.0)
            {
                base.scaleStart = 1.0;
            }
            if (base.scaleStop <= 0.0)
            {
                base.scaleStop = 10.0;
            }
            if (base.scaleStart == base.scaleStop)
            {
                base.scaleStop = base.scaleStart * 10.0;
            }
        }

        public override object Clone()
        {
            LogScale scale = new LogScale();
            scale.Copy(this);
            return scale;
        }

        public override double CoordinateAdd(double v, double rincrement)
        {
            return (v + rincrement);
        }

        public void Copy(LogScale source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            this.AdjustLogScaleForCommonErrors();
            return base.ErrorCheck(nerror);
        }

        public override AutoScale GetCompatibleAutoScale()
        {
            return new LogAutoScale();
        }

        public override Axis GetCompatibleAxis()
        {
            return new LogAxis();
        }

        private void InitDefaults()
        {
            base.SetScale(1.0, 100.0);
            base.chartObjType = 0x4b1;
        }

        public override double PhysToWorkingScale(double v)
        {
            v = ChartSupport.Log10Ex(v);
            return v;
        }

        public override double WorkingToPhysScale(double v)
        {
            v = ChartSupport.Antilog10Ex(v);
            return v;
        }
    }
}

