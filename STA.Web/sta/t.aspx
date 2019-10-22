<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="t.aspx.cs" Inherits="STA.Web.test" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script language="javascript" type="text/javascript" src="../admin/js/jquery.js"></script>
    <script type="text/javascript" src="../admin/plugin/scripts/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../admin/js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <cc1:textbox runat="server" ID="txt1" />
         <cc1:textbox runat="server" ID="txt2" />
         <cc1:textbox runat="server" ID="txt3" />
    <cc1:Button ID="Button1" runat="server" onclick="Button1_Click1" />
         <br />
         <cc1:TextBox ID="TextBox1" runat="server"></cc1:TextBox>
         <cc1:Button ID="Button2" runat="server" onclick="Button2_Click" />
         <span onclick="alert($('#txtDesc').val());">sdfsdf</span>

         <cc1:TextBox runat="server" ID="txtDesc" Width="406" TextMode="MultiLine" Height="150"/>
    <asp:Button ID="Button3" runat="server" Text="Button" onclick="Button3_Click" />
         <br />
         <br />
         <br />
         <cc1:TextBox ID="TextBox2" runat="server"></cc1:TextBox>
         <cc1:Button ID="Button4" runat="server" onclick="Button4_Click" />
        <script type="text/javascript">
            $("#txtDesc").xheditor(xhconfig);
    </script>


         <p>
             &nbsp;</p>
         <p>
             &nbsp;</p>
         替换内容：<cc1:TextBox ID="txtSource" runat="server"
             TextMode="MultiLine" Width="500" Height="300"></cc1:TextBox>
         <cc1:Button ID="Button5"
                 runat="server" Text="开始替换" onclick="Button5_Click"/>

         \<br />
         <br />
         下载：<cc1:TextBox ID="TextBox3" runat="server" Height="100px" 
             TextMode="MultiLine" Width="500px"></cc1:TextBox>
         <cc1:Button ID="Button6" runat="server" onclick="Button6_Click" />

    </form>
        

</body>
</html>
