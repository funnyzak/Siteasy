<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_clearpms.aspx.cs" Inherits="STA.Web.Admin.clearpms" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>清理短消息</title>
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
                <div class="bar">清理短消息</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2"><input type="checkbox" name="isnew" value="1" id="isnew" checked="checked"/> 不删除未读信息(建议选择)</td></tr>
                        <tr><td class="itemtitle2" colspan="2">删除多少天前</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox id="postdatetime" runat="server" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">不限制时间请输入</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">按发信人&nbsp;<input type="checkbox" name="lowerupper" value="1" id="lowerupper" runat="server" checked="checked"/>不区分大小写</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox id="msgfromlist" runat="server" Width="200"></cc1:TextBox>
                            </td>
		                    <td class="vtop txt_desc">多用户名中间请用半角逗号 "," 分割</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">按关键字搜索主题</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                        <cc1:TextBox id="subject" runat="server" Width="200"></cc1:TextBox>
                            </td>
		                    <td class="vtop txt_desc">关键字中间用","分割</td>
	                    </tr>
	                        <tr><td class="itemtitle2" colspan="2">按关键字搜索全文</td></tr>
	                        <tr>
		                        <td class="vtop rowform">
			                         <cc1:TextBox id="message" runat="server" RequiredFieldType="暂无校验" Width="200"></cc1:TextBox>
		                        </td>
		                        <td class="vtop">关键字中间用","分割</td>
	                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
           <button id="BtnDelPms" type="button" class="ManagerButton" onclick="SConfirm(function(){ DeletePM(); }, '你确认要清理短消息吗?');"><img src="../images/submit.gif" /> 删除短消息</button>
<input type="checkbox" name="isupdateusernewpm" value="1" id="isupdateusernewpm" checked="checked"/>同时更新收件人新短消息数</div>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </form>
    <script type="text/javascript">
        function DeletePM() {
            Loading("正在删除消息,请稍等..");
            $.post("../ajax.aspx", $("#form1").serialize() + "&t=delpms", function (resp) {
                $.unblockUI();
                SAlert(resp != ""? "执行成功,删除了符合条件的 "+ resp + "条信息" : "删除失败");
            });
        }
    </script>
</body>
</html>