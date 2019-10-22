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
    public partial class menulist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delmenu":
                        InsertLog(2, "删除菜单", "ID：" + STARequest.GetString("hidValue"));
                        Message(ConUtils.DeleteMenu(STARequest.GetFormInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                ddlPageType.AddTableData(ConUtils.GetEnumTable(typeof(PageType)), "name", "id", "全部,0");
                ddlmenus.AddTableData(Menus.GetMenuTable(-1), "name", "id");
                txtPageSize.Text = managelistcount.ToString();
                LoadData(1);
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Menus.GetMenuDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

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
                    ConUtils.DeleteMenu(id);
                    InsertLog(2, "删除菜单", "ID：" + d);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"] = Menus.GetMenuSearchCondition(TypeParse.StrToInt(ddlPageType.SelectedValue, 0), 0, TypeParse.StrToInt(ddlSystem.SelectedValue), txtKeywords.Text);
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