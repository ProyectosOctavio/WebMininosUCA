<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfNuevaContraseña.aspx.cs" Inherits="ProyectoWebMininosUCA.wfNuevaContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Nueva Contraseña</h1>
    </div>
    <link rel="stylesheet" href="Content/MininosStyleSheet.css" />

    <div class="content-container body-content">

        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Ingrese su nueva contraseña</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <asp:TextBox ID="txtCorreo" runat="server" placeholder="Introduzca su correo" Class="mininos-input" type="email" />
                        <asp:TextBox ID="txtNuevapPw" runat="server" Placeholder="Nueva contraseña" Class="mininos-input" TextMode="Password" />
                        <div class="password-container">
                            <asp:TextBox ID="txtCorfirmacionPw" runat="server" Placeholder="Confirmar contraseña" Class="mininos-input" TextMode="Password" />
                            <span class="password-toggle" onclick="togglePasswordVisibility(this)"></span>
                        </div>
                        <asp:Button ID="btnGuardarPw" runat="server" Class="mininos-button-primary button" Text="Guardar Contraseña" OnClick="btnGuardarPw_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
