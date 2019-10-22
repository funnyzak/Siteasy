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
    public partial class useradd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ddlUserGroup.AddTableData(Users.GetUserGroupDataPage(0), "name", "id", null);
            ddlSysGroup.AddTableData(Users.GetUserGroupDataPage(1), "name", "id", null);
            SystemChange(TypeParse.StrToInt(rblSysuser.SelectedValue, 0));
        }

        private void SystemChange(int system)
        {
            trSysGroup.Visible = system > 0;
            rblSysuser.SelectedValue = system > 0 ? "1" : "0";
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text.Trim() != txtRePwd.Text.Trim())
            {
                Message("两次输入的密码不一样，请确认！");
                return;
            }
            if (config.Emailmultuser == 0 && Users.CheckUserEmailExist(txtEmail.Text.Trim()) > 0)
            {
                Message("邮件地址已存在,换一个吧！");
                return;
            }
            if (Users.ExistUser(txtUserName.Text) > 0)
            {
                Message("该用户名已经存在，不可用！");
                return;
            }
            UserInfo info = Create();
            info.Id = Users.AddUser(info);
            if (info.Id > 0)
            {
                UserfieldInfo ufinfo = new UserfieldInfo();
                ufinfo.Uid = info.Id;
                Users.AddUserField(ufinfo);
            }
            Message(info.Id > 0, "global_userlist.aspx");
            ConUtils.InsertLog(1, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "添加用户：" + info.Username, "");
        }

        private void rblSysuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemChange(TypeParse.StrToInt(rblSysuser.SelectedValue, 0));
        }

        private UserInfo Create()
        {
            UserInfo info = new UserInfo();
            info.Password = Utils.MD5(txtPwd.Text);
            info.Gender = byte.Parse(rblGender.SelectedValue);
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Nickname = txtNickName.Text;
            info.Username = txtUserName.Text;
            info.Groupid = TypeParse.StrToInt(ddlUserGroup.SelectedValue, 2);
            info.Adminid = rblSysuser.SelectedValue == "1" ? TypeParse.StrToInt(ddlSysGroup.SelectedValue, 1) : 0;
            info.Email = txtEmail.Text;
            info.Groupname = ddlUserGroup.SelectedItem.Text;
            info.Admingroupname = ddlSysGroup.SelectedItem.Text;
            info.Ischeck = 1;
            info.Loginip = info.Regip = STARequest.GetIP();
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
            this.rblSysuser.SelectedIndexChanged += new EventHandler(this.rblSysuser_SelectedIndexChanged);
        }
        #endregion
    }
}