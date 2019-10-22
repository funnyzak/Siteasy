<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="slideimglist.aspx.cs" Inherits="STA.Plus.Admin.slideimglist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>幻灯图片管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../plugin/scripts/fancybox/mousewheel.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-1">
                    <div class="bar">
                        幻灯图片搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    标识：<cc1:DropDownList runat="server" ID="ddlLikeid"></cc1:DropDownList>&nbsp;&nbsp;
                                    标题：<cc1:TextBox ID="txtKeywords" Width="220" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索幻灯" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;幻灯图片列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           标题
                                        </th>
                                        <th>
                                            标识
                                        </th>
                                        <th width="70">
                                            排序
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
                                        <span class="ptip" onclick="window.open('<%#((DataRowView)Container.DataItem)["url"]%>','_blank')"><%#((DataRowView)Container.DataItem)["title"]%></span> 
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["likeid"]%>
                                    </td>
                                    <td>
                                        <input type="hidden" name="hidid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#((DataRowView)Container.DataItem)["orderid"]%>"
                                            onblur="this.className='txt';" style="width: 50px;" name="txtOrderId<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <a href="slideimgadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delslideimg', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
                                        <a title="<%#((DataRowView)Container.DataItem)["title"]%>" href="<%#((DataRowView)Container.DataItem)["img"]%>" rel="fanimg">预览</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["title"]%>" />
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
                    <cc1:Button runat="server" ID="SubmitEdit" Text="提交"/>
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../../images/icon/add.gif" OnClientClick="location.href='slideimgadd.aspx'" Text=" 幻灯图片添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        RegFancyBox("a[rel=fanimg]");
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
