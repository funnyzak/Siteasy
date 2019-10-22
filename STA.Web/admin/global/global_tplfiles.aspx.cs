using System;
using System.Web;
using System.Data;
using System.IO;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class tplfiles : AdminPage
    {
        public string currentvirtualpath = "";
        string fieldstr = string.Empty;
        private string currentPathCookieName = "admin_tplfiles_currentpath";
        private string rootPathCookieName = "admin_tplfiles_rootpath";
        private string currentActionCookieName = "admin_tplfiles_action";
        private string clipCookieName = "admin_tplfiles_clipboard";
        public string tplpathname = STARequest.GetQueryString("path");
        #region 页面加载
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (tplpathname == "") tplpathname = "default";
            if (!IsPostBack)
            {
                SetRootPath(Utils.GetMapPath("../../templates/" + tplpathname + "/"));
                BindData();
            }
            else
                Action();
        }
        #endregion

        #region 绑定数据
        public void BindData()
        {
            FileUtil.SetRootPath(this.RootPath);
            rptData.DataSource = FormatList(BuildData());
            rptData.DataBind();
        }

        public List<FileItem> BuildData()
        {
            string currentpath = CurrentPath.IndexOf(this.RootPath) >= 0 ? this.CurrentPath : this.RootPath;
            SetCurrentPath(currentpath);
            currentvirtualpath = CurrentPath.Replace(RootPath, "/").Replace('\\', '/');
            return FileUtil.GetItems(currentpath, STARequest.GetQueryString("filetype"), STARequest.GetQueryString("query"));
        }

        private List<FileItem> FormatList(List<FileItem> list)
        {
            if (list.Count == 0) return list;
            List<FileItem> nlist = new List<FileItem>();
            foreach (FileItem f in list)
            {
                if (STARequest.GetQueryInt("listfolder", 1) != 1 && f.IsFolder) continue;
                if (f.Name == "[根目录]" || f.Name == "[上一级]")
                {
                    if (f.Name == "[上一级]")
                    {
                        f.Other = string.Format("<span class=\"topdir\" onclick=\"SubmitForm('openfolder','^{0}^')\" title=\"{1}\"><img src=\"../images/file/{2}.gif\" style=\"vertical-align:middle;\" /> {3}</span>", "uppath",
                                 currentvirtualpath, "topdir", "上级目录&nbsp;" + string.Format("(当前目录:<span>{0}</span>)", Utils.GetUnicodeSubString(currentvirtualpath, 60, "..")));
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (f.IsFolder)
                    {
                        f.Other = string.Format("<span onclick=\"SubmitForm('openfolder','{0}');\" title=\"{0}\"><img src=\"../images/file/folder.gif\" style=\"vertical-align:middle;\" /> {1}</span>",
                                 f.Name, Utils.GetUnicodeSubString(f.Name, 100, ".."));
                    }
                    else
                    {
                        string ext = Utils.GetFileExtName(f.Name).ToLower();
                        string fileurl = f.FullName.Replace(Utils.GetMapPath("/"), "/").Replace("\\", "/");
                        string fileuiname = Utils.GetUnicodeSubString(f.Name, 60, "..." + ext);
                        bool isimg = Utils.InArray(ext, "jpg,gif,bmp,png,jpeg");
                        bool istxtread = Utils.InArray(ext, "html,htm,jhtml,shtml,js,css,aspx,cs,ini,asp,aspx,ashx,asmx,asax,sitemap,txt,php,jsp,config,xml,sql");
                        string ftype = " target='_blank' ", title = "";
                        if (isimg)
                        {
                            title = " for=\"tip\" title=\"<img src='" + fileurl + "' width=150/>\"";
                            ftype = string.Format(" rel=\"img\"", fileurl);
                        }
                        else if (istxtread)
                        {
                            ftype = " ftype=\"txtread\" class=\"fancybox.iframe\" ";
                            fileurl = string.Format("../tools/readfilecontent.aspx?url={0}", Utils.UrlEncode(fileurl));
                        }
                        else if (ext == "swf")
                        {
                            ftype = " ftype = \"swf\" ";
                        }
                        ftype += string.Format(" href='{0}' ", fileurl);
                        f.Other = string.Format("<span{0}><img src=\"../images/file/{1}.gif\" onerror=\"this.src='../images/file/other.gif';\" style=\"vertical-align:middle;\" />"
                                                                                                     + " <a {2} title=\"{3}\">{4}</a></span>", title, ext, ftype, f.Name, fileuiname);
                    }
                }
                nlist.Add(f);
            }
            return nlist;
        }
        #endregion

        #region 公用属性
        private string CurrentPath
        {
            get
            {
                string currentpath = Utils.GetCookie(currentPathCookieName);
                if (currentpath == string.Empty)
                    return RootPath;
                if (!Directory.Exists(currentpath))
                    Directory.CreateDirectory(currentpath);
                return currentpath;
            }
        }

        private string RootPath
        {
            get
            {
                string rootpath = Utils.GetCookie(rootPathCookieName);
                if (rootpath == string.Empty)
                    rootpath = Utils.GetMapPath("../../templates/" + tplpathname + "/");
                if (!Directory.Exists(rootpath)) Directory.CreateDirectory(rootpath);
                return rootpath;
            }
        }

        private void SetRootPath(string value)
        {
            Utils.WriteCookie(rootPathCookieName, value);
        }

        private void SetCurrentPath(string value)
        {
            Utils.WriteCookie(currentPathCookieName, value);
        }
        #endregion

        #region  用户动作
        private void Action()
        {
            fieldstr = STARequest.GetFormString("hidValue").Trim();
            string action = STARequest.GetFormString("hidAction");
            switch (action)
            {
                case "openfolder": OpenFolder(); break;
                case "cratefolder": CreateFolder(); break;
                case "uploadfiles": Up(); break;
                case "spacesize": CheckSpaceSize(); break;
                case "zipfolder": ZipFolder(); break;
                case "unzipfile": UnZipFile(); break;
                case "delfile": DelFiles(); break;
                case "copyfile": CopyFiles(); break;
                case "cutfile": CutFiles(); break;
                case "pastefile": PasteFiles(); break;
                default: break;
            }
            BindData();
        }
        #endregion

        #region 粘贴文件
        private void PasteFiles()
        {
            string caction = Utils.GetCookie(currentActionCookieName);
            string clip = Utils.GetCookie(clipCookieName);
            if (caction == "")
            {
                Message("您还没有剪切或复制内容！");
            }
            else if (clip == "")
            {
                Message("剪贴板没有内容，请重新剪切复制！");
            }
            else
            {
                string targetpath = CurrentPath + clip.Substring(clip.LastIndexOf('\\') + 1);
                string targetfilename = targetpath.Replace(Utils.GetMapPath(sitepath + "/"), "/").Replace('\\', '/');
                string name = clip.Substring(clip.LastIndexOf('\\') + 1);
                string clipfilename = clip.Replace(Utils.GetMapPath(sitepath + "/"), "/").Replace('\\', '/');
                bool isfiles = FileUtil.FileExists(clip);
                if (caction == "copyfile")
                {
                    if (isfiles)
                    {
                        if (FileUtil.FileExists(targetpath))
                        {
                            Message(string.Format("文件 <b>{0}</b> 已存在,请选择其他目录！", name));
                            return;
                        }
                        File.Copy(clip, targetpath);
                    }
                    else
                    {
                        if (targetpath.IndexOf(clip) >= 0)
                        {
                            Message(string.Format("不能把文件夹 <b>{0}</b> 复制到它的子文件夹！", name));
                            return;
                        }
                        FileUtil.CopyFolder(clip, targetpath);
                    }
                }
                else
                {
                    if (isfiles)
                    {
                        if (FileUtil.FileExists(targetpath))
                        {
                            Message(string.Format("文件 <b>{0}</b> 已存在,请选择其他目录！", name));
                            return;
                        }
                        AttachmentInfo ainfo = Contents.GetAttachment(clipfilename);
                        if (ainfo != null)
                        {
                            ainfo.Filename = targetfilename;
                            Contents.EditAttachment(ainfo);
                        }
                        FileUtil.MoveFile(clip, targetpath);
                    }
                    else
                    {
                        if (targetpath.IndexOf(clip) >= 0)
                        {
                            Message(string.Format("不能文件夹 <b>{0}</b> 剪切到它的子文件夹！", name));
                            return;
                        }
                        ConUtils.Movefolder(clip, targetpath);
                    }
                    Utils.WriteCookie(currentActionCookieName, "");
                    Utils.WriteCookie(clipCookieName, "");
                }
                Message(string.Format("文件{0} <b>{1}</b> 已粘贴到当前目录！", isfiles ? "" : "夹", name));
            }
        }
        #endregion

        #region 复制文件
        private void CopyFiles()
        {
            Message(string.Format("文件{0} <b>{1}</b> 已复制到剪贴板！", FileUtil.FileExists(CurrentPath + fieldstr) ? "" : "夹", fieldstr));
            Utils.WriteCookie(clipCookieName, CurrentPath + fieldstr);
            Utils.WriteCookie(currentActionCookieName, "copyfile");
        }
        #endregion

        #region 剪切文件
        private void CutFiles()
        {
            if (CurrentPath == Utils.GetMapPath(BaseConfigs.GetSitePath + "/") && !IsFounder(userid))
            {
                Message("根目录的文件或文件夹只有创始人可以进行剪切操作！");
            }
            else
            {
                Message(string.Format("文件{0} <b>{1}</b> 已剪切到剪贴板！", FileUtil.FileExists(CurrentPath + fieldstr) ? "" : "夹", fieldstr));
                Utils.WriteCookie(clipCookieName, CurrentPath + fieldstr);
                Utils.WriteCookie(currentActionCookieName, "cutfile");
            }
        }
        #endregion

        #region 删除文件或文件夹
        private void DelFiles()
        {
            if (CurrentPath == Utils.GetMapPath(BaseConfigs.GetSitePath + "/") && !IsFounder(userid))
            {
                Message("根目录的文件或文件夹只有创始人可以执行删除操作！");
            }
            else
            {
                string currentfilepath = CurrentPath.Replace(Utils.GetMapPath(sitepath + "/"), "/").Replace('\\', '/');
                if (FileUtil.FileExists(CurrentPath + fieldstr))
                {
                    ConUtils.DelAttachment(currentfilepath + fieldstr);
                    Message(string.Format("文件 <b>{0}</b> 删除{1}！", fieldstr, FileUtil.DeleteFile(CurrentPath + fieldstr) ? "成功" : "失败"));
                }
                else
                {
                    List<FileItem> filelist = FileUtil.GetFiles(CurrentPath + fieldstr);
                    foreach (FileItem fitem in filelist)
                    {
                        ConUtils.DelAttachment(fitem.FullName.Replace(Utils.GetMapPath(sitepath + "/"), "/").Replace('\\', '/'));
                        FileUtil.DeleteFile(fitem.FullName);
                    }
                    Message(string.Format("文件夹 <b>{0}</b> 删除{1}！", fieldstr, FileUtil.DeleteFolder(CurrentPath + fieldstr, true) ? "成功" : "失败"));
                }
            }
        }
        #endregion

        #region 解压文件
        public void UnZipFile()
        {
            ZipFile.UnZip(ZipFile.UnzipType.ToCurrentDirctory, fieldstr.Substring(0, fieldstr.Length - 4), CurrentPath + fieldstr);
            Message("当前文件解压成功,请查看目录 <b>" + fieldstr.Substring(0, fieldstr.Length - 4) + "</b>！", 3);
        }
        #endregion

        #region 压缩文件夹
        public void ZipFolder()
        {
            ZipFile.ZipDirectory(this.CurrentPath);
            Message("当前文件夹压缩成功,请查看当前目录 <b>" + CurrentPath.Split('\\')[CurrentPath.Split('\\').Length - 2] + ".zip</b> 压缩包！", 5);
        }
        #endregion

        #region 空间大小
        private void CheckSpaceSize()
        {
            Message("当前目录占用空间：" + Utils.ConvertFileSize(FileUtil.GetDirectoryLength(CurrentPath)), 100);
        }
        #endregion

        #region 上传文件
        private void Up()
        {
            string filetype = STARequest.GetQueryString("filetype");
            filetype = filetype == "" ? "*" : filetype;
            string filename = "";
            if (STARequest.GetQueryInt("rename", 1) == 0)
                filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            AttachmentInfo[] attach = ConUtils.SaveRequestFiles(1, filetype, 1024000, this.CurrentPath, filename, 0, STARequest.GetQueryString("watermark") == "no" ? 0 : config.Waterposition, "", "upfile", config);
            if (attach != null)
            {
                if (attach[0].Noupload == "")
                {
                    if (STARequest.GetQueryInt("savedb", 1) == 1)
                    {
                        attach[0].Uid = userid;
                        attach[0].Username = username;
                        Contents.AddAttachment(attach[0]);
                    }
                    Message("文件已经成功上传到当前目录！");
                }
                else
                {
                    Message(attach[0].Noupload);
                }
            }
        }
        #endregion

        #region 打开文件夹
        private void OpenFolder()
        {
            if (fieldstr == "^rootpath^")
            {
                SetCurrentPath(this.RootPath);
            }
            else if (fieldstr == "^uppath^")
            {
                string cpath = CurrentPath.Substring(0, CurrentPath.Length - 1);
                SetCurrentPath(cpath.Substring(0, cpath.LastIndexOf('\\') + 1));
            }
            else
            {
                SetCurrentPath(this.CurrentPath + fieldstr + "\\");
            }
        }
        #endregion

        #region 创建文件夹
        private void CreateFolder()
        {
            if (FileUtil.CreateFolder((fieldstr.Trim() == string.Empty ? "新建文件夹" : fieldstr), CurrentPath))
                Message("文件夹 " + (fieldstr.Trim() == string.Empty ? "新建文件夹" : fieldstr) + " 新建完成！");
            else
                Message();
        }
        #endregion

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        #endregion
    }
}