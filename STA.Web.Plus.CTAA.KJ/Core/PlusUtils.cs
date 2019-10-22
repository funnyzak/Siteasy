using System;
using System.Text;
using System.Data;
using STA.Common;

using STA.Web.Plus.CTAA.KJ.Entity;
using STA.Core;
using Newtonsoft.Json.Linq;

namespace STA.Web.Plus.CTAA.KJ.Core
{
    public partial class PlusUtils
    {
        private static DateTime _dtStart = new DateTime(1970, 1, 1, 8, 0, 0);

        public static String responseData(String msg = "", Boolean success = false)
        {
            JObject jObject = new JObject();
            jObject["code"] = success ? 10000 : -1;
            jObject["msg"] = msg;
            return jObject.ToString();
        }

        /// <summary> 
        /// 获取时间戳 
        /// </summary>  
        public static string GetTimeStamp(DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.Subtract(_dtStart).TotalMilliseconds).ToString();
        }

        /// <summary> 
        /// 根据时间戳获取时间 
        /// </summary>  
        public static DateTime TimeStampToDateTime(string timeStamp)
        {
            return _dtStart.AddMilliseconds(Convert.ToInt64(timeStamp));
        }

        /// <summary> 
        /// 根据时间戳获取时间 
        /// </summary>  
        public static DateTime TimeStampToDateTime(long timeStamp)
        {
            if (timeStamp > 0)
            {
                return _dtStart.AddMilliseconds(timeStamp);
            }
            return DateTime.MinValue;
        }

    }
}