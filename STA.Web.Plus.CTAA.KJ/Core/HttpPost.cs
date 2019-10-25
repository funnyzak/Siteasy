using System;
using System.IO;
using System.Net;
using System.Text;

using STA.Core;
using STA.Common;
using STA.Web.Plus.CTAA.KJ.Entity;
using Newtonsoft.Json;

namespace STA.Web.Plus.CTAA.KJ.Core
{
    public class HttpPost
    {
        private static readonly ConnectorConfig config;


        static HttpPost()
        {
            try
            {
                String configValue = Caches.GetVariable("ctaa_connector_config", 300);
                config = JsonConvert.DeserializeObject<ConnectorConfig>(Utils.HtmlDecode(configValue));
            }
            catch (Exception ex)
            {
                LogProvider.Logger.ErrorFormat("插件青少儿初始化信息出错，错误信息：{0}", ex.ToString());
                config = defaultConfig();
            }
        }


        public static ConnectorConfig defaultConfig()
        {
            return new ConnectorConfig()
            {
                id = "48931735830",
                secretKey = "0af89e84b77e49da9ab3894e7b7d67aa",
                apiHost = "http://192.168.11.225:9779"
            };
        }


        public static String apiUrl(ApiMethod method)
        {
            switch (method)
            {
                case ApiMethod.EXAMINEE_QUERY: return "/open/grade/examinee/query";
                case ApiMethod.EXAMINER_PAPER_QUERY: return "/open/grade/examiner/paper/query";
                case ApiMethod.EXAMINER_QUERY: return "/open/grade/examiner/query";
                case ApiMethod.EXAMINER_APPLY: return "/open/grade/examiner/apply/new";
                case ApiMethod.EXAMINER_EDIT: return "/open/grade/examiner/apply/edit";
            }
            return "";
        }


        public static string Post(ApiMethod method, string jsonContent)
        {
            string result = "";
            String ts = DateTimeUtil.GetTimeStamp(DateTime.Now);

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(config.apiHost + apiUrl(method));
                req.Method = "POST";
                req.ContentType = "application/json";
                req.Headers.Add("X-APP-KEY", config.id);
                req.Headers.Add("X-TIMESTAMP", ts);
                req.Headers.Add("X-APP-SIGN", Utils.MD5(apiUrl(method) + ts + config.secretKey).ToLower());

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(jsonContent);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogProvider.Logger.ErrorFormat("请求青少儿主服务器出错，错误信息:{0}", ex.ToString());
                return null;
            }
        }
    }
}