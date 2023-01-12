using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TMSI_MFLD.Basis.Xml.xmlclass
{
    /// <summary>
    /// 可序列化顶部菜单项类
    /// </summary>
    [Serializable]
    public class language
    {

        public language()
        {

        }

        /// <summary>
        /// 语言存储
        /// </summary>
        [XmlAttribute]
        public string code { get; set; }
    }
}
