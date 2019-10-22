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
    public partial class admintasklogs : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "dellogs":
                        Message(Users.DelAdminLog(STARequest.GetFormInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ViewState["condition"] = Users.GetAdminlogSearchCondition(3, "", "", "", "");
                LoadData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Users.GetAdminLogDataPage(pageIndex, managelistcount, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (!IsFounder(userid))
            {
                Message(base.notfoundermsg);
                return;
            }
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Users.DelAdminLog(id);
                }
                Message();
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void EmptyBtn_Click(object sender, EventArgs e)
        {
            if (!IsFounder(userid))
            {
                Message(base.notfoundermsg);
                return;
            }
            Message(Users.DelAdminlogWhere(TypeParse.ObjToString(ViewState["condition"])) > 0);
            LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        }

        private void EmptyAllBtn_Click(object sender, EventArgs e)
        {
            if (!IsFounder(userid))
            {
                Message(base.notfoundermsg);
                return;
            }
            Message(Users.DelAdminlogWhere(Users.GetAdminlogSearchCondition(3, "", "", "", "")) > 0);
            LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            config.Adminlogs = 0;
            Message(GeneralConfigs.SaveConfig(config));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"] = Users.GetAdminlogSearchCondition(3, txtStartDate.Text, txtEndDate.Text, "", txtKeywords.Text);
            LoadData(1);
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
            this.EmptyAllBtn.Click += new EventHandler(this.EmptyAllBtn_Click);
            this.EmptyBtn.Click += new EventHandler(this.EmptyBtn_Click);
            this.StopBtn.Click += new EventHandler(this.StopBtn_Click);
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}