<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadfiles.aspx.cs" Inherits="STA.Web.Admin.Tools.uploadfiles" %>

<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>上传文件</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
    .row{height:35px;overflow:hidden;}
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div id="mwrapper" style="padding: 0 7px 0 12px; overflow: hidden;">
        <div id="main" style="margin: 0;overflow:hidden;">
            <table>
                <tr>
                    <td class="itemtitle2">
                        <cc1:Button runat="server" AutoPostBack="false" OnClientClick="AddUpCount();" ID="Button2" Text="增加文件" />
                        <cc1:Button runat="server" ID="SaveInfo" Text="开始上传"/>
                        <cc1:Button runat="server" AutoPostBack="false" OnClientClick="window.parent.$('#editbox').jqmHide();" ID="Button1" Text="取消上传" />
                    </td>
                </tr>
                <tr>
                    <td class="vtop rowform file" style="padding-bottom:0;">
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                        <div class="row"><input type="file" name="files" class="fileup"/></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function AddUpCount() {
            $("td.file").append("<div class=\"row\"><input type=\"file\" name=\"files\" class=\"fileup\"/></div><div class=\"row\"><input type=\"file\" name=\"files\" class=\"fileup\"/></div>");
        }
        $("#SaveInfo").bind("click",function () {
            $(this).attr("disabled", "true");
            Loading("上传中,请稍等..");
            //parent.$("#editclose").unbind().html("");
        });
    </script>
</body>
</html>
