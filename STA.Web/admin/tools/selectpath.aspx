<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectpath.aspx.cs" Inherits="STA.Web.Admin.Tools.selectpath" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>选择文件</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../js/zlib.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
    #mwrapper{padding:0 0px 0 5px;overflow:hidden;}
    #main{margin:0;}
    .conb-2{padding:0;margin:0;}
    .conb-2 .con,.conb-2 .bar{border:0;padding-top:3px;}
    .tools{width:100%;padding:0px 0 3px;overflow:hidden; height:24px;}
    .tools .left{width:45%;float:left;overflow:hidden;display:block;padding:3px 0 0 0;}
    .tools .left ul{padding:0 0 0 5px;}
    .tools .left li{display:inline;line-height:16px;height:16px;padding:0 3px 0 0;}
    .tools .left a{font-size:13px;padding:3px 0px;}
    .tools .right{float:right;width:55%;height:24px;text-align:right;display:block;overflow:hidden;}
    .tools .right input{border:1px solid #ccc;}
    .tools .right input.t1{width:150px;padding:2px 0 0 2px;}
    .tools .right input.t2{width:50px;padding:2px 0 0 0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="tools clearfix">
                <div class="left">
                     <ul>
                        <li><a href="javascript:SubmitForm();"><img alt="刷新" style="vertical-align:middle;" src="../images/refreshfolder.gif" />刷新</a></li>
                        <li><a href="javascript:void(0);" onclick="$('#fdiv').css('display',$('#fdiv').css('display')=='none'?'':'none');"><img alt="新建" style="vertical-align:middle;" src="../images/foldernew.gif" />新建</a></li>
                        <li><a href="javascript:void(0);" onclick="SelectCurrentPath();"><img alt="上传" style="vertical-align:middle;" src="../images/submit.gif" />选择当前路径</a></li>
                    </ul>
                </div>

                <div class="right">
                    <span style="display: none;" id="fdiv">
                      <input type="text" id="dirname" class="t1" value="新建文件夹" />
                      <input type="button" id="dirbtn" onclick="SubmitForm(1,$('#dirname').val());" class="t2" value="新建" />&nbsp;&nbsp;
                    </span>
                </div>
            </div>
            <div class="conb-2">
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th width="46%">
                                        文件夹名
                                    </th>
                                    <th width="27%">
                                        创建时间
                                    </th>
                                    <th width="27%">
                                        最近修改
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                     <%# Eval("name")%>
                                </td>
                                <td>
                                     <%#DataBinder.Eval(Container.DataItem, "CreationDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "LastWriteDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hidAction" runat="server" value="" />
    <input type="hidden" id="hidValue" runat="server" value="" />
    </form>
    <script type="text/javascript">
        $(function () {
            if ($("span.topdir").length > 0) {
                $(".list tr:eq(1) td:gt(0)").remove(); ;
                $(".list tr:eq(1) td").attr("colspan", "3");
            }
        });
        function ReturnImg(reimg) {
            var funcNum = $D.queryString('CKEditorFuncNum');
            var fileUrl = reimg;
            window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
            window.close();
        }
        function SelectCurrentPath() {
         <%if(STARequest.GetString("fele")!=string.Empty){ %>
            window.parent.document.getElementById("<%=STARequest.GetString("fele")%>").value = "<%=currentvirtualpath%>";
            window.parent.$("#editbox").jqmHide();
       <%}%>
        }
    </script>
</body>
</html>