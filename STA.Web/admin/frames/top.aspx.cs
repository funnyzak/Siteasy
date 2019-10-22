using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Frame
{
    public partial class top : AdminPage
    {
        public string menuStr = "";
        public string menuIds = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            MenuJson(Menus.GetMenuTable(STARequest.GetQueryInt("type", 1), PageType.菜单页));
        }

        private void MenuJson(DataTable menudt)
        {
            menuIds = "," + ConUtils.DataTableToString(Menus.GetMenuRelatetionsByGroupId(admingroupid), 0, ",") + ",";
            foreach (DataRow topdr in menudt.Select("parentid=0"))
            {
                if (menuIds.IndexOf("," + topdr["id"].ToString() + ",") < 0 && !IsFounder(userid)) continue;
                menuStr += Globals.GetMenuItem(topdr) + ",";
            }
            if (menuStr.EndsWith(",")) menuStr = menuStr.Substring(0, menuStr.Length - 1);

        }
    }
}