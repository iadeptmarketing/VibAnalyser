namespace DI_Analyser.Forms
{
    partial class frmHasp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHasp));
            this.btnTryAgain = new DevExpress.XtraEditors.SimpleButton();
            this.btnInstallDrivers = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDemo = new DevExpress.XtraEditors.SimpleButton();
            this.lblPlz = new System.Windows.Forms.Label();
            this.lblInstallDrivers = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Location = new System.Drawing.Point(341, 12);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(75, 23);
            this.btnTryAgain.TabIndex = 1;
            this.btnTryAgain.Text = "Try";
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // btnInstallDrivers
            // 
            this.btnInstallDrivers.Location = new System.Drawing.Point(341, 48);
            this.btnInstallDrivers.Name = "btnInstallDrivers";
            this.btnInstallDrivers.Size = new System.Drawing.Size(75, 22);
            this.btnInstallDrivers.TabIndex = 2;
            this.btnInstallDrivers.Text = "Drivers";
            this.btnInstallDrivers.Visible = false;
            this.btnInstallDrivers.Click += new System.EventHandler(this.btnInstallDrivers_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(341, 88);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDemo
            // 
            this.btnDemo.Location = new System.Drawing.Point(341, 117);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Size = new System.Drawing.Size(75, 23);
            this.btnDemo.TabIndex = 4;
            this.btnDemo.Text = "Demo";
            this.btnDemo.Visible = false;
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // lblPlz
            // 
            this.lblPlz.AutoSize = true;
            this.lblPlz.Location = new System.Drawing.Point(135, 18);
            this.lblPlz.Name = "lblPlz";
            this.lblPlz.Size = new System.Drawing.Size(120, 13);
            this.lblPlz.TabIndex = 5;
            this.lblPlz.Text = "Insert key and press Try";
            // 
            // lblInstallDrivers
            // 
            this.lblInstallDrivers.AutoSize = true;
            this.lblInstallDrivers.Location = new System.Drawing.Point(135, 52);
            this.lblInstallDrivers.Name = "lblInstallDrivers";
            this.lblInstallDrivers.Size = new System.Drawing.Size(192, 13);
            this.lblInstallDrivers.TabIndex = 6;
            this.lblInstallDrivers.Text = "Click on Drivers Button to install Drivers";
            this.lblInstallDrivers.Visible = false;
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Location = new System.Drawing.Point(135, 95);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(116, 13);
            this.lblExit.TabIndex = 7;
            this.lblExit.Text = "Click Exit Button to Exit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Click on Demo";
            this.label1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 153);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(133, 139);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Status:";
            // 
            // frmHasp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 152);
            this.ControlBox = false;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.lblInstallDrivers);
            this.Controls.Add(this.lblPlz);
            this.Controls.Add(this.btnDemo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnInstallDrivers);
            this.Controls.Add(this.btnTryAgain);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHasp";
            this.Text = "Dongle Detection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmHasp_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.SimpleButton btnTryAgain;
        private DevExpress.XtraEditors.SimpleButton btnInstallDrivers;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnDemo;
        private System.Windows.Forms.Label lblPlz;
        private System.Windows.Forms.Label lblInstallDrivers;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;

    }
}