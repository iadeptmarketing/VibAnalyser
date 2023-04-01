using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace Analyser.Forms
{
    public partial class BalDataForm : DevExpress.XtraEditors.XtraForm
    {
        public BalDataForm()
        {
            InitializeComponent();
        }
        public DataGridViewX _Vib
        {
            get
            {
                return dataGridViewVib;
            }
            set
            {
                dataGridViewVib = value;
            }
        }
        public DataGridViewX _Wts
        {
            get
            {
                return dataGridViewWts;
            }
            set
            {
                dataGridViewWts = value;
            }
        }
    }
}
