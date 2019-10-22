using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Data;
using System.Drawing.Drawing2D;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Control;
using STA.Cache;

namespace STA.Core
{
    public class ConUtils
    {


        public static bool DelChannel(GeneralConfigInfo config, int id)
        {
            DataTable dt = Contents.GetChannelDataTable("id,parentid");
            EmptyChannel(id);
            DataRow[] drs = dt.Select("parentid=" + id.ToString());
            foreach (DataRow dr in drs)
            {

                _DeleteChannel(config, dt, dr["id"].ToString());
                _DelChannel2(config, TypeParse.StrToInt(dr["id"]));
            }
            _DelChannel2(config, id);
            Contents.DeleteChannel(id);
            return true;
        }

        private static bool _DeleteChannel(GeneralConfigInfo config, DataTable dt, string id)
        {
            DataRow[] drs = dt.Select("parentid=" + id);
            foreach (DataRow r in drs)
            {
                _DeleteChannel(config, dt, r["id"].ToString());
                _DelChannel2(config, TypeParse.StrToInt(r["id"]));
            }
            return true;
        }

        /// <summary>
        /// 删除频道静态和数据库数据
        /// </summary>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool _DelChannel2(GeneralConfigInfo config, int id)
        {
            DelChannelHtmlFile(GeneralConfigs.GetConfig(), id);
            return Contents.DeleteChannel(id) > 0;
        }

        public static bool ChannelInherit(GeneralConfigInfo config, ChannelInfo chl)
        {
            DataTable dt = Contents.GetChannelDataTable("id,parentid");
            DataRow[] drs = dt.Select("parentid=" + chl.Id.ToString());
            foreach (DataRow dr in drs)
            {

                _ChannelInherit(config, chl, dt, dr["id"].ToString());
                _ChannelInherit2(config, chl, TypeParse.StrToInt(dr["id"]));
            }
            return true;
        }

        private static bool _ChannelInherit(GeneralConfigInfo config, ChannelInfo ichl, DataTable dt, string id)
        {
            DataRow[] drs = dt.Select("parentid=" + id);
            foreach (DataRow r in drs)
            {
                _ChannelInherit(config, ichl, dt, r["id"].ToString());
                _ChannelInherit2(config, ichl, TypeParse.StrToInt(r["id"]));
            }
            return true;
        }

        public static bool _ChannelInherit2(GeneralConfigInfo config, ChannelInfo ichl, int id)
        {
            ChannelInfo chl = Contents.GetChannel(id);
            if (chl == null) return false;

            chl.Conrule = ichl.Conrule;
            chl.Listrule = ichl.Listrule;
            chl.Covertem = ichl.Covertem;
            chl.Listem = ichl.Listem;
            chl.Contem = ichl.Contem;
            chl.Typeid = ichl.Typeid;
            chl.Viewgroup = ichl.Viewgroup;
            chl.Viewcongroup = ichl.Viewcongroup;
            chl.Ipaccess = ichl.Ipaccess;
            chl.Ipdenyaccess = ichl.Ipdenyaccess;
            chl.Ishidden = ichl.Ishidden;
            chl.Ispost = ichl.Ispost;

            if (chl.Ctype != 3)
            {
                chl.Filename = ichl.Filename;
                chl.Savepath = ichl.Savepath + "/" + Utils.ConvertE(chl.Name).ToLower();
            }

            return Contents.EditChannel(chl) > 0;

        }

        public static bool DeleteMenu(int id)
        {
            DataTable dt = Menus.GetMenuTable(-1);
            Menus.DelMenu(id);
            DataRow[] drs = dt.Select("parentid=" + id.ToString());
            foreach (DataRow dr in drs)
            {
                Menus.DelMenu(TypeParse.StrToInt(dr["id"]));
                _DeleteMenu(dt, dr["id"].ToString());
            }
            return true;
        }

        private static bool _DeleteMenu(DataTable dt, string id)
        {
            DataRow[] drs = dt.Select("parentid=" + id);
            foreach (DataRow r in drs)
            {
                Menus.DelMenu(TypeParse.StrToInt(r["id"]));
                _DeleteMenu(dt, r["id"].ToString());
            }
            return true;
        }

        public static bool DeleteArea(int id)
        {
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            DataTable dt = Contents.GetAreaDataTable();
            Contents.DelArea(id);
            DataRow[] drs = dt.Select("parentid=" + id.ToString());
            foreach (DataRow dr in drs)
            {
                Contents.DelArea(TypeParse.StrToInt(dr["id"]));
                _DeleteArea(config, dt, dr["id"].ToString());
            }
            return true;
        }

        private static bool _DeleteArea(GeneralConfigInfo Config, DataTable dt, string id)
        {
            DataRow[] drs = dt.Select("parentid=" + id);
            foreach (DataRow r in drs)
            {
                Contents.DelArea(TypeParse.StrToInt(r["id"]));
                _DeleteArea(Config, dt, r["id"].ToString());
            }
            return true;
        }

        /// <summary>
        /// 当频道删除时，清理频道下的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool EmptyChannel(int id)
        {
            DataTable dt = Contents.GetContentsByChannelId(id); //频道下内容

            Contents.EditContentsWhereChannelDel(id);
            GeneralConfigInfo config = GeneralConfigs.GetConfig();

            foreach (DataRow dr in dt.Rows)
                DelContent(config, TypeParse.StrToInt(dr["id"]), TypeParse.StrToInt(dr["typeid"]));
            return true;
        }

        public static bool EmptyUserContent(int uid)
        {
            DataTable ids = DatabaseProvider.GetInstance().GetContentIds(uid);

            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            foreach (DataRow dr in ids.Rows)
            {
                DelContent(config, TypeParse.StrToInt(dr["id"], 0), TypeParse.StrToInt(dr["typeid"]));
            }
            return true;
        }


        /// <summary>
        /// 删除用户，并删除他的相关数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool DelUserData(int uid)
        {
            EmptyUserContent(uid);
            Users.DelUser(uid);
            return true;
        }

        public static bool DelContype(int id)
        {
            ContypeInfo info = Contents.GetContype(id);
            if (info.System == 1) return false;
            return Contents.DeleteContype(id) > 0;
        }

        /// <summary>
        /// 重建模型字段列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BulidContypeFields(int id)
        {
            ContypeInfo info = Contents.GetContype(id);
            if (info == null) return false;
            DataTable dt = Contents.GetTableField(info.Extable);
            string fields = string.Empty;
            if (dt == null) return false;
            if (dt.Rows.Count <= 2)
            {
                fields = string.Empty;
            }
            else
            {
                for (int i = 2; i < dt.Rows.Count; i++)
                {
                    fields += dt.Rows[i]["name"].ToString() + ",";
                }
                fields = fields.Substring(0, fields.Length - 1);
            }
            info.Fields = fields;
            return Contents.EditContype(info) > 0;
        }

        public static DataTable BulidChannelList(int typeid, DataTable cdt, DropDownTreeList ddtl)
        {
            cdt = RemoveTableRow(cdt, "ishidden", "1");
            DataTable ncdt = cdt.Copy();
            ddtl.BuildTree(cdt, "name", "id");
            return ncdt;
            //if (typeid <= 0) return ncdt;
            //for (int i = 0; i < ddtl.Items.Count; i++)
            //{
            //    string id = ddtl.Items[i].Value;
            //    DataRow[] drs = cdt.Select("id=" + id);
            //    if (drs == null || drs.Length <= 0) continue;
            //    string ctype = drs[0]["ctype"].ToString().Trim();
            //    if (typeid.ToString() == drs[0]["typeid"].ToString().Trim() && ctype == "1") continue;
            //    ddtl.Items[i].Text += "[X]";
            //    ddtl.Items[i].Value += "[X]";
            //    ncdt.Rows.Remove(ncdt.Select("id=" + id)[0]);
            //}
            //return ncdt;
        }

        /// <summary>
        /// 获取符合投稿条件的频道
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="cdt"></param>
        /// <returns></returns>
        public static DataTable GetChlsForContribute(int typeid, DataTable cdt)
        {
            cdt = RemoveTableRow(cdt, "ishidden", "1");
            //cdt = RemoveTableRow(cdt, "ctype", "2,3");
            cdt = RemoveTableRow(cdt, "ispost", "0");
            for (int i = cdt.Rows.Count - 1; i >= 0; i--)
            {
                int chl_typeid = TypeParse.StrToInt(cdt.Rows[i]["typeid"]);
                if (typeid == chl_typeid) continue;
                cdt.Rows.Remove(cdt.Rows[i]);
            }
            return cdt;
        }

        public static string GetChannelFamily(int id, string fm)
        {
            int pid = Contents.ChannelParentId(id);
            if (pid == 0) return fm;
            fm += pid.ToString() + ",";
            return GetChannelFamily(pid, fm);
        }

        #region 创建自定义字段控件
        public static string BulidContentControlHtml(ContypefieldInfo info)
        {
            string ret = string.Empty;
            string script = string.Empty;
            string append = string.Empty;
            string tipscript = "$('#~cusname~').poshytip({className: 'tip-yellowsimple',alignTo:'target',alignX: 'center',alignY: 'top', offsetX: 5,offsetY:5 });";
            XmlDocument doc = XMLUtil.LoadDocument(Utils.GetMapPath(BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/xml/contem.config"));
            string basetem = doc.SelectSingleNode("control/base").InnerText;
            string selecttem = doc.SelectSingleNode("control/select").InnerText;
            basetem = basetem.Substring(8, basetem.Length - 12);
            string fieldtype = info.Fieldtype.Trim();
            string fieldname = info.Fieldname.Trim();
            if (info.Vinnertext == "")
            {
                if (Utils.InArray(fieldtype, "char|nchar|varchar|nvarchar|ntext|int|float|datetime|selectfile|decimal(18,2)".Split('|')))
                {
                    if (info.Fieldvalue != "")
                        info.Defvalue = info.Fieldvalue;
                    if (fieldtype == "datetime")
                        info.Defvalue = TypeParse.StrToDateTime(info.Defvalue).ToString("yyyy-MM-dd HH:mm:ss");
                    ret = basetem.Replace("~name~", info.Desctext).Replace("~cusname~", fieldname).Replace("~tiptext~", info.Tiptext.Replace("\"", "'")).Replace("~defval~", info.Defvalue);
                    if (info.Tiptext != string.Empty)
                    {
                        script += tipscript.Replace("~cusname~", fieldname);
                    }
                    if (fieldtype == "datetime")
                    {
                        script += "$(\"#" + fieldname + "\").click(function () { WdatePicker({ isShowWeek: true ,dateFmt:'yyyy-MM-dd HH:mm:ss'}) });";
                    }
                    if (fieldtype == "selectfile")
                    {
                        string selectid = "selectfile_" + fieldname;
                        append += " <span id=\"" + selectid + "\" class=\"selectbtn\">选择</span> <span onclick=\"window.open($('#" + fieldname + "').val(),'_blank')\" class=\"selectbtn\">打开</span>";
                        script += "RegSelectFilePopWin(\"" + selectid + "\", \"文件选择器\", \"root=" + GeneralConfigs.GetConfig().Attachsavepath + "&fullname=1&cltmed=1&fele=" + fieldname + "\", \"click\");";
                    }
                    append += "<script type=\"text/javascript\">$(function(){" + script + "});</script>";
                    ret = ret.Replace("~append~", append).Replace("~null~", info.Isnull.ToString());
                }
                else if (Utils.InArray(fieldtype, "select,radio,checkbox,stepselect,editor"))
                {
                    if (fieldtype == "select")
                    {
                        ret = "<select id=\"" + fieldname + "\" name=\"" + fieldname + "\" title=\"" + info.Tiptext.Replace("\"", "'") + "\">";
                        //ret += string.Format("<option value=''>{0}</option>", "请选择");
                        foreach (string v in info.Defvalue.Split(','))
                        {
                            if (v.Trim() == "") continue;
                            string select = "";
                            if (info.Fieldvalue == v)
                                select = "selected='selected'";
                            ret += string.Format("<option value='{0}' {1}>{0}</option>", v, select);
                        }
                        ret += "</select>";
                    }
                    else if (fieldtype == "radio")
                    {
                        ret = "<span id=\"" + fieldname + "\" title=\"" + info.Tiptext.Replace("\"", "'") + "\">";
                        int tloop = 0;
                        foreach (string v in info.Defvalue.Split(','))
                        {
                            if (v.Trim() == "") continue;
                            string select = "";
                            if ((info.Fieldvalue != "" && info.Fieldvalue == v) || (info.Fieldvalue == "" && tloop == 0))
                                select = "checked='checkeded'";

                            ret += string.Format("<input type=\"radio\" value='{0}' name=\"{1}\" {2}/>{0}&nbsp;&nbsp;", v, fieldname, select);
                            tloop++;
                        }
                        ret += "</span>";
                    }
                    else if (fieldtype == "checkbox")
                    {
                        ret = "<span id=\"" + fieldname + "\" title=\"" + info.Tiptext.Replace("\"", "'") + "\">";
                        foreach (string v in info.Defvalue.Split(','))
                        {
                            if (v.Trim() == "") continue;
                            string select = "";
                            if (Utils.InArray(v, info.Fieldvalue))
                                select = "checked='checkeded'";
                            ret += string.Format("<input type=\"checkbox\" value='{0}' name=\"{1}\" {2}/>{0}&nbsp;&nbsp;", v, fieldname, select);
                        }
                        ret += "</span>";
                    }
                    else if (fieldtype == "stepselect")
                    {
                        string tempfieldname = fieldname.Substring(4);

                        info.Fieldvalue = info.Fieldvalue.Trim();
                        if (info.Fieldvalue.Trim() == "") info.Fieldvalue = "0";

                        ret = string.Format("<input type='hidden' id='{0}' name='{0}' value='{1}' />", fieldname, info.Fieldvalue);
                        ret += string.Format("<span id='span_{0}'></span> <span id='span_{0}_son'></span> <span id='span_{0}_sec'></span>", tempfieldname);
                        ret += string.Format("<script language='javascript' type='text/javascript' src='../../sta/data/select/{0}.js'></script>", tempfieldname);
                        ret += string.Format("<script language='javascript' type='text/javascript'>MakeTopSelect('{0}', {1});</script>", tempfieldname, info.Fieldvalue);
                    }
                    else if (fieldtype == "editor")
                    {

                        ret = string.Format("<textarea name='{0}' id='{0}'>{1}</textarea>\r\n<script type='text/javascript'>CKEDITOR.replace('{0}', config.editorset);</script>", fieldname, Utils.HtmlEncode(info.Fieldvalue));
                    }
                    if (info.Tiptext != string.Empty && fieldtype != "editor")
                    {
                        ret += "<script type=\"text/javascript\">$(function(){" + tipscript.Replace("~cusname~", fieldname) + "});</script>";
                    }
                    ret = selecttem.Replace("~name~", info.Desctext).Replace("~control~", ret);
                }
            }
            else
            {
                ret = info.Vinnertext.Replace("~name~", info.Desctext).Replace("~field~", info.Fieldname).Replace("~text~", info.Fieldvalue);
            }
            return ret;
        }
        #endregion

        public static void ClearUserCookie()
        {
            ClearUserCookie("sta");
        }

        public static void ClearUserCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            string cookieDomain = GeneralConfigs.GetConfig().Domaincookie;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static OnlineUserInfo WriteUserCookie(UserInfo userinfo, int expires)
        {
            if (userinfo == null)
                userinfo = new UserInfo();

            OnlineUserInfo olinfo = new OnlineUserInfo();
            olinfo.Userid = userinfo.Id;
            olinfo.Nickname = userinfo.Nickname;
            olinfo.Username = Utils.UrlEncode(userinfo.Username.ToString());
            //olinfo.Password = Utils.UrlEncode(userinfo.Password.ToString());
            olinfo.Safecode = Utils.UrlEncode(userinfo.Safecode);
            olinfo.Adminid = userinfo.Adminid;
            olinfo.Groupid = userinfo.Groupid;
            olinfo.Admingroupname = Utils.UrlEncode(userinfo.Admingroupname);
            olinfo.Groupname = Utils.UrlEncode(userinfo.Groupname);
            olinfo.Expires = expires;
            olinfo.Ip = STARequest.GetIP();
            return UserUtils.UpdateOnlineUserInfo(olinfo, expires);

        }

        /// <summary>
        /// 获得系统cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <returns>值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["sta"] != null && HttpContext.Current.Request.Cookies["sta"][strName] != null)
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies["sta"][strName].ToString());
            return "";
        }

        public static OnlineUserInfo GetOnlineUser()
        {
            OnlineUserInfo info = new OnlineUserInfo();
            info.Userid = TypeParse.StrToInt(GetCookie("userid"), -1);
            info.Username = Utils.UrlDecode(GetCookie("username").Trim());
            info.Adminid = TypeParse.StrToInt(GetCookie("adminid"), 0);
            info.Groupid = TypeParse.StrToInt(GetCookie("groupid"), 0);
            info.Nickname = Utils.UrlDecode(GetCookie("nickname").Trim());
            info.Groupname = Utils.UrlDecode(GetCookie("groupname").Trim());
            //info.Password = Utils.UrlDecode(GetCookie("password").Trim());
            info.Safecode = Utils.UrlDecode(GetCookie("safecode").Trim());
            info.Admingroupname = Utils.UrlDecode(GetCookie("admingroupname").Trim());
            info.Lastsearchtime = GetCookie("lastsearchtime").Trim();
            info.Expires = TypeParse.StrToInt(GetCookie("expires"), 0);
            return info;
        }


        /// <summary>
        /// 是否为有效域
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(string host)
        {
            if (host.IndexOf(".") == -1)
                return false;
            return new Regex(@"^\d+$").IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }


        /// <summary>
        /// 返回用户安全问题答案的存储数据
        /// </summary>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns></returns>
        public static string GetUserSecques(int questionid, string answer)
        {
            if (questionid > 0)
                return Utils.MD5(answer + Utils.MD5(questionid.ToString())).Substring(15, 8);
            return "";
        }

        /// <summary>
        /// 写系统cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["sta"];
            if (cookie == null)
            {
                cookie = new HttpCookie("sta");
                cookie.Values[strName] = Utils.UrlEncode(strValue);
            }
            else
            {
                cookie.Values[strName] = Utils.UrlEncode(strValue);
                if (HttpContext.Current.Request.Cookies["sta"]["expires"] != null)
                {
                    int expires = Utils.StrToInt(HttpContext.Current.Request.Cookies["sta"]["expires"].ToString(), 0);
                    if (expires > 0)
                    {
                        cookie.Expires = DateTime.Now.AddMinutes(Utils.StrToInt(HttpContext.Current.Request.Cookies["sta"]["expires"].ToString(), 0));
                    }
                }
            }

            string cookieDomain = GeneralConfigs.GetConfig().Domaincookie.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void InsertLog(int admintype, int uid, string username, int admingroupid, string admingroupname, string ip, string action, string remark)
        {
            if (GeneralConfigs.GetConfig().Adminlogs > 0 || admintype == 1)
            {
                Users.AddAdminLog(new AdminLogInfo(admintype, uid, username, admingroupid, admingroupname, ip, DateTime.Now, action, remark));
            }
        }

        public static void UpdateLikeset(LikesetInfo info)
        {
            try
            {
                string filename = Utils.GetMapPath(BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/xml/user_" + info.Uid + ".config");
                if (!FileUtil.FileExists(filename)) return;
                XmlDocument doc = XMLUtil.LoadDocument(filename);
                doc.SelectSingleNode("data/Systemstyle").InnerText = info.Systemstyle;
                doc.SelectSingleNode("data/Managelistcount").InnerText = info.Managelistcount.ToString();
                doc.SelectSingleNode("data/Msgtip").InnerText = info.Msgtip.ToString();
                doc.SelectSingleNode("data/Overlay").InnerText = info.Overlay.ToString();
                doc.SelectSingleNode("data/Fastmenucount").InnerText = info.Fastmenucount.ToString();
                //doc.SelectSingleNode("data/Popwinbgcolor").InnerText = info.Popwinbgcolor;
                doc.Save(filename);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "用户配置文件不存在，或者参数缺失！ ", ex);
            }
        }

        public static LikesetInfo GetLikeset(int uid)
        {
            try
            {
                STACache cache = STACache.GetCacheService();
                LikesetInfo info = cache.RetrieveObject(CacheKeys.SET + "LikeSet" + uid) as LikesetInfo;
                if (info == null)
                {
                    info = new LikesetInfo();
                    string filename = Utils.GetMapPath(BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "/xml/user_" + uid + ".config");
                    if (!FileUtil.FileExists(filename)) return info;
                    XmlDocument doc = XMLUtil.LoadDocument(filename);
                    info.Uid = uid;
                    info.Systemstyle = doc.SelectSingleNode("data/Systemstyle").InnerText;
                    //info.Popwinbgcolor = doc.SelectSingleNode("data/Popwinbgcolor").InnerText;
                    info.Managelistcount = TypeParse.StrToInt(doc.SelectSingleNode("data/Managelistcount").InnerText, 20);
                    info.Msgtip = TypeParse.StrToInt(doc.SelectSingleNode("data/Msgtip").InnerText, 0);
                    info.Overlay = TypeParse.StrToInt(doc.SelectSingleNode("data/Overlay").InnerText, 0);
                    info.Fastmenucount = TypeParse.StrToInt(doc.SelectSingleNode("data/Fastmenucount").InnerText, 15);
                }
                cache.AddObject(CacheKeys.SET + "LikeSet" + uid, info);
                return info;
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "用户配置文件不存在，或者参数缺失！ ", ex);
                return new LikesetInfo();
            }
            finally
            {

            }
        }

        /// <summary>
        /// 获取图片缩略图地址
        /// </summary>
        /// <param name="filename">/file/1.jpg</param>
        /// <param name="checkexist">是否检查图片是否存在</param>
        /// <returns>/file/1.thumb.jpg</returns>
        //public static string GetImgThumb(string filename, bool checkexist)
        //{
        //    if (Utils.IsImgHttp(filename) || !Utils.IsImgFilename(filename) || (checkexist && !FileUtil.FileExists(Utils.GetMapPath(filename)))) return filename;
        //    string ext = Utils.GetFileExtName(filename);
        //    string thumb = filename.Substring(0, filename.Length - ext.Length) + "thumb." + ext;

        //    if (!checkexist) return thumb;

        //    if (FileUtil.FileExists(Utils.GetMapPath(thumb))) return thumb;
        //    else return filename;
        //}

        /// <summary>
        /// 是否图片缩略图
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        //public static bool IsImgThumb(string filename)
        //{
        //    if (!Utils.IsImgFilename(filename)) return false;
        //    string[] array = filename.Trim().Split('.');
        //    Array.Reverse(array);
        //    return array[1].ToString() == "thumb";
        //}


        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="watermarkFilename">水印文件名</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void AddImageSignPic(Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            Graphics g = Graphics.FromImage(img);
            //设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image watermark = new Bitmap(watermarkFilename);

            if (watermark.Height >= img.Height || watermark.Width >= img.Width)
                return;

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5F;
            if (watermarkTransparency >= 1 && watermarkTransparency <= 100)
                transparency = (watermarkTransparency / 100.0F);


            float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
            }

            g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                    ici = codec;
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
                quality = 80;

            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
                img.Save(filename, ici, encoderParams);
            else
                img.Save(filename);

            g.Dispose();
            img.Dispose();
            watermark.Dispose();
            imageAttributes.Dispose();
        }


        /// <summary>
        /// 增加图片文字水印
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void AddImageSignText(Image img, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
        {
            Graphics g = Graphics.FromImage(img);
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);

            float xpos = 0;
            float ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (float)img.Width * (float).01;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 2:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = (float)img.Height * (float).01;
                    break;
                case 3:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 4:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 5:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 6:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 7:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 8:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 9:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
            }

            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                    ici = codec;
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
                quality = 80;

            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
                img.Save(filename, ici, encoderParams);
            else
                img.Save(filename);

            g.Dispose();
            img.Dispose();
        }

        /// <summary>
        /// 获得附件存放目录
        /// </summary>
        /// <returns></returns>
        private static string GetAttachmentPath(GeneralConfigInfo config)
        {
            StringBuilder saveDir = new StringBuilder("");
            //0:按年存入不同目录(不推荐) 1:按年/月存入不同目录 2:按年/月/日存入不同目录 3:按年/月/日/时存入不同目录
            if (config.Attachsaveway == 0)
            {
                saveDir.Append(DateTime.Now.ToString("yyyy"));
                saveDir.Append(Path.DirectorySeparatorChar);
            }
            if (config.Attachsaveway == 1)
            {
                saveDir.Append(DateTime.Now.ToString("yyyy"));
                saveDir.Append(Path.DirectorySeparatorChar);
                saveDir.Append(DateTime.Now.ToString("MM"));
                saveDir.Append(Path.DirectorySeparatorChar);
            }
            else if (config.Attachsaveway == 2)
            {
                saveDir.Append(DateTime.Now.ToString("yyyy"));
                saveDir.Append(Path.DirectorySeparatorChar);
                saveDir.Append(DateTime.Now.ToString("MM"));
                saveDir.Append(Path.DirectorySeparatorChar);
                saveDir.Append(DateTime.Now.ToString("dd"));
                saveDir.Append(Path.DirectorySeparatorChar);
            }
            else if (config.Attachsaveway == 3)
            {
                saveDir.Append(DateTime.Now.ToString("yyyy"));
                saveDir.Append(Path.DirectorySeparatorChar);
                saveDir.Append(DateTime.Now.ToString("MM"));
                saveDir.Append(Path.DirectorySeparatorChar);
                saveDir.Append(DateTime.Now.ToString("dd"));
                saveDir.Append(Path.DirectorySeparatorChar);
                saveDir.Append(DateTime.Now.ToString("hh"));
                saveDir.Append(Path.DirectorySeparatorChar);
            }
            return saveDir.ToString();
        }


        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="filename">文件名如123.jpg</param>
        /// <param name="nameway">文件命名方式</param>
        /// <returns></returns>
        private static string GetAttachmentName(int nameway, string filename)
        {
            //0:保持原文件名不变  1:自动随机字符串  2:随机字符串和原文件名组合
            if (nameway == 0)
                return filename;
            else if (nameway == 1)
                return Rand.RamTime() + "." + Utils.GetFileExtName(filename);
            else
                return filename.Substring(0, filename.LastIndexOf('.')) + "_" + Rand.RamTime() + "." + Utils.GetFileExtName(filename);
        }


        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="maxallowfilecount">最大允许的上传文件个数</param>
        /// <param name="filekey">File控件的Key(即Name属性)</param>
        /// <param name="savedir">自定义文件保存的文件夹,不填写按系统配置</param>
        /// <param name="acceptfiletype">自定义文件类型限制*表示不限制</param>
        /// <param name="maxfilesize">自定义文件大小限制单位：KB</param>
        /// <param name="savename">自定义保存文件名,不填写按系统配置 eg:123</param>
        /// <param name="overfile">如果存在相同文件名是否覆盖</param>
        /// <param name="config"></param>
        /// <param name="watermarkposition">水印位置;0 则不添加</param>
        /// <param name="thumbsize">如果缩小图片尺寸，缩小的比例(宽高) 留空或者0则不缩小</param>
        /// <returns>文件信息结构</returns>
        public static AttachmentInfo[] SaveRequestFiles(int maxallowfilecount, string acceptfiletype, int maxfilesize, string savedir, string savename, int overfile, int watermarkposition, string thumbsize, string filekey, GeneralConfigInfo config)
        {
            string reasonsize = "文件大小超过了当前大小限制！最大可上传" + Utils.FormatBytesStr(maxfilesize * 1024);
            string reasontype = "允许上传的文件类型为：" + acceptfiletype;
            int saveFileCount = 0;
            int fCount = HttpContext.Current.Request.Files.Count;

            for (int i = 0; i < fCount; i++)
            {
                if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
                {
                    saveFileCount++;
                }
            }
            AttachmentInfo[] attachments = saveFileCount > 0 ? new AttachmentInfo[saveFileCount] : null;
            if (saveFileCount > maxallowfilecount)
                return attachments;
            saveFileCount = 0;
            for (int i = 0; i < fCount; i++)
            {
                if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
                {
                    string filename = Path.GetFileName(HttpContext.Current.Request.Files[i].FileName);
                    string fileextname = Utils.CutString(filename, filename.LastIndexOf(".") + 1).ToLower();
                    string filetype = HttpContext.Current.Request.Files[i].ContentType.ToLower();
                    int filesize = HttpContext.Current.Request.Files[i].ContentLength;
                    string newfilename = "";
                    attachments[saveFileCount] = new AttachmentInfo();
                    attachments[saveFileCount].Attachment = filename;
                    if (acceptfiletype != "")
                    {
                        if (acceptfiletype != "*" && !Utils.InArray(fileextname, acceptfiletype))
                        {
                            attachments[saveFileCount].Noupload = reasontype;
                        }
                    }
                    else
                    {
                        if (!(Utils.InArray(fileextname, config.Attachimgtype)
                            || Utils.InArray(fileextname, config.Attachsofttype)
                            || Utils.InArray(fileextname, config.Attachmediatype)))
                        {
                            attachments[saveFileCount].Noupload = reasontype;
                        }
                    }
                    if (maxfilesize > 0)
                    {
                        if (filesize > maxfilesize * 1024)
                        {
                            attachments[saveFileCount].Noupload = reasonsize;
                        }
                    }
                    else
                    {
                        if (filesize > config.Attachimgmaxsize * 1024 * 1024
                            || Utils.InArray(fileextname, config.Attachsofttype)
                            || Utils.InArray(fileextname, config.Attachmediatype))
                        {
                            attachments[saveFileCount].Noupload = reasonsize;
                        }
                    }
                    if (savedir == "")
                        savedir = Utils.GetMapPath(BaseConfigs.GetSitePath + config.Attachsavepath + "/") + GetAttachmentPath(config);
                    newfilename = savedir + (savename == "" ? GetAttachmentName(config.Attachnameway, filename) : (savename + "." + fileextname));
                    if (FileUtil.FileExists(newfilename) && overfile != 1)
                        attachments[saveFileCount].Noupload = "已存在相同命名的文件，请修改文件名后再上传！";
                    if (attachments[saveFileCount].Noupload == "")
                    {
                        if (!Directory.Exists(savedir))
                            FileUtil.CreateFolder(savedir);
                        try
                        {

                            if ((fileextname == "bmp" || fileextname == "jpg" || fileextname == "jpeg" || fileextname == "png"))
                            {
                                Image img = Image.FromStream(HttpContext.Current.Request.Files[i].InputStream);
                                attachments[saveFileCount].Width = img.Width;
                                attachments[saveFileCount].Height = img.Height;

                                if (attachments[saveFileCount].Noupload == "")
                                {
                                    if (thumbsize != "" && thumbsize != "0" && thumbsize.Split(',').Length > 1)
                                    {
                                        int width = TypeParse.StrToInt(thumbsize.Split(',')[0], 150);
                                        int height = TypeParse.StrToInt(thumbsize.Split(',')[1], 150);
                                        if (width <= 0)
                                        {
                                            width = 150;
                                        }
                                        if (height <= 0)
                                        {
                                            height = 150;
                                        }
                                        img = STA.Common.ImgHelper.Thumbnail.GetThumbnailImage(img, width, height, false);
                                    }

                                    if (watermarkposition == 0)
                                    {
                                        img.Save(newfilename);
                                        //HttpContext.Current.Request.Files[i].SaveAs(newfilename);
                                    }
                                    else
                                    {
                                        int imgminwidth = 0, imgminheight = 0;
                                        bool iswatermark = true;
                                        if (config.Waterlimitsize != "0" && config.Waterlimitsize != "")
                                        {
                                            imgminwidth = TypeParse.StrToInt(config.Waterlimitsize.Split('*')[0]);
                                            imgminheight = TypeParse.StrToInt(config.Waterlimitsize.Split('*')[1]);
                                            if ((imgminwidth > 0 && img.Width < imgminwidth) || imgminheight > 0 && img.Height < imgminheight)
                                                iswatermark = false;
                                        }

                                        if (config.Watertype == 1 && File.Exists(Utils.GetMapPath(config.Waterimg)) && iswatermark)
                                        {
                                            AddImageSignPic(img, newfilename, Utils.GetMapPath(config.Waterimg), watermarkposition, config.Waterquality, config.Wateropacity);
                                        }
                                        else
                                        {
                                            string watermarkText;
                                            watermarkText = config.Watertext.Replace("{1}", Utils.GetDate());
                                            watermarkText = watermarkText.Replace("{2}", Utils.GetTime());
                                            watermarkText = watermarkText.Replace("{3}", config.Webname);
                                            watermarkText = watermarkText.Replace("{4}", config.Weburl.Replace("http://", "") + BaseConfigs.GetSitePath);
                                            AddImageSignText(img, newfilename, watermarkText, watermarkposition, config.Waterquality, config.Waterfontname, config.Waterfontsize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                HttpContext.Current.Request.Files[i].SaveAs(newfilename);
                            }
                        }
                        catch
                        {
                            //当上传目录和临时文件夹都没有上传的文件时
                            if (!Utils.FileExists(newfilename))
                            {
                                HttpContext.Current.Request.Files[i].SaveAs(newfilename);
                            }
                        }
                    }
                    attachments[saveFileCount].Filesize = filesize; ;
                    attachments[saveFileCount].Filename = newfilename.Replace(Utils.GetMapPath(BaseConfigs.GetSitePath + "/"), "/").Replace('\\', '/');
                    attachments[saveFileCount].Attachment = filename.Substring(0, filename.LastIndexOf('.'));
                    attachments[saveFileCount].Description = attachments[saveFileCount].Fileext = fileextname;
                    attachments[saveFileCount].Filetype = filetype;

                    saveFileCount++;
                }
            }
            return attachments;
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="source">格式如E:\source</param>
        /// <param name="target">格式如：E:\target</param>
        public static void Movefolder(string source, string target)
        {
            if (source == target) return;
            FileUtil.CreateFolder(target);
            List<FileItem> filelist = FileUtil.GetFiles(source);
            foreach (FileItem fitem in filelist)
            {
                string tempfilename = fitem.FullName.Replace(Utils.GetMapPath("/"), "/").Replace('\\', '/'); //原文件虚拟路径
                string temptargetfilename = target + fitem.FullName.Replace(source, ""); //目标文件物理路径
                string temptargetvirname = temptargetfilename.Replace(Utils.GetMapPath("/"), "/").Replace('\\', '/'); //目标文件虚拟路径
                string temptargetfilepath = temptargetfilename.Substring(0, temptargetfilename.LastIndexOf('\\') + 1); //目录文件所在目标目录
                if (!FileUtil.DirExists(temptargetfilepath)) //目标目录不存在则创建
                    FileUtil.CreateFolder(temptargetfilepath);
                AttachmentInfo tempainfo = Contents.GetAttachment(tempfilename);
                if (tempainfo != null)
                {
                    tempainfo.Filename = temptargetvirname;
                    Contents.EditAttachment(tempainfo);
                }
                FileUtil.MoveFile(fitem.FullName, temptargetfilename);
            }
            FileUtil.DeleteFolder(source, true);
        }

        public static string DataTableToString(DataTable dt, int columnindex, string split)
        {
            string ret = string.Empty;
            if (columnindex + 1 >= dt.Columns.Count)
                columnindex = dt.Columns.Count - 1;
            foreach (DataRow dr in dt.Rows)
            {
                ret += dr[columnindex].ToString().Trim() + split;
            }
            return ret == "" ? "" : ret.Substring(0, ret.Length - split.Length);
        }

        public static string DataTableToString(DataTable dt, string columnname, string split)
        {
            string ret = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                ret += dr[columnname].ToString().Trim() + split;
            }
            return ret == "" ? "" : ret.Substring(0, ret.Length - split.Length);
        }

        public static void DelAttachment(string vfilename)
        {
            //if (Utils.IsImgFilename(vfilename))
            //    FileUtil.DeleteFile(Utils.GetMapPath(GetImgThumb(vfilename, true)));

            FileUtil.DeleteFile(Utils.GetMapPath(vfilename));
            AttachmentInfo info = Contents.GetAttachment(vfilename);
            if (info != null)
                Contents.DelAttachment(info.Id);
        }

        public static bool DelAttachment(int id, string filename)
        {
            if (filename == "")
            {
                AttachmentInfo info = Contents.GetAttachment(id);
                if (info != null) filename = info.Filename;
            }
            if (filename != "")
            {
                //if (Utils.IsImgFilename(filename))
                //    FileUtil.DeleteFile(Utils.GetMapPath(GetImgThumb(filename, true)));
                FileUtil.DeleteFile(Utils.GetMapPath(filename));
            }

            return Contents.DelAttachment(id);
        }


        public static DataTable GetPhotoList(string imgs)
        {
            DataTable imgdt = new DataTable();
            imgdt.Columns.Add("id", Type.GetType("System.String"));
            imgdt.Columns.Add("name", Type.GetType("System.String"));
            imgdt.Columns.Add("url", Type.GetType("System.String"));
            foreach (Match img in Regex.Matches(imgs, "<img id=\"(\\d+)\" url=\"([^\"]+)\" name=\"([^\"]*)\"/>"))
            {
                DataRow imgdr = imgdt.NewRow();
                imgdr["id"] = img.Groups[1].Value;
                imgdr["url"] = img.Groups[2].Value;
                imgdr["name"] = img.Groups[3].Value;
                imgdt.Rows.Add(imgdr);
            }
            return imgdt;
        }

        public static DataTable GetSoftList(string softs)
        {
            DataTable softdt = new DataTable();
            softdt.Columns.Add("id", Type.GetType("System.String"));
            softdt.Columns.Add("name", Type.GetType("System.String"));
            softdt.Columns.Add("url", Type.GetType("System.String"));
            foreach (Match img in Regex.Matches(softs, "<soft id=\"(\\d+)\" url=\"([^\"]*)\" name=\"([^\"]*)\"/>"))
            {
                DataRow softdr = softdt.NewRow();
                softdr["id"] = img.Groups[1].Value;
                softdr["url"] = img.Groups[2].Value;
                softdr["name"] = img.Groups[3].Value;
                softdt.Rows.Add(softdr);
            }
            return softdt;
        }

        public static DataTable GetMagazineList(string softs)
        {
            DataTable magdt = new DataTable();
            magdt.Columns.Add("orderid", Type.GetType("System.String"));
            magdt.Columns.Add("attid", Type.GetType("System.String"));
            magdt.Columns.Add("name", Type.GetType("System.String"));
            magdt.Columns.Add("url", Type.GetType("System.String"));
            foreach (Match img in Regex.Matches(softs, "<item name=\"([^\"]*)\" url=\"([^\"]*)\" orderid=\"(\\d+)\" attid=\"(\\d+)\"/>"))
            {
                DataRow magdr = magdt.NewRow();
                magdr["attid"] = img.Groups[4].Value;
                magdr["orderid"] = img.Groups[3].Value;
                magdr["url"] = img.Groups[2].Value;
                magdr["name"] = img.Groups[1].Value;
                magdt.Rows.Add(magdr);
            }
            return magdt;
        }

        /// <summary>
        /// 内容过滤规则列表
        /// </summary>
        public static DataTable GetContentFilterList(string confilter)
        {
            DataTable matchdt = new DataTable();
            matchdt.Columns.Add("match", Type.GetType("System.String"));
            matchdt.Columns.Add("replace", Type.GetType("System.String"));
            foreach (Match replace in Regex.Matches(confilter, "<item match=\"([^\"]*)\" \\s*replace=\"([^\"]*)\"\\s*/>"))
            {
                DataRow matchdr = matchdt.NewRow();
                matchdr["match"] = replace.Groups[1].Value;
                matchdr["replace"] = replace.Groups[2].Value;
                matchdt.Rows.Add(matchdr);
            }
            return matchdt;
        }

        /// <summary>
        /// 数据库信息采集 字段匹配列表
        /// </summary>
        public static DataTable GetCollectMatchList(string softs)
        {
            DataTable matchdt = new DataTable();
            matchdt.Columns.Add("name", Type.GetType("System.String"));
            matchdt.Columns.Add("sname", Type.GetType("System.String"));
            foreach (Match img in Regex.Matches(softs, "<item name=\"([^\"]*)\" sname=\"([^\"]*)\"/>"))
            {
                DataRow matchdr = matchdt.NewRow();
                matchdr["name"] = img.Groups[1].Value;
                matchdr["sname"] = img.Groups[2].Value;
                matchdt.Rows.Add(matchdr);
            }
            return matchdt;
        }

        /// <summary>
        /// 获取站点采集配置规则
        /// </summary>
        /// <param name="attrs">参数列表</param>
        /// <param name="setting">内容</param>
        /// <returns></returns>
        public static Hashtable GetCollectRuleSet(string attrs, string setting)
        {
            if (attrs == "") return null;

            Hashtable hbset = new Hashtable(50);
            foreach (string attr in attrs.Split(','))
            {
                if (attr == "") continue;
                hbset[attr] = TypeParse.ObjToString(Regex.Match(setting, string.Format("setting_{0}_start_([\\s\\S]*)_setting_{0}_end", attr)).Groups[1]);
            }
            return hbset;
        }

        public static DataTable RemoveTableRow(DataTable dt, string columnname, string values)
        {
            if (dt == null && dt.Rows.Count <= 0) return dt;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (!Utils.InArray(dt.Rows[i][columnname].ToString().Trim(), values)) continue;
                dt.Rows.Remove(dt.Rows[i]);
            }
            return dt;
        }


        public static DataTable GetEnumTable(Type type)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            foreach (int item in Enum.GetValues(type))
            {
                DataRow dr = dt.NewRow();
                dr["id"] = item.ToString();
                dr["name"] = Enum.GetName(type, item);
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static int EditAd(AdInfo info)
        {
            string savepath = GeneralConfigs.GetConfig().Attachsavepath + "/ad/";
            if (info.Id == 0)
            {
                info.Filename = savepath + Rand.RamTime() + ".js";
                WriteAdFile(info);
                return Contents.AddAd(info);
            }
            else
            {
                WriteAdFile(info);
                return Contents.EditAd(info);
            }
        }

        public static bool WriteAdFile(AdInfo info)
        {
            if (info == null) return false;

            if (info.Filename != "")
            {
                string adpath = (BaseConfigs.GetSitePath + info.Filename).Substring(0, (BaseConfigs.GetSitePath + info.Filename).LastIndexOf('/'));
                FileUtil.CreateFolder(Utils.GetMapPath(adpath));
            }

            return FileUtil.WriteFile(Utils.GetMapPath(BaseConfigs.GetSitePath + info.Filename), GetAdScript(info));
        }

        public static string GetAdHtml(AdInfo info)
        {
            string[] pmlist = Utils.SplitString(info.Paramarray, ("*sta*"));
            if (info.Adtype == AdType.文字)
            {
                string fsize = pmlist[2].Trim() == "" ? "" : ("font-size:" + pmlist[2] + ";");
                string fcolor = pmlist[3].Trim() == "" ? "" : ("color:#" + pmlist[3] + ";");
                return string.Format("<a href=\"{0}\" style=\"{1}{2}\" title=\"{3}\" target=\"_blank\">{3}</a>", pmlist[1].Trim() == "" ? "javascript:void(0);" : pmlist[1], fsize, fcolor, pmlist[0]);
            }
            else if (info.Adtype == AdType.图片)
            {
                return string.Format("<a href=\"{0}\" title=\"{1}\" target=\"_blank\"><img src=\"{2}\"" + " alt=\"{1}\" width=\"{3}\" height=\"{4}\"/></a>", pmlist[1].Trim() == "" ? "javascript:void(0);" : pmlist[1], pmlist[2], pmlist[0], pmlist[3], pmlist[4]);
            }
            else if (info.Adtype == AdType.Flash)
            {
                return string.Format("<embed width=\"{0}\" height=\"{1}\" src=\"{2}\" type=\"application/x-shockwave-flash\"></embed>", pmlist[1], pmlist[2], pmlist[0]);
            }
            else
            {
                return info.Paramarray;
            }
        }

        public static string GetAdScript(AdInfo info)
        {
            if (info.Status == AdStatus.不生效)
            {
                return "";
                //return Utils.HtmlToJs(info.Outdate);
            }
            else
            {
                if (info.Status == AdStatus.永远生效)
                {
                    return Utils.HtmlToJs(GetAdHtml(info));
                }
                else if (info.Status == AdStatus.生效)
                {
                    StringBuilder script = new StringBuilder(10000);
                    script.AppendFormat("var dte = [new Date(\"{0}\"),new Date(\"{1}\")];\r\n", info.Startdate.ToString("yyyy/MM/dd").Replace('-', '/'), info.Enddate.ToString("yyyy/MM/dd").Replace('-', '/'));
                    script.Append("var today = new Date();\r\n");
                    script.Append("if(today >= dte[0] && today <= dte[1]){\r\n");
                    script.Append("~script~ \r\n}else{\r\n");
                    script.Append(Utils.HtmlToJs(info.Outdate, "    ") + "\r\n}");
                    return script.ToString().Replace("~script~", Utils.HtmlToJs(GetAdHtml(info), "    "));
                }
            }
            return "";
        }

        /// <summary>
        /// 收缩数据库
        /// </summary>
        /// <param name="shrinksize">收缩大小</param>
        /// <param name="dbname">数据库名</param>
        public static string ShrinkDataBase(string strDbName, string size)
        {
            try
            {
                string shrinksize = !Utils.StrIsNullOrEmpty(size) ? size : "0";
                Databases.ShrinkDataBase(shrinksize, strDbName);
                return "yes";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 清空数据库日志
        /// </summary>
        /// <param name="dbname"></param>
        public static string ClearDBLog(string dbname)
        {
            try
            {
                Databases.ClearDBLog(dbname);
                return "yes";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string UserLikeXmlPath(int userid, BaseConfigInfo config)
        {
            return Utils.GetMapPath(string.Format(config.Sitepath + config.Adminpath + "/xml/user_{0}.config", userid));
        }

        /// <summary>
        /// 重置内容保存路径信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static ContentInfo EditContentPath(ContentInfo info)
        {
            if (info.Filename == "")
            {
                string conrule = "", channelpath = "";
                if (info.Channelid > 0)
                {
                    ChannelInfo cinfo = Contents.GetChannel(info.Channelid);
                    if (cinfo != null)
                    {
                        conrule = cinfo.Conrule;
                        channelpath = cinfo.Savepath;
                    }
                }
                string savename = Urls.ConRuleConvert(conrule, channelpath, info.Channelid, info.Id, info.Addtime);

                int tempidex = savename.LastIndexOf('/');
                if (info.Savepath == "")
                {
                    info.Savepath = tempidex == -1 ? "/" : savename.Substring(0, tempidex);
                }
                if (info.Filename == "")
                {
                    info.Filename = (tempidex + 1) == savename.Length ? info.Id.ToString() : savename.Substring(tempidex + 1);
                }
            }
            return info;
        }


        /// <summary>
        /// 把两个时间差，用人性化时间表示
        /// </summary>
        /// <param name="date">被比较的时间</param>
        /// <param name="currentDateTime">目标时间</param>
        /// <returns></returns>
        public static string ConvertDateTime(string date, DateTime currentDateTime)
        {
            if (Utils.StrIsNullOrEmpty(date))
                return "";

            DateTime time;
            if (!DateTime.TryParse(date, out time))
                return date;

            string result = "";

            if (DateDiff("year", time, currentDateTime) > 0)
            {
                return DateDiff("year", time, currentDateTime) + "年前";
            }
            else if (DateDiff("month", time, currentDateTime) < 12 && DateDiff("month", time, currentDateTime) > 0)
            {
                return DateDiff("month", time, currentDateTime) + "月前";
            }
            else if (DateDiff("day", time, currentDateTime) < 30 && DateDiff("day", time, currentDateTime) > 0)
            {
                return DateDiff("day", time, currentDateTime) + "天前";
            }
            else if (DateDiff("hour", time, currentDateTime) < 24)
            {
                if (DateDiff("hour", time, currentDateTime) > 0)
                    return DateDiff("hour", time, currentDateTime) + "小时前";

                if (DateDiff("minute", time, currentDateTime) > 0)
                    return DateDiff("minute", time, currentDateTime) + "分钟前";

                //if (DateDiff("second", time, currentDateTime) >= 0)
                //    return DateDiff("second", time, currentDateTime) + "秒前";
                else
                    return "刚才";
            }
            else
                result = time.ToString("yyyy-MM-dd HH:mm");
            return result;
        }

        /// <summary>
        /// 两个时间的差值，可以为秒，小时，天，分钟
        /// </summary>
        /// <param name="Interval">需要得到的时差方式</param>
        /// <param name="StartDate">起始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static long DateDiff(string Interval, DateTime StartDate, DateTime EndDate)
        {

            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case "second":
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case "minute":
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case "hour":
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case "day":
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case "week":
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case "month":
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case "quarter":
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case "year":
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }

        /// <summary>
        /// 转换时间为人性化时间
        /// </summary>
        /// <param name="strdate">被转换的时间</param>
        /// <returns></returns>
        public static string HumanizeTime(string date)
        {
            return ConvertDateTime(date, DateTime.Now);
        }

        public static void MakeSitemap()
        {
            STA.Core.Publish.SitemapCreate.Create("daily", "0.8", GeneralConfigs.GetConfig().Sitemapconcount);
        }

        /// <summary>
        /// 把内容放入回收站并删除静态文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static bool DelContent(GeneralConfigInfo config, int id, int typeid)
        {
            if (id <= 0) return false;
            ConUtils.DelConHtmlFile(config, id, typeid);
            return Contents.PutContentRecycle(id);
        }

        /// <summary>
        /// 获取内容HTML路径
        /// </summary>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ContentHtmlPath(GeneralConfigInfo config, int id, int page)
        {
            DataRow drinfo = Caches.GetContent(id, GeneralConfigs.GetConfig().Cacheinterval * 60);
            string savename = "";
            if (drinfo != null)
            {
                savename = drinfo["savepath"].ToString() + "/" + drinfo["filename"].ToString().Trim();
            }
            else
            {
                savename = Contents.ContentSaveName(id);
            }
            if (savename.EndsWith("/")) savename += id.ToString();

            if (page > 1)
                savename += "_" + page.ToString();
            return BaseConfigs.GetSitePath + config.Htmlsavepath + savename + config.Suffix;
        }

        public static string PageHtmlPath(GeneralConfigInfo config, int id)
        {
            string savename = Contents.PageSaveName(id);
            if (savename.EndsWith("/")) savename += id.ToString();
            return BaseConfigs.GetSitePath + config.Htmlsavepath + savename + config.Suffix;
        }

        public static string ChannelHtmlPath(GeneralConfigInfo config, int id, int page)
        {
            DataRow[] channel = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + id.ToString());
            if (channel == null || channel.Length == 0) return "";

            string savepath, filename, pagerule;
            filename = channel[0]["filename"].ToString().Trim();
            if (filename == "") filename = id.ToString();

            savepath = channel[0]["savepath"].ToString().Trim();
            pagerule = channel[0]["listrule"].ToString().Trim();
            int ctype = TypeParse.StrToInt(channel[0]["ctype"], 1);
            if (page > 1)
            {
                if (ctype == 1)
                {
                    return BaseConfigs.GetSitePath + config.Htmlsavepath + (Urls.ChlListRuleConver(pagerule, savepath, id, page.ToString())) + config.Suffix;
                }
                else if (ctype == 2)
                {
                    return BaseConfigs.GetSitePath + config.Htmlsavepath + savepath + "/" + filename + "_" + page.ToString() + config.Suffix;
                }
            }
            return BaseConfigs.GetSitePath + config.Htmlsavepath + savepath + "/" + filename + config.Suffix;
        }

        /// <summary>
        /// 获取内容页数
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int ConPageCount(string content)
        {
            string pattern = "(\\[STA:PAGE(=([^\\s]*))?\\])";
            return Regex.Matches(content, pattern).Count + 1;
        }

        /// <summary>
        /// 获取频道索引页数
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ctype"></param>
        /// <param name="id"></param>
        /// <param name="listcount"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int ChannelPageCount(GeneralConfigInfo config, int ctype, int id, int listcount, string content)
        {
            if (ctype == 1)
            {
                listcount = listcount <= 0 ? config.Listinfocount : listcount;
                int infocount = Contents.ContentCountByChannelId(id);
                return (listcount + infocount) / listcount;
            }
            else if (ctype == 2)
            {
                return config.Contentpage == 1 ? ConPageCount(content) : 1;
            }
            return 0;
        }

        public static ContentInfo GetContent(int id)
        {
            ContentInfo simpleInfo = GetSimpleContent(id);
            if (simpleInfo == null) return null;
            return Contents.GetContent(id, simpleInfo.Typeid);
        }

        /// <summary>
        /// 删除文档静态文件,不包括分页
        /// </summary>
        /// <param name="config"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static void DelConHtmlFile(GeneralConfigInfo config, int id, int typeid)
        {
            ContentInfo info = Contents.GetShortContent(id);
            int pagecount = ConPageCount(info.Content);

            for (int i = 1; i <= pagecount; i++)
            {
                FileUtil.DeleteFile(Utils.GetMapPath(ContentHtmlPath(config, id, i)));
            }

            if (typeid == 0)
            {
                config.Dynamiced = 0;
                DataTable gdt = Contents.GetSpecgroups(id);
                foreach (DataRow dr in gdt.Rows)
                {
                    string path = Urls.SpecGroup(config, id, TypeParse.StrToInt(dr["id"]));
                    if (config.Withweburl > 0 && path.IndexOf(config.Weburl) >= 0)
                        path = path.Replace(config.Weburl, "");
                    FileUtil.DeleteFile(Utils.GetMapPath(path));
                }
            }
        }

        /// <summary>
        /// 删除单页模型静态文件,不包括分页
        /// </summary>
        /// <param name="config"></param>
        /// <param name="id"></param>
        public static void DelPageHtmlFile(GeneralConfigInfo config, int id)
        {
            FileUtil.DeleteFile(Utils.GetMapPath(PageHtmlPath(config, id)));
        }

        /// <summary>
        /// 删除频道静态文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="id"></param>
        public static void DelChannelHtmlFile(GeneralConfigInfo config, int id)
        {
            try
            {
                ChannelInfo info = Contents.GetChannel(id);
                int pagecount = ChannelPageCount(config, info.Ctype, info.Id, info.Listcount, info.Content);
                for (int i = 1; i <= pagecount; i++)
                {
                    FileUtil.DeleteFile(Utils.GetMapPath(ChannelHtmlPath(config, id, i)));
                }
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "删除频道静态出错！频道ID: " + id.ToString(), ex);
            }
        }

        ///// <summary>
        ///// 获取标签字符串列表
        ///// </summary>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static string GetHotTagString(int count)
        //{
        //    string str = "";
        //    foreach (DataRow dr in Tags.GetHotTags(count).Rows)
        //    {
        //        str += dr["name"].ToString() + "(" + dr["count"].ToString() + "),";
        //    }
        //    return str.EndsWith(",") ? str.Substring(0, str.Length - 1) : str;
        //}

        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns>验证码</returns>
        public static string CreateAuthStr(int len)
        {
            int number;
            StringBuilder checkCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    checkCode.Append((char)('0' + (char)(number % 10)));
                else
                    checkCode.Append((char)('A' + (char)(number % 26)));
            }
            return checkCode.ToString();
        }

        /// <summary>
        /// 过滤html标签,不包含内容
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tags">a,p</param>
        /// <returns></returns>
        public static string FilterHtmlTags(string html, string tags)
        {
            if (html.Trim() == "" || tags.Trim() == "") return html;
            foreach (string s in tags.Split(','))
            {
                if (s.Trim() == "") continue;
                html = Utils.FilterHtmlTag(html, s.Trim());
            }
            return html;
        }

        /// <summary>
        /// 接受通过页面请求的投票
        /// </summary>
        /// <returns>如果返回为空则投票成功,反之返回投票失败信息</returns>
        public static bool SetRequestVote(out string retstr)
        {
            retstr = "";
            int topicid = STARequest.GetInt("voteitem", 0);
            string optids = STARequest.GetString("item");
            string vcode = STARequest.GetString("vcode").Trim().ToLower();
            string realname = STARequest.GetString("realname");
            string idcard = STARequest.GetString("idcard");
            string phone = STARequest.GetString("phone");
            VoteConfigInfo voteinfo = VoteConfigs.GetConfig();
            VotetopicInfo vtinfo = Votes.GetVotetopic(topicid);
            if (vtinfo == null)
            {
                retstr = "主题无效,无法进行投票！";
                return false;
            }

            DateTime starttime = TypeParse.StrToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + voteinfo.Timeslot.Split('|')[0] + ":00");
            DateTime endtime = TypeParse.StrToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + voteinfo.Timeslot.Split('|')[1] + ":00");
            OnlineUserInfo onlineuser = GetOnlineUser();
            if (Utils.InIPArray(STARequest.GetIP(), voteinfo.Forbidips.Split('|')))
            {
                retstr = "IP已被拉入黑名单,如有疑问请联系管理员！";
                return false;
            }
            else if (!(DateTime.Now >= starttime && DateTime.Now <= endtime))
            {
                retstr = string.Format("现在不能进行投票,请在{0}-{1}在时间段再进行投票！", voteinfo.Timeslot.Split('|')[0], voteinfo.Timeslot.Split('|')[1]);
                return false;
            }
            else if ((voteinfo.Login == 1 || vtinfo.Islogin == 1) && onlineuser.Userid <= 0)
            {
                retstr = "只有登录用户才可以投票！";
                return false;
            }
            else if ((voteinfo.Infoinput == 1 || vtinfo.Isinfo == 1) && (realname == "" || idcard == "" || phone == ""))
            {
                retstr = "投票前个人信息必须填写完整！";
                return false;
            }
            else if ((voteinfo.Vcode == 1 || vtinfo.Isvcode == 1) && vcode != Utils.GetCookie("vote" + topicid.ToString()))
            {
                retstr = "验证码输入有误！";
                return false;
            }
            else if (optids == "")
            {
                retstr = "请选择至少一项,再进行投票！";
                return false;
            }
            if (vtinfo.Endtime < DateTime.Now)
            {
                retstr = vtinfo.Endtext.Trim() == "" ? "该投票已过期！" : vtinfo.Endtext;
                return false;
            }
            else if (voteinfo.Timeinterval > 0 && Votes.VoteRecordIpTimeInterval(STARequest.GetIP(), topicid, voteinfo.Timeinterval) > 0)
            {
                retstr = "您已经投过票了！";
                return false;
            }

            Votes.Vote(vtinfo.Id, optids);
            VoterecordInfo vrinfo = new VoterecordInfo();
            vrinfo.Topicid = vtinfo.Id;
            vrinfo.Topicname = vtinfo.Name;
            vrinfo.Optionids = optids;
            vrinfo.Userid = onlineuser.Userid;
            vrinfo.Username = onlineuser.Username;
            vrinfo.Userip = STARequest.GetIP();
            vrinfo.Realname = realname;
            vrinfo.Idcard = idcard;
            vrinfo.Phone = phone;
            Votes.AddVoterecord(vrinfo);
            retstr = vtinfo.Voted.Trim() == "" ? "投票成功！" : vtinfo.Voted;
            return true;
        }

        /// <summary>
        /// 返回当前页面是否是跨站提交
        /// </summary>
        /// <returns>当前页面是否是跨站提交</returns>
        public static bool IsCrossSitePost()
        {
            // 如果不是提交则为true
            if (!STARequest.IsPost())
                return true;

            return IsCrossSitePost(STARequest.GetUrlReferrer(), STARequest.GetHost());
        }

        /// <summary>
        /// 判断是否是跨站提交
        /// </summary>
        /// <param name="urlReferrer">上个页面地址</param>
        /// <param name="host">论坛url</param>
        /// <returns>bool</returns>
        public static bool IsCrossSitePost(string urlReferrer, string host)
        {
            if (urlReferrer.Length < 7)
                return true;

            Uri u = new Uri(urlReferrer);

            return u.Host != host;
        }

        public static ContentInfo GetSimpleContent(int id)
        {
            DataTable dt = Caches.GetSimpleContentTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = dt.Select("id=" + id);
            if (drs.Length == 0) return null;
            ContentInfo cinfo = new ContentInfo();
            cinfo.Id = TypeParse.StrToInt(drs[0]["id"]);
            cinfo.Adduser = TypeParse.StrToInt(drs[0]["adduser"]);
            cinfo.Iscomment = byte.Parse(drs[0]["iscomment"].ToString());
            cinfo.Title = drs[0]["title"].ToString();
            cinfo.Img = drs[0]["img"].ToString();
            cinfo.Typeid = short.Parse(drs[0]["typeid"].ToString());
            cinfo.Channelid = TypeParse.StrToInt(drs[0]["channelid"]);
            cinfo.Filename = drs[0]["filename"].ToString().Trim();
            cinfo.Savepath = drs[0]["savepath"].ToString().Trim();
            cinfo.Orderid = TypeParse.StrToInt(drs[0]["orderid"]);
            cinfo.Credits = TypeParse.StrToInt(drs[0]["credits"]);
            cinfo.Status = byte.Parse(drs[0]["status"].ToString());
            return cinfo;
        }

        public static ContypeInfo GetSimpleContype(int id)
        {
            DataTable dt = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = dt.Select("id=" + id);
            if (drs.Length == 0) return null;

            ContypeInfo info = new ContypeInfo();
            info.Id = short.Parse(TypeParse.ObjToString(drs[0]["id"]));
            info.Open = byte.Parse(TypeParse.ObjToString(drs[0]["open"]));
            info.System = byte.Parse(TypeParse.ObjToString(drs[0]["system"]));
            info.Ename = TypeParse.ObjToString(drs[0]["ename"]);
            info.Name = TypeParse.ObjToString(drs[0]["name"]);
            info.Maintable = TypeParse.ObjToString(drs[0]["maintable"]);
            info.Extable = TypeParse.ObjToString(drs[0]["extable"]);
            info.Addtime = TypeParse.StrToDateTime(drs[0]["addtime"]);
            info.Fields = drs[0]["fields"].ToString();
            info.Bgaddmod = drs[0]["bgaddmod"].ToString();
            info.Bgeditmod = drs[0]["bgeditmod"].ToString();
            info.Bglistmod = drs[0]["bglistmod"].ToString();
            info.Addmod = drs[0]["addmod"].ToString();
            info.Editmod = drs[0]["editmod"].ToString();
            info.Listmod = drs[0]["listmod"].ToString();
            info.Orderid = TypeParse.StrToInt(drs[0]["orderid"], 0);

            return info;
        }

        public static ChannelInfo GetSimpleChannel(int id)
        {
            //id,typeid,parentid,name,ctype,img,orderid,ipdenyaccess,ipaccess
            DataTable dt = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = dt.Select("id=" + id);
            if (drs.Length == 0) return null;
            ChannelInfo cinfo = new ChannelInfo();
            cinfo.Id = TypeParse.StrToInt(drs[0]["id"]);
            cinfo.Typeid = short.Parse(drs[0]["typeid"].ToString());
            cinfo.Parentid = TypeParse.StrToInt(drs[0]["parentid"]);
            cinfo.Img = drs[0]["img"].ToString();
            cinfo.Addtime = TypeParse.StrToDateTime(drs[0]["addtime"]);
            cinfo.Name = TypeParse.ObjToString(drs[0]["name"]);
            cinfo.Ctype = byte.Parse(drs[0]["ctype"].ToString());
            cinfo.Filename = drs[0]["filename"].ToString().Trim();
            cinfo.Savepath = drs[0]["savepath"].ToString().Trim();
            cinfo.Conrule = TypeParse.ObjToString(drs[0]["conrule"]);
            cinfo.Listrule = TypeParse.ObjToString(drs[0]["listrule"]);
            cinfo.Moresite = byte.Parse(TypeParse.ObjToString(drs[0]["moresite"]));
            cinfo.Siteurl = TypeParse.ObjToString(drs[0]["siteurl"]);
            cinfo.Ispost = byte.Parse(TypeParse.ObjToString(drs[0]["ispost"]));
            cinfo.Ishidden = byte.Parse(TypeParse.ObjToString(drs[0]["ishidden"]));
            cinfo.Orderid = TypeParse.StrToInt(drs[0]["orderid"]);
            cinfo.Listcount = TypeParse.StrToInt(drs[0]["listcount"]);
            cinfo.Viewgroup = TypeParse.ObjToString(drs[0]["viewgroup"]);
            cinfo.Viewcongroup = TypeParse.ObjToString(drs[0]["viewcongroup"]);
            cinfo.Ipdenyaccess = TypeParse.ObjToString(drs[0]["ipdenyaccess"]);
            cinfo.Ipaccess = TypeParse.ObjToString(drs[0]["ipaccess"]);
            return cinfo;
        }

        public static TagInfo GetTag(int id)
        {
            TagInfo cinfo = new TagInfo();
            DataTable dt = Caches.GetTagTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = dt.Select("id=" + id);
            if (drs.Length == 0) return cinfo;

            cinfo.Id = TypeParse.StrToInt(drs[0]["id"]);
            cinfo.Name = drs[0]["name"].ToString();
            cinfo.Count = TypeParse.StrToInt(drs[0]["count"]);
            cinfo.Addtime = TypeParse.StrToDateTime(drs[0]["addtime"]);
            return cinfo;
        }

        /// <summary>
        /// 是否是动态url
        /// </summary>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <param name="con"></param>
        /// <param name="chl"></param>
        /// <returns></returns>
        public static bool IsDynamicedCon(GeneralConfigInfo config, int id, ref ContentInfo con, ref ChannelInfo chl)
        {
            if (config.Dynamiced >= 1) return true;

            con = GetSimpleContent(id);
            if (con == null) return true;

            if (con.Channelid > 0)
            {
                chl = GetSimpleChannel(con.Channelid);
                if (chl != null && (chl.Viewcongroup != "" || chl.Ipaccess != "" || chl.Ipdenyaccess != "")) return true;
            }
            if (con.Credits > 0 || con.Status != 2 || con.Orderid < -1000) return true;
            return false;
        }

        public static bool IsDynamicedCon(GeneralConfigInfo config, int id)
        {
            ContentInfo con = null;
            ChannelInfo chl = null;
            return IsDynamicedCon(config, id, ref con, ref chl);
        }

        public static bool IsDynamicedChl(GeneralConfigInfo config, int id, ref ChannelInfo chl)
        {
            if (config.Dynamiced >= 1) return true;

            chl = GetSimpleChannel(id);

            if (chl == null || chl.Viewgroup != "" || chl.Ipaccess != "" || chl.Ipdenyaccess != "") return true;
            else return false;
        }

        public static bool IsDynamicedChl(GeneralConfigInfo config, int id)
        {
            ChannelInfo chl = null;
            return IsDynamicedChl(config, id, ref chl);
        }
    }
}
