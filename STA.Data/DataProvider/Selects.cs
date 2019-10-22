using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Selects
    {
        public static int AddSelect(SelectInfo info)
        {
            return DatabaseProvider.GetInstance().AddSelect(info);
        }

        public static bool EditSelect(SelectInfo info)
        {
            return DatabaseProvider.GetInstance().EditSelect(info) > 0;
        }

        public static bool DelSelect(int id)
        {
            return DatabaseProvider.GetInstance().DelSelect(id) > 0;
        }

        public static string GetSelectSearchCondition(string ename, float maxvalue, float minvalue)
        {
            return DatabaseProvider.GetInstance().GetSelectSearchCondition(ename, maxvalue, minvalue);
        }

        public static DataTable GetSelectDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetSelectDataPage(pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static int AddSelectType(SelecttypeInfo info)
        {
            return DatabaseProvider.GetInstance().AddSelectType(info);
        }

        public static bool EditSelectType(SelecttypeInfo info)
        {
            return DatabaseProvider.GetInstance().EditSelectType(info) > 0;
        }

        public static bool DelSelectType(int id)
        {
            return DatabaseProvider.GetInstance().DelSelectType(id) > 0;
        }

        public static DataTable GetSelectTypeDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetSelectTypeDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static bool DelSelectByEname(string ename)
        {
            return DatabaseProvider.GetInstance().DelSelectByEname(ename) > 0;
        }

        public static DataTable GetSelectTypeTable()
        {
            return DatabaseProvider.GetInstance().GetSelectTypeTable();
        }

        public static SelectInfo GetSelect(int id)
        {
            SelectInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetSelect(id))
            {
                if (reader.Read())
                {
                    info = new SelectInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString().Trim();
                    info.Value = reader["value"].ToString().Trim();
                    info.Ename = reader["ename"].ToString().Trim();
                    info.Orderid = TypeParse.StrToInt(reader["orderid"]);
                    info.Issign = byte.Parse(reader["issign"].ToString());
                }
            }
            return info;
        }

        public static SelecttypeInfo GetSelectType(int id)
        {
            SelecttypeInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetSelectType(id))
            {
                if (reader.Read())
                {
                    info = new SelecttypeInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString();
                    info.Ename = reader["ename"].ToString();
                    info.Issign = byte.Parse(reader["issign"].ToString());
                    info.System = byte.Parse(reader["system"].ToString());
                }
            }
            return info;
        }

        public static DataTable GetSelectByWhere(string where)
        {
            return DatabaseProvider.GetInstance().GetSelectByWhere(where);
        }

        public static string SelectName(string ename, string value)
        {
            return DatabaseProvider.GetInstance().SelectName(ename, value);
        }
    }
}
