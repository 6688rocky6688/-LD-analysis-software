using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMSI_MFLD.Controls.Load
{
    /// <summary>
    /// 静态动画控件共用定时器(ButtonExt、SwitchButtonExt、WaveRippleExt)
    /// </summary>
    [ToolboxItem(false)]
    [Description("静态动画控件共用定时器(ButtonExt、SwitchButtonExt)")]
    public class AnimationStaticTimer : Control
    {
        #region 字段
        /// <summary>
        /// 动画对象列表锁
        /// </summary>
        protected internal static object buttonExtList_object = new object();
        /// <summary>
        /// 动画对象列表
        /// </summary>
        protected internal static List<object> buttonExtList = new List<object>();
        /// <summary>
        /// 动画定时器锁
        /// </summary>
        protected internal static object timer_object = new object();
        /// <summary>
        /// 动画定时器
        /// </summary>
        protected internal static Timer timer = null;
        /// <summary>
        /// 动画定时器空闲时间
        /// </summary>
        protected internal static int leisure_time = 0;
        /// <summary>
        /// 动画定时器最大空闲时间(超过关闭定时器)
        /// </summary>
        protected internal static int leisure_maxtime = 10000;

        #endregion

        #region 重写

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected internal static void Timer_Tick(object sender, EventArgs e)
        {
            leisure_time += timer.Interval;
            lock (buttonExtList_object)
            {
                for (int i = 0; i < buttonExtList.Count; i++)
                {
                    if (buttonExtList[i] != null)
                    {
                        ((IAnimationStaticTimer)buttonExtList[i]).Animationing();
                        leisure_time = 0;
                    }
                }
            }
            if (leisure_time > leisure_maxtime)
            {
                lock (buttonExtList_object)
                {
                    timer.Enabled = false;
                    buttonExtList.Clear();
                }
            }
        }

        /// <summary>
        /// 开始指定控件动画
        /// </summary>
        protected internal static void AnimationStart(object control)
        {
            if (timer == null)
            {
                lock (timer_object)
                {
                    timer = new Timer();
                    timer.Interval = 20;
                    timer.Tick += Timer_Tick;
                }
            }
            timer.Enabled = true;
            lock (buttonExtList_object)
            {
                if (buttonExtList.IndexOf(control) < 0)
                {
                    buttonExtList.Add(control);
                }
            }
            leisure_time = 0;
        }

        /// <summary>
        /// 停止指定控件动画
        /// </summary>
        protected internal static void AnimationStop(object control)
        {
            for (int i = 0; i < buttonExtList.Count; i++)
            {
                if (buttonExtList[i] == control)
                {
                    buttonExtList[i] = null;
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 动画控件一般要继承该接口
    /// </summary>
    public interface IAnimationStaticTimer
    {
        /// <summary>
        /// 动画控件动画中要处理的内容
        /// </summary>
        void Animationing();
    }
}
