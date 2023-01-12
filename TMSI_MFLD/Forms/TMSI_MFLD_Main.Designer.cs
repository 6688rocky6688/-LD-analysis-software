
namespace TMSI_MFLD.Forms
{
    partial class TMSI_MFLD_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMSI_MFLD_Main));
            this.pan_head = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pic_TmsiLogo = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pan_Frms = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.leftTask = new FormControls.WinControls.Treeview.TreeViewEx();
            this.pan_head.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_TmsiLogo)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_head
            // 
            this.pan_head.BackColor = System.Drawing.Color.White;
            this.pan_head.Controls.Add(this.panel3);
            this.pan_head.Controls.Add(this.pic_TmsiLogo);
            resources.ApplyResources(this.pan_head, "pan_head");
            this.pan_head.Name = "pan_head";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // pic_TmsiLogo
            // 
            resources.ApplyResources(this.pic_TmsiLogo, "pic_TmsiLogo");
            this.pic_TmsiLogo.Name = "pic_TmsiLogo";
            this.pic_TmsiLogo.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pan_Frms);
            this.panel4.Controls.Add(this.panel5);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // pan_Frms
            // 
            resources.ApplyResources(this.pan_Frms, "pan_Frms");
            this.pan_Frms.Name = "pan_Frms";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.leftTask);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // leftTask
            // 
            this.leftTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(43)))), ((int)(((byte)(51)))));
            this.leftTask.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.leftTask.Click = false;
            resources.ApplyResources(this.leftTask, "leftTask");
            this.leftTask.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.leftTask.FullRowSelect = true;
            this.leftTask.HideSelection = false;
            this.leftTask.IsShowByCustomModel = true;
            this.leftTask.IsShowTip = false;
            this.leftTask.ItemHeight = 50;
            this.leftTask.LstTips = null;
            this.leftTask.Name = "leftTask";
            this.leftTask.NodeBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(43)))), ((int)(((byte)(51)))));
            this.leftTask.NodeDownPic = global::TMSI_MFLD.Properties.Resources.list_down;
            this.leftTask.NodeForeColor = System.Drawing.Color.White;
            this.leftTask.NodeHeight = 50;
            this.leftTask.NodeIsShowSplitLine = false;
            this.leftTask.NodeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.leftTask.NodeSelectedForeColor = System.Drawing.Color.White;
            this.leftTask.NodeSplitLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.leftTask.NodeUpPic = global::TMSI_MFLD.Properties.Resources.list_up;
            this.leftTask.ParentNodeCanSelect = true;
            this.leftTask.ShowLines = false;
            this.leftTask.ShowPlusMinus = false;
            this.leftTask.ShowRootLines = false;
            this.leftTask.TipFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.leftTask.TipImage = null;
            this.leftTask.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.left_Taskbar_AfterSelect);
            // 
            // TMSI_MFLD_Main
            // 
            this.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pan_head);
            this.InnerBorderColor = System.Drawing.Color.Transparent;
            this.Name = "TMSI_MFLD_Main";
            this.Load += new System.EventHandler(this.TMSI_MFLD_Main_Load);
            this.Shown += new System.EventHandler(this.TMSI_MFLD_Main_Shown);
            this.pan_head.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_TmsiLogo)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pic_TmsiLogo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pan_Frms;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Panel pan_head;
        private System.Windows.Forms.Panel panel3;
        private FormControls.WinControls.Treeview.TreeViewEx leftTask;
    }
}