<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_dbcollectdd.aspx.cs" Inherits="STA.Web.Admin.dbcollectdd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>数据库采集规则添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
        .box_collect_1{float:left;overflow:hidden;margin:0 10px 0 0;display:inline;}
        .box_collect_1 .tit{font-size:13px;padding:0 0 5px 0;text-align:center;}
        .box_collect_1 .op{padding:70px 0 0 0;font-size:14px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">数据库采集规则添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                入库频道：
                            </td>
                            <td>
                                <cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                规则名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                默认状态：
                            </td>
                            <td>
                               <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="5" Width="500" runat="server" ID="rblStatus"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                数据库类型：
                            </td>
                            <td>
                               <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="5" Width="500" runat="server" ID="rblDbtype">
                                    <asp:ListItem Value="2" Selected="True">MS SqlServer</asp:ListItem>
                               </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                 IP地址或服务器名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDatasource" Width="150" Text="(local)" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                 数据库名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDbname" Width="150" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                 登录帐号：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUserid" Width="150" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                 登录密码：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtPassword" Width="150" CanBeNull="必填"/>  <a href="javascript:;" onclick="LoadDbCollectLSet();">加载规则配置</a>
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle">
                                 采集数据表：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlTbnames" />
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle">
                                 标题字段：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlRepeatkey" HelpText="对应文档标题的字段,入库后文档的标题字段,也用来检测文档重复"/>
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle">
                                 主键字段：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlPrimarykey" HelpText="为确保采集正确，主键字段必须为数值类型"/>
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle">
                                 排序字段：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlOrderbykey" />
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle">
                                排序方式：
                            </td>
                            <td>
                               <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="5" Width="300" runat="server" ID="rblSortby">
                                    <asp:ListItem Value="DESC" Selected="True">降序</asp:ListItem>
                                    <asp:ListItem Value="ASC">升序</asp:ListItem>
                               </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle">
                                 WHERE条件：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtWhere" Height="70" TextMode="MultiLine" HelpText="如果要入库特定的记录，可以在这里设置;即填写查询条件语句,如：id between 20 and 30"/>
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle" colspan="2">
                                 采集字段匹配<cc1:Help runat="server" HelpText="操作方法：<br/>添加匹配：在左右表选取字段然后点击 <- 进行字段匹配<br/>移除匹配：在左表选取已匹配的字段点击 -> 移除" Text=""/>:
                            </td>
                        </tr>
                        <tr step="2" style="display:none;">
                            <td class="itemtitle" style="width:auto;padding-top:5px;" colspan="2">
                                 <div class="box_collect_1">
                                    <div class="tit">入库表：<%=baseconfig.Tableprefix%>contents</div>
                                    <div class="box"><cc1:ListBox runat="server" ID="lbConfields" Width="400" Height="150"/></div>
                                 </div>
                                 <div class="box_collect_1" style="width:15px;">
                                    <div class="op">
                                    <a href="javascript:;" onclick="SetMatchField();" title="匹配字段"><-</a><br />
                                    <a href="javascript:;" onclick="MoveMatchField();" title="移除匹配字段">-></a>
                                    </div>
                                 </div>
                                 <div class="box_collect_1">
                                    <div class="tit">采集表：<span class="stbname"></span></div>
                                    <div class="box"><cc1:ListBox runat="server" ID="lbStb"  Width="230" Height="150"/></div>
                                 </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <input type="hidden" runat="server" id="hidtbname" />
            <input type="hidden" runat="server" id="hidprimarykey" />
            <input type="hidden" runat="server" id="hidorderbykey" />
            <input type="hidden" runat="server" id="hidrepeatkey" />
            <textarea id="matchs" runat="server" style="display:none;"/>
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 规 则 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_dbcollect.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        var isedit = $("#hidAction").val() == "edit", fields = [], matchs = [], cgeloop = 0;
        var cfields = <%=cfields%>;
        $("#ddlTbnames").change(function () {
            Loading("正在加载表字段,请稍等..");
            var source = $.trim($("#txtDatasource").val()), userid = $.trim($("#txtUserid").val()), pwd = $.trim($("#txtPassword").val()), dbname = $.trim($("#txtDbname").val());
            var connect = "source=" + source + "&userid=" + userid + "&password=" + pwd + "&dbname=" + dbname + "&tbname=" + $(this).val();
            $(".stbname").html($(this).val());
            if(cgeloop>0) {
                matchs = [];
                $("#hidtbname").val($(this).val());
            }
            Ajax("dbtablefields&" + connect, function (data) {
                var uitb = $("#ddlPrimarykey,#ddlOrderbykey,#ddlRepeatkey,#lbStb").empty();
                fields = ToJson(data);
                $.each(fields, function (idx, obj) { $("<option value='" + $.trim(obj.name) + "'>" + obj.name + "[" + obj.type + "]</option>").appendTo(uitb) });
                if (isedit && cgeloop == 0) {
                    $("#ddlPrimarykey").val($("#hidprimarykey").val());
                    $("#ddlOrderbykey").val($("#hidorderbykey").val());
                    $("#ddlRepeatkey").val($("#hidrepeatkey").val());
                }
                Reset();
                cgeloop++;
                $.unblockUI();
            });
        });

        function LoadDbCollectLSet(){
            if($("tr[step='2']:eq(0)").css("display")=="none"){
                DbCollectLSet();
            }else{
                SConfirm(DbCollectLSet,"加载将覆盖之前的规则设置，确认加载？");
            }
        }

        function DbCollectLSet() {
            var source = $.trim($("#txtDatasource").val()), userid = $.trim($("#txtUserid").val()), pwd = $.trim($("#txtPassword").val()), dbname = $.trim($("#txtDbname").val());
            if (source == "" || userid == "" || pwd == "" || dbname == "") {
                SAlert("数据库连接信息填写不完整！")
                return;
            }
            Loading("正在测试连接,请稍等..")
            var connect = "source=" + source + "&userid=" + userid + "&password=" + pwd + "&dbname=" + dbname
            Ajax("dbconnecttest&" + connect,
                 function (ret) {
                     if (ret != "True") {
                         $.unblockUI({ onUnblock: function () { SAlert("数据库连接超时或连接有误！"); $("tr[step='2']").css("display", "none"); } });
                     } else {
                         Loading("正在加载数据库表,请稍等..");
                         Ajax("dbtables&" + connect, function (data) {
                             var tbs = ToJson(data), uitb = $("#ddlTbnames").empty();
                             $.each(tbs, function (idx, obj) { $("<option value='" + obj.name + "'>" + obj.name + "</option>").appendTo(uitb) });
                             if (isedit) uitb.val($("#hidtbname").val());
                             $(".stbname").html(uitb.val());
                             $("tr[step='2']").css("display", "");
                             $("#ddlTbnames").trigger("change");
                         });
                     }
                 });
        };

        function SetMatchField() {
            var tfds = Ele("lbConfields").value, sfds = Ele("lbStb").value;
            if (CheckInMatch(tfds)!=null) return;
            matchs.push({sname:sfds,name:tfds});
            Reset();
        };


        function MoveMatchField() {
            for(var i = 0;i< $("#lbConfields").val().toString().split(",").length;i++){
                RemoveMatch($("#lbConfields").val().toString().split(",")[i]);
            }
            Reset();
        };

        function RemoveMatch(name){
            for (var i = 0; i < matchs.length; i++) {
                if ($.trim(matchs[i].name) == $.trim(name)){
                      matchs.remove(i);  
                      break;
                }
            }
        }

        //检测字段是否已设置匹配
        function CheckInMatch(name){
            for (var i = 0; i < matchs.length; i++) {
                if (matchs[i].name == name) return matchs[i];
            }
            return null;
        }

        function GetMatchs() {
            var str = $("#matchs").val();
            for (var i = 0; i < str.split(",").length; i++) { 
                var item = str.split(",")[i];
                if (item == "") continue;
                if(GetObj(item.split(":")[1])!=null) matchs.push({sname:item.split(":")[0],name:item.split(":")[1]});
            }
        }

        //获取入库表字段
        function GetObj(name){
            for (var i = 0; i < cfields.length; i++) {
                if (cfields[i].name == name) return cfields[i];
            } 
            return null;
        }

        //获取采集表字段
        function GetSObj(name){
            for (var i = 0; i < fields.length; i++) {
                if (fields[i].name == name) return fields[i];
            } 
            return null;
        }

        function Reset() {
            $("#matchs").empty();
            $.each(matchs, function (idx, obj) {
                $("#matchs").append(obj.sname + ":" + obj.name + ",");
            });
            var uitb = $("#lbConfields").empty();
            $.each(cfields, function (idx, obj) {
                var m = CheckInMatch(obj.name), sobj;
                if(m!=null) sobj = GetSObj(m.sname);
                if(sobj != null){
                    $("<option value='" + obj.name + "'>" + obj.ntext + " -> "+ sobj.name + "[" + sobj.type + "]</option>").appendTo(uitb)
                }else{
                    $("<option value='" + obj.name + "'>" + obj.ntext + "</option>").appendTo(uitb)
                } 
            });
        };

        $(function () { 
            if (isedit){ 
                GetMatchs();
                DbCollectLSet();
            }
        });
    </script>
</body>
</html>