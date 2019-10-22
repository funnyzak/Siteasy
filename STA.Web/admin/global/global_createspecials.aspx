<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_createspecials.aspx.cs" Inherits="STA.Web.Admin.createspecials" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>专题发布</title>
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">专题发布</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                专题所属频道：   
                            </td>
                            <td>
                                <cc1:DropDownTreeList runat="server" ID="ddrChannels"/>
                            </td>  
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                专题发布日期：    
                            </td>
                            <td>
                                <cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />
                            </td>  
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                专题ID范围：   
                            </td>
                            <td>
                                <cc1:TextBox ID="txtIdmin" Text="0" Width="70" runat="server" /> - <cc1:TextBox ID="txtMax" Width="70" runat="server" />
                            </td>  
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 开始发布 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
    </script>
</body>
</html>