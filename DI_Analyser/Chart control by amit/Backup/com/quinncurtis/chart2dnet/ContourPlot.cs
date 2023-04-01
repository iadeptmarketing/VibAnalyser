namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ContourPlot : ChartPlot
    {
        internal ContourDataset contourDataset;
        internal bool[] contourLabelFlags;
        internal double[] contourLevels;
        internal int contourLineAlgorithm;
        internal bool[] contourLineFlags;
        internal int contourType;
        internal int numContourLevels;
        internal bool polygonGridOn;

        public ContourPlot()
        {
            this.contourDataset = new ContourDataset();
            this.polygonGridOn = false;
            this.contourType = 1;
            this.contourLineAlgorithm = 1;
            this.InitDefaults();
        }

        public ContourPlot(PhysicalCoordinates transform)
        {
            this.contourDataset = new ContourDataset();
            this.polygonGridOn = false;
            this.contourType = 1;
            this.contourLineAlgorithm = 1;
            this.InitDefaults();
            this.SetChartObjScale(transform);
        }

        public ContourPlot(PhysicalCoordinates transform, ContourDataset dataset, double[] contourlevels, ChartAttribute[] attribs, int numcontourlevels, int contourtype)
        {
            this.contourDataset = new ContourDataset();
            this.polygonGridOn = false;
            this.contourType = 1;
            this.contourLineAlgorithm = 1;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitContourPlot(dataset, contourlevels, attribs, numcontourlevels, contourtype);
        }

        public ContourPlot(PhysicalCoordinates transform, ContourDataset dataset, double[] contourlevels, ChartAttribute[] attribs, bool[] blineflags, bool[] blabelflags, int numcontourlevels, int contourtype)
        {
            this.contourDataset = new ContourDataset();
            this.polygonGridOn = false;
            this.contourType = 1;
            this.contourLineAlgorithm = 1;
            this.InitDefaults();
            this.SetChartObjScale(transform);
            this.InitContourPlot(dataset, contourlevels, attribs, blineflags, blabelflags, numcontourlevels, contourtype);
        }

        public override bool CalcNearestPoint(Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            return ChartSupport.CalcNearestPoint(base.chartObjScale, this.contourDataset, base.coordinateSwap, testpoint, nmode, nearestpoint);
        }

        private bool CheckForPolyContour3D(Point3D[] pv, int n, double contour, int[] order)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            bool flag = false;
            Point3D pointd = new Point3D();
            Point3D pointd2 = new Point3D();
            for (int i = 0; i < n; i++)
            {
                pointd.SetLocation(pv[i]);
                pointd2.SetLocation(pv[(i + 1) % n]);
                if (((pointd.GetZ() <= contour) && (pointd2.GetZ() >= contour)) || ((pointd.GetZ() >= contour) && (pointd2.GetZ() <= contour)))
                {
                    flag = true;
                    num3++;
                    if (num3 == 1)
                    {
                        num = i;
                    }
                    else
                    {
                        num2 = i;
                        break;
                    }
                }
            }
            order[0] = num;
            order[1] = num2;
            pointd = null;
            pointd2 = null;
            return flag;
        }

        private int CheckTriNumContours(Point3D[] pv, int numpoints, int[] minmax)
        {
            int num3;
            int num = 0;
            int num2 = 0;
            int[] numArray = new int[numpoints];
            int num4 = 0;
            for (num3 = 0; num3 < numpoints; num3++)
            {
                numArray[num3] = this.FindContourIndex(pv[num3].GetZ());
            }
            num = num2 = numArray[0];
            for (num3 = 1; num3 < numpoints; num3++)
            {
                if (numArray[num3] < num)
                {
                    num = numArray[num3];
                }
                if (numArray[num3] > num2)
                {
                    num2 = numArray[num3];
                }
            }
            num4 = (num2 - num) + 1;
            minmax[0] = num;
            minmax[1] = num2;
            return num4;
        }

        public override object Clone()
        {
            ContourPlot plot = new ContourPlot();
            plot.Copy(this);
            return plot;
        }

        public virtual void Copy(ContourPlot source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.contourDataset = (ContourDataset) source.contourDataset.Clone();
                this.numContourLevels = source.numContourLevels;
                this.contourLevels = new double[this.numContourLevels];
                this.contourLineFlags = new bool[this.numContourLevels];
                this.contourLabelFlags = new bool[this.numContourLevels];
                for (int i = 0; i < this.numContourLevels; i++)
                {
                    this.contourLevels[i] = source.contourLevels[i];
                    this.contourLineFlags[i] = source.contourLineFlags[i];
                    this.contourLabelFlags[i] = source.contourLabelFlags[i];
                }
                this.polygonGridOn = source.polygonGridOn;
                this.contourType = source.contourType;
                this.contourLineAlgorithm = source.contourLineAlgorithm;
            }
        }

        private GraphicsPath DefinePolysurfaceArea(Graphics g2, Point3D[] pv, int n)
        {
            PointF[] points = new PointF[n];
            Point2D dest = new Point2D();
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < n; i++)
            {
                dest.SetLocation(pv[i].GetX(), pv[i].GetY());
                base.chartObjScale.ConvertCoord(dest, 0, dest, 1);
                points[i] = new PointF((float) dest.GetX(), (float) dest.GetY());
            }
            path.AddPolygon(points);
            points = null;
            dest = null;
            return path;
        }

        public override void Draw(Graphics g2)
        {
            if ((this.GetChartObjEnable() == 1) && (this.ErrorCheck(0) == 0))
            {
                this.PrePlot(g2);
                if ((this.contourType == 2) || (this.contourType == 1))
                {
                    this.DrawFilledContourChart(g2);
                }
                if ((this.contourType == 2) || (this.contourType == 0))
                {
                    this.DrawLineContourChart(g2);
                }
            }
        }

        private void DrawFilledContourChart(Graphics g2)
        {
            int num;
            Polysurface polysurface = null;
            int num3;
            polysurface = this.contourDataset.GetPolysurface();
            int pSNumPolygons = polysurface.GetPSNumPolygons();
            int[] numArray1 = new int[2];
            Point3D[] points = new Point3D[6];
            ChartAttribute attrib = new ChartAttribute(Color.Black, 1.0, DashStyle.Solid, Color.Blue);
            for (num = 0; num < 6; num++)
            {
                points[num] = new Point3D();
            }
            for (num = 0; num < pSNumPolygons; num++)
            {
                num3 = polysurface.GPSPolygon(num, points);
                this.OrderPolygon(points);
                this.ProcessTriContours(g2, points, num3);
            }
            if (this.polygonGridOn)
            {
                for (num = 0; num < pSNumPolygons; num++)
                {
                    num3 = polysurface.GPSPolygon(num, points);
                    attrib = this.GetChartObjAttributes();
                    attrib.SetFillFlag(false);
                    this.DrawPolysurfacePolygon(g2, attrib, points, num3);
                }
            }
        }

        private void DrawLineContourChart(Graphics g2)
        {
            if (this.contourLineAlgorithm == 0)
            {
                this.DrawLineWalkContourChart(g2);
            }
            else if (this.contourLineAlgorithm == 1)
            {
                this.DrawLinePolygonContourChart(g2);
            }
        }

        private void DrawLinePolygonContourChart(Graphics g2)
        {
            int num;
            Polysurface polysurface = null;
            int num3;
            polysurface = this.contourDataset.GetPolysurface();
            int pSNumPolygons = polysurface.GetPSNumPolygons();
            int[] numArray1 = new int[2];
            Point3D[] points = new Point3D[6];
            ChartAttribute attrib = new ChartAttribute(Color.Black, 1.0, DashStyle.Solid, Color.Blue);
            for (num = 0; num < 6; num++)
            {
                points[num] = new Point3D();
            }
            for (num = 0; num < pSNumPolygons; num++)
            {
                num3 = polysurface.GPSPolygon(num, points);
                this.OrderPolygon(points);
                this.DrawMultiContourTriangle(g2, points, num3);
            }
            if (this.polygonGridOn)
            {
                for (num = 0; num < pSNumPolygons; num++)
                {
                    num3 = polysurface.GPSPolygon(num, points);
                    attrib = this.GetChartObjAttributes();
                    attrib.SetFillFlag(false);
                    this.DrawPolysurfacePolygon(g2, attrib, points, num3);
                }
            }
        }

        private void DrawLineWalkContourChart(Graphics g2)
        {
            int numtri = 0;
            ChartAttribute attrib = new ChartAttribute(base.chartObjAttributes);
            int[] startstoppoly = new int[2];
            Polysurface psf = null;
            NumericLabel plotLabelTemplate = base.GetPlotLabelTemplate();
            plotLabelTemplate.SetChartObjClipping(3);
            plotLabelTemplate.SetXJust(1);
            plotLabelTemplate.SetYJust(1);
            base.chartObjScale.SetCurrentAttributes(attrib);
            psf = this.contourDataset.GetPolysurface();
            if (psf != null)
            {
                int pSNumPolygons = psf.GetPSNumPolygons();
                bool[] pchecked = new bool[pSNumPolygons];
                int[] ctrilist = new int[pSNumPolygons];
                for (int i = 0; i < this.numContourLevels; i++)
                {
                    double num4 = this.contourLevels[i];
                    num4 += 1E-10;
                    attrib.Copy(base.GetSegmentAttributes(i));
                    for (int j = 0; j < pSNumPolygons; j++)
                    {
                        pchecked[j] = false;
                    }
                    int num5 = 0;
                    int num6 = 0;
                    do
                    {
                        startstoppoly[0] = num5;
                        numtri = this.GetContourPolyList(psf, num4, startstoppoly, ctrilist, pchecked);
                        num6 = startstoppoly[1];
                        if (numtri == 0)
                        {
                            break;
                        }
                        this.PlotContourSegment2D(g2, psf, ctrilist, numtri, num4, attrib, this.contourLineFlags[i], this.contourLabelFlags[i], plotLabelTemplate);
                        num5 = num6 + 1;
                    }
                    while (num5 < pSNumPolygons);
                }
                pchecked = null;
                ctrilist = null;
            }
        }

        private void DrawMultiContourTriangle(Graphics g2, Point3D[] pv, int numpoints)
        {
            int num;
            Point3D startp = new Point3D();
            Point3D stopp = new Point3D();
            int[] order = new int[2];
            Point3D[] pointdArray = new Point3D[6];
            int index = 0;
            ChartAttribute attrib = new ChartAttribute();
            int num3 = 0;
            int[] minmax = new int[2];
            num3 = this.CheckTriNumContours(pv, numpoints, minmax);
            for (num = 0; num < 6; num++)
            {
                pointdArray[num] = new Point3D();
            }
            if (num3 > 1)
            {
                for (num = minmax[0]; num <= minmax[1]; num++)
                {
                    index = ChartSupport.ClampInt(num, 0, this.numContourLevels);
                    if (this.contourLineFlags[index])
                    {
                        this.InterpolatePolyContour3D(pv, numpoints, this.contourLevels[index], startp, stopp, order);
                        attrib.Copy(base.GetSegmentAttributes(index));
                        base.thePath.Reset();
                        base.chartObjScale.SetCurrentAttributes(attrib);
                        base.chartObjScale.WLineAbs(base.thePath, startp.GetX(), startp.GetY(), stopp.GetX(), stopp.GetY());
                        base.chartObjScale.DrawPath(g2, attrib.GetCurrentPen(), base.thePath);
                        base.thePath.Reset();
                    }
                }
            }
        }

        private void DrawPolysurfaceArea(Graphics g2, ChartAttribute attrib, GraphicsPath areaobj)
        {
            Point2D[] pointdArray1 = new Point2D[6];
            new Point2D();
            GraphicsPath path = new GraphicsPath();
            base.chartObjScale.SetCurrentAttributes(attrib);
            path.AddPath(areaobj, false);
            base.chartObjScale.DrawFillPath(g2, path);
        }

        private void DrawPolysurfacePolygon(Graphics g2, ChartAttribute attrib, Point3D[] pv, int n)
        {
            PointF[] points = new PointF[n];
            Point2D dest = new Point2D();
            GraphicsPath path = new GraphicsPath();
            base.chartObjScale.SetCurrentAttributes(attrib);
            for (int i = 0; i < n; i++)
            {
                dest.SetLocation(pv[i].GetX(), pv[i].GetY());
                base.chartObjScale.ConvertCoord(dest, 0, dest, 1);
                points[i] = new PointF((float) dest.GetX(), (float) dest.GetY());
            }
            path.AddPolygon(points);
            base.chartObjScale.DrawFillPath(g2, path);
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.contourDataset == null)
                {
                    nerror = 520;
                }
                else
                {
                    nerror = this.contourDataset.ErrorCheck(nerror);
                }
            }
            return base.ErrorCheck(nerror);
        }

        private void FillMultiContourTriangle(Graphics g2, Point3D[] pv, int numpoints, int[] minmax)
        {
            int num;
            Point3D startp = new Point3D();
            Point3D stopp = new Point3D();
            new Point3D();
            new Point3D();
            int[] order = new int[2];
            Point3D[] pointdArray = new Point3D[6];
            int n = 4;
            int num3 = 0;
            int index = 0;
            ChartAttribute attrib = new ChartAttribute();
            new GraphicsPath();
            GraphicsPath areaobj = new GraphicsPath();
            new GraphicsPath();
            for (num = 0; num < 6; num++)
            {
                pointdArray[num] = new Point3D();
            }
            for (num = minmax[0]; num <= minmax[1]; num++)
            {
                index = ChartSupport.ClampInt(num, 0, this.numContourLevels);
                this.InterpolatePolyContour3D(pv, numpoints, this.contourLevels[index], startp, stopp, order);
                if (((order[0] == 0) && (order[1] == 1)) || ((order[1] == 0) && (order[0] == 1)))
                {
                    n = 4;
                    pointdArray[0].SetLocation(pv[0]);
                    pointdArray[1].SetLocation(startp);
                    pointdArray[2].SetLocation(stopp);
                    pointdArray[3].SetLocation(pv[2]);
                }
                else if (((order[0] == 0) && (order[1] == 2)) || ((order[1] == 0) && (order[0] == 2)))
                {
                    n = 3;
                    pointdArray[0].SetLocation(pv[0]);
                    pointdArray[1].SetLocation(startp);
                    pointdArray[2].SetLocation(stopp);
                }
                else if (((order[0] == 1) && (order[1] == 2)) || ((order[1] == 1) && (order[0] == 2)))
                {
                    n = 4;
                    pointdArray[0].SetLocation(pv[0]);
                    pointdArray[1].SetLocation(pv[1]);
                    pointdArray[2].SetLocation(startp);
                    pointdArray[3].SetLocation(stopp);
                }
                else
                {
                    n = 3;
                    pointdArray[0].SetLocation(pv[0]);
                    pointdArray[1].SetLocation(pv[1]);
                    pointdArray[2].SetLocation(pv[2]);
                }
                num3++;
                GraphicsPath path1 = (GraphicsPath) areaobj.Clone();
                areaobj = this.DefinePolysurfaceArea(g2, pointdArray, n);
                GraphicsPath path2 = (GraphicsPath) areaobj.Clone();
                attrib.Copy(base.GetSegmentAttributes(index));
                attrib.SetFillFlag(true);
                attrib.SetLineFlag(false);
                this.DrawPolysurfaceArea(g2, attrib, areaobj);
            }
        }

        private void FillOneContourTriangle(Graphics g2, Point3D[] pv, int numpoints, int[] minmax)
        {
            int nsegment = minmax[0];
            if (nsegment >= 0)
            {
                if (nsegment > (this.numContourLevels - 1))
                {
                    nsegment = this.numContourLevels - 1;
                }
                ChartAttribute attrib = new ChartAttribute(Color.Black, 1.0, DashStyle.Solid);
                attrib.Copy(base.GetSegmentAttributes(nsegment));
                attrib.SetFillFlag(true);
                this.DrawPolysurfacePolygon(g2, attrib, pv, numpoints);
            }
        }

        private int FindContourIndex(double contourvalue1)
        {
            int index = 0;
            int num2 = 0;
            if (contourvalue1 < this.contourLevels[0])
            {
                return -1;
            }
            index = 0;
            while (index < (this.numContourLevels - 1))
            {
                if ((contourvalue1 >= this.contourLevels[index]) && (contourvalue1 < this.contourLevels[index + 1]))
                {
                    num2 = index;
                    break;
                }
                index++;
            }
            if (index == (this.numContourLevels - 1))
            {
                num2 = index;
            }
            return num2;
        }

        public virtual bool GetContourLabelFlag(int index)
        {
            bool flag = false;
            if (index < this.numContourLevels)
            {
                flag = this.contourLabelFlags[index];
            }
            return flag;
        }

        public virtual double GetContourLevel(int index)
        {
            double num = 0.0;
            if (index < this.numContourLevels)
            {
                num = this.contourLevels[index];
            }
            return num;
        }

        public virtual int GetContourLineAlgorithm()
        {
            return this.contourLineAlgorithm;
        }

        public virtual bool GetContourLineFlag(int index)
        {
            bool flag = false;
            if (index < this.numContourLevels)
            {
                flag = this.contourLineFlags[index];
            }
            return flag;
        }

        private int GetContourPolyList(Polysurface psf, double contourvalue1, int[] startstoppoly, int[] ctrilist, bool[] pchecked)
        {
            int num;
            Point3D[] points = new Point3D[6];
            new Point3D();
            new Point3D();
            int[] order = new int[2];
            bool flag = false;
            int index = 0;
            Color black = Color.Black;
            Color bordercolor = Color.Black;
            int[] adjlist = new int[3];
            for (num = 0; num < 6; num++)
            {
                points[num] = new Point3D();
            }
            int num10 = startstoppoly[0];
            int pSNumPolygons = psf.GetPSNumPolygons();
            int num2 = 0;
            num = num10;
            while (num < pSNumPolygons)
            {
                if (pchecked[num])
                {
                    goto Label_01A3;
                }
                int n = psf.GetPolysurfacePolygon(num, points, black, bordercolor);
                flag = this.CheckForPolyContour3D(points, n, contourvalue1, order);
                int num6 = order[0];
                int num7 = order[1];
                int num12 = num6;
                int num13 = num7;
                if (!flag)
                {
                    goto Label_01A3;
                }
                int num9 = num;
                pchecked[num] = true;
                int tri = num9;
                int num11 = 0;
                index = 0;
            Label_00AE:
                ctrilist[index] = tri;
                index++;
                psf.GetAdjacentPolyList(tri, adjlist);
                tri = adjlist[num7];
                if ((index == 1) && (tri == -1))
                {
                    tri = adjlist[num6];
                }
                if (tri != -1)
                {
                    if (tri >= pSNumPolygons)
                    {
                        goto Label_019B;
                    }
                    if (pchecked[tri])
                    {
                        tri = adjlist[num6];
                    }
                    if (tri >= pSNumPolygons)
                    {
                        goto Label_019B;
                    }
                }
                if (tri == -1)
                {
                    num11++;
                    if (num11 == 2)
                    {
                        goto Label_019B;
                    }
                    tri = num9;
                    for (int i = 0; i < (index / 2); i++)
                    {
                        int num15 = ctrilist[i];
                        ctrilist[i] = ctrilist[(index - i) - 1];
                        ctrilist[(index - i) - 1] = num15;
                    }
                    num7 = num13;
                    num6 = num12;
                    index--;
                    goto Label_00AE;
                }
                if (!pchecked[tri])
                {
                    n = psf.GetPolysurfacePolygon(tri, points, black, bordercolor);
                    flag = this.CheckForPolyContour3D(points, n, contourvalue1, order);
                    num6 = order[0];
                    num7 = order[1];
                    if (flag)
                    {
                        pchecked[tri] = true;
                        goto Label_00AE;
                    }
                }
            Label_019B:
                num2++;
                break;
            Label_01A3:
                num++;
            }
            startstoppoly[1] = num;
            return index;
        }

        public virtual int GetContourType()
        {
            return this.contourType;
        }

        public override ChartDataset GetDataset()
        {
            return this.contourDataset;
        }

        public virtual int GetNumContourLevels()
        {
            return this.numContourLevels;
        }

        public virtual bool GetPolygonGridOn()
        {
            return this.polygonGridOn;
        }

        public void InitContourPlot(ContourDataset dataset, double[] contourlevels, ChartAttribute[] attribs, int numcontourlevels, int contourtype)
        {
            int num;
            this.contourDataset = dataset;
            this.numContourLevels = numcontourlevels;
            this.contourLevels = new double[this.numContourLevels];
            for (num = 0; num < this.numContourLevels; num++)
            {
                this.contourLevels[num] = contourlevels[num];
            }
            base.InitSegmentAttributes(attribs, (int) (this.numContourLevels + 1));
            this.contourLineFlags = new bool[this.numContourLevels];
            for (num = 0; num < this.numContourLevels; num++)
            {
                this.contourLineFlags[num] = true;
            }
            this.contourLabelFlags = new bool[this.numContourLevels];
            for (num = 0; num < this.numContourLevels; num++)
            {
                this.contourLabelFlags[num] = true;
            }
            this.numContourLevels = numcontourlevels;
            this.contourType = contourtype;
        }

        public void InitContourPlot(ContourDataset dataset, double[] contourlevels, ChartAttribute[] attribs, bool[] blineflags, bool[] blabelflags, int numcontourlevels, int contourtype)
        {
            int num;
            this.InitContourPlot(dataset, contourlevels, attribs, numcontourlevels, contourtype);
            this.contourLineFlags = new bool[this.numContourLevels];
            for (num = 0; num < this.numContourLevels; num++)
            {
                this.contourLineFlags[num] = blineflags[num];
            }
            this.contourLabelFlags = new bool[this.numContourLevels];
            for (num = 0; num < this.numContourLevels; num++)
            {
                this.contourLabelFlags[num] = blabelflags[num];
            }
        }

        private void InitDefaults()
        {
            base.chartObjType = 70;
            base.chartObjClipping = 2;
            base.moveableType = 0;
        }

        private void Interpolatepoint3D(Point3D known1, Point3D known2, Point3D partknown, int plane)
        {
            double px = 0.0;
            double py = 0.0;
            double pz = 0.0;
            double num = known2.GetX() - known1.GetX();
            double num2 = known2.GetY() - known1.GetY();
            double num3 = known2.GetZ() - known1.GetZ();
            switch (plane)
            {
                case 0:
                case 3:
                    px = known1.GetX() + ((num * (partknown.GetZ() - known1.GetZ())) / num3);
                    py = known1.GetY() + ((num2 * (partknown.GetZ() - known1.GetZ())) / num3);
                    partknown.SetLocation(px, py, partknown.GetZ());
                    return;

                case 1:
                case 4:
                    px = known1.GetX() + ((num * (partknown.GetY() - known1.GetY())) / num2);
                    pz = known1.GetZ() + ((num3 * (partknown.GetY() - known1.GetY())) / num2);
                    partknown.SetLocation(px, partknown.GetY(), pz);
                    return;

                case 2:
                case 5:
                    py = known1.GetY() + ((num2 * (partknown.GetX() - known1.GetX())) / num);
                    pz = known1.GetZ() + ((num3 * (partknown.GetX() - known1.GetX())) / num);
                    partknown.SetLocation(partknown.GetX(), py, pz);
                    return;
            }
        }

        private bool InterpolatePolyContour3D(Point3D[] pv, int n, double contour, Point3D startp, Point3D stopp, int[] order)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            bool flag = true;
            Point3D pointd = new Point3D();
            Point3D pointd2 = new Point3D();
            Point3D partknown = new Point3D();
            for (int i = 0; i < n; i++)
            {
                pointd.SetLocation(pv[i]);
                pointd2.SetLocation(pv[(i + 1) % n]);
                double pz = contour;
                partknown.SetLocation(0.0, 0.0, pz);
                if (((pointd.GetZ() < contour) && (pointd2.GetZ() > contour)) || ((pointd.GetZ() > contour) && (pointd2.GetZ() < contour)))
                {
                    if (Math.Abs((double) (pointd.GetZ() - pointd2.GetZ())) > 1E-20)
                    {
                        this.Interpolatepoint3D(pointd, pointd2, partknown, 0);
                        flag = true;
                        num3++;
                        if (num3 == 1)
                        {
                            startp.SetLocation(partknown);
                            num = i;
                            goto Label_00EB;
                        }
                        stopp.SetLocation(partknown);
                        num2 = i;
                        break;
                    }
                    flag = false;
                    startp.SetLocation(pointd);
                    stopp.SetLocation(pointd2);
                Label_00EB:;
                }
            }
            order[0] = num;
            order[1] = num2;
            return flag;
        }

        private void OrderPolygon(Point3D[] source)
        {
            Point3D p = new Point3D();
            if (source[0].GetZ() < source[1].GetZ())
            {
                p.SetLocation(source[1]);
                source[1].SetLocation(source[0]);
                source[0].SetLocation(p);
            }
            if (source[0].GetZ() < source[2].GetZ())
            {
                p.SetLocation(source[2]);
                source[2].SetLocation(source[0]);
                source[0].SetLocation(p);
            }
        }

        private void OutContourLabel2D(Graphics g2, Point3D pnt1, Point3D pnt2, NumericLabel textobj)
        {
            double rotation = 0.0;
            Point3D p = new Point3D();
            p.SetLocation(pnt1);
            Point3D pointd2 = new Point3D();
            pointd2.SetLocation(pnt2);
            double x = pointd2.GetX() - p.GetX();
            if (x < 0.0)
            {
                x = -x;
                Point3D pointd3 = new Point3D();
                pointd3.SetLocation(p);
                p.SetLocation(pointd2);
                pointd2.SetLocation(pointd3);
            }
            double y = pointd2.GetY() - p.GetY();
            rotation = 57.295779513082323 * Math.Atan2(y, x);
            if ((rotation > 90.0) && (rotation < 270.0))
            {
                rotation += 180.0;
            }
            else if ((rotation < -90.0) && (rotation > -270.0))
            {
                rotation += 180.0;
            }
            textobj.SetLocation(p.GetX() + (x / 2.0), p.GetY() + (y / 2.0), 1);
            textobj.SetTextRotation(rotation);
            textobj.Draw(g2);
        }

        private void PlotContourSegment2D(Graphics g2, Polysurface psf, int[] ctrilist, int numtri, double contour, ChartAttribute attrib, bool bdrawline, bool binsertlabel, NumericLabel textobj)
        {
            int num;
            Point3D[] points = new Point3D[6];
            Point3D startp = new Point3D();
            Point3D stopp = new Point3D();
            Point3D pointd3 = new Point3D();
            Point3D pointd4 = new Point3D();
            int[] order = new int[2];
            int num3 = -1;
            for (num = 0; num < 6; num++)
            {
                points[num] = new Point3D();
            }
            double pz = 0.0;
            if (binsertlabel)
            {
                textobj.SetNumericValue(contour);
                num3 = 2 + ((int) ((numtri - 4) * ChartSupport.GetRandomDouble()));
                num3 = Math.Min(Math.Max(num3, 1), numtri - 2);
            }
            pointd3.SetLocation(0.0, 0.0, pz);
            pointd4.SetLocation(0.0, 0.0, pz);
            base.thePath.Reset();
            base.chartObjScale.SetCurrentAttributes(attrib);
            for (num = 0; num < numtri; num++)
            {
                int num2 = psf.GPSPolygon(ctrilist[num], points);
                if (this.InterpolatePolyContour3D(points, num2 + 1, contour, startp, stopp, order))
                {
                    if (bdrawline)
                    {
                        base.chartObjScale.WLineAbs(g2, base.thePath, startp.GetX(), startp.GetY(), stopp.GetX(), stopp.GetY(), attrib.CurrentPen, true, false);
                    }
                    if (binsertlabel && (num == num3))
                    {
                        pointd3.SetLocation(startp);
                        pointd4.SetLocation(stopp);
                    }
                }
            }
            if (binsertlabel)
            {
                this.OutContourLabel2D(g2, pointd3, pointd4, textobj);
            }
            startp = null;
            stopp = null;
            pointd3 = null;
            pointd4 = null;
        }

        private void ProcessTriContours(Graphics g2, Point3D[] pv, int numpoints)
        {
            int[] minmax = new int[2];
            if (this.CheckTriNumContours(pv, numpoints, minmax) == 1)
            {
                this.FillOneContourTriangle(g2, pv, numpoints, minmax);
            }
            else
            {
                this.FillMultiContourTriangle(g2, pv, numpoints, minmax);
            }
        }

        public virtual void SetContourLabelFlag(int index, bool bvalue1)
        {
            if (index < this.numContourLevels)
            {
                this.contourLabelFlags[index] = bvalue1;
            }
        }

        public virtual void SetContourLevel(int index, double value)
        {
            if (index < this.numContourLevels)
            {
                this.contourLevels[index] = value;
            }
        }

        public virtual void SetContourLineAlgorithm(int contourlinealgorithm)
        {
            this.contourLineAlgorithm = contourlinealgorithm;
        }

        public virtual void SetContourLineFlag(int index, bool bvalue1)
        {
            if (index < this.numContourLevels)
            {
                this.contourLineFlags[index] = bvalue1;
            }
        }

        public void SetContourPlotAttributes(double[] contourlevels, ChartAttribute[] attribs, int numcontourlevels, int contourtype)
        {
            this.InitContourPlot(this.contourDataset, contourlevels, attribs, numcontourlevels, contourtype);
        }

        public void SetContourPlotAttributes(double[] contourlevels, ChartAttribute[] attribs, bool[] blineflags, bool[] blabelflags, int numcontourlevels, int contourtype)
        {
            this.InitContourPlot(this.contourDataset, contourlevels, attribs, blineflags, blabelflags, numcontourlevels, contourtype);
        }

        public virtual void SetContourType(int contourtype)
        {
            this.contourType = contourtype;
        }

        public virtual void SetDataset(ContourDataset dataset)
        {
            this.contourDataset = dataset;
        }

        public virtual void SetPolygonGridOn(bool bvalue1)
        {
            this.polygonGridOn = bvalue1;
        }

        public int ContourType
        {
            get
            {
                return this.contourType;
            }
            set
            {
                this.contourType = value;
            }
        }

        public int NumContourLevels
        {
            get
            {
                return this.numContourLevels;
            }
        }

        public bool PolygonGridOn
        {
            get
            {
                return this.polygonGridOn;
            }
            set
            {
                this.polygonGridOn = value;
            }
        }
    }
}

