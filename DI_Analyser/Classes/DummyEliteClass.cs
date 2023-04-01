using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Analyser.interfaces;
using DI_Analyser;

namespace Analyser.Classes
{
    class DummyEliteClass : Elite_Interface
    {
        public Form1 _Form1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DataGridView _dataGridView2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string[] _Xunit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string[] _Yunit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _LineofResolution { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _BandWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ReadBA2File(string Dest)
        {
            throw new NotImplementedException();
        }

        public void ReadDRDfile(string FileName)
        {
            throw new NotImplementedException();
        }

        public void ReadFD2File(string FileName)
        {
            throw new NotImplementedException();
        }

        public void ReadOTDfile(string FileName)
        {
            throw new NotImplementedException();
        }
    }
}
