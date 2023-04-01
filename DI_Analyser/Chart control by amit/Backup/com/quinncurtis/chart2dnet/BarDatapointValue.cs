namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class BarDatapointValue : NumericLabel
    {
        internal Rectangle2D barRect;
        internal Point2D datapointLocation;
        internal ChartPlot plotObj;

        public BarDatapointValue()
        {
            this.plotObj = new SimpleBarPlot();
            this.datapointLocation = new Point2D();
            this.InitDefaults();
        }

        public BarDatapointValue(NumericLabel numlabel, ChartPlot plotobj, Point2D datapointloc, Rectangle2D barrect)
        {
            this.plotObj = new SimpleBarPlot();
            this.datapointLocation = new Point2D();
            this.InitBarDatapointValue(numlabel, plotobj, datapointloc, barrect);
        }

        public override object Clone()
        {
            BarDatapointValue value2 = new BarDatapointValue();
            value2.Copy(this);
            return value2;
        }

        public void Copy(BarDatapointValue source)
        {
            if (source != null)
            {
                if (source.barRect != null)
                {
                    this.barRect = (Rectangle2D) source.barRect.Clone();
                }
                this.plotObj = source.plotObj;
                if (source.datapointLocation != null)
                {
                    this.datapointLocation = (Point2D) source.datapointLocation.Clone();
                }
                base.Copy(source);
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawBarDatapointValue(g2, this.datapointLocation);
            }
        }

        private void DrawBarDatapointValue(Graphics g2, Point2D datapointpos)
        {
            double x = datapointpos.GetX();
            double y = datapointpos.GetY();
            Rectangle2D rectangled = (Rectangle2D) this.barRect.Clone();
            double num3 = 0.0;
            if (this.plotObj.GetBarDatapointLabelPosition() == 7)
            {
                base.SetXJust(1);
                base.SetYJust(1);
                if (this.plotObj.GetBarOrient() == 1)
                {
                    num3 = y;
                }
                else
                {
                    num3 = x;
                }
                this.DrawDatapointValue(g2, rectangled.GetX() + (rectangled.GetWidth() / 2.0), rectangled.GetY() + (rectangled.GetHeight() / 2.0), num3);
            }
            else if (this.plotObj.GetBarOrient() == 1)
            {
                if (((y < this.plotObj.GetFillBaseValue()) && (this.plotObj.GetBarDatapointLabelPosition() == 6)) || ((y > this.plotObj.GetFillBaseValue()) && (this.plotObj.GetBarDatapointLabelPosition() == 5)))
                {
                    if (base.chartObjScale.ScaleInverted(1))
                    {
                        base.SetXJust(1);
                        base.SetYJust(0);
                        base.SetTextNudge(0.0, -4.0);
                    }
                    else
                    {
                        base.SetXJust(1);
                        base.SetYJust(2);
                        base.SetTextNudge(0.0, 4.0);
                    }
                }
                else if (base.chartObjScale.ScaleInverted(1))
                {
                    base.SetXJust(1);
                    base.SetYJust(2);
                    base.SetTextNudge(0.0, 4.0);
                }
                else
                {
                    base.SetXJust(1);
                    base.SetYJust(0);
                    base.SetTextNudge(0.0, -4.0);
                }
                this.DrawDatapointValue(g2, rectangled.GetX() + (rectangled.GetWidth() / 2.0), rectangled.GetY() + rectangled.GetHeight(), y);
            }
            else
            {
                if (((x < this.plotObj.GetFillBaseValue()) && (this.plotObj.GetBarDatapointLabelPosition() == 6)) || ((x > this.plotObj.GetFillBaseValue()) && (this.plotObj.GetBarDatapointLabelPosition() == 5)))
                {
                    if (base.chartObjScale.ScaleInverted(0))
                    {
                        base.SetXJust(0);
                        base.SetYJust(1);
                        base.SetTextNudge(4.0, 0.0);
                    }
                    else
                    {
                        base.SetXJust(2);
                        base.SetYJust(1);
                        base.SetTextNudge(-4.0, 0.0);
                    }
                }
                else if (base.chartObjScale.ScaleInverted(0))
                {
                    base.SetXJust(2);
                    base.SetYJust(1);
                    base.SetTextNudge(-4.0, 0.0);
                }
                else
                {
                    base.SetXJust(0);
                    base.SetYJust(1);
                    base.SetTextNudge(4.0, 0.0);
                }
                if (ChartSupport.IsKindOf(this.plotObj, "GroupPlot"))
                {
                    this.DrawDatapointValue(g2, rectangled.GetX() + rectangled.GetWidth(), rectangled.GetY() + (rectangled.GetHeight() / 2.0), y);
                }
                else
                {
                    this.DrawDatapointValue(g2, rectangled.GetX() + rectangled.GetWidth(), rectangled.GetY() + (rectangled.GetHeight() / 2.0), x);
                }
            }
        }

        public void DrawDatapointValue(Graphics g2, double xpos, double ypos, double value)
        {
            this.SetChartObjScale(base.chartObjScale);
            this.SetLocation(xpos, ypos, 1);
            base.SetNumericValue(value);
            base.Draw(g2);
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.barRect == null)
                {
                    nerror = 0x2bd;
                }
                else if (this.plotObj == null)
                {
                    nerror = 700;
                }
                else
                {
                    nerror = this.plotObj.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        public void InitBarDatapointValue(NumericLabel numlabel, ChartPlot plotobj, Point2D datapointloc, Rectangle2D barrect)
        {
            this.InitDefaults();
            base.Copy(numlabel);
            this.barRect = (Rectangle2D) barrect.Clone();
            this.plotObj = plotobj;
            this.datapointLocation = (Point2D) datapointloc.Clone();
            this.SetChartObjScale(this.plotObj.GetChartObjScale());
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x261;
            base.chartObjClipping = 1;
            base.moveableType = 1;
            base.positionType = 1;
        }
    }
}

