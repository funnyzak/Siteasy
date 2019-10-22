<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_runsql.aspx.cs" Inherits="STA.Web.Admin.runsql" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import NameSpace="STA.Common"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
<title>执行SQL脚本</title>
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
        <cc1:PageInfo runat="server" Text="如果要一次运行多条SQL语句,语句之间请使用 --/* www.stacms.com */-- 进行分割即可. "/>
        <div class="conb-1">
        	<div class="bar">执行SQL命令</div>
            <div class="con">
                <cc1:TextBox TextMode="MultiLine" Width="99%" Height="150" ID="txtSqlString" runat="server"/>
            	<div class="navbutton inner-pad-2">
                    <cc1:Button runat="server" ID="ExecuteSql" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif" Text=" 执行SQL命令"/>&nbsp;
                    <cc1:Button runat="server" ID="BtnExport" Visible="false" Text=" 导出查询结果 "/>
                </div>
            </div>
        </div>

        <div runat="server" id="tbdata" class="conb-2">

        </div>
        <div id="footer">
        	<%=footer %>
        </div>
    </div>
</div>
</form>
</body>
</html>