using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Collects
    {
        public static int AddDbCollect(DbcollectInfo info)
        {
            return DatabaseProvider.GetInstance().AddDbCollect(info);
        }

        public static bool EditDbCollect(DbcollectInfo info)
        {
            return DatabaseProvider.GetInstance().EditDbCollect(info) > 0;
        }

        public static bool DelDbCollect(int id)
        {
            return DatabaseProvider.GetInstance().DelDbCollect(id) > 0;
        }

        public static DbcollectInfo GetDbCollect(int id)
        {
            DbcollectInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetDbCollect(id))
            {
                if (reader.Read())
                {
                    info = new DbcollectInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString();
                    info.Channelid = TypeParse.StrToInt(reader["channelid"]);
                    info.Channelname = reader["channelname"].ToString();
                    info.Constatus = byte.Parse(reader["constatus"].ToString());
                    info.Dbtype = byte.Parse(reader["dbtype"].ToString());
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Datasource = reader["datasource"].ToString();
                    info.Userid = reader["userid"].ToString();
                    info.Password = reader["password"].ToString();
                    info.Dbname = reader["dbname"].ToString();
                    info.Tbname = reader["tbname"].ToString();
                    info.Primarykey = reader["primarykey"].ToString();
                    info.Orderkey = reader["orderkey"].ToString();
                    info.Repeatkey = reader["repeatkey"].ToString();
                    info.Sortby = reader["sortby"].ToString();
                    info.Where = reader["where"].ToString();
                    info.Matchs = reader["matchs"].ToString();
                }
            }
            return info;
        }

        public static DataTable GetDbCollectDataTable(int pageIndex, int pagesize, out int pageCount, out int recordCount)
        {
            return DatabaseProvider.GetInstance().GetDbCollectDataTable(pageIndex, pagesize, out pageCount, out recordCount);
        }

        public static int AddWebCollect(WebcollectInfo info)
        {
            return DatabaseProvider.GetInstance().AddWebCollect(info);
        }

        public static bool EditWebCollect(WebcollectInfo info)
        {
            return DatabaseProvider.GetInstance().EditWebCollect(info) > 0;
        }

        public static bool DelWebCollect(int id)
        {
            return DatabaseProvider.GetInstance().DelWebCollect(id) > 0;
        }

        public static WebcollectInfo GetWebCollect(int id)
        {
            WebcollectInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetWebCollect(id))
            {
                if (reader.Read())
                {
                    info = new WebcollectInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString();
                    info.Channelid = TypeParse.StrToInt(reader["channelid"]);
                    info.Channelname = reader["channelname"].ToString();
                    info.Constatus = byte.Parse(reader["constatus"].ToString());
                    info.Hosturl = reader["hosturl"].ToString();
                    info.Collecttype = (CollectType)TypeParse.StrToInt(reader["collecttype"]);
                    info.Curl = reader["curl"].ToString();
                    info.Clistpage = reader["clistpage"].ToString().Trim();
                    info.Clisturl = reader["clisturl"].ToString();
                    info.Curls = reader["curls"].ToString();
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Encode = reader["encode"].ToString().Trim();
                    info.Property = reader["property"].ToString();
                    info.Filter = reader["filter"].ToString();
                    info.Attrs = reader["attrs"].ToString();
                    info.Setting = reader["setting"].ToString();
                    info.Confilter = reader["confilter"].ToString();
                }
            }
            return info;
        }

        public static DataTable GetWebCollectDataTable(int pageIndex, int pagesize, string where, out int pageCount, out int recordCount)
        {
            return DatabaseProvider.GetInstance().GetWebCollectDataTable(pageIndex, pagesize, where, out pageCount, out recordCount);
        }


        public static string GetWebCollectSearchCondition(string name, int channelid, string startdate, string enddate)
        {
            return DatabaseProvider.GetInstance().GetWebCollectSearchCondition(name, channelid, startdate, enddate);
        }
    }
}
