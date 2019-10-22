using System;
using System.Data;
using System.Web;
using System.Text;
using System.Xml;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Data;
using STA.Config;
using STA.Core;

namespace STA.Web.UI
{
    public class Download : System.Web.UI.Page
    {
        public Download()
        {
            if (GeneralConfigs.GetConfig().Closed == 1)
            {
                HttpContext.Current.Response.Write(GeneralConfigs.GetConfig().Closedreason);
                return;
            }

            if (STARequest.IsGet())
            {
                int cid = STARequest.GetInt("cid", 0);
                string durl = Utils.UrlDecode(STARequest.GetString("durl"));
                int aid = STARequest.GetQueryInt("aid", 0);
                string filename = Utils.UrlDecode(STARequest.GetString("name")).Trim();

                if (cid > 0)
                    Contents.UpdateSoftDownloadCount(cid);

                if (aid > 0 && durl == "")
                {
                    AttachmentInfo att = Contents.GetAttachment(aid);
                    if (att != null && FileUtil.FileExists(Utils.GetMapPath(att.Filename)))
                    {
                        if (filename == "") filename = att.Attachment;
                        if (filename == "") filename = Rand.Str(12);
                        filename += "." + att.Fileext;
                        Utils.ResponseFile(Utils.GetMapPath(att.Filename), filename, Globals.GetContentType(att.Fileext));
                    }
                    else
                    {
                        //HttpContext.Current.Response.StatusCode = 403;
                        HttpContext.Current.Response.Write("file not found");
                    }
                }
                else if (durl != "")
                {
                    HttpContext.Current.Response.Redirect(durl);
                }
                else
                {
                    HttpContext.Current.Response.Write("access error,please contact:" + GeneralConfigs.GetConfig().Adminmail);
                    //HttpContext.Current.Response.StatusCode = 403;
                }

            }
        }



    }
}