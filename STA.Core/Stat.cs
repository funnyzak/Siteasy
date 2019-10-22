using System;
using System.Web;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using System.Collections;
using System.Data.OleDb;

namespace STA.Core
{
    public partial class Stat
    {
        public static string GetCityByIp(string _ip, string def)
        {
            DataView dv1 = new DataView();
            long _ipnow = _GetIP(_ip);
            string Sqll = "";
            Sqll = "select * from Address where StarIP<=" + _ipnow + " and EndIP>=" + _ipnow + "";
            DataTable dt_stat = stat_AcDb(Sqll);

            if (dt_stat != null && dt_stat.Rows.Count > 0)
            {
                string country = dt_stat.Rows[0]["Country"].ToString();
                if (country.IndexOf("保留地址") >= 0) return def;
                return country + dt_stat.Rows[0]["City"].ToString();
            }
            else
            {
                return def;
            }
        }

        /// <summary>
        /// 取IP
        /// </summary>
        /// <param name="_ip"></param>
        /// <returns></returns>

        public static long _GetIP(string _ip)
        {
            string[] _streachip = _ip.Split('.');
            long _intip = 0;
            for (int i = 0; i < 4; i++)
            {
                _intip += (long)(int.Parse(_streachip[i]) * System.Math.Pow(256, 3 - i));
            }
            return _intip;
        }


        /// <summary>
        /// 连接IP地址数据库并执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回数据表</returns>

        protected static DataTable stat_AcDb(string sql)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;data source=" + Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/data/database/AddressIp.mdb") + "";
            try
            {
                conn.Open();
            }
            catch (OleDbException ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "打开IP地址数据库失败! ", ex);
                return new DataTable();
            }
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            DataTable dt = null;
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            try
            {
                dt = ds.Tables["table"];
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "打开IP地址数据库失败,原因未知! ", ex);
                return new DataTable();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
    }
}
