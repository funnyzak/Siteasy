using System;
using System.Web;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Data;
using STA.Core;

namespace STA.Web.UI.VCode
{
    public class VCode : System.Web.UI.Page
    {
        private static object lockhelper = new object();
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string cookiename = STARequest.GetString("cookiename");
            int num = STARequest.GetQueryInt("num", 4);
            string vcode = Rand.Str(num).ToLower();
            if (STARequest.GetQueryInt("live", 1) == 1 && Utils.GetCookie(cookiename) != "")
                vcode = Utils.GetCookie(cookiename);
            Utils.WriteCookie(cookiename, vcode);
            CreateValidateCode(vcode, Color.White);
        }

        private void CreateValidateCode(string chkCode, Color bgcolor)
        {
            //颜色列表，用于验证码、噪线、噪点 
            //Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            Color[] color = { Color.Black };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            Random rnd = new Random();

            Bitmap bmp = new Bitmap(22 * chkCode.Length, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 5; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
            }
            //画噪点 
            //for (int i = 0; i < 100; i++)
            //{
            //    int x = rnd.Next(bmp.Width);
            //    int y = rnd.Next(bmp.Height);
            //    Color clr = color[rnd.Next(color.Length)];
            //    bmp.SetPixel(x, y, clr);
            //}
            //清除该页输出缓存，设置该页无缓存 
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                Response.ClearContent();
                Response.ContentType = "image/Png";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}