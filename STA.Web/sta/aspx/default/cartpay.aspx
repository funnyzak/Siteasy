<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Cartpay" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>买家付款 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"买家付款,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\">\r\n<meta name=\"keywords\" content=\"买家付款,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\">\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/blockUI.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/global.js\"></");
	templateBuilder.Append("script>\r\n<style type=\"text/css\">\r\n.payment-info{padding:2px;border:2px #ccc solid; background-color:#fff;display:none;width:334px;height:304px;position:absolute;top:50%;left:50%;margin:-100px 0 0 -167px;}\r\n.payment-info .cont{width:330px; height:300px; border:2px #ccc solid; background:#fff; text-align:center}\r\n</style>\r\n</head>\r\n<body>          \r\n<div class=\"vt_wrapper\">\r\n    ");
	templateBuilder.Append("    <div class=\"header\">\r\n    	<div class=\"left\">\r\n            <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"  title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" target=\"_self\"><img title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a>\r\n        </div>\r\n        <div class=\"right cgrey\">\r\n            ");
	if (userid>0)
	{

	templateBuilder.Append("\r\n                您好,<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">");
	templateBuilder.Append(oluser.Nickname.ToString().Trim());
	templateBuilder.Append("</a>&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">个人中心</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/profile.aspx\">我的资料</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=loginout\">退出登录</a>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n                欢迎来到");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append(", 您可以&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx?returnurl=");templateBuilder.Append(Utils.UrlEncode(cururl));
	templateBuilder.Append("\">登录</a>&nbsp;或&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/register.aspx\">注册</a>\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n        </div>\r\n    </div>");

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	买家付款\r\n    </div>\r\n    <div class=\"votecon\">\r\n        <input type=\"hidden\" value=\"\" name=\"adrid\" id=\"adrid\"/>\r\n        <table class=\"step\">\r\n        	<tr>\r\n            	<td>下单购买</td>\r\n                <td class=\"cur\">买家付款</td>\r\n                <td>确认收货</td>\r\n                <td>完成交易</td>\r\n           </tr>\r\n        </table>\r\n        <table class=\"cart olist\">\r\n        	<tr class=\"caption\">\r\n            	<th>&nbsp;&nbsp;<b>订单已提交,请尽快付款</b></th>\r\n            </tr>\r\n            <tr>\r\n                <td>\r\n                	<div class=\"cartpay\">\r\n                    	<div class=\"cartpay-2\">\r\n                        	您的订单号为：<span style=\"color:#ff0000;font-weight:bold;font-size:16px;\">");
	templateBuilder.Append(info.Oid.ToString().Trim());
	templateBuilder.Append("</span>&nbsp;\r\n                            应付金额：<span style=\"color:#ff0000;font-weight:bold;font-size:16px;\">");
	templateBuilder.Append(info.Totalprice.ToString().Trim());
	templateBuilder.Append("</span>元&nbsp;\r\n                            支付方式：");
	templateBuilder.Append(pinfo.Name.ToString().Trim());
	templateBuilder.Append("\r\n                        </div>\r\n                        <div class=\"cartpay-2 clearfix\">\r\n                              <div class=\"left\">还差一步，请立即支付(请您在");
	templateBuilder.Append(config.Orderbackday.ToString().Trim());
	templateBuilder.Append("日内付清款项，否则订单会被自动取消)</div>\r\n                              <div class=\"right\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/orderdetail.aspx?orderno=");
	templateBuilder.Append(info.Oid.ToString().Trim());
	templateBuilder.Append("\" class=\"cblue bold\"  target=\"_blank\">查看订单状态</a>&nbsp;</div>\r\n                        </div>\r\n                        <div class=\"carpay-2 cartpay-3\">\r\n							请选择以下支付平台支付\r\n                            <div class=\"cartpay-2 gopay\">\r\n                            	");
	templateBuilder.Append(payurl.ToString());
	templateBuilder.Append("\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"cartpay-2 s14\">\r\n    						  完成支付后，您可以：<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/orderdetail.aspx?orderno=");
	templateBuilder.Append(info.Oid.ToString().Trim());
	templateBuilder.Append("\" class=\"s14\" target=\"_blank\">查看订单状态</a>　<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"s14\" >继续购物</a>\r\n                        </div>\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n<div class=\"payment-info\">\r\n    <div class=\"cont\">\r\n    <p style=\"margin:0; padding:10px; text-align:right; background:#f1f1f1\"><a href=\"javascript:;\" style=\"text-decoration:underline\" class=\"payment-info-close\">关闭</a></p>\r\n    <table width=\"300\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n      <tr>\r\n        <td width=\"100\" height=\"100\" align=\"center\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/cart_warn.gif\" width=\"49\" height=\"50\" /></td>\r\n        <td width=\"200\" style=\"font-size:14px; font-weight:bold; color:#000000; line-height:25px\" align=\"left\">请您在新打开的网上银行页面完成付款</td>\r\n      </tr>\r\n      <tr>\r\n        <td colspan=\"2\" style=\"line-height:25px\">付款完成前请不要关闭此窗口<br />\r\n          完成付款后请根据您的情况点击下列按纽</td>\r\n      </tr>\r\n      <tr>\r\n        <td height=\"30\" colspan=\"2\" align=\"center\"><input type=\"button\" name=\"button\" value=\"已完成付款\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/orderdetail.aspx?orderno=");
	templateBuilder.Append(info.Oid.ToString().Trim());
	templateBuilder.Append("'\"/> \r\n            <input type=\"button\" name=\"button1\" value=\"付款遇到问题\" onclick=\"location.href='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/orderdetail.aspx?orderno=");
	templateBuilder.Append(info.Oid.ToString().Trim());
	templateBuilder.Append("'\"/></td>\r\n      </tr>\r\n    </table>\r\n    </div>\r\n</div>\r\n<script type=\"text/javascript\">\r\n    $(\".gopay\").click(function () { $.blockUI({ message: $(\".payment-info\"), overlayCSS: { backgroundColor: '#fff', opacity: 0.5 }, css: { border: 'none', background: \"transparent\"} }) });\r\n    $(\".payment-info-close\").click(function () { $.unblockUI(); });\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
