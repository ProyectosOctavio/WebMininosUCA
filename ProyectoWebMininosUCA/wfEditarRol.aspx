<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEditarRol.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEditarRol" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="banner-medium">
        <h1>Editar Roles</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>EDITAR ROLES</h1>
                    <div class="mininos-form">
                        <div id="pnlAdvertencia" runat="server" class="alert alert-warning" visible="false">
    <strong>Aviso:</strong>
    <span id="lblAdvertencia" runat="server"></span>
                            </div>
   
        
   
    <asp:TextBox ID="txtIdRolEdit" Visible="false" runat="server"  /> 
                        
     <div class="input-group">
    <asp:TextBox ID="txtNombreEdit" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Nombre"></asp:TextBox>
    <label id="lblNombreEdit" for="txtNombreEdit" class="label" runat="server">Nombre</label>
    </div>  

    <div class="input-group">
    <asp:TextBox ID="txtDescripcionEdit" runat="server" CssClass="mininos-input input-area" onfocus="clearPlaceholder(this)" onblur="restorePlaceholder7(this)" Placeholder="Descripcion"></asp:TextBox>
    <label id="lblDescripcionEdit" for="txtDescripcionEdit" class="label" runat="server">Descripcion</label>
    </div>

         

                        <br />
                        <br />
                        <br />

 </div>
    <asp:Button ID="btnEditarRol" runat="server"  OnClick="btnEditarRol_Click" Class="mininos-button-primary button"  Text="Modificar Rol"></asp:Button>
                     <br/>
                    <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                    
   </div>
            </div>
        </div>
    </div>
    
 
</asp:Content>
