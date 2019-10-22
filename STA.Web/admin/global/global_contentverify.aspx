<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contentverify.aspx.cs"Inherits="STA.Web.Admin.contentverify" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>待审核文档</title>
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
                    文档搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                频道：<cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>&nbsp;&nbsp; 状态：<cc1:DropDownList runat="server" ID="ddlStatus">
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
                                 发布时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                发布用户：<cc1:TextBox ID="txtUsers" Width="120" HelpText="如果多个用户中间请用空格或半角逗号隔开" runat="server" />&nbsp;&nbsp;
                                关键字：<cc1:TextBox ID="txtKeywords" Width="270" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索内容" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;待审核文档</div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th>
                                        <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" />
                                    </th>
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
                                        发布时间
                                    </th>
                                    <th>
                                        点击
                                    </th>
                                    <th>
                                        发布用户
                                    </th>
                                    <th>
                                       HTML
                                    </th>
                                    <th width="100">
                                        操作
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr tid="<%#((DataRowView)Container.DataItem)["id"]%>">
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','VerifyBtn','VerifyNo')" name="cbid" value="<%#Eval("id")%>" />
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["typename"]%>
                                </td>
                                <td>
                                    <span class="ptip"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                </td>
                                <td class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>">
                                    <%#((DataRowView)Container.DataItem)["channelname"]%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["click"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["addusername"]%>
                                </td>
                                <td>
                                    <%#(((DataRowView)Container.DataItem)["ishtml"]).ToString()=="1"?"已生成":"未生成"%>
                                </td>
                                <td>
                                    <a href="javascript:;" onclick="SubmitForm('verify', <%#((DataRowView)Container.DataItem)["id"]%>);">通过</a>
                                    <a href="javascript:;" onclick="SubmitForm('verifyno', <%#((DataRowView)Container.DataItem)["id"]%>);">退稿</a>
                                    <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delcon', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
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
                <cc1:Button runat="server" Enabled="false" ID="DelBtn" AutoPostBack="false" OnClientClick="ControlPostBack('DelBtn','删除后不可恢复，确认删除吗？');return;" Text="删除" />
                <cc1:Button runat="server" ID="VerifyBtn" Text="审核通过" Enabled="false"/>
                <cc1:Button runat="server" ID="VerifyNo" Text="退稿" Enabled="false"/>
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'VerifyBtn', 'VerifyNo')
        }
        RegColumnPostip(".ptip", 36, "..");
        $(".chlname").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类") } });
    </script>
</body>
</html>
