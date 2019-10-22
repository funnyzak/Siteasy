using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Data;
using STA.Cache;
using STA.Config;
using System.Data;

namespace STA.Core.Collect
{
    public class DbCollect
    {
        public static int successcount = 0;
        public static int failcount = 0;
        public static int totalcount = 0;
        public static DataTable cltdt = new DataTable();
        public static DbcollectInfo dbinfo;

        public static void Collect()
        {
            Reset();

            if (dbinfo == null) return;
            short typeid = 1;
            totalcount = cltdt.Rows.Count;
            ChannelInfo chlinfo;
            ContypeInfo tpeinfo;

            if (dbinfo.Channelid > 0)
            {
                chlinfo = Contents.GetChannel(dbinfo.Channelid);
                if (chlinfo != null)
                {
                    typeid = chlinfo.Typeid;
                }
            }

            tpeinfo = Contents.GetContype(typeid);
            string typename = tpeinfo == null ? "" : tpeinfo.Name;
            string channelfamily = "";
            if (dbinfo.Channelid > 0)
                channelfamily = ConUtils.GetChannelFamily(dbinfo.Channelid, ",");
            DataTable matchdt = ConUtils.GetCollectMatchList(dbinfo.Matchs);

            GeneralConfigInfo config = GeneralConfigs.GetConfig();

            foreach (DataRow dr in cltdt.Rows)
            {
                string _value_repeatkey = dr[dbinfo.Repeatkey].ToString().Trim();
                string _value_primarykey = dr[dbinfo.Primarykey].ToString().Trim();

                if (!Contents.CheckContentRepeat(_value_repeatkey))
                {
                    ContentInfo info = new ContentInfo();
                    info.Typeid = typeid;
                    info.Typename = typename;
                    info.Channelid = dbinfo.Channelid;
                    info.Channelname = dbinfo.Channelname;
                    info.Channelfamily = channelfamily;
                    info.Title = _value_repeatkey;
                    info.Title = CollectUtils.TitFormat(config, info.Title);
                    info.Status = dbinfo.Constatus;

                    //字段匹配
                    if (matchdt != null && matchdt.Rows.Count > 0)
                    {
                        info.Addusername = "数据采集";
                        info.Subtitle = TypeParse.ObjToString(MatchValue("subtitle", matchdt, dr));
                        info.Addtime = TypeParse.StrToDateTime(MatchValue("addtime", matchdt, dr));
                        info.Updatetime = TypeParse.StrToDateTime(MatchValue("updatetime", matchdt, dr));
                        info.Color = TypeParse.ObjToString(MatchValue("color", matchdt, dr), "000000");
                        info.Property = TypeParse.ObjToString(MatchValue("property", matchdt, dr));
                        info.Author = TypeParse.ObjToString(MatchValue("author", matchdt, dr));
                        info.Source = TypeParse.ObjToString(MatchValue("source", matchdt, dr));
                        info.Img = TypeParse.ObjToString(MatchValue("img", matchdt, dr));
                        info.Url = TypeParse.ObjToString(MatchValue("url", matchdt, dr));
                        info.Seotitle = TypeParse.ObjToString(MatchValue("seotitle", matchdt, dr));
                        info.Seokeywords = TypeParse.ObjToString(MatchValue("seokeyword", matchdt, dr));
                        info.Seodescription = TypeParse.ObjToString(MatchValue("seodescription", matchdt, dr));
                        info.Savepath = TypeParse.ObjToString(MatchValue("savepath", matchdt, dr));
                        info.Filename = TypeParse.ObjToString(MatchValue("filename", matchdt, dr));
                        info.Template = TypeParse.ObjToString(MatchValue("template", matchdt, dr));
                        info.Content = TypeParse.ObjToString(MatchValue("content", matchdt, dr));
                        info.Content = CollectUtils.ConFormat(config, info.Content);
                        info.Click = TypeParse.StrToInt(MatchValue("click", matchdt, dr));
                        info.Orderid = TypeParse.StrToInt(MatchValue("orderid", matchdt, dr));
                        info.Diggcount = TypeParse.StrToInt(MatchValue("diggcount", matchdt, dr));
                        info.Stampcount = TypeParse.StrToInt(MatchValue("stampcount", matchdt, dr));
                        info.Credits = TypeParse.StrToInt(MatchValue("credits", matchdt, dr));
                        info.Ext = new System.Collections.Hashtable();
                    }

                    if (Contents.AddContent(info) > 0)
                        successcount++;
                    else
                        failcount++;
                }
                else
                {
                    failcount++;
                }
            }
            dbinfo = null;
            cltdt.Dispose();
        }

        private static object MatchValue(string name, DataTable matchdt, DataRow currow)
        {
            DataRow[] drs = matchdt.Select("name='" + name + "'");
            if (drs != null && drs.Length > 0)
                return currow[drs[0]["sname"].ToString()];
            return null;
        }

        public static void Reset()
        {
            successcount = 0;
            failcount = 0;
            totalcount = 0;
            cltdt.Dispose();
        }
    }
}
