namespace DI_Analyser.Forms
{
    partial class InstrumentConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrumentConnection));
            this.panelExBackground = new DevComponents.DotNetBar.PanelEx();
            this.panelExComputer = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.bbCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.reflectionImage1 = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.reflectionLabel4 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.panelExInstrument = new DevComponents.DotNetBar.PanelEx();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.reflectionLabel3 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.expandableSplitterInstrument = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelExModule = new DevComponents.DotNetBar.PanelEx();
            this.rbModuleFRF = new System.Windows.Forms.RadioButton();
            this.rbModuleBalance = new System.Windows.Forms.RadioButton();
            this.rbModuleDC = new System.Windows.Forms.RadioButton();
            this.rbModuleRUCD = new System.Windows.Forms.RadioButton();
            this.reflectionLabel2 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.rbModuleRecorder = new System.Windows.Forms.RadioButton();
            this.rbModuleCC = new System.Windows.Forms.RadioButton();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.rbModuleAnalyser = new System.Windows.Forms.RadioButton();
            this.panelExMedia = new DevComponents.DotNetBar.PanelEx();
            this.rbMediaSDCard = new System.Windows.Forms.RadioButton();
            this.rbMediaPCCard = new System.Windows.Forms.RadioButton();
            this.rbMediaInternal = new System.Windows.Forms.RadioButton();
            this.expandableSplitterMedia = new DevComponents.DotNetBar.ExpandableSplitter();
            this.reflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelExBackground.SuspendLayout();
            this.panelExComputer.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelExInstrument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.panelExModule.SuspendLayout();
            this.panelExMedia.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExBackground
            // 
            this.panelExBackground.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExBackground.Controls.Add(this.panelExComputer);
            this.panelExBackground.Controls.Add(this.panelExInstrument);
            this.panelExBackground.Controls.Add(this.panelExModule);
            this.panelExBackground.Controls.Add(this.panelExMedia);
            this.panelExBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExBackground.Location = new System.Drawing.Point(0, 0);
            this.panelExBackground.Name = "panelExBackground";
            this.panelExBackground.Size = new System.Drawing.Size(834, 633);
            this.panelExBackground.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExBackground.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelExBackground.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExBackground.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExBackground.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExBackground.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExBackground.Style.GradientAngle = 90;
            this.panelExBackground.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExBackground.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.panelExBackground.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.panelExBackground.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelExBackground.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelExBackground.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExBackground.StyleMouseOver.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
            this.panelExBackground.StyleMouseOver.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2;
            this.panelExBackground.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelExBackground.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelExBackground.TabIndex = 0;
            // 
            // panelExComputer
            // 
            this.panelExComputer.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExComputer.Controls.Add(this.panelEx3);
            this.panelExComputer.Controls.Add(this.panelEx2);
            this.panelExComputer.Controls.Add(this.panelEx1);
            this.panelExComputer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExComputer.Location = new System.Drawing.Point(527, 0);
            this.panelExComputer.Name = "panelExComputer";
            this.panelExComputer.Size = new System.Drawing.Size(307, 633);
            this.panelExComputer.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExComputer.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelExComputer.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelExComputer.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExComputer.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelExComputer.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelExComputer.Style.GradientAngle = 90;
            this.panelExComputer.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExComputer.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.panelExComputer.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.panelExComputer.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelExComputer.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelExComputer.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExComputer.StyleMouseOver.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
            this.panelExComputer.StyleMouseOver.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2;
            this.panelExComputer.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelExComputer.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelExComputer.TabIndex = 5;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.Controls.Add(this.treeList1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(91, 98);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(216, 535);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.panelEx3.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.panelEx3.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelEx3.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelEx3.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelEx3.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelEx3.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelEx3.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelEx3.TabIndex = 5;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(216, 535);
            this.treeList1.TabIndex = 2;
            this.treeList1.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.treeList1_BeforeExpand);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.FieldName = "treeListColumn1";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.AllowMove = false;
            this.treeListColumn1.OptionsColumn.AllowSort = false;
            this.treeListColumn1.OptionsColumn.FixedWidth = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.Controls.Add(this.bbCancel);
            this.panelEx2.Controls.Add(this.buttonX1);
            this.panelEx2.Controls.Add(this.reflectionImage1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 98);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(91, 535);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelEx2.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelEx2.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelEx2.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelEx2.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelEx2.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelEx2.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelEx2.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelEx2.TabIndex = 4;
            // 
            // bbCancel
            // 
            this.bbCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bbCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bbCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bbCancel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.bbCancel.Location = new System.Drawing.Point(0, 472);
            this.bbCancel.Margin = new System.Windows.Forms.Padding(1);
            this.bbCancel.Name = "bbCancel";
            this.bbCancel.Size = new System.Drawing.Size(91, 63);
            this.bbCancel.TabIndex = 4;
            this.bbCancel.Text = "<font size=\"15\" color=\"#ED1C24\"><b> Close</b></font>";
            this.bbCancel.Click += new System.EventHandler(this.bbCancel_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonX1.Location = new System.Drawing.Point(0, 149);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(91, 63);
            this.buttonX1.TabIndex = 3;
            this.buttonX1.Text = "Create<font color=\"#22B14C\"><b> New Folder</b></font>";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // reflectionImage1
            // 
            this.reflectionImage1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            // 
            // 
            // 
            this.reflectionImage1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.reflectionImage1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.reflectionImage1.BackgroundStyle.BackgroundImage = global::Analyser.Properties.Resources.dl;
            this.reflectionImage1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Double;
            this.reflectionImage1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Double;
            this.reflectionImage1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Double;
            this.reflectionImage1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Double;
            this.reflectionImage1.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionImage1.Location = new System.Drawing.Point(0, 0);
            this.reflectionImage1.Name = "reflectionImage1";
            this.reflectionImage1.Size = new System.Drawing.Size(91, 149);
            this.reflectionImage1.TabIndex = 1;
            this.reflectionImage1.Click += new System.EventHandler(this.reflectionImage1_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Controls.Add(this.reflectionLabel4);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(307, 98);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            // 
            // reflectionLabel4
            // 
            this.reflectionLabel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.reflectionLabel4.Location = new System.Drawing.Point(124, 0);
            this.reflectionLabel4.Name = "reflectionLabel4";
            this.reflectionLabel4.Size = new System.Drawing.Size(183, 98);
            this.reflectionLabel4.TabIndex = 0;
            this.reflectionLabel4.Text = "<b><font size=\"+6\"><i>Computer</i><font color=\"#B02B2C\">Explorer</font></font></b" +
    ">";
            // 
            // panelExInstrument
            // 
            this.panelExInstrument.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExInstrument.Controls.Add(this.treeList2);
            this.panelExInstrument.Controls.Add(this.reflectionLabel3);
            this.panelExInstrument.Controls.Add(this.expandableSplitterInstrument);
            this.panelExInstrument.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelExInstrument.Location = new System.Drawing.Point(347, 0);
            this.panelExInstrument.Name = "panelExInstrument";
            this.panelExInstrument.Size = new System.Drawing.Size(180, 633);
            this.panelExInstrument.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExInstrument.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelExInstrument.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelExInstrument.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExInstrument.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelExInstrument.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelExInstrument.Style.GradientAngle = 90;
            this.panelExInstrument.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExInstrument.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.panelExInstrument.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.panelExInstrument.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelExInstrument.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelExInstrument.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExInstrument.StyleMouseOver.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
            this.panelExInstrument.StyleMouseOver.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2;
            this.panelExInstrument.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelExInstrument.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelExInstrument.TabIndex = 4;
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList2.Location = new System.Drawing.Point(0, 98);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(177, 535);
            this.treeList2.TabIndex = 2;
            this.treeList2.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.treeList2_BeforeExpand);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.FieldName = "treeListColumn2";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.OptionsColumn.AllowMove = false;
            this.treeListColumn2.OptionsColumn.AllowSize = false;
            this.treeListColumn2.OptionsColumn.AllowSort = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // reflectionLabel3
            // 
            this.reflectionLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionLabel3.Location = new System.Drawing.Point(0, 0);
            this.reflectionLabel3.Name = "reflectionLabel3";
            this.reflectionLabel3.Size = new System.Drawing.Size(177, 98);
            this.reflectionLabel3.TabIndex = 1;
            this.reflectionLabel3.Text = "<b><font size=\"+6\"><i>Instrument</i><font color=\"#B02B2C\">Items</font></font></b>" +
    "";
            // 
            // expandableSplitterInstrument
            // 
            this.expandableSplitterInstrument.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterInstrument.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterInstrument.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitterInstrument.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitterInstrument.Enabled = false;
            this.expandableSplitterInstrument.ExpandableControl = this.panelExInstrument;
            this.expandableSplitterInstrument.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterInstrument.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterInstrument.ExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitterInstrument.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitterInstrument.GripDarkColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitterInstrument.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitterInstrument.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.expandableSplitterInstrument.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitterInstrument.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(142)))), ((int)(((byte)(75)))));
            this.expandableSplitterInstrument.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(139)))));
            this.expandableSplitterInstrument.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitterInstrument.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitterInstrument.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterInstrument.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterInstrument.HotExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitterInstrument.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitterInstrument.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterInstrument.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterInstrument.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.expandableSplitterInstrument.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitterInstrument.Location = new System.Drawing.Point(177, 0);
            this.expandableSplitterInstrument.Name = "expandableSplitterInstrument";
            this.expandableSplitterInstrument.Size = new System.Drawing.Size(3, 633);
            this.expandableSplitterInstrument.TabIndex = 0;
            this.expandableSplitterInstrument.TabStop = false;
            this.expandableSplitterInstrument.Visible = false;
            // 
            // panelExModule
            // 
            this.panelExModule.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExModule.Controls.Add(this.rbModuleFRF);
            this.panelExModule.Controls.Add(this.rbModuleBalance);
            this.panelExModule.Controls.Add(this.rbModuleDC);
            this.panelExModule.Controls.Add(this.rbModuleRUCD);
            this.panelExModule.Controls.Add(this.reflectionLabel2);
            this.panelExModule.Controls.Add(this.rbModuleRecorder);
            this.panelExModule.Controls.Add(this.rbModuleCC);
            this.panelExModule.Controls.Add(this.expandableSplitter1);
            this.panelExModule.Controls.Add(this.rbModuleAnalyser);
            this.panelExModule.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelExModule.Location = new System.Drawing.Point(165, 0);
            this.panelExModule.Name = "panelExModule";
            this.panelExModule.Size = new System.Drawing.Size(182, 633);
            this.panelExModule.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExModule.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelExModule.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelExModule.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExModule.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelExModule.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelExModule.Style.GradientAngle = 90;
            this.panelExModule.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExModule.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelExModule.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelExModule.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelExModule.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelExModule.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExModule.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelExModule.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelExModule.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelExModule.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelExModule.TabIndex = 3;
            // 
            // rbModuleFRF
            // 
            this.rbModuleFRF.AutoSize = true;
            this.rbModuleFRF.Location = new System.Drawing.Point(20, 470);
            this.rbModuleFRF.Name = "rbModuleFRF";
            this.rbModuleFRF.Size = new System.Drawing.Size(45, 17);
            this.rbModuleFRF.TabIndex = 4;
            this.rbModuleFRF.Text = "FRF";
            this.rbModuleFRF.UseVisualStyleBackColor = true;
            this.rbModuleFRF.Visible = false;
            this.rbModuleFRF.CheckedChanged += new System.EventHandler(this.rbModuleFRF_CheckedChanged);
            // 
            // rbModuleBalance
            // 
            this.rbModuleBalance.AutoSize = true;
            this.rbModuleBalance.Location = new System.Drawing.Point(21, 410);
            this.rbModuleBalance.Name = "rbModuleBalance";
            this.rbModuleBalance.Size = new System.Drawing.Size(72, 17);
            this.rbModuleBalance.TabIndex = 3;
            this.rbModuleBalance.Text = "Balancing";
            this.rbModuleBalance.UseVisualStyleBackColor = true;
            this.rbModuleBalance.Visible = true;
            this.rbModuleBalance.CheckedChanged += new System.EventHandler(this.rbModuleBalance_CheckedChanged);
            // 
            // rbModuleDC
            // 
            this.rbModuleDC.AutoSize = true;
            this.rbModuleDC.Location = new System.Drawing.Point(21, 230);
            this.rbModuleDC.Name = "rbModuleDC";
            this.rbModuleDC.Size = new System.Drawing.Size(95, 17);
            this.rbModuleDC.TabIndex = 3;
            this.rbModuleDC.Text = "Data Recorder";
            this.rbModuleDC.UseVisualStyleBackColor = true;
            this.rbModuleDC.CheckedChanged += new System.EventHandler(this.rbModuleDC_CheckedChanged);
            // 
            // rbModuleRUCD
            // 
            this.rbModuleRUCD.AutoSize = true;
            this.rbModuleRUCD.Location = new System.Drawing.Point(21, 350);
            this.rbModuleRUCD.Name = "rbModuleRUCD";
            this.rbModuleRUCD.Size = new System.Drawing.Size(56, 17);
            this.rbModuleRUCD.TabIndex = 2;
            this.rbModuleRUCD.Text = "RUCD";
            this.rbModuleRUCD.UseVisualStyleBackColor = true;
            this.rbModuleRUCD.Visible = false;
            this.rbModuleRUCD.CheckedChanged += new System.EventHandler(this.rbModuleRUCD_CheckedChanged);
            // 
            // reflectionLabel2
            // 
            this.reflectionLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionLabel2.Location = new System.Drawing.Point(0, 0);
            this.reflectionLabel2.Name = "reflectionLabel2";
            this.reflectionLabel2.Size = new System.Drawing.Size(179, 98);
            this.reflectionLabel2.TabIndex = 1;
            this.reflectionLabel2.Text = "<b><font size=\"+6\"><i>Module</i><font color=\"#B02B2C\">Selection</font></font></b>" +
    "";
            // 
            // rbModuleRecorder
            // 
            this.rbModuleRecorder.AutoSize = true;
            this.rbModuleRecorder.Location = new System.Drawing.Point(21, 170);
            this.rbModuleRecorder.Name = "rbModuleRecorder";
            this.rbModuleRecorder.Size = new System.Drawing.Size(134, 17);
            this.rbModuleRecorder.TabIndex = 2;
            this.rbModuleRecorder.Text = "Order Tracking Module";
            this.rbModuleRecorder.UseVisualStyleBackColor = true;
            this.rbModuleRecorder.CheckedChanged += new System.EventHandler(this.rbModuleRecorder_CheckedChanged);
            // 
            // rbModuleCC
            // 
            this.rbModuleCC.AutoSize = true;
            this.rbModuleCC.Location = new System.Drawing.Point(21, 290);
            this.rbModuleCC.Name = "rbModuleCC";
            this.rbModuleCC.Size = new System.Drawing.Size(122, 17);
            this.rbModuleCC.TabIndex = 1;
            this.rbModuleCC.Text = "Conformance Check";
            this.rbModuleCC.UseVisualStyleBackColor = true;
            this.rbModuleCC.Visible = false;
            this.rbModuleCC.CheckedChanged += new System.EventHandler(this.rbModuleCC_CheckedChanged);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitter1.Enabled = false;
            this.expandableSplitter1.ExpandableControl = this.panelExModule;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(142)))), ((int)(((byte)(75)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(139)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(179, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 633);
            this.expandableSplitter1.TabIndex = 0;
            this.expandableSplitter1.TabStop = false;
            this.expandableSplitter1.Visible = false;
            // 
            // rbModuleAnalyser
            // 
            this.rbModuleAnalyser.AutoSize = true;
            this.rbModuleAnalyser.Checked = true;
            this.rbModuleAnalyser.Location = new System.Drawing.Point(21, 110);
            this.rbModuleAnalyser.Name = "rbModuleAnalyser";
            this.rbModuleAnalyser.Size = new System.Drawing.Size(82, 17);
            this.rbModuleAnalyser.TabIndex = 1;
            this.rbModuleAnalyser.TabStop = true;
            this.rbModuleAnalyser.Text = "FFT Module";
            this.rbModuleAnalyser.UseVisualStyleBackColor = true;
            this.rbModuleAnalyser.CheckedChanged += new System.EventHandler(this.rbModuleAnalyser_CheckedChanged);
            // 
            // panelExMedia
            // 
            this.panelExMedia.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExMedia.Controls.Add(this.rbMediaSDCard);
            this.panelExMedia.Controls.Add(this.rbMediaPCCard);
            this.panelExMedia.Controls.Add(this.rbMediaInternal);
            this.panelExMedia.Controls.Add(this.expandableSplitterMedia);
            this.panelExMedia.Controls.Add(this.reflectionLabel1);
            this.panelExMedia.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelExMedia.Location = new System.Drawing.Point(0, 0);
            this.panelExMedia.Name = "panelExMedia";
            this.panelExMedia.Size = new System.Drawing.Size(165, 633);
            this.panelExMedia.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMedia.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelExMedia.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.panelExMedia.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExMedia.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelExMedia.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelExMedia.Style.GradientAngle = 90;
            this.panelExMedia.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMedia.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelExMedia.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelExMedia.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
            this.panelExMedia.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText;
            this.panelExMedia.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMedia.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.panelExMedia.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.panelExMedia.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder;
            this.panelExMedia.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText;
            this.panelExMedia.TabIndex = 2;
            // 
            // rbMediaSDCard
            // 
            this.rbMediaSDCard.AutoSize = true;
            this.rbMediaSDCard.Location = new System.Drawing.Point(13, 230);
            this.rbMediaSDCard.Name = "rbMediaSDCard";
            this.rbMediaSDCard.Size = new System.Drawing.Size(65, 17);
            this.rbMediaSDCard.TabIndex = 3;
            this.rbMediaSDCard.Text = "SD Card";
            this.rbMediaSDCard.UseVisualStyleBackColor = true;
            this.rbMediaSDCard.Visible = false;
            this.rbMediaSDCard.CheckedChanged += new System.EventHandler(this.rbMediaSDCard_CheckedChanged);
            // 
            // rbMediaPCCard
            // 
            this.rbMediaPCCard.AutoSize = true;
            this.rbMediaPCCard.Checked = true;
            this.rbMediaPCCard.Location = new System.Drawing.Point(13, 170);
            this.rbMediaPCCard.Name = "rbMediaPCCard";
            this.rbMediaPCCard.Size = new System.Drawing.Size(87, 17);
            this.rbMediaPCCard.TabIndex = 2;
            this.rbMediaPCCard.TabStop = true;
            this.rbMediaPCCard.Text = "Storage Card";
            this.rbMediaPCCard.UseVisualStyleBackColor = true;
            this.rbMediaPCCard.CheckedChanged += new System.EventHandler(this.rbMediaPCCard_CheckedChanged);
            // 
            // rbMediaInternal
            // 
            this.rbMediaInternal.AutoSize = true;
            this.rbMediaInternal.Location = new System.Drawing.Point(13, 110);
            this.rbMediaInternal.Name = "rbMediaInternal";
            this.rbMediaInternal.Size = new System.Drawing.Size(100, 17);
            this.rbMediaInternal.TabIndex = 1;
            this.rbMediaInternal.Text = "Internal Memory";
            this.rbMediaInternal.UseVisualStyleBackColor = true;
            this.rbMediaInternal.Visible = false;
            this.rbMediaInternal.CheckedChanged += new System.EventHandler(this.rbMediaInternal_CheckedChanged);
            // 
            // expandableSplitterMedia
            // 
            this.expandableSplitterMedia.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterMedia.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterMedia.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitterMedia.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitterMedia.Enabled = false;
            this.expandableSplitterMedia.ExpandableControl = this.panelExMedia;
            this.expandableSplitterMedia.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterMedia.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterMedia.ExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitterMedia.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitterMedia.GripDarkColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitterMedia.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitterMedia.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.expandableSplitterMedia.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitterMedia.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(142)))), ((int)(((byte)(75)))));
            this.expandableSplitterMedia.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(139)))));
            this.expandableSplitterMedia.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitterMedia.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitterMedia.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterMedia.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterMedia.HotExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.expandableSplitterMedia.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitterMedia.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.expandableSplitterMedia.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitterMedia.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.expandableSplitterMedia.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitterMedia.Location = new System.Drawing.Point(162, 98);
            this.expandableSplitterMedia.Name = "expandableSplitterMedia";
            this.expandableSplitterMedia.Size = new System.Drawing.Size(3, 535);
            this.expandableSplitterMedia.TabIndex = 0;
            this.expandableSplitterMedia.TabStop = false;
            this.expandableSplitterMedia.Visible = false;
            // 
            // reflectionLabel1
            // 
            this.reflectionLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionLabel1.Location = new System.Drawing.Point(0, 0);
            this.reflectionLabel1.Name = "reflectionLabel1";
            this.reflectionLabel1.Size = new System.Drawing.Size(165, 98);
            this.reflectionLabel1.TabIndex = 0;
            this.reflectionLabel1.Text = "<b><font size=\"+6\"><i>Media</i><font color=\"#B02B2C\">Selection</font></font></b>";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // InstrumentConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 633);
            this.Controls.Add(this.panelExBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InstrumentConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InstrumentConnection";
            this.Load += new System.EventHandler(this.InstrumentConnection_Load);
            this.Shown += new System.EventHandler(this.InstrumentConnection_Shown);
            this.panelExBackground.ResumeLayout(false);
            this.panelExComputer.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelExInstrument.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.panelExModule.ResumeLayout(false);
            this.panelExModule.PerformLayout();
            this.panelExMedia.ResumeLayout(false);
            this.panelExMedia.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelExBackground;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel1;
        private DevComponents.DotNetBar.PanelEx panelExMedia;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitterMedia;
        private DevComponents.DotNetBar.PanelEx panelExModule;
        private DevComponents.DotNetBar.PanelEx panelExInstrument;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel3;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitterInstrument;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel2;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelExComputer;
        private DevComponents.DotNetBar.Controls.ReflectionImage reflectionImage1;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel4;
        private System.Windows.Forms.RadioButton rbMediaSDCard;
        private System.Windows.Forms.RadioButton rbMediaPCCard;
        private System.Windows.Forms.RadioButton rbMediaInternal;
        private System.Windows.Forms.RadioButton rbModuleDC;
        private System.Windows.Forms.RadioButton rbModuleRecorder;
        private System.Windows.Forms.RadioButton rbModuleAnalyser;
        private System.Windows.Forms.RadioButton rbModuleFRF;
        private System.Windows.Forms.RadioButton rbModuleBalance;
        private System.Windows.Forms.RadioButton rbModuleRUCD;
        private System.Windows.Forms.RadioButton rbModuleCC;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevComponents.DotNetBar.ButtonX bbCancel;
        

    }
}