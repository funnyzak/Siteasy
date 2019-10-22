using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Entity;
using STA.Core;
using STA.Config;

namespace STA.Web.Admin.Tools
{
    public class View : AdminPage
    {
        private string viewname = STARequest.GetQueryString("name");
        private int id = STARequest.GetQueryInt("id", 0);
        private int tid = STARequest.GetQueryInt("tid", 1);

        public View()
        {
            string url = "";
            switch (viewname)
            {
                case "content": url = Urls.Content(id, tid); break;
                case "page": url = Urls.Page(id); break;
                case "channel": url = Urls.Channel(id); break;
                case "specgroup": url = Urls.SpecGroup(id, STARequest.GetInt("group", 0)); break;
            }
            url = url == null || url == "" ? "/" : url;
            Redirect(url);
        }
    }
}
