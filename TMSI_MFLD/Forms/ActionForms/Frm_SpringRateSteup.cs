using FunctionClassLibrary.SqlClass;
using FunctionClassLibrary.XML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMSI_MFLD.Basis.Glodal;
using TMSI_MFLD.Basis.Xml.xmlclass;
using TMSI_MFLD.Controls.DgvHelper;
using TMSI_MFLD.Properties;

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_SpringRateSteup : CCWin.CCSkinMain
    {
        #region 字段定义




        #endregion 

        #region 构造函数

        public Frm_SpringRateSteup()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件定义

        /// <summary>
        /// ok按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtn_ok_BtnClick(object sender, EventArgs e)
        {
            #region 设置文件路径

            #region 一、制作数据源

            FilePath filepath = new FilePath();
            filepath.FilePathXML.ZtaPath = txtzta.Text;
            filepath.FilePathXML.ExcelPath = txtrport.Text;

            #endregion

            #region 二、写入xml

            //文件路径
            string xmlFileName = (Path.Combine(Application.StartupPath, "configuration", "configuration.xml"));

            lock (xmlFileName)
            {
                try
                {
                    string dir1 = Path.GetDirectoryName(xmlFileName);
                    if (!Directory.Exists(dir1))
                    {
                        Directory.CreateDirectory(dir1);
                    }
                    //SerializeObject转换成string
                    string xmlContent = SerializeHandler.SerializeObject("");
                    //根据路径和格式，写入Xml文件
                    FunctionClassLibrary.XML.FileHelper.WriteFile(xmlFileName, xmlContent, System.Text.Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    FunctionClassLibrary.Public.ConsoleOut.Error(ex.Message);
                }
            }
            #endregion

            this.DialogResult = DialogResult.OK;

            #endregion
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucbtn_cancel_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Frm_SpringRateSteup_Load(object sender, EventArgs e)
        {
           
        }

        #endregion

        #region 方法定义

        public void DgvDS(DataGridView dataGridView)
        {
            DataTable dataTable = new DataTable();


        }

        #endregion
    }
}
