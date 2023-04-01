namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;

    public class DummyObj : GraphObj
    {
        public DummyObj()
        {
            base.chartObjType = 0;
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            DummyObj obj2 = new DummyObj();
            obj2.Copy(this);
            return obj2;
        }

        public void Copy(DummyObj source)
        {
            if (source != null)
            {
                base.Copy(source);
            }
        }

        public override void Draw(Graphics g2)
        {
        }

        public override int ErrorCheck(int nerror)
        {
            return 0;
        }
    }
}

