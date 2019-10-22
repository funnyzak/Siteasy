using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Actions
    {
        public static int AddActionColumn(ActioncolumnInfo info)
        {
            return DatabaseProvider.GetInstance().AddActionColumn(info);
        }

        public static bool EditActionColumn(ActioncolumnInfo info)
        {
            return DatabaseProvider.GetInstance().EditActionColumn(info) > 0;
        }

        public static bool DelActionColumn(int id)
        {
            return DatabaseProvider.GetInstance().DelActionColumn(id) > 0;
        }

        public static ActioncolumnInfo GetActionColumn(int id)
        {
            ActioncolumnInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetActionColumn(id))
            {
                if (reader.Read())
                {
                    info = new ActioncolumnInfo();
                    info.Columnname = reader["columnname"].ToString();
                    info.Status = byte.Parse(reader["status"].ToString());
                }
            }
            return info;
        }

        public static int AddAction(ActionInfo info)
        {
            return DatabaseProvider.GetInstance().AddAction(info);
        }

        public static bool EditAction(ActionInfo info)
        {
            return DatabaseProvider.GetInstance().EditAction(info) > 0;
        }

        public static bool DelAction(int id)
        {
            return DatabaseProvider.GetInstance().DelAction(id) > 0;
        }

        public static ActionInfo GetAction(int id)
        { 
            ActionInfo info = null;
            using(IDataReader reader = DatabaseProvider.GetInstance().GetAction(id))
            {
                if (reader.Read())
                {
                    info = new ActionInfo();
                    info.Id = id;
                    info.Actionname = reader["actionname"].ToString().Trim();
                    info.Columnid = TypeParse.StrToInt(reader["columnid"]);
                    info.Action = reader["action"].ToString().Trim();
                    info.Status = byte.Parse(reader["status"].ToString());
                }
            }
            return info;
        }
        public static int AddActionGroup(ActiongroupInfo info)
        {
            return DatabaseProvider.GetInstance().AddActionGroup(info);
        }

        public static bool DelActionGroup(int id)
        {
            return DatabaseProvider.GetInstance().DelActionGroup(id) > 0;
        }

        public static bool CheckAction(int gid, string actionname)
        {
            return DatabaseProvider.GetInstance().CheckAction(gid, actionname.Trim()) > 0;
        }

        public static DataTable GetActionListByGroupId(int gid)
        {
            return DatabaseProvider.GetInstance().GetActionListByGroupId(gid);
        }

        public static DataTable GetActionColumns()
        {
            return DatabaseProvider.GetInstance().GetActionColumns();
        }

        public static DataTable GetActionDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetActionDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }
    }
}
