<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_voteadd.aspx.cs" Inherits="STA.Web.Admin.voteadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>发起投票</title>
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
                                投票分类：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlVotecates"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                投票类型：
                            </td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblType" epeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">普通</asp:ListItem>
                                    <asp:ListItem Value="2">图片</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                投票标题：
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
                                标识：<cc1:TextBox runat="server" ID="txtLikeid" Width="100" HelpText="可以用标识调用投票集合,或其他作用"/> <a href="javascript:;" id="likeids">已存在标识</a> <a href="javascript:;" id="previewlike">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                截至日期：
                            </td>
                            <td>
			                    <cc1:TextBox runat="server" ID="txtEndtime" CanBeNull="必填" Width="170" RequiredFieldType="日期时间"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                图片：
                            </td>
                            <td>
			                    <cc1:TextBox runat="server" ID="txtImg"/>
                                <span id="selectimg" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                单选/多选：
                            </td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblVtype" epeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">单选</asp:ListItem>
                                    <asp:ListItem Value="2">多选</asp:ListItem>
                                </cc1:RadioButtonList> 
                            </td>
                        </tr>
                        <tr id="smaxv">
                            <td class="itemtitle">
                                最多可选：
                            </td>
                            <td>
			                    <cc1:TextBox runat="server" ID="txtMaxvote" Width="70" Text="2"/> 项
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
                            开启验证码：
                        </td>
                        <td>
			                <cc1:RadioButtonList runat="server" ID="rblIsvcode" epeatDirection="Horizontal" RepeatColumns="3">
                                <asp:ListItem Value="2" Selected="True">按系统默认</asp:ListItem>
                                <asp:ListItem Value="1">开启</asp:ListItem>
                                <asp:ListItem Value="0">关闭</asp:ListItem>
                            </cc1:RadioButtonList> 
                        </td>
                    </tr>
                        <tr>
                            <td class="itemtitle">
                               开启投票登记：
                            </td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblIsinfo" epeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="2" Selected="True">按系统默认</asp:ListItem>
                                    <asp:ListItem Value="1">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList> 
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               开启登录投票：
                            </td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblIslogin" epeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="2" Selected="True">按系统默认</asp:ListItem>
                                    <asp:ListItem Value="1">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList> 
                            </td>
                        </tr>
                    <tr>
                        <td class="itemtitle">
                            投票详细说明：
                        </td>
                        <td>
                            <cc1:TextBox runat="server" ID="txtDesc" Width="550" TextMode="MultiLine" Height="130"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="itemtitle">
                            投票成功内容：
                        </td>
                        <td>
                            <cc1:TextBox runat="server" ID="txtVoted" Width="550" TextMode="MultiLine" Height="130"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="itemtitle">
                            投票结束内容：
                        </td>
                        <td>
                            <cc1:TextBox runat="server" ID="txtEndtext" Width="550" TextMode="MultiLine" Height="130"/>
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
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_votelist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
     $("#likeids").click(function () { RegisterPopInsertText($("#likeidlist").val(), "txtLikeid"); });
     $("#txtEndtime").click(function () { WdatePicker({ dateFmt:'yyyy-MM-dd HH:mm:ss'}) });
     $("#txtDesc,#txtEndtext,#txtVoted").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3" }));
     RegSelectFilePopWin("selectimg", "图片选择", "root=<%=filesavepath%>&path=<%=filesavepath%>" + "&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg", "click");
     RegPreviewImg("#txtImg", "#previewImg");
     $("input[name='rblVtype']").click(function () { $("#smaxv").css("display", $(this).val() == "2" ? "" : "none"); });
     $("input[name='rblVtype']:checked").trigger("click");
     $("#previewlike").click(function () {
         var likeid = $.trim($("#txtLikeid").val());
         if (likeid == "") return;
         window.open("../../sta/vote.aspx?display=html&vtype=like&relval=" + likeid, "_blank");
     });
    </script>
</body>
</html>