using System;
using System.Collections.Generic;

using System.Text;
using Analyser.Forms;

namespace DI_Analyser.interfaces
{
    interface Bal_interface
    {
        void ReadBalFile(string Dest);
        double Amplitude
        {
            get;
        }
        double angle
        {
            get;
        }
        string Detection
        {
            get;
        }
        string MeasType
        {
            get;
        }
        BalDataForm _BALForm
        {
            get;
            set;
        }
    }
}
