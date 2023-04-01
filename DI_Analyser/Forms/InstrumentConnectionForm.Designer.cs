namespace DI_Analyser.Forms
{
    partial class InstrumentConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrumentConnectionForm));
            this.bubbleBar1 = new DevComponents.DotNetBar.BubbleBar();
            this.bubbleBarTab1 = new DevComponents.DotNetBar.BubbleBarTab(this.components);
            this.bubbleButtonTour = new DevComponents.DotNetBar.BubbleButton();
            this.bubbleButtonCSV = new DevComponents.DotNetBar.BubbleButton();
            this.bubbleButtonWAV = new DevComponents.DotNetBar.BubbleButton();
            this.bubbleButtonDOWNLOAD = new DevComponents.DotNetBar.BubbleButton();
            this.rcPanel = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel();
            this.expandablePanelWAV = new DevComponents.DotNetBar.ExpandablePanel();
            this.listBoxWAV = new System.Windows.Forms.ListBox();
            this.expandablePanelCSV = new DevComponents.DotNetBar.ExpandablePanel();
            this.listBoxCSV = new System.Windows.Forms.ListBox();
            this.expandablePanelTour = new DevComponents.DotNetBar.ExpandablePanel();
            this.listBoxTour = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.bubbleBar1)).BeginInit();
            this.rcPanel.SuspendLayout();
            this.expandablePanelWAV.SuspendLayout();
            this.expandablePanelCSV.SuspendLayout();
            this.expandablePanelTour.SuspendLayout();
            this.SuspendLayout();
            // 
            // bubbleBar1
            // 
            this.bubbleBar1.Alignment = DevComponents.DotNetBar.eBubbleButtonAlignment.Bottom;
            this.bubbleBar1.AntiAlias = true;
            this.bubbleBar1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            // 
            // 
            // 
            this.bubbleBar1.ButtonBackAreaStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bubbleBar1.ButtonBackAreaStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderBottomWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bubbleBar1.ButtonBackAreaStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderLeftWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderRightWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderTopWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingBottom = 3;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingLeft = 3;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingRight = 3;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingTop = 3;
            this.bubbleBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bubbleBar1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.bubbleBar1.ImageSizeLarge = new System.Drawing.Size(200, 200);
            this.bubbleBar1.ImageSizeNormal = new System.Drawing.Size(50, 50);
            this.bubbleBar1.Location = new System.Drawing.Point(0, 498);
            this.bubbleBar1.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight;
            this.bubbleBar1.Name = "bubbleBar1";
            this.bubbleBar1.SelectedTab = this.bubbleBarTab1;
            this.bubbleBar1.SelectedTabColors.BorderColor = System.Drawing.Color.Black;
            this.bubbleBar1.Size = new System.Drawing.Size(599, 80);
            this.bubbleBar1.TabIndex = 0;
            this.bubbleBar1.Tabs.Add(this.bubbleBarTab1);
            this.bubbleBar1.Text = "bubbleBar1";
            // 
            // bubbleBarTab1
            // 
            this.bubbleBarTab1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.bubbleBarTab1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
            this.bubbleBarTab1.Buttons.AddRange(new DevComponents.DotNetBar.BubbleButton[] {
            this.bubbleButtonTour,
            this.bubbleButtonCSV,
            this.bubbleButtonWAV,
            this.bubbleButtonDOWNLOAD});
            this.bubbleBarTab1.DarkBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bubbleBarTab1.LightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bubbleBarTab1.Name = "bubbleBarTab1";
            this.bubbleBarTab1.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Blue;
            this.bubbleBarTab1.Text = "Download";
            this.bubbleBarTab1.TextColor = System.Drawing.Color.Black;
            // 
            // bubbleButtonTour
            // 
            this.bubbleButtonTour.Image = ((System.Drawing.Image)(resources.GetObject("bubbleButtonTour.Image")));
            this.bubbleButtonTour.ImageLarge = ((System.Drawing.Image)(resources.GetObject("bubbleButtonTour.ImageLarge")));
            this.bubbleButtonTour.Name = "bubbleButtonTour";
            this.bubbleButtonTour.TooltipText = "View Tours";
            this.bubbleButtonTour.Visible = false;
            this.bubbleButtonTour.Click += new DevComponents.DotNetBar.ClickEventHandler(this.bubbleButtonTour_Click);
            // 
            // bubbleButtonCSV
            // 
            this.bubbleButtonCSV.Image = ((System.Drawing.Image)(resources.GetObject("bubbleButtonCSV.Image")));
            this.bubbleButtonCSV.ImageLarge = ((System.Drawing.Image)(resources.GetObject("bubbleButtonCSV.ImageLarge")));
            this.bubbleButtonCSV.Name = "bubbleButtonCSV";
            this.bubbleButtonCSV.TooltipText = "View CSV Files";
            this.bubbleButtonCSV.Visible = false;
            this.bubbleButtonCSV.Click += new DevComponents.DotNetBar.ClickEventHandler(this.bubbleButtonCSV_Click);
            // 
            // bubbleButtonWAV
            // 
            this.bubbleButtonWAV.Image = ((System.Drawing.Image)(resources.GetObject("bubbleButtonWAV.Image")));
            this.bubbleButtonWAV.ImageLarge = ((System.Drawing.Image)(resources.GetObject("bubbleButtonWAV.ImageLarge")));
            this.bubbleButtonWAV.Name = "bubbleButtonWAV";
            this.bubbleButtonWAV.TooltipText = "View WAV Files";
            this.bubbleButtonWAV.Visible = false;
            this.bubbleButtonWAV.Click += new DevComponents.DotNetBar.ClickEventHandler(this.bubbleButtonWAV_Click);
            // 
            // bubbleButtonDOWNLOAD
            // 
            this.bubbleButtonDOWNLOAD.Image = ((System.Drawing.Image)(resources.GetObject("bubbleButtonDOWNLOAD.Image")));
            this.bubbleButtonDOWNLOAD.ImageLarge = ((System.Drawing.Image)(resources.GetObject("bubbleButtonDOWNLOAD.ImageLarge")));
            this.bubbleButtonDOWNLOAD.Name = "bubbleButtonDOWNLOAD";
            this.bubbleButtonDOWNLOAD.TooltipText = "Download Selected";
            this.bubbleButtonDOWNLOAD.Click += new DevComponents.DotNetBar.ClickEventHandler(this.bubbleButtonDOWNLOAD_Click);
            // 
            // rcPanel
            // 
            this.rcPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rcPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.rcPanel.Controls.Add(this.expandablePanelWAV);
            this.rcPanel.Controls.Add(this.expandablePanelCSV);
            this.rcPanel.Controls.Add(this.expandablePanelTour);
            this.rcPanel.Controls.Add(this.bubbleBar1);
            this.rcPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rcPanel.Location = new System.Drawing.Point(0, 0);
            this.rcPanel.Name = "rcPanel";
            this.rcPanel.Size = new System.Drawing.Size(599, 578);
            // 
            // 
            // 
            this.rcPanel.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.TopRight;
            this.rcPanel.Style.Class = "RibbonClientPanel";
            this.rcPanel.TabIndex = 1;
            this.rcPanel.Text = "ribbonClientPanel1";
            // 
            // expandablePanelWAV
            // 
            this.expandablePanelWAV.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanelWAV.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expandablePanelWAV.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanelWAV.Controls.Add(this.listBoxWAV);
            this.expandablePanelWAV.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandablePanelWAV.Location = new System.Drawing.Point(400, 0);
            this.expandablePanelWAV.Name = "expandablePanelWAV";
            this.expandablePanelWAV.Size = new System.Drawing.Size(200, 498);
            this.expandablePanelWAV.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelWAV.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelWAV.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelWAV.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanelWAV.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanelWAV.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanelWAV.Style.GradientAngle = 90;
            this.expandablePanelWAV.TabIndex = 3;
            this.expandablePanelWAV.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelWAV.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelWAV.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelWAV.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanelWAV.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanelWAV.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanelWAV.TitleStyle.GradientAngle = 90;
            this.expandablePanelWAV.TitleText = "<b>WAV</b> in Instrument";
            // 
            // listBoxWAV
            // 
            this.listBoxWAV.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listBoxWAV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxWAV.FormattingEnabled = true;
            this.listBoxWAV.Location = new System.Drawing.Point(0, 26);
            this.listBoxWAV.Name = "listBoxWAV";
            this.listBoxWAV.Size = new System.Drawing.Size(200, 472);
            this.listBoxWAV.TabIndex = 1;
            this.listBoxWAV.Enter += new System.EventHandler(this.listBoxWAV_Enter);
            // 
            // expandablePanelCSV
            // 
            this.expandablePanelCSV.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanelCSV.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expandablePanelCSV.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanelCSV.Controls.Add(this.listBoxCSV);
            this.expandablePanelCSV.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandablePanelCSV.Location = new System.Drawing.Point(200, 0);
            this.expandablePanelCSV.Name = "expandablePanelCSV";
            this.expandablePanelCSV.Size = new System.Drawing.Size(200, 498);
            this.expandablePanelCSV.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelCSV.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelCSV.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelCSV.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanelCSV.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanelCSV.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanelCSV.Style.GradientAngle = 90;
            this.expandablePanelCSV.TabIndex = 2;
            this.expandablePanelCSV.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelCSV.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelCSV.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelCSV.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanelCSV.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanelCSV.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanelCSV.TitleStyle.GradientAngle = 90;
            this.expandablePanelCSV.TitleText = "<b>CSV</b> in Instrument";
            // 
            // listBoxCSV
            // 
            this.listBoxCSV.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listBoxCSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCSV.FormattingEnabled = true;
            this.listBoxCSV.Location = new System.Drawing.Point(0, 26);
            this.listBoxCSV.Name = "listBoxCSV";
            this.listBoxCSV.Size = new System.Drawing.Size(200, 472);
            this.listBoxCSV.TabIndex = 1;
            this.listBoxCSV.Enter += new System.EventHandler(this.listBoxCSV_Enter);
            // 
            // expandablePanelTour
            // 
            this.expandablePanelTour.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanelTour.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expandablePanelTour.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanelTour.Controls.Add(this.listBoxTour);
            this.expandablePanelTour.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandablePanelTour.Location = new System.Drawing.Point(0, 0);
            this.expandablePanelTour.Name = "expandablePanelTour";
            this.expandablePanelTour.Size = new System.Drawing.Size(200, 498);
            this.expandablePanelTour.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelTour.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelTour.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelTour.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanelTour.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanelTour.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanelTour.Style.GradientAngle = 90;
            this.expandablePanelTour.TabIndex = 1;
            this.expandablePanelTour.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelTour.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelTour.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelTour.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanelTour.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanelTour.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanelTour.TitleStyle.GradientAngle = 90;
            this.expandablePanelTour.TitleText = "<b>Tours</b> in Instrument";
            // 
            // listBoxTour
            // 
            this.listBoxTour.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listBoxTour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTour.FormattingEnabled = true;
            this.listBoxTour.Location = new System.Drawing.Point(0, 26);
            this.listBoxTour.Name = "listBoxTour";
            this.listBoxTour.Size = new System.Drawing.Size(200, 472);
            this.listBoxTour.TabIndex = 1;
            this.listBoxTour.Enter += new System.EventHandler(this.listBoxTour_Enter);
            // 
            // InstrumentConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 578);
            this.Controls.Add(this.rcPanel);
            this.Name = "InstrumentConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instrument Connection";
            this.Load += new System.EventHandler(this.InstrumentConnectionForm_Load);
            this.SizeChanged += new System.EventHandler(this.InstrumentConnectionForm_SizeChanged);
            this.Shown += new System.EventHandler(this.InstrumentConnectionForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.bubbleBar1)).EndInit();
            this.rcPanel.ResumeLayout(false);
            this.expandablePanelWAV.ResumeLayout(false);
            this.expandablePanelCSV.ResumeLayout(false);
            this.expandablePanelTour.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.BubbleBar bubbleBar1;
        private DevComponents.DotNetBar.BubbleBarTab bubbleBarTab1;
        private DevComponents.DotNetBar.BubbleButton bubbleButtonTour;
        private DevComponents.DotNetBar.BubbleButton bubbleButtonCSV;
        private DevComponents.DotNetBar.BubbleButton bubbleButtonWAV;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel rcPanel;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanelWAV;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanelCSV;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanelTour;
        private DevComponents.DotNetBar.BubbleButton bubbleButtonDOWNLOAD;
        private System.Windows.Forms.ListBox listBoxCSV;
        private System.Windows.Forms.ListBox listBoxTour;
        private System.Windows.Forms.ListBox listBoxWAV;
    }
}