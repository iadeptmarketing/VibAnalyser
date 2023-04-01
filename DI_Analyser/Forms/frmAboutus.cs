using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Resources;
using System.Reflection;
using Analyser.Properties;
using DevComponents.DotNetBar;
using Analyser.Classes;

namespace DI_Analyser
{
    public partial class frmAboutus :  DevExpress.XtraEditors.XtraForm
    {
        public frmAboutus()
        {

            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        /// <summary>
        /// This function connects to the http://www.iadeptreliability.com
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("IExplore", linkLabel1.Text.ToString());
                linkLabel1.LinkVisited = true;

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                MessageBoxEx.Show("Error in opening Internet Explorer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        /// <summary>
        /// This function connects to the http://www.iadeptmarketing.com
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("IExplore", linkLabel2.Text.ToString());
                linkLabel2.LinkVisited = true;

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                MessageBoxEx.Show("Error in opening Internet Explorer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        /// <summary>
        /// This function connects to the http://www.iadept.co.in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("IExplore", linkLabel3.Text.ToString());
                linkLabel3.LinkVisited = true;

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                MessageBoxEx.Show("Error in opening Internet Explorer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        /// <summary>
        /// This function sets the Picture box size and sets the image to that
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAboutus_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"globe.swf"))
                {
                   
                    axShockwaveFlash1.Movie = AppDomain.CurrentDomain.BaseDirectory + @"globe.swf";
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
                ErrorLog_Class.ErrorLogEntry(ex);
                
            }
        }



        //#region ReadResourceFile
        ///// <summary>
        ///// method for reading a value from a resource file
        ///// (.resx file)
        ///// </summary>
        ///// <param name="file">file to read from</param>
        ///// <param name="key">key to get the value for</param>
        ///// <returns>a string value</returns>
        //public string ReadResourceValue(string file, string key)
        //{
        //    //value for our return value
        //    string resourceValue = string.Empty;
        //    try
        //    {
        //        // specify your resource file name 
        //        string resourceFile = file;
        //        // get the path of your file
        //        string filePath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        //        // create a resource manager for reading from
        //        //the resx file
        //        ResourceManager resourceManager = ResourceManager.CreateFileBasedResourceManager(resourceFile, filePath, null);
                
        //        // retrieve the value of the specified key
        //        resourceValue = resourceManager.GetString(key);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        resourceValue = string.Empty;
        //    }
        //    return resourceValue;
        //}
        //#endregion
    }
}
