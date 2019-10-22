using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Config;
using STA.Core;
using System.Drawing.Text;
using System.Drawing;

namespace STA.Web.Admin
{
    public partial class waterset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            rblWatertype.SelectedValue = info.Watertype.ToString();
            hidPosition.Value = info.Waterposition.ToString();
            txtWateropacity.Text = info.Wateropacity.ToString();
            txtWaterlimitsize.Text = info.Waterlimitsize;
            txtWaterquality.Text = info.Waterquality.ToString();
            txtWaterimg.Text = info.Waterimg;
            LoadSystemFont();
            ddlWaterfontname.SelectedValue = info.Waterfontname;
            txtWaterfontsize.Text = info.Waterfontsize.ToString();
            txtWatertext.Text = info.Watertext;
        }

        private void LoadSystemFont()
        {
            #region 加载系统字体
            ddlWaterfontname.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
                ddlWaterfontname.Items.Add(new ListItem(family.Name, family.Name));
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            hidPosition.Value = STARequest.GetFormString("position");
            if (hidPosition.Value != "0")
            {
                if (rblWatertype.SelectedValue == "1" && (!Utils.IsImgFilename(txtWaterimg.Text) || Utils.IsImgHttp(txtWaterimg.Text)))
                {
                    Message("图片水印方式必须选择一张本地的水印图片");
                    return;
                }
                if (txtWaterlimitsize.Text != "0" && !Regex.IsMatch(txtWaterlimitsize.Text, @"^\d{1,6}\*\d{1,6}$"))
                {
                    Message("水印图片限制尺寸设置不正确");
                    return;
                }

            }
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Waterquality = TypeParse.StrToInt(txtWaterquality.Text, 80);
            info.Watertype = TypeParse.StrToInt(rblWatertype.SelectedValue, 0);
            info.Waterposition = STARequest.GetFormInt("position", 9);
            info.Wateropacity = TypeParse.StrToInt(txtWateropacity.Text, 60);
            if (info.Wateropacity < 0 || info.Wateropacity > 100) info.Wateropacity = 100;
            if (info.Waterquality < 0 || info.Waterquality > 100) info.Waterquality = 80;
            info.Waterlimitsize = txtWaterlimitsize.Text;
            info.Waterimg = txtWaterimg.Text;
            info.Waterfontname = ddlWaterfontname.SelectedValue;
            info.Waterfontsize = TypeParse.StrToInt(txtWaterfontsize.Text, 13);
            info.Watertext = txtWatertext.Text;
            Message(GeneralConfigs.SaveConfig(info));
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