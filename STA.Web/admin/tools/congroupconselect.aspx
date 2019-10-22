<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="congroupconselect.aspx.cs" Inherits="STA.Web.Admin.Tools.congroupconselect" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>内容选择</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
    <div id="mwrapper" style="padding:0 7px 0 12px;overflow:hidden;">
        <div id="main" style="margin:0;overflow:hidden;">
            <div class="conb-1">
                <div class="bar">
                    内容搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                &nbsp; 频道：<cc1:DropDownTreeList runat="server" ID="ddlConType" Width="120"/>&nbsp;&nbsp; 状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                     <asp:ListItem Text="全部" Value="-1" Selected="True" />
                                     <asp:ListItem Text="草稿" Value="0" />
                                     <asp:ListItem Text="待审核" Value="1" />
                                     <asp:ListItem Text="通过" Value="2"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
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
                                发布用户：<cc1:TextBox ID="txtUsers" Width="90" HelpText="如果多个用户中间请用空格或半角逗号隔开" runat="server" />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 &nbsp; 发布时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />
                                &nbsp;&nbsp;关键字：<cc1:TextBox ID="txtKeywords" Width="150" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索内容" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;内容列表<span class="red" id="statustxt"></span></div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th>
                                        类型
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th>
                                        所属频道
                                    </th>
                                    <th>
                                        添加
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["typename"]%>
                                </td>
                                <td>
                                   <span class="ptip" onclick="window.open('view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&tid=<%#((DataRowView)Container.DataItem)["typeid"]%>&name=content','_blank')"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["channelname"]%>
                                </td>
                                <td>
                                    <a href="javascript:void(0)" onclick="AddCon(<%#((DataRowView)Container.DataItem)["id"]%>,<%#((DataRowView)Container.DataItem)["orderid"]%>);">添加</a>
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
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function AddCon(cid, oid) {
            var type = "<%=STARequest.GetString("type")%>";
             if(type == "spec"){
                 Ajax("addspecon&sid=<%=STARequest.GetString("sid")%>&cid="+cid,function(ret){
                    $("#statustxt").show().html((ret=="True"? "添加成功":"已在专题内")+"&nbsp;&nbsp;").fadeOut(1000);
                 });
             }else{
                 Ajax("addcongroupcon&gid=<%=STARequest.GetString("id")%>&cid="+cid+"&oid="+oid,function(ret){
                    $("#statustxt").show().html((ret=="True"? "添加成功":"已在内容组")+"&nbsp;&nbsp;").fadeOut(1000);
                 });
             }
        };
        RegColumnPostip(".ptip", 48, "..");
    </script>
</body>
</html>
