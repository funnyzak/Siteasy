using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using ZXing;
using System.Drawing;

namespace STA.Web.Admin.Tools
{
    public partial class qrcode : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ddlMake.AddTableData(ConUtils.GetEnumTable(typeof(BarcodeFormat)), "name", "name", null);
            ddlMake.SelectedValue = "QR_CODE";
            txtCodecontent.Text = string.Format("欢迎访问{0}:{1}", config.Webname, config.Weburl);
        }

        protected void BtnDecode_Click(object sender, EventArgs e)
        {
            Result result = null;

            if (rblScantype.SelectedValue == "1")
            {
                if (txtQrcode.Text.IndexOf("/") == 0)
                    result = BarQrCode.CodeDecodeFromFilename(Utils.GetMapPath(txtQrcode.Text), "qr");
                else
                    result = BarQrCode.CodeDecodeFromUrl(txtQrcode.Text, "qr");
            }
            else if (rblScantype.SelectedValue == "2")
            {
                result = BarQrCode.CodeDecodeFromRequest("fileQrcode", "qr");
            }
            else if (rblScantype.SelectedValue == "3")
            {
                if (txtBarcode.Text.IndexOf("/") == 0)
                    result = BarQrCode.CodeDecodeFromFilename(Utils.GetMapPath(txtBarcode.Text), "bar");
                else
                    result = BarQrCode.CodeDecodeFromUrl(txtBarcode.Text, "bar"); ;
            }
            else
            {
                result = BarQrCode.CodeDecodeFromRequest("fileBarcode", "bar");
            }

            trDecoderlt.Visible = false;
            txtDecoderlt.Text = "";
            if (result != null)
            {
                trDecoderlt.Visible = true;
                txtDecoderlt.Text = string.Format("原始数据：{0}\n原始字节：{1}\n条码格式：{2}\n扫描结果：{3}", result.Text, result.RawBytes != null ? BitConverter.ToString(result.RawBytes) : "无", result.BarcodeFormat.ToString(), result.ToString());
            }
            else
            {
                Message("无法识别您设置的扫描项!");
            }
        }

        protected void BtnMake_Click(object sender, EventArgs e)
        {
            trMake.Visible = false;

            int width = TypeParse.StrToInt(txtWidth.Text, 200);
            int height = TypeParse.StrToInt(txtHeight.Text, 200);
            string txt = txtCodecontent.Text;
            string format = ddlMake.SelectedValue;
            if (width <= 0 || height <= 0 || txt.Trim() == "")
            {
                Message("参数设置有误!宽高必须大于0,条码内容必须填写!", 2);
                return;
            }

            string name = "barcode_" + format + "_" + Rand.RamTime();
            string savepath = GeneralConfigs.GetConfig().Attachsavepath + "/barcode/";
            string filename = savepath + name + ".png";
            FileUtil.CreateFolder(Utils.GetMapPath(savepath));

            Bitmap bmap = null;
            string msg = BarQrCode.CodeBulid(txt, width, height, format, ref bmap);

            if (msg == "" && bmap != null)
            {
                bmap.Save(Utils.GetMapPath(filename));

                trMake.Visible = true;
                AttachmentInfo att = new AttachmentInfo();
                att.Attachment = name;
                att.Description = string.Format("条码类型:{0}，条码名称:{1}，条码内容：{2}", format, name, txt);
                att.Fileext = "png";
                att.Width = bmap.Width;
                att.Height = bmap.Height;
                att.Filename = filename;
                att.Filesize = (int)FileUtil.GetFileInfo(Utils.GetMapPath(filename)).Length;
                att.Filetype = "image/png";
                att.Uid = att.Lastedituid = userid;
                att.Username = att.Lasteditusername = username;
                int attid = Contents.AddAttachment(att);
                InsertLog(2, "生成条码", string.Format("条码类型:{0}，条码名称:{1}，条码内容：{2}", format, name, att.Description));

                imgMake.Width = width;
                imgMake.Height = height;
                imgMake.ImageUrl = filename;
                hlDown.NavigateUrl = BaseConfigs.GetSitePath + "/attachment.aspx?attid=" + attid;
                hlDown.Target = "_blank";
            }
            else
            {
                Message(msg, 3);
            }
        }
    }
}