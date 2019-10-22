using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Shops
    {
        #region Payment
        public static int AddPayment(PaymentInfo info)
        {
            return DatabaseProvider.GetInstance().AddPayment(info);
        }

        public static int EditPayment(PaymentInfo info)
        {
            return DatabaseProvider.GetInstance().EditPayment(info);
        }

        public static bool DelPayment(int id)
        {
            return DatabaseProvider.GetInstance().DelPayment(id) > 0;
        }

        public static bool DelPayment(string dll)
        {
            return DatabaseProvider.GetInstance().DelPayment(dll) > 0;
        }

        public static DataTable GetPaymentTable(string fields)
        {
            return DatabaseProvider.GetInstance().GetPaymentTable(fields);
        }

        public static PaymentInfo GetPayment(int id)
        {
            PaymentInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPayment(id))
            {
                if (reader.Read())
                {
                    info = LoadPaymentInfo(reader);
                }
            }
            return info;
        }

        public static PaymentInfo GetPayment(string dll)
        {
            PaymentInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPayment(dll))
            {
                if (reader.Read())
                {
                    info = LoadPaymentInfo(reader);
                }
            }
            return info;
        }

        private static PaymentInfo LoadPaymentInfo(IDataReader reader)
        {
            PaymentInfo info = new PaymentInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Dll = TypeParse.ObjToString(reader["dll"]).Trim();
            info.Name = TypeParse.ObjToString(reader["name"]).Trim();
            info.Description = TypeParse.ObjToString(reader["description"]).Trim();
            info.Pic = TypeParse.ObjToString(reader["pic"]).Trim();
            info.Isvalid = byte.Parse(TypeParse.ObjToString(reader["isvalid"]));
            info.Parms = TypeParse.ObjToString(reader["parms"]).Trim();
            info.Version = TypeParse.ObjToString(reader["version"]).Trim();
            info.Author = TypeParse.ObjToString(reader["author"]).Trim();
            info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
            return info;
        }

        public static DataTable GetPaymentDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetPaymentDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion

        #region Shopdelivery
        public static int AddShopdelivery(ShopdeliveryInfo info)
        {
            return DatabaseProvider.GetInstance().AddShopdelivery(info);
        }

        public static int EditShopdelivery(ShopdeliveryInfo info)
        {
            return DatabaseProvider.GetInstance().EditShopdelivery(info);
        }

        public static DataTable GetDeliveryTable(string fields)
        {
            return DatabaseProvider.GetInstance().GetDeliveryTable(fields);
        }

        public static bool DelShopdelivery(int id)
        {
            return DatabaseProvider.GetInstance().DelShopdelivery(id) > 0;
        }

        public static ShopdeliveryInfo GetShopdelivery(int id)
        {
            ShopdeliveryInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetShopdelivery(id))
            {
                if (reader.Read())
                {
                    info = LoadShopdeliveryInfo(reader);
                }
            }
            return info;
        }

        private static ShopdeliveryInfo LoadShopdeliveryInfo(IDataReader reader)
        {
            ShopdeliveryInfo info = new ShopdeliveryInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Name = TypeParse.ObjToString(reader["name"]).Trim();
            info.Ename = TypeParse.ObjToString(reader["ename"]).Trim();
            info.Description = TypeParse.ObjToString(reader["description"]).Trim();
            info.Fweight = decimal.Parse(reader["fweight"].ToString());
            info.Iscod = byte.Parse(TypeParse.ObjToString(reader["iscod"]));
            info.Parms = TypeParse.ObjToString(reader["parms"]).Trim();
            info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
            return info;
        }

        public static DataTable GetShopdeliveryDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetShopdeliveryDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion

        #region Shopgood
        public static int AddShopgood(ShopgoodInfo info)
        {
            return DatabaseProvider.GetInstance().AddShopgood(info);
        }

        public static int EditShopgood(ShopgoodInfo info)
        {
            return DatabaseProvider.GetInstance().EditShopgood(info);
        }

        public static DataTable GetShopgoodTableByOid(string oid, string fields)
        {
            return DatabaseProvider.GetInstance().GetShopgoodTableByOid(oid, fields);
        }

        public static bool DelShopgood(int id)
        {
            return DatabaseProvider.GetInstance().DelShopgood(id) > 0;
        }

        public static DataTable GetShopgoodByOid(string oid, string fields)
        {
            return DatabaseProvider.GetInstance().GetShopgoodByOid(oid, fields);
        }

        public static ShopgoodInfo GetShopgood(int id)
        {
            ShopgoodInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetShopgood(id))
            {
                if (reader.Read())
                {
                    info = LoadShopgoodInfo(reader);
                }
            }
            return info;
        }

        private static ShopgoodInfo LoadShopgoodInfo(IDataReader reader)
        {
            ShopgoodInfo info = new ShopgoodInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Gtype = (GoodType)byte.Parse(TypeParse.ObjToString(reader["gtype"]));
            info.Cid = TypeParse.StrToInt(reader["cid"], 0);
            info.Oid = TypeParse.ObjToString(reader["oid"]).Trim();
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            info.Goodname = TypeParse.ObjToString(reader["goodname"]).Trim();
            info.Price = decimal.Parse(reader["price"].ToString());
            info.Buynum = TypeParse.StrToInt(reader["buynum"], 0);
            return info;
        }

        public static DataTable GetShopgoodDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetShopgoodDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion

        #region Shoporder
        public static int AddShoporder(ShoporderInfo info)
        {
            return DatabaseProvider.GetInstance().AddShoporder(info);
        }

        public static DataTable GetBackShoporder(int backday, string fields)
        {
            return DatabaseProvider.GetInstance().GetBackShoporder(backday, fields);
        }

        public static int DelShoporder(string oid)
        {
            return DatabaseProvider.GetInstance().DelShoporder(oid);
        }

        public static int EditShoporder(ShoporderInfo info)
        {
            return DatabaseProvider.GetInstance().EditShoporder(info);
        }

        public static bool DelShoporder(int id)
        {
            return DatabaseProvider.GetInstance().DelShoporder(id) > 0;
        }

        public static ShoporderInfo GetShoporder(int id)
        {
            ShoporderInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetShoporder(id))
            {
                if (reader.Read())
                {
                    info = LoadShoporderInfo(reader);
                }
            }
            return info;
        }

        public static ShoporderInfo GetShoporder(string oid, int uid)
        {
            ShoporderInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetShoporder(oid, uid))
            {
                if (reader.Read())
                {
                    info = LoadShoporderInfo(reader);
                }
            }
            return info;
        }

        public static ShoporderInfo GetShoporder(string oid)
        {
            ShoporderInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetShoporder(oid))
            {
                if (reader.Read())
                {
                    info = LoadShoporderInfo(reader);
                }
            }
            return info;
        }

        private static ShoporderInfo LoadShoporderInfo(IDataReader reader)
        {
            ShoporderInfo info = new ShoporderInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Oid = TypeParse.ObjToString(reader["oid"]).Trim();
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            info.Gtype = (GoodType)byte.Parse(reader["gtype"].ToString());
            info.Did = TypeParse.StrToInt(reader["did"], 0);
            info.Pid = TypeParse.StrToInt(reader["pid"], 0);
            info.Adrid = TypeParse.StrToInt(reader["adrid"], 0);
            info.Cartcount = TypeParse.StrToInt(reader["cartcount"], 0);
            info.Dprice = decimal.Parse(reader["dprice"].ToString());
            info.Price = decimal.Parse(reader["price"].ToString());
            info.Totalprice = decimal.Parse(reader["totalprice"].ToString());
            info.Status = (OrderStatus)TypeParse.ObjectToInt(reader["status"], 3);
            info.Ip = TypeParse.ObjToString(reader["ip"]).Trim();
            info.Isinvoice = byte.Parse(TypeParse.ObjToString(reader["isinvoice"]));
            info.Invoicehead = TypeParse.ObjToString(reader["invoicehead"]).Trim();
            info.Invoicecost = decimal.Parse(reader["invoicecost"].ToString());
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Remark = TypeParse.ObjToString(reader["remark"]).Trim();
            info.Parms = TypeParse.ObjToString(reader["parms"]);
            return info;
        }

        public static string GetShoporderSearchCondition(int gtype, int status, string oid, int pid, int did, string usernamelist, string startdate, string enddate, string sprice, string eprice)
        {
            return DatabaseProvider.GetInstance().GetShoporderSearchCondition(gtype, status, oid, pid, did, usernamelist, startdate, enddate, sprice, eprice);
        }

        //public static DataTable GetShoporderDataPageWithAddr(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        //{
        //    return DatabaseProvider.GetInstance().GetShoporderDataPageWithAddr(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        //}

        public static DataTable GetShoporderDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetShoporderDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }


        #endregion

        #region Useraddress
        public static int AddUseraddress(UseraddressInfo info)
        {
            return DatabaseProvider.GetInstance().AddUseraddress(info);
        }

        public static DataTable GetUserAddressTableByUid(int uid)
        {
            return DatabaseProvider.GetInstance().GetUserAddressTableByUid(uid);
        }

        public static int EditUseraddress(UseraddressInfo info)
        {
            return DatabaseProvider.GetInstance().EditUseraddress(info);
        }

        public static bool DelUseraddress(int id)
        {
            return DatabaseProvider.GetInstance().DelUseraddress(id) > 0;
        }

        public static UseraddressInfo GetUseraddress(int id)
        {
            UseraddressInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUseraddress(id))
            {
                if (reader.Read())
                {
                    info = LoadUseraddressInfo(reader);
                }
            }
            return info;
        }

        private static UseraddressInfo LoadUseraddressInfo(IDataReader reader)
        {
            UseraddressInfo info = new UseraddressInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            info.Title = TypeParse.ObjToString(reader["title"]).Trim();
            info.Email = TypeParse.ObjToString(reader["email"]).Trim();
            info.Address = TypeParse.ObjToString(reader["address"]).Trim();
            info.Postcode = TypeParse.ObjToString(reader["postcode"]).Trim();
            info.Phone = TypeParse.ObjToString(reader["phone"]).Trim();
            info.Parms = TypeParse.ObjToString(reader["parms"]).Trim();
            return info;
        }

        public static DataTable GetUseraddressDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetUseraddressDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion

        #region Paylog
        public static int AddPaylog(PaylogInfo info)
        {
            return DatabaseProvider.GetInstance().AddPaylog(info);
        }

        public static int EditPaylog(PaylogInfo info)
        {
            return DatabaseProvider.GetInstance().EditPaylog(info);
        }

        public static bool DelPaylog(int id)
        {
            return DatabaseProvider.GetInstance().DelPaylog(id) > 0;
        }

        public static PaylogInfo GetPaylog(int id)
        {
            PaylogInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPaylog(id))
            {
                if (reader.Read())
                {
                    info = LoadPaylogInfo(reader);
                }
            }
            return info;
        }

        private static PaylogInfo LoadPaylogInfo(IDataReader reader)
        {
            PaylogInfo info = new PaylogInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Oid = TypeParse.ObjToString(reader["oid"]).Trim();
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            info.Title = TypeParse.ObjToString(reader["title"]).Trim();
            info.Gtype = (GoodType)byte.Parse(TypeParse.ObjToString(reader["gtype"]));
            info.Amount = decimal.Parse(reader["amount"].ToString());
            info.Payid = TypeParse.StrToInt(reader["payid"], 0);
            info.Payname = TypeParse.ObjToString(reader["payname"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            return info;
        }

        public static string GetPaylogSearchCondition(int gtype, string oid, int pid, string usernamelist, string startdate, string enddate, string sprice, string eprice)
        {
            return DatabaseProvider.GetInstance().GetPaylogSearchCondition(gtype, oid, pid, usernamelist, startdate, enddate, sprice, eprice);
        }

        public static DataTable GetPaylogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetPaylogDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion

    }
}
