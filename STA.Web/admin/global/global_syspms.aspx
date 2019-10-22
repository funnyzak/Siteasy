<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_syspms.aspx.cs" Inherits="STA.Web.Admin.syspms" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>系统消息管理</title>
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
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;系统日志</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th width="30">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" />
                                        </th>
                                        <th>
                                            消息标题
                                        </th>
                                        <th>
                                            消息内容
                                        </th>
                                        <th width="150">
                                            发送日期
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                          <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','BtnDel')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <span class="ptip"><%#((DataRowView)Container.DataItem)["subject"]%></span>
                                    </td>
                                    <td>
                                        <span class="ptip2"><%#(((DataRowView)Container.DataItem)["content"]).ToString()%></span>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
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
                    <cc1:Button ID="BtnDel" runat="server" OnClientClick="ControlPostBack('BtnDel', ' 删除后不可恢复,确认删除吗?');return;"
                        Text=" 删除所选" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/del.gif" Enabled="false" />&nbsp;
                    <cc1:Button ID="BtnAdd" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" AutoPostBack="false" Text=" 发送系统消息" />&nbsp;
                </div>
            </div>
    <div class="modals jqmWindow" style="display:none;cursor:move;width:400px;margin:auto auto auto -250px;border-width:5px;padding:10px 10px 0 15px;">
            <div class="conb-1">
                <div class="bar">发送系统消息</div>
                <div class="con" style="padding-bottom:0;">
	            <table cellspacing="0" cellpadding="4" width="100%" align="center">
		            <tr>
			            <td class="itemtitle" style="width:70px;">标题:</td>
			            <td>
				            <cc1:TextBox ID="txtSubject" runat="server"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td class="itemtitle" style="width:70px;">内容:</td>
			            <td>
				            <cc1:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td colspan="2" align="center" style="height:35px;padding-top:10px;">
				            <cc1:Button ID="BtnSend" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif" runat="server" Text=" 发送消息"></cc1:Button>&nbsp;&nbsp;
                            <cc1:Button ID="Button1" runat="server" Text=" 取消发送" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/del.gif" AutoPostBack="false" OnClientClick="$('.modals').jqmHide();"></cc1:Button>
			            </td>
		            </tr>
	            </table>
                </div>
             </div>
     </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $(".modals").draggable().jqm({ overlay: config.overlay <= 0 ? 1 : config.overlay, modal: false, overlayClass: config.overlayClass, trigger: "#BtnAdd" });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'BtnDel')
        }
        RegColumnPostip(".ptip", 36, "..");
        RegColumnPostip(".ptip2", 100, "..");
    </script>
</body>
</html>
