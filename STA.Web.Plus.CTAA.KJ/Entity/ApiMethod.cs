using System;
using System.Collections.Generic;
using System.Linq;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    public enum ApiMethod
    {
        /// <summary>
        /// 考官证书查询
        /// </summary>
        EXAMINER_PAPER_QUERY = 0,
        /// <summary>
        /// 考生信息查询
        /// </summary>
        EXAMINEE_QUERY,
        /// <summary>
        /// 考官申请信息查询
        /// </summary>
        EXAMINER_QUERY,
        /// <summary>
        /// 考官申请增加
        /// </summary>
        EXAMINER_APPLY,
        /// <summary>
        /// 考官编辑
        /// </summary>
        EXAMINER_EDIT,
        /// <summary>
        /// 图片下载
        /// </summary>
        IMAGE_DOWNLOAD
    }
}