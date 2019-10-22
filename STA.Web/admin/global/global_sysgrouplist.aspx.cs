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
    public partial class sysgrouplist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delusergroup":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        break;
                }
                hidAction.Value = "";
                LoadData();
            }
            if (!IsPostBack) LoadData();
        }

        private void LoadData()
        {
            int pageCount, recordCount;
            DataTable dt = Users.GetUserGroupDataPage(1, 10000, 1, out pageCount, out recordCount);
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void Del(int id)
        {
            if (id <= 2) return;
            Users.DelUserGroup(id);
            InsertLog(2, "删除用户组", "ID:" + id.ToString());
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    Del(TypeParse.StrToInt(d));
                }
                LoadData();
            }
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.DelBtn.Click += new EventHandler(this.DelBtn_Click);
        }
        #endregion
    }
}