<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_webcollect.aspx.cs" Inherits="STA.Web.Admin.webcollect" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>站点信息采集</title>
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
                        规则搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                 入库频道：<cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>&nbsp;&nbsp;
                                   <cc1:TextBox ID="txtKeywords" Width="300" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索规则" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;站点信息采集</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            采集规则名
                                        </th>
                                        <th>
                                            入库频道
                                        </th>
                                        <th>
                                            默认状态
                                        </th>
                                        <th>
                                            上次采集时间
                                        </th>
                                        <th>
                                            网页编码
                                        </th>
                                        <th>
                                            采集方式
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
                                    <td id="cne<%#((DataRowView)Container.DataItem)["id"]%>">
                                        <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>">
                                        <%#((DataRowView)Container.DataItem)["channelname"]%>
                                    </td>
                                    <td>
                                        <span cstatus="<%#((DataRowView)Container.DataItem)["constatus"]%>"></span>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["encode"]%>
                                    </td>
                                    <td>
                                       <%#((STA.Entity.CollectType)TypeParse.StrToInt(((DataRowView)Container.DataItem)["collecttype"])).ToString()%>
                                    </td>
                                    <td>
                                        <a href="global_webcollectadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delwebcollect', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                        <a href="javascript:;" onclick="Collect(<%#((DataRowView)Container.DataItem)["id"]%>);">采集</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'CopyBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="CopyBtn" runat="server" Text="复制规则" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_webcollectadd.aspx'" Text=" 采集规则添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("span[cstatus]").each(function () { $(this).html(ConStatus($(this).attr("cstatus"))); });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'CopyBtn')
        };
        $(".chlname").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类") } });
        function Collect(id) {
            PopWindow("站点信息采集:" + $("#cne" + id).html(), "../tools/collectpre.aspx?id=" + id, "", 450, 150, null, function () { Ajax("webcolreset"); });
        };
    </script>
</body>
</html>
