<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" CodeBehind="wfGaleriaPreview.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGaleriaPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Galeria Preview</h1>
    </div>
    <div>
        <!-- Ejemplo de etiquetas <span> para mostrar mensajes de error -->
        <span id="nombreZonaError" class="error-message"></span>
        <span id="fechaNacimientoError" class="error-message"></span>
        <span id="nombreResidenteError" class="error-message"></span>
        <span id="fechaIngresoError" class="error-message"></span>
        <span id="descripcionError" class="error-message"></span>
        <span id="fotoError" class="error-message"></span>

        <div class="content-container body-content">
            <div class="row">
                <div class="col-full">
                    <h1>Nuestros Mininos</h1>
                    <p>Lorem Ipsum pero aqui ponemos algo sobre como los gatos se mantienen alrededor del campus y que en esta galeria los pueden ver y si los ven en la uca que los respeten y asi.</p>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-full">
                    <div class="gallery-container">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <ul id="gallery" class="gallery">
                            <% foreach (var minino in CargarDatos())
                                { %>

                            <li class="card">
                                <div class="gallery-image">
                                    <% if (minino.foto != null)
                                        { %>
                                    <img src="data:image/jpeg;base64,<%= Convert.ToBase64String(minino.foto) %>" alt="logo" />
                                    <% }
                                    else
                                    { %>
                                    <img src="images/mininos_logo.png" alt="imagen por defecto" />
                                    <% } %>
                                </div>

                                <h2><%= minino.Residente.nombre ?? "" %></h2>
                                <h4><%= minino.Residente.descripcion ?? "" %></h4>
                                <h4>Habita por: <%= minino.Zona.nombre ?? "" %></h4>
                            </li>
                            <% } %>
                        </ul>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
