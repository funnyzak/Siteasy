<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="slideimgadd.aspx.cs" Inherits="STA.Plus.Admin.slideimgadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>幻灯图片添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">幻灯图片添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                标题：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTitle" CanBeNull="必填" Width="420"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                大图：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImg" CanBeNull="必填" Width="420"/> 
                                <span id="selectimgfile" class="selectbtn">选择</span> <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                小图：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtThumb" Width="420"/> 
                                <span id="selectthumbfile" class="selectbtn">选择</span> <a href="javascript:;" id="previewThumb">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                链接：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUrl" CanBeNull="必填" Width="420"/>
                                <a href="javascript:void(0);" onclick="if($.trim($('#txtUrl').val())==''){return}window.open($('#txtUrl').val(),'_blank')">打开</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                描述：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtText" TextMode="MultiLine" Height="70" Width="420"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOrderid" Width="100" Text="0" RequiredFieldType="数据校验" CanBeNull="必填"/>&nbsp;&nbsp;
                                标识：<cc1:TextBox runat="server" ID="txtLikeid" Width="100" />
                                 <a href="javascript:;" id="likeids">已存在标识</a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <textarea id="likeidlist" runat="server" style="display:none;"/>
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 图 片 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'slideimglist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function RegSelectFilePopWin2(triggerid, title, query, trigger, bindele, pos) {
            var popwin = function () { PopWindow((title || "文件浏览器"), "../../tools/selectfile.aspx?" + query, "", 630, 300, bindele, pos); };
            $("#" + triggerid).bind((trigger || "dblclick"), popwin);
        };
        RegPreviewImg("#txtImg", "#previewImg");
        RegPreviewImg("#txtThumb", "#previewThumb");
        RegSelectFilePopWin2("selectimgfile", "图片选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg", "click", $("#txtImg"));
        RegSelectFilePopWin2("selectthumbfile", "缩略图选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtThumb", "click", $("#txtThumb"));
        $("#likeids").click(function () { RegisterPopInsertText($("#likeidlist").val(), "txtLikeid"); });
    </script>
</body>
</html>