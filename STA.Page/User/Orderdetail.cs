using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Payment;
using STA.Entity;
namespace STA.Page.User
{
    public class Orderdetail : UserBase
    {
        public ShoporderInfo info;
        public PaymentInfo pinfo;

        protected override void PageShow()
        {
            if (!IsLogin()) return;

            info = Shops.GetShoporder(STARequest.GetString("orderno"), userid);

            if (info == null)
            {
                PageInfo("无效的订单");
            }
            else
            {
                pinfo = Shops.GetPayment(info.Pid);
            }
        }
    }
}
