using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Analyser.Graph_Controls
{
    public partial class DataGrid_Control : UserControl
    {
        public DataGrid_Control()
        {
            InitializeComponent();
        }
        
        public DataGridView _DataGridView
        {
            get
            {
                return dataGridViewX1;
            }
           
        }
    }
}
