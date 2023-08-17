<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEditarPublicacion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarPublicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner-medium">
        <h1>Editar Publicaciones</h1> 
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>Editar Publicación</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
                           <strong>Aviso:</strong>
                           <span id="lblAdvertencia" runat="server"></span>
                        </div>

                        <asp:TextBox ID="txtIdPublicacionEdit" Visible="false" runat="server" />

                        <asp:FileUpload ID="archivoPublicacionEdit" runat="server" Placeholder="Archivo" Class="mininos-input" />

                        <asp:Image ID="imgFotoEdit" runat="server" Class="mininos-input" BorderColor="White"/>

                        <asp:TextBox ID="txtTituloPublicacionEdit" runat="server" Placeholder="Título" Class="mininos-input" />

                        <asp:TextBox ID="txtTipoPublicacionEdit" runat="server" Placeholder="Tipo" Class="mininos-input" />

                        <asp:TextBox ID="txtContenidoEdit" TextMode="MultiLine" Wrap="true" runat="server" Placeholder="Descripción" Height="200px" Class="mininos-input" />



                        <br />
                        <br />
                        <br />

                    </div>
                    <asp:Button ID="btnEditarPublicacion" runat="server" Class="mininos-button-primary" Text="Editar Publicacion" OnClick="btnEditarPublicacion_Click" ></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= archivoPublicacionEdit.ClientID %>').change(function () {
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

</asp:Content>
