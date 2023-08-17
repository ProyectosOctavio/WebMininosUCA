<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEditarContacto.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarContacto" EnableEventValidation="false" EnableViewState="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>CONTACTO</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR CONTACTO</h1>
                    <div class="mininos-form">
                        <div class="mininos-form">
                           
                            <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                                <strong>Aviso:</strong>
                                <span id="lblAdvertencia" runat="server"></span>
                            </div>

                            <asp:TextBox ID="txtIdEdit" Visible="false" runat="server" />

                            <div class="input-group">
                                <asp:TextBox ID="txtTelefonoEdit" runat="server" Placeholder="Telefono" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder(this)" />
                                <label id="lblTelefono" for="txtTelefono" class="label" runat="server">Telefono</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtCorreoEdit" runat="server" Placeholder="Correo" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder2(this)" />
                                <label id="lblCorreo" for="txtCorreoEdit" class="label" runat="server">Correo</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtCorreo2Edit" runat="server" Placeholder="Segundo correo" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder3(this)" />
                                <label id="lblCorreo2" for="txtCorreo2Edit" class="label" runat="server">Segundo correo</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtTwitterEdit" runat="server" Placeholder="Twitter" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder4(this)" />
                                <label id="lblTwitter" for="txtTwitterEdit" class="label" runat="server">Twitter</label>
                            </div>

                            <div class="input-group">
                                <asp:TextBox ID="txtInstaEdit" runat="server" Placeholder="Instagram" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder5(this)" />
                                <label id="lblInsta" for="txtInstaEdit" class="label" runat="server">Instagram</label>
                            </div>

                             <div class="input-group">
                                <asp:TextBox ID="txtFacebookEdit" runat="server" Placeholder="Facebook" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder6(this)" />
                                <label id="lblFacebook" for="txtFacebookEdit" class="label" runat="server">Facebook</label>
                            </div>

                            
                        <br />
                        <br />
                        <br />

                            </div>
                    <asp:Button ID="btnEditarContacto" runat="server" OnClick="btnEditarContacto_Click" Class="mininos-button-primary button" Text="Modificar Contacto"></asp:Button>
                      <br />
                        <br />

                </div>
            </div>
        </div>
    </div>
        </div>


                              <script>

        function clearPlaceholder(element) {
            element.placeholder = '';
        }

        function restorePlaceholder(element) {
            element.placeholder = 'Telefono';
        }

        function restorePlaceholder2(element) {
            element.placeholder = 'Correo';
        }

        function restorePlaceholder3(element) {
            element.placeholder = 'Segundo correo';
        }

        function restorePlaceholder4(element) {
            element.placeholder = 'Twitter';
        }

         function restorePlaceholder5(element) {
            element.placeholder = 'Instagram';
        }

          function restorePlaceholder6(element) {
            element.placeholder = 'Facebook';
        }

    </script>



</asp:Content>