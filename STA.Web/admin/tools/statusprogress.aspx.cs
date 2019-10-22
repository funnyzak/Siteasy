using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Core.Publish;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class statusprogress : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            isShowSysMenu = false;
        }
    }
}