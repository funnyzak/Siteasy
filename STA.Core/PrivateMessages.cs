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
    /// ����Ϣ������
    /// </summary>
    public class PrivateMessages
    {
        /// <summary>
        /// ���������û�ע�Ỷӭ�ż����û�����, ������ͬʱ�������û�ע��
        /// </summary>
        public const string SystemUserName = "ϵͳ";

        /// <summary>
        /// ����ʼ������ߵ���Ч��
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool CheckToUser(string username)
        {
            return Users.CheckUserNameExist(username) > 0 && username != SystemUserName && ConUtils.GetOnlineUser().Username != username;
        }

        /// <summary>
        /// ����Ϣ����ת����html
        /// </summary>
        /// <param name="message"></param>
        public static string ParseMessageToHtml(string message)
        {
            message = message.Replace("\r", "<br/>");
            return Utils.TextAddLink(message);
        }
        /// <summary>
        /// �ύ���Ȩ�޼��
        /// </summary>
        /// <returns>������ؿ�,����֤�ɹ�</returns>
        public static string CheckPermissionAfterPost()
        {
            if (ConUtils.IsCrossSitePost())
            {
                return "����������·����ȷ���޷��ύ���������װ��ĳ��Ĭ��������·��Ϣ�ĸ��˷���ǽ���(�� Norton Internet Security)���������䲻Ҫ��ֹ��·��Ϣ�����ԡ�";
            }
            else if (Utils.StrIsNullOrEmpty(STARequest.GetString("content")))
            {
                return "���ݲ���Ϊ��";
            }
            else if (Utils.StrIsNullOrEmpty(STARequest.GetString("msgto")))
            {
                return "�����˲���Ϊ��";
            }
            else if (Utils.StrIsNullOrEmpty(STARequest.GetString("subject")) || STARequest.GetString("subject").Trim().Length > 60)
            {
                return "���ⲻ��Ϊ��,�Ҳ��ܳ���60��";
            }
            else if (!CheckToUser(STARequest.GetString("msgto")))
            {
                return "���ܸ���ע���û����Լ�������Ϣ";
            }
            return "";
        }

        /// <summary>
        /// ���ָ��ID�Ķ���Ϣ������
        /// </summary>
        /// <param name="pmid">����Ϣpmid</param>
        /// <returns>����Ϣ����</returns>
        public static PrivateMessageInfo GetPrivateMessageInfo(int pmid)
        {
            return pmid > 0 ? STA.Data.PrivateMessages.GetPrivateMessage(pmid) : null;
        }

        /// <summary>
        /// �õ����û��Ķ���Ϣ����
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="folder">�����ļ���(0:�ռ���,1:������,2:�ݸ���)</param>
        /// <returns>����Ϣ����</returns>
        public static int GetPrivateMessageCount(int userId, int folder)
        {
            return userId > 0 ? GetPrivateMessageCount(userId, folder, -1) : 0;
        }

        /// <summary>
        /// �õ����û��Ķ���Ϣ����
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="folder">�����ļ���(0:�ռ���,1:������,2:�ݸ���)</param>
        /// <param name="state">����Ϣ״̬(0:�Ѷ�����Ϣ��1:δ������Ϣ��2:�����Ϣ��7���ڣ���-1:ȫ������Ϣ)</param>
        /// <returns>����Ϣ����</returns>
        public static int GetPrivateMessageCount(int userId, int folder, int state)
        {
            return userId > 0 ? STA.Data.PrivateMessages.GetPrivateMessageCount(userId, folder, state) : 0;
        }

        /// <summary>
        /// �õ�������Ϣ����
        /// </summary>
        /// <returns>������Ϣ����</returns>
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
        /// ��������Ϣ
        /// </summary>
        /// <param name="privatemessageinfo">����Ϣ����</param>
        /// <param name="savetosentbox">���ö���Ϣ�Ƿ��ڷ����䱣��(0Ϊ������, 1Ϊ����)</param>
        /// <returns>����Ϣ�����ݿ��е�pmid</returns>
        public static int CreatePrivateMessage(PrivateMessageInfo privatemessageinfo, int savetosentbox)
        {
            return STA.Data.PrivateMessages.AddPrivateMessage(privatemessageinfo, savetosentbox);
        }


        /// <summary>
        /// ɾ��ָ���û��Ķ���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="pmitemid">Ҫɾ���Ķ���Ϣ�б�(����)</param>
        /// <returns>ɾ����¼��</returns>
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
        /// ɾ��ָ���û���һ������Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="pmid">Ҫɾ���Ķ���ϢID</param>
        /// <returns>ɾ����¼��</returns>
        public static int DelPrivateMessage(int userId, int pmid)
        {
            return userId > 0 ? DelPrivateMessages(userId, new string[] { pmid.ToString() }) : 0;
        }

        /// <summary>
        /// ���ö���Ϣ״̬
        /// </summary>
        /// <param name="pmid">����ϢID</param>
        /// <param name="state">״ֵ̬</param>
        /// <returns>���¼�¼��</returns>
        public static int SetPrivateMessageState(int pmid, byte state)
        {
            return pmid > 0 ? STA.Data.PrivateMessages.SetPrivateMessageState(pmid, state) : 0;
        }

        /// <summary>
        /// ���ָ���û��Ķ���Ϣ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="folder">����Ϣ����(0:�ռ���,1:������,2:�ݸ���)</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="inttype">ɸѡ����1Ϊδ��</param>
        /// <returns>����Ϣ�б�</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageList(int userId, int folder, int pagesize, int pageindex, int readStatus)
        {
            return (userId > 0 && pageindex > 0 && pagesize > 0) ? STA.Data.PrivateMessages.GetPrivateMessageList(userId, folder, pagesize, pageindex, readStatus) : null;
        }


        /// <summary>
        /// ���ָ���û��Ķ���Ϣ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="folder">����Ϣ����(0:�ռ���,1:������,2:�ݸ���)</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="inttype">ɸѡ����1Ϊδ��</param>
        /// <returns>����Ϣ�б�</returns>
        public static DataTable GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType, out int pagecount, out int recordcount)
        {
            pagecount = 0;
            recordcount = 0;
            return (userId > 0 && pageIndex > 0 && pageSize > 0) ? STA.Data.PrivateMessages.GetPrivateMessageList(userId, folder, pageSize, pageIndex, intType, out pagecount, out recordcount) : (new DataTable());
        }

        /// <summary>
        /// ��ù�����Ϣ�б�
        /// </summary>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <returns>������Ϣ�б�</returns>
        public static List<PrivateMessageInfo> GetAnnouncePrivateMessageList(int pagesize, int pageindex)
        {
            if (pagesize == -1)
                return STA.Data.PrivateMessages.GetAnnouncePrivateMessageList(-1, 0);
            return (pagesize > 0 && pageindex > 0) ? STA.Data.PrivateMessages.GetAnnouncePrivateMessageList(pagesize, pageindex) : null;
        }

        /// <summary>
        /// ���ض̱�����ռ������Ϣ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="pagesize">ÿҳ��ʾ����Ϣ��</param>
        /// <param name="pageindex">��ǰҪ��ʾ��ҳ��</param>
        /// <param name="strwhere">ɸѡ����</param>
        /// <returns>�ռ������Ϣ�б�</returns>
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
        /// �������µ�һ����¼ID
        /// </summary>
        /// <param name="userId">�û�ID</param>
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
        /// ��ȡɾ������Ϣ������
        /// </summary>
        /// <param name="isNew">�Ƿ�ɾ���¶���Ϣ</param>
        /// <param name="postDateTime">��������</param>
        /// <param name="msgFromList">�������б�</param>
        /// <param name="lowerUpper">�Ƿ����ִ�Сд</param>
        /// <param name="subject">����</param>
        /// <param name="message">����</param>
        /// <param name="isUpdateUserNewPm">�Ƿ�����û�����Ϣ��</param>
        /// <returns></returns>
        public static string DelPrivateMessages(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm)
        {
            return Data.PrivateMessages.DelPrivateMessages(isNew, postDateTime, msgFromList, lowerUpper, subject, message, isUpdateUserNewPm);
        }
    }
}
