
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Cache;

namespace STA.Data
{
    public class Contents
    {
        private static ContentInfo EditContentEvent(ContentInfo info)
        {
            STACache.GetCacheService().RemoveObject(CacheKeys.DATATABLE + "allsimplecontent");

            if (info.Channelid == 0)
                info.Channelname = "";
            return info;
        }

        public static int EditContent(ContentInfo info)
        {
            info = EditContentEvent(info);
            return DatabaseProvider.GetInstance().EditContent(info);
        }

        public static int AddContent(ContentInfo info)
        {
            info = EditContentEvent(info);
            //if (info.Channelid != 0)
            //{
            //    ChannelInfo chlinfo = GetChannel(info.Channelid);
            //    if (chlinfo != null)
            //        info.Viewgroup = chlinfo.Viewcongroup;
            //}
            return DatabaseProvider.GetInstance().AddContent(info);
        }

        public static int EditContentsWhereChannelDel(int id)
        {
            return DatabaseProvider.GetInstance().EditContentsWhereChannelDel(id);
        }

        public static bool ContentDigg(int id)
        {
            return DatabaseProvider.GetInstance().ContentDigg(id) > 0;
        }

        public static bool EmptyRecycle()
        {
            return DatabaseProvider.GetInstance().EmptyRecycle();
        }

        public static bool CheckContentRepeat(string name)
        {
            return DatabaseProvider.GetInstance().CheckContentRepeat(name);
        }

        public static bool ContentStamp(int id)
        {
            return DatabaseProvider.GetInstance().ContentStamp(id) > 0;
        }

        public static int ContentTypeId(int id)
        {
            return TypeParse.StrToInt(DatabaseProvider.GetInstance().ContentTypeId(id), 1);
        }

        public static DataTable GetContentTableByWhere(int count, string fields, string where)
        {
            return DatabaseProvider.GetInstance().GetContentTableByWhere(count, fields, where);
        }

        /// <summary>
        /// 获取文档相关内容
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <param name="count">获取数量</param>
        /// <param name="fields">文档字段</param>
        /// <returns></returns>
        public static DataTable GetRelateConList(int id, int count, string fields)
        {
            return DatabaseProvider.GetInstance().GetRelateConList(id, count, fields);
        }

        public static string ContentSaveName(int id)
        {
            return TypeParse.ObjToString(DatabaseProvider.GetInstance().ContentSaveName(id)).Trim();
        }

        public static bool ConHtmlStatus(int id, int status)
        {
            return DatabaseProvider.GetInstance().ConHtmlStatus(id, status) > 0;
        }

        public static int DeleteContent(int id)
        {
            return DatabaseProvider.GetInstance().DeleteContent(id);
        }

        public static DataTable GetExtConTableByIds(string extname, string fields, string ids)
        {
            return DatabaseProvider.GetInstance().GetExtConTableByIds(extname, fields, ids);
        }

        public static DataTable GetPublishSpecialTable(string condition)
        {
            return DatabaseProvider.GetInstance().GetPublishSpecialTable(condition);
        }

        public static DataTable GetPublishRssTable(string ids)
        {
            return DatabaseProvider.GetInstance().GetPublishRssTable(ids);
        }

        public static DataTable RepepeatConTitleCheck(int typeid)
        {
            return DatabaseProvider.GetInstance().RepeatConTitleCheck(typeid);
        }

        public static string GetContentPublishCondition(string startdate, string enddate, int minid, int maxid, int channelid)
        {
            return DatabaseProvider.GetInstance().GetContentPublishCondition(startdate, enddate, minid, maxid, channelid);
        }

        public static int AddChannel(ChannelInfo info)
        {
            info = EditChannelEvent(info);
            return DatabaseProvider.GetInstance().AddChannel(info);
        }

        public static string ChannelConRule(int id)
        {
            return TypeParse.ObjToString(DatabaseProvider.GetInstance().ChannelConRule(id));
        }

        public static int DeleteChannel(int id)
        {
            return DatabaseProvider.GetInstance().DeleteChannel(id);
        }

        public static int EditChannel(ChannelInfo info)
        {
            info = EditChannelEvent(info);
            return DatabaseProvider.GetInstance().EditChannel(info);
        }

        private static ChannelInfo EditChannelEvent(ChannelInfo info)
        {
            //STACache.GetCacheService().RemoveObject(CacheKeys.DATATABLE + "allchannel");
            return info;
        }

        public static ContentInfo GetContentForHtml(int id)
        {
            ContentInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetContentForHtml(id))
            {
                if (reader.Read())
                {
                    info = new ContentInfo();
                    info.Savepath = reader["savepath"].ToString();
                    info.Filename = reader["filename"].ToString();
                    info.Content = reader["content"].ToString();
                }
            }
            return info;
        }

        public static PageInfo GetPageForHtml(int id)
        {
            PageInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPageForHtml(id))
            {
                if (reader.Read())
                {
                    info = new PageInfo();
                    info.Savepath = reader["savepath"].ToString().Trim();
                    info.Filename = reader["filename"].ToString().Trim();
                    info.Content = reader["content"].ToString().Trim();
                }
            }
            return info;
        }

        public static ChannelInfo GetChannelForHtml(int id)
        {
            ChannelInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetChannelForHtml(id))
            {
                if (reader.Read())
                {
                    info = new ChannelInfo();
                    info.Listrule = reader["listrule"].ToString().Trim();
                    info.Savepath = reader["savepath"].ToString().Trim();
                    info.Filename = reader["filename"].ToString().Trim();
                }
            }
            return info;
        }

        private static ChannelInfo LoadChannelInfo(IDataReader reader)
        {
            ChannelInfo info = new ChannelInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Typeid = short.Parse(TypeParse.ObjToString(reader["typeid"]));
            info.Parentid = TypeParse.StrToInt(reader["parentid"], 0);
            info.Name = TypeParse.ObjToString(reader["name"]);
            info.Savepath = TypeParse.ObjToString(reader["savepath"]).Trim();
            info.Filename = TypeParse.ObjToString(reader["filename"]).Trim();
            info.Ctype = byte.Parse(TypeParse.ObjToString(reader["ctype"]));
            info.Img = reader["img"].ToString();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Covertem = TypeParse.ObjToString(reader["covertem"]);
            info.Listem = TypeParse.ObjToString(reader["listem"]);
            info.Contem = TypeParse.ObjToString(reader["contem"]);
            info.Conrule = TypeParse.ObjToString(reader["conrule"]);
            info.Listrule = TypeParse.ObjToString(reader["listrule"]);
            info.Seotitle = TypeParse.ObjToString(reader["seotitle"]);
            info.Seokeywords = TypeParse.ObjToString(reader["seokeywords"]);
            info.Seodescription = TypeParse.ObjToString(reader["seodescription"]);
            info.Moresite = byte.Parse(TypeParse.ObjToString(reader["moresite"]));
            info.Siteurl = TypeParse.ObjToString(reader["siteurl"]);
            info.Content = TypeParse.ObjToString(reader["content"]);
            info.Ispost = byte.Parse(TypeParse.ObjToString(reader["ispost"]));
            info.Ishidden = byte.Parse(TypeParse.ObjToString(reader["ishidden"]));
            info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
            info.Listcount = TypeParse.StrToInt(reader["listcount"], 0);
            info.Viewgroup = TypeParse.ObjToString(reader["viewgroup"]);
            info.Viewcongroup = TypeParse.ObjToString(reader["viewcongroup"]);
            info.Ipaccess = TypeParse.ObjToString(reader["ipaccess"]);
            info.Ipdenyaccess = TypeParse.ObjToString(reader["ipdenyaccess"]);
            return info;
        }

        public static DataTable GetContentsByChannelId(int id)
        {
            return DatabaseProvider.GetInstance().GetContentsByChannelId(id);
        }

        public static ChannelInfo GetChannel(int id)
        {
            ChannelInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetChannel(id))
            {
                if (reader.Read())
                {
                    info = LoadChannelInfo(reader);
                }
            }
            return info;
        }

        public static int ChannelParentId(int id)
        {
            return DatabaseProvider.GetInstance().ChannelParentId(id);
        }

        public static List<ChannelInfo> GetAllChannel()
        {
            List<ChannelInfo> list = new List<ChannelInfo>();
            using (IDataReader reader = DatabaseProvider.GetInstance().GetAllChannel())
            {
                while (reader.Read())
                {
                    list.Add(LoadChannelInfo(reader));
                }
            }
            return list;
        }

        public static DataTable GetChannelDataTable()
        {
            return DatabaseProvider.GetInstance().GetChannelDataTable("*");
        }

        public static DataTable GetChannelDataTable(string fields)
        {
            return DatabaseProvider.GetInstance().GetChannelDataTable(fields);
        }

        public static DataTable GetContypeDataTable()
        {
            return DatabaseProvider.GetInstance().GetContypeDataTable();
        }

        public static int ContentCountByChannelId(int id)
        {
            return DatabaseProvider.GetInstance().ContentCountByChannelId(id);
        }

        public static string GetExtFieldValue(string ext, int cid, string field)
        {
            return DatabaseProvider.GetInstance().GetExtFieldValue(ext, cid, field);
        }

        public static int AddContype(ContypeInfo info)
        {
            return DatabaseProvider.GetInstance().AddContype(info);
        }

        public static int DeleteContype(int id)
        {
            return DatabaseProvider.GetInstance().DeleteContype(id);
        }

        public static int EditContype(ContypeInfo info)
        {
            return DatabaseProvider.GetInstance().EditContype(info);
        }

        public static ContypeInfo GetContype(int id)
        {
            ContypeInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetContype(id))
            {
                if (reader.Read())
                {
                    info = LoadContypeInfo(reader);
                }
            }
            return info;
        }

        private static ContypeInfo LoadContypeInfo(IDataReader reader)
        {
            ContypeInfo info = new ContypeInfo();
            info.Id = short.Parse(TypeParse.ObjToString(reader["id"]));
            info.Open = byte.Parse(TypeParse.ObjToString(reader["open"]));
            info.System = byte.Parse(TypeParse.ObjToString(reader["system"]));
            info.Ename = TypeParse.ObjToString(reader["ename"]);
            info.Name = TypeParse.ObjToString(reader["name"]);
            info.Maintable = TypeParse.ObjToString(reader["maintable"]);
            info.Extable = TypeParse.ObjToString(reader["extable"]);
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Fields = reader["fields"].ToString();
            info.Bgaddmod = reader["bgaddmod"].ToString();
            info.Bgeditmod = reader["bgeditmod"].ToString();
            info.Bglistmod = reader["bglistmod"].ToString();
            info.Addmod = reader["addmod"].ToString();
            info.Editmod = reader["editmod"].ToString();
            info.Listmod = reader["listmod"].ToString();
            info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
            return info;
        }

        public static bool ExistContypeField(int id, string extable, string ename)
        {
            return DatabaseProvider.GetInstance().ExistContypeField(id, extable, ename) > 0;
        }

        public static int AddContypeField(ContypefieldInfo info)
        {
            return DatabaseProvider.GetInstance().AddContypeField(info);
        }

        public static bool EditContypeField(ContypefieldInfo info)
        {
            return DatabaseProvider.GetInstance().EditContypeField(info) > 0;
        }

        public static bool DeleteContypeField(int id)
        {
            return DatabaseProvider.GetInstance().DeleteContypeField(id) > 0;
        }
        public static bool DeleteContypeFieldByCid(int cid)
        {
            return DatabaseProvider.GetInstance().DeleteContypeFieldByCid(cid) > 0;
        }

        public static ContypefieldInfo GetContypeField(int id)
        {
            ContypefieldInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetContypeField(id))
            {
                if (reader.Read())
                {
                    info = LoadContypeField(reader);
                }
            }
            return info;
        }

        public static List<ContypefieldInfo> GetContypeFieldList(int cid)
        {
            List<ContypefieldInfo> list = new List<ContypefieldInfo>();
            using (IDataReader reader = DatabaseProvider.GetInstance().GetContypeFieldList(cid))
            {
                while (reader.Read())
                {
                    list.Add(LoadContypeField(reader));
                }
            }
            return list;
        }

        private static ContypefieldInfo LoadContypeField(IDataReader reader)
        {
            ContypefieldInfo info = new ContypefieldInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Cid = TypeParse.StrToInt(reader["cid"]);
            info.Fieldname = reader["fieldname"].ToString();
            info.Fieldtype = reader["fieldtype"].ToString();
            info.Length = TypeParse.StrToInt(reader["length"], 20);
            info.Isnull = byte.Parse(reader["isnull"].ToString());
            info.Defvalue = reader["defvalue"].ToString();
            info.Desctext = reader["desctext"].ToString();
            info.Tiptext = reader["tiptext"].ToString();
            info.Orderid = TypeParse.StrToInt(reader["orderid"]);
            info.Vinnertext = reader["vinnertext"].ToString();
            return info;
        }

        public static int AddPage(PageInfo info)
        {
            return DatabaseProvider.GetInstance().AddPage(info);
        }

        public static string PageSaveName(int id)
        {
            return TypeParse.ObjToString(DatabaseProvider.GetInstance().PageSaveName(id)).Trim();
        }

        public static bool EditPage(PageInfo info)
        {
            return DatabaseProvider.GetInstance().EditPage(info);
        }

        public static bool DeletePage(int id)
        {
            return DatabaseProvider.GetInstance().DeletePage(id);
        }

        public static List<PageInfo> GetAlikePage(string alikeid)
        {
            List<PageInfo> list = new List<PageInfo>();
            using (IDataReader reader = DatabaseProvider.GetInstance().GetAlikePage(alikeid))
            {
                if (reader.Read())
                {
                    PageInfo info = LoadPageInfo(reader);
                    list.Add(info);
                }
            }
            return list;
        }

        public static PageInfo GetPage(int id)
        {
            PageInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPage(id))
            {
                if (reader.Read())
                {
                    info = LoadPageInfo(reader);
                }
            }
            return info;
        }

        private static PageInfo LoadPageInfo(IDataReader reader)
        {
            PageInfo info = new PageInfo();
            info.Id = TypeParse.StrToInt(reader["id"]);
            info.Name = reader["name"].ToString().Trim();
            info.Alikeid = reader["alikeid"].ToString().Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"].ToString());
            info.Seotitle = reader["seotitle"].ToString();
            info.Seokeywords = reader["seokeywords"].ToString();
            info.Seodescription = reader["seodescription"].ToString();
            info.Ishtml = byte.Parse(reader["ishtml"].ToString());
            info.Savepath = reader["savepath"].ToString().Trim();
            info.Filename = reader["filename"].ToString().Trim();
            info.Template = reader["template"].ToString().Trim();
            info.Content = reader["content"].ToString().Trim();
            info.Orderid = TypeParse.StrToInt(reader["orderid"]);
            return info;
        }
        public static DataTable GetPageLikeIdList()
        {
            return DatabaseProvider.GetInstance().GetPageLikeIdList();
        }

        public static DataTable GetDataPage(string tbname, string fieldkey, int pagecurrent, int pagesize, string fieldshow, string fieldorder, string where, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetDataPage(tbname, fieldkey, pagecurrent, pagesize, fieldshow, fieldorder, where, out pagecount, out recordcount);
        }

        public static DataTable GetPageDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetPageDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static DataTable GetContentDataPage(string fields, int pagecurrent, int pagesize, string where, string orderby, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetContentDataPage(fields, pagecurrent, pagesize, where, orderby, out pagecount, out recordcount);
        }

        public static DataTable GetContentDataPage(string fields, string extname, int pagecurrent, int pagesize, string where, string orderby, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetContentDataPage(fields, extname, pagecurrent, pagesize, where, orderby, out pagecount, out recordcount);
        }

        public static DataTable GetContentDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetContentDataPage("", pagecurrent, pagesize, where, "", out pagecount, out recordcount);
        }

        private static ContentInfo LoadShortContentInfo(IDataReader reader)
        {
            ContentInfo info = new ContentInfo();
            info.Id = TypeParse.StrToInt(reader["id"]);
            info.Typeid = short.Parse(reader["typeid"].ToString());
            info.Typename = reader["typename"].ToString();
            info.Addusername = reader["addusername"].ToString();
            info.Lasteditusername = reader["lasteditusername"].ToString();
            info.Channelfamily = reader["channelfamily"].ToString();
            info.Channelid = TypeParse.StrToInt(reader["channelid"], 0);
            info.Channelname = reader["channelname"].ToString();
            info.Extchannels = reader["extchannels"].ToString();
            info.Title = reader["title"].ToString();
            info.Subtitle = reader["subtitle"].ToString();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Updatetime = TypeParse.StrToDateTime(reader["updatetime"]);
            info.Color = reader["color"].ToString();
            info.Property = reader["property"].ToString();
            info.Adduser = TypeParse.StrToInt(reader["adduser"], 0);
            info.Lastedituser = TypeParse.StrToInt(reader["lastedituser"], 0);
            info.Author = reader["author"].ToString();
            info.Source = reader["source"].ToString();
            info.Img = reader["img"].ToString();
            info.Url = reader["url"].ToString();
            info.Seotitle = reader["seotitle"].ToString();
            info.Seokeywords = reader["seokeywords"].ToString();
            info.Seodescription = reader["seodescription"].ToString();
            info.Savepath = reader["savepath"].ToString().Trim();
            info.Filename = reader["filename"].ToString().Trim();
            info.Template = reader["template"].ToString();
            info.Content = reader["content"].ToString();
            info.Status = byte.Parse(reader["status"].ToString());
            info.Viewgroup = reader["viewgroup"].ToString();
            info.Iscomment = byte.Parse(reader["iscomment"].ToString());
            info.Ishtml = byte.Parse(reader["ishtml"].ToString());
            info.Click = TypeParse.StrToInt(reader["click"], 1);
            info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
            info.Diggcount = TypeParse.StrToInt(reader["diggcount"], 0);
            info.Stampcount = TypeParse.StrToInt(reader["stampcount"], 0);
            info.Commentcount = TypeParse.StrToInt(reader["commentcount"], 0);
            info.Credits = TypeParse.StrToInt(reader["credits"], 0);
            info.Relates = TypeParse.ObjToString(reader["relates"]);
            return info;
        }

        public static ContentInfo GetShortContent(int id)
        {
            ContentInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetShortContent(id))
            {
                if (reader.Read())
                {
                    info = LoadShortContentInfo(reader);
                }
            }
            return info;
        }

        //public static ContentInfo GetContent(int id, DataTable ctedt)
        //{
        //    ContentInfo info = null;
        //    using (IDataReader reader = DatabaseProvider.GetInstance().GetContent(id))
        //    {
        //        if (reader.Read())
        //        {
        //            info = LoadShortContentInfo(reader);

        //            DataRow[] tdrs = ctedt.Select("id=" + info.Typeid.ToString());
        //            if (tdrs.Length > 0)
        //            {
        //                DataRow ctype = tdrs[0];
        //                if (ctype["fields"].ToString().Trim() != string.Empty)
        //                {
        //                    using (IDataReader reader3 = DatabaseProvider.GetInstance().GetExtFieldByCid(ctype["fields"].ToString().Trim(), ctype["extable"].ToString().Trim(), info.Id.ToString()))
        //                    {
        //                        if (reader3.Read())
        //                        {
        //                            info.Ext = new Hashtable(50);
        //                            foreach (string field in ctype["fields"].ToString().Trim().Split(','))
        //                            {
        //                                info.Ext.Add(field, reader3[field]);
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return info;
        //}

        public static ContentInfo GetContent(int id, int typeid)
        {
            ContentInfo info = null;
            string extfields = "";
            using (IDataReader reader = DatabaseProvider.GetInstance().GetContent(id, typeid, ref extfields))
            {
                if (reader.Read())
                {
                    info = LoadShortContentInfo(reader);
                    if (extfields.Length > 0)
                    {
                        info.Ext = new Hashtable(50);
                        foreach (string field in extfields.Split(','))
                        {
                            info.Ext.Add(field, reader[field]);
                        }
                    }
                }
            }
            return info;
        }

        public static DataTable GetTableField(string tablename)
        {
            return DatabaseProvider.GetInstance().GetTableField(tablename);
        }

        public static DataTable GetAllTableName()
        {
            return DatabaseProvider.GetInstance().GetAllTableName();
        }

        public static DataTable GetTableData(string tablename)
        {
            return DatabaseProvider.GetInstance().GetTableData(tablename);
        }

        public static string GetContentSearchCondition(int typeid, string addusers, int reclyle, int channelid, int status, string property,
                                                                                            string startdate, string enddate, string keyword)
        {
            return DatabaseProvider.GetInstance().GetContentSearchCondition(typeid, addusers, reclyle, channelid, status, property.Trim(),
                                                                                            startdate.Trim(), enddate.Trim(), keyword.Trim());
        }

        public static string GetContentSearchCondition(int typeid, string addusers, int reclyle, int channelid, int self, int status, string property,
                                                                                    string startdate, string enddate, string keyword)
        {
            return DatabaseProvider.GetInstance().GetContentSearchCondition(typeid, addusers, reclyle, channelid, self, status, property.Trim(),
                                                                                            startdate.Trim(), enddate.Trim(), keyword.Trim());
        }

        public static bool PutContentRecycle(int id)
        {
            return DatabaseProvider.GetInstance().PutContentRecycle(id) > 0;
        }

        public static bool RecoverContent(int id)
        {
            return DatabaseProvider.GetInstance().RecoverContent(id) > 0;
        }

        public static int AddConGroup(CongroupInfo info)
        {
            return DatabaseProvider.GetInstance().AddConGroup(info);
        }

        public static bool DelConGroup(int id)
        {
            return DatabaseProvider.GetInstance().DelConGroup(id) > 0;
        }

        public static bool EditConGroup(CongroupInfo info)
        {
            return DatabaseProvider.GetInstance().EditConGroup(info) > 0;
        }

        public static CongroupInfo GetConGroup(int id)
        {
            CongroupInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetConGroup(id))
            {
                if (reader.Read())
                {
                    info = new CongroupInfo();
                    info.Id = id;
                    info.Type = byte.Parse(reader["type"].ToString());
                    info.Name = reader["name"].ToString().Trim();
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Desctext = reader["desctext"].ToString();
                }
            }
            return info;
        }
        public static string GetConGroupSearchCondition(int type, string keywords)
        {
            return DatabaseProvider.GetInstance().GetConGroupSearchCondition(type, keywords);
        }

        public static DataTable GetConGroupDataPage(int pageIndex, int pagesize, string where, out int pageCount, out int recordCount)
        {
            return DatabaseProvider.GetInstance().GetConGroupDataPage(pageIndex, pagesize, where, out pageCount, out recordCount);
        }

        public static DataTable GetConGroupContentDataPage(int groupid, string columns, int pageIndex, int pageSize, out int pageCount, out int recordCount)
        {
            return DatabaseProvider.GetInstance().GetConGroupContentDataPage(groupid, columns, pageIndex, pageSize, out pageCount, out recordCount);
        }

        public static bool DelGroupCon(int id)
        {
            return DatabaseProvider.GetInstance().DelGroupCon(id) > 0;
        }

        public static bool DelGroupCon(int cid, int gid)
        {
            return DatabaseProvider.GetInstance().DelGroupCon(cid, gid) > 0;
        }

        public static bool AddGroupCon(int gid, int cid, int orderid)
        {
            return DatabaseProvider.GetInstance().AddGroupCon(gid, cid, orderid) > 0;
        }

        public static bool EditGroupCon(int id, int orderid)
        {
            return DatabaseProvider.GetInstance().EditGroupCon(id, orderid) > 0;
        }

        public static DataTable GetChannelGroupIds(int gid)
        {
            return DatabaseProvider.GetInstance().GetChannelGroupIds(gid);
        }

        public static int AddLink(LinkInfo info)
        {
            return DatabaseProvider.GetInstance().AddLink(info);
        }

        public static bool EditLink(LinkInfo info)
        {
            return DatabaseProvider.GetInstance().EditLink(info) > 0;
        }

        public static bool DelLink(int id)
        {
            return DatabaseProvider.GetInstance().DelLink(id) > 0;
        }

        public static LinkInfo GetLink(int id)
        {
            LinkInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetLink(id))
            {
                if (reader.Read())
                {
                    info = new LinkInfo();
                    info.Id = id;
                    info.Typeid = TypeParse.StrToInt(reader["typeid"]);
                    info.Name = reader["name"].ToString().Trim();
                    info.Url = reader["url"].ToString().Trim();
                    info.Logo = reader["logo"].ToString().Trim();
                    info.Email = reader["email"].ToString();
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Description = reader["description"].ToString();
                    info.Orderid = TypeParse.StrToInt(reader["orderid"]);
                    info.Status = byte.Parse(reader["status"].ToString());
                }
            }
            return info;
        }

        public static DataTable GetLinkDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetLinkDataPage(pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static string GetLinkSearchCondition(int typeid, int status, string startdate, string enddate, string keywords)
        {
            return DatabaseProvider.GetInstance().GetLinkSearchCondition(typeid, status, startdate, enddate, keywords);
        }

        public static int AddLinkType(LinktypeInfo info)
        {
            return DatabaseProvider.GetInstance().AddLinkType(info);
        }

        public static bool DelLinkType(int id)
        {
            return DatabaseProvider.GetInstance().DelLinkType(id) > 0;
        }

        public static bool EditLinkType(LinktypeInfo info)
        {
            return DatabaseProvider.GetInstance().EditLinkType(info) > 0;
        }

        public static DataTable GetLinkType()
        {
            return DatabaseProvider.GetInstance().GetLinkType();
        }

        public static bool VerifyLink(int id, int status)
        {
            return DatabaseProvider.GetInstance().VerifyLink(id, status) > 0;
        }

        public static LinktypeInfo GetLinkType(int id)
        {
            LinktypeInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetLinkType(id))
            {
                if (reader.Read())
                {
                    info = new LinktypeInfo();
                    info.Id = id;
                    info.Name = reader["name"].ToString().Trim();
                    info.Orderid = TypeParse.StrToInt(reader["orderid"]);
                }
            }
            return info;
        }
        public static DataTable GetLinkTypePage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetLinkTypePage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static int AddArea(AreaInfo info)
        {
            return DatabaseProvider.GetInstance().AddArea(info);
        }

        public static bool EditArea(AreaInfo info)
        {
            return DatabaseProvider.GetInstance().EditArea(info) > 0;
        }


        public static string CommentSummary(int ctype, int cid)
        {
            return DatabaseProvider.GetInstance().CommentSummary(ctype, cid);
        }

        public static int AddComment(CommentInfo info)
        {
            return DatabaseProvider.GetInstance().AddComment(info);
        }

        public static bool EditComment(CommentInfo info)
        {
            return DatabaseProvider.GetInstance().EditComment(info) > 0;
        }

        public static bool DelComment(int id, int ctype)
        {
            return DatabaseProvider.GetInstance().DelComment(id, ctype) > 0;
        }

        public static bool DelCommentByUid(int uid)
        {
            return DatabaseProvider.GetInstance().DelCommentByUid(uid) > 0;
        }

        public static int DelCommentByCid(int cid, int ctype)
        {
            return DatabaseProvider.GetInstance().DelCommentByCid(cid, ctype);
        }

        public static bool CommentStatus(int id, CommentStatus status)
        {
            return DatabaseProvider.GetInstance().CommentStatus(id, (int)status) > 0;
        }

        public static string GetCommentSearchCondition(int status, int ctype, int cid, string users, string ip, string startdate, string enddate, string contitle, string keyword)
        {
            return DatabaseProvider.GetInstance().GetCommentSearchCondition(status, ctype, cid, users, ip, startdate, enddate, contitle, keyword);
        }

        public static bool CommentDigg(int id)
        {
            return DatabaseProvider.GetInstance().CommentDigg(id) > 0;
        }

        public static bool commentStamp(int id)
        {
            return DatabaseProvider.GetInstance().commentStamp(id) > 0;
        }

        public static CommentInfo GetComment(int id)
        {
            CommentInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetComment(id))
            {
                if (reader.Read())
                {
                    info = new CommentInfo();
                    info.Id = id;
                    info.Ctype = TypeParse.StrToInt(reader["ctype"]);
                    info.Cid = TypeParse.StrToInt(reader["cid"]);
                    info.Uid = TypeParse.StrToInt(reader["uid"]);
                    info.Username = reader["username"].ToString();
                    info.Title = reader["title"].ToString();
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Verifytime = TypeParse.StrToDateTime(reader["verifytime"]);
                    info.Userip = reader["userip"].ToString();
                    info.Status = (CommentStatus)byte.Parse(reader["status"].ToString());
                    info.Diggcount = TypeParse.StrToInt(reader["diggcount"]);
                    info.Stampcount = TypeParse.StrToInt(reader["stampcount"]);
                    info.Msg = reader["msg"].ToString();
                    info.Contitle = reader["contitle"].ToString();
                    info.Quote = reader["quote"].ToString();
                    info.Replay = TypeParse.StrToInt(reader["replay"]);
                    info.City = reader["city"].ToString();
                    info.Star = TypeParse.StrToInt(reader["star"]);
                    info.Useragent = reader["useragent"].ToString();
                }
            }
            return info;
        }

        public static bool VerifyContent(int id, int status)
        {
            return DatabaseProvider.GetInstance().VerifyContent(id, status) > 0;
        }

        public static DataTable GetCommentDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return GetCommentDataPage("", "", pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static DataTable GetCommentDataPage(string fields, string orderby, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetCommentDataPage(fields, orderby, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        public static bool DelArea(int id)
        {
            return DatabaseProvider.GetInstance().DelArea(id) > 0;
        }

        public static DataTable GetAreaDataTable()
        {
            return DatabaseProvider.GetInstance().GetAreaDataTable();
        }

        public static DataTable GetAreaDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetAreaDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static int AddAttachment(AttachmentInfo info)
        {
            return DatabaseProvider.GetInstance().AddAttachment(info);
        }

        public static bool EditAttachment(AttachmentInfo info)
        {
            return DatabaseProvider.GetInstance().EditAttachment(info) > 0;
        }

        public static bool DelAttachment(int id)
        {
            return DatabaseProvider.GetInstance().DelAttachment(id) > 0;
        }

        private static AttachmentInfo LoadAttachmentInfo(IDataReader reader)
        {
            AttachmentInfo info = null;
            if (reader.Read())
            {
                info = new AttachmentInfo();
                info.Id = TypeParse.StrToInt(reader["id"]);
                info.Uid = TypeParse.StrToInt(reader["uid"]);
                info.Lastedituid = TypeParse.StrToInt(reader["lastedituid"]);
                info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                info.Lasteditime = TypeParse.StrToDateTime(reader["lastedittime"]);
                info.Filename = reader["filename"].ToString();
                info.Description = reader["description"].ToString();
                info.Filetype = reader["filetype"].ToString();
                info.Filesize = TypeParse.StrToInt(reader["filesize"]);
                info.Attachment = reader["attachment"].ToString();
                info.Width = TypeParse.StrToInt(reader["width"]);
                info.Fileext = reader["fileext"].ToString();
                info.Username = reader["username"].ToString();
                info.Lasteditusername = reader["lasteditusername"].ToString();
                info.Height = TypeParse.StrToInt(reader["height"]);
                info.Downloads = TypeParse.StrToInt(reader["downloads"]);
                info.Attachcredits = TypeParse.StrToInt(reader["attachcredits"]);
            }
            return info;
        }

        public static AttachmentInfo GetAttachment(string filename)
        {
            return LoadAttachmentInfo(DatabaseProvider.GetInstance().GetAttachment(filename));
        }

        public static string GetAttachSearchCondition(string startdate, string enddate, string users, string fileext, int minsize, int maxsize, string keywords)
        {
            return DatabaseProvider.GetInstance().GetAttachSearchCondition(startdate, enddate, users, fileext, minsize, maxsize, keywords);
        }

        public static AttachmentInfo GetAttachment(int id)
        {
            return LoadAttachmentInfo(DatabaseProvider.GetInstance().GetAttachment(id));
        }

        public static DataTable GetAttachmentDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetAttachmentDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static int AddAd(AdInfo info)
        {
            return DatabaseProvider.GetInstance().AddAd(info);
        }

        public static string AdFilename(int id)
        {
            return DatabaseProvider.GetInstance().AdFilename(id);
        }

        public static string AdFilename(string adname)
        {
            return DatabaseProvider.GetInstance().AdFilename(adname);
        }

        public static int EditAd(AdInfo info)
        {
            return DatabaseProvider.GetInstance().EditAd(info);
        }

        public static bool DelAd(int id)
        {
            return DatabaseProvider.GetInstance().DelAd(id) > 0;
        }

        public static AdInfo GetAd(int id)
        {
            AdInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetAd(id))
            {
                if (reader.Read())
                {
                    info = LoadAdInfo(reader);
                }
            }
            return info;
        }

        public static AdInfo GetAd(string name)
        {
            AdInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetAd(name))
            {
                if (reader.Read())
                {
                    info = LoadAdInfo(reader);
                }
            }
            return info;
        }

        private static AdInfo LoadAdInfo(IDataReader reader)
        {
            AdInfo info = new AdInfo();
            info.Id = TypeParse.StrToInt(reader["id"]);
            info.Name = reader["name"].ToString();
            info.Status = (AdStatus)TypeParse.StrToInt(reader["status"].ToString());
            info.Filename = reader["filename"].ToString();
            info.Adtype = (AdType)TypeParse.StrToInt(reader["adtype"].ToString());
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Startdate = TypeParse.StrToDateTime(reader["startdate"]);
            info.Enddate = TypeParse.StrToDateTime(reader["enddate"]);
            info.Click = TypeParse.StrToInt(reader["click"]);
            info.Paramarray = reader["paramarray"].ToString();
            info.Outdate = reader["outdate"].ToString();
            return info;
        }

        public static string GetAdSearchCondtion(string startdate, string enddate, int adtype, string keywords)
        {
            return DatabaseProvider.GetInstance().GetAdSearchCondtion(startdate, enddate, adtype, keywords);
        }

        public static DataTable GetAdDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetAdDataPage(pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }

        public static int AddSearchCache(SearchcacheInfo info)
        {
            return DatabaseProvider.GetInstance().AddSearchCache(info);
        }


        public static bool DelSearchCache(int id)
        {
            return DatabaseProvider.GetInstance().DelSearchCache(id) > 0;
        }

        public static DataTable GetSearchCacheDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetSearchCacheDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        /// <summary>
        /// 添加标签 
        /// </summary>
        /// <param name="tagName">每个用半角逗号或空格隔开</param>
        /// <param name="conId"></param>
        public static void AddTag(string tagName, int conId)
        {
            DelTagsByCid(conId);
            if (conId == 0) return;
            string[] tags = Utils.DistinctStringArray(tagName.Split(',', ' ', ','));
            foreach (string tag in tags)
            {
                if (tag.Trim() == "") continue;
                DatabaseProvider.GetInstance().AddTag(tag, conId);
            }
        }

        public static bool DelTag(string tagName)
        {
            return DatabaseProvider.GetInstance().DelTag(tagName) > 0;
        }

        public static int AddTag(string TagName)
        {
            return DatabaseProvider.GetInstance().AddTag(TagName);
        }

        public static bool EditTag(int id, string tagname)
        {
            return DatabaseProvider.GetInstance().EditTag(id, tagname) > 0;
        }

        public static DataTable GetTagDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetTagDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static bool DelTag(int tid)
        {
            return DatabaseProvider.GetInstance().DelTag(tid) > 0;
        }

        public static int DelTagByCid(string tagName, int conId)
        {
            return DatabaseProvider.GetInstance().DelTagByCid(tagName, conId);
        }

        public static bool DelTagsByCid(int conId)
        {
            return DatabaseProvider.GetInstance().DelTagsByCid(conId) > 0;
        }

        public static DataTable GetTagsByCid(int conId)
        {
            return DatabaseProvider.GetInstance().GetTagsByCid(conId);
        }


        public static int AddSpecgroup(SpecgroupInfo info)
        {
            return DatabaseProvider.GetInstance().AddSpecgroup(info);
        }

        public static bool DelSpecgroup(int id)
        {
            return DatabaseProvider.GetInstance().DelSpecgroup(id) > 0;
        }

        public static bool DelSpecgroupBySpecid(int specid)
        {
            return DatabaseProvider.GetInstance().DelSpecgroupBySpecid(specid) > 0;
        }

        public static bool EditSpecgroup(SpecgroupInfo info)
        {
            return DatabaseProvider.GetInstance().EditSpecgroup(info) > 0;
        }

        public static int AddSpeccontent(SpecontentInfo info)
        {
            return DatabaseProvider.GetInstance().AddSpeccontent(info);
        }

        public static bool EditSpeccontent(SpecontentInfo info)
        {
            return DatabaseProvider.GetInstance().EditSpeccontent(info) > 0;
        }

        public static bool DelSpeccontent(int specid, int contentid)
        {
            return DatabaseProvider.GetInstance().DelSpeccontent(specid, contentid) > 0;
        }

        public static DataTable GetSpeccontentDataTable(int pageIndex, int pagesize, int specid, int groupid, string condition, out int pageCount, out int recordCount)
        {
            return DatabaseProvider.GetInstance().GetSpeccontentDataTable(pageIndex, pagesize, specid, groupid, condition, out pageCount, out recordCount);
        }

        public static DataTable GetSpecgroups(int specid)
        {
            return DatabaseProvider.GetInstance().GetSpecgroups(specid);
        }

        public static DataTable GetSpeconids(int specid)
        {
            return DatabaseProvider.GetInstance().GetSpeconids(specid);
        }

        public static string GetPageSearchCondition(string startdate, string enddate, int minid, int maxid, string keywords)
        {
            return DatabaseProvider.GetInstance().GetPageSearchCondition(startdate, enddate, minid, maxid, keywords);
        }

        public static DataTable GetPublishChannelTable(string ids)
        {
            return DatabaseProvider.GetInstance().GetPublishChannelTable(ids);
        }

        public static DataTable GetPublishPageTable(string condition)
        {
            return DatabaseProvider.GetInstance().GetPublishPageTable(condition);
        }

        public static DataTable GetPublishContentTable(string condition)
        {
            return DatabaseProvider.GetInstance().GetPublishContentTable(condition);
        }

        public static int AddUrlStaticize(StaticizeInfo info)
        {
            return DatabaseProvider.GetInstance().AddUrlStaticize(info);
        }
        public static bool DelUrlStaticize(int id)
        {
            return DatabaseProvider.GetInstance().DelUrlStaticize(id) > 0;
        }

        public static bool EditUrlStaticize(StaticizeInfo info)
        {
            return DatabaseProvider.GetInstance().EditUrlStaticize(info) > 0;
        }

        public static StaticizeInfo GetUrlStaticize(int id)
        {
            StaticizeInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetUrlStaticize(id))
            {
                if (reader.Read())
                {
                    info = new StaticizeInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Title = reader["title"].ToString();
                    info.Charset = reader["charset"].ToString();
                    info.Url = reader["url"].ToString();
                    info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
                    info.Maketime = TypeParse.StrToDateTime(reader["maketime"]);
                    info.Savepath = reader["savepath"].ToString();
                    info.Filename = reader["filename"].ToString();
                    info.Suffix = reader["suffix"].ToString();
                }
            }
            return info;
        }

        public static DataTable GetUrlStaticizeDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetUrlStaticizeDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static int UpdateContentClick(int id, bool backclick)
        {
            return DatabaseProvider.GetInstance().UpdateContentClick(id, backclick);
        }

        public static int GetContentClick(int id)
        {
            return DatabaseProvider.GetInstance().GetContentClick(id);
        }

        public static string GetDiggStamp(int id)
        {
            return DatabaseProvider.GetInstance().GetDiggStamp(id);
        }

        public static int ConCommentCount(int id)
        {
            return DatabaseProvider.GetInstance().ConCommentCount(id);
        }

        public static bool UpdateSoftDownloadCount(int id)
        {
            return DatabaseProvider.GetInstance().UpdateSoftDownloadCount(id) > 0;
        }

        public static int GetSoftDownloadCount(int id)
        {
            return DatabaseProvider.GetInstance().GetSoftDownloadCount(id);
        }

        public static DataTable MagazineLikeIds()
        {
            return DatabaseProvider.GetInstance().MagazineLikeIds();
        }

        public static int AddMagazine(MagazineInfo info)
        {
            return DatabaseProvider.GetInstance().AddMagazine(info);
        }

        public static int EditMagazine(MagazineInfo info)
        {
            return DatabaseProvider.GetInstance().EditMagazine(info);
        }

        public static string GetMagazineSearchCondition(string startdate, string enddate, string likeid, int status, string keywords)
        {
            return DatabaseProvider.GetInstance().GetMagazineSearchCondition(startdate, enddate, likeid, status, keywords);
        }

        public static bool DelMagazine(int id)
        {
            return DatabaseProvider.GetInstance().DelMagazine(id) > 0;
        }

        public static MagazineInfo GetMagazine(int id)
        {
            MagazineInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetMagazine(id))
            {
                if (reader.Read())
                {
                    info = LoadMagazineInfo(reader);
                }
            }
            return info;
        }

        private static MagazineInfo LoadMagazineInfo(IDataReader reader)
        {
            MagazineInfo info = new MagazineInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Name = TypeParse.ObjToString(reader["name"]).Trim();
            info.Addtime = TypeParse.StrToDateTime(reader["addtime"]);
            info.Updatetime = TypeParse.StrToDateTime(reader["updatetime"]);
            info.Likeid = TypeParse.ObjToString(reader["likeid"]).Trim();
            info.Ratio = TypeParse.ObjToString(reader["ratio"]).Trim();
            info.Cover = TypeParse.ObjToString(reader["cover"]).Trim();
            info.Description = TypeParse.ObjToString(reader["description"]).Trim();
            info.Content = TypeParse.ObjToString(reader["content"]).Trim();
            info.Pages = TypeParse.StrToInt(reader["pages"], 0);
            info.Orderid = TypeParse.StrToInt(reader["orderid"], 0);
            info.Status = byte.Parse(TypeParse.ObjToString(reader["status"]));
            info.Click = TypeParse.StrToInt(reader["click"], 0);
            info.Parms = TypeParse.ObjToString(reader["parms"]).Trim();
            return info;
        }

        public static int UpdateMagazineClick(int id, bool backclick)
        {
            return DatabaseProvider.GetInstance().UpdateMagazineClick(id, backclick);
        }

        public static int GetMagazineClick(int id)
        {
            return DatabaseProvider.GetInstance().GetMagazineClick(id);
        }

        public static DataTable GetMagazineDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetMagazineDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
    }
}
