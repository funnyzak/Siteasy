<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.User.Outboxpm" %>
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

	templateBuilder.Append("\r\n    <div class=\"left right-container\">\r\n        <div class=\"con-box left col-1\">\r\n            <div class=\"bartitle clearfix\">\r\n            	<div class=\"bartit-picon left\" style=\"background-position:-188px 0;\">&nbsp;</div><h3 class=\"disblock left pad\">发件箱</h3>\r\n            	<button class=\"btn-pmsend\" title=\"\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/sendpm.aspx'\">&nbsp;</button>\r\n            </div>\r\n            <div class=\"con-info\">\r\n                ");	int page = STARequest.GetQueryInt("page",1);
	
	int pagecount = 0;
	
	int recordcount = 0;
	
	DataTable msglist = STA.Core.PrivateMessages.GetPrivateMessageList(userid,1,9,page,2,out pagecount,out recordcount);
	
	templateBuilder.Append("\r\n                <form name=\"zform\" id=\"zform\" action=\"\" method=\"post\">\r\n                    ");
	if (msglist.Rows.Count<=0)
	{

	templateBuilder.Append("\r\n                    <div class=\"zerotable\">发件箱暂无短信息</div>\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                     <table width=\"100%\" cellspacing=\"0\"  class=\"list\">\r\n                        <thead>\r\n                            <tr>\r\n                            <th width=\"5%\"><input type=\"checkbox\" value=\"\" id=\"check_box\" onclick=\"checkAll(this.form,this.checked);\"/></th>\r\n                            <th width=\"3%\"></th>\r\n                            <th width=\"40%\">标 题</th>\r\n                            <th width=\"15%\">收件人</th>\r\n                            <th width=\"15%\">发送时间</th>\r\n                            </tr>\r\n                        </thead>\r\n                        <tbody>\r\n                            ");
	item__loop__id=0;
	foreach(DataRow item in msglist.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                            <tr>\r\n                                <td width=\"5%\" align=\"center\"><input type=\"checkbox\" name=\"cbid\" value=\"" + item["id"].ToString().Trim() + "\" onclick=\"checkedEnabledButton(this.form, 'cbid', 'btndelete', 'btnread')\"/></td>\r\n                                <td width=\"3%\" align=\"center\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/pm_" + item["new"].ToString().Trim() + ".gif\" alt=\"");
	if (item["new"].ToString().Trim()=="1")
	{

	templateBuilder.Append("未读");
	}
	else
	{

	templateBuilder.Append("已读");
	}	//end if

	templateBuilder.Append("\"/></td>\r\n                                <td width=\"40%\" align=\"left\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/readpm.aspx?pmid=" + item["id"].ToString().Trim() + "&from=outbox\" class=\"cblue");
	if (item["new"].ToString().Trim()=="1")
	{

	templateBuilder.Append(" bold");
	}	//end if

	templateBuilder.Append("\">" + item["subject"].ToString().Trim() + "</a></td>\r\n                                <td width=\"15%\" align=\"center\" class=\"cgrey\">" + item["msgto"].ToString().Trim() + "</td>\r\n                                <td width=\"15%\" align=\"center\" class=\"cgrey\"><span class=\"posttime\" title=\"");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(item["addtime"].ToString().Trim())).ToString(" yyyy-MM-dd HH:mm:ss"));
	templateBuilder.Append("\"></span></td>\r\n                            </tr>\r\n                            ");
	}	//end loop

	templateBuilder.Append("\r\n                        </tbody>\r\n                    </table>\r\n                	<div class=\"btn-manage\">\r\n                    	<input name=\"btndelete\" id=\"btndelete\" type=\"button\" class=\"button\" value=\"删除选中\" onclick=\"delPms(getCbCheckedVals(),'");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/outboxpm.aspx')\" disabled=\"disabled\"/>&nbsp;\r\n                        <input name=\"btnread\" id=\"btnread\" type=\"button\" class=\"button\" value=\"标记为已读\" disabled=\"disabled\" onclick=\"setPmsState(getCbCheckedVals(),0,'");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/outboxpm.aspx')\"/>&nbsp;&nbsp;\r\n                    </div> \r\n                        ");
	if (pagecount>1)
	{

	string pagenumbers = PageNumber(siteurl+"/user/outboxpm.aspx",siteurl+"/user/outboxpm.aspx?page=@page",page,pagecount,recordcount,7, false).ToString();
	
	templateBuilder.Append("\r\n                            <div class=\"pages\">\r\n                                ");
	templateBuilder.Append(pagenumbers.ToString());
	templateBuilder.Append("\r\n                            </div>\r\n                        ");
	}	//end if

	templateBuilder.Append("\r\n                    \r\n		                <script type=\"text/javascript\">\r\n		                    $(function () {\r\n		                        $(\"table.list tr:even\").css({ \"background-color\": \"#f8f8f8\" });\r\n		                        $(\"span.posttime\").timeago();\r\n		                    });\r\n		                    function checkAll(form, checked) {\r\n		                        checkByName(form, 'cbid', checked);\r\n		                        checkedEnabledButton(form, 'cbid', 'btndelete', 'btnread')\r\n		                    };\r\n                        </");
	templateBuilder.Append("script>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                </form>   \r\n            </div>\r\n            <span class=\"o1\"></span><span class=\"o2\"></span><span class=\"o3\"></span><span class=\"o4\"></span>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
	templateBuilder.Append("<div class=\"footer\">\r\n    <a href=\"#\" target=\"_blank\">关于我们</a> |  \r\n    <a href=\"#\" target=\"_blank\">联系方式</a> |  \r\n    <a href=\"#\" target=\"_blank\">版权声明</a> |  \r\n    <a href=\"#\" target=\"_blank\">招聘信息</a> |  \r\n    <a href=\"#\" target=\"_blank\">友情链接</a>\r\n    <p class=\"cp\">Powered by <strong><a href=\"http://www.stacms.com\" target=\"_blank\">STACMS</a></strong> <em>v1.0</em> &copy; 2013 <img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/copyright.gif\"/>\r\n    </p>\r\n</div>\r\n</body>\r\n</html>\r\n");


	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
