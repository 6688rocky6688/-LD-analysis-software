//预备判断项目号
#define HASETRI

using FormControls.Helpers;
using FunctionClassLibrary.XML;
using ICSharpCode.Core;
using Sunny.UI;
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
using TMSI_MFLD.Basis.EventArgs;
using TMSI_MFLD.Basis.Glodal;
using TMSI_MFLD.Basis.Xml;
using TMSI_MFLD.Basis.Xml.xmlclass;
using TMSI_MFLD.Forms.ActionForms;
using static TMSI_MFLD.Basis.Glodal.Glodal;

namespace TMSI_MFLD.Forms
{
    public partial class TMSI_MFLD_Main : CCWin.CCSkinMain
    {
        #region 字段定义

        string _timenow;

        #region 窗体字段

        Frm_Logo frm_Logo;
        Frm_About frm_About;
        Frm_PhotoReportUtility frm_PhotoReportUtility;
        Frm_PlotPackageUtility frm_PlotPackageUtility;

        #endregion

        //点击左侧任务栏名称存储
        string leftTaskbar = string.Empty;

        int a = 0;

        #endregion

        #region 构造函数 

        public TMSI_MFLD_Main()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件定义

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMSI_MFLD_Main_Load(object sender, EventArgs e)
        {
            #region 左侧任务栏加载

            try
            {
                ControlHelper.FreezeControl(this, true);
                //  Plot Package Utility
                TreeNode PlotPackageUitilty = new TreeNode(StringParser.Parse(ResourceService.GetString("PlotPackageUitilty")));
                this.leftTask.Nodes.Add(PlotPackageUitilty);
                //  Create Excel Report
                TreeNode CreateExcelReport = new TreeNode(StringParser.Parse(ResourceService.GetString("CreateExcelReport")));
                this.leftTask.Nodes.Add(CreateExcelReport);

                //系统
                TreeNode System = new TreeNode("  System");
#if HASETRI
                System.Nodes.Add("Setting");
#endif
                System.Nodes.Add("About");
                System.Nodes.Add("Reboot");
                System.Nodes.Add("Close");
                this.leftTask.Nodes.Add(System);
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }

            #endregion

            #region 窗体缓存

            FrmCache();

            #endregion

            #region 初次加载时先加载TMSI Logo

            pan_Frms.Controls.Clear();
            FrmsShow(frm_Logo, this.pan_Frms);

            #endregion

            #region 事件订阅

            Glodal.MainTimeChanged.TimeChanged -= MainTimeChanged_TimeChanged;
            Glodal.MainTimeChanged.TimeChanged += MainTimeChanged_TimeChanged;

            Glodal.ExitFrmChanged.FrmChanged -= ExitFrmChanged_FrmChanged;
            Glodal.ExitFrmChanged.FrmChanged += ExitFrmChanged_FrmChanged;

            #endregion

            #region 图标加载

            //this.btn__language.BackgroundImageLayout = ImageLayout.Stretch;
            //this.btn__language.Text = string.Empty;

            //if (Basis.Glodal.Glodal.languageCode.languagecode== "zh-CN")
            //{
            //    this.btn__language.BackgroundImage = ResourceService.GetImageResource("England") as Image; 
            //}
            //else
            //{
            //    this.btn__language.BackgroundImage = ResourceService.GetImageResource("China") as Image;
            //}

            #endregion

            #region 窗体最大化

            this.WindowState = FormWindowState.Maximized;

            #endregion
        }

        /// <summary>
        /// 主页面显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitFrmChanged_FrmChanged(object sender, EventArgs e)
        {
            pan_Frms.Controls.Clear();
            this.leftTask.SelectedNode = null;
            FrmsShow(frm_Logo, this.pan_Frms);
            Glodal.ExitFrmChanged.Exit = false;
        }

        /// <summary>
        /// 主界面时间改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTimeChanged_TimeChanged(object sender, EventArgs e)
        {
            if (e is ParamEventArgs)
            {
                ParamEventArgs pe = e as ParamEventArgs;
                _timenow = pe.Parameter.ToString();

                ////跨线程表达式
                //if (this.lal_time.InvokeRequired)
                //{
                //    // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                //    Action<string> action = (x) =>
                //    {
                //        this.lal_time.Text = x;
                //    };
                //    // 或者
                //    // Action<string> actionDelegate = delegate(string txt) { this.label3.Text = txt; };
                //    this.lal_time.Invoke(action, _timenow);
                //}
            }
        }

        /// <summary>
        /// 左侧任务栏双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void left_Taskbar_AfterSelect(object sender, TreeNodeMouseClickEventArgs e)
        {
            //当前点击名称
            leftTaskbar = e.Node.Text.Trim();
            //执行点击功能
            switch (leftTaskbar)
            {
                case "Plot Package Utility":
                    {
                        this.pan_Frms.Controls.Clear();
                        frm_PlotPackageUtility = new Frm_PlotPackageUtility();
                        FrmsShow(frm_PlotPackageUtility, this.pan_Frms);
                    }
                    break;
                case "曲线显示":
                    {
                        this.pan_Frms.Controls.Clear();
                        frm_PlotPackageUtility = new Frm_PlotPackageUtility();
                        FrmsShow(frm_PlotPackageUtility, this.pan_Frms);
                    }
                    break;
                case "Report Package Utility":

                    break;
                case "Create SLR Report":
                    this.pan_Frms.Controls.Clear();
                    Frm_SLR Frm_SLR = new Frm_SLR();
                    FrmsShow(Frm_SLR, this.pan_Frms);
                    break;
                case "Create Excel Report":

                    {
#if TMSI
                        this.pan_Frms.Controls.Clear();
                        frm_PhotoReportUtility = new Frm_PhotoReportUtility();
                        FrmsShow(frm_PhotoReportUtility, this.pan_Frms);
#elif HASETRI
                        this.pan_Frms.Controls.Clear();
                        Frm_PhotoReportUtility_HASETRI frm_PhotoReportUtility_HASETRI = new Frm_PhotoReportUtility_HASETRI();
                        FrmsShow(frm_PhotoReportUtility_HASETRI, this.pan_Frms);
#elif JINYU
                        this.pan_Frms.Controls.Clear();
                        Frm_PhotoReportUtility_JinYu Frm_PhotoReportUtility_JinYu = new Frm_PhotoReportUtility_JinYu();
                        FrmsShow(Frm_PhotoReportUtility_JinYu, this.pan_Frms);
#endif
                    }
                    break;
                case "Spring Rate Steup":
                    {
                        if (this.pan_Frms.Controls.Count > 0)
                        {
                            foreach (Control control in this.pan_Frms.Controls)
                            {
                                if (control is Form)
                                {
                                    if (control.Text == "Logo")
                                    {
                                        Frm_SpringRateSteup frm_SpringRateSteup = new Frm_SpringRateSteup();
                                        frm_SpringRateSteup.ShowDialog();
                                    }
                                    else
                                    {
                                        pan_Frms.Controls.Clear();
                                        FrmsShow(frm_Logo, this.pan_Frms);
                                        Frm_SpringRateSteup frm_SpringRateSteup = new Frm_SpringRateSteup();
                                        frm_SpringRateSteup.ShowDialog();
                                    }
                                }
                            }
                        }
                        else
                        {
                            pan_Frms.Controls.Clear();
                            FrmsShow(frm_Logo, this.pan_Frms);
                            Frm_SpringRateSteup frm_SpringRateSteup = new Frm_SpringRateSteup();
                            frm_SpringRateSteup.ShowDialog();
                        }
                    }
                    break;
#if HASETRI
                //case "Setting":
                //    {
                //        Frm_SpringRateSteup Frm_SpringRateSteup = new Frm_SpringRateSteup();
                //        Frm_SpringRateSteup.ShowDialog();
                //    }
                //    break;
#endif
                case "About":
                    {
                        frm_About = new Frm_About();
                        frm_About.ShowDialog();
                    }
                    break;
                case "Reboot":
                    {
                        //--------------重启软件 start---------------
                        //开启新的实例
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        //关闭当前实例
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        //--------------重启软件 end----------------- 
                    }
                    break;
                case "Close":
                    {
                        Process.GetCurrentProcess().Kill();//此方法完全奏效，绝对是完全退出。
                    }
                    break;
            }
        }

        /// <summary>
        /// 系统时间刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tim_timenow_Tick(object sender, EventArgs e)
        {
            _timenow = DateTime.Now.ToString();

            //if (lal_time.InvokeRequired)
            //{
            //    Action<string> action = (x) => this.lal_time.Text = x;

            //    this.lal_time.Invoke(action, _timenow);
            //}
        }

        /// <summary>
        /// 窗体加载完成之后执行(修bug控件加载第一次自动选定左侧任务栏第一个)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMSI_MFLD_Main_Shown(object sender, EventArgs e)
        {
            this.leftTask.SelectedNode = null;
        }

        /// <summary>
        /// 语言选择按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn__language_Click(object sender, EventArgs e)
        {
            #region 1.写入xml

            #region 一、制作数据源

            configuration conf = new configuration();

            if (languageCode.languagecode == "en-US")
            {
                conf.Language.code = "zh-CN";
            }
            else
            {
                conf.Language.code = "en-US";
            }

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
                    string xmlContent = SerializeHandler.SerializeObject(conf);
                    //根据路径和格式，写入Xml文件
                    FunctionClassLibrary.XML.FileHelper.WriteFile(xmlFileName, xmlContent, System.Text.Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    FunctionClassLibrary.Public.ConsoleOut.Error(ex.Message);
                }
            }
            #endregion

            #endregion

            #region 2.重启

            //--------------重启软件 start---------------
            //开启新的实例
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            //关闭当前实例
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //--------------重启软件 end----------------- 

            #endregion
        }

        #endregion

        #region 方法定义

        /// <summary>
        /// 加载窗体至Panel
        /// </summary>
        /// <param name="form"></param>
        /// <param name="control"></param>
        private void FrmsShow(Form form, Control control)
        {
            if (control is Panel)
            {
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                form.Parent = control;
                form.Show();
            }
        }

        /// <summary>
        /// 窗体缓存
        /// </summary>
        private void FrmCache()
        {
            frm_Logo = new Frm_Logo();
        }

        #endregion

    }
}
