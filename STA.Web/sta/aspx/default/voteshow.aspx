<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.VoteShow" %>
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
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:15.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:15. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;
	int citem__loop__id=0;
	int oitem__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"投票,");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n<body>          \r\n<div class=\"vt_wrapper\">\r\n	");
	templateBuilder.Append("	<div class=\"header\">\r\n    	<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"  title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" target=\"_self\"><img title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a>\r\n    </div>");


	item__loop__id=0;
	foreach(DataRow item in votelist.Rows)
	{
		item__loop__id++;

	int tid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	DataTable options = VoteOptionList(tid,"name,count,orderid","count","desc");
	

	if (options.Rows.Count>0)
	{

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    ");
	if (votelist.Rows.Count>1)
	{

	templateBuilder.Append("\r\n        ");
	templateBuilder.Append(item__loop__id.ToString());
	templateBuilder.Append("、" + item["name"].ToString().Trim() + "\r\n    ");
	}
	else
	{

	templateBuilder.Append("\r\n    	" + item["name"].ToString().Trim() + "\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n    </div>\r\n    ");
	    int maxcount = TypeParse.StrToInt(options.Rows[0]["count"]);
	    
	templateBuilder.Append("\r\n    <div class=\"votecon\">\r\n        ");
	oitem__loop__id=0;
	foreach(DataRow oitem in options.Rows)
	{
		oitem__loop__id++;

	string prec = "";
	
	int count = TypeParse.StrToInt(oitem["count"].ToString().Trim());
	
	int wid = VoteBarWid(TypeParse.StrToInt(item["votecount"].ToString().Trim()),maxcount,count,470,out prec);
	
	templateBuilder.Append("\r\n    	<div class=\"voteitem clearfix\">\r\n        	<div class=\"tit\">");
	templateBuilder.Append(oitem__loop__id.ToString());
	templateBuilder.Append("、" + oitem["name"].ToString().Trim() + "</div>\r\n            <div class=\"precent\">");
	templateBuilder.Append(prec.ToString());
	templateBuilder.Append("%</div>\r\n            ");
	            string[] colors = new string[] {"6fb82c","4dbfd6","e04564","7064ab","8c559f","f8dd45","5d5d5d","4b89dd" };
	            string color = colors[(oitem__loop__id-1)%8];
	            
	templateBuilder.Append("\r\n            <div class=\"bar\" style=\"width:0;background:#");
	templateBuilder.Append(color.ToString());
	templateBuilder.Append(";\" wid=\"");
	templateBuilder.Append(wid.ToString());
	templateBuilder.Append("\"></div>\r\n            <div class=\"count\">" + oitem["count"].ToString().Trim() + "票</div>\r\n        </div>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"voteinfo\">总票数：" + item["votecount"].ToString().Trim() + "</div>\r\n    ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n    <script type=\"text/javascript\">\r\n        $(\".bar\").each(function (idx, obj) {\r\n            $(obj).animate({ width: $(obj).attr(\"wid\") }, 1500);\r\n        });\r\n    </");
	templateBuilder.Append("script>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
