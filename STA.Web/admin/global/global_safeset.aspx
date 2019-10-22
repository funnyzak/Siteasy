<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_safeset.aspx.cs" Inherits="STA.Web.Admin.safeset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>安全控制</title>
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
                <div class="bar">安全控制</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                验证码开启：
                            </td>
                            <td>
                                <div style="width:300px;">
                                <cc1:CheckBoxList runat="server" ID="cblVcodemods" RepeatColumns="10" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">用户注册</asp:ListItem>
                                    <asp:ListItem Value="2">会员登录</asp:ListItem>
                                    <asp:ListItem Value="3">文档评论</asp:ListItem>
                                    <asp:ListItem Value="4">申请友链</asp:ListItem>
                                </cc1:CheckBoxList>
                                </div>
                            </td>
                        </tr>
<%--                        <tr>
                            <td class="itemtitle">
                                禁止访问IP列表：：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="TextBox1" Width="600" Height="80" TextMode="MultiLine"/>
                            </td>
                        </tr>--%>
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