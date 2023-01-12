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
    public class configuration
    {
        private language _language = new language();

        public configuration()
        {

        }


        [XmlElement("language")]
        public language Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }
    }
}
