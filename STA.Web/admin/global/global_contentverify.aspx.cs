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
    public partial class contentverify : AdminPage
    {
        string action = STARequest.GetString("hidAction");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPageSize.Text = managelistcount.ToString();
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
                ConUtils.BulidChannelList(-1, Contents.GetChannelDataTable(), ddlConType);
                ViewState["condition"] = Contents.GetContentSearchCondition(STARequest.GetQueryInt("type", -1), "", 0, 0, 1, "", "", "", "");
                BindData(1);
            }
            else if (IsPostBack && action != "")
            {
                switch (action)
                {
                    case "delcon":
                        Message(Contents.PutContentRecycle(STARequest.GetFormInt("hidValue", 0)));
                        break;
                    case "verify":
                        Message(Contents.VerifyContent(STARequest.GetFormInt("hidValue", 0), 2));
                        break;
                    case "verifyno":
                        Message(Contents.VerifyContent(STARequest.GetFormInt("hidValue", 0), 3));
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
            DataTable dt = Contents.GetContentDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.PutContentRecycle(id);
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.VerifyContent(id, (int)ConStatus.通过);
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void VerifyNo_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.VerifyContent(id, (int)ConStatus.退稿);
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchcondition = Contents.GetContentSearchCondition(STARequest.GetInt("type", -1), txtUsers.Text, 0, TypeParse.StrToInt(ddlConType.SelectedValue, 0),
                                    -1, ddlProperty.SelectedValue, txtStartDate.Text, txtEndDate.Text, txtKeywords.Text);
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
            this.DelBtn.Click += new EventHandler(this.Del_Click);
            this.VerifyBtn.Click += new EventHandler(this.VerifyBtn_Click);
            this.VerifyNo.Click += new EventHandler(this.VerifyNo_Click);
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}