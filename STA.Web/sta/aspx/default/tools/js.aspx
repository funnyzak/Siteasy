<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.PageBase" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="STA.Common" %>
<%@ Import namespace="STA.Data" %>
<%@ Import namespace="STA.Core" %>
<%@ Import namespace="STA.Entity" %>
<%@ Import namespace="STA.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:14.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:14. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;


	DataTable list;
	DataTable pht;

	templateBuilder.Capacity = 220000;

	String url = STARequest.GetString("url");
	String encode = STARequest.GetString("encode");
	encode = encode==""?"utf-8":encode;
	String ret = "";
	if(url.Trim() != "")
	{
	   if(url.StartsWith("/"))
	   {
	      url = siteurl + url;
	   }
	   ret = Utils.HtmlToJs(Utils.GetPageContent(new Uri(url),encode));
	}
	
	templateBuilder.Append("\r\n");
	templateBuilder.Append(ret.ToString());
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
