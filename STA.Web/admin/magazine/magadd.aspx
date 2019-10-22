<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magadd.aspx.cs" Inherits="STA.Web.Admin.magazineadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>创建/编辑杂志</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="contab clearfix">
                <ul>
                    <li>基本信息</li>
                    <li>高级设置</li>
                </ul>
            </div>
            <div class="cont-3">
                <div class="box">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                杂志标题：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填" Width="400"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOrderid" Width="50" Text="0" RequiredFieldType="数据校验" CanBeNull="必填"/>&nbsp;&nbsp;&nbsp;
                                标识：<cc1:TextBox runat="server" ID="txtLikeid" Width="100" HelpText="可以用标识调用投票集合,或其他作用"/> <a href="javascript:;" id="likeids">已存在标识</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                封面图片：
                            </td>
                            <td>
			                    <cc1:TextBox runat="server" ID="txtImg"/>
                                <span id="selectimg" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                状态：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblStatus" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">关闭浏览</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">正常浏览</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                    </table>
            </div>
        </div>
        <div class="cont-3">
            <div class="box">
                <table>
                    <tr>
                        <td class="itemtitle">
                            浏览量：
                        </td>
                        <td>
                            <cc1:TextBox runat="server" ID="txtClick" Text="1" RequiredFieldType="数据校验" Width="100" />
                        </td>
                    </tr>
                    <tr>
                        <td class="itemtitle">
                            杂志尺寸：
                        </td>
                        <td>
                           <cc1:TextBox runat="server" ID="txtRation" HelpText="杂志宽高,填写格式如：614,450" CanBeNull="必填" Width="100" Text="614,450"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="itemtitle">
                            杂志详细说明：
                        </td>
                        <td>
                            <cc1:TextBox runat="server" ID="txtDesc" Width="550" TextMode="MultiLine" Height="170"/>
                        </td>
                    </tr>
                  
                </table>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <textarea id="likeidlist" runat="server" style="display:none;"/>
            <asp:Button ID="SaveInfo" runat="server" Text="保 存 返 回" CssClass="mbutton"/>
            <asp:Button runat="server" ID="NextStep" Text=" 下 一 步 "  CssClass="mbutton"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'maglist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#likeids").click(function () { RegisterPopInsertText($("#likeidlist").val(), "txtLikeid"); });
        $("#txtDesc").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3" }));
        RegSelectFilePopWin("selectimg", "封面图片选择", "root=<%=filesavepath%>&path=<%=filesavepath%>" + "&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg", "click");
        RegPreviewImg("#txtImg", "#previewImg");
    </script>
</body>
</html>