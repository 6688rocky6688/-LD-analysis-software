
namespace TMSI_MFLD.Forms.ActionForms
{
    partial class Frm_About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_About));
            this.ucBtn_Close = new TMSI_MFLD.Controls.Btn.UCBtnExtNew();
            this.ucControlBase1 = new FormControls.WinControls.UCControlBase();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_TmsiLogo = new System.Windows.Forms.PictureBox();
            this.ucControlBase1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_TmsiLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // ucBtn_Close
            // 
            this.ucBtn_Close.BackColor = System.Drawing.Color.Gray;
            this.ucBtn_Close.BtnBackColor = System.Drawing.Color.Gray;
            this.ucBtn_Close.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBtn_Close.BtnForeColor = System.Drawing.Color.White;
            this.ucBtn_Close.BtnText = "OK";
            this.ucBtn_Close.ConerRadius = 10;
            this.ucBtn_Close.FillColor = System.Drawing.Color.Transparent;
            this.ucBtn_Close.IsRadius = true;
            this.ucBtn_Close.IsShowRect = true;
            this.ucBtn_Close.Location = new System.Drawing.Point(105, 416);
            this.ucBtn_Close.Name = "ucBtn_Close";
            this.ucBtn_Close.RectColor = System.Drawing.Color.Silver;
            this.ucBtn_Close.RectWidth = 5;
            this.ucBtn_Close.Size = new System.Drawing.Size(152, 56);
            this.ucBtn_Close.TabIndex = 1;
            this.ucBtn_Close.BtnClick += new System.EventHandler(this.ucBtn_Close_BtnClick);
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.ConerRadius = 24;
            this.ucControlBase1.Controls.Add(this.pic_TmsiLogo);
            this.ucControlBase1.Controls.Add(this.linkLabel1);
            this.ucControlBase1.Controls.Add(this.label7);
            this.ucControlBase1.Controls.Add(this.label6);
            this.ucControlBase1.Controls.Add(this.label4);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = false;
            this.ucControlBase1.IsShowRect = true;
            this.ucControlBase1.Location = new System.Drawing.Point(0, 0);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.RectColor = System.Drawing.Color.White;
            this.ucControlBase1.RectWidth = 10;
            this.ucControlBase1.Size = new System.Drawing.Size(341, 377);
            this.ucControlBase1.TabIndex = 3;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.linkLabel1.ForeColor = System.Drawing.Color.White;
            this.linkLabel1.LinkVisited = true;
            this.linkLabel1.Location = new System.Drawing.Point(23, 295);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(278, 27);
            this.linkLabel1.TabIndex = 20;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Internet: www.tmsi-usa.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label7.Location = new System.Drawing.Point(22, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "Fax: 888-867-4872\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label6.Location = new System.Drawing.Point(22, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(309, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "Voice:888-TMSI-USA (867-4872)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label4.Location = new System.Drawing.Point(23, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(303, 66);
            this.label4.TabIndex = 6;
            this.label4.Text = "8817 PleaseantwoodAve.N.W.North \r\nCanton Ohio 44720\r\n\r\n";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.ucControlBase1);
            this.panel1.Location = new System.Drawing.Point(8, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 377);
            this.panel1.TabIndex = 4;
            // 
            // pic_TmsiLogo
            // 
            this.pic_TmsiLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_TmsiLogo.BackgroundImage")));
            this.pic_TmsiLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_TmsiLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pic_TmsiLogo.Location = new System.Drawing.Point(65, 14);
            this.pic_TmsiLogo.Name = "pic_TmsiLogo";
            this.pic_TmsiLogo.Size = new System.Drawing.Size(208, 86);
            this.pic_TmsiLogo.TabIndex = 24;
            this.pic_TmsiLogo.TabStop = false;
            // 
            // Frm_About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(358, 481);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucBtn_Close);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ucControlBase1.ResumeLayout(false);
            this.ucControlBase1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_TmsiLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.Btn.UCBtnExtNew ucBtn_Close;
        private FormControls.WinControls.UCControlBase ucControlBase1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pic_TmsiLogo;
    }
}