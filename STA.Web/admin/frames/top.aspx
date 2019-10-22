<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="STA.Web.Admin.Frame.top" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>Siteasy 内容管理系统</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="keywords" content="Siteasy 内容管理系统" />
    <meta name="description" content="Siteasy,CMS,asp.net" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
        var ms = [<%=menuStr%>],menuids="<%=menuIds%>";
    </script>
</head>
<body>
    <div id="twrapper">
        <div id="drop" class="dropbtn">
            常用功能</div>
        <div id="topnav">
            <ul>
            </ul>
        </div>
        <div id="topsearch">
            <div class="btn">
                <img src="../images/place.gif" height="22" alt="搜索" /></div>
            <input class="keyword" type="text" />
            <div class="searchtype">
                内容</div>
            <div class="arrow">
                <img src="../images/place.gif" height="11" alt="选择搜索类型" /></div>
        </div>
        <div id="statustip">
        </div>
    </div>
    <script type="text/javascript">
        var defaultTxt = "请键入关键字...";
        var searchTypeIndex = 0;
        var searchType = ["内容", "专题"];
        //var searchpage = ["global_contentlist.aspx","global_userlist.aspx","global_speciallist.aspx"];
        var doing = false;
        $(function () {
            $("#drop").bind("mouseover", function () {
                this.className = "dropbtn_on";
                top.MenuMouseIn();
            });

            $("img:eq(1)").click(function () {
                var next = searchTypeIndex + 1;
                searchTypeIndex = next > (searchType.length - 1) ? 0 : next;
                $(".searchtype").html(searchType[searchTypeIndex]);
            });

            $("input:eq(0)").val(defaultTxt).hover(function () {
                if ($(this).val() == defaultTxt) {
                    $(this).val("").focus();
                }
            }, function () {
                if ($(this).val() == "") {
                    $(this).val(defaultTxt).blur();
                }
            }).keypress(function () {
                if (event.which == '13') {
                    $("img:eq(0)").trigger("click");
                }
            });

            $("img:eq(0)").click(function () {
                var query = $("input:eq(0)").val();
                if (query == defaultTxt || query == "") {
                    $("input:eq(0)").val("").focus();
                    return;
                }
                var searchType = $(".searchtype").html();
                top.Search(searchType, encodeURI(query));
            });
            var menus = "";

            $.each(ms, function (index, obj) {
                var clsname = index == 0 ? " class=\"on\" " : "";
                menus += "<li " + clsname + " onclick=\"ChangeMenu(" + index + "," + obj.id + ",'" + obj.url + "');\">" + obj.name + "</li>";
            });
            $("#topnav").find("ul").html(menus);
            if (ms[0].id != 1) {
                $("#topnav li:eq(0)").trigger("click");
            }
        });

        function DropFocus() {
            $("#drop").removeClass().addClass("dropbtn_on");
        };

        function ChangeMenu(index, id, link) {
            MenuMouseOut();
            $("#topnav").find("li").removeClass();
            $("#topnav").find("li:eq(" + index + ")").addClass("on");
            if ($.trim(link) != "") window.open("loading.aspx?type=loading&url=../" + link, "main");
            top.LoadMenuData(id,"../" + link);
        }

        function MenuMouseOut() {
            $("#drop").removeClass();
            $("#drop").addClass("dropbtn");
        }

        function StatusAction(action, astr, func) {
            if (doing) return;
            astr = decodeURIComponent(astr);
            $("#statustip").show();
            $("#statustip").html("正在" + astr + "...");
            doing = true;
            Ajax(action, function (msg) {
                $("#statustip").html(msg == "yes" ? astr + "成功" : astr + "失败");
                $("#statustip").fadeOut("slow");
                if (func) { func(msg); }
                doing = false;
            });
        }
    </script>
</body>
</html>
