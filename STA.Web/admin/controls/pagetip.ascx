<%@ Control Language="c#" AutoEventWireup="false" CodeBehind="pagetip.ascx.cs" Inherits="STA.Web.Admin.UserControls.pagetip" %>
<div class="lightip" id="<%=this.ID %>" style="<%=this.Style %>">
    <div class="close" title="点击关闭" onclick="document.getElementById('<%=this.ID %>').style.display='none';">x</div>
    <div class="msg" style="background-image:url('<%=GetInfoImg()%>')"><%=this.Text %></div>
</div>