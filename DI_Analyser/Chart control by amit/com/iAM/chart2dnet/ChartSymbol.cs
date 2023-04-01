namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ChartSymbol : GraphObj
    {
        internal int maxSymbolNum;
        internal int symbolNumber;
        internal double symbolRotation;
        internal GraphicsPath symbolShape;

        public ChartSymbol()
        {
            this.symbolNumber = 1;
            this.symbolShape = null;
            this.maxSymbolNum = 11;
            this.symbolRotation = 0.0;
            this.InitDefaults();
        }

        public ChartSymbol(PhysicalCoordinates transform)
        {
            this.symbolNumber = 1;
            this.symbolShape = null;
            this.maxSymbolNum = 11;
            this.symbolRotation = 0.0;
            this.SetChartObjScale(transform);
        }

        public ChartSymbol(PhysicalCoordinates transform, GraphicsPath symbolshape, ChartAttribute attrib)
        {
            this.symbolNumber = 1;
            this.symbolShape = null;
            this.maxSymbolNum = 11;
            this.symbolRotation = 0.0;
            this.SetChartObjScale(transform);
            this.symbolShape = symbolshape;
            base.chartObjAttributes.Copy(attrib);
        }

        public ChartSymbol(PhysicalCoordinates transform, int nsymbol, ChartAttribute attrib)
        {
            this.symbolNumber = 1;
            this.symbolShape = null;
            this.maxSymbolNum = 11;
            this.symbolRotation = 0.0;
            this.SetChartObjScale(transform);
            this.InitChartSymbol(nsymbol, attrib);
        }

        public GraphicsPath CalcSymbolShape(int nsymbol)
        {
            switch (nsymbol)
            {
                case 0:
                    return this.GetNoSymbolShape();

                case 1:
                    return this.GetSquareShape();

                case 2:
                    return this.GetUpTriangleShape();

                case 3:
                    return this.GetDownTriangleShape();

                case 4:
                    return this.GetDiamondShape();

                case 5:
                    return this.GetCrossShape();

                case 6:
                    return this.GetPlusShape();

                case 7:
                    return this.GetStarShape();

                case 8:
                    return this.GetLineShape();

                case 9:
                    return this.GetHBarShape();

                case 10:
                    return this.GetVBarShape();

                case 11:
                    return this.GetCircleShape();
            }
            return this.GetSquareShape();
        }

        public override bool CheckIntersection(Point2D testpoint, NearestPointData np)
        {
            return this.DefaultCheckIntersection(testpoint, np);
        }

        public override object Clone()
        {
            ChartSymbol symbol = new ChartSymbol();
            symbol.Copy(this);
            return symbol;
        }

        public void Copy(ChartSymbol source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.symbolNumber = source.symbolNumber;
                this.maxSymbolNum = source.maxSymbolNum;
                if (source.symbolShape == null)
                {
                    this.symbolShape = this.CalcSymbolShape(this.symbolNumber);
                }
                else
                {
                    this.symbolShape = (GraphicsPath) source.symbolShape.Clone();
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                this.DrawSymbol(g2);
            }
        }

        private void DrawSymbol(Graphics g2)
        {
            Point2D pointd = base.chartObjScale.ConvertCoord(0, this.GetLocation(), base.positionType);
            double num = base.chartObjAttributes.GetSymbolSize() * base.resizeMultiplier;
            double num2 = -this.symbolRotation;
            Matrix matrix = new Matrix();
            matrix.Translate((float) pointd.GetX(), (float) pointd.GetY());
            matrix.Rotate((float) num2);
            matrix.Scale((float) num, (float) num);
            matrix.Translate(-0.5f, -0.5f);
            GraphicsPath addingPath = (GraphicsPath) this.symbolShape.Clone();
            addingPath.Transform(matrix);
            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            base.boundingBox.Reset();
            base.thePath.AddPath(addingPath, false);
            base.boundingBox.AddPath(addingPath, false);
            base.chartObjScale.DrawFillPath(g2, base.thePath);
            base.thePath.Reset();
        }

        public override int ErrorCheck(int nerror)
        {
            return base.ErrorCheck(nerror);
        }

        public GraphicsPath GetCircleShape()
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(0f, 0f, 1f, 1f);
            path.AddEllipse(rect);
            return path;
        }

        public GraphicsPath GetCrossShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0f, (float) 0.2f, (float) 0.3f, (float) 0.5f);
            path.AddLine((float) 0f, (float) 0.8f, (float) 0.2f, (float) 1f);
            path.AddLine((float) 0.5f, (float) 0.7f, (float) 0.8f, (float) 1f);
            path.AddLine((float) 1f, (float) 0.8f, (float) 0.7f, (float) 0.5f);
            path.AddLine((float) 1f, (float) 0.2f, (float) 0.8f, (float) 0f);
            path.AddLine((float) 0.5f, (float) 0.3f, (float) 0.2f, (float) 0f);
            path.CloseFigure();
            return path;
        }

        public GraphicsPath GetDiamondShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0.5f, (float) 0f, (float) 0f, (float) 0.5f);
            path.AddLine((float) 0f, (float) 0.5f, (float) 0.5f, (float) 1f);
            path.AddLine((float) 0.5f, (float) 1f, (float) 1f, (float) 0.5f);
            path.CloseFigure();
            return path;
        }

        public GraphicsPath GetDownTriangleShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0f, (float) 0f, (float) 0.5f, (float) 1f);
            path.AddLine((float) 0.5f, (float) 1f, (float) 1f, (float) 0f);
            path.CloseFigure();
            return path;
        }

        public GraphicsPath GetHBarShape()
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(0f, 0.25f, 1f, 0.5f);
            path.AddRectangle(rect);
            return path;
        }

        public GraphicsPath GetLineShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0f, (float) 0.5f, (float) 1f, (float) 0.5f);
            return path;
        }

        public GraphicsPath GetNoSymbolShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0.5f, (float) 0.5f, (float) 0.5f, (float) 0.5f);
            return path;
        }

        public GraphicsPath GetPlusShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0.4f, (float) 0f, (float) 0.4f, (float) 0.4f);
            path.AddLine((float) 0f, (float) 0.4f, (float) 0f, (float) 0.6f);
            path.AddLine((float) 0.4f, (float) 0.6f, (float) 0.4f, (float) 1f);
            path.AddLine((float) 0.6f, (float) 1f, (float) 0.6f, (float) 0.6f);
            path.AddLine((float) 1f, (float) 0.6f, (float) 1f, (float) 0.4f);
            path.AddLine((float) 0.6f, (float) 0.4f, (float) 0.6f, (float) 0f);
            path.CloseFigure();
            return path;
        }

        public GraphicsPath GetSquareShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle2D(0.0, 0.0, 1.0, 1.0).GetRectangleF());
            return path;
        }

        public GraphicsPath GetStarShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0.5f, (float) 0f, (float) 0.15f, (float) 1f);
            path.AddLine((float) 1f, (float) 0.4f, (float) 0f, (float) 0.4f);
            path.AddLine((float) 0f, (float) 0.4f, (float) 0.85f, (float) 1f);
            path.CloseFigure();
            return path;
        }

        public int GetSymbolNumber()
        {
            return this.symbolNumber;
        }

        public GraphicsPath GetSymbolShape()
        {
            return this.symbolShape;
        }

        public double GetSymbolSize()
        {
            return base.chartObjAttributes.GetSymbolSize();
        }

        public GraphicsPath GetUpTriangleShape()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine((float) 0f, (float) 1f, (float) 0.5f, (float) 0f);
            path.AddLine((float) 0.5f, (float) 0f, (float) 1f, (float) 1f);
            path.CloseFigure();
            return path;
        }

        public GraphicsPath GetVBarShape()
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(0.25f, 0f, 0.5f, 1f);
            path.AddRectangle(rect);
            return path;
        }

        public void InitChartSymbol(int nsymbol, ChartAttribute attrib)
        {
            this.InitDefaults();
            this.symbolNumber = nsymbol;
            this.symbolShape = this.CalcSymbolShape(this.symbolNumber);
            base.chartObjAttributes.Copy(attrib);
        }

        public void InitDefaults()
        {
            base.chartObjType = 0x25c;
            base.chartObjClipping = 3;
            base.moveableType = 0;
        }

        public void SetSymbolNumber(int symbol)
        {
            this.symbolNumber = symbol;
            this.symbolShape = this.CalcSymbolShape(this.symbolNumber);
        }

        public void SetSymbolShape(GraphicsPath GraphicsPath)
        {
            this.symbolShape = GraphicsPath;
            this.symbolNumber = 12;
        }

        public void SetSymbolSize(double size)
        {
            base.chartObjAttributes.SetSymbolSize(size);
        }

        public int SymbolNumber
        {
            get
            {
                return this.symbolNumber;
            }
            set
            {
                this.symbolNumber = value;
                this.symbolShape = this.CalcSymbolShape(this.symbolNumber);
            }
        }

        public double SymbolRotation
        {
            get
            {
                return this.symbolRotation;
            }
            set
            {
                this.symbolRotation = value;
            }
        }

        public double SymbolSize
        {
            get
            {
                return base.chartObjAttributes.GetSymbolSize();
            }
            set
            {
                base.chartObjAttributes.SetSymbolSize(value);
            }
        }
    }
}

