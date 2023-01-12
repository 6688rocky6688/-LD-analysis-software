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
    public class PathSet
    {
        public PathSet()
        {

        }

        [XmlAttribute]
        public string ZtaPath { get; set; }

        [XmlAttribute]
        public string ExcelPath { get; set; }

        [XmlAttribute]
        public string SlrPath { get; set; }
    }
}
