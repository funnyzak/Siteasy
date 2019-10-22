<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.User.Action" %>
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





	templateBuilder.Capacity = 220000;

	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
