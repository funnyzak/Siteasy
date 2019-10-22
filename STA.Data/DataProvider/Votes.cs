using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Votes
    {
        public static int AddVotecate(VotecateInfo info)
        {
            return DatabaseProvider.GetInstance().AddVotecate(info);
        }

        public static bool DelVotecate(int id)
        {
            return DatabaseProvider.GetInstance().DelVotecate(id) > 0;
        }

        public static bool EditVotecate(VotecateInfo info)
        {
            return DatabaseProvider.GetInstance().EditVotecate(info) > 0;
        }

        public static DataTable VoteLikeIds()
        {
            return DatabaseProvider.GetInstance().VoteLikeIds();
        }

        public static DataTable GetVoteCateTable()
        {
            return DatabaseProvider.GetInstance().GetVoteCateTable();
        }

        public static VotecateInfo GetVotecate(int id)
        {
            VotecateInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetVotecate(id))
            {
                if (reader.Read())
                {
                    info = new VotecateInfo();
                    info.Id = TypeParse.StrToInt(reader["id"], 0);
                    info.Name = reader["name"].ToString();
                    info.Ename = reader["ename"].ToString();
                }
            }
            return info;
        }


        public static DataTable GetVoteOptionDataTable(int id, string fields, string orderby, string sort)
        {
            return DatabaseProvider.GetInstance().GetVoteOptionDataTable(id, fields, orderby, sort);
        }

        public static DataTable GetVotecateDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetVotecateDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static int AddVotetopic(VotetopicInfo info)
        {
            return DatabaseProvider.GetInstance().AddVotetopic(info);
        }

        public static bool DelVotetopic(int id)
        {
            return DatabaseProvider.GetInstance().DelVotetopic(id) > 0;
        }

        public static bool EditVotetopic(VotetopicInfo info)
        {
            return DatabaseProvider.GetInstance().EditVotetopic(info) > 0;
        }

        public static VotetopicInfo GetVotetopic(int id)
        {
            VotetopicInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetVotetopic(id))
            {
                if (reader.Read())
                {
                    info = new VotetopicInfo();
                    info.Id = TypeParse.StrToInt(reader["id"], 0);
                    info.Name = reader["name"].ToString();
                    info.Desc = reader["desc"].ToString();
                    info.Cateid = TypeParse.StrToInt(reader["cateid"]);
                    info.Catename = reader["catename"].ToString();
                    info.Type = byte.Parse(reader["type"].ToString());
                    info.Img = reader["img"].ToString();
                    info.Likeid = reader["likeid"].ToString();
                    info.Maxvote = TypeParse.StrToInt(reader["maxvote"], 1);
                    info.Endtime = TypeParse.StrToDateTime(reader["endtime"]);
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Endtext = reader["endtext"].ToString();
                    info.Voted = reader["voted"].ToString();
                    info.Votecount = TypeParse.StrToInt(reader["votecount"]);
                    info.Orderid = TypeParse.StrToInt(reader["orderid"]);
                    info.Isinfo = TypeParse.StrToInt(reader["isinfo"]);
                    info.Isvcode = TypeParse.StrToInt(reader["isvcode"]);
                    info.Islogin = TypeParse.StrToInt(reader["islogin"]);
                }
            }
            return info;
        }

        public static int DelVoterecordWhere(string where)
        {
            return DatabaseProvider.GetInstance().DelVoterecordWhere(where);
        }

        public static DataTable GetVotetopicDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetVotetopicDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static string GetVotetopicSearchCondition(string startdate, string enddate, int cateid, int type, string keywords)
        {
            return DatabaseProvider.GetInstance().GetVotetopicSearchCondition(startdate, enddate, cateid, type, keywords);
        }

        public static int AddVoteoption(VoteoptionInfo info)
        {
            return DatabaseProvider.GetInstance().AddVoteoption(info);
        }

        public static bool DelVoteoption(int id)
        {
            return DatabaseProvider.GetInstance().DelVoteoption(id) > 0;
        }

        public static bool EditVoteoption(VoteoptionInfo info)
        {
            return DatabaseProvider.GetInstance().EditVoteoption(info) > 0;
        }

        public static VoteoptionInfo GetVoteoption(int id)
        {
            VoteoptionInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetVoteoption(id))
            {
                if (reader.Read())
                {
                    info = new VoteoptionInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString();
                    info.Desc = reader["desc"].ToString();
                    info.Topicname = reader["topicname"].ToString();
                    info.Topicid = TypeParse.StrToInt(reader["topicid"]);
                    info.Img = reader["img"].ToString();
                    info.Count = TypeParse.StrToInt(reader["count"]);
                    info.Orderid = TypeParse.StrToInt(reader["orderid"]);
                }
            }
            return info;
        }

        public static DataTable GetVoteoptionDataPage(int pagecurrent, int pagesize, int topicid, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetVoteoptionDataPage(pagecurrent, pagesize, topicid, out pagecount, out recordcount);
        }

        public static int AddVoterecord(VoterecordInfo info)
        {
            return DatabaseProvider.GetInstance().AddVoterecord(info);
        }

        public static bool DelVoterecord(int id)
        {
            return DatabaseProvider.GetInstance().DelVoterecord(id) > 0;
        }

        public static DataTable GetVoterecordDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetVoterecordDataPage(pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static string GetVoterecordSearchCondition(string startdate, string enddate, string ip, int topicid, string idcard, string phone, string keywords)
        {
            return DatabaseProvider.GetInstance().GetVoterecordSearchCondition(startdate, enddate, ip, topicid, idcard, phone, keywords);
        }

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="items">选项集合,半角逗号分开</param>
        /// <returns></returns>
        public static bool Vote(int topicid, string items)
        {
            return DatabaseProvider.GetInstance().Vote(topicid, items) > 0;
        }

        public static DataTable GetVoteByLikeid(string likeid)
        {
            return DatabaseProvider.GetInstance().GetVoteByLikeid(likeid);
        }

        public static DataTable GetVoteByIds(string ids)
        {
            return DatabaseProvider.GetInstance().GetVoteByIds(ids);
        }

        /// <summary>
        /// 检查Ip是否在特定分钟内进行了投票
        /// </summary>
        public static int VoteRecordIpTimeInterval(string ip, int topicid, int minute)
        {
            return DatabaseProvider.GetInstance().VoteRecordIpTimeInterval(ip, topicid, minute);
        }
    }
}
