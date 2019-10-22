using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin.Pay
{
    public partial class deliveryadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            ShopdeliveryInfo info = Shops.GetShopdelivery(id);
            if (info == null) return;

            txtName.Text = info.Name;
            txtEname.Text = info.Ename;
            txtDescription.Text = info.Description;
            txtFweight.Text = info.Fweight.ToString();
            rblIscod.SelectedValue = info.Iscod.ToString();
            txtOrderid.Text = info.Orderid.ToString();

            string[] parms = info.Parms.Split(',');
            txtFwmondy.Text = parms[1];
            txtCweight.Text = parms[2];
            txtCmondy.Text = parms[3];
            txtFree.Text = parms[4];

            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private ShopdeliveryInfo CreateInfo()
        {
            ShopdeliveryInfo info = new ShopdeliveryInfo();
            //if (hidAction.Value == "edit")
            //    info = Shops.GetShopdelivery(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Ename = txtEname.Text;
            info.Description = txtDescription.Text;
            info.Fweight = TypeParse.StrToDecimal(txtFweight.Text);
            info.Iscod = Byte.Parse(rblIscod.SelectedValue);
            info.Parms = txtFweight.Text + "," + txtFwmondy.Text + "," + txtCweight.Text + "," + txtCmondy.Text + "," + txtFree.Text;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            return info;
        }

        void SaveInfo_Click(object sender, EventArgs e)
        {
            ShopdeliveryInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Shops.AddShopdelivery(info);
            else
                Shops.EditShopdelivery(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "配送方式", string.Format("ID:{0},名称:{1}", info.Id, info.Name));
            Redirect("deliverylist.aspx");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(SaveInfo_Click);
        }
        #endregion
    }
}