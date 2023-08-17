<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfRecuperarContrasena.aspx.cs" Inherits="RecuperarContraseñaMininosUca.wfRecuperarContraseña"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Recuperar Contraseña</h1>
    </div>
    <link rel="stylesheet" href="Content/MininosStyleSheet.css" />

    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Recuperar Contraseña</h1>
                    <div class="mininos-form">
                        <asp:Label runat="server" CssClass="row p">¿Tienes problemas para recuperar o recordar tu contraseña?</asp:Label>
                        <asp:Label runat="server" CssClass="row p">Ingresa un correo electrónico seguro y te enviaremos un enlace para que recuperes el acceso a tu cuenta.</asp:Label>
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <asp:TextBox ID="txtIdCorreo" runat="server" CssClass="mininos-input" />
                        <asp:Button ID="btnEnviarSoli" runat="server" CssClass="mininos-button-primary button" Text="Enviar Solicitud" OnClick="btnEnviarSoli_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>