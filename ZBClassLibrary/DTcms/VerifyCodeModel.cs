using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZbClassLibrary.DTcms
{
    /// <summary>
    ///验证码配置信息
    /// </summary>
    [Serializable]
    public class VerifyCodeModel
    {
        public VerifyCodeModel()
        { }
        public int Width { set; get; }
        public int Height { set; get; }
        public int FontSize { set; get; }
        public int Length { set; get; }
        public int NoiseCount { set; get; }
        public int LineCount { set; get; }
    }
}
