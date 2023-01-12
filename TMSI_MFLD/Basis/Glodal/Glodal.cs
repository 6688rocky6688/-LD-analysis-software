using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using TMSI_MFLD.Basis.EventArgs;

namespace TMSI_MFLD.Basis.Glodal
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class Glodal
    {
        #region 主界面时间修改事件

        public class MainTimeChanged
        {
            public static event EventHandler TimeChanged;

            private static bool _starttime;
            public static bool StartTime
            {
                get
                {
                    return _starttime;
                }
                set
                {
                    if (_starttime != value)
                    {
                        if (TimeChanged != null)
                        {
                            ParamEventArgs pe = new ParamEventArgs(DateTime.Now);
                            TimeChanged(null, pe);
                        }
                    }
                }
            }
        }

        #endregion

        #region 返回主页面事件

        public class ExitFrmChanged
        {
            public static event EventHandler FrmChanged;

            private static bool _exit;
            public static bool Exit
            {
                get
                {
                    return _exit;
                }
                set
                {
                    if (_exit != value)
                    {
                        if (FrmChanged != null)
                        {
                            ParamEventArgs pe = new ParamEventArgs(null);
                            FrmChanged(null, pe);
                        }
                    }
                }
            }
        }

        #endregion

        #region 头文件

        public const int MaxPLCRegs = 15;
        public const int MinPnt = -1024;
        public const int MaxPnt = 75000;//7167;
        public const int PntsPERchan = MaxPnt - MinPnt + 1;

        [Serializable]
        public class HeadrecV2
        {
            public static readonly byte[] filler1 = new byte[256];

            //Test information
            public static string TestNo; // 30 Test Number
            public static string Operators; // 15 Technician
            public static string Engineer;// 15
            public static string Customer;// 15
            public static string TestPurpose;  //30 was 10
            public static string TestType;// 20
            public static string TestStandard;  // 20 Test standard
            public static string RequestDep;  // 15 request department, was SecChief
            public static string Requestor;// 15 
            public static string ProjectNum;// 15 
            public static string TestDate;// 10 
            public static string TestTime;  // 8 Stored in Testing Program 
            public static string Manager;// 15
            public static string Mode;// 15
            public static string Purpose;// 15
            public static string Title;// 10
            public static int TestCompletionDate;
            public static int TestCompletionTime;
            public static string Feature;// 30
            public static string Specification; // 30
            public static string TestMachine;// 10
            public static string OldStyleTestDate;  // 10 Stored in Testing Program 
            public static readonly byte[] filler2 = new byte[236];

            //tire information
            public static string TireNumber;
            public static string TireSize;
            public static string TireSN;
            public static string TireType;
            public static string LoadIndex; // was 4
            public static string SpeedLevel;
            public static string IsTubeLess;
            public static string Manufacture; // was 10
            public static string Brand;
            public static string Pattern;
            public static string TirePly;
            public static float TireInflation, TireLoad, TireRadius, TireDia, TireWeight, TRAWeight; // always PSI, Pounds, Inches
            public static string TireDate;
            public static string TireWeeks;
            public static string TireOrientation;
            public static string TireDesign;
            public static string TreadHardness;
            public static float RimOffset;
            public static float WheelWidth;
            public static string wheelSize;
            public static string wheelType;
            public static string Remarks;
            public static readonly byte[] filler3 = new byte[230];

            //Test condition
            public static float Temperature;
            public static float Humidity;
            public static string CNFFileName; // 30
            public static string SeqFileName; // 30
            public static string CtrlFileName; //8   Cardcage Control Setup File
            public static string RecipeFile; // 8   201 Test Setup

            public static float TargetLoad, TargetDeflection, TargetInflation, VectorRatePz, VectorRatePxy, VectorRateAz;  // (English)
            public static float TestDuration; //
            public static float PlungerDiameter;
            public static float TestPlateHeight;
            public static string Platforms; //15
            public static string PinNo; //15

            //Plunger and BPO Data
            public static float Energy, Force, Penetration;  // English
            public static readonly byte[] filler4 = new byte[121];

            //unknow but leave it here
            public static byte PropChan;
            public static readonly DummyRec[] DummyInfo = new DummyRec[4];
            public static double SpeedGate;// real48;
            public static string Restraint;//20
            public static SledDataRec SledInfo;
            public static string SledType;//6
            public static string MIRAConfig;// 6
            public static byte TstType;        // 0 = PAB; 1 = DAB
            public static byte TCType;        // 0 = J Type Thermocouple 1 = K Type Thermocouple
            public static readonly ItemHeadRec[] Items = new ItemHeadRec[10];
            public static double HeaderDate;
            public static string PostTestRemarks;// 100
            public static string status;//22
            public static readonly byte[] filler5 = new byte[1024];

        }

        public class DummyRec
        {
            public string DummyDesc; //15; Dummy Code Number    
            public char SeatBelt; // Y or N
            public char SqbSelect;     // Y or N
            public short SqbDelay; // 0 to 100 msec
            public string DumLoc; // 2
            public float PreSeatBeltTension;
            public float PreBuckleRelease;
            public readonly string[] DumInst = new string[3]; // array[1..3] of str25
        }
        public class SledDataRec
        {
            public readonly float[] ReqValue = new float[MaxPLCRegs + 1];
            public readonly float[] FireValue = new float[MaxPLCRegs + 1];
            public string extras; //26
        }

        public class ItemHeadRec
        {
            public string InfSerialNo; // 20
            public string ModSerialNo;  // 20
            public string ModManufacturer; // 15
            public string ModColor;// 10
            public string Extra1;// 4
            public string ModPartNo;// 20
            public string Temperature;// 10
            public string Vehicle;// 10
            public string SampleType;// 10
            public string Extra2;// 30
        }

        #endregion

        #region 数据库链接字符串

        public class SqlConStr
        {
            public static string Sqlconnection
            {
                get;
                set;
            }
        }

        #endregion

        #region 曲线过程值全局存储

        public class ChartSeries
        {
            private static Series _chattseries;

            public static Series ChartSerie
            {
                get
                {
                    return _chattseries;
                }
                set
                {
                    _chattseries = value;
                }
            }
        }

        #endregion

        #region 图表dataset

        public class PreviewDataSet
        {
            private static DataSet _rpdataset;

            public static DataSet RPDataSet
            {
                get
                {
                    if(_rpdataset==null)
                    {
                        _rpdataset = new DataSet();
                    }
                    return _rpdataset;
                }
                set
                {
                    _rpdataset = value;
                }
            }
        }
        #endregion

        #region language

        public class languageCode
        {
            public static string _languagecode;

            public static string languagecode
            {
                get
                {

                    return _languagecode;
                }
                set
                {
                    _languagecode = value;
                }
            }
        }

        #endregion 
    }
}