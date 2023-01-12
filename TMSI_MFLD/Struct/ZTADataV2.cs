using System.Runtime.Serialization;

namespace zta_convert
{
    public class ZTADataV2
    {
        private HeadrecV2 m_Headrec;
        private TestCFGrec[] m_Config;
        private ConvertedDataArray[] m_RawData;

        /// <summary>
        /// 头
        /// </summary>
        [DataMember(Name = "Header")]
        public HeadrecV2 Header
        {
            get => m_Headrec;
            set => m_Headrec = value;
        }

        /// <summary>
        /// 配置
        /// </summary>
        [DataMember(Name = "Config")]
        public TestCFGrec[] Config
        {
            get => m_Config;
            set => m_Config = value;
        }

        /// <summary>
        /// 记录数
        /// </summary>
        [DataMember(Name = "ReadRecordCount")]
        public int ReadRecordCount { get; set; }

        /// <summary>
        /// raw数据
        /// </summary>
        [DataMember(Name = "RawData")]
        public ConvertedDataArray[] RawData
        {
            get => m_RawData;
            set => m_RawData = value;
        }
    }
}
