namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;

    public class Polysurface : ChartObj
    {
        private int[] adjpolygonlist;
        private bool adjtriflag;
        private bool bEvenGrid;
        private int BI;
        private bool bWireFrame;
        private bool delaunayinitf;
        private int[] delaunaylist;
        private int[] delaunaystack;
        private int[] Edofs;
        private int[] Edpntr;
        private int maxStack;
        private int nGridType;
        private int numc;
        private int numtriangles;
        private int RGColumns;
        private double RGMaxX;
        private double RGMaxY;
        private double RGMaxZ;
        private double RGMinX;
        private double RGMinY;
        private double RGMinZ;
        private double RGRangeX;
        private double RGRangeY;
        private double RGRangeZ;
        private int RGRows;
        private double RGStepX;
        private double RGStepY;
        private SurfaceFunction surfaceFunction;
        private pointListType surfacepointlist;
        private polygonListType surfacepolygonlist;
        private int topstk;
        private int[] Vdofs;
        private int[] Vdpntr;

        public Polysurface()
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.OpenPolysurface(0, 0, 0);
        }

        public Polysurface(Polysurface ps)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.Copy(ps);
        }

        public Polysurface(Point3D[] points, int numpoints)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.OpenPolysurface(numpoints, 2 * (numpoints + 1), 3);
            this.SetPolysurfacePoints(points, 0, numpoints);
            this.DelaunaySurface(true);
        }

        public Polysurface(Polysurface ps, int reduction)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.ReducePolysurface(ps, reduction);
        }

        public Polysurface(int numpoints, int numpolygons, int pntsperpoly)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.OpenPolysurface(numpoints, numpolygons, pntsperpoly);
        }

        public Polysurface(Point3D[] grid, int rows, int columns, int gridtype)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.ConvertRG2PS(grid, rows, columns, gridtype);
        }

        public Polysurface(double[] x, double[] y, double[] z, int numpoints)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            Point3D[] points = new Point3D[numpoints];
            for (int i = 0; i < numpoints; i++)
            {
                points[i] = new Point3D(x[i], y[i], z[i]);
            }
            this.OpenPolysurface(numpoints, 2 * (numpoints + 1), 3);
            this.SetPolysurfacePoints(points, 0, numpoints);
            this.DelaunaySurface(true);
        }

        public Polysurface(int rows, int columns, double x1, double y1, double x2, double y2, SurfaceFunction sf)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.CreatePolysurfaceFunction(rows, columns, x1, y1, x2, y2, sf);
        }

        public Polysurface(int rows, int columns, double x1, double y1, double x2, double y2, double[] zvalues, int gridtype)
        {
            this.surfaceFunction = null;
            this.BI = 1;
            this.Edofs = new int[4];
            this.Vdofs = new int[4];
            this.CreatePolysurfaceEvenGrid(rows, columns, x1, y1, x2, y2, zvalues, gridtype);
        }

        private void AddAdjacentPolyList(int tri, int[] adjlist)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.adjpolygonlist[(3 * tri) + i] = adjlist[i];
                }
            }
        }

        public void AddPolysurfacePolygon(int polyindex, int[] pointindices, int numedges, Color outsidecolor, Color bordercolor)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                if ((this.surfacepolygonlist.polygonedgelist == null) || (this.surfacepolygonlist.polygonentrys == null))
                {
                    this.ErrorCheck(620);
                }
                else
                {
                    this.surfacepolygonlist.polygonentrys[polyindex].surfacecolors = outsidecolor;
                    this.surfacepolygonlist.polygonentrys[polyindex].bordercolor = bordercolor;
                    this.surfacepolygonlist.polygonentrys[polyindex].numedges = numedges;
                    this.surfacepolygonlist.polygonentrys[polyindex].edgestart = this.surfacepolygonlist.tableindex;
                    for (int i = 0; i < numedges; i++)
                    {
                        this.surfacepolygonlist.polygonedgelist[this.surfacepolygonlist.polygonentrys[polyindex].edgestart + i] = pointindices[i];
                    }
                    this.surfacepolygonlist.tableindex += numedges;
                    this.surfacepolygonlist.usedpolygons++;
                }
            }
        }

        private void AllocAdjacentPolygonList()
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                if (this.adjpolygonlist != null)
                {
                    this.FreeAdjacentPolygonList();
                }
                this.adjpolygonlist = new int[this.surfacepolygonlist.numpolygons * 3];
                if (this.adjpolygonlist == null)
                {
                    this.ErrorCheck(600);
                }
            }
        }

        private void Bsort(int n, Point3D bmin, Point3D bmax, Point3D bmx)
        {
            Point3D point = new Point3D();
            bintclass[] array = new bintclass[n + 4];
            int index = 0;
            while (index < (n + 4))
            {
                array[index] = new bintclass(0, 0);
                index++;
            }
            if (array == null)
            {
                this.ErrorCheck(600);
            }
            else
            {
                int num5 = this.Rtround(Math.Pow((double) n, 0.25));
                double num6 = ((double) num5) / ((bmx.GetX() * 1.01) / bmx.GetX());
                double num7 = ((double) num5) / ((bmx.GetY() * 1.01) / bmx.GetY());
                int num3 = 1;
                while (num3 <= n)
                {
                    int num4 = this.delaunaylist[num3];
                    this.GetOnePolysurfacePoint(num4 - this.BI, point);
                    index = (int) (point.GetY() * num7);
                    int num2 = (int) (point.GetX() * num6);
                    if ((index % 2) == 0)
                    {
                        array[num3].bint1 = ((index * num5) + num2) + 1;
                    }
                    else
                    {
                        array[num3].bint1 = ((index + 1) * num5) - num2;
                    }
                    array[num3].bint2 = num4;
                    num3++;
                }
                array[0].bint1 = 0;
                array[0].bint2 = 0;
                Array.Sort(array, 1, n, null);
                for (num3 = 1; num3 <= n; num3++)
                {
                    this.delaunaylist[num3] = array[num3].bint2;
                }
                if (array != null)
                {
                    array = null;
                }
            }
        }

        public override object Clone()
        {
            Polysurface polysurface = new Polysurface();
            polysurface.Copy(this);
            return polysurface;
        }

        private void CloseDelaunay()
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                if (this.delaunaylist != null)
                {
                    this.delaunaylist = null;
                }
                if (this.delaunaystack != null)
                {
                    this.delaunaystack = null;
                }
                if (this.Edpntr != null)
                {
                    this.Edpntr = null;
                }
                if (this.Vdpntr != null)
                {
                    this.Vdpntr = null;
                }
                this.delaunayinitf = false;
            }
        }

        public void ClosePolysurface()
        {
            if (this.delaunayinitf)
            {
                this.CloseDelaunay();
            }
            if (this.surfacepointlist.polynumpoints != 0)
            {
                this.surfacepointlist.polynumpoints = 0;
                this.surfacepolygonlist.numpolygons = 0;
                this.surfacepolygonlist.usedpolygons = 0;
                this.surfacepolygonlist.tablelength = 0;
                this.surfacepolygonlist.tableindex = 0;
                if (this.surfacepointlist.polypointlist != null)
                {
                    this.surfacepointlist.polypointlist = null;
                }
                if (this.surfacepolygonlist.polygonentrys != null)
                {
                    this.surfacepolygonlist.polygonentrys = null;
                }
                if (this.surfacepolygonlist.polygonedgelist != null)
                {
                    this.surfacepolygonlist.polygonedgelist = null;
                }
                if (this.adjpolygonlist != null)
                {
                    this.adjpolygonlist = null;
                }
                if (this.delaunaylist != null)
                {
                    this.delaunaylist = null;
                }
                if (this.delaunaystack != null)
                {
                    this.delaunaystack = null;
                }
                if (this.Edpntr != null)
                {
                    this.Edpntr = null;
                }
                if (this.Vdpntr != null)
                {
                    this.Vdpntr = null;
                }
            }
        }

        private void ConvertRG2PS(Point3D[] grid, int rows, int columns, int gridtype)
        {
            int polyindex = 0;
            int numpnts = 0;
            int[] pointindices = new int[5];
            int[] numArray2 = new int[4];
            numpnts = rows * columns;
            this.RGRows = rows;
            this.RGColumns = columns;
            this.nGridType = gridtype;
            switch (gridtype)
            {
                case 0:
                    this.OpenPolysurface(numpnts, (rows - 1) * (columns - 1), 4);
                    if (this.surfacepointlist.polynumpoints != 0)
                    {
                        this.SetPolysurfacePoints(grid, 0, numpnts);
                        break;
                    }
                    this.ErrorCheck(620);
                    return;

                case 1:
                    this.OpenPolysurface(numpnts, (2 * (rows - 1)) * (columns - 1), 3);
                    if (this.surfacepointlist.polynumpoints != 0)
                    {
                        this.SetPolysurfacePoints(grid, 0, numpnts);
                        break;
                    }
                    this.ErrorCheck(620);
                    return;

                case 2:
                    gridtype = 0;
                    break;

                case 3:
                    gridtype = 1;
                    break;
            }
            this.RGMinX = this.surfacepointlist.polypointlist[0].GetX();
            this.RGMaxX = this.surfacepointlist.polypointlist[numpnts - 1].GetX();
            this.RGMinY = this.surfacepointlist.polypointlist[0].GetY();
            this.RGMaxY = this.surfacepointlist.polypointlist[numpnts - 1].GetY();
            this.RGRangeX = this.RGMaxX - this.RGMinX;
            this.RGRangeY = this.RGMaxY - this.RGMinY;
            this.RGStepX = this.RGRangeX / ((double) (this.RGColumns - 1));
            this.RGStepY = this.RGRangeY / ((double) (this.RGRows - 1));
            for (int i = 0; i < (rows - 1); i++)
            {
                int num4 = columns * i;
                int num5 = num4 + columns;
                for (int j = 0; j < (columns - 1); j++)
                {
                    pointindices[0] = num4 + j;
                    pointindices[1] = (num4 + j) + 1;
                    pointindices[2] = (num5 + j) + 1;
                    pointindices[3] = num5 + j;
                    switch (gridtype)
                    {
                        case 0:
                            this.AddPolysurfacePolygon(polyindex, pointindices, 4, Color.Blue, Color.Black);
                            polyindex++;
                            break;

                        case 1:
                            numArray2[0] = pointindices[0];
                            numArray2[1] = pointindices[1];
                            numArray2[2] = pointindices[3];
                            this.AddPolysurfacePolygon(polyindex, numArray2, 3, Color.Blue, Color.Black);
                            polyindex++;
                            numArray2[0] = pointindices[1];
                            numArray2[1] = pointindices[2];
                            numArray2[2] = pointindices[3];
                            this.AddPolysurfacePolygon(polyindex, numArray2, 3, Color.Blue, Color.Black);
                            polyindex++;
                            break;
                    }
                }
            }
            this.surfacepolygonlist.usedpolygons = polyindex;
            this.bEvenGrid = true;
        }

        public void Copy(Polysurface ps)
        {
            int[] indices = new int[10];
            int[] adjlist = new int[3];
            Color black = Color.Black;
            Color bordercolor = Color.Black;
            Point3D point = new Point3D();
            int pSNumPoints = ps.GetPSNumPoints();
            int pSMaxPolygons = ps.GetPSMaxPolygons();
            this.OpenPolysurface(pSNumPoints, pSMaxPolygons, ps.GetPSPointsPerPolygon());
            if (this.surfacepointlist.polynumpoints != 0)
            {
                int num;
                this.RGRows = ps.GetRGRows();
                this.RGColumns = ps.GetRGColumns();
                this.RGMinX = ps.GetRGMinX();
                this.RGMaxX = ps.GetRGMaxX();
                this.RGMinY = ps.GetRGMinY();
                this.RGMinZ = ps.GetRGMinZ();
                this.RGMaxZ = ps.GetRGMaxZ();
                this.RGMaxY = ps.GetRGMaxY();
                this.RGStepX = ps.GetRGStepX();
                this.RGStepY = ps.GetRGStepY();
                this.RGRangeX = ps.GetRGRangeX();
                this.RGRangeY = ps.GetRGRangeY();
                this.RGRangeZ = ps.GetRGRangeZ();
                this.bEvenGrid = ps.GetEvenGridFlag();
                this.bWireFrame = ps.bWireFrame;
                for (num = 0; num < pSNumPoints; num++)
                {
                    ps.GetOnePolysurfacePoint(num, point);
                    this.surfacepointlist.polypointlist[num].SetLocation(point);
                }
                pSMaxPolygons = ps.GetPSNumPolygons();
                for (num = 0; num < pSMaxPolygons; num++)
                {
                    int numedges = ps.GetPSPolygonIndices(num, indices, black, bordercolor);
                    this.AddPolysurfacePolygon(num, indices, numedges, black, bordercolor);
                }
                if (ps.GetAdjacentPolygonPntr() != null)
                {
                    this.AllocAdjacentPolygonList();
                    for (num = 0; num < pSMaxPolygons; num++)
                    {
                        ps.GetAdjacentPolyList(num, adjlist);
                        this.AddAdjacentPolyList(num, adjlist);
                    }
                }
            }
        }

        private void CreatePolysurfaceEvenGrid(int rows, int columns, double x1, double y1, double x2, double y2, double[] rdata, int gridtype)
        {
            Point3D point = new Point3D();
            int index = 0;
            double num8 = 0.0;
            double num9 = 0.0;
            if ((rows <= 1) || (columns <= 1))
            {
                this.ErrorCheck(630);
            }
            else
            {
                int numpnts = rows * columns;
                this.RGRows = rows;
                this.RGColumns = columns;
                this.OpenPolysurface(numpnts, 2 * (numpnts + 1), 3);
                if (this.surfacepointlist.polynumpoints == 0)
                {
                    this.ErrorCheck(620);
                }
                else
                {
                    double num5 = (x2 - x1) / ((double) (columns - 1));
                    double num6 = (y2 - y1) / ((double) (rows - 1));
                    double pz = rdata[0];
                    num8 = num9 = pz;
                    point.SetLocation(x1, y1, pz);
                    double num7 = x1;
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            pz = rdata[index];
                            index++;
                            point.SetLocation(x1, y1, pz);
                            this.SetOnePolysurfacePoint((i * columns) + j, point);
                            if (pz > num9)
                            {
                                num9 = pz;
                            }
                            if (pz < num8)
                            {
                                num8 = pz;
                            }
                            x1 += num5;
                        }
                        x1 = num7;
                        y1 += num6;
                    }
                    this.ConvertRG2PS(this.surfacepointlist.polypointlist, rows, columns, gridtype + 2);
                    this.CreateRGAdjacentPolygonList();
                    this.RGMinZ = num8;
                    this.RGMaxZ = num9;
                    this.RGRangeZ = Math.Abs((double) (num9 - num8));
                    this.bEvenGrid = true;
                }
            }
        }

        private void CreatePolysurfaceFunction(int rows, int columns, double x1, double y1, double x2, double y2, SurfaceFunction sf)
        {
            Point3D point = new Point3D();
            int num10 = 1;
            this.surfaceFunction = sf;
            if (this.surfaceFunction == null)
            {
                this.ErrorCheck(620);
            }
            else if ((rows <= 1) || (columns <= 1))
            {
                this.ErrorCheck(620);
            }
            else
            {
                int numpnts = rows * columns;
                this.RGRows = rows;
                this.RGColumns = columns;
                this.OpenPolysurface(numpnts, 2 * (numpnts + 1), 3);
                if (this.surfacepointlist.polynumpoints == 0)
                {
                    this.ErrorCheck(620);
                }
                else
                {
                    double num7;
                    double num4 = (x2 - x1) / ((double) (columns - 1));
                    double num5 = (y2 - y1) / ((double) (rows - 1));
                    double pz = this.surfaceFunction.CalcZValue(x1, y1);
                    double num6 = num7 = pz;
                    point.SetLocation(x1, y1, pz);
                    double num9 = x1;
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            pz = this.surfaceFunction.CalcZValue(x1, y1);
                            point.SetLocation(x1, y1, pz);
                            this.SetOnePolysurfacePoint((i * columns) + j, point);
                            if (pz > num7)
                            {
                                num7 = pz;
                            }
                            if (pz < num6)
                            {
                                num6 = pz;
                            }
                            x1 += num4;
                        }
                        x1 = num9;
                        y1 += num5;
                    }
                    this.RGMinZ = num6;
                    this.RGMaxZ = num7;
                    this.bEvenGrid = false;
                    this.RGRangeZ = Math.Abs((double) (num7 - num6));
                    this.ConvertRG2PS(this.surfacepointlist.polypointlist, rows, columns, num10 + 2);
                    this.CreateRGAdjacentPolygonList();
                    this.RGMinZ = num6;
                    this.RGMaxZ = num7;
                    this.RGRangeZ = Math.Abs((double) (num7 - num6));
                }
            }
        }

        private void CreateRGAdjacentPolygonList()
        {
            int[] adjlist = new int[3];
            if (this.bEvenGrid)
            {
                int num4 = this.RGRows - 1;
                int num5 = this.RGColumns - 1;
                this.AllocAdjacentPolygonList();
                int num3 = num5 * 2;
                int tri = 0;
                for (int i = 0; i < num4; i++)
                {
                    for (int j = 0; j < num5; j++)
                    {
                        if (i == 0)
                        {
                            adjlist[0] = -1;
                        }
                        else
                        {
                            adjlist[0] = (tri - num3) + 1;
                        }
                        adjlist[1] = tri + 1;
                        if (j == 0)
                        {
                            adjlist[2] = -1;
                        }
                        else
                        {
                            adjlist[2] = tri - 1;
                        }
                        this.AddAdjacentPolyList(tri, adjlist);
                        tri++;
                        if (j == (num5 - 1))
                        {
                            adjlist[0] = -1;
                        }
                        else
                        {
                            adjlist[0] = tri + 1;
                        }
                        if (i < (num4 - 1))
                        {
                            adjlist[1] = (tri + num3) - 1;
                        }
                        else
                        {
                            adjlist[1] = -1;
                        }
                        adjlist[2] = tri - 1;
                        this.AddAdjacentPolyList(tri, adjlist);
                        tri++;
                    }
                }
            }
        }

        public void DelaunaySurface(bool keepadjtrilist)
        {
            int[] pointindices = new int[3];
            if (this.delaunayinitf)
            {
                this.CloseDelaunay();
            }
            this.InitDelaunay();
            int pSNumPoints = this.GetPSNumPoints();
            this.Edofs[0] = 0;
            this.Edofs[1] = 0;
            this.Edofs[2] = this.numc + 1;
            this.Edofs[3] = (this.numc + 1) * 2;
            this.Vdofs[0] = 0;
            this.Vdofs[1] = 0;
            this.Vdofs[2] = this.numc + 1;
            this.Vdofs[3] = (this.numc + 1) * 2;
            this.numtriangles = this.Deltri(pSNumPoints, this.numtriangles);
            this.adjtriflag = keepadjtrilist;
            if (keepadjtrilist)
            {
                this.AllocAdjacentPolygonList();
            }
            this.ResetPolysurfacePolygons();
            for (int i = 0; i < this.numtriangles; i++)
            {
                int index = 0;
                while (index < 3)
                {
                    pointindices[index] = this.Vdpntr[(this.Vdofs[index + 1] + i) + 1] - 1;
                    index++;
                }
                this.AddPolysurfacePolygon(i, pointindices, 3, Color.Blue, Color.White);
                for (index = 0; index < 3; index++)
                {
                    pointindices[index] = this.Edpntr[(this.Edofs[index + 1] + i) + 1] - 1;
                }
                if (keepadjtrilist)
                {
                    this.AddAdjacentPolyList(i, pointindices);
                }
            }
        }

        private int Delaunwork(int numpts, int numtri)
        {
            int num5;
            int num12;
            double px = 0.0;
            double num2 = 100.0;
            Point3D point = new Point3D();
            Point3D pointd2 = new Point3D();
            Point3D pointd3 = new Point3D();
            Point3D pointd4 = new Point3D();
            int num7 = numpts + 1;
            int num8 = numpts + 2;
            int num9 = numpts + 3;
            this.Vdpntr[this.Vdofs[1] + 1] = num7;
            this.Vdpntr[this.Vdofs[2] + 1] = num8;
            this.Vdpntr[this.Vdofs[3] + 1] = num9;
            this.Edpntr[this.Edofs[1] + 1] = 0;
            this.Edpntr[this.Edofs[2] + 1] = 0;
            this.Edpntr[this.Edofs[3] + 1] = 0;
            pointd4.SetLocation(-num2, -num2, pointd4.GetZ());
            this.SetOnePolysurfacePoint(num7 - this.BI, pointd4);
            pointd4.SetLocation(num2, -num2, pointd4.GetZ());
            this.SetOnePolysurfacePoint(num8 - this.BI, pointd4);
            pointd4.SetLocation(px, num2, pointd4.GetZ());
            this.SetOnePolysurfacePoint(num9 - this.BI, pointd4);
            int num20 = 1;
            this.topstk = 0;
            this.maxStack = numpts;
            int index = 1;
            while (index <= numpts)
            {
                int num6 = this.delaunaylist[index];
                this.GetOnePolysurfacePoint(num6 - this.BI, point);
                num5 = this.Triloc(point.GetX(), point.GetY(), num20);
                num12 = this.Edpntr[this.Edofs[1] + num5];
                int nt = this.Edpntr[this.Edofs[2] + num5];
                int num14 = this.Edpntr[this.Edofs[3] + num5];
                num7 = this.Vdpntr[this.Vdofs[1] + num5];
                num8 = this.Vdpntr[this.Vdofs[2] + num5];
                num9 = this.Vdpntr[this.Vdofs[3] + num5];
                this.Vdpntr[this.Vdofs[1] + num5] = num6;
                this.Vdpntr[this.Vdofs[2] + num5] = num7;
                this.Vdpntr[this.Vdofs[3] + num5] = num8;
                this.Edpntr[this.Edofs[1] + num5] = num20 + 2;
                this.Edpntr[this.Edofs[2] + num5] = num12;
                this.Edpntr[this.Edofs[3] + num5] = num20 + 1;
                num20++;
                this.Vdpntr[this.Vdofs[1] + num20] = num6;
                this.Vdpntr[this.Vdofs[2] + num20] = num8;
                this.Vdpntr[this.Vdofs[3] + num20] = num9;
                this.Edpntr[this.Edofs[1] + num20] = num5;
                this.Edpntr[this.Edofs[2] + num20] = nt;
                this.Edpntr[this.Edofs[3] + num20] = num20 + 1;
                num20++;
                this.Vdpntr[this.Vdofs[1] + num20] = num6;
                this.Vdpntr[this.Vdofs[2] + num20] = num9;
                this.Vdpntr[this.Vdofs[3] + num20] = num7;
                this.Edpntr[this.Edofs[1] + num20] = num20 - 1;
                this.Edpntr[this.Edofs[2] + num20] = num14;
                this.Edpntr[this.Edofs[3] + num20] = num5;
                if (num12 != 0)
                {
                    this.Push(num5);
                }
                if (nt != 0)
                {
                    this.Edpntr[this.Edofs[this.Edg(nt, num5)] + nt] = num20 - 1;
                    this.Push(num20 - 1);
                }
                if (num14 != 0)
                {
                    this.Edpntr[this.Edofs[this.Edg(num14, num5)] + num14] = num20;
                    this.Push(num20);
                }
                if (this.topstk > 0)
                {
                    do
                    {
                        int k = this.Pop();
                        int num11 = this.Edpntr[this.Edofs[2] + k];
                        int num15 = this.Edg(num11, k);
                        int num16 = (num15 % 3) + 1;
                        int num17 = (num16 % 3) + 1;
                        num7 = this.Vdpntr[this.Vdofs[num15] + num11];
                        num8 = this.Vdpntr[this.Vdofs[num16] + num11];
                        num9 = this.Vdpntr[this.Vdofs[num17] + num11];
                        this.GetOnePolysurfacePoint(num7 - this.BI, pointd2);
                        this.GetOnePolysurfacePoint(num8 - this.BI, pointd3);
                        this.GetOnePolysurfacePoint(num9 - this.BI, pointd4);
                        if (this.Swap(pointd2.GetX(), pointd2.GetY(), pointd3.GetX(), pointd3.GetY(), pointd4.GetX(), pointd4.GetY(), point.GetX(), point.GetY()))
                        {
                            num12 = this.Edpntr[this.Edofs[num16] + num11];
                            nt = this.Edpntr[this.Edofs[num17] + num11];
                            num14 = this.Edpntr[this.Edofs[3] + k];
                            this.Vdpntr[this.Vdofs[3] + k] = num9;
                            this.Edpntr[this.Edofs[2] + k] = num12;
                            this.Edpntr[this.Edofs[3] + k] = num11;
                            this.Vdpntr[this.Vdofs[1] + num11] = num6;
                            this.Vdpntr[this.Vdofs[2] + num11] = num9;
                            this.Vdpntr[this.Vdofs[3] + num11] = num7;
                            this.Edpntr[this.Edofs[1] + num11] = k;
                            this.Edpntr[this.Edofs[2] + num11] = nt;
                            this.Edpntr[this.Edofs[3] + num11] = num14;
                            if (num12 != 0)
                            {
                                this.Edpntr[this.Edofs[this.Edg(num12, num11)] + num12] = k;
                                this.Push(k);
                            }
                            if (nt != 0)
                            {
                                this.Push(num11);
                            }
                            if (num14 != 0)
                            {
                                this.Edpntr[this.Edofs[this.Edg(num14, k)] + num14] = num11;
                            }
                        }
                    }
                    while (this.topstk > 0);
                }
                index++;
            }
            if (num20 != ((2 * numpts) + 1))
            {
                this.ErrorCheck(640);
                return 0;
            }
            num5 = 1;
            while (num5 <= num20)
            {
                if (((this.Vdpntr[this.Vdofs[1] + num5] > numpts) || (this.Vdpntr[this.Vdofs[2] + num5] > numpts)) || (this.Vdpntr[this.Vdofs[3] + num5] > numpts))
                {
                    for (int i = this.BI; i < (this.BI + 3); i++)
                    {
                        num12 = this.Edpntr[this.Edofs[i] + num5];
                        if (num12 != 0)
                        {
                            this.Edpntr[this.Edofs[this.Edg(num12, num5)] + num12] = 0;
                        }
                    }
                    break;
                }
                num5++;
            }
            int num18 = num5 + 1;
            int num19 = num20;
            num20 = num5 - 1;
            for (num5 = num18; num5 <= num19; num5++)
            {
                if (((this.Vdpntr[this.Vdofs[1] + num5] > numpts) || (this.Vdpntr[this.Vdofs[2] + num5] > numpts)) || (this.Vdpntr[this.Vdofs[3] + num5] > numpts))
                {
                    index = this.BI;
                    while (index < (this.BI + 3))
                    {
                        num12 = this.Edpntr[this.Edofs[index] + num5];
                        if (num12 != 0)
                        {
                            this.Edpntr[this.Edofs[this.Edg(num12, num5)] + num12] = 0;
                        }
                        index++;
                    }
                }
                else
                {
                    num20++;
                    for (index = this.BI; index < (this.BI + 3); index++)
                    {
                        num12 = this.Edpntr[this.Edofs[index] + num5];
                        this.Edpntr[this.Edofs[index] + num20] = num12;
                        this.Vdpntr[this.Vdofs[index] + num20] = this.Vdpntr[this.Vdofs[index] + num5];
                        if (num12 != 0)
                        {
                            this.Edpntr[this.Edofs[this.Edg(num12, num5)] + num12] = num20;
                        }
                    }
                }
            }
            numtri = num20;
            return numtri;
        }

        private int Deltri(int numpts, int numtri)
        {
            int num;
            int num2;
            double num4;
            double num6;
            Point3D point = new Point3D();
            this.GetOnePolysurfacePoint(this.delaunaylist[1] - this.BI, point);
            double num3 = num4 = point.GetX();
            double num5 = num6 = point.GetY();
            for (num = 2; num <= numpts; num++)
            {
                num2 = this.delaunaylist[num];
                this.GetOnePolysurfacePoint(num2 - this.BI, point);
                num3 = Math.Min(num3, point.GetX());
                num4 = Math.Max(num4, point.GetX());
                num5 = Math.Min(num5, point.GetY());
                num6 = Math.Max(num6, point.GetY());
            }
            double px = Math.Max((double) (num4 - num3), (double) (num6 - num5));
            Point3D mx = new Point3D();
            Point3D b = new Point3D();
            Point3D bmax = new Point3D();
            mx.SetLocation(num4 - num3, num6 - num5, 1.0);
            b.SetLocation(num3, num5, 0.0);
            bmax.SetLocation(num4, num6, 0.0);
            mx.SetLocation(px, px, 1.0);
            for (num = this.BI; num < (numpts + 1); num++)
            {
                num2 = this.delaunaylist[num];
                this.NormalizePolysurfacePoint(num2 - this.BI, mx, b);
            }
            this.Bsort(numpts, b, bmax, mx);
            this.Edpntr = new int[(this.numc + 1) * 3];
            if (this.Edpntr == null)
            {
                this.ErrorCheck(600);
                return 0;
            }
            this.Vdpntr = new int[(this.numc + 1) * 3];
            if (this.Vdpntr == null)
            {
                this.Edpntr = null;
                this.ErrorCheck(600);
                return 0;
            }
            numtri = this.Delaunwork(numpts, numtri);
            for (num = this.BI; num < (numpts + 1); num++)
            {
                this.UnNormalizePolysurfacePoint(num - this.BI, mx, b);
            }
            return numtri;
        }

        private int Edg(int nt, int k)
        {
            int num2 = -1;
            for (int i = this.BI; i < (this.BI + 3); i++)
            {
                if (this.Edpntr[this.Edofs[i] + nt] == k)
                {
                    num2 = i;
                }
            }
            if (num2 == -1)
            {
                this.ErrorCheck(640);
                num2 = 0;
            }
            return num2;
        }

        public override int ErrorCheck(int nerror)
        {
            if (nerror == 0)
            {
                if (this.surfacepointlist == null)
                {
                    nerror = 620;
                }
                else if (this.surfacepolygonlist == null)
                {
                    nerror = 620;
                }
                else if (this.surfacepointlist.polypointlist == null)
                {
                    nerror = 620;
                }
                else if (this.surfacepolygonlist.polygonentrys == null)
                {
                    nerror = 620;
                }
            }
            return base.ErrorCheck(nerror);
        }

        private void FreeAdjacentPolygonList()
        {
            if ((this.surfacepointlist.polynumpoints != 0) && (this.adjpolygonlist != null))
            {
                this.adjpolygonlist = null;
            }
        }

        private int[] GetAdjacentPolygonPntr()
        {
            return this.adjpolygonlist;
        }

        internal void GetAdjacentPolyList(int tri, int[] adjlist)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    adjlist[i] = this.adjpolygonlist[(3 * tri) + i];
                }
            }
        }

        private bool GetEvenGridFlag()
        {
            return this.bEvenGrid;
        }

        private int GetGridType()
        {
            return this.nGridType;
        }

        public void GetOnePolysurfacePoint(int index, Point3D point)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                point.SetLocation(this.surfacepointlist.polypointlist[index]);
            }
        }

        public Point3D[] GetPolysurfacePointList()
        {
            if (this.surfacepointlist != null)
            {
                return this.surfacepointlist.polypointlist;
            }
            return null;
        }

        public int GetPolysurfacePolygon(int polyindex, Point3D[] points, Color outsidecolor, Color bordercolor)
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            if ((this.surfacepolygonlist.polygonedgelist == null) || (this.surfacepolygonlist.polygonentrys == null))
            {
                this.ErrorCheck(620);
                return 0;
            }
            outsidecolor = this.surfacepolygonlist.polygonentrys[polyindex].surfacecolors;
            bordercolor = this.surfacepolygonlist.polygonentrys[polyindex].bordercolor;
            int numedges = this.surfacepolygonlist.polygonentrys[polyindex].numedges;
            int index = 0;
            while (index < numedges)
            {
                int num3 = this.surfacepolygonlist.polygonedgelist[this.surfacepolygonlist.polygonentrys[polyindex].edgestart + index];
                points[index].SetLocation(this.surfacepointlist.polypointlist[num3]);
                index++;
            }
            points[index].SetLocation(points[0]);
            return numedges;
        }

        public int GetPSMaxPolygons()
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            return this.surfacepolygonlist.numpolygons;
        }

        public int GetPSNumPoints()
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            return (this.surfacepointlist.polynumpoints - 4);
        }

        public int GetPSNumPolygons()
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            return this.surfacepolygonlist.usedpolygons;
        }

        public double GetPSPMean(int polyindex, int coord)
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0.0;
            }
            if ((this.surfacepolygonlist.polygonedgelist == null) || (this.surfacepolygonlist.polygonentrys == null))
            {
                return 0.0;
            }
            int num3 = this.surfacepolygonlist.polygonentrys[polyindex].numedges & 0xff;
            double num4 = 0.0;
            for (int i = 0; i < num3; i++)
            {
                int index = this.surfacepolygonlist.polygonedgelist[this.surfacepolygonlist.polygonentrys[polyindex].edgestart + i];
                switch (coord)
                {
                    case 0:
                        num4 += this.surfacepointlist.polypointlist[index].GetX();
                        break;

                    case 1:
                        num4 += this.surfacepointlist.polypointlist[index].GetY();
                        break;

                    case 2:
                        num4 += this.surfacepointlist.polypointlist[index].GetZ();
                        break;
                }
            }
            if (num3 == 0)
            {
                return 0.0;
            }
            return (num4 / ((double) num3));
        }

        public int GetPSPointsPerPolygon()
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            return this.surfacepolygonlist.pointsperpolygon;
        }

        public int GetPSPolygonIndices(int polyindex, int[] indices, Color outsidecolor, Color bordercolor)
        {
            int numedges = 0;
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            if ((this.surfacepolygonlist.polygonedgelist == null) || (this.surfacepolygonlist.polygonentrys == null))
            {
                this.ErrorCheck(620);
                return 0;
            }
            outsidecolor = this.surfacepolygonlist.polygonentrys[polyindex].surfacecolors;
            bordercolor = this.surfacepolygonlist.polygonentrys[polyindex].bordercolor;
            numedges = this.surfacepolygonlist.polygonentrys[polyindex].numedges;
            for (int i = 0; i < numedges; i++)
            {
                indices[i] = this.surfacepolygonlist.polygonedgelist[this.surfacepolygonlist.polygonentrys[polyindex].edgestart + i];
            }
            return numedges;
        }

        private int GetRGColumns()
        {
            return this.RGColumns;
        }

        private double GetRGMaxX()
        {
            return this.RGMaxX;
        }

        private double GetRGMaxY()
        {
            return this.RGMaxY;
        }

        private double GetRGMaxZ()
        {
            return this.RGMaxZ;
        }

        private double GetRGMinX()
        {
            return this.RGMinX;
        }

        private double GetRGMinY()
        {
            return this.RGMinY;
        }

        private double GetRGMinZ()
        {
            return this.RGMinZ;
        }

        private double GetRGRangeX()
        {
            return this.RGRangeX;
        }

        private double GetRGRangeY()
        {
            return this.RGRangeY;
        }

        private double GetRGRangeZ()
        {
            return this.RGRangeZ;
        }

        private int GetRGRows()
        {
            return this.RGRows;
        }

        private double GetRGStepX()
        {
            return this.RGStepX;
        }

        private double GetRGStepY()
        {
            return this.RGStepY;
        }

        public int GPSPolygon(int polyindex, Point3D[] points)
        {
            if (this.surfacepointlist.polynumpoints == 0)
            {
                return 0;
            }
            if ((this.surfacepolygonlist.polygonedgelist == null) || (this.surfacepolygonlist.polygonentrys == null))
            {
                this.ErrorCheck(620);
                return 0;
            }
            int numedges = this.surfacepolygonlist.polygonentrys[polyindex].numedges;
            int index = 0;
            while (index < numedges)
            {
                int num3 = this.surfacepolygonlist.polygonedgelist[this.surfacepolygonlist.polygonentrys[polyindex].edgestart + index];
                points[index].SetLocation(this.surfacepointlist.polypointlist[num3]);
                index++;
            }
            points[index].SetLocation(points[0]);
            return numedges;
        }

        private int InitDelaunay()
        {
            this.Edpntr = null;
            this.Vdpntr = null;
            this.delaunaylist = null;
            this.delaunaystack = null;
            int pSNumPoints = this.GetPSNumPoints();
            if (pSNumPoints != 0)
            {
                this.delaunaylist = new int[pSNumPoints + 2];
                if (this.delaunaylist == null)
                {
                    this.ErrorCheck(600);
                    return 0;
                }
                this.delaunaystack = new int[pSNumPoints + 2];
                if (this.delaunaystack == null)
                {
                    this.delaunaylist = null;
                    this.ErrorCheck(600);
                    return 0;
                }
                for (int i = 0; i < pSNumPoints; i++)
                {
                    this.delaunaylist[i + 1] = 1 + i;
                }
                this.numc = (2 * pSNumPoints) + 1;
                this.delaunayinitf = true;
            }
            return 0;
        }

        private void NormalizePolysurfacePoint(int polyindex, Point3D mx, Point3D b)
        {
            Point3D point = new Point3D();
            this.GetOnePolysurfacePoint(polyindex, point);
            double x = point.GetX();
            double y = point.GetY();
            double z = point.GetZ();
            x = (x - b.GetX()) / mx.GetX();
            y = (y - b.GetY()) / mx.GetY();
            z = (z - b.GetZ()) / mx.GetZ();
            point.SetLocation(x, y, z);
            this.SetOnePolysurfacePoint(polyindex, point);
        }

        public void OpenPolysurface(int numpnts, int numpolygons, int pntsperpoly)
        {
            this.surfacepointlist = new pointListType();
            this.surfacepolygonlist = new polygonListType();
            if (numpnts != 0)
            {
                numpnts += 4;
                this.surfacepointlist.polypointlist = new Point3D[numpnts];
                int index = 0;
                while (index < numpnts)
                {
                    this.surfacepointlist.polypointlist[index] = new Point3D();
                    index++;
                }
                if (this.surfacepointlist.polypointlist == null)
                {
                    this.ErrorCheck(600);
                }
                else
                {
                    this.surfacepointlist.polynumpoints = numpnts;
                    this.surfacepolygonlist.polygonentrys = new polygonEntryType[numpolygons];
                    for (index = 0; index < numpolygons; index++)
                    {
                        this.surfacepolygonlist.polygonentrys[index] = new polygonEntryType();
                    }
                    if (this.surfacepolygonlist.polygonentrys == null)
                    {
                        this.surfacepointlist.polypointlist = null;
                        this.ErrorCheck(600);
                    }
                    else
                    {
                        this.surfacepolygonlist.numpolygons = numpolygons;
                        this.surfacepolygonlist.pointsperpolygon = pntsperpoly;
                        this.surfacepolygonlist.usedpolygons = 0;
                        this.surfacepolygonlist.tablelength = (int) ((1.1 * numpolygons) * pntsperpoly);
                        this.surfacepolygonlist.tableindex = 0;
                        this.surfacepolygonlist.polygonedgelist = new int[this.surfacepolygonlist.tablelength];
                        if (this.surfacepolygonlist.polygonedgelist == null)
                        {
                            this.surfacepointlist.polypointlist = null;
                            this.surfacepolygonlist.polygonentrys = null;
                            this.ErrorCheck(600);
                        }
                        else
                        {
                            this.RGRows = this.RGColumns = 0;
                            this.RGMinX = this.RGMaxX = this.RGMinY = this.RGMaxY = 0.0;
                            this.RGStepX = this.RGStepY = this.RGRangeX = this.RGRangeY = 0.0;
                            this.RGMinZ = this.RGMaxZ = this.RGRangeZ = 0.0;
                            this.adjpolygonlist = null;
                            this.delaunaylist = null;
                            this.delaunaystack = null;
                            this.delaunayinitf = false;
                            this.bEvenGrid = false;
                            this.nGridType = 1;
                            this.bWireFrame = false;
                            this.Edpntr = null;
                            this.Vdpntr = null;
                        }
                    }
                }
            }
        }

        private int Pop()
        {
            int num = 0;
            if (this.topstk > 0)
            {
                num = this.delaunaystack[this.topstk];
                this.topstk--;
                return num;
            }
            num = this.delaunaystack[1];
            this.ErrorCheck(640);
            return num;
        }

        private void Push(int item)
        {
            if (this.topstk < this.maxStack)
            {
                this.topstk++;
                this.delaunaystack[this.topstk] = item;
            }
            else
            {
                this.ErrorCheck(640);
            }
        }

        public void ReducePolysurface(Polysurface ps, int reduction)
        {
            Point3D point = new Point3D();
            ps.GetGridType();
            int rows = ps.GetRGRows() / reduction;
            int columns = ps.GetRGColumns() / reduction;
            int num8 = rows * columns;
            Point3D[] grid = new Point3D[num8];
            int index = 0;
            while (index < num8)
            {
                grid[index] = new Point3D();
                index++;
            }
            if (grid == null)
            {
                this.ErrorCheck(600);
            }
            else
            {
                double num9 = ps.GetRGStepX() * reduction;
                double num10 = ps.GetRGStepY() * reduction;
                double rGMinX = ps.GetRGMinX();
                ps.GetRGMinX();
                double rGMinY = ps.GetRGMinY();
                ps.GetRGMinY();
                ps.GetRGMinZ();
                ps.GetRGMaxZ();
                int rGColumns = ps.GetRGColumns();
                for (index = 0; index < rows; index++)
                {
                    int num6 = index * reduction;
                    double py = rGMinY + (index * num10);
                    for (int i = 0; i < columns; i++)
                    {
                        double px = rGMinX + (i * num9);
                        int num7 = i * reduction;
                        int num5 = (num6 * rGColumns) + num7;
                        double num16 = 0.0;
                        for (int j = 0; j < reduction; j++)
                        {
                            for (int k = 0; k < reduction; k++)
                            {
                                ps.GetOnePolysurfacePoint((num5 + (k * rGColumns)) + j, point);
                                num16 += point.GetZ();
                            }
                        }
                        point.SetLocation(px, py, num16 / ((double) (reduction * reduction)));
                        grid[(index * columns) + i].SetLocation(point);
                    }
                }
                this.ConvertRG2PS(grid, rows, columns, 1);
            }
        }

        private void ResetPolysurfacePolygons()
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                this.surfacepolygonlist.usedpolygons = 0;
                this.surfacepolygonlist.tableindex = 0;
            }
        }

        private int Rtround(double x)
        {
            return (int) Math.Floor((double) (x + 0.5));
        }

        public void SetOnePolysurfacePoint(int index, Point3D point)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                this.surfacepointlist.polypointlist[index].SetLocation(point);
            }
        }

        public void SetPolysurfaceColors(int polyindex, Color outsidecolor, Color bordercolor)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                this.surfacepolygonlist.polygonentrys[polyindex].surfacecolors = outsidecolor;
                this.surfacepolygonlist.polygonentrys[polyindex].bordercolor = bordercolor;
            }
        }

        public void SetPolysurfacePoints(Point3D[] points, int startindex, int numpnts)
        {
            if (this.surfacepointlist.polynumpoints != 0)
            {
                int index = startindex;
                numpnts = Math.Min(startindex + numpnts, this.surfacepointlist.polynumpoints);
                for (int i = 0; i < numpnts; i++)
                {
                    this.surfacepointlist.polypointlist[index].SetLocation(points[i]);
                    index++;
                }
            }
        }

        private bool Swap(double x1, double y1, double x2, double y2, double x3, double y3, double xp, double yp)
        {
            double num13 = 0.0;
            double num = x1 - x3;
            double num2 = y1 - y3;
            double num3 = x2 - x3;
            double num4 = y2 - y3;
            double num5 = x1 - xp;
            double num6 = y1 - yp;
            double num7 = x2 - xp;
            double num8 = y2 - yp;
            double num9 = (num * num3) + (num2 * num4);
            double num10 = (num7 * num5) + (num6 * num8);
            if ((num9 >= num13) && (num10 >= num13))
            {
                return false;
            }
            if ((num9 < num13) && (num10 < num13))
            {
                return true;
            }
            double num11 = (num * num4) - (num3 * num2);
            double num12 = (num7 * num6) - (num5 * num8);
            return (((num11 * num10) + (num12 * num9)) < num13);
        }

        private int Triloc(double xp, double yp, int numtri)
        {
            bool flag = false;
            Point3D point = new Point3D();
            Point3D pointd2 = new Point3D();
            int num4 = numtri;
            do
            {
                for (int i = this.BI; i < (this.BI + 3); i++)
                {
                    int num = this.Vdpntr[this.Vdofs[i] + num4];
                    int num2 = this.Vdpntr[this.Vdofs[(i % 3) + this.BI] + num4];
                    this.GetOnePolysurfacePoint(num - this.BI, point);
                    this.GetOnePolysurfacePoint(num2 - this.BI, pointd2);
                    if (((point.GetY() - yp) * (pointd2.GetX() - xp)) > ((point.GetX() - xp) * (pointd2.GetY() - yp)))
                    {
                        num4 = this.Edpntr[this.Edofs[i] + num4];
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            while (!flag);
            return num4;
        }

        private void UnNormalizePolysurfacePoint(int polyindex, Point3D mx, Point3D b)
        {
            Point3D point = new Point3D();
            this.GetOnePolysurfacePoint(polyindex, point);
            double x = point.GetX();
            double y = point.GetY();
            double z = point.GetZ();
            x = (x * mx.GetX()) + b.GetX();
            y = (y * mx.GetY()) + b.GetY();
            z = (z * mx.GetZ()) + b.GetZ();
            point.SetLocation(x, y, z);
            this.SetOnePolysurfacePoint(polyindex, point);
        }

        private class bintclass : IComparable
        {
            public int bint1;
            public int bint2;

            public bintclass()
            {
            }

            public bintclass(int x, int y)
            {
                this.bint1 = x;
                this.bint2 = y;
            }

            public int CompareTo(object o)
            {
                int num = 0;
                Polysurface.bintclass bintclass = (Polysurface.bintclass) o;
                if (bintclass.bint1 < this.bint1)
                {
                    return 1;
                }
                if (bintclass.bint1 > this.bint1)
                {
                    num = -1;
                }
                return num;
            }
        }

        protected class pointListType
        {
            internal int polynumpoints;
            internal Point3D[] polypointlist;
        }

        protected class polygonEntryType
        {
            internal Color bordercolor = Color.Black;
            internal int edgestart;
            internal Color insidecolors = Color.Black;
            internal int numedges;
            internal Color surfacecolors = Color.Blue;
        }

        protected class polygonListType
        {
            internal int numpolygons;
            internal int pointsperpolygon;
            internal int[] polygonedgelist;
            internal Polysurface.polygonEntryType[] polygonentrys;
            internal int tableindex;
            internal int tablelength;
            internal int usedpolygons;
        }
    }
}

