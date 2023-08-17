<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfLogin.aspx.cs" Inherits="ProyectoWebMininosUCA.wfLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner">
        <div class="row">
            <div class="col-half">
                <div class="login-logo-center">
                    <img src="images/mininos_logo.png" alt="logo" width="400" height="400" />
                </div>
            </div>
            <div class="col-half">
                <div class="mininos-form-container">
                    <div class="mininos-login-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <asp:TextBox ID="txtUsername" runat="server" Class="mininos-input" placeholder="Ingrese su usuario" />
                        <asp:TextBox ID="txtPassword" runat="server" Class="mininos-input" TextMode="Password" placeholder="Ingrese su clave" />
                        <asp:Button ID="btnLogin" runat="server" Class="mininos-button-primary button" Text="Ingresar" OnClick="btnLogin_Click"></asp:Button>
                        <a runat="server" href="~/wfRecuperarContrasena">¿Restablecer Contraseña?</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
