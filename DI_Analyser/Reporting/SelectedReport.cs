using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;

namespace Analyser.Reporting
{
    public partial class SelectedReport : DevExpress.XtraEditors.XtraForm
    {
        public SelectedReport()
        {
            InitializeComponent();
        }
        Hashtable _hashtable = new Hashtable();
        public Hashtable _HashTable
        {
            get
            {
                return _hashtable;
            }            
        }
        bool showgraph = true;
        public bool _ShowGraph
        {
            get
            {
                return showgraph;
            }            
        }
        string sErrorLogPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
        
        private void buttonX1_Click(object sender, EventArgs e)
        {
            XmlAttribute xaFreq = null;
            string[] sarrLabel ={"Date","Trigger_Type","Trigger_Level","Slope","Transducer_Units","Sensitivity",
                    "Transducer_offset","Display_Units","High_Pass_Filter","Coupling_Type","Channel_Input",
                "Freq_Type","Orders","No._of_Averages","Average_Type","Overlap","Detection","No._of_Lines",
                "Window_Type","Input_Range_Mode","Fixed_Range_Value","Auto_Mode","Meas._Type","Meas._Domain","Trigger_Hysperesis",
                "Trigger_Pullup","Bin_Zeroing","Units_String","Y-axis_Units","X-axis_Units","View_Signal", "Y-axis_Display", 
                "Sensor_Type","Overall", "Show_Graph"};
            bool[] sarrValues ={cbDateTime.Checked, cbTriggertype.Checked, cbTriggerlevel.Checked, cbSlope.Checked
, cbTransducerUnit.Checked, cbSensitivity.Checked, cbTransducerOffset.Checked, cbDisplayUnit.Checked, cbHPF.Checked
, cbCouplingtype.Checked, cbChannelInput.Checked, cbFrequencyType.Checked, cbOrder.Checked, cbNoofAvg.Checked
, cbAverageType.Checked, cbOverlap.Checked, cbDetection.Checked, cbNoofLines.Checked, cbWindowtype.Checked, cbInputRangeMode.Checked
, cbFixedRangevalue.Checked, cbAutoMode.Checked, cbMeastype.Checked, cbMeasdomain.Checked, cbTriggerHysteresis.Checked
, cbTriggerPullup.Checked, cbBinzeroing.Checked, cbUnitString.Checked, cbYaxisUnits.Checked, cbXaxisUnits.Checked
, cbViewsignal.Checked, cbYaxisDisplay.Checked, cbSensorType.Checked, cbOverall.Checked,cbGraph.Checked};

            XmlDocument m_xdDocument = new XmlDocument();

            {
                if (!File.Exists(sErrorLogPath + "\\RS.XML"))
                {

                    m_xdDocument = new XmlDocument();
                    XmlNode xnBand = m_xdDocument.CreateElement("Report");
                    //XmlNode xnValues = m_xdDocument.CreateElement("Values");
                    //xnBand.AppendChild(xnValues);
                    m_xdDocument.AppendChild(xnBand);
                    m_xdDocument.Save(sErrorLogPath + "\\RS.XML");


                }
                {

                    m_xdDocument.Load(sErrorLogPath + "\\RS.XML");
                    string sXPath = "//Report/Parameters";

                    XmlNode xnFile = m_xdDocument.SelectSingleNode("//Report/Parameters");
                    if (xnFile != null)
                    {
                        xnFile.ParentNode.RemoveChild(xnFile);

                    }

                    {
                        XmlNode xnBand = m_xdDocument.SelectSingleNode("//Report");
                        XmlNode xnFilePath = m_xdDocument.CreateElement("Parameters");

                        for (int i = 0; i < sarrLabel.Length; i++)
                        {
                            //if (dgvDiFF.Rows[i].Cells[0].Value != null && dgvDiFF.Rows[i].Cells[1].Value != null)
                            {
                                XmlNode xnValues = m_xdDocument.CreateElement("Values");
                                xaFreq = m_xdDocument.CreateAttribute(sarrLabel[i].ToString());
                                xaFreq.Value = sarrValues[i].ToString();


                                xnValues.Attributes.Append(xaFreq);


                                xnFilePath.AppendChild(xnValues);
                            }
                        }
                        xnBand.AppendChild(xnFilePath);
                        //m_xdDocument.AppendChild(xnBand);
                        m_xdDocument.Save(sErrorLogPath + "\\RS.XML");
                    }


                }



                //_hashtable = new Hashtable();
                //_hashtable.Add("Date", cbDateTime.Checked);
                //_hashtable.Add("Trigger Type", cbTriggertype.Checked);
                //_hashtable.Add("Trigger Level", cbTriggerlevel.Checked);
                //_hashtable.Add("Slope", cbSlope.Checked);
                //_hashtable.Add("Transducer Units", cbTransducerUnit.Checked);
                //_hashtable.Add("Sensitivity", cbSensitivity.Checked);
                //_hashtable.Add("Transducer offset", cbTransducerOffset.Checked);
                //_hashtable.Add("Display Units", cbDisplayUnit.Checked);
                //_hashtable.Add("High Pass Filter", cbHPF.Checked);
                //_hashtable.Add("Coupling Type", cbCouplingtype.Checked);
                //_hashtable.Add("Channel Input", cbChannelInput.Checked);
                //_hashtable.Add("Freq Type", cbFrequencyType.Checked);
                //_hashtable.Add("Orders", cbOrder.Checked);
                //_hashtable.Add("No. of Averages", cbNoofAvg.Checked);
                //_hashtable.Add("Average Type", cbAverageType.Checked);
                //_hashtable.Add("Overlap", cbOverlap.Checked);
                //_hashtable.Add("Detection", cbDetection.Checked);
                //_hashtable.Add("No. of Lines", cbNoofLines.Checked);
                //_hashtable.Add("Window Type", cbWindowtype.Checked);
                //_hashtable.Add("Input Range Mode", cbInputRangeMode.Checked);
                //_hashtable.Add("Fixed Range Value", cbFixedRangevalue.Checked);
                //_hashtable.Add("Auto Mode", cbAutoMode.Checked);
                //_hashtable.Add("Meas. Type", cbMeastype.Checked);
                //_hashtable.Add("Meas. Domain", cbMeasdomain.Checked);
                //_hashtable.Add("Trigger Hysperesis", cbTriggerHysteresis.Checked);
                //_hashtable.Add("Trigger Pullup", cbTriggerPullup.Checked);
                //_hashtable.Add("Bin Zeroing", cbBinzeroing.Checked);
                //_hashtable.Add("Units String", cbUnitString.Checked);
                //_hashtable.Add("Y-axis Units", cbYaxisUnits.Checked);
                //_hashtable.Add("X-axis Units", cbXaxisUnits.Checked);
                //_hashtable.Add("View Signal", cbViewsignal.Checked);
                //_hashtable.Add("Y-axis Display", cbYaxisDisplay.Checked);
                //_hashtable.Add("Sensor Type", cbSensorType.Checked);
                //_hashtable.Add("Overall", cbOverall.Checked);
                //showgraph = cbGraph.Checked;
                this.Close();
            }
        }
        //private void buttonX1_Click(object sender, EventArgs e)
        //{
        //    _hashtable = new Hashtable();
        //    _hashtable.Add("Date", cbDateTime.Checked);
        //    _hashtable.Add("Trigger Type", cbTriggertype.Checked);
        //    _hashtable.Add("Trigger Level", cbTriggerlevel.Checked);
        //    _hashtable.Add("Slope", cbSlope.Checked);
        //    _hashtable.Add("Transducer Units", cbTransducerUnit.Checked);
        //    _hashtable.Add("Sensitivity", cbSensitivity.Checked);
        //    _hashtable.Add("Transducer offset", cbTransducerOffset.Checked);
        //    _hashtable.Add("Display Units", cbDisplayUnit.Checked);
        //    _hashtable.Add("High Pass Filter", cbHPF.Checked);
        //    _hashtable.Add("Coupling Type", cbCouplingtype.Checked);
        //    _hashtable.Add("Channel Input", cbChannelInput.Checked);
        //    _hashtable.Add("Freq Type", cbFrequencyType.Checked);
        //    _hashtable.Add("Orders", cbOrder.Checked);
        //    _hashtable.Add("No. of Averages", cbNoofAvg.Checked);
        //    _hashtable.Add("Average Type", cbAverageType.Checked);
        //    _hashtable.Add("Overlap", cbOverlap.Checked);
        //    _hashtable.Add("Detection", cbDetection.Checked);
        //    _hashtable.Add("No. of Lines", cbNoofLines.Checked);
        //    _hashtable.Add("Window Type", cbWindowtype.Checked);
        //    _hashtable.Add("Input Range Mode", cbInputRangeMode.Checked);
        //    _hashtable.Add("Fixed Range Value", cbFixedRangevalue.Checked);
        //    _hashtable.Add("Auto Mode", cbAutoMode.Checked);
        //    _hashtable.Add("Meas. Type", cbMeastype.Checked);
        //    _hashtable.Add("Meas. Domain", cbMeasdomain.Checked);
        //    _hashtable.Add("Trigger Hysperesis", cbTriggerHysteresis.Checked);
        //    _hashtable.Add("Trigger Pullup", cbTriggerPullup.Checked);
        //    _hashtable.Add("Bin Zeroing", cbBinzeroing.Checked);
        //    _hashtable.Add("Units String", cbUnitString.Checked);
        //    _hashtable.Add("Y-axis Units", cbYaxisUnits.Checked);
        //    _hashtable.Add("X-axis Units", cbXaxisUnits.Checked);
        //    _hashtable.Add("View Signal", cbViewsignal.Checked);
        //    _hashtable.Add("Y-axis Display", cbYaxisDisplay.Checked);
        //    _hashtable.Add("Sensor Type", cbSensorType.Checked);
        //    _hashtable.Add("Overall", cbOverall.Checked);
        //    showgraph = cbGraph.Checked;
        //    this.Close();
        //}
    }
}
