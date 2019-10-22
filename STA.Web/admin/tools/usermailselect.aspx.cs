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
    public partial class usermailselect : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtActionstartdate.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
                txtActionenddate.Text = DateTime.Now.ToShortDateString();
                BindData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Users.GetUserDataPage(pageIndex, 6, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchcondition = DatabaseProvider.GetInstance().GetUserSearchCondition(TypeParse.StrToInt(ddlGender.SelectedValue, -1),
                            TypeParse.StrToInt(ddlStatus.SelectedValue, -1), "", "", txtActionstartdate.Text, txtActionenddate.Text,
                            0, txtUsername.Text, txtNickname.Text, txtEmail.Text);
            ViewState["condition"] = searchcondition;
            BindData(1);
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
            isShowSysMenu = false;
        }

        private void InitializeComponent()
        {
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}