namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class PieChart : ChartPlot
    {
        internal int[] labelInOut;
        internal int pieSliceLabelFormat;
        internal Arc2D[] pieSlices;
        internal string[] pieSliceStrings;
        internal StringLabel[] pieTextObj;
        internal double startPieSliceAngle;
        internal double sumPieValues;
        internal SimpleDataset theDataset;

        public PieChart()
        {
            this.theDataset = null;
            this.sumPieValues = 0.0;
            this.startPieSliceAngle = 0.0;
            this.pieSliceLabelFormat = 3;
            this.InitDefaults();
        }

        public PieChart(PhysicalCoordinates transform, SimpleDataset dataset, string[] spiestring1s, ChartAttribute[] attribs, int labelinout1, int pielabelformat)
        {
            this.theDataset = null;
            this.sumPieValues = 0.0;
            this.startPieSliceAngle = 0.0;
            this.pieSliceLabelFormat = 3;
            int numberDatapoints = dataset.GetNumberDatapoints();
            this.SetChartObjScale(transform);
            this.InitDefaults();
            this.theDataset = dataset;
            base.InitSegmentAttributes(attribs, numberDatapoints);
            this.pieTextObj = new StringLabel[numberDatapoints];
            this.pieSliceStrings = new string[numberDatapoints];
            this.pieSlices = new Arc2D[numberDatapoints];
            this.labelInOut = new int[numberDatapoints];
            for (int i = 0; i < numberDatapoints; i++)
            {
                this.pieSliceStrings[i] = string.Copy(spiestring1s[i]);
                this.pieTextObj[i] = new StringLabel();
                this.pieSlices[i] = new Arc2D();
                this.labelInOut[i] = labelinout1;
            }
            this.pieSliceLabelFormat = pielabelformat;
        }

        private void AdjustPieRectangle(Rectangle2D r)
        {
            double x = r.GetX();
            double y = r.GetY();
            double width = r.GetWidth();
            double height = r.GetHeight();
            double num5 = x + (width / 2.0);
            double num6 = y + (height / 2.0);
            if (width > height)
            {
                width = height;
                x = num5 - (width / 2.0);
            }
            else if (height > width)
            {
                height = width;
                y = num6 - (height / 2.0);
            }
            r.SetFrame(x, y, width, height);
        }

        public override bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            int num;
            int num4 = -1;
            new Point2D();
            new Point2D();
            new Point2D();
            bool flag = false;
            DoubleArray xData = this.theDataset.XData;
            DoubleArray yData = this.theDataset.YData;
            Rectangle2D testr = new Rectangle2D((double) (((int) testpoint.GetX()) - 1), (double) (((int) testpoint.GetY()) - 1), 3.0, 3.0);
            int numberDatapoints = this.theDataset.GetNumberDatapoints();
            for (num = 0; num < numberDatapoints; num++)
            {
                if (this.theDataset.IsDataPointGood(num))
                {
                    num4 = num;
                    break;
                }
            }
            if (num4 < 0)
            {
                flag = false;
                if (nearestpoint != null)
                {
                    nearestpoint.nearestPointValid = flag;
                }
                return flag;
            }
            int num3 = num4;
            for (num = num4; num < numberDatapoints; num++)
            {
                if (this.theDataset.IsDataPointGood(num) && this.pieSlices[num].Contains(testr))
                {
                    num3 = num;
                    flag = true;
                    break;
                }
            }
            if (nearestpoint != null)
            {
                nearestpoint.nearestPointValid = flag;
                nearestpoint.nearestPoint.SetLocation(xData[num3], yData[num3]);
                nearestpoint.nearestPointIndex = num3;
            }
            return flag;
        }

        private void CalcPieWedges()
        {
            double stAng = 0.0;
            double num3 = 0.0;
            Rectangle2D plotRect = base.chartObjScale.GetPlotRect();
            this.AdjustPieRectangle(plotRect);
            Rectangle2D rect = new Rectangle2D();
            int numberDatapoints = this.theDataset.GetNumberDatapoints();
            DoubleArray xData = this.theDataset.XData;
            DoubleArray yData = this.theDataset.YData;
            BoolArray validData = this.theDataset.ValidData;
            this.sumPieValues = this.GetSumPieSlices();
            stAng = this.startPieSliceAngle;
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (this.CheckValidPoint(xData[i], yData[i], validData[i]))
                {
                    double num4 = xData[i] / this.sumPieValues;
                    double extAng = 360.0 * num4;
                    num3 = stAng + extAng;
                    double degrees = stAng + (extAng / 2.0);
                    double d = ChartSupport.ToRadians(degrees);
                    rect.SetFrame(plotRect);
                    double x = plotRect.GetX();
                    double y = plotRect.GetY();
                    double width = plotRect.GetWidth();
                    double height = plotRect.GetHeight();
                    double num11 = width / 2.0;
                    double num13 = (yData[i] * num11) * Math.Cos(d);
                    double num14 = (yData[i] * num11) * Math.Sin(d);
                    x += num13;
                    y -= num14;
                    rect.SetFrame(x, y, width, height);
                    this.pieSlices[i].SetArc(rect, stAng, extAng, 2);
                }
                if (base.chartObjScale.CheckValidPoint(xData[i], yData[i]))
                {
                    stAng = num3;
                }
            }
        }

        private void CartesianToPolar(Point2D dest, Point2D source)
        {
            double px = 0.0;
            double py = 0.0;
            double x = source.GetX();
            double y = source.GetY();
            py = Math.Atan2(y, x);
            px = Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2.0));
            if (py < 0.0)
            {
                py += 6.2831853071795862;
            }
            dest.SetLocation(px, py);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            bool flag = false;
            new Point2D();
            this.CalcNearestPoint(testpoint, 5, np);
            if (np.nearestPointMinDistance <= base.intersectionTestDistance)
            {
                flag = true;
            }
            return flag;
        }

        public override object Clone()
        {
            PieChart chart = new PieChart();
            chart.Copy(this);
            return chart;
        }

        public void Copy(PieChart source)
        {
            int numberDatapoints = 0;
            if (source != null)
            {
                base.Copy(source);
                if (source.theDataset != null)
                {
                    numberDatapoints = source.theDataset.GetNumberDatapoints();
                    this.theDataset = new SimpleDataset();
                    this.theDataset.Copy(source.theDataset);
                }
                this.pieTextObj = new StringLabel[numberDatapoints];
                this.pieSliceStrings = new string[numberDatapoints];
                this.pieSlices = new Arc2D[numberDatapoints];
                this.labelInOut = new int[numberDatapoints];
                for (int i = 0; i < numberDatapoints; i++)
                {
                    this.pieSliceStrings[i] = string.Copy(source.pieSliceStrings[i]);
                    this.pieTextObj[i] = new StringLabel();
                    this.pieSlices[i] = new Arc2D();
                    this.labelInOut[i] = source.labelInOut[i];
                }
                this.sumPieValues = source.sumPieValues;
                this.pieSliceLabelFormat = source.pieSliceLabelFormat;
                this.startPieSliceAngle = source.startPieSliceAngle;
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawPieChart(g2);
            }
        }

        public void DrawPieChart(Graphics g2)
        {
            this.DrawPieWedges(g2);
            this.DrawPieText(g2);
        }

        private void DrawPieText(Graphics g2)
        {
            string tstring = "";
            int format = 8;
            int decs = 0;
            ChartLabel plotLabelTemplate = base.GetPlotLabelTemplate();
            NumericLabel label2 = base.GetPlotLabelTemplate();
            int numberDatapoints = this.theDataset.GetNumberDatapoints();
            DoubleArray xData = this.theDataset.XData;
            DoubleArray yData = this.theDataset.YData;
            BoolArray validData = this.theDataset.ValidData;
            format = label2.GetNumericFormat();
            decs = label2.GetDecimalPos();
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (this.CheckValidPoint(xData[i], yData[i], validData[i]))
                {
                    double num6;
                    double num7;
                    double num = xData[i];
                    tstring = "";
                    if (format == 5)
                    {
                        num /= this.sumPieValues;
                    }
                    if ((this.pieSliceLabelFormat & 2) == 2)
                    {
                        tstring = ChartSupport.NumToString(num, format, decs, NumericLabel.GetNumStrFormatPostfix());
                    }
                    if ((this.pieSliceLabelFormat & 1) == 1)
                    {
                        if (tstring.Length > 0)
                        {
                            tstring = this.pieSliceStrings[i] + '\n' + tstring;
                        }
                        else
                        {
                            tstring = this.pieSliceStrings[i];
                        }
                    }
                    this.pieTextObj[i].Copy(plotLabelTemplate);
                    this.pieTextObj[i].InitChartText(base.chartObjScale, plotLabelTemplate.GetTextFont(), tstring, 0.0, 0.0, 0, 0, 0, 0.0);
                    this.pieTextObj[i].SetResizeMultiplier(this.GetResizeMultiplier());
                    double height = this.pieSlices[i].GetHeight();
                    double width = this.pieSlices[i].GetWidth();
                    double x = this.pieSlices[i].GetX() + (width / 2.0);
                    double y = this.pieSlices[i].GetY() + (height / 2.0);
                    double degrees = this.pieSlices[i].GetStartAngle() + (this.pieSlices[i].GetExtentAngle() / 2.0);
                    double d = ChartSupport.ToRadians(degrees);
                    double num8 = 1.05 * (width / 2.0);
                    if (this.labelInOut[i] == 2)
                    {
                        if ((degrees < 45.0) || (degrees > 315.0))
                        {
                            num8 += this.pieTextObj[i].GetTextMaxSizeY(g2, 0) / 2.0;
                        }
                        num6 = num8 * Math.Cos(d);
                        num7 = num8 * Math.Sin(d);
                        x += num6;
                        y -= num7;
                        this.SetPieTextJust(this.pieTextObj[i], degrees);
                        this.pieTextObj[i].SetLocation(x, y, 0);
                    }
                    else if (this.labelInOut[i] == 1)
                    {
                        this.pieTextObj[i].SetXJust(1);
                        this.pieTextObj[i].SetYJust(1);
                        num6 = (0.66 * num8) * Math.Cos(d);
                        num7 = (0.66 * num8) * Math.Sin(d);
                        x += num6;
                        y -= num7;
                        this.pieTextObj[i].SetLocation(x, y, 0);
                    }
                    this.pieTextObj[i].SetChartObjClipping(1);
                    if (this.labelInOut[i] != 0)
                    {
                        this.pieTextObj[i].Draw(g2);
                    }
                    if (base.showDatapointValue)
                    {
                        x = this.pieSlices[i].GetX() + (width / 2.0);
                        y = this.pieSlices[i].GetY() + (height / 2.0);
                        num6 = (0.66 * num8) * Math.Cos(d);
                        num7 = (0.66 * num8) * Math.Sin(d);
                        x += num6;
                        y -= num7;
                        tstring = ChartSupport.NumToString(num, format, decs, NumericLabel.GetNumStrFormatPostfix());
                        this.pieTextObj[i].InitChartText(base.chartObjScale, plotLabelTemplate.GetTextFont(), tstring, x, y, 0, 1, 1, 0.0);
                        this.pieTextObj[i].SetResizeMultiplier(this.GetResizeMultiplier());
                        this.pieTextObj[i].SetLocation(x, y, 0);
                        this.pieTextObj[i].SetChartObjClipping(1);
                        this.pieTextObj[i].Draw(g2);
                    }
                }
            }
        }

        private void DrawPieWedges(Graphics g2)
        {
            int numberDatapoints = this.theDataset.GetNumberDatapoints();
            DoubleArray xData = this.theDataset.XData;
            DoubleArray yData = this.theDataset.YData;
            BoolArray validData = this.theDataset.ValidData;
            this.CalcPieWedges();
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            for (int i = 0; i < numberDatapoints; i++)
            {
                if (this.CheckValidPoint(xData[i], yData[i], validData[i]))
                {
                    ChartAttribute segmentAttributes = base.GetSegmentAttributes(i);
                    base.chartObjScale.SetCurrentAttributes(segmentAttributes);
                    float startAngle = (360f - ((float) this.pieSlices[i].GetStartAngle())) - ((float) this.pieSlices[i].GetExtentAngle());
                    float extentAngle = (float) this.pieSlices[i].GetExtentAngle();
                    if (segmentAttributes.FillFlag)
                    {
                        g2.FillPie(segmentAttributes.GetCurrentBrush(), this.pieSlices[i].GetRectangle(), startAngle, extentAngle);
                    }
                    if (segmentAttributes.LineFlag)
                    {
                        g2.DrawPie(segmentAttributes.GetCurrentPen(), this.pieSlices[i].GetRectangle(), startAngle, extentAngle);
                    }
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.theDataset == null)
                {
                    nerror = 530;
                }
                if (nerror == 0)
                {
                    nerror = this.theDataset.ErrorCheck(nerror);
                }
                if ((nerror == 0) && (this.pieSliceStrings == null))
                {
                    nerror = 700;
                }
                if ((nerror == 0) && (this.labelInOut == null))
                {
                    nerror = 700;
                }
            }
            return base.ErrorCheck(nerror);
        }

        public override ChartDataset GetDataset()
        {
            return this.theDataset;
        }

        public int GetLabelInOut(int index)
        {
            int num = 0;
            if (index < this.theDataset.GetNumberDatapoints())
            {
                num = this.labelInOut[index];
            }
            return num;
        }

        public int GetPieSliceLabelFormat()
        {
            return this.pieSliceLabelFormat;
        }

        public void GetPieSlicePoints(int pieslice, Point2D arcstart, Point2D arcstop, Point2D arcorigin)
        {
            this.CalcPieWedges();
            arcstart = this.pieSlices[pieslice].GetStartPoint();
            arcstop = this.pieSlices[pieslice].GetEndPoint();
            if (arcorigin != null)
            {
                arcorigin.SetLocation(this.pieSlices[pieslice].GetCenterX(), this.pieSlices[pieslice].GetCenterY());
            }
        }

        public string GetPieSliceStrings(int index)
        {
            string str = "";
            if (index < this.theDataset.GetNumberDatapoints())
            {
                str = this.pieSliceStrings[index];
            }
            return str;
        }

        public double GetStartPieSliceAngle()
        {
            return this.startPieSliceAngle;
        }

        public double GetSumPieSlices()
        {
            this.sumPieValues = 0.0;
            for (int i = 0; i < this.theDataset.GetNumberDatapoints(); i++)
            {
                this.sumPieValues += this.theDataset.XData[i];
            }
            return this.sumPieValues;
        }

        private void InitDefaults()
        {
            base.chartObjType = 50;
            base.chartObjClipping = 0;
        }

        public void SetDataset(SimpleDataset dataset)
        {
            int numsegments = 0;
            if (dataset != null)
            {
                numsegments = dataset.NumberDatapoints;
            }
            this.theDataset = dataset;
            base.ResizeSegmentAttributes(numsegments);
        }

        public void SetLabelInOut(int index, int labelinout1)
        {
            if (index < this.theDataset.GetNumberDatapoints())
            {
                this.labelInOut[index] = labelinout1;
            }
        }

        public void SetPieSliceLabelFormat(int format)
        {
            this.pieSliceLabelFormat = format;
        }

        public void SetPieSliceStrings(int index, string piestring1)
        {
            if (index < this.theDataset.GetNumberDatapoints())
            {
                this.pieSliceStrings[index] = piestring1;
            }
        }

        private void SetPieTextJust(StringLabel text, double rangle)
        {
            if (rangle < 0.0)
            {
                rangle += 360.0;
            }
            if ((rangle >= 0.0) && (rangle < 15.0))
            {
                text.SetXJust(0);
                text.SetYJust(1);
            }
            else if ((rangle >= 15.0) && (rangle < 45.0))
            {
                text.SetXJust(0);
                text.SetYJust(1);
            }
            else if ((rangle >= 45.0) && (rangle < 75.0))
            {
                text.SetXJust(0);
                text.SetYJust(0);
            }
            else if ((rangle >= 75.0) && (rangle < 105.0))
            {
                text.SetXJust(1);
                text.SetYJust(0);
            }
            else if ((rangle >= 105.0) && (rangle < 135.0))
            {
                text.SetXJust(2);
                text.SetYJust(0);
            }
            else if ((rangle >= 135.0) && (rangle < 165.0))
            {
                text.SetXJust(2);
                text.SetYJust(0);
            }
            else if ((rangle >= 165.0) && (rangle < 195.0))
            {
                text.SetXJust(2);
                text.SetYJust(1);
            }
            else if ((rangle >= 195.0) && (rangle < 225.0))
            {
                text.SetXJust(2);
                text.SetYJust(2);
            }
            else if ((rangle >= 225.0) && (rangle < 255.0))
            {
                text.SetXJust(2);
                text.SetYJust(2);
            }
            else if ((rangle >= 255.0) && (rangle < 285.0))
            {
                text.SetXJust(1);
                text.SetYJust(2);
            }
            else if ((rangle >= 285.0) && (rangle < 315.0))
            {
                text.SetXJust(0);
                text.SetYJust(2);
            }
            else if ((rangle >= 315.0) && (rangle < 345.0))
            {
                text.SetXJust(0);
                text.SetYJust(1);
            }
            else if ((rangle >= 345.0) && (rangle <= 375.0))
            {
                text.SetXJust(0);
                text.SetYJust(1);
            }
        }

        public void SetStartPieSliceAngle(double angle)
        {
            this.startPieSliceAngle = angle;
        }

        public int PieSliceLabelFormat
        {
            get
            {
                return this.pieSliceLabelFormat;
            }
            set
            {
                this.pieSliceLabelFormat = value;
            }
        }

        public double StartPieSliceAngle
        {
            get
            {
                return this.startPieSliceAngle;
            }
            set
            {
                this.startPieSliceAngle = value;
            }
        }
    }
}

