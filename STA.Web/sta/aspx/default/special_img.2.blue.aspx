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
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:14.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:14. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;


	DataTable list;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/base.css\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/special.2.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
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
	templateBuilder.Append("');\" class=\"cdarkgrey\" target=\"_self\">加入收藏</a></div>\r\n             <script type=\"text/javascript\">                 searchEvent();</");
	templateBuilder.Append("script>\r\n        </div>\r\n    </div>\r\n    ");	DataTable gps = SpecConGroup();
	
	templateBuilder.Append("\r\n	<div class=\"header\">\r\n    	<div class=\"banner\"><img src=\"" + info.Ext["ext_banner"].ToString().Trim() + "\" width=\"960\" /></div>\r\n        <div class=\"nav\">\r\n        	<ul  style=\"background:url(");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/nav_bluebg.jpg)\">\r\n    	        <li style=\"background:url(");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/nav_li_blue)\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cwhite\">网站首页</a></li>\r\n                ");
	item__loop__id=0;
	foreach(DataRow item in gps.Rows)
	{
		item__loop__id++;

	int gid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n                <li style=\"background:url(");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/nav_li_blue)\"><a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" onclick=\"$('.group" + item["id"].ToString().Trim() + "').ScrollTo(200);return false;\" class=\"cwhite\">" + item["name"].ToString().Trim() + "</a></li>\r\n                ");
	}	//end loop

	templateBuilder.Append("\r\n            </ul>\r\n            <script type=\"text/javascript\">                $(\".nav li:last\").addClass(\"last_li\");</");
	templateBuilder.Append("script>\r\n        </div>\r\n    </div>\r\n    <div class=\"item1\">\r\n    	<div class=\"item1_left\">\r\n               <div id=\"topslide\" style=\"visibility:hidden;\">\r\n                  <div class=\"loading\"></div>\r\n                  <div class=\"pic\">\r\n                      <ul>        \r\n                    ");	string ssql = "select title,img,thumb,text,url from plus_slideimgs where likeid = '专题:" + info.Title + "' order by orderid desc";
	

	list = GetSqlTable("" + ssql.ToString() +"");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                        <li><a href=\"" + item["url"].ToString().Trim() + "\" target=\"_blank\"><img alt=\"" + item["title"].ToString().Trim() + "\" text=\"" + item["text"].ToString().Trim() + "\" width=\"514\" height=\"300\"  src=\"" + item["img"].ToString().Trim() + "\"/></a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("  \r\n                      </ul>\r\n                  </div>\r\n                </div>\r\n				<script type=\"text/javascript\">\r\n				    $('#topslide').myFocus({ pattern: 'mF_classicHC', width: 514, auto: true, height: 300, trigger: \"mouseover\" });\r\n                </");
	templateBuilder.Append("script>  \r\n        </div>\r\n        <div class=\"item1_right\">\r\n        	");
	templateBuilder.Append(info.Content.ToString().Trim());
	templateBuilder.Append("\r\n        </div>\r\n    </div>\r\n\r\n    ");
	if (gps.Rows.Count>0)
	{

	int gid = TypeParse.StrToInt(gps.Rows[0]["id"]);
	
	string name = gps.Rows[0]["name"].ToString();
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n    <div class=\"item2 group");
	templateBuilder.Append(gid.ToString());
	templateBuilder.Append("\">\r\n    	<h3 style=\"color:#003399\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/item_tubiao_blue.jpg\" />");
	templateBuilder.Append(name.ToString());
	templateBuilder.Append("<span><a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"");
	templateBuilder.Append(name.ToString());
	templateBuilder.Append("\">更多>></a></span></h3>\r\n        <ul class=\"item2_ul\">\r\n        ");
	list = GetTable("content", "type=special id=" + id.ToString() + " group=" + gid.ToString() + " num=" +  "10" + " fields=id,typeid,savepath,filename,title,img order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/default.png"):item["img"].ToString().Trim();
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n        <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/default.png'\" width=\"176\" height=\"152\"/></a><span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),24,""));
	templateBuilder.Append("</a></span></li>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n        </ul>\r\n    </div>\r\n    ");
	}	//end if


	if (gps.Rows.Count>1)
	{

	int gid = TypeParse.StrToInt(gps.Rows[1]["id"]);
	
	string name = gps.Rows[1]["name"].ToString();
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n    <div class=\"item3 group");
	templateBuilder.Append(gid.ToString());
	templateBuilder.Append("\">\r\n        <h3 style=\"color:#003399\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/item_tubiao_blue.jpg\"/> ");
	templateBuilder.Append(name.ToString());
	templateBuilder.Append("<span><a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"");
	templateBuilder.Append(name.ToString());
	templateBuilder.Append("\">更多>></a></span></h3>\r\n        <div class=\"item3_left group");
	templateBuilder.Append(gid.ToString());
	templateBuilder.Append("\">\r\n        ");
	list = GetTable("content", "type=special id=" + id.ToString() + " group=" + gid.ToString() + " num=" +  "1" + " fields=id,typeid,savepath,filename,title,img order=" +  "2" + " property=r");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/default.png"):item["img"].ToString().Trim();
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" class=\"s13 bold\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/default.png'\" width=\"350\" height=\"260\"/></a><span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),48,""));
	templateBuilder.Append("</a></span>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n        </div>\r\n        <ul class=\"item3_ul\">\r\n        ");
	list = GetTable("content", "type=special id=" + id.ToString() + " group=" + gid.ToString() + " num=" +  "6" + " fields=id,typeid,savepath,filename,title,img order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/default.png"):item["img"].ToString().Trim();
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n        <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/default.png'\" width=\"176\" height=\"152\"/></a><span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),24,""));
	templateBuilder.Append("</a></span></li>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n        </ul>\r\n    </div>\r\n    ");
	}	//end if


	if (gps.Rows.Count>2)
	{

	int gid = TypeParse.StrToInt(gps.Rows[2]["id"]);
	
	string name = gps.Rows[2]["name"].ToString();
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n    <div class=\"item4 group");
	templateBuilder.Append(gid.ToString());
	templateBuilder.Append("\">\r\n        <h3 style=\"color:#003399\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/item_tubiao_blue.jpg\"/> ");
	templateBuilder.Append(name.ToString());
	templateBuilder.Append("<span><a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"");
	templateBuilder.Append(name.ToString());
	templateBuilder.Append("\">更多>></a></span></h3>\r\n        <ul class=\"item4_ul\">\r\n        ");
	list = GetTable("content", "type=special id=" + id.ToString() + " group=" + gid.ToString() + " num=" +  "5" + " fields=id,typeid,savepath,filename,title,img order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/default.png"):item["img"].ToString().Trim();
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n        <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/default.png'\" width=\"155\" height=\"117\"/></a><span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),24,""));
	templateBuilder.Append("</a></span></li>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n        </ul>\r\n    </div>\r\n    ");
	}	//end if


	if (gps.Rows.Count>3)
	{


	gp__loop__id=0;
	foreach(DataRow gp in gps.Rows)
	{
		gp__loop__id++;


	if (gp__loop__id>3)
	{

	int gid = TypeParse.StrToInt(gp["id"].ToString().Trim());
	
	string url = Urls.SpecGroup(id,gid);
	
	templateBuilder.Append("\r\n    <div class=\"item2 group");
	templateBuilder.Append(gid.ToString());
	templateBuilder.Append("\">\r\n        <h3 style=\"color:#003399\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/spec/2/item_tubiao_blue.jpg\"/> " + gp["name"].ToString().Trim() + "<span><a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + gp["name"].ToString().Trim() + "\">更多>></a></span></h3>\r\n        <ul class=\"item2_ul\">\r\n        ");
	list = GetTable("content", "type=special id=" + id.ToString() + " group=" + gid.ToString() + " num=" +  "5" + " fields=id,typeid,savepath,filename,title,img order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/default.png"):item["img"].ToString().Trim();
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n        <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/default.png'\" width=\"155\" height=\"117\"/></a><span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),24,""));
	templateBuilder.Append("</a></span></li>\r\n        ");
	}	//end loop

	templateBuilder.Append("\r\n        </ul>\r\n    </div>\r\n    ");
	}	//end if


	}	//end loop


	}	//end if

	templateBuilder.Append("\r\n\r\n    <div class=\"cont\" style=\"padding-top:10px;\">\r\n		<div class=\"cmt960\">\r\n        	<div class=\"s14\"><span class=\"s14\"><b>发帖区</b></span> 有 <span class=\"s14 cblue commentcount\">");
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
	templateBuilder.Append("script>\r\n                <!-- JiaThis Button END -->\r\n            </div>\r\n        </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append(" \r\n </div>\r\n<script type=\"text/javascript\">\r\n    $(\".btn-comment\").click(function () { window.open(\"");
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
