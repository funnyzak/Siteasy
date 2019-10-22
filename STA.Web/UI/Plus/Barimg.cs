using System;
using System.Text;
using System.Web;
using System.Drawing;
using System.Net;
using System.IO;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Common.ImgHelper;
using System.Drawing.Imaging;

namespace STA.Web.Plus
{
    /// <summary>
    /// 生成条形码
    /// 参数：width宽  height高 format条码类型 txt内容 
    /// 其中：QR_CODE 附加 icon图标(/file/icon.jpg)  icon_borderWidth图标边框 
    /// action（view查看 down下载） savename 保存名称
    /// </summary>
    public class Barimg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string barformat = STARequest.GetString("format");
            string action = STARequest.GetString("action");
            string savename = Utils.UrlDecode(STARequest.GetString("savename"));
            string icon = Utils.UrlDecode(STARequest.GetString("icon"));
            int width = STARequest.GetInt("width", 200);
            int height = STARequest.GetInt("height", 200);
            width = width <= 20 ? 200 : width;
            height = height <= 20 ? 200 : height;
            savename = savename == "" ? Rand.Str(12) : savename;
            action = action == "" ? "view" : action;

            if (barformat == "")
                barformat = "QR_CODE";

            if (!(STARequest.GetUrlReferrer() != "" && STARequest.GetUrlReferrer().IndexOf(GeneralConfigs.GetConfig().Weburl) < 0))
            {
                Bitmap bmap = null;
                string msg = BarQrCode.CodeBulid(Utils.UrlDecode(STARequest.GetString("txt")), width, height, barformat, ref bmap);
                if (msg == "" && bmap != null)
                {
                    string tempbarpath = BaseConfigs.GetSitePath + "/sta/cache/temp/3/";

                    if (!icon.StartsWith("http") && Utils.IsImgFilename(icon) && barformat == "QR_CODE")
                    {
                        if (icon != "" && FileUtil.FileExists(Utils.GetMapPath(icon)))
                        {
                            int icon_W = (int)width / 4;
                            //FileUtil.CreateFolder(Utils.GetMapPath(tempbarpath) + "icon/");
                            FileUtil.CreateFolder(Utils.GetMapPath(tempbarpath) + "code/");

                            //string tempbarpath_icon = tempbarpath + "icon/" + Rand.Str(30) + "." + Utils.GetFileExtName(icon);
                            Bitmap bm_baricon = new Bitmap(Bitmap.FromFile(Utils.GetMapPath(icon)), icon_W, icon_W);
                            //生成图标正方形
                            //Thumbnail.MakeSquareImage(Bitmap.FromFile(Utils.GetMapPath(icon)), Utils.GetMapPath(tempbarpath_icon), icon_W);

                            //正方形加白边
                            //int icon_borderWidth = STARequest.GetInt("icon_borderWidth", 3); //边框宽度
                            //icon_borderWidth = icon_borderWidth <= 0 ? 0 : icon_borderWidth;
                            //icon_W += icon_borderWidth * 2;

                            //Bitmap bm_icon = new Bitmap(icon_W, icon_W);
                            //Graphics gicon = Graphics.FromImage(bm_icon);
                            //gicon.DrawRectangle(new Pen(Color.Transparent, 3), 0, 0, icon_W, icon_W);
                            //gicon.FillRectangle((Brush)Brushes.Transparent, 0, 0, icon_W, icon_W);
                            //gicon.DrawImage(Bitmap.FromFile(Utils.GetMapPath(tempbarpath_icon)), icon_borderWidth, icon_borderWidth);
                            //gicon.Dispose();

                            //把正方形加到二维码中间
                            int xpos = (width - icon_W) / 2;
                            Graphics gb = Graphics.FromImage(bmap);
                            gb.DrawImage(bm_baricon, new Rectangle(xpos, xpos, icon_W, icon_W), new Rectangle(0, 0, icon_W, icon_W), GraphicsUnit.Pixel);
                            gb.Dispose();
                        }
                    }
                    if (action == "down")
                    {
                        string tempbarpath_code = tempbarpath + "code/" + savename + ".png";
                        bmap.Save(Utils.GetMapPath(tempbarpath_code));
                        Utils.ResponseFile(Utils.GetMapPath(tempbarpath_code), savename + ".png", "image/png");
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        bmap.Save(ms, ImageFormat.Png);
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.ContentType = "image/Png";
                        HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                    }
                }
            }

        }

    }
}