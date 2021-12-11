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

namespace STA.Core.Publish
{
    public class StaticCreate
    {
        private static string fullHost = String.Format("http://{0}", STARequest.GetCurrentFullHost());
        private static string contenturl = "content.aspx?id={0}&page={1}";
        private static string specialurl = "special.aspx?id={0}";
        private static string specialgroupurl = "specgroup.aspx?id={0}&group={1}";
        private static string channelurl = "channel.aspx?id={0}&page={1}";
        private static string pageurl = "page.aspx?id={0}";
        private static string rssurl = "tools/rss.aspx?chl={0}&count={1}";


        public static bool CreateContent(GeneralConfigInfo config, int id, int page, string path, string filename)
        {
            if (config.Dynamiced >= 1) return false;


            try
            {
                //if (page == 1)
                //    Contents.ConHtmlStatus(id, 1);

                filename = filename.Trim() == "" ? id.ToString() : filename.Trim();
                Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + config.Htmlsavepath + path.Trim() + "/"),
                                     string.Format(fullHost + BaseConfigs.GetSitePath + "/" + contenturl, id, page),
                                     (page == 1 ? filename : filename + "_" + page) + config.Suffix);
                return true;
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成文档静态出错！", ex);
                return false;
            }
        }

        public static bool CreatePage(GeneralConfigInfo config, int id, int page, string path, string filename)
        {
            if (config.Dynamiced >= 1) return false;

            try
            {
                filename = filename.Trim() == "" ? id.ToString() : filename.Trim();
                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + config.Htmlsavepath + path.Trim() + "/"),
                                            string.Format(fullHost + BaseConfigs.GetSitePath + "/" + pageurl, id),
                                            (page == 1 ? filename : filename + "_" + page) + config.Suffix);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成单页静态出错！", ex);
                return false;
            }
        }

        public static bool CreateIndex(GeneralConfigInfo config)
        {
            if (config.Dynamiced >= 1) return false;

            try
            {
                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + "/"),
                                            fullHost + BaseConfigs.GetSitePath + "/index.aspx",
                                           "index" + config.Suffix);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成首页静态出错！", ex);
                return false;
            }
        }

        public static bool CreateSiteMap(GeneralConfigInfo config)
        {
            if (config.Dynamiced >= 1) return false;

            try
            {
                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + "/"),
                                            fullHost + BaseConfigs.GetSitePath + "/sitemap.aspx",
                                           "sitemap" + config.Suffix);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成站点地图静态出错！", ex);
                return false;
            }
        }

        public static bool CreateSpecial(GeneralConfigInfo config, int id, string path, string filename)
        {
            if (config.Dynamiced >= 1) return false;

            try
            {
                //Contents.ConHtmlStatus(id, 1);
                filename = filename.Trim() == "" ? id.ToString() : filename.Trim();
                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + config.Htmlsavepath + path.Trim() + "/"),
                                            string.Format(fullHost + BaseConfigs.GetSitePath + "/" + specialurl, id),
                                            filename + config.Suffix);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成专题静态出错！", ex);
                return false;
            }
        }

        public static bool CreateSpecialGroup(GeneralConfigInfo config, int id, int group, string path, string filename)
        {
            if (config.Dynamiced >= 1) return false;

            try
            {
                filename = filename.Trim() == "" ? id.ToString() : filename.Trim();
                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + config.Htmlsavepath + path.Trim() + "/"),
                                            string.Format(fullHost + BaseConfigs.GetSitePath + "/" + specialgroupurl, id, group),
                                            filename + "_list_" + group.ToString() + config.Suffix);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成专题静态出错！", ex);
                return false;
            }
        }

        public static bool CreateChannel(GeneralConfigInfo config, int id, int ctype, int page, string path, string filename, string pagerule)
        {
            if (config.Dynamiced >= 1) return false;

            try
            {
                if (page > 1)
                {
                    if (ctype == 1)
                    {
                        string savename = Urls.ChlListRuleConver(pagerule, path, id, page.ToString());
                        path = savename.Substring(0, savename.LastIndexOf('/'));
                        filename = savename.Substring(savename.LastIndexOf('/') + 1);
                    }
                    else if (ctype == 2)
                    {
                        filename = filename + "_" + page;
                    }

                }
                filename = filename.Trim() == "" ? id.ToString() : filename.Trim();

                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + config.Htmlsavepath + path + "/"),
                                             string.Format(fullHost + BaseConfigs.GetSitePath + "/" + channelurl, id, page),
                                             filename + config.Suffix);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成频道静态出错！", ex);
                return false;
            }
        }

        public static bool CreateRss(GeneralConfigInfo config, int chlid, int count)
        {
            try
            {
                return Utils.CreateHTMLPage(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/data/rss/"),
                                             string.Format(fullHost + BaseConfigs.GetSitePath + "/" + rssurl, chlid, count),
                                             chlid + ".xml");
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "生成RSS订阅文件出错！", ex);
                return false;
            }
        }

        public static bool CreateContent(int id, int mtype)
        {
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            string url;
            if (Urls.ContentJumpUrl(id, out url)) return false;

            if (ConUtils.IsDynamicedCon(config, id)) return false;

            ContentInfo info = Contents.GetContentForHtml(id);
            if (info == null) return false;

            if (mtype == 1)
            {
                int pagecount = config.Contentpage == 1 ? ConUtils.ConPageCount(info.Content) : 1; //如果关闭文档分页则页数为1页

                for (int i = 1; i <= pagecount; i++)
                {
                    bool success = CreateContent(config, id, i, info.Savepath, info.Filename);

                    if (!success)
                        return false;
                }
                return true;
            }
            else if (mtype == 2)
            {
                return CreateSpecial(config, id, info.Savepath, info.Filename);
            }
            else if (mtype == 3)
            {
                DataTable spgroupdt = Contents.GetSpecgroups(id);
                foreach (DataRow idr in spgroupdt.Rows)
                {
                    StaticCreate.CreateSpecialGroup(GeneralConfigs.GetConfig(), id, TypeParse.StrToInt(idr["id"]), info.Savepath, info.Filename);
                }
                return CreateSpecial(GeneralConfigs.GetConfig(), id, info.Savepath, info.Filename);
            }
            return true;
        }

        public static bool CreateContent(int id)
        {
            return CreateContent(id, 1);
        }

        public static bool CreateSpecial(int id)
        {
            return CreateContent(id, 2);
        }

        public static bool CreateSpecialWithGroup(int id)
        {
            return CreateContent(id, 3);
        }

        public static bool CreatePage(int id)
        {
            if (GeneralConfigs.GetConfig().Dynamiced >= 1) return false;

            PageInfo info = Contents.GetPageForHtml(id);
            if (info == null) return false;

            GeneralConfigInfo config = GeneralConfigs.GetConfig();

            int pagecount = config.Contentpage == 1 ? ConUtils.ConPageCount(info.Content) : 1; //如果关闭分页则页数为1页

            for (int i = 1; i <= pagecount; i++)
            {
                bool success = CreatePage(config, id, i, info.Savepath, info.Filename);

                if (!success)
                    return false;
            }
            return true;
        }

        public static bool CreateIndex()
        {
            if (GeneralConfigs.GetConfig().Dynamiced >= 1) return false;

            return CreateIndex(GeneralConfigs.GetConfig());
        }

        public static bool CreateSiteMap()
        {
            if (GeneralConfigs.GetConfig().Dynamiced >= 1) return false;

            return CreateSiteMap(GeneralConfigs.GetConfig());
        }

        public static bool CreateChannel(int id, int page)
        {
            if (ConUtils.IsDynamicedChl(GeneralConfigs.GetConfig(), id)) return false;

            ChannelInfo info = Contents.GetChannelForHtml(id);
            if (info == null) return false;

            return CreateChannel(GeneralConfigs.GetConfig(), id, info.Ctype, page, info.Savepath, info.Filename, info.Listrule);
        }
    }
}
