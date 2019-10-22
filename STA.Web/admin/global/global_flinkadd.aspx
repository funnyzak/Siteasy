<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_flinkadd.aspx.cs" Inherits="STA.Web.Admin.flinkadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>链接添加</title>
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
                <div class="bar">链接添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOrderid" Width="100" RequiredFieldType="数据校验" Text="0" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                网站名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                网站地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUrl" CanBeNull="必填"/>
                                <a href="javascript:void(0);" onclick="OpenInputLink('#txtUrl')">打开</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                联系邮箱：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEmail" RequiredFieldType="电子邮箱"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                所属分类：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlLinktype" />
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                网站LOGO：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImg"/>
                                <span id="selectimg" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                网站简介：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                状态：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblStatus" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">待审核</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">通过审核</asp:ListItem>
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
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 链 接 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_flinks.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegPreviewImg("#txtImg", "#previewImg");
        RegSelectFilePopWin("selectimg", "LOGO选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg&watermark=no&rename=0", "click", $("#txtImg"));
    </script>
</body>
</html>