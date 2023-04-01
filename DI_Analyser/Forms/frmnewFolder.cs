using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Analyser.Classes;

namespace Analyser.Forms
{
    public partial class frmnewFolder :  DevExpress.XtraEditors.XtraForm
    {
        public frmnewFolder()
        {
            InitializeComponent();
        }
        string NametoReturn = null;
        public string _NewFolderName
        {
            get
            {
                return NametoReturn;
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbNewFolderName.Text.ToString()))
                {
                    if (tbNewFolderName.Text.ToString().Contains("/") || tbNewFolderName.Text.ToString().Contains("\\") || tbNewFolderName.Text.ToString().Contains(":") || tbNewFolderName.Text.ToString().Contains("*") || tbNewFolderName.Text.ToString().Contains("?") || tbNewFolderName.Text.ToString().Contains("\"") || tbNewFolderName.Text.ToString().Contains("<") || tbNewFolderName.Text.ToString().Contains(">") || tbNewFolderName.Text.ToString().Contains("|") || tbNewFolderName.Text.ToString().Contains(".") || tbNewFolderName.Text.ToString().Contains("{") || tbNewFolderName.Text.ToString().Contains("}") || tbNewFolderName.Text.ToString().Contains("[") || tbNewFolderName.Text.ToString().Contains("]"))
                    {
                        MessageBoxEx.Show("Folder Name cannot contain special characters");
                    }
                    else
                    {
                        NametoReturn = tbNewFolderName.Text.ToString();
                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
    }
}
