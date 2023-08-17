<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEliminarRol.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarRol" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <div class="banner-medium">
        <h1>Eliminar roles </h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar el Rol?</h1>
                    <div class="mininos-form">
   
        
   
    <asp:TextBox ID="txtIdRolEliminar" Visible="false" runat="server" Enabled="false"  />
   
    <div class="input-group">
    <asp:TextBox ID="txtNombreEliminar" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Nombre"></asp:TextBox>
    <label id="lblNombreEliminar" for="txtNombreEliminar" class="label" runat="server">Nombre</label>
    </div>  

    <div class="input-group">
    <asp:TextBox ID="txtDescripcionEliminar" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Descripcion"></asp:TextBox>
    <label id="lblDescripcionEliminar" for="txtDescripcionEliminar" class="label" runat="server">Descripcion</label>
    </div>
 

                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar Rol" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
