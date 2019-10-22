using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Plugin.Mail;

namespace STA.Web.Admin
{
    public partial class emailset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            EmailConfigInfo info = EmailConfigs.GetConfig();
            txtSmtpServer.Text = info.Smtp;
            txtSmtpPort.Text = info.Port.ToString();
            txtSysemail.Text = info.Sysemail;
            txtUserName.Text = info.Username;
            txtNickname.Text = info.Nickname;
            txtPassword.Text = info.Password;
            txtSubcont.Text = info.Subcont;
            txtSubtitle.Text = info.Subtitle;
            try
            {
                SetSmtpEmailPlugIn(HttpRuntime.BinDirectory);
            }
            catch
            {
                ddlSendWay.Items.Clear();
                try
                {
                    SetSmtpEmailPlugIn(Utils.GetMapPath("/bin/"));
                }
                catch
                {
                    ddlSendWay.Items.Add(new ListItem(".net邮件发送程序", "STA.Plugin.Mail.SysMailMessage,STA.Plugin.dll"));
                    ddlSendWay.Items.Add(new ListItem("DNT邮件发送程序", "STA.Plugin.Mail.SendMail,STA.Plugin.dll"));
                }
            }

            try
            {
                ddlSendWay.SelectedValue = info.PluginNameSpace + "," + info.DllFileName;
            }
            catch
            {
                throw new Exception("邮件配置出错！");
            }
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            EmailConfigInfo info = EmailConfigs.GetConfig();
            info.Smtp = txtSmtpServer.Text;
            info.Port = TypeParse.StrToInt(txtSmtpPort.Text, 25);
            info.Sysemail = txtSysemail.Text;
            info.Username = txtUserName.Text;
            info.Password = txtPassword.Text;
            info.Nickname = txtNickname.Text;
            info.Subtitle = txtSubtitle.Text;
            info.Subcont = txtSubcont.Text;
            info.PluginNameSpace = ddlSendWay.SelectedValue.Split(',')[0];
            info.DllFileName = ddlSendWay.SelectedValue.Split(',')[1];
            Emails.ReSetISmtpMail();
            Message(EmailConfigs.SaveConfig(info));
        }

        public void SetSmtpEmailPlugIn(string filepath)
        {
            #region 获取邮件发送插件

            DirectoryInfo dirinfo = new DirectoryInfo(filepath);
            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    if (file.Extension.ToLower().Equals(".dll"))
                    {
                        try
                        {
                            Assembly a = Assembly.LoadFrom(HttpRuntime.BinDirectory + file);

                            foreach (Module m in a.GetModules())
                            {
                                foreach (Type t in m.FindTypes(Module.FilterTypeName, "*"))  
                                {
                                    foreach (object arr in t.GetCustomAttributes(typeof(SmtpEmailAttribute), true))
                                    {
                                        SmtpEmailAttribute sea = (SmtpEmailAttribute)arr;
                                        ddlSendWay.Items.Add(new ListItem(sea.PlugInName, t.FullName + "," + sea.DllFileName));
                                    }
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            #endregion
        }

        private void btnTestmail_Click(object sender, EventArgs e)
        {
            #region 发送测试邮件
            if (txtTestmail.Text != "")
            {
                Emails.STASmtpMailToUser(txtTestmail.Text, "测试邮件", "这是一封由" + config.Webname + "发送的一封测试邮件，请勿回复！");
                Message("邮件已发送，请登录测试邮箱查收！");
            }
            else
            {
                Message("请输入测试发送EMAIL地址！");
            }
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
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
            this.btnTestmail.Click += new EventHandler(this.btnTestmail_Click);
        }
        #endregion
    }
}