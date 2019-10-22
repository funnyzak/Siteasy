using System;
using System.Web;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Data;
using STA.Core;

namespace STA.Web.UI.VCode
{
    public class Mathvcode : System.Web.UI.Page
    {
        private static object lockhelper = new object();
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string cookiename = STARequest.GetString("cookiename");
            CreateValidateCode(cookiename, Color.White);
        }

        private void CreateValidateCode(string cookiename, Color bgcolor)
        {
            int mathResult = 0;
            string expression = null;
            Random rnd = new Random();
            //生成3个10以内的整数，用来运算
            int operator1 = rnd.Next(0, 10);
            int operator2 = rnd.Next(0, 10);
            int operator3 = rnd.Next(0, 10);

            switch (rnd.Next(0, 3))
            {
                case 0:
                    mathResult = operator1 + operator2 * operator3;
                    expression = string.Format("{0} + {1} * {2} = ?", operator1, operator2, operator3);
                    break;
                case 1:
                    mathResult = operator1 * operator2 + operator3;
                    expression = string.Format("{0} * {1} + {2} = ?", operator1, operator2, operator3);
                    break;
                default:
                    mathResult = operator2 + operator1 * operator3;
                    expression = string.Format("{0} + {1} * {2} = ?", operator2, operator1, operator3);
                    break;
            }
            using (Bitmap bmp = new Bitmap(150, 25))
            {
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    graph.Clear(bgcolor); 

                    //画噪线 
                    for (int i = 0; i < 3; i++)
                    {
                        int x1 = rnd.Next(150);
                        int y1 = rnd.Next(25);
                        int x2 = rnd.Next(150);
                        int y2 = rnd.Next(25);
                        graph.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                    }

                    //输出表达式
                    for (int i = 0; i < expression.Length; i++)
                    {
                        graph.DrawString(expression.Substring(i, 1),
                            new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                            new SolidBrush(Color.Black),
                            5 + i * 10,
                            rnd.Next(1, 5));
                    }
                }

                Utils.WriteCookie(cookiename, mathResult.ToString());

                //清除该页输出缓存，设置该页无缓存 
                Response.Buffer = true;
                Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.AppendHeader("Pragma", "No-Cache");

                Response.ContentType = "image/gif";
                bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                Response.End();
            }
        }

    }
}