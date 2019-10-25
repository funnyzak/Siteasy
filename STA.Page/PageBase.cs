using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

using STA.Data;
using STA.Core;
using STA.Common;
using STA.Config;
using STA.Entity;
using System.Collections;
using System.Web;
using STA.Entity.Common;

namespace STA.Page
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 页标题
        /// </summary>
        protected internal string seotitle = string.Empty;
        /// <summary>
        /// 页关键字
        /// </summary>
        protected internal string seokeywords = string.Empty;
        /// <summary>
        /// 页描述
        /// </summary>
        protected internal string seodescription = string.Empty;
        /// <summary>
        /// 当前用户的用户ID
        /// </summary>
        protected internal int userid;

        protected internal int usergroupid;

        protected internal int useradminid;
        /// <summary>
        /// 当前用户的用户名
        /// </summary>
        protected internal string username;
        /// <summary>
        /// 当前用户的密码
        /// </summary>
        protected internal string password;

        protected internal string userip;
        /// <summary>
        /// 当前日期
        /// </summary>
        protected internal string nowdate;
        /// <summary>
        /// 当前时间
        /// </summary>
        protected internal string nowtime;
        /// <summary>
        /// 当前日期时间
        /// </summary>
        protected internal string nowdatetime;
        /// <summary>
        /// 当前页面名称
        /// </summary>
        public string pagename = STARequest.GetPageName();
        /// <summary>
        /// 全局配置文件
        /// </summary>
        protected internal GeneralConfigInfo config;

        protected internal OnlineUserInfo oluser;

        protected internal BaseConfigInfo baseconfig;
        /// <summary>
        /// meta标签
        /// </summary>
        protected internal string meta = string.Empty;

        protected internal string link = string.Empty;
        /// <summary>
        /// 脚本
        /// </summary>
        protected internal string script = string.Empty;

        /// <summary>
        /// 搜索关键字
        /// </summary>
        //public List<string> searchKeywords;

        protected internal bool ispost;
        /// <summary>
        /// 是否压缩输出
        /// </summary>
        protected internal bool iscompress;

        protected internal string location = string.Empty;
        protected internal string location_format = "{0}<a href=\"{1}\" title=\"{2}\" target=\"_self\">{2}</a>";

        protected internal string weburl = string.Empty;

        protected internal string siteurl = string.Empty;

        protected internal string sitedir = string.Empty;

        protected internal string tempurl = string.Empty;

        protected internal string tempdir = string.Empty;

        protected internal string webname = string.Empty;

        /// <summary>
        /// 当前页面检查到的错误数
        /// </summary>
        protected internal int errors = 0;
        /// <summary>
        /// 提示文字
        /// </summary>
        protected internal string msgtext = "";

        protected internal string cururl = string.Empty;

        protected internal string curlang = "zh-CN";
        /// <summary>
        /// 查询次数统计
        /// </summary>
        public int querycount = 0;

#if DEBUG
        public string querydetail = "";
#endif
        protected internal System.Text.StringBuilder templateBuilder = new System.Text.StringBuilder();
        /// <summary>
        /// PageBese构造函数
        /// </summary>
        public PageBase()
        {
            config = GeneralConfigs.GetConfig();
            baseconfig = BaseConfigs.GetBaseConfig();
            userip = STARequest.GetIP();

            //#if DEBUG
            //            DbHelper.QueryCount = 0;
            //            DbHelper.QueryDetail = "";
            //#endif

            weburl = config.Weburl;
            siteurl = weburl + baseconfig.Sitepath;
            sitedir = baseconfig.Sitepath;

            tempdir = baseconfig.Sitepath + "/templates/" + config.Templatename;
            tempurl = weburl + tempdir;
            cururl = STARequest.GetRawUrl();

            seotitle = config.Webtitle;
            seodescription = config.Description;
            seokeywords = config.Keywords;
            webname = config.Webname;

            nowdate = Utils.GetDate();
            nowtime = Utils.GetTime();
            nowdatetime = Utils.GetDateTime();
            iscompress = config.Htmlcompress == 1;
            ispost = STARequest.IsPost();

            curlang = STARequest.GetQueryString("lang");
            curlang = curlang == "" ? config.Weblang : curlang;

            //else if (HttpContext.Current.Request.Cookies["lang"] != null && HttpContext.Current.Request.Cookies["lang"].Value != "")
            //{
            //    curlang = HttpContext.Current.Request.Cookies["lang"].Value;
            //}

            //校验用户是否可以访问网站
            if (!ValidateUserPermission())
                return;

            oluser = ConUtils.GetOnlineUser();
            userid = oluser.Userid;
            username = oluser.Username;
            useradminid = oluser.Adminid;
            usergroupid = oluser.Groupid;
            //password = oluser.Password;

            if (userid < 0)
                oluser = UserUtils.UpdateOnlineUserInfo(null, 0);

            location = string.Format(location_format, "", weburl + sitedir, config.Indexlinkname);
            PageShow();

            querycount = DbHelper.QueryCount;
            DbHelper.QueryCount = 0;

#if DEBUG

            querydetail = DbHelper.QueryDetail;
            DbHelper.QueryDetail = "";
#endif

        }

        /// <summary>
        /// 校验用户是否可以访问网站
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserPermission()
        {

            // 如果IP访问列表有设置则进行判断
            if (config.Ipaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipaccess, "\n");
                if (!Utils.InIPArray(userip, regctrl))
                {
                    PageInfo("您无法访问本网站", "#", 2);
                    return false;
                }
            }


            // 如果IP访问列表有设置则进行判断
            if (config.Ipdenyaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipdenyaccess, "\n");
                if (Utils.InIPArray(userip, regctrl))
                {
                    PageInfo("您被禁止访问本网站", "#", 2);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// OnUnload事件处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {

#if DEBUG

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write(templateBuilder.Replace("</body>", "<div>注意: 以下为数据查询分析工具。</div>" + querydetail + "</body>").ToString());
            System.Web.HttpContext.Current.Response.End();
#endif
            base.OnUnload(e);
        }

        /// <summary>
        /// 控件初始化时计算执行时间
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }


        protected string Variable(string key)
        {
            return Caches.GetVariable(key, config.Cacheinterval * 60);
        }

        protected DataTable GetTable(string action, string parms)
        {
            return UIDataProvide.GetUITable(action, parms);
        }

        protected DataTable GetDbTable(string table, string parms)
        {
            return UIDataProvide.GetDbTable(table, parms);
        }

        protected DataTable GetSqlTable(string sql)
        {
            try
            {
                return DbHelper.ExecuteDataset(sql.Replace("tbprefix_", baseconfig.Tableprefix)).Tables[0];
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "数据调用：Sql查询语法错误！SQL: " + sql, ex);
                return new DataTable();
            }
        }

        protected virtual void PageShow()
        {
            return;
        }

        /// <summary>
        /// 格式化文档标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="property"></param>
        /// <param name="color">颜色代码</param>
        /// <param name="len"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        protected string TitleFormat(string title, string property, string color, int len, string tail)
        {
            color = color.Trim() == "" ? "000" : color;
            return Utils.GetColorTitle(title, "#" + color, property.IndexOf(",b,") >= 0, property.IndexOf(",i,") >= 0, len, tail);
        }


        public static ContentInfo SimpleContent(int id)
        {
            return ConUtils.GetSimpleContent(id);
        }

        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="imgs"></param>
        /// <returns></returns>
        protected DataTable PhotoList(string imgs)
        {
            return ConUtils.GetPhotoList(imgs);
        }

        /// <summary>
        /// 生成页面导航
        /// </summary>
        /// <param name="defUrl">第一地址,可为空</param>
        /// <param name="urlFormat">地址格式如：list.aspx?page=@page</param>
        /// <param name="pageIndex">当前地址</param>
        /// <param name="pageCount">页数</param>
        /// <param name="recordCount">记录总数,可为空</param>
        /// <param name="pageNCount">生成也导航链接数量</param>
        /// <param name="isSelect">是否显示select跳转</param>
        /// <returns></returns>
        public static string PageNumber(String defUrl, String urlFormat, int pageIndex, int pageCount, int recordCount, int pageNCount, bool isSelect)
        {
            if (pageCount > 0)
                return Utils.GetDynamicPageNumber(defUrl, urlFormat.Replace("@page", "{0}"), pageIndex, pageCount, recordCount, pageNCount, isSelect).ToString();
            else
                return Utils.GetRandomPageNumber(urlFormat.Replace("@page", "{0}"), pageIndex, pageNCount).ToString();
        }

        public static string PageNumber(String urlFormat, int pageNCount)
        {
            return PageNumber("", urlFormat.Replace("@page", "{0}"), STARequest.GetInt("page", 1), 0, 0, pageNCount, false).ToString();
        }

        public static string PageNumber(String urlFormat, int pageIndex, int pageNCount)
        {
            return PageNumber("", urlFormat.Replace("@page", "{0}"), pageIndex, 0, 0, pageNCount, false).ToString();
        }

        public static string PageNumber(String urlFormat)
        {
            return PageNumber("", urlFormat.Replace("@page", "{0}"), STARequest.GetInt("page", 1), 0, 0, 7, false).ToString();
        }

        public static string PageNumber()
        {
            string url = STARequest.GetRawUrl();
            if (url.IndexOf("?") > 0)
                url = url.Substring(0, url.IndexOf("?"));
            return PageNumber("", url + "?page=@page", STARequest.GetInt("page", 1), 0, 0, 7, false).ToString();
        }

        /// <summary>
        /// 软件下载列表
        /// </summary>
        /// <param name="softs"></param>
        /// <returns></returns>
        protected DataTable SoftList(string softs)
        {
            return ConUtils.GetSoftList(softs);
        }

        protected DataTable MagazineList(string cons)
        {
            return ConUtils.GetMagazineList(cons);
        }

        protected string GetDownloadUrl(string durl, int cid)
        {
            if (durl.IndexOf(sitedir + "/attachment.aspx") >= 0)
            {
                return string.Format("{0}&conid={1}", durl, cid);
            }
            else
            {
                return string.Format("{0}/attachment.aspx?conid={1}&downurl={2}", siteurl, cid, Utils.UrlEncode(durl));
            }

        }

        //protected string ImgThumb(string url)
        //{
        //    string thumb = ConUtils.GetImgThumb(url, true);
        //    return (thumb.IndexOf("/") == 0 && config.Withweburl > 0) ? (weburl + thumb) : thumb;
        //}

        protected string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)
        {
            return Translators.Translate(sourceText, sourceLanguageCode, targetLanguageCode);
        }


        protected string Translate(string sourceText, string targetLanguageCode)
        {
            return Translators.Translate(sourceText, targetLanguageCode);
        }

        /// <summary>
        /// 如果扩展字段为联动字段
        /// </summary>
        /// <param name="ename">联动标识(不包括ext_)</param>
        /// <param name="value">联动值</param>
        /// <returns></returns>
        protected SelectInfo SelectByVal(string ename, string value)
        {
            #region
            SelectInfo info = new SelectInfo();
            DataTable sdt = Caches.GetSelectTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            decimal temv = TypeParse.StrToDecimal(value);
            DataRow[] drs = sdt.Select(string.Format("ename='{0}' and value='{1}'", ename, value));
            if (drs.Length == 0) return info;

            info.Value = drs[0]["value"].ToString().Trim();
            info.Name = drs[0]["name"].ToString().Trim();
            info.Id = TypeParse.StrToInt(drs[0]["id"]);
            return info;
            //if (temv % 500 == 0)
            //{
            //    return drs[0]["name"].ToString().Trim();
            //}
            //else
            //{
            //    DataRow[] top = sdt.Select(string.Format("ename='{0}' and value='{1}'", ename, TypeParse.StrToInt(value) / 500 * 500));
            //    DataRow[] son = sdt.Select(string.Format("ename='{0}' and value='{1}'", ename, Math.Floor(temv)));
            //    return string.Format("{0}{3}{1}{2}", top.Length > 0 ? top[0]["name"].ToString().Trim() : "", son.Length > 0 ? son[0]["name"].ToString().Trim() : "", temv % 1 == 0 ? "" : (split + drs[0]["name"].ToString().Trim()), split);
            //}
            #endregion
        }

        /// <summary>
        /// 获取联动的下级列表
        /// </summary>
        /// <param name="ename"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected DataTable SelectSubList(string ename, string value)
        {
            return Caches.SelectSubListByVal(ename, value, config.Cacheinterval * 60);
        }

        /// <summary>
        /// 根据联动值获取上级节点
        /// </summary>
        /// <param name="ename"></param>
        ///<param name="value"></param>
        /// <returns></returns>
        protected SelectInfo SelectParentNode(string ename, string value)
        {
            SelectInfo info = new SelectInfo();
            decimal temv = TypeParse.StrToDecimal(value.Trim());
            if (temv % 500 == 0) return info;

            DataTable sdt = Caches.GetSelectTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = null;
            if (temv % 1 == 0)
            {
                drs = sdt.Select(string.Format("ename = '{0}' and value = '{1}'", ename, TypeParse.StrToInt(value.Trim()) / 500 * 500));
            }
            else
            {
                drs = sdt.Select(string.Format("ename = '{0}' and value = '{1}'", ename, Math.Floor(temv)));
            }
            if (drs.Length <= 0) return info;
            info.Value = drs[0]["value"].ToString().Trim();
            info.Name = drs[0]["name"].ToString().Trim();
            info.Id = TypeParse.StrToInt(drs[0]["id"]);
            return info;
        }

        /// <summary>
        /// 根据标签获取相关内容
        /// </summary>
        /// <param name="id">内容ID</param>
        /// <param name="count">数量</param>
        /// <param name="fields">字段列表</param>
        /// <returns></returns>
        protected DataTable RelateConList(int id, int count, string fields)
        {
            return Contents.GetRelateConList(id, count, fields);
        }

        /// <summary>
        /// 获取投票选项
        /// </summary>
        /// <param name="id">主题ID</param>
        /// <param name="fields">字段列表</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="sort">排序方式 降序DESC,升序ASC</param>
        /// <returns></returns>
        protected DataTable VoteOptionList(int id, string fields, string orderby, string sort)
        {
            return Votes.GetVoteOptionDataTable(id, fields, orderby, sort);
        }

        /// <summary>
        /// 计算投票选项进度条宽度
        /// </summary>
        /// <param name="total">总投票数</param>
        /// <param name="maxcount">获得最大投票数的选项票数</param>
        /// <param name="count">当前投票数</param>
        /// <param name="basewidth">进度条长度参考值</param>
        /// <param name="prec">获取百分比</param>
        /// <returns>计算得到的宽度</returns>
        protected int VoteBarWid(int total, int maxcount, int count, int basewidth, out string prec)
        {
            prec = "0.0";
            if (total <= 0) return 0;
            double precent = Convert.ToDouble(count) / Convert.ToDouble(total);
            prec = (precent * 100).ToString("#0.0");
            return Convert.ToInt32(Convert.ToDouble(basewidth) / (maxcount / Convert.ToDouble(total)) * precent);
        }

        /// <summary>
        /// 获取子频道
        /// </summary>
        /// <returns></returns>
        protected DataTable SubChlList(int id)
        {
            DataTable chldt = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            if (id < 0) return chldt;

            DataTable dt = chldt.Clone();
            DataRow[] chls = chldt.Select("parentid=" + id.ToString());
            foreach (DataRow item in chls)
            {
                dt.Rows.Add(item.ItemArray);
            }
            return dt;
        }

        protected void Redirect(string url)
        {
            System.Web.HttpContext.Current.Response.Redirect(url);
        }

        protected void ResponseText(string text)
        {
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        /// <param name="sec">时间(秒)</param>
        /// <param name="url">转向地址</param>
        public void SetMetaRefresh(int sec, string url)
        {
            meta = meta + "\r\n<meta http-equiv=\"refresh\" content=\"" + sec.ToString() + "; url=" + url + "\" />";
        }

        /// <summary>
        /// 插入指定路径的CSS
        /// </summary>
        /// <param name="url">CSS路径</param>
        public void AddLinkCss(string url)
        {
            link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />";
        }

        public void AddLinkRss(string url, string title)
        {
            link = link + "\r\n<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" + title + "\" href=\"" + url + "\" />";
        }

        /// <summary>
        /// 插入指定路径的CSS
        /// </summary>
        /// <param name="url">CSS路径</param>
        public void AddLinkCss(string url, string linkid)
        {
            link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" id=\"" + linkid + "\" />";
        }

        /// <summary>
        /// 添加页面Meta信息
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void AddMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            if (Seokeywords != "")
            {
                meta = meta + "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
            }
            if (Seodescription != "")
            {
                meta = meta + "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
            }
            meta = meta + (Seohead.Trim() == "" ? "" : ("\r\n" + Seohead));
        }

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script></script>的脚本主体字符串</param>
        public void AddScript(string scriptstr)
        {
            AddScript(scriptstr, "javascript");
        }

        public void AddLinkScript(string url)
        {
            script = script + "\r\n<script type=\"text/javascript\" src=\"" + url + "\"></script>";
        }

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script>
        /// <param name="scripttype">脚本类型(值为：vbscript或javascript,默认为javascript)</param>
        public void AddScript(string scriptstr, string scripttype)
        {
            if (!scripttype.ToLower().Equals("vbscript") && !scripttype.ToLower().Equals("vbscript"))
            {
                scripttype = "javascript";
            }
            script = script + "\r\n<script type=\"text/" + scripttype + "\">" + scriptstr + "</script>";
        }

        protected string GetChannelLocation(int id, string fm)
        {
            DataRow[] dr = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + id.ToString());
            if (dr.Length == 0) return fm;
            fm = string.Format("&nbsp;" + config.Locationseparator + "&nbsp;<a href=\"{0}\" title=\"{1}\">{1}</a>", Urls.Channel(TypeParse.StrToInt(dr[0]["id"])), dr[0]["name"]) + fm;
            return GetChannelLocation(TypeParse.StrToInt(dr[0]["parentid"]), fm);
        }

        protected void SetContentLocation(ContentInfo info)
        {
            DataTable chls = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            string[] family = info.Channelfamily.Split(',');
            for (int i = family.Length - 1; i >= 0; i--)
            {
                if (family[i] == "") continue;
                DataRow[] chl = chls.Select("id=" + family[i]);
                if (chl.Length <= 0) continue;

                location += string.Format(location_format, "&nbsp;" + config.Locationseparator + "&nbsp;", Urls.Channel(TypeParse.StrToInt(chl[0]["id"])), chl[0]["name"]);
            }
            if (info.Channelid != 0)
                location += string.Format(location_format, "&nbsp;" + config.Locationseparator + "&nbsp;", Urls.Channel(info.Channelid), info.Channelname);
        }

        protected internal void PageInfo(string msg, string url)
        {
            PageInfo(msg, url, 3);
        }

        protected internal void PageInfo(string msg, int second)
        {
            PageInfo(msg, "", second);
        }

        protected internal void PageInfo(string msg)
        {
            PageInfo(msg, "", 3);
        }

        /// <summary>
        /// 是否已经发生错误
        /// </summary>
        /// <returns>有错误则返回true, 无错误则返回false</returns>
        public bool IsErr()
        {
            return errors > 0;
        }

        /// <summary>
        /// 增加错误提示
        /// </summary>
        /// <param name="errinfo">错误提示信息</param>
        public void AddErrLine(string errinfo)
        {
            msgtext = msgtext + (msgtext.Length == 0 ? "" : "<br/>") + errinfo;
            errors++;
        }


        /// <summary>
        /// 返回JSON数据格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="emsg"></param>
        /// <returns></returns>
        protected internal string Result<T>(T data, int code = 0, string emsg = "")
        {
            JsonResult<T> jsonResult = new JsonResult<T>(code, emsg) { Result = data };
            string result = JsonHelper.JsonSerializer<JsonResult<T>>(jsonResult, true);

            LogProvider.Logger.InfoFormat("{0}{1}\r\n\r\nAPI Response:{2}\r\n\r\n\r\n\r\n", STARequest.PrintRequestData(), STARequest.GetRequestHeader(), result);

            return result;
        }

        /// <summary>
        /// 增加提示信息
        /// </summary>
        /// <param name="strtxt">提示信息</param>
        public void AddMsgLine(string strtxt)
        {
            msgtext = msgtext + (msgtext.Length == 0 ? "" : "<br/>") + strtxt;
        }

        protected internal void PageInfo(string msg, string url, int second)
        {
            if (!ConUtils.IsCrossSitePost())
                url = STARequest.GetUrlReferrer();
            if (url == "")
                url = weburl;

            HttpContext.Current.Response.Clear();
            string txt1 = "提示信息";
            string txt2 = "如果浏览器没有自动跳转，请点击这里";
            if (curlang != "zh-CN")
            {
                txt1 = Translate(txt1, "zh-CN", curlang);
                txt2 = Translate(txt2, "zh-CN", curlang);
                webname = Translate(webname, "zh-CN", curlang);
                msg = Translate(msg, "zh-CN", curlang);
            }
            string repstr = "<html><head><title>" + webname + " " + txt1 + "</title>\r\n";
            repstr += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n";
            repstr += "<base target='_self'/>\r\n";
            repstr += "<style>div{line-height:160%;}\r\n</style></head>\r\n";
            repstr += "<body leftmargin='0' topmargin='0' bgcolor='#FFFFFF'><center>\r\n";
            repstr += "<br /><div style='width:450px;padding:0px;border:1px solid #DADADA;'><div style='padding:6px;font-size:12px;border-bottom:1px solid #DADADA;background:#f7f8f8 url(" + siteurl + "/sta/pics/bg_01.gif);'><b>" + webname + " " + txt1 + "！</b></div>\r\n";
            repstr += "<div style='_height:120px;min-height:120px;padding:0 15px;font-size:10pt;background:#ffffff'><br />\r\n";
            repstr += msg + "\r\n";
            if (url != string.Empty)
            {
                repstr += "<br/><br/><a href='" + (url == "back" ? "javascript:history.back(-1);" : url) + "'>" + txt2 + "</a></div>\r\n";
                repstr += "<script>setTimeout(function(){" + (url == "back" ? "history.back(-1)" : ("location.href='" + url + "'")) + "}," + second.ToString() + "000);</script>\r\n";
            }

            repstr += "</center></body></html>";
            HttpContext.Current.Response.Write(repstr);
            HttpContext.Current.Response.End();
        }
    }
}
