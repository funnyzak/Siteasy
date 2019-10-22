<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_repeatconcheck.aspx.cs"
    Inherits="STA.Web.Admin.repeatconcheck" %>

<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>重复文档检测</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1" style="margin-bottom:10px;">
                <div class="bar">
                    重复文档检测</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                频道模型：
                            </td>
                            <td>
                               <cc1:DropDownList runat="server" ID="ddlConType"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        <div class="navbutton" style="padding:0px 0 20px;">
            <cc1:Button runat="server" ID="CheckCon" Text=" 开始检测重复文档 " ButtonImgUrl="../images/submit.gif" ButtontypeMode="WithImage"/>
        </div>
            <div class="conb-2" runat="server" id="uirecords">
                <div class="bar">
                    &nbsp;&nbsp;<span runat="server" id="spantablename" />重复文档</div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th>
                                        ID
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th>
                                        所属频道
                                    </th>
                                    <th>
                                        发布时间
                                    </th>
                                    <th>
                                        发布用户
                                    </th>
                                    <th>
                                        删除
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
                                    <span class="ptip" onclick="location.href='<%#GetConAddPage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"],1))%>&action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>'"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                </td>
                                <td class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>">
                                    <%#((DataRowView)Container.DataItem)["channelname"]%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["addusername"]%>
                                </td>
                                <td>
                                    <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delcon', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%>(ID:<%#((DataRowView)Container.DataItem)["id"]%>)</b> 吗？');">删除</a>
                                </td>
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#Eval("id")%>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate" style="padding-top:10px;">
                    <cc1:Button runat="server" Enabled="false" ID="DelBtn" AutoPostBack="false" OnClientClick="ControlPostBack('DelBtn','确认删除吗？');return;" Text="删除所选" />
                </div>
            </div>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegColumnPostip(".ptip", 42, "..");
        $(".defval").each(function () { $(this).html($.trim($(this).html()) == "" ? "null" : $(this).html()); });
        $(".chlname").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类") } });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
