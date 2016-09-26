using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZbClassLibrary
{
    /// <summary>
    /// 条码生成器
    ///  Barcode barCode = new Barcode();
    ///  barCode.Generate("692645690151").Save("barcode.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
    /// </summary>
    public class Barcode
    {
        //原始数据
        private int[] _Code;
        //条形码图案
        private Bitmap _Result;
        //第n个竖线单位 竖线单位数=6x7+6x7+3+3+5=95
        private int _X;
        //背景色和前景色
        Brush[] _Cors = { Brushes.White, Brushes.Black };
        //分辨率 默认为1单位像素
        private int _Zoom = 2;

        public Bitmap Generate(string code)
        {
            //初始化数据
            _Code = new int[12];
            _Result = new Bitmap(105 * _Zoom, 55 * _Zoom);
            _X = 0;
            if (!RegexValidate("^[0-9]{12}$", code))
            {
                throw new Exception("数据格式错误，必须为12位的数字组合！");
            }
            else
            {
                for (int i = 0; i < _Code.Length; i++)
                {
                    _Code[i] = int.Parse(code[i].ToString());
                }
            }
            Graphics g = Graphics.FromImage(_Result);
            g.FillRectangle(_Cors[0], 0, 0, _Result.Width, _Result.Height);
            for (int i = 0; i <= 11; i++)
            {
                step(i);
            }
            //清空数据
            _Code = new int[12];
            //result = new Bitmap(95, 44);
            _X = 0;
            return _Result;
        }

        private static bool RegexValidate(string regexString, string validateString)
        {
            Regex regex = new Regex(regexString);
            return regex.IsMatch(validateString.Trim());
        }

        //按步骤写入单个码，全局变量code 索引 0~11
        //索引为0时 是模式匹配，所以只会写入起始符
        //索引为6时 左数据结束 会写入中间的分隔符
        //索引为11时为最后一个 会写入效验码和结束符
        private void step(int codeIndx)
        {
            Graphics g = Graphics.FromImage(_Result);
            g.TranslateTransform(7 * _Zoom, 0);
            if (codeIndx == 0)
            {
                //导入值字符
                g.DrawString(_Code[codeIndx].ToString(), new Font(FontFamily.GenericSerif, 7f * _Zoom), _Cors[1], new PointF(_X - 7 * _Zoom, _Result.Height * 0.76f));
                //起始符
                g.FillRectangle(_Cors[1], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[0], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[1], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
            }
            else
            {
                //底端字符
                g.DrawString(_Code[codeIndx].ToString(), new Font(FontFamily.GenericSerif, 7f * _Zoom), _Cors[1], new PointF(_X, _Result.Height * 0.76f));

                //提取码
                byte csh_1 = 0x40, csh_2 = 0x20, csh_3 = 0x10, csh_4 = 0x8, csh_5 = 0x4, csh_6 = 0x2, csh_7 = 0x1;

                //数据位
                byte coded = getCodeVal(_Code[codeIndx], codeIndx < 7 ? getCp(codeIndx - 1) : codeType.C);
                g.FillRectangle((coded & csh_1) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                g.FillRectangle((coded & csh_2) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                g.FillRectangle((coded & csh_3) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                g.FillRectangle((coded & csh_4) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                g.FillRectangle((coded & csh_5) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                g.FillRectangle((coded & csh_6) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                g.FillRectangle((coded & csh_7) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;

                if (codeIndx == 11)
                {
                    //效验码
                    int C1 = _Code[0] + _Code[2] + _Code[4] + _Code[6] + _Code[8] + _Code[10];
                    int C2 = (_Code[1] + _Code[3] + _Code[5] + _Code[7] + _Code[9] + _Code[11]) * 3;
                    int CC = C1 + C2;
                    int C = CC % 10;
                    C = 10 - C;
                    if (C == 10)
                        C = 0;
                    coded = getCodeVal(C, codeType.C);
                    //效验字符
                    g.DrawString(C.ToString(), new Font(FontFamily.GenericSerif, 7f * _Zoom), _Cors[1], new PointF(_X, _Result.Height * 0.76f));

                    g.FillRectangle((coded & csh_1) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                    g.FillRectangle((coded & csh_2) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                    g.FillRectangle((coded & csh_3) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                    g.FillRectangle((coded & csh_4) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                    g.FillRectangle((coded & csh_5) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                    g.FillRectangle((coded & csh_6) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                    g.FillRectangle((coded & csh_7) == 0 ? _Cors[0] : _Cors[1], _X, 0, _Zoom, _Result.Height * 0.76f); _X += _Zoom;
                }
            }
            if (codeIndx == 6)
            {
                //分隔符
                g.FillRectangle(_Cors[0], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[1], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[0], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[1], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[0], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
            }
            else if (codeIndx == 11)
            {
                //结束符
                g.FillRectangle(_Cors[1], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[0], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
                g.FillRectangle(_Cors[1], _X, 0, _Zoom, _Result.Height); _X += _Zoom;
            }
        }


        private codeType getCp(int left)//left 0~5
        {
            codeType[][] cps = new codeType[10][];
            cps[0] = new codeType[] { codeType.A, codeType.A, codeType.A, codeType.A, codeType.A, codeType.A };
            cps[1] = new codeType[] { codeType.A, codeType.A, codeType.B, codeType.A, codeType.B, codeType.B };
            cps[2] = new codeType[] { codeType.A, codeType.A, codeType.B, codeType.B, codeType.A, codeType.B };
            cps[3] = new codeType[] { codeType.A, codeType.A, codeType.B, codeType.B, codeType.B, codeType.A };
            cps[4] = new codeType[] { codeType.A, codeType.B, codeType.A, codeType.A, codeType.B, codeType.B };

            cps[5] = new codeType[] { codeType.A, codeType.B, codeType.B, codeType.A, codeType.A, codeType.B };
            cps[6] = new codeType[] { codeType.A, codeType.B, codeType.B, codeType.B, codeType.A, codeType.A };
            cps[7] = new codeType[] { codeType.A, codeType.B, codeType.A, codeType.B, codeType.A, codeType.B };
            cps[8] = new codeType[] { codeType.A, codeType.B, codeType.A, codeType.B, codeType.B, codeType.A };
            cps[9] = new codeType[] { codeType.A, codeType.B, codeType.B, codeType.A, codeType.B, codeType.A };

            int first = int.Parse(_Code[0].ToString());
            return cps[first][left];
        }

        private byte getCodeVal(int val, codeType cp)
        {
            byte[,] values ={
                                {Convert.ToByte("0001101", 2),Convert.ToByte("0100111", 2),Convert.ToByte("1110010", 2)},
                                {Convert.ToByte("0011001", 2),Convert.ToByte("0110011", 2),Convert.ToByte("1100110", 2)},
                                {Convert.ToByte("0010011", 2),Convert.ToByte("0011011", 2),Convert.ToByte("1101100", 2)},
                                {Convert.ToByte("0111101", 2),Convert.ToByte("0100001", 2),Convert.ToByte("1000010", 2)},
                                {Convert.ToByte("0100011", 2),Convert.ToByte("0011101", 2),Convert.ToByte("1011100", 2)},
                                {Convert.ToByte("0110001", 2),Convert.ToByte("0111001", 2),Convert.ToByte("1001110", 2)},
                                {Convert.ToByte("0101111", 2),Convert.ToByte("0000101", 2),Convert.ToByte("1010000", 2)},
                                {Convert.ToByte("0111011", 2),Convert.ToByte("0010001", 2),Convert.ToByte("1000100", 2)},
                                {Convert.ToByte("0110111", 2),Convert.ToByte("0001001", 2),Convert.ToByte("1001000", 2)},
                                {Convert.ToByte("0001011", 2),Convert.ToByte("0010111", 2),Convert.ToByte("1110100", 2)}
                            };
            switch (cp)
            {
                case codeType.A:
                    return values[val, 0];
                case codeType.B:
                    return values[val, 1];
                case codeType.C:
                    return values[val, 2];
                default:
                    return 0;
            }
        }
        public enum codeType
        {
            A, B, C
        }
    }
}
