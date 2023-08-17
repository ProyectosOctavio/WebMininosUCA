<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEliminarRolOpcion.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarRolOpcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <div class="banner-medium">
        <h1>Eliminar Rol Opcion</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar el Rol?</h1>
                    <div class="mininos-form">

    <asp:TextBox ID="txtIdEliminar" Visible="false" runat="server" Enabled="false"  />

                        <asp:Label ID="lblRol" runat="server" Text="Rol"></asp:Label>
                        <asp:DropDownList ID="ddlRolEliminar" runat="server" Class="mininos-input" placeholder="Roles" Enabled="false">                
                        </asp:DropDownList>

                        <asp:Label ID="lblOpcion" runat="server" Text="Opcion"></asp:Label>
                        <asp:DropDownList ID="ddlOpcionEliminar" runat="server" Class="mininos-input" placeholder="Opciones" Enabled="false">
                        </asp:DropDownList>

                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar RolOpcion" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
