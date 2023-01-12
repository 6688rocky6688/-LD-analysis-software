using FunctionClassLibrary.Public;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSI_MFLD.Basis.Timerlass
{
    public class TimerRunService
    {
        #region 字段定义

        private System.Timers.Timer _timer = null;
        private int _cnt = 0;           //计数器
        bool plcoffline = false;
        object oj = new object();

        #endregion

        #region 单例实现

        private static TimerRunService _instance = null;

        private TimerRunService() { }

        public static TimerRunService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimerRunService();
                }
                return _instance;
            }
        }

        #endregion

        #region 启动定时器运行服务
        /// <summary>
        /// 启动定时器运行服务
        /// </summary>
        public void Start()
        {
            if (this._timer == null)
            {
                this._timer = new System.Timers.Timer();
                this._timer.Interval = 1000;
                this._timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                this._timer.Start();
            }
        }

        #endregion

        #region 停止定时器运行服务
        /// <summary>
        /// 停止定时器运行服务
        /// </summary>
        public void Stop()
        {
            if (this._timer != null)
            {
                this._timer.Stop();
                this._timer.Dispose();
                this._timer = null;
            }
        }

        #endregion

        #region 定时器服务事件处理

        /// <summary>
        /// 定时器服务事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (oj)
            {
                #region 1.时间改变触发

                Glodal.Glodal.MainTimeChanged.StartTime = true;

                #endregion 
            }
        }

        #endregion
    }
}
