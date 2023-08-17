<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfEliminarDonante.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarDonante" EnableEventValidation="false"  EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="banner-medium">
        <h1>Eliminar donante</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar el donante?</h1>
                    <div class="mininos-form">


            
   
    <asp:TextBox ID="txtIdEliminar" Visible="false" runat="server" Enabled="false"  />
   
    <asp:TextBox ID="txtNombreEliminar" runat="server"  Placeholder="Nombres" Enabled="false" Class="mininos-input" />

    
    <asp:TextBox ID="txtApellidoEliminar" runat="server" Placeholder="Apellidos" Enabled="false" Class="mininos-input" />

    <asp:TextBox ID="txtCorreoEliminar" runat="server" Placeholder="Correo" Enabled="false" Class="mininos-input" />

     <asp:TextBox ID="txtTelefonoEliminar" runat="server" Placeholder="Telefono" Enabled="false" Class="mininos-input" />

 
    <asp:TextBox ID="txtAliasEliminar" runat="server" Placeholder="Alias" Enabled="false" Class="mininos-input" />
   
    
    <asp:TextBox ID="txtPaisEliminar" runat="server" Enabled="false" Placeholder="Pais" Class="mininos-input"  />

    <asp:TextBox ID="txtCiudadEliminar" runat="server" Enabled="false" Placeholder="Ciudad" Class="mininos-input"  />  
                                
                                <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar Donante" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
