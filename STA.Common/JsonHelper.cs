using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace STA.Common
{

    public class JsonParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="limit_list">每个用半角逗号隔开</param>
        /// <returns></returns>
        public static string JsonSerializerWithLimit(object obj, string limit_list)
        {
            try
            {
                var settings = new JsonSerializerSettings();
                settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                settings.ContractResolver = new LimitPropsContractResolver(limit_list.Split(','));
                //settings.ContractResolver = new DynamicContractResolver('f');


                settings.DefaultValueHandling = DefaultValueHandling.Include;
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                settings.MaxDepth = 3;
                //settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

                return JsonConvert.SerializeObject(obj, Formatting.None, settings);
            }
            catch
            {
                return null;
            }
        }


        public static string JsonSerializer(object data)
        {
            try
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                return JsonConvert.SerializeObject(data, Formatting.None, timeFormat);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString, bool parseDate = false)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
        }
    }

    public class JsonHelper
    {
        public static string JsonSerializer<T>(T data, bool parseDate = false)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, data);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                if (parseDate)
                {
                    string pattern = @"\\/Date\(([-]?)(\d+)\+\d+\)\\/";
                    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                    Regex reg = new Regex(pattern);
                    jsonString = reg.Replace(jsonString, matchEvaluator);
                }
                return jsonString;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString, bool parseDate = false)
        {
            try
            {
                if (parseDate)
                {
                    string pattern = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
                    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
                    Regex reg = new Regex(pattern);
                    jsonString = reg.Replace(jsonString, matchEvaluator);
                }
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(m.Groups[1].Value != "" ? -long.Parse(m.Groups[2].Value) : long.Parse(m.Groups[2].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

    }
}
