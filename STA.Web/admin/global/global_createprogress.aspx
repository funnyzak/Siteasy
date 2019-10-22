<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_createprogress.aspx.cs" Inherits="STA.Web.Admin.createprogress" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title><%=ptitle%></title>
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
                <div class="bar"><%=ptitle%></div>
                <div class="con">
            <table>
                <tr>
                    <td>
                        <div class="progressbox">
                            <div class="progressbarside">
                                <div class="probarpercent">0%</div>
                                <div class="progressbar"></div>
                            </div>
                            <div class="progressmsg">正在加载数据, 请稍等...</div>
                        </div>
                    </td>
                </tr>
            </table>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="Publish" Text=" 开始发布 " Enabled="false" OnClientClick="PbhStart();" AutoPostBack="false"/>
            <cc1:Button runat="server" ID="Where" Text=" 重新选择 " AutoPostBack="false"/>
        </div>
    </div>
    </div>
    <input type="hidden" id="dynamiced" value="<%=config.Dynamiced%>" />
    </form>
    <script type="text/javascript">
        $("#Where").hide().click(function(){history.back(-1);});;
        var total = 0, success = 0, fail = 0 , pbhinterval, msg = "", type = "<%=STARequest.GetString("type")%>";
        function SetPorgressBar(msg, pos) {
            $('.progressbar').width(pos + "%");
            $('.probarpercent').html(pos + "%");
            $('.progressmsg').html(msg);
            if (parseInt(pos) >= 100) {
                clearInterval(pbhinterval);
                $('.progressmsg').html("页面已发布完毕！共成功发布了 "+ success +" 个页面, 失败 " + fail + " 个！ 您可以离开此页面！");
            }
        };

        function PbhStart() {
            $("#Publish").attr("disabled", true).hide();
            if(total>=0){
                Ajax("spublish&name=<%=STARequest.GetString("type")%>&pub=true");
                PbhStatus();
                pbhinterval = setInterval(PbhStatus, 2000); 
            }
        }

        function PbhStatus() {
            Ajax("pbhstatus", function (data) {
                data = ToJson(data);
                success = data.success, fail = data.fail;
                msg = "共有 " + total + " 个页面,已成功生成 " + success + " 个, 失败 " + fail + " 个.";
                SetPorgressBar(msg, parseInt(((success + fail) / total) * 100));
            });
        }

        $(function(){
            var start = "<%=STARequest.GetString("start")%>", name = "<%=STARequest.GetString("type")%>";
            $("#Publish").hide();
            if(parseInt($("#dynamiced").val())>=1 && $.inArray(name, ["rss"])<0){
                $("#Publish").attr("disabled", true);
                $('.progressbar').width("100%");
                $('.probarpercent').html("100%");
                $('.progressmsg').html("网站当前为动态模式,不需要发布静态页面！");
                return;
            }
            Ajax("spublish&name=" + type,function(data){
                total = parseInt(data);
                if(total <= 0){
                    $("#Publish").attr("disabled", true);
                    SetPorgressBar("没有可发布的项，请选择其他发布！","0");
                    $("#Where").show();
                }else{
                    if(start=="yes"){
                        PbhStart();
                    }else{
                        SetPorgressBar("检测到共有 " + total + " 个页面可以发布,请点击按钮开始.","0");
                        $("#Publish").show().attr("disabled",false);
                    }
                }
            });
        });
    </script>
</body>
</html>