using System;
using System.Data;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class createnewfile : AdminPage
    {
        string filename = Utils.UrlDecode(STARequest.GetString("path"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (FileUtil.FileExists(Utils.GetMapPath(filename)))
            {
                txtName.Text = Path.GetFileName(Utils.GetMapPath(filename));
                txtContent.Text = FileUtil.ReadFile(Utils.GetMapPath(filename));
            }
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            name = txtName.Text == "" ? "新建文件.html" : (name.IndexOf('.') < 0 ? (name + ".html") : name);
            string content = txtContent.Text;
            string filenamepath = Utils.GetMapPath(filename);
            string newfilename = "";
            AttachmentInfo info = null;

            string filenamevpath = filenamepath.Replace(Utils.GetMapPath("/"), "/").Replace("\\", "/");
            if (FileUtil.FileExists(filenamepath))
            {
                newfilename = filenamepath.Replace(filenamepath.Substring(filenamepath.LastIndexOf('\\') + 1), name);
                if (newfilename != filenamepath && FileUtil.FileExists(newfilename))
                {
                    Message("该文件已经存在，请使用其他文件名！");
                }
                else
                {
                    FileUtil.WriteFile(filenamepath, txtContent.Text);
                    info = Contents.GetAttachment(filenamevpath);
                    if (info != null)
                    {
                        info.Filename = filenamevpath.Replace(filenamevpath.Substring(filenamevpath.LastIndexOf('/') + 1), name);
                        info.Filesize = TypeParse.StrToInt(FileUtil.GetFileInfo(filenamepath).Length);
                        info.Fileext = Utils.GetFileExtName(name);
                        info.Lasteditusername = username;
                        info.Lastedituid = userid;
                        info.Lasteditime = DateTime.Now;
                        Contents.EditAttachment(info);
                    }
                    if (newfilename != filenamepath)
                        FileUtil.MoveFile(filenamepath, newfilename);
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"文件修改成功！\", 2, function(){parent.SubmitForm()})");
                }
            }
            else
            {
                newfilename = filenamepath + name;
                if (FileUtil.FileExists(newfilename))
                {
                    Message("该文件已经存在，请使用其他文件名！");
                }
                else
                {
                    FileUtil.WriteFile(newfilename, txtContent.Text);
                    info = new AttachmentInfo();
                    FileInfo finfo = FileUtil.GetFileInfo(newfilename);
                    info.Attachment = name.Substring(0, name.LastIndexOf('.'));
                    info.Filesize = TypeParse.StrToInt(finfo.Length);
                    info.Fileext = info.Description = finfo.Extension.Substring(1);
                    info.Filename = filenamevpath + name;
                    info.Lastedituid = info.Uid = userid;
                    info.Lasteditusername = info.Username = username;
                    Contents.AddAttachment(info);
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"文件 " + name + " 新建成功！\", 2, function(){parent.SubmitForm()})");
                }
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