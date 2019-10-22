using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Cache;
using System.Collections.Generic;

namespace STA.Core
{
    public class UserLogs
    {
        public static int AddUserPointLog(int uid, string username, int value, string action, string remark)
        {
            UserlogInfo info = new UserlogInfo();
            info.Uid = uid;
            if (username == "") username = Users.GetUser(uid).Username;
            info.Username = username;
            info.Ip = STARequest.GetIP();
            info.Action = action;
            info.Remark = remark;
            info.Identify = "积分";
            info.Value = value;
            return Users.AddUserlog(info);
        }
    }
}
