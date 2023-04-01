namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Windows.Forms;

    public class FindObj : MouseListener
    {
        internal string classToFind;
        internal GraphObj selectedObj;

        public FindObj()
        {
            this.selectedObj = null;
            this.classToFind = "GraphObj";
        }

        public FindObj(ChartView component) : base(component)
        {
            this.selectedObj = null;
            this.classToFind = "GraphObj";
        }

        public FindObj(ChartView component, string classname) : base(component)
        {
            this.selectedObj = null;
            this.classToFind = "GraphObj";
            this.classToFind = classname;
        }

        public override object Clone()
        {
            FindObj obj2 = new FindObj();
            obj2.Copy(this);
            return obj2;
        }

        public void Copy(FindObj source)
        {
            if (source != null)
            {
                base.Copy(source);
                base.buttonMask = source.buttonMask;
                this.classToFind = source.classToFind;
            }
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public string GetClassToFind()
        {
            return this.classToFind;
        }

        public GraphObj GetSelectedObject()
        {
            return this.selectedObj;
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
            if ((mouseevent.Button & base.buttonMask) != MouseButtons.None)
            {
                this.selectedObj = null;
                this.selectedObj = base.chartObjComponent.FindObj(testpoint, this.classToFind);
            }
        }

        public override void OnMouseMove(MouseEventArgs mouseevent)
        {
        }

        public override void OnMouseUp(MouseEventArgs mouseevent)
        {
        }

        public void SetClassToFind(string findclass)
        {
            this.classToFind = findclass;
        }

        public string ClassToFind
        {
            get
            {
                return this.classToFind;
            }
            set
            {
                this.classToFind = value;
            }
        }
    }
}

