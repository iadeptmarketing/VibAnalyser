using System;
using System.Collections.Generic;

using System.Text;
using DI_Analyser.interfaces;
using System.IO;
using System.Windows.Forms;
using Analyser.Properties;
using Analyser.Classes;
using Analyser.interfaces;

namespace DI_Analyser.Classes
{
    class Dat_Control:Dat_Interface
    {
        public void GetDIfromDatabase(string _Path)
        {
            try
            {

                objlistimg.Images.Add(Resources.DarkRed);
                objlistimg.Images.Add(Resources.DarkGreen);
                objlistimg.Images.Add(Resources.DarkGoldenRod);
                objlistimg.Images.Add(Resources.DarkVoilet);
                objlistimg.Images.Add(Resources.DarkBlue);
                objlistimg.Images.Add(Resources.DimGrey);
                objlistimg.Images.Add(Resources.Chocolate);
                objlistimg.Images.Add(Resources.DarkKhaki);
                objlistimg.Images.Add(Resources.Black);
                objlistimg.Images.Add(Resources.Orange);
                objlistimg.Images.Add(Resources.Cyan);
                objlistimg.Images.Add(Resources.AquaMarine);
                objlistimg.Images.Add(Resources.Bisque);
                objlistimg.Images.Add(Resources.Blue);
                objlistimg.Images.Add(Resources.BlueViolet);
                objlistimg.Images.Add(Resources.Coral);
                objlistimg.Images.Add(Resources.Darkmagenta);
                objlistimg.Images.Add(Resources.DarkseaGreen);
                objlistimg.Images.Add(Resources.DarkSlateBlue);
                objlistimg.Images.Add(Resources.Deeppink);
                objlistimg.Images.Add(Resources.DodgerBlue);
                objlistimg.Images.Add(Resources.FireBrick);
                objlistimg.Images.Add(Resources.ForestGreen);
                objlistimg.Images.Add(Resources.GreenYellow);
                objlistimg.Images.Add(Resources.HotPink);
                objlistimg.Images.Add(Resources.IndianRed);
                objlistimg.Images.Add(Resources.Darkorange);
                objlistimg.Images.Add(Resources.Darkorchid);
                objlistimg.Images.Add(Resources.DeepSkyBlue);
                objlistimg.Images.Add(Resources.SandyBrown);
                //CreateXMLStartingElements();
                ////myDataBase = null;
                ////myDataBase = "Data Source=" + _SdfPath;
                trendValCtr = 0;
                using (FileStream objInput = new FileStream(_Path, FileMode.Open, FileAccess.Read))
                {
                    byte[] MainArr = new byte[(int)objInput.Length];
                    int contents = objInput.Read(MainArr, 0, (int)objInput.Length);
                    if (Directory.Exists("c:\\vvtemp\\"))
                    {
                        Directory.Delete("c:\\vvtemp\\", true);
                    }
                    ExtractOffDataDiForUsb(MainArr);

                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }


        
        bool bAlreadyEntered = false;
        int Value = 0;
        bool PhaseExtraction = false;

        int dualChnl = 0;
        int iFScale = 0;    //fullscale--1
        int iMesure = 0;    //measuretype--5
        int iFltrType = 0;  //filter type--3
        int iFltrVal = 0;
        int iFreq = 0;      //frequency--4
        int iWin = 0;       //window--2
        int iCpl = 0;       //couple
        int iUnit = 0;      //unit
        int idetc = 0;      //detection
        int iLor = 0;       //line of resolution
        int isens = 100;      //sensitivity
        bool bmesure = false;
        string NewID = null;
        int trendValCtr = 0;
        FileStream aa = null;
        StreamWriter sw = null;
        StreamReader sr = null;
        ImageList objlistimg = new ImageList();
        bool ISVolt = false;
        bool IsInspection = false;
        bool ISUm = false;
        double OverallValueDecoded = 0.0;
        bool overalFound = false;
        ResizeArray_Interface _ResizeArray = new ResizeArray_Control();

        private void ExtractOffDataDiForUsb(byte[] MainArr)
        {
            bool AckGetBt = false;
            int CtrToStart = 0;
            double key = 0.0;
            double[] FactorUMMS = { 200, 100, 50, 20, 10, 5, 2, 1 };
            double[] FactorUMIL = { 50, 25, 10, 5, 2.5, 1 };
            int arrchannel = 0;
            string sPointName = null;
            string sPointDescription = null;
            string sFactoryName = null;// RoutePresentInCurrentDatabaseAgn(sFactoryName1);
            string sResourceName = null;
            string sElementName = null;
            string sSubElementName = null;
            byte[] NameExtracter = new byte[17];
            double[] FactorInspection = { 1000, 1000, 200, 200, 10, 10 };
            double[] LimitIPS = { 20, 10, 5, 2, 1, .5, .2, .1, .05, .02, .01, .005, .002, .001 };
            double[] LimitMMS = { 500, 250, 125, 50, 25, 12.5, 5, 2.5, 1.25, .5, .25, .125, .05, .025 };
            double[] LimitMIL = { 100, 50, 20, 10, 5, 2, 1, .5, .2, .1, .05, .02 };
            double[] LimitUM = { 2500, 1250, 500, 250, 125, 50, 25, 12.5, 5, 2.5, 1.25, 0.5 };
            double[] LimitG = { 100, 50, 20, 10, 5, 2, 1, .5, .2, .1, .05, .02, .01, .005, .002, .001 };
            double[] LimitVolt = { 10, 5, 2, 1, .5, .2, .1, .05, .02, .01, .005, 20, 50 };
            double[] LimitInspection = { 100000, 10000, 1000, 100, 10, 1, .1, .01, .001 };
            double[] LimitRPM = { 100000, 50000, 10000, 5000, 1000, 500, 100, 50, 10, 5, 1 };

            double[] FreqArray = { 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 40000, 25 };
            double[] ResolutionFFT = { 100, 200, 400, 800, 1600, 3200, 6400 };
            double[] ResolutionTime = { 256, 512, 1024, 2048, 4096, 8192, 16384 };
            string PreviousFacName = null;
            string PreviousEqupname = null;
            string PreviousComname = null;
            string PreviousSubCompname = null;
            string PreviousPointName = null;
            bool SamePointDifferentChannel = false;
            NewID = null;
            dualChnl = 0;
            iFScale = 0;    //fullscale--1
            iMesure = 0;    //measuretype--5
            iFltrType = 0;  //filter type--3
            iFltrVal = 0;
            iFreq = 0;      //frequency--4
            iWin = 0;       //window--2
            iCpl = 0;       //couple
            iUnit = 0;      //unit
            idetc = 0;      //detection
            iLor = 0;       //line of resolution
            isens = 100;      //sensitivity
            bmesure = false;
            bool dataEntered = false;

            double CalculatedFullScale = 0;

            string PointDate = null;
            string PointMonth = null;
            string PointYear = null;
            string PointHour = null;
            string PointMinute = null;
            string PointSecond = null;
            bool DateFound = false;
            int offptctr = 0;
            {
                try
                {
                    Value = 0;
                    do
                    {
                        int Factor = 0;
                        int KeyFactor = 0;
                        AckGetBt = false;
                        do
                        {
                            if (MainArr[CtrToStart] == 0x58 && MainArr[CtrToStart + 1] == 0x02 && MainArr[CtrToStart + 2] == 0x06 && MainArr[CtrToStart + 3] == 0x01)
                            {
                                dualChnl = 0;
                                iWin = 0;
                                iLor = 0;
                                iFScale = 0;
                                iUnit = 0;
                                iMesure = 0;
                                iFltrType = 0;
                                iFltrVal = 0;
                                iFreq = 0;
                                idetc = 0;
                                bmesure = false;
                                string sUnit = null;
                                string sCT = null;
                                int newCtr = CtrToStart + 4;
                                byte[] fs = new byte[1];

                                fs[0] = MainArr[newCtr];
                                //string fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                string fsval =Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                string[] fsvalacc = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                                if (fsvalacc.Length > 1)
                                {
                                    sCT = (fsvalacc[0].ToString());

                                }
                                else
                                {
                                    sCT = (fsvalacc[0].ToString());
                                }
                                newCtr = CtrToStart + 10;
                                //fs = new byte[1];

                                //fs[0] = MainArr[newCtr];
                                //fsval = DeciamlToHexadeciaml1(Convert.ToInt32(fs[0].ToString()));
                                //dualChnl = Convert.ToInt32(fsval);

                                //Amit Jain VA_10   Channel data not correct for Generate from file 4-3-2010
                                string SFH = null;
                                for (int i = newCtr, j = 0; j < 5; i++, j++)
                                {

                                    if (j == 4)
                                    {
                                        if (MainArr[i].ToString() == "32")
                                        {
                                            if (SFH == "2000")
                                            {
                                                dualChnl = 0;
                                            }
                                            else
                                            {
                                                dualChnl = 1;
                                            }
                                        }
                                        else
                                        {
                                            dualChnl = 0;
                                        }
                                    }
                                    SFH += MainArr[i].ToString();
                                }

                                //end

                                //int SF = HexadecimaltoDecimal(SFH);

                                //dualChnl = SF;


                                CtrToStart = CtrToStart + 15;


                                for (int NmCtr = 0; NmCtr < 17; NmCtr++)
                                {
                                    NameExtracter[NmCtr] = MainArr[CtrToStart];
                                    CtrToStart++;
                                }
                                sElementName = Encoding.ASCII.GetString(NameExtracter);
                                sElementName = sElementName.Trim(new char[] { '\0' });
                                NameExtracter = new byte[17];
                                for (int NmCtr = 0; NmCtr < 17; NmCtr++)
                                {
                                    NameExtracter[NmCtr] = MainArr[CtrToStart];
                                    CtrToStart++;
                                }
                                sPointName = Encoding.ASCII.GetString(NameExtracter);
                                sPointName = sPointName.Trim(new char[] { '\0' });
                                NameExtracter = new byte[17];
                                for (int NmCtr = 0; NmCtr < 17; NmCtr++)
                                {
                                    NameExtracter[NmCtr] = MainArr[CtrToStart];
                                    CtrToStart++;
                                }
                                sSubElementName = Encoding.ASCII.GetString(NameExtracter);
                                sSubElementName = sSubElementName.Trim(new char[] { '\0' });
                                NameExtracter = new byte[17];
                                for (int NmCtr = 0; NmCtr < 17; NmCtr++)
                                {
                                    NameExtracter[NmCtr] = MainArr[CtrToStart];
                                    CtrToStart++;
                                }
                                sPointDescription = Encoding.ASCII.GetString(NameExtracter);
                                sPointDescription = sPointDescription.Trim(new char[] { '\0' });
                                NameExtracter = new byte[17];
                                AckGetBt = true;
                                bool ResFound = false;
                                int OverallFactor = 0;
                                int MaxToLook = 0;
                                int MaxToLookForFF = 0;
                                overalFound = false;
                                sUnit = null;
                                newCtr = CtrToStart + 21;
                                fs = new byte[2];
                                fs[1] = MainArr[newCtr];
                                fs[0] = MainArr[newCtr + 1];
                                //fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));common.
                                //string fsval1 = DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                fsval = Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                string fsval1 = Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                fsvalacc = fsval1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                string[] fsvalacc1 = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                if (fsvalacc1.Length > 1)
                                {
                                    sUnit += (fsvalacc1[1].ToString());
                                }
                                else
                                {
                                    sUnit += (fsvalacc1[0].ToString());
                                }
                                if (fsvalacc.Length > 1)
                                {
                                    sUnit += (fsvalacc[0].ToString());
                                    iFScale = Convert.ToInt32(fsvalacc[1].ToString());
                                }
                                else
                                {
                                    try
                                    {
                                        iFScale = Convert.ToInt32(fsvalacc[0].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorLog_Class.ErrorLogEntry(ex);
                                        //iFScale = Convert.ToInt32(HexadecimaltoDecimal(fsvalacc[0].ToString()));
                                        iFScale = Convert.ToInt32(Common.HexadecimaltoDecimal(fsvalacc[0].ToString()));
                                    }
                                }

                                iUnit = Convert.ToInt32(sUnit);
                                switch (iUnit)
                                {
                                    case 0:
                                        {//accel(G)
                                            iUnit = 0;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitG[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 1:
                                        {//A -> V (IPS)
                                            iUnit = 1;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitIPS[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 11:
                                        {//A -> V (MM/S)
                                            iUnit = 2;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitMMS[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 2:
                                        {//VEL (IPS)
                                            iUnit = 5;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitIPS[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 21:
                                        {//VEL (MM/S)
                                            iUnit = 6;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitMMS[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;

                                        }
                                    case 3:
                                        {//A -> D (MILS)
                                            iUnit = 3;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitMIL[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 31:
                                        {//A -> D (UM)
                                            iUnit = 4;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitUM[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 4:
                                        {//V -> D (MILS)
                                            iUnit = 7;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitMIL[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 41:
                                        {//V -> D (UM)
                                            iUnit = 8;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitUM[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 5:
                                        {//DISP (MILS)
                                            iUnit = 9;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitMIL[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 51:
                                        {//DISP (UM)
                                            iUnit = 10;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitUM[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 9:
                                        {//esp
                                            iUnit = 11;
                                            break;
                                        }
                                    case 6:
                                        {//volt
                                            iUnit = 12;
                                            try
                                            {
                                                CalculatedFullScale = (double)LimitVolt[iFScale];
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                CalculatedFullScale = iFScale;
                                            }
                                            break;
                                        }
                                    case 8:
                                        {//RPM
                                            iUnit = 13;
                                            break;
                                        }
                                }

                                string sFT = null;
                                string sFV = null;
                                fs = new byte[2];
                                fs[0] = MainArr[newCtr + 2];
                                fs[1] = MainArr[newCtr + 3];
                                //fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                //fsval1 = DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                fsval =Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                fsval1 = Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                fsvalacc = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                fsvalacc1 = fsval1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                if (fsvalacc.Length > 1)
                                {
                                    for (int i = 0; i < fsvalacc.Length; i++)
                                    {
                                        sFT = sFT + fsvalacc[i].ToString();
                                    }
                                }
                                else
                                {
                                    sFT = fsvalacc[0].ToString();
                                }

                                if (fsvalacc1.Length > 1)
                                {
                                    for (int i = 0; i < fsvalacc1.Length; i++)
                                    {
                                        sFV = sFV + fsvalacc1[i].ToString();
                                    }
                                }
                                else
                                {
                                    sFV = fsvalacc1[0].ToString();
                                }

                                if (sFT == "80")
                                {
                                    if (sFV == "8")
                                    {
                                        iFltrVal = 1;
                                        iFltrType = 2;
                                    }
                                    else if (sFV == "4")
                                    {
                                        iFltrVal = 4; iFltrType = 1;
                                    }
                                    else if (sFV == "5")
                                    {
                                        iFltrVal = 6; iFltrType = 1;
                                    }
                                    else if (sFV == "C")
                                    {
                                        iFltrVal = 4; iFltrType = 3;
                                    }
                                    else if (sFV == "D")
                                    {
                                        iFltrVal = 6;
                                        iFltrType = 3;
                                    }
                                }
                                else if (sFT == "0")
                                {
                                    if (sFV == "8")
                                    {
                                        iFltrVal = 0;
                                        iFltrType = 2;
                                    }
                                    else if (sFV == "9")
                                    {
                                        iFltrVal = 2;
                                        iFltrType = 2;
                                    }
                                    else if (sFV == "4")
                                    {
                                        iFltrVal = 2;
                                        iFltrType = 1;
                                    }
                                    else if (sFV == "5")
                                    {
                                        iFltrVal = 5;
                                        iFltrType = 1;
                                    }
                                    else if (sFV == "6")
                                    {
                                        iFltrVal = 7;
                                        iFltrType = 1;
                                    }
                                    else if (sFV == "C")
                                    {
                                        iFltrVal = 2;
                                        iFltrType = 3;
                                    }
                                    else if (sFV == "D")
                                    {
                                        iFltrVal = 5;
                                        iFltrType = 3;
                                    }
                                    else if (sFV == "E")
                                    {
                                        iFltrVal = 7;
                                        iFltrType = 3;
                                    }
                                    else if (sFV == "0")
                                    {
                                        iFltrVal = 0;
                                        iFltrType = 0;
                                        iMesure = 0;
                                    }
                                }
                                else
                                {
                                    if (sFV == "0")
                                    {
                                        iCpl = Convert.ToInt32(fsvalacc[0].ToString());
                                    }
                                }







                                do
                                {
                                    if ((MainArr[CtrToStart] == 0x3F && MainArr[CtrToStart - 1] == 0x3F && MainArr[CtrToStart - 2] == 0x3F && MainArr[CtrToStart - 3] == 0x3F && MainArr[CtrToStart - 4] == 0x3F && MainArr[CtrToStart - 5] == 0x3F) || (MainArr[CtrToStart] == 0xFF && MainArr[CtrToStart - 1] == 0xFF && MainArr[CtrToStart - 2] == 0xFF && MainArr[CtrToStart - 3] == 0xFF && MainArr[CtrToStart - 4] == 0xFF && MainArr[CtrToStart - 5] == 0xFF))
                                    {
                                        newCtr = CtrToStart - 7;

                                        fs = new byte[2];
                                        fs[0] = MainArr[newCtr];
                                        fs[1] = MainArr[newCtr + 1];
                                        //fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        //fsval1 = DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                        fsval =Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        fsval1 = Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                        fsvalacc = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        if (fsvalacc.Length > 1)
                                        {
                                            iMesure = Convert.ToInt32(fsvalacc[1].ToString());
                                        }
                                        else
                                        {
                                            iMesure = Convert.ToInt32(fsvalacc[0].ToString());
                                        }


                                        newCtr = CtrToStart + 4;

                                        fs = new byte[1];
                                        fs[0] = MainArr[newCtr];

                                        //fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        fsval =Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        fsvalacc = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        if (fsvalacc.Length > 1)
                                        {
                                            idetc = Convert.ToInt32(fsvalacc[1].ToString());
                                        }
                                        else
                                        {
                                            idetc = Convert.ToInt32(fsvalacc[0].ToString());
                                        }

                                        try
                                        {
                                            newCtr = CtrToStart + 18;
                                            PointSecond = Convert.ToString(MainArr[newCtr]);
                                            PointMinute = Convert.ToString(MainArr[newCtr + 1]);
                                            PointHour = Convert.ToString(MainArr[newCtr + 2]);
                                            PointDate = Convert.ToString(MainArr[newCtr + 3]);
                                            PointMonth = Convert.ToString(MainArr[newCtr + 4]);
                                            PointYear = Convert.ToString(MainArr[newCtr + 5]);
                                            PointYear = Convert.ToString((Convert.ToInt16(PointYear)) + 1900);
                                            PointMonth += "/" + PointDate + "/" + PointYear + " " + PointHour + ":" + PointMinute + ":" + PointSecond;
                                            DateFound = true;
                                        }
                                        catch (Exception eee)
                                        {
                                            ErrorLog_Class.ErrorLogEntry(eee);
                                            DateFound = false;
                                        }


                                        KeyFactor = CtrToStart - 11;
                                        if (MainArr[KeyFactor - 12] != 0x1b)
                                        {
                                            try
                                            {
                                                //Factor = Convert.ToInt16(DeciamlToHexadeciaml1(MainArr[KeyFactor]));
                                                Factor = Convert.ToInt16(Common.DeciamlToHexadeciaml1(MainArr[KeyFactor]));
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLog_Class.ErrorLogEntry(ex);
                                                Factor = Convert.ToInt16(MainArr[KeyFactor]);
                                            }
                                        }
                                        else
                                            Factor = 0;
                                        Factor = Factor % 10;

                                        OverallFactor = CtrToStart + 7;

                                        OverallValueDecoded = 0.0;
                                        do
                                        {

                                            if (MainArr[OverallFactor] != 0x00)
                                            {
                                                OverallValueDecoded = ((MainArr[OverallFactor + 1] * 256 + MainArr[OverallFactor]));
                                                overalFound = true;
                                            }
                                            else if (MainArr[OverallFactor] == 0x00)
                                            {
                                                OverallValueDecoded = (MainArr[OverallFactor + 1]);
                                                overalFound = true;
                                            }
                                        } while (overalFound == false);

                                        newCtr = CtrToStart + 31;
                                        fs = new byte[1];
                                        fs[0] = MainArr[newCtr];

                                        //fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        fsval =Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        fsvalacc = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        string sLor = null;
                                        if (fsvalacc.Length > 1)
                                        {
                                            for (int i = 0; i < fsvalacc.Length; i++)
                                            {
                                                sLor = sLor + fsvalacc[i].ToString();
                                            }
                                        }
                                        else
                                        {
                                            sLor = fsvalacc[0].ToString();
                                        }
                                        iLor = Convert.ToInt32(sLor);

                                        switch (iLor)
                                        {
                                            case 0:
                                                {
                                                    iLor = 2;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    iLor = 0;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    iLor = 1;
                                                    break;
                                                }
                                        }


                                        newCtr++;
                                        fs = new byte[2];
                                        fs[0] = MainArr[newCtr];
                                        fs[1] = MainArr[newCtr + 2];
                                        //fsval = DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        //fsval1 = DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                        fsval =Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[0].ToString()));
                                        fsval1 = Common.DeciamlToHexadeciaml(Convert.ToInt32(fs[1].ToString()));
                                        fsvalacc = fsval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        fsvalacc1 = fsval1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        string win = null;
                                        string frq = null;

                                        if (fsvalacc.Length > 1)
                                        {
                                            for (int i = 0; i < fsvalacc.Length; i++)
                                            {
                                                win = win + fsvalacc[i].ToString();
                                            }
                                        }
                                        else
                                        {
                                            win = fsvalacc[0].ToString();
                                        }

                                        if (fsvalacc1.Length > 1)
                                        {
                                            for (int i = 0; i < fsvalacc1.Length; i++)
                                            {
                                                frq = frq + fsvalacc1[i].ToString();
                                            }
                                        }
                                        else
                                        {
                                            frq = fsvalacc1[0].ToString();
                                        }

                                        try
                                        {
                                            iWin = Convert.ToInt32(win);
                                            iFreq = Convert.ToInt32(frq);
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorLog_Class.ErrorLogEntry(ex);
                                            iFreq = 3;
                                        }

                                        CtrToStart = CtrToStart + 57;
                                        for (int NmCtr = 0; NmCtr < 17; NmCtr++)
                                        {
                                            if ((MainArr[CtrToStart] < 58 && MainArr[CtrToStart] > 47) || (MainArr[CtrToStart] < 91 && MainArr[CtrToStart] > 64) || (MainArr[CtrToStart] < 123 && MainArr[CtrToStart] > 96))
                                                NameExtracter[NmCtr] = MainArr[CtrToStart];
                                            CtrToStart++;
                                        }
                                        ResFound = true;
                                        sResourceName = Encoding.ASCII.GetString(NameExtracter);
                                        sResourceName = sResourceName.Trim(new char[] { '\0' });

                                        try
                                        {
                                            newCtr = CtrToStart;
                                            do
                                            {
                                                if (MainArr[newCtr + 5] == 0x6c || MainArr[newCtr + 5] == 0x6d)
                                                {
                                                    PointSecond = Convert.ToString(MainArr[newCtr]);
                                                    PointMinute = Convert.ToString(MainArr[newCtr + 1]);
                                                    PointHour = Convert.ToString(MainArr[newCtr + 2]);
                                                    PointDate = Convert.ToString(MainArr[newCtr + 3]);
                                                    PointMonth = Convert.ToString(MainArr[newCtr + 4]);
                                                    PointYear = Convert.ToString(MainArr[newCtr + 5]);
                                                    PointYear = Convert.ToString((Convert.ToInt16(PointYear) - 100) + 2000);
                                                    PointMonth += "/" + PointDate + "/" + PointYear + " " + PointHour + ":" + PointMinute + ":" + PointSecond;


                                                    DateFound = true;
                                                }
                                                newCtr--;
                                            } while (DateFound == false);//Extracting Equipment Name
                                        }
                                        catch (Exception ex1)
                                        {
                                            ErrorLog_Class.ErrorLogEntry(ex1);
                                            //ErrorLogFile(ex1);
                                        }

                                        DateFound = false;

                                    }
                                    CtrToStart++;
                                } while (ResFound == false);
                                break;
                            }
                            CtrToStart++;
                        } while (true);
                        if (AckGetBt)
                        {
                            sFactoryName = "RouteFrmInstrmnt";
                            if (PreviousFacName != sFactoryName)
                            {
                                string name = sFactoryName.TrimEnd(new char[] { ' ' });
                                name = name.TrimStart(new char[] { ' ' });
                                string desc = "Factory";
                              //  InsertItemsInDataBase("Factory", null, name + "|" + desc);
                                PreviousFacName = sFactoryName;
                            }
                            if (PreviousEqupname != sResourceName)
                            {
                                string name = sResourceName.TrimEnd(new char[] { ' ' });
                                name = name.TrimStart(new char[] { ' ' });
                                string desc = "Resource";
                                //InsertItemsInDataBase("Resource", "1P", name + "|" + desc);
                            }
                            if (PreviousComname != sElementName)
                            {
                                string name = sElementName.TrimEnd(new char[] { ' ' });
                                name = name.TrimStart(new char[] { ' ' });
                                string desc = "Element";
                               // InsertItemsInDataBase("Element", sResourceID, name + "|" + desc);
                            }
                            if (PreviousSubCompname != sSubElementName)
                            {
                                string name = sSubElementName.TrimEnd(new char[] { ' ' });
                                name = name.TrimStart(new char[] { ' ' });
                                string desc = "SubElement";
                               // InsertItemsInDataBase("SubElement", sElementID, name + "|" + desc);
                            }
                            if (dualChnl == offptctr)
                            {
                                arrchannel++;
                            }
                            int BdualChannel = 0;

                            //Amit Jain VA_10   Channel data not correct for Generate from file 4-3-2010
                            if (dualChnl == 1)
                            {
                                SamePointDifferentChannel = true;
                                if (dataEntered)
                                {
                                    BdualChannel = 1;
                                }
                                else
                                {

                                    BdualChannel = 0;
                                }
                            }
                            else
                            {
                                SamePointDifferentChannel = false;
                                dataEntered = false;
                                BdualChannel = 0;
                                NewID = null;
                            }
                            //end
                            offptctr = dualChnl;
                            {
                                if (!SamePointDifferentChannel)// || bAlreadyEntered)
                                {
                                    if (NewID == null)
                                    {
                                        string name = sPointName.TrimEnd(new char[] { ' ' });
                                        name = name.TrimStart(new char[] { ' ' });
                                        string desc = sPointDescription.TrimEnd(new char[] { ' ' });
                                        desc = desc.TrimStart(new char[] { ' ' });
                                        NewID = name + trendValCtr.ToString();
                                       // InsertItemsInDataBase("Point", sSubElementID, name + "|" + desc);
                                    }
                                }
                            }
                            if (dataEntered)
                            {
                                bAlreadyEntered = true;
                            }
                            else
                            {
                                bAlreadyEntered = false;
                            }
                            if (NewID != null)
                            {
                                //SetPointParameters(NewID, iUnit, iFScale, iCpl, idetc, iWin, iFltrType, iFltrVal, iMesure, iLor, iFreq, dualChnl);
                                double OriginalFac = 0.0;
                                double LimitVal = 0.0;
                                int startZero = 0;
                                double[] DataFinal = new double[1];
                                double[] dd = null;
                                double[] Xvalues = null;
                                byte[] Data = new byte[1];
                                int dtCtr = 0;
                                int dtfCtr = 0;
                                bool tobreak = false;
                                bool NewArray = true;
                                {
                                    string[] Parameters = { iUnit.ToString(), iMesure.ToString(), iLor.ToString(), "0", iFreq.ToString(), idetc.ToString() };//PointInformationForDwnLoad2(sFactoryName, sResourceName, sElementName, sSubElementName, sPointName).Split(new string[] { "," }, StringSplitOptions.None);//Extracting Parameters Of the Point For Finding There Keys To Decode Data
                                    ISVolt = false;
                                    IsInspection = false;
                                    ISUm = false;
                                    if (Convert.ToInt16(Parameters[0]) == 12)
                                    {
                                        ISVolt = true;
                                    }
                                    if (Convert.ToInt16(Parameters[0]) > 13)
                                    {
                                        IsInspection = true;
                                    }

                                    if (Convert.ToInt16(Parameters[0]) == 4 || Convert.ToInt16(Parameters[0]) == 8 || Convert.ToInt16(Parameters[0]) == 10)
                                    {
                                        ISUm = true;
                                    }


                                    if (PreviousFacName == sFactoryName && PreviousEqupname == sResourceName && PreviousComname == sElementName && PreviousSubCompname == sSubElementName && PreviousPointName == sPointName)
                                    {
                                        SamePointDifferentChannel = true;
                                    }
                                    else
                                        SamePointDifferentChannel = false;

                                    PreviousFacName = sFactoryName;
                                    PreviousEqupname = sResourceName;
                                    PreviousComname = sElementName;
                                    PreviousSubCompname = sSubElementName;
                                    PreviousPointName = sPointName;
                                    if (Convert.ToInt16(Parameters[0]) == 2 || Convert.ToInt16(Parameters[0]) == 6)
                                    {
                                        key = .0000763;//Setting The Key Value For Decoding
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 1 || Convert.ToInt16(Parameters[0]) == 5)
                                    {
                                        key = .00000305;//Setting The Key Value For Decoding
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 3 || Convert.ToInt16(Parameters[0]) == 7 || Convert.ToInt16(Parameters[0]) == 9 || Convert.ToInt16(Parameters[0]) == 12)
                                    {
                                        key = .0000610;//Setting the key Value For Decoding
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 4 || Convert.ToInt16(Parameters[0]) == 8 || Convert.ToInt16(Parameters[0]) == 10)
                                    {
                                        key = .001533;//Setting the Key Value For Decoding
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 0 || Convert.ToInt16(Parameters[0]) == 11 || Convert.ToInt16(Parameters[0]) > 12)
                                    {
                                        key = .00000305;//Setting The Key Value For Decoding
                                    }

                                    int KeyToSerch = CtrToStart;

                                    bool OrbtPt = false;
                                    for (int ik = 0; ik < 90; ik++)
                                    {
                                        if (MainArr[KeyToSerch] == 0x58 && MainArr[KeyToSerch + 1] == 0x02 && MainArr[KeyToSerch + 2] == 0x06 && MainArr[KeyToSerch + 3] == 0x01 || KeyToSerch == MainArr.Length - 3)
                                        {
                                            OrbtPt = true;
                                            break;
                                        }
                                        KeyToSerch++;

                                    }
                                    if (!OrbtPt)
                                    {

                                        KeyFactor = CtrToStart + 82;
                                    }

                                    if (Convert.ToInt16(Parameters[0]) == 2 || Convert.ToInt16(Parameters[0]) == 6 || Convert.ToInt16(Parameters[0]) == 1 || Convert.ToInt16(Parameters[0]) == 5)
                                    {
                                        if (Factor <= 7)
                                            OriginalFac = FactorUMMS[Factor];//Setting the Factor Value
                                        else
                                            OriginalFac = 1;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 3 || Convert.ToInt16(Parameters[0]) == 7 || Convert.ToInt16(Parameters[0]) == 9 || Convert.ToInt16(Parameters[0]) == 4 || Convert.ToInt16(Parameters[0]) == 8 || Convert.ToInt16(Parameters[0]) == 10)
                                    {
                                        if (Factor <= 5)
                                            OriginalFac = FactorUMIL[Factor];//Setting the factor value
                                        else
                                            OriginalFac = 1;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 0 || Convert.ToInt16(Parameters[0]) == 11)
                                    {

                                        if (Factor <= 7 && Factor != 0)
                                        {
                                            Factor = Factor - 2;
                                            if (Factor >= 0)
                                            {
                                                OriginalFac = FactorUMMS[Factor];//Setting The Factor Value
                                            }
                                            else
                                                OriginalFac = 1;
                                        }
                                        else
                                            OriginalFac = 1;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 12)
                                    {
                                        if (Factor <= 9 && Factor != 0)
                                        {
                                            Factor = Factor - 2;
                                            if (Factor >= 0)
                                            {
                                                OriginalFac = FactorUMMS[Factor];//Setting The Factor Value
                                            }
                                            else
                                            {
                                                Factor = 0;
                                                OriginalFac = 1;
                                            }
                                        }
                                        else
                                            OriginalFac = 1;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 13)
                                    {
                                        if (Factor <= 5)
                                        {

                                            if (Factor >= 0)
                                            {
                                                OriginalFac = FactorUMMS[Factor] * FactorInspection[Factor];//Setting The Factor Value
                                                if (Factor == 2)
                                                {
                                                    OriginalFac = OriginalFac * 10;
                                                }
                                                else if (Factor == 3)
                                                {
                                                    OriginalFac = 50000;
                                                }
                                                else if (Factor > 3 && Factor < 6)
                                                {
                                                    OriginalFac = OriginalFac * 100;
                                                }

                                            }
                                            else
                                            {
                                                Factor = 0;
                                                OriginalFac = 1;
                                            }
                                        }
                                        else if (Factor > 5)
                                        {
                                            OriginalFac = 1000;
                                        }
                                        else
                                            OriginalFac = 1;

                                        OriginalFac = OriginalFac * .9999;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) > 13)
                                    {
                                        if (Factor <= 5)
                                        {

                                            if (Factor >= 0)
                                            {
                                                OriginalFac = FactorUMMS[Factor] * FactorInspection[Factor];//Setting The Factor Value
                                                if (Factor == 3)
                                                {
                                                    OriginalFac = 1000;
                                                }
                                            }
                                            else
                                            {
                                                Factor = 0;
                                                OriginalFac = 1;
                                            }
                                        }
                                        else
                                            OriginalFac = 1;
                                    }

                                    if (Convert.ToInt16(Parameters[0]) == 2 || Convert.ToInt16(Parameters[0]) == 6)
                                    {
                                        if (Factor <= 7 && Factor >= 0)
                                            LimitVal = LimitMMS[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = 2.5;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 1 || Convert.ToInt16(Parameters[0]) == 5)
                                    {
                                        if (Factor <= 7 && Factor >= 0)
                                            LimitVal = LimitIPS[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = .1;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 3 || Convert.ToInt16(Parameters[0]) == 7 || Convert.ToInt16(Parameters[0]) == 9)
                                    {
                                        if (Factor <= 5 && Factor >= 0)
                                            LimitVal = LimitMIL[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = 2;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 4 || Convert.ToInt16(Parameters[0]) == 8 || Convert.ToInt16(Parameters[0]) == 10)
                                    {
                                        if (Factor <= 5 && Factor >= 0)
                                            LimitVal = LimitUM[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = 50;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 0 || Convert.ToInt16(Parameters[0]) == 11)
                                    {
                                        if (Factor <= 9 && Factor >= 0)
                                            LimitVal = LimitG[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = .1;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 12)
                                    {
                                        if (Factor <= 9 && Factor >= 0)
                                            LimitVal = LimitVolt[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = .01;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) == 13)
                                    {
                                        if (Factor <= 9 && Factor >= 0)
                                            LimitVal = LimitRPM[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = 5;
                                    }
                                    else if (Convert.ToInt16(Parameters[0]) > 13)
                                    {
                                        if (Factor <= 8 && Factor >= 0)
                                            LimitVal = LimitInspection[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = .001;
                                    }

                                    else if (Convert.ToInt16(Parameters[0]) > 13)
                                    {
                                        if (Factor <= 8 && Factor >= 0)
                                            LimitVal = LimitInspection[Factor];//Setting The Limit Factor Value
                                        else
                                            LimitVal = .001;
                                    }


                                    do//Extracting Values Of Data Sets
                                    {

                                        if (KeyFactor != MainArr.Length - 3)
                                        {
                                            {
                                                if (KeyFactor > MainArr.Length)
                                                {
                                                    tobreak = true;
                                                    NewArray = false;
                                                }
                                            }
                                        }
                                        if (NewArray == true)
                                        {
                                            if (KeyFactor >= MainArr.Length)
                                            {
                                                tobreak = true;
                                            }
                                            else if (KeyFactor <= MainArr.Length - 3)
                                            {

                                                if (MainArr[KeyFactor] == 0x58 && MainArr[KeyFactor + 1] == 0x02 && MainArr[KeyFactor + 2] == 0x06 && MainArr[KeyFactor + 3] == 0x01)
                                                    tobreak = true;
                                            }
                                            if (tobreak == false)
                                            {
                                                Data[dtCtr] = MainArr[KeyFactor];

                                            }
                                            dtCtr++;
                                            KeyFactor++;
                                            //Array.Resize(ref Data, Data.Length + 1);
                                            _ResizeArray.IncreaseArrayByte(ref Data, 1);

                                        }

                                    } while (tobreak == false);

                                    dtCtr = 0;
                                    for (int d = 0; d < startZero; d++)
                                    {
                                        DataFinal[dtfCtr] = 0.0;
                                        //Array.Resize(ref DataFinal, DataFinal.Length + 1);
                                        _ResizeArray.IncreaseArrayDouble(ref DataFinal,  1);
                                        dtfCtr++;
                                    }

                                    {
                                        if (Convert.ToInt16(Parameters[3]) < 3)//FillDataIntoDatabase
                                        {
                                            if (Convert.ToInt16(Parameters[1]) == 2 || Convert.ToInt16(Parameters[1]) == 3)
                                            {
                                                key = key * OriginalFac;//Calculating Key For Decoding

                                                dd = CalculateData(DataFinal, Data, CalculatedFullScale);

                                                Xvalues = new double[dd.Length];

                                                double Difference = Convert.ToDouble(1 / (FreqArray[Convert.ToInt16(Parameters[4])] * 2.56));
                                                Difference = Math.Round(Difference, 6);
                                                for (int values = 0; values < dd.Length; values++)
                                                {
                                                    Xvalues[values] = Convert.ToDouble(Difference * (values));
                                                }
                                                if (overalFound == true)
                                                {
                                                    if (ISVolt)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) / 196.4), 3);
                                                    }
                                                    else if (IsInspection)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) * 1.0007), 3);
                                                    }
                                                    else
                                                    {
                                                        OverallValueDecoded = Math.Round((OverallValueDecoded * key), 3);
                                                    }
                                                }

                                                FillDataIntoDatabaseUsb(NewID, Xvalues, dd, Convert.ToBoolean(BdualChannel), PointMonth, Parameters[5]);
                                                dataEntered = !dataEntered;//MainForm.CreateGraph(Xvalues, dd, Convert.ToString(DateTime.Now), dd.Length);
                                            }
                                            else if (Convert.ToInt16(Parameters[1]) == 0 || Convert.ToInt16(Parameters[1]) == 1)
                                            {
                                                if (Convert.ToInt16(Parameters[1]) == 1)
                                                    PhaseExtraction = true;
                                                else
                                                    PhaseExtraction = false;
                                                key = key * OriginalFac;//Calculating Key for Decoding

                                                dd = CalculateData(DataFinal, Data, CalculatedFullScale);


                                                int actLength = (dd.Length / 100) * 100;
                                                Xvalues = new double[actLength + 1];
                                                double Difference = Convert.ToDouble(FreqArray[Convert.ToInt16(Parameters[4])] / ResolutionFFT[Convert.ToInt16(Parameters[2])]);
                                                for (int values = 0; values < actLength + 1; values++)
                                                {
                                                    Xvalues[values] = Convert.ToDouble(Difference * (values));
                                                }
                                                if (overalFound == true)
                                                {
                                                    if (ISVolt)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) / 196.4), 3);
                                                    }
                                                    else if (IsInspection)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) * 1.0007), 3);
                                                    }
                                                    else
                                                    {
                                                        OverallValueDecoded = Math.Round((OverallValueDecoded * key), 3);
                                                    }
                                                }

                                                FillDataIntoDatabaseUsb(NewID, Xvalues, dd, Convert.ToBoolean(BdualChannel), PointMonth, Parameters[5]);
                                                dataEntered = !dataEntered;//MainForm.CreateGraph(Xvalues, dd, Convert.ToString(DateTime.Now), dd.Length);

                                            }
                                            else
                                            {
                                                key = key * OriginalFac;//Calculating Key For Decoding

                                                dd = CalculateData(DataFinal, Data, CalculatedFullScale);

                                                Xvalues = new double[dd.Length];

                                                double Difference = Convert.ToDouble(1 / (FreqArray[Convert.ToInt16(Parameters[4])] * 2.56));
                                                Difference = Math.Round(Difference, 6);
                                                for (int values = 0; values < dd.Length; values++)
                                                {
                                                    Xvalues[values] = Convert.ToDouble(Difference * (values));
                                                }
                                                if (overalFound == true)
                                                {
                                                    if (ISVolt)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) / 196.4), 3);
                                                    }
                                                    else if (IsInspection)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) * 1.0007), 3);
                                                    }
                                                    else
                                                    {
                                                        OverallValueDecoded = Math.Round((OverallValueDecoded * key), 3);
                                                    }


                                                }
                                                FillGapDataIntoDatabaseUsb(NewID, Xvalues, dd, Convert.ToBoolean(BdualChannel), PointMonth, Parameters[5]);

                                            }
                                        }
                                        else if (Convert.ToInt16(Parameters[3]) >= 3)
                                        {
                                            if (Convert.ToInt16(Parameters[1]) == 2)
                                            {
                                                key = key * OriginalFac;//Calculating Key For Decoding

                                                dd = CalculateData(DataFinal, Data, CalculatedFullScale);

                                                Xvalues = new double[dd.Length];
                                                double Difference = Convert.ToDouble(1 / (FreqArray[Convert.ToInt16(Parameters[4])] * 2.56));
                                                Difference = Math.Round(Difference, 6);
                                                for (int values = 0; values < dd.Length; values++)
                                                {
                                                    Xvalues[values] = Convert.ToDouble(Difference * (values));
                                                }
                                                if (overalFound == true)
                                                {
                                                    if (ISVolt)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) / 196.4), 3);
                                                    }
                                                    else if (IsInspection)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) * 1.0007), 3);
                                                    }
                                                    else
                                                    {
                                                        OverallValueDecoded = Math.Round((OverallValueDecoded * key), 3);
                                                    }
                                                }

                                                FillDataIntoDatabaseUsb(NewID, Xvalues, dd, false, PointMonth, Parameters[5]);
                                                dataEntered = !dataEntered;//MainForm.CreateGraph(Xvalues, dd, Convert.ToString(DateTime.Now), dd.Length);

                                            }
                                            else if (Convert.ToInt16(Parameters[1]) == 3)
                                            {

                                                key = key * OriginalFac;//Calculating Key For Decoding

                                                dd = CalculateData(DataFinal, Data, CalculatedFullScale);

                                                Xvalues = new double[dd.Length];
                                                double Difference = Convert.ToDouble(1 / (FreqArray[Convert.ToInt16(Parameters[4])] * 2.56));
                                                Difference = Math.Round(Difference, 6);

                                                for (int values = 0; values < dd.Length; values++)
                                                {
                                                    Xvalues[values] = Convert.ToDouble(Difference * (values));
                                                }
                                                if (overalFound == true)
                                                {
                                                    if (ISVolt)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) / 196.4), 3);
                                                    }
                                                    else if (IsInspection)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) * 1.0007), 3);
                                                    }
                                                    else
                                                    {
                                                        OverallValueDecoded = Math.Round((OverallValueDecoded * key), 3);
                                                    }
                                                }
                                                FillDataIntoDatabaseUsb(NewID, Xvalues, dd, false, PointMonth, Parameters[5]);
                                            }
                                            else if (Convert.ToInt16(Parameters[1]) == 0)
                                            {
                                                key = key * OriginalFac;//Calculating Key For Decoding
                                                dd = CalculateData(DataFinal, Data, CalculatedFullScale);

                                                Xvalues = new double[dd.Length];
                                                double Difference = Convert.ToDouble(FreqArray[Convert.ToInt16(Parameters[4])] / ResolutionFFT[Convert.ToInt16(Parameters[2])]);
                                                for (int values = 0; values < dd.Length; values++)
                                                {
                                                    Xvalues[values] = Convert.ToDouble(Difference * (values));
                                                }
                                                if (overalFound == true)
                                                {
                                                    if (ISVolt)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) / 196.4), 3);
                                                    }
                                                    else if (IsInspection)
                                                    {
                                                        OverallValueDecoded = Math.Round(((OverallValueDecoded * key) * 1.0007), 3);
                                                    }
                                                    else
                                                    {
                                                        OverallValueDecoded = Math.Round((OverallValueDecoded * key), 3);
                                                    }
                                                }

                                                FillDataIntoDatabaseUsb(NewID, Xvalues, dd, false, PointMonth, Parameters[5]);
                                            }
                                            else
                                            {
                                                dataEntered = false;
                                            }
                                        }


                                    }



                                }
                            }
                        }

                    } while (CtrToStart < MainArr.Length-3);
                }
                catch (Exception ex)
                {
                    ErrorLog_Class.ErrorLogEntry(ex);

                }
            }
        }

        private void FillGapDataIntoDatabaseUsb(string NewID, double[] Xvalues, double[] dd, bool p, string PointMonth, string p_6)
        {
            //throw new NotImplementedException();
        }
        
        private void FillDataIntoDatabaseUsb(string UID, double[] XValues, double[] YValues, bool SamePointDifferentChannel, string PointDate, string Detection)
        {
            
            try
            {
                {
                    //if (channel == 1)
                    //{
                    //    s = trendValCtr.ToString();
                    //    s = (ExectTime * (double)trendValCtr).ToString();
                    //}
                    //else if (channel == 2 && trendValCtr % 2 == 0)
                    //{
                    //    abc += 1;
                    //    s = abc.ToString() + " Ch-1";
                    //    s = (ExectTime * (double)trendValCtr).ToString() + " Ch-1";
                    //}
                    //else if (channel == 2 && trendValCtr % 2 != 0)
                    //{
                    //    s = abc.ToString() + " Ch-2";
                    //    s = (ExectTime * (double)(trendValCtr - 1)).ToString() + " Ch-2";
                    //}

                    if (!Directory.Exists("c:\\vvtemp\\"))
                    {
                        Directory.CreateDirectory("c:\\vvtemp\\");
                    }

                    aa = new FileStream("c:\\vvtemp\\" + UID + ".txt", FileMode.Create, FileAccess.ReadWrite);

                    sw = new StreamWriter(aa);
                    for (int i = 0; i < XValues.Length; i++)
                    {
                        sw.Write(XValues[i] + "/././" + YValues[i] + ".....");
                    }
                    sw.Close();


                    //node1 = trlPlantMangerComponents.AppendNode(new object[] { s + ".txt" }, parentNode);
                    //node1.Tag = "File";
                    //node1.StateImageIndex = 1;

                    int iCCtr = trendValCtr % 30;
                    dataGridView2.Rows.Add(1);
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = UID;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value = UID;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[iCCtr];
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[iCCtr].ToString();

                    trendValCtr++;
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        
        string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };

        //private string DeciamlToHexadeciaml1(int number)
        //{
        //    string[] hexvalues = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        //    string result = null, final = null;
        //    int rem = 0, div = 0;
        //    try
        //    {
        //        while (true)
        //        {
        //            rem = (number % 2);
        //            result += hexvalues[rem].ToString();

        //            if (number < 2)
        //                break;
        //            result += ',';
        //            number = (number / 2);

        //        }

        //        for (int i = 0; i <= (result.Length - 1); i++)
        //        {
        //            final += result[i];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return final;
        //}


        //private int HexadecimaltoDecimal(string hexadecimal)
        //{
        //    int result = 0;

        //    for (int i = 0; i < hexadecimal.Length; i++)
        //    {
        //        result += Convert.ToInt32(this.GetNumberFromNotation(hexadecimal[i]) * Math.Pow(16, Convert.ToInt32(hexadecimal.Length) - (i + 1)));
        //    }
        //    return Convert.ToInt32(result);
        //}
        //private int GetNumberFromNotation(char c)
        //{
        //    if (c == 'A')
        //        return 10;
        //    else if (c == 'B')
        //        return 11;
        //    else if (c == 'C')
        //        return 12;
        //    else if (c == 'D')
        //        return 13;
        //    else if (c == 'E')
        //        return 14;
        //    else if (c == 'F')
        //        return 15;

        //    return Convert.ToInt32(c.ToString());
        //}
        //private string DeciamlToHexadeciaml(int number)
        //{
        //    string[] hexvalues = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        //    string result = null, final = null;
        //    int rem = 0, div = 0;
        //    try
        //    {
        //        while (true)
        //        {
        //            rem = (number % 16);
        //            result += hexvalues[rem].ToString();

        //            if (number < 16)
        //                break;
        //            result += ',';
        //            number = (number / 16);

        //        }

        //        for (int i = (result.Length - 1); i >= 0; i--)
        //        {
        //            final += result[i];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // ErrorLogFile(ex);
        //    }
        //    return final;//Returning Final Hexadecimal Value 
        //}
       
      
        private double[] CalculateData(double[] DataFinal, byte[] Data, double iFScale)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "TempFiles"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "TempFiles");
                }

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "TempFiles\\TempD.dat"))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "TempFiles\\TempD.dat");
                }

                using (FileStream objStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "TempFiles\\TempD.dat", FileMode.Create, FileAccess.Write))
                {
                    objStream.Write(Data, 0, Data.Length);
                }


                double[] soundBytes = new double[0];
                string spath = AppDomain.CurrentDomain.BaseDirectory + "TempFiles\\TempD.dat";
                try
                {
                    using (FileStream wav = new FileStream(spath, FileMode.Open, FileAccess.Read))
                    {
                        int ctr = 0;
                        short sample;
                        double[] narray = new double[0];
                        BinaryReader fr = new BinaryReader(wav);

                        while (true)//while (fr.BaseStream.Position != fr.BaseStream.Length)
                        {
                            sample = fr.ReadInt16();
                            double SampleVal = Convert.ToDouble(sample);
                            //Array.Resize(ref soundBytes, soundBytes.Length + 1);
                            _ResizeArray.IncreaseArrayDouble(ref soundBytes, 1);
                            if (SampleVal < 100)
                            {
                                SampleVal = Math.Round(SampleVal, 2);
                                soundBytes[soundBytes.Length - 1] = (SampleVal);
                            }
                            else
                            {
                                SampleVal = Math.Round(SampleVal);
                                soundBytes[soundBytes.Length - 1] = (SampleVal);
                            }
                            ctr++;

                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog_Class.ErrorLogEntry(ex);
                }

                double MainCalculatedValue = 0;// (double)((iFScale * 10));

                {
                    MainCalculatedValue = iFScale;
                }
                DataFinal = new double[soundBytes.Length];
                for (int i = 0; i < soundBytes.Length; i++)
                {
                    DataFinal[i] = (soundBytes[i] * MainCalculatedValue) / 32767;
                }


            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return DataFinal;
        }


        #region Dat_Interface Members

        Form1 objForm = null;
        public Form1 _Form1
        {
            get
            {
                return objForm;
            }
            set
            {
                objForm = value;
            }
        }

        DataGridView dataGridView2 = null;
        public System.Windows.Forms.DataGridView _dataGridView2
        {
            get
            {
                return dataGridView2;
            }
            set
            {
                dataGridView2 = value;
            }
        }

        #endregion
    }
}
