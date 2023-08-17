<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfVerPublicaciones.aspx.cs" Inherits="ProyectoWebMininosUCA.wfVerPublicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Publicaciones</h1>
    </div>
        <span id="Foto" class="error-message"></span>
        <span id="Titulo" class="error-message"></span>
        <span id="Type" class="error-message"></span>
        <span id="Descripcion" class="error-message"></span>
        <span id="Fecha" class="error-message"></span>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="publications-container">
                    <ul id="publications" class="publications">
                        <% foreach (var minino in CargarPublicacion())
                            { %>
                            <li class="card">
                            <div class="publication-image">
                                <img src="data:image/jpeg;base64,<%= Convert.ToBase64String(minino.fotoPublicacion) %>" alt="logo" />
                            </div>
                            <h2><%= minino.titulo %></h2>
                            <h4><%= minino.fecha.ToString("dd/MM/yyyy") %></h4>
                            <h2><%= minino.tipo %></h2>
                            <h4><%= minino.contenido %></h4>
                            </li>
                        <% } %>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content> 
