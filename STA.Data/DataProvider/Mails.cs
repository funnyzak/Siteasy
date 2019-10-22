using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Mails
    {
        #region Mailog
        public static int AddMailog(MailogInfo info)
        {
            return DatabaseProvider.GetInstance().AddMailog(info);
        }

        public static int EditMailog(MailogInfo info)
        {
            return DatabaseProvider.GetInstance().EditMailog(info);
        }

        public static bool DelMailog(int id)
        {
            return DatabaseProvider.GetInstance().DelMailog(id) > 0;
        }

        public static MailogInfo GetMailog(int id)
        {
            MailogInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetMailog(id))
            {
                if (reader.Read())
                {
                    info = LoadMailogInfo(reader);
                }
            }
            return info;
        }

        private static MailogInfo LoadMailogInfo(IDataReader reader)
        {
            MailogInfo info = new MailogInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Rgroup = TypeParse.ObjToString(reader["rgroup"]).Trim();
            info.Mails = TypeParse.ObjToString(reader["mails"]).Trim();
            info.Title = TypeParse.ObjToString(reader["title"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Content = TypeParse.ObjToString(reader["content"]).Trim();
            info.Userid = TypeParse.StrToInt(reader["userid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            return info;
        }

        public static DataTable GetMailogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetMailogDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static string GetMailogSearchCondition(string title, string startdate, string enddate, string users)
        {
            return DatabaseProvider.GetInstance().GetMailogSearchCondition(title, startdate, enddate, users);
        }
        #endregion

        #region Submail
        public static string AddSubmail(MailsubInfo info)
        {
            if (!Validator.IsEmail(info.Mail.Trim()))
                return "邮件地址有误,请检查";

            if (info.Ip.Trim() == "") info.Ip = STARequest.GetIP();
            if (info.Safecode.Trim() == "") info.Safecode = Guid.NewGuid().ToString();
            if (info.Forgroup.Trim() == "") info.Forgroup = "默认";

            MailsubInfo sminfo = GetSubmail(info.Mail);
            if (sminfo == null)
            {
                DatabaseProvider.GetInstance().AddSubmail(info);
                return "";
            }
            else
            {
                if (sminfo.Status == 0)
                {
                    sminfo.Status = 1;
                    sminfo.Safecode = Guid.NewGuid().ToString();
                    EditSubmail(sminfo);
                    return "";
                }
                else
                {
                    return "该邮件已经订阅过了";
                }
            }
        }

        public static int EditSubmail(MailsubInfo info)
        {
            if (info.Safecode.Trim() == "") info.Safecode = Guid.NewGuid().ToString();
            if (info.Forgroup.Trim() == "") info.Forgroup = "默认";
            return DatabaseProvider.GetInstance().EditSubmail(info);
        }

        public static bool DelSubmail(string mail)
        {
            return DatabaseProvider.GetInstance().DelSubmail(mail) > 0;
        }

        public static DataTable GetSubmailGroups()
        {
            return DatabaseProvider.GetInstance().GetSubmailGroups();
        }

        public static DataTable GetSubMailList(string fields)
        {
            return DatabaseProvider.GetInstance().GetSubMailList(fields);
        }

        public static MailsubInfo GetSubmail(string mail)
        {
            MailsubInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetSubmail(mail))
            {
                if (reader.Read())
                {
                    info = LoadSubmailInfo(reader);
                }
            }
            return info;
        }

        private static MailsubInfo LoadSubmailInfo(IDataReader reader)
        {
            MailsubInfo info = new MailsubInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Name = TypeParse.ObjToString(reader["name"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Mail = TypeParse.ObjToString(reader["mail"]).Trim();
            info.Ip = TypeParse.ObjToString(reader["ip"]).Trim();
            info.Status = byte.Parse(TypeParse.ObjToString(reader["status"]));
            info.Safecode = TypeParse.ObjToString(reader["safecode"]).Trim();
            info.Forgroup = TypeParse.ObjToString(reader["forgroup"]).Trim();
            return info;
        }

        public static DataTable GetSubmailDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetSubmailDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static string GetSubmailSearchCondition(int status, string name, string mail, string startdate, string enddate, string ip, string forgroup)
        {
            return DatabaseProvider.GetInstance().GetSubmailSearchCondition(status, name, mail, startdate, enddate, ip, forgroup);
        }
        #endregion
    }
}
