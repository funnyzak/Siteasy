using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class menumanage : AdminPage
    {
        public string menuStr = "";
        public string iconStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            {
                rblpagetype.AddTableData(ConUtils.GetEnumTable(typeof(PageType)), "name", "id", null);
                rblpagetype.SelectedValue = "1";
                SetMenuJson(Menus.GetMenuTable(STARequest.GetQueryInt("type", 1)));
                SetIconJson();
            }
        }

        private void SetMenuJson(DataTable dt)
        {
            menuStr += "{id:0,Pid:-1,name:\"功能菜单\",orderid:10000,icon:\"\"},";
            foreach (DataRow dr in dt.Rows)
            {
                string icon = dr["icon"].ToString().Trim();
                if (icon != "")
                    icon = "../images/icon/" + icon;
                PageType pt = (PageType)TypeParse.StrToInt(dr["pagetype"]);
                string name = dr["name"].ToString().Trim();
                if (pt == PageType.对话框 || pt == PageType.流程页)
                {
                    name += "[" + pt.ToString().Substring(0, 1) + "]";
                }
                menuStr += "{" + string.Format("id:{0},pId:{1},name:\"{2}\",url:\"{3}\",target:\"{4}\",icon:\"{5}\",system:{6},orderid:{7},type:{8},pagetype:{9},identify:\"{10}\"", dr["id"], dr["parentid"], name, dr["url"], dr["target"].ToString().Trim(), icon, dr["system"], dr["orderid"], dr["type"], dr["pagetype"], dr["identify"]) + "},";
            }
            if (menuStr.EndsWith(",")) menuStr = menuStr.Substring(0, menuStr.Length - 1);
        }

        private void SetIconJson()
        {
            foreach (FileItem item in FileUtil.GetFiles(Utils.GetMapPath("../images/icon/"), "png,gif,jpeg,jpg,bmp"))
            {
                iconStr += "\"" + item.Name + "\",";
            }
            if (iconStr.EndsWith(",")) iconStr = iconStr.Substring(0, iconStr.Length - 1);
        }

    }
}