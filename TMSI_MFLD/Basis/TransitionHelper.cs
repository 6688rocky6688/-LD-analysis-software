using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSI_MFLD.Basis
{
    /// <summary>
    /// 公制转换扩展类
    /// </summary>
    public static class TransitionHelper
    {
        #region 字段定义

        //进制字段
        static double kpa = 6.89475728;          //压强的单位，名称是千帕
        static double N = 4.4482216;            //力的单位，即牛顿，简称牛
        static double mm = 25.4;               //毫米
        static double s = 1;                   //角度
        static double Kgf = 0.4535924;         //是一种力的单位,叫千克力
        static double _kg = 9.80665;               //公斤
        static double _kgm = 0.1382863;         //公斤米
        static double _nm = 1.355;                  //牛米
        static double _psi = 1.355;

        #endregion

        /// <summary>
        /// 英制转公制扩展方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valuename"></param>
        /// <returns></returns>
        public static double EnglishToSi(this double value, string valuename)
        {
            switch (valuename)
            {
                case "Fz":
                    value = value * N;
                    Convert.ToDouble(value.ToString("0.000"));
                    return value;
                case "Pz":
                    value = value * mm;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Fx":
                    value = value * N;
                    Convert.ToDouble(value.ToString("0.000"));
                    return value;
                case "Mx":
                    value = value * N;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "My":
                    value = value * N;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Mz":
                    value = value * _nm;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Pxy":
                    value = value * mm;
                    Convert.ToDouble(value.ToString("0.000"));
                    return value;
                case "Az":
                    value = value * s;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Inf":
                    value = value * N;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Fy":
                    value = value * N;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                default:
                    return 0.0;
            }
        }

        /// <summary>
        /// 英制转公制扩展方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valuename"></param>
        /// <returns></returns>
        public static double EnglishToSi(this double value, Units units)
        {
            //C（摄氏度）= (F（华氏度）-32)÷1.8来转换。
            if (units == Units.c) return (value - 32) / 1.8;
            if (units == Units.n) return value * N;
            if (units == Units.kg) return (value * N)/_kg;
            if (units == Units.kpa) return value * kpa;
            if (units == Units.inch) return value * mm;
            return 0.0;
        }

        /// <summary>
        /// 英制转MKS扩展方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valuename"></param>
        /// <returns></returns>
        public static double EnglishToMKS(this double value, string valuename)
        {
            switch (valuename)
            {
                case "Fz"://kg
                    value = (value * N) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Pz"://mm
                    value = value * mm;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Fx"://kg
                    value = (value * N) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Mx":
                    value = (value * N) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "My":
                    value = (value * N) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Mz"://
                    value = (value * _nm) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Pxy"://mm
                    value = value * mm;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Az"://deg--三个制度都是角度，不用转换
                    value = value * s;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Inf"://压强
                    value = (value * N) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                case "Fy"://kg
                    value = (value * N) / _kg;
                    Convert.ToDouble(value.ToString("f4"));
                    return value;
                default:
                    return 0.0;
            }
        }

        /// <summary>
        /// 单位设置拓展方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Transition"></param>
        /// <returns></returns>
        public static string Unit(this string name, string Transition)
        {
            switch (Transition)
            {
                case "English":
                    switch (name)
                    {
                        case "Fz":
                            name = "(lbs)";
                            return name;
                        case "Pz":
                            name = "(in)";
                            return name;
                        case "Fx":
                            name = "(lbs)";
                            return name;
                        case "Mx":
                            name = "(ftp)";
                            return name;
                        case "My":
                            name = "(lbs)";
                            return name;
                        case "Mz":
                            name = "(lbs)";
                            return name;
                        case "Pxy":
                            name = "(in)";
                            return name;
                        case "Az":
                            name = "(deg)";
                            return name;
                        case "Inf":
                            name = "(psi)";
                            return name;
                        case "Fy":
                            name = "(lbs)";
                            return name;
                        default:
                            return "";
                    }
                case "SI":
                    switch (name)
                    {
                        case "Fz":
                            name = "(N)";
                            return name;
                        case "Pz":
                            name = "(mm)";
                            return name;
                        case "Fx":
                            name = "(N)";
                            return name;
                        case "Mx":
                            name = "(Nm)";
                            return name;
                        case "My":
                            name = "(Nm)";
                            return name;
                        case "Mz":
                            name = "(Nm)";
                            return name;
                        case "Pxy":
                            name = "(mm)";
                            return name;
                        case "Az":
                            name = "(deg)";
                            return name;
                        case "Inf":
                            name = "(kpa)";
                            return name;
                        case "Fy":
                            name = "(N)";
                            return name;
                        default:
                            return "";
                    }
                case "MKS":
                    switch (name)
                    {
                        case "Fz":
                            name = "(kg)";
                            return name;
                        case "Pz":
                            name = "(mm)";
                            return name;
                        case "Fx":
                            name = "(kg)";
                            return name;
                        case "Mx":
                            name = "(kgm)";
                            return name;
                        case "My":
                            name = "(kgm)";
                            return name;
                        case "Mz":
                            name = "(kgm)";
                            return name;
                        case "Pxy":
                            name = "(mm)";
                            return name;
                        case "Az":
                            name = "(deg)";
                            return name;
                        case "Inf":
                            name = "(kpa)";
                            return name;
                        case "Fy":
                            name = "(kg)";
                            return name;
                        default:
                            return "";
                    }
                default:
                    return "";
            }
        }

        /// <summary>
        /// chart起始值扩展方法
        /// </summary>
        /// <param name="doublearray"></param>
        /// <param name="strarray"></param>
        /// <returns></returns>
        public static double ReXMin(this double xmin, string xname, string Transition, string yname)
        {
            switch (Transition)
            {
                case "English":
                    switch (xname)
                    {
                        case "Fz":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Pz":
                            xmin = Convert.ToDouble(xmin.ToString("f1"));
                            return xmin;
                        case "Fx":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Mx":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "My":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Mz":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Pxy":
                            if (yname == "Fx")
                            {
                                xmin = Convert.ToDouble(xmin.ToString("f2"));
                                return xmin;
                            }
                            else
                            {
                                xmin = Convert.ToDouble(xmin.ToString("f2"));
                                return xmin;
                            }
                        case "Az":
                            xmin = Convert.ToDouble(xmin.ToString("f0"));
                            return xmin;
                        case "Inf":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Fy":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        default:
                            return 0.0;
                    }
                case "SI":
                    switch (xname)
                    {
                        case "Fz":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Pz":
                            xmin = Convert.ToDouble(xmin.ToString("f0"));
                            return xmin;
                        case "Fx":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Mx":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "My":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Mz":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Pxy":
                            if (yname == "Fx")
                            {
                                xmin = Convert.ToDouble(xmin.ToString("f1"));
                                return xmin;
                            }
                            else
                            {
                                xmin = Convert.ToDouble(xmin.ToString("f0"));
                                return xmin;
                            }
                        case "Az":
                            xmin = Convert.ToDouble(xmin.ToString("f0"));
                            return xmin;
                        case "Inf":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Fy":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        default:
                            return 0.0;
                    }
                case "MKS":
                    switch (xname)
                    {
                        case "Fz":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Pz":
                            xmin = Convert.ToDouble(xmin.ToString("f0"));
                            return xmin;
                        case "Fx":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Mx":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "My":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Mz":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Pxy":
                            if (yname == "Fx")
                            {
                                xmin = Convert.ToDouble(xmin.ToString("f1"));
                                return xmin;
                            }
                            else
                            {
                                xmin = Convert.ToDouble(xmin.ToString("f0"));
                                return xmin;
                            }
                        case "Az":
                            xmin = Convert.ToDouble(xmin.ToString("f0"));
                            return xmin;
                        case "Inf":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        case "Fy":
                            xmin = Convert.ToDouble(xmin.ToString("f2"));
                            return xmin;
                        default:
                            return 0.0;
                    }
                default:
                    return 0.0;
            }
        }

        /// <summary>
        /// chart偏移拓展方法
        /// </summary>
        /// <returns></returns>
        public static double Interval(this string xname, string Transition, string yname)
        {
            switch (Transition)
            {
                case "English":
                    switch (xname)
                    {
                        case "Fz":
                            return 0.0;
                        case "Pz":
                            return 0.1;
                        case "Fx":
                            return 0.0;
                        case "Mx":
                            return 0.0;
                        case "My":
                            return 0.0;
                        case "Mz":
                            return 0.0;
                        case "Pxy":
                            if (yname == "Fx")
                            {
                                return 0.005;
                            }
                            else
                            {
                                return 0.025;
                            }
                        case "Az":
                            return 0.5;
                        case "Inf":
                            return 0.0;
                        case "Fy":
                            return 0.0;
                        default:
                            return 0.0;
                    }
                case "SI":
                    switch (xname)
                    {
                        case "Fz":
                            return 0.0;
                        case "Pz":
                            return 5.0;
                        case "Fx":
                            return 0.0;
                        case "Mx":
                            return 0.0;
                        case "My":
                            return 0.0;
                        case "Mz":
                            return 0.0;
                        case "Pxy":
                            if (yname == "Fx")
                            {
                                return 0.25;
                            }
                            else
                            {
                                return 0.5;
                            }
                        case "Az":
                            return 0.5;
                        case "Inf":
                            return 0.0;
                        case "Fy":
                            return 0.0;
                        default:
                            return 0.0;
                    }
                case "MKS":
                    switch (xname)
                    {
                        case "Fz":
                            return 0.0;
                        case "Pz":
                            return 5.0;
                        case "Fx":
                            return 0.0;
                        case "Mx":
                            return 0.0;
                        case "My":
                            return 0.0;
                        case "Mz":
                            return 0.0;
                        case "Pxy":
                            if (yname == "Fx")
                            {
                                return 0.25;
                            }
                            else
                            {
                                return 0.5;
                            }
                        case "Az":
                            return 0.5;
                        case "Inf":
                            return 0.0;
                        case "Fy":
                            return 0.0;
                        default:
                            return 0.0;
                    }
                default:
                    return 0.0;
            }
        }


        /// <summary>
        /// 传入一个数组,求出一个数组的最大值的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int MaxIndex<T>(T[] arr) where T : IComparable<T>
        {
            var i_Pos = 0;
            var value = arr[0];
            for (var i = 1; i < arr.Length; ++i)
            {
                var _value = arr[i];
                if (_value.CompareTo(value) > 0)
                {
                    value = _value;
                    i_Pos = i;
                }
            }
            return i_Pos;
        }

        public enum Units
        {
            kg = 1,
            n = 2,
            c = 3,
            f = 4,
            kpa = 5,
            nm = 6,
            inch = 7,
            PSI = 7
        }
    }
}
