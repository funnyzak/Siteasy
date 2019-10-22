<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_userregset.aspx.cs" Inherits="STA.Web.Admin.userregset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>用户注册设置</title>
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
                <div class="bar">用户注册设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">开放注册</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblOpenreg" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开放</asp:ListItem>
                                    <asp:ListItem Value="2">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">选择"关闭"将禁止新用户注册, 但不影响过去已注册的会员的使用</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">允许同一Email注册不同用户</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblEmailmultuser" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">允许</asp:ListItem>
                                    <asp:ListItem Value="0">禁止</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">选择"禁止" ,一个 Email 地址只能注册一个用户名</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">用户名保留关键字</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtForbiduserwords" Height="50" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">用户名称保留关键字,保留关键字不允许客户端注册，每个用半角逗号隔开</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">用户验证方式</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblUserverifyway" RepeatDirection="Horizontal" RepeatColumns="5">
                                    <asp:ListItem Value="1">人工审核</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Email 验证</asp:ListItem>
                                    <asp:ListItem Value="0">无验证</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">"Email验证"会向用户注册 Email 发送一封验证邮件以确认邮箱的有效性;选择"人工审核"将由管理员人工逐个审核新用户</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">Email验证邮件内容</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtUserverifyemailcontent" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">{username}表用户名，{weburl}表网站地址，{webname}表网站名称</td>
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