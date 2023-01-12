
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TMSI_MFLD.Basis.Xml.xmlclass
{
    /// <summary>
    /// 可序列化
    /// </summary>
    [Serializable]
    public class FilePath
    {

        private PathSet _filepath = new PathSet();

        public FilePath()
        {

        }


        [XmlElement("filepath")]
        public PathSet FilePathXML
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
            }
        }
    }
}
