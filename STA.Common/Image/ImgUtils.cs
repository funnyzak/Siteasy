using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace STA.Common.ImgHelper
{
    public class ImgUtils
    {
        /// <summary> 
        /// 字节转图片 
        /// </summary> 
        public static Image FromBytes(byte[] bs)
        {
            if (bs == null) return null;
            try
            {
                MemoryStream ms = new MemoryStream(bs);
                Image returnImage = Image.FromStream(ms);
                ms.Close();
                return returnImage;
            }
            catch { return null; }
        }

        /// <summary> 
        /// 将其它格式的图片转为JPG文件 
        /// </summary> 
        public static Image ToJPG(Image source)
        {
            Bitmap bmp = new Bitmap(source);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return Bitmap.FromStream(ms);
        }

        /// <summary> 
        /// 将其它格式的图片转为PNG文件 
        /// </summary> 
        public static Image ToPNG(Image source)
        {
            Bitmap bmp = new Bitmap(source);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return FromBytes(ms.ToArray());
        }
    }
}
