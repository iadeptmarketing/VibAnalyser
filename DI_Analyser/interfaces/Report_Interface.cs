using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace DI_Analyser.interfaces
{
    /// <summary>
    /// Amit Jain   11-Feb-2010
    /// Interface to implement the Reporting Module in the main Analyser Code
    /// </summary>
    interface Report_Interface
    {
        Report _Report
        {
            get;
            set;
        }
        Form1 _Form1
        {
            get;
            set;
        }

        string _CSVData
        {
            get;
            set;
        }
        double[] _Xdata
        {            
            set;
        }
        double[] _Ydata
        {
            set;
        }
        string _Xunit
        {
            set;
        }
        string _Yunit
        {
            set;
        }
        string _CurrentFilePath
        {
            set;
        }
        int generateReport(string selectedReport);
        ArrayList _DATA
        {
            get;
            set;
        }
        ArrayList _WAVDataGridValue
        {
            get;
            set;
        }
        ArrayList _WAVDataValues
        {
            get;
            set;
        }
        string _SelectedCaption
        {
            get;
            set;
        }
    }
}
