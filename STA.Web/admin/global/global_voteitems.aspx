<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_voteitems.aspx.cs"Inherits="STA.Web.Admin.voteitems" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>投票选项编辑</title>
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
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;投票主题：<span class="red"><%=cinfo.Name%></span></div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th>
                                        <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选项名
                                    </th>
                                    <th>
                                        票数
                                    </th>
                                    <th>
                                        权重
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                   <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#Eval("id")%>" /> <span class="ptip"><%#((DataRowView)Container.DataItem)["name"]%></span>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["count"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["orderid"]%>
                                </td>
                                <td width="100">
                                    <a href="global_voteoptionadd.aspx?tid=<%=STARequest.GetString("id")%>&action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                    <a title="删除" href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('deloption', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
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
                <cc1:Button ID="EditTopic" AutoPostBack="false" runat="server" Text=" 编辑主题 " />
                <cc1:Button ID="MulAdd" AutoPostBack="false" runat="server" Text=" 批量添加选项 " />
                <cc1:Button ID="AddBtn" AutoPostBack="false" runat="server" Text=" 添加投票选项 "/>
                <cc1:Button ID="Button2" AutoPostBack="false" runat="server" OnClientClick="location.href='global_votelist.aspx'" Text=" 返回投票管理 " />
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
        $("#MulAdd").click(function () { PopWindow("选项批量添加", "../tools/voteoptionmuladd.aspx?id="+<%=STARequest.GetString("id")%>, "",500, 203)});
        $("#AddBtn").click(function () {location.href="global_voteoptionadd.aspx?tid=<%=STARequest.GetString("id")%>";});
        $("#EditTopic").click(function () {location.href="global_voteadd.aspx?action=edit&id=<%=STARequest.GetString("id")%>";});
        RegColumnPostip(".ptip", 36, "..");
    </script>
</body>
</html>
