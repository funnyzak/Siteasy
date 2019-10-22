using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS.Web.UCenter.Api;
using DS.Web.UCenter;

using STA.Core;
using STA.Entity;
using STA.Common;
using STA.Data;
using STA.Core.Api;
namespace STA.Web.api
{
    /// <summary>
    /// ucenter 的摘要说明
    /// </summary>
    public class ucenter : UcApiBase
    {

        public override ApiReturn DeleteUser(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                UserfieldInfo ufinfo = Users.GetUserFieldByUcid(id);
                if (ufinfo != null)
                {
                    Users.DelUser(ufinfo.Uid);
                }
            }

            return ApiReturn.Success;
        }

        public override ApiReturn RenameUser(int uid, string oldUserName, string newUserName)
        {
            throw new NotImplementedException();
        }

        public override UcTagReturns GetTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn SynLogin(int uid)
        {
            UserfieldInfo ufinfo = Users.GetUserFieldByUcid(uid);
            if (ufinfo != null)
            {
                OnlineUserInfo oluser = ConUtils.GetOnlineUser();
                if (oluser.Userid <= 0)
                {
                    UserInfo uinfo = Users.GetUser(ufinfo.Uid);
                    Globals.UpdateLoginStatus(uinfo);
                    ConUtils.WriteUserCookie(uinfo, 100000);
                }
            }
            return ApiReturn.Success;
        }

        public override ApiReturn SynLogout()
        {
            ConUtils.ClearUserCookie();
            return ApiReturn.Success;
        }

        public override ApiReturn UpdatePw(string userName, string passWord)
        {
            UserInfo uinfo = Users.GetUser(userName);
            if (uinfo != null && passWord != "")
            {
                uinfo.Password = Utils.MD5(passWord);
                Users.EditUser(uinfo);
            }
            //else if (uinfo == null && passWord != "")
            //{
            //    UcUserInfo ucinfo = UCenter.UserInfo(userName);
            //    if (ucinfo.Success)
            //    {
            //        uinfo = new UserInfo();
            //        uinfo.Username = userName;
            //        uinfo.Password = Utils.MD5(passWord);
            //        uinfo.Email = ucinfo.Mail;
            //        uinfo.Regip = STARequest.GetIP();
            //        uinfo = UserUtils.InitUserGroup(uinfo);

            //        uinfo.Id = Users.AddUser(uinfo);
            //        if (uinfo.Id > 0)
            //        {
            //            UserfieldInfo ufinfo = new UserfieldInfo();
            //            ufinfo.Ucid = ucinfo.Uid;
            //            ufinfo.Uid = uinfo.Id;
            //            Users.AddUserField(ufinfo);
            //        }
            //    }
            //}
            return ApiReturn.Success;
        }

        public override ApiReturn UpdateBadWords(UcBadWords badWords)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateHosts(UcHosts hosts)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateApps(UcApps apps)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateClient(UcClientSetting client)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateCredit(int uid, int credit, int amount)
        {
            throw new NotImplementedException();
        }

        public override UcCreditSettingReturns GetCreditSettings()
        {
            throw new NotImplementedException();
        }

        public override ApiReturn GetCredit(int uid, int credit)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateCreditSettings(UcCreditSettings creditSettings)
        {
            throw new NotImplementedException();
        }
    }
}