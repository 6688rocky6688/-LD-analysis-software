using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Data;
using FormControls.Helpers;
using System.Windows.Forms;
using Spire.Xls;
using TMSI_MFLD.Forms.ShowDialog;
using static TMSI_MFLD.Forms.ShowDialog.FrmMode;
using TMSI_MFLD.Basis;
using static TMSI_MFLD.Basis.TransitionHelper;

namespace TMSI_MFLD.Excel
{
    public class ExcelOperations
    {
        #region 字段定义

        struct Csv
        {
            public string xname;
            public string yname;
            public double[] xvalue;
            public double[] yvalue;
        };

        //文件路径
        static string _path;

        public static object IDataFormat { get; private set; }

        #endregion

        #region 构造函数

        #endregion

        #region 方法定义

        /// <summary>
        /// 新建模板Excel，并针对单元格进行赋值。
        /// </summary>
        public static void Excel(DataTable dt, Dictionary<double, double[]> keyValues, double slope, string unit, string filetype)
        {
            if (filetype == null || filetype == string.Empty) return;
            switch (filetype)
            {
                case "KLat":
                    _path = "Excel\\Excel_Lateral.xls";
                    break;
                case "KLong":
                    _path = "Excel\\Excel_Longitudinal.xls";
                    break;
                case "Ktor":
                    _path = "Excel\\Excel_Torsional.xls";
                    break;
                case "Kv":
                    _path = "Excel\\Excel_Radial.xls";
                    break;
                default:
                    return;
            }

            #region 获取保存路径

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xls files(*.xls)|*.xls";//保存文件类型
            sfd.FileName = String.Format("{0:yyyyMMdd}", DateTime.Now);//保存文件名称
            sfd.AddExtension = true;
            DialogResult result = sfd.ShowDialog();//打开保存界面

            #endregion

            #region 开始保存

            if (result == DialogResult.OK)
            {
                //获得文件路径
                string localFilePath = sfd.FileName.ToString();
                //模板文件流
                FileStream fs = File.OpenRead(_path);
                //另存文件流
                FileStream fileStream = new FileStream(sfd.FileName, FileMode.Create);
                try
                {
                    IWorkbook workbook = null;
                    //判断excel文件格式
                    if (_path.IndexOf(".xlsx") > 0) // 2007版本
                        workbook = new XSSFWorkbook(fs);
                    else if (_path.IndexOf(".xls") > 0) // 2003版本
                        workbook = new HSSFWorkbook(fs);
                    // 获取此文件第一个Sheet页
                    var sheet = workbook.GetSheetAt(0);

                    #region 赋值开始

                    sheet.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["TestNo"].ToString());
                    sheet.GetRow(7).GetCell(1).SetCellValue(dt.Rows[0]["Operators"].ToString());
                    sheet.GetRow(6).GetCell(5).SetCellValue(dt.Rows[0]["TestPurpose"].ToString());
                    sheet.GetRow(6).GetCell(1).SetCellValue(dt.Rows[0]["Requestor"].ToString());
                    sheet.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["TestDate"].ToString());
                    sheet.GetRow(9).GetCell(1).SetCellValue(dt.Rows[0]["TireSize"].ToString());
                    sheet.GetRow(10).GetCell(1).SetCellValue(dt.Rows[0]["TireSN"].ToString());
                    sheet.GetRow(9).GetCell(3).SetCellValue(dt.Rows[0]["LoadIndex"].ToString());
                    sheet.GetRow(10).GetCell(3).SetCellValue(dt.Rows[0]["SpeedLevel"].ToString());
                    sheet.GetRow(11).GetCell(1).SetCellValue(dt.Rows[0]["IsTubeLess"].ToString());
                    sheet.GetRow(4).GetCell(1).SetCellValue(dt.Rows[0]["Manufacture"].ToString());
                    sheet.GetRow(9).GetCell(5).SetCellValue(dt.Rows[0]["Brand"].ToString());
                    sheet.GetRow(10).GetCell(5).SetCellValue(dt.Rows[0]["Pattern"].ToString());
                    sheet.GetRow(11).GetCell(5).SetCellValue(dt.Rows[0]["TirePly"].ToString());
                    sheet.GetRow(13).GetCell(6).SetCellValue(dt.Rows[0]["Platforms"].ToString());
                    sheet.GetRow(11).GetCell(3).SetCellValue(dt.Rows[0]["TireWeeks"].ToString());
                    sheet.GetRow(14).GetCell(6).SetCellValue(dt.Rows[0]["wheelSize"].ToString());
                    sheet.GetRow(5).GetCell(1).SetCellValue(dt.Rows[0]["RequestDep"].ToString());
                    sheet.GetRow(30).GetCell(2).SetCellValue(slope);
                    //xy百分比数值
                    sheet.GetRow(20).GetCell(1).SetCellValue(keyValues[1][1]);
                    sheet.GetRow(20).GetCell(2).SetCellValue(keyValues[1][0]);
                    sheet.GetRow(21).GetCell(1).SetCellValue(keyValues[2][1]);
                    sheet.GetRow(21).GetCell(2).SetCellValue(keyValues[2][0]);
                    sheet.GetRow(22).GetCell(1).SetCellValue(keyValues[3][1]);
                    sheet.GetRow(22).GetCell(2).SetCellValue(keyValues[3][0]);
                    sheet.GetRow(23).GetCell(1).SetCellValue(keyValues[4][1]);
                    sheet.GetRow(23).GetCell(2).SetCellValue(keyValues[4][0]);
                    sheet.GetRow(24).GetCell(1).SetCellValue(keyValues[5][1]);
                    sheet.GetRow(24).GetCell(2).SetCellValue(keyValues[5][0]);
                    sheet.GetRow(25).GetCell(1).SetCellValue(keyValues[6][1]);
                    sheet.GetRow(25).GetCell(2).SetCellValue(keyValues[6][0]);
                    sheet.GetRow(26).GetCell(1).SetCellValue(keyValues[7][1]);
                    sheet.GetRow(26).GetCell(2).SetCellValue(keyValues[7][0]);
                    sheet.GetRow(27).GetCell(1).SetCellValue(keyValues[8][1]);
                    sheet.GetRow(27).GetCell(2).SetCellValue(keyValues[8][0]);
                    sheet.GetRow(28).GetCell(1).SetCellValue(keyValues[9][1]);
                    sheet.GetRow(28).GetCell(2).SetCellValue(keyValues[9][0]);
                    sheet.GetRow(29).GetCell(1).SetCellValue(keyValues[10][1]);
                    sheet.GetRow(29).GetCell(2).SetCellValue(keyValues[10][0]);

                    //以下需要根据单位进行转换。
                    double TireLoad, TireRadius, TireDia, TireWeight, Temperature, TargetLoad, TargetInflation, VectorRatePz, VectorRatePxy;

                    try
                    {
                        TireLoad = double.Parse(dt.Rows[0]["TireLoad"].ToString());
                        TireRadius = double.Parse(dt.Rows[0]["TireRadius"].ToString());
                        TireDia = double.Parse(dt.Rows[0]["TireDia"].ToString());
                        TireWeight = double.Parse(dt.Rows[0]["TireWeight"].ToString());
                        Temperature = double.Parse(dt.Rows[0]["Temperature"].ToString());
                        TargetLoad = double.Parse(dt.Rows[0]["TargetLoad"].ToString());
                        TargetInflation = double.Parse(dt.Rows[0]["TargetInflation"].ToString());
                        VectorRatePz = double.Parse(dt.Rows[0]["VectorRatePz"].ToString());
                        VectorRatePxy = double.Parse(dt.Rows[0]["VectorRatePxy"].ToString());

                        switch (unit)
                        {
                            case "English":
                                //Max Load
                                sheet.GetRow(9).GetCell(6).SetCellValue(sheet.GetRow(9).GetCell(6).StringCellValue + "Ibs");
                                //Max Inflate Pressure
                                sheet.GetRow(10).GetCell(6).SetCellValue(sheet.GetRow(10).GetCell(6).StringCellValue + "psi");
                                //Tire Weight
                                sheet.GetRow(11).GetCell(6).SetCellValue(sheet.GetRow(11).GetCell(6).StringCellValue + "Ibs");
                                //Temperature
                                sheet.GetRow(13).GetCell(0).SetCellValue(sheet.GetRow(13).GetCell(0).StringCellValue + "F");
                                //Test Pressure
                                sheet.GetRow(14).GetCell(0).SetCellValue(sheet.GetRow(14).GetCell(0).StringCellValue + "psi");
                                //Tire Radius
                                sheet.GetRow(15).GetCell(0).SetCellValue(sheet.GetRow(15).GetCell(0).StringCellValue + "inch");
                                //Travel Speed of Pxy
                                sheet.GetRow(16).GetCell(0).SetCellValue(sheet.GetRow(16).GetCell(0).StringCellValue + "in/min");
                                //Radial Load，Ibs	
                                sheet.GetRow(17).GetCell(0).SetCellValue(sheet.GetRow(17).GetCell(0).StringCellValue + "Ibs");
                                //Load Speed of Pz,in/ min
                                sheet.GetRow(16).GetCell(4).SetCellValue(sheet.GetRow(16).GetCell(4).StringCellValue + "in/min");
                                //Load，Ibs
                                sheet.GetRow(19).GetCell(1).SetCellValue(sheet.GetRow(19).GetCell(1).StringCellValue + "Ibs");
                                switch (filetype)
                                {
                                    case "KLat":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "in");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "Ibs/in");
                                        break;
                                    case "KLong":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "in");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "Ibs/in");
                                        break;
                                    case "Ktor":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "°");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "Ibs/°");
                                        break;
                                    case "Kv":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "in");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "Ibs/in");
                                        break;
                                }
                                break;
                            case "SI":
                                //Max Load
                                sheet.GetRow(9).GetCell(6).SetCellValue(sheet.GetRow(9).GetCell(6).StringCellValue + "n");
                                //Max Inflate Pressure
                                sheet.GetRow(10).GetCell(6).SetCellValue(sheet.GetRow(10).GetCell(6).StringCellValue + "kpa");
                                //Tire Weight
                                sheet.GetRow(11).GetCell(6).SetCellValue(sheet.GetRow(11).GetCell(6).StringCellValue + "n");
                                //Temperature
                                sheet.GetRow(13).GetCell(0).SetCellValue(sheet.GetRow(13).GetCell(0).StringCellValue + "°C");
                                //Test Pressure
                                sheet.GetRow(14).GetCell(0).SetCellValue(sheet.GetRow(14).GetCell(0).StringCellValue + "kpa");
                                //Tire Radius
                                sheet.GetRow(15).GetCell(0).SetCellValue(sheet.GetRow(15).GetCell(0).StringCellValue + "mm");
                                //Travel Speed of Pxy
                                sheet.GetRow(16).GetCell(0).SetCellValue(sheet.GetRow(16).GetCell(0).StringCellValue + "mm/min");
                                //Radial Load，Ibs	
                                sheet.GetRow(17).GetCell(0).SetCellValue(sheet.GetRow(17).GetCell(0).StringCellValue + "n");
                                //Load Speed of Pz,in/ min
                                sheet.GetRow(16).GetCell(4).SetCellValue(sheet.GetRow(16).GetCell(4).StringCellValue + "mm/min");
                                //Load，Ibs
                                sheet.GetRow(19).GetCell(1).SetCellValue(sheet.GetRow(19).GetCell(1).StringCellValue + "n");
                                switch (filetype)
                                {
                                    case "KLat":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "mm");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "n/mm");
                                        break;
                                    case "KLong":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "mm");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "n/mm");
                                        break;
                                    case "Ktor":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "°");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "n/°");
                                        break;
                                    case "Kv":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "mm");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "n/mm");
                                        break;
                                }
                                TireLoad = TireLoad.EnglishToSi(Units.n);
                                TireRadius = TireRadius.EnglishToSi(Units.kpa);
                                TireDia = TireDia.EnglishToSi(Units.inch);
                                TireWeight = TireWeight.EnglishToSi(Units.n);
                                Temperature = Temperature.EnglishToSi(Units.c);
                                TargetLoad = TargetLoad.EnglishToSi(Units.n);
                                TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                                VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                                VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);
                                break;
                            case "MKS":
                                //Max Load
                                sheet.GetRow(9).GetCell(6).SetCellValue(sheet.GetRow(9).GetCell(6).StringCellValue + "kg");
                                //Max Inflate Pressure
                                sheet.GetRow(10).GetCell(6).SetCellValue(sheet.GetRow(10).GetCell(6).StringCellValue + "kpa");
                                //Tire Weight
                                sheet.GetRow(11).GetCell(6).SetCellValue(sheet.GetRow(11).GetCell(6).StringCellValue + "kg");
                                //Temperature
                                sheet.GetRow(13).GetCell(0).SetCellValue(sheet.GetRow(13).GetCell(0).StringCellValue + "°C");
                                //Test Pressure
                                sheet.GetRow(14).GetCell(0).SetCellValue(sheet.GetRow(14).GetCell(0).StringCellValue + "kpa");
                                //Tire Radius
                                sheet.GetRow(15).GetCell(0).SetCellValue(sheet.GetRow(15).GetCell(0).StringCellValue + "mm");
                                //Travel Speed of Pxy
                                sheet.GetRow(16).GetCell(0).SetCellValue(sheet.GetRow(16).GetCell(0).StringCellValue + "mm/min");
                                //Radial Load，Ibs	
                                sheet.GetRow(17).GetCell(0).SetCellValue(sheet.GetRow(17).GetCell(0).StringCellValue + "kg");
                                //Load Speed of Pz,in/ min
                                sheet.GetRow(16).GetCell(4).SetCellValue(sheet.GetRow(16).GetCell(4).StringCellValue + "mm/min");
                                //Load，Ibs
                                sheet.GetRow(19).GetCell(1).SetCellValue(sheet.GetRow(19).GetCell(1).StringCellValue + "kg");
                                switch (filetype)
                                {
                                    case "KLat":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "mm");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "kg/mm");
                                        break;
                                    case "KLong":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "mm");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "kg/mm");
                                        break;
                                    case "Ktor":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "°");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "kg/°");
                                        break;
                                    case "Kv":
                                        //Deflection
                                        sheet.GetRow(19).GetCell(2).SetCellValue(sheet.GetRow(19).GetCell(2).StringCellValue + "mm");
                                        //Radial Stiffness
                                        sheet.GetRow(30).GetCell(0).SetCellValue(sheet.GetRow(30).GetCell(0).StringCellValue + "kg/mm");
                                        break;
                                }
                                TireLoad = TireLoad.EnglishToSi(Units.kg);
                                TireRadius = TireRadius.EnglishToSi(Units.kpa);
                                TireDia = TireDia.EnglishToSi(Units.inch);
                                TireWeight = TireWeight.EnglishToSi(Units.kg);
                                Temperature = Temperature.EnglishToSi(Units.c);
                                TargetLoad = TargetLoad.EnglishToSi(Units.kg);
                                TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                                VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                                VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);
                                break;
                        }

                        sheet.GetRow(9).GetCell(7).SetCellValue(TireLoad);
                        sheet.GetRow(10).GetCell(7).SetCellValue(TireRadius);
                        sheet.GetRow(15).GetCell(2).SetCellValue(TireDia);
                        sheet.GetRow(11).GetCell(7).SetCellValue(TireWeight);
                        sheet.GetRow(13).GetCell(2).SetCellValue(Temperature);
                        sheet.GetRow(17).GetCell(2).SetCellValue(TargetLoad);
                        sheet.GetRow(14).GetCell(2).SetCellValue(TargetInflation);
                        sheet.GetRow(16).GetCell(6).SetCellValue(VectorRatePz);
                        sheet.GetRow(16).GetCell(2).SetCellValue(VectorRatePxy);

                    }
                    catch
                    {
                        sheet.GetRow(9).GetCell(7).SetCellValue(dt.Rows[0]["TireLoad"].ToString());
                        sheet.GetRow(10).GetCell(7).SetCellValue(dt.Rows[0]["TireRadius"].ToString());
                        sheet.GetRow(15).GetCell(2).SetCellValue(dt.Rows[0]["TireDia"].ToString());
                        sheet.GetRow(11).GetCell(7).SetCellValue(dt.Rows[0]["TireWeight"].ToString());
                        sheet.GetRow(13).GetCell(2).SetCellValue(dt.Rows[0]["Temperature"].ToString());
                        sheet.GetRow(17).GetCell(2).SetCellValue(dt.Rows[0]["TargetLoad"].ToString());
                        sheet.GetRow(14).GetCell(2).SetCellValue(dt.Rows[0]["TargetInflation"].ToString());
                        sheet.GetRow(16).GetCell(6).SetCellValue(dt.Rows[0]["VectorRatePz"].ToString());
                        sheet.GetRow(16).GetCell(2).SetCellValue(dt.Rows[0]["VectorRatePxy"].ToString());
                    }
                    #endregion

                    workbook.Write(fileStream);

                    Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Success);
                    frm_Sd_Excel.ShowDialog();

                }
                catch
                {
                    Frm_Sd_Excel frm_Sd_Excel = new Frm_Sd_Excel(FrmModeExcel.Failure);
                    frm_Sd_Excel.ShowDialog();
                }
                finally
                {
                    fileStream.Close();
                    fs.Close();
                }
            }

            #endregion
        }


        /// <summary>
        /// 同一个excel文件jk
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyValues"></param>
        /// <param name="slope"></param>
        /// <param name="unit"></param>
        public static void Excel(DataTable[] dt, Dictionary<string, List<double[]>> keyValuePairs, Dictionary<string, double> slopes, string filepath, string[] path, string unit, List<string> lstype, string type, Dictionary<int, List<double>> directory, double[] XYmin, Dictionary<int, List<double>> xdri, Dictionary<int, List<double>> ydri)
        {
            IWorkbook workb = null;

            switch (unit)
            {
                case "English":
                    _path = "Excel\\MRFLDReport_GB_En.xls";
                    break;
                case "SI":
                    _path = "Excel\\MRFLDReport_GB_SI.xls";
                    break;
                case "MKS":
                    _path = "Excel\\MRFLDReport_GB_MKS.xls";
                    break;
            }

            #region 开始保存

            //模板文件流
            FileStream fs = File.OpenRead(_path);

            FileStream fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                //判断excel文件格式
                if (_path.IndexOf(".xlsx") > 0) // 2007版本
                    workb = new XSSFWorkbook(fs);
                else if (_path.IndexOf(".xls") > 0) // 2003版本
                    workb = new HSSFWorkbook(fs);

                #region 预赋值

                for (int ii = 0; ii < lstype.Count; ii++)
                {
                    // 获取此文件第一个Sheet页
                    int num = 0;
                    int AzAngle = 0;
                    switch (lstype[ii])
                    {
                        case "KLat":
                            num = 2;
                            AzAngle = 90;
                            break;
                        case "KLong":
                            num = 1;
                            AzAngle = 0;
                            break;
                        case "Ktor":
                            num = 3;
                            AzAngle = 45;
                            break;
                        case "Kv":
                            num = 0;
                            AzAngle = 0;
                            break;
                        default:

                            break;
                    }

                    var sheet = workb.GetSheetAt(num);

                    #region 刷新原先的excel单位

                    #endregion

                    int[] excelx = new int[] { 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 10, 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18, 19, 19, 22, 22, 23, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 29, 30, 30, 31, 31, 32 };
                    int[] excely = new int[] { 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 3, 5, 7, 1, 3, 5, 7, 1, 3, 5, 7, 2, 6, 2, 6, 2, 6, 2, 6, 2, 6, 2, 6, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 2 };
                    string[] str = new string[55];

                    //HTAC No.
                    str[0] = dt[ii].Rows[0]["Customer"].ToString();
                    //Report No.
                    str[1] = dt[ii].Rows[0]["TestNo"].ToString();
                    //Request Department
                    str[2] = dt[ii].Rows[0]["RequestDep"].ToString();
                    //TestDate
                    str[3] = dt[ii].Rows[0]["TestDate"].ToString();
                    //Requestor
                    str[4] = dt[ii].Rows[0]["Requestor"].ToString();
                    //Test Purpose
                    str[5] = dt[ii].Rows[0]["TestPurpose"].ToString();
                    //Operator
                    str[6] = dt[ii].Rows[0]["Operators"].ToString();
                    //Report Export date
                    str[7] = DateTime.Now.ToString();
                    //Comments 
                    string[] strarry = path[ii].Split('\\');

                    str[8] = strarry[strarry.Length-1];//dt[ii].Rows[0]["Remarks"].ToString();
                    //Test Procedure
                    str[9] = dt[ii].Rows[0]["TestStandard"].ToString();
                    //Tire Size
                    str[10] = dt[ii].Rows[0]["TireSize"].ToString();
                    //Load Index
                    str[11] = dt[ii].Rows[0]["LoadIndex"].ToString();
                    //Brand
                    str[12] = dt[ii].Rows[0]["Brand"].ToString();
                    //Max Load (kg)
                    double maxload;
                    double.TryParse(dt[ii].Rows[0]["TireLoad"].ToString(), out maxload);
                    maxload = maxload.EnglishToSi(Units.kg);
                    str[13] = maxload.ToString("f2");
                    //Product ID
                    str[14] = dt[ii].Rows[0]["TireSN"].ToString();
                    //Speed Symbol
                    str[15] = dt[ii].Rows[0]["SpeedLevel"].ToString();
                    //Pattern
                    str[16] = dt[ii].Rows[0]["Pattern"].ToString();
                    //Max Inflate Pressure
                    double MaxInflatePressure;
                    double.TryParse(dt[ii].Rows[0]["TireInflation"].ToString(), out MaxInflatePressure);
                    MaxInflatePressure = MaxInflatePressure.EnglishToSi(Units.kpa);
                    str[17] = MaxInflatePressure.ToString("f2");
                    //Tube/Tubeless
                    str[18] = dt[ii].Rows[0]["IsTubeLess"].ToString();
                    //Manufacture Date
                    str[19] = dt[ii].Rows[0]["TireDate"].ToString();
                    //SW (mm)
                    double Sw;
                    double.TryParse(dt[ii].Rows[0]["TirePly"].ToString(), out Sw);
                    //Sw = Sw.EnglishToSi(Units.inch);
                    str[20] = Sw.ToString("f2");
                    //Tire Weight
                    double TireWeight;
                    double.TryParse(dt[ii].Rows[0]["TireWeight"].ToString(), out TireWeight);
                    TireWeight = TireWeight.EnglishToSi(Units.kg);
                    str[21] = TireWeight.ToString("f2");
                    //Platform surface Type
                    str[23] = dt[ii].Rows[0]["Platforms"].ToString();
                    //Humidity (%)
                    str[24] = dt[ii].Rows[0]["Humidity"].ToString();
                    //Test Plate Height (mm)
                    double TestPlateHeight;
                    double.TryParse(dt[ii].Rows[0]["TestPlateHeight"].ToString(), out TestPlateHeight);
                    TestPlateHeight = TestPlateHeight.EnglishToSi(Units.inch);
                    str[25] = TestPlateHeight.ToString("f2");
                    //RIM Size
                    str[27] = dt[ii].Rows[0]["wheelSize"].ToString();
                    //Tire OD
                    double TireOD;
                    double.TryParse(dt[ii].Rows[0]["TireDia"].ToString(), out TireOD);
                    TireOD = TireOD.EnglishToSi(Units.inch);
                    str[28] = TireOD.ToString("f2");
                    //RIM Offset (mm)
                    double RimOffset;
                    double.TryParse(dt[ii].Rows[0]["RimOffset"].ToString(), out RimOffset);
                    RimOffset = RimOffset.EnglishToSi(Units.inch);
                    str[29] = RimOffset.ToString("f2");
                    //Az Angle (deg)
                    str[31] = AzAngle.ToString();

                    //下面的都需要进行转换
                    double Temperature; double.TryParse(dt[ii].Rows[0]["Temperature"].ToString(), out Temperature);
                    double TireInflation; double.TryParse(dt[ii].Rows[0]["TargetInflation"].ToString(), out TireInflation);
                    double TravelSpeedofPxy = 0.0;
                    double.TryParse(dt[ii].Rows[0]["VectorRatePxy"].ToString(), out TravelSpeedofPxy);
                    double RadialLoad; double.TryParse(dt[ii].Rows[0]["TargetLoad"].ToString(), out RadialLoad);
                    double LoadSpeedofPz; double.TryParse(dt[ii].Rows[0]["VectorRatePz"].ToString(), out LoadSpeedofPz);

                    switch (unit)
                    {
                        case "English":
                            string array = sheet.GetRow(32).GetCell(0).StringCellValue;
                            array += " between ";
                            array += "( ";
                            array += (directory[ii][0] * 100).ToString();
                            array += "% ) ";
                            if (int.Parse((directory[ii][0] * 10).ToString()) - 1 > -1) array += keyValuePairs[path[ii]][int.Parse((directory[ii][0] * 10).ToString()) - 1][1].ToString("f2");
                            else array += XYmin[0].ToString("f2");
                            if (lstype[ii] != "Ktor") array += "Ibs,";
                            else array += "ftIbs,";
                            if (int.Parse((directory[ii][0] * 10).ToString()) - 1 > -1) array += keyValuePairs[path[ii]][int.Parse((directory[ii][0] * 10).ToString()) - 1][0].ToString("f2");
                            else array += XYmin[1].ToString("f2");
                            if (lstype[ii] != "Ktor") array += "in";
                            else array += "°";
                            array += " to ";
                            array += "(";
                            array += directory[ii][1] * 100;
                            array += "%)";
                            array += keyValuePairs[path[ii]][int.Parse((directory[ii][1] * 10).ToString()) - 1][1].ToString("f2");
                            if (lstype[ii] != "Ktor") array += "Ibs,";
                            else array += "ftIbs,";
                            array += keyValuePairs[path[ii]][int.Parse((directory[ii][1] * 10).ToString()) - 1][0].ToString("f2");
                            if (lstype[ii] != "Ktor") array += "in";
                            else array += "°";
                            sheet.GetRow(32).GetCell(0).SetCellValue(array);

                            switch (lstype[ii])
                            {
                                case "KLat":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("in");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("Ibs");
                                    break;
                                case "KLong":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("in");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("Ibs");
                                    break;
                                case "Ktor":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("°");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("ftIbs");
                                    break;
                                case "Kv":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("in");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("Ibs");
                                    break;
                                default:

                                    break;
                            }
                            int jump = 0;
                            for (int aa = 0; aa < xdri[ii].Count - 1; aa++)
                            {
                                if (ii < xdri[ii].Count - 10)
                                {
                                    for (int i = 1; i < 10; i++)
                                    {
                                        if (xdri[ii][aa] == xdri[ii][aa + i])
                                        {
                                            jump += 1;
                                        }
                                    }
                                }
                                if (jump == 9) break;
                                else jump = 0;
                                if (4 + aa < 34)
                                {
                                    sheet.GetRow(4 + aa).CreateCell(12).SetCellValue(xdri[ii][aa]);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(ydri[ii][aa]);
                                }                          
                                else if (4 + aa == 34)
                                {
                                    sheet.CreateRow(4 + aa).CreateCell(12).SetCellValue(xdri[ii][aa]);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(ydri[ii][aa]);
                                }
                                else if (4 + aa == 35)
                                {                               
                                    sheet.GetRow(4 + aa).CreateCell(12).SetCellValue(xdri[ii][aa]);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(ydri[ii][aa]);
                                }                        
                                else
                                {
                                    if (aa < 65530)
                                    {
                                        sheet.CreateRow(4 + aa).CreateCell(12).SetCellValue(xdri[ii][aa]);
                                        sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(ydri[ii][aa]);
                                    }
                                }

                                if (4 + aa == 33)
                                {
                                    string Remarks = dt[ii].Rows[0]["Remarks"].ToString();
                                    sheet.GetRow(4 + aa).GetCell(1).SetCellValue(Remarks);
                                }
                            }

                            break;
                        case "SI":

                            string array1 = sheet.GetRow(32).GetCell(0).StringCellValue;
                            array1 += " between ";
                            array1 += "( ";
                            array1 += (directory[ii][0] * 100).ToString();
                            array1 += "% ) ";
                            if (int.Parse((directory[ii][0] * 10).ToString()) - 1 > -1) array1 += keyValuePairs[path[ii]][int.Parse((directory[ii][0] * 10).ToString()) - 1][1].ToString("f2");
                            else array1 += XYmin[0].ToString("f2");
                            if (lstype[ii] != "Ktor") array1 += "n,";
                            else array1 += "N.M,";
                            if (int.Parse((directory[ii][0] * 10).ToString()) - 1 > -1) array1 += keyValuePairs[path[ii]][int.Parse((directory[ii][0] * 10).ToString()) - 1][0].ToString("f2");
                            else array1 += XYmin[1].ToString("f2");
                            if (lstype[ii] != "Ktor") array1 += "mm";
                            else array1 += "°";
                            array1 += " to ";
                            array1 += "(";
                            array1 += directory[ii][1] * 100;
                            array1 += "%)";
                            array1 += keyValuePairs[path[ii]][int.Parse((directory[ii][1] * 10).ToString()) - 1][1].ToString("f2");
                            if (lstype[ii] != "Ktor") array1 += "n,";
                            else array1 += "N.M,";
                            array1 += keyValuePairs[path[ii]][int.Parse((directory[ii][1] * 10).ToString()) - 1][0].ToString("f2");
                            if (lstype[ii] != "Ktor") array1 += "mm";
                            else array1 += "°";
                            sheet.GetRow(32).GetCell(0).SetCellValue(array1);

                            switch (lstype[ii])
                            {
                                case "KLat":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("mm");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("n");
                                    break;
                                case "KLong":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("mm");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("n");
                                    break;
                                case "Ktor":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("°");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("N.M");
                                    break;
                                case "Kv":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("mm");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("n");
                                    break;
                                default:

                                    break;
                            }
                            double Xvalue = 0.0, Yvalue = 0.0;
                            int jumpsi = 0;
                            for (int aa = 0; aa < xdri[ii].Count - 1; aa++)
                            {
                                if (ii < xdri[ii].Count - 10)
                                {
                                    for (int i = 1; i < 10; i++)
                                    {
                                        if (xdri[ii][aa] == xdri[ii][aa + i])
                                        {
                                            jumpsi += 1;
                                        }
                                    }
                                }
                                if (jumpsi == 9) break;
                                else jumpsi = 0;
                                if (lstype[ii] != "Ktor")
                                {
                                    Xvalue = xdri[ii][aa].EnglishToSi(Units.inch);
                                    Yvalue = ydri[ii][aa].EnglishToSi(Units.n);
                                }
                                else
                                {
                                    Xvalue = xdri[ii][aa];
                                    Yvalue = ydri[ii][aa].EnglishToSi("Mz");
                                }

                                if (4 + aa < 34)
                                {
                                    sheet.GetRow(4 + aa).CreateCell(12).SetCellValue(Xvalue);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(Yvalue);
                                }
                                else if (4 + aa == 34)
                                {
                                    sheet.CreateRow(4 + aa).CreateCell(12).SetCellValue(Xvalue);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(Yvalue);
                                }
                                else if (4 + aa == 35)
                                {
                                    
                                    sheet.GetRow(4 + aa).CreateCell(12).SetCellValue(Xvalue);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(Yvalue);
                                }                   
                                else
                                {
                                    if (aa < 65530)
                                    {
                                        sheet.CreateRow(4 + aa).CreateCell(12).SetCellValue(Xvalue);
                                        sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(Yvalue);
                                    }
                                }

                                if (4 + aa == 33)
                                {
                                    string Remarks = dt[ii].Rows[0]["Remarks"].ToString();
                                    sheet.GetRow(4 + aa).GetCell(1).SetCellValue(Remarks);
                                }
                            }


                            Temperature = Temperature.EnglishToSi(Units.c);
                            TireInflation = TireInflation.EnglishToSi(Units.kpa);
                            TravelSpeedofPxy = TravelSpeedofPxy.EnglishToSi(Units.inch);
                            RadialLoad = RadialLoad.EnglishToSi(Units.n);
                            LoadSpeedofPz = LoadSpeedofPz.EnglishToSi(Units.inch);
                            break;
                        case "MKS":

                            string array2 = sheet.GetRow(32).GetCell(0).StringCellValue;
                            array2 += " between ";
                            array2 += "( ";
                            array2 += (directory[ii][0] * 100).ToString();
                            array2 += "% ) ";
                            if (int.Parse((directory[ii][0] * 10).ToString()) - 1 > -1) array2 += keyValuePairs[path[ii]][int.Parse((directory[ii][0] * 10).ToString()) - 1][1].ToString("f2");
                            else array2 += XYmin[0].ToString("f2");
                            if (lstype[ii] != "Ktor") array2 += "kg,";
                            else array2 += "kgm,";
                            if (int.Parse((directory[ii][0] * 10).ToString()) - 1 > -1) array2 += keyValuePairs[path[ii]][int.Parse((directory[ii][0] * 10).ToString()) - 1][0].ToString("f2");
                            else array2 += XYmin[1].ToString("f2");
                            if (lstype[ii] != "Ktor") array2 += "mm";
                            else array2 += "°";
                            array2 += " to ";
                            array2 += "(";
                            array2 += directory[ii][1] * 100;
                            array2 += "%)";
                            array2 += keyValuePairs[path[ii]][int.Parse((directory[ii][1] * 10).ToString()) - 1][1].ToString("f2");
                            if (lstype[ii] != "Ktor") array2 += "kg,";
                            else array2 += "kgm,";
                            array2 += keyValuePairs[path[ii]][int.Parse((directory[ii][1] * 10).ToString()) - 1][0].ToString("f2");
                            if (lstype[ii] != "Ktor") array2 += "mm";
                            else array2 += "°";
                            sheet.GetRow(32).GetCell(0).SetCellValue(array2);

                            switch (lstype[ii])
                            {
                                case "KLat":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("mm");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("kg");
                                    break;
                                case "KLong":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("mm");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("kg");
                                    break;
                                case "Ktor":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("°");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("kgM");
                                    break;
                                case "Kv":
                                    sheet.GetRow(3).CreateCell(12).SetCellValue("mm");
                                    sheet.GetRow(3).CreateCell(13).SetCellValue("kg");
                                    break;
                                default:
                                    break;
                            }
                            double XvalueMKS = 0.0, YvalueMKS = 0.0;
                            int jumpsmks = 0;
                            for (int aa = 0; aa < xdri[ii].Count - 1; aa++)
                            {
                                if (ii < xdri[ii].Count - 10)
                                {
                                    for (int i = 1; i < 10; i++)
                                    {
                                        if (xdri[ii][aa] == xdri[ii][aa + i])
                                        {
                                            jumpsmks += 1;
                                        }
                                    }
                                }
                                if (jumpsmks == 9) break;
                                else jumpsmks = 0;
                                if (lstype[ii] != "Ktor")
                                {
                                    XvalueMKS = xdri[ii][aa].EnglishToSi(Units.inch);
                                    YvalueMKS = ydri[ii][aa].EnglishToSi(Units.kg);
                                }
                                else
                                {
                                    XvalueMKS = xdri[ii][aa];
                                    YvalueMKS = ydri[ii][aa].EnglishToMKS("Mz");
                                }

                                if (4 + aa < 34)
                                {
                                    sheet.GetRow(4 + aa).CreateCell(12).SetCellValue(XvalueMKS);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(YvalueMKS);
                                }
                                else if (4 + aa == 34)
                                {
                                    sheet.CreateRow(4 + aa).CreateCell(12).SetCellValue(XvalueMKS);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(YvalueMKS);
                                }
                                else if (4 + aa == 35)
                                {
                                    sheet.GetRow(4 + aa).CreateCell(12).SetCellValue(XvalueMKS);
                                    sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(YvalueMKS);
                                }                         
                                else
                                {
                                    if (aa < 65530)
                                    {
                                        sheet.CreateRow(4 + aa).CreateCell(12).SetCellValue(XvalueMKS);
                                        sheet.GetRow(4 + aa).CreateCell(13).SetCellValue(YvalueMKS);
                                    }
                                }

                                if (4 + aa == 33)
                                {
                                    string Remarks = dt[ii].Rows[0]["Remarks"].ToString();
                                    sheet.GetRow(4 + aa).GetCell(1).SetCellValue(Remarks);
                                }
                            }

                            Temperature = Temperature.EnglishToSi(Units.c);
                            TireInflation = TireInflation.EnglishToSi(Units.kpa);
                            TravelSpeedofPxy = TravelSpeedofPxy.EnglishToSi(Units.inch);
                            RadialLoad = RadialLoad.EnglishToSi(Units.kg);
                            LoadSpeedofPz = LoadSpeedofPz.EnglishToSi(Units.inch);
                            break;
                        default:
                            break;
                    }

                    //Temperature
                    str[22] = Temperature.ToString("f2");
                    //Test Pressure
                    str[26] = TireInflation.ToString("f2");
                    //TravelSpeedofPxy
                    if (lstype[ii] != "Kv")
                    {
                        str[30] = TravelSpeedofPxy.ToString("f2");
                    }
                    else
                    {
                        str[30] = "NA";
                    }
                    //RadialLoad
                    str[32] = RadialLoad.ToString("f2");
                    //LoadSpeedofPz
                    str[33] = LoadSpeedofPz.ToString("f2");

                    //以下是百分点和斜率
                    str[34] = keyValuePairs[path[ii]][0][1].ToString("f2");
                    str[35] = keyValuePairs[path[ii]][0][0].ToString("f2");
                    str[36] = keyValuePairs[path[ii]][1][1].ToString("f2");
                    str[37] = keyValuePairs[path[ii]][1][0].ToString("f2");
                    str[38] = keyValuePairs[path[ii]][2][1].ToString("f2");
                    str[39] = keyValuePairs[path[ii]][2][0].ToString("f2");
                    str[40] = keyValuePairs[path[ii]][3][1].ToString("f2");
                    str[41] = keyValuePairs[path[ii]][3][0].ToString("f2");
                    str[42] = keyValuePairs[path[ii]][4][1].ToString("f2");
                    str[43] = keyValuePairs[path[ii]][4][0].ToString("f2");
                    str[44] = keyValuePairs[path[ii]][5][1].ToString("f2");
                    str[45] = keyValuePairs[path[ii]][5][0].ToString("f2");
                    str[46] = keyValuePairs[path[ii]][6][1].ToString("f2");
                    str[47] = keyValuePairs[path[ii]][6][0].ToString("f2");
                    str[48] = keyValuePairs[path[ii]][7][1].ToString("f2");
                    str[49] = keyValuePairs[path[ii]][7][0].ToString("f2");
                    str[50] = keyValuePairs[path[ii]][8][1].ToString("f2");
                    str[51] = keyValuePairs[path[ii]][8][0].ToString("f2");
                    str[52] = keyValuePairs[path[ii]][9][1].ToString("f2");
                    str[53] = keyValuePairs[path[ii]][9][0].ToString("f2");
                    //斜率
                    str[54] = slopes[path[ii]].ToString("f2");

                    //循环表格赋值 
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i < 35)
                        {
                            sheet.GetRow(excelx[i]).GetCell(excely[i]).SetCellValue(str[i]);
                        }
                        else
                        {
                            double value;
                            double.TryParse(str[i].ToString(), out value);
                            sheet.GetRow(excelx[i]).GetCell(excely[i]).SetCellFormula(value.ToString("f4") + "*1");
                            sheet.ForceFormulaRecalculation = true;
                        }
                    }
                }
                workb.Write(fileStream);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                workb.Write(fileStream);
                fileStream.Close();
                fs.Close();

            }

            #endregion
        }

        /// <summary>
        /// 同一个excel文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyValues"></param>
        /// <param name="slope"></param>
        /// <param name="unit"></param>
        public static void Excel(DataTable[] dt, Dictionary<string, List<double[]>> keyValuePairs, Dictionary<string, double> slopes, DialogResult result, SaveFileDialog sfd, string[] path, string unit, List<string> lstype, string type)
        {
            ////全部的sheet
            //Worksheet[] worksheets = new Worksheet[4];
            ////加载Excel模板文件
            //Workbook workbook = new Workbook();
            ////加载Excel新文件
            //Workbook worknew = new Workbook();
            //模板文件路径
            //_path = "Excel\\MRFLDReport_GB.xls";
            ////根据路径获取excel文件
            //workbook.LoadFromFile("Excel\\Excel.xls");
            ////获取模板的第一个工作表
            //Worksheet sheet1 = workbook.Worksheets[0];
            IWorkbook workb = null;
            //string[] sheetname = new string[] { "Radial", "Longitudinal", "Lateral", "Torsional" };

            switch (unit)
            {
                case "English":
                    _path = "Excel\\MRFLDReport_GB_En.xls";
                    break;
                case "SI":
                    _path = "Excel\\MRFLDReport_GB_SI.xls";
                    break;
                case "MKS":
                    _path = "Excel\\MRFLDReport_GB_MKS.xls";
                    break;
            }

            #region 开始保存

            if (result == DialogResult.OK)
            {
                //模板文件流
                FileStream fs = File.OpenRead(_path);

                FileStream fileStream = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                try
                {
                    //判断excel文件格式
                    if (_path.IndexOf(".xlsx") > 0) // 2007版本
                        workb = new XSSFWorkbook(fs);
                    else if (_path.IndexOf(".xls") > 0) // 2003版本
                        workb = new HSSFWorkbook(fs);

                    #region 预赋值

                    for (int ii = 0; ii < lstype.Count; ii++)
                    {
                        // 获取此文件第一个Sheet页
                        int num = 0;
                        switch (lstype[ii])
                        {
                            case "KLat":
                                num = 2;
                                break;
                            case "KLong":
                                num = 1;
                                break;
                            case "Ktor":
                                num = 3;
                                break;
                            case "Kv":
                                num = 0;
                                break;
                            default:

                                break;
                        }

                        var sheet = workb.GetSheetAt(num);

                        #region 刷新原先的excel单位

                        #endregion

                        int[] excelx = new int[] { 4, 7, 6, 6, 5, 9, 10, 9, 10, 11, 4, 9, 10, 11, 13, 11, 14, 5, 30, 9, 10, 15, 11, 13, 17, 14, 16, 16, 20, 20, 21, 21, 22, 22, 23, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 29 };
                        int[] excely = new int[] { 5, 1, 5, 1, 5, 1, 1, 3, 3, 1, 1, 5, 5, 5, 6, 3, 6, 1, 2, 7, 7, 2, 7, 2, 2, 2, 6, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
                        string[] str = new string[48];
                        //以下需要根据单位进行转换。
                        double TireLoad, TireRadius, TireDia, TireWeight, Temperature, TargetLoad, TargetInflation, VectorRatePz, VectorRatePxy;

                        str[0] = dt[ii].Rows[0]["TestNo"].ToString();
                        str[1] = dt[ii].Rows[0]["Operators"].ToString();
                        str[2] = dt[ii].Rows[0]["TestPurpose"].ToString();
                        str[3] = dt[ii].Rows[0]["Requestor"].ToString();
                        str[4] = dt[ii].Rows[0]["TestDate"].ToString();
                        str[5] = dt[ii].Rows[0]["TireSize"].ToString();
                        str[6] = dt[ii].Rows[0]["TireSN"].ToString();
                        str[7] = dt[ii].Rows[0]["LoadIndex"].ToString();
                        str[8] = dt[ii].Rows[0]["SpeedLevel"].ToString();
                        str[9] = dt[ii].Rows[0]["IsTubeLess"].ToString();
                        str[10] = dt[ii].Rows[0]["Manufacture"].ToString();
                        str[11] = dt[ii].Rows[0]["Brand"].ToString();
                        str[12] = dt[ii].Rows[0]["Pattern"].ToString();
                        str[13] = dt[ii].Rows[0]["TirePly"].ToString();
                        str[14] = dt[ii].Rows[0]["Platforms"].ToString();
                        str[15] = dt[ii].Rows[0]["TireWeeks"].ToString();
                        str[16] = dt[ii].Rows[0]["wheelSize"].ToString();
                        str[17] = dt[ii].Rows[0]["RequestDep"].ToString();
                        str[18] = slopes[path[ii]].ToString("f4");

                        try
                        {
                            TireLoad = double.Parse(dt[ii].Rows[0]["TireLoad"].ToString());
                            TireRadius = double.Parse(dt[ii].Rows[0]["TireRadius"].ToString());
                            TireDia = double.Parse(dt[ii].Rows[0]["TireDia"].ToString());
                            TireWeight = double.Parse(dt[ii].Rows[0]["TireWeight"].ToString());
                            Temperature = double.Parse(dt[ii].Rows[0]["Temperature"].ToString());
                            TargetLoad = double.Parse(dt[ii].Rows[0]["TargetLoad"].ToString());
                            TargetInflation = double.Parse(dt[ii].Rows[0]["TargetInflation"].ToString());
                            VectorRatePz = double.Parse(dt[ii].Rows[0]["VectorRatePz"].ToString());
                            VectorRatePxy = double.Parse(dt[ii].Rows[0]["VectorRatePxy"].ToString());

                            switch (unit)
                            {
                                case "English":

                                    break;
                                case "SI":
                                    TireLoad = TireLoad.EnglishToSi(Units.n);
                                    TireRadius = TireRadius.EnglishToSi(Units.kpa);
                                    TireWeight = TireWeight.EnglishToSi(Units.n);
                                    TireDia = TireDia.EnglishToSi(Units.inch);
                                    Temperature = Temperature.EnglishToSi(Units.c);
                                    TargetLoad = TargetLoad.EnglishToSi(Units.n);
                                    TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                                    VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                                    VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);
                                    break;
                                case "MKS":
                                    TireLoad = TireLoad.EnglishToSi(Units.kg);
                                    TireRadius = TireRadius.EnglishToSi(Units.kpa);
                                    TireWeight = TireWeight.EnglishToSi(Units.kg);
                                    TireDia = TireDia.EnglishToSi(Units.inch);
                                    Temperature = Temperature.EnglishToSi(Units.c);
                                    TargetLoad = TargetLoad.EnglishToSi(Units.kg);
                                    TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                                    VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                                    VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);
                                    break;
                                default:
                                    break;
                            }
                            str[19] = TireLoad.ToString("f2");
                            str[20] = TireRadius.ToString("f2");
                            str[21] = TireDia.ToString("f2");
                            str[22] = TireWeight.ToString("f2");
                            str[23] = Temperature.ToString("f2");
                            str[24] = TargetLoad.ToString("f2");
                            str[25] = TargetInflation.ToString("f2");
                            str[26] = VectorRatePz.ToString("f2");
                            str[27] = VectorRatePxy.ToString("f2");

                        }
                        catch
                        {
                            str[19] = dt[ii].Rows[0]["TireLoad"].ToString();///
                            str[20] = dt[ii].Rows[0]["TireRadius"].ToString();///
                            str[21] = dt[ii].Rows[0]["TireDia"].ToString();///
                            str[22] = dt[ii].Rows[0]["TireWeight"].ToString();///
                            str[23] = dt[ii].Rows[0]["Temperature"].ToString();///
                            str[24] = dt[ii].Rows[0]["TargetLoad"].ToString();///
                            str[25] = dt[ii].Rows[0]["TargetInflation"].ToString();///
                            str[26] = dt[ii].Rows[0]["VectorRatePz"].ToString();///
                            str[27] = dt[ii].Rows[0]["VectorRatePxy"].ToString();///

                        }

                        str[28] = keyValuePairs[path[ii]][0][1].ToString("f4");
                        str[29] = keyValuePairs[path[ii]][0][0].ToString("f4");
                        str[30] = keyValuePairs[path[ii]][1][1].ToString("f4");
                        str[31] = keyValuePairs[path[ii]][1][0].ToString("f4");
                        str[32] = keyValuePairs[path[ii]][2][1].ToString("f4");
                        str[33] = keyValuePairs[path[ii]][2][0].ToString("f4");
                        str[34] = keyValuePairs[path[ii]][3][1].ToString("f4");
                        str[35] = keyValuePairs[path[ii]][3][0].ToString("f4");
                        str[36] = keyValuePairs[path[ii]][4][1].ToString("f4");
                        str[37] = keyValuePairs[path[ii]][4][0].ToString("f4");
                        str[38] = keyValuePairs[path[ii]][5][1].ToString("f4");
                        str[39] = keyValuePairs[path[ii]][5][0].ToString("f4");
                        str[40] = keyValuePairs[path[ii]][6][1].ToString("f4");
                        str[41] = keyValuePairs[path[ii]][6][0].ToString("f4");
                        str[42] = keyValuePairs[path[ii]][7][1].ToString("f4");
                        str[43] = keyValuePairs[path[ii]][7][0].ToString("f4");
                        str[44] = keyValuePairs[path[ii]][8][1].ToString("f4");
                        str[45] = keyValuePairs[path[ii]][8][0].ToString("f4");
                        str[46] = keyValuePairs[path[ii]][9][1].ToString("f4");
                        str[47] = keyValuePairs[path[ii]][9][0].ToString("f4");

                        //循环表格赋值 
                        for (int i = 0; i < 48; i++)
                        {
                            if (i < 27)
                            {
                                sheet.GetRow(excelx[i]).GetCell(excely[i]).SetCellValue(str[i]);
                            }
                            else
                            {
                                double value = double.Parse(double.Parse(str[i]).ToString("f4"));
                                sheet.GetRow(excelx[i]).GetCell(excely[i]).SetCellFormula(str[i] + "*1");
                                sheet.ForceFormulaRecalculation = true;
                            }
                        }
                    }
                    workb.Write(fileStream);
                    #endregion
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    fileStream.Close();
                    fs.Close();
                    //File.Delete("Excel\\Excel2.xls");
                }
            }

            #endregion
        }

        /// <summary>
        /// 保存slr
        /// </summary>
        /// <param name="imagePath">保存的文件路径</param>
        /// <param name="type">类型</param>
        /// <param name="unit">单位</param>
        /// <param name="x">x轴集合</param>
        /// <param name="y">y轴集合</param>
        public static void Excel(string imagePath, string type, string unit, List<double> x, List<double> y, DataTable dt)
        {
            //模板文件路径
            _path = "Excel\\Excel_SLR";
            //初始获取的模板
            int sheetnum = 0;
            //加载不同的模板
            switch (unit)
            {
                case "SI":
                    _path += "_SI.xls";
                    break;
                case "MKS":
                    _path += "_MKS.xls";
                    break;
                default:
                    _path += "_En.xls";
                    break;
            }
            //删除页集合
            int[] sheetdele = new int[3];
            string yname = "";
            //加载不同页
            switch (type)
            {
                case "KLat":
                    sheetnum = 2;
                    sheetdele = new int[] { 0, 0, 1 };
                    yname = "Fy";
                    break;
                case "KLong":
                    sheetnum = 1;
                    sheetdele = new int[] { 0, 1, 1 };
                    yname = "Fx";
                    break;
                case "Ktor":
                    sheetnum = 3;
                    sheetdele = new int[] { 0, 0, 0 };
                    yname = "Mz";
                    break;
                case "Kv":
                    sheetnum = 0;
                    sheetdele = new int[] { 1, 1, 1 };
                    yname = "Fz";
                    break;
            }

            #region 开始保存

            //模板文件流
            FileStream fs = File.OpenRead(_path);
            //另存文件流
            FileStream fileStream = new FileStream(imagePath, FileMode.Create);

            try
            {
                IWorkbook workbook = null;
                //判断excel文件格式
                if (_path.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (_path.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                //删除未用页面
                for (int ii = 0; ii < 3; ii++)
                {
                    workbook.RemoveSheetAt(sheetdele[ii]);
                }

                //获取此文件第一个slr标准页
                var sheetslr = workbook.GetSheetAt(0);
                var sheetsraw = workbook.GetSheetAt(1);

                for (int ii = 0; ii < x.Count; ii++)
                {
                    switch (unit)
                    {
                        case "SI":
                            y[ii] = y[ii].EnglishToSi(yname);
                            break;
                        case "MSK":
                            y[ii] = y[ii].EnglishToMKS(yname);
                            break;
                    }
                    sheetsraw.CreateRow(ii).CreateCell(0).SetCellValue(x[ii]);
                    sheetsraw.GetRow(ii).CreateCell(1).SetCellValue(y[ii]);
                }

                //以下需要根据单位进行转换。
                double TireLoad, TireRadius, TireDia, TireWeight, Temperature, TargetLoad, TargetInflation, VectorRatePz, VectorRatePxy;

                sheetslr.GetRow(4).GetCell(5).SetCellValue(dt.Rows[0]["TestNo"].ToString());
                sheetslr.GetRow(7).GetCell(1).SetCellValue(dt.Rows[0]["Operators"].ToString());
                sheetslr.GetRow(6).GetCell(5).SetCellValue(dt.Rows[0]["TestPurpose"].ToString());
                sheetslr.GetRow(6).GetCell(1).SetCellValue(dt.Rows[0]["Requestor"].ToString());
                sheetslr.GetRow(5).GetCell(5).SetCellValue(dt.Rows[0]["TestDate"].ToString());
                sheetslr.GetRow(9).GetCell(1).SetCellValue(dt.Rows[0]["TireSize"].ToString());
                sheetslr.GetRow(10).GetCell(1).SetCellValue(dt.Rows[0]["TireSN"].ToString());
                sheetslr.GetRow(9).GetCell(3).SetCellValue(dt.Rows[0]["LoadIndex"].ToString());
                sheetslr.GetRow(10).GetCell(3).SetCellValue(dt.Rows[0]["SpeedLevel"].ToString());
                sheetslr.GetRow(11).GetCell(1).SetCellValue(dt.Rows[0]["IsTubeLess"].ToString());
                sheetslr.GetRow(4).GetCell(1).SetCellValue(dt.Rows[0]["Manufacture"].ToString());
                sheetslr.GetRow(9).GetCell(5).SetCellValue(dt.Rows[0]["Brand"].ToString());
                sheetslr.GetRow(10).GetCell(5).SetCellValue(dt.Rows[0]["Pattern"].ToString());
                sheetslr.GetRow(11).GetCell(5).SetCellValue(dt.Rows[0]["TirePly"].ToString());
                sheetslr.GetRow(13).GetCell(6).SetCellValue(dt.Rows[0]["Platforms"].ToString());
                sheetslr.GetRow(11).GetCell(3).SetCellValue(dt.Rows[0]["TireWeeks"].ToString());
                sheetslr.GetRow(14).GetCell(6).SetCellValue(dt.Rows[0]["wheelSize"].ToString());
                sheetslr.GetRow(5).GetCell(1).SetCellValue(dt.Rows[0]["RequestDep"].ToString());

                try
                {
                    TireLoad = double.Parse(dt.Rows[0]["TireLoad"].ToString());
                    TireRadius = double.Parse(dt.Rows[0]["TireRadius"].ToString());
                    TireDia = double.Parse(dt.Rows[0]["TireDia"].ToString());
                    TireWeight = double.Parse(dt.Rows[0]["TireWeight"].ToString());
                    Temperature = double.Parse(dt.Rows[0]["Temperature"].ToString());
                    TargetLoad = double.Parse(dt.Rows[0]["TargetLoad"].ToString());
                    TargetInflation = double.Parse(dt.Rows[0]["TargetInflation"].ToString());
                    VectorRatePz = double.Parse(dt.Rows[0]["VectorRatePz"].ToString());
                    VectorRatePxy = double.Parse(dt.Rows[0]["VectorRatePxy"].ToString());

                    switch (unit)
                    {
                        case "English":
                            break;
                        case "SI":
                            TireDia = TireDia.EnglishToSi(Units.inch);
                            Temperature = Temperature.EnglishToSi(Units.c);
                            TargetLoad = TargetLoad.EnglishToSi(Units.n);
                            TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                            VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                            VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);
                            break;
                        case "MKS":
                            TireDia = TireDia.EnglishToSi(Units.inch);
                            Temperature = Temperature.EnglishToSi(Units.c);
                            TargetLoad = TargetLoad.EnglishToSi(Units.n);
                            TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                            VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                            VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);
                            break;
                    }

                    sheetslr.GetRow(9).GetCell(7).SetCellValue(TireLoad);
                    sheetslr.GetRow(10).GetCell(7).SetCellValue(TireRadius);
                    sheetslr.GetRow(15).GetCell(2).SetCellValue(TireDia);
                    sheetslr.GetRow(11).GetCell(7).SetCellValue(TireWeight);
                    sheetslr.GetRow(13).GetCell(2).SetCellValue(Temperature);
                    sheetslr.GetRow(17).GetCell(2).SetCellValue(TargetLoad);
                    sheetslr.GetRow(14).GetCell(2).SetCellValue(TargetInflation);
                    sheetslr.GetRow(16).GetCell(6).SetCellValue(VectorRatePz);
                    sheetslr.GetRow(16).GetCell(2).SetCellValue(VectorRatePxy);

                }
                catch
                {
                    sheetslr.GetRow(9).GetCell(7).SetCellValue(dt.Rows[0]["TireLoad"].ToString());
                    sheetslr.GetRow(10).GetCell(7).SetCellValue(dt.Rows[0]["TireRadius"].ToString());
                    sheetslr.GetRow(15).GetCell(2).SetCellValue(dt.Rows[0]["TireDia"].ToString());
                    sheetslr.GetRow(11).GetCell(7).SetCellValue(dt.Rows[0]["TireWeight"].ToString());
                    sheetslr.GetRow(13).GetCell(2).SetCellValue(dt.Rows[0]["Temperature"].ToString());
                    sheetslr.GetRow(17).GetCell(2).SetCellValue(dt.Rows[0]["TargetLoad"].ToString());
                    sheetslr.GetRow(14).GetCell(2).SetCellValue(dt.Rows[0]["TargetInflation"].ToString());
                    sheetslr.GetRow(16).GetCell(6).SetCellValue(dt.Rows[0]["VectorRatePz"].ToString());
                    sheetslr.GetRow(16).GetCell(2).SetCellValue(dt.Rows[0]["VectorRatePxy"].ToString());

                }

                #endregion

                workbook.Write(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fileStream.Close();
                fs.Close();
            }
            #endregion
        }

        /// <summary>
        ///HASETRI_SLR
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="type"></param>
        /// <param name="unit"></param>
        /// <param name="dt"></param>
        public static void Excel(string imagePath, string unit, Dictionary<string, List<double>> pairs, List<DataTable> lsdt, List<double> maxpz, string[] names)
        {
            #region 字段定义

            //模板文件路径
            _path = "Excel\\SLR report format.xls";
            //xy轴名称
            string x = "Pz", y = "Fz";

            #endregion 

            #region 开始保存

            //模板文件流
            FileStream fs = File.OpenRead(_path);
            //另存文件流
            FileStream fileStream = new FileStream(imagePath, FileMode.Create);

            try
            {
                IWorkbook workbook = null;
                //判断excel文件格式
                if (_path.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (_path.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                //获取此文件第一个slr标准页
                var sheetslr = workbook.GetSheetAt(0);

                #region 复制粘贴报告

                for (int ii = 0; ii < lsdt.Count - 1; ii++)
                {
                    int startnum = ii * 8;
                    CopyRow(sheetslr, 10 + startnum, 2, 8);
                }

                #endregion

                #region 赋值

                double Temperature, TargetLoad, TargetInflation, VectorRatePz, slr, TireDia, MaxIP, RatedLoad, TireWeight, sw;

                for (int ii = 0; ii < lsdt.Count; ii++)
                {
                    //位置更新
                    int startnum = ii * 8 + 2;
                    //HTAC NO.
                    sheetslr.GetRow(startnum).GetCell(2).SetCellValue(lsdt[ii].Rows[0]["Customer"].ToString());
                    //Test Date
                    sheetslr.GetRow(startnum).GetCell(10).SetCellValue(lsdt[ii].Rows[0]["TestDate"].ToString());
                    //Test Procedure
                    sheetslr.GetRow(startnum + 1).GetCell(10).SetCellValue(lsdt[ii].Rows[0]["TestStandard"].ToString());
                    sheetslr.GetRow(startnum + 1).GetCell(2).SetCellValue(lsdt[ii].Rows[0]["TestStandard"].ToString());
                    //Tire Size
                    sheetslr.GetRow(startnum + 3).GetCell(0).SetCellValue(lsdt[ii].Rows[0]["TireSize"].ToString());
                    //Brand
                    sheetslr.GetRow(startnum + 3).GetCell(2).SetCellValue(lsdt[ii].Rows[0]["Brand"].ToString());
                    //Product ID
                    sheetslr.GetRow(startnum + 3).GetCell(4).SetCellValue(lsdt[ii].Rows[0]["TireSN"].ToString());
                    //TireDia
                    TireDia = double.Parse(lsdt[ii].Rows[0]["TireDia"].ToString());
                    TireDia = TireDia.EnglishToSi(Units.inch);
                    sheetslr.GetRow(startnum + 3).GetCell(6).SetCellValue(TireDia);
                    //SW (mm)
                    double.TryParse(lsdt[ii].Rows[0]["TirePly"].ToString(), out sw);
                    sheetslr.GetRow(startnum + 3).GetCell(8).SetCellValue(sw);
                    //Temperature
                    Temperature = double.Parse(lsdt[ii].Rows[0]["Temperature"].ToString());
                    Temperature = Temperature.EnglishToSi(Units.c);
                    sheetslr.GetRow(startnum + 3).GetCell(10).SetCellValue(Temperature);
                    //Tire Weight (kg)
                    TireWeight = double.Parse(lsdt[ii].Rows[0]["TireWeight"].ToString());
                    TireWeight = TireWeight.EnglishToSi(Units.kg);
                    sheetslr.GetRow(startnum + 5).GetCell(0).SetCellValue(TireWeight);
                    //Rim Size
                    sheetslr.GetRow(startnum + 5).GetCell(2).SetCellValue(lsdt[ii].Rows[0]["wheelSize"].ToString());
                    //Max IP (psi)
                    MaxIP = double.Parse(lsdt[ii].Rows[0]["TireInflation"].ToString());
                    MaxIP = MaxIP.EnglishToSi(Units.kpa);
                    //MaxIP = MaxIP.ToString("f2");
                    sheetslr.GetRow(startnum + 5).GetCell(3).SetCellValue(MaxIP.ToString("f2"));
                    //Rated Load (kg)
                    RatedLoad = double.Parse(lsdt[ii].Rows[0]["TireLoad"].ToString());
                    RatedLoad = RatedLoad.EnglishToSi(Units.kg);
                    sheetslr.GetRow(startnum + 5).GetCell(4).SetCellValue(RatedLoad);
                    //Test Load (kg)
                    TargetLoad = double.Parse(lsdt[ii].Rows[0]["TargetLoad"].ToString());
                    TargetLoad = TargetLoad.EnglishToSi(Units.kg);
                    sheetslr.GetRow(startnum + 5).GetCell(6).SetCellValue(TargetLoad);
                    //Test Inf. (kPa)
                    TargetInflation = double.Parse(lsdt[ii].Rows[0]["TargetInflation"].ToString());
                    TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                    sheetslr.GetRow(startnum + 5).GetCell(8).SetCellValue(TargetInflation);
                    //Humidity %
                    sheetslr.GetRow(startnum + 5).GetCell(10).SetCellValue(lsdt[ii].Rows[0]["Humidity"].ToString());
                    //SLR
                    double TireRadius = double.Parse(lsdt[ii].Rows[0]["TireRadius"].ToString());
                    TireRadius = TireRadius.EnglishToSi(Units.inch);
                    slr = TireRadius - maxpz[ii];
                    sheetslr.GetRow(startnum + 1).GetCell(6).SetCellValue(slr);
                    //Test Speed (mm/sec)
                    VectorRatePz = double.Parse(lsdt[ii].Rows[0]["VectorRatePz"].ToString());
                    VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                    sheetslr.GetRow(startnum + 7).GetCell(0).SetCellValue(VectorRatePz);
                    //Rim Offset (mm)	    
                    sheetslr.GetRow(startnum + 7).GetCell(2).SetCellValue(lsdt[ii].Rows[0]["RimOffset"].ToString());
                    //Radial Stiffness	
                    List<double> ls = pairs[names[ii]];
                    string RadialStiffnessun = sheetslr.GetRow(startnum + 6).GetCell(4).StringCellValue;
                    //string SpringRateBetween = sheetslr.GetRow(startnum + 6).GetCell(7).StringCellValue;
                    //switch (unit)
                    //{
                    //    case "English":
                    //        RadialStiffnessun += "(Ibs/in)";
                    //        sheetslr.GetRow(startnum + 6).GetCell(4).SetCellValue(RadialStiffnessun);
                    //        sheetslr.GetRow(startnum + 7).GetCell(4).SetCellValue(ls[0].ToString("f2"));
                    //        SpringRateBetween += "(Pz:in,Fz:Ibs)";
                    //        sheetslr.GetRow(startnum + 6).GetCell(7).SetCellValue(SpringRateBetween);
                    //        sheetslr.GetRow(startnum + 7).GetCell(7).SetCellValue($"Pz:{ls[1].ToString("f2")}-{ls[2].ToString("f2")};Fz:{ls[3].ToString("f2")}-{ls[4].ToString("f2")}");
                    //        break;
                    //    case "SI":
                    //        RadialStiffnessun += "(n/mm)";
                    //        sheetslr.GetRow(startnum + 6).GetCell(4).SetCellValue(RadialStiffnessun);
                    //        sheetslr.GetRow(startnum + 7).GetCell(4).SetCellValue(ls[0].ToString("f2"));
                    //        SpringRateBetween += "(Pz:mm,Fz:n)";
                    //        sheetslr.GetRow(startnum + 6).GetCell(7).SetCellValue(SpringRateBetween);
                    //        sheetslr.GetRow(startnum + 7).GetCell(7).SetCellValue($"Pz:{ls[1].ToString("f2")}-{ls[2].ToString("f2")};Fz:{ls[3].ToString("f2")}-{ls[4].ToString("f2")}");
                    //        break;
                    //    case "MKS":
                    //        RadialStiffnessun += "(kg/mm)";
                    //        sheetslr.GetRow(startnum + 6).GetCell(4).SetCellValue(RadialStiffnessun);
                    //        sheetslr.GetRow(startnum + 7).GetCell(4).SetCellValue(ls[0].ToString("f2"));
                    //        SpringRateBetween += "(Pz:mm,Fz:kg)";
                    //        sheetslr.GetRow(startnum + 6).GetCell(7).SetCellValue(SpringRateBetween);
                    //        sheetslr.GetRow(startnum + 7).GetCell(7).SetCellValue($"Pz:{ls[1].ToString("f2")}-{ls[2].ToString("f2")};Fz:{ls[3].ToString("f2")}-{ls[4].ToString("f2")}");
                    //        break;
                    //}			                   
                }

                #endregion
                workbook.Write(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fileStream.Close();
                fs.Close();
            }

            #endregion
        }

        /// <summary>
        /// 保存滑移力
        /// </summary>
        /// <param name="imagePath">保存的文件路径</param>
        /// <param name="type">类型</param>
        /// <param name="unit">单位</param>
        /// <param name="x">x轴集合</param>
        /// <param name="y">y轴集合</param>
        public static void Excel(string imagePath, string type, string unit, List<double> x, List<double> y, List<double> slope, DataTable dt)
        {
            //模板文件路径
            _path = "Excel\\Excel_Silp";
            //初始获取的模板
            int sheetnum = 0;
            //加载不同的模板
            switch (unit)
            {
                case "SI":
                    _path += "_SI.xls";
                    break;
                case "MKS":
                    _path += "_MKS.xls";
                    break;
                default:
                    _path += "_En.xls";
                    break;
            }
            //删除页集合
            int sheetdele = 0, sheetdele1 = 0;
            int AzAngle = 0;
            string yname = "";
            //加载不同页
            switch (type)
            {
                case "KLat":
                    sheetdele = 1;
                    sheetdele1 = 1;
                    yname = "Fy";
                    AzAngle = 90;
                    break;
                case "KLong":
                    sheetdele = 0;
                    sheetdele1 = 1;
                    yname = "Mz";
                    AzAngle = 0;
                    break;
                case "Kv":
                    sheetdele1 = 0;
                    sheetdele = 0;
                    yname = "Mz";
                    AzAngle = 0;
                    break;
            }

            #region 开始保存

            //模板文件流
            FileStream fs = File.OpenRead(_path);
            //另存文件流
            FileStream fileStream = new FileStream(imagePath, FileMode.Create);

            try
            {
                IWorkbook workbook = null;
                //判断excel文件格式
                if (_path.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (_path.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                //删除未用页面
                workbook.RemoveSheetAt(sheetdele);
                workbook.RemoveSheetAt(sheetdele1);

                //获取此文件第一个slr标准页
                var sheetslr = workbook.GetSheetAt(0);

                for (int ii = 0; ii < x.Count; ii++)
                {
                    sheetslr.GetRow(22 + ii).GetCell(1).SetCellValue(x[ii]);
                    sheetslr.GetRow(22 + ii).GetCell(0).SetCellValue(y[ii]);
                    if (ii < x.Count - 1)
                    {
                        sheetslr.GetRow(23 + ii).GetCell(2).SetCellValue(slope[ii]);
                    }

                    if (ii > 28) break;
                }

                int[] excelx = new int[] { 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 10, 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18, 19, 19 };
                int[] excely = new int[] { 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 3, 5, 7, 1, 3, 5, 7, 1, 3, 5, 7, 2, 6, 2, 6, 2, 6, 2, 6, 2, 6, 2, 6 };
                string[] str = new string[34];

                //HTAC No.
                str[0] = dt.Rows[0]["Customer"].ToString();
                //Report No.
                str[1] = dt.Rows[0]["TestNo"].ToString();
                //Request Department
                str[2] = dt.Rows[0]["RequestDep"].ToString();
                //TestDate
                str[3] = dt.Rows[0]["TestDate"].ToString();
                //Requestor
                str[4] = dt.Rows[0]["Requestor"].ToString();
                //Test Purpose
                str[5] = dt.Rows[0]["TestPurpose"].ToString();
                //Operator
                str[6] = dt.Rows[0]["Operators"].ToString();
                //Report Export date
                str[7] = DateTime.Now.ToString();
                //Comments 
                str[8] = dt.Rows[0]["Remarks"].ToString();
                //Test Procedure
                str[9] = dt.Rows[0]["TestStandard"].ToString();
                //Tire Size
                str[10] = dt.Rows[0]["TireSize"].ToString();
                //Load Index
                str[11] = dt.Rows[0]["LoadIndex"].ToString();
                //Brand
                str[12] = dt.Rows[0]["Brand"].ToString();
                //Max Load (kg)
                double maxload;
                double.TryParse(dt.Rows[0]["TireLoad"].ToString(), out maxload);
                maxload = maxload.EnglishToSi(Units.kg);
                str[13] = maxload.ToString("f2");
                //Product ID
                str[14] = dt.Rows[0]["TireSN"].ToString();
                //Speed Symbol
                str[15] = dt.Rows[0]["SpeedLevel"].ToString();
                //Pattern
                str[16] = dt.Rows[0]["Pattern"].ToString();
                //Max Inflate Pressure
                double MaxInflatePressure;
                double.TryParse(dt.Rows[0]["TargetInflation"].ToString(), out MaxInflatePressure);
                MaxInflatePressure = MaxInflatePressure.EnglishToSi(Units.kpa);
                str[17] = MaxInflatePressure.ToString("f2");
                //Tube/Tubeless
                str[18] = dt.Rows[0]["IsTubeLess"].ToString();
                //Manufacture Date
                str[19] = dt.Rows[0]["TireDate"].ToString();
                //SW (mm)
                double Sw;
                double.TryParse(dt.Rows[0]["TirePly"].ToString(), out Sw);
                //Sw = Sw.EnglishToSi(Units.inch);
                str[20] = Sw.ToString("f2");
                //Tire Weight
                double TireWeight;
                double.TryParse(dt.Rows[0]["TireWeight"].ToString(), out TireWeight);
                TireWeight = TireWeight.EnglishToSi(Units.kg);
                str[21] = TireWeight.ToString("f2");
                //Platform surface Type
                str[23] = dt.Rows[0]["Platforms"].ToString();
                //Humidity (%)
                str[24] = dt.Rows[0]["Humidity"].ToString();
                //Test Plate Height (mm)
                double TestPlateHeight;
                double.TryParse(dt.Rows[0]["TestPlateHeight"].ToString(), out TestPlateHeight);
                TestPlateHeight = TestPlateHeight.EnglishToSi(Units.inch);
                str[25] = TestPlateHeight.ToString("f2");
                //RIM Size
                str[27] = dt.Rows[0]["wheelSize"].ToString();
                //Tire OD
                double TireOD;
                double.TryParse(dt.Rows[0]["TireDia"].ToString(), out TireOD);
                TireOD = TireOD.EnglishToSi(Units.inch);
                str[28] = TireOD.ToString("f2");
                //RIM Offset (mm)
                double RimOffset;
                double.TryParse(dt.Rows[0]["RimOffset"].ToString(), out RimOffset);
                RimOffset = RimOffset.EnglishToSi(Units.inch);
                str[29] = RimOffset.ToString("f2");
                //Az Angle (deg)
                str[31] = AzAngle.ToString();

                //下面的都需要进行转换
                double Temperature; double.TryParse(dt.Rows[0]["Temperature"].ToString(), out Temperature);
                double TireInflation; double.TryParse(dt.Rows[0]["TireInflation"].ToString(), out TireInflation);
                double TravelSpeedofPxy; double.TryParse(dt.Rows[0]["VectorRatePxy"].ToString(), out TravelSpeedofPxy);
                double RadialLoad; double.TryParse(dt.Rows[0]["TargetLoad"].ToString(), out RadialLoad);
                double LoadSpeedofPz; double.TryParse(dt.Rows[0]["VectorRatePz"].ToString(), out LoadSpeedofPz);

                switch (unit)
                {
                    case "English":

                        break;
                    case "SI":
                        Temperature = Temperature.EnglishToSi(Units.c);
                        TireInflation = TireInflation.EnglishToSi(Units.kpa);
                        TravelSpeedofPxy = TravelSpeedofPxy.EnglishToSi(Units.inch);
                        RadialLoad = RadialLoad.EnglishToSi(Units.n);
                        LoadSpeedofPz = LoadSpeedofPz.EnglishToSi(Units.inch);
                        break;
                    case "MKS":
                        Temperature = Temperature.EnglishToSi(Units.c);
                        TireInflation = TireInflation.EnglishToSi(Units.kpa);
                        TravelSpeedofPxy = TravelSpeedofPxy.EnglishToSi(Units.inch);
                        RadialLoad = RadialLoad.EnglishToSi(Units.kg);
                        LoadSpeedofPz = LoadSpeedofPz.EnglishToSi(Units.inch);
                        break;
                    default:
                        break;
                }

                //Temperature
                str[22] = Temperature.ToString("f2");
                //Test Pressure
                str[26] = TireInflation.ToString("f2");
                //TravelSpeedofPxy
                str[30] = TravelSpeedofPxy.ToString("f2");
                //RadialLoad
                str[32] = RadialLoad.ToString("f2");
                //LoadSpeedofPz
                str[33] = LoadSpeedofPz.ToString("f2");

                //循环表格赋值 
                for (int i = 0; i < str.Length; i++)
                {
                    sheetslr.GetRow(excelx[i]).GetCell(excely[i]).SetCellValue(str[i]);
                }


                workbook.Write(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fileStream.Close();
                fs.Close();
            }
            #endregion
        }

        /// <summary>
        /// 复制行格式并插入指定行数
        /// </summary>
        /// <param name="sheet">当前sheet</param>
        /// <param name="startRowIndex">起始行位置</param>
        /// <param name="sourceRowIndex">模板行位置</param>
        /// <param name="insertCount">插入行数</param>
        public static void CopyRow(ISheet sheet, int startRowIndex, int sourceRowIndex, int insertCount)
        {
            for (int i = 0; i < insertCount; i++)
            {
                sheet.CopyRow(sourceRowIndex + i, startRowIndex + i);
            }
        }

        /// <summary>
        /// 金宇保存文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyValues"></param>
        /// <param name="slope"></param>
        /// <param name="unit"></param>
        public static void ExcelJinYu(DataTable[] dt, Dictionary<string, List<double[]>> keyValuePairs, Dictionary<string, double> slopes, DialogResult result, SaveFileDialog sfd, string[] path, string templateType, List<string> lstype)
        {
            //全部的sheet
            IWorkbook workb = null;
            //文件路径
            switch (templateType)
            {
                case "GB":
                    _path = "Excel\\Excel_JinYuFourLDReport_GB.xls";
                    break;
                case "QB":
                    _path = "Excel\\Excel_JinYuFourLDReport_QB.xls";
                    break;
            }

            #region 开始保存

            if (result == DialogResult.OK)
            {
                //模板文件流
                FileStream fs = File.OpenRead(_path);
                //保存的文件流
                FileStream fileStream = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                try
                {
                    //判断excel文件格式
                    if (_path.IndexOf(".xlsx") > 0) // 2007版本
                        workb = new XSSFWorkbook(fs);
                    else if (_path.IndexOf(".xls") > 0) // 2003版本
                        workb = new HSSFWorkbook(fs);

                    #region 预赋值

                    for (int ii = 0; ii < lstype.Count; ii++)
                    {
                        // 获取此文件第一个Sheet页
                        int num = 0;
                        switch (lstype[ii])
                        {
                            case "KLat":
                                num = 2;
                                break;
                            case "KLong":
                                num = 1;
                                break;
                            case "Ktor":
                                num = 3;
                                break;
                            case "Kv":
                                num = 0;
                                break;
                        }

                        var sheet = workb.GetSheetAt(num);

                        #region 刷新原先的excel单位

                        #endregion

                        int[] excelx = new int[] { 4, 7, 6, 6, 5, 9, 10, 9, 10, 11, 4, 9, 10, 11, 13, 11, 14, 5, 30, 9, 10, 15, 11, 13, 17, 14, 16, 16, 20, 20, 21, 21, 22, 22, 23, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 29 };
                        int[] excely = new int[] { 5, 1, 5, 1, 5, 1, 1, 3, 3, 1, 1, 5, 5, 5, 6, 3, 6, 1, 2, 7, 7, 2, 7, 2, 2, 2, 6, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
                        string[] str = new string[48];
                        //以下需要根据单位进行转换。
                        double TireLoad, TireRadius, TireDia, TireWeight, Temperature, TargetLoad, TargetInflation, VectorRatePz, VectorRatePxy;

                        str[0] = dt[ii].Rows[0]["TestNo"].ToString();
                        str[1] = dt[ii].Rows[0]["Operators"].ToString();
                        str[2] = dt[ii].Rows[0]["TestPurpose"].ToString();
                        str[3] = dt[ii].Rows[0]["Requestor"].ToString();
                        str[4] = dt[ii].Rows[0]["TestDate"].ToString();
                        str[5] = dt[ii].Rows[0]["TireSize"].ToString();
                        str[6] = dt[ii].Rows[0]["TireSN"].ToString();
                        str[7] = dt[ii].Rows[0]["LoadIndex"].ToString();
                        str[8] = dt[ii].Rows[0]["SpeedLevel"].ToString();
                        str[9] = dt[ii].Rows[0]["IsTubeLess"].ToString();
                        str[10] = dt[ii].Rows[0]["Manufacture"].ToString();
                        str[11] = dt[ii].Rows[0]["Brand"].ToString();
                        str[12] = dt[ii].Rows[0]["Pattern"].ToString();
                        str[13] = dt[ii].Rows[0]["TirePly"].ToString();
                        str[14] = dt[ii].Rows[0]["Platforms"].ToString();
                        str[15] = dt[ii].Rows[0]["TireWeeks"].ToString();
                        str[16] = dt[ii].Rows[0]["wheelSize"].ToString();
                        str[17] = dt[ii].Rows[0]["RequestDep"].ToString();
                        str[18] = slopes[path[ii]].ToString("f4");

                        try
                        {
                            TireLoad = double.Parse(dt[ii].Rows[0]["TireLoad"].ToString());
                            TireRadius = double.Parse(dt[ii].Rows[0]["TireRadius"].ToString());
                            TireDia = double.Parse(dt[ii].Rows[0]["TireDia"].ToString());
                            TireWeight = double.Parse(dt[ii].Rows[0]["TireWeight"].ToString());
                            Temperature = double.Parse(dt[ii].Rows[0]["Temperature"].ToString());
                            TargetLoad = double.Parse(dt[ii].Rows[0]["TargetLoad"].ToString());
                            TargetInflation = double.Parse(dt[ii].Rows[0]["TargetInflation"].ToString());
                            VectorRatePz = double.Parse(dt[ii].Rows[0]["VectorRatePz"].ToString());
                            VectorRatePxy = double.Parse(dt[ii].Rows[0]["VectorRatePxy"].ToString());

                            TireDia = TireDia.EnglishToSi(Units.inch);
                            Temperature = Temperature.EnglishToSi(Units.c);
                            TargetLoad = TargetLoad.EnglishToSi(Units.n);
                            TargetInflation = TargetInflation.EnglishToSi(Units.kpa);
                            VectorRatePz = VectorRatePz.EnglishToSi(Units.inch);
                            VectorRatePxy = VectorRatePxy.EnglishToSi(Units.inch);

                            str[19] = TireLoad.ToString("f2");
                            str[20] = TireRadius.ToString("f2");
                            str[21] = TireDia.ToString("f2");
                            str[22] = TireWeight.ToString("f2");
                            str[23] = Temperature.ToString("f2");
                            str[24] = TargetLoad.ToString("f2");
                            str[25] = TargetInflation.ToString("f2");
                            str[26] = VectorRatePz.ToString("f2");
                            str[27] = VectorRatePxy.ToString("f2");

                        }
                        catch
                        {
                            str[19] = dt[ii].Rows[0]["TireLoad"].ToString();
                            str[20] = dt[ii].Rows[0]["TireRadius"].ToString();
                            str[21] = dt[ii].Rows[0]["TireDia"].ToString();
                            str[22] = dt[ii].Rows[0]["TireWeight"].ToString();
                            str[23] = dt[ii].Rows[0]["Temperature"].ToString();
                            str[24] = dt[ii].Rows[0]["TargetLoad"].ToString();
                            str[25] = dt[ii].Rows[0]["TargetInflation"].ToString();
                            str[26] = dt[ii].Rows[0]["VectorRatePz"].ToString();
                            str[27] = dt[ii].Rows[0]["VectorRatePxy"].ToString();
                        }

                        str[28] = keyValuePairs[path[ii]][0][1].ToString("f4");
                        str[29] = keyValuePairs[path[ii]][0][0].ToString("f4");
                        str[30] = keyValuePairs[path[ii]][1][1].ToString("f4");
                        str[31] = keyValuePairs[path[ii]][1][0].ToString("f4");
                        str[32] = keyValuePairs[path[ii]][2][1].ToString("f4");
                        str[33] = keyValuePairs[path[ii]][2][0].ToString("f4");
                        str[34] = keyValuePairs[path[ii]][3][1].ToString("f4");
                        str[35] = keyValuePairs[path[ii]][3][0].ToString("f4");
                        str[36] = keyValuePairs[path[ii]][4][1].ToString("f4");
                        str[37] = keyValuePairs[path[ii]][4][0].ToString("f4");
                        str[38] = keyValuePairs[path[ii]][5][1].ToString("f4");
                        str[39] = keyValuePairs[path[ii]][5][0].ToString("f4");
                        str[40] = keyValuePairs[path[ii]][6][1].ToString("f4");
                        str[41] = keyValuePairs[path[ii]][6][0].ToString("f4");
                        str[42] = keyValuePairs[path[ii]][7][1].ToString("f4");
                        str[43] = keyValuePairs[path[ii]][7][0].ToString("f4");
                        str[44] = keyValuePairs[path[ii]][8][1].ToString("f4");
                        str[45] = keyValuePairs[path[ii]][8][0].ToString("f4");
                        str[46] = keyValuePairs[path[ii]][9][1].ToString("f4");
                        str[47] = keyValuePairs[path[ii]][9][0].ToString("f4");

                        //循环表格赋值 
                        for (int i = 0; i < 48; i++)
                        {
                            if (i < 27)
                            {
                                sheet.GetRow(excelx[i]).GetCell(excely[i]).SetCellValue(str[i]);
                            }
                            else
                            {
                                double value = double.Parse(double.Parse(str[i]).ToString("f4"));
                                sheet.GetRow(excelx[i]).GetCell(excely[i]).SetCellFormula(str[i] + "*1");
                                sheet.ForceFormulaRecalculation = true;
                            }
                        }
                    }
                    workb.Write(fileStream);
                    #endregion
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    fileStream.Close();
                    fs.Close();
                }
            }

            #endregion
        }

    }
}

