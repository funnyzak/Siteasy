<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.User.ContentAdd" %>
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
	templateBuilder.Append("/sta/js/formvalidator/formValidator.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/formvalidator/formValidatorRegex.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/editor/ueditor/ueditor.config.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/editor/ueditor/ueditor.all.min.js\"></");
	templateBuilder.Append("script>\r\n    <div class=\"left right-container\">\r\n        <div class=\"con-box left col-1\">\r\n            <div class=\"bartitle clearfix\"><div class=\"bartit-picon left\" style=\"background-position:-47px -47px;\">&nbsp;</div><h3 class=\"disblock left pad\">");
	if (action=="add")
	{

	templateBuilder.Append("发表");
	}
	else
	{

	templateBuilder.Append("编辑");
	}	//end if
	templateBuilder.Append(ctinfo.name.ToString().Trim());
	templateBuilder.Append("</h3></div>\r\n            <div class=\"con-info\">\r\n                <form name=\"zform\" id=\"zform\" action=\"\" method=\"post\" enctype=\"multipart/form-data\">	\r\n				<table width=\"100%\" cellspacing=\"0\" class=\"zform\">\r\n					<tr>\r\n						<th width=\"100\">标题：</th> \r\n						<td><input id=\"title\" name=\"title\" value=\"");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"50\" /></td>\r\n                        <td width=\"340\" style=\"padding-top:1px;\"><div id=\"titleTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>标签(Tags)：</th> \r\n                        <td><input id=\"tags\" name=\"tags\" value=\"");
	templateBuilder.Append(tags.ToString());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"50\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"tagsTip\"></div></td>\r\n					</tr>\r\n					<tr>\r\n						<th>作者：</th> \r\n						<td><input id=\"author\" name=\"author\" value=\"");
	templateBuilder.Append(author.ToString());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"20\" /></td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"authorTip\"></div></td>\r\n					</tr>\r\n                    <tr>\r\n                        <th>所属频道：</th>\r\n                        <td>\r\n                            ");	string chlstr = ChlSelects("channelid",typeid,channelid.ToString());
	
	templateBuilder.Append("\r\n                            ");
	templateBuilder.Append(chlstr.ToString());
	templateBuilder.Append("\r\n                        </td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"channelidTip\"></div></td>\r\n                    </tr>\r\n					<tr>\r\n						<th>缩略图：</th> \r\n						<td colspan=\"2\">\r\n                            <input id=\"img\" name=\"img\" value=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" type=\"text\" class=\"input-text\" size=\"50\" />\r\n                            &nbsp;<span class=\"cblue bold\" style=\"width:30px\">上传</span>\r\n                            <span class=\"locup-span\" style=\"margin-left:-30px;\"><input name=\"conimg\" type=\"file\" size=\"1\" class='locup-input-file'/></span>\r\n                            <span class=\"previewimgbox\" style=\"margin-left:50px;\"></span>\r\n                        </td>\r\n					</tr>\r\n                    <tr>\r\n                        <th>详细内容：</th>\r\n                        <td colspan=\"2\">\r\n                            <textarea name=\"content\" id=\"editcontent\">");
	templateBuilder.Append(content.ToString());
	templateBuilder.Append("</textarea>\r\n                            <script type=\"text/javascript\">\r\n                                UE.getEditor('editcontent', {\r\n                                    initialFrameWidth: 650,\r\n                                    initialFrameHeight: 200,\r\n                                    toolbars: ue_deftoolbars,\r\n                                    imageUrl: \"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=ueditorimgup\",\r\n                                    imageManagerUrl: \"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=ueditorimgmanage\",\r\n                                    fileUrl: \"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=ueditorattup\" \r\n                                });\r\n                            </");
	templateBuilder.Append("script>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <th>验证码：</th>\r\n                        <td>\r\n                            <input id=\"vcode\" name=\"vcode\" value=\"\" type=\"text\" class=\"input-text\" size=\"15\" />  \r\n                            <img src=\"#\" id=\"vimg\" height=\"28\" style=\"cursor:pointer;\" />\r\n                        </td>\r\n                        <td style=\"padding-top:1px;\"><div id=\"vcodeTip\"></div></td>\r\n                    </tr>\r\n					<tr>\r\n                    	<th width=\"100\"></th>\r\n						<td style=\"padding-top:10px;\">\r\n                            <input name=\"savebtn\" type=\"submit\" id=\"savebtn\" value=\"保存");
	templateBuilder.Append(ctinfo.name.ToString().Trim());
	templateBuilder.Append("\" class=\"button\"/>\r\n                             <input name=\"goback\" type=\"submit\" id=\"goback\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/");
	templateBuilder.Append(ctinfo.listmod.ToString().Trim());
	templateBuilder.Append("'\" value=\"返回\" class=\"button\"/>\r\n                        </td>\r\n					</tr>\r\n				</table>\r\n                </form>\r\n            </div>\r\n            <span class=\"o1\"></span><span class=\"o2\"></span><span class=\"o3\"></span><span class=\"o4\"></span>\r\n        </div>\r\n		<script type=\"text/javascript\">\r\n		    $(function () {\r\n		        $(\"#title\").focus();\r\n		        $.formValidator.initConfig({ formID: \"zform\", mode: \"FixTip\", onError: function (msg) { }, onSuccess: function () { return subConForm(); } });\r\n		        $(\"#title\").formValidator({ onShow: \"请填写标题\", onFocus: \"请填写标题\" }).inputValidator({ min: 1, max: 100, onError: \"标题不能为空\" });\r\n		        $(\"#tags\").formValidator({ onShow: \"请填写标签,多个标签请用半角逗号隔开\", onFocus: \"请填写标签,多个标签请用半角逗号隔开\" }).inputValidator({ min: 1, max: 100, onError: \"标签不能为空\" });\r\n		        $(\"#vcode\").formValidator({ onShow: \"请填写验证码\", onFocus: \"请填写验证码\" }).inputValidator({ min: 4, max: 4, onError: \"验证码输入有误\" });\r\n		        $(\"#channelid\").formValidator({ onShow: \"请选择所属频道\", onFocus: \"请选择所属频道\" }).functionValidator({ fun: function (val) { if (val == \"0\") { return \"必须选择一个频道\" } } });\r\n		        $(\"#vimg\").click(function () { $(\"#vimg\").attr(\"src\", \"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/vcode.aspx?cookiename=staconadd&live=0&date=\" + new Date()); }).trigger(\"click\"); ;\r\n		        conImgUp(\"conimg\", function (path) { $(\"#img\").val(path).trigger(\"change\"); });\r\n		        regPreviewImgBox(\"#img\", \".previewimgbox\");\r\n		        function subConForm() {\r\n		            if (UE.getEditor('editcontent').getContent() == \"\") {\r\n		                $.dialog.alert(\"内容没有添加,还不能保存！\");\r\n		                return false;\r\n		            }\r\n		            return true;\r\n		        };\r\n                ");
	if (IsErr()&&ispost)
	{

	templateBuilder.Append("\r\n                $.dialog.alert(\"");
	templateBuilder.Append(msgtext.ToString());
	templateBuilder.Append("\");\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n		    });\r\n        </");
	templateBuilder.Append("script>\r\n    </div>\r\n</div>\r\n");
	templateBuilder.Append("<div class=\"footer\">\r\n    <a href=\"#\" target=\"_blank\">关于我们</a> |  \r\n    <a href=\"#\" target=\"_blank\">联系方式</a> |  \r\n    <a href=\"#\" target=\"_blank\">版权声明</a> |  \r\n    <a href=\"#\" target=\"_blank\">招聘信息</a> |  \r\n    <a href=\"#\" target=\"_blank\">友情链接</a>\r\n    <p class=\"cp\">Powered by <strong><a href=\"http://www.stacms.com\" target=\"_blank\">STACMS</a></strong> <em>v1.0</em> &copy; 2013 <img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/copyright.gif\"/>\r\n    </p>\r\n</div>\r\n</body>\r\n</html>\r\n");


	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
