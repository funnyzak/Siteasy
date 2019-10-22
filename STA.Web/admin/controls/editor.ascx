<%@ Control Language="c#" AutoEventWireup="false" CodeBehind="editor.ascx.cs" Inherits="STA.Web.Admin.UserControls.editor" %>
                    <script type="text/javascript" src="../../sta/editor/ckeditor/<%=browser.IndexOf("ie")>=0?"3.6.3":"4.1.1"%>/ckeditor.js"></script> 
                    <textarea runat="server" id="nContent"/>
                    <script type="text/javascript">
                        CKEDITOR.replace('<%=this.ClientID%>', { <%=editorset%> });
                    </script> 