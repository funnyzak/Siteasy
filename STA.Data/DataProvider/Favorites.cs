using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Favorites
    {
        #region Favorite
        /// <summary>
        /// 添加相关内容到收藏  如何返回-1则表示先前已收藏
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static int AddFavorite(FavoriteInfo info)
        {
            return DatabaseProvider.GetInstance().AddFavorite(info);
        }

        public static int EditFavorite(FavoriteInfo info)
        {
            return DatabaseProvider.GetInstance().EditFavorite(info);
        }

        public static int DelFavorites(string cids, int typeid, int uid)
        {
            if (!Utils.IsNumericList(cids))
                return -1;

            return DatabaseProvider.GetInstance().DelFavorites(cids, typeid, uid);
        }

        public static DataTable GetFavoriteDataPage(int typeid, int uid, string fields, int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetFavoriteDataPage(typeid, uid, fields, pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static bool IsAddFavorite(FavoriteInfo info)
        {
            return DatabaseProvider.GetInstance().IsAddFavorite(info) > 0 ? true : false;
        }
        #endregion
    }
}
