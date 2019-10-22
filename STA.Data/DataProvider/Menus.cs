using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Menus
    {
        public static int AddMenu(MenuInfo info)
        {
            return DatabaseProvider.GetInstance().AddMenu(info);
        }

        public static bool DelMenu(int id)
        {
            return DatabaseProvider.GetInstance().DelMenu(id) > 0;
        }

        public static bool EditMenu(MenuInfo info)
        {
            return DatabaseProvider.GetInstance().EditMenu(info) > 0;
        }

        public static MenuInfo GetMenu(int id)
        {
            MenuInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetMenu(id))
            {
                if (reader.Read())
                {
                    info = new MenuInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString();
                    info.Parentid = TypeParse.StrToInt(reader["parentid"]);
                    info.System = byte.Parse(reader["system"].ToString());
                    info.Type = byte.Parse(reader["type"].ToString());
                    info.Icon = reader["icon"].ToString();
                    info.Url = reader["url"].ToString();
                    info.Target = reader["target"].ToString();
                    info.Orderid = TypeParse.StrToInt(reader["orderid"]);
                    info.Pagetype = (PageType)TypeParse.StrToInt(reader["pagetype"]);
                    info.Identify = TypeParse.ObjToString(reader["identify"]);
                }
            }
            return info;
        }
        public static DataTable GetMenuTable(int type)
        {
            return DatabaseProvider.GetInstance().GetMenuTable(type);
        }

        public static DataTable GetMenuTable(int type, PageType pagetype)
        {
            return DatabaseProvider.GetInstance().GetMenuTable(type, pagetype);
        }

        public static string GetMenuSearchCondition(int pagetype, int type, int system, string keyword)
        {
            return DatabaseProvider.GetInstance().GetMenuSearchCondition(pagetype, type, system, keyword);
        }

        public static DataTable GetMenuDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetMenuDataPage(pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static bool AddMenuRelation(int groupid, int menuid)
        {
            return DatabaseProvider.GetInstance().AddMenuRelation(groupid, menuid) > 0;
        }

        public static bool DelMenuRelation(int groupid, int menuid)
        {
            return DatabaseProvider.GetInstance().DelMenuRelation(groupid, menuid) > 0;
        }

        public static bool DelMenuRelation(int groupid)
        {
            return DatabaseProvider.GetInstance().DelMenuRelation(groupid) > 0;
        }

        public static DataTable GetMenuRelatetionsByGroupId(int groupid)
        {
            return DatabaseProvider.GetInstance().GetMenuRelatetionsByGroupId(groupid);
        }

        public static bool CheckPageAuthority(int groupid, string page)
        {
            return DatabaseProvider.GetInstance().CheckPageAuthority(groupid, page);
        }

        public static bool CheckPageAuthority(int groupid, int menuid)
        {
            return DatabaseProvider.GetInstance().CheckPageAuthority(groupid, menuid);
        }
    }
}
