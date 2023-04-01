using System;
using System.Collections.Generic;

using System.Text;
using Analyser.interfaces;
using DevComponents.DotNetBar;
using System.IO;
using System.Windows.Forms;

namespace Analyser.Classes
{
   static class ErrorLog_Class
    {
       static string sErrorLogPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
       static FileStream aa = null;
       static StreamWriter sw = null;


        static public void ErrorLogEntry(Exception ex)
        {
            try
            {
                if (ex.Message == "Exception of type 'System.OutOfMemoryException' was thrown.")
                {
                    MessageBoxEx.Show("System Memory is low. Please Close Some Applications and Try again", "Low Memory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (!File.Exists(sErrorLogPath + "\\VibAnalyser_Log.txt"))
                {
                    aa = new FileStream(sErrorLogPath + "\\VibAnalyser_Log.txt", FileMode.Create, FileAccess.ReadWrite);
                }
                else
                {
                    aa = new FileStream(sErrorLogPath + "\\VibAnalyser_Log.txt", FileMode.Append, FileAccess.Write);
                }
                sw = new StreamWriter(aa);
                sw.WriteLine(ex.Message + "      " + ex.StackTrace + "          " + System.DateTime.Now.ToString("MM/dd/yyyy HH:m:s tt"));
                sw.Close();
            }
            catch (Exception ex1)
            {

            }
        }

       
    }
}
