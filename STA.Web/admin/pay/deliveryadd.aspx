<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deliveryadd.aspx.cs" Inherits="STA.Web.Admin.Pay.deliveryadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>添加/编辑配送方式</title>
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
                <div class="bar">添加/编辑配送方式</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                支持货到付款：
                            </td>
                            <td>
                               <cc1:RadioButtonList runat="server" ID="rblIscod" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">支持</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">不支持</asp:ListItem>
                               </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOrderid" Width="100" Text="0" RequiredFieldType="数据校验" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                英文名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEname"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                手续费：
                            </td>
                            <td>
                                 首重<cc1:TextBox runat="server" ID="txtFweight" RequiredFieldType="数据校验" Width="30" Text="1.00"/>千克,
                                 费用<cc1:TextBox runat="server" ID="txtFwmondy" RequiredFieldType="金额" Width="30" Text="15.00"/>元&nbsp;
                                 续重<cc1:TextBox runat="server" ID="txtCweight" RequiredFieldType="数据校验" Width="30" Text="1.00"/>千克,
                                 费用<cc1:TextBox runat="server" ID="txtCmondy" RequiredFieldType="金额" Width="30" Text="10.00"/>元&nbsp;
                                 满<cc1:TextBox runat="server" ID="txtFree" RequiredFieldType="金额" Width="50" Text="999"/>元免运费
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                描述：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="70" Width="500" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保存配送方式 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'deliverylist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
    </script>
</body>
</html>