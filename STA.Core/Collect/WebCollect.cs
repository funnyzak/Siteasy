using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Data;
using STA.Cache;
using STA.Config;

namespace STA.Core.Collect
{
    public class WebCollect
    {
        public static int successcount = 0;
        public static int failcount = 0;
        public static int totalcount = 0;
        public static Hashtable urls = new Hashtable();
        public static Hashtable setting = new Hashtable();
        public static WebcollectInfo webinfo;
        public static DataTable conalldt = new DataTable();

        public static void CollectPre(WebcollectInfo info, int collectcount)
        {
            Reset();
            if (info == null) return;
            webinfo = info;
            setting = ConUtils.GetCollectRuleSet(WebcollectInfo.ats, info.Setting);
            FigureUrlList();
            if (collectcount == 0)
                totalcount = urls.Count;
            else
                totalcount = urls.Count > collectcount ? collectcount : urls.Count;
        }

        public static void Collect()
        {
            if (totalcount <= 0) return;
            short typeid = 1;
            ChannelInfo chlinfo;
            ContypeInfo tpeinfo;
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            if (webinfo.Channelid > 0)
            {
                chlinfo = Contents.GetChannel(webinfo.Channelid);
                if (chlinfo != null)
                {
                    typeid = chlinfo.Typeid;
                }
            }

            tpeinfo = Contents.GetContype(typeid);
            string typename = tpeinfo == null ? "" : tpeinfo.Name;
            string channelfamily = "";
            if (webinfo.Channelid > 0)
                channelfamily = ConUtils.GetChannelFamily(webinfo.Channelid, ",");

            conalldt = new DataTable();
            if (webinfo.Property.IndexOf("r") >= 0)
                conalldt = Contents.GetContentTableByWhere(10000000, "title", "");

            int j = webinfo.Property.IndexOf("d") >= 0 ? (urls.Count + 1) : 0; //如果反序采集


            for (int i = 0; i < totalcount; i++)
            {
                string url = urls[webinfo.Property.IndexOf("d") >= 0 ? (--j) : ++j].ToString();
                if (url == "")
                {
                    failcount++;
                    continue;
                }
                try
                {
                    string pagecontent = Fetch(url, webinfo.Encode);
                    if (pagecontent == "")
                    {
                        failcount++;
                        continue;
                    }

                    //检查标题重复
                    string title = GetTarget(pagecontent, setting["title"].ToString(), "[匹配内容]").Trim();
                    if (title == "" //标题为空
                        || (webinfo.Property.IndexOf("r") >= 0 && conalldt.Select("title='" + title.Replace("'", "''") + "'").Length > 0)) //如果标题重复
                    {
                        failcount++;
                        continue;
                    }
                    if (webinfo.Property.IndexOf("r") >= 0)
                    {
                        DataRow rdr = conalldt.NewRow();
                        rdr["title"] = title;
                        conalldt.Rows.Add(rdr);
                    }


                    string content = GetTarget(pagecontent, setting["content"].ToString(), "[匹配内容]");
                    if (webinfo.Property.IndexOf("c") >= 0 && content == "") //如果内容为空
                    {
                        failcount++;
                        continue;
                    }

                    if (setting["conpage"].ToString().IndexOf("[匹配内容]") >= 0 && setting["conpageurl"].ToString().IndexOf("[匹配内容]") >= 0)
                    {
                        if (webinfo.Property.IndexOf("p") >= 0 && GetTarget(pagecontent, setting["conpage"].ToString(), "[匹配内容]") != "")
                        {
                            failcount++;
                            continue;
                        }
                        else
                        {
                            content = GetPageContent(pagecontent, content);
                        }
                    }

                    content = SetImgUrl(content);
                    if (webinfo.Property.IndexOf("m") >= 0)
                    {
                        content = RemoteFile.Remote(content, new string[] { "gif", "jpg", "bmp", "ico", "png", "jpeg" },
                                          BaseConfigs.GetSitePath + config.Attachsavepath + "/remote/", config.Colfilestorage == 1);
                    }

                    ContentInfo info = new ContentInfo();
                    info.Id = TypeParse.StrToInt(Rand.Number(7));
                    info.Typeid = typeid;
                    info.Typename = typename;
                    info.Channelid = webinfo.Channelid;
                    info.Channelname = webinfo.Channelname;
                    info.Channelfamily = channelfamily;
                    info.Click = CollectUtils.ConClick(config);
                    info.Title = CollectUtils.TitFormat(config, title);

                    info.Addusername = "信息采集";
                    info.Status = webinfo.Constatus;
                    info.Addtime = TypeParse.StrToDateTime(GetMatchValue(pagecontent, "addtime"));
                    info.Source = GetMatchValue(pagecontent, "source");
                    info.Author = GetMatchValue(pagecontent, "author");
                    if (webinfo.Property.IndexOf("a") >= 0)
                    {
                        string[] imgs = Utils.GetContentImgList(content);
                        info.Img = imgs.Length > 0 ? imgs[0] : "";
                    }
                    info = ConUtils.EditContentPath(info);
                    content = TagFilter(content);
                    content = CustomFilter(content);
                    //if (webinfo.Filter.IndexOf("space") >= 0)
                    //    content = Utils.CompressHtml(content);
                    info.Content = CollectUtils.ConFormat(config, content);
                    info.Ext = new System.Collections.Hashtable();
                    if (Contents.AddContent(info) > 0)
                        successcount++;
                    else
                        failcount++;
                }
                catch (Exception ex)
                {
                    failcount++;
                    STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "站点采集", ex);
                }
            }
            conalldt.Dispose();
        }

        private static string GetMatchValue(string pagecontent, string attr)
        {
            string attrvalue = setting[attr].ToString().Trim();
            if (attrvalue.IndexOf("[匹配内容]") >= 0)
                attrvalue = GetTarget(pagecontent, attrvalue, "[匹配内容]");
            return attrvalue;
        }

        /// <summary>
        /// 标签过滤
        /// </summary>
        private static string TagFilter(string content)
        {
            foreach (string tag in webinfo.Filter.Split(','))
            {
                if (tag.Trim() == "") continue;
                if (Utils.InArray(tag, "iframe,script,object"))
                    content = Utils.RemoveHtmlTag(content, tag);
                else
                    content = Utils.FilterHtmlTag(content, tag);
            }
            return content;
        }

        /// <summary>
        /// 自定义内容过滤
        /// </summary>
        private static string CustomFilter(string content)
        {
            foreach (DataRow dr in ConUtils.GetContentFilterList(webinfo.Confilter).Rows)
            {
                try
                {
                    content = Regex.Replace(content, dr["match"].ToString(), dr["replace"].ToString(), RegexOptions.IgnoreCase);
                }
                catch (Exception ex)
                {
                    STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "站点采集：内容自定义过滤出错,正则：" + dr["match"].ToString(), ex);
                }
            }
            return content;
        }

        /// <summary>
        /// 设置内容里绝对路径的图片为全路径(带域名）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string SetImgUrl(string content)
        {
            if (webinfo.Hosturl.Trim() == "") return content;

            Regex r = new Regex("src=[\"'][^(http:)][\\/]?[^\\s]+[\"']", RegexOptions.IgnoreCase);
            Match m = r.Match(content);
            while (m.Success)
            {
                content = content.Replace(m.Value, m.Value.Replace("src=\"", "src=\"" + webinfo.Hosturl));
                m = m.NextMatch();
            }
            return content;
        }


        /// <summary>
        /// 获取内容分页内容
        /// </summary>
        /// <param name="pagecontent">页面内容</param>
        /// <param name="content">默认内容</param>
        /// <returns></returns>
        private static string GetPageContent(string pagecontent, string content)
        {
            string conpagecontent = GetTarget(pagecontent, setting["conpage"].ToString(), "[匹配内容]");
            if (conpagecontent.Trim() == "") return content;

            List<string> pages = GetTargetList(conpagecontent, setting["conpageurl"].ToString(), "[匹配内容]");
            if (pages.Count <= 0) return content;

            string rcontent = content;
            string host = string.Empty;
            if (pages[0].IndexOf("http://") == -1 && webinfo.Hosturl != "")
                host = webinfo.Hosturl + ((pages[0].IndexOf("/") > 0 || pages[0].IndexOf("/") == -1) ? "/" : "");

            Hashtable links = new Hashtable(pages.Count);
            foreach (string s in pages)
            {
                if (links.Contains(s)) continue;
                rcontent += "[STA:PAGE]\r\n" + GetTarget(Fetch((host + s), webinfo.Encode), setting["content"].ToString(), "[匹配内容]");
                links.Add(s, s);
            }
            return rcontent;
        }

        /// <summary>
        /// 获取所有内容链接
        /// </summary>
        private static void FigureUrlList()
        {
            if (setting["urllist"].ToString().IndexOf("[匹配内容]") < 0) return;

            int loop = 1;
            Match m = GetMatch(GetUrlList(), setting["url"].ToString(), "[匹配内容]");
            while (m.Success)
            {
                string url = m.Groups["TARGET"].Value.Replace("../", "/");
                if (url.IndexOf("/") == 0)
                    url = webinfo.Hosturl + url;

                urls.Add(loop, url);
                m = m.NextMatch();
                loop++;
            }
        }

        private static string GetUrlList()
        {
            if (webinfo.Collecttype == CollectType.单页)
                return FigureSingleContent();
            else if (webinfo.Collecttype == CollectType.索引分页)
                return FigurePageContent(TypeParse.StrToInt(webinfo.Clistpage.Split(',')[0]), "");
            else
                return FigureUrlsContent();
        }


        private static string FigureSingleContent()
        {
            return GetTarget(Fetch(webinfo.Curl, webinfo.Encode), setting["urllist"].ToString(), "[匹配内容]");
        }

        private static string FigurePageContent(int pageindex, string lc)
        {
            string url = webinfo.Clisturl.Replace("@page", pageindex.ToString());
            lc += GetTarget(Fetch(url, webinfo.Encode), setting["urllist"].ToString(), "[匹配内容]");
            if ((pageindex + 1) > TypeParse.StrToInt(webinfo.Clistpage.Split(',')[1])) return lc;
            return FigurePageContent(pageindex + 1, lc);
        }

        private static string FigureUrlsContent()
        {
            string ret = "";
            foreach (string url in Utils.SplitString(webinfo.Curls, "\r\n"))
            {
                ret += GetTarget(Fetch(url, webinfo.Encode), setting["urllist"].ToString(), "[匹配内容]");
            }
            return ret;
        }

        private static string GetTarget(string input, string pattern, string find)
        {
            string targets = string.Empty;
            Match m = GetMatch(input, pattern, find);
            while (m.Success)
            {
                targets += m.Groups["TARGET"].Value;
                m = m.NextMatch();
            }
            return targets;
        }

        private static List<string> GetTargetList(string input, string pattern, string find)
        {
            List<string> targets = new List<string>();
            Match m = GetMatch(input, pattern, find);
            while (m.Success)
            {
                targets.Add(m.Groups["TARGET"].Value);
                m = m.NextMatch();
            }
            return targets;
        }

        public static string Fetch(string Url, string encode)
        {
            if (!Utils.IsURL(Url)) return "";
            try
            {
                return Utils.GetPageContent(new Uri(Url), encode);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "站点采集", ex);
                return "";
            }
        }


        /// <summary>
        /// 获取一个目标的匹配结果
        /// </summary>
        /// <param name="input">要匹配的字符串</param>
        /// <param name="pattern"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static Match GetMatch(string input, string pattern, string find)
        {
            string _pattn = Regex.Escape(pattern);
            _pattn = _pattn.Replace(@"\[变量]", @"[\s\S]*?");
            _pattn = Regex.Replace(_pattn, @"((\\r\\n)|(\\ ))+", @"\s*", RegexOptions.Compiled);
            if (Regex.Match(pattern.TrimEnd(), Regex.Escape(find) + "$", RegexOptions.Compiled).Success)
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+)");
            else
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+?)");
            Regex r = new Regex(_pattn, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = r.Match(input);
            return m;
        }

        public static void Reset()
        {
            urls.Clear();
            setting.Clear();
            successcount = 0;
            failcount = 0;
            totalcount = 0;
            conalldt.Dispose();
        }
    }
}
