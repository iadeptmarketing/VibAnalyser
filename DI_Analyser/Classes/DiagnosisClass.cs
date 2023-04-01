using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyser.Classes
{
    class DiagnosisClass
    {


        //double[] xx = null;
        //int[] yy = null;
        int[] ff = null;
        double[] dXVals = null;
        double[] dYVals = null;
        
        public ArrayList FaultNameWithValue = new ArrayList();
        string allPeak = null;
        List<string> FaultName = new List<string>();
        List<string> FaultDescription = new List<string>();

        /// <summary>
        /// Returns the Faults Name for provided data
        /// </summary>
        /// <param name="xyData1">ArrayList for x and y data</param>
        /// <param name="rpm">RPM value. In case RPM value is not known pass 0</param>
        /// <param name="rpmMargin"> RPM Margin value. 0 to be passed if not required</param>
        /// <returns></returns>
        public List<string> BasicDiagnosis(ArrayList xyData1, int rpm, int rpmMargin)
        {
            FaultName = new List<string>();
            FaultDescription = new List<string>();
            try
            {
                dXVals = (double[]) xyData1[0];
                dYVals = (double[]) xyData1[1];
                double Fst;
                double Scnd;
                double Thrd;
                List<Peak> peaks = new List<Peak>();
                //Fetching the peeks
                try
                {
                    for (int i = 2; i < dYVals.Length; i++)
                    {
                        Fst = dYVals[i - 2];
                        Scnd = dYVals[i - 1];
                        Thrd = dYVals[i];

                        if (Fst < Scnd && Scnd > Thrd)
                        {
                            peaks.Add(new Peak() {PeakAmplitude = dYVals[i - 1], PeakLocation = dXVals[i - 1]});
                        }
                    }
                }
                catch
                {
                }

                //Sorting Peaks in ascending order
                peaks.Sort();
                //Rearranging Peeks in descending order
                peaks.Reverse();

                //Getting highest Peak x (in CPM/RPM) and y
                int highestPeakX = (int) (peaks[0].PeakLocation * (double) 60);
                double highestPeakY = peaks[0].PeakAmplitude;

                //Finalizing RPM
                if (rpm <= 0)
                {
                    if (Math.Abs(rpm - highestPeakX) < rpmMargin)
                    {
                        rpm = highestPeakX;
                    }
                }
                else if (rpmMargin > 0)
                {
                    var rpmMarginLower = rpm - rpmMargin;
                    var rpmMarginHigher = rpm + rpmMargin;
                    if (Enumerable.Range(rpmMarginLower, rpmMarginHigher).Contains(highestPeakX))
                    {
                        rpm = highestPeakX;
                    }
                }

                //Maintaining limited peaks subjected to 1/5th of the highest peak
                double rangeValue = highestPeakY / 5;
                peaks.RemoveAll(x => x.PeakAmplitude < rangeValue);

                FaultName = new List<string>();
                List<string> FaultDescription = new List<string>();

                List<double> peakOrder = new List<double>();
                foreach (var peak in peaks)
                {
                    peakOrder.Add(Math.Round(peak.PeakLocation * 60 / rpm, 2));
                }

                double margin = rpmMargin <= 0 ? 0.05 : (double) 1 / (double) rpmMargin;

                GeneralIssues(peakOrder, margin, peaks);

                FaultNameWithValue.Clear();
                //FaultNameWithValue.Add(ff);
                FaultNameWithValue.Add(FaultName);
                FaultNameWithValue.Add(FaultDescription);
            }
            catch
            {
            }

            return FaultName;
        }

        void GeneralIssues(List<double> peakOrder, double margin, List<Peak> peaks)
        {
            bool[] finalstatus = new bool[1];
            allPeak = peakOrder.Count.ToString();
            if (peakOrder[0] <= (1.0 + margin) && peakOrder[0] >= (1.0 - margin))
            {
                if (allPeak == "1")
                {
                    //case 1 Unbalance and possible Losseness Structural
                    //ff[0] = peaks[0].PeakLocation;
                    FaultName.Add("Unbalance / Losseness Structural");
                    FaultDescription.Add("Description :" + System.Environment.NewLine +
                                         "There is only one significant peak on first order");
                    finalstatus[0] = true;
                }
                else if (allPeak == "2")
                {
                    if (peakOrder[1] <= (2.0 + margin) && peakOrder[1] >= (2.0 - margin))
                    {
                        //case 2 Bent Shaft

                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        FaultName.Add("Bent Shaft / Unbalance");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are two significant peaks and also first is on first order and second is on second order ");
                        finalstatus[0] = true;
                    }
                    else
                    {
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        FaultName.Add("Unbalance");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are two significant peaks and also first is on first order and second is on more than fifth order");
                        finalstatus[0] = true;
                    }
                }
                else if (allPeak == "3")
                {
                    if ((peakOrder[1] <= (2.0 + margin) && peakOrder[1] >= (2.0 - margin)) &&
                        (peakOrder[2] <= (3.0 + margin) && peakOrder[2] >= (3.0 - margin)))
                    {
                        //case 3 Angular Missalignment
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        FaultName.Add("Angular Missalignment / Unbalance");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are three significant peaks and also first is on first order and second is on second or third order ");
                        finalstatus[0] = true;
                    }
                    else if ((peakOrder[1] <= (3.0 + margin) && peakOrder[1] >= (3.0 - margin)) &&
                             (peakOrder[2] <= (2.0 + margin) && peakOrder[2] >= (2.0 - margin)))
                    {
                        //case 4 Angular Missalignment
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        FaultName.Add("Angular Missalignment / Unbalance");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are three significant peaks and also first is on first order and second is on third order");
                        finalstatus[0] = true;
                    }
                }
                else if (Convert.ToInt32(allPeak) >= 4)
                {
                    if (peakOrder[0] <= (1.0 + margin) && peakOrder[0] >= (1.0 - margin))
                    {
                        if (peakOrder[1] <= (2.0 + margin) && peakOrder[1] >= (2.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order.  ");
                            finalstatus[0] = true;
                        }
                        else if (peakOrder[1] <= (3.0 + margin) && peakOrder[1] >= (3.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }
                        else if (peakOrder[1] <= (4.0 + margin) && peakOrder[1] >= (4.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }
                        else if (peakOrder[1] <= (5.0 + margin) && peakOrder[1] >= (5.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }
                        else
                        {
                            //ff[0] = yy[0];
                            FaultName.Add("Unbalance / Losseness Structural");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are more than one significant peak but highest peak is on first order as per our basic fault detection system....");
                            finalstatus[0] = true;
                        }


                    }
                    else if (peakOrder[0] <= (2.0 + margin) && peakOrder[0] >= (2.0 - margin))
                    {
                        if (peakOrder[1] <= (1.0 + margin) && peakOrder[1] >= (1.0 - margin))
                        {
                            //case 11 Severe misalignment and possible Looseness
                            //for (int i = 0; i > Convert.ToInt32(allPeak); i++)
                            //{
                            //    ff[i] = yy[i];
                            //}

                            FaultName.Add(" Severe misalignment / Losseness Rotating");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. but first peak is on second order ");
                            finalstatus[0] = true;
                        }
                    }
                }
            }
            else if (peakOrder[0] <= (2.0 + margin) && peakOrder[0] >= (2.0 - margin))
            {
                if (allPeak == "3")
                {
                    if ((peakOrder[1] <= (1.0 + margin) && peakOrder[1] >= (1.0 - margin)) &&
                        (peakOrder[2] <= (3.0 + margin) && peakOrder[2] >= (3.0 - margin)))
                    {
                        //case 5 Cocked bearing
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        FaultName.Add("Cocked bearing / Parallel Misallignment");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are three significant peaks and also first is on second or first order and second is on first or second order and third is on third order  ");
                        finalstatus[0] = true;
                    }
                    else if ((peakOrder[1] <= (1.0 + margin) && peakOrder[1] >= (1.0 - margin)) &&
                             (peakOrder[2] <= (3.0 + margin) && peakOrder[2] >= (3.0 - margin)))
                    {
                        //case 6 Parallel Misallignment
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        FaultName.Add(" Parallel Misallignment / Cocked bearing");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are three significant peaks and also first is on second order and second is on first order and third is on third order ");
                        finalstatus[0] = true;
                    }
                    else if ((peakOrder[1] <= (3.0 + margin) && peakOrder[1] >= (3.0 - margin)) &&
                             (peakOrder[2] <= (1.0 + margin) && peakOrder[2] >= (1.0 - margin)))
                    {
                        //case 7 Parallel Misallignment
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        FaultName.Add(" Parallel Misallignment / Cocked bearing");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are three significant peaks and also first is on second order and second is on third order and third is on first order  ");
                        finalstatus[0] = true;
                    }
                }
                else if (allPeak == "4")
                {
                    if (((peakOrder[1] <= (1.0 + margin) && peakOrder[1] >= (1.0 - margin)) &&
                         (peakOrder[2] <= (3.0 + margin) && peakOrder[2] >= (3.0 - margin))) &&
                        (peakOrder[3] >= (1.0 - margin)))
                    {
                        //case 8 Losseness Pillow block
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        //ff[3] = yy[3];
                        FaultName.Add("Losseness Pillow block / Misallignment");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are four significant peaks and also first is on second order and second is on first order and third is on third order  ");
                        finalstatus[0] = true;
                    }
                    else if (((peakOrder[1] <= (3.0 + margin) && peakOrder[1] >= (3.0 - margin)) &&
                              (peakOrder[2] <= (1.0 + margin) && peakOrder[2] >= (1.0 - margin)) &&
                              (peakOrder[3] >= (1.0 - margin))))
                    {
                        //case 9 Losseness Pillow block
                        //ff[0] = yy[0];
                        //ff[1] = yy[1];
                        //ff[2] = yy[2];
                        //ff[3] = yy[3];
                        FaultName.Add("Losseness Pillow block / Misallignment");
                        FaultDescription.Add("Description :" + System.Environment.NewLine +
                                             "There are four significant peaks and also first is on second order and second is on third order and third is on first order  ");
                        finalstatus[0] = true;
                    }
                }
            }
            else
            {
                int firstOrder = Convert.ToInt32(allPeak[0]);

                if (Convert.ToInt32(allPeak) >= 5)
                {
                    if (peakOrder[0] <= (1.0 + margin) && peakOrder[0] >= (1.0 - margin))
                    {
                        if (peakOrder[1] <= (2.0 + margin) && peakOrder[1] >= (2.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }
                        else if (peakOrder[1] <= (3.0 + margin) && peakOrder[1] >= (3.0 - margin))
                        {
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }
                        else if (peakOrder[1] <= (4.0 + margin) && peakOrder[1] >= (4.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }
                        else if (peakOrder[1] <= (5.0 + margin) && peakOrder[1] >= (5.0 - margin))
                        {
                            //case 10 Losseness Rotating and possible  Unbalance
                            //ff[0] = yy[0];
                            //ff[1] = yy[1];
                            //ff[2] = yy[2];
                            //ff[3] = yy[3];
                            FaultName.Add(" Losseness Rotating / Unbalance");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. ");
                            finalstatus[0] = true;
                        }

                    }
                    else if (peakOrder[0] <= (2.0 + margin) && peakOrder[0] >= (2.0 - margin))
                    {
                        if (peakOrder[1] <= (1.0 + margin) && peakOrder[1] >= (1.0 - margin))
                        {
                            //case 11 Severe misalignment and possible Looseness
                            //for (int i = 0; i > Convert.ToInt32(allPeak); i++)
                            //{
                            //    ff[i] = yy[i];
                            //}

                            FaultName.Add(" Severe misalignment / Losseness Rotating");
                            FaultDescription.Add("Description :" + System.Environment.NewLine +
                                                 "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order. but first peak is on second order ");
                            finalstatus[0] = true;
                        }
                    }
                }
            }
        }

        public string Description { get; set; }
    }
}

