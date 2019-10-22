using System;
using System.Xml;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin.Frame
{
    public partial class left : AdminPage
    {
        public string menuStr = "";
        public string menuIds = "";
        public int id = STARequest.GetQueryInt("id", 1);
        private DataTable newdt;
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuJson(Menus.GetMenuTable(STARequest.GetQueryInt("type", 1), PageType.菜单页));
        }

        private void MenuJson(DataTable menudt)
        {
            DataRow[] topdrs = menudt.Select("id=" + id.ToString());
            if (topdrs.Length <= 0) return;

            menuIds = "," + ConUtils.DataTableToString(Menus.GetMenuRelatetionsByGroupId(admingroupid), 0, ",") + ",";
            newdt = menudt.Clone();
            newdt.Clear();
            GetDataTableByTop(topdrs[0], menudt);
            SetMenuJson(newdt);
            if (menuStr.EndsWith(",")) menuStr = menuStr.Substring(0, menuStr.Length - 1);

        }

        private void SetMenuJson(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 1) return;

            menuStr += "[";
            foreach (DataRow dr in dt.Rows)
            {
                if (menuIds.IndexOf("," + dr["id"].ToString() + ",") < 0 && !IsFounder(userid)) continue;

                string url = dr["url"].ToString().Trim();
                if (url.StartsWith("$"))
                {
                    menuStr += LoadDynamicItems(url, dr["parentid"].ToString());
                }
                else
                {
                    menuStr += Globals.GetMenuItem(dr) + ",";
                }
            }
            if (menuStr.EndsWith(",")) menuStr = menuStr.Substring(0, menuStr.Length - 1);
            menuStr += "]";
        }

        private void GetDataTableByTop(DataRow ddr, DataTable menudt)
        {
            DataRow[] ddrs = menudt.Select("parentid=" + ddr["id"].ToString());
            foreach (DataRow dr in ddrs)
            {
                newdt.Rows.Add(dr.ItemArray);
                GetDataTableByTop(dr, menudt);
            }
        }

        private string LoadDynamicItems(string name, string pId)
        {
            string ret = string.Empty;
            switch (name)
            {
                case "$模型文档管理":
                    ret = GetModelItems(pId);
                    break;
                case "$模型文档添加":
                    ret = GetModelAddItems(pId);
                    break;
                case "$扩展数据管理":
                    ret = GetExtItems(pId);
                    break;
                default: break;
            }
            return ret;
        }

        private string GetModelItems(string pId)
        {
            string ret = "";
            foreach (DataRow dr in Contents.GetContypeDataTable().Rows)
            {
                int id = TypeParse.StrToInt(dr["id"]);
                int open = TypeParse.StrToInt(dr["open"]);
                if (open == 0) continue;
                string listmode = dr["bglistmod"].ToString();
                if (listmode.Trim() == "") listmode = "global_contentlist.aspx";
                ret += "{" + string.Format("id:{0}, pId:{1}, name:\"{2}\",url:\"{3}\",target:\"{4}\",icon:\"{5}\"", Rand.Number(7),
     pId, dr["name"].ToString(), "global/" + listmode + "?type=" + id.ToString(), "main", "") + "},";
            }
            return ret;
        }

        private string GetModelAddItems(string pId)
        {
            string ret = "";
            foreach (DataRow dr in Contents.GetContypeDataTable().Rows)
            {
                int id = TypeParse.StrToInt(dr["id"]);
                int open = TypeParse.StrToInt(dr["open"]);
                if (open == 0) continue;

                string addmod = dr["bgaddmod"].ToString();
                if (addmod.Trim() == "") addmod = "global_contentadd.aspx";

                ret += "{" + string.Format("id:{0}, pId:{1}, name:\"{2}\",url:\"{3}\",target:\"{4}\",icon:\"{5}\"", Rand.Number(7),
     pId, dr["name"].ToString(), "global/" + addmod + "?type=" + id.ToString(), "main", "") + "},";
            }
            return ret;
        }

        private string GetExtItems(string pId)
        {
            string ret = "";
            foreach (DataRow dr in Globals.PluginMenu(Plugins.GetPluginTable(-1), pId).Rows)
            {
                ret += "{" + string.Format("id:{0}, pId:{1}, name:\"{2}\",url:\"{3}\",target:\"{4}\"", dr["id"].ToString(),
                    dr["pid"].ToString(), dr["name"], (dr["url"].ToString() != "" && !dr["url"].ToString().StartsWith("http") ? ("plus/" + dr["url"]) : dr["url"].ToString()), "main") + "},";
            }
            return ret;
        }
    }
}