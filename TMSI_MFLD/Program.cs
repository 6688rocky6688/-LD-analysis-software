using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMSI_MFLD.Basis.initialization;
using TMSI_MFLD.Basis.Timerlass;
using TMSI_MFLD.Forms;
using TMSI_MFLD.Forms.ActionForms;

namespace TMSI_MFLD
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 服务启动

            //定时服务启动--1S触发
            TimerRunService.Instance.Start();
            //系统初始化服务启动
            Initialization.Instance.Start();

            #endregion

            #region 注册资源文件
            
            Assembly exe = typeof(Program).Assembly;
            FileUtility.ApplicationRootPath = Path.GetDirectoryName(exe.Location);

            ResourceService.RegisterNeutralStrings(new System.Resources.ResourceManager("TMSI_MFLD.Properties.TmsiResource", exe));
            ResourceService.RegisterNeutralImages(new System.Resources.ResourceManager("TMSI_MFLD.Properties.TmsiResource", exe));

            ResourceService.RegisterNeutralStrings(new System.Resources.ResourceManager("TMSI_MFLD.Properties.Resources", exe));
            ResourceService.RegisterNeutralImages(new System.Resources.ResourceManager("TMSI_MFLD.Properties.Resources", exe));

            ResourceService.RegisterNeutralStrings(new System.Resources.ResourceManager("TMSI_MFLD.Properties.TmsiPublic", exe));
            ResourceService.RegisterNeutralImages(new System.Resources.ResourceManager("TMSI_MFLD.Properties.TmsiPublic", exe));

            #endregion

            #region 设置起始区域

            string languagecode = Basis.Glodal.Glodal.languageCode.languagecode;
            System.Globalization.CultureInfo currentUICulture = new System.Globalization.CultureInfo(languagecode);
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentUICulture;

            #endregion 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TMSI_MFLD_Main());
        }
    }
}
