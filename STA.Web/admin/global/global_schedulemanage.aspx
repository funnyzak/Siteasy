<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_schedulemanage.aspx.cs" Inherits="STA.Web.Admin.schedulemanage" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>定时任务</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
            <cc1:PageInfo ID="PageInfo1" runat="server" Text="定时任务分为系统级和用户级两种.系统级任务不可禁用;添加的任务均为用户级,可以设置开启或禁用, 此功能适用于开发人员。 "/>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;定时任务</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            任务标识
                                        </th>
                                        <th>
                                            执行方式
                                        </th>
                                        <th>
                                            上次执行时间
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th>
                                            级别
                                        </th>
                                        <th width="100">
                                            操作
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["key"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["exetime"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["lastexecute"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["enable"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["issystemevent"]%>
                                    </td>
                                    <td>
                                        <a href="global_scheduleadd.aspx?action=edit&key=<%#((DataRowView)Container.DataItem)["key"]%>">编辑</a>
              <%--                          <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delpage', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>--%>
                                        <a href="javascript:;" onclick="SConfirm(function () { SubmitForm('exec', '<%#((DataRowView)Container.DataItem)["key"]%>'); }, '确认执行 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["key"])%></b> 吗？');">立即执行</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate">
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_scheduleadd.aspx'" Text=" 添加定时任务" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
