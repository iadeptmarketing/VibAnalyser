using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DI_Analyser.Properties;
using System.IO;

namespace DI_Analyser
{
    public partial class BusyFrm : DevExpress.XtraEditors.XtraForm
    {
        public BusyFrm()
        {
            InitializeComponent();
            this.Refresh();
        }
        bool WorkDone = false;

        private void BusyFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Comp\globe.swf"))
                {

                    axShockwaveFlash1.Movie = AppDomain.CurrentDomain.BaseDirectory + @"Comp\globe.swf";
                    axShockwaveFlash1.Size = new Size(145, 117);

                }
                else
                {

                    PictureBox pb = new PictureBox();
                    pb.Image = Resources.company_name;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Location = axShockwaveFlash1.Location;
                    pb.BringToFront();
                    pb.Size = new Size(145, 117);
                    this.Controls.Remove(axShockwaveFlash1);
                    this.Controls.Add(pb);

                }

            }
            catch (Exception ex)
            {

            }
        }
       
    }
}
