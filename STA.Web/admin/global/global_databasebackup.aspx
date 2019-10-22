<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_databasebackup.aspx.cs" Inherits="STA.Web.Admin.databasebackup" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Config" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>数据库备份</title>
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
                <cc1:PageInfo ID="PageInfo1" runat="server" Text="数据库备份文件保存在服务器是非常危险的，因此建议备份数据库后立即下载，然后删除备份文件. "/>
                <div class="conb-2">
                    <div class="bar" title="为了数据安全，备份数据库成功后请尽快下载备份文件并将服务器备份文件删除。">
                        &nbsp;&nbsp;数据库备份</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            文件名称
                                        </th>
                                        <th>
                                            大小
                                        </th>
                                        <th>
                                            创建时间
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <a href="#" onclick="SubmitForm('downback', $(this).html());return false;" title="点击下载"><%# (Eval("name")).ToString().Split('.')[0]%></a>
                                    </td>
                                    <td rel="fsize">
                                        <%# Eval("size")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "CreationDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%# Eval("name")%>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate">
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除备份" Enabled="false"/>
                    <cc1:Button ID="BackUp" AutoPostBack="false" runat="server" Text="备份数据库" />
                    <cc1:Button ID="RestoreBtn"  runat="server" Visible="false" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/buildings.png" Text="恢复数据库" />
                </div>
            </div>
            <div class="database jqmWindow" style="display:none;cursor:move;width:500px;margin:auto auto auto -250px;border-width:5px;padding:10px 10px 0 15px;">
            <div class="conb-1">
                <div class="bar">建立数据库备份</div>
                <div class="con" style="padding-bottom:0;">
	            <table cellspacing="0" cellpadding="4" width="100%" align="center">
		            <tr>
			            <td class="itemtitle">服务器名称:</td>
			            <td>
				            <cc1:TextBox ID="ServerName" runat="server"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td class="itemtitle">用户名:</td>
			            <td>
				            <cc1:TextBox ID="UserName" runat="server"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td class="itemtitle">备份的名称:</td>
			            <td>
				            <cc1:TextBox ID="backupname" runat="server"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td class="itemtitle">数据库名称:</td>
			            <td>
				            <cc1:TextBox ID="strDbName" runat="server"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td class="itemtitle">密码:</td>
			            <td>
				            <cc1:TextBox ID="Password" runat="server" TextMode="Password"></cc1:TextBox>
			            </td>
		            </tr>
		            <tr>
			            <td colspan="2" align="center" style="height:35px;padding-top:10px;">
				            <cc1:Button ID="BackBtn" runat="server" Text="开始备份数据库"></cc1:Button>&nbsp;&nbsp;
                            <cc1:Button ID="Button1" runat="server" Text="取消" AutoPostBack="false" OnClientClick="$('.database').jqmHide();"></cc1:Button>
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
        $(".database").draggable().jqm({ overlay: config.overlay <= 0 ? 1 : config.overlay, modal: false, overlayClass: config.overlayClass, trigger: "#BackUp" });
        $("td[rel=fsize]").each(function (idx) { $(this).html(ConvertSize(parseInt($(this).html()))); });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
