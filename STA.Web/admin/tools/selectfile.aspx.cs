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

namespace STA.Web.Admin.Tools
{
    //?path=upload,img&root=&filetype=jpg,png&listfolder=0(列出文件夹)&query=(搜索)&
    //cltmed=&fullname=1(返回的是虚拟路径还是物理路径)&watermark=no(上传不加水印)
    //&rename=0(文件上传是否重命名) 
    //&savedb=1(上传文件是否保存到数据库记录)
    //&retsux=1 返回值是否加后缀名
    //cusmethod 自定义返回的方法 weburl 返回地址是否加网址
    public partial class selectfile : AdminPage
    {
        public string currentvirtualpath = "";
        string fieldstr = string.Empty;
        private string currentPathCookieName = "admin_fileselect_currentpath";
        private string rootPathCookieName = "admin_fileselect_rootpath";
        #region 页面加载
        protected override void OnLoad(EventArgs e)
        {
            isShowSysMenu = false;
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRootPath(GetPath(STARequest.GetQueryString("root")));
                SetCurrentPath(GetPath(STARequest.GetString("path")));
                BindData();
            }
            else
            {
                Action();
            }
        }

        private string GetPath(string tpath)
        {
            string path = string.Empty;
            if (tpath == string.Empty)
                path = Utils.GetMapPath(BaseConfigs.GetSitePath + "/");
            else
                path = Utils.GetMapPath(BaseConfigs.GetSitePath + "/" + tpath.Replace(",", "/") + "/");
            if (!FileUtil.DirExists(path))
            {
                if (!FileUtil.CreateFolder(path))
                    path = Utils.GetMapPath(BaseConfigs.GetSitePath + "/");
            }
            return path;
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
            string currentpath = this.CurretPath.IndexOf(this.RootPath) >= 0 ? this.CurretPath : this.RootPath;
            SetCurrentPath(currentpath);
            currentvirtualpath = this.CurretPath.Replace(Utils.GetMapPath("/"), "/").Replace('\\', '/');
            List<FileItem> list = FileUtil.GetItems(currentpath, STARequest.GetQueryString("filetype"), STARequest.GetQueryString("query"));


            //for (int i = list.Count - 1; i >= 0; i--)
            //{
            //    if (!ConUtils.IsImgThumb(list[i].Name)) continue;
            //    list.Remove(list[i]);
            //}

            return list;
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
                        f.Name = string.Format("<span class=\"topdir\" onclick=\"OpenFolder('^{0}^')\" title=\"{1}\"><img src=\"../images/file/{2}.gif\" style=\"vertical-align:middle;\" /> {3}</span>", "uppath",
                                 currentvirtualpath, "topdir", "上级目录&nbsp;" + string.Format("(当前目录:<span>{0}</span>)", Utils.GetUnicodeSubString(currentvirtualpath, 100, "..")));
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
                        f.Name = string.Format("<span onclick=\"OpenFolder('{0}');\" title=\"{0}\"><img src=\"../images/file/folder.gif\" style=\"vertical-align:middle;\" /> {1}</span>",
                                 f.Name, Utils.GetUnicodeSubString(f.Name, 100, ".."));
                    }
                    else
                    {
                        string title = string.Empty;
                        string ext = Utils.GetFileExtName(f.Name);
                        string vpath = f.FullName.Replace(Utils.GetMapPath("/"), "/").Replace("\\", "/");
                        string clientMethod = STARequest.GetString("cltmed").Trim();
                        if (clientMethod == "")
                        {
                            clientMethod = "ReturnImg";
                        }
                        else
                        {
                            if (clientMethod == "2")
                                clientMethod = "AppendValue";
                            else if (clientMethod == "5")
                                clientMethod = "AppendValueByMethod";
                            else if (clientMethod == "3")
                                clientMethod = "XHReturnValue";
                            else
                                clientMethod = "ReturnValue";
                        }
                        string returnvalue = STARequest.GetQueryInt("fullname", 1) == 0 ? f.Name : vpath;
                        returnvalue = STARequest.GetQueryInt("retsux", 1) == 1 ? returnvalue : returnvalue.Substring(0, returnvalue.LastIndexOf("."));
                        string method = string.Format("onclick=\"{0}('{1}')\"", clientMethod, returnvalue);
                        if (Utils.InArray(ext.ToLower(), "jpg,gif,bmp,png,jpeg"))
                            title = string.Format(" ftype =\"img\" title=\"<img src='{0}' width=150/>\"", vpath);
                        else
                            title = string.Format(" title=\"{0}\"", f.Name);
                        string fileuiname = Utils.GetUnicodeSubString(f.Name, 46, "..." + ext);
                        if (File.Exists(Server.MapPath(string.Format("../images/file/{0}.gif", ext))))
                            f.Name = string.Format("<img src=\"../images/file/{0}.gif\" style=\"border:none; vertical-align:middle;\" /> <span {1} {2}>{3}</span>", ext, method, title, fileuiname);
                        else
                            f.Name = string.Format("<img src=\"../images/file/other.gif\" style=\"border:none; vertical-align:middle;\"/> <span {0} {1}>{2}</span>", method, title, fileuiname);
                    }
                }
                nlist.Add(f);
            }
            return nlist;
        }
        #endregion

        #region 公用属性
        private string CurretPath
        {
            get
            {
                if (Utils.GetCookie(currentPathCookieName) == string.Empty || !Directory.Exists(Utils.GetCookie(currentPathCookieName))) return RootPath;
                return Utils.GetCookie(currentPathCookieName);
            }
        }

        private string RootPath
        {
            get
            {
                string rootpath = Utils.GetCookie(rootPathCookieName);
                if (rootpath == string.Empty)
                    rootpath = Utils.GetMapPath(BaseConfigs.GetSitePath + "/");
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
            fieldstr = STARequest.GetFormString("fieldstr").Trim();
            int action = STARequest.GetFormInt("action", 100);
            switch (action)
            {
                case 0: OpenFolder(); break;
                case 1: CreateFolder(); break;
                case 2: Up(); break;
                default: break;
            }
            BindData();
        }
        #endregion

        #region 上传文件
        private void Up()
        {
            string filetype = STARequest.GetQueryString("filetype");
            filetype = filetype == "" ? "*" : filetype;
            string filename = "";
            if (STARequest.GetQueryInt("rename", 1) == 0)
            {
                filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
                filename = filename.Substring(0, filename.LastIndexOf('.'));
            }

            string thumbsize = STARequest.GetFormInt("iwidth", 150).ToString() + "," + STARequest.GetFormInt("iheight", 150);
            if (STARequest.GetFormString("thumbsize") == "")
                thumbsize = "";
            AttachmentInfo[] attach = ConUtils.SaveRequestFiles(1, filetype, 1024000, this.CurretPath, filename, 0, STARequest.GetFormString("watermark") == "" ? 0 : config.Waterposition, thumbsize, "upfile", config);
            if (attach != null)
            {
                if (attach[0].Noupload == "")
                {
                    if (STARequest.GetQueryInt("savedb", 1) == 1)
                    {
                        attach[0].Uid = attach[0].Lastedituid = userid;
                        attach[0].Username = attach[0].Lasteditusername = username;
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
                string cpath = CurretPath.Substring(0, CurretPath.Length - 1);
                SetCurrentPath(cpath.Substring(0, cpath.LastIndexOf('\\') + 1));
            }
            else
            {
                SetCurrentPath(this.CurretPath + fieldstr + "\\");
            }
        }
        #endregion

        #region 创建文件夹
        private void CreateFolder()
        {
            if (FileUtil.CreateFolder((fieldstr.Trim() == string.Empty ? "新建文件夹" : fieldstr), CurretPath))
                Message("文件夹 " + (fieldstr.Trim() == string.Empty ? "新建文件夹" : fieldstr) + " 新建完成！");
            else
                Message();
        }
        #endregion
    }
}