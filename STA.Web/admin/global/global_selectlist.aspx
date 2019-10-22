<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_selectlist.aspx.cs" Inherits="STA.Web.Admin.selectlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>联动项管理</title>
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
                <div class="conb-1" style="display:none;">
                    <div class="bar">
                        快捷操作</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    联动类型：<cc1:DropDownList runat="server" ID="ddlSelect" AutoPostBack="true"/>&nbsp;&nbsp;&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="修 改" />&nbsp;
                                    <cc1:Button ID="Button1" runat="server" Text=" 删 除" />&nbsp;
                                    <cc1:Button ID="Button2" runat="server" Text="预 览" />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-1">
                    <div class="bar">
                        <%=STARequest.GetString("name") %> 联动项管理</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    所属分类：<cc1:DropDownList runat="server" ID="ddlCurrent" AutoPostBack="true"/>&nbsp;&nbsp;
                                    分类名称：<cc1:TextBox Width="300" runat="server" ID="txtNames" HelpText="如果没选择分类则表示增加的是一级分类，用半角逗号,分开可以一次增加多个分类。"/>&nbsp;&nbsp;
                                    <cc1:Button Text="添 加" runat="server" ID="BtnAdd"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;<%=STARequest.GetString("name") %> 联动项管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            ID
                                        </th>
                                        <th>
                                            项名称
                                        </th>
                                        <th>
                                            级数
                                        </th>
                                        <th>
                                            联动标识
                                        </th>
                                        <th>
                                            项值
                                        </th>
                                        <th>
                                            权重
                                        </th>
                                        <th width="100">
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
                                        <%#((DataRowView)Container.DataItem)["id"]%>
                                    </td>
                                    <td>
                                        <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#(((DataRowView)Container.DataItem)["sname"]).ToString().Trim()%>"
                                            onblur="this.className='txt';" style="width: 170px;" name="txtName<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                      <%#((DataRowView)Container.DataItem)["slevel"]%>级
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["ename"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["value"])%>
                                    </td>
                                    <td>
                                        <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#((DataRowView)Container.DataItem)["orderid"]%>"
                                            onblur="this.className='txt';" style="width: 70px;" name="txtOrderId<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="SubmitForm('update', <%#((DataRowView)Container.DataItem)["id"]%>);">更新</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delselect', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
                                        <input type="hidden" name="id" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除所选" Enabled="false"/>
                    <cc1:Button ID="SaveBtn" runat="server" Text="提交修改"/>
                    <cc1:Button ID="Button3" runat="server" Text="返回管理" AutoPostBack="false" OnClientClick="location.href='global_selects.aspx'"/>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
