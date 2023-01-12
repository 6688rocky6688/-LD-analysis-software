
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using TMSI_MFLD.Basis;
using TMSI_MFLD.Basis.Glodal;
using TMSI_MFLD.Controls.DgvHelper;
using TMSI_MFLD.Excel;
using TMSI_MFLD.Forms.ShowDialog;
using zta_convert;
using static TMSI_MFLD.Basis.TransitionHelper;
using static TMSI_MFLD.Forms.ShowDialog.FrmMode;

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_PhotoReportUtility_HASETRI : Form
    {
        #region 字段定义

        //记录文件位置
        string _path;
        //返回的坐标外宽
        int x, y, width, hight;
        //控件坐标
        int x_dgv, y_dgv;
        //选定的表格列和行号
        int _cell, row;
        //全部的文件路径集合
        List<string> filepath = new List<string>();
        //解析的RowData集合
        Dictionary<string, List<DataTable>> rowhead_dt = new Dictionary<string, List<DataTable>>();
        //上下百分比集合
        List<double[]> percent = new List<double[]>();
        //斜率
        Dictionary<string, double> slopes = new Dictionary<string, double>();
        //全部的百分比
        Dictionary<string, List<double[]>> keyValuePairs = new Dictionary<string, List<double[]>>();
        //报告导出名称
        List<string> excelshreet = new List<string>();
        List<string> ls1 = new List<string>();
        //全部的解析数据
        Dictionary<string, List<double>> xanalysedata = new Dictionary<string, List<double>>();
        Dictionary<string, List<double>> yanalysedata = new Dictionary<string, List<double>>();
        //类型
        string type = string.Empty;
        //线程object
        object obj1 = new object();
        //csv格式 
        struct Csv
        {
            public string xname;
            public string yname;
            public double[] xvalue;
            public double[] yvalue;
        };
        Dictionary<string, Csv> csvdata = new Dictionary<string, Csv>();

        //SLR原始数据
        List<double> lsrawdata = new List<double>();
        List<double> lsrawxdata = new List<double>();
        List<double> lsrawtime = new List<double>();
        //滑移真实数据
        List<double> lsslipx = new List<double>();
        List<double> lsslipy = new List<double>();

        DataTable dthead = new DataTable();

        #region 动态控件

        UIComboBox com_percenet = new UIComboBox();

        #endregion

        #endregion

        #region 构造函数

        public Frm_PhotoReportUtility_HASETRI()
        {
            InitializeComponent();
        }

        #endregion 

        #region 事件定义

        /// <summary>
        /// 点击打开文件按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtn_TN_Click(object sender, EventArgs e)
        {
            int ii = 0;
            //输出文件路径
            FolderBrowserDialog dialog = new FolderBrowserDialog();//提示用户打开文件窗体
            //dialog.SelectedPath = @"C:\Users\wzy\Desktop\原始数据";
            dialog.Description = "Please select folder!";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.btn_slrload.Enabled = false;
                this.lab_Units.Text = "English";
                //获取文件夹的文件
                DirectoryInfo fdir = new DirectoryInfo(dialog.SelectedPath);
                FileInfo[] fileinfo = fdir.GetFiles();

                if (fileinfo.Length > 0)
                {
                    try
                    {
                        var dataTable = this.dgv_PhotoReportUtility.DataSource;
                        DataTable dt = dataTable as DataTable;

                        foreach (DataRow item in dt.Rows)
                        {
                            item["File Name"] = string.Empty;
                            item["Load"] = string.Empty;
                            item["Low Percent %"] = string.Empty;
                            item["High Percent %"] = string.Empty;
                            item["Type"] = string.Empty;
                            item["State"] = string.Empty;
                        }

                        foreach (var item in fileinfo)
                        {
                            string[] str = item.FullName.Split('.');
                            if (str[str.Length - 1] == "zta")
                            {
                                dt.Rows[ii]["File Name"] = item.FullName;
                                ii++;
                            }
                        }
                        this.dgv_PhotoReportUtility.DataSource = dt;
                        this.textBox1.Text = fdir.FullName;
                    }
                    catch
                    {
                    }
                }
                this.ucBtn_TN.Enabled = false;
                //多线程开始
                Thread thread = new Thread(ExportExcel);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        /// <summary>
        /// 导出csv文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conBut_csv_Click(object sender, EventArgs e)
        {
            if (this.ucBtn_TN.Enabled == false || this.textBox1.Text == "Test Number") return;
            if (csvdata.Count != 0)
            {
                //输出文件路径
                //FolderBrowserDialog dialog = new FolderBrowserDialog();//提示用户打开文件窗体
                //dialog.Description = "请选择文件路径";
                //if (dialog.ShowDialog() == DialogResult.OK)
                //{
                string[] path = this.textBox1.Text.Split("\\");
                foreach (string item in filepath)
                {
                    string[] fileName = item.Split('\\');
                    string[] name = fileName[fileName.Length - 1].Split('.');
                    try
                    {
                        SaveCSV(path[path.Length - 1], name[0] + ".csv", csvdata[item].xvalue, csvdata[item].yvalue, rowhead_dt[item][0]);
                    }
                    catch
                    {

                    }
                }
                //}
                Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Csv);
                frm_Sd_Excel.ShowDialog();
            }
        }
        /// <summary>
        /// 修改文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "Test Number")
            {
                textBox1.ForeColor = Color.Silver;
            }
            else
            {
                textBox1.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 导出全部的Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_excel_Click(object sender, EventArgs e)
        {
            //判断是否正在加载
            if (this.ucBtn_TN.Enabled == false || this.textBox1.Text == "Test Number") return;
            //添加文件名称
            Frm_FileName frm_FileName = new Frm_FileName();
            DialogResult dialogResult = frm_FileName.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;
            string filename = frm_FileName.FileName;
            if (filename == "") return;
            string[] thisfilepath = this.textBox1.Text.Split("\\");
            string Folder = Environment.CurrentDirectory + "\\" + "Reports" + "\\" + thisfilepath[thisfilepath.Length - 1]; // 文件夹路径
            // 判断文件夹是否存在
            if (!System.IO.Directory.Exists(Folder))
                System.IO.Directory.CreateDirectory(Folder); // 创建文件夹
            //文件名称
            string savepath = Folder + "\\" + filename + ".xls";
            if (File.Exists(savepath))
                File.Delete(savepath);

            #region 导出报告

            //字段
            Dictionary<int, List<double>> xdri = new Dictionary<int, List<double>>();
            Dictionary<int, List<double>> ydri = new Dictionary<int, List<double>>();
            DataTable dataTable = this.dgv_PhotoReportUtility.DataSource as DataTable;
            List<string> lstype = new List<string>();
            List<string> lsokpath = new List<string>();
            Dictionary<int, List<double>> directory = new Dictionary<int, List<double>>();
            int numPercenet = 0;
            double[] xymin = new double[2];
            slopes.Clear();
            keyValuePairs.Clear();
            excelshreet.Clear();
            string x, y;
            //计算斜率以及百分比
            foreach (DataRow dataRow in dataTable.Rows)
            {

                //斜率百分比
                double list = 0;
                double high = 0;
                try
                {
                    high = double.Parse(dataRow["High Percent %"].ToString()) / 100;
                    list = double.Parse(dataRow["Low Percent %"].ToString()) / 100;
                    List<double> Percenet = new List<double>();
                    Percenet.Add(list);
                    Percenet.Add(high);
                    directory.Add(numPercenet, Percenet);
                }
                catch
                {

                }
                numPercenet += 1;
                //查看种类
                string type = dataRow["Type"].ToString();
                //百分比必须正常
                if (list < high && list < 1)
                {
                    //首次加载类型判断
                    if (lstype.IndexOf(type) == -1)
                    {
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
                        lstype.Add(type); lsokpath.Add(dataRow["File Name"].ToString());
                        //根据进制选择

                        double[] xls = xanalysedata[dataRow["File Name"].ToString()].ToArray();
                        double[] yls = yanalysedata[dataRow["File Name"].ToString()].ToArray();

                        if (this.uc_units.Text == "SI")
                        {
                            //转换成公制
                            for (int ii = 0; ii < xanalysedata[dataRow["File Name"].ToString()].Count; ii++)
                            {
                                xls[ii] = xanalysedata[dataRow["File Name"].ToString()][ii].EnglishToSi(x);
                                yls[ii] = yanalysedata[dataRow["File Name"].ToString()][ii].EnglishToSi(y);
                            }
                        }
                        else if (this.uc_units.Text == "MKS")
                        {
                            //转换成mks
                            for (int ii = 0; ii < xanalysedata[dataRow["File Name"].ToString()].Count; ii++)
                            {
                                xls[ii] = xanalysedata[dataRow["File Name"].ToString()][ii].EnglishToMKS(x);
                                yls[ii] = yanalysedata[dataRow["File Name"].ToString()][ii].EnglishToMKS(y);
                            }
                        }
                        //没有时执行原始英制数据
                        double[] xdata = xls;
                        double[] ydata = yls;
                        xymin[0] = xls.Min();
                        xymin[1] = yls.Min();

                        double slope = Slope(xdata, ydata, ydata.Max() * list, ydata.Max() * high);
                        slopes.Add(dataRow["File Name"].ToString(), slope);
                        //找出10个百分点
                        List<double[]> keyValues = PercentXYValue(xdata, ydata, ydata.Max());
                        keyValuePairs.Add(dataRow["File Name"].ToString(), keyValues);
                        excelshreet.Add(dataRow["File Name"].ToString());
                    }
                }
            }
            int Cyclic = 0;
            if (rowhead_dt.Count != 0 && excelshreet.Count != 0)
            {
                List<DataTable> datas = new List<DataTable>();
                foreach (string path in filepath)
                {
                    if (excelshreet.IndexOf(path) != -1)
                    {
                        datas.Add(rowhead_dt[path][0]);
                        xdri.Add(Cyclic, csvdata[path].xvalue.ToList<double>());
                        ydri.Add(Cyclic, csvdata[path].yvalue.ToList<double>());
                        Cyclic++;
                    }
                }

                Excel.ExcelOperations.Excel(datas.ToArray(), keyValuePairs, slopes, savepath, excelshreet.ToArray(), this.uc_units.Text, lstype, this.uc_units.Text, directory, xymin, xdri, ydri);

                Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Success);
                frm_Sd_Excel.ShowDialog();
            }

            #endregion
        }
        /// </summary>
        /// 返回主界面
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conBut_exit_Click(object sender, EventArgs e)
        {
            Glodal.ExitFrmChanged.Exit = true;
        }

        /// <summary>
        ///频率设定 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int frequency;
            int.TryParse(this.textBox3.Text, NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out frequency);
            if (frequency < 0 || frequency > 25)
                textBox3.Text = "25";
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void Frm_PhotoReportUtility_Load(object sender, EventArgs e)
        {
            //数据源 
            DataTable dt = new DataTable();
            dt.Columns.Add("File Name");
            dt.Columns.Add("Load");
            dt.Columns.Add("Type");
            dt.Columns.Add("State");
            dt.Columns.Add("Low Percent %");
            dt.Columns.Add("High Percent %");
            //初始化数据(初始增加50行数据)
            for (int ii = 0; ii < 50; ii++)
            {
                DataRow r1 = dt.NewRow();
                dt.Rows.Add(r1);
            }
            //初始化下拉框
            com_percenet.Items.Add("10");
            com_percenet.Items.Add("20");
            com_percenet.Items.Add("30");
            com_percenet.Items.Add("40");
            com_percenet.Items.Add("50");
            com_percenet.Items.Add("60");
            com_percenet.Items.Add("70");
            com_percenet.Items.Add("80");
            com_percenet.Items.Add("90");
            //更新数据源
            this.dgv_PhotoReportUtility.DataSource = dt;
            //重绘表格样式
            DataGridViewHelper.SetStyle(dgv_PhotoReportUtility, DataGridViewAutoSizeColumnsMode.Fill);
            this.dgv_PhotoReportUtility.Columns[0].MinimumWidth = 600;
            this.dgv_PhotoReportUtility.Columns[0].Width = 600;
            //数据源 
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("File Name");
            dt1.Columns.Add("Type");
            dt1.Columns.Add("State");
            //初始化数据(初始增加50行数据)
            for (int ii = 0; ii < 5; ii++)
            {
                DataRow r11 = dt1.NewRow();
                dt1.Rows.Add(r11);
            }
            this.dgv_slr.DataSource = dt1;
            DataGridViewHelper.SetStyle(this.dgv_slr, DataGridViewAutoSizeColumnsMode.Fill);
            dgv_PhotoReportUtility.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_PhotoReportUtility.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13);
            this.dgv_slr.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13);
            this.dgv_PhotoReportUtility.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.dgv_slr.DefaultCellStyle.Font = new Font("Segoe UI", 12);
        }
        /// <summary>
        /// 选择表格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_PhotoReportUtility_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_PhotoReportUtility.SelectedCells.Count == 1 && dgv_PhotoReportUtility.SelectedCells != null)
            {
                DataGridViewCell dataGridViewCell = dgv_PhotoReportUtility.SelectedCells[0];

                #region 动态控件加载

                if (dataGridViewCell.ColumnIndex == 4 || dataGridViewCell.ColumnIndex == 5)
                {
                    if (textBox1.Text != "Test Number")
                    {
                        dgv_PhotoReportUtility.ClearSelection();

                        //返回坐标&外宽
                        BaseHelper.DgvCellCoordinate(dgv_PhotoReportUtility, out x, out y, out width, out hight);
                        //返回表格控件坐标
                        BaseHelper.DgvCoordinate(dgv_PhotoReportUtility, out x_dgv, out y_dgv);
                        string str = string.Format("{0},{1},{2},{3}", x.ToString(), y.ToString(), width.ToString(), hight.ToString());
                        Console.WriteLine(str);
                        //查找所选的列、行 号
                        BaseHelper.DgvCellRow(dgv_PhotoReportUtility, out row, out _cell);
                        //获取列名
                        switch (this.dgv_PhotoReportUtility.Columns[this.dgv_PhotoReportUtility.CurrentCell.ColumnIndex].HeaderText.ToString())
                        {
                            case "Low Percent %":
                                {
                                    if (dgv_PhotoReportUtility.Rows[row].Cells[0].Value.ToString() != null && dgv_PhotoReportUtility.Rows[row].Cells[0].Value.ToString() != string.Empty)
                                    {
                                        BaseHelper.DgvCellRow(dgv_PhotoReportUtility, out row, out _cell);
                                        //动态生成
                                        com_percenet.Enabled = true;
                                        com_percenet.RectColor = Color.DarkGray;
                                        com_percenet.Text = dataGridViewCell.Value.ToString();
                                        com_percenet.Location = new Point(x - x_dgv, y - y_dgv);
                                        com_percenet.Size = new Size(width, hight);
                                        com_percenet.Font = new Font("Segoe UI", 13F);
                                        //加载进控件内
                                        this.dgv_PhotoReportUtility.Controls.Add(com_percenet);
                                        com_percenet.Focus();//将光标定位在combox1上
                                        com_percenet.Leave -= ComboBox_Leave;
                                        com_percenet.Leave += ComboBox_Leave;
                                    }
                                }
                                break;
                            case "High Percent %":
                                {
                                    if (dgv_PhotoReportUtility.Rows[row].Cells[0].Value.ToString() != null && dgv_PhotoReportUtility.Rows[row].Cells[0].Value.ToString() != string.Empty)
                                    {
                                        //动态生成
                                        com_percenet.Enabled = true;
                                        com_percenet.RectColor = Color.DarkGray;
                                        com_percenet.Text = dataGridViewCell.Value.ToString();
                                        com_percenet.Location = new Point(x - x_dgv, y - y_dgv);
                                        com_percenet.Size = new Size(width, hight);
                                        com_percenet.Font = new Font("Segoe UI", 13F);
                                        //加载进控件内
                                        this.dgv_PhotoReportUtility.Controls.Add(com_percenet);
                                        com_percenet.Focus();//将光标定位在combox1上
                                        com_percenet.Leave -= ComboBox_Leave;
                                        com_percenet.Leave += ComboBox_Leave;
                                    }
                                    break;
                                }
                        }
                    }

                }
                else if (dataGridViewCell.ColumnIndex == 2 || dataGridViewCell.ColumnIndex == 3)
                {
                    this.dgv_PhotoReportUtility.Rows[dataGridViewCell.RowIndex].Selected = false;
                }

                #endregion
            }
        }
        /// <summary>
        /// slr加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clrload_Click(object sender, EventArgs e)
        {
            //用户选择打开的文件
            OpenFileDialog ofd = new OpenFileDialog();
            //等于false表示可以选择多个文件
            ofd.Multiselect = false;
            //用户需要打开的文件类型
            ofd.Filter = "raw File(*.zta)|*.zta";
            //设置绝对路径
            ofd.InitialDirectory = _path;
            //自动添加扩展名
            ofd.AddExtension = true;
            //打开窗体。
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.ucBtn_TN.Enabled = false;
                this.com_unitslr.Text = "English";
                this.btn_slrload.Enabled = false;
                this.textBox2.Text = ofd.FileName;
                this.textBox2.ForeColor = Color.Black;
                //先清除原本的表格
                DataTable dt = this.dgv_slr.DataSource as DataTable;
                dt.Rows[0]["File Name"] = ofd.FileName;
                dt.Rows[0]["Type"] = "";
                dt.Rows[0]["State"] = "Loading...";
                this.dgv_slr.DataSource = dt;
                //线程开始
                Thread thread = new Thread(Analysis);
                thread.IsBackground = true;
                thread.Start(ofd.FileName);
            }
        }
        /// <summary>
        /// 点击斜率设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_slip_Click(object sender, EventArgs e)
        {
            if (this.txt_slip.Text == "Input Slip")
            {
                this.txt_slip.Text = "";
                this.txt_slip.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 斜率设定失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_slip_Leave(object sender, EventArgs e)
        {
            if (this.txt_slip.Text == "" || this.txt_slip.Text == "Input Slip")
            {
                this.txt_slip.Text = "Input Slip";
                this.txt_slip.ForeColor = Color.Silver;
            }
        }
        /// <summary>
        /// 解析原始数据
        /// </summary>
        /// </summary>
        /// <param name="path"></param>
        private void Analysis(object path)
        {
            lock (obj1)
            {
                #region 解析文件

                //解析文件
                var zta = new ZTAFile(path.ToString());
                string ztaJsonStr = JsonConvert.SerializeObject(zta.DataV2, Formatting.Indented);
                //添加解析表格
                DataTable row = JsonToDataTable(ztaJsonStr);
                DataTable head = JsonToDataTableHead(ztaJsonStr);
                DataTable Config = JsonToDataTableConfig(ztaJsonStr);
                dthead.Clear();
                dthead = head;

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
                type = head.Rows[0]["TestType"].ToString();
                string load = head.Rows[0]["TargetLoad"].ToString();
                string TestDuration = head.Rows[0]["TestDuration"].ToString();
                string SampRate = Config.Rows[0]["SampRate"].ToString();
                //解析出来的采样频率
                double SampleFrequency = double.Parse(head.Rows[0]["SampleFrequency"].ToString());
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

                //加载类型
                if (this.dgv_slr.InvokeRequired)
                {
                    Action action = () =>
                    {
                        this.dgv_slr.Rows[0].Cells["Type"].Value = type;
                    };
                    this.dgv_slr.Invoke(action);
                }
                //计算截取开始个数
                int startnum = Convert.ToInt32(1024 + double.Parse(StartTime) * double.Parse(SampRate) * 1000);
                //单次时间
                double singletime = 1 / (double.Parse(SampRate) * 1000);
                //全部的次数
                int all = startnum + int.Parse((double.Parse(TestDuration) * double.Parse(SampRate) * 1000).ToString()) - 1;

                #region 比率输出

                //需要截取的比率
                double ratio = 1.0;
                if (SampleFrequency < 250 && SampleFrequency != 0)
                {
                    ratio = 250 / SampleFrequency;
                }

                #endregion

                try
                {
                    if (x != string.Empty)
                    {
                        //xy坐标数组类型
                        string[] xvaluearr = dicRawdata[x].ToArray();
                        string[] yvaluearr = dicRawdata[y].ToArray();
                        //xy坐标浮点数类型
                        double[] xvaluearrdou = new double[all];
                        double[] yvaluearrdou = new double[all];

                        List<double> xcsv = new List<double>();
                        List<double> ycsv = new List<double>();
                        for (int i = startnum; i < all + startnum; i++)
                        {
                            xvaluearrdou[i - startnum] = double.Parse(xvaluearr[i]);
                            yvaluearrdou[i - startnum] = double.Parse(yvaluearr[i]);
                        }
                        //先清除原来的数据
                        lsslipx.Clear();
                        lsslipy.Clear();
                        lsrawdata.Clear();
                        lsrawxdata.Clear();
                        lsrawtime.Clear();
                        //加载最真实的解析数据
                        lsslipx = xvaluearrdou.ToList();
                        lsslipy = yvaluearrdou.ToList();
                        //数组最大值
                        int maxindex = TransitionHelper.MaxIndex(yvaluearrdou);
                        //间隔次数
                        double cache = 0;
                        int interval = 0;
                        //开始加载数据
                        for (int i = 0; i < all - 1; i++)
                        {
                            if (ratio != 1.0)
                            {
                                cache += ratio;
                                string[] str = cache.ToString().Split('.');
                                if (str.Length >= 2)
                                {
                                    string first = str[1].Substring(0, 1);
                                    if (int.Parse(first) > 5) interval = int.Parse(str[0]) + 1;
                                    else interval = int.Parse(str[0]);
                                }
                                else interval = i;
                            }
                            else interval = i;
                            if (interval >= maxindex) break;
                            if (yvaluearrdou[interval] > 0.0)
                            {
                                switch (this.com_unitslr.Text)
                                {
                                    case "SI":
                                        xvaluearrdou[interval] = xvaluearrdou[interval].EnglishToSi(x);
                                        yvaluearrdou[interval] = yvaluearrdou[interval].EnglishToSi(y);
                                        break;
                                    case "MKS":
                                        xvaluearrdou[interval] = xvaluearrdou[interval].EnglishToMKS(x);
                                        yvaluearrdou[interval] = yvaluearrdou[interval].EnglishToMKS(y);
                                        break;
                                }
                                lsrawxdata.Add(xvaluearrdou[interval]);
                                lsrawdata.Add(yvaluearrdou[interval]);
                                lsrawtime.Add(interval * singletime);
                            }
                        }
                        lsrawdata.Add(0);
                        lsrawtime.Add(lsrawtime.Max());
                        lsrawdata.Add(0);
                        lsrawtime.Add(lsrawtime.Max() + 10);
                    }
                }
                finally
                {
                    //加载类型
                    if (this.dgv_slr.InvokeRequired)
                    {
                        Action action = () =>
                        {
                            this.dgv_slr.Rows[0].Cells["State"].Value = "Succeed";
                        };
                        this.dgv_slr.Invoke(action);
                    }
                    if (this.btn_excel.InvokeRequired)
                    {
                        Action action = () =>
                        {
                            this.btn_excel.Enabled = true;
                        };
                        this.btn_excel.Invoke(action);
                    }
                    if (this.btn_slrload.InvokeRequired)
                    {
                        Action action = () =>
                        {
                            this.btn_slrload.Enabled = true;
                        };
                        this.btn_slrload.Invoke(action);
                    }
                    if (this.txt_slip.InvokeRequired)
                    {
                        Action action = () =>
                        {
                            this.txt_slip.Enabled = true;
                        };
                        this.txt_slip.Invoke(action);
                    }
                    if (this.ucBtn_TN.InvokeRequired)
                    {
                        Action action = () =>
                        {
                            this.ucBtn_TN.Enabled = true;
                        };
                        this.ucBtn_TN.Invoke(action);
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 导出slr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brn_exslr_Click(object sender, EventArgs e)
        {
            if (this.btn_slrload.Enabled == false || this.textBox1.Text == "Test Number") return;

            //字段
            DataTable dataTable = this.dgv_PhotoReportUtility.DataSource as DataTable;
            List<string> lstype = new List<string>();
            List<string> filename = new List<string>();
            List<string> lsokpath = new List<string>();
            List<string> SpringRateBetween = new List<string>();
            Dictionary<string, List<double>> pairs = new Dictionary<string, List<double>>();
            List<DataTable> datas = new List<DataTable>();
            List<double> maxy = new List<double>();
            //先清除集合斜率
            slopes.Clear();
            string x = "Pz", y = "Fz";
            //计算斜率以及百分比
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //查看种类
                string type = dataRow["Type"].ToString();
                //判断类型
                if (type == "Kv")
                {
                    //斜率百分比
                    double list = 0;
                    double high = 0;
                    try
                    {
                        high = double.Parse(dataRow["High Percent %"].ToString()) / 100;
                        list = double.Parse(dataRow["Low Percent %"].ToString()) / 100;
                    }
                    catch { }
                    //百分比必须正常
                    if (list < high && list < 1)
                    {
                        //添加计算信息
                        lstype.Add(type);
                        //添加计算地址
                        lsokpath.Add(dataRow["File Name"].ToString());
                        //没有时执行原始英制数据
                        double[] xdata = xanalysedata[dataRow["File Name"].ToString()].ToArray();
                        double[] ydata = yanalysedata[dataRow["File Name"].ToString()].ToArray();
                        maxy.Add(xdata.Max().EnglishToSi("Pz"));
                        List<double> slope = Slope(ydata.Max() * list, ydata.Max() * high, xdata, ydata, this.uc_units.Text);
                        filename.Add(dataRow["File Name"].ToString());
                        pairs.Add(dataRow["File Name"].ToString(), slope);
                        datas.Add(rowhead_dt[dataRow["File Name"].ToString()][0]);
                    }
                }
            }
            if (datas.Count != 0)
            {
                //获取保存位置
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xls files(*.xls)|*.xls";//保存文件类型                    
                sfd.FileName = String.Format("{0:yyyyMMdd}", DateTime.Now);//保存文件名称
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = sfd.FileName.ToString();
                    //生成报告
                    Excel.ExcelOperations.Excel(localFilePath, this.uc_units.Text, pairs, datas, maxy, filename.ToArray());
                    Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Success);
                    frm_Sd_Excel.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 窗体加载完事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_PhotoReportUtility_Shown(object sender, EventArgs e)
        {
            this.dgv_PhotoReportUtility.Rows[0].Selected = false;
            this.dgv_slr.Rows[0].Selected = false;
        }
        private void ComboBox_Leave(object sender, EventArgs e)
        {
            BaseHelper.DgvCellRow(dgv_PhotoReportUtility, out row, out _cell);
            dgv_PhotoReportUtility.Rows[row].Cells[_cell].Value = com_percenet.Text;
            this.dgv_PhotoReportUtility.Controls.Clear();
        }
        /// <summary>
        /// 删除选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conBut_remove_Click(object sender, EventArgs e)
        {
            if (this.ucBtn_TN.Enabled == false || this.textBox1.Text == "Test Number") return;
            if (this.dgv_PhotoReportUtility.SelectedCells.Count == 1 && this.dgv_PhotoReportUtility.SelectedCells != null)
            {
                using (DataTable dt = this.dgv_PhotoReportUtility.DataSource as DataTable)
                {
                    //查询行号
                    for (int ii = 0; ii < dt.Rows.Count; ii++)
                    {
                        if (this.dgv_PhotoReportUtility.SelectedCells[0].Value.ToString() == dt.Rows[ii][0].ToString())
                        {
                            //删除选中行
                            dt.Rows.RemoveAt(ii);
                            //刷新表格
                            this.dgv_PhotoReportUtility.DataSource = dt;
                            //关闭选中焦点
                            this.dgv_PhotoReportUtility.Rows[ii].Selected = false;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 划移力量计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_slipforce_Click(object sender, EventArgs e)
        {
            if (this.btn_slrload.Enabled == false || this.textBox2.Text == "Test Number") return;

            #region 1.原始数据

            //类型判断
            string type = dthead.Rows[0]["TestType"].ToString();
            string x = "", y = "";
            if (type != "KLat" && type != "KLong" && type != "Kv") return;
            if (type == "KLat")
            {
                x = "Pxy"; y = "Fy";
            }
            if (type == "KLong")
            {
                x = "Pxy"; y = "Fx";
            }
            if (type == "Kv")
            {
                x = "Pz"; y = "Fz";
            }
            //斜率设定读取
            double slipsetting = 0;
            double.TryParse(this.txt_slip.Text, out slipsetting);
            //滑移力
            double slipforceindex = 0;

            #endregion

            #region 2.循环查找斜率

            List<double> vs = new List<double>();
            if (slipsetting != 0)
            {
                for (int i = 0; i < lsrawxdata.Count - 2; i++)
                {
                    double slopeDifference = 0.0;
                    //斜率差
                    if ((lsrawxdata[i + 1] - lsrawxdata[i]) != 0) slopeDifference = (lsrawdata[i + 1] - lsrawdata[i]) / (lsrawxdata[i + 1] - lsrawxdata[i]);
                    vs.Add(slopeDifference);
                    if (slopeDifference < slipsetting && slopeDifference != 0 && slopeDifference > 0)
                    {
                        //找出原始数据的滑移力
                        slipforceindex = i;
                        break;
                    }
                }
                if (slipforceindex == 0) slipforceindex = lsrawdata.Count;//没有滑移就最大
            }
            else slipforceindex = lsrawdata.Count;//不设置滑移就最大

            #endregion

            #region 3.小于比找寻出的滑移力的加载力每100公斤都进行计算。

            List<double> xvalue = new List<double>();
            List<double> yvalue = new List<double>();
            //100公斤等于220.46英镑
            double ibs = 220.46;
            int record = 1;
            switch (this.com_unitslr.Text)
            {
                case "English":
                    ibs = 220.46;
                    xvalue = lsrawxdata;
                    yvalue = lsrawdata;
                    break;
                case "SI":
                    ibs = 980;
                    for (int ii = 0; ii < lsrawxdata.Count; ii++)
                    {
                        xvalue.Add(lsrawxdata[ii].EnglishToSi(x));
                        yvalue.Add((lsrawdata[ii].EnglishToSi(y)));
                    }
                    break;
                case "MKS":
                    ibs = 100;
                    for (int ii = 0; ii < lsrawxdata.Count; ii++)
                    {
                        xvalue.Add(lsrawxdata[ii].EnglishToMKS(x));
                        yvalue.Add(lsrawdata[ii].EnglishToMKS(y));
                    }
                    break;
            }
            //是kv就250kg
            if (type == "Kv") ibs = ibs * 2.5;


            //集合存储
            List<double> silp = new List<double>();
            List<double> distance = new List<double>();
            List<double> slopestorage = new List<double>();
            List<double> slope = new List<double>();

            //找出第一个
            for (int ii = 0; ii < xvalue.Count; ii++)
            {
                if (xvalue[ii] > 0 && yvalue[ii] > 0)
                {
                    silp.Add(yvalue[ii]);
                    distance.Add(xvalue[ii]);
                    break;
                }
            }
            //循环查找
            for (int ii = 0; ii < slipforceindex - 2; ii++)
            {
                if ((yvalue[ii] > (record * ibs)))
                {
                    silp.Add(yvalue[ii]);
                    distance.Add(xvalue[ii]);
                    record += 1;
                }
                if (ii == slipforceindex - 3)
                {
                    if ((yvalue[ii] - silp[silp.Count - 1]) > 10)
                    {
                        silp.Add(yvalue[ii]);
                        distance.Add(xvalue[ii]);
                    }
                    break;
                }
            }

            #endregion

            #region 4.开始计算

            for (int i = 0; i < silp.Count - 1; i++)
            {
                slope.Add((silp[i + 1] - silp[i]) / (distance[i + 1] - distance[i]));
            }

            #endregion

            #region 5.导出报告

            //保存对象
            SaveFileDialog sfd = new SaveFileDialog();
            //保存文件类型
            sfd.Filter = "xls files(*.xls)|*.xls";
            //保存文件名称
            sfd.FileName = String.Format("{0:yyyyMMdd}", DateTime.Now);
            //加扩展名
            sfd.AddExtension = true;
            //show
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                //保存文件路径
                string localFilePath = sfd.FileName.ToString();
                //生成报告
                ExcelOperations.Excel(localFilePath, type, this.com_unitslr.Text, distance, silp, slope, dthead);

                Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Success);
                frm_Sd_Excel.ShowDialog();
            }

            #endregion
        }

        #endregion

        #region 方法定义

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

        public DataTable JsonToDataTableHead(string strJson)
        {
            DataTable dt = null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(strJson);
                string Header = jo["Header"].ToString();
                dt = DictionaryToDataTableHeader(Header);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
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
            object SampleFrequency = null;//22
            object filler5 = null;
            //样本类型
            object TestNo = null;
            object TestType = null;
            object TestDate = null;
            object TireSize = null;
            object TireInflation = null;
            object TireLoad = null;
            object TireDia = null;
            object TargetLoad = null;
            object TargetDeflection = null;
            object TargetInflation = null;
            object VectorRatePz = null;
            object VectorRatePxy = null;
            object VectorRateAz = null;  // (English)

            #endregion

            #region 获取指定键的值

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
            json.TryGetValue("SampleFrequency", out SampleFrequency);//22
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
            dic.Add("SampleFrequency", SampleFrequency);
            dic.Add("filler5", "");

            #endregion

            DataTable dt = DicToTable(dic);
            return dt;
        }

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

        //导出csv
        public void SaveCSV(string path, string fileName, double[] XList, double[] YList, DataTable data)
        {
            double[] XLS = new double[XList.Length];
            double[] YLS = new double[XList.Length];
            string x = "Pxy", y = "Fy";
            int dtrownum = 0;
            StringBuilder sb = new StringBuilder();
            switch (data.Rows[0]["TestType"].ToString())
            {
                case "KLat":
                    if (this.uc_units.Text == "English") { sb.Append("in,Ibs"); }
                    else if (this.uc_units.Text == "SI") { sb.Append("mm,n"); }
                    else { sb.Append("mm,kg"); };
                    x = "Pxy"; y = "Fy";
                    break;
                case "KLong":
                    if (this.uc_units.Text == "English") { sb.Append("in,Ibs"); }
                    else if (this.uc_units.Text == "SI") { sb.Append("mm,n"); }
                    else { sb.Append("mm,kg"); };
                    x = "Pxy"; y = "Fx";
                    break;
                case "Ktor":
                    if (this.uc_units.Text == "English") { sb.Append("DEG,Ibs.M"); }
                    else if (this.uc_units.Text == "SI") { sb.Append("DEG,N.M"); }
                    else { sb.Append("°,N.M"); };
                    x = "Az"; y = "Mz";
                    break;
                case "Kv":
                    if (this.uc_units.Text == "English") { sb.Append("in,Ibs"); }
                    else if (this.uc_units.Text == "SI") { sb.Append("mm,n"); }
                    else { sb.Append("mm,kg"); };
                    x = "Pz"; y = "Fz";
                    break;
            }
            sb.Append("\r\n");
            List<string> columnNameList = new List<string>();
            foreach (DataColumn col in data.Columns)
            {
                columnNameList.Add(col.ColumnName);//获取到DataColumn列对象的列名
            }
            int jump = 0;
            for (int ii = 0; ii < XList.Length; ii++)
            {
                if (ii < XList.Length - 10)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if (XList[ii] == XList[ii + i])
                        {
                            jump += 1;
                        }
                    }
                }
                if (jump == 9) break;
                else jump = 0;
                if (this.uc_units.Text == "SI")
                {
                    //转换成公制
                    XLS[ii] = XList[ii].EnglishToSi(x);
                    YLS[ii] = YList[ii].EnglishToSi(y);
                }
                else if (this.uc_units.Text == "MKS")
                {
                    //转换成mks
                    XLS[ii] = XList[ii].EnglishToMKS(x);
                    YLS[ii] = YList[ii].EnglishToMKS(y);
                }
                else
                {
                    XLS[ii] = XList[ii];
                    YLS[ii] = YList[ii];
                }
                sb.Append(XLS[ii].ToString());
                sb.Append(",");
                sb.Append(YLS[ii].ToString());
                sb.Append(",,");

                if (columnNameList.Count > dtrownum)
                {
                    sb.Append(columnNameList[dtrownum]);
                    sb.Append(",");
                    sb.Append(data.Rows[0][dtrownum].ToString());
                    sb.Append(",");
                    dtrownum++;
                }
                sb.Append("\r\n");
            }
            string str = data.Rows[0]["TireDesign"].ToString();
            string msg = sb.ToString();

            SaveCSV(path, fileName, msg);
        }

        public void SaveCSV(string path, string fileName, string msg)
        {
            string Folder = Environment.CurrentDirectory + "\\" + "RawData" + "\\" + path; // 文件夹路径
                                                                                           // 判断文件夹是否存在
            if (!System.IO.Directory.Exists(Folder))
                System.IO.Directory.CreateDirectory(Folder); // 创建文件夹
            fileName = Folder + "\\" + fileName;
            if (File.Exists(fileName))
                File.Delete(fileName);


            using (TextWriter fw = new StreamWriter(fileName, true)) // 以有序字符写入
            {
                fw.WriteLine(msg); // 写入数据
            }
        }

        public void ExportExcel()
        {
            Thread.Sleep(2000);
            #region 初始
            //清空
            slopes.Clear();
            keyValuePairs.Clear();
            percent.Clear();
            filepath.Clear();
            rowhead_dt.Clear();
            excelshreet.Clear();
            csvdata.Clear();
            ls1.Clear();
            xanalysedata.Clear();
            yanalysedata.Clear();
            int ii = 0;

            #endregion
            //变量
            bool bit = false;

            //遍历表格中全部的文件路径以及计算的百分比
            foreach (DataGridViewRow row in this.dgv_PhotoReportUtility.Rows)
            {
                if (row.Cells["File Name"].Value as string != null && row.Cells["File Name"].Value as string != string.Empty)
                {
                    filepath.Add(row.Cells["File Name"].Value as string);
                    if (this.dgv_PhotoReportUtility.InvokeRequired)
                    {
                        Action action = () => { this.dgv_PhotoReportUtility.Rows[row.Index].Cells["State"].Value = "loading..."; ; };
                        this.dgv_PhotoReportUtility.Invoke(action);
                    }
                }
            }

            //依次解析文件
            foreach (string path in filepath)
            {
                #region 解析文件
                int[] aaaa = new int[100000];
                int[] bbbb = new int[100000];
                //解析文件
                var zta = new ZTAFile(path.ToString());
                string ztaJsonStr = JsonConvert.SerializeObject(zta.DataV2, Formatting.Indented);
                //添加解析表格
                DataTable row = JsonToDataTable(ztaJsonStr);
                DataTable head = JsonToDataTableHead(ztaJsonStr);
                DataTable Config = JsonToDataTableConfig(ztaJsonStr);
                //解析表格缓存
                List<DataTable> dataTables = new List<DataTable>();
                dataTables.Add(head); dataTables.Add(row);
                rowhead_dt.Add(path, dataTables);

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
                //查找报告类型。
                string type = head.Rows[0]["TestType"].ToString();
                string load = head.Rows[0]["TargetLoad"].ToString();
                double SampleFrequency = double.Parse(this.textBox3.Text);
                //xy轴
                string x, y;
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
                //简化数据去重复
                if (x != string.Empty)
                {
                    //xy坐标数组类型
                    string[] xvaluearr = dicRawdata[x].ToArray();
                    string[] yvaluearr = dicRawdata[y].ToArray();
                    //xy坐标浮点数类型
                    double[] xvaluearrdou = new double[xvaluearr.Length];
                    double[] yvaluearrdou = new double[yvaluearr.Length];
                    //xy坐标值类型
                    double xvalue = 0, yvalue = 0, xcsvd = 0, ycsvd = 0;
                    List<double> xcsv = new List<double>();
                    List<double> ycsv = new List<double>();

                    for (int i = 0; i < xvaluearr.Length - 1; i++)
                    {
                        xvaluearrdou[i] = double.Parse(xvaluearr[i]);
                        yvaluearrdou[i] = double.Parse(yvaluearr[i]);
                    }
                    //数组最大值
                    int maxindex = TransitionHelper.MaxIndex(yvaluearrdou);
                    //间隔次数
                    double cache = 0;
                    int interval = 0;
                    //最大值
                    double ymax = yvaluearrdou.Max();

                    #region 比率输出

                    //需要截取的比率
                    double ratio = 1.0;

                    if (SampleFrequency < 25 && SampleFrequency != 0)
                    {
                        ratio = 25 / SampleFrequency;
                    }

                    #endregion

                    for (int i = 1024; i < xvaluearr.Length - 3; i++)
                    {
                        if (ratio != 1.0)
                        {
                            cache += ratio;
                            string[] str = cache.ToString().Split('.');
                            if (str.Length >= 2)
                            {
                                string first = str[1].Substring(0, 1);
                                if (int.Parse(first) > 5) interval = int.Parse(str[0]) + 1 + i;
                                else interval = int.Parse(str[0]) + i;
                            }
                            else interval = int.Parse(str[0]) + i;
                        }
                        else interval = i;
                        if (interval < xvaluearr.Length - 1)
                        {
                            if (interval < maxindex)
                            {
                                try
                                {
                                    if (Convert.ToDouble(xvaluearr[interval]) != Convert.ToDouble(xvaluearr[interval + 1]))
                                    {
                                        //英制数据
                                        xvalue = Convert.ToDouble(Convert.ToDouble(xvaluearr[interval]).ToString(""));
                                        yvalue = Convert.ToDouble(Convert.ToDouble(yvaluearr[interval]).ToString(""));
                                        //数组存储
                                        xdou.Add(xvalue);
                                        ydou.Add(yvalue);
                                    }
                                }
                                catch { }
                            }
                            if (xcsv.Count < xvaluearr.Length)
                            {
                                //英制数据
                                xcsvd = Convert.ToDouble(Convert.ToDouble(xvaluearr[interval]).ToString(""));
                                ycsvd = Convert.ToDouble(Convert.ToDouble(yvaluearr[interval]).ToString(""));
                                //数组存储
                                xcsv.Add(xcsvd);
                                ycsv.Add(ycsvd);
                            }
                        }
                        bbbb[i] = interval;
                    }
                    //csv赋值
                    Csv csv = new Csv();
                    csv.xname = x;
                    csv.yname = y;
                    csv.xvalue = xcsv.ToArray();
                    csv.yvalue = ycsv.ToArray();
                    csvdata.Add(path, csv);
                    //有效值赋值
                    xanalysedata.Add(path, xdou);
                    yanalysedata.Add(path, ydou);
                }
                else
                {
                    bit = true;
                    DgvStateErr(ii);
                }

                if (this.dgv_PhotoReportUtility.InvokeRequired)
                {
                    Action action = () => { this.dgv_PhotoReportUtility.Rows[ii].Cells["Type"].Value = type; };
                    this.dgv_PhotoReportUtility.Invoke(action);
                }
                if (this.dgv_PhotoReportUtility.InvokeRequired)
                {
                    Action action = () =>
                    {
                        double loaddou = double.Parse(load);
                        loaddou = loaddou.EnglishToSi(Units.kg);
                        this.dgv_PhotoReportUtility.Rows[ii].Cells["Load"].Value = loaddou.ToString("f1");
                    };
                    this.dgv_PhotoReportUtility.Invoke(action);
                }

                if (!bit) DgvStateSuc(ii);
                bit = false;
                ii++;
                #endregion
            }

            #region 刷新控件

            if (this.ucBtn_TN.InvokeRequired)
            {
                Action action = () => { this.ucBtn_TN.Enabled = true; };
                this.ucBtn_TN.Invoke(action);
            }
            if (this.conBut_remove.InvokeRequired)
            {
                Action action = () => { this.conBut_remove.Enabled = true; };
                this.conBut_remove.Invoke(action);
            }
            if (this.btn_slrload.InvokeRequired)
            {
                Action action = () => { this.btn_slrload.Enabled = true; };
                this.btn_slrload.Invoke(action);
            }

            #endregion
        }

        public void DgvStateErr(int ii)
        {
            if (this.dgv_PhotoReportUtility.InvokeRequired)
            {
                Action action = () => { this.dgv_PhotoReportUtility.Rows[ii].Cells["State"].Value = "Error"; ; };
                this.dgv_PhotoReportUtility.Invoke(action);
            }
        }

        public void DgvStateSuc(int ii)
        {
            if (this.dgv_PhotoReportUtility.InvokeRequired)
            {
                Action action = () => { this.dgv_PhotoReportUtility.Rows[ii].Cells["State"].Value = "Succeed"; ; };
                this.dgv_PhotoReportUtility.Invoke(action);
            }
        }

        /// <summary>
        /// 斜率计算
        /// </summary>
        /// <returns></returns>
        public double Slope(double[] xdouarr, double[] ydouarr, double miny, double maxy)
        {
            double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            //获取最接近区间的点，进行斜率计算
            for (int ii = 0; ii < xdouarr.Length; ii++)
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
            if (y2 == 0.0)
            {
                y2 = ydouarr.Max();
                int max = MaxIndex(ydouarr);
                x2 = xdouarr[max];
            }
            return (y2 - y1) / (x2 - x1);
        }
        /// <summary>
        /// slr斜率计算
        /// </summary>
        /// <returns></returns>
        public List<double> Slope(double miny, double maxy, double[] xdouarr, double[] ydouarr, string unit)
        {
            double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            string x = "Pz", y = "Fz";
            //获取最接近区间的点，进行斜率计算
            for (int ii = 0; ii < xdouarr.Length; ii++)
            {
                //最小接近点
                if (ydouarr[ii] > miny && y1 == 0.0)
                {
                    switch (unit)
                    {
                        case "English":
                            y1 = ydouarr[ii];
                            x1 = xdouarr[ii];
                            break;
                        case "SI":
                            y1 = ydouarr[ii].EnglishToSi(y);
                            x1 = xdouarr[ii].EnglishToSi(x);
                            break;
                        case "MKS":
                            y1 = ydouarr[ii].EnglishToMKS(y);
                            x1 = xdouarr[ii].EnglishToMKS(x);
                            break;
                    }
                }
                //最大接近点
                if (ydouarr[ii] > maxy && y2 == 0.0)
                {
                    switch (unit)
                    {
                        case "English":
                            y2 = ydouarr[ii];
                            x2 = xdouarr[ii];
                            break;
                        case "SI":
                            y2 = ydouarr[ii].EnglishToSi(y);
                            x2 = xdouarr[ii].EnglishToSi(x);
                            break;
                        case "MKS":
                            y2 = ydouarr[ii].EnglishToMKS(y);
                            x2 = xdouarr[ii].EnglishToMKS(x);
                            break;
                    }
                    break;
                }
            }
            List<double> ret = new List<double>()
            {
                (y2 - y1) / (x2 - x1),
                x2,x1,y2,y1
            };
            return ret;
        }

        /// <summary>
        /// 百分比坐标值
        /// </summary>
        public List<double[]> PercentXYValue(double[] dax, double[] day, double ymax)
        {
            double x, y, percent = 0;

            List<double[]> keyValues = new List<double[]>();

            for (int ii = 0; ii < 10; ii++)
            {
                percent = 0.1 + percent;

                x = 0.0;
                y = 0.0;

                for (int aa = 0; aa < dax.Length; aa++)
                {
                    //最大接近点
                    if (day[aa] > percent * ymax && percent < 9)
                    {
                        x = dax[aa];
                        y = day[aa];
                        break;
                    }
                    if (ii == 9)
                    {
                        x = dax.Max();
                        y = day.Max();
                        break;
                    }
                }
                double[] vs1 = new double[2];
                vs1[0] = x;
                vs1[1] = y;
                keyValues.Add(vs1);
            }
            return keyValues;
        }

        #endregion
    }
}
