using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_Analyser.interfaces
{
    interface CImageCreation_interface
    {
        string decrypt(string strdecrypt);
        void FirstTest(byte[] Param1);
        void FirstTest(double[] Param1);
        bool Test(string sText);
        bool NewTest();
        bool GetAndCheck();
        bool Check(byte[] Param1);
        bool UnCheck(byte[] Param2);
        bool SecondTest(double[] Param2);
        bool SecondTest(byte[] Param2);
        byte[] GetBytes();
        double[] GetDouble();    
    }
}
