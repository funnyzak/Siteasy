using System;
using System.Web;
using System.Data;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Payment;
using STA.Entity;

namespace STA.Page
{
    public class Cartpay : UserBase
    {
        public ShoporderInfo info;
        public PaymentInfo pinfo;
        public string payurl = "";
        public Cartpay()
        {
            info = Shops.GetShoporder(STARequest.GetString("orderno"), userid);
            if (info == null)
            {
                PageInfo("无效的订单");
            }
            else if (info.Status == OrderStatus.未处理)
            {
                pinfo = Shops.GetPayment(info.Pid);
                if (pinfo != null)
                {
                    IPayment pay = PaymentFactory.GetInstance(pinfo.Dll);
                    if (pay != null)
                    {
                        payurl = pay.GetPayUrl(pinfo.Parms, info.Oid, info.Oid, info.Oid, info.Totalprice.ToString("0.00"));
                    }
                    else
                    {
                        PageInfo("支付方式无效");
                    }
                }
                else
                {
                    PageInfo("支付方式无效");
                }
            }
            else
            {
                PageInfo("当前状态为：" + info.Status.ToString());
            }
        }

    }

}
