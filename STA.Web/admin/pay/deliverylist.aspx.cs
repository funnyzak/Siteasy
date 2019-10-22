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
    public partial class deliverylist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "deldelivery":
                        Message(Del(STARequest.GetInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                LoadData(1);
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private bool Del(int id)
        {
            if (id <= 0) return false;
            Shops.DelShopdelivery(id);
            InsertLog(2, "删除配送方式", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
            return true;
        }


        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Shops.GetShopdeliveryDataPage("", pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

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

        void SubmitEdit_Click(object sender, EventArgs e)
        {
            #region 编辑排序
            if (STARequest.GetString("hidid") != "")
            {
                string ids = STARequest.GetString("hidid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    ShopdeliveryInfo info = Shops.GetShopdelivery(id);
                    info.Orderid = TypeParse.StrToInt(STARequest.GetString("txtOrderId" + d));
                    Shops.EditShopdelivery(info);
                }
                LoadData(1);
                Message();
            }
            #endregion
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
            this.SubmitEdit.Click += new EventHandler(SubmitEdit_Click);
        }

        #endregion
    }
}