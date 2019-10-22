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
    public class ValidateCode : System.Web.UI.Page
    {
        private static object lockhelper = new object();
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string cookiename = STARequest.GetString("cookiename");
            string vcode = Rand.Number(4);
            if (STARequest.GetQueryInt("live", 1) == 1 && Utils.GetCookie(cookiename) != "")
                vcode = Utils.GetCookie(cookiename);
            Utils.WriteCookie(cookiename, vcode);
            CreateValidateCode(vcode, Color.White);
        }

        private void CreateValidateCode(string vcode, Color bgcolor)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling((double)(vcode.Length * 12.0)), 0x1a);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.Clear(bgcolor);
            Font font = new Font("宋体", 14f);
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Black, 1.4f, false);
            graphics.DrawString(vcode, font, brush, (float)2f, (float)2f);
            brush.Dispose();
            graphics.Dispose();
            HttpContext.Current.Response.ContentType = "image/Gif";
            image.Save(this.Response.OutputStream, ImageFormat.Gif);
        }
    }
}