<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Residente.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="wfEliminarResidente.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarResidente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
        <div class="banner-medium">
        <h1>Eliminar residente</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Eliminar un residente</h1>
                     <div class="mininos-form">
                         <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
</div>
                         <asp:TextBox ID="txtId_residenteEliminar" Visible="false" runat="server" />

             <asp:TextBox ID="txtNombreEliminar" runat="server" Class="mininos-input" placeholder="Nombre"/>

             <asp:TextBox ID="txtDescripcionEliminar" runat="server" Class="mininos-input" placeholder="Descripcion"/>

             <asp:DropDownList ID="ddlZonaEliminar" runat="server" Class="mininos-input">
                 <asp:ListItem Text="Zona" Value=""></asp:ListItem>
             </asp:DropDownList>

                         <asp:FileUpload ID="fuResidenteEliminar" runat="server" Class="mininos-input"/>
                             <asp:Image ID="imgFotoEliminar" runat="server" CssClass="mininos-imagenes" />

            
        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                     <br />
    <asp:Button ID="btnEliminarResidente" runat="server"   Class="mininos-button-primary"  Text="Eliminar residente" OnClick="btnEliminarResidente_Click"> </asp:Button>
                     <br />

                     </div>
                     </div>
                         </div>
       </div> 
                 </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= fuResidenteEliminar.ClientID %>').change(function () {
          if (this.files && this.files[0]) {
              var reader = new FileReader();

              reader.onload = function (e) {
                  $('#<%= imgFotoEliminar.ClientID %>').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(this.files[0]);
                }
            });
        });
    </script>

    </asp:Content>