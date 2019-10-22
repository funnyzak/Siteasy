using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity.Plus;

namespace STA.Data
{
    public class Plus
    {
        public static DataTable GetStaVoteTable(int pageIndex, string where, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetStaVoteTable(pageIndex, where, out pagecount, out recordcount);
        }

        public static int AddStaVote(STA.Entity.Plus.VoteInfo info)
        {
            return DatabaseProvider.GetInstance().AddStaVote(info);
        }

        public static int EditStaVote(STA.Entity.Plus.VoteInfo info)
        {
            return DatabaseProvider.GetInstance().EditStaVote(info);
        }

        public static int DelStaVote(int id)
        {
            return DatabaseProvider.GetInstance().DelStaVote(id);
        }

        public static VoteInfo GetStaVote(int id)
        {
            VoteInfo info = null;
            using (IDataReader dr = DatabaseProvider.GetInstance().GetStaVote(id))
            {
                if (dr.Read())
                {
                    info = new VoteInfo();
                    info.Id = TypeParse.StrToInt(dr["id"], 0);
                    info.Title = dr["title"].ToString();
                    info.StartDate = Convert.ToDateTime(dr["startdate"]);
                    info.EndDate = Convert.ToDateTime(dr["enddate"]);
                    info.IsMore = TypeParse.StrToInt(dr["ismore"], 0);
                    info.IsView = TypeParse.StrToInt(dr["isview"], 0);
                    info.IsEnable = TypeParse.StrToInt(dr["isenable"], 0);
                    info.Interval = TypeParse.StrToInt(dr["interval"], 0);
                    info.Items = dr["items"].ToString();
                }
            }
            return info;
        }

        public static bool UpdateStaVoteCount(int vid, int iid)
        {
            return DatabaseProvider.GetInstance().UpdateStaVoteCount(vid, iid);
        }

        public static int AddVariables(VariableInfo info)
        {
            if (VariableKeyExist(info.Key)) return 0;

            return DatabaseProvider.GetInstance().AddVariables(info);
        }

        public static bool EditVariable(VariableInfo info)
        {
            VariableInfo tinfo = GetVariable(info.Id);
            if (tinfo != null && tinfo.Key != info.Key && VariableKeyExist(info.Key)) 
                return false;

            return DatabaseProvider.GetInstance().EditVariable(info) > 0;
        }

        public static int DelVariableByKey(string key)
        {
            return DatabaseProvider.GetInstance().DelVariableByKey(key);
        }

        public static bool DelVariable(int id)
        {
            return DatabaseProvider.GetInstance().DelVariable(id) > 0;
        }

        public static bool DelVariable(string likeid)
        {
            return DatabaseProvider.GetInstance().DelVariable(likeid) > 0;
        }

        public static VariableInfo GetVariable(int id)
        {
            VariableInfo info = null;
            using (IDataReader dr = DatabaseProvider.GetInstance().GetVariable(id))
            {
                if (dr.Read())
                    info = ReadVariable(dr);
            }
            return info;
        }

        public static VariableInfo GetVariable(string key)
        {
            VariableInfo info = null;
            using (IDataReader dr = DatabaseProvider.GetInstance().GetVariable(key))
            {
                if (dr.Read())
                    info = ReadVariable(dr);
            }
            return info;
        }

        private static VariableInfo ReadVariable(IDataReader dr)
        {
            VariableInfo info = new VariableInfo();
            info.Id = TypeParse.StrToInt(dr["id"], 0);
            info.Name = dr["name"].ToString();
            info.Likeid = dr["likeid"].ToString();
            info.Key = dr["key"].ToString();
            info.Desc = dr["desc"].ToString();
            info.KValue = dr["value"].ToString();
            info.System = byte.Parse(dr["system"].ToString());
            return info;
        }

        public static DataTable GetVariableList(string fields, string likeid)
        {
            return DatabaseProvider.GetInstance().GetVariableList(fields, likeid);
        }

        public static DataTable GetVariableLikeidList()
        {
            return DatabaseProvider.GetInstance().GetVariableLikeidList();
        }

        public static bool VariableKeyExist(string key)
        {
            return DatabaseProvider.GetInstance().ExistVariableKey(key);
        }

        public static string GetVariableCondition(string name, string likeid, string key)
        {
            return DatabaseProvider.GetInstance().GetVariableCondition(name, likeid, key);
        }

        public static DataTable GetVariableDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetVariableDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
    }
}
