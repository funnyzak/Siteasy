using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using STA.Payment;

namespace STA.Web.Admin.Pay
{
    public partial class payset : AdminPage
    {
        private string dll = STARequest.GetString("dll");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || dll == "") return;

            LoadData(dll);
        }

        private void LoadData(string dll)
        {
            #region 加载页面数据
            PaymentInfo info = Shops.GetPayment(dll);
            if (info == null)
            {
                Redirect("paylist.aspx");
                return;
            }

            txtName.Text = info.Name;
            txtDescription.Text = info.Description;
            txtOrderid.Text = info.Orderid.ToString();
            rblIsvalid.SelectedValue = info.Isvalid.ToString();
            txtPic.Text = info.Pic;

            IPayment payment = PaymentFactory.GetInstance(dll);
            if (payment != null)
            {
                string[] parms = info.Parms.Split(',');
                int i = 0;
                foreach (string str in payment.GetParms().Split(','))
                {
                    ltrCtrs.Text += BuildControl(str, parms[i]);
                    i++;
                }
            }
            #endregion
        }

        private string BuildControl(string name, string value)
        {
            ContypefieldInfo info = new ContypefieldInfo();
            info.Fieldtype = "nvarchar";
            info.Fieldname = info.Desctext = name;
            info.Defvalue = info.Fieldvalue = value;
            return ConUtils.BulidContentControlHtml(info);
        }

        private PaymentInfo CreateInfo()
        {
            PaymentInfo info = Shops.GetPayment(dll);
            info.Name = txtName.Text;
            info.Description = txtDescription.Text;
            info.Pic = txtPic.Text;
            info.Isvalid = byte.Parse(rblIsvalid.SelectedValue);
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);

            IPayment payment = PaymentFactory.GetInstance(dll);
            if (payment != null)
            {
                info.Parms = string.Empty;
                foreach (string str in payment.GetParms().Split(','))
                {
                    info.Parms += STARequest.GetString(str) + ",";
                }
                info.Parms += info.Pic;
            }
            return info;
        }

        void SaveInfo_Click(object sender, EventArgs e)
        {
            PaymentInfo info = CreateInfo();
            Shops.EditPayment(info);
            InsertLog(2, "支付方式配置", string.Format("名称:{0}", info.Name));
            Redirect("paylist.aspx");
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