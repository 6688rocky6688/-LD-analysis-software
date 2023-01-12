
namespace TMSI_MFLD.Forms.ShowDialog
{
    partial class Frm_Sd_Excel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sd_Excel));
            this.lab_excel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_excel
            // 
            resources.ApplyResources(this.lab_excel, "lab_excel");
            this.lab_excel.Name = "lab_excel";
            this.lab_excel.TextChanged += new System.EventHandler(this.lab_excel_TextChanged);
            // 
            // Frm_Sd_Excel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnOkShow = true;
            this.Controls.Add(this.lab_excel);
            this.FrmName = "System Prompt";
            this.Name = "Frm_Sd_Excel";
            this.ThisCloseBtn = System.Drawing.Color.WhiteSmoke;
            this.ThisOkBtn = System.Drawing.Color.WhiteSmoke;
            this.Controls.SetChildIndex(this.lab_excel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_excel;
    }
}