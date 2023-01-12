using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionClassLibrary.SqlClass;
using System.Data;
using FunctionClassLibrary.Siemens_S7.DBClass;
using FunctionClassLibrary.XML;
using System.IO;
using System.Windows.Forms;
using TMSI_MFLD.Basis.Xml.xmlclass;

namespace TMSI_MFLD.Basis.initialization
{
    /// <summary>
    /// 单例启动执行的初始化
    /// </summary>
    public class Initialization
    {
        #region 字段定义

        private static string InitializatioLock = "InitializatioLock";
        private bool runState = false;

        #endregion

        #region 单例实现

        private static Initialization _instance = null;

        private Initialization()
        {

        }

        public static Initialization Instance
        {
            get
            {
                lock (InitializatioLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Initialization();
                    }
                    return _instance;
                }
            }
        }

        #endregion

        #region 启动服务

        public void Start()
        {
            try
            {
                if (this.runState)
                {
                    return;
                }
                lock (String.Empty)
                {
                    string xmlFileName = (Path.Combine(Application.StartupPath, "configuration", "configuration.xml"));
                    configuration _configuration = new configuration();
                    //启动服务
                    try
                    {
                        //xml文件路径，并接住返回值
                        _configuration = SerializeHandler.DeserializeXml<configuration>(Path.Combine(Application.StartupPath, "configuration", "configuration.xml"));
                        Glodal.Glodal.languageCode.languagecode = _configuration.Language.code;
                    }
                    catch
                    {

                    }

                }
            }
            catch
            {

            }
        }

        #endregion

        #region 停止服务

        public void Stop()
        {
            try
            {
                if (!this.runState)
                {
                    return;
                }
                lock (String.Empty)
                {
                    this.runState = false;
                }

            }
            catch
            {

            }
        }

        #endregion
    }
}

