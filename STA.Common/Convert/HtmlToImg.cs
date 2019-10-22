using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Threading;

namespace STA.Common
{
    public class ConvertHTML2
    {
        private System.Drawing.Bitmap bitmap;
        private string url = "http://www.ctaanet.com";
        private int w = 760, h = 900;//A4纸张对应的分辨率大概就是760*900
        public void setBitmap()
        {
            using (WebBrowser wb = new WebBrowser())
            {
                wb.Width = w;
                wb.Height = h;
                wb.ScrollBarsEnabled = false;
                wb.Navigate(url);
                //确保页面被解析完全
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                bitmap = new System.Drawing.Bitmap(w, h);
                wb.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, w, h));
                wb.Dispose();
            }
        }


        public void Convert()
        {
            Thread thread = new Thread(new ThreadStart(setBitmap));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            while (thread.IsAlive)
                Thread.Sleep(100);
            bitmap.Save(HttpContext.Current.Server.MapPath("t.bmp"));
        }
    }
}
