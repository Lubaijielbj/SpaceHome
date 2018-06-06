using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.Common
{
    public class CheckCode
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="codeLenght">需要生成的验证码长度</param>
        /// <returns></returns>
        public static string CreateCheckCode(int codeLenght)
        {
            string checkCode = string.Empty;
            char code;

            Random random = new Random();

            for (int i = 0; i < codeLenght; i++)
            {
                int num = random.Next();
                if (num % 2 == 0)
                {
                    code = (char)('0' + (char)(num % 10));
                }
                else
                {
                    code = (char)('A' + (char)(num % 26));
                }

                checkCode += code.ToString();
            }

            return checkCode;
        }

        public static byte[] CheckCodeImg(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == string.Empty)
                return null;

            Bitmap image = new Bitmap(80, 30);
            Graphics g = Graphics.FromImage(image);

            Random random = new Random();

            try
            {
                g.Clear(Color.White);

                //画线
                for (int i = 0; i < 6; i++)
                {
                    int x1 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int x2 = random.Next(image.Width);
                    int y2 = random.Next(image.Height);

                    Pen p = new Pen(Color.FromArgb(random.Next()), random.Next(1, 4));
                    g.DrawLine(p, x1, y1, x2, y2);
                }

                //写验证码
                Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkCyan, 1.2f, true);
                g.DrawString(checkCode, new Font("Arial", 15), b, 5, 5);

                //画噪点
                for (int i = 0; i < 200; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画边框
                g.DrawRectangle(new Pen(Color.Silver, 1.0f), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                return ms.ToArray();
            }
            finally
            {
                image.Dispose();
                g.Dispose();
            }
        }
    }
}
