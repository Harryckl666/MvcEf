using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Threading;
using System.Globalization;

namespace ZbClassLibrary
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class BigFileInfo
    {
        public string Title = "";
        public string Thumb = "";
        public string Path = "";
        public int Bytes = 0;
        public string Extension = "";

        public string ymd = "";
        public string filename = "";
        public string dirPath = "";
    }

    /// <summary>
    /// 大文件上传类
    /// </summary>
    public class BigFileUpload
    {
        /// <summary>
        /// 允许上传的文件格式
        /// </summary>
        static string[] fileExt = {  ".gif", ".jpg", ".jpeg", ".png", ".bmp", 
                                      ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pdf", ".pptx", ".xml", ".rar", ".zip", ".iso", 
                                      ".mp3", ".wav", ".wma", ".aif", ".cda", ".ape", ".aac", ".vqf", ".asf", ".ogg",
                                       ".flv", ".asf", ".avi", ".mpg", ".3gp", ".mov", ".wmv", ".rm", ".rmvb"  
                                     };
        /// <summary>
        /// 允许上传的图片格式
        /// </summary>
        static string[] imgExt = { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
        /// <summary>
        /// 允许上传文档文件格式
        /// </summary>
        static string[] docZipExt = { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pdf", ".pptx", ".xml", ".rar", ".zip", ".iso" };
        /// <summary>
        /// 允许上传音频文件格式
        /// </summary>
        static string[] audExt = { ".mp3", ".wav", ".wma", ".aif", ".cda", ".ape", ".aac", ".vqf", ".asf", ".ogg"};
        /// <summary>
        /// 允许上传的视频格式
        /// </summary>
        static string[] vodExt = {".flv", ".asf", ".avi", ".mpg", ".3gp", ".mov", ".wmv", ".rm", ".rmvb" };
        /// <summary>
        /// 上传文件的编码值,这个可以防止上传恶意程序
        /// </summary>
        static int[] fileCode = { 7173, 255216, 255216, 13780, 6677, 8297, 208207, 208207, 208207, 6063 };

        /// <summary>
        /// 限制上传文件大小单位是M
        /// </summary>
        static int fileLimitLen = 10240;

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postFile"></param>
        /// <returns></returns>
        public static BigFileInfo Uploading(HttpPostedFileBase postFile)
        {
            return Uploading(postFile,null);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postFile"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static BigFileInfo Uploading(HttpPostedFileBase postFile, string filename = "")
        {
            BigFileInfo info = new BigFileInfo();
            info.Bytes = postFile.ContentLength / 1024;
            info.Extension = System.IO.Path.GetExtension(postFile.FileName);

            int sindex = postFile.FileName.LastIndexOf('\\') + 1;

            if (sindex > 0)
            {
                int slen = postFile.FileName.Length;
                info.Title = postFile.FileName.Substring(sindex, slen - sindex);
                info.Title = info.Title.Replace(info.Extension, "");
            }
            else
            {
                info.Title = postFile.FileName.Replace(info.Extension, "");
            }

            int len = (postFile.ContentLength / 1024) / 1024;

            if (len > fileLimitLen)
            {
                return null;
            }

            if (IsUploadFile(info.Extension, fileExt))
            {
                string savePath = "";

                if (string.IsNullOrEmpty(filename))
                {
                    filename = Date.GetCuurentUTC().ToString() + (new System.Random()).Next(1000, 9999).ToString();
                }

                string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);

                string savefilename = "";
                string dirPath = "";
                string fullpath = "";
                if (IsUploadFile(info.Extension, imgExt)) 
                { 
                    savePath = "/ALLFile/editor/upload/image/origin";
                    info.filename = filename;
                    info.ymd = ymd;
                    info.Thumb = savePath + "/" + filename + info.Extension;
                    dirPath = FileObject.GetMapPath(savePath);
                    fullpath = dirPath + "/" + filename + info.Extension;
                    info.Path = savePath + "/" + filename + info.Extension;
                    info.dirPath = savePath;
                }
                else if (IsUploadFile(info.Extension, docZipExt)) 
                {
                    savePath = "/ALLFile/editor/upload/file";
                    savefilename = filename;
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    fullpath = dirPath + "/" + savefilename+ info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + savefilename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }
                else if (IsUploadFile(info.Extension, vodExt)) 
                {
                    savePath = "/ALLFile/editor/upload/video"; 
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    savefilename = filename;
                    fullpath = dirPath + "/" + savefilename+ info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + savefilename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }
                else if (IsUploadFile(info.Extension, audExt)) 
                {
                    savePath = "/ALLFile/editor/upload/audio"; 
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    savefilename = filename;
                    fullpath = dirPath + "/" + savefilename + info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + savefilename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }

                //目录不存在，创建目录
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                byte[] buffer = new byte[1024]; //分块容量

                Stream fss = System.IO.File.Create(fullpath);

                System.IO.StreamReader sr = new System.IO.StreamReader(
                                postFile.InputStream, System.Text.Encoding.Default);  //按默认编码读取数据


                System.IO.Stream stream = sr.BaseStream;

                int l;
                do
                {
                    l = stream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                    {
                        fss.Write(buffer, 0, l);
                    }
                }
                while (l > 0);


                fss.Close();

                sr.BaseStream.Close(); //数据流关闭
                sr.Close(); //StreamReader 关闭

                ////视频抓图
                //if (info.Extension == ".flv")
                //{
                //    info.Thumb = "/uploads/image/"+ymd+"/" + filename + ".jpg";
                //    CutVideoImage("/ffmpeg/ffmpeg.exe", info.Path, info.Thumb, "550*450");
                //}

                return info;
            }

            return null;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postFile"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static BigFileInfo MemberUploading(HttpPostedFileBase postFile, string filename = "")
        {
            BigFileInfo info = new BigFileInfo();
            info.Bytes = postFile.ContentLength / 1024;
            info.Extension = System.IO.Path.GetExtension(postFile.FileName);

            int sindex = postFile.FileName.LastIndexOf('\\') + 1;

            if (sindex > 0)
            {
                int slen = postFile.FileName.Length;
                info.Title = postFile.FileName.Substring(sindex, slen - sindex);
                info.Title = info.Title.Replace(info.Extension, "");
            }
            else
            {
                info.Title = postFile.FileName.Replace(info.Extension, "");
            }

            int len = (postFile.ContentLength / 1024) / 1024;

            if (len > fileLimitLen)
            {
                return null;
            }

            if (IsUploadFile(info.Extension, fileExt))
            {
                string savePath = "";

                if (string.IsNullOrEmpty(filename))
                {
                    filename = Date.GetCuurentUTC().ToString() + (new System.Random()).Next(1000, 9999).ToString();
                }

                string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);

                string savefilename = "";
                string dirPath = "";
                string fullpath = "";
                if (IsUploadFile(info.Extension, imgExt))
                {
                    savePath = "/upload/member/image";
                    info.filename = filename;
                    info.ymd = ymd;
                    info.Thumb = savePath + "/" + filename + info.Extension;
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    fullpath = dirPath + "/" + filename + info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + filename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }
                else if (IsUploadFile(info.Extension, docZipExt))
                {
                    savePath = "/upload/member/file";
                    savefilename = filename;
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    fullpath = dirPath + "/" + savefilename + info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + savefilename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }
                else if (IsUploadFile(info.Extension, vodExt))
                {
                    savePath = "/upload/member/video";
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    savefilename = filename;
                    fullpath = dirPath + "/" + savefilename + info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + savefilename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }
                else if (IsUploadFile(info.Extension, audExt))
                {
                    savePath = "/upload/member/audio";
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    savefilename = filename;
                    fullpath = dirPath + "/" + savefilename + info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + savefilename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }

                //目录不存在，创建目录
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                byte[] buffer = new byte[1024]; //分块容量

                Stream fss = System.IO.File.Create(fullpath);

                System.IO.StreamReader sr = new System.IO.StreamReader(
                                postFile.InputStream, System.Text.Encoding.Default);  //按默认编码读取数据


                System.IO.Stream stream = sr.BaseStream;

                int l;
                do
                {
                    l = stream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                    {
                        fss.Write(buffer, 0, l);
                    }
                }
                while (l > 0);


                fss.Close();

                sr.BaseStream.Close(); //数据流关闭
                sr.Close(); //StreamReader 关闭

                ////视频抓图
                //if (info.Extension == ".flv")
                //{
                //    info.Thumb = "/uploads/image/"+ymd+"/" + filename + ".jpg";
                //    CutVideoImage("/ffmpeg/ffmpeg.exe", info.Path, info.Thumb, "550*450");
                //}

                return info;
            }

            return null;
        }
        /// <summary>
        /// 上传图像文件
        /// </summary>
        /// <param name="postFile"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static BigFileInfo PhotoUploading(HttpPostedFileBase postFile, string filename = "")
        {
            BigFileInfo info = new BigFileInfo();
            info.Bytes = postFile.ContentLength / 1024;
            info.Extension = System.IO.Path.GetExtension(postFile.FileName);

            int sindex = postFile.FileName.LastIndexOf('\\') + 1;

            if (sindex > 0)
            {
                int slen = postFile.FileName.Length;
                info.Title = postFile.FileName.Substring(sindex, slen - sindex);
                info.Title = info.Title.Replace(info.Extension, "");
            }
            else
            {
                info.Title = postFile.FileName.Replace(info.Extension, "");
            }

            int len = (postFile.ContentLength / 1024);

            if (len > 1024)
            {
                return null;
            }

            if (IsUploadFile(info.Extension, fileExt))
            {
                string savePath = "";

                if (string.IsNullOrEmpty(filename))
                {
                    filename = Date.GetCuurentUTC().ToString() + (new System.Random()).Next(1000, 9999).ToString();
                }

                string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);

                string dirPath = "";
                string fullpath = "";
                if (IsUploadFile(info.Extension, imgExt))
                {
                    savePath = "/upload/member/image";
                    info.filename = filename;
                    info.ymd = ymd;
                    info.Thumb = savePath + "/" + filename + info.Extension;
                    dirPath = FileObject.GetMapPath(savePath) + "/" + ymd;
                    fullpath = dirPath + "/" + filename + info.Extension;
                    info.Path = savePath + "/" + ymd + "/" + filename + info.Extension;
                    info.dirPath = savePath + "/" + ymd;
                }
                else 
                {
                    return null;
                }

                //目录不存在，创建目录
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                byte[] buffer = new byte[1024]; //分块容量

                Stream fss = System.IO.File.Create(fullpath);

                System.IO.StreamReader sr = new System.IO.StreamReader(
                                postFile.InputStream, System.Text.Encoding.Default);  //按默认编码读取数据


                System.IO.Stream stream = sr.BaseStream;

                int l;
                do
                {
                    l = stream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                    {
                        fss.Write(buffer, 0, l);
                    }
                }
                while (l > 0);


                fss.Close();

                sr.BaseStream.Close(); //数据流关闭
                sr.Close(); //StreamReader 关闭

                ////视频抓图
                //if (info.Extension == ".flv")
                //{
                //    info.Thumb = "/uploads/image/"+ymd+"/" + filename + ".jpg";
                //    CutVideoImage("/ffmpeg/ffmpeg.exe", info.Path, info.Thumb, "550*450");
                //}

                return info;
            }

            return null;
        }
        public static void TheadingPro()
        {
            
        }

        /// <summary>
        /// 判断是否是允许上传的文件
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        static bool IsUploadFile(string ext,string[] exts)
        {
            foreach (string et in exts)
            {
                if (et.ToLower() == ext.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断图片
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsImage(string ext)
        {
            return IsUploadFile(ext, imgExt);
        }

        /// <summary>
        /// 判断允许上传的文件
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsFile(string ext)
        {
            return IsUploadFile(ext, fileExt);
        }
        /// <summary>
        /// 判断音频
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsAudio(string ext)
        {
            return IsUploadFile(ext, audExt);
        }

        /// <summary>
        /// 转化成Flv格式的视频
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="flvPath"></param>
        /// <param name="vodWH"></param>
        /// <param name="imgPath"></param>
        /// <param name="imgWH"></param>
        static void VideoConvertFlv(string ffmpeg, string videoPath, string flvPath, string vodWH)
        {
            try
            {
                string arguments = "-i \"" + videoPath + "\" -y -ab 56 -ar 22050 -b 500 -r 15 -s " + vodWH + " \"" + flvPath + "\"";
                string appName = FileObject.GetMapPath(ffmpeg);
                FileObject.StartApp(appName, arguments);

                System.Threading.Thread.Sleep(5000);
            }
            catch (NullReferenceException)
            { }
        }

        /// <summary>
        /// 从视频获取一张图片
        /// </summary>
        /// <param name="ffmpeg"></param>
        /// <param name="videoPath"></param>
        /// <param name="imgPath"></param>
        /// <param name="imgWH"></param>
        static void CutVideoImage(string ffmpeg, string videoPath, string imgPath, string imgWH)
        {
            try
            {
                string arguments = "-i \"" + FileObject.GetMapPath(videoPath) + "\" -y -f image2 -ss 8 -t 0.001 -s " + imgWH + " \"" + FileObject.GetMapPath(imgPath) + "\"";
                string appName = FileObject.GetMapPath(ffmpeg);
                FileObject.StartApp(appName, arguments);
            }
            catch (NullReferenceException)
            { }
        }


        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param name="postFile"></param>
        /// <returns></returns>
        static int GetFileCode(HttpPostedFile postFile)
        {
            System.IO.BinaryReader r = new System.IO.BinaryReader(postFile.InputStream);
            string fileclass = "";
            byte buffer = 0;

            buffer = r.ReadByte();
            fileclass = buffer.ToString();
            buffer = r.ReadByte();
            fileclass += buffer.ToString();
            r.Close();
            return int.Parse(fileclass);
        }
    }
}
