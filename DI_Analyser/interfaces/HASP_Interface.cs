using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace DI_Analyser.interfaces
{
    /// <summary>
    /// Amit Jain   29 Mar 2010
    /// Interface to implement the functions of HASP
    /// </summary>
    interface HASP_Interface
    {
        string getSerialNo();
        string[] ExtractDiRoutUsingUsb();
        string[] ExtractDiRoutUsingUsbfrmCard();
        string Key_serial
        {
            set;
        }
        string[] ExtractRoutes();
        string[] ExtractCSV();
        string[] ExtractWAV();
        Hashtable _CSV
        {
            get;
        }
        Hashtable _WAV
        {
            get;
        }
        Hashtable _TOUR
        {
            get;
        }
        bool DownloadFile(string sPath, string PathofFile, string FileName);
        bool DownloadFile(string sPath, string[] PathofFile, string[] FileName);
    }
}
