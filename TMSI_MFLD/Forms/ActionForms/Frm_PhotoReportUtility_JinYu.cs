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
using static TMSI_MFLD.Forms.ShowDialog.FrmMode;

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_PhotoReportUtility_JinYu : Form
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

        public Frm_PhotoReportUtility_JinYu()
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
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
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
                            item["Low Percenet %"] = string.Empty;
                            item["High Percenet %"] = string.Empty;
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
            if (this.ucBtn_TN.Enabled == false) return;

            #region 导出报告

            //字段
            DataTable dataTable = this.dgv_PhotoReportUtility.DataSource as DataTable;
            List<string> lstype = new List<string>();
            List<string> lsokpath = new List<string>();
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
                    high = double.Parse(dataRow["High Percenet %"].ToString()) / 100;
                    list = double.Parse(dataRow["Low Percenet %"].ToString()) / 100;
                }
                catch
                {

                }
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

                        //转换成公制
                        for (int ii = 0; ii < xanalysedata[dataRow["File Name"].ToString()].Count; ii++)
                        {
                            xanalysedata[dataRow["File Name"].ToString()][ii] = xanalysedata[dataRow["File Name"].ToString()][ii].EnglishToSi(x);
                            yanalysedata[dataRow["File Name"].ToString()][ii] = yanalysedata[dataRow["File Name"].ToString()][ii].EnglishToSi(y);
                        }
                        //没有时执行原始英制数据
                        double[] xdata = xanalysedata[dataRow["File Name"].ToString()].ToArray();
                        double[] ydata = yanalysedata[dataRow["File Name"].ToString()].ToArray();
                        double slope = Slope(xdata, ydata, ydata.Max() * list, ydata.Max() * high);
                        slopes.Add(dataRow["File Name"].ToString(), slope);
                        //找出10个百分点
                        List<double[]> keyValues = PercentXYValue(xdata, ydata, ydata.Max());
                        keyValuePairs.Add(dataRow["File Name"].ToString(), keyValues);
                        excelshreet.Add(dataRow["File Name"].ToString());
                    }
                }
            }
            if (rowhead_dt.Count != 0 && excelshreet.Count != 0)
            {
                //获取保存位置
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xls files(*.xls)|*.xls";//保存文件类型
                string localFilePath = sfd.FileName.ToString();
                sfd.FileName = String.Format("{0:yyyyMMdd}", DateTime.Now);//保存文件名称
                sfd.AddExtension = true;
                DialogResult result = sfd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    List<DataTable> datas = new List<DataTable>();
                    foreach (string path in filepath)
                    {
                        if (excelshreet.IndexOf(path) != -1)
                        {
                            datas.Add(rowhead_dt[path][0]);
                        }
                    }

                    Excel.ExcelOperations.ExcelJinYu(datas.ToArray(), keyValuePairs, slopes, result, sfd, excelshreet.ToArray(),"GB" , lstype);

                    Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Success);
                    frm_Sd_Excel.ShowDialog();
                }
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
            dt.Columns.Add("Low Percenet %");
            dt.Columns.Add("High Percenet %");
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
            dgv_PhotoReportUtility.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_PhotoReportUtility.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13);
            this.dgv_PhotoReportUtility.DefaultCellStyle.Font = new Font("Segoe UI", 12);
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
                            case "Low Percenet %":
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
                            case "High Percenet %":
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
        /// 窗体加载完事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_PhotoReportUtility_Shown(object sender, EventArgs e)
        {
            this.dgv_PhotoReportUtility.Rows[0].Selected = false;
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
        /// 金宇企业标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_excel_qb_Click(object sender, EventArgs e)
        {
            if (this.ucBtn_TN.Enabled == false) return;

            #region 导出报告

            //字段
            DataTable dataTable = this.dgv_PhotoReportUtility.DataSource as DataTable;
            List<string> lstype = new List<string>();
            List<string> lsokpath = new List<string>();
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
                    high = double.Parse(dataRow["High Percenet %"].ToString()) / 100;
                    list = double.Parse(dataRow["Low Percenet %"].ToString()) / 100;
                }
                catch
                {

                }
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

                        //转换成公制
                        for (int ii = 0; ii < xanalysedata[dataRow["File Name"].ToString()].Count; ii++)
                        {
                            xanalysedata[dataRow["File Name"].ToString()][ii] = xanalysedata[dataRow["File Name"].ToString()][ii].EnglishToSi(x);
                            yanalysedata[dataRow["File Name"].ToString()][ii] = yanalysedata[dataRow["File Name"].ToString()][ii].EnglishToSi(y);
                        }
                        //没有时执行原始英制数据
                        double[] xdata = xanalysedata[dataRow["File Name"].ToString()].ToArray();
                        double[] ydata = yanalysedata[dataRow["File Name"].ToString()].ToArray();
                        double slope = SlopeJinYu(xdata, ydata, xdata.Max() * list, xdata.Max() * high);
                        slopes.Add(dataRow["File Name"].ToString(), slope);
                        //找出10个百分点
                        List<double[]> keyValues = PercentXYValue(xdata, ydata, ydata.Max());
                        keyValuePairs.Add(dataRow["File Name"].ToString(), keyValues);
                        excelshreet.Add(dataRow["File Name"].ToString());
                    }
                }
            }
            if (rowhead_dt.Count != 0 && excelshreet.Count != 0)
            {
                //获取保存位置
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xls files(*.xls)|*.xls";//保存文件类型
                string localFilePath = sfd.FileName.ToString();
                sfd.FileName = String.Format("{0:yyyyMMdd}", DateTime.Now);//保存文件名称
                sfd.AddExtension = true;
                DialogResult result = sfd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    List<DataTable> datas = new List<DataTable>();
                    foreach (string path in filepath)
                    {
                        if (excelshreet.IndexOf(path) != -1)
                        {
                            datas.Add(rowhead_dt[path][0]);
                        }
                    }

                    Excel.ExcelOperations.ExcelJinYu(datas.ToArray(), keyValuePairs, slopes, result, sfd, excelshreet.ToArray(), "GB", lstype);

                    Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Success);
                    frm_Sd_Excel.ShowDialog();
                }
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
            string x = "Pxy", y = "Fy";
            int dtrownum = 0;
            StringBuilder sb = new StringBuilder();
            switch (data.Rows[0]["TestType"].ToString())
            {
                case "KLat":
                    sb.Append("Ibs,in");
                    sb.Append("N,mm");
                    sb.Append("N,mm");
                    x = "Pxy"; y = "Fy";
                    break;
                case "KLong":
                    sb.Append("Ibs,in"); sb.Append("N,mm");
                    sb.Append("N,mm");
                    x = "Pxy"; y = "Fx";
                    break;
                case "Ktor":
                    sb.Append("Ibs.M,DEG");
                    sb.Append("N.M,DEG");
                    sb.Append("N.M,°");
                    x = "Az"; y = "Mz";
                    break;
                case "Kv":
                    sb.Append("Ibs,in");
                    sb.Append("N,mm");

                    sb.Append("N,mm");
                    x = "Pz"; y = "Fz";
                    break;
                    sb.Append("\r\n");
                    List<string> columnNameList = new List<string>();
                    foreach (DataColumn col in data.Columns)
                    {
                        columnNameList.Add(col.ColumnName);//获取到DataColumn列对象的列名
                    }
                    for (int ii = 0; ii < XList.Length; ii++)
                    {

                        //转换成公制
                        XList[ii] = XList[ii].EnglishToSi(x);
                        YList[ii] = YList[ii].EnglishToSi(y);

                        //转换成mks
                        XList[ii] = XList[ii].EnglishToMKS(x);
                        YList[ii] = YList[ii].EnglishToMKS(y);

                        sb.Append(XList[ii].ToString());
                        sb.Append(",");
                        sb.Append(YList[ii].ToString());
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
                double SampleFrequency = double.Parse(head.Rows[0]["SampleFrequency"].ToString());
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

                    if (SampleFrequency < 250 && SampleFrequency != 0)
                    {
                        ratio = 250 / SampleFrequency;
                    }

                    #endregion

                    for (int i = 1024; i < xvaluearr.Length - 1; i++)
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
                            else interval = int.Parse(str[0]);
                        }
                        else interval = i;
                        if (interval < xvaluearr.Length - 1)
                        {
                            if (yvaluearrdou[interval] < ymax)
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
                                //英制数据
                                xcsvd = Convert.ToDouble(Convert.ToDouble(xvaluearr[interval]).ToString(""));
                                ycsvd = Convert.ToDouble(Convert.ToDouble(yvaluearr[interval]).ToString(""));
                                //数组存储
                                xcsv.Add(xcsvd);
                                ycsv.Add(ycsvd);
                            }
                            else
                            {
                                break;
                            }
                        }
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
                    Action action = () => { this.dgv_PhotoReportUtility.Rows[ii].Cells["Load"].Value = load; };
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
            return (y2 - y1) / (x2 - x1);
        }

        /// <summary>
        /// 斜率计算
        /// </summary>
        /// <returns></returns>
        public double SlopeJinYu(double[] xdouarr, double[] ydouarr, double minx, double maxx)
        {
            double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            //获取最接近区间的点，进行斜率计算
            for (int ii = 0; ii < xdouarr.Length; ii++)
            {
                //最小接近点
                if (xdouarr[ii] > minx && y1 == 0.0)
                {
                    y1 = ydouarr[ii];
                    x1 = xdouarr[ii];
                }
                //最大接近点
                if (xdouarr[ii] > maxx && y2 == 0.0)
                {
                    y2 = ydouarr[ii];
                    x2 = xdouarr[ii];
                    break;
                }
            }
            return (y2 - y1) / (x2 - x1);
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
