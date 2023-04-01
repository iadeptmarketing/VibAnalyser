namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PolarGrid : Grid
    {
        internal int gridAngleType;
        internal int gridMagnitudeType;

        public PolarGrid()
        {
            this.gridMagnitudeType = 0;
            this.gridAngleType = 0;
            this.InitDefaults();
        }

        public PolarGrid(PolarAxes polaraxis, int gridtype)
        {
            this.gridMagnitudeType = 0;
            this.gridAngleType = 0;
            this.InitDefaults();
            base.SetGridAxis(0, polaraxis);
            this.gridMagnitudeType = gridtype;
            this.gridAngleType = gridtype;
            this.SetChartObjScale((PolarCoordinates) polaraxis.GetChartObjScale());
        }

        public PolarGrid(PolarAxes polaraxis, int gridmagtype, int gridangletype)
        {
            this.gridMagnitudeType = 0;
            this.gridAngleType = 0;
            this.InitDefaults();
            base.SetGridAxis(0, polaraxis);
            this.gridMagnitudeType = gridmagtype;
            this.gridAngleType = gridangletype;
            this.SetChartObjScale((PolarCoordinates) polaraxis.GetChartObjScale());
        }

        public override object Clone()
        {
            PolarGrid grid = new PolarGrid();
            grid.Copy(this);
            return grid;
        }

        public void Copy(PolarGrid source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.gridMagnitudeType = source.gridMagnitudeType;
                this.gridAngleType = source.gridAngleType;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.SetChartObjScale((PolarCoordinates) this.GetChartObjScale());
                base.thePath = new GraphicsPath();
                base.chartObjScale.ChartTransform(g2);
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                this.DrawGridRadians(base.thePath);
                this.DrawPolarGridCircles(base.thePath);
                base.boundingBox.Reset();
                base.boundingBox.AddPath(base.thePath, false);
                if (this.GetChartObjEnable() == 1)
                {
                    base.chartObjScale.DrawPath(g2, base.chartObjAttributes.GetCurrentPen(), base.thePath);
                }
            }
        }

        private void DrawGridRadians(GraphicsPath path)
        {
            Point2D dest = new Point2D();
            Point2D tickStop = new Point2D();
            ArrayList axisTicksArrayList = base.GetGridAxis(0).GetAxisTicksArrayList();
            int count = axisTicksArrayList.Count;
            dest.SetLocation((double) 0.0, 0.0);
            base.chartObjScale.ConvertCoord(dest, 0, dest, 1);
            for (int i = 0; i < count; i++)
            {
                TickMark mark = (TickMark) axisTicksArrayList[i];
                int tickType = mark.GetTickType();
                if (((tickType == 0) && ((this.gridAngleType == 0) || (this.gridAngleType == 2))) || ((tickType == 1) && ((this.gridAngleType == 1) || (this.gridAngleType == 2))))
                {
                    tickStop = mark.GetTickStop();
                    base.chartObjScale.PLineAbs(path, dest.GetX(), dest.GetY(), tickStop.GetX(), tickStop.GetY());
                }
            }
        }

        private void DrawPolarGridCircles(GraphicsPath path)
        {
            Point2D dest = new Point2D();
            Point2D pointd2 = new Point2D();
            Rectangle2D rectangled = new Rectangle2D();
            int tickType = 0;
            double px = 1.0;
            ArrayList axisTicksArrayList = ((PolarAxes) base.GetGridAxis(0)).GetPolarXAxis().GetAxisTicksArrayList();
            int count = axisTicksArrayList.Count;
            for (int i = 0; i < count; i++)
            {
                TickMark mark = (TickMark) axisTicksArrayList[i];
                tickType = mark.GetTickType();
                if (((tickType == 0) && ((this.gridMagnitudeType == 0) || (this.gridMagnitudeType == 2))) || ((tickType == 1) && ((this.gridMagnitudeType == 1) || (this.gridMagnitudeType == 2))))
                {
                    dest = mark.GetTickStart();
                    base.chartObjScale.ConvertCoord(dest, 1, dest, 0);
                    px = dest.GetX();
                    dest.SetLocation(-px, -px);
                    pointd2.SetLocation(px, px);
                    base.chartObjScale.ConvertCoord(dest, 0, dest, 1);
                    base.chartObjScale.ConvertCoord(pointd2, 0, pointd2, 1);
                    double ww = pointd2.GetX() - dest.GetX();
                    double hh = dest.GetY() - pointd2.GetY();
                    rectangled.SetFrame(dest.GetX(), pointd2.GetY(), ww, hh);
                    path.AddEllipse(rectangled.GetRectangleF());
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (base.GetGridAxis(0) == null)
                {
                    nerror = 150;
                }
                if (nerror == 0)
                {
                    nerror = base.GetGridAxis(0).ErrorCheck(nerror);
                }
            }
            return 0;
        }

        public int GetGridAngleType()
        {
            return this.gridAngleType;
        }

        public int GetGridMagnitudeType()
        {
            return this.gridMagnitudeType;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x80;
            base.chartObjAttributes.SetLineAttributes(Color.Black, 1.0, DashStyle.Dot);
        }

        public void SetChartObjScale(PolarCoordinates transform)
        {
            base.SetChartObjScale(transform);
            base.gridXAxis.SetChartObjScale(transform);
        }

        public void SetGridAngleType(int ngrid)
        {
            this.gridAngleType = ngrid;
        }

        public void SetGridMagnitudeType(int ngrid)
        {
            this.gridMagnitudeType = ngrid;
        }

        public void Update()
        {
            this.SetChartObjScale(base.GetGridAxis(0).GetChartObjScale());
        }

        public int GridAngleType
        {
            get
            {
                return this.gridAngleType;
            }
            set
            {
                this.gridAngleType = value;
            }
        }

        public int GridMagnitudeType
        {
            get
            {
                return this.gridMagnitudeType;
            }
            set
            {
                this.gridMagnitudeType = value;
            }
        }
    }
}

