using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Payment;
using STA.Entity;
namespace STA.Page.User
{
    public class Sendpm : UserBase
    {
        #region 页面变量
        /// <summary>
        /// 短消息收件人
        /// </summary>
        public string msgto = Utils.HtmlEncode(STARequest.GetString("msgto"));
        /// <summary>
        /// 短消息标题
        /// </summary>
        public string subject = Utils.HtmlEncode(STARequest.GetString("subject"));
        /// <summary>
        /// 短消息内容
        /// </summary>
        public string message = Utils.HtmlEncode(STARequest.GetString("content"));

        /// <summary>
        /// 短消息
        /// </summary>
        PrivateMessageInfo pm = new PrivateMessageInfo();
        #endregion

        protected override void PageShow()
        {
            if (!IsLogin()) return;

            if (ispost && !ConUtils.IsCrossSitePost())
            {
                if (!CheckPermissionAfterPost())
                    return;

                SendPM();
                if (IsErr()) return;
            }

            string action = STARequest.GetQueryString("action").ToLower();
            if (action == "re" || action == "fw") //回复或者转发
            {
                if (STARequest.GetQueryInt("pmid", -1) != -1)
                {
                    PrivateMessageInfo pm = STA.Data.PrivateMessages.GetPrivateMessage(STARequest.GetQueryInt("pmid", -1));
                    if (pm != null && (pm.Msgtoid == userid || pm.Msgfromid == userid))
                    {
                        msgto = action.CompareTo("re") == 0 ? Utils.HtmlEncode(pm.Msgfrom) : "";
                        subject = Utils.HtmlEncode(action) + ":" + pm.Subject;
                        message = Utils.HtmlEncode("> ") + pm.Content.Replace("\n", "\n> ") + "\r\n\r\n";
                    }
                }
            }
        }

        /// <summary>
        /// 提交后的权限检查
        /// </summary>
        /// <returns></returns>
        private bool CheckPermissionAfterPost()
        {
            if (ConUtils.IsCrossSitePost())
            {
                AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                return false;
            }
            if (Utils.StrIsNullOrEmpty(STARequest.GetString("content")))
            {
                AddErrLine("内容不能为空");
                return false;
            }
            if (Utils.StrIsNullOrEmpty(STARequest.GetString("msgto")))
            {
                AddErrLine("接收人不能为空");
                return false;
            }
            if (Utils.StrIsNullOrEmpty(STARequest.GetString("subject")) || STARequest.GetString("subject").Trim().Length > 60)
            {
                AddErrLine("标题不能为空,且不能超过60字");
                return false;
            }
            // 不能给负责发送新用户注册欢迎信件的用户名称发送消息
            if (STARequest.GetString("msgto") == STA.Core.PrivateMessages.SystemUserName)
            {
                AddErrLine("不能系统发送消息");
                return false;
            }
            if (STARequest.GetString("msgto") == username)
            {
                AddErrLine("不能给自己发送消息");
                return false;
            }
            if (Users.CheckUserNameExist(username) == 0)
            {
                AddErrLine("不能给非注册用户发送消息");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <param name="email">接收人邮箱</param>
        /// <param name="privatemessageinfo">短消息对象</param>
        public void SendNotifyEmail(string email, PrivateMessageInfo pm)
        {
            string jumpurl = string.Format("http://{0}/user/readpm.aspx?pmid={1}", STARequest.GetCurrentFullHost(), pm.Id);
            StringBuilder sb_body = new StringBuilder("# 站内短消息: <a href=\"" + jumpurl + "\" target=\"_blank\">" + pm.Subject + "</a>");
            //发送人邮箱
            sb_body.AppendFormat("\r\n\r\n{0}\r\n<hr/>", pm.Content);
            sb_body.AppendFormat("发送人:{0}\r\n", pm.Msgfrom);
            sb_body.AppendFormat("Email:<a href=\"mailto:{0}\" target=\"_blank\">{0}</a>\r\n", Users.GetUser(userid).Email.Trim());
            sb_body.AppendFormat("详 细:<a href=\"{0}\" target=\"_blank\">{0}</a>\r\n", jumpurl);
            sb_body.AppendFormat("时 间:{0}", pm.Addtime);
            Emails.SendEmailNotify(email, "[" + config.Webname + "短消息通知]" + pm.Subject, sb_body.ToString());
        }

        /// <summary>
        /// 创建并发送短消息
        /// </summary>
        public void SendPM()
        {
            #region 创建并发送短消息

            // 收件箱
            if (useradminid > 0)
            {
                pm.Content = Utils.HtmlEncode(STARequest.GetString("content"));
                pm.Subject = Utils.HtmlEncode(STARequest.GetString("subject"));
            }
            else
            {
                pm.Content = Utils.HtmlEncode(Globals.BanWordFilter(STARequest.GetString("content")));
                pm.Subject = Utils.HtmlEncode(Globals.BanWordFilter(STARequest.GetString("subject")));
            }

            if (useradminid != 1 && (Globals.HasBannedWord(pm.Content) || Globals.HasBannedWord(pm.Subject) || Globals.HasAuditWord(pm.Content) || Globals.HasAuditWord(pm.Subject)))
            {
                string bannedWord = Globals.GetBannedWord(pm.Content) == string.Empty ? Globals.GetBannedWord(pm.Subject) : Globals.GetBannedWord(pm.Content);
                AddErrLine(string.Format("对不起, 您提交的内容包含不良信息 <font color=\"red\">{0}</font>, 因此无法提交, 请返回修改!", bannedWord));
                return;
            }
            UserInfo touser = Users.GetUser(STARequest.GetString("msgto"));

            pm.Content = Globals.BanWordFilter(pm.Content);
            pm.Subject = Globals.BanWordFilter(pm.Subject);
            pm.Msgto = STARequest.GetString("msgto");
            pm.Msgtoid = touser.Id;
            pm.Msgfrom = username;
            pm.Msgfromid = userid;
            pm.New = 1;
            pm.Folder = Folder.收件;
            if (STARequest.GetString("savetosentbox") == "1")
            {
                
                pm.Id = STA.Core.PrivateMessages.CreatePrivateMessage(pm, 1);
                AddMsgLine("发送完毕, 且已将消息保存到发件箱");
            }
            else
            {
                pm.Id = STA.Core.PrivateMessages.CreatePrivateMessage(pm, 0);
                AddMsgLine("发送完毕");
            }

            if (STARequest.GetString("emailnotify") == "1")
            {
                SendNotifyEmail(touser.Email.Trim(), pm);
            }

            #endregion
        }
    }
}
