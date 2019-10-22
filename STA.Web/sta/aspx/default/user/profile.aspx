<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.User.Profile" %>
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

	templateBuilder.Append("\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.tools.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/datepicker/WdatePicker.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/formvalidator/formValidator.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/formvalidator/formValidatorRegex.js\"></");
	templateBuilder.Append("script>\r\n    <div class=\"left right-container\">\r\n        <div class=\"con-box left col-1\">\r\n            <div class=\"bartitle clearfix\"><div class=\"bartit-picon left\" style=\"background-position:-47px -47px;\">&nbsp;</div><h3 class=\"disblock left pad\">个人信息维护</h3></div>\r\n            <div class=\"con-info\">\r\n                <form name=\"zform\" id=\"zform\" action=\"\" method=\"post\">\r\n                <ul class=\"f_tab\">\r\n                    <li class=\"now\">基本资料</li>\r\n                    <li>联系方式</li>\r\n                </ul>				\r\n				<table width=\"100%\" cellspacing=\"0\" class=\"zform fill\">\r\n					<tr>\r\n						<th width=\"100\">真实姓名</th> \r\n						<td><input id=\"realname\" name=\"realname\" value=\"");
	templateBuilder.Append(userfield.realname.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td width=\"460\" style=\"padding-top:1px;\"><div id=\"realnameTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>性别</th> \r\n						<td>\r\n                        <span class=\"gender\">\r\n						    <input type=\"radio\" name=\"gender\" id=\"male\" value=\"1\"");
	if (user.Gender==1)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> <label for=\"male\">男</label>&nbsp;&nbsp;\r\n						    <input type=\"radio\" name=\"gender\" id=\"female\" value=\"0\"");
	if (user.Gender==0)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> <label for=\"female\">女</label>\r\n					    </span>\r\n                        </td>\r\n                        <td></td>\r\n					</tr>\r\n					<tr>\r\n						<th>生日</th> \r\n                        ");	string birthday = user.Birthday.ToString("yyyy-MM-dd");
	
	templateBuilder.Append("\r\n						<td><input id=\"birthday\" name=\"birthday\" value=\"");
	templateBuilder.Append(birthday.ToString());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"15\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"birthdayTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>昵称</th> \r\n						<td><input id=\"nickname\" name=\"nickname\" value=\"");
	templateBuilder.Append(user.nickname.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"nicknameTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>来自</th> \r\n						<td><input id=\"areaname\" name=\"areaname\" value=\"");
	templateBuilder.Append(userfield.areaname.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"areanameTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>身份证号</th> \r\n						<td><input id=\"idcard\" name=\"idcard\" value=\"");
	templateBuilder.Append(userfield.idcard.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"idcardTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>个性签名</th> \r\n						<td><input id=\"signature\" name=\"signature\" value=\"");
	templateBuilder.Append(userfield.signature.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"signatureTip\"></div></td>\r\n					</tr>\r\n\r\n					<tr>\r\n						<th>个人介绍</th> \r\n						<td colspan=\"2\"><textarea id=\"description\" name=\"description\" style=\"width:325px;height:100px;\">");
	templateBuilder.Append(userfield.description.ToString().Trim());
	templateBuilder.Append("</textarea></td>\r\n					</tr>\r\n				</table>\r\n				<table width=\"100%\" cellspacing=\"0\" class=\"zform fill\" style=\"display:none\">\r\n					<tr>\r\n						<th width=\"100\">个人网站</th> \r\n						<td><input id=\"website\" name=\"website\" value=\"");
	templateBuilder.Append(userfield.website.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                         <td width=\"460\" style=\"padding-top:1px;\"><div id=\"websiteTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>移动电话</th> \r\n						<td><input id=\"mobile\" name=\"mobile\" value=\"");
	templateBuilder.Append(userfield.mobile.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th>办公电话</th> \r\n						<td><input id=\"worktel\" name=\"worktel\" value=\"");
	templateBuilder.Append(userfield.worktel.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th>家庭电话</th> \r\n						<td><input id=\"hometel\" name=\"hometel\" value=\"");
	templateBuilder.Append(userfield.hometel.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th>邮政编码</th> \r\n						<td><input id=\"postcode\" name=\"postcode\" value=\"");
	templateBuilder.Append(userfield.postcode.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th>联系地址</th> \r\n						<td><input id=\"address\" name=\"address\" value=\"");
	templateBuilder.Append(userfield.address.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th>QQ</th> \r\n						<td><input id=\"qq\" name=\"qq\" value=\"");
	templateBuilder.Append(userfield.qq.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th>ICQ</th> \r\n						<td><input id=\"icq\" name=\"icq\" value=\"");
	templateBuilder.Append(userfield.icq.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n					<tr>\r\n						<th width=\"100\">SKYPE</th> \r\n						<td><input id=\"skype\" name=\"skype\" value=\"");
	templateBuilder.Append(userfield.skype.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"30\" /></td>\r\n                        <td style=\"padding-top:1px;\"></td>\r\n					</tr>\r\n				</table>\r\n				<table width=\"100%\" cellspacing=\"0\" class=\"zform\">\r\n					<tr>\r\n                    	<th width=\"100\"></th>\r\n						<td style=\"padding-top:10px;\"><input name=\"saveprofile\" type=\"submit\" id=\"saveprofile\" value=\"提交\" class=\"button\"/></td>\r\n					</tr>\r\n				</table>\r\n                </form>\r\n            </div>\r\n            <span class=\"o1\"></span><span class=\"o2\"></span><span class=\"o3\"></span><span class=\"o4\"></span>\r\n        </div>\r\n		<script type=\"text/javascript\">\r\n		    $(function () {\r\n		        $(\"#realname\").focus();\r\n		        $(\".f_tab\").tabs(\".fill\", { event: 'click', current: 'now' });\r\n\r\n		        $.formValidator.initConfig({ formID: \"zform\", mode: \"FixTip\", onError: function (msg) { }, onSuccess: function () { saveProfileForm('#zform'); return false; } });\r\n		        $(\"#birthday\").click(function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', startDate: '1980-01-01', minDate: '1900-01-01', maxDate: '2017-1-1' }) }).formValidator({ onShow: \"输入的出生日期\", onFocus: \"输入的出生日期\", onCorrect: \"输入的日期合法\" }).inputValidator({ min: \"1900-01-01\", max: \"2018-12-31\", type: \"date\", onerror: \"日期必须在\\\"1900-01-01\\\"和\\\"2018-12-31\\\"之间\" }); ;\r\n		        $(\"#realname\").formValidator({ onShow: \"输入您的真实姓名\", onFocus: \"输入您的真实姓名\" }).inputValidator({ min: 1, max: 20, onError: \"真实姓名不能为空\" });\r\n		        $(\"#nickname\").formValidator({ onShow: \"给自己起一个昵称\", onFocus: \"给自己起一个昵称\" }).inputValidator({ min: 1, max: 20, onError: \"昵称不能为空\" });\r\n		        $(\"#areaname\").formValidator({ onFocus: \"输入地名\" }).inputValidator({ min: 1, max: 20, onError: \"好想知道你来自哪里\" });\r\n		        $(\"#signature\").formValidator({ onShow: \"一句话的个性签名\", onFocus: \"一句话的个性签名，控制在20个字符以内\" }).inputValidator({type:\"size\", min: 1, max: 30, onError: \"请正确设置个性签名\" });\r\n		        $(\"#website\").formValidator({ onShow: \"输入个人网站的访问地址\", onFocus: \"输入个人网站的访问地址\" });\r\n		    });\r\n        </");
	templateBuilder.Append("script>\r\n    </div>\r\n</div>\r\n");
	templateBuilder.Append("<div class=\"footer\">\r\n    <a href=\"#\" target=\"_blank\">关于我们</a> |  \r\n    <a href=\"#\" target=\"_blank\">联系方式</a> |  \r\n    <a href=\"#\" target=\"_blank\">版权声明</a> |  \r\n    <a href=\"#\" target=\"_blank\">招聘信息</a> |  \r\n    <a href=\"#\" target=\"_blank\">友情链接</a>\r\n    <p class=\"cp\">Powered by <strong><a href=\"http://www.stacms.com\" target=\"_blank\">STACMS</a></strong> <em>v1.0</em> &copy; 2013 <img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/copyright.gif\"/>\r\n    </p>\r\n</div>\r\n</body>\r\n</html>\r\n");


	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
