using System;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;

using STA.Entity;
using STA.Entity.Plus;

namespace STA.Data
{
    public partial interface IDataProvider
    {
        #region content
        int AddContent(ContentInfo info);
        int DeleteContent(int id);
        int EditContent(ContentInfo info);
        int PutContentRecycle(int id);
        int ContentDigg(int id);
        int ContentStamp(int id);
        int ConCommentCount(int id);
        /// <summary>
        /// 获取内容顶踩,用半角,分隔符分开
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetDiggStamp(int id);
        object ContentTypeId(int id);
        object ContentSaveName(int id);

        /// <summary>
        /// 更新文档点击
        /// </summary>
        /// <param name="id"></param>
        /// <param name="backclick">是否返回点击量</param>
        /// <returns></returns>
        int UpdateContentClick(int id, bool backclick);
        int GetContentClick(int id);
        /// <summary>
        /// 获取扩展表字段值
        /// </summary>
        /// <param name="ext">扩展表名,空表示为基本表</param>
        /// <param name="cid">文档ID</param>
        /// <param name="fields">读取的字段</param>
        /// <returns></returns>
        string GetExtFieldValue(string ext, int cid, string field);
        /// <summary>
        /// 根据标题检测文档是否存在
        /// </summary>
        bool CheckContentRepeat(string name);
        int RecoverContent(int id);
        int ConHtmlStatus(int id, int status);
        int VerifyContent(int id, int status);
        DataTable RepeatConTitleCheck(int typeid);
        DataTable GetContentIds(int uid);
        bool EmptyRecycle();
        IDataReader GetShortContent(int id);
        IDataReader GetContent(int id, int typeid, ref string extfields);
        IDataReader GetContentForHtml(int id);
        IDataReader GetExtFieldByCid(string fields, string tbname, string cid);
        DataTable GetContentsByChannelId(int id);
        int ContentCountByChannelId(int id);
        int SpecialCount();
        int ContentCount();
        int ContentCount(int typeid);
        DataTable GetContentForSiteMap(int count);
        int EditContentsWhereChannelDel(int id);
        DataTable GetContentTableByWhere(int count, string fields, string where);

        int UpdateSoftDownloadCount(int id);
        int GetSoftDownloadCount(int id);

        DataTable GetExtConTableByIds(string extname, string fields, string ids);
        /// <summary>
        /// 获取相关内容
        /// </summary>
        DataTable GetRelateConList(int id, int count, string fields);
        #endregion

        #region channel/contype
        int AddChannel(ChannelInfo info);
        object ChannelConRule(int id);
        IDataReader GetChannelForHtml(int id);
        int ChannelCount();
        int DeleteChannel(int id);
        int EditChannel(ChannelInfo info);
        int ChannelParentId(int id);
        IDataReader GetChannel(int id);

        int AddContype(ContypeInfo info);
        int DeleteContype(int id);
        int EditContype(ContypeInfo info);
        IDataReader GetContype(int id);
        DataTable GetContypeDataTable();
        int ExistContypeField(int id, string extable, string ename);
        IDataReader GetAllChannel();
        DataTable GetChannelDataTable(string fields);
        #endregion

        //template
        string TemplateContent(int id);
        string TemplateSpecial(int id);
        string TemplateChannel(int id);
        string TemplatePage(int id);
        string TemplateSpecGroup(int id);

        int AddSpecgroup(SpecgroupInfo info);
        int DelSpecgroup(int id);
        int DelSpecgroupBySpecid(int specid);
        int EditSpecgroup(SpecgroupInfo info);
        int AddSpeccontent(SpecontentInfo info);
        int EditSpeccontent(SpecontentInfo info);
        int DelSpeccontent(int specid, int contentid);
        DataTable GetSpeconids(int specid);
        DataTable GetSpeccontentDataTable(int pageIndex, int pagesize, int specid, int groupid, string condition, out int pageCount, out int recordCount);
        DataTable GetSpecgroups(int specid);

        int AddTag(string tagName, int conId);
        int AddTag(string TagName);
        int DelTag(string tagName);
        int DelTag(int tid);
        int EditTag(int id, string tagname);
        int DelTagByCid(string tagName, int conId);
        int DelTagsByCid(int conId);
        DataTable GetTagsByCid(int conId);
        DataTable GetHotTags(int count);
        int GetMaxTagId();
        DataTable GetTagDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        int AddContypeField(ContypefieldInfo info);
        int EditContypeField(ContypefieldInfo info);
        int DeleteContypeField(int id);
        int DeleteContypeFieldByCid(int cid);
        IDataReader GetContypeField(int id);
        IDataReader GetContypeFieldList(int cid);

        object LastLoginTime(int uid);

        int AddWebCollect(WebcollectInfo info);
        int EditWebCollect(WebcollectInfo info);
        int DelWebCollect(int id);
        IDataReader GetWebCollect(int id);
        string GetWebCollectSearchCondition(string name, int channelid, string startdate, string enddate);
        DataTable GetWebCollectDataTable(int pageIndex, int pagesize, string where, out int pageCount, out int recordCount);

        int AddDbCollect(DbcollectInfo info);
        int EditDbCollect(DbcollectInfo info);
        int DelDbCollect(int id);

        /// <summary>
        /// 根据采集配置获取目标数据库内容
        /// </summary>
        DataTable DbCollectDataTableBySet(DbcollectInfo info, int count);
        IDataReader GetDbCollect(int id);
        DataTable GetDbCollectDataTable(int pageIndex, int pagesize, out int pageCount, out int recordCount);

        //操作表
        DataTable GetAllTableName();
        DataTable GetTableData(string tablename);
        bool AddTableField(string tablename, string fieldname, string fieldtype, int size);
        bool EditTableField(string tablename, string fieldname, string fieldtype, int size);
        bool DropTableField(string tablename, string fieldname);
        bool AddExtTable(string tablename);
        bool DropTable(string tablename);
        bool ReTableName(string newtablename, string oldtablename);
        int ExistTable(string tablename);
        int ExistTableField(string table, string fieldname);
        DataTable GetTableField(string tablename);
        DataTable GetFieldListTable(string tablename, string where, string fieldname);
        string ReplaceTableField(string tablename, string where, string fieldname, string source, string target);
        string UpdateTableFieldByPrimaryKey(string tablename, string fieldname, string content, string primarykeyname, string keyvalue);

        //int EditTag(TagInfo info);
        int AddConGroup(CongroupInfo info);
        int DelConGroup(int id);
        int EditConGroup(CongroupInfo info);
        IDataReader GetConGroup(int id);
        string GetConGroupSearchCondition(int type, string keywords);
        DataTable GetConGroupDataPage(int pageIndex, int pagesize, string where, out int pageCount, out int recordCount);

        int AddPage(PageInfo info);
        bool EditPage(PageInfo info);
        bool DeletePage(int id);
        object PageSaveName(int id);
        IDataReader GetPageForHtml(int id);
        IDataReader GetAlikePage(string alikeid);
        IDataReader GetPage(int id);
        DataTable GetPageLikeIdList();
        DataTable GetPageTableForSiteMap();
        DataTable GetPageDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        int DelGroupCon(int id);
        int DelGroupCon(int cid, int gid);
        int AddGroupCon(int gid, int cid, int orderid);
        int EditGroupCon(int id, int orderid);
        DataTable GetChannelGroupIds(int gid);

        DataTable GetUserListByUids(string fields, string uids);
        DataTable GetUserListByUsernames(string fields, string usernames);
        int AddUser(UserInfo info);
        int UserCount();
        int DelUser(int id);
        int EditUser(UserInfo info);
        int LockUser(int uid, int locked);
        int CheckUserEmailExist(string email);
        int CheckUserNameExist(string username);
        IDataReader GetUser(int id);
        IDataReader GetUser(string username);
        int ExistUser(string username);
        IDataReader CheckUserLogin(string username, string password, int system);
        int AddUserField(UserfieldInfo info);
        int DelUserField(int uid);
        int EditUserField(UserfieldInfo info);
        IDataReader GetUserField(int ucid);
        IDataReader GetUserFieldByUcid(int ucid);
        string GetUserSearchCondition(int gender, int status, string regstartdate, string regenddate, string actionstartdate, string actionenddate, int groupid, string username, string nickname, string email);
        DataTable GetUserDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount);
        int AddUserGroup(UserGroupInfo info);
        int EditUserGroup(UserGroupInfo info);
        int DelUserGroup(int id);
        DataTable GetUserGroupTable();
        IDataReader GetUserGroupZeroScoreId();
        IDataReader GetUserGroup(int id);
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="groupIdList"></param>
        /// <returns></returns>
        DataTable GetUserListByGroupid(int top, string fields, int start_uid, string groupIdList);

        DataTable GetUserGroupDataPage(int pagecurrent, int pagesize, int system, out int pagecount, out int recordcount);

        int AddAdminLog(AdminLogInfo info);
        int DelAdminLog(int id);
        int DelAdminlogWhere(string where);
        string GetAdminlogSearchCondition(int admintype, string startdate, string enddate, string users, string keywords);
        DataTable GetAdminLogDataPage(int pagecurrent, int pagesize, string where, out int pagecount, out int recordcount);

        int AddLink(LinkInfo info);
        int EditLink(LinkInfo info);
        int DelLink(int id);
        IDataReader GetLink(int id);
        DataTable GetLinkDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        int AddLinkType(LinktypeInfo info);
        int DelLinkType(int id);
        int EditLinkType(LinktypeInfo info);
        DataTable GetLinkType();
        IDataReader GetLinkType(int id);
        int VerifyLink(int id, int status);
        string GetLinkSearchCondition(int typeid, int status, string startdate, string enddate, string keywords);
        DataTable GetLinkTypePage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        int AddArea(AreaInfo info);
        int EditArea(AreaInfo info);
        int DelArea(int id);
        DataTable GetAreaDataTable();
        DataTable GetAreaDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        int AddAttachment(AttachmentInfo info);
        int EditAttachment(AttachmentInfo info);
        int DelAttachment(int id);
        int DelAttachment(string filename);
        IDataReader GetAttachment(int id);
        IDataReader GetAttachment(string filename);
        DataTable GetAttachTableByLikeName(string filename);
        string GetAttachSearchCondition(string startdate, string enddate, string users, string fileext, int minsize, int maxsize, string keywords);
        DataTable GetAttachmentDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);

        int AddVariables(VariableInfo info);
        int EditVariable(VariableInfo info);
        int DelVariable(int id);
        int DelVariable(string likeid);
        int DelVariableByKey(string key);
        IDataReader GetVariable(int id);
        IDataReader GetVariable(string key);
        bool ExistVariableKey(string key);
        DataTable GetVariableList(string fields, string likeid);
        DataTable GetVariableLikeidList();
        string GetVariableCondition(string name, string likeid, string key);
        DataTable GetVariableDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);

        int AddAd(AdInfo info);
        int EditAd(AdInfo info);
        int DelAd(int id);
        string AdFilename(int id);
        string AdFilename(string adname);
        IDataReader GetAd(int id);
        IDataReader GetAd(string adname);
        string GetAdSearchCondtion(string startdate, string enddate, int adtype, string keywords);
        DataTable GetAdDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);

        int AddSearchCache(SearchcacheInfo info);
        int DelSearchCache(int id);
        DataTable GetSearchCacheDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        string CommentSummary(int ctype, int cid);
        int AddComment(CommentInfo info);
        int EditComment(CommentInfo info);
        int DelComment(int id, int ctype);
        int DelCommentByUid(int uid);
        int DelCommentByCid(int cid, int ctype);
        int CommentCount();
        int CommentStatus(int id, int status);
        int CommentDigg(int id);
        int commentStamp(int id);
        IDataReader GetComment(int id);
        DataTable GetCommentDataPage(string fields, string orderby, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        string GetCommentSearchCondition(int status, int ctype, int cid, string users, string ip, string startdate, string enddate, string contitle, string keyword);

        int AddUserauth(UserauthInfo info);
        IDataReader GetUserauth(string code, AuthType atype);
        IDataReader GetUserauthByUsername(string username, AuthType atype);
        int DelUserauth(int id);

        int AddActionColumn(ActioncolumnInfo info);
        int EditActionColumn(ActioncolumnInfo info);
        int DelActionColumn(int id);
        IDataReader GetActionColumn(int id);
        int AddAction(ActionInfo info);
        int EditAction(ActionInfo info);
        int DelAction(int id);
        IDataReader GetAction(int id);
        int AddActionGroup(ActiongroupInfo info);
        int DelActionGroup(int id);
        int CheckAction(int gid, string actionname);
        DataTable GetActionListByGroupId(int gid);
        DataTable GetActionColumns();
        DataTable GetActionDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        DataTable GetDataPage(string tbname, string fieldkey, int pagecurrent, int pagesize, string fieldshow, string fieldorder, string where, out int pagecount, out int recordcount);
        DataTable GetContentDataPage(string fields, int pagecurrent, int pagesize, string where, string orderby, out int pagecount, out int recordcount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="extname">是否调用扩展,扩展标识</param>
        /// <param name="pagecurrent"></param>
        /// <param name="pagesize"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="pagecount"></param>
        /// <param name="recordcount"></param>
        /// <returns></returns>
        DataTable GetContentDataPage(string fields, string extname, int pagecurrent, int pagesize, string where, string orderby, out int pagecount, out int recordcount);
        string GetContentSearchCondition(int typeid, string addusers, int reclyle, int channelid, int status, string property, string startdate, string enddate, string keyword);
        string GetContentSearchCondition(int typeid, string addusers, int reclyle, int channelid, int self, int status, string property, string startdate, string enddate, string keyword);
        DataTable GetConGroupContentDataPage(int groupid, string columns, int pageIndex, int pageSize, out int pageCount, out int recordCount);

        string RestoreDatabase(string backUpPath, string serverName, string userName, string passWord, string strDbName, string strFileName);
        string BackUpDatabase(string backUpPath, string serverName, string userName, string password, string strDbName, string strFileName);
        string RunSql(string sql);
        void ShrinkDataBase(string shrinkSize, string dbName);
        string GetDataBaseVersion();
        string GetDbName();
        void ClearDBLog(string dbName);

        string GetPageSearchCondition(string startdate, string enddate, int minid, int maxid, string keywords);
        string GetContentPublishCondition(string startdate, string enddate, int minid, int maxid, int channelid);
        DataTable GetPublishChannelTable(string ids);
        DataTable GetPublishRssTable(string ids);
        DataTable GetPublishPageTable(string condition);
        DataTable GetPublishContentTable(string condition);
        DataTable GetPublishSpecialTable(string condition);

        int AddVotecate(VotecateInfo info);
        int DelVotecate(int id);
        int EditVotecate(VotecateInfo info);
        DataTable GetVoteCateTable();
        IDataReader GetVotecate(int id);
        DataTable GetVotecateDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        int AddVotetopic(VotetopicInfo info);
        int DelVotetopic(int id);
        int EditVotetopic(VotetopicInfo info);
        IDataReader GetVotetopic(int id);
        DataTable VoteLikeIds();
        DataTable GetVoteByLikeid(string likeid);
        DataTable GetVoteByIds(string ids);
        DataTable GetVotetopicDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        string GetVotetopicSearchCondition(string startdate, string enddate, int cateid, int type, string keywords);

        int AddVoteoption(VoteoptionInfo info);
        int DelVoteoption(int id);
        int EditVoteoption(VoteoptionInfo info);
        IDataReader GetVoteoption(int id);
        DataTable GetVoteOptionDataTable(int id, string fields, string orderby, string sort);
        DataTable GetVoteoptionDataPage(int pagecurrent, int pagesize, int topicid, out int pagecount, out int recordcount);

        int AddVoterecord(VoterecordInfo info);
        /// <summary>
        /// 检查Ip是否在特定分钟内进行了投票
        /// </summary>
        int VoteRecordIpTimeInterval(string ip, int topicid, int minute);
        int DelVoterecord(int id);
        int DelVoterecordWhere(string where);
        DataTable GetVoterecordDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        string GetVoterecordSearchCondition(string startdate, string enddate, string ip, int topicid, string idcard, string phone, string keywords);

        int AddSelect(SelectInfo info);
        int EditSelect(SelectInfo info);
        int DelSelect(int id);
        string SelectName(string ename, string value);
        int DelSelectByEname(string ename);
        IDataReader GetSelect(int id);
        IDataReader GetSelectType(int id);
        DataTable GetSelectByWhere(string condition);
        string GetSelectSearchCondition(string ename, float maxvalue, float minvalue);
        DataTable GetSelectDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);

        int AddSelectType(SelecttypeInfo info);
        int EditSelectType(SelecttypeInfo info);
        int DelSelectType(int id);
        DataTable GetSelectTypeTable();
        DataTable GetSelectTypeDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        #region 菜单管理
        int AddMenu(MenuInfo info);
        int DelMenu(int id);
        int EditMenu(MenuInfo info);
        IDataReader GetMenu(int id);
        DataTable GetMenuTable(int type, PageType pagetype);
        DataTable GetMenuTable(int type);
        int AddMenuRelation(int groupid, int menuid);
        int DelMenuRelation(int groupid, int menuid);
        int DelMenuRelation(int groupid);
        DataTable GetMenuRelatetionsByGroupId(int groupid);
        string GetMenuSearchCondition(int pagetype, int type, int system, string keyword);
        bool CheckPageAuthority(int groupid, string page);
        bool CheckPageAuthority(int groupid, int menuid);
        DataTable GetMenuDataPage(int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region 插件
        int AddPlugin(PluginInfo info);
        int DelPlugin(int id);
        int EditPlugin(PluginInfo info);
        IDataReader GetPlugin(int id);
        DataTable GetPluginTable(int setup);
        DataTable GetPluginDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);
        #endregion

        #region Shop

        #region Paylog
        int AddPaylog(PaylogInfo info);
        int EditPaylog(PaylogInfo info);
        int DelPaylog(int id);
        IDataReader GetPaylog(int id);
        string GetPaylogSearchCondition(int gtype, string oid, int pid, string usernamelist, string startdate, string enddate, string sprice, string eprice);
        DataTable GetPaylogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion


        #region Payment
        int AddPayment(PaymentInfo info);
        int EditPayment(PaymentInfo info);
        int DelPayment(int id);
        int DelPayment(string dll);
        DataTable GetPaymentTable(string fields);
        IDataReader GetPayment(int id);
        IDataReader GetPayment(string dll);
        DataTable GetPaymentDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Shopdelivery
        int AddShopdelivery(ShopdeliveryInfo info);
        int EditShopdelivery(ShopdeliveryInfo info);
        int DelShopdelivery(int id);
        DataTable GetDeliveryTable(string fields);
        IDataReader GetShopdelivery(int id);
        DataTable GetShopdeliveryDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Shopgood
        int AddShopgood(ShopgoodInfo info);
        int EditShopgood(ShopgoodInfo info);
        int DelShopgood(int id);
        DataTable GetShopgoodByOid(string oid, string fields);
        /// <summary>
        /// 查询订单下产品(商城商品 与扩展表三表联查)
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        DataTable GetShopgoodTableByOid(string oid, string fields);
        IDataReader GetShopgood(int id);
        DataTable GetShopgoodDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Shoporder
        int AddShoporder(ShoporderInfo info);
        int EditShoporder(ShoporderInfo info);
        int DelShoporder(int id);
        int ShoporderCount();
        DataTable GetBackShoporder(int backday, string fields);
        int DelShoporder(string oid);
        string GetShoporderSearchCondition(int gtype, int status, string oid, int pid, int did, string usernamelist, string startdate, string enddate, string sprice, string eprice);
        IDataReader GetShoporder(int id);
        IDataReader GetShoporder(string oid);
        IDataReader GetShoporder(string oid, int uid);
        //DataTable GetShoporderDataPageWithAddr(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        DataTable GetShoporderDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Useraddress
        int AddUseraddress(UseraddressInfo info);
        int EditUseraddress(UseraddressInfo info);
        int DelUseraddress(int id);
        DataTable GetUserAddressTableByUid(int uid);
        IDataReader GetUseraddress(int id);
        DataTable GetUseraddressDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #endregion

        #region magazine
        DataTable MagazineLikeIds();
        int UpdateMagazineClick(int id, bool backclick);
        int GetMagazineClick(int id);
        int AddMagazine(MagazineInfo info);
        int EditMagazine(MagazineInfo info);
        int DelMagazine(int id);
        IDataReader GetMagazine(int id);
        string GetMagazineSearchCondition(string startdate, string enddate, string likeid, int status, string keywords);
        DataTable GetMagazineDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        //URL静态化
        int AddUrlStaticize(StaticizeInfo info);
        int DelUrlStaticize(int id);
        int EditUrlStaticize(StaticizeInfo info);
        IDataReader GetUrlStaticize(int id);
        DataTable GetUrlStaticizeDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount);

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmNum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        int SetUserNewPMCount(int uid, int pmNum);

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="items">选项集合,半角逗号分开</param>
        /// <returns></returns>
        int Vote(int topicid, string items);

        /// <summary>
        /// 根据标签ID获取内容
        /// </summary>
        DataTable GetTagContentDataPage(int tagid, string columns, int pageIndex, int pageSize, string orderby, string where, out int pageCount, out int recordCount);

        /// <summary>
        /// 获取跳转类型的内容
        /// </summary>
        DataTable GetJumpContentList();

        /// <summary>
        /// 获取搜索缓存ID
        /// </summary>
        int Search(int chlid, string keywords, int day, int contype, int order, int ordertype, int searchtype);
        DataTable GetSearchCacheIds(int searchId);
        DataTable GetSearchContentsList(int pagesize, string columns, string strids, int order, int ordertype, string ext);

        /// <summary>
        /// 设置上次任务计划的执行时间
        /// </summary>
        /// <param name="key">任务的标识</param>
        /// <param name="serverName">主机名</param>
        /// <param name="lastExecuted">最后执行时间</param>
        void SetLastExecuteScheduledEventDateTime(string key, string serverName, DateTime lastExecuted);
        /// <summary>
        /// 获取上次任务计划的执行时间
        /// </summary>
        /// <param name="key">任务的标识</param>
        /// <param name="serverName">主机名</param>
        /// <returns></returns>
        DateTime GetLastExecuteScheduledEventDateTime(string key, string serverName);

        DataTable ExecuteTable(string connectstring, string commandText);

        bool DbConnectTest(string connectstring);
        /// <summary>
        /// 获取数据表名列表
        /// </summary>
        DataTable DbTables(string connectstring);
        string DbConnectString(string datasource, string userid, string password, string dbname);
        DataTable DbTableFields(string connectstring, string tbname);
        //前台调用方法集
        DataTable GetUIContentData(DataParmInfo info);
        DataTable GetUILinkData(DataParmInfo info);
        DataTable GetUILinkTypeData(DataParmInfo info);
        DataTable GetUIPageData(DataParmInfo info);
        DataTable GetUIChannelData(DataParmInfo info);
        DataTable GetUITagData(DataParmInfo info);
        DataTable GetUICommentData(DataParmInfo info);
        DataTable GetUIVoteData(DataParmInfo info);
        DataTable GetUIMagazineData(DataParmInfo info);
        DataTable GetDbTable(string table, int num, string where, string fields, string order);
        //plus
        DataTable GetStaVoteTable(int pageIndex, string where, out int pagecount, out int recordcount);
        int AddStaVote(STA.Entity.Plus.VoteInfo info);
        int EditStaVote(STA.Entity.Plus.VoteInfo info);
        int DelStaVote(int id);
        IDataReader GetStaVote(int id);
        bool UpdateStaVoteCount(int vid, int iid);

        #region PrivateMessage
        int AddPrivateMessage(PrivateMessageInfo info);
        int EditPrivateMessage(PrivateMessageInfo info);
        int DelPrivateMessage(int id);
        IDataReader GetPrivateMessage(int id);
        DataTable GetAnnouncePrivateMessageDataPage(string fields, int pageindex, int pagesize, out int pagecount, out int recordcount);
        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        IDataReader GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType);

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        DataTable GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType, out int pagecount, out int recordcount);

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        int GetPrivateMessageCount(int userId, int folder, int state);

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        int GetAnnouncePrivateMessageCount();

        /// <summary>
        /// 获得公共短信息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数,为-1时返回全部</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>短信息列表</returns>
        IDataReader GetAnnouncePrivateMessageList(int pageSize, int pageIndex);

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="__privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        int AddPrivateMessage(PrivateMessageInfo info, int saveToSentBox);
        /// <summary>
        /// 获取新短消息数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        int GetNewPMCount(int userId);
        /// <summary>
        /// 删除短消息
        /// </summary>
        /// <param name="isNew">是否删除新短消息</param>
        /// <param name="postDateTime">发送日期</param>
        /// <param name="msgFromList">发送者列表</param>
        /// <param name="lowerUpper">是否区分大小写</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        /// <param name="isUpdateUserNewPm">是否更新用户短消息数</param>
        /// <returns></returns>
        int DelPrivateMessages(bool isnew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm);
        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        int DelPrivateMessages(int userId, string pmIdList);

        /// <summary>
        /// 获得新短消息数
        /// </summary>
        /// <returns></returns>
        int GetNewPrivateMessageCount(int userId);

        /// <summary>
        /// 删除指定用户的一条短消息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pmid">pmid</param>
        /// <returns></returns>
        int DelPrivateMessage(int userId, int pmId);

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        int SetPrivateMessageState(int pmId, byte state);
        DataTable GetPrivateMessageDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Mailog
        int AddMailog(MailogInfo info);
        int EditMailog(MailogInfo info);
        int DelMailog(int id);
        IDataReader GetMailog(int id);
        string GetMailogSearchCondition(string title, string startdate, string enddate, string users);
        DataTable GetMailogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Submail
        int AddSubmail(MailsubInfo info);
        int EditSubmail(MailsubInfo info);
        int DelSubmail(string mail);
        DataTable GetSubmailGroups();
        DataTable GetSubMailList(string fields);
        string GetSubmailSearchCondition(int status, string name, string mail, string startdate, string enddate, string ip, string forgroup);
        IDataReader GetSubmail(string mail);
        DataTable GetSubmailDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Favorite
        int AddFavorite(FavoriteInfo info);
        int IsAddFavorite(FavoriteInfo info);
        int EditFavorite(FavoriteInfo info);
        int DelFavorites(string cids, int typeid, int uid);
        //DataTable GetFavoriteLikeidTable(int uid);
        DataTable GetFavoriteDataPage(int typeid, int uid, string fields, int pagecurrent, int pagesize, out int pagecount, out int recordcount);
        #endregion

        #region Word
        int AddWord(WordInfo info);
        int EditWord(WordInfo info);
        int DelWord(int id);
        int DelWords(string idlist);
        void UpdateBadWords(string find, string replacement);
        DataTable GetBanWordList();
        IDataReader GetWord(int id);
        DataTable GetWordDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Appconnect
        //int AddAppconnect(AppconnectInfo info);
        //int EditAppconnect(AppconnectInfo info);
        //int DelAppconnect(int id);
        //IDataReader GetAppconnect(int id);
        //IDataReader GetAppconnect(string identify);
        //DataTable GetAppconnectDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Userconnect
        int AddUserconnect(UserconnectInfo info);
        int EditUserconnect(UserconnectInfo info);
        int DelUserconnect(int id);
        //IDataReader GetUserconnect(int id);
        IDataReader GetUserconnect(int uid, string identify);
        IDataReader GetUserconnect(string openid, string identify);
        DataTable GetUserconnectDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion

        #region Userlog
        int AddUserlog(UserlogInfo info);
        int EditUserlog(UserlogInfo info);
        int DelUserlog(int id);
        string UserlogSearchCondition(string users, string startdate, string enddate, string ip, string keywords, string identify);
        IDataReader GetUserlog(int id);
        DataTable GetUserlogDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount);
        #endregion
    }
}