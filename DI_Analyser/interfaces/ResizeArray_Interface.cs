using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using com.iAM.chart3dnet;
using com.iAM.chart2dnet;
namespace Analyser.interfaces
{
    interface ResizeArray_Interface
    {
        void IncreaseArrayString(ref string[] values, int increment);
        void IncreaseArrayInt(ref int[] values, int increment);
        void IncreaseArrayByte(ref byte[] values, int increment);
        void IncreaseArrayDouble(ref double[] values, int increment);
        void IncreaseArrayPointF(ref PointF[] values, int increment);


        void IncreaseArrayLinePlot3D(ref com.iAM.chart3dnet.SimpleLinePlot[] values, int increment);
        void IncreaseArrayLinePlot2D(ref com.iAM.chart2dnet.SimpleLinePlot[] values, int increment);
        void IncreaseArrayLinePlotDataset2D(ref com.iAM.chart2dnet.SimpleDataset[] values, int increment);
        void IncreaseArrayLinePlotMarker2D(ref com.iAM.chart2dnet.Marker[] values, int increment);
        void IncreaseArrayLinePlotChartText2D(ref com.iAM.chart2dnet.ChartText[] values, int increment);
        void IncreaseArrayLinePlotChartShape2D(ref com.iAM.chart2dnet.ChartShape[] values, int increment);
    }
}
