using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;

namespace ZbClassLibrary.DTcms
{
    /// <summary>
    /// 手机短信
    /// </summary>
    public partial class SMSMessage
    {
        private string url = "http://utf8.sms.webchinese.cn/";
        private string ckurl = "http://sms.webchinese.cn/web_api/SMS/";
        private string Uid = "ok9v";
        private string Key = "42daed65316f17f66f23";
       
        public SMSMessage()
        { }

        /// <summary>
        /// 检查账户信息是否正确
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(Uid) || string.IsNullOrEmpty(Key))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobiles">手机号码，以英文“,”逗号分隔开</param>
        /// <param name="content">短信内容 每条短信小于等于64个字</param>
        /// <param name="msg">返回提示信息</param>
        /// <returns>bool</returns>
        public bool Send(string mobiles, string content, out string msg)
        {
            //检查是否设置好短信账号
            if (!Exists())
            {
                msg = "短信配置参数有误，请完善后再提交！";
                return false;
            }
            //检查手机号码，如果超过100则分批发送
            int sucCount = 0; //成功提交数量
            string errorMsg = string.Empty; //错误消息
            string[] oldMobileArr = mobiles.Split(',');
            int batch = oldMobileArr.Length / 100 + 1; //100条为一批，求出分多少批

            for (int i = 0; i < batch; i++)
            {
                StringBuilder sb = new StringBuilder();
                int sendCount = 0; //发送数量
                int maxLenght = (i + 1) * 100; //循环最大的数

                //检测号码，忽略不合格的，重新组合
                for (int j = 0; j < oldMobileArr.Length && j < maxLenght; j++)
                {
                    int arrNum = j + (i * 100);
                    string pattern = @"^1\d{10}$";
                    string mobile = oldMobileArr[arrNum].Trim();
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
                    Match m = r.Match(mobile); //搜索匹配项
                    if (m != null)
                    {
                        sendCount++;
                        sb.Append(mobile + ",");
                    }
                }

                //发送短信
                if (sb.ToString().Length > 0)
                {
                    try
                    {
                        string result = Utils.HttpPost(url,
                            "Uid="+Uid+"&Key="+Key+"&smsMob="+Utils.DelLastComma(sb.ToString())+"&smsText="+ Utils.UrlEncode(content));
                        switch (result)
                        {
                            case "-1":
                                errorMsg = "没有该用户账户";
                                break;
                            case "-2":
                                errorMsg = "接口密钥不正确";
                                break;
                            case "-21":
                                errorMsg = "MD5接口密钥加密不正确";
                                break;
                            case "-3":
                                errorMsg = "短信数量不足";
                                break;
                            case "-11":
                                errorMsg = "该用户被禁用";
                                break;
                            case "-14":
                                errorMsg = "短信内容出现非法字符";
                                break;
                            case "-4":
                                errorMsg = "手机号格式不正确";
                                break;
                            case "-41":
                                errorMsg = "手机号码为空";
                                break;
                            case "-42":
                                errorMsg = "短信内容为空";
                                break;
                            case "-51":
                                errorMsg = "短信签名格式不正确";
                                break;
                            case "-6":
                                errorMsg = "IP限制";
                                break;
                            default: break;
                        }
                        sucCount += Utils.StrToInt(result,0); //成功数量
                    }
                    catch(Exception ee)
                    {
                        msg = ee.Message + "," + ee.StackTrace;
                        //没有动作
                    }
                }
            }

            //返回状态
            if (sucCount > 0)
            {
                msg = "成功提交" + sucCount + "条，失败" + (oldMobileArr.Length - sucCount) + "条";
                return true;
            }
            msg = errorMsg;
            return false;
        }

        /// <summary>
        /// 查询账户剩余短信数量
        /// </summary>
        public int GetAccountQuantity()
        {
            //检查是否设置好短信账号
            if (!Exists())
            {
                return 0;
            }
            try
            {
                string result = Utils.HttpPost(ckurl, "Action=SMS_Num&Uid="+Uid+"&Key="+Key+"");
                
                return Utils.StrToInt(result, 0);
            }
            catch
            {
                return 0;
            }
        }

    }
}
