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
    public partial class usergroupadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            rblSysgroup.SelectedValue = STARequest.GetQueryInt("system", 0).ToString();
            if (STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)
                LoadData(STARequest.GetInt("id", 0));
        }

        protected void LoadData(int id)
        {
            UserGroupInfo info = Users.GetUserGroup(id);
            if (info == null) return;
            hidId.Value = info.Id.ToString();
            rblSysgroup.SelectedValue = info.System.ToString();
            txtName.Text = info.Name;
            txtCreditsmin.Text = info.Creditsmin.ToString();
            txtCreditsmax.Text = info.Creditsmax.ToString();
            txtColor.Text = info.Color;
            txtStar.Text = info.Star.ToString();
            //头像
            rblSysgroup.Enabled = false;
            hidAction.Value = "edit";
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            UserGroupInfo info = Create();
            if (hidAction.Value == "edit")
                Users.EditUserGroup(info);
            else
                info.Id = Users.AddUserGroup(info);

            InsertLog(1, (hidAction.Value == "add" ? "添加" : "编辑") + "用户组", string.Format("ID:{0}", info.Id));
            Redirect("global_usergrouplist.aspx");
        }

        private UserGroupInfo Create()
        {
            UserGroupInfo info = new UserGroupInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.System = TypeParse.StrToInt(rblSysgroup.SelectedValue, 0);
            info.Name = txtName.Text;
            info.Creditsmax = TypeParse.StrToInt(txtCreditsmax.Text, 20);
            info.Creditsmin = TypeParse.StrToInt(txtCreditsmin.Text, 10);
            info.Color = txtColor.Text;
            //头像
            info.Star = TypeParse.StrToInt(txtStar.Text, 0);
            return info;
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