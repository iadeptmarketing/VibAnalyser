namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;

    public class DataToolTip : MouseListener
    {
        internal Point2D actualCursorPosition;
        internal int dataToolTipFormat;
        internal double hitTestThreshold;
        internal Point2D nearestDataValue;
        internal NearestPointData nearestPoint;
        internal bool nearestPointFound;
        internal string[] oHLCStrings;
        internal PhysicalCoordinates selectedCoordinateSystem;
        internal ChartDataset selectedDataset;
        internal ChartPlot selectedPlotObj;
        internal ChartText textTemplate;
        internal bool tooltipActive;
        internal Graphics toolTipGraphics;
        internal ChartSymbol toolTipSymbol;
        internal ChartLabel xValueTemplate;
        internal ChartLabel yValueTemplate;

        public DataToolTip()
        {
            this.nearestPoint = new NearestPointData();
            this.hitTestThreshold = 10.0;
            this.toolTipGraphics = null;
            this.textTemplate = new ChartText(null, null, "", 0.0, 0.0, 1);
            this.xValueTemplate = new NumericLabel(null, null, 0.0, 0.0, 0.0, 1, 1, 1);
            this.yValueTemplate = new NumericLabel(null, null, 0.0, 0.0, 0.0, 1, 1, 1);
            this.toolTipSymbol = new ChartSymbol(null, 1, new ChartAttribute(Color.Black));
            this.nearestDataValue = new Point2D();
            this.actualCursorPosition = new Point2D();
            this.selectedCoordinateSystem = null;
            this.selectedPlotObj = null;
            this.selectedDataset = null;
            this.dataToolTipFormat = 2;
            this.nearestPointFound = false;
            this.tooltipActive = false;
            this.oHLCStrings = new string[] { "", "Open", "High", "Low", "Close" };
            this.InitDefaults();
        }

        public DataToolTip(ChartView component)
        {
            this.nearestPoint = new NearestPointData();
            this.hitTestThreshold = 10.0;
            this.toolTipGraphics = null;
            this.textTemplate = new ChartText(null, null, "", 0.0, 0.0, 1);
            this.xValueTemplate = new NumericLabel(null, null, 0.0, 0.0, 0.0, 1, 1, 1);
            this.yValueTemplate = new NumericLabel(null, null, 0.0, 0.0, 0.0, 1, 1, 1);
            this.toolTipSymbol = new ChartSymbol(null, 1, new ChartAttribute(Color.Black));
            this.nearestDataValue = new Point2D();
            this.actualCursorPosition = new Point2D();
            this.selectedCoordinateSystem = null;
            this.selectedPlotObj = null;
            this.selectedDataset = null;
            this.dataToolTipFormat = 2;
            this.nearestPointFound = false;
            this.tooltipActive = false;
            this.oHLCStrings = new string[] { "", "Open", "High", "Low", "Close" };
            base.chartObjComponent = component;
            this.InitDefaults();
        }

        public DataToolTip(ChartView component, MouseButtons buttonmask)
        {
            this.nearestPoint = new NearestPointData();
            this.hitTestThreshold = 10.0;
            this.toolTipGraphics = null;
            this.textTemplate = new ChartText(null, null, "", 0.0, 0.0, 1);
            this.xValueTemplate = new NumericLabel(null, null, 0.0, 0.0, 0.0, 1, 1, 1);
            this.yValueTemplate = new NumericLabel(null, null, 0.0, 0.0, 0.0, 1, 1, 1);
            this.toolTipSymbol = new ChartSymbol(null, 1, new ChartAttribute(Color.Black));
            this.nearestDataValue = new Point2D();
            this.actualCursorPosition = new Point2D();
            this.selectedCoordinateSystem = null;
            this.selectedPlotObj = null;
            this.selectedDataset = null;
            this.dataToolTipFormat = 2;
            this.nearestPointFound = false;
            this.tooltipActive = false;
            this.oHLCStrings = new string[] { "", "Open", "High", "Low", "Close" };
            base.chartObjComponent = component;
            base.buttonMask = buttonmask;
        }

        public override object Clone()
        {
            DataToolTip tip = new DataToolTip();
            tip.Copy(this);
            return tip;
        }

        public void Copy(DataToolTip source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.hitTestThreshold = source.hitTestThreshold;
                base.enabled = source.enabled;
                base.buttonMask = source.buttonMask;
            }
        }

        public override void Draw(Graphics g2)
        {
            this.DrawDefaultDataToolTip();
        }

        private void DrawDefaultDataToolTip()
        {
            int num = 1;
            if ((this.selectedDataset != null) && (this.toolTipGraphics != null))
            {
                if (this.toolTipSymbol != null)
                {
                    num += ((int) this.toolTipSymbol.GetChartObjAttributes().GetSymbolSize()) / 2;
                    this.toolTipSymbol.SetChartObjScale(this.selectedCoordinateSystem);
                    this.toolTipSymbol.SetLocation(this.nearestDataValue.GetX(), this.nearestDataValue.GetY(), this.selectedPlotObj.GetPositionType());
                    this.toolTipSymbol.SetChartObjEnable(1);
                    this.toolTipSymbol.Draw(this.toolTipGraphics);
                }
                string thestring = this.MakeDefaultDataToolTipString();
                this.textTemplate.SetTextString(thestring);
                if (thestring.Length > 0)
                {
                    this.textTemplate.SetChartObjScale(this.selectedCoordinateSystem);
                    this.textTemplate.SetLocation(this.actualCursorPosition.GetX(), this.actualCursorPosition.GetY(), this.selectedPlotObj.GetPositionType());
                    this.textTemplate.SetChartObjEnable(1);
                    this.textTemplate.Draw(this.toolTipGraphics);
                    this.tooltipActive = true;
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public ChartPlot FindObj(Point2D testpoint)
        {
            ArrayList chartObjectsArrayList = base.chartObjComponent.GetChartObjectsArrayList();
            int count = chartObjectsArrayList.Count;
            bool flag = true;
            NearestPointData np = new NearestPointData();
            GraphObj obj2 = null;
            for (int i = 0; i < count; i++)
            {
                obj2 = (GraphObj) chartObjectsArrayList[i];
                if (((obj2 != null) && (obj2.ErrorCheck(0) == 0)) && ChartSupport.IsKindOf(obj2, "ChartPlot"))
                {
                    ChartPlot plot = (ChartPlot) obj2;
                    PhysicalCoordinates chartObjScale = plot.GetChartObjScale();
                    if (chartObjScale != null)
                    {
                        chartObjScale.ConvertCoord(plot.GetPositionType(), testpoint, 0);
                        plot.SetIntersectionTestDistance(this.hitTestThreshold);
                        if (plot.CheckIntersection(testpoint, np))
                        {
                            this.nearestPoint.Copy(np);
                            return plot;
                        }
                        if (flag)
                        {
                            flag = false;
                        }
                    }
                }
            }
            return null;
        }

        public Point2D GetActualCursorPosition()
        {
            return this.actualCursorPosition;
        }

        public int GetDataToolTipFormat()
        {
            return this.dataToolTipFormat;
        }

        public double GetHitTestThreshold()
        {
            return this.hitTestThreshold;
        }

        public Point2D GetNearestDataValue()
        {
            return this.nearestDataValue;
        }

        public NearestPointData GetNearestPoint()
        {
            return this.nearestPoint;
        }

        public string[] GetOHLCStrings()
        {
            return this.OHLCStrings;
        }

        public PhysicalCoordinates GetSelectedCoordinateSystem()
        {
            return this.selectedCoordinateSystem;
        }

        public ChartDataset GetSelectedDataset()
        {
            return this.selectedDataset;
        }

        public ChartPlot GetSelectedPlotObj()
        {
            return this.selectedPlotObj;
        }

        public ChartText GetTextTemplate()
        {
            return this.textTemplate;
        }

        public Graphics GetToolTipGraphics()
        {
            return this.toolTipGraphics;
        }

        public ChartSymbol GetToolTipSymbol()
        {
            return this.toolTipSymbol;
        }

        private string GetValueString(ChartLabel label, double value)
        {
            string textString = "";
            if (label != null)
            {
                if (ChartSupport.IsKindOf(label, "NumericLabel"))
                {
                    ((NumericLabel) label).SetNumericValue(value);
                    return ((NumericLabel) label).GetTextString();
                }
                if (ChartSupport.IsKindOf(label, "TimeLabel"))
                {
                    ((TimeLabel) label).SetTimeNumericValue(value);
                    textString = ((TimeLabel) label).GetTextString();
                }
            }
            return textString;
        }

        public ChartLabel GetXValueTemplate()
        {
            return this.xValueTemplate;
        }

        public ChartLabel GetYValueTemplate()
        {
            return this.yValueTemplate;
        }

        private void InitDefaults()
        {
            this.textTemplate.SetChartObjEnable(0);
            this.textTemplate.SetTextBgColor(Color.FromArgb(0xff, 0xff, 0xcc));
            this.textTemplate.SetTextBgMode(true);
        }

        public string MakeDefaultDataToolTipString()
        {
            string str = "";
            if (this.selectedDataset != null)
            {
                int num;
                if (this.toolTipGraphics == null)
                {
                    return str;
                }
                string valueString = this.GetValueString(this.xValueTemplate, this.nearestDataValue.GetX());
                string str3 = this.GetValueString(this.yValueTemplate, this.nearestDataValue.GetY());
                switch (this.dataToolTipFormat)
                {
                    case 0:
                        return str;

                    case 1:
                        return valueString;

                    case 2:
                        return str3;

                    case 3:
                        return (valueString + ", " + str3);

                    case 4:
                        return (valueString + "\n" + str3);

                    case 5:
                        str = "x = " + valueString;
                        num = 0;
                        while (num < this.selectedDataset.GetNumberGroups())
                        {
                            str3 = "y" + num.ToString() + " = " + this.GetValueString(this.yValueTemplate, this.selectedDataset.GetYDataValue(num, this.nearestPoint.GetNearestPointIndex()));
                            str = str + "\n" + str3;
                            num++;
                        }
                        return str;

                    case 6:
                        str = this.oHLCStrings[0] + " " + valueString;
                        for (num = 0; num < Math.Min(4, this.selectedDataset.GetNumberGroups()); num++)
                        {
                            str3 = this.oHLCStrings[num + 1] + " " + this.GetValueString(this.yValueTemplate, this.selectedDataset.GetYDataValue(num, this.nearestPoint.GetNearestPointIndex()));
                            str = str + "\n" + str3;
                        }
                        return str;
                }
            }
            return str;
        }

        public override void OnClick(EventArgs mouseevent)
        {
        }

        public override void OnDoubleClick(EventArgs mouseevent)
        {
        }

        public override void OnMouseDown(MouseEventArgs mouseevent)
        {
            Point2D testpoint = new Point2D();
            testpoint.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            if (base.enabled && ((mouseevent.Button & base.buttonMask) != MouseButtons.None))
            {
                this.selectedPlotObj = this.FindObj(testpoint);
                if (this.selectedPlotObj != null)
                {
                    this.selectedDataset = this.selectedPlotObj.GetDataset();
                    this.selectedCoordinateSystem = this.selectedPlotObj.GetChartObjScale();
                    this.actualCursorPosition = this.selectedCoordinateSystem.ConvertCoord(this.selectedPlotObj.GetPositionType(), testpoint, 0);
                    this.toolTipGraphics = base.chartObjComponent.CreateGraphics();
                    if (this.nearestPoint.nearestPointIndex >= 0)
                    {
                        this.nearestDataValue = (Point2D) this.nearestPoint.nearestPoint.Clone();
                        //if (this.dataToolTipFormat != 0)
                        {
                            this.Draw(this.toolTipGraphics);
                        }
                    }
                }
            }
        }

        public override void OnMouseMove(MouseEventArgs mouseevent)
        {
        }

        public override void OnMouseUp(MouseEventArgs mouseevent)
        {
            //if (this.dataToolTipFormat != 0)
            {
                this.ReleaseDefaultDataToolTip();
            }
        }

        private void ReleaseDefaultDataToolTip()
        {
            if (this.tooltipActive)
            {
                if (this.textTemplate != null)
                {
                    this.textTemplate.SetTextString("");
                    this.textTemplate.SetChartObjEnable(0);
                }
                if (this.toolTipSymbol != null)
                {
                    this.toolTipSymbol.SetChartObjEnable(0);
                }
                base.chartObjComponent.UpdateDraw();
                if (this.toolTipGraphics != null)
                {
                    this.toolTipGraphics.Dispose();
                }
                this.toolTipGraphics = null;
                this.tooltipActive = false;
            }
        }

        public void SetDataToolTipFormat(int format)
        {
            this.dataToolTipFormat = format;
        }

        public void SetHitTestThreshold(double nearvalue)
        {
            this.hitTestThreshold = nearvalue;
        }

        public void SetOHLCStrings(string[] ohlcstrings)
        {
            this.OHLCStrings = ohlcstrings;
        }

        public void SetTextTemplate(ChartText texttemplate)
        {
            if (texttemplate != null)
            {
                this.textTemplate = (ChartText) texttemplate.Clone();
            }
        }

        public void SetToolTipSymbol(ChartSymbol symbol)
        {
            if (symbol != null)
            {
                this.toolTipSymbol = (ChartSymbol) symbol.Clone();
            }
        }

        public void SetXValueTemplate(ChartLabel xvalue1template)
        {
            if (xvalue1template != null)
            {
                this.xValueTemplate = (ChartLabel) xvalue1template.Clone();
            }
        }

        public void SetYValueTemplate(ChartLabel yvalue1template)
        {
            if (yvalue1template != null)
            {
                this.yValueTemplate = (ChartLabel) yvalue1template.Clone();
            }
        }

        public int DataToolTipFormat
        {
            get
            {
                return this.dataToolTipFormat;
            }
            set
            {
                this.dataToolTipFormat = value;
            }
        }

        public double HitTestThreshold
        {
            get
            {
                return this.hitTestThreshold;
            }
            set
            {
                this.hitTestThreshold = value;
            }
        }

        public string[] OHLCStrings
        {
            get
            {
                return this.oHLCStrings;
            }
            set
            {
                this.oHLCStrings = value;
            }
        }

        public ChartText TextTemplate
        {
            get
            {
                return this.textTemplate;
            }
            set
            {
                if (value != null)
                {
                    this.textTemplate = (ChartText) value.Clone();
                }
            }
        }

        public bool TooltipActive
        {
            get
            {
                return this.tooltipActive;
            }
            set
            {
                this.tooltipActive = value;
            }
        }

        public ChartSymbol ToolTipSymbol
        {
            get
            {
                return this.toolTipSymbol;
            }
            set
            {
                if (value != null)
                {
                    this.toolTipSymbol = (ChartSymbol) value.Clone();
                }
            }
        }

        public ChartLabel XValueTemplate
        {
            get
            {
                return this.xValueTemplate;
            }
            set
            {
                if (value != null)
                {
                    this.xValueTemplate = (ChartLabel) value.Clone();
                }
            }
        }

        public ChartLabel YValueTemplate
        {
            get
            {
                return this.yValueTemplate;
            }
            set
            {
                if (value != null)
                {
                    this.yValueTemplate = (ChartLabel) value.Clone();
                }
            }
        }
    }
}

