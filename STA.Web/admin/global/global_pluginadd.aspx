<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_pluginadd.aspx.cs" Inherits="STA.Web.Admin.pluginadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>扩展创建向导</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">扩展创建向导</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                扩展名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                联系邮件：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEmail" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                扩展作者：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtAuthor" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                发布时间：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtPubtime" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                官方网站：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOfficesite"/>
                                 <a href="javascript:void(0);" onclick="OpenInputLink('#txtOfficesite')">打开</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                后台管理菜单：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtMenu" Width="500" />
                                <div class="description">说明：格式如:name="留言本" url="guestbook.aspx" (url链接相对管理文件夹plus下)</div>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                扩展说明：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDescription" Width="506" TextMode="MultiLine" Height="130"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                数据库安装脚本：
                            </td>
                            <td>
                               <cc1:TextBox runat="server" ID="txtDbcreate" Width="500" TextMode="MultiLine" Height="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                数据库卸载脚本：
                            </td>
                            <td>
                               <cc1:TextBox runat="server" ID="txtDbdelete" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr style="display:none;" id="trfilelist">
                            <td class="itemtitle">
                                扩展包文件列表：
                            </td>
                            <td>
                               <cc1:TextBox runat="server" ID="txtFilelist" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                扩展文件包：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtPackage" HelpText="文件包只支持ZIP格式的文件,扩展包文件必须按标准格式存放否则不能正确安装" CanBeNull="必填"/> <span id="selectbag" class="selectbtn">选择</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidSetup" />
            <input type="hidden" runat="server" id="hidAction" />
            <asp:Button ID="SaveInfo" runat="server" Text="保 存 扩 展" CssClass="mbutton"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtPubtime").click(function () { WdatePicker({ isShowWeek: true }) });
        $("#txtDescription").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3" }));
        $("#trfilelist").css("display", $("#hidSetup").val() == "1" ? "" : "none");
        RegSelectFilePopWin("selectbag", "扩展文件包选择", "root=<%=filesavepath%>&path=<%=filesavepath%>,plus&filetype=zip&fullname=1&cltmed=1&fele=txtPackage&rename=0", "click");
    </script>
</body>
</html>