using System;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;

using STA.Entity;
using STA.Common;
using STA.Config;
using STA.Core.ScheduledEvents;
using System.Threading;
using System.Collections.Generic;

namespace STA.Core
{
    public class HttpModule : IHttpModule
    {
        static Timer eventTimer;

        /// <summary>
        /// List of Headers to remove
        /// </summary>
        private readonly List<string> _headersToCloak;

        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;


            if (eventTimer == null && ScheduleConfigs.GetConfig().Enabled)
            {
                EventLogs.LogFileName = Utils.GetMapPath(string.Format("{0}/sta/logs/scheduleeventfaildlog.config", BaseConfigs.GetSitePath));
                EventManager.RootPath = Utils.GetMapPath(BaseConfigs.GetSitePath + "/");
                eventTimer = new Timer(new TimerCallback(ScheduledEventWorkCallback), context.Context, 60000, EventManager.TimerMinutesInterval * 60000);
            }

            context.Error += new EventHandler(Application_OnError);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HideServerHeaderModule"/> class.
        /// </summary>
        public HttpModule()
        {
            _headersToCloak = new List<string>
                                      {
                                              "Server",
                                              "X-AspNet-Version",
                                              "X-AspNetMvc-Version",
                                              "X-Powered-By"
                                      };
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            //设置Server的值

            _headersToCloak.ForEach(h => HttpContext.Current.Response.Headers.Remove(h));
            HttpContext.Current.Response.Headers.Set("Server", "nginx");
            HttpContext.Current.Response.Headers.Set("Powered-By", "Potato");
        }


        private void ScheduledEventWorkCallback(object sender)
        {
            try
            {
                if (ScheduleConfigs.GetConfig().Enabled)
                {
                    EventManager.Execute();
                }
            }
            catch
            {
                EventLogs.WriteFailedLog("计划任务执行失败！");
            }

        }

        public void Application_OnError(Object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            if (context.Server.GetLastError().GetBaseException() is STAException)
            {
                STAException ex = (STAException)context.Server.GetLastError().GetBaseException();
                context.Response.Write("<html><body style=\"font-size:14px;\">");
                context.Response.Write("System Error:<br />");
                context.Response.Write("<textarea name=\"errormessage\" style=\"width:100%; height:100%; word-break:break-all\">");
                context.Response.Write(System.Web.HttpUtility.HtmlEncode(context.Server.GetLastError().ToString()));
                context.Response.Write("</textarea>");
                context.Response.Write("</body></html>");
                context.Response.End();
            }
        }

        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public void Dispose()
        {
            _headersToCloak.Clear();
        }

        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            HttpRequest request = context.Request;
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            BaseConfigInfo baseconfig = BaseConfigs.GetBaseConfig();

            if (baseconfig == null || config == null)
                return;

            string requestpath = request.Path.ToLower();
            string urlquery = request.Url.Query;

            if (config.Closed == 1
                && !requestpath.StartsWith(BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath)
                && !requestpath.StartsWith(BaseConfigs.GetSitePath + "/sta/"))
            {
                context.Response.Write(config.Closedreason);
                context.Response.End();
            }

            if (requestpath.IndexOf(".aspx") > 0
                && !requestpath.StartsWith(baseconfig.Sitepath + baseconfig.Adminpath)
                && !requestpath.StartsWith(baseconfig.Sitepath + "/sta/"))
            {
                string aspxPath = baseconfig.Sitepath + "/sta/aspx/";
                string templatename = config.Templatename;
                string aspxpage = string.Empty;

                if (requestpath.Substring(baseconfig.Sitepath.Length).LastIndexOf("/") == 0)
                {
                    string urlpattern = "\\/(" + Caches.GetContypeUrlList("|", GeneralConfigs.GetConfig().Cacheinterval * 60) + "|page|specgroup|channel)\\.aspx\\?(id=([0-9]+)(&\\w*=\\w*)*)";
                    if (Regex.IsMatch(requestpath + urlquery, urlpattern, RegexOptions.IgnoreCase))
                    {
                        Match m = Regex.Match(requestpath + urlquery, urlpattern, RegexOptions.IgnoreCase);
                        int id = TypeParse.StrToInt(m.Groups[3].Value, 0);
                        string name = m.Groups[1].Value;
                        string query = m.Groups[2].Value;
                        aspxpage = TemplateName(name, id);

                        context.RewritePath(aspxPath + templatename + "/" + aspxpage + ".aspx", "", query);
                        return;
                    }
                    else
                    {
                        //if (config.Dynamiced == 2)
                        //{
                        foreach (SiteUrls.URLRewrite url in SiteUrls.GetSiteUrls().Urls)
                        {
                            if (Regex.IsMatch(requestpath, url.Pattern, Utils.GetRegexCompiledOptions() | RegexOptions.IgnoreCase))
                            {
                                Match m = Regex.Match(requestpath, url.Pattern, Utils.GetRegexCompiledOptions() | RegexOptions.IgnoreCase);

                                int id = TypeParse.StrToInt(m.Groups[1].Value, 0);
                                aspxpage = TemplateName(url.Name, id);
                                aspxpage = aspxpage == "" ? url.Page : (aspxpage + ".aspx");

                                string newUrl = Regex.Replace(requestpath.Substring(context.Request.Path.LastIndexOf("/")), url.Pattern, url.QueryString, Utils.GetRegexCompiledOptions() | RegexOptions.IgnoreCase);
                                newUrl = urlquery.IndexOf("?") == 0 ? (newUrl + "&" + urlquery.Substring(1)) : newUrl;
                                context.RewritePath(aspxPath + templatename + "/" + aspxpage, "", newUrl);
                                return;
                            }
                        }
                    }
                    context.RewritePath(aspxPath + templatename + (baseconfig.Sitepath == "" ? requestpath : requestpath.Replace(baseconfig.Sitepath, "")), string.Empty, Utils.GetSubString(urlquery, 1, 0));
                    //}
                }
                else
                {
                    if (requestpath.Substring(baseconfig.Sitepath.Length).StartsWith("/plus/"))
                        templatename = "default";

                    context.RewritePath(aspxPath + templatename + (baseconfig.Sitepath == "" ? requestpath : requestpath.Replace(baseconfig.Sitepath, "")), string.Empty, Utils.GetSubString(urlquery, 1, 0));
                }

            }
        }

        public string TemplateName(string name, int id)
        {
            string tname = "";
            if (Utils.InArray(name, Caches.GetContypeUrlList(",", GeneralConfigs.GetConfig().Cacheinterval * 60)))
            {
                tname = Templates.ContentTemplate(id);
            }
            else if (name == "specgroup")
            {
                tname = Templates.SpecGroupTemplate(id);
            }
            else if (name == "channel")
            {
                tname = Templates.ChannelTemplate(id);
            }
            else if (name == "page")
            {
                tname = Templates.PageTemplate(id);
            }

            return tname;

        }
    }

    #region 站点伪Url信息类
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        #region 内部属性和方法
        private static object lockHelper = new object();
        private static volatile SiteUrls instance = null;

        string SiteUrlsFile = HttpContext.Current.Server.MapPath(BaseConfigs.GetSitePath + "/sta/config/urls.config");
        private System.Collections.ArrayList _Urls;
        public System.Collections.ArrayList Urls
        {
            get
            {
                return _Urls;
            }
            set
            {
                _Urls = value;
            }
        }

        private SiteUrls()
        {
            Urls = new System.Collections.ArrayList();

            XmlDocument xml = new XmlDocument();

            xml.Load(SiteUrlsFile);

            XmlNode root = xml.SelectSingleNode("urls");
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
                {
                    XmlAttribute name = n.Attributes["name"];
                    XmlAttribute path = n.Attributes["path"];
                    XmlAttribute page = n.Attributes["page"];
                    XmlAttribute querystring = n.Attributes["querystring"];
                    XmlAttribute pattern = n.Attributes["pattern"];

                    if (name != null && path != null && page != null && querystring != null && pattern != null)
                    {
                        if (name.Value == "content")
                        {
                            string[] contypes = Caches.GetContypeUrlList("|", GeneralConfigs.GetConfig().Cacheinterval * 60).Split('|');
                            foreach (string ct in contypes)
                            {
                                Urls.Add(new URLRewrite(ct, pattern.Value.Replace("content", ct), page.Value, querystring.Value.Replace("^", "&")));
                            }
                        }
                        else
                        {
                            Urls.Add(new URLRewrite(name.Value, pattern.Value, page.Value.Replace("^", "&"), querystring.Value.Replace("^", "&")));
                        }
                    }
                }
            }
        }
        #endregion

        public static SiteUrls GetSiteUrls()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new SiteUrls();
                    }
                }
            }
            return instance;

        }

        public static void SetInstance(SiteUrls anInstance)
        {
            if (anInstance != null)
                instance = anInstance;
        }

        public static void SetInstance()
        {
            SetInstance(new SiteUrls());
        }


        /// <summary>
        /// 重写伪地址
        /// </summary>
        public class URLRewrite
        {
            #region 成员变量
            private string _Name;
            public string Name
            {
                get
                {
                    return _Name;
                }
                set
                {
                    _Name = value;
                }
            }

            private string _Pattern;
            public string Pattern
            {
                get
                {
                    return _Pattern;
                }
                set
                {
                    _Pattern = value;
                }
            }

            private string _Page;
            public string Page
            {
                get
                {
                    return _Page;
                }
                set
                {
                    _Page = value;
                }
            }

            private string _QueryString;
            public string QueryString
            {
                get
                {
                    return _QueryString;
                }
                set
                {
                    _QueryString = value;
                }
            }
            #endregion

            #region 构造函数
            public URLRewrite(string name, string pattern, string page, string querystring)
            {
                _Name = name;
                _Pattern = pattern;
                _Page = page;
                _QueryString = querystring;
            }
            #endregion
        }

    }
    #endregion
}


