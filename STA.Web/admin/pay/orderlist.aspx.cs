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
    public partial class orderlist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delorder":
                        Message(Del(STARequest.GetString("hidValue")));
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                RedirectMessage();
                ddlStatus.AddTableData(ConUtils.GetEnumTable(typeof(OrderStatus)), "name", "id", "全部,-1");
                ddlDlist.AddTableData(Shops.GetDeliveryTable("id,name"), "name", "id", "全部,-1");
                ddlPlist.AddTableData(Shops.GetPaymentTable("id,name"), "name", "id", "全部,-1");
                ViewState["condition"] = Shops.GetShoporderSearchCondition((int)GoodType.实物商品, -1, "", -1, -1, "", "", "", "", "");
                LoadData(1);
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private bool Del(string oid)
        {
            if (oid == "") return false;
            Shops.DelShoporder(oid);
            InsertLog(2, "删除订单", string.Format("订单号:{0}", oid.ToString()));
            return true;
        }


        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Shops.GetShoporderDataPage("", pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

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
                    Del(d);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        void SetStatus(OrderStatus os)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    ShoporderInfo info = Shops.GetShoporder(d);
                    if (info == null||info.Status == os) continue;
                    info.Status = os;
                    Shops.EditShoporder(info);
                    InsertLog(2, "更改订单状态", "订单号：" + d);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        void BtnSEnd_Click(object sender, EventArgs e)
        {
            SetStatus(OrderStatus.已完成);
        }

        void BtnSSended_Click(object sender, EventArgs e)
        {
            SetStatus(OrderStatus.已发货);
        }

        void BtnSPayed_Click(object sender, EventArgs e)
        {
            SetStatus(OrderStatus.已付款);
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["pagesize"] = TypeParse.StrToInt(txtPageSize.Text, managelistcount);
            ViewState["condition"] = Shops.GetShoporderSearchCondition((int)GoodType.实物商品,TypeParse.StrToInt(ddlStatus.SelectedValue, -1), txtOid.Text, TypeParse.StrToInt(ddlPlist.SelectedValue, -1), TypeParse.StrToInt(ddlDlist.SelectedValue, -1), txtUsers.Text, txtStartDate.Text, txtEndDate.Text, txtSprice.Text, txtEprice.Text);
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
            this.BtnSPayed.Click += new EventHandler(BtnSPayed_Click);
            this.BtnSSended.Click += new EventHandler(BtnSSended_Click);
            this.BtnSEnd.Click += new EventHandler(BtnSEnd_Click);
        }
        #endregion
    }
}