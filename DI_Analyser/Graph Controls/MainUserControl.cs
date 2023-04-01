using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Analyser.interfaces;
using System.Collections;
using DevComponents.DotNetBar;
using DI_Analyser;

namespace Analyser.Graph_Controls
{
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }



        #region MainControl_Interface Members

        public string _Date
        {
            get
            {
                
                return labelXDateValue.Text.ToString();
            }
            set
            {
                labelXDateValue.Text = value;
                
            }
        }

        public string _TriggerType
        {
            get
            {
                return labelXTriggerTypeValue.Text.ToString();
            }
            set
            {
                labelXTriggerTypeValue.Text = value;
            }
        }

        public string _TriggerLevel
        {
            get
            {
                return labelXTriggerLevelValue.Text.ToString();
            }
            set
            {
                labelXTriggerLevelValue.Text = value;
            }
        }

        public string _Slope
        {
            get
            {
                return labelXSlopeValue.Text.ToString();
            }
            set
            {
                labelXSlopeValue.Text = value;
            }
        }

        public string _TransducerUnits
        {
            get
            {
                return labelXTransducerUnitsValue.Text.ToString();
            }
            set
            {
                labelXTransducerUnitsValue.Text = value;
            }
        }

        public string _DisplayUnits
        {
            get
            {
                return labelXDisplayUnitsValue.Text.ToString();
            }
            set
            {
                labelXDisplayUnitsValue.Text = value;
            }
        }

        public string _Sensitivity
        {
            get
            {
                return labelXSensitivityValue.Text.ToString();
            }
            set
            {
                labelXSensitivityValue.Text = value;
            }
        }

        public string _TransducerOffset
        {
            get
            {
                return labelXTransducerOffsetValue.Text.ToString();
            }
            set
            {
                labelXTransducerOffsetValue.Text = value;
            }
        }

        public string _HighPassFilter
        {
            get
            {
                return labelXHighPassFilterValue.Text.ToString();
            }
            set
            {
                labelXHighPassFilterValue.Text = value;
            }
        }

        public string _CouplingType
        {
            get
            {
                return labelXCouplingTypeValue.Text.ToString();
            }
            set
            {
                labelXCouplingTypeValue.Text = value;
            }
        }

        public string _ChannelInput
        {
            get
            {
                return labelXChannelInputValue.Text.ToString();
            }
            set
            {
                labelXChannelInputValue.Text = value;
            }
        }

        public string _FreqType
        {
            get
            {
                return labelXFreqTypeValue.Text.ToString();
            }
            set
            {
                labelXFreqTypeValue.Text = value;
            }
        }

        public string _Orders
        {
            get
            {
                return labelXOrdersValue.Text.ToString();
            }
            set
            {
                labelXOrdersValue.Text = value;
            }
        }

        public string _NoofAvg
        {
            get
            {
                return labelXNoOfAvgValue.Text.ToString();
            }
            set
            {
                labelXNoOfAvgValue.Text = value;
            }
        }

        public string _AvgType
        {
            get
            {
                return labelXAvgTypeValue.Text.ToString();
            }
            set
            {
                labelXAvgTypeValue.Text = value;
            }
        }

        public string _Overlap
        {
            get
            {
                return labelXOverlapValue.Text.ToString();
            }
            set
            {
                labelXOverlapValue.Text = value;
            }
        }

        public string _Detection
        {
            get
            {
                return labelXDetectionValue.Text.ToString();
            }
            set
            {
                labelXDetectionValue.Text = value;
            }
        }

        public string _ApplicationType
        {
            get
            {
                return labelXAppTypeValue.Text.ToString();
            }
            set
            {
                labelXAppTypeValue.Text = value;
            }
        }

        public string _NoofLines
        {
            get
            {
                return labelXNoOfLinesValue.Text.ToString();
            }
            set
            {
                labelXNoOfLinesValue.Text = value;
            }
        }

        public string _WindowType
        {
            get
            {
                return labelXWindowTypeValue.Text.ToString();
            }
            set
            {
                labelXWindowTypeValue.Text = value;
            }
        }

        public string _InputRangeMode
        {
            get
            {
                return labelXInputRangeModeValue.Text.ToString();
            }
            set
            {
                labelXInputRangeModeValue.Text = value;
            }
        }

        public string _FixedRangeValue
        {
            get
            {
                return labelXFixedRangeValueValue.Text.ToString();
            }
            set
            {
                labelXFixedRangeValueValue.Text = value;
            }
        }

        public string _UnitsString
        {
            get
            {
                return labelXUnitsStringsValue.Text.ToString();
            }
            set
            {
                labelXUnitsStringsValue.Text = value;
            }
        }

        public string _YaxisUnits
        {
            get
            {
                return labelXYaxisUnitsValue.Text.ToString();
            }
            set
            {
                labelXYaxisUnitsValue.Text = value;
            }
        }

        public string _XaxisUnits
        {
            get
            {
                return labelXXaxisUnitsValue.Text.ToString();
            }
            set
            {
                labelXXaxisUnitsValue.Text = value;
            }
        }

        public string _ViewSignal
        {
            get
            {
                return labelXViewSignalValue.Text.ToString();
            }
            set
            {
                labelXViewSignalValue.Text = value;
            }
        }

        public string _YaxisDisplay
        {
            get
            {
                return labelXYaxisDisplayValue.Text.ToString();
            }
            set
            {
                labelXYaxisDisplayValue.Text = value;
            }
        }

        public string _SensorType
        {
            get
            {
                return labelXSensorTypeValue.Text.ToString();
            }
            set
            {
                labelXSensorTypeValue.Text = value;
            }
        }

        public string _Overall
        {
            get
            {
                return labelXOverallValue.Text.ToString();
            }
            set
            {
                labelXOverallValue.Text = value;
            }
        }

        public string _AutoMode
        {
            get
            {
                return labelXAutoModeValue.Text.ToString();
            }
            set
            {
                labelXAutoModeValue.Text = value;
            }
        }

        public string _MeasType
        {
            get
            {
                return labelXMeasTypeValue.Text.ToString();
            }
            set
            {
                labelXMeasTypeValue.Text = value;
            }
        }

        public string _MeasDomain
        {
            get
            {
                return labelXMeasDomainValue.Text.ToString();
            }
            set
            {
                labelXMeasDomainValue.Text = value;
            }
        }

        public string _TriggerHysteresis
        {
            get
            {
                return labelXTriggerHysteresisValue.Text.ToString();
            }
            set
            {
                labelXTriggerHysteresisValue.Text = value;
            }
        }

        public string _TriggerPullup
        {
            get
            {
                return labelXTriggerPullupValue.Text.ToString();
            }
            set
            {
                labelXTriggerPullupValue.Text = value;
            }
        }

        public string _BinZeroing
        {
            get
            {
                return labelXBinZeroingValue.Text.ToString();
            }
            set
            {
                labelXBinZeroingValue.Text = value;
            }
        }

        Form1 objForm1 = null;
        public Form1 _Form1
        {
            get
            {
                return objForm1;
            }
            set
            {
                objForm1 = value;
            }
        }
       

        public void DrawLineGraphs(int GraphCount, ArrayList XYData)
        {
            try
            {
                mainGraph_Control1._Form1 = _Form1;
                mainGraph_Control1.DrawLineGraphs(GraphCount, XYData);
                
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
