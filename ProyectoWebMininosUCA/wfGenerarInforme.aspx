<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfGenerarInforme.aspx.cs" Inherits="Registro.wfInformeIncidentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Generar Nuevo Informe</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Generar Informe</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>
                        <asp:DropDownList ID="ddlInforme" runat="server" Class="mininos-input" placeholder="Tipo de Informe">
                            <asp:ListItem Text="Informe a Generar" Value=""></asp:ListItem>
                            <asp:ListItem Text="Residentes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Donacion Monetaria" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Donacion Especie" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Incidentes" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Donantes" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                        <div class="date-selector">
                            <input type="date" id="txtFechaInicio" runat="server" class="mininos-input" />
                        </div>

                        <div class="date-selector">
                            <input type="date" id="txtFechaFin" runat="server" class="mininos-input" />
                        </div>

                        <asp:Button ID="btnGenerarInforme" runat="server" Class="mininos-button-primary" Text="Generar" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
