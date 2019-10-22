using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class imgcut : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {   


        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            isShowSysMenu = false;
            base.OnInit(e);
        }
        #endregion
    }
}