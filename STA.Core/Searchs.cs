using System;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Cache;
using STA.Entity;
using System.Data;
namespace STA.Core
{
    public class Searchs
    {
        /// <summary>
        /// 获得当前页的id列表
        /// </summary>
        /// <param name="ids">全部id列表</param>
        /// <returns></returns>
        private static string GetCurrentPageIds(string ids, out int recordcount, out int pagecount, int pagesize, int pageindex)
        {
            string[] cid = Utils.SplitString(ids, ",");
            recordcount = cid.Length;
            pagecount = 0;
            if (recordcount == 0) return "";

            pagecount = (recordcount + pagesize - 1) / pagesize;

            if (pageindex > pagecount)
                pageindex = pagecount;

            int startindex = pagesize * (pageindex - 1);
            StringBuilder strids = new StringBuilder();
            for (int i = startindex; i < recordcount; i++)
            {
                if (i > startindex + pagesize)
                    break;
                else
                {
                    strids.Append(cid[i]);
                    strids.Append(",");
                }
            }
            return strids.Remove(strids.Length - 1, 1).ToString();
        }

        /// <summary>
        /// 搜索文档
        /// </summary>
        /// <param name="chlid">频道ID 0为不限</param>
        /// <param name="keywords">搜索关键字</param>
        /// <param name="day">几天内,时间0为全部</param>
        /// <param name="contype">内容类型 -1为不限</param>
        /// <param name="order">排序方式 0:时间 1:id 2:权重 3:点击 4:顶 5:踩 6:评论数 默认Id</param>
        /// <param name="ordertype">1降序 2升序</param>
        /// <param name="searchtype">1为只搜索标题  2为模糊搜索</param>
        /// <returns></returns>
        public static int Search(int chlid, string keywords, int day, int contype, int order, int ordertype, int searchtype)
        {

            return DatabaseProvider.GetInstance().Search(chlid, keywords, day, contype, order, ordertype, searchtype);
        }

        /// <summary>
        /// 获取内容缓存
        /// </summary>
        /// <param name="columns">要获取的列</param>
        /// <param name="searchid">缓存ID</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页码</param>
        /// <param name="order">排序方式 0:时间 1:id 2:权重 3:点击 4:顶 5:踩 6:评论数 默认Id</param>
        /// <param name="ordertype">1降序 2升序</param>
        /// <param name="typeid">扩展标识id</param>
        /// <param name="recordcount">记录数</param>
        /// <param name="pagecount">页数</param>
        /// <returns></returns>
        public static DataTable GetSearchCacheList(string columns, int searchid, int pagesize, int pageindex, int order, int ordertype, int typeid, out int recordcount, out int pagecount)
        {
            recordcount = 0;
            pagecount = 0;

            DataTable dt = DatabaseProvider.GetInstance().GetSearchCacheIds(searchid);

            if (dt.Rows.Count == 0)
                return new DataTable();

            string cachedidlist = dt.Rows[0][0].ToString();

            string ids = GetCurrentPageIds(cachedidlist, out recordcount, out pagecount, pagesize, pageindex);

            if (Utils.StrIsNullOrEmpty(ids))
                return new DataTable();

            string ext = "";
            if (typeid > -1)
            {
                DataRow[] drs = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + typeid.ToString());
                if (drs != null && drs.Length > 0)
                {
                    ext = drs[0]["ename"].ToString().Trim();
                }
            }

            return DatabaseProvider.GetInstance().GetSearchContentsList(pagesize, columns, ids, order, ordertype, ext);
        }
    }
}
