namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class CustomDataToolTip : DataToolTip
    {
        private string customToolTipString;

        public CustomDataToolTip()
        {
            this.customToolTipString = "";
        }

        public CustomDataToolTip(ChartView component)
        {
            this.customToolTipString = "";
            base.chartObjComponent = component;
        }

        public override object Clone()
        {
            CustomDataToolTip tip = new CustomDataToolTip();
            tip.Copy(this);
            return tip;
        }

        public void Copy(CustomDataToolTip source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override void Draw(Graphics g2)
        {
            this.DrawCustomDataToolTip();
        }

        private void DrawCustomDataToolTip()
        {
            if ((base.GetSelectedDataset() != null) && (base.GetToolTipGraphics() != null))
            {
                base.GetNearestPoint().GetNearestPointIndex();
                string thestring = base.MakeDefaultDataToolTipString() + this.customToolTipString;
                base.TextTemplate.SetTextString(thestring);
                if (thestring.Length > 0)
                {
                    base.TextTemplate.SetChartObjScale(base.GetSelectedCoordinateSystem());
                    base.TextTemplate.SetLocation(base.GetActualCursorPosition().GetX(), base.GetActualCursorPosition().GetY(), base.GetSelectedPlotObj().GetPositionType());
                    base.TextTemplate.SetChartObjEnable(1);
                    base.TextTemplate.Draw(base.GetToolTipGraphics());
                    base.TooltipActive = true;
                }
            }
        }

        public string CustomToolTipString
        {
            get
            {
                return this.customToolTipString;
            }
            set
            {
                this.customToolTipString = value;
            }
        }
    }
}

