<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfAgregarPublicacion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfAgregarPublicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Agregar Publicaciones</h1> 
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Nueva Publicación</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                           <strong>Aviso:</strong>
                           <span id="lblAdvertencia" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtIdPublicacion" Visible="false" runat="server" />

                        <asp:FileUpload ID="archivoPublicacion" runat="server" Placeholder="Archivo" Class="mininos-input" />

                        <asp:Image ID="imgFoto" runat="server" Class="mininos-input" BorderColor="White" />

                        <asp:TextBox ID="txtTituloPublicacion" runat="server" Placeholder="Título" Class="mininos-input" />

                        <asp:TextBox ID="txtTipoPublicacion" runat="server" Placeholder="Tipo" Class="mininos-input" />

                        <asp:TextBox ID="txtContenido" TextMode="MultiLine" Wrap="true" runat="server" Placeholder="Descripción" Height="200px" Class="mininos-input" />
                        
                        

                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnAgregarPublicacion" runat="server" Class="mininos-button-primary" Text="Agregar Publicación" OnClick="btnAgregarPublicacion_Click"></asp:Button>
                    <br />
                    <asp:Button ID="btnLimpiarCampos" runat="server" OnClick="btnLimpiarCampos_Click" Class="mininos-button-primary button" Text="Limpiar"></asp:Button>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= archivoPublicacion.ClientID %>').change(function () {
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

</asp:Content>
