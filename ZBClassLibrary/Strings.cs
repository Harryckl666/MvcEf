using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace ZbClassLibrary
{
    public class Strings
    {
        /// <summary>
        /// 是否是Email邮箱地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            if (new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").IsMatch(input))
            {
                return true;
            }
            return false;
        }
        public static bool IsIDcard(string str_idcard)
        {

            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");

        }
        /// <summary>
        /// 验证输入字符区间长度
        /// </summary>
        /// <param name="slen"></param>
        /// <param name="elen"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLen(int slen,int elen,string input)
        {
            if (input.Length > slen && input.Length <= elen)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证输入的必须是数字,却长度等于len
        /// </summary>
        /// <param name="len"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(int len,string input)
        {
            if (len == 0)
            {
                try { long.Parse(input); return true; }
                catch { return false; }
            }

            if (len >0)
            {
                try 
                { 
                   long a= long.Parse(input);
                   if (a.ToString().Length == len)
                   {
                       return true;
                   } 
                }
                catch { return false; }
            }

            return false; 
        }

        /// <summary>
        /// 格式化数字 格式如:1,860,000
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FormatNumber(int num)
        {
            string result1 = "", result2 = "", str = num.ToString();

            for (int i = str.Length - 1; i >= 0; i--)
            {
                result1 += str[i];
            }

            for (int i = result1.Length - 1; i >= 0; i--)
            {
                if (i % 3 == 0)
                {
                    result2 += result1[i] + ",";
                }
                else
                {
                    result2 += result1[i];
                }
            }

            return result2.TrimStart(',').TrimEnd(',');
        }

        /// <summary>
        /// 转化首字母为大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FristToUpper(string input)
        {
            string str = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0) { str += input[i].ToString().ToUpper(); }
                else { str += input[i]; }
            }

            return str;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string[] SplitString(string input, string split)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.IndexOf(split) < 0)
                    return new string[] { input };

                return Regex.Split(input, Regex.Escape(split), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 按长度分割字符串，如文章分页就需要用到
        /// </summary>
        /// <param name="input"></param>
        /// <param name="split"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string[] SplitString(string input, string split, int len)
        {
            string[] result = new string[len];
            string[] splited = SplitString(input, split);

            for (int i = 0; i < len; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 裁剪前面N位字符
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutBeginString(string input, int len)
        {
            string result = "";

            if (input.Length > len)
            {
                for (int i = len - 1; i < input.Length; i++)
                {
                    result += input[i];
                }
            }

            return result;
        }

        /// <summary>
        /// 裁剪字符长度
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="intLen"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string CutString(string strInput, int intLen, string sp)
        {
            intLen = intLen * 2;
            strInput = strInput.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(strInput);
            if (myByte.Length > intLen)
            {
                //截取操作
                string resultStr = "";
                for (int i = 0; i < strInput.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(resultStr);
                    if (tempByte.Length < intLen - 4)
                    {
                        resultStr += strInput.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return resultStr + sp;
            }
            else
            {
                return strInput;
            }
        }

        /// <summary>
        /// 从一段HTML文本中提取出一定字数的纯文本
        /// </summary>
        /// <param name="html">HTML代码</param>
        /// <param name="withLink">是否要链接里面的字</param>
        /// <returns>纯文本</returns>
        public static string filterHTML(string html, bool withLink)
        {
            string m_outstr = "";

            m_outstr = html.Clone() as string;
            m_outstr = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
            m_outstr = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
            m_outstr = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");

            if (!withLink) m_outstr = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");

            Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg.Replace(m_outstr, "");
            Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg2.Replace(m_outstr, " ");

            return m_outstr.Replace("&nbsp;", "");
        }

    }
}
