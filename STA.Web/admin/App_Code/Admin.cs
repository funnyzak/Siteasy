using System;
using System.Xml;
using System.Web;
using System.Text;
using System.Web.UI;

using STA.Entity;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Data;
using System.Data;

namespace STA.Web.Admin
{
    public class AdminPage : System.Web.UI.Page
    {

        protected internal string username = "admin";

        /// <summary>
        /// 当前用户的用户ID
        /// </summary>
        protected internal int userid = 1;

        protected internal string password = "";

        protected internal bool isShowSysMenu = true;

        protected internal string filesavepath = "/files";

        protected internal string currentpage = "";

        protected internal GeneralConfigInfo config;

        protected internal BaseConfigInfo baseconfig;

        protected internal LikesetInfo likeinfo;

        protected int maxfilesize = 1024000;

        protected internal string notfoundermsg = "只有创始人可以执行此操作！";
        /// <summary>
        /// 当前用户的管理组ID
        /// </summary>
        protected internal int admingroupid = 0;

        protected internal string admingroupname = string.Empty;

        protected internal string adminpath = string.Empty;

        protected internal string sitepath = string.Empty;

        protected internal string systyle = string.Empty;

        /// <summary>
        /// 浏览器名称和版本号
        /// </summary>
        protected internal string browser = string.Empty;

        protected internal string editorset = string.Empty;

        protected internal string templatename = string.Empty;

        protected internal int managelistcount = 20;

        public string footer = "Copyright © 2009-" + DateTime.Now.Year.ToString() + " By <span class=\"darkgrey\"><a href=\"" + Utils.OfficeSite + "\" target=\"_blank\" title=\"" + Utils.ProductName + "\">" + Utils.ProductName + "</a></span> All Rights Reserved";


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public AdminPage()
        {
            config = GeneralConfigs.GetConfig();
            baseconfig = BaseConfigs.GetBaseConfig();
            sitepath = baseconfig.Sitepath;
            adminpath = baseconfig.Adminpath;
            templatename = config.Templatename;
            filesavepath = sitepath + config.Attachsavepath;
            currentpage = STARequest.GetUrl();

            // 如果IP访问列表有设置则进行判断
            if (config.Adminipaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Adminipaccess, "\n");
                if (!Utils.InIPArray(STARequest.GetIP(), regctrl))
                {
                    Redirect(sitepath + adminpath + "/login.aspx");
                    return;
                }
            }

            OnlineUserInfo oluserinfo = ConUtils.GetOnlineUser();
            this.password = oluserinfo.Password;
            this.userid = oluserinfo.Userid;
            this.admingroupname = oluserinfo.Admingroupname;
            this.username = oluserinfo.Username;
            this.admingroupid = oluserinfo.Adminid;

            likeinfo = ConUtils.GetLikeset(this.userid);
            browser = STARequest.GetBrowser();
            systyle = likeinfo.Systemstyle;
            editorset = GetEditorSet();
            managelistcount = likeinfo.Managelistcount;

            if (!Page.IsPostBack)
                this.RegisterAdminPageClientScriptBlock();
            if (userid <= 0 || oluserinfo.Adminid == 0 || Context.Request.Cookies["staadmin"] == null || Context.Request.Cookies["staadmin"]["userid"] == null || (!IsPostBack && !CheckAuthority()))
            {
                Redirect(sitepath + adminpath + "/login.aspx");
                return;
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["staadmin"];
                cookie.Expires = DateTime.Now.AddMinutes(60);
                cookie.Values["userid"] = oluserinfo.Userid.ToString();
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        protected string GetEditorSet()
        {
            string uiColor = "";
            switch (systyle)
            {
                case "blue": uiColor = "#99CCFF"; break;
                case "black": uiColor = "#999999"; break;
                case "fashion": uiColor = "#9966CC"; break;
                case "green": uiColor = "#CCFF99"; break;
                case "red": uiColor = "#FF6666"; break;
                case "yellow": uiColor = "#FFCC99"; break;
                case "pink": uiColor = "#f75fc7"; break;
                default: uiColor = ""; break;
            }
            return GetEditorSet(uiColor);
        }

        protected string GetEditorSet(string uicolor)
        {
            return string.Format("filebrowserBrowseUrl: '{0}/tools/selectfile.aspx?root={1}',"
                                 + "filebrowserImageUploadUrl: '{0}/tools/editorupload.aspx?ftype=image&savedir=cimg',"
                                 + "filebrowserFlashUploadUrl: '{0}/tools/editorupload.aspx?ftype=flash&&savedir=cswf',"
                                 + "uiColor:'{2}'", adminpath, filesavepath, uicolor);
        }
        /// <summary>
        /// 如果没有页面访问权限
        /// </summary>
        /// <returns></returns>
        protected bool CheckAuthority()
        {
            string page = HttpContext.Current.Request.Url.AbsolutePath.Replace(baseconfig.Sitepath + baseconfig.Adminpath + "/", "");
            page = page.Substring(0, page.IndexOf(".aspx") + 5);

            if (Utils.InArray(page, "ajax.aspx,index.aspx", ",") || IsFounder(userid) || page.StartsWith("frames/") || page.StartsWith("tools/"))
                return true;

            if (page.StartsWith("plus/"))
                return Menus.CheckPageAuthority(admingroupid, 129);

            return Menus.CheckPageAuthority(admingroupid, page);
        }

        protected string GetConfigFieldValue(string name)
        {
            string fieldconfigfilename = sitepath + adminpath + "/xml/field.config";
            return XMLUtil.GetNodeValue(Utils.GetMapPath(fieldconfigfilename), name);
        }

        public new void RegisterStartupScript(string key, string scriptstr)
        {
            base.ClientScript.RegisterStartupScript(this.GetType(), key, "<script>" + scriptstr + "</script>");
        }

        /// <summary>
        /// 检查cookie是否有效
        /// </summary>
        /// <returns></returns>
        public bool CheckCookie()
        {
            // 获取用户信息
            OnlineUserInfo oluserinfo = ConUtils.GetOnlineUser();
            if (userid <= 0 || oluserinfo.Adminid == 0 || Context.Request.Cookies["staadmin"] == null || Context.Request.Cookies["staadmin"]["userid"] == null)
            {
                Context.Response.Redirect(sitepath + adminpath + "login.aspx");
                return false;
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["staadmin"];
                cookie.Expires = DateTime.Now.AddMinutes(60);
                cookie.Values["userid"] = oluserinfo.Userid.ToString();
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            return true;
        }

        /// <summary>
        /// 注册提示信息JS脚本
        /// </summary>
        public void RegisterAdminPageClientScriptBlock()
        {
            string overlayclass = "popbgcolor-" + likeinfo.Systemstyle;
            string popwinoverlay = likeinfo.Overlay.ToString();
            StringBuilder script = new StringBuilder(2000);
            script.Append("<script type=\"text/javascript\">$(function(){$(\"#msgbox\").jqm({ overlay: " + popwinoverlay + " , modal: true , overlayClass: \"" + overlayclass + "\", trigger: false });"
                            + "$(\"#editbox,#confirmbox,#msgbox\").draggable({ handle: \".mbtitle\", containment: \"document\" });  $(\"#msgclose\").bind(\"click\", function () { $(\"#msgbox\").jqmHide(); });");
            script.Append("$(\"#confirmbox\").jqm({ overlay: " + popwinoverlay + " , modal: true , overlayClass: \"" + overlayclass + "\", trigger: false }); $(\"#confirmclose\").bind(\"click\","
                            + " function () { $(\"#confirmbox\").jqmHide(); });");
            script.Append("$(\"#editbox\").jqm({ overlay:" + popwinoverlay + ", modal: true, overlayClass: \"" + overlayclass + "\",trigger: false });$(\"#editclose\").bind(\"click\", function ()"
                            + " {$(\"#editbox\").jqmHide();})});</script>");
            script.Append("<div style=\"z-index: 200; display: none;\" class=\"jqmWindow\" id=\"msgbox\">");
            script.Append("<div class=\"mbtitle jqDrag\"><div id=\"msgtitle\">&nbsp;信息提示</div><div id=\"msgclose\" class=\"mbclose\" title=\"点击关闭\">关闭</div></div>");
            script.Append("<div class=\"mbcontent\" id=\"msgcontent\">操作已成功执行！</div></div>");

            script.Append("<div style=\"z-index: 200; display: none;\" class=\"jqmWindow\" id=\"editbox\"><div class=\"mbtitle\"><div id=\"edittitle\"></div><div id=\"editclose\" class=\"mbclose\" title=\"点击关闭\">关闭</div></div><div class=\"mbcontent3\" id=\"editcontent\"></div></div>");

            script.Append("<div style='z-index: 300; display: none;' class='jqmWindow' id='confirmbox'><div class='mbtitle jqDrag'><div id='confirmtitle'>&nbsp;请确认您的操作</div>             <div id='confirmclose' class='mbclose' title='点击关闭'>关闭</div></div><div class='mbcontent2' id='confirmcontent'></div><div class='jqmbutton'><input type='button' value=' 确 认 ' id='confirmok'/><input type='button' value=' 取 消 ' id='confirmcancel'/></div></div>");

            //script.Append("<div class='" + overlayclass + "' id='overlay_backgroundcolor' style='display:none;'></div>");
            script.Append("<script>config={webname:\"" + config.Webname + "\",weburl:\"" + config.Weburl + "\",adminpath:\"" + sitepath + adminpath + "\",editorset:{ " + editorset + "}");
            script.Append(",browser:\"" + browser + "\"");
            script.Append(",overlay:" + popwinoverlay + ",overlayClass: \"" + overlayclass + "\"");
            script.Append(",sitepath:\"" + sitepath + "\",filesavepath:\"" + filesavepath + "\"}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Page", script.ToString());
        }

        public void RegisterClientScriptBlock(string script)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "<script type=\"text/javascript\">" + script + "</script>");
        }

        protected void NoFounderMessage()
        {
            Message("只有创始人可以执行此操作！");
        }

        public bool IsFounder(int uid)
        {
            if (BaseConfigs.GetBaseConfig().Founderuid == 0) return true;
            else
            {
                if (BaseConfigs.GetBaseConfig().Founderuid == uid)
                    return true;
                else
                    return false;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!isShowSysMenu) return;
            string topHtml = "<div id=\"sysnav\" class=\"clearfix\"><ul>";
            //topHtml += "<li class=\"anchor\"><a href=\"javascript:ShowMenuSiteMap();\">功能导航</a></li>";
            topHtml += "<li class=\"advise\" title=\"如果有任何建议及系统BUG可以通过这里提交\"><a href=\"javascript:Advise();\">需求建议</a></li>";
            FavoriteMenuStatus status = GetFavouriteMenuStatus();
            if (status != FavoriteMenuStatus.Hidden)
            {
                if (status == FavoriteMenuStatus.Exist)
                {
                    topHtml += "<li class=\"addmenu\" style=\"background-image:url('../images/existmenu.gif')\"><a href='#' title=\"该页面已加入收藏\">已加入收藏</a></li>";
                }
                else if (status == FavoriteMenuStatus.Full)
                {
                    topHtml += "<li class=\"addmenu\" style=\"background-image:url('../images/existmenu.gif')\"><a href='#' title=\"收藏已满\">收藏已满</a></li>";
                }
                else if (status == FavoriteMenuStatus.Show)
                {
                    topHtml += "<li class=\"addmenu\"><a href='javascript:top.StatusAction(\"addfastmenu&name=\" + encodeURI(window.document.title) + \"&url=\" + window.location.pathname.toLowerCase().replace(\"" + sitepath + adminpath + "/\",\"\") + window.location.search.toLowerCase() + \"&target=main\", \"加入菜单\", function () {$(\"#sysnav\").find(\"li.addmenu\").html(\"已加入收藏\"); $(\"#sysnav\").find(\"li.addmenu\").css(\"background-image\", \"url(../images/existmenu.gif)\");top.LoadFastMenuData();});' title=\"将该页面加入快捷菜单\">加入常用功能</a></li>";
                }
            }
            topHtml += "</ul></div>";
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Html", topHtml);
        }

        // 收藏夹的状态，Show正常显示，Full收藏夹已满，Exist收藏项已经存在
        private enum FavoriteMenuStatus { Show, Full, Exist, Hidden }

        /// <summary>
        /// 获取快捷菜单的状态
        /// </summary>
        /// <returns></returns>
        private FavoriteMenuStatus GetFavouriteMenuStatus()
        {
            try
            {
                string filename = string.Format(sitepath + adminpath + "/xml/user_{0}.config", userid);
                XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(filename));

                string url = STARequest.GetUrl().ToLower();
                string pagename = url.Substring(url.LastIndexOf(baseconfig.Adminpath + "/") + baseconfig.Adminpath.Length + 1);

                DataTable mdt = Caches.GetMenus(1, GeneralConfigs.GetConfig().Cacheinterval * 60);
                bool hasmenu = false;

                foreach (DataRow dr in mdt.Rows)
                {
                    string menurl = dr["url"].ToString().Trim();
                    if (pagename.Length > 10 && menurl.EndsWith(pagename))
                    {
                        hasmenu = true;
                        break;
                    }
                }
                if (!hasmenu)
                    return FavoriteMenuStatus.Hidden;


                int menuCount = doc.SelectNodes("data/fastmenu/item").Count;
                if (menuCount >= likeinfo.Fastmenucount)
                    return FavoriteMenuStatus.Full;

                foreach (XmlNode fastmenu in doc.SelectNodes("data/fastmenu/item"))
                {
                    if (fastmenu.Attributes.Count < 3) continue;
                    string menurl = fastmenu.Attributes["url"].Value;
                    if (menurl.EndsWith(pagename)) return FavoriteMenuStatus.Exist;
                }
                return FavoriteMenuStatus.Show;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected string ReplaceJsComma(object target, int length)
        {
            string ret = Utils.GetUnicodeSubString(TypeParse.ObjToString(target), length, "..");
            return ret.Replace("'", "\\'").Replace("\"", "\\\"");
        }

        protected string ReplaceJsComma(object target)
        {
            return ReplaceJsComma(target, 100000);
        }

        protected string GetConManagePage(int type)
        {
            DataTable cdt = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = cdt.Select("id=" + type.ToString());
            if (drs.Length > 0)
                return BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/global/" + drs[0]["bglistmod"].ToString() + "?type=" + type.ToString();
            else
                return BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/global/" + "global_contentlist.aspx?type=" + type.ToString();
        }

        protected string GetConAddPage(int type)
        {
            DataTable cdt = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = cdt.Select("id=" + type.ToString());
            if (drs.Length > 0)
                return BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/global/" + drs[0]["bgaddmod"].ToString() + "?type=" + type.ToString();
            else
                return BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/global/" + "global_contentadd.aspx?type=" + type.ToString();
        }

        protected void RedirectMessage()
        {
            if (STARequest.GetQueryString("msg") != string.Empty)
                Message(Utils.UrlDecode(STARequest.GetQueryString("msg")));
        }

        protected void RedirectManangePage(string msg)
        {
            string url = Utils.UrlDecode(STARequest.GetQueryString("url"));
            if (url.Trim() != "")
            {
                if (url.IndexOf("?msg=") >= 0 || url.IndexOf("&msg=") >= 0)
                {
                    url = url.Substring(0, url.IndexOf("msg=") - 1);
                }
                Redirect(url + (url.Split('?').Length > 1 ? "&" : "?") + "msg=" + Utils.UrlEncode(msg));
            }
            else
            {
                Redirect(GetConManagePage(STARequest.GetQueryInt("type", 1)) + "&msg=" + Utils.UrlEncode(msg));
            }
        }

        protected void InsertLog(int admintype, string action, string desc)
        {
            ConUtils.InsertLog(admintype, userid, username, admingroupid, admingroupname, STARequest.GetIP(), action, desc);
        }

        protected void Message(string title, string msg, string url, int second, string func)
        {
            string script = string.Empty;
            if (likeinfo.Msgtip == 1)
            {
                script = "<script type=\"text/javascript\">$(function(){$(\"#msgtitle\").html(\"&nbsp;" + (title == string.Empty ? "信息提示" : title) + "\");";
                if (url.Trim() != string.Empty)
                    script += "$(\"#msgclose\").bind(\"click\",function(){window.location.href=\"" + url + "\";});";
                script += "$(\"#msgcontent\").html(\"" + (msg == string.Empty ? "执行的操作已完成！" : msg) + "\");";
                if (second > 0)
                {
                    script += "window.setTimeout(function(){$(\"#msgbox\").jqmHide();";
                    if (url.Trim() != string.Empty)
                        script += "window.location.href=\"" + url + "\";";
                    if (func.Trim() != string.Empty)
                        script += func;
                    script += "}," + (second * 1000).ToString() + ");";
                }
                script += "$(\"#msgbox\").jqmShow();});</script>";

            }
            else
            {
                if (url.Trim() != string.Empty)
                    script = "<script type=\"text/javascript\">location.href='" + url + "';</script>";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", script);
        }

        protected void Message()
        {
            Message(string.Empty);
        }

        protected void Message(string msg)
        {
            Message(string.Empty, msg, string.Empty, 1, "");
        }

        protected void Message(int second)
        {
            Message(string.Empty, string.Empty, string.Empty, second, "");
        }

        protected void Message(bool successed)
        {
            Message(string.Format("操作执行{0}！", successed ? "成功" : "失败"));
        }
        protected void Message(bool successed, string url)
        {
            Message(string.Format("操作执行{0}！", successed ? "成功" : "失败"), url);
        }

        protected void Message(string msg, int second, string func)
        {
            Message("", msg, "", second, func);
        }

        protected void Message(string msg, bool isReload)
        {
            Message(string.Empty, msg, isReload ? STARequest.GetUrl() : string.Empty, 1, "");
        }

        protected void Message(string msg, int second)
        {
            Message(string.Empty, msg, string.Empty, second, "");
        }

        protected void Message(string msg, string url)
        {
            Message(string.Empty, msg, url, 1, "");
        }

        protected void Message(string msg, string url, int second)
        {
            Message(string.Empty, msg, url, second, "");
        }

        protected void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

    }
}
