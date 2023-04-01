namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;

    public class MoveObj : MouseListener
    {
        internal Point2D endMouseLocation;
        internal string moveObjectFilter;
        internal int moveObjMode;
        internal GraphObj selectedObj;
        internal Point2D startMouseLocation;

        public MoveObj()
        {
            this.startMouseLocation = new Point2D();
            this.endMouseLocation = new Point2D();
            this.selectedObj = new ChartText();
            this.moveObjMode = 2;
            this.moveObjectFilter = "GraphObj";
        }

        public MoveObj(ChartView component)
        {
            this.startMouseLocation = new Point2D();
            this.endMouseLocation = new Point2D();
            this.selectedObj = new ChartText();
            this.moveObjMode = 2;
            this.moveObjectFilter = "GraphObj";
            base.chartObjComponent = component;
        }

        public MoveObj(ChartView component, MouseButtons buttonmask)
        {
            this.startMouseLocation = new Point2D();
            this.endMouseLocation = new Point2D();
            this.selectedObj = new ChartText();
            this.moveObjMode = 2;
            this.moveObjectFilter = "GraphObj";
            base.chartObjComponent = component;
            base.buttonMask = buttonmask;
        }

        public MoveObj(ChartView component, MouseButtons buttonmask, string object1filter)
        {
            this.startMouseLocation = new Point2D();
            this.endMouseLocation = new Point2D();
            this.selectedObj = new ChartText();
            this.moveObjMode = 2;
            this.moveObjectFilter = "GraphObj";
            base.chartObjComponent = component;
            base.buttonMask = buttonmask;
            this.moveObjectFilter = object1filter;
        }

        public override object Clone()
        {
            MoveObj obj2 = new MoveObj();
            obj2.Copy(this);
            return obj2;
        }

        public void Copy(MoveObj source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.moveObjMode = source.moveObjMode;
                base.buttonMask = source.buttonMask;
                this.moveObjectFilter = source.moveObjectFilter;
                base.enabled = source.enabled;
            }
        }

        public virtual void DrawBoundingBox(Graphics g2, Point2D pstart, Point2D pstop)
        {
            Rectangle2D boundingBox = this.selectedObj.GetBoundingBox();
            Point2D pointd = new Point2D(pstop.GetX() - pstart.GetX(), pstop.GetY() - pstart.GetY());
            Rectangle rect = base.chartObjComponent.RectangleToScreen(new Rectangle2D((double) ((int) (boundingBox.GetX() + pointd.GetX())), (double) ((int) (boundingBox.GetY() + pointd.GetY())), (double) ((int) boundingBox.GetWidth()), (double) ((int) boundingBox.GetHeight())).GetRectangle());
            base.chartObjScale.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public GraphObj FindObj(Point2D testpoint)
        {
            ArrayList chartObjectsArrayList = base.chartObjComponent.GetChartObjectsArrayList();
            NearestPointData np = new NearestPointData();
            int count = chartObjectsArrayList.Count;
            GraphObj obj2 = null;
            for (int i = 0; i < count; i++)
            {
                obj2 = (GraphObj) chartObjectsArrayList[i];
                if (((obj2 != null) && ChartSupport.IsKindOf(obj2, this.moveObjectFilter)) && ((obj2.GetMoveableType() == 1) && obj2.CheckIntersection(testpoint, np)))
                {
                    this.startMouseLocation.SetLocation(testpoint.GetX(), testpoint.GetY());
                    return obj2;
                }
            }
            return null;
        }

        public string GetMoveObjectFilter()
        {
            return this.moveObjectFilter;
        }

        public int GetMoveObjMode()
        {
            return this.moveObjMode;
        }

        public bool IsMoveableObject(GraphObj chartobj)
        {
            return (chartobj.GetMoveableType() == 1);
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
                this.selectedObj = this.FindObj(testpoint);
                this.endMouseLocation.SetLocation(testpoint.GetX(), testpoint.GetY());
                if (this.selectedObj != null)
                {
                    base.chartObjScale.StartXORMode(base.chartObjComponent, base.chartObjAttributes.PrimaryColor, 0);
                    this.DrawBoundingBox(base.tempGraphics, this.startMouseLocation, this.endMouseLocation);
                }
            }
        }

        public override void OnMouseMove(MouseEventArgs mouseevent)
        {
            Point2D pointd = new Point2D();
            pointd.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            if ((base.enabled && (this.selectedObj != null)) && ((mouseevent.Button & base.buttonMask) != MouseButtons.None))
            {
                this.DrawBoundingBox(base.tempGraphics, this.startMouseLocation, this.endMouseLocation);
                this.endMouseLocation.SetLocation(pointd.GetX(), pointd.GetY());
                this.DrawBoundingBox(base.tempGraphics, this.startMouseLocation, this.endMouseLocation);
            }
        }

        public override void OnMouseUp(MouseEventArgs mouseevent)
        {
            Point2D pointd = new Point2D();
            pointd.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            if ((base.enabled && (this.selectedObj != null)) && ((mouseevent.Button & base.buttonMask) != MouseButtons.None))
            {
                this.endMouseLocation.SetLocation(pointd.GetX(), pointd.GetY());
                this.DrawBoundingBox(base.tempGraphics, this.startMouseLocation, this.endMouseLocation);
                base.chartObjScale.EndXORMode(base.chartObjComponent);
                PhysicalCoordinates chartObjScale = this.selectedObj.GetChartObjScale();
                if (chartObjScale == null)
                {
                    return;
                }
                Point2D pointd2 = chartObjScale.ConvertCoord(this.selectedObj.GetPositionType(), this.startMouseLocation, 0);
                Point2D pointd3 = chartObjScale.ConvertCoord(this.selectedObj.GetPositionType(), this.endMouseLocation, 0);
                Point2D pointd4 = new Point2D(pointd3.GetX() - pointd2.GetX(), pointd3.GetY() - pointd2.GetY());
                switch (this.moveObjMode)
                {
                    case 0:
                        this.selectedObj.MoveRel(pointd4.GetX(), 0.0);
                        break;

                    case 1:
                        this.selectedObj.MoveRel(0.0, pointd4.GetY());
                        break;

                    default:
                        this.selectedObj.MoveRel(pointd4.GetX(), pointd4.GetY());
                        break;
                }
                base.chartObjComponent.UpdateDraw();
            }
            base.tempGraphics = null;
            this.selectedObj = null;
        }

        public void SetMoveObjectFilter(string objfilter)
        {
            this.moveObjectFilter = objfilter;
        }

        public void SetMoveObjMode(int movemode)
        {
            this.moveObjMode = movemode;
        }

        public string MoveObjectFilter
        {
            get
            {
                return this.moveObjectFilter;
            }
            set
            {
                this.moveObjectFilter = value;
            }
        }

        public int MoveObjMode
        {
            get
            {
                return this.moveObjMode;
            }
            set
            {
                this.moveObjMode = value;
            }
        }
    }
}

