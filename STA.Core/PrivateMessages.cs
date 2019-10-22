using System;
using System.Data;
using System.Data.Common;

using STA.Common;
using STA.Data;
using STA.Config;
using STA.Entity;
using STA.Cache;
using STA.Common.Generic;
using System.Collections.Generic;

namespace STA.Core
{
    /// <summary>
    /// 短消息操作类
    /// </summary>
    public class PrivateMessages
    {
        /// <summary>
        /// 负责发送新用户注册欢迎信件的用户名称, 该名称同时不允许用户注册
        /// </summary>
        public const string SystemUserName = "系统";

        /// <summary>
        /// 检查邮件接收者的有效性
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool CheckToUser(string username)
        {
            return Users.CheckUserNameExist(username) > 0 && username != SystemUserName && ConUtils.GetOnlineUser().Username != username;
        }

        /// <summary>
        /// 给信息内容转换成html
        /// </summary>
        /// <param name="message"></param>
        public static string ParseMessageToHtml(string message)
        {
            message = message.Replace("\r", "<br/>");
            return Utils.TextAddLink(message);
        }
        /// <summary>
        /// 提交后的权限检查
        /// </summary>
        /// <returns>如果返回空,则验证成功</returns>
        public static string CheckPermissionAfterPost()
        {
            if (ConUtils.IsCrossSitePost())
            {
                return "您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。";
            }
            else if (Utils.StrIsNullOrEmpty(STARequest.GetString("content")))
            {
                return "内容不能为空";
            }
            else if (Utils.StrIsNullOrEmpty(STARequest.GetString("msgto")))
            {
                return "接收人不能为空";
            }
            else if (Utils.StrIsNullOrEmpty(STARequest.GetString("subject")) || STARequest.GetString("subject").Trim().Length > 60)
            {
                return "标题不能为空,且不能超过60字";
            }
            else if (!CheckToUser(STARequest.GetString("msgto")))
            {
                return "不能给非注册用户或自己发送消息";
            }
            return "";
        }

        /// <summary>
        /// 获得指定ID的短消息的内容
        /// </summary>
        /// <param name="pmid">短消息pmid</param>
        /// <returns>短消息内容</returns>
        public static PrivateMessageInfo GetPrivateMessageInfo(int pmid)
        {
            return pmid > 0 ? STA.Data.PrivateMessages.GetPrivateMessage(pmid) : null;
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(int userId, int folder)
        {
            return userId > 0 ? GetPrivateMessageCount(userId, folder, -1) : 0;
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、2:最近消息（7天内）、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(int userId, int folder, int state)
        {
            return userId > 0 ? STA.Data.PrivateMessages.GetPrivateMessageCount(userId, folder, state) : 0;
        }

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        public static int GetAnnouncePrivateMessageCount()
        {
            STACache cache = STACache.GetCacheService();
            int announcepmcount = Utils.StrToInt(cache.RetrieveObject("/STA/AnnouncePrivateMessageCount"), -1);

            if (announcepmcount < 0)
            {
                announcepmcount = STA.Data.PrivateMessages.GetAnnouncePrivateMessageCount();
                cache.AddObject("/STA/AnnouncePrivateMessageCount", announcepmcount);
            }
            return announcepmcount;
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public static int CreatePrivateMessage(PrivateMessageInfo privatemessageinfo, int savetosentbox)
        {
            return STA.Data.PrivateMessages.AddPrivateMessage(privatemessageinfo, savetosentbox);
        }


        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public static int DelPrivateMessages(int userId, string[] pmitemid)
        {
            if (!Utils.IsNumericArray(pmitemid))
                return -1;

            int reval = STA.Data.PrivateMessages.DelPrivateMessages(userId, String.Join(",", pmitemid));
            if (reval > 0)
                STA.Data.Users.SetUserNewPMCount(userId, STA.Data.PrivateMessages.GetNewPMCount(userId));

            return reval;
        }

        /// <summary>
        /// 删除指定用户的一条短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmid">要删除的短信息ID</param>
        /// <returns>删除记录数</returns>
        public static int DelPrivateMessage(int userId, int pmid)
        {
            return userId > 0 ? DelPrivateMessages(userId, new string[] { pmid.ToString() }) : 0;
        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public static int SetPrivateMessageState(int pmid, byte state)
        {
            return pmid > 0 ? STA.Data.PrivateMessages.SetPrivateMessageState(pmid, state) : 0;
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageList(int userId, int folder, int pagesize, int pageindex, int readStatus)
        {
            return (userId > 0 && pageindex > 0 && pagesize > 0) ? STA.Data.PrivateMessages.GetPrivateMessageList(userId, folder, pagesize, pageindex, readStatus) : null;
        }


        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        public static DataTable GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType, out int pagecount, out int recordcount)
        {
            pagecount = 0;
            recordcount = 0;
            return (userId > 0 && pageIndex > 0 && pageSize > 0) ? STA.Data.PrivateMessages.GetPrivateMessageList(userId, folder, pageSize, pageIndex, intType, out pagecount, out recordcount) : (new DataTable());
        }

        /// <summary>
        /// 获得公共消息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>公共消息列表</returns>
        public static List<PrivateMessageInfo> GetAnnouncePrivateMessageList(int pagesize, int pageindex)
        {
            if (pagesize == -1)
                return STA.Data.PrivateMessages.GetAnnouncePrivateMessageList(-1, 0);
            return (pagesize > 0 && pageindex > 0) ? STA.Data.PrivateMessages.GetAnnouncePrivateMessageList(pagesize, pageindex) : null;
        }

        /// <summary>
        /// 返回短标题的收件箱短消息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="strwhere">筛选条件</param>
        /// <returns>收件箱短消息列表</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageListForInbox(int userId, int pagesize, int pageindex, int inttype)
        {
            List<PrivateMessageInfo> list = GetPrivateMessageList(userId, 0, pagesize, pageindex, inttype);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Content = Utils.GetSubString(list[i].Content, 20, "...");
                }
            }
            return list;
        }

        /// <summary>
        /// 返回最新的一条记录ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static int GetLatestPMID(int userId)
        {
            List<PrivateMessageInfo> list = STA.Data.PrivateMessages.GetPrivateMessageList(userId, 0, 1, 1, 0);
            int latestpmid = 0;

            foreach (PrivateMessageInfo info in list)
            {
                latestpmid = info.Id;
                break;
            }
            return latestpmid;
        }

        /// <summary>
        /// 获取删除短消息的条件
        /// </summary>
        /// <param name="isNew">是否删除新短消息</param>
        /// <param name="postDateTime">发送日期</param>
        /// <param name="msgFromList">发送者列表</param>
        /// <param name="lowerUpper">是否区分大小写</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        /// <param name="isUpdateUserNewPm">是否更新用户短消息数</param>
        /// <returns></returns>
        public static string DelPrivateMessages(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm)
        {
            return Data.PrivateMessages.DelPrivateMessages(isNew, postDateTime, msgFromList, lowerUpper, subject, message, isUpdateUserNewPm);
        }
    }
}
