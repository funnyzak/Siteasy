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
using STA.Cache;
using System.Collections;

namespace STA.Core
{
    public partial class Caches
    {
        public static UserInfo GetUser(int uid, int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.USERINFO + "U" + uid;
            UserInfo info = cache.RetrieveObject(key) as UserInfo;
            if (info != null) return info;

            info = Users.GetUser(uid);
            if (info != null) cache.AddObject(key, info, cachetime * 60);

            return info;
        }

        public static DataTable GetSimpleContentTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "allsimplecontent";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            cdt = Contents.GetContentTableByWhere(200000000, "id,title,adduser,typeid,iscomment,channelid,img,viewgroup,savepath,filename,credits,status,orderid", ""); ;
            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime * 60);

            return cdt;
        }

        public static string GetVariable(string key, int cachetime)
        {
            return TypeParse.ObjToString(GetVariableTable(cachetime)[key]);
        }


        public static Hashtable GetVariableTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.PAGE_VARIABLE + "custom_variables";
            Hashtable hvs = cache.RetrieveObject(key) as Hashtable;
            if (hvs != null) return hvs;

            DataTable dt = Plus.GetVariableList("[key],[value]", "");

            if (dt != null && dt.Rows.Count > 0)
            {
                hvs = new Hashtable(dt.Rows.Count);

                foreach (DataRow dr in dt.Rows)
                    hvs[dr["key"]] = dr["value"];
                cache.AddObject(key, hvs, cachetime * 60);
                return hvs;
            }
            else
            {
                return new Hashtable();
            }

        }

        public static DataRow GetContent(int id, int cachetime)
        {
            DataRow[] drs = GetSimpleContentTable(cachetime).Select("id=" + id.ToString());
            if (drs.Length > 0) return drs[0];
            return null;
        }

        public static DataTable GetChannelTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "allchannel";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            cdt = Contents.GetChannelDataTable("id,typeid,parentid,name,savepath,filename,ctype,img,addtime,conrule,listrule,moresite,siteurl,ispost,ishidden,orderid," +
                  "listcount,viewgroup,viewcongroup,ipdenyaccess,ipaccess");
            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime * 60);

            return cdt;
        }

        public static DataTable GetTagTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "alltags";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            cdt = Tags.GetHotTags(10000);
            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime * 60);

            return cdt;
        }
        /// <summary>
        /// 获取联动所有项
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSelectTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "allselect";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            cdt = Selects.GetSelectByWhere(Selects.GetSelectSearchCondition("", 0, 0));

            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime * 60);

            return cdt;
        }


        /// <summary>
        /// 获取联动的下级列表
        /// </summary>
        /// <param name="ename"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable SelectSubListByVal(string ename, string value, int cachetime)
        {
            #region
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "selectsublist_" + ename + "_" + value;
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            DataTable dt = GetSelectTable(cachetime);
            if (dt == null && dt.Rows.Count <= 0) return dt;

            cdt = dt.Clone();
            decimal temv = TypeParse.StrToDecimal(value);

            if (temv == 0)
            {
                foreach (DataRow dr in dt.Select("ename='" + ename + "'"))
                {
                    decimal temval = TypeParse.StrToDecimal(dr["value"].ToString().Trim());
                    if (temval % 500 == 0)
                    {
                        DataRow ndr = cdt.NewRow();
                        ndr["value"] = dr["value"];
                        ndr["name"] = dr["name"];
                        cdt.Rows.Add(ndr);
                    }
                }
            }
            else if (temv % 500 == 0)
            {
                foreach (DataRow dr in dt.Select("ename='" + ename + "'"))
                {
                    decimal temval = TypeParse.StrToDecimal(dr["value"].ToString().Trim());
                    if (temval > temv && temval < (temv + 500) && temval % 1 == 0)
                    {
                        DataRow ndr = cdt.NewRow();
                        ndr["value"] = dr["value"];
                        ndr["name"] = dr["name"];
                        cdt.Rows.Add(ndr);
                    }
                }
            }
            else if (temv % 500 > 0 && temv % 1 == 0)
            {
                foreach (DataRow dr in dt.Select("ename='" + ename + "'"))
                {
                    decimal temval = TypeParse.StrToDecimal(dr["value"].ToString().Trim());
                    if (temval % 500 > 0 && temval > temv && temval < (temv + 0.999M))
                    {
                        DataRow ndr = cdt.NewRow();
                        ndr["value"] = dr["value"];
                        ndr["name"] = dr["name"];
                        cdt.Rows.Add(ndr);
                    }
                }
            }

            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt);

            return cdt;
            #endregion
        }

        /// <summary>
        /// 获取菜单页类型的菜单
        /// </summary>
        /// <param name="type">1后台 2前台</param>
        /// <returns></returns>
        public static DataTable GetMenus(int type, int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "allmenu" + type.ToString();
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            cdt = Menus.GetMenuTable(type, PageType.菜单页);
            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime);

            return cdt;
        }

        public static DataTable GetJumpContentTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "jumpcontentlist";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null) return cdt;

            cdt = DatabaseProvider.GetInstance().GetJumpContentList();
            if (cdt != null && cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime);

            return cdt;
        }

        /// <summary>
        /// 获取标识列表 格式如 photo|soft|hr
        /// </summary>
        /// <returns></returns>
        public static string GetContypeUrlList(string split, int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.STRING + "contypeurllist" + (split == "," ? "comma" : "split");
            string lists = cache.RetrieveObject(key) as string;
            if (lists != null && lists != "") return lists;

            lists = ConUtils.DataTableToString(GetContypeTable(cachetime), "ename", split);
            cache.AddObject(key, lists, cachetime);
            return lists;
        }

        public static DataTable GetContypeTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "contypelist";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null && cdt.Rows.Count > 0) return cdt;

            cdt = Contents.GetContypeDataTable();
            if (cdt != null & cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime);

            return cdt;
        }

        public static DataTable GetConnectTable(int cachetime)
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.DATATABLE + "oauthconnectlist";
            DataTable cdt = cache.RetrieveObject(key) as DataTable;
            if (cdt != null && cdt.Rows.Count > 0) return cdt;

            cdt = new DataTable();

            cdt.Columns.Add("name", Type.GetType("System.String"));
            cdt.Columns.Add("valid", Type.GetType("System.String"));
            cdt.Columns.Add("appid", Type.GetType("System.String"));
            cdt.Columns.Add("appkey", Type.GetType("System.String"));

            XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/config/connect.config"));
            foreach (XmlNode node in doc.SelectNodes("/config/connect"))
            {
                DataRow dr = cdt.NewRow();
                dr["name"] = node.Attributes["name"].Value.Trim();
                dr["valid"] = node.Attributes["valid"].Value.Trim();
                dr["appid"] = node.Attributes["appid"].Value.Trim();
                dr["appkey"] = node.Attributes["appkey"].Value.Trim();
                cdt.Rows.Add(dr);
            }

            if (cdt != null & cdt.Rows.Count > 0) cache.AddObject(key, cdt, cachetime);

            return cdt;
        }

        /// <summary>
        /// 数字正则式静态实例
        /// </summary>
        private static Regex r = new Regex("\\{(\\d+)\\}", Utils.GetRegexCompiledOptions());

        /// <summary>
        /// 返回脏字过滤列表
        /// </summary>
        /// <returns>返回脏字过滤列表数组</returns>
        public static string[,] GetBanWordList()
        {
            STACache cache = STACache.GetCacheService();
            string key = CacheKeys.STRING + "BanWordList";
            string[,] str = cache.RetrieveObject(key) as string[,];
            if (str == null)
            {
                DataTable dt = DatabaseProvider.GetInstance().GetBanWordList();
                str = new string[dt.Rows.Count, 2];
                string temp = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    temp = dt.Rows[i]["find"].ToString().Trim();
                    foreach (Match m in r.Matches(temp))
                    {
                        temp = temp.Replace(m.Groups[0].ToString(), m.Groups[0].ToString().Replace("{", ".{0,"));
                    }
                    str[i, 0] = BanWords.ConvertRegexCode(temp);
                    str[i, 1] = dt.Rows[i]["replacement"].ToString().Trim();
                }
                cache.AddObject(key, str);
                dt.Dispose();
            }
            return str;
        }

        public static object GetObject(string key)
        {
            STACache cache = STACache.GetCacheService();
            return cache.RetrieveObject(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="o"></param>
        public static void AddObject(string key, object o)
        {
            STACache.GetCacheService().AddObject(key, o, GeneralConfigs.GetConfig().Cacheinterval * 60);
        }

        public static void RemoveSetCache()
        {
            RemoveObject(CacheKeys.SET);
        }

        public static void RemoveUserInfoCache()
        {
            RemoveObject(CacheKeys.USERINFO);
        }

        public static void RemoveTemplateNameCache()
        {
            RemoveObject(CacheKeys.TEMPLATE_NAME);
        }

        public static void RemoveDataTableCache()
        {
            RemoveObject(CacheKeys.DATATABLE);
        }

        public static void RemoveUrlCache()
        {
            RemoveObject(CacheKeys.URL);
        }

        /// <summary>
        /// 移除前台数据调用缓存
        /// </summary>
        public static void RemovePageDataCache()
        {
            RemoveObject(CacheKeys.PAGE_DATA);
        }

        public static void RemoveVariableCache()
        {
            RemoveObject(CacheKeys.PAGE_VARIABLE);
        }

        public static void RemoveStringCache()
        {
            RemoveObject(CacheKeys.STRING);
        }

        public static void RemoveObject(string key)
        {
            STACache.GetCacheService().RemoveObject(key);
        }

        public static void RomoveCache()
        {
            RemoveVariableCache();
            RemoveStringCache();
            RemovePageDataCache();
            RemoveDataTableCache();
            RemoveUserInfoCache();
            RemoveSetCache();
            RemoveUrlCache();
            RemoveTemplateNameCache();
        }

        /// <summary>
        /// 更新所有缓存
        /// </summary>
        public static void ReSetAllCache()
        {
            STACache.GetCacheService().FlushAll();
        }

    }
}
