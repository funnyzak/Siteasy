using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class userlist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (!IsPostBack)
            {
                RedirectMessage();
                txtPageSize.Text = managelistcount.ToString();
                txtRegStartDate.Text = txtActionstartdate.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                txtRegEndDate.Text = txtActionenddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ddlUsergroup.AddTableData(Users.GetUserGroupDataPage(-1), "name", "id");
                BindData(1);
            }
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "deluser":
                        DelUser(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Users.GetUserDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void DelUser(int uid)
        {
            if (uid <= 0 || IsFounder(uid)) return;
            Users.DelUser(uid);
            InsertLog(1, "删除用户", string.Format("用户ID:{0},用户名:{1}", uid.ToString(), STARequest.GetFormString("name" + uid.ToString())));
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("id") != "")
            {
                string ids = STARequest.GetString("id");
                string[] userlist = ids.Split(',');
                foreach (string uid in userlist)
                {
                    DelUser(int.Parse(uid));
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void UnLockBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("id") != "")
            {
                string ids = STARequest.GetString("id");
                string[] userlist = ids.Split(',');
                foreach (string uid in userlist)
                {
                    UserInfo uinfo = Users.GetUser(TypeParse.StrToInt(uid));
                    uinfo.Locked = 0;
                    Users.EditUser(uinfo);
                    InsertLog(1, "解锁用户", string.Format("用户ID:{0},用户名:{1}", uid.ToString(), STARequest.GetFormString("name" + uid.ToString())));
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void LockBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("id") != "")
            {
                string ids = STARequest.GetString("id");
                string[] userlist = ids.Split(',');
                foreach (string uid in userlist)
                {
                    if (IsFounder(TypeParse.StrToInt(uid))) continue;
                    UserInfo uinfo = Users.GetUser(TypeParse.StrToInt(uid));
                    uinfo.Locked = 1;
                    Users.EditUser(uinfo);
                    InsertLog(1, "锁定用户", string.Format("用户ID:{0},用户名:{1}", uid.ToString(), STARequest.GetFormString("name" + uid.ToString())));
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void VerifyOk_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("id") != "")
            {
                string ids = STARequest.GetString("id");
                string[] userlist = ids.Split(',');
                foreach (string uid in userlist)
                {
                    UserInfo uinfo = Users.GetUser(TypeParse.StrToInt(uid));
                    if (uinfo.Ischeck != 1)
                    {
                        uinfo.Ischeck = 1;
                        uinfo = UserUtils.InitUserGroup(uinfo);
                        Users.EditUser(uinfo);
                        InsertLog(1, "审核用户", string.Format("用户ID:{0},用户名:{1}", uid.ToString(), STARequest.GetFormString("name" + uid.ToString())));
                    }
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchcondition = DatabaseProvider.GetInstance().GetUserSearchCondition(TypeParse.StrToInt(ddlGender.SelectedValue, -1),
                            TypeParse.StrToInt(ddlStatus.SelectedValue, -1), txtRegStartDate.Text, txtRegEndDate.Text, txtActionstartdate.Text, txtActionenddate.Text,
                            TypeParse.StrToInt(ddlUsergroup.SelectedValue, 0), txtUsername.Text, txtNickname.Text, txtEmail.Text);
            ViewState["condition"] = searchcondition;
            ViewState["pagesize"] = txtPageSize.Text;
            BindData(1);
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
            this.LockBtn.Click += new EventHandler(this.LockBtn_Click);
            this.UnLockBtn.Click += new EventHandler(this.UnLockBtn_Click);
            this.VerifyOk.Click += new EventHandler(this.VerifyOk_Click);
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}