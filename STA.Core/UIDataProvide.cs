using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Cache;

namespace STA.Core
{
    public class UIDataProvide
    {
        public static DataTable GetUITable(string action, string parms)
        {
            int cache = TypeParse.StrToInt(GetDataParmValue(parms, "cache"), 1);
            //string cachekey = CacheKeys.PAGE_DATA + action.Trim() + parms.Replace(" ", "_").Replace("=", "_").Replace(",", "_");
            string cachekey = CacheKeys.PAGE_DATA + action.Trim() + Regex.Replace(parms, @"\s|=|,", "_");
            DataTable uidt = new DataTable();

            if (cache == 1)
            {
                uidt = Caches.GetObject(cachekey) as DataTable;
                if (uidt != null) return uidt;
            }

            DataParmInfo prminfo = GetDataParm(parms + " ");
            switch (action.Trim())
            {
                case "content": uidt = DatabaseProvider.GetInstance().GetUIContentData(prminfo); break;
                case "link": uidt = DatabaseProvider.GetInstance().GetUILinkData(prminfo); break;
                case "linktype": uidt = DatabaseProvider.GetInstance().GetUILinkTypeData(prminfo); break;
                case "channel": uidt = DatabaseProvider.GetInstance().GetUIChannelData(prminfo); break;
                case "page": uidt = DatabaseProvider.GetInstance().GetUIPageData(prminfo); break;
                case "tag": uidt = DatabaseProvider.GetInstance().GetUITagData(prminfo); break;
                case "comment": uidt = DatabaseProvider.GetInstance().GetUICommentData(prminfo); break;
                case "vote": uidt = DatabaseProvider.GetInstance().GetUIVoteData(prminfo); break;
                case "magazine": uidt = DatabaseProvider.GetInstance().GetUIMagazineData(prminfo); break;
            }

            if (cache == 1 && uidt != null && uidt.Rows.Count > 0) Caches.AddObject(cachekey, uidt);
            return uidt;
        }



        public static DataTable GetDbTable(string table, string parms)
        {
            int cache = TypeParse.StrToInt(GetDataParmValue(parms, "cache"), 1);
            string cachekey = CacheKeys.PAGE_DATA + table.Trim() + "_" + Regex.Replace(parms, @"\s|=|,|\[|\]|'|\(|\)|\<|\>|!|\%", "_");
            DataTable uidt = new DataTable();

            if (cache == 1)
            {
                uidt = Caches.GetObject(cachekey) as DataTable;
                if (uidt != null) return uidt;
            }

            uidt = DatabaseProvider.GetInstance().GetDbTable(table.Trim().ToLower().Replace("sta_", BaseConfigs.GetTablePrefix), TypeParse.StrToInt(GetDataParmValue(parms, "num"), 10)
                                                            , GetDataParmValue(parms, "where").Replace("_", " "), GetDataParmValue(parms, "fields")
                                                            , GetDataParmValue(parms, "order").Replace("_", " "));
            if (cache == 1 && uidt != null && uidt.Rows.Count > 0) Caches.AddObject(cachekey, uidt);
            return uidt;
        }


        public static DataParmInfo GetDataParm(string parms)
        {
            DataParmInfo info = new DataParmInfo();

            info.Num = TypeParse.StrToInt(GetDataParmValue(parms, "num"), 10);
            info.Num = info.Num <= 0 ? 10 : info.Num;

            info.Type = GetDataParmValue(parms, "type");
            info.Ctype = TypeParse.StrToInt(GetDataParmValue(parms, "ctype"), -1);
            info.Id = GetDataParmValue(parms, "id");
            info.Ext = GetDataParmValue(parms, "ext");
            info.Fields = GetDataParmValue(parms, "fields");
            info.Order = TypeParse.StrToInt(GetDataParmValue(parms, "order"));

            info.Durdate = TypeParse.StrToInt(GetDataParmValue(parms, "durdate"), 0);
            info.Page = TypeParse.StrToInt(GetDataParmValue(parms, "page"), 1);
            info.Page = info.Page <= 0 ? 1 : info.Page;

            info.Propery = GetDataParmValue(parms, "property");
            info.Likeid = GetDataParmValue(parms, "likeid");
            info.Uid = TypeParse.StrToInt(GetDataParmValue(parms, "uid"));

            info.Self = TypeParse.StrToInt(GetDataParmValue(parms, "self"), 0);
            info.Ordertype = TypeParse.StrToInt(GetDataParmValue(parms, "ordertype"), 1);
            info.Group = TypeParse.StrToInt(GetDataParmValue(parms, "group"), 0);
            return info;
        }

        private static string GetDataParmValue(string parms, string name)
        {
            Match match = Regex.Match(parms, name + "=([^ ]+)(?:\\s*)", RegexOptions.IgnoreCase);
            if (match == null) return "";
            return match.Groups[1].Value.Trim();
        }

    }
}
