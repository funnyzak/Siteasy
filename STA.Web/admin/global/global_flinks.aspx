<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_flinks.aspx.cs"
    Inherits="STA.Web.Admin.flinks" %>

<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>链接管理</title>
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
                        链接搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    添加时间：<cc1:TextBox ID="txtStartDate" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" Width="70" runat="server" /> &nbsp; 
                                    状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                                <asp:ListItem Selected="True" Value="-1">全部</asp:ListItem>
                                                <asp:ListItem Value="1">通过</asp:ListItem>
                                                <asp:ListItem Value="0">待审核</asp:ListItem>
                                           </cc1:DropDownList>&nbsp;
                                    分类：<cc1:DropDownList runat="server" ID="ddlLinktype" />&nbsp;&nbsp; 
                                    关键字：<cc1:TextBox ID="txtKeywords" Width="150" runat="server" />
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索链接" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;链接列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            名称
                                        </th>
                                        <th>
                                            分类
                                        </th>
                                        <th>
                                            Email
                                        </th>
                                        <th>
                                            添加时间
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th>
                                            权重
                                        </th>
                                        <th width="100">
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" />
                                            选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <a href="<%#((DataRowView)Container.DataItem)["url"]%>" class="ptip" target="_blank"><%#((DataRowView)Container.DataItem)["name"]%></a>
                                    </td>
                                    <td clsid="<%#((DataRowView)Container.DataItem)["typeid"]%>">
                                    </td>
                                    <td>
                                        <a href="global_emailsend.aspx?email=<%#((DataRowView)Container.DataItem)["email"]%>" title="发送邮件"><%#((DataRowView)Container.DataItem)["email"]%></a>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["status"]).ToString()=="1"?"通过":"待审核"%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["orderid"]%>
                                    </td>
                                    <td>
                                        <a href="global_flinkadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('dellink', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','VerifyBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false" />
                    <cc1:Button ID="VerifyBtn" runat="server" Text="审核通过" Enabled="false" />
                    <cc1:Button ID="Button6" runat="server" AutoPostBack="false" ButtontypeMode="WithImage"
                        ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_flinkadd.aspx'"
                        Text=" 链接添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        EditForList("VerifyBtn", Ele("form1"));
        var types = [];
        $.each($("#ddlLinktype option"), function (i, o) { if (o.value != "0") { types.push(o); } });
        function GetClassName(cid) {
            if (cid == "0") return "未分类";
            for (var i = 0; i < types.length; i++) {
                if (types[i].value == cid) return types[i].text;
            }
            return "未分类";
        };
        $("td[clsid]").each(function () {
            $(this).html(GetClassName($(this).attr("clsid")));
        });
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'VerifyBtn')
        };
        RegColumnPostip(".ptip", 38, "..");
    </script>
</body>
</html>
