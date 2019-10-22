<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_channelist.aspx.cs" Inherits="STA.Web.Admin.channelist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>频道管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <link href="../plugin/scripts/treetable/css/jquery.treetable.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/treetable/js/jquery.treetable.min.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#channeltable').treeTable({ initialState: $("#hidTableState").val() });
        });
    </script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;频道列表&nbsp;<%=cinfo%></div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list" id="channeltable">
                                    <tr>
                                        <th>
                                           <span style="padding-left: 13px;">频道名称</span> 
                                        </th>
                                        <th width="70">
                                            排序
                                        </th>
                                        <th width="300">
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="node-<%#((DataRowView)Container.DataItem)["id"]%>" <%#((DataRowView)Container.DataItem)["parentnode"]%>>
                                    <td style="padding-left: 25px;">
                                        <a href="global_channeladd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>" title="编辑频道">
                                            <img align="absmiddle" border="0" src="../images/icon/folder.gif" /></a><%#(((DataRowView)Container.DataItem)["ishidden"]).ToString()=="1"?"<font color='red'>[隐]</font>":""%><a href="../tools/view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&name=channel"
                                                title="浏览频道" target="_blank"><%#((DataRowView)Container.DataItem)["name"]%></a>[ID:<%#((DataRowView)Container.DataItem)["id"]%>]
                                        (<span tpid="<%#((DataRowView)Container.DataItem)["typeid"]%>"></span>:<%#((DataRowView)Container.DataItem)["contentcount"]%>)
                                    </td>
                                    <td>
                                        <input type="hidden" name="hidid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#((DataRowView)Container.DataItem)["orderid"]%>"
                                            onblur="this.className='txt';" style="width: 50px;" name="txtOrderId<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <a href="global_channeladd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="<%# GetConManagePage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"]))%>&chlid=<%#((DataRowView)Container.DataItem)["id"]%>">内容</a>
                                        <a href="global_channeladd.aspx?parent=<%#((DataRowView)Container.DataItem)["id"]%>">增加子频道</a> 
                                        <a href="<%#GetConAddPage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"],1))%>&chlid=<%#((DataRowView)Container.DataItem)["id"]%>">添加内容</a>
                                        <a href="global_createchannels.aspx?chlid=<%#((DataRowView)Container.DataItem)["id"]%>">发布</a>
                                        <a href="../tools/view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&name=channel" target="_blank">查看</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delchannel', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('emptychannel', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认清空频道 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 的所有内容 吗？');">清空</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelChannelBtn', 'EmptyChannelBtn', 'DelChlHtmlBtn', 'CopyBtn')"
                                            name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" id="cname<%#((DataRowView)Container.DataItem)["id"]%>" name="cname<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <input type="hidden" id="hidTableState" runat="server" value="expanded" />
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate">
                    <cc1:Button runat="server" ID="SubmitEdit" Text="提交" />
                    <cc1:Button ID="TableState" runat="server" Text="收起" />
                    <cc1:Button runat="server" Enabled="false" ID="DelChannelBtn" OnClientClick="ControlPostBack('DelChannelBtn', '频道的所有子频道及内容将全部删除，确认删除吗?');return;" Text="删除频道" />
                    <cc1:Button runat="server" Enabled="false" ID="CopyBtn" Text="复制频道" />
                    <cc1:Button runat="server" Enabled="false" ID="DelChlHtmlBtn" OnClientClick="ControlPostBack('DelChlHtmlBtn', '确认删除频道所有的索引静态页吗?');return;" Text="删除静态页面" />
                    <cc1:Button runat="server" Enabled="false" ID="EmptyChannelBtn" Text="清空内容" OnClientClick="ControlPostBack('EmptyChannelBtn', '您选择的频道及子频道的所有内容将全部清空，确认清空吗?');return;" />
                    <cc1:Button ID="Button6" ButtonImgUrl="../images/icon/add.gif" ButtontypeMode="WithImage" runat="server" AutoPostBack="false" OnClientClick="location.href='global_channeladd.aspx'" Text=" 频道添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        <div style="display:none"><cc1:DropDownList runat="server" ID="ddlChltype"></cc1:DropDownList></div>
        </form>
    </div>
    <script type="text/javascript">
        var types = [];
        $.each($("#ddlChltype option"), function (i, o) { if (o.value != "0") { types.push(o); } });
        function GetTypeName(cid) {
            if (cid == "0") return "未知模型";
            for (var i = 0; i < types.length; i++) {
                if (types[i].value == cid) return types[i].text;
            }
            return "未知模型";
        };
        $("span[tpid]").each(function (idx, obj) {
            $(obj).html(GetTypeName($(obj).attr("tpid")));
        });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelChannelBtn', 'EmptyChannelBtn', 'DelChlHtmlBtn', 'CopyBtn')
        }  
    </script>
</body>
</html>
