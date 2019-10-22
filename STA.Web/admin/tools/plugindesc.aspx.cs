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
    public partial class plugindesc : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            PluginInfo info = Plugins.GetPlugin(STARequest.GetInt("id", 0));

            if (info == null) return;

            this.Page.Title = "扩展：" + info.Name + " 使用说明";
            pdesc.InnerHtml = info.Description;
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