using System;
using System.Text;
using System.Web;

namespace STA.Common
{
   public static class Alert
   {

       #region 提示信息并返回原页面
       public static void show(string text)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>alert('" + text + "');window.history.back();</script>");
           HttpContext.Current.Response.End();
       }
       #endregion

       #region 不提示信息只返回原页面
       public static void Up()
       {
           HttpContext.Current.Response.Write("<script language='javascript'>window.history.go(-1);</script>");
       }
       #endregion

       #region 提示信息并重新加载原页面
       public static void reload(string text)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>alert('" + text + "');window.location.reload();</script>");
           HttpContext.Current.Response.End();
       }
       #endregion

       #region 只提示信息
       public static void showOnly(string text)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>alert('" + text + "');</script>");
       }
       #endregion

       #region 提示信息并跳转到指定页面
       public static void showAndGo(string text, string goUrl)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>alert('" + text + "');location.href='" + goUrl + "';</script>");
       }
       #endregion

       #region 不提示信息只跳转到指定页面
       public static void GoHref(string goUrl)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>location.href='" + goUrl + "';</script>");
           HttpContext.Current.Response.End();
       }
       #endregion

       #region 不提示信息只跳转到父页面
       public static void GoParent(string goUrl)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>parent.location.href='" + goUrl + "';</script>");
           HttpContext.Current.Response.End();
       }
       #endregion

       #region 只显示部分字符串
       public static string SubStr(string sString, int nLeng)
       {
           if (sString.Length <= nLeng)
           {
               return sString;
           }
           string sNewStr = sString.Substring(0, nLeng);
           sNewStr = sNewStr + "...";
           return sNewStr;
       }
       public static void RedirectUrl(string url)
       {
           HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location.href='" + url + "';</script>");
       }
       #endregion

       #region 向页面注册JavaScript，使页面弹出提示框时页面背景不会为空白
       /// <summary>
       /// 显示消息提示对话框
       /// </summary>
       /// <param name="page">当前页面指针，一般为this</param>
       /// <param name="msg">提示信息</param>
       public static void Show(System.Web.UI.Page page, string msg)
       {
           page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
       }

       public static void ShowAndBack(System.Web.UI.Page page, string msg)
       {
           page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');window.history.back();</script>");
 
       }
       /// <summary>
       /// 控件点击 消息确认提示框
       /// </summary>
       /// <param name="page">当前页面指针，一般为this</param>
       /// <param name="msg">提示信息</param>
       public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
       {
           Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
       }

       /// <summary>
       /// 显示消息提示对话框，并进行页面跳转
       /// </summary>
       /// <param name="page">当前页面指针，一般为this</param>
       /// <param name="msg">提示信息</param>
       /// <param name="url">跳转的目标URL</param>
       public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
       {
           StringBuilder Builder = new StringBuilder();
           Builder.Append("<script language='javascript' defer>");
           Builder.AppendFormat("alert('{0}');", msg);
           Builder.AppendFormat("top.location.href='{0}'", url);
           Builder.Append("</script>");
           page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

       }

       /// <summary>
       /// 输出自定义脚本信息
       /// </summary>
       /// <param name="page">当前页面指针，一般为this</param>
       /// <param name="script">输出脚本</param>
       public static void ResponseScript(System.Web.UI.Page page, string script)
       {
           page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
       }
       #endregion
    
   }
}
