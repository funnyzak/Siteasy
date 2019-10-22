using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using System.IO;

namespace STA.Web.Admin
{
    public partial class attachmentedit : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            isShowSysMenu = false;
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));

        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            AttachmentInfo info = Contents.GetAttachment(id);
            if (info == null) return;
            txtName.Text = info.Attachment;
            txtDescText.Text = info.Description;
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            tiphelp.HelpText = "如果要覆盖现附件，必须上传和原文件同类型的附件,否则将忽略";
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            int id = TypeParse.StrToInt(hidId.Value, 0);
            int filecount = HttpContext.Current.Request.Files.Count;
            AttachmentInfo[] attach;
            if (hidAction.Value == "edit")
            {
                AttachmentInfo info = Contents.GetAttachment(id);
                if (info != null)
                {
                    if (filecount > 0)
                    {
                        string savepath = Utils.GetMapPath(info.Filename.Substring(0, info.Filename.LastIndexOf('/') + 1));
                        string filename = info.Filename.Substring(info.Filename.LastIndexOf('/') + 1);
                        attach = ConUtils.SaveRequestFiles(1, info.Fileext.Trim(), maxfilesize, savepath, filename.Split('.')[0], 1, config.Waterposition, "", "overupload", config);
                        if (attach != null && attach[0].Noupload == "")
                        {
                            info.Filename = attach[0].Filename;
                            info.Filesize = attach[0].Filesize;
                            info.Filetype = attach[0].Filetype;
                            info.Attachment = attach[0].Attachment;
                            info.Description = attach[0].Description;
                        }
                        info.Lasteditusername = username;
                        info.Lastedituid = userid;
                        info.Lasteditime = DateTime.Now;
                    }
                    if (txtName.Text != "")
                        info.Attachment = txtName.Text;
                    if (txtDescText.Text != "")
                        info.Description = txtDescText.Text;
                    Contents.EditAttachment(info);
                    FlushData();
                }
            }
            else
            {
                attach = ConUtils.SaveRequestFiles(1, "*", maxfilesize, "", "", 1, config.Waterposition, "", "overupload", config);
                if (attach != null && attach[0].Noupload == "")
                {
                    attach[0].Lastedituid = attach[0].Uid = userid;
                    attach[0].Lasteditusername = attach[0].Username = username;
                    if (txtName.Text != "")
                        attach[0].Attachment = txtName.Text;
                    if (txtDescText.Text != "")
                        attach[0].Description = txtDescText.Text;
                    Contents.AddAttachment(attach[0]);
                    FlushData();
                }
                else
                {
                    Message("上传失败，请选择合适的文件再提交！");
                }
            }

        }

        private void FlushData()
        {
            base.RegisterStartupScript("cgscript", "window.parent.SubmitForm('flush');");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}