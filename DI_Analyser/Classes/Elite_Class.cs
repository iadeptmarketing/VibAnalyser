using System;
using System.Collections.Generic;
using System.Text;
using Analyser.interfaces;
using DI_Analyser;
using System.Windows.Forms;
using Analyser.Properties;
using System.IO;
//using ImpaqFileSystemLib;

namespace Analyser.Classes
{
    class Elite_Class //: Elite_Interface
    {

    //    int[] band_table = { 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 40000 };
    //    int[] line_table = { 100, 200, 400, 800, 1600, 3200, 6400, 12800 };
    //    double[] resolution_table = { 0.5, 0.25, 0.125, 0.0625 };
    //    double[] x = new double[0];
    //    double[] y = new double[0];
    //    string Xlabel = null;
    //    string Ylabel = null;
    //    int imgctr = 0;
    //    string[] ColorCode = { "7667712", "16751616", "4684277", "7077677", "16777077", "9868951", "2987746", "4343957", "16777216", "23296", "16711681", "8388652", "6972", "16776961", "7722014", "32944", "7667573", "7357301", "12042869", "60269", "14774017", "5103070", "14513374", "5374161", "38476", "3318692", "29696", "6737204", "16728065", "744352" };
    //    ImageList objlistimg = new ImageList();
    //    FileStream aa = null;
    //    StreamWriter sw = null;
    //    StreamReader sr = null;

    //    #region FD2_Interface Members

    //    Form1 objForm = null;
    //    public Form1 _Form1
    //    {
    //        get
    //        {
    //            return objForm;
    //        }
    //        set
    //        {
    //            objForm = value;
    //        }
    //    }

    //    DataGridView dataGridView2 = null;
    //    public DataGridView _dataGridView2
    //    {
    //        get
    //        {
    //            return dataGridView2;
    //        }
    //        set
    //        {
    //            dataGridView2 = value;
    //        }
    //    }

    //    string sLineofResolution = null;
    //    public string _LineofResolution
    //    {
    //        get
    //        {
    //            return sLineofResolution;
    //        }
    //        set
    //        {
    //            sLineofResolution = value;
    //        }
    //    }

    //    string sBandWidth = null;
    //    public string _BandWidth
    //    {
    //        get
    //        {
    //            return sBandWidth;
    //        }
    //        set
    //        {
    //            sBandWidth = value;
    //        }
    //    }

 

    //    string[] Xunit = null;
    //    string[] Yunit = null;
    //    public string[] _Xunit
    //    {
    //        get
    //        {
    //            return Xunit;
    //        }
    //        set
    //        {
    //            Xunit = value;
    //        }
    //    }
    //    public string[] _Yunit
    //    {
    //        get
    //        {
    //            return Yunit;
    //        }
    //        set
    //        {
    //            Yunit = value;
    //        }
    //    }
    //    ResizeArray_Interface _ResizeArray = new ResizeArray_Control();
    //    public void ReadFD2File(string FileName)
    //    {
    //        objlistimg.Images.Add(Resources.DarkRed);
    //        objlistimg.Images.Add(Resources.DarkGreen);
    //        objlistimg.Images.Add(Resources.DarkGoldenRod);
    //        objlistimg.Images.Add(Resources.DarkVoilet);
    //        objlistimg.Images.Add(Resources.DarkBlue);
    //        objlistimg.Images.Add(Resources.DimGrey);
    //        objlistimg.Images.Add(Resources.Chocolate);
    //        objlistimg.Images.Add(Resources.DarkKhaki);
    //        objlistimg.Images.Add(Resources.Black);
    //        objlistimg.Images.Add(Resources.Orange);
    //        objlistimg.Images.Add(Resources.Cyan);
    //        objlistimg.Images.Add(Resources.AquaMarine);
    //        objlistimg.Images.Add(Resources.Bisque);
    //        objlistimg.Images.Add(Resources.Blue);
    //        objlistimg.Images.Add(Resources.BlueViolet);
    //        objlistimg.Images.Add(Resources.Coral);
    //        objlistimg.Images.Add(Resources.Darkmagenta);
    //        objlistimg.Images.Add(Resources.DarkseaGreen);
    //        objlistimg.Images.Add(Resources.DarkSlateBlue);
    //        objlistimg.Images.Add(Resources.Deeppink);
    //        objlistimg.Images.Add(Resources.DodgerBlue);
    //        objlistimg.Images.Add(Resources.FireBrick);
    //        objlistimg.Images.Add(Resources.ForestGreen);
    //        objlistimg.Images.Add(Resources.GreenYellow);
    //        objlistimg.Images.Add(Resources.HotPink);
    //        objlistimg.Images.Add(Resources.IndianRed);
    //        objlistimg.Images.Add(Resources.Darkorange);
    //        objlistimg.Images.Add(Resources.Darkorchid);
    //        objlistimg.Images.Add(Resources.DeepSkyBlue);
    //        objlistimg.Images.Add(Resources.SandyBrown);

    //        if (Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.Delete("c:\\vvtemp\\", true);
    //        }
    //        Xlabel = null;
    //        Ylabel = null;

    //        _Xunit = new string[0];
    //        _Yunit = new string[0];
    //        imgctr = 0;
    //       // ImpaqFileSystemLib.FFTDataFileClass _classFFT = new ImpaqFileSystemLib.FFTDataFileClass();
    //        ImpaqFileSystemLib.FFTDataFileClass _classFFT = new ImpaqFileSystemLib.FFTDataFileClass();


    //        if (_classFFT.LoadFile(FileName))// if (_InterfaceFFT.LoadFile("C:\0011.fd2"))
    //        {
    //            int band = band_table[(int)(_classFFT.Setup.Band)];
    //            int Line = line_table[(int)(_classFFT.Setup.Line)];

    //            double samplingRate = (double)1 / (double)(band * 2.56);
    //            double deltaFreq = (double)band / (double)Line;



    //            _BandWidth = band.ToString();
    //            _LineofResolution = Line.ToString();

    //            for (int ch_number = 0; ch_number < 4; ch_number++)
    //            {
    //                if (_classFFT.Setup.get_ChEnabled((byte)ch_number))
    //                {
    //                    //Read Time waveform
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatBuffer time = _classFFT.Spectrum.get_Time((byte)ch_number);
    //                        if (time.Size > 0)
    //                        {
    //                            x = new double[time.Size];
    //                            y = new double[time.Size];
    //                            for (int i = 0; i < time.Size; i++)
    //                            {
    //                                x[i] = i * samplingRate;
    //                                y[i] = time[i];
    //                            }
    //                            Xlabel = "Sec";
    //                            Ylabel = _classFFT.Setup.get_ChSensorUnit((byte)ch_number);
    //                            Create_datapads(dataGridView2, "Time Wave CH", ch_number, x, y, Xlabel, Ylabel);
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }

    //                    //Read Complex Spectrum
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatComplexBuffer spectrum = _classFFT.Spectrum.get_ComplexSpectrum((byte)ch_number);

    //                        if (spectrum.Size > 0)
    //                        {
    //                            x = new double[spectrum.Size];
    //                            y = new double[spectrum.Size];
    //                            double[] y_real = new double[spectrum.Size];
    //                            double[] y_imag = new double[spectrum.Size];
    //                            double[] y_phase = new double[spectrum.Size];

    //                            for (int i = 0; i < spectrum.Size; i++)
    //                            {
    //                                x[i] = i * deltaFreq;
    //                                y[i] = Math.Pow((double)(Math.Pow((double)spectrum.get_Real(i), (double)2) + Math.Pow((double)spectrum.get_Imaginary(i), (double)2)), (double)0.5);
    //                                y_real[i] = (double)spectrum.get_Real(i);
    //                                y_imag[i] = (double)spectrum.get_Imaginary(i);
    //                                if (i > 0)
    //                                {
    //                                    y_phase[i] = calculatePhase(y_real[i], y_imag[i]);
    //                                }
    //                            }
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            //Draw FFT xy graph with labels//
    //                            Create_datapads(dataGridView2, "Spectrum CH", ch_number, x, y, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Spectrum_real CH", ch_number, x, y_real, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Spectrum_imag CH", ch_number, x, y_imag, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Spectrum_phase CH", ch_number, x, y_phase, Xlabel, "Deg");
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    //Read Envelope
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatBuffer Pspectrum = _classFFT.Spectrum.get_Envelop((byte)ch_number);
    //                        if (Pspectrum.Size > 0)
    //                        {
    //                            x = new double[Pspectrum.Size];
    //                            y = new double[Pspectrum.Size];
    //                            for (int i = 0; i < Pspectrum.Size; i++)
    //                            {
    //                                x[i] = i * deltaFreq;
    //                                y[i] = (double)Pspectrum[i];
    //                            }
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            Create_datapads(dataGridView2, "Envelope CH", ch_number, x, y, Xlabel, Ylabel);

    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    //Read Power Spectrum
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatBuffer Pspectrum = _classFFT.Spectrum.get_PowerSpectrum((byte)ch_number);
    //                        if (Pspectrum.Size > 0)
    //                        {
    //                            x = new double[Pspectrum.Size];
    //                            y = new double[Pspectrum.Size];
    //                            for (int i = 0; i < Pspectrum.Size; i++)
    //                            {
    //                                x[i] = i * deltaFreq;
    //                                y[i] = (double)Pspectrum[i];
    //                            }
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            Create_datapads(dataGridView2, "Power Spectrum CH", ch_number, x, y, Xlabel, Ylabel);

    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    //Read Spectrum Coherence
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatBuffer coherence = _classFFT.Spectrum.get_Coherence((byte)ch_number);
    //                        if (coherence.Size > 0)
    //                        {
    //                            x = new double[coherence.Size];
    //                            y = new double[coherence.Size];

    //                            for (int i = 0; i < coherence.Size; i++)
    //                            {
    //                                x[i] = i * deltaFreq;
    //                                y[i] = (double)coherence[i];
    //                            }
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            Create_datapads(dataGridView2, "Coherence CH", ch_number, x, y, Xlabel, Ylabel);
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    //Read Spectrum FRF
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatComplexBuffer frf = _classFFT.Spectrum.get_FRF((byte)ch_number);
    //                        if (frf.Size > 0)
    //                        {
    //                            x = new double[frf.Size];
    //                            y = new double[frf.Size];
    //                            double[] y_real = new double[frf.Size];
    //                            double[] y_imag = new double[frf.Size];
    //                            double[] y_phase = new double[frf.Size];
    //                            for (int i = 0; i < frf.Size; i++)
    //                            {
    //                                x[i] = i * deltaFreq;
    //                                y[i] = Math.Pow((double)(Math.Pow((double)frf.get_Real(i), (double)2) + Math.Pow((double)frf.get_Imaginary(i), (double)2)), (double)0.5);
    //                                y_real[i] = (double)frf.get_Real(i);
    //                                y_imag[i] = (double)frf.get_Imaginary(i);
    //                                if (i > 0)
    //                                {
    //                                    y_phase[i] = calculatePhase(y_real[i], y_imag[i]);
    //                                }
    //                            }
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            Create_datapads(dataGridView2, "FRF CH", ch_number, x, y, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "FRF_real CH", ch_number, x, y_real, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "FRF_imag CH", ch_number, x, y_imag, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "FRF_phase CH", ch_number, x, y_phase, Xlabel, "Deg");
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    //Read Cross Spectrum 
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.IFloatComplexBuffer cross = _classFFT.Spectrum.get_CrossSpectrum((byte)ch_number);
    //                        if (cross.Size > 0)
    //                        {
    //                            x = new double[cross.Size];
    //                            y = new double[cross.Size];
    //                            double[] y_real = new double[cross.Size];
    //                            double[] y_imag = new double[cross.Size];
    //                            double[] y_phase = new double[cross.Size];
    //                            for (int i = 0; i < cross.Size; i++)
    //                            {
    //                                x[i] = i * deltaFreq;
    //                                y[i] = Math.Pow((double)(Math.Pow((double)cross.get_Real(i), (double)2) + Math.Pow((double)cross.get_Imaginary(i), (double)2)), (double)0.5);
    //                                y_real[i] = (double)cross.get_Real(i);
    //                                y_imag[i] = (double)cross.get_Imaginary(i);
    //                                if (i > 0)
    //                                {
    //                                    y_phase[i] = calculatePhase(y_real[i], y_imag[i]);
    //                                }
    //                            }
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            Create_datapads(dataGridView2, "Cross Spectrum CH", ch_number, x, y, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Cross Spectrum_real CH", ch_number, x, y_real, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Cross Spectrum_imag CH", ch_number, x, y_imag, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Cross Spectrum_phase CH", ch_number, x, y_phase, Xlabel, "Deg");
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    //Read waterfall 
    //                    try
    //                    {
    //                        ImpaqFileSystemLib.ICollection coll = _classFFT.Waterfall.Spectrum;
                          
    //                        if (coll.Count > 0)
    //                        {
    //                            Xlabel = "Hz";
    //                            Ylabel = _classFFT.Setup.get_ChDisplayUnit((byte)ch_number);
    //                            for (int i1 = 0; i1 < coll.Count; i1++)
    //                            {
    //                                ImpaqFileSystemLib.FFTSpectrumData realdata = (ImpaqFileSystemLib.FFTSpectrumData)_classFFT.Waterfall.Spectrum[i1];
    //                                ImpaqFileSystemLib.IFloatBuffer spectrum = realdata.get_PowerSpectrum((byte)ch_number);
    //                                if (spectrum.Size > 0)
    //                                {
    //                                    x = new double[spectrum.Size];
    //                                    y = new double[spectrum.Size];
    //                                    for (int i = 0; i < spectrum.Size; i++)
    //                                    {
    //                                        x[i] = i * deltaFreq;
    //                                        y[i] = spectrum[i];
    //                                    }
    //                                    Create_datapads(dataGridView2, "Waterfall data " + (i1 + 1).ToString() + " CH", ch_number, x, y, Xlabel, Ylabel);
    //                                }
    //                            }
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            MessageBox.Show("Not accessible");
    //        }
    //    }

    //    public void ReadOTDfile(string FileName)
    //    {
    //        objlistimg.Images.Add(Resources.DarkRed);
    //        objlistimg.Images.Add(Resources.DarkGreen);
    //        objlistimg.Images.Add(Resources.DarkGoldenRod);
    //        objlistimg.Images.Add(Resources.DarkVoilet);
    //        objlistimg.Images.Add(Resources.DarkBlue);
    //        objlistimg.Images.Add(Resources.DimGrey);
    //        objlistimg.Images.Add(Resources.Chocolate);
    //        objlistimg.Images.Add(Resources.DarkKhaki);
    //        objlistimg.Images.Add(Resources.Black);
    //        objlistimg.Images.Add(Resources.Orange);
    //        objlistimg.Images.Add(Resources.Cyan);
    //        objlistimg.Images.Add(Resources.AquaMarine);
    //        objlistimg.Images.Add(Resources.Bisque);
    //        objlistimg.Images.Add(Resources.Blue);
    //        objlistimg.Images.Add(Resources.BlueViolet);
    //        objlistimg.Images.Add(Resources.Coral);
    //        objlistimg.Images.Add(Resources.Darkmagenta);
    //        objlistimg.Images.Add(Resources.DarkseaGreen);
    //        objlistimg.Images.Add(Resources.DarkSlateBlue);
    //        objlistimg.Images.Add(Resources.Deeppink);
    //        objlistimg.Images.Add(Resources.DodgerBlue);
    //        objlistimg.Images.Add(Resources.FireBrick);
    //        objlistimg.Images.Add(Resources.ForestGreen);
    //        objlistimg.Images.Add(Resources.GreenYellow);
    //        objlistimg.Images.Add(Resources.HotPink);
    //        objlistimg.Images.Add(Resources.IndianRed);
    //        objlistimg.Images.Add(Resources.Darkorange);
    //        objlistimg.Images.Add(Resources.Darkorchid);
    //        objlistimg.Images.Add(Resources.DeepSkyBlue);
    //        objlistimg.Images.Add(Resources.SandyBrown);

    //        if (Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.Delete("c:\\vvtemp\\", true);
    //        }
    //        _Xunit = new string[0];
    //        _Yunit = new string[0];





           






    //          ImpaqFileSystemLib.OrderTrackingDataFileClass _classOrbit = new ImpaqFileSystemLib.OrderTrackingDataFileClass();
    //          ImpaqFileSystemLib.OrderTrackingSpectrumData order_Spectrum = new ImpaqFileSystemLib.OrderTrackingSpectrumData();


    //          OrderTrackingOrbitData orbit_Data = _classOrbit.Orbit;
    //          //for (byte orbitIndex = 0; orbitIndex <= 1; orbitIndex++)
    //          //{
    //          //    byte bCh1 = Convert.ToByte(2) * orbitIndex;
    //          //    byte bCh2 = bCh1 + Convert.ToByte(1);
    //          //    if (setup.ChEnabled(bCh1) & setup.ChEnabled(bCh2))
    //          //    {
    //          //        OrderTrackingTimewaveData waveData = (OrderTrackingTimewaveData)orbit_Data.Timewave;
    //          //        OrderTrackingSpectrumData spectrumData = (OrderTrackingSpectrumData)orbit_Data.Spectrum;
    //          //        double deltaTime = waveData.DeltaTime;
    //          //        IFloatBuffer waveform1 = waveData.Data(bCh1);
    //          //        IFloatBuffer waveform2 = waveData.Data(bCh2);
    //          //        sMessage.AppendLine(new string('=', 40));
    //          //        sMessage.AppendLine("Ch " + (bCh1 + 1).ToString + "& " + (bCh2 + 1).ToString + " Orbit Waveform");
    //          //        sMessage.AppendLine("y1 DC = " + waveData.DC(bCh1).ToString);
    //          //        sMessage.AppendLine("y2 DC = " + waveData.DC(bCh2).ToString);
    //          //        sMessage.AppendLine("x, y1(" + waveData.Setup.ChUnit(bCh1) + "), y2(" + waveData.Setup.ChUnit(bCh2) + ")");
    //          //        sMessage.AppendLine(new string('=', 40));
    //          //        for (int i = 0; i <= waveform1.Size - 1; i++)
    //          //        {
    //          //            x = i * deltaTime;
    //          //            sMessage.AppendLine(x.ToString("0.000000") + ", " + waveform1(i).ToString("0.000000") + ", " + waveform2(i).ToString("0.000000"));
    //          //        }
    //          //        sMessage.AppendLine("");

    //          //IFloatComplexBuffer spectrum1 = spectrumData.Data(bCh1);
    //          //IFloatComplexBuffer spectrum2 = spectrumData.Data(bCh2);
    //          //sMessage.AppendLine(new string('=', 40));
    //          //sMessage.AppendLine("Ch " + (bCh1 + 1).ToString + "&" + (bCh1 + 1).ToString + " Orbit Spectrum");
    //          ////sMessage.AppendLine("DeltaTime = " & wave.DeltaTime.ToString)
    //          //string ch1Unit = waveData.Setup.ChUnit(bCh1) + _detection_table(order_Trace.Setup.Detection);
    //          //string ch2Unit = waveData.Setup.ChUnit(bCh2) + _detection_table(order_Trace.Setup.Detection);
    //          //sMessage.AppendLine("x(Order), y1_re(" + ch1Unit + "), y1_im(" + ch1Unit + "), y2_re(" + ch2Unit + "), y2_im(" + ch2Unit + ")");
    //          //sMessage.AppendLine(new string('=', 40));
    //          //for (int i = 0; i <= spectrum1.Size - 1; i++)
    //          //{
    //          //    x = i * Resolution;
    //          //    sMessage.AppendLine(x.ToString("0.000000") + ", " + spectrum1.Real(i).ToString("0.000000") + ", " + spectrum1.Imaginary(i).ToString("0.000000") + ", " + spectrum2.Real(i).ToString("0.000000") + ", " + spectrum2.Imaginary(i).ToString("0.000000"));
    //          //}
    //          //sMessage.AppendLine("");

    //          //    }
    //          //}

    //          ////Orbit Waveform of records
    //          //ImpaqFileSystemLib.ICollection orbit_WaveMap = otdFile.TimewaveMap.Timewave;
    //          //if (orbit_WaveMap.Count > 0)
    //          //{
    //          //    sMessage.AppendLine(new string('=', 40));
    //          //    sMessage.AppendLine("Orbit Waveform");
    //          //    for (int I = 0; I <= orbit_WaveMap.Count - 1; I++)
    //          //    {
    //          //        OrderTrackingTimewaveData wave = (OrderTrackingTimewaveData)orbit_WaveMap.Item(I);
    //          //        for (int orbitIndex = 0; orbitIndex <= 1; orbitIndex++)
    //          //        {
    //          //            byte bCh1 = Convert.ToByte(orbitIndex * 2);
    //          //            byte bCh2 = Convert.ToByte(orbitIndex * 2 + 1);
    //          //            if (setup.ChEnabled(bCh1) & setup.ChEnabled(bCh2))
    //          //            {
    //          //                double deltaTime = wave.DeltaTime;
    //          //                IFloatBuffer waveform1 = wave.Data(bCh1);
    //          //                IFloatBuffer waveform2 = wave.Data(bCh2);
    //          //                string ch1Unit = setup.ChUnit(bCh1);
    //          //                string ch2Unit = setup.ChUnit(bCh2);
    //          //                sMessage.AppendLine("Waveform #" + (I + 1).ToString + "/" + orbit_WaveMap.Count.ToString);
    //          //                sMessage.AppendLine("Ch " + (bCh1 + 1).ToString + "&" + (bCh2 + 1).ToString + " Orbit Waveform");
    //          //                sMessage.AppendLine("y1 DC = " + wave.DC(bCh1).ToString);
    //          //                sMessage.AppendLine("y2 DC = " + wave.DC(bCh2).ToString);
    //          //                sMessage.AppendLine("x, y1(" + ch1Unit + "), y2(" + ch2Unit + ")");
    //          //                sMessage.AppendLine(new string('=', 40));
    //          //                for (int J = 0; J <= waveform1.Size - 1; J++)
    //          //                {
    //          //                    x = J * deltaTime;
    //          //                    sMessage.AppendLine(x.ToString("0.000000") + ", " + waveform1(J).ToString("0.000000") + ", " + waveform2(J).ToString("0.000000"));
    //          //                }
    //          //                sMessage.AppendLine("");
    //          //            }
    //          //        }
    //          //    }
    //          //}

    //          ////=======================================================
    //          ////Service provided by Telerik (www.telerik.com)
    //          ////Conversion powered by NRefactory.
    //          ////Twitter: @telerik
    //          ////Facebook: facebook.com/telerik
    //          ////=======================================================


    //          if (_classOrbit.LoadFile(FileName))// if (_InterfaceFFT.LoadFile("C:\0011.fd2"))
    //          {
    //              order_Spectrum = _classOrbit.Spectrum;
    //              for (int orbitIndex = 0; orbitIndex <= 1; orbitIndex++)
    //              {
    //                  int ch1 = 2 * orbitIndex;
    //                  int ch2 = ch1 + 1;

    //                  float speedRPMs = _classOrbit.Orbit.Timewave.RPM;
                     
    //                  if (_classOrbit.Setup.get_ChEnabled((byte)ch1) && _classOrbit.Setup.get_ChEnabled((byte)ch2))
    //                  {



    //                      //OrderTrackingTimewaveData waveData = (OrderTrackingTimewaveData)orbit_Data.Timewave;
    //                      //OrderTrackingSpectrumData spectrumData = (OrderTrackingSpectrumData)orbit_Data.Spectrum;

    //                      //IFloatComplexBuffer spectrum1 = spectrumData.get_Data((byte)(ch1));
    //                      //IFloatComplexBuffer spectrum2 = spectrumData.get_Data((byte)(ch2));
    //                      ////sMessage.AppendLine(new string('=', 40));
    //                      ////sMessage.AppendLine("Ch " + (bCh1 + 1).ToString + "&" + (bCh1 + 1).ToString + " Orbit Spectrum");
    //                      ////sMessage.AppendLine("DeltaTime = " & wave.DeltaTime.ToString)
    //                      //string ch1Unit = waveData.Setup.ChUnit(bCh1) + _detection_table(order_Trace.Setup.Detection);
    //                      //string ch2Unit = waveData.Setup.ChUnit(bCh2) + _detection_table(order_Trace.Setup.Detection);
    //                      //sMessage.AppendLine("x(Order), y1_re(" + ch1Unit + "), y1_im(" + ch1Unit + "), y2_re(" + ch2Unit + "), y2_im(" + ch2Unit + ")");
    //                      //sMessage.AppendLine(new string('=', 40));
    //                      //for (int i = 0; i <= spectrum1.Size - 1; i++)
    //                      //{
    //                      //    x = i * Resolution;
    //                      //    sMessage.AppendLine(x.ToString("0.000000") + ", " + spectrum1.Real(i).ToString("0.000000") + ", " + spectrum1.Imaginary(i).ToString("0.000000") + ", " + spectrum2.Real(i).ToString("0.000000") + ", " + spectrum2.Imaginary(i).ToString("0.000000"));
    //                      //}
    //                      //sMessage.AppendLine("");




    //                      float deltaTime = _classOrbit.Orbit.Timewave.DeltaTime; //file.orbit.timewave.get('DeltaTime');
    //                      ImpaqFileSystemLib.IFloatBuffer waveform1 = _classOrbit.Orbit.Timewave.get_Data((byte)(ch1));
    //                      ImpaqFileSystemLib.IFloatBuffer waveform2 = _classOrbit.Orbit.Timewave.get_Data((byte)(ch2));
    //                      float DC1 = _classOrbit.Orbit.Timewave.get_DC((byte)ch1); //file.orbit.timewave.get('DC', ch1);
    //                      float DC2 = _classOrbit.Orbit.Timewave.get_DC((byte)ch2);//file.orbit.timewave.get('DC', ch2);
    //                      string waveUnit1 = _classOrbit.Orbit.Timewave.Setup.get_ChUnit((byte)ch1);//file.orbit.timewave.setup.get('ChUnit', ch1);
    //                      string waveUnit2 = _classOrbit.Orbit.Timewave.Setup.get_ChUnit((byte)ch2);//file.orbit.timewave.setup.get('ChUnit', ch2);
    //                      x = new double[waveform1.Size];
    //                      y = new double[waveform1.Size];
    //                      double[] y1 = new double[waveform2.Size];

    //                      for (int i = 0; i < x.Length; i++)
    //                      {
    //                          x[i] = (i + 1) * deltaTime;
    //                          y[i] = waveform1[i];
    //                          y1[i] = waveform2[i];
    //                      }

    //                      Xlabel = "Ch" + Convert.ToString(ch1 + 1);
    //                      Ylabel = "Ch" + Convert.ToString(ch2 + 1);
    //                      //Draw Time wave xy graph with labels//
    //                      Create_datapads(dataGridView2, "Orbit" + Xlabel + Ylabel, y, y1, Xlabel, Ylabel);


    //                      Xlabel = "Time (Sec)";
    //                      Ylabel = waveUnit1;
    //                      Create_datapads(dataGridView2, "Waveform Ch" + (ch1 + 1).ToString(), x, y, Xlabel, Ylabel);
    //                      Create_datapads(dataGridView2, "Waveform Ch" + (ch2 + 1).ToString(), x, y1, Xlabel, Ylabel);
    //                      //Create_datapads(dataGridView2, "Trace_imaginary" + (Odr_no + 1).ToString() + " CH ", ch_number, x, y, Xlabel, Ylabel);

    //                      // Xlabel = "RPM";
    //                      //Ylabel = _classOrbit.Setup.get_ChDisplayUnit((byte)orbitIndex);
    //                      //Create_datapads(dataGridView2, "Trace" + (orbitIndex + 1).ToString() + " CH ", orbitIndex, x, y, Xlabel, Ylabel);
    //                  }
    //              }
    //              //for (int orbitIndex = 0; orbitIndex <= 1; orbitIndex++)
    //              //{
    //              //    int ch1 = 2 * orbitIndex;
    //              //    int ch2 = ch1 + 1;

    //              //    float speedRPMs = _classOrbit.Orbit.Timewave.RPM;

    //              //    if (_classOrbit.Setup.get_ChEnabled((byte)ch1) && _classOrbit.Setup.get_ChEnabled((byte)ch2))
    //              //    {
    //              //        float deltaTime = _classOrbit.Orbit.Timewave.DeltaTime; //file.orbit.timewave.get('DeltaTime');
    //              //        ImpaqFileSystemLib.IFloatBuffer waveform1 = _classOrbit.Orbit.Timewave.get_Data((byte)(ch1));
    //              //        ImpaqFileSystemLib.IFloatBuffer waveform2 = _classOrbit.Orbit.Timewave.get_Data((byte)(ch2));
    //              //        float DC1 = _classOrbit.Orbit.Timewave.get_DC((byte)ch1); //file.orbit.timewave.get('DC', ch1);
    //              //        float DC2 = _classOrbit.Orbit.Timewave.get_DC((byte)ch2);//file.orbit.timewave.get('DC', ch2);
    //              //        string waveUnit1 = _classOrbit.Orbit.Timewave.Setup.get_ChUnit((byte)ch1);//file.orbit.timewave.setup.get('ChUnit', ch1);
    //              //        string waveUnit2 = _classOrbit.Orbit.Timewave.Setup.get_ChUnit((byte)ch2);//file.orbit.timewave.setup.get('ChUnit', ch2);
    //              //        x = new double[waveform1.Size];
    //              //        y = new double[waveform1.Size];
    //              //        double[] y1 = new double[waveform2.Size];

    //              //        for (int i = 0; i < x.Length; i++)
    //              //        {
    //              //            x[i] = (i + 1) * deltaTime;
    //              //            y[i] = waveform1[i];
    //              //            y1[i] = waveform2[i];
    //              //        }

    //              //        Xlabel = "Ch"+ Convert.ToString(ch1 + 1);
    //              //        Ylabel = "Ch" + Convert.ToString(ch2 + 1);
    //              //        //Draw Time wave xy graph with labels//
    //              //        Create_datapads(dataGridView2, "Orbit" + Xlabel+Ylabel ,  y, y1, Xlabel, Ylabel);


    //              //        Xlabel="Time (Sec)";
    //              //        Ylabel = waveUnit1;
    //              //        Create_datapads(dataGridView2, "Waveform Ch" + (ch1 + 1).ToString(),  x, y, Xlabel, Ylabel);
    //              //        Create_datapads(dataGridView2, "Waveform Ch" + (ch2 + 1).ToString(),  x, y1, Xlabel, Ylabel);
    //              //        //Create_datapads(dataGridView2, "Trace_imaginary" + (Odr_no + 1).ToString() + " CH ", ch_number, x, y, Xlabel, Ylabel);
                            
    //              //        // Xlabel = "RPM";
    //              //        //Ylabel = _classOrbit.Setup.get_ChDisplayUnit((byte)orbitIndex);
    //              //        //Create_datapads(dataGridView2, "Trace" + (orbitIndex + 1).ToString() + " CH ", orbitIndex, x, y, Xlabel, Ylabel);
    //              //    }
    //              //}
    //          }
    

    ////        x = (1:waveform1.size).*deltaTime;
    ////        size = waveform1.size;
    ////        y1 = zeros(1, size);
    ////        y2 = zeros(1, size);
    ////        for i = 0:size-1
    ////            y1(i+1) = waveform1.Item(i);
    ////            y2(i+1) = waveform2.Item(i);
    ////        end
    ////        figure(21 + orbitIndex);
    ////        subplot(211)
    ////        plot(x, y1, x, y2);
    ////        grid on;
    ////        title(['Waveform Ch' num2str(ch1 + 1) ' & Ch ' num2str(ch2 + 1)]);
    ////        xlabel('Time (Sec)');
    ////        ylabel([waveUnit1 ', ' waveUnit2]);
            
    ////        subplot(212)
    ////        plot(y1, y2);
    ////        grid on;
    ////        title('Orbit');
    ////        xlabel(['Ch' num2str(ch1 + 1)]);
    ////        ylabel(['Ch ' num2str(ch2 + 1)]);
    ////    end
    ////end



          

    //        ImpaqFileSystemLib.OrderTrackingDataFileClass _classOrderTracking = new ImpaqFileSystemLib.OrderTrackingDataFileClass();
    //        if (_classOrderTracking.LoadFile(FileName))// if (_InterfaceFFT.LoadFile("C:\0011.fd2"))
    //        {
    //            double deltaOrder = resolution_table[(int)(_classOrderTracking.Setup.Resolution)];

    //            int j = 0;
    //            for (int ch_number = 0; ch_number < 4; ch_number++)
    //            {
    //                //Read Order Trace
    //                if (_classOrderTracking.Setup.get_TraceEnabled((byte)ch_number))
    //                {
    //                    ImpaqFileSystemLib.IFloatBuffer rpm = _classOrderTracking.Trace.RPM;


    //                    for (int Odr_no = 0; Odr_no < 4; Odr_no++)
    //                    {
    //                        ImpaqFileSystemLib.IFloatComplexBuffer trace = _classOrderTracking.Trace.get_Order((byte)(j));
    //                        j++;
    //                        if (trace.Size > 0)
    //                        {
    //                            x = new double[trace.Size];
    //                            y = new double[trace.Size];
    //                            double[] y_real = new double[trace.Size];
    //                            double[] y_imag = new double[trace.Size];
    //                            for (int i = 0; i < trace.Size; i++)
    //                            {
    //                                x[i] = rpm[i];
    //                                y[i] = Math.Pow((double)(Math.Pow((double)trace.get_Real(i), (double)2) + Math.Pow((double)trace.get_Imaginary(i), (double)2)), (double)0.5);
    //                                y_real[i] = (double)trace.get_Real(i);
    //                                y_imag[i] = (double)trace.get_Imaginary(i);
    //                            }
    //                            Xlabel = "RPM";
    //                            Ylabel = _classOrderTracking.Setup.get_ChDisplayUnit((byte)Odr_no);
    //                            //Draw Time wave xy graph with labels//
    //                            Create_datapads(dataGridView2, "Trace" + (Odr_no + 1).ToString() + " CH ", ch_number, x, y, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Trace_real" + (Odr_no + 1).ToString() + " CH ", ch_number, x, y, Xlabel, Ylabel);
    //                            Create_datapads(dataGridView2, "Trace_imaginary" + (Odr_no + 1).ToString() + " CH ", ch_number, x, y, Xlabel, Ylabel);
    //                        }
    //                    }
    //                }



    //                //Read Order Spectrum
    //                if (_classOrderTracking.Setup.get_ChEnabled((byte)ch_number))
    //                {
    //                    ImpaqFileSystemLib.IFloatComplexBuffer spectrum = _classOrderTracking.Spectrum.get_Data((byte)ch_number);

    //                    if (spectrum.Size > 0)
    //                    {
    //                        x = new double[spectrum.Size];
    //                        y = new double[spectrum.Size];
    //                        double[] y_real = new double[spectrum.Size];
    //                        double[] y_imag = new double[spectrum.Size];
    //                        for (int i = 0; i < spectrum.Size; i++)
    //                        {
    //                            x[i] = i * deltaOrder;
    //                            y[i] = Math.Pow((double)(Math.Pow((double)spectrum.get_Real(i), (double)2) + Math.Pow((double)spectrum.get_Imaginary(i), (double)2)), (double)0.5);
    //                            y_real[i] = (double)spectrum.get_Real(i);
    //                            y_imag[i] = (double)spectrum.get_Imaginary(i);
    //                        }
    //                        Xlabel = "Order";
    //                        Ylabel = _classOrderTracking.Setup.get_ChDisplayUnit((byte)ch_number);
    //                        //Draw FFT xy graph with labels//
    //                        Create_datapads(dataGridView2, "Spectrum CH ", ch_number, x, y, Xlabel, Ylabel);
    //                        Create_datapads(dataGridView2, "Spectrum_real CH ", ch_number, x, y_real, Xlabel, Ylabel);
    //                        Create_datapads(dataGridView2, "Spectrum_imag CH ", ch_number, x, y_imag, Xlabel, Ylabel);

    //                    }

    //                }

    //                //Read waterfall (order spectrum)
    //                {
    //                    ImpaqFileSystemLib.ICollection coll = _classOrderTracking.Waterfall.Spectrum;

    //                    if (coll.Count > 0)
    //                    {
    //                        Xlabel = "Hz";
    //                        Ylabel = _classOrderTracking.Setup.get_ChDisplayUnit((byte)ch_number);
    //                        for (int i1 = 0; i1 < coll.Count; i1++)
    //                        {
    //                            ImpaqFileSystemLib.OrderTrackingSpectrumData realdata = (ImpaqFileSystemLib.OrderTrackingSpectrumData)_classOrderTracking.Waterfall.Spectrum[i1];
    //                            ImpaqFileSystemLib.IFloatComplexBuffer spectrum = realdata.get_Data((byte)ch_number);
    //                            if (spectrum.Size > 0)
    //                            {
    //                                x = new double[spectrum.Size];
    //                                y = new double[spectrum.Size];

    //                                double[] y_real = new double[spectrum.Size];
    //                                double[] y_imag = new double[spectrum.Size];
    //                                for (int i = 0; i < spectrum.Size; i++)
    //                                {
    //                                    x[i] = i * deltaOrder;
    //                                    y[i] = Math.Pow((double)(Math.Pow((double)spectrum.get_Real(i), (double)2) + Math.Pow((double)spectrum.get_Imaginary(i), (double)2)), (double)0.5);
    //                                    y_real[i] = (double)spectrum.get_Real(i);
    //                                    y_imag[i] = (double)spectrum.get_Imaginary(i);
    //                                }
    //                                Create_datapads(dataGridView2, "Waterfall data " + (i1 + 1).ToString() + " CH", ch_number, x, y, Xlabel, Ylabel);
    //                            }
    //                        }
    //                    }
    //                }
    //            }


    //            ////Read orbit Spectrum
    //            //for (int orbitIndex  = 0; orbitIndex  < 2; orbitIndex ++)
    //            //{
    //            //    int ch_number1 = 2 * orbitIndex;
    //            //    int ch_number2 = ch_number1 + 1;
    //            //    if (_classOrderTracking.Setup.get_ChEnabled((byte)ch_number1) && _classOrderTracking.Setup.get_ChEnabled((byte)ch_number2))
    //            //    {
    //            //        ImpaqFileSystemLib.IOrderTrackingTimewaveData Waveform1 = _classOrderTracking.Orbit.Timewave.Data[ch_number1];
    //            //        ImpaqFileSystemLib.IOrderTrackingTimewaveData Waveform2 = _classOrderTracking.Orbit.Timewave.Data[ch_number2];
    //            //        //ImpaqFileSystemLib.IFloatComplexBuffer spectrum = _classOrderTracking.Orbit.((byte)ch_number);
    //            //        _classOrderTracking.Orbit.Timewave.Data[0];

    //            //        float DeltaTime = _classOrderTracking.Orbit.Timewave.DeltaTime;


    //            //        if (spectrum.Size > 0)
    //            //        {
    //            //            double[] x = new double[spectrum.Size];
    //            //            double[] y = new double[spectrum.Size];
    //            //            double[] y_real = new double[spectrum.Size];
    //            //            double[] y_imag = new double[spectrum.Size];
    //            //            for (int i = 0; i < spectrum.Size; i++)
    //            //            {
    //            //                x[i] = (double)i * (double)DeltaTime;
    //            //                y[i] = Math.Pow((double)(Math.Pow((double)spectrum.get_Real(i), (double)2) + Math.Pow((double)spectrum.get_Imaginary(i), (double)2)), (double)0.5);
    //            //                y_real[i] = (double)spectrum.get_Real(i);
    //            //                y_imag[i] = (double)spectrum.get_Imaginary(i);
    //            //            }
    //            //            string Xlabel = "Order";
    //            //            string Ylabel = _classOrderTracking.Setup.get_ChDisplayUnit((byte)ch_number);
    //            //            //Draw FFT xy graph with labels//
    //            //            _ResizeArray.IncreaseArrayString(ref Xunit, 1);
    //            //            _Xunit[_Xunit.Length - 1] = Xlabel;
    //            //            _ResizeArray.IncreaseArrayString(ref Yunit, 1);
    //            //            _Yunit[_Yunit.Length - 1] = Ylabel;
    //            //            dataGridView2.Rows.Add(1);
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = "Orbit Spectrum CH" + (ch_number + 1).ToString();
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[(int)((2 * ch_number) + 1)];
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[(int)((2 * ch_number) + 1)].ToString();
    //            //            Create_datapads(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value.ToString(), x, y);

    //            //            _ResizeArray.IncreaseArrayString(ref Xunit, 1);
    //            //            _Xunit[_Xunit.Length - 1] = Xlabel;
    //            //            _ResizeArray.IncreaseArrayString(ref Yunit, 1);
    //            //            _Yunit[_Yunit.Length - 1] = Ylabel;

    //            //            dataGridView2.Rows.Add(1);
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = "Orbit Spectrum_real CH" + (ch_number + 1).ToString();
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[(int)((2 * ch_number) + 1)];
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[(int)((2 * ch_number) + 1)].ToString();
    //            //            Create_datapads(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value.ToString(), x, y_real);

    //            //            _ResizeArray.IncreaseArrayString(ref Xunit, 1);
    //            //            _Xunit[_Xunit.Length - 1] = Xlabel;
    //            //            _ResizeArray.IncreaseArrayString(ref Yunit, 1);
    //            //            _Yunit[_Yunit.Length - 1] = Ylabel;

    //            //            dataGridView2.Rows.Add(1);
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = "Orbit Spectrum_imag CH" + (ch_number + 1).ToString();
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[(int)((2 * ch_number) + 1)];
    //            //            dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[(int)((2 * ch_number) + 1)].ToString();
    //            //            Create_datapads(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value.ToString(), x, y_imag);
    //            //        }

    //            //    }
    //            //}

    //        }
    //        else
    //        {
    //            MessageBox.Show("Not accessible");
    //        }
    //    }
    //    public void ReadDRDfile(string FileName)
    //    {
    //        objlistimg.Images.Add(Resources.DarkRed);
    //        objlistimg.Images.Add(Resources.DarkGreen);
    //        objlistimg.Images.Add(Resources.DarkGoldenRod);
    //        objlistimg.Images.Add(Resources.DarkVoilet);
    //        objlistimg.Images.Add(Resources.DarkBlue);
    //        objlistimg.Images.Add(Resources.DimGrey);
    //        objlistimg.Images.Add(Resources.Chocolate);
    //        objlistimg.Images.Add(Resources.DarkKhaki);
    //        objlistimg.Images.Add(Resources.Black);
    //        objlistimg.Images.Add(Resources.Orange);
    //        objlistimg.Images.Add(Resources.Cyan);
    //        objlistimg.Images.Add(Resources.AquaMarine);
    //        objlistimg.Images.Add(Resources.Bisque);
    //        objlistimg.Images.Add(Resources.Blue);
    //        objlistimg.Images.Add(Resources.BlueViolet);
    //        objlistimg.Images.Add(Resources.Coral);
    //        objlistimg.Images.Add(Resources.Darkmagenta);
    //        objlistimg.Images.Add(Resources.DarkseaGreen);
    //        objlistimg.Images.Add(Resources.DarkSlateBlue);
    //        objlistimg.Images.Add(Resources.Deeppink);
    //        objlistimg.Images.Add(Resources.DodgerBlue);
    //        objlistimg.Images.Add(Resources.FireBrick);
    //        objlistimg.Images.Add(Resources.ForestGreen);
    //        objlistimg.Images.Add(Resources.GreenYellow);
    //        objlistimg.Images.Add(Resources.HotPink);
    //        objlistimg.Images.Add(Resources.IndianRed);
    //        objlistimg.Images.Add(Resources.Darkorange);
    //        objlistimg.Images.Add(Resources.Darkorchid);
    //        objlistimg.Images.Add(Resources.DeepSkyBlue);
    //        objlistimg.Images.Add(Resources.SandyBrown);

    //        if (Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.Delete("c:\\vvtemp\\", true);
    //        }
    //        _Xunit = new string[0];
    //        _Yunit = new string[0];
    //        ImpaqFileSystemLib.DataRecorderDataFileClass _classDRD = new ImpaqFileSystemLib.DataRecorderDataFileClass();
    //        if (_classDRD.LoadFile(FileName))
    //        {
    //            int band = band_table[(int)(_classDRD.Setup.Band)];

    //            double samplingRate = (double)1 / (double)(band * 2.56);

    //            for (int ch_number = 0; ch_number < 4; ch_number++)
    //            {
    //                if (_classDRD.Setup.get_ChEnabled((byte)ch_number))
    //                {

    //                    //Read Time waveform
    //                    ImpaqFileSystemLib.IFloatBuffer time = _classDRD.get_Y((byte)ch_number);
    //                    if (time.Size > 0)
    //                    {
    //                        x = new double[time.Size];
    //                        y = new double[time.Size];
    //                        for (int i = 0; i < time.Size; i++)
    //                        {
    //                            x[i] = i * samplingRate;
    //                            y[i] = time[i];
    //                        }
    //                        Xlabel = "Sec";
    //                        Ylabel = _classDRD.Setup.get_ChSensorUnitText((byte)ch_number);

    //                        //Draw Time wave xy graph with labels//
    //                        _ResizeArray.IncreaseArrayString(ref Xunit, 1);
    //                        _Xunit[_Xunit.Length - 1] = Xlabel;
    //                        _ResizeArray.IncreaseArrayString(ref Yunit, 1);
    //                        _Yunit[_Yunit.Length - 1] = Ylabel;

    //                        dataGridView2.Rows.Add(1);
    //                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = "Time Wave CH" + (ch_number + 1).ToString();
    //                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
    //                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[(int)((2 * ch_number))];
    //                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[(int)((2 * ch_number))].ToString();
    //                        Create_datapads(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value.ToString(), x, y);
    //                        //DrawLineGraphs(x, y, Xlabel, Ylabel);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            MessageBox.Show("Not accessible");
    //        }
    //    }

    //    public List<double> angle = new List<double>();
    //    public void ReadBA2File(string FileName)
    //    {
    //        objlistimg.Images.Add(Resources.DarkRed);
    //        objlistimg.Images.Add(Resources.DarkGreen);
    //        objlistimg.Images.Add(Resources.DarkGoldenRod);
    //        objlistimg.Images.Add(Resources.DarkVoilet);
    //        objlistimg.Images.Add(Resources.DarkBlue);
    //        objlistimg.Images.Add(Resources.DimGrey);
    //        objlistimg.Images.Add(Resources.Chocolate);
    //        objlistimg.Images.Add(Resources.DarkKhaki);
    //        objlistimg.Images.Add(Resources.Black);
    //        objlistimg.Images.Add(Resources.Orange);
    //        objlistimg.Images.Add(Resources.Cyan);
    //        objlistimg.Images.Add(Resources.AquaMarine);
    //        objlistimg.Images.Add(Resources.Bisque);
    //        objlistimg.Images.Add(Resources.Blue);
    //        objlistimg.Images.Add(Resources.BlueViolet);
    //        objlistimg.Images.Add(Resources.Coral);
    //        objlistimg.Images.Add(Resources.Darkmagenta);
    //        objlistimg.Images.Add(Resources.DarkseaGreen);
    //        objlistimg.Images.Add(Resources.DarkSlateBlue);
    //        objlistimg.Images.Add(Resources.Deeppink);
    //        objlistimg.Images.Add(Resources.DodgerBlue);
    //        objlistimg.Images.Add(Resources.FireBrick);
    //        objlistimg.Images.Add(Resources.ForestGreen);
    //        objlistimg.Images.Add(Resources.GreenYellow);
    //        objlistimg.Images.Add(Resources.HotPink);
    //        objlistimg.Images.Add(Resources.IndianRed);
    //        objlistimg.Images.Add(Resources.Darkorange);
    //        objlistimg.Images.Add(Resources.Darkorchid);
    //        objlistimg.Images.Add(Resources.DeepSkyBlue);
    //        objlistimg.Images.Add(Resources.SandyBrown);

    //        if (Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.Delete("c:\\vvtemp\\", true);
    //        }
    //        Xlabel = null;
    //        Ylabel = null;

    //        _Xunit = new string[0];
    //        _Yunit = new string[0];
    //        imgctr = 0;

    //        List<double> mass = new List<double>();
    //        //List<double> angle = new List<double>();
            
    //        ImpaqFileSystemLib.BalancerDataFileClass _classBal = new ImpaqFileSystemLib.BalancerDataFileClass();
            
    //        try
    //        {
    //            if (_classBal.LoadFile(FileName))
    //            {

    //                ImpaqFileSystemLib.ICollection iclM = _classBal.AddMass2;
    //                int _PlaneCount = _classBal.Setup2.PlaneCount;

    //                for (int j = 0; j < _PlaneCount; j++)
    //                {
    //                    for (int i = 0; i < iclM.Count; i++)
    //                    {
    //                        ImpaqFileSystemLib.BalancerMassData mdc1 = (ImpaqFileSystemLib.BalancerMassData)_classBal.AddMass2[i];

    //                        ImpaqFileSystemLib.BalancerWeightState bws = mdc1.get_WeightStateItem(j);
    //                        mass.Add(bws.Mass);
    //                        angle.Add(bws.Position);
    //                      //  _Form1._Datagridview1.Rows.Add();
    //                        _Form1._Datagridview1.Rows.Add();
    //                        _Form1._Datagridview1[0, i].Value = i+1;
    //                        _Form1._Datagridview1[1, i].Value = Convert.ToString(bws.Mass);
    //                        _Form1._Datagridview1[2, i].Value = Convert.ToString(bws.Position) + "°";
    //                        //_Form1._Datagridview1.Refresh();          
    //                       // _Form1._Datagridview1.Rows[_Form1._Datagridview1.Rows.Count - 2].Cells[0].Value = Convert.ToString(bws.Mass);
    //                       // _Form1._Datagridview1.Rows[_Form1._Datagridview1.Rows.Count - 2].Cells[1].Value = Convert.ToString(bws.Position) + "°";
    //                       // _Form1._Datagridview1.Rows[_Form1._Datagridview1.Rows.Count - 2].Cells[1].Value = Convert.ToString(Math.Round(Convert.ToDouble(PhaseValueActual[i]), 2)) + "°";
    //                        //Create_datapads(dataGridView2, "Balance Plane " + (j+1).ToString()+" Run ", i ,new double[]{bws.Mass},new double[] {bws.Position}, Xlabel, Ylabel);
    //                    }
    //                    Create_datapads(dataGridView2, "Balance Plane Run", j, mass.ToArray(), angle.ToArray(), Xlabel, Ylabel);
    //                   // yarray = angle.ToArray();
    //                }
    //            }
    //            else
    //            {
    //                MessageBox.Show("Not accessible");
    //            }
    //        }
    //        catch
    //        {
    //        }
    //    }

    //    private double calculatePhase(double Real, double Imaginary)
    //    {
    //        double Retval = 0;
    //        try
    //        {
    //            if (Real > 0)
    //            {
    //                Retval = (double)(Math.Atan(Imaginary / Real));
    //            }
    //            else if (Real < 0)
    //            {
    //                if (Imaginary >= 0)
    //                {
    //                    Retval = (double)(Math.Atan(Imaginary / Real)) + Math.PI;
    //                }
    //                else
    //                {
    //                    Retval = (double)(Math.Atan(Imaginary / Real)) - Math.PI;
    //                }
    //            }
    //            else
    //            {
    //                if (Imaginary > 0)
    //                {
    //                    Retval = (double)Math.PI / 2;
    //                }
    //                else if (Imaginary < 0)
    //                {
    //                    Retval = -(double)Math.PI / 2;
    //                }
    //                else
    //                {
    //                    Retval = 0;
    //                }
    //            }
    //            Retval = convert_Rad_to_Deg(Retval);

    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        return Retval;

    //    }

    //    private double convert_Rad_to_Deg(double rad)
    //    {
    //        double retval;
    //        retval = (double)(rad * (180 / Math.PI));
    //        return retval;
    //    }

    //    #endregion

    //    private void Create_datapads(string UID, double[] XValues, double[] YValues)
    //    {

    //        if (!Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.CreateDirectory("c:\\vvtemp\\");
    //        }

    //        aa = new FileStream("c:\\vvtemp\\" + UID + ".txt", FileMode.Create, FileAccess.ReadWrite);

    //        sw = new StreamWriter(aa);
    //        for (int i = 0; i < XValues.Length; i++)
    //        {
    //            sw.Write(XValues[i] + "/././" + YValues[i] + ".....");
    //        }
    //        sw.Close();

    //    }
    //    private void Create_datapads(DataGridView dataGridView2, string rowtext, int ch_number, double[] x, double[] y, string Xlabel, string Ylabel)
    //    {
    //        _ResizeArray.IncreaseArrayString(ref Xunit, 1);
    //        _Xunit[_Xunit.Length - 1] = Xlabel;
    //        _ResizeArray.IncreaseArrayString(ref Yunit, 1);
    //        _Yunit[_Yunit.Length - 1] = Ylabel;

    //        if (imgctr > 29)
    //        {
    //            imgctr = 0;
    //        }
    //        dataGridView2.Rows.Add(1);
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = rowtext + (ch_number + 1).ToString();
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[imgctr];
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[imgctr].ToString();
    //        imgctr++;

    //        if (!Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.CreateDirectory("c:\\vvtemp\\");
    //        }

    //        aa = new FileStream("c:\\vvtemp\\" + rowtext + (ch_number + 1).ToString() + ".txt", FileMode.Create, FileAccess.ReadWrite);

    //        sw = new StreamWriter(aa);
    //        for (int i = 0; i < x.Length; i++)
    //        {
    //            sw.Write(x[i] + "/././" + y[i] + ".....");
    //        }
    //        sw.Close();

    //    }
    //    private void Create_datapads(DataGridView dataGridView2, string rowtext, double[] x, double[] y, string Xlabel, string Ylabel)
    //    {
    //        _ResizeArray.IncreaseArrayString(ref Xunit, 1);
    //        _Xunit[_Xunit.Length - 1] = Xlabel;
    //        _ResizeArray.IncreaseArrayString(ref Yunit, 1);
    //        _Yunit[_Yunit.Length - 1] = Ylabel;

    //        if (imgctr > 29)
    //        {
    //            imgctr = 0;
    //        }
    //        dataGridView2.Rows.Add(1);
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[0].Value = rowtext ;
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "X";
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = objlistimg.Images[imgctr];
    //        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Tag = ColorCode[imgctr].ToString();
    //        imgctr++;

    //        if (!Directory.Exists("c:\\vvtemp\\"))
    //        {
    //            Directory.CreateDirectory("c:\\vvtemp\\");
    //        }

    //        aa = new FileStream("c:\\vvtemp\\" + rowtext  + ".txt", FileMode.Create, FileAccess.ReadWrite);

    //        sw = new StreamWriter(aa);
    //        for (int i = 0; i < x.Length; i++)
    //        {
    //            sw.Write(x[i] + "/././" + y[i] + ".....");
    //        }
    //        sw.Close();

    //    }

       
    }
}
