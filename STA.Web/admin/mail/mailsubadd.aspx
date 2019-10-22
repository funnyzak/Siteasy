<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mailsubadd.aspx.cs" Inherits="STA.Web.Admin.mailsubadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>邮件订阅添加/编辑</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">邮件订阅添加/编辑</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                订阅分组：
                            </td>
                            <td>
                               <cc1:TextBox runat="server" ID="txtForgroup" Width="100" HelpText="为订阅用户分组" CanBeNull="必填" Text="默认"/> <a href="javascript:;" id="groups">已存在组</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                订阅人：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                邮件地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEmail" RequiredFieldType="电子邮箱" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                安全码：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSafecode" HelpText="安全码用来进行订阅验证,如不填则系统自动生成"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                订阅状态：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblStatus" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">取消</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <textarea id="grouplist" runat="server" style="display:none;"/>
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 订 阅 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'mailsubcribe.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#groups").click(function () { RegisterPopInsertText($("#grouplist").val(), "txtForgroup"); });
    </script>
</body>
</html>