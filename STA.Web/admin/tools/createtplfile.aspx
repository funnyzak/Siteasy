<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createtplfile.aspx.cs" Inherits="STA.Web.Admin.Tools.createtplfile" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>创建模版</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
    .label,a.label:link,a.label:visited,a.label:hover,a.label:active{display:block;width:auto;float:left;margin:0 17px 0 0;font-size:14px;cursor:pointer; text-decoration:underline;}
    a.label:hover{font-weight:bold;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">创建/修改模版</div>
                <div class="con" style="width:96%;margin:0 2% 0 0;">
                    <table>
                        <tr>
                            <td style="width:70px;">文件名：</td>
                            <td><cc1:TextBox ID="txtFilename" Text="new_template.htm" runat="server"/></td>
                        </tr>
                        <tr>
                            <td>调用标签：</td>
                            <td> 
                                <a class="label">文档</a>
                                <a class="label">频道</a>
                                <a class="label">链接</a>
                                <a class="label">单页</a>
                                <a class="label">Tag</a>
                            </td>
                        </tr>
                        <tr>
                            <td>模板语法：</td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
	                        <td colspan="2">
                                <cc1:TextBox TextMode="MultiLine" Height="320" ID="txtTplcontent" Width="99%" runat="server"/>
                            </td>
                        </tr> 
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 模板 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>