<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.User.Readpm" %>
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


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>");
	templateBuilder.Append(meta.ToString());
	templateBuilder.Append("\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/artdialog.css\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/reset.css\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/member.css\" type=\"text/css\" />");
	templateBuilder.Append(link.ToString());
	templateBuilder.Append("\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/jquery.artDialog.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/artDialog.plugins.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/blockUI.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/timeago/jquery.timeago.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/timeago/locales/jquery.timeago.zh-CN.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/member.js\"></");
	templateBuilder.Append("script>");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n</head>\r\n<body>\r\n<div class=\"header\">\r\n  <div class=\"logo\">\r\n  	<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\" class=\"left disblock\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/member/eagle.png\" width=\"124\" height=\"108\"/></a>\r\n    <h3 class=\"left disblock\">会员中心</h3>\r\n  </div>\r\n  <div class=\"link\">你好 (");
	templateBuilder.Append(username.ToString());
	templateBuilder.Append(")<span> | <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/inboxpm.aspx\">短消息</a> |");
	if (user.Adminid>0)
	{

	templateBuilder.Append(" </span><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append(baseconfig.adminpath.ToString().Trim());
	templateBuilder.Append("/\" target=\"_blank\">系统管理</a><span> |");
	}	//end if

	templateBuilder.Append(" </span><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=loginout\">退出</a><span> | </span><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/\" target=\"_blank\">首页</a> </div>\r\n");
	int curmenu = 1;
	if(Utils.InArray(pagename,"avatar.aspx,account.aspx,profile.aspx,changepassword.aspx,changeemail.aspx"))
	{
	    curmenu = 2;
	}
	else if(Utils.InArray(pagename,"favorite.aspx,comment.aspx"))
	{
	    curmenu = 3;
	}
	
	templateBuilder.Append("\r\n  <div class=\"menu\">\r\n  	<div class=\"cont\">\r\n        <ul class=\"nav\">\r\n           <li");
	if (curmenu==1)
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">管理中心</a></li>\r\n           <li");
	if (curmenu==2)
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/account.aspx\">账号管理</a></li>\r\n           <li");
	if (curmenu==3)
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/favorite.aspx\">互动管理</a></li>\r\n        </ul>\r\n    </div>\r\n    <div class=\"bot\"></div>\r\n  </div>\r\n</div>\r\n<div class=\"conarea\">\r\n    <div class=\"left col-1 left-memu\">\r\n        ");
	if (curmenu==2)
	{

	templateBuilder.Append("\r\n        <h5 class=\"title\"> 基本设置</h5>\r\n        <ul>\r\n            <li");
	if (pagename=="profile.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/profile.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/user.gif\" width=\"14\" height=\"15\" /> 个人信息维护</a></li>\r\n            <li");
	if (pagename=="avatar.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/avatar.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/job.gif\" width=\"14\" height=\"16\" /> 修改头像</a></li>\r\n        </ul>\r\n        <h6 class=\"title\">安全设置</h6>\r\n        <ul>\r\n            <li");
	if (pagename=="changepassword.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/changepassword.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/key.gif\" width=\"14\" height=\"16\" /> 修改密码</a></li>\r\n            <li");
	if (pagename=="changeemail.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/changeemail.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/email.gif\" width=\"14\" height=\"15\" /> 更换邮箱</a></li>\r\n            <li><a href=\"#\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/configuration.gif\" width=\"14\" height=\"15\" /> 隐私设置</a></li>\r\n        </ul>\r\n        ");
	}
	else if (curmenu==3)
	{

	templateBuilder.Append("\r\n        <h5 class=\"title\"> 互动管理</h5>\r\n        <ul>\r\n            <li");
	if (pagename=="favorite.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/favorite.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/log.gif\" width=\"14\" height=\"15\" /> 我的收藏</a></li>\r\n            <li");
	if (pagename=="comment.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/comment.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/comment.png\" width=\"14\" height=\"15\" /> 我的评论</a></li>\r\n        </ul>\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <h5 class=\"title\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_1.png\" width=\"15\" height=\"15\" /> 管理中心</h5>\r\n        <ul>\r\n            <li");
	if (pagename=="con_add.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/con_add.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_2.png\" width=\"14\" height=\"15\" /> 在线投稿</a></li>\r\n            <li");
	if (pagename=="con_list.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/con_list.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_3.png\" width=\"14\" height=\"16\" /> 已发布稿件</a></li>\r\n            <li");
	if (pagename=="attachs.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/attachs.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/attach2.gif\" width=\"14\" height=\"16\" /> 已上传附件</a></li>\r\n        </ul>\r\n<!--        <h6 class=\"title\">财务管理</h6>\r\n        <ul>\r\n            <li><a href=\"#\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_4.png\" width=\"15\" height=\"16\" /> 在线充值</a></li>\r\n            <li><a href=\"#\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_8.png\" width=\"16\" height=\"16\" /> 支付记录</a></li>\r\n            <li><a href=\"#\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/table-information.png\" width=\"16\" height=\"16\" /> 消费记录</a></li>\r\n            <li><a href=\"#\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/coins_add.png\" width=\"16\" height=\"16\" /> 积分购买/兑换</a></li>\r\n        </ul>-->\r\n        <h6 class=\"title\">短消息</h6>\r\n        <ul>\r\n            <li");
	if (pagename=="sendpm.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/sendpm.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_9.png\" width=\"16\" height=\"14\" /> 发送短消息</a></li>\r\n            <li");
	if (pagename=="inboxpm.aspx"||(pagename=="readpm.aspx"&&STARequest.GetString("from")=="inbox"))
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/inboxpm.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_11.png\" width=\"16\" height=\"16\" /> 收件箱");
	if (user.Newpmcount>0)
	{

	templateBuilder.Append("<font color=\"red\">(");
	templateBuilder.Append(user.newpmcount.ToString().Trim());
	templateBuilder.Append(")</font>");
	}	//end if

	templateBuilder.Append("</a></li>\r\n            <li");
	if (pagename=="outboxpm.aspx"||(pagename=="readpm.aspx"&&STARequest.GetString("from")=="outbox"))
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/outboxpm.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/m_10.png\" width=\"16\" height=\"16\" /> 发件箱</a></li>\r\n            <li");
	if (pagename=="syspm.aspx")
	{

	templateBuilder.Append(" class=\"on\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/syspm.aspx\"><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/pics/icon/lightbulb.png\" width=\"16\" height=\"16\" /> 系统消息(");
	templateBuilder.Append(STA.Core.PrivateMessages.GetAnnouncePrivateMessageCount().ToString().Trim());
	templateBuilder.Append(")</a></li>\r\n        </ul>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n        <span class=\"o1\"></span><span class=\"o2\"></span><span class=\"o3\"></span><span class=\"o4\"></span>\r\n    </div>");

	string from = STARequest.GetString("from")=="inbox"?"inbox":"outbox";
	
	templateBuilder.Append("\r\n    <div class=\"left right-container\">\r\n    	<div class=\"con-box left col-1\">\r\n            ");
	if (canread)
	{

	templateBuilder.Append("\r\n        	<div class=\"bartitle\"><h3>与 ");
	if (info.Msgfromid==userid)
	{
	templateBuilder.Append(info.msgto.ToString().Trim());
	}
	else
	{
	templateBuilder.Append(info.Msgfrom.ToString().Trim());
	}	//end if

	templateBuilder.Append("的短消息</h3></div>\r\n            <div class=\"con-info\">\r\n                <table width=\"100%\" cellspacing=\"0\" class=\"zform\">      \r\n                 <tr>\r\n                       <td>\r\n                       <div class=\"readpm\">\r\n                       <h3>");
	templateBuilder.Append(info.subject.ToString().Trim());
	templateBuilder.Append(" - ");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(info.Addtime)).ToString(" yyyy-MM-dd HH:mm:ss"));
	templateBuilder.Append("</h3>\r\n                       发送人: <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/sendpm.aspx?action=re&pmid=");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append("\">");
	if (info.Msgfromid==userid)
	{
	templateBuilder.Append(info.msgto.ToString().Trim());
	}
	else
	{
	templateBuilder.Append(info.Msgfrom.ToString().Trim());
	}	//end if

	templateBuilder.Append("</a>\r\n                       <br /><br /><p>");
	templateBuilder.Append(info.content.ToString().Trim());
	templateBuilder.Append("</p>\r\n                       </div>\r\n                       </td>\r\n                 </tr>\r\n                </table>\r\n                	<div class=\"btn-manage\">\r\n                    	<input name=\"btnback\" id=\"btnback\" type=\"button\" class=\"button\" value=\"返回列表\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/");
	templateBuilder.Append(from.ToString());
	templateBuilder.Append("pm.aspx'\"/>&nbsp;\r\n                        ");
	if ((int)info.Folder==0)
	{

	templateBuilder.Append("\r\n                        <input name=\"replay\" id=\"replay\" type=\"button\" class=\"button\" value=\"回复\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/sendpm.aspx?action=re&pmid=");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append("'\"/>&nbsp;\r\n                        ");
	}	//end if

	templateBuilder.Append("\r\n                        <input name=\"forward\" id=\"forward\" type=\"button\" class=\"button\" value=\"转发\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/sendpm.aspx?action=fw&pmid=");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append("'\"/>&nbsp;\r\n                        <!--<input name=\"noread\" id=\"noread\" type=\"button\" class=\"button\" value=\"标记为未读\"  onclick=\"setPmsState(");
	templateBuilder.Append(pmid.ToString());
	templateBuilder.Append(",1,'");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/readpm.aspx?action=noread&pmid=");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append("')\"/>&nbsp;-->\r\n                        <input name=\"del\" id=\"del\" type=\"button\" class=\"button\" value=\"删除\" onclick=\"delPms(");
	templateBuilder.Append(pmid.ToString());
	templateBuilder.Append(",'");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/");
	templateBuilder.Append(from.ToString());
	templateBuilder.Append("pm.aspx')\"/>&nbsp;\r\n                    </div> \r\n            </div>\r\n        	<span class=\"o1\"></span><span class=\"o2\"></span><span class=\"o3\"></span><span class=\"o4\"></span>\r\n        </div>\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <div class=\"bartitle\"><h3>查看短消息</h3></div>\r\n            <div class=\"con-info\">\r\n                <div class=\"zerotable\">");
	templateBuilder.Append(msgtext.ToString());
	templateBuilder.Append("</div>\r\n            </div>\r\n            <script type=\"text/javascript\">\r\n                $.dialog.tip(\"");
	templateBuilder.Append(msgtext.ToString());
	templateBuilder.Append("\", 1, function () { location.href = '");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/");
	templateBuilder.Append(from.ToString());
	templateBuilder.Append("pm.aspx'; });\r\n            </");
	templateBuilder.Append("script>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n    </div>\r\n</div>\r\n");
	templateBuilder.Append("<div class=\"footer\">\r\n    <a href=\"#\" target=\"_blank\">关于我们</a> |  \r\n    <a href=\"#\" target=\"_blank\">联系方式</a> |  \r\n    <a href=\"#\" target=\"_blank\">版权声明</a> |  \r\n    <a href=\"#\" target=\"_blank\">招聘信息</a> |  \r\n    <a href=\"#\" target=\"_blank\">友情链接</a>\r\n    <p class=\"cp\">Powered by <strong><a href=\"http://www.stacms.com\" target=\"_blank\">STACMS</a></strong> <em>v1.0</em> &copy; 2013 <img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/copyright.gif\"/>\r\n    </p>\r\n</div>\r\n</body>\r\n</html>\r\n");


	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
