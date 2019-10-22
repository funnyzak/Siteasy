<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fastcontentedit.aspx.cs" Inherits="STA.Web.Admin.Tools.fastcontentedit" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>文档快速编辑</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/colorpicker/css/colorpicker.css" type="text/css" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script type="text/javascript" src="../plugin/scripts/colorpicker/js/colorpicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper" style="0;margin:0;overflow:hidden;">
        <div id="main" style="margin:0;">
            <table>
                <tr>
                    <td class="itemtitle3">
                        文档标题：
                    </td>
                    <td>
                        <cc1:TextBox runat="server" ID="txtTitle" Width="300" />&nbsp;&nbsp;
                        短标题：<cc1:TextBox runat="server" ID="txtSubTitle" Width="200" />
                    </td>
                </tr>
                <tr>
                    <td class="itemtitle3">
                        文档所属频道：
                    </td>
                    <td>
                        <cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>
                    </td>
                </tr>
                <tr>
                    <td class="itemtitle3">
                        自定义属性：
                    </td>
                    <td>
                        <cc1:CheckBox runat="server" ID="cbP_h" Text="头条[h]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_r" Text="推荐[r]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_f" Text="幻灯[f]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_a" Text="特荐[a]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_s" Text="滚动[s]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_b" Text="加粗[b]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_i" Text="斜体[i]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_p" Text="图片[p]" Checked="false" />
                        <cc1:CheckBox runat="server" ID="cbP_j" Text="跳转[j]" Checked="false" />
                    </td>
                </tr>
                <tr>
                    <td class="itemtitle3">
                        文档标签：
                    </td>
                    <td>
                        <cc1:TextBox runat="server" ID="txtTags" />
                        (每个标签用','号分开)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 权重：<cc1:TextBox runat="server" ID="txtOrderId"
                            CanBeNull="必填" Text="0" HelpText="只能添数字,默认数字越大频道越靠前" RequiredFieldType="数据校验"
                            Width="50" />
                    </td>
                </tr>
            </table>
        <div class="navbutton" style="text-align:right;padding:0 50px 0 0;">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 "/>
            <cc1:Button runat="server" ID="CancelBtn" AutoPostBack="false" Text=" 取 消 "/>
        </div>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        var channels = [];
        $.each($("#ddlConType option"), function (i, o) { if (o.value != "0") { channels.push(o); } });
        RegChannelSelect("ddlConType");
        $("#CancelBtn").click(function () { 
            window.parent.$("#fe<%=STARequest.GetString("id")%>").trigger("click");
        });
    </script>
</body>
</html>