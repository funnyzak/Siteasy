using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class congroupconselect : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            isShowSysMenu = false;
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
                DataTable cdt = Contents.GetChannelDataTable();
                ConUtils.BulidChannelList(STARequest.GetInt("type", 1), cdt, ddlConType);
                ViewState["condition"] = Contents.GetContentSearchCondition(-1, "", 0, 0, -1, "", "", "", "");
            }
            if (!IsPostBack)
                BindData(1);
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetContentDataPage(pageIndex, 6, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"]  = Contents.GetContentSearchCondition(-1, txtUsers.Text, 0, TypeParse.StrToInt(ddlConType.SelectedValue, 0),
                      TypeParse.StrToInt(ddlStatus.SelectedValue, -1), ddlProperty.SelectedValue, txtStartDate.Text, txtEndDate.Text, txtKeywords.Text);
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
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}