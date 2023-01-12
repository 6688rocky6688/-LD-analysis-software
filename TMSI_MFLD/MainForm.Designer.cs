namespace TMSI_MFLD 
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.uiTabControlMenu1 = new Sunny.UI.UITabControlMenu();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.txtDataPath = new Sunny.UI.UITextBox();
            this.btnLoadRawData = new Sunny.UI.UIButton();
            this.btnAyalyseData = new Sunny.UI.UIButton();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.chtTestPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grbTestConditions = new System.Windows.Forms.GroupBox();
            this.txtTargetInflation = new Sunny.UI.UITextBox();
            this.txtTireInflation = new Sunny.UI.UITextBox();
            this.txtTireLoad = new Sunny.UI.UITextBox();
            this.lblTireLoad = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtVaz = new Sunny.UI.UITextBox();
            this.txtTargetDeflection = new Sunny.UI.UITextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtTireSize = new Sunny.UI.UITextBox();
            this.txtVxy = new Sunny.UI.UITextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtVz = new Sunny.UI.UITextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtTargetLoad = new Sunny.UI.UITextBox();
            this.txtTestDate = new Sunny.UI.UITextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTireDia = new Sunny.UI.UITextBox();
            this.txtTestType = new Sunny.UI.UITextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTestID = new Sunny.UI.UITextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.uiComboBox22 = new Sunny.UI.UIComboBox();
            this.uiComboBox24 = new Sunny.UI.UIComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiComboBox1 = new Sunny.UI.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new Sunny.UI.UIButton();
            this.cb01 = new Sunny.UI.UIComboBox();
            this.uiComboBox21 = new Sunny.UI.UIComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSave = new Sunny.UI.UIButton();
            this.cb02 = new Sunny.UI.UIComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.uiButton5 = new Sunny.UI.UIButton();
            this.uiButton4 = new Sunny.UI.UIButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.uiTabControlMenu1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtTestPlot)).BeginInit();
            this.grbTestConditions.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTabControlMenu1
            // 
            this.uiTabControlMenu1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.uiTabControlMenu1.Controls.Add(this.tabPage1);
            this.uiTabControlMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControlMenu1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControlMenu1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControlMenu1.ItemSize = new System.Drawing.Size(80, 280);
            this.uiTabControlMenu1.Location = new System.Drawing.Point(0, 0);
            this.uiTabControlMenu1.Multiline = true;
            this.uiTabControlMenu1.Name = "uiTabControlMenu1";
            this.uiTabControlMenu1.SelectedIndex = 0;
            this.uiTabControlMenu1.Size = new System.Drawing.Size(1640, 907);
            this.uiTabControlMenu1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControlMenu1.TabIndex = 6;
            this.uiTabControlMenu1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.uiButton3);
            this.tabPage1.Controls.Add(this.chtTestPlot);
            this.tabPage1.Controls.Add(this.grbTestConditions);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.uiButton5);
            this.tabPage1.Controls.Add(this.uiButton4);
            this.tabPage1.Location = new System.Drawing.Point(281, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1359, 907);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Analysis";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.uiButton1);
            this.groupBox2.Controls.Add(this.txtDataPath);
            this.groupBox2.Controls.Add(this.btnLoadRawData);
            this.groupBox2.Controls.Add(this.btnAyalyseData);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 457);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1359, 95);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SelectFile";
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(1175, 31);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(138, 38);
            this.uiButton1.TabIndex = 129;
            this.uiButton1.Text = "AnalyseData";
            this.uiButton1.Click += new System.EventHandler(this.uiButton2_Click_1);
            // 
            // txtDataPath
            // 
            this.txtDataPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDataPath.FillColor = System.Drawing.Color.White;
            this.txtDataPath.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDataPath.Location = new System.Drawing.Point(16, 31);
            this.txtDataPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDataPath.Maximum = 2147483647D;
            this.txtDataPath.Minimum = -2147483648D;
            this.txtDataPath.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtDataPath.Name = "txtDataPath";
            this.txtDataPath.Size = new System.Drawing.Size(858, 38);
            this.txtDataPath.TabIndex = 17;
            this.txtDataPath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLoadRawData
            // 
            this.btnLoadRawData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadRawData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnLoadRawData.Location = new System.Drawing.Point(885, 31);
            this.btnLoadRawData.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLoadRawData.Name = "btnLoadRawData";
            this.btnLoadRawData.Size = new System.Drawing.Size(98, 38);
            this.btnLoadRawData.TabIndex = 17;
            this.btnLoadRawData.Text = "...";
            this.btnLoadRawData.Click += new System.EventHandler(this.btnLoadRawData_Click);
            // 
            // btnAyalyseData
            // 
            this.btnAyalyseData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAyalyseData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAyalyseData.Location = new System.Drawing.Point(1003, 31);
            this.btnAyalyseData.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAyalyseData.Name = "btnAyalyseData";
            this.btnAyalyseData.Size = new System.Drawing.Size(138, 38);
            this.btnAyalyseData.TabIndex = 128;
            this.btnAyalyseData.Text = "AnalyseData";
            this.btnAyalyseData.Click += new System.EventHandler(this.uiButton2_Click_2);
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton3.ForeColor = System.Drawing.Color.OrangeRed;
            this.uiButton3.Location = new System.Drawing.Point(1022, 987);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton3.Size = new System.Drawing.Size(154, 38);
            this.uiButton3.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton3.TabIndex = 129;
            this.uiButton3.Text = "Exit";
            this.uiButton3.Click += new System.EventHandler(this.uiButton3_Click);
            // 
            // chtTestPlot
            // 
            chartArea1.AxisX.Title = "X Channel";
            chartArea1.AxisY.Title = "Y Channel";
            chartArea1.Name = "ChartArea1";
            this.chtTestPlot.ChartAreas.Add(chartArea1);
            this.chtTestPlot.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chtTestPlot.Legends.Add(legend1);
            this.chtTestPlot.Location = new System.Drawing.Point(0, 0);
            this.chtTestPlot.Name = "chtTestPlot";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chtTestPlot.Series.Add(series1);
            this.chtTestPlot.Size = new System.Drawing.Size(1359, 457);
            this.chtTestPlot.TabIndex = 127;
            this.chtTestPlot.Text = "chart3";
            // 
            // grbTestConditions
            // 
            this.grbTestConditions.Controls.Add(this.txtTargetInflation);
            this.grbTestConditions.Controls.Add(this.txtTireInflation);
            this.grbTestConditions.Controls.Add(this.txtTireLoad);
            this.grbTestConditions.Controls.Add(this.lblTireLoad);
            this.grbTestConditions.Controls.Add(this.label21);
            this.grbTestConditions.Controls.Add(this.label20);
            this.grbTestConditions.Controls.Add(this.txtVaz);
            this.grbTestConditions.Controls.Add(this.txtTargetDeflection);
            this.grbTestConditions.Controls.Add(this.label30);
            this.grbTestConditions.Controls.Add(this.txtTireSize);
            this.grbTestConditions.Controls.Add(this.txtVxy);
            this.grbTestConditions.Controls.Add(this.label22);
            this.grbTestConditions.Controls.Add(this.label31);
            this.grbTestConditions.Controls.Add(this.label19);
            this.grbTestConditions.Controls.Add(this.txtVz);
            this.grbTestConditions.Controls.Add(this.label32);
            this.grbTestConditions.Controls.Add(this.txtTargetLoad);
            this.grbTestConditions.Controls.Add(this.txtTestDate);
            this.grbTestConditions.Controls.Add(this.label25);
            this.grbTestConditions.Controls.Add(this.label18);
            this.grbTestConditions.Controls.Add(this.txtTireDia);
            this.grbTestConditions.Controls.Add(this.txtTestType);
            this.grbTestConditions.Controls.Add(this.label26);
            this.grbTestConditions.Controls.Add(this.label16);
            this.grbTestConditions.Controls.Add(this.txtTestID);
            this.grbTestConditions.Controls.Add(this.label17);
            this.grbTestConditions.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grbTestConditions.Location = new System.Drawing.Point(16, 771);
            this.grbTestConditions.Name = "grbTestConditions";
            this.grbTestConditions.Size = new System.Drawing.Size(1448, 161);
            this.grbTestConditions.TabIndex = 125;
            this.grbTestConditions.TabStop = false;
            this.grbTestConditions.Text = "Test Conditions";
            // 
            // txtTargetInflation
            // 
            this.txtTargetInflation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTargetInflation.FillColor = System.Drawing.Color.White;
            this.txtTargetInflation.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTargetInflation.Location = new System.Drawing.Point(1194, 75);
            this.txtTargetInflation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTargetInflation.Maximum = 2147483647D;
            this.txtTargetInflation.Minimum = -2147483648D;
            this.txtTargetInflation.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTargetInflation.Name = "txtTargetInflation";
            this.txtTargetInflation.RectColor = System.Drawing.Color.LightGray;
            this.txtTargetInflation.Size = new System.Drawing.Size(127, 26);
            this.txtTargetInflation.Style = Sunny.UI.UIStyle.Custom;
            this.txtTargetInflation.TabIndex = 161;
            this.txtTargetInflation.Text = "psi";
            this.txtTargetInflation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTireInflation
            // 
            this.txtTireInflation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTireInflation.FillColor = System.Drawing.Color.White;
            this.txtTireInflation.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTireInflation.Location = new System.Drawing.Point(939, 75);
            this.txtTireInflation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTireInflation.Maximum = 2147483647D;
            this.txtTireInflation.Minimum = -2147483648D;
            this.txtTireInflation.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTireInflation.Name = "txtTireInflation";
            this.txtTireInflation.RectColor = System.Drawing.Color.LightGray;
            this.txtTireInflation.Size = new System.Drawing.Size(120, 26);
            this.txtTireInflation.Style = Sunny.UI.UIStyle.Custom;
            this.txtTireInflation.TabIndex = 154;
            this.txtTireInflation.Text = "psi";
            this.txtTireInflation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTireLoad
            // 
            this.txtTireLoad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTireLoad.FillColor = System.Drawing.Color.White;
            this.txtTireLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTireLoad.Location = new System.Drawing.Point(688, 36);
            this.txtTireLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTireLoad.Maximum = 2147483647D;
            this.txtTireLoad.Minimum = -2147483648D;
            this.txtTireLoad.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTireLoad.Name = "txtTireLoad";
            this.txtTireLoad.RectColor = System.Drawing.Color.LightGray;
            this.txtTireLoad.Size = new System.Drawing.Size(120, 26);
            this.txtTireLoad.Style = Sunny.UI.UIStyle.Custom;
            this.txtTireLoad.TabIndex = 156;
            this.txtTireLoad.TabStop = false;
            this.txtTireLoad.Text = "lb";
            this.txtTireLoad.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTireLoad
            // 
            this.lblTireLoad.AutoSize = true;
            this.lblTireLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTireLoad.Location = new System.Drawing.Point(616, 41);
            this.lblTireLoad.Name = "lblTireLoad";
            this.lblTireLoad.Size = new System.Drawing.Size(74, 20);
            this.lblTireLoad.TabIndex = 155;
            this.lblTireLoad.Text = "Tire Load:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(1079, 78);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(116, 20);
            this.label21.TabIndex = 157;
            this.label21.Text = "Target Inflation:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(831, 78);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 20);
            this.label20.TabIndex = 153;
            this.label20.Text = "Tire Inflation:";
            // 
            // txtVaz
            // 
            this.txtVaz.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVaz.FillColor = System.Drawing.Color.White;
            this.txtVaz.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVaz.Location = new System.Drawing.Point(1194, 114);
            this.txtVaz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVaz.Maximum = 2147483647D;
            this.txtVaz.Minimum = -2147483648D;
            this.txtVaz.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtVaz.Name = "txtVaz";
            this.txtVaz.RectColor = System.Drawing.Color.LightGray;
            this.txtVaz.Size = new System.Drawing.Size(127, 26);
            this.txtVaz.Style = Sunny.UI.UIStyle.Custom;
            this.txtVaz.TabIndex = 173;
            this.txtVaz.Text = "°/s";
            this.txtVaz.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTargetDeflection
            // 
            this.txtTargetDeflection.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTargetDeflection.FillColor = System.Drawing.Color.White;
            this.txtTargetDeflection.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTargetDeflection.Location = new System.Drawing.Point(1194, 36);
            this.txtTargetDeflection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTargetDeflection.Maximum = 2147483647D;
            this.txtTargetDeflection.Minimum = -2147483648D;
            this.txtTargetDeflection.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTargetDeflection.Name = "txtTargetDeflection";
            this.txtTargetDeflection.RectColor = System.Drawing.Color.LightGray;
            this.txtTargetDeflection.Size = new System.Drawing.Size(127, 26);
            this.txtTargetDeflection.Style = Sunny.UI.UIStyle.Custom;
            this.txtTargetDeflection.TabIndex = 162;
            this.txtTargetDeflection.Text = "in";
            this.txtTargetDeflection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(1155, 120);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(36, 20);
            this.label30.TabIndex = 170;
            this.label30.Text = "Vaz:";
            // 
            // txtTireSize
            // 
            this.txtTireSize.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTireSize.FillColor = System.Drawing.Color.White;
            this.txtTireSize.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTireSize.Location = new System.Drawing.Point(106, 78);
            this.txtTireSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTireSize.Maximum = 2147483647D;
            this.txtTireSize.Minimum = -2147483648D;
            this.txtTireSize.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTireSize.Name = "txtTireSize";
            this.txtTireSize.RectColor = System.Drawing.Color.LightGray;
            this.txtTireSize.Size = new System.Drawing.Size(254, 26);
            this.txtTireSize.Style = Sunny.UI.UIStyle.Custom;
            this.txtTireSize.TabIndex = 154;
            this.txtTireSize.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVxy
            // 
            this.txtVxy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVxy.FillColor = System.Drawing.Color.White;
            this.txtVxy.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVxy.Location = new System.Drawing.Point(939, 121);
            this.txtVxy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVxy.Maximum = 2147483647D;
            this.txtVxy.Minimum = -2147483648D;
            this.txtVxy.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtVxy.Name = "txtVxy";
            this.txtVxy.RectColor = System.Drawing.Color.LightGray;
            this.txtVxy.Size = new System.Drawing.Size(120, 26);
            this.txtVxy.Style = Sunny.UI.UIStyle.Custom;
            this.txtVxy.TabIndex = 174;
            this.txtVxy.Text = "°/s";
            this.txtVxy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(1066, 40);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(129, 20);
            this.label22.TabIndex = 158;
            this.label22.Text = "Target Deflection:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(889, 124);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(35, 20);
            this.label31.TabIndex = 171;
            this.label31.Text = "Vxy:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(29, 82);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 20);
            this.label19.TabIndex = 153;
            this.label19.Text = "TireSize:";
            // 
            // txtVz
            // 
            this.txtVz.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVz.FillColor = System.Drawing.Color.White;
            this.txtVz.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVz.Location = new System.Drawing.Point(688, 114);
            this.txtVz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVz.Maximum = 2147483647D;
            this.txtVz.Minimum = -2147483648D;
            this.txtVz.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtVz.Name = "txtVz";
            this.txtVz.RectColor = System.Drawing.Color.LightGray;
            this.txtVz.Size = new System.Drawing.Size(120, 26);
            this.txtVz.Style = Sunny.UI.UIStyle.Custom;
            this.txtVz.TabIndex = 168;
            this.txtVz.Text = "°/s";
            this.txtVz.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.Location = new System.Drawing.Point(658, 120);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(28, 20);
            this.label32.TabIndex = 167;
            this.label32.Text = "Vz:";
            // 
            // txtTargetLoad
            // 
            this.txtTargetLoad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTargetLoad.FillColor = System.Drawing.Color.White;
            this.txtTargetLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTargetLoad.Location = new System.Drawing.Point(688, 75);
            this.txtTargetLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTargetLoad.Maximum = 2147483647D;
            this.txtTargetLoad.Minimum = -2147483648D;
            this.txtTargetLoad.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTargetLoad.Name = "txtTargetLoad";
            this.txtTargetLoad.RectColor = System.Drawing.Color.LightGray;
            this.txtTargetLoad.Size = new System.Drawing.Size(120, 26);
            this.txtTargetLoad.Style = Sunny.UI.UIStyle.Custom;
            this.txtTargetLoad.TabIndex = 163;
            this.txtTargetLoad.Text = "lb";
            this.txtTargetLoad.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTestDate
            // 
            this.txtTestDate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTestDate.FillColor = System.Drawing.Color.White;
            this.txtTestDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestDate.Location = new System.Drawing.Point(451, 78);
            this.txtTestDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTestDate.Maximum = 2147483647D;
            this.txtTestDate.Minimum = -2147483648D;
            this.txtTestDate.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTestDate.Name = "txtTestDate";
            this.txtTestDate.RectColor = System.Drawing.Color.LightGray;
            this.txtTestDate.Size = new System.Drawing.Size(144, 26);
            this.txtTestDate.Style = Sunny.UI.UIStyle.Custom;
            this.txtTestDate.TabIndex = 154;
            this.txtTestDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(598, 81);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(92, 20);
            this.label25.TabIndex = 159;
            this.label25.Text = "Target Load:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(376, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 20);
            this.label18.TabIndex = 153;
            this.label18.Text = "Test Date:";
            // 
            // txtTireDia
            // 
            this.txtTireDia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTireDia.FillColor = System.Drawing.Color.White;
            this.txtTireDia.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTireDia.Location = new System.Drawing.Point(939, 36);
            this.txtTireDia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTireDia.Maximum = 2147483647D;
            this.txtTireDia.Minimum = -2147483648D;
            this.txtTireDia.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTireDia.Name = "txtTireDia";
            this.txtTireDia.RectColor = System.Drawing.Color.LightGray;
            this.txtTireDia.Size = new System.Drawing.Size(120, 26);
            this.txtTireDia.Style = Sunny.UI.UIStyle.Custom;
            this.txtTireDia.TabIndex = 164;
            this.txtTireDia.Text = "in";
            this.txtTireDia.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTestType
            // 
            this.txtTestType.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTestType.FillColor = System.Drawing.Color.White;
            this.txtTestType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestType.Location = new System.Drawing.Point(451, 39);
            this.txtTestType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTestType.Maximum = 2147483647D;
            this.txtTestType.Minimum = -2147483648D;
            this.txtTestType.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTestType.Name = "txtTestType";
            this.txtTestType.RectColor = System.Drawing.Color.LightGray;
            this.txtTestType.Size = new System.Drawing.Size(144, 26);
            this.txtTestType.Style = Sunny.UI.UIStyle.Custom;
            this.txtTestType.TabIndex = 154;
            this.txtTestType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(865, 40);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 20);
            this.label26.TabIndex = 160;
            this.label26.Text = "Tire Dia:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(376, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 20);
            this.label16.TabIndex = 153;
            this.label16.Text = "Test Type:";
            // 
            // txtTestID
            // 
            this.txtTestID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTestID.FillColor = System.Drawing.Color.White;
            this.txtTestID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestID.Location = new System.Drawing.Point(106, 40);
            this.txtTestID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTestID.Maximum = 2147483647D;
            this.txtTestID.Minimum = -2147483648D;
            this.txtTestID.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtTestID.Name = "txtTestID";
            this.txtTestID.RectColor = System.Drawing.Color.LightGray;
            this.txtTestID.Size = new System.Drawing.Size(254, 26);
            this.txtTestID.Style = Sunny.UI.UIStyle.Custom;
            this.txtTestID.TabIndex = 152;
            this.txtTestID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(34, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 20);
            this.label17.TabIndex = 149;
            this.label17.Text = "TestNo:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.uiComboBox22);
            this.groupBox3.Controls.Add(this.uiComboBox24);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Location = new System.Drawing.Point(16, 638);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(321, 127);
            this.groupBox3.TabIndex = 124;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channel Definition";
            // 
            // uiComboBox22
            // 
            this.uiComboBox22.DataSource = null;
            this.uiComboBox22.FillColor = System.Drawing.Color.White;
            this.uiComboBox22.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiComboBox22.ForeColor = System.Drawing.Color.RoyalBlue;
            this.uiComboBox22.Items.AddRange(new object[] {
            "Fz",
            "Pz",
            "Fx",
            "Fy",
            "Mx",
            "My",
            "Mz",
            "Pxy",
            "Az",
            "Inf"});
            this.uiComboBox22.Location = new System.Drawing.Point(137, 73);
            this.uiComboBox22.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox22.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox22.Name = "uiComboBox22";
            this.uiComboBox22.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox22.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox22.Style = Sunny.UI.UIStyle.Custom;
            this.uiComboBox22.TabIndex = 126;
            this.uiComboBox22.Text = "Fz";
            this.uiComboBox22.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiComboBox24
            // 
            this.uiComboBox24.DataSource = null;
            this.uiComboBox24.FillColor = System.Drawing.Color.White;
            this.uiComboBox24.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiComboBox24.ForeColor = System.Drawing.Color.RoyalBlue;
            this.uiComboBox24.Items.AddRange(new object[] {
            "Time",
            "Fz",
            "Pz",
            "Fx",
            "Fy",
            "Mx",
            "My",
            "Mz",
            "Pxy",
            "Az",
            "Inf"});
            this.uiComboBox24.Location = new System.Drawing.Point(137, 36);
            this.uiComboBox24.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox24.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox24.Name = "uiComboBox24";
            this.uiComboBox24.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox24.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox24.Style = Sunny.UI.UIStyle.Custom;
            this.uiComboBox24.TabIndex = 125;
            this.uiComboBox24.Text = "Time";
            this.uiComboBox24.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 123;
            this.label13.Text = "Y Channel:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 20);
            this.label14.TabIndex = 121;
            this.label14.Text = "X Channel:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.uiComboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.cb01);
            this.groupBox1.Controls.Add(this.uiComboBox21);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.cb02);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(353, 638);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 127);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "仅用于计算测试结果";
            // 
            // uiComboBox1
            // 
            this.uiComboBox1.DataSource = null;
            this.uiComboBox1.FillColor = System.Drawing.Color.White;
            this.uiComboBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiComboBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.uiComboBox1.Items.AddRange(new object[] {
            "English",
            "SI",
            "MKS"});
            this.uiComboBox1.Location = new System.Drawing.Point(75, 79);
            this.uiComboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox1.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox1.Name = "uiComboBox1";
            this.uiComboBox1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox1.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiComboBox1.TabIndex = 132;
            this.uiComboBox1.Text = "English";
            this.uiComboBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiComboBox1.SelectedIndexChanged += new System.EventHandler(this.uiComboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 131;
            this.label1.Text = " Unit：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.DimGray;
            this.btnConfirm.Location = new System.Drawing.Point(563, 36);
            this.btnConfirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfirm.Size = new System.Drawing.Size(130, 31);
            this.btnConfirm.Style = Sunny.UI.UIStyle.Custom;
            this.btnConfirm.TabIndex = 130;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.uiButton11_Click);
            // 
            // cb01
            // 
            this.cb01.DataSource = null;
            this.cb01.FillColor = System.Drawing.Color.White;
            this.cb01.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cb01.ForeColor = System.Drawing.Color.RoyalBlue;
            this.cb01.Items.AddRange(new object[] {
            "10%",
            "20%",
            "30%",
            "40%",
            "50%",
            "60%",
            "70%",
            "80%",
            "90%",
            "100%"});
            this.cb01.Location = new System.Drawing.Point(371, 32);
            this.cb01.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb01.MinimumSize = new System.Drawing.Size(63, 0);
            this.cb01.Name = "cb01";
            this.cb01.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cb01.Size = new System.Drawing.Size(150, 29);
            this.cb01.Style = Sunny.UI.UIStyle.Custom;
            this.cb01.TabIndex = 129;
            this.cb01.Text = "0%";
            this.cb01.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiComboBox21
            // 
            this.uiComboBox21.DataSource = null;
            this.uiComboBox21.FillColor = System.Drawing.Color.White;
            this.uiComboBox21.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiComboBox21.ForeColor = System.Drawing.Color.RoyalBlue;
            this.uiComboBox21.Location = new System.Drawing.Point(75, 32);
            this.uiComboBox21.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox21.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox21.Name = "uiComboBox21";
            this.uiComboBox21.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox21.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox21.Style = Sunny.UI.UIStyle.Custom;
            this.uiComboBox21.TabIndex = 128;
            this.uiComboBox21.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 20);
            this.label15.TabIndex = 127;
            this.label15.Text = "Recipe:";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.DimGray;
            this.btnSave.Location = new System.Drawing.Point(563, 73);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Size = new System.Drawing.Size(130, 27);
            this.btnSave.Style = Sunny.UI.UIStyle.Custom;
            this.btnSave.TabIndex = 126;
            this.btnSave.Text = "Save As Defaults";
            // 
            // cb02
            // 
            this.cb02.DataSource = null;
            this.cb02.FillColor = System.Drawing.Color.White;
            this.cb02.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cb02.ForeColor = System.Drawing.Color.RoyalBlue;
            this.cb02.ForeDisableColor = System.Drawing.Color.RoyalBlue;
            this.cb02.Items.AddRange(new object[] {
            "10%",
            "20%",
            "30%",
            "40%",
            "50%",
            "60%",
            "70%",
            "80%",
            "90%",
            "100%"});
            this.cb02.Location = new System.Drawing.Point(371, 80);
            this.cb02.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb02.MinimumSize = new System.Drawing.Size(63, 0);
            this.cb02.Name = "cb02";
            this.cb02.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cb02.RectColor = System.Drawing.Color.LightGray;
            this.cb02.Size = new System.Drawing.Size(150, 27);
            this.cb02.Style = Sunny.UI.UIStyle.Custom;
            this.cb02.TabIndex = 123;
            this.cb02.Text = "100%";
            this.cb02.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(264, 87);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 20);
            this.label23.TabIndex = 122;
            this.label23.Text = "High Percent:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(270, 36);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(94, 20);
            this.label24.TabIndex = 121;
            this.label24.Text = "Low Percent:";
            // 
            // uiButton5
            // 
            this.uiButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton5.ForeColor = System.Drawing.Color.Black;
            this.uiButton5.Location = new System.Drawing.Point(1193, 987);
            this.uiButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton5.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton5.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton5.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton5.Size = new System.Drawing.Size(169, 38);
            this.uiButton5.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton5.TabIndex = 22;
            this.uiButton5.Text = "Exporting Raw Data";
            this.uiButton5.Click += new System.EventHandler(this.uiButton5_Click);
            // 
            // uiButton4
            // 
            this.uiButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton4.ForeColor = System.Drawing.Color.Black;
            this.uiButton4.Location = new System.Drawing.Point(1380, 987);
            this.uiButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton4.Name = "uiButton4";
            this.uiButton4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton4.Size = new System.Drawing.Size(154, 38);
            this.uiButton4.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton4.TabIndex = 21;
            this.uiButton4.Text = "Export Report";
            this.uiButton4.Click += new System.EventHandler(this.uiButton4_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1640, 907);
            this.Controls.Add(this.uiTabControlMenu1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.uiTabControlMenu1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtTestPlot)).EndInit();
            this.grbTestConditions.ResumeLayout(false);
            this.grbTestConditions.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITabControlMenu uiTabControlMenu1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton btnLoadRawData;
        private System.Windows.Forms.GroupBox groupBox1;
        private Sunny.UI.UIComboBox cb02;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private Sunny.UI.UIButton btnSave;
        private System.Windows.Forms.GroupBox grbTestConditions;
        private Sunny.UI.UITextBox txtTestID;
        private System.Windows.Forms.Label label17;
        private Sunny.UI.UITextBox txtTargetInflation;
        private Sunny.UI.UITextBox txtTireInflation;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private Sunny.UI.UITextBox txtTargetDeflection;
        private Sunny.UI.UITextBox txtTireSize;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private Sunny.UI.UITextBox txtTargetLoad;
        private Sunny.UI.UITextBox txtTestDate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label18;
        private Sunny.UI.UITextBox txtTireDia;
        private Sunny.UI.UITextBox txtTestType;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label16;
        private Sunny.UI.UITextBox txtTireLoad;
        private System.Windows.Forms.Label lblTireLoad;
        private Sunny.UI.UITextBox txtVaz;
        private System.Windows.Forms.Label label30;
        private Sunny.UI.UITextBox txtVxy;
        private System.Windows.Forms.Label label31;
        private Sunny.UI.UITextBox txtVz;
        private System.Windows.Forms.Label label32;
        private Sunny.UI.UIComboBox uiComboBox24;
        private Sunny.UI.UIComboBox uiComboBox22;
        private Sunny.UI.UIComboBox cb01;
        private Sunny.UI.UIComboBox uiComboBox21;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtTestPlot;
        private Sunny.UI.UIComboBox uiComboBox1;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIButton btnConfirm;
        private Sunny.UI.UIButton btnAyalyseData;
        private Sunny.UI.UIButton uiButton3;
        private System.Windows.Forms.GroupBox groupBox2;
        private Sunny.UI.UITextBox txtDataPath;
        private Sunny.UI.UIButton uiButton1;
    }
}

