using System;
using System.Collections.Generic;

using System.Text;
using DI_Analyser.interfaces;
using System.Windows.Forms;
using trial6;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using Analyser.Classes;
using Analyser.interfaces;

namespace DI_Analyser.Classes
{
    class NewHasp_Control:NewHASP_Interface
    {
        string SelectedMedia = "Storage Card";
        string SelectedModule = "FFT Module";

        RAPI objDI460 = new RAPI();
        Hashtable TourRoute = null;
        Hashtable CSV = null;
        string[] InstrumentData = null;
        int HeightCountOfTour = 0;
        string[] NmsUsb = null;
        string InstrumentSerial = null;

        string SerialFromKey = null;
        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();
        private string[] ExtractDiRoutUsingUsbfrmCard()
        {
            objDI460 = new RAPI();
            string[] Name = new string[0];
            try
            {
                if (objDI460.DevicePresent)
                {
                    if (!objDI460.Connected)
                    {
                        objDI460.Connect();
                    }
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
        private string getSerialNo()
        {
            string serialNo = null;
            //try
            //{
            //    byte[] objbyte = new byte[32];
            //    byte[] objbytein = new byte[0];
            //    objDI460 = new RAPI();
            //    if (objDI460.DevicePresent)
            //    {
            //        if (!objDI460.Connected)
            //        {
            //            objDI460.Connect();
            //        }
            //        try
            //        {
            //            if (objDI460.DeviceFileExists(@"\Windows\DI460RapiDLL.dll"))
            //            {
            //                objDI460.DeleteDeviceFile(@"\Windows\DI460RapiDLL.dll");
            //            }
            //            objDI460.CopyFileToDevice(AppDomain.CurrentDomain.BaseDirectory + "DI460RapiDLL.dll", @"\Windows\DI460RapiDLL.dll");
            //        }
            //        catch (Exception ex)
            //        {
            //            ErrorLog_Class.ErrorLogEntry(ex);
            //        }
            //        objDI460.Invoke(@"\Windows\DI460RapiDLL.dll", "DIGetSerialNumber", objbytein, out objbyte);
            //        serialNo = Encoding.ASCII.GetString(objbyte);
            //        serialNo = serialNo.Trim('\0');
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorLog_Class.ErrorLogEntry(ex);
            //}
            return serialNo;
        }
        private string[] ExtractDiRoutUsingUsb()
        {
            objDI460 = new RAPI();
            string[] Name = new string[0];
            try
            {
                if (objDI460.DevicePresent)
                {
                    if (!objDI460.Connected)
                    {
                        objDI460.Connect();
                    }
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

       

        public void MediaSelected(object sender, EventArgs e)
        {
            try
            {
                RadioButton _RadioButton = (RadioButton)sender;
                SelectedMedia = _RadioButton.Text.ToString();
                setParentTag();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        //private void setParentTag()
        //{
        //    InstrumentData = new string[0];
        //    string[] Name = new string[0];
        //    try
        //    {
        //        InstrumentSerial = getSerialNo();
        //        //if (InstrumentSerial == SerialFromKey)
        //        {
        //            if (objDI460.DevicePresent)
        //            {
        //                if (!objDI460.Connected)
        //                {
        //                    objDI460.Connect();
        //                }

        //                switch (SelectedMedia)
        //                {
        //                    case "Internal Memory":
        //                        {

        //                            switch (SelectedModule)
        //                            {
        //                                case "Analyser and Bump Test":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\Analyser";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Recorder":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser\\Recorder"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\Analyser\\Recorder";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "FRF":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\FRF"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\FRF";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Data Collector":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\~pl302"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\~pl302";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "RUCD":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\RUCD"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\RUCD";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Balancing":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\~pl302\\balance"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\~pl302\\balance";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Conformance Check":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\ConfCheck"))
        //                                        {
        //                                            _ParentTag = "\\Internal Disk\\ConfCheck";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                            }
        //                            break;
        //                        }
        //                    case "PC Card":
        //                        {
        //                            switch (SelectedModule)
        //                            {
        //                                case "Analyser and Bump Test":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\Analyser"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\Analyser";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Recorder":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\Analyser\\Recorder"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\Analyser\\Recorder";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "FRF":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\FRF"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\FRF";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }


        //                                case "Data Collector":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\~pl302"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\~pl302";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "RUCD":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\RUCD"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\RUCD";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Balancing":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\~pl302\\balance"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\~pl302\\balance";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Conformance Check":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\ConfCheck"))
        //                                        {
        //                                            _ParentTag = "\\Storage Card\\ConfCheck";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                            }



        //                            break;
        //                        }
        //                    case "SD Card":
        //                        {
        //                            switch (SelectedModule)
        //                            {
        //                                case "Analyser and Bump Test":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\Analyser"))
        //                                        {

        //                                            _ParentTag = "\\SD Card\\Analyser";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Recorder":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\Analyser\\Recorder"))
        //                                        {
        //                                            _ParentTag = "\\SD Card\\Analyser\\Recorder";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "FRF":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\FRF"))
        //                                        {
        //                                            _ParentTag = "\\SD Card\\FRF";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }


        //                                case "Data Collector":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\~pl302"))
        //                                        {
        //                                            _ParentTag = "\\SD Card\\~pl302";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "RUCD":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\RUCD"))
        //                                        {
        //                                            _ParentTag = "\\SD Card\\RUCD";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Balancing":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\~pl302\\balance"))
        //                                        {
        //                                            _ParentTag = "\\SD Card\\~pl302\\balance";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Conformance Check":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\ConfCheck"))
        //                                        {

        //                                            _ParentTag = "\\SD Card\\ConfCheck";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                            }
        //                            break;
        //                        }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog_Class.ErrorLogEntry(ex);
        //    }           
        //}
        private void setParentTag()
        {
            InstrumentData = new string[0];
            string[] Name = new string[0];
            try
            {
               // InstrumentSerial = getSerialNo();
                //if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        if (!objDI460.Connected)
                        {
                            objDI460.Connect();
                        }

                        switch (SelectedMedia)
                        {
                            case "Storage Card":
                                {
                                    switch (SelectedModule)
                                    {
                                        case "Balancing":
                                            {
                                                //if (objDI460.DeviceFileExists("\\Storage Card\\~pl302\\balance"))
                                                //{

                                                //    FileList objList = objDI460.EnumFiles("\\Storage Card\\~pl302\\balance\\*.*");
                                                //    foreach (FileInformation test1 in objList)
                                                //    {
                                                //        //Array.Resize(ref Name, Name.Length + 1);
                                                //        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                //        Name[Name.Length - 1] = test1.FileName;
                                                //        //  InstrumentData.Add(test1.FileName, "\\Storage Card\\~pl302\\balance\\");
                                                //        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                //        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                //        InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\~pl302\\balance\\";
                                                //    }
                                                //    _ParentTag = "\\Storage Card\\~pl302\\balance";
                                                //}
                                                //else
                                                //{
                                                //    _ParentTag = null;
                                                //}
                                                //break;
                                                if (objDI460.DeviceFileExists(@"\Storage Card\FieldpaqII\Balancer\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\FieldpaqII\Balancer\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\FieldpaqII\Balancer\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\FieldpaqII\Balancer\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                        case "FFT Module":
                                            {
                                                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\FFT\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\Fieldpaq\FFT\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\Fieldpaq\FFT\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\Fieldpaq\FFT\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                        case "Order Tracking Module":
                                            {
                                                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\OrderTracking\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\Fieldpaq\OrderTracking\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\Fieldpaq\OrderTracking\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\Fieldpaq\OrderTracking\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                        case "Data Recorder":
                                            {//This PC\Fingertip5\Storage Card\impaqElite\DataRecorder\Data
                                                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\DataRecorder\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\Fieldpaq\DataRecorder\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\Fieldpaq\DataRecorder\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\Fieldpaq\DataRecorder\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        public string _SelectedMedia
        {
            get
            {
                return SelectedMedia;
            }
            set
            {
                SelectedMedia = value; ;
            }
        }
        public void ModuleSelected(object sender, EventArgs e)
        {
            try
            {
                RadioButton _RadioButton = (RadioButton)sender;
                SelectedModule = _RadioButton.Text.ToString();
                setParentTag();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }

        }
        public string _SelectedModule
        {
            get
            {
                return SelectedModule;
            }
            set
            {
                SelectedModule = value;
            }
        }
        string ParentTag = null;
        public string _ParentTag
        {
            get
            {
                return ParentTag;
            }
            set
            {
                ParentTag = value;
            }
        }
        //public string[] AccessInstrument(string selectedMedia,string SelectedModule)
        //{
        //    InstrumentData = new string[0];
        //    string[] Name = new string[0];
        //    try
        //    {
        //        //InstrumentSerial = getSerialNo();
        //        //if (InstrumentSerial == SerialFromKey)
        //        {
        //            if (objDI460.DevicePresent)
        //            {
        //                if (!objDI460.Connected)
        //                {
        //                    objDI460.Connect();
        //                }

        //                switch (SelectedMedia)
        //                {
        //                    case "Internal Memory":
        //                        {

        //                            switch (SelectedModule)
        //                            {
        //                                case "Analyser and Bump Test":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\Analyser\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;

        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] =  "\\Internal Disk\\Analyser\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\Analyser";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Recorder":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\Analyser\\Recorder"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\Analyser\\Recorder\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Internal Disk\\Analyser\\Recorder\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Internal Disk\\Analyser\\Recorder\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\Analyser\\Recorder";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "FRF":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\FRF"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\FRF\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Internal Disk\\FRF\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Internal Disk\\FRF\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\FRF";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Data Collector":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\~pl302"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\~pl302\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Internal Disk\\~pl302\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Internal Disk\\~pl302\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\~pl302";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "RUCD":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\RUCD"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\RUCD\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\Internal Disk\\RUCD\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Internal Disk\\RUCD\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\RUCD";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Balancing":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\~pl302\\balance"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\~pl302\\balance\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\Internal Disk\\~pl302\\balance\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Internal Disk\\~pl302\\balance\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\~pl302\\balance";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Conformance Check":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Internal Disk\\ConfCheck"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Internal Disk\\ConfCheck\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\Internal Disk\\ConfCheck\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Internal Disk\\ConfCheck\\";
        //                                            }
        //                                            _ParentTag = "\\Internal Disk\\ConfCheck";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                            }
        //                            break;
        //                        }
        //                    case "PC Card":
        //                        {
        //                            switch (SelectedModule)
        //                            {
        //                                case "Analyser and Bump Test":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\Analyser"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\Analyser\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Storage Card\\Analyser\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\Analyser\\";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\Analyser";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Recorder":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\Analyser\\Recorder"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\Analyser\\Recorder\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Storage Card\\Analyser\\Recorder\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\Analyser\\Recorder\\";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\Analyser\\Recorder";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "FRF":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\FRF"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\FRF\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Storage Card\\FRF");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\FRF";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\FRF";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }


        //                                case "Data Collector":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\~pl302"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\~pl302\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Storage Card\\~pl302\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\~pl302\\";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\~pl302";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "RUCD":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\RUCD"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\RUCD\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\Storage Card\\RUCD\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\RUCD\\";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\RUCD";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Balancing":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\~pl302\\balance"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\~pl302\\balance\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                              //  InstrumentData.Add(test1.FileName, "\\Storage Card\\~pl302\\balance\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\~pl302\\balance\\";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\~pl302\\balance";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Conformance Check":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\Storage Card\\ConfCheck"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\Storage Card\\ConfCheck\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\Storage Card\\ConfCheck\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\ConfCheck\\";
        //                                            }
        //                                            _ParentTag = "\\Storage Card\\ConfCheck";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                            }



        //                            break;
        //                        }
        //                    case "SD Card":
        //                        {
        //                            switch (SelectedModule)
        //                            {
        //                                case "Analyser and Bump Test":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\Analyser"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Card\\Analyser\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\Analyser\\";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\Analyser";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Recorder":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\Analyser\\Recorder"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Card\\Analyser\\Recorder\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\Analyser\\Recorder\\";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\Analyser\\Recorder";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "FRF":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\FRF"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Disk\\FRF\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\SD Card\\FRF");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\FRF";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\FRF";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }


        //                                case "Data Collector":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\~pl302"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Card\\~pl302\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\SD Card\\~pl302\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\~pl302\\";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\~pl302";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "RUCD":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\RUCD"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Card\\RUCD\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\SD Card\\RUCD\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\RUCD\\";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\RUCD";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Balancing":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\~pl302\\balance"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Card\\~pl302\\balance\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                               // InstrumentData.Add(test1.FileName, "\\SD Card\\~pl302\\balance\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\~pl302\\balance\\";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\~pl302\\balance";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                                case "Conformance Check":
        //                                    {
        //                                        if (objDI460.DeviceFileExists("\\SD Card\\ConfCheck"))
        //                                        {

        //                                            FileList objList = objDI460.EnumFiles("\\SD Card\\ConfCheck\\*.*");
        //                                            foreach (FileInformation test1 in objList)
        //                                            {
        //                                                //Array.Resize(ref Name, Name.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref Name, 1);
        //                                                Name[Name.Length - 1] = test1.FileName;
        //                                                //InstrumentData.Add(test1.FileName, "\\SD Card\\ConfCheck\\");
        //                                                //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
        //                                                _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
        //                                                InstrumentData[InstrumentData.Length - 1] = "\\SD Card\\ConfCheck\\";
        //                                            }
        //                                            _ParentTag = "\\SD Card\\ConfCheck";
        //                                        }
        //                                        else
        //                                        {
        //                                            _ParentTag = null;
        //                                        }
        //                                        break;
        //                                    }
        //                            }
        //                            break;
        //                        }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog_Class.ErrorLogEntry(ex);
        //    }
        //    return Name;
        //}
        public string[] AccessInstrument(string selectedMedia, string SelectedModule)
        {
            InstrumentData = new string[0];
            string[] Name = new string[0];
            try
            {
                //InstrumentSerial = getSerialNo();
                //if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        if (!objDI460.Connected)
                        {
                            objDI460.Connect();
                        }

                        switch (SelectedMedia)
                        {
                            case "Storage Card":
                                {
                                    switch (SelectedModule)
                                    {
                                        case "Balancing":
                                            {
                                                if (objDI460.DeviceFileExists(@"\Storage Card\FieldpaqII\Balancer\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\FieldpaqII\Balancer\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\FieldpaqII\Balancer\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\FieldpaqII\Balancer\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                                //if (objDI460.DeviceFileExists("\\Storage Card\\~pl302\\balance"))
                                                //{

                                                //    FileList objList = objDI460.EnumFiles("\\Storage Card\\~pl302\\balance\\*.*");
                                                //    foreach (FileInformation test1 in objList)
                                                //    {
                                                //        //Array.Resize(ref Name, Name.Length + 1);
                                                //        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                //        Name[Name.Length - 1] = test1.FileName;
                                                //        //  InstrumentData.Add(test1.FileName, "\\Storage Card\\~pl302\\balance\\");
                                                //        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                //        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                //        InstrumentData[InstrumentData.Length - 1] = "\\Storage Card\\~pl302\\balance\\";
                                                //    }
                                                //    _ParentTag = "\\Storage Card\\~pl302\\balance";
                                                //}
                                                //else
                                                //{
                                                //    _ParentTag = null;
                                                //}
                                                //break;
                                            }
                                        case "FFT Module":
                                            {
                                                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\FFT\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\Fieldpaq\FFT\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\Fieldpaq\FFT\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\Fieldpaq\FFT\Data";
                                                }
                                                else if (objDI460.DeviceFileExists(@"\Storage Card\impaqElite\FFT\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\impaqElite\FFT\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\impaqElite\FFT\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\impaqElite\FFT\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                        case "Order Tracking Module":
                                            {
                                                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\OrderTracking\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\Fieldpaq\OrderTracking\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\Fieldpaq\OrderTracking\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\Fieldpaq\OrderTracking\Data";
                                                }
                                                else if (objDI460.DeviceFileExists(@"\Storage Card\impaqElite\OrderTracking\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\impaqElite\OrderTracking\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\impaqElite\OrderTracking\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\impaqElite\OrderTracking\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                        case "Data Recorder":
                                            {//This PC\Fingertip5\Storage Card\impaqElite\DataRecorder\Data
                                                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\DataRecorder\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\Fieldpaq\DataRecorder\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\Fieldpaq\DataRecorder\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\Fieldpaq\DataRecorder\Data";
                                                }
                                                else if (objDI460.DeviceFileExists(@"\Storage Card\impaqElite\DataRecorder\Data"))
                                                {

                                                    FileList objList = objDI460.EnumFiles(@"\Storage Card\impaqElite\DataRecorder\Data\*.*");
                                                    foreach (FileInformation test1 in objList)
                                                    {
                                                        //Array.Resize(ref Name, Name.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref Name, 1);
                                                        Name[Name.Length - 1] = test1.FileName;
                                                        // InstrumentData.Add(test1.FileName, "\\SD Card\\Analyser\\Recorder\\");
                                                        //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                                                        _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                                                        InstrumentData[InstrumentData.Length - 1] = @"\Storage Card\impaqElite\DataRecorder\Data\";
                                                    }
                                                    _ParentTag = @"\Storage Card\impaqElite\DataRecorder\Data";
                                                }
                                                else
                                                {
                                                    _ParentTag = null;
                                                }
                                                break;
                                            }
                                    }
                                    break;
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


        public string[] _InstrumentData
        {
            get
            {
                return InstrumentData;
            }
        }

        public string Key_serial
        {
            set
            {
                SerialFromKey = value;
            }
        }
       
        public string[] AccessInstrument(string SelectedMedia, string SelectedModule, DevExpress.XtraTreeList.Nodes.TreeListNode treeListNode)
        {
            CSV = new Hashtable();
            InstrumentData = new string[0];
            string[] Name = new string[0];
            DevExpress.XtraTreeList.Nodes.TreeListNode ParentNode = treeListNode.ParentNode;
            try
            {
                
                InstrumentSerial = getSerialNo();
                
                if (InstrumentSerial == SerialFromKey)
                {
                    if (objDI460.DevicePresent)
                    {
                        if (!objDI460.Connected)
                        {
                            objDI460.Connect();
                        }
                      string Parentname=  ParentNode.GetDisplayText(0).ToString();
                      string ParentTag = ParentNode.Tag.ToString();
                      string CurrentName = treeListNode.GetDisplayText(0).ToString();
                      string CurrentTag = treeListNode.Tag.ToString();
                       if(objDI460.DeviceFileExists(CurrentTag+CurrentName))
                       {
                           FileList objList = objDI460.EnumFiles(CurrentTag + CurrentName+"\\*.*");
                           foreach (FileInformation test1 in objList)
                           {
                               //Array.Resize(ref Name, Name.Length + 1);
                               _ResizeArray.IncreaseArrayString(ref Name, 1);
                               Name[Name.Length - 1] = test1.FileName;
                               // InstrumentData.Add(test1.FileName, "\\SD Card\\~pl302\\balance\\");
                               //Array.Resize(ref InstrumentData, InstrumentData.Length + 1);
                               _ResizeArray.IncreaseArrayString(ref InstrumentData, 1);
                               InstrumentData[InstrumentData.Length - 1] = CurrentTag + CurrentName+"\\";
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

        bool IsDownload = false;
        public bool _DownloadComplete
        {
            get
            {
                return IsDownload;
            }
            set
            {
                IsDownload = value;
            }
        }
        public void DownloadInstrumenttoSystem(string Instrument, string Computer)
        {
            try
            {

                if (Instrument.ToString().EndsWith(".fd2", true, null) || Instrument.ToString().EndsWith(".otd", true, null) || Instrument.ToString().EndsWith(".fs2", true, null) || Instrument.ToString().EndsWith(".ots", true, null) || Instrument.ToString().EndsWith(".drd", true, null))// || Instrument.ToString().EndsWith(".cfg", true, null) || Instrument.ToString().EndsWith(".ccr", true, null) || Instrument.ToString().EndsWith(".ccs", true, null))
                {
                    if (File.Exists(Computer))
                    {
                        string[] sarrcomputer = Computer.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                        string spath = null;

                        if (MessageBoxEx.Show(sarrcomputer[sarrcomputer.Length - 1] + " already exists. Do you want to overwrite", "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            objDI460.CopyFileFromDevice(Computer, Instrument, true);
                            IsDownload = true;
                        }
                        else
                        {
                            IsDownload = false;
                        }
                    }
                    else
                    {
                        objDI460.CopyFileFromDevice(Computer, Instrument, true);
                        IsDownload = true;
                    }
                }
                else
                {
                    bool bCreate = true;
                    string[] sarrcomputer = Computer.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    string spath = null;
                    for (int i = 0; i < sarrcomputer.Length - 1; i++)
                    {
                        spath = spath + sarrcomputer[i] + "\\";
                    }
                    if (Directory.Exists(Computer))
                    {
                       
                        DialogResult dr=MessageBoxEx.Show("Do you want to overwrite existing folder"+"\n"+"Click No to append data in the folder", "Overwrite", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                        {
                           
                        }
                        else
                        {
                            Directory.Delete(Computer,true);
                        }
                    }
                    if (bCreate)
                    {
                        Directory.CreateDirectory(Computer);

                        FileList objList = objDI460.EnumFiles(Instrument + "\\*.*");
                        if (objList != null)
                        {
                            foreach (FileInformation test1 in objList)
                            {
                                //if (test1.FileName.ToString().EndsWith(".csv", true, null) || test1.FileName.ToString().EndsWith(".dat", true, null) || test1.FileName.ToString().EndsWith(".wav", true, null) || test1.FileName.ToString().EndsWith(".bal", true, null))
                                //{
                                string[] OldFile = test1.FileName.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                string sNewFile = null;
                                for (int i = 0; i < OldFile.Length - 1; i++)
                                {
                                    sNewFile += OldFile[i].ToString();
                                }
                                sNewFile += "." + OldFile[OldFile.Length - 1].ToString();
                                string path = Instrument + "\\" + test1.FileName.ToString();
                                if (objDI460.DeviceFileExists(path))
                                {
                                    DownloadInstrumenttoSystem(path, Computer + "\\" + sNewFile);
                                }
                                //}
                                //else
                                //{

                                //}

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                //MessageBoxEx.Show(ex.Message);
            }
        }



        


        public bool IsInstrumentAccessible()
        {
            bool AccessGranted = false;
            try
            {
                InstrumentSerial = getSerialNo();
                if (InstrumentSerial != null)
                {
                    if (InstrumentSerial == SerialFromKey)
                    {
                        if (objDI460.DevicePresent)
                        {
                            if (!objDI460.Connected)
                            {
                                objDI460.Connect();
                            }
                            AccessGranted = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return AccessGranted;
        }



        #region NewHASP_Interface Members


        public string GetInstrumentName()
        {
            string returnstring = null;
            //throw new NotImplementedException();
            objDI460 = new RAPI();
            if (objDI460.DevicePresent)
            {
                if (!objDI460.Connected)
                {
                    objDI460.Connect();
                }
                if (objDI460.DeviceFileExists(@"\Storage Card\Fieldpaq\FFT\Data"))
                {
                    returnstring= "Fieldpaq";
                }
                else if (objDI460.DeviceFileExists(@"\Storage Card\impaqElite\FFT\Data"))
                {
                    returnstring= "impaqElite";
                }
            }
            return returnstring;
        }

        #endregion
    }
}
