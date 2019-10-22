<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_createchannels.aspx.cs" Inherits="STA.Web.Admin.createchannels" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>频道发布</title>
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
                <div class="bar">频道发布</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle" style="width:200px;">
                                选择发布频道：
                                <br />1.可按Ctrl、Shift复选
                                <br />2.如不选，则发布所有频道      
                            </td>
                            <td>
                                <cc1:ListBox runat="server" ID="lbChannels" Height="300"></cc1:ListBox> 
                            </td>  
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 开始发布 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#lbChannels").css("min-width", "150px");
    </script>
</body>
</html>