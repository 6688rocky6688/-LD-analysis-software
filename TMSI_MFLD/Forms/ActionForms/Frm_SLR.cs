using ICSharpCode.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TMSI_MFLD.Basis;
using TMSI_MFLD.Basis.Glodal;
using TMSI_MFLD.Basis.ToolStripRendererEx;
using TMSI_MFLD.Controls.DgvHelper;
using TMSI_MFLD.Excel;
using TMSI_MFLD.Forms.ShowDialog;
using zta_convert;

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_SLR : System.Windows.Forms.Form
    {
        #region 字段定义

        List<double> lsrawdata = new List<double>();
        List<double> lsrawtime = new List<double>();
        object obj = new object();
        object obj1 = new object();
        //返回的坐标外宽
        int x, y, width, hight;
        //控件坐标
        int x_dgv, y_dgv;
        //选定的表格列和行号
        int _cell, row;
        //窗体加载结束标志位用途：窗体首次加载时会操作表格控件，并且会自动选择表格控件，避免错误选择。
        bool _frmshown = false;
        //记录文件位置
        string _pathtestnumber;
        string _pathplotformat;
        //zta文件解析后获得的Json字符串
        string ztaJsonStr = string.Empty;
        //Header字符串
        public string jaHeader = string.Empty;
        public string jaConfig = string.Empty;
        public string jaRawData = string.Empty;
        //zta文件大结构体对象
        JObject jo = new JObject();
        //解析出来的RawData对象
        JArray ja = new JArray();
        //新排列顺序通道的集合
        ArrayList channelCoordinates = new ArrayList { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //重新排列的第一通道RowData数组数据对象
        JObject joList01 = null;
        JObject joList02 = null;
        JObject joList03 = null;
        JObject joList04 = null;
        JObject joList05 = null;
        JObject joList06 = null;
        JObject joList07 = null;
        JObject joList08 = null;
        JObject joList09 = null;
        JObject joList10 = null;

        //每个RowData中取出的测试数量的，相应的数据值
        ArrayList aisle01ArrRawData = new ArrayList();      //第一个通道取出的测试数值
        ArrayList aisle02ArrRawData = new ArrayList();
        ArrayList aisle03ArrRawData = new ArrayList();
        ArrayList aisle04ArrRawData = new ArrayList();
        ArrayList aisle05ArrRawData = new ArrayList();
        ArrayList aisle06ArrRawData = new ArrayList();
        ArrayList aisle07ArrRawData = new ArrayList();
        ArrayList aisle08ArrRawData = new ArrayList();
        ArrayList aisle09ArrRawData = new ArrayList();
        ArrayList aisle10ArrRawData = new ArrayList();

        //样本类型
        private static object TestNo = null;
        private static object TestType = null;
        private static object TestDate = null;
        private static object TireSize = null;
        private static object TireInflation = null;
        private static object TireLoad = null;
        private static object TireDia = null;
        private static object TargetLoad = null;
        private static object TargetDeflection = null;
        private static object TargetInflation = null;
        private static object VectorRatePz = null;
        private static object VectorRatePxy = null;
        private static object VectorRateAz = null;  // (English)

        ArrayList arrStartTime = new ArrayList();       //开始时间
        ArrayList arrSampRate = new ArrayList();        //取样率
        ArrayList arrTestDuration = new ArrayList();    //持续时间
        ArrayList countSelect = new ArrayList();        //取多少的数量

        Decimal timeChangeData = new Decimal();         //每秒取样数量

        #region 单位转换常量

        //压强的单位，名称是千帕
        decimal kpa = Convert.ToDecimal(6.894757);
        //力的单位，即牛顿，简称牛
        decimal N = Convert.ToDecimal(4.448222);
        //毫米
        decimal mm = Convert.ToDecimal(25.4);
        //角度
        decimal s = Convert.ToDecimal(1);
        //是一种力的单位,叫千克力
        decimal Kgf = Convert.ToDecimal(0.4535924);
        //X轴名称
        string axisX = "";
        //Y轴名称
        string axisY = "";
        //测量单位分类名称
        string measureUnit = "";

        #endregion

        //Header的Datatable数据
        public DataTable dt01;
        //Config的Datatable数据
        public DataTable dt02;
        //Config + RawData，2个datatable合一起的数据
        public DataTable dt03;
        //X轴与Y轴合拼的datatable(有效的测试数据)
        public DataTable dt04;
        //报告标题集合
        public DataTable dt05;

        public bool isMouseDown = false;

        #region 动态生成的下拉框控件

        ComboBox ComboBox_Pg = new ComboBox();

        #endregion

        double? exmax, exmin;
        //曲线对象
        Series test;

        DataTable dtHead;

        string xname, yname;

        double minimum = 0.0;
        double interval = 0.0;

        #endregion

        #region 构造函数

        public Frm_SLR()
        {
            InitializeComponent();
            //生成右击菜单
            ToolStripManager.Renderer = new ProfessionalToolStripRendererEx();
            //曲线控件行为
            this.chtTestPlot.ContextMenuStrip = this.chart_contextMsp;
            bindMouseWheel(dgv_PlotPackageUtility);
            //初始化界面元素
            InitUIMethod();
        }

        #endregion

        #region 事件定义

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_PlotPackageUtility_Load(object sender, EventArgs e)
        {
            //初始化DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Pg");
            dt.Columns.Add("PI.");
            dt.Columns.Add("Ov");
            dt.Columns.Add("Calc.");
            dt.Columns.Add("Filter");
            dt.Columns.Add("Data Description");
            dt.Columns.Add("Start");
            dt.Columns.Add("End");
            dt.Columns.Add("X");
            dt.Columns.Add("Title(opt.)");
            //初始化数据(初始增加50行数据)
            for (int ii = 0; ii < 50; ii++)
            {
                DataRow r1 = dt.NewRow();
                r1["Calc."] = "Raw";
                r1["Filter"] = "none";
                r1["Start"] = "0.000";
                r1["End"] = "0.000";
                r1["X"] = "tm";
                dt.Rows.Add(r1);
            }
            //更新数据源
            this.dgv_PlotPackageUtility.DataSource = dt;
            //设置表格字体
            this.dgv_PlotPackageUtility.RowsDefaultCellStyle.Font = new Font("宋体", 15, FontStyle.Regular);
            //重绘表格样式
            DataGridViewHelper.SetStyle(dgv_PlotPackageUtility, DataGridViewAutoSizeColumnsMode.Fill);
            //窗体大小改变事件
            //this.Resize += new System.EventHandler(this.Form_Resize);
        }

        /// <summary>
        /// 表格选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_PlotPackageUtility_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_PlotPackageUtility.SelectedCells.Count == 1 && dgv_PlotPackageUtility.SelectedCells != null && _frmshown == true)
            {
                //返回坐标&外宽
                BaseHelper.DgvCellCoordinate(dgv_PlotPackageUtility, out x, out y, out width, out hight);
                //返回表格控件坐标
                BaseHelper.DgvCoordinate(dgv_PlotPackageUtility, out x_dgv, out y_dgv);
                //获取点击列
                DataGridViewCell selectedcell = dgv_PlotPackageUtility.SelectedCells[0];
                //获取列名
                switch (this.dgv_PlotPackageUtility.Columns[this.dgv_PlotPackageUtility.CurrentCell.ColumnIndex].HeaderText.ToString())
                {
                    case "Pg":
                        {
                            //动态生成
                            ComboBox_Pg.Enabled = true;
                            ComboBox_Pg.BackColor = Color.White;
                            ComboBox_Pg.Text = selectedcell.Value.ToString();
                            ComboBox_Pg.Location = new Point(x - x_dgv, y - y_dgv);
                            ComboBox_Pg.Size = new Size(width, hight);
                            ComboBox_Pg.Font = new Font("Segoe UI", 15F);
                            //加载下拉框控件集合
                            ComboBox_Pg.Items.Clear();
                            ComboBox_Pg.Items.Add("X1");
                            ComboBox_Pg.Items.Add("X2");
                            ComboBox_Pg.Items.Add("X3");
                            ComboBox_Pg.Items.Add("X4");
                            for (int ii = 1; ii < 129; ii++)
                            {
                                ComboBox_Pg.Items.Add(ii.ToString());
                            }
                            //加载进控件内
                            this.dgv_PlotPackageUtility.Controls.Add(ComboBox_Pg);
                            ComboBox_Pg.Focus();//将光标定位在combox1上
                            ComboBox_Pg.Leave -= ComboBox_Leave;
                            ComboBox_Pg.Leave += ComboBox_Leave;
                        }
                        break;
                    case "Pi":

                        break;
                    case "Calc":

                        break;
                    case "Filter":

                        break;
                    case "Data Description":
                        //动态生成
                        ComboBox_Pg.Enabled = true;
                        ComboBox_Pg.BackColor = Color.White;
                        ComboBox_Pg.Text = selectedcell.Value.ToString();
                        ComboBox_Pg.Location = new Point(x - x_dgv, y - y_dgv);
                        ComboBox_Pg.Size = new Size(width, hight);
                        ComboBox_Pg.Font = new Font("Segoe UI", 15F);
                        //加载下拉框控件集合
                        ComboBox_Pg.Items.Clear();
                        ComboBox_Pg.Items.Add("Fz");
                        ComboBox_Pg.Items.Add("Pz");
                        ComboBox_Pg.Items.Add("Fx");
                        ComboBox_Pg.Items.Add("Fy");
                        ComboBox_Pg.Items.Add("Az");
                        ComboBox_Pg.Items.Add("Mx");
                        ComboBox_Pg.Items.Add("My");
                        ComboBox_Pg.Items.Add("Mz");
                        ComboBox_Pg.Items.Add("Pxy");
                        ComboBox_Pg.Items.Add("Az");
                        ComboBox_Pg.Items.Add("Inf");
                        //加载进控件内
                        this.dgv_PlotPackageUtility.Controls.Add(ComboBox_Pg);
                        ComboBox_Pg.Focus();//将光标定位在combox1上
                        ComboBox_Pg.Leave -= ComboBox_Leave;
                        ComboBox_Pg.Leave += ComboBox_Leave;
                        break;
                }

            }
        }

        /// <summary>
        /// Test Number点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtn_TN_BtnClick(object sender, EventArgs e)
        {
            //禁止重复加载
            this.btn_Pages.Enabled = false;
            //加载时关闭选择文件按钮
            this.ucBtn_TN.Enabled = false;
            //用户选择打开的文件
            OpenFileDialog ofd = new OpenFileDialog();
            //用户需要打开的文件类型
            ofd.Filter = "raw File(*.zta)|*.zta";
            //设置绝对路径
            ofd.InitialDirectory = _pathtestnumber;
            //自动添加扩展名
            ofd.AddExtension = true;
            //打开窗体。
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                _pathtestnumber = ofd.FileName;
                if (!String.IsNullOrEmpty(_pathtestnumber)) this.txtDataPath.Text = ofd.SafeFileName;
                this.txtDataPath.ForeColor = Color.Black;
                this.uc_units.Text = "English";
            }
            Thread thread = new Thread(Analysis);
            thread.Start(ofd.FileName);
        }

        private void Analysis(object path)
        {
            #region 解析文件

            //解析文件
            var zta = new ZTAFile(path.ToString());
            string ztaJsonStr = JsonConvert.SerializeObject(zta.DataV2, Formatting.Indented);
            //添加解析表格
            DataTable row = JsonToDataTable(ztaJsonStr);
            DataTable head = JsonToDataTableHead(ztaJsonStr);
            DataTable Config = JsonToDataTableConfig(ztaJsonStr);

            #endregion

            #region Dictionary原始数据

            //声明字典接取全部的数据
            Dictionary<string, List<string>> dicRawdata = new Dictionary<string, List<string>>();
            string[] rdarr;
            List<string> ls = new List<string>();
            int num = 0;

            foreach (DataRow dr in row.Rows)
            {
                //去头去尾
                string str = dr[0].ToString().Substring(1, (dr[0].ToString().Length) - 1);
                //去重复
                rdarr = str.Split(',');
                //加载至字典内
                ls = rdarr.ToList();
                //寻找字典的键值
                string[] dickey = zta.DataV2.Config[num++].DataDesc.Split(' ');

                if (dickey[0] == "Px" || dickey[0] == "Py")
                {
                    dickey[0] = "Pxy";
                }

                dicRawdata.Add(dickey[0], ls);
            }

            #endregion

            #region 计算

            //xy坐标浮点数集合
            List<double> xdou = new List<double>();
            List<double> ydou = new List<double>();
            //xy轴
            string x, y;
            //查找报告类型。
            string type = head.Rows[0]["TestType"].ToString();
            string load = head.Rows[0]["TargetLoad"].ToString();
            string TestDuration = head.Rows[0]["TestDuration"].ToString();
            string SampRate = Config.Rows[0]["SampRate"].ToString();
            string StartTime = Config.Rows[0]["StartTime"].ToString();
            //区分报告的数据
            switch (type)
            {
                case "KLat":
                    x = "Pxy"; y = "Fy";
                    break;
                case "KLong":
                    x = "Pxy"; y = "Fx";
                    break;
                case "Ktor":
                    x = "Az"; y = "Mz";
                    break;
                case "Kv":
                    x = "Pz"; y = "Fz";
                    break;
                default:
                    x = string.Empty; y = string.Empty;
                    break;
            }
            //计算截取开始个数
            int startnum = Convert.ToInt32(1024 + double.Parse(StartTime) * double.Parse(SampRate) * 1000);
            //单次时间
            double singletime = 1 / (double.Parse(SampRate) * 1000);
            //全部的次数
            int all = startnum + int.Parse((double.Parse(TestDuration) * double.Parse(SampRate) * 1000).ToString()) - 1;

            //开始截取
            if (x != string.Empty)
            {
                //xy坐标数组类型
                string[] xvaluearr = dicRawdata[x].ToArray();
                string[] yvaluearr = dicRawdata[y].ToArray();
                //xy坐标浮点数类型
                double[] xvaluearrdou = new double[all];
                double[] yvaluearrdou = new double[all];
                //xy坐标值类型
                double xvalue = 0, yvalue = 0, xcsvd = 0, ycsvd = 0;
                List<double> xcsv = new List<double>();
                List<double> ycsv = new List<double>();

                for (int i = startnum; i < all + startnum; i++)
                {
                    xvaluearrdou[i - startnum] = double.Parse(xvaluearr[i]);
                    yvaluearrdou[i - startnum] = double.Parse(yvaluearr[i]);
                }
                lsrawdata.Clear();
                lsrawtime.Clear();
                for (int i = 0; i < all - 1; i++)
                {

                    if (yvaluearrdou[i] > 0.0)
                    {
                        lsrawdata.Add(yvaluearrdou[i]);
                        lsrawtime.Add(i * singletime);
                        if (yvaluearrdou[i] == yvaluearrdou.Max()) break;
                    }
                }
            }


            if (this.btn_Pages.InvokeRequired)
            {
                Action action = () =>
                {
                    this.btn_Pages.Enabled = true;
                };
                this.btn_Pages.Invoke(action);
            }
            if (this.ucBtn_TN.InvokeRequired)
            {
                Action action = () =>
                {
                    this.ucBtn_TN.Enabled = true;
                };
                this.ucBtn_TN.Invoke(action);
            }

            #endregion
        }

        /// <summary>
        /// Plot Format按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnExtNew1_BtnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 表格体积缩放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_PlotPackageUtility_Resize(object sender, EventArgs e)
        {
            //返回坐标&外宽
            BaseHelper.DgvCellCoordinate(dgv_PlotPackageUtility, out x, out y, out width, out hight);
            //返回表格控件坐标
            BaseHelper.DgvCoordinate(dgv_PlotPackageUtility, out x_dgv, out y_dgv);
            //获取点击列
            DataGridViewCell selectedcell = dgv_PlotPackageUtility.SelectedCells[0];
            //获取列名
            switch (this.dgv_PlotPackageUtility.Columns[this.dgv_PlotPackageUtility.CurrentCell.ColumnIndex].HeaderText.ToString())
            {
                case "Pg":
                    {
                        ComboBox_Pg.Size = new Size(width, hight);
                    }
                    break;
                case "Photo":

                    break;
                case "FileName":

                    break;
                case "Sample No.":

                    break;
                case "Comment":

                    break;
            }

        }

        /// <summary>
        /// ComboBox未选定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Leave(object sender, EventArgs e)
        {
            BaseHelper.DgvCellRow(dgv_PlotPackageUtility, out row, out _cell);
            dgv_PlotPackageUtility.Rows[row].Cells[_cell].Value = ComboBox_Pg.Text;
            this.dgv_PlotPackageUtility.Controls.Clear();
        }

        /// <summary>
        /// 窗体加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_PlotPackageUtility_Shown(object sender, EventArgs e)
        {
            _frmshown = true;
            this.dgv_PlotPackageUtility.Rows[0].Selected = false;
        }

        /// <summary>
        /// 表格滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.dgv_PlotPackageUtility.Controls.Clear();
        }

        /// <summary>
        /// 返回TMSI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void containedButton1_Click(object sender, EventArgs e)
        {
            Glodal.ExitFrmChanged.Exit = true;
        }

        /// <summary>
        /// 右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaximizeToolSM_Click(object sender, EventArgs e)
        {
            if (Glodal.ChartSeries.ChartSerie != null)
            {
                Frm_ChartShow frm_ChartShow = new Frm_ChartShow(Glodal.ChartSeries.ChartSerie, minimum, interval);
                //展示画面
                frm_ChartShow.ShowDialog();
            }
        }

        /// <summary>
        /// 生成曲线事件，异步方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_Pages_Click(object sender, EventArgs e)
        {
            #region 初始化
            test = new Series();
            //设备图表类型
            test.ChartType = SeriesChartType.Line;
            //等待加载控件开始
            this.loadExt1.Visible = true;
            this.loadExt1.Active = true;
            //关闭导出excel文件按钮
            this.btn_csv.Enabled = false;
            //先清除控件曲线
            this.chtTestPlot.Series.Clear();
            this.chtTestPlot.ChartAreas[0].RecalculateAxesScale();

            #endregion

            //异步方法
            Task t = Task.Run(() =>
            {
                // 异步任务中执行费时的事情...
                ZTAFileToChart(_pathtestnumber);
            });

            await t;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_excel_Click(object sender, EventArgs e)
        {
            //先截图
            try
            {
                Point screenPoint = this.chtTestPlot.PointToScreen(new Point());
                Rectangle rect = new Rectangle(screenPoint, chtTestPlot.Size);
                Image img = new Bitmap(rect.Width, rect.Height);
                Graphics g = Graphics.FromImage(img);
                g.CopyFromScreen(rect.X - 1, rect.Y - 1, 0, 0, rect.Size);
                img.Save(@"img\123.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //图片存储进Excel
            //ExcelOperations.Excel(@"img\123.png");

            return;

            int step = 0;
            try
            {
                step = int.Parse(this.com_selectsteps.Text);
            }
            catch
            {
                return;
            }

            if (test != null && this.com_selectsteps.Text != string.Empty)
            {
                #region 字段定义

                Dic dic = new Dic();
                List<double> vs = new List<double>();
                double? maxy, miny;
                double rowymax;
                Dictionary<double, double[]> keyValues = new Dictionary<double, double[]>();

                #endregion

                #region 查询区间

                //返回坐标值
                DataPoint[] dp = test.Points.ToArray();
                //分段处理
                Dictionary<int, Dic> keyValuePairs = GetGroup(dp);
                //区间赋值
                switch (step)
                {
                    case 1:
                        dic = keyValuePairs[1];
                        break;
                    case 2:
                        dic = keyValuePairs[2];
                        break;
                    case 3:
                        dic = keyValuePairs[3];
                        break;
                    case 4:
                        dic = keyValuePairs[4];
                        break;
                    default: return;//不存在分段
                }
                //区间集合转换成数组
                double[] xdouarr = dic.listX.ToArray();
                double[] ydouarr = dic.listY.ToArray();
                //找出截取的点位

                if (step < 3)
                {
                    maxy = ydouarr.Max() * this.exmax;
                    miny = ydouarr.Max() * this.exmin;
                    rowymax = ydouarr.Max();
                }
                else
                {
                    maxy = ydouarr.Min() * this.exmax;
                    miny = ydouarr.Min() * this.exmin;
                    rowymax = ydouarr.Min();
                }


                #endregion

                #region 开始计算

                //斜率计算
                double slope = Slope(xdouarr, ydouarr, miny, maxy, step);
                //找出10个百分点
                keyValues = PercentXYValue(xdouarr, ydouarr, rowymax, step);

                #endregion

                #region 导出excel报告

                ExcelOperations.Excel(dtHead, keyValues, slope, uc_units.Text, uc_units.Text);

                #endregion
            }
        }

        /// <summary>
        /// 斜率最大值选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void com_max_TextChangedEvent(object sender, EventArgs e)
        {
            ExcelButton(ref exmax, ref exmin);
        }

        /// <summary>
        /// 斜率最小值选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void com_min_SelectedChangedEvent(object sender, EventArgs e)
        {
            ExcelButton(ref exmax, ref exmin);
        }

        /// <summary>
        /// 导出原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_csv_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(this.dgv_PlotPackageUtility.Rows[0].Cells["Data Description"].Value.ToString());
            dt.Columns.Add(this.dgv_PlotPackageUtility.Rows[1].Cells["Data Description"].Value.ToString());

            DataPoint[] dp = test.Points.ToArray();

            if (dp.Length > 0)
            {
                foreach (var item in dp)
                {
                    dt.Rows.Add(item.XValue.ToString(), item.YValues[0].ToString());
                }

                //提示用户选择文件保存的位置
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xls files(*.xls)|*.xls";//保存文件类型
                sfd.FileName = String.Format("{0:yyyyMMdd}", DateTime.Now);//保存文件名称
                sfd.AddExtension = true;
                DialogResult result = sfd.ShowDialog();//打开保存界面
                                                       //开始保存
                if (result == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    if (!String.IsNullOrEmpty(fileName))
                    {
                        System.IO.Stream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                        try
                        {
                            DataToFileHandler.Instance.ToExcel(dt, ref fs);
                            fs.Close();

                            Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmMode.FrmModeExcel.Success);
                            frm_Sd_Excel.ShowDialog();
                        }
                        catch
                        {
                            Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmMode.FrmModeExcel.Failure);
                            frm_Sd_Excel.ShowDialog();
                        }
                        finally
                        {
                            fs.Close();
                        }

                    }
                }
            }
            else
            {
                return;
            }
        }

        #endregion

        #region 方法定义

        /// <summary>
        /// 给datagridview添加鼠标滚轮事件
        /// </summary>
        /// <param name="dataGridView1"></param>
        public void bindMouseWheel(System.Windows.Forms.DataGridView dataGridView)
        {
            //订阅滚轮事件
            dataGridView.MouseWheel += new System.Windows.Forms.MouseEventHandler(dataGridView_MouseWheel);
            dataGridView.TabIndex = 0; //获得焦点
        }

        /// <summary>
        /// 重新排列通道
        /// </summary>
        public void RearrangeChannels()
        {

            //重新按指定排列通道，jaConfig重新排列，RawData重新排列
            //1-jaConfig重新排列
            var config = jo["Config"];
            //1-1 在Config中获取Fz通道的坐标

            //List<String> channelCoordinates = new List<string>();
            //int[] channelCoordinates = new int[config.Count()];
            for (int i = 0; i < config.Count(); i++)
            {
                var h = config[i]["DataDesc"].ToString();
                string[] after = h.Split(' ');
                for (int j = 0; j < after.Length; j++)
                {
                    switch (after[j])//X轴名称
                    {
                        case "Fz":
                            //channelCoordinates[0] = jo["RawData"][i];
                            joList01 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Pz":
                            //channelCoordinates[1] = jo["RawData"][i];
                            joList02 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Fx":
                            //channelCoordinates[2] = jo["RawData"][i];
                            joList03 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Fy":
                            //channelCoordinates[3] = jo["RawData"][i];
                            joList04 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Mx":
                            //channelCoordinates[4] = jo["RawData"][i];
                            joList05 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "My":
                            //channelCoordinates[5] = jo["RawData"][i];
                            joList06 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Mz":
                            //channelCoordinates[0] = jo["RawData"][i];
                            joList07 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Pxy":
                            //channelCoordinates[6] = jo["RawData"][i];
                            joList08 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Py":
                            //channelCoordinates[6] = jo["RawData"][i];
                            joList08 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Px":
                            //channelCoordinates[6] = jo["RawData"][i];
                            joList08 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Az":
                            //channelCoordinates[0] = jo["RawData"][i];
                            joList09 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                        case "Inf":
                            //channelCoordinates[7] = jo["RawData"][i];
                            joList10 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                    }
                }

            }
        }

        public void boxGetValue()
        {
            //txtTestID.Text = TestNo.ToString();
            //txtTestType.Text = TestType.ToString();
            //txtTestDate.Text = TestDate.ToString();
            //txtTireSize.Text = TireSize.ToString();

            //txtTargetDeflection.Text = (Convert.ToDecimal(TireInflation.ToString())).ToString("0.00") + " in";
            //txtTireLoad.Text = (Convert.ToDecimal(TireLoad.ToString())).ToString("0.00") + " lb";
            //txtTireDia.Text = (Convert.ToDecimal(TireDia.ToString())).ToString("0.00") + " in";
            //txtTargetLoad.Text = (Convert.ToDecimal(TargetLoad.ToString())).ToString("0.00") + " lb";
            //txtTireInflation.Text = (Convert.ToDecimal(TargetDeflection.ToString())).ToString("0.00") + " psi";
            //txtTargetInflation.Text = (Convert.ToDecimal(TargetInflation.ToString())).ToString("0.00") + " psi";
            //txtVz.Text = (Convert.ToDecimal(VectorRatePz.ToString())).ToString("0.00") + " °/s";
            //txtVxy.Text = (Convert.ToDecimal(VectorRatePxy.ToString())).ToString("0.00") + " °/s";
            //txtVaz.Text = (Convert.ToDecimal(VectorRateAz.ToString())).ToString("0.00") + " °/s";

            //uiComboBox21.Text = TestType.ToString();
        }

        /// <summary>
        /// Dictionary转datatable
        /// </summary>
        /// <param name="str">Header字符串</param>
        /// <returns></returns>
        public static DataTable DictionaryToDataTableHeader(string str)
        {
            //DataTable dt01 = new DataTable("tableName01");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue; //取得最大数值

            Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(str);//旧键值对
            Dictionary<string, object> dic = new Dictionary<string, object>();//新键值对，空

            #region 字段声明

            object filler1 = null;

            //object TestNo = null; // 30 Test Number
            object Operators = null; // 15 Technician
            object Engineer = null;// 15
            object Customer = null;// 15
            object TestPurpose = null;  //30 was 10
            //object TestType = null;// 20
            object TestStandard = null;  // 20 Test standard
            object RequestDep = null;  // 15 request department, was SecChief
            object Requestor = null;// 15 
            object ProjectNum = null;// 15 
            //object TestDate = null;// 10 
            object TestTime = null;  // 8 Stored in Testing Program 
            object Manager = null;// 15
            object Mode = null;// 15
            object Purpose = null;// 15
            object Title = null;// 10
            object TestCompletionDate = null;
            object TestCompletionTime = null;
            object Feature = null;// 30
            object Specification = null; // 30
            object TestMachine = null;// 10
            object OldStyleTestDate = null;  // 10 Stored in Testing Program 
            object filler2 = null;

            //tire information
            object TireNumber = null;
            //object TireSize = null;
            object TireSN = null;
            object TireType = null;
            object LoadIndex = null; // was 4
            object SpeedLevel = null;
            object IsTubeLess = null;
            object Manufacture = null; // was 10
            object Brand = null;
            object Pattern = null;
            object TirePly = null;
            //object TireInflation = null;
            //object TireLoad = null;
            object TireRadius = null;
            //object TireDia = null;
            object TireWeight = null;
            object TRAWeight = null; // always PSI, Pounds, Inches
            object TireDate = null;
            object TireWeeks = null;
            object TireOrientation = null;
            object TireDesign = null;
            object TreadHardness = null;
            object RimOffset = null;
            object WheelWidth = null;
            object wheelSize = null;
            object wheelType = null;
            object Remarks = null;
            object filler3 = null;

            //Test condition
            object Temperature = null;
            object Humidity = null;
            object CNFFileName = null; // 30
            object SeqFileName = null; // 30
            object CtrlFileName = null; //8   Cardcage Control Setup File
            object RecipeFile = null; // 8   201 Test Setup

            //object TargetLoad = null;
            //object TargetDeflection = null;
            //object TargetInflation = null;
            //object VectorRatePz = null;
            //object VectorRatePxy = null;
            //object VectorRateAz = null;  // (English)
            object TestDuration = null; //
            object PlungerDiameter = null;
            object TestPlateHeight = null;
            object Platforms = null; //15
            object PinNo = null; //15

            //Plunger and BPO Data
            object Energy = null;
            object Force = null;
            object Penetration = null;  // English
            object filler4 = null;

            //unknow but leave it here
            object PropChan = null;
            object DummyInfo = null;
            object SpeedGate = null;// real48;
            object Restraint = null;//20
            object SledInfo = null;
            object SledType = null;//6
            object MIRAConfig = null;// 6
            object TstType = null;        // 0 = PAB; 1 = DAB
            object TCType = null;        // 0 = J Type Thermocouple 1 = K Type Thermocouple
            object Items = null;
            object HeaderDate = null;
            object PostTestRemarks = null;// 100
            object status = null;//22
            object filler5 = null;

            #endregion

            # region 获取指定键的值

            //--------------获取指定键的值
            json.TryGetValue("filler1", out filler1);

            json.TryGetValue("TestNo", out TestNo); // 30 Test Number
            json.TryGetValue("Operators", out Operators); // 15 Technician
            json.TryGetValue("Engineer", out Engineer);// 15
            json.TryGetValue("Customer", out Customer);// 15
            json.TryGetValue("TestPurpose", out TestPurpose);  //30 was 10
            json.TryGetValue("TestType", out TestType);// 20
            json.TryGetValue("TestStandard", out TestStandard);  // 20 Test standard
            json.TryGetValue("RequestDep", out RequestDep);  // 15 request department, was SecChief
            json.TryGetValue("Requestor", out Requestor);// 15 
            json.TryGetValue("ProjectNum", out ProjectNum);// 15 
            json.TryGetValue("TestDate", out TestDate);// 10 
            json.TryGetValue("TestTime", out TestTime);  // 8 Stored in Testing Program 
            json.TryGetValue("Manager", out Manager);// 15
            json.TryGetValue("Mode", out Mode);// 15
            json.TryGetValue("Purpose", out Purpose);// 15
            json.TryGetValue("Title", out Title);// 10
            json.TryGetValue("TestCompletionDate", out TestCompletionDate);
            json.TryGetValue("TestCompletionTime", out TestCompletionTime);
            json.TryGetValue("Feature", out Feature);// 30
            json.TryGetValue("Specification", out Specification); // 30
            json.TryGetValue("TestMachine", out TestMachine);// 10
            json.TryGetValue("OldStyleTestDate", out OldStyleTestDate);  // 10 Stored in Testing Program 
            json.TryGetValue("filler2", out filler2);

            //tire information
            json.TryGetValue("TireNumber", out TireNumber);
            json.TryGetValue("TireSize", out TireSize);
            json.TryGetValue("TireSN", out TireSN);
            json.TryGetValue("TireType", out TireType);
            json.TryGetValue("LoadIndex", out LoadIndex); // was 4
            json.TryGetValue("SpeedLevel", out SpeedLevel);
            json.TryGetValue("IsTubeLess", out IsTubeLess);
            json.TryGetValue("Manufacture", out Manufacture); // was 10
            json.TryGetValue("Brand", out Brand);
            json.TryGetValue("Pattern", out Pattern);
            json.TryGetValue("TirePly", out TirePly);
            json.TryGetValue("TireInflation", out TireInflation);
            json.TryGetValue("TireLoad", out TireLoad);
            json.TryGetValue("TireRadius", out TireRadius);
            json.TryGetValue("TireDia", out TireDia);
            json.TryGetValue("TireWeight", out TireWeight);
            json.TryGetValue("TRAWeight", out TRAWeight); // always PSI, Pounds, Inches
            json.TryGetValue("TireDate", out OldStyleTestDate);
            json.TryGetValue("TireWeeks", out TireWeeks);
            json.TryGetValue("TireOrientation", out TireOrientation);
            json.TryGetValue("TireDesign", out TireDesign);
            json.TryGetValue("TreadHardness", out TreadHardness);
            json.TryGetValue("RimOffset", out RimOffset);
            json.TryGetValue("WheelWidth", out WheelWidth);
            json.TryGetValue("wheelSize", out wheelSize);
            json.TryGetValue("wheelType", out wheelType);
            json.TryGetValue("Remarks", out Remarks);
            json.TryGetValue("filler3", out filler3);

            //Test condition
            json.TryGetValue("Temperature", out Temperature);
            json.TryGetValue("Humidity", out Humidity);
            json.TryGetValue("CNFFileName", out CNFFileName); // 30
            json.TryGetValue("SeqFileName", out SeqFileName); // 30
            json.TryGetValue("CtrlFileName", out CtrlFileName); //8   Cardcage Control Setup File
            json.TryGetValue("RecipeFile", out RecipeFile); // 8   201 Test Setup

            json.TryGetValue("TargetLoad", out TargetLoad);
            json.TryGetValue("TargetDeflection", out TargetDeflection);
            json.TryGetValue("TargetInflation", out TargetInflation);
            json.TryGetValue("VectorRatePz", out VectorRatePz);
            json.TryGetValue("VectorRatePxy", out VectorRatePxy);
            json.TryGetValue("VectorRateAz", out VectorRateAz);  // (English)
            json.TryGetValue("TestDuration", out TestDuration); //
            json.TryGetValue("PlungerDiameter", out PlungerDiameter);
            json.TryGetValue("TestPlateHeight", out TestPlateHeight);
            json.TryGetValue("Platforms", out Platforms); //15
            json.TryGetValue("PinNo", out PinNo); //15

            //Plunger and BPO Data
            json.TryGetValue("Energy", out Energy);
            json.TryGetValue("Force", out Force);
            json.TryGetValue("Penetration", out Penetration);  // English
            json.TryGetValue("filler4", out filler4);

            //unknow but leave it here
            json.TryGetValue("PropChan", out PropChan);
            json.TryGetValue("DummyInfo", out DummyInfo);
            json.TryGetValue("SpeedGate", out SpeedGate);// real48;
            json.TryGetValue("Restraint", out Restraint);//20
            json.TryGetValue("SledInfo", out SledInfo);
            json.TryGetValue("SledType", out SledType);//6
            json.TryGetValue("MIRAConfig", out MIRAConfig);// 6
            json.TryGetValue("TstType", out TstType);        // 0 = PAB; 1 = DAB
            json.TryGetValue("TCType", out TCType);        // 0 = J Type Thermocouple 1 = K Type Thermocouple
            json.TryGetValue("Items", out Items);
            json.TryGetValue("HeaderDate", out HeaderDate);
            json.TryGetValue("PostTestRemarks", out PostTestRemarks);// 100
            json.TryGetValue("status", out status);//22
            json.TryGetValue("filler5", out filler5);

            #endregion

            #region 给新的Dictionary赋值
            //--------给新的Dictionary赋值
            dic.Add("filler1", "");

            dic.Add("TestNo", TestNo);
            dic.Add("Operators", Operators);
            dic.Add("Engineer", Engineer);
            dic.Add("Customer", Customer);
            dic.Add("TestPurpose", TestPurpose);
            dic.Add("TestType", TestType);
            dic.Add("TestStandard", TestStandard);
            dic.Add("RequestDep", RequestDep);
            dic.Add("Requestor", Requestor);
            dic.Add("ProjectNum", ProjectNum);
            dic.Add("TestDate", TestDate);
            dic.Add("TestTime", TestTime);
            dic.Add("Manager", Manager);
            dic.Add("Mode", Mode);
            dic.Add("Purpose", Purpose);
            dic.Add("Title", Title);
            dic.Add("TestCompletionDate", TestCompletionDate);
            dic.Add("TestCompletionTime", TestCompletionTime);
            dic.Add("Feature", Feature);
            dic.Add("Specification", Specification);
            dic.Add("TestMachine", TestMachine);
            dic.Add("OldStyleTestDate", OldStyleTestDate);
            dic.Add("filler2", "");

            //tire information
            dic.Add("TireNumber", TireNumber);
            dic.Add("TireSize", TireSize);
            dic.Add("TireSN", TireSN);
            dic.Add("TireType", TireType);
            dic.Add("LoadIndex", LoadIndex);
            dic.Add("SpeedLevel", SpeedLevel);
            dic.Add("IsTubeLess", IsTubeLess);
            dic.Add("Manufacture", Manufacture);
            dic.Add("Brand", Brand);
            dic.Add("Pattern", Pattern);
            dic.Add("TirePly", TirePly);
            dic.Add("TireInflation", TireInflation);
            dic.Add("TireLoad", TireLoad);
            dic.Add("TireRadius", TireRadius);
            dic.Add("TireDia", TireDia);
            dic.Add("TireWeight", TireWeight);
            dic.Add("TRAWeight", TRAWeight);
            dic.Add("TireDate", TireDate);
            dic.Add("TireWeeks", TireWeeks);
            dic.Add("TireOrientation", TireOrientation);
            dic.Add("TireDesign", TireDesign);
            dic.Add("TreadHardness", TreadHardness);
            dic.Add("RimOffset", RimOffset);
            dic.Add("WheelWidth", WheelWidth);
            dic.Add("wheelSize", wheelSize);
            dic.Add("wheelType", wheelType);
            dic.Add("Remarks", Remarks);
            dic.Add("filler3", "");

            //Test condition
            dic.Add("Temperature", Temperature);
            dic.Add("Humidity", Humidity);
            dic.Add("CNFFileName", CNFFileName);
            dic.Add("SeqFileName", SeqFileName);
            dic.Add("CtrlFileName", CtrlFileName);
            dic.Add("RecipeFile", RecipeFile);

            dic.Add("TargetLoad", TargetLoad);
            dic.Add("TargetDeflection", TargetDeflection);
            dic.Add("TargetInflation", TargetInflation);
            dic.Add("VectorRatePz", VectorRatePz);
            dic.Add("VectorRatePxy", VectorRatePxy);
            dic.Add("VectorRateAz", VectorRateAz);
            dic.Add("TestDuration", TestDuration);
            dic.Add("PlungerDiameter", PlungerDiameter);
            dic.Add("TestPlateHeight", TestPlateHeight);
            dic.Add("Platforms", Platforms);
            dic.Add("PinNo", PinNo);

            //Plunger and BPO Data
            dic.Add("Energy", Energy);
            dic.Add("Force", Force);
            dic.Add("Penetration", Penetration);
            dic.Add("filler4", "");

            //unknow but leave it here
            dic.Add("PropChan", PropChan);
            dic.Add("DummyInfo", "");
            dic.Add("SpeedGate", SpeedGate);
            dic.Add("Restraint", Restraint);
            dic.Add("SledInfo", "");
            dic.Add("SledType", SledType);
            dic.Add("MIRAConfig", MIRAConfig);
            dic.Add("TstType", TstType);
            dic.Add("TCType", TCType);
            dic.Add("Items", "");
            dic.Add("HeaderDate", HeaderDate);
            dic.Add("PostTestRemarks", PostTestRemarks);
            dic.Add("status", status);
            dic.Add("filler5", "");

            #endregion

            DataTable dt = DicToTable(dic);
            return dt;
        }

        /// <summary>
        /// Dictionary转datatable
        /// </summary>
        /// <param name="dicDep">新Dictionary字符串</param>
        /// <returns></returns>
        public static DataTable DicToTable(Dictionary<string, object> dicDep)
        {
            DataTable dt = new DataTable();

            foreach (var colName in dicDep.Keys)
            {
                dt.Columns.Add(colName, typeof(string));
            }
            DataRow dr = dt.NewRow();

            foreach (KeyValuePair<string, object> item in dicDep)
            {
                dr[item.Key] = item.Value;
            }
            dt.Rows.Add(dr);
            return dt;
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTableTwo(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合,2个table合一起
        /// </summary>
        /// <param name="arrayListConfig"></param>
        /// <param name="arrayListRawData"></param>
        /// <returns></returns>
        public DataTable ToDataTableTwo02(string arrayListConfig, string arrayListRawData)
        {
            DataTable dataTable01 = new DataTable();  //实例化
            DataTable dataTable02 = new DataTable();  //实例化

            DataTable result;

            try
            {
                JavaScriptSerializer javaScriptSerializerConfig = new JavaScriptSerializer();
                javaScriptSerializerConfig.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList01 = javaScriptSerializerConfig.Deserialize<ArrayList>(arrayListConfig);

                JavaScriptSerializer javaScriptSerializerRawData = new JavaScriptSerializer();
                javaScriptSerializerRawData.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList02 = javaScriptSerializerRawData.Deserialize<ArrayList>(arrayListRawData);


                if (arrayList01.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList01)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable01;
                            return result;
                        }
                        //Columns
                        if (dataTable01.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable01.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable01.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable01.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }

                if (arrayList02.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList02)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable02;
                            return result;
                        }
                        //Columns
                        if (dataTable02.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable02.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable02.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable02.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }

                //getRowDataDtial_X();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            dataTable01.Columns.Add("Data", typeof(string));


            for (int j = 0; j < dataTable01.Rows.Count; j++)
            {
                dataTable01.Rows[j]["Data"] = j + 1 >=
                    dataTable02.Rows.Count
                    ? dataTable02.Rows[j]["Data"]
                    : dataTable02.Rows[j]["Data"];
            }
            return dataTable01;
        }

        public ArrayList getXChannelcountSelectArr()
        {
            //TestDuration* SampRate=取多少；获取取值数据，然后从data数组中逐一删掉
            //获取开始时间、持续时间、取样率数值，如果取样率<1则+1024，如果取样率>1则-1024

            //-------------------------------------------------------------------------------------------------

            //解析jaHeader数据，获取TestDuration-------------
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            Dictionary<string, object> jsonHeader = (Dictionary<string, object>)serializer.DeserializeObject(jaHeader);//旧键值对

            for (int i = 0; i < jsonHeader.Count; i++)
            {
                arrTestDuration.Add(jsonHeader["TestDuration"]);
            }

            //解析jaConfig数据，获取StartTime、SampRate----------
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayListConfig = javaScriptSerializer.Deserialize<ArrayList>(jaConfig);

            foreach (var item in arrayListConfig)
            {
                string itemStr = JsonConvert.SerializeObject(item);
                JavaScriptSerializer Jss = new JavaScriptSerializer();
                Dictionary<string, object> DicText = (Dictionary<string, object>)Jss.DeserializeObject(itemStr);
                string StartTime = DicText["StartTime"].ToString();
                string SampRate = DicText["SampRate"].ToString();

                arrStartTime.Add(StartTime);
                arrSampRate.Add(SampRate);
            }

            //获取 取多少的数量---------------
            for (int i = 0; i < 10; i++)//10个通道
            {
                if (Convert.ToDecimal(arrSampRate[i]) < 1)
                {
                    var oneSelect = Convert.ToDecimal(arrTestDuration[i]) * Convert.ToDecimal(arrSampRate[i]) * 1000;
                    countSelect.Add(oneSelect);//得到1个通道取多少的数量数组
                                               //timeChangeData.Add(Convert.ToDecimal(arrTestDuration[i]) / oneSelect * 1000);//每秒数据变化
                    timeChangeData = Convert.ToDecimal(arrTestDuration[i]) / oneSelect * 1000;//每秒数据变化
                }
                else
                {
                    var oneSelect = Convert.ToDecimal(arrTestDuration[i]) * Convert.ToDecimal(arrSampRate[i]);
                    countSelect.Add(oneSelect);
                    //timeChangeData.Add(Convert.ToDecimal(arrTestDuration[i]) / oneSelect * 1000);//每秒数据变化
                    timeChangeData = Convert.ToDecimal(arrTestDuration[i]) / oneSelect * 1000;//每秒数据变化
                }
            }
            //从data数组中删除对应数据
            return countSelect;
        }

        /// <summary>
        /// 判断字符，是否是数字或小数（利用正则表达式）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool IsAllNumber(string text)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(text) && !objTwoDotPattern.IsMatch(text) && !objTwoMinusPattern.IsMatch(text) && objNumberPattern.IsMatch(text);
        }

        public DataTable JsonToDataTable(string strJson)
        {
            DataTable dt = null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(strJson);
                JArray ja = (JArray)jo["RawData"];
                dt = ToDataTable(ja.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }

        public DataTable JsonToDataTableHead(string strJson)
        {
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(strJson);
                string Header = jo["Header"].ToString();
                dtHead = DictionaryToDataTableHeader(Header);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dtHead;
        }

        public static DataTable ToDataTable(string json)
        {
            DataTable table = new DataTable("table01");
            JArray array = JsonConvert.DeserializeObject(json) as JArray;//反序列化为数组
            var q = JsonConvert.DeserializeObject<dynamic>(json);
            if (array.Count > 0)
            {
                StringBuilder columns = new StringBuilder();

                JObject objColumns = array[0] as JObject;
                //构造表头
                foreach (JToken jkon in objColumns.AsEnumerable<JToken>())
                {
                    string name = ((JProperty)(jkon)).Name;
                    columns.Append(name + ",");
                    table.Columns.Add(name);
                }
                //向表中添加数据
                for (int i = 0; i < array.Count; i++)
                {
                    DataRow row = table.NewRow();
                    JObject obj = array[i] as JObject;
                    foreach (JToken jkon in obj.AsEnumerable<JToken>())
                    {

                        string name = ((JProperty)(jkon)).Name;
                        string value = ((JProperty)(jkon)).Value.ToString();
                        string result = System.Text.RegularExpressions.Regex.Replace(value, "[\r\n\t]", "");
                        string h = result.Replace(" ", "");

                        row[name] = h;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        /// <summary>
        /// 截图工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Point screenPoint = this.chtTestPlot.PointToScreen(new Point());
                Rectangle rect = new Rectangle(screenPoint, chtTestPlot.Size);
                Image img = new Bitmap(rect.Width, rect.Height);
                Graphics g = Graphics.FromImage(img);
                g.CopyFromScreen(rect.X - 1, rect.Y - 1, 0, 0, rect.Size);
                img.Save(@"C:\Users\wzy\Desktop\123.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 多线程加载控件
        /// </summary>
        /// <param name="path"></param>
        public void ZTAFileToChart(object path)
        {
            lock (obj)
            {
                if (path.ToString() != string.Empty)
                {

                    #region 曲线绘制

                    string x, y;

                    try
                    {
                        //多线程曲线加载控件
                        if (chtTestPlot.InvokeRequired)
                        {
                            Action action = () =>
                            {
                                //加载xy坐标值
                                try
                                {
                                    //添加曲线
                                    for (int i = 0; i < lsrawdata.Count; i++)
                                    {
                                        double x1 = lsrawtime[i];
                                        double y1 = lsrawdata[i];

                                        test.Points.AddXY(x1, y1);
                                    }
                                    test.Points.AddXY(lsrawtime.Max(), 0);

                                    int maxtime = 0;
                                    for (int ii = 0; ii < 10000; ii++)
                                    {
                                        maxtime += 10;
                                        if (maxtime > lsrawtime.Max())
                                        {
                                            if (maxtime < 20)
                                            {
                                                maxtime += 10;
                                            }
                                            else
                                            {
                                                maxtime += 20;
                                            }

                                            break;
                                        }
                                    }

                                    test.Points.AddXY(maxtime, 0);
                                    //加载曲线。
                                    chtTestPlot.Series.Add(test);
                                    //曲线名称加载。
                                    //chtTestPlot.ChartAreas[0].AxisX.Title = x + x.Unit(uc_units.Text);
                                    //chtTestPlot.ChartAreas[0].AxisY.Title = y + y.Unit(uc_units.Text);
                                    //设置x轴曲线偏移              
                                    chtTestPlot.ChartAreas[0].AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Auto;
                                    chtTestPlot.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Auto;
                                    chtTestPlot.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Auto;
                                    chtTestPlot.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                                    chtTestPlot.ChartAreas[0].AxisX.Minimum = 0;
                                    chtTestPlot.ChartAreas[0].AxisX.Interval = 5;

                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }
                            };
                            chtTestPlot.Invoke(action);
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        //必须执行加载控件
                        if (ucBtn_TN.InvokeRequired)
                        {
                            Action action = () =>
                            {
                                this.ucBtn_TN.Enabled = true;
                            };
                            ucBtn_TN.Invoke(action);
                        }
                        if (loadExt1.InvokeRequired)
                        {
                            Action act = () =>
                            {
                                //等待加载控件结束
                                this.loadExt1.Visible = false;
                                this.loadExt1.Active = false;
                            };
                            loadExt1.Invoke(act);
                        }
                        if (this.btn_Pages.InvokeRequired == true)
                        {
                            Action act = () =>
                            {
                                //等待加载控件才可以重新绘制曲线
                                this.btn_Pages.Enabled = true;
                            };
                            btn_Pages.Invoke(act);
                        }
                        if (this.btn_csv.InvokeRequired == true)
                        {
                            Action act = () =>
                            {
                                //原始数据导出
                                this.btn_csv.Enabled = true;
                            };
                            btn_csv.Invoke(act);
                        }
                    }

                    #endregion

                    #region 放大曲线

                    Glodal.ChartSeries.ChartSerie = test;

                    #endregion
                }
            }
        }

        /// <summary>
        /// 初始化界面元素
        /// </summary>
        public void InitUIMethod()
        {
            this.lab_Units.Text = StringParser.Parse(ResourceService.GetString("PlotPackageUtility_Units"));
            this.btn_Pages.Text = StringParser.Parse(ResourceService.GetString("PlotPackageUtility_btn_Pages"));
            this.containedButton1.Text = StringParser.Parse(ResourceService.GetString("Exit"));
        }

        /// <summary>
        /// 导出excel按钮是否可以使用
        /// </summary>
        public void ExcelButton(ref double? doumax, ref double? doumin)
        {
            try
            {
                if (this.com_max.Text != string.Empty && this.com_max.Text != string.Empty)
                {
                    string[] strmax = this.com_max.Text.Split('%');
                    string[] strmin = this.com_min.Text.Split('%');

                    doumax = (double.Parse(strmax[0])) / 100;
                    doumin = (double.Parse(strmin[0])) / 100;

                    _ = doumax > doumin ? this.btn_excel.Enabled = true : this.btn_excel.Enabled = false;
                }
                else
                {
                    this.btn_excel.Enabled = false;
                    return;
                }
            }
            catch
            {
                this.btn_excel.Enabled = false;
                return;
            }
        }

        /// <summary>
        /// 百分比坐标值
        /// </summary>
        public Dictionary<double, double[]> PercentXYValue(double[] dax, double[] day, double ymax, int mode)
        {
            double x, y, percent = 0;

            Dictionary<double, double[]> keyValues = new Dictionary<double, double[]>();

            for (int ii = 0; ii < 10; ii++)
            {
                percent = 0.1 + percent;

                x = 0.0;
                y = 0.0;

                for (int aa = 0; aa < dax.Length; aa++)
                {
                    if (mode == 1 || mode == 4)
                    {
                        //最大接近点
                        if (day[aa] > percent * ymax)
                        {
                            x = dax[aa];
                            y = day[aa];
                            break;
                        }
                    }
                    else
                    {
                        //最大接近点
                        if (day[aa] < percent * ymax)
                        {
                            x = dax[aa];
                            y = day[aa];
                            break;
                        }
                    }
                }

                double[] vs1 = new double[2];
                vs1[0] = x;
                vs1[1] = y;
                keyValues.Add(ii + 1, vs1);
            }
            return keyValues;
        }

        /// <summary>
        /// 缓存基类
        /// </summary>
        private class Dic
        {
            public List<double> listX = new List<double>();
            public List<double> listY = new List<double>();
        }

        /// <summary>
        /// 曲线分段
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        private Dictionary<int, Dic> GetGroup(DataPoint[] dp)
        {
            #region 字段定义

            double[] douarray = new double[dp.Length];
            double ymax, ymin;
            Dictionary<int, Dic> keyValuePairs = new Dictionary<int, Dic>();
            bool[] bol = new bool[] { false, false, false, false };
            Dic dic1 = new Dic();
            Dic dic2 = new Dic();
            Dic dic3 = new Dic();
            Dic dic4 = new Dic();

            #endregion

            #region 查询最大值&最小值

            //加载全部的y轴值
            for (int ii = 0; ii < dp.Length; ii++)
            {
                douarray[ii] = dp[ii].YValues[0];
            }
            //找出最大值
            ymax = douarray.Max();
            ymin = douarray.Min();

            #endregion

            #region 分段处理

            for (int ii = 0; ii < dp.Length; ii++)
            {
                //第1阶段
                if (!bol[0])
                {
                    if (dp[ii].YValues[0] < ymax)
                    {
                        dic1.listX.Add(dp[ii].XValue);
                        dic1.listY.Add(dp[ii].YValues[0]);
                    }
                    else
                    {
                        bol[0] = true;
                        keyValuePairs.Add(1, dic1);
                    }
                    continue;
                }
                //第2阶段
                if (!bol[1])
                {
                    if (dp[ii].YValues[0] > 0)
                    {
                        dic2.listX.Add(dp[ii].XValue);
                        dic2.listY.Add(dp[ii].YValues[0]);
                    }
                    else
                    {
                        bol[1] = true;
                        keyValuePairs.Add(2, dic2);
                    }
                    continue;
                }
                //第3阶段
                if (!bol[2])
                {
                    if (dp[ii].YValues[0] != ymin)
                    {
                        dic3.listX.Add(dp[ii].XValue);
                        dic3.listY.Add(dp[ii].YValues[0]);
                    }
                    else
                    {
                        bol[2] = true;
                        keyValuePairs.Add(3, dic3);
                    }
                    continue;
                }
                //第一阶段
                if (!bol[3])
                {
                    dic4.listX.Add(dp[ii].XValue);
                    dic4.listY.Add(dp[ii].YValues[0]);

                    if (ii == dp.Length - 1)
                    {
                        keyValuePairs.Add(4, dic4);
                    }
                    continue;
                }

            }

            #endregion

            return keyValuePairs;
        }

        /// <summary>
        /// 斜率计算
        /// </summary>
        /// <returns></returns>
        public double Slope(double[] xdouarr, double[] ydouarr, double? miny, double? maxy, int mode)
        {
            double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            //获取最接近区间的点，进行斜率计算
            for (int ii = 0; ii < xdouarr.Length; ii++)
            {
                switch (mode)
                {
                    case 1:
                        {
                            //最小接近点
                            if (ydouarr[ii] > miny && y1 == 0.0)
                            {
                                y1 = ydouarr[ii];
                                x1 = xdouarr[ii];
                            }
                            //最大接近点
                            if (ydouarr[ii] > maxy && y2 == 0.0)
                            {
                                y2 = ydouarr[ii];
                                x2 = xdouarr[ii];
                                break;
                            }
                        }
                        break;
                    case 2:
                        {
                            //最大接近点
                            if (ydouarr[ii] < maxy && y2 == 0.0)
                            {
                                y2 = ydouarr[ii];
                                x2 = xdouarr[ii];
                            }
                            //最小接近点
                            if (ydouarr[ii] < miny && y1 == 0.0)
                            {
                                y1 = ydouarr[ii];
                                x1 = xdouarr[ii];
                                break;
                            }
                        }
                        break;
                    case 3:
                        {
                            //最大接近点
                            if (ydouarr[ii] < maxy && y2 == 0.0)
                            {
                                y2 = ydouarr[ii];
                                x2 = xdouarr[ii];
                            }
                            //最小接近点
                            if (ydouarr[ii] < miny && y1 == 0.0)
                            {
                                y1 = ydouarr[ii];
                                x1 = xdouarr[ii];
                                break;
                            }
                        }
                        break;
                    case 4:
                        {
                            //最大接近点
                            if (ydouarr[ii] > maxy && y2 == 0.0)
                            {
                                y2 = ydouarr[ii];
                                x2 = xdouarr[ii];
                            }
                            //最小接近点
                            if (ydouarr[ii] > miny && y1 == 0.0)
                            {
                                y1 = ydouarr[ii];
                                x1 = xdouarr[ii];
                                break;
                            }
                        }
                        break;

                }
            }
            return (y2 - y1) / (x2 - x1);
        }

        public DataTable JsonToDataTableConfig(string strJson)
        {
            DataTable dt = null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(strJson);
                JArray ja = (JArray)jo["Config"];
                dt = ToDataTable(ja.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }
    }
    #endregion
}

