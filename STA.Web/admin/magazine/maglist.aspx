<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="maglist.aspx.cs"
    Inherits="STA.Web.Admin.magazinelist" %>

<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>杂志管理</title>
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
                        杂志搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    添加时间：<cc1:TextBox ID="txtStartDate" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" Width="70" runat="server" /> &nbsp; 
                                    状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                                <asp:ListItem Selected="True" Value="-1">全部</asp:ListItem>
                                                <asp:ListItem Value="1">正常</asp:ListItem>
                                                <asp:ListItem Value="0">关闭</asp:ListItem>
                                           </cc1:DropDownList>&nbsp;
                                    标识：<cc1:TextBox ID="txtLikeid" Width="150" runat="server" />&nbsp;&nbsp; 
                                    关键字：<cc1:TextBox ID="txtKeywords" Width="150" runat="server" />
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索杂志" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;杂志列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th style="text-align:center" width="110">
                                            封面
                                        </th>
                                        <th>
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 名称
                                        </th>
                                        <th>
                                           标识
                                        </th>
                                        <th>
                                           更新时间
                                        </th>
                                        <th>
                                           页数
                                        </th>
                                        <th>
                                           宽/高
                                        </th>
                                        <th>
                                            浏览数
                                        </th>
                                        <th>
                                            权重
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:center">
                                   <a class="conimg" url="<%#(((DataRowView)Container.DataItem)["cover"]).ToString().Trim()%>" href="magadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">
                                        <img src="<%#(((DataRowView)Container.DataItem)["cover"]).ToString().Trim()==""?"../images/nopic.png":(((DataRowView)Container.DataItem)["cover"]).ToString().Trim()%>" width="80" height="50" onerror="this.src='../images/nopic.png'"/>
                                    </a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'EditBtn', 'UpBtn', 'SetBtn', 'ViewBtn')" name="cbid" value="<%#Eval("id")%>" />
                                        <a onclick="window.open('../../magview.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>','_blank')" class="ptip"><%#((DataRowView)Container.DataItem)["name"]%></a><br />

                                        发布时间：<%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td likeid="<%#((DataRowView)Container.DataItem)["likeid"]%>"><%#((DataRowView)Container.DataItem)["likeid"]%></td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "updatetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["pages"]%>(页)
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["ratio"]).ToString().Split(',')[0]%>/<%#(((DataRowView)Container.DataItem)["ratio"]).ToString().Split(',')[1]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["click"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["orderid"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["status"]).ToString()=="1"?"正常":"关闭"%>
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除杂志" Enabled="false" />
                    <cc1:Button ID="EditBtn" runat="server" Text="编辑杂志" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="UpBtn" runat="server" Text="上传内容" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="SetBtn" runat="server" Text="编辑内容" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="ViewBtn" runat="server" Text="阅读杂志" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="Button6" runat="server" AutoPostBack="false" ButtontypeMode="WithImage"
                        ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='magadd.aspx'"
                        Text=" 创建杂志" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        //RegColumnImg(".conimg");
        EditForList("EditBtn", Ele("form1"), "magadd.aspx?action=edit&id=", true);
        EditForList("ViewBtn", Ele("form1"), "", true, function (id) { window.open("../../magview.aspx?id=" + id); });
        EditForList("UpBtn", Ele("form1"), "magups.aspx?action=edit&id=", true);
        EditForList("SetBtn", Ele("form1"), "magset.aspx?action=edit&id=", true);
        $("td[likeid]").each(function () {
            if ($.trim($(this).html()) == "") { $(this).html("无"); }
        });
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'EditBtn', 'UpBtn', 'SetBtn', 'ViewBtn')
        };
        RegColumnPostip(".ptip", 38, "..");
    </script>
</body>
</html>
