using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Entity;
using STA.Core;
using STA.Config;
using System.IO;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public class Magupload : System.Web.UI.Page
    {
        public Magupload()
        {
            try
            {
                //<item name="名称" url="" orderid="">
                int uid = STARequest.GetQueryInt("uid", 0);
                MagazineInfo info = Contents.GetMagazine(STARequest.GetInt("id", 0));
                string username = Utils.UrlDecode(STARequest.GetQueryString("username"));
                string password = Utils.UrlDecode(STARequest.GetQueryString("password"));
                if (UserUtils.CheckUserByCache(uid, password, Caches.GetUser(uid, GeneralConfigs.GetConfig().Cacheinterval * 60), 1) && info != null)
                {
                    GeneralConfigInfo config = GeneralConfigs.GetConfig();
                    string savepath = Utils.GetMapPath(BaseConfigs.GetSitePath + config.Attachsavepath + "/magazine/" + info.Id + "/");
                    AttachmentInfo[] attach = ConUtils.SaveRequestFiles(100, "jpg,png,jpeg,gif", 102400, savepath, "", 0, 0, "", "Filedata", config);
                    if (attach != null)
                    {
                        if (attach[0].Noupload == "")
                        {
                            attach[0].Lasteditusername = attach[0].Username = username;
                            attach[0].Lastedituid = attach[0].Uid = uid;
                            attach[0].Attachment = "杂志内容:" + info.Name + "";
                            attach[0].Description = STARequest.GetQueryString("desc").Length > 0 ? Utils.UrlDecode(STARequest.GetQueryString("desc")) : attach[0].Description;
                            attach[0].Id = Contents.AddAttachment(attach[0]);

                            info.Updatetime = DateTime.Now;
                            info.Pages = ConUtils.GetMagazineList(info.Content).Rows.Count + 1;
                            info.Content += string.Format("<item name=\"{0}\" url=\"{1}\" orderid=\"{2}\" attid=\"{3}\"/>\r\n", attach[0].Attachment, attach[0].Filename, 0, attach[0].Id);
                            Contents.EditMagazine(info);

                            ConUtils.InsertLog(2, uid, username, STARequest.GetInt("groupid", 0), Utils.UrlDecode(STARequest.GetString("groupname")), STARequest.GetIP(), "上传杂志内容", "文件ID:" + attach[0].Id.ToString() + ",文件名:" + attach[0].Attachment);
                            HttpContext.Current.Response.Write(attach[0].Id.ToString() + "*sta*" + attach[0].Attachment + "*sta*" + attach[0].Filename);
                            HttpContext.Current.Response.End();
                        }
                        else
                        {
                            HttpContext.Current.Response.Write(attach[0].Noupload);
                            HttpContext.Current.Response.End();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("上传失败.");
                        HttpContext.Current.Response.End();
                    }
                }
                else
                {
                    HttpContext.Current.Response.StatusCode = 177;
                    HttpContext.Current.Response.Write("身份验证失败.");
                    HttpContext.Current.Response.End();
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("上传失败.");
                HttpContext.Current.Response.End();
            }
        }
    }
}
