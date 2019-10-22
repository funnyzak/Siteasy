<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysinfo.aspx.cs" Inherits="STA.Web.Admin.Tools.sysinfo" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>系统信息</title>
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
                        &nbsp;&nbsp;系统信息</div>
                    <div class="con">
                        <table>
                            <tr>
                                <td width="50%">
                                    <table>
                                        <tr>
						                    <td class="itemtitle">服务器名称:</td>
						                    <td><asp:label ID="servername" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">服务器IP地址:</td>
						                    <td><asp:label ID="serverip" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">服务器IIS版本:</td>
						                    <td><asp:label ID="serversoft" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">HTTPS:</td>
						                    <td><asp:label ID="serverhttps" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">服务端脚本执行超时:</td>
						                    <td><asp:label ID="serverout" runat="server" />秒</td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">程序数据库类型:</td>
						                    <td><asp:label ID="databasetype" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%">
                                    <table>
					                    <tr>
						                    <td class="itemtitle">服务器操作系统:</td>
						                    <td><asp:label ID="serverms" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">服务器域名:</td>
						                    <td><asp:label ID="server_name" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">.NET解释引擎版本:</td>
						                    <td><asp:label ID="servernet" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">HTTP访问端口:</td>
						                    <td><asp:label ID="serverport" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">服务器当前时间:</td>
						                    <td><asp:label ID="servertime" runat="server" /></td>
					                    </tr>
                                        <tr>
						                    <td class="itemtitle">数据库名称:</td>
						                    <td><asp:label ID="databasename" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
<%--                                        <tr>
                                            <td class="itemtitle">执行文件绝对路径:</td>
						                    <td><asp:label ID="servernpath" runat="server" /></td>
                                        </tr>--%>
					                    <tr>
						                    <td class="itemtitle">虚拟目录绝对路径:</td>
						                    <td><asp:label ID="serverppath" runat="server" /></td>
					                    </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;浏览器信息</div>
                    <div class="con">
                        <table>
                            <tr>
                                <td width="50%">
                                    <table>
                                        <tr>
						                    <td class="itemtitle">浏览者ip地址:</td>
						                    <td><asp:label ID="cip" runat="server"></asp:label></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">浏览器:</td>
						                    <td><asp:label ID="ie" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">JavaScript:</td>
						                    <td><asp:label ID="javas" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">JavaApplets:</td>
						                    <td><asp:label ID="javaa" runat="server" /></td>
                                        </tr>
                                        <tr>
						                    <td class="itemtitle">语言:</td>
						                    <td><asp:label ID="cl" runat="server"></asp:label></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%">
                                    <table>
					                    <tr>
						                    <td class="itemtitle">浏览者操作系统:</td>
						                    <td><asp:label ID="ms" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">浏览器版本:</td>
						                    <td><asp:label ID="vi" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">VBScript:</td>
						                    <td><asp:label ID="vbs" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">Cookies:</td>
						                    <td><asp:label ID="cookies" runat="server" /></td>
					                    </tr>
					                    <tr>
						                    <td class="itemtitle">Frames(分栏):</td>
						                    <td><asp:label ID="frames" runat="server" /></td>
					                    </tr>
                                    </table>
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
</body>
</html>
