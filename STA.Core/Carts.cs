using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Entity;

namespace STA.Core
{
    public partial class Carts
    {
        public static string GetCookieString(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0) return "";
            string rstr = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                rstr += string.Format("{0}={1}={2}&", dr["id"], dr["num"], dr["ext_vipprice"]);
            }
            return rstr == "" ? rstr : rstr.Substring(0, rstr.Length - 1);
        }

        public static void WriteCookie(DataTable dt)
        {
            HttpCookie cookie = new HttpCookie("cartgoods");
            cookie.Value = GetCookieString(dt);
            cookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static DataTable GetCartProdsListByCookies()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", Type.GetType("System.Int32"));
            dt.Columns.Add("num", Type.GetType("System.Int32"));
            dt.Columns.Add("price", Type.GetType("System.Decimal"));

            string cookies = GetCookie();

            if (cookies == "") return dt;

            foreach (string s in cookies.Split('&'))
            {
                string[] ms = s.Split('=');
                if (ms.Length < 3) continue;

                DataRow dr = dt.NewRow();
                dr["id"] = TypeParse.StrToInt(ms[0]);
                dr["num"] = TypeParse.StrToInt(ms[1]);
                dr["price"] = TypeParse.StrToDecimal(ms[2]);

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable GetCartProdList(DataTable cookiedt)
        {
            DataTable list = new DataTable();
            if (cookiedt == null || cookiedt.Rows.Count <= 0) return list;

            if (cookiedt != null && cookiedt.Rows.Count > 0)
            {
                string ids = Utils.DataTableToString(cookiedt, "id", ":", ",");
                if (ids.Length >= 1)
                {
                    list = Contents.GetExtConTableByIds("product", "id,title,img,ext_price,ext_vipprice,ext_unit,ext_weight,ext_storage", ids);

                    list.Columns.Add("num", Type.GetType("System.String"));
                    foreach (DataRow dr in list.Rows)
                    {
                        int cnum = TypeParse.StrToInt(cookiedt.Select("id=" + dr["id"].ToString())[0]["num"]);
                        dr["num"] = cnum > TypeParse.StrToInt(dr["ext_storage"]) ? TypeParse.StrToInt(dr["ext_storage"]) : cnum;
                    }
                }
            }
            return list;
        }

        public static string GetCookie()
        {
            return HttpContext.Current.Request.Cookies["cartgoods"] != null ? Utils.UrlDecode(TypeParse.ObjToString(HttpContext.Current.Request.Cookies["cartgoods"].Value)) : "";

        }

        public static void EmptyCart()
        {
            HttpCookie cookie = new HttpCookie("cartgoods");
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}
