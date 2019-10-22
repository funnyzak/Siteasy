<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_html2js.aspx.cs" Inherits="STA.Web.Admin.html2js" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>HTML/JS互转</title>
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
                <div class="bar">
                    HTML转JAVASCRIPT</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                请将 HTML 源代码拷贝到下面:
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="80" ID="txtHtml_1" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                下面是相应的 JAVASCRIPT 代码:
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="80" ID="txtJs_1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-1">
                <div class="bar">
                    JAVASCRIPT转HTML</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                请将 JAVASCRIPT 源代码拷贝到下面:
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="80" ID="txtJs_2" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                下面是相应的 HTML 代码:
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="80" ID="txtHtml_2" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-1">
                <div class="bar">
                    JAVASCRIPT加密/解密</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                请将要处理的JAVASCRIPT源代码拷贝到下面( <a href="javascript:;" onclick="encode()">加密</a> <a href="javascript:;" onclick="decode();">解密</a> ):
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="100" ID="code" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-1">
                <div class="bar">
                    URL转JS引用代码</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                请将 URL 链接拷贝到下面( <a href="javascript:;" onclick="setJsFile('utf-8')">UTF-8</a> <a href="javascript:;" onclick="setJsFile('gb2312')">GB2312</a> <a href="javascript:;" onclick="setJsFile('gbk')">GBK</a> <a href="javascript:;" onclick="setJsFile('big5')">BIG5</a> ):
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox Width="100%" ID="txtUrl" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                JS引用代码：
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                               <cc1:TextBox Width="100%" ID="txtScript" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtHtml_1").change(function () {
            $("#txtJs_1").val("document.writeln(\"" + $("#txtHtml_1").val().replace(/\\/g, "\\\\").replace(/\\/g, "\\/").replace(/\'/g, "\\\'").replace(/\"/g, "\\\"").split('\r\n').join("\");\ndocument.writeln(\"") + "\");");
        }).trigger("change");
        $("#txtJs_2").change(function () {
            $("#txtHtml_2").val($("#txtJs_2").val().replace(/document.writeln\("/g, "").replace(/"\);/g, "").replace(/\\\"/g, "\"").replace(/\\\'/g, "\'").replace(/\\\//g, "\/").replace(/\\\\/g, "\\"));
        }).trigger("change");
        $("#txtUrl").change(function () { setJsFile("utf-8"); });
        function setJsFile(encode) {
            $("#txtScript").val("<script  type=\"text\/javascript\" src=\"<%=config.Weburl+baseconfig.Sitepath%>/tools/js.aspx?url=" + escape($("#txtUrl").val()) + "&encode=" + (encode||"utf-8") +"\"><\/script>");
        }
        a = 62;
        function encode() {
            var code = document.getElementById('code').value;
            code = code.replace(/[\r\n]+/g, '');
            code = code.replace(/'/g, "\\'");
            var tmp = code.match(/\b(\w+)\b/g);
            tmp.sort();
            var dict = [];
            var i, t = '';
            for (var i = 0; i < tmp.length; i++) {
                if (tmp[i] != t) dict.push(t = tmp[i]);
            }
            var len = dict.length;
            var ch;
            for (i = 0; i < len; i++) {
                ch = num(i);
                code = code.replace(new RegExp('\\b' + dict[i] + '\\b', 'g'), ch);
                if (ch == dict[i]) dict[i] = '';
            }
            document.getElementById('code').value = "eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\\\b'+e(c)+'\\\\b','g'),k[c]);return p}(" + "'" + code + "'," + a + "," + len + ",'" + dict.join('|') + "'.split('|'),0,{}))";
        }

        function num(c) {
            return (c < a ? '' : num(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36));
        }

        function decode() {
            var code = document.getElementById('code').value;
            code = code.replace(/^eval/, '');
            document.getElementById('code').value = eval(code);
        }
    </script>
</body>
</html>
