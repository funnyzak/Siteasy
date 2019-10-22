using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin.Tools
{
    public partial class uploadfiles : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            config.Attachnameway = 0;
            string savepath = Utils.GetMapPath(Utils.UrlDecode(STARequest.GetString("path")));
            AttachmentInfo[] attach = ConUtils.SaveRequestFiles(100, "*", 1024000, savepath, "", 0, config.Waterposition, "", "files", config);
            string message = "";
            if (attach != null && attach.Length > 0)
            {
                foreach (AttachmentInfo info in attach)
                {
                    if (info != null)
                    {
                        if (info.Noupload == "")
                        {
                            info.Uid = userid;
                            info.Username = username;
                            Contents.AddAttachment(info);
                        }
                        else
                        {
                            message += "文件：" + info.Attachment;
                            message += "<br/>错误：" + info.Noupload + "<br/>";
                        }
                    }
                }
                int second = message == "" ? 2 : 0;
                message = message == "" ? "所有附件已成功上传！" : string.Format("<div style='text-align:left;'>{0}<div>", message);
                base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"" + message + "\"," + second.ToString() + ",function(){parent.SubmitForm()})");
            }
            else
            {
                base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"上传失败，您没有选择文件！\")");
            }

        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            isShowSysMenu = false;
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}