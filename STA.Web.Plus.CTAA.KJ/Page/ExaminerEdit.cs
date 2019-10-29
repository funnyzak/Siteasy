using STA.Common;
using STA.Core;
using STA.Page;
using System;

using STA.Web.Plus.CTAA.KJ.Core;
using STA.Web.Plus.CTAA.KJ.Entity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;

namespace STA.Web.Plus.CTAA.KJ.Page
{
    public class ExaminerEdit : GradeBase
    {
        public string action = STARequest.GetString("action");

        protected override void PageShow()
        {
            if (ConUtils.IsCrossSitePost())
            {
                HttpContext.Current.Response.StatusCode = 404;
                return;
            }

            Examiner examiner = requestForm();

            #region 验证表单
            String errorMsg = "";
            if (String.IsNullOrEmpty(examiner.gender) ||
                String.IsNullOrEmpty(examiner.photo) ||
                String.IsNullOrEmpty(examiner.realName) ||
                String.IsNullOrEmpty(examiner.phone) ||
                String.IsNullOrEmpty(examiner.idCard) ||
                String.IsNullOrEmpty(examiner.resume) ||
                String.IsNullOrEmpty(examiner.major) ||
                String.IsNullOrEmpty(examiner.graduateSchool) ||
                String.IsNullOrEmpty(examiner.workTitle) ||
                String.IsNullOrEmpty(examiner.workUnit) ||
                String.IsNullOrEmpty(examiner.applyMajor) ||
                String.IsNullOrEmpty(examiner.workOrganization) ||
                String.IsNullOrEmpty(examiner.majorResume) ||
                String.IsNullOrEmpty(examiner.workResume)
                )
            {
                errorMsg = "数据填写不完整";
            }
            else if (!Utils.IsImgHttp(examiner.photo))
            {
                errorMsg = "请上传照片";
            }
            else if (examiner.birthday <= 0 || examiner.graduateDate <= 0)
            {
                errorMsg = "日期有误";
            }

            if (errorMsg.Length > 0)
            {
                ResponseJSON(Result<String>(null, -1, errorMsg));
                return;
            }
            #endregion

            String result = HttpPost.Post(action == "edit" ? ApiMethod.EXAMINER_EDIT : ApiMethod.EXAMINER_APPLY, JsonConvert.SerializeObject(examiner));

            resultOuput<Examiner>(result);
        }


        private Examiner requestForm()
        {
            return new Examiner()
            {
                num = Utils.RemoveUnsafeStr(STARequest.GetFormString("num")),
                branchId = STARequest.GetFormInt("branchId", 0),
                gender = Utils.RemoveUnsafeStr(STARequest.GetFormString("gender")),
                photo = Utils.RemoveUnsafeStr(STARequest.GetFormString("photo")),
                realName = Utils.RemoveUnsafeStr(STARequest.GetFormString("realName")),
                birthday = STARequest.GetFormInt("birthday", 0),
                graduateDate = STARequest.GetFormInt("graduateDate", 0),
                workDate = STARequest.GetFormInt("workDate", 0),
                artLearnYear = STARequest.GetFormInt("artLearnYear", 0),
                artWorkYear = STARequest.GetFormInt("artWorkYear", 0),
                phone = Utils.RemoveUnsafeStr(STARequest.GetFormString("phone")),
                idCard = Utils.RemoveUnsafeStr(STARequest.GetFormString("idCard")),
                resume = Utils.RemoveUnsafeStr(STARequest.GetFormString("resume")),
                major = Utils.RemoveUnsafeStr(STARequest.GetFormString("major")),
                graduateSchool = Utils.RemoveUnsafeStr(STARequest.GetFormString("graduateSchool")),
                workTitle = Utils.RemoveUnsafeStr(STARequest.GetFormString("workTitle")),
                workUnit = Utils.RemoveUnsafeStr(STARequest.GetFormString("workUnit")),
                applyMajor = Utils.RemoveUnsafeStr(STARequest.GetFormString("applyMajor")),
                workOrganization = Utils.RemoveUnsafeStr(STARequest.GetFormString("workOrganization")),
                majorResume = Utils.RemoveUnsafeStr(STARequest.GetFormString("majorResume")),
                workResume = Utils.RemoveUnsafeStr(STARequest.GetFormString("workResume"))
            };
        }
    }
}