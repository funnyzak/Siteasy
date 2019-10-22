﻿using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using STA.Control;

namespace STA.Web.Admin
{
    public partial class specialadd : AdminPage
    {
        public string script = "\r\n<script type=\"text/javascript\">\r\n pics = [];</script>";
        List<ContypefieldInfo> list = Contents.GetContypeFieldList(0);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            //txtSavePath.Text =  "/special";
            //txtFileName.Text = Rand.Number(9);
            LoadPageControl();
            ddlConType.BuildTree(Contents.GetChannelDataTable(), "name", "id");
            tagslist.InnerText = ConUtils.DataTableToString(Tags.GetHotTags(50), 1, ",");
            rblStatus.AddTableData(ConUtils.GetEnumTable(typeof(ConStatus)), "name", "id", null);
            rblStatus.SelectedValue = "2";
            ddlConType.SelectedValue = STARequest.GetString("chlid");

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadPageControl()
        {
            extHtml.Text = "";
            foreach (ContypefieldInfo info in list)
            {
                if (Utils.InArray(info.Fieldname.Trim(), ContypefieldInfo.speclistfield.Split(','))) continue;
                extHtml.Text += ConUtils.BulidContentControlHtml(info);
            }
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            ContentInfo info = ConUtils.GetContent(id);
            if (info == null) return;
            foreach (string s in Contents.GetContype(info.Typeid).Fields.Split(','))
            {
                foreach (ContypefieldInfo i in list)
                {
                    if (i.Fieldname.Trim() == s.Trim())
                    {
                        i.Fieldvalue = TypeParse.ObjToString(info.Ext[s.Trim()]);
                        break;
                    }
                }
            }
            LoadPageControl();
            ext_grouptpl.Text = info.Ext["ext_grouptpl"].ToString();
            ddlConType.SelectedValue = info.Channelid.ToString();
            txtExtChannels.Text = info.Extchannels.ToString();
            txtTitle.Text = info.Title;
            txtSubTitle.Text = info.Subtitle;
            hidTitleColor.Value = info.Color;
            txtAuthor.Text = info.Author;
            //txtSource.Text = info.Source;
            txtImg.Text = info.Img;
            txtUrl.Text = info.Url;
            cbP_h.Checked = info.Property.IndexOf("h") >= 0;
            cbP_r.Checked = info.Property.IndexOf("r") >= 0;
            cbP_f.Checked = info.Property.IndexOf("f") >= 0;
            cbP_a.Checked = info.Property.IndexOf("a") >= 0;
            cbP_s.Checked = info.Property.IndexOf("s") >= 0;
            cbP_b.Checked = info.Property.IndexOf("b") >= 0;
            cbP_i.Checked = info.Property.IndexOf("i") >= 0;
            cbP_p.Checked = info.Property.IndexOf("p") >= 0;
            if (cbP_p.Checked)
                trImg.Style.Add("display", "");
            cbP_j.Checked = info.Property.IndexOf("j") >= 0;
            if (cbP_j.Checked)
                trImg.Style.Add("display", "");
            txtPageTitle.Text = info.Seotitle;
            txtDescription.Text = info.Seodescription;
            txtKeywords.Text = info.Seokeywords;
            txtSavePath.Text = info.Savepath;
            txtFileName.Text = info.Filename;
            txtTemplate.Text = info.Template;
            txtContent.Text = info.Content;
            txtAddtime.Text = info.Addtime.ToString("yyyy-MM-dd HH:mm:ss");
            rblStatus.SelectedValue = info.Status.ToString();
            //viewgroup
            rblIsComment.SelectedValue = info.Iscomment.ToString();
            txtClick.Text = info.Click.ToString();
            txtOrderId.Text = info.Orderid.ToString();
            txtDiggCount.Text = info.Diggcount.ToString();
            txtStampCount.Text = info.Stampcount.ToString();
            txtCredits.Text = info.Credits.ToString();
            txtTags.Text = ConUtils.DataTableToString(Contents.GetTagsByCid(id), 1, ",");
            hidAction.Value = "edit";
            hidId.Value = id.ToString();
            #endregion
        }


        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (ddlConType.SelectedValue.EndsWith("[X]"))
            {
                Message("不是有效的频道类型,带<strong>[X]</strong>的不能被选择!");
                return;
            }
            ContentInfo info = CreateInfo();
            bool success = false;
            if (hidAction.Value == "add")
            {
                info.Id = Contents.AddContent(info);
                success = info.Id > 0;
            }
            else
            {
                success = Contents.EditContent(info) > 0;
            }
            if (success)
            {
                Contents.AddTag(info.Tags, info.Id);
                Contents.EditContent(ConUtils.EditContentPath(info));
                STA.Core.Publish.StaticCreate.CreateSpecialWithGroup(info.Id);
                ConUtils.InsertLog(2, userid, username, admingroupid, admingroupname, STARequest.GetIP(), (hidAction.Value == "add" ? "添加" : "编辑") + "专题", string.Format("专题ID:{0},专题名:{1}", info.Id, info.Title));
            }
            RedirectManangePage(string.Format("<b>{0}</b> 已成功{1}！", info.Title, hidAction.Value == "add" ? "添加" : "修改"));
        }

        private ContentInfo CreateInfo()
        {
            #region 内容实体
            ContentInfo info = new ContentInfo();
            if (hidAction.Value == "edit")
                info = ConUtils.GetContent(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Channelid = TypeParse.StrToInt(ddlConType.SelectedValue, 0);
            info.Channelfamily = ConUtils.GetChannelFamily(info.Channelid, ",");
            info.Channelname = ddlConType.SelectedItem.Text;
            info.Lastedituser = userid;
            info.Updatetime = DateTime.Now;
            info.Extchannels = txtExtChannels.Text.StartsWith(",") ? txtExtChannels.Text : ("," + txtExtChannels.Text);
            info.Title = txtTitle.Text;
            info.Subtitle = txtSubTitle.Text;
            info.Color = hidTitleColor.Value;
            info.Property = ",";
            if (cbP_h.Checked)
                info.Property += "h,";
            if (cbP_r.Checked)
                info.Property += "r,";
            if (cbP_f.Checked)
                info.Property += "f,";
            if (cbP_a.Checked)
                info.Property += "a,";
            if (cbP_s.Checked)
                info.Property += "s,";
            if (cbP_b.Checked)
                info.Property += "b,";
            if (cbP_i.Checked)
                info.Property += "i,";
            if (cbP_p.Checked)
                info.Property += "p,";
            if (cbP_j.Checked)
                info.Property += "j,";
            info.Author = txtAuthor.Text;
            //info.Source = txtSource.Text;
            info.Img = txtImg.Text;
            info.Url = txtUrl.Text;
            info.Lasteditusername = username;
            info.Seotitle = txtPageTitle.Text;
            info.Seokeywords = txtKeywords.Text;
            info.Seodescription = txtDescription.Text;
            info.Savepath = txtSavePath.Text;
            info.Filename = txtFileName.Text;

            //info.Savepath = info.Savepath == "" ? config.Htmlsavepath + "/special" : info.Savepath;
            //info.Filename = info.Filename == "" ? Rand.Number(9) : info.Filename;

            info.Tags = txtTags.Text;
            info.Template = txtTemplate.Text;
            info.Content = txtContent.Text;
            if (cbIsRemote.Checked && info.Content.Trim() != string.Empty)
                info.Content = STA.Core.Collect.RemoteFile.Remote(info.Content, txtResourceFType.Text.Split(',', '，', ' '), sitepath + filesavepath + "/remote/", config.Colfilestorage == 1);
            if (cbIsFiterDangerousCode.Checked && info.Content.Trim() != string.Empty)
                info.Content = Utils.RemoveUnsafeHtml(info.Content);
            if (cbFiltertags.Checked && txtFiltertags.Text.Trim() != "")
                info.Content = ConUtils.FilterHtmlTags(info.Content, txtFiltertags.Text.Trim());
            if (cbPickImg.Checked && info.Content.Trim() != string.Empty)
            {
                string[] imgarray = Utils.GetContentImgList(info.Content);
                int imgindex = TypeParse.StrToInt(txtPicImgLocal.Text, 1);
                if (imgarray.Length > 0)
                    info.Img = (imgindex == 1 || imgindex > imgarray.Length) ? imgarray[0] : imgarray[imgindex - 1];
            }
            info.Status = byte.Parse(rblStatus.SelectedValue);
            info.Addtime = TypeParse.StrToDateTime(txtAddtime.Text);
            //viewgroup
            info.Iscomment = byte.Parse(rblIsComment.SelectedValue);
            info.Click = TypeParse.StrToInt(txtClick.Text);
            info.Orderid = TypeParse.StrToInt(txtOrderId.Text);
            info.Diggcount = TypeParse.StrToInt(txtDiggCount.Text);
            info.Stampcount = TypeParse.StrToInt(txtStampCount.Text);
            info.Credits = TypeParse.StrToInt(txtCredits.Text);
            if (hidAction.Value == "add")
            {
                info.Adduser = userid;
                info.Addusername = username;
                info.Typeid = 0;
            }
            ContypeInfo tyInfo = Contents.GetContype(TypeParse.StrToInt(info.Typeid));
            info.Typename = tyInfo.Name;
            info.Ext = new Hashtable();
            foreach (string s in tyInfo.Fields.Split(','))
            {
                info.Ext.Add(s, Utils.InArray(s, "") ? GetFieldFormString(s) : STARequest.GetString(s));
            }
            return info;
            #endregion
        }

        private string GetFieldFormString(string field)
        {
            //
            return "";
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