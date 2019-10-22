<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="slideimglist.aspx.cs" Inherits="STA.Plus.Admin.advisorylist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>留言咨询管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-1">
                    <div class="bar">
                        留言咨询搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    来源IP：<cc1:TextBox ID="txtIp" Width="100" runat="server" />&nbsp;&nbsp;
                                    咨询类型：<cc1:TextBox ID="txtQtype" Width="100" runat="server" />&nbsp;&nbsp;
                                    标题：<cc1:TextBox ID="txtKeywords" Width="220" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索咨询" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;留言咨询列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 标题
                                        </th>
                                        <th>
                                           类型
                                        </th>
                                        <th>
                                           咨询时间
                                        </th>
                                        <th>
                                            姓名
                                        </th>
                                        <th>
                                            联系电话
                                        </th>
                                        <th>
                                            来源IP
                                        </th>
                                        <th width="100">
                                            操作
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','ViewInfo','ExportDoc')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["title"]%>" />
                                        <span class="ptip" onclick="ShowInfo(<%#((DataRowView)Container.DataItem)["id"]%>)"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["qtype"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["uname"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["phone"]%>
                                    </td>
                                    <td>
                                        <span><%#((DataRowView)Container.DataItem)["ip"]%></span>
                                         <div id="cinfo<%#((DataRowView)Container.DataItem)["id"]%>" style="display:none;">
	                                        <table style="width:480px;overflow:hidden;">
		                                        <tr>
			                                        <td class="itemtitle5">标题:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["title"]%>（<%#((DataRowView)Container.DataItem)["qtype"]%>）
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">联系姓名:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["uname"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">联系邮件:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["email"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">联系电话:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["phone"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">来源IP:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["ip"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">咨询时间:</td>
			                                        <td>
				                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">咨询内容:</td>
			                                        <td>
				                                        <%#Utils.StrFormatPtag((((DataRowView)Container.DataItem)["message"]).ToString())%>
			                                        </td>
		                                        </tr>
                                            </table>
                                         </div>
                                    </td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('deladvisory', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
                                        <a title="<%#((DataRowView)Container.DataItem)["title"]%>" onclick="ShowInfo(<%#((DataRowView)Container.DataItem)["id"]%>)" href="javascript:;">查看详细</a>
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
                    <cc1:Button ID="ViewInfo" runat="server" Text="查看详细" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="ExportDoc" runat="server" Text="导出为Word文档" Enabled="false"/>
                    <cc1:Button ID="Button1" runat="server" OnClientClick="window.open('/plus/advisory.aspx')" Text="打开前台" AutoPostBack="false"/>
                </div>
            </div>
             <div class="cominfo jqmWindow" style="display:none;width:500px;margin:auto auto auto -250px;height:230px;overflow-y:auto;border-width:5px;padding:10px 10px 0 15px;"> </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        RegColumnPostip(".ptip", 36, "..");
        $(".cominfo").jqm({ overlay: config.overlay <= 0 ? 1 : config.overlay, modal: false, overlayClass: config.overlayClass });
        EditForList("ViewInfo", Ele("form1"), "", true, function (id) { ShowInfo(id); });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'ViewInfo', 'ExportDoc')
        }

        function ShowInfo(id) {
            $(".cominfo").html($("#cinfo" + id).html());
            $(".cominfo").jqmShow();
        };
    </script>
</body>
</html>
