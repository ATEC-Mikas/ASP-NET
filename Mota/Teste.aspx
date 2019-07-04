<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teste.aspx.cs" Inherits="Mota.Teste" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="index.css" />
</head>
<body>
    <div class="row">
        <h2>Teste</h2>
        <form id="formP" runat="server">
            <asp:TextBox placeholder="Primeiro Nome" id="pname" runat="server"></asp:TextBox>
            <asp:TextBox placeholder="Ultimo Nome" id="uname" runat="server"></asp:TextBox>
            <asp:Button runat="server" Text="Ok" id="buttonOk" OnClick="buttonOK_Click" />
            <span id="erro"><% Context.Response.Write(ViewState["erro"]); %></span>
            <span id="resposta"><% Context.Response.Write(ViewState["resposta"]); %></span>
        </form>
    </div>
</body>
    <script src="index.js"></script>
</html>
