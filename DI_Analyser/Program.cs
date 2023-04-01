using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace DI_Analyser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (ProcessUtils.ThisProcessIsAlreadyRunning())
            {
                ProcessUtils.SetFocusToPreviousInstance("VibAnalyser");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //SplashScreenCreation objSpashScreenCreation = new SplashScreenCreation();
                DevExpress.Skins.SkinManager.EnableFormSkinsIfNotVista();
                DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
                Application.Run(new Form1());
            }
        }
    }
}
