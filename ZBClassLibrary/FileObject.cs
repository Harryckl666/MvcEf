using System;
using System.Collections.Generic;

using System.Web;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ZbClassLibrary
{
    public class FileObject
    {
        /// <summary>
        /// 获取绝对路劲
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="appPath"></param>
        /// <param name="appParma"></param>
        public static void StartApp(string appName, string appPath, string appParma)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = appName;//需要启动的程序名       
            p.StartInfo.WorkingDirectory = GetMapPath(appPath);
            p.StartInfo.Arguments = appParma;
            p.Start();//启动   
        }

        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="appPath"></param>
        /// <param name="appParma"></param>
        public static void StartApp(string appName, string appParma)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = appName;
            psi.Arguments = appParma;
            psi.UseShellExecute = true;
            psi.CreateNoWindow = false;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            System.Diagnostics.Process.Start(psi);
        }


        /// <summary>
        /// 高质量裁剪图片的方法
        /// </summary>
        /// <param name="originalImage">原始图像</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns>返回一个裁剪好的图像</returns>
        public static Image CutBitmap(Image originalImage, int width, int height)
        {
            if (originalImage == null) return null;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            //指定高度的压缩方法
            if (width == 0)
            {
                if (oh > height)
                {
                    width = height * ow / oh;
                }
                else
                {
                    height = oh;
                    width = ow;
                }
            }

            //指定宽度的压缩方法
            if (height == 0)
            {
                if (ow > width)
                {
                    height = width * oh / ow;
                }
                else
                {
                    width = ow;
                    height = oh;
                }
            }

            //高宽都不指定
            if (width == 0 && height == 0)
            {
                width = ow;
                height = oh;
            }

            //压缩裁剪的方法
            if ((double)originalImage.Width / (double)originalImage.Height > (double)width / (double)height)
            {
                oh = originalImage.Height;
                ow = originalImage.Height * width / height;
                y = 0;
                x = (originalImage.Width - ow) / 2;
            }
            else
            {
                ow = originalImage.Width;
                oh = originalImage.Width * height / width;
                x = 0;
                y = (originalImage.Height - oh) / 2;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(width, height);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, width, height),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            return bitmap;
        }

        /// <summary>
        /// 自动识别页面编码
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(string html)
        {
            string pattern = "(charset=(?<charset>[^=]+)?\")|(encoding=\"(?<charset>[^=]+)?\")";
            string charset = Regex.Match(html, pattern).Groups["charset"].Value;
            try { return Encoding.GetEncoding(charset); }
            catch (ArgumentException) { return null; }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns></returns>
        public static int SaveFile(string path, string fileContent)
        {
            int result = 0;
            if (File.Exists(path))
            {
                try
                {
                    StreamWriter Fso = new StreamWriter(path, false, Encoding.Default);
                    Fso.WriteLine(fileContent);

                    Encoding encoding = GetEncoding(fileContent);

                    if (encoding != null)
                    {
                        Fso = new StreamWriter(path, false, encoding);
                        Fso.WriteLine(fileContent);
                    }

                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
                result = 1;
            }
            else
            {
                try
                {
                    StreamWriter Fso = new StreamWriter(path, true, Encoding.Default);
                    Fso.WriteLine(fileContent);

                    Encoding encoding = GetEncoding(fileContent);

                    if (encoding != null)
                    {
                        Fso = new StreamWriter(path, true, encoding);
                        Fso.WriteLine(fileContent);
                    }

                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns></returns>
        public static string GetFileText(string path)
        {
            string str_content = "";

            if (File.Exists(path))
            {
                try
                {
                    StreamReader Fso = new StreamReader(path, Encoding.Default);
                    str_content = Fso.ReadToEnd();

                    Encoding encoding = GetEncoding(str_content);

                    if (encoding != null)
                    {
                        Fso = new StreamReader(path, encoding);
                        str_content = Fso.ReadToEnd();
                    }

                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
            }
            else
            {
                throw new Exception("找不到相应的文件!");
            }
            return str_content;
        }

        /// <summary>
        /// 删除一个文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// 获取当前目录下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo[] GetFileInfos(string path)
        {
            DirectoryInfo FatherDirectory = new DirectoryInfo(path);
            return FatherDirectory.GetFiles();
        }

        /// <summary>
        /// 获取当前目录的所有子目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo[] GetDirectoryInfos(string path)
        {
            DirectoryInfo FatherDirectory = new DirectoryInfo(path);
            return FatherDirectory.GetDirectories("*.*");
        }
    }
}
