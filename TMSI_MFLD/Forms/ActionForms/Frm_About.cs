using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMSI_MFLD.Properties;

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_About : CCWin.CCSkinMain
    {
        #region 构造函数

        public Frm_About()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件定义

        private void ucBtn_Close_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("http://www.tmsi-usa.com");
        }

        #endregion

        #region 方法定义

        /// <summary>
        /// 联系开发商方法链接文字
        /// </summary>
        /// <param name="输入的网址"></param>
        public void OpenUrl(string url)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = "iexplore.exe";
            pro.StartInfo.Arguments = url;
            pro.Start();
        }

        #endregion

    }
}
