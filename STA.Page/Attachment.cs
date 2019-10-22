using System;
using System.Data;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;
using System.Web;

namespace STA.Page
{
    public class Attachment : PageBase
    {
        public int attid = STARequest.GetInt("attid", -1);
        public int conid = STARequest.GetInt("conid", -1);
        public string downurl = STARequest.GetString("downurl");
        string attname = Utils.UrlDecode(STARequest.GetString("attname")).Trim();
        public AttachmentInfo att;

        protected override void PageShow()
        {
            if (conid > 0)
                Contents.UpdateSoftDownloadCount(conid);

            if (attid > 0)
            {
                att = Contents.GetAttachment(attid);
                if (att != null && FileUtil.FileExists(Utils.GetMapPath(sitedir + att.Filename)))
                {
                    if (attname == "") attname = att.Attachment;
                    if (attname == "") attname = Rand.Str(12);
                    attname += "." + att.Fileext;

                    att.Downloads += 1;
                    Contents.EditAttachment(att);

                    Utils.ResponseFile(Utils.GetMapPath(sitedir + att.Filename), attname, Globals.GetContentType(att.Fileext));
                }
                else
                {
                    msgtext = "1";
                }
            }
            else if (downurl != "")
            {
                Redirect(downurl);
            }
            else
            {
                Redirect(sitedir + "/");
            }
        }
    }
}
