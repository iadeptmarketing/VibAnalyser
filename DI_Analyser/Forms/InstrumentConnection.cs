using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using Analyser.Properties;
using DevExpress.XtraTreeList.Nodes;
using System.IO;
using DI_Analyser.interfaces;
using DI_Analyser.Classes;
using System.Collections;
//using System.Security.AccessControl;
using Analyser.Forms;
using DevComponents.DotNetBar;
using Analyser.Classes;

namespace DI_Analyser.Forms
{
    public partial class InstrumentConnection : Form//DevExpress.XtraEditors.XtraForm
    {
        public InstrumentConnection()
        {
            InitializeComponent();
            m_ImgLst.Images.Add(Resources.openfolder);
            m_ImgLst.Images.Add(Resources.CSVf);
            m_ImgLst.Images.Add(Resources.WAVf);
            m_ImgLst.Images.Add(Resources.DAT);
            m_ImgLst.Images.Add(Resources.file);
            treeList1.StateImageList = m_ImgLst;
            treeList2.StateImageList = m_ImgLst;
        }
        ImageList m_ImgLst = new ImageList();
        NewHASP_Interface _HaspInt = new NewHasp_Control();
        TreeListNode node = null;
        TreeListNode node1 = null;


        private void InstrumentConnection_Shown(object sender, EventArgs e)
        {  
                try
                {

                    FillComputerTreelist();
                    FillInstrumentTreelist();
                    setTreelist2Tag();
                    
                }
                catch (Exception err)
                {
                    ErrorLog_Class.ErrorLogEntry(err);
                }            
        }

        private void setTreelist2Tag()
        {
            //throw new NotImplementedException();
           string InstrumentName= _HaspInt.GetInstrumentName();
            treeList2.FocusedNode.Tag = @"\Storage Card\"+InstrumentName+@"\FFT\Data";
        }
        string SerialFromKey = null;
        public string Key_serial
        {
            set
            {
                SerialFromKey = value;
            }
        }
        private void FillComputerTreelist()
        {
            try
            {
                // �-create the root node in treelist of Computer�-
                string sPath = null;
                sPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                if (string.IsNullOrEmpty(sPath))
                {
                    sPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                node = treeList1.AppendNode(new object[] { sPath }, null);
                node.Tag = "Folder";
                node.StateImageIndex = 0;

                node1 = treeList1.AppendNode(new object[] { "" }, node);

                treeList1.FocusedNode = node;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        string SelectedMedia = null;
        string SelectedModule = null;
        private void FillInstrumentTreelist()
        {
            try
            {

                // �-create the root node in treelist of Instrument�-

               
                string sPath = "Mobile Device";

                node = treeList2.AppendNode(new object[] { sPath }, null);
                if (_HaspInt._ParentTag != null)
                {
                    node.Tag = _HaspInt._ParentTag;
                }
                else
                {
                    node.Tag = null;//"\\Internal Disk\\Analyser"
                }
                node.StateImageIndex = 0;

                node1 = treeList2.AppendNode(new object[] { "" }, node);

                treeList2.FocusedNode = node;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            try
            {
                e.Node.Nodes.Clear(); // clears all the nodes and...
                displayChildNodes(e.Node); //  create the nodes again
            }
            catch (Exception err)
            {
                ErrorLog_Class.ErrorLogEntry(err);
                MessageBoxEx.Show(err.Message);
            }
        }

        private void displayChildNodes(TreeListNode parentNode)
        {
            DirectoryInfo FS = new DirectoryInfo(GetPath(parentNode));
            TreeListNode node1 = null;
            TreeListNode node = null;
            try
            {
                foreach (DirectoryInfo dirInfo in FS.GetDirectories())
                {
                    //' �-create a new node �-


                    node1 = treeList1.AppendNode(new object[] { dirInfo.Name }, parentNode);
                    node1.Tag = "Folder";
                    node1.StateImageIndex = 0;

                    node = treeList1.AppendNode(new object[] { "" }, node1);
                    
                }
            }
            catch (Exception err)
            {
                ErrorLog_Class.ErrorLogEntry(err);
                MessageBoxEx.Show(err.Message);
            }

            try
            {
                // �display all files�-
                foreach (FileInfo fileInfo in FS.GetFiles())
                {
                    if (fileInfo.Name.EndsWith(".csv", true, null))
                    {
                        node1 = treeList1.AppendNode(new object[] { fileInfo.Name }, parentNode);
                        node1.Tag = "CSVFile";
                        node1.StateImageIndex = 1;
                    }
                    //if (fileInfo.Name.EndsWith(".wav", true, null))
                    //{
                    //    node1 = treeList1.AppendNode(new object[] { fileInfo.Name }, parentNode);
                    //    node1.Tag = "WAVFile";
                    //    node1.StateImageIndex = 2;
                    //}
                    //if (fileInfo.Name.EndsWith(".dat", true, null))
                    //{
                    //    node1 = treeList1.AppendNode(new object[] { fileInfo.Name }, parentNode);
                    //    node1.Tag = "DATFile";
                    //    node1.StateImageIndex = 3;
                    //}
                    //if (fileInfo.Name.EndsWith(".bal", true, null))
                    //{
                    //    node1 = treeList1.AppendNode(new object[] { fileInfo.Name }, parentNode);
                    //    node1.Tag = "BALFile";
                    //    node1.StateImageIndex = 4;
                    //}

                }
            }
            catch (Exception err)
            {
                ErrorLog_Class.ErrorLogEntry(err);
                MessageBoxEx.Show(err.Message);
            }
        }
        private string GetPath(TreeListNode parentNode)
        {
            string SelectedPath = null;
            string temp = null;
            try
            {
                do
                {
                    temp += parentNode.GetDisplayText(0);
                    temp += "<>";
                    parentNode = parentNode.ParentNode;
                } while (parentNode != null);

                string[] splitedtemp = temp.Split(new string[] { "<>" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = splitedtemp.Length - 1; i >= 0; i--)
                {
                    SelectedPath += splitedtemp[i] + @"\";
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return SelectedPath;
        }
        string lastaccessedFolder = null;
        public string _LastAccessedFolder
        {
            get
            {
                return lastaccessedFolder;
            }
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (treeList1.FocusedNode != null && treeList1.FocusedNode.StateImageIndex == 0)
                {
                    DirectoryInfo FS = new DirectoryInfo(GetPath(treeList1.FocusedNode));
                    lastaccessedFolder = FS.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbMediaInternal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbMediaInternal.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.MediaSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbMediaPCCard_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbMediaPCCard.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.MediaSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbMediaSDCard_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbMediaSDCard.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.MediaSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleAnalyser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleAnalyser.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleRecorder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleRecorder.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleDC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleDC.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleCC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleCC.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleRUCD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleRUCD.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleBalance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleBalance.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void rbModuleFRF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbModuleFRF.Checked)
                {
                    treeList2.ClearNodes();
                    _HaspInt.ModuleSelected(sender, e);
                    FillInstrumentTreelist();
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void InstrumentConnection_Load(object sender, EventArgs e)
        {
            try
            {
                _HaspInt.Key_serial = SerialFromKey;
                  
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        public bool _AccessGranted()
        {
            bool AccessGranted = false;
            try
            {
                _HaspInt.Key_serial = SerialFromKey;
                AccessGranted = _HaspInt.IsInstrumentAccessible();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            return AccessGranted;
        }
        private void treeList2_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            try
            {
                try
                {                    
                        e.Node.Nodes.Clear(); // clears all the nodes and...                   
                }
                catch (Exception ex)
                {
                    ErrorLog_Class.ErrorLogEntry(ex);
                }
                SelectedMedia = _HaspInt._SelectedMedia;
                SelectedModule = _HaspInt._SelectedModule;
                
                string[] nodeValues = null;

                if (e.Node.ParentNode == null)
                {
                    nodeValues = _HaspInt.AccessInstrument(SelectedMedia, SelectedModule);
                }
                else
                {
                    nodeValues = _HaspInt.AccessInstrument(SelectedMedia, SelectedModule, e.Node);
                }
                string[] InstrumentItem = _HaspInt._InstrumentData;

               // treeList2.ClearNodes();

                 // �-create the root node in treelist of Instrument�-

               
                //string sPath = "Mobile Device";

                //node = treeList2.AppendNode(new object[] { sPath }, null);
                //node.Tag = "Folder";
                //node.StateImageIndex = 0;

                ////node1 = treeList2.AppendNode(new object[] { "" }, node);

                //treeList2.FocusedNode = node;

               for (int i = 0; i < nodeValues.Length; i++)
               {
                   if(nodeValues[i].ToString().Contains('.')) //if (nodeValues[i].EndsWith(".csv", true, null) || nodeValues[i].EndsWith(".dat", true, null) || nodeValues[i].EndsWith(".wav", true, null) || nodeValues[i].EndsWith(".bal", true, null) || nodeValues[i].EndsWith(".ccs", true, null) || nodeValues[i].EndsWith(".ccr", true, null) || nodeValues[i].EndsWith(".cfg", true, null))
                   {
                       node1 = treeList2.AppendNode(new object[] { nodeValues[i] }, e.Node);
                       node1.Tag = InstrumentItem[i].ToString();// "File";
                       if (nodeValues[i].EndsWith(".csv", true, null))
                       {
                           node1.StateImageIndex = 1;
                       }
                       else if (nodeValues[i].EndsWith(".dat", true, null))
                       {
                           node1.StateImageIndex = 3;
                       }
                       else if (nodeValues[i].EndsWith(".wav", true, null))
                       {
                           node1.StateImageIndex = 2;
                       }
                       else
                       {
                           node1.StateImageIndex = 4;
                       }
                       
                   }
                   else
                   {
                       node1 = treeList2.AppendNode(new object[] { nodeValues[i] }, e.Node);
                       node1.Tag = InstrumentItem[i].ToString(); //"Folder";
                       node1.StateImageIndex = 0;

                       node = treeList2.AppendNode(new object[] { "" }, node1);
                   }
               }


               //for(FileInfo fileInfo in FS.GetFiles())
               //{
               //    if (fileInfo.Name.EndsWith(".csv", true, null))
               //    {
               //        node1 = treeList1.AppendNode(new object[] { fileInfo.Name }, parentNode);
               //        node1.Tag = "CSVFile";
               //        node1.StateImageIndex = 1;
               //    }
               //}
            }
            catch (Exception err)
            {
                ErrorLog_Class.ErrorLogEntry(err);
              MessageBoxEx.Show(err.Message);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo FS = new DirectoryInfo(GetPath(treeList1.FocusedNode));
                if (treeList1.FocusedNode != null)
                {
                    if (MessageBoxEx.Show("Do you want to create a directory at " + "\n" + treeList1.FocusedNode.GetDisplayText(0).ToString(), "Create Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmnewFolder _NewFolder = new frmnewFolder();
                        _NewFolder.ShowDialog();
                        string FolderName = _NewFolder._NewFolderName.ToString();
                        if (Directory.Exists(FS.FullName + FolderName))
                        {
                            MessageBoxEx.Show("Directory Already Exists");
                        }
                        else
                        {
                            Directory.CreateDirectory(FS.FullName + FolderName);
                            MessageBoxEx.Show("Created");
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }

        }
        
        private void reflectionImage1_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                }
                catch
                {
                }
               // timer1.Start();

                if (treeList2.FocusedNode != null)
                {
                    if (treeList1.FocusedNode != null && treeList1.FocusedNode.StateImageIndex == 0)
                    {
                        if (treeList2.FocusedNode.ParentNode != null)
                        {

                            DirectoryInfo FS = new DirectoryInfo(GetPath(treeList1.FocusedNode));

                            string[] OldFile = treeList2.FocusedNode.GetDisplayText(0).ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            string sNewFile = null;
                            for (int i = 0; i < OldFile.Length - 1; i++)
                            {
                                sNewFile += OldFile[i].ToString();
                            }
                            sNewFile += "." + OldFile[OldFile.Length - 1].ToString();

                            if (treeList2.FocusedNode.Tag.ToString().EndsWith("\\"))
                            {
                                _HaspInt.DownloadInstrumenttoSystem(treeList2.FocusedNode.Tag.ToString() + treeList2.FocusedNode.GetDisplayText(0).ToString(), FS.FullName.ToString() + sNewFile);
                            }
                            else
                            {
                                _HaspInt.DownloadInstrumenttoSystem(treeList2.FocusedNode.Tag.ToString() + "\\" + treeList2.FocusedNode.GetDisplayText(0).ToString(), FS.FullName.ToString() + sNewFile);
                            }
                            //timer1.Stop();

                            MessageBoxEx.Show("Download Process Complete");
                        }
                        else
                        {
                            DirectoryInfo FS = new DirectoryInfo(GetPath(treeList1.FocusedNode));

                            string[] OldFile = treeList2.FocusedNode.GetDisplayText(0).ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            string sNewFile = null;
                            for (int i = 0; i < OldFile.Length - 1; i++)
                            {
                                sNewFile += OldFile[i].ToString();
                            }
                            sNewFile += "." + OldFile[OldFile.Length - 1].ToString();
                            if (treeList2.FocusedNode.Tag != null)
                            {
                                if (treeList2.FocusedNode.Tag.ToString().EndsWith("\\"))
                                {
                                    _HaspInt.DownloadInstrumenttoSystem(treeList2.FocusedNode.Tag.ToString(), FS.FullName.ToString() + sNewFile);
                                }
                                else
                                {
                                    _HaspInt.DownloadInstrumenttoSystem(treeList2.FocusedNode.Tag.ToString(), FS.FullName.ToString() + sNewFile);
                                }
                                //timer1.Stop();

                                MessageBoxEx.Show("Download Process Complete");
                            }
                            else
                            {
                                MessageBoxEx.Show("No File/Folder Found to Download");
                                //MessageBoxEx.Show("Select a subfolder or file from the instrument");
                            }
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("Select a folder on Computer");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("Error in Downloading");
                ErrorLog_Class.ErrorLogEntry(ex);
            }
            try
            {
                this.Cursor = Cursors.Default;
            }
            catch
            {
            }
           // timer1.Stop();
            //reflectionImage1.Image = Resources.dl;
            //reflectionImage1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                
                reflectionImage1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                reflectionImage1.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        private void bbCancel_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                FolderBrowserDialog _Browse = new FolderBrowserDialog();
                _Browse.RootFolder = Environment.SpecialFolder.MyComputer;
                _Browse.ShowDialog();
               string sPath = _Browse.SelectedPath;

                if (string.IsNullOrEmpty(sPath))
                {
                    sPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                treeList1.Nodes.Clear();
                node = treeList1.AppendNode(new object[] { sPath }, null);
                node.Tag = "Folder";
                node.StateImageIndex = 0;

                node1 = treeList1.AppendNode(new object[] { "" }, node);

                treeList1.FocusedNode = node;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

     
    }
}
