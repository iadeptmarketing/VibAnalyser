namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class StringLabel : ChartLabel
    {
        public StringLabel()
        {
            this.InitDefaults();
        }

        public StringLabel(PhysicalCoordinates transform)
        {
            this.SetChartObjScale(transform);
            this.InitDefaults();
        }

        public StringLabel(Font tfont, string tstring)
        {
            this.InitDefaults();
            base.InitChartText(null, tfont, tstring, 0.0, 0.0, 1, 0, 0, 0.0);
        }

        public StringLabel(PhysicalCoordinates transform, Font tfont, string tstring, double x, double y, int npostype)
        {
            this.InitDefaults();
            base.InitChartText(transform, tfont, tstring, x, y, npostype, 0, 0, 0.0);
        }

        public StringLabel(PhysicalCoordinates transform, Font tfont, string tstring, double x, double y, int npostype, int xjust, int yjust, double rotation)
        {
            this.InitDefaults();
            base.InitChartText(transform, tfont, tstring, x, y, npostype, xjust, yjust, rotation);
        }

        public override object Clone()
        {
            StringLabel label = new StringLabel();
            label.Copy(this);
            return label;
        }

        public void Copy(StringLabel source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                base.Draw(g2);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x260;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
        }
    }
}

