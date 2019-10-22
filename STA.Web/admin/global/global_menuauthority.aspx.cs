using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class menuauthority : AdminPage
    {
        public string menuStr = "";
        public string menuIds = "";
        private int gid = STARequest.GetQueryInt("gid", 0);
        private DataTable newdt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadData();
        }

        private void LoadData()
        {
            UserGroupInfo uginfo = Users.GetUserGroup(gid);
            if (uginfo == null || uginfo.System != 1)
            {
                Redirect("global_sysgrouplist.aspx");
                return;
            }
            dgname.InnerText = uginfo.Name;

            DataTable menudt = Menus.GetMenuTable(1);
            DataTable gpmenudt = Menus.GetMenuRelatetionsByGroupId(gid);
            string gpmenustring = ConUtils.DataTableToString(gpmenudt, 0, ",");
            DataRow[] topdrs = menudt.Select("parentid=0");
            foreach (DataRow topdr in topdrs)
            {
                newdt = menudt.Clone();
                newdt.Clear();
                newdt.Rows.Add(topdr.ItemArray);
                GetDataTableByTop(topdr, menudt);
                SetMenuJson(newdt);
            }
            if (menuStr.EndsWith(",")) menuStr = menuStr.Substring(0, menuStr.Length - 1);

            menuIds = ConUtils.DataTableToString(Menus.GetMenuRelatetionsByGroupId(gid), 0, ",");
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


        private void SetMenuJson(DataTable dt)
        {
            menuStr += "[";
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
                menuStr += "{" + string.Format("id:{0},pId:{1},name:\"{2}\",icon:\"{3}\"", dr["id"], dr["parentid"], name, icon) + "},";
            }
            if (menuStr.EndsWith(",")) menuStr = menuStr.Substring(0, menuStr.Length - 1);
            menuStr += "],";
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            Menus.DelMenuRelation(gid);
            foreach (string id in STARequest.GetFormString("menuidlist").Split(','))
            {
                int mid = TypeParse.StrToInt(id);
                if (mid == 0) continue;
                Menus.AddMenuRelation(gid, mid);
            }

            InsertLog(2, "权限设置", string.Format("管理组ID:{0}", gid.ToString()));
            Redirect("global_sysgrouplist.aspx");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}