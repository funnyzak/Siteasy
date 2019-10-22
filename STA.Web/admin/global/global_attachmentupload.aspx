<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_attachmentupload.aspx.cs" Inherits="STA.Web.Admin.attachmentupload" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>附件批量上传</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../plugin/swfupload/default.css" type="text/css" rel="stylesheet" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../plugin/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../plugin/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../plugin/swfupload/fileprogress.js"></script>
    <script type="text/javascript" src="../plugin/swfupload/handlers.js" charset="gb2312"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
	    var upload1;
	    window.onload = function () {
	        upload1 = new SWFUpload({
	            upload_url: "../tools/multupload.aspx?uid=<%=userid%>&username=<%=Utils.UrlEncode(username)%>&password=<%=Utils.UrlEncode(STA.Data.Users.GetUser(userid).Password)%>&<%=Request.QueryString.ToString()%>",
	            post_params: {
	                "sid": "<%=Session.SessionID %>",
	                "groupid": "<%=admingroupid%>",
	                "groupname": "<%=Utils.UrlEncode(admingroupname)%>"
	            },
	            file_size_limit: "1024000", 
	            file_types: "*.*",
	            file_types_description: "All Files",
	            file_upload_limit: "200",
	            file_queue_limit: "0",

	            file_dialog_start_handler: fileDialogStart,
	            file_queued_handler: fileQueued,
	            file_queue_error_handler: fileQueueError,
	            file_dialog_complete_handler: fileDialogComplete,
	            upload_start_handler: uploadStart,
	            upload_progress_handler: uploadProgress,
	            upload_error_handler: uploadError,
	            upload_success_handler: uploadSuccess,
	            upload_complete_handler: uploadComplete,

	            button_image_url: "../plugin/swfupload/UploadText_61x22.png",
	            button_placeholder_id: "spanButtonPlaceholder1",
	            button_width: 61,
	            button_height: 22,

	            flash_url: "../plugin/swfupload/swfupload.swf",

	            custom_settings: {
	                progressTarget: "fsUploadProgress1",
	                cancelButtonId: "btnCancel1"
	            },

	            debug: false
	        });
	    }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post" action="../tools/multupload.aspx">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">批量附件上传</div>
                <div class="con" >
                    <table>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help2" runat="server" Text="配置说明"/>：</td>
                            <td>
                            <div class="filetext" runat="server" id="filetext"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help1" runat="server" Text="批量附件上传" HelpText="批量最大同时上传文件个数为200个;上传速度以您网站所在服务器性能、带宽来决定,建议单文件控制在10MB以内" />：</td>
	                        <td>
						        <div class="fieldset flash" id="fsUploadProgress1">
							        <span class="legend">上传状态</span>
						        </div>
						        <div style="padding-left: 5px;">
							        <span id="spanButtonPlaceholder1"></span>
							        <input id="btnCancel1" type="button" value="取消上传" onclick="cancelQueue(upload1);" disabled="disabled" style="margin-left: 2px; height: 22px; font-size: 8pt;" />
							        <br />
						        </div>
                            </td>
                        </tr> 
                    </table>
                </div>
            </div>
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