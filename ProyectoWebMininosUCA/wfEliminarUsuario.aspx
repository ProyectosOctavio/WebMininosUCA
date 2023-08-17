<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Seguridad.Master" AutoEventWireup="true" CodeBehind="wfEliminarUsuario.aspx.cs" Inherits="ProyectoWebMininosUCA.wfEliminarUsuario" EnableEventValidation="false"  EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="banner-medium">
        <h1>USUARIOS</h1>
    </div>
    <div class="content-container body-content">
        <div class="row">
            <div class="col-full">
                <div class="mininos-form-container">
                    <h1>¿Desea eliminar el usuario?</h1>
                    <div class="mininos-form">
   
        
   
    <asp:TextBox ID="txtIdUsuarioEliminar" Visible="false" runat="server" Enabled="false"  />
   
    <asp:TextBox ID="txtNombreEliminar" runat="server"  Placeholder="Nombres" Enabled="false" Class="mininos-input" />

    
    <asp:TextBox ID="txtApellidoEliminar" runat="server" Placeholder="Apellidos" Enabled="false" Class="mininos-input" />

    <asp:TextBox ID="txtEmailEliminar" runat="server" Placeholder="Email" Enabled="false" Class="mininos-input" />

     <asp:TextBox ID="txtTelefonoEliminar" runat="server" Placeholder="Telefono" Enabled="false" Class="mininos-input" />

 
    <asp:TextBox ID="txtNombreUsuarioEliminar" runat="server" Placeholder="Nombre de usuario" Enabled="false" Class="mininos-input" />
   
    
    <asp:TextBox ID="txtClaveEliminar" runat="server" Enabled="false" Placeholder="Clave" Class="mininos-input"  />

<asp:FileUpload ID="fuFotoEliminar" runat="server" Enabled="false" Class="mininos-input" />

                                <asp:Image ID="imgFotoEliminar" runat="server" Class="mininos-imagenes"  />

                            <asp:Label ID="lblRol" runat="server" Text="Rol"></asp:Label>

                          <asp:DropDownList ID="ddlRolEliminar" runat="server" Class="mininos-input" placeholder="Roles" Enabled="false">
                            
                        </asp:DropDownList>




                        <br />
                        <br />
                        <br />

 </div>
     <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Class="mininos-button-primary button" />
                     <br />
                        <br />
                        <br />
    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar Usuario" Class="mininos-button-primary button"></asp:Button>
                    
   </div>
            </div>
        </div>
    </div>
</asp:Content>
