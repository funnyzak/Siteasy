<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contentlist.aspx.cs"Inherits="STA.Web.Admin.contentlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>所有<%=ctinfo.Name%>列表</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">
                    <%=ctinfo.Name%>搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                频道：<cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>&nbsp;&nbsp; 状态：<cc1:DropDownList runat="server" ID="ddlStatus"/>&nbsp;&nbsp;
                                属性：<cc1:DropDownList runat="server" ID="ddlProperty">
                                     <asp:ListItem Text="全部" Value="" Selected="True"/>
                                     <asp:ListItem Text="头条" Value="h"/>
                                     <asp:ListItem Text="推荐" Value="r"/>
                                     <asp:ListItem Text="幻灯" Value="f" />
                                     <asp:ListItem Text="特荐" Value="a" />
                                     <asp:ListItem Text="滚动" Value="s" />
                                     <asp:ListItem Text="加粗" Value="b" />
                                     <asp:ListItem Text="斜体" Value="i" />
                                     <asp:ListItem Text="图片" Value="p" />
                                     <asp:ListItem Text="跳转" Value="j" />
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 发布时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                发布用户：<cc1:TextBox ID="txtUsers" Width="120" HelpText="如果多个用户中间请用空格或半角逗号隔开" runat="server" />&nbsp;&nbsp;
                                关键字：<cc1:TextBox ID="txtKeywords" Width="270" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索文档" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;<%=ctinfo.Name%>列表</div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="text-align:center" width="110">
                                        图片
                                    </th>
                                    <th>
                                       <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 标题
                                    </th>
                                    <th>
                                        频道
                                    </th>
                                    <th>
                                        点击
                                    </th>
                                    <th>
                                        权重
                                    </th>
                                    <th>
                                        评论
                                    </th>
                                    <th>
                                        发布/编辑用户
                                    </th>
                                    <th>
                                       状态
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr tid="<%#((DataRowView)Container.DataItem)["id"]%>">
                                <td style="text-align:center">
                                   <a class="conimg" url="<%#(((DataRowView)Container.DataItem)["img"]).ToString().Trim()%>" href="<%#GetConAddPage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"],2))%>&action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>&url=<%=Utils.UrlEncode(currentpage)%>">
                                        <img src="<%#(((DataRowView)Container.DataItem)["img"]).ToString().Trim()==""?"../images/nopic.png":(((DataRowView)Container.DataItem)["img"]).ToString().Trim()%>" width="80" height="50" onerror="this.src='../images/nopic.png'"/>
                                    </a>
                                </td>
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid', 'DelBtn','MakeHtmlBtn','ViewComment','DelHtmlBtn','EditBtn')" name="cbid" value="<%#Eval("id")%>" />
                                    <img src="../images/trun.gif" style="vertical-align:middle;" alt="编辑属性" id="fe<%#((DataRowView)Container.DataItem)["id"]%>" onclick="FastEditTr('../tools/fastcontentedit.aspx?type=2&id=', <%#((DataRowView)Container.DataItem)["id"]%>, 8, '100%', '170');"/>

                                    <a href="../tools/view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&tid=<%#((DataRowView)Container.DataItem)["typeid"]%>&name=content" class="ptip" target="_blank"><%#((DataRowView)Container.DataItem)["title"]%></a><br />

                                    <span title="发布时间：<%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>">更新时间：<%#DataBinder.Eval(Container.DataItem, "updatetime", "{0:yyyy-MM-dd HH:mm:ss}")%></span>
                                </td>
                                <td>
                                    <span class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>"><%#((DataRowView)Container.DataItem)["channelname"]%></span><br />
                                    ID: <%#((DataRowView)Container.DataItem)["id"]%>
                                </td>
                                <td>
                                    <span style="display:none;" id="contitle<%#((DataRowView)Container.DataItem)["id"]%>"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                    <%#((DataRowView)Container.DataItem)["click"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["orderid"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["commentcount"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["addusername"]%><br />
                                    <%#((DataRowView)Container.DataItem)["lasteditusername"]%>
                                </td>
                                <td>
                                    <span cstatus="<%#((DataRowView)Container.DataItem)["status"]%>"></span>
                                    <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["title"]%>" />
                                    <input type="hidden" name="type<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["typeid"]%>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                     <ucl:PageGuide ID="pGuide" runat="server" />
                </div>
            </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
            <div class="operate">
                <cc1:Button runat="server" Enabled="false" ID="DelBtn" AutoPostBack="false" OnClientClick="ControlPostBack('DelBtn','确认删除吗？');return;" Text="删除" />
                <cc1:Button runat="server" Enabled="false" ID="EditBtn" AutoPostBack="false" Text="编辑" />
                <cc1:Button runat="server" ID="VerifyContentBtn" Text="审核通过" Visible="false" Enabled="false" />
                <cc1:Button runat="server" ID="ViewComment" Enabled="false" AutoPostBack="false" Text="查看评论" />
                <cc1:Button ID="MakeHtmlBtn" runat="server" Enabled="false" AutoPostBack="false" Text="生成静态" />
                <cc1:Button ID="DelHtmlBtn" runat="server" Enabled="false" Text="删除静态文件" OnClientClick="ControlPostBack('DelHtmlBtn','确认删除静态文件吗？');return;" />
                <cc1:Button ID="AddContentBtn" runat="server" ButtontypeMode="WithImage" AutoPostBack="false" ButtonImgUrl="../images/icon/add.gif" Text=" 新增文档 "/>
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#AddContentBtn").click(function () { location.href = "<%=GetConAddPage(ctinfo.Id)%>&url=<%=Utils.UrlEncode(currentpage)%>"; });
        EditForList("EditBtn", Ele("form1"), "<%=GetConAddPage(ctinfo.Id)%>&url=<%=Utils.UrlEncode(currentpage)%>&action=edit&id=", true);
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        EditForList("MakeHtmlBtn", Ele("form1"));
        EditForList("VerifyContentBtn", Ele("form1"));
        EditForList("ViewComment", Ele("form1"), "", true, function (id) { location.href = 'global_comments.aspx?contitle=' + encodeURI($("#contitle" + id).html()) });
        RegColumnPostip(".ptip", 36, "..");
        $(".chlname").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类") } });
        $("span[cstatus]").each(function () { $(this).html(ConStatus($(this).attr("cstatus"))); });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'MakeHtmlBtn', 'ViewComment', 'DelHtmlBtn', 'EditBtn')
        };

        $("#btnSearch").text(" 搜索<%=ctinfo.Name%> ");
        $("#AddContentBtn").html(" <img src='../images/icon/add.gif'/> 新增<%=ctinfo.Name%> ");
    </script>
</body>
</html>
