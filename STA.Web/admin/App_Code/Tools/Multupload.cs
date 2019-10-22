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
    public class Multupload : System.Web.UI.Page
    {
        public Multupload()
        {
            try
            {
                int uid = STARequest.GetQueryInt("uid", 0);
                string username = Utils.UrlDecode(STARequest.GetQueryString("username"));
                string password = Utils.UrlDecode(STARequest.GetQueryString("password"));
                if (UserUtils.CheckUserByCache(uid, password, Caches.GetUser(uid, GeneralConfigs.GetConfig().Cacheinterval * 60), 1))
                {
                    GeneralConfigInfo config = GeneralConfigs.GetConfig();
                    int overfile = 0;
                    int water = config.Waterposition;
                    string savepath = "";
                    if (STARequest.GetQueryString("over") == "o1")
                        overfile = 1;
                    if (STARequest.GetQueryString("nway") == "name0")
                        config.Attachnameway = 0;
                    if (STARequest.GetQueryString("water") == "w0")
                        water = 0;
                    if (STARequest.GetQueryString("path").Length > 0)
                        savepath = Utils.GetMapPath(Utils.UrlDecode(STARequest.GetQueryString("path")));
                    AttachmentInfo[] attach = ConUtils.SaveRequestFiles(100, "*", 102400, savepath, "", overfile, water,"", "Filedata", config);
                    if (attach != null)
                    {
                        if (attach[0].Noupload == "")
                        {
                            attach[0].Lasteditusername = attach[0].Username = username;
                            attach[0].Lastedituid = attach[0].Uid = uid;
                            attach[0].Attachment = STARequest.GetQueryString("name").Length > 0 ? Utils.UrlDecode(STARequest.GetQueryString("name")) : attach[0].Attachment;
                            attach[0].Description = STARequest.GetQueryString("desc").Length > 0 ? Utils.UrlDecode(STARequest.GetQueryString("desc")) : attach[0].Description;
                            attach[0].Id = Contents.AddAttachment(attach[0]);
                            ConUtils.InsertLog(2, uid, username, STARequest.GetInt("groupid", 0), Utils.UrlDecode(STARequest.GetString("groupname")), STARequest.GetIP(), "上传文件", "文件ID:" + attach[0].Id.ToString() + ",文件名:" + attach[0].Attachment);
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
