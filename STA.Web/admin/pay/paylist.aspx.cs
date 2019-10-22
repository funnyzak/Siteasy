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
    public partial class paylist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindData();
        }

        private void BindData()
        {
            DataTable dt = Pays.BuildPaymentTable();
            dt.Columns.Add("setup", Type.GetType("System.Int32"));
            dt.Columns.Add("isvalid", Type.GetType("System.Int32"));
            foreach (DataRow dr in dt.Rows)
            {
                PaymentInfo info = Shops.GetPayment(dr["dll"].ToString());
                dr["setup"] = info == null ? 0 : 1;
                dr["isvalid"] = (info == null || info.Isvalid == 0) ? 0 : 1;
            }

            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        void Unstall(string dll)
        {
            if (Shops.DelPayment(dll))
                InsertLog(2, "卸载支付插件", "插件名：" + STARequest.GetString("name_" + dll));
        }

        void Setup(string dll)
        {
            if (Pays.PaymentSetup(dll))
                InsertLog(2, "安装支付插件", "插件名：" + STARequest.GetString("name_" + dll));
        }

        void BtnUnstall_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    Unstall(d);
                }
                BindData();
                Message();
            }
        }

        void BtnSetup_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    Setup(d);
                }
                BindData();
                Message();
            }
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.BtnSetup.Click += new EventHandler(BtnSetup_Click);
            this.BtnUnstall.Click += new EventHandler(BtnUnstall_Click);
        }

        #endregion
    }
}