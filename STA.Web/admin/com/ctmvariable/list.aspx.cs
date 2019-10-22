using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Plus.ctmvariable
{
    public partial class list : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delvariable":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                LoadData(1);
                ddlLikeids.AddTableData(STA.Data.Plus.GetVariableLikeidList(), "likeid", "likeid");
                RedirectMessage();
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = STA.Data.Plus.GetVariableDataPage("", pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
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

        private void Del(int id)
        {
            if (id <= 0 || STARequest.GetFormInt("system" + id.ToString(), 0) == 1) return;

            STA.Data.Plus.DelVariable(id);
            InsertLog(2, "删除自定义变量", string.Format("ID:{0},变量键:{1}", id.ToString(), STARequest.GetFormString("key" + id.ToString())));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"] = STA.Data.Plus.GetVariableCondition(txtName.Text, ddlLikeids.SelectedValue, txtKey.Text);
            ViewState["pagesize"] = txtPageSize.Text;
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