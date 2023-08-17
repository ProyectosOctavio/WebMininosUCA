<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfGestDonantes.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestDonante" EnableEventValidation="false" EnableViewState="true" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Gestionar Donantes</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
        <div class="col-full">
                <div class="mininos-form-container">
                    <h1>AGREGAR DONANTE</h1>
                    <div class="mininos-form">

                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                        <strong>Aviso:</strong>
                             <span id="lblAdvertencia" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtId" Visible="false" runat="server" />

                        <div class="input-group">
                            <asp:TextBox ID="txtNombre" runat="server" Placeholder="Nombres" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder(this)" />
                            <label id="lblNombre" for="txtNombre" class="label" runat="server">Nombres</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtApellido" runat="server" Placeholder="Apellidos" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder2(this)" />
                            <label id="lblApellido" for="txtApellido" class="label" runat="server">Apellidos</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtCorreo" runat="server" Placeholder="Correo" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder3(this)" />
                            <label id="lblCorreo" for="txtCorreo" class="label" runat="server">Correo</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Telefono" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder4(this)" />
                            <label id="lblTelefono" for="txtTelefono" class="label" runat="server">Telefono</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtAlias" runat="server" Placeholder="Alias" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder5(this)" />
                            <label id="lblAlias" for="txtAlias" class="label" runat="server">Alias</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtPais" runat="server" Placeholder="Pais" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder6(this)" />
                            <label id="lblPais" for="txtPais" class="label" runat="server">Pais</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtCiudad" runat="server" Placeholder="Ciudad" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" />
                            <label id="lblCiudad" for="txtCiudad" class="label" runat="server">Ciudad</label>
                        </div>

                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnGuardarDonante" runat="server" OnClick="guardarDonante_Click" Class="mininos-button-primary button" Text="Guardar"></asp:Button>
                                            <br />
                        <br />
                    <asp:Button ID="btnLimpiarCampos" runat="server" OnClick="limpiarDonante_Click" Class="mininos-button-primary button" Text="Limpiar"></asp:Button>

                    <br />
                    <br />
                    <br />

                </div>

                    <br />
                    <br />
                    <br />

            <div class="scrollable-gridview">
                <div class="row">
                         <div class="row">
                             <div class="col-full">
                                 <div class="table-responsive">
                                     <asp:GridView ID="gvDonante" runat="server" OnRowDataBound="gvDonante_RowDataBound" AllowPaging="true" OnPageIndexChanging="gv_Donante_PageIndexChanging"
                                         CssClass="table table-bordered table-condensed table-responsive table-hover ">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Acciones">
                                                 <ItemTemplate>
                                                     <asp:Button runat="server" Class="btn btn-primary" Text="Editar" ID="btnEditarDonante" OnClick="btnEditarDonante_Click" />
                                                    <asp:Button runat="server" CssClass="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarDonante" OnClick="btnEliminarDonante_Click" />
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                         <AlternatingRowStyle BackColor="White" />
                                         <HeaderStyle BackColor=" #01ADC3" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
                                         <RowStyle BackColor="#f5f5f5" />
                                         <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                                     </asp:GridView>
                                 </div>
                             </div>
                         </div>
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