using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;

namespace ZbClassLibrary
{
    /// <summary>
    /// 表达式计算
    /// </summary>
    public class Evaluate
    {
        /// <summary>
        /// 计算组合条件(目前支持两个组合条件)
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool Expression(string expression)
        {
            try
            {
                return Convert.ToBoolean(expression);
            }
            catch
            {
                string strreg = @"(?<x>.+)(?<link>(&&|\|\|))(?<y>.+)";
                Regex regint = new Regex(strreg);

                if (regint.IsMatch(expression))
                {
                    Match m = regint.Match(expression);
                    string x = m.Groups["x"].Value;
                    string y = m.Groups["y"].Value;
                    string link = m.Groups["link"].Value;

                    //计算X表达式
                    bool xbool = EvaluateBool(x);

                    //计算Y表达式
                    bool ybool = EvaluateBool(y);

                    if (link == "&&")
                    {
                        if (xbool && ybool) { return true; }
                    }

                    if (link == "||")
                    {
                        if (xbool || ybool) { return true; }
                    }
                }
                else
                {
                    return EvaluateBool(expression);
                }
            }

            return false;
        }

        /// <summary>
        /// 计算逻辑表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        static bool EvaluateBool(string expression)
        {
            try
            {
                return Convert.ToBoolean(expression);
            }
            catch
            {
                string strreg = @"(?<x>.+)(?<operate>(>|<|>=|<=|==|!=))(?<y>.+)|(?<x>.+)(?<operate>(>|<|>=|<=|==|!=))""(?<y>.+)""";

                Regex regint = new Regex(strreg);

                if (regint.IsMatch(expression))
                {
                    Match m = regint.Match(expression);
                    string x = EvaluateValue(m.Groups["x"].Value);
                    string y = EvaluateValue(m.Groups["y"].Value);
                    string operate = m.Groups["operate"].Value;

                    return EvaluateBool(x, operate, y);
                }

                return false;
            }
        }


        /// <summary>
        /// 计算+,-,*,/,%
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        static string EvaluateValue(string expression)
        {
            string strreg = @"(?<x>.+)(?<operate>(\+|\-|\*|\/|\%))(?<y>.+)";

            Regex regint = new Regex(strreg);

            if (regint.IsMatch(expression))
            {
                Match m = regint.Match(expression);
                double x = 0;
                double y = 0;
                try
                {
                    x = double.Parse(m.Groups["x"].Value);
                    y = double.Parse(m.Groups["y"].Value);
                }
                catch { return expression; }
                string operate = m.Groups["operate"].Value;

                return EvaluateValue(x, operate, y).ToString();
            }

            return expression;
        }

        /// <summary>
        ///  计算+,-,*,/,%
        /// </summary>
        /// <param name="x"></param>
        /// <param name="operate"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static double EvaluateValue(double x, string operate, double y)
        {
            if (operate == "+")
            {
                return x + y;
            }

            if (operate == "-")
            {
                return x - y;
            }

            if (operate == "*")
            {
                return x * y;
            }

            if (operate == "/")
            {
                return x / y;
            }

            if (operate == "%")
            {
                return x % y;
            }

            return 0;
        }

        /// <summary>
        /// 计算两个数值的bool类型
        /// </summary>
        /// <param name="x"></param>
        /// <param name="operate"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static bool EvaluateBool(double x, string operate, double y)
        {
            if (operate == ">")
            {
                if (x > y) { return true; }
            }
            if (operate == "==")
            {
                if (x == y) { return true; }
            }
            if (operate == "<")
            {
                if (x < y) { return true; }
            }
            if (operate == ">=")
            {
                if (x >= y) { return true; }
            }
            if (operate == "<=")
            {
                if (x <= y) { return true; }
            }

            return false;
        }

        /// <summary>
        /// 计算两个字符的bool类型
        /// </summary>
        /// <param name="x"></param>
        /// <param name="operate"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static bool EvaluateBool(string x, string operate, string y)
        {
            try
            {
                return EvaluateBool(double.Parse(x), operate, double.Parse(y));
            }
            catch
            {
                if (operate == "==")
                {
                    if (x == y) { return true; }
                }

                if (operate == "!=")
                {
                    if (x != y) { return true; }
                }
            }

            return false;
        }
    }
}
