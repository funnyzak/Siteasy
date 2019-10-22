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
	templateBuilder.Append("<document>\r\n    <webMaster>");
	templateBuilder.Append(config.Adminmail.ToString().Trim());
	templateBuilder.Append("</webMaster>\r\n    <webSite>");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("/</webSite>\r\n    <updatePeri>" + STARequest.GetString("peri") + "</updatePeri>\r\n    ");	int count = STARequest.GetInt("count",100);
	
	int order = STARequest.GetInt("order",0);
	
	int cache = STARequest.GetInt("cache",0);
	
	int chl = STARequest.GetInt("chl",0);
	

	list = GetTable("content", "type=channel id=" + chl.ToString() + " num=" + count.ToString() + " fields=id,title,typeid,savepath,filename,img,seokeywords,seodescription,author,source,addtime,content order=" + order.ToString() + " cache=" + cache.ToString() + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim(),0);
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string html = Utils.CompressHtml(Utils.RemoveHtml(item["content"].ToString())).Replace("&", "&amp;");
	
	string keywords = item["seokeywords"].ToString()==String.Empty? seokeywords:item["seokeywords"].ToString();
	
	string description = item["seodescription"].ToString()==String.Empty? seodescription:item["seodescription"].ToString();
	
	string img = item["img"].ToString().Trim().ToLower();
	

	if (img!=String.Empty&&!img.StartsWith("http"))
	{

	 img = weburl+img;
	

	}	//end if

	string author = item["author"].ToString()==String.Empty?webname:item["author"].ToString().Trim();
	
	string source = item["source"].ToString()==String.Empty?webname:item["source"].ToString();
	
	templateBuilder.Append("\r\n    <item>\r\n        <link>");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("</link>\r\n        <title>" + item["title"].ToString().Trim() + "</title>\r\n        <description>");
	templateBuilder.Append(description.ToString());
	templateBuilder.Append("</description>\r\n        <text>");
	templateBuilder.Append(html.ToString());
	templateBuilder.Append("</text>\r\n        <image>");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("</image>\r\n        <keywords>");
	templateBuilder.Append(keywords.ToString());
	templateBuilder.Append("</keywords>\r\n        <author>");
	templateBuilder.Append(author.ToString());
	templateBuilder.Append("</author>\r\n        <source>");
	templateBuilder.Append(source.ToString());
	templateBuilder.Append("</source>\r\n        <pubDate>");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(item["addtime"].ToString().Trim())).ToString(" yyyy-MM-dd HH:mm:ss"));
	templateBuilder.Append("</pubDate>\r\n    </item>\r\n    ");
	}	//end loop

	templateBuilder.Append("\r\n</document>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
