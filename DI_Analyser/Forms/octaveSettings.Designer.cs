namespace iAdeptMain.AllForms
{
    partial class octaveSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(octaveSettings));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtN = new DevExpress.XtraEditors.TextEdit();
            this.lblN = new DevExpress.XtraEditors.LabelControl();
            this.cmbBarstyle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblBarstyle = new DevExpress.XtraEditors.LabelControl();
            this.cmbOctave = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblOctave = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBarstyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOctave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtN);
            this.groupControl1.Controls.Add(this.lblN);
            this.groupControl1.Controls.Add(this.cmbBarstyle);
            this.groupControl1.Controls.Add(this.lblBarstyle);
            this.groupControl1.Controls.Add(this.cmbOctave);
            this.groupControl1.Controls.Add(this.lblOctave);
            this.groupControl1.Location = new System.Drawing.Point(7, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(185, 101);
            this.groupControl1.TabIndex = 0;
            // 
            // txtN
            // 
            this.txtN.EditValue = "2";
            this.txtN.Location = new System.Drawing.Point(67, 47);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(40, 20);
            this.txtN.TabIndex = 5;
            this.txtN.Visible = false;
            // 
            // lblN
            // 
            this.lblN.Location = new System.Drawing.Point(44, 50);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(17, 13);
            this.lblN.TabIndex = 4;
            this.lblN.Text = "n =";
            this.lblN.Visible = false;
            // 
            // cmbBarstyle
            // 
            this.cmbBarstyle.EditValue = "Bars with Border";
            this.cmbBarstyle.Location = new System.Drawing.Point(56, 73);
            this.cmbBarstyle.Name = "cmbBarstyle";
            this.cmbBarstyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBarstyle.Properties.Items.AddRange(new object[] {
            "Bars with Border",
            "Empty Bars",
            "Filled bars",
            "Thick Lines",
            "Thin Lines"});
            this.cmbBarstyle.Size = new System.Drawing.Size(102, 20);
            this.cmbBarstyle.TabIndex = 3;
            // 
            // lblBarstyle
            // 
            this.lblBarstyle.Location = new System.Drawing.Point(6, 73);
            this.lblBarstyle.Name = "lblBarstyle";
            this.lblBarstyle.Size = new System.Drawing.Size(43, 13);
            this.lblBarstyle.TabIndex = 2;
            this.lblBarstyle.Text = "Bar Style";
            // 
            // cmbOctave
            // 
            this.cmbOctave.EditValue = "1";
            this.cmbOctave.Location = new System.Drawing.Point(92, 21);
            this.cmbOctave.Name = "cmbOctave";
            this.cmbOctave.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOctave.Properties.Items.AddRange(new object[] {
            "1",
            "3",
            "12",
            "n"});
            this.cmbOctave.Size = new System.Drawing.Size(51, 20);
            this.cmbOctave.TabIndex = 1;
            this.cmbOctave.SelectedIndexChanged += new System.EventHandler(this.cmbOctave_SelectedIndexChanged);
            // 
            // lblOctave
            // 
            this.lblOctave.Location = new System.Drawing.Point(6, 24);
            this.lblOctave.Name = "lblOctave";
            this.lblOctave.Size = new System.Drawing.Size(80, 13);
            this.lblOctave.TabIndex = 0;
            this.lblOctave.Text = "Octaves          1/";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(136, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(51, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(199, 138);
            this.panelControl1.TabIndex = 2;
            // 
            // octaveSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 138);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "octaveSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "octaveSettings";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBarstyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOctave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbBarstyle;
        private DevExpress.XtraEditors.LabelControl lblBarstyle;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOctave;
        private DevExpress.XtraEditors.LabelControl lblOctave;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txtN;
        private DevExpress.XtraEditors.LabelControl lblN;
        private DevExpress.XtraEditors.PanelControl panelControl1;

    }
}