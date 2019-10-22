using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using ZXing;
using System.Drawing;

namespace STA.Web.Admin.Tools
{
    public partial class qrcodemake : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            Response.ContentType = "image/png";

            Bitmap bmap = null;
            string msg = BarQrCode.CodeBulid(Utils.UrlDecode(STARequest.GetString("txt")), STARequest.GetInt("width", 200), STARequest.GetInt("height", 200), STARequest.GetString("format"), ref bmap);

            if (msg == "" && bmap != null)
            {
                bmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                (Bitmap.FromFile(Utils.GetMapPath("../images/nopic.png"))).Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            }
            Response.End();
        }
    }
}