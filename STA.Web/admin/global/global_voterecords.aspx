<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_voterecords.aspx.cs" Inherits="STA.Web.Admin.voterecords" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>投票记录</title>
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
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
            <div class="conb-1">
                <div class="bar">
                    记录搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                 发布时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />&nbsp;&nbsp;
                                投票IP：<cc1:TextBox ID="txtIp" Width="120" runat="server" />&nbsp;&nbsp;
                                身份证号：<cc1:TextBox ID="txtIdcard" Width="120" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                主题ID：<cc1:TextBox ID="txtTopicids" Width="30" runat="server" />&nbsp;&nbsp;
                                电话：<cc1:TextBox ID="txtPhone" Width="100" runat="server" />&nbsp;&nbsp;
                                关键字：<cc1:TextBox ID="txtKeywords" Width="200" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索记录" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;投票记录</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            主题
                                        </th>
                                        <th>
                                            投票时间
                                        </th>
                                        <th>
                                            IP
                                        </th>
                                        <th>
                                            用户
                                        </th>
                                        <th>
                                            其他信息
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <span class="ctip"><%#((DataRowView)Container.DataItem)["topicname"]%></span>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["userip"]%>
                                    </td>
                                    <td>
                                         <span class="uname" title="<%#((DataRowView)Container.DataItem)["username"]%>" uid="<%#((DataRowView)Container.DataItem)["userid"]%>"><%#((DataRowView)Container.DataItem)["username"]%></span>
                                    </td>
                                    <td>
                                        <span class="infotip" rname="<%#((DataRowView)Container.DataItem)["realname"]%>" idcard="<%#((DataRowView)Container.DataItem)["idcard"]%>" phone="<%#((DataRowView)Container.DataItem)["phone"]%>"></span>
                                    </td>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                <div class="operate">
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除所选" Enabled="false"/>
                    <cc1:Button ID="EmptyBtn" runat="server" OnClientClick="ControlPostBack('EmptyBtn', '确认清空符合当前条件的记录吗?');return;" Text="清空当前" />&nbsp;
                    <cc1:Button ID="EmptyAllBtn" runat="server" OnClientClick="ControlPostBack('EmptyAllBtn', '确认所有记录吗?');return;" Text="全部清空" />&nbsp;
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
        RegColumnPostip(".ctip", 36, "..");

        $(".uname").each(function () {
            var uid = $.trim($(this).attr("uid")), name = $(this).html();
            if (parseInt(uid) > 0) {
                $(this).html("<a href='javascript:;' target='_blank'>" + name + "</a>");
            }
        });

        $("span[phone]").each(function () {
            var rn = $(this).attr("rname"), idcard = $(this).attr("idcard"), phone = $(this).attr("phone"), str = "";
            if ($.trim(rn) != "") str += "姓名:" + rn;
            if ($.trim(idcard) != "") str += "&nbsp;身份证号：" + idcard;
            if ($.trim(phone) != "") str += "&nbsp;手机：" + phone;
            if (str == "") str = "无";
            $(this).html(str);
        });
        RegColumnPostip(".infotip", 36, "..");

        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        };
    </script>
</body>
</html>
