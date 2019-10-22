<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="voteadd.aspx.cs" Inherits="STA.Web.Admin.Plus.voteadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>投票添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
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
                <div class="bar">投票添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                标题：
                            </td>
                            <td>
                                <cc1:TextBox ID="txtTitle" runat="server"></cc1:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                开始日期：
                            </td>
                            <td>
                                 <cc1:TextBox runat="server" ID="txtStartDate"  Width="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                 结束日期：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEndDate" Width="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                是否多选：
                            </td>
                            <td>
                                <cc1:RadioButtonList ID="rblIsMore" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                    <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="否"  Selected="True"></asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                是否允许查看结果：
                            </td>
                            <td>
                                <cc1:RadioButtonList ID="rblIsView" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                    <asp:ListItem Value="1" Text="是" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                投票间隔天数：
                            </td>
                            <td>
                                <cc1:TextBox ID="txtInterval" HelpText="(N天后可再次投票，0 表示无限制)" Text="0" runat="server"></cc1:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                投票选项：
                            </td>
                            <td>
                                <cc1:TextBox ID="txtItems" HelpText="增加选项请回车换行,id不能重复,count为投票数" TextMode="MultiLine" Width="550" Height="170" runat="server"><item id='1' count='1'>选项内容</item></cc1:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                是否启用：
                            </td>
                            <td>
                                <cc1:RadioButtonList ID="rblIsEnable" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                    <asp:ListItem Value="1" Text="是" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 投 票 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'votelist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
    </script>
</body>
</html>