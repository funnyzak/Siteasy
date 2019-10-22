using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Cache;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class contypefieldedit : AdminPage
    {
        public ContypeInfo cinfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            cinfo = Contents.GetContype(STARequest.GetInt("id", 0));
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delfield":
                        Databases.DropTableField(cinfo.Extable, STARequest.GetFormString("cfieldname" + STARequest.GetFormString("hidValue")).Trim());
                        ConUtils.BulidContypeFields(STARequest.GetInt("id", 0));
                        Message(Contents.DeleteContypeField(STARequest.GetFormInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
            }
            LoadData();
        }

        private void LoadData()
        {
            List<ContypefieldInfo> list = Contents.GetContypeFieldList(STARequest.GetInt("id", 0));
            rptData.Visible = true;
            rptData.DataSource = list;
            rptData.DataBind();
        }

        private void RebulidFieldBtn_Click(object sender, EventArgs e)
        {
            List<ContypefieldInfo> list = Contents.GetContypeFieldList(STARequest.GetInt("id", 0));
            ContypeInfo info = Contents.GetContype(STARequest.GetInt("id", 0));
            foreach (ContypefieldInfo i in list)
            {
                Databases.AddTableField(info.Extable, i.Fieldname.Trim(), i.Fieldtype.Trim(), i.Length);
            }
            ConUtils.BulidContypeFields(STARequest.GetInt("id", 0));
            Caches.RemoveObject(CacheKeys.DATATABLE + "contypelist");
            Message("所有字段重建完毕！");
        }

        private void BulidBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Databases.AddTableField(cinfo.Extable, STARequest.GetString("cfieldname" + d).Trim(), STARequest.GetString("cfieldtype" + d).Trim(), STARequest.GetInt("csize" + d, 20));
                }
                ConUtils.BulidContypeFields(STARequest.GetInt("id", 0));
                Caches.RemoveObject(CacheKeys.DATATABLE + "contypelist");
                Message("所选字段重建完毕！");
            }
        }

        private void DelFieldBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Databases.DropTableField(cinfo.Extable, STARequest.GetString("cfieldname" + d).Trim());
                    Contents.DeleteContypeField(id);
                }
                ConUtils.BulidContypeFields(STARequest.GetInt("id", 0));
                Caches.RemoveObject(CacheKeys.DATATABLE + "contypelist");
                LoadData();
            }
        }

        private void SubmitEditBtn_Click(object sender, EventArgs e)
        {
            #region 编辑排序
            if (STARequest.GetString("hidid") != "")
            {
                string ids = STARequest.GetString("hidid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    ContypefieldInfo info = Contents.GetContypeField(id);
                    info.Orderid = TypeParse.StrToInt(STARequest.GetString("txtOrderId" + d));
                    Contents.EditContypeField(info);
                }
                LoadData();
                Message("您提交的修改，已生效！");
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
            this.RebulidFieldBtn.Click += new EventHandler(this.RebulidFieldBtn_Click);
            this.BulidBtn.Click += new EventHandler(this.BulidBtn_Click);
            this.DelFieldBtn.Click += new EventHandler(this.DelFieldBtn_Click);
            this.SubmitEditBtn.Click += new EventHandler(this.SubmitEditBtn_Click);
        }
        #endregion
    }
}