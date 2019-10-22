<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="STA.Web.Admin.Plus.ctmvariable.add" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>自定义变量添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">自定义变量添加/修改</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                系统变量：
                            </td>
                            <td>
                                <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="3" Width="300" runat="server"
                                    ID="rblsystem">
                                    <asp:ListItem Text="是" Value="1"/>
                                    <asp:ListItem Text="否" Value="0" Selected="True"/>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                标识：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtLikeid" Width="100"/> <a href="javascript:;" id="likeids">已存在标识</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                变量名：
                            </td>
                            <td>
                                 <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                变量键名：
                            </td>
                            <td>
                                 <cc1:TextBox runat="server" ID="txtKey" CanBeNull="必填" ValidationExpression="\w+" RequiredFieldType="自定义" ErrorMsg="变量键名只能由数字、字母或下划线组成"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                变量描述：
                            </td>
                            <td>
                                 <cc1:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                变量值：
                            </td>
                            <td>
                                 <cc1:TextBox runat="server" ID="txtVal" TextMode="MultiLine" Height="150" Width="520"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <input type="hidden" runat="server" id="hidkey" />
            <textarea id="likeidlist" runat="server" style="display:none;"/>
            <asp:Button ID="SaveInfo" runat="server" Text="保 存 变 量" CssClass="mbutton"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'list.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#likeids").click(function () { RegisterPopInsertText($("#likeidlist").val(), "txtLikeid"); });
    </script>
</body>
</html>