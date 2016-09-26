using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Text.RegularExpressions;


namespace DAL
{
    public class WebHelp
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }
        /// <summary>
        /// 得到去除重复后的ID
        /// </summary>dt---需要去除重复的DataTable,zd----要得到的字段
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public static List<string> GetList(DataTable dt, string zd)
        {

            //存放供货商id(所有)
            string[] suids = new string[dt.Rows.Count];
            List<string> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                suids[i] = dt.Rows[i][zd].ToString();

            }
            foreach (string s in suids)
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }
            return list;
        }


        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string Ip)
        {
            if (Ip == null || Ip == string.Empty || Ip.Length < 7 || Ip.Length > 15) return false;
            string regformat = @"^/d{1,3}[/.]/d{1,3}[/.]/d{1,3}[/.]/d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(Ip);
        }
        /// <summary>
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址
        /// </summary>
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP
                    }
                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }



        #region 压缩图片,商品详情页

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="filePath">要压缩的图片的路径</param>
        /// <param name="filePath_ystp">压缩后的图片的路径</param>
        public void ystp(string filePath, string filePath_ystp)
        {
        
            //Bitmap
            Bitmap bmp = null;

            //ImageCoderInfo 
            ImageCodecInfo ici = null;

            //Encoder
            System.Drawing.Imaging.Encoder ecd = null;

            //EncoderParameter
            EncoderParameter ept = null;

            //EncoderParameters
            EncoderParameters eptS = null;

            try
            {
                bmp = new Bitmap(filePath);

                ici = this.getImageCoderInfo("image/jpeg");

                ecd = System.Drawing.Imaging.Encoder.Quality;

                eptS = new EncoderParameters(1);

                ept = new EncoderParameter(ecd, 60L);//压缩比
                eptS.Param[0] = ept;
                bmp.Save(filePath_ystp, ici, eptS);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                bmp.Dispose();

                ept.Dispose();

                eptS.Dispose();
            }
        }

        /// <summary>
        /// 获取图片编码类型信息
        /// </summary>
        /// <param name="coderType">编码类型</param>
        /// <returns>ImageCodecInfo</returns>
        private ImageCodecInfo getImageCoderInfo(string coderType)
        {
            ImageCodecInfo[] iciS = ImageCodecInfo.GetImageEncoders();

            ImageCodecInfo retIci = null;

            foreach (ImageCodecInfo ici in iciS)
            {
                if (ici.MimeType.Equals(coderType))
                    retIci = ici;
            }

            return retIci;
        }

        #endregion 压缩图片




        #region 图片等比压缩，首页，手机
        /// <summary>
        /// 矩形框内部图片原比例压缩
        /// </summary>
        /// <param name="AbsoluteFileName">原路径</param>
        /// <param name="SaveServer">新存放路径</param>
        /// <param name="H">对应矩形高度</param>
        /// <param name="W">对应矩形宽度</param>
        /// <returns></returns>
        public string PicShrink(string AbsoluteFileName, string SaveServer, int H, int W)
        {
          
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(AbsoluteFileName))
            {
                int OriginalHeight = img.Height; //原图高度
                int OriginalWeight = img.Width; //原图宽度
                int NewHeight = OriginalHeight;
                int NewWeight = OriginalWeight;
                double p = 0;//缩放比例

                //高度压缩比例小或相等[宽和高都溢出]
                if ((double)H / (double)OriginalHeight <= (double)W / (double)OriginalWeight)
                {
                    NewHeight = H;
                    p = (double)H / (double)OriginalHeight;
                    NewWeight = (int)Math.Round(p * OriginalWeight);
                }
                //宽度压缩比例小[宽和高都溢出]
                if ((double)H / (double)OriginalHeight > (double)W / (double)OriginalWeight)
                {
                    NewWeight = W;
                    p = (double)W / (double)OriginalWeight;
                    NewHeight = (int)Math.Round(p * OriginalHeight);
                }
                using (Bitmap bm = new Bitmap(NewWeight, NewHeight))
                {
                    Graphics grap = Graphics.FromImage(bm);
                    grap.Clear(Color.Transparent);  //指定图片背景色
                    Rectangle rt = new Rectangle(0, 0, NewWeight, NewHeight);
                    grap.DrawImage(img, rt);
                    string MathPath = System.Web.HttpContext.Current.Server.MapPath(SaveServer);
                    if (File.Exists(MathPath))
                    {
                        File.Delete(MathPath);
                    }
                    bm.Save(MathPath, ImageFormat.Jpeg);
                    bm.Dispose();
                    grap.Dispose();
                }
            }
            return SaveServer;
        }
        #endregion



        #region 生成缩略图文件
        /// <summary>
        /// 1.要生成缩略图的文件，2.保存后的文件
        /// </summary>
        public static string SLT(string sltFile, string savePath,int width,int height)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(sltFile);
            img = img.GetThumbnailImage(width, height, null, IntPtr.Zero);//缩放
            img.Save(savePath);
            img.Dispose();
            return savePath;
        }
        #endregion


        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <returns></returns>
        public string ResizePic(string oldpath, string newpath, string newpath2)
        {
            #region 压缩图片开始
            bool IsImgFile = true;  //判断是否为图片文件         
            if (IsImgFile)
            {
                int maxWidth = 220;   //图片宽度最大限制
          
                System.Drawing.Image imgPhoto =
                    System.Drawing.Image.FromFile(oldpath);
                int imgWidth = imgPhoto.Width;
                int imgHeight = imgPhoto.Height;
                if (imgWidth > imgHeight)  //如果宽度超过高度以宽度为准来压缩
                {
                    if (imgWidth > maxWidth)  //如果图片宽度超过限制
                    {
                        float toImgWidth = maxWidth;   //图片压缩后的宽度
                        float toImgHeight = imgHeight / (float)(imgWidth / toImgWidth); //图片压缩后的高度
                        System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto,
                                                                              int.Parse(toImgWidth.ToString("F0")),
                                                                              int.Parse(toImgHeight.ToString("F0")));
                        string strResizePicName = newpath;
                        img.Save(strResizePicName);  //保存压缩后的图片
                        ystp(strResizePicName, newpath2);
                    }
                }
                else if (imgWidth > maxWidth)
                {
                    float toImgWidth = maxWidth;   //图片压缩后的宽度
                    float toImgHeight = imgHeight / (float)(imgWidth / toImgWidth); //图片压缩后的高度
                    System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto,
                                                                          int.Parse(toImgWidth.ToString("F0")),
                                                                          int.Parse(toImgHeight.ToString("F0")));
                    string strResizePicName = newpath;
                    img.Save(strResizePicName);  //保存压缩后的图片
                    ystp(strResizePicName, newpath2);
                
                }
                //else
                //{
                //    if (imgHeight > maxHeight)
                //    {
                //        float toImgHeight1 = maxHeight;
                //        float toImgWidth1 =maxWidth;

                //        System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto,
                //                                                              int.Parse(toImgWidth1.ToString("F0")),
                //                                                              int.Parse(toImgHeight1.ToString("F0")));
                //        string strResizePicName = newpath;
                //        img.Save(strResizePicName);
                //        //filePath = strImgPath + filePathName + "/_small_" + fileSysName;
                //    }
                //}
            }
            return newpath2;
            #endregion
        }


        /// <summary>  
        /// 无损压缩图片  
        /// </summary>  
        /// <param name="sFile">原图片</param>  
        /// <param name="dFile">压缩后保存位置</param>  
        /// <param name="dHeight">高度</param>  
        /// <param name="dWidth">宽度</param>  
        /// <param name="flag">压缩质量 1-100</param>  
        /// <returns></returns>  
        public static bool CompressImage(string sFile, string dFile, int dHeight, int dWidth, int flag)
        {

            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放  
            Size tem_size = new Size(iSource.Width, iSource.Height);

            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }
            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量  
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100  
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径  
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }

        }
        #region Table转换为Json
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>DataTable转JSON字符串</returns>   
        /// <remarks></remarks>
        public static string TableToJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling       
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"data\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            string intro = dt.Rows[i][j].ToString();
                            intro = intro.Replace("\r\n","\\r\\n");
                            intro = intro.Replace("\n", "\\r\\n");
                            intro = intro.Replace("\"", "“");
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + intro + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            string intro = dt.Rows[i][j].ToString();
                            intro = intro.Replace("\r\n", "\\r\\n");
                            intro = intro.Replace("\n", "\\r\\n");
                            intro = intro.Replace("\"", "“");
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + intro + "\"");
                        }
                    }


                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                JsonString.Append("," + "\"success\"" + ":" + "\"true\"" + "}");
                return JsonString.ToString();
            }
            else
            {
                return "null";
            }
        }
        #endregion


        /// <summary>
        /// 汉字转拼音静态类,包括功能全拼和缩写，方法全部是静态的
        /// </summary>

        #region 属性数据定义
        /// <summary>
        /// 汉字的机内码数组
        /// </summary>
        private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };
        /// <summary>
        /// 机内码对应的拼音数组
        /// </summary>
        private static string[] pyName = new string[]
        {
            "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
            "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
            "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
            "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
            "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
            "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
            "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
            "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
            "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
            "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
            "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
            "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
            "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
            "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
            "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
            "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
            "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
            "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
            "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
            "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
            "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
            "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
            "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
            "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
            "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
            "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
            "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
            "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
            "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
            "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
            "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
            "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
            "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };
        #endregion
        #region 把汉字转换成拼音(全拼)
        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string ConvertToPinYin(string hzString)
        {
            // 匹配中文字符
            Regex regex = new Regex(@"^[\u4e00-\u9fa5]+$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();
            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += "Zhen";                       
                        else if (chrAsc == -9027)  // 修正“芙”字
                            pyString += "Fu";
                        else if (chrAsc == -10536)  // 修正“重（chong）”字
                            pyString += "Chong";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }
        #endregion

       

        /// <summary> 
        /// 给图片上水印 
        /// </summary> 
        /// <param name="filePath">原图片地址</param> 
        /// <param name="waterFile">水印图片地址</param> 
        public void MarkWater(string filePath, string waterFile,string SavePath)
        {
            //GIF不水印 
            int i = filePath.LastIndexOf(".");
            string ex = filePath.Substring(i, filePath.Length - i);
            if (string.Compare(ex, ".gif", true) == 0)
            {
                return;
            }

            string ModifyImagePath = SavePath;//修改的图像路径 
            int lucencyPercent = 100;
            System.Drawing.Image modifyImage = null;
            System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象 
                modifyImage = System.Drawing.Image.FromFile(ModifyImagePath, true);
                drawedImage = System.Drawing.Image.FromFile(waterFile, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标 
                int x = modifyImage.Width - drawedImage.Width;
                int y = modifyImage.Height - drawedImage.Height-10;
                //设置颜色矩阵 
                float[][] matrixItems ={ 
            new float[] {1, 0, 0, 0, 0}, 
            new float[] {0, 1, 0, 0, 0}, 
            new float[] {0, 0, 1, 0, 0}, 
            new float[] {0, 0, 0, (float)lucencyPercent/100f, 0}, 
            new float[] {0, 0, 0, 0, 1}};

                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //绘制阴影图像 
                g.DrawImage(drawedImage, new Rectangle(x, y, drawedImage.Width, drawedImage.Height), 0, 0, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(ModifyImagePath);
                ImageFormat imageType = ImageFormat.Gif;
                switch (fi.Extension.ToLower())
                {
                    case ".jpg": imageType = ImageFormat.Jpeg; break;
                    case ".gif": imageType = ImageFormat.Gif; break;
                    case ".png": imageType = ImageFormat.Png; break;
                    case ".bmp": imageType = ImageFormat.Bmp; break;
                    case ".tif": imageType = ImageFormat.Tiff; break;
                    case ".wmf": imageType = ImageFormat.Wmf; break;
                    case ".ico": imageType = ImageFormat.Icon; break;
                    default: break;
                }
                MemoryStream ms = new MemoryStream();
                modifyImage.Save(ms, imageType);
                byte[] imgData = ms.ToArray();
                modifyImage.Dispose();
                drawedImage.Dispose();
                g.Dispose();
                FileStream fs = null;
                File.Delete(ModifyImagePath);
                fs = new FileStream(ModifyImagePath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
            }
            finally
            {
                try
                {
                    drawedImage.Dispose();
                    modifyImage.Dispose();
                    g.Dispose();
                }
                catch
                {
                }
            }
        } 


    }



}
