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

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_ChartShow : CCWin.CCSkinMain
    {
        #region 字段定义

        #endregion

        #region 属性定义

        #endregion

        #region 构造函数

        public Frm_ChartShow()
        {
            InitializeComponent();
        }

        public Frm_ChartShow(Series series, double minimum, double interval)
        {
            //生成窗体初始
            InitializeComponent();
            //设置全屏   
            this.WindowState = FormWindowState.Maximized;    //最大化窗体
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //曲线控件
            Maxim_chart.Series.Clear();
            Maxim_chart.Series.Add(series);
            Maxim_chart.ChartAreas[0].AxisX.Minimum = minimum;
            Maxim_chart.ChartAreas[0].AxisX.Interval = interval;
        }
        //写入偏移



        #endregion

        #region 事件定义

        #endregion

        #region 重写事件

        /// <summary>
        /// 禁止拖动窗体
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0X00A1 && m.WParam.ToInt32() == 2)
            {
                return;
            }
            if (m.Msg == 0xA3)
            {
                return;
            }
            base.WndProc(ref m);
        }


        #endregion

        #region 方法定义

        #endregion
    }
}
