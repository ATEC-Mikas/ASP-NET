<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Mota.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="index.css" />
    <script src="index.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyUpper" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyMiddle" runat="server">
    <div>
      <div class="row">
        <h2>Teste</h2>
        <div id="formP">
            <asp:TextBox placeholder="Primeiro Nome" id="pname" runat="server"></asp:TextBox>
            <asp:TextBox placeholder="Ultimo Nome" id="uname" runat="server"></asp:TextBox>
            <asp:Button runat="server" Text="Ok" id="buttonOk" OnClick="buttonOK_Click" />
            <span id="erro"><% Context.Response.Write(ViewState["erro"]); %></span>
            <span id="resposta"><% Context.Response.Write(ViewState["resposta"]); %></span>
        </div>
    </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyLower" runat="server">
</asp:Content>
