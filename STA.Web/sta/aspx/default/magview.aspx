<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Magview" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">\r\n<html>\r\n<head>\r\n<title>杂志:");
	templateBuilder.Append(info.Name.ToString().Trim());
	templateBuilder.Append(" - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<link href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" rel=\"icon\">\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(info.Name.ToString().Trim());
	templateBuilder.Append(",杂志,在线阅读下载\">\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(info.Name.ToString().Trim());
	templateBuilder.Append("在线阅读下载\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/booklet/jquery.easing.1.3.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n<script src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/booklet/jquery.booklet.1.1.0.min.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n<link href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/booklet/jquery.booklet.1.1.0.css\" type=\"text/css\" rel=\"stylesheet\" media=\"screen\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/booklet/style.css\" type=\"text/css\" media=\"screen\" />\r\n<script src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/cufon/cufon-yui.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n<script src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/cufon/ChunkFive_400.font.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n<script src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/mag/cufon/Note_this_400.font.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n    Cufon.replace('h1,p,.b-counter');\r\n    Cufon.replace('.book_wrapper a', { hover: true });\r\n    Cufon.replace('.title', { textShadow: '1px 1px #C59471', fontFamily: 'ChunkFive' });\r\n    Cufon.replace('.reference a', { textShadow: '1px 1px #C59471', fontFamily: 'ChunkFive' });\r\n    Cufon.replace('.loading', { textShadow: '1px 1px #000', fontFamily: 'ChunkFive' });\r\n</");
	templateBuilder.Append("script>\r\n</head>\r\n<body>\r\n	<div style=\"position:absolute;top:0;left:0;z-index:0;width:100%;height:100%\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/mag/images/01.jpg\" class=\"zbg\" width=\"100%\" height=\"100%\"/></div>\r\n    <div class=\"book_wrapper\">\r\n			<a id=\"next_page_button\"></a>\r\n			<a id=\"prev_page_button\"></a>\r\n			<div id=\"loading\" class=\"loading\">Loading pages...</div>\r\n			<div id=\"mybook\" style=\"display:none;\">\r\n				<div id=\"Pagecontent\" class=\"b-load\">\r\n                ");	DataTable lt = MagazineList(info.Content);
	

	item__loop__id=0;
	foreach(DataRow item in lt.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                <div><img src=\"" + item["url"].ToString().Trim() + "\"/></div>\r\n                ");
	}	//end loop

	templateBuilder.Append("\r\n                </div>\r\n			</div>\r\n		</div>\r\n        <script type=\"text/javascript\">\r\n            $(function () {\r\n                var $mybook = $('#mybook');\r\n                var $bttn_next = $('#next_page_button');\r\n                var $bttn_prev = $('#prev_page_button');\r\n                var $loading = $('#loading');\r\n                var $mybook_images = $mybook.find('img');\r\n                var cnt_images = $mybook_images.length;\r\n                var loaded = 0;\r\n\r\n                $mybook_images.each(function () {\r\n                    var $img = $(this);\r\n                    var source = $img.attr('src');\r\n                    $('<img/>').load(function () {\r\n                        ++loaded;\r\n                        if (loaded == cnt_images) {\r\n                            $loading.hide();\r\n                            $bttn_next.show();\r\n                            $bttn_prev.show();\r\n                            $mybook.show().booklet({\r\n                                name: null,\r\n                                width: " + info.Ratio.Split(',')[0].ToString().Trim() + ",\r\n                                height: " + info.Ratio.Split(',')[1].ToString().Trim() + ",\r\n                                speed: 600,\r\n                                direction: 'LTR',\r\n                                startingPage: 0,\r\n                                easing: 'easeInOutQuad',\r\n                                easeIn: 'easeInQuad',\r\n                                easeOut: 'easeOutQuad',\r\n\r\n                                closed: false,\r\n                                closedFrontTitle: null,\r\n                                closedFrontChapter: null,\r\n                                closedBackTitle: null,\r\n                                closedBackChapter: null,\r\n                                covers: false,\r\n\r\n                                pagePadding: 0,\r\n                                pageNumbers: false,\r\n\r\n                                hovers: false,\r\n                                overlays: false,\r\n                                tabs: false,\r\n                                tabWidth: 60,\r\n                                tabHeight: 20,\r\n                                arrows: false,\r\n                                cursor: 'pointer',\r\n\r\n                                hash: false,\r\n                                keyboard: true,\r\n                                next: $bttn_next,\r\n                                prev: $bttn_prev,\r\n\r\n                                menu: null,\r\n                                pageSelector: false,\r\n                                chapterSelector: false,\r\n\r\n                                shadows: true,\r\n                                shadowTopFwdWidth: 0,\r\n                                shadowTopBackWidth: 0,\r\n                                shadowBtmWidth: 50,\r\n\r\n                                before: function () { },\r\n                                after: function () { }\r\n                            });\r\n                            Cufon.refresh();\r\n                        }\r\n                    }).attr('src', source);\r\n                });\r\n\r\n            });\r\n        </");
	templateBuilder.Append("script>\r\n</body>\r\n</html>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
