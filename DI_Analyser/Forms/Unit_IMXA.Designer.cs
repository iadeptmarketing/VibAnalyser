namespace Analyser.AllForms
{
    partial class Unit_IMXA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Unit_IMXA));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.cmbUnits = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rbUnit = new System.Windows.Forms.RadioButton();
            this.rbFormula = new System.Windows.Forms.RadioButton();
            this.tbFormula = new System.Windows.Forms.TextBox();
            this.cmbFExpression = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFormulaUnit = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rbConvertTo = new System.Windows.Forms.RadioButton();
            this.cmbConvertTo = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFExpression.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConvertTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(291, 98);
            this.btnCancel.LookAndFeel.SkinName = "iMaginary";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(224, 98);
            this.btnUpdate.LookAndFeel.SkinName = "iMaginary";
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbUnits
            // 
            this.cmbUnits.Location = new System.Drawing.Point(127, 37);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnits.Properties.LookAndFeel.SkinName = "iMaginary";
            this.cmbUnits.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbUnits.Size = new System.Drawing.Size(224, 20);
            this.cmbUnits.TabIndex = 3;
            // 
            // rbUnit
            // 
            this.rbUnit.AutoSize = true;
            this.rbUnit.Checked = true;
            this.rbUnit.Location = new System.Drawing.Point(13, 38);
            this.rbUnit.Name = "rbUnit";
            this.rbUnit.Size = new System.Drawing.Size(97, 17);
            this.rbUnit.TabIndex = 7;
            this.rbUnit.TabStop = true;
            this.rbUnit.Text = "Change to Unit";
            this.rbUnit.UseVisualStyleBackColor = true;
            this.rbUnit.CheckedChanged += new System.EventHandler(this.rbUnit_CheckedChanged);
            // 
            // rbFormula
            // 
            this.rbFormula.AutoSize = true;
            this.rbFormula.Location = new System.Drawing.Point(14, 63);
            this.rbFormula.Name = "rbFormula";
            this.rbFormula.Size = new System.Drawing.Size(70, 17);
            this.rbFormula.TabIndex = 8;
            this.rbFormula.Text = "Formulize";
            this.rbFormula.UseVisualStyleBackColor = true;
            this.rbFormula.CheckedChanged += new System.EventHandler(this.rbFormula_CheckedChanged);
            // 
            // tbFormula
            // 
            this.tbFormula.Enabled = false;
            this.tbFormula.Location = new System.Drawing.Point(187, 63);
            this.tbFormula.Name = "tbFormula";
            this.tbFormula.Size = new System.Drawing.Size(104, 20);
            this.tbFormula.TabIndex = 9;
            // 
            // cmbFExpression
            // 
            this.cmbFExpression.Enabled = false;
            this.cmbFExpression.Location = new System.Drawing.Point(127, 62);
            this.cmbFExpression.Name = "cmbFExpression";
            this.cmbFExpression.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFExpression.Properties.Items.AddRange(new object[] {
            "+",
            "-",
            "*",
            "/"});
            this.cmbFExpression.Properties.LookAndFeel.SkinName = "iMaginary";
            this.cmbFExpression.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFExpression.Size = new System.Drawing.Size(41, 20);
            this.cmbFExpression.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "(";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = ")";
            // 
            // tbFormulaUnit
            // 
            // 
            // 
            // 
            this.tbFormulaUnit.Border.Class = "TextBoxBorder";
            this.tbFormulaUnit.Enabled = false;
            this.tbFormulaUnit.Location = new System.Drawing.Point(308, 62);
            this.tbFormulaUnit.Name = "tbFormulaUnit";
            this.tbFormulaUnit.Size = new System.Drawing.Size(43, 21);
            this.tbFormulaUnit.TabIndex = 11;
            this.tbFormulaUnit.WatermarkText = "Unit";
            // 
            // rbConvertTo
            // 
            this.rbConvertTo.AutoSize = true;
            this.rbConvertTo.Location = new System.Drawing.Point(14, 12);
            this.rbConvertTo.Name = "rbConvertTo";
            this.rbConvertTo.Size = new System.Drawing.Size(79, 17);
            this.rbConvertTo.TabIndex = 12;
            this.rbConvertTo.Text = "Convert To";
            this.rbConvertTo.UseVisualStyleBackColor = true;
            this.rbConvertTo.Visible = false;
            this.rbConvertTo.CheckedChanged += new System.EventHandler(this.rbConvertTo_CheckedChanged);
            // 
            // cmbConvertTo
            // 
            this.cmbConvertTo.Enabled = false;
            this.cmbConvertTo.Location = new System.Drawing.Point(127, 12);
            this.cmbConvertTo.Name = "cmbConvertTo";
            this.cmbConvertTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbConvertTo.Properties.Items.AddRange(new object[] {
            "Acceleration (g)",
            "Acceleration (mm/s2)",
            "Acceleration (cm/s2)",
            "Acceleration (m/s2)",
            "Velocity (IPS)",
            "Velocity (mm/s)",
            "Velocity (cm/s)",
            "Velocity (m/s)",
            "Velocity (ft/s)",
            "Displacement (Mils)",
            "Displacement (mm)",
            "Displacement (um)"});
            this.cmbConvertTo.Properties.LookAndFeel.SkinName = "iMaginary";
            this.cmbConvertTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbConvertTo.Size = new System.Drawing.Size(224, 20);
            this.cmbConvertTo.TabIndex = 13;
            this.cmbConvertTo.Visible = false;
            // 
            // Unit_IMXA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 125);
            this.Controls.Add(this.cmbConvertTo);
            this.Controls.Add(this.rbConvertTo);
            this.Controls.Add(this.tbFormulaUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFormula);
            this.Controls.Add(this.rbFormula);
            this.Controls.Add(this.rbUnit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cmbFExpression);
            this.Controls.Add(this.cmbUnits);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Unit_IMXA";
            this.ShowInTaskbar = false;
            this.Text = "Unit_IMXA";
            this.Shown += new System.EventHandler(this.Unit_IMXA_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFExpression.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConvertTo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.ComboBoxEdit cmbUnits;
        private System.Windows.Forms.RadioButton rbUnit;
        private System.Windows.Forms.RadioButton rbFormula;
        private System.Windows.Forms.TextBox tbFormula;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbFormulaUnit;
        private System.Windows.Forms.RadioButton rbConvertTo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbConvertTo;
    }
}