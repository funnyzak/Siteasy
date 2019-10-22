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
    public partial class orderset : AdminPage
    {
        public string oid = STARequest.GetString("oid");
        public ShoporderInfo orderinfo;
        public PaymentInfo paymentinfo;
        public ShopdeliveryInfo deliveryinfo;
        public UseraddressInfo useraddressinfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            orderinfo = Shops.GetShoporder(oid);
            if (orderinfo != null)
            {
                paymentinfo = Shops.GetPayment(orderinfo.Pid);
                useraddressinfo = Shops.GetUseraddress(orderinfo.Adrid);
                deliveryinfo = Shops.GetShopdelivery(orderinfo.Did);
            }

            if (IsPostBack) return;

            if (orderinfo == null)
            {
                Redirect("orderlist.aspx");
                return;
            }

            rblStatus.AddTableData(ConUtils.GetEnumTable(typeof(OrderStatus)), "name", "id", null);
            rblStatus.SelectedValue = ((int)orderinfo.Status).ToString();
            if (orderinfo.Status == OrderStatus.管理员取消 || orderinfo.Status == OrderStatus.用户取消)
                rblStatus.Enabled = false;

            ddlPids.AddTableData(ConUtils.RemoveTableRow(Shops.GetPaymentTable("id,name,isvalid"), "isvalid", "0"), "name", "id");
            ddlPids.SelectedValue = orderinfo.Pid.ToString();
            txtTotalprice.Text = orderinfo.Totalprice.ToString();
            txtTotalprice.HelpText = "在这里修改订单价格,订单实际总金额为：" + (orderinfo.Dprice + orderinfo.Price + orderinfo.Invoicecost).ToString("0.00") + " 元";
            txtRemark.Text = orderinfo.Remark;
            txtTracknum.Text = orderinfo.Parms.Split(',')[0];

            if (paymentinfo == null) paymentinfo = new PaymentInfo();
            if (useraddressinfo == null) useraddressinfo = new UseraddressInfo();
            if (deliveryinfo == null) deliveryinfo = new ShopdeliveryInfo();

            prodData.Visible = true;
            prodData.DataSource = Shops.GetShopgoodTableByOid(oid, "title,channelid,channelname,img,price,buynum,ext_price");
            prodData.DataBind();
        }

        void btn_EditOrder_Click(object sender, EventArgs e)
        {
            decimal totalprice = TypeParse.StrToDecimal(txtTotalprice.Text);
            int pid = TypeParse.StrToInt(ddlPids.SelectedValue);
            string remark = txtRemark.Text;
            OrderStatus os = (OrderStatus)TypeParse.StrToInt(rblStatus.SelectedValue, (int)OrderStatus.未处理);

            if (orderinfo.Totalprice != totalprice)
                InsertLog(2, "修改订单价格", string.Format("订单号：{0},由{1}改为{2}", oid, orderinfo.Totalprice, totalprice));
            if (orderinfo.Status != os)
                InsertLog(2, "修改订单状态", string.Format("订单号：{0},由 {1} 改为 {2}", oid, orderinfo.Status.ToString(), os.ToString()));
            if (orderinfo.Pid != pid)
                InsertLog(2, "修改订单支付方式", string.Format("订单号：{0},由 {1} 改为 {2}", oid, paymentinfo.Name, ddlPids.SelectedItem.Text));
            if (orderinfo.Remark != remark)
                InsertLog(2, "修改订单备注", string.Format("订单号：{0},由 {1} 改为 {2}", oid, orderinfo.Remark, remark));

            orderinfo.Status = os;
            orderinfo.Pid = pid;
            orderinfo.Totalprice = totalprice;
            orderinfo.Remark = remark;
            orderinfo.Parms = txtTracknum.Text;

            Shops.EditShoporder(orderinfo);
            Redirect("orderlist.aspx?msg=" + Utils.UrlEncode(string.Format("订单：<b>{0}</b> 已成功修改！", oid)));
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            btn_EditOrder.Click += new EventHandler(btn_EditOrder_Click);
        }

        #endregion
    }
}