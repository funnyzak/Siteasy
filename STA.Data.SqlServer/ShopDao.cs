using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

using STA.Data;
using STA.Config;
using STA.Common;
using STA.Entity;

namespace STA.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        #region Payment
        private DbParameter[] EntityPayment(PaymentInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@dll",(DbType)SqlDbType.NVarChar,50,info.Dll),
				DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,30,info.Name),
				DbHelper.MakeInParam("@description",(DbType)SqlDbType.NVarChar,700,info.Description),
				DbHelper.MakeInParam("@pic",(DbType)SqlDbType.NVarChar,100,info.Pic),
				DbHelper.MakeInParam("@isvalid",(DbType)SqlDbType.TinyInt,1,info.Isvalid),
				DbHelper.MakeInParam("@parms",(DbType)SqlDbType.NVarChar,500,info.Parms),
				DbHelper.MakeInParam("@version",(DbType)SqlDbType.NVarChar,30,info.Version),
				DbHelper.MakeInParam("@author",(DbType)SqlDbType.NVarChar,30,info.Author),
				DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        public int AddPayment(PaymentInfo info)
        {
            if (ExistPayment(info.Dll)) return 0;

            string commandText = string.Format("INSERT INTO [{0}payments] ([dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (@dll, @name, @description, @pic, @isvalid, @parms, @version, @author, @orderid);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityPayment(info)));
        }

        private bool ExistPayment(string dll)
        {
            string commandText = string.Format("SELECT TOP 1 id FROM [{0}payments] WHERE [{0}payments].[dll] = '{1}'", BaseConfigs.GetTablePrefix, dll);
            return DbHelper.ExecuteScalar(CommandType.Text, commandText) != null;
        }

        public DataTable GetPaymentTable(string fields)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {0} FROM [{1}payments] ORDER BY [{1}payments].[orderid] DESC", fields.Trim() == "" ? "*" : fields, BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public int EditPayment(PaymentInfo info)
        {
            string commandText = string.Format("UPDATE [{0}paylogs] SET [payname] = @name WHERE [payid] = @id;UPDATE [{0}payments] SET [dll] = @dll, [name] = @name, [description] = @description, [pic] = @pic, [isvalid] = @isvalid, [parms] = @parms, [version] = @version, [author] = @author, [orderid] = @orderid WHERE [{0}payments].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityPayment(info));
        }

        public int DelPayment(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}payments] WHERE [{0}payments].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int DelPayment(string dll)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}payments] WHERE [{0}payments].[dll] = '{1}'", BaseConfigs.GetTablePrefix, dll));
        }

        public IDataReader GetPayment(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}payments] WHERE [{0}payments].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetPayment(string dll)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}payments] WHERE [{0}payments].[dll] = @dll", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@dll", (DbType)SqlDbType.NVarChar, 50, dll));
        }

        public DataTable GetPaymentDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}payments", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion

        #region Shopdelivery
        private DbParameter[] EntityShopdelivery(ShopdeliveryInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
				DbHelper.MakeInParam("@ename",(DbType)SqlDbType.NVarChar,50,info.Ename),
				DbHelper.MakeInParam("@description",(DbType)SqlDbType.NVarChar,300,info.Description),
				DbHelper.MakeInParam("@fweight",(DbType)SqlDbType.Decimal,9,info.Fweight),
				DbHelper.MakeInParam("@iscod",(DbType)SqlDbType.TinyInt,1,info.Iscod),
				DbHelper.MakeInParam("@parms",(DbType)SqlDbType.NVarChar,200,info.Parms),
				DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        public int AddShopdelivery(ShopdeliveryInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}shopdelivery] ([name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (@name, @ename, @description, @fweight, @iscod, @parms, @orderid);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityShopdelivery(info)));
        }

        public int EditShopdelivery(ShopdeliveryInfo info)
        {
            string commandText = string.Format("UPDATE [{0}shopdelivery] SET [name] = @name, [ename] = @ename, [description] = @description, [fweight] = @fweight, [iscod] = @iscod, [parms] = @parms, [orderid] = @orderid WHERE [{0}shopdelivery].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityShopdelivery(info));
        }

        public int DelShopdelivery(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}shopdelivery] WHERE [{0}shopdelivery].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetShopdelivery(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shopdelivery] WHERE [{0}shopdelivery].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetDeliveryTable(string fields)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {0} FROM [{1}shopdelivery] ORDER BY [{1}shopdelivery].[orderid] DESC", fields.Trim() == "" ? "*" : fields, BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetShopdeliveryDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}shopdelivery", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion

        #region Shopgood
        private DbParameter[] EntityShopgood(ShopgoodInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@gtype",(DbType)SqlDbType.TinyInt,1,info.Gtype),
				DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int,4,info.Cid),
				DbHelper.MakeInParam("@oid",(DbType)SqlDbType.NVarChar,30,info.Oid),
				DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
				DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
				DbHelper.MakeInParam("@goodname",(DbType)SqlDbType.NVarChar,100,info.Goodname),
				DbHelper.MakeInParam("@price",(DbType)SqlDbType.Decimal,9,info.Price),
				DbHelper.MakeInParam("@buynum",(DbType)SqlDbType.Int,4,info.Buynum)
            };
        }

        public int AddShopgood(ShopgoodInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}shopgoods] ([gtype], [cid], [oid], [uid], [username], [goodname], [price], [buynum]) VALUES (@gtype, @cid, @oid, @uid, @username, @goodname, @price, @buynum);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityShopgood(info)));
        }

        public int EditShopgood(ShopgoodInfo info)
        {
            string commandText = string.Format("UPDATE [{0}shopgoods] SET [gtype] = @gtype, [cid] = @cid, [oid] = @oid, [uid] = @uid, [username] = @username, [goodname] = @goodname, [price] = @price, [buynum] = @buynum WHERE [{0}shopgoods].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityShopgood(info));
        }

        public DataTable GetShopgoodByOid(string oid, string fields)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {0} FROM [{1}shopgoods] WHERE [{1}shopgoods].[oid] = '{2}'", fields.Trim() == "" ? "*" : fields, BaseConfigs.GetTablePrefix, RplDanField(oid))).Tables[0];
        }

        public DataTable GetShopgoodTableByOid(string oid, string fields)
        {
            string commandText = string.Format("SELECT c.id,c.typeid{0} FROM [{1}contents] c INNER JOIN [{1}extproducts] p ON c.[id] = p.[cid] INNER JOIN [{1}shopgoods] g ON g.[cid] = p.[cid] WHERE g.[oid] = '{2}'", fields.Trim() == "" ? "" : ("," + fields), BaseConfigs.GetTablePrefix, RplDanField(oid));
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public int DelShopgood(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}shopgoods] WHERE [{0}shopgoods].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetShopgood(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shopgoods] WHERE [{0}shopgoods].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetShopgoodDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}shopgoods", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion

        #region Shoporder
        private DbParameter[] EntityShoporder(ShoporderInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@oid",(DbType)SqlDbType.NVarChar,30,info.Oid),
				DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
				DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
                DbHelper.MakeInParam("@gtype",(DbType)SqlDbType.TinyInt,1,(byte)info.Gtype),
				DbHelper.MakeInParam("@did",(DbType)SqlDbType.Int,4,info.Did),
				DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int,4,info.Pid),
                DbHelper.MakeInParam("@adrid",(DbType)SqlDbType.Int,4,info.Adrid),
				DbHelper.MakeInParam("@cartcount",(DbType)SqlDbType.Int,4,info.Cartcount),
				DbHelper.MakeInParam("@dprice",(DbType)SqlDbType.Decimal,9,info.Dprice),
				DbHelper.MakeInParam("@price",(DbType)SqlDbType.Decimal,9,info.Price),
				DbHelper.MakeInParam("@totalprice",(DbType)SqlDbType.Decimal,9,info.Totalprice),
				DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,(int)info.Status),
				DbHelper.MakeInParam("@ip",(DbType)SqlDbType.NVarChar,24,info.Ip),
                DbHelper.MakeInParam("@isinvoice",(DbType)SqlDbType.TinyInt,1,info.Isinvoice),
                DbHelper.MakeInParam("@invoicehead",(DbType)SqlDbType.NVarChar,30,info.Invoicehead),
                DbHelper.MakeInParam("@invoicecost",(DbType)SqlDbType.Decimal,9,info.Invoicecost),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
				DbHelper.MakeInParam("@remark",(DbType)SqlDbType.NVarChar,300,info.Remark),
                DbHelper.MakeInParam("@parms",(DbType)SqlDbType.NVarChar,500,info.Parms)
            };
        }

        public int AddShoporder(ShoporderInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}shoporders] ([oid], [uid], [username], [gtype], [did], [pid], [adrid], [cartcount], [dprice], [price], [totalprice], [status], [ip], [isinvoice], [invoicehead], [invoicecost], [addtime], [remark], [parms]) VALUES (@oid, @uid, @username, @gtype, @did, @pid, @adrid, @cartcount, @dprice, @price, @totalprice, @status, @ip, @isinvoice, @invoicehead, @invoicecost, @addtime, @remark, @parms);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityShoporder(info)));
        }

        public int EditShoporder(ShoporderInfo info)
        {
            string commandText = string.Format("UPDATE [{0}shoporders] SET [oid] = @oid, [gtype] = @gtype, [adrid] = @adrid, [uid] = @uid, [username] = @username, [did] = @did, [pid] = @pid, [cartcount] = @cartcount, [dprice] = @dprice, [price] = @price, [totalprice] = @totalprice, [status] = @status, [ip] = @ip, [isinvoice] = @isinvoice, [invoicehead]=@invoicehead, [invoicecost] = @invoicecost, [addtime] = @addtime, [remark] = @remark, [parms] = @parms WHERE [{0}shoporders].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityShoporder(info));
        }

        public int DelShoporder(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}shoporders] WHERE [{0}shoporders].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int DelShoporder(string oid)
        {
            string commandText = string.Format("DELETE FROM [{0}shopgoods] WHERE [{0}shopgoods].[oid] = '{1}';"
                                 + "DELETE FROM [{0}shoporders] WHERE [{0}shoporders].[oid] = '{1}'", BaseConfigs.GetTablePrefix, RplDanField(oid));
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetBackShoporder(int backday, string fields)
        {
            string commandText = string.Format("SELECT * FROM [{0}shoporders] WHERE [{0}shoporders].[status] = {1} AND [{0}shoporders].[addtime] < '{2}'", BaseConfigs.GetTablePrefix, (int)OrderStatus.未处理, DateTime.Now.AddDays(-backday).ToString("yyyy-MM-dd HH:mm:ss"));
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public IDataReader GetShoporder(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shoporders] WHERE [{0}shoporders].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetShoporder(string oid, int uid)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shoporders] WHERE [{0}shoporders].[uid] = {1} AND [{0}shoporders].[oid] = {2}", BaseConfigs.GetTablePrefix, uid, oid));
        }

        public IDataReader GetShoporder(string oid)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}shoporders] WHERE [{0}shoporders].[oid] = '{1}'", BaseConfigs.GetTablePrefix, RplDanField(oid)));
        }

        public string GetShoporderSearchCondition(int gtype, int status, string oid, int pid, int did, string usernamelist, string startdate, string enddate, string sprice, string eprice)
        {
            string condition = " id > 0 ";
            if (gtype > 0)
                condition += string.Format(" AND gtype = {0}", gtype);
            if (oid.Trim() != "")
                if (status > 0)
                    condition += string.Format(" AND status = {0}", status);
            if (pid > 0)
                condition += string.Format(" AND pid = {0}", pid);
            if (oid.Trim() != "")
                condition += string.Format(" AND oid = '{0}'", RplDanField(oid));
            if (did > 0)
                condition += string.Format(" AND did = {0}", did);
            if (startdate != string.Empty)
                condition += string.Format(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition += string.Format(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));

            usernamelist = RegSqlCharList(usernamelist);
            if (usernamelist != string.Empty)
                condition += string.Format(" AND username IN ({0})", usernamelist);

            decimal startprice = TypeParse.StrToDecimal(sprice, 0);
            decimal endprice = TypeParse.StrToDecimal(eprice, 0);

            if (startprice >= 0)
                condition += string.Format(" AND totalprice >= {0}", startprice);
            if (endprice > 0)
                condition += string.Format(" AND totalprice <= {0}", endprice);

            return condition;
        }

        //public DataTable GetShoporderDataPageWithAddr(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        //{
        //    return GetShoporderDataPage(string.Format("[{0}shoporders] INNER JOIN [{0}useraddress] ON [{0}shoporders].[adrid] = [{0}useraddress].[id]",
        //                                BaseConfigs.GetTablePrefix), fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        //}

        public int ShoporderCount()
        {
            string commandText = string.Format("SELECT COUNT(*) FROM [{0}shoporders]",BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public DataTable GetShoporderDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetShoporderDataPage(string.Format("[{0}shoporders]", BaseConfigs.GetTablePrefix), fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public DataTable GetShoporderDataPage(string table, string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(table, string.Format("{0}shoporders.id", BaseConfigs.GetTablePrefix), pagecurrent, pagesize, fields == "" ? "*" : fields,
                               string.Format("{0}shoporders.id desc", BaseConfigs.GetTablePrefix), condition, out pagecount, out recordcount);
        }
        #endregion

        #region Useraddress
        private DbParameter[] EntityUseraddress(UseraddressInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
				DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
				DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,30,info.Title),
				DbHelper.MakeInParam("@email",(DbType)SqlDbType.Char,100,info.Email),
				DbHelper.MakeInParam("@address",(DbType)SqlDbType.NVarChar,150,info.Address),
				DbHelper.MakeInParam("@postcode",(DbType)SqlDbType.NVarChar,20,info.Postcode),
				DbHelper.MakeInParam("@phone",(DbType)SqlDbType.NVarChar,50,info.Phone),
				DbHelper.MakeInParam("@parms",(DbType)SqlDbType.NVarChar,50,info.Parms)
            };
        }

        public int AddUseraddress(UseraddressInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}useraddress] ([uid], [username], [title], [email], [address], [postcode], [phone], [parms]) VALUES (@uid, @username, @title, @email, @address, @postcode, @phone, @parms);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityUseraddress(info)));
        }

        public int EditUseraddress(UseraddressInfo info)
        {
            string commandText = string.Format("UPDATE [{0}useraddress] SET [uid] = @uid, [username] = @username, [title] = @title, [email] = @email, [address] = @address, [postcode] = @postcode, [phone] = @phone, [parms] = @parms WHERE [{0}useraddress].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityUseraddress(info));
        }

        public int DelUseraddress(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}useraddress] WHERE [{0}useraddress].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetUseraddress(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}useraddress] WHERE [{0}useraddress].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetUserAddressTableByUid(int uid)
        {
            string commandText = string.Format("SELECT * FROM [{0}useraddress] WHERE [{0}useraddress].[uid] = {1}", BaseConfigs.GetTablePrefix, uid);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public DataTable GetUseraddressDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}useraddress", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion

        #region Paylog
        private DbParameter[] EntityPaylog(PaylogInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@oid",(DbType)SqlDbType.NVarChar,30,info.Oid),
				DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
				DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
				DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,100,info.Title),
				DbHelper.MakeInParam("@gtype",(DbType)SqlDbType.TinyInt,1,info.Gtype),
				DbHelper.MakeInParam("@amount",(DbType)SqlDbType.Decimal,9,info.Amount),
				DbHelper.MakeInParam("@payid",(DbType)SqlDbType.Int,4,info.Payid),
				DbHelper.MakeInParam("@payname",(DbType)SqlDbType.NVarChar,50,info.Payname),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime)
            };
        }

        public int AddPaylog(PaylogInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}paylogs] ([oid], [uid], [username], [title], [gtype], [amount], [payid], [payname], [addtime]) VALUES (@oid, @uid, @username, @title, @gtype, @amount, @payid, @payname, @addtime);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityPaylog(info)));
        }

        public int EditPaylog(PaylogInfo info)
        {
            string commandText = string.Format("UPDATE [{0}paylogs] SET [oid] = @oid, [uid] = @uid, [username] = @username, [title] = @title, [gtype] = @gtype, [amount] = @amount, [payid] = @payid, [payname] = @payname, [addtime] = @addtime WHERE [{0}paylogs].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityPaylog(info));
        }

        public int DelPaylog(int id)
        {
            string commandText = string.Format("DELETE FROM [{0}paylogs] WHERE [{0}paylogs].[id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetPaylog(int id)
        {
            string commandText = string.Format("SELECT TOP 1 * FROM [{0}paylogs] WHERE [{0}paylogs].[id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public string GetPaylogSearchCondition(int gtype, string oid, int pid, string usernamelist, string startdate, string enddate, string sprice, string eprice)
        {
            string condition = " id > 0 ";
            if (pid > 0)
                condition += string.Format(" AND payid = {0}", pid);
            if (gtype > 0)
                condition += string.Format(" AND gtype = {0}", gtype);
            if (oid.Trim() != "")
                condition += string.Format(" AND oid = '{0}'", RplDanField(oid));
            if (startdate != string.Empty)
                condition += string.Format(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition += string.Format(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));

            usernamelist = RegSqlCharList(usernamelist);
            if (usernamelist != string.Empty)
                condition += string.Format(" AND username IN ({0})", usernamelist);

            decimal startprice = TypeParse.StrToDecimal(sprice, 0);
            decimal endprice = TypeParse.StrToDecimal(eprice, 0);

            if (startprice >= 0)
                condition += string.Format(" AND amount >= {0}", startprice);
            if (endprice > 0)
                condition += string.Format(" AND amount <= {0}", endprice);

            return condition;
        }

        public DataTable GetPaylogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}paylogs", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion
    }
}
