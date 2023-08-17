<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfGaleria.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGaleria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Galeria</h1>
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
                    <p>¡Descubre la encantadora presencia de nuestros amigos felinos en el campus! Los adorables gatos se han convertido en habitantes habituales de nuestra galería fotográfica. Si tienes la suerte de encontrarte con ellos en la UCA, te pedimos que los respetes y admires desde la distancia. ¡Explora nuestra galería para ver estas adorables criaturas en todo su esplendor!</p>
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
