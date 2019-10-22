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
using System.Text.RegularExpressions;

namespace STA.Data.MySql
{
    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }

        public string RplDanField(string str)
        {
            return Regex.Replace(str, "--|;|'|\"", "", RegexOptions.Compiled | RegexOptions.Multiline);
        }


        public string RegSqlCharList(string str)
        {
            if (str.Trim() == "") return "";
            string ret = string.Empty; ;
            foreach (string s in str.Split(','))
            {
                if (s.Trim() == string.Empty) continue;
                ret += string.Format("'{0}',", RplDanField(s));
            }
            return ret.Length > 1 ? ret.Substring(0, ret.Length - 1) : "";
        }


        public string BackUpDatabase(string backUpPath, string serverName, string userName, string password, string strDbName, string strFileName)
        {
            return "";
        }

        public string RestoreDatabase(string backUpPath, string serverName, string userName, string password, string strDbName, string strFileName)
        {
            return "";
        }

        public void ShrinkDataBase(string shrinkSize, string dbName)
        {
            StringBuilder sqlBuilder = new StringBuilder("SET NOCOUNT ON ");

            sqlBuilder.Append("DECLARE @LogicalFileName sysname, @MaxMinutes INT, @NewSize INT ");
            sqlBuilder.AppendFormat("USE `{0}` -- 要操作的数据库名 ", dbName);
            sqlBuilder.AppendFormat("SELECT @LogicalFileName = '{0}_log', -- 日志文件名 ", dbName);
            sqlBuilder.Append("@MaxMinutes = 10, -- Limit on time allowed to wrap log. ");
            sqlBuilder.Append("@NewSize = 1 -- 你想设定的日志文件的大小(M) ");
            sqlBuilder.Append("-- Setup / initialize ");
            sqlBuilder.Append("DECLARE @OriginalSize int ");
            sqlBuilder.AppendFormat("SELECT @OriginalSize = {0}", shrinkSize);
            sqlBuilder.Append("FROM sysfiles ");
            sqlBuilder.Append("WHERE name = @LogicalFileName ");
            sqlBuilder.Append("SELECT 'Original Size of ' + db_name() + ' LOG is ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),@OriginalSize) + ' 8K pages or ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),(@OriginalSize*8/1024)) + 'MB' ");
            sqlBuilder.Append("FROM sysfiles ");
            sqlBuilder.Append("WHERE name = @LogicalFileName ");
            sqlBuilder.Append("CREATE TABLE DummyTrans ");
            sqlBuilder.Append("(DummyColumn char (8000) not null) ");
            sqlBuilder.Append("DECLARE @Counter INT, ");
            sqlBuilder.Append("@StartTime DATETIME, ");
            sqlBuilder.Append("@TruncLog VARCHAR(255) ");
            sqlBuilder.Append("SELECT @StartTime = GETDATE(), ");
            sqlBuilder.Append("@TruncLog = 'BACKUP LOG ' + db_name() + ' WITH TRUNCATE_ONLY' ");
            sqlBuilder.Append("DBCC SHRINKFILE (@LogicalFileName, @NewSize) ");
            sqlBuilder.Append("EXEC (@TruncLog) ");
            sqlBuilder.Append("-- Wrap the log if necessary. ");
            sqlBuilder.Append("WHILE @MaxMinutes > DATEDIFF (mi, @StartTime, GETDATE()) -- time has not expired ");
            sqlBuilder.Append("AND @OriginalSize = (SELECT size FROM sysfiles WHERE name = @LogicalFileName) ");
            sqlBuilder.Append("AND (@OriginalSize * 8 /1024) > @NewSize ");
            sqlBuilder.Append("BEGIN -- Outer loop. ");
            sqlBuilder.Append("SELECT @Counter = 0 ");
            sqlBuilder.Append("WHILE ((@Counter < @OriginalSize / 16) AND (@Counter < 50000)) ");
            sqlBuilder.Append("BEGIN -- update ");
            sqlBuilder.Append("INSERT DummyTrans VALUES ('Fill Log') ");
            sqlBuilder.Append("DELETE DummyTrans ");
            sqlBuilder.Append("SELECT @Counter = @Counter + 1 ");
            sqlBuilder.Append("END ");
            sqlBuilder.Append("EXEC (@TruncLog) ");
            sqlBuilder.Append("END ");
            sqlBuilder.Append("SELECT 'Final Size of ' + db_name() + ' LOG is ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),size) + ' 8K pages or ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),(size*8/1024)) + 'MB' ");
            sqlBuilder.Append("FROM sysfiles ");
            sqlBuilder.Append("WHERE name = @LogicalFileName ");
            sqlBuilder.Append("DROP TABLE DummyTrans ");
            sqlBuilder.Append("SET NOCOUNT OFF ");

            DbHelper.ExecuteDataset(CommandType.Text, sqlBuilder.ToString());
        }

        public void ClearDBLog(string dbName)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@DBName", (DbType)SqlDbType.VarChar, 50, dbName),
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}shrinklog", BaseConfigs.GetTablePrefix), parms);
        }

        public string RunSql(string sql)
        {
            string errorInfo = "";
            if (sql != "")
            {
                string[] sqlArray = Utils.SplitString(sql, "--/* www.stacms.com */--");
                foreach (string sqlStr in sqlArray)
                {
                    if (Utils.StrIsNullOrEmpty(sqlStr))   //当读到空的Sql语句则继续
                    {
                        continue;
                    }
                    try
                    {
                        DbHelper.ExecuteNonQuery(CommandType.Text, sqlStr);
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("'", " ");
                        message = message.Replace("\\", "/");
                        message = message.Replace("\r\n", "\\r\\n");
                        message = message.Replace("\r", "\\r");
                        message = message.Replace("\n", "\\n");
                        errorInfo += message + "<br />";
                    }

                }
            }
            return errorInfo;
        }

        //得到数据库的名称
        public string GetDbName()
        {
            foreach (string info in BaseConfigs.GetDBConnectString.Split(';'))
            {
                if (info.ToLower().IndexOf("initial catalog") >= 0 || info.ToLower().IndexOf("database") >= 0)
                    return info.Split('=')[1].Trim();
            }
            return "siteasy";
        }

        #region 表操作

        public DataTable GetFieldListTable(string tablename, string where, string fieldname)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {0} FROM `{1}`{2}", fieldname, tablename, (where.Trim() != "" ? " where " : "") + where)).Tables[0];
        }

        public string UpdateTableFieldByPrimaryKey(string tablename, string fieldname, string content, string primarykeyname, string keyvalue)
        {
            try
            {
                DbParameter[] parms = new DbParameter[]{
                    new SqlParameter("@content",content),
                    new SqlParameter("@keyval",keyvalue)
                };
                string commandText = "update [" + tablename + "] set [" + fieldname + "]= @content where " + primarykeyname + " = @keyval";
                return "成功 " + DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.Replace("'", " ").Replace("\"", " ").Replace("\n", " ").Replace("\\", "/");
            }
        }

        public string ReplaceTableField(string tablename, string where, string fieldname, string source, string target)
        {
            try
            {
                DbParameter[] parms = new DbParameter[]{
                    new SqlParameter("@old",source),
                    new SqlParameter("@new",target)
                };
                string commandText = "update [" + tablename + "] set [" + fieldname + "]=replace([" + fieldname + "] ,@old, @new)" + (where.Trim() != "" ? " where " : "") + where;
                return "替换执行成功！操作的的行数为 <b>" + DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms).ToString() + "</b> 行！";
            }
            catch (Exception ex)
            {
                return ex.Message.Replace("'", " ").Replace("\"", " ").Replace("\n", " ").Replace("\\", "/");
            }
        }

        public DataTable GetTableData(string tablename)
        {
            return DbHelper.ExecuteDataset("SELECT * FROM " + tablename).Tables[0];
        }

        #region 表字段
        public DataTable GetTableField(string tablename)
        {
            return DbHelper.ExecuteDataset(GetTableFieldSqlString(tablename)).Tables[0];
        }

        public string GetDataBaseVersion()
        {
            return DbHelper.ExecuteScalar(CommandType.Text, "SELECT @@version").ToString();
        }

        public DataTable GetAllTableName()
        {
            return DbHelper.ExecuteDataset(GetDataBaseVersion().Replace(" ", "").ToLower().IndexOf("sqlserver2005") != -1 ? "SELECT name FROM sysobjects WHERE (type = 'U') AND name <> 'sysdiagrams'" : "select name from sysobjects where xtype='u' and status>=0").Tables[0];
        }

        private string GetTableFieldSqlString(string tablename)
        {
            string str;
            if (GetDataBaseVersion().Replace(" ", "").ToLower().IndexOf("sqlserver2005") != -1)
            {
                str = "SELECT col.name, col.column_id, st.name AS type, col.max_length AS length, col.is_nullable,col.`precision`, col.scale, col.is_identity, defCst.definition";
                str = ((((((str + " FROM sys.columns AS col LEFT OUTER JOIN" + " sys.types AS st ON st.user_type_id = col.user_type_id LEFT OUTER JOIN") + " sys.types AS bt ON bt.user_type_id = col.system_type_id LEFT OUTER JOIN" + " sys.objects AS robj ON robj.object_id = col.rule_object_id AND robj.type = 'R' LEFT OUTER JOIN") + " sys.objects AS dobj ON dobj.object_id = col.default_object_id AND dobj.type = 'D' LEFT OUTER JOIN" + " sys.default_constraints AS defCst ON defCst.parent_object_id = col.object_id AND defCst.parent_column_id = col.column_id LEFT OUTER JOIN") + " sys.identity_columns AS idc ON idc.object_id = col.object_id AND idc.column_id = col.column_id LEFT OUTER JOIN" + " sys.computed_columns AS cmc ON cmc.object_id = col.object_id AND cmc.column_id = col.column_id LEFT OUTER JOIN") + " sys.fulltext_index_columns AS ftc ON ftc.object_id = col.object_id AND ftc.column_id = col.column_id LEFT OUTER JOIN" + " sys.xml_schema_collections AS xmlcoll ON xmlcoll.xml_collection_id = col.xml_collection_id") + " WHERE     (col.object_id = OBJECT_ID(N'dbo." + tablename + "'))") + " ORDER BY col.column_id";
            }
            else if (GetDataBaseVersion().Replace(" ", "").ToLower().IndexOf("sqlserver2008") != -1)
            {
                str = "SELECT [表名]=case when a.colorder=1 then d.name else '' end,[表说明]=case when a.colorder=1 then isnull(f.value,'') else '' end,[字段序号]=a.colorder,`name`=a.name,[is_identity]=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,[主键]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (SELECT name FROM sysindexes WHERE indid in(SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then '√' else '' end,`type`=b.name,`length`=a.length,`precision`=COLUMNPROPERTY(a.id,a.name,'PRECISION'),`scale`=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),[is_nullable]=case when a.isnullable=1 then '√'else '' end,`definition`=isnull(e.text,''),[字段说明]=isnull(g.`value`,'') FROM syscolumns a left join systypes b on a.xusertype=b.xusertype inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0 where d.name='" + tablename + "' order by a.id,a.colorder";
            }
            else
            {
                str = "SELECT a.name,a.colorder,b.name as type,a.length,a.isnullable as is_nullable,COLUMNPROPERTY(a.id,a.name,'PRECISION') as `precision`,isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),'') as scale,COLUMNPROPERTY(   a.id,a.name,'IsIdentity') as is_identity,e.text as definition  ";
                str = ((((str + "FROM   syscolumns   a   " + "left   join   systypes   b   on   a.xtype=b.xusertype   ") + "inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype='U'   and     d.name<>'dtproperties'   " + "left   join   syscomments   e   on   a.cdefault=e.id   ") + "left   join   sysproperties   g   on   a.id=g.id   and   a.colid=g.smallid       " + "left   join   sysproperties   f   on   d.id=f.id   and   f.smallid=0   ") + "where   d.name='" + tablename + "' ") + "order by a.colorder";
            }
            return str;
        }
        #endregion

        private string GetFieldType(string fieldtype, int size)
        {
            string ftypeformat = string.Empty;
            if (Utils.InArray(fieldtype, "char,nchar,varchar,nvarchar,editor"))
            {
                if ((Utils.InArray(fieldtype, "char,nchar") && size > 8000) || (Utils.InArray(fieldtype, "varchar,nvarchar") && size > 4000) || fieldtype == "editor")
                    return "ntext";
                else
                    return fieldtype + "(" + (size < 0 ? 20 : size).ToString() + ")";
            }
            else if (Utils.InArray(fieldtype, "radio,checkbox,select,selectfile,stepselect"))
                return "nvarchar(1000)";
            else
                return fieldtype;
        }

        public bool AddTableField(string tablename, string fieldname, string fieldtype, int size)
        {
            try
            {
                string sql = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] {0} NULL";
                DbHelper.ExecuteNonQuery(string.Format(sql, GetFieldType(fieldtype, size)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DropTableField(string tablename, string fieldname)
        {
            try
            {
                DbHelper.ExecuteNonQuery("ALTER TABLE [" + tablename + "] DROP COLUMN [" + fieldname + "]");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddExtTable(string tablename)
        {
            try
            {
                DbHelper.ExecuteNonQuery("create table [" + tablename + "](cid int not null,typeid int not null)");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DropTable(string tablename)
        {
            try
            {
                DbHelper.ExecuteNonQuery("DROP TABLE [" + tablename + "]");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int ExistTableField(string tablename, string fieldname)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("select count(*) from syscolumns where id=object_id(N'`dbo`.`{0}`') and name='{1}'", tablename, fieldname)), 0);
        }

        public bool ReTableName(string newtablename, string oldtablename)
        {
            try
            {
                DbHelper.ExecuteNonQuery("sp_rename '" + oldtablename + "', '" + newtablename + "', 'OBJECT'");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditTableField(string tablename, string fieldname, string fieldtype, int size)
        {
            try
            {
                string sql = "ALTER TABLE [" + tablename + "] ALTER COLUMN [" + fieldname + "] {0} NULL";
                DbHelper.ExecuteNonQuery(string.Format(sql, GetFieldType(fieldtype, size)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int ExistTable(string tablename)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "select count(*) from sysobjects where id = object_id(N'`dbo`.[" + tablename + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1"), 0);
        }
        #endregion


        public int Search(int channelid, string keyword, int day, int typeid, int order, int ordertype, int searchtype)
        {
            DeleteExpriedSearchCache(); //删除过期缓存

            StringBuilder strids = new StringBuilder();
            StringBuilder sql = new StringBuilder(string.Format("SELECT `id` FROM `{0}contents`  WHERE `status`=2 AND `orderid`>=-1000", BaseConfigs.GetTablePrefix));

            keyword = Regex.Replace(keyword, "--|;|'|\"", "", RegexOptions.Compiled | RegexOptions.Multiline);

            if (typeid >= 0)
                sql.AppendFormat(" AND typeid = {0}", typeid.ToString());
            if (channelid > 0)
                sql.AppendFormat(" AND (channelid = {0} or channelfamily like '%,{0},%' or extchannels like '%,{0},%')", channelid.ToString());
            if (day != 0)
                sql.AppendFormat(" AND addtime >= '{0}'", DateTime.Now.AddDays(day));
            if (keyword.Trim() != "")
            {
                if (searchtype == 1)
                    sql.AppendFormat(" AND title like '%{0}%'", RegEsc(keyword));
                else
                    sql.AppendFormat(" AND (title like '%{0}%' or content like '%{0}%')", RegEsc(keyword));
            }

            sql.Append(" ORDER BY " + GetConOrderBy(order, ordertype, BaseConfigs.GetTablePrefix));

            int searchid = TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("UPDATE `{0}searchcaches` SET `searchtime` = getdate(), `scount` = `scount`+1 WHERE `searchstring`=@searchstring;SELECT TOP 1 `id` FROM `{0}searchcaches` WHERE `searchstring`=@searchstring", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@searchstring", (DbType)SqlDbType.VarChar, 255, sql.ToString())));

            if (searchid > 0)
                return searchid;

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, sql.ToString());

            if (reader != null)
            {
                SearchcacheInfo cacheinfo = new SearchcacheInfo();
                cacheinfo.Keywords = keyword;
                cacheinfo.Searchstring = sql.ToString();
                cacheinfo.Ids = GetSearchIds(reader).ToString();
                cacheinfo.Rcount = cacheinfo.Ids.Split(',').Length;
                reader.Close();

                return AddSearchCache(cacheinfo);
            }

            return 0;
        }

        private StringBuilder GetSearchIds(IDataReader reader)
        {
            StringBuilder strids = new StringBuilder();

            while (reader.Read())
            {
                strids.AppendFormat("{0},", reader[0].ToString());
            }
            reader.Close();

            if (strids.ToString().EndsWith(","))
                strids.Length--;

            return strids;
        }

        /// <summary>
        /// 删除超过过期的缓存记录
        /// </summary>
        public void DeleteExpriedSearchCache()
        {
            DbHelper.ExecuteNonQuery(CommandType.Text,
                                     string.Format(@"DELETE FROM `{0}searchcaches` WHERE `expiration`<@expiration", BaseConfigs.GetTablePrefix),
                                     DbHelper.MakeInParam("@expiration", (DbType)SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(-GeneralConfigs.GetConfig().Searchcachetime).ToString("yyyy-MM-dd HH:mm:ss")));
        }

        /// <summary>
        /// 获得搜索缓存ID列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSearchCacheIds(int searchId)
        {
            return DbHelper.ExecuteDataset(CommandType.Text,
                                           string.Format("SELECT TOP 1 `ids` FROM `{0}searchcaches` WHERE `id`=@searchid", BaseConfigs.GetTablePrefix),
                                           DbHelper.MakeInParam("@searchid", (DbType)SqlDbType.Int, 4, searchId)).Tables[0];
        }

        public DataTable GetSearchContentsList(int pagesize, string columns, string strids, int order, int ordertype, string ext)
        {
            string commandText = string.Format("SELECT TOP {1} {2} FROM `{0}contents` {5} WHERE `{0}contents`.`id` IN ({3}) AND `{0}contents`.`status` = 2 AND `{0}contents`.`orderid` >= -1000 ORDER BY {4}",
                                 BaseConfigs.GetTablePrefix, pagesize, columns == "" ? "*" : (string.Format("`{0}contents`.`typeid`,", BaseConfigs.GetTablePrefix) + columns), strids, GetConOrderBy(order, ordertype, BaseConfigs.GetTablePrefix),
                                 ext == "" ? "" : string.Format("INNER JOIN `{0}ext{1}s` ON `{0}contents`.`id` = `{0}ext{1}s`.`cid`", BaseConfigs.GetTablePrefix, ext));
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public void SetLastExecuteScheduledEventDateTime(string key, string serverName, DateTime lastExecuted)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@key", (DbType)SqlDbType.VarChar, 100, key),
                DbHelper.MakeInParam("@servername", (DbType)SqlDbType.VarChar, 100, serverName),
                DbHelper.MakeInParam("@lastexecuted", (DbType)SqlDbType.DateTime, 8, lastExecuted)
            };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                     string.Format("{0}setlastexecutescheduledeventdatetime", BaseConfigs.GetTablePrefix),
                                     parms);
        }

        public DateTime GetLastExecuteScheduledEventDateTime(string key, string serverName)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@key", (DbType)SqlDbType.VarChar, 100, key),
                DbHelper.MakeInParam("@servername", (DbType)SqlDbType.VarChar, 100, serverName),
                DbHelper.MakeOutParam("@lastexecuted", (DbType)SqlDbType.DateTime, 8)
            };

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                     string.Format("{0}getlastexecutescheduledeventdatetime", BaseConfigs.GetTablePrefix),
                                     parms);

            return Convert.IsDBNull(parms[2].Value) ? DateTime.MinValue : Convert.ToDateTime(parms[2].Value);
        }

        private DbParameter[] EntityDbCollect(DbcollectInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@channelid",(DbType)SqlDbType.Int,4,info.Channelid),
                DbHelper.MakeInParam("@channelname",(DbType)SqlDbType.NVarChar,50,info.Channelname),
                DbHelper.MakeInParam("@constatus",(DbType)SqlDbType.TinyInt,1,info.Constatus),
                DbHelper.MakeInParam("@dbtype",(DbType)SqlDbType.TinyInt,1,info.Dbtype),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@datasource",(DbType)SqlDbType.NVarChar,50,info.Datasource),
                DbHelper.MakeInParam("@userid",(DbType)SqlDbType.NVarChar,50,info.Userid),
                DbHelper.MakeInParam("@password",(DbType)SqlDbType.NVarChar,50,info.Password),
                DbHelper.MakeInParam("@dbname",(DbType)SqlDbType.NVarChar,50,info.Dbname),
                DbHelper.MakeInParam("@tbname",(DbType)SqlDbType.NVarChar,50,info.Tbname),
                DbHelper.MakeInParam("@primarykey",(DbType)SqlDbType.NVarChar,50,info.Primarykey),
                DbHelper.MakeInParam("@orderkey",(DbType)SqlDbType.NVarChar,50,info.Orderkey),
                DbHelper.MakeInParam("@repeatkey",(DbType)SqlDbType.NVarChar,50,info.Repeatkey),
                DbHelper.MakeInParam("@sortby",(DbType)SqlDbType.Char,4,info.Sortby),
                DbHelper.MakeInParam("@where",(DbType)SqlDbType.NVarChar,1000,info.Where),
                DbHelper.MakeInParam("@matchs",(DbType)SqlDbType.NVarChar,4000,info.Matchs)
            };
        }

        private DbParameter[] EntityWebCollect(WebcollectInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@channelid",(DbType)SqlDbType.Int,4,info.Channelid),
                DbHelper.MakeInParam("@channelname",(DbType)SqlDbType.NVarChar,50,info.Channelname),
                DbHelper.MakeInParam("@constatus",(DbType)SqlDbType.TinyInt,1,info.Constatus),
                DbHelper.MakeInParam("@hosturl",(DbType)SqlDbType.NVarChar,200,info.Hosturl),
                DbHelper.MakeInParam("@collecttype",(DbType)SqlDbType.TinyInt,1,(int)info.Collecttype),
                DbHelper.MakeInParam("@curl",(DbType)SqlDbType.NVarChar,200,info.Curl),
                DbHelper.MakeInParam("@clisturl",(DbType)SqlDbType.NVarChar,200,info.Clisturl),
                DbHelper.MakeInParam("@clistpage",(DbType)SqlDbType.NChar,20,info.Clistpage),
                DbHelper.MakeInParam("@curls",(DbType)SqlDbType.NText,0,info.Curls),
                DbHelper.MakeInParam("@encode",(DbType)SqlDbType.NChar,20,info.Encode),
                DbHelper.MakeInParam("@property",(DbType)SqlDbType.NVarChar,200,info.Property),
                DbHelper.MakeInParam("@filter",(DbType)SqlDbType.NVarChar,200,info.Filter),
                DbHelper.MakeInParam("@attrs",(DbType)SqlDbType.NVarChar,200,info.Attrs),
                DbHelper.MakeInParam("@setting",(DbType)SqlDbType.NText,0,info.Setting),
                DbHelper.MakeInParam("@confilter",(DbType)SqlDbType.NText,0,info.Confilter)
            };
        }

        public int AddDbCollect(DbcollectInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createdbcollect", BaseConfigs.GetTablePrefix), EntityDbCollect(info)));
        }

        public int EditDbCollect(DbcollectInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatedbcollect", BaseConfigs.GetTablePrefix), EntityDbCollect(info)));
        }

        public int DelDbCollect(int id)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id)
			};
            string commandText = string.Format("DELETE FROM `{0}dbcollects` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public IDataReader GetDbCollect(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}dbcollects` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public DataTable GetDbCollectDataTable(int pageindex, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "dbcollects", "id", pageindex, pagesize, "*", "id desc", string.Empty, out pagecount, out recordcount);
        }

        public string DbConnectString(string datasource, string userid, string password, string dbname)
        {
            return string.Format("Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=true", datasource, userid, password, dbname);
        }

        public int AddWebCollect(WebcollectInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createwebcollect", BaseConfigs.GetTablePrefix), EntityWebCollect(info)));
        }

        public int EditWebCollect(WebcollectInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatewebcollect", BaseConfigs.GetTablePrefix), EntityWebCollect(info)));
        }

        public int DelWebCollect(int id)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id)
			};
            string commandText = string.Format("DELETE FROM `{0}webcollects` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public string GetWebCollectSearchCondition(string name, int channelid, string startdate, string enddate)
        {
            string condition = "id>0";
            if (name.Trim() != "")
                condition += string.Format(" and name like '%{0}%'", RegEsc(name));
            if (channelid > 0)
                condition += string.Format("and channelid = {0}", channelid);
            if (startdate != string.Empty)
                condition += string.Format(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition += string.Format(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            return condition;
        }

        public IDataReader GetWebCollect(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}webcollects` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public DataTable GetWebCollectDataTable(int pageindex, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "webcollects", "id", pageindex, pagesize, "*", "id desc", where, out pagecount, out recordcount);
        }

        public bool DbConnectTest(string connectstring)
        {
            try
            {
                using (DbConnection connection = DbHelper.Factory.CreateConnection())
                {
                    connection.ConnectionString = connectstring;
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public DataTable ExecuteTable(string connectstring, string commandText)
        {
            using (DbConnection connection = DbHelper.Factory.CreateConnection())
            {
                connection.ConnectionString = connectstring;
                connection.Open();
                // 预处理
                DbCommand cmd = DbHelper.Factory.CreateCommand();

                bool mustCloseConnection = false;
                DbHelper.PrepareCommand(cmd, connection, (DbTransaction)null, CommandType.Text, commandText, null, out mustCloseConnection);

                // 创建DbDataAdapter和DataSet.
                using (DbDataAdapter da = DbHelper.Factory.CreateDataAdapter())
                {
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    // 填充DataSet.
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }

        public DataTable DbTableFields(string connectstring, string tbname)
        {
            return ExecuteTable(connectstring, GetTableFieldSqlString(tbname));
        }

        public DataTable DbTables(string connectstring)
        {
            return ExecuteTable(connectstring, "select name from sysobjects where xtype='u' and status>=0");
        }

        public DataTable DbCollectDataTableBySet(DbcollectInfo info, int count)
        {
            if (info.Tbname.Trim() != "" && info.Primarykey.Trim() != "")
            {
                string commandText = string.Format("SELECT TOP {3} * FROM {0} {1} {2}", info.Tbname, info.Where.Trim() != "" ? "WHERE " + info.Where : "",
                                     info.Orderkey != "" ? ("ORDER BY " + info.Orderkey + " " + info.Sortby) : "", count);

                try
                {
                    string dbconn = DbConnectString(info.Datasource, info.Userid, info.Password, info.Dbname);
                    return ExecuteTable(dbconn, commandText);
                }
                catch (Exception ex)
                {
                    STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "数据库采集查询数据错误！sql:" + commandText, ex);
                    return new DataTable();
                }
            }

            return new DataTable();
        }

        private DbParameter[] EntitySelect(SelectInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,20,info.Name),
                DbHelper.MakeInParam("@value",(DbType)SqlDbType.NChar,30,info.Value),
                DbHelper.MakeInParam("@ename",(DbType)SqlDbType.NChar,20,info.Ename),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@issign",(DbType)SqlDbType.TinyInt,1,info.Issign)
            };
        }

        private DbParameter[] EntitySelectType(SelecttypeInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,30,info.Name),
                DbHelper.MakeInParam("@ename",(DbType)SqlDbType.NChar,20,info.Ename),
                DbHelper.MakeInParam("@issign",(DbType)SqlDbType.TinyInt,1,info.Issign),
                DbHelper.MakeInParam("@system",(DbType)SqlDbType.TinyInt,1,info.System)
            };
        }

        private DbParameter[] EntityMenu(MenuInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@parentid",(DbType)SqlDbType.Int,4,info.Parentid),
                DbHelper.MakeInParam("@pagetype",(DbType)SqlDbType.TinyInt,1,(int)info.Pagetype),
                DbHelper.MakeInParam("@system",(DbType)SqlDbType.TinyInt,1,info.System),
                DbHelper.MakeInParam("@type",(DbType)SqlDbType.TinyInt,1,info.Type),
                DbHelper.MakeInParam("@icon",(DbType)SqlDbType.NVarChar,100,info.Icon),
                DbHelper.MakeInParam("@url",(DbType)SqlDbType.NVarChar,200,info.Url),
                DbHelper.MakeInParam("@target",(DbType)SqlDbType.NChar,20,info.Target),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@identify",(DbType)SqlDbType.NVarChar,30,info.Identify)
            };
        }

        private DbParameter[] EntityPlugin(PluginInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@email",(DbType)SqlDbType.NVarChar,100,info.Email),
                DbHelper.MakeInParam("@author",(DbType)SqlDbType.NVarChar,50,info.Author),
                DbHelper.MakeInParam("@pubtime",(DbType)SqlDbType.DateTime,8,info.Pubtime),
                DbHelper.MakeInParam("@officesite",(DbType)SqlDbType.NVarChar,100,info.Officesite),
                DbHelper.MakeInParam("@menu",(DbType)SqlDbType.NVarChar,500,info.Menu),
                DbHelper.MakeInParam("@description",(DbType)SqlDbType.NText,0,info.Description),
                DbHelper.MakeInParam("@dbcreate",(DbType)SqlDbType.NText,0,info.Dbcreate),
                DbHelper.MakeInParam("@dbdelete",(DbType)SqlDbType.NText,0,info.Dbdelete),
                DbHelper.MakeInParam("@filelist",(DbType)SqlDbType.NText,0,info.Filelist),
                DbHelper.MakeInParam("@package",(DbType)SqlDbType.NVarChar,200,info.Package),
                DbHelper.MakeInParam("@setup",(DbType)SqlDbType.TinyInt,1,info.Setup)
            };
        }

        private DbParameter[] EntityUrlStaticize(StaticizeInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,50,info.Title),
                DbHelper.MakeInParam("@charset",(DbType)SqlDbType.NVarChar,15,info.Charset),
                DbHelper.MakeInParam("@url",(DbType)SqlDbType.NVarChar,200,info.Url),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@maketime",(DbType)SqlDbType.DateTime,8,info.Maketime),
                DbHelper.MakeInParam("@savepath",(DbType)SqlDbType.NVarChar,50,info.Savepath),
                DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NVarChar,50,info.Filename),
                DbHelper.MakeInParam("@suffix",(DbType)SqlDbType.NVarChar,10,info.Suffix)
            };
        }

        public int AddSelect(SelectInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createselect", BaseConfigs.GetTablePrefix), EntitySelect(info)));
        }

        public int EditSelect(SelectInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateselect", BaseConfigs.GetTablePrefix), EntitySelect(info)));
        }

        public int DelSelect(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}selects` WHERE id = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public string SelectName(string ename, string value)
        {
            string commandText = string.Format("SELECT name FROM `{0}selects` WHERE ename = @ename AND `value` = @value", BaseConfigs.GetTablePrefix);
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@ename", (DbType)SqlDbType.NChar, 20,ename ),
                DbHelper.MakeInParam("@value",(DbType)SqlDbType.NChar,30,value)
			};
            return TypeParse.ObjToString(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        public int DelSelectByEname(string ename)
        {
            string commandText = string.Format("DELETE FROM `{0}selects` WHERE ename = '{1}'", BaseConfigs.GetTablePrefix, ename);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetSelectDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "selects", "id", pagecurrent, pagesize, "*", "cast(value as float) asc", condition, out pagecount, out recordcount);
        }

        public string GetSelectSearchCondition(string ename, float maxvalue, float minvalue)
        {
            string condition = "id>0";
            if (ename.Trim() != "")
                condition += string.Format(" and ename = '{0}'", ename.Trim());
            if (maxvalue > 0)
                condition += string.Format("and cast(value as float) <= {0}", maxvalue);
            if (minvalue > 0)
                condition += string.Format(" and cast(value as float) >= {0}", minvalue);
            return condition;
        }

        public int AddSelectType(SelecttypeInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createselecttype", BaseConfigs.GetTablePrefix), EntitySelectType(info)));
        }

        public int EditSelectType(SelecttypeInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateselecttype", BaseConfigs.GetTablePrefix), EntitySelectType(info)));
        }

        public IDataReader GetSelect(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}selects` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public IDataReader GetSelectType(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}selecttypes` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public DataTable GetSelectTypeTable()
        {
            return DbHelper.ExecuteDataset(string.Format("select * from `{0}selecttypes`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetSelectByWhere(string where)
        {
            return DbHelper.ExecuteDataset(string.Format("select * from `{0}selects` {1} order by cast(`value` as float) asc", BaseConfigs.GetTablePrefix, where == "" ? "" : ("where " + where))).Tables[0];
        }

        public int DelSelectType(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}selecttypes` WHERE id = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetSelectTypeDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "selecttypes", "id", pagecurrent, pagesize, "*", "id desc", "", out pagecount, out recordcount);
        }

        public int AddUrlStaticize(StaticizeInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createstaticize", BaseConfigs.GetTablePrefix), EntityUrlStaticize(info)));
        }

        public int DelUrlStaticize(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}staticizes` WHERE id = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public int EditUrlStaticize(StaticizeInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatestaticize", BaseConfigs.GetTablePrefix), EntityUrlStaticize(info)));
        }

        public IDataReader GetUrlStaticize(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}staticizes` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public DataTable GetUrlStaticizeDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "staticizes", "id", pagecurrent, pagesize, "*", "id desc", "", out pagecount, out recordcount);
        }

        public int AddPlugin(PluginInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createplugin", BaseConfigs.GetTablePrefix), EntityPlugin(info)));
        }

        public int DelPlugin(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}plugins` WHERE id = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetPluginTable(int setup)
        {
            string commandText = string.Format("SELECT * FROM `{0}plugins` {1}", BaseConfigs.GetTablePrefix, setup < 0 ? "" : ("WHERE setup=" + setup.ToString()));
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public int EditPlugin(PluginInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateplugin", BaseConfigs.GetTablePrefix), EntityPlugin(info)));
        }

        public IDataReader GetPlugin(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}plugins` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public DataTable GetPluginDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "plugins", "id", pagecurrent, pagesize, "*", "id desc", "", out pagecount, out recordcount);
        }

        public int AddMenu(MenuInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createmenu", BaseConfigs.GetTablePrefix), EntityMenu(info)));
        }

        public int DelMenu(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletemenu", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id)));
        }

        public int EditMenu(MenuInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatemenu", BaseConfigs.GetTablePrefix), EntityMenu(info)));
        }

        public IDataReader GetMenu(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}menus` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public DataTable GetMenuTable(int type)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}menus` {1} ORDER BY orderid DESC", BaseConfigs.GetTablePrefix, type > 0 ? string.Format("WHERE type={0}", type) : "")).Tables[0];
        }

        public DataTable GetMenuTable(int type, PageType pagetype)
        {
            string where = "pagetype=" + ((int)pagetype).ToString();
            if (type > 0)
                where += string.Format(" AND type = {0}", type);

            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}menus` WHERE {1} ORDER BY orderid DESC", BaseConfigs.GetTablePrefix, where)).Tables[0];
        }

        public int AddMenuRelation(int groupid, int menuid)
        {
            string commandText = string.Format("INSERT INTO `{0}menurelations`(groupid, menuid) VALUES ({1}, {2})", BaseConfigs.GetTablePrefix, groupid, menuid);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public int DelMenuRelation(int groupid, int menuid)
        {
            string commandText = string.Format("DELETE FROM `{0}menurelations` WHERE groupid = {1} AND menuid = {2}", BaseConfigs.GetTablePrefix, groupid, menuid);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public int DelMenuRelation(int groupid)
        {
            string commandText = string.Format("DELETE FROM `{0}menurelations` WHERE groupid = {1}", BaseConfigs.GetTablePrefix, groupid);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetMenuRelatetionsByGroupId(int groupid)
        {
            string commandText = string.Format("SELECT menuid FROM `{0}menurelations` WHERE groupid = {1}", BaseConfigs.GetTablePrefix, groupid);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public string GetMenuSearchCondition(int pagetype, int type, int system, string keyword)
        {
            string ret = "id>0 ";
            if (pagetype > 0)
                ret += " AND pagetype=" + pagetype.ToString();
            if (type > 0)
                ret += " AND type=" + type.ToString();
            if (system >= 0)
                ret += " AND system =" + system.ToString();
            if (keyword.Trim().Length > 0)
                ret += string.Format(" AND name like '%{0}%'", RegEsc(keyword));
            return ret;
        }

        public DataTable GetMenuDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "menus", "id", pagecurrent, pagesize, "*", "id desc", condition, out pagecount, out recordcount);
        }

        public bool CheckPageAuthority(int groupid, int menuid)
        {
            string commandText = string.Format("SELECT count(*) FROM `{0}menurelations` WHERE groupid={1} AND menuid = {2}", BaseConfigs.GetTablePrefix, groupid, menuid);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText)) > 0;
        }

        public bool CheckPageAuthority(int groupid, string page)
        {
            string commandText = string.Format("SELECT TOP 1 name FROM `{0}menus`,`{0}menurelations` WHERE url like '{1}%' AND `{0}menurelations`.`groupid` = {2} AND `{0}menurelations`.`menuid` = `{0}menus`.`id`", BaseConfigs.GetTablePrefix, RegEsc(page), groupid);
            return DbHelper.ExecuteScalar(CommandType.Text, commandText) != null;
        }

        #region Mailog
        private DbParameter[] EntityMailog(MailogInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@rgroup",(DbType)SqlDbType.NVarChar,200,info.Rgroup),
                DbHelper.MakeInParam("@mails",(DbType)SqlDbType.NVarChar,2000,info.Mails),
				DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,100,info.Title),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
				DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
				DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,info.Userid),
				DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username)
            };
        }

        public int AddMailog(MailogInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}mailogs` (`rgroup`, `mails`, `title`, `addtime`, `content`, `userid`, `username`) VALUES (@rgroup, @mails, @title, @addtime, @content, @userid, @username);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityMailog(info)));
        }

        public int EditMailog(MailogInfo info)
        {
            string commandText = string.Format("UPDATE `{0}mailogs` SET `rgroup` = @rgroup, `mails` = @mails, `title` = @title, `addtime` = @addtime, `content` = @content, `userid` = @userid, `username` = @username WHERE `{0}mailogs`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityMailog(info));
        }

        public int DelMailog(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}mailogs` WHERE `{0}mailogs`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetMailog(int id)
        {
            string commandText = string.Format("SELECT TOP 1 * FROM `{0}mailogs` WHERE `{0}mailogs`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetMailogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}mailogs", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }

        public string GetMailogSearchCondition(string title, string startdate, string enddate, string users)
        {
            string condition = "id>0";
            if (title.Trim() != "")
                condition += string.Format(" and title like '%{0}%'", RegEsc(title));
            if (startdate != string.Empty)
                condition += string.Format(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition += string.Format(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            users = RegSqlCharList(users);
            if (users != string.Empty)
                condition += string.Format(" AND username IN ({0})", users);
            return condition;
        }
        #endregion

        #region Submail
        private DbParameter[] EntitySubmail(MailsubInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
				DbHelper.MakeInParam("@mail",(DbType)SqlDbType.NVarChar,100,info.Mail),
				DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15,info.Ip),
				DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status),
				DbHelper.MakeInParam("@safecode",(DbType)SqlDbType.NVarChar,50,info.Safecode),
				DbHelper.MakeInParam("@forgroup",(DbType)SqlDbType.NVarChar,50,info.Forgroup)
            };
        }

        public int AddSubmail(MailsubInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}submails` (`name`, `addtime`, `mail`, `ip`, `status`, `safecode`, `forgroup`) VALUES (@name, @addtime, @mail, @ip, @status, @safecode, @forgroup);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntitySubmail(info)));
        }

        public int EditSubmail(MailsubInfo info)
        {
            string commandText = string.Format("UPDATE `{0}submails` SET `name` = @name, `addtime` = @addtime, `mail` = @mail, `ip` = @ip, `status` = @status, `safecode` = @safecode, `forgroup` = @forgroup WHERE `{0}submails`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntitySubmail(info));
        }

        public int DelSubmail(string mail)
        {
            string commandText = string.Format("DELETE FROM `{0}submails` WHERE `{0}submails`.`mail` = @mail", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, DbHelper.MakeInParam("@mail", (DbType)SqlDbType.NVarChar, 100, mail));
        }

        public IDataReader GetSubmail(string mail)
        {
            string commandText = string.Format("SELECT TOP 1 * FROM `{0}submails` WHERE `{0}submails`.`mail` = @mail", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, DbHelper.MakeInParam("@mail", (DbType)SqlDbType.NVarChar, 100, mail));
        }

        public DataTable GetSubmailDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}submails", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }


        public DataTable GetSubmailGroups()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT DISTINCT `forgroup` FROM `{0}submails`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetSubMailList(string fields)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT {1} FROM `{0}submails`", BaseConfigs.GetTablePrefix, fields == "" ? "*" : fields)).Tables[0];
        }

        public string GetSubmailSearchCondition(int status, string name, string mail, string startdate, string enddate, string ip, string forgroup)
        {
            string condition = "id>0";
            if (name.Trim() != "")
                condition += string.Format(" and name like '%{0}%'", RegEsc(name));
            if (mail.Trim() != "")
                condition += string.Format(" and mail like '%{0}%'", RegEsc(mail));
            if (ip.Trim() != "")
                condition += string.Format(" and ip like '%{0}%'", RegEsc(ip));
            if (status >= 0)
                condition += string.Format(" and status = {0}", status);
            if (forgroup != "")
                condition += string.Format(" and forgroup = '{0}'", forgroup);
            if (startdate != string.Empty)
                condition += string.Format(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition += string.Format(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            return condition;
        }
        #endregion
    }
}