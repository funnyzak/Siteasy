using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Data;
using STA.Core;
using System.IO;

namespace STA.Web.Admin.Tools
{
    public partial class readfilecontent : AdminPage
    {
        private int maxreadsize = 5;
        string filename = Utils.GetMapPath(Utils.UrlDecode(STARequest.GetQueryString("url")));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    if (!FileUtil.FileExists(filename))
                    {
                        txtContent.InnerText = "加载失败，文件不存在！";
                    }
                    else
                    {
                        FileInfo info = FileUtil.GetFileInfo(filename);
                        if (info.Length > maxreadsize * 1024 * 1024)
                            txtContent.InnerText = "加载失败，文件大小超出了最大读取限制！";
                        else
                            txtContent.InnerText = FileUtil.ReadFile(filename);
                    }
                }
                catch
                {
                    txtContent.InnerText = "加载失败";
                }
            }
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (FileUtil.WriteFile(filename, txtContent.InnerText))
            {
                AttachmentInfo info = Contents.GetAttachment(Utils.UrlDecode(STARequest.GetQueryString("url")));
                if (info != null)
                {
                    info.Filesize = TypeParse.StrToInt(FileUtil.GetFileInfo(filename).Length);
                    info.Lasteditusername = username;
                    info.Lastedituid = userid;
                    info.Lasteditime = DateTime.Now;
                    Contents.EditAttachment(info);
                }
                Message("文件修改成功！");
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
            //this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}