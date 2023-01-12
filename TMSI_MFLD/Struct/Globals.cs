namespace zta_convert
{
    using System;
    using TSTDate = System.Int32;
    using TSTTime = System.Int32;

    /// <summary>
    /// 常量定义
    /// </summary>
    public class Globals
    {
        public const int MaxPLCRegs = 15;
        public const int MinPnt = -1024;
        public const int MaxPnt = 75000;//7167;
        public const int PntsPERchan = MaxPnt - MinPnt + 1;
    }

    [Serializable]
    public class ConvertedDataArray
    {
        public readonly float[] Data = new float[Globals.PntsPERchan];
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
        public readonly float[] ReqValue = new float[Globals.MaxPLCRegs + 1];
        public readonly float[] FireValue = new float[Globals.MaxPLCRegs + 1];
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
    };


    public class HeadrecV1
    {
        public string OldStyleTestDate; // string[8], Stored in Testing Program
        public string TestTime;  // string[8], Stored in Testing Program 
        public string TestNo;   // string[24]
        public readonly byte[] filler3 = new byte[1024];  // 0..1023
        public string TestType; //String[22];  // Really Tire Serial Number
        public string TestPurpose;// string[10];           
        public string Operator;//String[15];       // Technician
        public string Engineer;// string[15];
        public string SecChief;// string[15];
        public string Manager;// string[15];
        public string Mode;// string[15];
        public string Purpose;// string[15];
        public string filler1;// String[8]; //CNFFileName
        public string filler2;// string[8]; //SeqFileName
        public byte PropChan;
        public readonly DummyRec[] DummyInfo = new DummyRec[4]; // 4
        public double SpeedGate; // real48
        public string Remarks; // 179
        public string Restraint; // 20
        public float TotalWeight;
        public byte Temperature;
        public byte Humidity;
        public float BarometricPress;
        public SledDataRec SledInfo;
        public string RecipeFile; // 8, 201 Test Setup
        public string PinNo; // 15
        public string Customer; /// 15
        public string CtrlFileName; //8, Cardcage Control Setup File
        public string SledType; // 6;
        public string MIRAConfig; // 6
        public byte TstType;
        public byte TCType;
        public readonly ItemHeadRec[] Items = new ItemHeadRec[10];
        public double HeaderDate;
        public string Platform; // string[15];
        public string Requestor; // string[15];
        public string ProjectNum; // string[15];

        //  public double   StartTime;
        //  public int SoakLength // equals minutes

        public string TestDate; // string[10];
        public string PostTestRemarks;// string[100];
        public double TestDuration;
        public TSTDate TestCompletionDate;
        public TSTTime TestCompletionTime;
        public string CNFFileName; // String[30];
        public string SeqFileName; // string[30];
        public float TireInflation; // always PSI, entered by operator
        public float TireLoad;// Always Pounds
        public float TireRadius; // Always Inches

        // Plunger and BPO Data
        public float
            Energy,
            Force,
            Penetration;  // English
        public float
            TargetLoad,
            TargetDeflection;  // English

        public string TireOrientation; // string[30]
        public string TireSize;//string[30]
        public float VectorRate;
        public float RimOffset;
        public string PlungerDiameter; // string[30];
        public string Feature;// string[30];
        public string Specification;// string[30];
        public string Status;// string[30]; // not used yet, put in place just in case
 
        public byte[] filler = new byte[1024];//1024
    }

    public enum tDataSource
    {
        EMEData,
        MicroStarData,
        OtherData
    }

    public enum PrePostType
    {
        Pre = 0,
        Post = 1
    }

    public class ShuntCalResultRec
    {
        public bool ExcitationFailed;
        public bool PosShuntFailed;
        public bool NegShuntFailed;
        public double PosShunt; // real
        public double NegShunt; // real;
    }

    [Serializable]
    public class TestCFGrec
    {
        public byte ChanNum;
        public double Gain; // real48
        public string SENTYP; // Str2;   // qualifier
        public string DataDesc; // Str16;  // qualifier
        public string SENLOC;// Str2;
        public string SENATT; // Str4;
        public string AXIS; // Str2;
        public string SenID; // Str7;
        public double ReqScale;// real48
        public string Polarity;// string[1];
        public double Sensi; //real48
        public float Excitation;
        public double BridgeRes;// real48
        public double MaxFullScale;//real48
        public double OutputAtFS; //real48
        public string Units;// Str3;
        public int ZeroOfs;
        public string SerialNo;// str12;
        public double SampRate;// real;
        public double BridgeSC;// real;
        public double GainSC; // real;
        public string ReptFilter;// str6;
        public bool CalRun;// boolean;
        public double CalGain;// real;
        public double CalPlusLev;// real;
        public double CalMinusLev;// real;
        public int NumCalRuns;
        public int RefNum;
        public tDataSource DataSource;
        public readonly ShuntCalResultRec[] ShuntCalRes = new ShuntCalResultRec[2]; // array[PrePostType] of ShuntCalResultRec
        public double StartTime;
        public int SoakLength; // equals minutes
        public double adc_factor;
        public int intChanNum;
        public double ActualRange;
        public ushort MicroStrain;
        public short CalPlus;
        public short CalMinus;
        public double MedianSpikeLevel;
        public int msecStartTime;
        public int msecStopTime;
        public double Offset;
        public bool UseMzVals;
        public float BeamRatio;
        public float Tb;
        public readonly byte[] filler = new byte[6]; // array[0..5] of byte;
    }

    [Serializable]
    public class HeadrecV2
    {
        public readonly byte[] filler1 = new byte[256];

        //Test information
        public string TestNo; // 30 Test Number
        public string Operators; // 15 Technician
        public string Engineer;// 15
        public string Customer;// 15
        public string TestPurpose;  //30 was 10
        public string TestType;// 20
        public string TestStandard;  // 20 Test standard
        public string RequestDep;  // 15 request department, was SecChief
        public string Requestor;// 15 
        public string ProjectNum;// 15 
        public string TestDate;// 10 
        public string TestTime;  // 8 Stored in Testing Program 
        public string Manager;// 15
        public string Mode;// 15
        public string Purpose;// 15
        public string Title;// 10
        public int TestCompletionDate;
        public int TestCompletionTime;
        public string Feature;// 30
        public string Specification; // 30
        public string TestMachine;// 10
        public string OldStyleTestDate;  // 10 Stored in Testing Program 
        public readonly byte[] filler2 = new byte[236];

        //tire information
        public string TireNumber;
        public string TireSize;
        public string TireSN;
        public string TireType;
        public string LoadIndex; // was 4
        public string SpeedLevel;
        public string IsTubeLess;
        public string Manufacture; // was 10
        public string Brand;
        public string Pattern;
        public string TirePly;
        public float
            TireInflation,
            TireLoad,
            TireRadius,
            TireDia,
            TireWeight,
            TRAWeight; // always PSI, Pounds, Inches
        public string TireDate;
        public string TireWeeks;
        public string TireOrientation;
        public string TireDesign;
        public string TreadHardness;
        public float RimOffset;
        public float WheelWidth;
        public string wheelSize;
        public string wheelType;
        public string Remarks;
        public readonly byte[] filler3 = new byte[230];

        //Test condition
        public float Temperature;
        public float Humidity;
        public string CNFFileName; // 30
        public string SeqFileName; // 30
        public string CtrlFileName; //8   Cardcage Control Setup File
        public string RecipeFile; // 8   201 Test Setup

        public float
            TargetLoad,
            TargetDeflection,
            TargetInflation,
            VectorRatePz,
            VectorRatePxy,
            VectorRateAz;  // (English)
        public float TestDuration; //
        public float PlungerDiameter;
        public float TestPlateHeight;
        public string Platforms; //15
        public string PinNo; //15

        //Plunger and BPO Data
        public float
            Energy,
            Force,
            Penetration;  // English
        public readonly byte[] filler4 = new byte[121];

        //unknow but leave it here
        public byte PropChan;
        public readonly DummyRec[] DummyInfo = new DummyRec[4];
        public double SpeedGate;// real48;
        public string Restraint;//20
        public SledDataRec SledInfo;
        public string SledType;//6
        public string MIRAConfig;// 6
        public byte TstType;        // 0 = PAB; 1 = DAB
        public byte TCType;        // 0 = J Type Thermocouple 1 = K Type Thermocouple
        public readonly ItemHeadRec[] Items = new ItemHeadRec[10];
        public double HeaderDate;
        public string PostTestRemarks;// 100
        public string status;//22
        //新增采样频率 {如果在1~249之间就按照这个采样，如果是0or大于250就原始数据}
        public int SampleFrequency;
        public readonly byte[] filler5 = new byte[1020];
    }

}
