using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Cache;
using STA.Core;
using STA.Data;
using System.Text.RegularExpressions;
namespace STA.Web.Admin
{
    public partial class contypeadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) >= 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            ContypeInfo info = Contents.GetContype(id);
            if (info == null) return;
            rblOpen.SelectedValue = info.Open.ToString();
            rblSystem.SelectedValue = info.System.ToString();
            if (info.System == 1)
            {
                rblSystem.Enabled = false;
                txtEname.Enabled = false;
            }
            txtName.Text = info.Name;
            txtEname.Text = info.Ename;
            //txtEname.Enabled = false;
            //txtExtable.Text = info.Extable;
            txtBgaddmod.Text = info.Bgaddmod;
            txtBgeditmod.Text = info.Bgeditmod;
            txtBglistmod.Text = info.Bglistmod;
            txtAddmod.Text = info.Addmod;
            txtEditmod.Text = info.Editmod;
            txtListmod.Text = info.Listmod;
            txtOrderid.Text = info.Orderid.ToString();

            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }

        private ContypeInfo CreateInfo()
        {
            ContypeInfo info = new ContypeInfo();
            if (hidAction.Value == "edit")
            {
                ContypeInfo tinfo = Contents.GetContype(TypeParse.StrToInt(hidId.Value, 0));
                if (tinfo != null)
                {
                    info = tinfo;
                    if (info.System != 1)
                        info.System = byte.Parse(rblSystem.SelectedValue);
                }
            }
            info.Open = Byte.Parse(rblOpen.SelectedValue);
            info.Ename = txtEname.Text;
            if (hidAction.Value == "add")
            {
                info.System = byte.Parse(rblSystem.SelectedValue);
            }
            info.Name = txtName.Text;

            info.Bgaddmod = txtBgaddmod.Text;
            info.Bgeditmod = txtBgeditmod.Text;
            info.Bglistmod = txtBglistmod.Text;
            info.Addmod = txtAddmod.Text;
            info.Editmod = txtEditmod.Text;
            info.Listmod = txtListmod.Text;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            ContypeInfo info = CreateInfo();
            if (hidAction.Value == "add" && (Utils.InArray(info.Ename, "page,channel,search,comment,vote") || !Regex.IsMatch(info.Ename, @"^[a-zA-Z\d_]{7,20}$")))
            {
                Message("标识设置有误，请查看帮助说明！", 2);
                return;
            }
            string exttable = BaseConfigs.GetTablePrefix + "ext" + info.Ename + "s";
            info.Extable = exttable;
            if (Contents.ExistContypeField(info.Id, info.Extable, info.Ename))
            {
                Message("模型标识或模型附加表已经存在！");
                return;
            }
            bool success = false;
            int id = 0;
            if (hidAction.Value == "add" && Databases.AddExtTable(exttable))
            {
                id = Contents.AddContype(info);
                success = id > 0;
            }
            else if (hidAction.Value == "edit")
            {
                ContypeInfo oldcyinfo = Contents.GetContype(TypeParse.StrToInt(hidId.Value, 0));
                string oldtable = oldcyinfo.Extable;

                bool cok = true;
                if (oldcyinfo.System != 1)
                {
                    cok = false;
                    if (!(Databases.ExistTable(oldtable) > 0))
                        cok = Databases.AddExtTable(exttable);
                    else
                        cok = Databases.ReTableName(exttable, oldtable);
                }
                if (cok)
                    success = Contents.EditContype(info) > 0;
            }
            Caches.RemoveStringCache();
            Caches.RemoveObject(CacheKeys.DATATABLE + "contypelist");
            SiteUrls.SetInstance();
            if (success)
            {
                InsertLog(2, (info.Id == 0 ? "添加" : "修改") + "频道模型", string.Format("ID:{0},模型名:{1}", (info.Id == 0 ? id : info.Id).ToString(), info.Name));
                Redirect("global_contypes.aspx?msg=" + string.Format("频道模型 <b>{0}</b> 已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));
            }
            else
                Message("保存失败，请检查参数格式是否有误,或者可能附加表数据库已经存在！", 2);
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