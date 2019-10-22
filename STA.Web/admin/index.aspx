<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="STA.Web.Admin.index" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
<title>Siteasy CMS 管理后台 - Powered by Siteasy</title>
<meta http-equiv="x-ua-compatible" content="ie=7" />
<meta name="keywords" content="Siteasy 内容管理系统" />
<meta name="description" content="Siteasy,CMS,asp.net" />
<link href="styles/base.css" type="text/css" rel="stylesheet" />
<link href="themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
<script language ="javascript" type ="text/javascript" src ="js/jquery.js"></script>
<script language="javascript" type="text/javascript" src="js/public.js"></script>
<script type="text/javascript">
    function DisplayMode() {
        if (document.getElementById("framebody").cols == "180,7,*") {
            document.getElementById("framebody").cols = "0,7,*";
            document.getElementById("separator").contentWindow.document.getElementById('swrapper').className = "swbg2";
            document.getElementById("left").contentWindow.$("#fastmenu").trigger("mouseleave");
        } else {
            document.getElementById("framebody").cols = "180,7,*"
            document.getElementById("separator").contentWindow.document.getElementById('swrapper').className = "swbg1";
        }
    }
    function DisplaySMenu() {
        document.getElementById("framebody").cols = "180,7,*"
        document.getElementById("separator").contentWindow.document.getElementById('swrapper').className = "swbg1";
    }
    function MenuMouseIn() {
        if (document.getElementById("framebody").cols == "0,7,*") {
            DisplayMode();
        }
        document.getElementById("left").contentWindow.MenuMouseIn();
    }

    function MenuMouseOut() {
        document.getElementById("top").contentWindow.MenuMouseOut();
    }

    function DropFocus() {
        document.getElementById("top").contentWindow.DropFocus();
    }

    function LoadMenuData(id, link) {
        document.getElementById("left").contentWindow.location.href = "left.aspx?type=1&id=" + id + "&link=" + link;
    }

    function OpenFastMenu(index, id, link) {
        document.getElementById("top").contentWindow.ChangeMenu(index, id, link);
    }

    function StatusAction(action,astr,func) {
        document.getElementById("top").contentWindow.StatusAction(action,astr,func);
    }

    function LoadFastMenuData() {
        document.getElementById("left").contentWindow.LoadFastMenuData();
    }

    function Search(type, query) {
        var page = "../global/global_contentlist.aspx";
        if (type == "专题") {
            page = "../global/global_contentlist.aspx?type=0&";
        }
        document.getElementById("main").contentWindow.location.href = page +"?query=" + query;
    }
</script>
</head>
<frameset id="frames" framespacing="0" border="false" rows="42,*" frameborder="0" scrolling="auto">
  <frame name="top" id="top" scrolling="no" src="frames/top.aspx">
  <frameset id="framebody" name="framebody" framespacing="0" border="false" cols="180,7,*" frameborder="0">
    <frame name="left" id="left" marginwidth="0" marginheight="0" src="frames/left.aspx" scrolling="auto"/>
    <frame id="separator" name="separator" src="frames/separator.aspx" noresize="noresize" scrolling="no" />
    <frame name="main" id="main" scrolling="auto" src="frames/main.aspx">
  </frameset>
</frameset>
<noframes></noframes>
</html>