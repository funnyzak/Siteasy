<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_visitset.aspx.cs" Inherits="STA.Web.Admin.visitset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>网站访问控制</title>
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
            <div class="conb-1">
                <div class="bar">网站访问控制</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">IP访问列表</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtIpaccess" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc" title="Ipaccess">只有当用户处于本列表中的 IP 地址时才可以访问本网站, 列表以外的地址访问将视为 IP 被禁止, 仅适用于诸如企业、学校内部网站等极个别场合. 每个 IP 一行, 例如 "192.168.*.*"(不含引号) 可匹配 192.168.0.0~192.168.255.255 范围内的所有地址, 留空为所有 IP 除明确禁止的以外均可访问</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">IP禁止访问列表</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtIpdenyaccess" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc" title="Ipdenyaccess">当用户处于本列表中的 IP 地址时将禁止访问本网站. 每个 IP 一行, 例如 "192.168.*.*"(不含引号) 可匹配 192.168.0.0~192.168.255.255 范围内的所有地址</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">管理员后台IP访问列表</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtAdminipaccess" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc" title="Adminipaccess">只有当管理员处于本列表中的 IP 地址时才可以访问网站后台, 列表以外的地址访问将无法访问. 每个 IP 一行, 例如 "192.168.*.*"(不含引号) 可匹配 192.168.0.0~192.168.255.255 范围内的所有地址, 留空为所有 IP 除明确禁止的以外均可访问后台</td>
	                    </tr> 
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 配 置 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">

    </script>
</body>
</html>