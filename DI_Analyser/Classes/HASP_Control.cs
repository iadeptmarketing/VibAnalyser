using System;
using System.Collections.Generic;

using System.Text;
using DI_Analyser.interfaces;
using trial6;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using Analyser.Classes;
using Analyser.interfaces;

namespace DI_Analyser.Classes
{
    class HASP_Control : HASP_Interface
    {

        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();
        public string getSerialNo()
        {
            string serialNo = null;
            try
            {
                byte[] objbyte = new byte[32];
                byte[] objbytein = new byte[0];
                objDI460 = new RAPI();
                if (objDI460.DevicePresent)
                {
                    objDI460.Connect();
                    try
                    {
                        if (objDI460.DeviceFileExists(@"\Windows\DI460RapiDLL.dll"))
                        {
                            objDI460.DeleteDeviceFile(@"\Windows\DI460RapiDLL.dll");
                        }
                        objDI460.CopyFileToDevice(AppDomain.CurrentDomain.BaseDirectory + "DI460RapiDLL.dll", @"\Windows\DI460RapiDLL.dll");
                    }
                    catch (Exception ex)
                    {
                        ErrorLog_Class.ErrorLogEntry(ex);
                    }
                    objDI460.Invoke(@"\Windows\DI460RapiDLL.dll", "DIGetSerialNumber", objbytein, out objbyte);
                    serialNo = Encoding.ASCII.GetString(objbyte);
                    serialNo = serialNo.Trim('\0');
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return serialNo;
        }

        public string[] ExtractDiRoutUsingUsb()
        {
            objDI460 = new RAPI();
            string[] Name = new string[0];
            try
            {
                if (objDI460.DevicePresent)
                {
                    objDI460.Connect();
                    if (objDI460.DeviceFileExists("\\Internal Disk\\~pl302\\config\\config.p11"))
                    {

                        FileList objList = objDI460.EnumFiles("\\Internal Disk\\~pl302\\Tour*");
                        foreach (FileInformation test1 in objList)
                        {
                            //Array.Resize(ref Name, Name.Length + 1);
                            _ResizeArray.IncreaseArrayString(ref Name, 1);
                            Name[Name.Length - 1] = test1.FileName;

                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("Connect DI 460 First then try again");
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return Name;
        }
        RAPI objDI460 = new RAPI();
        public string[] ExtractDiRoutUsingUsbfrmCard()
        {
            objDI460 = new RAPI();
            string[] Name = new string[0];
            try
            {
                if (objDI460.DevicePresent)
                {
                    objDI460.Connect();
                    if (objDI460.DeviceFileExists("\\Storage Card\\~pl302"))
                    {

                        FileList objList = objDI460.EnumFiles("\\Storage Card\\~pl302\\Tour*");
                        if (objList != null)
                        {
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;

                            }
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("Either Card Not Accessible or Instrument Not Connected. Try Again");
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return Name;
        }


        #region HASP_Interface Members

        string SerialFromKey = null;
        public string Key_serial
        {
            set
            {
                SerialFromKey = value;
            }
        }

        public Hashtable _CSV
        {
            get
            {
                return CSV;
            }
        }
        public Hashtable _WAV
        {
            get
            {
                return WAV;
            }
        }
        public Hashtable _TOUR
        {
            get
            {
                return TourRoute;
            }
        }


        //Hashtable objTableForRoutes = null;
        Hashtable TourRoute = null;
        Hashtable CSV = null;
        Hashtable WAV = null;
        int HeightCountOfTour = 0;
        string[] NmsUsb = null;
        string InstrumentSerial = null;
        public string[] ExtractCSV()
        {
            CSV = new Hashtable();
            string[] Name = new string[0];
            try
            {
                InstrumentSerial = getSerialNo();
                if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        objDI460.Connect();
                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser"))
                        {

                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\Analyser\\*.csv");
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;
                                CSV.Add(test1.FileName, "\\Internal Disk\\Analyser\\");
                            }
                        }
                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser\\Recorder"))
                        {

                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\Analyser\\Recorder\\*.csv");
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;
                                CSV.Add(test1.FileName, "\\Internal Disk\\Analyser\\Recorder\\");
                            }
                        }
                        if (objDI460.DeviceFileExists("\\Internal Disk\\FRF"))
                        {

                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\FRF\\*.csv");
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;
                                CSV.Add(test1.FileName, "\\Internal Disk\\FRF\\");
                            }
                        }
                        if (objDI460.DeviceFileExists("\\Storage Card\\FRF"))
                        {

                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\FRF\\*.csv");
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;
                                CSV.Add(test1.FileName, "\\Storage Card\\FRF");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return Name;
        }
        public string[] ExtractWAV()
        {
            WAV = new Hashtable();
            string[] Name = new string[0];
            try
            {
                InstrumentSerial = getSerialNo();
                if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        objDI460.Connect();
                        if (objDI460.DeviceFileExists("\\Internal Disk\\RUCD"))
                        {

                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\RUCD\\*.wav");
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;
                                WAV.Add(test1.FileName, "\\Internal Disk\\RUCD\\");
                            }
                        }
                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser\\Recorder"))
                        {

                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\Analyser\\Recorder\\*.wav");
                            foreach (FileInformation test1 in objList)
                            {
                                //Array.Resize(ref Name, Name.Length + 1);
                                _ResizeArray.IncreaseArrayString(ref Name, 1);
                                Name[Name.Length - 1] = test1.FileName;
                                WAV.Add(test1.FileName, "\\Internal Disk\\Analyser\\Recorder\\");
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return Name;
        }
        public string[] ExtractRoutes()
        {
            //objTableForRoutes = new Hashtable();
            TourRoute = new Hashtable();
            objDI460 = new RAPI();
            string[] RouteNamesUsb = new string[0];
            try
            {
                InstrumentSerial = getSerialNo();
                if (InstrumentSerial == SerialFromKey)
                {
                    string[] RoutesUsb = ExtractDiRoutUsingUsb();
                    {
                        for (int i = 0; i < RoutesUsb.Length; i++)
                        {
                            byte[] arrPrsntName = new byte[0];
                            if (objDI460.DeviceFileExists(@"\Internal Disk\~pl302\" + RoutesUsb[i] + "\\ctrl.cfg"))
                            {
                                objDI460.CopyFileFromDevice(AppDomain.CurrentDomain.BaseDirectory + "ctrl.cfg", @"\Internal Disk\~pl302\" + RoutesUsb[i] + "\\ctrl.cfg", true);


                                StreamReader objInput = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "ctrl.cfg", System.Text.Encoding.Default);
                                string contents = objInput.ReadToEnd().Trim();
                                string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", RegexOptions.None);
                                string[] objStr = new string[split.Length];
                                int ik = 0;

                                byte[] bytes = Encoding.ASCII.GetBytes(contents);
                                for (int j = 10; j < 28; j++)
                                {
                                    try
                                    {
                                        if (bytes[j] > 64 && bytes[j] < 91 || bytes[j] > 47 && bytes[j] < 58 || bytes[j] == 45 || bytes[j] > 96 && bytes[j] < 123 || bytes[j] == 32 || bytes[j] == 95)//Sorting Displavable carecters
                                        {
                                            //Array.Resize(ref arrPrsntName, arrPrsntName.Length + 1);
                                            _ResizeArray.IncreaseArrayByte(ref arrPrsntName, 1);
                                            arrPrsntName[arrPrsntName.Length - 1] = bytes[j];


                                        }
                                    }
                                    catch (Exception exx)
                                    {
                                        ErrorLog_Class.ErrorLogEntry(exx);
                                        break;
                                    }
                                }
                                ik++;

                                objInput.Close();
                                string NmRt = Encoding.ASCII.GetString(arrPrsntName);
                                if (!string.IsNullOrEmpty(NmRt))
                                {
                                    //Array.Resize(ref RouteNamesUsb, RouteNamesUsb.Length + 1);
                                    _ResizeArray.IncreaseArrayString(ref RouteNamesUsb, 1);
                                    RouteNamesUsb[RouteNamesUsb.Length - 1] = NmRt + "/Inst";
                                    //objTableForRoutes.Add((object)NmRt, (object)RoutesUsb[i]);
                                    string[] CalcCtr = RoutesUsb[i].Split(new string[] { "TOUR", "Tour", "tour" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (CalcCtr.Length > 0)
                                    {
                                        if (Convert.ToInt32(CalcCtr[0]) > HeightCountOfTour)
                                        {
                                            HeightCountOfTour = Convert.ToInt32(CalcCtr[0]);
                                        }
                                    }
                                    TourRoute.Add(NmRt+"/Inst", @"\Internal Disk\~pl302\"+RoutesUsb[i].ToString());

                                }
                            }
                        }
                        {
                            if (objDI460.DeviceFileExists(@"\Storage Card"))
                            {
                                string[] RoutesUsbCard = ExtractDiRoutUsingUsbfrmCard();
                                for (int i = 0; i < RoutesUsbCard.Length; i++)
                                {
                                    byte[] arrPrsntName = new byte[0];
                                    if (objDI460.DeviceFileExists(@"\Storage Card\~pl302\" + RoutesUsbCard[i] + "\\ctrl.cfg"))
                                    {
                                        objDI460.CopyFileFromDevice(AppDomain.CurrentDomain.BaseDirectory + "ctrl.cfg", @"\Storage Card\~pl302\" + RoutesUsbCard[i] + "\\ctrl.cfg", true);


                                        StreamReader objInput = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "ctrl.cfg", System.Text.Encoding.Default);
                                        string contents = objInput.ReadToEnd().Trim();
                                        string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", RegexOptions.None);
                                        string[] objStr = new string[split.Length];
                                        int ik = 0;
                                        byte[] bytes = Encoding.ASCII.GetBytes(contents);
                                        for (int j = 10; j < 28; j++)
                                        {
                                            try
                                            {
                                                if (bytes[j] > 64 && bytes[j] < 91 || bytes[j] > 47 && bytes[j] < 58 || bytes[j] == 45 || bytes[j] > 96 && bytes[j] < 123 || bytes[j] == 32 || bytes[j] == 95)//Sorting Displavable carecters
                                                {
                                                    //Array.Resize(ref arrPrsntName, arrPrsntName.Length + 1);
                                                    _ResizeArray.IncreaseArrayByte(ref arrPrsntName, 1);
                                                    arrPrsntName[arrPrsntName.Length - 1] = bytes[j];


                                                }
                                            }
                                            catch (Exception eec)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(eec);
                                                break;
                                            }
                                        }
                                        ik++;

                                        objInput.Close();
                                        string NmRt = Encoding.ASCII.GetString(arrPrsntName);
                                        if (!string.IsNullOrEmpty(NmRt))
                                        {
                                            //Array.Resize(ref RouteNamesUsb, RouteNamesUsb.Length + 1);
                                            _ResizeArray.IncreaseArrayString(ref RouteNamesUsb, 1);
                                            RouteNamesUsb[RouteNamesUsb.Length - 1] = NmRt + "/Card";
                                            //objTableForRoutes.Add((object)NmRt, (object)RoutesUsbCard[i]);
                                            string[] CalcCtr = RoutesUsbCard[i].Split(new string[] { "TOUR", "Tour", "tour" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (CalcCtr.Length > 0)
                                            {
                                                if (Convert.ToInt32(CalcCtr[0]) > HeightCountOfTour)
                                                {
                                                    HeightCountOfTour = Convert.ToInt32(CalcCtr[0]);
                                                }
                                            }
                                            TourRoute.Add(NmRt + "/Card", @"\Storage Card\~pl302\" + RoutesUsbCard[i].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (InstrumentSerial == null)
                    {
                        MessageBoxEx.Show("Instrument Not Connected");
                    }
                    else
                    {
                        MessageBoxEx.Show("Key Does Not Match With the Instrument", "Key Mismatch");
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return RouteNamesUsb;
        }

        public bool DownloadFile(string sPath, string PathofFile,string FileName)
        {
            bool ReturnValue = false;
            try
            {
                if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        if (!objDI460.Connected)
                        {
                            objDI460.Connect();
                        }
                        if (objDI460.DeviceFileExists(PathofFile))
                        {
                            objDI460.CopyFileFromDevice(sPath +"\\"+ FileName, PathofFile, true);
                            ReturnValue = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                
            }
            return ReturnValue;
        }
        public bool DownloadFile(string sPath, string[] PathofFile, string[] FileName)
        {
            bool ReturnValue = false;
            try
            {
                if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        if (!objDI460.Connected)
                        {
                            objDI460.Connect();
                        }
                        for (int i = 0; i < PathofFile.Length; i++)
                        {
                            if (objDI460.DeviceFileExists(PathofFile[i]))
                            {
                                objDI460.CopyFileFromDevice(sPath + "\\" + FileName[i], PathofFile[i], true);
                                ReturnValue = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);

            }
            return ReturnValue;
        }

        #endregion
    }
}
