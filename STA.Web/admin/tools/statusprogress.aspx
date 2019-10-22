<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="statusprogress.aspx.cs" Inherits="STA.Web.Admin.Tools.statusprogress" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title><%=STARequest.GetString("type")%>进度</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script> 
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
    <div id="mwrapper" style="margin:0;padding:0;">
        <div id="main" style="margin:0;">
        <div class="progressbox" style="width:90%;" style="margin:10px auto 0;">
            <div class="progressbarside">
                <div class="probarpercent">0%</div>
                <div class="progressbar"></div>
            </div>
            <div class="progressmsg">正在加载数据, 请稍等...</div>
        </div>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        var total = 0, success = 0, fail = 0 , pbhinterval;
        function SetPorgressBar(msg, pos) {
            if(parseInt(pos)>100) pos = "100";
            $('.progressbar').width(pos + "%");
            $('.probarpercent').html(pos + "%");
            $('.progressmsg').html(msg);
            if (parseInt(pos) == 100) {
                clearInterval(pbhinterval);
                $('.progressmsg').html("<%=STARequest.GetString("type")%>完毕！成功了 "+ success +" 个, 失败 " + fail + " 个！ 您可以离开此页面！");
            }
        };

        function PbhStatus() {
            Ajax("<%=STARequest.GetString("action")%>status", function (data) {
                data = ToJson(data);
                success = data.success, fail = data.fail, total = data.total, msg = data.msg;
                var pos = total == 0? "100":(parseInt(((success + fail) / total) * 100));
                SetPorgressBar(msg, pos);
            });
        }

        $(function(){
            Ajax("<%=STARequest.GetString("action")%>");
            pbhinterval = setInterval(PbhStatus, 1000); 
        });
    </script>
</body>
</html>