using System;
using System.Data;
using System.Web;
using System.Text;
using System.Xml;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Config;
using STA.Data;
using STA.Core.Publish;
using STA.Core.Collect;
using STA.Core;

namespace STA.Web.Admin
{
    public class Ajax : AdminPage
    {
        private static object lockhelper = new object();
        public Ajax()
        {
            string resultmessage = "";
            switch (STARequest.GetString("t").ToLower())
            {
                case "topmenu":
                    LoadTopMenu();
                    break;
                case "fastmenu":
                    LoadFastMenu();
                    break;
                case "clearcache":
                    ClearCache();
                    break;
                case "addfastmenu":
                    AddFastMenu();
                    break;
                case "getfield":
                    GetField();
                    break;
                case "savefield":
                    SaveField();
                    break;
                case "addcongroupcon":
                    AddConGroupContent();
                    break;
                case "addspecon":
                    AddSpecon();
                    break;
                case "delcongroupcon":
                    DelConGroupContent();
                    break;
                case "chnelgroupids":
                    GetChnelGroupIds();
                    break;
                case "delfiles":
                    DelFiles();
                    break;
                case "tplmake":
                    TplMake();
                    break;
                case "htmlcreate":
                    HtmlCreate();
                    break;
                case "pbhstatus":
                    PublishStatus();
                    break;
                case "spublish":
                    Publish();
                    break;
                case "dbcollectstatus":
                    DbCollectStatus();
                    break;
                case "webcollectstatus":
                    WebCollectStatus();
                    break;
                case "dbcollect":
                    STA.Core.Collect.DbCollect.Collect();
                    break;
                case "webcollect":
                    STA.Core.Collect.WebCollect.Collect();
                    break;
                case "webcolreset":
                    STA.Core.Collect.WebCollect.Reset();
                    break;
                case "sitemapmake":
                    SitemapMake();
                    break;
                case "readfile":
                    ReadFile();
                    break;
                case "dbconnecttest":
                    DbCollectTest();
                    break;
                case "dbtables":
                    DbTables();
                    break;
                case "dbtablefields":
                    DbTableFields();
                    break;
                case "addmenu":
                    AddMenu();
                    break;
                case "editmenu":
                    EditMenu();
                    break;
                case "delmenu":
                    int id = STARequest.GetInt("id", 0);
                    InsertLog(2, "删除菜单", "ID:" + id.ToString());
                    ResponseText(ConUtils.DeleteMenu(id).ToString());
                    break;
                case "getcontentlist":
                    GetContentList();
                    break;
                case "getattidbyname":
                    string filename = Utils.UrlDecode(STARequest.GetString("filename"));
                    if (sitepath != "" && filename.IndexOf(sitepath) == 0)
                        filename = filename.Substring(sitepath.Length);
                    AttachmentInfo att = Contents.GetAttachment(filename);
                    ResponseText(att == null ? "0" : att.Id.ToString());
                    break;
                case "translate":
                    string sourcetext = Utils.UrlDecode(STARequest.GetString("text"));
                    string sourcelang = STARequest.GetString("sl");
                    string targetlang = STARequest.GetString("tl");
                    string deftext = Utils.UrlDecode(STARequest.GetString("def"));
                    if (Utils.IsURL(sourcetext))
                        sourcetext = Utils.GetPageContent(new Uri(sourcetext), "utf-8");
                    if (sourcelang == "auto")
                        ResponseText(Translators.Translate(sourcetext, targetlang));
                    else
                        ResponseText(Translators.Translate(sourcetext, sourcelang, targetlang, deftext == "" ? sourcetext : deftext));
                    break;
                case "delpms"://批量删除短消息 返回删除数量
                    ResponseText(STA.Core.PrivateMessages.DelPrivateMessages(STARequest.GetString("isnew") != "1", STARequest.GetInt("postdatetime", 100).ToString(), STARequest.GetString("msgfromlist"), STARequest.GetString("lowerupper") != "1", STARequest.GetString("subject"), STARequest.GetString("message"), STARequest.GetString("isupdateusernewpm") == "1").ToString());
                    break;
                case "sendpmtogroup"://批量发送短消息
                    {
                        int start_uid = STARequest.GetInt("start_uid", 0);
                        resultmessage = UserUtils.SendPMByGroupidList(STARequest.GetString("groupidlist"), STARequest.GetInt("topnumber", 0),
                            ref start_uid,
                            STARequest.GetString("msgfrom"), STARequest.GetInt("msguid", 1), STARequest.GetInt("folder", 0),
                            STARequest.GetString("subject"), STARequest.GetString("postdatetime"), STARequest.GetString("message")).ToString();
                        resultmessage = string.Format("{{'startuid':{0},'count':'{1}'}}", start_uid, resultmessage);
                        System.Threading.Thread.Sleep(4000);/*暂停4秒，以减轻数据库压力*/
                        ResponseText(resultmessage);
                        break;
                    }
                case "usergroupsendemail"://批量发送邮件
                    {
                        int start_uid = STARequest.GetInt("start_uid", 0);
                        resultmessage = UserUtils.SendEmailByGroupidList(STARequest.GetString("groupidlist"), STARequest.GetInt("topnumber", 0),
                            ref start_uid, STARequest.GetString("subject"), STARequest.GetString("body")).ToString();
                        resultmessage = string.Format("{{'startuid':{0},'count':'{1}'}}", start_uid, resultmessage);
                        System.Threading.Thread.Sleep(4000);/*暂停4秒，以减轻数据库压力*/
                        ResponseText(resultmessage);
                        break;
                    }
                //case "xheditor":
                //    XHEdiror();
                //    break;
                default:
                    HttpContext.Current.Response.StatusCode = 404;
                    break;
            }
        }

        //private void XHEdiror()
        //{
        //    string type = STARequest.GetString("type").Trim();
        //    GeneralConfigInfo config = GeneralConfigs.GetConfig();
        //    if (type == "img")
        //    {
        //        AttachmentInfo[] atts = ConUtils.SaveRequestFiles(1, "jpg,gif,png,bmp,jpeg", 5096, "", "", 0, config.Waterposition, "filedata", config);
        //        if (atts != null)
        //        {
        //            ResponseJSON("{'err':'','msg':{'url':'" + atts[0].Filename.Replace("/","\\/") + "','localname':'" + atts[0].Attachment + "','id':'" + atts[0].Id + "'}}");
        //        }
        //    }
        //    ResponseJSON("");
        //}

        private void GetContentList()
        {
            int num = STARequest.GetInt("num", 10);
            string fields = STARequest.GetString("fields");
            num = num <= 0 ? 10 : num;
            fields = fields.Trim() == "" ? "*" : fields;

            string where = Contents.GetContentSearchCondition(STARequest.GetInt("typeid", -1), Utils.UrlDecode(STARequest.GetString("addusers")), STARequest.GetInt("recyle", 0), STARequest.GetInt("channelid", 0), (int)ConStatus.通过, STARequest.GetString("property"), STARequest.GetString("startdate"), STARequest.GetString("enddate"), Utils.UrlDecode(STARequest.GetString("keywords")));

            ResponseJSON(Utils.DataTableToJSON(Contents.GetContentTableByWhere(num, fields, where)).ToString());
        }

        private void AddMenu()
        {
            int id = Menus.AddMenu(GetRequestMenuInfo());
            InsertLog(2, "添加菜单", "ID:" + id);
            ResponseText(id.ToString());
        }




        private MenuInfo GetRequestMenuInfo()
        {
            MenuInfo info = new MenuInfo();
            info.Id = STARequest.GetInt("id", 0);
            info.Name = Utils.UrlDecode(STARequest.GetString("name"));
            info.Parentid = STARequest.GetInt("parentid", 0);
            info.System = byte.Parse(STARequest.GetInt("system", 1).ToString());
            info.Pagetype = (PageType)STARequest.GetInt("pagetype", 1);
            info.Type = byte.Parse(STARequest.GetInt("type", 1).ToString());
            info.Icon = Utils.UrlDecode(STARequest.GetString("icon"));
            info.Target = Utils.UrlDecode(STARequest.GetString("target"));
            info.Url = Utils.UrlDecode(STARequest.GetString("url"));
            info.Orderid = STARequest.GetInt("orderid", 0);
            info.Identify = Utils.UrlDecode(STARequest.GetString("identify"));
            return info;
        }

        private void EditMenu()
        {
            MenuInfo info = GetRequestMenuInfo();
            InsertLog(2, "编辑菜单", "ID:" + info.Id.ToString());
            ResponseText(Menus.EditMenu(info).ToString());
        }

        private void DbCollectTest()
        {
            ResponseText(Databases.DbConnectTest(Databases.DbConnectString(STARequest.GetString("source"), STARequest.GetString("userid"), STARequest.GetString("password"), STARequest.GetString("dbname"))).ToString());
        }

        private void DbTableFields()
        {
            ResponseText(Utils.DataTableToJSON(Databases.DbTableFields(Databases.DbConnectString(STARequest.GetString("source"), STARequest.GetString("userid"), STARequest.GetString("password"), STARequest.GetString("dbname")), STARequest.GetString("tbname"))).ToString());
        }

        private void DbTables()
        {
            ResponseText(Utils.DataTableToJSON(Databases.DbTables(Databases.DbConnectString(STARequest.GetString("source"), STARequest.GetString("userid"), STARequest.GetString("password"), STARequest.GetString("dbname")))).ToString());
        }

        private void ReadFile()
        {

        }

        private void SitemapMake()
        {
            SitemapCreate.Create(STARequest.GetString("frequency"), STARequest.GetString("priority"), STARequest.GetInt("count", 1000));
            ResponseText("yes");
        }

        private void TplMake()
        {
            string skiname = STARequest.GetString("skinname");
            if (skiname == "")
                skiname = config.Templatename;
            if (skiname != "default")
                Globals.MakeTemplate("default");
            ResponseText(Globals.MakeTemplate(skiname));
        }

        private void PublishStatus()
        {
            string json = "{" + string.Format("total:{0},success:{1},fail:{2}", StaticPublish.totalcount, StaticPublish.successcount, StaticPublish.failcount) + "}";
            ResponseJSON(json);
        }

        private void Publish()
        {
            StaticPublish.Reset();
            bool publish = TypeParse.StrToBool(STARequest.GetString("pub"), false);
            switch (STARequest.GetString("name"))
            {
                case "rss": StaticPublish.PublishRss(publish); break;
                case "content": StaticPublish.PublishContent(publish); break;
                case "channel": StaticPublish.PublishChannel(publish); break;
                case "page": StaticPublish.PublishPage(publish); break;
                case "special": StaticPublish.PublishSpecial(publish); break;
                case "sitemap": StaticPublish.PublishSiteMap(publish); break;
                case "onekey": StaticPublish.OnKeyPublish(publish); break;
                default: StaticPublish.PublishIndex(publish); break;
            }
            ResponseText(StaticPublish.totalcount.ToString());
        }

        private void DbCollectStatus()
        {
            string json = "{" + string.Format("total:{0},success:{1},fail:{2},msg:'数据库采集,共有{0} 个文档可采集,已成功采集 {1} 个, 失败 {2} 个.'", STA.Core.Collect.DbCollect.totalcount, STA.Core.Collect.DbCollect.successcount, STA.Core.Collect.DbCollect.failcount) + "}";
            ResponseJSON(json);
        }

        private void WebCollectStatus()
        {
            string json = "{" + string.Format("total:{0},success:{1},fail:{2},msg:'站点采集,共有{0} 个文档可采集,已成功采集 {1} 个, 失败 {2} 个.'", STA.Core.Collect.WebCollect.totalcount, STA.Core.Collect.WebCollect.successcount, STA.Core.Collect.WebCollect.failcount) + "}";
            ResponseJSON(json);
        }

        private void HtmlCreate()
        {
            string type = STARequest.GetString("type");
            int id = STARequest.GetInt("id", 0);
            int page = STARequest.GetInt("page", 1);
            bool success = false;
            switch (type)
            {
                case "rss": success = StaticCreate.CreateRss(config, id, config.Rssconcount); break;
                case "content": success = StaticCreate.CreateContent(id); break;
                case "page": success = StaticCreate.CreatePage(id); break;
                case "special": success = StaticCreate.CreatePage(id); break;
                case "channel": success = StaticCreate.CreateChannel(id, page); break;
                default: success = StaticCreate.CreateIndex(); break;
            }
            ResponseText(success.ToString());
        }

        private void DelConGroupContent()
        {
            ResponseText(Contents.DelGroupCon(STARequest.GetInt("cid", 0), STARequest.GetInt("gid", 0)).ToString());
        }

        private void DelFiles()
        {
            foreach (string img in Utils.SplitString(STARequest.GetFormString("files"), "*sta*"))
            {
                if (img.Trim() == "") return;
                ConUtils.DelAttachment(Utils.UrlDecode(img));
            }
            ResponseText("yes");
        }

        private void AddConGroupContent()
        {
            int cid = STARequest.GetInt("cid", 0);
            int gid = STARequest.GetInt("gid", 0);
            int orderid = STARequest.GetInt("oid", 0);
            ResponseText(Contents.AddGroupCon(gid, cid, orderid).ToString());
        }

        private void AddSpecon()
        {
            int cid = STARequest.GetInt("cid", 0);
            int sid = STARequest.GetInt("sid", 0);
            ResponseText((Contents.AddSpeccontent(new SpecontentInfo(sid, 0, cid)) > 0).ToString());
        }

        private void GetChnelGroupIds()
        {
            int gid = STARequest.GetInt("gid", 0);
            ResponseText(Utils.DataTableToJSON(Contents.GetChannelGroupIds(gid)).ToString());
        }

        private void SaveField()
        {
            try
            {
                string nodename = Utils.UrlDecode(STARequest.GetString("node"));
                string content = Utils.UrlDecode(STARequest.GetString("content"));
                string filename = sitepath + adminpath + "/xml/field.config";
                XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(filename));
                doc.SelectSingleNode("FieldConfigInfo/" + nodename).InnerText = content;
                XMLUtil.SaveDocument(Utils.GetMapPath(filename), doc);
                ResponseText("yes");
            }
            catch { }
        }

        private void GetField()
        {
            try
            {
                string nodename = Utils.UrlDecode(STARequest.GetString("node"));
                XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(sitepath + adminpath + "/xml/field.config"));
                ResponseText(doc.SelectSingleNode("FieldConfigInfo/" + nodename).InnerText);
            }
            catch { }
        }

        private void ClearCache()
        {
            Caches.ReSetAllCache();
            ResponseText("yes");
        }

        private void AddFastMenu()
        {
            lock (lockhelper)
            {
                string name = Utils.UrlDecode(STARequest.GetString("name"));
                string url = Utils.UrlDecode(STARequest.GetString("url"));
                XmlDocument doc = XMLUtil.LoadDocument(ConUtils.UserLikeXmlPath(userid, baseconfig));
                XmlNode item = XMLUtil.CreateNode(doc.SelectSingleNode("data/fastmenu"), "item");
                item.Attributes.Append(XMLUtil.CreateAttribute(doc, "name", name));
                item.Attributes.Append(XMLUtil.CreateAttribute(doc, "url", url));
                item.Attributes.Append(XMLUtil.CreateAttribute(doc, "target", STARequest.GetString("target")));
                XMLUtil.SaveDocument(ConUtils.UserLikeXmlPath(userid, baseconfig), doc);
                ResponseText("yes");
            }
        }


        private void LoadFastMenu()
        {
            lock (lockhelper)
            {
                string json = string.Empty;
                XmlDocument doc = XMLUtil.LoadDocument(ConUtils.UserLikeXmlPath(userid, baseconfig));
                foreach (XmlNode node in doc.SelectNodes("data/fastmenu/item"))
                {
                    string url = node.Attributes["url"].Value;
                    url = url.Trim().Length > 3 ? sitepath + adminpath + "/" + url : string.Empty;
                    json += "{" + string.Format("'name':'{0}','url':'{1}','target':'{2}'", node.Attributes["name"].Value, url, node.Attributes["target"].Value) + "},";
                }
                json = json.Length > 1 ? json.Substring(0, json.Length - 1) : json;
                ResponseJSON("[" + json + "]");
            }
        }

        private void LoadTopMenu()
        {
            lock (lockhelper)
            {
                string json = string.Empty;
                XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(sitepath + adminpath + "/xml/menu.config"));
                foreach (XmlNode node in doc.SelectNodes("data/top"))
                {
                    string url = node.Attributes["url"].Value;
                    url = url.Trim().Length > 3 ? url : string.Empty;
                    json += "{" + string.Format("'id':'{0}','name':'{1}','url':'{2}'", node.Attributes["id"].Value, node.Attributes["name"].Value, url) + "},";
                }
                json = json.Length > 1 ? json.Substring(0, json.Length - 1) : json;
                ResponseJSON("[" + json + "]");
            }
        }

        private void ResponseText(String text)
        {
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }

        private void ResponseJSON(string json)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/json";
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Write(json);
            System.Web.HttpContext.Current.Response.End();
        }
    }
}