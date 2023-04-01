using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;


namespace DI_Analyser.interfaces
{
    /// <summary>
    /// Amit Jain   5 Mar 2010
    /// Interface to implement the functions for the click of the .dat file
    /// </summary>
    interface Dat_Interface
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

        void GetDIfromDatabase(string _Path);

    }
}
