namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ChartShape : GraphObj
    {
        internal int shapeCoordsType;
        internal GraphicsPath shapeObj;
        internal double shapeRotation;

        public ChartShape()
        {
            this.shapeObj = null;
            this.shapeCoordsType = 0;
            this.shapeRotation = 0.0;
            this.InitDefaults();
        }

        public ChartShape(PhysicalCoordinates transform)
        {
            this.shapeObj = null;
            this.shapeCoordsType = 0;
            this.shapeRotation = 0.0;
            this.InitDefaults();
            this.InitChartShapeObj(transform, null, 0, 0.0, 0.0, 1, 0.0);
        }

        public ChartShape(PhysicalCoordinates transform, GraphicsPath ashape, int shapecoordstype, double x, double y, int npositiontype, int rotation)
        {
            this.shapeObj = null;
            this.shapeCoordsType = 0;
            this.shapeRotation = 0.0;
            this.InitDefaults();
            this.InitChartShapeObj(transform, ashape, shapecoordstype, x, y, npositiontype, (double) rotation);
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            ChartShape shape = new ChartShape();
            shape.Copy(this);
            return shape;
        }

        private void ConvertSegment(double[] coords, int ntype)
        {
            int[] numArray = new int[] { 1, 1, 2, 3, 0 };
            Point2D location = this.GetLocation(0);
            Dimension source = new Dimension();
            for (int i = 0; i < numArray[ntype]; i++)
            {
                double w = coords[i * 2];
                double h = coords[(i * 2) + 1];
                source.SetSize(w, h);
                Dimension dimension2 = base.chartObjScale.ConvertDimension(0, source, this.shapeCoordsType);
                w = dimension2.GetWidth() + location.GetX();
                h = dimension2.GetHeight() + location.GetY();
                coords[i * 2] = w;
                coords[(i * 2) + 1] = h;
            }
        }

        private GraphicsPath ConvertShape(GraphicsPath s)
        {
            GraphicsPath path = new GraphicsPath();
            PointF[] points = new PointF[s.PointCount];
            for (int i = 0; i < s.PointCount; i++)
            {
                PointF tf = s.PathPoints[i];
                Point2D source = new Point2D((double) tf.X, (double) tf.Y);
                Point2D pointd2 = base.chartObjScale.ConvertCoord(0, source, this.shapeCoordsType);
                points[i] = new PointF((float) pointd2.X, (float) pointd2.Y);
            }
            if ((s.PointCount > 0) && (s.PointCount < 3))
            {
                path.AddLine(points[0], points[1]);
                return path;
            }
            path.AddPolygon(points);
            return path;
        }

        public void Copy(ChartShape source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.shapeObj = source.shapeObj;
                this.shapeCoordsType = source.shapeCoordsType;
                this.shapeRotation = source.shapeRotation;
            }
        }

        public override void Draw(Graphics g2)
        {
            if (this.ErrorCheck(0) == 0)
            {
                this.PrePlot(g2);
                this.DrawShape(g2, base.thePath);
            }
        }

        public void DrawShape(Graphics g2, GraphicsPath path)
        {
            double num = -this.shapeRotation;
            if (this.shapeObj != null)
            {
                Matrix matrix = new Matrix();
                Point2D location = this.GetLocation();
                Point2D pointd2 = base.chartObjScale.ConvertCoord(this.shapeCoordsType, location, this.GetPositionType());
                matrix.Translate((float) pointd2.X, (float) pointd2.Y);
                matrix.Rotate((float) num);
                GraphicsPath s = (GraphicsPath) this.shapeObj.Clone();
                s.Transform(matrix);
                GraphicsPath addingPath = this.ConvertShape(s);
                base.chartObjScale.ConvertCoord(0, location, this.GetPositionType());
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
                base.boundingBox.Reset();
                if (this.GetChartObjEnable() == 1)
                {
                    path.AddPath(addingPath, false);
                    base.chartObjScale.DrawFillPath(g2, path);
                    base.boundingBox.AddPath(addingPath, false);
                    path.Reset();
                }
            }
        }

        public override int ErrorCheck(int nerror)
        {
            if ((nerror == 0) && (this.shapeObj == null))
            {
                nerror = 240;
            }
            return base.ErrorCheck(nerror);
        }

        public int GetShapeCoordsType()
        {
            return this.shapeCoordsType;
        }

        public GraphicsPath GetShapeObject()
        {
            return this.shapeObj;
        }

        public double GetShapeRotation()
        {
            return this.shapeRotation;
        }

        public void InitChartShapeObj(PhysicalCoordinates transform, GraphicsPath ashape, int shapecoordstype, double x, double y, int npostype, double rotation)
        {
            this.SetChartObjScale(transform);
            this.SetLocation(x, y);
            this.shapeObj = ashape;
            this.shapeRotation = rotation;
            this.SetPositionType(npostype);
            this.shapeCoordsType = shapecoordstype;
            if (npostype == 3)
            {
                base.chartObjClipping = 1;
            }
            else
            {
                base.chartObjClipping = 2;
            }
        }

        private void InitDefaults()
        {
            base.chartObjType = 0x25b;
            base.chartObjClipping = 2;
            base.moveableType = 1;
            this.InitChartShapeObj(base.chartObjScale, null, 1, 0.0, 0.0, 1, 0.0);
        }

        public void SetShapeCoordsType(int shapecoordstype)
        {
            this.shapeCoordsType = shapecoordstype;
        }

        public void SetShapeObj(GraphicsPath s)
        {
            this.shapeObj = s;
        }

        public void SetShapeRotation(double rotation)
        {
            this.shapeRotation = rotation;
        }

        public int ShapeCoordsType
        {
            get
            {
                return this.shapeCoordsType;
            }
            set
            {
                this.shapeCoordsType = value;
            }
        }

        public GraphicsPath ShapeObj
        {
            get
            {
                return this.shapeObj;
            }
            set
            {
                this.shapeObj = value;
            }
        }

        public double ShapeRotation
        {
            get
            {
                return this.shapeRotation;
            }
            set
            {
                this.shapeRotation = value;
            }
        }
    }
}

