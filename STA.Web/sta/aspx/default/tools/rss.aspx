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
	string defname = webname;
	
	string defurl = weburl;
	
	int count = STARequest.GetInt("count",50);
	
	int order = STARequest.GetInt("order",0);
	
	int cache = STARequest.GetInt("cache",0);
	
	int chl = STARequest.GetInt("chl",0);
	

	if (chl>0)
	{

	ChannelInfo cinfo = Contents.GetChannel(chl);
	

	if (cinfo!=null)
	{

	 defname = cinfo.Name;
	
	 defurl = Urls.Channel(chl);
	

	}	//end if


	}	//end if

	templateBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<rss version=\"2.0\">\r\n<channel>\r\n<title><![CDATA[");
	templateBuilder.Append(defname.ToString());
	templateBuilder.Append("]]></title>\r\n<link>");
	templateBuilder.Append(defurl.ToString());
	templateBuilder.Append("</link>\r\n<language>zh-cn</language>\r\n<docs><![CDATA[");
	templateBuilder.Append(defname.ToString());
	templateBuilder.Append("]]></docs>\r\n<generator>Rss Powered By ");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("</generator>\r\n<image>\r\n<url>");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png</url>\r\n</image>\r\n<description>");
	templateBuilder.Append(config.Description.ToString().Trim());
	templateBuilder.Append("</description>\r\n<ttl>5</ttl>\r\n");
	list = GetTable("content", "type=channel id=" + chl.ToString() + " num=" + count.ToString() + " fields=id,title,channelname,author,source,addtime,content order=" + order.ToString() + " cache=" + cache.ToString() + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim(),0);
	
	string author = item["author"].ToString()==String.Empty?webname:item["author"].ToString().Trim();
	
	templateBuilder.Append("\r\n<item>\r\n    <link>");
	if (config.Withweburl!=1)
	{
	templateBuilder.Append(weburl.ToString());
	}	//end if
	templateBuilder.Append(Urls.Content(cid).ToString().Trim());
	templateBuilder.Append("</link>\r\n    <title><![CDATA[" + item["title"].ToString().Trim() + "]]></title>\r\n    <description><![CDATA[" + item["content"].ToString().Trim() + "]]></description>\r\n    <author><![CDATA[");
	templateBuilder.Append(author.ToString());
	templateBuilder.Append("]]></author>\r\n    <category><![CDATA[" + item["channelname"].ToString().Trim() + "]]></category>\r\n    <pubDate>");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(item["addtime"].ToString().Trim())).ToString(" yyyy-MM-dd HH:mm:ss"));
	templateBuilder.Append("</pubDate>\r\n</item>\r\n");
	}	//end loop

	templateBuilder.Append("\r\n</channel>\r\n</rss>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
