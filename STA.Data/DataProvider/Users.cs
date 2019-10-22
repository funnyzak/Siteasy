using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Users
    {
        public static int AddUser(UserInfo info)
        {
            return DatabaseProvider.GetInstance().AddUser(info);
        }

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmnum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        public static int SetUserNewPMCount(int uid, int pmnum)
        {
            return DatabaseProvider.GetInstance().SetUserNewPMCount(uid, pmnum);
        }

        public static int AddUser(UserInfo info, UserfieldInfo finfo)
        {
            finfo.Uid = AddUser(info);
            if (finfo.Uid <= 0) return 0;
            return AddUserField(finfo);
        }

        public static DataTable GetUserListByUids(string fields, string uids)
        {
            if (!Utils.IsNumericList(uids)) return null;
            return DatabaseProvider.GetInstance().GetUserListByUids(fields, uids);
        }

        public static DataTable GetUserListByUsernames(string fields, string usernames)
        {
            return DatabaseProvider.GetInstance().GetUserListByUsernames(fields, usernames);
        }

        public static bool DelUser(int id)
        {
            return DatabaseProvider.GetInstance().DelUser(id) > 0;
        }

        public static bool EditUser(UserInfo info)
        {
            return DatabaseProvider.GetInstance().EditUser(info) > 0;
        }

        public static UserInfo GetUser(int id)
        {
            return GetUser(DatabaseProvider.GetInstance().GetUser(id));
        }

        public static UserInfo GetUser(string username)
        {
            return GetUser(DatabaseProvider.GetInstance().GetUser(username));
        }

        public static int ExistUser(string username)
        {
            return DatabaseProvider.GetInstance().ExistUser(username);
        }

        public static UserInfo CheckUserLogin(string username, string password, int system)
        {
            return GetUser(DatabaseProvider.GetInstance().CheckUserLogin(username, password, system)); ;
        }

        private static UserInfo GetUser(IDataReader reader)
        {
            UserInfo info = null;
            if (reader.Read())
            {
                info = new UserInfo();
                info.Id = TypeParse.StrToInt(reader["id"], 0);
                info.Username = TypeParse.ObjToString(reader["username"]).Trim();
                info.Nickname = TypeParse.ObjToString(reader["nickname"]).Trim();
                info.Password = TypeParse.ObjToString(reader["password"]).Trim();
                info.Safecode = TypeParse.ObjToString(reader["safecode"]).Trim();
                info.Spaceid = TypeParse.StrToInt(reader["spaceid"], 0);
                info.Gender = byte.Parse(TypeParse.ObjToString(reader["gender"]));
                info.Birthday = TypeParse.StrToDateTime(reader["birthday"]);
                info.Adminid = TypeParse.StrToInt(reader["adminid"], 0);
                info.Admingroupname = TypeParse.ObjToString(reader["admingroupname"]).Trim();
                info.Groupid = TypeParse.StrToInt(reader["groupid"], 0);
                info.Groupname = TypeParse.ObjToString(reader["groupname"]).Trim();
                info.Extgroupids = TypeParse.ObjToString(reader["extgroupids"]).Trim();
                info.Regip = TypeParse.ObjToString(reader["regip"]).Trim();
                info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                info.Loginip = TypeParse.ObjToString(reader["loginip"]).Trim();
                info.Logintime = TypeParse.StrToDateTime(reader["logintime"]);
                info.Lastaction = TypeParse.StrToDateTime(reader["lastaction"]);
                info.Money = decimal.Parse(reader["money"].ToString());
                info.Credits = TypeParse.StrToInt(reader["credits"], 0);
                info.Extcredits1 = TypeParse.StrToInt(reader["extcredits1"], 0);
                info.Extcredits2 = TypeParse.StrToInt(reader["extcredits2"], 0);
                info.Extcredits3 = TypeParse.StrToInt(reader["extcredits3"], 0);
                info.Extcredits4 = TypeParse.StrToInt(reader["extcredits4"], 0);
                info.Extcredits5 = TypeParse.StrToInt(reader["extcredits5"], 0);
                info.Email = TypeParse.ObjToString(reader["email"]).Trim();
                info.Ischeck = byte.Parse(TypeParse.ObjToString(reader["ischeck"]));
                info.Locked = byte.Parse(TypeParse.ObjToString(reader["locked"]));
                info.Newpm = byte.Parse(TypeParse.ObjToString(reader["newpm"]));
                info.Newpmcount = TypeParse.StrToInt(reader["newpmcount"], 0);
                info.Onlinestate = byte.Parse(TypeParse.ObjToString(reader["onlinestate"]));
                info.Invisible = byte.Parse(TypeParse.ObjToString(reader["Invisible"]));
                info.Showemail = byte.Parse(TypeParse.ObjToString(reader["showemail"]));
                return info;
            }
            reader.Close();
            return info;
        }

        public static int AddUserField(UserfieldInfo info)
        {
            return DatabaseProvider.GetInstance().AddUserField(info);
        }

        public static bool DelUserField(int uid)
        {
            return DatabaseProvider.GetInstance().DelUserField(uid) > 0;
        }

        public static bool EditUserField(UserfieldInfo info)
        {
            return DatabaseProvider.GetInstance().EditUserField(info) > 0;
        }


        private static UserfieldInfo GetUserfield(IDataReader reader)
        {
            UserfieldInfo info = null;
            if (reader.Read())
            {
                info = new UserfieldInfo();
                info.Id = TypeParse.StrToInt(reader["id"]);
                info.Uid = TypeParse.StrToInt(reader["uid"]); ;
                info.Realname = reader["realname"].ToString().Trim();
                info.Idcard = reader["idcard"].ToString().Trim();
                info.Signature = reader["signature"].ToString().Trim();
                info.Description = reader["description"].ToString().Trim();
                info.Areaid = TypeParse.StrToInt(reader["areaid"]);
                info.Areaname = reader["areaname"].ToString().Trim();
                info.Address = reader["address"].ToString().Trim();
                info.Postcode = reader["postcode"].ToString().Trim();
                info.Hometel = reader["hometel"].ToString().Trim();
                info.Worktel = reader["worktel"].ToString().Trim();
                info.Mobile = reader["mobile"].ToString().Trim();
                info.Icq = reader["icq"].ToString().Trim();
                info.Qq = reader["qq"].ToString().Trim();
                info.Skype = reader["skype"].ToString().Trim();
                info.Msn = reader["msn"].ToString().Trim();
                info.Website = reader["website"].ToString().Trim();
                info.Ucid = TypeParse.StrToInt(reader["ucid"]);
            }
            return info;
        }

        public static UserfieldInfo GetUserFieldByUcid(int ucid)
        {
            return GetUserfield(DatabaseProvider.GetInstance().GetUserFieldByUcid(ucid));
        }
        public static UserfieldInfo GetUserField(int uid)
        {
            return GetUserfield(DatabaseProvider.GetInstance().GetUserField(uid));
        }

        public static DataTable GetUserDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetUserDataPage(pagecurrent, pagesize, where, out pagecount, out recordcount);
        }

        public static int AddUserGroup(UserGroupInfo info)
        {
            return DatabaseProvider.GetInstance().AddUserGroup(info);
        }

        public static bool EditUserGroup(UserGroupInfo info)
        {
            return DatabaseProvider.GetInstance().EditUserGroup(info) > 0;
        }

        public static bool DelUserGroup(int id)
        {
            if (id <= 2) return false;
            return DatabaseProvider.GetInstance().DelUserGroup(id) > 0;
        }
        public static UserGroupInfo GetUserGroup(int id)
        {
            UserGroupInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUserGroup(id))
            {
                if (reader.Read())
                {
                    info = LoadUserGroup(reader);
                }
            }
            return info;
        }

        public static UserGroupInfo GetUserGroupZeroScoreId()
        {
            UserGroupInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUserGroupZeroScoreId())
            {
                if (reader.Read())
                {
                    info = LoadUserGroup(reader);
                }
            }
            return info;
        }

        private static UserGroupInfo LoadUserGroup(IDataReader reader)
        {
            UserGroupInfo info = new UserGroupInfo();
            info = new UserGroupInfo();
            info.Id = TypeParse.StrToInt(reader["id"]);
            info.System = TypeParse.StrToInt(reader["system"]);
            info.Name = reader["name"].ToString();
            info.Creditsmax = TypeParse.StrToInt(reader["creditsmax"]);
            info.Creditsmin = TypeParse.StrToInt(reader["creditsmin"]);
            info.Color = reader["color"].ToString().Trim();
            info.Avatar = reader["avatar"].ToString().Trim();
            info.Star = TypeParse.StrToInt(reader["star"]);
            return info;
        }

        public static DataTable GetUserGroupDataPage(int pagecurrent, int pagesize, int system, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetUserGroupDataPage(pagecurrent, pagesize, system, out pagecount, out recordcount);
        }

        public static DataTable GetUserGroupDataPage(int system)
        {
            int pagecount = 10, recordcount = 10;
            return GetUserGroupDataPage(1, 100000, system, out pagecount, out recordcount);
        }

        public static int AddAdminLog(AdminLogInfo info)
        {
            return DatabaseProvider.GetInstance().AddAdminLog(info);
        }

        public static bool DelAdminLog(int id)
        {
            return DatabaseProvider.GetInstance().DelAdminLog(id) > 0;
        }

        public static string GetAdminlogSearchCondition(int admintype, string startdate, string enddate, string users, string keywords)
        {
            return DatabaseProvider.GetInstance().GetAdminlogSearchCondition(admintype, startdate, enddate, users, keywords);
        }

        public static DataTable GetAdminLogDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetAdminLogDataPage(pagecurrent, pagesize, where, out pagecount, out recordcount);
        }

        public static int DelAdminlogWhere(string where)
        {
            return DatabaseProvider.GetInstance().DelAdminlogWhere(where);
        }

        public static object LastLoginTime(int uid)
        {
            return DatabaseProvider.GetInstance().LastLoginTime(uid);
        }

        public static int CheckUserEmailExist(string email)
        {
            return DatabaseProvider.GetInstance().CheckUserEmailExist(email);
        }

        public static int CheckUserNameExist(string username)
        {
            return DatabaseProvider.GetInstance().CheckUserNameExist(username);
        }

        public static int AddUserauth(UserauthInfo info)
        {
            return DatabaseProvider.GetInstance().AddUserauth(info);
        }

        public static UserauthInfo GetUserauth(string code, AuthType atype)
        {
            return GetUserauth(DatabaseProvider.GetInstance().GetUserauth(code, atype));
        }

        public static UserauthInfo GetUserauthByUsername(string username, AuthType atype)
        {
            return GetUserauth(DatabaseProvider.GetInstance().GetUserauthByUsername(username, atype));
        }

        public static DataTable GetUserListByGroupid(int top, string fields, int start_uid, string groupIdList)
        {
            if (groupIdList == null && !Utils.IsNumericList(groupIdList))
            {
                return null;
            }
            return DatabaseProvider.GetInstance().GetUserListByGroupid(top, fields, start_uid, groupIdList);
        }

        private static UserauthInfo GetUserauth(IDataReader reader)
        {
            UserauthInfo info = null;
            if (reader.Read())
            {
                info = new UserauthInfo();
                info.Id = TypeParse.StrToInt(reader["id"]);
                info.Userid = TypeParse.StrToInt(reader["userid"]);
                info.Username = reader["username"].ToString();
                info.Email = reader["email"].ToString();
                info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                info.Atype = (AuthType)TypeParse.StrToInt(reader["atype"]);
                info.Expirs = TypeParse.StrToInt(reader["expirs"]);
                info.Code = reader["code"].ToString();
            }
            reader.Close();
            return info;
        }

        public static bool DelUserauth(int id)
        {
            return DatabaseProvider.GetInstance().DelUserauth(id) > 0;
        }

        public static DataTable GetUserGroupTable()
        {
            return DatabaseProvider.GetInstance().GetUserGroupTable();
        }

        #region Userlog
        public static int AddUserlog(UserlogInfo info)
        {
            return DatabaseProvider.GetInstance().AddUserlog(info);
        }

        public static int EditUserlog(UserlogInfo info)
        {
            return DatabaseProvider.GetInstance().EditUserlog(info);
        }

        public static bool DelUserlog(int id)
        {
            return DatabaseProvider.GetInstance().DelUserlog(id) > 0;
        }

        public static UserlogInfo GetUserlog(int id)
        {
            UserlogInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUserlog(id))
            {
                if (reader.Read())
                {
                    info = LoadUserlogInfo(reader);
                }
            }
            return info;
        }

        private static UserlogInfo LoadUserlogInfo(IDataReader reader)
        {
            UserlogInfo info = new UserlogInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            info.Ip = TypeParse.ObjToString(reader["ip"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Action = TypeParse.ObjToString(reader["action"]).Trim();
            info.Value = TypeParse.StrToInt(reader["value"], 0);
            info.Remark = TypeParse.ObjToString(reader["remark"]).Trim();
            info.Identify = TypeParse.ObjToString(reader["identify"]).Trim();
            return info;
        }

        public static DataTable GetUserlogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetUserlogDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion

    }
}
