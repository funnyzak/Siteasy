<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Content" %>
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


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/base.css\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/special.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.tools.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/myfocus/myfocus.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/global.js\"></");
	templateBuilder.Append("script>");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n</head>\r\n<body class=\"wrapper\">\r\n	<div class=\"topheader\">\r\n    	<div class=\"left w230\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/logo2.jpg\" height=\"27\" width=\"131\"/></a></div>\r\n        <div class=\"right w730 tright\">\r\n             <div class=\"right\" style=\"padding:5px 0 0 10px;\"><input type=\"text\" class=\"ipt-query query\" value=\"键入关键字搜索..\" name=\"query\" id=\"query\" />\r\n             <input type=\"button\" class=\"btn-search gosearch\" name=\"gosearch\" id=\"gosearch\" value=\"\"/></div>\r\n        	 <div class=\"right\" style=\"padding:7px 0 0 0;\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/plus/advancedsearch.aspx\" class=\"cdarkgrey\" target=\"_blank\">高级搜索</a> - <a href=\"");
	templateBuilder.Append(Urls.Sitemap().ToString().Trim());
	templateBuilder.Append("\" class=\"cdarkgrey\" target=\"_blank\">网站地图</a> - <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/plus/advisory.aspx\" class=\"cdarkgrey\" target=\"_blank\">留言建议</a> - <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/rss.html\" class=\"cdarkgrey\" target=\"_blank\">RSS订阅</a> - <a href=\"javascript:addFavourite('");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("', '");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("');\" class=\"cdarkgrey\" target=\"_self\">加入收藏</a></div>\r\n             <script type=\"text/javascript\">searchEvent();</");
	templateBuilder.Append("script>\r\n        </div>\r\n    </div>\r\n    ");	DataTable gps = SpecConGroup();
	
	templateBuilder.Append("\r\n    <div class=\"header\">\r\n    	<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\"><img src=\"" + info.Ext["ext_banner"].ToString().Trim() + "\" width=\"960\"/></a>\r\n    <div class=\"menu cwhite\">\r\n    	<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cwhite\">网站首页</a>\r\n        ");
	item__loop__id=0;
	foreach(DataRow item in gps.Rows)
	{
		item__loop__id++;

	int gid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n        | <a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" onclick=\"$('.group" + item["id"].ToString().Trim() + "').ScrollTo(200);\" class=\"cwhite\" target=\"_blank\">" + item["name"].ToString().Trim() + "</a>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"cont\">\r\n    	<div class=\"w700 left\">\r\n           <div id=\"topslide\" style=\"visibility:hidden\">\r\n              <div class=\"loading\"></div>\r\n              <div class=\"pic\">\r\n                  <ul> \r\n                    ");	string ssql = "select title,img,thumb,text,url from plus_slideimgs where likeid = '专题:" + info.Title + "' order by orderid desc";
	

	list = GetSqlTable("" + ssql.ToString() +"");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                        <li><a href=\"" + item["url"].ToString().Trim() + "\" target=\"_blank\"><img alt=\"" + item["title"].ToString().Trim() + "\" text=\"" + item["text"].ToString().Trim() + "\" width=\"560\" height=\"300\"  src=\"" + item["img"].ToString().Trim() + "\"/></a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("      \r\n                  </ul>\r\n              </div>\r\n            </div>\r\n        </div>\r\n        ");
	if (list.Rows.Count>0)
	{

	templateBuilder.Append("\r\n        <script type=\"text/javascript\">\r\n            $('#topslide').myFocus({ pattern: 'mF_pithy_tb', width: 560, auto: true, height: 300, trigger: \"mouseover\" });\r\n		</");
	templateBuilder.Append("script>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n        <div class=\"w260 right\">\r\n        	<div class=\"bar260\"><b class=\"s20 fyahei cwhite\">导语</b></div>\r\n            <div class=\"fyahei cgrey lh22 con260\">\r\n                    ");
	templateBuilder.Append(info.Content.ToString().Trim());
	templateBuilder.Append("\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"cont\" style=\"padding-top:0;\">\r\n        ");
	im__loop__id=0;
	foreach(DataRow im in gps.Rows)
	{
		im__loop__id++;

	int gid = TypeParse.StrToInt(im["id"].ToString().Trim());
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n    	<div class=\"pht960\">\r\n        	<div class=\"bar\"><a class=\"s18 fyahei cwhite bold group" + im["id"].ToString().Trim() + "\" target=\"_blank\" href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\">" + im["name"].ToString().Trim() + "</a></div>\r\n            <div class=\"con\">\r\n            	<div class=\"lbtn\">&nbsp;</div>\r\n                <div class=\"rbtn\">&nbsp;</div>\r\n                <div class=\"scrollable scrollable" + im["id"].ToString().Trim() + "\">\r\n                	<div class=\"items\">\r\n                        <div class=\"main\">\r\n                        ");
	list = GetTable("content", "type=special id=" + id.ToString() + " group=" + gid.ToString() + " num=" +  "400" + " fields=id,typeid,savepath,filename,title,img order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/random/tb" + (new Random(Guid.NewGuid().GetHashCode())).Next(1,20)  + ".jpg"):item["img"].ToString().Trim();
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n                            <ul>\r\n                                <li class=\"img\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" alt=\"" + item["title"].ToString().Trim() + "\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/nopic.png'\" /></a></li>\r\n                                <li class=\"txt\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\" class=\"fyahei s13 cgrey\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),28,".."));
	templateBuilder.Append("</a></li>\r\n                            </ul>\r\n                        ");
	if (item__loop__id%4==0&&item__loop__id!=list.Rows.Count)
	{

	templateBuilder.Append("\r\n                        </div><div class=\"main\">\r\n                        ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n				<script type=\"text/javascript\">\r\n				    $(\".scrollable" + im["id"].ToString().Trim() + "\").scrollable({ next: \".lbtn\", prev: \".rbtn\", circular: true, speed: 700 });\r\n                </");
	templateBuilder.Append("script>\r\n            </div>\r\n        </div>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"cont\" style=\"padding-top:0;\">\r\n		<div class=\"cmt960\">\r\n        	<div class=\"s14\"><span class=\"s14\"><b>发帖区</b></span> 有 <span class=\"s14 cblue commentcount\">");
	templateBuilder.Append(info.Commentcount.ToString().Trim());
	templateBuilder.Append("</span> 位网友参与评论 <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/comment.aspx?id=");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"cred s14\">(点击查看)</a></div>\r\n            <div class=\"desc s13 cgrey\" style=\"padding-top:10px;\">网友评论仅供网友表达个人看法，并不表明本站同意其观点或证实其描述</div>\r\n            <textarea name=\"msg\" id=\"msg\" class=\"cmt\"></textarea>\r\n            <div class=\"tright\">\r\n            	<input type=\"button\" value=\"提交评论\" name=\"subcomment\" id=\"subcomment\" class=\"btn-comment\"/>\r\n            </div>\r\n        </div>\r\n        <div class=\"sinfo960 clearfix\">\r\n        	<div class=\"linfo left\">\r\n            	<div class=\"left\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/info01.jpg\" /></div>\r\n                <div class=\"left cgrey s12\" style=\"padding:5px 0 0 10px;\">责任编辑:");
	templateBuilder.Append(info.Author.ToString().Trim());
	templateBuilder.Append("&nbsp;&nbsp;&nbsp;&nbsp;时间:");	templateBuilder.Append(Convert.ToDateTime(info.Addtime).ToString(" yyyy-MM-dd"));
	templateBuilder.Append("</div>\r\n            </div>\r\n            <div class=\"rinfo right tright\" style=\"padding-top:7px;\">\r\n                <!-- JiaThis Button BEGIN -->\r\n                <div class=\"jiathis_style\">\r\n                    <span class=\"jiathis_txt\">分享到：</span>\r\n                    <a class=\"jiathis_button_icons_1\"></a>\r\n                    <a class=\"jiathis_button_icons_2\"></a>\r\n                    <a class=\"jiathis_button_icons_3\"></a>\r\n                    <a class=\"jiathis_button_icons_4\"></a>\r\n                    <a href=\"http://www.jiathis.com/share?uid=1665239\" class=\"jiathis jiathis_txt jtico jtico_jiathis\" target=\"_blank\"></a>\r\n                    <a class=\"jiathis_counter_style\"></a>&nbsp;\r\n                    <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cgrey\">返回首页</a>\r\n                    <a href=\"javascript:;\" onclick=\"$('.wrapper').ScrollTo(200);\" class=\"cgrey\">回到顶部</a>\r\n                </div>\r\n                <script type=\"text/javascript\">\r\n                    var jiathis_config = { data_track_clickback: 'true' };\r\n                </");
	templateBuilder.Append("script>\r\n                <script type=\"text/javascript\" src=\"http://v3.jiathis.com/code/jia.js?uid=1344725024936987\" charset=\"utf-8\"></");
	templateBuilder.Append("script>\r\n                <!-- JiaThis Button END -->\r\n            </div>\r\n        </div>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append(" \r\n<script type=\"text/javascript\">\r\n    $(\".btn-comment\").click(function () { window.open(\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/comment.aspx?id=");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append("&msg=\" + escape($(\"#msg\").val())); });\r\n    setCommentCount(");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(",function(ret){ $(\".commentcount\").html(ret); });\r\n    setContentClick(");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append(",\"no\");\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
