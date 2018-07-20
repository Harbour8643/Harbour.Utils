using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Harbour.Utils
{
    /// <summary>
    /// 验证码类
    /// </summary>
    public class VerifyCodeHelper
    {
        #region setting
        /// <summary>
        /// //验证码长度
        /// </summary>
        public int Length { get; set; } = 4;
        /// <summary>
        /// 验证码字符串
        /// </summary>
        public string VerifyCodeText { get; set; }
        /// <summary>
        /// 是否加入小写字母
        /// </summary>
        public bool AddLowerAlphabet { get; set; } = true;
        /// <summary>
        /// 是否加入大写字母
        /// </summary>
        public bool AddUpperAlphabet { get; set; } = true;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; } = 16;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;
        /// <summary>
        /// 随机码的旋转角度
        /// </summary>
        public int RandomAngle { get; set; } = 40;

        /// <summary>
        /// 图片宽度
        /// </summary>
        private int With
        {
            get
            {
                return this.VerifyCodeText.Length * (FontSize + 2);
            }
        }
        /// <summary>
        /// 图片高度
        /// </summary>
        private int Height
        {
            get
            {
                return Convert.ToInt32((60.0 / 100) * FontSize + FontSize);
            }
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public VerifyCodeHelper()
        {
            this.GetVerifyCodeText();
        }

        /// <summary>
        /// 得到验证码图片
        /// </summary>
        public Bitmap GetVerifyCodeImage()
        {
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman" };
            string codeText = this.VerifyCodeText;
            //创建绘图
            Bitmap result = new Bitmap(With, Height);
            using (Graphics objGraphics = Graphics.FromImage(result))
            {
                objGraphics.SmoothingMode = SmoothingMode.HighQuality;
                //清除整个绘图面并以指定背景色填充
                objGraphics.Clear(this.BackgroundColor);
                Random rnd = new Random();
                //画噪线 
                for (int i = 0; i < 3; i++)
                {
                    int x1 = rnd.Next(With);
                    int y1 = rnd.Next(Height);
                    int x2 = rnd.Next(With);
                    int y2 = rnd.Next(Height);
                    Color clr = color[rnd.Next(color.Length)];
                    objGraphics.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }
                //画验证码字符串 
                for (int i = 0; i < codeText.Length; i++)
                {
                    string fnt = font[rnd.Next(font.Length)];
                    Font ft = new Font(fnt, this.FontSize);
                    Color clr = color[rnd.Next(color.Length)];
                    float angle = rnd.Next(-this.RandomAngle, this.RandomAngle);
                    objGraphics.DrawString(codeText[i].ToString(), ft, new SolidBrush(clr), (float)i * this.FontSize + 2, (float)2);
                }
            }
            return result;
        }

        /// <summary>
        /// 绘制验证码
        /// </summary>
        public byte[] GetVerifyCodeImageByte()
        {
            using (Bitmap objBitmap = this.GetVerifyCodeImage())
            {
                if (objBitmap != null)
                {
                    using (MemoryStream objMS = new MemoryStream())
                    {
                        objBitmap.Save(objMS, ImageFormat.Jpeg);
                        return objMS.ToArray();
                    }
                }
                else
                {
                    return new byte[0];
                }
            }
        }


        /// <summary>
        /// 得到验证码字符串
        /// </summary>
        private void GetVerifyCodeText()
        {
            //没有外部输入验证码时随机生成
            if (String.IsNullOrEmpty(this.VerifyCodeText))
            {
                StringBuilder objStringBuilder = new StringBuilder();
                //加入数字1-9
                for (int i = 1; i <= 9; i++)
                {
                    objStringBuilder.Append(i.ToString());
                }
                //加入大写字母A-Z，不包括O
                if (this.AddUpperAlphabet)
                {
                    char temp = ' ';
                    for (int i = 0; i < 26; i++)
                    {
                        temp = Convert.ToChar(i + 65);
                        //如果生成的字母不是'O'
                        if (!temp.Equals('O'))
                            objStringBuilder.Append(temp);
                    }
                }
                //加入小写字母a-z，不包括o
                if (this.AddLowerAlphabet)
                {
                    char temp = ' ';
                    for (int i = 0; i < 26; i++)
                    {
                        temp = Convert.ToChar(i + 97);
                        //如果生成的字母不是'o'
                        if (!temp.Equals('o'))
                            objStringBuilder.Append(temp);
                    }
                }
                Random objRandom = new Random();
                //生成验证码字符串
                int index = 0;
                for (int i = 0; i < Length; i++)
                {
                    index = objRandom.Next(0, objStringBuilder.Length);
                    this.VerifyCodeText += objStringBuilder[index];
                    objStringBuilder.Remove(index, 1);
                }
            }
        }
    }

}