namespace TMSI_MFLD 
{
    partial class ReportPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportPreview));
            this.previewControl1 = new FastReport.Preview.PreviewControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewControl1
            // 
            this.previewControl1.ActivePageBorderColor = System.Drawing.Color.White;
            this.previewControl1.BackColor = System.Drawing.Color.White;
            this.previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl1.Font = new System.Drawing.Font("宋体", 9F);
            this.previewControl1.Location = new System.Drawing.Point(0, 0);
            this.previewControl1.Name = "previewControl1";
            this.previewControl1.PageOffset = new System.Drawing.Point(10, 10);
            this.previewControl1.Size = new System.Drawing.Size(792, 745);
            this.previewControl1.TabIndex = 0;
            this.previewControl1.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.previewControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 745);
            this.panel2.TabIndex = 3;
            // 
            // ReportPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(800, 777);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Preview";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl previewControl1;
        private System.Windows.Forms.Panel panel2;
    }
}