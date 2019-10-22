using System;
using System.Text;
using System.Data;

using STA.Entity;

namespace STA.Data
{
    public class Databases
    {
        /// <summary>
        /// 恢复备份数据库          
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="serverName">服务器名称</param>
        /// <param name="userName">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="fileName">备份文件名</param>
        /// <returns></returns>
        public static string RestoreDatabase(string backupPath, string serverName, string userName, string password, string dbName, string fileName)
        {
            return DatabaseProvider.GetInstance().RestoreDatabase(backupPath, serverName, userName, password, dbName, fileName);
        }

        /// <summary>
        /// 备份数据库          
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="serverName">服务器名称</param>
        /// <param name="userName">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="fileName">备份文件名</param>
        /// <returns></returns>
        public static string BackUpDatabase(string backupPath, string serverName, string userName, string password, string dbName, string fileName)
        {
            return DatabaseProvider.GetInstance().BackUpDatabase(backupPath, serverName, userName, password, dbName, fileName);
        }

        /// <summary>
        /// 是否允许备份数据库
        /// </summary>
        /// <returns></returns>
        public static bool IsBackupDatabase()
        {
            return DbHelper.Provider.IsBackupDatabase();
        }

        /// <summary>
        /// 是否可用全文搜索
        /// </summary>
        /// <returns></returns>
        public static bool IsFullTextSearchEnabled()
        {
            return DbHelper.Provider.IsFullTextSearchEnabled();
        }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        public static string GetDbName()
        {
            return DatabaseProvider.GetInstance().GetDbName();
        }

        /// <summary>
        /// 是否允许创建存储过程
        /// </summary>
        /// <returns></returns>
        public static bool IsStoreProc()
        {
            return DbHelper.Provider.IsStoreProc();
        }

        /// <summary>
        /// 是否支持收缩数据库
        /// </summary>
        /// <returns></returns>
        public static bool IsShrinkData()
        {
            return DbHelper.Provider.IsShrinkData();
        }

        /// <summary>
        /// 收缩数据库
        /// </summary>
        /// <param name="shrinksize">收缩大小</param>
        /// <param name="dbname">数据库名</param>
        public static void ShrinkDataBase(string shrinksize, string dbname)
        {
            DatabaseProvider.GetInstance().ShrinkDataBase(shrinksize, dbname);
        }

        /// <summary>
        /// 清空数据库日志
        /// </summary>
        /// <param name="dbname"></param>
        public static void ClearDBLog(string dbname)
        {
            DatabaseProvider.GetInstance().ClearDBLog(dbname);
        }

        /// <summary>
        /// 运行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public static string RunSql(string sql)
        {
            return DatabaseProvider.GetInstance().RunSql(sql);
        }

        /// <summary>
        /// 获取数据库版本
        /// </summary>
        /// <returns></returns>
        public static string GetDataBaseVersion()
        {
            return DatabaseProvider.GetInstance().GetDataBaseVersion();
        }

        public static DataTable GetFieldListTable(string tablename, string where, string fieldname)
        {
            return DatabaseProvider.GetInstance().GetFieldListTable(tablename, where, fieldname);
        }

        public static string ReplaceTableField(string tablename, string where, string fieldname, string source, string target)
        {
            return DatabaseProvider.GetInstance().ReplaceTableField(tablename, where, fieldname, source, target);
        }

        public static string UpdateTableFieldByPrimaryKey(string tablename, string fieldname, string content, string primarykeyname, string keyvalue)
        {
            return DatabaseProvider.GetInstance().UpdateTableFieldByPrimaryKey(tablename, fieldname, content, primarykeyname, keyvalue);
        }

        public static bool AddTableField(string tablename, string fieldname, string fieldtype, int size)
        {
            return DatabaseProvider.GetInstance().AddTableField(tablename, fieldname, fieldtype, size);
        }

        public static bool DropTableField(string tablename, string fieldname)
        {
            return DatabaseProvider.GetInstance().DropTableField(tablename, fieldname);
        }
        public static bool AddExtTable(string tablename)
        {
            return DatabaseProvider.GetInstance().AddExtTable(tablename);
        }
        public static bool DropTable(string tablename)
        {
            return DatabaseProvider.GetInstance().DropTable(tablename);
        }
        public static bool ReTableName(string newtablename, string oldtablename)
        {
            return DatabaseProvider.GetInstance().ReTableName(newtablename, oldtablename);
        }
        public static int ExistTable(string tablename)
        {
            return DatabaseProvider.GetInstance().ExistTable(tablename);
        }

        public static bool ExistTableField(string tablename, string fieldname)
        {
            return DatabaseProvider.GetInstance().ExistTableField(tablename, fieldname) > 0;
        }

        public static bool EditTableField(string tablename, string fieldname, string fieldtype, int size)
        {
            return DatabaseProvider.GetInstance().EditTableField(tablename, fieldname, fieldtype, size);
        }

        public static bool DbConnectTest(string connectstring)
        {
            return DatabaseProvider.GetInstance().DbConnectTest(connectstring);
        }

        public static string DbConnectString(string datasource, string userid, string password, string dbname)
        {
            return DatabaseProvider.GetInstance().DbConnectString(datasource, userid, password, dbname);
        }

        public static DataTable DbTables(string connectstring)
        {
            return DatabaseProvider.GetInstance().DbTables(connectstring);
        }

        public static DataTable DbTableFields(string connectstring, string tbname)
        {
            return DatabaseProvider.GetInstance().DbTableFields(connectstring, tbname);
        }

        /// <summary>
        /// 根据采集配置获取目标数据库内容
        /// </summary>
        public static DataTable DbCollectDataTableBySet(DbcollectInfo info, int count)
        {
            return DatabaseProvider.GetInstance().DbCollectDataTableBySet(info, count);
        }

        public static DataTable ExecuteTable(string connectstring, string commandText)
        {
            return DatabaseProvider.GetInstance().ExecuteTable(connectstring, commandText);
        }
    }
}
