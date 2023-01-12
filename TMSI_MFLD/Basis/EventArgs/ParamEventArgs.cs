using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSI_MFLD.Basis.EventArgs
{  
    /// <summary>
    /// 带返回参数的事件参数类
    /// </summary>
    public class ParamEventArgs : System.EventArgs
    {
        private object parameter;

        public ParamEventArgs(object parameter)
        {
            this.parameter = parameter;
        }

        public object Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }
    }
}
