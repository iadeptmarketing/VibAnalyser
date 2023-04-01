namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;

    public class ChartSupport : ChartObj
    {
        private static Random randomNumber = new Random();
        private static Color[] simpleColorArray = new Color[] { Color.Black, Color.Blue, Color.Cyan, Color.DarkGray, Color.Gray, Color.Green, Color.LightGray, Color.Magenta, Color.Orange, Color.Pink, Color.Red, Color.White, Color.Yellow };

        public static int AdjustDecs(double r, int decs)
        {
            int num2 = 0;
            if (r != 0.0)
            {
                num2 = (int) Math.Floor(Log10Ex(Math.Abs(r)));
                decs += num2;
                return decs;
            }
            decs = 1;
            return decs;
        }

        public static double Antilog10Ex(double r)
        {
            return Math.Pow(10.0, r);
        }

        public static bool BGoodValue(Point2D p)
        {
            bool flag = true;
            if (p.GetX() == double.MaxValue)
            {
                flag = false;
            }
            if (p.GetY() == double.MaxValue)
            {
                flag = false;
            }
            return flag;
        }

        public static bool BGoodValue(double x)
        {
            bool flag = true;
            if (x == double.MaxValue)
            {
                flag = false;
            }
            return flag;
        }

        public static bool BGoodValue(double x, double y)
        {
            bool flag = true;
            if (x == double.MaxValue)
            {
                flag = false;
            }
            if (y == double.MaxValue)
            {
                flag = false;
            }
            return flag;
        }

        public static bool BGoodValue(double x1, double y1, double z1)
        {
            bool flag = true;
            if (!BGoodValue(x1, y1))
            {
                flag = false;
            }
            if (!BGoodValue(z1))
            {
                flag = false;
            }
            return flag;
        }

        public static bool BGoodValue(double x1, double y1, double x2, double y2)
        {
            bool flag = true;
            if (!BGoodValue(x1, y1))
            {
                flag = false;
            }
            if (!BGoodValue(x2, y2))
            {
                flag = false;
            }
            return flag;
        }

        public static bool CalcNearestPoint(PhysicalCoordinates transform, GroupDataset dataset, bool coordswap, Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            Point2D source = new Point2D();
            Point2D dest = new Point2D();
            Point2D pointd3 = new Point2D();
            bool flag = true;
            int index = 0;
            int ngroup = 0;
            int numberDatapoints = 0;
            int autoScaleNumberGroups = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = -1;
            int num12 = 0;
            if (dataset == null)
            {
                return false;
            }
            DoubleArray xData = dataset.XData;
            DoubleArray2D groupDataObj = dataset.GetGroupDataObj();
            int nsrcpostype = 1;
            nsrcpostype = GetCoordinateSystemType(transform);
            numberDatapoints = dataset.GetNumberDatapoints();
            autoScaleNumberGroups = dataset.GetAutoScaleNumberGroups();
            for (ngroup = 0; ngroup < autoScaleNumberGroups; ngroup++)
            {
                index = 0;
                while (index < numberDatapoints)
                {
                    if (dataset.CheckValidGroupData(transform, ngroup, index))
                    {
                        num11 = index;
                        ngroup = autoScaleNumberGroups;
                        break;
                    }
                    index++;
                }
            }
            if (num11 < 0)
            {
                flag = false;
                if (nearestpoint != null)
                {
                    nearestpoint.nearestPointValid = flag;
                }
                return flag;
            }
            num9 = num11;
            num10 = num12;
            transform.ConvertCoord(pointd3, 0, testpoint, nsrcpostype);
            switch (nmode)
            {
                case 0:
                    source.SetLocation(xData[num11], groupDataObj[num12][num11]);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    num = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                    for (ngroup = num12; ngroup < autoScaleNumberGroups; ngroup++)
                    {
                        index = num11;
                        while (index < numberDatapoints)
                        {
                            if (dataset.CheckValidGroupData(transform, ngroup, index))
                            {
                                source.SetLocation(xData[index], groupDataObj[ngroup][index]);
                                if (coordswap)
                                {
                                    SwapCoords(source);
                                }
                                num3 = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                                if (num3 < num)
                                {
                                    num = num3;
                                    num9 = index;
                                    num10 = ngroup;
                                }
                            }
                            index++;
                        }
                    }
                    break;

                case 1:
                    source.SetLocation(xData[num11], groupDataObj[num12][num11]);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    num = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                    for (ngroup = num12; ngroup < autoScaleNumberGroups; ngroup++)
                    {
                        index = num11;
                        while (index < numberDatapoints)
                        {
                            if (dataset.CheckValidGroupData(transform, ngroup, index))
                            {
                                source.SetLocation(xData[index], groupDataObj[ngroup][index]);
                                if (coordswap)
                                {
                                    SwapCoords(source);
                                }
                                num2 = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                                if (num2 < num)
                                {
                                    num = num2;
                                    num9 = index;
                                    num10 = ngroup;
                                }
                            }
                            index++;
                        }
                    }
                    break;

                case 2:
                    source.SetLocation(xData[num11], groupDataObj[num12][num11]);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    num3 = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                    num2 = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                    num = Math.Sqrt((num3 * num3) + (num2 * num2));
                    for (ngroup = num12; ngroup < autoScaleNumberGroups; ngroup++)
                    {
                        index = num11;
                        while (index < numberDatapoints)
                        {
                            if (dataset.CheckValidGroupData(transform, ngroup, index))
                            {
                                source.SetLocation(xData[index], groupDataObj[ngroup][index]);
                                if (coordswap)
                                {
                                    SwapCoords(source);
                                }
                                num3 = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                                num2 = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                                num4 = Math.Sqrt((num3 * num3) + (num2 * num2));
                                if (num4 < num)
                                {
                                    num = num4;
                                    num9 = index;
                                    num10 = ngroup;
                                }
                            }
                            index++;
                        }
                    }
                    break;

                case 3:
                    source = dataset.GetDataPoint(num12, num11);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    transform.ConvertCoord(dest, 0, source, nsrcpostype);
                    num = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                    for (ngroup = num12; ngroup < autoScaleNumberGroups; ngroup++)
                    {
                        index = num11;
                        while (index < numberDatapoints)
                        {
                            if (dataset.CheckValidGroupData(transform, ngroup, index))
                            {
                                source = dataset.GetDataPoint(ngroup, index);
                                if (coordswap)
                                {
                                    SwapCoords(source);
                                }
                                transform.ConvertCoord(dest, 0, source, nsrcpostype);
                                num3 = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                                if (num3 < num)
                                {
                                    num = num3;
                                    num9 = index;
                                    num10 = ngroup;
                                }
                            }
                            index++;
                        }
                    }
                    break;

                case 4:
                    source = dataset.GetDataPoint(num12, num11);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    transform.ConvertCoord(dest, 0, source, nsrcpostype);
                    num = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                    for (ngroup = num12; ngroup < autoScaleNumberGroups; ngroup++)
                    {
                        index = num11;
                        while (index < numberDatapoints)
                        {
                            if (dataset.CheckValidGroupData(transform, ngroup, index))
                            {
                                source = dataset.GetDataPoint(ngroup, index);
                                if (coordswap)
                                {
                                    SwapCoords(source);
                                }
                                transform.ConvertCoord(dest, 0, source, nsrcpostype);
                                num2 = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                                if (num2 < num)
                                {
                                    num = num2;
                                    num9 = index;
                                    num10 = ngroup;
                                }
                            }
                            index++;
                        }
                    }
                    break;

                case 5:
                    source = dataset.GetDataPoint(num12, num11);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    transform.ConvertCoord(dest, 0, source, nsrcpostype);
                    num3 = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                    num2 = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                    num = Math.Sqrt((num3 * num3) + (num2 * num2));
                    for (ngroup = num12; ngroup < autoScaleNumberGroups; ngroup++)
                    {
                        for (index = num11; index < numberDatapoints; index++)
                        {
                            if (dataset.CheckValidGroupData(transform, ngroup, index))
                            {
                                source = dataset.GetDataPoint(ngroup, index);
                                if (coordswap)
                                {
                                    SwapCoords(source);
                                }
                                transform.ConvertCoord(dest, 0, source, nsrcpostype);
                                num3 = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                                num2 = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                                num4 = Math.Sqrt((num3 * num3) + (num2 * num2));
                                if (num4 < num)
                                {
                                    num = num4;
                                    num9 = index;
                                    num10 = ngroup;
                                }
                            }
                        }
                    }
                    break;
            }
            if (nearestpoint != null)
            {
                nearestpoint.nearestPointValid = flag;
                nearestpoint.nearestPoint.SetLocation(xData[num9], groupDataObj[num10][num9]);
                nearestpoint.actualPoint.SetLocation(testpoint);
                if (coordswap)
                {
                    SwapCoords(nearestpoint.nearestPoint);
                }
                nearestpoint.nearestPointMinDistance = num;
                nearestpoint.nearestPointIndex = num9;
                nearestpoint.nearestGroupIndex = num10;
            }
            return flag;
        }

        public static bool CalcNearestPoint(PhysicalCoordinates transform, SimpleDataset dataset, bool coordswap, Point2D testpoint, int nmode, NearestPointData nearestpoint)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            Point2D source = new Point2D();
            Point2D dest = new Point2D();
            Point2D pointd3 = new Point2D();
            bool flag = true;
            int index = 0;
            int numberDatapoints = 0;
            int num7 = 0;
            int num8 = -1;
            if (dataset == null)
            {
                return false;
            }
            DoubleArray xData = dataset.XData;
            DoubleArray yData = dataset.YData;
            int nsrcpostype = 1;
            nsrcpostype = GetCoordinateSystemType(transform);
            numberDatapoints = dataset.GetNumberDatapoints();
            for (index = 0; index < numberDatapoints; index++)
            {
                if (dataset.CheckValidData(transform, index))
                {
                    num8 = index;
                    break;
                }
            }
            if (num8 < 0)
            {
                flag = false;
                if (nearestpoint != null)
                {
                    nearestpoint.nearestPointValid = flag;
                }
                return flag;
            }
            num7 = num8;
            transform.ConvertCoord(pointd3, 0, testpoint, nsrcpostype);
            switch (nmode)
            {
                case 0:
                    source = dataset.GetDataPoint(num8);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    num = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                    for (index = num8 + 1; index < numberDatapoints; index++)
                    {
                        if (dataset.CheckValidData(transform, index))
                        {
                            source = dataset.GetDataPoint(index);
                            if (coordswap)
                            {
                                SwapCoords(source);
                            }
                            num3 = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                            if (num3 < num)
                            {
                                num = num3;
                                num7 = index;
                            }
                        }
                    }
                    break;

                case 1:
                    source = dataset.GetDataPoint(num8);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    num = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                    for (index = num8 + 1; index < numberDatapoints; index++)
                    {
                        if (dataset.CheckValidData(transform, index))
                        {
                            source = dataset.GetDataPoint(index);
                            if (coordswap)
                            {
                                SwapCoords(source);
                            }
                            num2 = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                            if (num2 < num)
                            {
                                num = num2;
                                num7 = index;
                            }
                        }
                    }
                    break;

                case 2:
                    source = dataset.GetDataPoint(num8);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    num3 = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                    num2 = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                    num = Math.Sqrt((num3 * num3) + (num2 * num2));
                    for (index = num8 + 1; index < numberDatapoints; index++)
                    {
                        if (dataset.CheckValidData(transform, index))
                        {
                            source = dataset.GetDataPoint(index);
                            if (coordswap)
                            {
                                SwapCoords(source);
                            }
                            num3 = Math.Abs((double) (source.GetX() - testpoint.GetX()));
                            num2 = Math.Abs((double) (source.GetY() - testpoint.GetY()));
                            num4 = Math.Sqrt((num3 * num3) + (num2 * num2));
                            if (num4 < num)
                            {
                                num = num4;
                                num7 = index;
                            }
                        }
                    }
                    break;

                case 3:
                    source = dataset.GetDataPoint(num8);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    transform.ConvertCoord(dest, 0, source, nsrcpostype);
                    num = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                    for (index = num8 + 1; index < numberDatapoints; index++)
                    {
                        if (dataset.CheckValidData(transform, index))
                        {
                            source = dataset.GetDataPoint(index);
                            if (coordswap)
                            {
                                SwapCoords(source);
                            }
                            transform.ConvertCoord(dest, 0, source, nsrcpostype);
                            num3 = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                            if (num3 < num)
                            {
                                num = num3;
                                num7 = index;
                            }
                        }
                    }
                    break;

                case 4:
                    source = dataset.GetDataPoint(num8);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    transform.ConvertCoord(dest, 0, source, nsrcpostype);
                    num = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                    for (index = num8 + 1; index < numberDatapoints; index++)
                    {
                        if (dataset.CheckValidData(transform, index))
                        {
                            source = dataset.GetDataPoint(index);
                            if (coordswap)
                            {
                                SwapCoords(source);
                            }
                            transform.ConvertCoord(dest, 0, source, nsrcpostype);
                            num2 = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                            if (num2 < num)
                            {
                                num = num2;
                                num7 = index;
                            }
                        }
                    }
                    break;

                case 5:
                    source = dataset.GetDataPoint(num8);
                    if (coordswap)
                    {
                        SwapCoords(source);
                    }
                    transform.ConvertCoord(dest, 0, source, nsrcpostype);
                    num3 = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                    num2 = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                    num = Math.Sqrt((num3 * num3) + (num2 * num2));
                    for (index = num8 + 1; index < numberDatapoints; index++)
                    {
                        if (dataset.CheckValidData(transform, index))
                        {
                            source = dataset.GetDataPoint(index);
                            if (coordswap)
                            {
                                SwapCoords(source);
                            }
                            transform.ConvertCoord(dest, 0, source, nsrcpostype);
                            num3 = Math.Abs((double) (dest.GetX() - pointd3.GetX()));
                            num2 = Math.Abs((double) (dest.GetY() - pointd3.GetY()));
                            num4 = Math.Sqrt((num3 * num3) + (num2 * num2));
                            if (num4 < num)
                            {
                                num = num4;
                                num7 = index;
                            }
                        }
                    }
                    break;
            }
            if (nearestpoint != null)
            {
                nearestpoint.nearestPointValid = flag;
                nearestpoint.nearestPoint.SetLocation(xData[num7], yData[num7]);
                nearestpoint.actualPoint.SetLocation(testpoint);
                if (coordswap)
                {
                    SwapCoords(nearestpoint.nearestPoint);
                }
                nearestpoint.nearestPointMinDistance = num;
                nearestpoint.nearestPointIndex = num7;
            }
            return flag;
        }

        public static int ClampInt(int i, int lowvalue1, int highvalue1)
        {
            if (i < lowvalue1)
            {
                return lowvalue1;
            }
            if (i > highvalue1)
            {
                return highvalue1;
            }
            return i;
        }

        public static double ClampReal(double r, double lowvalue1, double highvalue1)
        {
            if (r < lowvalue1)
            {
                return lowvalue1;
            }
            if (r > highvalue1)
            {
                return highvalue1;
            }
            return r;
        }

        public static double ClampToViewCoordinates(double r)
        {
            if (r < -32766.0)
            {
                return -32766.0;
            }
            if (r > 32766.0)
            {
                return 32766.0;
            }
            return r;
        }

        public override object Clone()
        {
            return new ChartSupport();
        }

        public static int FindRelatedAxisLabels(int index, ArrayList source)
        {
            int count = source.Count;
            Axis axis = null;
            axis = (Axis) source[index];
            if (axis != null)
            {
                AxisLabels axisLabels = axis.GetAxisLabels();
                GraphObj obj2 = null;
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) source[i];
                    if (obj2 == axisLabels)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int FindRelatedAxisTitleAxis(int index, ArrayList source)
        {
            int count = source.Count;
            AxisTitle title = null;
            title = (AxisTitle) source[index];
            if (title != null)
            {
                Axis titleAxis = title.GetTitleAxis();
                GraphObj obj2 = null;
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) source[i];
                    if (obj2 == titleAxis)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int FindRelatedGridAxes(int index, ArrayList source, int xgridaxis, int ygridaxis)
        {
            int count = source.Count;
            int num2 = -1;
            Grid grid = null;
            grid = (Grid) source[index];
            xgridaxis = -1;
            ygridaxis = -1;
            if (grid != null)
            {
                Axis gridAxis = grid.GetGridAxis(grid.GetGridAxisType());
                Axis gridXAxis = grid.GetGridXAxis();
                Axis gridYAxis = grid.GetGridYAxis();
                GraphObj obj2 = null;
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) source[i];
                    if (obj2 == gridAxis)
                    {
                        num2 = i;
                    }
                    if (obj2 == gridXAxis)
                    {
                        xgridaxis = i;
                    }
                    if (obj2 == gridYAxis)
                    {
                        ygridaxis = i;
                    }
                }
            }
            return num2;
        }

        public static int FindRelatedPolarAxesLabels(int index, ArrayList source)
        {
            int count = source.Count;
            PolarAxes axes = null;
            axes = (PolarAxes) source[index];
            if (axes != null)
            {
                PolarAxesLabels polarAxesLabels = axes.GetPolarAxesLabels();
                GraphObj obj2 = null;
                for (int i = 0; i < count; i++)
                {
                    obj2 = (GraphObj) source[i];
                    if (obj2 == polarAxesLabels)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static void FixCommonRangeError(Rectangle2D rect, double typicalmin, double typicalmax)
        {
            double num = rect.GetX1();
            double num2 = rect.GetX2();
            double num3 = rect.GetY1();
            double num4 = rect.GetY2();
            num = Math.Min(num, num2);
            num2 = Math.Max(num, num2);
            if (num == num2)
            {
                if (num == 0.0)
                {
                    num2 = typicalmax;
                }
                else if (num < 0.0)
                {
                    num2 = 0.0;
                }
                else if (num > 0.0)
                {
                    num = typicalmin;
                }
            }
            num3 = Math.Min(num3, num4);
            num4 = Math.Max(num3, num4);
            if (num3 == num4)
            {
                if (num3 == 0.0)
                {
                    num4 = typicalmax;
                }
                else if (num < 0.0)
                {
                    num4 = 0.0;
                }
                else if (num3 > 0.0)
                {
                    num3 = typicalmin;
                }
            }
            rect.SetFrameFromDiagonal(num, num3, num2, num4);
        }

        public static double[] FloatToDoubleArray(float[] floatarray)
        {
            double[] numArray = null;
            if (floatarray != null)
            {
                int length = floatarray.Length;
                numArray = new double[length];
                for (int i = 0; i < length; i++)
                {
                    numArray[i] = floatarray[i];
                }
            }
            return numArray;
        }

        public static int GetCoordinateSystemType(PhysicalCoordinates transform)
        {
            int num = 1;
            if (IsKindOf(transform, "PolarCoordinates"))
            {
                num = 2;
            }
            return num;
        }

        public static double GetDatasetsMax(ChartDataset[] datasets, int numdatasets, int naxis)
        {
            double datasetMax = datasets[0].GetDatasetMax(naxis);
            for (int i = 1; i < numdatasets; i++)
            {
                double num = datasets[i].GetDatasetMax(naxis);
                if (num > datasetMax)
                {
                    datasetMax = num;
                }
            }
            return datasetMax;
        }

        public static double GetDatasetsMin(ChartDataset[] datasets, int numdatasets, int naxis)
        {
            double datasetMin = datasets[0].GetDatasetMin(naxis);
            for (int i = 1; i < numdatasets; i++)
            {
                double num = datasets[i].GetDatasetMin(naxis);
                if (num < datasetMin)
                {
                    datasetMin = num;
                }
            }
            return datasetMin;
        }

        public static double GetDatasetsSumMax(GroupDataset[] datasets, int numdatasets, int naxis)
        {
            double groupDatasetSumMax = datasets[0].GetGroupDatasetSumMax(naxis);
            for (int i = 1; i < numdatasets; i++)
            {
                double num = datasets[i].GetGroupDatasetSumMax(naxis);
                if (num > groupDatasetSumMax)
                {
                    groupDatasetSumMax = num;
                }
            }
            return groupDatasetSumMax;
        }

        public static double GetDatasetsSumMin(GroupDataset[] datasets, int numdatasets, int naxis)
        {
            double groupDatasetSumMin = datasets[0].GetGroupDatasetSumMin(naxis);
            for (int i = 1; i < numdatasets; i++)
            {
                double num = datasets[i].GetGroupDatasetSumMin(naxis);
                if (num < groupDatasetSumMin)
                {
                    groupDatasetSumMin = num;
                }
            }
            return groupDatasetSumMin;
        }

        public static int GetFirstValidIndex(Point2D[] p, int n)
        {
            int num = n;
            for (int i = 0; i < n; i++)
            {
                if ((p[i].GetX() != double.MaxValue) && (p[i].GetY() != double.MaxValue))
                {
                    return i;
                }
            }
            return num;
        }

        public static int GetFirstValidIndex(bool[] valid, int n)
        {
            int num = n;
            for (int i = 0; i < n; i++)
            {
                if (!valid[i])
                {
                    return i;
                }
            }
            return num;
        }

        public static int GetFirstValidIndex(double[] r, int n)
        {
            int num = n;
            for (int i = 0; i < n; i++)
            {
                if (r[i] != double.MaxValue)
                {
                    return i;
                }
            }
            return num;
        }

        public static int GetFirstValidIndex(double[] x, double[] y, int n)
        {
            int num = n;
            for (int i = 0; i < n; i++)
            {
                if ((x[i] != double.MaxValue) && (y[i] != double.MaxValue))
                {
                    return i;
                }
            }
            return num;
        }

        public static double GetFirstValidValue(double[] r, int n)
        {
            double num = 0.0;
            int firstValidIndex = GetFirstValidIndex(r, n);
            if (firstValidIndex < n)
            {
                num = r[firstValidIndex];
            }
            return num;
        }

        public static double GetMaximum(double[] r)
        {
            DoubleArray array = new DoubleArray(r);
            return GetMaximum(array);
        }

        public static double GetMaximum(DoubleArray r)
        {
            double minValue = double.MinValue;
            int length = r.Length;
            if (length > 0)
            {
                if (r[0] != double.MaxValue)
                {
                    minValue = r[0];
                }
                for (int i = 0; i < length; i++)
                {
                    if ((r[i] != double.MaxValue) && (r[i] > minValue))
                    {
                        minValue = r[i];
                    }
                }
            }
            if (minValue == double.MinValue)
            {
                minValue = 1.0;
            }
            return minValue;
        }

        public static double GetMaximum(DoubleArray r, BoolArray valid)
        {
            double minValue = double.MinValue;
            int length = r.Length;
            if (length > 0)
            {
                if (valid[0] && (r[0] != double.MaxValue))
                {
                    minValue = r[0];
                }
                for (int i = 0; i < length; i++)
                {
                    if ((valid[i] && (r[i] != double.MaxValue)) && (r[i] > minValue))
                    {
                        minValue = r[i];
                    }
                }
            }
            if (minValue == double.MinValue)
            {
                minValue = 1.0;
            }
            return minValue;
        }

        public static double GetMaximum(DoubleArray2D rc, BoolArray valid, int ngroup)
        {
            double maximum = 1.0;
            double minValue = double.MinValue;
            int numColumns = rc.NumColumns;
            ngroup = Math.Min(rc.NumRows, ngroup);
            DoubleArray array = new DoubleArray(numColumns);
            if ((numColumns > 0) && (ngroup >= 1))
            {
                maximum = GetMaximum(rc.GetRowObject(0), valid);
                for (int i = 1; i < ngroup; i++)
                {
                    minValue = GetMaximum(rc.GetRowObject(i), valid);
                    if (minValue > maximum)
                    {
                        maximum = minValue;
                    }
                }
            }
            return maximum;
        }

        public static double GetMinimum(DoubleArray r)
        {
            double maxValue = double.MaxValue;
            int length = r.Length;
            if (length > 0)
            {
                if (r[0] != double.MaxValue)
                {
                    maxValue = r[0];
                }
                for (int i = 0; i < length; i++)
                {
                    if ((r[i] != double.MaxValue) && (r[i] < maxValue))
                    {
                        maxValue = r[i];
                    }
                }
            }
            if (maxValue == double.MaxValue)
            {
                maxValue = 0.0;
            }
            return maxValue;
        }

        public static double GetMinimum(double[] r)
        {
            DoubleArray array = new DoubleArray(r);
            return GetMinimum(array);
        }

        public static double GetMinimum(DoubleArray r, BoolArray valid)
        {
            double maxValue = double.MaxValue;
            int length = r.Length;
            if (length > 0)
            {
                if (valid[0] && (r[0] != double.MaxValue))
                {
                    maxValue = r[0];
                }
                for (int i = 0; i < length; i++)
                {
                    if ((valid[i] && (r[i] != double.MaxValue)) && (r[i] < maxValue))
                    {
                        maxValue = r[i];
                    }
                }
            }
            if (maxValue == double.MaxValue)
            {
                maxValue = 0.0;
            }
            return maxValue;
        }

        public static double GetMinimum(DoubleArray2D rc, BoolArray valid, int ngroup)
        {
            double minimum = 0.0;
            double maxValue = double.MaxValue;
            int numColumns = rc.NumColumns;
            ngroup = Math.Min(rc.NumRows, ngroup);
            DoubleArray array = new DoubleArray(numColumns);
            if ((numColumns > 0) && (ngroup >= 1))
            {
                minimum = GetMinimum(rc.GetRowObject(0), valid);
                for (int i = 1; i < ngroup; i++)
                {
                    maxValue = GetMinimum(rc.GetRowObject(i), valid);
                    if (maxValue < minimum)
                    {
                        minimum = maxValue;
                    }
                }
            }
            return minimum;
        }

        public static double GetRandomDouble()
        {
            return randomNumber.NextDouble();
        }

        public static void GradientBar(Graphics g2, Rectangle2D bgrect, Rectangle2D scalerect, Color color1, Color color2, double stripwidth, int direction)
        {
            double x = bgrect.X;
            double y = bgrect.Y;
            double interpolatevalue = 0.0;
            int num4 = (int) Math.Ceiling((double) (1.0 / stripwidth));
            Color white = Color.White;
            Brush cachedBrush = ChartAttribute.GetCachedBrush(color1);
            Rectangle2D rectangled = (Rectangle2D) bgrect.Clone();
            for (int i = 0; i < num4; i++)
            {
                cachedBrush = ChartAttribute.GetCachedBrush(InterpolateColor(color1, color2, interpolatevalue));
                interpolatevalue += stripwidth;
                if (direction == 1)
                {
                    rectangled.SetFrame(x, y, bgrect.Width, scalerect.Height * stripwidth);
                    if ((rectangled.Y + rectangled.Height) > (bgrect.Y + bgrect.Height))
                    {
                        rectangled.Height = bgrect.Y + bgrect.Height;
                    }
                    rectangled.Height++;
                    y += scalerect.Height * stripwidth;
                }
                else
                {
                    rectangled.SetFrame(x, y, scalerect.Width * stripwidth, bgrect.Height);
                    if ((rectangled.X + rectangled.Width) > (bgrect.X + bgrect.Width))
                    {
                        rectangled.Width = bgrect.X + bgrect.Width;
                    }
                    rectangled.Width++;
                    x += scalerect.Width * stripwidth;
                }
                g2.FillRectangle(cachedBrush, rectangled.GetRectangle());
            }
        }

        public static Color InterpolateColor(Color color1, Color color2, double interpolatevalue)
        {
            return Color.FromArgb(color1.A + ((int) ((color2.A - color1.A) * interpolatevalue)), color1.R + ((int) ((color2.R - color1.R) * interpolatevalue)), color1.G + ((int) ((color2.G - color1.G) * interpolatevalue)), color1.B + ((int) ((color2.B - color1.B) * interpolatevalue)));
        }

        public static bool IsKindOf(object obj, string typestring)
        {
            return IsKindOf(obj, typestring, true);
        }

        public static bool IsKindOf(object obj, string typestring, bool appendqc)
        {
            if (obj != null)
            {
                if (appendqc)
                {
                    if ((typestring[0] == 'R') && (typestring[1] == 'T'))
                    {
                        typestring = "com.quinncurtis.rtgraphnet." + typestring;
                    }
                    else
                    {
                        typestring = "com.quinncurtis.chart2dnet." + typestring;
                    }
                }
                for (Type type = obj.GetType(); type != null; type = type.BaseType)
                {
                    if (type.FullName == typestring)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsStackedPlotType(ChartPlot plotobj)
        {
            bool flag = false;
            if ((plotobj.GetChartObjType() != 0x16) && (plotobj.GetChartObjType() != 0x17))
            {
                return flag;
            }
            return true;
        }

        public static bool IsType(object obj, string typestring)
        {
            bool flag = false;
            if ((obj != null) && obj.GetType().FullName.Equals(typestring))
            {
                flag = true;
            }
            return flag;
        }

        public static double Log10Abs(double r)
        {
            return Log10Ex(Math.Abs(r));
        }

        public static double Log10Ex(double r)
        {
            double num = 1.0;
            if (r <= 0.0)
            {
                r = 1.0;
            }
            else if (r < 1E-30)
            {
                r = 1E-30;
            }
            else if (num > 1E+30)
            {
                r = 1E+30;
            }
            return (Math.Log(r) / 2.302585093);
        }

        public static DoubleArray MakeCompressArray(int ctype, int wSkip, int lFirst, int lLast, DoubleArray source, BoolArray validflags)
        {
            int num3;
            int num4;
            int num5;
            int length = source.Length;
            int num2 = 0;
            int num6 = 0;
            double num7 = 0.0;
            double maxValue = double.MaxValue;
            double minValue = double.MinValue;
            DoubleArray array = null;
            lFirst = ClampInt(lFirst, 0, length - 1);
            lLast = ClampInt(lLast, 0, length - 1);
            if (lFirst >= lLast)
            {
                return null;
            }
            num2 = ((lLast - lFirst) + 1) / wSkip;
            if ((((lLast - lFirst) + 1) % wSkip) != 0)
            {
                num2++;
            }
            if (ctype == 5)
            {
                wSkip *= 2;
            }
            array = new DoubleArray(Math.Max(num2, 2));
            wSkip = Math.Max(1, wSkip);
            num6 = 0;
            switch (ctype)
            {
                case 1:
                    for (num3 = lFirst; num3 <= lLast; num3 += wSkip)
                    {
                        array[num6] = source[num3];
                        num6++;
                    }
                    return array;

                case 2:
                    num3 = lFirst;
                    goto Label_0153;

                case 3:
                    for (num3 = lFirst; num3 <= lLast; num3 += wSkip)
                    {
                        maxValue = double.MaxValue;
                        for (num4 = 0; num4 < wSkip; num4++)
                        {
                            if ((num3 + num4) > lLast)
                            {
                                break;
                            }
                            if (validflags[num3 + num4] && (source[num3 + num4] < maxValue))
                            {
                                maxValue = source[num3 + num4];
                            }
                        }
                        if (maxValue == double.MaxValue)
                        {
                            array[num6] = double.MaxValue;
                        }
                        else
                        {
                            array[num6] = maxValue;
                        }
                        num6++;
                    }
                    return array;

                case 4:
                    for (num3 = lFirst; num3 <= lLast; num3 += wSkip)
                    {
                        minValue = double.MinValue;
                        for (num4 = 0; num4 < wSkip; num4++)
                        {
                            if ((num3 + num4) > lLast)
                            {
                                break;
                            }
                            if (validflags[num3 + num4] && (source[num3 + num4] > minValue))
                            {
                                minValue = source[num3 + num4];
                            }
                        }
                        if (minValue == double.MinValue)
                        {
                            array[num6] = double.MaxValue;
                        }
                        else
                        {
                            array[num6] = minValue;
                        }
                        num6++;
                    }
                    return array;

                case 5:
                    for (num3 = lFirst; num3 <= lLast; num3 += wSkip)
                    {
                        minValue = double.MinValue;
                        maxValue = double.MaxValue;
                        for (num4 = 0; num4 < wSkip; num4++)
                        {
                            if ((num3 + num4) > lLast)
                            {
                                break;
                            }
                            if (validflags[num3 + num4])
                            {
                                if (source[num3 + num4] > minValue)
                                {
                                    minValue = source[num3 + num4];
                                }
                                if (source[num3 + num4] < maxValue)
                                {
                                    maxValue = source[num3 + num4];
                                }
                            }
                        }
                        if (maxValue == double.MaxValue)
                        {
                            array[num6] = double.MaxValue;
                        }
                        else
                        {
                            array[num6] = maxValue;
                        }
                        num6++;
                        if (minValue == double.MinValue)
                        {
                            array[num6] = double.MaxValue;
                        }
                        else
                        {
                            array[num6] = minValue;
                        }
                        num6++;
                    }
                    return array;

                case 6:
                    for (num3 = lFirst; num3 <= lLast; num3 += wSkip)
                    {
                        num7 = 0.0;
                        num5 = 0;
                        for (num4 = 0; num4 < wSkip; num4++)
                        {
                            if ((num3 + num4) > lLast)
                            {
                                break;
                            }
                            if (validflags[num3 + num4])
                            {
                                num7 += source[num3 + num4];
                                num5++;
                            }
                        }
                        if (num5 == 0)
                        {
                            num7 = double.MaxValue;
                        }
                        array[num6] = num7;
                        num6++;
                    }
                    return array;

                default:
                    num3 = lFirst;
                    while (num3 <= lLast)
                    {
                        array[num6] = source[num3];
                        num6++;
                        num3 += wSkip;
                    }
                    return array;
            }
        Label_011F:
            if (num5 == 0)
            {
                num7 = double.MaxValue;
            }
            else
            {
                num7 /= (double) Math.Max(1, num5);
            }
            array[num6] = num7;
            num6++;
            num3 += wSkip;
        Label_0153:
            if (num3 <= lLast)
            {
                num7 = 0.0;
                num5 = 0;
                for (num4 = 0; num4 < wSkip; num4++)
                {
                    if ((num3 + num4) > lLast)
                    {
                        break;
                    }
                    if (validflags[num3 + num4])
                    {
                        num7 += source[num3 + num4];
                        num5++;
                    }
                }
                goto Label_011F;
            }
            return array;
        }

        public static DoubleArray2D MakeGroupCompressArray(int ctype, int wSkip, int lFirst, int lLast, DoubleArray2D source, BoolArray validflags)
        {
            int num4;
            int num5;
            int num6;
            int num7;
            DoubleArray array4;
            int numRows = source.GetNumRows();
            int numColumns = source.GetNumColumns();
            int num3 = 0;
            int num8 = 0;
            DoubleArray array = new DoubleArray(numRows);
            DoubleArray array2 = new DoubleArray(numRows);
            DoubleArray array3 = new DoubleArray(numRows);
            DoubleArray2D arrayd = null;
            lFirst = ClampInt(lFirst, 0, numColumns - 1);
            lLast = ClampInt(lLast, 0, numColumns - 1);
            if (lFirst >= lLast)
            {
                return null;
            }
            num3 = ((lLast - lFirst) + 1) / wSkip;
            if ((((lLast - lFirst) + 1) % wSkip) != 0)
            {
                num3++;
            }
            if (ctype == 5)
            {
                wSkip *= 2;
            }
            num3 = Math.Max(num3, 2);
            arrayd = new DoubleArray2D(numRows, num3);
            wSkip = Math.Max(1, wSkip);
            num8 = 0;
            int num9 = ctype;
            switch (num9)
            {
                case 1:
                    for (num4 = lFirst; num4 <= lLast; num4 += wSkip)
                    {
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            arrayd[num6][num8] = source[num6][num4];
                            num6++;
                        }
                        num8++;
                    }
                    return arrayd;

                case 2:
                    for (num4 = lFirst; num4 <= lLast; num4 += wSkip)
                    {
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            array[num6] = 0.0;
                            num7 = 0;
                            num5 = 0;
                            while (num5 < wSkip)
                            {
                                if ((num4 + num5) > lLast)
                                {
                                    break;
                                }
                                if (validflags[num4 + num5])
                                {
                                    (array4 = array)[num9 = num6] = array4[num9] + source[num6][num4 + num5];
                                    num7++;
                                }
                                num5++;
                            }
                            if (num7 == 0)
                            {
                                array[num6] = double.MaxValue;
                            }
                            else
                            {
                                array[num6] /= (double) Math.Max(1, num7);
                            }
                            arrayd[num6][num8] = array[num6];
                            num6++;
                        }
                        num8++;
                    }
                    return arrayd;

                case 3:
                    for (num4 = lFirst; num4 <= lLast; num4 += wSkip)
                    {
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            array2[num6] = double.MaxValue;
                            num5 = 0;
                            while (num5 < wSkip)
                            {
                                if ((num4 + num5) > lLast)
                                {
                                    break;
                                }
                                if (validflags[num4 + num5] && (source[num6][num4 + num5] < array2[num6]))
                                {
                                    array2[num6] = source[num6][num4 + num5];
                                }
                                num5++;
                            }
                            if (array2[num6] == double.MaxValue)
                            {
                                arrayd[num6][num8] = double.MaxValue;
                            }
                            else
                            {
                                arrayd[num6][num8] = array2[num6];
                            }
                            num6++;
                        }
                        num8++;
                    }
                    return arrayd;

                case 4:
                    for (num4 = lFirst; num4 <= lLast; num4 += wSkip)
                    {
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            array3[num6] = double.MinValue;
                            num5 = 0;
                            while (num5 < wSkip)
                            {
                                if ((num4 + num5) > lLast)
                                {
                                    break;
                                }
                                if (validflags[num4 + num5] && (source[num6][num4 + num5] > array3[num6]))
                                {
                                    array3[num6] = source[num6][num4 + num5];
                                }
                                num5++;
                            }
                            if (array3[num6] == double.MinValue)
                            {
                                arrayd[num6][num8] = double.MaxValue;
                            }
                            else
                            {
                                arrayd[num6][num8] = array3[num6];
                            }
                            num6++;
                        }
                        num8++;
                    }
                    return arrayd;

                case 5:
                    for (num4 = lFirst; num4 <= lLast; num4 += wSkip)
                    {
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            array2[num6] = double.MaxValue;
                            num5 = 0;
                            while (num5 < wSkip)
                            {
                                if ((num4 + num5) > lLast)
                                {
                                    break;
                                }
                                if (validflags[num4 + num5] && (source[num6][num4 + num5] < array2[num6]))
                                {
                                    array2[num6] = source[num6][num4 + num5];
                                }
                                num5++;
                            }
                            if (array2[num6] == double.MaxValue)
                            {
                                arrayd[num6][num8] = double.MaxValue;
                            }
                            else
                            {
                                arrayd[num6][num8] = array2[num6];
                            }
                            num6++;
                        }
                        num8++;
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            array3[num6] = double.MinValue;
                            for (num5 = 0; num5 < wSkip; num5++)
                            {
                                if ((num4 + num5) > lLast)
                                {
                                    break;
                                }
                                if (validflags[num4 + num5] && (source[num6][num4 + num5] > array3[num6]))
                                {
                                    array3[num6] = source[num6][num4 + num5];
                                }
                            }
                            if (array3[num6] == double.MinValue)
                            {
                                arrayd[num6][num8] = double.MaxValue;
                            }
                            else
                            {
                                arrayd[num6][num8] = array3[num6];
                            }
                            num6++;
                        }
                        num8++;
                    }
                    return arrayd;

                case 6:
                    num4 = lFirst;
                    while (num4 <= lLast)
                    {
                        num6 = 0;
                        while (num6 < numRows)
                        {
                            array[num6] = 0.0;
                            num7 = 0;
                            for (num5 = 0; num5 < wSkip; num5++)
                            {
                                if ((num4 + num5) > lLast)
                                {
                                    break;
                                }
                                if (validflags[num4 + num5])
                                {
                                    (array4 = array)[num9 = num6] = array4[num9] + source[num6][num4 + num5];
                                    num7++;
                                }
                            }
                            if (num7 == 0)
                            {
                                array[num6] = double.MaxValue;
                            }
                            arrayd[num6][num8] = array[num6];
                            num6++;
                        }
                        num8++;
                        num4 += wSkip;
                    }
                    return arrayd;
            }
            for (num4 = lFirst; num4 <= lLast; num4 += wSkip)
            {
                for (num6 = 0; num6 < numRows; num6++)
                {
                    arrayd[num6][num8] = source[num6][num4];
                }
                num8++;
            }
            return arrayd;
        }

        public static DoubleArray MakeTimeCompressArray(int compresstimefield, int compresstype, int lFirst, int lLast, DoubleArray sourcetime, DoubleArray sourcey, BoolArray validflags)
        {
            int length = sourcetime.Length;
            int num2 = 0;
            int num4 = 0;
            int n = 0;
            double num6 = 0.0;
            double maxValue = double.MaxValue;
            double minValue = double.MinValue;
            ChartCalendar dest = new ChartCalendar();
            ChartCalendar calendar2 = new ChartCalendar();
            ChartCalendar date = new ChartCalendar();
            ChartCalendar calendar4 = new ChartCalendar();
            bool flag = false;
            bool flag2 = false;
            lFirst = ClampInt(lFirst, 0, length - 1);
            lLast = ClampInt(lLast, 0, length - 1);
            if (lFirst >= lLast)
            {
                return null;
            }
            num2 = (lLast - lFirst) + 1;
            DoubleArray source = new DoubleArray(Math.Max(num2, 2));
            n = 0;
            ChartCalendar.SetCalendarMsecs(date, (long) sourcetime[lFirst]);
            for (int i = lFirst; i <= lLast; i++)
            {
                ChartCalendar.CalendarCopy(calendar4, date);
                ChartCalendar.SetCalendarMsecs(date, (long) sourcetime[i]);
                ChartCalendar.CalendarCopy(calendar2, date);
                ChartCalendar.CalendarCopy(dest, calendar4);
                ChartCalendar.CalendarTruncate(calendar2, compresstimefield);
                ChartCalendar.CalendarTruncate(dest, compresstimefield);
                if (date.Get(compresstimefield) != calendar4.Get(compresstimefield))
                {
                    flag = true;
                }
                else if ((date.Get(compresstimefield) == calendar4.Get(compresstimefield)) && (calendar2.GetCalendarMsecs() != dest.GetCalendarMsecs()))
                {
                    flag = true;
                }
                switch (compresstype)
                {
                    case 1:
                    {
                        if (flag || (i == lFirst))
                        {
                            source[n] = sourcey[i];
                            n++;
                            flag = false;
                        }
                        continue;
                    }
                    case 2:
                        if (!flag)
                        {
                            goto Label_01AC;
                        }
                        if (num4 != 0)
                        {
                            break;
                        }
                        num6 = double.MaxValue;
                        goto Label_018B;

                    case 3:
                        if (!flag)
                        {
                            goto Label_0269;
                        }
                        if (maxValue != double.MaxValue)
                        {
                            goto Label_024A;
                        }
                        source[n] = double.MaxValue;
                        goto Label_0255;

                    case 4:
                        if (!flag)
                        {
                            goto Label_02D8;
                        }
                        if (minValue != double.MinValue)
                        {
                            goto Label_02B9;
                        }
                        source[n] = double.MaxValue;
                        goto Label_02C4;

                    case 5:
                        if (!flag)
                        {
                            goto Label_0392;
                        }
                        if (!flag2)
                        {
                            goto Label_0388;
                        }
                        if (maxValue != double.MaxValue)
                        {
                            goto Label_032F;
                        }
                        source[n] = double.MaxValue;
                        goto Label_033A;

                    case 6:
                    {
                        if (flag)
                        {
                            if (num4 == 0)
                            {
                                num6 = double.MaxValue;
                            }
                            source[n] = num6;
                            n++;
                            num6 = 0.0;
                            num4 = 0;
                            flag = false;
                        }
                        if (validflags[i])
                        {
                            num6 += sourcey[i];
                            num4++;
                        }
                        continue;
                    }
                    default:
                        goto Label_03CA;
                }
                num6 /= (double) Math.Max(1, num4);
            Label_018B:
                source[n] = num6;
                n++;
                num6 = 0.0;
                num4 = 0;
                flag = false;
            Label_01AC:
                if (validflags[i])
                {
                    num6 += sourcey[i];
                    num4++;
                }
                continue;
            Label_024A:
                source[n] = maxValue;
            Label_0255:
                n++;
                flag = false;
                maxValue = double.MaxValue;
            Label_0269:
                if (validflags[i] && (sourcey[i] < maxValue))
                {
                    maxValue = sourcey[i];
                }
                continue;
            Label_02B9:
                source[n] = minValue;
            Label_02C4:
                n++;
                flag = false;
                minValue = double.MinValue;
            Label_02D8:
                if (validflags[i] && (sourcey[i] > minValue))
                {
                    minValue = sourcey[i];
                }
                continue;
            Label_032F:
                source[n] = maxValue;
            Label_033A:
                n++;
                maxValue = double.MaxValue;
                if (minValue == double.MinValue)
                {
                    source[n] = double.MaxValue;
                }
                else
                {
                    source[n] = minValue;
                }
                minValue = double.MinValue;
                n++;
            Label_0388:
                flag2 = !flag2;
                flag = false;
            Label_0392:
                if (validflags[i])
                {
                    if (sourcey[i] > minValue)
                    {
                        minValue = sourcey[i];
                    }
                    if (sourcey[i] < maxValue)
                    {
                        maxValue = sourcey[i];
                    }
                }
                continue;
            Label_03CA:
                if (flag || (i == lFirst))
                {
                    source[n] = sourcey[i];
                    n++;
                    flag = false;
                }
            }
            DoubleArray array2 = new DoubleArray(n);
            DoubleArray.CopyArray(source, 0, array2, 0, n);
            return array2;
        }

        public static bool NearTest(double a1, double a2, double threshold)
        {
            if (Math.Abs((double) (a2 - a1)) > threshold)
            {
                return false;
            }
            return true;
        }

        public static string NumToString(double num, int format, int decs, string numStrPostfix)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            int num3 = decs;
            double num4 = num;
            string str2 = "F";
            switch (format)
            {
                case 1:
                    str2 = "F";
                    goto Label_026B;

                case 2:
                    builder.Append("0.0E0");
                    num3 = AdjustDecs(num, decs);
                    str2 = "E";
                    goto Label_026B;

                case 3:
                    break;

                case 4:
                    num4 = Math.Abs(num);
                    if ((num4 >= 999000.0) || ((num4 > 0.0) && (num4 < 9.99E-07)))
                    {
                        builder.Append("0.0E0");
                        num3 = Math.Abs(decs);
                        str2 = "e";
                    }
                    else
                    {
                        builder.Append("0.0");
                        str2 = "F";
                    }
                    goto Label_026B;

                case 5:
                    num *= 100.0;
                    builder.Append("0.0");
                    str = "%";
                    str2 = "F";
                    goto Label_026B;

                case 6:
                    builder.Append("0.0E0");
                    num3 = AdjustDecs(num, decs);
                    str2 = "E";
                    goto Label_026B;

                case 7:
                    str2 = "c";
                    break;

                case 8:
                    str2 = "c";
                    goto Label_026B;

                case 10:
                    num *= 100.0;
                    builder.Append("0.0");
                    str2 = "F";
                    goto Label_026B;

                case 12:
                    str2 = "F";
                    str = "s";
                    goto Label_026B;

                default:
                    builder.Append("0.0");
                    goto Label_026B;
            }
            num4 = Math.Abs(num);
            if ((num4 >= 999000000000) && (num4 < 1E+15))
            {
                num /= 1000000000000;
                startIndex = 3;
                str = numStrPostfix.Substring(startIndex, (startIndex + 1) - startIndex);
                num3 += 12;
            }
            else if (num4 >= 999000000.0)
            {
                num /= 1000000000.0;
                startIndex = 2;
                str = numStrPostfix.Substring(startIndex, (startIndex + 1) - startIndex);
                num3 += 9;
            }
            else if (num4 >= 999000.0)
            {
                num /= 1000000.0;
                startIndex = 1;
                str = numStrPostfix.Substring(startIndex, (startIndex + 1) - startIndex);
                num3 += 6;
            }
            else if (num4 >= 1000.0)
            {
                num /= 1000.0;
                startIndex = 0;
                str = numStrPostfix.Substring(startIndex, (startIndex + 1) - startIndex);
                num3 += 3;
            }
            else
            {
                startIndex = 0;
                str = "";
            }
        Label_026B:
            if (num3 < 0)
            {
                num3 = 0;
            }
            str2 = str2 + num3.ToString();
            return (num.ToString(str2) + str);
        }

        public static void SwapCoords(Point2D source)
        {
            if (source != null)
            {
                double x = source.GetX();
                source.SetLocation(source.GetY(), x);
            }
        }

        public static void SwapCoords(Point2D dest, Point2D source)
        {
            if (dest == null)
            {
                dest = new Point2D();
            }
            double x = source.GetX();
            dest.SetLocation(source.GetY(), x);
        }

        public static double ToDegrees(double radians)
        {
            return (180.0 * (radians / 3.1415926535897931));
        }

        public static double ToRadians(double degrees)
        {
            return ((degrees / 180.0) * 3.1415926535897931);
        }
    }
}

