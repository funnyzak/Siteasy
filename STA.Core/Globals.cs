using System;
using System.Web;
using System.Data;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using STA.Common;
using System.Collections;
using System.Text;

using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Core
{
    public class Globals
    {
        private static Regex r_word;

        /// <summary>
        /// 替换原始字符串中的脏字词语
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>替换后的结果</returns>
        public static string BanWordFilter(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            string[,] str = Caches.GetBanWordList();
            int count = str.Length / 2;
            for (int i = 0; i < count; i++)
            {
                if (str[i, 1] != "{BANNED}" && str[i, 1] != "{MOD}")
                {
                    sb = new StringBuilder().Append(
                                  Regex.Replace(sb.ToString(), str[i, 0],
                                        str[i, 1],
                                        Utils.GetRegexCompiledOptions()));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 判断字符串是否包含脏字词语
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>如果包含则返回true, 否则反悔false</returns>
        public static bool InBanWordArray(string text)
        {
            string[,] str = Caches.GetBanWordList();

            for (int i = 0; i < str.Length / 2; i++)
            {
                r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                foreach (Match m in r_word.Matches(text))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 指定的字符串中是否含有禁止词汇
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public static bool HasBannedWord(string text)
        {
            string[,] str = Caches.GetBanWordList();
            text = RemoveSpecialChars(text, GeneralConfigs.GetConfig().Antispamreplacement);

            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i, 1] == "{BANNED}")
                {
                    r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                    foreach (Match m in r_word.Matches(text))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得被屏蔽掉的关键词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetBannedWord(string text)
        {
            string[,] str = Caches.GetBanWordList();
            text = RemoveSpecialChars(text, GeneralConfigs.GetConfig().Antispamreplacement);

            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i, 1] == "{BANNED}")
                {
                    r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                    foreach (Match m in r_word.Matches(text))
                        return m.Groups[0].ToString();
                }
            }
            return string.Empty;

        }

        /// <summary>
        /// 指定的字符串中是否含有需要审核词汇
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>bool</returns>
        public static bool HasAuditWord(string text)
        {
            string[,] str = Caches.GetBanWordList();
            text = RemoveSpecialChars(text, GeneralConfigs.GetConfig().Antispamreplacement);

            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i, 1] == "{MOD}")
                {
                    r_word = new Regex(str[i, 0], Utils.GetRegexCompiledOptions());
                    foreach (Match m in r_word.Matches(text))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除特殊字符
        /// </summary>
        /// <param name="content"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string RemoveSpecialChars(string content, string keyCharString)
        {
            char[] c = keyCharString.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                content = content.Replace(Convert.ToString(c[i]), string.Empty);
            }

            return content.Replace(" ", "");
        }

        /// <summary>
        /// 生成JS配置文件
        /// </summary>
        /// <param name="savepath"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string CreateJsConfigFile(string savepath, GeneralConfigInfo config)
        {
            if (savepath == "")
                savepath = Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/js/");

            FileUtil.CreateFolder(savepath);

            string configstr = "var webconfig = {\r\n";
            configstr += string.Format("\twebname:'{0}',\r\n", config.Webname);
            configstr += string.Format("\tweburl:'{0}',\r\n", config.Weburl);
            configstr += string.Format("\temail:\'{0}\',\r\n", config.Adminmail);
            configstr += string.Format("\twebdir:'{0}',\r\n", BaseConfigs.GetSitePath);
            configstr += string.Format("\ttempname:'{0}',\r\n", config.Templatename);
            //configstr += string.Format("\temailmult:{0},\r\n", config.Emailmultuser);
            configstr += string.Format("\topencomment:{0},\r\n", config.Opencomment);
            configstr += string.Format("\tcommentinterval:{0},\r\n", config.Commentinterval);
            configstr += string.Format("\tcommentverify:{0},\r\n", config.Commentverify);
            //configstr += string.Format("\tcommentfloor:{0},\r\n", config.Commentfloor);
            configstr += string.Format("\tcommentlogin:{0},\r\n", config.Commentlogin);
            //configstr += string.Format("\tregforbidwords:'{0}',\r\n", config.Forbiduserwords);
            configstr += string.Format("\tvcodemods:'{0}',\r\n", config.Vcodemods);
            configstr += string.Format("\tvtype:{0}\r\n", config.Dynamiced);
            configstr += "};";

            FileUtil.WriteFile(savepath + "config.js", configstr);

            return configstr;
        }

        public static DataTable BuildTemplates(string tplpath)
        {
            DataTable tpldt = new DataTable();
            tpldt.Columns.Add("id", Type.GetType("System.Int32"));
            tpldt.Columns.Add("name", Type.GetType("System.String"));
            tpldt.Columns.Add("pathname", Type.GetType("System.String"));
            tpldt.Columns.Add("author", Type.GetType("System.String"));
            tpldt.Columns.Add("createtime", Type.GetType("System.String"));
            tpldt.Columns.Add("version", Type.GetType("System.String"));
            tpldt.Columns.Add("copyright", Type.GetType("System.String"));
            tpldt.Columns.Add("storage", Type.GetType("System.String"));

            List<FileItem> flist = FileUtil.GetFiles(tplpath, "config", "about.config");
            int loop = 0;
            foreach (FileItem fitem in flist)
            {
                if (fitem.FullName.Replace(tplpath, "").Replace("\\about.config", "").IndexOf("\\") >= 0) //模板是否在模版根文件夹
                    continue;
                XmlDocument doc = XMLUtil.LoadDocument(fitem.FullName);
                DataRow tdr = tpldt.NewRow();
                XmlNode nodename = doc.SelectSingleNode("about/name");
                XmlNode nodeauthor = doc.SelectSingleNode("about/author");
                XmlNode nodetime = doc.SelectSingleNode("about/createtime");
                XmlNode nodecopyright = doc.SelectSingleNode("about/copyright");
                XmlNode nodeversion = doc.SelectSingleNode("about/version");

                tdr["id"] = loop.ToString();
                tdr["name"] = nodename == null ? "未知" : nodename.InnerText;
                tdr["author"] = nodeauthor == null ? "未知" : nodeauthor.InnerText;
                tdr["createtime"] = nodetime == null ? "未知" : nodetime.InnerText;
                tdr["copyright"] = nodecopyright == null ? "未知" : nodecopyright.InnerText;
                tdr["version"] = nodeversion == null ? "未知" : nodeversion.InnerText;
                string pathname = fitem.FullName.Replace("\\about.config", "");
                pathname = pathname.Substring(pathname.LastIndexOf('\\') + 1);
                tdr["pathname"] = pathname;

                string aspxpath = Utils.GetMapPath("../../sta/aspx/" + pathname);
                tdr["storage"] = FileUtil.GetFiles(aspxpath, "aspx").Count > 3 ? "已生成" : "未生成";

                tpldt.Rows.Add(tdr);
                loop++;
            }
            return tpldt;
        }

        public static string MakeTemplate(string skinname)
        {
            Templates tpls = new Templates();
            return tpls.MakeTplAspx(skinname);
        }

        public static string GetMenuItem(DataRow dr)
        {
            string icon = dr["icon"].ToString().Trim();
            if (icon != "")
                icon = "../images/icon/" + icon;

            return "{" + string.Format("id:{0},pId:{1},name:\"{2}\",url:\"{3}\",target:\"{4}\",icon:\"{5}\",system:{6},orderid:{7},type:{8},pagetype:{9}", dr["id"], dr["parentid"], dr["name"], dr["url"], dr["target"].ToString().Trim(), icon, dr["system"], dr["orderid"], dr["type"], dr["pagetype"]) + "}";
        }

        /// <summary>
        /// 删除扩展文件及数据库脚本
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool UnInstallPlugin(PluginInfo info)
        {
            if (info == null || info.Setup != 1) return true;

            if (info.Dbdelete.Trim() != "")
                DbHelper.ExecuteCommandWithSplitter(info.Dbdelete.Replace("@tbprefix_", BaseConfigs.GetTablePrefix));

            foreach (string file in Utils.SplitString(info.Filelist, "@separator@"))
            {
                if (FileUtil.FileExists(Utils.GetMapPath(BaseConfigs.GetSitePath + file)))
                    FileUtil.DeleteFile(Utils.GetMapPath(BaseConfigs.GetSitePath + file));
            }
            info.Setup = 0;
            info.Filelist = "";
            Plugins.EditPlugin(info);

            return true;
        }

        /// <summary>
        /// 安装扩展包
        /// </summary>
        /// <param name="info"></param>
        /// <param name="overfile">存在同名文件是否覆盖</param>
        /// <returns></returns>
        public static bool InstallPlugin(PluginInfo info, bool over)
        {
            if (info == null || info.Setup == 1 || !(info.Package.ToLower().EndsWith(".zip") && FileUtil.FileExists(Utils.GetMapPath(info.Package)))) return false;

            if (info.Dbcreate.Trim() != "")
                DbHelper.ExecuteCommandWithSplitter(info.Dbcreate.Replace("@tbprefix_", BaseConfigs.GetTablePrefix));

            info.Filelist = "";
            string zipfilepath = BaseConfigs.GetSitePath + "/sta/cache/temp/1/" + Rand.RamTime();
            FileUtil.CreateFolder(Utils.GetMapPath(zipfilepath));
            string zipfilepathtarget = BaseConfigs.GetSitePath + "/";
            ArrayList zipfiles = ZipFile.ZipFiles(Utils.GetMapPath(info.Package));
            foreach (string s in zipfiles)
            {
                info.Filelist += (s.StartsWith("/admin/") ? (BaseConfigs.GetAdminPath + s.Substring(6)) : s) + "@separator@";
            }
            DbHelper.ExecuteCommandWithSplitter(info.Dbcreate);
            info.Setup = 1;

            ZipFile.UnZip(ZipFile.UnzipType.ToOtherDirctory, Utils.GetMapPath(zipfilepath), new string[1] { Utils.GetMapPath(info.Package) });

            if (BaseConfigs.GetAdminPath != "/admin")
            {
                foreach (string dirname in Directory.GetDirectories(Utils.GetMapPath(zipfilepath)))
                {
                    if (dirname.EndsWith("\\admin"))
                    {
                        FileUtil.MoveFolder(Utils.GetMapPath(zipfilepath + "/admin"), Utils.GetMapPath(zipfilepath + BaseConfigs.GetAdminPath));
                    }
                }
            }

            List<FileItem> flist = FileUtil.GetFiles(Utils.GetMapPath(zipfilepath));
            foreach (FileItem f in flist)
            {
                FileUtil.MoveFile(f.FullName, Utils.GetMapPath(zipfilepathtarget) + f.FullName.Replace(Utils.GetMapPath(zipfilepath) + "\\", ""), over);
            }

            Plugins.EditPlugin(info);
            return true;
        }

        /// <summary>
        /// 上传扩展包
        /// </summary>
        public static string UploadPluginZip(GeneralConfigInfo config, string filekey, int uid, string username, ref PluginInfo info)
        {
            AttachmentInfo[] attachs = ConUtils.SaveRequestFiles(1, "zip", 100000, Utils.GetMapPath(BaseConfigs.GetSitePath + config.Attachsavepath + "/plus/"), "", 0, 0, "", filekey, config);
            if (attachs == null) return "请选择文件再上传！";

            if (attachs[0].Noupload != "") return attachs[0].Noupload;
            attachs[0].Uid = attachs[0].Lastedituid = uid;
            attachs[0].Username = attachs[0].Lasteditusername = username;
            Contents.AddAttachment(attachs[0]);

            string filename = attachs[0].Filename;
            string zipfilepath = BaseConfigs.GetSitePath + "/sta/cache/temp/2/" + Rand.RamTime();
            FileUtil.CreateFolder(Utils.GetMapPath(zipfilepath));
            ZipFile.UnZip(ZipFile.UnzipType.ToOtherDirctory, Utils.GetMapPath(zipfilepath), new string[1] { Utils.GetMapPath(filename) });

            string aboutxml = zipfilepath + "/about.xml";
            string installsql = zipfilepath + "/install.sql";
            string uninstallsql = zipfilepath + "/uninstall.sql";
            string readme = zipfilepath + "/readme.txt";
            string zip = zipfilepath + "/file.zip";
            string newzip = BaseConfigs.GetSitePath + config.Attachsavepath + "/plus/" + attachs[0].Attachment + "_" + Rand.RamTime() + ".zip";

            if (!FileUtil.FileExists(Utils.GetMapPath(aboutxml)) || !FileUtil.FileExists(Utils.GetMapPath(zip)))
                return "扩展包缺失必要的文件";

            FileUtil.MoveFile(Utils.GetMapPath(zip), Utils.GetMapPath(newzip));

            info = new PluginInfo();
            try
            {
                XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(aboutxml));
                info.Name = doc.SelectSingleNode("plugin/name").InnerText;
                info.Email = doc.SelectSingleNode("plugin/email").InnerText;
                info.Author = doc.SelectSingleNode("plugin/author").InnerText;
                info.Pubtime = TypeParse.StrToDateTime(doc.SelectSingleNode("plugin/publishdate").InnerText);
                info.Officesite = doc.SelectSingleNode("plugin/officesite").InnerText;
                info.Menu = doc.SelectSingleNode("plugin/menu").InnerText.Replace('\'', '\"');
            }
            catch
            {
                return "扩展包about.xml文件有错";
            }
            info.Package = newzip;

            if (FileUtil.FileExists(Utils.GetMapPath(readme)))
                info.Description = FileUtil.ReadFile(Utils.GetMapPath(readme));

            if (FileUtil.FileExists(Utils.GetMapPath(installsql)))
                info.Dbcreate = FileUtil.ReadFile(Utils.GetMapPath(installsql));

            if (FileUtil.FileExists(Utils.GetMapPath(uninstallsql)))
                info.Dbdelete = FileUtil.ReadFile(Utils.GetMapPath(uninstallsql));

            info.Id = Plugins.AddPlugin(info);
            return "";
        }

        /// <summary>
        /// 获取插件数据管理菜单
        /// </summary>
        /// <param name="plugindt"></param>
        /// <returns></returns>
        public static DataTable PluginMenu(DataTable plugindt, string pid)
        {
            DataTable pmenudt = new DataTable();
            pmenudt.Columns.Add("id", Type.GetType("System.String"));
            pmenudt.Columns.Add("name", Type.GetType("System.String"));
            pmenudt.Columns.Add("url", Type.GetType("System.String"));
            pmenudt.Columns.Add("pid", Type.GetType("System.String"));
            int tloop = 8;
            foreach (DataRow dr in plugindt.Rows)
            {
                if (dr["setup"].ToString() != "1") continue;

                DataRow ndrr = pmenudt.NewRow();
                string strid = Rand.Number(tloop++, true);
                ndrr["id"] = strid;
                ndrr["name"] = dr["name"].ToString();
                ndrr["url"] = "";
                ndrr["pid"] = pid;
                int loop = 1;
                foreach (Match mc in Regex.Matches(dr["menu"].ToString(), "name=\"([^\"]*)\" url=\"([^\"]*)\""))
                {
                    string name = mc.Groups[1].Value;
                    string url = mc.Groups[2].Value;
                    if (name == "" || url == "") continue;
                    DataRow ndr = pmenudt.NewRow();
                    ndr["name"] = name;
                    ndr["url"] = url;
                    ndr["pid"] = strid;
                    ndr["id"] = Rand.Number(tloop++);
                    pmenudt.Rows.Add(ndr);
                    loop++;
                }
                if (loop > 1)
                {
                    pmenudt.Rows.Add(ndrr);
                }
            }
            return pmenudt;
        }

        public static bool UrlStaticizeCreate(StaticizeInfo info)
        {
            try
            {
                if (info == null) return false;
                string url = info.Url.Replace("{weburl}", GeneralConfigs.GetConfig().Weburl + BaseConfigs.GetSitePath);
                FileUtil.CreateFolder(Utils.GetMapPath(info.Savepath));
                bool success = FileUtil.WriteFile(Utils.GetMapPath(BaseConfigs.GetSitePath + info.Savepath + "/" + info.Filename + "." + info.Suffix), Utils.GetPageContent(new Uri(url), info.Charset), System.Text.Encoding.GetEncoding(info.Charset));
                if (!success) return false;

                info.Maketime = DateTime.Now;
                return Contents.EditUrlStaticize(info);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), string.Format("生成自定义静态失败 ID:{0},Name:{1}", info.Id, info.Title), ex);
                return false;
            }
        }

        public static bool UrlStaticizeDelete(StaticizeInfo info)
        {
            if (info == null) return false;
            FileUtil.DeleteFile(Utils.GetMapPath(info.Savepath + "/" + info.Filename + "." + info.Suffix));
            return Contents.DelUrlStaticize(info.Id);
        }



        /// <summary>
        /// 根据获取联动下级值范围
        /// </summary>
        /// <param name="num"></param>
        /// <param name="maxvalue"></param>
        /// <param name="minvalue"></param>
        public static void SelectRange(float num, out float maxvalue, out float minvalue)
        {
            maxvalue = minvalue = 0;
            if (num < 0) return;
            minvalue = num;

            if (num % 500 == 0)
                maxvalue = num + 499;
            else
                maxvalue = num + 0.99f;
        }

        /// <summary>
        /// 生成联动缓存文件
        /// </summary>
        /// <param name="ename">标识</param>
        /// <param name="filepath">保存路径</param>
        /// <param name="dt">数据</param>
        /// <returns></returns>
        public static bool CreateSelectFile(string ename, string filepath, DataTable dt)
        {
            FileUtil.CreateFolder(filepath);
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("em_{0}s=new Array();\r\n", ename);
            ;
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("em_{0}s[{1}]='{2}';\r\n", ename, dr["value"].ToString().Trim(), dr["name"].ToString().Trim());
            }

            return FileUtil.WriteFile(filepath + ename + ".js", sb.ToString());
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string AddComment(CommentInfo info)
        {
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            info.Msg = Utils.RemoveHtml(info.Msg);

            if (config.Opencomment == 0)
            {
                return "评论已经关闭！";
            }
            if (config.Commentlogin == 1 && ConUtils.GetOnlineUser().Userid <= 0)
            {
                return "只有登陆后方可评论！";
            }

            if (config.Vcodemods.IndexOf("3") >= 0)
            {
                string cname = STARequest.GetString("cname");
                string vcode = STARequest.GetString("vcode");
                if (vcode == "" || vcode != Utils.GetCookie(cname))
                {
                    return "验证码有误！";
                }
                //if (cname != "")
                //    Utils.ClearCookie(cname);
            }

            if (config.Commentinterval > 0)
            {
                string cookname = "stacommentinterval";
                string tempval = Utils.GetCookie(cookname);
                if (tempval != "")
                {
                    int tempsecond = Utils.StrDateDiffSeconds(tempval, GeneralConfigs.GetConfig().Commentinterval);
                    if (tempsecond < 0)
                    {
                        return string.Format("您发帖太快了!过{0}秒再发！", tempsecond * -1);
                    }
                    else
                    {
                        Utils.WriteCookie(cookname, Utils.GetDateTime());
                    }
                }
                else
                {
                    Utils.WriteCookie(cookname, Utils.GetDateTime());
                }
            }

            if (config.Commentverify == 0 && !HasAuditWord(info.Title) && !HasAuditWord(info.Msg))
                info.Status = CommentStatus.通过;

            if (config.Commentlength > 0)
            {
                info.Msg = Utils.GetUnicodeSubString(info.Msg, config.Commentlength, "");
            }
            info.Title = BanWordFilter(info.Title);
            info.Msg = BanWordFilter(info.Msg);

            if (info.Replay > 0)
            {
                CommentInfo tinfo = Contents.GetComment(info.Replay);
                if (tinfo != null)
                {
                    string quotetpl = "<div class=\"quote\" cid=\"{0}\" uid=\"{1}\">{2}<div class=\"title clearfix\"><span class=\"left\">{3}网友 [<a href=\"{4}>{5}</a>] 的原帖</span><span class=\"right\">{6}</span></div><div class=\"content\">{7}</div></div>";
                    string uurl = tinfo.Uid <= 0 ? "javascript:;\"" : (BaseConfigs.GetSitePath + "/userinfo.aspx?id=" + tinfo.Uid + "\" target=\"_blank\"");
                    int floor = Regex.Matches(tinfo.Quote, "<div class=\\\"quote\\\"", RegexOptions.IgnoreCase).Count + 1;
                    if (config.Commentfloor > 0 && floor >= config.Commentfloor)
                    {
                        return string.Format("不能回复大于{0}楼的评论！", config.Commentfloor);
                    }

                    string quote = string.Format(quotetpl, tinfo.Id, tinfo.Uid, tinfo.Quote, config.Webname + (tinfo.City == "" ? "火星" : tinfo.City), uurl, (tinfo.Uid <= 0 ? "网友" : tinfo.Username), floor, Utils.StrFormatPtag(tinfo.Msg).Replace("\r\n", ""));
                    info.Quote = quote;
                }
            }
            info.Title = Utils.RemoveHtml(info.Title);
            info.Contitle = Utils.RemoveHtml(info.Contitle);

            IPLocation loc = IPSearch.GetIPLocation(info.Userip);
            info.City = loc.country + loc.area;//Stat.GetCityByIp(info.Userip, "");
            info.Msg = info.Msg.Replace("#nowtime", Utils.GetDateTime());
            return Contents.AddComment(info).ToString();
        }

        public static bool UpdateLoginStatus(UserInfo uinfo)
        {
            uinfo.Loginip = STARequest.GetIP();
            uinfo.Logintime = uinfo.Lastaction = DateTime.Now;
            return Users.EditUser(uinfo);
        }


        public static string StrRandomConvert(string original, DateTime _DateTime)
        {
            string year02 = _DateTime.ToString("yy");
            string year04 = _DateTime.ToString("yyyy");
            string month = _DateTime.ToString("MM");
            string day = _DateTime.ToString("dd");
            string hour = _DateTime.ToString("hh");
            string minute = (_DateTime.Minute).ToString();
            string second = (_DateTime.Second).ToString();

            original = original.Replace("{@year02}", year02);
            original = original.Replace("{@year04}", year04);
            original = original.Replace("{@month}", month);
            original = original.Replace("{@day}", day);
            original = original.Replace("{@second}", second);
            original = original.Replace("{@minute}", minute);
            original = original.Replace("{@hour}", hour);

            if (original.IndexOf("{@ram", 0) != -1)
            {
                for (int i = 0; i <= 9; i++)
                {
                    original = original.Replace("{@ram" + i + "}", Rand.Str(i));
                }
            }

            return original;
        }

        /// <summary>
        /// 根据联动值生成sql查询条件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetSqlWhereBySelectValue(string name, decimal val)
        {
            if (val <= 0)
            {
                return "";
            }
            else if (val % 500 == 0)
            {
                return string.Format(" cast(ext_{2} as decimal) between {0} and {1}", val, val + 499, name);
            }
            else if (val % 1 == 0)
            {
                return string.Format(" cast(ext_{2} as decimal) between {0} and {1}", val, val + 0.999M, name);
            }
            else
            {
                return string.Format(" ext_{1} = {0}", val, name);
            }
        }

        #region 获取相应扩展名的ContentType类型
        public static string GetContentType(string fileextname)
        {
            switch (fileextname)
            {
                #region 常用文件类型
                case "jpeg": return "image/jpeg";
                case "jpg": return "image/jpeg";
                case "js": return "application/x-javascript";
                case "jsp": return "text/html";
                case "gif": return "image/gif";
                case "htm": return "text/html";
                case "html": return "text/html";
                case "asf": return "video/x-ms-asf";
                case "avi": return "video/avi";
                case "bmp": return "application/x-bmp";
                case "asp": return "text/asp";
                case "wma": return "audio/x-ms-wma";
                case "wav": return "audio/wav";
                case "wmv": return "video/x-ms-wmv";
                case "ra": return "audio/vnd.rn-realaudio";
                case "ram": return "audio/x-pn-realaudio";
                case "rm": return "application/vnd.rn-realmedia";
                case "rmvb": return "application/vnd.rn-realmedia-vbr";
                case "xhtml": return "text/html";
                case "png": return "image/png";
                case "ppt": return "application/x-ppt";
                case "tif": return "image/tiff";
                case "tiff": return "image/tiff";
                case "xls": return "application/x-xls";
                case "xlw": return "application/x-xlw";
                case "xml": return "text/xml";
                case "xpl": return "audio/scpls";
                case "swf": return "application/x-shockwave-flash";
                case "torrent": return "application/x-bittorrent";
                case "dll": return "application/x-msdownload";
                case "asa": return "text/asa";
                case "asx": return "video/x-ms-asf";
                case "au": return "audio/basic";
                case "css": return "text/css";
                case "doc": return "application/msword";
                case "exe": return "application/x-msdownload";
                case "mp1": return "audio/mp1";
                case "mp2": return "audio/mp2";
                case "mp2v": return "video/mpeg";
                case "mp3": return "audio/mp3";
                case "mp4": return "video/mpeg4";
                case "mpa": return "video/x-mpg";
                case "mpd": return "application/vnd.ms-project";
                case "mpe": return "video/x-mpeg";
                case "mpeg": return "video/mpg";
                case "mpg": return "video/mpg";
                case "mpga": return "audio/rn-mpeg";
                case "mpp": return "application/vnd.ms-project";
                case "mps": return "video/x-mpeg";
                case "mpt": return "application/vnd.ms-project";
                case "mpv": return "video/mpg";
                case "mpv2": return "video/mpeg";
                case "wml": return "text/vnd.wap.wml";
                case "wsdl": return "text/xml";
                case "xsd": return "text/xml";
                case "xsl": return "text/xml";
                case "xslt": return "text/xml";
                case "htc": return "text/x-component";
                case "mdb": return "application/msaccess";
                case "zip": return "application/zip";
                case "rar": return "application/x-rar-compressed";
                #endregion

                case "*": return "application/octet-stream";
                case "001": return "application/x-001";
                case "301": return "application/x-301";
                case "323": return "text/h323";
                case "906": return "application/x-906";
                case "907": return "drawing/907";
                case "a11": return "application/x-a11";
                case "acp": return "audio/x-mei-aac";
                case "ai": return "application/postscript";
                case "aif": return "audio/aiff";
                case "aifc": return "audio/aiff";
                case "aiff": return "audio/aiff";
                case "anv": return "application/x-anv";
                case "awf": return "application/vnd.adobe.workflow";
                case "biz": return "text/xml";
                case "bot": return "application/x-bot";
                case "c4t": return "application/x-c4t";
                case "c90": return "application/x-c90";
                case "cal": return "application/x-cals";
                case "cat": return "application/vnd.ms-pki.seccat";
                case "cdf": return "application/x-netcdf";
                case "cdr": return "application/x-cdr";
                case "cel": return "application/x-cel";
                case "cer": return "application/x-x509-ca-cert";
                case "cg4": return "application/x-g4";
                case "cgm": return "application/x-cgm";
                case "cit": return "application/x-cit";
                case "class": return "java/*";
                case "cml": return "text/xml";
                case "cmp": return "application/x-cmp";
                case "cmx": return "application/x-cmx";
                case "cot": return "application/x-cot";
                case "crl": return "application/pkix-crl";
                case "crt": return "application/x-x509-ca-cert";
                case "csi": return "application/x-csi";
                case "cut": return "application/x-cut";
                case "dbf": return "application/x-dbf";
                case "dbm": return "application/x-dbm";
                case "dbx": return "application/x-dbx";
                case "dcd": return "text/xml";
                case "dcx": return "application/x-dcx";
                case "der": return "application/x-x509-ca-cert";
                case "dgn": return "application/x-dgn";
                case "dib": return "application/x-dib";
                case "dot": return "application/msword";
                case "drw": return "application/x-drw";
                case "dtd": return "text/xml";
                case "dwf": return "application/x-dwf";
                case "dwg": return "application/x-dwg";
                case "dxb": return "application/x-dxb";
                case "dxf": return "application/x-dxf";
                case "edn": return "application/vnd.adobe.edn";
                case "emf": return "application/x-emf";
                case "eml": return "message/rfc822";
                case "ent": return "text/xml";
                case "epi": return "application/x-epi";
                case "eps": return "application/x-ps";
                case "etd": return "application/x-ebx";
                case "fax": return "image/fax";
                case "fdf": return "application/vnd.fdf";
                case "fif": return "application/fractals";
                case "fo": return "text/xml";
                case "frm": return "application/x-frm";
                case "g4": return "application/x-g4";
                case "gbr": return "application/x-gbr";
                case "gcd": return "application/x-gcd";

                case "gl2": return "application/x-gl2";
                case "gp4": return "application/x-gp4";
                case "hgl": return "application/x-hgl";
                case "hmr": return "application/x-hmr";
                case "hpg": return "application/x-hpgl";
                case "hpl": return "application/x-hpl";
                case "hqx": return "application/mac-binhex40";
                case "hrf": return "application/x-hrf";
                case "hta": return "application/hta";
                case "htt": return "text/webviewhtml";
                case "htx": return "text/html";
                case "icb": return "application/x-icb";
                case "ico": return "application/x-ico";
                case "iff": return "application/x-iff";
                case "ig4": return "application/x-g4";
                case "igs": return "application/x-igs";
                case "iii": return "application/x-iphone";
                case "img": return "application/x-img";
                case "ins": return "application/x-internet-signup";
                case "isp": return "application/x-internet-signup";
                case "IVF": return "video/x-ivf";
                case "java": return "java/*";
                case "jfif": return "image/jpeg";
                case "jpe": return "application/x-jpe";
                case "la1": return "audio/x-liquid-file";
                case "lar": return "application/x-laplayer-reg";
                case "latex": return "application/x-latex";
                case "lavs": return "audio/x-liquid-secure";
                case "lbm": return "application/x-lbm";
                case "lmsff": return "audio/x-la-lms";
                case "ls": return "application/x-javascript";
                case "ltr": return "application/x-ltr";
                case "m1v": return "video/x-mpeg";
                case "m2v": return "video/x-mpeg";
                case "m3u": return "audio/mpegurl";
                case "m4e": return "video/mpeg4";
                case "mac": return "application/x-mac";
                case "man": return "application/x-troff-man";
                case "math": return "text/xml";
                case "mfp": return "application/x-shockwave-flash";
                case "mht": return "message/rfc822";
                case "mhtml": return "message/rfc822";
                case "mi": return "application/x-mi";
                case "mid": return "audio/mid";
                case "midi": return "audio/mid";
                case "mil": return "application/x-mil";
                case "mml": return "text/xml";
                case "mnd": return "audio/x-musicnet-download";
                case "mns": return "audio/x-musicnet-stream";
                case "mocha": return "application/x-javascript";
                case "movie": return "video/x-sgi-movie";
                case "mpw": return "application/vnd.ms-project";
                case "mpx": return "application/vnd.ms-project";
                case "mtx": return "text/xml";
                case "mxp": return "application/x-mmxp";
                case "net": return "image/pnetvue";
                case "nrf": return "application/x-nrf";
                case "nws": return "message/rfc822";
                case "odc": return "text/x-ms-odc";
                case "out": return "application/x-out";
                case "p10": return "application/pkcs10";
                case "p12": return "application/x-pkcs12";
                case "p7b": return "application/x-pkcs7-certificates";
                case "p7c": return "application/pkcs7-mime";
                case "p7m": return "application/pkcs7-mime";
                case "p7r": return "application/x-pkcs7-certreqresp";
                case "p7s": return "application/pkcs7-signature";
                case "pc5": return "application/x-pc5";
                case "pci": return "application/x-pci";
                case "pcl": return "application/x-pcl";
                case "pcx": return "application/x-pcx";
                case "pdf": return "application/pdf";
                case "pdx": return "application/vnd.adobe.pdx";
                case "pfx": return "application/x-pkcs12";
                case "pgl": return "application/x-pgl";
                case "pic": return "application/x-pic";
                case "pko": return "application/vnd.ms-pki.pko";
                case "pl": return "application/x-perl";
                case "plg": return "text/html";
                case "pls": return "audio/scpls";
                case "plt": return "application/x-plt";
                case "pot": return "application/vnd.ms-powerpoint";
                case "ppa": return "application/vnd.ms-powerpoint";
                case "ppm": return "application/x-ppm";
                case "pps": return "application/vnd.ms-powerpoint";
                case "pr": return "application/x-pr";
                case "prf": return "application/pics-rules";
                case "prn": return "application/x-prn";
                case "prt": return "application/x-prt";
                case "ps": return "application/x-ps";
                case "ptn": return "application/x-ptn";
                case "pwz": return "application/vnd.ms-powerpoint";
                case "r3t": return "text/vnd.rn-realtext3d";
                case "ras": return "application/x-ras";
                case "rat": return "application/rat-file";
                case "rdf": return "text/xml";
                case "rec": return "application/vnd.rn-recording";
                case "red": return "application/x-red";
                case "rgb": return "application/x-rgb";
                case "rjs": return "application/vnd.rn-realsystem-rjs";
                case "rjt": return "application/vnd.rn-realsystem-rjt";
                case "rlc": return "application/x-rlc";
                case "rle": return "application/x-rle";
                case "rmf": return "application/vnd.adobe.rmf";
                case "rmi": return "audio/mid";
                case "rmj": return "application/vnd.rn-realsystem-rmj";
                case "rmm": return "audio/x-pn-realaudio";
                case "rmp": return "application/vnd.rn-rn_music_package";
                case "rms": return "application/vnd.rn-realmedia-secure";
                case "rmx": return "application/vnd.rn-realsystem-rmx";
                case "rnx": return "application/vnd.rn-realplayer";
                case "rp": return "image/vnd.rn-realpix";
                case "rpm": return "audio/x-pn-realaudio-plugin";
                case "rsml": return "application/vnd.rn-rsml";
                case "rt": return "text/vnd.rn-realtext";
                case "rtf": return "application/msword";
                case "rv": return "video/vnd.rn-realvideo";
                case "sam": return "application/x-sam";
                case "sat": return "application/x-sat";
                case "sdp": return "application/sdp";
                case "sdw": return "application/x-sdw";
                case "sit": return "application/x-stuffit";
                case "slb": return "application/x-slb";
                case "sld": return "application/x-sld";
                case "slk": return "drawing/x-slk";
                case "smi": return "application/smil";
                case "smil": return "application/smil";
                case "smk": return "application/x-smk";
                case "snd": return "audio/basic";
                case "sol": return "text/plain";
                case "sor": return "text/plain";
                case "spc": return "application/x-pkcs7-certificates";
                case "spl": return "application/futuresplash";
                case "spp": return "text/xml";
                case "ssm": return "application/streamingmedia";
                case "sst": return "application/vnd.ms-pki.certstore";
                case "stl": return "application/vnd.ms-pki.stl";
                case "stm": return "text/html";
                case "sty": return "application/x-sty";
                case "svg": return "text/xml";
                case "tdf": return "application/x-tdf";
                case "tg4": return "application/x-tg4";
                case "tga": return "application/x-tga";
                case "tld": return "text/xml";
                case "top": return "drawing/x-top";
                case "tsd": return "text/xml";
                case "txt": return "text/plain";
                case "uin": return "application/x-icq";
                case "uls": return "text/iuls";
                case "vcf": return "text/x-vcard";
                case "vda": return "application/x-vda";
                case "vdx": return "application/vnd.visio";
                case "vml": return "text/xml";
                case "vpg": return "application/x-vpeg005";
                case "vsd": return "application/vnd.visio";
                case "vss": return "application/vnd.visio";
                case "vst": return "application/vnd.visio";
                case "vsw": return "application/vnd.visio";
                case "vsx": return "application/vnd.visio";
                case "vtx": return "application/vnd.visio";
                case "vxml": return "text/xml";
                case "wax": return "audio/x-ms-wax";
                case "wb1": return "application/x-wb1";
                case "wb2": return "application/x-wb2";
                case "wb3": return "application/x-wb3";
                case "wbmp": return "image/vnd.wap.wbmp";
                case "wiz": return "application/msword";
                case "wk3": return "application/x-wk3";
                case "wk4": return "application/x-wk4";
                case "wkq": return "application/x-wkq";
                case "wks": return "application/x-wks";
                case "wm": return "video/x-ms-wm";
                case "wmd": return "application/x-ms-wmd";
                case "wmf": return "application/x-wmf";
                case "wmx": return "video/x-ms-wmx";
                case "wmz": return "application/x-ms-wmz";
                case "wp6": return "application/x-wp6";
                case "wpd": return "application/x-wpd";
                case "wpg": return "application/x-wpg";
                case "wpl": return "application/vnd.ms-wpl";
                case "wq1": return "application/x-wq1";
                case "wr1": return "application/x-wr1";
                case "wri": return "application/x-wri";
                case "wrk": return "application/x-wrk";
                case "ws": return "application/x-ws";
                case "ws2": return "application/x-ws";
                case "wsc": return "text/scriptlet";
                case "wvx": return "video/x-ms-wvx";
                case "xdp": return "application/vnd.adobe.xdp";
                case "xdr": return "text/xml";
                case "xfd": return "application/vnd.adobe.xfd";
                case "xfdf": return "application/vnd.adobe.xfdf";
                case "xq": return "text/xml";
                case "xql": return "text/xml";
                case "xquery": return "text/xml";
                case "xwd": return "application/x-xwd";
                case "x_b": return "application/x-x_b";
                case "x_t": return "application/x-x_t";
            }
            return "application/octet-stream";
        }
        #endregion
    }
}
