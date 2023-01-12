using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace zta_convert
{

    /// <summary>
    /// zta 文件处理类
    /// </summary>
    public class ZTAFile
    {
        private MemoryStream m_Stream;
        private BinaryReader m_Reader;
        //private ZTADataV1 m_Data_V1;
        private ZTADataV2 m_Data_V2;

        /// <summary>
        /// 构造函数，自动解析传入的文件名
        /// </summary>
        /// <param name="FileName">zta文件名</param>
        public ZTAFile(string FileName, int version = 2)
        {
            if (FileName != "")
            {
                using (var f = new FileStream(FileName, FileMode.Open))
                {
                    m_Stream = new MemoryStream();
                    // 使用DeflateStream解压缩
                    using (var deflateStream = new DeflateStream(f, CompressionMode.Decompress, true))
                    {
                        var buf = new byte[1024];
                        int len;
                        while ((len = deflateStream.Read(buf, 0, buf.Length)) > 0)
                            m_Stream.Write(buf, 0, len);
                    }
                    m_Stream.Position = 0;
                    m_Reader = new BinaryReader(m_Stream);
                }
                //if (version == 1)
                //{
                //    m_Data_V1 = new ZTADataV1();
                //    ParseFileV1();
                //}
                //else
                //{
                m_Data_V2 = new ZTADataV2();
                ParseFileV2();
                //}
            }
        }

        ///// <summary>
        ///// 解析zta V1文件
        ///// </summary>
        //private void ParseFileV1()
        //{
        //    //记录数
        //    m_Data_V1.ReadRecordCount = ReadInt32();
        //    //解析文件头
        //    m_Data_V1.Header = ReadHeadRecV1();
        //    m_Data_V1.Config = new TestCFGrec[m_Data_V1.ReadRecordCount];
        //    m_Data_V1.RawData = new ConvertedDataArray[m_Data_V1.ReadRecordCount];
        //    //解析配置数据
        //    for (int i = 0; i < m_Data_V1.ReadRecordCount; i++)
        //    {
        //        m_Data_V1.Config[i] = ReadTestCFGRec();
        //    }
        //    //解析raw数据
        //    for (int i = 0; i < m_Data_V1.ReadRecordCount; i++)
        //    {
        //        m_Data_V1.RawData[i] = ReadConvertedDataArray();
        //    }
        //}

        /// <summary>
        /// 解析zta V2文件
        /// </summary>
        private void ParseFileV2()
        {
            //记录数
            m_Data_V2.ReadRecordCount = ReadInt32();
            //解析文件头
            m_Data_V2.Header = ReadHeadRecV2();
            m_Data_V2.Config = new TestCFGrec[m_Data_V2.ReadRecordCount];
            m_Data_V2.RawData = new ConvertedDataArray[m_Data_V2.ReadRecordCount];
            //解析配置数据
            for (int i = 0; i < m_Data_V2.ReadRecordCount; i++)
            {
                m_Data_V2.Config[i] = ReadTestCFGRec();
            }
            //解析raw数据
            for (int i = 0; i < m_Data_V2.ReadRecordCount; i++)
            {
                m_Data_V2.RawData[i] = ReadConvertedDataArray();
            }
        }

        #region record read functions

        /// <summary>
        /// 读取raw数据
        /// </summary>
        /// <returns>返回一个raw数据记录</returns>
        private ConvertedDataArray ReadConvertedDataArray()
        {
            var r = new ConvertedDataArray();
            for (int i = 0; i < Globals.PntsPERchan; i++)
            {
                r.Data[i] = ReadSingle();
            }
            return r;
        }

        /// <summary>
        /// 读取配置记录
        /// </summary>
        /// <returns>返回一条配置记录</returns>
        private TestCFGrec ReadTestCFGRec()
        {
            var r = new TestCFGrec();
            r.ChanNum = ReadByte();
            r.Gain = ReadReal48();
            r.SENTYP = ReadShortString(2);
            r.DataDesc = ReadShortString(16);
            r.SENLOC = ReadShortString(2);
            r.SENATT = ReadShortString(4);
            r.AXIS = ReadShortString(2);
            r.SenID = ReadShortString(7);
            r.ReqScale = ReadReal48();
            r.Polarity = ReadShortString(1);
            r.Sensi = ReadReal48();
            r.Excitation = ReadSingle();
            r.BridgeRes = ReadReal48();
            r.MaxFullScale = ReadReal48();
            r.OutputAtFS = ReadReal48();
            r.Units = ReadShortString(3);
            r.ZeroOfs = ReadInt32();
            r.SerialNo = ReadShortString(12);
            r.SampRate = ReadReal48();
            r.BridgeSC = ReadReal48();
            r.GainSC = ReadReal48();
            r.ReptFilter = ReadShortString(6);
            r.CalRun = ReadBoolean();
            r.CalGain = ReadReal48();
            r.CalPlusLev = ReadReal48();
            r.CalMinusLev = ReadReal48();
            r.NumCalRuns = ReadInt32();
            r.RefNum = ReadInt32();
            r.DataSource = (tDataSource)ReadByte();
            for (int i = 0; i < 2; i++)
            {
                r.ShuntCalRes[i] = ReadShuntCalResultRec();
            }
            r.StartTime = ReadDouble();
            r.SoakLength = ReadInt32();
            r.adc_factor = ReadDouble();
            r.intChanNum = ReadInt32();
            r.ActualRange = ReadDouble();
            r.MicroStrain = ReadUShort();
            r.CalPlus = ReadShort();
            r.CalMinus = ReadShort();
            r.MedianSpikeLevel = ReadDouble();
            r.msecStartTime = ReadInt32();
            r.msecStopTime = ReadInt32();
            r.Offset = ReadDouble();
            r.UseMzVals = ReadBoolean();
            r.BeamRatio = ReadSingle();
            r.Tb = ReadSingle();
            Read(r.filler, 0, 6);
            return r;
        }

        /// <summary>
        /// 读取ShuntCalResultRec记录
        /// </summary>
        /// <returns>返回ShuntCalResultRec记录</returns>
        private ShuntCalResultRec ReadShuntCalResultRec()
        {
            var r = new ShuntCalResultRec();
            r.ExcitationFailed = ReadBoolean();
            r.PosShuntFailed = ReadBoolean();
            r.NegShuntFailed = ReadBoolean();
            r.PosShunt = ReadReal48(); // real
            r.NegShunt = ReadReal48(); // real;
            return r;
        }

        /// <summary>
        /// 读取头记录(V1)
        /// </summary>
        /// <returns>返回一个HeadrecV1记录</returns>
        private HeadrecV1 ReadHeadRecV1()
        {
            // 顺序读取所有数据
            var r = new HeadrecV1();
            r.OldStyleTestDate = ReadShortString(8);
            r.TestTime = ReadShortString(8);
            r.TestNo = ReadShortString(24);
            Read(r.filler3, 0, 1024);
            r.TestType = ReadShortString(22);
            r.TestPurpose = ReadShortString(10);
            r.Operator = ReadShortString(15);
            r.Engineer = ReadShortString(15);
            r.SecChief = ReadShortString(15);
            r.Manager = ReadShortString(15);
            r.Mode = ReadShortString(15);
            r.Purpose = ReadShortString(15);
            r.filler1 = ReadShortString(8);
            r.filler2 = ReadShortString(8);
            r.PropChan = ReadByte();
            for (int i = 0; i < 4; i++)
            {
                r.DummyInfo[i] = ReadDummyRec();
            }

            r.SpeedGate = ReadReal48();
            r.Remarks = ReadShortString(179);
            r.Restraint = ReadShortString(20);
            r.TotalWeight = ReadSingle();
            r.Temperature = ReadByte();
            r.Humidity = ReadByte();
            r.BarometricPress = ReadSingle();
            r.SledInfo = ReadSledDataRec();

            r.RecipeFile = ReadShortString(8);
            r.PinNo = ReadShortString(15);
            r.Customer = ReadShortString(15);
            r.CtrlFileName = ReadShortString(8);
            r.SledType = ReadShortString(6);
            r.MIRAConfig = ReadShortString(6);
            r.TstType = ReadByte();
            r.TCType = ReadByte();
            for (int i = 0; i < 10; i++)
            {
                r.Items[i] = ReadItemHeadRec();
            }

            r.HeaderDate = ReadDouble();
            r.Platform = ReadShortString(15);
            r.Requestor = ReadShortString(15);
            r.ProjectNum = ReadShortString(15);

            r.TestDate = ReadShortString(10);
            r.PostTestRemarks = ReadShortString(100);
            r.TestDuration = ReadDouble();
            r.TestCompletionDate = ReadInt32();
            r.TestCompletionTime = ReadInt32();
            r.CNFFileName = ReadShortString(30);
            r.SeqFileName = ReadShortString(30);
            r.TireInflation = ReadSingle();
            r.TireLoad = ReadSingle();
            r.TireRadius = ReadSingle();
            r.Energy = ReadSingle();
            r.Force = ReadSingle();
            r.Penetration = ReadSingle();
            r.TargetLoad = ReadSingle();
            r.TargetDeflection = ReadSingle();
            r.TireOrientation = ReadShortString(30);
            r.TireSize = ReadShortString(30);
            r.VectorRate = ReadSingle();
            r.RimOffset = ReadSingle();
            r.PlungerDiameter = ReadShortString(30);
            r.Feature = ReadShortString(30);
            r.Specification = ReadShortString(30);
            r.Status = ReadShortString(30);
            r.filler = ReadBytes(1024);
            return r;
        }

        /// <summary>
        /// 读取头记录(V2)
        /// </summary>
        /// <returns>返回一个HeadrecV1记录</returns>
        private HeadrecV2 ReadHeadRecV2()
        {
            // 顺序读取所有数据
            var r = new HeadrecV2();
            Read(r.filler1, 0, 256);
            r.TestNo = ReadShortString(30);
            r.Operators = ReadShortString(15);
            r.Engineer = ReadShortString(15);
            r.Customer = ReadShortString(15);
            r.TestPurpose = ReadShortString(30);
            r.TestType = ReadShortString(20);
            r.TestStandard = ReadShortString(20);
            r.RequestDep = ReadShortString(15);
            r.Requestor = ReadShortString(15);
            r.ProjectNum = ReadShortString(15);
            r.TestDate = ReadShortString(10);
            r.TestTime = ReadShortString(8);
            r.Manager = ReadShortString(15);
            r.Mode = ReadShortString(15);
            r.Purpose = ReadShortString(15);
            r.Title = ReadShortString(10);
            r.TestCompletionDate = ReadInt32();
            r.TestCompletionTime = ReadInt32();
            r.Feature = ReadShortString(30);
            r.Specification = ReadShortString(30);
            r.TestMachine = ReadShortString(10);
            r.OldStyleTestDate = ReadShortString(8);
            Read(r.filler2, 0, 236);

            r.TireNumber = ReadShortString(10);
            r.TireSize = ReadShortString(30);
            r.TireSN = ReadShortString(20);
            r.TireType = ReadShortString(10);
            r.LoadIndex = ReadShortString(10);
            r.SpeedLevel = ReadShortString(4);
            r.IsTubeLess = ReadShortString(4);
            r.Manufacture = ReadShortString(30);
            r.Brand = ReadShortString(10);
            r.Pattern = ReadShortString(10);
            r.TirePly = ReadShortString(10);

            r.TireInflation = ReadSingle();
            r.TireLoad = ReadSingle();
            r.TireRadius = ReadSingle();
            r.TireDia = ReadSingle();
            r.TireWeight = ReadSingle();
            r.TRAWeight = ReadSingle();

            r.TireDate = ReadShortString(10);
            r.TireWeeks = ReadShortString(10);
            r.TireOrientation = ReadShortString(20);
            r.TireDesign = ReadShortString(10);
            r.TreadHardness = ReadShortString(10);
            r.RimOffset = ReadSingle();
            r.WheelWidth = ReadSingle();
            r.wheelSize = ReadShortString(20);
            r.wheelType = ReadShortString(10);
            r.Remarks = ReadShortString(128);
            Read(r.filler3, 0, 230);

            r.Temperature = ReadSingle();
            r.Humidity = ReadSingle();
            r.CNFFileName = ReadShortString(30);
            r.SeqFileName = ReadShortString(30);
            r.CtrlFileName = ReadShortString(8);
            r.RecipeFile = ReadShortString(8);

            r.TargetLoad = ReadSingle();
            r.TargetDeflection = ReadSingle();
            r.TargetInflation = ReadSingle();
            r.VectorRatePz = ReadSingle();
            r.VectorRatePxy = ReadSingle();
            r.VectorRateAz = ReadSingle();
            r.TestDuration = ReadSingle();
            r.PlungerDiameter = ReadSingle();
            r.TestPlateHeight = ReadSingle();
            r.Platforms = ReadShortString(15);
            r.PinNo = ReadShortString(15);

            r.Energy = ReadSingle();
            r.Force = ReadSingle();
            r.Penetration = ReadSingle();
            Read(r.filler4, 0, 121);

            r.PropChan = ReadByte();
            for (int i = 0; i < 4; i++)
            {
                r.DummyInfo[i] = ReadDummyRec();
            }

            r.SpeedGate = ReadReal48();
            r.Restraint = ReadShortString(20);
            r.SledInfo = ReadSledDataRec();
            r.SledType = ReadShortString(6);
            r.MIRAConfig = ReadShortString(6);
            r.TstType = ReadByte();
            r.TCType = ReadByte();
            for (int i = 0; i < 10; i++)
            {
                r.Items[i] = ReadItemHeadRec();
            }
            r.HeaderDate = ReadDouble();
            r.PostTestRemarks = ReadShortString(100);
            r.status = ReadShortString(22);
            r.SampleFrequency = ReadInt32();
            Read(r.filler5, 0, 1020);
            return r;
        }

        /// <summary>
        /// 读取一个ItemHeadRec记录
        /// </summary>
        /// <returns>返回ItemHeadRec记录</returns>
        private ItemHeadRec ReadItemHeadRec()
        {
            var r = new ItemHeadRec();
            r.InfSerialNo = ReadShortString(20);
            r.ModSerialNo = ReadShortString(20);
            r.ModManufacturer = ReadShortString(15);
            r.ModColor = ReadShortString(10);
            r.Extra1 = ReadShortString(4);
            r.ModPartNo = ReadShortString(20);
            r.Temperature = ReadShortString(10);
            r.Vehicle = ReadShortString(10);
            r.SampleType = ReadShortString(10);
            r.Extra2 = ReadShortString(30);
            return r;
        }

        /// <summary>
        /// 读取ReadSledDataRec记录
        /// </summary>
        /// <returns>返回ReadSledDataRec记录</returns>
        private SledDataRec ReadSledDataRec()
        {
            var r = new SledDataRec();
            for (int i = 0; i <= Globals.MaxPLCRegs; i++)
            {
                r.ReqValue[i] = ReadSingle();
            }
            for (int i = 0; i <= Globals.MaxPLCRegs; i++)
            {
                r.FireValue[i] = ReadSingle();
            }
            r.extras = ReadShortString(26);
            return r;
        }

        /// <summary>
        /// 读取ReadDummyRec记录
        /// </summary>
        /// <returns>返回ReadDummyRec记录</returns>
        private DummyRec ReadDummyRec()
        {
            var r = new DummyRec();
            r.DummyDesc = ReadShortString(15);
            r.SeatBelt = ReadChar();
            r.SqbSelect = ReadChar();
            r.SqbDelay = ReadShort();
            r.DumLoc = ReadShortString(2);
            r.PreSeatBeltTension = ReadSingle();
            r.PreBuckleRelease = ReadSingle();
            for (int i = 0; i < 3; i++)
            {
                r.DumInst[i] = ReadShortString(25);
            }
            return r;
        }
        #endregion
        #region basic functions

        /// <summary>
        /// 跳过count字节
        /// </summary>
        public void Skip(int count)
        {
            m_Reader.ReadBytes(count);
        }

        /// <summary>
        /// 读取一个逻辑型(bool)数据
        /// </summary>
        /// <returns>返回一个bool数据</returns>
        public bool ReadBoolean()
        {
            return m_Reader.ReadBoolean();
        }

        /// <summary>
        /// 读取一个字节型(byte)数据
        /// </summary>
        /// <returns>返回一个字节型(byte)数据</returns>
        public byte ReadByte()
        {
            return m_Reader.ReadByte();
        }

        /// <summary>
        /// 读取一个delphi real型数据
        /// </summary>
        /// <returns>返回real对应的double值</returns>
        public double ReadReal48()
        {
            // 读取real型数据6字节
            var real48 = m_Reader.ReadBytes(6);

            if (real48[0] == 0)
                return 0.0; // Null exponent = 0
            // 转换成double值
            double exponent = real48[0] - 129.0;
            double mantissa = 0.0;

            for (int i = 1; i < 5; i++) // loop through bytes 1-4
            {
                mantissa += real48[i];
                mantissa *= 0.00390625; // mantissa /= 256
            }


            mantissa += (real48[5] & 0x7F);
            mantissa *= 0.0078125; // mantissa /= 128
            mantissa += 1.0;

            if ((real48[5] & 0x80) == 0x80) // Sign bit check
                mantissa = -mantissa;

            return mantissa * Math.Pow(2.0, exponent);
        }

        /// <summary>
        /// 读取字节数组
        /// </summary>
        /// <param name="count">读取数量</param>
        /// <returns>返回字节数组</returns>
        public byte[] ReadBytes(int count)
        {
            return m_Reader.ReadBytes(count);
        }

        /// <summary>
        /// 读取单精度浮点
        /// </summary>
        /// <returns>返回一个单精度浮点值</returns>
        public float ReadSingle()
        {
            return m_Reader.ReadSingle();
        }

        /// <summary>
        /// 读取双精度浮点
        /// </summary>
        /// <returns>返回一个双精度浮点值</returns>
        public double ReadDouble()
        {
            return m_Reader.ReadDouble();
        }

        /// <summary>
        /// 读取一个短整型，对应smallint
        /// </summary>
        /// <returns>返回一个短整型</returns>
        public short ReadShort()
        {
            return m_Reader.ReadInt16();
        }

        /// <summary>
        /// 读取一个无符号短整型，对应word
        /// </summary>
        /// <returns>返回一个无符号短整型</returns>
        public ushort ReadUShort()
        {
            return m_Reader.ReadUInt16();
        }

        /// <summary>
        /// 读取一个单字节字符，对应delphi的AnsiChar(Char)
        /// </summary>
        /// <returns>返回一个字符</returns>
        public char ReadChar()
        {
            return (char)m_Reader.ReadByte();
        }

        /// <summary>
        /// 读取字节数组
        /// </summary>
        /// <param name="buffer">目标缓冲区</param>
        /// <param name="index">起始索引</param>
        /// <param name="count">数量</param>
        /// <returns>返回读取数量</returns>
        public int Read(byte[] buffer, int index, int count)
        {
            return m_Reader.Read(buffer, index, count);
        }

        /// <summary>
        /// 读取一个整型值，对应delphi的Integer和LongInt
        /// </summary>
        /// <returns>返回一个整型值</returns>
        public int ReadInt32()
        {
            return m_Reader.ReadInt32();
        }

        /// <summary>
        /// 读取一个delphi的短字符串
        /// </summary>
        /// <param name="Length">字符串最大长度</param>
        /// <returns>返回一个字符串</returns>
        public string ReadShortString(int Length)
        {
            // 短字符串占用内存为最大长度(Length)+1，读取到字节数组
            var arr = m_Reader.ReadBytes(Length + 1);
            if (arr[0] == 0)
            {
                return "";
            }
            else
            {
                int l = Math.Min(arr[0], Length);
                // 首字节为长度
                byte[] str = new byte[l];
                Array.Copy(arr, 1, str, 0, l);
                // 按照ascii码转换成字符串
                return Encoding.ASCII.GetString(str);
            }
        }
        #endregion

        /// <summary>
        /// zta数据, v1
        /// </summary>
        //public ZTADataV1 DataV1 { get => m_Data_V1; }


        /// <summary>
        /// zta数据, v1
        /// </summary>
        public ZTADataV2 DataV2 { get => m_Data_V2; }
    }
}
