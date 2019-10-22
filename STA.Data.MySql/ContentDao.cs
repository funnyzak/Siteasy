using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

using STA.Data;
using STA.Config;
using STA.Common;
using STA.Entity;

namespace STA.Data.MySql
{
    public partial class DataProvider : IDataProvider
    {
        public int AddContent(ContentInfo info)
        {
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    info.Id = TypeParse.StrToInt(DbHelper.ExecuteScalar(trans, CommandType.StoredProcedure, string.Format("{0}createcontent",
                                                                                        BaseConfigs.GetTablePrefix), EntityContent(info)), 0);
                    DataRow[] tdrs = STA.Core.Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + info.Typeid.ToString());
                    if (tdrs.Length <= 0) trans.Rollback();

                    DataRow tdr = tdrs[0];
                    string fields = "cid,typeid" + (tdr["fields"].ToString().Trim() == string.Empty ? string.Empty : ",") + tdr["fields"].ToString().Trim();
                    string commandText = string.Format("INSERT INTO {0}({1})values({2})", tdr["extable"].ToString().Trim(), fields, "@" + fields.Replace(",", ",@"));
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, commandText, EntityContypeField(info));
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            conn.Close();
            return info.Id;
        }

        public int EditContent(ContentInfo info)
        {
            int cid = 0;
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    cid = DbHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, string.Format("{0}updatecontent", BaseConfigs.GetTablePrefix), EntityContent(info));
                    ContypeInfo cinfo = Contents.GetContype(info.Typeid);
                    if (cinfo.Fields.Trim() != string.Empty)
                    {
                        string commandText = "UPDATE " + cinfo.Extable.Trim() + " SET ";
                        foreach (string s in cinfo.Fields.Trim().Split(','))
                        {
                            commandText += s + "=@" + s + ",";
                        }
                        commandText = commandText.EndsWith(",") ? commandText.Substring(0, commandText.Length - 1) : commandText;
                        commandText += " WHERE cid = " + info.Id.ToString();
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, commandText, EntityContypeField(info));
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            conn.Close();
            return cid;
        }

        public IDataReader GetContentForHtml(int id)
        {
            string commandText = string.Format("SELECT savepath,filename,viewgroup,content FROM `{0}contents` WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable GetContentTableByWhere(int count, string fields, string where)
        {
            if (fields == "")
                fields = "*";
            if (count <= 0)
                count = 10;
            if (where != "")
                where = "AND " + where;
            return DbHelper.ExecuteDataset(string.Format("SELECT TOP {0} {1} FROM `{2}contents` WHERE orderid >= -1000 {3}", count, fields, BaseConfigs.GetTablePrefix, where)).Tables[0];
        }

        public int UpdateContentClick(int id, bool backclick)
        {
            string commandText = string.Format("UPDATE `{0}contents` SET `{0}contents`.click = `{0}contents`.click + 1 WHERE `{0}contents`.id = {1};", BaseConfigs.GetTablePrefix, id);
            string commandText2 = string.Format("SELECT click FROM `{0}contents` WHERE `{0}contents`.id = {1}", BaseConfigs.GetTablePrefix, id);
            if (backclick)
                return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText + commandText2));
            else
                return DbHelper.ExecuteNonQuery(commandText);
        }

        public int GetContentClick(int id)
        {
            string commandText = string.Format("SELECT click FROM `{0}contents` WHERE `{0}contents`.id = {1}", BaseConfigs.GetTablePrefix, id);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        public DataTable GetRelateConList(int id, int count, string fields)
        {
            DbParameter[] parms = 
            { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),
                DbHelper.MakeInParam("@count",(DbType)SqlDbType.Int,4,count),
                DbHelper.MakeInParam("@fields",(DbType)SqlDbType.NVarChar,300,fields)
            };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getrelateconlist", parms).Tables[0];
        }

        public IDataReader GetPageForHtml(int id)
        {
            string commandText = string.Format("SELECT savepath,filename,content FROM `{0}pages` WHERE `{0}pages`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetChannelForHtml(int id)
        {
            string commandText = string.Format("SELECT savepath,filename,listrule,viewgroup FROM `{0}channels` WHERE `{0}channels`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public object ContentTypeId(int id)
        {
            string commandText = string.Format("SELECT typeid FROM `{0}contents` WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteScalar(CommandType.Text, commandText);
        }

        public object ChannelConRule(int id)
        {
            string commandText = string.Format("SELECT conrule FROM `{0}channels` WHERE `{0}channels`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteScalar(CommandType.Text, commandText);
        }

        public int ConHtmlStatus(int id, int status)
        {
            string commandText = string.Format("UPDATE `{0}contents` SET `{0}contents`.`ishtml` = {1} WHERE `{0}contents`.`id` = {2}", BaseConfigs.GetTablePrefix, status, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public object ContentSaveName(int id)
        {
            string commandText = string.Format("SELECT `savepath`+'/'+`filename` FROM `{0}contents` WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteScalar(CommandType.Text, commandText);
        }

        private DbParameter[] EntityContent(ContentInfo info)
        {
            return new DbParameter[] 
            {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.SmallInt, 2, info.Typeid),
                DbHelper.MakeInParam("@typename",(DbType)SqlDbType.NVarChar,20,info.Typename),
                DbHelper.MakeInParam("@channelfamily",(DbType)SqlDbType.NChar,200,info.Channelfamily),
                DbHelper.MakeInParam("@channelid",(DbType)SqlDbType.Int,4,info.Channelid),
                DbHelper.MakeInParam("@channelname",(DbType)SqlDbType.NVarChar,100,info.Channelname),
                DbHelper.MakeInParam("@extchannels",(DbType)SqlDbType.NVarChar,100,info.Extchannels),
                DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,100,info.Title),
                DbHelper.MakeInParam("@subtitle",(DbType)SqlDbType.NVarChar,100,info.Subtitle),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@updatetime",(DbType)SqlDbType.DateTime,8,info.Updatetime),
                DbHelper.MakeInParam("@color",(DbType)SqlDbType.Char,7,info.Color),
                DbHelper.MakeInParam("@property",(DbType)SqlDbType.NChar,50,info.Property),
                DbHelper.MakeInParam("@adduser",(DbType)SqlDbType.Int,4,info.AddUser),
                DbHelper.MakeInParam("@addusername",(DbType)SqlDbType.NChar,20,info.Addusername),
                DbHelper.MakeInParam("@lastedituser",(DbType)SqlDbType.Int,4,info.LastEditUser),
                DbHelper.MakeInParam("@lasteditusername",(DbType)SqlDbType.NChar,20,info.Lasteditusername),
                DbHelper.MakeInParam("@author",(DbType)SqlDbType.NVarChar,20,info.Author),
                DbHelper.MakeInParam("@source",(DbType)SqlDbType.NVarChar,20,info.Source),
                DbHelper.MakeInParam("@img",(DbType)SqlDbType.NChar,300,info.Img),
                DbHelper.MakeInParam("@url",(DbType)SqlDbType.NChar,300,info.Url),
                DbHelper.MakeInParam("@seotitle",(DbType)SqlDbType.NVarChar,100,info.Seotitle),
                DbHelper.MakeInParam("@seokeywords",(DbType)SqlDbType.NVarChar,200,info.Seokeywords),
                DbHelper.MakeInParam("@seodescription",(DbType)SqlDbType.NVarChar,200,info.Seodescription),
                DbHelper.MakeInParam("@savepath",(DbType)SqlDbType.NVarChar,200,info.Savepath),
                DbHelper.MakeInParam("@filename",(DbType)SqlDbType.Char,100,info.Filename),
                DbHelper.MakeInParam("@template",(DbType)SqlDbType.Char,50,info.Template),
                DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
                DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status),
                DbHelper.MakeInParam("@viewgroup",(DbType)SqlDbType.NVarChar,200,info.Viewgroup),
                DbHelper.MakeInParam("@iscomment",(DbType)SqlDbType.TinyInt,1,info.Iscomment),
                DbHelper.MakeInParam("@ishtml",(DbType)SqlDbType.TinyInt,1,info.Ishtml),
                DbHelper.MakeInParam("@click",(DbType)SqlDbType.Int,4,info.Click),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@diggcount",(DbType)SqlDbType.Int,4,info.Diggcount),
                DbHelper.MakeInParam("@stampcount",(DbType)SqlDbType.Int,4,info.Stampcount),
                DbHelper.MakeInParam("@credits",(DbType)SqlDbType.Int,4,info.Credits),
                DbHelper.MakeInParam("@commentcount",(DbType)SqlDbType.Int,4,info.Commentcount),
                DbHelper.MakeInParam("@relates",(DbType)SqlDbType.NVarChar,300,info.Relates)
            };
        }

        private DbParameter[] EntityContypeField(ContentInfo info)
        {
            DbParameter[] parms = new DbParameter[50];
            parms[0] = DbHelper.MakeInParam("@cid", (DbType)SqlDbType.Int, 4, info.Id);
            parms[1] = DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.Int, 4, info.Typeid);
            using (IDataReader reader = GetContypeFieldList(info.Typeid))
            {
                int loop = 2;
                while (reader.Read())
                {
                    parms[loop] = GetContentParmaeter(reader["fieldtype"].ToString().Trim(), reader["fieldname"].ToString().Trim(), TypeParse.StrToInt(reader["length"], 20), info.Ext);
                    loop++;
                }
            }
            return parms;
        }

        private DbParameter GetContentParmaeter(string fieldtype, string fieldname, int length, Hashtable ht)
        {
            DbParameter parm = null;
            switch (fieldtype)
            {
                case "char":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.Char, length, TypeParse.ObjToString(ht[fieldname]));
                    break;
                case "nchar":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.NVarChar, length, TypeParse.ObjToString(ht[fieldname]));
                    break;
                case "varchar":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.VarChar, length, TypeParse.ObjToString(ht[fieldname]));
                    break;
                case "nvarchar":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.NVarChar, length, TypeParse.ObjToString(ht[fieldname]));
                    break;
                case "int":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.Int, 4, TypeParse.StrToInt(ht[fieldname]));
                    break;
                case "ntext":
                case "editor":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.NText, 0, TypeParse.ObjToString(ht[fieldname]));
                    break;
                case "datetime":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.DateTime, 8, TypeParse.StrToDateTime(ht[fieldname]));
                    break;
                case "float":
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.Float, 0, TypeParse.StrToFloat(ht[fieldname], 0));
                    break;
                default:
                    parm = DbHelper.MakeInParam("@" + fieldname, (DbType)SqlDbType.NVarChar, 1000, TypeParse.ObjToString(ht[fieldname]));
                    break;
            }
            return parm;
        }

        public IDataReader GetContent(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM {0}contents WHERE id = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public bool CheckContentRepeat(string name)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT SUM(id) FROM `{0}contents` WHERE title = @title AND orderid >= -1000", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 200, name))) > 1;
        }

        public DataTable RepeatConTitleCheck(int typeid)
        {
            string commandText = string.Format("select * from {0}contents where title in (select title from {0}contents  where typeid = {1} AND orderid >= -1000 group by title having count(title) > 1 ) and typeid ={1} AND orderid >= -1000 order by  title desc", BaseConfigs.GetTablePrefix, typeid.ToString());
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public IDataReader GetExtFieldByCid(string fields, string tbname, string cid)
        {
            string commandText2 = string.Format("SELECT {0} FROM {1} WHERE cid = {2}", fields, tbname, cid);
            return DbHelper.ExecuteReader(CommandType.Text, commandText2);
        }

        public int DeleteContent(int id)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletecontent", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        public string GetExtFieldValue(string ext, int cid, string field)
        {
            string commandText = string.Format("SELECT {0} FROM `{1}contents` WHERE id = {2}", field, BaseConfigs.GetTablePrefix, cid);
            if (ext.Trim() != "")
            {
                commandText = string.Format("SELECT ext_{0} FROM `{1}ext{3}s` WHERE cid = {2}", field, BaseConfigs.GetTablePrefix, cid, ext);
            }
            return TypeParse.ObjToString(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        public DataTable GetContentsByChannelId(int id)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT * FROM `{0}contents` WHERE `channelid` = {1} OR `channelfamily` LIKE '%,{1},%'", BaseConfigs.GetTablePrefix, id.ToString())).Tables[0];
        }

        public int EditContentsWhereChannelDel(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}contents` SET `channelid` = 0,`channelname` = '' WHERE `channelid` = {1} OR `channelfamily` LIKE '%,{1},%'", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int ContentDigg(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}contents` SET `diggcount` = `diggcount`+1 WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int ContentStamp(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}contents` SET `stampcount` = `stampcount`+1 WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id)); ;
        }

        public DataTable GetExtConTableByIds(string extname, string fields, string ids)
        {
            if (ids.Trim() == "") return new DataTable();

            string commandText = string.Format("SELECT {0} FROM `{1}contents` INNER JOIN `{1}ext{2}s` ON `{1}contents`.`id` = `{1}ext{2}s`.`cid` WHERE `{1}contents`.`id` IN ({3})", (fields.Trim() == "" ? "id,title,img,ext_price,ext_vipprice,ext_unit,ext_weight,ext_storage" : fields), BaseConfigs.GetTablePrefix, extname, ids);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public string GetDiggStamp(int id)
        {
            return TypeParse.ObjToString(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT cast(`diggcount` as nvarchar(20))+','+cast(`stampcount` as nvarchar(20)) from [sta_contents] WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id))); ;
        }

        public int ContentCountByChannelId(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}contents` WHERE (`channelid` = {1} OR `channelfamily` LIKE '%,{1},%'  or extchannels like '%,{1},%') AND `orderid` >= -1000 AND status = 2", BaseConfigs.GetTablePrefix, id.ToString())));
        }

        public DataTable GetContentIds(int uid)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT id,typeid FROM `{0}contents` WHERE `{0}contents`.`adduser` = {1}", BaseConfigs.GetTablePrefix, uid)).Tables[0];
        }

        public int ConCommentCount(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}comments` WHERE cid = {1} AND status = 1", BaseConfigs.GetTablePrefix, id.ToString())));
        }

        private DbParameter[] EntityChannel(ChannelInfo info)
        {
            return new DbParameter[] 
            {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.SmallInt, 2, info.Typeid),
                DbHelper.MakeInParam("@parentid",(DbType)SqlDbType.Int,4,info.Parentid),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,100,info.Name),
                DbHelper.MakeInParam("@savepath",(DbType)SqlDbType.NVarChar,100,info.Savepath),
                DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NVarChar,50,info.Filename),
                DbHelper.MakeInParam("@ctype",(DbType)SqlDbType.TinyInt,1,info.Ctype),
                DbHelper.MakeInParam("@img",(DbType)SqlDbType.NVarChar,100,info.Img),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@covertem",(DbType)SqlDbType.Char,50,info.Covertem),
                DbHelper.MakeInParam("@listem",(DbType)SqlDbType.Char,50,info.Listem),
                DbHelper.MakeInParam("@contem",(DbType)SqlDbType.Char,50,info.Contem),
                DbHelper.MakeInParam("@conrule",(DbType)SqlDbType.NVarChar,100,info.Conrule),
                DbHelper.MakeInParam("@listrule",(DbType)SqlDbType.NVarChar,100,info.Listrule),
                DbHelper.MakeInParam("@seotitle",(DbType)SqlDbType.NVarChar,100,info.Seotitle),
                DbHelper.MakeInParam("@seokeywords",(DbType)SqlDbType.NVarChar,200,info.Seokeywords),
                DbHelper.MakeInParam("@seodescription",(DbType)SqlDbType.NVarChar,200,info.Seodescription),
                DbHelper.MakeInParam("@moresite",(DbType)SqlDbType.TinyInt,1,info.Moresite),
                DbHelper.MakeInParam("@siteurl",(DbType)SqlDbType.NChar,100,info.Siteurl),
                DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
                DbHelper.MakeInParam("@ispost",(DbType)SqlDbType.TinyInt,1,info.Ispost),
                DbHelper.MakeInParam("@ishidden",(DbType)SqlDbType.TinyInt,1,info.Ishidden),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@listcount",(DbType)SqlDbType.Int,4,info.Listcount),
                DbHelper.MakeInParam("@viewgroup",(DbType)SqlDbType.NVarChar,300,info.Viewgroup),
                DbHelper.MakeInParam("@viewcongroup",(DbType)SqlDbType.NVarChar,300,info.Viewcongroup),
                DbHelper.MakeInParam("@ipdenyaccess",(DbType)SqlDbType.NVarChar,500,info.Ipdenyaccess),
                DbHelper.MakeInParam("@ipaccess",(DbType)SqlDbType.NVarChar,500,info.Ipaccess)
            };
        }


        public IDataReader GetAllChannel()
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}channels`", BaseConfigs.GetTablePrefix));
        }

        public int ChannelParentId(int id)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT parentid  FROM `{0}channels` WHERE id={1}", BaseConfigs.GetTablePrefix, id.ToString())));
        }

        public DataTable GetChannelDataTable(string fields)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT {1} FROM `{0}channels` ORDER BY ORDERID DESC,ID DESC", BaseConfigs.GetTablePrefix, fields.Trim() == "" ? "*" : fields)).Tables[0];
        }

        public DataTable GetContypeDataTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT * FROM `{0}contypes` ORDER BY ORDERID DESC,ID DESC", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public IDataReader GetChannel(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}channels` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }



        public int AddChannel(ChannelInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createchannel", BaseConfigs.GetTablePrefix), EntityChannel(info)), 0);
        }

        public int DeleteChannel(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("UPDATE `{0}contents` SET `{0}contents`.`channelid` = 0 WHERE `{0}contents`.`channelid` = @id;"
                + "DELETE FROM `{0}channels` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public int EditChannel(ChannelInfo info)
        {
            try
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatechannel", BaseConfigs.GetTablePrefix), EntityChannel(info));
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "编辑频道发生错误！", ex);
                return 0;
            }
        }

        public int ExistContypeField(int id, string extable, string ename)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id ),
                DbHelper.MakeInParam("@ename",(DbType)SqlDbType.NChar,30,ename),
                DbHelper.MakeInParam("@extable",(DbType)SqlDbType.Char,30,extable)
			};
            string commandText = string.Format("SELECT COUNT(*) FROM `{0}contypes` WHERE (ename = @ename OR extable = @extable) AND id <> @id", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), 0);
        }

        private DbParameter[] EntityContype(ContypeInfo info)
        {
            return new DbParameter[]
            {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@open",(DbType)SqlDbType.TinyInt,1,info.Open),
                DbHelper.MakeInParam("@system",(DbType)SqlDbType.TinyInt,1,info.System),
                DbHelper.MakeInParam("@ename",(DbType)SqlDbType.NChar,30,info.Ename),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,30,info.Name),
                DbHelper.MakeInParam("@maintable",(DbType)SqlDbType.Char,30,info.Maintable),
                DbHelper.MakeInParam("@extable",(DbType)SqlDbType.Char,30,info.Extable),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@fields",(DbType)SqlDbType.NVarChar,1000,info.Fields),
                DbHelper.MakeInParam("@bgaddmod",(DbType)SqlDbType.NVarChar,50,info.Bgaddmod),
                DbHelper.MakeInParam("@bgeditmod",(DbType)SqlDbType.NVarChar,50,info.Bgeditmod),
                DbHelper.MakeInParam("@bglistmod",(DbType)SqlDbType.NVarChar,50,info.Bglistmod),
                DbHelper.MakeInParam("@addmod",(DbType)SqlDbType.NVarChar,50,info.Addmod),
                DbHelper.MakeInParam("@editmod",(DbType)SqlDbType.NVarChar,50,info.Editmod),
                DbHelper.MakeInParam("@listmod",(DbType)SqlDbType.NVarChar,50,info.Listmod),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        public int AddContype(ContypeInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createcontype", BaseConfigs.GetTablePrefix), EntityContype(info)), 0);
        }

        public int DeleteContype(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("DELETE FROM `{0}contypefields` WHERE `cid` = @id;DELETE FROM `{0}contypes` WHERE `id` = @id;", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public int EditContype(ContypeInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatecontype", BaseConfigs.GetTablePrefix), EntityContype(info));
        }

        public IDataReader GetContype(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}contypes` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        private DbParameter[] EntityContypeField(ContypefieldInfo info)
        {
            return new DbParameter[] 
            {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int,4,info.Cid),
                DbHelper.MakeInParam("@fieldname",(DbType)SqlDbType.Char,20,info.Fieldname),
                DbHelper.MakeInParam("@fieldtype",(DbType)SqlDbType.Char,20,info.Fieldtype),
                DbHelper.MakeInParam("@length",(DbType)SqlDbType.Int,4,info.Length),
                DbHelper.MakeInParam("@isnull",(DbType)SqlDbType.TinyInt,1,info.Isnull),
                DbHelper.MakeInParam("@defvalue",(DbType)SqlDbType.NVarChar,200,info.Defvalue),
                DbHelper.MakeInParam("@tiptext",(DbType)SqlDbType.NVarChar,200,info.Tiptext),
                DbHelper.MakeInParam("@desctext",(DbType)SqlDbType.NVarChar,30,info.Desctext),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@vinnertext",(DbType)SqlDbType.NVarChar,2000,info.Vinnertext)
            };
        }

        public int AddContypeField(ContypefieldInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createcontypefield", BaseConfigs.GetTablePrefix), EntityContypeField(info)), 0);
        }

        public int EditContypeField(ContypefieldInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatecontypefield", BaseConfigs.GetTablePrefix), EntityContypeField(info));
        }

        public int DeleteContypeField(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("DELETE FROM `{0}contypefields` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public int DeleteContypeFieldByCid(int cid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@cid", (DbType)SqlDbType.Int, 4,cid )
			};
            string commandText = string.Format("DELETE FROM `{0}contypefields` WHERE `cid` = @cid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public IDataReader GetContypeField(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}contypefields` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public IDataReader GetContypeFieldList(int cid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@cid", (DbType)SqlDbType.Int, 4,cid )
			};
            string commandText = string.Format("SELECT * FROM `{0}contypefields` WHERE `cid` = @cid ORDER BY orderid DESC", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public int AddTag(string tagName, int conId)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 20, tagName),
                DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int, 4, conId)
			};
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}createtag", BaseConfigs.GetTablePrefix), parms);
        }

        public DataTable GetHotTags(int count)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT TOP {0} id,name,count,addtime FROM `{1}tags` ORDER BY count DESC", count, BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetTagDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "tags", "id", pagecurrent, pagesize, "*", "id desc", string.Empty, out pagecount, out recordcount);
        }

        public int DelTag(string tagName)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletetag", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 20, tagName));
        }

        public int DelTag(int tid)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletetagbytid", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@tid", (DbType)SqlDbType.Int, 4, tid));
        }

        public int DelTagByCid(string tagName, int conId)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 20, tagName),
                DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int, 4, conId)
			};
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletetagbycid", BaseConfigs.GetTablePrefix), parms);
        }

        public int DelTagsByCid(int conId)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletetagsbycid", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@cid", (DbType)SqlDbType.Int, 4, conId));
        }

        public DataTable GetTagsByCid(int conId)
        {
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}getagsbycid", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@cid", (DbType)SqlDbType.Int, 4, conId)).Tables[0];
        }

        public int EditTag(int id, string tagname)
        {
            int tempid = ExistTag(tagname);
            if (tempid > 0 && tempid != id) return 0;
            DbParameter[] parms = new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,20,tagname)
            };
            string commandText = string.Format("UPDATE `{0}tags` SET `{0}tags`.`name` = @name WHERE `{0}tags`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public int AddTag(string tagname)
        {
            if (ExistTag(tagname) > 0) return 0;
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("INSERT INTO `{0}tags` (`name`) VALUES (@name)", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 20, tagname));
        }

        private int ExistTag(string name)
        {
            string existSql = string.Format("SELECT id FROM `{0}tags` WHERE `{0}tags`.`name` = @name", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, existSql, DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 20, name)));
        }

        public int GetMaxTagId()
        {
            string commandText = string.Format("SELECT ISNULL(MAX(id), 0) FROM `{0}tags`", BaseConfigs.GetTablePrefix);
            return TypeParse.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        private DbParameter[] EntityPage(PageInfo info)
        {
            return new DbParameter[] 
            {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,50,info.Name),
                DbHelper.MakeInParam("@alikeid",(DbType)SqlDbType.NVarChar,20,info.Alikeid),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@seotitle",(DbType)SqlDbType.NVarChar,200,info.Seotitle),
                DbHelper.MakeInParam("@seodescription",(DbType)SqlDbType.NVarChar,200,info.Seodescription),
                DbHelper.MakeInParam("@seokeywords",(DbType)SqlDbType.NVarChar,200,info.Seokeywords),
                DbHelper.MakeInParam("@ishtml",(DbType)SqlDbType.TinyInt,1,info.Ishtml),
                DbHelper.MakeInParam("@savepath",(DbType)SqlDbType.NVarChar,100,info.Savepath),
                DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NVarChar,50,info.Filename),
                DbHelper.MakeInParam("@template",(DbType)SqlDbType.Char,50,info.Template),
                DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        public int AddPage(PageInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createpage", BaseConfigs.GetTablePrefix), EntityPage(info)), 0);
        }

        public bool EditPage(PageInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatepage", BaseConfigs.GetTablePrefix), EntityPage(info)) > 0;
        }

        public bool DeletePage(int id)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id)
			};
            string commandText = string.Format("DELETE FROM `{0}pages` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        public IDataReader GetAlikePage(string alikeid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@alikeid", (DbType)SqlDbType.NVarChar, 20, alikeid )
			};
            string commandText = string.Format("SELECT * FROM `{0}pages` WHERE `alikeid` = @alikeid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public IDataReader GetPage(int id)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id )
			};
            string commandText = string.Format("SELECT * FROM `{0}pages` WHERE `id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public object PageSaveName(int id)
        {
            string commandText = string.Format("SELECT `savepath`+'/'+`filename` FROM `{0}pages` WHERE `{0}pages`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString());
            return DbHelper.ExecuteScalar(CommandType.Text, commandText);
        }

        public DataTable GetPageLikeIdList()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT DISTINCT `alikeid` FROM `{0}pages`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="tbname">表名</param>
        /// <param name="fieldkey">用于定位记录的主键(惟一键)字段,可以是逗号分隔的多个字段</param>
        /// <param name="pagecurrent">要显示的页码</param>
        /// <param name="pagesize">每页的大小(记录数)</param>
        /// <param name="fieldshow">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
        /// <param name="fieldorder">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC用于指定排序顺序</param>
        /// <param name="where">查询条件</param>
        /// <param name="pagecount">总页数</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns></returns>
        public DataTable GetDataPage(string tbname, string fieldkey, int pagecurrent, int pagesize, string fieldshow, string fieldorder, string where, out int pagecount, out int recordcount)
        {
            DbParameter parmPageCount = DbHelper.MakeOutParam("@pagecount", (DbType)SqlDbType.Int, 4);
            DbParameter parmRecordCount = DbHelper.MakeOutParam("@recordcount", (DbType)SqlDbType.Int, 4);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@tbname",(DbType)SqlDbType.NVarChar,128,tbname),
                DbHelper.MakeInParam("@fieldkey",(DbType)SqlDbType.NVarChar,50,fieldkey),
                DbHelper.MakeInParam("@pagecurrent",(DbType)SqlDbType.Int,4,pagecurrent),
                DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,pagesize),
                DbHelper.MakeInParam("@fieldshow",(DbType)SqlDbType.NVarChar,1000,fieldshow),
                DbHelper.MakeInParam("@fieldorder",(DbType)SqlDbType.NVarChar,1000,fieldorder),
                DbHelper.MakeInParam("@where",(DbType)SqlDbType.NVarChar,1000,where),
                parmPageCount,parmRecordCount
			};
            DataTable dt = DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}pageview", BaseConfigs.GetTablePrefix), parms).Tables[0];
            pagecount = TypeParse.StrToInt(parmPageCount.Value, 0);
            recordcount = TypeParse.StrToInt(parmRecordCount.Value, 0);
            return dt;
        }

        public DataTable GetPageDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "pages", "id", pagecurrent, pagesize, "*", "id desc", string.Empty, out pagecount, out recordcount);
        }


        public DataTable GetContentDataPage(string fields, int pagecurrent, int pagesize, string where, string orderby, out int pagecount, out int recordcount)
        {
            return GetContentDataPage(fields, "", pagecurrent, pagesize, where, orderby, out pagecount, out recordcount);
        }

        public DataTable GetContentDataPage(string fields, string extname, int pagecurrent, int pagesize, string where, string orderby, out int pagecount, out int recordcount)
        {
            string table = string.Format("`{0}contents`", BaseConfigs.GetTablePrefix);
            if (extname.Trim() != "")
                table = string.Format("`{0}contents` inner join `{0}ext{1}s` on `{0}ext{1}s`.`cid` = `{0}contents`.`id`", BaseConfigs.GetTablePrefix, extname);

            return GetDataPage(table, BaseConfigs.GetTablePrefix + "contents.id", pagecurrent, pagesize, fields == "" ? "*" : (string.Format("`{0}contents`.`typeid`,", BaseConfigs.GetTablePrefix) + fields), orderby == "" ? "id desc" : orderby, where, out pagecount, out recordcount);
        }

        public string GetContentSearchCondition(int typeid, string users, int reclyle, int channelid, int status, string property, string startdate, string enddate, string keyword)
        {
            return GetContentSearchCondition(typeid, users, reclyle, channelid, 0, status, property, startdate, enddate, keyword);
        }

        public string GetContentSearchCondition(int typeid, string users, int reclyle, int channelid, int self, int status, string property, string startdate, string enddate, string keyword)
        {
            StringBuilder condition = new StringBuilder(string.Format(" orderid {0} -1000 ", reclyle == 1 ? "<" : ">="));
            if (typeid >= 0)
                condition.AppendFormat(" AND `{0}contents`.typeid = {1}", BaseConfigs.GetTablePrefix, typeid.ToString());
            users = RegSqlCharList(users);
            if (users != string.Empty)
                condition.AppendFormat(" AND addusername IN ({0})", users);
            if (channelid > 0)
            {
                if (self >= 1)
                    condition.AppendFormat(" AND channelid = {0} ", channelid.ToString());
                else
                    condition.AppendFormat(" AND (channelid = {0} or channelfamily like '%,{0},%' or extchannels like '%,{0},%')", channelid.ToString());
            }
            if (status >= 0)
                condition.AppendFormat(" AND status = {0}", status.ToString());
            if (property != string.Empty)
                condition.AppendFormat(" AND property like '%{0}%'", property);
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            if (keyword != string.Empty)
                condition.AppendFormat(" AND title like '%{0}%'", RegEsc(keyword));
            return condition.ToString();
        }

        public bool EmptyRecycle()
        {
            foreach (DataRow dr in DbHelper.ExecuteDataset(string.Format("SELECT id FROM `{0}contents` WHERE orderid < -1000", BaseConfigs.GetTablePrefix)).Tables[0].Rows)
            {
                DatabaseProvider.GetInstance().DeleteContent(TypeParse.StrToInt(dr["id"]));
            }
            return true;
        }

        public int VerifyContent(int id, int status)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}contents` SET `{0}contents`.`status` = {1} WHERE `{0}contents`.`id` = {2}", BaseConfigs.GetTablePrefix, status.ToString(), id.ToString()));
        }

        public int PutContentRecycle(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}contents` SET `{0}contents`.`orderid` = -1001,`{0}contents`.`ishtml`=0 WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int RecoverContent(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}contents` SET `{0}contents`.`orderid` = 0 WHERE `{0}contents`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        private DbParameter[] EntityConGroup(CongroupInfo info)
        {
            return new DbParameter[] 
            {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@type",(DbType)SqlDbType.TinyInt,1,info.Type),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,20,info.Name),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@desctext",(DbType)SqlDbType.NVarChar,200,info.Desctext)
            };
        }

        public int AddConGroup(CongroupInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}congroups` (`type`, `name`, `addtime`, `desctext`) VALUES (@type, @name, @addtime, @desctext);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityConGroup(info)), 0);
        }

        public int DelConGroup(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}congroupcons` WHERE gid = {1};DELETE FROM `{0}congroups` WHERE id = {1};",
                                            BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int EditConGroup(CongroupInfo info)
        {
            string commandText = string.Format("UPDATE `{0}congroups` SET `name` = @name, `desctext` = @desctext WHERE `{0}congroups`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityConGroup(info));
        }

        public int DelGroupCon(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}congroupcons` WHERE `{0}congroupcons`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int DelGroupCon(int cid, int gid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}congroupcons` WHERE `{0}congroupcons`.`cid` = {1} and `{0}congroupcons`.`gid` = {2}", BaseConfigs.GetTablePrefix, cid, gid));
        }

        public int AddGroupCon(int gid, int cid, int orderid)
        {
            if (TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT * FROM `{0}congroupcons` WHERE `{0}congroupcons`.`gid`={1} AND `{0}congroupcons`.`cid` = {2}", BaseConfigs.GetTablePrefix, gid, cid)), 0) > 0)
                return 0;
            return DbHelper.ExecuteNonQuery(string.Format("INSERT INTO `{0}congroupcons` (`gid`, `cid`, `orderid`) VALUES ({1} , {2} , {3})", BaseConfigs.GetTablePrefix, gid, cid, orderid));
        }

        public int EditGroupCon(int id, int orderid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}congroupcons` SET `orderid` = {1} WHERE `id` = {2}", BaseConfigs.GetTablePrefix, orderid, id));
        }

        public IDataReader GetConGroup(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}congroups` WHERE id = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int UpdateSoftDownloadCount(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}extsofts` SET [ext_downcount] = [ext_downcount]+1 WHERE `cid` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int GetSoftDownloadCount(int id)
        {
            string commandText = string.Format("SELECT [ext_downcount] FROM `{0}extsofts` WHERE `{0}extsofts`.cid = {1}", BaseConfigs.GetTablePrefix, id);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        public string GetConGroupSearchCondition(int type, string keywords)
        {
            StringBuilder condition = new StringBuilder(string.Format("`{0}congroups`.`id` > 0", BaseConfigs.GetTablePrefix));
            if (type >= 0)
                condition.AppendFormat(" AND `{0}congroups`.`type` = {1}", BaseConfigs.GetTablePrefix, type);
            if (keywords != string.Empty)
                condition.AppendFormat(" AND `{0}congroups`.`name` LIKE '%{1}%'", BaseConfigs.GetTablePrefix, RegEsc(keywords));
            return condition.ToString();
        }

        public DataTable GetConGroupDataPage(int pageIndex, int pageSize, string where, out int pageCount, out int recordCount)
        {
            return GetDataPage(BaseConfigs.GetTablePrefix + "congroups", "id", pageIndex, pageSize, "*", "id desc", where, out pageCount, out recordCount);
        }

        public DataTable GetConGroupContentDataPage(int groupid, string columns, int pageIndex, int pageSize, out int pageCount, out int recordCount)
        {
            return GetConGroupContentDataPage(groupid, columns, pageIndex, pageSize, "addtime desc", "", out pageCount, out recordCount);
        }

        public DataTable GetConGroupContentDataPage(int groupid, string columns, int pageIndex, int pageSize,
                                                    string orderby, string where, out int pageCount, out int recordCount)
        {
            columns = string.Format("`{0}contents`." + columns, BaseConfigs.GetTablePrefix).Replace(",", string.Format(",`{0}contents`.", BaseConfigs.GetTablePrefix));
            columns += string.Format(",`{0}congroupcons`.`id` as cgid,`{0}congroupcons`.`orderid` as cgorderid", BaseConfigs.GetTablePrefix);
            return GetDataPage(string.Format("`{0}congroupcons` INNER JOIN `{0}contents` ON `{0}contents`.`id` = `{0}congroupcons`.`cid`",
                               BaseConfigs.GetTablePrefix), "id", pageIndex, pageSize, columns, string.Format("`{0}congroupcons`.`orderid` DESC,", BaseConfigs.GetTablePrefix)
                               + orderby.Trim(), string.Format("`{0}congroupcons`.`gid` = {1} AND" + " `{0}contents`.`orderid` >= -1000" + (where == "" ? "" : " AND" + where),
                               BaseConfigs.GetTablePrefix, groupid), out pageCount, out recordCount);
        }

        public DataTable GetChlGroupChannelDataPage(int groupid, string columns, string orderby, int pageIndex, int pageSize, out int pageCount, out int recordCount)
        {
            columns = string.Format("`{0}channels`." + columns, BaseConfigs.GetTablePrefix).Replace(",", string.Format(",`{0}channels`.", BaseConfigs.GetTablePrefix));
            return GetDataPage(string.Format("`{0}congroupcons` INNER JOIN `{0}channels` ON `{0}channels`.`id` = `{0}congroupcons`.`cid`", BaseConfigs.GetTablePrefix), "id",
                pageIndex, pageSize, columns, orderby.Trim(), string.Format("`{0}congroupcons`.`gid` = {1}", BaseConfigs.GetTablePrefix, groupid), out pageCount, out recordCount);
        }


        public DataTable GetTagContentDataPage(int tagid, string columns, int pageIndex, int pageSize,
                                            string orderby, string where, out int pageCount, out int recordCount)
        {
            columns = string.Format("`{0}contents`." + columns, BaseConfigs.GetTablePrefix).Replace(",", string.Format(",`{0}contents`.", BaseConfigs.GetTablePrefix));
            return GetDataPage(string.Format("`{0}contags` INNER JOIN `{0}contents` ON `{0}contents`.`id` = `{0}contags`.`contentid`",
                               BaseConfigs.GetTablePrefix), "id", pageIndex, pageSize, columns,
                                orderby.Trim(), string.Format("`{0}contags`.`tagid` = {1} "
                               + (where == "" ? "" : " AND" + where), BaseConfigs.GetTablePrefix, tagid), out pageCount, out recordCount);
        }

        public DataTable GetChannelGroupIds(int gid)
        {
            string commandText = string.Format("SELECT * FROM `{0}congroupcons`  WHERE `{0}congroupcons`.`gid`={1} ", BaseConfigs.GetTablePrefix, gid);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public int UpdateMagazineClick(int id, bool backclick)
        {
            string commandText = string.Format("UPDATE `{0}magazines` SET `{0}magazines`.click = `{0}magazines`.click + 1 WHERE `{0}magazines`.id = {1};", BaseConfigs.GetTablePrefix, id);
            string commandText2 = string.Format("SELECT click FROM `{0}magazines` WHERE `{0}magazines`.id = {1}", BaseConfigs.GetTablePrefix, id);
            if (backclick)
                return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText + commandText2));
            else
                return DbHelper.ExecuteNonQuery(commandText);
        }

        public int GetMagazineClick(int id)
        {
            string commandText = string.Format("SELECT click FROM `{0}magazines` WHERE `{0}magazines`.id = {1}", BaseConfigs.GetTablePrefix, id);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }


        public string GetMagazineSearchCondition(string startdate, string enddate, string likeid, int status, string keywords)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            if (keywords != string.Empty)
                condition.AppendFormat(" AND name LIKE '%{0}%'", RegEsc(keywords));
            if (status >= 0)
                condition.AppendFormat(" AND status = {0}", status.ToString());
            if (likeid != string.Empty)
                condition.AppendFormat(" AND likeid LIKE '%{0}%'", RegEsc(likeid));
            return condition.ToString();
        }

        private DbParameter[] EntityMagazine(MagazineInfo info)
        {
            return new DbParameter[] { 
				DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
				DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,info.Name),
				DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
				DbHelper.MakeInParam("@updatetime",(DbType)SqlDbType.DateTime,8,info.Updatetime),
				DbHelper.MakeInParam("@likeid",(DbType)SqlDbType.NVarChar,20,info.Likeid),
				DbHelper.MakeInParam("@ratio",(DbType)SqlDbType.NVarChar,10,info.Ratio),
				DbHelper.MakeInParam("@cover",(DbType)SqlDbType.NVarChar,100,info.Cover),
				DbHelper.MakeInParam("@description",(DbType)SqlDbType.NText,0,info.Description),
				DbHelper.MakeInParam("@content",(DbType)SqlDbType.NText,0,info.Content),
				DbHelper.MakeInParam("@pages",(DbType)SqlDbType.Int,4,info.Pages),
				DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
				DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status),
				DbHelper.MakeInParam("@click",(DbType)SqlDbType.Int,4,info.Click),
				DbHelper.MakeInParam("@parms",(DbType)SqlDbType.NVarChar,300,info.Parms)
            };
        }

        public int AddMagazine(MagazineInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}magazines` (`name`, `addtime`, `updatetime`, `likeid`, `ratio`, `cover`, `description`, `content`, `pages`, `orderid`, `status`, `click`, `parms`) VALUES (@name, @addtime, @updatetime, @likeid, @ratio, @cover, @description, @content, @pages, @orderid, @status, @click, @parms);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityMagazine(info)));
        }

        public int EditMagazine(MagazineInfo info)
        {
            string commandText = string.Format("UPDATE `{0}magazines` SET `name` = @name, `addtime` = @addtime, `updatetime` = @updatetime, `likeid` = @likeid, `ratio` = @ratio, `cover` = @cover, `description` = @description, `content` = @content, `pages` = @pages, `orderid` = @orderid, `status` = @status, `click` = @click, `parms` = @parms WHERE `{0}magazines`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityMagazine(info));
        }

        public int DelMagazine(int id)
        {
            string commandText = string.Format("DELETE FROM `{0}magazines` WHERE `{0}magazines`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public IDataReader GetMagazine(int id)
        {
            string commandText = string.Format("SELECT TOP 1 * FROM `{0}magazines` WHERE `{0}magazines`.`id` = {1}", BaseConfigs.GetTablePrefix, id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public DataTable MagazineLikeIds()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT DISTINCT `likeid` FROM `{0}magazines`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetMagazineDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("{0}magazines", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, "id desc", condition, out pagecount, out recordcount);
        }


        public int AddLink(LinkInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createlink", BaseConfigs.GetTablePrefix), EntityLink(info)));
        }

        public int EditLink(LinkInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatelink", BaseConfigs.GetTablePrefix), EntityLink(info));
        }

        public int DelLink(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}links` WHERE `{0}links`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetLink(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}links` WHERE `{0}links`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetLinkDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}links`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, "*", "id desc", condition, out pagecount, out recordcount);
        }

        public int AddLinkType(LinktypeInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}linktypes` (name, orderid) VALUES (@name, @orderid);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityLinkType(info)));
        }

        public int DelLinkType(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}links` SET `{0}links`.`typeid` = 0;DELETE FROM `{0}linktypes` WHERE `{0}linktypes`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int EditLinkType(LinktypeInfo info)
        {
            string commandText = string.Format("UPDATE `{0}linktypes` SET `{0}linktypes`.`name` = @name,`{0}linktypes`.`orderid` = @orderid WHERE `{0}linktypes`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityLinkType(info));
        }

        public IDataReader GetLinkType(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}linktypes` WHERE `{0}linktypes`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetLinkType()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}linktypes`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetLinkTypePage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}linktypes`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, "*", "id desc", "", out pagecount, out recordcount);
        }

        public string GetLinkSearchCondition(int typeid, int status, string startdate, string enddate, string keywords)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (typeid > 0)
                condition.Append("AND typeid = " + typeid.ToString());
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            if (keywords != string.Empty)
                condition.AppendFormat(" AND (name LIKE '%{0}%' OR description LIKE '%{0}%')", RegEsc(keywords));
            if (status >= 0)
                condition.AppendFormat(" AND status = {0}", status.ToString());
            return condition.ToString();
        }

        public int VerifyLink(int id, int status)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}links` SET `{0}links`.`status` = {1} WHERE `{0}links`.`id` = {2}", BaseConfigs.GetTablePrefix, status.ToString(), id.ToString()));
        }

        private DbParameter[] EntityLinkType(LinktypeInfo info)
        {
            return new DbParameter[]{
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,20,info.Name),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        private DbParameter[] EntityLink(LinkInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@typeid",(DbType)SqlDbType.Int,4,info.Typeid),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,50,info.Name),
                DbHelper.MakeInParam("@url",(DbType)SqlDbType.NChar,100,info.Url),
                DbHelper.MakeInParam("@logo",(DbType)SqlDbType.NChar,100,info.Logo),
                DbHelper.MakeInParam("@email",(DbType)SqlDbType.NChar,100,info.Email),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@description",(DbType)SqlDbType.NVarChar,500,info.Description),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid),
                DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status)
            };
        }

        public int AddArea(AreaInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}areas` (name, parentid, orderid) VALUES (@name, @parentid, @orderid);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntityArea(info)));
        }

        public int EditArea(AreaInfo info)
        {
            string commandText = string.Format("UPDATE `{0}areas` SET `{0}areas`.`name` = @name,`{0}areas`.`parentid` = @parentid,`{0}areas`.`orderid` = @orderid WHERE `{0}areas`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntityArea(info));
        }

        private DbParameter[] EntityArea(AreaInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,50,info.Name),
                DbHelper.MakeInParam("@parentid",(DbType)SqlDbType.Int,4,info.Parentid),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        public int DelArea(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}areas` WHERE `{0}areas`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetAreaDataTable()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}areas`", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        public DataTable GetAreaDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}areas`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, "*", "id desc", string.Empty, out pagecount, out recordcount);
        }

        public int AddAttachment(AttachmentInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createattachment", BaseConfigs.GetTablePrefix), EntityAttachment(info)));
        }

        public int EditAttachment(AttachmentInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateattachment", BaseConfigs.GetTablePrefix), EntityAttachment(info));
        }

        private DbParameter[] EntityAttachment(AttachmentInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
                DbHelper.MakeInParam("@lasteditusername",(DbType)SqlDbType.NChar,20,info.Lasteditusername),
                DbHelper.MakeInParam("@lastedituid",(DbType)SqlDbType.Int,4,info.Lastedituid),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@lastedittime",(DbType)SqlDbType.DateTime,8,info.Lasteditime),
                DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NChar,200,info.Filename),
                DbHelper.MakeInParam("@fileext",(DbType)SqlDbType.NChar,10,info.Fileext),
                DbHelper.MakeInParam("@description",(DbType)SqlDbType.NVarChar,300,info.Description),
                DbHelper.MakeInParam("@filetype",(DbType)SqlDbType.NChar,100,info.Filetype),
                DbHelper.MakeInParam("@filesize",(DbType)SqlDbType.Int,4,info.Filesize),
                DbHelper.MakeInParam("@attachment",(DbType)SqlDbType.NVarChar,100,info.Attachment),
                DbHelper.MakeInParam("@width",(DbType)SqlDbType.Int,4,info.Width),
                DbHelper.MakeInParam("@height",(DbType)SqlDbType.Int,4,info.Height),
                DbHelper.MakeInParam("@downloads",(DbType)SqlDbType.Int,4,info.Downloads),
                DbHelper.MakeInParam("@attachcredits",(DbType)SqlDbType.Int,4,info.Attachcredits)
            };
        }

        public int DelAttachment(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}attachments` WHERE `{0}attachments`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int DelAttachment(string filename)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}attachments` WHERE `{0}attachments`.`filename` like '{1}%'", BaseConfigs.GetTablePrefix, RegEsc(filename)));
        }

        public IDataReader GetAttachment(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}attachments` WHERE `{0}attachments`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetAttachTableByLikeName(string filename)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}attachments` WHERE `{0}attachments`.`filename` = '{1}%'", BaseConfigs.GetTablePrefix, RegEsc(filename))).Tables[0];
        }

        public IDataReader GetAttachment(string filename)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}attachments` WHERE `{0}attachments`.`filename` like '{1}%'", BaseConfigs.GetTablePrefix, RegEsc(filename)));
        }

        public DataTable GetAttachmentDataPage(int pagecurrent, int pagesize, string condtion, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}attachments`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, "*", "id desc", condtion, out pagecount, out recordcount);
        }

        public string AdFilename(int id)
        {
            return TypeParse.ObjToString(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT TOP 1 filename FROM `{0}ads` WHERE `{0}ads`.`id` = {1} ORDER BY ID DESC", BaseConfigs.GetTablePrefix, id.ToString())));
        }

        public string AdFilename(string adname)
        {
            return TypeParse.ObjToString(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT TOP 1 filename FROM `{0}ads` WHERE `{0}ads`.`name` = @name ORDER BY ID DESC", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 100, adname)));
        }

        public int AddAd(AdInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createad", BaseConfigs.GetTablePrefix), EntityAd(info)));
        }

        public int EditAd(AdInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatead", BaseConfigs.GetTablePrefix), EntityAd(info));
        }

        private DbParameter[] EntityAd(AdInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,50,info.Name),
                DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NVarChar,200,info.Filename),
                DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,(byte)info.Status),
                DbHelper.MakeInParam("@adtype",(DbType)SqlDbType.TinyInt,1,(byte)info.Adtype),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@startdate",(DbType)SqlDbType.DateTime,8,info.Startdate),
                DbHelper.MakeInParam("@enddate",(DbType)SqlDbType.DateTime,8,info.Enddate),
                DbHelper.MakeInParam("@click",(DbType)SqlDbType.Int,4,info.Click),
                DbHelper.MakeInParam("@paramarray",(DbType)SqlDbType.NText,0,info.Paramarray),
                DbHelper.MakeInParam("@outdate",(DbType)SqlDbType.NVarChar,500,info.Outdate)
            };
        }

        public int DelAd(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}ads` WHERE `{0}ads`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetAd(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}ads` WHERE `{0}ads`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public IDataReader GetAd(string adname)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP 1 * FROM `{0}ads` WHERE `{0}ads`.`name` = @name ORDER BY ID DESC", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 100, adname));
        }

        public string GetAdSearchCondtion(string startdate, string enddate, int adtype, string keywords)
        {
            StringBuilder condition = new StringBuilder("id > 0");
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            if (keywords != string.Empty)
                condition.AppendFormat(" AND name LIKE '%{0}%'", RegEsc(keywords));
            if (adtype > 0)
                condition.AppendFormat(" AND adtype = {0}", adtype.ToString());
            return condition.ToString();
        }


        public DataTable GetAdDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}ads`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, "*", "id desc", condition, out pagecount, out recordcount);
        }

        public int AddSearchCache(SearchcacheInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createsearchcache", BaseConfigs.GetTablePrefix), EntitySearchCache(info)));
        }


        public int DelSearchCache(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}searchcaches` WHERE `{0}searchcaches`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetSearchCacheDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}searchcaches`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, "*", "id desc", string.Empty, out pagecount, out recordcount);
        }

        public string GetAttachSearchCondition(string startdate, string enddate, string users, string fileext, int minsize, int maxsize, string keywords)
        {
            StringBuilder condition = new StringBuilder("filesize >= " + minsize.ToString());
            if (maxsize != 0)
                condition.AppendFormat(" AND filesize <= {0}", maxsize.ToString());
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            if (keywords != string.Empty)
                condition.AppendFormat(" AND (attachment LIKE '%{0}%' OR description LIKE '%{0}%')", RegEsc(keywords));
            users = RegSqlCharList(users);
            if (users != string.Empty)
                condition.AppendFormat(" AND username IN ({0})", users);
            fileext = RegSqlCharList(fileext);
            if (fileext != string.Empty)
                condition.AppendFormat(" AND fileext IN ({0})", fileext);
            return condition.ToString();
        }

        private DbParameter[] EntitySearchCache(SearchcacheInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar,200,info.Keywords),
                DbHelper.MakeInParam("@searchstring",(DbType)SqlDbType.NVarChar,300,info.Searchstring),
                DbHelper.MakeInParam("@searchtime",(DbType)SqlDbType.DateTime,8,info.Searchtime),
                DbHelper.MakeInParam("@expiration",(DbType)SqlDbType.DateTime,8,info.Expiration),
                DbHelper.MakeInParam("@scount",(DbType)SqlDbType.Int,4,info.Scount),
                DbHelper.MakeInParam("@rcount",(DbType)SqlDbType.Int,4,info.Rcount),
                DbHelper.MakeInParam("@ids",(DbType)SqlDbType.NText,0,info.Ids)
            };
        }

        public int AddComment(CommentInfo info)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createcomment", BaseConfigs.GetTablePrefix), EntityComment(info)));
        }

        public int EditComment(CommentInfo info)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatecomment", BaseConfigs.GetTablePrefix), EntityComment(info));
        }

        public int DelComment(int id)
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}deletecomment", BaseConfigs.GetTablePrefix), DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id));
        }

        public int DelCommentByUid(int uid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}comments` WHERE `{0}comments`.`uid` = {1}", BaseConfigs.GetTablePrefix, uid));
        }

        public int CommentStatus(int id, int status)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}comments` SET `status` = {1} WHERE `{0}comments`.`id` = {2}", BaseConfigs.GetTablePrefix, status, id));
        }

        public int CommentDigg(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}comments` SET `diggcount` = `diggcount`+1 WHERE `{0}comments`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int commentStamp(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}comments` SET `stampcount` = `stampcount`+1 WHERE `{0}comments`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public int DelCommentByCid(int cid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}comments` WHERE `{0}comments`.`cid` = {1}", BaseConfigs.GetTablePrefix, cid));
        }

        public IDataReader GetComment(int id)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}comments` WHERE `{0}comments`.`id` = {1}", BaseConfigs.GetTablePrefix, id));
        }

        public DataTable GetCommentDataPage(string fields, string orderby, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetDataPage(string.Format("`{0}comments`", BaseConfigs.GetTablePrefix), "id", pagecurrent, pagesize, fields == "" ? "*" : fields, orderby == "" ? "id desc" : orderby, condition, out pagecount, out recordcount);
        }

        public string GetCommentSearchCondition(int status, int cid, string users, string ip, string startdate, string enddate, string contitle, string keywords)
        {
            StringBuilder condition = new StringBuilder("id>0");
            if (status > -1)
                condition.AppendFormat(" AND status = {0}", status);
            if (cid > 0)
                condition.AppendFormat(" AND cid = {0}", cid);
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
            users = RegSqlCharList(users);
            if (users != string.Empty)
                condition.AppendFormat(" AND username IN ({0})", users);
            if (ip != "")
                condition.AppendFormat(" AND userip = {0}", ip);
            if (keywords != string.Empty)
                condition.AppendFormat(" AND (title LIKE '%{0}%' OR msg LIKE '%{0}%')", RegEsc(keywords));
            if (contitle != string.Empty)
                condition.AppendFormat(" AND contitle = '{0}'", RegEsc(contitle));
            return condition.ToString();
        }

        private DbParameter[] EntityComment(CommentInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int,4,info.Cid),
                DbHelper.MakeInParam("@contitle",(DbType)SqlDbType.NVarChar,100,info.Contitle),
                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,info.Uid),
                DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar,20,info.Username),
                DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,100,info.Title),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@verifytime",(DbType)SqlDbType.DateTime,8,info.Verifytime),
                DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt,1,info.Status),
                DbHelper.MakeInParam("@userip",(DbType)SqlDbType.Char,15,info.Userip),
                DbHelper.MakeInParam("@diggcount",(DbType)SqlDbType.Int,4,info.Diggcount),
                DbHelper.MakeInParam("@stampcount",(DbType)SqlDbType.Int,4,info.Stampcount),
                DbHelper.MakeInParam("@msg",(DbType)SqlDbType.NText,0,info.Msg),
                DbHelper.MakeInParam("@quote",(DbType)SqlDbType.NText,0,info.Quote),
                DbHelper.MakeInParam("@replay",(DbType)SqlDbType.Int,4,info.Replay),
                DbHelper.MakeInParam("@city",(DbType)SqlDbType.NVarChar,50,info.City)
            };
        }

        private DbParameter[] EntitySpecgroup(SpecgroupInfo info)
        {
            return new DbParameter[] { 
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,info.Id),
                DbHelper.MakeInParam("@specid",(DbType)SqlDbType.Int,4,info.Specid),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar,20,info.Name),
                DbHelper.MakeInParam("@addtime",(DbType)SqlDbType.DateTime,8,info.Addtime),
                DbHelper.MakeInParam("@orderid",(DbType)SqlDbType.Int,4,info.Orderid)
            };
        }

        public DataTable GetContentForSiteMap(int count)
        {
            string commandText = string.Format("SELECT TOP {1} id,typeid,updatetime,savepath,filename FROM `{0}contents` WHERE orderid >= -1000 AND status=2 ORDER BY addtime DESC", BaseConfigs.GetTablePrefix, count);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public DataTable GetPageTableForSiteMap()
        {
            string commandText = string.Format("SELECT id,addtime FROM `{0}pages` ORDER BY ID DESC", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public int AddSpecgroup(SpecgroupInfo info)
        {
            string commandText = string.Format("INSERT INTO `{0}specgroups` (`specid`, `name`, `addtime`, `orderid`) VALUES (@specid, @name, @addtime, @orderid);" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix);
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, EntitySpecgroup(info)));
        }

        public int DelSpecgroup(int id)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}specontents` SET `{0}specontents`.`groupid` = 0 WHERE `{0}specontents`.`groupid` = {1};DELETE FROM `{0}specgroups` WHERE `{0}specgroups`.`id` = {1}", BaseConfigs.GetTablePrefix, id.ToString()));
        }

        public int EditSpecgroup(SpecgroupInfo info)
        {
            string commandText = string.Format("UPDATE `{0}specgroups` SET `{0}specgroups`.`name` = @name, `{0}specgroups`.`orderid` = @orderid WHERE `{0}specgroups`.`id` = @id", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, EntitySpecgroup(info));
        }

        public int AddSpeccontent(SpecontentInfo info)
        {
            string commandText = string.Format("SELECT contentid FROM `{0}specontents` WHERE `{0}specontents`.`specid` = {1} AND `{0}specontents`.`contentid` = {2}", BaseConfigs.GetTablePrefix, info.Specid.ToString(), info.Contentid.ToString());
            if (TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText)) > 0) return 0;
            string commandText2 = string.Format("INSERT INTO `{0}specontents` (`specid`, `groupid`, `contentid`) VALUES ({1}, {2}, {3});" + DbHelper.Provider.GetLastIdSql(), BaseConfigs.GetTablePrefix, info.Specid.ToString(), info.Groupid.ToString(), info.Contentid.ToString());
            return DbHelper.ExecuteNonQuery(commandText2);
        }

        public int EditSpeccontent(SpecontentInfo info)
        {
            string commandText = string.Format("UPDATE `{0}specontents` SET `{0}specontents`.`groupid` = {1} WHERE `{0}specontents`.`specid` = {2} AND `{0}specontents`.`contentid` = {3}", BaseConfigs.GetTablePrefix, info.Groupid.ToString(), info.Specid.ToString(), info.Contentid.ToString());
            return DbHelper.ExecuteNonQuery(commandText);
        }

        public int DelSpecgroupBySpecid(int specid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}specontents` SET `{0}specontents`.`groupid` = 0;DELETE FROM `{0}specgroups` WHERE `{0}spegroups`.`specid` = {1}", BaseConfigs.GetTablePrefix, specid.ToString()));
        }

        public int DelSpeccontent(int specid, int contentid)
        {
            return DbHelper.ExecuteNonQuery(string.Format("DELETE FROM `{0}specontents` WHERE `{0}specontents`.`specid` = {1} AND `{0}specontents`.`contentid` = {2}",
                                                                                                    BaseConfigs.GetTablePrefix, specid.ToString(), contentid.ToString()));
        }

        public DataTable GetSpeccontentDataTable(int pageIndex, int pagesize, int specid, int groupid, string condition, out int pageCount, out int recordCount)
        {
            return GetSpeccontentDataTable(DbFields.SPEC_CONTENT, pageIndex, pagesize, specid, groupid, "addtime desc", condition, out pageCount, out recordCount);
        }

        public DataTable GetSpeccontentDataTable(string fields, int pageIndex, int pagesize, int specid, int groupid, string orderby, string condition, out int pageCount, out int recordCount)
        {
            DbParameter pcparm = DbHelper.MakeOutParam("@pagecount", (DbType)SqlDbType.Int, 4);
            DbParameter rcparm = DbHelper.MakeOutParam("@recordcount", (DbType)SqlDbType.Int, 4);
            DbParameter[] parms = new DbParameter[] {
                DbHelper.MakeInParam("@specid",(DbType)SqlDbType.Int,4,specid),
                DbHelper.MakeInParam("@specgroupid",(DbType)SqlDbType.Int,4,groupid),
                DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,pagesize),
                DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
                DbHelper.MakeInParam("@fields",(DbType)SqlDbType.NVarChar,300,fields),
                DbHelper.MakeInParam("@where",(DbType)SqlDbType.NVarChar,300,condition),
                DbHelper.MakeInParam("@orderby",(DbType)SqlDbType.NVarChar,100,orderby),pcparm,rcparm
            };
            DataTable dt = DbHelper.ExecuteDataset(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "specontentpage", parms).Tables[0];
            pageCount = TypeParse.StrToInt(pcparm.Value, 0);
            recordCount = TypeParse.StrToInt(rcparm.Value, 0);
            return dt;
        }

        public DataTable GetSpecgroups(int specid)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}specgroups` WHERE `{0}specgroups`.`specid` = {1} ORDER BY ORDERID DESC ", BaseConfigs.GetTablePrefix, specid.ToString())).Tables[0];
        }

        public DataTable GetSpeconids(int specid)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT * FROM `{0}specontents` WHERE `{0}specontents`.`specid` = {1}", BaseConfigs.GetTablePrefix, specid.ToString())).Tables[0];
        }

        public int ChannelCount()
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}channels`", BaseConfigs.GetTablePrefix)));
        }

        public int CommentCount()
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}comments`", BaseConfigs.GetTablePrefix)));
        }

        public int SpecialCount()
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}contents` WHERE `{0}contents`.`typeid` = 0  AND `orderid` >= -1000", BaseConfigs.GetTablePrefix)));
        }

        public int ContentCount()
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}contents` WHERE `{0}contents`.`typeid` > 0 AND `orderid` >= -1000", BaseConfigs.GetTablePrefix)));
        }

        public int ContentCount(int typeid)
        {
            return TypeParse.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM `{0}contents` WHERE `{0}contents`.`typeid` = {1}", BaseConfigs.GetTablePrefix, typeid.ToString())));
        }

        public string TemplateContent(int id)
        {
            DbParameter template = DbHelper.MakeOutParam("@template", (DbType)SqlDbType.NVarChar, 50);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),template
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}templatecontent", BaseConfigs.GetTablePrefix), parms);
            return TypeParse.ObjToString(template.Value, "");
        }

        public string TemplateSpecial(int id)
        {
            DbParameter template = DbHelper.MakeOutParam("@template", (DbType)SqlDbType.NVarChar, 50);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),template
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}templatespecial", BaseConfigs.GetTablePrefix), parms);
            return TypeParse.ObjToString(template.Value, "");
        }

        public string TemplateChannel(int id)
        {
            DbParameter template = DbHelper.MakeOutParam("@template", (DbType)SqlDbType.NVarChar, 50);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),template
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}templatechannel", BaseConfigs.GetTablePrefix), parms);
            return TypeParse.ObjToString(template.Value, "");
        }

        public string TemplatePage(int id)
        {
            DbParameter template = DbHelper.MakeOutParam("@template", (DbType)SqlDbType.NVarChar, 50);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),template
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}templatepage", BaseConfigs.GetTablePrefix), parms);
            return TypeParse.ObjToString(template.Value, "");
        }

        public string TemplateSpecGroup(int id)
        {
            DbParameter template = DbHelper.MakeOutParam("@template", (DbType)SqlDbType.NVarChar, 50);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,id),template
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}templatespecgroup", BaseConfigs.GetTablePrefix), parms);
            return TypeParse.ObjToString(template.Value, "");
        }

        public string GetPageSearchCondition(string startdate, string enddate, int minid, int maxid, string keyword)
        {
            StringBuilder condition = new StringBuilder("id>" + minid.ToString());
            if (maxid > 0)
                condition.AppendFormat(" AND id < {0}", maxid.ToString());
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            if (keyword != string.Empty)
                condition.AppendFormat(" AND name like '%{0}%'", RegEsc(keyword));
            return condition.ToString();
        }

        public string GetContentPublishCondition(string startdate, string enddate, int minid, int maxid, int channelid)
        {
            StringBuilder condition = new StringBuilder("id>" + minid.ToString());
            if (maxid > 0)
                condition.AppendFormat(" AND id < {0}", maxid.ToString());
            if (startdate != string.Empty)
                condition.AppendFormat(" AND addtime >= '{0}'", TypeParse.StrToDateTime(startdate));
            if (enddate != string.Empty)
                condition.AppendFormat(" AND addtime <= '{0}'", TypeParse.StrToDateTime(enddate).AddDays(1));
            if (channelid > 0)
                condition.AppendFormat(" AND (channelid = {0} or channelfamily like '%,{0},%' or extchannels like '%,{0},%')", channelid.ToString());
            return condition.ToString();
        }

        public DataTable GetPublishChannelTable(string ids)
        {
            string commandText = string.Format("SELECT `id`,`ctype`,`savepath`,`filename`,`listrule`,`content`,`listcount` FROM `{0}channels` WHERE ctype != 3 AND viewgroup = '' {1}",
                                 BaseConfigs.GetTablePrefix,
                                 (ids.Trim() != "" ? (" AND id IN (" + ids + ")") : ""));
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public DataTable GetPublishRssTable(string ids)
        {
            string commandText = string.Format("SELECT `id` FROM `{0}channels` WHERE ctype != 3 AND viewcongroup = '' {1}",
                                 BaseConfigs.GetTablePrefix,
                                 (ids.Trim() != "" ? (" AND id IN (" + ids + ")") : ""));
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        public DataTable GetPublishPageTable(string condition)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT `id`,`savepath`,`filename`,`content` FROM `{0}pages` {1}",
                                           BaseConfigs.GetTablePrefix,
                                           (condition.Trim() != "" ? (" WHERE " + condition) : ""))).Tables[0];
        }

        public DataTable GetPublishContentTable(string condition)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT `id`,`savepath`,`filename`,`content` FROM `{0}contents` WHERE typeid > 0 AND orderid>=-1000 AND status=2 AND viewgroup = '' AND credits = 0 {1}",
                                   BaseConfigs.GetTablePrefix,
                                   (condition.Trim() != "" ? (" AND " + condition) : ""))).Tables[0];
        }

        public DataTable GetPublishSpecialTable(string condition)
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT `id`,`savepath`,`filename` FROM `{0}contents` WHERE typeid = 0 AND orderid>=-1000 AND viewgroup = '' {1}",
                           BaseConfigs.GetTablePrefix,
                           (condition.Trim() != "" ? (" AND " + condition) : ""))).Tables[0];
        }

        public DataTable GetJumpContentList()
        {
            return DbHelper.ExecuteDataset(string.Format("SELECT `id`,`url` FROM `{0}contents` WHERE `property` like '%,j,%'", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        #region 页面数据调用

        public DataTable GetUILinkData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}links`", tableprefix), orderby = "", where = string.Format(" `{0}links`.`status` = 1 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `orderid` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                if (TypeParse.StrToInt(info.Id) > 0)
                    where += "AND typeid = " + info.Id;

                int pagecount = 100000, recordcount = 100000;
                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "链接调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUILinkTypeData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}linktypes`", tableprefix), orderby = "", where = string.Format(" `id` > 0 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 1: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `orderid` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                int pagecount = 100000, recordcount = 100000;
                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "链接类型调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUITagData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}tags`", tableprefix), orderby = "", where = string.Format(" `{0}tags`.`id` > 0 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `count` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                int pagecount = 100000, recordcount = 100000;
                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "标签调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUIMagazineData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}magazines`", tableprefix), orderby = "", where = string.Format(" `{0}magazines`.`status` = 1 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `orderid` {1}", tableprefix, temporder); break;
                    case 7: orderby = string.Format(" `updatetime` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                if (info.Likeid != "")
                    where += " AND likeid = '" + info.Likeid.Replace("'", "''") + "'";

                int pagecount = 100000, recordcount = 100000;
                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "杂志调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUIVoteData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}votetopics`", tableprefix), orderby = "", where = string.Format(" `{0}votetopics`.`id` > 0 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `orderid` {1}", tableprefix, temporder); break;
                    case 3: orderby = string.Format(" `votecount` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                if (info.Likeid != "")
                    where += " AND likeid = '" + info.Likeid.Replace("'", "''") + "'";
                if (TypeParse.StrToInt(info.Id) > 0)
                    where += " AND cateid = " + info.Id.ToString();

                int pagecount = 100000, recordcount = 100000;
                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "投票调用读取发生错误！", ex);
                return new DataTable();
            }
        }


        public DataTable GetUIPageData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}pages`", tableprefix), orderby = "", where = string.Format(" `{0}pages`.`id` > 0 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `{0}pages`.`addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `{0}pages`.`id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `{0}pages`.`orderid` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `{0}pages`.`id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                if (info.Likeid != "")
                    where += " AND alikeid = '" + info.Likeid.Replace("'", "''") + "'";

                int pagecount = 100000, recordcount = 100000;
                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "单页调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUIChannelData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}channels`", tableprefix), orderby = "", where = string.Format(" `{0}channels`.`ishidden` = 0 ", tableprefix);

                if (info.Id.Trim() == "")
                    info.Id = "0";

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `{0}channels`.`addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `{0}channels`.`id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `{0}channels`.`orderid` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `{0}channels`.`id` {1}", tableprefix, temporder); break;
                }

                if (info.Fields == "")
                    info.Fields = "*";

                int pagecount = 100000, recordcount = 100000;
                if (info.Type == "chlgroup")
                {
                    return GetChlGroupChannelDataPage(TypeParse.StrToInt(info.Id), info.Fields, orderby, info.Page, info.Num, out pagecount, out recordcount);
                }
                else
                {
                    if (TypeParse.StrToInt(info.Id) >= 0)
                        where += " AND parentid = " + info.Id;
                    return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
                }
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "频道调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUICommentData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}comments`", tableprefix), orderby = "", where = string.Format(" `{0}comments`.`id` > 0 ", tableprefix);

                string temporder = info.Ordertype == 1 ? "desc" : "asc";
                switch (info.Order)
                {
                    case 0: orderby = string.Format(" `{0}comments`.`addtime` {1}", tableprefix, temporder); break;
                    case 1: orderby = string.Format(" `{0}comments`.`id` {1}", tableprefix, temporder); break;
                    case 2: orderby = string.Format(" `{0}comments`.`diggcount` {1}", tableprefix, temporder); break;
                    case 3: orderby = string.Format(" `{0}comments`.`stampcount` {1}", tableprefix, temporder); break;
                    default: orderby = string.Format(" `{0}comments`.`id` {1}", tableprefix, temporder); break;
                }

                if (TypeParse.StrToInt(info.Id) > 0)
                    where += " AND cid = " + info.Id.ToString();

                if (info.Fields == "")
                    info.Fields = "*";

                int pagecount = 100000, recordcount = 100000;

                return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "评论调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public DataTable GetUIContentData(DataParmInfo info)
        {
            try
            {
                string tableprefix = BaseConfigs.GetTablePrefix;
                string table = string.Format("`{0}contents`", tableprefix), orderby = "", where = string.Format(" `{0}contents`.`orderid` >= -1000 AND `{0}contents`.`status` = 2 ", tableprefix);

                orderby = GetConOrderBy(info.Order, info.Ordertype, tableprefix);

                if (info.Fields == "")
                    info.Fields = "*";

                if (info.Uid > 0)
                    where += string.Format(" AND `{0}contents`.`adduser` = {1}", tableprefix, info.Uid);

                if (info.Ctype >= 0)
                    where += string.Format(" AND `{0}contents`.`typeid` = {1}", tableprefix, info.Ctype);

                if (info.Durdate > 0)
                    where += string.Format(" AND `{0}contents`.`addtime` >= '{1}'", tableprefix, DateTime.Now.AddDays(-info.Durdate).ToString("yyyy-MM-dd HH:mm:ss"));

                if (info.Propery.Length >= 1)
                {
                    foreach (string p in info.Propery.Split(','))
                    {
                        if (p.Trim() == "" || p.Length > 1) continue;
                        where += string.Format(" AND `{0}contents`.`property` like '%,{1},%'", tableprefix, p);
                    }
                }


                int pagecount = 100000, recordcount = 100000;

                if (info.Type == "channel")
                {
                    if (info.Ext.Trim() != "")
                        table = string.Format("`{0}contents` inner join `{0}ext{1}s` on `{0}ext{1}s`.`cid` = `{0}contents`.`id`", tableprefix, info.Ext);
                    info.Fields = info.Fields == "" ? "*" : (string.Format("`{0}contents`.`typeid`,", BaseConfigs.GetTablePrefix) + info.Fields);
                    where += GetChannelIdsWhere(info.Id, info.Self);

                    return GetDataPage(table, tableprefix + "contents.id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
                }
                else if (info.Type == "congroup")
                {
                    return GetConGroupContentDataPage(TypeParse.StrToInt(info.Id), info.Fields, info.Page, info.Num, orderby, where, out pagecount, out recordcount);
                }
                else if (info.Type == "special")
                {
                    return GetSpeccontentDataTable(info.Fields, info.Page, info.Num, TypeParse.StrToInt(info.Id), info.Group, orderby, where, out pagecount, out recordcount);
                }
                else if (info.Type == "tag")
                {
                    return GetTagContentDataPage(TypeParse.StrToInt(info.Id), info.Fields, info.Page, info.Num, orderby, where, out pagecount, out recordcount);
                }
                else
                {
                    return GetDataPage(table, "id", info.Page, info.Num, info.Fields, orderby, where, out pagecount, out recordcount);
                }

            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "文档调用读取发生错误！", ex);
                return new DataTable();
            }
        }

        public string GetChannelIdsWhere(string ids, int self)
        {
            string chlwhere = "";
            foreach (string s in ids.Split(','))
            {
                int id = TypeParse.StrToInt(s);
                if (id <= 0) continue;

                if (self == 0)
                    chlwhere += string.Format(" (channelid = {0} or channelfamily like '%,{0},%' or extchannels like '%,{0},%') OR", s);
                else
                    chlwhere += string.Format(" channelid = {0} OR", s);
            }
            if (chlwhere == "") return "";

            return " AND ( " + chlwhere.Substring(0, chlwhere.Length - 3) + " ) ";
        }

        public string GetConOrderBy(int orderid, int ordertype, string tableprefix)
        {
            string temporder = ordertype == 1 ? "desc" : "asc";
            switch (orderid)
            {
                case 0: return string.Format(" `{0}contents`.`addtime` {1}", tableprefix, temporder);
                case 1: return string.Format(" `{0}contents`.`id` {1}", tableprefix, temporder);
                case 2: return string.Format(" `{0}contents`.`orderid` {1}", tableprefix, temporder);
                case 3: return string.Format(" `{0}contents`.`click` {1}", tableprefix, temporder);
                case 4: return string.Format(" `{0}contents`.`diggcount` {1}", tableprefix, temporder);
                case 5: return string.Format(" `{0}contents`.`stampcount` {1}", tableprefix, temporder);
                case 6: return string.Format(" `{0}contents`.`commentcount` {1}", tableprefix, temporder);
                case 7: return string.Format(" `{0}contents`.`updatetime` {1}", tableprefix, temporder);
                default: return string.Format(" `{0}contents`.`addtime` {1}", tableprefix, temporder);
            }
        }

        #endregion

    }
}