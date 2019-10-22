<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_dbfieldreplace.aspx.cs" Inherits="STA.Web.Admin.dbfieldreplace" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>表字段批量替换</title>
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
                    表字段批量替换</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                选择表字段：
                            </td>
                            <td>
                                <cc1:ListBox runat="server" AutoPostBack="true" ID="ltbTables" Height="100" Width="160"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <cc1:ListBox runat="server" ID="ltbFields" Visible="false" Height="100" Width="220"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                替换方式：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblWay" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">普通替换</asp:ListItem>
                                    <asp:ListItem Value="2">正则替换</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="display:none;" id="fkey">
                            <td class="itemtitle">
                                表主键字段：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtPrimarykey" HelpText="正则替换适用于有主键的表，否则不可使用" Width="70"/> <a href="javascript:;" class="selectbtn" id="regexbtn">参考</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                原字段内容：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSource" Height="100" Width="500" TextMode="MultiLine" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                替换为：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTarget" Height="100" Width="500" TextMode="MultiLine"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                替换条件：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtWhere" Width="500" HelpText="如果要替换符合条件的记录，可以在这里设置;即填写查询条件语句,如：id between 20 and 30"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                安全验证：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtVcode" Width="70"/>&nbsp;&nbsp;
                                <img src="" id="vimg" align="absMiddle" style="cursor:pointer;" alt="看不清，换一张" title="看不清，换一张"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="Replace" Text=" 开始替换内容 " ButtonImgUrl="../images/submit.gif" ButtontypeMode="WithImage"/>
        </div>
        <cc1:PageInfo ID="PageInfo1" runat="server" Text="字段替换是非常危险的，请慎重使用，替换前建议先<a class='red' href='global_databasebackup.aspx'>备份数据库</a>。如不熟悉系统结构建议勿使用此功能！"/>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <div class="jqmWindow" id="regexdesc"  style="display:none;width:500px;margin:auto auto auto -250px;padding-bottom:5px;">
      <div class="mbtitle jqDrag"><div class="poptitle">&nbsp;各式各样的正则表达式参考大全</div><div class="mbclose jqmClose" title="点击关闭">关闭</div></div>
      <div class="mbcontent" style="height:200px;padding:10px;overflow-y:scroll;text-align:left;">
        ^\d+$　　//匹配非负整数（正整数 + 0） 　<br />
        //匹配整数   ^\d+(\.\d+)?$　　//匹配非负浮点数（正浮点数 + 0） <br />
        ^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$　　//匹配正浮点数 <br />
        ^((-\d+(\.\d+)?)|(0+(\.0+)?))$　　//匹配非正浮点数（负浮点数 + 0） <br />
        ^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$　　//匹配负浮点数 <br />
        ^(-?\d+)(\.\d+)?$　　//匹配浮点数 <br />
        ^[A-Za-z]+$ //匹配由26个英文字母组成的字符串 <br />
        ^[A-Z]+$ //匹配由26个英文字母的大写组成的字符串 <br />
        ^[a-z]+$　　//匹配由26个英文字母的小写组成的字符串 <br />
        ^[A-Za-z0-9]+$　　//匹配由数字和26个英文字母组成的字符串 <br />
        ^\w+$　　//匹配由数字、26个英文字母或者下划线组成的字符串 <br />
        ^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$　　　　//匹配email地址 <br />
        ^[a-zA-z]+://匹配(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$　　//匹配url <br />
        匹配中文字符的正则表达式： [\u4e00-\u9fa5] <br />
        匹配双字节字符(包括汉字在内)：[^\x00-\xff] <br />
        匹配空行的正则表达式：\n[\s| ]*\r <br />
        匹配HTML标记的正则表达式：/&lt;(.*)&gt;.*&lt;\/&gt;|&lt;(.*)   \/&gt;/ <br />
        匹配首尾空格的正则表达式：(^\s*)|(\s*$) <br />
        匹配Email地址的正则表达式：\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)* <br />
        匹配网址URL的正则表达式：^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$ <br />
        匹配帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)：^[a-zA-Z][a-zA-Z0-9_]{4,15}$ <br />
        匹配国内电话号码：(\d{3}-|\d{4}-)?(\d{8}|\d{7})? <br />
        匹配腾讯QQ号：^[1-9]*[1-9][0-9]*$ <br />
        <br />
        下表是元字符及其在正则表达式上下文中的行为的一个完整列表，具体到每个正则表达式符号： <br />
        \   将下一个字符标记为一个特殊字符、或一个原义字符、或一个后向引用、或一个八进制转义符。 <br />
        ^ 匹配输入字符串的开始位置。如果设置了 RegExp   对象的Multiline 属性，^ 也匹配 ’\n’ 或 ’\r’ 之后的位置。 <br />
        $ 匹配输入字符串的结束位置。如果设置了 RegExp   对象的Multiline 属性，$ 也匹配 ’\n’ 或 ’\r’ 之前的位置。 <br />
        * 匹配前面的子表达式零次或多次。 <br />
        +   匹配前面的子表达式一次或多次。+ 等价于 {1,}。 <br />
        ? 匹配前面的子表达式零次或一次。? 等价于 {0,1}。 <br />
        {n} n   是一个非负整数，匹配确定的n 次。 <br />
        {n,} n 是一个非负整数，至少匹配n 次。 <br />
        {n,m} m 和 n 均为非负整数，其中n &lt;=   m。最少匹配 n 次且最多匹配 m 次。在逗号和两个数之间不能有空格。 <br />
        ? 当该字符紧跟在任何一个其他限制符 (*, +, ?, {n}, {n,},   {n,m}) 后面时，匹配模式是非贪婪的。非贪婪模式尽可能少的匹配所搜索的字符串，而默认的贪婪模式则尽可能多的匹配所搜索的字符串。 <br />
        . 匹配除 &quot;\n&quot;   之外的任何单个字符。要匹配包括 ’\n’ 在内的任何字符，请使用象 ’[.\n]’ 的模式。 <br />
        (pattern) 匹配pattern 并获取这一匹配。   (?:pattern) 匹配pattern 但不获取匹配结果，也就是说这是一个非获取匹配，不进行存储供以后使用。 (?=pattern) 正向预查，在任何匹配   pattern 的字符串开始处匹配查找字符串。这是一个非获取匹配，也就是说，该匹配不需要获取供以后使用。 (?!pattern)   负向预查，与(?=pattern)作用相反 x|y 匹配 x 或 y。 <br />
        [xyz] 字符集合。 <br />
        [^xyz] 负值字符集合。 <br />
        [a-z]   字符范围，匹配指定范围内的任意字符。<br />
        [^a-z] 负值字符范围，匹配任何不在指定范围内的任意字符。 <br />
        \b   匹配一个单词边界，也就是指单词和空格间的位置。 <br />
        \B 匹配非单词边界。 <br />
        \cx 匹配由x指明的控制字符。 <br />
        \d 匹配一个数字字符。等价于   [0-9]。 <br />
        \D 匹配一个非数字字符。等价于 [^0-9]。<br />
        ?
        \f 匹配一个换页符。等价于 \x0c 和 \cL。 <br />
        \n   匹配一个换行符。等价于 \x0a 和 \cJ。 <br />
        \r 匹配一个回车符。等价于 \x0d 和 \cM。 <br />
        \s   匹配任何空白字符，包括空格、制表符、换页符等等。等价于[ \f\n\r\t\v]。 <br />
        \S 匹配任何非空白字符。等价于 [^ \f\n\r\t\v]。 <br />
        \t 匹配一个制表符。等价于 \x09 和 \cI。<br />
        \v 匹配一个垂直制表符。等价于 \x0b 和 \cK。 <br />
        \w   匹配包括下划线的任何单词字符。等价于’[A-Za-z0-9_]’。<br />
        \W 匹配任何非单词字符。等价于 ’[^A-Za-z0-9_]’。<br />
        \xn 匹配   n，其中 n 为十六进制转义值。十六进制转义值必须为确定的两个数字长。 <br />
        \num 匹配 num，其中num是一个正整数。对所获取的匹配的引用。 <br />
        \n 标识一个八进制转义值或一个后向引用。如果 \n 之前至少 n 个获取的子表达式，则 n 为后向引用。否则，如果 n 为八进制数字 (0-7)，则   n 为一个八进制转义值。 <br />
        \nm 标识一个八进制转义值或一个后向引用。如果 \nm 之前至少有is preceded by at least nm   个获取得子表达式，则 nm 为后向引用。如果 \nm 之前至少有 n 个获取，则 n 为一个后跟文字 m 的后向引用。如果前面的条件都不满足，若 n 和 m   均为八进制数字 (0-7)，则 \nm 将匹配八进制转义值 nm。 \nml 如果 n 为八进制数字 (0-3)，且 m 和 l 均为八进制数字   (0-7)，则匹配八.
        </div>
    </div>
    <script type="text/javascript">
        $("#regexdesc").draggable({ handle: ".mbtitle"}).jqm({ overlay: 0, modal: false, trigger: "#regexbtn" });
        $("#vimg").click(function () { $(this).attr("src", "../../sta/vcode/vcode.aspx?cookiename=replacefield&num=5&live=1&date=" + new Date()); }).trigger("click");
        $("input[name='rblWay']").click(function () { $("#fkey").css("display", $(this).val() == "2" ? "" : "none"); });
        $("input[name='rblWay']:checked").trigger("click");
    </script>
</body>
</html>
