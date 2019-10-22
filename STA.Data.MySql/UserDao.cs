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

namespace STA.Data.MySql
{
    public partial class DataProvider : IDataProvider
    {

        public int AddUserField(UserfieldInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createuserfield", BaseConfigs.GetTablePrefix), EntityUserField(info)));
        }

        public int DelUserField(int uid)
        {
            string commandSql = string.Format("DELETE FROM `{0}userfields` WHERE `{0}userfields`.`uid` = {1}", BaseConfigs.GetTablePrefix, uid);
            return DbHelper.ExecuteNonQuery(commandSql);
        }

        public int EditUserField(UserfieldInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateuserfield", BaseConfigs.GetTablePrefix), EntityUserField(info));
        }

        public IDataReader GetUserField(int uid)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}userfields` WHERE `{0}userfields`.`uid` = {1}", BaseConfigs.GetTablePrefix, uid));
        }

        public int AddUser(UserInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createuser", BaseConfigs.GetTablePrefix), EntityUser(info)));
        }

        public int LockUser(int uid, int locked)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}users` SET locked = {1} WHERE `{0}users`.`id` = {2}", BaseConfigs.GetTablePrefix, locked.ToString(), uid.ToString()));
        }

        public int DelUser(int id)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deleteuser", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        public int UserCount()
        {
            string commandText = string.Format("SELECT COUNT(*) FROM `{0}users`", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public int EditUser(UserInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateuser", BaseConfigs.GetTablePrefix), EntityUser(info));
        }

        public IDataReader GetUser(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}users` WHERE `{0}users`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetUser(string username)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}users` WHERE `{0}users`.`username` = '{1}'", BaseConfigs.GetTablePrefix,
                                                                                                                                            username.Trim()));
        }

        public int CheckUserEmailExist(string email)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT id FROM `{0}users` WHERE `{0}users`.`email` = '{1}'", BaseConfigs.GetTablePrefix, email)), 0);
        }

        public int CheckUserNameExist(string username)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT id FROM `{0}users` WHERE `{0}users`.`username` = '{1}'", BaseConfigs.GetTablePrefix, username)), 0);
        }

        public int ExistUser(string username)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT `id` FROM `{0}users` WHERE `{0}users`.`username` = '{1}'",
                                                                                                            BaseConfigs.GetTablePrefix, RegEsc(username.Trim()))), 0);
        }

        public IDataReader CheckUserLogin(string username, string password, int system)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}users` WHERE `{0}users`.`username` = '{1}' AND "
                                                                         + "`{0}users`.`password` = '{2}' {3}", BaseConfigs.GetTablePrefix,
                                                                         RegEsc(username.Trim()), RegEsc(password), (system == 1 ? "AND [" + BaseConfigs.GetTablePrefix +
                                                                         "users].`adminid` > 0" : string.Empty)));
        }

        public DataTable GetUserDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "users", "id", pagecurrent, pagesize, "*", "id desc", where, out pagecount, out recordcount);
        }

        public int AddUserGroup(UserGroupInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createusergroup", BaseConfigs.GetTablePrefix), EntityUserGroup(info)));
        }

        public int EditUserGroup(UserGroupInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateusergroup", BaseConfigs.GetTablePrefix), EntityUserGroup(info));
        }

        public int DelUserGroup(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}menurelations` WHERE `{0}menurelations`.`groupid` = {1};"
                             + "DELETE FROM `{0}usergroups` WHERE `{0}usergroups`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetUserGroup(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}usergroups` WHERE `{0}usergroups`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetUserGroupTable()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}usergroups`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetUserGroupDataPage(int pagecurrent, int pagesize, int system, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "usergroups", "id", pagecurrent, pagesize, "*", "id desc", system >= 0 ? ("system = " + system.ToString()) : "", out pagecount, out recordcount);
        }


        public int AddAdminLog(AdminLogInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("INSERT INTO `{0}adminlogs` (`uid`, `username`, `groupid`, `groupname`, `ip`, `addtime`, `action`, `remark`, `admintype`) VALUES (@uid, @username, @groupid, @groupname, @ip, @addtime, @action, @remark, @admintype);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix), EntityAdminLog(info)));
        }

        public int DelAdminLog(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}adminlogs` WHERE `{0}adminlogs`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetAdminLogDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "adminlogs", "id", pagecurrent, pagesize, "*", "id desc", where, out pagecount, out recordcount);
        }


        public int DelAdminlogWhere(string where)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}adminlogs`{1}", BaseConfigs.GetTablePrefix, (where == "" ? "" : " WHERE " + where)));
        }

        public IDataReader GetUserGroupZeroScoreId()
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT top 1 * FROM `{0}usergroups` WHERE `creditsmin` = 0 AND `system` = 0", BaseConfigs.GetTablePrefix));
        }

        public string GetAdminlogSearchCondition(int admintype, string startdate, string enddate, string users, string keywords)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (admintype < 0)
                condition.Append(" AND admintype > 0");
            else
                condition.Append(" AND admintype = " + admintype.ToString());
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            if (keywords != string.Empty)
                condition.AppendFormat(" AND (action LIKE '%{0}%' OR remark LIKE '%{0}%')", RegEsc(keywords));
            users = RegSqlCharList(users);
            if (users != string.Empty)
                condition.AppendFormat(" AND username IN ({0})", users);
            return condition.ToString();
        }

        public string GetUserSearchCondition(int gender, int status, string regstartdate, string regenddate, string actionstartdate, string actionenddate, int groupid,
                                                                                                                        string username, string nickname, string email)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (gender >= 0)
                condition.AppendFormat(" AND gender = {0}", gender.ToString());
            if (status >= 0)
                condition.AppendFormat(" AND ischeck = {0}", status.ToString());
            if (regstartdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(regstartdate));
            if (regenddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(regenddate).AddDays(1));
            if (actionstartdate != string.Empty)
                condition.AppendFormat(" AND logintime >= '{0}'", TypeParse.StrToDateTime(actionstartdate));
            if (actionenddate != string.Empty)
                condition.AppendFormat(" AND logintime <= '{0}'", TypeParse.StrToDateTime(actionenddate).AddDays(1));
            if (groupid > 0)
                condition.AppendFormat(" AND (groupid = {0} OR adminid= {0})", groupid.ToString());
            if (username.Trim() != string.Empty)
                condition.AppendFormat(" AND username like '%{0}%'", RegEsc(username));
            if (nickname.Trim() != string.Empty)
                condition.AppendFormat(" AND nickname like '%{0}%'", RegEsc(nickname));
            if (email.Trim() != string.Empty)
                condition.AppendFormat(" AND email like '%{0}%'", RegEsc(email));
            return condition.ToString();
        }

        public DataTable GetMailTable(string uids)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT id,username,email FROM `{0}users` WHERE id IN ({1})", BaseConfigs.GetTablePrefix, RegEsc(uids))).Tables[0];
        }

        public object LastLoginTime(int uid)
        {
            return DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT TOP 1 addtime FROM `{0}adminlogs` WHERE id not in (SELECT TOP 1 id FROM `{0}adminlogs` WHERE `{0}adminlogs`.`uid` = {1} AND `{0}adminlogs`.`action` = '系统登录' ORDER BY ID DESC) AND `{0}adminlogs`.`uid` = {1} AND `{0}adminlogs`.`action` = '系统登录' ORDER BY ID DESC", BaseConfigs.GetTablePrefix, uid.ToString()));
        }

        private DbParameter[] EntityUserauth(UserauthInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,info.Userid),
                DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
                DbHelper.MakeInParam("@email",(DbType)SqlDbType.NVarChar,200,info.Email),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@expirs",(DbType)SqlDbType.Int,4,info.Expirs),
                DbHelper.MakeInParam("@atype",(DbType)SqlDbType.TinyInt,1,(byte)info.Atype),
                DbHelper.MakeInParam("@code",(DbType)SqlDbType.Char,30,info.Code)
            };
        }

        public int AddUserauth(UserauthInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}userauths` (`userid`, `username`, `email`, `addtime`, `expirs`, `atype`, `code`) VALUES (@userid, @username, @email, @addtime, @expirs, @atype, @code);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityUserauth(info)));
        }
        public IDataReader GetUserauth(string code, AuthType atype)
        {
            string commandText = string.Format("select * from `{0}userauths` where code = '{1}' and atype = {2}", BaseConfigs.GetTablePrefix, code, (byte)atype);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetUserauthByUsername(string username, AuthType atype)
        {
            string commandText = string.Format("select top 1 * from `{0}userauths` where username = '{1}' and atype = {2} order by id desc", BaseConfigs.GetTablePrefix, username, (byte)atype);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public int DelUserauth(int id)
        {
            string commandText = string.Format("delete from `{0}userauths` where id = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        private DbParameter[] EntityAdminLog(AdminLogInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
                DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
                DbHelper.MakeInParam("@groupid",(DbType)SqlDbType.Int,4,info.Groupid),
                DbHelper.MakeInParam("@groupname",(DbType)SqlDbType.Char,50,info.Groupname),
                DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15,info.Ip),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@action",(DbType)SqlDbType.NVarChar,100,info.Action),
                DbHelper.MakeInParam("@remark",(DbType)SqlDbType.NVarChar,300,info.Remark),
                DbHelper.MakeInParam("@admintype",(DbType)SqlDbType.TinyInt,1,info.Admintype)
            };
        }

        private DbParameter[] EntityUser(UserInfo info)
        {
            return new DbParameter[] {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
                DbHelper.MakeInParam("@nickname",(DbType)SqlDbType.NChar,20,info.Nickname),
                DbHelper.MakeInParam("@password",(DbType)SqlDbType.Char,32,info.Password),
                DbHelper.MakeInParam("@safecode",(DbType)SqlDbType.Char,32,info.Safecode),
                DbHelper.MakeInParam("@gender",(DbType)SqlDbType.TinyInt,1,info.Gender),
                DbHelper.MakeInParam("@adminid",(DbType)SqlDbType.Int,4,info.Adminid),
                DbHelper.MakeInParam("@admingroupname",(DbType)SqlDbType.NVarChar,30,info.Admingroupname),
                DbHelper.MakeInParam("@groupname",(DbType)SqlDbType.NVarChar,30,info.Groupname),
                DbHelper.MakeInParam("@locked",(DbType)SqlDbType.TinyInt,1,info.Locked),
                DbHelper.MakeInParam("@groupid",(DbType)SqlDbType.Int,4,info.Groupid),
                DbHelper.MakeInParam("@extgroupids",(DbType)SqlDbType.Char,100,info.Extgroupids),
                DbHelper.MakeInParam("@regip",(DbType)SqlDbType.Char,15,info.Regip),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@loginip",(DbType)SqlDbType.Char,15,info.Loginip),
                DbHelper.MakeInParam("@logintime",(DbType)SqlDbType.DateTime,8,info.Logintime),
                DbHelper.MakeInParam("@lastaction",(DbType)SqlDbType.DateTime,8,info.Lastaction),
                DbHelper.MakeInParam("@money",(DbType)SqlDbType.Decimal,0,info.Money),
                DbHelper.MakeInParam("@credits",(DbType)SqlDbType.Int,4,info.Credits),
                DbHelper.MakeInParam("@extcredits1",(DbType)SqlDbType.Int,4,info.Extcredits1),
                DbHelper.MakeInParam("@extcredits2",(DbType)SqlDbType.Int,4,info.Extcredits2),
                DbHelper.MakeInParam("@extcredits3",(DbType)SqlDbType.Int,4,info.Extcredits3),
                DbHelper.MakeInParam("@extcredits4",(DbType)SqlDbType.Int,4,info.Extcredits4),
                DbHelper.MakeInParam("@extcredits5",(DbType)SqlDbType.Int,4,info.Extcredits5),
                DbHelper.MakeInParam("@email",(DbType)SqlDbType.Char,100,info.Email),
                DbHelper.MakeInParam("@ischeck",(DbType)SqlDbType.TinyInt,1,info.Ischeck)
            };
        }

        private DbParameter[] EntityUserGroup(UserGroupInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@system",(DbType)SqlDbType.TinyInt,4,info.System),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.Char,50,info.Name),
                DbHelper.MakeInParam("@creditsmax",(DbType)SqlDbType.Int,4,info.Creditsmax),
                DbHelper.MakeInParam("@creditsmin",(DbType)SqlDbType.Int,4,info.Creditsmin),
                DbHelper.MakeInParam("@color",(DbType)SqlDbType.Char,7,info.Color),
                DbHelper.MakeInParam("@avatar",(DbType)SqlDbType.NChar,100,info.Avatar),
                DbHelper.MakeInParam("@star",(DbType)SqlDbType.Int,4,info.Star)
            };
        }

        private DbParameter[] EntityUserField(UserfieldInfo info)
        {
            return new DbParameter[] {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
                DbHelper.MakeInParam("@realname",(DbType)SqlDbType.NChar,20,info.Realname),
                DbHelper.MakeInParam("@idcard",(DbType)SqlDbType.Char,30,info.Idcard),
                DbHelper.MakeInParam("@signature",(DbType)SqlDbType.NChar,20,info.Signature),
                DbHelper.MakeInParam("@description",(DbType)SqlDbType.NText,0,info.Description),
                DbHelper.MakeInParam("@areaid",(DbType)SqlDbType.Int,4,info.Areaid),
                DbHelper.MakeInParam("@areaname",(DbType)SqlDbType.Char,20,info.Areaname),
                DbHelper.MakeInParam("@address",(DbType)SqlDbType.NVarChar,50,info.Address),
                DbHelper.MakeInParam("@postcode",(DbType)SqlDbType.Char,10,info.Postcode),
                DbHelper.MakeInParam("@hometel",(DbType)SqlDbType.Char,20,info.Hometel),
                DbHelper.MakeInParam("@worktel",(DbType)SqlDbType.Char,20,info.Worktel),
                DbHelper.MakeInParam("@mobile",(DbType)SqlDbType.Char,20,info.Mobile),
                DbHelper.MakeInParam("@icq",(DbType)SqlDbType.Char,20,info.Icq),
                DbHelper.MakeInParam("@qq",(DbType)SqlDbType.Char,20,info.Qq),
                DbHelper.MakeInParam("@skype",(DbType)SqlDbType.NChar,100,info.Skype),
                DbHelper.MakeInParam("@msn",(DbType)SqlDbType.NChar,100,info.Msn),
                DbHelper.MakeInParam("@website",(DbType)SqlDbType.NChar,200,info.Website)
            };
        }

        #region Pms
        private DbParameter[] EntityPms(PrivateMessageInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@msgfrom",(DbType)SqlDbType.NChar,20,info.Msgfrom),
				DbHelper.MakeInParam("@msgfromid",(DbType)SqlDbType.Int,4,info.Msgfromid),
				DbHelper.MakeInParam("@msgto",(DbType)SqlDbType.NChar,20,info.Msgto),
				DbHelper.MakeInParam("@msgtoid",(DbType)SqlDbType.Int,4,info.Msgtoid),
				DbHelper.MakeInParam("@folder",(DbType)SqlDbType.TinyInt,1,(byte)info.Folder),
				DbHelper.MakeInParam("@new",(DbType)SqlDbType.Int,4,info.New),
				DbHelper.MakeInParam("@subject",(DbType)SqlDbType.NVarChar,60,info.Subject),
				DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime)
            };
        }

        public int AddPms(PrivateMessageInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}pms` (`msgfrom`, `msgfromid`, `msgto`, `msgtoid`, `folder`, `new`, `subject`, `content`, `addtime`) VALUES (@msgfrom, @msgfromid, @msgto, @msgtoid, @folder, @new, @subject, @content, @addtime);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityPms(info)));
        }

        public int EditPms(PrivateMessageInfo info)
        {
            string commandText = string.Format("UPDATE `{0}pms` SET `msgfrom` = @msgfrom, `msgfromid` = @msgfromid, `msgto` = @msgto, `msgtoid` = @msgtoid, `folder` = @folder, `new` = @new, `subject` = @subject, `content` = @content, `addtime` = @addtime WHERE `{0}pms`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityPms(info));
        }

        public int DelPms(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}pms` WHERE `{0}pms`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetPms(int id)
        {
            string commandText = string.Format("SELECT TOP 1 * FROM `{0}pms` WHERE `{0}pms`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetPmsDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}pms", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion
    }
}