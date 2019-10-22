using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Entity;
using STA.Payment;

namespace STA.Core
{
    public class Pays
    {
        /// <summary>
        /// 计算商品配送价格
        /// </summary>
        /// <param name="totalprice">商品总价</param>
        /// <param name="weight">商品总重</param>
        /// <param name="parmstr">配送参数</param>
        /// <returns></returns>
        public static decimal CaclDeliveryPrice(decimal totalprice, decimal weight, string parmstr)
        {
            string[] parms = parmstr.Split(',');
            decimal fweight = TypeParse.StrToDecimal(parms[0]);
            decimal fmoney = TypeParse.StrToDecimal(parms[1]);
            decimal cweight = TypeParse.StrToDecimal(parms[2]);
            decimal cmoney = TypeParse.StrToDecimal(parms[3]);
            decimal free = TypeParse.StrToDecimal(parms[4]);

            if (totalprice >= free)
                return 0;

            decimal price = fmoney;
            if (weight <= fweight)
            {
                return price;
            }
            else
            {
                price += (weight - fweight) / cweight * cmoney;
            }
            return price;
        }

        /// <summary>
        /// 生成新订单号
        /// </summary>
        /// <returns></returns>
        public static string NewOrderNo()
        {
            //string ordernorule = GeneralConfigs.GetConfig().Ordernorule;
            //if (ordernorule == "")
            //    ordernorule = "{@ram9}";
            //return Globals.StrRandomConvert(ordernorule, DateTime.Now);
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        public static decimal CalcTotal(DataTable proddt, string calcname)
        {
            decimal total = 0;
            foreach (DataRow dr in proddt.Rows)
            {
                total += TypeParse.StrToDecimal(dr[calcname]) * TypeParse.StrToInt(dr["num"]);
            }
            return total;
        }

        public static decimal TotalProductWeight(DataTable dt)
        {
            return CalcTotal(dt, "ext_weight");
        }

        public static decimal TotalProductPrice(DataTable dt)
        {
            return CalcTotal(dt, "ext_vipprice");
        }

        public static int TotalProductNum(DataTable dt)
        {
            int total = 0;
            foreach (DataRow dr in dt.Rows)
            {
                total += TypeParse.StrToInt(dr["num"]);
            }
            return total;
        }

        public static decimal InvoiceCost(decimal totalprice)
        {
            return totalprice * GeneralConfigs.GetConfig().Invoicerate * 0.01M;
        }

        /// <summary>
        /// 获取支付插件列表
        /// </summary>
        /// <returns></returns>
        public static DataTable BuildPaymentTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", Type.GetType("System.Int32"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("dll", Type.GetType("System.String"));
            dt.Columns.Add("officeurl", Type.GetType("System.String"));
            dt.Columns.Add("author", Type.GetType("System.String"));
            dt.Columns.Add("version", Type.GetType("System.String"));
            dt.Columns.Add("description", Type.GetType("System.String"));
            dt.Columns.Add("defaultdescription", Type.GetType("System.String"));

            List<FileItem> flist = FileUtil.GetFiles(Utils.GetMapPath("/bin"), "dll");

            int loop = 1;
            foreach (FileItem item in flist)
            {
                if (!item.Name.StartsWith("STA.Payment.")) continue;

                string dllname = item.Name.Replace("STA.Payment.", "").Replace(".dll", "");
                if (dllname.Trim() == "dll") continue;
                IPayment payment = PaymentFactory.GetInstance(dllname);
                if (payment == null) continue;

                DataRow dr = dt.NewRow();
                dr["id"] = loop;
                dr["author"] = payment.Author();
                dr["dll"] = dllname;
                dr["description"] = payment.Description();
                dr["defaultdescription"] = payment.DefaultDescription();
                dr["version"] = payment.Version();
                dr["name"] = payment.Name();
                dr["officeurl"] = payment.OfficeUrl();
                dt.Rows.Add(dr);
                loop++;
            }
            return dt;
        }

        public static bool PaymentSetup(string dllname)
        {
            IPayment payment = PaymentFactory.GetInstance(dllname);
            if (payment == null) return false;

            PaymentInfo info = new PaymentInfo();
            info.Dll = dllname;
            info.Name = payment.Name();
            info.Description = payment.Description();
            info.Version = payment.Version();
            info.Isvalid = 1;
            info.Author = payment.Author();
            info.Parms = ",,,,,,,,,,,,,,,,,,";

            return Shops.AddPayment(info) > 0;
        }

        public static bool DelBackOrder(int backday)
        {
            DataTable odt = Shops.GetBackShoporder(backday, "oid");
            foreach (DataRow dr in odt.Rows)
            {
                ShoporderInfo orderinfo = Shops.GetShoporder(dr["oid"].ToString());
                orderinfo.Status = OrderStatus.管理员取消;
                Shops.EditShoporder(orderinfo);
                ConUtils.InsertLog(2, 0, "系统任务", 1, "系统", "127.0.0.1", "取消过期订单", "订单号：" + dr["oid"].ToString());
            }
            return true;
        }
    }
}
