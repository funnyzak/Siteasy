<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_interactset.aspx.cs" Inherits="STA.Web.Admin.interactset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>互动设置</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">互动设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">评论审核</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblCommentverify" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">用户发表的评论是否需要评论后再显示，如果关闭评论将直接显示</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">非会员限制</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblCommentlogin" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">限制</asp:ListItem>
                                    <asp:ListItem Value="0">不限制</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">是否只有登陆会员才可以评论,不限制则任何人都可以发表评论</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">评论长度</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="3000" ID="txtCommentlength" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc" title="Templatesavedirname">评论所允许的字符长度,英文字母,半角符号等为1个字节,汉字为两个字节.超过限定长度，将会被截取.0则不限制. 单位(字节)</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">回复楼层</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="30" ID="txtCommentfloor" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc" title="Templatesavedirname">评论回复所允许的最大楼层数(回复嵌套数);0为不限制</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">评论间隔</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="0" ID="txtCommentinterval" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc" title="Templatesavedirname">两次评论至少间隔时间，0为不限制. 防止用户恶意刷屏. 单位(秒)</td>
	                    </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 配 置 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>