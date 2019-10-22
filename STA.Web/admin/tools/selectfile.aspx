<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectfile.aspx.cs" Inherits="STA.Web.Admin.Tools.selectfile" %>
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
    .tools{width:100%;padding:0px 0 3px;overflow:hidden; height:27px;}
    .tools .left{width:200px;float:left;overflow:hidden;display:block;padding:3px 0 0 0;}
    .tools .left ul{padding:0 0 0 5px;}
    .tools .left li{display:inline;line-height:16px;height:16px;padding:0 3px 0 0;}
    .tools .left a{font-size:13px;padding:3px 0px;}
    .tools .right{float:right;width:380px;height:27px;text-align:right;display:block;overflow:hidden;padding:0 15px 0 0;}
    .tools .right input{}
    .tools .right input.t1{width:150px;padding:2px 0 0 2px;}
    .tools .right input.t2{width:50px;padding:2px 0 0 0;}
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div id="mwrapper">
        <div id="main">
<%--            <%if (STARequest.GetString("CKEditorFuncNum") == "")
              {%>--%>
            <div class="tools clearfix" <%=STARequest.GetString("CKEditorFuncNum")!=""||STARequest.GetString("cltmed").Trim()=="3"? "style='padding:10px 0;'":"" %>>
                <div class="left">
                     <ul>
                        <li><a href="javascript:$$('form1').submit();"><img alt="刷新" style="vertical-align:middle;" src="../images/refreshfolder.gif" />刷新</a></li>
                        <li><a href="javascript:void(0);" onclick="Action(1);"><img alt="新建" style="vertical-align:middle;" src="../images/foldernew.gif" />新建文件夹</a></li>
                        <li><a href="javascript:void(0);" onclick="Action(9);"><img alt="上传" style="vertical-align:middle;" src="../images/fileup.gif" />上传</a></li>
                    </ul>
                </div>

                <div class="right">
                    <span style="display: none;" id="fdiv">
                      <input type="text" id="dirname" class="t1" value="新建文件夹" />
                      <input type="button" id="dirbtn" class="t2" value="新建" />
                    </span>
                    <div style="display: none;" id="updiv">
                        <input type="file" name="upfile" id="upfile" style="height:20px;width:150px;" size="10"/>&nbsp;&nbsp;
                        <input type="checkbox" name="watermark" value="1" title="上传图片类型的文件时加水印,此项仅在系统开启并设置水印功能才生效"/>水印
                        <input type='checkbox' name="thumbsize" value="1" title="上传图片类型的文件时是否自动缩小(等比例缩小)"/>缩小
                        宽：<input type="text" style="width:25px" name="iwidth" value="<%=config.Thumbsize.Split(',')[0]%>" title="图片缩小到的最大宽度"/>
                        高：<input type="text" style="width:25px" name="iheight" value="<%=config.Thumbsize.Split(',')[1]%>" title="图片缩小到的最大高度"/>
                    </div>&nbsp;&nbsp;
                </div>
            </div>
            <div class="conb-2">
<%--            <%}else{%>
            <div class="conb-2" style="padding:10px 0;">
            <%} %>--%>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th width="67%">
                                        文件名
                                    </th>
                                    <th width="13%">
                                        文件大小
                                    </th>
                                    <th width="20%">
                                        创建时间
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                     <%# Eval("name")%>
                                </td>
                                <td rel="fsize" folder="<%# Eval("isfolder")%>" size="<%# Eval("size")%>"> 
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "CreationDate", "{0:yyyy-MM-dd HH:mm}")%>
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
            <textarea style="display: none" name="fieldstr" id="fieldstr"></textarea>
        <input type="hidden" name="action" id="action" value="" />
    </form>
    <script type="text/javascript">
        //atturl=1
        var weburl = "<%=STARequest.GetInt("weburl",0)==1? config.Weburl:"" %>";
        $(function () {
            if ($("span.topdir").length > 0) {
                $(".list tr:eq(1) td:gt(0)").remove(); ;
                $(".list tr:eq(1) td").attr("colspan", "3");
            }
        });

        RegPostip('*[ftype="img"]');

        function ReturnImg(reimg) {
            <%if(STARequest.GetString("atturl")=="1"){ %>
            Ajax("getattidbyname&filename=" + encodeURI(reimg), function (aid) {
                var funcNum = queryString('CKEditorFuncNum'), fileUrl = "";
                if($.trim(aid) == "" || $.trim(aid)=="0"){
                    fileUrl = weburl + reimg;
                }else{
                    fileUrl = "<%=sitepath%>/attachment.aspx?attid=" + aid;
                }
                window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
                window.close();
            });
            <%}else{%>
            var funcNum = queryString('CKEditorFuncNum');
            var fileUrl = weburl + reimg;
            window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
            window.close();
            <%}%>
        };

         function XHReturnValue(value) {
            callback(weburl + value);
        };

        <%if(STARequest.GetString("fele")!=string.Empty){ %>
        function ReturnValue(value) {
            <%if(STARequest.GetString("atturl")=="1"){ %>
            Ajax("getattidbyname&filename=" + encodeURI(value), function (aid) {
                if($.trim(aid) == "" || $.trim(aid)=="0"){
                    window.parent.document.getElementById("<%=STARequest.GetString("fele")%>").value = weburl + value;
                }else{
                    window.parent.document.getElementById("<%=STARequest.GetString("fele")%>").value = "<%=sitepath%>/attachment.aspx?attid=" + aid;
                }
                window.parent.$("#editbox").jqmHide();
            });
            <%}else{%>
            window.parent.document.getElementById("<%=STARequest.GetString("fele")%>").value = weburl + value;
            window.parent.$("#editbox").jqmHide();
            <%}%>
        }
        function AppendValue(value) {
            window.parent.document.getElementById("<%=STARequest.GetString("fele")%>").value += weburl + value;
        }
        <%}%>

        <%if(STARequest.GetString("cusmethod")!=string.Empty){ %>
        function AppendValueByMethod(value){
            window.parent.<%=STARequest.GetString("cusmethod")%>(weburl + value);
        }
        <%}%>
        $("td[rel=fsize]").each(function (idx) {
            var size = parseInt($(this).attr("size")),isfolder = $(this).attr("folder");
            $(this).html(isfolder!="True"?ConvertSize(size):"");
        });
        function OpenFolder(str) {
            SetField(0, str);
            $$("form1").submit();
        }
        function SetField(action, str) {
            $$("fieldstr").value = str;
            $$("action").value = action;
        }
        function CreateFolder() {
            $$("updiv").style.display = "none";
            $$("fdiv").style.display = $$("fdiv").style.display == "" ? "none" : "";
        }
        function Up() {
            $$("fdiv").style.display = "none";
            $$("updiv").style.display = $$("updiv").style.display == "" ? "none" : "";
        }
        $("#upfile").change(function () {
            Loading("上传中,请稍等..");
            SetField(2, $$("upfile").value);
            $$("form1").submit();
        });
        $("#dirbtn").click(function () {
            SetField(1, $$("dirname").value);
            $$("form1").submit();
        });
        function Action(type) {
            switch (type) {
                case 1: CreateFolder(); break;
                case 9: Up(); break;
                default: break;
            }
        }
    </script>
</body>
</html>