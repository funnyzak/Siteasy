using Newtonsoft.Json;
using STA.Common;
using STA.Core;
using STA.Page;
using STA.Web.Plus.CTAA.KJ.Core;
using STA.Web.Plus.CTAA.KJ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STA.Web.Plus.CTAA.KJ.Page
{
    public class GradeBase: PageBase
    {
        public virtual void resultOuput<T>(String result)
        {
            if (Utils.StrIsNullOrEmpty(result))
            {
                ResponseJSON(PlusUtils.Result<String>(null, -1, "失败，请稍后重试"));
                return;
            }

            HttpPostResponse hpr = JsonConvert.DeserializeObject<HttpPostResponse>(result);

            LogProvider.Logger.InfoFormat("result:{0}, entity: {1}", result, hpr.ToString());
            try
            {
                if (!hpr.success)
                {
                    ResponseJSON(PlusUtils.Result<String>(null, -1, hpr.errors[0]));
                    return;
                }

                T info = JsonConvert.DeserializeObject<T>(hpr.data["info"].ToString());
                ResponseJSON(PlusUtils.Result<T>(info));
            }
            catch (Exception ex)
            {
                LogProvider.Logger.ErrorFormat("请求考级服务器出错，信息：{0}", ex);

                ResponseJSON(PlusUtils.Result<String>(null, -1, "失败，请稍后重试"));
            }

        }
    }
}