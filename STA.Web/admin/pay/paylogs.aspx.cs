using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Pay
{
    public partial class paylogs : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delpaylog":
                        Message(Del(STARequest.GetInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                RedirectMessage();
                ddlGtype.AddTableData(ConUtils.GetEnumTable(typeof(GoodType)), "name", "id", "全部,-1");
                ddlPlist.AddTableData(Shops.GetPaymentTable("id,name"), "name", "id", "全部,-1");
                LoadData(1);
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private bool Del(int id)
        {
            if (id <= 0) return false;
            Shops.DelPaylog(id);
            InsertLog(2, "删除支付记录", string.Format("订单号:{0}", STARequest.GetString("oid_" + id.ToString())));
            return true;
        }


        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Shops.GetPaylogDataPage("", pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

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
                    Del(TypeParse.StrToInt(d, 0));
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["pagesize"] = TypeParse.StrToInt(txtPageSize.Text, managelistcount);
            ViewState["condition"] = Shops.GetPaylogSearchCondition(TypeParse.StrToInt(ddlGtype.SelectedValue, -1), txtOid.Text, TypeParse.StrToInt(ddlPlist.SelectedValue, -1), txtUsers.Text, txtStartDate.Text, txtEndDate.Text, txtSprice.Text, txtEprice.Text);
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
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
        }

        #endregion
    }
}