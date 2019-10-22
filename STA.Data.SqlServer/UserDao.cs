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

        public int AddUserField(UserfieldInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createuserfield", BaseConfigs.GetTablePrefix), EntityUserField(info)));
        }

        public int DelUserField(int uid)
        {
            string commandSql = string.Format("DELETE FROM [{0}userfields] WHERE [{0}userfields].[uid] = {1}", BaseConfigs.GetTablePrefix, uid);
            return DbHelper.ExecuteNonQuery(commandSql);
        }

        public int EditUserField(UserfieldInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateuserfield", BaseConfigs.GetTablePrefix), EntityUserField(info));
        }

        public IDataReader GetUserField(int uid)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}userfields] WHERE [{0}userfields].[uid] = {1}", BaseConfigs.GetTablePrefix, uid));
        }

        public IDataReader GetUserFieldByUcid(int ucid)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}userfields] WHERE [{0}userfields].[ucid] = {1}", BaseConfigs.GetTablePrefix, ucid));
        }

        public int AddUser(UserInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createuser", BaseConfigs.GetTablePrefix), EntityUser(info)));
        }

        public int LockUser(int uid, int locked)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE [{0}users] SET locked = {1} WHERE [{0}users].[id] = {2}", BaseConfigs.GetTablePrefix, locked.ToString(), uid.ToString()));
        }

        public int DelUser(int id)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deleteuser", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        public int UserCount()
        {
            string commandText = string.Format("SELECT COUNT(*) FROM [{0}users]", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public int EditUser(UserInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateuser", BaseConfigs.GetTablePrefix), EntityUser(info));
        }

        public IDataReader GetUser(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}users] WHERE [{0}users].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetUser(string username)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}users] WHERE [{0}users].[username] = '{1}'", BaseConfigs.GetTablePrefix,
                                                                                                                                            username.Trim()));
        }

        public int CheckUserEmailExist(string email)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT id FROM [{0}users] WHERE [{0}users].[email] = '{1}'", BaseConfigs.GetTablePrefix, email)), 0);
        }

        public int CheckUserNameExist(string username)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT id FROM [{0}users] WHERE [{0}users].[username] = '{1}'", BaseConfigs.GetTablePrefix, username)), 0);
        }

        public int ExistUser(string username)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT [id] FROM [{0}users] WHERE [{0}users].[username] = '{1}'",
                                                                                                            BaseConfigs.GetTablePrefix, RegEsc(username.Trim()))), 0);
        }

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmnum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        public int SetUserNewPMCount(int uid, int pmNum)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid), 
									   DbHelper.MakeInParam("@value", (DbType)SqlDbType.Int, 4, pmNum)
			                      };
            string commandText = string.Format("UPDATE [{0}users] SET [newpmcount]=@value WHERE [id]=@uid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public IDataReader CheckUserLogin(string username, string password, int system)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}users] WHERE [{0}users].[username] = '{1}' AND "
                                                                         + "[{0}users].[password] = '{2}' {3}", BaseConfigs.GetTablePrefix,
                                                                         RegEsc(username.Trim()), RegEsc(password), (system == 1 ? "AND [" + BaseConfigs.GetTablePrefix +
                                                                         "users].[adminid] > 0" : string.Empty)));
        }

        public DataTable GetUserDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "users", "id", pagecurrent, pagesize, "*", "id desc", where, out pagecount, out recordcount);
        }

        public DataTable GetUserListByGroupid(int top, string fields, int start_uid, string groupIdList)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT top {0} {1} FROM [{2}users] WHERE (groupid IN({3}) OR adminid IN({3})) AND id > {4} Order By id Asc", top, fields == "" ? DbFields.User : fields, BaseConfigs.GetTablePrefix, groupIdList, start_uid)).Tables[0];
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
            string commandText = string.Format("DELETE FROM [{0}menurelations] WHERE [{0}menurelations].[groupid] = {1};"
                             + "DELETE FROM [{0}usergroups] WHERE [{0}usergroups].[id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetUserGroup(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}usergroups] WHERE [{0}usergroups].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetUserGroupTable()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM [{0}usergroups]", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetUserGroupDataPage(int pagecurrent, int pagesize, int system, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "usergroups", "id", pagecurrent, pagesize, "*", "id desc", system >= 0 ? ("system = " + system.ToString()) : "", out pagecount, out recordcount);
        }


        public int AddAdminLog(AdminLogInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createadminlog", BaseConfigs.GetTablePrefix), EntityAdminLog(info)));
        }

        public int DelAdminLog(int id)
        {
            string commandText = string.Format("DELETE FROM [{0}adminlogs] WHERE [{0}adminlogs].[id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetAdminLogDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "adminlogs", "id", pagecurrent, pagesize, "*", "id desc", where, out pagecount, out recordcount);
        }


        public int DelAdminlogWhere(string where)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}adminlogs]{1}", BaseConfigs.GetTablePrefix, (where == "" ? "" : " WHERE " + where)));
        }

        public IDataReader GetUserGroupZeroScoreId()
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT top 1 * FROM [{0}usergroups] WHERE [creditsmin] = 0 AND [system] = 0", BaseConfigs.GetTablePrefix));
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

        public DataTable GetUserListByUids(string fields, string uids)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {2} FROM [{0}users] WHERE id IN ({1})", BaseConfigs.GetTablePrefix, uids, fields == "" ? "*" : fields)).Tables[0];
        }

        public DataTable GetUserListByUsernames(string fields, string usernames)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {2} FROM [{0}users] WHERE username IN ({1})", BaseConfigs.GetTablePrefix, RegSqlCharList(usernames), fields == "" ? "*" : fields)).Tables[0];
        }

        public object LastLoginTime(int uid)
        {
            return DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT TOP 1 addtime FROM [{0}adminlogs] WHERE id not in (SELECT TOP 1 id FROM [{0}adminlogs] WHERE [{0}adminlogs].[uid] = {1} AND [{0}adminlogs].[action] = '系统登录' ORDER BY ID DESC) AND [{0}adminlogs].[uid] = {1} AND [{0}adminlogs].[action] = '系统登录' ORDER BY ID DESC", BaseConfigs.GetTablePrefix, uid.ToString()));
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
            string commandText = string.Format("INSERT INTO [{0}userauths] ([userid], [username], [email], [addtime], [expirs], [atype], [code]) VALUES (@userid, @username, @email, @addtime, @expirs, @atype, @code);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityUserauth(info)));
        }
        public IDataReader GetUserauth(string code, AuthType atype)
        {
            string commandText = string.Format("select * from [{0}userauths] where code = '{1}' and atype = {2}", BaseConfigs.GetTablePrefix, code, (byte)atype);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetUserauthByUsername(string username, AuthType atype)
        {
            string commandText = string.Format("select top 1 * from [{0}userauths] where username = '{1}' and atype = {2} order by id desc", BaseConfigs.GetTablePrefix, username, (byte)atype);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public int DelUserauth(int id)
        {
            string commandText = string.Format("delete from [{0}userauths] where id = {1}", BaseConfigs.GetTablePrefix, id);
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
				DbHelper.MakeInParam("@password",(DbType)SqlDbType.NChar,32,info.Password),
				DbHelper.MakeInParam("@safecode",(DbType)SqlDbType.NChar,32,info.Safecode),
				DbHelper.MakeInParam("@spaceid",(DbType)SqlDbType.Int,4,info.Spaceid),
				DbHelper.MakeInParam("@gender",(DbType)SqlDbType.TinyInt,1,info.Gender),
				DbHelper.MakeInParam("@birthday",(DbType)SqlDbType.DateTime,8,info.Birthday),
				DbHelper.MakeInParam("@adminid",(DbType)SqlDbType.Int,4,info.Adminid),
				DbHelper.MakeInParam("@admingroupname",(DbType)SqlDbType.NVarChar,30,info.Admingroupname),
				DbHelper.MakeInParam("@groupid",(DbType)SqlDbType.Int,4,info.Groupid),
				DbHelper.MakeInParam("@groupname",(DbType)SqlDbType.NVarChar,30,info.Groupname),
				DbHelper.MakeInParam("@extgroupids",(DbType)SqlDbType.Char,100,info.Extgroupids),
				DbHelper.MakeInParam("@regip",(DbType)SqlDbType.Char,15,info.Regip),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
				DbHelper.MakeInParam("@loginip",(DbType)SqlDbType.Char,15,info.Loginip),
				DbHelper.MakeInParam("@logintime",(DbType)SqlDbType.DateTime,8,info.Logintime),
				DbHelper.MakeInParam("@lastaction",(DbType)SqlDbType.DateTime,8,info.Lastaction),
				DbHelper.MakeInParam("@money",(DbType)SqlDbType.Decimal,9,info.Money),
				DbHelper.MakeInParam("@credits",(DbType)SqlDbType.Int,4,info.Credits),
				DbHelper.MakeInParam("@extcredits1",(DbType)SqlDbType.Int,4,info.Extcredits1),
				DbHelper.MakeInParam("@extcredits2",(DbType)SqlDbType.Int,4,info.Extcredits2),
				DbHelper.MakeInParam("@extcredits3",(DbType)SqlDbType.Int,4,info.Extcredits3),
				DbHelper.MakeInParam("@extcredits4",(DbType)SqlDbType.Int,4,info.Extcredits4),
				DbHelper.MakeInParam("@extcredits5",(DbType)SqlDbType.Int,4,info.Extcredits5),
				DbHelper.MakeInParam("@email",(DbType)SqlDbType.Char,100,info.Email),
				DbHelper.MakeInParam("@ischeck",(DbType)SqlDbType.TinyInt,1,info.Ischeck),
				DbHelper.MakeInParam("@locked",(DbType)SqlDbType.TinyInt,1,info.Locked),
				DbHelper.MakeInParam("@newpm",(DbType)SqlDbType.TinyInt,1,info.Newpm),
				DbHelper.MakeInParam("@newpmcount",(DbType)SqlDbType.Int,4,info.Newpmcount),
				DbHelper.MakeInParam("@onlinestate",(DbType)SqlDbType.TinyInt,1,info.Onlinestate),
				DbHelper.MakeInParam("@Invisible",(DbType)SqlDbType.TinyInt,1,info.Invisible),
				DbHelper.MakeInParam("@showemail",(DbType)SqlDbType.TinyInt,1,info.Showemail)
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
                DbHelper.MakeInParam("@website",(DbType)SqlDbType.NChar,200,info.Website),
                DbHelper.MakeInParam("@ucid",(DbType)SqlDbType.Int,4,info.Ucid)
            };
        }

        #region Pms
        private DbParameter[] EntityPrivateMessage(PrivateMessageInfo info)
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

        public int AddPrivateMessage(PrivateMessageInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}pms] ([msgfrom], [msgfromid], [msgto], [msgtoid], [folder], [new], [subject], [content], [addtime]) VALUES (@msgfrom, @msgfromid, @msgto, @msgtoid, @folder, @new, @subject, @content, @addtime);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityPrivateMessage(info)));
        }

        public int EditPrivateMessage(PrivateMessageInfo info)
        {
            string commandText = string.Format("UPDATE [{0}pms] SET [msgfrom] = @msgfrom, [msgfromid] = @msgfromid, [msgto] = @msgto, [msgtoid] = @msgtoid, [folder] = @folder, [new] = @new, [subject] = @subject, [content] = @content, [addtime] = @addtime WHERE [{0}pms].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityPrivateMessage(info));
        }

        public int DelPrivateMessage(int id)
        {
            string commandText = string.Format("DELETE FROM [{0}pms] WHERE [{0}pms].[id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetPrivateMessage(int id)
        {
            string commandText = string.Format("SELECT TOP 1 * FROM [{0}pms] WHERE [{0}pms].[id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得新短消息数
        /// </summary>
        /// <returns></returns>
        public int GetNewPMCount(int userId)
        {
            string commandText = string.Format("SELECT COUNT([id]) AS [pmcount] FROM [{0}pms] WHERE [new] = 1 AND [folder] = 0 AND [msgtoid] = {1}",
                                                BaseConfigs.GetTablePrefix,
                                                userId);
            return TypeParse.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        public IDataReader GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,userId),
									   DbHelper.MakeInParam("@folder",(DbType)SqlDbType.Int,4,folder),
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
									   DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
								       DbHelper.MakeInParam("@inttype",(DbType)SqlDbType.VarChar,500,intType)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getpmlist", BaseConfigs.GetTablePrefix), parms);
        }

        public DataTable GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType, out int pagecount, out int recordcount)
        {
            string where = "";
            if (folder != 0)
            {
                if (intType != 1)
                {
                    where = string.Format("[msgfromid] = {0} AND [folder] = {1}", userId, folder);
                }
                else
                {
                    where = string.Format("[msgfromid] = {0} AND [folder] = {1} AND [new] = 1", userId, folder);
                }
            }
            else
            {
                if (intType != 1)
                {
                    where = string.Format("[msgtoid] = {0} AND [folder] = {1} ", userId, folder);
                }
                else
                {
                    where = string.Format("[msgtoid] = {0} AND [folder] = {1} AND [new] = 1", userId, folder);
                }
            }
            return GetDataPage(BaseConfigs.GetTablePrefix + "pms", "id", pageIndex, pageSize, "*", "id desc", where, out pagecount, out recordcount);
        }
        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public int GetPrivateMessageCount(int userId, int folder, int state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,userId),
									   DbHelper.MakeInParam("@folder",(DbType)SqlDbType.Int,4,folder),								   
									   DbHelper.MakeInParam("@state",(DbType)SqlDbType.Int,4,state)
								   };
            return TypeParse.StrToInt(
                                 DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                        string.Format("{0}getpmcount", BaseConfigs.GetTablePrefix),
                                                        parms));
        }

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        public int GetAnnouncePrivateMessageCount()
        {
            return TypeParse.StrToInt(
                         DbHelper.ExecuteScalarInMasterDB(CommandType.Text,
                                                             string.Format("SELECT COUNT(id) FROM [{0}pms] WHERE [msgtoid] = 0", BaseConfigs.GetTablePrefix)));
        }


        /// <summary>
        /// 获得公共短信息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数,为-1时返回全部</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>短信息列表</returns>
        public IDataReader GetAnnouncePrivateMessageList(int pageSize, int pageIndex)
        {
            string commandText = "";
            if (pageSize == -1)
                commandText = string.Format("SELECT {0} FROM [{1}pms] WHERE [msgtoid] = 0 ORDER BY [id] DESC",
                                             DbFields.PMS, BaseConfigs.GetTablePrefix);
            else if (pageIndex <= 1)
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}pms] WHERE [msgtoid] = 0  ORDER BY [id] DESC",
                                             pageSize, DbFields.PMS, BaseConfigs.GetTablePrefix);
            else
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}pms] WHERE [msgtoid] = 0 AND [pmid] < (SELECT MIN([id]) FROM (SELECT TOP {3} [id] FROM [{2}pms] WHERE [msgtoid] = 0  ORDER BY [id] DESC) AS tblTmp)  ORDER BY [id] DESC",
                                             pageSize, DbFields.PMS, BaseConfigs.GetTablePrefix, (pageIndex - 1) * pageSize);

            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetAnnouncePrivateMessageDataPage(string fields, int pageindex, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "pms", "id", pageindex, pagesize, fields == "" ? "*" : fields, "id desc", "msgtoid=0", out pagecount, out recordcount);
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="__privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public int AddPrivateMessage(PrivateMessageInfo info, int saveToSentBox)
        {
            DbParameter[] parms = { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@msgfrom",(DbType)SqlDbType.NChar,20,info.Msgfrom),
				DbHelper.MakeInParam("@msgfromid",(DbType)SqlDbType.Int,4,info.Msgfromid),
				DbHelper.MakeInParam("@msgto",(DbType)SqlDbType.NChar,20,info.Msgto),
				DbHelper.MakeInParam("@msgtoid",(DbType)SqlDbType.Int,4,info.Msgtoid),
				DbHelper.MakeInParam("@folder",(DbType)SqlDbType.TinyInt,1,(byte)info.Folder),
				DbHelper.MakeInParam("@new",(DbType)SqlDbType.Int,4,info.New),
				DbHelper.MakeInParam("@subject",(DbType)SqlDbType.NVarChar,60,info.Subject),
				DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@savetosentbox",(DbType)SqlDbType.TinyInt,1,saveToSentBox)
            };
            return TypeParse.StrToInt(
                                 DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                        string.Format("{0}createpm", BaseConfigs.GetTablePrefix), parms), -1);
        }

        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public int DelPrivateMessages(int userId, string pmIdList)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,4, userId)
			                      };
            string commandText = string.Format("DELETE FROM [{0}pms] WHERE [id] IN ({1}) AND ([msgtoid] = @userid OR [msgfromid] = @userid)",
                                                BaseConfigs.GetTablePrefix,
                                                pmIdList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public int DelPrivateMessages(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm)
        {
            string commandText = "WHERE [id]>0";

            if (!isNew)
                commandText += " AND [new]=0";

            if (!Utils.StrIsNullOrEmpty(postDateTime))
                commandText += string.Format(" AND DATEDIFF(day,addtime,getdate())>={0}", postDateTime);

            if (msgFromList != "")
            {
                commandText += " AND (";
                foreach (string msgfrom in msgFromList.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(msgfrom))
                    {
                        if (!lowerUpper)
                            commandText += string.Format(" [msgfrom]='{0}' OR", msgfrom);
                        else
                            commandText += string.Format(" [msgfrom] COLLATE Chinese_PRC_CS_AS_WS ='{0}' OR", msgfrom);
                    }
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }

            if (subject != "")
            {
                commandText += " AND (";
                foreach (string sub in subject.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(sub))
                        commandText += string.Format(" [subject] LIKE '%{0}%' OR ", RegEsc(sub));
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }

            if (message != "")
            {
                commandText += " AND (";
                foreach (string mess in message.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(mess))
                        commandText += string.Format(" [content] LIKE '%{0}%' OR ", RegEsc(mess));
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }
            //最多每次只更新100条记录
            if (isUpdateUserNewPm)
            {
                DbHelper.ExecuteNonQuery(string.Format("UPDATE [{0}users] SET [newpm]=0 WHERE [id] IN (SELECT TOP 100 [msgtoid] FROM [{0}pms] {1} Order By [id] ASC)", BaseConfigs.GetTablePrefix, commandText));
            }
            //最多每次只删除100条记录
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}pms] WHERE [id] IN (SELECT TOP 100 [id] FROM [{0}pms] {1} Order By [id] ASC)", BaseConfigs.GetTablePrefix, commandText));
        }

        /// <summary>
        /// 获得新短消息数
        /// </summary>
        /// <returns></returns>
        public int GetNewPrivateMessageCount(int userId)
        {
            string commandText = string.Format("SELECT COUNT([id]) AS [pmcount] FROM [{0}pms] WHERE [new] = 1 AND [folder] = 0 AND [msgtoid] = {1}",
                                                BaseConfigs.GetTablePrefix,
                                                userId);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 删除指定用户的一条短消息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pmid">pmid</param>
        /// <returns></returns>
        public int DelPrivateMessage(int userId, int pmId)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,4, userId),
									   DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int,4, pmId)
			                      };
            string commandText = string.Format("DELETE FROM [{0}pms] WHERE [id]=@id AND ([msgtoid] = @userid OR [msgfromid] = @userid)",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public int SetPrivateMessageState(int pmId, byte state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int,1,pmId),
									   DbHelper.MakeInParam("@state",(DbType)SqlDbType.TinyInt,1,state)
								   };
            string commandText = string.Format("UPDATE [{0}pms] SET [new]=@state WHERE [id]=@id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public DataTable GetPrivateMessageDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}pms", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
        #endregion

        #region Favorite
        private DbParameter[] EntityFavorite(FavoriteInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
				DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int,4,info.Cid),
                DbHelper.MakeInParam("@likeid",(DbType)SqlDbType.NVarChar,20,info.Likeid),
				DbHelper.MakeInParam("@typeid",(DbType)SqlDbType.TinyInt,1,info.Typeid),
				DbHelper.MakeInParam("@favtime",(DbType)SqlDbType.DateTime,8,info.Favtime)
            };
        }

        public int AddFavorite(FavoriteInfo info)
        {
            if (IsAddFavorite(info) > 0)
            {
                return -1;
            }
            string commandText = string.Format("INSERT INTO [{0}favorites] ([uid], [cid], [typeid], [likeid], [favtime]) VALUES (@uid, @cid, @typeid, @likeid, @favtime);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityFavorite(info)));
        }

        public int EditFavorite(FavoriteInfo info)
        {
            string commandText = string.Format("UPDATE [{0}favorites] SET [uid] = @uid, [cid] = @cid, [typeid] = @typeid, [favtime] = @favtime, [likeid] = @likeid WHERE [{0}favorites].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityFavorite(info));
        }

        public int DelFavorites(string cids, int typeid, int uid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}favorites] WHERE [{0}favorites].[cid] in ({1}) AND [{0}favorites].[uid] = {2} AND [{0}favorites].[typeid] = {3}", BaseConfigs.GetTablePrefix, cids, uid, typeid));
        }

        public IDataReader GetFavorite(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}favorites] WHERE [{0}favorites].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int IsAddFavorite(FavoriteInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT id FROM [{0}favorites] WHERE [{0}favorites].[typeid] = {1} AND [{0}favorites].[uid] = {2}  AND [{0}favorites].[cid] = {3}", BaseConfigs.GetTablePrefix, info.Typeid, info.Uid, info.Cid)), 0);
        }

        //默认包含字段 uid,cid,favtime,typeid
        public DataTable GetFavoriteDataPage(int typeid, int uid, string fields, int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {

            string table = string.Format("[{0}favorites]", BaseConfigs.GetTablePrefix);
            if (typeid == 2)
            {

            }
            else
            {
                fields = string.Format("[{0}contents]." + fields, BaseConfigs.GetTablePrefix).Replace(",", string.Format(",[{0}contents].", BaseConfigs.GetTablePrefix));
                fields = fields == "" ? "*" : (string.Format("[{0}contents].[typeid],", BaseConfigs.GetTablePrefix) + fields);
                table += string.Format(" inner join [{0}contents] on [{0}favorites].[cid] = [{0}contents].[id]", BaseConfigs.GetTablePrefix);
            }
            string where = string.Format("[{0}contents].[orderid]>=-1000 AND [{0}contents].[status]=2 AND [{0}favorites].[uid] {1}", BaseConfigs.GetTablePrefix, uid > 0 ? ("=" + uid.ToString()) : ">0");

            fields += string.Format(",[{0}favorites].[uid],[{0}favorites].[cid],[{0}favorites].[favtime]", BaseConfigs.GetTablePrefix);
            return GetDataPage(table, string.Format("[{0}favorites].[id]", BaseConfigs.GetTablePrefix), pagecurrent, pagesize, fields, "favtime desc", where, out pagecount, out recordcount);
        }
        #endregion

        #region Userlog
        private DbParameter[] EntityUserlog(UserlogInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
				DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
				DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15,info.Ip),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
				DbHelper.MakeInParam("@action",(DbType)SqlDbType.NVarChar,100,info.Action),
				DbHelper.MakeInParam("@value",(DbType)SqlDbType.Int,4,info.Value),
				DbHelper.MakeInParam("@remark",(DbType)SqlDbType.NVarChar,300,info.Remark),
				DbHelper.MakeInParam("@identify",(DbType)SqlDbType.NVarChar,30,info.Identify)
            };
        }

        public int AddUserlog(UserlogInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}userlogs] ([uid], [username], [ip], [addtime], [action], [value], [remark], [identify]) VALUES (@uid, @username, @ip, @addtime, @action, @value, @remark, @identify);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityUserlog(info)));
        }

        public int EditUserlog(UserlogInfo info)
        {
            string commandText = string.Format("UPDATE [{0}userlogs] SET [uid] = @uid, [username] = @username, [ip] = @ip, [addtime] = @addtime, [action] = @action, [value] = @value, [remark] = @remark, [identify] = @identify WHERE [{0}userlogs].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityUserlog(info));
        }

        public int DelUserlog(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}userlogs] WHERE [{0}userlogs].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetUserlog(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM [{0}userlogs] WHERE [{0}userlogs].[id] = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetUserlogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}userlogs", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }

        public string UserlogSearchCondition(string users, string startdate, string enddate, string ip, string keywords, string identify)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            if (keywords != string.Empty)
                condition.AppendFormat(" AND (action LIKE '%{0}%' OR remark LIKE '%{0}%')", RegEsc(keywords));
            if (ip != string.Empty)
                condition.AppendFormat(" AND (ip LIKE '%{0}%')", RegEsc(ip));
            if (identify != "")
                condition.AppendFormat(" AND identify = '{0}'", identify);
            users = RegSqlCharList(users);
            if (users != string.Empty)
                condition.AppendFormat(" AND username IN ({0})", users);
            return condition.ToString();
        }
        #endregion
    }
}
