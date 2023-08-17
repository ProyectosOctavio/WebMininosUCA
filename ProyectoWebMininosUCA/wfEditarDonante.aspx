<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfEditarDonante.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarDonante" EnableEventValidation="false" EnableViewState="true" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Gestionar Donantes</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
        <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR DONANTE</h1>
                    <div class="mininos-form">

                        <div id="pnlAdvertenciaEdit" runat="server" class="alert alert-warning" visible="false">
                        <strong>Aviso:</strong>
                             <span id="lblAdvertenciaEdit" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtIdEdit" Visible="false" runat="server" />

                        <div class="input-group">
                            <asp:TextBox ID="txtNombreEdit" runat="server" Placeholder="Nombres" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder(this)" />
                            <label id="lblNombre" for="txtNombre" class="label" runat="server">Nombres</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtApellidoEdit" runat="server" Placeholder="Apellidos" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder2(this)" />
                            <label id="lblApellido" for="txtApellido" class="label" runat="server">Apellidos</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtCorreoEdit" runat="server" Placeholder="Correo" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder3(this)" />
                            <label id="lblCorreo" for="txtCorreo" class="label" runat="server">Correo</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtTelefonoEdit" runat="server" Placeholder="Telefono" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder4(this)" />
                            <label id="lblTelefono" for="txtTelefono" class="label" runat="server">Telefono</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtAliasEdit" runat="server" Placeholder="Alias" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder5(this)" />
                            <label id="lblAlias" for="txtAlias" class="label" runat="server">Alias</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtPaisEdit" runat="server" Placeholder="Pais" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder6(this)" />
                            <label id="lblPais" for="txtPais" class="label" runat="server">Pais</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtCiudadEdit" runat="server" Placeholder="Ciudad" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" />
                            <label id="lblCiudad" for="txtCiudad" class="label" runat="server">Ciudad</label>
                        </div>

                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnEditarDonante" runat="server" OnClick="btnEditarDonante_Click" Class="mininos-button-primary button" Text="Modificar Donante"></asp:Button>
                                            <br />
                        <br />

                </div>
            </div>
        </div>
    </div>

    <script>
        function clearPlaceholder(element) {
            element.placeholder = '';
        }

        function restorePlaceholder(element) {
            element.placeholder = 'Nombres';
        }

        function restorePlaceholder2(element) {
            element.placeholder = 'Apellidos';
        }

        function restorePlaceholder3(element) {
            element.placeholder = 'Correo';
        }

        function restorePlaceholder4(element) {
            element.placeholder = 'Telefono';
        }

        function restorePlaceholder5(element) {
            element.placeholder = 'Alias';
        }

        function restorePlaceholder6(element) {
            element.placeholder = 'Pais';
        }

        function restorePlaceholder7(element) {
            element.placeholder = 'Ciudad';
        }

   </script>




</asp:Content>