using System;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Cache;
using STA.Entity;
using System.Data;

namespace STA.Core
{
    public partial class Urls
    {
        //protected static GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();

        //public static void ReSetUrl()
        //{
        //    configinfo = GeneralConfigs.GetConfig();
        //}

        #region 获取文档链接

        /// <summary>
        ///  文档是否跳转类型
        /// </summary>
        public static bool ContentJumpUrl(int id, out string url)
        {
            url = "";

            DataTable jumptable = Caches.GetJumpContentTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            if (jumptable == null) return false;

            DataRow[] dr = jumptable.Select("id=" + id.ToString());

            if (dr.Length == 0) return false;

            url = dr[0]["url"].ToString().Trim();
            return true;
        }

        public static string Content(int id, int typeid)
        {
            return Content(GeneralConfigs.GetConfig(), id, typeid, "", "");
        }

        public static string Content(int id, int typeid, string savepath, string filename)
        {
            return Content(GeneralConfigs.GetConfig(), id, typeid, savepath, filename);
        }

        public static string Content(GeneralConfigInfo configinfo, int id, int typeid, string savepath, string filename)
        {
            string key = CacheKeys.URL + "content_" + id.ToString();
            string url = Caches.GetObject(key) as string;
            if (url != null) return url;

            if (!ContentJumpUrl(id, out url))
            {
                ContentInfo con = null;
                ChannelInfo chl = null;

                bool powerset = ConUtils.IsDynamicedCon(configinfo, id, ref con, ref chl);

                if (configinfo.Dynamiced == 0 && !powerset && con != null)
                {
                    string savename = savepath.Trim() + "/" + filename.Trim();
                    if (savename == "/")
                    {
                        if (con != null)
                        {
                            savename = con.Savepath + "/" + con.Filename;
                        }
                        else
                        {
                            savename = Contents.ContentSaveName(id).Trim();
                        }

                    }

                    if (savename.EndsWith("/index"))
                    {
                        url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savename.Substring(0, savename.LastIndexOf("/index") + 1);
                    }
                    else
                    {
                        if (savename.EndsWith("/")) savename += id.ToString();
                        url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savename + configinfo.Suffix;
                    }
                }
                else
                {
                    if (typeid == -1 && con != null)
                    {
                        typeid = con.Typeid;
                    }
                    string name = "";

                    DataRow[] ctr = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + typeid.ToString());
                    if (ctr.Length > 0)
                        name = ctr[0]["ename"].ToString().Trim();
                    else
                        name = "content";

                    if (configinfo.Dynamiced == 2)
                        url = BaseConfigs.GetSitePath + "/" + name + "-" + id.ToString() + configinfo.Rewritesuffix;
                    else
                        url = BaseConfigs.GetSitePath + "/" + name + ".aspx?id=" + id.ToString();

                }
            }
            url = configinfo.Withweburl > 0 ? (configinfo.Weburl + url) : url;

            if (url != null && url != "")
                Caches.AddObject(key, url);

            return url;
        }

        public static string Special(int id)
        {
            return Content(id, 0);
        }

        public static string Soft(int id)
        {
            return Content(id, 3);
        }

        public static string Photo(int id)
        {
            return Content(id, 2);
        }

        public static string Info(int id)
        {
            return Content(id, 5);
        }

        public static string Product(int id)
        {
            return Content(id, 4);
        }

        public static string Content(int id)
        {
            return Content(id, -1);
        }

        public static string SpecGroup(GeneralConfigInfo configinfo, int specid, int group)
        {
            string key = CacheKeys.URL + "/specgroup_" + specid.ToString() + "_" + group.ToString();
            string url = Caches.GetObject(key) as string;
            if (url != null) return url;

            if (configinfo.Dynamiced == 1)
            {
                url = BaseConfigs.GetSitePath + "/specgroup.aspx?id=" + specid.ToString() + "&group=" + group.ToString();
            }
            else if (configinfo.Dynamiced == 2)
            {
                url = BaseConfigs.GetSitePath + "/specgroup-" + specid.ToString() + "-" + group.ToString() + configinfo.Rewritesuffix;
            }
            else
            {
                string savename = Contents.ContentSaveName(specid);
                if (savename.EndsWith("/")) savename += specid.ToString();

                savename += "_list_" + group.ToString();
                url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savename + configinfo.Suffix;
            }
            url = configinfo.Withweburl > 0 ? (configinfo.Weburl + url) : url;

            if (url != null && url != "")
                Caches.AddObject(key, url);

            return url;
        }

        public static string SpecGroup(int specid, int group)
        {
            return SpecGroup(GeneralConfigs.GetConfig(), specid, group);
        }

        #endregion

        #region 获取单页链接

        public static string Page(int id)
        {
            return Page(GeneralConfigs.GetConfig(), id);
        }

        public static string Page(GeneralConfigInfo configinfo, int id)
        {
            string key = CacheKeys.URL + "/page_" + id.ToString();
            string url = Caches.GetObject(key) as string;
            if (url != null) return url;

            if (configinfo.Dynamiced == 1)
            {
                url = BaseConfigs.GetSitePath + "/page.aspx?id=" + id.ToString();
            }
            else if (configinfo.Dynamiced == 2)
            {
                url = BaseConfigs.GetSitePath + "/page-" + id + configinfo.Rewritesuffix;
            }
            else
            {
                string savename = Contents.PageSaveName(id);
                if (savename.EndsWith("/index"))
                {
                    url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savename.Substring(0, savename.LastIndexOf("/index") + 1);
                }
                else
                {
                    if (savename.EndsWith("/")) savename += id.ToString();
                    url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savename + configinfo.Suffix;
                }
            }
            url = configinfo.Withweburl > 0 ? (configinfo.Weburl + url) : url;

            if (url != null && url != "")
                Caches.AddObject(key, url);

            return url;
        }

        #endregion

        #region 获取频道链接

        /// <summary>
        ///  频道是否跳转类型
        /// </summary>
        public static bool ChannelJumpUrl(int id, out string url)
        {
            url = "";

            DataTable jumptable = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            if (jumptable == null) return false;

            DataRow[] dr = jumptable.Select("id=" + id.ToString() + " and ctype=3");

            if (dr.Length == 0) return false;

            url = dr[0]["savepath"].ToString().Trim();
            return true;
        }

        public static string Channel(int id, int page)
        {
            return Channel(GeneralConfigs.GetConfig(), id, page);
        }

        public static string Channel(GeneralConfigInfo configinfo, int id, int page)
        {
            string key = CacheKeys.URL + "/channel_" + id.ToString();
            string url = Caches.GetObject(key) as string;
            if (url != null) return url;

            ChannelInfo chl = null;

            bool powerset = ConUtils.IsDynamicedChl(configinfo, id, ref chl);

            bool isjumpurl = ChannelJumpUrl(id, out url);
            if (!isjumpurl)
            {
                if (configinfo.Dynamiced == 0 && !powerset && chl != null)
                {
                    string savepath, filename, pagerule;
                    filename = chl.Filename;
                    if (filename == "") filename = id.ToString();

                    if (chl.Moresite == 1) //是否开启了多站点支持
                    {
                        url = chl.Siteurl + (filename == "index" ? "" : ("/" + filename + configinfo.Suffix));
                    }
                    else
                    {
                        savepath = chl.Savepath;
                        pagerule = chl.Listrule;
                        int ctype = chl.Ctype;
                        if (page > 1)
                        {
                            if (ctype == 1)
                            {
                                url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + (ChlListRuleConver(pagerule, savepath, id, page.ToString())) + configinfo.Suffix;
                            }
                            else if (ctype == 2)
                            {
                                url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savepath + "/" + filename + "_" + page.ToString() + configinfo.Suffix;
                            }
                        }
                        else
                        {
                            url = BaseConfigs.GetSitePath + configinfo.Htmlsavepath + savepath + "/" + (filename == "index" ? "" : (filename + configinfo.Suffix));
                        }
                    }
                }
                else
                {
                    if (configinfo.Dynamiced == 2)
                        url = BaseConfigs.GetSitePath + "/channel-" + id + (page > 1 ? ("-" + page) : "") + configinfo.Rewritesuffix;
                    else
                        url = BaseConfigs.GetSitePath + "/channel.aspx?id=" + id.ToString() + (page > 1 ? ("&page=" + page.ToString()) : "");
                }
            }

            url = (configinfo.Withweburl > 0 && !isjumpurl) ? (configinfo.Weburl + url) : url;

            if (url != null && url != "")
                Caches.AddObject(key, url);

            return url;
        }


        public static String Sitemap()
        {
            GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();
            string url = configinfo.Withweburl > 0 ? configinfo.Weburl : "";

            if (GeneralConfigs.GetConfig().Dynamiced == 0)
                return url + BaseConfigs.GetSitePath + "/sitemap" + GeneralConfigs.GetConfig().Suffix;
            else
                return url + BaseConfigs.GetSitePath + "/" + "sitemap.aspx";
        }

        public static string Channel(int id)
        {
            return Channel(id, 1);
        }

        #endregion

        public static string ChlListRuleConver(string listrule, string channelpath, int channelid, string page)
        {
            listrule = listrule.ToLower().Trim();
            if (listrule == "")
                listrule = "{@channelpath}/list_{@channelid}_{@page}";

            listrule = listrule.Replace("{@page}", page.ToString());
            listrule = listrule.Replace("{@channelid}", channelid.ToString());
            listrule = listrule.Replace("{@channelpath}", channelpath);

            return listrule;
        }

        public static string ConRuleConvert(string conrule, string channelpath, int channelid, int cId, DateTime _DateTime)
        {
            string _pathname = conrule.ToLower().Trim();

            if (_pathname == "")
                _pathname = "/{@year02}/{@month}{@day}/{@contentid}";

            _pathname = Globals.StrRandomConvert(_pathname, _DateTime);

            _pathname = _pathname.Replace("{@contentid}", cId.ToString());
            _pathname = _pathname.Replace("{@channelid}", channelid.ToString());
            _pathname = _pathname.Replace("{@channelpath}", channelpath);

            return _pathname;
        }
    }
}
