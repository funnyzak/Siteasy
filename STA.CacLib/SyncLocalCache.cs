namespace STA.CacLib
{
    using STA.Cache;
    using STA.Common;
    using STA.Config;
    using System;
    using System.Web;
    using System.Web.Services;

    [WebService(Namespace="http://tempuri.org/")]
    public class SyncLocalCache : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str = context.Request.QueryString["cacheKey"];
            string str2 = context.Request.QueryString["passKey"];
            if (Utils.StrIsNullOrEmpty(str))
            {
                context.Response.Write("CacheKey is not null!");
            }
            else if (!str.StartsWith("/STA"))
            {
                context.Response.Write("CacheKey is not valid!");
            }
            else if (str2 != DES.Encode(str, LoadBalanceConfigs.GetConfig().AuthCode))
            {
                context.Response.Write("AuthCode is not valid!");
            }
            else
            {
                STACache cacheService = STACache.GetCacheService();
                cacheService.LoadCacheStrategy(new DefaultCacheStrategy());
                cacheService.RemoveObject(str);
                cacheService.LoadDefaultCacheStrategy();
                context.Response.Write("OK");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

