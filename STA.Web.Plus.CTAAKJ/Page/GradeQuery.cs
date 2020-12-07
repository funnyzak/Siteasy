using STA.Web.Plus.CTAA.KJ.Entity;
using STA.Common;
using STA.Core;
using STA.Page;
using System;

using STA.Web.Plus.CTAA.KJ.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace STA.Web.Plus.CTAA.KJ.Page
{
    public class GradeQuery : GradeBase
    {
        public string action = STARequest.GetString("action");
        public string vcode = STARequest.GetString("vcode");

        public String realName = STARequest.GetString("realName");
        public String num = STARequest.GetString("num");
        public String idCard = STARequest.GetString("idCard");

        public Object info;

        protected override void PageShow()
        {
            if (STARequest.IsPost() && ConUtils.IsCrossSitePost()) return;

            switch (action)
            {
                case "examinee": ExamineeQuery(); break;
                case "examiner": ExaminerQuery(); break;
                default: ExaminerPaperQuery(); break;
            }

        }

        private void ExamineeQuery()
        {
            if (Utils.StrIsNullOrEmpty(realName) || Utils.StrIsNullOrEmpty(num))
            {
                return;
            }

            JObject jsonObject = new JObject();
            jsonObject["certNo"] = num;
            jsonObject["realName"] = realName;

            String result = null;
            try
            {
                result = HttpPost.Post(ApiMethod.EXAMINEE_QUERY, jsonObject.ToString());
            }
            catch
            {
                result = null;
            }

            if (Utils.StrIsNullOrEmpty(result))
            {
                return;
            }
            resultOuput<Examinee>(result);
        }

        private void ExaminerQuery()
        {
            if (Utils.StrIsNullOrEmpty(realName) || Utils.StrIsNullOrEmpty(num) || Utils.StrIsNullOrEmpty(idCard))
            {
                return;
            }

            //if (vcode == "" || vcode.ToLower() != Utils.GetCookie(action + "_query").ToLower())
            //{
            //    AddErrLine("验证码输入有误");
            //    return;
            //}

            //Utils.ClearCookie(action + "_query");
            JObject jsonObject = new JObject();
            jsonObject["num"] = num;
            jsonObject["realName"] = realName;
            jsonObject["idCard"] = idCard;

            String result = HttpPost.Post(ApiMethod.EXAMINER_QUERY, jsonObject.ToString());
            if (Utils.StrIsNullOrEmpty(result))
            {
                AddErrLine("查询失败，请稍后重试");
                return;
            }
            resultOuput<Examiner>(result);
        }

        private void ExaminerPaperQuery()
        {
            if (Utils.StrIsNullOrEmpty(realName) || Utils.StrIsNullOrEmpty(num))
            {
                return;
            }

            JObject jsonObject = new JObject();
            jsonObject["certNum"] = num;
            jsonObject["realName"] = realName;

        
            String result = HttpPost.Post(ApiMethod.EXAMINER_PAPER_QUERY, jsonObject.ToString());
            resultOuput<ExaminerPaper>(result);
        }

        public override void resultOuput<T>(String result)
        {
            if (Utils.StrIsNullOrEmpty(result))
            {
                AddErrLine("查询失败，请稍后重试");
                return;
            }


            HttpPostResponse hpr = JsonConvert.DeserializeObject<HttpPostResponse>(result);

            try
            {
                if (!hpr.success)
                {
                    AddErrLine(hpr.errors[0]);
                    return;
                }

                info = JsonConvert.DeserializeObject<T>(hpr.data["info"].ToString());
            }
            catch (Exception ex)
            {

                LogProvider.Logger.ErrorFormat("考级查询处理出错，出错信息：{0}", ex.ToString());
            }

        }
    }
}
