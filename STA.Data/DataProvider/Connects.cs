using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Connects
    {
        #region Appconnect
        //public static int AddAppconnect(AppconnectInfo info)
        //{
        //    return DatabaseProvider.GetInstance().AddAppconnect(info);
        //}

        //public static int EditAppconnect(AppconnectInfo info)
        //{
        //    return DatabaseProvider.GetInstance().EditAppconnect(info);
        //}

        //public static bool DelAppconnect(int id)
        //{
        //    return DatabaseProvider.GetInstance().DelAppconnect(id) > 0;
        //}

        //public static AppconnectInfo GetAppconnect(int id)
        //{
        //    AppconnectInfo info = null;
        //    using (IDataReader reader = DatabaseProvider.GetInstance().GetAppconnect(id))
        //    {
        //        if (reader.Read())
        //        {
        //            info = LoadAppconnectInfo(reader);
        //        }
        //    }
        //    return info;
        //}

        //public static AppconnectInfo GetAppconnect(string identify)
        //{
        //    AppconnectInfo info = null;
        //    using (IDataReader reader = DatabaseProvider.GetInstance().GetAppconnect(identify))
        //    {
        //        if (reader.Read())
        //        {
        //            info = LoadAppconnectInfo(reader);
        //        }
        //    }
        //    return info;
        //}

        //private static AppconnectInfo LoadAppconnectInfo(IDataReader reader)
        //{
        //    AppconnectInfo info = new AppconnectInfo();
        //    info.Id = TypeParse.StrToInt(reader["id"], 0);
        //    info.Name = TypeParse.ObjToString(reader["name"]).Trim();
        //    info.Description = TypeParse.ObjToString(reader["description"]).Trim();
        //    info.Identify = TypeParse.ObjToString(reader["identify"]).Trim();
        //    info.Isvalid = byte.Parse(TypeParse.ObjToString(reader["isvalid"]));
        //    info.Img = TypeParse.ObjToString(reader["img"]).Trim();
        //    info.Appid = TypeParse.ObjToString(reader["appid"]).Trim();
        //    info.Appkey = TypeParse.ObjToString(reader["appkey"]).Trim();
        //    info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
        //    info.Ext = TypeParse.ObjToString(reader["ext"]).Trim();
        //    return info;
        //}

        //public static DataTable GetAppconnectDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        //{
        //    return DatabaseProvider.GetInstance().GetAppconnectDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        //}
        #endregion

        #region Userconnect
        public static int AddUserconnect(UserconnectInfo info)
        {
            return DatabaseProvider.GetInstance().AddUserconnect(info);
        }

        public static int EditUserconnect(UserconnectInfo info)
        {
            return DatabaseProvider.GetInstance().EditUserconnect(info);
        }

        public static bool DelUserconnect(int id)
        {
            return DatabaseProvider.GetInstance().DelUserconnect(id) > 0;
        }

        public static UserconnectInfo GetUserconnect(int uid, string identify)
        {
            UserconnectInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUserconnect(uid, identify))
            {
                if (reader.Read())
                {
                    info = LoadUserconnectInfo(reader);
                }
            }
            return info;
        }


        public static UserconnectInfo GetUserconnect(String openid, string identify)
        {
            UserconnectInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUserconnect(openid, identify))
            {
                if (reader.Read())
                {
                    info = LoadUserconnectInfo(reader);
                }
            }
            return info;
        }


        private static UserconnectInfo LoadUserconnectInfo(IDataReader reader)
        {
            UserconnectInfo info = new UserconnectInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Appidentify = TypeParse.ObjToString(reader["appidentify"]).Trim();
            info.Openid = TypeParse.ObjToString(reader["openid"]).Trim();
            info.Token = TypeParse.ObjToString(reader["token"]).Trim();
            info.Secret = TypeParse.ObjToString(reader["secret"]).Trim();
            info.Ext = TypeParse.ObjToString(reader["ext"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            return info;
        }

        public static DataTable GetUserconnectDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetUserconnectDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion
		

    }
}
