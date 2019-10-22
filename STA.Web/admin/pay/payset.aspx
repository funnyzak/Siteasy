<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payset.aspx.cs" Inherits="STA.Web.Admin.Pay.payset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>配置支付方式</title>
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
                <div class="bar">配置支付方式</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                是否启用：
                            </td>
                            <td>
                               <cc1:RadioButtonList runat="server" ID="rblIsvalid" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">启用</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">不启用</asp:ListItem>
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
                        <asp:Literal ID="ltrCtrs" runat="server"></asp:Literal>
                        <tr>
                            <td class="itemtitle">
                                支付按钮图片：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtPic"/>
                                <span id="selectimg" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                支付方式描述：
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
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保存支付设置 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'paylist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegPreviewImg("#txtPic", "#previewImg");
        RegSelectFilePopWin("selectimg", "支付按钮图片旋转", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtPic&watermark=no&rename=0", "click", $("#txtPic"));
    </script>
</body>
</html>