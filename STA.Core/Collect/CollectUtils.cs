using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Data;
using STA.Cache;
using STA.Config;

namespace STA.Core.Collect
{
    public class CollectUtils
    {
        public static string TitFormat(GeneralConfigInfo config, string title)
        {
            //同义词替换
            if (config.Coltitrplopen == 1)
                title = Utils.ReplaceWordList(title, Utils.SplitString(config.Coltitreplace, "\n"));
            //插入关键词
            if (config.Coltititpos > 0)
                title = Utils.InsertWordList(title, config.Coltititpos, Utils.SplitString(config.Coltitkeywords, "|"), 1);
            return title;
        }

        public static string ConFormat(GeneralConfigInfo config, string content)
        {
            //内链添加
            if (config.Colautolink == 1)
                content = Utils.AddConLinkList(content, Utils.SplitString(config.Colseolinks, "\n"));
            //插入关键词
            if (config.Colseorate > 0)
                content = Utils.InsertWordList(content, 3, Utils.SplitString(config.Colseocontent, "\n"), config.Colseorate);
            return content;
        }

        public static int ConClick(GeneralConfigInfo config)
        {
            string clickrange = config.Colclickrange;
            if (clickrange == "")
                return 0;
            if (Utils.IsInt(clickrange))
                return TypeParse.StrToInt(clickrange, 0);
            else
            {
                string[] crs = clickrange.Split(',');
                System.Random random = new Random(unchecked((int)DateTime.Now.Ticks));
                return random.Next(TypeParse.StrToInt(crs[0], 0), TypeParse.StrToInt(crs[1], 100));
            }
        }

    }
}
