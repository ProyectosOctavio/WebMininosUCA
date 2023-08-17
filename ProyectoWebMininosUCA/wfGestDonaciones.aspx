<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfGestDonaciones.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestDonaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner-medium">
        <h1>Donaciones</h1>
    </div>

    <span id="montoNI" class="error-message"></span>
    <span id="fecha" class="error-message"></span>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <h1>Activas</h1>

                <ul class="mininos-entry-list">
                    <% foreach (var activo in cargarDonacion("activo", 2)) { %>

                    <li class="card">
                        <div class="mininos-donation-img">
                            <img src="../images/donacion_monetaria_logo.png" alt="monetaria" />
                        </div>
                        <div class="mininos-donacion-activa">
                            <div class="info">
                                <h1>Donacion Monetaria</h1>
                                <h3>Para comida</h3>
                                <% foreach (var donacionMonetaria in activo.DonacionMonetaria) { %>
                                <h3><%= donacionMonetaria.fecha.ToString("dd/MM/yyyy") %></h3>
                                <h4><%= donacionMonetaria.montoNi %></h4>
                                <% } %>
                            </div>
                        </div>
                    </li>
                    <% } %>
                </ul>


                <ul class="mininos-entry-list">
                    <% foreach (var activo in cargarDonacion("activo", 2)) { %>
                    <li class="card">
                        <div class="mininos-donation-img">
                            <img src="images/donacion_especias_logo.png" alt="monetaria" />
                        </div>
                        <div class="mininos-donacion-activa">
                            <div class="info">
                                <h1>Donacion de Especias</h1>

                                <% foreach (var donacionMonetaria in activo.DonacionMonetaria) { %>
                                <% foreach (var donacionEspecie in activo.DonacionEspecies) { %>
                                <h3><%= donacionEspecie.tipoEspecie %></h3>
                                <h3><%= donacionMonetaria.montoNi %></h3>
                                <h4><%= donacionEspecie.fecha.ToString("dd/MM/yyyy") %></h4>
                                <% } %>
                                <% } %>
                            </div>
                        </div>
                    </li>
                    <% } %>
                </ul>


            </div>
        </div>
        <hr />


        <div class="row">
            <div class="col-full">
                <h1 class="hi">Historial</h1>
                <ul class="mininos-entry-list">

                    <% foreach (var historial in cargarDonacion("historial", 3)) { %>
                    <li class="mininos-entry">
                        <div class="mininos-donation-img">
                            <img src="../images/donacion_monetaria_logo.png" alt="monetaria" />
                        </div>
                        <div class="mininos-donacion-activa">
                            <div class="info">
                                <h1>Donacion Monetaria</h1>
                                <h3>Para comida</h3>
                                <% foreach (var donacionMonetaria in historial.DonacionMonetaria) { %>
                                <h3><%= donacionMonetaria.montoNi %></h3>
                                <h4><%= donacionMonetaria.fecha.ToString("dd/MM/yyyy") %></h4>
                                <% } %>
                            </div>
                        </div>
                    </li>
                    <% } %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
