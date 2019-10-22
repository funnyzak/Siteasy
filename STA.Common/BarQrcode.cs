using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;

using ZXing.QrCode;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace STA.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class BarQrCode
    {
        public static Result CodeDecode(Bitmap bmap, string type)
        {
            if (type == "bar")
            {
                IBarcodeReader reader = new BarcodeReader();
                return reader.Decode(bmap);
            }
            else
            {
                LuminanceSource source = new BitmapLuminanceSource(bmap);
                BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));

                QRCodeReader reader = new QRCodeReader();
                return reader.decode(bitmap);
            }
        }

        /// <summary>
        /// 通过URL识别条形码/二维码
        /// </summary>
        /// <param name="url">如：http://www.example.com/barcode.png </param>
        /// <param name="type">bar为条形码,qr为二维码</param>
        /// <returns></returns>
        public static Result CodeDecodeFromUrl(string url, string type)
        {
            if (url.Trim() == "" || !Utils.IsImgHttp(url)) return null;
            Stream stream = Utils.GetImgStreamByUrl(url);
            if (stream == null) return null;
            return CodeDecode((Bitmap)Bitmap.FromStream(stream), type);
        }

        /// <summary>
        /// 通过文件识别条形码/二维码
        /// </summary>
        /// <param name="url">如：c:\barcode.png</param>
        /// <param name="type">bar为条形码,qr为二维码</param>
        /// <returns></returns>
        public static Result CodeDecodeFromFilename(string filename, string type)
        {
            if (filename.Trim() == "" || !FileUtil.FileExists(filename)) return null;
            return CodeDecode((Bitmap)Bitmap.FromFile(filename), type);
        }

        /// <summary>
        /// 通过form文件提交识别条形码/二维码
        /// </summary>
        /// <param name="type">bar为条形码,qr为二维码</param>
        /// <returns></returns>
        public static Result CodeDecodeFromRequest(string filekey, string type)
        {
            int fCount = HttpContext.Current.Request.Files.Count, filecount = 0;
            for (int i = 0; i < fCount; i++)
            {
                if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
                {
                    filecount++;
                }
            }
            if (filecount == 0)
                return null;

            return CodeDecode((Bitmap)Bitmap.FromStream(HttpContext.Current.Request.Files[filekey].InputStream), type);
        }

        public static string CodeBulid(string txt, int width, int height, string codeformat, ref Bitmap bitmap)
        {
            try
            {
                if (txt.Trim() == "") return "place set content!";

                width = width <= 0 ? 200 : width;
                height = height <= 0 ? 200 : height;

                EncodingOptions options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = width,
                    Margin = 0,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.Q,
                    Height = height
                };

                BarcodeWriter writer = new BarcodeWriter();
                writer.Format = (BarcodeFormat)(Enum.Parse(typeof(BarcodeFormat), codeformat));
                writer.Options = options;

                bitmap = writer.Write(txt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static bool CodeBulid(string txt, int width, int height, string codeformat, string savepath, ref string msg)
        {
            try
            {
                Bitmap bmap = null;
                msg = CodeBulid(txt, width, height, codeformat, ref bmap);

                if (msg != "") return false;

                bmap.Save(savepath);
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
    }
}
