<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="STA.Web.Admin.Frame.main" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Config" %>
<%@ Import Namespace="STA.Core" %>
<%@ Import Namespace="STA.Data" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>Siteasy 内容管理系统</title>
    <meta name="keywords" content="Siteasy 内容管理系统" />
    <meta name="description" content="Siteasy,CMS,asp.net" />
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
            欢迎使用 Siteasy CMS 内容管理系统 — 为您提供简单快速的建站方案。
            <ul class="fleft push-td-10" style="padding-bottom: 40px;">
                <li class="help01" style="padding:0 0 0 20px;"><a href="http://doc.stacms.com/" target="_blank">文档中心</a></li>
                <li class="help02" style="padding:0 0 0 20px;"><a href="http://bbs.stacms.com/" target="_blank">支持论坛</a></li>
            </ul>
            <%--<%Response.Write(Regex.Replace("sdfsdf=lksjdkf sdf sdfe ,sdd,3 ! sd>sdf<3)d(", @"\s|=|,|\[|\]|'|\(|\)|\<|\>|!", "_"));%>--%>
<%--            <div class="conb-1">
                <div class="bar">
                    <a href="http://www.stacms.com/" target="_blank">最新消息</a></div>
                <div class="con">
                    <ul class="links">
                        <li><a href="javascript:;">SiteEasy Cms 全面开始公测，为广大站长企业提供简单跨上的建站方案</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2011/9/23</li>
                        <li><a href="javascript:;">SiteEasy Cms 和中国站长站合作举办站长大会，欢迎大家报名</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2011/8/23</li>
                        <li><a href="javascript:;">SiteEasy Cms 全面开始公测，为广大站长企业提供简单跨上的建站方案</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2011/7/23</li>
                    </ul>
                </div>
            </div>--%>
            <div class="conb-1">
                <div class="bar">
                    信息统计</div>
                <div class="con">
                    <table width="100%">
                        <tr>
                            <td>
                                文档总数:<%=contentcount%>条
                            </td>
                            <td>
                                频道总数:<%=channelcount%>个
                            </td>
                            <td>
                                专题总数:<%=speccount%>个
                            </td>
                            <td>
                                评论条数:<%=commentcount%>条
                            </td>
                        </tr>
                        <tr>
                            <td>
                                用户总数:<%=DatabaseProvider.GetInstance().UserCount()%>个
                            </td>
                            <td>
                                订单总数:<%=DatabaseProvider.GetInstance().ShoporderCount()%>个
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-1">
                <div class="bar">
                    系统信息</div>
                <div class="con">
                    <table width="100%">
                        <tr>
                            <td>
                                服务器名称:
                            </td>
                            <td align="left">
                                <%=Server.MachineName%>
                            </td>
                            <td>
                                服务器操作系统:
                            </td>
                            <td align="left">
                                <%=Environment.OSVersion.ToString()%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                服务器IIS版本:
                            </td>
                            <td align="left">
                                <%=Request.ServerVariables["SERVER_SOFTWARE"] == String.Empty ? "虚拟IIS" : Request.ServerVariables["SERVER_SOFTWARE"]%>
                            </td>
                            <td>
                                .NET解释引擎版本:
                            </td>
                            <td align="left">
                                .NET CLR
                                <%=Environment.Version.Major %>.<%=Environment.Version.Minor %>.<%=Environment.Version.Build %>.<%=Environment.Version.Revision %>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-1" style="margin-bottom: 0px;">
                <div class="bar">
                    相关链接</div>
                <div class="con">
                    <a href="http://team.stacms.com" target="_blank">团队网站</a>, <a href="http://bbs.stacms.com/" target="_blank">
                            模板</a>, <a href="http://bbs.stacms.com/" target="_blank">插件</a>,
                    <a href="http://doc.stacms.com/" target="_blank">文档</a>, <a href="http://bbs.stacms.com/"
                        target="_blank">讨论区</a>, 邮件：<a href="mailto:f@stacms.com"
                        target="_blank">f@stacms.com</a>
                </div>
            </div>
            <div class="push-td-10">
                上次登录时间:<%=lastlogintime%></div>
            <div id="footer">
                <%=footer%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
