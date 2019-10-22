using System;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Entity;
using STA.Core;
using STA.Data;
using System.Data;

namespace STA.Page.User
{
    public class Action : UserBase
    {
        public string action = STARequest.GetString("action");
        public string returnurl = Utils.UrlDecode(STARequest.GetString("returnurl"));

        protected override void PageShow()
        {
            if (!IsLogin()) return;

            string msg = "操作成功！";
            switch (action)
            {
                case "addfavorite":
                    AddFavorite();
                    break;
                case "loginout":
                    ConUtils.ClearUserCookie();
                    msg += "已退出登录！";
                    PageInfo(msg, returnurl == "" ? weburl : returnurl, 1);
                    break;
                case "useravatarup":
                    UserAvatorUpload();
                    break;
                case "conimgup":
                    ConImgUp();
                    break;
                case "saveprofile":
                    SaveProfile();
                    break;
                case "changepassword":
                    ChangePassword();
                    break;
                case "changeemail":
                    ChangeEmail();
                    break;
                case "useravatarset":
                    ResponseText(Avatars.UserAvatorSet(userid, Utils.UrlDecode(STARequest.GetString("avatar")), 350, STARequest.GetString("coords")));
                    break;
                case "sendpm":
                    SendPm();
                    break;
                case "managepm":
                    ManagePm();
                    break;
                case "delcomment":
                    DelComment();
                    break;
                case "ueditorimgup":
                    UeditorUpload(config.Attachimgmaxsize * 1024, config.Attachimgtype);
                    break;
                case "ueditorattup":
                    UeditorUpload(config.Attachsoftmaxsize * 1024, config.Attachsofttype);
                    break;
                case "delcons":
                    int successcount = 0;
                    foreach (string str in STARequest.GetString("ids").Split(','))
                    {
                        ContentInfo cinfo = ConUtils.GetSimpleContent(TypeParse.StrToInt(str));
                        if (cinfo != null && cinfo.Adduser == userid)
                        {
                            ConUtils.DelContent(config, cinfo.Id, cinfo.Typeid);
                            successcount++;
                        }
                    }
                    ResponseText(successcount.ToString());
                    break;
                case "delatts":
                    successcount = 0;
                    foreach (string str in STARequest.GetString("ids").Split(','))
                    {
                        AttachmentInfo info = Contents.GetAttachment(TypeParse.StrToInt(str));
                        if (info != null && info.Uid == userid)
                        {
                            ConUtils.DelAttachment(info.Id, sitedir + info.Filename);
                            successcount++;
                        }
                    }
                    ResponseText(successcount.ToString());
                    break;
                case "ueditorimgmanage":
                    int pagecount, recordcount;
                    DataTable dt = Contents.GetAttachmentDataPage("filename", 1, 10000, Contents.GetAttachSearchCondition("", "", username, config.Attachimgtype, -1, -1, ""), out pagecount, out recordcount);
                    string resp = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        resp += dr["filename"].ToString() + "ue_separate_ue";
                    }
                    ResponseText(resp);
                    break;
                case "delfavorites":
                    if (ConUtils.IsCrossSitePost()) return;
                    ResponseText(STA.Data.Favorites.DelFavorites(STARequest.GetString("cids"), STARequest.GetInt("typeid", 1), userid).ToString());
                    break;
                default:
                    msg = "操作有误！";
                    break;
            }
        }

        void UeditorUpload(int maxsize, string filetype)
        {
            if (ConUtils.IsCrossSitePost()) return;
            AttachmentInfo[] ufileinfo = ConUtils.SaveRequestFiles(1, filetype, maxsize, "", "", 1, 0, "", "upfile", config);
            if (ufileinfo != null && ufileinfo[0].Noupload == "")
            {
                ufileinfo[0].Attachment = ufileinfo[0].Attachment;
                ufileinfo[0].Uid = ufileinfo[0].Lastedituid = ufileinfo[0].Uid = userid;
                ufileinfo[0].Username = ufileinfo[0].Lasteditusername = ufileinfo[0].Username = username;
                Contents.AddAttachment(ufileinfo[0]);
                ResponseText("{'url':'" + baseconfig.Sitepath + ufileinfo[0].Filename + "','fileType':'." + ufileinfo[0].Fileext + "','title':'" + ufileinfo[0].Attachment + "','original':'" + ufileinfo[0].Attachment + "','state':'SUCCESS'}");
            }
            else
            {
                string state = ufileinfo == null ? "上传失败" : ufileinfo[0].Noupload;
                ResponseText("{'url':'','title':'','original':'','state':'" + state + "'}");
            }
        }

        void DelComment()
        {
            int cid = STARequest.GetInt("id", 0);
            int ctype = STARequest.GetInt("ctype", 1);
            if (cid > 0 && ctype > 0)
            {
                CommentInfo cinfo = Contents.GetComment(cid);
                if (cinfo != null && cinfo.Uid == userid)
                {
                    ResponseText(Contents.DelComment(cid, ctype) ? "1" : "0");
                    return;
                }
            }
            ResponseText("0");
        }

        void AddFavorite()
        {
            if (STARequest.GetInt("type", 1) == 1)
            {
                ContentInfo cinfo = ConUtils.GetSimpleContent(STARequest.GetInt("cid", 0));
                if (cinfo != null)
                {
                    FavoriteInfo info = new FavoriteInfo();
                    info.Cid = STARequest.GetInt("cid", 0);
                    info.Uid = userid;
                    info.Typeid = 1;
                    string backurl = Urls.Content(cinfo.Id, cinfo.Typeid, cinfo.Savepath, cinfo.Filename);
                    if (Favorites.AddFavorite(info) == -1)
                    {
                        PageInfo("已收藏过了,不须重复收藏", backurl);
                    }
                    else
                    {
                        PageInfo("已成功加入收藏", backurl);
                    }
                }
                else
                {
                    PageInfo("要收藏的内容不存在", siteurl);
                }
            }
        }

        void ManagePm()
        {
            if (!ConUtils.IsCrossSitePost())
            {
                string rtype = STARequest.GetString("rtype");
                string[] pmids = STARequest.GetString("pmids").Split(',');
                switch (rtype)
                {
                    case "delpms":
                        ResponseText(STA.Core.PrivateMessages.DelPrivateMessages(userid, pmids) > 0 ? "1" : "0");
                        break;
                    case "setstate":
                        foreach (string pmid in pmids)
                        {
                            PrivateMessageInfo msginfo = STA.Data.PrivateMessages.GetPrivateMessage(TypeParse.StrToInt(pmid));
                            if (msginfo != null && ((msginfo.Msgtoid == userid && msginfo.Folder == Folder.收件) || (msginfo.Msgfromid == userid && msginfo.Folder == Folder.发件)))
                            {
                                STA.Core.PrivateMessages.SetPrivateMessageState(TypeParse.StrToInt(pmid), byte.Parse(STARequest.GetInt("state", 0).ToString()));
                            }
                        }
                        STA.Data.Users.SetUserNewPMCount(userid, STA.Data.PrivateMessages.GetNewPMCount(userid));
                        ResponseText("1");
                        break;
                }
            }
        }
        void SendPm()
        {
            string rtype = STARequest.GetString("rtype");
            if (rtype == "validatetouser")
            {
                ResponseText(STA.Core.PrivateMessages.CheckToUser(Utils.UrlDecode(STARequest.GetString("msgto"))) ? "1" : "0");
            }
            else
            {
                string alertxt = STA.Core.PrivateMessages.CheckPermissionAfterPost();
                if (alertxt != "")
                {
                    ResponseText(alertxt);
                }
            }
        }

        void ChangePassword()
        {
            if (!ConUtils.IsCrossSitePost())
            {
                UserInfo uinfo = Users.CheckUserLogin(user.Username, Utils.MD5(STARequest.GetFormString("oldpwd")), 0);
                if (uinfo == null)
                {
                    ResponseText("旧密码输入有误,请重试");
                }
                else if (STARequest.GetFormString("newpwd").Length < 6)
                {
                    ResponseText("密码长度必须大于6位");
                }
                else if (Utils.MD5(STARequest.GetFormString("oldpwd")) == Utils.MD5(STARequest.GetFormString("newpwd")))
                {
                    ResponseText("新密码不能和旧密码一样");
                }
                else
                {
                    uinfo.Password = Utils.MD5(STARequest.GetFormString("newpwd"));
                    Users.EditUser(uinfo);
                    if (config.Thirducenter == 1)
                    {
                        UserfieldInfo ufinfo = Users.GetUserField(uinfo.Id);
                    }
                    ConUtils.ClearUserCookie();
                    ResponseText("");
                }
            }
        }

        void ChangeEmail()
        {
            if (!ConUtils.IsCrossSitePost())
            {
                string email = STARequest.GetString("newemail");
                string pwd = STARequest.GetString("pwd");
                UserInfo uinfo = Users.CheckUserLogin(user.Username, Utils.MD5(pwd), 0);

                if (uinfo == null)
                {
                    ResponseText("密码输入有误,请重试");
                    return;
                }
                else if (user.Email == email)
                {
                    ResponseText("请设置一个新的邮箱");
                    return;
                }
                else if (config.Emailmultuser == 0 && Users.CheckUserEmailExist(email) > 0)
                {
                    ResponseText("该邮箱已经被别人使用");
                    return;
                }
                else
                {
                    if (config.Thirducenter == 1)
                    {
                        UserfieldInfo ufinfo = Users.GetUserField(uinfo.Id);
                    }
                    uinfo.Email = email;
                    Users.EditUser(uinfo);
                    ResponseText("");
                }
            }
        }

        void SaveProfile()
        {
            if (!ConUtils.IsCrossSitePost())
            {
                UserInfo uinfo = Users.GetUser(userid);
                UserfieldInfo ufinfo = Users.GetUserField(userid);
                uinfo.Nickname = Utils.HtmlEncode(STARequest.GetString("nickname"));
                uinfo.Gender = byte.Parse(STARequest.GetString("gender"));
                uinfo.Birthday = TypeParse.StrToDateTime(STARequest.GetString("birthday"));
                ufinfo.Realname = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("realname")));
                ufinfo.Areaname = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("areaname")));
                ufinfo.Idcard = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("idcard")));
                ufinfo.Signature = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("signature")));
                ufinfo.Description = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("description")));
                ufinfo.Website = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("website")));
                ufinfo.Mobile = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("mobile")));
                ufinfo.Worktel = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("worktel")));
                ufinfo.Hometel = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("hometel")));
                ufinfo.Postcode = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("postcode")));
                ufinfo.Address = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("address")));
                ufinfo.Qq = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("qq")));
                ufinfo.Skype = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("skype")));
                ufinfo.Msn = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("msn")));
                ufinfo.Icq = Utils.HtmlEncode(Utils.UrlDecode(STARequest.GetString("icq")));

                Users.EditUser(uinfo);
                Users.EditUserField(ufinfo);
                ResponseText("");
            }
        }

        void ImgUp(int maxsize, string thumbsize, bool foruser)
        {
            if (!ConUtils.IsCrossSitePost())
            {
                AttachmentInfo[] info = ConUtils.SaveRequestFiles(1, config.Attachimgtype, maxsize, "", "", 1, 0, thumbsize, STARequest.GetString("filekey"), config);
                if (info != null && info[0].Noupload == "")
                {

                    info[0].Attachment = (foruser ? "" : "临时图片：") + info[0].Attachment;
                    info[0].Lastedituid = info[0].Uid = userid;
                    info[0].Lasteditusername = info[0].Username = username;
                    info[0].Uid = foruser ? userid : 0;
                    info[0].Username = foruser ? username : "系统";
                    Contents.AddAttachment(info[0]);
                    ResponseText("{" + string.Format("'id':'{0}','name':'{1}','path':'{2}','msg':'1'", info[0].Id, info[0].Attachment, baseconfig.Sitepath + info[0].Filename) + "}");
                }
                else
                {
                    ResponseText("{" + string.Format("'id':'','name':'','path':'','msg':'{0}'", info != null ? info[0].Noupload : "上传失败") + "}");
                }
            }
        }

        void ConImgUp()
        {
            ImgUp(config.Attachimgmaxsize * 1024, config.Thumbsize, true);
        }

        void UserAvatorUpload()
        {
            ImgUp(config.Attachimgmaxsize * 1024, "", false);
        }
    }
}
