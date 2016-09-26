using System;
using System.Linq;
using System.Xml.Linq;

namespace ZbClassLibrary
{
    public class VerifyCodeConfig
    {
        /// <summary>
        /// 更新验证码规格参数
        /// </summary>
        /// <param name="codeSetting"></param>
        /// <returns></returns>
        public static bool UpdateCodeSetting(CodeSetting codeSetting)
        {
            string path = Appsettings.InitConfig;
            XElement xel = XElement.Load(path);
            var elements = xel.Descendants("VerifyCode").Select(p => p);
            if (elements != null)
            {
                elements.ToList().ForEach(x =>
                {
                    x.SetAttributeValue("width", codeSetting.Width);
                    x.SetAttributeValue("height", codeSetting.Height);
                    x.SetAttributeValue("framecount", codeSetting.FrameCount);
                    x.SetAttributeValue("delay", codeSetting.Delay);
                    x.SetAttributeValue("noisecount", codeSetting.NoiseCount);
                    x.SetAttributeValue("linecount", codeSetting.LineCount);
                });
            }
            try
            {
                xel.Save(path);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取验证码规格参数
        /// </summary>
        /// <returns></returns>
        public static CodeSetting GetCodeSetting()
        {
            return GetCodeSetting(Appsettings.InitConfig);
        }

        /// <summary>
        /// 获取验证码规格参数
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <returns>获取验证码规格参数</returns>
        private static CodeSetting GetCodeSetting(string path)
        {
            var result = XElement.Load(path).Descendants("VerifyCode").Select(p => new { 
                width = p.Attribute("width").Value, 
                height = p.Attribute("height").Value, 
                framecount = p.Attribute("framecount").Value, 
                delay = p.Attribute("delay").Value, 
                noisecount = p.Attribute("noisecount").Value, 
                linecount = p.Attribute("linecount").Value 
            }).Single();
            if (result != null)
            {
                return new CodeSetting { 
                    Width = result.width.ToInt32(), 
                    Height = result.height.ToInt32(), 
                    FrameCount = result.framecount.ToInt32(), 
                    Delay = result.delay.ToInt32(), 
                    NoiseCount = result.noisecount.ToInt32(), 
                    LineCount = result.linecount.ToInt32() 
                };
            }
            return new CodeSetting { 
                Width = 105, 
                Height = 30, 
                FrameCount = 4, 
                Delay = 900, 
                NoiseCount = 100, 
                LineCount = 6 
            };
        }
    }
}
