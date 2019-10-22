using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class adadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            rblStatus.AddTableData(ConUtils.GetEnumTable(typeof(AdStatus)), "name", "id", null);
            ddlAdtype.AddTableData(ConUtils.GetEnumTable(typeof(AdType)), "name", "id", null);
            rblStatus.SelectedValue = "1";
            txtStartdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEnddate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            AdInfo info = Contents.GetAd(id);
            if (info == null) return;
            txtName.Text = info.Name;
            txtStartdate.Text = info.Startdate.ToString("yyyy-MM-dd");
            txtEnddate.Text = info.Enddate.ToString("yyyy-MM-dd");
            rblStatus.SelectedValue = ((int)info.Status).ToString();
            ddlAdtype.SelectedValue = ((int)info.Adtype).ToString();
            txtOutdate.Text = info.Outdate;
            FillPage(info);
            hidAction.Value = "edit";
            hidId.Value = id.ToString();
            #endregion
        }

        #region 填充类型数据
        private void FillPage(AdInfo info)
        {
            string[] array = Utils.SplitString(info.Paramarray, "*sta*");
            if (info.Adtype == AdType.文字)
            {
                txtTextcontent.Text = array[0];
                txtTextlink.Text = array[1];
                txtTextsize.Text = array[2];
                txtTextcolor.Text = array[3];
            }
            else if (info.Adtype == AdType.图片)
            {
                txtImgurl.Text = array[0];
                txtImglink.Text = array[1];
                txtImgalt.Text = array[2];
                txtImgwidth.Text = array[3];
                txtImgheight.Text = array[4];
            }
            else if (info.Adtype == AdType.Flash)
            {
                txtFlashurl.Text = array[0];
                txtFlashwidth.Text = array[1];
                txtFlashheight.Text = array[2];
            }
            else if (info.Adtype == AdType.代码)
            {
                txtCode.Text = info.Paramarray;
            }
        }
        #endregion

        #region 加载类型数据
        private string GetArray()
        {
            string parmsarray = "", splitor = "*sta*";
            AdType adtpe = (AdType)TypeParse.StrToInt(ddlAdtype.SelectedValue, 1);
            if (adtpe == AdType.文字)
            {
                parmsarray = txtTextcontent.Text + splitor + txtTextlink.Text + splitor + txtTextsize.Text + splitor + txtTextcolor.Text;
            }
            else if (adtpe == AdType.图片)
            {
                parmsarray = txtImgurl.Text + splitor + txtImglink.Text + splitor + txtImgalt.Text + splitor + TypeParse.StrToInt(txtImgwidth.Text, 300).ToString()
                                                                                              + splitor + TypeParse.StrToInt(txtImgheight.Text, 200).ToString();

            }
            else if (adtpe == AdType.Flash)
            {
                parmsarray = txtFlashurl.Text + splitor + TypeParse.StrToInt(txtFlashwidth.Text, 300).ToString() + splitor
                                                                    + TypeParse.StrToInt(txtFlashheight.Text, 200).ToString();
            }
            else if (adtpe == AdType.代码)
            {
                parmsarray = txtCode.Text;
            }
            return parmsarray;
        }
        #endregion

        private AdInfo CreateInfo()
        {
            AdInfo info = new AdInfo();
            if (hidAction.Value == "edit")
                info = Contents.GetAd(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Status = (AdStatus)TypeParse.StrToInt(rblStatus.SelectedValue, 1);
            info.Adtype = (AdType)TypeParse.StrToInt(ddlAdtype.SelectedValue, 1);
            info.Startdate = TypeParse.StrToDateTime(txtStartdate.Text);
            info.Enddate = TypeParse.StrToDateTime(txtEnddate.Text);
            info.Paramarray = GetArray();
            info.Outdate = txtOutdate.Text;
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            AdInfo info = CreateInfo();
            int id = ConUtils.EditAd(info);
            InsertLog(2, (info.Id == 0 ? "添加" : "修改") + "广告", string.Format("ID:{0},广告名:{1}", (info.Id == 0 ? id : info.Id).ToString(), info.Name));
            Redirect("global_adlist.aspx?msg=" + string.Format("广告 <b>{0}</b> 已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));

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