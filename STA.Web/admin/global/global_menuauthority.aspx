<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_menuauthority.aspx.cs" Inherits="STA.Web.Admin.menuauthority" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>权限设置</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../styles/ztree.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../js/tinyscrollbar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/ztree.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
    .tdmenus{width:auto;}
    .tinyscrollbar {width:290px; margin:0px 15px 15px 0; display:inline; float:left; }
    .tinyscrollbar .viewport { width: 270px; height: 200px; overflow: hidden; position: relative; }
    .tinyscrollbar .overview { list-style: none; position: absolute; left: 0; top: 0; padding: 0; margin: 0; }
    .tinyscrollbar .scrollbar{ background: transparent url(../images/bg-scrollbar-track-y.png) no-repeat 0 0; position: relative; background-position: 0 0; float: right; width: 15px; }
    .tinyscrollbar .track { background: transparent url(../images/bg-scrollbar-trackend-y.png) no-repeat 0 100%; height: 100%; width:13px; position: relative; padding: 0 1px; }
    .tinyscrollbar .thumb { background: transparent url(../images/bg-scrollbar-thumb-y.png) no-repeat 50% 100%; height: 20px; width: 25px; cursor: pointer; overflow: hidden; position: absolute; top: 0; left: -5px; }
    .tinyscrollbar .thumb .end { background: transparent url(../images/bg-scrollbar-thumb-y.png) no-repeat 50% 0; overflow: hidden; height: 5px; width: 25px; }
    .tinyscrollbar .disable { display: none; }
    </style>
</head>
<body>
    <div class="menuicons" style="display:none"></div>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">权限设置</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle" colspan="2">
                               编辑 <span style="font-weight:bold;" runat="server" id="dgname" /> 菜单权限：
                            </td>
                        </tr>
                        <tr>
                            <td class="tdmenus" colspan="2" style="padding-left:5px;">

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <textarea runat="server" id="menuidlist" name="menuidlist" style="display:none;"></textarea>
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 权 限 设 置" AutoPostBack="false"/>
            <cc1:Button ID="GoBack" runat="server" Text=" 返 回 " OnClientClick="location.href = 'global_sysgrouplist.aspx'" AutoPostBack="false"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    <div style="display:none">
     <cc1:Button runat="server" ID="Button1" Text=" d"/>
    </div>
    </form>
    <div id="tpl" style="display:none;">
	    <div class="tinyscrollbar">
		    <div class="scrollbar"><div class="track"><div class="thumb"><div class="end"></div></div></div></div>
		    <div class="viewport">
			     <div class="overview">    
                 tpl           
			    </div>
		    </div>
	    </div>	
    </div>
    <script type="text/javascript">
    var menus = [<%=menuStr%>],menuids=",<%=menuIds%>,";
    var setting = {
		data: {
			simpleData: {
				enable: true
			}
		},
        view:{
            selectedMulti: false,
            showLine: false,
            expandSpeed: ""
        },
        callback: {
            beforeClick: beforeClick,
            beforeCollapse: beforeCollapse
        },
        check: {
            enable: true,
            chkboxType:{ "Y" : "ps", "N" : "ps" }
        }
    };
    function beforeClick(treeId, treeNodes){   
        return false;
    };

    function beforeCollapse(treeId, treeNodes){   
        return true;
    };

    function BuildTree(){
        $.each(menus,function(idx,obj){
            $.each(obj,function(idx,o){
                o.checked = menuids.indexOf(","+o.id+",")>=0? true:false;
            });
            var html = $("#tpl").html().replace("tpl","<ul id='menutree" + idx + "' class='ztree'></ul>");
            $(html).appendTo(".tdmenus");
            $.fn.zTree.init($("#menutree"+idx), setting, obj).expandAll(true);
        });
        $('.tinyscrollbar').tinyscrollbar();	
    };

    function GetSelectedIds(nodes){
        var ids = "";
        $.each(nodes,function(idx,obj){ ids += obj.id.toString()+","; });
        return ids;
    }

    $(function(){
        BuildTree();
        $("#SaveInfo").click(function(){
            var sels = "";
            for(var i=0;i<menus.length;i++){
                sels += GetSelectedIds($.fn.zTree.getZTreeObj("menutree"+i).getCheckedNodes(true));
            }
            $("#menuidlist").val(sels);
            __doPostBack("SaveInfo","");
        });
    });
    </script>
</body>
</html>