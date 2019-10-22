using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class channelist : AdminPage
    {
        DataTable ndt = new DataTable();
        public string cinfo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delchannel":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                    case "emptychannel":
                        Empty(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData();
                ChangeTableState("");
            }
            else if (!IsPostBack)
            {
                RedirectMessage();
                ddlChltype.AddTableData(Contents.GetContypeDataTable(), "name", "id", null);
                LoadData();
                ChangeTableState("");
            }
        }

        private void Del(int id)
        {
            if (id <= 0) return;
            ConUtils.DelChannel(config, id);
            InsertLog(2, "删除频道", string.Format("频道ID:{0},名称:{1}", id, STARequest.GetString("cname" + id.ToString())));
        }

        private void Empty(int id)
        {
            if (id <= 0) return;
            ConUtils.EmptyChannel(id);
            InsertLog(2, "清空频道内容", string.Format("频道ID:{0},名称:{1}", id, STARequest.GetString("cname" + id.ToString())));
        }


        #region 绑定数据
        private void LoadData()
        {
            DataTable channels = Contents.GetChannelDataTable("id,typeid,parentid,name,ctype,img,orderid,ishidden");
            DataRow[] drs = channels.Select("parentid=0", "orderid desc");
            ndt = channels.Clone();
            ViewState["chlcount"] = channels.Rows.Count;
            foreach (DataRow r in drs)
            {
                ndt.Rows.Add(r.ItemArray);
                _SortChildChannel(r["id"].ToString(), channels);
            };
            ndt.Columns.Add("parentnode");
            ndt.Columns.Add("contentcount");
            foreach (DataRow rr in ndt.Rows)
            {
                rr["parentnode"] = rr["parentid"].ToString() == "0" ? "" : (" class=\"child-of-node-" + rr["parentid"].ToString() + "\"");
                rr["contentcount"] = Contents.ContentCountByChannelId(TypeParse.StrToInt(rr["id"]));
            }
            rptData.Visible = true;
            rptData.DataSource = ndt;
            rptData.DataBind();
        }

        private void _SortChildChannel(string id, DataTable dt)
        {
            DataRow[] drs = dt.Select("parentid=" + id, "orderid desc");
            foreach (DataRow r in drs)
            {
                ndt.Rows.Add(r.ItemArray);
                _SortChildChannel(r["id"].ToString(), dt);
            }
        }
        #endregion

        private void DelChannel_Click(object sender, EventArgs e)
        {
            #region 删除频道
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Del(id);
                }
                LoadData();
                hidTableState.Value = "expanded";
                TableState.Text = "收起";
                Message();
            }
            #endregion
        }

        void CopyBtn_Click(object sender, EventArgs e)
        {
            #region 复制频道
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    ChannelInfo cinfo = Contents.GetChannel(id);
                    if (cinfo == null) return;
                    Contents.AddChannel(cinfo);
                    InsertLog(2, "复制频道", string.Format("复制频道ID:{0},名称:{1}", cinfo.Id, cinfo.Name));
                }
                LoadData();
                hidTableState.Value = "expanded";
                TableState.Text = "收起";
                Message();
            }
            #endregion
        }

        void DelChlHtmlBtn_Click(object sender, EventArgs e)
        {
            #region 删除频道静态
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    ConUtils.DelChannelHtmlFile(config, id);
                    InsertLog(2, "删除频道静态", string.Format("频道ID:{0},名称:{1}", id, STARequest.GetString("cname" + id.ToString())));
                }
                LoadData();
                hidTableState.Value = "expanded";
                TableState.Text = "收起";
                Message();
            }
            #endregion
        }

        private void SubmitEdit_Click(object sender, EventArgs e)
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
                    ChannelInfo info = Contents.GetChannel(id);
                    info.Orderid = TypeParse.StrToInt(STARequest.GetString("txtOrderId" + d));
                    Contents.EditChannel(info);
                }
                LoadData();
                ChangeTableState("");
                Message("您提交的修改，已生效！");
            }
            #endregion
        }

        private void TableState_Click(object sender, EventArgs e)
        {
            #region 展开收起
            ChangeTableState(hidTableState.Value);
            #endregion
        }

        private void ChangeTableState(string state)
        {
            #region 展开收起
            if (state == "")
            {
                state = ConUtils.GetCookie("channeltablestate");
            }
            if (state == "collapsed")
            {
                hidTableState.Value = "expanded";
                TableState.Text = "收起";
            }
            else
            {
                hidTableState.Value = "collapsed";
                TableState.Text = "展开";
            }
            ConUtils.WriteCookie("channeltablestate", state);
            cinfo = string.Format("<a href=\"#\" onclick=\"__doPostBack('{0}','');\">{1}</a>({2}个)", "TableState", (state == "collapsed" ? "收起" : "展开"), TypeParse.StrToInt(ViewState["chlcount"]));
            #endregion
        }

        private void EmptyChannelBtn_Click(object sender, EventArgs e)
        {
            #region 清空频道内容
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Empty(id);
                }
                LoadData();
                Message();
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
            this.DelChannelBtn.Click += new EventHandler(this.DelChannel_Click);
            this.TableState.Click += new EventHandler(this.TableState_Click);
            this.CopyBtn.Click += new EventHandler(CopyBtn_Click);
            this.SubmitEdit.Click += new EventHandler(this.SubmitEdit_Click);
            this.EmptyChannelBtn.Click += new EventHandler(this.EmptyChannelBtn_Click);
            this.DelChlHtmlBtn.Click += new EventHandler(DelChlHtmlBtn_Click);
        }
        #endregion
    }
}