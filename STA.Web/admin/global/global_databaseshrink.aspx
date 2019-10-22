<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_databaseshrink.aspx.cs" Inherits="STA.Web.Admin.databaseshrink" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>收缩数据库</title>
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
                <div class="bar">收缩数据库</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">数据库名称</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                     <cc1:TextBox ID="strDbName" runat="server" Text="" CanBeNull="必填" Width="100px" RequiredFieldType="暂无校验" Enabled="false"></cc1:TextBox>
		                    </td>
		                    <td class="vtop txt_desc"></td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">要收缩的大小范围</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                     <cc1:TextBox ID="size" runat="server" Text="0" Width="100px" RequiredFieldType="数据校验"></cc1:TextBox> 单位: M (兆字节)
		                    </td>
		                    <td class="vtop txt_desc">此值仅供程序压缩时进行参考</td>
	                    </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button ID="ClearLog" runat="server" Text="清空日志"></cc1:Button>&nbsp;<cc1:Button ID="ShrinkDB" runat="server" Text="收缩数据库"></cc1:Button>
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