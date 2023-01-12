
namespace TMSI_MFLD.Forms.ShowDialog
{
    partial class Frm_FileName
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
            this.ucTextRect2 = new FormControls.WinControls.Text.UCTextRect();
            this.txtfilename = new System.Windows.Forms.TextBox();
            this.ucTextRect2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTextRect2
            // 
            this.ucTextRect2.BackColor = System.Drawing.Color.White;
            this.ucTextRect2.ConerRadius = 20;
            this.ucTextRect2.Controls.Add(this.txtfilename);
            this.ucTextRect2.FillColor = System.Drawing.Color.Transparent;
            this.ucTextRect2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextRect2.IsRadius = true;
            this.ucTextRect2.IsShowRect = false;
            this.ucTextRect2.Location = new System.Drawing.Point(78, 88);
            this.ucTextRect2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucTextRect2.Name = "ucTextRect2";
            this.ucTextRect2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucTextRect2.RectWidth = 1;
            this.ucTextRect2.Size = new System.Drawing.Size(276, 35);
            this.ucTextRect2.TabIndex = 12;
            // 
            // txtfilename
            // 
            this.txtfilename.BackColor = System.Drawing.Color.White;
            this.txtfilename.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtfilename.ForeColor = System.Drawing.Color.Black;
            this.txtfilename.Location = new System.Drawing.Point(7, 6);
            this.txtfilename.Name = "txtfilename";
            this.txtfilename.Size = new System.Drawing.Size(264, 23);
            this.txtfilename.TabIndex = 1;
            // 
            // Frm_FileName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnCloseShow = "Cancel";
            this.BtnOkName = "OK";
            this.BtnOkShow = true;
            this.ClientSize = new System.Drawing.Size(432, 241);
            this.Controls.Add(this.ucTextRect2);
            this.FrmName = "Please enter a file name";
            this.Name = "Frm_FileName";
            this.Text = "Frm_FileName";
            this.Controls.SetChildIndex(this.ucTextRect2, 0);
            this.ucTextRect2.ResumeLayout(false);
            this.ucTextRect2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FormControls.WinControls.Text.UCTextRect ucTextRect2;
        private System.Windows.Forms.TextBox txtfilename;
    }
}