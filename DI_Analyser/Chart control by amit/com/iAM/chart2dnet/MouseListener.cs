namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public abstract class MouseListener : GraphObj
    {
        internal MouseButtons buttonMask;
        internal Point2D cursorDevLocation;
        internal Point2D cursorPhysLocation;
        internal bool enabled;
        internal Point2D lastPhysLocation;
        internal bool objActive;
        internal Graphics tempGraphics;

        public MouseListener()
        {
            this.cursorDevLocation = new Point2D();
            this.cursorPhysLocation = new Point2D();
            this.lastPhysLocation = new Point2D();
            this.tempGraphics = null;
            this.objActive = false;
            this.buttonMask = MouseButtons.Left;
            this.enabled = false;
            this.InitDefaults();
        }

        public MouseListener(ChartView component)
        {
            this.cursorDevLocation = new Point2D();
            this.cursorPhysLocation = new Point2D();
            this.lastPhysLocation = new Point2D();
            this.tempGraphics = null;
            this.objActive = false;
            this.buttonMask = MouseButtons.Left;
            this.enabled = false;
            this.InitDefaults();
            base.chartObjComponent = component;
            this.enabled = true;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return false;
        }

        public void Copy(MouseListener source)
        {
            if (source != null)
            {
                base.Copy(source);
                base.chartObjComponent = source.chartObjComponent;
                this.enabled = source.enabled;
            }
        }

        public override void Draw(Graphics g2)
        {
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (base.chartObjComponent == null))
            {
                nerror = 50;
            }
            return base.ErrorCheck(nerror);
        }

        public MouseButtons GetButtonMask()
        {
            return this.buttonMask;
        }

        public bool GetEnable()
        {
            return this.enabled;
        }

        private void InitDefaults()
        {
        }

        public abstract void OnClick(EventArgs mouseevent);
        public abstract void OnDoubleClick(EventArgs mouseevent);
        public abstract void OnMouseDown(MouseEventArgs mouseevent);
        public abstract void OnMouseMove(MouseEventArgs mouseevent);
        public abstract void OnMouseUp(MouseEventArgs mouseevent);
        public void SetButtonMask(MouseButtons buttonmask)
        {
            this.buttonMask = buttonmask;
        }

        public void SetEnable(bool on)
        {
            this.enabled = on;
        }

        public MouseButtons ButtonMask
        {
            get
            {
                return this.buttonMask;
            }
            set
            {
                this.buttonMask = value;
            }
        }

        public bool ObjEnable
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        public Graphics TempGraphics
        {
            get
            {
                return this.tempGraphics;
            }
            set
            {
                this.tempGraphics = value;
            }
        }
    }
}

