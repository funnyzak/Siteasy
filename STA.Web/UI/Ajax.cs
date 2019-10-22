using System;
using System.Data;
using System.Web;
using System.Text;
using System.Xml;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Data;
using STA.Config;
using STA.Core;
using LitJson;
using System.Collections.Generic;

namespace STA.Web.UI
{
    public class Ajax : System.Web.UI.Page
    {
        private static object lockhelper = new object();
        public Ajax()
        {
            if (GeneralConfigs.GetConfig().Closed == 1)
            {
                ResponseText(GeneralConfigs.GetConfig().Closedreason);
                return;
            }

            switch (STARequest.GetString("t"))
            {
                case "conclick":
                    if (GeneralConfigs.GetConfig().Updateclick != 1)
                        ResponseText(Contents.GetContentClick(STARequest.GetInt("id", 0)).ToString());
                    ResponseText(Contents.UpdateContentClick(STARequest.GetInt("id", 0), STARequest.GetString("back") == "yes").ToString());
                    break;
                case "confieldval":
                    ResponseText(Contents.GetExtFieldValue(STARequest.GetString("ext"), STARequest.GetInt("cid", 0), STARequest.GetString("field")));
                    break;
                case "getconclick":
                    ResponseText(Contents.GetContentClick(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "magclick":
                    ResponseText(Contents.UpdateMagazineClick(STARequest.GetInt("id", 0), STARequest.GetString("back") == "yes").ToString());
                    break;
                case "getmagclick":
                    ResponseText(Contents.GetMagazineClick(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "getsoftdowncount":
                    ResponseText(Contents.GetSoftDownloadCount(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "diggstamp":
                    ResponseText(Contents.GetDiggStamp(STARequest.GetInt("id", 0)));
                    break;
                case "condigg":
                    ResponseText(Contents.ContentDigg(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "constamp":
                    ResponseText(Contents.ContentStamp(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "comdigg":
                    ResponseText(Contents.CommentDigg(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "comstamp":
                    ResponseText(Contents.commentStamp(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "subcomment":
                    SubComment();
                    break;
                case "concomcount":
                    ResponseText(Contents.ConCommentCount(STARequest.GetInt("id", 0)).ToString());
                    break;
                case "getconcomment":
                    GetComment();
                    break;
                case "usernameexist":
                    ResponseText(Users.CheckUserNameExist(Utils.UrlDecode(STARequest.GetString("username"))).ToString());
                    break;
                case "useremailexist":
                    ResponseText(Users.CheckUserEmailExist(Utils.UrlDecode(STARequest.GetString("email"))).ToString());
                    break;
                case "getcontentlist":
                    GetContentList();
                    break;
                case "getuseraddress":
                    ResponseText(Utils.DataTableToJSON(Shops.GetUserAddressTableByUid(STARequest.GetInt("uid", 0))).ToString());
                    break;
                case "edituseraddress":
                    EditUseraddress();
                    break;
                case "getprodsbyids":
                    ResponseText(Utils.DataTableToJSON(Contents.GetExtConTableByIds("product", STARequest.GetString("fields"), STARequest.GetString("ids"))).ToString());
                    break;
                case "userlogin":
                    ResponseText(UserUtils.UserLogin(GeneralConfigs.GetConfig(),
                                STARequest.GetString("username"),
                                STARequest.GetString("password"),
                                STARequest.GetInt("expires", -1)));
                    break;
                case "userconnectbind": //绑定第三方登录账户
                    UserConnectBind();
                    break;
                case "userconnectregister": //注册第三方登录账户
                    UserConnectRegister();
                    break;
                case "mailsubcribe":
                    MailSubcribe();
                    break;
                case "translate":
                    string sourcetext = Utils.UrlDecode(STARequest.GetString("text"));
                    string sourcelang = STARequest.GetString("sl");
                    string targetlang = STARequest.GetString("tl");
                    string deftext = Utils.UrlDecode(STARequest.GetString("def"));
                    if (sourcelang == "auto") ResponseText(Translators.Translate(sourcetext, targetlang));
                    else ResponseText(Translators.Translate(sourcetext, sourcelang, targetlang, deftext == "" ? sourcetext : deftext));
                    break;
                default:
                    HttpContext.Current.Response.StatusCode = 404;
                    break;
            }
        }

        void UserConnectBind()
        {
            //检查URL参数
            if (HttpContext.Current.Session["oauth_name"] == null)
            {
                ResponseText("{\"status\": 0, \"msg\": \"错误提示：授权参数不正确！\"}");
                return;
            }
            //获取授权信息
            string result = Utils.UrlExecute(GeneralConfigs.GetConfig().Weburl + BaseConfigs.GetSitePath + "/api/oauth/" + HttpContext.Current.Session["oauth_name"].ToString() + "/resultjson.aspx");
            if (result.Contains("error"))
            {
                ResponseText("{\"status\": 0, \"msg\": \"错误提示：请检查URL是否正确！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                ResponseText("{\"status\": 0, \"msg\": \"错误代码：" + dic["ret"] + "，描述：" + dic["msg"] + "\"}");
                return;
            }

            //检查用户名密码
            string username = STARequest.GetString("username");
            string password = STARequest.GetString("password");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ResponseText("{\"status\": 0, \"msg\": \"请输入用户名或密码！\"}");
                return;
            }

            UserInfo uinfo = Users.CheckUserLogin(username, Utils.MD5(password), 0);

            if (uinfo == null)
            {
                ResponseText("{\"status\":0, \"msg\":\"用户名或密码错误，请重试！\"}");
                return;
            }

            //开始绑定
            UserconnectInfo connectinfo = new UserconnectInfo();
            connectinfo.Uid = uinfo.Id;
            connectinfo.Appidentify = dic["oauth_name"].ToString();
            connectinfo.Openid = dic["oauth_openid"].ToString();
            connectinfo.Token = dic["oauth_access_token"].ToString();

            int id = Connects.AddUserconnect(connectinfo);

            if (id < 1)
            {
                ResponseText("{\"status\":0, \"msg\":\"错误提示：绑定过程中出现错误，请重新登录授权！\"}");
                return;
            }

            ResponseText("{\"status\":1, \"msg\":\"会员登录成功！\"}");
        }

        void UserConnectRegister()
        {
            //检查URL参数
            if (HttpContext.Current.Session["oauth_name"] == null)
            {
                ResponseText("{\"status\": 0, \"msg\": \"错误提示：授权参数不正确！\"}");
                return;
            }
            //获取授权信息
            string result = Utils.UrlExecute(GeneralConfigs.GetConfig().Weburl + BaseConfigs.GetSitePath + "/api/oauth/" + HttpContext.Current.Session["oauth_name"].ToString() + "/resultjson.aspx");
            if (result.Contains("error"))
            {
                ResponseText("{\"status\": 0, \"msg\": \"错误提示：请检查URL是否正确！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                ResponseText("{\"status\": 0, \"msg\": \"错误代码：" + dic["ret"] + "，描述：" + dic["msg"] + "\"}");
                return;
            }

            string username = STARequest.GetFormString("username");
            string password = STARequest.GetFormString("password");
            string email = STARequest.GetFormString("email");

            if (GeneralConfigs.GetConfig().Forbiduserwords.IndexOf(username) >= 0 || Users.CheckUserNameExist(username) > 0)
            {
                ResponseText("{\"status\": 0, \"msg\": \"用户名已经存在,请使用其他用户名!\"}");
                return;
            }
            if (GeneralConfigs.GetConfig().Emailmultuser == 0 && Users.CheckUserEmailExist(email) > 0)
            {
                ResponseText("{\"status\": 0, \"msg\": \"邮箱已经存在,请使用其他邮箱注册!\"}");
                return;
            }

            UserInfo uinfo = new UserInfo();
            uinfo.Username = username;
            uinfo.Password = Utils.MD5(password);
            uinfo.Email = email;
            uinfo.Regip = STARequest.GetIP();
            uinfo.Gender = byte.Parse(STARequest.GetFormString("gender"));
            uinfo.Nickname = STARequest.GetFormString("nickname");

            if (GeneralConfigs.GetConfig().Userverifyway == 0)
            {
                uinfo = UserUtils.InitUserGroup(uinfo);
                uinfo.Ischeck = 1;
            }
            uinfo.Id = Users.AddUser(uinfo);
            if (uinfo.Id > 0)
            {
                UserfieldInfo ufinfo = new UserfieldInfo();
                ufinfo.Uid = uinfo.Id;
                Users.AddUserField(ufinfo);

                if (GeneralConfigs.GetConfig().Userverifyway == 2)
                {
                    UserauthInfo uactinfo = new UserauthInfo();
                    uactinfo.Userid = uinfo.Id;
                    uactinfo.Username = uinfo.Username;
                    uactinfo.Email = uinfo.Email;
                    uactinfo.Code = ConUtils.CreateAuthStr(30);
                    Users.AddUserauth(uactinfo);

                    Emails.STASmtpRegisterMail(uinfo.Username, uinfo.Email, uinfo.Password, uactinfo.Code);
                }
            }
            else
            {
                ResponseText("{\"status\": 0, \"msg\": \"注册失败，请稍后重试！\"}");
                return;
            }

            ////赠送积分金额
            //if (modelGroup.point > 0)
            //{
            //    new BLL.point_log().Add(model.id, model.user_name, modelGroup.point, "注册赠送积分");
            //}
            //if (modelGroup.amount > 0)
            //{
            //    new BLL.amount_log().Add(model.id, model.user_name, DTEnums.AmountTypeEnum.SysGive.ToString(), modelGroup.amount, "注册赠送金额", 1);
            //}
            ////判断是否发送站内短消息
            //if (userConfig.regmsgstatus == 1)
            //{
            //    new BLL.user_message().Add(1, "", model.user_name, "欢迎您成为本站会员", userConfig.regmsgtxt);
            //}
            ////绑定到对应的授权类型
            //Model.user_oauth oauthModel = new Model.user_oauth();
            //oauthModel.oauth_name = dic["oauth_name"].ToString();
            //oauthModel.user_id = model.id;
            //oauthModel.user_name = model.user_name;
            //oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            //oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            //new BLL.user_oauth().Add(oauthModel);

            //context.Session[DTKeys.SESSION_USER_INFO] = model;
            //context.Session.Timeout = 45;
            ////记住登录状态，防止Session提前过期
            //Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
            //Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
            ////写入登录日志
            //new BLL.user_login_log().Add(model.id, model.user_name, "会员登录", DTRequest.GetIP());
            ////返回URL
            //context.Response.Write("{\"msg\":1, \"msgbox\":\"会员登录成功！\"}");
            //return;
        }

        void MailSubcribe()
        {
            MailsubInfo info = new MailsubInfo();
            info.Name = STARequest.GetString("name");
            info.Mail = STARequest.GetString("mail");
            info.Forgroup = STARequest.GetString("group");
            ResponseText(Mails.AddSubmail(info));
        }

        void EditUseraddress()
        {
            if (ConUtils.GetOnlineUser().Userid <= 0)
            {
                ResponseText("0");
                return;
            }
            UseraddressInfo info = new UseraddressInfo();
            info.Id = STARequest.GetInt("id", 0);
            info.Uid = STARequest.GetInt("uid", 0);
            info.Username = Utils.UrlDecode(STARequest.GetString("username"));
            info.Title = Utils.UrlDecode(STARequest.GetString("title"));
            info.Email = Utils.UrlDecode(STARequest.GetString("email"));
            info.Address = Utils.UrlDecode(STARequest.GetString("address"));
            info.Postcode = Utils.UrlDecode(STARequest.GetString("postcode"));
            info.Phone = Utils.UrlDecode(STARequest.GetString("phone"));
            info.Parms = Utils.UrlDecode(STARequest.GetString("parms"));
            if (info.Id <= 0)
                ResponseText(Shops.AddUseraddress(info).ToString());
            else
                ResponseText(Shops.EditUseraddress(info).ToString());
        }

        void GetContentList()
        {
            int num = STARequest.GetInt("num", 10);
            string fields = STARequest.GetString("fields");
            num = num <= 0 ? 10 : num;
            fields = fields.Trim() == "" ? "*" : fields;

            string where = Contents.GetContentSearchCondition(STARequest.GetInt("typeid", -1), Utils.UrlDecode(STARequest.GetString("addusers")), STARequest.GetInt("recyle", 0), STARequest.GetInt("channelid", 0), (int)ConStatus.通过, STARequest.GetString("property"), STARequest.GetString("startdate"), STARequest.GetString("enddate"), Utils.UrlDecode(STARequest.GetString("keywords")));

            ResponseJSON(Utils.DataTableToJSON(Contents.GetContentTableByWhere(num, fields, where)).ToString());
        }

        void GetComment()
        {
            ContentInfo con = ConUtils.GetSimpleContent(STARequest.GetInt("cid", 0));
            if (GeneralConfigs.GetConfig().Opencomment == 0 || (con != null && con.Iscomment == 0))
            {
                ResponseText("");
                return;
            }
            string fields = STARequest.GetString("fields");
            if (fields == "")
                fields = "id,uid,username,title,addtime,diggcount,stampcount,msg,quote,replay,star,userip,city";

            int pagecount, recordcount;
            string orderby = STARequest.GetString("order");
            orderby = orderby == "" ? "id" : orderby;
            DataTable dt = Contents.GetCommentDataPage(fields, orderby + " desc", STARequest.GetInt("page", 1), STARequest.GetInt("pagesize", 20)
                           , Contents.GetCommentSearchCondition((int)CommentStatus.通过, STARequest.GetInt("ctype", 1), STARequest.GetInt("cid", 0), "", "", "", "", "", ""), out pagecount, out recordcount);

            if (dt.Rows.Count <= 0 || dt == null)
            {
                ResponseText("");
            }
            else
            {
                if (dt.Columns.Contains("msg"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["msg"] = dr["msg"].ToString().Replace("\n", "<br/>").Replace("'", "”");
                    }
                }
                ResponseJSON("{pagecount:" + pagecount.ToString() + ",recordcount:" + recordcount.ToString() + ",content:" + Utils.DataTableToJSON(dt).ToString() + "}");
            }
        }

        void SubComment()
        {
            CommentInfo info = new CommentInfo();
            info.Cid = STARequest.GetInt("cid", 0);
            info.Ctype = STARequest.GetInt("ctype", 1);
            info.Contitle = Utils.HtmlEncode(STARequest.GetString("contitle"));
            info.Uid = STARequest.GetInt("uid", 0);
            info.Username = Utils.HtmlDecode(STARequest.GetString("username"));
            info.Title = Utils.HtmlEncode(STARequest.GetString("title"));
            info.Userip = STARequest.GetIP();
            info.Msg = Utils.HtmlEncode(STARequest.GetString("msg"));
            info.Star = STARequest.GetInt("star", 5);
            info.Useragent = HttpContext.Current.Request.UserAgent;
            //info.Quote = Utils.HtmlDecode(STARequest.GetString("quote")).Replace("\r\n", "").Replace("\n", "").ToLower();
            info.Replay = STARequest.GetInt("replay", 0);

            ResponseText(Globals.AddComment(info).ToString());
        }

        void ResponseText(String text)
        {
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }

        void ResponseJSON(string json)
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