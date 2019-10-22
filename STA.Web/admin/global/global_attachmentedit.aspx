<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_attachmentedit.aspx.cs" Inherits="STA.Web.Admin.attachmentedit" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>附件编辑</title>
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
    <div id="mwrapper" style="padding:0 7px 0 12px;overflow:hidden;">
        <div id="main" style="margin:0;overflow:hidden;">
            <table>
                <tr>
                    <td style="width:70px;">
                        附件名称：
                    </td>
                    <td>
                        <cc1:TextBox runat="server" ID="txtName"/>
                    </td>
                </tr>
                <tr>
                    <td>
                       <cc1:Help runat="server" Text=" 文件" HelpText="请上传合法格式的文件" ID="tiphelp"/>：
                    </td>
                    <td>
                        <input type="file" id="overupload" runat="server" class="fileup"/>
                    </td>
                </tr>
                <tr>
                    <td>
                       附件描述：
                    </td>
                    <td>
                        <cc1:TextBox runat="server" ID="txtDescText" TextMode="MultiLine" Height="70"/>
                    </td>
                </tr>
            </table>
        <div class="navbutton" style="text-align:center;">
                    <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 附 件 "/>
        </div>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtName").focus();
        $("#SaveInfo").bind("click", function () { Loading("提交中,请稍等.."); });
    </script>
</body>
</html>