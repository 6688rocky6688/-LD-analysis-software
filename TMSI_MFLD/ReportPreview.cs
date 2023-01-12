using FastReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMSI_MFLD
{
    public partial class ReportPreview : CCWin.CCSkinMain
    {
        #region 字段定义

        DataTable dt01;
        DataTable dt02;
        DataTable dt03;

        #endregion

        #region 构造函数

        public ReportPreview()
        {
            InitializeComponent();
        }

        public ReportPreview(DataTable dt1, DataTable dt2, DataTable dt3)
        {
            InitializeComponent();

            //dt01 = fm.dt01;
            //dt02 = fm.dt04;
            //dt03 = fm.dt05;

            dt01 = dt1;
            dt02 = dt2;
            dt03 = dt3;
        }

        #endregion

        #region 事件定义
        private void Form3_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(Preview);
            thread.Start();
        }

        #endregion

        #region 方法定义
        public void Preview()
        {

            //DataTable dt03 = new DataTable();            
            //DataColumn dc1 = new DataColumn("ReportHead", Type.GetType("System.String"));
            //dt03.Columns.Add(dc1);
            //DataRow dr = dt03.NewRow();
            //dr["ReportHead"] = "Torsional Stiffness Report";
            //dt03.Rows.Add(dr);

            if (dt02 == null)
            {
                MessageBox.Show("请先点击ConFirm");
                return;
            }
            else
            {
                dt01.TableName = "Data1";
                dt02.TableName = "Data2";
                dt03.TableName = "Data3";

                try
                {
                    Basis.Glodal.Glodal.PreviewDataSet.RPDataSet.Tables.Clear();
                    Basis.Glodal.Glodal.PreviewDataSet.RPDataSet.Tables.Add(dt01);
                    Basis.Glodal.Glodal.PreviewDataSet.RPDataSet.Tables.Add(dt02);
                    Basis.Glodal.Glodal.PreviewDataSet.RPDataSet.Tables.Add(dt03);

                    FastReport.Report report = new FastReport.Report();
                    string filename = @"Reports\Groups08_08.frx";
                    //加载报表
                    report.Load(filename);
                    //让报表显示在窗体的控件中
                    report.Preview = this.previewControl1;
                    //注册应用程序数据集的所有表和关系，以使用它在报告中。
                    //report.SetParameterValue("time", DateTime.Now.Date.ToString("yyyy-MM-dd"));//报表里的参数赋值
                    report.RegisterData(Basis.Glodal.Glodal.PreviewDataSet.RPDataSet);
                    //准备报告。                                  
                    report.Prepare();

                    if (this.previewControl1.InvokeRequired)
                    {
                        Action action = () =>
                        {
                                //预览报告。 报告应该使用快速报告方法。  
                                report.ShowPrepared();
                        };
                        this.previewControl1.Invoke(action);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }        
        }

        #endregion
    }
}
