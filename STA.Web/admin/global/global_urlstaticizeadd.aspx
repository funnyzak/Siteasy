<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_urlstaticizeadd.aspx.cs" Inherits="STA.Web.Admin.urlstaticizeadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>URL静态添加</title>
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
                <div class="bar">URL静态添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                静态化名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTitle" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                网页编码：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEncode" CanBeNull="必填" Text="UTF-8" HelpText="目标网页所使用的页面编码" Width="100"/>
                                <a href="javascript:;" class="setvalue">UTF-8</a>&nbsp;
                                <a href="javascript:;" class="setvalue">GB2312</a>&nbsp;
                                <a href="javascript:;" class="setvalue">GBK</a>&nbsp;
                                <a href="javascript:;" class="setvalue">BIG5</a>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                URL地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUrl" CanBeNull="必填" HelpText="URL地址为空,可使用{weburl}标签(表网站地址,包含网站目录)"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                保存路径：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSavePath" Text="/staticize" HelpText="格式如：/staticize/stacms"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                保存文件名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtFileName" CanBeNull="必填" HelpText="不包括文件后缀名,填写如：stacms"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                静态后缀名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSuffix" Width="100" Text="html" CanBeNull="必填"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 静 态 信 息 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_urlstaticize.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegSetTargetValue($(".setvalue"), $("#txtEncode"));
    </script>
</body>
</html>