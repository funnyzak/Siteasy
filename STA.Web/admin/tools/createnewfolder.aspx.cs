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
    public partial class createnewfolder : AdminPage
    {
        string dirname = Utils.UrlDecode(STARequest.GetString("path"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            txtName.Text = dirname.Substring(dirname.LastIndexOf('/') + 1);
            txtName.Text = txtName.Text == "" ? "新建文件夹" : txtName.Text;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            string currentname = dirname.Substring(dirname.LastIndexOf('/') + 1);
            if (txtName.Text != "" && Regex.IsMatch(txtName.Text, @"^\w+$"))
            {
                if (currentname == "")
                {
                    string message = FileUtil.CreateFolder(txtName.Text, Utils.GetMapPath(Utils.UrlDecode(STARequest.GetString("path")))) ? "成功" : "失败,请检查文件夹名称是否有误";
                    message = string.Format("文件夹 {0} 创建{1}！", txtName.Text, message);
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"" + message + "\", 2, function(){parent.SubmitForm()})");
                }
                else
                {
                    string parentdir = dirname.Substring(0, dirname.LastIndexOf('/') + 1);
                    if (!FileUtil.DirExists(Utils.GetMapPath(parentdir + txtName.Text)))
                    {
                        ConUtils.Movefolder(Utils.GetMapPath(dirname), Utils.GetMapPath(parentdir + txtName.Text));
                        base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"已成功将文件夹改为 " + txtName.Text + "！\", 2, function(){parent.SubmitForm()})");
                    }
                    else
                    {
                        base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"您修改的文件夹已经存在，已取消修改！\")");
                    }
                }
            }
            else
            {
                base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"文件夹名非法或未填写，已取消操作！\")");
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