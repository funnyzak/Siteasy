<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_menuadd.aspx.cs" Inherits="STA.Web.Admin.menuadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>菜单添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
    var icons = [<%=iconStr%>];
    $(function () {
        $.each(icons, function (idx, obj) {
            $(".menuicons").append("<img src='../images/icon/" + obj + "' class='menuicon_' onclick=\"$('#txticon').val('" + obj + "');$('.menuicons').hide();\"/>");
        });
        $(".menuicons").position({ of: $("#txticon"), my: 'left top', at: "left bottom", offset: '0 1px', collision: "fit none" });
        $(".selecticon").click(function () {
            $(".menuicons img").each(function (idx, obj) {
                $(this).css("border-color", $("#txticon").val() != "" && $(this).attr("src").indexOf($("#txticon").val()) >= 0 ? "#333" : "#fff");
            });
            $(".menuicons").show();
        });
    });
    </script>
</head>
<body>
    <div class="menuicons" style="display:none"></div>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">菜单添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                系统菜单：
                            </td>
                            <td>
                                <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="3" Width="300" runat="server"
                                    ID="rblsystem">
                                    <asp:ListItem Text="是" Value="1" Selected="True"/>
                                    <asp:ListItem Text="否" Value="0" />
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                菜单类型：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" RepeatColumns="3" RepeatDirection="Horizontal" ID="rblPagetype" />
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                菜单名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtname" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                链接地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txturl"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                排序号：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtorderid" Text="0" Width="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                打开窗口：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txttarget" Text="main" Width="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                图标：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txticon" Width="100"/> <a href="javascript:;" class="selecticon">选择</a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 菜 单 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_menulist.aspx'" />
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