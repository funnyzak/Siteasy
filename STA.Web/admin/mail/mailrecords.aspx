<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mailrecords.aspx.cs" Inherits="STA.Web.Admin.mailrecords" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>邮件发送记录</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-1">
                    <div class="bar">
                        邮件发送搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    &nbsp;&nbsp; 
                                    发送时间：<cc1:TextBox ID="txtStartDate" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" Width="70" runat="server" /> &nbsp; &nbsp; 
                                    发送者：<cc1:TextBox ID="txtUsers" Width="150" runat="server" HelpText="如果多个用户中间请用空格或半角逗号隔开"/>&nbsp;&nbsp; 
                                    标题：<cc1:TextBox ID="txtTitle" Width="200" runat="server" />&nbsp;&nbsp; 
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索邮件" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;邮件发送记录</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           邮件标题
                                        </th>
                                        <th>
                                           发送时间
                                        </th>
                                        <th>
                                           发送者
                                        </th>
                                        <th width="150">
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["title"]%>
                                    </td>
                                    <td>
                                         <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                    <a href="/userinfo.aspx?id=<%#((DataRowView)Container.DataItem)["userid"]%>" target="_blank" title="查看用户信息">
                                    <%#((DataRowView)Container.DataItem)["username"]%></a>
                                    </td>
                                    <td>
                                        <a href="mailsend.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑重发</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delmail', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="title<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["title"]%>" />
                                        <input type="hidden" name="username<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["username"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='mailsend.aspx'" Text=" 发送订阅邮件 " />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
       
    </script>
</body>
</html>
