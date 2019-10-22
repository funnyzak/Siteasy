<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_voteoptionadd.aspx.cs" Inherits="STA.Web.Admin.voteoptionadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>投票选项添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">投票选项添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                投票主题：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" Enabled="false" ID="txtTopicname" CanBeNull="必填" Width="400"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                选项内容：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填" Width="400"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                选项描述：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDesc" Width="406" TextMode="MultiLine" Height="150"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOrderid" Width="50" Text="0" RequiredFieldType="数据校验" CanBeNull="必填"/>&nbsp;&nbsp;&nbsp;
                                初始票数：<cc1:TextBox runat="server" ID="txtCount" Width="80"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                选项图片：
                            </td>
                            <td>
			                    <cc1:TextBox runat="server" ID="txtImg"/>
                                <span id="selectimg" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <asp:Button ID="SaveInfo" runat="server" Text="保 存 返 回" CssClass="mbutton"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
     $("#txtDesc").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3" }));
     RegSelectFilePopWin("selectimg", "图片选择", "root=<%=filesavepath%>&path=<%=filesavepath%>" + "&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg", "click");
     RegPreviewImg("#txtImg", "#previewImg");
     $("#GoBack").click(function () { location.href = "global_voteitems.aspx?id=<%=STARequest.GetString("tid")%>"; });
    </script>
</body>
</html>