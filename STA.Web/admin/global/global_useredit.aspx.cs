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
    public partial class useredit : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ddlUserGroup.AddTableData(Users.GetUserGroupDataPage(0), "name", "id", null);
            ddlSysGroup.AddTableData(Users.GetUserGroupDataPage(1), "name", "id", null);
            SystemChange(TypeParse.StrToInt(rblSysuser.SelectedValue, 0));
            if (STARequest.GetInt("id", 0) > 0) LoadData(STARequest.GetInt("id", 0));
        }

        protected void LoadData(int id)
        {
            UserInfo info = Users.GetUser(id);
            if (info == null) return;
            hidId.Value = info.Id.ToString();
            txtUserName.Enabled = false;
            txtUserName.Text = info.Username;
            rblGender.SelectedValue = info.Gender.ToString();
            txtNickName.Text = info.Nickname;
            ddlUserGroup.SelectedValue = info.Groupid.ToString();
            SystemChange(info.Adminid);
            txtEmail.Text = info.Email;
            txtCredits.Text = info.Credits.ToString();
            txtCredits1.Text = info.Extcredits1.ToString();
            ddlSysGroup.SelectedValue = info.Adminid.ToString();
            ddlUserGroup.SelectedValue = info.Groupid.ToString();
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

            UserInfo uinfo = Users.GetUser(TypeParse.StrToInt(hidId.Value, 0));
            if (config.Emailmultuser == 0 && txtEmail.Text.Trim() != uinfo.Email && Users.CheckUserEmailExist(txtEmail.Text.Trim()) > 0)
            {
                Message("邮件地址已存在,换一个吧！");
                return;
            }
            UserInfo info = Create();
            //if (txtPwd.Text != "000000")
            //{ 
            //    STA.Core.Api.UCenter.UserEdit(info.Username,
            //}
            if (!IsFounder(userid) && info.Id == BaseConfigs.GetFounderUid)
            {
                Message("您不可以编辑网站创建者！");
                return;
            }
            if (cbDelavatar.Checked)
            {
                Avatars.DeleteAvatar(info.Id);
            }


            Redirect("global_userlist.aspx?msg=" + string.Format("用户 <b>{0}</b> 已{1}修改！", info.Username, Users.EditUser(info) ? "成功" : "失败"));
            ConUtils.InsertLog(1, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "修改用户：" + info.Username, "");
        }

        private void rblSysuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemChange(TypeParse.StrToInt(rblSysuser.SelectedValue, 0));
        }

        private UserInfo Create()
        {
            UserInfo info = Users.GetUser(TypeParse.StrToInt(hidId.Value, 0));
            info.Password = txtPwd.Text != "000000" ? Utils.MD5(txtPwd.Text) : info.Password;
            info.Gender = byte.Parse(rblGender.SelectedValue);
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Nickname = txtNickName.Text;
            info.Username = txtUserName.Text;
            info.Groupid = TypeParse.StrToInt(ddlUserGroup.SelectedValue, 2);
            info.Adminid = rblSysuser.SelectedValue == "1" ? TypeParse.StrToInt(ddlSysGroup.SelectedValue, 1) : 0;
            info.Email = txtEmail.Text;
            info.Credits = TypeParse.StrToInt(txtCredits.Text, 100);
            info.Extcredits1 = TypeParse.StrToInt(txtCredits1.Text, 0);
            info.Groupname = ddlUserGroup.SelectedItem.Text;
            info.Admingroupname = ddlSysGroup.SelectedItem.Text;
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