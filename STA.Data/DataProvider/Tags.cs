using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Tags
    {
        public static int AddTag(string tagName, int conId)
        {
            return DatabaseProvider.GetInstance().AddTag(tagName, conId);
        }

        public static int DelTag(string tagName)
        {
            return DatabaseProvider.GetInstance().DelTag(tagName);
        }

        public static int DelTagByCid(string tagName, int conId)
        {
            return DatabaseProvider.GetInstance().DelTagByCid(tagName, conId);
        }

        public static DataTable GetHotTags(int count)
        {
            return DatabaseProvider.GetInstance().GetHotTags(count);
        }

        public static int DelTagsByCid(int conId)
        {
            return DatabaseProvider.GetInstance().DelTagsByCid(conId);
        }

        public static DataTable GetTagsByCid(int conId)
        {
            return DatabaseProvider.GetInstance().GetTagsByCid(conId);
        }

        public static int GetMaxTagId()
        {
            return DatabaseProvider.GetInstance().GetMaxTagId();
        }
    }
}
