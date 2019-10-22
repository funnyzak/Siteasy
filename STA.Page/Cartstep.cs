using System;
using System.Web;
using System.Data;

using STA.Data;
using STA.Entity;
using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Page
{
    public class Cartstep : UserBase
    {
        public DataTable prods = new DataTable();  //购物车商品列表
        public DataTable addrs = new DataTable();  //用户地址列表
        public DataTable delys = new DataTable();  //配送方式列表
        public DataTable payms = new DataTable();  //支付方式列表
        public Cartstep()
        {
            DataTable cdt = Carts.GetCartProdsListByCookies();
            if (cdt.Rows.Count <= 0)
            {
                PageInfo("购物车没有任何商品,请先选购！", weburl);
                return;
            }

            prods = Carts.GetCartProdList(cdt);
            addrs = Shops.GetUserAddressTableByUid(userid);
            delys = Shops.GetDeliveryTable("id,name,ename,description,parms");
            payms = Shops.GetPaymentTable("id,name,description,pic,isvalid");
            Carts.WriteCookie(prods);

            decimal prodprice = Pays.TotalProductPrice(prods);
            decimal prodweight = Pays.TotalProductWeight(prods);

            delys.Columns.Add("cost", Type.GetType("System.String"));
            foreach (DataRow idr in delys.Rows)
            {
                idr["cost"] = Pays.CaclDeliveryPrice(prodprice, prodweight, idr["parms"].ToString());
            }

            if (ispost)
            {
                ShoporderInfo info = new ShoporderInfo();
                info.Oid = Pays.NewOrderNo();
                info.Uid = userid;
                info.Username = oluser.Username;
                info.Did = STARequest.GetFormInt("did", 0);
                info.Pid = STARequest.GetFormInt("pid", 0);
                info.Adrid = STARequest.GetFormInt("adrid", 0);
                info.Cartcount = Pays.TotalProductNum(prods);
                info.Dprice = Pays.CaclDeliveryPrice(prodprice, prodweight, delys.Select("id=" + info.Did.ToString())[0]["parms"].ToString());
                info.Price = prodprice;
                info.Status = OrderStatus.未处理;
                info.Ip = STARequest.GetIP();
                info.Isinvoice = byte.Parse(STARequest.GetFormString("isinvoice"));
                info.Invoicehead = STARequest.GetFormString("invoicehead");
                info.Invoicecost = 0;
                info.Totalprice = info.Dprice + info.Price;
                if (info.Isinvoice == 1)
                {
                    info.Invoicecost = Pays.InvoiceCost(info.Price);
                    info.Totalprice += info.Invoicecost;
                }
                info.Remark = STARequest.GetFormString("remark");
                Shops.AddShoporder(info);

                foreach (DataRow gdr in prods.Rows)
                {
                    ShopgoodInfo ginfo = new ShopgoodInfo();
                    ginfo.Cid = TypeParse.StrToInt(gdr["id"]);
                    ginfo.Oid = info.Oid;
                    ginfo.Uid = userid;
                    ginfo.Username = oluser.Username;
                    ginfo.Goodname = gdr["title"].ToString();
                    ginfo.Price = TypeParse.StrToDecimal(gdr["ext_vipprice"]);
                    ginfo.Buynum = TypeParse.StrToInt(gdr["num"]);
                    Shops.AddShopgood(ginfo);

                    ContentInfo cinfo = ConUtils.GetContent(ginfo.Cid);
                    if (cinfo != null)
                    {
                        cinfo.Ext["ext_storage"] = TypeParse.StrToInt(cinfo.Ext["ext_storage"]) - ginfo.Buynum;
                        Contents.EditContent(cinfo);
                    }
                }

                Carts.EmptyCart();

                Redirect("cartpay.aspx?orderno=" + info.Oid);
                //生成订单
            }

        }

    }

}
