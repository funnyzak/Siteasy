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
        public int AddActionColumn(ActioncolumnInfo info)
        {
            string commandText = string.Format("INSERT INTO [{0}actioncolumns] (columnname, status) VALUES (@columnname, @status);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityActionColumn(info)));
        }

        public int EditActionColumn(ActioncolumnInfo info)
        {
            string commandText = string.Format("UPDATE [{0}actioncolumns] SET columnname = @columnname,status = @status WHERE [{0}actioncolumns].[id] = {1}", BaseConfigs.GetTablePrefix, info.Id);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityActionColumn(info));
        }

        public int DelActionColumn(int id)
        {
            string commandText = string.Format("UPDATE [{0}actions] SET columnid = 0 WHERE [{0}actions].[columnid] = {1}; DELETE FROM [{0}actioncolumns] WHERE [{0}actioncolumns].[id] = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetActionColumn(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT columnname,status WHERE [{0}actioncolumns].[id] = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int AddAction(ActionInfo info)
        {
            if (TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT * FROM [{0}actions] WHERE [{0}actions].[action]={1}", BaseConfigs.GetTablePrefix, info.Action.Trim())), 0) > 0)
                return 0;
            string commandText = string.Format("INSERT INTO [{0}actions] (actionname,columnid,action,status) VALUES (@actionname,@columnid,@action,@status);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityAction(info)));
        }

        public int EditAction(ActionInfo info)
        {
            string commandText = string.Format("UPDATE [{0}actions] SET actionname = @actionname,columnid = @columnid,action = @action, status=@status"
                            + " WHERE [{0}actions].[id] = @id", BaseConfigs.GetTablePrefix, info.Id);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityAction(info)));
        }

        public int DelAction(int id)
        {
            string commandText = string.Format("DELETE FROM [{0}actiongroups] WHERE [{0}actiongroups].[action] = {1};DELETE FROM [{0}actions] WHERE [{0}actions].[id] = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetAction(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * WHERE [{0}actions].[id] = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int AddActionGroup(ActiongroupInfo info)
        {
            if (TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT * FROM [{0}actiongroups] WHERE [{0}actiongroups].[gid]={1} AND [{0}actiongroups].[action] = {2}", BaseConfigs.GetTablePrefix, info.Gid, info.Action)), 0) > 0)
                return 0;
            string commandText = string.Format("INSERT INTO [{0}actiongroups] (gid,action,adminname,addtime) VALUES (@gid,@action,@adminname,@addtime);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityActionGroup(info)));
        }

        public int DelActionGroup(int id)
        {
            string commandText = string.Format("DELETE FROM [{0}actiongroups] WHERE [{0}actiongroups].[id] = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public int CheckAction(int gid, string actionname)
        {
            int actionid = TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT id WHERE [{0}actions].[actionname] = {1}", BaseConfigs.GetTablePrefix, actionname)));
            if (actionid == 0) return -1;
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT * FROM [{0}actiongroups]"
                    + " WHERE [{0}actiongroups].[gid] = {1} AND [{0}actiongroups].[action] = {2}", BaseConfigs.GetTablePrefix, gid, actionid)));
        }

        public DataTable GetActionListByGroupId(int gid)
        {
            string commandText = string.Format("SELECT [ag].[action] [actionid],[actionname],[columnid],[as].[action] [identity] FROM [{0}actiongroups] [ag] INNER JOIN [{0}actions] [as]"
                        + " ON [ag].[action] = [as].[id] WHERE [ag].[gid] = {1}", BaseConfigs.GetTablePrefix, gid);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }


        public DataTable GetActionDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "actions", "id", pagecurrent, pagesize, "*", "id desc", string.Empty, out pagecount, out recordcount);
        }

        public DataTable GetActionColumns()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM [{0}actioncolumns]", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        private DbParameter[] EntityAction(ActionInfo info)
        {
            return new DbParameter[] {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@actionname",(DbType)SqlDbType.NVarChar,20,info.Actionname),
                DbHelper.MakeInParam("@columnid",(DbType)SqlDbType.Int,4,info.Columnid),
                DbHelper.MakeInParam("@action",(DbType)SqlDbType.NVarChar,30,info.Action),
                DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status)
            };
        }

        private DbParameter[] EntityActionColumn(ActioncolumnInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@columnname",(DbType)SqlDbType.NVarChar,20,info.Columnname),
                DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status)
            };
        }

        private DbParameter[] EntityActionGroup(ActiongroupInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@gid",(DbType)SqlDbType.Int,4,info.Gid),
                DbHelper.MakeInParam("@action",(DbType)SqlDbType.Int,4,info.Action),
                DbHelper.MakeInParam("@adminname",(DbType)SqlDbType.NVarChar,20,info.Adminname),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime)
            };
        }



    }
}
