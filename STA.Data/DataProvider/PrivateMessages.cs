using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class PrivateMessages
    {
        #region pms

        private static PrivateMessageInfo LoadPrivateMessageInfo(IDataReader reader)
        {
            PrivateMessageInfo info = new PrivateMessageInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Msgfrom = TypeParse.ObjToString(reader["msgfrom"]).Trim();
            info.Msgfromid = TypeParse.StrToInt(reader["msgfromid"], 0);
            info.Msgto = TypeParse.ObjToString(reader["msgto"]).Trim();
            info.Msgtoid = TypeParse.StrToInt(reader["msgtoid"], 0);
            info.Folder = (Folder)byte.Parse(TypeParse.ObjToString(reader["folder"]));
            info.New = TypeParse.StrToInt(reader["new"], 0);
            info.Subject = TypeParse.ObjToString(reader["subject"]).Trim();
            info.Content = TypeParse.ObjToString(reader["content"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            return info;
        }

        public static DataTable GetPrivateMessageDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetPrivateMessageDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static int AddPrivateMessage(PrivateMessageInfo info)
        {
            return DatabaseProvider.GetInstance().AddPrivateMessage(info);
        }

        public static int EditPrivateMessage(PrivateMessageInfo info)
        {
            return DatabaseProvider.GetInstance().EditPrivateMessage(info);
        }

        public static int DelPrivateMessage(int id)
        {
            return DatabaseProvider.GetInstance().DelPrivateMessage(id);
        }

        public static PrivateMessageInfo GetPrivateMessage(int id)
        {
            PrivateMessageInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPrivateMessage(id))
            {
                if (reader.Read())
                {
                    info = LoadPrivateMessageInfo(reader);
                }
            }
            return info;
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
        public static List<PrivateMessageInfo> GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType)
        {
            List<PrivateMessageInfo> coll = new List<PrivateMessageInfo>();
            IDataReader reader = DatabaseProvider.GetInstance().GetPrivateMessageList(userId, folder, pageSize, pageIndex, intType);
            if (reader != null)
            {
                while (reader.Read())
                {
                    coll.Add(LoadPrivateMessageInfo(reader));
                }
                reader.Close();
            }
            return coll;
        }

        public static DataTable GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetPrivateMessageList(userId, folder, pageSize, pageIndex, intType, out pagecount, out recordcount);
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(int userId, int folder, int state)
        {
            return DatabaseProvider.GetInstance().GetPrivateMessageCount(userId, folder, state);
        }

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        public static int GetAnnouncePrivateMessageCount()
        {
            return DatabaseProvider.GetInstance().GetAnnouncePrivateMessageCount();
        }

        /// <summary>
        /// 获得公共短信息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数,为-1时返回全部</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>短信息列表</returns>
        public static List<PrivateMessageInfo> GetAnnouncePrivateMessageList(int pagesize, int pageindex)
        {
            List<PrivateMessageInfo> coll = new List<PrivateMessageInfo>();
            IDataReader reader = DatabaseProvider.GetInstance().GetAnnouncePrivateMessageList(pagesize, pageindex);
            if (reader != null)
            {
                while (reader.Read())
                {
                    coll.Add(LoadPrivateMessageInfo(reader));
                }
                reader.Close();
            }
            return coll;
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="__privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public static int AddPrivateMessage(PrivateMessageInfo info, int saveToSentBox)
        {
            return DatabaseProvider.GetInstance().AddPrivateMessage(info, saveToSentBox);
        }

        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public static int DelPrivateMessages(int userId, string pmIdList)
        {
            return DatabaseProvider.GetInstance().DelPrivateMessages(userId, pmIdList);
        }

        /// <summary>
        /// 获得新短消息数
        /// </summary>
        /// <returns></returns>
        public static int GetNewPrivateMessageCount(int userId)
        {
            return DatabaseProvider.GetInstance().GetNewPrivateMessageCount(userId);
        }

        public static DataTable GetAnnouncePrivateMessageDataPage(string fields, int pageindex, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetAnnouncePrivateMessageDataPage(fields, pageindex, pagesize, out pagecount, out recordcount);
        }

        /// <summary>
        /// 删除指定用户的一条短消息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pmid">pmid</param>
        /// <returns></returns>
        public static int DelPrivateMessage(int userId, int pmId)
        {
            return DatabaseProvider.GetInstance().DelPrivateMessage(userId, pmId);
        }

        /// <summary>
        /// 删除短消息
        /// </summary>
        /// <param name="isNew">是否删除新短消息</param>
        /// <param name="postDateTime">发送日期</param>
        /// <param name="msgFromList">发送者列表</param>
        /// <param name="lowerUpper">是否区分大小写</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        /// <param name="isUpdateUserNewPm">是否更新用户短消息数</param>
        /// <returns></returns>
        public static string DelPrivateMessages(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject,
            string message, bool isUpdateUserNewPm)
        {
            return DatabaseProvider.GetInstance().DelPrivateMessages(isNew, postDateTime, msgFromList, lowerUpper,
                subject, message, isUpdateUserNewPm).ToString();
        }


        /// <summary>
        /// 获取新短消息数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static int GetNewPMCount(int userId)
        {
            return DatabaseProvider.GetInstance().GetNewPMCount(userId);
        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public static int SetPrivateMessageState(int pmId, byte state)
        {
            return DatabaseProvider.GetInstance().SetPrivateMessageState(pmId, state);
        }
        #endregion
    }
}
