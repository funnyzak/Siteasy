using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

using STA.Data;
using STA.Config;
using STA.Common;
using STA.Entity.Plus;
using System.Text.RegularExpressions;

namespace STA.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {

        public bool UpdateStaVoteCount(int vid, int iid)
        {
            VoteInfo info = STA.Data.Plus.GetStaVote(vid);
            string pattern = string.Format(@"<item id='{0}' count='(\d+)'>([\s\S]+?)</item>", iid.ToString());
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            foreach (Match m in r.Matches(info.Items))
            {
                info.Items = info.Items.Replace(m.Groups[0].Value, m.Groups[0].Value.Replace(m.Groups[1].Value, (TypeParse.StrToInt(m.Groups[1].Value, 1) + 1).ToString()));
            }
            return EditStaVote(info) > 0;
        }

        private DbParameter[] EntityStaVote(VoteInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,300,info.Title),
                DbHelper.MakeInParam("@startdate",(DbType)SqlDbType.DateTime,8, info.StartDate),
                DbHelper.MakeInParam("enddate",(DbType)SqlDbType.DateTime,8,info.EndDate),
                DbHelper.MakeInParam("@ismore",(DbType)SqlDbType.TinyInt,1, info.IsMore),
                DbHelper.MakeInParam("@isview",(DbType)SqlDbType.TinyInt,1,info.IsView),
                DbHelper.MakeInParam("@isenable",(DbType)SqlDbType.TinyInt,1,info.IsEnable),
                DbHelper.MakeInParam("@interval",(DbType)SqlDbType.Int,4,  info.Interval),
                DbHelper.MakeInParam("@items", (DbType)SqlDbType.NText,0,info.Items)
            };
        }

        public DataTable GetStaVoteTable(int pageIndex, string where, out int pagecount, out int recordcount)
        {
            return Contents.GetDataPage(BaseConfigs.GetTablePrefix + "plusvotes", "id", pageIndex, 20, "*", "id desc", where, out pagecount, out recordcount);
        }


        public int AddStaVote(VoteInfo info)
        {
            string sql = string.Format("insert into {0}plusvotes(title,startdate,enddate,ismore,isview,isenable,interval,items)"
                        + "values(@title,@startdate,@enddate,@ismore,@isview,@isenable,@interval,@items)", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, EntityStaVote(info));
        }

        public int EditStaVote(VoteInfo info)
        {
            string sql = string.Format("update {0}plusvotes set title = @title,startdate=@startdate,enddate=@enddate,ismore=@ismore,isview=@isview,isenable=@isenable,interval=@interval,items=@items where id = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, EntityStaVote(info));
        }

        public int DelStaVote(int id)
        {
            string sql = string.Format("delete from [{0}plusvotes] where id = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public IDataReader GetStaVote(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("select * from {0}plusvotes where id = {1}", BaseConfigs.GetTablePrefix, id));
        }

        private DbParameter[] EntityVariable(VariableInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@likeid",(DbType)SqlDbType.NVarChar,50,info.Likeid),
                DbHelper.MakeInParam("@key",(DbType)SqlDbType.NVarChar,50,info.Key),
                DbHelper.MakeInParam("@desc",(DbType)SqlDbType.NVarChar,200,info.Desc),
                DbHelper.MakeInParam("@value",(DbType)SqlDbType.NVarChar,3000,info.KValue),
                DbHelper.MakeInParam("@system",(DbType)SqlDbType.TinyInt,1,info.System)
            };
        }

        public int AddVariables(VariableInfo info)
        {
            string sql = string.Format("INSERT INTO [{0}variables] ([name], [likeid], [key], [desc], [value], [system]) VALUES (@name, @likeid, @key, @desc, @value, @system);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, EntityVariable(info)));
        }

        public int EditVariable(VariableInfo info)
        {
            string sql = string.Format("UPDATE [sta_variables] SET [name] = @name, [likeid] = @likeid, [key] = @key, [desc] = @desc, [value] = @value, [system] = @system WHERE [{0}variables].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, EntityVariable(info));
        }

        public int DelVariable(int id)
        {
            string sql = string.Format("DELETE FROM [{0}variables] WHERE [{0}variables].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        public int DelVariable(string likeid)
        {
            string sql = string.Format("DELETE FROM [{0}variables] WHERE [{0}variables].[likeid] = @likeid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, DbHelper.MakeInParam("@likeid", (DbType)SqlDbType.NVarChar, 50, likeid));
        }

        public int DelVariableByKey(string key)
        {
            string sql = string.Format("DELETE FROM [{0}variables] WHERE [{0}variables].[key] = @key", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, DbHelper.MakeInParam("@key", (DbType)SqlDbType.NVarChar, 50, key));
        }

        public IDataReader GetVariable(int id)
        {
            string sql = string.Format("SELECT TOP 1 * FROM [{0}variables] WHERE [{0}variables].[id] = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, sql, DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        public IDataReader GetVariable(string key)
        {
            string sql = string.Format("SELECT TOP 1 * FROM [{0}variables] WHERE [{0}variables].[key] = @key", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, sql, DbHelper.MakeInParam("@key", (DbType)SqlDbType.NVarChar, 50, key));
        }

        public bool ExistVariableKey(string key)
        {
            string sql = string.Format("SELECT id FROM [{0}variables] WHERE [{0}variables].[key] = @key", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, DbHelper.MakeInParam("@key", (DbType)SqlDbType.NVarChar, 50, key))) > 0;
        }

        public DataTable GetVariableList(string fields, string likeid)
        {
            string sql = string.Format("SELECT {0} FROM [{1}variables] {2}", (fields.Trim() == "" ? "*" : fields), BaseConfigs.GetTablePrefix,
                likeid.Trim() == "" ? "" : (string.Format("WHERE [{0}variables].[likeid] = '{1}'", BaseConfigs.GetTablePrefix, likeid)));
            return DbHelper.ExecuteDataset(sql).Tables[0];
        }

        public DataTable GetVariableLikeidList()
        {
            string sql = string.Format("SELECT DISTINCT [likeid] FROM [{0}variables]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(sql).Tables[0];
        }

        public string GetVariableCondition(string name, string likeid, string key)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (name != string.Empty)
                condition.AppendFormat(" AND ([name] LIKE '%{0}%')", RegEsc(name));
            if (likeid != string.Empty)
                condition.AppendFormat(" AND ([likeid] LIKE '%{0}%')", RegEsc(likeid));
            if (key != string.Empty)
                condition.AppendFormat(" AND ([key] LIKE '%{0}%')", RegEsc(key));
            return condition.ToString();
        }

        public DataTable GetVariableDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return Contents.GetDataPage(BaseConfigs.GetTablePrefix + "variables", "id", pagecurrent, pagesize, fields.Trim() == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }
    }
}
