using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

using STA.Data;
using STA.Config;
using STA.Common;
using STA.Entity;
namespace STA.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        public int AddVotecate(VotecateInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createvotecate", BaseConfigs.GetTablePrefix), EntityVotecate(info)));
        }

        public int DelVotecate(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletevotecate", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id)));
        }

        public DataTable GetVoteOptionDataTable(int id, string fields, string orderby, string sort)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}voteoptions] WHERE topicid = {2} ORDER BY {3} {4}", fields == "" ? "*" : fields, BaseConfigs.GetTablePrefix, id, orderby, sort);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public DataTable VoteLikeIds()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT DISTINCT [likeid] FROM [{0}votetopics]", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public int EditVotecate(VotecateInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatevotecate", BaseConfigs.GetTablePrefix), EntityVotecate(info)));
        }

        public DataTable GetVoteCateTable()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM [{0}votecates]", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public IDataReader GetVotecate(int id)
        {
            string commandText = string.Format("SELECT * FROM [{0}votecates] WHERE [id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetVotecateDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "votecates", "id", pagecurrent, pagesize, "*", "id desc", "", out pagecount, out recordcount);
        }

        public int AddVotetopic(VotetopicInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createvotetopic", BaseConfigs.GetTablePrefix), EntityVotetopic(info)));
        }

        public int DelVotetopic(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletevotetopic", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id)));
        }

        public int EditVotetopic(VotetopicInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatevotetopic", BaseConfigs.GetTablePrefix), EntityVotetopic(info)));
        }

        public IDataReader GetVotetopic(int id)
        {
            string commandText = string.Format("SELECT * FROM [{0}votetopics] WHERE [id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetVotetopicDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "votetopics", "id", pagecurrent, pagesize, fields.Trim() == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }

        public string GetVotetopicSearchCondition(string startdate, string enddate, int cateid, int type, string keywords)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            if (cateid > 0)
                condition.AppendFormat(" AND cateid = {0}", cateid);
            if (type > 0)
                condition.AppendFormat(" AND [type] = {0}", type);
            if (keywords != string.Empty)
                condition.AppendFormat(" AND (name LIKE '%{0}%' OR [desc] LIKE '%{0}%')", RegEsc(keywords));
            return condition.ToString();
        }

        public int AddVoteoption(VoteoptionInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createvoteoption", BaseConfigs.GetTablePrefix), EntityVoteoption(info)));
        }

        public int DelVoteoption(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletevoteoption", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id)));
        }

        public int EditVoteoption(VoteoptionInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatevoteoption", BaseConfigs.GetTablePrefix), EntityVoteoption(info)));
        }

        public IDataReader GetVoteoption(int id)
        {
            string commandText = string.Format("SELECT * FROM [{0}voteoptions] WHERE [id] = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetVoteoptionDataPage(int pagecurrent, int pagesize, int topicid, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "voteoptions", "id", pagecurrent, pagesize, "*", "id desc", "topicid = " + topicid.ToString(), out pagecount, out recordcount);
        }


        public int AddVoterecord(VoterecordInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createvoterecord", BaseConfigs.GetTablePrefix), EntityVoterecord(info)));
        }

        public int VoteRecordIpTimeInterval(string ip, int topicid, int minute)
        {
            string commandText = string.Format("select top 1 id from [{0}voterecords] where userip='{1}' and addtime >= '{2}' {3}", BaseConfigs.GetTablePrefix, ip, DateTime.Now.AddMinutes(-minute), topicid > 0 ? ("AND topicid = " + topicid.ToString()) : "");
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        public int DelVoterecord(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletevoterecord", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id)));
        }

        public DataTable GetVoterecordDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "voterecords", "id", pagecurrent, pagesize, "*", "id desc", condition, out pagecount, out recordcount);
        }

        public string GetVoterecordSearchCondition(string startdate, string enddate, string ip, int topicid, string idcard, string phone, string keywords)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            if (ip != "")
                condition.AppendFormat(" AND userip = {0}", ip);
            if (idcard != "")
                condition.AppendFormat(" AND idcard = {0}", ip);
            if (topicid > 0)
                condition.AppendFormat(" AND topicid = {0}", topicid);
            if (phone != "")
                condition.AppendFormat(" AND phone = {0}", ip);
            if (keywords != string.Empty)
                condition.AppendFormat(" AND topicname LIKE '%{0}%'", RegEsc(keywords));
            return condition.ToString();
        }

        public int DelVoterecordWhere(string where)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}voterecords]{1}", BaseConfigs.GetTablePrefix, (where == "" ? "" : " WHERE " + where)));
        }

        public int Vote(int topicid, string items)
        {
            int count = items.Split(',').Length;
            if (count <= 0)
                return 0;

            string commandText = string.Format("UPDATE [{0}voteoptions] SET count = count+1 WHERE id in ({1});", BaseConfigs.GetTablePrefix, items);
            commandText += string.Format("UPDATE [{0}votetopics] SET votecount = votecount + {1} WHERE id = {2}", BaseConfigs.GetTablePrefix, count, topicid);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public DataTable GetVoteByLikeid(string likeid)
        {
            return DbHelper.ExecuteDataset(string.Format("select * from [{0}votetopics] where likeid = '{1}' order by orderid desc", BaseConfigs.GetTablePrefix, likeid)).Tables[0];
        }

        public DataTable GetVoteByIds(string ids)
        {
            if (!Utils.IsNumericList(ids) || ids == "") return new DataTable();
            return DbHelper.ExecuteDataset(string.Format("select * from [{0}votetopics] where id in ({1})  order by orderid desc", BaseConfigs.GetTablePrefix, ids)).Tables[0];
        }

        private DbParameter[] EntityVotecate(VotecateInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@ename",(DbType)SqlDbType.NVarChar,50,info.Ename),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        private DbParameter[] EntityVoteoption(VoteoptionInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
                DbHelper.MakeInParam("@desc",(DbType)SqlDbType.NText,0,info.Desc),
                DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int,4,info.Topicid),
                DbHelper.MakeInParam("@topicname",(DbType)SqlDbType.NVarChar,300,info.Topicname),
                DbHelper.MakeInParam("@img",(DbType)SqlDbType.NVarChar,200,info.Img),
                DbHelper.MakeInParam("@count",(DbType)SqlDbType.Int,4,info.Count),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        private DbParameter[] EntityVoterecord(VoterecordInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int,4,info.Topicid),
                DbHelper.MakeInParam("@topicname",(DbType)SqlDbType.NVarChar,300,info.Topicname),
                DbHelper.MakeInParam("@optionids",(DbType)SqlDbType.NVarChar,300,info.Optionids),
                DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,info.Userid),
                DbHelper.MakeInParam("@username",(DbType)SqlDbType.NVarChar,20,info.Username),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@userip",(DbType)SqlDbType.NChar,15,info.Userip),
                DbHelper.MakeInParam("@realname",(DbType)SqlDbType.NVarChar,20,info.Realname),
                DbHelper.MakeInParam("@idcard",(DbType)SqlDbType.NVarChar,18,info.Idcard),
                DbHelper.MakeInParam("@phone",(DbType)SqlDbType.NVarChar,30,info.Phone)
            };
        }

        private DbParameter[] EntityVotetopic(VotetopicInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,300,info.Name),
                DbHelper.MakeInParam("@desc",(DbType)SqlDbType.NText,0,info.Desc),
                DbHelper.MakeInParam("@cateid",(DbType)SqlDbType.Int,4,info.Cateid),
                DbHelper.MakeInParam("@catename",(DbType)SqlDbType.NVarChar,50,info.Catename),
                DbHelper.MakeInParam("@type",(DbType)SqlDbType.TinyInt,1,info.Type),
                DbHelper.MakeInParam("@img",(DbType)SqlDbType.NVarChar,200,info.Img),
                DbHelper.MakeInParam("@likeid",(DbType)SqlDbType.NVarChar,50,info.Likeid),
                DbHelper.MakeInParam("@maxvote",(DbType)SqlDbType.Int,4,info.Maxvote),
                DbHelper.MakeInParam("@endtime",(DbType)SqlDbType.DateTime,8,info.Endtime),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@endtext",(DbType)SqlDbType.NText,0,info.Endtext),
                DbHelper.MakeInParam("@voted",(DbType)SqlDbType.NVarChar,2000,info.Voted),
                DbHelper.MakeInParam("@votecount",(DbType)SqlDbType.Int,4,info.Votecount),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@isinfo",(DbType)SqlDbType.TinyInt,4,info.Isinfo),
                DbHelper.MakeInParam("@islogin",(DbType)SqlDbType.TinyInt,4,info.Islogin),
                DbHelper.MakeInParam("@isvcode",(DbType)SqlDbType.TinyInt,4,info.Isvcode)
            };
        }
    }
}
