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
    public partial class magazinelist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                RedirectMessage();
                LoadData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetMagazineDataPage("*", pageIndex, managelistcount, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private bool Del(int id)
        {
            InsertLog(2, "删除杂志", string.Format("ID为:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
            return Contents.DelMagazine(id);
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Del(id);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"] = Contents.GetMagazineSearchCondition(txtStartDate.Text, txtEndDate.Text, txtLikeid.Text.Trim(), TypeParse.StrToInt(ddlStatus.SelectedValue, 0), txtKeywords.Text);
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
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}