namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public abstract class ChartLabel : ChartText
    {
        public ChartLabel()
        {
            this.InitDefaults();
        }

        public ChartLabel(PhysicalCoordinates transform)
        {
            this.SetChartObjScale(transform);
            this.InitDefaults();
        }

        public void Copy(ChartLabel source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        private void InitDefaults()
        {
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
        }

        public virtual void MakeLabel()
        {
        }

        public void SetLabels(Font font, double rotation, Color labcolor)
        {
            base.SetTextFont(font);
            base.textRotation = rotation;
            base.chartObjAttributes.SetPrimaryColor(labcolor);
        }
    }
}

