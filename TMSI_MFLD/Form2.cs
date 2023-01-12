using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TMSI_MFLD 
{
    public partial class Form2 : Form
    {
        private Series dataTable1Series = new Series("2020盈亏表");
        Random rd = new Random();

        public Form2()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new  Basis.ToolStripRendererEx.ProfessionalToolStripRendererEx();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.TabText = "统计图表";
            initData();
            initStyle();
        }

        public void initStyle()
        {
            //图表框架及背景设置
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chart1.BackSecondaryColor = System.Drawing.Color.White;
            chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.BorderlineWidth = 2;
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chart1.BackColor = Color.SteelBlue;
            chart1.Dock = DockStyle.Fill;

            //设置是否在 Chart 中显示 坐标点值
            dataTable1Series.IsValueShownAsLabel = true;
            dataTable1Series.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
            dataTable1Series.ShadowOffset = 1;
            dataTable1Series.YValuesPerPoint = 2;

            chart1.ChartAreas[0].AxisY.Maximum = 300.0;//设置Y轴最大值
            chart1.ChartAreas[0].AxisY.Minimum = -100.0; //设置Y轴最小值
            //chart1.Series[0].LegendText = "";//系列名字
            chart1.Series[0].LegendToolTip = "Target Output";//鼠标放到系列上出现的文字 

        }

        public void initData()
        {

            DataTable dataTable1 = new System.Data.DataTable();
            dataTable1.Columns.Add("日期", typeof(Int32));
            dataTable1.Columns.Add("金额", typeof(double));

            for (int i = 0; i < 11; i++)
            {
                dataTable1.Rows.Add((i + 1), 1032.0 * rd.Next() / 10000000000);
            }

            chart1.Series.Clear();//清空表中的数据
                                  //第一个表中的数据

            dataTable1Series.Points.DataBind(dataTable1.AsEnumerable(), "日期", "金额", "");
            dataTable1Series.XValueType = ChartValueType.Int32; //设置X轴类型为时间
            dataTable1Series.ChartType = SeriesChartType.Pie;  //设置Y轴为折线
            chart1.Series.Add(dataTable1Series);


        }



    }
}
