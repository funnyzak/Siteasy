namespace STA.CacLib
{
    using STA.Common;
    using STA.Config;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class SyncCache
    {
        private static LoadBalanceConfigInfo loadBalanceConfigInfo = LoadBalanceConfigs.GetConfig();
        private static List<string> syncCacheUrlList = null;

        static SyncCache()
        {
            syncCacheUrlList = new List<string>();
            syncCacheUrlList.AddRange(loadBalanceConfigInfo.SiteUrl.Replace("sta/", "sta/cache/SyncLocalCache.ashx").Split(new char[] { ',' }));
            int port = HttpContext.Current.Request.Url.Port;
            string localUrl = string.Format("{0}://{1}{2}{3}", new object[] { HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, ((port == 80) || (port == 0)) ? "" : (":" + port), BaseConfigs.GetSitePath});
            Predicate<string> match = delegate (string webUrl) {
                return webUrl.IndexOf(localUrl) >= 0;
            };
            syncCacheUrlList.RemoveAll(match);
        }

        public static void SyncRemoteCache(string cacheKey)
        {
            foreach (string str in syncCacheUrlList)
            {
                ThreadSyncRemoteCache cache = new ThreadSyncRemoteCache(string.Format("{0}?cacheKey={1}&passKey={2}", str, cacheKey, Utils.UrlEncode(DES.Encode(cacheKey, loadBalanceConfigInfo.AuthCode))));
                new Thread(new ThreadStart(cache.Send)).Start();
            }
        }

        public class ThreadSyncRemoteCache
        {
            public string _url;

            public ThreadSyncRemoteCache(string url)
            {
                this._url = url;
            }

            public void Send()
            {
                try
                {
                    try
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (this.SendWebRequest(this._url) == "OK")
                            {
                                return;
                            }
                            Thread.Sleep(0x1388);
                        }
                    }
                    catch
                    {
                    }
                }
                finally
                {
                    if (Thread.CurrentThread.IsAlive)
                    {
                        Thread.CurrentThread.Abort();
                    }
                }
            }

            public string SendWebRequest(string url)
            {
                StringBuilder builder = new StringBuilder();
                try
                {
                    WebRequest request = WebRequest.Create(new Uri(url));
                    request.Method = "GET";
                    request.Timeout = 0x3a98;
                    request.ContentType = "Text/XML";
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            builder.Append(reader.ReadToEnd());
                        }
                    }
                }
                catch
                {
                    builder.Append("Process Failed!");
                }
                return builder.ToString();
            }
        }
    }
}

