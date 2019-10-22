<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usermailselect.aspx.cs" Inherits="STA.Web.Admin.Tools.usermailselect" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>用户邮件选择</title>
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
                    用户搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                 性别：<cc1:DropDownList runat="server" ID="ddlGender">
                                     <asp:ListItem Text="请选择" Value="-1" />
                                     <asp:ListItem Text="男" Value="1" />
                                     <asp:ListItem Text="女" Value="0"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                     <asp:ListItem Text="请选择" Value="-1" />
                                     <asp:ListItem Text="未验证" Value="0" />
                                     <asp:ListItem Text="验证" Value="1"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 活动时间：<cc1:TextBox ID="txtActionstartdate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtActionenddate" RequiredFieldType="日期" Width="70" runat="server" />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                用户名：<cc1:TextBox ID="txtUsername" Width="100" runat="server" />&nbsp;&nbsp;
                                昵称：<cc1:TextBox ID="txtNickname" Width="100" runat="server" />&nbsp;&nbsp;
                                Email：<cc1:TextBox ID="txtEmail" Width="100" runat="server" />&nbsp;&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索用户" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;用户列表<span class="right red" id="statustxt"></span></div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th>
                                        用户名
                                    </th>
                                    <th>
                                        昵称
                                    </th>
                                    <th>
                                        注册时间
                                    </th>
                                    <th>
                                        Email
                                    </th>
                                    <th>
                                        添加
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["username"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["nickname"]%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval (Container.DataItem, "addtime","{0:yyyy-MM-dd}")%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["email"]%>
                                </td>
                                <td>
                                    <a href="javascript:void(0)" onclick="SetMail('<%#(((DataRowView)Container.DataItem)["email"]).ToString().Trim()%>')">添加</a>
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
        $("#txtRegStartDate,#txtRegEndDate,#txtActionstartdate,#txtActionenddate").click(function () { WdatePicker({ isShowWeek: true }) });
        function SetMail(mail) {
            if ($.trim(mail) == "") {
                StatusText("该用户没有填写邮件地址");
                return;
            }
            var mails = parent.$("#txtUsers").val();
            if (mails.indexOf(mail) >= 0) {
                StatusText("该用户邮件地址已经添加");
                return;
            }
            parent.AddMail(mail);
        }
        function StatusText(txt) {
            $("#statustxt").show().html(txt + "&nbsp;&nbsp;").fadeOut(1000);
        }
    </script>
</body>
</html>