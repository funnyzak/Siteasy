using System;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Entity.Plus;

namespace STA.Web.Admin.Plus
{
    public partial class voteview : AdminPage
    {
        public string html = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            html = Utils.GetPageContent(new Uri(config.Weburl + baseconfig.Sitepath + "/sta/plus/vote.aspx?display=html&id=" + Request.QueryString["id"]), "utf-8");
        }

    }
}