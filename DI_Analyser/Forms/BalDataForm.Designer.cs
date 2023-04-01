namespace Analyser.Forms
{
    partial class BalDataForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.dataGridViewWts = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ColTitleWt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRadius = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabItemWts = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.dataGridViewVib = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ColTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPhase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabItemVib = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWts)).BeginInit();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVib)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(440, 522);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItemVib);
            this.tabControl1.Tabs.Add(this.tabItemWts);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.dataGridViewWts);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 22);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(440, 500);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItemWts;
            // 
            // dataGridViewWts
            // 
            this.dataGridViewWts.AllowUserToDeleteRows = false;
            this.dataGridViewWts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTitleWt,
            this.ColMass,
            this.ColAngle,
            this.ColRadius});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewWts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewWts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewWts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewWts.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewWts.Name = "dataGridViewWts";
            this.dataGridViewWts.ReadOnly = true;
            this.dataGridViewWts.Size = new System.Drawing.Size(438, 498);
            this.dataGridViewWts.TabIndex = 0;
            // 
            // ColTitleWt
            // 
            this.ColTitleWt.HeaderText = "Title";
            this.ColTitleWt.Name = "ColTitleWt";
            this.ColTitleWt.ReadOnly = true;
            this.ColTitleWt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColMass
            // 
            this.ColMass.HeaderText = "Mass";
            this.ColMass.Name = "ColMass";
            this.ColMass.ReadOnly = true;
            this.ColMass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColAngle
            // 
            this.ColAngle.HeaderText = "Angle";
            this.ColAngle.Name = "ColAngle";
            this.ColAngle.ReadOnly = true;
            this.ColAngle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColRadius
            // 
            this.ColRadius.HeaderText = "Radius";
            this.ColRadius.Name = "ColRadius";
            this.ColRadius.ReadOnly = true;
            this.ColRadius.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabItemWts
            // 
            this.tabItemWts.AttachedControl = this.tabControlPanel2;
            this.tabItemWts.Name = "tabItemWts";
            this.tabItemWts.Text = "Weights Summary Table";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.dataGridViewVib);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 22);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(440, 500);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItemVib;
            // 
            // dataGridViewVib
            // 
            this.dataGridViewVib.AllowUserToDeleteRows = false;
            this.dataGridViewVib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVib.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTitle,
            this.ColMag,
            this.ColPhase});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewVib.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewVib.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewVib.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewVib.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewVib.Name = "dataGridViewVib";
            this.dataGridViewVib.ReadOnly = true;
            this.dataGridViewVib.Size = new System.Drawing.Size(438, 498);
            this.dataGridViewVib.TabIndex = 0;
            // 
            // ColTitle
            // 
            this.ColTitle.HeaderText = "Title";
            this.ColTitle.Name = "ColTitle";
            this.ColTitle.ReadOnly = true;
            this.ColTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColTitle.Width = 130;
            // 
            // ColMag
            // 
            this.ColMag.HeaderText = "Mag";
            this.ColMag.Name = "ColMag";
            this.ColMag.ReadOnly = true;
            this.ColMag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColMag.Width = 130;
            // 
            // ColPhase
            // 
            this.ColPhase.HeaderText = "Phase";
            this.ColPhase.Name = "ColPhase";
            this.ColPhase.ReadOnly = true;
            this.ColPhase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColPhase.Width = 130;
            // 
            // tabItemVib
            // 
            this.tabItemVib.AttachedControl = this.tabControlPanel1;
            this.tabItemVib.Name = "tabItemVib";
            this.tabItemVib.Text = "Vibration Summary Table";
            // 
            // BalDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 522);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BalDataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balancing Data";
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWts)).EndInit();
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVib)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItemVib;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItemWts;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewVib;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewWts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTitleWt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAngle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRadius;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMag;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPhase;
    }
}