namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class Grid : GraphObj
    {
        internal Axis gridAxis;
        internal int gridAxisType;
        internal int gridType;
        internal Axis gridXAxis;
        internal Axis gridYAxis;

        public Grid()
        {
            this.gridType = 0;
            this.gridAxisType = 0;
            this.gridXAxis = new LinearAxis();
            this.gridYAxis = new LinearAxis();
            this.gridAxis = new LinearAxis();
            this.InitDefaults();
        }

        public Grid(Axis xaxis, Axis yaxis, int gridaxistype, int gridtype)
        {
            this.gridType = 0;
            this.gridAxisType = 0;
            this.gridXAxis = new LinearAxis();
            this.gridYAxis = new LinearAxis();
            this.gridAxis = new LinearAxis();
            this.InitDefaults();
            this.gridXAxis = xaxis;
            this.gridYAxis = yaxis;
            this.gridAxisType = gridaxistype;
            this.gridType = gridtype;
            this.gridAxis = this.GetGridAxis(gridaxistype);
            if (this.gridAxis != null)
            {
                this.SetChartObjScale(this.gridAxis.GetChartObjScale());
            }
        }

        private bool CalcGridPoints(TickMark tickmark, Point2D pstart, Point2D pstop)
        {
            bool flag = false;
            int tickType = tickmark.GetTickType();
            double tickLocation = tickmark.GetTickLocation();
            if (this.CheckGridType(tickType))
            {
                flag = true;
                if (this.gridAxisType == 0)
                {
                    if (this.gridYAxis != null)
                    {
                        pstart.SetLocation(tickLocation, this.gridYAxis.GetAxisMin());
                        pstop.SetLocation(tickLocation, this.gridYAxis.GetAxisMax());
                        return flag;
                    }
                    pstart.SetLocation(tickLocation, base.chartObjScale.GetStartY());
                    pstop.SetLocation(tickLocation, base.chartObjScale.GetStopY());
                    return flag;
                }
                if (this.gridXAxis != null)
                {
                    pstart.SetLocation(this.gridXAxis.GetAxisMin(), tickLocation);
                    pstop.SetLocation(this.gridXAxis.GetAxisMax(), tickLocation);
                    return flag;
                }
                pstart.SetLocation(base.chartObjScale.GetStartX(), tickLocation);
                pstop.SetLocation(base.chartObjScale.GetStopX(), tickLocation);
            }
            return flag;
        }

        private bool CheckGridType(int ngridtype)
        {
            bool flag = false;
            if (((ngridtype != 0) || ((this.gridType != 0) && (this.gridType != 2))) && ((ngridtype != 1) || ((this.gridType != 1) && (this.gridType != 2))))
            {
                return flag;
            }
            return true;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            Rectangle2D rectangled = new Rectangle2D();
            bool flag = false;
            Point2D pstart = new Point2D();
            Point2D pstop = new Point2D();
            Point2D pointd3 = new Point2D();
            Point2D pointd4 = new Point2D();
            ArrayList axisTicksArrayList = this.GetGridAxis(this.gridAxisType).GetAxisTicksArrayList();
            int count = axisTicksArrayList.Count;
            base.chartObjScale.ConvertCoord(1, testpoint, 0);
            for (int i = 0; i < count; i++)
            {
                TickMark tickmark = (TickMark) axisTicksArrayList[i];
                if (this.CalcGridPoints(tickmark, pstart, pstop))
                {
                    pointd3 = base.chartObjScale.ConvertCoord(0, pstart, 1);
                    pointd4 = base.chartObjScale.ConvertCoord(0, pstop, 1);
                    if (this.gridAxisType == 0)
                    {
                        rectangled.SetFrame(pointd3.GetX() - 2.0, pointd4.GetY(), 4.0, pointd3.GetY() - pointd4.GetY());
                    }
                    else
                    {
                        rectangled.SetFrame(pointd3.GetX(), pointd4.GetY() - 2.0, pointd4.GetX() - pointd3.GetX(), 4.0);
                    }
                    if ((rectangled != null) && rectangled.Contains((double) ((int) testpoint.GetX()), (double) ((int) testpoint.GetY())))
                    {
                        return true;
                    }
                }
                if (flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public override object Clone()
        {
            Grid grid = new Grid();
            grid.Copy(this);
            return grid;
        }

        public void Copy(Grid source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.gridType = source.gridType;
                this.gridAxisType = source.gridAxisType;
                this.gridXAxis = source.gridXAxis;
                this.gridYAxis = source.gridYAxis;
                this.gridAxis = source.gridAxis;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.SetChartObjScale(this.GetChartObjScale());
                this.PrePlot(g2);
                this.DrawGrid(g2, base.thePath);
                base.boundingBox.Reset();
                base.boundingBox.AddPath(base.thePath, false);
            }
        }

        private void DrawGrid(Graphics g2, GraphicsPath path)
        {
            Point2D pstart = new Point2D();
            Point2D pstop = new Point2D();
            Pen currentPen = base.chartObjAttributes.CurrentPen;
            ArrayList axisTicksArrayList = this.gridAxis.GetAxisTicksArrayList();
            int count = axisTicksArrayList.Count;
            if (((this.gridAxis != null) && (this.gridXAxis != null)) && (this.gridYAxis != null))
            {
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                for (int i = 0; i < count; i++)
                {
                    TickMark tickmark = (TickMark) axisTicksArrayList[i];
                    if (this.CalcGridPoints(tickmark, pstart, pstop) && (this.GetChartObjEnable() == 1))
                    {
                        base.chartObjScale.WLineAbs(g2, path, pstart.GetX(), pstart.GetY(), pstop.GetX(), pstop.GetY(), currentPen, true, true);
                    }
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.gridXAxis == null)
                {
                    nerror = 120;
                }
                else
                {
                    nerror = this.gridXAxis.ErrorCheck(nerror);
                }
                if (nerror == 0)
                {
                    if (this.gridYAxis == null)
                    {
                        nerror = 120;
                    }
                    else
                    {
                        nerror = this.gridYAxis.ErrorCheck(nerror);
                    }
                }
            }
            return base.ErrorCheck(nerror);
        }

        public Axis GetGridAxis(int axtype)
        {
            Axis gridYAxis = null;
            if (axtype == 0)
            {
                return this.gridXAxis;
            }
            if (axtype == 1)
            {
                gridYAxis = this.gridYAxis;
            }
            return gridYAxis;
        }

        public int GetGridAxisType()
        {
            return this.gridAxisType;
        }

        public int GetGridType()
        {
            return this.gridType;
        }

        public Axis GetGridXAxis()
        {
            return this.gridXAxis;
        }

        public Axis GetGridYAxis()
        {
            return this.gridYAxis;
        }

        private void InitDefaults()
        {
            base.chartObjType = 300;
            this.gridYAxis.SetAxisType(1);
            base.chartObjAttributes.SetLineAttributes(Color.Black, 1.0, DashStyle.Dot);
            base.chartObjClipping = 2;
            base.zOrder = 40;
        }

        public void InitGrid(Axis xaxis, Axis yaxis, int gridaxistype, int gridtype)
        {
            this.gridXAxis = xaxis;
            this.gridYAxis = yaxis;
            this.gridAxisType = gridaxistype;
            this.gridType = gridtype;
            this.gridAxis = this.GetGridAxis(gridaxistype);
            if (this.gridAxis != null)
            {
                this.SetChartObjScale(this.gridAxis.GetChartObjScale());
            }
        }

        public override void SetChartObjScale(PhysicalCoordinates transform)
        {
            base.SetChartObjScale(transform);
            if (this.gridXAxis != null)
            {
                this.gridXAxis.SetChartObjScale(transform);
            }
            if (this.gridYAxis != null)
            {
                this.gridYAxis.SetChartObjScale(transform);
            }
        }

        public void SetGridAxis(Axis axis)
        {
            if (axis.GetAxisType() == 0)
            {
                this.gridXAxis = axis;
            }
            else if (axis.GetAxisType() == 1)
            {
                this.gridYAxis = axis;
            }
            this.gridAxis = axis;
        }

        public void SetGridAxis(int axtype, Axis axis)
        {
            if (axtype == 0)
            {
                this.gridXAxis = axis;
            }
            else if (axtype == 1)
            {
                this.gridYAxis = axis;
            }
            this.gridAxis = axis;
            if (this.gridAxis != null)
            {
                this.SetChartObjScale(this.gridAxis.GetChartObjScale());
            }
        }

        public void SetGridAxisType(int naxis)
        {
            this.gridAxisType = naxis;
            this.gridAxis = this.GetGridAxis(naxis);
        }

        public void SetGridType(int ngrid)
        {
            this.gridType = ngrid;
        }

        public void SetGridXAxis(Axis axis)
        {
            this.gridXAxis = axis;
        }

        public void SetGridYAxis(Axis axis)
        {
            this.gridYAxis = axis;
        }

        public int GridAxisType
        {
            get
            {
                return this.gridAxisType;
            }
            set
            {
                this.gridAxisType = value;
            }
        }

        public int GridType
        {
            get
            {
                return this.gridType;
            }
            set
            {
                this.gridType = value;
            }
        }
    }
}

