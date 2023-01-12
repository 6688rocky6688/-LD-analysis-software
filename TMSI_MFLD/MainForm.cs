using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TMSI_MFLD.Basis;
using zta_convert;


namespace TMSI_MFLD
{
    public partial class MainForm : Form
    {
        #region 字段定义

        string ztaJsonStr = "";                         //zta文件解析后获得的Json字符串

        public DataTable dt01 = null;                   //Header的Datatable数据
        public DataTable dt02 = null;                   //Config的Datatable数据
        public DataTable dt03 = null;                   //Config + RawData，2个datatable合一起的数据
        public DataTable dt04 = null;                   //X轴与Y轴合拼的datatable(有效的测试数据)
        public DataTable dt05 = null;                   //报告标题集合


        public string jaHeader = "";                    //Header字符串
        public string jaConfig = "";
        public string jaRawData = "";


        JObject jo = new JObject();                     //zta文件大结构体对象
        JArray ja = new JArray();                       //解析出来的RawData对象

        private DateTime minValue, maxValue;            //横坐标最小和最大值
        private Random rand = new Random();             //时间随机数

        public string TestDuration = "";

        ArrayList arrStartTime = new ArrayList();       //开始时间
        ArrayList arrSampRate = new ArrayList();        //取样率
        ArrayList arrTestDuration = new ArrayList();    //持续时间

        ArrayList countSelect = new ArrayList();        //取多少的数量
        Decimal timeChangeData = new Decimal();         //每秒取样数量
        //ArrayList timeDuration = new ArrayList();

        private static object TestNo = null;
        private static object TestType = null;          //样本类型
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

        //新排列顺序通道的集合
        ArrayList channelCoordinates = new ArrayList { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        JObject joList01 = null;                            //重新排列的第一通道RowData数组数据对象
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


        //单位转换常量
        decimal kpa = Convert.ToDecimal(6.894757);          //压强的单位，名称是千帕
        decimal N = Convert.ToDecimal(4.448222);            //力的单位，即牛顿，简称牛
        decimal mm = Convert.ToDecimal(25.4);               //毫米
        decimal s = Convert.ToDecimal(1);                   //角度
        decimal Kgf = Convert.ToDecimal(0.4535924);         //是一种力的单位,叫千克力


        string axisX = "";                                  //X轴名称
        string axisY = "";                                  //Y轴名称
        string measureUnit = "";                            //测量单位分类名称

        //存储当前的单位
        unit unitsave = unit.English;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;     //设置窗体为无边框样式
            //this.WindowState = FormWindowState.Maximized;    //最大化窗体
        }

        #region 按钮
        private void uiButton6_Click(object sender, EventArgs e)
        {



        }

        /// <summary>
        /// 解析新排列RowData数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton2_Click_2(object sender, EventArgs e)
        {
            try
            {
                #region
                if (aisle01ArrRawData.Count != 0)
                {
                    aisle01ArrRawData.Clear();
                }
                if (aisle02ArrRawData.Count != 0)
                {
                    aisle02ArrRawData.Clear();
                }
                if (aisle03ArrRawData.Count != 0)
                {
                    aisle03ArrRawData.Clear();
                }
                if (aisle04ArrRawData.Count != 0)
                {
                    aisle04ArrRawData.Clear();
                }
                if (aisle05ArrRawData.Count != 0)
                {
                    aisle05ArrRawData.Clear();
                }
                if (aisle06ArrRawData.Count != 0)
                {
                    aisle06ArrRawData.Clear();
                }
                if (aisle07ArrRawData.Count != 0)
                {
                    aisle07ArrRawData.Clear();
                }
                if (aisle08ArrRawData.Count != 0)
                {
                    aisle08ArrRawData.Clear();
                }
                if (aisle09ArrRawData.Count != 0)
                {
                    aisle09ArrRawData.Clear();
                }
                if (aisle10ArrRawData.Count != 0)
                {
                    aisle10ArrRawData.Clear();
                }
                #endregion

                #region  10个通道，每个通道取出的测试数值

                ArrayList XChannelcountSelect = getXChannelcountSelectArr();    //获取取样数量


                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[0]); i++)
                {
                    if (joList01 != null)
                    {
                        aisle01ArrRawData.Add(joList01["Data"][i + 1024].ToString());
                    }
                }


                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[1]); i++)
                {
                    if (joList02 != null)
                    {

                        aisle02ArrRawData.Add(joList02["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[2]); i++)
                {
                    if (joList03 != null)
                    {

                        aisle03ArrRawData.Add(joList03["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[3]); i++)
                {
                    if (joList04 != null)
                    {

                        aisle04ArrRawData.Add(joList04["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[4]); i++)
                {
                    if (joList05 != null)
                    {

                        aisle05ArrRawData.Add(joList05["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[5]); i++)
                {
                    if (joList06 != null)
                    {

                        aisle06ArrRawData.Add(joList06["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[6]); i++)
                {
                    if (joList07 != null)
                    {

                        aisle07ArrRawData.Add(joList07["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[7]); i++)
                {
                    if (joList08 != null)
                    {

                        aisle08ArrRawData.Add(joList08["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[8]); i++)
                {
                    if (joList09 != null)
                    {

                        aisle09ArrRawData.Add(joList09["Data"][i + 1024].ToString());
                    }
                }

                for (int i = 0; i < Convert.ToDecimal(XChannelcountSelect[9]); i++)
                {
                    if (joList10 != null)
                    {

                        aisle10ArrRawData.Add(joList10["Data"][i + 1024].ToString());
                    }
                }
                #endregion

                dt01 = DictionaryToDataTableHeader(jaHeader);
                dt02 = ToDataTableTwo(jaConfig);
                dt03 = ToDataTableTwo02(jaConfig, jaRawData);

                boxGetValue();

                MessageBox.Show("文件解析成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件解析失败，原因：" + ex.ToString());
                ex.ToString();
            }

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
                        case "Tire":
                            //channelCoordinates[7] = jo["RawData"][i];
                            joList10 = (JObject)JsonConvert.DeserializeObject(jo["RawData"][i].ToString());
                            break;
                    }
                }

            }
        }

        public void boxGetValue()
        {
            txtTestID.Text = TestNo.ToString();
            txtTestType.Text = TestType.ToString();
            txtTestDate.Text = TestDate.ToString();
            txtTireSize.Text = TireSize.ToString();

            txtTargetDeflection.Text = (Convert.ToDecimal(TireInflation.ToString())).ToString("0.00") + " in";
            txtTireLoad.Text = (Convert.ToDecimal(TireLoad.ToString())).ToString("0.00") + " lb";
            txtTireDia.Text = (Convert.ToDecimal(TireDia.ToString())).ToString("0.00") + " in";
            txtTargetLoad.Text = (Convert.ToDecimal(TargetLoad.ToString())).ToString("0.00") + " lb";
            txtTireInflation.Text = (Convert.ToDecimal(TargetDeflection.ToString())).ToString("0.00") + " psi";
            txtTargetInflation.Text = (Convert.ToDecimal(TargetInflation.ToString())).ToString("0.00") + " psi";
            txtVz.Text = (Convert.ToDecimal(VectorRatePz.ToString())).ToString("0.00") + " °/s";
            txtVxy.Text = (Convert.ToDecimal(VectorRatePxy.ToString())).ToString("0.00") + " °/s";
            txtVaz.Text = (Convert.ToDecimal(VectorRateAz.ToString())).ToString("0.00") + " °/s";

            uiComboBox21.Text = TestType.ToString();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {

        }

        private void uiButton4_Click_1(object sender, EventArgs e)
        {
            #region 
            dt05 = new DataTable();
            DataColumn dc1 = new DataColumn("ReportHead", Type.GetType("System.String"));
            dt05.Columns.Add(dc1);

            DataRow dr = dt05.NewRow();
            switch (TestType.ToString())//X轴名称
            {
                case "Kv":
                    dr["ReportHead"] = "Radial stiffness report";
                    break;
                case "KLong":
                    dr["ReportHead"] = "Longitudinal stiffness report";
                    break;
                case "KLat":
                    dr["ReportHead"] = "Lateral stiffness report";
                    break;
                case "KT":
                    dr["ReportHead"] = "Torsional stiffness report";
                    break;
                default:
                    dr["ReportHead"] = TestType;
                    break;
            }

            dt05.Rows.Add(dr);

            #endregion

            ReportPreview reportPreview = new ReportPreview(dt01, dt04, dt05);
            reportPreview.ShowDialog();
            //reportPreview.Show();
        }
        private void uiButton2_Click_1(object sender, EventArgs e)
        {
            #region 清除控件当前曲线

            //先清除控件曲线
            this.chtTestPlot.Series.Clear();
            this.chtTestPlot.ChartAreas[0].RecalculateAxesScale();

            #endregion 

            #region 解析原始数据

            //大结构体
            var zta = new ZTAFile(txtDataPath.Text);
            ztaJsonStr = JsonConvert.SerializeObject(zta.DataV2, Formatting.Indented);

            var dt = JsonToDataTable(ztaJsonStr);

            chtTestPlot.Series.Clear();
            Series test = new Series("耐力");
            test.ChartType = SeriesChartType.Spline;//设备图表类型

            #endregion

            #region Dictionary原始数据

            //声明字典接取全部的数据
            Dictionary<string, List<string>> dicRawdata = new Dictionary<string, List<string>>();
            string[] rdarr;
            List<string> ls = new List<string>();
            int num = 0;

            foreach (DataRow dr in dt.Rows)
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

            #region 曲线绘制

            //获取想获取曲线的xy名称
            string x = this.uiComboBox24.Text;
            string y = this.uiComboBox22.Text;

            if (x == "Tire")
            {
                x = "Inf";
            }
            if (y == "Tire")
            {
                y = "Inf";
            }

            //xy坐标string类型
            string[] xvaluearr = dicRawdata[x].ToArray();
            string[] yvaluearr = dicRawdata[y].ToArray();
            //xy坐标值类型
            double xvalue, yvalue;

            int q = 0;

            //加载xy坐标值
            try
            {
                for (int i = 0; i < yvaluearr.Length - 2; i++)
                {
                    if (yvaluearr[i] != "0.0")
                    {
                        //英制数据
                        xvalue = Convert.ToDouble(Convert.ToDouble(xvaluearr[i]).ToString("f4"));
                        yvalue = Convert.ToDouble(Convert.ToDouble(yvaluearr[i]).ToString("f4"));

                        //判断进制
                        switch (uiComboBox1.Text)
                        {
                            case "English":
                                break;
                            case "SI":
                                xvalue = xvalue.EnglishToSi(x);
                                yvalue = yvalue.EnglishToSi(y);
                                break;
                            case "MKS":
                                xvalue = xvalue.EnglishToMKS(x);
                                yvalue = yvalue.EnglishToMKS(y);
                                break;
                        }
                        //添加曲线
                        if (i != 0 && i != 76023 && Convert.ToDouble(xvaluearr[i]) != Convert.ToDouble(xvaluearr[i + 1]))
                        {
                            q++;
                            test.Points.AddXY(xvalue, yvalue);
                        }
                    }
                }
                //加载曲线。
                chtTestPlot.Series.Add(test);
                //曲线名称加载。
                chtTestPlot.ChartAreas[0].AxisX.Title = x + x.Unit(uiComboBox1.Text);
                chtTestPlot.ChartAreas[0].AxisY.Title = y + y.Unit(uiComboBox1.Text);
                //设置x轴曲线偏移              
                chtTestPlot.ChartAreas[0].AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Auto;
                chtTestPlot.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Auto;
                chtTestPlot.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Auto;
                chtTestPlot.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                //查询最小点
                DataPoint valueminx = test.Points.FindMinByValue();
                //写入起始值
                if(valueminx.XValue < valueminx.XValue.ReXMin(this.uiComboBox24.Text, this.uiComboBox1.Text,this.uiComboBox22.Text))
                {
                    chtTestPlot.ChartAreas[0].AxisX.Minimum = valueminx.XValue.ReXMin(this.uiComboBox24.Text, this.uiComboBox1.Text, this.uiComboBox22.Text) - this.uiComboBox24.Text.Interval(this.uiComboBox1.Text, this.uiComboBox22.Text);
                }
                else
                {
                    chtTestPlot.ChartAreas[0].AxisX.Minimum = valueminx.XValue.ReXMin(this.uiComboBox24.Text, this.uiComboBox1.Text, this.uiComboBox22.Text);
                }
                //写入偏移
                chtTestPlot.ChartAreas[0].AxisX.Interval = this.uiComboBox24.Text.Interval(this.uiComboBox1.Text, this.uiComboBox22.Text);

                MessageBox.Show(q.ToString());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            #endregion
        }

        private void uiButton11_Click(object sender, EventArgs e)
        {
            switch (uiComboBox24.Text)//X轴名称
            {
                case "Time":
                    getYButtonOneAisle();
                    break;
                case "01-Fz":
                    getYButtonTwoAisle(aisle01ArrRawData, "Fz");
                    break;
                case "02-Pz":
                    chtTestPlot.Titles.Clear();
                    getYButtonTwoAisle(aisle02ArrRawData, "Pz");
                    break;
                case "03-Fx":
                    getYButtonTwoAisle(aisle03ArrRawData, "Fx");
                    break;
                case "04-Fy":
                    getYButtonTwoAisle(aisle04ArrRawData, "Fy");
                    break;
                case "05-Mx":
                    getYButtonTwoAisle(aisle05ArrRawData, "Mx");
                    break;
                case "06-Mx":
                    getYButtonTwoAisle(aisle06ArrRawData, "Mx");
                    break;
                case "07-Mz":
                    getYButtonTwoAisle(aisle07ArrRawData, "Mz");
                    break;
                case "08-Pxy":
                    getYButtonTwoAisle(aisle08ArrRawData, "Pxy");
                    break;
                case "09-Az":
                    getYButtonTwoAisle(aisle09ArrRawData, "Az");
                    break;
                case "10-Tire":
                    getYButtonTwoAisle(aisle10ArrRawData, "Tire");
                    break;
            }
        }

        /// <summary>
        /// updae
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton15_Click(object sender, EventArgs e)
        {
            string str1 = uiComboBox21.Text;//配方名称
            string str2 = cb01.Text;//最小值范围
            string str3 = cb02.Text;//最大值范围

            string strX = uiComboBox24.Text;//X轴名称
            string strY = uiComboBox22.Text;//Y轴名称
            getYButtonOneAisle();
        }


        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //默认是英制单位，点击修改textbox值

            switch (uiComboBox1.Text)//X轴名称
            {
                case "English"://英制单位
                    englishMetricSystem();
                    break;

                case "SI"://公制单位
                    siMetricSystem();
                    break;
                case "MKS":
                    mksMetricSystem();
                    break;
            }
        }

        #endregion

        #region 公制计算
        /// <summary>
        /// 英制单位
        /// </summary>
        public void englishMetricSystem()
        {
            try
            {
                txtTargetDeflection.Text = (Convert.ToDecimal(TireInflation.ToString())).ToString("0.00") + " psi";

                txtTireLoad.Text = (Convert.ToDecimal(TireLoad.ToString())).ToString("0.00") + " lb";///
                txtTireDia.Text = (Convert.ToDecimal(TireDia.ToString())).ToString("0.00") + " in"; ////
                txtTargetLoad.Text = (Convert.ToDecimal(TargetLoad.ToString())).ToString("0.00") + " lb"; ////

                txtTireInflation.Text = (Convert.ToDecimal(TargetDeflection.ToString())).ToString("0.00") + " in";
                txtTargetInflation.Text = (Convert.ToDecimal(TargetInflation.ToString())).ToString("0.00") + " psi";

                txtVz.Text = (Convert.ToDecimal(VectorRatePz.ToString())).ToString("0.00") + " in/s"; ////
                txtVxy.Text = (Convert.ToDecimal(VectorRatePxy.ToString())).ToString("0.00") + " in/s"; ////
                txtVaz.Text = (Convert.ToDecimal(VectorRateAz.ToString())).ToString("0.00") + " °/s"; ////

                setDatatableNewValue(dt01);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 公制单位
        /// </summary>
        public void siMetricSystem()
        {
            try
            {
                txtTargetDeflection.Text = (Convert.ToDecimal(TireInflation.ToString()) * kpa).ToString("0.00") + " kpa";

                txtTireLoad.Text = (Convert.ToDecimal(TireLoad.ToString()) * N).ToString("0.00") + " N";///
                txtTireDia.Text = (Convert.ToDecimal(TireDia.ToString()) * mm).ToString("0.00") + " mm"; ////
                txtTargetLoad.Text = (Convert.ToDecimal(TargetLoad.ToString()) * N).ToString("0.00") + " N"; ////

                txtTireInflation.Text = (Convert.ToDecimal(TargetDeflection.ToString()) * mm).ToString("0.00") + " mm";
                txtTargetInflation.Text = (Convert.ToDecimal(TargetInflation.ToString()) * kpa).ToString("0.00") + " kpa";////?????????

                txtVz.Text = (Convert.ToDecimal(VectorRatePz.ToString()) * mm).ToString("0.00") + " mm/s"; ////0.018288
                txtVxy.Text = (Convert.ToDecimal(VectorRatePxy.ToString()) * mm).ToString("0.00") + " mm/s"; //////
                txtVaz.Text = (Convert.ToDecimal(VectorRateAz.ToString()) * s).ToString("0.00") + " °/s"; /////

                setDatatableNewValue(dt01);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// mks单位
        /// </summary>
        public void mksMetricSystem()
        {
            try
            {
                txtTargetDeflection.Text = (Convert.ToDecimal(TireInflation.ToString()) * kpa).ToString("0.00") + " kpa";

                txtTireLoad.Text = (Convert.ToDecimal(TireLoad.ToString()) * Kgf).ToString("0.00") + " Kgf";///
                txtTireDia.Text = (Convert.ToDecimal(TireDia.ToString()) * mm).ToString("0.00") + " mm"; ////
                txtTargetLoad.Text = (Convert.ToDecimal(TargetLoad.ToString()) * Kgf).ToString("0.00") + " Kgf"; ////

                txtTireInflation.Text = (Convert.ToDecimal(TargetDeflection.ToString()) * mm).ToString("0.00") + " mm";
                txtTargetInflation.Text = (Convert.ToDecimal(TargetInflation.ToString()) * kpa).ToString("0.00") + " kpa";////?????????

                txtVz.Text = (Convert.ToDecimal(VectorRatePz.ToString()) * mm).ToString("0.00") + " mm/s"; ////0.018288
                txtVxy.Text = (Convert.ToDecimal(VectorRatePxy.ToString()) * mm).ToString("0.00") + " mm/s"; //////
                txtVaz.Text = (Convert.ToDecimal(VectorRateAz.ToString()) * s).ToString("0.00") + " °/s"; /////

                setDatatableNewValue(dt01);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 重新回写给datatable
        /// </summary>
        /// <param name="dt"></param>
        public void setDatatableNewValue(DataTable dt)
        {
            DataRow[] dr = dt.Select();
            dr[0]["TireInflation"] = txtTargetDeflection.Text;
            dr[0]["TireLoad"] = txtTireLoad.Text;
            dr[0]["TireDia"] = txtTireDia.Text;
            dr[0]["TargetLoad"] = txtTargetLoad.Text;
            dr[0]["TargetDeflection"] = txtTireInflation.Text;
            dr[0]["TargetInflation"] = txtTargetInflation.Text;
            dr[0]["VectorRatePz"] = txtVz.Text;
            dr[0]["VectorRatePxy"] = txtVxy.Text;
            dr[0]["VectorRateAz"] = txtVaz.Text;
        }

        #endregion

        #region
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

        //DataTable table
        private void uiButton5_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer javaScriptSerializerConfig = new JavaScriptSerializer();
            javaScriptSerializerConfig.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayListConfig = javaScriptSerializerConfig.Deserialize<ArrayList>(jaConfig);

            JavaScriptSerializer javaScriptSerializerRawData = new JavaScriptSerializer();
            javaScriptSerializerRawData.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayListRawData = javaScriptSerializerRawData.Deserialize<ArrayList>(jaRawData);

            try
            {
                HSSFWorkbook workBook = new HSSFWorkbook();
                createSheet01(workBook, "Header", dt01);//创建sheet,排列为竖向
                //createSheet01(workBook, "Config", dt02);
                createSheet01(workBook, "Config", dt03);
                createSheet02(workBook, "RawData", dt04);

                string path = Application.StartupPath + @"\test测试.xls";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    workBook.Write(file);  //创建Excel文件。
                    file.Close();
                }
                MessageBox.Show("导入EXCEL成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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

            DataTable dt01 = DicToTable(dic);
            return dt01;
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
            //int i = -1;
            //while (++i < dataTable01.Rows.Count)
            //{
            //    dataTable01.Rows[i]["Data"] = i + 1 >= dataTable02.Rows.Count ? "0" : dataTable02.Rows[i]["Data"];
            //}

            return dataTable01;
        }


        #endregion

        #region


        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTableToExcel01(string json)
        {
            DataTable dataTable = new DataTable("table02"); //实例化
            DataTable result;
            try
            {
                //dataTable.Columns.Add("Header");
                dataTable.Columns.Add("filler1");
                dataTable.Columns.Add("TestNo");
                dataTable.Columns.Add("Operators");
                dataTable.Columns.Add("Engineer");
                dataTable.Columns.Add("Customer");
                dataTable.Columns.Add("TestPurpose");
                dataTable.Columns.Add("TestStandard");
                dataTable.Columns.Add("RequestDep");
                dataTable.Columns.Add("Requestor");
                dataTable.Columns.Add("ProjectNum");

                dataTable.Columns.Add("TestDate");
                dataTable.Columns.Add("TestTime");
                dataTable.Columns.Add("Manager");
                dataTable.Columns.Add("Mode");
                dataTable.Columns.Add("Purpose");
                dataTable.Columns.Add("Title");
                dataTable.Columns.Add("TestCompletionDate");
                dataTable.Columns.Add("TestCompletionTime");
                dataTable.Columns.Add("Feature");
                dataTable.Columns.Add("Specification");


                dataTable.Columns.Add("TestMachine");
                dataTable.Columns.Add("OldStyleTestDate");
                dataTable.Columns.Add("filler2");
                dataTable.Columns.Add("TireNumber");
                dataTable.Columns.Add("TireSize");
                dataTable.Columns.Add("TireSN");
                dataTable.Columns.Add("TireType");
                dataTable.Columns.Add("LoadIndex");
                dataTable.Columns.Add("SpeedLevel");
                dataTable.Columns.Add("IsTubeLess");

                dataTable.Columns.Add("Manufacture");
                dataTable.Columns.Add("Brand");
                dataTable.Columns.Add("Pattern");
                dataTable.Columns.Add("TirePly");
                dataTable.Columns.Add("TireInflation");
                dataTable.Columns.Add("TireLoad");
                dataTable.Columns.Add("TireRadius");
                dataTable.Columns.Add("TireDia");
                dataTable.Columns.Add("TireWeight");
                dataTable.Columns.Add("TRAWeight");

                dataTable.Columns.Add("TireDate");
                dataTable.Columns.Add("TireWeeks");
                dataTable.Columns.Add("TireOrientation");
                dataTable.Columns.Add("TireDesign");
                dataTable.Columns.Add("TreadHardness");
                dataTable.Columns.Add("RimOffset");
                dataTable.Columns.Add("WheelWidth");
                dataTable.Columns.Add("wheelSize");
                dataTable.Columns.Add("wheelType");
                dataTable.Columns.Add("Remarks");

                dataTable.Columns.Add("filler3");
                dataTable.Columns.Add("Temperature");
                dataTable.Columns.Add("Humidity");
                dataTable.Columns.Add("CNFFileName");
                dataTable.Columns.Add("SeqFileName");
                dataTable.Columns.Add("CtrlFileName");
                dataTable.Columns.Add("RecipeFile");
                dataTable.Columns.Add("TargetLoad");
                dataTable.Columns.Add("TargetDeflection");
                dataTable.Columns.Add("TargetInflation");

                dataTable.Columns.Add("VectorRatePz");
                dataTable.Columns.Add("VectorRatePxy");
                dataTable.Columns.Add("VectorRateAz");
                dataTable.Columns.Add("TestDuration");
                dataTable.Columns.Add("PlungerDiameter");
                dataTable.Columns.Add("TestPlateHeight");
                dataTable.Columns.Add("Platforms");
                dataTable.Columns.Add("PinNo");
                dataTable.Columns.Add("Energy");
                dataTable.Columns.Add("Force");

                dataTable.Columns.Add("Penetration");
                dataTable.Columns.Add("filler4");
                dataTable.Columns.Add("PropChan");
                dataTable.Columns.Add("DummyInfo");
                dataTable.Columns.Add("SpeedGate");
                dataTable.Columns.Add("Restraint");
                dataTable.Columns.Add("SledInfo");
                dataTable.Columns.Add("SledType");
                dataTable.Columns.Add("MIRAConfig");
                dataTable.Columns.Add("TstType");

                dataTable.Columns.Add("TCType");
                dataTable.Columns.Add("Items");
                dataTable.Columns.Add("HeaderDate");
                dataTable.Columns.Add("PostTestRemarks");
                dataTable.Columns.Add("status");
                dataTable.Columns.Add("filler5");

                string str = "{\"name\": \"甄嬛体\",\"2012-05-04 14:59\": \"5724\"}";
                JsonObject obj = JsonConvert.DeserializeObject(json) as JsonObject;
                foreach (KeyValuePair<string, object> k in obj)
                {
                    Console.WriteLine("Key：{0} Value：{1}", k.Key, k.Value);
                }

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
                        }//Rows
                        string root = "";
                        foreach (string current in dictionary.Keys)
                        {
                            if (current != "data")
                                root = current;
                            else
                            {
                                ArrayList list = dictionary[current] as ArrayList;
                                foreach (Dictionary<string, object> dic in list)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow[root] = dictionary[root];
                                    foreach (string key in dic.Keys)
                                    {
                                        dataRow[key] = dic[key];
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            result = dataTable;
            return result;
        }



        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTableToExcel02(string json)
        {
            DataTable dataTable = new DataTable("table03"); //实例化
            DataTable result;
            json = System.Text.RegularExpressions.Regex.Replace(json, "[\r\n\t]", "");
            json = json.Replace(" ", "");
            try
            {

                //JObject jo = (JObject)JsonConvert.DeserializeObject(json);

                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //serializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                //List<ZTADataV2> list01 = serializer.Deserialize<List<ZTADataV2>>(json);
                //var list02 = JsonConvert.DeserializeObject<ZTADataV2>(json);
                //DeserializeObject

                //JObject o = JObject.Parse(json);

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
                                if (current != "data")
                                    dataTable.Columns.Add(current, dictionary[current].GetType());
                                else
                                {
                                    ArrayList list = dictionary[current] as ArrayList;
                                    foreach (Dictionary<string, object> dic in list)
                                    {
                                        foreach (string key in dic.Keys)
                                        {
                                            dataTable.Columns.Add(key, dic[key].GetType());
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        //Rows
                        string root = "";
                        foreach (string current in dictionary.Keys)
                        {
                            if (current != "data")
                                root = current;
                            else
                            {
                                ArrayList list = dictionary[current] as ArrayList;
                                foreach (Dictionary<string, object> dic in list)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow[root] = dictionary[root];
                                    foreach (string key in dic.Keys)
                                    {
                                        dataRow[key] = dic[key];
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            result = dataTable;
            return result;
        }


        #endregion

        #region

        /// <summary>
        /// 创建sheet,排列为竖向
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="sheetName"></param>
        /// <param name="datatable"></param>
        /// <returns></returns>
        private ISheet createSheet01(HSSFWorkbook workBook, string sheetName, DataTable datatable)
        {

            //workBook = new HSSFWorkbook();
            if (string.IsNullOrEmpty(sheetName))
                sheetName = "Sheet1";//工作簿
            ISheet sheet = workBook.CreateSheet(sheetName);
            IRow row = sheet.CreateRow(0);
            IFont font = workBook.CreateFont();
            font.FontName = "Arial";//字体样式
            font.Boldweight = (short)FontBoldWeight.None;
            ICellStyle headerStyle2 = workBook.CreateCellStyle();
            headerStyle2.VerticalAlignment = VerticalAlignment.Center; //垂直居中
            headerStyle2.WrapText = false;//自动换行
            font.FontHeightInPoints = 9;//字体

            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                ICellStyle header = workBook.CreateCellStyle();
                header.VerticalAlignment = VerticalAlignment.Center; //垂直居中
                header.WrapText = false;//自动换行
                var row1_ = sheet.CreateRow(i);//创建行
                var cellName = row1_.CreateCell(0);//列名
                cellName.SetCellValue(datatable.Columns[i].ColumnName.ToString().Replace("<br />", "\n"));
                IFont font2 = workBook.CreateFont();
                font2.Boldweight = (short)FontBoldWeight.None;
                header.SetFont(font);
                cellName.CellStyle = header;
                //填充内容
                for (int j = 0; j < datatable.Rows.Count; j++)
                {
                    var cell1_ = row1_.CreateCell(j + 1);
                    cell1_.SetCellValue(datatable.Rows[j][i].ToString().Replace("<br />", "\n"));
                    cell1_.CellStyle = headerStyle2; //把样式赋给单元格
                }
                //设置列宽
                sheet.SetColumnWidth(0, 20 * 256 + 200);//列宽为10
            }

            //设置行高
            //row.Height = 30 * 20;//行高为30

            return sheet;
        }

        /// <summary>
        /// 创建sheet,排列为横向
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="sheetName"></param>
        /// <param name="datatable"></param>
        /// <returns></returns>
        private ISheet createSheet02(HSSFWorkbook workBook, string sheetName, DataTable datatable)
        {
            ISheet sheet = workBook.CreateSheet(sheetName);
            IRow RowHead = sheet.CreateRow(0);

            //var h= datatable.Columns[i].ColumnName;


            for (int iColumnIndex = 0; iColumnIndex < datatable.Columns.Count; iColumnIndex++)
            {
                RowHead.CreateCell(iColumnIndex).SetCellValue(datatable.Columns[iColumnIndex].ColumnName.ToString());//列名赋值
            }

            for (int iRowIndex = 0; iRowIndex < datatable.Rows.Count; iRowIndex++)
            {
                IRow RowBody = sheet.CreateRow(iRowIndex + 1);
                for (int iColumnIndex = 0; iColumnIndex < datatable.Columns.Count; iColumnIndex++)
                {
                    //RowBody.CreateCell(iColumnIndex).SetCellValue(DateTime.Now.Millisecond);
                    var h = datatable.Rows[iRowIndex][iColumnIndex].ToString();
                    RowBody.CreateCell(iColumnIndex).SetCellValue(h);//为单元格设置字符串值。
                    //sheet.AutoSizeColumn(iColumnIndex);//调整列宽以适应内容。
                }
            }
            return sheet;
        }


        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="file"></param>
        public void dataTableToCsv01(DataTable table, string file)
        {
            string title = "";
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                title += table.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格
            }
            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);
            foreach (DataRow row in table.Rows)
            {
                string line = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格
                }
                line = line.Substring(0, line.Length - 1) + "\n";
                sw.Write(line);
            }
            sw.Close();
            fs.Close();
        }


        #endregion

        #region 图表

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

        public void getYButtonOneAisle()
        {
            switch (uiComboBox22.Text)//Y轴名称
            {
                case "01-Fz":
                    getYButtonValueOneAisle(aisle01ArrRawData);
                    break;
                case "02-Pz":
                    getYButtonValueOneAisle(aisle02ArrRawData);
                    break;
                case "03-Fx":
                    getYButtonValueOneAisle(aisle03ArrRawData);
                    break;
                case "04-Fy":
                    getYButtonValueOneAisle(aisle04ArrRawData);
                    break;
                case "05-Mx":
                    getYButtonValueOneAisle(aisle05ArrRawData);
                    break;
                case "06-My":
                    getYButtonValueOneAisle(aisle06ArrRawData);
                    break;
                case "07-Mz":
                    getYButtonValueOneAisle(aisle07ArrRawData);
                    break;
                case "08-Px":
                    getYButtonValueOneAisle(aisle08ArrRawData);
                    break;
                case "09-Az":
                    getYButtonValueOneAisle(aisle09ArrRawData);
                    break;
                case "10-Tire":
                    getYButtonValueOneAisle(aisle10ArrRawData);
                    break;
            }
        }


        public void getYButtonValueOneAisle(ArrayList list)
        {
            string str = string.Join(",", (string[])list.ToArray(typeof(string)));
            //int countStr = list.Count / 100;//取样数量
            string str1 = uiComboBox21.Text;//配方名称,默认值：KV
            string str2 = cb01.Text;//最小值范围,默认值：100%  
            string str3 = cb02.Text;//最大值范围,默认值：100%

            chtTestPlot.Legends.Clear();
            chtTestPlot.Series.Clear();

            #region 图表设置

            Series test = new Series();
            test.ChartType = SeriesChartType.Spline;//设备图表类型
            chtTestPlot.ChartAreas[0].AxisX.Title = "Time（Second）";
            test.XValueType = ChartValueType.DateTime;
            //chtTestPlot.ChartAreas["ChartArea1"].AxisY.Maximum = CalculationMax(list);//设置Y轴最大值
            chtTestPlot.ChartAreas["ChartArea1"].AxisY.Maximum = Convert.ToDouble(CalculationMax(list).ToString("0.0")); //设置Y轴最大值

            InitChartOne();

            string dateString = "00";
            string TestDuration = arrTestDuration[0].ToString();

            minValue = DateTime.ParseExact(dateString, "ss", System.Globalization.CultureInfo.CurrentCulture);//x轴最小刻度 
            maxValue = minValue.AddSeconds(double.Parse(TestDuration)); //X轴最大刻度


            chtTestPlot.ChartAreas[0].AxisX.Minimum = minValue.ToOADate();
            chtTestPlot.ChartAreas[0].AxisX.Maximum = maxValue.ToOADate();

            #endregion

            for (int i = 0; i < list.Count; i++)
            {
                double h = (double)i / list.Count * double.Parse(TestDuration);//全部有效数据在测试时间内，每个数据的长度
                test.Points.AddXY(minValue.AddSeconds(h), list[i]);//最小秒数循环加 h
            }
            chtTestPlot.Series.Add(test);
            chtTestPlot.Visible = true;
            chtTestPlot.Visible = false;

        }


        public void getYButtonTwoAisle(ArrayList listX, string strX)
        {
            switch (uiComboBox22.Text)//Y轴名称
            {
                case "01-Fz":
                    getYButtonValueTwoAisle(listX, aisle01ArrRawData, strX, "Fz");
                    break;
                case "02-Pz":
                    chtTestPlot.Series.Clear();
                    getYButtonValueTwoAisle(listX, aisle02ArrRawData, strX, "Pz");
                    break;
                case "03-Fx":
                    getYButtonValueTwoAisle(listX, aisle03ArrRawData, strX, "Fx");
                    break;
                case "04-Fy":
                    getYButtonValueTwoAisle(listX, aisle04ArrRawData, strX, "Fy");
                    break;
                case "05-Mx":
                    getYButtonValueTwoAisle(listX, aisle05ArrRawData, strX, "Mx");
                    break;
                case "06-My":
                    getYButtonValueTwoAisle(listX, aisle06ArrRawData, strX, "My");
                    break;
                case "07-Mz":
                    getYButtonValueTwoAisle(listX, aisle07ArrRawData, strX, "Mz");
                    break;
                case "08-Pxy":
                    getYButtonValueTwoAisle(listX, aisle08ArrRawData, strX, "Pxy");
                    break;
                case "08-Py":
                    getYButtonValueTwoAisle(listX, aisle08ArrRawData, strX, "Pxy");
                    break;
                case "08-Px":
                    getYButtonValueTwoAisle(listX, aisle08ArrRawData, strX, "Pxy");
                    break;
                case "09-Az":
                    getYButtonValueTwoAisle(listX, aisle09ArrRawData, strX, "Az");
                    break;
                case "10-Tire":
                    getYButtonValueTwoAisle(listX, aisle10ArrRawData, strX, "Tire");
                    break;
            }
        }
        Series test1 = new Series();

        public void getYButtonValueTwoAisle(ArrayList listX, ArrayList listY, string strX, string strY)
        {
            dt04 = new DataTable();
            dt04.TableName = "table04";
            DataColumn dc1 = new DataColumn("XValue", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("YValue", Type.GetType("System.String"));

            dt04.Columns.Add(dc1);
            dt04.Columns.Add(dc2);

            string str1 = string.Join(",", (string[])listX.ToArray(typeof(string)));
            string str2 = string.Join(",", (string[])listY.ToArray(typeof(string)));

            string strRecipe = uiComboBox21.Text;//配方名称
            decimal strMin = decimal.Parse(cb01.Text.Replace("%", "")) / 100;//最小值范围
            decimal strMAX = decimal.Parse(cb02.Text.Replace("%", "")) / 100;//最大值范围
            string strUnit = uiComboBox1.Text;


            ArrayList resultX = getArrayListSection(strMin, strMAX, listX);//获取listX最小值--最大小范围内的数据
            ArrayList resultY = getArrayListSection(strMin, strMAX, listY);//获取listY最小值--最大小范围内的数据

            #region 图表设置
            var h = test1.Points;
            if (h != null)
            {
                test1.Points.Clear();
            }

            chtTestPlot.Legends.Clear();///////////++++++++++
            chtTestPlot.Series.Clear();
            test1.ChartType = SeriesChartType.Spline;//设备图表类  
            //chart3.ChartAreas[0].AxisX.Title = "X Channel（mm）";

            measureUnit = uiComboBox1.Text;//测量单位分类名称
            switch (measureUnit)
            {
                case "English"://英制
                    axisXShowEnglish(strX, strY);// XY轴英制单位显示
                    break;
                case "SI"://公制
                    axisXShowSI(strX, strY);// XY轴公制单位显示
                    break;
                case "MKS"://MKS
                    //axisXShowMKS(strX, strY);
                    break;
            }


            if (Convert.ToDouble(CalculationMax(listY).ToString("0.0")) > 0)
            {
                chtTestPlot.ChartAreas["ChartArea1"].AxisY.Maximum = Convert.ToDouble(CalculationMax(listY).ToString("0.0"));
                //Convert.ToDouble(CalculationMax(listY).ToString("0.0")); //设置Y轴最大值
                chtTestPlot.ChartAreas["ChartArea1"].AxisY.Minimum = 0; //是在Y轴最小值
                                                                        //chtTestPlot.ChartAreas["ChartArea1"].AxisY.Interval = 10;//设置每个刻度的跨度
            }
            else
            {
                chtTestPlot.ChartAreas["ChartArea1"].AxisY.Maximum = 0;
                //Convert.ToDouble(CalculationMax(listY).ToString("0.0")); //设置Y轴最大值
                chtTestPlot.ChartAreas["ChartArea1"].AxisY.Minimum = Convert.ToDouble(CalculationMax(listY).ToString("0.0")); //是在Y轴最小值
                                                                                                                              //chtTestPlot.ChartAreas["ChartArea1"].AxisY.Interval = 10;//设置每个刻度的跨度
            }

            chtTestPlot.Titles.Clear();
            chtTestPlot.Titles.Add("耐力测试02");
            chtTestPlot.Titles[0].ForeColor = Color.RoyalBlue;
            chtTestPlot.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            //chart3.Titles[0].Text = string.Format("频组数据显示");

            #endregion

            for (int i = 0; i < resultX.Count; i++)
            {
                var valueX = Convert.ToDouble(resultX[i]).ToString("0.0");//X轴值
                var valueY = Convert.ToDouble(resultY[i]).ToString("0.0");//Y轴值



                switch (uiComboBox1.Text)
                {
                    case "English"://英制
                        //test1.Points.AddXY(valueX, valueY);
                        //test1.Points.AddXY(valueX + 1, valueY + 1);
                        break;
                    case "SI"://公制
                        axisXYUnitSI(valueX, valueY, strX, strY);
                        break;
                    case "MKS"://MKS
                               //axisXShowMKS(strX, strY);
                        break;
                }

                test1.Points.AddXY(valueX, valueY);

                //X轴与Y轴合并为datatable，后传给图表
                DataRow dr = dt04.NewRow();
                dr["XValue"] = valueX;
                dr["YValue"] = valueY;
                dt04.Rows.Add(dr);




            }

            chtTestPlot.Series.Add(test1);
            chtTestPlot.Visible = true;
            chtTestPlot.Visible = false;


        }
        /// <summary>
        /// 公制单位下，XY轴获取转换后的值
        /// </summary>
        /// <param name="valueX">英制单位下X轴的一个点值</param>
        /// <param name="valueY">英制单位下Y轴的一个点值</param>
        /// <param name="strX">英制单位下X轴的单位名称</param>
        /// <param name="strY">英制单位下Y轴的单位名称</param>
        public void axisXYUnitSI(string valueX, string valueY, string strX, string strY)
        {
            switch (strX)
            {
                case "Fz":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N）";//力
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;
                case "Pz":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（mm）";//偏移、位移
                    valueX = (Convert.ToDecimal(valueX) * mm).ToString();
                    break;
                case "Fx":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N）";//力
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;
                case "Mx":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N/mm）";//力距
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;
                case "My":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N/mm）";//力距
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;
                case "Mz":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N/mm）";//力距
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;
                case "Pxy":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（mm）";//偏移、位移
                    valueX = (Convert.ToDecimal(valueX) * mm).ToString();
                    break;
                case "Az":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（°）";//角度
                    valueX = (Convert.ToDecimal(valueX) * s).ToString();
                    break;
                case "Inf":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N）";//力
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;
                case "Fy":
                    //chart3.ChartAreas[0].AxisX.Title = "X Channel（N）";//
                    valueX = (Convert.ToDecimal(valueX) * N).ToString();
                    break;

            }

            switch (strY)
            {
                case "Fz":
                    //chart3.ChartAreas[0].AxisY.Title = "Y Channel（N）";//力
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;
                case "Pz":
                    //chart3.ChartAreas[0].AxisY.Title = "Y Channel（mm）";//偏移、位移
                    valueY = (Convert.ToDecimal(valueY) * mm).ToString();
                    break;
                case "Fx":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//力
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;
                case "Mx":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N/mm）";//力距
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;
                case "My":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N/mm）";//力距
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;
                case "Mz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N/mm）";//力距
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;
                case "Pxy":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（mm）";//偏移、位移
                    valueY = (Convert.ToDecimal(valueY) * mm).ToString();
                    break;
                case "Az":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（°）";//角度
                    valueY = (Convert.ToDecimal(valueY) * s).ToString();
                    break;
                case "Inf":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//力
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;
                case "Fy":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//
                    valueY = (Convert.ToDecimal(valueY) * N).ToString();
                    break;

            }
        }




        /// <summary>
        /// X轴英制单位显示
        /// </summary>
        /// <param name="strX">X轴通道名称</param>
        /// <param name="strY">Y轴通道名称</param>
        public void axisXShowEnglish(string strX, string strY)
        {
            switch (strX)
            {
                case "Fz":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb）";//力
                    break;
                case "Pz":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（in）";//偏移、位移
                    break;
                case "Fx":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb）";//力
                    break;
                case "Mx":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb/in）";//力距
                    break;
                case "My":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb/in）";//力距
                    break;
                case "Mz":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb/in）";//力距
                    break;
                case "Pxy":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（in）";//偏移、位移
                    break;
                case "Az":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（°）";//角度
                    break;
                case "Inf":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb）";//力
                    break;
                case "Fy":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（lb）";//
                    break;

            }

            switch (strY)
            {
                case "Fz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb）";//力
                    break;
                case "Pz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（in）";//偏移、位移
                    break;
                case "Fx":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb）";//力
                    break;
                case "Mx":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb/in）";//力距
                    break;
                case "My":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb/in）";//力距
                    break;
                case "Mz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb/in）";//力距
                    break;
                case "Pxy":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（in）";//偏移、位移
                    break;
                case "Az":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（°）";//角度
                    break;
                case "Inf":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb）";//力
                    break;
                case "Fy":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（lb）";//
                    break;

            }


        }

        /// <summary>
        /// X轴公制单位显示
        /// </summary>
        /// <param name="strX">X轴通道名称</param>
        /// <param name="strY">Y轴通道名称</param>
        public void axisXShowSI(string strX, string strY)
        {
            switch (strX)
            {
                case "Fz":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N）";//力
                    break;
                case "Pz":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（mm）";//偏移、位移
                    break;
                case "Fx":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N）";//力
                    break;
                case "Mx":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N/mm）";//力距
                    break;
                case "My":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N/mm）";//力距
                    break;
                case "Mz":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N/mm）";//力距
                    break;
                case "Pxy":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（mm）";//偏移、位移
                    break;
                case "Az":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（°）";//角度
                    break;
                case "Inf":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N）";//力
                    break;
                case "Fy":
                    chtTestPlot.ChartAreas[0].AxisX.Title = "X Channel（N）";//
                    break;

            }

            switch (strY)
            {
                case "Fz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//力
                    break;
                case "Pz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（mm）";//偏移、位移
                    break;
                case "Fx":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//力
                    break;
                case "Mx":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N/mm）";//力距
                    break;
                case "My":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N/mm）";//力距
                    break;
                case "Mz":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N/mm）";//力距
                    break;
                case "Pxy":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（mm）";//偏移、位移
                    break;
                case "Az":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（°）";//角度
                    break;
                case "Inf":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//力
                    break;
                case "Fy":
                    chtTestPlot.ChartAreas[0].AxisY.Title = "Y Channel（N）";//
                    break;

            }

        }


        /// <summary>
        /// 获取ArrayList最小值百分比-最大值百分比之间的数据
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="arrList"></param>
        /// <returns></returns>
        public ArrayList getArrayListSection(decimal min, decimal max, ArrayList arrList)
        {
            var h1 = arrList.Count * min;
            var q1 = arrList.Count * max - arrList.Count * min;
            ArrayList resultX = arrList.GetRange((int)h1, (int)q1);
            return resultX;
        }

        /// <summary>
        /// 获取ArrayList最大值
        /// </summary>
        /// <param name="sampleList"></param>
        /// <returns></returns>
        public double CalculationMax(ArrayList sampleList)
        {
            try
            {
                double maxDevation = 0.0;
                int totalCount = sampleList.Count;
                if (totalCount >= 1)
                {
                    double max = Double.Parse(sampleList[0].ToString());
                    for (int i = 0; i < totalCount; i++)
                    {
                        double temp = Double.Parse(sampleList[i].ToString());
                        if (temp > max)
                        {
                            max = temp;
                        }
                    }
                    maxDevation = max;
                }
                return maxDevation;
            }
            catch (Exception ex)
            {
                ex.ToString();
                //LogInfo.InsertErrorSystemLog("通用计算公式", "获取集合中的最大值:" + ex.Message);
                throw ex;
            }

        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Load Rawdata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadRawData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDataPath.Text = System.IO.Path.GetFullPath(openFileDialog1.FileName);  //绝对路径

                var zta = new ZTAFile(txtDataPath.Text);         //大结构体
                ztaJsonStr = JsonConvert.SerializeObject(zta.DataV2, Formatting.Indented);

                jo = (JObject)JsonConvert.DeserializeObject(ztaJsonStr);
                jaHeader = jo["Header"].ToString();
                jaConfig = jo["Config"].ToString();
                //var jaReadRecordCount = jo["ReadRecordCount"].ToString();
                jaRawData = jo["RawData"].ToString();
                ja = (JArray)jo["RawData"];

                RearrangeChannels();// 重新排列通道
            }
        }

        private void chtTestPlot_Click(object sender, EventArgs e)
        {

        }

        public void InitChartOne()
        {
            chtTestPlot.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;       //----------------
            chtTestPlot.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds;
            chtTestPlot.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;

            chtTestPlot.ChartAreas[0].AxisX.Interval = 0.005;//这个interval可以用来修改显示间隔，因为上面设置单位为秒，这里间隔为0.5秒

            chtTestPlot.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Milliseconds;
            chtTestPlot.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            chtTestPlot.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Center;
            chtTestPlot.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chtTestPlot.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            chtTestPlot.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;

            chtTestPlot.ChartAreas[0].AxisX.LabelStyle.Format = "ss"; //x轴时间显示格式为时:分:秒
            chtTestPlot.ChartAreas[0].AxisX.Interval = 1000;




            //设置标题
            this.chtTestPlot.Titles.Clear();
            this.chtTestPlot.Titles.Add("耐力测试01");
            this.chtTestPlot.Titles[0].ForeColor = Color.RoyalBlue;
            this.chtTestPlot.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            //this.chtTestPlot.Titles[0].Text = string.Format("频组数据显示");
        }


        #endregion

        #region 单位枚举

        public enum unit
        {
            English = 1,
            SI = 2,
            MKS = 3
        }

        #endregion
    }

}
