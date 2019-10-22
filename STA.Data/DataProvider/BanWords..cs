using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class BanWords
    {
        #region BanWords
        public static int AddWord(WordInfo info)
        {
            return DatabaseProvider.GetInstance().AddWord(info);
        }

        public static int EditWord(WordInfo info)
        {
            return DatabaseProvider.GetInstance().EditWord(info);
        }

        /// <summary>
        /// 更新过滤词
        /// </summary>
        /// <param name="find">要替换的词</param>
        /// <param name="replacement">被替换的词</param>
        public static void UpdateBadWords(string find, string replacement)
        {
            DatabaseProvider.GetInstance().UpdateBadWords(find.Trim('|'), replacement);
        }

        public static bool DelWord(int id)
        {
            return DatabaseProvider.GetInstance().DelWord(id) > 0;
        }

        public static int DelWords(string idList)
        {
            return DatabaseProvider.GetInstance().DelWords(idList);
        }

        public static WordInfo GetWord(int id)
        {
            WordInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetWord(id))
            {
                if (reader.Read())
                {
                    info = LoadWordInfo(reader);
                }
            }
            return info;
        }

        private static WordInfo LoadWordInfo(IDataReader reader)
        {
            WordInfo info = new WordInfo();
            info.Id = TypeParse.StrToInt(reader["id"], 0);
            info.Uid = TypeParse.StrToInt(reader["uid"], 0);
            info.Username = TypeParse.ObjToString(reader["username"]).Trim();
            info.Find = TypeParse.ObjToString(reader["find"]).Trim();
            info.Replacement = TypeParse.ObjToString(reader["replacement"]).Trim();
            return info;
        }

        public static DataTable GetBanWordList()
        {
            return DatabaseProvider.GetInstance().GetBanWordList();
        }

        public static DataTable GetWordDataPage(string fields, int pagecurrent, int pagesize, string condition, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetWordDataPage(fields, pagecurrent, pagesize, condition, out pagecount, out recordcount);
        }
        #endregion
    }
}
