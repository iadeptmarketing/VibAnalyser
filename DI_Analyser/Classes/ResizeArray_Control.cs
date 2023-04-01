using System;
using System.Collections.Generic;

using System.Text;
using Analyser.interfaces;
using System.Drawing;
using com.iAM.chart3dnet;
using com.iAM.chart2dnet;
namespace Analyser.Classes
{
    class ResizeArray_Control:ResizeArray_Interface
    {
        #region ResizeArray_Interface Members

        public void IncreaseArrayString(ref string[] values, int increment)
        {
            string[] array = new string[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;

        }

        public void IncreaseArrayInt(ref int[] values, int increment)
        {
            int[] array = new int[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayByte(ref byte[] values, int increment)
        {
            byte[] array = new byte[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayDouble(ref double[] values, int increment)
        {
            double[] array = new double[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayPointF(ref PointF[] values, int increment)
        {
            PointF[] array = new PointF[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayLinePlot3D(ref com.iAM.chart3dnet.SimpleLinePlot[] values, int increment)
        {
            com.iAM.chart3dnet.SimpleLinePlot[] array = new com.iAM.chart3dnet.SimpleLinePlot[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayLinePlot2D(ref com.iAM.chart2dnet.SimpleLinePlot[] values, int increment)
        {
            com.iAM.chart2dnet.SimpleLinePlot[] array = new com.iAM.chart2dnet.SimpleLinePlot[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayLinePlotDataset2D(ref com.iAM.chart2dnet.SimpleDataset[] values, int increment)
        {
            com.iAM.chart2dnet.SimpleDataset[] array = new com.iAM.chart2dnet.SimpleDataset[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayLinePlotMarker2D(ref com.iAM.chart2dnet.Marker[] values, int increment)
        {
            com.iAM.chart2dnet.Marker[] array = new com.iAM.chart2dnet.Marker[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayLinePlotChartText2D(ref com.iAM.chart2dnet.ChartText[] values, int increment)
        {
            com.iAM.chart2dnet.ChartText[] array = new com.iAM.chart2dnet.ChartText[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }

        public void IncreaseArrayLinePlotChartShape2D(ref com.iAM.chart2dnet.ChartShape[] values, int increment)
        {
            com.iAM.chart2dnet.ChartShape[] array = new com.iAM.chart2dnet.ChartShape[values.Length + increment];

            values.CopyTo(array, 0);

            values = array;
        }



        #endregion
    }
}
