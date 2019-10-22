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


	if(info==null){
	    Server.Transfer("nofound.aspx");
	    return;
	}
	


	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>");
	templateBuilder.Append(meta.ToString());
	templateBuilder.Append("\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/sgallery/p_show.css\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/sgallery/jquery.sgallery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/global.js\"></");
	templateBuilder.Append("script>");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n</head>\r\n<body>\r\n<div class=\"center\">\r\n	<div class=\"banner\"><a href=\"");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("\" title=\"图片频道\"></a></div>\r\n    <div class=\"photo-channel\">\r\n        <div id=\"Article\">\r\n            <div class=\"png_warp\">\r\n                <div class=\"pngtm_t\"></div>\r\n                <div class=\"big-pic\">\r\n                	<div id=\"nhp_poparea\"><a href=\"");
	templateBuilder.Append(Urls.Content(id).ToString().Trim());
	templateBuilder.Append("\" class=\"nhp_pclose\"></a><span>支持键盘← →键翻阅图片</span></div>\r\n\r\n                    <div id=\"big-pic\"></div>\r\n                    <div class=\"photo_prev\"><a id=\"photoPrev\" title=\"&lt;上一张\" class=\"btn_pphoto\" target=\"_self\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showpic('pre');\"></a></div>\r\n                    <div class=\"photo_next\"><a id=\"photoNext\" title=\"下一张&gt;\"class=\"btn_nphoto\" target=\"_self\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showpic('next')\"></a></div>\r\n                    <div id=\"text_warp\" class=\"active\">\r\n                        <div class=\"text-bg\"></div>\r\n                        <div class=\"text-warp\">\r\n                        	<h1>");
	templateBuilder.Append(info.title.ToString().Trim());
	templateBuilder.Append("</h1>\r\n                        	<div class=\"text\" id=\"picinfo\"></div>\r\n                        	来源│");
	templateBuilder.Append(info.source.ToString().Trim());
	templateBuilder.Append("&nbsp;&nbsp;&nbsp;&nbsp;热度│<span class=\"conclick\">");
	templateBuilder.Append(info.click.ToString().Trim());
	templateBuilder.Append("</span>&nbsp;&nbsp;&nbsp;&nbsp;编辑│");
	templateBuilder.Append(info.author.ToString().Trim());
	templateBuilder.Append("</div>\r\n			                <script type=\"text/javascript\">\r\n                            setContentClick(");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append(",\"yes\",function(c){ $(\".conclick\").html(c); });\r\n                            </");
	templateBuilder.Append("script>\r\n                    </div>\r\n                    <div id=\"disp_text\"></div>\r\n                    <div id=\"endSelect\" style=\"display: none;\">\r\n                        <div id=\"endSelClose\" onClick=\"$('#endSelect').hide();\"></div>\r\n                        <div class=\"bg\"></div>\r\n                        <div class=\"E_Cont\">\r\n                            <p>您已经浏览完所有图片</p>\r\n                            <p><a id=\"rePlayBut\" href=\"javascript:void(0)\" onClick=\"showpic('next', 1);\"></a>\r\n                        ");	ContentInfo pinfo = PrevCon(true, "");
	

	if (pinfo!=null)
	{

	string url = Urls.Content(pinfo.Id,pinfo.Typeid,pinfo.Savepath,pinfo.Filename);
	
	templateBuilder.Append("\r\n                             <a id=\"nextPicsBut\" href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\"></a>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                             <a id=\"nextPicsBut\" href=\"javascript:alert('最后一页');\"></a>\r\n                        ");
	}	//end if

	templateBuilder.Append("\r\n                           \r\n                            \r\n                            </p>	\r\n                        </div>\r\n                    </div>\r\n                </div><!--/big-pic-->\r\n                <div class=\"pngtm_m\"></div>\r\n            </div>\r\n            <div class=\"list-pic\">\r\n                <a href=\"javascript:;\" onclick=\"showpic('pre')\" class=\"pre-bnt\"><span></span></a>\r\n                <div class=\"cont\" style=\"position:relative\">\r\n                    <ul class=\"cont picbig\" id=\"pictureurls\" style=\"position:absolute\">\r\n                     ");	 list = PhotoList(info.Ext["ext_imgs"].ToString().Trim());
	

	if (list.Rows.Count>0)
	{


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	string thumb = ImgThumb(item["url"].ToString().Trim());
	
	templateBuilder.Append("\r\n                            <li class=\"\"><div class=\"img-wrap\"><a href=\"javascript:;\" hidefocus=\"true\"><img src=\"");
	templateBuilder.Append(thumb.ToString());
	templateBuilder.Append("\" width=\"160\" height=\"100\" alt=\"" + item["name"].ToString().Trim() + "\" rel=\"" + item["url"].ToString().Trim() + "\" data-pinit=\"registered\"></a><span>");
	templateBuilder.Append(item__loop__id.ToString());
	templateBuilder.Append("/");
	templateBuilder.Append(list.Rows.Count.ToString().Trim());
	templateBuilder.Append("</span></div></li>\r\n                        ");
	}	//end loop


	}	//end if

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n                <a href=\"javascript:;\" onclick=\"showpic('next')\" class=\"next-bnt\"><span></span></a>\r\n            </div><!--/list-pic-->\r\n        </div><!--/Article-->\r\n    </div><!--/photo-channel-->\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/sgallery/show_picture.js\"></");
	templateBuilder.Append("script>\r\n</div><!--/center-->\r\n<div class=\"footer\">\r\n    <p> Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("</p>\r\n</div>\r\n</body>\r\n</html>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
