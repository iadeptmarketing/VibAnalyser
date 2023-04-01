namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DataCursor : MouseListener
    {
        private bool cursorObjActive;
        private Marker mouseMarker;

        public DataCursor()
        {
            this.cursorObjActive = false;
            this.InitDefaults();
        }

        public DataCursor(ChartView component, PhysicalCoordinates transform, int nmarkertype, double rsize)
        {
            this.cursorObjActive = false;
            this.InitChartDataCursor(component, transform, nmarkertype, rsize);
        }

        public override object Clone()
        {
            DataCursor cursor = new DataCursor();
            cursor.Copy(this);
            return cursor;
        }

        public void Copy(DataCursor source)
        {
            if (source != null)
            {
                base.Copy(source);
                if (source.cursorDevLocation != null)
                {
                    base.cursorDevLocation = (Point2D) source.cursorDevLocation.Clone();
                }
                if (source.cursorPhysLocation != null)
                {
                    base.cursorPhysLocation = (Point2D) source.cursorPhysLocation.Clone();
                }
                if (source.lastPhysLocation != null)
                {
                    base.lastPhysLocation = (Point2D) source.lastPhysLocation.Clone();
                }
                base.chartObjScale = source.chartObjScale;
                this.cursorObjActive = false;
                base.buttonMask = source.buttonMask;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.mouseMarker.DrawReversibleMarker(base.chartObjComponent);
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public override Point2D GetLocation()
        {
            return this.mouseMarker.GetLocation();
        }

        public Marker GetMarker()
        {
            return this.mouseMarker;
        }

        public double GetMarkerSize()
        {
            return this.mouseMarker.GetMarkerSize();
        }

        public int GetMarkerType()
        {
            return this.mouseMarker.GetMarkerType();
        }

        public void InitChartDataCursor(ChartView component, PhysicalCoordinates transform, int nmarkertype, double rsize)
        {
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.mouseMarker = new Marker(transform, nmarkertype, 0.0, 0.0, rsize, ChartSupport.GetCoordinateSystemType(transform));
            base.chartObjComponent = component;
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x3e9;
        }

        public override void OnClick(EventArgs mouseevent)
        {
        }

        public override void OnDoubleClick(EventArgs mouseevent)
        {
        }

        public override void OnMouseDown(MouseEventArgs mouseevent)
        {
            base.cursorDevLocation.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            if (base.chartObjScale != null)
            {
                base.chartObjScale.ConvertCoord(base.cursorPhysLocation, ChartSupport.GetCoordinateSystemType(base.chartObjScale), base.cursorDevLocation, 0);
                this.mouseMarker.SetLocation(base.cursorPhysLocation.GetX(), base.cursorPhysLocation.GetY());
                if ((mouseevent.Button & base.buttonMask) != MouseButtons.None)
                {
                    this.cursorObjActive = true;
                    this.mouseMarker.SetLocation(base.cursorPhysLocation.GetX(), base.cursorPhysLocation.GetY());
                    base.chartObjScale.StartXORMode(base.chartObjComponent, base.chartObjAttributes.PrimaryColor, 0);
                    this.mouseMarker.DrawReversibleMarker(base.chartObjComponent);
                    base.lastPhysLocation.SetLocation(base.cursorPhysLocation.GetX(), base.cursorPhysLocation.GetY());
                }
            }
        }

        public override void OnMouseMove(MouseEventArgs mouseevent)
        {
            base.cursorDevLocation.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
            if (base.chartObjScale != null)
            {
                base.chartObjScale.ConvertCoord(base.cursorPhysLocation, ChartSupport.GetCoordinateSystemType(base.chartObjScale), base.cursorDevLocation, 0);
                if (this.cursorObjActive && ((mouseevent.Button & base.buttonMask) != MouseButtons.None))
                {
                    this.mouseMarker.SetLocation(base.lastPhysLocation.GetX(), base.lastPhysLocation.GetY());
                    this.mouseMarker.DrawReversibleMarker(base.chartObjComponent);
                    this.mouseMarker.SetLocation(base.cursorPhysLocation.GetX(), base.cursorPhysLocation.GetY());
                    this.mouseMarker.DrawReversibleMarker(base.chartObjComponent);
                    base.lastPhysLocation.SetLocation(base.cursorPhysLocation.GetX(), base.cursorPhysLocation.GetY());
                }
            }
        }

        public override void OnMouseUp(MouseEventArgs mouseevent)
        {
            if (((mouseevent.Button & base.buttonMask) != MouseButtons.None) & this.cursorObjActive)
            {
                this.mouseMarker.SetLocation(base.cursorPhysLocation.GetX(), base.cursorPhysLocation.GetY());
                this.mouseMarker.DrawReversibleMarker(base.chartObjComponent);
                base.chartObjScale.EndXORMode(base.chartObjComponent);
                this.cursorObjActive = false;
            }
        }

        public void SetMarkerSize(double rsize)
        {
            this.mouseMarker.SetMarkerSize(rsize);
        }

        public void SetMarkerType(int nmarkertype)
        {
            this.mouseMarker.SetMarkerType(nmarkertype);
        }

        public Point2D Location
        {
            get
            {
                return this.mouseMarker.GetLocation();
            }
        }

        public double MarkerSize
        {
            get
            {
                return this.mouseMarker.MarkerSize;
            }
            set
            {
                this.mouseMarker.MarkerSize = value;
            }
        }

        public int MarkerType
        {
            get
            {
                return this.mouseMarker.MarkerType;
            }
            set
            {
                this.mouseMarker.MarkerType = value;
            }
        }

        public Marker MouseMarker
        {
            get
            {
                return this.mouseMarker;
            }
        }
    }
}

