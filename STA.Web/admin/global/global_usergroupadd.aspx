<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_usergroupadd.aspx.cs" Inherits="STA.Web.Admin.usergroupadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>用户组添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/colorpicker/css/colorpicker.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script type="text/javascript" src="../plugin/scripts/colorpicker/js/colorpicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#txtColor').ColorPicker({
                color: $(this).val(),
                onChange: function (hsb, hex, rg) {
                    $("#txtColor").val(hex);
                   // $("#txtColor").css("background-color", "#"+hex);
                },
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).ColorPickerHide();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">用户组添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                               系统组：
                            </td>
                            <td>
                                <cc1:RadioButtonList ID="rblSysgroup" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               用户组名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               积分下限：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCreditsmin" Text="10" CanBeNull="必填" RequiredFieldType="数据校验"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               积分上限：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCreditsmax" Text="20" CanBeNull="必填" RequiredFieldType="数据校验"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               显示名颜色：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtColor" Text="000"/> 
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               星星数：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" Text="4" ID="txtStar" CanBeNull="必填" RequiredFieldType="数据校验"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 用 户 组 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>