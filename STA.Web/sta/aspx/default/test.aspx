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
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:16.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:16. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;
	int citem__loop__id=0;
	int oitem__loop__id=0;
	int ty__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;

	templateBuilder.Append("<div class=\"footer\">\r\n    <a href=\"#\" target=\"_blank\">关于我们</a> |  \r\n    <a href=\"#\" target=\"_blank\">联系方式</a> |  \r\n    <a href=\"#\" target=\"_blank\">版权声明</a> |  \r\n    <a href=\"#\" target=\"_blank\">招聘信息</a> |  \r\n    <a href=\"#\" target=\"_blank\">友情链接</a>\r\n    <p class=\"cp\">Powered by <strong><a href=\"http://www.stacms.com\" target=\"_blank\">STACMS</a></strong> <em>v1.0</em> &copy; 2013 <img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/copyright.gif\"/>\r\n    </p>\r\n</div>\r\n</body>\r\n</html>\r\n");


	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
