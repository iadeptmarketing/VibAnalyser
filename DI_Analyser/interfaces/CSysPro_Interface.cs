using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_Analyser.interfaces
{
    interface CSysPro_Interface
    {
        string FromOtherThread
        {
            set;
        }

        string GetFormClosed
        {
            get;
        }
        bool IsOnceLogged
        {
            get;
            set;
        }
        string decrypt(string s);
        bool Test();
        bool Test(string sText);
        bool NewTest();
        bool Check(byte[] Param1);
        bool UnCheck(byte[] Param2);
        void objHasp_ExitButtonClicked();
        void OpenFrm();
        void objHasp_DemoButtonClicked();
        void objHasp_TryButtonClicked();
        void ECB();
        bool GetAndCheck();
    }
}
