using com.iAM.chart2dnet;
namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.chartView1 = new ChartView();
            this.chartView2 = new ChartView();
            this.SuspendLayout();
            // 
            // chartView1
            // 
            this.chartView1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chartView1.BackgroundDrawEnable = true;
            this.chartView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartView1.DoubleBufferEnable = true;
            this.chartView1.Location = new System.Drawing.Point(0, 0);
            this.chartView1.Name = "chartView1";
            this.chartView1.PreferredSize = new System.Drawing.Size(424, 272);
            this.chartView1.PreRenderingMode = 2;
            this.chartView1.RenderingMode = 0;
            this.chartView1.ResizeMode = 1;
            this.chartView1.Size = new System.Drawing.Size(599, 272);
            this.chartView1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.chartView1.TabIndex = 0;
            this.chartView1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.chartView1.ZOrderSortEnable = true;
            this.chartView1.Paint += new System.Windows.Forms.PaintEventHandler(this.chartView1_Paint);
            // 
            // chartView2
            // 
            this.chartView2.BackColor = System.Drawing.Color.MistyRose;
            this.chartView2.BackgroundDrawEnable = true;
            this.chartView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartView2.DoubleBufferEnable = true;
            this.chartView2.Location = new System.Drawing.Point(0, 272);
            this.chartView2.Name = "chartView2";
            this.chartView2.PreferredSize = new System.Drawing.Size(424, 272);
            this.chartView2.PreRenderingMode = 2;
            this.chartView2.RenderingMode = 0;
            this.chartView2.ResizeMode = 1;
            this.chartView2.Size = new System.Drawing.Size(599, 235);
            this.chartView2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.chartView2.TabIndex = 1;
            this.chartView2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.chartView2.ZOrderSortEnable = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 507);
            this.Controls.Add(this.chartView2);
            this.Controls.Add(this.chartView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ChartView chartView1;
        private ChartView chartView2;
    }
}

