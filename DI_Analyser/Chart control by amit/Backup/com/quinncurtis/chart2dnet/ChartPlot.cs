namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class ChartPlot : GraphObj
    {
        internal int barDatapointLabelPosition = 6;
        internal int barJust = 0;
        internal int barOrient = 1;
        internal double barWidth = 1.0;
        internal bool coordinateSwap = false;
        internal int fastClipMode = 2;
        internal double fillBaseValue = 0.0;
        internal int numSegments = 0;
        internal NumericLabel plotLabelTemplate = new NumericLabel();
        internal ArrayList segmentAttributesArrayList = null;
        internal bool segmentColorMode = false;
        internal bool showDatapointValue = false;
        internal int stepMode = 0;

        public ChartPlot()
        {
            this.InitDefaults();
        }

        public abstract bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint);
        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            bool flag = false;
            int ndestpostype = 1;
            ndestpostype = ChartSupport.GetCoordinateSystemType(base.chartObjScale);
            Point2D pointd = base.chartObjScale.ConvertCoord(ndestpostype, testpoint, 0);
            this.GetDataset().CalcNearestPoint(base.chartObjScale, pointd, 5, np);
            if (np.nearestPointMinDistance <= base.intersectionTestDistance)
            {
                flag = true;
            }
            return flag;
        }

        public virtual bool CheckValidPoint(double x, double y, bool valid)
        {
            return (valid && base.chartObjScale.CheckValidPoint(x, y));
        }

        public void Copy(ChartPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.fillBaseValue = source.fillBaseValue;
                this.barWidth = source.barWidth;
                this.barOrient = source.barOrient;
                this.barJust = source.barJust;
                this.numSegments = source.numSegments;
                this.segmentColorMode = source.segmentColorMode;
                this.barDatapointLabelPosition = source.barDatapointLabelPosition;
                this.plotLabelTemplate = (NumericLabel) source.plotLabelTemplate.Clone();
                this.showDatapointValue = source.showDatapointValue;
                this.stepMode = source.stepMode;
                this.fastClipMode = source.fastClipMode;
                if (source.segmentAttributesArrayList != null)
                {
                    this.segmentAttributesArrayList = (ArrayList) source.segmentAttributesArrayList.Clone();
                    for (int i = 0; i < this.segmentAttributesArrayList.Count; i++)
                    {
                        this.segmentAttributesArrayList[i] = (ChartAttribute) ((ChartAttribute) this.segmentAttributesArrayList[i]).Clone();
                    }
                }
            }
        }

        public int CreateLineFillArrays(double[] xdest, double[] ydest, double[] xsource, double[] ysource, bool[] valid, int numpoints, int norient)
        {
            int index = 0;
            int num2 = 0;
            for (int i = 0; i < numpoints; i++)
            {
                if (this.CheckValidPoint(xsource[i], ysource[i], valid[i]))
                {
                    xdest[index] = xsource[i];
                    ydest[index] = ysource[i];
                    index++;
                }
            }
            num2 = index;
            if (num2 > 1)
            {
                if (norient == 1)
                {
                    xdest[num2] = xdest[num2 - 1];
                    ydest[num2] = this.fillBaseValue;
                    xdest[num2 + 1] = xdest[0];
                    ydest[num2 + 1] = this.fillBaseValue;
                    xdest[num2 + 2] = xdest[0];
                    ydest[num2 + 2] = ydest[0];
                    return num2;
                }
                ydest[num2] = ydest[num2 - 1];
                xdest[num2] = this.fillBaseValue;
                ydest[num2 + 1] = ydest[0];
                xdest[num2 + 1] = this.fillBaseValue;
                ydest[num2 + 2] = ydest[0];
                xdest[num2 + 2] = xdest[0];
            }
            return num2;
        }

        public virtual void DrawBarDatapointValue(Graphics g2, double x, double y, Rectangle2D barrect)
        {
            BarDatapointValue value2 = new BarDatapointValue();
            Point2D datapointloc = new Point2D();
            datapointloc.SetLocation(x, y);
            value2.InitBarDatapointValue(this.GetPlotLabelTemplate(), this, datapointloc, barrect);
            value2.SetChartObjEnable(this.GetChartObjEnable());
            value2.Draw(g2);
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            base.chartObjScale.SetClippingArea(base.chartObjClipping);
        }

        public virtual void DrawSimpleDatapointValue(Graphics g2, double x, double y, double displayvalue1)
        {
            NumericLabel plotLabelTemplate = this.GetPlotLabelTemplate();
            plotLabelTemplate.SetLocation(x, y);
            plotLabelTemplate.SetNumericValue(displayvalue1);
            plotLabelTemplate.Draw(g2);
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            base.chartObjScale.SetClippingArea(base.chartObjClipping);
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.plotLabelTemplate == null))
            {
                nerror = 700;
            }
            return base.ErrorCheck(nerror);
        }

        public void FreeSegmentColors()
        {
            this.segmentAttributesArrayList = null;
            this.segmentColorMode = false;
            this.numSegments = 0;
        }

        public int GetBarDatapointLabelPosition()
        {
            return this.barDatapointLabelPosition;
        }

        public int GetBarJust()
        {
            return this.barJust;
        }

        public int GetBarOrient()
        {
            return this.barOrient;
        }

        public double GetBarWidth()
        {
            return this.barWidth;
        }

        public bool GetCoordinateSwap()
        {
            return this.coordinateSwap;
        }

        public abstract ChartDataset GetDataset();
        public int GetFastClipMode()
        {
            return this.fastClipMode;
        }

        public double GetFillBaseValue()
        {
            return this.fillBaseValue;
        }

        public int GetLabelTemplateDecimalPos()
        {
            return this.plotLabelTemplate.GetDecimalPos();
        }

        public int GetLabelTemplateNumericFormat()
        {
            return this.plotLabelTemplate.GetNumericFormat();
        }

        public NumericLabel GetPlotLabelTemplate()
        {
            this.plotLabelTemplate.SetChartObjScale(this.GetChartObjScale());
            this.plotLabelTemplate.SetResizeMultiplier(this.GetResizeMultiplier());
            return this.plotLabelTemplate;
        }

        public ChartAttribute GetSegmentAttributes(int nsegment)
        {
            ChartAttribute chartObjAttributes = base.chartObjAttributes;
            if (((this.segmentAttributesArrayList != null) && (nsegment >= 0)) && (nsegment < this.numSegments))
            {
                chartObjAttributes = (ChartAttribute) this.segmentAttributesArrayList[nsegment];
            }
            return chartObjAttributes;
        }

        public bool GetSegmentAttributesMode()
        {
            return this.segmentColorMode;
        }

        public bool GetShowDatapointValue()
        {
            return this.showDatapointValue;
        }

        public int GetStepMode()
        {
            return this.stepMode;
        }

        private void InitDefaults()
        {
            base.chartObjClipping = 1;
        }

        public void InitSegmentAttributes()
        {
            int numberDatapoints = this.GetDataset().GetNumberDatapoints();
            this.InitSegmentAttributes(this.GetChartObjAttributes(), numberDatapoints);
        }

        public void InitSegmentAttributes(ChartAttribute attrib)
        {
            int numberDatapoints = this.GetDataset().GetNumberDatapoints();
            this.InitSegmentAttributes(attrib, numberDatapoints);
        }

        public void InitSegmentAttributes(ChartAttribute attribs, GroupDataset dataset)
        {
            int nnumsegments = 0;
            if (dataset != null)
            {
                nnumsegments = dataset.GetNumberDatapoints();
            }
            this.InitSegmentAttributes(attribs, nnumsegments);
        }

        public void InitSegmentAttributes(ChartAttribute[] attribs, GroupDataset dataset)
        {
            int nnumsegments = 0;
            if (dataset != null)
            {
                nnumsegments = dataset.GetNumberGroups();
            }
            else if (attribs != null)
            {
                nnumsegments = attribs.Length;
            }
            this.InitSegmentAttributes(attribs, nnumsegments);
        }

        public void InitSegmentAttributes(ChartAttribute[] attribs, int nnumsegments)
        {
            this.numSegments = nnumsegments;
            this.segmentAttributesArrayList = new ArrayList(this.numSegments);
            for (int i = 0; i < this.numSegments; i++)
            {
                ChartAttribute attribute = new ChartAttribute(attribs[i]);
                this.segmentAttributesArrayList.Add(attribute);
            }
            this.segmentColorMode = true;
        }

        public void InitSegmentAttributes(ChartAttribute attrib, int nnumsegments)
        {
            this.numSegments = nnumsegments;
            this.segmentAttributesArrayList = new ArrayList(this.numSegments);
            for (int i = 0; i < this.numSegments; i++)
            {
                ChartAttribute attribute = new ChartAttribute(attrib);
                this.segmentAttributesArrayList.Add(attribute);
            }
            this.segmentColorMode = true;
        }

        public override void PrePlot(Graphics g2)
        {
            base.thePath = new GraphicsPath();
            base.chartObjScale.ChartTransform(g2);
            base.chartObjScale.SetClippingArea(base.chartObjClipping);
        }

        public void ResizeSegmentAttributes(int numsegments)
        {
            if (numsegments != this.numSegments)
            {
                ArrayList list = new ArrayList(numsegments);
                for (int i = 0; i < numsegments; i++)
                {
                    ChartAttribute attribute;
                    if (i < this.numSegments)
                    {
                        attribute = (ChartAttribute) this.segmentAttributesArrayList[i];
                    }
                    else
                    {
                        attribute = new ChartAttribute();
                    }
                    list.Add(attribute);
                }
                this.numSegments = numsegments;
                this.segmentAttributesArrayList = list;
            }
        }

        public void SegmentAttributesSet(int index)
        {
            if (this.segmentColorMode && (this.segmentAttributesArrayList == null))
            {
                this.InitSegmentAttributes();
            }
            if (this.segmentColorMode && (this.segmentAttributesArrayList != null))
            {
                if ((index >= 0) && (index < this.numSegments))
                {
                    base.chartObjScale.SetCurrentAttributes(this.GetSegmentAttributes(index));
                }
                else
                {
                    base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                }
            }
            else
            {
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            }
        }

        public void SegmentSymbolAttributesSet(int index, ChartSymbol chartsymbol)
        {
            ChartAttribute attr = (ChartAttribute) base.chartObjAttributes.Clone();
            if (this.segmentColorMode && (this.segmentAttributesArrayList == null))
            {
                this.InitSegmentAttributes();
            }
            if (this.segmentColorMode && (this.segmentAttributesArrayList != null))
            {
                if ((index >= 0) && (index < this.numSegments))
                {
                    attr = this.GetSegmentAttributes(index);
                    chartsymbol.SetChartObjAttributes(attr);
                    base.chartObjScale.SetCurrentAttributes(attr);
                }
                else
                {
                    base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                }
            }
            else
            {
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            }
        }

        public void SetBarDatapointLabelPosition(int pos)
        {
            this.barDatapointLabelPosition = pos;
        }

        public void SetBarJust(int barjust)
        {
            this.barJust = barjust;
        }

        public void SetBarOrient(int barorient)
        {
            this.barOrient = barorient;
        }

        public void SetBarWidth(double rwidth)
        {
            this.barWidth = Math.Abs(rwidth);
        }

        protected virtual void SetCoordinateSwap(bool swap)
        {
            this.coordinateSwap = swap;
        }

        public void SetFastClipMode(int fastclip)
        {
            this.fastClipMode = fastclip;
        }

        public void SetFillBaseValue(double rbase)
        {
            this.fillBaseValue = rbase;
        }

        public void SetLabelTemplateDecimalPos(int ndecplace)
        {
            this.plotLabelTemplate.SetDecimalPos(ndecplace);
        }

        public void SetLabelTemplateNumericFormat(int nformat)
        {
            this.plotLabelTemplate.SetNumericFormat(nformat);
        }

        public void SetPlotLabelTemplate(NumericLabel numlabel)
        {
            if (numlabel != null)
            {
                this.plotLabelTemplate = (NumericLabel) numlabel.Clone();
                this.plotLabelTemplate.SetChartObjScale(this.GetChartObjScale());
            }
        }

        public void SetSegmentAttributes(int nsegment, ChartAttribute attrib)
        {
            if (this.segmentAttributesArrayList == null)
            {
                this.InitSegmentAttributes();
            }
            if ((nsegment >= 0) && (nsegment < this.numSegments))
            {
                this.GetSegmentAttributes(nsegment).Copy(attrib);
            }
        }

        public void SetSegmentAttributesMode(bool bmode)
        {
            this.segmentColorMode = bmode;
            this.InitSegmentAttributes();
        }

        public void SetSegmentColor(int nsegment, Color rgbcolor)
        {
            if (this.segmentAttributesArrayList == null)
            {
                this.InitSegmentAttributes();
            }
            if ((nsegment >= 0) && (nsegment < this.numSegments))
            {
                ChartAttribute segmentAttributes = this.GetSegmentAttributes(nsegment);
                segmentAttributes.SetPrimaryColor(rgbcolor);
                segmentAttributes.SetFillColor(rgbcolor);
            }
        }

        public void SetSegmentFillColor(int nsegment, Color rgbcolor)
        {
            if (this.segmentAttributesArrayList == null)
            {
                this.InitSegmentAttributes();
            }
            if ((nsegment >= 0) && (nsegment < this.numSegments))
            {
                this.GetSegmentAttributes(nsegment).SetFillColor(rgbcolor);
            }
        }

        public void SetSegmentLineColor(int nsegment, Color rgbcolor)
        {
            if (this.segmentAttributesArrayList == null)
            {
                this.InitSegmentAttributes();
            }
            if ((nsegment >= 0) && (nsegment < this.numSegments))
            {
                this.GetSegmentAttributes(nsegment).SetPrimaryColor(rgbcolor);
            }
        }

        public void SetShowDatapointValue(bool show)
        {
            this.showDatapointValue = show;
        }

        public void SetStepMode(int stepmode)
        {
            this.stepMode = stepmode;
        }

        public int BarDatapointLabelPosition
        {
            get
            {
                return this.barDatapointLabelPosition;
            }
            set
            {
                this.barDatapointLabelPosition = value;
            }
        }

        public int BarJust
        {
            get
            {
                return this.barJust;
            }
            set
            {
                this.barJust = value;
            }
        }

        public int BarOrient
        {
            get
            {
                return this.barOrient;
            }
            set
            {
                this.barOrient = value;
            }
        }

        public double BarWidth
        {
            get
            {
                return this.barWidth;
            }
            set
            {
                this.barWidth = value;
            }
        }

        public int FastClipMode
        {
            get
            {
                return this.fastClipMode;
            }
            set
            {
                this.fastClipMode = value;
            }
        }

        public double FillBaseValue
        {
            get
            {
                return this.fillBaseValue;
            }
            set
            {
                this.fillBaseValue = value;
            }
        }

        public int LabelTemplateDecimalPos
        {
            get
            {
                return this.plotLabelTemplate.GetDecimalPos();
            }
            set
            {
                this.plotLabelTemplate.SetDecimalPos(value);
            }
        }

        public int LabelTemplateNumericFormat
        {
            get
            {
                return this.plotLabelTemplate.GetNumericFormat();
            }
            set
            {
                this.plotLabelTemplate.SetNumericFormat(value);
            }
        }

        public NumericLabel PlotLabelTemplate
        {
            get
            {
                this.plotLabelTemplate.SetChartObjScale(this.GetChartObjScale());
                this.plotLabelTemplate.SetResizeMultiplier(this.GetResizeMultiplier());
                return this.plotLabelTemplate;
            }
            set
            {
                if (value != null)
                {
                    this.plotLabelTemplate = (NumericLabel) value.Clone();
                    this.plotLabelTemplate.SetChartObjScale(this.GetChartObjScale());
                }
            }
        }

        public bool ShowDatapointValue
        {
            get
            {
                return this.showDatapointValue;
            }
            set
            {
                this.showDatapointValue = value;
            }
        }

        public int StepMode
        {
            get
            {
                return this.stepMode;
            }
            set
            {
                this.stepMode = value;
            }
        }
    }
}

