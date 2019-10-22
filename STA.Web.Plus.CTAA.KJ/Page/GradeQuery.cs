﻿using STA.Web.Plus.CTAA.KJ.Entity;
using STA.Common;
using STA.Core;
using STA.Page;
using System;

using STA.Web.Plus.CTAA.KJ.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace STA.Web.Plus.CTAA.KJ.Page
{
    public class GradeQuery : PageBase
    {
        public string action = STARequest.GetString("action");
        public string vcode = STARequest.GetString("vcode");

        public String realName = STARequest.GetString("realName");
        public String num = STARequest.GetString("num");
        public String idCard = STARequest.GetString("idCard");

        public Object info;

        protected override void PageShow()
        {
            if (ConUtils.IsCrossSitePost() || !STARequest.IsPost()) return;

            if (vcode == "" ||
                vcode.ToLower() != Utils.GetCookie(action + "_query").ToLower())
            {
                AddErrLine("验证码输入有误");
                return;
            }

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
                AddErrLine("查询信息填写不完整");
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
                AddErrLine("查询失败，请稍后重试");
                return;
            }
            resultOuput<Examinee>(result);
        }

        private void ExaminerQuery()
        {
            if (Utils.StrIsNullOrEmpty(realName) || Utils.StrIsNullOrEmpty(num) || Utils.StrIsNullOrEmpty(idCard))
            {
                AddErrLine("查询信息填写不完整");
                return;
            }

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
                AddErrLine("查询信息填写不完整");
                return;
            }

            JObject jsonObject = new JObject();
            jsonObject["certNum"] = num;
            jsonObject["realName"] = realName;


            String result = HttpPost.Post(ApiMethod.EXAMINER_PAPER_QUERY, jsonObject.ToString());
            resultOuput<ExaminerPaper>(result);
        }

        public void resultOuput<T>(String result)
        {
            if (Utils.StrIsNullOrEmpty(result))
            {
                AddErrLine("查询失败，请稍后重试");
                return;
            }

            JObject resultObject = JObject.Parse(result);
            if (!TypeParse.StrToBool(resultObject["success"], false))
            {
                AddErrLine(resultObject["errors]"][0].ToString());
                return;
            }

            info = JsonConvert.DeserializeObject<T>(resultObject["data"]["info"].ToString());
        }
    }
}