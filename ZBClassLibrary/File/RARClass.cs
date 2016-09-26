using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ZbClassLibrary
{
    public class RARClass
    {

        /* 调用方法
             string Path = "c:\\de";
            string resultPath = string.Empty;
            bool rel = false;
            TimeSpan nowTimeSpan = new TimeSpan();
            resultPath = YaSuo(out rel, out nowTimeSpan);
            ResponseFile(resultPath);
         */
        /// <summary>  
        /// 压缩文件  
        /// </summary>  
        /// <returns>返回压缩后的路径</returns>  
        public static string YaSuo(out bool bo, out TimeSpan times,string path)
        {
            string rarurlPath = string.Empty;
            bo = false;
            //压缩文件  
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("/ALLFile/RAR/")))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("/ALLFile/RAR/"));
            }
            string yasuoPathXN = "/ALLFile/RAR/" + Guid.NewGuid().ToString() + ".rar";
            string yasuoPathSave = System.Web.HttpContext.Current.Server.MapPath(yasuoPathXN);
            string yasuoPath = System.Web.HttpContext.Current.Server.MapPath(path);
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            pro.StartInfo.FileName = @"WinRAR.exe";//WinRAR所在路径  
            //pro.StartInfo.Arguments = "a " + yasuoPathSave + " " + yasuoPath + " -r ";//dir是你的目录名   
            //pro.StartInfo.Arguments = string.Format("a {0} {1} -r", yasuoPathSave, yasuoPath);
            //// 1
            ////压缩c:\freezip\free.txt(即文件夹及其下文件freezip\free.txt)
            ////到c:\freezip\free.rar
            //strzipPath = "C:\\freezip\\free";//默认压缩方式为 .rar
            //Process1.StartInfo.Arguments = " a -r " + strzipPath + " " + strtxtPath;

            //// 2
            ////压缩c:\freezip\free.txt(即文件夹及其下文件freezip\free.txt)
            ////到c:\freezip\free.rar
            //strzipPath = "C:\\freezip\\free";//设置压缩方式为 .zip
            //Process1.StartInfo.Arguments = " a -afzip " + strzipPath + " " + strtxtPath;

            //// 3
            ////压缩c:\freezip\free.txt(即文件夹及其下文件freezip\free.txt)
            ////到c:\freezip\free.zip  直接设定为free.zip
            //Process1.StartInfo.Arguments = " a -r "+strzipPath+" " + strtxtPath ;

            //// 4
            ////搬迁压缩c:\freezip\free.txt(即文件夹及其下文件freezip\free.txt)
            ////到c:\freezip\free.rar 压缩后 原文件将不存在
            //Process1.StartInfo.Arguments = " m " + strzipPath + " " + strtxtPath;

            //// 5
            ////压缩c:\freezip\下的free.txt(即文件free.txt)
            ////到c:\freezip\free.zip  直接设定为free.zip 只有文件 而没有文件夹
            //Process1.StartInfo.Arguments = " a -ep " + strzipPath + " " + strtxtPath;

            //// 6
            ////解压缩c:\freezip\free.rar
            ////到 c:\freezip\
            //strtxtPath = "c:\\freezip\\";
            //Process1.StartInfo.Arguments = " x " + strzipPath + " " + strtxtPath;

            //// 7
            ////加密压缩c:\freezip\free.txt(即文件夹及其下文件freezip\free.txt)
            ////到c:\freezip\free.zip  密码为123456 注意参数间不要空格
            //Process1.StartInfo.Arguments = " a -p123456 " + strzipPath + " " + strtxtPath;

            //// 8
            ////解压缩加密的c:\freezip\free.rar
            ////到 c:\freezip\   密码为123456 注意参数间不要空格
            //strtxtPath = "c:\\freezip\\";
            //Process1.StartInfo.Arguments = " x -p123456 " + strzipPath + " " + strtxtPath;

            //// 9 
            ////-o+ 覆盖 已经存在的文件
            //// -o- 不覆盖 已经存在的文件
            //strtxtPath = "c:\\freezip\\";
            //Process1.StartInfo.Arguments = " x -o+ " + strzipPath + " " + strtxtPath;

            ////10
            //// 只从指定的zip中
            //// 解压出free1.txt
            //// 到指定路径下
            //// 压缩包中的其他文件 不予解压
            //strtxtPath = "c:\\freezip\\";
            //Process1.StartInfo.Arguments = " x " + strzipPath + " " +" free1.txt" + " " + strtxtPath;

            //// 11
            //// 通过 -y 对所有询问回应为"是" 以便 即便发生错误 也不弹出WINRAR的窗口
            //// -cl 转换文件名为小写字母 
            //strtxtPath = "c:\\freezip\\";
            //Process1.StartInfo.Arguments = " t -y -cl " + strzipPath + " " + " free1.txt";

            //pro.StartInfo.Arguments = " a -ep " + yasuoPathSave + " " + yasuoPath;
            pro.StartInfo.Arguments = " a -ep " + yasuoPathSave + " " + yasuoPath;
            pro.Start();
            times = pro.TotalProcessorTime;
            bo = pro.WaitForExit(60000);//设定一分钟  
            if (!bo)
                pro.Kill();
            pro.Close();
            pro.Dispose();
            rarurlPath = yasuoPathXN;
            return rarurlPath;
        }


        /// <summary>  
        /// 下载文件，根据文件所在路径  
        /// </summary>  
        /// <param name="path">文件路径</param>  
        /// <param name="name">文件名</param>  
        public static void ResponseFile(string path, string name)
        {
            //流方式下载文件  
            string fileName = name;//客户端保存的文件名  
            string filePath = path;//路径  
            string extension = filePath.Substring(filePath.LastIndexOf('.') + 1);
            string file = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            //以字符流的形式下载文件  
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //HttpContext.Current.Response.ContentType = getContentType(extension);  
            //通知浏览器下载文件而不是打开  
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }  
    }
}
