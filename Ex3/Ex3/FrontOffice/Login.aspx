<%@ Page Title="Login" Language="C#" MasterPageFile="~/FrontOffice/FrontOffice.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ex3.Pages.FrontOffice.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        div.error {
            margin-top:20px;
            padding:10px;
            color:red;
        }
        div.form-box {
            display:flex;
            flex-direction:column;
        }
        div.form-box > * {
            margin:5px 0;
        }
    </style>
</asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
        <div id="info" class="info">
            <%= (ViewState["info"] == null ? "" : ViewState["info"]) %>
        </div>

        <%if (ViewState["Registar"] == null || ViewState["Registar"] == "") {%>
            <h2>Login</h2>

        <div class="form-box">
            <asp:TextBox ID="txtNickname" placeholder="Nickname" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" TextMode="Password" ></asp:TextBox>
            <asp:Button  ID="BtnLogin" runat="server" OnClick="BtnLogin_Click"  Text="Login"/>
            <asp:LinkButton ID="linkRegistar" runat="server" OnClick="linkRegistar_Click">Fazer o Registo</asp:LinkButton>
        </div>

        <div id="error" class="error">
            <%= (ViewState["error"] == null ? "" : ViewState["error"]) %>
        </div>

        <script>
            var nickname = document.getElementById("<%=txtNickname.ClientID%>");
            var password = document.getElementById("<%=txtPassword.ClientID%>");
            var btnLogin = document.getElementById("<%=BtnLogin.ClientID%>");
            var error = document.getElementById("error");
            btnLogin.addEventListener("click", event => {
                error.innerText = "";
                if (nickname.value == "") {
                    event.preventDefault();
                    error.innerText += "Invalid Nickname \n";
                }
                if (password.value == "") {
                    event.preventDefault();
                    error.innerText += "Invalid Password \n";
                }
                
            });
        </script>
        <%} else {%>
            <h2>Registar</h2>
        <div class="form-box">
            <asp:TextBox id="txtRNickname" placeholder="Nickname" runat="server"></asp:TextBox>
            <asp:TextBox id="txtRName" placeholder="Name" runat="server"></asp:TextBox>
            <asp:TextBox id="txtRPassword" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:TextBox id="txtRConfPassword" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Button id="btnRegistar" text="Registar" runat="server" OnClick="btnRegistar_Click"/>
            <asp:LinkButton runat="server" id="linkLogin" OnClick="linkLogin_Click">Fazer Login</asp:LinkButton>
        </div>
           
        <div id="error" class="error">
            <%= (ViewState["error"] == null ? "" : ViewState["error"]) %>
        </div>

        <%} %>
    </asp:Content>
