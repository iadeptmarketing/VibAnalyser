namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class MoveData : DataCursor
    {
        internal Point2D endMouseLocation;
        internal double hitTestThreshold;
        private bool moveDataActive;
        internal int moveMode;
        internal NearestPointData nearestPoint;
        internal ChartDataset selectedDataset;
        internal ChartPlot selectedPlotObj;
        internal bool xYDatasetSwap;

        public MoveData()
        {
            this.selectedPlotObj = new SimpleLinePlot();
            this.selectedDataset = new SimpleDataset();
            this.moveDataActive = false;
            this.endMouseLocation = new Point2D();
            this.moveMode = 2;
            this.nearestPoint = new NearestPointData();
            this.hitTestThreshold = 10.0;
            this.xYDatasetSwap = false;
        }

        public MoveData(ChartView component, PhysicalCoordinates transform)
        {
            this.selectedPlotObj = new SimpleLinePlot();
            this.selectedDataset = new SimpleDataset();
            this.moveDataActive = false;
            this.endMouseLocation = new Point2D();
            this.moveMode = 2;
            this.nearestPoint = new NearestPointData();
            this.hitTestThreshold = 10.0;
            this.xYDatasetSwap = false;
            base.InitChartDataCursor(component, transform, 3, 8.0);
            base.chartObjComponent = component;
        }

        public MoveData(ChartView component, PhysicalCoordinates transform, MouseButtons buttonmask)
        {
            this.selectedPlotObj = new SimpleLinePlot();
            this.selectedDataset = new SimpleDataset();
            this.moveDataActive = false;
            this.endMouseLocation = new Point2D();
            this.moveMode = 2;
            this.nearestPoint = new NearestPointData();
            this.hitTestThreshold = 10.0;
            this.xYDatasetSwap = false;
            base.InitChartDataCursor(component, transform, 3, 8.0);
            base.buttonMask = buttonmask;
            base.chartObjComponent = component;
        }

        public void Copy(MoveData source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.moveMode = source.moveMode;
                this.hitTestThreshold = source.hitTestThreshold;
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
            double num3 = 0.0;
            bool flag = true;
            NearestPointData nearestpoint = new NearestPointData();
            GraphObj chartobj = null;
            ChartPlot plot2 = null;
            for (int i = 0; i < count; i++)
            {
                chartobj = (GraphObj) chartObjectsArrayList[i];
                if ((((chartobj != null) && (chartobj.ErrorCheck(0) == 0)) && (chartobj.GetMoveableType() == 2)) && ((this.IsMoveableSimplePlotObj(chartobj) || this.IsMoveableGroupPlotObj(chartobj)) || this.IsMoveablePolarPlotObj(chartobj)))
                {
                    double nearestPointMinDistance = 0.0;
                    ChartPlot plot = (ChartPlot) chartobj;
                    PhysicalCoordinates chartObjScale = plot.GetChartObjScale();
                    if (chartObjScale != null)
                    {
                        Point2D pointd = chartObjScale.ConvertCoord(chartobj.GetPositionType(), testpoint, 0);
                        plot.CalcNearestPoint(pointd, 5, nearestpoint);
                        nearestPointMinDistance = nearestpoint.nearestPointMinDistance;
                        if (flag || (nearestPointMinDistance < num3))
                        {
                            num3 = nearestPointMinDistance;
                            plot2 = plot;
                            this.nearestPoint.Copy(nearestpoint);
                            if (flag)
                            {
                                flag = false;
                            }
                        }
                    }
                }
            }
            if (num3 > this.hitTestThreshold)
            {
                plot2 = null;
            }
            return plot2;
        }

        public double GetHitTestThreshold()
        {
            return this.hitTestThreshold;
        }

        public int GetMoveMode()
        {
            return this.moveMode;
        }

        public bool IsMoveableGroupPlotObj(GraphObj chartobj)
        {
            return (chartobj.GetMoveableType() == 2);
        }

        public bool IsMoveablePolarPlotObj(GraphObj chartobj)
        {
            return (chartobj.GetMoveableType() == 2);
        }

        public bool IsMoveableSimplePlotObj(GraphObj chartobj)
        {
            return (chartobj.GetMoveableType() == 2);
        }

        public override void OnMouseDown(MouseEventArgs mouseevent)
        {
            Point2D testpoint = new Point2D();
            testpoint.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            base.OnMouseDown(mouseevent);
            if (base.enabled && ((mouseevent.Button & base.buttonMask) != MouseButtons.None))
            {
                this.selectedPlotObj = this.FindObj(testpoint);
                this.endMouseLocation.SetLocation(testpoint.GetX(), testpoint.GetY());
                if (this.selectedPlotObj != null)
                {
                    this.selectedDataset = this.selectedPlotObj.GetDataset();
                    this.moveDataActive = true;
                }
            }
        }

        public override void OnMouseMove(MouseEventArgs mouseevent)
        {
            Point2D pointd = new Point2D();
            pointd.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            base.OnMouseMove(mouseevent);
            if ((base.enabled && this.moveDataActive) && ((this.selectedPlotObj != null) && ((mouseevent.Button & base.buttonMask) != MouseButtons.None)))
            {
                this.endMouseLocation.SetLocation(pointd.GetX(), pointd.GetY());
            }
        }

        public override void OnMouseUp(MouseEventArgs mouseevent)
        {
            Point2D pointd = new Point2D();
            pointd.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            base.OnMouseUp(mouseevent);
            if ((base.enabled && this.moveDataActive) && ((this.selectedPlotObj != null) && ((mouseevent.Button & base.buttonMask) != MouseButtons.None)))
            {
                this.endMouseLocation.SetLocation(pointd.GetX(), pointd.GetY());
                PhysicalCoordinates chartObjScale = this.selectedPlotObj.GetChartObjScale();
                if (chartObjScale == null)
                {
                    return;
                }
                Point2D pointd2 = chartObjScale.ConvertCoord(this.selectedPlotObj.GetPositionType(), this.endMouseLocation, 0);
                if (this.nearestPoint.nearestPointIndex >= 0)
                {
                    if ((this.moveMode == 0) || (this.moveMode == 2))
                    {
                        if (this.xYDatasetSwap)
                        {
                            this.selectedDataset.SetXDataValue(this.nearestPoint.nearestPointIndex, pointd2.GetY());
                        }
                        else
                        {
                            this.selectedDataset.SetXDataValue(this.nearestPoint.nearestPointIndex, pointd2.GetX());
                        }
                    }
                    if ((this.moveMode == 1) || (this.moveMode == 2))
                    {
                        if (this.xYDatasetSwap)
                        {
                            this.selectedDataset.SetYDataValue(this.nearestPoint.nearestGroupIndex, this.nearestPoint.nearestPointIndex, pointd2.GetX());
                        }
                        else
                        {
                            this.selectedDataset.SetYDataValue(this.nearestPoint.nearestGroupIndex, this.nearestPoint.nearestPointIndex, pointd2.GetY());
                        }
                    }
                }
                base.chartObjComponent.UpdateDraw();
            }
            this.moveDataActive = false;
            base.tempGraphics = null;
            this.selectedPlotObj = null;
        }

        public void SetHitTestThreshold(double nearvalue)
        {
            this.hitTestThreshold = nearvalue;
        }

        public void SetMoveMode(int movemode)
        {
            this.moveMode = movemode;
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

        public int MoveMode
        {
            get
            {
                return this.moveMode;
            }
            set
            {
                this.moveMode = value;
            }
        }

        public bool XYDatasetSwap
        {
            get
            {
                return this.xYDatasetSwap;
            }
            set
            {
                this.xYDatasetSwap = value;
            }
        }
    }
}

