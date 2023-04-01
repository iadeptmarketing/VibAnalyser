using System;
using System.Collections.Generic;

using System.Text;

namespace DI_Analyser.interfaces
{
	interface UI_Interface
	{

        Form1 _Form1
        {
            get;
            set;
        }
        void ChangeStyle(string StyleName);
        void ChangeColorStyle(int A, int R, int G, int B);
        void ChangeColorStyle(string Name);
	}
}
