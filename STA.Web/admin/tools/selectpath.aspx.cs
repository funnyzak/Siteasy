using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using System.IO;
using System.Collections.Generic;

namespace STA.Web.Admin.Tools
{
    //root=&path=&fele=(返回inputid)
    public partial class selectpath : AdminPage
    {
        public string currentvirtualpath = "";
        string fieldstr = "";
        private string currentPathCookieName = "admin_pathselect_currentpath";
        private string rootPathCookieName = "admin_pathselect_rootpath";
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
            currentvirtualpath = this.CurretPath.Replace(Utils.GetMapPath("/"), "/").Replace('\\', '/');
            rptData.DataSource = FormatList(BuildData());
            rptData.DataBind();
        }

        public List<FileItem> BuildData()
        {
            string currentpath = this.CurretPath.IndexOf(this.RootPath) >= 0 ? this.CurretPath : this.RootPath;
            SetCurrentPath(currentpath);
            return FileUtil.GetItems(currentpath);
        }

        private List<FileItem> FormatList(List<FileItem> list)
        {
            if (list.Count == 0) return list;
            List<FileItem> nlist = new List<FileItem>();
            foreach (FileItem f in list)
            {
                if (f.IsFolder)
                {
                    if (f.Name == "[根目录]" || f.Name == "[上一级]")
                    {
                        if (f.Name == "[上一级]")
                        {
                            f.Name = string.Format("<span  class=\"topdir\" onclick=\"SubmitForm(0,'^{0}^')\" title=\"{1}\"><img src=\"../images/file/{2}.gif\" style=\"vertical-align:middle;\" /> {3}</span>", "uppath",
                                     currentvirtualpath, "topdir", "上级目录&nbsp;" + string.Format("(当前目录:<span>{0}</span>)", Utils.GetUnicodeSubString(currentvirtualpath, 100, "..")));
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        f.Name = string.Format("<span onclick=\"SubmitForm(0,'{0}');\" title=\"{0}\"><img src=\"../images/file/folder.gif\" style=\"vertical-align:middle;\" /> {1}</span>",
                                    f.Name, Utils.GetUnicodeSubString(f.Name, 30, ".."));

                    }
                    nlist.Add(f);
                }
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
            fieldstr = STARequest.GetFormString("hidValue").Trim();
            int action = STARequest.GetFormInt("hidAction", 100);
            switch (action)
            {
                case 0: OpenFolder(); break;
                case 1: CreateFolder(); break;
                default: break;
            }
            BindData();
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