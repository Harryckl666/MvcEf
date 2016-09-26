using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Drawing.Imaging;

namespace ZbClassLibrary.DTcms
{
    public class CreateVerifyCode
    {
        private Random _Random = new Random();

        private int _Width = 150;
        /// <summary>
        /// 验证码宽
        /// </summary>
        public int Width { get { return _Width; } }
        private int _Height = 28;
        /// <summary>
        /// 验证码高
        /// </summary>
        public int Height { get { return _Height; } }
        private int _Length = 4;
        /// <summary>
        /// 验证码位数
        /// </summary>
        public int Length { get { return _Length; } }
        private int _FontSize = 18;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get { return _FontSize; } }
        private int _NoiseCount = 100;
        /// <summary>
        /// 噪点个数
        /// </summary>
        public int NoiseCount { get { return _NoiseCount; } }
        private int _LineCount = 6;
        /// <summary>
        /// 干扰线个数
        /// </summary>
        public int LineCount { get { return _LineCount; } }

        /// <summary>
        /// 验证码构造函数
        /// </summary>
        /// <param name="codeSetting">验证码规格参数设置</param>
        public CreateVerifyCode(VerifyCodeModel verifycode)
            : this(verifycode.Width, verifycode.Height, verifycode.FontSize,verifycode.Length, verifycode.NoiseCount, verifycode.LineCount)
        { }
        /// <summary>
        /// 验证码构造函数
        /// </summary>
        /// <param name="width">验证码宽</param>
        /// <param name="height">验证码高</param>
        /// <param name="frameCount">帧数</param>
        /// <param name="delay">延迟</param>
        /// <param name="noiseCount">噪点高数</param>
        /// <param name="lineCount">干扰线个数</param>
        public CreateVerifyCode(int width, int height, int fontSize, int length, int noiseCount, int lineCount)
        {
            _Width = width < 1 ? 1 : width;
            _Height = height < 1 ? 1 : height;

            _Length = length;
            _FontSize = fontSize;
            _NoiseCount = noiseCount;
            _LineCount = lineCount;
        }

        private MemoryStream ProcessGraphicPng(out string verifyCode, MemoryStream stream)
        {
            int codeW = _Width;
            int codeH = _Height;
            int fontSize = _FontSize;
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < _Length; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            verifyCode = chkCode;
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < _LineCount; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)0);
            }
            //画噪点 
            for (int i = 0; i < _NoiseCount; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }
            try
            {
                bmp.Save(stream, ImageFormat.Png);
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }

            return stream;
            
        }
        public Stream Create(out string verifyCode, MemoryStream stream)
        {
            return ProcessGraphicPng(out verifyCode, stream); 
        }

        public MemoryStream Create(out string verifyCode)
        {
            MemoryStream stream = new MemoryStream();
            return ProcessGraphicPng(out verifyCode, stream); 
        }
        public void ProcessRequest(out string verifyCode, HttpContext context)
        {
            //清除该页输出缓存，设置该页无缓存 
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AppendHeader("Pragma", "No-Cache");

            context.Response.ClearContent();
            context.Response.ContentType = "image/Png";
            var stream = Create(out verifyCode);
            context.Response.BinaryWrite(stream.ToArray());
            stream.Dispose();
        }
    }
}