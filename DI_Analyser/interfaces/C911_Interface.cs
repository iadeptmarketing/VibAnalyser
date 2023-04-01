using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DI_Analyser;
using System.Windows.Forms;

namespace Analyser.interfaces
{
    interface C911_Interface
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
        void ReadFFTFile(string FileName);

        bool _Is2Channel
        {
            get;
        }
        float _RMS
        {
            get;
            set;
        }
        float _RMS2
        {
            get;
            set;
        }
        double _highestFreq
        {
            get;
            set;
        }
        float _P_P
        {
            get;
            set;
        }
        float _P_P2
        {
            get;
            set;
        }
        double _dF
        {
            get;
            set;
        }

        int _Number_Of_Spectrum
        {
            get;
            set;
        }
        int _Window
        {
            get;
            set;
        }
        int _Window2
        {
            get;
            set;
        }
        int _pwr2
        {
            get;
            set;
        }

        int _Measurement
        {
            get;
            set;
        }
        int _Measurement2
        {
            get;
            set;
        }
        int _ChnlA
        {
            get;
            set;
        }

        int _ChnlB
        {
            get;
            set;
        }


        int _Trig
        {
            get;
            set;
        }

        int _Avgm
        {
            get;
            set;
        }


        int _Navg
        {
            get;
            set;
        }

        int _EPC
        {
            get;
            set;
        }

        int _Ampmode
        {
            get;
            set;
        }
        int _Ampmode2
        {
            get;
            set;
        }
        int _Amphpf
        {
            get;
            set;
        }
        int _Amphpf2
        {
            get;
            set;
        }
        int _Ampenvcr
        {
            get;
            set;
        }
        int _Ampenvcr2
        {
            get;
            set;
        }
        float _Sens
        {
            get;
            set;
        }
        float _Sens2
        {
            get;
            set;
        }
        ulong _SerialN
        {
            get;
            set;
        }

        int _Revision
        {
            get;
            set;
        }

    }
}
