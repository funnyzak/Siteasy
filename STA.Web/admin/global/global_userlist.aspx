<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_userlist.aspx.cs" Inherits="STA.Web.Admin.userlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>所有用户列表</title>
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
                    用户搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                 性别：<cc1:DropDownList runat="server" ID="ddlGender">
                                     <asp:ListItem Text="空" Value="-1" />
                                     <asp:ListItem Text="男" Value="1" />
                                     <asp:ListItem Text="女" Value="0"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                     <asp:ListItem Text="请选择" Value="-1" />
                                     <asp:ListItem Text="未验证" Value="0" />
                                     <asp:ListItem Text="验证" Value="1"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 注册时间：<cc1:TextBox ID="txtRegStartDate" Width="68" runat="server" /> - <cc1:TextBox ID="txtRegEndDate" Width="70" runat="server" />&nbsp;&nbsp;
                                 活动时间：<cc1:TextBox ID="txtActionstartdate" Width="68" runat="server" /> - <cc1:TextBox ID="txtActionenddate" Width="70" runat="server" />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                用户组：<cc1:DropDownList runat="server" ID="ddlUsergroup"/>&nbsp;&nbsp;
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
                    &nbsp;&nbsp;用户列表</div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list listc">
                                <tr>
<%--                                    <th>
                                        ID
                                    </th>--%>
                                    <th>
                                        用户名
                                    </th>
                                    <th>
                                        头像
                                    </th>
                                    <th>
                                        昵称/Email
                                    </th>
                                    <th>
                                        性别
                                    </th>
                                    <th>
                                        会员级别
                                    </th>
                                    <th>
                                        注册时间
                                    </th>
                                    <th>
                                        状态
                                    </th>
                                    <th>
                                        最后登录
                                    </th>
                                    <th width="90">
                                        操作
                                    </th>
                                    <th width="40">
                                        选择
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
<%--                                <td>
                                    <%#((DataRowView)Container.DataItem)["id"]%>
                                </td>--%>
                                <td>
                                    <a href="/userinfo.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>" target="_blank" title="查看用户信息">
                                    <%#((DataRowView)Container.DataItem)["username"]%></a>
                                </td>
                                <td>
                                    <a href="global_useredit.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>"><img src="<%#STA.Core.Avatars.GetAvatarUrl(STA.Common.TypeParse.StrToInt(((DataRowView)Container.DataItem)["id"]),STA.Core.AvatarSize.Small)%>" alt="" width="45" height="45" onerror="this.src='../../sta/pics/avator/noavatar_medium.gif'"/></a>
                                </td>
                                <td>
                                    昵称:<%#((DataRowView)Container.DataItem)["nickname"]%><br />
                                    <%#((DataRowView)Container.DataItem)["email"]%>
                                </td>
                                <td>
                                    <%#(((DataRowView)Container.DataItem)["gender"]).ToString()=="1"?"男":"女"%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["groupname"]%><br />
                                    积分：<%#((DataRowView)Container.DataItem)["credits"]%> 金币：<%#((DataRowView)Container.DataItem)["extcredits1"]%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                   <%#(((DataRowView)Container.DataItem)["ischeck"]).ToString()=="1"?"已验证":"未验证"%><br />
                                   <%#(((DataRowView)Container.DataItem)["locked"]).ToString()=="1"?"锁定":"正常"%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "logintime", "{0:yyyy-MM-dd HH:mm:ss}")%><br />
                                    <%#((DataRowView)Container.DataItem)["loginip"]%>
                                </td>
                                <td>
                                    <a href="global_useredit.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                    <a href="global_contentlist.aspx?users=<%#(((DataRowView)Container.DataItem)["username"]).ToString().Trim()%>">文档</a>
                                    <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('deluser', <%#((DataRowView)Container.DataItem)["id"]%>); }, '用户的相关数据将全部清空，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["username"])%></b> 吗？');">删除</a>
                                </td>
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'id','DelBtn','LockBtn','UnLockBtn','SendEmailBtn', 'SendPmsBtn', 'VerifyOk')" name="id" value="<%#Eval("id")%>" />
                                    <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["username"]%>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <input type="hidden" id="hidAction" runat="server" value="" />
                    <input type="hidden" id="hidValue" runat="server" value="" />
                     <ucl:PageGuide ID="pGuide" runat="server" />
                </div>
            </div>
            <div class="operate">
                <cc1:Button runat="server" ID="DelBtn" Text=" 删除用户 " Enabled="false" 
                OnClientClick="ControlPostBack('DelBtn', '您选择的用户的相关数据将全部清空，确认删除吗？');return;"/>
                <cc1:Button runat="server" ID="LockBtn" Text=" 锁定用户 " Enabled="false" 
                OnClientClick="ControlPostBack('LockBtn', '锁定用户后，账户将不能使用，请确认？');return;"/>
                <cc1:Button runat="server" ID="UnLockBtn" Text=" 解除锁定 " Enabled="false" />
                <cc1:Button runat="server" ID="VerifyOk" Text=" 验证通过 " Enabled="false" OnClientClick="ControlPostBack('VerifyOk', '请确认？');return;"/>
                <cc1:Button runat="server" ID="SendEmailBtn" Text=" 发送邮件 " Enabled="false" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/email.gif"/>
                <cc1:Button runat="server" ID="SendPmsBtn" Text=" 发送站内信 " Enabled="false" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/comment.gif"/>
                <cc1:Button ID="AddBtn" OnClientClick="location.href='global_useradd.aspx';" AutoPostBack="false" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Text=" 用户添加 " />
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtRegStartDate,#txtRegEndDate,#txtActionstartdate,#txtActionenddate").click(function () { WdatePicker({ isShowWeek: true }) });
        EditForList("SendEmailBtn", Ele("form1"), "global_emailsend.aspx?uid=");
        EditForList("SendPmsBtn", Ele("form1"), "global_sendpmsingle.aspx?uid=");
        function CheckAll(form, checked) {
            CheckByName(form, 'id', checked);
            CheckedEnabledButton(form, 'id', 'DelBtn', 'LockBtn', 'UnLockBtn', 'SendEmailBtn', 'SendPmsBtn', 'VerifyOk')
        }
    </script>
</body>
</html>
