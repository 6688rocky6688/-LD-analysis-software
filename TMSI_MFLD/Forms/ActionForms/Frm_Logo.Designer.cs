
namespace TMSI_MFLD.Forms.ActionForms
{
    partial class Frm_Logo
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
            ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem imageItem1 = new ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem();
            ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem imageItem2 = new ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem();
            ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem imageItem3 = new ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem();
            ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem imageItem4 = new ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem();
            ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem imageItem5 = new ExtensionControls.ExtensionCtrls.ImageWhirligigExt.ImageItem();
            this.imageWhirligigExt1 = new ExtensionControls.ExtensionCtrls.ImageWhirligigExt();
            this.SuspendLayout();
            // 
            // imageWhirligigExt1
            // 
            this.imageWhirligigExt1.BackColor = System.Drawing.Color.White;
            this.imageWhirligigExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.imageWhirligigExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageWhirligigExt1.ImageFrameHeight = 700;
            this.imageWhirligigExt1.ImageFrameWidth = 1037;
            imageItem1.Image = global::TMSI_MFLD.Properties.Resources.TMSI_Background;
            imageItem1.Text = null;
            imageItem2.Image = global::TMSI_MFLD.Properties.Resources._39e461e300c4481a_jpg_20220309160501_1920x0;
            imageItem2.Text = null;
            imageItem3.Image = global::TMSI_MFLD.Properties.Resources._68283c9821a5ba19_jpg_20200218114413_1920x0;
            imageItem3.Text = null;
            imageItem4.Image = global::TMSI_MFLD.Properties.Resources.ca0bc3b29f345d5e_png_20211029144915_1920x0;
            imageItem4.Text = null;
            imageItem5.Image = global::TMSI_MFLD.Properties.Resources.de385ee8a77a3834_png_20211029111252_1920x0;
            imageItem5.Text = null;
            this.imageWhirligigExt1.Images.Add(imageItem1);
            this.imageWhirligigExt1.Images.Add(imageItem2);
            this.imageWhirligigExt1.Images.Add(imageItem3);
            this.imageWhirligigExt1.Images.Add(imageItem4);
            this.imageWhirligigExt1.Images.Add(imageItem5);
            this.imageWhirligigExt1.Location = new System.Drawing.Point(0, 0);
            this.imageWhirligigExt1.Name = "imageWhirligigExt1";
            this.imageWhirligigExt1.Size = new System.Drawing.Size(1037, 700);
            this.imageWhirligigExt1.TabIndex = 0;
            this.imageWhirligigExt1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageWhirligigExt1_Paint);
            // 
            // Frm_Logo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1037, 700);
            this.Controls.Add(this.imageWhirligigExt1);
            this.Name = "Frm_Logo";
            this.Text = "Logo";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtensionControls.ExtensionCtrls.ImageWhirligigExt imageWhirligigExt1;
    }
}