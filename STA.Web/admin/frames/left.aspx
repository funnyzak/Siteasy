<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="left.aspx.cs" Inherits="STA.Web.Admin.Frame.left" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
<title>Siteasy 内容管理系统</title>
<meta name="keywords" content="Siteasy 内容管理系统" />
<meta name="description" content="Siteasy,CMS,asp.net" />
<link href="../styles/base.css" type="text/css" rel="stylesheet" />
<link href="../styles/ztree.css" type="text/css" rel="stylesheet" />
<link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
<script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
<script language="javascript" type="text/javascript" src="../js/zlib.js"></script>
<script language="javascript" type="text/javascript" src="../js/ztree.js"></script>
<script language="javascript" type="text/javascript" src="../js/public.js"></script>
<style type="text/css">
#mtree{overflow:hidden;}
</style>
<script type="text/javascript">
    var menuNodes = <%=menuStr%>,mTree,nodes,dlink = $.trim($D.queryString("link"));
    $.each(menuNodes,function(idx,obj){
        if(obj.url.length>5){
            if(obj.url.toLowerCase().indexOf("http://")<0){
                obj.url = "loading.aspx?type=loading&url=../"+escape(obj.url);
            }
        }
    });
    var setting = {
		data: {
			simpleData: {
				enable: true
			}
		},
        view:{
             showLine: false,
             expandSpeed: ""
        },
        callback: {
            beforeClick: function (treeId, treeNode) {
                if (treeNode.url == "" ) {
                    return false;
                }
                else if(treeNode.url.indexOf("http://") === 0 || treeNode.url.indexOf("https://") === 0){
                    window.open(treeNode.url);
                    return false;
                }
            },
            onClick:function(event,treeId,treeNode){

                        
            }
        }
    };
    $(function () {
        mTree = $.fn.zTree.init($("#mtree"), setting, menuNodes);
        nodes = mTree.transformToArray(mTree.getNodes());
        if(dlink.length>0){
            for(var i =0; i< nodes.length; i++) {
                if(nodes[i].url.indexOf(escape(dlink))>0) {
                    mTree.selectNode(nodes[i]);
                    break;
                }
            }
        }
        if(mTree.getNodeByParam("id", "128", null) != null ){
            mTree.expandNode(mTree.getNodeByParam("id", "128", null),true,false,true);
        }
    });
</script>
</head>
<body style="overflow-x:hidden;">
<div id="fastmenubg">&nbsp;</div>
<div id="fastmenu">
	<div class="my"><ul class="fmenu"></ul></div>
    <div class="default">
    	<ul>
            <li onclick="window.open('<%=sitepath%>/index.aspx','_blank')">打开首页</li>
            <li onclick="top.StatusAction('clearcache', '清理缓存');">清空缓存</li>
            <li onclick="top.StatusAction('tplmake', '生成模板');">生成模板</li>
            <li onclick="window.open('../tools/sysinfo.aspx','main')">系统信息</li>
            <li onclick="window.open('../tools/likeset.aspx','main')">个人偏好设置</li>
    <%--        <li onclick="window.open('../global/global_menumanage.aspx','main')">管理功能菜单</li>--%>
            <li onclick="window.open('../tools/fastmenumanage.aspx','main')">管理快捷菜单</li>
            <li onclick="window.open('http://doc.stacms.com/','_blank')">帮助</li>
            <li onclick="window.open('../login.aspx?action=loginout','_top')">退出</li>
        </ul>
    </div>
</div>
<div id="lwrapper"> 
    <ul id="mtree" class="ztree"></ul>
</div>
<script type="text/javascript">
    var widthInterval, isHidden = true;
    function LoadFastMenuData() {
        var menus = "";
        Ajax("fastmenu", function (data) {
            $.each(ToJson(data), function (index, obj) {
                menus += "<li onclick=\"window.open('" + obj.url + "','" + obj.target + "')\">" + obj.name + "</li>";
            });
            if (menus.length > 0) {
                $(".fmenu").html(menus);
                $(".my").css("display", "block");
            }
            else {
                $(".fmenu").html("");
                $(".my").css("display", "none");
            }
            LoadMenuEvent();
        });
    }
    LoadFastMenuData();
    $("#fastmenu").bind("mouseleave", function () {
        isHidden = true;
        $(window).unbind("scroll");
        //window.clearInterval(widthInterval);
        $("#fastmenubg,#fastmenu").css("display", "none");
        top.MenuMouseOut();
    });
    function LoadMenuEvent() {
        $("#fastmenu").find("li").each(function () {
            $(this).mouseenter(function () {
                $(this).addClass("on");
            }).mouseleave(function () {
                $(this).removeClass("on");
            });
        });
    }
    function MenuMouseIn() {
        isHidden = false;
        PositionMenu();
        $(window).scroll(PositionMenu);
        //if (!$.browser.webkit) {
        //widthInterval = window.setInterval(PositionMenu, 700);
        //}
    }
    function PositionMenu() {
        if (isHidden) {
            window.clearInterval(widthInterval);
            return;
        }
        top.DropFocus();
        var scrollTop = $(document).scrollTop();
        var docElem = document.documentElement;
        pHeight = self.innerHeight || (docElem && docElem.clientHeight) || document.body.clientHeight
        if (pHeight < $(document).height()) {
            $("#fastmenu").css({ width: "163" });
            $("#fastmenu").find("div").css("width", "150");
            $("#fastmenu").find("ul").css("width", "140");
            $("#fastmenubg").css({ width: "163", height: $("#fastmenu").height(), background: "url(../themes/<%=systyle%>/droptopscroll.gif) no-repeat left bottom" });
        } else {
            $("#fastmenu").css({ width: "180" });
            $("#fastmenu").find("div").css("width", "170");
            $("#fastmenu").find("ul").css("width", "150");
            $("#fastmenubg").css({ width: "180", height: $("#fastmenu").height(), background: "url(../themes/<%=systyle%>/droptop.gif) no-repeat left bottom" });
        }
        $("#fastmenu,#fastmenubg").css("top", $(document).scrollTop());
        $("#fastmenubg,#fastmenu").slideDown(300);
    }
    if (menuNodes.length > 0) {
        menuNodes[0].open = true;
    }
</script>
</body>
</html>