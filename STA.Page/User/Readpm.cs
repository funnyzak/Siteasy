using System;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Payment;
using STA.Entity;

namespace STA.Page.User
{
    public class Readpm : UserBase
    {
        #region 页面变量
        public PrivateMessageInfo info;
        /// <summary>
        /// 短消息Id
        /// </summary>
        public int pmid = STARequest.GetQueryInt("pmid", -1);
        /// <summary>
        /// 是否能够阅读短消息
        /// </summary>
        public bool canread = false;
        #endregion

        protected override void PageShow()
        {
            if (!IsLogin()) return;

            if (pmid <= 0)
            {
                AddErrLine("参数无效");
                return;
            }

            info = PrivateMessages.GetPrivateMessageInfo(pmid);
            if (info == null)
            {
                AddErrLine("无效的短消息ID");
                return;
            }

            if (info.Msgfrom == "系统" && info.Msgfromid == 0)
                info.Content = Utils.HtmlDecode(info.Content);

            if (info != null && ((info.Msgtoid == userid && info.Folder == Folder.收件)
                                    || (info.Msgfromid == userid && info.Folder == Folder.发件)))
            {
                canread = true;

                if (info.New == 1 && STARequest.GetString("action") != "noread")
                {
                    PrivateMessages.SetPrivateMessageState(pmid, 0);
                    STA.Data.Users.SetUserNewPMCount(userid, STA.Data.PrivateMessages.GetNewPMCount(userid));
                }
                info.Content = STA.Core.PrivateMessages.ParseMessageToHtml(info.Content);
                info.Msgto = (info.Folder == 0) ? info.Msgfrom : info.Msgto;
                return;
            }
            AddErrLine("对不起, 短消息不存在或已被删除.");
        }
    }
}
