<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_menumanage.aspx.cs" Inherits="STA.Web.Admin.menumanage" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>功能菜单</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../styles/ztree.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../js/zlib.js"></script>
    <script language="javascript" type="text/javascript" src="../js/ztree.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
    var menus = [<%=menuStr%>], icons = [<%=iconStr%>], type = $.trim($D.queryString("type")), zTree, curNode, curaction="add";
    var setting = {
		data: {
			simpleData: {
				enable: true
			}
		},
		edit: {
			enable: true
		},
        view:{
            selectedMulti: false,
            showLine: false,
            expandSpeed: 150
        },
        callback: {
            beforeDrag: beforeDrag,
            beforeEditName: beforeEditName,
            beforeRemove: beforeRemove,
            beforeRename: beforeRename,
            onRemove: onRemove,
            beforeClick: beforeClick
        }
    };

    function beforeClick(treeId, treeNodes)
    {   
        ActionChange("add",treeNodes);
        return true;
    };

    function onRemove(e, treeId, treeNode) {
        Ajax("delmenu&id="+treeNode.id);
        if(curNode.id==treeNode.id){
            ActionChange("add",zTree.getNodes()[0]);
        }
    };

    function ActionChange(action,nodes){
        curaction = action;
        curNode = nodes;
        zTree.selectNode(curNode);
        var name = nodes.name.replace("[流]","").replace("[对]","");
        $("#identify").val("");
        $("input[name='rblsystem'],input[name='rblpagetype']").unbind("click");
        $("#txturl,#txtname,#txticon,#txttarget").val("").removeAttr("readonly");
        $("#txttarget").val("main");
        if(action=="add"){
            $(".curaction").html("添加功能菜单:上级菜单 -> "+name);
        }else{ 
            $("#identify").val(nodes.identify);
            $(".curaction").html("修改功能菜单："+name);
            $("#txtname").val(name);
            $("input[name='rblsystem'][value='" + nodes.system + "']").attr("checked","true");
            $("input[name='rblpagetype'][value='" + nodes.pagetype + "']").attr("checked","true");
            $("#txturl").val(nodes.url);
            $("#txttarget").val(nodes.target);
            $("#txticon").val(nodes.icon.replace("../images/icon/",""));
            if(nodes.system==1){
                 $("#txturl,#txttarget").attr("readonly","readonly");
                 $("input[name='rblsystem'],input[name='rblpagetype']").click(function(){return false;});
            }
        }

    };

    function beforeDrag(treeId, treeNodes) {
	    return false;
    };

    function beforeEditName(treeId, treeNode) {
        if(treeNode.id==0)return false;
        ActionChange("edit",treeNode);
	    return false;
    };

    function beforeRename(treeId, treeNode, newName) {
	    if (newName.length == 0) {
		    setTimeout(function(){zTree.editName(treeNode)}, 10);
		    return false;
	    }
	    return true;
    };

    function beforeRemove(treeId, treeNode) {
        if(treeNode.system==1){
            alert("系统菜单不可以删除！");
            return false;
        }
	    return confirm("确认删除功能菜单 " + treeNode.name + " 吗？");
    };

    function GetMenuObj(id,pid,name,url,target,icon,system,orderid,type,pagetype,identify){
        return {id:id,pId:pid,name:name,url:url,target:target,icon:icon,system:system,orderid:orderid,type:type,pagetype:pagetype,identify:identify};
    };

    function GetOrderIdByAdd(){
       var nodes = zTree.transformToArray(zTree.getNodes()),chns = [];
       for(var i=0;i<nodes.length;i++){
            if(nodes[i].pId==curNode.id) chns.push(nodes[i]);
       }
       if(chns.length==undefined||chns.length<=0) {
            return 50000;
       }else {
             return chns[chns.length-1].orderid-50;  
       }
    }

    $(function () {
        zTree = $.fn.zTree.init($("#menutree"), setting, menus);
        zTree.expandNode(zTree.getNodeByParam("id",0, null), true, false, true);
        ActionChange("add",zTree.getNodeByParam("id", 0, null));
        $.each(icons,function(idx,obj){
            $(".menuicons").append("<img src='../images/icon/"+ obj +"' class='menuicon_' onclick=\"$('#txticon').val('"+ obj +"');$('.menuicons').hide();\"/>");
        });
        $(".menuicons").position({ of: $("#txticon"), my: 'left top', at: "left bottom", offset: '0 1px', collision: "fit none" });
        $(".selecticon").click(function(){ 
            $(".menuicons img").each(function(idx,obj){
                  $(this).css("border-color",$("#txticon").val()!=""&&$(this).attr("src").indexOf($("#txticon").val())>=0?"#333":"#fff");
            });
            $(".menuicons").show(); 
        });
        $("#saveinfo").click(function(){
            var name = $.trim($("#txtname").val()).replaceAll("\"",""),system = parseInt($("input[name='rblsystem']:checked").val()),icon = $.trim($("#txticon").val()),url = $("#txturl").val(),target=$("#txttarget").val(),pagetype=parseInt($("input[name='rblpagetype']:checked").val()),identify = $.trim($("#identify").val()),tempname="";
            if(name==""){
                Tips("菜单名称不能为空！");
                return false;
            }
            tempname = name;
            var posts = "id="+curNode.id+"&name="+encodeURI(name)+"&system="+system +"&pagetype="+ pagetype +"&type=1&icon="+encodeURI(icon)+"&url="+encodeURI(url)+"&target="+encodeURI(target)+"&identify="+encodeURI(identify);
            if(pagetype>1){ if(pagetype==2){ tempname = name + "[流]"; }else{  tempname = name + "[对]"; }} 
            if(curaction=="add"){
                var orderid = GetOrderIdByAdd();
                posts += "&parentid="+curNode.id+"&orderid="+orderid;
                icon = icon==""? "":("../images/icon/"+icon);
                Ajax("addmenu&"+posts,function(id){   
                    zTree.addNodes(curNode,GetMenuObj(parseInt(id),curNode.id,tempname,url,target,icon,system,orderid,1,pagetype,identify));
                    ActionChange("add",curNode);
                });
            }else{
                posts += "&parentid="+curNode.pId+"&orderid="+curNode.orderid;
                Ajax("editmenu&"+posts,function(ret){
                    if(ret=="True"){
                        curNode.name = tempname;
                        curNode.url = url;
                        curNode.target = target;
                        curNode.system = system;
                        curNode.pagetype = pagetype;
                        curNode.icon = icon==""? "":("../images/icon/"+icon);
                        setTimeout(function(){zTree.updateNode(curNode); Tips("修改成功！");}, 10);
                    }
                });
            }
        });
    });

    function Tips(tips) {
        $("#tips").show().html(tips).fadeOut(1000);
    };
    </script>
	<style type="text/css">
    .ztree li button.add {margin-left:2px; margin-right: -1px; background-position:-112px 0; vertical-align:top; *vertical-align:middle}
    td.itemtitle{width:90px;}
	</style>
</head>
<body>
    <div class="menuicons" style="display:none"></div>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-2">
                    <div class="bar">&nbsp;&nbsp;功能菜单</div>
                    <div class="con">
                        <table>
                            <tr>
                                <td width="270" align="left" valign="top" style="border-right: #999 1px dashed;padding:0 0 0 10px">
                                    <ul id="menutree" class="ztree"></ul>
                                    <input type="hidden" id="curpid" value="0" />
                                    <input type="hidden" id="curid" value="0" />
                                </td>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td class="itemtitle">
                                                当前操作：

                                            </td>
                                            <td class="curaction">
                                                 添加功能菜单:上级菜单 -> 功能菜单
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle">
                                                系统菜单：
                                            </td>
                                            <td>
                                                <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="3" Width="300" runat="server"
                                                    ID="rblsystem">
                                                    <asp:ListItem Text="是" Value="1" Selected="True"/>
                                                    <asp:ListItem Text="否" Value="0" />
                                                </cc1:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle">
                                                菜单类型：
                                            </td>
                                            <td>
                                                <cc1:RadioButtonList runat="server" RepeatColumns="3" RepeatDirection="Horizontal" ID="rblpagetype" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle">
                                                菜单名称：
                                            </td>
                                            <td>
                                                <cc1:TextBox runat="server" ID="txtname" Width="200"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle">
                                                链接地址：
                                            </td>
                                            <td>
                                                <cc1:TextBox runat="server" ID="txturl" Width="200"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle">
                                                打开窗口：
                                            </td>
                                            <td>
                                                <cc1:TextBox runat="server" ID="txttarget" Text="main" Width="100"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle">
                                                图标：
                                            </td>
                                            <td>
                                                <cc1:TextBox runat="server" ID="txticon" Width="100"/> <a href="javascript:;" class="selecticon">选择</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="itemtitle" style="padding-top:20px;">
                                            </td>
                                            <td>
                                                <input type="hidden" name="identify" id="identify" />
                                                <cc1:Button runat="server" ID="saveinfo" Text=" 保 存 菜 单 " AutoPostBack="false"/>
                                                <span style="color:#ff0000;" id="tips"></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        </form>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
</body>
</html>
