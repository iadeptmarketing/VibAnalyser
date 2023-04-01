using System;
using System.Collections.Generic;

using System.Text;
using DI_Analyser.interfaces;
using System.IO;
using Analyser.Forms;
using Analyser.Classes;

namespace DI_Analyser.Classes
{
    class Bal_Control:Bal_interface
    {
        StreamReader srReader = null;
        FileStream fsFile = null;
        string _Detection = null;
        string _MeasType = null;
        double _Amplitude = 0;
        double _Angle = 0;

        #region Bal_interface Members
            
        public double Amplitude
        {
            get { return _Amplitude; }
        }

        public double angle
        {
            get { return _Angle; }
        }

        public string Detection
        {
            get
            {
                return _Detection;
            }
        }
        public string MeasType
        {
            get
            {
                return _MeasType;
            }
        }
        BalDataForm _BALForm1 = null;
        public BalDataForm _BALForm
        {
            get
            {
                return _BALForm1;
            }
            set
            {
                _BALForm1 = value;
            }
        }
        public void ReadBalFile(string Dest)
        {
            string data = null;
            _Detection = null;
            _Amplitude = 0;
            _Angle = 0;
            _MeasType = null;

            try
            {
                
                fsFile = new FileStream(Dest, FileMode.Open, FileAccess.Read);
                srReader = new StreamReader(fsFile);
                data = srReader.ReadToEnd();
                srReader.Close();
                string[] SplittedData = data.ToString().Replace(":", "").Split(new string[] { "\r\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                int ivib=0;
                int iwts=0;
                for (int i = 0; i < SplittedData.Length; i++)
                {
                    if (SplittedData[i].ToString().Contains("Run"))
                    {
                        string[] AmplitudeandAngle = SplittedData[i + 1].ToString().Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                        if (AmplitudeandAngle.Length > 1)
                        {
                            _BALForm._Vib.Rows.Add(1);
                            _Amplitude = Convert.ToDouble(AmplitudeandAngle[0].ToString());
                            _Angle = Convert.ToDouble(AmplitudeandAngle[1].ToString());
                            _BALForm._Vib.Rows[ivib].Cells[0].Value = SplittedData[i].ToString();
                            _BALForm._Vib.Rows[ivib].Cells[1].Value = _Amplitude.ToString();
                            _BALForm._Vib.Rows[ivib].Cells[2].Value = _Angle.ToString();
                            ivib++;
                        }
                    }
                    if (SplittedData[i].ToString().Contains("MeasmtType"))
                    {
                        string[] data1 = SplittedData[i + 1].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (data.Length > 1)
                        {
                            _MeasType = data1[1].ToString();
                        }
                        else
                        {
                            _MeasType = data1[0].ToString();
                        }
                    }
                    if (SplittedData[i].ToString().Contains("Detection"))
                    {
                        string[] data1 = SplittedData[i + 1].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (data.Length > 1)
                        {
                            _Detection = data1[1].ToString();
                        }
                        else
                        {
                            _Detection = data1[0].ToString();
                        }
                    }
                    if (SplittedData[i].ToString().Contains("Mass"))
                    {
                        string[] MassandAngle = SplittedData[i + 1].ToString().Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                        if (MassandAngle.Length > 1)
                        {
                            _BALForm._Wts.Rows.Add(1);
                            double Mass = Convert.ToDouble(MassandAngle[0].ToString());
                            double Ang = Convert.ToDouble(MassandAngle[1].ToString());
                            _BALForm._Wts.Rows[iwts].Cells[0].Value = SplittedData[i].ToString();
                            _BALForm._Wts.Rows[iwts].Cells[1].Value = Mass.ToString();
                            _BALForm._Wts.Rows[iwts].Cells[2].Value = Ang.ToString();
                            iwts++;
                        }
                    }
                    if (SplittedData[i].ToString().Contains("Radius"))
                    {
                        iwts--;
                        string Radius = SplittedData[i + 1].ToString();
                        _BALForm._Wts.Rows[iwts].Cells[3].Value = Radius;
                        iwts++;
                    }

                }

               
               

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        #endregion

        private void ReadTXTfile(string Dest, bool Exact)
        {
            //xarray = new double[0];
            //yarray = new double[0];
            //string data = null;
            //try
            //{
            //    string[] sarrpath = null;
            //    if (!Exact)
            //    {
            //        sarrpath = Dest.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            //        aa = new FileStream("c:\\vvtemp\\" + sarrpath[sarrpath.Length - 1], FileMode.Open, FileAccess.Read);
            //        DestbeforeTrend = "c:\\vvtemp\\" + sarrpath[sarrpath.Length - 1];
            //    }
            //    else
            //    {

            //        aa = new FileStream(Dest, FileMode.Open, FileAccess.Read);
            //        DestbeforeTrend = Dest;
            //    }
            //    sr = new StreamReader(aa);
            //    data = sr.ReadToEnd();
            //    sr.Close();
            //    string[] splitedData = data.Split(new string[] { "....." }, StringSplitOptions.RemoveEmptyEntries);
            //    xarray = new double[splitedData.Length];
            //    yarray = new double[splitedData.Length];
            //    for (int i = 0; i < splitedData.Length; i++)
            //    {
            //        string[] splittedXYData = splitedData[i].ToString().Split(new string[] { "/././" }, StringSplitOptions.RemoveEmptyEntries);
            //        xarray[i] = Convert.ToDouble(splittedXYData[0]);
            //        yarray[i] = Convert.ToDouble(splittedXYData[1]);
            //    }

            //}
            //catch (Exception ex)
            //{
            //}
        }

      


       
    }
}
