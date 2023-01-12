
namespace TMSI_MFLD.Forms.ActionForms
{
    partial class Frm_PhotoReportUtility_JinYu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.conBut_exit = new MaterialSurface.ContainedButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv_PhotoReportUtility = new System.Windows.Forms.DataGridView();
            this.ucTextRect1 = new FormControls.WinControls.Text.UCTextRect();
            this.ucBtn_TN = new Sunny.UI.UIButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_excel = new MaterialSurface.ContainedButton();
            this.conBut_csv = new MaterialSurface.ContainedButton();
            this.conBut_remove = new MaterialSurface.ContainedButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_excel_qb = new MaterialSurface.ContainedButton();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PhotoReportUtility)).BeginInit();
            this.ucTextRect1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // conBut_exit
            // 
            this.conBut_exit.EffectType = MaterialSurface.ET.Custom;
            this.conBut_exit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conBut_exit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.conBut_exit.Icon = null;
            this.conBut_exit.Location = new System.Drawing.Point(852, 25);
            this.conBut_exit.MouseState = MaterialSurface.MouseState.OUT;
            this.conBut_exit.Name = "conBut_exit";
            this.conBut_exit.PrimaryColor = System.Drawing.Color.DimGray;
            this.conBut_exit.Radius = 6;
            this.conBut_exit.ShawdowDepth = 3;
            this.conBut_exit.ShawdowOpacity = 50;
            this.conBut_exit.Size = new System.Drawing.Size(136, 50);
            this.conBut_exit.TabIndex = 5;
            this.conBut_exit.Text = "Exit";
            this.conBut_exit.TextAlignment = System.Drawing.StringAlignment.Center;
            this.conBut_exit.UseVisualStyleBackColor = true;
            this.conBut_exit.Click += new System.EventHandler(this.conBut_exit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1167, 614);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_PhotoReportUtility);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1167, 614);
            this.panel4.TabIndex = 2;
            // 
            // dgv_PhotoReportUtility
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_PhotoReportUtility.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_PhotoReportUtility.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PhotoReportUtility.BackgroundColor = System.Drawing.Color.White;
            this.dgv_PhotoReportUtility.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_PhotoReportUtility.ColumnHeadersHeight = 36;
            this.dgv_PhotoReportUtility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_PhotoReportUtility.Location = new System.Drawing.Point(0, 0);
            this.dgv_PhotoReportUtility.Name = "dgv_PhotoReportUtility";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_PhotoReportUtility.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_PhotoReportUtility.RowTemplate.Height = 36;
            this.dgv_PhotoReportUtility.Size = new System.Drawing.Size(1167, 614);
            this.dgv_PhotoReportUtility.TabIndex = 0;
            this.dgv_PhotoReportUtility.SelectionChanged += new System.EventHandler(this.dgv_PhotoReportUtility_SelectionChanged);
            this.dgv_PhotoReportUtility.Resize += new System.EventHandler(this.dgv_PhotoReportUtility_SelectionChanged);
            // 
            // ucTextRect1
            // 
            this.ucTextRect1.BackColor = System.Drawing.Color.White;
            this.ucTextRect1.ConerRadius = 20;
            this.ucTextRect1.Controls.Add(this.ucBtn_TN);
            this.ucTextRect1.Controls.Add(this.textBox1);
            this.ucTextRect1.FillColor = System.Drawing.Color.Transparent;
            this.ucTextRect1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextRect1.IsRadius = true;
            this.ucTextRect1.IsShowRect = false;
            this.ucTextRect1.Location = new System.Drawing.Point(10, 33);
            this.ucTextRect1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucTextRect1.Name = "ucTextRect1";
            this.ucTextRect1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucTextRect1.RectWidth = 1;
            this.ucTextRect1.Size = new System.Drawing.Size(207, 35);
            this.ucTextRect1.TabIndex = 0;
            // 
            // ucBtn_TN
            // 
            this.ucBtn_TN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtn_TN.FillColor = System.Drawing.Color.DimGray;
            this.ucBtn_TN.FillDisableColor = System.Drawing.Color.DimGray;
            this.ucBtn_TN.FillHoverColor = System.Drawing.Color.Silver;
            this.ucBtn_TN.FillPressColor = System.Drawing.Color.Silver;
            this.ucBtn_TN.FillSelectedColor = System.Drawing.Color.DimGray;
            this.ucBtn_TN.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ucBtn_TN.Location = new System.Drawing.Point(156, -12);
            this.ucBtn_TN.MinimumSize = new System.Drawing.Size(1, 1);
            this.ucBtn_TN.Name = "ucBtn_TN";
            this.ucBtn_TN.RectColor = System.Drawing.Color.Transparent;
            this.ucBtn_TN.RectDisableColor = System.Drawing.Color.Transparent;
            this.ucBtn_TN.RectHoverColor = System.Drawing.Color.Transparent;
            this.ucBtn_TN.RectPressColor = System.Drawing.Color.Transparent;
            this.ucBtn_TN.RectSelectedColor = System.Drawing.Color.Transparent;
            this.ucBtn_TN.Size = new System.Drawing.Size(51, 50);
            this.ucBtn_TN.Style = Sunny.UI.UIStyle.Custom;
            this.ucBtn_TN.TabIndex = 135;
            this.ucBtn_TN.Text = "...";
            this.ucBtn_TN.Click += new System.EventHandler(this.ucBtn_TN_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBox1.ForeColor = System.Drawing.Color.Silver;
            this.textBox1.Location = new System.Drawing.Point(9, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(158, 17);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Test Number";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_excel
            // 
            this.btn_excel.BackColor = System.Drawing.Color.GhostWhite;
            this.btn_excel.EffectType = MaterialSurface.ET.Custom;
            this.btn_excel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_excel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_excel.Icon = null;
            this.btn_excel.Location = new System.Drawing.Point(384, 25);
            this.btn_excel.MouseState = MaterialSurface.MouseState.OUT;
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.PrimaryColor = System.Drawing.Color.DimGray;
            this.btn_excel.Radius = 6;
            this.btn_excel.ShawdowDepth = 3;
            this.btn_excel.ShawdowOpacity = 50;
            this.btn_excel.Size = new System.Drawing.Size(143, 50);
            this.btn_excel.TabIndex = 1;
            this.btn_excel.Text = "Export Report-GB";
            this.btn_excel.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btn_excel.UseVisualStyleBackColor = false;
            this.btn_excel.Click += new System.EventHandler(this.btn_excel_Click);
            // 
            // conBut_csv
            // 
            this.conBut_csv.BackColor = System.Drawing.Color.LightGray;
            this.conBut_csv.EffectType = MaterialSurface.ET.Custom;
            this.conBut_csv.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conBut_csv.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.conBut_csv.Icon = null;
            this.conBut_csv.Location = new System.Drawing.Point(701, 25);
            this.conBut_csv.MouseState = MaterialSurface.MouseState.OUT;
            this.conBut_csv.Name = "conBut_csv";
            this.conBut_csv.PrimaryColor = System.Drawing.Color.DimGray;
            this.conBut_csv.Radius = 6;
            this.conBut_csv.ShawdowDepth = 3;
            this.conBut_csv.ShawdowOpacity = 50;
            this.conBut_csv.Size = new System.Drawing.Size(136, 50);
            this.conBut_csv.TabIndex = 2;
            this.conBut_csv.Text = "Export RawDate";
            this.conBut_csv.TextAlignment = System.Drawing.StringAlignment.Center;
            this.conBut_csv.UseVisualStyleBackColor = false;
            this.conBut_csv.Click += new System.EventHandler(this.conBut_csv_Click);
            // 
            // conBut_remove
            // 
            this.conBut_remove.EffectType = MaterialSurface.ET.Custom;
            this.conBut_remove.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conBut_remove.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.conBut_remove.Icon = null;
            this.conBut_remove.Location = new System.Drawing.Point(233, 25);
            this.conBut_remove.MouseState = MaterialSurface.MouseState.OUT;
            this.conBut_remove.Name = "conBut_remove";
            this.conBut_remove.PrimaryColor = System.Drawing.Color.DimGray;
            this.conBut_remove.Radius = 6;
            this.conBut_remove.ShawdowDepth = 3;
            this.conBut_remove.ShawdowOpacity = 50;
            this.conBut_remove.Size = new System.Drawing.Size(136, 50);
            this.conBut_remove.TabIndex = 3;
            this.conBut_remove.Text = "Remove Row";
            this.conBut_remove.TextAlignment = System.Drawing.StringAlignment.Center;
            this.conBut_remove.UseVisualStyleBackColor = true;
            this.conBut_remove.Click += new System.EventHandler(this.conBut_remove_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.btn_excel_qb);
            this.panel1.Controls.Add(this.conBut_remove);
            this.panel1.Controls.Add(this.conBut_csv);
            this.panel1.Controls.Add(this.conBut_exit);
            this.panel1.Controls.Add(this.btn_excel);
            this.panel1.Controls.Add(this.ucTextRect1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 100);
            this.panel1.TabIndex = 0;
            // 
            // btn_excel_qb
            // 
            this.btn_excel_qb.BackColor = System.Drawing.Color.GhostWhite;
            this.btn_excel_qb.EffectType = MaterialSurface.ET.Custom;
            this.btn_excel_qb.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_excel_qb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_excel_qb.Icon = null;
            this.btn_excel_qb.Location = new System.Drawing.Point(542, 25);
            this.btn_excel_qb.MouseState = MaterialSurface.MouseState.OUT;
            this.btn_excel_qb.Name = "btn_excel_qb";
            this.btn_excel_qb.PrimaryColor = System.Drawing.Color.DimGray;
            this.btn_excel_qb.Radius = 6;
            this.btn_excel_qb.ShawdowDepth = 3;
            this.btn_excel_qb.ShawdowOpacity = 50;
            this.btn_excel_qb.Size = new System.Drawing.Size(144, 50);
            this.btn_excel_qb.TabIndex = 6;
            this.btn_excel_qb.Text = "Export Report-QB";
            this.btn_excel_qb.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btn_excel_qb.UseVisualStyleBackColor = false;
            this.btn_excel_qb.Click += new System.EventHandler(this.btn_excel_qb_Click);
            // 
            // Frm_PhotoReportUtility_JinYu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 714);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_PhotoReportUtility_JinYu";
            this.Text = "Frm_PhotoReportUtility";
            this.Load += new System.EventHandler(this.Frm_PhotoReportUtility_Load);
            this.Shown += new System.EventHandler(this.Frm_PhotoReportUtility_Shown);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PhotoReportUtility)).EndInit();
            this.ucTextRect1.ResumeLayout(false);
            this.ucTextRect1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private MaterialSurface.ContainedButton conBut_exit;
        private System.Windows.Forms.DataGridView dgv_PhotoReportUtility;
        private System.Windows.Forms.Panel panel4;
        private FormControls.WinControls.Text.UCTextRect ucTextRect1;
        private Sunny.UI.UIButton ucBtn_TN;
        private System.Windows.Forms.TextBox textBox1;
        private MaterialSurface.ContainedButton btn_excel;
        private MaterialSurface.ContainedButton conBut_csv;
        private MaterialSurface.ContainedButton conBut_remove;
        private System.Windows.Forms.Panel panel1;
        private MaterialSurface.ContainedButton btn_excel_qb;
    }
}