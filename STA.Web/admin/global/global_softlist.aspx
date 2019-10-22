<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_softlist.aspx.cs"Inherits="STA.Web.Admin.softlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>所有软件列表</title>
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
                    软件搜索</div>
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
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索软件" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;软件列表</div>
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
                                   <span class="conimg" url="<%#(((DataRowView)Container.DataItem)["img"]).ToString().Trim()%>" onclick="location.href='global_softadd.aspx?url=<%=Utils.UrlEncode(currentpage)%>&action=edit&type=3&id=<%#((DataRowView)Container.DataItem)["id"]%>'">
                                        <img src="<%#(((DataRowView)Container.DataItem)["img"]).ToString().Trim()==""?"../images/nopic.png":(((DataRowView)Container.DataItem)["img"]).ToString().Trim()%>" width="80" height="50" onerror="this.src='../images/nopic.png'"/>
                                    </span>
                                </td>
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'EditBtn','MakeHtmlBtn','ViewComment')" name="cbid" value="<%#Eval("id")%>" />
                                    <img src="../images/trun.gif" style="vertical-align:middle;" title="编辑属性" id="fe<%#((DataRowView)Container.DataItem)["id"]%>" onclick="FastEditTr('../tools/fastcontentedit.aspx?type=3&id=', <%#((DataRowView)Container.DataItem)["id"]%>, 8, '100%', '170');"/>
                                    <span onclick="window.open('../tools/view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&tid=<%#((DataRowView)Container.DataItem)["typeid"]%>&name=content','_blank')" class="ptip"><%#((DataRowView)Container.DataItem)["title"]%></span><br />
                                    发布时间：<%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <span class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>"><%#((DataRowView)Container.DataItem)["channelname"]%></span><br />
                                    ID: <%#((DataRowView)Container.DataItem)["id"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["click"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["orderid"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["commentcount"]%>
                                </td>
                                <td>
                                    <span style="display:none;" id="contitle<%#((DataRowView)Container.DataItem)["id"]%>"><%#((DataRowView)Container.DataItem)["title"]%></span>
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
                <cc1:Button runat="server" ID="ViewComment" Enabled="false" AutoPostBack="false" Text="查看评论" />
                <cc1:Button ID="MakeHtmlBtn" runat="server" Enabled="false" AutoPostBack="false" Text="生成静态" />
                <cc1:Button ID="AddContentBtn" runat="server" ButtontypeMode="WithImage" AutoPostBack="false" ButtonImgUrl="../images/icon/add.gif" Text=" 软件添加"/>
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#AddContentBtn").click(function () { location.href = "global_softadd.aspx?type=3&url=<%=Utils.UrlEncode(currentpage)%>"; });
        $(".chlname").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类") } });
        EditForList("EditBtn", Ele("form1"), "global_softadd.aspx?url=<%=Utils.UrlEncode(currentpage)%>&action=edit&type=3&id=", true);
        EditForList("MakeHtmlBtn", Ele("form1"));
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'MakeHtmlBtn', 'ViewComment')
        }
        //RegColumnImg(".conimg");
        RegColumnPostip(".ptip", 36, "..");
        EditForList("ViewComment", Ele("form1"), "", true, function (id) { location.href = 'global_comments.aspx?contitle=' + encodeURI($("#contitle" + id).html()) });
        $("span[cstatus]").each(function () { $(this).html(ConStatus($(this).attr("cstatus"))); });
    </script>
</body>
</html>
