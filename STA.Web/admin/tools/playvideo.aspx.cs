using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class playvideo : AdminPage
    {
        public AttachmentInfo info;
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (STARequest.GetInt("id", 0) > 0)
            {
                info = Contents.GetAttachment(STARequest.GetInt("id", 0));
            }
            if (info == null)
            {
                info = new AttachmentInfo();
                info.Attachment = "视频预览";
                info.Filename = STARequest.GetString("filename");
            }

        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            isShowSysMenu = false;
            base.OnInit(e);
        }
        #endregion
    }
}