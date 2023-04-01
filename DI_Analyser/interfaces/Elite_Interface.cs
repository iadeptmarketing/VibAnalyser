using System;
using System.Collections.Generic;

using System.Text;
using DI_Analyser;
using System.Windows.Forms;

namespace Analyser.interfaces
{
    /// <summary>
    /// 12 April 2012
    /// Interface to control FD2 files
    /// </summary>
    interface Elite_Interface
    {
        Form1 _Form1
        {
            get;
            set;
        }

        DataGridView _dataGridView2
        {
            get;
            set;
        }
        void ReadFD2File(string FileName);
        void ReadOTDfile(string FileName);
        void ReadDRDfile(string FileName);
        string[] _Xunit
        {
            get;
            set;

        }
        string[] _Yunit
        {
            get;
            set;
        }

        string _LineofResolution
        {
            get;
            set;
        }

        string _BandWidth
        {
            get;
            set;
        }

       

        void ReadBA2File(string Dest);
    }
}
