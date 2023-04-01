using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace DI_Analyser.interfaces
{
    interface NewHASP_Interface
    {
        void MediaSelected(object sender, EventArgs e);
        string _SelectedMedia
        {
            get;
            set;
        }
        void ModuleSelected(object sender, EventArgs e);
        string _SelectedModule
        {
            get;
            set;
        }
        string[] AccessInstrument(string selectedMedia,string SelectedModule);
       
        string Key_serial
        {
            set;
        }
        string[] _InstrumentData
        {
            get;            
        }
        string _ParentTag
        {
            get;
            set;
        }
        bool IsInstrumentAccessible();
        string[] AccessInstrument(string SelectedMedia, string SelectedModule, DevExpress.XtraTreeList.Nodes.TreeListNode treeListNode);
        void DownloadInstrumenttoSystem(string Instrument, string Computer);

        string GetInstrumentName();
    }
}
