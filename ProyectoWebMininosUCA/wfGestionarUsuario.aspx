<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfGestionarUsuario.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestionarUsuarios" EnableEventValidation="false" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>USUARIOS</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>AGREGAR USUARIOS</h1>
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
                            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder3(this)" />
                            <label id="lblEmail" for="txtEmail" class="label" runat="server">Correo</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Telefono" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder4(this)" />
                            <label id="lblTelefono" for="txtTelefono" class="label" runat="server">Telefono</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtNombreUsuario" runat="server" Placeholder="Usuario" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder5(this)" />
                            <label id="lblUsername" for="txtNombreUsuario" class="label" runat="server">Usuario</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtClave" runat="server" Placeholder="Clave" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder6(this)" />
                            <label id="lblClave" for="txtClave" class="label" runat="server">Clave</label>
                        </div>

                        <div class="input-group">
                            <asp:FileUpload ID="fuFoto" runat="server" CssClass="mininos-input input-area" />
                            <label id="lblSeleccion" for="fuFoto" class="label" runat="server">Seleccione una foto</label>
                        </div>

                        <div class="input-group">
                            <asp:Image ID="imgFoto" runat="server" Class="mininos-imagenes" />
                            <label id="lblFoto" for="fuFoto" class="label" runat="server">Foto</label>
                        </div>


                        <asp:Label ID="lblRol" runat="server" Text="Rol"></asp:Label>



                        <asp:DropDownList ID="ddlRol" runat="server" Class="mininos-input" placeholder="Roles">
                        </asp:DropDownList>



                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnGuardarUsuario" runat="server" OnClick="guardarUsuario_Click" Class="mininos-button-primary button" Text="Guardar"></asp:Button>
                    <br />
                    <br />
                    <asp:Button ID="btnLimpiarCampos" runat="server" OnClick="limpiarUsuario_Click" Class="mininos-button-primary button" Text="Limpiar"></asp:Button>

                    <br />
                    <br />
                    <br />

                </div>

                <br />
                <br />
                <br />

                <div class="form-row-2">
                    <asp:TextBox ID="txtBusqueda" runat="server" Placeholder="Ingresa un dato a buscar" Class="mininos-input"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Class="mininos-button-secondary" />
                </div>
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="row">
                        <div class="col-full">
                            <div class="table-responsive">
                                <asp:GridView ID="gvUsuario" runat="server" OnRowDataBound="gvUsuario_RowDataBound" AllowPaging="true" OnPageIndexChanging="gv_Usuario_PageIndexChanging"
                                    CssClass="table table-bordered table-condensed table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:Button runat="server" CssClass="btn btn-primary " Text="Editar" ID="btnEditarUsuario" OnClick="btnEditarUsuario_Click" />
                                                <asp:Button runat="server" CssClass="btn btn-primary btn-eliminar" Text="Eliminar" ID="btnEliminarUsuario" OnClick="btnEliminarUsuario_Click" />
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

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= fuFoto.ClientID %>').change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#<%= imgFoto.ClientID %>').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(this.files[0]);
                }
            });
        });
    </script>

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
            element.placeholder = 'Telefono ';
        }

        function restorePlaceholder5(element) {
            element.placeholder = 'Usuario';
        }

        function restorePlaceholder6(element) {
            element.placeholder = 'Clave';
        }

    </script>




</asp:Content>
