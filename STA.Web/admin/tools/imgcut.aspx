<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imgcut.aspx.cs" Inherits="STA.Web.Admin.Tools.imgcut" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Core" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>图片剪切</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../plugin/scripts/jcrop/css/jquery.Jcrop.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jcrop/js/jquery.Jcrop.min.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
    html,body{background-color:#333;margin:0;padding:0;}
    #wrap{background:#999;font-size:14px;margin:10px 20px;padding:10px;}
    .img{float:left;border:#333 3px solid;}
    .img_pre{float:left;width:250px;margin-left:30px;}
    .img_pre strong{display:block;font-size:12px;text-align:center;}
    .img_pre button{width:80px;margin:0 auto;}
    .img_pre .cont,.img_pre .cont img{height:170px;overflow:hidden;width:250px;}
    .img_pre .btn{padding:50px 0 0 0;text-align:center}
    #data{clear:both;padding-top:20px;}
    #dobutton{clear:both;margin-top:8px;}
    #preview-pane{display:block;position:absolute;z-index:2000;top:30px;right:-280px;border:1px rgba(0,0,0,.4) solid;background-color:white;-webkit-border-radius:6px;-moz-border-radius:6px;border-radius:6px;-webkit-box-shadow:1px 1px 5px 2px rgba(0,0,0,0.2);-moz-box-shadow:1px 1px 5px 2px rgba(0,0,0,0.2);box-shadow:1px 1px 5px 2px rgba(0,0,0,0.2);padding:6px;}
    #preview-pane .preview-container{width:250px;height:170px;overflow:hidden;}
    </style>
</head>

<script type="text/javascript">
    jQuery(function ($) {
        var jcrop_api,
        boundx,
        boundy,
        $preview = $('#preview-pane'),
        $pcnt = $('#preview-pane .preview-container'),
        $pimg = $('#preview-pane .preview-container img'),

        xsize = $pcnt.width(),
        ysize = $pcnt.height();

        $('#target').Jcrop({
            bgColor: "#fff",
            minSize: [100, 100],
            setSelect: [60, 70, 60 + 240, 70 + 180],
            onChange: updatePreview,
            onSelect: updatePreview
        }, function () {
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            jcrop_api = this;
            $preview.appendTo(jcrop_api.ui.holder);
        });

        $('#coords').on('change', 'input', function (e) {
            var x1 = $('#x1').val(),
		  x2 = $('#x2').val(),
		  y1 = $('#y1').val(),
		  y2 = $('#y2').val();
            jcrop_api.setSelect([x1, y1, x2, y2]);
        });
        function updatePreview(c) {
            if (parseInt(c.w) > 0) {
                var rx = xsize / c.w;
                var ry = ysize / c.h;

                $pimg.css({
                    width: Math.round(rx * boundx) + 'px',
                    height: Math.round(ry * boundy) + 'px',
                    marginLeft: '-' + Math.round(rx * c.x) + 'px',
                    marginTop: '-' + Math.round(ry * c.y) + 'px'
                });
            }
            showCoords(c)
        };
    });

    function showCoords(c) {
        $('#x1').val(c.x);
        $('#y1').val(c.y);
        $('#x2').val(c.x2);
        $('#y2').val(c.y2);
        $('#w').val(c.w);
        $('#h').val(c.h);
    };

    function clearCoords() {
        $('#coords input').val('');
    };

</script>
<body>
    <div id="wrap">
        <form id="form1" runat="server">
        <input type="hidden" name="file" value="<%=STARequest.GetString("filename")%>" />
        <div class="img">
            <img alt="" src="<%=STARequest.GetString("filename")%>" id="target" width="400" />
        </div>
        <div id="data" style='color: #ffffff'>
            left <input type="text" size="4" id="x1" name="x1" />
            top <input type="text" size="4" id="y1" name="y1" />
            <label style="display:none">X2 <input type="text" size="4" id="x2" name="x2" /></label>
            <label style="display:none">Y2 <input type="text" size="4" id="y2" name="y2" /></label>
            宽： <input type="text" size="4" id="w" name="w" />
            高： <input type="text" size="4" id="h" name="h" />
        </div>
        </form>
    </div>
</body>
</html>
