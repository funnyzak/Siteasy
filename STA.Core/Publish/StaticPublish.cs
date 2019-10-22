using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Data;
using STA.Cache;
using STA.Config;
using System.Threading;
using System.Data;

namespace STA.Core.Publish
{
    public class StaticPublish
    {
        public static int successcount = 0;
        public static int failcount = 0;
        public static int totalcount = 0;
        public static DataTable chldt;
        public static DataTable condt;
        public static DataTable pgedt;
        public static DataTable specdt;
        public static DataTable rssdt;
        public static GeneralConfigInfo config;


        public StaticPublish()
        {
            config = GeneralConfigs.GetConfig();
        }

        public static void Reset()
        {
            //chldt.Dispose();
            //condt.Dispose();
            //pgedt.Dispose();
            //specdt.Dispose();

            Caches.RemovePageDataCache();
            Caches.RemoveDataTableCache();
            Caches.RemoveUrlCache();
            config = GeneralConfigs.GetConfig();
            successcount = 0;
            failcount = 0;
            totalcount = 0;
        }

        public static void OnKeyPublish(bool publish)
        {
            if (!publish)
            {
                chldt = Contents.GetPublishChannelTable("");
                condt = Contents.GetPublishContentTable("");
                pgedt = Contents.GetPublishPageTable("");
                specdt = Contents.GetPublishSpecialTable("");
                rssdt = Contents.GetPublishRssTable("");
            }

            //if (publish) Caches.RemovePageDataCache();

            PublishChannel(publish);
            PublishRss(publish);
            PublishContent(publish);
            PublishPage(publish);
            PublishSpecial(publish);
            PublishIndex(publish);
            PublishSiteMap(publish);
        }

        public static void PublishChannel(bool publish)
        {
            if (chldt == null || chldt.Rows.Count <= 0) return;
            foreach (DataRow dr in chldt.Rows)
            {
                int id = TypeParse.StrToInt(dr["id"], 0);
                int ctype = TypeParse.StrToInt(dr["ctype"], 1); //1列表 2封面
                int pagecount = ConUtils.ChannelPageCount(config, ctype, id, TypeParse.StrToInt(dr["listcount"]), dr["content"].ToString());
                if (!publish)
                {
                    totalcount += pagecount;
                }
                else
                {
                    string filename = dr["filename"].ToString().Trim();
                    string savepath = dr["savepath"].ToString().Trim();
                    string pagerule = dr["listrule"].ToString().Trim();
                    for (int i = 1; i <= pagecount; i++)
                    {
                        if (StaticCreate.CreateChannel(config, id, ctype, i, savepath, filename, pagerule))
                            successcount++;
                        else
                            failcount++;
                    }
                }
            }
            if (publish) { chldt.Dispose(); }
        }

        public static void PublishRss(bool publish)
        {
            if (rssdt == null || rssdt.Rows.Count <= 0) return;
            foreach (DataRow dr in rssdt.Rows)
            {
                int id = TypeParse.StrToInt(dr["id"], 0);
                if (!publish)
                {
                    totalcount += 1;
                }
                else
                {
                    if (StaticCreate.CreateRss(config, id, config.Rssconcount))
                        successcount++;
                    else
                        failcount++;
                }
            }
            if (publish) { rssdt.Dispose(); }
        }

        public static void PublishContent(bool publish)
        {
            if (condt == null || condt.Rows.Count <= 0) return;

            foreach (DataRow dr in condt.Rows)
            {
                string content = dr["content"].ToString();
                int pagecount = config.Contentpage == 1 ? ConUtils.ConPageCount(content) : 1;
                if (!publish)
                {
                    totalcount += pagecount;
                }
                else
                {
                    int id = TypeParse.StrToInt(dr["id"]);
                    string filename = dr["filename"].ToString().Trim();
                    string savepath = dr["savepath"].ToString().Trim();
                    for (int i = 1; i <= pagecount; i++)
                    {
                        if (StaticCreate.CreateContent(config, id, i, savepath, filename))
                            successcount++;
                        else
                            failcount++;
                    }
                }
            }
            if (publish) condt.Dispose();
        }

        public static void PublishPage(bool publish)
        {
            if (pgedt == null || pgedt.Rows.Count <= 0) return;

            foreach (DataRow dr in pgedt.Rows)
            {
                string content = dr["content"].ToString();
                int pagecount = config.Contentpage == 1 ? ConUtils.ConPageCount(content) : 1;
                if (!publish)
                {
                    totalcount += pagecount;
                }
                else
                {
                    int id = TypeParse.StrToInt(dr["id"]);
                    string filename = dr["filename"].ToString().Trim();
                    string savepath = dr["savepath"].ToString().Trim();

                    for (int i = 1; i <= pagecount; i++)
                    {
                        if (StaticCreate.CreatePage(config, id, i, savepath, filename))
                            successcount++;
                        else
                            failcount++;
                    }
                }
            }
            if (publish) pgedt.Dispose();
        }

        public static void PublishSpecial(bool publish)
        {
            if (specdt == null || specdt.Rows.Count <= 0) return;
            if (!publish)
            {
                foreach (DataRow dr in specdt.Rows)
                {
                    totalcount += Contents.GetSpecgroups(TypeParse.StrToInt(dr["id"])).Rows.Count + 1;
                }
                return;
            }

            foreach (DataRow dr in specdt.Rows)
            {
                int id = TypeParse.StrToInt(dr["id"]);
                string filename = dr["filename"].ToString().Trim();
                string savepath = dr["savepath"].ToString().Trim();

                if (StaticCreate.CreateSpecial(config, id, savepath, filename))
                    successcount++;
                else
                    failcount++;

                DataTable spgroupdt = Contents.GetSpecgroups(TypeParse.StrToInt(dr["id"]));
                foreach (DataRow idr in spgroupdt.Rows)
                {
                    if (StaticCreate.CreateSpecialGroup(config, id, TypeParse.StrToInt(idr["id"]), savepath, filename))
                        successcount++;
                    else
                        failcount++;
                }


            }
            if (publish) specdt.Dispose();
        }

        public static void PublishSiteMap(bool publish)
        {
            totalcount += 1;
            if (publish)
            {
                if (StaticCreate.CreateSiteMap())
                {
                    ++successcount;
                }
                else
                {
                    ++failcount;
                }
            }
        }

        public static void PublishIndex(bool publish)
        {
            totalcount += 1;
            if (publish)
            {
                if (StaticCreate.CreateIndex())
                {
                    ++successcount;
                }
                else
                {
                    ++failcount;
                }
            }
        }
    }
}
