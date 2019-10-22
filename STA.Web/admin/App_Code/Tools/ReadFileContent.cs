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
    public class ReadFileContent : AdminPage
    {
        public ReadFileContent()
        {
            try
            {
                string url = Utils.UrlDecode(STARequest.GetQueryString("url"));
                if (!FileUtil.FileExists(Utils.GetMapPath(url)))
                {
                    HttpContext.Current.Response.Write("加载失败.");
                    HttpContext.Current.Response.End();
                }
                string content = FileUtil.ReadFile(Utils.GetMapPath(url));
               // content = string.Format("<textarea></textarea>",content);
                HttpContext.Current.Response.Write(content);
                HttpContext.Current.Response.End();
            }
            catch
            {
                HttpContext.Current.Response.Write("加载失败.");
                HttpContext.Current.Response.End();
            }
        }
    }
}
