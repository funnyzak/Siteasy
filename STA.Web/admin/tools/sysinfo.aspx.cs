using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class sysinfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadSystemInf();
            }
        }

        protected void LoadSystemInf()
        {
            #region 检测系统信息

            Response.Expires = 0;
            Response.CacheControl = "no-cache";

            servername.Text = Server.MachineName;
            serverip.Text = Request.ServerVariables["LOCAL_ADDR"];
            server_name.Text = Request.ServerVariables["SERVER_NAME"];

            int build, major, minor, revision;
            build = Environment.Version.Build;
            major = Environment.Version.Major;
            minor = Environment.Version.Minor;
            revision = Environment.Version.Revision;
            servernet.Text = ".NET CLR  " + major + "." + minor + "." + build + "." + revision;
            serverms.Text = Environment.OSVersion.ToString();

            serversoft.Text = Request.ServerVariables["SERVER_SOFTWARE"];
            serverport.Text = Request.ServerVariables["SERVER_PORT"];
            serverout.Text = Server.ScriptTimeout.ToString();

            cl.Text = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            servertime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            serverppath.Text = Request.ServerVariables["APPL_PHYSICAL_PATH"];
            //servernpath.Text = Request.ServerVariables["PATH_TRANSLATED"];
            serverhttps.Text = Request.ServerVariables["HTTPS"];
            databasetype.Text = BaseConfigs.GetDbType;
            databasename.Text = Databases.GetDbName();

            HttpBrowserCapabilities bc = Request.Browser;
            ie.Text = bc.Browser.ToString();
            cookies.Text = bc.Cookies.ToString();
            frames.Text = bc.Frames.ToString();
            javaa.Text = bc.JavaApplets.ToString();
            javas.Text = bc.EcmaScriptVersion.ToString();
            ms.Text = bc.Platform.ToString();
            vbs.Text = bc.VBScript.ToString();
            vi.Text = bc.Version.ToString();

            cip.Text = STARequest.GetIP(); 

            #endregion
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }

        #endregion
    }
}