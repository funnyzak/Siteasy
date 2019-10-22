using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Common
{
    /// <summary>
    /// 时间工具类
    /// </summary>
    public class DateTimeUtil
    {
        private static DateTime _dtStart = new DateTime(1970, 1, 1, 8, 0, 0);

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
