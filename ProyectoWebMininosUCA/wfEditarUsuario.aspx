<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEditarUsuario.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarUsuario" EnableEventValidation="false" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>USUARIOS</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR USUARIO </h1>
                    <div class="mininos-form">

                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                            <strong>Aviso:</strong>
                            <span id="lblAdvertencia" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtIdUsuarioEdit" Visible="false" runat="server" />

                        <div class="input-group">
                            <asp:TextBox ID="txtNombreEdit" runat="server" Placeholder="Nombres" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder(this)" />
                            <label id="lblNombre" for="txtNombre" class="label" runat="server">Nombres</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtApellidoEdit" runat="server" Placeholder="Apellidos" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder2(this)" />
                            <label id="lblApellido" for="txtApellido" class="label" runat="server">Apellidos</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtEmailEdit" runat="server" Placeholder="Email" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder3(this)" />
                            <label id="lblEmail" for="txtEmail" class="label" runat="server">Correo</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtTelefonoEdit" runat="server" Placeholder="Telefono" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder4(this)" />
                            <label id="lblTelefono" for="txtTelefono" class="label" runat="server">Telefono</label>
                        </div>

                        <div class="input-group">
                            <asp:TextBox ID="txtNombreUsuarioEdit" runat="server" Placeholder="Usuario" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder5(this)" />
                            <label id="lblUsername" for="txtNombreUsuario" class="label" runat="server">Usuario</label>
                        </div>

                         <div class="input-group">
                        <asp:TextBox ID="txtClaveEdit" runat="server" Placeholder="Clave" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder6(this)" />
                             <label id="lblClave" for="txtClave" class="label" runat="server">Clave</label>
                             </div>

                        <div class="input-group">
                        <asp:FileUpload ID="fuFotoEdit" runat="server" CssClass="mininos-input input-area" />
                            <label id="lblSeleccion" for="fuFoto" class="label" runat="server">Seleccione una foto</label>
                            </div>

                        <div class="input-group">
                        <asp:Image ID="imgFotoEdit" runat="server" Class="mininos-imagenes" />
                            <label id="lblFoto" for="fuFoto" class="label" runat="server">Foto</label>
                        </div>



                            <asp:Label ID="lblRol" runat="server" Text="Rol"></asp:Label>

                        <asp:DropDownList ID="ddlRolEdit" runat="server" Class="mininos-input" placeholder="Roles">
                        </asp:DropDownList>




                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnEditarUsuario" runat="server" OnClick="btnEditarUsuario_Click" Class="mininos-button-primary button" Text="Modificar Usuario"></asp:Button>
                      <br />
                        <br />

                </div>
            </div>
        </div>
    </div>

    
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= fuFotoEdit.ClientID %>').change(function () {
          if (this.files && this.files[0]) {
              var reader = new FileReader();

              reader.onload = function (e) {
                  $('#<%= imgFotoEdit.ClientID %>').attr('src', e.target.result);
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


