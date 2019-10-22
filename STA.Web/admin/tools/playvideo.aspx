<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="playvideo.aspx.cs" Inherits="STA.Web.Admin.Tools.playvideo" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Core" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>播放视频 - <%=info.Attachment %></title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/js/config.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/js/common.js"></script>
</head>
<body>
        <%if (STARequest.GetInt("type", 0) == 1 && !STARequest.GetString("filename").ToString().EndsWith(".mp4") && !STARequest.GetString("filename").ToString().EndsWith(".flv"))
          {
              Response.Write(UBB.ParseMedia("flv", STARequest.GetInt("width", 500), STARequest.GetInt("height", 350), true,Utils.UrlDecode(STARequest.GetString("filename")))); %>
        <%}
          else
          {%>
        <object id="f4Player" width="100%" height="100%" type="application/x-shockwave-flash" data="../../sta/plugin/videoplayer/f4player/player.swf?v1.3.5"> 
          <param name="movie" value="../../sta/plugin/videoplayer/f4player/player.swf?v1.3.5" /> 
          <param name="quality" value="high" /> 
          <param name="menu" value="false" /> 
          <param name="scale" value="noscale" /> 
          <param name="allowfullscreen" value="true"> 
          <param name="allowscriptaccess" value="always"> 
          <param name="swlivevonnect" value="true" /> 
          <param name="cachebusting" value="false"> 
          <param name="flashvars"   value="skin=../../sta/plugin/videoplayer/f4player/skins/mySkin.swf&video=<%=info.Filename%>"/> 
          <a href="http://www.adobe.com/go/flashplayer/">Download it from Adobe.</a> 
         
        </object>

        <%}%>
</body>
</html>
