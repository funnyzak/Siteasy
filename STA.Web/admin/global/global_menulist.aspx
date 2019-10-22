<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_menulist.aspx.cs" Inherits="STA.Web.Admin.menulist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>菜单管理</title>
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
            <div class="conb-1">
                <div class="bar">
                    菜单搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                系统：<cc1:DropDownList runat="server" ID="ddlSystem">
                                            <asp:ListItem Selected="True" Value="-1">全部</asp:ListItem>
                                            <asp:ListItem Value="1">系统</asp:ListItem>
                                            <asp:ListItem Value="0">用户</asp:ListItem>
                                      </cc1:DropDownList>
                                &nbsp;&nbsp;
                                页类型:<cc1:DropDownList runat="server" ID="ddlPageType"/>&nbsp;&nbsp;
                                关键字：<cc1:TextBox ID="txtKeywords" Width="250" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索菜单" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;菜单管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            菜单名
                                        </th>
                                        <th>
                                            菜单类型
                                        </th>
                                        <th>
                                            上级菜单
                                        </th>
                                        <th>
                                            打开方式
                                        </th>
                                        <th>
                                            系统
                                        </th>
                                        <th>
                                            权重
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
                                        <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td>
                                         <%#((STA.Entity.PageType)TypeParse.StrToInt(((DataRowView)Container.DataItem)["pagetype"])).ToString()%>
                                    </td>
                                    <td>
                                        <span parent="<%#((DataRowView)Container.DataItem)["parentid"]%>"></span>
                                    </td>
                                    <td>
                                         <%#((DataRowView)Container.DataItem)["target"]%>
                                    </td>
                                    <td>
                                       <%#(((DataRowView)Container.DataItem)["system"]).ToString()=="1"?"是":"否"%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["orderid"]%>
                                    </td>
                                    <td>
                                        <a title="编辑" href="global_menuadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a title="添加子菜单" href="global_menuadd.aspx?pid=<%#((DataRowView)Container.DataItem)["id"]%>">添加子菜单</a>
                                        <a system="<%#((DataRowView)Container.DataItem)["system"]%>" title="删除" href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delmenu', <%#((DataRowView)Container.DataItem)["id"]%>); }, '该菜单的子菜单也将删除,确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'EditBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button runat="server" Enabled="false" ID="EditBtn" AutoPostBack="false" Text="编辑" />
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_menuadd.aspx'" Text=" 创建顶级菜单 " />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        <div style="display:none;"><cc1:DropDownList runat="server" ID="ddlmenus"/></div>
        </form>
    </div>
    <script type="text/javascript">
        var menus = []
        $.each($("#ddlmenus option"), function (i, o) { if (o.value != "0") { menus.push(o); } });
        function GetMenuName(id) {
            if(id=="0") return "顶级菜单";
            for (var i = 0; i < menus.length; i++) { 
                if(menus[i].value==id) return menus[i].text;
            }
            return "顶级菜单";
        };
        EditForList("EditBtn", Ele("form1"), "global_menuadd.aspx?action=edit&id=", true);
        $("span[parent]").each(function () {   $(this).html(GetMenuName($(this).attr("parent"))); });
        EditForList("EditBtn", Ele("form1"), "global_voteadd.aspx?action=edit&id=", true);
        $("a[system]").each(function () {
            if ($(this).attr("system") == "1") {
                $(this).remove(); ;
            }
        });
        $("#DelBtn").hide();
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'EditBtn')
        };
    </script>
</body>
</html>
