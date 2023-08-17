<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Donacion.Master" AutoEventWireup="true" CodeBehind="wfEliminarMoneda.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarMoneda" EnableEventValidation="false" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="banner-medium">
        <h1>Eliminar donacion monetaria</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar la donacion?</h1>
                    <div class="mininos-form">
   
   
    
                        <asp:TextBox ID="txtIdMonedaEliminar" Visible="false" runat="server" />

                         <asp:TextBox ID="txtIdDonanteEliminar" Visible="false" runat="server"  />

                        <asp:TextBox ID="txtNombreEliminar" Enabled="false" Class="mininos-input"  runat="server"  Placeholder="Nombres" />

                        <asp:TextBox ID="txtApellidoEliminar" Enabled="false" Class="mininos-input"  runat="server" Placeholder="Apellidos"  />

                        <asp:TextBox ID="txtAliasEliminar" Enabled="false" Class="mininos-input" runat="server" Placeholder="Alias"  />


                         <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label>

                        <asp:TextBox ID="txtMontoEliminar" Enabled="false"   runat="server" Placeholder="Monto" Class="mininos-input" />


                        
                       <asp:FileUpload ID="fuFotoEliminar" runat="server" Enabled="false" Class="mininos-input" />

                                <asp:Image ID="imgFotoEliminar" runat="server" Class="mininos-imagenes"  />





                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar donacion" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
